using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    public class AnimationScript
    {
        // universal variables
        private int type; public int Type { get { return this.type; } }
        private int index; public int Index { get { return this.index; } set { this.index = value; } }
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables
        private List<AnimationCommand> commands;
        public List<AnimationCommand> Commands
        {
            get
            {
                if (this.commands == null)
                    this.commands = new List<AnimationCommand>();
                return this.commands;
            }
            set { this.commands = value; }
        }
        public byte amem = 0; public byte AMEM { get { return this.amem; } set { this.amem = value; } }
        private int baseOffset; public int BaseOffset { get { return this.baseOffset; } set { this.baseOffset = value; } }
        // constant variables
        private readonly int[] behaviorOffsets = new int[]
        {
            0x3505C6,0x3505DA,0x350635,0x350669,0x3506A7,0x350737,0x350790,0x350796,
            0x3507A2,0x3507E9,0x350830,0x35086A,0x3508A4,0x3508BA,0x350916,0x35091C,
            0x350928,0x35096F,0x35099D,0x35099F,0x3509D5,0x350A38,0x350A3E,0x350A55,
            0x350A9C,0x350ABD,0x350AF7,0x350B2D,0x350BB7,0x350BF3,0x350BF9,0x350BFD,
            0x350C14,0x350C5B,0x350C9E,0x350CDC,0x350D22,0x350D36,0x350D72,0x350D9D,
            0x350DA3,0x350DAF,0x350DED,0x350E38,0x350E4A,0x350E84,0x350E98,0x350EEE,
            0x350F1A,0x350F44,0x350F4A,0x350F56,0x350F6B,0x350F7A
        };
        private readonly int[] characterOffsets = new int[] // character weapon scripts that play when character draws near monsters to attack
        {
            0x358916,0x3589D5,0x358A6C,0x358B57,0x358BEC 
        };
        // constructor
        public AnimationScript(int index, int type)
        {
            this.index = index;
            this.type = type;
            Disassemble();
        }
        // disassembler
        private void Disassemble()
        {
            int bank = 0x020000, start = 0;
            switch (type)
            {
                case 1: start = 0x1026; bank = 0x350000; break;
                case 2: start = 0x2128; bank = 0x350000; break;
                case 3: start = 0x1493; bank = 0x350000; break;
                case 4: start = 0xC761; bank = 0x350000; break;
                case 5: start = 0xC992; bank = 0x350000; break;
                case 6: start = 0xECA2; bank = 0x350000; break;
                case 7: start = 0x816D; bank = 0x350000; break;
                case 8: start = 0x8271; bank = 0x350000; break;
                case 9: start = 0x6004; bank = 0x3A0000; break;
                case 10: start = 0xF455; bank = 0x020000; break;
                case 11: start = 0xF4BD; bank = 0x020000; break;

                    // Be sure to add 0x0402 for additional editable script behaviors

            }
            if (type == 0)
                baseOffset = behaviorOffsets[index];

            else if (type == 12)
                baseOffset = characterOffsets[index];
            else
                baseOffset = bank + Bits.GetShort(rom, bank + start + index * 2);
            ParseScript(rom);
        }
        // functions
        private void ParseScript(byte[] rom)
        {
            int offset = baseOffset, length = 0;
            AnimationCommand temp;
            if (type == 9)
            {
                offset = baseOffset + 2;
                if (index == 22) offset = baseOffset + 4;
                if (index == 70) offset = baseOffset + 6;
                if (index == 85) offset = baseOffset + 6;
            }
            if (this.commands == null)
                this.commands = new List<AnimationCommand>();
            while (offset < rom.Length)
            {
                if (offset == 0x3A6BA1)   // another annoying rare case
                    break;
                //
                length = GetOpcodeLength(rom, offset);
                temp = new AnimationCommand(Bits.GetBytes(rom, offset, length), offset, this, null);
                // memory modifiying commands
                switch (temp.Opcode)
                {
                    case 0x20:
                    case 0x21: if ((temp.Param1 & 0x0F) == 0) amem = temp.CommandData[2]; break;
                    case 0x2C:
                    case 0x2D: if ((temp.Param1 & 0x0F) == 0) amem += temp.CommandData[2]; break;
                    case 0x2E:
                    case 0x2F: if ((temp.Param1 & 0x0F) == 0) amem -= temp.CommandData[2]; break;
                    case 0x30:
                    case 0x31: if ((temp.Param1 & 0x0F) == 0) amem++; break;
                    case 0x32:
                    case 0x33: if ((temp.Param1 & 0x0F) == 0) amem--; break;
                    case 0x34:
                    case 0x35: if ((temp.Param1 & 0x0F) == 0) amem = 0; break;
                    case 0x6A:
                    case 0x6B: if ((temp.Param1 & 0x0F) == 0) amem = (byte)(temp.CommandData[2] - 1); break;
                }
                commands.Add(temp);
                // termination commands
                if (rom[offset] == 0x07 || // end animation packet
                    rom[offset] == 0x09 || // jump directly to address (thus ending this)
                    rom[offset] == 0x11 || // end subroutine
                    rom[offset] == 0x5E)   // end sprite subroutine
                    break;
                //
                offset += length;
            }
        }
        public void RefreshScript()
        {
            this.commands = null;
            Disassemble();
        }
        public int GetOpcodeLength(byte[] rom, int offset)
        {
            byte param;
            byte opcode = rom[offset];
            if (rom.Length - offset > 1)
                param = rom[offset + 1];
            else
                param = 0;
            return A_ScriptEnums.GetCommandLength(opcode, param);
        }
        public void Add(AnimationCommand command)
        {
            commands.Add(command);
        }
    }
}
