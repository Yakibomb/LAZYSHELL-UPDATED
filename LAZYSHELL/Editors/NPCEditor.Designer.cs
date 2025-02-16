
namespace LAZYSHELL
{
    partial class NPCEditor
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
            this.unknownBits = new System.Windows.Forms.CheckedListBox();
            this.layerPriority = new System.Windows.Forms.CheckedListBox();
            this.yPixelShift = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.axisAcute = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.axisObtuse = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.shadow = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.searchResults = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.npcNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchSpriteNames = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.spriteName = new System.Windows.Forms.ToolStripComboBox();
            this.spriteNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.editSprite = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.vramStore = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cannotClone = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.vramSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showShadow = new System.Windows.Forms.CheckBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.searchSpriteName = new System.Windows.Forms.ToolStripComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spritePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPixelShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisAcute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisObtuse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vramSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.toolStrip3.SuspendLayout();
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
            this.spritePictureBox.TabIndex = 451;
            this.spritePictureBox.TabStop = false;
            this.spritePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spritePictureBox_Paint);
            // 
            // unknownBits
            // 
            this.unknownBits.CheckOnClick = true;
            this.unknownBits.ColumnWidth = 50;
            this.unknownBits.FormattingEnabled = true;
            this.unknownBits.Items.AddRange(new object[] {
            "B2,b0",
            "B2,b1",
            "B2,b2",
            "B2,b3",
            "B2,b4",
            "B5,b6",
            "B5,b7",
            "B6,b2"});
            this.unknownBits.Location = new System.Drawing.Point(6, 20);
            this.unknownBits.MultiColumn = true;
            this.unknownBits.Name = "unknownBits";
            this.unknownBits.Size = new System.Drawing.Size(108, 68);
            this.unknownBits.TabIndex = 0;
            this.unknownBits.SelectedIndexChanged += new System.EventHandler(this.unknownBits_SelectedIndexChanged);
            // 
            // layerPriority
            // 
            this.layerPriority.CheckOnClick = true;
            this.layerPriority.FormattingEnabled = true;
            this.layerPriority.Items.AddRange(new object[] {
            "priority 0 tiles",
            "priority 1 tiles",
            "priority 2 tiles"});
            this.layerPriority.Location = new System.Drawing.Point(6, 20);
            this.layerPriority.Name = "layerPriority";
            this.layerPriority.Size = new System.Drawing.Size(122, 52);
            this.layerPriority.TabIndex = 0;
            this.layerPriority.SelectedIndexChanged += new System.EventHandler(this.layerPriority_SelectedIndexChanged);
            // 
            // yPixelShift
            // 
            this.yPixelShift.Location = new System.Drawing.Point(73, 117);
            this.yPixelShift.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.yPixelShift.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            -2147483648});
            this.yPixelShift.Name = "yPixelShift";
            this.yPixelShift.Size = new System.Drawing.Size(56, 21);
            this.yPixelShift.TabIndex = 5;
            this.yPixelShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yPixelShift.ValueChanged += new System.EventHandler(this.yPixelShift_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y pixel shift";
            // 
            // axisAcute
            // 
            this.axisAcute.Location = new System.Drawing.Point(76, 20);
            this.axisAcute.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.axisAcute.Name = "axisAcute";
            this.axisAcute.Size = new System.Drawing.Size(53, 21);
            this.axisAcute.TabIndex = 1;
            this.axisAcute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.axisAcute.ValueChanged += new System.EventHandler(this.axisAcute_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Acute axis";
            // 
            // axisObtuse
            // 
            this.axisObtuse.Location = new System.Drawing.Point(76, 41);
            this.axisObtuse.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.axisObtuse.Name = "axisObtuse";
            this.axisObtuse.Size = new System.Drawing.Size(53, 21);
            this.axisObtuse.TabIndex = 3;
            this.axisObtuse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.axisObtuse.ValueChanged += new System.EventHandler(this.axisObtuse_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Obtuse axis";
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(76, 62);
            this.height.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(53, 21);
            this.height.TabIndex = 5;
            this.height.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.height.ValueChanged += new System.EventHandler(this.height_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Height";
            // 
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Location = new System.Drawing.Point(19, 522);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "Apply";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.Location = new System.Drawing.Point(181, 522);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // shadow
            // 
            this.shadow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shadow.DropDownWidth = 70;
            this.shadow.IntegralHeight = false;
            this.shadow.Items.AddRange(new object[] {
            "oval (small)",
            "oval (med)",
            "oval (big)",
            "block"});
            this.shadow.Location = new System.Drawing.Point(73, 96);
            this.shadow.Name = "shadow";
            this.shadow.Size = new System.Drawing.Size(56, 21);
            this.shadow.TabIndex = 3;
            this.shadow.SelectedIndexChanged += new System.EventHandler(this.shadow_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Shadow";
            // 
            // searchResults
            // 
            this.searchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResults.FormattingEnabled = true;
            this.searchResults.IntegralHeight = false;
            this.searchResults.Location = new System.Drawing.Point(2, 86);
            this.searchResults.Name = "searchResults";
            this.searchResults.Size = new System.Drawing.Size(228, 465);
            this.searchResults.TabIndex = 2;
            this.searchResults.SelectedIndexChanged += new System.EventHandler(this.searchResults_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.npcNum});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(260, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel1.Text = " NPC # ";
            // 
            // npcNum
            // 
            this.npcNum.AutoSize = false;
            this.npcNum.ContextMenuStrip = null;
            this.npcNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcNum.Hexadecimal = false;
            this.npcNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.npcNum.Location = new System.Drawing.Point(49, 2);
            this.npcNum.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.npcNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.npcNum.Name = "npcNum";
            this.npcNum.Size = new System.Drawing.Size(70, 21);
            this.npcNum.Text = "0";
            this.npcNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.npcNum.ValueChanged += new System.EventHandler(this.npcNum_ValueChanged);
            // 
            // searchBox
            // 
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(200, 21);
            // 
            // searchSpriteNames
            // 
            this.searchSpriteNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchSpriteNames.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchSpriteNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchSpriteNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchSpriteNames.Name = "searchSpriteNames";
            this.searchSpriteNames.Size = new System.Drawing.Size(23, 17);
            this.searchSpriteNames.Text = "Find sprite";
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
            1023,
            0,
            0,
            0});
            this.spriteNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spriteNum.Name = "spriteNum";
            this.spriteNum.Size = new System.Drawing.Size(50, 21);
            this.spriteNum.Text = "0";
            this.spriteNum.Value = new decimal(new int[] {
            0,
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
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.buttonReset);
            this.panel2.Controls.Add(this.buttonOK);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Controls.Add(this.spritePictureBox);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 551);
            this.panel2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.unknownBits);
            this.groupBox4.Location = new System.Drawing.Point(140, 379);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 137);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Unknown";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.vramStore);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cannotClone);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.vramSize);
            this.groupBox3.Location = new System.Drawing.Point(140, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 96);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VRAM Buffer";
            // 
            // vramStore
            // 
            this.vramStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vramStore.DropDownWidth = 100;
            this.vramStore.IntegralHeight = false;
            this.vramStore.Items.AddRange(new object[] {
            "SW/SE, NW/NE",
            "SW/SE, NW/NE, S",
            "SW/SE",
            "SW/SE, NW/NE",
            "all directions",
            "____",
            "____",
            "all directions"});
            this.vramStore.Location = new System.Drawing.Point(45, 21);
            this.vramStore.Name = "vramStore";
            this.vramStore.Size = new System.Drawing.Size(69, 21);
            this.vramStore.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Store";
            // 
            // cannotClone
            // 
            this.cannotClone.Appearance = System.Windows.Forms.Appearance.Button;
            this.cannotClone.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cannotClone.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.cannotClone.Location = new System.Drawing.Point(5, 68);
            this.cannotClone.Name = "cannotClone";
            this.cannotClone.Size = new System.Drawing.Size(109, 21);
            this.cannotClone.TabIndex = 4;
            this.cannotClone.Text = "CANNOT CLONE";
            this.cannotClone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cannotClone.UseVisualStyleBackColor = false;
            this.cannotClone.CheckedChanged += new System.EventHandler(this.cannotClone_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Size";
            // 
            // vramSize
            // 
            this.vramSize.Location = new System.Drawing.Point(45, 42);
            this.vramSize.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.vramSize.Name = "vramSize";
            this.vramSize.Size = new System.Drawing.Size(69, 21);
            this.vramSize.TabIndex = 3;
            this.vramSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.axisObtuse);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.axisAcute);
            this.groupBox2.Controls.Add(this.height);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(0, 427);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 89);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solidity Field";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.shadow);
            this.groupBox1.Controls.Add(this.showShadow);
            this.groupBox1.Controls.Add(this.yPixelShift);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.layerPriority);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(0, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 144);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Priority";
            // 
            // showShadow
            // 
            this.showShadow.Appearance = System.Windows.Forms.Appearance.Button;
            this.showShadow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showShadow.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.showShadow.Location = new System.Drawing.Point(6, 73);
            this.showShadow.Name = "showShadow";
            this.showShadow.Size = new System.Drawing.Size(122, 21);
            this.showShadow.TabIndex = 1;
            this.showShadow.Text = "SHOW SHADOW";
            this.showShadow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.showShadow.UseVisualStyleBackColor = false;
            this.showShadow.CheckedChanged += new System.EventHandler(this.showShadow_CheckedChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.FlatAppearance.BorderSize = 0;
            this.buttonReset.Location = new System.Drawing.Point(100, 522);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.searchResults);
            this.panel6.Controls.Add(this.toolStrip3);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(260, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.panel6.Size = new System.Drawing.Size(230, 551);
            this.panel6.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchSpriteName,
            this.searchBox,
            this.searchSpriteNames});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip3.Location = new System.Drawing.Point(2, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(228, 61);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // searchSpriteName
            // 
            this.searchSpriteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchSpriteName.DropDownWidth = 400;
            this.searchSpriteName.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.searchSpriteName.Name = "searchSpriteName";
            this.searchSpriteName.Size = new System.Drawing.Size(200, 21);
            this.searchSpriteName.SelectedIndexChanged += new System.EventHandler(this.searchSpriteName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.label8.Size = new System.Drawing.Size(228, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "FIND NPCS CONTAINING SPRITE...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NPCEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 551);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NPCEditor";
            this.Text = "NPCS - LAZYSHELL++";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NPCEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spritePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPixelShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisAcute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisObtuse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vramSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.PictureBox spritePictureBox;
        private System.Windows.Forms.CheckedListBox unknownBits;
        private System.Windows.Forms.CheckedListBox layerPriority;
        private System.Windows.Forms.NumericUpDown yPixelShift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown axisAcute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown axisObtuse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ComboBox shadow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox searchResults;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private LAZYSHELL.ToolStripNumericUpDown npcNum;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox spriteName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripButton searchSpriteNames;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripComboBox searchSpriteName;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label8;
        private ToolStripNumericUpDown spriteNum;
        private System.Windows.Forms.CheckBox cannotClone;
        private System.Windows.Forms.CheckBox showShadow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown vramSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox vramStore;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton editSprite;
    }
}