using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class MenusEditor : NewForm
    {
        private Menus menus; public Menus Menus { get { return menus; } set { menus = value; } }
        private Settings settings = Settings.Default;
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        public int Index { get { return menuTextName.SelectedIndex; } set { menuTextName.SelectedIndex = value; } }
        private MenuTexts menuText { get { return Model.MenuTexts[Index]; } set { Model.MenuTexts[Index] = value; } }
                //
        public MenusEditor()
        {
            //
            InitializeComponent();
            Do.AddShortcut(toolStrip2, Keys.Control | Keys.S, new EventHandler(save_Click));
            LoadOverworldEditor();
            //
            for (int i = 0; i < Model.MenuTexts.Length; i++)
                this.menuTextName.Items.Add(Model.MenuTexts[i].GetMenuString(textView.Checked));
            this.Index = 0;
            //
            menus.TopLevel = false;
            menus.Dock = DockStyle.Fill;
            //overworld.SetToolTips(toolTip1);
            panel1.Controls.Add(menus);
            menus.BringToFront();
            //openMenus.Checked = true;
            menus.Visible = true;
            new ToolTipLabel(this, null, helpTips);
            //
            this.History = new History(this, false);
            GC.Collect();
        }
        private void RefreshMenuText()
        {
            this.Updating = true;
            this.menuTextBox.Text = menuText.GetMenuString(textView.Checked);
            this.toolStripSeparator2.Visible =
                this.toolStripLabel2.Visible =
                this.xCoord.Visible = menuText.Index >= 14 && menuText.Index <= 19;
            this.xCoord.Value = menuText.X;
            CalculateFreeSpace();
            this.Updating = false;
        }
        private void LoadOverworldEditor()
        {
            if (menus == null)
                menus = new Menus(this);
            else
                menus.Reload(this);
        }
        private int CalculateFreeSpace()
        {
            int used = 0;
            MenuTexts lastMenuText = null;
            foreach (MenuTexts menuText in Model.MenuTexts)
            {
                if (lastMenuText != null && menuText.Length != 0 && Bits.Compare(menuText.Text, lastMenuText.Text))
                    continue;
                lastMenuText = menuText;
                used += menuText.Length + 1;
            }
            int left = 0x700 - used;
            this.charactersLeft.Text = "(" + left.ToString() + " characters left)";
            this.charactersLeft.BackColor = left >= 0 ? SystemColors.Control : Color.Red;
            return left;
        }
        private void Assemble()
        {
            int offset = 0;
            byte[] temp = new byte[0x700];
            MenuTexts lastMenuText = null;
            foreach (MenuTexts menuText in Model.MenuTexts)
            {
                if (lastMenuText != null && menuText.Length != 0 && Bits.Compare(menuText.Text, lastMenuText.Text))
                {
                    Bits.SetShort(Model.ROM, menuText.Index * 2 + 0x3EEF00, lastMenuText.Offset);
                    menuText.Offset = lastMenuText.Offset;
                    continue;
                }
                if (offset + menuText.Length + 1 >= 0x700)
                {
                    MessageBox.Show("Menu texts exceed allotted ROM space. Stopped saving at index " + menuText.Index + ".");
                    break;
                }
                menuText.Offset = offset;
                lastMenuText = menuText;
                //
                Bits.SetShort(Model.ROM, menuText.Index * 2 + 0x3EEF00, offset);
                Bits.SetChars(temp, offset, menuText.Text);
                offset += menuText.Length;
                temp[offset++] = 0;
                switch (menuText.Index)
                {
                    case 14: Bits.SetByteBits(Model.ROM, 0x03328E, (byte)(menuText.X * 2), 0x3F); break;
                    case 15: Bits.SetByteBits(Model.ROM, 0x03327E, (byte)(menuText.X * 2), 0x3F); break;
                    case 16: Bits.SetByteBits(Model.ROM, 0x033282, (byte)(menuText.X * 2), 0x3F); break;
                    case 17: Bits.SetByteBits(Model.ROM, 0x033286, (byte)(menuText.X * 2), 0x3F); break;
                    case 18: Bits.SetByteBits(Model.ROM, 0x03328A, (byte)(menuText.X * 2), 0x3F); break;
                    case 19: Bits.SetByteBits(Model.ROM, 0x03327A, (byte)(menuText.X * 2), 0x3F); break;
                }
            }
            Bits.SetBytes(Model.ROM, 0x3EF000, temp);
            //Bits.SetShort(Model.Data, 0x3EF600, 0x344F);
            menus.Assemble();
            menus.Modified = false;
            this.Modified = false;
        }
        //
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void MenusEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !menus.Modified)
                goto Close;
            DialogResult result = MessageBox.Show(
                "Menus have not been saved.\n\nWould you like to save changes?", "LAZY SHELL",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.MenuFrameGraphics = null;
                Model.FontPaletteMenu = null;
                Model.MenuBGGraphics = null;
                Model.MenuBGPalette = null;
                Model.ShopBGPalette = null;
                Model.MenuBGTileset = null;
                Model.MenuCursorGraphics = null;
                //
                Model.GameSelectGraphics = null;
                Model.GameSelectPalettes = null;
                Model.GameSelectPaletteSet = null;
                Model.GameSelectTileset = null;
                //
                Model.MenuTexts = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            menus.Close();
        }
        private void menuTextNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            menuTextName.SelectedIndex = (int)menuTextNum.Value;
            this.Updating = false;
            RefreshMenuText();
        }
        private void menuTextName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            menuTextNum.Value = menuTextName.SelectedIndex;
            this.Updating = false;
            RefreshMenuText();
        }
        private void menuTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            //
            char[] text = menuTextBox.Text.ToCharArray();
            char[] swap;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    int tempSel = menuTextBox.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    menuTextBox.Text = new string(swap);
                    text = menuTextBox.Text.ToCharArray();
                    i += 2;
                    menuTextBox.SelectionStart = tempSel + 2;
                }
            }
            if (textHelper.VerifySymbols(this.menuTextBox.Text.ToCharArray(), !textView.Checked))
            {
                menuText.SetMenuString(menuTextBox.Text, textView.Checked);
                menuText.Error = textHelper.Error;
                this.Updating = true;
                int index = this.Index;
                menuTextName.Items.RemoveAt(index);
                menuTextName.Items.Insert(index, menuTextBox.Text);
                menuTextName.Text = menuTextBox.Text;
                menuTextName.Invalidate();
                this.Index = index;
                this.Updating = false;
            }
            CalculateFreeSpace();
            menus.Picture.Invalidate();
        }
        private void textView_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void xCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            menuText.X = (int)xCoord.Value;
            menus.SetTextObjects();
            menus.Picture.Invalidate();
        }
    }
}
