using AsphaltMod.Common;
using System.IO;
using Terraria;
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

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte type = reader.ReadByte();
            switch (type) 
            {
                case 0:
                    if (Main.LocalPlayer.TryGetModPlayer(out Common.Players.CarPlayer carPlayer))
                    {
                        carPlayer.ReceiveClientChanges();
                    }
                    break;
                default:
                    Logger.WarnFormat("Unknown packet type {0}", type);
                    break;
            }
        }
	}
}
