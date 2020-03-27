
namespace LAZYSHELL
{
    partial class Effects
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
            this.number = new LAZYSHELL.ToolStripNumericUpDown();
            this.yNegShift = new System.Windows.Forms.NumericUpDown();
            this.label96 = new System.Windows.Forms.Label();
            this.xNegShift = new System.Windows.Forms.NumericUpDown();
            this.e_paletteIndex = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.e_codec = new System.Windows.Forms.ComboBox();
            this.e_availableBytes = new System.Windows.Forms.Label();
            this.e_paletteSetSize = new System.Windows.Forms.NumericUpDown();
            this.label107 = new System.Windows.Forms.Label();
            this.imageNum = new System.Windows.Forms.NumericUpDown();
            this.label90 = new System.Windows.Forms.Label();
            this.e_graphicSetSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripButton();
            this.export = new System.Windows.Forms.ToolStripButton();
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.clear = new System.Windows.Forms.ToolStripButton();
            this.cullAnimations = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.name = new System.Windows.Forms.ToolStripComboBox();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchEffectNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showMain = new System.Windows.Forms.ToolStripButton();
            this.openMolds = new System.Windows.Forms.ToolStripButton();
            this.openSequences = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.openPalettes = new System.Windows.Forms.ToolStripButton();
            this.openGraphics = new System.Windows.Forms.ToolStripButton();
            this.panelMolds = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelSequences = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.yNegShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xNegShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_paletteIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_paletteSetSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_graphicSetSize)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // number
            // 
            this.number.AutoSize = false;
            this.number.ContextMenuStrip = null;
            this.number.Hexadecimal = false;
            this.number.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.number.Location = new System.Drawing.Point(223, 2);
            this.number.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.number.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(50, 21);
            this.number.Text = "0";
            this.number.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.number.ValueChanged += new System.EventHandler(this.number_ValueChanged);
            // 
            // yNegShift
            // 
            this.yNegShift.Location = new System.Drawing.Point(93, 148);
            this.yNegShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.yNegShift.Name = "yNegShift";
            this.yNegShift.Size = new System.Drawing.Size(43, 21);
            this.yNegShift.TabIndex = 7;
            this.yNegShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yNegShift.ValueChanged += new System.EventHandler(this.yNegShift_ValueChanged);
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(10, 150);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(31, 13);
            this.label96.TabIndex = 5;
            this.label96.Text = "(X,Y)";
            // 
            // xNegShift
            // 
            this.xNegShift.Location = new System.Drawing.Point(49, 148);
            this.xNegShift.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.xNegShift.Name = "xNegShift";
            this.xNegShift.Size = new System.Drawing.Size(43, 21);
            this.xNegShift.TabIndex = 6;
            this.xNegShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xNegShift.ValueChanged += new System.EventHandler(this.xNegShift_ValueChanged);
            // 
            // e_paletteIndex
            // 
            this.e_paletteIndex.Location = new System.Drawing.Point(161, 6);
            this.e_paletteIndex.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.e_paletteIndex.Name = "e_paletteIndex";
            this.e_paletteIndex.Size = new System.Drawing.Size(49, 21);
            this.e_paletteIndex.TabIndex = 3;
            this.e_paletteIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.e_paletteIndex.ValueChanged += new System.EventHandler(this.e_paletteIndex_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Palette";
            // 
            // e_codec
            // 
            this.e_codec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.e_codec.FormattingEnabled = true;
            this.e_codec.Items.AddRange(new object[] {
            "4bpp",
            "2bpp"});
            this.e_codec.Location = new System.Drawing.Point(79, 82);
            this.e_codec.Name = "e_codec";
            this.e_codec.Size = new System.Drawing.Size(62, 21);
            this.e_codec.TabIndex = 6;
            this.e_codec.SelectedIndexChanged += new System.EventHandler(this.e_codec_SelectedIndexChanged);
            // 
            // e_availableBytes
            // 
            this.e_availableBytes.BackColor = System.Drawing.Color.Lime;
            this.e_availableBytes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.e_availableBytes.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.e_availableBytes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.e_availableBytes.Location = new System.Drawing.Point(6, 17);
            this.e_availableBytes.Name = "e_availableBytes";
            this.e_availableBytes.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.e_availableBytes.Size = new System.Drawing.Size(203, 20);
            this.e_availableBytes.TabIndex = 0;
            this.e_availableBytes.Text = "0 bytes free";
            this.e_availableBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // e_paletteSetSize
            // 
            this.e_paletteSetSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.e_paletteSetSize.Location = new System.Drawing.Point(79, 40);
            this.e_paletteSetSize.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.e_paletteSetSize.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.e_paletteSetSize.Name = "e_paletteSetSize";
            this.e_paletteSetSize.Size = new System.Drawing.Size(62, 21);
            this.e_paletteSetSize.TabIndex = 2;
            this.e_paletteSetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.e_paletteSetSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.e_paletteSetSize.ValueChanged += new System.EventHandler(this.e_paletteSetSize_ValueChanged);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(6, 42);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(63, 13);
            this.label107.TabIndex = 1;
            this.label107.Text = "Palette Size";
            // 
            // imageNum
            // 
            this.imageNum.Location = new System.Drawing.Point(55, 6);
            this.imageNum.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.imageNum.Name = "imageNum";
            this.imageNum.Size = new System.Drawing.Size(49, 21);
            this.imageNum.TabIndex = 1;
            this.imageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.imageNum.ValueChanged += new System.EventHandler(this.imageNum_ValueChanged);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(6, 84);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(58, 13);
            this.label90.TabIndex = 5;
            this.label90.Text = "BPP Codec";
            // 
            // e_graphicSetSize
            // 
            this.e_graphicSetSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.e_graphicSetSize.Location = new System.Drawing.Point(79, 61);
            this.e_graphicSetSize.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.e_graphicSetSize.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.e_graphicSetSize.Name = "e_graphicSetSize";
            this.e_graphicSetSize.Size = new System.Drawing.Size(62, 21);
            this.e_graphicSetSize.TabIndex = 4;
            this.e_graphicSetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.e_graphicSetSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.e_graphicSetSize.ValueChanged += new System.EventHandler(this.e_graphicSetSize_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Image";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(6, 63);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(65, 13);
            this.label89.TabIndex = 3;
            this.label89.Text = "Graphic Size";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator1,
            this.import,
            this.export,
            this.reset,
            this.clear,
            this.cullAnimations,
            this.toolStripSeparator12,
            this.helpTips,
            this.baseConvertor});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(792, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
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
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.reset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.Text = "Reset";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // clear
            // 
            this.clear.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(23, 22);
            this.clear.ToolTipText = "Clear";
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // cullAnimations
            // 
            this.cullAnimations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cullAnimations.Image = global::LAZYSHELL.Properties.Resources.broom;
            this.cullAnimations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cullAnimations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cullAnimations.Name = "cullAnimations";
            this.cullAnimations.Size = new System.Drawing.Size(23, 22);
            this.cullAnimations.ToolTipText = "Clean unused animation data";
            this.cullAnimations.Click += new System.EventHandler(this.cullAnimations_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // helpTips
            // 
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
            this.baseConvertor.CheckOnClick = true;
            this.baseConvertor.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.baseConvertor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.baseConvertor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConvertor.Name = "baseConvertor";
            this.baseConvertor.Size = new System.Drawing.Size(23, 22);
            this.baseConvertor.ToolTipText = "Base Convertor";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.name,
            this.number,
            this.searchBox,
            this.searchEffectNames,
            this.toolStripSeparator2,
            this.showMain,
            this.openMolds,
            this.openSequences,
            this.toolStripSeparator4,
            this.openPalettes,
            this.openGraphics});
            this.toolStrip3.Location = new System.Drawing.Point(0, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(792, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // name
            // 
            this.name.DropDownHeight = 500;
            this.name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.name.DropDownWidth = 310;
            this.name.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.name.IntegralHeight = false;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(214, 25);
            this.name.SelectedIndexChanged += new System.EventHandler(this.name_SelectedIndexChanged);
            // 
            // searchBox
            // 
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(200, 25);
            // 
            // searchEffectNames
            // 
            this.searchEffectNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchEffectNames.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchEffectNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchEffectNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchEffectNames.Name = "searchEffectNames";
            this.searchEffectNames.Size = new System.Drawing.Size(23, 22);
            this.searchEffectNames.Text = "Search for effect";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showMain
            // 
            this.showMain.Checked = true;
            this.showMain.CheckOnClick = true;
            this.showMain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMain.Image = global::LAZYSHELL.Properties.Resources.showMain;
            this.showMain.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showMain.Name = "showMain";
            this.showMain.Size = new System.Drawing.Size(23, 22);
            this.showMain.ToolTipText = "Main";
            this.showMain.Click += new System.EventHandler(this.showMain_Click);
            // 
            // openMolds
            // 
            this.openMolds.CheckOnClick = true;
            this.openMolds.Image = global::LAZYSHELL.Properties.Resources.mainEffects;
            this.openMolds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openMolds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMolds.Name = "openMolds";
            this.openMolds.Size = new System.Drawing.Size(23, 22);
            this.openMolds.ToolTipText = "Molds";
            this.openMolds.Click += new System.EventHandler(this.openMolds_Click);
            // 
            // openSequences
            // 
            this.openSequences.CheckOnClick = true;
            this.openSequences.Image = global::LAZYSHELL.Properties.Resources.openEffectSequences;
            this.openSequences.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSequences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSequences.Name = "openSequences";
            this.openSequences.Size = new System.Drawing.Size(23, 22);
            this.openSequences.ToolTipText = "Frames";
            this.openSequences.Click += new System.EventHandler(this.openSequences_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // openPalettes
            // 
            this.openPalettes.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPalettes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPalettes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPalettes.Name = "openPalettes";
            this.openPalettes.Size = new System.Drawing.Size(23, 22);
            this.openPalettes.ToolTipText = "Palettes";
            this.openPalettes.Click += new System.EventHandler(this.openPalettes_Click);
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
            // panelMolds
            // 
            this.panelMolds.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMolds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMolds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMolds.Location = new System.Drawing.Point(221, 50);
            this.panelMolds.Name = "panelMolds";
            this.panelMolds.Size = new System.Drawing.Size(571, 319);
            this.panelMolds.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.yNegShift);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label96);
            this.panel2.Controls.Add(this.xNegShift);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.e_paletteIndex);
            this.panel2.Controls.Add(this.imageNum);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(221, 319);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.e_codec);
            this.groupBox1.Controls.Add(this.e_availableBytes);
            this.groupBox1.Controls.Add(this.label89);
            this.groupBox1.Controls.Add(this.e_graphicSetSize);
            this.groupBox1.Controls.Add(this.label90);
            this.groupBox1.Controls.Add(this.e_paletteSetSize);
            this.groupBox1.Controls.Add(this.label107);
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Properties";
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // panelSequences
            // 
            this.panelSequences.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSequences.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSequences.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSequences.Location = new System.Drawing.Point(0, 369);
            this.panelSequences.Name = "panelSequences";
            this.panelSequences.Size = new System.Drawing.Size(792, 370);
            this.panelSequences.TabIndex = 3;
            // 
            // Effects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 739);
            this.Controls.Add(this.panelMolds);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panelSequences);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Effects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EFFECTS - Lazy Shell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Effects_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.yNegShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xNegShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_paletteIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_paletteSetSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e_graphicSetSize)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.NumericUpDown yNegShift;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.NumericUpDown xNegShift;
        private System.Windows.Forms.NumericUpDown e_paletteIndex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox e_codec;
        private System.Windows.Forms.Label e_availableBytes;
        private System.Windows.Forms.NumericUpDown e_paletteSetSize;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.NumericUpDown imageNum;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.NumericUpDown e_graphicSetSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton import;
        private System.Windows.Forms.ToolStripButton export;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripComboBox name;
        private System.Windows.Forms.ToolStripButton searchEffectNames;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton openPalettes;
        private System.Windows.Forms.ToolStripButton openGraphics;
        private System.Windows.Forms.ToolStripButton openSequences;
        private System.Windows.Forms.ToolStripButton openMolds;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.Panel panelMolds;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton showMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton clear;
        private ToolStripNumericUpDown number;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton reset;
        private System.Windows.Forms.ToolStripButton cullAnimations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelSequences;
    }
}