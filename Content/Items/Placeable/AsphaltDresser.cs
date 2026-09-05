using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltDresser : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GlassDresser);
        Item.createTile = ModContent.TileType<Tiles.AsphaltDresser>();
        Item.placeStyle = 0;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 16)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
