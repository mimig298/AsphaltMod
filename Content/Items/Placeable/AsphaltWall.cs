using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableWall(ModContent.WallType<Walls.AsphaltWall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient(ItemID.AsphaltBlock)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe undoWall = Recipe.Create(ItemID.AsphaltBlock);
            undoWall.AddIngredient<AsphaltWall>(4);
            undoWall.AddTile(TileID.WorkBenches);
            undoWall.Register();
        }
    }
}
