using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class MenuTexts
    {
        // universal variables and accessors
        private int index; public int Index { get { return index; } set { index = value; } }
        private int x; public int X { get { return x; } set { x = value; } }
        [NonSerialized()]
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        public string[] Keystrokes
        {
            get
            {
                if (Dialogue)
                    return Lists.Keystrokes;
                else
                    return Lists.KeystrokesMenu;
            }
        }
        // public variables
        public char[] Text;
        public int Offset;
        public bool Error;
        // public accessors
        public int Length { get { return Text.Length; } }
        public bool Dialogue
        {
            get
            {
                if (index == 24 ||
                    (index >= 33 && index <= 40) ||
                    index == 53 ||
                    index == 54 ||
                    index == 60 ||
                    index == 61 ||
                    (index >= 64 && index <= 75) ||
                    (index >= 88 && index <= 108) ||
                    index == 117)
                    return true;
                return false;
            }
        }
        public Size Size
        {
            get
            {
                int[] palette = Model.FontPaletteMenu.Palette;
                MenuTextPreview preview = new MenuTextPreview();
                int[] pixels = preview.GetPreview(Model.FontMenu, palette, Text, false, false);
                Rectangle rectangle = Do.Crop(pixels, 256, 12);
                return new Size(rectangle.Width, rectangle.Height);
            }
        }
        // constructor
        public MenuTexts(int index)
        {
            this.index = index;
            int offset = Bits.GetShort(Model.ROM, index * 2 + 0x3EEF00) + 0x3EF000;
            List<char> characters = new List<char>();
            while (Model.ROM[offset] != 0)
                characters.Add((char)Model.ROM[offset++]);
            Text = characters.ToArray();
            //
            switch (index)
            {
                case 14: x = (Model.ROM[0x03328E] & 0x3F) / 2; break;
                case 15: x = (Model.ROM[0x03327E] & 0x3F) / 2; break;
                case 16: x = (Model.ROM[0x033282] & 0x3F) / 2; break;
                case 17: x = (Model.ROM[0x033286] & 0x3F) / 2; break;
                case 18: x = (Model.ROM[0x03328A] & 0x3F) / 2; break;
                case 19: x = (Model.ROM[0x03327A] & 0x3F) / 2; break;
            }
        }
        // accessor functions
        public string GetMenuString(bool textView)
        {
            if (!this.Error)
                return new string(textHelper.Decode(Text, !textView, 0, Keystrokes));
            else
                return new string(Text);
        }
        public void SetMenuString(string value, bool textView)
        {
            Text = textHelper.Encode(value.ToCharArray(), !textView, 0, Keystrokes);
        }
        public Rectangle Rectangle(int x, int y)
        {
            Size size = Size;
            return new Rectangle(x, y, size.Width, size.Height);
        }
        // universal functions
        public override string ToString()
        {
            return new string(Text);
        }
    }
}
