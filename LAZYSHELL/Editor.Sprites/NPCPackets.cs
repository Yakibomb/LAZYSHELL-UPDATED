using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class NPCPackets : NewForm
    {
                private NPCPacket[] npcPackets { get { return Model.NPCPackets; } set { Model.NPCPackets = value; } }
        private NPCPacket npcPacket { get { return npcPackets[index]; } set { npcPackets[index] = value; } }
        private int index { get { return (int)packetNum.Value; } set { packetNum.Value = value; } }
        private Bitmap spriteImage;
        private Sprite sprite { get { return Model.Sprites[(int)spriteNum.Value]; } }
        // constructor
        public NPCPackets()
        {
            InitializeComponent();
            packetName.Items.AddRange(Lists.Numerize(Lists.NPCPackets));
            spriteName.Items.AddRange(Lists.Numerize(192, 256, Lists.SpriteNames));
            this.packetName.SelectedIndex = 0;
            InitializePackets();
        }
        private void InitializePackets()
        {
            this.Updating = true;
            this.spriteName.SelectedIndex = npcPacket.Sprite;
            this.action.Value = npcPacket.Action;
            this.byte0.Value = npcPacket.B0;
            this.byte1a.Value = npcPacket.B1a;
            this.byte1b.Value = npcPacket.B1b;
            this.byte1c.Value = npcPacket.B1c;
            this.byte4.Value = npcPacket.B4;
            this.unknownBits.SetItemChecked(0, npcPacket.B2b2);
            this.unknownBits.SetItemChecked(1, npcPacket.B2b3);
            this.unknownBits.SetItemChecked(2, npcPacket.B2b4);
            this.showShadow.Checked = npcPacket.ShowShadow;
            this.byte2.Value = npcPacket.B2;
            SetSpriteImage();
            this.Updating = false;
        }
        private void SetSpriteImage()
        {
            Size size = new Size(0, 0);
            int[] spritePixels = sprite.GetPixels(false, true, 0, 3, false, true, ref size);
            if (spritePixels.Length == 0)
            {
                spritePixels = new int[2];
                size.Width = 1;
                size.Height = 1;
            }
            spriteImage = Do.PixelsToImage(spritePixels, size.Width, size.Height);
            spritePictureBox.Invalidate();
        }
        // event handlers
        private void packetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            packetNum.Value = packetName.SelectedIndex;
        }
        private void packetNum_ValueChanged(object sender, EventArgs e)
        {
            packetName.SelectedIndex = (int)packetNum.Value;
            InitializePackets();
        }
        private void save_Click(object sender, EventArgs e)
        {
            foreach (NPCPacket npcPacket in npcPackets)
                npcPacket.Assemble();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            this.npcPacket = new NPCPacket(index);
            InitializePackets();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void spritePictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (spriteImage != null)
                e.Graphics.DrawImage(spriteImage, 128 - (spriteImage.Width / 2), 128 - (spriteImage.Height / 2));
        }
        private void spriteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteNum.Value = spriteName.SelectedIndex + 192;
            if (this.Updating)
                return;
            npcPacket.Sprite = (byte)spriteName.SelectedIndex;
            SetSpriteImage();
        }
        private void spriteNum_ValueChanged(object sender, EventArgs e)
        {
            spriteName.SelectedIndex = (int)spriteNum.Value - 192;
        }
        private void editSprite_Click(object sender, EventArgs e)
        {
            if (Model.Program.Sprites == null || !Model.Program.Sprites.Visible)
                Model.Program.CreateSpritesWindow();
            //
            Model.Program.Sprites.Index = (int)spriteNum.Value;
            Model.Program.Sprites.BringToFront();
        }
        private void action_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.Action = (ushort)action.Value;
        }
        private void actionButton_Click(object sender, EventArgs e)
        {
            if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                Model.Program.CreateEventScriptsWindow();
            //
            Model.Program.EventScripts.EventName.SelectedIndex = 1;
            Model.Program.EventScripts.EventNum.Value = action.Value;
            Model.Program.EventScripts.BringToFront();
        }
        private void byte0_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B0 = (byte)byte0.Value;
        }
        private void byte1a_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B1a = (byte)byte1a.Value;
        }
        private void byte1b_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B1b = (byte)byte1b.Value;
        }
        private void byte1c_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B1c = (byte)byte1c.Value;
        }
        private void byte4_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B4 = (byte)byte4.Value;
        }
        private void unknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B2b2 = unknownBits.GetItemChecked(0);
            npcPacket.B2b3 = unknownBits.GetItemChecked(1);
            npcPacket.B2b4 = unknownBits.GetItemChecked(2);
            npcPacket.ShowShadow = unknownBits.GetItemChecked(3);
        }
        private void showShadow_CheckedChanged(object sender, EventArgs e)
        {
            showShadow.ForeColor = showShadow.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating)
                return;
            npcPacket.ShowShadow = showShadow.Checked;
        }
        private void byte2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            npcPacket.B2 = (byte)byte2.Value;
        }
    }
}
