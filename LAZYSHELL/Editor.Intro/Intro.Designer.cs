
namespace LAZYSHELL
{
    partial class Intro
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTitleL1L2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilesetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTitleTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPalettesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTitlePalettesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spritePalettesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetMainTitleCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetOpeningIntroCardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 660);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.helpTips,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // save
            // 
            this.save.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.ToolTipText = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // helpTips
            // 
            this.helpTips.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpTips.CheckOnClick = true;
            this.helpTips.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.Text = "Help Tips";
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
            this.resetGraphicsToolStripMenuItem,
            this.resetTilesetsToolStripMenuItem,
            this.resetPalettesToolStripMenuItem,
            this.resetMainTitleCoordinatesToolStripMenuItem,
            this.resetOpeningIntroCardsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // resetGraphicsToolStripMenuItem
            // 
            this.resetGraphicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainTitleL1L2ToolStripMenuItem,
            this.spriteGraphicsToolStripMenuItem});
            this.resetGraphicsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetGraphicsToolStripMenuItem.Name = "resetGraphicsToolStripMenuItem";
            this.resetGraphicsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetGraphicsToolStripMenuItem.Text = "Reset Graphics";
            // 
            // mainTitleL1L2ToolStripMenuItem
            // 
            this.mainTitleL1L2ToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.mainTitleL1L2ToolStripMenuItem.Name = "mainTitleL1L2ToolStripMenuItem";
            this.mainTitleL1L2ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.mainTitleL1L2ToolStripMenuItem.Text = "Main Title Graphics";
            this.mainTitleL1L2ToolStripMenuItem.Click += new System.EventHandler(this.mainTitleL1L2ToolStripMenuItem_Click);
            // 
            // spriteGraphicsToolStripMenuItem
            // 
            this.spriteGraphicsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.spriteGraphicsToolStripMenuItem.Name = "spriteGraphicsToolStripMenuItem";
            this.spriteGraphicsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spriteGraphicsToolStripMenuItem.Text = "Sprite Graphics";
            this.spriteGraphicsToolStripMenuItem.Click += new System.EventHandler(this.spriteGraphicsToolStripMenuItem_Click);
            // 
            // resetTilesetsToolStripMenuItem
            // 
            this.resetTilesetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainTitleTilesetToolStripMenuItem});
            this.resetTilesetsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetTilesetsToolStripMenuItem.Name = "resetTilesetsToolStripMenuItem";
            this.resetTilesetsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetTilesetsToolStripMenuItem.Text = "Reset Tilesets";
            // 
            // mainTitleTilesetToolStripMenuItem
            // 
            this.mainTitleTilesetToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.mainTitleTilesetToolStripMenuItem.Name = "mainTitleTilesetToolStripMenuItem";
            this.mainTitleTilesetToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.mainTitleTilesetToolStripMenuItem.Text = "Main Title Tileset";
            this.mainTitleTilesetToolStripMenuItem.Click += new System.EventHandler(this.mainTitleTilesetToolStripMenuItem_Click);
            // 
            // resetPalettesToolStripMenuItem
            // 
            this.resetPalettesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainTitlePalettesToolStripMenuItem,
            this.spritePalettesToolStripMenuItem});
            this.resetPalettesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetPalettesToolStripMenuItem.Name = "resetPalettesToolStripMenuItem";
            this.resetPalettesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetPalettesToolStripMenuItem.Text = "Reset Palettes";
            // 
            // mainTitlePalettesToolStripMenuItem
            // 
            this.mainTitlePalettesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.mainTitlePalettesToolStripMenuItem.Name = "mainTitlePalettesToolStripMenuItem";
            this.mainTitlePalettesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.mainTitlePalettesToolStripMenuItem.Text = "Main Title Palettes";
            this.mainTitlePalettesToolStripMenuItem.Click += new System.EventHandler(this.mainTitlePalettesToolStripMenuItem_Click);
            // 
            // spritePalettesToolStripMenuItem
            // 
            this.spritePalettesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.spritePalettesToolStripMenuItem.Name = "spritePalettesToolStripMenuItem";
            this.spritePalettesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.spritePalettesToolStripMenuItem.Text = "Sprite Palettes";
            this.spritePalettesToolStripMenuItem.Click += new System.EventHandler(this.spritePalettesToolStripMenuItem_Click);
            // 
            // resetMainTitleCoordinatesToolStripMenuItem
            // 
            this.resetMainTitleCoordinatesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetMainTitleCoordinatesToolStripMenuItem.Name = "resetMainTitleCoordinatesToolStripMenuItem";
            this.resetMainTitleCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetMainTitleCoordinatesToolStripMenuItem.Text = "Reset Main Title Coordinates";
            this.resetMainTitleCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.resetMainTitleCoordinatesToolStripMenuItem_Click);
            // 
            // resetOpeningIntroCardsToolStripMenuItem
            // 
            this.resetOpeningIntroCardsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetOpeningIntroCardsToolStripMenuItem.Name = "resetOpeningIntroCardsToolStripMenuItem";
            this.resetOpeningIntroCardsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetOpeningIntroCardsToolStripMenuItem.Text = "Reset Intro Title Cards";
            this.resetOpeningIntroCardsToolStripMenuItem.Click += new System.EventHandler(this.resetOpeningIntroCardsToolStripMenuItem_Click);
            // 
            // Intro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 685);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.mainMainTitle_ico;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Intro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "INTRO - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Intro_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem resetGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainTitleL1L2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spriteGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetTilesetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainTitleTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPalettesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainTitlePalettesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spritePalettesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetMainTitleCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetOpeningIntroCardsToolStripMenuItem;
    }
}