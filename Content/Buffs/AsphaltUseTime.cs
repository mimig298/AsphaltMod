using AsphaltMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Buffs
{
    public class AsphaltUseTime : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffPlayer>().AsphaltSpeedRegen = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.7f;
        }
    }
}
