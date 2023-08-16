using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Tools
{
    public class AsphaltAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 32;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(gold: 2, silver: 75);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.axe = 109;
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
