using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltPiano : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Piano);
        Item.createTile = ModContent.TileType<Tiles.AsphaltPiano>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 15)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ItemID.Book)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
