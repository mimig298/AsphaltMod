﻿using Terraria;
using Terraria.ModLoader;
using AsphaltMod.Common.Players;

namespace AsphaltMod.Content.Buffs
{
    public class AsphaltSpeed : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffPlayer>().AsphaltSpeedRegen = true;
            player.maxRunSpeed *= 3.5f;
            player.runSlowdown += 2f;
        }
    }
}