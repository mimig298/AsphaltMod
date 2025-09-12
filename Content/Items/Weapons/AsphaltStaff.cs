using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Weapons
{
    public class AsphaltStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
            ItemID.Sets.CanBePlacedOnWeaponRacks[Type] = true;
        }

        public override void SetDefaults()
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;

            Item.DefaultToStaff(ModContent.ProjectileType<Projectiles.AsphaltBolt>(), balanced ? 9 : 20, 5, balanced ? 10 : 7);
            Item.SetWeaponValues(balanced ? 18 : 35, balanced ? 1.3f : 2);

            Item.UseSound = SoundID.Item43 with { Volume = 0.7f, MaxInstances = 2, SoundLimitBehavior = SoundLimitBehavior.IgnoreNew };
            Item.rare = ItemRarityID.LightRed;
            Item.value = 12500;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 55)
                .AddTile(TileID.Blendomatic)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
