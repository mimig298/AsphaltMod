using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltCandelabra : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Candelabra);

        Item.createTile = ModContent.TileType<Tiles.AsphaltCandelabra>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 5)
            .AddIngredient(ItemID.Torch, 3)
            .AddTile<Tiles.AsphaltMachine>()
            .Register();
    }
}
