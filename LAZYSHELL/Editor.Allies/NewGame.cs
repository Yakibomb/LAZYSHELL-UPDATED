using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using static LAZYSHELL.NewGame;
using System.Windows.Forms;

namespace LAZYSHELL
{
        [Serializable()]
        public class NewGame : Element
        {
            // universal variables
            private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
            private int index = 0; public override int Index { get { return index; } set { index = value; } }
            #region class variables
            // starting inventory
            private ushort startingCoins;
            private byte startingCurrentFP;
            private byte startingMaximumFP;
            private ushort startingFrogCoins;
            // timing
            private byte defenseStartL1;
            private byte defenseStartL2;
            private byte defenseEndL2;
            private byte defenseEndL1;
            #endregion
            #region accessors
            // starting inventory
            public ushort StartingCoins { get { return this.startingCoins; } set { this.startingCoins = value; } }
            public byte StartingCurrentFP { get { return this.startingCurrentFP; } set { this.startingCurrentFP = value; } }
            public byte StartingMaximumFP { get { return this.startingMaximumFP; } set { this.startingMaximumFP = value; } }
            public ushort StartingFrogCoins { get { return this.startingFrogCoins; } set { this.startingFrogCoins = value; } }
            // timing
            public byte DefenseStartL1 { get { return this.defenseStartL1; } set { this.defenseStartL1 = value; } }
            public byte DefenseStartL2 { get { return this.defenseStartL2; } set { this.defenseStartL2 = value; } }
            public byte DefenseEndL2 { get { return this.defenseEndL2; } set { this.defenseEndL2 = value; } }
            public byte DefenseEndL1 { get { return this.defenseEndL1; } set { this.defenseEndL1 = value; } }
            #endregion
            // constructor
            public NewGame()
            {
                Disassemble();
            }
            // disassembler
            private void Disassemble()
            {
                //
                startingCoins = Bits.GetShort(rom, 0x3A00DB);
                startingCurrentFP = rom[0x3A00DD];
                startingMaximumFP = rom[0x3A00DE];
                startingFrogCoins = Bits.GetShort(rom, 0x3A00DF);
                //
                defenseStartL1 = rom[0x02C9B3];
                defenseStartL2 = rom[0x02C9B9];
                defenseEndL2 = rom[0x02C9BF];
                defenseEndL1 = rom[0x02C9C5];
                //
            }
            public void Assemble()
            {
                Bits.SetShort(rom, 0x3A00DB, startingCoins);
                rom[0x3A00DD] = startingCurrentFP;
                rom[0x3A00DE] = startingMaximumFP;
                Bits.SetShort(rom, 0x3A00DF, startingFrogCoins);
                //
                rom[0x02C9B3] = defenseStartL1;
                rom[0x02C9B9] = defenseStartL2;
                rom[0x02C9BF] = defenseEndL2;
                rom[0x02C9C5] = defenseEndL1;
            }
            public override void Clear()
            {
                startingCoins = 0;
                startingCurrentFP = 0;
                startingMaximumFP = 0;
                startingFrogCoins = 0;
            }
        }
}
