using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class Character : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // external selectors
        private byte indexLevel = 2;
        #region class variables
        private LevelUp[] levels;
        private char[] name;
        // starting stats
        private byte startingLevel;
        private ushort startingCurrentHP;
        private ushort startingMaxHP;
        private byte startingSpeed;
        private byte startingAttack;
        private byte startingDefense;
        private byte startingMgAttack;
        private byte startingMgDefense;
        private ushort startingExperience;
        private byte startingWeapon;
        private byte startingArmor;
        private byte startingAccessory;
        private bool[] startingMagic;
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
        public byte IndexLevel { get { return this.indexLevel; } set { this.indexLevel = value; } }
        public LevelUp[] Levels { get { return levels; } }
        public char[] Name { get { return this.name; } set { this.name = value; } }
        // starting stats
        public byte StartingLevel { get { return this.startingLevel; } set { this.startingLevel = value; } }
        public ushort StartingCurrentHP { get { return this.startingCurrentHP; } set { this.startingCurrentHP = value; } }
        public ushort StartingMaxHP { get { return this.startingMaxHP; } set { this.startingMaxHP = value; } }
        public byte StartingSpeed { get { return this.startingSpeed; } set { this.startingSpeed = value; } }
        public byte StartingAttack { get { return this.startingAttack; } set { this.startingAttack = value; } }
        public byte StartingDefense { get { return this.startingDefense; } set { this.startingDefense = value; } }
        public byte StartingMgAttack { get { return this.startingMgAttack; } set { this.startingMgAttack = value; } }
        public byte StartingMgDefense { get { return this.startingMgDefense; } set { this.startingMgDefense = value; } }
        public ushort StartingExperience { get { return this.startingExperience; } set { this.startingExperience = value; } }
        public byte StartingWeapon { get { return this.startingWeapon; } set { this.startingWeapon = value; } }
        public byte StartingArmor { get { return this.startingArmor; } set { this.startingArmor = value; } }
        public byte StartingAccessory { get { return this.startingAccessory; } set { this.startingAccessory = value; } }
        // starting magic
        public bool[] StartingMagic { get { return startingMagic; } set { startingMagic = value; } }
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
        // level-up
        public ushort LevelExpNeeded { get { return this.levels[indexLevel].ExpNeeded; } set { this.levels[indexLevel].ExpNeeded = value; } }
        public byte LevelSpellLearned { get { return this.levels[indexLevel].SpellLearned; } set { this.levels[indexLevel].SpellLearned = value; } }
        public byte LevelHpPlus { get { return this.levels[indexLevel].HpPlus; } set { this.levels[indexLevel].HpPlus = value; } }
        public byte LevelAttackPlus { get { return this.levels[indexLevel].AttackPlus; } set { this.levels[indexLevel].AttackPlus = value; } }
        public byte LevelDefensePlus { get { return this.levels[indexLevel].DefensePlus; } set { this.levels[indexLevel].DefensePlus = value; } }
        public byte LevelMgAttackPlus { get { return this.levels[indexLevel].MgAttackPlus; } set { this.levels[indexLevel].MgAttackPlus = value; } }
        public byte LevelMgDefensePlus { get { return this.levels[indexLevel].MgDefensePlus; } set { this.levels[indexLevel].MgDefensePlus = value; } }
        public byte LevelHpPlusBonus { get { return this.levels[indexLevel].HpPlusBonus; } set { this.levels[indexLevel].HpPlusBonus = value; } }
        public byte LevelAttackPlusBonus { get { return this.levels[indexLevel].AttackPlusBonus; } set { this.levels[indexLevel].AttackPlusBonus = value; } }
        public byte LevelDefensePlusBonus { get { return this.levels[indexLevel].DefensePlusBonus; } set { this.levels[indexLevel].DefensePlusBonus = value; } }
        public byte LevelMgAttackPlusBonus { get { return this.levels[indexLevel].MgAttackPlusBonus; } set { this.levels[indexLevel].MgAttackPlusBonus = value; } }
        public byte LevelMgDefensePlusBonus { get { return this.levels[indexLevel].MgDefensePlusBonus; } set { this.levels[indexLevel].MgDefensePlusBonus = value; } }
        #endregion
        // constructor
        public Character(int index)
        {
            this.index = index;
            this.indexLevel = 2;
            Disassemble();
        }
        // disassembler
        private void Disassemble()
        {
            int offset = (index * 20) + 0x3A002C;
            //
            startingLevel = rom[offset++];
            startingCurrentHP = Bits.GetShort(rom, offset); offset += 2;
            startingMaxHP = Bits.GetShort(rom, offset); offset += 2;
            startingSpeed = rom[offset++];
            startingAttack = rom[offset++];
            startingDefense = rom[offset++];
            startingMgAttack = rom[offset++];
            startingMgDefense = rom[offset++];
            startingExperience = Bits.GetShort(rom, offset); offset += 2;
            startingWeapon = rom[offset++];
            startingArmor = rom[offset++];
            startingAccessory = rom[offset]; offset += 2;
            //
            startingMagic = new bool[32];
            int a = 0;
            for (int o = 0; o < 4; o++, offset++)
                for (int i = 0; i < 8; i++)
                    startingMagic[a++] = Bits.GetBit(rom, offset, i);
            // set up the levels
            levels = new LevelUp[31];
            for (int i = 2; i < levels.Length; i++)
                levels[i] = new LevelUp(i, index);
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
            name = new char[10];
            for (int i = 0; i < name.Length; i++)
                name[i] = (char)rom[(index * 10) + 0x3a134d + i];
        }
        public void Assemble()
        {
            int offset = (index * 20) + 0x3A002C;
            //
            rom[offset++] = startingLevel;
            Bits.SetShort(rom, offset, startingCurrentHP); offset += 2;
            Bits.SetShort(rom, offset, startingMaxHP); offset += 2;
            rom[offset++] = startingSpeed;
            rom[offset++] = startingAttack;
            rom[offset++] = startingDefense;
            rom[offset++] = startingMgAttack;
            rom[offset++] = startingMgDefense;
            Bits.SetShort(rom, offset, startingExperience); offset += 2;
            rom[offset++] = startingWeapon;
            rom[offset++] = startingArmor;
            Bits.SetByte(rom, offset, startingAccessory); offset += 2;
            //
            int a = 0;
            for (int o = 0; o < 4; o++, offset++)
                for (int i = 0; i < 8; i++)
                    Bits.SetBit(rom, offset, i, startingMagic[a++]);
            //
            foreach (LevelUp l in levels)
                if (l != null)
                    l.Assemble();
            if (index == 0)
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
            Bits.SetChars(rom, 0x3a134d + (index * 10), name);
        }
        // functions
        public override string ToString()
        {
            return new string(name);
        }
        public override void Clear()
        {
            Bits.Fill(name, '\x20');
            startingLevel = 1;
            startingCurrentHP = 0;
            startingMaxHP = 0;
            startingSpeed = 0;
            startingAttack = 0;
            startingDefense = 0;
            startingMgAttack = 0;
            startingMgDefense = 0;
            startingExperience = 0;
            startingWeapon = 0;
            startingArmor = 0;
            startingAccessory = 0;
            startingMagic = new bool[32];
            startingCoins = 0;
            startingCurrentFP = 0;
            startingMaximumFP = 0;
            startingFrogCoins = 0;
            foreach (LevelUp levelUp in levels)
                if (levelUp != null)
                    levelUp.Clear();
        }
        [Serializable()]
        public class LevelUp
        {
            // universal variables
            private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
            private int index; public int Index { get { return index; } }
            // class variables
            private int owner;
            // properties
            private ushort expNeeded; public ushort ExpNeeded { get { return this.expNeeded; } set { this.expNeeded = value; } }
            private byte spellLearned; public byte SpellLearned { get { return this.spellLearned; } set { this.spellLearned = value; } }
            // increments
            private byte hpPlus; public byte HpPlus { get { return this.hpPlus; } set { this.hpPlus = value; } }
            private byte attackPlus; public byte AttackPlus { get { return this.attackPlus; } set { this.attackPlus = value; } }
            private byte defensePlus; public byte DefensePlus { get { return this.defensePlus; } set { this.defensePlus = value; } }
            private byte mgAttackPlus; public byte MgAttackPlus { get { return this.mgAttackPlus; } set { this.mgAttackPlus = value; } }
            private byte mgDefensePlus; public byte MgDefensePlus { get { return this.mgDefensePlus; } set { this.mgDefensePlus = value; } }
            // bonus
            private byte hpPlusBonus; public byte HpPlusBonus { get { return this.hpPlusBonus; } set { this.hpPlusBonus = value; } }
            private byte attackPlusBonus; public byte AttackPlusBonus { get { return this.attackPlusBonus; } set { this.attackPlusBonus = value; } }
            private byte defensePlusBonus; public byte DefensePlusBonus { get { return this.defensePlusBonus; } set { this.defensePlusBonus = value; } }
            private byte mgAttackPlusBonus; public byte MgAttackPlusBonus { get { return this.mgAttackPlusBonus; } set { this.mgAttackPlusBonus = value; } }
            private byte mgDefensePlusBonus; public byte MgDefensePlusBonus { get { return this.mgDefensePlusBonus; } set { this.mgDefensePlusBonus = value; } }
            // constructor
            public LevelUp(int index, int owner)
            {
                this.index = index;
                this.owner = owner;
                Disassemble();
            }
            // disassembler
            private void Disassemble()
            {
                int offset = 0x3A1AFF + ((index - 2) * 2);
                expNeeded = Bits.GetShort(rom, offset);
                //
                byte temp;
                offset = (owner * 3) + ((index - 2) * 15) + 0x3A1B39;
                hpPlus = rom[offset++];
                temp = rom[offset]; attackPlus = (byte)((temp & 0xF0) >> 4);
                temp = rom[offset]; defensePlus = (byte)((temp & 0x0F)); offset++;
                temp = rom[offset]; mgAttackPlus = (byte)((temp & 0xF0) >> 4);
                temp = rom[offset]; mgDefensePlus = (byte)((temp & 0x0F)); offset++;
                //
                offset = (owner * 3) + ((index - 2) * 15) + 0x3A1CEC;
                hpPlusBonus = rom[offset++];
                temp = rom[offset]; attackPlusBonus = (byte)((temp & 0xF0) >> 4);
                temp = rom[offset]; defensePlusBonus = (byte)((temp & 0x0F)); offset++;
                temp = rom[offset]; mgAttackPlusBonus = (byte)((temp & 0xF0) >> 4);
                temp = rom[offset]; mgDefensePlusBonus = (byte)((temp & 0x0F)); offset++;
                //
                spellLearned = rom[owner + ((index - 2) * 5) + 0x3A42F5];
                if (spellLearned > 0x1F)
                    spellLearned = 0x20;
            }
            // universal functions
            public void Assemble()
            {
                int offset = ((index - 2) * 2) + 0x3A1AFF;
                if (owner == 0)
                    Bits.SetShort(rom, offset, expNeeded);
                //
                offset = (owner * 3) + ((index - 2) * 15) + 0x3A1B39;
                Bits.SetByte(rom, offset, hpPlus); offset++;
                Bits.SetByte(rom, offset, (byte)((attackPlus << 4) + defensePlus)); offset++;
                Bits.SetByte(rom, offset, (byte)((mgAttackPlus << 4) + mgDefensePlus)); offset++;
                //
                offset = (owner * 3) + ((index - 2) * 15) + 0x3A1CEC;
                Bits.SetByte(rom, offset, hpPlusBonus); offset++;
                Bits.SetByte(rom, offset, (byte)((attackPlusBonus << 4) + defensePlusBonus)); offset++;
                Bits.SetByte(rom, offset, (byte)((mgAttackPlusBonus << 4) + mgDefensePlusBonus)); offset++;
                //
                if (spellLearned == 0x20)
                    rom[owner + ((index - 2) * 5) + 0x3A42F5] = 0xFF;
                else
                    rom[owner + ((index - 2) * 5) + 0x3A42F5] = spellLearned;
            }
            public void Clear()
            {
                expNeeded = 0;
                hpPlus = 0;
                attackPlus = 0;
                defensePlus = 0;
                mgAttackPlus = 0;
                mgDefensePlus = 0;
                hpPlusBonus = 0;
                attackPlusBonus = 0;
                defensePlusBonus = 0;
                mgAttackPlusBonus = 0;
                mgDefensePlusBonus = 0;
                spellLearned = 32;
            }
            // class functions
        }
    }
}
