using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltSink : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MetalSink);
        Item.createTile = ModContent.TileType<Tiles.AsphaltSink>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 6)
            .AddIngredient(ItemID.WaterBucket)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
