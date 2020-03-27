using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class Effect
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables
        private byte paletteIndex; public byte PaletteIndex { get { return paletteIndex; } set { paletteIndex = value; } }
        private byte animationPacket; public byte AnimationPacket { get { return animationPacket; } set { animationPacket = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        // constructor
        public Effect(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 4) + 0x251000;
            paletteIndex = (byte)(rom[offset] & 7); offset++;
            animationPacket = rom[offset++];
            x = (byte)(rom[offset] - 1 ^ 255); offset++;
            y = (byte)(rom[offset] - 1 ^ 255); offset++;
        }
        public void Assemble()
        {
            int offset = (index * 4) + 0x251000;
            rom[offset] = paletteIndex; offset++;
            rom[offset] = animationPacket; offset++;
            rom[offset] = (byte)(x - 1 ^ 255); offset++;
            rom[offset] = (byte)(y - 1 ^ 255); offset++;
        }
    }
}
