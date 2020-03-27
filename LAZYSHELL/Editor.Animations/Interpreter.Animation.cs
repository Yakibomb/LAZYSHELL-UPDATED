using System;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        private Settings settings = Settings.Default;
        #region Static Data
        private static string[] AnimationCommands = new string[]
        {
            "New object: sprite = \"{0}\" (sequence = {1}), coords (AMEM $32)",			// 0x00
            "AMEM $32 = coords ({0},{1},{2}), origin = {3}",			// 0x01
            "",			// 0x02
            "Sprite = \"{0}\" (sequence = {1}), coords (AMEM $32)",			// 0x03
            "Pause script until {0}",			// 0x04
            "Remove object",			// 0x05
            "",			// 0x06
            "Return object queue",			// 0x07
            "Move object: speed = {0}, startPos = {1}, endPos = {2}, axes ({3})",			// 0x08
            "Jump to address ${0}",			// 0x09
            "Pause script for 1 frame",			// 0x0A
            "AMEM $40 = coords ({0},{1},{2}), origin = {3}",			// 0x0B
            "{0} sprite to coords (AMEM $40), speed = {1}, archHeight = {2}",			// 0x0C
            "",			// 0x0D
            "Reset target mapping memory",			// 0x0E
            "Reset object mapping memory",			// 0x0F
			
            "Jump to subroutine ${0}",			// 0x10
            "Return subroutine",			// 0x11
            "",			// 0x12
            "",			// 0x13
            "",			// 0x14
            "",			// 0x15
            "",			// 0x16
            "",			// 0x17
            "",			// 0x18
            "",			// 0x19
            "{0} on",			// 0x1A
            "{0} off",			// 0x1B
            "",			// 0x1C
            "",			// 0x1D
            "",			// 0x1E
            "",			// 0x1F
			
            "AMEM (8-bit) ${0} = {1}",			// 0x20
            "AMEM (16-bit) ${0} = {1}",			// 0x21
            "{0} = AMEM (8-bit) ${1}",			// 0x22
            "{0} = AMEM (16-bit) ${1}",			// 0x23
            "If AMEM (8-bit) ${0} = {1}, jump to ${2}",			// 0x24
            "If AMEM (16-bit) ${0} = {1}, jump to ${2}",			// 0x25
            "If AMEM (8-bit) ${0} != {1}, jump to ${2}",			// 0x26
            "If AMEM (16-bit) ${0} != {1}, jump to ${2}",			// 0x27
            "If AMEM (8-bit) ${0} < {1}, jump to ${2}",			// 0x28
            "If AMEM (16-bit) ${0} < {1}, jump to ${2}",			// 0x29
            "If AMEM (8-bit) ${0} >= {1}, jump to ${2}",			// 0x2A
            "If AMEM (16-bit) ${0} >= {1}, jump to ${2}",			// 0x2B
            "AMEM (8-bit) ${0} += {1}",			// 0x2C
            "AMEM (16-bit) ${0} += {1}",			// 0x2D
            "AMEM (8-bit) ${0} -= {1}",			// 0x2E
            "AMEM (16-bit) ${0} -= {1}",			// 0x2F
			
            "Increment AMEM (8-bit) ${0}",			// 0x30
            "Increment AMEM (16-bit) ${0}",			// 0x31
            "Decrement AMEM (8-bit) ${0}",			// 0x32
            "Decrement AMEM (16-bit) ${0}",			// 0x33
            "Clear AMEM (8-bit) ${0}",			// 0x34
            "Clear AMEM (16-bit) ${0}",			// 0x35
            "Set AMEM ${0} bits {1}",			// 0x36
            "Clear AMEM ${0} bits {1}",			// 0x37
            "If AMEM ${0} bits {1} set, jump to ${2}",			// 0x38
            "If AMEM ${0} bits {1} clear, jump to ${2}",			// 0x39
            "Attack timer begins",			// 0x3A
            "",			// 0x3B
            "",			// 0x3C
            "",			// 0x3D
            "",			// 0x3E
            "",			// 0x3F
			
            "Pause script until AMEM ${0} bits {1} set",			// 0x40
            "Pause script until AMEM ${0} bits {1} clear",			// 0x41
            "",			// 0x42
            "Sprite sequence = {0} ({1}, {2})",			// 0x43
            "",			// 0x44
            "AMEM $60 = current target",			// 0x45
            "",			// 0x46
            "",			// 0x47
            "",			// 0x48
            "",			// 0x49
            "",			// 0x4A
            "",			// 0x4B
            "",			// 0x4C
            "",			// 0x4D
            "Pause script until sprite sequence done",			// 0x4E
            "",			// 0x4F
			
            "If target disabled, jump to address ${0}",			// 0x50
            "If target alive, jump to address ${0}",			// 0x51
            "",			// 0x52
            "",			// 0x53
            "",			// 0x54
            "",			// 0x55
            "",			// 0x56
            "",			// 0x57
            "",			// 0x58
            "",			// 0x59
            "",			// 0x5A
            "",			// 0x5B
            "",			// 0x5C
            "Sprite queue [${0}] (sprite = {1} #{2})",			// 0x5D
            "Return sprite queue",			// 0x5E
            "",			// 0x5F
			
            "",			// 0x60
            "",			// 0x61
            "",			// 0x62
            "Display {0} @ OMEM $60",			// 0x63
            "Object queue [${0}] index = AMEM $60",			// 0x64
            "",			// 0x65
            "",			// 0x66
            "",			// 0x67
            "Object queue [${0}, AMEM $60] index = {1}",			// 0x68
            "OMEM $60 = memory $072C",			// 0x69
            "AMEM ${0} = random # between 0 and {1}",			// 0x6A
            "AMEM ${0} = random # between 0 and {1}",			// 0x6B
            "",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
			
            "Enable sprites on subscreen",			// 0x70
            "Disable sprites on subscreen",			// 0x71
            "New object: effect = {0}",			// 0x72
            "Pause script for 2 frames",			// 0x73
            "Pause script until {0}",			// 0x74
            "Pause script until {0}",			// 0x75
            "Clear effect index",			// 0x76
            "L3 on: {0} ({1})",			// 0x77
            "L3 off: {0} ({1})",			// 0x78
            "",			// 0x79
            "Display {0}: {1}",			// 0x7A
            "Pause script until dialogue closed",			// 0x7B
            "",			// 0x7C
            "",			// 0x7D
            "Fade out object, duration = {0}",			// 0x7E
            "Reset sprite sequence",			// 0x7F
			
            "Shine effect: {0} (duration = {1})",			// 0x80
            "",			// 0x81
            "",			// 0x82
            "",			// 0x83
            "",			// 0x84
            "{0} object: {1}, duration = {2}",			// 0x85
            "Shake object: {0} (amount = {1}, speed = {2})",			// 0x86
            "Stop shaking object",			// 0x87
            "",			// 0x88
            "",			// 0x89
            "",			// 0x8A
            "",			// 0x8B
            "",			// 0x8C
            "",			// 0x8D
            "Screen flash {0} (duration = {1})",			// 0x8E
            "Screen flash {0}",			// 0x8F
			
            "",			// 0x90
            "",			// 0x91
            "",			// 0x92
            "",			// 0x93
            "",			// 0x94
            "Initialize bonus message sequence",			// 0x95
            "Display bonus message: {0} coords ({1},{2})",			// 0x96
            "Pause script until bonus message complete",			// 0x97
            "",			// 0x98
            "",			// 0x99
            "",			// 0x9A
            "",			// 0x9B
            "",			// 0x9C
            "",			// 0x9D
            "",			// 0x9E
            "",			// 0x9F
			
            "",			// 0xA0
            "",			// 0xA1
            "",			// 0xA2
            "Screen effect: {0}",			// 0xA3
            "",			// 0xA4
            "",			// 0xA5
            "",			// 0xA6
            "",			// 0xA7
            "",			// 0xA8
            "",			// 0xA9
            "",			// 0xAA
            "Play sound (ch.6,7): {0}",			// 0xAB
            "",			// 0xAC
            "",			// 0xAD
            "Play sound (ch.4,5): {0}",			// 0xAE
            "",			// 0xAF
			
            "Play music: {0} (current volume)",			// 0xB0
            "Play music: {0} (volume = {1})",			// 0xB1
            "Stop current sound effect",			// 0xB2
            "",			// 0xB3
            "",			// 0xB4
            "",			// 0xB5
            "Fade out current music to volume {1} (speed: {0})",			// 0xB6
            "",			// 0xB7
            "",			// 0xB8
            "",			// 0xB9
            "",			// 0xBA
            "Set target: {0}",			// 0xBB
            "{0} item inventory: {1}",			// 0xBC
            "{0} special item inventory: {1}",			// 0xBD
            "Coins += {0}",			// 0xBE
            "Store to item inventory Yoshi Cookie of target: {0}",			// 0xBF
			
            "",			// 0xC0
            "",			// 0xC1
            "",			// 0xC2
            "Mask: {0}",			// 0xC3
            "",			// 0xC4
            "",			// 0xC5
            "Mask coords = {{ {0} }}",			// 0xC6
            "",			// 0xC7
            "",			// 0xC8
            "",			// 0xC9
            "",			// 0xCA
            "Sprite sequence speed = {0}",			// 0xCB
            "Timing: Start tracking ally button inputs",			// 0xCC
            "Timing: End tracking ally button inputs ",			// 0xCD
            "Timing: Press A/B/X/Y within range {1}-{0} frames. Press before {2}-{3}-{4} frame to Time, else jump to {5}",  // 0xCE
            "Timing: Press A/B/X/Y within range {1}-{0} frames. Press before {2} frame to Time, else jump to {3}",			// 0xCF
			
            "Timing: Push A/B/X/Y within {0} frames before jumping to {1}",			// 0xD0
            "Timing: for Button Mash... ???",			// 0xD1
            "Timing: Mash A/B/X/Y up to {0} times ",			// 0xD2
            "Timing: Rotate D-Pad from {1}-{0} frames, up to {2} times",			// 0xD3
            "Timing: Hold to Charge for {0}-{1}-{2}-{3}-{4} frames",			// 0xD4
            "Summon monster: {0}, formation position #{1}",			// 0xD5
            "",			// 0xD6
            "",			// 0xD7
            "Timing: ... for Mute: Jump to {0}",			// 0xD8
            "Display \"Can\'t run\" dialogue",			// 0xD9
            "",			// 0xDA
            "",			// 0xDB
            "",			// 0xDC
            "",			// 0xDD
            "",			// 0xDE
            "",			// 0xDF
			
            "Store OMEM $60 to item inventory",			// 0xE0
            "Run battle event: {0} [offset += {1}]",			// 0xE1
            "",			// 0xE2
            "",			// 0xE3
            "",			// 0xE4
            "",			// 0xE5
            "",			// 0xE6
            "",			// 0xE7
            "",			// 0xE8
            "",			// 0xE9
            "",			// 0xEA
            "",			// 0xEB
            "",			// 0xEC
            "",			// 0xED
            "",			// 0xEE
            "",			// 0xEF
			
            "",			// 0xF0
            "",			// 0xF1
            "",			// 0xF2
            "",			// 0xF3
            "",			// 0xF4
            "",			// 0xF4
            "",			// 0xF6
            "",			// 0xF7
            "",			// 0xF8
            "",			// 0xF9
            "",			// 0xFA
            "",			// 0xFB
            "",			// 0xFC
            "",         // 0xFD
            "",         // 0xFE
            ""			// 0xFF
        };
        public static string[] ScreenEffects = new string[]
        {
            "Geno Flash",
            "Snowy",
            "Terrorize",
            "Shocker",
            "{unknown}",
            "slash (instant death)",
            "screen flashes white",
            "change battlefield",
            "Come Back",
            "Geno Beam",
            "Geno Blast",
            "Howl",
            "win battle window",
            "set battlefield coords",
            "squash big star",
            "{unknown}",
            "{unknown}",
            "Corona",
            "Mega-Drain",
            "{unknown}",
            "{unknown}"
        };
        private static string[] MaskEffects = new string[] 
        { 
            "...", "incline", "incline", "circle", "dome", 
            "polygon", "wavy circle", "cylinder"
        };
        public static string[] VariableNames = new string[]
        {
            "absolute value",
            "$7E:xxxx",
            "$7F:xxxx",
            "AMEM $6x",
            "OMEM (current)",
            "$7E:xxxx",
            "OMEM (main)",
            "{07}",
            "{08}",
            "{09}",
            "{0A}",
            "{0B}"
        };
        #endregion
        public string InterpretCommand(AnimationCommand asc)
        {
            string[] vars = new string[16];
            switch (asc.Opcode)
            {
                case 0x01:
                case 0x0B:
                    vars[0] = ((short)Bits.GetShort(asc.CommandData, 2)).ToString();
                    vars[1] = ((short)Bits.GetShort(asc.CommandData, 4)).ToString();
                    vars[2] = ((short)Bits.GetShort(asc.CommandData, 6)).ToString();
                    switch ((asc.CommandData[1] & 0xF0) >> 4)
                    {
                        case 0: vars[3] = "absolute position"; break;
                        case 1: vars[3] = "caster's initial position"; break;
                        case 2: vars[3] = "target's current position"; break;
                        case 3: vars[3] = "caster's current position"; break;
                    }
                    break;
                case 0x08:
                    vars[0] = Bits.GetShort(asc.CommandData, 6).ToString();
                    vars[1] = ((short)Bits.GetShort(asc.CommandData, 2)).ToString();
                    vars[2] = ((short)Bits.GetShort(asc.CommandData, 4)).ToString();
                    vars[3] = GetBits((byte)(asc.Param1 & 0x07), new string[] { "Z", "Y", "X" }, 3);
                    break;
                case 0x0C:
                    switch (asc.Param1)
                    {
                        case 0x02: vars[0] = "Shift"; break;
                        case 0x04: vars[0] = "Transfer"; break;
                    }
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 4).ToString();
                    break;
                case 0x00:
                case 0x03:
                    ushort temp = (ushort)(Bits.GetShort(asc.CommandData, 3) & 0x3FF);
                    vars[0] = Lists.Numerize(Lists.SpriteNames, temp);
                    vars[1] = (asc.Param5 & 15).ToString();
                    break;
                case 0x04:
                    switch (asc.Param1)
                    {
                        case 0x06: vars[0] = "shift complete"; break;
                        case 0x10: vars[0] = Bits.GetShort(asc.CommandData, 2) + " frames elapsed"; break;
                        default: vars[0] = "{" + BitConverter.ToString(asc.CommandData, 1) + "}"; break;
                    }
                    break;
                case 0x09:
                case 0x10:
                case 0x50:
                case 0x51:
                    vars[0] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 1).ToString("X4");
                    break;
                case 0x1A:
                case 0x1B:
                    if (asc.Param1 == 0)
                        vars[0] = "{nothing}";
                    else if (asc.Param1 == 1)
                        vars[0] = "Visibility";
                    else
                        vars[0] = "{unknown}";
                    break;
                case 0x20:
                case 0x21:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x2F:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    switch ((asc.Param1 & 0xF0) >> 4)
                    {
                        case 0: vars[1] = Bits.GetShort(asc.CommandData, 2).ToString(); break;
                        case 1: vars[1] = "$7E" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 2: vars[1] = "$7F" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 3: vars[1] = "AMEM $" + (asc.Param2 + 0x60).ToString("X2"); break;
                        case 4: vars[1] = "OMEM (current) $" + asc.Param2.ToString("X2"); break;
                        case 5: vars[1] = "$7E" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 6: vars[1] = "OMEM (main) $" + asc.Param2.ToString("X2"); break;
                    }
                    break;
                case 0x22:
                case 0x23:
                    switch ((asc.Param1 & 0xF0) >> 4)
                    {
                        case 0: vars[0] = "N/A"; break;
                        case 1: vars[0] = "$7E" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 2: vars[0] = "$7F" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 3: vars[0] = "AMEM $" + (asc.Param2 + 0x60).ToString("X2"); break;
                        case 4: vars[0] = "OMEM (current) $" + asc.Param2.ToString("X2"); break;
                        case 5: vars[0] = "$7E" + (Bits.GetShort(asc.CommandData, 2)).ToString("X4"); break;
                        case 6: vars[0] = "OMEM (main) $" + asc.Param2.ToString("X2"); break;
                    }
                    vars[1] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    break;
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    switch ((asc.Param1 & 0xF0) >> 4)
                    {
                        case 0: vars[1] = Bits.GetShort(asc.CommandData, 2).ToString(); break;
                        case 1: vars[1] = "$7E" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 2: vars[1] = "$7F" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 3: vars[1] = "AMEM $" + Bits.GetShort(asc.CommandData, 2).ToString("X2"); break;
                        case 4: vars[1] = "OMEM (current) $" + Bits.GetShort(asc.CommandData, 2).ToString("X2"); break;
                        case 5: vars[1] = "$7E" + Bits.GetShort(asc.CommandData, 2).ToString("X4"); break;
                        case 6: vars[1] = "OMEM (main) $" + Bits.GetShort(asc.CommandData, 2).ToString("X2"); break;
                    }
                    vars[2] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 4).ToString("X4");
                    break;
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35: vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2"); break;
                case 0x36:
                case 0x37:
                case 0x40:
                case 0x41:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    vars[1] = GetBits(asc.Param2, new string[] { "0", "1", "2", "3", "4", "5", "6", "7" }, 8);
                    break;
                case 0x38:
                case 0x39:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    vars[1] = GetBits(asc.Param2, new string[] { "0", "1", "2", "3", "4", "5", "6", "7" }, 8);
                    vars[2] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0x5D:
                    vars[0] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    switch (asc.Param1 & 0xF8)
                    {
                        case 0x08: vars[1] = "slot"; break;
                        case 0x10: vars[1] = "???"; break;
                        case 0x20: vars[1] = "???"; break;
                        case 0x40: vars[1] = "target"; break;
                        case 0x80: vars[1] = "???"; break;
                    }
                    vars[2] = asc.Param2.ToString();
                    break;
                case 0x63:
                    switch (asc.Param1)
                    {
                        case 0x00: vars[0] = "attack name"; break;
                        case 0x01: vars[0] = "spell name"; break;
                        case 0x02: vars[0] = "item name"; break;
                    }
                    break;
                case 0x64:
                case 0x68:
                    vars[0] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 1).ToString("X4");
                    if (asc.Opcode == 0x68)
                        vars[1] = asc.Param3.ToString();
                    break;
                case 0x6A:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0x6B:
                    vars[0] = ((asc.Param1 & 0x0F) + 0x60).ToString("X2");
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    break;
                case 0x72:
                    vars[0] = Lists.Numerize(Lists.EffectNames, asc.Param2);
                    break;
                case 0x7A:
                    switch (asc.Param1 & 3)
                    {
                        case 0x00:
                            vars[0] = "battle dialogue";
                            vars[1] = "{" + asc.Param2.ToString("X2") + "}  \"" +
                                Model.BattleDialogues[asc.Param2].GetStub() + "\"";
                            break;
                        case 0x01:
                            vars[0] = "psychopath message";
                            vars[1] = "{" + asc.Param2.ToString("X2") + "}  \"" + "..." + "\"";
                            break;
                        case 0x02:
                            vars[0] = "battle message";
                            vars[1] = "{" + asc.Param2.ToString("X2") + "}  \"" + "..." + "\"";
                            break;
                    }
                    break;
                case 0x80:
                    vars[0] = asc.Param1 != 0 ? "westward reflection" : "eastward reflection";
                    vars[1] = asc.Param3.ToString();
                    break;
                case 0x96:
                    if (asc.Param2 < 7)
                        vars[0] = Model.BonusMessages[asc.Param2].Text;
                    else
                        vars[0] = "null";
                    vars[1] = ((sbyte)asc.Param3).ToString();
                    vars[2] = ((sbyte)asc.Param4).ToString();
                    break;
                case 0x43:
                    vars[0] = (asc.Param1 & 0x0F).ToString();
                    if ((asc.Param1 & 0x10) == 0x10)
                        vars[1] = "looping on";
                    else if ((asc.Param1 & 0x20) == 0x20)
                        vars[1] = "looping off";
                    vars[2] = (asc.Param1 & 0x80) == 0x80 ? "mirror" : "...";
                    break;
                case 0xC3: vars[0] = MaskEffects[asc.Param1 & 0x0F]; break;
                case 0x7E:
                case 0xCB: vars[0] = asc.Param1.ToString(); break;
                case 0x85:
                    if (((asc.Param1 & 0x0F) >> 1) == 0)
                        vars[0] = "Fade out";
                    else
                        vars[0] = "Fade in";
                    switch (asc.Param1 >> 4)
                    {
                        case 0: vars[1] = "effect"; break;
                        case 1: vars[1] = "sprite"; break;
                        case 2: vars[1] = "screen"; break;
                        default: vars[1] = (asc.Param1 >> 4).ToString(); break;
                    }
                    vars[2] = asc.Param2.ToString(); break;
                case 0xAB:
                case 0xAE:
                    if (asc.Param1 < Lists.BattleSoundNames.Length)
                        vars[0] = Lists.Numerize(Lists.BattleSoundNames, asc.Param1);
                    else
                        vars[0] = "{INVALID}";
                    break;
                case 0xB0: vars[0] = Lists.Numerize(Lists.MusicNames, asc.Param1); break;
                case 0xB1:
                    vars[0] = Lists.Numerize(Lists.MusicNames, asc.Param1);
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString(); break;
                case 0xC6:
                    for (int i = 0; i < asc.Param1; i++)
                    {
                        if (i % 2 == 0)
                            vars[0] += "(" + (sbyte)asc.CommandData[i + 2] + ",";
                        else
                            vars[0] += (sbyte)asc.CommandData[i + 2] + ")";
                        if (i % 2 != 0 && i < asc.Param1 - 1)
                            vars[0] += ", ";
                    }
                    break;
                case 0x74:
                    switch (Bits.GetShort(asc.CommandData, 1))
                    {
                        case 0x0004:
                            vars[0] = "sequence complete (4bpp)"; break;
                        case 0x0008:
                            vars[0] = "sequence complete (2bpp)"; break;
                        case 0x0200:
                            vars[0] = "fade in complete"; break;
                        case 0x0400:
                            vars[0] = "fade complete (4bpp)"; break;
                        case 0x0800:
                            vars[0] = "fade complete (2bpp)"; break;
                        default:
                            vars[0] = "bits " + Bits.GetShort(asc.CommandData, 1).ToString("X4") + " set"; break;
                    }
                    break;
                case 0x75:
                    vars[0] = "bits " + Bits.GetShort(asc.CommandData, 1).ToString("X4") + " clear"; break;
                case 0x77:
                case 0x78:
                    string[] overlap = new string[] 
                    { 
                        "transparency off", "overlap all", "overlap none", "overlap all except allies" 
                    };
                    vars[0] = overlap[asc.Param1 >> 4];
                    if ((asc.Param1 & 0x02) == 0x02)
                        vars[1] = "4bpp";
                    if ((asc.Param1 & 0x04) == 0x04)
                        vars[1] = "2bpp";
                    break;
                case 0xBB: vars[0] = Lists.Numerize(Lists.TargetNames, asc.Param1); break;
                case 0xBC:
                case 0xBD:
                    if (asc.Param2 == 0xFF)
                    {
                        vars[0] = "Remove item from";
                        vars[1] = Model.ItemNames.NumerizeUnsorted(Math.Abs((short)Bits.GetShort(asc.CommandData, 1)), 1);
                    }
                    else
                    {
                        vars[0] = "Store item to";
                        vars[1] = Model.ItemNames.NumerizeUnsorted(Bits.GetShort(asc.CommandData, 1), 1);
                    }
                    break;
                case 0xBE: vars[0] = Bits.GetShort(asc.CommandData, 1).ToString(); break;
                case 0xB6:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0x86:
                    ushort value = Bits.GetShort(asc.CommandData, 1);
                    if (value == 1)
                        vars[0] = "screen";
                    else if (value == 2)
                        vars[0] = "objects";
                    else if (value == 4)
                        vars[0] = "screen + all objects";
                    vars[1] = asc.Param4.ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 5).ToString();
                    break;
                case 0x8E:
                case 0x8F:
                    switch (asc.Param1 & 0x07)
                    {
                        case 0x00: vars[0] = "{none}"; break;
                        case 0x01: vars[0] = "red"; break;
                        case 0x02: vars[0] = "green"; break;
                        case 0x03: vars[0] = "yellow"; break;
                        case 0x04: vars[0] = "blue"; break;
                        case 0x05: vars[0] = "pink"; break;
                        case 0x06: vars[0] = "aqua"; break;
                        case 0x07: vars[0] = "white"; break;
                    }
                    if (asc.Opcode == 0x8E)
                        vars[1] = asc.Param2.ToString();
                    break;
                case 0xE1:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString();
                    vars[1] = asc.Param3.ToString();
                    break;
                case 0xA3: vars[0] = ScreenEffects[asc.Param1]; break;
                case 0xBF: vars[0] = Lists.TargetNames[asc.Param1]; break;
                case 0xD5:
                    vars[0] = Model.MonsterNames.NumerizeUnsorted(asc.Param2);
                    vars[1] = asc.Param3.ToString();
                    break;
              //Timing
                case 0xCE:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    vars[2] = asc.Param3.ToString();
                    vars[3] = asc.Param4.ToString();
                    vars[4] = asc.Param5.ToString();
                    vars[5] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 6).ToString("X4");
                    break;
                case 0xCF:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    vars[2] = asc.Param3.ToString();
                    vars[3] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 4).ToString("X4");
                    break;
                case 0xD0:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0xD2:
                    vars[0] = asc.Param1.ToString();
                    break;
                case 0xD3:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    vars[2] = asc.Param3.ToString();
                    break;
                case 0xD4:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    vars[2] = asc.Param3.ToString();
                    vars[3] = asc.Param4.ToString();
                    vars[4] = asc.Param5.ToString();
                    break;
                case 0xD8:
                    vars[0] = ((asc.InternalOffset & 0xFF0000) >> 16).ToString("X2") +
                        Bits.GetShort(asc.CommandData, 1).ToString("X4");
                    break;
                default:
                    break;
            }
            string command = AnimationCommands[asc.Opcode];
            if (command == "")
                command = "{{" + BitConverter.ToString(asc.CommandData) + "}}";
            return string.Format(command, vars);
        }
        private string GetBits(int src, string[] names, int length)
        {
            string bits = "";
            string[] bit = new string[length];
            bool pre = false;
            for (int b = 1, i = 0; i < length; b *= 2, i++)
            {
                if ((src & b) == b)
                {
                    if (pre && i > 0)
                        bit[i] = ",";
                    if (names != null)
                        bit[i] += names[i];
                    else
                        bit[i] += i;
                    pre = true;
                }
                else bit[i] = "";
            }
            for (int k = 0; k < bit.Length; k++)
                bits += bit[k];
            return bits;
        }
    }
}
