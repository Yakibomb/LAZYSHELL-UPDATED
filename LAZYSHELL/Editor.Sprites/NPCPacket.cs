using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class NPCPacket
    {
        // universal functions
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private byte sprite; public byte Sprite { get { return sprite; } set { sprite = value; } }
        private byte b0; public byte B0 { get { return b0; } set { b0 = value; } }
        private byte b1a; public byte B1a { get { return b1a; } set { b1a = value; } }
        private byte b1b; public byte B1b { get { return b1b; } set { b1b = value; } }
        private byte b1c; public byte B1c { get { return b1c; } set { b1c = value; } }
        private bool b2b2; public bool B2b2 { get { return b2b2; } set { b2b2 = value; } }
        private bool b2b3; public bool B2b3 { get { return b2b3; } set { b2b3 = value; } }
        private bool b2b4; public bool B2b4 { get { return b2b4; } set { b2b4 = value; } }
        private bool showShadow; public bool ShowShadow { get { return showShadow; } set { showShadow = value; } }
        private byte b2; public byte B2 { get { return b2; } set { b2 = value; } }
        private ushort action; public ushort Action { get { return action; } set { action = value; } }
        private byte b4; public byte B4 { get { return b4; } set { b4 = value; } }
        // constructor
        public NPCPacket(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = index * 5 + 0x1DB000;
            //
            sprite = (byte)(rom[offset] & 0x3F);
            b0 = (byte)(rom[offset++] >> 6);
            b1a = (byte)(rom[offset] & 0x07);
            b1b = (byte)((rom[offset] >> 3) & 0x03);
            b1c = (byte)(rom[offset++] >> 5);
            b2b2 = Bits.GetBit(rom, offset, 2);
            b2b3 = Bits.GetBit(rom, offset, 3);
            b2b4 = Bits.GetBit(rom, offset, 4);
            showShadow = Bits.GetBit(rom, offset, 5);
            b2 = (byte)(rom[offset++] >> 6);
            action = (ushort)(Bits.GetShort(rom, offset++) & 0x3FF);
            b4 = (byte)(rom[offset] >> 4);
        }
        public void Assemble()
        {
            int offset = index * 5 + 0x1DB000;
            //
            rom[offset] = sprite;
            rom[offset++] |= (byte)(b0 << 6);
            rom[offset] = b1a;
            rom[offset] |= (byte)(b1b << 3);
            rom[offset++] |= (byte)(b1c << 5);
            Bits.SetBit(rom, offset, 2, b2b2);
            Bits.SetBit(rom, offset, 3, b2b3);
            Bits.SetBit(rom, offset, 4, b2b4);
            Bits.SetBit(rom, offset, 5, showShadow);
            rom[offset++] |= (byte)(b2 << 6);
            Bits.SetShort(rom, offset++, action);
            rom[offset] |= (byte)(b4 << 4);
        }
    }
}
