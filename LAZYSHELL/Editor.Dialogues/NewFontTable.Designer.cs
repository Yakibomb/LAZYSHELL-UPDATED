
namespace LAZYSHELL
{
    partial class NewFontTable
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
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.fontFamily = new System.Windows.Forms.ToolStripComboBox();
            this.fontSize = new LAZYSHELL.ToolStripNumericUpDown();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.fontBold = new System.Windows.Forms.ToolStripButton();
            this.fontItalics = new System.Windows.Forms.ToolStripButton();
            this.fontUnderline = new System.Windows.Forms.ToolStripButton();
            this.autoSetWidths = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.characterHeight = new LAZYSHELL.ToolStripNumericUpDown();
            this.panel65 = new System.Windows.Forms.Panel();
            this.generateFontTableImage = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.padding = new LAZYSHELL.ToolStripNumericUpDown();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.shiftTableUp = new System.Windows.Forms.ToolStripButton();
            this.shiftTableDown = new System.Windows.Forms.ToolStripButton();
            this.shiftTableLeft = new System.Windows.Forms.ToolStripButton();
            this.shiftTableRight = new System.Windows.Forms.ToolStripButton();
            this.resetTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel71 = new System.Windows.Forms.Panel();
            this.fontTable = new System.Windows.Forms.Panel();
            this.toolStrip3.SuspendLayout();
            this.panel65.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel71.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip3
            // 
            this.toolStrip3.CanOverflow = false;
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontFamily,
            this.fontSize,
            this.toolStripSeparator3,
            this.fontBold,
            this.fontItalics,
            this.fontUnderline});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(318, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.TabStop = true;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // fontFamily
            // 
            this.fontFamily.DropDownHeight = 400;
            this.fontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontFamily.DropDownWidth = 250;
            this.fontFamily.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.fontFamily.IntegralHeight = false;
            this.fontFamily.Name = "fontFamily";
            this.fontFamily.Size = new System.Drawing.Size(180, 25);
            this.fontFamily.SelectedIndexChanged += new System.EventHandler(this.fontFamily_SelectedIndexChanged);
            // 
            // fontSize
            // 
            this.fontSize.Hexadecimal = false;
            this.fontSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fontSize.Location = new System.Drawing.Point(189, 3);
            this.fontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.fontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fontSize.Name = "fontSize";
            this.fontSize.Size = new System.Drawing.Size(39, 22);
            this.fontSize.Text = "8";
            this.fontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.fontSize.ValueChanged += new System.EventHandler(this.fontSize_ValueChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // fontBold
            // 
            this.fontBold.AutoSize = false;
            this.fontBold.CheckOnClick = true;
            this.fontBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fontBold.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontBold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fontBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fontBold.Name = "fontBold";
            this.fontBold.Size = new System.Drawing.Size(20, 20);
            this.fontBold.Text = "B";
            this.fontBold.ToolTipText = "Bold";
            this.fontBold.Click += new System.EventHandler(this.fontBold_Click);
            // 
            // fontItalics
            // 
            this.fontItalics.AutoSize = false;
            this.fontItalics.CheckOnClick = true;
            this.fontItalics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fontItalics.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontItalics.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fontItalics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fontItalics.Name = "fontItalics";
            this.fontItalics.Size = new System.Drawing.Size(20, 20);
            this.fontItalics.Text = "I";
            this.fontItalics.ToolTipText = "Italic";
            this.fontItalics.Click += new System.EventHandler(this.fontItalics_Click);
            // 
            // fontUnderline
            // 
            this.fontUnderline.AutoSize = false;
            this.fontUnderline.CheckOnClick = true;
            this.fontUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fontUnderline.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontUnderline.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fontUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fontUnderline.Name = "fontUnderline";
            this.fontUnderline.Size = new System.Drawing.Size(20, 20);
            this.fontUnderline.Text = "U";
            this.fontUnderline.ToolTipText = "Underline";
            this.fontUnderline.Click += new System.EventHandler(this.fontUnderline_Click);
            // 
            // autoSetWidths
            // 
            this.autoSetWidths.Checked = true;
            this.autoSetWidths.CheckOnClick = true;
            this.autoSetWidths.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSetWidths.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.autoSetWidths.Image = global::LAZYSHELL.Properties.Resources.crop;
            this.autoSetWidths.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.autoSetWidths.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoSetWidths.Name = "autoSetWidths";
            this.autoSetWidths.Size = new System.Drawing.Size(23, 22);
            this.autoSetWidths.ToolTipText = "Auto Crop Widths";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel1.Text = "Height ";
            // 
            // characterHeight
            // 
            this.characterHeight.AutoSize = false;
            this.characterHeight.Hexadecimal = false;
            this.characterHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.characterHeight.Location = new System.Drawing.Point(130, 3);
            this.characterHeight.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.characterHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.characterHeight.Name = "characterHeight";
            this.characterHeight.Size = new System.Drawing.Size(49, 22);
            this.characterHeight.Text = "12";
            this.characterHeight.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.characterHeight.ValueChanged += new System.EventHandler(this.characterHeight_ValueChanged);
            // 
            // panel65
            // 
            this.panel65.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel65.Controls.Add(this.generateFontTableImage);
            this.panel65.Controls.Add(this.toolStrip2);
            this.panel65.Controls.Add(this.toolStrip1);
            this.panel65.Controls.Add(this.panel71);
            this.panel65.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel65.Location = new System.Drawing.Point(0, 25);
            this.panel65.Name = "panel65";
            this.panel65.Size = new System.Drawing.Size(318, 200);
            this.panel65.TabIndex = 1;
            // 
            // generateFontTableImage
            // 
            this.generateFontTableImage.BackColor = System.Drawing.SystemColors.Control;
            this.generateFontTableImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.generateFontTableImage.FlatAppearance.BorderSize = 0;
            this.generateFontTableImage.Location = new System.Drawing.Point(132, 177);
            this.generateFontTableImage.Name = "generateFontTableImage";
            this.generateFontTableImage.Size = new System.Drawing.Size(186, 23);
            this.generateFontTableImage.TabIndex = 3;
            this.generateFontTableImage.Text = "Generate Image";
            this.generateFontTableImage.UseVisualStyleBackColor = false;
            this.generateFontTableImage.Click += new System.EventHandler(this.generateFontTableImage_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.padding,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.characterHeight});
            this.toolStrip2.Location = new System.Drawing.Point(132, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(186, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel2.Text = "Pad ";
            // 
            // padding
            // 
            this.padding.AutoSize = false;
            this.padding.Hexadecimal = false;
            this.padding.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.padding.Location = new System.Drawing.Point(34, 3);
            this.padding.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.padding.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.padding.Name = "padding";
            this.padding.Size = new System.Drawing.Size(49, 22);
            this.padding.Text = "1";
            this.padding.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shiftTableUp,
            this.shiftTableDown,
            this.shiftTableLeft,
            this.shiftTableRight,
            this.resetTable,
            this.toolStripSeparator1,
            this.autoSetWidths});
            this.toolStrip1.Location = new System.Drawing.Point(132, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(186, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // shiftTableUp
            // 
            this.shiftTableUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftTableUp.Image = global::LAZYSHELL.Properties.Resources.moveup;
            this.shiftTableUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.shiftTableUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftTableUp.Name = "shiftTableUp";
            this.shiftTableUp.Size = new System.Drawing.Size(23, 22);
            this.shiftTableUp.ToolTipText = "Shift Table Up";
            this.shiftTableUp.Click += new System.EventHandler(this.shiftTableUp_Click);
            // 
            // shiftTableDown
            // 
            this.shiftTableDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftTableDown.Image = global::LAZYSHELL.Properties.Resources.movedown;
            this.shiftTableDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.shiftTableDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftTableDown.Name = "shiftTableDown";
            this.shiftTableDown.Size = new System.Drawing.Size(23, 22);
            this.shiftTableDown.ToolTipText = "Shift Table Down";
            this.shiftTableDown.Click += new System.EventHandler(this.shiftTableDown_Click);
            // 
            // shiftTableLeft
            // 
            this.shiftTableLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftTableLeft.Image = global::LAZYSHELL.Properties.Resources.back;
            this.shiftTableLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.shiftTableLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftTableLeft.Name = "shiftTableLeft";
            this.shiftTableLeft.Size = new System.Drawing.Size(23, 22);
            this.shiftTableLeft.ToolTipText = "Shift Table Left";
            this.shiftTableLeft.Click += new System.EventHandler(this.shiftTableLeft_Click);
            // 
            // shiftTableRight
            // 
            this.shiftTableRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shiftTableRight.Image = global::LAZYSHELL.Properties.Resources.foward;
            this.shiftTableRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.shiftTableRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shiftTableRight.Name = "shiftTableRight";
            this.shiftTableRight.Size = new System.Drawing.Size(23, 22);
            this.shiftTableRight.ToolTipText = "Shift Table Right";
            this.shiftTableRight.Click += new System.EventHandler(this.shiftTableRight_Click);
            // 
            // resetTable
            // 
            this.resetTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resetTable.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetTable.Name = "resetTable";
            this.resetTable.Size = new System.Drawing.Size(23, 22);
            this.resetTable.ToolTipText = "Reset Table";
            this.resetTable.Click += new System.EventHandler(this.resetTable_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panel71
            // 
            this.panel71.BackColor = System.Drawing.SystemColors.Control;
            this.panel71.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel71.Controls.Add(this.fontTable);
            this.panel71.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel71.Location = new System.Drawing.Point(0, 0);
            this.panel71.Name = "panel71";
            this.panel71.Size = new System.Drawing.Size(132, 200);
            this.panel71.TabIndex = 1;
            // 
            // fontTable
            // 
            this.fontTable.Location = new System.Drawing.Point(0, 0);
            this.fontTable.Name = "fontTable";
            this.fontTable.Size = new System.Drawing.Size(128, 192);
            this.fontTable.TabIndex = 0;
            // 
            // NewFontTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 225);
            this.Controls.Add(this.panel65);
            this.Controls.Add(this.toolStrip3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewFontTable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "NEW FONT TABLE";
            this.TopMost = true;
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel65.ResumeLayout(false);
            this.panel65.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel71.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Panel panel65;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton fontBold;
        private System.Windows.Forms.ToolStripButton fontItalics;
        private System.Windows.Forms.ToolStripButton fontUnderline;
        private System.Windows.Forms.Button generateFontTableImage;
        private System.Windows.Forms.Panel panel71;
        private System.Windows.Forms.Panel fontTable;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton shiftTableUp;
        private System.Windows.Forms.ToolStripButton shiftTableDown;
        private System.Windows.Forms.ToolStripButton shiftTableLeft;
        private System.Windows.Forms.ToolStripButton shiftTableRight;
        private System.Windows.Forms.ToolStripButton resetTable;
        private System.Windows.Forms.ToolStripButton autoSetWidths;
        private LAZYSHELL.ToolStripNumericUpDown fontSize;
        private System.Windows.Forms.ToolStripComboBox fontFamily;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private LAZYSHELL.ToolStripNumericUpDown characterHeight;
        private LAZYSHELL.ToolStripNumericUpDown padding;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}