using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using LAZYSHELL.ScriptsEditor.Commands;
using LAZYSHELL.ScriptsEditor;

namespace LAZYSHELL.ScriptsEditor
{
    [Serializable()]
    public class ActionScript : Element
    {
        // universal variables
        private int index; public override int Index { get { return index; } set { index = value; } }
        // non-serialized variables
        [NonSerialized()]
        private byte[] rom = null;
        // class variables
        private List<ActionCommand> commands;
        public List<ActionCommand> Commands { get { return this.commands; } set { this.commands = value; } }
        public ActionCommand LastCommand
        {
            get
            {
                if (commands.Count > 0)
                    return commands[commands.Count - 1];
                else
                    return null;
            }
        }
        private byte[] script; public byte[] Script { get { return script; } set { script = value; } }
        private int baseOffset; public int BaseOffset { get { return this.baseOffset; } set { this.baseOffset = value; } }
        private int endInternalOffset
        {
            get
            {
                if (LastCommand != null)
                    return LastCommand.InternalOffset + LastCommand.Length;
                return 0;
            }
        }
        private int endOffset
        {
            get
            {
                if (LastCommand != null)
                    return LastCommand.Offset + LastCommand.Length;
                return 0;
            }
        }
        public int Length
        {
            get
            {
                return script.Length;
            }
        }
        public bool Undoing;
        private bool embedded = false;
        public bool Embedded { get { return this.embedded; } set { this.embedded = value; } }
        // constructors
        public ActionScript(int index)
        {
            this.rom = Model.ROM;
            this.index = index;
            this.embedded = false;
            Disassemble();
        }
        public ActionScript(byte[] commandData, int index, int offset)
        {
            this.script = commandData;
            this.index = index;
            this.baseOffset = offset;
            this.embedded = true;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int length, offset = 0x210000;
            if (rom != null)
            {
                length = GetLength();
                offset = Bits.GetShort(rom, 0x210000 + (index * 2));
                this.baseOffset = 0x210000 + offset;
                this.script = Bits.GetBytes(rom, 0x210000 + offset, length);
            }
            ParseScript();
        }
        public void Assemble()
        {
            int offset = 0;
            if (commands == null)
            {
                script = new byte[offset];
                return;
            }
            foreach (ActionCommand asc in commands)
            {
                asc.Assemble();
                offset += asc.Length;
            }
            script = new byte[offset];
            offset = 0;
            foreach (ActionCommand asc in commands)
            {
                asc.CommandData.CopyTo(script, offset);
                offset += asc.Length;
            }
        }
        // class functions
        public void ParseScript()
        {
            this.commands = new List<ActionCommand>();
            int offset = 0, length;
            while (offset < script.Length)
            {
                byte param1 = 0;
                if (script.Length - offset > 1)
                    param1 = script[offset + 1];
                length = ScriptEnums.GetActionCommandLength(script[offset], param1);
                commands.Add(new ActionCommand(Bits.GetBytes(script, offset, length), this.baseOffset + offset));
                offset += length;
            }
        }
        private int GetLength()
        {
            int offset = Bits.GetShort(rom, 0x210000 + index * 2);
            int length;
            int start;
            if (index == 1023)
            {
                length = 0xC000 - offset;
                start = 0x21BFFF;
                while (rom[start] == 0xFF)
                {
                    length--;
                    start--;
                }
                return length;
            }
            else
                return Bits.GetShort(rom, 0x210000 + (index + 1) * 2) - offset;
        }
        // list management
        public void Add(ActionCommand asc)
        {
            commands.Add(asc);
        }
        public void Insert(int index, ActionCommand asc)
        {
            try
            {
                commands.Insert(index, asc);
            }
            catch
            {
                throw new Exception("Invalid index.");
            }
        }
        public int RemoveAt(int index)
        {
            try
            {
                ActionCommand asc = commands[index];
                int length = asc.Length;
                commands.RemoveAt(index);
                return length;
            }
            catch
            {
                throw new Exception("Invalid index.");
            }
        }
        public void Reverse(int index1, int index2)
        {
            ActionCommand asc = commands[index1];
            commands[index1] = commands[index2];
            commands[index2] = asc;
        }
        public void Refresh()
        {
            if (commands == null)
                return;
            Assemble();
            // refresh offsets
            int offset = baseOffset;
            foreach (ActionCommand asc in commands)
            {
                asc.Offset = offset;
                offset += asc.Length;
            }
            // update internal offsets
            EventActionCommand eac;
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                eac.PointerChanged = new bool[256];
            }
            it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                if (!Undoing && State.Instance.AutoPointerUpdate)
                    UpdatePointersToCommand(eac);
                eac.InternalOffset = eac.Offset;
            }
            if (!Undoing && State.Instance.AutoPointerUpdate)
                UpdatePointersAfterScript();
        }
        /// <summary>
        /// Updates all of the pointers in the script pointing directly to a given command's offset.
        /// </summary>
        /// <param name="reference">The reference command in the script.</param>
        private void UpdatePointersToCommand(EventActionCommand reference)
        {
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                EventActionCommand eac = it.Next();
                int pointer;
                if (eac.Opcode == 0x42 || eac.Opcode == 0x67 || eac.Opcode == 0xE9)
                {
                    if (eac.GetType() == typeof(EventCommand) || eac.Opcode == 0xE9)
                    {
                        pointer = eac.ReadPointerSpecial(0);
                        if (pointer == (reference.InternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                        {
                            eac.WritePointerSpecial(0, (ushort)(reference.Offset & 0xFFFF));
                            eac.PointerChanged[0] = true;
                        }
                        pointer = eac.ReadPointerSpecial(1);
                        if (pointer == (reference.InternalOffset & 0xFFFF) && !eac.PointerChanged[1])
                        {
                            eac.WritePointerSpecial(1, (ushort)(reference.Offset & 0xFFFF));
                            eac.PointerChanged[1] = true;
                        }
                    }
                    else
                    {
                        pointer = eac.ReadPointer();
                        if (pointer == (reference.InternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                        {
                            eac.WritePointer((ushort)(reference.Offset & 0xFFFF));
                            eac.PointerChanged[0] = true;
                        }
                    }
                }
                else
                {
                    pointer = eac.ReadPointer();
                    if (pointer == (reference.InternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                    {
                        eac.WritePointer((ushort)(reference.Offset & 0xFFFF));
                        eac.PointerChanged[0] = true;
                    }
                }
            }
        }
        /// <summary>
        /// Adds/subtracts a value from all of the pointers in the script that point to an offset after the script.
        /// </summary>
        /// <param name="delta">The value to add/subtract.</param>
        private void UpdatePointersAfterScript()
        {
            int delta = this.endOffset - this.endInternalOffset;
            //
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                EventActionCommand eac = it.Next();
                int pointer;
                if (eac.Opcode == 0x42 || eac.Opcode == 0x67 || eac.Opcode == 0xE9)
                {
                    if (eac.GetType() == typeof(EventCommand) || eac.Opcode == 0xE9)
                    {
                        pointer = eac.ReadPointerSpecial(0);
                        if (pointer >= (this.endInternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                        {
                            eac.WritePointerSpecial(0, (ushort)(pointer + delta));
                            eac.PointerChanged[0] = true;
                        }
                        pointer = eac.ReadPointerSpecial(1);
                        if (pointer >= (this.endInternalOffset & 0xFFFF) && !eac.PointerChanged[1])
                        {
                            eac.WritePointerSpecial(1, (ushort)(pointer + delta));
                            eac.PointerChanged[1] = true;
                        }
                    }
                    else
                    {
                        pointer = eac.ReadPointer();
                        if (pointer >= (this.endInternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                        {
                            eac.WritePointer((ushort)(pointer + delta));
                            eac.PointerChanged[0] = true;
                        }
                    }
                }
                else
                {
                    pointer = eac.ReadPointer();
                    if (pointer >= (this.endInternalOffset & 0xFFFF) && !eac.PointerChanged[0])
                    {
                        eac.WritePointer((ushort)(pointer + delta));
                        eac.PointerChanged[0] = true;
                    }
                }
            }
        }
        public ActionScript Copy()
        {
            ActionScript copy = new ActionScript(this.index);
            return copy;
        }
        public override void Clear()
        {
            if (commands != null)
                commands.Clear();
            Assemble();
        }
        // public functions
        public void UpdateOffsets(int delta, int conditionOffset)
        {
            UpdateOffsets(delta, conditionOffset, true);
        }
        public void UpdateOffsets(int delta, int conditionOffset, bool pushOffsetTreeWrapper)
        {
            if (this.baseOffset >= conditionOffset || conditionOffset == 0x7FFFFFFF)
                this.baseOffset += delta;
            if (commands == null)
                return;
            foreach (ActionCommand asc in commands)
                asc.UpdatePointer(delta, conditionOffset, pushOffsetTreeWrapper);
        }
    }
}
