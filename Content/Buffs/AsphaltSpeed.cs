using Terraria;
using Terraria.ModLoader;
using AsphaltMod.Common.Players;
using Terraria.Localization;

namespace AsphaltMod.Content.Buffs
{
    public class AsphaltSpeed : ModBuff
    {
        public override LocalizedText Description => this.GetLocalization(((AsphaltMod)Mod).BalanceChanges ? "Description" : "DescriptionOld");

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffPlayer>().asphaltMoveSpeed = true;
        }
    }
}
