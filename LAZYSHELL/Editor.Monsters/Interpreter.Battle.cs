using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        private static string[] BattleCommands = new string[]
        {
            "","","","","","","","","","","","","","","","", // 0x00-0x0F
            "","","","","","","","","","","","","","","","", // 0x10-0x1F
            "","","","","","","","","","","","","","","","", // 0x20-0x2F
            "","","","","","","","","","","","","","","","", // 0x30-0x3F
            "","","","","","","","","","","","","","","","", // 0x40-0x4F
            "","","","","","","","","","","","","","","","", // 0x50-0x5F
            "","","","","","","","","","","","","","","","", // 0x60-0x6F
            "","","","","","","","","","","","","","","","", // 0x70-0x7F
            "","","","","","","","","","","","","","","","", // 0x80-0x8F
            "","","","","","","","","","","","","","","","", // 0x90-0x9F
            "","","","","","","","","","","","","","","","", // 0xA0-0xAF
            "","","","","","","","","","","","","","","","", // 0xB0-0xBF
            "","","","","","","","","","","","","","","","", // 0xC0-0xCF
            "","","","","","","","","","","","","","","","", // 0xD0-0xDF
            "Do 1 of 3 possible attacks: {0}  /  {1}  /  {2}",			// 0xE0
            "",			// 0xE1
            "Set target: {0}",			// 0xE2
            "Run battle dialogue: {0}",			// 0xE3
            "",			// 0xE4
            "Run battle event: {0}",			// 0xE5
            "{0} memory $7EE00{1}",			// 0xE6
            "{0} memory $7EE00{1}, bit(s) {2}",			// 0xE7
            "Clear memory $7EE00{0}",			// 0xE8
            "",			// 0xE9
            "{0} target: {1}",			// 0xEA
            "{0} invincibility for target: {1}",			// 0xEB
            "Exit Battle",			// 0xEC
            "Memory $7EE005 = random # less than {0}",			// 0xED
            "",			// 0xEE
            "Do 1 spell: {0}",			// 0xEF
			
            "Do 1 of 3 possible spells: {0}  /  {1}  /  {2}",			// 0xF0
            "Run object sequence: {0}",			// 0xF1
            "{0} target: {1}",			// 0xF2
            "{0} command(s): {1}",			// 0xF3
            "{0} item inventory",			// 0xF4
            "",			// 0xF5
            "",			// 0xF6
            "",			// 0xF7
            "",			// 0xF8
            "",			// 0xF9
            "",			// 0xFA
            "Do Nothing",			// 0xFB
            "",			// 0xFC
            "Wait 1 turn",			// 0xFD
            "Wait 1 turn, restart script",			// 0xFE
            "Counter commands"		    // 0xFF
        };
        private string[] BattleParams = new string[]
        {
            "",			// 0x00
            "If attacked by command: {0}",			// 0x01
            "If attacked by spell: {0}",			// 0x02
            "If attacked by item: {0}",			// 0x03
            "If attacked by element: {0}",			// 0x04
            "If attacked",			// 0x05
            "If target: {0}, HP is below: {1}",			// 0x06
            "If HP is below: {0}",			// 0x07
            "If target: {0}, is affected by status: {1}",			// 0x08
            "If target: {0}, is NOT affected by status: {1}",			// 0x09
            "If attack phase counter (7EE006) = {0}",			// 0x0A
            "",			// 0x0B
            "If memory $7EE00{0} < {1}",			// 0x0C
            "If memory $7EE00{0} >= {1}",			// 0x0D
            "",			// 0x0E
            "",			// 0x0F
			
            "If target {0}: {1}",			// 0x10
            "If memory $7EE00{0}, bit(s) set: {1}",			// 0x11
            "If memory $7EE00{0}, bit(s) clear: {1}",			// 0x12
            "If in formation: {0}",			// 0x13
            "If only one alive",			// 0x14
            "",			// 0x15
            "",			// 0x16
            "",			// 0x17
            "",			// 0x18
            "",			// 0x19
            "",			// 0x1A
            "",			// 0x1B
            "",			// 0x1C
            "",			// 0x1D
            "",			// 0x1E
            ""		    // 0x1F
        };
        private string[] CommandNames = new string[]
        {
            "","","Attack","Special","Items"
        };
        private string[] ElementNames = new string[]
        {
            "Ice","Thunder","Fire","Jump"
        };
        private string[] EffectNames = new string[]
        {
            "Mute","Sleep","Poison","Fear",
            "","Mushroom","Scarecrow","Invincible"
        };
        public string InterpretCommand(BattleCommand bsc)
        {
            string[] vars = new string[16];
            string command = BattleCommands[bsc.Opcode];
            switch (bsc.Opcode)
            {
                case 0xE0:
                    vars[0] = bsc.Param1 == 0xFB ? "{Do nothing}" : Model.AttackNames.NumerizeUnsorted(bsc.Param1, Lists.Keystrokes);
                    vars[1] = bsc.Param2 == 0xFB ? "{Do nothing}" : Model.AttackNames.NumerizeUnsorted(bsc.Param2, Lists.Keystrokes);
                    vars[2] = bsc.Param3 == 0xFB ? "{Do nothing}" : Model.AttackNames.NumerizeUnsorted(bsc.Param3, Lists.Keystrokes);
                    break;
                case 0xE2:
                    vars[0] = Lists.TargetNames[bsc.Param1];
                    break;
                case 0xE3:
                    vars[0] = "{" + bsc.Param1.ToString("d3") + "}  \"" +
                        Model.BattleDialogues[bsc.Param1].GetStub() + "\"";
                    break;
                case 0xE5:
                    vars[0] = Lists.Numerize(Lists.BattleEventNames, bsc.Param1);
                    break;
                case 0xE6:
                    vars[0] = bsc.Param1 == 0 ? "Increment" : "Decrement";
                    vars[1] = bsc.Param2.ToString("X1");
                    break;
                case 0xE7:
                    vars[0] = bsc.Param1 == 0 ? "Set" : "Clear";
                    vars[1] = bsc.Param2.ToString();
                    vars[2] = GetBits(bsc.Param3, BitNames, 8);
                    break;
                case 0xE8:
                    vars[0] = bsc.Param1.ToString("X1");
                    break;
                case 0xEA:
                    vars[0] = bsc.Param1 == 0 ? "Remove" : "Call";
                    vars[1] = Lists.TargetNames[bsc.Param3];
                    break;
                case 0xEB:
                    vars[0] = bsc.Param1 == 0 ? "Set" : "Null";
                    vars[1] = Lists.TargetNames[bsc.Param2];
                    break;
                case 0xED:
                    vars[0] = bsc.Param1.ToString();
                    break;
                case 0xEF:
                    vars[0] = bsc.Param1 == 0xFB ? "{Do nothing}" : Model.SpellNames.NumerizeUnsorted(bsc.Param1, 1);
                    break;
                case 0xF0:
                    vars[0] = bsc.Param1 == 0xFB ? "{Do nothing}" : Model.SpellNames.NumerizeUnsorted(bsc.Param1, 1);
                    vars[1] = bsc.Param2 == 0xFB ? "{Do nothing}" : Model.SpellNames.NumerizeUnsorted(bsc.Param2, 1);
                    vars[2] = bsc.Param3 == 0xFB ? "{Do nothing}" : Model.SpellNames.NumerizeUnsorted(bsc.Param3, 1);
                    break;
                case 0xF1:
                    vars[0] = bsc.Param1.ToString();
                    break;
                case 0xF2:
                    vars[0] = bsc.Param1 == 0 ? "Disable" : "Enable";
                    vars[1] = bsc.Param2 == 0 ? "self" : "monster " + bsc.Param2;
                    break;
                case 0xF3:
                    vars[0] = bsc.Param1 == 0 ? "Enable" : "Disable";
                    vars[1] = GetBits(bsc.Param2, new string[] { "Attack", "Special", "Item" }, 3);
                    break;
                case 0xF4:
                    vars[0] = bsc.Param2 == 0 ? "Clear" : "Restore";
                    break;
                case 0xFB:
                    return "{Do nothing}";
                case 0xFC:
                    command = BattleParams[bsc.Param1];
                    switch (bsc.Param1)
                    {
                        case 0x01: vars[0] = CommandNames[bsc.Param2]; break;
                        case 0x02:
                            vars[0] = bsc.Param2 == 0xFB ? "{Do nothing}" : Model.SpellNames.NumerizeUnsorted(bsc.Param2, 1);
                            break;
                        case 0x03:
                            vars[0] = bsc.Param2 == 0xFB ? "{Do nothing}" : Model.ItemNames.NumerizeUnsorted(bsc.Param2, 1);
                            break;
                        case 0x04:
                            vars[0] = GetBits((byte)(bsc.Param2 >> 4), ElementNames, 4);
                            break;
                        case 0x06:
                            vars[0] = Lists.TargetNames[bsc.Param2];
                            vars[1] = (bsc.Param3 * 16).ToString();
                            break;
                        case 0x07:
                            vars[0] = (bsc.Param3 * 256 + bsc.Param2).ToString();
                            break;
                        case 0x08:
                        case 0x09:
                            vars[0] = Lists.TargetNames[bsc.Param2];
                            vars[1] = GetBits(bsc.Param3, EffectNames, 8);
                            break;
                        case 0x0A: vars[0] = bsc.Param2.ToString(); break;
                        case 0x0C:
                        case 0x0D:
                            vars[0] = bsc.Param2.ToString("X1");
                            vars[1] = bsc.Param3.ToString();
                            break;
                        case 0x10:
                            vars[0] = bsc.Param2 == 0 ? "alive" : "dead";
                            vars[1] = Lists.TargetNames[bsc.Param3];
                            break;
                        case 0x11:
                        case 0x12:
                            vars[0] = bsc.Param2.ToString("X1");
                            vars[1] = GetBits(bsc.Param3, BitNames, 8);
                            break;
                        case 0x13:
                            vars[0] = (bsc.Param3 * 256 + bsc.Param2).ToString();
                            break;
                    }
                    break;
                default:
                    if (bsc.Opcode < 0xE0)
                        return "Do 1 attack: " + Model.AttackNames.NumerizeUnsorted(bsc.Opcode, Lists.Keystrokes);
                    break;
            }
            if (command == "")
                command = "{{" + BitConverter.ToString(bsc.CommandData) + "}}";
            return string.Format(command, vars);
        }
        #endregion
    }
}
