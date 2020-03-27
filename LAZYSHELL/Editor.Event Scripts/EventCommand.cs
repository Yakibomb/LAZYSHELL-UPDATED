using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    [Serializable()]
    public class EventCommand : EventActionCommand
    {
        // class variables
        private byte[] commandData;
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        private ActionScript queue; public ActionScript Queue { get { return this.queue; } set { this.queue = value; } }
        private bool modified; public bool Modified { get { return this.modified; } set { this.modified = value; } }
        public int Length { get { return this.commandData.Length; } }
        public bool QueueTrigger
        {
            get
            {
                return (
                    commandData[0] >= 0 &&
                    commandData[0] <= 0x2F &&
                    commandData[1] <= 0xF1);
            }
        }
        // accessors
        protected override byte GetOpcode()
        {
            if (this.commandData.Length > 0)
                return this.commandData[0];
            else
                return 0;
        }
        protected override void SetOpcode(byte opcode)
        {
            this.commandData[0] = opcode;
        }
        protected override byte GetParam(int index)
        {
            if (this.commandData.Length > 1)
                return this.commandData[index];
            else
                return 0;
        }
        protected override void SetParam(byte param, int index)
        {
            this.commandData[index] = param;
        }
        // only for indexes 3329 and 3729
        private bool locked; public bool Locked { get { return locked; } set { locked = value; } }
        // constructor
        public EventCommand(byte[] commandData, int offset)
        {
            this.commandData = commandData;
            this.offset = offset;
            this.originalOffset = offset;
            this.internalOffset = offset;
            if (commandData.Length >= 2)
            {
                if (commandData[0] >= 0 && commandData[0] <= 0x2F && commandData[1] <= 0xF1)
                {
                    if (commandData[1] == 0xF0 || commandData[1] == 0xF1)
                        queue = new ActionScript(Bits.GetBytes(commandData, 3, commandData.Length - 3), -1, offset + 3);
                    else
                        queue = new ActionScript(Bits.GetBytes(commandData, 2, commandData.Length - 2), -1, offset + 2);
                }
            }
            if (Opcode == 0xFD && Interpreter.EventCommandsFD[Param1] == "")
            {
                Model.MostCommonEventsFD[Param1].Opcode = Opcode;
                Model.MostCommonEventsFD[Param1].Param1 = Param1;
                Model.MostCommonEventsFD[Param1].Frequency++;
            }
            else if (Opcode != 0xFD && Interpreter.EventCommands[Opcode] == "")
            {
                Model.MostCommonEvents[Opcode].Opcode = Opcode;
                Model.MostCommonEvents[Opcode].Param1 = Param1;
                Model.MostCommonEvents[Opcode].Frequency++;
            }
        }
        // assemblers
        public void Assemble()
        {
            // Assembles this command back to binary data
            // Stores it in byte[]eventData
            int start;
            if (Locked)   // for events 0xD01 and 0xE91 only
            {
                start = 0;
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.CommandData.CopyTo(commandData, start);
                    start += aqc.Length;
                }
            }
            else if (QueueTrigger && queue != null)
            {
                int offset = start = Param1 < 0xF0 ? 2 : 3;
                byte a = 0, b = 0, c = 0;
                a = commandData[0];
                b = commandData[1];
                if (Param1 >= 0xF0)
                    c = commandData[2];
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.Assemble();
                    offset += aqc.Length;
                }
                commandData = new byte[offset];
                commandData[0] = a;
                commandData[1] = b;
                if (Param1 >= 0xF0)
                    commandData[2] = c;
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.CommandData.CopyTo(commandData, start);
                    start += aqc.Length;
                }
            }
        }
        // data managers
        public void RefreshOffsets(int offset)
        {
            this.offset = offset;
            Assemble(); // added 2008-12-20
            if (Locked)
            {
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.Offset = offset;
                    offset += aqc.Length;
                }
            }
            if (QueueTrigger && queue != null)
            {
                offset += commandData[1] == 0xF0 || commandData[1] == 0xF1 ? 3 : 2;
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.Offset = offset;
                    offset += aqc.Length;
                }
            }
        }
        /// <summary>
        /// Adds/subtracts a value from the command's offset and any pointers in the command that point to or after a given offset.
        /// </summary>
        /// <param name="delta">The value to add/subtract from any pointers.</param>
        /// <param name="conditionOffset">The offset to compare to.</param>
        public void UpdatePointer(int delta, int conditionOffset)
        {
            ushort pointer;
            if (this.offset >= conditionOffset || conditionOffset == 0x7FFFFFFF)
            {
                this.offset += delta;
                this.internalOffset += delta;   // 2009-01-07
            }
            if ((this.Locked || this.QueueTrigger) && this.queue != null)
                queue.UpdateOffsets(delta, conditionOffset);
            conditionOffset &= 0xFFFF; // convert to pointer
            if (commandData[0] == 0x42 || commandData[0] == 0x67 || commandData[0] == 0xE9)
            {
                pointer = ReadPointerSpecial(0);
                if (pointer >= conditionOffset)
                    WritePointerSpecial(0, (ushort)(pointer + delta));
                pointer = ReadPointerSpecial(1);
                if (pointer >= conditionOffset)
                    WritePointerSpecial(1, (ushort)(pointer + delta));
            }
            else
            {
                pointer = ReadPointer();
                if (pointer >= conditionOffset)
                    WritePointer((ushort)(pointer + delta));
            }
        }
        public override ushort ReadPointer()
        {
            switch (commandData[0])
            {
                case 0x3D:  // 1
                case 0x41:
                case 0x5C:
                case 0x66:
                case 0xD2:
                case 0xD3:
                case 0xDB:
                case 0xDF:
                case 0xE8:
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    return Bits.GetShort(commandData, 1);
                case 0x32:  // 2
                case 0x39:
                case 0x3F:
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    return Bits.GetShort(commandData, 2);
                case 0x3E:  // 3
                case 0xE0:
                case 0xE1:
                case 0xE2:
                case 0xE3:
                case 0xE6:
                case 0xE7:
                case 0xF8:
                    return Bits.GetShort(commandData, 3);
                case 0x3C:  // 4
                case 0xE4:
                case 0xE5:
                    return Bits.GetShort(commandData, 4);
                case 0x3A:  // 5
                case 0x3B:
                    return Bits.GetShort(commandData, 5);
                case 0x42:  // 1,3
                case 0x67:
                case 0xE9:
                    throw new Exception("E9");
                case 0xFD:
                    switch (commandData[1])
                    {
                        case 0x3F:  // 2
                        case 0x62:
                            return Bits.GetShort(commandData, 2);
                        case 0x33:  // 3
                        case 0x34:
                        case 0x3D:
                        case 0x96:
                        case 0x97:
                            return Bits.GetShort(commandData, 3);
                        case 0xF0:  // 4
                            return Bits.GetShort(commandData, 4);
                        case 0x3E:  // 5
                            return Bits.GetShort(commandData, 5);
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }
        }
        public override ushort ReadPointerSpecial(int index)
        {
            if (commandData[0] != 0xE9 && commandData[0] != 0x42 && commandData[0] != 0x67)
                throw new Exception("Not Command 0xE9, 0x42 or 0x67");
            return Bits.GetShort(commandData, index * 2 + 1);
        }
        public override void WritePointer(ushort pointer)
        {
            switch (commandData[0])
            {
                case 0x3D:  // 1
                case 0x41:
                case 0x5C:
                case 0x66:
                case 0xD2:
                case 0xD3:
                case 0xDB:
                case 0xDF:
                case 0xE8:
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    Bits.SetShort(commandData, 1, pointer); break;
                case 0x32:  // 2
                case 0x39:
                case 0x3F:
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    Bits.SetShort(commandData, 2, pointer); break;
                case 0x3E:  // 3
                case 0xE0:
                case 0xE1:
                case 0xE2:
                case 0xE3:
                case 0xE6:
                case 0xE7:
                case 0xF8:
                    Bits.SetShort(commandData, 3, pointer); break;
                case 0x3C:  // 4
                case 0xE4:
                case 0xE5:
                    Bits.SetShort(commandData, 4, pointer); break;
                case 0x3A:  // 5
                case 0x3B:
                    Bits.SetShort(commandData, 5, pointer); break;
                case 0x42:  // 1,3
                case 0x67:
                case 0xE9:
                    throw new Exception("E9");
                case 0xFD:
                    switch (commandData[1])
                    {
                        case 0x3F:  // 2
                        case 0x62:
                            Bits.SetShort(commandData, 2, pointer); break;
                        case 0x33:  // 3
                        case 0x34:
                        case 0x3D:
                        case 0x96:
                        case 0x97:
                            Bits.SetShort(commandData, 3, pointer); break;
                        case 0xF0:  // 4
                            Bits.SetShort(commandData, 4, pointer); break;
                        case 0x3E:  // 5
                            Bits.SetShort(commandData, 5, pointer); break;
                        default: break;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void WritePointerSpecial(int index, ushort pointer)
        {
            if (commandData[0] != 0xE9 && commandData[0] != 0x42 && commandData[0] != 0x67)
                throw new Exception("Not Command 0xE9, 0x42 or 0x67");
            Bits.SetShort(commandData, index * 2 + 1, pointer);
        }
        public void ResetOriginalOffset()
        {
            this.originalOffset = this.offset;
            if (this.QueueTrigger && this.queue != null && this.queue.Commands != null)
            {
                foreach (ActionCommand aqc in queue.Commands)
                    aqc.ResetOriginalOffset();
            }
        }
        // spawning
        public TreeNode Node
        {
            get
            {
                TreeNode node = new TreeNode("[" + (offset).ToString("X6") + "]   " + ToString());
                if (locked)
                    node.Text = "NON-EMBEDDED ACTION QUEUE";
                if (QueueTrigger)
                    node.BackColor = Color.FromArgb(192, 224, 255);
                else if (Opcode >= 0xFE)
                    node.BackColor = Color.FromArgb(255, 255, 160);
                node.ForeColor = modified ? Color.Red : SystemColors.ControlText;
                node.Checked = modified;
                node.Tag = this;
                return node;
            }
        }
        public EventCommand Copy()
        {
            return new EventCommand(Bits.Copy(commandData), this.offset);
        }
        // universal functions
        public override string ToString()
        {
            return Interpreter.Instance.InterpretCommand(this);
        }
    }
}
