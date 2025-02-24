
namespace LAZYSHELL
{
    public partial class Previewer
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
            this.emuPathLabel = new System.Windows.Forms.Label();
            this.eventListBox = new System.Windows.Forms.ListBox();
            this.launchButton = new System.Windows.Forms.Button();
            this.romLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectIndex = new System.Windows.Forms.NumericUpDown();
            this.cancelButton = new System.Windows.Forms.Button();
            this.emuPathTextBox = new System.Windows.Forms.TextBox();
            this.romPathTextBox = new System.Windows.Forms.TextBox();
            this.zsnesArgs = new System.Windows.Forms.TextBox();
            this.linkLabelZSNES = new System.Windows.Forms.LinkLabel();
            this.adjustX = new System.Windows.Forms.NumericUpDown();
            this.adjustY = new System.Windows.Forms.NumericUpDown();
            this.adjustZ = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.battleBG = new System.Windows.Forms.ComboBox();
            this.snes9xArgs = new System.Windows.Forms.TextBox();
            this.linkLabelSNES9X = new System.Windows.Forms.LinkLabel();
            this.alliesInParty = new System.Windows.Forms.CheckedListBox();
            this.level = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.maxOutStats = new System.Windows.Forms.CheckBox();
            this.allyWeapon = new System.Windows.Forms.ComboBox();
            this.label133 = new System.Windows.Forms.Label();
            this.allyAccessory = new System.Windows.Forms.ComboBox();
            this.allyArmor = new System.Windows.Forms.ComboBox();
            this.label135 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.allyName = new System.Windows.Forms.ComboBox();
            this.reset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.enableDebug = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.changeEmuButton = new System.Windows.Forms.Button();
            this.dynamicROMPath = new System.Windows.Forms.CheckBox();
            this.defaultSNES9X = new System.Windows.Forms.Button();
            this.defaultZSNES = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.selectIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.level)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // emuPathLabel
            // 
            this.emuPathLabel.AutoSize = true;
            this.emuPathLabel.Location = new System.Drawing.Point(6, 21);
            this.emuPathLabel.Name = "emuPathLabel";
            this.emuPathLabel.Size = new System.Drawing.Size(78, 13);
            this.emuPathLabel.TabIndex = 0;
            this.emuPathLabel.Text = "Emulator Path:";
            // 
            // eventListBox
            // 
            this.eventListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventListBox.FormattingEnabled = true;
            this.eventListBox.IntegralHeight = false;
            this.eventListBox.Location = new System.Drawing.Point(3, 17);
            this.eventListBox.Name = "eventListBox";
            this.eventListBox.Size = new System.Drawing.Size(412, 386);
            this.eventListBox.TabIndex = 0;
            this.eventListBox.SelectedIndexChanged += new System.EventHandler(this.eventListBox_SelectedIndexChanged);
            // 
            // launchButton
            // 
            this.launchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.launchButton.FlatAppearance.BorderSize = 0;
            this.launchButton.Location = new System.Drawing.Point(6, 381);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(81, 23);
            this.launchButton.TabIndex = 8;
            this.launchButton.Text = "Launch";
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // romLabel
            // 
            this.romLabel.AutoSize = true;
            this.romLabel.Location = new System.Drawing.Point(6, 42);
            this.romLabel.Name = "romLabel";
            this.romLabel.Size = new System.Drawing.Size(57, 13);
            this.romLabel.TabIndex = 3;
            this.romLabel.Text = "Rom Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Index";
            // 
            // selectIndex
            // 
            this.selectIndex.Location = new System.Drawing.Point(84, 12);
            this.selectIndex.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.selectIndex.Name = "selectIndex";
            this.selectIndex.Size = new System.Drawing.Size(84, 21);
            this.selectIndex.TabIndex = 3;
            this.selectIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.selectIndex.ValueChanged += new System.EventHandler(this.selectNumericUpDown_ValueChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(89, 381);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(78, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // emuPathTextBox
            // 
            this.emuPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emuPathTextBox.Location = new System.Drawing.Point(89, 18);
            this.emuPathTextBox.Name = "emuPathTextBox";
            this.emuPathTextBox.ReadOnly = true;
            this.emuPathTextBox.Size = new System.Drawing.Size(433, 21);
            this.emuPathTextBox.TabIndex = 1;
            // 
            // romPathTextBox
            // 
            this.romPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.romPathTextBox.Location = new System.Drawing.Point(89, 39);
            this.romPathTextBox.Name = "romPathTextBox";
            this.romPathTextBox.ReadOnly = true;
            this.romPathTextBox.Size = new System.Drawing.Size(433, 21);
            this.romPathTextBox.TabIndex = 4;
            // 
            // zsnesArgs
            // 
            this.zsnesArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zsnesArgs.Location = new System.Drawing.Point(89, 81);
            this.zsnesArgs.Name = "zsnesArgs";
            this.zsnesArgs.Size = new System.Drawing.Size(433, 21);
            this.zsnesArgs.TabIndex = 10;
            // 
            // linkLabelZSNES
            // 
            this.linkLabelZSNES.AutoSize = true;
            this.linkLabelZSNES.Location = new System.Drawing.Point(6, 84);
            this.linkLabelZSNES.Name = "linkLabelZSNES";
            this.linkLabelZSNES.Size = new System.Drawing.Size(77, 13);
            this.linkLabelZSNES.TabIndex = 9;
            this.linkLabelZSNES.TabStop = true;
            this.linkLabelZSNES.Tag = "";
            this.linkLabelZSNES.Text = "ZSNESW Args:";
            this.linkLabelZSNES.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelZSNES_LinkClicked);
            // 
            // adjustX
            // 
            this.adjustX.Location = new System.Drawing.Point(41, 20);
            this.adjustX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.adjustX.Name = "adjustX";
            this.adjustX.Size = new System.Drawing.Size(43, 21);
            this.adjustX.TabIndex = 1;
            this.adjustX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // adjustY
            // 
            this.adjustY.Location = new System.Drawing.Point(84, 20);
            this.adjustY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.adjustY.Name = "adjustY";
            this.adjustY.Size = new System.Drawing.Size(42, 21);
            this.adjustY.TabIndex = 2;
            this.adjustY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // adjustZ
            // 
            this.adjustZ.Location = new System.Drawing.Point(126, 20);
            this.adjustZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.adjustZ.Name = "adjustZ";
            this.adjustZ.Size = new System.Drawing.Size(42, 21);
            this.adjustZ.TabIndex = 4;
            this.adjustZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "X,Y,Z";
            // 
            // battleBG
            // 
            this.battleBG.DropDownHeight = 392;
            this.battleBG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.battleBG.DropDownWidth = 220;
            this.battleBG.FormattingEnabled = true;
            this.battleBG.IntegralHeight = false;
            this.battleBG.Location = new System.Drawing.Point(6, 20);
            this.battleBG.Name = "battleBG";
            this.battleBG.Size = new System.Drawing.Size(162, 21);
            this.battleBG.TabIndex = 0;
            this.battleBG.SelectedIndexChanged += new System.EventHandler(this.battleBGListBox_SelectedIndexChanged);
            // 
            // snes9xArgs
            // 
            this.snes9xArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.snes9xArgs.Location = new System.Drawing.Point(89, 60);
            this.snes9xArgs.Name = "snes9xArgs";
            this.snes9xArgs.Size = new System.Drawing.Size(433, 21);
            this.snes9xArgs.TabIndex = 7;
            // 
            // linkLabelSNES9X
            // 
            this.linkLabelSNES9X.AutoSize = true;
            this.linkLabelSNES9X.Location = new System.Drawing.Point(6, 63);
            this.linkLabelSNES9X.Name = "linkLabelSNES9X";
            this.linkLabelSNES9X.Size = new System.Drawing.Size(73, 13);
            this.linkLabelSNES9X.TabIndex = 6;
            this.linkLabelSNES9X.TabStop = true;
            this.linkLabelSNES9X.Tag = "";
            this.linkLabelSNES9X.Text = "SNES9X Args:";
            this.linkLabelSNES9X.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSNES9X_LinkClicked);
            // 
            // alliesInParty
            // 
            this.alliesInParty.CheckOnClick = true;
            this.alliesInParty.ColumnWidth = 78;
            this.alliesInParty.FormattingEnabled = true;
            this.alliesInParty.Items.AddRange(new object[] {
            "Mario",
            "Toadstool",
            "Bowser",
            "Geno",
            "Mallow"});
            this.alliesInParty.Location = new System.Drawing.Point(6, 20);
            this.alliesInParty.MultiColumn = true;
            this.alliesInParty.Name = "alliesInParty";
            this.alliesInParty.Size = new System.Drawing.Size(162, 52);
            this.alliesInParty.TabIndex = 0;
            this.alliesInParty.SelectedIndexChanged += new System.EventHandler(this.alliesInParty_SelectedIndexChanged);
            // 
            // level
            // 
            this.level.Location = new System.Drawing.Point(129, 20);
            this.level.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.level.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(39, 21);
            this.level.TabIndex = 2;
            this.level.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.level.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.level.ValueChanged += new System.EventHandler(this.level_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "LV";
            // 
            // maxOutStats
            // 
            this.maxOutStats.Appearance = System.Windows.Forms.Appearance.Button;
            this.maxOutStats.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxOutStats.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.maxOutStats.Location = new System.Drawing.Point(6, 113);
            this.maxOutStats.Name = "maxOutStats";
            this.maxOutStats.Size = new System.Drawing.Size(81, 23);
            this.maxOutStats.TabIndex = 9;
            this.maxOutStats.Text = "MAX STATS";
            this.maxOutStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.maxOutStats.UseVisualStyleBackColor = false;
            this.maxOutStats.CheckedChanged += new System.EventHandler(this.maxOutStats_CheckedChanged);
            // 
            // allyWeapon
            // 
            this.allyWeapon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.allyWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.allyWeapon.DropDownHeight = 317;
            this.allyWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allyWeapon.DropDownWidth = 150;
            this.allyWeapon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allyWeapon.IntegralHeight = false;
            this.allyWeapon.ItemHeight = 15;
            this.allyWeapon.Location = new System.Drawing.Point(67, 45);
            this.allyWeapon.Name = "allyWeapon";
            this.allyWeapon.Size = new System.Drawing.Size(101, 21);
            this.allyWeapon.TabIndex = 4;
            this.allyWeapon.Tag = "";
            this.allyWeapon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.allyWeapon.SelectedIndexChanged += new System.EventHandler(this.allyWeapon_SelectedIndexChanged);
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(5, 90);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(56, 13);
            this.label133.TabIndex = 7;
            this.label133.Text = "Accessory";
            // 
            // allyAccessory
            // 
            this.allyAccessory.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.allyAccessory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.allyAccessory.DropDownHeight = 317;
            this.allyAccessory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allyAccessory.DropDownWidth = 150;
            this.allyAccessory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allyAccessory.IntegralHeight = false;
            this.allyAccessory.ItemHeight = 15;
            this.allyAccessory.Location = new System.Drawing.Point(67, 87);
            this.allyAccessory.Name = "allyAccessory";
            this.allyAccessory.Size = new System.Drawing.Size(101, 21);
            this.allyAccessory.TabIndex = 8;
            this.allyAccessory.Tag = "";
            this.allyAccessory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.allyAccessory.SelectedIndexChanged += new System.EventHandler(this.allyAccessory_SelectedIndexChanged);
            // 
            // allyArmor
            // 
            this.allyArmor.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.allyArmor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.allyArmor.DropDownHeight = 317;
            this.allyArmor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allyArmor.DropDownWidth = 150;
            this.allyArmor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allyArmor.IntegralHeight = false;
            this.allyArmor.ItemHeight = 15;
            this.allyArmor.Location = new System.Drawing.Point(67, 66);
            this.allyArmor.Name = "allyArmor";
            this.allyArmor.Size = new System.Drawing.Size(101, 21);
            this.allyArmor.TabIndex = 6;
            this.allyArmor.Tag = "";
            this.allyArmor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.allyArmor.SelectedIndexChanged += new System.EventHandler(this.allyArmor_SelectedIndexChanged);
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(5, 48);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(47, 13);
            this.label135.TabIndex = 3;
            this.label135.Text = "Weapon";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(5, 69);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(36, 13);
            this.label134.TabIndex = 5;
            this.label134.Text = "Armor";
            // 
            // allyName
            // 
            this.allyName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.allyName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.allyName.DropDownHeight = 317;
            this.allyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allyName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allyName.IntegralHeight = false;
            this.allyName.ItemHeight = 15;
            this.allyName.Location = new System.Drawing.Point(6, 20);
            this.allyName.Name = "allyName";
            this.allyName.Size = new System.Drawing.Size(99, 21);
            this.allyName.TabIndex = 0;
            this.allyName.Tag = "";
            this.allyName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.allyName_DrawItem);
            this.allyName.SelectedIndexChanged += new System.EventHandler(this.allyName_SelectedIndexChanged);
            // 
            // reset
            // 
            this.reset.FlatAppearance.BorderSize = 0;
            this.reset.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reset.Location = new System.Drawing.Point(89, 113);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(78, 23);
            this.reset.TabIndex = 10;
            this.reset.Text = "RESET";
            this.reset.UseVisualStyleBackColor = false;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.adjustX);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.adjustY);
            this.groupBox1.Controls.Add(this.adjustZ);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mario\'s Coordinates";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.battleBG);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 47);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Battlefield";
            // 
            // enableDebug
            // 
            this.enableDebug.AutoSize = true;
            this.enableDebug.Location = new System.Drawing.Point(6, 358);
            this.enableDebug.Name = "enableDebug";
            this.enableDebug.Size = new System.Drawing.Size(151, 17);
            this.enableDebug.TabIndex = 10;
            this.enableDebug.Text = "Enable battle debug menu";
            this.enableDebug.UseVisualStyleBackColor = true;
            this.enableDebug.CheckedChanged += new System.EventHandler(this.enableDebug_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.alliesInParty);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 78);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Allies in Party";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.allyAccessory);
            this.groupBox4.Controls.Add(this.allyArmor);
            this.groupBox4.Controls.Add(this.reset);
            this.groupBox4.Controls.Add(this.allyWeapon);
            this.groupBox4.Controls.Add(this.allyName);
            this.groupBox4.Controls.Add(this.maxOutStats);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label133);
            this.groupBox4.Controls.Add(this.level);
            this.groupBox4.Controls.Add(this.label135);
            this.groupBox4.Controls.Add(this.label134);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 211);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(174, 143);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ally Status";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.changeEmuButton);
            this.groupBox5.Controls.Add(this.dynamicROMPath);
            this.groupBox5.Controls.Add(this.emuPathLabel);
            this.groupBox5.Controls.Add(this.defaultSNES9X);
            this.groupBox5.Controls.Add(this.zsnesArgs);
            this.groupBox5.Controls.Add(this.defaultZSNES);
            this.groupBox5.Controls.Add(this.linkLabelZSNES);
            this.groupBox5.Controls.Add(this.snes9xArgs);
            this.groupBox5.Controls.Add(this.romLabel);
            this.groupBox5.Controls.Add(this.romPathTextBox);
            this.groupBox5.Controls.Add(this.linkLabelSNES9X);
            this.groupBox5.Controls.Add(this.emuPathTextBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(592, 108);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Emulator Properties";
            // 
            // changeEmuButton
            // 
            this.changeEmuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeEmuButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeEmuButton.Location = new System.Drawing.Point(524, 18);
            this.changeEmuButton.Name = "changeEmuButton";
            this.changeEmuButton.Size = new System.Drawing.Size(62, 21);
            this.changeEmuButton.TabIndex = 2;
            this.changeEmuButton.Text = "...";
            this.changeEmuButton.UseCompatibleTextRendering = true;
            this.changeEmuButton.UseVisualStyleBackColor = false;
            this.changeEmuButton.Click += new System.EventHandler(this.changeEmuButton_Click);
            // 
            // dynamicROMPath
            // 
            this.dynamicROMPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dynamicROMPath.Appearance = System.Windows.Forms.Appearance.Button;
            this.dynamicROMPath.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynamicROMPath.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.dynamicROMPath.Location = new System.Drawing.Point(524, 39);
            this.dynamicROMPath.Name = "dynamicROMPath";
            this.dynamicROMPath.Size = new System.Drawing.Size(62, 21);
            this.dynamicROMPath.TabIndex = 5;
            this.dynamicROMPath.Text = "DYNAMIC";
            this.dynamicROMPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dynamicROMPath.UseCompatibleTextRendering = true;
            this.dynamicROMPath.UseVisualStyleBackColor = false;
            this.dynamicROMPath.CheckedChanged += new System.EventHandler(this.dynamicROMPath_CheckedChanged);
            // 
            // defaultSNES9X
            // 
            this.defaultSNES9X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultSNES9X.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultSNES9X.Location = new System.Drawing.Point(524, 60);
            this.defaultSNES9X.Name = "defaultSNES9X";
            this.defaultSNES9X.Size = new System.Drawing.Size(62, 21);
            this.defaultSNES9X.TabIndex = 8;
            this.defaultSNES9X.Text = "DEFAULT";
            this.defaultSNES9X.UseCompatibleTextRendering = true;
            this.defaultSNES9X.UseVisualStyleBackColor = false;
            this.defaultSNES9X.Click += new System.EventHandler(this.defaultSNES9X_Click);
            // 
            // defaultZSNES
            // 
            this.defaultZSNES.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultZSNES.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.defaultZSNES.Location = new System.Drawing.Point(524, 81);
            this.defaultZSNES.Name = "defaultZSNES";
            this.defaultZSNES.Size = new System.Drawing.Size(62, 21);
            this.defaultZSNES.TabIndex = 11;
            this.defaultZSNES.Text = "DEFAULT";
            this.defaultZSNES.UseCompatibleTextRendering = true;
            this.defaultZSNES.UseVisualStyleBackColor = false;
            this.defaultZSNES.Click += new System.EventHandler(this.defaultZSNES_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.eventListBox);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 133);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(418, 406);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Source of Entrance";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.selectIndex);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(174, 39);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.enableDebug);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.launchButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(418, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 406);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTips,
            this.baseConvertor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(592, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // helpTips
            // 
            this.helpTips.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpTips.CheckOnClick = true;
            this.helpTips.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.Text = "Help Tips";
            // 
            // baseConvertor
            // 
            this.baseConvertor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.baseConvertor.CheckOnClick = true;
            this.baseConvertor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.baseConvertor.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.baseConvertor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConvertor.Name = "baseConvertor";
            this.baseConvertor.Size = new System.Drawing.Size(23, 22);
            this.baseConvertor.Text = "Base Convertor";
            // 
            // Previewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 539);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Name = "Previewer";
            this.Text = "PREVIEWER - LAZYSHELL++";
            ((System.ComponentModel.ISupportInitialize)(this.selectIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.level)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label emuPathLabel;
        private System.Windows.Forms.ListBox eventListBox;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.Label romLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown selectIndex;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox emuPathTextBox;
        private System.Windows.Forms.TextBox romPathTextBox;
        private System.Windows.Forms.TextBox zsnesArgs;
        private System.Windows.Forms.LinkLabel linkLabelZSNES;
        private System.Windows.Forms.NumericUpDown adjustX;
        private System.Windows.Forms.NumericUpDown adjustY;
        private System.Windows.Forms.NumericUpDown adjustZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox battleBG;
        private System.Windows.Forms.TextBox snes9xArgs;
        private System.Windows.Forms.LinkLabel linkLabelSNES9X;
        private System.Windows.Forms.CheckedListBox alliesInParty;
        private System.Windows.Forms.CheckBox maxOutStats;
        private System.Windows.Forms.NumericUpDown level;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox allyWeapon;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.ComboBox allyAccessory;
        private System.Windows.Forms.ComboBox allyArmor;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.ComboBox allyName;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button changeEmuButton;
        private System.Windows.Forms.CheckBox dynamicROMPath;
        private System.Windows.Forms.Button defaultSNES9X;
        private System.Windows.Forms.Button defaultZSNES;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.CheckBox enableDebug;
    }
}