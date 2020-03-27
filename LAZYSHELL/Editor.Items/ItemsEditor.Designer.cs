
namespace LAZYSHELL
{
    partial class ItemsEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripDropDownButton();
            this.importItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportShopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetShopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearShopsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showItems = new System.Windows.Forms.ToolStripButton();
            this.showShops = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 664);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator4,
            this.import,
            this.export,
            this.toolStripDropDownButton1,
            this.clear,
            this.toolStripSeparator1,
            this.helpTips,
            this.baseConvertor,
            this.toolStripSeparator2,
            this.showItems,
            this.showShops});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(622, 25);
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
            this.importItemsToolStripMenuItem,
            this.importShopsToolStripMenuItem});
            this.import.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(27, 22);
            // 
            // importItemsToolStripMenuItem
            // 
            this.importItemsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.importItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importItemsToolStripMenuItem.Name = "importItemsToolStripMenuItem";
            this.importItemsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importItemsToolStripMenuItem.Text = "Import Items...";
            this.importItemsToolStripMenuItem.Click += new System.EventHandler(this.importItemsToolStripMenuItem_Click);
            // 
            // importShopsToolStripMenuItem
            // 
            this.importShopsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.importShopsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importShopsToolStripMenuItem.Name = "importShopsToolStripMenuItem";
            this.importShopsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importShopsToolStripMenuItem.Text = "Import Shops...";
            this.importShopsToolStripMenuItem.Click += new System.EventHandler(this.importShopsToolStripMenuItem_Click);
            // 
            // export
            // 
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportItemsToolStripMenuItem,
            this.exportShopsToolStripMenuItem});
            this.export.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(27, 22);
            // 
            // exportItemsToolStripMenuItem
            // 
            this.exportItemsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.exportItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportItemsToolStripMenuItem.Name = "exportItemsToolStripMenuItem";
            this.exportItemsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exportItemsToolStripMenuItem.Text = "Export Items...";
            this.exportItemsToolStripMenuItem.Click += new System.EventHandler(this.exportItemsToolStripMenuItem_Click);
            // 
            // exportShopsToolStripMenuItem
            // 
            this.exportShopsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.exportShopsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportShopsToolStripMenuItem.Name = "exportShopsToolStripMenuItem";
            this.exportShopsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exportShopsToolStripMenuItem.Text = "Export Shops...";
            this.exportShopsToolStripMenuItem.Click += new System.EventHandler(this.exportShopsToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetItemToolStripMenuItem,
            this.resetShopToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(27, 22);
            // 
            // resetItemToolStripMenuItem
            // 
            this.resetItemToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetItemToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetItemToolStripMenuItem.Name = "resetItemToolStripMenuItem";
            this.resetItemToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.resetItemToolStripMenuItem.Text = "Reset item";
            this.resetItemToolStripMenuItem.Click += new System.EventHandler(this.resetItemToolStripMenuItem_Click);
            // 
            // resetShopToolStripMenuItem
            // 
            this.resetShopToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetShopToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetShopToolStripMenuItem.Name = "resetShopToolStripMenuItem";
            this.resetShopToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.resetShopToolStripMenuItem.Text = "Reset shop";
            this.resetShopToolStripMenuItem.Click += new System.EventHandler(this.resetShopToolStripMenuItem_Click);
            // 
            // clear
            // 
            this.clear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearItemsToolStripMenuItem,
            this.clearShopsToolStripMenuItem});
            this.clear.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(28, 22);
            // 
            // clearItemsToolStripMenuItem
            // 
            this.clearItemsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clearItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearItemsToolStripMenuItem.Name = "clearItemsToolStripMenuItem";
            this.clearItemsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.clearItemsToolStripMenuItem.Text = "Clear Items...";
            this.clearItemsToolStripMenuItem.Click += new System.EventHandler(this.clearItemsToolStripMenuItem_Click);
            // 
            // clearShopsToolStripMenuItem
            // 
            this.clearShopsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clearShopsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearShopsToolStripMenuItem.Name = "clearShopsToolStripMenuItem";
            this.clearShopsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.clearShopsToolStripMenuItem.Text = "Clear Shops...";
            this.clearShopsToolStripMenuItem.Click += new System.EventHandler(this.clearShopsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // baseConvertor
            // 
            this.baseConvertor.CheckOnClick = true;
            this.baseConvertor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.baseConvertor.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.baseConvertor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.baseConvertor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConvertor.Name = "baseConvertor";
            this.baseConvertor.Size = new System.Drawing.Size(23, 22);
            this.baseConvertor.Text = "Base Convertor";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showItems
            // 
            this.showItems.Checked = true;
            this.showItems.CheckOnClick = true;
            this.showItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showItems.Image = global::LAZYSHELL.Properties.Resources.mainItems;
            this.showItems.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showItems.Name = "showItems";
            this.showItems.Size = new System.Drawing.Size(23, 22);
            this.showItems.ToolTipText = "Items";
            this.showItems.Click += new System.EventHandler(this.showItems_Click);
            // 
            // showShops
            // 
            this.showShops.Checked = true;
            this.showShops.CheckOnClick = true;
            this.showShops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showShops.Image = global::LAZYSHELL.Properties.Resources.openShops;
            this.showShops.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showShops.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showShops.Name = "showShops";
            this.showShops.Size = new System.Drawing.Size(23, 22);
            this.showShops.ToolTipText = "Shops";
            this.showShops.Click += new System.EventHandler(this.showShops_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // ItemsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 689);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "ItemsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ITEMS - Lazy Shell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemsEditor_FormClosing);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton import;
        private System.Windows.Forms.ToolStripDropDownButton export;
        private System.Windows.Forms.ToolStripDropDownButton clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showItems;
        private System.Windows.Forms.ToolStripButton showShops;
        private System.Windows.Forms.ToolStripMenuItem importItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importShopsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportShopsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearShopsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem resetItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetShopToolStripMenuItem;
    }
}