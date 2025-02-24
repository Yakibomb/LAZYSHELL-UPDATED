
namespace LAZYSHELL
{
    partial class Characters
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
            this.label188 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.startingMgAttack = new System.Windows.Forms.NumericUpDown();
            this.startingMgDefense = new System.Windows.Forms.NumericUpDown();
            this.startingExperience = new System.Windows.Forms.NumericUpDown();
            this.startingDefense = new System.Windows.Forms.NumericUpDown();
            this.label128 = new System.Windows.Forms.Label();
            this.startingCurrentHP = new System.Windows.Forms.NumericUpDown();
            this.startingAttack = new System.Windows.Forms.NumericUpDown();
            this.startingMaximumHP = new System.Windows.Forms.NumericUpDown();
            this.startingLevel = new System.Windows.Forms.NumericUpDown();
            this.label131 = new System.Windows.Forms.Label();
            this.startingWeapon = new System.Windows.Forms.ComboBox();
            this.label133 = new System.Windows.Forms.Label();
            this.startingAccessory = new System.Windows.Forms.ComboBox();
            this.startingArmor = new System.Windows.Forms.ComboBox();
            this.startingSpeed = new System.Windows.Forms.NumericUpDown();
            this.label138 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.textBoxCharacterName = new System.Windows.Forms.ToolStripTextBox();
            this.startingMagic = new LAZYSHELL.NewCheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.startingMgAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMgDefense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingDefense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCurrentHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMaximumHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingSpeed)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Location = new System.Drawing.Point(6, 22);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(32, 13);
            this.label188.TabIndex = 0;
            this.label188.Text = "Level";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(6, 232);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(60, 13);
            this.label132.TabIndex = 21;
            this.label132.Text = "Experience";
            // 
            // startingMgAttack
            // 
            this.startingMgAttack.Location = new System.Drawing.Point(98, 104);
            this.startingMgAttack.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startingMgAttack.Name = "startingMgAttack";
            this.startingMgAttack.Size = new System.Drawing.Size(117, 21);
            this.startingMgAttack.TabIndex = 10;
            this.startingMgAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingMgAttack.ValueChanged += new System.EventHandler(this.startingMgAttack_ValueChanged);
            // 
            // startingMgDefense
            // 
            this.startingMgDefense.Location = new System.Drawing.Point(98, 125);
            this.startingMgDefense.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startingMgDefense.Name = "startingMgDefense";
            this.startingMgDefense.Size = new System.Drawing.Size(117, 21);
            this.startingMgDefense.TabIndex = 12;
            this.startingMgDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingMgDefense.ValueChanged += new System.EventHandler(this.startingMgDefense_ValueChanged);
            // 
            // startingExperience
            // 
            this.startingExperience.Location = new System.Drawing.Point(98, 230);
            this.startingExperience.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.startingExperience.Name = "startingExperience";
            this.startingExperience.Size = new System.Drawing.Size(117, 21);
            this.startingExperience.TabIndex = 22;
            this.startingExperience.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingExperience.ValueChanged += new System.EventHandler(this.startingExperience_ValueChanged);
            // 
            // startingDefense
            // 
            this.startingDefense.Location = new System.Drawing.Point(98, 83);
            this.startingDefense.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startingDefense.Name = "startingDefense";
            this.startingDefense.Size = new System.Drawing.Size(117, 21);
            this.startingDefense.TabIndex = 8;
            this.startingDefense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingDefense.ValueChanged += new System.EventHandler(this.startingDefense_ValueChanged);
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(6, 43);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(20, 13);
            this.label128.TabIndex = 2;
            this.label128.Text = "HP";
            // 
            // startingCurrentHP
            // 
            this.startingCurrentHP.Location = new System.Drawing.Point(98, 41);
            this.startingCurrentHP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.startingCurrentHP.Name = "startingCurrentHP";
            this.startingCurrentHP.Size = new System.Drawing.Size(58, 21);
            this.startingCurrentHP.TabIndex = 3;
            this.startingCurrentHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingCurrentHP.ValueChanged += new System.EventHandler(this.startingCurrentHP_ValueChanged);
            // 
            // startingAttack
            // 
            this.startingAttack.Location = new System.Drawing.Point(98, 62);
            this.startingAttack.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startingAttack.Name = "startingAttack";
            this.startingAttack.Size = new System.Drawing.Size(117, 21);
            this.startingAttack.TabIndex = 6;
            this.startingAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingAttack.ValueChanged += new System.EventHandler(this.startingAttack_ValueChanged);
            // 
            // startingMaximumHP
            // 
            this.startingMaximumHP.Location = new System.Drawing.Point(156, 41);
            this.startingMaximumHP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.startingMaximumHP.Name = "startingMaximumHP";
            this.startingMaximumHP.Size = new System.Drawing.Size(59, 21);
            this.startingMaximumHP.TabIndex = 4;
            this.startingMaximumHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingMaximumHP.ValueChanged += new System.EventHandler(this.startingMaximumHP_ValueChanged);
            // 
            // startingLevel
            // 
            this.startingLevel.Location = new System.Drawing.Point(98, 20);
            this.startingLevel.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.startingLevel.Name = "startingLevel";
            this.startingLevel.Size = new System.Drawing.Size(117, 21);
            this.startingLevel.TabIndex = 1;
            this.startingLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingLevel.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.startingLevel.ValueChanged += new System.EventHandler(this.startingLevel_ValueChanged);
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(6, 148);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(37, 13);
            this.label131.TabIndex = 13;
            this.label131.Text = "Speed";
            // 
            // startingWeapon
            // 
            this.startingWeapon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingWeapon.DropDownHeight = 317;
            this.startingWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingWeapon.DropDownWidth = 150;
            this.startingWeapon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingWeapon.IntegralHeight = false;
            this.startingWeapon.ItemHeight = 15;
            this.startingWeapon.Location = new System.Drawing.Point(98, 167);
            this.startingWeapon.Name = "startingWeapon";
            this.startingWeapon.Size = new System.Drawing.Size(117, 21);
            this.startingWeapon.TabIndex = 16;
            this.startingWeapon.Tag = "";
            this.startingWeapon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingWeapon.SelectedIndexChanged += new System.EventHandler(this.startingWeapon_SelectedIndexChanged);
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(6, 211);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(56, 13);
            this.label133.TabIndex = 19;
            this.label133.Text = "Accessory";
            // 
            // startingAccessory
            // 
            this.startingAccessory.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingAccessory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingAccessory.DropDownHeight = 317;
            this.startingAccessory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingAccessory.DropDownWidth = 150;
            this.startingAccessory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingAccessory.IntegralHeight = false;
            this.startingAccessory.ItemHeight = 15;
            this.startingAccessory.Location = new System.Drawing.Point(98, 209);
            this.startingAccessory.Name = "startingAccessory";
            this.startingAccessory.Size = new System.Drawing.Size(117, 21);
            this.startingAccessory.TabIndex = 20;
            this.startingAccessory.Tag = "";
            this.startingAccessory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingAccessory.SelectedIndexChanged += new System.EventHandler(this.startingAccessory_SelectedIndexChanged);
            // 
            // startingArmor
            // 
            this.startingArmor.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingArmor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingArmor.DropDownHeight = 317;
            this.startingArmor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingArmor.DropDownWidth = 150;
            this.startingArmor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingArmor.IntegralHeight = false;
            this.startingArmor.ItemHeight = 15;
            this.startingArmor.Location = new System.Drawing.Point(98, 188);
            this.startingArmor.Name = "startingArmor";
            this.startingArmor.Size = new System.Drawing.Size(117, 21);
            this.startingArmor.TabIndex = 18;
            this.startingArmor.Tag = "";
            this.startingArmor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingArmor.SelectedIndexChanged += new System.EventHandler(this.startingArmor_SelectedIndexChanged);
            // 
            // startingSpeed
            // 
            this.startingSpeed.Location = new System.Drawing.Point(98, 146);
            this.startingSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startingSpeed.Name = "startingSpeed";
            this.startingSpeed.Size = new System.Drawing.Size(117, 21);
            this.startingSpeed.TabIndex = 14;
            this.startingSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingSpeed.ValueChanged += new System.EventHandler(this.startingSpeed_ValueChanged);
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(6, 106);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(68, 13);
            this.label138.TabIndex = 9;
            this.label138.Text = "Magic Attack";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(6, 127);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(77, 13);
            this.label125.TabIndex = 11;
            this.label125.Text = "Magic Defense";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(6, 85);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(47, 13);
            this.label127.TabIndex = 7;
            this.label127.Text = "Defense";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(6, 64);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(38, 13);
            this.label90.TabIndex = 5;
            this.label90.Text = "Attack";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(6, 169);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(47, 13);
            this.label135.TabIndex = 15;
            this.label135.Text = "Weapon";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(6, 190);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(36, 13);
            this.label134.TabIndex = 17;
            this.label134.Text = "Armor";
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxCharacterName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(506, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxCharacterName.MaxLength = 10;
            this.textBoxCharacterName.Name = "textBoxCharacterName";
            this.textBoxCharacterName.Size = new System.Drawing.Size(190, 25);
            this.textBoxCharacterName.TextChanged += new System.EventHandler(this.textBoxCharacterName_TextChanged);
            // 
            // startingMagic
            // 
            this.startingMagic.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingMagic.CheckOnClick = true;
            this.startingMagic.ColumnWidth = 126;
            this.startingMagic.ForeColor = System.Drawing.SystemColors.Control;
            this.startingMagic.Location = new System.Drawing.Point(6, 20);
            this.startingMagic.MultiColumn = true;
            this.startingMagic.Name = "startingMagic";
            this.startingMagic.Size = new System.Drawing.Size(258, 260);
            this.startingMagic.TabIndex = 0;
            this.startingMagic.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.startingMagic_DrawItem);
            this.startingMagic.SelectedIndexChanged += new System.EventHandler(this.startingMagic_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.startingAccessory);
            this.groupBox3.Controls.Add(this.startingArmor);
            this.groupBox3.Controls.Add(this.startingWeapon);
            this.groupBox3.Controls.Add(this.label188);
            this.groupBox3.Controls.Add(this.label132);
            this.groupBox3.Controls.Add(this.label134);
            this.groupBox3.Controls.Add(this.startingMgAttack);
            this.groupBox3.Controls.Add(this.label135);
            this.groupBox3.Controls.Add(this.startingMgDefense);
            this.groupBox3.Controls.Add(this.label90);
            this.groupBox3.Controls.Add(this.startingExperience);
            this.groupBox3.Controls.Add(this.label127);
            this.groupBox3.Controls.Add(this.startingDefense);
            this.groupBox3.Controls.Add(this.label125);
            this.groupBox3.Controls.Add(this.label128);
            this.groupBox3.Controls.Add(this.label138);
            this.groupBox3.Controls.Add(this.startingCurrentHP);
            this.groupBox3.Controls.Add(this.startingSpeed);
            this.groupBox3.Controls.Add(this.startingAttack);
            this.groupBox3.Controls.Add(this.startingMaximumHP);
            this.groupBox3.Controls.Add(this.startingLevel);
            this.groupBox3.Controls.Add(this.label133);
            this.groupBox3.Controls.Add(this.label131);
            this.groupBox3.Location = new System.Drawing.Point(4, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 258);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ally Starting Status";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.startingMagic);
            this.groupBox4.Location = new System.Drawing.Point(231, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(270, 286);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ally Starting Spells";
            // 
            // Characters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 317);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Characters";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.startingMgAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMgDefense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingDefense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCurrentHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMaximumHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingSpeed)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.NumericUpDown startingMgAttack;
        private System.Windows.Forms.NumericUpDown startingMgDefense;
        private System.Windows.Forms.NumericUpDown startingExperience;
        private System.Windows.Forms.NumericUpDown startingDefense;
        private System.Windows.Forms.NumericUpDown startingAttack;
        private System.Windows.Forms.NumericUpDown startingLevel;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.ComboBox startingWeapon;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.NumericUpDown startingMaximumHP;
        private System.Windows.Forms.ComboBox startingAccessory;
        private System.Windows.Forms.ComboBox startingArmor;
        private System.Windows.Forms.NumericUpDown startingSpeed;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.NumericUpDown startingCurrentHP;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label134;
        private NewCheckedListBox startingMagic;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox textBoxCharacterName;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}