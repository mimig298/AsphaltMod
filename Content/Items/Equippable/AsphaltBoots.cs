using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Equippable
{
    [AutoloadEquip(EquipType.Shoes)]
    internal class AsphaltBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 1, silver: 70);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HermesBoots)
                .AddIngredient(ItemID.AsphaltBlock, 35)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.FlurryBoots)
                .AddIngredient(ItemID.AsphaltBlock, 35)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.SailfishBoots)
                .AddIngredient(ItemID.AsphaltBlock, 35)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.SandBoots)
                .AddIngredient(ItemID.AsphaltBlock, 35)
                .AddTile(TileID.MythrilAnvil)
                .AddTile(TileID.Blendomatic)
                .Register();
        }
    }
}
