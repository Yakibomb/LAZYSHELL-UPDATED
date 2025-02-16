using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    partial class Sprites
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
            this.PlaybackSequence = new System.ComponentModel.BackgroundWorker();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.panelMolds = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.paletteIndex = new System.Windows.Forms.NumericUpDown();
            this.label71 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.paletteOffset = new System.Windows.Forms.NumericUpDown();
            this.graphicOffset = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.animationAvailableBytes = new System.Windows.Forms.Label();
            this.animationVRAM = new System.Windows.Forms.NumericUpDown();
            this.imageNum = new System.Windows.Forms.NumericUpDown();
            this.animationPacket = new System.Windows.Forms.NumericUpDown();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.name = new System.Windows.Forms.ToolStripComboBox();
            this.number = new LAZYSHELL.ToolStripNumericUpDown();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchEffectNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openPalettes = new System.Windows.Forms.ToolStripButton();
            this.openGraphics = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripButton();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.animationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allMoldImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.clear = new System.Windows.Forms.ToolStripButton();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.npcPacketButton = new System.Windows.Forms.ToolStripButton();
            this.hexEditor = new System.Windows.Forms.ToolStripButton();
            this.previewerButton = new System.Windows.Forms.ToolStripButton();
            this.openSequences = new System.Windows.Forms.ToolStripButton();
            this.openMolds = new System.Windows.Forms.ToolStripButton();
            this.showMain = new System.Windows.Forms.ToolStripButton();
            this.characterNumLabel = new System.Windows.Forms.Label();
            this.panelSequences = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteIndex)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animationVRAM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationPacket)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlaybackSequence
            // 
            this.PlaybackSequence.WorkerReportsProgress = true;
            this.PlaybackSequence.WorkerSupportsCancellation = true;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(31, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // toolTip2
            // 
            this.toolTip2.IsBalloon = true;
            this.toolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTip2.ToolTipTitle = "WARNING";
            // 
            // toolTip3
            // 
            this.toolTip3.Active = false;
            // 
            // panelMolds
            // 
            this.panelMolds.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMolds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMolds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMolds.Location = new System.Drawing.Point(221, 50);
            this.panelMolds.Name = "panelMolds";
            this.panelMolds.Size = new System.Drawing.Size(571, 371);
            this.panelMolds.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.paletteIndex);
            this.panel1.Controls.Add(this.label71);
            this.panel1.Controls.Add(this.label73);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label72);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.imageNum);
            this.panel1.Controls.Add(this.animationPacket);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 371);
            this.panel1.TabIndex = 0;
            // 
            // paletteIndex
            // 
            this.paletteIndex.Location = new System.Drawing.Point(161, 6);
            this.paletteIndex.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.paletteIndex.Name = "paletteIndex";
            this.paletteIndex.Size = new System.Drawing.Size(49, 21);
            this.paletteIndex.TabIndex = 3;
            this.paletteIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paletteIndex.ValueChanged += new System.EventHandler(this.paletteIndex_ValueChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(10, 10);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(37, 13);
            this.label71.TabIndex = 0;
            this.label71.Text = "Image";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(112, 10);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(41, 13);
            this.label73.TabIndex = 2;
            this.label73.Text = "Palette";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.paletteOffset);
            this.groupBox2.Controls.Add(this.graphicOffset);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(3, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 69);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Properties";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Palette Set";
            // 
            // paletteOffset
            // 
            this.paletteOffset.Location = new System.Drawing.Point(111, 20);
            this.paletteOffset.Maximum = new decimal(new int[] {
            818,
            0,
            0,
            0});
            this.paletteOffset.Name = "paletteOffset";
            this.paletteOffset.Size = new System.Drawing.Size(83, 21);
            this.paletteOffset.TabIndex = 1;
            this.paletteOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paletteOffset.ValueChanged += new System.EventHandler(this.paletteOffset_ValueChanged);
            // 
            // graphicOffset
            // 
            this.graphicOffset.Hexadecimal = true;
            this.graphicOffset.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.graphicOffset.Location = new System.Drawing.Point(111, 41);
            this.graphicOffset.Maximum = new decimal(new int[] {
            3342320,
            0,
            0,
            0});
            this.graphicOffset.Minimum = new decimal(new int[] {
            2621440,
            0,
            0,
            0});
            this.graphicOffset.Name = "graphicOffset";
            this.graphicOffset.Size = new System.Drawing.Size(83, 21);
            this.graphicOffset.TabIndex = 3;
            this.graphicOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.graphicOffset.Value = new decimal(new int[] {
            2621440,
            0,
            0,
            0});
            this.graphicOffset.ValueChanged += new System.EventHandler(this.graphicOffset_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "BPP GFX Offset";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(10, 114);
            this.label72.Name = "label72";
            this.label72.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label72.Size = new System.Drawing.Size(56, 16);
            this.label72.TabIndex = 5;
            this.label72.Text = "Animation";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.animationAvailableBytes);
            this.groupBox1.Controls.Add(this.animationVRAM);
            this.groupBox1.Location = new System.Drawing.Point(3, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 67);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Properties";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "VRAM Size";
            // 
            // animationAvailableBytes
            // 
            this.animationAvailableBytes.BackColor = System.Drawing.Color.Lime;
            this.animationAvailableBytes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.animationAvailableBytes.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.animationAvailableBytes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.animationAvailableBytes.Location = new System.Drawing.Point(6, 17);
            this.animationAvailableBytes.Name = "animationAvailableBytes";
            this.animationAvailableBytes.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.animationAvailableBytes.Size = new System.Drawing.Size(203, 20);
            this.animationAvailableBytes.TabIndex = 0;
            this.animationAvailableBytes.Text = "0 bytes free";
            this.animationAvailableBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // animationVRAM
            // 
            this.animationVRAM.Increment = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.animationVRAM.Location = new System.Drawing.Point(71, 39);
            this.animationVRAM.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.animationVRAM.Minimum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.animationVRAM.Name = "animationVRAM";
            this.animationVRAM.Size = new System.Drawing.Size(60, 21);
            this.animationVRAM.TabIndex = 2;
            this.animationVRAM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationVRAM.Value = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.animationVRAM.ValueChanged += new System.EventHandler(this.animationVRAM_ValueChanged);
            // 
            // imageNum
            // 
            this.imageNum.Location = new System.Drawing.Point(55, 6);
            this.imageNum.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.imageNum.Name = "imageNum";
            this.imageNum.Size = new System.Drawing.Size(49, 21);
            this.imageNum.TabIndex = 1;
            this.imageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.imageNum.ValueChanged += new System.EventHandler(this.imageNum_ValueChanged);
            // 
            // animationPacket
            // 
            this.animationPacket.Location = new System.Drawing.Point(72, 112);
            this.animationPacket.Maximum = new decimal(new int[] {
            443,
            0,
            0,
            0});
            this.animationPacket.Name = "animationPacket";
            this.animationPacket.Size = new System.Drawing.Size(49, 21);
            this.animationPacket.TabIndex = 6;
            this.animationPacket.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationPacket.ValueChanged += new System.EventHandler(this.animationPacket_ValueChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.name,
            this.number,
            this.searchBox,
            this.searchEffectNames,
            this.toolStripSeparator1,
            this.openPalettes,
            this.openGraphics});
            this.toolStrip3.Location = new System.Drawing.Point(0, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(792, 25);
            this.toolStrip3.TabIndex = 1;
            // 
            // name
            // 
            this.name.DropDownHeight = 500;
            this.name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.name.DropDownWidth = 350;
            this.name.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.name.IntegralHeight = false;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(214, 25);
            this.name.SelectedIndexChanged += new System.EventHandler(this.name_SelectedIndexChanged);
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
            this.number.Location = new System.Drawing.Point(225, 2);
            this.number.Maximum = new decimal(new int[] {
            1023,
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
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
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
            this.searchEffectNames.Text = "Search for sprite";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator11,
            this.import,
            this.export,
            this.toolStripSeparator2,
            this.reset,
            this.clear,
            this.helpTips,
            this.baseConvertor,
            this.toolStripSeparator12,
            this.toolStripSeparator3,
            this.npcPacketButton,
            this.hexEditor,
            this.previewerButton,
            this.openSequences,
            this.openMolds,
            this.showMain});
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
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
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
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animationsToolStripMenuItem,
            this.allMoldImagesToolStripMenuItem});
            this.export.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(29, 22);
            // 
            // animationsToolStripMenuItem
            // 
            this.animationsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportData;
            this.animationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.animationsToolStripMenuItem.Name = "animationsToolStripMenuItem";
            this.animationsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.animationsToolStripMenuItem.Text = "Animation(s)...";
            this.animationsToolStripMenuItem.Click += new System.EventHandler(this.export_Click);
            // 
            // allMoldImagesToolStripMenuItem
            // 
            this.allMoldImagesToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.allMoldImagesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allMoldImagesToolStripMenuItem.Name = "allMoldImagesToolStripMenuItem";
            this.allMoldImagesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.allMoldImagesToolStripMenuItem.Text = "Sprite image(s)...";
            this.allMoldImagesToolStripMenuItem.Click += new System.EventHandler(this.allMoldImagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.reset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.ToolTipText = "Reset";
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
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // npcPacketButton
            // 
            this.npcPacketButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcPacketButton.Image = global::LAZYSHELL.Properties.Resources.openPackets;
            this.npcPacketButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcPacketButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcPacketButton.Name = "npcPacketButton";
            this.npcPacketButton.Size = new System.Drawing.Size(23, 22);
            this.npcPacketButton.Text = "NPC Packets";
            this.npcPacketButton.Click += new System.EventHandler(this.npcPacketButton_Click);
            // 
            // hexEditor
            // 
            this.hexEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hexEditor.Image = global::LAZYSHELL.Properties.Resources.hexEditor;
            this.hexEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hexEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hexEditor.Name = "hexEditor";
            this.hexEditor.Size = new System.Drawing.Size(23, 22);
            this.hexEditor.Text = "Hex Editor";
            this.hexEditor.Click += new System.EventHandler(this.hexViewer_Click);
            // 
            // previewerButton
            // 
            this.previewerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previewerButton.Image = global::LAZYSHELL.Properties.Resources.preview;
            this.previewerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previewerButton.Name = "previewerButton";
            this.previewerButton.Size = new System.Drawing.Size(23, 22);
            this.previewerButton.Text = "Open previewer";
            this.previewerButton.Click += new System.EventHandler(this.previewer_Click);
            // 
            // openSequences
            // 
            this.openSequences.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openSequences.CheckOnClick = true;
            this.openSequences.Image = global::LAZYSHELL.Properties.Resources.openSequences;
            this.openSequences.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSequences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSequences.Name = "openSequences";
            this.openSequences.Size = new System.Drawing.Size(23, 22);
            this.openSequences.ToolTipText = "Sequences";
            this.openSequences.Click += new System.EventHandler(this.openSequences_Click);
            // 
            // openMolds
            // 
            this.openMolds.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openMolds.CheckOnClick = true;
            this.openMolds.Image = global::LAZYSHELL.Properties.Resources.openMolds;
            this.openMolds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openMolds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMolds.Name = "openMolds";
            this.openMolds.Size = new System.Drawing.Size(23, 22);
            this.openMolds.ToolTipText = "Molds";
            this.openMolds.Click += new System.EventHandler(this.openMolds_Click);
            // 
            // showMain
            // 
            this.showMain.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            // characterNumLabel
            // 
            this.characterNumLabel.BackColor = System.Drawing.SystemColors.Info;
            this.characterNumLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.characterNumLabel.Location = new System.Drawing.Point(234, 0);
            this.characterNumLabel.Name = "characterNumLabel";
            this.characterNumLabel.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.characterNumLabel.Size = new System.Drawing.Size(100, 18);
            this.characterNumLabel.TabIndex = 5;
            this.characterNumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.characterNumLabel.Visible = false;
            // 
            // panelSequences
            // 
            this.panelSequences.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSequences.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSequences.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSequences.Location = new System.Drawing.Point(0, 421);
            this.panelSequences.Name = "panelSequences";
            this.panelSequences.Size = new System.Drawing.Size(792, 370);
            this.panelSequences.TabIndex = 6;
            // 
            // Sprites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 791);
            this.Controls.Add(this.panelMolds);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.characterNumLabel);
            this.Controls.Add(this.panelSequences);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.mainSprites_2_ico;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Sprites";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SPRITES - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sprites_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteIndex)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animationVRAM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animationPacket)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private BackgroundWorker PlaybackSequence;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator8;
        private Label animationAvailableBytes;
        private Label label23;
        private NumericUpDown paletteOffset;
        private Label label9;
        private NumericUpDown graphicOffset;
        private Label label18;
        private NumericUpDown animationVRAM;
        private Label label72;
        private NumericUpDown animationPacket;
        private Label label73;
        private Label label71;
        private NumericUpDown paletteIndex;
        private NumericUpDown imageNum;
        private ToolTip toolTip1;
        private Panel panelMolds;
        private ToolTip toolTip2;
        private Label characterNumLabel;
        private ToolStrip toolStrip2;
        private ToolStripButton save;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton import;
        private ToolStripButton clear;
        private ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripComboBox name;
        private ToolStripButton searchEffectNames;
        private ToolStripTextBox searchBox;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton showMain;
        private ToolStripButton openPalettes;
        private ToolStripButton openGraphics;
        private ToolStripButton openMolds;
        private ToolStripButton openSequences;
        private Panel panel1;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripButton helpTips;
        private ToolStripButton baseConvertor;
        private ToolStripDropDownButton export;
        private ToolStripMenuItem allMoldImagesToolStripMenuItem;
        private ToolStripMenuItem animationsToolStripMenuItem;
        private ToolStripNumericUpDown number;
        private ToolTip toolTip3;
        private ToolStripButton hexEditor;
        private ToolStripButton reset;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Panel panelSequences;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton npcPacketButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton previewerButton;
    }
}

