
namespace LAZYSHELL
{
    partial class HackingTools
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.percentControl = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.selectAll = new System.Windows.Forms.Button();
            this.deselectAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.adjustAll = new System.Windows.Forms.RadioButton();
            this.adjustCurrent = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.percentControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(87, 286);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(6, 286);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "Apply";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "HP",
            "FP",
            "Attack",
            "Defense",
            "Mg.Attack",
            "Mg.Defense",
            "Evade",
            "Mg.Evade",
            "Experience",
            "Coins"});
            this.checkedListBox1.Location = new System.Drawing.Point(6, 60);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(156, 164);
            this.checkedListBox1.TabIndex = 2;
            // 
            // percentControl
            // 
            this.percentControl.Location = new System.Drawing.Point(56, 259);
            this.percentControl.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.percentControl.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.percentControl.Name = "percentControl";
            this.percentControl.Size = new System.Drawing.Size(106, 21);
            this.percentControl.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Percent";
            // 
            // selectAll
            // 
            this.selectAll.Location = new System.Drawing.Point(6, 230);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(75, 23);
            this.selectAll.TabIndex = 3;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // deselectAll
            // 
            this.deselectAll.Location = new System.Drawing.Point(87, 230);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(75, 23);
            this.deselectAll.TabIndex = 4;
            this.deselectAll.Text = "Deselect All";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.adjustAll);
            this.groupBox1.Controls.Add(this.adjustCurrent);
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonOK);
            this.groupBox1.Controls.Add(this.percentControl);
            this.groupBox1.Controls.Add(this.selectAll);
            this.groupBox1.Controls.Add(this.deselectAll);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adjust all monster stats";
            // 
            // adjustAll
            // 
            this.adjustAll.AutoSize = true;
            this.adjustAll.Location = new System.Drawing.Point(9, 37);
            this.adjustAll.Name = "adjustAll";
            this.adjustAll.Size = new System.Drawing.Size(81, 17);
            this.adjustAll.TabIndex = 1;
            this.adjustAll.Text = "All monsters";
            this.adjustAll.UseVisualStyleBackColor = true;
            // 
            // adjustCurrent
            // 
            this.adjustCurrent.AutoSize = true;
            this.adjustCurrent.Checked = true;
            this.adjustCurrent.Location = new System.Drawing.Point(9, 20);
            this.adjustCurrent.Name = "adjustCurrent";
            this.adjustCurrent.Size = new System.Drawing.Size(99, 17);
            this.adjustCurrent.TabIndex = 0;
            this.adjustCurrent.TabStop = true;
            this.adjustCurrent.Text = "Current monster";
            this.adjustCurrent.UseVisualStyleBackColor = true;
            // 
            // HackingTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(192, 339);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HackingTools";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "HACKING TOOLS";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HackingTools_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.percentControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.NumericUpDown percentControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button deselectAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton adjustAll;
        private System.Windows.Forms.RadioButton adjustCurrent;
    }
}