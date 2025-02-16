
namespace LAZYSHELL
{
    partial class FormationsEditor
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripDropDownButton();
            this.importFormationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportFormationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetFormationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearFormationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showFormations = new System.Windows.Forms.ToolStripButton();
            this.showPacks = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator4,
            this.import,
            this.export,
            this.toolStripSeparator12,
            this.toolStripDropDownButton1,
            this.clear,
            this.helpTips,
            this.baseConvertor,
            this.toolStripSeparator1,
            this.showPacks,
            this.showFormations});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(648, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // import
            // 
            this.import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFormationsToolStripMenuItem,
            this.importPacksToolStripMenuItem});
            this.import.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(29, 22);
            // 
            // importFormationsToolStripMenuItem
            // 
            this.importFormationsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.importFormationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importFormationsToolStripMenuItem.Name = "importFormationsToolStripMenuItem";
            this.importFormationsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.importFormationsToolStripMenuItem.Text = "Import Formations...";
            this.importFormationsToolStripMenuItem.Click += new System.EventHandler(this.importFormationsToolStripMenuItem_Click);
            // 
            // importPacksToolStripMenuItem
            // 
            this.importPacksToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.importPacksToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importPacksToolStripMenuItem.Name = "importPacksToolStripMenuItem";
            this.importPacksToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.importPacksToolStripMenuItem.Text = "Import Packs...";
            this.importPacksToolStripMenuItem.Click += new System.EventHandler(this.importPacksToolStripMenuItem_Click);
            // 
            // export
            // 
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportFormationsToolStripMenuItem,
            this.exportPacksToolStripMenuItem});
            this.export.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(29, 22);
            // 
            // exportFormationsToolStripMenuItem
            // 
            this.exportFormationsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.exportFormationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportFormationsToolStripMenuItem.Name = "exportFormationsToolStripMenuItem";
            this.exportFormationsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exportFormationsToolStripMenuItem.Text = "Export Formations...";
            this.exportFormationsToolStripMenuItem.Click += new System.EventHandler(this.exportFormationsToolStripMenuItem_Click);
            // 
            // exportPacksToolStripMenuItem
            // 
            this.exportPacksToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.exportPacksToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportPacksToolStripMenuItem.Name = "exportPacksToolStripMenuItem";
            this.exportPacksToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exportPacksToolStripMenuItem.Text = "Export Packs...";
            this.exportPacksToolStripMenuItem.Click += new System.EventHandler(this.exportPacksToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetFormationToolStripMenuItem,
            this.resetPackToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // resetFormationToolStripMenuItem
            // 
            this.resetFormationToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetFormationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetFormationToolStripMenuItem.Name = "resetFormationToolStripMenuItem";
            this.resetFormationToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.resetFormationToolStripMenuItem.Text = "Reset formation";
            this.resetFormationToolStripMenuItem.Click += new System.EventHandler(this.resetFormationToolStripMenuItem_Click);
            // 
            // resetPackToolStripMenuItem
            // 
            this.resetPackToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetPackToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetPackToolStripMenuItem.Name = "resetPackToolStripMenuItem";
            this.resetPackToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.resetPackToolStripMenuItem.Text = "Reset pack";
            this.resetPackToolStripMenuItem.Click += new System.EventHandler(this.resetPackToolStripMenuItem_Click);
            // 
            // clear
            // 
            this.clear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearFormationsToolStripMenuItem,
            this.clearPacksToolStripMenuItem});
            this.clear.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(29, 22);
            // 
            // clearFormationsToolStripMenuItem
            // 
            this.clearFormationsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clearFormationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearFormationsToolStripMenuItem.Name = "clearFormationsToolStripMenuItem";
            this.clearFormationsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.clearFormationsToolStripMenuItem.Text = "Clear Formations...";
            this.clearFormationsToolStripMenuItem.Click += new System.EventHandler(this.clearFormationsToolStripMenuItem_Click);
            // 
            // clearPacksToolStripMenuItem
            // 
            this.clearPacksToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clearPacksToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearPacksToolStripMenuItem.Name = "clearPacksToolStripMenuItem";
            this.clearPacksToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.clearPacksToolStripMenuItem.Text = "Clear Packs...";
            this.clearPacksToolStripMenuItem.Click += new System.EventHandler(this.clearPacksToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // helpTips
            // 
            this.helpTips.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpTips.CheckOnClick = true;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.ToolTipText = "Help Tips";
            // 
            // baseConvertor
            // 
            this.baseConvertor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.baseConvertor.CheckOnClick = true;
            this.baseConvertor.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.baseConvertor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.baseConvertor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConvertor.Name = "baseConvertor";
            this.baseConvertor.Size = new System.Drawing.Size(23, 22);
            this.baseConvertor.ToolTipText = "Base Convertor";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // showFormations
            // 
            this.showFormations.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showFormations.Checked = true;
            this.showFormations.CheckOnClick = true;
            this.showFormations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFormations.Image = global::LAZYSHELL.Properties.Resources.mainFormations;
            this.showFormations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showFormations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showFormations.Name = "showFormations";
            this.showFormations.Size = new System.Drawing.Size(23, 22);
            this.showFormations.ToolTipText = "Formations";
            this.showFormations.Click += new System.EventHandler(this.showFormations_Click);
            // 
            // showPacks
            // 
            this.showPacks.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showPacks.Checked = true;
            this.showPacks.CheckOnClick = true;
            this.showPacks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPacks.Image = global::LAZYSHELL.Properties.Resources.openPacks;
            this.showPacks.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPacks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showPacks.Name = "showPacks";
            this.showPacks.Size = new System.Drawing.Size(23, 22);
            this.showPacks.ToolTipText = "Formation Packs";
            this.showPacks.Click += new System.EventHandler(this.showPacks_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 500);
            this.panel1.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // FormationsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 525);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.mainFormations_ico;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "FormationsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FORMATIONS - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormationsEditor_FormClosing);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton import;
        private System.Windows.Forms.ToolStripDropDownButton export;
        private System.Windows.Forms.ToolStripDropDownButton clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showFormations;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton showPacks;
        private System.Windows.Forms.ToolStripMenuItem importFormationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFormationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearFormationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPacksToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem resetFormationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPackToolStripMenuItem;
    }
}