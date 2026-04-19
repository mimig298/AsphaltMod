using AsphaltMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Items.Placeable;

public class AsphaltSlimeBanner : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SlimeBanner);
        Item.createTile = ModContent.TileType<AsphaltSlimeBannerTile>();
        Item.placeStyle = 0;
    }
}
