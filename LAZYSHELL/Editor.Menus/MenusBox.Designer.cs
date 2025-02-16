
namespace LAZYSHELL
{
    partial class MenusBox
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
            this.moveUp = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.groupBoxMenuOverworld = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // moveUp
            // 
            this.moveUp.Location = new System.Drawing.Point(12, 226);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(90, 23);
            this.moveUp.TabIndex = 2;
            this.moveUp.Text = "MOVE UP";
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.Location = new System.Drawing.Point(115, 226);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(90, 23);
            this.moveDown.TabIndex = 3;
            this.moveDown.Text = "MOVE DOWN";
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // groupBoxMenuOverworld
            // 
            this.groupBoxMenuOverworld.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMenuOverworld.Name = "groupBoxMenuOverworld";
            this.groupBoxMenuOverworld.Size = new System.Drawing.Size(213, 219);
            this.groupBoxMenuOverworld.TabIndex = 1;
            this.groupBoxMenuOverworld.TabStop = false;
            this.groupBoxMenuOverworld.Text = "Overworld Menu List";
            // 
            // MenusBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 256);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxMenuOverworld);
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenusBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.GroupBox groupBoxMenuOverworld;
    }
}