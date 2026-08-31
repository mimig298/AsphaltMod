using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltCandle : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GlassCandle);
        Item.value = Item.sellPrice(copper: 60);

        Item.createTile = ModContent.TileType<Tiles.AsphaltCandle>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 4)
            .AddIngredient(ItemID.Torch)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
