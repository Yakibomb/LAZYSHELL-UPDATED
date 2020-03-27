using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    public class Partitions
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private bool b1b0; public bool Byte1b0 { get { return b1b0; } set { b1b0 = value; } }
        private bool b1b1; public bool Byte1b1 { get { return b1b1; } set { b1b1 = value; } }
        private bool b1b2; public bool Byte1b2 { get { return b1b2; } set { b1b2 = value; } }
        private bool b1b3; public bool Byte1b3 { get { return b1b3; } set { b1b3 = value; } }
        // main buffer
        private bool extraSprites; public bool ExtraSprites { get { return extraSprites; } set { extraSprites = value; } }
        private bool fullPaletteBuffer; public bool FullPaletteBuffer { get { return fullPaletteBuffer; } set { fullPaletteBuffer = value; } }
        private byte allySpriteBuffer; public byte AllySpriteBuffer { get { return allySpriteBuffer; } set { allySpriteBuffer = value; } }
        private byte extraSpriteBuffer; public byte ExtraSpriteBuffer { get { return extraSpriteBuffer; } set { extraSpriteBuffer = value; } }
        // clone buffers
        private byte cloneAsprite; public byte CloneASprite { get { return cloneAsprite; } set { cloneAsprite = value; } }
        private byte cloneAmain; public byte CloneAMain { get { return cloneAmain; } set { cloneAmain = value; } }
        private bool cloneAindexing; public bool CloneAIndexing { get { return cloneAindexing; } set { cloneAindexing = value; } }
        private byte cloneBsprite; public byte CloneBSprite { get { return cloneBsprite; } set { cloneBsprite = value; } }
        private byte cloneBmain; public byte CloneBMain { get { return cloneBmain; } set { cloneBmain = value; } }
        private bool cloneBindexing; public bool CloneBIndexing { get { return cloneBindexing; } set { cloneBindexing = value; } }
        private byte cloneCsprite; public byte CloneCSprite { get { return cloneCsprite; } set { cloneCsprite = value; } }
        private byte cloneCmain; public byte CloneCMain { get { return cloneCmain; } set { cloneCmain = value; } }
        private bool cloneCindexing; public bool CloneCIndexing { get { return cloneCindexing; } set { cloneCindexing = value; } }
        // constructor
        public Partitions(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = index * 4 + 0x1DDE00;
            byte temp = 0;
            temp = rom[offset++];
            extraSprites = (temp & 0x10) == 0x10;
            fullPaletteBuffer = (temp & 0x80) == 0x80;
            allySpriteBuffer = (byte)((temp & 0x60) >> 5);
            extraSpriteBuffer = (byte)(temp & 0x0F);
            temp = rom[offset++];
            cloneAsprite = (byte)(temp & 0x07);
            cloneAmain = (byte)((temp & 0x70) >> 4);
            cloneAindexing = (temp & 0x80) == 0x80;
            temp = rom[offset++];
            cloneBsprite = (byte)(temp & 0x07);
            cloneBmain = (byte)((temp & 0x70) >> 4);
            cloneBindexing = (temp & 0x80) == 0x80;
            temp = rom[offset++];
            cloneCsprite = (byte)(temp & 0x07);
            cloneCmain = (byte)((temp & 0x70) >> 4);
            cloneCindexing = (temp & 0x80) == 0x80;
        }
        public void Assemble()
        {
            int offset = index * 4 + 0x1DDE00;
            //
            rom[offset] = 0;
            rom[offset] = (byte)(allySpriteBuffer << 5);
            rom[offset] |= extraSpriteBuffer;
            Bits.SetBit(rom, offset, 4, extraSprites);
            Bits.SetBit(rom, offset, 7, fullPaletteBuffer);
            offset++;
            //
            rom[offset] = cloneAsprite;
            rom[offset] |= (byte)(cloneAmain << 4);
            Bits.SetBit(rom, offset, 7, cloneAindexing);
            offset++;
            //
            rom[offset] = cloneBsprite;
            rom[offset] |= (byte)(cloneBmain << 4);
            Bits.SetBit(rom, offset, 7, cloneBindexing);
            offset++;
            //
            rom[offset] = cloneCsprite;
            rom[offset] |= (byte)(cloneCmain << 4);
            Bits.SetBit(rom, offset, 7, cloneCindexing);
            offset++;
        }
    }
}
