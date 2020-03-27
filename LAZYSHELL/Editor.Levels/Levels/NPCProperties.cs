using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class NPCProperties
    {
        // universal functions
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private ushort sprite; public ushort Sprite { get { return sprite; } set { sprite = value; } }
        private bool priority0; public bool Priority0 { get { return priority0; } set { priority0 = value; } }
        private bool priority1; public bool Priority1 { get { return priority1; } set { priority1 = value; } }
        private bool priority2; public bool Priority2 { get { return priority2; } set { priority2 = value; } }
        private byte yPixelShiftUp; public byte YPixelShiftUp { get { return yPixelShiftUp; } set { yPixelShiftUp = value; } }
        private bool shift16pxDown; public bool Shift16pxDown { get { return shift16pxDown; } set { shift16pxDown = value; } }
        private byte acuteAxis; public byte AcuteAxis { get { return acuteAxis; } set { acuteAxis = value; } }
        private byte obtuseAxis; public byte ObtuseAxis { get { return obtuseAxis; } set { obtuseAxis = value; } }
        private byte height; public byte Height { get { return height; } set { height = value; } }
        private byte shadow; public byte Shadow { get { return shadow; } set { shadow = value; } }
        private byte byte1a; public byte Byte1a { get { return byte1a; } set { byte1a = value; } }
        private byte byte1b; public byte Byte1b { get { return byte1b; } set { byte1b = value; } }
        private bool b2b0; public bool B2b0 { get { return b2b0; } set { b2b0 = value; } }
        private bool b2b1; public bool B2b1 { get { return b2b1; } set { b2b1 = value; } }
        private bool b2b2; public bool B2b2 { get { return b2b2; } set { b2b2 = value; } }
        private bool b2b3; public bool B2b3 { get { return b2b3; } set { b2b3 = value; } }
        private bool b2b4; public bool B2b4 { get { return b2b4; } set { b2b4 = value; } }
        private bool activeVRAM; public bool ActiveVRAM { get { return activeVRAM; } set { activeVRAM = value; } }
        private bool showShadow; public bool ShowShadow { get { return showShadow; } set { showShadow = value; } }
        private bool b5b6; public bool B5b6 { get { return b5b6; } set { b5b6 = value; } }
        private bool b5b7; public bool B5b7 { get { return b5b7; } set { b5b7 = value; } }
        private bool b6b2; public bool B6b2 { get { return b6b2; } set { b6b2 = value; } }
        // constructor
        public NPCProperties(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = index * 7 + 0x1DB800;
            ushort temp = Bits.GetShort(rom, offset++);
            //
            sprite = (ushort)(temp & 0x03FF);
            byte1a = (byte)((rom[offset] >> 2) & 7);
            byte1b = (byte)(rom[offset++] >> 5);
            //
            priority0 = (rom[offset] & 0x20) == 0x20;
            priority1 = (rom[offset] & 0x40) == 0x40;
            priority2 = (rom[offset] & 0x80) == 0x80;
            b2b0 = (rom[offset] & 0x01) == 0x01;
            b2b1 = (rom[offset] & 0x02) == 0x02;
            b2b2 = (rom[offset] & 0x04) == 0x04;
            b2b3 = (rom[offset] & 0x08) == 0x08;
            b2b4 = (rom[offset++] & 0x10) == 0x10;
            //
            yPixelShiftUp = (byte)(rom[offset] & 0x0F);
            shift16pxDown = (rom[offset] & 0x10) == 0x10;
            shadow = (byte)((rom[offset] & 0x60) >> 5);
            activeVRAM = (rom[offset++] & 0x80) == 0x80;
            //
            acuteAxis = (byte)(rom[offset] & 0x0F);
            obtuseAxis = (byte)((rom[offset++] & 0xF0) >> 4);
            //
            height = (byte)(rom[offset] & 0x1F);
            showShadow = (rom[offset] & 0x20) == 0x20;
            b5b6 = (rom[offset] & 0x40) == 0x40;
            b5b7 = (rom[offset++] & 0x80) == 0x80;
            //
            b6b2 = (rom[offset] & 0x04) == 0x04;
        }
        public void Assemble()
        {
            int offset = index * 7 + 0x1DB800;
            //
            Bits.SetShort(rom, offset++, sprite);
            rom[offset] |= (byte)(byte1a << 2);
            rom[offset++] |= (byte)(byte1b << 5);
            //
            Bits.SetBit(rom, offset, 5, priority0);
            Bits.SetBit(rom, offset, 6, priority1);
            Bits.SetBit(rom, offset, 7, priority2);
            Bits.SetBit(rom, offset, 0, b2b0);
            Bits.SetBit(rom, offset, 1, b2b1);
            Bits.SetBit(rom, offset, 2, b2b2);
            Bits.SetBit(rom, offset, 3, b2b3);
            Bits.SetBit(rom, offset++, 4, b2b4);
            //
            rom[offset] = yPixelShiftUp;
            Bits.SetBit(rom, offset, 4, shift16pxDown);
            rom[offset] &= 0x9F;
            rom[offset] |= (byte)(shadow << 5);
            Bits.SetBit(rom, offset++, 7, activeVRAM);
            //
            rom[offset] = acuteAxis;
            rom[offset++] |= (byte)(obtuseAxis << 4);
            //
            rom[offset] = height;
            Bits.SetBit(rom, offset, 5, showShadow);
            Bits.SetBit(rom, offset, 6, b5b6);
            Bits.SetBit(rom, offset++, 7, b5b7);
            //
            Bits.SetBit(rom, offset, 2, b6b2);
        }
        // spawning
        public NPCProperties Copy()
        {
            return new NPCProperties(this.index);
        }
    }
}
