using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltWorkbench : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AsphaltWorkbench>());
            Item.width = 32;
            Item.height = 18;
            Item.value = Item.sellPrice(copper: 30);
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
