
namespace LAZYSHELL
{
    partial class Audio
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleSamples = new System.Windows.Forms.ToolStripButton();
            this.toggleSPCs = new System.Windows.Forms.ToolStripButton();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 672);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.helpTips,
            this.toolStripSeparator1,
            this.toggleSamples,
            this.toggleSPCs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(992, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // save
            // 
            this.save.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.ToolTipText = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toggleSamples
            // 
            this.toggleSamples.Checked = true;
            this.toggleSamples.CheckOnClick = true;
            this.toggleSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleSamples.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleSamples.Image = global::LAZYSHELL.Properties.Resources.openSamples;
            this.toggleSamples.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleSamples.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleSamples.Name = "toggleSamples";
            this.toggleSamples.Size = new System.Drawing.Size(23, 22);
            this.toggleSamples.Text = "Show/hide Samples";
            this.toggleSamples.Click += new System.EventHandler(this.toggleSamples_Click);
            // 
            // toggleSPCs
            // 
            this.toggleSPCs.Checked = true;
            this.toggleSPCs.CheckOnClick = true;
            this.toggleSPCs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleSPCs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleSPCs.Image = global::LAZYSHELL.Properties.Resources.openSPCs;
            this.toggleSPCs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleSPCs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleSPCs.Name = "toggleSPCs";
            this.toggleSPCs.Size = new System.Drawing.Size(23, 22);
            this.toggleSPCs.Text = "Show/hide SPCs";
            this.toggleSPCs.Click += new System.EventHandler(this.toggleSPCs_Click);
            // 
            // helpTips
            // 
            this.helpTips.CheckOnClick = true;
            this.helpTips.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.Text = "Help Tips";
            // 
            // Audio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 697);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Audio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AUDIO - Lazy Shell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Audio_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toggleSamples;
        private System.Windows.Forms.ToolStripButton toggleSPCs;
        private System.Windows.Forms.ToolStripButton helpTips;
    }
}