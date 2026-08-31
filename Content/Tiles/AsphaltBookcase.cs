using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltBookcase : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

        DustType = -1;
        AdjTiles = [TileID.Bookcases];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Bookcases, 0));
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(44, 41, 50), Language.GetText("ItemName.Bookcase"));
    }
}
