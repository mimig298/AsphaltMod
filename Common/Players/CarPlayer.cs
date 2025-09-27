using AsphaltMod.Content.Mounts;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AsphaltMod.Common.Players;

public class CarPlayer : ModPlayer
{
    public bool lightsOn = false;
    public int lightCooldown = 0;
    public int velDirection = 0;

    public override void CopyClientState(ModPlayer targetCopy)
    {
        ((CarPlayer)targetCopy).lightsOn = lightsOn;
    }

    public override void SendClientChanges(ModPlayer clientPlayer)
    {
        CarPlayer clone = (CarPlayer)clientPlayer;
        if (Player.mount.Active && Player.mount.Type == ModContent.MountType<AsphaltCar>() && clone.lightsOn != lightsOn)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)0);
            packet.Send();
        }
    }

    internal void ReceiveClientChanges()
    {
        lightsOn = !lightsOn;
    }

    public override void PostUpdate()
    {
        velDirection = Player.direction;
        if (Player.velocity.X != 0)
            velDirection = float.Sign(Player.velocity.X);
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (Player.mount.Active && Player.mount.Type == ModContent.MountType<AsphaltCar>())
        {
            if (Player.velocity.X > 0)
            {
                drawInfo.playerEffect &= ~SpriteEffects.FlipHorizontally;
            }
            else if (Player.velocity.X < 0)
            {
                drawInfo.playerEffect |= SpriteEffects.FlipHorizontally;
            }
        }
    }
}
