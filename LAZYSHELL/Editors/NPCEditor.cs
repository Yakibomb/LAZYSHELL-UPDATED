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
    public partial class NPCEditor : NewForm
    {
        #region Variables
        private NPCProperties _npcProperty;
        private Settings settings = Settings.Default;
        private NPCProperties[] npcProperties { get { return Model.NPCProperties; } set { Model.NPCProperties = value; } }
        private NPCProperties npcProperty { get { return npcProperties[index]; } set { npcProperties[index] = value; } }
        private Bitmap spriteImage;
        private Sprite sprite { get { return Model.Sprites[(int)spriteName.SelectedIndex]; } }
        private int[] spritePixels;
        private Levels level;
        private int index { get { return (int)npcNum.Value; } set { npcNum.Value = value; } }
        private Search searchWindow;
                #endregion
        // constructor
        public NPCEditor(Levels level, decimal npcID)
        {
            this.level = level;
            InitializeComponent();
            spriteName.Items.AddRange(Lists.Numerize(Lists.SpriteNames));
            searchSpriteName.Items.AddRange(Lists.Numerize(Lists.SpriteNames));
            searchSpriteName.SelectedIndex = 0;
            npcNum.Value = npcID;
            InitializeNPCs();
            searchWindow = new Search(searchSpriteName, searchBox, searchSpriteNames, searchSpriteName.Items);
            this.History = new History(this, null, npcNum);
        }
        // functions
        public void Reload(decimal npcID)
        {
            npcNum.Value = Math.Min(511, npcID);
            InitializeNPCs();
        }
        private void InitializeNPCs()
        {
            this.Updating = true;
            this.spriteName.SelectedIndex = npcProperty.Sprite;
            this.layerPriority.SetItemChecked(0, npcProperty.Priority0);
            this.layerPriority.SetItemChecked(1, npcProperty.Priority1);
            this.layerPriority.SetItemChecked(2, npcProperty.Priority2);
            this.yPixelShift.Value = npcProperty.YPixelShiftUp + (npcProperty.Shift16pxDown ? -16 : 0);
            this.axisAcute.Value = npcProperty.AcuteAxis;
            this.axisObtuse.Value = npcProperty.ObtuseAxis;
            this.height.Value = npcProperty.Height;
            this.showShadow.Checked = npcProperty.ShowShadow;
            this.shadow.SelectedIndex = npcProperty.Shadow;
            this.cannotClone.Checked = npcProperty.ActiveVRAM;
            this.vramStore.SelectedIndex = npcProperty.Byte1a;
            this.vramSize.Value = npcProperty.Byte1b;
            this.unknownBits.SetItemChecked(0, npcProperty.B2b0);
            this.unknownBits.SetItemChecked(1, npcProperty.B2b1);
            this.unknownBits.SetItemChecked(2, npcProperty.B2b2);
            this.unknownBits.SetItemChecked(3, npcProperty.B2b3);
            this.unknownBits.SetItemChecked(4, npcProperty.B2b4);
            this.unknownBits.SetItemChecked(5, npcProperty.B5b6);
            this.unknownBits.SetItemChecked(6, npcProperty.B5b7);
            this.unknownBits.SetItemChecked(7, npcProperty.B6b2);
            SetSpriteImage();
            this.Updating = false;
            //if (level.npcs.NumberOfNPCs != 0)
            //    level.NPCID.Value = npcNum.Value;
        }
        private void LoadSearch()
        {
            searchResults.Items.Clear();
            bool notFound;
            int val = (int)searchSpriteName.SelectedIndex;
            for (int i = 0; i < npcProperties.Length; i++)
            {
                notFound = false;
                if (searchSpriteName.SelectedIndex != npcProperties[i].Sprite) notFound = true;
                if (!notFound)
                    searchResults.Items.Add("NPC #" + i.ToString() + "\n");
            }
        }
        private void SetSpriteImage()
        {
            Size size = new Size(0, 0);
            spritePixels = sprite.GetPixels(false, true, 0, 3, false, true, ref size);
            if (spritePixels.Length == 0)
            {
                spritePixels = new int[2];
                size.Width = 1;
                size.Height = 1;
            }
            spriteImage = Do.PixelsToImage(spritePixels, size.Width, size.Height);
            spritePictureBox.Invalidate();
        }
        #region Event handlers
        private void npcNum_ValueChanged(object sender, EventArgs e)
        {
            _npcProperty = npcProperty.Copy();
            InitializeNPCs();
        }
        private void spritePictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (spriteImage != null)
                e.Graphics.DrawImage(spriteImage, 128 - (spriteImage.Width / 2), 128 - (spriteImage.Height / 2));
        }
        private void spriteNum_ValueChanged(object sender, EventArgs e)
        {
            spriteName.SelectedIndex = (int)spriteNum.Value;
        }
        private void spriteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteNum.Value = spriteName.SelectedIndex;
            if (this.Updating)
                return;
            SetSpriteImage();
        }
        private void editSprite_Click(object sender, EventArgs e)
        {
            if (Model.Program.Sprites == null || !Model.Program.Sprites.Visible)
                Model.Program.CreateSpritesWindow();
            Model.Program.Sprites.Index = (int)spriteNum.Value;
            Model.Program.Sprites.BringToFront();
        }
        private void layerPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void showShadow_CheckedChanged(object sender, EventArgs e)
        {
            showShadow.ForeColor = showShadow.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
        }
        private void yPixelShift_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void axisAcute_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void axisObtuse_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void height_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void shadow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        private void cannotClone_CheckedChanged(object sender, EventArgs e)
        {
            cannotClone.ForeColor = cannotClone.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
        }
        private void unknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
        }
        //
        private void searchSpriteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSearch();
        }
        private void searchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchResults.SelectedItem == null)
                return;
            npcNum.Value = Convert.ToInt32(searchResults.SelectedItem.ToString().Substring(5));
        }
        //
        private void buttonReset_Click(object sender, EventArgs e)
        {
            npcProperty = _npcProperty.Copy();
            InitializeNPCs();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            npcProperty.Sprite = (ushort)spriteName.SelectedIndex;
            npcProperty.Priority0 = layerPriority.GetItemChecked(0);
            npcProperty.Priority1 = layerPriority.GetItemChecked(1);
            npcProperty.Priority2 = layerPriority.GetItemChecked(2);
            if (yPixelShift.Value >= 0)
            {
                npcProperty.YPixelShiftUp = (byte)yPixelShift.Value;
                npcProperty.Shift16pxDown = false;
            }
            else
            {
                npcProperty.YPixelShiftUp = (byte)(16 + yPixelShift.Value);
                npcProperty.Shift16pxDown = true;
            }
            npcProperty.AcuteAxis = (byte)axisAcute.Value;
            npcProperty.ObtuseAxis = (byte)axisObtuse.Value;
            npcProperty.Height = (byte)height.Value;
            npcProperty.ShowShadow = showShadow.Checked;
            npcProperty.Shadow = (byte)shadow.SelectedIndex;
            npcProperty.ActiveVRAM = cannotClone.Checked;
            npcProperty.Byte1a = (byte)vramStore.SelectedIndex;
            npcProperty.Byte1b = (byte)vramSize.Value;
            npcProperty.B2b0 = unknownBits.GetItemChecked(0);
            npcProperty.B2b1 = unknownBits.GetItemChecked(1);
            npcProperty.B2b2 = unknownBits.GetItemChecked(2);
            npcProperty.B2b3 = unknownBits.GetItemChecked(3);
            npcProperty.B2b4 = unknownBits.GetItemChecked(4);
            npcProperty.B5b6 = unknownBits.GetItemChecked(5);
            npcProperty.B5b7 = unknownBits.GetItemChecked(6);
            npcProperty.B6b2 = unknownBits.GetItemChecked(7);
            level.overlay.NPCImages = null;
            level.picture.Invalidate();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void NPCEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchWindow.Hide();
            this.Hide();
            e.Cancel = true;
        }
        #endregion
    }
}