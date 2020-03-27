using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class FontCharacter
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } }
        // class variables, accessors
        private FontType type; public FontType Type { get { return type; } }
        private byte width; public byte Width { get { return width; } set { width = value; } }
        private byte height; public byte Height { get { return height; } }
        private byte[] graphics; public byte[] Graphics { get { return graphics; } set { graphics = value; } }
        private byte maxWidth; public byte MaxWidth { get { return maxWidth; } }
        // constructor
        public FontCharacter(int index, FontType type)
        {
            this.index = index;
            this.type = type;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            switch (type)
            {
                case FontType.Menu: // menu font
                    width = (byte)rom[index + 0x249300]; maxWidth = 8; height = 12;
                    graphics = Bits.GetBytes(rom, index * 0x18 + 0x249400, 0x18);
                    break;
                case FontType.Dialogue: // dialogue font
                    width = (byte)rom[index + 0x249280]; maxWidth = 16; height = 12;
                    graphics = Bits.GetBytes(rom, index * 0x30 + 0x37C000, 0x30);
                    break;
                case FontType.Description: // description font
                    width = (byte)rom[index + 0x249380]; maxWidth = 8; height = 8;
                    graphics = Bits.GetBytes(rom, index * 0x10 + 0x37D800, 0x10);
                    break;
                case FontType.Triangles: // triangles
                    if (index < 7) { width = maxWidth = 8; height = 16; }
                    else { width = maxWidth = 16; height = 8; }
                    graphics = Bits.GetBytes(rom, index * 0x20 + 0x3DFA00, 0x20);
                    break;
                case FontType.BattleMenu: // battle menu font
                    width = 8; maxWidth = 8; height = 8;
                    graphics = Bits.GetBytes(Model.BattleMenuGraphics, index * 0x20, 0x20);
                    break;
                case FontType.FlowerBonus: // flower bonus font
                    width = 8; maxWidth = 8; height = 8;
                    graphics = Bits.GetBytes(Model.BonusFontGraphics, index * 0x20, 0x20);
                    break;
            }
        }
        public void Assemble()
        {
            switch (type)
            {
                case FontType.Menu: // menu font
                    rom[index + 0x249300] = width;
                    Bits.SetBytes(rom, index * 0x18 + 0x249400, graphics);
                    break;
                case FontType.Dialogue: // dialogue font
                    rom[index + 0x249280] = width;
                    Bits.SetBytes(rom, index * 0x30 + 0x37C000, graphics);
                    break;
                case FontType.Description: // description font
                    rom[index + 0x249380] = width;
                    Bits.SetBytes(rom, index * 0x10 + 0x37D800, graphics);
                    break;
                case FontType.Triangles: // triangles
                    Bits.SetBytes(rom, index * 0x20 + 0x3DFA00, graphics);
                    break;
                case FontType.BattleMenu: // battle menu font
                    Bits.SetBytes(rom, index * 0x20, graphics);
                    break;
            }
        }
        // class functions
        public int[] GetPixels(int[] palette)
        {
            int offset = 0;
            int[] pixels = new int[maxWidth * height];
            if ((int)type < 4)
            {
                byte b1, b2, t1, t2, col = 0;
                for (int a = 0; a < maxWidth / 8; a++)
                {
                    for (byte i = 0; i < height; i++)
                    {
                        b1 = graphics[offset];
                        b2 = graphics[offset + 1];
                        for (byte z = 7; col < maxWidth; z--)
                        {
                            t1 = (byte)((b1 >> z) & 1);
                            t2 = (byte)((b2 >> z) & 1);
                            if (t2 * 2 + t1 != 0)
                            {
                                if (type != FontType.Triangles)
                                    pixels[(i * maxWidth) + col] = palette[(t2 * 2) + t1];
                                else
                                    pixels[(i * maxWidth) + col] = palette[(t2 * 2) + t1 + 4];
                            }
                            col++;
                        }
                        col = (byte)(a * 8);
                        offset += 2;
                    }
                    col += 8;
                }
            }
            else
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
                        if (row[c] != 0)
                            pixels[r * 8 + c] = palette[row[c]]; // Set pixel in 8x8 tile
                    }
                }
            }
            return pixels;
        }
        public int GetLeftMostPixel(int[] palette)
        {
            int[] pixels = GetPixels(palette);
            int right = 0;
            for (int x = 0; x < maxWidth; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pixels[y * maxWidth + x] != 0)
                    {
                        right = x;
                        return right;
                    }
                }
            }
            return 0;
        }
        public int GetRightMostPixel(int[] palette)
        {
            int[] pixels = GetPixels(palette);
            int left = maxWidth;
            for (int x = maxWidth - 1; x >= 0; x--)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pixels[y * maxWidth + x] != 0 && x < left)
                    {
                        left = x;
                        return left;
                    }
                }
            }
            return 0;
        }
        public int GetTopMostPixel(int[] palette)
        {
            int[] pixels = GetPixels(palette);
            int bottom = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    if (pixels[y * maxWidth + x] != 0)
                    {
                        bottom = y;
                        return bottom;
                    }
                }
            }
            return 0;
        }
        public int GetBottomMostPixel(int[] palette)
        {
            int[] pixels = GetPixels(palette);
            int top = height;
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    if (pixels[y * maxWidth + x] != 0 && y < top)
                    {
                        top = y;
                        return top;
                    }
                }
            }
            return 0;
        }
        public void Mirror(int[] palette)
        {
            int maxY = GetBottomMostPixel(palette);
            int maxX = GetRightMostPixel(palette);
            int minY = GetTopMostPixel(palette);
            int minX = GetLeftMostPixel(palette);
            for (int y = minY; y < maxY + 1; y++)
            {
                for (int a = minX, b = maxX; a < b; a++, b--)
                {
                    byte rowA = (byte)y;
                    byte colA = (byte)a;
                    byte bitA = (byte)((colA & 7) ^ 7);
                    int offsetA = rowA * 2;
                    byte rowB = (byte)y;
                    byte colB = (byte)b;
                    byte bitB = (byte)((colB & 7) ^ 7);
                    int offsetB = rowB * 2;
                    switch (Type)
                    {
                        case FontType.Menu:
                        case FontType.Description:
                            bool tempM = Bits.GetBit(Graphics, offsetA, bitA);
                            bool tempN = Bits.GetBit(Graphics, offsetA + 1, bitA);
                            Bits.SetBit(Graphics, offsetA, bitA, Bits.GetBit(Graphics, offsetB, bitB));
                            Bits.SetBit(Graphics, offsetA + 1, bitA, Bits.GetBit(Graphics, offsetB + 1, bitB));
                            Bits.SetBit(Graphics, offsetB, bitB, tempM);
                            Bits.SetBit(Graphics, offsetB + 1, bitB, tempN);
                            break;
                        case FontType.Dialogue:
                            offsetA += colA >= 8 ? 24 : 0;
                            offsetB += colB >= 8 ? 24 : 0;
                            goto case 0;
                        case FontType.Triangles:
                            offsetA += colA >= 8 ? 16 : 0;
                            offsetB += colB >= 8 ? 16 : 0;
                            goto case 0;
                    }
                }
            }
        }
        public void Invert(int[] palette)
        {
            int maxY = GetBottomMostPixel(palette);
            int maxX = GetRightMostPixel(palette);
            int minY = GetTopMostPixel(palette);
            int minX = GetLeftMostPixel(palette);
            for (int x = minX; x < maxX + 1; x++)
            {
                for (int a = minY, b = maxY; a < b; a++, b--)
                {
                    byte rowA = (byte)a;
                    byte colA = (byte)x;
                    byte bitA = (byte)((colA & 7) ^ 7);
                    int offsetA = rowA * 2;
                    byte rowB = (byte)b;
                    byte colB = (byte)x;
                    byte bitB = (byte)((colB & 7) ^ 7);
                    int offsetB = rowB * 2;
                    switch (Type)
                    {
                        case FontType.Menu:
                        case FontType.Description:
                            bool tempM = Bits.GetBit(Graphics, offsetA, bitA);
                            bool tempN = Bits.GetBit(Graphics, offsetA + 1, bitA);
                            Bits.SetBit(Graphics, offsetA, bitA, Bits.GetBit(Graphics, offsetB, bitB));
                            Bits.SetBit(Graphics, offsetA + 1, bitA, Bits.GetBit(Graphics, offsetB + 1, bitB));
                            Bits.SetBit(Graphics, offsetB, bitB, tempM);
                            Bits.SetBit(Graphics, offsetB + 1, bitB, tempN);
                            break;
                        case FontType.Dialogue:
                            offsetA += colA >= 8 ? 24 : 0;
                            offsetB += colB >= 8 ? 24 : 0;
                            goto case 0;
                        case FontType.Triangles:
                            offsetA += colA >= 8 ? 16 : 0;
                            offsetB += colB >= 8 ? 16 : 0;
                            goto case 0;
                    }
                }
            }
        }
    }
}
