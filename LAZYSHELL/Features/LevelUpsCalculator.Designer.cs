
namespace LAZYSHELL
{
    partial class LevelUpsCalculator
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
            this.LabelMonsterValMgDef = new System.Windows.Forms.Label();
            this.LabelMonsterValMgAtk = new System.Windows.Forms.Label();
            this.LabelMonsterValAtk = new System.Windows.Forms.Label();
            this.LabelMonsterValDef = new System.Windows.Forms.Label();
            this.attackerAttack = new System.Windows.Forms.NumericUpDown();
            this.attackerMgDefense = new System.Windows.Forms.NumericUpDown();
            this.attackerMgAttack = new System.Windows.Forms.NumericUpDown();
            this.attackerDefense = new System.Windows.Forms.NumericUpDown();
            this.attackerWeapon = new System.Windows.Forms.ComboBox();
            this.labelAttackerAccessory = new System.Windows.Forms.Label();
            this.attackerAccessory = new System.Windows.Forms.ComboBox();
            this.attackerArmor = new System.Windows.Forms.ComboBox();
            this.labelAttackerWeapon = new System.Windows.Forms.Label();
            this.labelAttackerArmor = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.attackerHP = new System.Windows.Forms.NumericUpDown();
            this.attackerName = new System.Windows.Forms.ComboBox();
            this.attackerLevel = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.attackerBonus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelAttackerProperties = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelAttackerStats = new System.Windows.Forms.Panel();
            this.listView1 = new LAZYSHELL.NewListView();
            this.spellIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.spellName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.attackerAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerMgDefense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerMgAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerDefense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerLevel)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelAttackerProperties.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelAttackerStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelMonsterValMgDef
            // 
            this.LabelMonsterValMgDef.AutoSize = true;
            this.LabelMonsterValMgDef.Location = new System.Drawing.Point(0, 86);
            this.LabelMonsterValMgDef.Name = "LabelMonsterValMgDef";
            this.LabelMonsterValMgDef.Size = new System.Drawing.Size(68, 13);
            this.LabelMonsterValMgDef.TabIndex = 4;
            this.LabelMonsterValMgDef.Text = "Mg. Defense";
            // 
            // LabelMonsterValMgAtk
            // 
            this.LabelMonsterValMgAtk.AutoSize = true;
            this.LabelMonsterValMgAtk.Location = new System.Drawing.Point(0, 65);
            this.LabelMonsterValMgAtk.Name = "LabelMonsterValMgAtk";
            this.LabelMonsterValMgAtk.Size = new System.Drawing.Size(59, 13);
            this.LabelMonsterValMgAtk.TabIndex = 3;
            this.LabelMonsterValMgAtk.Text = "Mg. Attack";
            // 
            // LabelMonsterValAtk
            // 
            this.LabelMonsterValAtk.AutoSize = true;
            this.LabelMonsterValAtk.Location = new System.Drawing.Point(0, 23);
            this.LabelMonsterValAtk.Name = "LabelMonsterValAtk";
            this.LabelMonsterValAtk.Size = new System.Drawing.Size(38, 13);
            this.LabelMonsterValAtk.TabIndex = 1;
            this.LabelMonsterValAtk.Text = "Attack";
            // 
            // LabelMonsterValDef
            // 
            this.LabelMonsterValDef.AutoSize = true;
            this.LabelMonsterValDef.Location = new System.Drawing.Point(0, 44);
            this.LabelMonsterValDef.Name = "LabelMonsterValDef";
            this.LabelMonsterValDef.Size = new System.Drawing.Size(47, 13);
            this.LabelMonsterValDef.TabIndex = 2;
            this.LabelMonsterValDef.Text = "Defense";
            // 
            // attackerAttack
            // 
            this.attackerAttack.Location = new System.Drawing.Point(97, 21);
            this.attackerAttack.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.attackerAttack.Name = "attackerAttack";
            this.attackerAttack.Size = new System.Drawing.Size(103, 21);
            this.attackerAttack.TabIndex = 9;
            this.attackerAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerAttack.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.attackerAttack.ValueChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerMgDefense
            // 
            this.attackerMgDefense.Location = new System.Drawing.Point(97, 84);
            this.attackerMgDefense.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.attackerMgDefense.Name = "attackerMgDefense";
            this.attackerMgDefense.Size = new System.Drawing.Size(103, 21);
            this.attackerMgDefense.TabIndex = 12;
            this.attackerMgDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerMgDefense.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.attackerMgDefense.ValueChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerMgAttack
            // 
            this.attackerMgAttack.Location = new System.Drawing.Point(97, 63);
            this.attackerMgAttack.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.attackerMgAttack.Name = "attackerMgAttack";
            this.attackerMgAttack.Size = new System.Drawing.Size(103, 21);
            this.attackerMgAttack.TabIndex = 11;
            this.attackerMgAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerMgAttack.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.attackerMgAttack.ValueChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerDefense
            // 
            this.attackerDefense.Location = new System.Drawing.Point(97, 42);
            this.attackerDefense.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.attackerDefense.Name = "attackerDefense";
            this.attackerDefense.Size = new System.Drawing.Size(103, 21);
            this.attackerDefense.TabIndex = 10;
            this.attackerDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerDefense.ValueChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerWeapon
            // 
            this.attackerWeapon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.attackerWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.attackerWeapon.DropDownHeight = 317;
            this.attackerWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerWeapon.DropDownWidth = 150;
            this.attackerWeapon.IntegralHeight = false;
            this.attackerWeapon.ItemHeight = 15;
            this.attackerWeapon.Location = new System.Drawing.Point(97, 105);
            this.attackerWeapon.Name = "attackerWeapon";
            this.attackerWeapon.Size = new System.Drawing.Size(103, 21);
            this.attackerWeapon.TabIndex = 13;
            this.attackerWeapon.Tag = "";
            this.attackerWeapon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.attackerWeapon.SelectedIndexChanged += new System.EventHandler(this.calculateTotal);
            // 
            // labelAttackerAccessory
            // 
            this.labelAttackerAccessory.AutoSize = true;
            this.labelAttackerAccessory.Location = new System.Drawing.Point(0, 149);
            this.labelAttackerAccessory.Name = "labelAttackerAccessory";
            this.labelAttackerAccessory.Size = new System.Drawing.Size(56, 13);
            this.labelAttackerAccessory.TabIndex = 7;
            this.labelAttackerAccessory.Text = "Accessory";
            // 
            // attackerAccessory
            // 
            this.attackerAccessory.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.attackerAccessory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.attackerAccessory.DropDownHeight = 317;
            this.attackerAccessory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerAccessory.DropDownWidth = 150;
            this.attackerAccessory.IntegralHeight = false;
            this.attackerAccessory.ItemHeight = 15;
            this.attackerAccessory.Location = new System.Drawing.Point(97, 147);
            this.attackerAccessory.Name = "attackerAccessory";
            this.attackerAccessory.Size = new System.Drawing.Size(103, 21);
            this.attackerAccessory.TabIndex = 15;
            this.attackerAccessory.Tag = "";
            this.attackerAccessory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.attackerAccessory.SelectedIndexChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerArmor
            // 
            this.attackerArmor.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.attackerArmor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.attackerArmor.DropDownHeight = 317;
            this.attackerArmor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerArmor.DropDownWidth = 150;
            this.attackerArmor.IntegralHeight = false;
            this.attackerArmor.ItemHeight = 15;
            this.attackerArmor.Location = new System.Drawing.Point(97, 126);
            this.attackerArmor.Name = "attackerArmor";
            this.attackerArmor.Size = new System.Drawing.Size(103, 21);
            this.attackerArmor.TabIndex = 14;
            this.attackerArmor.Tag = "";
            this.attackerArmor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.attackerArmor.SelectedIndexChanged += new System.EventHandler(this.calculateTotal);
            // 
            // labelAttackerWeapon
            // 
            this.labelAttackerWeapon.AutoSize = true;
            this.labelAttackerWeapon.Location = new System.Drawing.Point(0, 107);
            this.labelAttackerWeapon.Name = "labelAttackerWeapon";
            this.labelAttackerWeapon.Size = new System.Drawing.Size(47, 13);
            this.labelAttackerWeapon.TabIndex = 5;
            this.labelAttackerWeapon.Text = "Weapon";
            // 
            // labelAttackerArmor
            // 
            this.labelAttackerArmor.AutoSize = true;
            this.labelAttackerArmor.Location = new System.Drawing.Point(0, 128);
            this.labelAttackerArmor.Name = "labelAttackerArmor";
            this.labelAttackerArmor.Size = new System.Drawing.Size(36, 13);
            this.labelAttackerArmor.TabIndex = 6;
            this.labelAttackerArmor.Text = "Armor";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "HP";
            // 
            // attackerHP
            // 
            this.attackerHP.Location = new System.Drawing.Point(97, 0);
            this.attackerHP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.attackerHP.Name = "attackerHP";
            this.attackerHP.Size = new System.Drawing.Size(103, 21);
            this.attackerHP.TabIndex = 8;
            this.attackerHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerHP.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.attackerHP.ValueChanged += new System.EventHandler(this.calculateTotal);
            // 
            // attackerName
            // 
            this.attackerName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.attackerName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.attackerName.DropDownHeight = 317;
            this.attackerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerName.DropDownWidth = 150;
            this.attackerName.IntegralHeight = false;
            this.attackerName.ItemHeight = 15;
            this.attackerName.Location = new System.Drawing.Point(0, -2);
            this.attackerName.Name = "attackerName";
            this.attackerName.Size = new System.Drawing.Size(200, 21);
            this.attackerName.TabIndex = 0;
            this.attackerName.Tag = "";
            this.attackerName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.attackerName_DrawItem);
            this.attackerName.SelectedIndexChanged += new System.EventHandler(this.loadProperties);
            // 
            // attackerLevel
            // 
            this.attackerLevel.Location = new System.Drawing.Point(97, 27);
            this.attackerLevel.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.attackerLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attackerLevel.Name = "attackerLevel";
            this.attackerLevel.Size = new System.Drawing.Size(103, 21);
            this.attackerLevel.TabIndex = 3;
            this.attackerLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackerLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attackerLevel.ValueChanged += new System.EventHandler(this.loadProperties);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Level";
            // 
            // attackerBonus
            // 
            this.attackerBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackerBonus.IntegralHeight = false;
            this.attackerBonus.ItemHeight = 13;
            this.attackerBonus.Items.AddRange(new object[] {
            "balanced",
            "always HP",
            "always Magic",
            "always Attack"});
            this.attackerBonus.Location = new System.Drawing.Point(97, 48);
            this.attackerBonus.Name = "attackerBonus";
            this.attackerBonus.Size = new System.Drawing.Size(103, 21);
            this.attackerBonus.TabIndex = 4;
            this.attackerBonus.Tag = "";
            this.attackerBonus.SelectedIndexChanged += new System.EventHandler(this.loadProperties);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Level-up bonus";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelAttackerProperties);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 94);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character Level";
            // 
            // panelAttackerProperties
            // 
            this.panelAttackerProperties.Controls.Add(this.attackerName);
            this.panelAttackerProperties.Controls.Add(this.attackerBonus);
            this.panelAttackerProperties.Controls.Add(this.label10);
            this.panelAttackerProperties.Controls.Add(this.label9);
            this.panelAttackerProperties.Controls.Add(this.attackerLevel);
            this.panelAttackerProperties.Location = new System.Drawing.Point(6, 18);
            this.panelAttackerProperties.Name = "panelAttackerProperties";
            this.panelAttackerProperties.Size = new System.Drawing.Size(200, 69);
            this.panelAttackerProperties.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelAttackerStats);
            this.groupBox3.Location = new System.Drawing.Point(12, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 196);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Character Status";
            // 
            // panelAttackerStats
            // 
            this.panelAttackerStats.Controls.Add(this.attackerHP);
            this.panelAttackerStats.Controls.Add(this.label13);
            this.panelAttackerStats.Controls.Add(this.attackerDefense);
            this.panelAttackerStats.Controls.Add(this.LabelMonsterValAtk);
            this.panelAttackerStats.Controls.Add(this.attackerMgAttack);
            this.panelAttackerStats.Controls.Add(this.labelAttackerWeapon);
            this.panelAttackerStats.Controls.Add(this.attackerAccessory);
            this.panelAttackerStats.Controls.Add(this.attackerMgDefense);
            this.panelAttackerStats.Controls.Add(this.LabelMonsterValMgAtk);
            this.panelAttackerStats.Controls.Add(this.attackerWeapon);
            this.panelAttackerStats.Controls.Add(this.labelAttackerAccessory);
            this.panelAttackerStats.Controls.Add(this.labelAttackerArmor);
            this.panelAttackerStats.Controls.Add(this.LabelMonsterValDef);
            this.panelAttackerStats.Controls.Add(this.attackerAttack);
            this.panelAttackerStats.Controls.Add(this.LabelMonsterValMgDef);
            this.panelAttackerStats.Controls.Add(this.attackerArmor);
            this.panelAttackerStats.Location = new System.Drawing.Point(6, 20);
            this.panelAttackerStats.Name = "panelAttackerStats";
            this.panelAttackerStats.Size = new System.Drawing.Size(200, 168);
            this.panelAttackerStats.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.spellIndex,
            this.spellName});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(230, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(172, 295);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            // 
            // spellIndex
            // 
            this.spellIndex.Tag = "";
            this.spellIndex.Text = "LV";
            this.spellIndex.Width = 29;
            // 
            // spellName
            // 
            this.spellName.Tag = "";
            this.spellName.Text = "Spell";
            this.spellName.Width = 139;
            // 
            // LevelUpsCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 312);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "LevelUpsCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "STATUS CALCULATOR - LAZYSHELL++";
            ((System.ComponentModel.ISupportInitialize)(this.attackerAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerMgDefense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerMgAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerDefense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackerLevel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panelAttackerProperties.ResumeLayout(false);
            this.panelAttackerProperties.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panelAttackerStats.ResumeLayout(false);
            this.panelAttackerStats.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Label LabelMonsterValMgDef;
        private System.Windows.Forms.Label LabelMonsterValMgAtk;
        private System.Windows.Forms.Label LabelMonsterValAtk;
        private System.Windows.Forms.Label LabelMonsterValDef;
        private System.Windows.Forms.NumericUpDown attackerAttack;
        private System.Windows.Forms.NumericUpDown attackerMgDefense;
        private System.Windows.Forms.NumericUpDown attackerMgAttack;
        private System.Windows.Forms.NumericUpDown attackerDefense;
        private System.Windows.Forms.ComboBox attackerWeapon;
        private System.Windows.Forms.Label labelAttackerAccessory;
        private System.Windows.Forms.ComboBox attackerAccessory;
        private System.Windows.Forms.ComboBox attackerArmor;
        private System.Windows.Forms.Label labelAttackerWeapon;
        private System.Windows.Forms.Label labelAttackerArmor;
        private System.Windows.Forms.ComboBox attackerName;
        private System.Windows.Forms.NumericUpDown attackerLevel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox attackerBonus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown attackerHP;
        private LAZYSHELL.NewListView listView1;
        private System.Windows.Forms.ColumnHeader spellIndex;
        private System.Windows.Forms.ColumnHeader spellName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panelAttackerStats;
        private System.Windows.Forms.Panel panelAttackerProperties;
    }
}