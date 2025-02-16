using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class MenusBox : NewForm
    {
        private Settings settings = Settings.Default;
        private int index { get { return 0; } }
        public int Index { get { return index; } }

        public ComboBox[] menus = new ComboBox[9];
        private ComboBox selectedItem;
        private MenusEditor menusParentEditor;
        private EditLabel labelWindow;

        // Constructor
        public MenusBox(MenusEditor MenusParentEditor)
        {
            this.menusParentEditor = MenusParentEditor;

            int push = 0;
            InitializeComponent();
            for (int i = 0; i < 9; i++)
            {
                if (i == 5) push = 4;
                this.menus[i] = new ComboBox();
                this.menus[i].BackColor = System.Drawing.SystemColors.ControlDarkDark;
                this.menus[i].DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                this.menus[i].DropDownHeight = 317;
                this.menus[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.menus[i].IntegralHeight = false;
                this.menus[i].ItemHeight = 15;
                //this.menus[i].MaxDropDownItems = 10;
                this.menus[i].Location = new System.Drawing.Point(6, i * 21 + 18 + push);
                this.menus[i].Name = "menus" + i;
                this.menus[i].Size = new System.Drawing.Size(200, 21);
                this.menus[i].TabIndex = i;
                this.menus[i].Tag = i;
                this.menus[i].DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.menuName_DrawItem);
                this.menus[i].SelectedIndexChanged += new System.EventHandler(this.menus_SelectedIndexChanged);
                this.menus[i].Click += new System.EventHandler(this.menus_Click);
                this.groupBoxMenuOverworld.Controls.Add(this.menus[i]);
                this.menus[i].Items.Add("");
                //this.menus[i].KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemsEditor.keyData_KeyDown);
                //this.menus[i].Click += new System.EventHandler(this.itemsEditor.keyData_Click);
            }
            InitializeStrings();
            //
            this.History = new History(this);
        }
        // functions
        private void InitializeStrings()
        {
            for (int i = 0; i < 9; i++)
            {
                menus[i].Items.Clear();
                menus[i].Items.AddRange(Model.MenuBoxNames);
                menus[i].SelectedIndex = Model.MenuBox[i].Menu >= 9 ? 9 : Model.MenuBox[i].Menu;
            }
        }
        public void RefreshMenus(string text, int index)
        {
            Model.MenuBoxNames[index] = text;
            for (int i = 0; i < 9; i++)
            {
                menus[i].Items.Clear();
                menus[i].Items.AddRange(Model.MenuBoxNames);
                menus[i].SelectedIndex = Model.MenuBox[i].Menu;
            }
        }
        #region Event Handlers
        private void menuName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.MenuBoxNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void menus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)((ComboBox)sender).Tag;
            Model.MenuBox[index].Menu = (byte)menus[index].SelectedIndex;
            menusParentEditor.Picture.Invalidate();
            menusParentEditor.SetTextObjects();
        }
        private void menus_Click(object sender, EventArgs e)
        {
            selectedItem = (ComboBox)sender;
        }
        private void moveUp_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item first.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = Convert.ToInt32(selectedItem.Name.Substring(5));
            if (index == 0)
                return;
            int temp = menus[index - 1].SelectedIndex;
            menus[index - 1].SelectedIndex = menus[index].SelectedIndex;
            menus[index].SelectedIndex = (byte)temp;
            selectedItem = (ComboBox)this.Controls.Find("menus" + (index - 1).ToString(), true)[0];
            //RefreshMenus();
            selectedItem.Focus();
        }
        private void moveDown_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Must select an item first.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = Convert.ToInt32(selectedItem.Name.Substring(5));
            if (index == 8)
                return;
            int temp = menus[index + 1].SelectedIndex;
            menus[index + 1].SelectedIndex = menus[index].SelectedIndex;
            menus[index].SelectedIndex = (byte)temp;
            selectedItem = (ComboBox)this.Controls.Find("menus" + (index + 1).ToString(), true)[0];
            //RefreshMenus();
            selectedItem.Focus();
        }
        #endregion
    }
}
