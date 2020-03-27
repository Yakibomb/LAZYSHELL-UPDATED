
namespace LAZYSHELL
{
    partial class EditLabel
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
            this.labelText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.Location = new System.Drawing.Point(0, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(400, 21);
            this.labelText.TabIndex = 0;
            this.labelText.TextChanged += new System.EventHandler(this.labelText_TextChanged);
            this.labelText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditLabel_KeyDown);
            // 
            // EditLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 23);
            this.Controls.Add(this.labelText);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditLabel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EDIT LABEL";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditLabel_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditLabel_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.TextBox labelText;
    }
}