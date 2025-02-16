using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Spells : NewForm
    {
        #region Variables
        private Spell[] spells { get { return Model.Spells; } set { Model.Spells = value; } }
        private Spell spell { get { return spells[index]; } set { spells[index] = value; } }
        public Spell Spell { get { return spell; } set { spell = value; } }
        private int index { get { return (int)spellNum.Value; } set { spellNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private Settings settings = Settings.Default;
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        private bool byteView { get { return !textView.Checked; } set { textView.Checked = !value; } }
        private Bitmap descriptionFrame;
        private Bitmap descriptionText;
        private EditLabel labelWindow;
        #endregion
        // constructor
        public Spells()
        {
            InitializeComponent();
            InitializeStrings();
            RefreshSpells();
            labelWindow = new EditLabel(spellName, spellNum, "Spells", false);
            //
            this.History = new History(this, spellName, spellNum);
        }
        #region Functions
        public void RefreshSpells()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.Updating)
                return;
            this.Updating = true;
            this.spellName.SelectedIndex = Model.SpellNames.GetSortedIndex(index);
            this.spellFPCost.Value = spell.FPCost;
            this.spellMagPower.Value = spell.MagicPower;
            this.spellHitRate.Value = spell.HitRate;
            this.spellNameIcon.Visible = index < 64;
            this.spellNameIcon.SelectedIndex = (int)(spell.Name[0] - 0x20);
            this.spellNameIcon.Invalidate();
            if (index < 64)
                this.textBoxSpellName.Text = Do.RawToASCII(spell.Name, Lists.KeystrokesMenu).Substring(1);
            else
                this.textBoxSpellName.Text = Do.RawToASCII(spell.Name, Lists.Keystrokes).Substring(1);
            if (index > 26)
            {
                this.textBoxSpellDescription.Text = " This spell[1] cannot have a[1] description";
                if (spell.RawDescription == null)
                    this.textBoxSpellDescription_TextChanged(null, null);
                this.textBoxSpellDescription.Enabled = false;
                this.toolStrip2.Enabled = false;
            }
            else
            {
                this.textBoxSpellDescription.Enabled = true;
                this.toolStrip2.Enabled = true;
                this.textBoxSpellDescription.Text = spell.GetDescription(byteView);
            }
            this.spellAttackProp.SetItemChecked(0, spell.CheckStats);
            this.spellAttackProp.SetItemChecked(1, spell.IgnoreDefense);
            this.spellAttackProp.SetItemChecked(2, spell.CheckMortality);
            this.spellAttackProp.SetItemChecked(3, spell.UsableOverworld);
            this.spellAttackProp.SetItemChecked(4, spell.MaxAttack);
            this.spellAttackProp.SetItemChecked(5, spell.HideDigits);
            this.spellStatusEffect.SetItemChecked(0, spell.EffectMute);
            this.spellStatusEffect.SetItemChecked(1, spell.EffectSleep);
            this.spellStatusEffect.SetItemChecked(2, spell.EffectPoison);
            this.spellStatusEffect.SetItemChecked(3, spell.EffectFear);
            this.spellStatusEffect.SetItemChecked(4, spell.EffectBerserk);
            this.spellStatusEffect.SetItemChecked(5, spell.EffectMushroom);
            this.spellStatusEffect.SetItemChecked(6, spell.EffectScarecrow);
            this.spellStatusEffect.SetItemChecked(7, spell.EffectInvincible);
            this.spellStatusChange.SetItemChecked(0, spell.ChangeAttack);
            this.spellStatusChange.SetItemChecked(1, spell.ChangeDefense);
            this.spellStatusChange.SetItemChecked(2, spell.ChangeMagicAttack);
            this.spellStatusChange.SetItemChecked(3, spell.ChangeMagicDefense);
            this.spellTargetting.SetItemChecked(0, spell.TargetLiveAlly);
            this.spellTargetting.SetItemChecked(1, spell.TargetEnemy);
            this.spellTargetting.SetItemChecked(2, spell.TargetAll);
            this.spellTargetting.SetItemChecked(3, spell.TargetWoundedOnly);
            this.spellTargetting.SetItemChecked(4, spell.TargetOnePartyOnly);
            this.spellTargetting.SetItemChecked(5, spell.TargetNotSelf);
            this.spellAttackType.SelectedIndex = spell.AttackType;
            this.spellEffectType.SelectedIndex = spell.EffectType;
            this.spellFunction.SelectedIndex = spell.InflictFunction;
            this.spellInflictElement.SelectedIndex = spell.InflictElement;
            if (spell.EffectType == 0)
            {
                this.groupBox8.Text = "Effect <INFLICT>";
                this.groupBox9.Text = "Status <UP>";
            }
            else if (spell.EffectType == 1)
            {
                this.groupBox8.Text = "Effect <NULLIFY>";
                this.groupBox9.Text = "Status <DOWN>";
            }
            else if (spell.EffectType == 2)
            {
                this.groupBox8.Text = "Effect <. . . .>";
                this.groupBox9.Text = "Status <. . . .>";
            }
            damageModifiersBox.Visible = index < 32;
            timingPropertiesBox.Visible = index < 32;
            if (index < 32)
            {
                this.AlliesSpellTimingPointer.Value = spell.TimingPointer;
                this.AlliesSpellDamagePointer.Value = spell.DamagePointer;
            }
            this.Updating = false;
            Cursor.Current = Cursors.Arrow;
        }
        private void InitializeStrings()
        {
            this.spellName.Items.Clear();
            this.spellName.Items.AddRange(Model.SpellNames.Names);
            this.spellName.SelectedIndex = Model.SpellNames.GetSortedIndex(index);
            string[] temp = new string[96];
            for (int i = 0; i < 96; i++)
                temp[i] = i.ToString();
            this.spellNameIcon.Items.Clear();
            this.spellNameIcon.Items.AddRange(temp);
        }
        private void InsertIntoDescriptionText(string toInsert)
        {
            char[] newText = new char[textBoxSpellDescription.Text.Length + toInsert.Length];
            textBoxSpellDescription.Text.CopyTo(0, newText, 0, textBoxSpellDescription.SelectionStart);
            toInsert.CopyTo(0, newText, textBoxSpellDescription.SelectionStart, toInsert.Length);
            textBoxSpellDescription.Text.CopyTo(textBoxSpellDescription.SelectionStart, newText, textBoxSpellDescription.SelectionStart + toInsert.Length, this.textBoxSpellDescription.Text.Length - this.textBoxSpellDescription.SelectionStart);
            if (byteView)
                spell.CaretPosByteView = this.textBoxSpellDescription.SelectionStart + toInsert.Length;
            else
                spell.CaretPosTextView = this.textBoxSpellDescription.SelectionStart + toInsert.Length;
            spell.SetDescription(new string(newText), byteView);
            textBoxSpellDescription.Text = spell.GetDescription(byteView);
        }
        #endregion
        #region Event Handlers
        private void spellNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshSpells();
            settings.LastSpell = index;
        }
        private void spellName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.spellNum.Value = Model.SpellNames.GetUnsortedIndex(spellName.SelectedIndex);
        }
        private void spellName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.SpellNames,
                Model.SpellNames.GetUnsortedIndex(e.Index) < 64 ? Model.FontMenu : Model.FontDialogue,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, false, Model.MenuBG_);
        }
        private void spellNameIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.Name[0] = (char)(spellNameIcon.SelectedIndex + 0x20);
            if (Model.SpellNames.GetUnsortedName(index).CompareTo(this.textBoxSpellName.Text) != 0)
            {
                Model.SpellNames.SetName(
                    index, new string(spell.Name));
                Model.SpellNames.SortAlphabetically();
                this.spellName.Items.Clear();
                this.spellName.Items.AddRange(Model.SpellNames.Names);
                this.spellName.SelectedIndex = Model.SpellNames.GetSortedIndex(index);
            }
        }
        private void spellNameIcon_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawIcon(sender, e, new MenuTextPreview(), 32, Model.FontMenu, Model.FontPaletteMenu.Palettes[0], false, Model.MenuBG_);
        }
        private void textBoxSpellName_TextChanged(object sender, EventArgs e)
        {
            char[] raw = new char[15];
            char[] temp;
            if (spellNum.Value < 64)
                temp = Do.ASCIIToRaw(this.textBoxSpellName.Text, Lists.KeystrokesMenu, 14);
            else
                temp = Do.ASCIIToRaw(this.textBoxSpellName.Text, Lists.Keystrokes, 14);
            for (int i = 0; i < 14; i++)
                raw[i + 1] = temp[i];
            raw[0] = (char)(spellNameIcon.SelectedIndex + 32);
            if (Model.SpellNames.GetUnsortedName(index).CompareTo(this.textBoxSpellName.Text) != 0)
            {
                spell.Name = raw;
                Model.SpellNames.SetName(
                    index, new string(spell.Name));
                Model.SpellNames.SortAlphabetically();
                this.spellName.Items.Clear();
                this.spellName.Items.AddRange(Model.SpellNames.Names);
                this.spellName.SelectedIndex = Model.SpellNames.GetSortedIndex(index);
            }
        }
        private void textBoxSpellName_Leave(object sender, EventArgs e)
        {
            spellName.Items.Clear();
            spellName.Items.AddRange(Model.SpellNames.Names);
            spellName.SelectedIndex = Model.SpellNames.GetSortedIndex(index);
            InitializeStrings();
        }
        private void spellFPCost_ValueChanged(object sender, EventArgs e)
        {
            spell.FPCost = (byte)this.spellFPCost.Value;
        }
        private void spellMagPower_ValueChanged(object sender, EventArgs e)
        {
            spell.MagicPower = (byte)this.spellMagPower.Value;
        }
        private void spellHitRate_ValueChanged(object sender, EventArgs e)
        {
            spell.HitRate = (byte)this.spellHitRate.Value;
        }
        private void spellAttackType_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.AttackType = (byte)this.spellAttackType.SelectedIndex;
        }
        private void spellEffectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.EffectType = (byte)this.spellEffectType.SelectedIndex;
            if (spell.EffectType == 0)
            {
                this.groupBox8.Text = "Effect <INFLICT>";
                this.groupBox9.Text = "Status <UP>";
            }
            else if (spell.EffectType == 1)
            {
                this.groupBox8.Text = "Effect <NULLIFY>";
                this.groupBox9.Text = "Status <DOWN>";
            }
            else if (spell.EffectType == 2)
            {
                this.groupBox8.Text = "Effect <. . . .>";
                this.groupBox9.Text = "Status <. . . .>";
            }
        }

        // new pointers for timing and damage
        private void AlliesSpellTimingPointer_ValueChanged(object sender, EventArgs e)
        {
            spell.TimingPointer = (ushort)AlliesSpellTimingPointer.Value;
            if (AlliesSpellTimingPointer.Value == 0xCB0E)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 0;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCBD8)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 1;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCC44)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 2;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCD1E)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 3;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCD3F)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 4;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCDA2)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 5;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCDE1)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 6;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCE75)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 7;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCE85)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 8;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCF22)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 9;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCF63)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 10;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCFC2)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 11;
            }
            else if (AlliesSpellTimingPointer.Value == 0xCFDF)
            {
                AlliesSpellTimingAutoset.SelectedIndex = 12;
            }
            else
            {
                AlliesSpellTimingAutoset.SelectedIndex = 13;
            }
        }
        private void AlliesSpellTimingAutoset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (AlliesSpellTimingAutoset.SelectedIndex)
            {
                case 0: AlliesSpellTimingPointer.Value = 0xCB0E; break; //1 timing for x1.25 or x1.5 dmg	(Jump, Therapy, Crusher, HP Rain, Shocker, x5 Dummy)
                case 1: AlliesSpellTimingPointer.Value = 0xCBD8; break; //multiple button presses 		(Fire Orb, Super Jump, Super Flame)
                case 2: AlliesSpellTimingPointer.Value = 0xCC44; break; //1 + more targets w/ presses		(Ultra Jump, Ultra Flame)
                case 3: AlliesSpellTimingPointer.Value = 0xCD1E; break; //1 timing for x1.25 dmg only		(Group Hug, Thunderbolt)
                case 4: AlliesSpellTimingPointer.Value = 0xCD3F; break; //rotate. 1 target, if timed: all	(Sleepy Time, Mute)
                case 5: AlliesSpellTimingPointer.Value = 0xCDA2; break; //timed heals all HP to target 0	(Come Back)
                case 6: AlliesSpellTimingPointer.Value = 0xCDE1; break; //button mash				(Psych Bomb, Bowser Crush)
                case 7: AlliesSpellTimingPointer.Value = 0xCE75; break; //rotate only				(Terrorize, Poison Gas, Snowy)
                case 8: AlliesSpellTimingPointer.Value = 0xCE85; break; //Charge only				(Geno Beam, Geno Blast, Geno Flash)
                case 9: AlliesSpellTimingPointer.Value = 0xCF22; break; //timed gives target Defense Up Buff	(Geno Boost)
                case 10: AlliesSpellTimingPointer.Value = 0xCF63; break; //timed for 9999+set enemy HP to 0	(Geno Whirl)
                case 11: AlliesSpellTimingPointer.Value = 0xCFC2; break; //time to activate HP read		(Psychopath)
                case 12: AlliesSpellTimingPointer.Value = 0xCFDF; break; //timed jumps				(Star Rain)
            }
        }
        private void AlliesSpellDamagePointer_ValueChanged(object sender, EventArgs e)
        {
            spell.DamagePointer = (ushort)AlliesSpellDamagePointer.Value;
            if (AlliesSpellDamagePointer.Value == 0xD09B)
            {
                AlliesSpellDamageAutoset.SelectedIndex = 0;
            }
            else if (AlliesSpellDamagePointer.Value == 0xD09C)
            {
                AlliesSpellDamageAutoset.SelectedIndex = 1;
            }
            else if (AlliesSpellDamagePointer.Value == 0xD177)
            {
                AlliesSpellDamageAutoset.SelectedIndex = 2;
            }
            else if (AlliesSpellDamagePointer.Value == 0xD0FB)
            {
                AlliesSpellDamageAutoset.SelectedIndex = 3;
            }
            else if (AlliesSpellDamagePointer.Value == 0xD14F)
            {
                AlliesSpellDamageAutoset.SelectedIndex = 4;
            }
            else
            {
                AlliesSpellDamageAutoset.SelectedIndex = 5;
            }
        }
        private void AlliesSpellDamageAutoset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (AlliesSpellDamageAutoset.SelectedIndex)
            {
                case 0: AlliesSpellDamagePointer.Value = 0xD09B; break;	// single timing?
                case 1: AlliesSpellDamagePointer.Value = 0xD09C; break;	// rapid press single target?
                case 2: AlliesSpellDamagePointer.Value = 0xD177; break;	// multiple presses single target?
                case 3: AlliesSpellDamagePointer.Value = 0xD0FB; break; // multiple presses multi target?
                case 4: AlliesSpellDamagePointer.Value = 0xD14F; break; // rapid press multi target?
            }

        }
        //
        private void spellFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.InflictFunction = (byte)this.spellFunction.SelectedIndex;
        }
        private void spellInflictElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.InflictElement = (byte)this.spellInflictElement.SelectedIndex;
        }
        private void spellAttackProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.CheckStats = this.spellAttackProp.GetItemChecked(0);
            spell.IgnoreDefense = this.spellAttackProp.GetItemChecked(1);
            spell.CheckMortality = this.spellAttackProp.GetItemChecked(2);
            spell.UsableOverworld = this.spellAttackProp.GetItemChecked(3);
            spell.MaxAttack = this.spellAttackProp.GetItemChecked(4);
            spell.HideDigits = this.spellAttackProp.GetItemChecked(5);
        }
        private void spellStatusEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.EffectMute = this.spellStatusEffect.GetItemChecked(0);
            spell.EffectSleep = this.spellStatusEffect.GetItemChecked(1);
            spell.EffectPoison = this.spellStatusEffect.GetItemChecked(2);
            spell.EffectFear = this.spellStatusEffect.GetItemChecked(3);
            spell.EffectBerserk = this.spellStatusEffect.GetItemChecked(4);
            spell.EffectMushroom = this.spellStatusEffect.GetItemChecked(5);
            spell.EffectScarecrow = this.spellStatusEffect.GetItemChecked(6);
            spell.EffectInvincible = this.spellStatusEffect.GetItemChecked(7);
        }
        private void spellTargetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.TargetLiveAlly = this.spellTargetting.GetItemChecked(0);
            spell.TargetEnemy = this.spellTargetting.GetItemChecked(1);
            spell.TargetAll = this.spellTargetting.GetItemChecked(2);
            spell.TargetWoundedOnly = this.spellTargetting.GetItemChecked(3);
            spell.TargetOnePartyOnly = this.spellTargetting.GetItemChecked(4);
            spell.TargetNotSelf = this.spellTargetting.GetItemChecked(5);
        }
        private void spellStatusChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            spell.ChangeAttack = this.spellStatusChange.GetItemChecked(0);
            spell.ChangeDefense = this.spellStatusChange.GetItemChecked(1);
            spell.ChangeMagicAttack = this.spellStatusChange.GetItemChecked(2);
            spell.ChangeMagicDefense = this.spellStatusChange.GetItemChecked(3);
        }
        private void textBoxSpellDescription_TextChanged(object sender, EventArgs e)
        {
            char[] text = textBoxSpellDescription.Text.ToCharArray();
            char[] swap;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    int tempSel = textBoxSpellDescription.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    textBoxSpellDescription.Text = new string(swap);
                    text = textBoxSpellDescription.Text.ToCharArray();
                    i += 2;
                    textBoxSpellDescription.SelectionStart = tempSel + 2;
                }
            }
            bool flag = textHelper.VerifySymbols(this.textBoxSpellDescription.Text.ToCharArray(), byteView);
            if (flag)
            {
                this.textBoxSpellDescription.BackColor = Color.FromArgb(255, 255, 255, 255);
                spell.SetDescription(this.textBoxSpellDescription.Text, byteView);
            }
            if (!flag || spell.DescriptionError)
                this.textBoxSpellDescription.BackColor = Color.Red;
            descriptionText = null;
            pictureBoxSpellDesc.Invalidate();
        }
        private void newLine_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDescriptionText("[1]");
            else
                InsertIntoDescriptionText("[newLine]");
        }
        private void endString_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDescriptionText("[0]");
            else
                InsertIntoDescriptionText("[endInput]");
        }
        private void pictureBoxSpellDesc_Paint(object sender, PaintEventArgs e)
        {
            if (spell.RawDescription == null)
                return;
            if (descriptionFrame == null)
            {
                int[] bgPixels = Do.ImageToPixels(Model.MenuBG);
                Do.DrawMenuFrame(bgPixels, 256, new Rectangle(0, 0, 15, 8), Model.MenuFrameGraphics, Model.FontPaletteMenu.Palette);
                descriptionFrame = Do.PixelsToImage(bgPixels, 256, 256);
            }
            e.Graphics.DrawImage(descriptionFrame, 0, 0);
            if (descriptionText == null)
                SetDescriptionText();
            e.Graphics.DrawImage(descriptionText, 0, 0);
        }
        private void SetDescriptionText()
        {
            int[] pixels = new MenuDescriptionPreview().GetPreview(
                Model.FontDescription, Model.FontPaletteMenu.Palettes[0], spell.RawDescription,
                new Size(120, 88), new Point(8, 8), 6);
            descriptionText = Do.PixelsToImage(pixels, 120, 88);
            pictureBoxSpellDesc.Invalidate();
        }
        private void byteOrText_Click(object sender, EventArgs e)
        {
            this.textBoxSpellDescription.Text = spell.GetDescription(byteView);
        }
        #endregion
    }
}
