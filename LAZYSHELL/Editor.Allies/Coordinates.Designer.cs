
namespace LAZYSHELL
{
    partial class Coordinates
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxCharacter = new System.Windows.Forms.PictureBox();
            this.label119 = new System.Windows.Forms.Label();
            this.characterTargetArrowY = new System.Windows.Forms.NumericUpDown();
            this.characterTargetArrowX = new System.Windows.Forms.NumericUpDown();
            this.cursorSpriteChoice = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.scarecrowButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCharacter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterTargetArrowY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterTargetArrowX)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCharacter);
            this.groupBox1.Controls.Add(this.label119);
            this.groupBox1.Controls.Add(this.characterTargetArrowY);
            this.groupBox1.Controls.Add(this.characterTargetArrowX);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 298);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character Image";
            // 
            // pictureBoxCharacter
            // 
            this.pictureBoxCharacter.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxCharacter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxCharacter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxCharacter.Location = new System.Drawing.Point(3, 17);
            this.pictureBoxCharacter.Name = "pictureBoxCharacter";
            this.pictureBoxCharacter.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxCharacter.TabIndex = 220;
            this.pictureBoxCharacter.TabStop = false;
            this.pictureBoxCharacter.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxCharacter_Paint);
            this.pictureBoxCharacter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCharacter_MouseDown);
            this.pictureBoxCharacter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCharacter_MouseUp);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(6, 276);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(69, 13);
            this.label119.TabIndex = 0;
            this.label119.Text = "Target (X, Y)";
            // 
            // characterTargetArrowY
            // 
            this.characterTargetArrowY.Location = new System.Drawing.Point(188, 274);
            this.characterTargetArrowY.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.characterTargetArrowY.Name = "characterTargetArrowY";
            this.characterTargetArrowY.Size = new System.Drawing.Size(65, 21);
            this.characterTargetArrowY.TabIndex = 2;
            this.characterTargetArrowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.characterTargetArrowY.ValueChanged += new System.EventHandler(this.characterTargetArrowY_ValueChanged);
            // 
            // characterTargetArrowX
            // 
            this.characterTargetArrowX.Location = new System.Drawing.Point(123, 274);
            this.characterTargetArrowX.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.characterTargetArrowX.Name = "characterTargetArrowX";
            this.characterTargetArrowX.Size = new System.Drawing.Size(65, 21);
            this.characterTargetArrowX.TabIndex = 1;
            this.characterTargetArrowX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.characterTargetArrowX.ValueChanged += new System.EventHandler(this.characterTargetArrowX_ValueChanged);
            // 
            // cursorSpriteChoice
            // 
            this.cursorSpriteChoice.AutoCompleteCustomSource.AddRange(new string[] {
            "Cursor",
            "ABXY"});
            this.cursorSpriteChoice.BackColor = System.Drawing.SystemColors.Control;
            this.cursorSpriteChoice.DropDownHeight = 317;
            this.cursorSpriteChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cursorSpriteChoice.DropDownWidth = 122;
            this.cursorSpriteChoice.IntegralHeight = false;
            this.cursorSpriteChoice.ItemHeight = 13;
            this.cursorSpriteChoice.Items.AddRange(new object[] {
            "ABXY",
            "Cursor"});
            this.cursorSpriteChoice.Location = new System.Drawing.Point(0, 326);
            this.cursorSpriteChoice.Name = "cursorSpriteChoice";
            this.cursorSpriteChoice.Size = new System.Drawing.Size(96, 21);
            this.cursorSpriteChoice.TabIndex = 4;
            this.cursorSpriteChoice.Tag = "";
            this.cursorSpriteChoice.SelectedIndexChanged += new System.EventHandler(this.cursorSpriteChoice_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scarecrowButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(262, 25);
            this.toolStrip1.TabIndex = 5;
            // 
            // scarecrowButton
            // 
            this.scarecrowButton.CheckOnClick = true;
            this.scarecrowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.scarecrowButton.Image = global::LAZYSHELL.Properties.Resources.toggleScarecrow;
            this.scarecrowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scarecrowButton.Name = "scarecrowButton";
            this.scarecrowButton.Size = new System.Drawing.Size(23, 22);
            this.scarecrowButton.CheckStateChanged += new System.EventHandler(this.scarecrowButton_Click);
            // 
            // Coordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 433);
            this.ControlBox = false;
            this.Controls.Add(this.cursorSpriteChoice);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Coordinates";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCharacter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterTargetArrowY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterTargetArrowX)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxCharacter;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.NumericUpDown characterTargetArrowY;
        private System.Windows.Forms.NumericUpDown characterTargetArrowX;
        private System.Windows.Forms.ComboBox cursorSpriteChoice;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton scarecrowButton;
    }
}