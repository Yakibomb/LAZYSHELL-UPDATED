using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    class DialoguePreview
    {
        // class variables
        private FontCharacter[] fontCharacters;
        private FontCharacter[] fontTriangles;
        private int[] palette;
        private int[] palette_t;
        private Point location;
        private int next = 0;
        private int page = 0;
        private bool drawPageBreak;
        private bool[] drawOptions;
        private int[] lineOffset = new int[3];
        private Stack<int> pages = new Stack<int>();
        // accessors
        private DTE[] tables { get { return Model.DTE; } }
        // constructor
        public DialoguePreview()
        {
            pages.Push(0);
        }
        // accessor functions
        public int[] GetPreview(FontCharacter[] fontCharacters, FontCharacter[] fontTriangles, 
            int[] palette, int[] palette_t, char[] text, int left)
        {
            this.fontCharacters = fontCharacters;
            this.fontTriangles = fontTriangles;
            this.palette = palette;
            this.palette_t = palette_t;
            //
            drawOptions = new bool[3];
            drawPageBreak = false;
            location = new Point(left + 1, 6);
            //
            text = ConvertSpecialCases(text);
            int offset = GetOffset(text, left);
            location = new Point(left + 1, 6);
            if (text.Length <= pages.Peek() + 1)
                PageUp();
            //
            int line = 0;
            int[] pixels = new int[256 * 56];
            ArrayList words = new ArrayList();
            AddWords(words, text, offset);
            foreach (List<char> word in words)
            {
                int wordWidth = WordWidth(word);
                if (location.X + wordWidth >= 256 - left)
                {
                    if (line == 2)
                    {
                        next = lineOffset[1];
                        AddBorder(pixels);
                        AddTriangles(pixels);
                        return pixels;
                    }
                    line++;
                    lineOffset[line] = offset + 1;
                    location.X = left + 1; location.Y += 16;
                }
                foreach (char l in word)
                {
                    if (l >= 0x20 && l <= 0x9F)
                    {
                        int height = fontCharacters[l - 32].Height;
                        int width = fontCharacters[l - 32].Width;
                        int maxWidth = fontCharacters[l - 32].MaxWidth;
                        int[] font = fontCharacters[l - 32].GetPixels(palette);
                        if (location.X + width >= 256 - left)
                        {
                            if (line == 2)
                            {
                                next = lineOffset[1];
                                AddBorder(pixels);
                                AddTriangles(pixels);
                                return pixels;
                            }
                            line++;
                            lineOffset[line] = offset + 1;
                            location.X = left + 1; location.Y += 16;
                            break;
                        }
                        for (int y = 0, b = location.Y; y < height; y++, b++) // 12 rows per character
                            for (int x = 0, a = location.X; x < width; x++, a++) // # of pixels per row
                                pixels[b * 256 + a] = font[y * maxWidth + x];
                        location.X += width + 1;
                    }
                    else
                    {
                        switch ((byte)l)
                        {
                            case 0x00: goto case 0x06; // End String Press A
                            case 0x06:
                                AddBorder(pixels);
                                AddTriangles(pixels);
                                return pixels;
                            case 0x01: // Line Break
                            case 0x02:
                                if (line == 2)
                                {
                                    next = lineOffset[1];
                                    AddBorder(pixels);
                                    AddTriangles(pixels);
                                    return pixels;
                                }
                                line++;
                                lineOffset[line] = offset + 1;
                                location.X = left + 1; location.Y += 16;
                                break;
                            case 0x03:  // Page Break Press A
                                drawPageBreak = true;
                                goto case 0x04;
                            case 0x04:
                                offset++;
                                next = offset;
                                AddBorder(pixels);
                                AddTriangles(pixels);
                                return pixels;
                            case 0x07: drawOptions[line] = true; break;
                            default: break;
                        }
                    }
                    offset++;
                }
            }
            AddBorder(pixels);
            AddTriangles(pixels);
            return pixels;
        }
        private int GetOffset(char[] text, int left)
        {
            int page = 0;
            int offset = 0;
            int line = 0;
            ArrayList words = new ArrayList();
            AddWords(words, text, offset);
            foreach (List<char> word in words)
            {
                if (page == this.page)
                    return offset;
                int wordWidth = WordWidth(word);
                if (location.X + wordWidth >= 256 - left)
                {
                    if (line == 2)
                    {
                        page++;
                        line = 0;
                        next = lineOffset[2];
                        if (page == this.page)
                            return lineOffset[2];
                    }
                    line++;
                    lineOffset[line] = offset + 1;
                    location.X = left + 1; location.Y += 16;
                }
                foreach (char l in word)
                {
                    if (l >= 0x20 && l <= 0x9F)
                    {
                        int width = fontCharacters[l - 32].Width;
                        if (location.X + width >= 256 - left)
                        {
                            if (line == 2)
                            {
                                page++;
                                line = 0;
                                next = lineOffset[2];
                                if (page == this.page)
                                    return lineOffset[2];
                            }
                            line++;
                            lineOffset[line] = offset + 1;
                            location.X = left + 1; location.Y += 16;
                            break;
                        }
                        location.X += width + 1;
                    }
                    else
                    {
                        switch ((byte)l)
                        {
                            case 0x00: // End string
                            case 0x06:
                                return offset;
                            case 0x01: // Line Break
                            case 0x02:
                                // if line break after 3rd line
                                if (line == 2)
                                {
                                    page++;
                                    line = 0;
                                    next = lineOffset[2];
                                    if (page == this.page)
                                        return lineOffset[2];
                                }
                                line++;
                                lineOffset[line] = offset + 1;
                                location.X = left + 1; location.Y += 16;
                                break;
                            case 0x03:  // Page Break Press A
                            case 0x04:
                                page++;
                                line = 0;
                                next = offset + 1;
                                if (page == this.page)
                                    return offset + 1;
                                break;
                            default: break;
                        }
                    }
                    offset++;
                }
            }
            return offset;
        }
        // class functions
        private char[] ConvertSpecialCases(char[] text)
        {
            List<char> letters = new List<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= 0x0E && text[i] <= 0x19)
                {
                    letters.AddRange(tables[text[i] - 0x0E].Text);
                    continue;
                }
                switch ((byte)text[i])
                {
                    // eliminate, will NOT affect drawing
                    case 0x05: break;
                    case 0x0C: break;
                    case 0x0D: i++; break;
                    case 0x1A: break;
                    case 0x1C: i++; break;
                    // 2 or more characters
                    case 0x08: letters.AddRange("  ".ToCharArray()); break;
                    case 0x09: letters.AddRange("   ".ToCharArray()); break;
                    case 0x0A: letters.AddRange("    ".ToCharArray()); break;
                    case 0x0B:
                        i++;
                        for (int a = 0; i < text.Length && a < text[i]; a++)
                            letters.Add((char)0x20);
                        break;
                    // 1 regular character >= 0x20
                    default: letters.Add(text[i]); break;
                }
            }
            return letters.ToArray();
        }
        private int WordWidth(List<char> word)
        {
            int width = 0;
            foreach (char l in word)
            {
                if (l >= 0x20 && l <= 0x9F)
                    width += fontCharacters[l - 32].Width + 1;
            }
            return width;
        }
        // drawing
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
        private void AddTriangles(int[] pixels)
        {
            Point location = new Point(17, 4);
            int[] triangle = fontTriangles[0].GetPixels(palette_t);
            for (int i = 0; i < 3; i++)
            {
                location.Y = i * 16 + 4;
                if (drawOptions[i])
                {
                    for (int y = 0, b = location.Y; y < 16; y++, b++) // # of rows
                    {
                        for (int x = 0, a = location.X; x < 8; x++, a++) // 1 row6
                            pixels[b * 256 + a] = triangle[y * 8 + x];
                    }
                }
            }
            if (drawPageBreak)
            {
                triangle = fontTriangles[7].GetPixels(palette_t);
                location = new Point(224, 44);
                for (int y = 0, b = location.Y; y < 8; y++, b++) // # of rows
                {
                    for (int x = 0, a = location.X; x < 16; x++, a++) // 1 row6
                        pixels[b * 256 + a] = triangle[y * 16 + x];
                }
            }
        }
        private void AddWords(ArrayList words, char[] text, int offset)
        {
            List<char> letters;
            while (offset < text.Length)
            {
                letters = new List<char>();
                if (text[offset] <= 0x20)   // create a single word from special case or 1 space
                {
                    letters.Add(text[offset]); offset++;
                }
                else   // create word from regular characters
                {
                    for (; offset < text.Length; offset++)
                    {
                        // stop adding characters if next character is...
                        if (text[offset] >= 0x00 && text[offset] <= 0x04) break;
                        if (text[offset] == 0x06) break;
                        if (text[offset] >= 0x0E && text[offset] <= 0x19) break;
                        if (text[offset] == 0x1B) break;
                        if (text[offset] == 0x20) break;
                        // ...otherwise add next character
                        letters.Add(text[offset]);
                    }
                }
                words.Add(letters);
            }
        }
        // public functions
        public void Reset()
        {
            next = 0;
            pages.Clear();
            pages.Push(0);
            location = new Point(0, 0);
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
    }
}
