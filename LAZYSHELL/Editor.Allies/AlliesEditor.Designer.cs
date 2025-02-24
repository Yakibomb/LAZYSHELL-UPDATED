
namespace LAZYSHELL
{
    partial class AlliesEditor
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
            this.import = new System.Windows.Forms.ToolStripButton();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearBaseNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCharactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showABYXCursor = new System.Windows.Forms.ToolStripButton();
            this.showLevelUps = new System.Windows.Forms.ToolStripButton();
            this.showNewGameStats = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetBaseNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetNewGameStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCurrentLevelupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCurrentCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new LAZYSHELL.NewPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MainDetailsPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ally1 = new System.Windows.Forms.ToolStripButton();
            this.ally2 = new System.Windows.Forms.ToolStripButton();
            this.ally3 = new System.Windows.Forms.ToolStripButton();
            this.ally4 = new System.Windows.Forms.ToolStripButton();
            this.ally5 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3.SuspendLayout();
            this.MainDetailsPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.toolStripSeparator1,
            this.clear,
            this.helpTips,
            this.baseConvertor,
            this.toolStripSeparator2,
            this.showABYXCursor,
            this.showLevelUps,
            this.showNewGameStats,
            this.toolStripButton1});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(1016, 25);
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
            this.import.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(23, 22);
            this.import.ToolTipText = "Import";
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // export
            // 
            this.export.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(23, 22);
            this.export.ToolTipText = "Export";
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // clear
            // 
            this.clear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearBaseNewGameToolStripMenuItem,
            this.clearCharactersToolStripMenuItem});
            this.clear.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(29, 22);
            this.clear.ToolTipText = "Clear";
            // 
            // clearBaseNewGameToolStripMenuItem
            // 
            this.clearBaseNewGameToolStripMenuItem.Name = "clearBaseNewGameToolStripMenuItem";
            this.clearBaseNewGameToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.clearBaseNewGameToolStripMenuItem.Text = "Clear new game";
            this.clearBaseNewGameToolStripMenuItem.Click += new System.EventHandler(this.clearBaseNewGameToolStripMenuItem_Click);
            // 
            // clearCharactersToolStripMenuItem
            // 
            this.clearCharactersToolStripMenuItem.Name = "clearCharactersToolStripMenuItem";
            this.clearCharactersToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.clearCharactersToolStripMenuItem.Text = "Clear character(s)";
            this.clearCharactersToolStripMenuItem.Click += new System.EventHandler(this.clearCharactersToolStripMenuItem_Click);
            // 
            // helpTips
            // 
            this.helpTips.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            this.baseConvertor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showABYXCursor
            // 
            this.showABYXCursor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showABYXCursor.Checked = true;
            this.showABYXCursor.CheckOnClick = true;
            this.showABYXCursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showABYXCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showABYXCursor.Image = global::LAZYSHELL.Properties.Resources.numerals;
            this.showABYXCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showABYXCursor.Name = "showABYXCursor";
            this.showABYXCursor.Size = new System.Drawing.Size(23, 22);
            this.showABYXCursor.Text = "toolStripButton2";
            this.showABYXCursor.Click += new System.EventHandler(this.showABXYCursor_Click);
            // 
            // showLevelUps
            // 
            this.showLevelUps.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showLevelUps.Checked = true;
            this.showLevelUps.CheckOnClick = true;
            this.showLevelUps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showLevelUps.Image = global::LAZYSHELL.Properties.Resources.openLevelUps;
            this.showLevelUps.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLevelUps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showLevelUps.Name = "showLevelUps";
            this.showLevelUps.Size = new System.Drawing.Size(23, 22);
            this.showLevelUps.ToolTipText = "Level-Ups";
            this.showLevelUps.Click += new System.EventHandler(this.showLevelUps_Click);
            // 
            // showNewGameStats
            // 
            this.showNewGameStats.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showNewGameStats.Checked = true;
            this.showNewGameStats.CheckOnClick = true;
            this.showNewGameStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNewGameStats.Image = global::LAZYSHELL.Properties.Resources.openNewGame;
            this.showNewGameStats.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showNewGameStats.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showNewGameStats.Name = "showNewGameStats";
            this.showNewGameStats.Size = new System.Drawing.Size(23, 22);
            this.showNewGameStats.ToolTipText = "New Game Stats";
            this.showNewGameStats.Click += new System.EventHandler(this.showNewGameStats_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetBaseNewGameToolStripMenuItem,
            this.resetNewGameStatsToolStripMenuItem,
            this.resetCurrentLevelupsToolStripMenuItem,
            this.resetCurrentCoordinatesToolStripMenuItem});
            this.toolStripButton1.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripButton1.Text = "Reset";
            // 
            // resetBaseNewGameToolStripMenuItem
            // 
            this.resetBaseNewGameToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetBaseNewGameToolStripMenuItem.Name = "resetBaseNewGameToolStripMenuItem";
            this.resetBaseNewGameToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.resetBaseNewGameToolStripMenuItem.Text = "Reset new game";
            this.resetBaseNewGameToolStripMenuItem.Click += new System.EventHandler(this.resetBaseNewGameToolStripMenuItem_Click);
            // 
            // resetNewGameStatsToolStripMenuItem
            // 
            this.resetNewGameStatsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetNewGameStatsToolStripMenuItem.Name = "resetNewGameStatsToolStripMenuItem";
            this.resetNewGameStatsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.resetNewGameStatsToolStripMenuItem.Text = "Reset current character new game status";
            this.resetNewGameStatsToolStripMenuItem.Click += new System.EventHandler(this.resetNewGameStatsToolStripMenuItem_Click);
            // 
            // resetCurrentLevelupsToolStripMenuItem
            // 
            this.resetCurrentLevelupsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetCurrentLevelupsToolStripMenuItem.Name = "resetCurrentLevelupsToolStripMenuItem";
            this.resetCurrentLevelupsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.resetCurrentLevelupsToolStripMenuItem.Text = "Reset current character level-ups";
            this.resetCurrentLevelupsToolStripMenuItem.Click += new System.EventHandler(this.resetCurrentLevelupsToolStripMenuItem_Click);
            // 
            // resetCurrentCoordinatesToolStripMenuItem
            // 
            this.resetCurrentCoordinatesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.resetCurrentCoordinatesToolStripMenuItem.Name = "resetCurrentCoordinatesToolStripMenuItem";
            this.resetCurrentCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.resetCurrentCoordinatesToolStripMenuItem.Text = "Reset current character coordinates";
            this.resetCurrentCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.resetCurrentCoordinatesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(64, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 445);
            this.panel1.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // MainDetailsPanel
            // 
            this.MainDetailsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.MainDetailsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainDetailsPanel.Controls.Add(this.toolStrip1);
            this.MainDetailsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainDetailsPanel.Location = new System.Drawing.Point(0, 25);
            this.MainDetailsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainDetailsPanel.Name = "MainDetailsPanel";
            this.MainDetailsPanel.Size = new System.Drawing.Size(64, 445);
            this.MainDetailsPanel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ally1,
            this.ally2,
            this.ally3,
            this.ally4,
            this.ally5});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(60, 266);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ally1
            // 
            this.ally1.AutoSize = false;
            this.ally1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ally1.Image = global::LAZYSHELL.Properties.Resources._BowserCameraGawk;
            this.ally1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ally1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ally1.Name = "ally1";
            this.ally1.Size = new System.Drawing.Size(48, 48);
            this.ally1.Text = "toolStripButton1";
            this.ally1.Click += new System.EventHandler(this.ally1_Click);
            // 
            // ally2
            // 
            this.ally2.AutoSize = false;
            this.ally2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ally2.Image = global::LAZYSHELL.Properties.Resources._BowserCameraGawk;
            this.ally2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ally2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ally2.Name = "ally2";
            this.ally2.Size = new System.Drawing.Size(48, 48);
            this.ally2.Text = "toolStripButton1";
            this.ally2.Click += new System.EventHandler(this.ally2_Click);
            // 
            // ally3
            // 
            this.ally3.AutoSize = false;
            this.ally3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ally3.Image = global::LAZYSHELL.Properties.Resources._BowserCameraGawk;
            this.ally3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ally3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ally3.Name = "ally3";
            this.ally3.Size = new System.Drawing.Size(48, 48);
            this.ally3.Text = "toolStripButton1";
            this.ally3.Click += new System.EventHandler(this.ally3_Click);
            // 
            // ally4
            // 
            this.ally4.AutoSize = false;
            this.ally4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ally4.Image = global::LAZYSHELL.Properties.Resources._BowserCameraGawk;
            this.ally4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ally4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ally4.Name = "ally4";
            this.ally4.Size = new System.Drawing.Size(48, 48);
            this.ally4.Text = "toolStripButton1";
            this.ally4.Click += new System.EventHandler(this.ally4_Click);
            // 
            // ally5
            // 
            this.ally5.AutoSize = false;
            this.ally5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ally5.Image = global::LAZYSHELL.Properties.Resources._BowserCameraGawk;
            this.ally5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ally5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ally5.Name = "ally5";
            this.ally5.Size = new System.Drawing.Size(48, 48);
            this.ally5.Text = "toolStripButton1";
            this.ally5.Click += new System.EventHandler(this.ally5_Click);
            // 
            // AlliesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 470);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainDetailsPanel);
            this.Controls.Add(this.toolStrip3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.mainAllies_ico;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "AlliesEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ALLIES - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlliesEditor_FormClosing);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.MainDetailsPanel.ResumeLayout(false);
            this.MainDetailsPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton import;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showNewGameStats;
        private System.Windows.Forms.ToolStripButton showLevelUps;
        private NewPanel panel1;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel MainDetailsPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ally1;
        private System.Windows.Forms.ToolStripButton ally2;
        private System.Windows.Forms.ToolStripButton ally3;
        private System.Windows.Forms.ToolStripButton ally4;
        private System.Windows.Forms.ToolStripButton ally5;
        private System.Windows.Forms.ToolStripButton showABYXCursor;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem resetNewGameStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetCurrentLevelupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetCurrentCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton clear;
        private System.Windows.Forms.ToolStripMenuItem clearBaseNewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCharactersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetBaseNewGameToolStripMenuItem;
    }
}