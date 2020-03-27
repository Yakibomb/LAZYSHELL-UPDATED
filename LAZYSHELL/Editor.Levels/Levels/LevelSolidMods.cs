using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelSolidMods
    {
        // universal variables
        private int index;
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables
        private List<LevelMod> mods = new List<LevelMod>();
        public List<LevelMod> Mods { get { return mods; } set { mods = value; } }
        public int Count { get { return mods.Count; } }
        // external selectors
        private LevelMod mod; public LevelMod MOD { get { return mod; } }
        private int currentMod = 0;
        public int CurrentMod
        {
            get
            {
                return this.currentMod;
            }
            set
            {
                if (this.mods.Count > value)
                {
                    mod = (LevelMod)mods[value];
                    this.currentMod = value;
                }
            }
        }
        private int selectedMod;
        public int SelectedMod { get { return this.selectedMod; } set { selectedMod = value; } }
        // public accessors
        public int X
        {
            get { return mod.X; }
            set
            {
                mod.X = value;
                mod.Image = null;
                mod.Pixels = null;
            }
        }
        public int Y
        {
            get { return mod.Y; }
            set
            {
                mod.Y = value;
                mod.Image = null;
                mod.Pixels = null;
            }
        }
        public int Width
        {
            get { return mod.Width; }
            set
            {
                mod.Width = value;
                mod.Image = null;
                mod.Pixels = null;
            }
        }
        public int Height
        {
            get { return mod.Height; }
            set
            {
                mod.Height = value;
                mod.Image = null;
                mod.Pixels = null;
            }
        }
        // constructor
        public LevelSolidMods(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int pointerOffset = (index * 2) + 0x1D8DB0;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (index == 0x1FF) offsetEnd = 0;
            // no exit fields for level
            if (offsetStart >= offsetEnd)
                return;
            //
            int offset = offsetStart + 0x1D0000;
            if (index == 84) index = 84;
            while (offset < offsetEnd + 0x1D0000)
            {
                LevelMod tMod = new LevelMod();
                tMod.Disassemble(ref offset);
                mods.Add(tMod);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset">The offset in the byte[0x20C2] array.</param>
        /// <returns></returns>
        public void Assemble(ref int offset)
        {
            int pointerOffset = (index * 2) + 0x1D8DB0;
            Bits.SetShort(rom, pointerOffset, (ushort)offset);
            foreach (LevelMod mod in mods)
                mod.Assemble(rom, ref offset);
        }
        // list managers
        public void ClearTilemaps()
        {
            foreach (LevelMod mod in mods)
            {
                mod.Image = null;
                mod.Pixels = null;
            }
        }
        public void ClearImages()
        {
            foreach (LevelMod mod in mods)
            {
                mod.Image = null;
            }
        }
        public void ReverseMod(int index)
        {
            mods.Reverse(index, 2);
        }
        public void Insert(int index, Point p)
        {
            LevelMod m = new LevelMod();
            m.X = (byte)p.X;
            m.Y = (byte)p.Y;
            m.Pixels = Solidity.Instance.GetTilemapPixels(m);
            if (index < mods.Count)
                mods.Insert(index, m);
            else
                mods.Add(m);
        }
        public void Insert(int index, LevelMod copy)
        {
            if (index < mods.Count)
                mods.Insert(index, copy);
            else
                mods.Add(copy);
        }
        public void CopyToTiles()
        {
            mod.CopyToTiles();
        }
        // class functions
        public bool WithinBounds(int offset)
        {
            if (mod != null)
                return mod.WithinBounds(offset);
            else
                return false;
        }
        // universal functions
        public void Clear()
        {
            foreach (LevelMod mod in mods)
                mod.Clear();
        }
        // classes
        [Serializable()]
        public class LevelMod : Tilemap
        {
            // universal variables
            private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
            // class variables
            private int x;
            private int y;
            private int width = 1;
            private int height = 1;
            private bool b0b7;
            private byte[] tiles = new byte[2];
            private byte[] tilemap_Bytes;
            [NonSerialized()]
            private int[] pixels;
            private Bitmap image;
            // public accessors
            public int X
            {
                get { return x; }
                set
                {
                    x = value;
                    CopyToTilemap();
                }
            }
            public int Y
            {
                get { return y; }
                set
                {
                    y = value;
                    CopyToTilemap();
                }
            }
            public int Width
            {
                get { return width; }
                set
                {
                    width = value;
                    Array.Resize(ref tiles, (width * height) * 2);
                    CopyToTilemap();
                }
            }
            public int Height
            {
                get { return height; }
                set
                {
                    height = value;
                    Array.Resize(ref tiles, (width * height) * 2);
                    CopyToTilemap();
                }
            }
            public int Length
            {
                get
                {
                    int length = 2;
                    if (!(width == 1 && height == 1))
                        length++;
                    length += width * height;
                    length += (width * height) / 4;
                    if ((width * height) % 4 != 0)
                        length++;
                    return length;
                }
            }
            public override int Width_p { get { return Width * 16; } set { Width = value / 16; } }
            public override int Height_p { get { return Height * 16; } set { Height = value / 16; } }
            public bool B0b7 { get { return b0b7; } set { b0b7 = value; } }
            public byte[] Tiles { get { return tiles; } set { tiles = value; } }
            public override byte[] Tilemap_Bytes
            {
                get
                {
                    if (tilemap_Bytes == null)
                        return new byte[0x20C2];
                    return tilemap_Bytes;
                }
                set
                {
                    tilemap_Bytes = value;
                    CopyToTiles();
                }
            }
            public override byte[][] Tilemaps_Bytes
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
            public override Tile[] Tilemap_Tiles
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
            public override Tile[][] Tilemaps_Tiles
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
            public override int[] Pixels { get { return pixels; } set { pixels = value; } }
            public override Bitmap Image
            {
                get
                {
                    if (image == null && pixels != null)
                        image = Do.PixelsToImage(pixels, 1024, 1024);
                    return image;
                }
                set { image = value; }
            }
            // assemblers
            public void Disassemble(ref int offset)
            {
                b0b7 = (rom[offset] & 0x80) == 0x80;
                this.x = rom[offset++] & 0x7F;
                bool one = (rom[offset] & 0x80) == 0x80;
                this.y = rom[offset++] & 0x7F;
                if (one)
                {
                    width = 1;
                    height = 1;
                }
                else
                {
                    width = (rom[offset] & 0x0F) + 1;
                    height = (rom[offset++] >> 4) + 1;
                }
                tiles = new byte[(width * height) * 2];
                byte upper = 0;
                for (int i = 0, c = 0; c < (width * height) * 2; i++)
                {
                    if (i % 5 == 0)
                        upper = rom[offset++];
                    else
                    {
                        tiles[c++] = rom[offset++];
                        tiles[c++] = (byte)((upper >> (((i % 5) - 1) * 2)) & 0x03);
                    }
                }
                CopyToTilemap();
            }
            public override void Assemble()
            {
                throw new NotImplementedException();
            }
            public void Assemble(byte[] data, ref int offset)
            {
                data[offset] = (byte)this.x;
                Bits.SetBit(data, offset++, 7, b0b7);
                data[offset] = (byte)this.y;
                Bits.SetBit(data, offset++, 7, width == 1 && height == 1);
                if (!(width == 1 && height == 1))
                {
                    data[offset] = (byte)(width - 1);
                    data[offset++] |= (byte)((height - 1) << 4);
                }
                for (int i = 0, c = 0; c < (width * height) * 2; i++)
                {
                    if (i % 5 == 0)
                        data[offset++] = 0;
                    else
                    {
                        data[offset] = tiles[c++];
                        data[offset++ - (i % 5)] |= (byte)(tiles[c++] << (((i % 5) - 1) * 2));
                    }
                }
            }
            // accessor functions
            public override int[] GetPixels(int layer, Point location, Size size)
            {
                throw new NotImplementedException();
            }
            public override int[] GetPixels(Point location, Size size)
            {
                throw new NotImplementedException();
            }
            public override int[] GetPriority1Pixels()
            {
                throw new NotImplementedException();
            }
            public override int GetPixelLayer(int x, int y)
            {
                return 0;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value">The tile index to change to.</param>
            /// <param name="x">The isometric X coord relative to a full solidity map.</param>
            /// <param name="y">The isometric Y coord relative to a full solidity map.</param>
            public override int GetTileNum(int index)
            {
                return Bits.GetShort(tilemap_Bytes, index * 2);
            }
            public override int GetTileNum(int layer, int x, int y)
            {
                throw new NotImplementedException();
            }
            public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
            {
                throw new NotImplementedException();
            }
            public override void SetTileNum()
            {
            }
            public override void SetTileNum(int tilenum, int layer, int x, int y)
            {
                int offset = 0x41 * (y / 2);
                if (y % 2 != 0)
                    offset += 0x21;
                offset += x;
                offset *= 2;
                Bits.SetShort(tilemap_Bytes, offset, (ushort)tilenum);
            }
            // class functions
            public void CopyToTilemap()
            {
                tilemap_Bytes = new byte[0x20C2];
                int startOffset = 0x41 * (this.y / 2);
                if (y % 2 != 0)
                    startOffset += 0x21;
                startOffset += x;
                startOffset *= 2;
                for (int i = 0, c = 0; c < tiles.Length; i += 0x42)
                {
                    if (c != 0 && (c / 2) % width == 0)
                        i = ((c / 2) / width) * 0x40;
                    if (c >= tiles.Length || startOffset + i >= tilemap_Bytes.Length) break;
                    tilemap_Bytes[startOffset + i] = tiles[c++];
                    tilemap_Bytes[startOffset + i + 1] = tiles[c++];
                }
            }
            public void CopyToTiles()
            {
                int startOffset = 0x41 * (this.y / 2);
                if (y % 2 != 0)
                    startOffset += 0x21;
                startOffset += x;
                startOffset *= 2;
                for (int i = 0, c = 0; c < tiles.Length; i += 0x42)
                {
                    if (c != 0 && (c / 2) % width == 0)
                        i = ((c / 2) / width) * 0x40;
                    if (c >= tiles.Length || startOffset + i >= tilemap_Bytes.Length) break;
                    tiles[c++] = tilemap_Bytes[startOffset + i];
                    tiles[c++] = tilemap_Bytes[startOffset + i + 1];
                }
            }
            public override void RedrawTilemaps()
            {
                throw new NotImplementedException();
            }
            public bool WithinBounds(int offset)
            {
                int startOffset = 0x41 * (this.y / 2);
                if (y % 2 != 0)
                    startOffset += 0x21;
                startOffset += x;
                startOffset *= 2;
                // check all offsets to see if parameter is one of them
                for (int i = 0, c = 0; c < tiles.Length; i += 0x42)
                {
                    if (c != 0 && (c / 2) % width == 0)
                        i = ((c / 2) / width) * 0x40;
                    if (c >= tiles.Length || startOffset + i >= tilemap_Bytes.Length)
                        return false;
                    if (startOffset + i == offset)
                        return true;
                    c += 2;
                }
                return false;
            }
            // universal functions
            public void Clear()
            {
                Array.Clear(tiles, 0, tiles.Length);
            }
            // spawning
            public LevelMod Copy()
            {
                LevelMod copy = new LevelMod();
                copy.Tiles = Bits.Copy(tiles);
                copy.Pixels = Bits.Copy(pixels);
                copy.Tilemap_Bytes = Bits.Copy(tilemap_Bytes);
                copy.Width = width;
                copy.Height = height;
                copy.X = x;
                copy.Y = y;
                return copy;
            }
        }
    }
}
