using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class DenseAsphalt : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DenseAsphalt>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 3)
                .AddIngredient(ItemID.Gel, 1)
                .AddTile<Tiles.AsphaltMachine>()
                .Register();
        }
    }
}
