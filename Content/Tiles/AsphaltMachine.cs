using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.DataStructures;

namespace AsphaltMod.Content.Tiles
{
    public class AsphaltMachine : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = [16, 18];
            TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
            TileObjectData.newTile.StyleLineSkip = 9;
            TileObjectData.addTile(Type);

            AnimationFrameHeight = 38;

            DustType = DustID.Asphalt;
            AdjTiles = [TileID.Blendomatic];
            LocalizedText mapEntryName = CreateMapEntryName();
            AddMapEntry(new Color(99, 99, 99), mapEntryName);
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                frame = ++frame % 4;
            }
        }
    }
}
