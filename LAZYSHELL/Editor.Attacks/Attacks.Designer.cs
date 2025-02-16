
namespace LAZYSHELL
{
    partial class Attacks
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.attackName = new LAZYSHELL.ToolStripComboBox();
            this.attackNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.attackAtkType = new System.Windows.Forms.CheckedListBox();
            this.attackStatusUp = new System.Windows.Forms.CheckedListBox();
            this.attackStatusEffect = new System.Windows.Forms.CheckedListBox();
            this.attackAtkLevel = new System.Windows.Forms.NumericUpDown();
            this.attackHitRate = new System.Windows.Forms.NumericUpDown();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.textBoxAttackName = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attackAtkLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackHitRate)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attackName,
            this.attackNum});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(202, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // attackName
            // 
            this.attackName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.attackName.ContextMenuStrip = null;
            this.attackName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.attackName.DropDownHeight = 497;
            this.attackName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attackName.DropDownWidth = 146;
            this.attackName.ItemHeight = 15;
            this.attackName.Location = new System.Drawing.Point(9, 1);
            this.attackName.Name = "attackName";
            this.attackName.SelectedIndex = -1;
            this.attackName.SelectedItem = null;
            this.attackName.Size = new System.Drawing.Size(146, 22);
            this.attackName.SelectedIndexChanged += new System.EventHandler(this.attackName_SelectedIndexChanged);
            this.attackName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.attackName_DrawItem);
            // 
            // attackNum
            // 
            this.attackNum.ContextMenuStrip = null;
            this.attackNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attackNum.Hexadecimal = false;
            this.attackNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attackNum.Location = new System.Drawing.Point(155, 1);
            this.attackNum.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.attackNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.attackNum.Name = "attackNum";
            this.attackNum.Size = new System.Drawing.Size(41, 22);
            this.attackNum.Text = "0";
            this.attackNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.attackNum.ValueChanged += new System.EventHandler(this.attackNum_ValueChanged);
            // 
            // attackAtkType
            // 
            this.attackAtkType.CheckOnClick = true;
            this.attackAtkType.Items.AddRange(new object[] {
            "9999 Damage",
            "No damage",
            "Hide Battle Numerals",
            "No damage"});
            this.attackAtkType.Location = new System.Drawing.Point(6, 20);
            this.attackAtkType.Name = "attackAtkType";
            this.attackAtkType.Size = new System.Drawing.Size(184, 68);
            this.attackAtkType.TabIndex = 0;
            this.attackAtkType.SelectedIndexChanged += new System.EventHandler(this.attackAtkType_SelectedIndexChanged);
            // 
            // attackStatusUp
            // 
            this.attackStatusUp.CheckOnClick = true;
            this.attackStatusUp.Items.AddRange(new object[] {
            "Attack",
            "Defense",
            "Magic Attack",
            "Magic Defense"});
            this.attackStatusUp.Location = new System.Drawing.Point(6, 20);
            this.attackStatusUp.Name = "attackStatusUp";
            this.attackStatusUp.Size = new System.Drawing.Size(184, 68);
            this.attackStatusUp.TabIndex = 0;
            this.attackStatusUp.SelectedIndexChanged += new System.EventHandler(this.attackStatusUp_SelectedIndexChanged);
            // 
            // attackStatusEffect
            // 
            this.attackStatusEffect.CheckOnClick = true;
            this.attackStatusEffect.ColumnWidth = 97;
            this.attackStatusEffect.Items.AddRange(new object[] {
            "Mute",
            "Sleep",
            "Poison",
            "Fear",
            "Berserk",
            "Mushroom",
            "Scarecrow",
            "Invincible"});
            this.attackStatusEffect.Location = new System.Drawing.Point(6, 20);
            this.attackStatusEffect.Name = "attackStatusEffect";
            this.attackStatusEffect.Size = new System.Drawing.Size(184, 132);
            this.attackStatusEffect.TabIndex = 0;
            this.attackStatusEffect.SelectedIndexChanged += new System.EventHandler(this.attackStatusEffect_SelectedIndexChanged);
            // 
            // attackAtkLevel
            // 
            this.attackAtkLevel.Location = new System.Drawing.Point(81, 42);
            this.attackAtkLevel.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.attackAtkLevel.Name = "attackAtkLevel";
            this.attackAtkLevel.Size = new System.Drawing.Size(109, 21);
            this.attackAtkLevel.TabIndex = 3;
            this.attackAtkLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackAtkLevel.ValueChanged += new System.EventHandler(this.attackAtkLevel_ValueChanged);
            // 
            // attackHitRate
            // 
            this.attackHitRate.Location = new System.Drawing.Point(81, 21);
            this.attackHitRate.Name = "attackHitRate";
            this.attackHitRate.Size = new System.Drawing.Size(109, 21);
            this.attackHitRate.TabIndex = 1;
            this.attackHitRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.attackHitRate.ValueChanged += new System.EventHandler(this.attackHitRate_ValueChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(9, 44);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(66, 13);
            this.label57.TabIndex = 2;
            this.label57.Text = "Attack Level";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(9, 23);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(57, 13);
            this.label58.TabIndex = 0;
            this.label58.Text = "Hit Rate%";
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxAttackName});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(202, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // textBoxAttackName
            // 
            this.textBoxAttackName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxAttackName.MaxLength = 13;
            this.textBoxAttackName.Name = "textBoxAttackName";
            this.textBoxAttackName.Size = new System.Drawing.Size(144, 25);
            this.textBoxAttackName.TextChanged += new System.EventHandler(this.textBoxAttackName_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Controls.Add(this.attackAtkLevel);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.attackHitRate);
            this.groupBox1.Location = new System.Drawing.Point(3, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attack Power";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.attackStatusEffect);
            this.groupBox2.Location = new System.Drawing.Point(3, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 157);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Effect Inflict";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.attackStatusUp);
            this.groupBox3.Location = new System.Drawing.Point(3, 291);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 94);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status Up";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.attackAtkType);
            this.groupBox4.Location = new System.Drawing.Point(3, 389);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(196, 95);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Attack Type";
            // 
            // Attacks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 488);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Attacks";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attackAtkLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attackHitRate)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private LAZYSHELL.ToolStripComboBox attackName;
        private System.Windows.Forms.CheckedListBox attackAtkType;
        private System.Windows.Forms.CheckedListBox attackStatusUp;
        private System.Windows.Forms.CheckedListBox attackStatusEffect;
        private System.Windows.Forms.NumericUpDown attackAtkLevel;
        private System.Windows.Forms.NumericUpDown attackHitRate;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private ToolStripNumericUpDown attackNum;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripTextBox textBoxAttackName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}