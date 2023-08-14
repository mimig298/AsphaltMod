using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Walls
{
    public class AsphaltWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            DustType = DustID.Asphalt;
            AddMapEntry(new Color(18, 19, 20));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 2 : 5;
        }
    }
}