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
    public partial class Attacks : NewForm
    {
        // variables
        //
        private Settings settings = Settings.Default;
        private Attack[] attacks { get { return Model.Attacks; } set { Model.Attacks = value; } }
        public Attack Attack { get { return attacks[index]; } set { attacks[index] = value; } }
        private int index { get { return (int)attackNum.Value; } set { attackNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private EditLabel labelWindow;
        // constructor
        public Attacks()
        {
            InitializeComponent();
            InitializeStrings();
            RefreshAttacks();
            labelWindow = new EditLabel(attackName, attackNum, "Attacks", false);
            //
            this.History = new History(this, attackName, attackNum);
        }
        // functions
        private void InitializeStrings()
        {
            this.attackName.Items.Clear();
            this.attackName.Items.AddRange(Model.AttackNames.Names);
        }
        public void RefreshAttacks()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            this.attackName.SelectedIndex = Model.AttackNames.GetSortedIndex(index);
            this.attackHitRate.Value = attacks[index].HitRate;
            this.attackAtkLevel.Value = attacks[index].AttackLevel;
            this.textBoxAttackName.Text = Do.RawToASCII(attacks[index].Name, Lists.Keystrokes);
            this.attackStatusEffect.SetItemChecked(0, attacks[index].EffectMute);
            this.attackStatusEffect.SetItemChecked(1, attacks[index].EffectSleep);
            this.attackStatusEffect.SetItemChecked(2, attacks[index].EffectPoison);
            this.attackStatusEffect.SetItemChecked(3, attacks[index].EffectFear);
            this.attackStatusEffect.SetItemChecked(4, attacks[index].EffectBerserk);
            this.attackStatusEffect.SetItemChecked(5, attacks[index].EffectMushroom);
            this.attackStatusEffect.SetItemChecked(6, attacks[index].EffectScarecrow);
            this.attackStatusEffect.SetItemChecked(7, attacks[index].EffectInvincible);
            this.attackStatusUp.SetItemChecked(0, attacks[index].UpAttack);
            this.attackStatusUp.SetItemChecked(1, attacks[index].UpDefense);
            this.attackStatusUp.SetItemChecked(2, attacks[index].UpMagicAttack);
            this.attackStatusUp.SetItemChecked(3, attacks[index].UpMagicDefense);
            this.attackAtkType.SetItemChecked(0, attacks[index].InstantDeath);
            this.attackAtkType.SetItemChecked(1, attacks[index].NoDamageA);
            this.attackAtkType.SetItemChecked(2, attacks[index].HideDigits);
            this.attackAtkType.SetItemChecked(3, attacks[index].NoDamageB);
            this.Updating = false;
        }
        #region Event Handlers
        private void attackNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshAttacks();
            settings.LastAttack = index;
        }
        private void attackName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.attackNum.Value = Model.AttackNames.GetUnsortedIndex(attackName.SelectedIndex);
        }
        private void attackName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.AttackNames, Model.FontDialogue,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, true, Model.MenuBG_);
        }
        private void textBoxAttackName_TextChanged(object sender, EventArgs e)
        {
            if (Model.AttackNames.GetUnsortedName(index).CompareTo(this.textBoxAttackName.Text) != 0)
            {
                attacks[index].Name = Do.ASCIIToRaw(this.textBoxAttackName.Text, Lists.Keystrokes, 13);
                Model.AttackNames.SetName(
                    index, new string(attacks[index].Name));
                Model.AttackNames.SortAlphabetically();
                this.attackName.Items.Clear();
                this.attackName.Items.AddRange(Model.AttackNames.Names);
                this.attackName.SelectedIndex = Model.AttackNames.GetSortedIndex(index);
            }
        }
        private void attackHitRate_ValueChanged(object sender, EventArgs e)
        {
            attacks[index].HitRate = (byte)attackHitRate.Value;
        }
        private void attackAtkLevel_ValueChanged(object sender, EventArgs e)
        {
            attacks[index].AttackLevel = (byte)attackAtkLevel.Value;
        }
        private void attackStatusEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            attacks[index].EffectMute = this.attackStatusEffect.GetItemChecked(0);
            attacks[index].EffectSleep = this.attackStatusEffect.GetItemChecked(1);
            attacks[index].EffectPoison = this.attackStatusEffect.GetItemChecked(2);
            attacks[index].EffectFear = this.attackStatusEffect.GetItemChecked(3);
            attacks[index].EffectBerserk = this.attackStatusEffect.GetItemChecked(4);
            attacks[index].EffectMushroom = this.attackStatusEffect.GetItemChecked(5);
            attacks[index].EffectScarecrow = this.attackStatusEffect.GetItemChecked(6);
            attacks[index].EffectInvincible = this.attackStatusEffect.GetItemChecked(7);
        }
        private void attackStatusUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            attacks[index].UpAttack = this.attackStatusUp.GetItemChecked(0);
            attacks[index].UpDefense = this.attackStatusUp.GetItemChecked(1);
            attacks[index].UpMagicAttack = this.attackStatusUp.GetItemChecked(2);
            attacks[index].UpMagicDefense = this.attackStatusUp.GetItemChecked(3);
        }
        private void attackAtkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            attacks[index].InstantDeath = this.attackAtkType.GetItemChecked(0);
            attacks[index].NoDamageA = this.attackAtkType.GetItemChecked(1);
            attacks[index].HideDigits = this.attackAtkType.GetItemChecked(2);
            attacks[index].NoDamageB = this.attackAtkType.GetItemChecked(3);
        }
        #endregion
    }
}
