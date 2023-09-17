using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Tiles
{
    public class DenseAsphalt : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMergeDirt[Type] = true;

            DustType = DustID.Asphalt;
            AddMapEntry(new Color(32, 33, 34));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 2 : 5;
        }

        public override void FloorVisuals(Player player)
        {
            player.GetModPlayer<Common.Players.AsphaltPlayer>().ultrarun = true;
        }
    }
}
