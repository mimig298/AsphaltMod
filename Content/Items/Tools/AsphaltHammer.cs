using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Tools
{
    public class AsphaltHammer : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 1, silver: 75);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.hammer = 80;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 60)
                .AddIngredient(ItemID.AdamantiteBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 60)
                .AddIngredient(ItemID.TitaniumBar, 15)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
