using AsphaltMod.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AsphaltMod.Content.NPCs;

public class AsphaltSlime : ModNPC
{
    private static int[] AsphaltTiles;

    public override void SetStaticDefaults()
    {
        AsphaltTiles = [TileID.Asphalt, ModContent.TileType<Tiles.AsphaltPlatform>(), ModContent.TileType<Tiles.DenseAsphalt>(), ModContent.TileType<Tiles.DenseAsphaltPlatform>()];

        Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];
        NPCID.Sets.ShimmerTransformToNPC[Type] = NPCID.ShimmerSlime;
    }

    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.BlueSlime);
        AnimationType = NPCID.BlueSlime;

        NPC.damage = 45;
        NPC.defense = 25;
        NPC.lifeMax = 180;
        NPC.value = 300;
        NPC.alpha = 50;
        NPC.color = Color.White;
        NPC.aiStyle = -1;

        Banner = Type;
        BannerItem = ModContent.ItemType<AsphaltSlimeBanner>();
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        var slimeDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.BlueSlime, false);
        foreach (var slimeDropRule in slimeDropRules)
            npcLoot.Add(slimeDropRule);

        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AsphaltBlock, chanceNumerator: 3, chanceDenominator: 5));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        return Array.Exists(AsphaltTiles, (type) => type == spawnInfo.SpawnTileType) ? SpawnCondition.OverworldDaySlime.Chance : 0;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.AddTags(
            new FlavorTextBestiaryInfoElement("Mods.AsphaltMod.NPCs.AsphaltSlime.Bestiary"),
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface);
    }

    // Rewrite of slime ai because i cant figure out a way to modify the velocity and cooldown after the normal ai
    public override void AI()
    {
        bool aggressive = false;
        if (!Main.dayTime || NPC.life != NPC.lifeMax || NPC.position.Y > Main.worldSurface * 16.0 || Main.slimeRain)
            aggressive = true;

        if (NPC.ai[2] > 1f)
            NPC.ai[2] -= 1f;

        if (NPC.wet)
        {
            if (NPC.collideY)
                NPC.velocity.Y = -2f;

            if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
            {
                NPC.direction *= -1;
                NPC.ai[2] = 200f;
            }

            if (NPC.velocity.Y > 0f)
            NPC.ai[3] = NPC.position.X;

            if (NPC.velocity.Y > 2f)
                NPC.velocity.Y *= 0.9f;

            NPC.velocity.Y -= 0.5f;
            if (NPC.velocity.Y < -4f)
                NPC.velocity.Y = -4f;

            if (NPC.ai[2] == 1f && aggressive)
                NPC.TargetClosest();
        }

        NPC.aiAction = 0;
        if (NPC.ai[2] == 0f)
        {
            NPC.ai[0] = -100f;
            NPC.ai[2] = 1f;
            NPC.TargetClosest();
        }

        if (NPC.velocity.Y == 0f)
        {
            if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                NPC.position.X -= NPC.velocity.X + NPC.direction;

            if (NPC.ai[3] == NPC.position.X)
            {
                NPC.direction *= -1;
                NPC.ai[2] = 200f;
            }

            NPC.ai[3] = 0f;
            NPC.velocity.X *= 0.8f;
            if (NPC.velocity.X > -0.1 && NPC.velocity.X < 0.1)
                NPC.velocity.X = 0f;

            if (aggressive)
                NPC.ai[0] += 1f;
            NPC.ai[0] += 4f;

            float attackTimer = -1000f;

            int attack = 0;
            if (NPC.ai[0] >= 0f)
                attack = 1;

            if (NPC.ai[0] >= attackTimer && NPC.ai[0] <= attackTimer * 0.5f)
                attack = 2;

            if (NPC.ai[0] >= attackTimer * 2f && NPC.ai[0] <= attackTimer * 1.5f)
                attack = 3;

            if (attack > 0)
            {
                NPC.netUpdate = true;
                if (aggressive && NPC.ai[2] == 1f)
                    NPC.TargetClosest();

                if (attack == 3)
                {
                    NPC.velocity.Y = -8f;
                    NPC.velocity.X += 4f * NPC.direction;
                    NPC.ai[0] = -200f;
                    NPC.ai[3] = NPC.position.X;
                }
                else
                {
                    NPC.velocity.Y = -6f;
                    NPC.velocity.X += 3 * NPC.direction;
                    NPC.ai[0] = -120f;
                    if (attack == 1)
                        NPC.ai[0] += attackTimer;
                    else
                        NPC.ai[0] += attackTimer * 2f;
                }
            }
            else if (NPC.ai[0] >= -30f)
            {
                NPC.aiAction = 1;
            }
        }
        else if (NPC.target < 255 && NPC.velocity.X < 4f * NPC.direction)
        {
            if (NPC.collideX && Math.Abs(NPC.velocity.X) == 0.2f)
                NPC.position.X -= 1.4f * NPC.direction;

            if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                NPC.position.X -= NPC.velocity.X + NPC.direction;

            if ((NPC.direction == -1 && NPC.velocity.X < 0.01) || (NPC.direction == 1 && NPC.velocity.X > -0.01))
                NPC.velocity.X += 0.2f * NPC.direction;
            else
                NPC.velocity.X *= 0.93f;
        }
    }

    // Adapted from VanillaHitEffect() for slimes
    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0)
        {
            for (int i = 0; i < hit.Damage / (double)NPC.lifeMax * 100.0; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Asphalt, hit.HitDirection, -1f, 200);
            }
        }
        else
        {
            for (int i = 0; i < 50; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Asphalt, 2 * hit.HitDirection, -2f, 200);
            }
        }
    }
}
