using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AsphaltMod.Content.Tiles;

public class AsphaltClock : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.Clock[Type] = true;

        DustType = -1;
        AdjTiles = [TileID.GrandfatherClocks];

        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.GrandfatherClocks, 0));
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(36, 45, 44), Language.GetText("ItemName.GrandfatherClock"));
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void MouseOver(int i, int j)
    {
        Main.LocalPlayer.noThrow = 2;
        Main.LocalPlayer.cursorItemIconEnabled = true;
        Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Items.Placeable.AsphaltClock>();
    }

    public override bool RightClick(int i, int j)
    {
        // Player.TileInteractionsUse for tile type 104

        string amOrPm = Language.GetTextValue("GameUI.TimeAtMorning");
        double time = Main.time;
        if (!Main.dayTime)
            time += 54000.0;

        time = time / 86400.0 * 24.0;
        time = time - 7.5 - 12.0;
        if (time < 0.0)
            time += 24.0;

        if (time >= 12.0)
            amOrPm = Language.GetTextValue("GameUI.TimePastMorning");

        int hours = (int)time;
        int minutes = (int)((time - hours) * 60.0);
        string minutesString = string.Concat(minutes);
        if (minutes < 10.0)
            minutesString = "0" + minutesString;

        if (hours > 12)
            hours -= 12;

        if (hours == 0)
            hours = 12;

        Main.NewText(Language.GetTextValue("Game.Time", $"{hours}:{minutesString} {amOrPm}"), byte.MaxValue, 240, 20);

        return true;
    }
}
