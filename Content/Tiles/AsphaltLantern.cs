using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltLantern : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.IsAMechanism[Type] = true;
        TileID.Sets.MultiTileSway[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.HangingLanterns];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.HangingLanterns, 15));
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(251, 235, 127), Language.GetText("MapObject.Lantern"));
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.AsphaltLantern>(), 1);
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].TileFrameX < 18)
        {
            r = 0.79f;
            g = 0.78f;
            b = 0.65f;
        }
    }

    public override void HitWire(int i, int j)
    {
        Wiring.ToggleHangingLantern(i, j, Main.tile[i, j], null, true);
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        // wind hates modded tiles
        offsetY += 2;
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        // return true;
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX % 18 == 0 && tile.TileFrameY % 36 == 0)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
        }
        return false;
    }

    public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
    {
        overrideWindCycle = 1f;
        windPushPowerY = 0f;
    }
}
