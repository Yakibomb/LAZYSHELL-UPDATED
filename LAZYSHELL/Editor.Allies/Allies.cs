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
    public partial class Allies : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return characters[index]; } set { characters[index] = value; } }
        private Slot[] slots { get { return Model.Slots; } set { Model.Slots = value; } }
        private Slot slot { get { return slots[(int)slotNum.Value]; } set { slots[(int)slotNum.Value] = value; } }
        //
        private int index = 0; public int Index { get { return index; } }
        #endregion
        #region Functions
        public Allies()
        {
            InitializeComponent();
            InitializeStrings();
            RefreshCharacter();
            RefreshSlots();
            this.startingCoins.Value = characters[0].StartingCoins;
            this.startingFrogCoins.Value = characters[0].StartingFrogCoins;
            this.startingCurrentFP.Value = characters[0].StartingCurrentFP;
            this.startingMaximumFP.Value = characters[0].StartingMaximumFP;
            this.lvl1TimingStart.Value = characters[0].DefenseStartL1;
            this.lvl2TimingStart.Value = characters[0].DefenseStartL2;
            this.lvl2TimingEnd.Value = characters[0].DefenseEndL2;
            this.lvl1TimingEnd.Value = characters[0].DefenseEndL1;
            //
            this.History = new History(this, characterName, null);
        }
        private void InitializeStrings()
        {
            this.Updating = true;
            this.characterName.Items.Clear();
            for (int i = 0; i < characters.Length; i++)
                this.characterName.Items.Add(new string(characters[i].Name));
            this.characterName.SelectedIndex = index;
            this.startingMagic.Items.Clear();
            for (int i = 0; i < 32; i++)
                this.startingMagic.Items.Add(new string(Model.Spells[i].Name));
            this.startingWeapon.Items.Clear();
            this.startingWeapon.Items.AddRange(Model.ItemNames.Names);
            this.startingAccessory.Items.Clear();
            this.startingAccessory.Items.AddRange(Model.ItemNames.Names);
            this.startingArmor.Items.Clear();
            this.startingArmor.Items.AddRange(Model.ItemNames.Names);
            this.startingItem.Items.Clear();
            this.startingItem.Items.AddRange(Model.ItemNames.Names);
            this.startingSpecialItem.Items.Clear();
            this.startingSpecialItem.Items.AddRange(Model.ItemNames.Names);
            this.startingEquipment.Items.Clear();
            this.startingEquipment.Items.AddRange(Model.ItemNames.Names);
            this.Updating = false;
        }
        public void RefreshCharacter()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            this.characterName.SelectedIndex = index;
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
            this.characterName.Invalidate();
            this.Updating = false;
        }
        private void RefreshSlots()
        {
            this.startingItem.SelectedIndex = Model.ItemNames.GetSortedIndex(slot.Item);
            if (this.slotNum.Value <= 14)
            {
                this.startingSpecialItem.Enabled = true;
                this.startingSpecialItem.SelectedIndex = Model.ItemNames.GetSortedIndex(slot.SpecialItem);
            }
            else
            {
                this.startingSpecialItem.Enabled = false;
                this.startingSpecialItem.SelectedIndex = 0;
            }
            this.startingEquipment.SelectedIndex = Model.ItemNames.GetSortedIndex(slot.Equipment);
        }
        #endregion
        #region Event Handlers
        private void characterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            index = characterName.SelectedIndex;
            RefreshCharacter();
        }
        private void characterName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
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
            this.characterName.Items.Clear();
            for (int i = 0; i < characters.Length; i++)
                this.characterName.Items.Add(new string(characters[i].Name));
            this.characterName.SelectedIndex = index;
            this.Updating = false;
        }
        private void textBoxCharacterName_Leave(object sender, EventArgs e)
        {
            InitializeStrings();
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
        private void startingCoins_ValueChanged(object sender, EventArgs e)
        {
            characters[0].StartingCoins = (ushort)this.startingCoins.Value;
        }
        private void startingFrogCoins_ValueChanged(object sender, EventArgs e)
        {
            characters[0].StartingFrogCoins = (ushort)this.startingFrogCoins.Value;
        }
        private void startingCurrentFP_ValueChanged(object sender, EventArgs e)
        {
            characters[0].StartingCurrentFP = (byte)this.startingCurrentFP.Value;
        }
        private void startingMaximumFP_ValueChanged(object sender, EventArgs e)
        {
            characters[0].StartingMaximumFP = (byte)this.startingMaximumFP.Value;
        }
        // defense timing
        private void lvl1TimingStart_ValueChanged(object sender, EventArgs e)
        {
            characters[0].DefenseStartL1 = (byte)this.lvl1TimingStart.Value;
        }
        private void lvl2TimingStart_ValueChanged(object sender, EventArgs e)
        {
            characters[0].DefenseStartL2 = (byte)this.lvl2TimingStart.Value;
        }
        private void lvl2TimingEnd_ValueChanged(object sender, EventArgs e)
        {
            characters[0].DefenseEndL2 = (byte)this.lvl2TimingEnd.Value;
        }
        private void lvl1TimingEnd_ValueChanged(object sender, EventArgs e)
        {
            characters[0].DefenseEndL1 = (byte)this.lvl1TimingEnd.Value;
        }
        // slots
        private void slotNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshSlots();
        }
        private void startingItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot.Item = (byte)Model.ItemNames.GetUnsortedIndex(this.startingItem.SelectedIndex);
        }
        private void startingSpecialItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot.SpecialItem = (byte)Model.ItemNames.GetUnsortedIndex(this.startingSpecialItem.SelectedIndex);
        }
        private void startingEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot.Equipment = (byte)Model.ItemNames.GetUnsortedIndex(this.startingEquipment.SelectedIndex);
        }
        //
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character. Go ahead with reset?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            character = new Character(index);
            characterName_SelectedIndexChanged(null, null);
        }
        #endregion
    }
}
