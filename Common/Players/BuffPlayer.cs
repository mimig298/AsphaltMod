using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Common.Players
{
    public class BuffPlayer : ModPlayer
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
                Player.lifeRegen = 0;
                Player.lifeRegen -= 7;
            }
        }
    }
}
