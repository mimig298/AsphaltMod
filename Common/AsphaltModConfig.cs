using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AsphaltMod.Common;

public class AsphaltModConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [ReloadRequired]
    [DefaultValue(true)]
    public bool BalanceChanges;
}
