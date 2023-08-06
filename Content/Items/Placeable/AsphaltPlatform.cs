using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable
{
    public class AsphaltPlatform : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 14;
            Item.maxStack = 999;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.createTile = ModContent.TileType<Tiles.AsphaltPlatform>();
            Item.rare = ItemRarityID.White;
            // Set other Item.X values here
        }

        public override void AddRecipes()
        {
            Recipe platformRecipe = Recipe.Create(ModContent.ItemType<AsphaltPlatform>(), 2);
            platformRecipe.AddIngredient(ItemID.AsphaltBlock, 1);
            platformRecipe.Register();

            Recipe undoPlatform = Recipe.Create(ItemID.AsphaltBlock, 1);
            undoPlatform.AddIngredient(ModContent.ItemType<AsphaltPlatform>(), 2);
            undoPlatform.Register();
            // Recipes here. See Basic Recipe Guide
        }
    }
}