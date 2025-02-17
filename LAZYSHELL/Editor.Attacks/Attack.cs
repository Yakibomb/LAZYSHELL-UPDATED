using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class Attack : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public override int Index { get { return index; } set { index = value; } }
        #region class variables
        private int index;
        private char[] name;
        private byte hitRate;
        private byte attackLevel;
        private bool effectMute;
        private bool effectSleep;
        private bool effectPoison;
        private bool effectFear;
        private bool effectBerserk;
        private bool effectMushroom;
        private bool effectScarecrow;
        private bool effectInvincible;
        private bool upAttack;
        private bool upDefense;
        private bool upMagicAttack;
        private bool upMagicDefense;
        private bool instantDeath;
        private bool noDamageA;
        private bool noDamageB;
        private bool hideDigits;
        #endregion
        #region public accessors
        public char[] Name { get { return this.name; } set { this.name = value; } }
        public byte HitRate { get { return this.hitRate; } set { this.hitRate = value; } }
        public byte AttackLevel { get { return this.attackLevel; } set { this.attackLevel = value; } }
        public bool EffectMute { get { return this.effectMute; } set { this.effectMute = value; } }
        public bool EffectSleep { get { return this.effectSleep; } set { this.effectSleep = value; } }
        public bool EffectPoison { get { return this.effectPoison; } set { this.effectPoison = value; } }
        public bool EffectFear { get { return this.effectFear; } set { this.effectFear = value; } }
        public bool EffectBerserk { get { return this.effectBerserk; } set { this.effectBerserk = value; } }
        public bool EffectMushroom { get { return this.effectMushroom; } set { this.effectMushroom = value; } }
        public bool EffectScarecrow { get { return this.effectScarecrow; } set { this.effectScarecrow = value; } }
        public bool EffectInvincible { get { return this.effectInvincible; } set { this.effectInvincible = value; } }
        public bool UpAttack { get { return this.upAttack; } set { this.upAttack = value; } }
        public bool UpDefense { get { return this.upDefense; } set { this.upDefense = value; } }
        public bool UpMagicAttack { get { return this.upMagicAttack; } set { this.upMagicAttack = value; } }
        public bool UpMagicDefense { get { return this.upMagicDefense; } set { this.upMagicDefense = value; } }
        public bool InstantDeath { get { return this.instantDeath; } set { this.instantDeath = value; } }
        public bool NoDamageA { get { return this.noDamageA; } set { this.noDamageA = value; } }
        public bool NoDamageB { get { return this.noDamageB; } set { this.noDamageB = value; } }
        public bool HideDigits { get { return this.hideDigits; } set { this.hideDigits = value; } }
        #endregion
        // constructor
        public Attack(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            name = new char[13];
            for (int i = 0; i < name.Length; i++)
                name[i] = (char)rom[(index * 13) + 0x3959F4 + i];
            int offset = (index * 4) + 0x391226;
            int temp = rom[offset++];
            attackLevel = (byte)(temp & 0x07);
            instantDeath = (temp & 0x08) == 0x08;
            noDamageA = (temp & 0x10) == 0x10;
            hideDigits = (temp & 0x20) == 0x20;
            noDamageB = (temp & 0x40) == 0x40;
            hitRate = rom[offset++];
            // status effect
            Status status = (Status)rom[offset++];
            effectMute = (status & Status.Mute) == Status.Mute;
            effectSleep = (status & Status.Sleep) == Status.Sleep;
            effectPoison = (status & Status.Poison) == Status.Poison;
            effectFear = (status & Status.Fear) == Status.Fear;
            effectBerserk = (status & Status.Berserk) == Status.Berserk;
            effectMushroom = (status & Status.Mushroom) == Status.Mushroom;
            effectScarecrow = (status & Status.Scarecrow) == Status.Scarecrow;
            effectInvincible = (status & Status.Invincible) == Status.Invincible;
            // status change
            temp = rom[offset++];
            upMagicAttack = (temp & 0x08) == 0x08;		// Magic Attack
            upAttack = (temp & 0x10) == 0x10;			// Attack
            upMagicDefense = (temp & 0x20) == 0x20;		// Magic Defense
            upDefense = (temp & 0x40) == 0x40;			// Defense
        }
        public void Assemble()
        {
            Bits.SetChars(rom, 0x3959F4 + (index * 13), name);
            //
            int offset = (index * 4) + 0x391226;
            rom[offset] = attackLevel;
            Bits.SetBit(rom, offset, 3, instantDeath);
            Bits.SetBit(rom, offset, 4, noDamageA);
            Bits.SetBit(rom, offset, 5, hideDigits);
            Bits.SetBit(rom, offset++, 6, noDamageB);
            //
            rom[offset++] = hitRate;
            Bits.SetBit(rom, offset, 0, effectMute);
            Bits.SetBit(rom, offset, 1, effectSleep);
            Bits.SetBit(rom, offset, 2, effectPoison);
            Bits.SetBit(rom, offset, 3, effectFear);
            Bits.SetBit(rom, offset, 4, effectBerserk);
            Bits.SetBit(rom, offset, 5, effectMushroom);
            Bits.SetBit(rom, offset, 6, effectScarecrow);
            Bits.SetBit(rom, offset++, 7, effectInvincible);
            //
            Bits.SetBit(rom, offset, 3, upMagicAttack);
            Bits.SetBit(rom, offset, 4, upAttack);
            Bits.SetBit(rom, offset, 5, upMagicDefense);
            Bits.SetBit(rom, offset, 6, upDefense);
        }
        // universal functions
        public override void Clear()
        {
            Bits.Fill(name, '\x20');
            hitRate = 0;
            attackLevel = 0;
            effectMute = false;
            effectSleep = false;
            effectPoison = false;
            effectFear = false;
            effectBerserk = false;
            effectMushroom = false;
            effectScarecrow = false;
            effectInvincible = false;
            upAttack = false;
            upDefense = false;
            upMagicAttack = false;
            upMagicDefense = false;
            instantDeath = false;
            noDamageA = false;
            noDamageB = false;
            hideDigits = false;
        }
    }
}
