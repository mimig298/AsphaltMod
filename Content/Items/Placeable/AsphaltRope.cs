using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltRope : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
        ItemID.Sets.SortingPriorityRopes[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 12;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 8;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.autoReuse = true;

        Item.value = 10;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;

        Item.tileBoost += 3;
        Item.createTile = ModContent.TileType<Tiles.AsphaltRope>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Rope, 40)
            .AddIngredient(ItemID.AsphaltBlock)
            .AddTile(TileID.Blendomatic)
            .Register();
    }
}

public class AsphaltRopeCoil : ModItem
{
    public override LocalizedText Tooltip => Language.GetText("ItemTooltip.RopeCoil");

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 20;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.value = 100;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;

        Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltRopeCoil>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<AsphaltRope>()
            .Register();
    }
}
