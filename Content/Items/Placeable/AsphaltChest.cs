using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltChest : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Chest);
        Item.createTile = ModContent.TileType<Tiles.AsphaltChest>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 8)
            .AddRecipeGroup(RecipeGroupID.IronBar, 2)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
