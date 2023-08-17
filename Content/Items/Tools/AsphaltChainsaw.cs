using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Tools
{
    public class AsphaltChainsaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsChainsaw[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 14;
            Item.useTime = 3;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.1f;
            Item.value = Item.sellPrice(gold: 2, silver: 7);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item23;
            Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltChainsaw>();
            Item.shootSpeed = 40f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.tileBoost = -2;
            Item.axe = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 37)
                .AddIngredient(ItemID.AdamantiteBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 37)
                .AddIngredient(ItemID.TitaniumBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
