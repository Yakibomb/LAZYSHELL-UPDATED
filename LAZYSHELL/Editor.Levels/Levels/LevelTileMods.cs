using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelTileMods
    {
        // universal variables
        private int index;
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables
        private List<Mod> mods = new List<Mod>();
        public List<Mod> Mods { get { return mods; } set { mods = value; } }
        public int Count { get { return mods.Count; } }
        // external selectors
        private Mod mod;
        public Mod MOD { get { return mod; } }
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
                    mod = (Mod)mods[value];
                    this.currentMod = value;
                }
            }
        }
        private int selectedMod;
        public int SelectedMod { get { return this.selectedMod; } set { selectedMod = value; } }
        private bool selectedB;
        public bool SelectedB { get { return this.selectedB; } set { selectedB = value; } }
        // public accessors
        public int X { get { return mod.X; } set { mod.X = value; } }
        public int Y { get { return mod.Y; } set { mod.Y = value; } }
        public int Width
        {
            get { return mod.Width; }
            set
            {
                mod.Width = value;
                mod.ImageA = null;
                mod.ImageB = null;
                mod.TilemapA = null;
                mod.TilemapB = null;
            }
        }
        public int Height
        {
            get { return mod.Height; }
            set
            {
                mod.Height = value;
                mod.ImageA = null;
                mod.ImageB = null;
                mod.TilemapA = null;
                mod.TilemapB = null;
            }
        }
        public bool Layer1 { get { return mod.Layer1; } set { mod.Layer1 = value; } }
        public bool Layer2 { get { return mod.Layer2; } set { mod.Layer2 = value; } }
        public bool Layer3 { get { return mod.Layer3; } set { mod.Layer3 = value; } }
        public bool B0b7 { get { return mod.B0b7; } set { mod.B0b7 = value; } }
        public bool Set { get { return mod.Set; } set { mod.Set = value; } }
        public byte[][] TilemapsA { get { return mod.TilemapsA; } set { mod.TilemapsA = value; } }
        public byte[][] TilemapsB { get { return mod.TilemapsB; } set { mod.TilemapsB = value; } }
        public LevelTilemap TilemapA { get { return mod.TilemapA; } set { mod.TilemapA = value; } }
        public LevelTilemap TilemapB { get { return mod.TilemapB; } set { mod.TilemapB = value; } }
        // constructor
        public LevelTileMods(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset;
            ushort offsetStart = 0;
            ushort offsetEnd = 0;
            Mod tMod;
            int pointerOffset = (index * 2) + 0x1D5EBD;
            offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (index == 0x1FF) offsetEnd = 0;
            if (offsetStart >= offsetEnd)
                return; // no exit fields for level
            offset = offsetStart + 0x1D0000;
            while (offset < offsetEnd + 0x1D0000)
            {
                tMod = new Mod();
                tMod.Disassemble(ref offset);
                mods.Add(tMod);
            }
        }
        public void Assemble(ref int offset)
        {
            int pointerOffset = (index * 2) + 0x1D5EBD;
            Bits.SetShort(rom, pointerOffset, (ushort)offset);
            foreach (Mod mod in mods)
                mod.Assemble(ref offset);
        }
        // accessor functions
        public void UpdateTilemaps()
        {
            foreach (Mod mod in mods)
                mod.UpdateTilemaps();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">In 16x16 tile units.</param>
        /// <param name="y">In 16x16 tile units.</param>
        /// <returns></returns>
        public bool WithinBounds(int x, int y)
        {
            if (mod != null)
                return mod.WithinBounds(x, y);
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">In 16x16 tile units.</param>
        /// <param name="y">In 16x16 tile units.</param>
        /// <returns></returns>
        public Point MousePosition(int x, int y)
        {
            return new Point(x - this.X, y - this.Y);
        }
        // list managers
        public void ClearTilemaps()
        {
            foreach (Mod mod in mods)
            {
                mod.ImageA = null;
                mod.ImageB = null;
                mod.TilemapA = null;
                mod.TilemapB = null;
            }
        }
        public void ClearImages()
        {
            foreach (Mod mod in mods)
            {
                mod.ImageA = null;
                mod.ImageB = null;
            }
        }
        public void RedrawTilemaps()
        {
            foreach (Mod mod in mods)
            {
                if (mod.TilemapA != null)
                    mod.TilemapA.RedrawTilemaps();
                if (mod.Set && mod.TilemapB != null)
                    mod.TilemapB.RedrawTilemaps();
            }
            ClearImages();
        }
        public void Reverse(int index)
        {
            mods.Reverse(index, 2);
        }
        public void Insert(int index, Point p, Level level, Tileset tileset)
        {
            Mod m = new Mod();
            m.X = (byte)p.X;
            m.Y = (byte)p.Y;
            m.TilemapA = new LevelTilemap(level, tileset, m, false);
            if (index < mods.Count)
                mods.Insert(index, m);
            else
                mods.Add(m);
        }
        public void Insert(int index, Mod copy)
        {
            if (index < mods.Count)
                mods.Insert(index, copy);
            else
                mods.Add(copy);
        }
        // universal functions
        public void Clear()
        {
            foreach (Mod mod in mods)
                mod.Clear();
        }
        // classes
        [Serializable()]
        public class Mod
        {
            // universal variables
            private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
            // class variables
            private int x;
            private int y;
            private int width = 1;
            private int height = 1;
            private bool layer1;
            private bool layer2;
            private bool layer3;
            private bool set;
            private bool b0b7;
            private byte[][] tilemapsA = new byte[3][];
            private byte[][] tilemapsB = new byte[3][];
            [NonSerialized()]
            private LevelTilemap tilemapA;
            [NonSerialized()]
            private LevelTilemap tilemapB;
            [NonSerialized()]
            private Bitmap imageA;
            [NonSerialized()]
            private Bitmap imageB;
            // private accessors
            private Rectangle region { get { return new Rectangle(location, size); } }
            private Point location { get { return new Point(x, y); } }
            private Size size { get { return new Size(width, height); } }
            // public accessors
            public LevelTilemap TilemapA { get { return tilemapA; } set { tilemapA = value; } }
            public LevelTilemap TilemapB { get { return tilemapB; } set { tilemapB = value; } }
            public Bitmap ImageA
            {
                get
                {
                    if (imageA == null && tilemapA != null)
                        imageA = Do.PixelsToImage(tilemapA.Pixels, width * 16, height * 16);
                    return imageA;
                }
                set { imageA = value; }
            }
            public Bitmap ImageB
            {
                get
                {
                    if (imageB == null && tilemapB != null)
                        imageB = Do.PixelsToImage(tilemapB.Pixels, width * 16, height * 16);
                    return imageB;
                }
                set { imageB = value; }
            }
            public int X { get { return x; } set { x = value; } }
            public int Y { get { return y; } set { y = value; } }
            public int Width
            {
                get { return width; }
                set
                {
                    width = value;
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < 2)
                            Array.Resize(ref tilemapsA[i], (width * height) * 2);
                        else
                            Array.Resize(ref tilemapsA[i], width * height);
                        if (set)
                            if (i < 2)
                                Array.Resize(ref tilemapsB[i], (width * height) * 2);
                            else
                                Array.Resize(ref tilemapsB[i], width * height);
                    }
                }
            }
            public int Height
            {
                get { return height; }
                set
                {
                    height = value;
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < 2)
                            Array.Resize(ref tilemapsA[i], (width * height) * 2);
                        else
                            Array.Resize(ref tilemapsA[i], width * height);
                        if (set)
                            if (i < 2)
                                Array.Resize(ref tilemapsB[i], (width * height) * 2);
                            else
                                Array.Resize(ref tilemapsB[i], width * height);
                    }
                }
            }
            public int Length
            {
                get
                {
                    int length = 3;
                    if (!(width == 1 && height == 1))
                        length++;
                    if (layer1)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    if (layer2)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    if (layer3)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    if (!set) return length;
                    if (layer1)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    if (layer2)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    if (layer3)
                    {
                        length += width * height;
                        length += (width * height) / 8;
                        if ((width * height) % 8 != 0)
                            length++;
                    }
                    return length;
                }
            }
            public bool Layer1 { get { return layer1; } set { layer1 = value; } }
            public bool Layer2 { get { return layer2; } set { layer2 = value; } }
            public bool Layer3 { get { return layer3; } set { layer3 = value; } }
            public bool Set { get { return set; } set { set = value; } }
            public byte[][] TilemapsA { get { return tilemapsA; } set { tilemapsA = value; } }
            public byte[][] TilemapsB { get { return tilemapsB; } set { tilemapsB = value; } }
            public bool B0b7 { get { return b0b7; } set { b0b7 = value; } }
            // constructor
            public Mod()
            {
            }
            // assemblers
            public void Disassemble(ref int offset)
            {
                this.b0b7 = (rom[offset] & 0x80) == 0x80;
                this.x = rom[offset++] & 0x3F;
                bool one = (rom[offset] & 0x80) == 0x80;
                this.y = rom[offset++] & 0x3F;
                if (one)
                    width = 1;
                else
                    width = (rom[offset] & 0x1F) + 1;
                layer1 = (rom[offset] & 0x20) == 0x20;
                layer2 = (rom[offset] & 0x40) == 0x40;
                layer3 = (rom[offset++] & 0x80) == 0x80;
                if (one)
                    height = 1;
                else
                {
                    height = (rom[offset] & 0x3F) + 1;
                    set = (rom[offset++] & 0x80) == 0x80;
                }
                byte upper = 0;
                tilemapsA[0] = new byte[(width * height) * 2];
                if (layer1)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsA[0][c++] = rom[offset++];
                            tilemapsA[0][c++] = (byte)((upper >> ((i % 9) - 1)) & 0x01);
                        }
                    }
                }
                tilemapsA[1] = new byte[(width * height) * 2];
                if (layer2)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsA[1][c++] = rom[offset++];
                            tilemapsA[1][c++] = (byte)((upper >> ((i % 9) - 1)) & 0x01);
                        }
                    }
                }
                tilemapsA[2] = new byte[width * height];
                if (layer3)
                {
                    for (int i = 0, c = 0; c < width * height; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsA[2][c++] = rom[offset++];
                        }
                    }
                }
                if (!set)
                    return;
                tilemapsB[0] = new byte[(width * height) * 2];
                if (layer1)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsB[0][c++] = rom[offset++];
                            tilemapsB[0][c++] = (byte)((upper >> ((i % 9) - 1)) & 0x01);
                        }
                    }
                }
                tilemapsB[1] = new byte[(width * height) * 2];
                if (layer2)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsB[1][c++] = rom[offset++];
                            tilemapsB[1][c++] = (byte)((upper >> ((i % 9) - 1)) & 0x01);
                        }
                    }
                }
                tilemapsB[2] = new byte[width * height];
                if (layer3)
                {
                    for (int i = 0, c = 0; c < width * height; i++)
                    {
                        if (i % 9 == 0)
                            upper = rom[offset++];
                        else
                        {
                            tilemapsB[2][c++] = rom[offset++];
                        }
                    }
                }
            }
            public void Assemble(ref int offset)
            {
                rom[offset] = (byte)this.x;
                Bits.SetBit(rom, offset++, 7, b0b7);
                rom[offset] = (byte)this.y;
                Bits.SetBit(rom, offset++, 7, this.width == 1 && this.height == 1);
                rom[offset] = (byte)(this.width - 1);
                Bits.SetBit(rom, offset, 5, this.layer1);
                Bits.SetBit(rom, offset, 6, this.layer2);
                Bits.SetBit(rom, offset++, 7, this.layer3);
                if (!(this.height == 1 && this.width == 1))
                {
                    rom[offset] = (byte)(this.height - 1);
                    Bits.SetBit(rom, offset++, 7, this.set);
                }
                if (layer1)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset] = tilemapsA[0][c++];
                            rom[offset++ - (i % 9)] |= (byte)(tilemapsA[0][c++] << ((i % 9) - 1));
                        }
                    }
                }
                if (layer2)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset] = tilemapsA[1][c++];
                            rom[offset++ - (i % 9)] |= (byte)(tilemapsA[1][c++] << ((i % 9) - 1));
                        }
                    }
                }
                if (layer3)
                {
                    for (int i = 0, c = 0; c < width * height; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset++] = tilemapsA[2][c++];
                        }
                    }
                }
                if (!set)
                    return;
                if (layer1)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset] = tilemapsB[0][c++];
                            rom[offset++ - (i % 9)] |= (byte)(tilemapsB[0][c++] << ((i % 9) - 1));
                        }
                    }
                }
                if (layer2)
                {
                    for (int i = 0, c = 0; c < (width * height) * 2; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset] = tilemapsB[1][c++];
                            rom[offset++ - (i % 9)] |= (byte)(tilemapsB[1][c++] << ((i % 9) - 1));
                        }
                    }
                }
                if (layer3)
                {
                    for (int i = 0, c = 0; c < width * height; i++)
                    {
                        if (i % 9 == 0)
                            rom[offset++] = 0;
                        else
                        {
                            rom[offset++] = tilemapsB[2][c++];
                        }
                    }
                }
            }
            // class functions
            public bool WithinBounds(int x, int y)
            {
                if (x >= this.x && x < this.x + this.width &&
                    y >= this.y && y < this.y + this.height)
                    return true;
                return false;
            }
            public void UpdateTilemaps()
            {
                for (int layer = 0; layer < 3; layer++)
                {
                    if (tilemapsA[layer] == null) continue;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (layer < 2)
                            {
                                tilemapsA[layer][y * width + x] = tilemapA.Tilemaps_Bytes[layer][y * width + x];
                                if (set)
                                    tilemapsB[layer][y * width + x] = tilemapB.Tilemaps_Bytes[layer][y * width + x];
                            }
                        }
                    }
                }
            }
            // universal functions
            public void Clear()
            {
                if (tilemapsA[0] != null)
                    Array.Clear(tilemapsA[0], 0, tilemapsA[0].Length);
                if (tilemapsA[1] != null)
                    Array.Clear(tilemapsA[1], 0, tilemapsA[1].Length);
                if (tilemapsA[2] != null)
                    Array.Clear(tilemapsA[2], 0, tilemapsA[2].Length);
            }
            // spawning
            public Mod Copy(Level level, Tileset tileset)
            {
                Mod copy = new Mod();
                copy.B0b7 = b0b7;
                copy.Height = height;
                copy.Layer1 = layer1;
                copy.Layer2 = layer2;
                copy.Layer3 = layer3;
                copy.Set = set;
                copy.TilemapsA[0] = Bits.Copy(tilemapsA[0]);
                copy.TilemapsA[1] = Bits.Copy(tilemapsA[1]);
                copy.TilemapsA[2] = Bits.Copy(tilemapsA[2]);
                copy.TilemapA = new LevelTilemap(level, tileset, this, false);
                if (this.set)
                {
                    copy.TilemapsB[0] = Bits.Copy(tilemapsB[0]);
                    copy.TilemapsB[1] = Bits.Copy(tilemapsB[1]);
                    copy.TilemapsB[2] = Bits.Copy(tilemapsB[2]);
                    copy.TilemapB = new LevelTilemap(level, tileset, this, true);
                }
                copy.Width = width;
                copy.X = x;
                copy.Y = y;
                return copy;
            }
        }
    }
}
