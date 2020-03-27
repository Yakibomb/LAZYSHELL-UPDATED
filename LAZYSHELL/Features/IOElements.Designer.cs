
namespace LAZYSHELL
{
    partial class IOElements
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
            this.radioButtonCurrent = new System.Windows.Forms.RadioButton();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.browseCurrent = new System.Windows.Forms.Button();
            this.browseAll = new System.Windows.Forms.Button();
            this.textBoxAll = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // radioButtonCurrent
            // 
            this.radioButtonCurrent.Location = new System.Drawing.Point(12, 12);
            this.radioButtonCurrent.Name = "radioButtonCurrent";
            this.radioButtonCurrent.Size = new System.Drawing.Size(445, 17);
            this.radioButtonCurrent.TabIndex = 0;
            this.radioButtonCurrent.TabStop = true;
            this.radioButtonCurrent.Text = "Current index as single file...";
            this.radioButtonCurrent.UseVisualStyleBackColor = true;
            this.radioButtonCurrent.CheckedChanged += new System.EventHandler(this.radioButtonCurrent_CheckedChanged);
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.Location = new System.Drawing.Point(12, 64);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(445, 17);
            this.radioButtonAll.TabIndex = 3;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "All indexes as directory of files...";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(301, 113);
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
            this.buttonCancel.Location = new System.Drawing.Point(382, 113);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.Location = new System.Drawing.Point(12, 35);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.ReadOnly = true;
            this.textBoxCurrent.Size = new System.Drawing.Size(412, 21);
            this.textBoxCurrent.TabIndex = 2;
            // 
            // browseCurrent
            // 
            this.browseCurrent.Location = new System.Drawing.Point(430, 35);
            this.browseCurrent.Name = "browseCurrent";
            this.browseCurrent.Size = new System.Drawing.Size(27, 23);
            this.browseCurrent.TabIndex = 1;
            this.browseCurrent.Text = "...";
            this.browseCurrent.UseVisualStyleBackColor = true;
            this.browseCurrent.Click += new System.EventHandler(this.browseCurrent_Click);
            // 
            // browseAll
            // 
            this.browseAll.Enabled = false;
            this.browseAll.Location = new System.Drawing.Point(430, 87);
            this.browseAll.Name = "browseAll";
            this.browseAll.Size = new System.Drawing.Size(27, 23);
            this.browseAll.TabIndex = 4;
            this.browseAll.Text = "...";
            this.browseAll.UseVisualStyleBackColor = true;
            this.browseAll.Click += new System.EventHandler(this.browseAll_Click);
            // 
            // textBoxAll
            // 
            this.textBoxAll.Enabled = false;
            this.textBoxAll.Location = new System.Drawing.Point(12, 87);
            this.textBoxAll.Name = "textBoxAll";
            this.textBoxAll.ReadOnly = true;
            this.textBoxAll.Size = new System.Drawing.Size(412, 21);
            this.textBoxAll.TabIndex = 5;
            // 
            // IOElements
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(469, 148);
            this.Controls.Add(this.browseAll);
            this.Controls.Add(this.textBoxAll);
            this.Controls.Add(this.browseCurrent);
            this.Controls.Add(this.textBoxCurrent);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonAll);
            this.Controls.Add(this.radioButtonCurrent);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IOElements";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.RadioButton radioButtonCurrent;
        private System.Windows.Forms.RadioButton radioButtonAll;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.Button browseCurrent;
        private System.Windows.Forms.Button browseAll;
        private System.Windows.Forms.TextBox textBoxAll;
    }
}