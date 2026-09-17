using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;

namespace AsphaltMod.Content.Tiles;

public class AsphaltToilet : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.CanBeSatOnForNPCs[Type] = true;
        TileID.Sets.CanBeSatOnForPlayers[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.Toilets];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Toilets, 0));
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
        AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Toilet"));
    }

    public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
    {
        width = 1;
        height = 2;
        extraY = 4;
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);
    }

    public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
    {
        Tile tile = Framing.GetTileSafely(i, j);

        if (tile.TileFrameY % 40 == 0)
            info.AnchorTilePosition.Y++;
        info.TargetDirection = tile.TileFrameX == 0 ? -1 : 1;
        info.ExtraInfo.IsAToilet = true;
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;

        if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
            return;

        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.AsphaltToilet>();
        player.cursorItemIconReversed = Main.tile[i, j].TileFrameX / 18 < 1;
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

    public override void HitWire(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int top = j - tile.TileFrameY % 40 / 18;
        Wiring.SkipWire(i, top);
        Wiring.SkipWire(i, top + 1);
        if (Wiring.CheckMech(i, top, 60))
        {
            Projectile.NewProjectile(Wiring.GetProjectileSource(i, top), i * 16 + 8, top * 16 + 12, 0f, 0f, ProjectileID.ToiletEffect, 0, 0f, Main.myPlayer);
        }
    }
}
