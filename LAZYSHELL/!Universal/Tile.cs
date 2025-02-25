using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    /// <summary>
    /// Generic 16x16 tile used in a tilemap, containing 4 subtiles.
    /// </summary>
    [Serializable()]
    public class Tile
    {
        // tile properties
        private Subtile[] subtiles = new Subtile[4];
        public Subtile[] Subtiles { get { return subtiles; } set { subtiles = value; } }
        private int index;
        public int Index { get { return index; } set { index = value; } }
        private bool mirror, invert;
        public bool Mirror { get { return mirror; } set { mirror = value; } }
        public bool Invert { get { return invert; } set { invert = value; } }
        // accessors
        private int[] pixels = new int[256];
        public int[] Pixels
        {
            get
            {
                int[] pixels = new int[16 * 16];
                if (subtiles[0] != null)
                    Do.PixelsToPixels(subtiles[0].Pixels, pixels, 16, new Rectangle(0, 0, 8, 8));
                if (subtiles[1] != null)
                    Do.PixelsToPixels(subtiles[1].Pixels, pixels, 16, new Rectangle(8, 0, 8, 8));
                if (subtiles[2] != null)
                    Do.PixelsToPixels(subtiles[2].Pixels, pixels, 16, new Rectangle(0, 8, 8, 8));
                if (subtiles[3] != null)
                    Do.PixelsToPixels(subtiles[3].Pixels, pixels, 16, new Rectangle(8, 8, 8, 8));
                if (mirror)
                    Do.FlipHorizontal(pixels, 16, 16);
                if (invert)
                    Do.FlipVertical(pixels, 16, 16);
                return pixels;
            }
            set
            {
                if (subtiles[0] != null)
                    subtiles[0].Pixels = Do.GetPixelRegion(value, new Rectangle(0, 0, 8, 8), 16, 16);
                if (subtiles[1] != null)
                    subtiles[1].Pixels = Do.GetPixelRegion(value, new Rectangle(8, 0, 8, 8), 16, 16);
                if (subtiles[2] != null)
                    subtiles[2].Pixels = Do.GetPixelRegion(value, new Rectangle(0, 8, 8, 8), 16, 16);
                if (subtiles[3] != null)
                    subtiles[3].Pixels = Do.GetPixelRegion(value, new Rectangle(8, 8, 8, 8), 16, 16);
            }
        }
        public int[] Pixels_P1
        {
            get
            {
                int[] pixels = new int[16 * 16];
                if (subtiles[0] != null && subtiles[0].Priority1)
                    Do.PixelsToPixels(subtiles[0].Pixels, pixels, 16, new Rectangle(0, 0, 8, 8));
                if (subtiles[1] != null && subtiles[1].Priority1)
                    Do.PixelsToPixels(subtiles[1].Pixels, pixels, 16, new Rectangle(8, 0, 8, 8));
                if (subtiles[2] != null && subtiles[2].Priority1)
                    Do.PixelsToPixels(subtiles[2].Pixels, pixels, 16, new Rectangle(0, 8, 8, 8));
                if (subtiles[3] != null && subtiles[3].Priority1)
                    Do.PixelsToPixels(subtiles[3].Pixels, pixels, 16, new Rectangle(8, 8, 8, 8));
                if (mirror)
                    Do.FlipHorizontal(pixels, 16, 16);
                if (invert)
                    Do.FlipVertical(pixels, 16, 16);
                for (int i = 0; i < 256; i++)
                    if (pixels[i] != 0) pixels[i] = Color.Blue.ToArgb();
                return pixels;
            }
        }
        public int[] Pixels_P1_Tileset
        {
            get
            {
                int[] pixels = new int[16 * 16];
                if (subtiles[0] != null && subtiles[0].Priority1)
                    Do.PixelsToPixels(subtiles[0].Pixels, pixels, 16, new Rectangle(0, 0, 8, 8));
                if (subtiles[1] != null && subtiles[1].Priority1)
                    Do.PixelsToPixels(subtiles[1].Pixels, pixels, 16, new Rectangle(8, 0, 8, 8));
                if (subtiles[2] != null && subtiles[2].Priority1)
                    Do.PixelsToPixels(subtiles[2].Pixels, pixels, 16, new Rectangle(0, 8, 8, 8));
                if (subtiles[3] != null && subtiles[3].Priority1)
                    Do.PixelsToPixels(subtiles[3].Pixels, pixels, 16, new Rectangle(8, 8, 8, 8));
                if (mirror)
                    Do.FlipHorizontal(pixels, 16, 16);
                if (invert)
                    Do.FlipVertical(pixels, 16, 16);
                return pixels;
            }
        }
        // constructors
        public Tile(int index)
        {
            this.index = index; // set tile Number
            this.pixels = new int[16 * 16];
            for (int p = 0; p < 4; p++)
                subtiles[p] = new Subtile(0, new byte[0x20], 0, new int[16], false, false, false, false);
        }
        // spawning
        public Tile Copy()
        {
            Tile copy = new Tile(this.index);
            for (int i = 0; i < 4; i++)
            {
                Subtile source = subtiles[i].Copy();
                copy.Subtiles[i] = source;
            }
            copy.Mirror = mirror;
            copy.Invert = invert;
            return copy;
        }
        // universal functions
        public void Clear()
        {
            foreach (Subtile tile in subtiles)
                tile.Clear();
        }
    }
}
