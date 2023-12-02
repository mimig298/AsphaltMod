using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles
{
    public class AsphaltBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 29;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 400;
        }

        public override void AI()
        {
            for (int k = 0; k < 2; k++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.3f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(in SoundID.Item8, Projectile.position);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(in SoundID.Item8, Projectile.position);
            for (int k = 0; k < 15; k++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Asphalt, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, default, 1.2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1.25f;
                Main.dust[dust].velocity *= 0.5f;
            }
        }
    }
}
