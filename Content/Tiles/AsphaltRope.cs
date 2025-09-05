using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AsphaltMod.Content.Tiles;

public class AsphaltRope : ModTile
{
    private const float AsphaltRopeAcceleration = 0.04f;
    private const float AsphaltRopeMaxAscendSpeed = -16f;
    private const float AsphaltRopeMaxDescentSpeedMultiplier = 2f;

    public override void Load()
    {
        try
        { 
            IL_Player.Update += AsphaltRopeMovement;
        }
        catch
        {
            MonoModHooks.DumpILHooks();
        }
    }

    public override void SetStaticDefaults()
    {
        Main.tileLavaDeath[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileRope[Type] = true;
        Main.tileSolid[Type] = false;

        AddMapEntry(new Color(32, 33, 34), Lang.GetItemName(ItemID.Rope));
        DustType = DustID.Asphalt;
    }

    private static bool IsAsphaltRope(int tileX, int tileY) => Main.tile[tileX, tileY].TileType == ModContent.TileType<AsphaltRope>();

    private static void ModifyRopeAcceleration(Player player)
    {
        player.velocity.Y -= AsphaltRopeAcceleration;
    }

    private static bool ModifyRopeMaxSpeed(Player player, bool asphaltRope)
    {
        if (asphaltRope && player.velocity.Y < AsphaltRopeMaxAscendSpeed)
            player.velocity.Y = AsphaltRopeMaxAscendSpeed;
        return asphaltRope;
    }

    private static bool ModifyRopeMaxFallSpeed(Player player, bool asphaltRope)
    {
        if (asphaltRope && player.velocity.Y > player.maxFallSpeed * AsphaltRopeMaxDescentSpeedMultiplier)
            player.velocity.Y = player.maxFallSpeed * AsphaltRopeMaxDescentSpeedMultiplier;
        return asphaltRope;
    }

    private static void AsphaltRopeMovement(ILContext il)
    {
        try
        {
            ILCursor cursor = new(il);
            int[] positions = new int[7];
            byte currentLoop = 0;

            // Find line 21739 `if (this.pulley)`
            while (cursor.TryGotoNext(i => i.MatchLdfld(out FieldReference fieldReference) && fieldReference.FullName == "System.Boolean Terraria.Player::pulley"))
            {
                if (!cursor.Prev.MatchLdarg0() || !cursor.Next.Next.MatchBrfalse(out _))
                    continue;

                positions[currentLoop++] = cursor.Index;

                // Find line 21745 `int num39 = (int)(position.X + (float)(width / 2)) / 16;`
                while (cursor.TryGotoNext(i => i.MatchStloc(out _) && i.Previous.MatchDiv() && i.Previous.Previous.MatchLdcI4(16)))
                {
                    int tileX = 0;
                    if (cursor.Next.Operand is VariableDefinition vD1)
                        tileX = vD1.Index;
                    else
                        continue;

                    // Find line 21747 `int num41 = (int)(position.Y - 8f) / 16;`
                    if (!cursor.TryGotoNext(i => i.MatchLdcR4(8f)) || !cursor.TryGotoNext(i => i.MatchStloc(out _)))
                        continue;

                    int tileY = 0;
                    if (cursor.Next.Operand is VariableDefinition vD2)
                        tileY = vD2.Index;
                    else
                        continue;

                    positions[currentLoop++] = cursor.Index;

                    // Find line 21794 `if (velocity.Y > -3f)`
                    while (cursor.TryGotoNext(i => i.MatchLdcR4(-3) && i.Previous.MatchLdfld(out FieldReference fR) && fR.FullName == "System.Single Microsoft.Xna.Framework.Vector2::Y" && i.Next.MatchBleUn(out _)))
                    {
                        Instruction accelElse = cursor.Next.Next; // IL_3cad: ble.un.s

                        cursor.Index += 2;

                        if (!cursor.TryGotoNext(i => i.MatchBr(out _))) // IL_3cc3: br
                            continue;

                        ILLabel accelEverySkipTo = null;
                        if (cursor.Next.Operand is ILLabel ilL2)
                            accelEverySkipTo = ilL2; // The label that IL_3cc3 skips to
                        else
                            continue;

                        Instruction insertionLocation1 = cursor.Next.Next; // Right after IL_3cc3: br

                        positions[currentLoop++] = cursor.Index;

                        // Find line 21799 `if (velocity.Y < -8f)`
                        while (cursor.TryGotoNext(i => i.MatchLdcR4(-8f)))
                        {
                            if (!cursor.Next.Next.MatchBgeUn(out _) || !cursor.Prev.MatchLdfld(out FieldReference fieldReference) && fieldReference.FullName == "System.Single Microsoft.Xna.Framework.Vector2::Y")
                                continue;

                            positions[currentLoop++] = cursor.Index;

                            if (!cursor.TryGotoNext(i => i.MatchBr(out _)))
                                continue;

                            Instruction maxSpeedSkipTo = cursor.Next;

                            // Move the cursor to right before the line
                            cursor.Goto(positions[currentLoop - 1]);
                            if (!cursor.TryGotoPrev(MoveType.After, i => i.MatchStindR4()))
                            {
                                cursor.Goto(positions[--currentLoop] + 1);
                                continue;
                            }

                            Instruction insertionLocation2 = cursor.Next;

                            positions[currentLoop++] = cursor.Index;

                            // Find line 21838 `if (velocity.Y > maxFallSpeed)`
                            while (cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld(out FieldReference fR) && fR.FullName == "System.Single Terraria.Player::maxFallSpeed" && i.Next.MatchBleUn(out _)))
                            {
                                ILLabel maxFallSpeedSkipTo = null;
                                if (cursor.Next.Operand is ILLabel ilL3)
                                    maxFallSpeedSkipTo = ilL3; // The label that IL_3f05 skips to
                                else
                                    continue;

                                positions[currentLoop++] = cursor.Index;

                                // Go to right before the start of the line
                                if (!cursor.TryGotoPrev(MoveType.After, i => i.MatchStindR4()))
                                {
                                    cursor.Goto(positions[--currentLoop] + 1);
                                    continue;
                                }

                                Instruction insertionLocation3 = cursor.Next;

                                // Now it's our actual code :D
                                // From this point forth, we only modify and go to places we already know well
                                // We do this because I'm afraid to break something if we do trygotos and store indexes after editing

                                // Go back to the place where we want modify the acceleration
                                cursor.Goto(insertionLocation1, MoveType.AfterLabel);
                                ILLabel edit1 = il.DefineLabel();

                                // Run `else if (IsAsphaltRope(num39, num41))`
                                cursor.EmitLdloc(tileX);
                                cursor.EmitLdloc(tileY);
                                cursor.EmitDelegate(IsAsphaltRope);
                                cursor.EmitBrfalse(edit1);

                                // Run `ModifyRopeAcceleration(this)`
                                cursor.EmitLdarg0();
                                cursor.EmitDelegate(ModifyRopeAcceleration);

                                // Skip the final `else` statement
                                cursor.EmitBr(accelEverySkipTo);

                                cursor.MarkLabel(edit1);

                                // Go to the place where we want to modify the max speed
                                cursor.Goto(insertionLocation2, MoveType.AfterLabel);
                                ILLabel edit2 = il.DefineLabel();

                                // Run `ModifyRopeMaxSpeed(this, IsAsphaltRope(num39, num41))`
                                cursor.EmitLdarg0();
                                cursor.EmitLdloc(tileX);
                                cursor.EmitLdloc(tileY);
                                cursor.EmitDelegate(IsAsphaltRope);
                                cursor.EmitDelegate(ModifyRopeMaxSpeed);

                                // If it returned true, skip the vanilla max speed limit enforcement
                                cursor.EmitBrtrue(maxSpeedSkipTo);

                                cursor.MarkLabel(edit2);

                                // Go to the place where we want to modify the max fall speed
                                cursor.Goto(insertionLocation3, MoveType.AfterLabel);
                                ILLabel edit3 = il.DefineLabel();

                                // Run `ModifyRopeMaxFallSpeed(this, IsAsphaltRope(num39, num41))`
                                cursor.EmitLdarg0();
                                cursor.EmitLdloc(tileX);
                                cursor.EmitLdloc(tileY);
                                cursor.EmitDelegate(IsAsphaltRope);
                                cursor.EmitDelegate(ModifyRopeMaxFallSpeed);

                                // If it returned true, skip the vanilla max speed limit enforcement
                                cursor.EmitBrtrue(maxFallSpeedSkipTo);

                                cursor.MarkLabel(edit3);

                                return;
                            }
                            cursor.Goto(positions[currentLoop -= 2] + 1);
                        }
                        cursor.Goto(positions[--currentLoop] + 1);
                    }
                    cursor.Goto(positions[--currentLoop] + 1);
                }
                cursor.Goto(positions[--currentLoop] + 1);
            }
            throw new Exception("AsphaltRopeMovement couldn't find a valid location for the IL edit");
        }
        catch
        {
            MonoModHooks.DumpIL(ModContent.GetInstance<AsphaltMod>(), il);
        }
    }
}
