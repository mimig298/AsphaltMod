using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltBricks : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.AsphaltBrick>());
            Item.width = 12;
            Item.height = 12;
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
