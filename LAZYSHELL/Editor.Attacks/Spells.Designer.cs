
namespace LAZYSHELL
{
    partial class Spells
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
            this.spellTargetting = new System.Windows.Forms.CheckedListBox();
            this.spellAttackProp = new System.Windows.Forms.CheckedListBox();
            this.spellStatusChange = new System.Windows.Forms.CheckedListBox();
            this.spellStatusEffect = new System.Windows.Forms.CheckedListBox();
            this.textBoxSpellDescription = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.textView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newLine = new System.Windows.Forms.ToolStripButton();
            this.endString = new System.Windows.Forms.ToolStripButton();
            this.pictureBoxSpellDesc = new System.Windows.Forms.PictureBox();
            this.spellInflictElement = new System.Windows.Forms.ComboBox();
            this.spellFunction = new System.Windows.Forms.ComboBox();
            this.spellEffectType = new System.Windows.Forms.ComboBox();
            this.label171 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.spellAttackType = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.spellHitRate = new System.Windows.Forms.NumericUpDown();
            this.label56 = new System.Windows.Forms.Label();
            this.spellFPCost = new System.Windows.Forms.NumericUpDown();
            this.spellMagPower = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.spellName = new LAZYSHELL.ToolStripComboBox();
            this.spellNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.spellNameIcon = new LAZYSHELL.ToolStripComboBox();
            this.textBoxSpellName = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.timingPropertiesBox = new System.Windows.Forms.GroupBox();
            this.AlliesSpellTimingPointer = new System.Windows.Forms.NumericUpDown();
            this.AlliesSpellTimingAutoset = new System.Windows.Forms.ComboBox();
            this.AlliesSpellDamagePointer = new System.Windows.Forms.NumericUpDown();
            this.AlliesSpellDamageAutoset = new System.Windows.Forms.ComboBox();
            this.damageModifiersBox = new System.Windows.Forms.GroupBox();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpellDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellHitRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellFPCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellMagPower)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.timingPropertiesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlliesSpellTimingPointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlliesSpellDamagePointer)).BeginInit();
            this.damageModifiersBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // spellTargetting
            // 
            this.spellTargetting.CheckOnClick = true;
            this.spellTargetting.ColumnWidth = 100;
            this.spellTargetting.Items.AddRange(new object[] {
            "Other Targets",
            "Enemy Party",
            "Entire Party",
            "Wounded Only",
            "One Party Only",
            "Not Self"});
            this.spellTargetting.Location = new System.Drawing.Point(6, 20);
            this.spellTargetting.MultiColumn = true;
            this.spellTargetting.Name = "spellTargetting";
            this.spellTargetting.Size = new System.Drawing.Size(183, 100);
            this.spellTargetting.TabIndex = 0;
            this.spellTargetting.SelectedIndexChanged += new System.EventHandler(this.spellTargetting_SelectedIndexChanged);
            // 
            // spellAttackProp
            // 
            this.spellAttackProp.CheckOnClick = true;
            this.spellAttackProp.Items.AddRange(new object[] {
            "Check Caster/Target Atk/Def",
            "Ignore Target\'s Defense",
            "Check Mortality Protection",
            "Usable in overworld menu",
            "9999 Damage/Heal",
            "Hide Battle Numerals"});
            this.spellAttackProp.Location = new System.Drawing.Point(6, 20);
            this.spellAttackProp.Name = "spellAttackProp";
            this.spellAttackProp.Size = new System.Drawing.Size(183, 100);
            this.spellAttackProp.TabIndex = 0;
            this.spellAttackProp.SelectedIndexChanged += new System.EventHandler(this.spellAttackProp_SelectedIndexChanged);
            // 
            // spellStatusChange
            // 
            this.spellStatusChange.CheckOnClick = true;
            this.spellStatusChange.ColumnWidth = 89;
            this.spellStatusChange.Items.AddRange(new object[] {
            "Attack",
            "Defense",
            "Mg. Attack",
            "Mg. Defense"});
            this.spellStatusChange.Location = new System.Drawing.Point(6, 20);
            this.spellStatusChange.MultiColumn = true;
            this.spellStatusChange.Name = "spellStatusChange";
            this.spellStatusChange.Size = new System.Drawing.Size(183, 36);
            this.spellStatusChange.TabIndex = 0;
            this.spellStatusChange.SelectedIndexChanged += new System.EventHandler(this.spellStatusChange_SelectedIndexChanged);
            // 
            // spellStatusEffect
            // 
            this.spellStatusEffect.CheckOnClick = true;
            this.spellStatusEffect.ColumnWidth = 89;
            this.spellStatusEffect.Items.AddRange(new object[] {
            "Mute",
            "Sleep",
            "Poison",
            "Fear",
            "Berserk",
            "Mushroom",
            "Scarecrow",
            "Invincible"});
            this.spellStatusEffect.Location = new System.Drawing.Point(6, 20);
            this.spellStatusEffect.MultiColumn = true;
            this.spellStatusEffect.Name = "spellStatusEffect";
            this.spellStatusEffect.Size = new System.Drawing.Size(183, 68);
            this.spellStatusEffect.TabIndex = 0;
            this.spellStatusEffect.SelectedIndexChanged += new System.EventHandler(this.spellStatusEffect_SelectedIndexChanged);
            // 
            // textBoxSpellDescription
            // 
            this.textBoxSpellDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSpellDescription.Location = new System.Drawing.Point(3, 110);
            this.textBoxSpellDescription.MaxLength = 255;
            this.textBoxSpellDescription.Name = "textBoxSpellDescription";
            this.textBoxSpellDescription.Size = new System.Drawing.Size(189, 77);
            this.textBoxSpellDescription.TabIndex = 2;
            this.textBoxSpellDescription.Text = "";
            this.textBoxSpellDescription.TextChanged += new System.EventHandler(this.textBoxSpellDescription_TextChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textView,
            this.toolStripSeparator2,
            this.newLine,
            this.endString});
            this.toolStrip2.Location = new System.Drawing.Point(3, 85);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(189, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // textView
            // 
            this.textView.CheckOnClick = true;
            this.textView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.textView.Image = global::LAZYSHELL.Properties.Resources.textView;
            this.textView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.textView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.textView.Name = "textView";
            this.textView.Size = new System.Drawing.Size(23, 22);
            this.textView.Click += new System.EventHandler(this.byteOrText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // newLine
            // 
            this.newLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newLine.Image = global::LAZYSHELL.Properties.Resources.newLine;
            this.newLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newLine.Name = "newLine";
            this.newLine.Size = new System.Drawing.Size(23, 22);
            this.newLine.Text = "New line";
            this.newLine.Click += new System.EventHandler(this.newLine_Click);
            // 
            // endString
            // 
            this.endString.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.endString.Image = global::LAZYSHELL.Properties.Resources.endString;
            this.endString.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.endString.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.endString.Name = "endString";
            this.endString.Size = new System.Drawing.Size(23, 22);
            this.endString.Text = "End string";
            this.endString.Click += new System.EventHandler(this.endString_Click);
            // 
            // pictureBoxSpellDesc
            // 
            this.pictureBoxSpellDesc.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxSpellDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSpellDesc.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSpellDesc.Name = "pictureBoxSpellDesc";
            this.pictureBoxSpellDesc.Size = new System.Drawing.Size(185, 64);
            this.pictureBoxSpellDesc.TabIndex = 0;
            this.pictureBoxSpellDesc.TabStop = false;
            this.pictureBoxSpellDesc.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSpellDesc_Paint);
            // 
            // spellInflictElement
            // 
            this.spellInflictElement.DropDownHeight = 250;
            this.spellInflictElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellInflictElement.IntegralHeight = false;
            this.spellInflictElement.Items.AddRange(new object[] {
            "Ice",
            "Thunder",
            "Fire",
            "Jump",
            "{NONE}"});
            this.spellInflictElement.Location = new System.Drawing.Point(90, 79);
            this.spellInflictElement.Name = "spellInflictElement";
            this.spellInflictElement.Size = new System.Drawing.Size(99, 21);
            this.spellInflictElement.TabIndex = 7;
            this.spellInflictElement.SelectedIndexChanged += new System.EventHandler(this.spellInflictElement_SelectedIndexChanged);
            // 
            // spellFunction
            // 
            this.spellFunction.DropDownHeight = 420;
            this.spellFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellFunction.IntegralHeight = false;
            this.spellFunction.Items.AddRange(new object[] {
            "Scan/Show HP",
            "Always Miss",
            "No Damage",
            "Revive/Heal",
            "Jump Power",
            "{NONE}"});
            this.spellFunction.Location = new System.Drawing.Point(90, 58);
            this.spellFunction.Name = "spellFunction";
            this.spellFunction.Size = new System.Drawing.Size(99, 21);
            this.spellFunction.TabIndex = 5;
            this.spellFunction.SelectedIndexChanged += new System.EventHandler(this.spellFunction_SelectedIndexChanged);
            // 
            // spellEffectType
            // 
            this.spellEffectType.DropDownHeight = 420;
            this.spellEffectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellEffectType.IntegralHeight = false;
            this.spellEffectType.Items.AddRange(new object[] {
            "Inflict",
            "Nullify",
            "{NONE}"});
            this.spellEffectType.Location = new System.Drawing.Point(90, 37);
            this.spellEffectType.Name = "spellEffectType";
            this.spellEffectType.Size = new System.Drawing.Size(99, 21);
            this.spellEffectType.TabIndex = 3;
            this.spellEffectType.SelectedIndexChanged += new System.EventHandler(this.spellEffectType_SelectedIndexChanged);
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(6, 61);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(78, 13);
            this.label171.TabIndex = 4;
            this.label171.Text = "Inflict Function";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Effect Type";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Attack Type";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 82);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Inflict Element";
            // 
            // spellAttackType
            // 
            this.spellAttackType.DropDownHeight = 420;
            this.spellAttackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellAttackType.IntegralHeight = false;
            this.spellAttackType.Items.AddRange(new object[] {
            "Damage",
            "Heal"});
            this.spellAttackType.Location = new System.Drawing.Point(90, 16);
            this.spellAttackType.Name = "spellAttackType";
            this.spellAttackType.Size = new System.Drawing.Size(99, 21);
            this.spellAttackType.TabIndex = 1;
            this.spellAttackType.SelectedIndexChanged += new System.EventHandler(this.spellAttackType_SelectedIndexChanged);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 22);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(44, 13);
            this.label65.TabIndex = 0;
            this.label65.Text = "FP Cost";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 43);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(67, 13);
            this.label64.TabIndex = 2;
            this.label64.Text = "Magic Power";
            // 
            // spellHitRate
            // 
            this.spellHitRate.Location = new System.Drawing.Point(79, 62);
            this.spellHitRate.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spellHitRate.Name = "spellHitRate";
            this.spellHitRate.Size = new System.Drawing.Size(110, 21);
            this.spellHitRate.TabIndex = 5;
            this.spellHitRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spellHitRate.ValueChanged += new System.EventHandler(this.spellHitRate_ValueChanged);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 64);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(57, 13);
            this.label56.TabIndex = 4;
            this.label56.Text = "Hit Rate%";
            // 
            // spellFPCost
            // 
            this.spellFPCost.Location = new System.Drawing.Point(79, 20);
            this.spellFPCost.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.spellFPCost.Name = "spellFPCost";
            this.spellFPCost.Size = new System.Drawing.Size(110, 21);
            this.spellFPCost.TabIndex = 1;
            this.spellFPCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spellFPCost.ValueChanged += new System.EventHandler(this.spellFPCost_ValueChanged);
            // 
            // spellMagPower
            // 
            this.spellMagPower.Location = new System.Drawing.Point(79, 41);
            this.spellMagPower.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spellMagPower.Name = "spellMagPower";
            this.spellMagPower.Size = new System.Drawing.Size(110, 21);
            this.spellMagPower.TabIndex = 3;
            this.spellMagPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spellMagPower.ValueChanged += new System.EventHandler(this.spellMagPower_ValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spellName,
            this.spellNum,
            this.toolStripSeparator1,
            this.spellNameIcon,
            this.textBoxSpellName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(408, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // spellName
            // 
            this.spellName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.spellName.ContextMenuStrip = null;
            this.spellName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.spellName.DropDownHeight = 497;
            this.spellName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellName.DropDownWidth = 130;
            this.spellName.ItemHeight = 15;
            this.spellName.Location = new System.Drawing.Point(9, 1);
            this.spellName.Name = "spellName";
            this.spellName.SelectedIndex = -1;
            this.spellName.SelectedItem = null;
            this.spellName.Size = new System.Drawing.Size(148, 22);
            this.spellName.SelectedIndexChanged += new System.EventHandler(this.spellName_SelectedIndexChanged);
            this.spellName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.spellName_DrawItem);
            // 
            // spellNum
            // 
            this.spellNum.ContextMenuStrip = null;
            this.spellNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellNum.Hexadecimal = false;
            this.spellNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spellNum.Location = new System.Drawing.Point(157, 1);
            this.spellNum.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.spellNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spellNum.Name = "spellNum";
            this.spellNum.Size = new System.Drawing.Size(41, 22);
            this.spellNum.Text = "0";
            this.spellNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spellNum.ValueChanged += new System.EventHandler(this.spellNum_ValueChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // spellNameIcon
            // 
            this.spellNameIcon.AutoSize = false;
            this.spellNameIcon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.spellNameIcon.ContextMenuStrip = null;
            this.spellNameIcon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.spellNameIcon.DropDownHeight = 497;
            this.spellNameIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spellNameIcon.DropDownWidth = 40;
            this.spellNameIcon.ItemHeight = 15;
            this.spellNameIcon.Location = new System.Drawing.Point(204, 2);
            this.spellNameIcon.Name = "spellNameIcon";
            this.spellNameIcon.SelectedIndex = -1;
            this.spellNameIcon.SelectedItem = null;
            this.spellNameIcon.Size = new System.Drawing.Size(36, 21);
            this.spellNameIcon.SelectedIndexChanged += new System.EventHandler(this.spellNameIcon_SelectedIndexChanged);
            this.spellNameIcon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.spellNameIcon_DrawItem);
            // 
            // textBoxSpellName
            // 
            this.textBoxSpellName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxSpellName.MaxLength = 14;
            this.textBoxSpellName.Name = "textBoxSpellName";
            this.textBoxSpellName.Size = new System.Drawing.Size(154, 25);
            this.textBoxSpellName.TextChanged += new System.EventHandler(this.textBoxSpellName_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label65);
            this.groupBox1.Controls.Add(this.spellMagPower);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.spellFPCost);
            this.groupBox1.Controls.Add(this.spellHitRate);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Location = new System.Drawing.Point(6, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spell Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.spellInflictElement);
            this.groupBox2.Controls.Add(this.spellFunction);
            this.groupBox2.Controls.Add(this.spellEffectType);
            this.groupBox2.Controls.Add(this.spellAttackType);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label171);
            this.groupBox2.Location = new System.Drawing.Point(6, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attack Properties";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxSpellDescription);
            this.groupBox3.Controls.Add(this.toolStrip2);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(207, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 190);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Description";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBoxSpellDesc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 68);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.spellTargetting);
            this.groupBox4.Location = new System.Drawing.Point(6, 367);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(195, 126);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Targetting";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.spellAttackProp);
            this.groupBox7.Location = new System.Drawing.Point(6, 235);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(195, 126);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Other Properties";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.spellStatusEffect);
            this.groupBox8.Location = new System.Drawing.Point(207, 225);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(195, 94);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Effects";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.spellStatusChange);
            this.groupBox9.Location = new System.Drawing.Point(207, 325);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(195, 62);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Status Effects";
            // 
            // timingPropertiesBox
            // 
            this.timingPropertiesBox.Controls.Add(this.AlliesSpellTimingPointer);
            this.timingPropertiesBox.Controls.Add(this.AlliesSpellTimingAutoset);
            this.timingPropertiesBox.Location = new System.Drawing.Point(207, 393);
            this.timingPropertiesBox.Name = "timingPropertiesBox";
            this.timingPropertiesBox.Size = new System.Drawing.Size(195, 45);
            this.timingPropertiesBox.TabIndex = 11;
            this.timingPropertiesBox.TabStop = false;
            this.timingPropertiesBox.Text = "Timing Properties";
            // 
            // AlliesSpellTimingPointer
            // 
            this.AlliesSpellTimingPointer.Hexadecimal = true;
            this.AlliesSpellTimingPointer.Location = new System.Drawing.Point(6, 17);
            this.AlliesSpellTimingPointer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.AlliesSpellTimingPointer.Name = "AlliesSpellTimingPointer";
            this.AlliesSpellTimingPointer.Size = new System.Drawing.Size(64, 21);
            this.AlliesSpellTimingPointer.TabIndex = 8;
            this.AlliesSpellTimingPointer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AlliesSpellTimingPointer.ValueChanged += new System.EventHandler(this.AlliesSpellTimingPointer_ValueChanged);
            // 
            // AlliesSpellTimingAutoset
            // 
            this.AlliesSpellTimingAutoset.DropDownHeight = 236;
            this.AlliesSpellTimingAutoset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlliesSpellTimingAutoset.DropDownWidth = 200;
            this.AlliesSpellTimingAutoset.IntegralHeight = false;
            this.AlliesSpellTimingAutoset.Items.AddRange(new object[] {
            "1 timing for x1.25 or x1.5 dmg",
            "multiple button presses",
            "1 + more targets w/ presses",
            "1 timing for x1.25 dmg only",
            "rotate. 1 target, if timed: all",
            "timed heals all HP to first target",
            "button mash",
            "rotate only",
            "charge only",
            "timed gives target Defense Up Buff",
            "timed for 9999+set enemy HP to 0",
            "time to activate HP read",
            "timed jumps",
            "{UNKNOWN}"});
            this.AlliesSpellTimingAutoset.Location = new System.Drawing.Point(70, 17);
            this.AlliesSpellTimingAutoset.Name = "AlliesSpellTimingAutoset";
            this.AlliesSpellTimingAutoset.Size = new System.Drawing.Size(119, 21);
            this.AlliesSpellTimingAutoset.TabIndex = 7;
            this.AlliesSpellTimingAutoset.SelectedIndexChanged += new System.EventHandler(this.AlliesSpellTimingAutoset_SelectedIndexChanged);
            // 
            // AlliesSpellDamagePointer
            // 
            this.AlliesSpellDamagePointer.Hexadecimal = true;
            this.AlliesSpellDamagePointer.Location = new System.Drawing.Point(6, 20);
            this.AlliesSpellDamagePointer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.AlliesSpellDamagePointer.Name = "AlliesSpellDamagePointer";
            this.AlliesSpellDamagePointer.Size = new System.Drawing.Size(64, 21);
            this.AlliesSpellDamagePointer.TabIndex = 10;
            this.AlliesSpellDamagePointer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AlliesSpellDamagePointer.ValueChanged += new System.EventHandler(this.AlliesSpellDamagePointer_ValueChanged);
            // 
            // AlliesSpellDamageAutoset
            // 
            this.AlliesSpellDamageAutoset.DropDownHeight = 236;
            this.AlliesSpellDamageAutoset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlliesSpellDamageAutoset.DropDownWidth = 200;
            this.AlliesSpellDamageAutoset.IntegralHeight = false;
            this.AlliesSpellDamageAutoset.Items.AddRange(new object[] {
            "No modifiers",
            "x0.0625 damage mod",
            "x0.5 damage mod",
            "x0.125 modifier with multi-targetting",
            "x0.0625 modifier w/ multi-targetting",
            "{UNKNOWN}"});
            this.AlliesSpellDamageAutoset.Location = new System.Drawing.Point(70, 20);
            this.AlliesSpellDamageAutoset.Name = "AlliesSpellDamageAutoset";
            this.AlliesSpellDamageAutoset.Size = new System.Drawing.Size(119, 21);
            this.AlliesSpellDamageAutoset.TabIndex = 9;
            this.AlliesSpellDamageAutoset.SelectedIndexChanged += new System.EventHandler(this.AlliesSpellDamageAutoset_SelectedIndexChanged);
            // 
            // damageModifiersBox
            // 
            this.damageModifiersBox.Controls.Add(this.AlliesSpellDamagePointer);
            this.damageModifiersBox.Controls.Add(this.AlliesSpellDamageAutoset);
            this.damageModifiersBox.Location = new System.Drawing.Point(207, 444);
            this.damageModifiersBox.Name = "damageModifiersBox";
            this.damageModifiersBox.Size = new System.Drawing.Size(195, 49);
            this.damageModifiersBox.TabIndex = 8;
            this.damageModifiersBox.TabStop = false;
            this.damageModifiersBox.Text = "Damage Modifiers";
            // 
            // Spells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 498);
            this.ControlBox = false;
            this.Controls.Add(this.damageModifiersBox);
            this.Controls.Add(this.timingPropertiesBox);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Spells";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpellDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellHitRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellFPCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spellMagPower)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.timingPropertiesBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AlliesSpellTimingPointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlliesSpellDamagePointer)).EndInit();
            this.damageModifiersBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.CheckedListBox spellTargetting;
        private System.Windows.Forms.CheckedListBox spellAttackProp;
        private System.Windows.Forms.CheckedListBox spellStatusChange;
        private System.Windows.Forms.CheckedListBox spellStatusEffect;
        private System.Windows.Forms.PictureBox pictureBoxSpellDesc;
        private System.Windows.Forms.RichTextBox textBoxSpellDescription;
        private System.Windows.Forms.ComboBox spellInflictElement;
        private System.Windows.Forms.ComboBox spellFunction;
        private System.Windows.Forms.ComboBox spellEffectType;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox spellAttackType;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.NumericUpDown spellHitRate;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.NumericUpDown spellFPCost;
        private System.Windows.Forms.NumericUpDown spellMagPower;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private LAZYSHELL.ToolStripComboBox spellName;
        private System.Windows.Forms.ToolStripTextBox textBoxSpellName;
        private LAZYSHELL.ToolStripComboBox spellNameIcon;
        private ToolStripNumericUpDown spellNum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton textView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton newLine;
        private System.Windows.Forms.ToolStripButton endString;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox timingPropertiesBox;
        private System.Windows.Forms.NumericUpDown AlliesSpellDamagePointer;
        private System.Windows.Forms.ComboBox AlliesSpellDamageAutoset;
        private System.Windows.Forms.NumericUpDown AlliesSpellTimingPointer;
        private System.Windows.Forms.ComboBox AlliesSpellTimingAutoset;
        private System.Windows.Forms.GroupBox damageModifiersBox;
    }
}