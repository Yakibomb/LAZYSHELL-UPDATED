using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    [Serializable()]
    public class Animation : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private byte[] buffer; public byte[] BUFFER { get { return buffer; } set { buffer = value; } }
        private int animationOffset; public int AnimationOffset { get { return animationOffset; } set { index = value; } }
        private ushort vramAllocation; public ushort VramAllocation { get { return vramAllocation; } set { vramAllocation = value; } }
        private ushort unknown;
        // animation lists
        private List<Sequence> sequences = new List<Sequence>(); public List<Sequence> Sequences { get { return sequences; } set { sequences = value; } }
        private List<Mold> molds = new List<Mold>(); public List<Mold> Molds { get { return molds; } set { molds = value; } }
        private List<Mold.Tile> uniqueTiles = new List<Mold.Tile>(); public List<Mold.Tile> UniqueTiles { get { return uniqueTiles; } set { uniqueTiles = value; } }
        // constructor
        public Animation(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            animationOffset = Bits.GetInt24(rom, 0x252000 + (index * 3)) - 0xC00000;
            int animationLength = Bits.GetShort(rom, animationOffset);
            buffer = Bits.GetBytes(rom, animationOffset, animationLength);
            //
            int offset = 2;
            ushort sequencePacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            ushort moldPacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            byte sequenceCount = buffer[offset++];
            byte moldCount = buffer[offset++];
            vramAllocation = (ushort)(buffer[offset] << 8); offset += 2;
            unknown = Bits.GetShort(buffer, offset);
            //
            offset = sequencePacketPointer;
            for (int i = 0; i < sequenceCount; i++)
            {
                Sequence tSequence = new Sequence();
                tSequence.Disassemble(buffer, offset);
                sequences.Add(tSequence);
                offset += 2;
            }
            offset = moldPacketPointer;
            for (int i = 0; i < moldCount; i++)
            {
                Mold tMold = new Mold();
                tMold.Disassemble(buffer, offset, uniqueTiles, index, animationOffset);
                molds.Add(tMold);
                offset += 2;
            }
        }
        public void Assemble()
        {
            // set sm to new byte with largest possible size
            byte[] temp = new byte[0x10000];
            // now start writing dynamic data
            temp[6] = (byte)sequences.Count;
            temp[7] = (byte)molds.Count;
            temp[8] = (byte)(vramAllocation >> 8);
            // Sequences
            int offset = 12; // where sequences begin
            Bits.SetShort(temp, 2, (ushort)0x0C);
            int fOffset = sequences.Count * 2 + 2;  // offset of first frame packet
            foreach (Sequence s in sequences)
            {
                if (s.Frames.Count != 0 && s.Active)
                {
                    Bits.SetShort(temp, offset, (ushort)(fOffset + offset));
                    fOffset += s.Frames.Count * 2 - 1;
                }
                else if (s.Active)
                {
                    Bits.SetShort(temp, offset, (ushort)(fOffset + offset));
                    fOffset -= 1;
                }
                else
                {
                    Bits.SetShort(temp, offset, 0xFFFF);
                    fOffset -= 2;
                }
                offset += 2;
            }
            Bits.SetShort(temp, offset, 0); offset += 2;
            foreach (Sequence s in sequences)
            {
                foreach (Sequence.Frame f in s.Frames)
                {
                    Bits.SetByte(temp, offset++, f.Duration);
                    Bits.SetByte(temp, offset++, f.Mold);
                }
                if (s.Active)
                    Bits.SetByte(temp, offset++, 0);
            }
            // Molds
            Bits.SetShort(temp, 4, (ushort)offset);
            int mOffset = molds.Count * 2 + 2;    // offset of first mold tilemap
            foreach (Mold m in molds)
            {
                if (index == 162 && mOffset == 0x132)
                    index = 162;
                if (m.Tiles.Count != 0)
                {
                    if (m.Gridplane)
                    {
                        Bits.SetShort(temp, offset, (ushort)(mOffset + offset + 0x8000));
                        Mold.Tile tile = m.Tiles[0];
                        if (tile.Subtile_bytes != null)
                            mOffset += tile.Length - 2;
                    }
                    else
                    {
                        Bits.SetShort(temp, offset, (ushort)(mOffset + offset));
                        mOffset += m.Recompress(mOffset + offset, molds).Length - 1;
                    }
                }
                else
                {
                    Bits.SetShort(temp, offset, (ushort)0xFFFF);
                    mOffset -= 2;
                }
                offset += 2;
            }
            Bits.SetShort(temp, offset += 2, 0);
            foreach (Mold m in molds)
            {
                if (m.Tiles.Count == 0) continue;
                if (m.Gridplane)
                {
                    Mold.Tile tile = m.Tiles[0];
                    if (tile.Subtile_bytes == null)
                        continue;
                    temp[offset] = (byte)(tile.Format & 3);
                    Bits.SetBit(temp, offset, 3, tile.Is16bit);
                    Bits.SetBit(temp, offset, 4, tile.YPlusOne == 1);
                    Bits.SetBit(temp, offset, 5, tile.YMinusOne == 1);
                    Bits.SetBit(temp, offset, 6, tile.Mirror);
                    Bits.SetBit(temp, offset++, 7, tile.Invert);
                    int size = tile.Is16bit ? tile.Length - 3 : tile.Length - 1;
                    // set bits for tiles that are 16bit
                    if (tile.Is16bit)
                    {
                        for (int i = 0; i < size; i++)
                            Bits.SetBit(temp, offset, i, tile.Subtile_bytes[i] >= 0x100);
                        offset += 2;
                    }
                    for (int i = 0; i < size; i++)
                        temp[offset++] = (byte)tile.Subtile_bytes[i];
                }
                else
                {
                    byte[] mold = m.Recompress(offset, molds);
                    mold.CopyTo(temp, offset);
                    offset += mold.Length;
                    temp[offset++] = 0;
                }
            }
            // finally, set the animation length
            Bits.SetShort(temp, 0, (ushort)offset);
            buffer = new byte[offset];
            Bits.SetBytes(buffer, 0, temp);
        }
        // public functions
        public void Refresh()
        {
            sequences = new List<Sequence>();
            molds = new List<Mold>();
            uniqueTiles = new List<Mold.Tile>();
            //
            int offset = 2;
            ushort sequencePacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            ushort moldPacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            byte sequenceCount = buffer[offset++];
            byte moldCount = buffer[offset++];
            vramAllocation = (ushort)(buffer[offset] << 8); offset += 2;
            unknown = Bits.GetShort(buffer, offset);
            //
            offset = sequencePacketPointer;
            for (int i = 0; i < sequenceCount; i++)
            {
                Sequence tSequence = new Sequence();
                tSequence.Disassemble(buffer, offset);
                sequences.Add(tSequence);
                offset += 2;
            }
            offset = moldPacketPointer;
            for (int i = 0; i < moldCount; i++)
            {
                Mold tMold = new Mold();
                tMold.Disassemble(buffer, offset, uniqueTiles, index, animationOffset);
                molds.Add(tMold);
                offset += 2;
            }
        }
        // accessor functions
        public int[] TilesetPixels()
        {
            int height = 16, x, y;
            Mold.Tile temp;
            height += (uniqueTiles.Count / 8) * 16;
            height += (uniqueTiles.Count % 8) != 0 ? 16 : 0;
            int[] pixels = new int[128 * height];
            for (int b = 0; b < height / 16; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if ((a + (b * 8)) >= uniqueTiles.Count) break;
                    temp = uniqueTiles[a + (b * 8)];
                    int[] theTile = temp.Get16x16TilePixels();
                    for (y = 0; y < 16; y++)
                    {
                        for (x = 0; x < 16; x++)
                            pixels[(((b * 16) + y) * 128) + ((a * 16) + x)] = theTile[(y * 16) + x];
                    }
                }
            }
            return pixels;
        }
        // universal functions
        public override void Clear()
        {
            vramAllocation = 2048;
            int moldCount = molds.Count;
            for (int i = 1; i < moldCount; i++)
                molds.RemoveAt(1);
            int tileCount = molds[0].Tiles.Count;
            for (int i = 1; i < tileCount; i++)
                molds[0].Tiles.RemoveAt(1);
            molds[0].Gridplane = false;
            uniqueTiles = new List<Mold.Tile>();
            if (molds[0].Tiles.Count > 0)
            {
                molds[0].Tiles[0] = molds[0].Tiles[0].New(false);
                uniqueTiles.Add(molds[0].Tiles[0]);
            }
            int sequenceCount = sequences.Count;
            for (int i = 1; i < sequenceCount; i++)
                sequences.RemoveAt(1);
            sequences[0].Frames = new List<Sequence.Frame>();
        }
    }
}
