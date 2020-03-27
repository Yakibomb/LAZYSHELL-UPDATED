
namespace LAZYSHELL
{
    partial class NPCPackets
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
            this.spritePictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.packetName = new System.Windows.Forms.ToolStripComboBox();
            this.packetNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.spriteName = new System.Windows.Forms.ToolStripComboBox();
            this.spriteNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.editSprite = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.actionButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.byte4 = new System.Windows.Forms.NumericUpDown();
            this.byte1c = new System.Windows.Forms.NumericUpDown();
            this.byte1a = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.byte1b = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.byte0 = new System.Windows.Forms.NumericUpDown();
            this.action = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unknownBits = new System.Windows.Forms.CheckedListBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.showShadow = new System.Windows.Forms.CheckBox();
            this.byte2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spritePictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.byte4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1c)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.action)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.byte2)).BeginInit();
            this.SuspendLayout();
            // 
            // spritePictureBox
            // 
            this.spritePictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.spritePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spritePictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.spritePictureBox.Location = new System.Drawing.Point(0, 25);
            this.spritePictureBox.Name = "spritePictureBox";
            this.spritePictureBox.Size = new System.Drawing.Size(260, 224);
            this.spritePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.spritePictureBox.TabIndex = 453;
            this.spritePictureBox.TabStop = false;
            this.spritePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spritePictureBox_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.packetName,
            this.packetNum,
            this.reset});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(260, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // packetName
            // 
            this.packetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.packetName.DropDownWidth = 250;
            this.packetName.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.packetName.Name = "packetName";
            this.packetName.Size = new System.Drawing.Size(152, 25);
            this.packetName.SelectedIndexChanged += new System.EventHandler(this.packetName_SelectedIndexChanged);
            // 
            // packetNum
            // 
            this.packetNum.AutoSize = false;
            this.packetNum.ContextMenuStrip = null;
            this.packetNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packetNum.Hexadecimal = false;
            this.packetNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.packetNum.Location = new System.Drawing.Point(184, 2);
            this.packetNum.Maximum = new decimal(new int[] {
            79,
            0,
            0,
            0});
            this.packetNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.packetNum.Name = "npcNum";
            this.packetNum.Size = new System.Drawing.Size(50, 21);
            this.packetNum.Text = "0";
            this.packetNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.packetNum.ValueChanged += new System.EventHandler(this.packetNum_ValueChanged);
            // 
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.reset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.Text = "Reset";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spriteName,
            this.spriteNum,
            this.editSprite});
            this.toolStrip2.Location = new System.Drawing.Point(0, 249);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(260, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // spriteName
            // 
            this.spriteName.AutoSize = false;
            this.spriteName.DropDownHeight = 300;
            this.spriteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spriteName.DropDownWidth = 300;
            this.spriteName.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.spriteName.IntegralHeight = false;
            this.spriteName.Name = "spriteName";
            this.spriteName.Size = new System.Drawing.Size(158, 21);
            this.spriteName.SelectedIndexChanged += new System.EventHandler(this.spriteName_SelectedIndexChanged);
            // 
            // spriteNum
            // 
            this.spriteNum.AutoSize = false;
            this.spriteNum.ContextMenuStrip = null;
            this.spriteNum.Hexadecimal = false;
            this.spriteNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spriteNum.Location = new System.Drawing.Point(167, 2);
            this.spriteNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spriteNum.Minimum = new decimal(new int[] {
            192,
            0,
            0,
            0});
            this.spriteNum.Name = "spriteNum";
            this.spriteNum.Size = new System.Drawing.Size(50, 21);
            this.spriteNum.Text = "192";
            this.spriteNum.Value = new decimal(new int[] {
            192,
            0,
            0,
            0});
            this.spriteNum.ValueChanged += new System.EventHandler(this.spriteNum_ValueChanged);
            // 
            // editSprite
            // 
            this.editSprite.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSprite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editSprite.Name = "editSprite";
            this.editSprite.Size = new System.Drawing.Size(40, 22);
            this.editSprite.Text = " EDIT ";
            this.editSprite.ToolTipText = "Edit sprite";
            this.editSprite.Click += new System.EventHandler(this.editSprite_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.actionButton);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.byte4);
            this.groupBox3.Controls.Add(this.byte1c);
            this.groupBox3.Controls.Add(this.byte1a);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.byte1b);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.byte0);
            this.groupBox3.Controls.Add(this.action);
            this.groupBox3.Location = new System.Drawing.Point(0, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 152);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Behavior Properties";
            // 
            // actionButton
            // 
            this.actionButton.BackColor = System.Drawing.SystemColors.Control;
            this.actionButton.FlatAppearance.BorderSize = 0;
            this.actionButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionButton.Location = new System.Drawing.Point(6, 20);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(65, 21);
            this.actionButton.TabIndex = 0;
            this.actionButton.Text = "Action #";
            this.actionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.actionButton.UseCompatibleTextRendering = true;
            this.actionButton.UseVisualStyleBackColor = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "B4,b4-7";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "B1,b5-7";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "B1,b3-4";
            // 
            // byte4
            // 
            this.byte4.Location = new System.Drawing.Point(73, 125);
            this.byte4.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.byte4.Name = "byte4";
            this.byte4.Size = new System.Drawing.Size(52, 21);
            this.byte4.TabIndex = 11;
            this.byte4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte4.ValueChanged += new System.EventHandler(this.byte4_ValueChanged);
            // 
            // byte1c
            // 
            this.byte1c.Location = new System.Drawing.Point(73, 104);
            this.byte1c.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.byte1c.Name = "byte1c";
            this.byte1c.Size = new System.Drawing.Size(52, 21);
            this.byte1c.TabIndex = 9;
            this.byte1c.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte1c.ValueChanged += new System.EventHandler(this.byte1c_ValueChanged);
            // 
            // byte1a
            // 
            this.byte1a.Location = new System.Drawing.Point(73, 62);
            this.byte1a.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.byte1a.Name = "byte1a";
            this.byte1a.Size = new System.Drawing.Size(52, 21);
            this.byte1a.TabIndex = 5;
            this.byte1a.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte1a.ValueChanged += new System.EventHandler(this.byte1a_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "B1,b0-2";
            // 
            // byte1b
            // 
            this.byte1b.Location = new System.Drawing.Point(73, 83);
            this.byte1b.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.byte1b.Name = "byte1b";
            this.byte1b.Size = new System.Drawing.Size(52, 21);
            this.byte1b.TabIndex = 7;
            this.byte1b.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte1b.ValueChanged += new System.EventHandler(this.byte1b_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "B0,b6-7";
            // 
            // byte0
            // 
            this.byte0.Location = new System.Drawing.Point(73, 41);
            this.byte0.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.byte0.Name = "byte0";
            this.byte0.Size = new System.Drawing.Size(52, 21);
            this.byte0.TabIndex = 3;
            this.byte0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte0.ValueChanged += new System.EventHandler(this.byte0_ValueChanged);
            // 
            // action
            // 
            this.action.Location = new System.Drawing.Point(73, 20);
            this.action.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(52, 21);
            this.action.TabIndex = 1;
            this.action.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.action.ValueChanged += new System.EventHandler(this.action_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.unknownBits);
            this.groupBox1.Location = new System.Drawing.Point(137, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 79);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unknown Bits";
            // 
            // unknownBits
            // 
            this.unknownBits.CheckOnClick = true;
            this.unknownBits.ColumnWidth = 50;
            this.unknownBits.FormattingEnabled = true;
            this.unknownBits.Items.AddRange(new object[] {
            "B2,b2",
            "B2,b3",
            "B2,b4"});
            this.unknownBits.Location = new System.Drawing.Point(6, 20);
            this.unknownBits.MultiColumn = true;
            this.unknownBits.Name = "unknownBits";
            this.unknownBits.Size = new System.Drawing.Size(111, 52);
            this.unknownBits.TabIndex = 0;
            this.unknownBits.SelectedIndexChanged += new System.EventHandler(this.unknownBits_SelectedIndexChanged);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(159, 406);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // showShadow
            // 
            this.showShadow.Appearance = System.Windows.Forms.Appearance.Button;
            this.showShadow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showShadow.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.showShadow.Location = new System.Drawing.Point(137, 358);
            this.showShadow.Name = "showShadow";
            this.showShadow.Size = new System.Drawing.Size(122, 21);
            this.showShadow.TabIndex = 454;
            this.showShadow.Text = "SHOW SHADOW";
            this.showShadow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.showShadow.UseVisualStyleBackColor = false;
            this.showShadow.CheckedChanged += new System.EventHandler(this.showShadow_CheckedChanged);
            // 
            // byte2
            // 
            this.byte2.Location = new System.Drawing.Point(207, 381);
            this.byte2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.byte2.Name = "byte2";
            this.byte2.Size = new System.Drawing.Size(52, 21);
            this.byte2.TabIndex = 9;
            this.byte2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.byte2.ValueChanged += new System.EventHandler(this.byte2_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "B2,b6-7";
            // 
            // NPCPackets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 430);
            this.Controls.Add(this.showShadow);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.byte2);
            this.Controls.Add(this.spritePictureBox);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.Name = "NPCPackets";
            this.Text = "NPC PACKETS - Lazy Shell";
            ((System.ComponentModel.ISupportInitialize)(this.spritePictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.byte4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1c)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte1b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.byte0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.action)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.byte2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.PictureBox spritePictureBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripNumericUpDown packetNum;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox spriteName;
        private ToolStripNumericUpDown spriteNum;
        private System.Windows.Forms.ToolStripButton editSprite;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown byte1b;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown byte0;
        private System.Windows.Forms.NumericUpDown action;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown byte4;
        private System.Windows.Forms.NumericUpDown byte1c;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox unknownBits;
        private System.Windows.Forms.ToolStripComboBox packetName;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton reset;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown byte1a;
        private System.Windows.Forms.CheckBox showShadow;
        private System.Windows.Forms.NumericUpDown byte2;
        private System.Windows.Forms.Label label6;
    }
}