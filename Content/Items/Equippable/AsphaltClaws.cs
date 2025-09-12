using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Equippable
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class AsphaltClaws : ModItem
    {
        public override LocalizedText Tooltip => this.GetLocalization(((AsphaltMod)Mod).BalanceChanges ? "Tooltip" : "TooltipOld");

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 30;
            Item.accessory = true; // Makes this item an accessory.
            Item.rare = ItemRarityID.LightRed;
            Item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            if (!balanced)
                player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += balanced ? 0.6f : 1f;
        }

        public override void AddRecipes()
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;
            if (balanced)
            {
                CreateRecipe()
                    .AddIngredient(ItemID.AsphaltBlock, 20)
                    .AddIngredient(ItemID.PowerGlove)
                    .AddTile(TileID.MythrilAnvil)
                    .AddTile(TileID.Blendomatic)
                    .Register();
            }
            else
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
}