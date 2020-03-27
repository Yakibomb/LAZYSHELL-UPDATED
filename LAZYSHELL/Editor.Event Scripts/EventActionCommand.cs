using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    [Serializable()]
    public abstract class EventActionCommand
    {
        protected int offset; public int Offset { get { return this.offset; } set { this.offset = value; } }
        // Used to link up pointers outside of current script
        protected int originalOffset; public int OriginalOffset { get { return this.originalOffset; } set { this.originalOffset = value; } }
        // Used to link up pointers inside of current script
        protected int internalOffset; public int InternalOffset { get { return this.internalOffset; } set { this.internalOffset = value; } }
        // Used for updating internal offsets and pointers
        protected bool[] pointerChanged; public bool[] PointerChanged { get { return this.pointerChanged; } set { this.pointerChanged = value; } }
        public int Delta { get { return this.offset - originalOffset; } }
        public byte Opcode { get { return GetOpcode(); } set { SetOpcode(value); } }
        public byte Param1 { get { return GetParam(1); } set { SetParam(value, 1); } }
        public byte Param2 { get { return GetParam(2); } set { SetParam(value, 2); } }
        public byte Param3 { get { return GetParam(3); } set { SetParam(value, 3); } }
        public byte Param4 { get { return GetParam(4); } set { SetParam(value, 4); } }
        public byte Param5 { get { return GetParam(5); } set { SetParam(value, 5); } }
        public abstract ushort ReadPointer();
        public abstract void WritePointer(ushort pointer);
        public abstract ushort ReadPointerSpecial(int index);
        public abstract void WritePointerSpecial(int index, ushort pointer);
        protected abstract byte GetOpcode();
        protected abstract void SetOpcode(byte opcode);
        protected abstract byte GetParam(int index);
        protected abstract void SetParam(byte param, int index);
    }
}
