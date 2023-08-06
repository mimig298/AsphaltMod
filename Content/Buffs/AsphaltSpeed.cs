using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Buffs
{
    public class AsphaltSpeed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asphalt Speed Boost");
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = "Asphalt movement enabled";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffCounterAsphaltSpeed>().AsphaltSpeedRegen = true;
            player.maxRunSpeed *= 3.5f;
            player.runSlowdown += 2f;
        }
    }

    public class BuffCounterAsphaltSpeed : ModPlayer
    {
        public bool AsphaltSpeedRegen;

        public override void ResetEffects()
        {
            AsphaltSpeedRegen = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (AsphaltSpeedRegen)
            {
                Player.lifeRegen -= 5;
            }
        }
    }
}