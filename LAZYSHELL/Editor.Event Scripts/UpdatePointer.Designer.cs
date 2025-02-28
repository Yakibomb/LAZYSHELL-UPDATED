
namespace LAZYSHELL
{
    partial class UpdatePointer
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
            this.fromIndex = new System.Windows.Forms.NumericUpDown();
            this.toIndex = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pointerUpdateNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.scriptChoices = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fromIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointerUpdateNum)).BeginInit();
            this.SuspendLayout();
            // 
            // fromIndex
            // 
            this.fromIndex.Enabled = false;
            this.fromIndex.Location = new System.Drawing.Point(48, 96);
            this.fromIndex.Name = "fromIndex";
            this.fromIndex.Size = new System.Drawing.Size(79, 21);
            this.fromIndex.TabIndex = 3;
            this.fromIndex.ValueChanged += new System.EventHandler(this.fromDialogue_ValueChanged);
            // 
            // toIndex
            // 
            this.toIndex.Enabled = false;
            this.toIndex.Location = new System.Drawing.Point(155, 96);
            this.toIndex.Name = "toIndex";
            this.toIndex.Size = new System.Drawing.Size(80, 21);
            this.toIndex.TabIndex = 5;
            this.toIndex.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.toIndex.ValueChanged += new System.EventHandler(this.toDialogue_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "to";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(79, 123);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(159, 123);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 56);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(98, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Update current";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 73);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(122, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Update within range";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // pointerUpdateNum
            // 
            this.pointerUpdateNum.Location = new System.Drawing.Point(161, 33);
            this.pointerUpdateNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.pointerUpdateNum.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.pointerUpdateNum.Name = "pointerUpdateNum";
            this.pointerUpdateNum.Size = new System.Drawing.Size(73, 21);
            this.pointerUpdateNum.TabIndex = 8;
            this.pointerUpdateNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Adjust Offset by how much?";
            // 
            // scriptChoices
            // 
            this.scriptChoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scriptChoices.Items.AddRange(new object[] {
            "Event",
            "Action"});
            this.scriptChoices.Location = new System.Drawing.Point(161, 11);
            this.scriptChoices.Name = "scriptChoices";
            this.scriptChoices.Size = new System.Drawing.Size(73, 21);
            this.scriptChoices.TabIndex = 21;
            this.scriptChoices.SelectedIndexChanged += new System.EventHandler(this.scriptChoices_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Script Type";
            // 
            // UpdatePointer
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(247, 156);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.scriptChoices);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pointerUpdateNum);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toIndex);
            this.Controls.Add(this.fromIndex);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UpdatePointer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MANUAL POINTER UPDATER";
            ((System.ComponentModel.ISupportInitialize)(this.fromIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointerUpdateNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.NumericUpDown fromIndex;
        private System.Windows.Forms.NumericUpDown toIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.NumericUpDown pointerUpdateNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox scriptChoices;
        private System.Windows.Forms.Label label4;
    }
}