using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    [Serializable()]
    public class ActionCommand : EventActionCommand
    {
        // class variables
        private byte[] commandData;
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        private bool modified; public bool Modified { get { return this.modified; } set { this.modified = value; } }
        public int Length { get { return this.commandData.Length; } }
        private bool embedded = false;
        public bool Embedded { get { return this.embedded; } set { this.embedded = value; } }
        // accessors
        protected override byte GetOpcode()
        {
            if (this.commandData.Length > 0)
                return this.commandData[0];
            else return 0;
        }
        protected override void SetOpcode(byte opcode)
        {
            this.commandData[0] = opcode;
        }
        protected override byte GetParam(int index)
        {
            if (this.commandData.Length > 1)
                return this.commandData[index];
            else return 0;
        }
        protected override void SetParam(byte param, int index)
        {
            this.commandData[index] = param;
        }
        // constructor
        public ActionCommand(byte[] commandData, int offset)
        {
            this.commandData = commandData;
            this.offset = offset;
            this.originalOffset = offset;
            this.internalOffset = offset;
        }
        // assemblers
        public void Assemble()
        {
        }
        // data managers
        /// <summary>
        /// Adds/subtracts a value from the command's offset and any pointers in the command that point to or after a given offset.
        /// </summary>
        /// <param name="delta">The value to add/subtract from any pointers.</param>
        /// <param name="conditionOffset">The offset to compare to.</param>
        /// <param name="pushOffsetTreeWrapper">If true, updates the tree wrapper offsets.</param>
        public void UpdatePointer(int delta, int conditionOffset, bool pushOffsetTreeWrapper)
        {
            ushort pointer;
            if (pushOffsetTreeWrapper)
                if (this.offset >= conditionOffset || conditionOffset == 0x7FFFFFFF)
                {
                    this.offset += delta;
                    this.internalOffset += delta;   // 2009-01-07
                }
            conditionOffset &= 0xFFFF;
            if (Opcode == 0xE9)
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
            switch (Opcode)
            {
                case 0xE4:
                case 0xE5:
                    return Bits.GetShort(commandData, 4);
                case 0x3C:
                case 0x3E:
                case 0xE0:
                case 0xE1:
                case 0xE2:
                case 0xE3:
                case 0xE6:
                case 0xE7:
                case 0xF8:
                    return Bits.GetShort(commandData, 3);
                case 0x3A:  // 5
                case 0x3B:
                    return Bits.GetShort(commandData, 4);
                case 0x3F:
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    return Bits.GetShort(commandData, 2);
                case 0x3D:
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
                case 0xE9:
                    throw new Exception("E9");
                case 0xFD:
                    switch (commandData[1])
                    {
                        case 0x3D:
                        case 0x3F:
                            return Bits.GetShort(commandData, 3);
                        case 0x3E:
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
            if (Opcode != 0xE9)
                throw new Exception("Not Command 0xE9");
            return Bits.GetShort(commandData, index * 2 + 1);
        }
        public override void WritePointer(ushort pointer)
        {
            switch (Opcode)
            {
                case 0xE4:
                case 0xE5:
                    Bits.SetShort(commandData, 4, pointer); break;
                case 0x3C:
                case 0x3E:
                case 0xE0:
                case 0xE1:
                case 0xE2:
                case 0xE3:
                case 0xE6:
                case 0xE7:
                case 0xF8:
                    Bits.SetShort(commandData, 3, pointer); break;
                case 0x3A:  // 5
                case 0x3B:
                    Bits.SetShort(commandData, 4, pointer); break;
                case 0x3F:
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    Bits.SetShort(commandData, 2, pointer); break;
                case 0x3D:
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
                case 0xE9:
                    throw new Exception("E9");
                case 0xFD:
                    switch (commandData[1])
                    {
                        case 0x3D:
                        case 0x3F:
                            Bits.SetShort(commandData, 3, pointer); break;
                        case 0x3E:
                            Bits.SetShort(commandData, 5, pointer); break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void WritePointerSpecial(int index, ushort pointer)
        {
            if (Opcode != 0xE9)
                throw new Exception("Not Command 0xE9");
            Bits.SetShort(commandData, index * 2 + 1, pointer);
        }
        public void ResetOriginalOffset()
        {
            this.originalOffset = this.offset;
        }
        // spawning
        public TreeNode Node
        {
            get
            {
                TreeNode node = new TreeNode("[" + (offset).ToString("X6") + "]   " + ToString());
                if (Opcode >= 0xFE)
                    node.BackColor = Color.FromArgb(255, 255, 160);
                node.ForeColor = modified ? Color.Red : SystemColors.ControlText;
                node.Checked = modified;
                node.Tag = this;
                return node;
            }
        }
        public ActionCommand Copy()
        {
            return new ActionCommand(Bits.Copy(commandData), this.offset);
        }
        // universal functions
        public override string ToString()
        {
            return Interpreter.Instance.InterpretCommand(this);
        }
    }
}
