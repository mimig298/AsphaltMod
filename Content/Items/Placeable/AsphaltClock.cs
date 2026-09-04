using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltClock : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GlassClock);

        Item.createTile = ModContent.TileType<Tiles.AsphaltClock>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ItemID.AsphaltBlock, 1)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
