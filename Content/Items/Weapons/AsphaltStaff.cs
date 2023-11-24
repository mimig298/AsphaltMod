using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons
{
    public class AsphaltStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.AsphaltBolt>(), 15, 5, 7);
            Item.SetWeaponValues(35, 2);

            Item.rare = ItemRarityID.LightRed;
            Item.value = 12500;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 55)
                .AddTile(TileID.Blendomatic)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
