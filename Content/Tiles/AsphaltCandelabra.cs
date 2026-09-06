using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltCandelabra : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

        DustType = -1;
        AdjTiles = [TileID.Candelabras];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Candelabras, 0));
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(32, 40, 45), Language.GetText("ItemName.Candelabra"));
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
        Tile tile = Main.tile[i, j];
        Wiring.Toggle2x2Light(i, j, tile, null, true);
    }
}
