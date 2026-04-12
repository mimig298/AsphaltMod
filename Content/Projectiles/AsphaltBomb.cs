using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles;

public class AsphaltBomb : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.Explosive[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Bomb);
        Projectile.timeLeft = 90;
    }

    public override void PrepareBombToBlow()
    {
        Projectile.Resize(128, 128);
        Projectile.damage = 100;
        Projectile.knockBack = 7f;
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        // Vanilla behaviour for explosives in Projectile.Damage()
        if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail && Main.expertMode)
        {
            modifiers.TargetDamageMultiplier /= 5;
        }
    }

    public override void OnKill(int timeLeft)
    {
        // Adapted from Projectile.Kill() for type 28

        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        Projectile.Resize(22, 22);

        for (int i = 0; i < 20; i++)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
            dust.velocity *= 1.4f;
        }

        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.5f);
            dust.noGravity = true;
            dust.velocity *= 5f;
            dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
            dust.velocity *= 3f;
        }

        for (int i = 0; i < 4; i++)
        {
            Gore gore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, Main.rand.Next(61, 64));
            gore.velocity *= 0.4f;
            gore.velocity.X += i % 2 == 0 ? 1f : -1f;
            gore.velocity.Y += i < 2 ? 1f : -1f;
        }

        if (Projectile.owner == Main.myPlayer)
        {
            int radius = 4;

            int minI = (int)(Projectile.position.X / 16f - radius);
            int maxI = (int)(Projectile.position.X / 16f + radius);
            int minJ = (int)(Projectile.position.Y / 16f - radius);
            int maxJ = (int)(Projectile.position.Y / 16f + radius);

            if (minI < 0)
                minI = 0;
            if (maxI > Main.maxTilesX)
                maxI = Main.maxTilesX;
            if (minJ < 0)
                minJ = 0;
            if (maxJ > Main.maxTilesY)
                maxJ = Main.maxTilesY;

            Projectile.ExplodeTiles(Projectile.position, radius, minI, maxI, minJ, maxJ, Projectile.ShouldWallExplode(Projectile.position, radius, minI, maxI, minJ, maxJ));
        }
    }
}
