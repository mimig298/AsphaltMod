using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Equippable;

public class AsphaltHook : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 28;

        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noUseGraphic = true;

        Item.UseSound = SoundID.Item1;

        Item.value = Item.sellPrice(gold: 1);
        Item.rare = ItemRarityID.LightRed;

        Item.shoot = ModContent.ProjectileType<Projectiles.AsphaltHook>();
        Item.shootSpeed = 14f;

        if (ModLoader.TryGetMod("HookStatsAndWingStats", out Mod hookStatsMod) && ContentSamples.ProjectilesByType.TryGetValue(Item.shoot, out Projectile hookProj))
        {
            float hookReach = hookProj.ModProjectile?.GrappleRange() ?? 440f;
            float hookSpeed = Item.shootSpeed * (hookProj.extraUpdates + 1);
            hookStatsMod.Call("SetHookStats", Type, hookReach, hookSpeed, 1, (byte)0);
        }
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GrapplingHook)
            .AddIngredient(ItemID.AsphaltBlock, 20)
            .AddTile(TileID.MythrilAnvil)
            .AddTile(TileID.Blendomatic)
            .Register();
    }
}
