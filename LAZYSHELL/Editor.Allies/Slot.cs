using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class Slot : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private byte equipment; public byte Equipment { get { return this.equipment; } set { this.equipment = value; } }
        private byte item; public byte Item { get { return this.item; } set { this.item = value; } }
        private byte specialItem; public byte SpecialItem { get { return this.specialItem; } set { this.specialItem = value; } }
        // constructor
        public Slot(int index)
        {
            this.index = index;
            Disassemble();
        }
        // disassembler
        private void Disassemble()
        {
            equipment = rom[index + 0x3A0090];
            item = rom[index + 0x3A00AE];
            if (index > 0x0E)
                specialItem = rom[0x0F + 0x3A00CC];
            else specialItem = rom[index + 0x3A00CC];
        }
        // universal functions
        public void Assemble()
        {
            rom[index + 0x3A0090] = equipment;
            rom[index + 0x3A00AE] = item;
            if (index <= 0x0E)
                rom[index + 0x3A00CC] = specialItem;
        }
        public override void Clear()
        {
            equipment = 0;
            item = 0;
            specialItem = 0;
        }
        // class functions
    }
}
