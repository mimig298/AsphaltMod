using AsphaltMod.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Projectiles;

public class AsphaltRopeCoil : ModProjectile
{
    private static Asset<Texture2D> ropeChain;

    public override void Load()
    {
        ropeChain = ModContent.Request<Texture2D>(Texture + "_Chain");
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.ForcePlateDetection[Type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        // Projectile.aiStyle = ProjAIStyleID.RopeCoil; not work for modded :(((((
        Projectile.tileCollide = false;
        Projectile.timeLeft = 400;
        Projectile.penetrate = -1;
    }

    // Adapted from projectile 171's DrawExtras
    public override bool PreDrawExtras()
    {
        Vector2 vector7 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
        float num36 = 0f - Projectile.velocity.X;
        float num37 = 0f - Projectile.velocity.Y;
        float num38 = 1f;
        if (Projectile.ai[0] <= 17f)
            num38 = Projectile.ai[0] / 17f;

        int num39 = (int)(30f * num38);
        float num40 = 1f;
        if (Projectile.ai[0] <= 30f)
            num40 = Projectile.ai[0] / 30f;

        float num41 = 0.4f * num40;
        float num42 = num41;
        num37 += num42;
        Vector2[] array = new Vector2[num39];
        float[] array2 = new float[num39];
        for (int k = 0; k < num39; k++)
        {
            float num43 = (float)Math.Sqrt(num36 * num36 + num37 * num37);
            float num44 = 5.6f;
            if (Math.Abs(num36) + Math.Abs(num37) < 1f)
                num44 *= Math.Abs(num36) + Math.Abs(num37) / 1f;

            num43 = num44 / num43;
            num36 *= num43;
            num37 *= num43;
            float num45 = (float)Math.Atan2(num37, num36) - 1.57f;
            array[k].X = vector7.X;
            array[k].Y = vector7.Y;
            array2[k] = num45;
            vector7.X += num36;
            vector7.Y += num37;
            num36 = 0f - Projectile.velocity.X;
            num37 = 0f - Projectile.velocity.Y;
            num42 += num41;
            num37 += num42;
        }

        for (int num46 = --num39; num46 >= 0; num46--)
        {
            vector7.X = array[num46].X;
            vector7.Y = array[num46].Y;
            float rotation4 = array2[num46];
            Color color11 = Lighting.GetColor((int)vector7.X / 16, (int)(vector7.Y / 16f));
            Main.EntitySpriteDraw(ropeChain.Value, new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Rectangle(0, 0, ropeChain.Width(), ropeChain.Height()), color11, rotation4, new Vector2(ropeChain.Width() * 0.5f, ropeChain.Height() * 0.5f), 0.8f, SpriteEffects.None);
        }

        return false;
    }

    // Adaptation of AI 35
    public override void AI()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] > 30f)
        {
            Projectile.velocity.Y += 0.2f;
            Projectile.velocity.X *= 0.985f;
            if (Projectile.velocity.Y > 14f)
                Projectile.velocity.Y = 14f;
        }

        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * Projectile.direction * 0.02f;
        if (Projectile.owner != Main.myPlayer)
            return;

        Vector2 vector25 = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, fallThrough: true, fall2: true);
        bool flag12 = false;
        if (vector25 != Projectile.velocity)
        {
            flag12 = true;
        }
        else
        {
            int num293 = (int)(Projectile.Center.X + Projectile.velocity.X) / 16;
            int num294 = (int)(Projectile.Center.Y + Projectile.velocity.Y) / 16;
            if (Main.tile[num293, num294] != null && Main.tile[num293, num294].HasTile && Main.tile[num293, num294].BottomSlope)
            {
                flag12 = true;
                Projectile.position.Y = num294 * 16 + 16 + 8;
                Projectile.position.X = num293 * 16 + 8;
            }
        }

        if (!flag12)
            return;

        int num295 = ModContent.TileType<Tiles.AsphaltRope>();

        int num296 = (int)(Projectile.position.X + (Projectile.width / 2)) / 16;
        int num297 = (int)(Projectile.position.Y + (Projectile.height / 2)) / 16;
        Projectile.position += vector25;
        int num298 = 10;
        if (Main.tile[num296, num297] == null)
            return;

        bool flag13 = false;
        while (num298 > 0)
        {
            bool flag14 = false;
            if (Main.tile[num296, num297] == null)
                break;

            if (Main.tile[num296, num297].HasTile)
            {
                if (Main.tile[num296, num297].TileType == 314 || TileID.Sets.Platforms[Main.tile[num296, num297].TileType])
                {
                    flag13 = !flag13;
                }
                else if (Main.tileCut[Main.tile[num296, num297].TileType] || Main.tile[num296, num297].TileType == 165)
                {
                    flag13 = false;
                    WorldGen.KillTile(num296, num297);
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, num296, num297);
                }
            }

            if (!Main.tile[num296, num297].HasTile)
            {
                flag13 = false;
                flag14 = true;
                WorldGen.PlaceTile(num296, num297, num295);
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, num296, num297, num295);
                Projectile.ai[1] += 1f;
            }
            else if (!flag13)
            {
                num298 = 0;
            }

            if (flag14)
                num298--;

            num297++;
        }

        Projectile.Kill();
    }

    // Adapted from type 171's kill
    public override void OnKill(int timeLeft)
    {
        if (Projectile.ai[1] == 0)
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

        if (Projectile.ai[1] < 10f)
        {
            Vector2 vector55 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num537 = -Projectile.velocity.X;
            float num538 = -Projectile.velocity.Y;
            float num539 = 1f;
            if (Projectile.ai[0] <= 17f)
                num539 = Projectile.ai[0] / 17f;

            int num540 = (int)(30f * num539);
            float num541 = 1f;
            if (Projectile.ai[0] <= 30f)
                num541 = Projectile.ai[0] / 30f;

            float num542 = 0.4f * num541;
            float num543 = num542;
            num538 += num543;
            for (int num544 = 0; num544 < num540; num544++)
            {
                float num545 = (float)Math.Sqrt(num537 * num537 + num538 * num538);
                float num546 = 5.6f;
                if (Math.Abs(num537) + Math.Abs(num538) < 1f)
                    num546 *= Math.Abs(num537) + Math.Abs(num538) / 1f;

                num545 = num546 / num545;
                num537 *= num545;
                num538 *= num545;
                Math.Atan2(num538, num537);
                if (num544 > Projectile.ai[1])
                {
                    for (int num547 = 0; num547 < 4; num547++)
                    {
                        int num548 = Dust.NewDust(vector55, Projectile.width, Projectile.height, DustID.Asphalt);
                        Main.dust[num548].noGravity = true;
                        Dust dust2 = Main.dust[num548];
                        dust2.velocity *= 0.3f;
                    }
                }

                vector55.X += num537;
                vector55.Y += num538;
                num537 = -Projectile.velocity.X;
                num538 = -Projectile.velocity.Y;
                num543 += num542;
                num538 += num543;
            }
        }

        if (!Projectile.noDropItem)
        {
            if (Projectile.ai[1] == 0f)
            {
                int num1018 = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Items.Placeable.AsphaltRopeCoil>());
                Main.item[num1018].noGrabDelay = 0;
            }
            else if (Projectile.ai[1] < 10f)
            {
                int num1018 = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<AsphaltRope>(), (int)(10f - Projectile.ai[1]));
                Main.item[num1018].noGrabDelay = 0;
            }
        }
    }
}
