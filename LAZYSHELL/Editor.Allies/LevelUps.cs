using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LAZYSHELL
{
    public partial class LevelUps : NewForm
    {
        #region Variables
        //
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return parentEditor.Characters[parentEditor.Index]; } set { parentEditor.Characters[parentEditor.Index] = value; } }
        private int index { get { return parentEditor.Index; } set { parentEditor.Index = value; } }
        public int Index { get { return index; } set { index = value; } }
        //
        private AlliesEditor parentEditor;
        public LevelUpsCalculator calculator;
        #endregion
        // constructor
        public LevelUps(AlliesEditor parentEditor)
        {
            this.parentEditor = parentEditor;
            InitializeComponent();
            InitializeStrings();
            index = 0;
            RefreshLevel();
            //
            this.History = new History(this, false);
        }
        // functions
        private void InitializeStrings()
        {
            this.Updating = true;
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
            this.Updating = false;
        }
        #region Event Handlers
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
            if (calculator != null) calculator.CalculateLevel();
        }
        private void attackPlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelAttackPlus = (byte)this.attackPlus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void defensePlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelDefensePlus = (byte)this.defensePlus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void mgAttackPlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgAttackPlus = (byte)this.mgAttackPlus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void mgDefensePlus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgDefensePlus = (byte)this.mgDefensePlus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void hpPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelHpPlusBonus = (byte)this.hpPlusBonus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void attackPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelAttackPlusBonus = (byte)this.attackPlusBonus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void defensePlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelDefensePlusBonus = (byte)this.defensePlusBonus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void mgAttackPlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgAttackPlusBonus = (byte)this.mgAttackPlusBonus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void mgDefensePlusBonus_ValueChanged(object sender, EventArgs e)
        {
            character.LevelMgDefensePlusBonus = (byte)this.mgDefensePlusBonus.Value;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void levelUpSpellLearned_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.LevelSpellLearned = (byte)this.levelUpSpellLearned.SelectedIndex;
            if (calculator != null) calculator.CalculateLevel();
        }
        private void levelUpSpellLearned_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Spells, 33),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, true, false, Model.MenuBG_);
        }
        private void calculatorButton_Click(object sender, EventArgs e)
        {
            settings.PreviewSprites = index;
            if (calculator == null || !calculator.Visible)
                calculator = new LevelUpsCalculator(index);
            else
                calculator.Reload(index);
            calculator.Show();
            calculator.BringToFront();
        }
        #endregion

    }
}
