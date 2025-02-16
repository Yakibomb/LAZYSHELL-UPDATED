
namespace LAZYSHELL
{
    partial class BattleDialogues
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleDialogues));
            this.battleDialogueTextBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.battleDlgType = new System.Windows.Forms.ToolStripComboBox();
            this.battleDialogueNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchButton = new System.Windows.Forms.ToolStripButton();
            this.bonusTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.bonusPreview = new System.Windows.Forms.ToolStripLabel();
            this.pictureBoxBattleDialogue = new System.Windows.Forms.PictureBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.pageUp = new System.Windows.Forms.ToolStripButton();
            this.pageDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.textView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.availableBytes = new System.Windows.Forms.ToolStripLabel();
            this.newLine = new System.Windows.Forms.ToolStripButton();
            this.endString = new System.Windows.Forms.ToolStripButton();
            this.pause60f = new System.Windows.Forms.ToolStripButton();
            this.pauseA = new System.Windows.Forms.ToolStripButton();
            this.pauseFrames = new System.Windows.Forms.ToolStripButton();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleText = new System.Windows.Forms.ToolStripButton();
            this.grid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openTileEditor = new System.Windows.Forms.ToolStripButton();
            this.openGraphics = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openPaletteDialogue = new System.Windows.Forms.ToolStripMenuItem();
            this.openPaletteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBattleDialogue)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // battleDialogueTextBox
            // 
            this.battleDialogueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.battleDialogueTextBox.Location = new System.Drawing.Point(0, 57);
            this.battleDialogueTextBox.Name = "battleDialogueTextBox";
            this.battleDialogueTextBox.Size = new System.Drawing.Size(592, 124);
            this.battleDialogueTextBox.TabIndex = 3;
            this.battleDialogueTextBox.Text = "";
            this.battleDialogueTextBox.TextChanged += new System.EventHandler(this.battleDialogueTextBox_TextChanged);
            this.battleDialogueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.battleDialogueTextBox_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.battleDlgType,
            this.battleDialogueNum,
            this.searchBox,
            this.searchButton,
            this.bonusTextBox,
            this.bonusPreview});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(616, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // battleDlgType
            // 
            this.battleDlgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.battleDlgType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.battleDlgType.Items.AddRange(new object[] {
            "Battle Dialogues",
            "Battle Messages",
            "Flower Bonus"});
            this.battleDlgType.Name = "battleDlgType";
            this.battleDlgType.Size = new System.Drawing.Size(120, 25);
            this.battleDlgType.SelectedIndexChanged += new System.EventHandler(this.battleDlgType_SelectedIndexChanged);
            // 
            // battleDialogueNum
            // 
            this.battleDialogueNum.AutoSize = false;
            this.battleDialogueNum.ContextMenuStrip = null;
            this.battleDialogueNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.battleDialogueNum.Hexadecimal = false;
            this.battleDialogueNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.battleDialogueNum.Location = new System.Drawing.Point(131, 2);
            this.battleDialogueNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.battleDialogueNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.battleDialogueNum.Name = "battleDialogueNum";
            this.battleDialogueNum.Size = new System.Drawing.Size(60, 21);
            this.battleDialogueNum.Text = "0";
            this.battleDialogueNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.battleDialogueNum.ValueChanged += new System.EventHandler(this.battleDialogueNum_ValueChanged);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(120, 25);
            // 
            // searchButton
            // 
            this.searchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchButton.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(23, 22);
            this.searchButton.Text = "Search For Dialogue";
            // 
            // bonusTextBox
            // 
            this.bonusTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bonusTextBox.MaxLength = 16;
            this.bonusTextBox.Name = "bonusTextBox";
            this.bonusTextBox.Size = new System.Drawing.Size(120, 25);
            this.bonusTextBox.Visible = false;
            this.bonusTextBox.TextChanged += new System.EventHandler(this.bonusTextBox_TextChanged);
            // 
            // bonusPreview
            // 
            this.bonusPreview.AutoSize = false;
            this.bonusPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bonusPreview.BackgroundImage")));
            this.bonusPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bonusPreview.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.bonusPreview.Name = "bonusPreview";
            this.bonusPreview.Size = new System.Drawing.Size(128, 8);
            this.bonusPreview.Visible = false;
            this.bonusPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.bonusPreview_Paint);
            // 
            // pictureBoxBattleDialogue
            // 
            this.pictureBoxBattleDialogue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxBattleDialogue.BackgroundImage")));
            this.pictureBoxBattleDialogue.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBattleDialogue.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBattleDialogue.Name = "pictureBoxBattleDialogue";
            this.pictureBoxBattleDialogue.Size = new System.Drawing.Size(616, 32);
            this.pictureBoxBattleDialogue.TabIndex = 520;
            this.pictureBoxBattleDialogue.TabStop = false;
            this.pictureBoxBattleDialogue.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBattleDialogue_Paint);
            this.pictureBoxBattleDialogue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBattleDialogue_MouseDown);
            this.pictureBoxBattleDialogue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBattleDialogue_MouseMove);
            this.pictureBoxBattleDialogue.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxBattleDialogue_PreviewKeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageUp,
            this.pageDown,
            this.toolStripSeparator1,
            this.textView,
            this.toolStripSeparator4,
            this.availableBytes});
            this.toolStrip2.Location = new System.Drawing.Point(0, 32);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(616, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // pageUp
            // 
            this.pageUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageUp.Image = ((System.Drawing.Image)(resources.GetObject("pageUp.Image")));
            this.pageUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pageUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pageUp.Name = "pageUp";
            this.pageUp.Size = new System.Drawing.Size(23, 22);
            this.pageUp.Text = "Page Up";
            this.pageUp.Click += new System.EventHandler(this.pageUp_Click);
            // 
            // pageDown
            // 
            this.pageDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageDown.Image = ((System.Drawing.Image)(resources.GetObject("pageDown.Image")));
            this.pageDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pageDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pageDown.Name = "pageDown";
            this.pageDown.Size = new System.Drawing.Size(23, 22);
            this.pageDown.Text = "Page Down";
            this.pageDown.Click += new System.EventHandler(this.pageDown_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // textView
            // 
            this.textView.CheckOnClick = true;
            this.textView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.textView.Image = ((System.Drawing.Image)(resources.GetObject("textView.Image")));
            this.textView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.textView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.textView.Name = "textView";
            this.textView.Size = new System.Drawing.Size(23, 22);
            this.textView.Text = "Text View";
            this.textView.Click += new System.EventHandler(this.textView_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // availableBytes
            // 
            this.availableBytes.Name = "availableBytes";
            this.availableBytes.Size = new System.Drawing.Size(78, 22);
            this.availableBytes.Text = "characters left";
            // 
            // newLine
            // 
            this.newLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newLine.Image = ((System.Drawing.Image)(resources.GetObject("newLine.Image")));
            this.newLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newLine.Name = "newLine";
            this.newLine.Size = new System.Drawing.Size(21, 20);
            this.newLine.Text = "New Line";
            this.newLine.Click += new System.EventHandler(this.newLine_Click);
            // 
            // endString
            // 
            this.endString.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.endString.Image = ((System.Drawing.Image)(resources.GetObject("endString.Image")));
            this.endString.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.endString.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.endString.Name = "endString";
            this.endString.Size = new System.Drawing.Size(21, 20);
            this.endString.Text = "End String";
            this.endString.Click += new System.EventHandler(this.endString_Click);
            // 
            // pause60f
            // 
            this.pause60f.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pause60f.Image = ((System.Drawing.Image)(resources.GetObject("pause60f.Image")));
            this.pause60f.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pause60f.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pause60f.Name = "pause60f";
            this.pause60f.Size = new System.Drawing.Size(21, 20);
            this.pause60f.Text = "Pause 1 second";
            this.pause60f.Click += new System.EventHandler(this.pause60f_Click);
            // 
            // pauseA
            // 
            this.pauseA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseA.Image = ((System.Drawing.Image)(resources.GetObject("pauseA.Image")));
            this.pauseA.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pauseA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseA.Name = "pauseA";
            this.pauseA.Size = new System.Drawing.Size(21, 20);
            this.pauseA.Text = "Pause, wait for input";
            this.pauseA.Click += new System.EventHandler(this.pauseA_Click);
            // 
            // pauseFrames
            // 
            this.pauseFrames.CheckOnClick = true;
            this.pauseFrames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseFrames.Image = ((System.Drawing.Image)(resources.GetObject("pauseFrames.Image")));
            this.pauseFrames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pauseFrames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseFrames.Name = "pauseFrames";
            this.pauseFrames.Size = new System.Drawing.Size(21, 20);
            this.pauseFrames.Text = "Pause for # of frames";
            this.pauseFrames.Click += new System.EventHandler(this.pauseFrames_Click);
            // 
            // toolStrip4
            // 
            this.toolStrip4.CanOverflow = false;
            this.toolStrip4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reset,
            this.toolStripSeparator2,
            this.toggleText,
            this.grid,
            this.toolStripSeparator3,
            this.openTileEditor,
            this.openGraphics,
            this.toolStripDropDownButton1});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(616, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = ((System.Drawing.Image)(resources.GetObject("reset.Image")));
            this.reset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.Text = "Reset";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toggleText
            // 
            this.toggleText.Checked = true;
            this.toggleText.CheckOnClick = true;
            this.toggleText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleText.Name = "toggleText";
            this.toggleText.Size = new System.Drawing.Size(30, 22);
            this.toggleText.Text = "TXT";
            this.toggleText.ToolTipText = "Show/hide Dialogue Text";
            this.toggleText.Click += new System.EventHandler(this.toggleText_Click);
            // 
            // grid
            // 
            this.grid.CheckOnClick = true;
            this.grid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.grid.Image = global::LAZYSHELL.Properties.Resources.buttonToggleGrid;
            this.grid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.grid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(23, 22);
            this.grid.ToolTipText = "Tile Grid";
            this.grid.Click += new System.EventHandler(this.grid_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // openTileEditor
            // 
            this.openTileEditor.Image = global::LAZYSHELL.Properties.Resources.openTileEditor;
            this.openTileEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTileEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTileEditor.Name = "openTileEditor";
            this.openTileEditor.Size = new System.Drawing.Size(23, 22);
            this.openTileEditor.ToolTipText = "Tile Editor";
            this.openTileEditor.Click += new System.EventHandler(this.openTileEditor_Click);
            // 
            // openGraphics
            // 
            this.openGraphics.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphics.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openGraphics.Name = "openGraphics";
            this.openGraphics.Size = new System.Drawing.Size(23, 22);
            this.openGraphics.ToolTipText = "BPP Graphics";
            this.openGraphics.Click += new System.EventHandler(this.openGraphics_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPaletteDialogue,
            this.openPaletteMenu});
            this.toolStripDropDownButton1.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // openPaletteDialogue
            // 
            this.openPaletteDialogue.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPaletteDialogue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteDialogue.Name = "openPaletteDialogue";
            this.openPaletteDialogue.Size = new System.Drawing.Size(177, 22);
            this.openPaletteDialogue.Text = "Dialogue Font Palette";
            this.openPaletteDialogue.Click += new System.EventHandler(this.openPalettes_Click);
            // 
            // openPaletteMenu
            // 
            this.openPaletteMenu.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPaletteMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteMenu.Name = "openPaletteMenu";
            this.openPaletteMenu.Size = new System.Drawing.Size(177, 22);
            this.openPaletteMenu.Text = "Menu Font Palette";
            this.openPaletteMenu.Click += new System.EventHandler(this.openPaletteMenu_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLine,
            this.endString,
            this.pause60f,
            this.pauseA,
            this.pauseFrames});
            this.toolStrip3.Location = new System.Drawing.Point(592, 57);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(24, 124);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.battleDialogueTextBox);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Controls.Add(this.pictureBoxBattleDialogue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 181);
            this.panel1.TabIndex = 2;
            // 
            // BattleDialogues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 231);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BattleDialogues";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBattleDialogue)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.RichTextBox battleDialogueTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox battleDlgType;
        private System.Windows.Forms.PictureBox pictureBoxBattleDialogue;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton pageUp;
        private System.Windows.Forms.ToolStripButton pageDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel availableBytes;
        private System.Windows.Forms.ToolStripButton newLine;
        private System.Windows.Forms.ToolStripButton endString;
        private System.Windows.Forms.ToolStripButton pause60f;
        private System.Windows.Forms.ToolStripButton pauseA;
        private System.Windows.Forms.ToolStripButton pauseFrames;
        private ToolStripNumericUpDown battleDialogueNum;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton openTileEditor;
        private System.Windows.Forms.ToolStripButton openGraphics;
        private System.Windows.Forms.ToolStripButton searchButton;
        private System.Windows.Forms.ToolStripButton textView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripButton reset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton grid;
        private System.Windows.Forms.ToolStripButton toggleText;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openPaletteDialogue;
        private System.Windows.Forms.ToolStripMenuItem openPaletteMenu;
        private System.Windows.Forms.ToolStripTextBox bonusTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripLabel bonusPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}