using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltLantern : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GlassLantern);
        Item.createTile = ModContent.TileType<Tiles.AsphaltLantern>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 6)
            .AddIngredient(ItemID.Torch)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
