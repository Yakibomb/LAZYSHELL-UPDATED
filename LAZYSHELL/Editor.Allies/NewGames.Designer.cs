
namespace LAZYSHELL
{
    partial class NewGames
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
            this.label92 = new System.Windows.Forms.Label();
            this.startingEquipment = new System.Windows.Forms.ComboBox();
            this.startingSpecialItem = new System.Windows.Forms.ComboBox();
            this.startingItem = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.slotNum = new System.Windows.Forms.NumericUpDown();
            this.label163 = new System.Windows.Forms.Label();
            this.startingMaximumFP = new System.Windows.Forms.NumericUpDown();
            this.startingCurrentFP = new System.Windows.Forms.NumericUpDown();
            this.startingFrogCoins = new System.Windows.Forms.NumericUpDown();
            this.startingCoins = new System.Windows.Forms.NumericUpDown();
            this.label165 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lvl2TimingEnd = new System.Windows.Forms.NumericUpDown();
            this.lvl2TimingStart = new System.Windows.Forms.NumericUpDown();
            this.label157 = new System.Windows.Forms.Label();
            this.lvl1TimingStart = new System.Windows.Forms.NumericUpDown();
            this.label159 = new System.Windows.Forms.Label();
            this.lvl1TimingEnd = new System.Windows.Forms.NumericUpDown();
            this.label160 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.slotNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMaximumFP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCurrentFP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingFrogCoins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCoins)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvl2TimingEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl2TimingStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl1TimingStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl1TimingEnd)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(9, 9);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(87, 13);
            this.label92.TabIndex = 3;
            this.label92.Text = "Inventory Slot #";
            // 
            // startingEquipment
            // 
            this.startingEquipment.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingEquipment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingEquipment.DropDownHeight = 317;
            this.startingEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingEquipment.DropDownWidth = 150;
            this.startingEquipment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingEquipment.IntegralHeight = false;
            this.startingEquipment.ItemHeight = 15;
            this.startingEquipment.Location = new System.Drawing.Point(80, 64);
            this.startingEquipment.Name = "startingEquipment";
            this.startingEquipment.Size = new System.Drawing.Size(135, 21);
            this.startingEquipment.TabIndex = 5;
            this.startingEquipment.Tag = "";
            this.startingEquipment.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingEquipment.SelectedIndexChanged += new System.EventHandler(this.startingEquipment_SelectedIndexChanged);
            // 
            // startingSpecialItem
            // 
            this.startingSpecialItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingSpecialItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingSpecialItem.DropDownHeight = 317;
            this.startingSpecialItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingSpecialItem.DropDownWidth = 150;
            this.startingSpecialItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingSpecialItem.IntegralHeight = false;
            this.startingSpecialItem.ItemHeight = 15;
            this.startingSpecialItem.Location = new System.Drawing.Point(80, 43);
            this.startingSpecialItem.Name = "startingSpecialItem";
            this.startingSpecialItem.Size = new System.Drawing.Size(135, 21);
            this.startingSpecialItem.TabIndex = 3;
            this.startingSpecialItem.Tag = "";
            this.startingSpecialItem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingSpecialItem.SelectedIndexChanged += new System.EventHandler(this.startingSpecialItem_SelectedIndexChanged);
            // 
            // startingItem
            // 
            this.startingItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.startingItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.startingItem.DropDownHeight = 317;
            this.startingItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingItem.DropDownWidth = 150;
            this.startingItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingItem.IntegralHeight = false;
            this.startingItem.ItemHeight = 15;
            this.startingItem.Location = new System.Drawing.Point(80, 22);
            this.startingItem.Name = "startingItem";
            this.startingItem.Size = new System.Drawing.Size(135, 21);
            this.startingItem.TabIndex = 1;
            this.startingItem.Tag = "";
            this.startingItem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.itemName_DrawItem);
            this.startingItem.SelectedIndexChanged += new System.EventHandler(this.startingItem_SelectedIndexChanged);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(6, 25);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(29, 13);
            this.label84.TabIndex = 0;
            this.label84.Text = "Item";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 46);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(68, 13);
            this.label88.TabIndex = 2;
            this.label88.Text = "Special Item ";
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(6, 67);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(57, 13);
            this.label162.TabIndex = 4;
            this.label162.Text = "Equipment";
            // 
            // slotNum
            // 
            this.slotNum.Location = new System.Drawing.Point(160, 7);
            this.slotNum.Maximum = new decimal(new int[] {
            29,
            0,
            0,
            0});
            this.slotNum.Name = "slotNum";
            this.slotNum.Size = new System.Drawing.Size(58, 21);
            this.slotNum.TabIndex = 4;
            this.slotNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.slotNum.ValueChanged += new System.EventHandler(this.slotNum_ValueChanged);
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(6, 23);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(33, 13);
            this.label163.TabIndex = 0;
            this.label163.Text = "Coins";
            // 
            // startingMaximumFP
            // 
            this.startingMaximumFP.Location = new System.Drawing.Point(77, 84);
            this.startingMaximumFP.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.startingMaximumFP.Name = "startingMaximumFP";
            this.startingMaximumFP.Size = new System.Drawing.Size(46, 21);
            this.startingMaximumFP.TabIndex = 6;
            this.startingMaximumFP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingMaximumFP.ValueChanged += new System.EventHandler(this.startingMaximumFP_ValueChanged);
            // 
            // startingCurrentFP
            // 
            this.startingCurrentFP.Location = new System.Drawing.Point(31, 84);
            this.startingCurrentFP.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.startingCurrentFP.Name = "startingCurrentFP";
            this.startingCurrentFP.Size = new System.Drawing.Size(46, 21);
            this.startingCurrentFP.TabIndex = 5;
            this.startingCurrentFP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingCurrentFP.ValueChanged += new System.EventHandler(this.startingCurrentFP_ValueChanged);
            // 
            // startingFrogCoins
            // 
            this.startingFrogCoins.Location = new System.Drawing.Point(77, 41);
            this.startingFrogCoins.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.startingFrogCoins.Name = "startingFrogCoins";
            this.startingFrogCoins.Size = new System.Drawing.Size(46, 21);
            this.startingFrogCoins.TabIndex = 3;
            this.startingFrogCoins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingFrogCoins.ValueChanged += new System.EventHandler(this.startingFrogCoins_ValueChanged);
            // 
            // startingCoins
            // 
            this.startingCoins.Location = new System.Drawing.Point(77, 20);
            this.startingCoins.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.startingCoins.Name = "startingCoins";
            this.startingCoins.Size = new System.Drawing.Size(46, 21);
            this.startingCoins.TabIndex = 1;
            this.startingCoins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.startingCoins.ValueChanged += new System.EventHandler(this.startingCoins_ValueChanged);
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(6, 86);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(19, 13);
            this.label165.TabIndex = 4;
            this.label165.Text = "FP";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Location = new System.Drawing.Point(6, 44);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(58, 13);
            this.label164.TabIndex = 2;
            this.label164.Text = "Frog Coins";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startingMaximumFP);
            this.groupBox1.Controls.Add(this.label163);
            this.groupBox1.Controls.Add(this.startingCurrentFP);
            this.groupBox1.Controls.Add(this.label164);
            this.groupBox1.Controls.Add(this.startingFrogCoins);
            this.groupBox1.Controls.Add(this.label165);
            this.groupBox1.Controls.Add(this.startingCoins);
            this.groupBox1.Location = new System.Drawing.Point(230, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 111);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Game Status";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.startingEquipment);
            this.groupBox5.Controls.Add(this.startingItem);
            this.groupBox5.Controls.Add(this.label88);
            this.groupBox5.Controls.Add(this.startingSpecialItem);
            this.groupBox5.Controls.Add(this.label162);
            this.groupBox5.Controls.Add(this.label84);
            this.groupBox5.Location = new System.Drawing.Point(3, 32);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(221, 111);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Inventory Slot Items";
            // 
            // lvl2TimingEnd
            // 
            this.lvl2TimingEnd.Location = new System.Drawing.Point(84, 63);
            this.lvl2TimingEnd.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.lvl2TimingEnd.Name = "lvl2TimingEnd";
            this.lvl2TimingEnd.Size = new System.Drawing.Size(45, 21);
            this.lvl2TimingEnd.TabIndex = 5;
            this.lvl2TimingEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lvl2TimingEnd.ValueChanged += new System.EventHandler(this.lvl2TimingEnd_ValueChanged);
            // 
            // lvl2TimingStart
            // 
            this.lvl2TimingStart.Location = new System.Drawing.Point(84, 42);
            this.lvl2TimingStart.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.lvl2TimingStart.Name = "lvl2TimingStart";
            this.lvl2TimingStart.Size = new System.Drawing.Size(45, 21);
            this.lvl2TimingStart.TabIndex = 3;
            this.lvl2TimingStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lvl2TimingStart.ValueChanged += new System.EventHandler(this.lvl2TimingStart_ValueChanged);
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(6, 86);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(67, 13);
            this.label157.TabIndex = 6;
            this.label157.Text = "1/2 dmg end";
            // 
            // lvl1TimingStart
            // 
            this.lvl1TimingStart.Location = new System.Drawing.Point(84, 21);
            this.lvl1TimingStart.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.lvl1TimingStart.Name = "lvl1TimingStart";
            this.lvl1TimingStart.Size = new System.Drawing.Size(45, 21);
            this.lvl1TimingStart.TabIndex = 1;
            this.lvl1TimingStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lvl1TimingStart.ValueChanged += new System.EventHandler(this.lvl1TimingStart_ValueChanged);
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(6, 65);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(57, 13);
            this.label159.TabIndex = 4;
            this.label159.Text = "0 dmg end";
            // 
            // lvl1TimingEnd
            // 
            this.lvl1TimingEnd.Location = new System.Drawing.Point(84, 84);
            this.lvl1TimingEnd.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.lvl1TimingEnd.Name = "lvl1TimingEnd";
            this.lvl1TimingEnd.Size = new System.Drawing.Size(45, 21);
            this.lvl1TimingEnd.TabIndex = 7;
            this.lvl1TimingEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lvl1TimingEnd.ValueChanged += new System.EventHandler(this.lvl1TimingEnd_ValueChanged);
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(6, 44);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(62, 13);
            this.label160.TabIndex = 2;
            this.label160.Text = "0 dmg start";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(6, 23);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(72, 13);
            this.label158.TabIndex = 0;
            this.label158.Text = "1/2 dmg start";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label158);
            this.groupBox2.Controls.Add(this.label160);
            this.groupBox2.Controls.Add(this.lvl1TimingEnd);
            this.groupBox2.Controls.Add(this.label159);
            this.groupBox2.Controls.Add(this.lvl1TimingStart);
            this.groupBox2.Controls.Add(this.label157);
            this.groupBox2.Controls.Add(this.lvl2TimingStart);
            this.groupBox2.Controls.Add(this.lvl2TimingEnd);
            this.groupBox2.Location = new System.Drawing.Point(365, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 111);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Defense Timing";
            // 
            // NewGames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 151);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label92);
            this.Controls.Add(this.slotNum);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGames";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.slotNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingMaximumFP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCurrentFP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingFrogCoins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startingCoins)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvl2TimingEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl2TimingStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl1TimingStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvl1TimingEnd)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.ComboBox startingEquipment;
        private System.Windows.Forms.ComboBox startingSpecialItem;
        private System.Windows.Forms.ComboBox startingItem;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.NumericUpDown slotNum;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.NumericUpDown startingMaximumFP;
        private System.Windows.Forms.NumericUpDown startingCurrentFP;
        private System.Windows.Forms.NumericUpDown startingFrogCoins;
        private System.Windows.Forms.NumericUpDown startingCoins;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown lvl2TimingEnd;
        private System.Windows.Forms.NumericUpDown lvl2TimingStart;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.NumericUpDown lvl1TimingStart;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.NumericUpDown lvl1TimingEnd;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}