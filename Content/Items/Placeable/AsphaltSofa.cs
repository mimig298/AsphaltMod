using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltSofa : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GlassSofa);
        Item.createTile = ModContent.TileType<Tiles.AsphaltSofa>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Blendomatic)
            .Register();
    }
}
