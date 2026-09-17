using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltToilet : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.ToiletGlass);
        Item.createTile = ModContent.TileType<Tiles.AsphaltToilet>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 6)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
