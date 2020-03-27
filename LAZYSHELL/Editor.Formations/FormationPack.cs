using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class FormationPack : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private ushort[] formations = new ushort[3];
        public ushort[] Formations { get { return this.formations; } set { this.formations = value; } }
        // constructor
        public FormationPack(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 4) + 0x39222A;
            formations[0] = rom[offset++];
            formations[1] = rom[offset++];
            formations[2] = rom[offset++];
            if ((rom[offset] & 0x01) == 0x01)
                formations[0] += 0x100;
            if ((rom[offset] & 0x02) == 0x02)
                formations[1] += 0x100;
            if ((rom[offset] & 0x04) == 0x04)
                formations[2] += 0x100;
        }
        public void Assemble()
        {
            int offset = (index * 4) + 0x39222A;
            rom[offset++] = (byte)formations[0];
            rom[offset++] = (byte)formations[1];
            rom[offset++] = (byte)formations[2];
            Bits.SetBit(rom, offset, 0, formations[0] >= 0x100);
            Bits.SetBit(rom, offset, 1, formations[1] >= 0x100);
            Bits.SetBit(rom, offset, 2, formations[2] >= 0x100);
        }
        // universal variables
        public override void Clear()
        {
            formations = new ushort[3];
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
