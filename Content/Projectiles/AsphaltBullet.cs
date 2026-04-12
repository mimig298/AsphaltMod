using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles
{
    public class AsphaltBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;
            Projectile.width = 2;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.noDropItem = true;
            Projectile.penetrate = balanced ? 2 : 3;
            Projectile.light = 0.5f;
            Projectile.scale = 1.2f;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt);
            }
        }
    }
}
