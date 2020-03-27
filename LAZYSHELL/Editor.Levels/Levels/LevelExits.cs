using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelExits
    {
        // class variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        private List<Exit> exits = new List<Exit>();
        private Exit exit;
        private int currentExit = 0;
        private int selectedExit;
        // accessors        
        public List<Exit> Exits { get { return exits; } }
        public int CurrentExit
        {
            get
            {
                return this.currentExit;
            }
            set
            {
                if (this.exits.Count > value)
                {
                    exit = (Exit)exits[value];
                    this.currentExit = value;
                }
            }
        }
        public int SelectedExit { get { return this.selectedExit; } set { selectedExit = value; } }
        public Exit Exit { get { return exit; } }
        public int Length
        {
            get
            {
                int length = 5;
                if (ExitType == 0)
                    length += 3;
                if (Width > 0)
                    length++;
                return length;
            }
        }
        public int Count { get { return exits.Count; } }
        // exit properties
        public byte ExitType { get { return exit.ExitType; } set { exit.ExitType = value; } }
        public bool ShowMessage { get { return exit.ShowMessage; } set { exit.ShowMessage = value; } }
        public byte X { get { return exit.X; } set { exit.X = value; } }
        public byte Y { get { return exit.Y; } set { exit.Y = value; } }
        public byte Z { get { return exit.Z; } set { exit.Z = value; } }
        public byte F { get { return exit.F; } set { exit.F = value; } }
        public byte Height { get { return exit.Height; } set { exit.Height = value; } }
        public byte Width { get { return exit.Width; } set { exit.Width = value; } }
        public bool X_Half { get { return exit.X_Half; } set { exit.X_Half = value; } }
        public bool Y_Half { get { return exit.Y_Half; } set { exit.Y_Half = value; } }
        // destination properties
        public ushort Destination { get { return exit.Destination; } set { exit.Destination = value; } }
        public byte DstX { get { return exit.DstX; } set { exit.DstX = value; } }
        public byte DstY { get { return exit.DstY; } set { exit.DstY = value; } }
        public byte DstZ { get { return exit.DstZ; } set { exit.DstZ = value; } }
        public byte DstF { get { return exit.DstFace; } set { exit.DstFace = value; } }
        public bool DstXb7 { get { return exit.DstXb7; } set { exit.DstXb7 = value; } }
        public bool DstYb7 { get { return exit.DstYb7; } set { exit.DstYb7 = value; } }
        // constructor, functions
        public LevelExits(int index)
        {
            this.index = index;
            Disassemble();
        }
        public LevelExits()
        {
        }
        private void Disassemble()
        {
            int pointerOffset = (index * 2) + 0x1D2D64;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset);
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset + 2);
            if (index == 0x1FF)
                offsetEnd = 0;
            // no exit fields for level
            if (offsetStart >= offsetEnd)
                return;
            int offset = offsetStart + 0x1D0000;
            while (offset < offsetEnd + 0x1D0000)
            {
                Exit tExit = new Exit();
                tExit.Disassemble(offset);
                exits.Add(tExit);
                offset += 5;
                if (tExit.ExitType == 0)
                    offset += 3;
                if (tExit.Width > 0)
                    offset += 1;
            }
        }
        public void Assemble(ref int offsetStart)
        {
            int offset = 0;
            int pointerOffset = (index * 2) + 0x1D2D64;
            // set the new pointer for the fields
            Bits.SetShort(rom, pointerOffset, offsetStart);
            // no exit fields for level
            if (exits.Count == 0)
                return;
            offset = offsetStart + 0x1D0000;
            foreach (Exit exit in exits)
            {
                exit.Assemble(offset);
                offset += 5;
                if (exit.ExitType == 0) offset += 3;
                if (exit.Width > 0) offset += 1;
            }
            offsetStart = (ushort)(offset - 0x1D0000);
        }
        public void Assemble(int offsetStart)
        {
            Assemble(ref offsetStart);
        }
        // list managers
        public void Clear()
        {
            exits.Clear();
            this.currentExit = 0;
        }
        public void New(int index, Point p)
        {
            Exit e = new Exit();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < exits.Count)
                exits.Insert(index, e);
            else
                exits.Add(e);
        }
        public void New(int index, Exit copy)
        {
            if (index < exits.Count)
                exits.Insert(index, copy);
            else
                exits.Add(copy);
        }
        public void Remove()
        {
            if (currentExit < exits.Count)
            {
                exits.Remove(exits[currentExit]);
                this.currentExit = 0;
            }
        }
    }
    [Serializable()]
    public class Exit
    {
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public int Index = 0;
        public bool Hilite = false;
        public int Length
        {
            get
            {
                int length = 5;
                if (exitType == 0)
                    length += 3;
                if (width > 0)
                    length++;
                return length;
            }
        }
        // exit properties
        private ushort destination; public ushort Destination { get { return destination; } set { destination = value; } }
        private byte exitType; public byte ExitType { get { return exitType; } set { exitType = value; } }
        private bool showMessage; public bool ShowMessage { get { return showMessage; } set { showMessage = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte z; public byte Z { get { return z; } set { z = value; } }
        private byte f; public byte F { get { return f; } set { f = value; } }
        private byte height; public byte Height { get { return height; } set { height = value; } }
        private byte width; public byte Width { get { return width; } set { width = value; } }
        private bool x_half; public bool X_Half { get { return x_half; } set { x_half = value; } }
        private bool y_half; public bool Y_Half { get { return y_half; } set { y_half = value; } }
        // destination properties
        private byte dstX; public byte DstX { get { return dstX; } set { dstX = value; } }
        private byte dstY; public byte DstY { get { return dstY; } set { dstY = value; } }
        private byte dstZ; public byte DstZ { get { return dstZ; } set { dstZ = value; } }
        private byte dstFace; public byte DstFace { get { return dstFace; } set { dstFace = value; } }
        private bool dstXb7; public bool DstXb7 { get { return dstXb7; } set { dstXb7 = value; } }
        private bool dstYb7; public bool DstYb7 { get { return dstYb7; } set { dstYb7 = value; } }
        // assemblers
        public void Disassemble(int offset)
        {
            byte temp = 0;
            destination = (ushort)(Bits.GetShort(rom, offset++) & 0x01FF);
            temp = rom[offset++];
            showMessage = (temp & 0x08) == 0x08;
            exitType = (byte)((temp & 0x60) >> 6);
            bool lengthOverOne = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            x_half = (temp & 0x80) == 0x80;
            x = (byte)(temp & 0x7F);
            temp = rom[offset++];
            y_half = (temp & 0x80) == 0x80;
            y = (byte)(temp & 0x7F);
            temp = rom[offset++];
            z = (byte)(temp & 0x1F);
            height = (byte)((temp & 0xF0) >> 5);
            //
            if (exitType == 0)
            {
                temp = rom[offset++];
                dstX = (byte)(temp & 0x7F);
                dstXb7 = (temp & 0x80) == 0x80;
                temp = rom[offset++];
                dstY = (byte)(temp & 0x7F);
                dstYb7 = (temp & 0x80) == 0x80;
                temp = rom[offset++];
                dstZ = (byte)(temp & 0x1F);
                dstFace = (byte)((temp & 0xF0) >> 5);
            }
            else if (exitType == 1)
                destination &= 0xFF;
            if (lengthOverOne)
            {
                temp = rom[offset++];
                width = (byte)(temp & 0x0F);
                f = (byte)((temp & 0x80) >> 7);
            }
        }
        public void Assemble(int offset)
        {
            Bits.SetShort(rom, offset++, destination);
            Bits.SetBit(rom, offset, 3, showMessage);
            if (exitType == 0)
                Bits.SetBit(rom, offset, 5, true);
            else if (exitType == 1)
                Bits.SetBit(rom, offset, 6, true);
            Bits.SetBit(rom, offset++, 7, width > 0);
            rom[offset] = x;
            Bits.SetBit(rom, offset++, 7, x_half);
            rom[offset] = y;
            Bits.SetBit(rom, offset++, 7, y_half);
            rom[offset] = z;
            Bits.SetBitsByByte(rom, offset++, (byte)(height << 5), true);
            if (exitType == 0)
            {
                rom[offset] = dstX;
                Bits.SetBit(rom, offset++, 7, dstXb7);
                rom[offset] = dstY;
                Bits.SetBit(rom, offset++, 7, dstYb7);
                rom[offset] = dstZ;
                Bits.SetBitsByByte(rom, offset++, (byte)(dstFace << 5), true);
            }
            if (width > 0)
            {
                rom[offset] = width;
                Bits.SetBitsByByte(rom, offset++, (byte)(f << 7), true);
            }
        }
        // spawning
        public Exit Copy()
        {
            Exit copy = new Exit();
            copy.Destination = destination;
            copy.ExitType = exitType;
            copy.ShowMessage = showMessage;
            copy.X = x;
            copy.Y = y;
            copy.Z = z;
            copy.Height = height;
            copy.X_Half = x_half;
            copy.Y_Half = y_half;
            copy.DstX = dstX;
            copy.DstY = dstY;
            copy.DstZ = dstZ;
            copy.DstFace = dstFace;
            copy.DstXb7 = dstXb7;
            copy.DstYb7 = dstYb7;
            copy.Width = width;
            copy.F = f;
            return copy;
        }
    }
}
