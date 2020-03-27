using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    [Serializable()]
    public class EventScript : Element
    {
        // universal variables
        private int index; public override int Index { get { return index; } set { index = value; } }
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables, accessors
        private byte[] script; public byte[] Script { get { return script; } set { script = value; } }
        private List<EventCommand> commands;
        public List<EventCommand> Commands
        {
            get
            {
                if (this.commands == null)
                    this.commands = new List<EventCommand>();
                return this.commands;
            }
            set { this.commands = value; }
        }
        public EventCommand LastCommand
        {
            get
            {
                if (commands.Count > 0)
                    return commands[commands.Count - 1];
                else
                    return null;
            }
        }
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
                return this.script.Length;
            }
        }
        public bool Undoing;
        // constructors
        public EventScript(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int bank;
            int indexinbank = 0;
            if (index >= 0 && index <= 1535)
            {
                bank = 0x1E0000;
                indexinbank = index;
            }
            else if (index >= 1536 && index <= 3071)
            {
                bank = 0x1F0000;
                indexinbank = index - 1536;
            }
            else if (index >= 3072 && index <= 4095)
            {
                bank = 0x200000;
                indexinbank = index - 3072;
            }
            else
                throw new Exception("Invalid index.");
            //
            int length = GetLength(bank, indexinbank);
            int offset = Bits.GetShort(rom, bank + indexinbank * 2);
            this.baseOffset = bank + offset;
            this.script = Bits.GetBytes(rom, bank + offset, length);
            //
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
            foreach (EventCommand esc in commands)
            {
                esc.Assemble();
                offset += esc.Length;
            }
            script = new byte[offset];
            offset = 0;
            foreach (EventCommand esc in commands)
            {
                esc.CommandData.CopyTo(script, offset);
                offset += esc.Length;
            }
        }
        // class functions
        public void ParseScript()
        {
            int offset = 0, length = 0;
            if (script.Length > 0 && this.commands == null)
                this.commands = new List<EventCommand>();
            //
            EventCommand esc;
            while (offset < script.Length)
            {
                switch (index)
                {
                    case 0x1D6: if (offset != 0xA1) goto default; length = 0x6B; goto case 0xE91;
                    case 0x72D: if (offset != 0x22) goto default; length = 0x3B; goto case 0xE91;
                    case 0x72F: if (offset != 0x22) goto default; length = 0x3C; goto case 0xE91;
                    case 0xD01: if (offset != 0x34) goto default; length = 0x87; goto case 0xE91;
                    case 0xE91: if (index == 0xE91) { if (offset != 0x3C4) goto default; length = 0x51; }
                        esc = new EventCommand(Bits.GetBytes(script, offset, length), this.baseOffset + offset);
                        esc.Queue = new ActionScript(Bits.GetBytes(esc.CommandData, 0, esc.Length), -1, esc.Offset);
                        esc.Locked = true;
                        commands.Add(esc);
                        break;
                    default:
                        length = GetCommandLength(script, offset);
                        esc = new EventCommand(Bits.GetBytes(script, offset, length), this.baseOffset + offset);
                        commands.Add(esc);
                        break;
                }
                offset += length;
            }
        }
        private int GetCommandLength(byte[] script, int offset)
        {
            byte opcode = script[offset];
            byte param1;
            if (script.Length - offset > 1)
                param1 = script[offset + 1];
            else
                param1 = 0;
            int length = ScriptEnums.GetEventCommandLength(opcode, param1);
            // Handles special case
            if (opcode <= 0x2F && (param1 == 0xF0 || param1 == 0xF1) && length == 3)
            {
                if (Bits.GetBit(script[offset + 2], 7))
                    length += script[offset + 2] & 0x3F; // Max value of 63 0x3F
                else
                    length += script[offset + 2] & 0x7F; // Max value of 127 0x7F
            }
            else if (opcode <= 0x2F && param1 < 0xF0)
            {
                for (int i = 0; i < length - 2; )
                {
                    opcode = script[offset + 2 + i];
                    if (script.Length - (offset + i + 2) > 1)
                        param1 = script[offset + 2 + 1 + i];
                    else
                        param1 = 0;
                    i += ScriptEnums.GetActionCommandLength(opcode, param1);
                }
            }
            return length;
        }
        private int GetLength(int bank, int indexinbank)
        {
            int offset = Bits.GetShort(rom, bank + indexinbank * 2);
            int length;
            if (this.index == 1535 || this.index == 3071)
                length = GetPrevLength(bank + 0xFFFF, 0x10000 - offset);
            else if (this.index == 4095)
                length = GetPrevLength(bank + 0xDFFF, 0xe000 - offset);
            else
                length = Bits.GetShort(rom, bank + ((indexinbank + 1) * 2)) - offset;
            return length;
        }
        private int GetPrevLength(int offset, int length)
        {
            while (rom[offset] == 0xFF)
            {
                length--;
                offset--;
            }
            return length;
        }
        // list managers
        public void Add(EventCommand esc)
        {
            commands.Add(esc);
        }
        public void Add(int parentIndex, ActionCommand asc)
        {
            EventCommand esc = commands[parentIndex];
            if (esc.QueueTrigger)
                esc.Queue.Add(asc);
        }
        public void Insert(int index, EventCommand esc)
        {
            try
            {
                commands.Insert(index, esc);
            }
            catch
            {
                throw new Exception("Event Script insert index invalid");
            }
        }
        public void Insert(int parentIndex, int childIndex, ActionCommand asc)
        {
            try
            {
                EventCommand esc = commands[parentIndex];
                if (esc.QueueTrigger)
                {
                    esc.Queue.Insert(childIndex, asc);
                }
            }
            catch
            {
                throw new Exception("Event Script insert index invalid");
            }
        }
        public int RemoveAt(int index)
        {
            try
            {
                EventCommand esc = commands[index];
                int len = esc.Length;
                commands.RemoveAt(index);
                return len;
            }
            catch
            {
                throw new Exception("Event Script remove index invalid");
            }
        }
        public int RemoveAt(int parentIndex, int childIndex)
        {
            try
            {
                EventCommand esc = commands[parentIndex];
                int length = 0;
                if (esc.QueueTrigger)
                {
                    ActionCommand aqc = esc.Queue.Commands[childIndex];
                    length = aqc.Length;
                    esc.Queue.RemoveAt(childIndex);
                }
                return length;
            }
            catch
            {
                throw new Exception("Event Script insert index invalid.");
            }
        }
        public void Reverse(int index1, int index2)
        {
            EventCommand esc = (EventCommand)commands[index1];
            commands[index1] = commands[index2];
            commands[index2] = esc;
        }
        public void Refresh()
        {
            if (commands == null)
                return;
            Assemble();
            // refresh offsets
            int offset = baseOffset;
            foreach (EventCommand esc in commands)
            {
                esc.RefreshOffsets(offset);
                offset += esc.Length;
            }
            // update internal pointers
            EventActionCommand eac;
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                eac.PointerChanged = new bool[256];
            }
            // if undo/redo, pointers update by raw script change
            if (!Undoing && State.Instance.AutoPointerUpdate)
                UpdatePointersAfterScript();
            it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                if (!Undoing && State.Instance.AutoPointerUpdate)
                    UpdatePointersToCommand(eac);
                eac.InternalOffset = eac.Offset;
            }
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
        /// Updates all of the pointers in the script that point to an offset after the script.
        /// </summary>
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
        public EventScript Copy()
        {
            EventScript copy = new EventScript(this.index);
            return copy;
        }
        public override void Clear()
        {
            if (commands != null)
                commands.Clear();
            Assemble();
        }
        // public functions
        public void UpdateAllOffsets(int delta, int conditionOffset)
        {
            if (this.baseOffset >= conditionOffset || conditionOffset == 0x7fffffff)
                this.baseOffset += delta;
            if (commands == null)
                return;
            foreach (EventCommand esc in commands)
                esc.UpdatePointer(delta, conditionOffset);
        }
    }
}
