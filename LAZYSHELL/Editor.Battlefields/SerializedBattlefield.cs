using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class SerializedBattlefield
    {
        // class variables
        public Battlefield battlefield;
        public byte[] tileset;
        public byte graphicSetA;
        public byte graphicSetB;
        public byte graphicSetC;
        public byte graphicSetD;
        public byte graphicSetE;
        public PaletteSet paletteSet;
        // constructors
        public SerializedBattlefield(byte[] tileset, PaletteSet paletteSet, Battlefield battlefield)
        {
            this.tileset = tileset;
            this.paletteSet = paletteSet;
            this.battlefield = battlefield;
            this.graphicSetA = battlefield.GraphicSetA;
            this.graphicSetB = battlefield.GraphicSetB;
            this.graphicSetC = battlefield.GraphicSetC;
            this.graphicSetD = battlefield.GraphicSetD;
            this.graphicSetE = battlefield.GraphicSetE;
        }
        public SerializedBattlefield()
        {
        }
    }
}
