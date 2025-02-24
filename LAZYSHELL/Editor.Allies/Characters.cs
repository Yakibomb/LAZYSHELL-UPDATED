using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Characters : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return parentEditor.Characters[parentEditor.Index]; } set { parentEditor.Characters[parentEditor.Index] = value; } }

        private int index { get { return parentEditor.Index; } set { parentEditor.Index = value; } }
        public int Index { get { return index; } set { index = value; } }
        //
        private AlliesEditor parentEditor;
        #endregion
        #region Functions
        public Characters(AlliesEditor parentEditor)
        {
            this.parentEditor = parentEditor;
            InitializeComponent();
            InitializeStrings();
            RefreshCharacter();
            //
            this.History = new History(this);
        }
        private void InitializeStrings()
        {
            this.Updating = true;
            this.startingMagic.Items.Clear();
            for (int i = 0; i < 32; i++)
                this.startingMagic.Items.Add(new string(Model.Spells[i].Name));
            this.startingWeapon.Items.Clear();
            this.startingWeapon.Items.AddRange(Model.ItemNames.Names);
            this.startingAccessory.Items.Clear();
            this.startingAccessory.Items.AddRange(Model.ItemNames.Names);
            this.startingArmor.Items.Clear();
            this.startingArmor.Items.AddRange(Model.ItemNames.Names);
            this.Updating = false;
        }
        public void RefreshCharacter()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            this.textBoxCharacterName.Text = Do.RawToASCII(character.Name, Lists.KeystrokesMenu);
            this.startingLevel.Value = character.StartingLevel;
            this.startingAttack.Value = character.StartingAttack;
            this.startingDefense.Value = character.StartingDefense;
            this.startingMgAttack.Value = character.StartingMgAttack;
            this.startingMgDefense.Value = character.StartingMgDefense;
            this.startingSpeed.Value = character.StartingSpeed;
            this.startingWeapon.SelectedIndex = Model.ItemNames.GetSortedIndex(character.StartingWeapon);
            this.startingArmor.SelectedIndex = Model.ItemNames.GetSortedIndex(character.StartingArmor);
            this.startingAccessory.SelectedIndex = Model.ItemNames.GetSortedIndex(character.StartingAccessory);
            this.startingExperience.Value = character.StartingExperience;
            this.startingCurrentHP.Value = character.StartingCurrentHP;
            this.startingMaximumHP.Value = character.StartingMaxHP;
            // All selected Magic
            for (int i = 0; i < character.StartingMagic.Length; i++)
                this.startingMagic.SetItemChecked(i, character.StartingMagic[i]);
            this.Updating = false;
        }
        #endregion
        #region Event Handlers
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            character.Name = Do.ASCIIToRaw(textBoxCharacterName.Text, Lists.KeystrokesMenu, 10);
            this.Updating = true;
            Model.CharacterNames.SetName(index, new string(character.Name));
            this.Updating = false;
        }
        private void textBoxCharacterName_Leave(object sender, EventArgs e)
        {
            InitializeStrings();
            parentEditor.RefreshAllyToolTipText();
        }
        private void startingLevel_ValueChanged(object sender, EventArgs e)
        {
            character.StartingLevel = (byte)this.startingLevel.Value;
        }
        private void startingAttack_ValueChanged(object sender, EventArgs e)
        {
            character.StartingAttack = (byte)this.startingAttack.Value;
        }
        private void startingDefense_ValueChanged(object sender, EventArgs e)
        {
            character.StartingDefense = (byte)this.startingDefense.Value;
        }
        private void startingMgAttack_ValueChanged(object sender, EventArgs e)
        {
            character.StartingMgAttack = (byte)this.startingMgAttack.Value;
        }
        private void startingMgDefense_ValueChanged(object sender, EventArgs e)
        {
            character.StartingMgDefense = (byte)this.startingMgDefense.Value;
        }
        private void startingSpeed_ValueChanged(object sender, EventArgs e)
        {
            character.StartingSpeed = (byte)this.startingSpeed.Value;
        }
        private void startingWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.StartingWeapon = (byte)Model.ItemNames.GetUnsortedIndex(this.startingWeapon.SelectedIndex);
        }
        private void startingArmor_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.StartingArmor = (byte)Model.ItemNames.GetUnsortedIndex(this.startingArmor.SelectedIndex);
        }
        private void startingAccessory_SelectedIndexChanged(object sender, EventArgs e)
        {
            character.StartingAccessory = (byte)Model.ItemNames.GetUnsortedIndex(this.startingAccessory.SelectedIndex);
        }
        private void startingExperience_ValueChanged(object sender, EventArgs e)
        {
            character.StartingExperience = (ushort)this.startingExperience.Value;
        }
        private void startingCurrentHP_ValueChanged(object sender, EventArgs e)
        {
            character.StartingCurrentHP = (ushort)this.startingCurrentHP.Value;
        }
        private void startingMaximumHP_ValueChanged(object sender, EventArgs e)
        {
            character.StartingMaxHP = (ushort)this.startingMaximumHP.Value;
        }
        private void startingMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < character.StartingMagic.Length; i++)
                character.StartingMagic[i] = this.startingMagic.GetItemChecked(i);
        }
        private void startingMagic_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Spells, 32, 1),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], -8, 10, 0, 0, false, false,
                Model.MenuBG__(startingMagic.ColumnWidth, 255));
        }
        #endregion
    }
}
