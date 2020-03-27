using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelOverlaps
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variales
        private List<Overlap> overlaps = new List<Overlap>();
        public List<Overlap> Overlaps { get { return overlaps; } }
        public int Count { get { return overlaps.Count; } }
        // external selectors
        private Overlap overlap;
        private int currentOverlap = 0;
        private int selectedOverlap;
        public Overlap Overlap { get { return overlap; } }
        public int CurrentOverlap
        {
            get
            {
                return this.currentOverlap;
            }
            set
            {
                if (this.overlaps.Count > value)
                {
                    overlap = overlaps[value];
                    this.currentOverlap = value;
                }
            }
        }
        public int SelectedOverlap { get { return this.selectedOverlap; } set { selectedOverlap = value; } }
        // overlap properties
        public byte X { get { return overlap.X; } set { overlap.X = value; } }
        public byte Y { get { return overlap.Y; } set { overlap.Y = value; } }
        public byte Z { get { return overlap.Z; } set { overlap.Z = value; } }
        public byte Type { get { return overlap.Type; } set { overlap.Type = value; } }
        public bool B0b7 { get { return overlap.B0b7; } set { overlap.B0b7 = value; } }
        public bool B1b7 { get { return overlap.B1b7; } set { overlap.B1b7 = value; } }
        public bool B2b5 { get { return overlap.B2b5; } set { overlap.B2b5 = value; } }
        public bool B2b6 { get { return overlap.B2b6; } set { overlap.B2b6 = value; } }
        public bool B2b7 { get { return overlap.B2b7; } set { overlap.B2b7 = value; } }
        // constructor, functions
        public LevelOverlaps(int index)
        {
            this.index = index;
            Disassemble();
        }
        private void Disassemble()
        {
            int offset;
            ushort offsetStart = 0;
            ushort offsetEnd = 0;
            Overlap tOverlap;
            int pointerOffset = (index * 2) + 0x1D4905;
            offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (index == 0x1FF) offsetEnd = 0;
            if (offsetStart >= offsetEnd)
                return; // no overlaps for level
            offset = offsetStart + 0x1D0000;
            while (offset < offsetEnd + 0x1D0000)
            {
                tOverlap = new Overlap();
                tOverlap.Disassemble(offset);
                overlaps.Add(tOverlap);
                offset += 4;
            }
        }
        public void Assemble(ref int offsetStart)
        {
            int pointerOffset = (index * 2) + 0x1D4905;
            Bits.SetShort(rom, pointerOffset, offsetStart);
            int offset = offsetStart + 0x1D0000;
            offsetStart = (ushort)(offset - 0x1D0000);
            // no exit fields for level
            if (overlaps.Count == 0)
                return;
            //
            foreach (Overlap overlap in overlaps)
            {
                overlap.Assemble(offset);
                offset += 4;
            }
            offsetStart = (ushort)(offset - 0x1D0000);
        }
        // list managers
        public void Clear()
        {
            overlaps.Clear();
            this.currentOverlap = 0;
        }
        public void New(int index, Point p)
        {
            Overlap e = new Overlap();
            e.Clear();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < overlaps.Count)
                overlaps.Insert(index, e);
            else
                overlaps.Add(e);
        }
        public void New(int index, Overlap copy)
        {
            if (index < overlaps.Count)
                overlaps.Insert(index, copy);
            else
                overlaps.Add(copy);
        }
        public void Remove()
        {
            if (currentOverlap < overlaps.Count)
            {
                overlaps.Remove(overlaps[currentOverlap]);
                this.currentOverlap = 0;
            }
        }
    }
    [Serializable()]
    public class Overlap
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public int Index = 0;
        public bool Hilite = false;
        // overlap properties
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte z; public byte Z { get { return z; } set { z = value; } }
        private byte type; public byte Type { get { return type; } set { type = value; } }
        private bool b0b7; public bool B0b7 { get { return b0b7; } set { b0b7 = value; } }
        private bool b1b7; public bool B1b7 { get { return b1b7; } set { b1b7 = value; } }
        private bool b2b5; public bool B2b5 { get { return b2b5; } set { b2b5 = value; } }
        private bool b2b6; public bool B2b6 { get { return b2b6; } set { b2b6 = value; } }
        private bool b2b7; public bool B2b7 { get { return b2b7; } set { b2b7 = value; } }
        // constructor
        public Overlap()
        {
        }
        // assemblers
        public void Disassemble(int offset)
        {
            x = (byte)(rom[offset] & 0x7F);
            b0b7 = (rom[offset++] & 0x80) == 0x80;
            y = (byte)(rom[offset] & 0x7F);
            b1b7 = (rom[offset++] & 0x80) == 0x80;
            z = rom[offset];
            b2b5 = (rom[offset] & 0x20) == 0x20;
            b2b6 = (rom[offset] & 0x40) == 0x40;
            b2b7 = (rom[offset++] & 0x80) == 0x80;
            type = rom[offset];
        }
        public void Assemble(int offset)
        {
            rom[offset] = x;
            Bits.SetBit(rom, offset++, 7, b0b7);
            rom[offset] = y;
            Bits.SetBit(rom, offset++, 7, b1b7);
            rom[offset] = z;
            Bits.SetBit(rom, offset, 5, b2b5);
            Bits.SetBit(rom, offset, 6, b2b6);
            Bits.SetBit(rom, offset++, 7, b2b7);
            rom[offset] = type;
        }
        // spawning
        public Overlap Copy()
        {
            Overlap copy = new Overlap();
            copy.B0b7 = b0b7;
            copy.B1b7 = b1b7;
            copy.B2b5 = b2b5;
            copy.B2b6 = b2b6;
            copy.B2b7 = b2b7;
            copy.X = x;
            copy.Y = y;
            copy.Z = z;
            copy.Type = type;
            return copy;
        }
        // universal functions
        public void Clear()
        {
            x = 0;
            y = 0;
            z = 0;
            type = 0;
            b0b7 = false;
            b1b7 = false;
            b2b5 = false;
            b2b6 = false;
            b2b7 = false;
        }
    }
}
