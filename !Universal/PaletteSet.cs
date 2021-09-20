using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class PaletteSet
    {
        // universal variables
        [NonSerialized()]
        private byte[] buffer; public byte[] BUFFER { get { return buffer; } set { buffer = value; } }
        // class variables
        private int offset, index, count, size, length;
        private int[] reds, greens, blues;
        private int[] palette;
        private int[][] palettes;
        // public accessors
        public int Length { get { return length; } }
        public int[] Reds { get { return reds; } set { reds = value; } }
        public int[] Greens { get { return greens; } set { greens = value; } }
        public int[] Blues { get { return blues; } set { blues = value; } }
        public int[] Palette { get { return Do.RGBToColors(reds, greens, blues); } }
        public int[][] Palettes { get { return Do.RGBToColors(reds, greens, blues, count, size); } }
        // constructors
        /// <summary>
        /// Creates a set of palettes from an offset in a byte array.
        /// </summary>
        /// <param name="buffer">The byte array.</param>
        /// <param name="index">The palette set's index.</param>
        /// <param name="offset">The palette set's offset.</param>
        /// <param name="count">The number of palettes in the set.</param>
        /// <param name="size">The number of colors in each palette.</param>
        /// <param name="length">The length, in raw 15-bit format, of each palette.</param>
        public PaletteSet(byte[] buffer, int index, int offset, int count, int size, int length)
        {
            this.buffer = buffer;
            this.index = index;
            this.offset = offset;
            this.count = count;
            this.size = size;
            this.length = length;
            Disassemble();
        }
        public PaletteSet()
        {
        }
        // assemblers
        private void Disassemble()
        {
            reds = new int[count * size];
            greens = new int[count * size];
            blues = new int[count * size];
            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < size; a++)
                {
                    if ((i * length) + (a * 2) + offset + 1 >= buffer.Length)
                        continue;
                    ushort color = Bits.GetShort(buffer, (i * length) + (a * 2) + offset);
                    reds[i * size + a] = (color % 0x20) * 8;
                    greens[i * size + a] = ((color >> 5) % 0x20) * 8;
                    blues[i * size + a] = ((color >> 10) % 0x20) * 8;
                }
            }
            palette = Do.RGBToColors(reds, greens, blues);
            palettes = Do.RGBToColors(reds, greens, blues, count, size);
        }
        public void Assemble()
        {
            for (int i = 0; i < count; i++)
            {
                for (int a = (32 - length) / 2; a < size; a++)
                {
                    int r = reds[i * size + a] / 8;
                    int g = greens[i * size + a] / 8;
                    int b = blues[i * size + a] / 8;
                    ushort color = (ushort)((b << 10) | (g << 5) | r);
                    Bits.SetShort(buffer, (i * length) + (a * 2) + offset, color);
                }
            }
        }
        public void Assemble(int startIndex)
        {
            for (int i = startIndex; i < count; i++)
            {
                for (int a = (32 - length) / 2; a < size; a++)
                {
                    int r = reds[i * size + a] / 8;
                    int g = greens[i * size + a] / 8;
                    int b = blues[i * size + a] / 8;
                    ushort color = (ushort)((b << 10) | (g << 5) | r);
                    Bits.SetShort(buffer, (i * length) + (a * 2) + offset, color);
                }
            }
        }
        public void Assemble(byte[] buffer, int offset)
        {
            for (int i = 0; i < count; i++)
            {
                for (int a = (32 - length) / 2; a < size; a++)
                {
                    int r = reds[i * size + a] / 8;
                    int g = greens[i * size + a] / 8;
                    int b = blues[i * size + a] / 8;
                    ushort color = (ushort)((b << 10) | (g << 5) | r);
                    Bits.SetShort(buffer, (i * length) + (a * 2) + offset, color);
                }
            }
        }
        // universal functions
        public void Clear()
        {
            reds = new int[count * size];
            greens = new int[count * size];
            blues = new int[count * size];
            palette = Do.RGBToColors(reds, greens, blues);
            palettes = Do.RGBToColors(reds, greens, blues, count, size);
        }
        public void Clear(int startIndex)
        {
            for (int i = startIndex; i < count; i++)
            {
                for (int a = 0; a < size; a++)
                {
                    reds[i * size + a] = 0;
                    greens[i * size + a] = 0;
                    blues[i * size + a] = 0;
                }
            }
            palette = Do.RGBToColors(reds, greens, blues);
            palettes = Do.RGBToColors(reds, greens, blues, count, size);
        }
        // spawning
        public PaletteSet Copy()
        {
            PaletteSet copy = new PaletteSet(buffer, index, offset, count, size, length);
            copy.BUFFER = Bits.Copy(buffer);
            copy.Reds = Bits.Copy(reds);
            copy.Greens = Bits.Copy(greens);
            copy.Blues = Bits.Copy(blues);
            return copy;
        }
        public void CopyTo(PaletteSet copyTo)
        {
            reds.CopyTo(copyTo.Reds, 0);
            greens.CopyTo(copyTo.Greens, 0);
            blues.CopyTo(copyTo.Blues, 0);
        }
    }
}
