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
    public partial class NewGames : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private Character[] characters { get { return Model.Characters; } set { Model.Characters = value; } }
        private Character character { get { return parentEditor.Characters[parentEditor.Index]; } set { parentEditor.Characters[parentEditor.Index] = value; } }
        private Slot[] slots { get { return Model.Slots; } set { Model.Slots = value; } }
        private Slot slot { get { return slots[(int)slotNum.Value]; } set { slots[(int)slotNum.Value] = value; } }
        private int index { get { return parentEditor.Index; } set { parentEditor.Index = value; } }
        public int Index { get { return index; } set { index = value; } }
        public NewGame NewGame { get { return Model.NewGame[0]; } set { Model.NewGame[0] = value; } }
        //
        private AlliesEditor parentEditor;
        #endregion
        #region Functions
        public NewGames(AlliesEditor parentEditor)
        {
            this.parentEditor = parentEditor;
            InitializeComponent();
            InitializeStrings();
            RefreshSlots();
            this.startingCoins.Value = NewGame.StartingCoins;
            this.startingFrogCoins.Value = NewGame.StartingFrogCoins;
            this.startingCurrentFP.Value = NewGame.StartingCurrentFP;
            this.startingMaximumFP.Value = NewGame.StartingMaximumFP;
            this.lvl1TimingStart.Value = NewGame.DefenseStartL1;
            this.lvl2TimingStart.Value = NewGame.DefenseStartL2;
            this.lvl2TimingEnd.Value = NewGame.DefenseEndL2;
            this.lvl1TimingEnd.Value = NewGame.DefenseEndL1;
            //
            this.History = new History(this);
        }
        private void InitializeStrings()
        {
            this.Updating = true;
            this.startingItem.Items.Clear();
            this.startingItem.Items.AddRange(Model.ItemNames.Names);
            this.startingSpecialItem.Items.Clear();
            this.startingSpecialItem.Items.AddRange(Model.ItemNames.Names);
            this.startingEquipment.Items.Clear();
            this.startingEquipment.Items.AddRange(Model.ItemNames.Names);
            this.Updating = false;
        }
        public void RefreshSlots()
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

            this.startingCoins.Value = NewGame.StartingCoins;
            this.startingFrogCoins.Value = NewGame.StartingFrogCoins;
            this.startingCurrentFP.Value = NewGame.StartingCurrentFP;
            this.startingMaximumFP.Value = NewGame.StartingMaximumFP;
            this.lvl1TimingStart.Value = NewGame.DefenseStartL1;
            this.lvl2TimingStart.Value = NewGame.DefenseStartL2;
            this.lvl2TimingEnd.Value = NewGame.DefenseEndL2;
            this.lvl1TimingEnd.Value = NewGame.DefenseEndL1;

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
        private void startingCoins_ValueChanged(object sender, EventArgs e)
        {
            NewGame.StartingCoins = (ushort)this.startingCoins.Value;
        }
        private void startingFrogCoins_ValueChanged(object sender, EventArgs e)
        {
            NewGame.StartingFrogCoins = (ushort)this.startingFrogCoins.Value;
        }
        private void startingCurrentFP_ValueChanged(object sender, EventArgs e)
        {
            NewGame.StartingCurrentFP = (byte)this.startingCurrentFP.Value;
        }
        private void startingMaximumFP_ValueChanged(object sender, EventArgs e)
        {
            NewGame.StartingMaximumFP = (byte)this.startingMaximumFP.Value;
        }
        // defense timing
        private void lvl1TimingStart_ValueChanged(object sender, EventArgs e)
        {
            NewGame.DefenseStartL1 = (byte)this.lvl1TimingStart.Value;
        }
        private void lvl2TimingStart_ValueChanged(object sender, EventArgs e)
        {
            NewGame.DefenseStartL2 = (byte)this.lvl2TimingStart.Value;
        }
        private void lvl2TimingEnd_ValueChanged(object sender, EventArgs e)
        {
            NewGame.DefenseEndL2 = (byte)this.lvl2TimingEnd.Value;
        }
        private void lvl1TimingEnd_ValueChanged(object sender, EventArgs e)
        {
            NewGame.DefenseEndL1 = (byte)this.lvl1TimingEnd.Value;
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
        #endregion
    }
}
