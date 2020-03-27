using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    /// <summary>
    /// Generic 8x8 subtile used anywhere.
    /// </summary>
    [Serializable()]
    public class Subtile
    {
        // tile properties
        private bool twobpp;
        private bool priority1, mirror, invert;
        public bool Priority1 { get { return priority1; } set { priority1 = value; } }
        public bool Mirror { get { return mirror; } set { mirror = value; } }
        public bool Invert { get { return invert; } set { invert = value; } }
        private int index, palette;
        public int Index { get { return index; } set { index = value; } }
        public int Palette { get { return this.palette; } set { this.palette = value; } }
        // accessors
        private int[] pixels = new int[64];
        public int[] Pixels
        {
            get { return pixels; }
            set
            {
                pixels = value;
            }
        }
        private int[] colors = new int[64];
        public int[] Colors
        {
            get { return colors; }
            set
            {
                colors = value;
            }
        }
        // constructors
        public Subtile(int index, byte[] graphics, int offset, int[] palette,
            bool mirror, bool invert, bool priority1, bool twobpp)
        {
            this.mirror = mirror;
            this.invert = invert;
            this.priority1 = priority1;
            this.index = index;
            this.twobpp = twobpp;
            if (twobpp == false)
            {
                for (int r = 0; r < 8; r++) // Number of Rows in an 8x8 Tile
                {
                    // Get all the pixels in a row
                    byte[] row = new byte[8];
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((graphics[offset + r * 2 + 0x11] & b) == b)
                            row[i] += 8;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((graphics[offset + r * 2 + 0x10] & b) == b)
                            row[i] += 4;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((graphics[offset + r * 2 + 1] & b) == b)
                            row[i] += 2;
                    for (int i = 7, b = 1; i >= 0; i--, b *= 2)
                        if ((graphics[offset + r * 2] & b) == b)
                            row[i]++;
                    for (int c = 0; c < 8; c++) // Number of Columns in an 8x8 Tile
                    {
                        colors[r * 8 + c] = row[c];
                        if (row[c] != 0)
                            pixels[r * 8 + c] = palette[row[c]]; // Set pixel in 8x8 tile
                    }
                }
            }
            else
            {
                byte b1, b2, t1, t2, col = 0;
                int[] pal = new int[4];
                for (int i = 0; i < 4; i++)
                    pal[i] = palette[i];
                for (byte i = 0; i < 8; i++)
                {
                    b1 = graphics[offset];
                    b2 = graphics[offset + 1];
                    for (byte z = 7; col < 8; z--)
                    {
                        t1 = (byte)((b1 >> z) & 1);
                        t2 = (byte)((b2 >> z) & 1);
                        colors[(i * 8) + col] = (t2 * 2) + t1;
                        if ((t2 * 2) + t1 != 0)
                            pixels[(i * 8) + col] = pal[(t2 * 2) + t1];
                        col++;
                    }
                    col = 0;
                    offset += 2;
                }
            }
            if (mirror) Do.FlipHorizontal(pixels, 8, 8);
            if (invert) Do.FlipVertical(pixels, 8, 8);
        }
        public Subtile(int index, byte[] graphics, int offset, int[] palette)
        {
            this.index = index;
            for (int i = 0; i < 64; i++)
            {
                if (i % 2 == 0)
                {
                    pixels[i] = palette[graphics[offset + (i / 2)] & 0x0F];
                    colors[i] = graphics[offset + (i / 2)] & 0x0F;
                }
                else
                {
                    pixels[i] = palette[(graphics[offset + (i / 2)] & 0xF0) >> 4];
                    colors[i] = (graphics[offset + (i / 2)] & 0xF0) >> 4;
                }
            }
        }
        private Subtile()
        {
        }
        // spawning
        public Subtile Copy()
        {
            Subtile copy = new Subtile();
            copy.Pixels = new int[this.pixels.Length]; this.Pixels.CopyTo(copy.Pixels, 0);
            copy.Colors = new int[this.colors.Length]; this.Colors.CopyTo(copy.Colors, 0);
            copy.Priority1 = this.priority1;
            copy.Mirror = this.mirror;
            copy.Invert = this.invert;
            copy.Index = this.index;
            copy.Palette = this.palette;
            return copy;
        }
        public void CopyTo(Subtile dest)
        {
            dest.Index = this.index;
            dest.Palette = this.palette;
            dest.Priority1 = this.priority1;
            dest.Mirror = this.mirror;
            dest.Invert = this.invert;
            this.pixels.CopyTo(dest.Pixels, 0);
            this.colors.CopyTo(dest.Colors, 0);
        }
        // universal functions
        public void Clear()
        {
            mirror = false;
            invert = false;
            priority1 = false;
            pixels = new int[64];
            colors = new int[64];
            index = 0;
            palette = 0;
        }
    }
}
