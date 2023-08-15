using AsphaltMod.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable.Furniture
{
    public class AsphaltDoor : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<AsphaltDoorClosed>());
            Item.width = 18;
            Item.height = 32;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 6)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
