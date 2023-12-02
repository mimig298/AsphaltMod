using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AsphaltMod.Content.Projectiles
{
    public class AsphaltArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.arrow = true;
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.Arrow; // or 1
            Projectile.friendly = true;
            Projectile.noDropItem = true;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int k = 0; k < 6; k++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt);
            }
        }
        // Additional hooks/methods here.
    }
}