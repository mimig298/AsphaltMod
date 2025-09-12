using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons
{
    public class AsphaltBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true;
        }

        public override void SetDefaults() 
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;

            Item.DefaultToBow(balanced ? 10 : 8, 12, true);

            Item.width = 18;
            Item.height = 36;
            Item.value = 13700;
            Item.rare = ItemRarityID.LightRed;

            Item.UseSound = SoundID.Item5;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = balanced ? 19 : 37;
            Item.knockBack = balanced ? 1.2f : 1.7f;

            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 45)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
