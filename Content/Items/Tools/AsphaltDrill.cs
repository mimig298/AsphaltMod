using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Tools
{
    public class AsphaltDrill : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsDrill[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 18;
            Item.useTime = 3;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.1f;
            Item.value = Item.sellPrice(gold: 2, silver: 25);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltDrill>();
            Item.shootSpeed = 32f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.tileBoost = -2;
            Item.pick = 190;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 50)
                .AddIngredient(ItemID.AdamantiteBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 50)
                .AddIngredient(ItemID.TitaniumBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
