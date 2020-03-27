using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public class AnimationCommand
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables
        private byte[] commandData;
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        private AnimationScript script;
        private AnimationCommand parent; public AnimationCommand Parent { get { return parent; } }
        private List<AnimationCommand> commands = new List<AnimationCommand>();
        public List<AnimationCommand> Commands { get { return this.commands; } set { this.commands = value; } }
        // accessors
        public byte Opcode
        {
            get
            {
                if (this.commandData.Length > 0)
                    return this.commandData[0];
                else
                    return 0;
            }
            set { this.commandData[0] = value; }
        }
        public byte Param1
        {
            get
            {
                if (this.commandData.Length > 0)
                    return this.commandData[1];
                else
                    return 0;
            }
            set { this.commandData[1] = value; }
        }
        public byte Param2
        {
            get
            {
                if (this.commandData.Length > 1)
                    return this.commandData[2];
                else
                    return 0;
            }
            set { this.commandData[2] = value; }
        }
        public byte Param3
        {
            get
            {
                if (this.commandData.Length > 2)
                    return this.commandData[3];
                else
                    return 0;
            }
            set { this.commandData[3] = value; }
        }
        public byte Param4
        {
            get
            {
                if (this.commandData.Length > 3)
                    return this.commandData[4];
                else
                    return 0;
            }
            set { this.commandData[4] = value; }
        }
        public byte Param5
        {
            get
            {
                if (this.commandData.Length > 4)
                    return this.commandData[5];
                else
                    return 0;
            }
            set { this.commandData[5] = value; }
        }
        public byte Param6
        {
            get
            {
                if (this.commandData.Length > 5)
                    return this.commandData[6];
                else
                    return 0;
            }
            set { this.commandData[6] = value; }
        }
        public byte Param7
        {
            get
            {
                if (this.commandData.Length > 6)
                    return this.commandData[7];
                else
                    return 0;
            }
            set { this.commandData[7] = value; }
        }
        public byte Param8
        {
            get
            {
                if (this.commandData.Length > 7)
                    return this.commandData[8];
                else
                    return 0;
            }
            set { this.commandData[8] = value; }
        }
        public byte Param9
        {
            get
            {
                if (this.commandData.Length > 8)
                    return this.commandData[9];
                else
                    return 0;
            }
            set { this.commandData[9] = value; }
        }
        public byte Param10
        {
            get
            {
                if (this.commandData.Length > 9)
                    return this.commandData[10];
                else
                    return 0;
            }
            set { this.commandData[10] = value; }
        }
        public byte Param11
        {
            get
            {
                if (this.commandData.Length > 10)
                    return this.commandData[11];
                else
                    return 0;
            }
            set { this.commandData[11] = value; }
        }
        public byte Param12
        {
            get
            {
                if (this.commandData.Length > 11)
                    return this.commandData[12];
                else
                    return 0;
            }
            set { this.commandData[12] = value; }
        }
        public int Length
        {
            get
            {
                if (commandData != null)
                    return commandData.Length;
                else
                    return 0;
            }
        }
        public AnimationCommand NextCommand
        {
            get
            {
                List<AnimationCommand> commandList;
                if (parent == null)
                    commandList = script.Commands;
                else
                    commandList = parent.Commands;
                int index = commandList.IndexOf(this);
                if (index >= 0 && index + 1 < commandList.Count - 1)
                    return commandList[index + 1];
                return null;
            }
        }
        public AnimationCommand PrevCommand
        {
            get
            {
                List<AnimationCommand> commandList;
                if (parent == null)
                    commandList = script.Commands;
                else
                    commandList = parent.Commands;
                int index = commandList.IndexOf(this);
                if (index > 0)
                    return commandList[index - 1];
                return null;
            }
        }
        public int Index
        {
            get
            {
                List<AnimationCommand> commandList;
                if (parent == null)
                    commandList = script.Commands;
                else
                    commandList = parent.Commands;
                return commandList.IndexOf(this);
            }
        }
        // used to link up pointers outside of current script
        protected int offset; public int Offset { get { return this.offset; } set { this.offset = value; } }
        // used to link up pointers inside of current script
        protected int originalOffset; public int OriginalOffset { get { return this.originalOffset; } set { this.originalOffset = value; } }
        // used for updating internal offsets and pointers
        protected int internalOffset; public int InternalOffset { get { return this.internalOffset; } set { this.internalOffset = value; } }
        // constructor
        public AnimationCommand(byte[] commandData, int offset, AnimationScript script, AnimationCommand parent)
        {
            this.commandData = commandData;
            this.parent = parent;
            this.offset = offset;
            this.originalOffset = offset;
            this.internalOffset = offset;
            this.script = script;     // for reading the "memory" variable
            //if (Opcode == 0x72 &&
            //    (Param1 & 0x01) == 0x01 &&
            //    (Param1 & 0x04) == 0x04)
            //    MessageBox.Show(script.Type + "," + script.Index);
            //
            int search = 0;
            switch (Opcode)
            {
                case 0xA3:
                    break;
                case 0x09:
                    search = (offset & 0xFF0000) + Bits.GetShort(commandData, 1);
                    if (parent == null && !ContainsOffset(script, search))
                        Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 1));
                    else if (parent != null && !parent.ContainsOffset(parent, search))
                        Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 1));
                    break;
                case 0x10:
                case 0x64:
                case 0x68:
                    if (offset == 0x3562C8 ||
                        offset == 0x3564FC)
                        script.AMEM = 0;
                    if ((offset & 0xFF0000) == 0x350000 && Bits.GetShort(commandData, 1) == 0x8499)
                        script.AMEM = 0;
                    if (offset == 0x3AA5EE)
                        script.AMEM = 12; // it's the only one that has enough pointers
                    Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 1));
                    break;
                case 0x5D:
                    Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 3));
                    break;
                case 0x20:
                case 0x21: if ((Param1 & 0x0F) == 0) script.AMEM = Param2; break;
                case 0x2C:
                case 0x2D: if ((Param1 & 0x0F) == 0) script.AMEM += Param2; break;
                case 0x2E:
                case 0x2F: if ((Param1 & 0x0F) == 0) script.AMEM -= Param2; break;
                case 0x30:
                case 0x31: if ((Param1 & 0x0F) == 0) script.AMEM++; break;
                case 0x32:
                case 0x33: if ((Param1 & 0x0F) == 0) script.AMEM--; break;
                case 0x34:
                case 0x35: if ((Param1 & 0x0F) == 0) script.AMEM = 0; break;
                case 0x6A:
                case 0x6B: if ((Param1 & 0x0F) == 0) script.AMEM = (byte)(Param2 - 1); break;
                default:
                    if (Opcode >= 0x24 && Opcode <= 0x2B)
                    {
                        search = (offset & 0xFF0000) + Bits.GetShort(commandData, 4);
                        if (parent == null && !ContainsOffset(script, search))
                            Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 4));
                        else if (parent != null && !parent.ContainsOffset(parent, search))
                            Disassemble((offset & 0xFF0000) + Bits.GetShort(commandData, 4));
                    }
                    break;
            }
        }
        // disassembler
        private void Disassemble(int offset)
        {
            int length = 0;
            AnimationCommand temp;
            switch (Opcode)
            {
                case 0x09:
                    while ((offset & 0xFFFF) < 0xFFFF)
                    {
                        // these are unusual cases, seems this is the only way
                        if (offset == 0x356076) break;
                        if (offset == 0x356087) break;
                        if (offset == 0x3560A9) break;
                        if (offset == 0x3560CD) break;
                        if (offset == 0x3560FE) break;
                        if (offset == 0x356131) break;
                        if (offset == 0x356152) break;
                        if (offset == 0x35617A) break;
                        if (offset == 0x3561AD) break;
                        if (offset == 0x3561E0) break;
                        if (offset == 0x356213) break;
                        if (offset == 0x35624B) break;
                        if (offset == 0x3A8A68) break;
                        if (offset == 0x3A8AC0) break;
                        if (offset == 0x3A8C8A) break;
                        if ((offset & 0xFF0000) == 0x3A0000 && offset < 0x3A60D0)
                            break;
                        length = GetOpcodeLength(rom, offset);
                        temp = new AnimationCommand(Bits.GetBytes(rom, offset, length), offset, script, this);
                        commands.Add(temp);
                        if (rom[offset] == 0x07 || // end animation packet
                            rom[offset] == 0x09 || // jump directly to address (thus ending this)
                            rom[offset] == 0x11 || // end subroutine
                            rom[offset] == 0x5E)   // end sprite subroutine
                            break;
                        offset += length;
                    }
                    break;
                case 0x10: goto case 0x09;
                case 0x5D: goto case 0x09;
                case 0x64:
                    if (script.AMEM > 0x10)
                    {
                        script.AMEM = 0;
                        offset = (offset & 0xFF0000) + Bits.GetShort(rom, offset);
                    }
                    else
                        offset = (offset & 0xFF0000) + Bits.GetShort(rom, offset + (script.AMEM * 2));
                    goto case 0x09;
                case 0x68:
                    if (script.AMEM >= 0x40)
                    {
                        script.AMEM = 0;
                        offset = (offset & 0xFF0000) + Bits.GetShort(rom, offset);
                    }
                    else
                        offset = (offset & 0xFF0000) + Bits.GetShort(rom, offset + (script.AMEM * 2));
                    //
                    if (offset == 0x356919 ||
                        offset == 0x356969)
                        offset += 2;
                    else
                        offset = (offset & 0xFF0000) + Bits.GetShort(rom, offset + commandData[3]);
                    goto case 0x09;
                default:
                    if (Opcode >= 0x24 && Opcode <= 0x2B)
                    {
                        offset = (offset & 0xFF0000) + Bits.GetShort(commandData, 4);
                        goto case 0x09;
                    }
                    break;
            }
        }
        // functions
        public int GetOpcodeLength(byte[] data, int offset)
        {
            byte opcode, option;
            int len;
            opcode = data[offset];
            if (data.Length - offset > 1)
                option = data[offset + 1];
            else
                option = 0;
            len = A_ScriptEnums.GetCommandLength(opcode, option);
            return len;
        }
        public bool ContainsOffset(AnimationCommand parent, int offset)
        {
            bool found = false;
            foreach (AnimationCommand child in parent.Commands)
            {
                if (child.InternalOffset == offset)
                    return true;
            }
            if (parent.Parent != null)
                found = ContainsOffset(parent.Parent, offset);
            return found;
        }
        public bool ContainsOffset(AnimationScript script, int offset)
        {
            bool found = false;
            foreach (AnimationCommand asc in script.Commands)
            {
                if (asc.InternalOffset == offset)
                    return true;
            }
            return found;
        }
        /// <summary>
        /// Returns the available space or final index for creating a new command, starting from this command.
        /// </summary>
        /// <param name="command">The first command to be replaced.</param>
        /// <param name="needed">The length of the new command (ie. the required space).</param>
        /// <param name="getIndex">Returns either the available space or the index of the last command to be replaced.</param>
        /// <returns></returns>
        public int AvailableSpace(int needed, bool getIndex)
        {
            int finalIndex = this.Index;
            int available = this.Length;
            AnimationCommand temp = this;
            while (
                needed > available && 
                temp.NextCommand != null &&
                temp.NextCommand.Opcode != 0x07 &&
                temp.NextCommand.Opcode != 0x11 &&
                temp.NextCommand.Opcode != 0x5E)
            {
                temp = temp.NextCommand;
                finalIndex++;
                available += temp.Length;
            }
            return getIndex ? finalIndex : available;
        }
        public override string ToString()
        {
            return Interpreter.Instance.InterpretCommand(this);
        }
        // spawning
        public AnimationCommand Copy()
        {
            return new AnimationCommand(Bits.Copy(commandData), this.offset, this.script, this.parent);
        }
    }
}
