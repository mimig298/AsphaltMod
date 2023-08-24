using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable.Furniture
{
    public class AsphaltWorkbench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.AsphaltWorkbench>());
            Item.width = 32;
            Item.height = 18;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 10)
                .AddTile(ModContent.TileType<Tiles.AsphaltMachine>())
                .Register();
        }
    }
}
