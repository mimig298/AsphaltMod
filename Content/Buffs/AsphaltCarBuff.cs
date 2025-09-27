using AsphaltMod.Content.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Buffs;

public class AsphaltCarBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.mount.SetMount(ModContent.MountType<AsphaltCar>(), player);
        player.buffTime[buffIndex] = 10;
    }
}
