using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Ammo
{
    public class AsphaltBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1f;
            Item.value = 50;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltBullet>();
            Item.shootSpeed = 10;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)
                .AddIngredient(ItemID.AsphaltBlock, 1)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}