using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AsphaltMod.Content.Items.Placeable.Furniture
{
    public class AsphaltLamp : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.AsphaltLamp>());
            Item.width = 10;
            Item.height = 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 3)
                .AddIngredient(ItemID.Torch)
                .AddTile(ModContent.TileType<Tiles.AsphaltMachine>())
                .Register();
        }
    }
}
