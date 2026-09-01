using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltChandelier : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CopperChandelier);
        Item.createTile = ModContent.TileType<Tiles.AsphaltChandelier>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 4)
            .AddIngredient(ItemID.Torch, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
