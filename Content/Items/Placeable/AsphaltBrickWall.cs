using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltBrickWall : ModItem
    {
        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableWall(ModContent.WallType<Walls.AsphaltBrickWall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient<AsphaltBrick>()
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe undoWall = Recipe.Create(ModContent.ItemType<AsphaltBrick>());
            undoWall.AddIngredient<AsphaltBrickWall>(4);
            undoWall.AddTile(TileID.WorkBenches);
            undoWall.Register();
        }
    }
}
