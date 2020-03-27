using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class Monster : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }        
        public override int Index { get { return index; } set { index = value; } }
        #region Monster Stats
        private int index;
        private char[] name;
        private char[] psychopath;
        private bool psychopathError = false;
        private ushort hp;
        private byte speed;
        private byte attack;
        private byte defense;
        private byte magicAttack;
        private byte magicDefense;
        private byte fp;
        private byte evade;
        private byte magicEvade;
        private ushort experience;
        private byte coins;
        private byte yoshiCookie;
        private byte itemWinA;
        private byte itemWinB;
        private bool elemNullIce;
        private bool elemNullThunder;
        private bool elemNullFire;
        private bool elemNullJump;
        private bool elemWeakIce;
        private bool elemWeakThunder;
        private bool elemWeakFire;
        private bool elemWeakJump;
        private bool effectNullMute;
        private bool effectNullSleep;
        private bool effectNullPoison;
        private bool effectNullFear;
        private bool effectNullMushroom;
        private bool effectNullScarecrow;
        private bool effectNullInvincible;
        private bool invincible;
        private bool mortalityProtection;
        private bool disableAutoDeath;
        private bool palette2bpp;
        private byte morphSuccess;
        private byte flowerBonus;
        private byte flowerOdds;
        private byte entranceStyle;
        private byte coinSize;
        private byte elevation;
        private byte spriteBehavior;
        private byte strikeSound;
        private byte otherSound;
        private byte cursorX;
        private byte cursorY;
        #endregion
        #region Accessors
        public char[] Name { get { return this.name; } set { this.name = value; } }
        // text
        public bool SetPsychopath(string value, bool byteView)
        {
            this.psychopath = textHelperReduced.Encode(value.ToCharArray(), byteView, 0, Lists.Keystrokes);
            this.psychopathError = textHelperReduced.Error;
            return !psychopathError;
        }
        public string GetPsychopath(bool byteView)
        {
            if (!psychopathError)
                return new string(textHelperReduced.Decode(psychopath, byteView, 0, Lists.Keystrokes));
            else
                return new string(psychopath);
        }
        public char[] RawPsychopath { get { return this.psychopath; } }
        public bool PsychopathError { get { return this.psychopathError; } set { this.psychopathError = value; } }
        private TextHelperReduced textHelperReduced { get { return TextHelperReduced.Instance; } }
        private int caretPosByteView = 0;
        private int caretPosTextView = 0;
        public int CaretPosByteView
        {
            get
            {
                return this.caretPosByteView;
            }
            set
            {
                this.caretPosByteView = value;
            }
        }
        public int CaretPosTextView
        {
            get
            {
                return this.caretPosTextView;
            }
            set
            {
                this.caretPosTextView = value;
            }
        }
        // stats
        public ushort HP { get { return this.hp; } set { this.hp = value; } }
        public byte Speed { get { return this.speed; } set { this.speed = value; } }
        public byte Attack { get { return this.attack; } set { this.attack = value; } }
        public byte Defense { get { return this.defense; } set { this.defense = value; } }
        public byte MagicAttack { get { return this.magicAttack; } set { this.magicAttack = value; } }
        public byte MagicDefense { get { return this.magicDefense; } set { this.magicDefense = value; } }
        public byte FP { get { return this.fp; } set { this.fp = value; } }
        public byte Evade { get { return this.evade; } set { this.evade = value; } }
        public byte MagicEvade { get { return this.magicEvade; } set { this.magicEvade = value; } }
        public bool ElemNullIce { get { return this.elemNullIce; } set { this.elemNullIce = value; } }
        public bool ElemNullThunder { get { return this.elemNullThunder; } set { this.elemNullThunder = value; } }
        public bool ElemNullFire { get { return this.elemNullFire; } set { this.elemNullFire = value; } }
        public bool ElemNullJump { get { return this.elemNullJump; } set { this.elemNullJump = value; } }
        public bool ElemWeakIce { get { return this.elemWeakIce; } set { this.elemWeakIce = value; } }
        public bool ElemWeakThunder { get { return this.elemWeakThunder; } set { this.elemWeakThunder = value; } }
        public bool ElemWeakFire { get { return this.elemWeakFire; } set { this.elemWeakFire = value; } }
        public bool ElemWeakJump { get { return this.elemWeakJump; } set { this.elemWeakJump = value; } }
        public bool EffectNullMute { get { return this.effectNullMute; } set { this.effectNullMute = value; } }
        public bool EffectNullSleep { get { return this.effectNullSleep; } set { this.effectNullSleep = value; } }
        public bool EffectNullPoison { get { return this.effectNullPoison; } set { this.effectNullPoison = value; } }
        public bool EffectNullFear { get { return this.effectNullFear; } set { this.effectNullFear = value; } }
        public bool EffectNullMushroom { get { return this.effectNullMushroom; } set { this.effectNullMushroom = value; } }
        public bool EffectNullScarecrow { get { return this.effectNullScarecrow; } set { this.effectNullScarecrow = value; } }
        public bool EffectNullInvincible { get { return this.effectNullInvincible; } set { this.effectNullInvincible = value; } }
        public bool Invincible { get { return this.invincible; } set { this.invincible = value; } }
        public bool MortalityProtection { get { return this.mortalityProtection; } set { this.mortalityProtection = value; } }
        public bool DisableAutoDeath { get { return this.disableAutoDeath; } set { this.disableAutoDeath = value; } }
        public bool Palette2bpp { get { return this.palette2bpp; } set { this.palette2bpp = value; } }
        public byte MorphSuccess { get { return this.morphSuccess; } set { this.morphSuccess = value; } }
        public byte FlowerBonus { get { return this.flowerBonus; } set { this.flowerBonus = value; } }
        public byte FlowerOdds { get { return this.flowerOdds; } set { this.flowerOdds = value; } }
        public byte EntranceStyle { get { return this.entranceStyle; } set { this.entranceStyle = value; } }
        public byte CoinSize { get { return this.coinSize; } set { this.coinSize = value; } }
        public byte Elevation { get { return this.elevation; } set { this.elevation = value; } }
        public byte SpriteBehavior { get { return this.spriteBehavior; } set { this.spriteBehavior = value; } }
        public byte StrikeSound { get { return this.strikeSound; } set { this.strikeSound = value; } }
        public byte OtherSound { get { return this.otherSound; } set { this.otherSound = value; } }
        // rewards
        public ushort Experience { get { return this.experience; } set { this.experience = value; } }
        public byte Coins { get { return this.coins; } set { this.coins = value; } }
        public byte YoshiCookie { get { return this.yoshiCookie; } set { this.yoshiCookie = value; } }
        public byte ItemWinA { get { return this.itemWinA; } set { this.itemWinA = value; } }
        public byte ItemWinB { get { return this.itemWinB; } set { this.itemWinB = value; } }
        // cursor
        public byte CursorX { get { return this.cursorX; } set { this.cursorX = value; } }
        public byte CursorY { get { return this.cursorY; } set { this.cursorY = value; } }
        // image accessors
        public Image Image
        {
            get
            {
                int[] pixels = Pixels;
                int[] palette = Model.NumeralPaletteSet.Palette;
                int[] cursor = Do.GetPixelRegion(Model.NumeralGraphics, 0x20, palette, 16, 12, 0, 2, 2, 0);
                for (int y = 112 - (cursorY * 8), n = 0; n < 16; y++, n++)
                {
                    for (int x = 112 - (cursorX * 8), m = 0; m < 16; x++, m++)
                    {
                        if (cursor[n * 16 + m] != 0 &&
                            y >= 0 && y < 256 && x >= 0 && x < 256)
                            pixels[y * 256 + x] = cursor[n * 16 + m];
                    }
                }
                return Do.PixelsToImage(pixels, 256, 256);
            }
        }
        public int[] Pixels
        {
            get
            {
                return Model.Sprites[this.index + 256].GetPixels();
            }
        }
        public int[] Shadow
        {
            get
            {
                int[] palette = Model.Sprites[this.index + 256].Palette;
                return Do.GetPixelRegion(Model.NumeralGraphics, 0x20, palette, 16, 14, 0, 2, 2, 0);
            }
        }
        #endregion
        // constructor
        public Monster(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            name = new char[13];
            for (int i = 0; i < name.Length; i++)
                name[i] = (char)rom[(index * 13) + 0x3992d1 + i];
            psychopath = ParsePsychopath(rom);
            // stats
            int offset = Bits.GetShort(rom, 0x390026 + index * 2) + 0x390000;
            // B0,1
            hp = Bits.GetShort(rom, offset); offset += 2;
            // B2-9
            speed = rom[offset++];
            attack = rom[offset++];
            defense = rom[offset++];
            magicAttack = rom[offset++];
            magicDefense = rom[offset++];
            fp = rom[offset++];
            evade = rom[offset++];
            magicEvade = rom[offset++];
            // B10
            byte temp = rom[offset++];
            disableAutoDeath = (temp & 0x01) == 1;
            palette2bpp = (temp & 0x02) == 2;
            // B11
            temp = rom[offset++];
            invincible = (temp & 0x01) == 1;
            mortalityProtection = (temp & 0x02) == 2;
            morphSuccess = (byte)((temp & 0x0C) >> 2);
            strikeSound = (byte)(temp >> 4);
            // B12
            temp = rom[offset++];
            elemNullIce = (temp & 0x10) == 0x10;
            elemNullThunder = (temp & 0x20) == 0x20;
            elemNullFire = (temp & 0x40) == 0x40;
            elemNullJump = (temp & 0x80) == 0x80;
            // B13
            temp = rom[offset];
            otherSound = (byte)(rom[offset++] & 7);
            elemWeakIce = (temp & 0x10) == 0x10;
            elemWeakThunder = (temp & 0x20) == 0x20;
            elemWeakFire = (temp & 0x40) == 0x40;
            elemWeakJump = (temp & 0x80) == 0x80;
            // B14
            Status status = (Status)rom[offset++];
            effectNullMute = (status & Status.Mute) == Status.Mute;
            effectNullSleep = (status & Status.Sleep) == Status.Sleep;
            effectNullPoison = (status & Status.Poison) == Status.Poison;
            effectNullFear = (status & Status.Fear) == Status.Fear;
            effectNullMushroom = (status & Status.Mushroom) == Status.Mushroom;
            effectNullScarecrow = (status & Status.Scarecrow) == Status.Scarecrow;
            effectNullInvincible = (status & Status.Invincible) == Status.Invincible;
            // B15
            temp = rom[offset++];
            entranceStyle = (byte)(temp & 0x0F);
            elevation = (byte)((temp & 0x30) >> 4);
            coinSize = (byte)(temp >> 6);
            // rewards
            offset = Bits.GetShort(rom, 0x39142a + index * 2) + 0x390000;
            experience = Bits.GetShort(rom, offset); offset += 2;
            coins = rom[offset++];
            yoshiCookie = rom[offset++];
            itemWinA = rom[offset++];
            itemWinB = rom[offset++];
            // flower bonus
            offset = index + 0x39BB44;
            flowerBonus = (byte)(rom[offset] & 0x0F);
            flowerOdds = (byte)Math.Min(10, (temp & 0xF0) >> 4);
            // death animation
            offset = index * 2 + 0x350202;
            switch (Bits.GetShort(rom, offset))
            {
                case 0x058A: spriteBehavior = 0; break;  // no movement for "Escape"
                case 0x0596: spriteBehavior = 1; break;  // slide backward when hit
                case 0x05A2: spriteBehavior = 2; break;  // Bowser Clone sprite
                case 0x05AE: spriteBehavior = 3; break;  // Mario Clone sprite
                case 0x05BA: spriteBehavior = 4; break;  // no reaction when hit
                case 0x0898: spriteBehavior = 5; break;  // sprite shadow
                case 0x0985: spriteBehavior = 6; break;  // floating, sprite shadow
                case 0x0991: spriteBehavior = 7; break;  // floating
                case 0x0AD3: spriteBehavior = 8; break;  // floating, slide backward when hit
                case 0x0ADF: spriteBehavior = 9; break;  // floating, slide backward when hit
                case 0x0AEB: spriteBehavior = 10; break;  // fade out death, floating
                case 0x0CF2: spriteBehavior = 11; break;  // fade out death
                case 0x0CFE: spriteBehavior = 12; break;  // fade out death
                case 0x0D0A: spriteBehavior = 13; break;  // fade out death, Smithy spell cast
                case 0x0D16: spriteBehavior = 14; break;  // fade out death, no "Escape" movement
                case 0x0E60: spriteBehavior = 15; break;  // fade out death, no "Escape" transition
                case 0x0E6C: spriteBehavior = 16; break;  // (normal)
                case 0x0E78: spriteBehavior = 17; break;  // no reaction when hit
                default: spriteBehavior = 0; break;
            }
            // cursor
            cursorX = (byte)((rom[0x39B944 + index] & 0xF0) >> 4);
            cursorY = (byte)(rom[0x39B944 + index] & 0x0F);
        }
        public void Assemble(ref int psychopathOffset)
        {
            // name
            Bits.SetChars(rom, 0x3992d1 + (index * 13), name);
            // psychopath
            int length = 0;
            Bits.SetShort(rom, 0x399FD1 + index * 2, psychopathOffset);
            if (this.psychopathError)
                MessageBox.Show("There was a problem saving monster #" + this.index + "'s psychopath message.");
            else
            {
                length = psychopath.Length;
                Bits.SetChars(rom, 0x390000 + psychopathOffset, psychopath);
            }
            psychopathOffset += length;
            // stats
            int offset = Bits.GetShort(rom, 0x390026 + index * 2) + 0x390000;
            // B0
            Bits.SetShort(rom, offset, hp); offset += 2;
            // B1-8
            rom[offset++] = speed;
            rom[offset++] = attack;
            rom[offset++] = defense;
            rom[offset++] = magicAttack;
            rom[offset++] = magicDefense;
            rom[offset++] = fp;
            rom[offset++] = evade;
            rom[offset++] = magicEvade;
            // B9
            Bits.SetBit(rom, offset, 0, disableAutoDeath);
            Bits.SetBit(rom, offset++, 1, palette2bpp);
            // B10
            rom[offset]= (byte)((strikeSound << 4) + (morphSuccess << 2));
            Bits.SetBit(rom, offset, 0, invincible);
            Bits.SetBit(rom, offset++, 1, mortalityProtection);
            // B11
            Bits.SetBit(rom, offset, 4, elemNullIce);
            Bits.SetBit(rom, offset, 5, elemNullThunder);
            Bits.SetBit(rom, offset, 6, elemNullFire);
            Bits.SetBit(rom, offset++, 7, elemNullJump);
            // B12
            rom[offset] = otherSound;
            Bits.SetBit(rom, offset, 4, elemWeakIce);
            Bits.SetBit(rom, offset, 5, elemWeakThunder);
            Bits.SetBit(rom, offset, 6, elemWeakFire);
            Bits.SetBit(rom, offset++, 7, elemWeakJump);
            // B13
            Bits.SetBit(rom, offset, 0, effectNullMute);
            Bits.SetBit(rom, offset, 1, effectNullSleep);
            Bits.SetBit(rom, offset, 2, effectNullPoison);
            Bits.SetBit(rom, offset, 3, effectNullFear);
            Bits.SetBit(rom, offset, 5, effectNullMushroom);
            Bits.SetBit(rom, offset, 6, effectNullScarecrow);
            Bits.SetBit(rom, offset++, 7, effectNullInvincible);
            // B14
            rom[offset]= (byte)(entranceStyle + (elevation << 4) + (coinSize << 6));
            // rewards
            offset = Bits.GetShort(rom, 0x39142a + index * 2) + 0x390000;
            Bits.SetShort(rom, offset, experience); offset += 2;
            rom[offset++] = coins;
            rom[offset++] = yoshiCookie;
            rom[offset++] = itemWinA;
            rom[offset++] = itemWinB;
            // flower bonus
            offset = index + 0x39BB44;
            rom[offset] =(byte)(flowerBonus + (flowerOdds << 4));
            // death animation
            offset = index * 2 + 0x350202;
            switch (spriteBehavior)								// DEATH ANIMATION
            {
                case 0: Bits.SetShort(rom, offset, 0x058A); break;	// no movement for "Escape"
                case 1: Bits.SetShort(rom, offset, 0x0596); break;	// slide backward when hit
                case 2: Bits.SetShort(rom, offset, 0x05A2); break;	// etc...
                case 3: Bits.SetShort(rom, offset, 0x05AE); break;
                case 4: Bits.SetShort(rom, offset, 0x05BA); break;
                case 5: Bits.SetShort(rom, offset, 0x0898); break;
                case 6: Bits.SetShort(rom, offset, 0x0985); break;
                case 7: Bits.SetShort(rom, offset, 0x0991); break;
                case 8: Bits.SetShort(rom, offset, 0x0AD3); break;
                case 9: Bits.SetShort(rom, offset, 0x0ADF); break;
                case 10: Bits.SetShort(rom, offset, 0x0AEB); break;
                case 11: Bits.SetShort(rom, offset, 0x0CF2); break;
                case 12: Bits.SetShort(rom, offset, 0x0CFE); break;
                case 13: Bits.SetShort(rom, offset, 0x0D0A); break;
                case 14: Bits.SetShort(rom, offset, 0x0D16); break;
                case 15: Bits.SetShort(rom, offset, 0x0E60); break;
                case 16: Bits.SetShort(rom, offset, 0x0E6C); break;
                case 17: Bits.SetShort(rom, offset, 0x0E78); break;
            }
            // cursor
            rom[0x39B944 + index] = (byte)(cursorX << 4);
            rom[0x39B944 + index] |= cursorY;
        }
        // universal functions
        public override void Clear()
        {
            Bits.Fill(name, '\x20');
            psychopath = new char[0];
            hp = 0;
            speed = 0;
            attack = 0;
            defense = 0;
            magicAttack = 0;
            magicDefense = 0;
            fp = 0;
            evade = 0;
            magicEvade = 0;
            experience = 0;
            coins = 0;
            yoshiCookie = 255;
            itemWinA = 255;
            itemWinB = 255;
            elemNullIce = false;
            elemNullThunder = false;
            elemNullFire = false;
            elemNullJump = false;
            elemWeakIce = false;
            elemWeakThunder = false;
            elemWeakFire = false;
            elemWeakJump = false;
            effectNullMute = false;
            effectNullSleep = false;
            effectNullPoison = false;
            effectNullFear = false;
            effectNullMushroom = false;
            effectNullScarecrow = false;
            effectNullInvincible = false;
            invincible = false;
            mortalityProtection = false;
            disableAutoDeath = false;
            palette2bpp = false;
            morphSuccess = 0;
            flowerBonus = 0;
            flowerOdds = 0;
            entranceStyle = 0;
            coinSize = 0;
            elevation = 0;
            spriteBehavior = 0;
            strikeSound = 0;
            otherSound = 0;
        }
        // text functions
        private char[] ParsePsychopath(byte[] data)
        {
            int offset = 0x390000 + Bits.GetShort(data, 0x399FD1 + index * 2);
            int counter = offset;
            int length = 0;
            int letter = -1;
            while (letter != 0)
            {
                letter = data[counter++];
                length++;
            }
            char[] psychopath = new char[length];
            for (int i = 0; i < length; i++)
                psychopath[i] = (char)data[offset + i];
            return psychopath;
        }
    }
}
