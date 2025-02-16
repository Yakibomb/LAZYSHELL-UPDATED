
namespace LAZYSHELL
{
    partial class HexEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.gotoAddress = new System.Windows.Forms.ToolStripTextBox();
            this.gotoAddressButton = new System.Windows.Forms.ToolStripButton();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.baseConvDec = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.baseConvHex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.fillWith = new System.Windows.Forms.ToolStripTextBox();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.info_sel = new System.Windows.Forms.Label();
            this.info_value = new System.Windows.Forms.Label();
            this.info_offset = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.currentROMData = new LAZYSHELL.NewRichTextBox();
            this.originalROMData = new LAZYSHELL.NewRichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.viewCurrent = new System.Windows.Forms.ToolStripButton();
            this.compareButton = new System.Windows.Forms.ToolStripButton();
            this.viewOriginal = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copy = new System.Windows.Forms.ToolStripButton();
            this.paste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undo = new System.Windows.Forms.ToolStripButton();
            this.redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.findRecentChangeLeft = new System.Windows.Forms.ToolStripButton();
            this.findRecentChangeRight = new System.Windows.Forms.ToolStripButton();
            this.ROMOffsets = new LAZYSHELL.NewRichTextBox();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.gotoAddress,
            this.gotoAddressButton,
            this.searchBox,
            this.toolStripLabel4,
            this.baseConvDec,
            this.toolStripLabel5,
            this.baseConvHex,
            this.toolStripLabel3,
            this.fillWith});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(505, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.Image = global::LAZYSHELL.Properties.Resources.jumpTo;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel2.ToolTipText = "Search for value(s)";
            // 
            // gotoAddress
            // 
            this.gotoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.gotoAddress.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gotoAddress.MaxLength = 6;
            this.gotoAddress.Name = "gotoAddress";
            this.gotoAddress.Size = new System.Drawing.Size(70, 25);
            this.gotoAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gotoAddress_KeyDown);
            // 
            // gotoAddressButton
            // 
            this.gotoAddressButton.AutoSize = false;
            this.gotoAddressButton.Image = global::LAZYSHELL.Properties.Resources.jumpToRight;
            this.gotoAddressButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.gotoAddressButton.Name = "gotoAddressButton";
            this.gotoAddressButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gotoAddressButton.Size = new System.Drawing.Size(22, 22);
            this.gotoAddressButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.gotoAddressButton.ToolTipText = "Search address (forwards)";
            this.gotoAddressButton.Click += new System.EventHandler(this.gotoAddressButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchBox.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.MaxLength = 24;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(132, 25);
            this.searchBox.Leave += new System.EventHandler(this.searchBox_Leave);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchValues_KeyDown);
            this.searchBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.searchBox_MouseDown);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.AutoSize = false;
            this.toolStripLabel4.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.toolStripLabel4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel4.ToolTipText = "Decimal <-> Hex";
            // 
            // baseConvDec
            // 
            this.baseConvDec.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.baseConvDec.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baseConvDec.MaxLength = 10;
            this.baseConvDec.Name = "baseConvDec";
            this.baseConvDec.Size = new System.Drawing.Size(70, 25);
            this.baseConvDec.TextChanged += new System.EventHandler(this.baseConvDec_TextChanged);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.AutoSize = false;
            this.toolStripLabel5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel5.Text = "$";
            this.toolStripLabel5.ToolTipText = "Hexadecimal";
            // 
            // baseConvHex
            // 
            this.baseConvHex.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.baseConvHex.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baseConvHex.MaxLength = 8;
            this.baseConvHex.Name = "baseConvHex";
            this.baseConvHex.Size = new System.Drawing.Size(70, 25);
            this.baseConvHex.TextChanged += new System.EventHandler(this.baseConvHex_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.AutoSize = false;
            this.toolStripLabel3.Image = global::LAZYSHELL.Properties.Resources.fill_small;
            this.toolStripLabel3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel3.ToolTipText = "Fill Selection";
            // 
            // fillWith
            // 
            this.fillWith.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.fillWith.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fillWith.MaxLength = 2;
            this.fillWith.Name = "fillWith";
            this.fillWith.Size = new System.Drawing.Size(30, 25);
            this.fillWith.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fillWith_KeyDown);
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.info_sel);
            this.panel1.Controls.Add(this.info_value);
            this.panel1.Controls.Add(this.info_offset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 535);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 15);
            this.panel1.TabIndex = 5;
            // 
            // info_sel
            // 
            this.info_sel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.info_sel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info_sel.Location = new System.Drawing.Point(260, 0);
            this.info_sel.Name = "info_sel";
            this.info_sel.Size = new System.Drawing.Size(245, 15);
            this.info_sel.TabIndex = 2;
            this.info_sel.Text = "Sel: 0 (0x0) bytes";
            this.info_sel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // info_value
            // 
            this.info_value.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.info_value.Dock = System.Windows.Forms.DockStyle.Left;
            this.info_value.Location = new System.Drawing.Point(130, 0);
            this.info_value.Name = "info_value";
            this.info_value.Size = new System.Drawing.Size(130, 15);
            this.info_value.TabIndex = 1;
            this.info_value.Text = "Value: 0";
            this.info_value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // info_offset
            // 
            this.info_offset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.info_offset.Dock = System.Windows.Forms.DockStyle.Left;
            this.info_offset.Location = new System.Drawing.Point(0, 0);
            this.info_offset.Name = "info_offset";
            this.info_offset.Size = new System.Drawing.Size(130, 15);
            this.info_offset.TabIndex = 0;
            this.info_offset.Text = "Offset: 000000";
            this.info_offset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.vScrollBar1.LargeChange = 16;
            this.vScrollBar1.Location = new System.Drawing.Point(488, 50);
            this.vScrollBar1.Maximum = 262144;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 485);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.currentROMData);
            this.panel2.Controls.Add(this.originalROMData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(63, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 485);
            this.panel2.TabIndex = 3;
            // 
            // currentROMData
            // 
            this.currentROMData.BackColor = System.Drawing.Color.White;
            this.currentROMData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.currentROMData.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.currentROMData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentROMData.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentROMData.ForeColor = System.Drawing.Color.DarkBlue;
            this.currentROMData.HideSelection = false;
            this.currentROMData.Location = new System.Drawing.Point(0, 0);
            this.currentROMData.Name = "currentROMData";
            this.currentROMData.ReadOnly = true;
            this.currentROMData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.currentROMData.Size = new System.Drawing.Size(425, 485);
            this.currentROMData.TabIndex = 0;
            this.currentROMData.Text = "";
            this.currentROMData.SelectionChanged += new System.EventHandler(this.richTextBox_SelectionChanged);
            this.currentROMData.SizeChanged += new System.EventHandler(this.richTextBox_SizeChanged);
            this.currentROMData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            this.currentROMData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyUp);
            this.currentROMData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseDown);
            this.currentROMData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseUp);
            this.currentROMData.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseWheel);
            this.currentROMData.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.richTextBox_PreviewKeyDown);
            // 
            // originalROMData
            // 
            this.originalROMData.BackColor = System.Drawing.Color.White;
            this.originalROMData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.originalROMData.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.originalROMData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalROMData.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalROMData.ForeColor = System.Drawing.Color.DarkBlue;
            this.originalROMData.HideSelection = false;
            this.originalROMData.Location = new System.Drawing.Point(0, 0);
            this.originalROMData.Name = "originalROMData";
            this.originalROMData.ReadOnly = true;
            this.originalROMData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.originalROMData.Size = new System.Drawing.Size(425, 485);
            this.originalROMData.TabIndex = 6;
            this.originalROMData.Text = "";
            this.originalROMData.Visible = false;
            this.originalROMData.SelectionChanged += new System.EventHandler(this.richTextBox_SelectionChanged);
            this.originalROMData.SizeChanged += new System.EventHandler(this.richTextBox_SizeChanged);
            this.originalROMData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            this.originalROMData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyUp);
            this.originalROMData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseDown);
            this.originalROMData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseUp);
            this.originalROMData.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.richTextBox_MouseWheel);
            this.originalROMData.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.richTextBox_PreviewKeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewCurrent,
            this.compareButton,
            this.viewOriginal,
            this.toolStripSeparator3,
            this.save,
            this.helpTips,
            this.toolStripSeparator1,
            this.copy,
            this.paste,
            this.toolStripSeparator2,
            this.undo,
            this.redo,
            this.toolStripSeparator4,
            this.findRecentChangeLeft,
            this.findRecentChangeRight});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(505, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // viewCurrent
            // 
            this.viewCurrent.Checked = true;
            this.viewCurrent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewCurrent.Image = global::LAZYSHELL.Properties.Resources.cartridge;
            this.viewCurrent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewCurrent.Name = "viewCurrent";
            this.viewCurrent.Size = new System.Drawing.Size(91, 22);
            this.viewCurrent.Text = "Current ROM";
            this.viewCurrent.ToolTipText = "Current ROM";
            this.viewCurrent.Click += new System.EventHandler(this.viewCurrent_Click);
            // 
            // compareButton
            // 
            this.compareButton.Checked = true;
            this.compareButton.CheckOnClick = true;
            this.compareButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.compareButton.Image = global::LAZYSHELL.Properties.Resources.update;
            this.compareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(23, 22);
            this.compareButton.Text = "toolStripButton1";
            this.compareButton.ToolTipText = "Compare new changes between ROMs";
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // viewOriginal
            // 
            this.viewOriginal.Image = global::LAZYSHELL.Properties.Resources.cartridge;
            this.viewOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewOriginal.Name = "viewOriginal";
            this.viewOriginal.Size = new System.Drawing.Size(92, 22);
            this.viewOriginal.Text = "Original ROM";
            this.viewOriginal.ToolTipText = "Original ROM";
            this.viewOriginal.Click += new System.EventHandler(this.viewOriginal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(8, 25);
            // 
            // helpTips
            // 
            this.helpTips.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpTips.CheckOnClick = true;
            this.helpTips.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.Text = "Help Tips";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // copy
            // 
            this.copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copy.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.copy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(23, 22);
            this.copy.Text = "Copy";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // paste
            // 
            this.paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paste.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.paste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(23, 22);
            this.paste.Text = "Paste";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // undo
            // 
            this.undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undo.Image = global::LAZYSHELL.Properties.Resources.undo_small;
            this.undo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(23, 22);
            this.undo.Text = "Undo";
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // redo
            // 
            this.redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redo.Image = global::LAZYSHELL.Properties.Resources.redo_small;
            this.redo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(23, 22);
            this.redo.Text = "Redo";
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // findRecentChangeLeft
            // 
            this.findRecentChangeLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findRecentChangeLeft.Image = ((System.Drawing.Image)(resources.GetObject("findRecentChangeLeft.Image")));
            this.findRecentChangeLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findRecentChangeLeft.Name = "findRecentChangeLeft";
            this.findRecentChangeLeft.Size = new System.Drawing.Size(23, 22);
            this.findRecentChangeLeft.Text = "findRecentChangeLeft";
            this.findRecentChangeLeft.ToolTipText = "Go left to new change";
            this.findRecentChangeLeft.Click += new System.EventHandler(this.findRecentChangeLeft_Click);
            // 
            // findRecentChangeRight
            // 
            this.findRecentChangeRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findRecentChangeRight.Image = ((System.Drawing.Image)(resources.GetObject("findRecentChangeRight.Image")));
            this.findRecentChangeRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findRecentChangeRight.Name = "findRecentChangeRight";
            this.findRecentChangeRight.Size = new System.Drawing.Size(23, 22);
            this.findRecentChangeRight.ToolTipText = "Go right to new change";
            this.findRecentChangeRight.Click += new System.EventHandler(this.findRecentChangeRight_Click);
            // 
            // ROMOffsets
            // 
            this.ROMOffsets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ROMOffsets.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ROMOffsets.Dock = System.Windows.Forms.DockStyle.Left;
            this.ROMOffsets.Enabled = false;
            this.ROMOffsets.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ROMOffsets.Location = new System.Drawing.Point(0, 50);
            this.ROMOffsets.Name = "ROMOffsets";
            this.ROMOffsets.ReadOnly = true;
            this.ROMOffsets.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.ROMOffsets.Size = new System.Drawing.Size(63, 485);
            this.ROMOffsets.TabIndex = 2;
            this.ROMOffsets.Text = "";
            this.ROMOffsets.SizeChanged += new System.EventHandler(this.richTextBox_SizeChanged);
            this.ROMOffsets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            this.ROMOffsets.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyUp);
            this.ROMOffsets.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.richTextBox_PreviewKeyDown);
            // 
            // HexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 550);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ROMOffsets);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Name = "HexEditor";
            this.Text = "HEX EDITOR - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HexViewer_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox gotoAddress;
        private LAZYSHELL.NewRichTextBox ROMOffsets;
        private LAZYSHELL.NewRichTextBox currentROMData;
        private LAZYSHELL.NewRichTextBox originalROMData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label info_value;
        private System.Windows.Forms.Label info_offset;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton undo;
        private System.Windows.Forms.ToolStripButton redo;
        private System.Windows.Forms.ToolStripButton copy;
        private System.Windows.Forms.ToolStripButton paste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label info_sel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox fillWith;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox baseConvDec;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox baseConvHex;
        private System.Windows.Forms.ToolStripButton viewCurrent;
        private System.Windows.Forms.ToolStripButton viewOriginal;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton compareButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton findRecentChangeRight;
        private System.Windows.Forms.ToolStripButton findRecentChangeLeft;
        private System.Windows.Forms.ToolStripButton gotoAddressButton;
    }
}