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

public class AsphaltCandle : ModTile
{
    private Asset<Texture2D> flameTexture;

    public override void Load()
    {
        flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
    }

    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.DisableSmartInteract[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.Candles];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Candles, 0));
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
        AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Candle"));
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX < 36)
        {
            r = 0.79f;
            g = 0.78f;
            b = 0.65f;
        }
    }

    public override void HitWire(int i, int j)
    {
        Wiring.ToggleCandle(i, j, Main.tile[i, j], null);
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];

        if (!TileDrawing.IsVisible(tile))
            return;

        Vector2 screenPos = Main.Camera.UnscaledPosition;
        Vector2 screenOffset = Main.drawToScreen ? Vector2.Zero : new(Main.offScreenRange);
        int width = 16;
        int height = 20;
        int offsetY = -4;
        short frameX = tile.TileFrameX;
        short frameY = tile.TileFrameY;
        SpriteEffects spriteEffects = SpriteEffects.None;
        TileLoader.SetSpriteEffects(i, j, Type, ref spriteEffects);
        TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);

        ulong seed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

        for (int n = 0; n < 7; n++)
        {
            float shakeX = Utils.RandomInt(ref seed, -10, 11) * 0.15f;
            float shakeY = Utils.RandomInt(ref seed, -10, 1) * 0.35f;
            Vector2 drawPos = new(i * 16 - (int)screenPos.X - (width - 16f) / 2f + shakeX, j * 16 - (int)screenPos.Y + offsetY + shakeY);
            drawPos += screenOffset;
            Rectangle source = new(frameX, frameY, width, height);
            spriteBatch.Draw(flameTexture.Value, drawPos, source, new Color(100, 100, 100, 0), 0f, Vector2.Zero, 1f, spriteEffects, 0f);
        }
    }
}
