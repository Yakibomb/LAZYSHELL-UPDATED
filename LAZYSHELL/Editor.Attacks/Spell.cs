using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class Spell : Element
    {
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public override int Index { get { return index; } set { index = value; } }
        #region class variables
        private int index;
        private char[] name;
        private char[] description;
        private bool descriptionError = false;
        private int caretPosByteView = 0;
        private int caretPosTextView = 0;
        private byte fpCost;
        private byte magicPower;
        private byte hitRate;
        private byte attackType;
        private byte effectType;
        private byte inflictFunction;
        private byte inflictElement;
        private bool checkStats;
        private bool ignoreDefense;
        private bool checkMortality;
        private bool usableOverworld;
        private bool maxAttack;
        private bool hideDigits;
        private bool effectMute;
        private bool effectSleep;
        private bool effectPoison;
        private bool effectFear;
        private bool effectBerserk;
        private bool effectMushroom;
        private bool effectScarecrow;
        private bool effectInvincible;
        private bool changeAttack;
        private bool changeDefense;
        private bool changeMagicAttack;
        private bool changeMagicDefense;
        private bool targetLiveAlly;
        private bool targetEnemy;
        private bool targetAll;
        private bool targetWoundedOnly;
        private bool targetOnePartyOnly;
        private bool targetNotSelf;
        // timing
        private ushort timingPointer;
        private ushort damagePointer;
        #endregion
        #region public accessors
        public char[] Name { get { return this.name; } set { this.name = value; } }
        // description
        private TextHelperReduced textHelperReduced { get { return TextHelperReduced.Instance; } }
        public char[] RawDescription { get { return this.description; } set { this.description = value; } }
        public bool SetDescription(string value, bool byteView)
        {
            this.description = textHelperReduced.Encode(value.ToCharArray(), byteView, 1, Lists.KeystrokesDesc);
            this.descriptionError = textHelperReduced.Error;
            return !descriptionError;
        }
        public string GetDescription(bool byteView)
        {
            if (!descriptionError)
                return new string(textHelperReduced.Decode(description, byteView, 1, Lists.KeystrokesDesc));
            else
                return new string(description);
        }
        public bool DescriptionError { get { return this.descriptionError; } set { this.descriptionError = value; } }
        public int CaretPosByteView { get { return this.caretPosByteView; } set { this.caretPosByteView = value; } }
        public int CaretPosTextView { get { return this.caretPosTextView; } set { this.caretPosTextView = value; } }
        // stats
        public byte FPCost { get { return this.fpCost; } set { this.fpCost = value; } }
        public byte MagicPower { get { return this.magicPower; } set { this.magicPower = value; } }
        public byte HitRate { get { return this.hitRate; } set { this.hitRate = value; } }
        public byte AttackType { get { return this.attackType; } set { this.attackType = value; } }
        public byte EffectType { get { return this.effectType; } set { this.effectType = value; } }
        public byte InflictFunction { get { return this.inflictFunction; } set { this.inflictFunction = value; } }
        public byte InflictElement { get { return this.inflictElement; } set { this.inflictElement = value; } }
        public bool CheckStats { get { return this.checkStats; } set { this.checkStats = value; } }
        public bool IgnoreDefense { get { return this.ignoreDefense; } set { this.ignoreDefense = value; } }
        public bool CheckMortality { get { return this.checkMortality; } set { this.checkMortality = value; } }
        public bool UsableOverworld { get { return this.usableOverworld; } set { this.usableOverworld = value; } }
        public bool MaxAttack { get { return this.maxAttack; } set { this.maxAttack = value; } }
        public bool HideDigits { get { return this.hideDigits; } set { this.hideDigits = value; } }
        public bool EffectMute { get { return this.effectMute; } set { this.effectMute = value; } }
        public bool EffectSleep { get { return this.effectSleep; } set { this.effectSleep = value; } }
        public bool EffectPoison { get { return this.effectPoison; } set { this.effectPoison = value; } }
        public bool EffectFear { get { return this.effectFear; } set { this.effectFear = value; } }
        public bool EffectBerserk { get { return this.effectBerserk; } set { this.effectBerserk = value; } }
        public bool EffectMushroom { get { return this.effectMushroom; } set { this.effectMushroom = value; } }
        public bool EffectScarecrow { get { return this.effectScarecrow; } set { this.effectScarecrow = value; } }
        public bool EffectInvincible { get { return this.effectInvincible; } set { this.effectInvincible = value; } }
        public bool ChangeAttack { get { return this.changeAttack; } set { this.changeAttack = value; } }
        public bool ChangeDefense { get { return this.changeDefense; } set { this.changeDefense = value; } }
        public bool ChangeMagicAttack { get { return this.changeMagicAttack; } set { this.changeMagicAttack = value; } }
        public bool ChangeMagicDefense { get { return this.changeMagicDefense; } set { this.changeMagicDefense = value; } }
        public bool TargetLiveAlly { get { return this.targetLiveAlly; } set { this.targetLiveAlly = value; } }
        public bool TargetEnemy { get { return this.targetEnemy; } set { this.targetEnemy = value; } }
        public bool TargetAll { get { return this.targetAll; } set { this.targetAll = value; } }
        public bool TargetWoundedOnly { get { return this.targetWoundedOnly; } set { this.targetWoundedOnly = value; } }
        public bool TargetOnePartyOnly { get { return this.targetOnePartyOnly; } set { this.targetOnePartyOnly = value; } }
        public bool TargetNotSelf { get { return this.targetNotSelf; } set { this.targetNotSelf = value; } }
        // timing
        public ushort TimingPointer { get { return this.timingPointer; } set { this.timingPointer = value; } }
        public ushort DamagePointer { get { return this.damagePointer; } set { this.damagePointer = value; } }
        #endregion
        // constructor
        public Spell(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            byte temp = 0;
            name = new char[15];
            for (int i = 0; i < name.Length; i++)
                name[i] = (char)rom[(index * 15) + 0x3A137F + i];
            if (index <= 0x1A)
                description = ParseDescription();
            else
                description = null;
            //
            int offset = (index * 12) + 0x3A20F1;
            //
            temp = rom[offset++];
            checkStats = (temp & 0x01) == 0x01;
            ignoreDefense = (temp & 0x02) == 0x02;
            checkMortality = (temp & 0x20) == 0x20;
            usableOverworld = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            attackType = (byte)(temp & 0x01);
            switch (temp & 0x06)
            {
                case 0x02: effectType = 0; break;
                case 0x04: effectType = 1; break;
                default: effectType = 2; break;
            }
            maxAttack = (temp & 0x08) == 0x08;
            //
            fpCost = rom[offset++];
            //
            Targetting target = (Targetting)rom[offset++];
            targetLiveAlly = (target & Targetting.LiveAlly) == Targetting.LiveAlly;
            targetEnemy = (target & Targetting.Enemy) == Targetting.Enemy;
            targetAll = (target & Targetting.All) == Targetting.All;
            targetWoundedOnly = (target & Targetting.WoundedOnly) == Targetting.WoundedOnly;
            targetOnePartyOnly = (target & Targetting.OnePartyOnly) == Targetting.OnePartyOnly;
            targetNotSelf = (target & Targetting.NotSelf) == Targetting.NotSelf;
            //
            temp = rom[offset++];
            switch (temp & 0xF0)
            {
                case 0x10: inflictElement = 0; break;			// Ice
                case 0x20: inflictElement = 1; break;		// Thunder
                case 0x40: inflictElement = 2; break;		// Fire
                case 0x80: inflictElement = 3; break;		// Earth
                default: inflictElement = 4; break;
            }
            magicPower = rom[offset++];
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
            temp = rom[offset]; offset += 2;
            changeMagicAttack = (temp & 0x08) == 0x08;		// Magic Attack
            changeAttack = (temp & 0x10) == 0x10;			// Attack
            changeMagicDefense = (temp & 0x20) == 0x20;		// Magic Defense
            changeDefense = (temp & 0x40) == 0x40;			// Defense
            //
            temp = rom[offset++];
            switch (temp)
            {
                case 0x00: inflictFunction = 0; break;			// Ice
                case 0x01: inflictFunction = 1; break;			// Ice
                case 0x02: inflictFunction = 2; break;		// Thunder
                case 0x03: inflictFunction = 3; break;		// Fire
                case 0x04: inflictFunction = 4; break;		// Earth
                default: inflictFunction = 5; break;
            }
            hideDigits = rom[offset] == 4;
            // timing
            if (index < 32)
            {
                // timing + damage pointers
                offset = index * 2 + 0x02CACE;
                timingPointer = Bits.GetShort(rom, offset);
                switch (temp * 2)
                {
                    case 0xCB0E: timingPointer = 0; break;     //
                    case 0xCBD8: timingPointer = 1; break;     //
                    case 0xCC44: timingPointer = 2; break;     //
                    case 0xCD1E: timingPointer = 3; break;     //
                    case 0xCD3F: timingPointer = 4; break;     //
                    case 0xCDA2: timingPointer = 5; break;     //
                    case 0xCDE1: timingPointer = 6; break;     //
                    case 0xCE75: timingPointer = 7; break;     //
                    case 0xCE85: timingPointer = 8; break;     //
                    case 0xCF22: timingPointer = 9; break;     //
                    case 0xCF63: timingPointer = 10; break;     //
                    case 0xCFC2: timingPointer = 11; break;     //
                    case 0xCFDF: timingPointer = 12; break;     //
                    default: break;       //no check
                }
                offset = index * 2 + 0x02D05B;
                damagePointer = Bits.GetShort(rom, offset);
            }
        }
        public void Assemble(ref int descriptionOffset)
        {
            Bits.SetChars(rom, 0x3A137F + (index * 15), name);
            // description
            int length = 0;
            if (index <= 0x1A)
            {
                Bits.SetShort(rom, 0x3A2B80 + index * 2, descriptionOffset);
                if (this.descriptionError)
                    MessageBox.Show("Unable to save spell #" + this.index + "'s description.");
                else
                {
                    length = description.Length;
                    Bits.SetChars(rom, 0x3A0000 + descriptionOffset, description); // Write the actual description
                }
            }
            descriptionOffset += length;
            // stats
            int offset = (index * 12) + 0x3A20F1;
            Bits.SetBit(rom, offset, 0, checkStats);
            Bits.SetBit(rom, offset, 1, ignoreDefense);
            Bits.SetBit(rom, offset, 5, checkMortality);
            Bits.SetBit(rom, offset++, 7, usableOverworld);
            //
            rom[offset] = attackType;
            if (effectType == 0) // Inflict
            {
                Bits.SetBit(rom, offset, 1, true);
                Bits.SetBit(rom, offset, 2, false);
            }
            else if (effectType == 1) // Nullify
            {
                Bits.SetBit(rom, offset, 1, false);
                Bits.SetBit(rom, offset, 2, true);
            }
            else if (effectType == 2) // {NONE}
            {
                Bits.SetBit(rom, offset, 1, false);
                Bits.SetBit(rom, offset, 2, false);
            }
            Bits.SetBit(rom, offset++, 3, maxAttack);
            //
            rom[offset++] = fpCost;
            Bits.SetBit(rom, offset, 1, targetLiveAlly);
            Bits.SetBit(rom, offset, 2, targetEnemy);
            Bits.SetBit(rom, offset, 4, targetAll);
            Bits.SetBit(rom, offset, 5, targetWoundedOnly);
            Bits.SetBit(rom, offset, 6, targetOnePartyOnly);
            Bits.SetBit(rom, offset++, 7, targetNotSelf);
            //
            switch (inflictElement)
            {
                case 0: rom[offset] = 0x10; break;
                case 1: rom[offset] = 0x20; break;
                case 2: rom[offset] = 0x40; break;
                case 3: rom[offset] = 0x80; break;
                case 4: rom[offset] = 0x00; break;
            }
            offset++;
            //
            rom[offset++] = magicPower;
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
            Bits.SetBit(rom, offset, 3, changeMagicAttack);
            Bits.SetBit(rom, offset, 4, changeAttack);
            Bits.SetBit(rom, offset, 5, changeMagicDefense);
            Bits.SetBit(rom, offset, 6, changeDefense);
            offset += 2;
            //
            switch (inflictFunction)
            {
                case 0: rom[offset] = 0x00; break;
                case 1: rom[offset] = 0x01; break;
                case 2: rom[offset] = 0x02; break;
                case 3: rom[offset] = 0x03; break;
                case 4: rom[offset] = 0x04; break;
                default: rom[offset] = 0xFF; break;
            }
            offset++;
            if (hideDigits == true)
                rom[offset] = 0x04;
            else
                rom[offset] = 0x00;
            // timing
            if (index < 32)
            {
                // timing + damage pointers
                offset = index * 2 + 0x02CACE;
                Bits.SetShort(rom, offset, timingPointer);
                offset = index * 2 + 0x02D05B;
                Bits.SetShort(rom, offset, damagePointer);
            }
        }
        // universal functions
        public override string ToString()
        {
            return new string(name);
        }
        public override void Clear()
        {
            Bits.Fill(name, '\x20');
            description = new char[1];
            fpCost = 0;
            magicPower = 0;
            hitRate = 0;
            attackType = 0;
            effectType = 2;
            inflictFunction = 5;
            inflictElement = 4;
            checkStats = false;
            ignoreDefense = false;
            checkMortality = false;
            usableOverworld = false;
            maxAttack = false;
            hideDigits = false;
            effectMute = false;
            effectSleep = false;
            effectPoison = false;
            effectFear = false;
            effectBerserk = false;
            effectMushroom = false;
            effectScarecrow = false;
            effectInvincible = false;
            changeAttack = false;
            changeDefense = false;
            changeMagicAttack = false;
            changeMagicDefense = false;
            targetLiveAlly = false;
            targetEnemy = false;
            targetAll = false;
            targetWoundedOnly = false;
            targetOnePartyOnly = false;
            targetNotSelf = false;
            // timing
            //    timingPointer = 0;
            //    damagePointer = 0;
        }
        // class functions
        private char[] ParseDescription()
        {
            int pointer = 0x3A0000 + Bits.GetShort(rom, 0x3A2B80 + index * 2);
            int counter = pointer;
            int length = 0;
            int letter = -1;
            while (letter != 0 && letter != 6)
            {
                letter = rom[counter++];
                length++;
            }
            char[] description = new char[length];
            for (int i = 0; i < length; i++)
                description[i] = (char)rom[pointer + i];
            return description;
        }
    }
}
