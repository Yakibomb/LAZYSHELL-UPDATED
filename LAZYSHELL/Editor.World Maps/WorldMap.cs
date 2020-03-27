using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class WorldMap : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private byte tileset; public byte Tileset { get { return tileset; } set { tileset = value; } }
        private sbyte x; public sbyte X { get { return x; } set { x = value; } }
        private sbyte y; public sbyte Y { get { return y; } set { y = value; } }
        private byte locations; public byte Locations { get { return locations; } set { locations = value; } }
        // constructor, functions
        public WorldMap(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = index * 3 + 0x3EF800;
            tileset = (byte)rom[offset++];
            x = (sbyte)(rom[offset++] - 1 ^ 255); 
            y = (sbyte)(rom[offset++] - 1 ^ 255); 
            locations = (byte)rom[0x3EF820 + index];
        }
        public void Assemble()
        {
            int offset = index * 3 + 0x3EF800;
            rom[offset++] = tileset; 
            rom[offset++] = (byte)((byte)x - 1 ^ 255);
            rom[offset] = (byte)((byte)y - 1 ^ 255);
            //
            offset = 0x3EF820 + index;
            rom[offset] = locations;
        }
        // universal functions
        public override void Clear()
        {
            tileset = 0;
            x = 0;
            y = 0;
            locations = 0;
        }
    }
}
