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
    public class E_Animation : Element
    {
        #region variables
        // universal variales
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // properties
        private byte[] buffer; public byte[] BUFFER { get { return buffer; } set { buffer = value; } }    // sequence mold data       
        private int animationOffset; public int AnimationOffset { get { return animationOffset; } set { index = value; } }
        private byte width; public byte Width { get { return width; } set { width = value; } }
        private byte height; public byte Height { get { return height; } set { height = value; } }
        private ushort codec; public ushort Codec { get { return codec; } set { codec = value; } }
        // collections
        private List<E_Sequence> sequences = new List<E_Sequence>();
        public List<E_Sequence> Sequences { get { return sequences; } set { sequences = value; } }
        private List<E_Mold> molds = new List<E_Mold>();
        public List<E_Mold> Molds { get { return molds; } set { molds = value; } }
        // tileset
        private int tilesetLength; public int TilesetLength { get { return tilesetLength; } set { tilesetLength = value; } }
        private byte[] tileset_bytes; public byte[] Tileset_bytes { get { return tileset_bytes; } set { tileset_bytes = value; } }
        private E_Tileset tileset_tiles; public E_Tileset Tileset_tiles { get { return tileset_tiles; } set { tileset_tiles = value; } }
        // graphics
        private byte[] graphicSet; public byte[] GraphicSet { get { return graphicSet; } set { graphicSet = value; } }
        private int graphicSetLength;
        public int GraphicSetLength { get { return graphicSetLength; } set { graphicSetLength = value; } }
        // palettes
        private PaletteSet paletteSet; public PaletteSet PaletteSet { get { return paletteSet; } set { paletteSet = value; } }
        private ushort paletteSetLength;
        public ushort PaletteSetLength { get { return paletteSetLength; } set { paletteSetLength = value; } }
        #endregion
        // constructor
        public E_Animation(int index)
        {
            this.index = index;
            Disassemble();
        }
        #region functions
        // assemblers
        private void Disassemble()
        {
            animationOffset = Bits.GetInt24(rom, 0x252C00 + (index * 3)) - 0xC00000;
            ushort animationLength = Bits.GetShort(rom, animationOffset);
            buffer = Bits.GetBytes(rom, animationOffset, Bits.GetShort(rom, animationOffset));
            //
            int offset = 2;
            ushort graphicSetPointer = Bits.GetShort(buffer, offset); offset += 2;
            ushort paletteSetPointer = Bits.GetShort(buffer, offset); offset += 2;
            ushort sequencePacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            ushort moldPacketPointer = Bits.GetShort(buffer, offset); offset += 2;
            // skip 2 unknown bytes
            offset += 2;
            //
            width = buffer[offset++];
            height = buffer[offset++];
            codec = Bits.GetShort(buffer, offset); offset += 2;
            //
            int tileSetPointer = Bits.GetShort(buffer, offset);
            graphicSetLength = paletteSetPointer - graphicSetPointer;
            graphicSet = new byte[0x2000];
            Buffer.BlockCopy(buffer, graphicSetPointer, graphicSet, 0, graphicSetLength);
            paletteSetLength = (ushort)(tileSetPointer - paletteSetPointer);
            paletteSet = new PaletteSet(buffer, 0, paletteSetPointer, 8, 16, 32);
            tilesetLength = sequencePacketPointer - tileSetPointer - 2;
            tileset_bytes = new byte[64 * 4 * 2 * 4];
            Buffer.BlockCopy(buffer, tileSetPointer, tileset_bytes, 0, tilesetLength);
            //
            offset = sequencePacketPointer;
            for (int i = 0; Bits.GetShort(buffer, offset) != 0x0000; i++)
            {
                E_Sequence tSequence = new E_Sequence();
                tSequence.Disassemble(buffer, offset);
                sequences.Add(tSequence);
                offset += 2;
            }
            offset = moldPacketPointer;
            ushort end = 0;
            for (int i = 0; Bits.GetShort(buffer, offset) != 0x0000; i++)
            {
                if (Bits.GetShort(buffer, offset + 2) == 0x0000)
                    end = animationLength;
                else
                    end = Bits.GetShort(buffer, offset + 2);
                E_Mold tMold = new E_Mold();
                tMold.Disassemble(buffer, offset, end);
                molds.Add(tMold);
                offset += 2;
            }
        }
        public int Assemble()
        {
            int size = this.buffer.Length;
            // set sm to new byte with largest possible size
            byte[] temp = new byte[0x10000];
            // not dynamic, can set before others
            temp[12] = width;
            temp[13] = height;
            Bits.SetShort(temp, 14, codec);
            // now start writing dynamic data
            int offset = 18;
            // Graphics
            Bits.SetShort(temp, 2, 0x12);
            Buffer.BlockCopy(graphicSet, 0, temp, 18, graphicSetLength);
            offset += graphicSetLength;
            // Palettes
            Bits.SetShort(temp, 4, (ushort)offset);
            paletteSet.Assemble(temp, offset);
            offset += paletteSetLength;
            // Tileset
            Bits.SetShort(temp, 16, (ushort)offset);
            int tilesetoffset = offset;
            foreach (Tile t in tileset_tiles.Tileset)
            {
                if (t.Index >= tilesetLength / 8)
                    break;
                if (t.Index > 0 && t.Index % 8 == 0)
                    offset += 32;
                for (int i = 0; i < 4; i++)
                {
                    Bits.SetShort(temp, offset, (byte)t.Subtiles[i].Index); offset++;
                    Bits.SetBit(temp, offset, 5, t.Subtiles[i].Priority1);
                    Bits.SetBit(temp, offset, 6, t.Subtiles[i].Mirror);
                    Bits.SetBit(temp, offset, 7, t.Subtiles[i].Invert);
                    if (i % 2 == 0) 
                        offset++;
                    else if (i == 1)
                        offset += 29;
                    else if (i == 3)
                        offset -= 31;
                }
            }
            Buffer.BlockCopy(temp, tilesetoffset, tileset_bytes, 0, tileset_bytes.Length);
            //
            offset += 32;
            Bits.SetShort(temp, offset, 0xFFFF); offset += 2;
            // Sequences
            Bits.SetShort(temp, 6, (ushort)offset);
            int fOffset = sequences.Count * 2 + 2;  // offset of first frame packet
            foreach (E_Sequence s in sequences)
            {
                if (s.Frames.Count != 0)
                {
                    Bits.SetShort(temp, offset, (ushort)(fOffset + offset));
                    fOffset += s.Frames.Count * 2 + 1;
                }
                else
                    Bits.SetShort(temp, offset, 0xFFFF);
                offset += 2;
            }
            Bits.SetShort(temp, offset, 0); offset += 2;
            foreach (E_Sequence s in sequences)
            {
                foreach (E_Sequence.Frame f in s.Frames)
                {
                    Bits.SetByte(temp, offset, f.Duration); offset++;
                    Bits.SetByte(temp, offset, f.Mold); offset++;
                }
                Bits.SetByte(temp, offset, 0); offset++;
            }
            // Molds
            Bits.SetShort(temp, 8, (ushort)offset);
            int mOffset = molds.Count * 2 + 2;    // offset of first mold tilemap
            foreach (E_Mold m in molds)
            {
                if (!m.Empty)
                {
                    Bits.SetShort(temp, offset, (ushort)(mOffset + offset));
                    mOffset += m.Recompress(m.Mold, width, height).Length - 2;
                }
                else
                    Bits.SetShort(temp, offset, 0xFFFF);
                offset += 2;
            }
            Bits.SetShort(temp, offset, 0); offset += 2;
            foreach (E_Mold m in molds)
            {
                if (!m.Empty)
                {
                    byte[] mold = m.Recompress(m.Mold, width, height);
                    Buffer.BlockCopy(mold, 0, temp, offset, mold.Length);
                    offset += mold.Length;
                }
            }
            // finally, set the animation length
            Bits.SetShort(temp, 0, (ushort)offset);
            this.buffer = new byte[offset];
            Bits.SetBytes(this.buffer, 0, temp);
            //Do.Export(sm, "test.bin");
            return size - this.buffer.Length;
        }
        // universal functions
        public override void Clear()
        {
            foreach (E_Sequence s in sequences)
                s.Frames = new List<E_Sequence.Frame>();
            int moldCount = molds.Count;
            for (int i = 1; i < moldCount; i++)
                molds.RemoveAt(1);
            tileset_bytes = new byte[tileset_bytes.Length];
            paletteSetLength = 32;
            graphicSetLength = 32;
            tilesetLength = 64;
            graphicSet = new byte[graphicSet.Length];
            paletteSet = new PaletteSet(new byte[256], 0, 0, 8, 16, 32);
            codec = 0;
            width = 1;
            height = 1;
            foreach (Tile tile in tileset_tiles.Tileset)
                for (int i = 0; i < 4; i++)
                    tile.Subtiles[i] = new Subtile(0, new byte[0x20], 0, new int[16], false, false, false, codec == 1);
        }
        #endregion
    }
}
