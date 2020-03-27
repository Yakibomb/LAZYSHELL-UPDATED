using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public class Mode7Tilemap : Tilemap
    {
        #region Variables
        private Tileset tileset;
        private PaletteSet paletteSet;
        private State state = State.Instance2;
        private int Width = 64;
        private int Height = 64;
        private byte[] tilemap_Bytes;
        private Tile[] tilemap_Tiles;
        private int[] pixels = new int[1024 * 1024];
        // public accessors
        public override int Width_p { get { return Width * 16; } set { Width = value / 16; } }
        public override int Height_p { get { return Height * 16; } set { Height = value / 16; } }
        public Size Size { get { return new Size(Width, Height); } }
        public Size Size_p { get { return new Size(Width_p, Height_p); } }
        public override byte[] Tilemap_Bytes { get { return tilemap_Bytes; } set { tilemap_Bytes = value; } }
        public override byte[][] Tilemaps_Bytes { get { return new byte[][] { tilemap_Bytes, null, null }; } set { tilemap_Bytes = value[0]; } }
        public override Tile[] Tilemap_Tiles { get { return tilemap_Tiles; } set { tilemap_Tiles = value; } }
        public override Tile[][] Tilemaps_Tiles { get { return new Tile[][] { tilemap_Tiles, null, null }; } set { tilemap_Tiles = value[0]; } }
        public override int[] Pixels { get { return pixels; } set { pixels = value; } }
        public override Bitmap Image { get { return null; } set { } }
        #endregion
        // constructor
        public Mode7Tilemap(byte[] tilemap, Tileset tileset, PaletteSet paletteSet)
        {
            this.tileset = tileset;
            this.paletteSet = paletteSet;
            this.tilemap_Bytes = tilemap;
            CreateLayer(); // Create any required layers
            DrawMainscreen();
        }
        // assemblers
        public override void Assemble()
        {
            if (tilemap_Tiles == null)
                return;
            for (int i = 0; i < tilemap_Tiles.Length; i++)
                tilemap_Bytes[i] = (byte)tilemap_Tiles[i].Index;
        }
        // drawing
        private void ChangeSingleTile(int placement, int tile, int x, int y)
        {
            this.tilemap_Tiles[placement] = tileset.Tileset_tiles[tile]; // Change the tile in the layer map
            Tile source = this.tilemap_Tiles[placement]; // Grab the new tile
            // Draw all 4 subtiles to the appropriate array based on priority
            Do.PixelsToPixels(source.Subtiles[0].Pixels, this.pixels, Width_p, new Rectangle(x, y, 8, 8));
            Do.PixelsToPixels(source.Subtiles[1].Pixels, this.pixels, Width_p, new Rectangle((x + 8), y, 8, 8));
            Do.PixelsToPixels(source.Subtiles[2].Pixels, this.pixels, Width_p, new Rectangle(x, (y + 8), 8, 8));
            Do.PixelsToPixels(source.Subtiles[3].Pixels, this.pixels, Width_p, new Rectangle((x + 8), (y + 8), 8, 8));
            DrawSingleMainscreenTile(x, y);
        }
        public void Clear()
        {
            Model.MinecartM7TilemapA = new byte[0x2000];
            Model.MinecartM7TilemapB = new byte[0x2000];
            RedrawTilemaps();
        }
        private void ClearSingleTile(int[] pixels, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                pixels[y * Width_p + x + counter] = 0;
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void CopySingleTileToArray(int[] dst, int[] src, int width, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                if (src[i] != 0)
                    dst[y * width + x + counter] = src[i];
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void CopyToPixelArray(int[] dst, int[] src)
        {
            try
            {
                for (int i = 0; i < src.Length; i++)
                    if (src[i] != 0)
                        dst[i] = src[i];
            }
            catch
            {
                // overflow
            }
        }
        private void CreateLayer()
        {
            if (tilemap_Bytes == null)
                return;
            if (tileset.Tileset_tiles == null)
                return;
            tilemap_Tiles = new Tile[Width * Height]; // Create our layer here
            int offset = 0;
            for (int i = 0; i < Width * Height && i < tilemap_Bytes.Length; i++)
            {
                byte tilenum = tilemap_Bytes[offset++];
                tilemap_Tiles[i] = tileset.Tileset_tiles[tilenum];
            }
        }
        private void DrawMainscreen()
        {
            if (tilemap_Tiles == null)
                return;
            for (int i = 0; i < tilemap_Tiles.Length; i++)
            {
                for (int z = 0; z < 4; z++)
                {
                    switch (z)
                    {
                        case 0:
                            Do.PixelsToPixels(tilemap_Tiles[i].Subtiles[z].Pixels, pixels, Width_p, new Rectangle((i % Width) * 16, (i / Width) * 16, 8, 8));
                            break;
                        case 1:
                            Do.PixelsToPixels(tilemap_Tiles[i].Subtiles[z].Pixels, pixels, Width_p, new Rectangle((i % Width) * 16 + 8, (i / Width) * 16, 8, 8));
                            break;
                        case 2:
                            Do.PixelsToPixels(tilemap_Tiles[i].Subtiles[z].Pixels, pixels, Width_p, new Rectangle((i % Width) * 16, (i / Width) * 16 + 8, 8, 8));
                            break;
                        case 3:
                            Do.PixelsToPixels(tilemap_Tiles[i].Subtiles[z].Pixels, pixels, Width_p, new Rectangle((i % Width) * 16 + 8, (i / Width) * 16 + 8, 8, 8));
                            break;
                        default:
                            break;
                    }
                }
            }
            int bgcolor = paletteSet.Palette[16];
            // Apply BG color
            if (state.BG)
            {
                for (int i = 0; i < Width_p * Height_p; i++)
                {
                    if (pixels[i] == 0)
                        pixels[i] = bgcolor;
                }
            }
        }
        private void DrawSingleMainscreenTile(int x, int y)
        {
            int bgcolor = paletteSet.Palette[16];
            CopySingleTileToArray(pixels, Do.GetPixelRegion(pixels, 1024, 1024, 16, 16, x, y), Width_p, x, y);
            // Apply BG color
            for (int b = y; b < y + 16; b++)
            {
                for (int a = x; a < x + 16; a++)
                {
                    if (pixels[b * Width_p + a] == 0)
                        pixels[b * Width_p + a] = bgcolor;
                }
            }
        }
        public override void RedrawTilemaps()
        {
            Array.Clear(pixels, 0, pixels.Length);
            CreateLayer();
            DrawMainscreen();
        }
        // accessor functions
        public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= Width_p) x = Width_p - 1;
            if (y >= Height_p) y = Height_p - 1;
            Point p = new Point(x % 16, y % 16);
            y /= 16;
            x /= 16;
            int placement = y * Width + x;
            if (tilemap_Tiles != null)
            {
                if (!ignoretransparent)
                    return tilemap_Tiles[placement].Index;
                else if (tilemap_Tiles[placement].Pixels[p.Y * 16 + p.X] != 0)
                    return tilemap_Tiles[placement].Index;
                else
                    return 0;
            }
            else
                return 0;
        }
        public override int GetTileNum(int layer, int x, int y)
        {
            return GetTileNum(0, x, y, false);
        }
        public override int GetTileNum(int index)
        {
            throw new NotImplementedException();
        }
        public override int[] GetPixels(int layer, Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            int bgcolor = paletteSet.Palette[16];
            for (int b = 0, y = p.Y; b < s.Height; b++, y++)
            {
                for (int a = 0, x = p.X; a < s.Width; a++, x++)
                {
                    int srcIndex = y * Width_p + x;
                    int dstIndex = b * s.Width + a;
                    if (srcIndex >= this.pixels.Length || dstIndex >= pixels.Length)
                        continue;
                    if (this.pixels[srcIndex] != 0)
                        pixels[dstIndex] = this.pixels[srcIndex];
                    else
                        pixels[dstIndex] = bgcolor;
                }
            }
            return pixels;
        }
        public override int[] GetPixels(Point location, Size size)
        {
            return GetPixels(0, location, size);
        }
        public override int GetPixelLayer(int x, int y)
        {
            return 0;
        }
        public override int[] GetPriority1Pixels()
        {
            return new int[1024 * 1024];
        }
        public override void SetTileNum()
        {
            throw new NotImplementedException();
        }
        public override void SetTileNum(int tilenum, int layer, int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width_p || y >= Height_p)
                return;
            y /= 16;
            x /= 16;
            int index = y * Width + x;
            if (index < 0x1000)
                ChangeSingleTile(index, tilenum, x * 16, y * 16);
            tilemap_Bytes[y * Width + x] = (byte)tilenum;
        }
    }
}
