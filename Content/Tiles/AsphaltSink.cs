using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltSink : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;
        TileID.Sets.CountsAsWaterSource[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.Sinks];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Sinks, 0));
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(105, 107, 125), Language.GetText("MapObject.Sink"));
    }
}
