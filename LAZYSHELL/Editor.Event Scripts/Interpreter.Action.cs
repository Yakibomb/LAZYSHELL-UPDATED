using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        private static string[] ActionCommands = new string[]
        {
            "Visibility on",			// 0x00
            "Visibility off",			// 0x01
            "Sequence playback on",			// 0x02
            "Sequence playback off",			// 0x03
            "Sequence looping on",			// 0x04
            "Sequence looping off",			// 0x05
            "Fixed F coord on",			// 0x06
            "Fixed F coord off",			// 0x07
            "{0} = {1} (sprite += {2}, {3}, {4})",			// 0x10
            "Reset properties",			// 0x09
            "Solidity bits = {0}",			// 0x0A
            "Set solidity bits {0}",			// 0x0B
            "Clear solidity bits {0}",			// 0x0C
            "Palette row = {0}",			// 0x0D
            "Palette row += {0}",			// 0x0E
            "Palette row += 1",			// 0x0F
            			
            "{0} speed = {1}",			// 0x10
            "Set object memory $0D bits {0}",			// 0x11
            "Set object memory $0B bits {0}",			// 0x12
            "VRAM priority = {0}",			// 0x13
            "Set object memory $0E bits {0}",			// 0x14
            "Set movement bits {0}",			// 0x15
            "",			// 0x16
            "",			// 0x17
            "",			// 0x18
            "",			// 0x19
            "",			// 0x1A
            "",			// 0x1B
            "",			// 0x1C
            "",			// 0x1D
            "",			// 0x1E
            "",			// 0x1F
            			
            "",			// 0x20
            "",			// 0x21
            "",			// 0x22
            "",			// 0x23
            "",			// 0x24
            "",			// 0x25
            "Embedded animation routine ($26) {0}",			// 0x26
            "Embedded animation routine ($27) {1}",			// 0x27
            "Embedded animation routine ($28) {2}",			// 0x28
            "",			// 0x29
            "",			// 0x2A
            "",			// 0x2B
            "",			// 0x2C
            "",			// 0x2D
            "",			// 0x2E
            "",			// 0x2F
            			
            "",			// 0x30
            "",			// 0x31
            "",			// 0x32
            "",			// 0x33
            "",			// 0x34
            "",			// 0x35
            "",			// 0x36
            "",			// 0x37
            "",			// 0x38
            "",			// 0x39
            "If distance of {0} is < ({1} tiles) and infinite Z coords apart jump to ${2}",			// 0x3A
            "If distance of {0} is < ({1} tiles), jump to ${2}",			// 0x3B
            "",			// 0x3C
            "If in air, jump to ${0}",			// 0x3D
            "Create new NPC ({0}) @ coords of {1} (if null, jump to ${2})",			// 0x3E
            "Create new NPC ({0}) @ coords of $7010-15 (if null, jump to ${1})",			// 0x3F
            			
            "Walk 1 step east",			// 0x40
            "Walk 1 step southeast",			// 0x41
            "Walk 1 step south",			// 0x42
            "Walk 1 step southwest",			// 0x43
            "Walk 1 step west",			// 0x44
            "Walk 1 step northwest",			// 0x45
            "Walk 1 step north",			// 0x46
            "Walk 1 step northeast",			// 0x47
            "Walk 1 step in F direction (F coord)",			// 0x48
            "",			// 0x49
            "Z coord += 1",			// 0x4A
            "Z coord -= 1",			// 0x4B
            "",			// 0x4C
            "",			// 0x4D
            "",			// 0x4E
            "",			// 0x4F
            			
            "Walk {0} steps east",			// 0x50
            "Walk {0} steps southeast",			// 0x51
            "Walk {0} steps south",			// 0x52
            "Walk {0} steps southwest",			// 0x53
            "Walk {0} steps west",			// 0x54
            "Walk {0} steps northwest",			// 0x55
            "Walk {0} steps north",			// 0x56
            "Walk {0} steps northeast",			// 0x57
            "Walk {0} steps in F direction (F coord)",			// 0x58
            "Walk 20 steps in F direction (F coord)",			// 0x59
            "Z coord += {0} steps",			// 0x5A
            "Z coord -= {0} steps",			// 0x5B
            "Z coord += 20 steps",			// 0x5C
            "Z coord -= 20 steps",			// 0x5D
            "",			// 0x5E
            "",			// 0x5F
            			
            "Walk {0} pixels east",			// 0x60
            "Walk {0} pixels southeast",			// 0x61
            "Walk {0} pixels south",			// 0x62
            "Walk {0} pixels southwest",			// 0x63
            "Walk {0} pixels west",			// 0x64
            "Walk {0} pixels northwest",			// 0x65
            "Walk {0} pixels north",			// 0x66
            "Walk {0} pixels northeast",			// 0x67
            "Walk {0} pixels in F direction",			// 0x68
            "Walk 16 pixels in F direction",			// 0x69
            "Z coord += {0} pixels",			// 0x6A
            "Z coord -= {0} pixels",			// 0x6B
            "",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
            			
            "Face east",			// 0x70
            "Face southeast",			// 0x71
            "Face south",			// 0x72
            "Face southwest",			// 0x73
            "Face west",			// 0x74
            "Face northwest",			// 0x75
            "Face north",			// 0x76
            "Face northeast",			// 0x77
            "Face Mario",			// 0x78
            "Turn clockwise 45°",			// 0x79
            "Turn in random direction",			// 0x7A
            "Turn clockwise 45° {0} times",			// 0x7B
            "Face east",			// 0x7C
            "Face southwest",			// 0x7D
            "Jump at {0} velocity",			// 0x7E
            "Jump (+SFX) at {0} velocity",			// 0x7F
            			
            "Walk to ({0},{1})",			// 0x80
            "Walk ({0},{1}) steps",			// 0x81
            "Transfer to ({0},{1})",			// 0x82
            "Transfer ({0},{1}) steps",			// 0x83
            "Transfer ({0},{1}) pixels",			// 0x84
            "",			// 0x85
            "",			// 0x86
            "Transfer to (x,y) of {0}",			// 0x87
            "Walk to (x,y) of Mem $7016-1B",			// 0x88
            "Transfer to (x,y) of Mem $7016-1B",			// 0x89
            "",			// 0x8A
            "",			// 0x8B
            "",			// 0x8C
            "",			// 0x8D
            "",			// 0x8E
            "",			// 0x8F
            			
            "Bounce to ({0},{1}), peak Z coord = {2}",			// 0x90
            "Bounce ({0},{1}) steps, peak Z coord = {2}",			// 0x91
            "Transfer to ({0},{1},{2})",			// 0x92
            "Transfer ({0},{1},{2}) steps",			// 0x93
            "Transfer ({0},{1},{2}) pixels",			// 0x94
            "Transfer to (x,y,z) of {0}",			// 0x95
            "",			// 0x96
            "",			// 0x97
            "Walk to (x,y,z) of Mem $7016-1B",			// 0x98
            "Transfer to (x,y,z) of Mem $7016-1B",			// 0x99
            "",			// 0x9A
            "Stop current sound",			// 0x9B
            "Play sound (ch.6,7): {0}",			// 0x9C
            "Play sound (ch.6,7): {0} (speaker balance = {1})",			// 0x9D
            "Fade out current sound to volume {1} (time stretch = {0})",			// 0x9E
            "",			// 0x9F
            "Set memory ${0} bit {1}",			// 0xA0
            "Set memory ${0} bit {1}",			// 0xA1
            "Set memory ${0} bit {1}",			// 0xA2
            "Set memory $704x [x @ $700C] bit {1}",			// 0xA3
            "Clear memory ${0} bit {1}",			// 0xA4
            "Clear memory ${0} bit {1}",			// 0xA5
            "Clear memory ${0} bit {1}",			// 0xA6
            "Clear memory $704x [x @ $700C] bit {1}",			// 0xA7
            "Memory ${0} = {1}",			// 0xA8
            "Memory ${0} += {1}",			// 0xA9
            "Memory ${0} += 1",			// 0xAA
            "Memory ${0} -= 1",			// 0xAB
            "Memory $700C = {0}",			// 0xAC
            "Memory $700C += ",			// 0xAD
            "Memory $700C + 1",			// 0xAE
            "Memory $700C - 1",			// 0xAF
            			
            "Memory ${0} = {1}",			// 0xB0
            "Memory ${0} += {1}",			// 0xB1
            "Memory ${0} += 1",			// 0xB2
            "Memory ${0} -= 1",			// 0xB3
            "Memory $700C = memory ${0}",			// 0xB4
            "Memory ${0} = memory $700C",			// 0xB5
            "Memory $700C = random # between 0 and {0}",			// 0xB6
            "Memory ${0} = random # between 0 and {1}",			// 0xB7
            "Memory $700C += memory ${0}",			// 0xB8
            "Memory $700C -= memory ${0}",			// 0xB9
            "Memory $700C = memory ${0}",			// 0xBA
            "Memory ${0} = memory $700C",			// 0xBB
            "Memory ${0} = memory ${1}",			// 0xBC
            "Memory ${0} <=> memory ${1}",			// 0xBD
            "",			// 0xBE
            "",			// 0xBF
            			
            "Memory compare $700C to {0}",			// 0xC0
            "Memory compare $7000 to memory ${0}",			// 0xC1
            "Memory ${0} compare {1}",			// 0xC2
            "Memory $700C = current level",			// 0xC3
            "Memory $700C = X ({1}) coord of {0}",			// 0xC4
            "Memory $700C = Y ({1}) coord of {0}",			// 0xC5
            "Memory $700C = Z ({1}) coord of {0}",			// 0xC6
            "",			// 0xC7
            "",			// 0xC8
            "",			// 0xC9
            "Memory $700C = pressed button",			// 0xCA
            "Memory $700C = tapped button",			// 0xCB
            "",			// 0xCC
            "",			// 0xCD
            "",			// 0xCE
            "",			// 0xCF
            			
            "Action script = #{0}",			// 0xD0
            "",			// 0xD1
            "Jump to ${0}",			// 0xD2
            "Jump to subroutine ${0}",			// 0xD3
            "Loop start, count = {0}",			// 0xD4
            "",			// 0xD5
            "Memory ${0} load",			// 0xD6
            "Loop end",			// 0xD7
            "If memory ${0} bit {1} is set, jump to {2}",			// 0xD8
            "If memory ${0} bit {1} is set, jump to {2}",			// 0xD9
            "If memory ${0} bit {1} is set, jump to {2}",			// 0xDA
            "If Memory $704x [x @ $700C] bit {0} set, jump to {1}",			// 0xDB
            "If memory ${0} bit {1} is clear, jump to {2}",			// 0xDC
            "If memory ${0} bit {1} is clear, jump to {2}",			// 0xDD
            "If memory ${0} bit {1} is clear, jump to {2}",			// 0xDE
            "If memory $704x [x @ $700C] bit {0} clear, jump to {1}",			// 0xDF
            			
            "If memory ${0} = {1}, jump to ${2}",			// 0xE0
            "If memory ${0} != {1}, jump to ${2}",			// 0xE1
            "If memory $700C = {0}, jump to ${1}",			// 0xE2
            "If memory $700C != {0}, jump to ${1}",			// 0xE3
            "If memory ${0} = {1}, jump to ${2}",			// 0xE4
            "If memory ${0} != {1}, jump to ${2}",			// 0xE5
            "If memory $700C all bits {0} clear, jump to {1}",			// 0xE6
            "If memory $700C any bits {0} set, jump to {1}",			// 0xE7
            "If random # between 0 and 255 > 128, jump to ${0}",			// 0xE8
            "If random # between 0 and 255 > 66, jump to ${0}",			// 0xE9
            "If loaded memory = 0, jump to ${0}",			// 0xEA
            "If loaded memory != 0, jump to ${0}",			// 0xEB
            "If comparison result is: >=, jump to ${0}",			// 0xEC
            "If comparison result is: <, jump to ${0}",			// 0xED
            "If loaded memory < 0, jump to ${0}",			// 0xEE
            "If loaded memory >= 0, jump to ${0}",			// 0xEF
            			
            "Pause script for {0} frames",			// 0xF0
            "Pause script for {0} frames",			// 0xF1
            "{0}'s presence in level {1}; is {2}",			// 0xF2
            "{0}'s event trigger in level {1}; is {2}",			// 0xF3
            "Summon object @ $70A8 to current level",			// 0xF4
            "Remove object @ $70A8 from current level",			// 0xF5
            "Enable event trigger for object @ $70A8",			// 0xF6
            "Disable event trigger for object @ $70A8",			// 0xF7
            "If {0} is present in level {1}; jump to ${2}",			// 0xF8
            "",			// 0xF9
            "",			// 0xFA
            "",			// 0xFB
            "",			// 0xFC
            "",			// 0xFD
            "Return queue",			// 0xFE
            "Return all"			// 0xFF
        };
        private static string[] ActionCommandsFD = new string[]
        {
            "Shadow {0}",			// 0x00
            "Shadow {0}",			// 0x01
            "Floating on",			// 0x02
            "Floating off",			// 0x03
            "Set object memory $0E bit 4",			// 0x04
            "Clear object memory $0E bit 4",			// 0x05
            "Set object memory $0E bit 5",			// 0x06
            "Clear object memory $0E bit 5",			// 0x07
            "Set object memory $09 bit 7",			// 0x08
            "Clear object memory $09 bit 7",			// 0x09
            "Set object memory $08 bit 4",			// 0x0A
            "Clear object memory $08 bit 3,4",			// 0x0B
            "Clear object memory $30 bit 4",			// 0x0C
            "Set object memory $30 bit 4",			// 0x0D
            "Clear object memory $09 bit 4,6, set bit 5",			// 0x0E
            "Priority = {0}",			// 0x0F
            			
            "",			// 0x10
            "",			// 0x11
            "",			// 0x12
            "",			// 0x13
            "",			// 0x14
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
            "",			// 0x1F
			
            "",			// 0x20
            "",			// 0x21
            "",			// 0x22
            "",			// 0x23
            "",			// 0x24
            "",			// 0x25
            "",			// 0x26
            "",			// 0x27
            "",			// 0x28
            "",			// 0x29
            "",			// 0x2A
            "",			// 0x2B
            "",			// 0x2C
            "",			// 0x2D
            "",			// 0x2E
            "",			// 0x2F
			
            "",			// 0x30
            "",			// 0x31
            "",			// 0x32
            "",			// 0x33
            "",			// 0x34
            "",			// 0x35
            "",			// 0x36
            "",			// 0x37
            "",			// 0x38
            "",			// 0x39
            "",			// 0x3A
            "",			// 0x3B
            "",			// 0x3C
            "",			// 0x3D
            "",			// 0x3E
            "",			// 0x3F
			
            "",			// 0x40
            "",			// 0x41
            "",			// 0x42
            "",			// 0x43
            "",			// 0x44
            "",			// 0x45
            "",			// 0x46
            "",			// 0x47
            "",			// 0x48
            "",			// 0x49
            "",			// 0x4A
            "",			// 0x4B
            "",			// 0x4C
            "",			// 0x4D
            "",			// 0x4E
            "",			// 0x4F
			
            "",			// 0x50
            "",			// 0x51
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
            "",			// 0x5D
            "",			// 0x5E
            "",			// 0x5F
			
            "",			// 0x60
            "",			// 0x61
            "",			// 0x62
            "",			// 0x63
            "",			// 0x64
            "",			// 0x65
            "",			// 0x66
            "",			// 0x67
            "",			// 0x68
            "",			// 0x69
            "",			// 0x6A
            "",			// 0x6B
            "",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
			
            "",			// 0x70
            "",			// 0x71
            "",			// 0x72
            "",			// 0x73
            "",			// 0x74
            "",			// 0x75
            "",			// 0x76
            "",			// 0x77
            "",			// 0x78
            "",			// 0x79
            "",			// 0x7A
            "",			// 0x7B
            "",			// 0x7C
            "",			// 0x7D
            "",			// 0x7E
            "",			// 0x7F
			
            "",			// 0x80
            "",			// 0x81
            "",			// 0x82
            "",			// 0x83
            "",			// 0x84
            "",			// 0x85
            "",			// 0x86
            "",			// 0x87
            "",			// 0x88
            "",			// 0x89
            "",			// 0x8A
            "",			// 0x8B
            "",			// 0x8C
            "",			// 0x8D
            "",			// 0x8E
            "",			// 0x8F
			
            "",			// 0x90
            "",			// 0x91
            "",			// 0x92
            "",			// 0x93
            "",			// 0x94
            "",			// 0x95
            "",			// 0x96
            "",			// 0x97
            "",			// 0x98
            "",			// 0x99
            "",			// 0x9A
            "",			// 0x9B
            "",			// 0x9C
            "",			// 0x9D
            "Play sound (ch.4,5): {0}",			// 0x9E
            "",			// 0x9F
			
            "",			// 0xA0
            "",			// 0xA1
            "",			// 0xA2
            "",			// 0xA3
            "",			// 0xA4
            "",			// 0xA5
            "",			// 0xA6
            "",			// 0xA7
            "",			// 0xA8
            "",			// 0xA9
            "",			// 0xAA
            "",			// 0xAB
            "",			// 0xAC
            "",			// 0xAD
            "",			// 0xAE
            "",			// 0xAF
			
            "Memory $700C &= {0}",			// 0xB0
            "Memory $700C |= {0}",			// 0xB1
            "Memory $700C ^= {0}",			// 0xB2
            "Memory $700C &= memory ${0}",			// 0xB3
            "Memory $700C |= memory ${0}",			// 0xB4
            "Memory $700C ^= memory ${0}",			// 0xB5
            "Shift memory ${0} left {1} times",			// 0xB6
            "",			// 0xB7
            "",			// 0xB8
            "",			// 0xB9
            "",			// 0xBA
            "",			// 0xBB
            "",			// 0xBC
            "",			// 0xBD
            "",			// 0xBE
            "",			// 0xBF
			
            "",			// 0xC0
            "",			// 0xC1
            "",			// 0xC2
            "",			// 0xC3
            "",			// 0xC4
            "",			// 0xC5
            "",			// 0xC6
            "",			// 0xC7
            "",			// 0xC8
            "",			// 0xC9
            "",			// 0xCA
            "",			// 0xCB
            "",			// 0xCC
            "",			// 0xCD
            "",			// 0xCE
            "",			// 0xCF
			
            "",			// 0xD0
            "",			// 0xD1
            "",			// 0xD2
            "",			// 0xD3
            "",			// 0xD4
            "",			// 0xD5
            "",			// 0xD6
            "",			// 0xD7
            "",			// 0xD8
            "",			// 0xD9
            "",			// 0xDA
            "",			// 0xDB
            "",			// 0xDC
            "",			// 0xDD
            "",			// 0xDE
            "",			// 0xDF
			
            "",			// 0xE0
            "",			// 0xE1
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
            "",			// 0xFD
            "",			// 0xFE
            ""			// 0xFF
        };
        private string[] ObjectSpeeds = new string[]
        {
            "normal",
            "fast","faster",
            "very fast", "fastest",
            "slow", "very slow", ""
        };
        #endregion
        public string InterpretCommand(ActionCommand asc)
        {
            string[] vars = new string[16];
            switch (asc.Opcode)
            {
                case 0x08:
                    vars[0] = (asc.Param1 & 0x08) == 0x08 ? "Mold" : "Sequence";
                    vars[1] = (asc.Param2 & 0x07).ToString();
                    vars[2] = (asc.Param1 & 0x03).ToString();
                    vars[3] = (asc.Param1 & 0x10) == 0x10 ? "looping off" : "looping on";
                    vars[4] = (asc.Param2 & 0x80) == 0x80 ? "mirrored" : "...";
                    break;
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x11:
                case 0x12:
                case 0x14:
                case 0x15:
                    vars[0] = GetBits(asc.Param1, BitNames, 8);
                    break;
                case 0x13:
                case 0xD4:
                    vars[0] = asc.Param1.ToString();
                    break;
                case 0x0D:
                case 0x0E:
                    vars[0] = (asc.Param1 & 0x0F).ToString();
                    break;
                case 0x10:
                    if ((asc.Param1 & 0x40) == 0x40 && (asc.Param1 & 0x80) == 0x80)
                        vars[0] = "Total";
                    else if ((asc.Param1 & 0x40) == 0x40)
                        vars[0] = "Walking";
                    else if ((asc.Param1 & 0x80) == 0x80)
                        vars[0] = "Sequence";
                    vars[1] = ObjectSpeeds[asc.Param1 & 0x07];
                    break;
                case 0x26:
                case 0x27:
                case 0x28:
                    vars[0] = BitConverter.ToString(asc.CommandData, 1);
                    break;
                case 0x3A:
                case 0x3B:
                    vars[0] = ObjectNames[asc.Param1];
                    vars[1] = asc.Param3.ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 4).ToString("X4");
                    break;
                case 0x3E:
                    vars[0] = Lists.NPCPackets[asc.Param1];
                    vars[1] = ObjectNames[asc.Param2];
                    vars[2] = Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0x3F:
                    vars[0] = Lists.NPCPackets[asc.Param1];
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString("X4");
                    break;
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x5A:
                case 0x5B:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x6A:
                case 0x6B:
                case 0x7B:
                    vars[0] = asc.Param1.ToString();
                    break;
                case 0xF0:
                    vars[0] = (asc.Param1 + 1).ToString();
                    break;
                case 0x9C:
                    vars[0] = Lists.Numerize(Lists.SoundNames, Math.Min(asc.Param1, (byte)0xA2));
                    break;
                case 0x7E:
                case 0x7F:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString();
                    break;
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                    if (asc.Opcode != 0x80 || asc.Opcode != 0x82)
                    {
                        vars[0] = ((sbyte)asc.Param1).ToString();
                        vars[1] = ((sbyte)asc.Param2).ToString();
                    }
                    else
                    {
                        vars[0] = asc.Param1.ToString();
                        vars[1] = asc.Param2.ToString();
                    }
                    break;
                case 0x87:
                    vars[0] = ObjectNames[asc.Param1];
                    break;
                case 0x88:
                case 0x89:
                    break;
                case 0x90:
                case 0x91:
                    if (asc.Opcode != 0x90)
                    {
                        vars[0] = ((sbyte)asc.Param1).ToString();
                        vars[1] = ((sbyte)asc.Param2).ToString();
                    }
                    else
                    {
                        vars[0] = asc.Param1.ToString();
                        vars[1] = asc.Param2.ToString();
                    }
                    vars[2] = asc.Param3.ToString();
                    break;
                case 0x92:
                case 0x93:
                case 0x94:
                    if (asc.Opcode != 0x92)
                    {
                        vars[0] = ((sbyte)asc.Param1).ToString();
                        vars[1] = ((sbyte)asc.Param2).ToString();
                    }
                    else
                    {
                        vars[0] = asc.Param1.ToString();
                        vars[1] = asc.Param2.ToString();
                    }
                    vars[2] = (asc.Param3 & 0x1F).ToString();
                    vars[3] = DirectionNames[((asc.Param3 & 0xE0) >> 5)];
                    break;
                case 0x95:
                    vars[0] = ObjectNames[asc.Param1];
                    break;
                case 0x98:
                case 0x99:
                    break;
                case 0x9D:
                    vars[0] = Lists.Numerize(Lists.SoundNames, asc.Param1);
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0x9E:
                    vars[0] = asc.Param1.ToString();
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0xA0:
                case 0xA1:
                case 0xA2:
                    vars[0] = (((((asc.Opcode * 0x100) + asc.Param1) - 0xA000) / 8) + 0x7040).ToString("X4");
                    vars[1] = (asc.Param1 & 0x07).ToString();
                    break;
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    vars[0] = (((((asc.Opcode * 0x100) + asc.Param1) - 0xA400) / 8) + 0x7040).ToString("X4");
                    vars[1] = (asc.Param1 & 0x07).ToString();
                    break;
                case 0xA8:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0xA9:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    vars[1] = asc.Param2.ToString();
                    break;
                case 0xAA:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    break;
                case 0xAB:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    break;
                case 0xAC:
                case 0xAD:
                case 0xC0:
                case 0xD0:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString();
                    break;
                case 0xF1:
                    vars[0] = (Bits.GetShort(asc.CommandData, 1) + 1).ToString();
                    break;
                case 0xB0:
                case 0xB1:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    break;
                case 0xB2:
                case 0xB3:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xB4:
                case 0xB5:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    break;
                case 0xB6:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString();
                    break;
                case 0xB7:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xC1:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xC2:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    break;
                case 0xBC:
                case 0xBD:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = ((asc.Param2 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    vars[0] = ObjectNames[asc.Param1 & 0x3F];
                    vars[1] = (asc.Param1 & 0x40) == 0x40 ? "isometric" : "pixel";
                    break;
                case 0xD6:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xD2:
                case 0xD3:
                case 0xDB:
                case 0xDF:
                    vars[0] = (Bits.GetShort(asc.CommandData, 1)).ToString("X4");
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                    vars[0] = (((((asc.Opcode * 0x100) + asc.Param1) - 0xD800) / 8) + 0x7040).ToString("X4");
                    vars[1] = (asc.Param1 & 0x07).ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 2).ToString("X4");
                    break;
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    vars[0] = (((((asc.Opcode * 0x100) + asc.Param1) - 0xDC00) / 8) + 0x7040).ToString("X4");
                    vars[1] = (asc.Param1 & 0x07).ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 2).ToString("X4");
                    break;
                case 0xE0:
                case 0xE1:
                    vars[0] = (asc.Param1 + 0x70A0).ToString("X4");
                    vars[1] = asc.Param2.ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0xE2:
                case 0xE3:
                    vars[0] = (Bits.GetShort(asc.CommandData, 1)).ToString();
                    vars[1] = Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0xE4:
                case 0xE5:
                    vars[0] = ((asc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(asc.CommandData, 2).ToString();
                    vars[2] = Bits.GetShort(asc.CommandData, 4).ToString("X4");
                    break;
                case 0xE6:
                case 0xE7:
                    vars[0] = GetBits(Bits.GetShort(asc.CommandData, 1), BitNames, 16);
                    vars[1] = (Bits.GetShort(asc.CommandData, 3)).ToString("X4");
                    break;
                case 0x3D:
                case 0xE8:
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString("X4");
                    break;
                case 0xE9:
                    vars[0] = Bits.GetShort(asc.CommandData, 1).ToString("X4");
                    vars[1] = Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0xF2:
                case 0xF3:
                    vars[0] = ObjectNames[((asc.Param2 >> 1) & 0x3F)];
                    vars[1] = Lists.Numerize(Lists.LevelNames, (Bits.GetShort(asc.CommandData, 1) & 0x1FF));
                    vars[2] = (asc.Param2 & 0x80) == 0x80 ? "true" : "false";
                    break;
                case 0xF8:
                    vars[0] = ObjectNames[((asc.Param2 >> 1) & 0x3F)];
                    vars[1] = Lists.Numerize(Lists.LevelNames, (Bits.GetShort(asc.CommandData, 1) & 0x1FF));
                    vars[2] = Bits.GetShort(asc.CommandData, 3).ToString("X4");
                    break;
                case 0xFD: return FD_Opcodes(asc);
                default:
                    break;
            }
            string command = ActionCommands[asc.Opcode];
            if (command == "")
                command = "{{" + BitConverter.ToString(asc.CommandData) + "}}";
            return string.Format(command, vars);
        }
        private string FD_Opcodes(ActionCommand asc)
        {
            string[] vars = new string[16];
            switch (asc.Param1)
            {
                case 0x00:
                case 0x01:
                    vars[0] = (asc.Param1 & 1) == 1 ? "on" : "off";
                    break;
                case 0x0F:
                    vars[0] = asc.Param2.ToString();
                    break;
                case 0x9E:
                    vars[0] = Lists.Numerize(Lists.SoundNames, asc.Param2);
                    break;
                case 0xB0:
                case 0xB1:
                case 0xB2:
                    vars[0] = Bits.GetShort(asc.CommandData, 2).ToString();
                    break;
                case 0xB3:
                case 0xB4:
                case 0xB5:
                    vars[0] = ((asc.Param2 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xB6:
                    vars[0] = ((asc.Param2 * 2) + 0x7000).ToString("X4");
                    vars[1] = ((asc.Param3 ^ 0xFF) + 1).ToString();
                    break;
                default:
                    break;
            }
            string command = ActionCommandsFD[asc.Param1];
            if (command == "")
                command = "{{" + BitConverter.ToString(asc.CommandData) + "}}";
            return string.Format(command, vars);
        }
    }
}