using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltBookcase : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Bookcase);

        Item.width = 24;
        Item.height = 30;
        Item.createTile = ModContent.TileType<Tiles.AsphaltBookcase>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
