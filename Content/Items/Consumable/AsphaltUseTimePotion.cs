using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Consumable
{
    public class AsphaltUseTimePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;

            ItemID.Sets.DrinkParticleColors[Type] =
            [
                new Color(32, 33, 34),
                new Color(18, 21, 25),
                new Color(67, 66, 56)
            ];
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 1, copper: 50);
            Item.buffType = ModContent.BuffType<Buffs.AsphaltUseTime>();
            Item.buffTime = 18000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.AsphaltBlock)
                .AddIngredient(ItemID.Waterleaf)
                .AddIngredient(ItemID.Fireblossom)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
