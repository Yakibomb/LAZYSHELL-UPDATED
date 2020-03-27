using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelMap
    {
        // class variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        private byte graphicSetA; public byte GraphicSetA { get { return graphicSetA; } set { graphicSetA = value; } }
        private byte graphicSetB; public byte GraphicSetB { get { return graphicSetB; } set { graphicSetB = value; } }
        private byte graphicSetC; public byte GraphicSetC { get { return graphicSetC; } set { graphicSetC = value; } }
        private byte graphicSetD; public byte GraphicSetD { get { return graphicSetD; } set { graphicSetD = value; } }
        private byte graphicSetE; public byte GraphicSetE { get { return graphicSetE; } set { graphicSetE = value; } }
        private byte graphicSetL3; public byte GraphicSetL3 { get { return graphicSetL3; } set { graphicSetL3 = value; } }
        private byte tilesetL1; public byte TilesetL1 { get { return tilesetL1; } set { tilesetL1 = value; } }
        private byte tilesetL2; public byte TilesetL2 { get { return tilesetL2; } set { tilesetL2 = value; } }
        private byte tilesetL3; public byte TilesetL3 { get { return tilesetL3; } set { tilesetL3 = value; } }
        private byte tilemapL1; public byte TilemapL1 { get { return tilemapL1; } set { tilemapL1 = value; } }
        private byte tilemapL2; public byte TilemapL2 { get { return tilemapL2; } set { tilemapL2 = value; } }
        private byte tilemapL3; public byte TilemapL3 { get { return tilemapL3; } set { tilemapL3 = value; } }
        private bool topPriorityL3; public bool TopPriorityL3 { get { return topPriorityL3; } set { topPriorityL3 = value; } }
        private byte solidityMap; public byte SolidityMap { get { return solidityMap; } set { solidityMap = value; } }
        private byte paletteSet; public byte PaletteSet { get { return paletteSet; } set { paletteSet = value; } }
        private byte battlefield; public byte Battlefield { get { return battlefield; } set { battlefield = value; } }
        // constructor
        public LevelMap(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 15) + 0x1D2440;
            graphicSetA = rom[offset++];
            graphicSetB = rom[offset++];
            graphicSetC = rom[offset++];
            graphicSetD = rom[offset++];
            graphicSetE = rom[offset++];
            tilesetL1 = rom[offset++];
            //
            byte temp = rom[offset++];
            topPriorityL3 = (temp & 0x80) == 0x80;
            tilesetL2 = (byte)(temp & 0x7F);
            //
            tilemapL1 = rom[offset++];
            tilemapL2 = rom[offset++];
            solidityMap = rom[offset++];
            paletteSet = rom[offset++];
            graphicSetL3 = rom[offset++];
            tilesetL3 = rom[offset++];
            temp = rom[offset++];
            tilemapL3 = (byte)(temp & 0x3F);
            battlefield = rom[offset];
        }
        public void Assemble()
        {
            int offset = (index * 15) + 0x1D2440;
            rom[offset++] = graphicSetA;
            rom[offset++] = graphicSetB;
            rom[offset++] = graphicSetC;
            rom[offset++] = graphicSetD;
            rom[offset++] = graphicSetE;
            rom[offset++] = tilesetL1;
            //
            rom[offset] = tilesetL2;
            Bits.SetBit(rom, offset++, 7, topPriorityL3);
            //
            rom[offset++] = tilemapL1;
            rom[offset++] = tilemapL2;
            rom[offset++] = solidityMap;
            rom[offset++] = paletteSet;
            rom[offset++] = graphicSetL3;
            rom[offset++] = tilesetL3;
            rom[offset++] = (byte)(tilemapL3 + 0x40);
            rom[offset] = battlefield;
        }
        // universal functions
        public void Clear()
        {
            this.graphicSetA = 0;
            this.graphicSetB = 0;
            this.graphicSetC = 0;
            this.graphicSetD = 0;
            this.graphicSetE = 0;
            this.tilesetL1 = 0;
            this.tilesetL2 = 0;
            this.topPriorityL3 = false;
            this.tilemapL1 = 0;
            this.tilemapL2 = 0;
            this.solidityMap = 0;
            this.paletteSet = 0;
            this.graphicSetL3 = 0;
            this.tilesetL3 = 0;
            this.tilemapL3 = 0;
            this.battlefield = 0;
        }
    }
}
