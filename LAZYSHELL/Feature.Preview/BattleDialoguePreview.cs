using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    class BattleDialoguePreview : Preview
    {
        // class variables
        private int page = 0;
        private Point location;
        private int next;
        private int[] palette;
        private FontCharacter[] fontCharacters;
        private Stack<int> pages = new Stack<int>();
        // constructor
        public BattleDialoguePreview()
        {
            pages.Push(0);
        }
        // class functions
        private void AddBorder(int[] pixels)
        {
            int[] borderCalc = { -1, 1, -256, 256, -257, 257, -255, 255 };
            for (int i = 0; i < pixels.Length; i++) // for each pixel in image
            {
                if (pixels[i] != 0 && pixels[i] != palette[3]) // draw border if it is a set pixel, and not border color
                {
                    for (int z = 0; z < borderCalc.Length; z++) // for each of the border pixels
                    {
                        if (pixels[i + borderCalc[z]] == 0) // if border pixels are empty
                            pixels[i + borderCalc[z]] = palette[3]; // fill pixel with border color
                    }
                }
            }
        }
        private byte MinimumWidth(FontCharacter fontCharacter)
        {
            byte width = fontCharacter.Width;
            if (width <= fontCharacter.GetRightMostPixel(palette))
                width = (byte)(fontCharacter.GetRightMostPixel(palette) + 1);
            return width;
        }
        // public functions
        public void Reset()
        {
            next = 0;
            pages.Clear();
            pages.Push(0);
            location = new Point(0, 0);
        }
        public void Refresh()
        {
            int page = this.page;
            while (page-- >= 0)
                PageUp();
            page = 0;
            while (page++ <= this.page)
                PageDown();
        }
        public void PageUp()
        {
            if (pages.Count > 1)
            {
                page--;
                pages.Pop();
                location = new Point(0, 0);
            }
        }
        public void PageDown()
        {
            if (next != pages.Peek())
            {
                page++;
                pages.Push(next);
                location = new Point(0, 0);
            }
        }
        // public accessor functions
        public int[] GetPreview(params object[] args)
        {
            if (args.Length == 5)
                return GetPreview((FontCharacter[])args[0], (int[])args[1], (char[])args[2], (bool)args[3], (bool)args[4]);
            else
                return GetPreview((FontCharacter[])args[0], (int[])args[1], (char[])args[2], (bool)args[3], true);
        }
        public int[] GetPreview(FontCharacter[] fontCharacters, int[] palette, char[] text, bool menu, bool allowclipping)
        {
            this.fontCharacters = fontCharacters;
            this.palette = palette;
            //
            int offset = GetOffset(text, menu);
            if (text.Length <= pages.Peek() + 1)
                PageUp();
            //
            location = new Point(9, 11);
            int[] pixels = new int[256 * 32];
            while (offset != text.Length)
            {
                if (text[offset] >= 0x20 && text[offset] <= 0x9F)
                {
                    if (location.X + fontCharacters[text[offset] - 32].Width >= 256)
                    {
                        AddBorder(pixels);
                        return pixels;
                    }
                    int width;
                    if (!allowclipping)
                        width = MinimumWidth(fontCharacters[text[offset] - 32]);
                    else
                        width = fontCharacters[text[offset] - 32].Width;
                    int maxWidth = fontCharacters[text[offset] - 32].MaxWidth;
                    int[] font = fontCharacters[text[offset] - 32].GetPixels(palette);
                    for (int y = 0, b = location.Y; y < 12; y++, b++) // 12 rows per character
                    {
                        for (int x = 0, a = location.X; x < width; x++, a++) // # of pixels per row
                            pixels[b * 256 + a] = font[y * maxWidth + x];
                    }
                    location.X += width + 1;
                }
                else if (!menu)
                {
                    switch ((byte)text[offset])
                    {
                        case 0x00: // END (End string)
                            offset++;
                            AddBorder(pixels);
                            return pixels;
                        case 0x01: // BREAK (Line break)
                            offset++;
                            next = offset;
                            AddBorder(pixels);
                            return pixels;
                        case 0x1C:
                            offset++;
                            break;
                        default: break;
                    }
                }
                offset++;
            }
            AddBorder(pixels);
            return pixels;
        }
        public int GetOffset(char[] text, bool menu)
        {
            int page = 0;
            int offset = 0;
            while (offset != text.Length)
            {
                if (page == this.page)
                    return offset;
                if (!menu)
                {
                    switch ((byte)text[offset])
                    {
                        case 0x00: // END (End string)
                            offset++;
                            return offset;
                        case 0x01: // BREAK (Line break)
                            page++;
                            offset++;
                            next = offset;
                            continue;
                        case 0x1C:
                            offset++;
                            break;
                        default: break;
                    }
                }
                offset++;
            }
            return offset;
        }
    }
}
