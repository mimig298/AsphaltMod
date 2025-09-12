using AsphaltMod.Common.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Buffs
{
    public class AsphaltUseTime : ModBuff
    {
        private static readonly float balancedMultiplier = 0.4f;
        private static readonly float originalMultiplier = 0.7f;

        public override LocalizedText Description => this.GetLocalization(((AsphaltMod)Mod).BalanceChanges ? "Description" : "DescriptionOld").WithFormatArgs((((AsphaltMod)Mod).BalanceChanges ? balancedMultiplier : originalMultiplier) * 100);

        public override void Update(Player player, ref int buffIndex)
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;
            player.GetModPlayer<BuffPlayer>().asphaltSwingSpeed = true;
            player.GetAttackSpeed(DamageClass.Generic) += balanced ? balancedMultiplier : originalMultiplier;
            if (balanced)
                player.GetKnockback(DamageClass.Generic) -= 0.8f;
        }
    }
}
