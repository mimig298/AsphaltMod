using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltBed : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Bed);

        Item.width = 34;
        Item.height = 20;
        Item.createTile = ModContent.TileType<Tiles.AsphaltBed>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 15)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
