using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltSofa : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.CanBeSatOnForPlayers[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.Benches];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Benches, 0));
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
        AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Sofa"));
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
    {
        Tile tile = Framing.GetTileSafely(i, j);

        info.TargetDirection = info.RestingEntity.direction;

        info.DirectionOffset = 0;
        info.FinalOffset = new Vector2(-1, 0);

        if ((tile.TileFrameX == 0 && info.TargetDirection == -1) || (tile.TileFrameX == 36 && info.TargetDirection == 1))
            info.VisualOffset = new Vector2(-3, 1);
        else if ((tile.TileFrameX == 0 && info.TargetDirection == 1) || (tile.TileFrameX == 36 && info.TargetDirection == -1))
            info.VisualOffset = new Vector2(5, 1);
        else
            info.VisualOffset = new Vector2(1, 1);
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;

        if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
            return;

        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.AsphaltSofa>();
    }

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
        {
            player.GamepadEnableGrappleCooldown();
            player.sitting.SitDown(player, i, j);
            return true;
        }
        return false;
    }
}
