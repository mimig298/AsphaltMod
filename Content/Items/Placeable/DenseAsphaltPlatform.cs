using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class DenseAsphaltPlatform : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DenseAsphaltPlatform>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(2)
                .AddIngredient<DenseAsphalt>()
                .Register();

            Recipe undoPlatform = Recipe.Create(ModContent.ItemType<DenseAsphalt>());
            undoPlatform.AddIngredient<DenseAsphaltPlatform>();
            undoPlatform.Register();
        }
    }
}
