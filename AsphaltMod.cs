using AsphaltMod.Common;
using Terraria.ModLoader;

namespace AsphaltMod
{
	public class AsphaltMod : Mod
	{
        public bool BalanceChanges { get; private set; }

        public override void PostSetupContent()
        {
            BalanceChanges = ModContent.GetInstance<AsphaltModConfig>().BalanceChanges;
        }
	}
}