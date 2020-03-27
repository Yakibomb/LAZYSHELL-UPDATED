using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    public class PrioritySet
    {
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index;
        // priority set properties
        private bool mainscreenL1; public bool MainscreenL1 { get { return mainscreenL1; } set { mainscreenL1 = value; } }
        private bool mainscreenL2; public bool MainscreenL2 { get { return mainscreenL2; } set { mainscreenL2 = value; } }
        private bool mainscreenL3; public bool MainscreenL3 { get { return mainscreenL3; } set { mainscreenL3 = value; } }
        private bool mainscreenOBJ; public bool MainscreenOBJ { get { return mainscreenOBJ; } set { mainscreenOBJ = value; } }
        private bool subscreenL1; public bool SubscreenL1 { get { return subscreenL1; } set { subscreenL1 = value; } }
        private bool subscreenL2; public bool SubscreenL2 { get { return subscreenL2; } set { subscreenL2 = value; } }
        private bool subscreenL3; public bool SubscreenL3 { get { return subscreenL3; } set { subscreenL3 = value; } }
        private bool subscreenOBJ; public bool SubscreenOBJ { get { return subscreenOBJ; } set { subscreenOBJ = value; } }
        private bool colorMathL1; public bool ColorMathL1 { get { return colorMathL1; } set { colorMathL1 = value; } }
        private bool colorMathL2; public bool ColorMathL2 { get { return colorMathL2; } set { colorMathL2 = value; } }
        private bool colorMathL3; public bool ColorMathL3 { get { return colorMathL3; } set { colorMathL3 = value; } }
        private bool colorMathOBJ; public bool ColorMathOBJ { get { return colorMathOBJ; } set { colorMathOBJ = value; } }
        private bool colorMathBG; public bool ColorMathBG { get { return colorMathBG; } set { colorMathBG = value; } }
        private byte colorMathHalfIntensity; public byte ColorMathHalfIntensity { get { return colorMathHalfIntensity; } set { colorMathHalfIntensity = value; } }
        private byte colorMathMinusSubscreen; public byte ColorMathMinusSubscreen { get { return colorMathMinusSubscreen; } set { colorMathMinusSubscreen = value; } }
        // constructor, functions
        public PrioritySet(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int layerPriorityOffset = (index * 3) + 0x1D0000;
            int temp = rom[layerPriorityOffset]; layerPriorityOffset++;
            mainscreenL1 = (temp & 0x01) == 0x01;
            mainscreenL2 = (temp & 0x02) == 0x02;
            mainscreenL3 = (temp & 0x04) == 0x04;
            mainscreenOBJ = (temp & 0x10) == 0x10;
            temp = rom[layerPriorityOffset]; layerPriorityOffset++;
            subscreenL1 = (temp & 0x01) == 0x01;
            subscreenL2 = (temp & 0x02) == 0x02;
            subscreenL3 = (temp & 0x04) == 0x04;
            subscreenOBJ = (temp & 0x10) == 0x10;
            temp = rom[layerPriorityOffset]; layerPriorityOffset++;
            colorMathL1 = (temp & 0x01) == 0x01;
            colorMathL2 = (temp & 0x02) == 0x02;
            colorMathL3 = (temp & 0x04) == 0x04;
            colorMathOBJ = (temp & 0x10) == 0x10;
            colorMathBG = (temp & 0x20) == 0x20;
            if ((temp & 0x40) == 0x40) colorMathHalfIntensity = 1; else colorMathHalfIntensity = 0;
            if ((temp & 0x80) == 0x80) colorMathMinusSubscreen = 1; else colorMathMinusSubscreen = 0;
        }
        public void Assemble()
        {
            int offset = (index * 3) + 0x1D0000;
            Bits.SetBit(rom, offset, 0, mainscreenL1);
            Bits.SetBit(rom, offset, 1, mainscreenL2);
            Bits.SetBit(rom, offset, 2, mainscreenL3);
            Bits.SetBit(rom, offset, 4, mainscreenOBJ);
            offset++;
            Bits.SetBit(rom, offset, 0, subscreenL1);
            Bits.SetBit(rom, offset, 1, subscreenL2);
            Bits.SetBit(rom, offset, 2, subscreenL3);
            Bits.SetBit(rom, offset, 4, subscreenOBJ);
            offset++;
            Bits.SetBit(rom, offset, 0, colorMathL1);
            Bits.SetBit(rom, offset, 1, colorMathL2);
            Bits.SetBit(rom, offset, 2, colorMathL3);
            Bits.SetBit(rom, offset, 4, colorMathOBJ);
            Bits.SetBit(rom, offset, 5, colorMathBG);
            if (colorMathHalfIntensity == 1) Bits.SetBit(rom, offset, 6, true); else Bits.SetBit(rom, offset, 6, false);
            if (colorMathMinusSubscreen == 1) Bits.SetBit(rom, offset, 7, true); else Bits.SetBit(rom, offset, 7, false);
        }
    }
}