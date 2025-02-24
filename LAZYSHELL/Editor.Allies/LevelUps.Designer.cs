
namespace LAZYSHELL
{
    partial class LevelUps
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
            this.levelUpSpellLearned = new System.Windows.Forms.ComboBox();
            this.label137 = new System.Windows.Forms.Label();
            this.hpPlusBonus = new System.Windows.Forms.NumericUpDown();
            this.label113 = new System.Windows.Forms.Label();
            this.defensePlusBonus = new System.Windows.Forms.NumericUpDown();
            this.attackPlusBonus = new System.Windows.Forms.NumericUpDown();
            this.label114 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.mgDefensePlusBonus = new System.Windows.Forms.NumericUpDown();
            this.mgAttackPlusBonus = new System.Windows.Forms.NumericUpDown();
            this.label115 = new System.Windows.Forms.Label();
            this.hpPlus = new System.Windows.Forms.NumericUpDown();
            this.attackPlus = new System.Windows.Forms.NumericUpDown();
            this.label122 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.mgAttackPlus = new System.Windows.Forms.NumericUpDown();
            this.label120 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.defensePlus = new System.Windows.Forms.NumericUpDown();
            this.mgDefensePlus = new System.Windows.Forms.NumericUpDown();
            this.label118 = new System.Windows.Forms.Label();
            this.expNeeded = new System.Windows.Forms.NumericUpDown();
            this.label124 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.calculatorButton = new System.Windows.Forms.ToolStripButton();
            this.levelNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.hpPlusBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensePlusBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackPlusBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgDefensePlusBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgAttackPlusBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hpPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgAttackPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensePlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgDefensePlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expNeeded)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // levelUpSpellLearned
            // 
            this.levelUpSpellLearned.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.levelUpSpellLearned.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.levelUpSpellLearned.DropDownHeight = 317;
            this.levelUpSpellLearned.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelUpSpellLearned.DropDownWidth = 150;
            this.levelUpSpellLearned.IntegralHeight = false;
            this.levelUpSpellLearned.ItemHeight = 15;
            this.levelUpSpellLearned.Location = new System.Drawing.Point(6, 20);
            this.levelUpSpellLearned.Name = "levelUpSpellLearned";
            this.levelUpSpellLearned.Size = new System.Drawing.Size(148, 21);
            this.levelUpSpellLearned.TabIndex = 0;
            this.levelUpSpellLearned.Tag = "";
            this.levelUpSpellLearned.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.levelUpSpellLearned_DrawItem);
            this.levelUpSpellLearned.SelectedIndexChanged += new System.EventHandler(this.levelUpSpellLearned_SelectedIndexChanged);
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(6, 85);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(67, 13);
            this.label137.TabIndex = 6;
            this.label137.Text = "Mg. Attack+";
            // 
            // hpPlusBonus
            // 
            this.hpPlusBonus.Location = new System.Drawing.Point(88, 20);
            this.hpPlusBonus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.hpPlusBonus.Name = "hpPlusBonus";
            this.hpPlusBonus.Size = new System.Drawing.Size(66, 21);
            this.hpPlusBonus.TabIndex = 1;
            this.hpPlusBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hpPlusBonus.ValueChanged += new System.EventHandler(this.hpPlusBonus_ValueChanged);
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(6, 106);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(76, 13);
            this.label113.TabIndex = 8;
            this.label113.Text = "Mg. Defense+";
            // 
            // defensePlusBonus
            // 
            this.defensePlusBonus.Location = new System.Drawing.Point(88, 62);
            this.defensePlusBonus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.defensePlusBonus.Name = "defensePlusBonus";
            this.defensePlusBonus.Size = new System.Drawing.Size(66, 21);
            this.defensePlusBonus.TabIndex = 5;
            this.defensePlusBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.defensePlusBonus.ValueChanged += new System.EventHandler(this.defensePlusBonus_ValueChanged);
            // 
            // attackPlusBonus
            // 
            this.attackPlusBonus.Location = new System.Drawing.Point(88, 41);
            this.attackPlusBonus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.attackPlusBonus.Name = "attackPlusBonus";
            this.attackPlusBonus.Size = new System.Drawing.Size(66, 21);
            this.attackPlusBonus.TabIndex = 3;
            this.attackPlusBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackPlusBonus.ValueChanged += new System.EventHandler(this.attackPlusBonus_ValueChanged);
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(6, 64);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(55, 13);
            this.label114.TabIndex = 4;
            this.label114.Text = "Defense+";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(6, 22);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(28, 13);
            this.label116.TabIndex = 0;
            this.label116.Text = "HP+";
            // 
            // mgDefensePlusBonus
            // 
            this.mgDefensePlusBonus.Location = new System.Drawing.Point(88, 104);
            this.mgDefensePlusBonus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.mgDefensePlusBonus.Name = "mgDefensePlusBonus";
            this.mgDefensePlusBonus.Size = new System.Drawing.Size(66, 21);
            this.mgDefensePlusBonus.TabIndex = 9;
            this.mgDefensePlusBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mgDefensePlusBonus.ValueChanged += new System.EventHandler(this.mgDefensePlusBonus_ValueChanged);
            // 
            // mgAttackPlusBonus
            // 
            this.mgAttackPlusBonus.Location = new System.Drawing.Point(88, 83);
            this.mgAttackPlusBonus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.mgAttackPlusBonus.Name = "mgAttackPlusBonus";
            this.mgAttackPlusBonus.Size = new System.Drawing.Size(66, 21);
            this.mgAttackPlusBonus.TabIndex = 7;
            this.mgAttackPlusBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mgAttackPlusBonus.ValueChanged += new System.EventHandler(this.mgAttackPlusBonus_ValueChanged);
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(6, 43);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(46, 13);
            this.label115.TabIndex = 2;
            this.label115.Text = "Attack+";
            // 
            // hpPlus
            // 
            this.hpPlus.Location = new System.Drawing.Point(88, 20);
            this.hpPlus.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.hpPlus.Name = "hpPlus";
            this.hpPlus.Size = new System.Drawing.Size(66, 21);
            this.hpPlus.TabIndex = 1;
            this.hpPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hpPlus.ValueChanged += new System.EventHandler(this.hpPlus_ValueChanged);
            // 
            // attackPlus
            // 
            this.attackPlus.Location = new System.Drawing.Point(88, 41);
            this.attackPlus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.attackPlus.Name = "attackPlus";
            this.attackPlus.Size = new System.Drawing.Size(66, 21);
            this.attackPlus.TabIndex = 3;
            this.attackPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackPlus.ValueChanged += new System.EventHandler(this.attackPlus_ValueChanged);
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(6, 22);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(28, 13);
            this.label122.TabIndex = 0;
            this.label122.Text = "HP+";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(6, 43);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(46, 13);
            this.label121.TabIndex = 2;
            this.label121.Text = "Attack+";
            // 
            // mgAttackPlus
            // 
            this.mgAttackPlus.Location = new System.Drawing.Point(88, 83);
            this.mgAttackPlus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.mgAttackPlus.Name = "mgAttackPlus";
            this.mgAttackPlus.Size = new System.Drawing.Size(66, 21);
            this.mgAttackPlus.TabIndex = 7;
            this.mgAttackPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mgAttackPlus.ValueChanged += new System.EventHandler(this.mgAttackPlus_ValueChanged);
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(6, 64);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(55, 13);
            this.label120.TabIndex = 4;
            this.label120.Text = "Defense+";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(6, 85);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(67, 13);
            this.label117.TabIndex = 6;
            this.label117.Text = "Mg. Attack+";
            // 
            // defensePlus
            // 
            this.defensePlus.Location = new System.Drawing.Point(88, 62);
            this.defensePlus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.defensePlus.Name = "defensePlus";
            this.defensePlus.Size = new System.Drawing.Size(66, 21);
            this.defensePlus.TabIndex = 5;
            this.defensePlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.defensePlus.ValueChanged += new System.EventHandler(this.defensePlus_ValueChanged);
            // 
            // mgDefensePlus
            // 
            this.mgDefensePlus.Location = new System.Drawing.Point(88, 104);
            this.mgDefensePlus.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.mgDefensePlus.Name = "mgDefensePlus";
            this.mgDefensePlus.Size = new System.Drawing.Size(66, 21);
            this.mgDefensePlus.TabIndex = 9;
            this.mgDefensePlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mgDefensePlus.ValueChanged += new System.EventHandler(this.mgDefensePlus_ValueChanged);
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(6, 106);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(76, 13);
            this.label118.TabIndex = 8;
            this.label118.Text = "Mg. Defense+";
            // 
            // expNeeded
            // 
            this.expNeeded.Location = new System.Drawing.Point(77, 20);
            this.expNeeded.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.expNeeded.Name = "expNeeded";
            this.expNeeded.Size = new System.Drawing.Size(77, 21);
            this.expNeeded.TabIndex = 1;
            this.expNeeded.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.expNeeded.ValueChanged += new System.EventHandler(this.expNeeded_ValueChanged);
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(6, 22);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(60, 13);
            this.label124.TabIndex = 0;
            this.label124.Text = "Experience";
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calculatorButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(166, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // calculatorButton
            // 
            this.calculatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.calculatorButton.Image = global::LAZYSHELL.Properties.Resources.calculator;
            this.calculatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calculatorButton.Name = "calculatorButton";
            this.calculatorButton.Size = new System.Drawing.Size(23, 22);
            this.calculatorButton.Click += new System.EventHandler(this.calculatorButton_Click);
            // 
            // levelNum
            // 
            this.levelNum.Location = new System.Drawing.Point(55, 28);
            this.levelNum.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.levelNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.levelNum.Name = "levelNum";
            this.levelNum.Size = new System.Drawing.Size(61, 21);
            this.levelNum.TabIndex = 2;
            this.levelNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.levelNum.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.levelNum.ValueChanged += new System.EventHandler(this.levelNum_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Level #";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label122);
            this.groupBox1.Controls.Add(this.hpPlus);
            this.groupBox1.Controls.Add(this.label118);
            this.groupBox1.Controls.Add(this.attackPlus);
            this.groupBox1.Controls.Add(this.mgDefensePlus);
            this.groupBox1.Controls.Add(this.defensePlus);
            this.groupBox1.Controls.Add(this.label121);
            this.groupBox1.Controls.Add(this.label117);
            this.groupBox1.Controls.Add(this.mgAttackPlus);
            this.groupBox1.Controls.Add(this.label120);
            this.groupBox1.Location = new System.Drawing.Point(3, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Initial Level-up Increments";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label124);
            this.groupBox2.Controls.Add(this.expNeeded);
            this.groupBox2.Location = new System.Drawing.Point(3, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Experience Needed to Level";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label116);
            this.groupBox3.Controls.Add(this.label137);
            this.groupBox3.Controls.Add(this.label115);
            this.groupBox3.Controls.Add(this.hpPlusBonus);
            this.groupBox3.Controls.Add(this.mgAttackPlusBonus);
            this.groupBox3.Controls.Add(this.label113);
            this.groupBox3.Controls.Add(this.mgDefensePlusBonus);
            this.groupBox3.Controls.Add(this.defensePlusBonus);
            this.groupBox3.Controls.Add(this.label114);
            this.groupBox3.Controls.Add(this.attackPlusBonus);
            this.groupBox3.Location = new System.Drawing.Point(3, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 131);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bonus Level-up Increments";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.levelUpSpellLearned);
            this.groupBox4.Location = new System.Drawing.Point(3, 383);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(160, 47);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Level-up Learned spell";
            // 
            // LevelUps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 433);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.levelNum);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LevelUps";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.hpPlusBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensePlusBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackPlusBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgDefensePlusBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgAttackPlusBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hpPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgAttackPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensePlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mgDefensePlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expNeeded)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ComboBox levelUpSpellLearned;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.NumericUpDown hpPlusBonus;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.NumericUpDown defensePlusBonus;
        private System.Windows.Forms.NumericUpDown attackPlusBonus;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.NumericUpDown mgDefensePlusBonus;
        private System.Windows.Forms.NumericUpDown mgAttackPlusBonus;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.NumericUpDown hpPlus;
        private System.Windows.Forms.NumericUpDown attackPlus;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.NumericUpDown mgAttackPlus;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.NumericUpDown defensePlus;
        private System.Windows.Forms.NumericUpDown mgDefensePlus;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.NumericUpDown expNeeded;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.NumericUpDown levelNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripButton calculatorButton;
    }
}