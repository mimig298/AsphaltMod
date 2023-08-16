using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Ammo
{
    internal class AsphaltArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults() 
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1.2f;
            Item.value = 12;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltArrow>();
            Item.shootSpeed = 5;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(150)
                .AddIngredient(ItemID.WoodenArrow, 150)
                .AddIngredient(ItemID.AsphaltBlock, 10)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
