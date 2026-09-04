using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltChandelier : ModTile
{
    private static Asset<Texture2D> flameTexture;

    public override void Load()
    {
        flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
    }

    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.MultiTileSway[Type] = true;
        TileID.Sets.IsAMechanism[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.Chandeliers];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Chandeliers, 0));
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(235, 166, 135), Language.GetText("MapObject.Chandelier"));
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].TileFrameX < 54)
        {
            r = 0.79f;
            g = 0.78f;
            b = 0.65f;
        }
    }

    public override void HitWire(int i, int j)
    {
        Wiring.ToggleChandelier(i, j, Main.tile[i, j], null, true);
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (TileObjectData.IsTopLeft(i, j))
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

    public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
    {
        tileFlameData.flameTexture = flameTexture.Value;
        tileFlameData.flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);
        tileFlameData.flameCount = 7;
        tileFlameData.flameColor = new Color(100, 100, 100, 0);
        tileFlameData.flameRangeXMin = -10;
        tileFlameData.flameRangeXMax = 11;
        tileFlameData.flameRangeYMin = -10;
        tileFlameData.flameRangeYMax = 1;
        tileFlameData.flameRangeMultX = 0.15f;
        tileFlameData.flameRangeMultY = 0.35f;
    }
}
