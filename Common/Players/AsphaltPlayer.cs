using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Common.Players
{
    public class AsphaltPlayer : ModPlayer
    {
        public bool ultrarun = false;

        public override void ResetEffects()
        {
            ultrarun = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            if (ultrarun)
            {
                Player.maxRunSpeed *= 5f;
                Player.runAcceleration *= 1.5f;
                Player.runSlowdown *= 4f;
            }
        }
    }
}
