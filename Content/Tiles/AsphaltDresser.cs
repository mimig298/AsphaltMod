using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltDresser : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileContainer[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.BasicDresser[Type] = true;
        TileID.Sets.AvoidedByNPCs[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;
        TileID.Sets.IsAContainer[Type] = true;
        TileID.Sets.PreventsTileHammeringIfOnTopOfIt[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

        AdjTiles = [TileID.Dressers];
        DustType = -1;

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Dressers, 0));
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Dresser"), MapChestName);
    }

    public override LocalizedText DefaultContainerName(int frameX, int frameY) => CreateMapEntryName();

    private static string MapChestName(string name, int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int left = i - tile.TileFrameX % 54 / 18;
        int top = j;
        if (tile.TileFrameY % 36 != 0)
            top--;

        int chest = Chest.FindChest(left, top);
        if (chest < 0)
            return Language.GetTextValue("LegacyDresserType.0");
        if (Main.chest[chest].name == "")
            return name;
        return name + ": " + Main.chest[chest].name;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
    {
        width = 3;
        height = 1;
        extraY = 0;
    }

    private static void MouseOverBoth(int i, int j, Player player)
    {
        Tile tile = Main.tile[i, j];
        int top = j;
        int left = i - tile.TileFrameX % 54 / 18;
        if (tile.TileFrameY % 36 != 0)
            top--;

        int chestIndex = Chest.FindChest(left, top);
        player.cursorItemIconID = -1;
        if (chestIndex < 0)
        {
            player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
        }
        else
        {
            if (Main.chest[chestIndex].name != "")
                player.cursorItemIconText = Main.chest[chestIndex].name;
            else
            {
                player.cursorItemIconID = ModContent.ItemType<Items.Placeable.AsphaltDresser>();
            }
        }

        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        MouseOverBoth(i, j, player);
        if (Main.tile[i, j].TileFrameY > 0)
        {
            player.cursorItemIconID = ItemID.FamiliarShirt;
            player.cursorItemIconText = "";
        }
    }

    public override void MouseOverFar(int i, int j)
    {
        Player player = Main.LocalPlayer;
        MouseOverBoth(i, j, player);
        if (player.cursorItemIconText == "")
        {
            player.cursorItemIconEnabled = false;
            player.cursorItemIconID = ItemID.None;
        }
    }

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];

        int left = i - tile.TileFrameX / 18;
        int top = j - tile.TileFrameY / 18;

        player.SetTalkNPC(-1);
        Main.npcChatCornerItem = 0;
        Main.npcChatText = "";

        if (tile.TileFrameY == 0)
        {
            Main.CancelClothesWindow(true);
            Main.mouseRightRelease = true;
            player.CloseSign();

            if (Main.editChest)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
                Main.editChest = false;
            }

            if (player.editedChestName)
            {
                NetMessage.SendData(MessageID.SyncPlayerChest, text: NetworkText.FromLiteral(Main.chest[player.chest].name), number: player.chest, number2: 1f);
                player.editedChestName = false;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (left == player.chestX && top == player.chestY && player.chest != -1)
                {
                    player.chest = -1;
                    Recipe.FindRecipes();
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
                else
                {
                    NetMessage.SendData(MessageID.RequestChestOpen, number: left, number2: top);
                    Main.stackSplit = 600;
                }
            }
            else
            {
                player.piggyBankProjTracker.Clear();
                player.voidLensChest.Clear();
                int chestIndex = Chest.FindChest(left, top);
                if (chestIndex != -1)
                {
                    Main.stackSplit = 600;
                    if (chestIndex == player.chest)
                    {
                        player.chest = -1;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                    else if (player.chest == -1)
                    {
                        player.OpenChest(left, top, chestIndex);
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                    else
                    {
                        player.OpenChest(left, top, chestIndex);
                        SoundEngine.PlaySound(SoundID.MenuTick);
                    }
                    Recipe.FindRecipes();
                }
            }
        }
        else
        {
            Main.playerInventory = false;
            player.chest = -1;
            Recipe.FindRecipes();
            Main.interactedDresserTopLeftX = left;
            Main.interactedDresserTopLeftY = top;
            Main.OpenClothesWindow();
        }

        return true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Chest.DestroyChest(i, j);
    }
}
