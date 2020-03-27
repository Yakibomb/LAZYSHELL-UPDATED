using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LAZYSHELL
{
    [Serializable()]
    public class Battlefield
    {
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        private byte graphicSetA; public byte GraphicSetA { get { return graphicSetA; } set { graphicSetA = value; } }
        private byte graphicSetB; public byte GraphicSetB { get { return graphicSetB; } set { graphicSetB = value; } }
        private byte graphicSetC; public byte GraphicSetC { get { return graphicSetC; } set { graphicSetC = value; } }
        private byte graphicSetD; public byte GraphicSetD { get { return graphicSetD; } set { graphicSetD = value; } }
        private byte graphicSetE; public byte GraphicSetE { get { return graphicSetE; } set { graphicSetE = value; } }
        private byte tileSet; public byte TileSet { get { return tileSet; } set { tileSet = value; } }
        private byte paletteSet; public byte PaletteSet { get { return paletteSet; } set { paletteSet = value; } }
        // constructor
        public Battlefield(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 8) + 0x39B644;
            graphicSetA = rom[offset++];
            graphicSetB = rom[offset++];
            graphicSetC = rom[offset++];
            graphicSetD = rom[offset++];
            graphicSetE = rom[offset]; offset += 2;
            if (graphicSetA > 0xC7) graphicSetA = 0xC8;
            if (graphicSetB > 0xC7) graphicSetB = 0xC8;
            if (graphicSetC > 0xC7) graphicSetC = 0xC8;
            if (graphicSetD > 0xC7) graphicSetD = 0xC8;
            if (graphicSetE > 0xC7) graphicSetE = 0xC8;
            tileSet = rom[offset++];
            paletteSet = rom[offset++];
        }
        public void Assemble()
        {
            int offset = (index * 8) + 0x39B644;
            if (graphicSetA == 0xC8) rom[offset] = 0xFF;
            else rom[offset] = graphicSetA; offset++;
            if (graphicSetB == 0xC8) rom[offset] = 0xFF;
            else rom[offset] = graphicSetB; offset++;
            if (graphicSetC == 0xC8) rom[offset] = 0xFF;
            else rom[offset] = graphicSetC; offset++;
            if (graphicSetD == 0xC8) rom[offset] = 0xFF;
            else rom[offset] = graphicSetD; offset++;
            if (graphicSetE == 0xC8) rom[offset] = 0xFF;
            else rom[offset] = graphicSetE; offset += 2;
            rom[offset] = tileSet; offset++;
            rom[offset] = paletteSet; offset++;
        }
    }
}
