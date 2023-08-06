﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Consumable
{
    internal class AsphaltPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
            {
                new Color(32, 33, 34),
                new Color(18, 21, 25),
                new Color(67, 66, 56)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 1, copper: 50);
            Item.buffType = ModContent.BuffType<Buffs.AsphaltSpeed>();
            Item.buffTime = 18000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SwiftnessPotion)
                .AddIngredient(ItemID.AsphaltBlock)
                .AddIngredient(ItemID.Blinkroot)
                .AddIngredient(ItemID.Daybloom)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
