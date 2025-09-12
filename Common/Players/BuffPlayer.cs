using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Common.Players
{
    public class BuffPlayer : ModPlayer
    {
        public bool asphaltMoveSpeed;
        public bool asphaltSwingSpeed;

        public override void ResetEffects()
        {
            asphaltMoveSpeed = false;
            asphaltSwingSpeed = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (((AsphaltMod)Mod).BalanceChanges)
            {
                if (asphaltMoveSpeed)
                {
                    if (Player.lifeRegen > 0)
                        Player.lifeRegen = 0;
                    Player.lifeRegenTime = 0;
                }
            }
            else if (asphaltMoveSpeed || asphaltSwingSpeed)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegen -= 7;
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (asphaltMoveSpeed && !((AsphaltMod)Mod).BalanceChanges)
            {
                Player.maxRunSpeed *= 3.5f;
                Player.runSlowdown += 2f;
            }
        }

        public override void PostUpdate()
        {
            if (asphaltMoveSpeed && ((AsphaltMod)Mod).BalanceChanges)
                Player.powerrun = true;
        }
    }
}
