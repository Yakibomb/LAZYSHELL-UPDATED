using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LAZYSHELL
{
    [Serializable()]
    public class MenuBox : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return this.index; } set { this.index = value; } }
        private byte menu; public byte Menu { get { return this.menu; } set { this.menu = value; } }

        // constructor
        public MenuBox(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = 0x03170D;
            menu = rom[offset + index];
            if (menu >= 9) menu = 0x09;
        }
        public void Assemble()
        {
            int offset = 0x03170D;
            if (menu >= 9) menu = 0xFF;
            rom[offset + index] = menu;
        }
        // universal functions
        public override void Clear()
        {
            byte[] menus = new byte[9] { 0x01, 0x03, 0x02, 0x00, 0x04, 0xFF, 0xFF, 0xFF, 0xFF };
            menus.CopyTo(rom, 0x03170D);
        }
    }
}
