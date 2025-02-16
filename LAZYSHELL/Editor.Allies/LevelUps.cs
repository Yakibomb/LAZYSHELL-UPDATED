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
    public partial class LevelUps : NewForm
    {
        #region Variables
        //
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return characters[index]; } set { characters[index] = value; } }
        private int index { get { return characterName.SelectedIndex; } set { characterName.SelectedIndex = value; } }
        #endregion
        // constructor
        public LevelUps()
        {
            InitializeComponent();
            InitializeStrings();
            index = 0;
            RefreshLevel();
            //
            this.History = new History(this, characterName, null);
        }
        // functions
        private void InitializeStrings()
        {
            this.Updating = true;
            this.characterName.Items.Clear();
            for (int i = 0; i < characters.Length; i++)
                this.characterName.Items.Add(new string(characters[i].Name));
            this.levelUpSpellLearned.Items.Clear();
            for (int i = 0; i < 32; i++)
                this.levelUpSpellLearned.Items.Add(new string(Model.Spells[i].Name));
            this.levelUpSpellLearned.Items.Add("{NOTHING}");
            this.Updating = false;
        }
        public void RefreshLevel()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            character.IndexLevel = (byte)levelNum.Value;
            this.hpPlus.Value = character.LevelHpPlus;
            this.attackPlus.Value = character.LevelAttackPlus;
            this.defensePlus.Value = character.LevelDefensePlus;
            this.mgAttackPlus.Value = character.LevelMgAttackPlus;
            this.mgDefensePlus.Value = character.LevelMgDefensePlus;
            this.hpPlusBonus.Value = character.LevelHpPlusBonus;
            this.attackPlusBonus.Value = character.LevelAttackPlusBonus;
            this.defensePlusBonus.Value = character.LevelDefensePlusBonus;
            this.mgAttackPlusBonus.Value = character.LevelMgAttackPlusBonus;
            this.mgDefensePlusBonus.Value = character.LevelMgDefensePlusBonus;
            this.expNeeded.Value = characters[0].LevelExpNeeded;
            this.levelUpSpellLearned.SelectedIndex = character.LevelSpellLearned;
            this.characterName.Invalidate();
            this.Updating = false;
        }
        #region Event Handlers
        private void characterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLevel();
        }
        private void characterName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
        private void levelNum_ValueChanged(object sender, EventArgs e)
        {
            foreach (Character character in characters)
                character.IndexLevel = (byte)levelNum.Value;
            RefreshLevel();
        }
        private void expNeeded_ValueChanged(object sender, EventArgs e)
        {
            characters[0].LevelExpNeeded = (ushort)this.expNeeded.Value;
        }
        private void hpPlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelHpPlus = (byte)this.hpPlus.Value;
        }
        private void attackPlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelAttackPlus = (byte)this.attackPlus.Value;
        }
        private void defensePlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelDefensePlus = (byte)this.defensePlus.Value;
        }
        private void mgAttackPlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgAttackPlus = (byte)this.mgAttackPlus.Value;
        }
        private void mgDefensePlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgDefensePlus = (byte)this.mgDefensePlus.Value;
        }
        private void hpPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelHpPlusBonus = (byte)this.hpPlusBonus.Value;
        }
        private void attackPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelAttackPlusBonus = (byte)this.attackPlusBonus.Value;
        }
        private void defensePlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelDefensePlusBonus = (byte)this.defensePlusBonus.Value;
        }
        private void mgAttackPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgAttackPlusBonus = (byte)this.mgAttackPlusBonus.Value;
        }
        private void mgDefensePlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgDefensePlusBonus = (byte)this.mgDefensePlusBonus.Value;
        }
        private void levelUpSpellLearned_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.LevelSpellLearned = (byte)this.levelUpSpellLearned.SelectedIndex;
        }
        private void levelUpSpellLearned_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Spells, 33),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, true, false, Model.MenuBG_);
        }
        //
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's level-up index. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            character = new Character(index);
            characterName_SelectedIndexChanged(null, null);
        }
        #endregion
    }
}
