using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Projectile.maxPenetrate = 2;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        // Additional hooks/methods here.
    }
}