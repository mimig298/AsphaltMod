using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons
{
    public class AsphaltGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true;
        }

        public override void SetDefaults()
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;

            Item.width = 44;
            Item.height = 20;
            Item.scale = 1;
            Item.value = 14200;
            Item.rare = ItemRarityID.LightRed;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item11;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = balanced ? 15 : 27;
            Item.knockBack = balanced ? 1.1f : 1.5f;
            Item.noMelee = true;

            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 64)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}
