using AsphaltMod.Common.Players;
using AsphaltMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Mounts;

public class AsphaltCar : ModMount
{
    private static Vector2 HitboxSize = new(172, 50);

    public override void SetStaticDefaults()
    {
        MountData.jumpHeight = 0;
        MountData.runSpeed = 21f; // 170 km/h = 155 ft/s = 78 tiles/s = 21 px/t
        MountData.acceleration = 0.035f; // Let's say it takes 10 seconds to reach max speed, it would be 2.1 px/t/s = 0.036 px/t^2
        MountData.blockExtraJumps = true;
        MountData.heightBoost = 18;
        MountData.flightTimeMax = 0;

        MountData.spawnDust = DustID.Asphalt;
        MountData.buff = ModContent.BuffType<AsphaltCarBuff>();

        MountData.totalFrames = 4;
        MountData.playerYOffsets = [.. Enumerable.Repeat(MountData.heightBoost, MountData.totalFrames)];
        MountData.xOffset = 6;
        MountData.bodyFrame = 3;
        MountData.playerHeadOffset = 20;

        MountData.standingFrameCount = 1;
        MountData.standingFrameDelay = 60;
        MountData.standingFrameStart = 0;

        MountData.runningFrameCount = 4;
        MountData.runningFrameDelay = 10;
        MountData.runningFrameStart = 0;

        MountData.idleFrameCount = 1;
        MountData.idleFrameDelay = 60;
        MountData.idleFrameStart = 0;

        MountData.inAirFrameCount = 1;
        MountData.inAirFrameDelay = 60;
        MountData.inAirFrameStart = 0;

        if (!Main.dedServ)
        {
            MountData.textureWidth = MountData.frontTexture.Width();
            MountData.textureHeight = MountData.frontTexture.Height();
        }
    }

    public override void SetMount(Player player, ref bool skipDust)
    {
        skipDust = true;
    }

    public override void Dismount(Player player, ref bool skipDust)
    {
        skipDust = true;
    }

    public override void UpdateEffects(Player player)
    {
        CarPlayer carPlayer = player.GetModPlayer<CarPlayer>();

        // Headlights

        Point tileCoords = new((int)player.MountedCenter.X / 16, (int)player.MountedCenter.Y / 16);
        float brightness = Lighting.Brightness(tileCoords.X, tileCoords.Y);
        if (brightness <= 0.5f && !carPlayer.lightsOn)
        {
            if (++carPlayer.lightCooldown >= 30)
            {
                carPlayer.lightsOn = true;
                carPlayer.lightCooldown = 0;
            }
        }
        else if (brightness >= 0.9f && carPlayer.lightsOn)
        {
            if (++carPlayer.lightCooldown >= 30)
            {
                carPlayer.lightsOn = false;
                carPlayer.lightCooldown = 0;
            }
        }
        else
        {
            carPlayer.lightCooldown = 0;
        }
        //Main.NewText($"{carPlayer.lightCooldown} {brightness}");
        if (carPlayer.lightsOn)
        {
            Vector2 headlightPos = player.Center + new Vector2(MountData.xOffset, 0) + new Vector2(80 * carPlayer.velDirection, 7);
            Vector2 speedAdjustedPos = headlightPos + new Vector2(player.velocity.X * 5); // the light falls behind when you go too fast so this counters that
            Lighting.AddLight(speedAdjustedPos, new Vector3(0.8f));
        }

        // Deal damage (adapated from minecart damage in Player.Update)

        float speed = player.velocity.Length();
        if (speed > 4 && player.whoAmI == Main.myPlayer)
        {
            Rectangle hitbox = GetHitbox(player);
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.dontTakeDamage || npc.friendly || npc.immune[player.whoAmI] != 0 || !player.CanNPCBeHitByPlayerOrPlayerProjectile(npc) || !hitbox.Intersects(npc.Hitbox))
                    return;

                float currentSpeed = speed / MountData.runSpeed;
                int damage = Main.DamageVar(25f + 70f * currentSpeed, player.luck);
                if (Main.hardMode)
                    damage = (int)(damage * 1.5f);
                if (Main.expertMode)
                    damage = (int)(damage * 1.5f);
                float knockback = 10f + 40f * currentSpeed;
                if (npc.knockBackResist < 1 && npc.knockBackResist > 0)
                    knockback /= npc.knockBackResist;
                int direction = player.velocity.X > 0 ? 1 : -1;

                player.ApplyDamageToNPC(npc, damage, knockback, direction);
                npc.immune[player.whoAmI] = 30;
            }
        }
    }

    private Rectangle GetHitbox(Player player)
    {
        int direction = player.direction;
        if (player.TryGetModPlayer(out CarPlayer carPlayer))
            direction = carPlayer.velDirection;
        Vector2 carCenter = player.Center + new Vector2(MountData.xOffset * direction, 0);
        Vector2 bottom = carCenter + new Vector2(0, MountData.textureHeight / 8 - 2);
        Rectangle hitbox = new((int)bottom.X - (int)HitboxSize.X / 2, (int)bottom.Y - (int)HitboxSize.Y, (int)HitboxSize.X, (int)HitboxSize.Y);
        return hitbox;
    }

    public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
    {
        drawPlayer.TryGetModPlayer(out CarPlayer carPlayer);

        // Fix the incorrect offset from manually flipping the player sprite
        if (carPlayer != null && drawPlayer.direction != carPlayer.velDirection)
        {
            drawPosition.X -= MountData.xOffset * drawPlayer.direction * 2;
        }

        // Only do special drawing for the front texture
        if (drawType != 2)
            return true;

        // Draw the normal texture normally
        DrawData data = new(texture, drawPosition, frame, drawColor, rotation, drawOrigin, drawScale, spriteEffects)
        { shader = Mount.currentShader };
        playerDrawData.Add(data);

        // Draw glow
        bool drawFrontGlow = carPlayer != null && carPlayer.lightsOn;
        bool drawBackGlow = (drawPlayer.controlLeft && drawPlayer.velocity.X > 0) || (drawPlayer.controlRight && drawPlayer.velocity.X < 0);
        if (drawFrontGlow || drawBackGlow)
        {
            Rectangle glowFrame = frame;
            Vector2 glowOrigin = drawOrigin;
            if (drawFrontGlow && !drawBackGlow)
            {
                glowFrame.X = (int)frame.Center().X;
                glowFrame.Width = frame.Width / 2;
                glowOrigin.X = !spriteEffects.HasFlag(SpriteEffects.FlipHorizontally) ? 0 : glowFrame.Width;
            }
            else if (drawBackGlow && !drawFrontGlow)
            {
                glowFrame.Width = frame.Width / 2;
                glowOrigin.X = !spriteEffects.HasFlag(SpriteEffects.FlipHorizontally) ? glowFrame.Width : 0;
            }
            data = new(glowTexture, drawPosition, glowFrame, Color.White, rotation, glowOrigin, drawScale, spriteEffects)
            { shader = Mount.currentShader };
            playerDrawData.Add(data);
        }

        // Hitbox debug
        /*
        Rectangle hitbox = GetHitbox(drawPlayer);
        playerDrawData.Add(new DrawData(Terraria.GameContent.TextureAssets.MagicPixel.Value, new Rectangle(hitbox.X - (int)Main.screenPosition.X, hitbox.Y - (int)Main.screenPosition.Y, hitbox.Width, hitbox.Height), Color.Red * 0.5f));
        */

        return false;
    }
}
