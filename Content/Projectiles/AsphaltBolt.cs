using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles
{
    public class AsphaltBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            bool balanced = ((AsphaltMod)Mod).BalanceChanges;

            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = balanced ? 1 : 3;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = balanced ? 1 : 0;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            for (int k = 0; k < 7; k++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.25f;
                Main.dust[dust].velocity *= 0.5f;
            }
        }
    }
}
