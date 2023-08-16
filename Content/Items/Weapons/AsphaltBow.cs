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
        }

        public override void SetDefaults() 
        {
            Item.DefaultToBow(8, 12, true);

            Item.width = 18;
            Item.height = 36;
            Item.value = 13700;
            Item.rare = ItemRarityID.LightRed;

            Item.UseSound = SoundID.Item5;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 37;
            Item.knockBack = 1.7f;

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
