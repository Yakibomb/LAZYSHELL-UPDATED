
namespace LAZYSHELL
{
    partial class MainTitle
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
            this.panel67 = new System.Windows.Forms.Panel();
            this.pictureBoxTitle = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openPalettes = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpritePalettes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openGraphics = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpriteGraphics = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel67.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel67
            // 
            this.panel67.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel67.Controls.Add(this.pictureBoxTitle);
            this.panel67.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel67.Location = new System.Drawing.Point(0, 21);
            this.panel67.Name = "panel67";
            this.panel67.Size = new System.Drawing.Size(260, 604);
            this.panel67.TabIndex = 1;
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.Size = new System.Drawing.Size(256, 600);
            this.pictureBoxTitle.TabIndex = 546;
            this.pictureBoxTitle.TabStop = false;
            this.pictureBoxTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTitle_Paint);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "  MAIN TITLE PREVIEW";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(538, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPalettes,
            this.openSpritePalettes});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(31, 22);
            // 
            // openPalettes
            // 
            this.openPalettes.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPalettes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPalettes.Name = "openPalettes";
            this.openPalettes.Size = new System.Drawing.Size(154, 24);
            this.openPalettes.Text = "Title Palettes";
            this.openPalettes.Click += new System.EventHandler(this.openPalettes_Click);
            // 
            // openSpritePalettes
            // 
            this.openSpritePalettes.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openSpritePalettes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSpritePalettes.Name = "openSpritePalettes";
            this.openSpritePalettes.Size = new System.Drawing.Size(154, 24);
            this.openSpritePalettes.Text = "Sprite Palettes";
            this.openSpritePalettes.Click += new System.EventHandler(this.openSpritePalettes_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGraphics,
            this.openSpriteGraphics});
            this.toolStripDropDownButton2.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(31, 22);
            // 
            // openGraphics
            // 
            this.openGraphics.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphics.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphics.Name = "openGraphics";
            this.openGraphics.Size = new System.Drawing.Size(154, 24);
            this.openGraphics.Text = "Title Graphics";
            this.openGraphics.Click += new System.EventHandler(this.openGraphics_Click);
            // 
            // openSpriteGraphics
            // 
            this.openSpriteGraphics.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openSpriteGraphics.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSpriteGraphics.Name = "openSpriteGraphics";
            this.openSpriteGraphics.Size = new System.Drawing.Size(154, 24);
            this.openSpriteGraphics.Text = "Sprite Graphics";
            this.openSpriteGraphics.Click += new System.EventHandler(this.openSpriteGraphics_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel67);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(278, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 625);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 625);
            this.panel2.TabIndex = 1;
            // 
            // MainTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 650);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainTitle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.panel67.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Panel panel67;
        private System.Windows.Forms.PictureBox pictureBoxTitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openPalettes;
        private System.Windows.Forms.ToolStripMenuItem openSpritePalettes;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem openGraphics;
        private System.Windows.Forms.ToolStripMenuItem openSpriteGraphics;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}