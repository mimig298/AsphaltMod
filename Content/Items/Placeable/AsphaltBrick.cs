using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltBrick : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AsphaltBrick>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 2)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
