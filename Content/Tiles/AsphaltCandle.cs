using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltCandle : ModTile
{
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
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.AsphaltCandle>(), 1);
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX < 18)
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
}
