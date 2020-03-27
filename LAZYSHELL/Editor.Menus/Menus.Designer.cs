
namespace LAZYSHELL
{
    partial class Menus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.menuName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openPalettesBG = new System.Windows.Forms.ToolStripMenuItem();
            this.openPalettesFG = new System.Windows.Forms.ToolStripMenuItem();
            this.openPaletteSpeakers = new System.Windows.Forms.ToolStripMenuItem();
            this.openPaletteCursors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openGraphicsBG = new System.Windows.Forms.ToolStripMenuItem();
            this.openGraphicsFG = new System.Windows.Forms.ToolStripMenuItem();
            this.openGraphicsSpeakers = new System.Windows.Forms.ToolStripMenuItem();
            this.openGraphicsCursors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.importBGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.labelMusic = new System.Windows.Forms.ToolStripLabel();
            this.music = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxFG = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxBG = new System.Windows.Forms.PictureBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cursorName = new System.Windows.Forms.ToolStripComboBox();
            this.cursorSpriteNum = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cursorSequence = new LAZYSHELL.ToolStripNumericUpDown();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFG)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.menuName,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton2,
            this.toolStripSeparator2,
            this.labelMusic,
            this.music});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(780, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel1.Text = " MENU ";
            // 
            // menuName
            // 
            this.menuName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuName.DropDownWidth = 200;
            this.menuName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.menuName.Items.AddRange(new object[] {
            "Game Select",
            "Overworld Menu - Main",
            "Overworld Menu - Item",
            "Overworld Menu - Status",
            "Overworld Menu - Special",
            "Overworld Menu - Equip",
            "Overworld Menu - Special Item",
            "Overworld Menu - Switch",
            "Shop Menu",
            "Shop Menu - Buy",
            "Shop Menu - Sell Items",
            "Shop Menu - Sell Weapons"});
            this.menuName.Name = "menuName";
            this.menuName.Size = new System.Drawing.Size(200, 25);
            this.menuName.SelectedIndexChanged += new System.EventHandler(this.menuName_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPalettesBG,
            this.openPalettesFG,
            this.openPaletteSpeakers,
            this.openPaletteCursors});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(31, 22);
            // 
            // openPalettesBG
            // 
            this.openPalettesBG.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPalettesBG.Name = "openPalettesBG";
            this.openPalettesBG.Size = new System.Drawing.Size(176, 24);
            this.openPalettesBG.Text = "Background Palette";
            this.openPalettesBG.Click += new System.EventHandler(this.openPalettesBG_Click);
            // 
            // openPalettesFG
            // 
            this.openPalettesFG.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPalettesFG.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPalettesFG.Name = "openPalettesFG";
            this.openPalettesFG.Size = new System.Drawing.Size(176, 24);
            this.openPalettesFG.Text = "Foreground Palette";
            this.openPalettesFG.Click += new System.EventHandler(this.openPalettesFG_Click);
            // 
            // openPaletteSpeakers
            // 
            this.openPaletteSpeakers.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPaletteSpeakers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteSpeakers.Name = "openPaletteSpeakers";
            this.openPaletteSpeakers.Size = new System.Drawing.Size(176, 24);
            this.openPaletteSpeakers.Text = "Mono/Stereo Palette";
            this.openPaletteSpeakers.Click += new System.EventHandler(this.openPaletteSpeakers_Click);
            // 
            // openPaletteCursors
            // 
            this.openPaletteCursors.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPaletteCursors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteCursors.Name = "openPaletteCursors";
            this.openPaletteCursors.Size = new System.Drawing.Size(176, 24);
            this.openPaletteCursors.Text = "Cursors Palette";
            this.openPaletteCursors.Click += new System.EventHandler(this.openPaletteCursors_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGraphicsBG,
            this.openGraphicsFG,
            this.openGraphicsSpeakers,
            this.openGraphicsCursors});
            this.toolStripDropDownButton3.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.toolStripDropDownButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(31, 22);
            // 
            // openGraphicsBG
            // 
            this.openGraphicsBG.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphicsBG.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicsBG.Name = "openGraphicsBG";
            this.openGraphicsBG.Size = new System.Drawing.Size(183, 24);
            this.openGraphicsBG.Text = "Background Graphics";
            this.openGraphicsBG.Click += new System.EventHandler(this.openGraphicsBG_Click);
            // 
            // openGraphicsFG
            // 
            this.openGraphicsFG.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphicsFG.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicsFG.Name = "openGraphicsFG";
            this.openGraphicsFG.Size = new System.Drawing.Size(183, 24);
            this.openGraphicsFG.Text = "Foreground Graphics";
            this.openGraphicsFG.Click += new System.EventHandler(this.openGraphicsFG_Click);
            // 
            // openGraphicsSpeakers
            // 
            this.openGraphicsSpeakers.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphicsSpeakers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicsSpeakers.Name = "openGraphicsSpeakers";
            this.openGraphicsSpeakers.Size = new System.Drawing.Size(183, 24);
            this.openGraphicsSpeakers.Text = "Mono/Stereo Graphics";
            this.openGraphicsSpeakers.Click += new System.EventHandler(this.openGraphicsSpeakers_Click);
            // 
            // openGraphicsCursors
            // 
            this.openGraphicsCursors.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphicsCursors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicsCursors.Name = "openGraphicsCursors";
            this.openGraphicsCursors.Size = new System.Drawing.Size(183, 24);
            this.openGraphicsCursors.Text = "Cursor Graphics";
            this.openGraphicsCursors.Click += new System.EventHandler(this.openGraphicsCursors_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importBGToolStripMenuItem,
            this.importFGToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(27, 22);
            // 
            // importBGToolStripMenuItem
            // 
            this.importBGToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.importBGToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importBGToolStripMenuItem.Name = "importBGToolStripMenuItem";
            this.importBGToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.importBGToolStripMenuItem.Text = "Import Background";
            this.importBGToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // importFGToolStripMenuItem
            // 
            this.importFGToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.importFGToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importFGToolStripMenuItem.Name = "importFGToolStripMenuItem";
            this.importFGToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.importFGToolStripMenuItem.Text = "Import Foreground";
            this.importFGToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // labelMusic
            // 
            this.labelMusic.Name = "labelMusic";
            this.labelMusic.Size = new System.Drawing.Size(45, 22);
            this.labelMusic.Text = " MUSIC ";
            // 
            // music
            // 
            this.music.DropDownHeight = 400;
            this.music.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.music.DropDownWidth = 300;
            this.music.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.music.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.music.IntegralHeight = false;
            this.music.Name = "music";
            this.music.Size = new System.Drawing.Size(214, 25);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageAsToolStripMenuItem,
            this.importImageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 48);
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.saveImageAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveImageAsToolStripMenuItem.Text = "Save image as...";
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // importImageToolStripMenuItem
            // 
            this.importImageToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.importImageToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importImageToolStripMenuItem.Name = "importImageToolStripMenuItem";
            this.importImageToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.importImageToolStripMenuItem.Text = "Import image...";
            this.importImageToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(780, 260);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pictureBoxPreview);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(520, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 260);
            this.panel4.TabIndex = 2;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(256, 224);
            this.pictureBoxPreview.TabIndex = 3;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPreview_Paint);
            this.pictureBoxPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseDown);
            this.pictureBoxPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPreview_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBoxFG);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(260, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 260);
            this.panel2.TabIndex = 1;
            // 
            // pictureBoxFG
            // 
            this.pictureBoxFG.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxFG.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBoxFG.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFG.Name = "pictureBoxFG";
            this.pictureBoxFG.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxFG.TabIndex = 560;
            this.pictureBoxFG.TabStop = false;
            this.pictureBoxFG.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFG_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBoxBG);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 260);
            this.panel1.TabIndex = 0;
            // 
            // pictureBoxBG
            // 
            this.pictureBoxBG.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxBG.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBoxBG.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBG.Name = "pictureBoxBG";
            this.pictureBoxBG.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxBG.TabIndex = 559;
            this.pictureBoxBG.TabStop = false;
            this.pictureBoxBG.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBG_Paint);
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.cursorName,
            this.cursorSpriteNum,
            this.toolStripLabel3,
            this.cursorSequence});
            this.toolStrip2.Location = new System.Drawing.Point(0, 285);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(780, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(94, 22);
            this.toolStripLabel2.Text = " SPRITE CURSOR ";
            // 
            // cursorName
            // 
            this.cursorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cursorName.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cursorName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorName.IntegralHeight = false;
            this.cursorName.Items.AddRange(new object[] {
            "Entering menu",
            "Idol in menu",
            "Leaving menu",
            "Enter name",
            "Saved to slot"});
            this.cursorName.Name = "cursorName";
            this.cursorName.Size = new System.Drawing.Size(130, 25);
            this.cursorName.SelectedIndexChanged += new System.EventHandler(this.cursorName_SelectedIndexChanged);
            // 
            // cursorSpriteNum
            // 
            this.cursorSpriteNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cursorSpriteNum.DropDownWidth = 300;
            this.cursorSpriteNum.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cursorSpriteNum.Name = "cursorSpriteNum";
            this.cursorSpriteNum.Size = new System.Drawing.Size(300, 25);
            this.cursorSpriteNum.SelectedIndexChanged += new System.EventHandler(this.cursorSpriteNum_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel3.Text = " SEQUENCE ";
            // 
            // cursorSequence
            // 
            this.cursorSequence.AutoSize = false;
            this.cursorSequence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorSequence.Hexadecimal = false;
            this.cursorSequence.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cursorSequence.Location = new System.Drawing.Point(599, 1);
            this.cursorSequence.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.cursorSequence.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cursorSequence.Name = "cursorSequence";
            this.cursorSequence.Size = new System.Drawing.Size(50, 22);
            this.cursorSequence.Text = "0";
            this.cursorSequence.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cursorSequence.ValueChanged += new System.EventHandler(this.cursorSequence_ValueChanged);
            // 
            // Menus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 310);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Menus";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFG)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBG)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openPalettesBG;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem openGraphicsBG;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem importBGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPaletteCursors;
        private System.Windows.Forms.ToolStripMenuItem openGraphicsCursors;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxBG;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBoxFG;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox menuName;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cursorName;
        private System.Windows.Forms.ToolStripComboBox cursorSpriteNum;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private ToolStripNumericUpDown cursorSequence;
        private System.Windows.Forms.ToolStripMenuItem openPalettesFG;
        private System.Windows.Forms.ToolStripMenuItem openPaletteSpeakers;
        private System.Windows.Forms.ToolStripMenuItem openGraphicsFG;
        private System.Windows.Forms.ToolStripMenuItem openGraphicsSpeakers;
        private System.Windows.Forms.ToolStripMenuItem importFGToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox music;
        private System.Windows.Forms.ToolStripLabel labelMusic;
    }
}