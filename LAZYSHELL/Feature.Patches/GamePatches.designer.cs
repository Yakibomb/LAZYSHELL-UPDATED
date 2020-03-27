
namespace LAZYSHELL.Patches
{
    partial class GamePatches
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
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.PatchListBox = new System.Windows.Forms.ListBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.DateCreatedLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.PatchNameLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.downloadingLabel = new System.Windows.Forms.Label();
            this.FreshRomLabel = new System.Windows.Forms.Label();
            this.GameHackLabel = new System.Windows.Forms.Label();
            this.AssemblyHackLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.clock = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(256, 224);
            this.ImagePictureBox.TabIndex = 2;
            this.ImagePictureBox.TabStop = false;
            // 
            // PatchListBox
            // 
            this.PatchListBox.FormattingEnabled = true;
            this.PatchListBox.HorizontalScrollbar = true;
            this.PatchListBox.IntegralHeight = false;
            this.PatchListBox.Location = new System.Drawing.Point(12, 12);
            this.PatchListBox.Name = "PatchListBox";
            this.PatchListBox.Size = new System.Drawing.Size(147, 270);
            this.PatchListBox.TabIndex = 0;
            this.PatchListBox.SelectedIndexChanged += new System.EventHandler(this.PatchListBox_SelectedIndexChanged);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(142, 347);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(0, 13);
            this.AuthorLabel.TabIndex = 4;
            // 
            // DateCreatedLabel
            // 
            this.DateCreatedLabel.AutoSize = true;
            this.DateCreatedLabel.Location = new System.Drawing.Point(142, 360);
            this.DateCreatedLabel.Name = "DateCreatedLabel";
            this.DateCreatedLabel.Size = new System.Drawing.Size(0, 13);
            this.DateCreatedLabel.TabIndex = 5;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(165, 12);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.Size = new System.Drawing.Size(300, 270);
            this.DescriptionTextBox.TabIndex = 2;
            this.DescriptionTextBox.Text = "";
            this.DescriptionTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.DescriptionTextBox_LinkClicked);
            // 
            // PatchNameLabel
            // 
            this.PatchNameLabel.AutoSize = true;
            this.PatchNameLabel.Location = new System.Drawing.Point(142, 334);
            this.PatchNameLabel.Name = "PatchNameLabel";
            this.PatchNameLabel.Size = new System.Drawing.Size(0, 13);
            this.PatchNameLabel.TabIndex = 7;
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(142, 373);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(0, 13);
            this.SizeLabel.TabIndex = 8;
            // 
            // applyButton
            // 
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(12, 285);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(147, 21);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "APPLY PATCH";
            this.applyButton.UseCompatibleTextRendering = true;
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // downloadingLabel
            // 
            this.downloadingLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.downloadingLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.downloadingLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.downloadingLabel.Location = new System.Drawing.Point(165, 285);
            this.downloadingLabel.Name = "downloadingLabel";
            this.downloadingLabel.Size = new System.Drawing.Size(300, 21);
            this.downloadingLabel.TabIndex = 3;
            this.downloadingLabel.Text = "              ...INITIALIZING...              ";
            this.downloadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FreshRomLabel
            // 
            this.FreshRomLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FreshRomLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreshRomLabel.Location = new System.Drawing.Point(471, 285);
            this.FreshRomLabel.Name = "FreshRomLabel";
            this.FreshRomLabel.Size = new System.Drawing.Size(260, 21);
            this.FreshRomLabel.TabIndex = 7;
            this.FreshRomLabel.Text = "This hack must be applied to a fresh rom";
            this.FreshRomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameHackLabel
            // 
            this.GameHackLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GameHackLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameHackLabel.Location = new System.Drawing.Point(471, 264);
            this.GameHackLabel.Name = "GameHackLabel";
            this.GameHackLabel.Size = new System.Drawing.Size(260, 21);
            this.GameHackLabel.TabIndex = 6;
            this.GameHackLabel.Text = "Game Hack";
            this.GameHackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AssemblyHackLabel
            // 
            this.AssemblyHackLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AssemblyHackLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssemblyHackLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AssemblyHackLabel.Location = new System.Drawing.Point(471, 243);
            this.AssemblyHackLabel.Name = "AssemblyHackLabel";
            this.AssemblyHackLabel.Size = new System.Drawing.Size(260, 21);
            this.AssemblyHackLabel.TabIndex = 5;
            this.AssemblyHackLabel.Text = "Assembly Hack";
            this.AssemblyHackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.ImagePictureBox);
            this.panel3.Location = new System.Drawing.Point(471, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 228);
            this.panel3.TabIndex = 4;
            // 
            // clock
            // 
            this.clock.WorkerReportsProgress = true;
            this.clock.WorkerSupportsCancellation = true;
            this.clock.DoWork += new System.ComponentModel.DoWorkEventHandler(this.clock_DoWork);
            this.clock.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.clock_ProgressChanged);
            this.clock.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.clock_RunWorkerCompleted);
            // 
            // GamePatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 315);
            this.Controls.Add(this.FreshRomLabel);
            this.Controls.Add(this.downloadingLabel);
            this.Controls.Add(this.GameHackLabel);
            this.Controls.Add(this.PatchListBox);
            this.Controls.Add(this.AssemblyHackLabel);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.PatchNameLabel);
            this.Controls.Add(this.DateCreatedLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.DescriptionTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.MaximizeBox = false;
            this.Name = "GamePatches";
            this.Text = "PATCHES - Lazy Shell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GamePatches_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.ListBox PatchListBox;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label DateCreatedLabel;
        private System.Windows.Forms.RichTextBox DescriptionTextBox;
        private System.Windows.Forms.Label PatchNameLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label AssemblyHackLabel;
        private System.Windows.Forms.Label FreshRomLabel;
        private System.Windows.Forms.Label GameHackLabel;
        private System.Windows.Forms.Label downloadingLabel;
        private System.Windows.Forms.Panel panel3;
        private System.ComponentModel.BackgroundWorker clock;
    }
}