using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class Formation : Element
    {
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // variables
        private byte[] monsters = new byte[8];
        private byte[] x = new byte[8];
        private byte[] y = new byte[8];
        private bool[] use = new bool[8];
        private bool[] hide = new bool[8];
        [NonSerialized()]
        private int[][] pixels = new int[8][];
        private byte music;
        private byte battleEvent;
        private bool cantRun;
        private byte unknown;
        private int elevation;
        private int[] pixelIndexes;
        // accessors
        public byte[] Monsters { get { return this.monsters; } set { this.monsters = value; } }
        public byte[] X { get { return this.x; } set { this.x = value; } }
        public byte[] Y { get { return this.y; } set { this.y = value; } }
        public bool[] Use { get { return this.use; } set { this.use = value; } }
        public bool[] Hide { get { return this.hide; } set { this.hide = value; } }
        public int[][] Pixels { get { return this.pixels; } set { this.pixels = value; } }
        public byte Music { get { return this.music; } set { this.music = value; } }
        public byte BattleEvent { get { return this.battleEvent; } set { this.battleEvent = value; } }
        public bool CantRun { get { return this.cantRun; } set { this.cantRun = value; } }
        public byte Unknown { get { return this.unknown; } set { this.unknown = value; } }
        public int Elevation { get { return this.elevation; } set { this.elevation = value; } }
        // drawing
        public int[] PixelIndexes
        {
            get
            {
                if (pixelIndexes == null)
                    SetPixelIndexes();
                return pixelIndexes;
            }
            set { pixelIndexes = value; }
        }
        private void SetPixelIndexes()
        {
            pixelIndexes = new int[256 * 256];
            Bits.Fill(pixelIndexes, -1);
            int[] order = new int[8];
            for (int i = 0; i < 8; i++)
                order[i] = i;
            byte[] temp = Bits.Copy(this.y);
            Array.Sort(temp, order);
            for (int g = 0; g < 8; g++)
            {
                int i = order[g];
                // If monster is used in formation
                if (use[i])
                {
                    // Get correct monster image
                    int[] pixels = Model.Monsters[monsters[i]].Pixels;
                    int elevation = Model.Monsters[monsters[i]].Elevation * 16;
                    for (int y = 0; y < 256; y++)
                    {
                        for (int x = 0; x < 256; x++)
                        {
                            int x_ = this.x[i] + x - 128;
                            int y_ = this.y[i] + y - 96 - elevation;
                            if ((pixels[y * 256 + x] & 0xFF000000) != 0)
                            {
                                if (x_ > 0 && x_ < 256 && y_ > 0 && y_ < 256)
                                    pixelIndexes[(y_ - 1) * 256 + x_] = i;
                            }
                        }
                    }
                }
            }
        }
        // lists
        public string[] Names
        {
            get
            {
                string[] names = new string[8];
                for (int i = 0; i < 8; i++)
                {
                    if (use[i])
                        names[i] = Do.RawToASCII(Model.Monsters[monsters[i]].Name, Lists.KeystrokesMenu);
                    else
                        names[i] = "{unused}";
                }
                return names;
            }
        }
        public string NamePack
        {
            get
            {
                string[] formationName = this.Names;
                return formationName[0] + "\n" +
                formationName[1] + "\n" +
                formationName[2] + "\n" +
                formationName[3] + "\n" +
                formationName[4] + "\n" +
                formationName[5] + "\n" +
                formationName[6] + "\n" +
                formationName[7];
            }
        }
        public string NameList
        {
            get
            {
                string names = "";
                for (int i = 0; i < 8; i++)
                {
                    if (use[i] == true)
                        names +=
                            Do.RawToASCII(Model.Monsters[monsters[i]].Name, Lists.KeystrokesMenu).Trim() +
                            ((i < 7) ? "  /  " : "");
                    else
                        names += "..." + ((i < 7) ? "  /  " : "");
                }
                return names;
            }
        }
        // constructor
        public Formation(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            byte temp = 0;
            int offset = (index * 0x1A) + 0x39C000;
            temp = rom[offset++];
            use[0] = (temp & 0x80) == 0x80;
            use[1] = (temp & 0x40) == 0x40;
            use[2] = (temp & 0x20) == 0x20;
            use[3] = (temp & 0x10) == 0x10;
            use[4] = (temp & 0x08) == 0x08;
            use[5] = (temp & 0x04) == 0x04;
            use[6] = (temp & 0x02) == 0x02;
            use[7] = (temp & 0x01) == 0x01;
            //
            temp = rom[offset++];
            hide[0] = (temp & 0x80) == 0x80;
            hide[1] = (temp & 0x40) == 0x40;
            hide[2] = (temp & 0x20) == 0x20;
            hide[3] = (temp & 0x10) == 0x10;
            hide[4] = (temp & 0x08) == 0x08;
            hide[5] = (temp & 0x04) == 0x04;
            hide[6] = (temp & 0x02) == 0x02;
            hide[7] = (temp & 0x01) == 0x01;
            for (int i = 0; i < 8; i++)
            {
                monsters[i] = rom[offset++];
                x[i] = rom[offset++];
                y[i] = rom[offset++];
            }
            //
            offset = (index * 3) + 0x392AAA;
            unknown = rom[offset++];
            temp = rom[offset++];
            battleEvent = temp == 0xFF ? (byte)102 : temp;
            temp = rom[offset++];
            cantRun = (temp & 0x03) == 0x03;
            if ((temp & 0xC0) == 0xC0)
                music = 8;
            else
                music = (byte)(temp >> 2);
        }
        public void Assemble()
        {
            int offset = (index * 0x1A) + 0x39C000;
            Bits.SetBit(rom, offset, 7, use[0]);
            Bits.SetBit(rom, offset, 6, use[1]);
            Bits.SetBit(rom, offset, 5, use[2]);
            Bits.SetBit(rom, offset, 4, use[3]);
            Bits.SetBit(rom, offset, 3, use[4]);
            Bits.SetBit(rom, offset, 2, use[5]);
            Bits.SetBit(rom, offset, 1, use[6]);
            Bits.SetBit(rom, offset++, 0, use[7]);
            //
            Bits.SetBit(rom, offset, 7, hide[0]);
            Bits.SetBit(rom, offset, 6, hide[1]);
            Bits.SetBit(rom, offset, 5, hide[2]);
            Bits.SetBit(rom, offset, 4, hide[3]);
            Bits.SetBit(rom, offset, 3, hide[4]);
            Bits.SetBit(rom, offset, 2, hide[5]);
            Bits.SetBit(rom, offset, 1, hide[6]);
            Bits.SetBit(rom, offset++, 0, hide[7]);
            //
            for (int i = 0; i < 8; i++)
            {
                rom[offset++] = monsters[i];
                rom[offset++] = x[i];
                rom[offset++] = y[i];
            }
            //
            offset = (index * 3) + 0x392AAA;
            rom[offset++] = unknown; 
            if (battleEvent == 102)
                rom[offset++] = 0xff;
            else
                rom[offset++] = battleEvent;
            if (music == 8)
                rom[offset] = 0xC0;
            else
                rom[offset] = (byte)(music << 2);
            Bits.SetBitsByByte(rom, offset++, 0x03, cantRun);
        }
        // universal functions
        public override void Clear()
        {
            monsters = new byte[8];
            x = new byte[8];
            y = new byte[8];
            use = new bool[8];
            hide = new bool[8];
            music = 8;
            battleEvent = 102;
            cantRun = false;
            unknown = 0;
        }
        public override string ToString()
        {
            return this.NameList;
        }
    }
}
