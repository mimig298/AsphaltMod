using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles
{
    public class DenseAsphaltPlatform : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileID.Sets.Platforms[Type] = true;
            TileObjectData.newTile.CoordinateHeights = [16];
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleMultiplier = 27;
            TileObjectData.newTile.StyleWrapLimit = 27;

            TileObjectData.newTile.UsesCustomCanPlace = false;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
            DustType = DustID.Asphalt;
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = [TileID.Platforms];
            AddMapEntry(new Color(32, 33, 34));

            // Set other values here
        }

        public override void FloorVisuals(Player player)
        {
            player.GetModPlayer<Common.Players.AsphaltPlayer>().ultrarun = true;
        }
    }
}