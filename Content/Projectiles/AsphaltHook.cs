using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles;

public class AsphaltHook : ModProjectile
{
    private static Asset<Texture2D> chainTexture;

    public override void Load()
    {
        chainTexture = ModContent.Request<Texture2D>(Texture + "_Chain");
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.SingleGrappleHook[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
        Projectile.extraUpdates = 1;
    }

    public override float GrappleRange()
    {
        return 440f;
    }

    public override void NumGrappleHooks(Player player, ref int numHooks)
    {
        numHooks = 1;
    }

    public override void GrappleRetreatSpeed(Player player, ref float speed)
    {
        speed = 22f;
    }

    public override void GrapplePullSpeed(Player player, ref float speed)
    {
        speed = 16f;
    }

    // exmaple mod
    public override bool PreDrawExtras()
    {
        Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
        Vector2 center = Projectile.Center;
        Vector2 directionToPlayer = playerCenter - Projectile.Center;
        float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
        float distanceToPlayer = directionToPlayer.Length();

        while (distanceToPlayer > 16f && !float.IsNaN(distanceToPlayer))
        {
            directionToPlayer /= distanceToPlayer; // get unit vector
            directionToPlayer *= chainTexture.Height(); // multiply by chain link length

            center += directionToPlayer; // update draw position
            directionToPlayer = playerCenter - center; // update distance
            distanceToPlayer = directionToPlayer.Length();

            Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

            Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                chainTexture.Value.Bounds, drawColor, chainRotation,
                chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
        }

        return false;
    }
}
