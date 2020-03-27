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
    public partial class Shops : NewForm
    {
        private Settings settings = Settings.Default;
        private Shop[] shops { get { return Model.Shops; } set { Model.Shops = value; } }
        private Shop shop { get { return shops[index]; } set { shops[index] = value; } }
        public Shop Shop { get { return shop; } set { shop = value; } }
                private int index { get { return (int)shopName.SelectedIndex; } set { shopName.SelectedIndex = value; } }
        public int Index { get { return index; } set { index = value; } }
        private ComboBox[] shopItems = new ComboBox[15];
        private ComboBox selectedItem;
        private ItemsEditor itemsEditor;
        private EditLabel labelWindow;
        // Constructor
        public Shops(ItemsEditor itemsEditor)
        {
            this.itemsEditor = itemsEditor;
            InitializeComponent();
            for (int i = 0; i < 15; i++)
            {
                this.shopItems[i] = new ComboBox();
                this.shopItems[i].BackColor = System.Drawing.SystemColors.ControlDarkDark;
                this.shopItems[i].DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                this.shopItems[i].DropDownHeight = 317;
                this.shopItems[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.shopItems[i].IntegralHeight = false;
                this.shopItems[i].ItemHeight = 15;
                this.shopItems[i].Location = new System.Drawing.Point(6, i * 21 + 20);
                this.shopItems[i].Name = "shopItem" + i;
                this.shopItems[i].Size = new System.Drawing.Size(173, 21);
                this.shopItems[i].TabIndex = i;
                this.shopItems[i].Tag = i;
                this.shopItems[i].DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
                this.shopItems[i].SelectedIndexChanged += new System.EventHandler(this.shopItem_SelectedIndexChanged);
                this.shopItems[i].Click += new System.EventHandler(this.shopItem_Click);
                this.groupBoxItems.Controls.Add(this.shopItems[i]);
            }
            InitializeStrings();
            index = 0;
            RefreshShops();
            labelWindow = new EditLabel(shopName, null, "Shops", true);
            //
            this.History = new History(this, shopName, null);
        }
        // functions
        private void InitializeStrings()
        {
            shopName.Items.AddRange(Lists.ShopNames);
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopItems[i].Items.Clear();
                shopItems[i].Items.AddRange(Model.ItemNames.Names);
            }
        }
        public void ResortStrings()
        {
            this.Updating = true;
            for (int i = 0; i < shopItems.Length; i++)
                shopItems[i].SelectedIndex = Model.ItemNames.GetSortedIndex(shop.Items[i]);
            this.Updating = false;
        }
        public void RefreshShops()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            for (int i = 0; i < shopItems.Length; i++)
                shopItems[i].SelectedIndex = Model.ItemNames.GetSortedIndex(shop.Items[i]);
            this.shopBuyOptions.SetItemChecked(0, shop.BuyFrogCoinOne);
            this.shopBuyOptions.SetItemChecked(1, shop.BuyFrogCoin);
            this.shopBuyOptions.SetItemChecked(2, shop.BuyOnlyA);
            this.shopBuyOptions.SetItemChecked(3, shop.BuyOnlyB);
            this.shopDiscounts.SetItemChecked(0, shop.Discount6);
            this.shopDiscounts.SetItemChecked(1, shop.Discount12);
            this.shopDiscounts.SetItemChecked(2, shop.Discount25);
            this.shopDiscounts.SetItemChecked(3, shop.Discount50);
            this.Updating = false;
        }
        #region Event Handlers
        private void shopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshShops();
        }
        private void shopName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.ShopNames,
                Model.FontDialogue, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, false, Model.MenuBG_);
        }
        private void shopItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)((ComboBox)sender).Tag;
            shop.Items[index] = (byte)Model.ItemNames.GetUnsortedIndex(shopItems[index].SelectedIndex);
        }
        private void shopItem_Click(object sender, EventArgs e)
        {
            selectedItem = (ComboBox)sender;
        }
        private void shopBuyOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            shop.BuyFrogCoinOne = this.shopBuyOptions.GetItemChecked(0);
            shop.BuyFrogCoin = this.shopBuyOptions.GetItemChecked(1);
            shop.BuyOnlyA = this.shopBuyOptions.GetItemChecked(2);
            shop.BuyOnlyB = this.shopBuyOptions.GetItemChecked(3);
        }
        private void shopDiscounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            shop.Discount6 = this.shopDiscounts.GetItemChecked(0);
            shop.Discount12 = this.shopDiscounts.GetItemChecked(1);
            shop.Discount25 = this.shopDiscounts.GetItemChecked(2);
            shop.Discount50 = this.shopDiscounts.GetItemChecked(3);
        }
        private void moveUp_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item first.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = Convert.ToInt32(selectedItem.Name.Substring(8));
            if (index == 0)
                return;
            int temp = shop.Items[index - 1];
            shop.Items[index - 1] = shop.Items[index];
            shop.Items[index] = (byte)temp;
            selectedItem = (ComboBox)this.Controls.Find("shopItem" + (index - 1).ToString(), true)[0];
            RefreshShops();
            selectedItem.Focus();
        }
        private void moveDown_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item first.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = Convert.ToInt32(selectedItem.Name.Substring(8));
            if (index == 14)
                return;
            int temp = shop.Items[index + 1];
            shop.Items[index + 1] = shop.Items[index];
            shop.Items[index] = (byte)temp;
            selectedItem = (ComboBox)this.Controls.Find("shopItem" + (index + 1).ToString(), true)[0];
            RefreshShops();
            selectedItem.Focus();
        }
        #endregion
    }
}
