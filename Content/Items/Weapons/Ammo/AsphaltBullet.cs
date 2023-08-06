using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons.Ammo
{
    public class AsphaltBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.knockBack = 1f;
            Item.value = 50;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ProjectileID.Bullet; // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 30f; // The speed of the projectile.
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.AsphaltBlock, 1)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}