
namespace LAZYSHELL
{
    partial class NewExceptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewExceptionForm));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.LinkLabel();
            this.ignoreError = new System.Windows.Forms.Button();
            this.copyContents = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 155);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(476, 259);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(372, 41);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(116, 23);
            this.close.TabIndex = 2;
            this.close.Text = "Close application";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 108);
            this.label1.TabIndex = 0;
            this.label1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label1_LinkClicked);
            // 
            // ignoreError
            // 
            this.ignoreError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ignoreError.Location = new System.Drawing.Point(372, 12);
            this.ignoreError.Name = "ignoreError";
            this.ignoreError.Size = new System.Drawing.Size(116, 23);
            this.ignoreError.TabIndex = 1;
            this.ignoreError.Text = "Ignore error";
            this.ignoreError.UseVisualStyleBackColor = true;
            this.ignoreError.Click += new System.EventHandler(this.ignoreError_Click);
            // 
            // copyContents
            // 
            this.copyContents.Location = new System.Drawing.Point(12, 126);
            this.copyContents.Name = "copyContents";
            this.copyContents.Size = new System.Drawing.Size(195, 23);
            this.copyContents.TabIndex = 3;
            this.copyContents.Text = "Copy contents to clipboard";
            this.copyContents.UseVisualStyleBackColor = true;
            this.copyContents.Click += new System.EventHandler(this.copyContents_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // NewExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 426);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ignoreError);
            this.Controls.Add(this.copyContents);
            this.Controls.Add(this.close);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewExceptionForm";
            this.Text = "ERROR - LAZYSHELL++";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.LinkLabel label1;
        private System.Windows.Forms.Button ignoreError;
        private System.Windows.Forms.Button copyContents;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}