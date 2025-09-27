using AsphaltMod.Content.Mounts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Equippable;

public class AsphaltCarKeys : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = Item.sellPrice(gold: 4, silver: 50);
        Item.UseSound = SoundID.Item22;
        Item.noMelee = true;
        Item.mountType = ModContent.MountType<AsphaltCar>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.AsphaltBlock, 100)
            .AddIngredient(ItemID.Wire, 70)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddRecipeGroup(RecipeGroupID.IronBar, 25)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
