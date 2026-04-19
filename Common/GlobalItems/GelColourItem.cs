using AsphaltMod.Content.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Common.GlobalItems;

public class GelColourItem : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.Gel;
    }

    public override void OnSpawn(Item item, IEntitySource source)
    {
        if (source is EntitySource_Loot lootSource && lootSource.Entity is NPC npcParent && npcParent.type == ModContent.NPCType<AsphaltSlime>())
        {
            item.color = new Color(32, 33, 34, 100);
            NetMessage.SendData(MessageID.ItemTweaker, number: item.whoAmI, number2: 1f);
        }
    }
}
