using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class Item : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        #region variables
        private int baseOffsetName;
        private int baseOffsetStats;
        private int baseOffsetPrice;
        private char[] name;
        private char[] description;
        private ushort price;
        private sbyte speed;
        private sbyte attack;
        private sbyte defense;
        private sbyte magicAttack;
        private sbyte magicDefense;
        private byte attackRange;
        private byte attackType;
        private byte elemAttack;
        private bool hideDigits;
        private byte inflictionAmount;
        private bool effectMute;
        private bool effectSleep;
        private bool effectPoison;
        private bool effectFear;
        private bool effectMushroom;
        private bool effectBerserk;
        private bool effectScarecrow;
        private bool effectInvincible;
        private bool changeAttack;
        private bool changeDefense;
        private bool changeMagicAttack;
        private bool changeMagicDefense;
        private bool elemNullIce;
        private bool elemNullFire;
        private bool elemNullThunder;
        private bool elemNullJump;
        private bool elemWeakIce;
        private bool elemWeakFire;
        private bool elemWeakThunder;
        private bool elemWeakJump;
        private bool equipMario;
        private bool equipToadstool;
        private bool equipBowser;
        private bool equipGeno;
        private bool equipMallow;
        private bool usageBattleMenu;
        private bool usageOverworldMenu;
        private bool usageReusable;
        private bool usageInstantDeath;
        private bool restoreFP;
        private bool restoreHP;
        private bool targetLiveAlly;
        private bool targetEnemy;
        private bool targetAll;
        private bool targetWoundedOnly;
        private bool targetOnePartyOnly;
        private bool targetNotSelf;
        private byte itemType = 0;
        private byte cursorBehavior;
        private byte inflictFunction;
        private byte weaponStartLevel1;
        private byte weaponStartLevel2;
        private byte weaponEndLevel2;
        private byte weaponEndLevel1;
        #endregion
        // hexitem
        public int BaseOffsetName { get { return this.baseOffsetName; } set { this.baseOffsetName = value; } }
        public int BaseOffsetStats { get { return this.baseOffsetStats; } set { this.baseOffsetStats = value; } }
        public int BaseOffsetPrice { get { return this.baseOffsetPrice; } set { this.baseOffsetPrice = value; } }
        // description
        public char[] RawDescription { get { return this.description; } set { this.description = value; } }
        private bool descriptionError = false; public bool DescriptionError { get { return this.descriptionError; } set { this.descriptionError = value; } }
        public TextHelperReduced textHelperReduced { get { return TextHelperReduced.Instance; } }
        private int caretPosByteView = 0; public int CaretPositionByteView { get { return this.caretPosByteView; } set { this.caretPosByteView = value; } }
        private int caretPosTextView = 0; public int CaretPositionTextView { get { return this.caretPosTextView; } set { this.caretPosTextView = value; } }
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
        #region accessors
        public char[] Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public ushort Price { get { return this.price; } set { this.price = value; } }
        public sbyte Speed { get { return this.speed; } set { this.speed = value; } }
        public sbyte Attack { get { return this.attack; } set { this.attack = value; } }
        public sbyte Defense { get { return this.defense; } set { this.defense = value; } }
        public sbyte MagicAttack { get { return this.magicAttack; } set { this.magicAttack = value; } }
        public sbyte MagicDefense { get { return this.magicDefense; } set { this.magicDefense = value; } }
        public byte AttackRange { get { return this.attackRange; } set { this.attackRange = value; } }
        public byte AttackType { get { return this.attackType; } set { this.attackType = value; } }
        public byte ElemAttack { get { return this.elemAttack; } set { this.elemAttack = value; } }
        public bool HideDigits { get { return this.hideDigits; } set { this.hideDigits = value; } }
        public byte InflictionAmount { get { return this.inflictionAmount; } set { this.inflictionAmount = value; } }
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
        public bool ElemNullIce { get { return this.elemNullIce; } set { this.elemNullIce = value; } }
        public bool ElemNullFire { get { return this.elemNullFire; } set { this.elemNullFire = value; } }
        public bool ElemNullThunder { get { return this.elemNullThunder; } set { this.elemNullThunder = value; } }
        public bool ElemNullJump { get { return this.elemNullJump; } set { this.elemNullJump = value; } }
        public bool ElemWeakIce { get { return this.elemWeakIce; } set { this.elemWeakIce = value; } }
        public bool ElemWeakFire { get { return this.elemWeakFire; } set { this.elemWeakFire = value; } }
        public bool ElemWeakThunder { get { return this.elemWeakThunder; } set { this.elemWeakThunder = value; } }
        public bool ElemWeakJump { get { return this.elemWeakJump; } set { this.elemWeakJump = value; } }
        public bool EquipMario { get { return this.equipMario; } set { this.equipMario = value; } }
        public bool EquipToadstool { get { return this.equipToadstool; } set { this.equipToadstool = value; } }
        public bool EquipBowser { get { return this.equipBowser; } set { this.equipBowser = value; } }
        public bool EquipGeno { get { return this.equipGeno; } set { this.equipGeno = value; } }
        public bool EquipMallow { get { return this.equipMallow; } set { this.equipMallow = value; } }
        public bool UsageBattleMenu { get { return this.usageBattleMenu; } set { this.usageBattleMenu = value; } }
        public bool UsageOverworldMenu { get { return this.usageOverworldMenu; } set { this.usageOverworldMenu = value; } }
        public bool UsageReusable { get { return this.usageReusable; } set { this.usageReusable = value; } }
        public bool UsageInstantDeath { get { return this.usageInstantDeath; } set { this.usageInstantDeath = value; } }
        public bool RestoreFP { get { return this.restoreFP; } set { this.restoreFP = value; } }
        public bool RestoreHP { get { return this.restoreHP; } set { this.restoreHP = value; } }
        public bool TargetLiveAlly { get { return this.targetLiveAlly; } set { this.targetLiveAlly = value; } }
        public bool TargetEnemy { get { return this.targetEnemy; } set { this.targetEnemy = value; } }
        public bool TargetAll { get { return this.targetAll; } set { this.targetAll = value; } }
        public bool TargetWoundedOnly { get { return this.targetWoundedOnly; } set { this.targetWoundedOnly = value; } }
        public bool TargetOnePartyOnly { get { return this.targetOnePartyOnly; } set { this.targetOnePartyOnly = value; } }
        public bool TargetNotSelf { get { return this.targetNotSelf; } set { this.targetNotSelf = value; } }
        public byte ItemType { get { return this.itemType; } set { this.itemType = value; } }
        public byte CursorBehavior { get { return this.cursorBehavior; } set { this.cursorBehavior = value; } }
        public byte InflictFunction { get { return this.inflictFunction; } set { this.inflictFunction = value; } }
        public byte WeaponStartLevel1 { get { return this.weaponStartLevel1; } set { this.weaponStartLevel1 = value; } }
        public byte WeaponStartLevel2 { get { return this.weaponStartLevel2; } set { this.weaponStartLevel2 = value; } }
        public byte WeaponEndLevel2 { get { return this.weaponEndLevel2; } set { this.weaponEndLevel2 = value; } }
        public byte WeaponEndLevel1 { get { return this.weaponEndLevel1; } set { this.weaponEndLevel1 = value; } }
        #endregion
        // constructor
        public Item(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            // name
            name = new char[15];
            for (int i = 0; i < name.Length; i++)
                name[i] = (char)rom[(index * 15) + 0x3A46EF + i];
            int offset = 0x3A46EF + (index * 15);
            this.baseOffsetName = offset;
            // description
            if (index <= 0xB0)
                description = ParseDescription(rom);
            else
                description = null;
            // price
            offset = (index * 2) + 0x3A40F2;
            this.baseOffsetPrice = offset;
            price = Bits.GetShort(rom, offset);
            // stats
            offset = (index * 18) + 0x3A014D;
            this.baseOffsetStats = offset;
            byte temp = rom[offset++];
            itemType = (byte)(temp & 3);
            // item usage
            usageBattleMenu = (temp & 0x08) == 0x08;		// Usable in battle
            usageOverworldMenu = (temp & 0x10) == 0x10;	// Usable in overworld menu
            usageReusable = (temp & 0x20) == 0x20;	// Reusable
            usageInstantDeath = (temp & 0x80) == 0x80;		// Death protection
            // attack type
            temp = rom[offset++];
            if ((temp & 0x02) == 0x02)
                attackType = 0;		// Inflict
            else if ((temp & 0x01) == 0x01)
                attackType = 1;		// Protect
            else if ((temp & 0x04) == 0x04)
                attackType = 2;		// Nullify
            else
                attackType = 3;     // None
            // cursor behavior
            cursorBehavior = (temp & 0x20) == 0x20 ? (byte)1 : (byte)0;
            //
            restoreFP = (temp & 0x40) == 0x40;		// Restore only if FP not maxed out
            restoreHP = (temp & 0x80) == 0x80;		// Restore only if HP not maxed out
            // equip
            temp = rom[offset++];
            equipMario = (temp & 0x01) == 0x01;		// Mario
            equipToadstool = (temp & 0x02) == 0x02;	// Toadstool
            equipBowser = (temp & 0x04) == 0x04;		// Bowser
            equipGeno = (temp & 0x08) == 0x08;			// Geno
            equipMallow = (temp & 0x10) == 0x10;		// Mallow
            // targetting
            temp = rom[offset++];
            targetLiveAlly = (temp & 0x02) == 0x02;			// Usable on any ally
            targetEnemy = (temp & 0x04) == 0x04;		// Usable on any enemy
            targetAll = (temp & 0x10) == 0x10;			// Usable on on all
            targetWoundedOnly = (temp & 0x20) == 0x20;			// Usable on wounded
            targetOnePartyOnly = (temp & 0x40) == 0x40;		// Usable in one party only
            targetNotSelf = (temp & 0x80) == 0x80;		// Cannot use on self
            //
            temp = rom[offset++];
            switch (temp & 0xF0)
            {
                case 0x10: elemAttack = 0; break;			// Ice
                case 0x20: elemAttack = 1; break;		// Thunder
                case 0x40: elemAttack = 2; break;		// Fire
                case 0x80: elemAttack = 3; break;		// Earth
                default: elemAttack = 4; break;
            }
            // elemental attributes: nullify
            temp = rom[offset++];
            elemNullIce = (temp & 0x10) == 0x10;		// Ice
            elemNullThunder = (temp & 0x20) == 0x20;	// Thunder
            elemNullFire = (temp & 0x40) == 0x40;	// Fire
            elemNullJump = (temp & 0x80) == 0x80;	// Earth
            // elemental attributes: weakness
            temp = rom[offset++];
            elemWeakIce = (temp & 0x10) == 0x10;		// Ice
            elemWeakThunder = (temp & 0x20) == 0x20;	// Thunder
            elemWeakFire = (temp & 0x40) == 0x40;	// Fire
            elemWeakJump = (temp & 0x80) == 0x80;	// Earth
            // status effect
            temp = rom[offset++];
            effectMute = (temp & 0x01) == 0x01;		// Mute
            effectSleep = (temp & 0x02) == 0x02;		// Sleep
            effectPoison = (temp & 0x04) == 0x04;		// Poison
            effectFear = (temp & 0x08) == 0x08;		// Fear
            effectBerserk = (temp & 0x10) == 0x10;	// Unused Berserk status
            effectMushroom = (temp & 0x20) == 0x20;	// Mushroom
            effectScarecrow = (temp & 0x40) == 0x40;	// Scarecrow
            effectInvincible = (temp & 0x80) == 0x80;	// Invincible
            // status change
            temp = rom[offset++];
            changeMagicAttack = (temp & 0x08) == 0x08;		// Magic Attack
            changeAttack = (temp & 0x10) == 0x10;			// Attack
            changeMagicDefense = (temp & 0x20) == 0x20;		// Magic Defense
            changeDefense = (temp & 0x40) == 0x40;			// Defense
            //
            speed = (sbyte)rom[offset++];
            attack = (sbyte)rom[offset++];
            defense = (sbyte)rom[offset++];
            magicAttack = (sbyte)rom[offset++];
            magicDefense = (sbyte)rom[offset++];
            attackRange = rom[offset++];
            inflictionAmount = rom[offset++];
            // inflict function
            temp = rom[offset++];
            switch (temp)
            {
                case 0x00: inflictFunction = 0; break;
                case 0x01: inflictFunction = 1; break;			// Revive
                case 0x02: inflictFunction = 2; break;			// Recover FP
                case 0x03: inflictFunction = 3; break;			// etc...
                case 0x04: inflictFunction = 4; break;
                case 0x05: inflictFunction = 5; break;
                case 0x06: inflictFunction = 6; break;
                case 0x07: inflictFunction = 7; break;
                case 0xFF: inflictFunction = 8; break;
                default: inflictFunction = 0; break;
            }
            hideDigits = (rom[offset] & 0x04) == 0x04;
            // timing
            if (index < 37)
            {
                offset = (index * 4) + 0x3A438A;
                weaponStartLevel1 = rom[offset++];
                weaponStartLevel2 = rom[offset++];
                weaponEndLevel2 = rom[offset++];
                weaponEndLevel1 = rom[offset++];
            }
        }
        public void Assemble(ref int descriptionOffset)
        {
            int offset = 0x3A46EF + (index * 15);
            Bits.SetChars(rom, offset, name);
            this.baseOffsetName = offset;
            // description
            int length = 0;
            if (index <= 0xB0)
            {
                Bits.SetShort(rom, 0x3A2F20 + index * 2, descriptionOffset);
                if (this.descriptionError)
                    MessageBox.Show("Unable to save item #" + this.index + "'s description.");
                else
                {
                    length = (ushort)description.Length;
                    Bits.SetChars(rom, 0x3A0000 + descriptionOffset, description); // Write the actual description
                }
            }
            descriptionOffset += length;
            // price
            Bits.SetShort(rom, (index * 2) + 0x3A40F2, price);
            this.baseOffsetPrice = offset;
            // stats
            offset = (index * 18) + 0x3A014D;
            this.baseOffsetStats = offset;
            rom[offset] = itemType;
            Bits.SetBit(rom, offset, 3, usageBattleMenu);
            Bits.SetBit(rom, offset, 4, usageOverworldMenu);
            Bits.SetBit(rom, offset, 5, usageReusable);
            Bits.SetBit(rom, offset++, 7, usageInstantDeath);
            //
            switch (attackType)
            {
                case 0: rom[offset] = 0x02; break;
                case 1: rom[offset] = 0x01; break;
                case 2: rom[offset] = 0x04; break;
                case 3: rom[offset] = 0x00; break;
            }
            // cursor
            if (cursorBehavior == 1)
                Bits.SetBit(rom, offset, 5, true);
            Bits.SetBit(rom, offset, 6, restoreFP);
            Bits.SetBit(rom, offset++, 7, restoreHP);
            //
            Bits.SetBit(rom, offset, 0, equipMario);
            Bits.SetBit(rom, offset, 1, equipToadstool);
            Bits.SetBit(rom, offset, 2, equipBowser);
            Bits.SetBit(rom, offset, 3, equipGeno);
            Bits.SetBit(rom, offset++, 4, equipMallow);
            //
            Bits.SetBit(rom, offset, 1, targetLiveAlly);
            Bits.SetBit(rom, offset, 2, targetEnemy);
            Bits.SetBit(rom, offset, 4, targetAll);
            Bits.SetBit(rom, offset, 5, targetWoundedOnly);
            Bits.SetBit(rom, offset, 6, targetOnePartyOnly);
            Bits.SetBit(rom, offset++, 7, targetNotSelf);
            //
            switch (elemAttack)
            {
                case 0: rom[offset++] = 0x10; break;			// Ice
                case 1: rom[offset++] = 0x20; break;		// Thunder
                case 2: rom[offset++] = 0x40; break;		// Fire
                case 3: rom[offset++] = 0x80; break;		// Earth
                case 4: rom[offset++] = 0x00; break;
            }
            // elemental attributes: nullify
            Bits.SetBit(rom, offset, 4, elemNullIce);
            Bits.SetBit(rom, offset, 5, elemNullThunder);
            Bits.SetBit(rom, offset, 6, elemNullFire);
            Bits.SetBit(rom, offset++, 7, elemNullJump);
            // elemental attributes: weakness
            Bits.SetBit(rom, offset, 4, elemWeakIce);
            Bits.SetBit(rom, offset, 5, elemWeakThunder);
            Bits.SetBit(rom, offset, 6, elemWeakFire);
            Bits.SetBit(rom, offset++, 7, elemWeakJump);
            // status effect
            Bits.SetBit(rom, offset, 0, effectMute);
            Bits.SetBit(rom, offset, 1, effectSleep);
            Bits.SetBit(rom, offset, 2, effectPoison);
            Bits.SetBit(rom, offset, 3, effectFear);
            Bits.SetBit(rom, offset, 4, effectBerserk);
            Bits.SetBit(rom, offset, 5, effectMushroom);
            Bits.SetBit(rom, offset, 6, effectScarecrow);
            Bits.SetBit(rom, offset++, 7, effectInvincible);
            // status change
            Bits.SetBit(rom, offset, 3, changeMagicAttack);
            Bits.SetBit(rom, offset, 4, changeAttack);
            Bits.SetBit(rom, offset, 5, changeMagicDefense);
            Bits.SetBit(rom, offset++, 6, changeDefense);
            //
            rom[offset++] = (byte)speed;
            rom[offset++] = (byte)attack;
            rom[offset++] = (byte)defense;
            rom[offset++] = (byte)magicAttack;
            rom[offset++] = (byte)magicDefense;
            rom[offset++] = attackRange;
            rom[offset++] = inflictionAmount;
            // inflict function
            switch (inflictFunction)
            {
                case 0: rom[offset++] = 0x00; break;
                case 1: rom[offset++] = 0x01; break;			// Revive
                case 2: rom[offset++] = 0x02; break;			// Recover FP
                case 3: rom[offset++] = 0x03; break;			// etc...
                case 4: rom[offset++] = 0x04; break;
                case 5: rom[offset++] = 0x05; break;
                case 6: rom[offset++] = 0x06; break;
                case 7: rom[offset++] = 0x07; break;
                case 8: rom[offset++] = 0xFF; break;
            }
            Bits.SetBit(rom, offset, 2, hideDigits);
            // timing
            if (index < 37)
            {
                offset = (index * 4) + 0x3A438A;
                rom[offset++] = weaponStartLevel1;
                rom[offset++] = weaponStartLevel2;
                rom[offset++] = weaponEndLevel2;
                rom[offset++] = weaponEndLevel1;
            }
        }
        // class functions
        private char[] ParseDescription(byte[] rom)
        {
            int offset = 0x3A0000 + Bits.GetShort(rom, 0x3A2F20 + index * 2);
            int counter = 0;
            int length = 0;
            int letter = -1;
            while (letter != 0 && letter != 6)
            {
                letter = rom[offset + counter++];
                length++;
            }
            char[] description = new char[length];
            for (int i = 0; i < length; i++)
                description[i] = (char)rom[offset + i];
            return description;
        }
        // universal functions
        public override string ToString()
        {
            return new string(name);
        }
        public override void Clear()
        {
            Bits.Fill(name, '\x20');
            description = new char[0];
            price = 0;
            speed = 0;
            attack = 0;
            defense = 0;
            magicAttack = 0;
            magicDefense = 0;
            attackRange = 0;
            attackType = 3;
            elemAttack = 4;
            hideDigits = false;
            inflictionAmount = 0;
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
            elemNullIce = false;
            elemNullFire = false;
            elemNullThunder = false;
            elemNullJump = false;
            elemWeakIce = false;
            elemWeakFire = false;
            elemWeakThunder = false;
            elemWeakJump = false;
            equipMario = false;
            equipToadstool = false;
            equipBowser = false;
            equipGeno = false;
            equipMallow = false;
            usageBattleMenu = false;
            usageOverworldMenu = false;
            usageReusable = false;
            usageInstantDeath = false;
            restoreFP = false;
            restoreHP = false;
            targetLiveAlly = false;
            targetEnemy = false;
            targetAll = false;
            targetWoundedOnly = false;
            targetOnePartyOnly = false;
            targetNotSelf = false;
            itemType = 0;
            cursorBehavior = 0;
            inflictFunction = 8;
        }
    }
}
