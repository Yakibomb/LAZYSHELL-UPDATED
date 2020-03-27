
namespace LAZYSHELL
{
    partial class LevelsTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelsTemplate));
            this.templateImport = new System.Windows.Forms.ToolStripButton();
            this.templateExport = new System.Windows.Forms.ToolStripButton();
            this.templateDelete = new System.Windows.Forms.ToolStripButton();
            this.templateCopy = new System.Windows.Forms.ToolStripButton();
            this.templatePaste = new System.Windows.Forms.ToolStripButton();
            this.templatesLoaded = new System.Windows.Forms.ListBox();
            this.panel114 = new System.Windows.Forms.Panel();
            this.pictureBoxTemplate = new System.Windows.Forms.PictureBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.templateTransfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.templateRename = new System.Windows.Forms.ToolStripButton();
            this.templateRenameText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel114.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // templateImport
            // 
            this.templateImport.Image = global::LAZYSHELL.Properties.Resources.open_small;
            this.templateImport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateImport.Name = "templateImport";
            this.templateImport.Size = new System.Drawing.Size(48, 22);
            this.templateImport.Text = "Load";
            this.templateImport.ToolTipText = "Load template(s)";
            this.templateImport.Click += new System.EventHandler(this.templateImport_Click);
            // 
            // templateExport
            // 
            this.templateExport.Enabled = false;
            this.templateExport.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.templateExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateExport.Name = "templateExport";
            this.templateExport.Size = new System.Drawing.Size(49, 22);
            this.templateExport.Text = "Save";
            this.templateExport.ToolTipText = "Save template(s)";
            this.templateExport.Click += new System.EventHandler(this.templateExport_Click);
            // 
            // templateDelete
            // 
            this.templateDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.templateDelete.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.templateDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateDelete.Name = "templateDelete";
            this.templateDelete.Size = new System.Drawing.Size(23, 22);
            this.templateDelete.Text = "Delete template";
            this.templateDelete.Click += new System.EventHandler(this.templateDelete_Click);
            // 
            // templateCopy
            // 
            this.templateCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.templateCopy.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.templateCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateCopy.Name = "templateCopy";
            this.templateCopy.Size = new System.Drawing.Size(23, 22);
            this.templateCopy.Text = "Copy template";
            this.templateCopy.Click += new System.EventHandler(this.templateCopy_Click);
            // 
            // templatePaste
            // 
            this.templatePaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.templatePaste.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.templatePaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templatePaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templatePaste.Name = "templatePaste";
            this.templatePaste.Size = new System.Drawing.Size(23, 22);
            this.templatePaste.Text = "Paste template";
            this.templatePaste.Click += new System.EventHandler(this.templatePaste_Click);
            // 
            // templatesLoaded
            // 
            this.templatesLoaded.Dock = System.Windows.Forms.DockStyle.Top;
            this.templatesLoaded.Enabled = false;
            this.templatesLoaded.FormattingEnabled = true;
            this.templatesLoaded.Location = new System.Drawing.Point(0, 25);
            this.templatesLoaded.Name = "templatesLoaded";
            this.templatesLoaded.Size = new System.Drawing.Size(270, 147);
            this.templatesLoaded.TabIndex = 1;
            this.templatesLoaded.SelectedIndexChanged += new System.EventHandler(this.templatesLoaded_SelectedIndexChanged);
            // 
            // panel114
            // 
            this.panel114.AutoScroll = true;
            this.panel114.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel114.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel114.Controls.Add(this.pictureBoxTemplate);
            this.panel114.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel114.Location = new System.Drawing.Point(0, 197);
            this.panel114.Name = "panel114";
            this.panel114.Size = new System.Drawing.Size(270, 362);
            this.panel114.TabIndex = 3;
            // 
            // pictureBoxTemplate
            // 
            this.pictureBoxTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxTemplate.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTemplate.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTemplate.Name = "pictureBoxTemplate";
            this.pictureBoxTemplate.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxTemplate.TabIndex = 450;
            this.pictureBoxTemplate.TabStop = false;
            this.pictureBoxTemplate.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTemplate_Paint);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateTransfer,
            this.toolStripSeparator45,
            this.templateImport,
            this.templateExport,
            this.toolStripSeparator43});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(270, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // templateTransfer
            // 
            this.templateTransfer.Image = global::LAZYSHELL.Properties.Resources.transfer_small;
            this.templateTransfer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateTransfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateTransfer.Name = "templateTransfer";
            this.templateTransfer.Size = new System.Drawing.Size(105, 22);
            this.templateTransfer.Text = "Create Template";
            this.templateTransfer.ToolTipText = "Create Template from current selection";
            this.templateTransfer.Click += new System.EventHandler(this.templateTransfer_Click);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateRename,
            this.templateRenameText,
            this.toolStripSeparator1,
            this.templateDelete,
            this.templateCopy,
            this.templatePaste});
            this.toolStrip1.Location = new System.Drawing.Point(0, 172);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(270, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // templateRename
            // 
            this.templateRename.Checked = true;
            this.templateRename.CheckOnClick = true;
            this.templateRename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.templateRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.templateRename.Image = global::LAZYSHELL.Properties.Resources.label;
            this.templateRename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.templateRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templateRename.Name = "templateRename";
            this.templateRename.Size = new System.Drawing.Size(23, 22);
            this.templateRename.Text = "Set Template Name";
            this.templateRename.Click += new System.EventHandler(this.templateRename_Click);
            // 
            // templateRenameText
            // 
            this.templateRenameText.Name = "templateRenameText";
            this.templateRenameText.Size = new System.Drawing.Size(150, 25);
            this.templateRenameText.TextChanged += new System.EventHandler(this.templateRenameText_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelsTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 559);
            this.ControlBox = false;
            this.Controls.Add(this.panel114);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.templatesLoaded);
            this.Controls.Add(this.toolStrip3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LevelsTemplate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel114.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.ListBox templatesLoaded;
        private System.Windows.Forms.Panel panel114;
        private System.Windows.Forms.PictureBox pictureBoxTemplate;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton templateTransfer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator45;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator43;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton templateRename;
        private System.Windows.Forms.ToolStripTextBox templateRenameText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton templateImport;
        private System.Windows.Forms.ToolStripButton templateExport;
        private System.Windows.Forms.ToolStripButton templateDelete;
        private System.Windows.Forms.ToolStripButton templateCopy;
        private System.Windows.Forms.ToolStripButton templatePaste;
    }
}