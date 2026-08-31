using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltChair : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AsphaltChair>());
            Item.width = 16;
            Item.height = 32;
            Item.value = Item.sellPrice(copper: 30);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 4)
                .AddTile(ModContent.TileType<Tiles.AsphaltMachine>())
                .Register();
        }
    }
}
