using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons;

public class AsphaltBomb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Bomb);

        Item.useAnimation = 15;
        Item.useTime = 15;

        Item.value = Item.buyPrice(silver: 5);
        Item.rare = ItemRarityID.LightRed;

        Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltBomb>();
        Item.shootSpeed = 9f;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Bomb)
            .AddIngredient(ItemID.AsphaltBlock)
            .AddTile(TileID.Blendomatic)
            .Register();
    }
}
