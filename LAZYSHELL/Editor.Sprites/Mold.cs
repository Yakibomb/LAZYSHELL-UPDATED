using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    [Serializable()]
    public class Mold
    {
        private byte[] buffer;
        // variables
        private bool gridplane;
        private List<Tile> tiles = new List<Tile>();
        public List<Tile> Tiles { get { return tiles; } set { tiles = value; } }
        public bool Gridplane
        {
            get { return gridplane; }
            set
            {
                gridplane = value;
                foreach (Tile t in tiles)
                    t.Gridplane = value;
            }
        }
        public int Length
        {
            get
            {
                int size = 0;
                foreach (Tile t in tiles)
                {
                    if (t.Gridplane)
                    {
                        if (t.Is16bit) size += 2;
                        switch (t.Format)
                        {
                            case 0: size += 9; break;
                            case 1: size += 12; break;
                            case 2: size += 12; break;
                            case 3: size += 16; break;
                            default: goto case 0;
                        }
                    }
                    else
                    {
                        size += 3;
                        for (int i = 0; i < 4; i++)
                        {
                            if (t.Subtile_bytes[i] != 0)
                                size += t.Format == 1 ? 2 : 1;
                        }
                    }
                }
                return size;
            }
        }
        // drawing
        private int[] moldTilesPerPixel;
        /// <summary>
        /// A tile index is assigned to each pixel;
        /// </summary>
        public int[] MoldTilesPerPixel { get { return moldTilesPerPixel; } set { moldTilesPerPixel = value; } }
        // assemblers
        public void Disassemble(byte[] buffer, int offset, List<Tile> uniqueTiles, int parent, int parentoffset)
        {
            if (Bits.GetShort(buffer, offset) == 0xFFFF)
                return;
            this.buffer = buffer;
            Tile tTile;
            gridplane = (buffer[offset + 1] & 0x80) == 0x80;
            ushort tilePacketPointer = (ushort)(Bits.GetShort(buffer, offset) & 0x7FFF);
            offset = tilePacketPointer;
            //
            if (gridplane)
            {
                tTile = new Tile();
                tTile.Disassemble(buffer, offset, gridplane);
                tiles.Add(tTile);
            }
            else
            {
                for (int i = 0; buffer[offset] != 0; i++)
                {
                    // copy
                    if ((buffer[offset] & 0x03) == 2)
                    {
                        int temp = offset;
                        int y = buffer[offset + 1];
                        int x = buffer[offset + 2];
                        bool mirror = (buffer[offset] & 0x04) == 0x04;
                        bool invert = (buffer[offset] & 0x08) == 0x08;
                        if (mirror)
                            mirror = true;
                        offset = Bits.GetShort(buffer, offset + 3);   // get offset of copy
                        if (offset > 0x7FFF)
                        {
                            ErrorMessage("attempted to read a gridplane mold as a copy.", parent, offset, parentoffset);
                            break;
                        }
                        for (int c = 0; c < buffer[temp] >> 4; c++)   // keep adding tiles until reach max
                        {
                            if (offset > buffer.Length)
                            {
                                ErrorMessage("offset was out of bounds of the animation data.", parent, offset, parentoffset);
                                break;
                            }
                            tTile = new Tile();
                            if ((buffer[offset] & 0x03) == 2)
                            {
                                ErrorMessage("attempted to read a copy within a copy.", parent, offset, parentoffset);
                                break;
                            }
                            else if ((buffer[offset] & 0x03) == 1)
                            {
                                tTile.Disassemble(buffer, offset, gridplane);
                                offset += tTile.Length;
                            }
                            else
                            {
                                tTile.Disassemble(buffer, offset, gridplane);
                                offset += tTile.Length;
                            }
                            tTile.X = (byte)(x + tTile.X);
                            tTile.Y = (byte)(y + tTile.Y);
                            tiles.Add(tTile);
                        }
                        offset = temp;
                        offset += 5;
                    }
                    // 16-bit
                    else if ((buffer[offset] & 0x03) == 1)
                    {
                        tTile = new Tile();
                        tTile.Disassemble(buffer, offset, gridplane);
                        offset += tTile.Length;
                        tiles.Add(tTile);
                        uniqueTiles.Add(tTile);
                    }
                    else
                    {
                        tTile = new Tile();
                        tTile.Disassemble(buffer, offset, gridplane);
                        offset += tTile.Length;
                        tiles.Add(tTile);
                        uniqueTiles.Add(tTile);
                    }
                }
            }
        }
        // class functions
        private void ErrorMessage(string error, int parent, int offset, int parentoffset)
        {
            NewMessage.Show("LAZYSHELL++", "Error in animation #" + parent + " @ offset $" + offset.ToString("X4") + ". " +
                "Data is corrupt: " + error + " " + 
                "The sprites editor will continue to load anyways.\n\nAnimation Data:",
                Do.BitArrayToString(buffer, 16, true, true, parentoffset), "Lucida Console");
        }
        public byte[] Recompress(int baseOffset, List<Mold> molds)
        {
            byte[] mold = new byte[0x10000];
            int offset = 0;
            int thisCounter = 0;    // must be outside loop to avoid resetting
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile tile = (Tile)tiles[i];
                #region first compare to find copies
                int counter = 0;
                int copySize = 0;
                Tile tileA = new Tile();
                Tile thisTileA = new Tile();
                Tile tileB = new Tile();
                Tile thisTileB = new Tile();
                int copyOffset = 0;
                int copyDiffX = 0;
                int copyDiffY = 0;
                // look through each mold for copies
                foreach (Mold m in molds)
                {
                    // cancel if we've reached the same mold
                    if (m == this) break;
                    // skip if gridplane; not copyable
                    if (m.Gridplane) continue;
                    counter = 0;
                    // look through all mold's tiles for copies
                    while (counter + 1 < m.Tiles.Count && thisCounter + 1 < tiles.Count)
                    {
                        copySize = 0;
                        tileA = (Tile)m.Tiles[counter];
                        thisTileA = (Tile)tiles[thisCounter];
                        tileB = (Tile)m.Tiles[counter + 1];
                        thisTileB = (Tile)tiles[thisCounter + 1];
                        copyOffset = tileA.TileOffset;
                        // tile's offset wasn't set, thus not a viable tile to copy from
                        if (copyOffset == 0)
                        {
                            counter++; continue;
                        }
                        copyDiffX = thisTileA.X - tileA.X;
                        copyDiffY = thisTileA.Y - tileA.Y;
                        // first check to see if going to be making a copy of more than one, regardless of active quadrants
                        if (tileB.TileOffset != 0 &&
                            thisTileA.Subtile_bytes[0] == tileA.Subtile_bytes[0] &&
                            thisTileA.Subtile_bytes[1] == tileA.Subtile_bytes[1] &&
                            thisTileA.Subtile_bytes[2] == tileA.Subtile_bytes[2] &&
                            thisTileA.Subtile_bytes[3] == tileA.Subtile_bytes[3] &&
                            thisTileA.Mirror == tileA.Mirror &&
                            thisTileA.Invert == tileA.Invert &&
                            thisTileB.Subtile_bytes[0] == tileB.Subtile_bytes[0] &&
                            thisTileB.Subtile_bytes[1] == tileB.Subtile_bytes[1] &&
                            thisTileB.Subtile_bytes[2] == tileB.Subtile_bytes[2] &&
                            thisTileB.Subtile_bytes[3] == tileB.Subtile_bytes[3] &&
                            thisTileB.Mirror == tileB.Mirror &&
                            thisTileB.Invert == tileB.Invert &&
                            copyDiffX == thisTileB.X - tileB.X &&
                            copyDiffY == thisTileB.Y - tileB.Y)
                        {
                            // v3.8: set TileOffset to 0 to prevent later molds from using as a copy
                            thisTileA.TileOffset = 0;
                            thisTileB.TileOffset = 0;
                            // we will be making a copy of at least 2
                            copySize = 2;
                            // now keep moving to find more copies
                            counter += 2;
                            thisCounter += 2;
                            // cancel if at end of first mold's tiles
                            if (counter >= m.Tiles.Count ||
                                thisCounter >= tiles.Count)
                                break;
                            Tile tileC = (Tile)m.Tiles[counter];
                            Tile thisTileC = (Tile)tiles[thisCounter];
                            while (copySize < 15 &&
                                   thisTileC.Subtile_bytes[0] == tileC.Subtile_bytes[0] &&
                                   thisTileC.Subtile_bytes[1] == tileC.Subtile_bytes[1] &&
                                   thisTileC.Subtile_bytes[2] == tileC.Subtile_bytes[2] &&
                                   thisTileC.Subtile_bytes[3] == tileC.Subtile_bytes[3] &&
                                   thisTileC.Mirror == tileC.Mirror &&
                                   copyDiffX == thisTileC.X - tileC.X &&
                                   copyDiffY == thisTileC.Y - tileC.Y)
                            {
                                // stop adding copies if the tile doesn't have an offset
                                // it is probably a copy itself, so we shouldn't add it
                                if (tileC.TileOffset == 0)
                                    break;
                                // v3.8: set TileOffset to 0 to prevent later molds from using as a copy
                                thisTileC.TileOffset = 0;
                                // adding another copy
                                copySize++;
                                // now set to check next one
                                counter++;
                                thisCounter++;
                                // cancel if at end of first mold's tiles
                                if (counter >= m.Tiles.Count ||
                                    thisCounter >= tiles.Count)
                                    break;
                                tileC = (Tile)m.Tiles[counter];
                                thisTileC = (Tile)tiles[thisCounter];
                            }
                            // must break out of this loop too
                            if (tileC.TileOffset == 0)
                                break;
                        }
                        // otherwise, check to see if going to be making a copy of at least one, 
                        // must have at least 3 quadrants active
                        else if (CompareTiles(thisTileA, tileA, true))
                        {
                            // since a copy, must set to 0 to prevent later molds from using as a copy
                            thisTileA.TileOffset = 0;
                            // we will be making a copy
                            copyOffset = tileA.TileOffset;
                            copyDiffX = thisTileA.X - tileA.X;
                            copyDiffY = thisTileA.Y - tileA.Y;
                            copySize++;
                            // move for next tile
                            thisCounter++;
                        }
                        // last tile wasn't a copy, so move to check next one
                        else
                            counter++;
                        // we've found a copy, so don't try checking for more
                        if (copySize > 0)
                            break;
                    }
                    // we've found a copy, so don't try checking in any more molds
                    if (copySize > 0)
                        break;
                }
                #endregion
                if (copySize > 0)
                {
                    mold[offset] = 2;
                    mold[offset] |= (byte)(copySize << 4);
                    mold[offset + 1] = (byte)copyDiffY;
                    mold[offset + 2] = (byte)copyDiffX;
                    Bits.SetShort(mold, offset + 3, (ushort)copyOffset);
                    offset += 5;
                    i += copySize - 1;
                    thisCounter--;  // must move back one to synchronize counter (it ++'s later)
                }
                // set normal tile
                else
                {
                    tile.TileOffset = baseOffset + offset;
                    tile.Is16bit = false;
                    for (int s = 0, b = 128; s < 4; s++, b /= 2)
                    {
                        if (tile.Subtile_bytes[s] != 0)
                            mold[offset] |= (byte)b;
                        if (tile.Subtile_bytes[s] >= 0x100)
                        {
                            mold[offset] |= 1;
                            tile.Is16bit = true;
                        }
                    }
                    Bits.SetBit(mold, offset, 2, tile.Mirror);
                    Bits.SetBit(mold, offset, 3, tile.Invert);
                    offset++;
                    mold[offset] = (byte)(tile.Y ^ 0x80); offset++;
                    mold[offset] = (byte)(tile.X ^ 0x80); offset++;
                    for (int s = 0; s < 4; s++)
                    {
                        if (tile.Subtile_bytes[s] != 0)
                        {
                            if (!tile.Is16bit)
                                mold[offset++] = (byte)tile.Subtile_bytes[s];
                            else
                            {
                                Bits.SetShort(mold, offset, tile.Subtile_bytes[s]); offset += 2;
                            }
                        }
                    }
                }
                thisCounter++;
            }
            byte[] temp = new byte[offset];
            Bits.SetBytes(temp, 0, mold);
            return temp;
        }
        private bool CompareTiles(Tile tileA, Tile tileB, bool checkIfAtLeastOne)
        {
            if (checkIfAtLeastOne)
            {
                int activeQuadrants = 0;
                if (tileA.Subtile_bytes[0] != 0) activeQuadrants++;
                if (tileA.Subtile_bytes[1] != 0) activeQuadrants++;
                if (tileA.Subtile_bytes[2] != 0) activeQuadrants++;
                if (tileA.Subtile_bytes[3] != 0) activeQuadrants++;
                if (tileA.Subtile_bytes[0] == tileB.Subtile_bytes[0] &&
                    tileA.Subtile_bytes[1] == tileB.Subtile_bytes[1] &&
                    tileA.Subtile_bytes[2] == tileB.Subtile_bytes[2] &&
                    tileA.Subtile_bytes[3] == tileB.Subtile_bytes[3] &&
                    tileA.Mirror == tileB.Mirror &&
                    tileA.Invert == tileB.Invert &&
                    activeQuadrants > 2)
                    return true;
                // if 16-bit and at least 2 active quadrants, make copy since will be saving space
                foreach (ushort subtile in tileA.Subtile_bytes)
                    if (subtile >= 0x100 &&
                        tileA.Subtile_bytes[0] == tileB.Subtile_bytes[0] &&
                        tileA.Subtile_bytes[1] == tileB.Subtile_bytes[1] &&
                        tileA.Subtile_bytes[2] == tileB.Subtile_bytes[2] &&
                        tileA.Subtile_bytes[3] == tileB.Subtile_bytes[3] &&
                        tileA.Mirror == tileB.Mirror &&
                        tileA.Invert == tileB.Invert &&
                        activeQuadrants > 1)
                        return true;
            }
            return false;
        }
        // accessor functions
        public int[] MoldPixels()
        {
            moldTilesPerPixel = new int[256 * 256];
            int[] pixels = new int[256 * 256];
            Bits.Fill(moldTilesPerPixel, 0xFF);
            if (tiles.Count == 0)
                return pixels;
            //
            int[] theTile;
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile temp = (Tile)tiles[i];
                int yc, xc, w, h;
                Point p;
                if (gridplane)
                {
                    theTile = temp.GetGridplanePixels();
                    yc = 132 - temp.Height + temp.YPlusOne - temp.YMinusOne;
                    xc = 128 - (temp.Width / 2);
                    w = h = 32;
                }
                else
                {
                    theTile = temp.Get16x16TilePixels();
                    yc = temp.Y; xc = temp.X; w = h = 16;
                }
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        p = new Point(x + xc, y + yc);
                        if (p.Y < 256 && p.X < 256 && pixels[p.Y * 256 + p.X] == 0)
                        {
                            pixels[p.Y * 256 + p.X] = theTile[(y * w) + x];
                            if (theTile[(y * w) + x] != 0)
                                moldTilesPerPixel[p.Y * 256 + p.X] = i;
                        }
                    }
                }
            }
            return pixels;
        }
        public int[] GridplanePixels()
        {
            if (tiles.Count > 0)
                return ((Tile)tiles[0]).GetGridplanePixels();
            else
                return new int[32 * 32];
        }
        public int[] TilesetPixels()
        {
            int height = 16, x, y;
            Tile temp;
            height += (tiles.Count / 8) * 16;
            height += (tiles.Count % 8) != 0 ? 16 : 0;
            int[] pixels = new int[128 * height];
            for (int b = 0; b < height / 16; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if ((a + (b * 8)) >= tiles.Count) break;
                    temp = (Tile)tiles[a + (b * 8)];
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
        // spawning
        public Mold New(bool gridplane)
        {
            Mold empty = new Mold();
            empty.Tiles.Add(new Mold.Tile().New(gridplane));
            empty.Gridplane = gridplane;
            return empty;
        }
        public Mold Copy()
        {
            Mold copy = new Mold();
            copy.Tiles = new List<Tile>();
            foreach (Tile tile in tiles)
                copy.Tiles.Add(tile.Copy());
            copy.Gridplane = gridplane;
            return copy;
        }
        // universal functions
        public void Clear()
        {
            gridplane = true;
        }
        // classes
        [Serializable()]
        public class Tile
        {
            // public managers
            public object Tag;
            private Point mouseDownPosition;
            private bool addedToBuffer;
            public Point MouseDownPosition { get { return mouseDownPosition; } set { mouseDownPosition = value; } }
            public bool AddedToBuffer { get { return addedToBuffer; } set { addedToBuffer = value; } }
            // public accessors
            public int Size
            {
                get
                {
                    if (gridplane)
                        return 0;
                    int counter = 3;
                    if (is16bit)
                    {
                        if (subtile_bytes[0] != 0) counter += 2;
                        if (subtile_bytes[1] != 0) counter += 2;
                        if (subtile_bytes[2] != 0) counter += 2;
                        if (subtile_bytes[3] != 0) counter += 2;
                    }
                    else
                    {
                        if (subtile_bytes[0] != 0) counter++;
                        if (subtile_bytes[1] != 0) counter++;
                        if (subtile_bytes[2] != 0) counter++;
                        if (subtile_bytes[3] != 0) counter++;
                    }
                    return counter;
                }
            }
            public int Width
            {
                get
                {
                    if (gridplane)
                        return (format & 2) == 2 ? 32 : 24;
                    else
                        return 16;
                }
                set
                {
                    if (value == 24 && Height == 24) format = 0;
                    if (value == 24 && Height == 32) format = 1;
                    if (value == 32 && Height == 24) format = 2;
                    if (value == 32 && Height == 32) format = 3;
                    length = 1;
                    if (is16bit)
                        length += 2;
                    switch (format)
                    {
                        case 0: length += 9; break;
                        case 1: length += 12; break;
                        case 2: length += 12; break;
                        case 3: length += 16; break;
                        default: goto case 0;
                    }
                }
            }
            public int Height
            {
                get
                {
                    if (gridplane)
                        return (format & 1) == 1 ? 32 : 24;
                    else
                        return 16;
                }
                set
                {
                    if (value == 24 && Width == 24) format = 0;
                    if (value == 32 && Width == 24) format = 1;
                    if (value == 24 && Width == 32) format = 2;
                    if (value == 32 && Width == 32) format = 3;
                    length = 1;
                    if (is16bit)
                        length += 2;
                    switch (format)
                    {
                        case 0: length += 9; break;
                        case 1: length += 12; break;
                        case 2: length += 12; break;
                        case 3: length += 16; break;
                        default: goto case 0;
                    }
                }
            }
            // properties
            private byte[] buffer;
            private ushort[] subtile_bytes; public ushort[] Subtile_bytes { get { return subtile_bytes; } set { subtile_bytes = value; } }
            private Subtile[] subtile_tiles; public Subtile[] Subtile_tiles { get { return this.subtile_tiles; } }
            private int length; public int Length { get { return length; } set { length = value; } }
            private bool gridplane; public bool Gridplane { get { return gridplane; } set { gridplane = value; } }
            private byte format; public byte Format { get { return format; } set { format = value; } }
            private bool is16bit; public bool Is16bit { get { return is16bit; } set { is16bit = value; } }
            private bool mirror; public bool Mirror { get { return mirror; } set { mirror = value; } }
            private bool invert; public bool Invert { get { return invert; } set { invert = value; } }
            private byte yPlusOne; public byte YPlusOne { get { return yPlusOne; } set { yPlusOne = value; } }
            private byte yMinusOne; public byte YMinusOne { get { return yMinusOne; } set { yMinusOne = value; } }
            private byte x; public byte X { get { return x; } set { x = value; } }
            private byte y; public byte Y { get { return y; } set { y = value; } }
            // assembler-only variables
            private int tileOffset; public int TileOffset { get { return tileOffset; } set { tileOffset = value; } }
            // assemblers
            public void Disassemble(byte[] buffer, int offset, bool gridplane)
            {
                this.buffer = buffer;
                this.gridplane = gridplane;
                length = 0;
                if (offset >= buffer.Length)
                    return;
                //
                if (gridplane)
                {
                    format = (byte)buffer[offset++];
                    //
                    is16bit = (format & 0x08) == 0x08;
                    yPlusOne = (format & 0x10) == 0x10 ? (byte)1 : (byte)0;
                    yMinusOne = (format & 0x20) == 0x20 ? (byte)1 : (byte)0;
                    mirror = (format & 0x40) == 0x40;
                    invert = (format & 0x80) == 0x80;
                    length++;
                    //
                    format &= 3;
                    //
                    int subtiles16bit = 0;
                    if (is16bit)
                    {
                        subtiles16bit = Bits.GetShort(buffer, offset);
                        offset += 2;
                        length += 2;
                    }
                    //
                    byte[] temp;
                    switch (format)
                    {
                        case 0: temp = Bits.GetBytes(buffer, offset, 9); length += 9; break;
                        case 1: temp = Bits.GetBytes(buffer, offset, 12); length += 12; break;
                        case 2: temp = Bits.GetBytes(buffer, offset, 12); length += 12; break;
                        case 3: temp = Bits.GetBytes(buffer, offset, 16); length += 16; break;
                        default: goto case 0;
                    }
                    subtile_bytes = new ushort[16];
                    for (int i = 0; i < 16; i++)
                        subtile_bytes[i] = 1;
                    temp.CopyTo(subtile_bytes, 0);
                    if (is16bit)
                    {
                        for (int i = 0, b = 1; i < temp.Length; i++, b *= 2)
                            if ((subtiles16bit & b) == b)
                                subtile_bytes[i] += 0x100;
                    }
                }
                else
                {
                    format = (byte)(buffer[offset] & 0x0F);
                    mirror = (format & 0x04) == 0x04;
                    invert = (format & 0x08) == 0x08;
                    format &= 3;
                    // Set active quadrants
                    bool[] quadrants = new bool[4];
                    for (int i = 0, b = 128; i < 4; i++, b /= 2)
                        quadrants[i] = (buffer[offset] & b) == b;
                    //
                    offset++; length++;
                    y = (byte)(buffer[offset] ^ 0x80); offset++; length++;
                    x = (byte)(buffer[offset] ^ 0x80); offset++; length++;
                    // Set the subtiles
                    subtile_bytes = new ushort[4];
                    for (int i = 0; i < 4; i++)
                    {
                        if (quadrants[i])
                        {
                            if (format == 1)
                            {
                                subtile_bytes[i] = (ushort)(Bits.GetShort(buffer, offset) & 0x1FF);
                                offset++; length++;
                            }
                            else
                                subtile_bytes[i] = (ushort)buffer[offset];
                            offset++; length++;
                        }
                    }
                }
            }
            // public functions
            public void DrawSubtiles(byte[] graphics, int[] palette, bool gridplane)
            {
                int stop = 0;
                if (gridplane)
                {
                    if (subtile_bytes == null)
                        return;
                    switch (format)
                    {
                        case 0: stop = 9; break;
                        case 1:
                        case 2: stop = 12; break;
                        case 3: stop = 16; break;
                    }
                    subtile_tiles = new Subtile[16];
                    for (int i = 0; i < stop; i++)
                    {
                        if (subtile_bytes[i] != 0)
                            subtile_tiles[i] = new Subtile(subtile_bytes[i] - 1, graphics, (subtile_bytes[i] - 1) * 0x20, palette, false, false, false, false);
                        else
                            subtile_tiles[i] = new Subtile(0, new byte[0x20], 0, new int[16], false, false, false, false);
                    }
                }
                else
                {
                    subtile_tiles = new Subtile[4];
                    for (int i = 0; i < 4; i++)
                    {
                        if (subtile_bytes[i] != 0)
                            subtile_tiles[i] = new Subtile(subtile_bytes[i] - 1, graphics, (subtile_bytes[i] - 1) * 0x20, palette, false, false, false, false);
                        else
                            subtile_tiles[i] = new Subtile(0, new byte[0x20], 0, new int[16], false, false, false, false);
                    }
                }
            }
            public bool CompareSubtiles(Tile source)
            {
                if (Bits.Compare(subtile_bytes, source.Subtile_bytes))
                    return true;
                return false;
            }
            // accessor functions
            public int[] Get16x16TilePixels()
            {
                int[] pixels = new int[16 * 16];
                int color = 0;
                if (subtile_tiles == null)
                    color = Color.Red.ToArgb();
                if (Bits.Empty(subtile_bytes))
                    color = Color.Gray.ToArgb();
                if (subtile_tiles == null || Bits.Empty(subtile_bytes))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        pixels[i * 16 + i] = color; // UL to LR
                        pixels[i * 16 + 15 - i] = color; // UR to LL
                        pixels[i] = color; // top line
                        pixels[i * 16] = color; // left line
                        pixels[i * 16 + 15] = color; // right line
                        pixels[15 * 16 + i] = color; // bottom line
                    }
                    return pixels;
                }
                for (int i = 0, a = 0, b = 0; i < 4; i++)
                {
                    a = (i == 0) || (i == 2) ? 0 : 8;
                    b = (i == 0) || (i == 1) ? 0 : 8;
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                            pixels[x + a + ((y + b) * 16)] = subtile_tiles[i].Pixels[y * 8 + x];
                    }
                }
                if (mirror)
                    Do.FlipHorizontal(pixels, 16, 16);
                if (invert)
                    Do.FlipVertical(pixels, 16, 16);
                return pixels;
            }
            public int[] GetGridplanePixels()
            {
                int[] pixels = new int[32 * 32];
                if (subtile_tiles == null)
                    return pixels;
                //
                int w; int h;
                int r; int c;
                switch (format)
                {
                    case 0: w = h = 24; break;
                    case 1: w = 24; h = 32; break;
                    case 2: w = 32; h = 24; break;
                    case 3: w = 32; h = 32; break;
                    default: goto case 0;
                }
                for (int i = 0; i < ((h / 8) * (w / 8)); i++)
                {
                    r = (i / (w / 8)) * 8; c = (i % (w / 8)) * 8;
                    for (int y = 0; y < 8; y++)
                    {
                        if (subtile_tiles[i] == null) break;
                        for (int x = 0; x < 8; x++)
                            pixels[x + c + ((y + r) * 32)] = subtile_tiles[i].Pixels[y * 8 + x];
                    }
                }
                if (mirror)
                    Do.FlipHorizontal(pixels, 32, 0, 0, w, h);
                if (invert)
                    Do.FlipVertical(pixels, 32, 0, 0, w, h);
                return pixels;
            }
            public void SetTileLength()
            {
                is16bit = false;
                foreach (ushort subtile in subtile_bytes)
                {
                    length = 3;
                    if (subtile != 0)
                        length++;
                    if (subtile >= 0x100)
                        is16bit = true;
                }
                if (gridplane)
                {
                    length = 1;
                    if (is16bit)
                        length += 2;
                    switch (format)
                    {
                        case 0: length += 9; break;
                        case 1: length += 12; break;
                        case 2: length += 12; break;
                        case 3: length += 16; break;
                        default: goto case 0;
                    }
                }
            }
            // spawning
            public Tile New(bool gridplane)
            {
                Tile empty = new Tile();
                if (gridplane)
                {
                    empty.Length = 10;
                    empty.Subtile_bytes = new ushort[16];
                    for (int i = 0; i < 9; i++)
                        empty.Subtile_bytes[i] = 1;
                }
                else
                {
                    empty.Length = 3;
                    empty.Subtile_bytes = new ushort[4];
                    empty.X = 0x80;
                    empty.Y = 0x80;
                }
                return empty;
            }
            public Tile Copy()
            {
                Tile copy = new Tile();
                copy.Gridplane = gridplane;
                copy.Invert = invert;
                copy.Is16bit = is16bit;
                copy.Mirror = mirror;
                copy.Subtile_bytes = Bits.Copy(subtile_bytes);
                copy.Format = format;
                copy.TileOffset = tileOffset;
                copy.Length = length;
                copy.X = x;
                copy.Y = y;
                copy.YMinusOne = yMinusOne;
                copy.YPlusOne = yPlusOne;
                return copy;
            }
        }
    }
}
