using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltTable : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AsphaltTable>());
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 8)
                .AddTile(ModContent.TileType<Tiles.AsphaltMachine>())
                .Register();
        }
    }
}
