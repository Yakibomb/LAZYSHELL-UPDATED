
namespace LAZYSHELL
{
    partial class CoordAdjust
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
            this.coordX = new System.Windows.Forms.NumericUpDown();
            this.coordY = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.applyToAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.coordX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordY)).BeginInit();
            this.SuspendLayout();
            // 
            // coordX
            // 
            this.coordX.Location = new System.Drawing.Point(12, 12);
            this.coordX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.coordX.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.coordX.Name = "coordX";
            this.coordX.Size = new System.Drawing.Size(75, 21);
            this.coordX.TabIndex = 0;
            this.coordX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            // 
            // coordY
            // 
            this.coordY.Location = new System.Drawing.Point(93, 12);
            this.coordY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.coordY.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.coordY.Name = "coordY";
            this.coordY.Size = new System.Drawing.Size(75, 21);
            this.coordY.TabIndex = 1;
            this.coordY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 62);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 62);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            this.buttonCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            // 
            // applyToAll
            // 
            this.applyToAll.AutoSize = true;
            this.applyToAll.Location = new System.Drawing.Point(12, 39);
            this.applyToAll.Name = "applyToAll";
            this.applyToAll.Size = new System.Drawing.Size(109, 17);
            this.applyToAll.TabIndex = 2;
            this.applyToAll.Text = "Apply to all molds";
            this.applyToAll.UseVisualStyleBackColor = true;
            this.applyToAll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            // 
            // CoordAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 97);
            this.Controls.Add(this.applyToAll);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.coordY);
            this.Controls.Add(this.coordX);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CoordAdjust";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ADJUST COORDS";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoordAdjust_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.coordX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coordY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.NumericUpDown coordX;
        private System.Windows.Forms.NumericUpDown coordY;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox applyToAll;
    }
}