using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Equippable
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class AsphaltClaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asphalt Glove");
            Tooltip.SetDefault("Increases melee knockback\r\n12% increased melee damage\r\nDoubled melee speed\r\nEnables auto swing for melee weapons\r\nIncreases the size of melee weapons");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 30;
            Item.accessory = true; // Makes this item an accessory.
            Item.rare = ItemRarityID.Pink;
            Item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += 1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AsphaltBlock, 20)
                .AddIngredient(ItemID.MechanicalGlove)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}