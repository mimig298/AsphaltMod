using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles
{
    public class AsphaltBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.noDropItem = true;
            Projectile.maxPenetrate = 3;
            Projectile.penetrate = 3;
            Projectile.light = 0.5f;
            Projectile.scale = 1.2f;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;
        }
    }
}
