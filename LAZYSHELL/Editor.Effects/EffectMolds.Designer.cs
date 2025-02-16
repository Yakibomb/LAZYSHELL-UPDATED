
namespace LAZYSHELL
{
    partial class EffectMolds
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
            this.e_moldWidth = new LAZYSHELL.ToolStripNumericUpDown();
            this.e_moldHeight = new LAZYSHELL.ToolStripNumericUpDown();
            this.panelMoldImage = new LAZYSHELL.NewPanel();
            this.pictureBoxE_Mold = new LAZYSHELL.NewPictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.draw = new System.Windows.Forms.ToolStripButton();
            this.erase = new System.Windows.Forms.ToolStripButton();
            this.select = new System.Windows.Forms.ToolStripButton();
            this.selectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cut = new System.Windows.Forms.ToolStripButton();
            this.copy = new System.Windows.Forms.ToolStripButton();
            this.paste = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mirror = new System.Windows.Forms.ToolStripButton();
            this.invert = new System.Windows.Forms.ToolStripButton();
            this.e_moldShowGrid = new System.Windows.Forms.ToolStripButton();
            this.e_moldZoomIn = new System.Windows.Forms.ToolStripButton();
            this.e_moldZoomOut = new System.Windows.Forms.ToolStripButton();
            this.panel105 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxEffectTileset = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.openTileEditor = new System.Windows.Forms.ToolStripButton();
            this.label86 = new System.Windows.Forms.Label();
            this.e_tileSetSize = new System.Windows.Forms.NumericUpDown();
            this.e_molds = new LAZYSHELL.NewListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.importIntoTilemap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.newMold = new System.Windows.Forms.ToolStripButton();
            this.deleteMold = new System.Windows.Forms.ToolStripButton();
            this.duplicateMold = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.showBG = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleZoomBox = new System.Windows.Forms.ToolStripButton();
            this.labelCoords = new System.Windows.Forms.Label();
            this.panelMoldImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxE_Mold)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.panel105.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEffectTileset)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e_tileSetSize)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // e_moldWidth
            // 
            this.e_moldWidth.AutoSize = false;
            this.e_moldWidth.ContextMenuStrip = null;
            this.e_moldWidth.Hexadecimal = false;
            this.e_moldWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldWidth.Location = new System.Drawing.Point(138, 4);
            this.e_moldWidth.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.e_moldWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldWidth.Name = "e_moldWidth";
            this.e_moldWidth.Size = new System.Drawing.Size(40, 17);
            this.e_moldWidth.Text = "1";
            this.e_moldWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldWidth.ValueChanged += new System.EventHandler(this.e_moldWidth_ValueChanged);
            // 
            // e_moldHeight
            // 
            this.e_moldHeight.AutoSize = false;
            this.e_moldHeight.ContextMenuStrip = null;
            this.e_moldHeight.Hexadecimal = false;
            this.e_moldHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldHeight.Location = new System.Drawing.Point(178, 4);
            this.e_moldHeight.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.e_moldHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldHeight.Name = "e_moldHeight";
            this.e_moldHeight.Size = new System.Drawing.Size(40, 17);
            this.e_moldHeight.Text = "1";
            this.e_moldHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e_moldHeight.ValueChanged += new System.EventHandler(this.e_moldHeight_ValueChanged);
            // 
            // panelMoldImage
            // 
            this.panelMoldImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMoldImage.AutoScroll = true;
            this.panelMoldImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMoldImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMoldImage.Controls.Add(this.pictureBoxE_Mold);
            this.panelMoldImage.Location = new System.Drawing.Point(102, 25);
            this.panelMoldImage.Name = "panelMoldImage";
            this.panelMoldImage.Size = new System.Drawing.Size(431, 260);
            this.panelMoldImage.TabIndex = 3;
            // 
            // pictureBoxE_Mold
            // 
            this.pictureBoxE_Mold.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxE_Mold.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBoxE_Mold.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxE_Mold.Name = "pictureBoxE_Mold";
            this.pictureBoxE_Mold.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxE_Mold.TabIndex = 399;
            this.pictureBoxE_Mold.TabStop = false;
            this.pictureBoxE_Mold.Zoom = 1;
            this.pictureBoxE_Mold.ZoomBoxEnabled = false;
            this.pictureBoxE_Mold.ZoomBoxPosition = new System.Drawing.Point(32, 32);
            this.pictureBoxE_Mold.ZoomBoxZoom = 4;
            this.pictureBoxE_Mold.ZoomEnabled = true;
            this.pictureBoxE_Mold.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxE_Mold_Paint);
            this.pictureBoxE_Mold.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxE_Mold_MouseClick);
            this.pictureBoxE_Mold.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxE_Mold_MouseDown);
            this.pictureBoxE_Mold.MouseEnter += new System.EventHandler(this.pictureBoxE_Mold_MouseEnter);
            this.pictureBoxE_Mold.MouseLeave += new System.EventHandler(this.pictureBoxE_Mold_MouseLeave);
            this.pictureBoxE_Mold.MouseHover += new System.EventHandler(this.pictureBoxE_Mold_MouseHover);
            this.pictureBoxE_Mold.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxE_Mold_MouseMove);
            this.pictureBoxE_Mold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxE_Mold_MouseUp);
            this.pictureBoxE_Mold.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxE_Mold_PreviewKeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator6,
            this.mirrorToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.toolStripSeparator5,
            this.saveImageAsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 170);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.cut_small;
            this.cutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cut_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.copyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copy_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.pasteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.paste_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(156, 6);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.mirror_small;
            this.mirrorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.mirror_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.flip_small;
            this.invertToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.invertToolStripMenuItem.Text = "Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invert_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(156, 6);
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.saveImageAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveImageAsToolStripMenuItem.Text = "Save Image As...";
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // toolStrip6
            // 
            this.toolStrip6.AutoSize = false;
            this.toolStrip6.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draw,
            this.erase,
            this.select,
            this.selectAll,
            this.toolStripSeparator4,
            this.cut,
            this.copy,
            this.paste,
            this.delete,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.mirror,
            this.invert});
            this.toolStrip6.Location = new System.Drawing.Point(78, 25);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip6.Size = new System.Drawing.Size(24, 284);
            this.toolStrip6.TabIndex = 2;
            this.toolStrip6.TabStop = true;
            this.toolStrip6.Text = "toolStrip1";
            // 
            // draw
            // 
            this.draw.CheckOnClick = true;
            this.draw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.draw.Image = global::LAZYSHELL.Properties.Resources.draw_small;
            this.draw.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.draw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.draw.Name = "draw";
            this.draw.Size = new System.Drawing.Size(22, 20);
            this.draw.Text = "Draw (D)";
            this.draw.Click += new System.EventHandler(this.draw_Click);
            // 
            // erase
            // 
            this.erase.CheckOnClick = true;
            this.erase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.erase.Image = global::LAZYSHELL.Properties.Resources.erase_small;
            this.erase.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.erase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.erase.Name = "erase";
            this.erase.Size = new System.Drawing.Size(22, 20);
            this.erase.Text = "Erase (E)";
            this.erase.Click += new System.EventHandler(this.erase_Click);
            // 
            // select
            // 
            this.select.CheckOnClick = true;
            this.select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.select.Image = global::LAZYSHELL.Properties.Resources.select_small;
            this.select.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(22, 20);
            this.select.Text = "Select (S)";
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // selectAll
            // 
            this.selectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAll.Image = global::LAZYSHELL.Properties.Resources.selectAll_small;
            this.selectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.selectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(22, 20);
            this.selectAll.Text = "Select All (Ctrl+A)";
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(22, 6);
            // 
            // cut
            // 
            this.cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cut.Image = global::LAZYSHELL.Properties.Resources.cut_small;
            this.cut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(22, 20);
            this.cut.Text = "Cut (Ctrl+X)";
            this.cut.Click += new System.EventHandler(this.cut_Click);
            // 
            // copy
            // 
            this.copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copy.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.copy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(22, 20);
            this.copy.Text = "Copy (Ctrl+C)";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // paste
            // 
            this.paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paste.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.paste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(22, 20);
            this.paste.Text = "Paste (Ctrl+V)";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delete.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.delete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(22, 20);
            this.delete.Text = "Delete (Del)";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(22, 6);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::LAZYSHELL.Properties.Resources.undo_small;
            this.undoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(22, 20);
            this.undoButton.Text = "Undo (Ctrl+Z)";
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::LAZYSHELL.Properties.Resources.redo_small;
            this.redoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(22, 20);
            this.redoButton.Text = "Redo (Ctrl+Y)";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(22, 6);
            // 
            // mirror
            // 
            this.mirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mirror.Image = global::LAZYSHELL.Properties.Resources.mirror_small;
            this.mirror.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mirror.Name = "mirror";
            this.mirror.Size = new System.Drawing.Size(23, 20);
            this.mirror.Text = "Mirror Selection";
            this.mirror.Click += new System.EventHandler(this.mirror_Click);
            // 
            // invert
            // 
            this.invert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.invert.Image = global::LAZYSHELL.Properties.Resources.flip_small;
            this.invert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.invert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.invert.Name = "invert";
            this.invert.Size = new System.Drawing.Size(23, 20);
            this.invert.Text = "Invert Selection";
            this.invert.Click += new System.EventHandler(this.invert_Click);
            // 
            // e_moldShowGrid
            // 
            this.e_moldShowGrid.CheckOnClick = true;
            this.e_moldShowGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.e_moldShowGrid.Image = global::LAZYSHELL.Properties.Resources.buttonToggleGrid;
            this.e_moldShowGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.e_moldShowGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.e_moldShowGrid.Name = "e_moldShowGrid";
            this.e_moldShowGrid.Size = new System.Drawing.Size(23, 22);
            this.e_moldShowGrid.Text = "Tile Grid (G)";
            this.e_moldShowGrid.Click += new System.EventHandler(this.e_moldShowGrid_Click);
            // 
            // e_moldZoomIn
            // 
            this.e_moldZoomIn.CheckOnClick = true;
            this.e_moldZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.e_moldZoomIn.Image = global::LAZYSHELL.Properties.Resources.zoomin_small;
            this.e_moldZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.e_moldZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.e_moldZoomIn.Name = "e_moldZoomIn";
            this.e_moldZoomIn.Size = new System.Drawing.Size(23, 22);
            this.e_moldZoomIn.Text = "Zoom In (Ctrl+Up)";
            this.e_moldZoomIn.Click += new System.EventHandler(this.e_moldZoomIn_Click);
            // 
            // e_moldZoomOut
            // 
            this.e_moldZoomOut.CheckOnClick = true;
            this.e_moldZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.e_moldZoomOut.Image = global::LAZYSHELL.Properties.Resources.zoomout_small;
            this.e_moldZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.e_moldZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.e_moldZoomOut.Name = "e_moldZoomOut";
            this.e_moldZoomOut.Size = new System.Drawing.Size(23, 22);
            this.e_moldZoomOut.Text = "Zoom Out (Ctrl+Down)";
            this.e_moldZoomOut.Click += new System.EventHandler(this.e_moldZoomOut_Click);
            // 
            // panel105
            // 
            this.panel105.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel105.Controls.Add(this.panel1);
            this.panel105.Controls.Add(this.toolStrip2);
            this.panel105.Controls.Add(this.label86);
            this.panel105.Controls.Add(this.e_tileSetSize);
            this.panel105.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel105.Location = new System.Drawing.Point(533, 25);
            this.panel105.Name = "panel105";
            this.panel105.Size = new System.Drawing.Size(132, 284);
            this.panel105.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBoxEffectTileset);
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(132, 238);
            this.panel1.TabIndex = 3;
            // 
            // pictureBoxEffectTileset
            // 
            this.pictureBoxEffectTileset.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxEffectTileset.ContextMenuStrip = this.contextMenuStrip2;
            this.pictureBoxEffectTileset.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxEffectTileset.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxEffectTileset.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxEffectTileset.Name = "pictureBoxEffectTileset";
            this.pictureBoxEffectTileset.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxEffectTileset.TabIndex = 397;
            this.pictureBoxEffectTileset.TabStop = false;
            this.pictureBoxEffectTileset.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxEffectTileset_Paint);
            this.pictureBoxEffectTileset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEffectTileset_MouseDown);
            this.pictureBoxEffectTileset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEffectTileset_MouseMove);
            this.pictureBoxEffectTileset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEffectTileset_MouseUp);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageAsToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip2.Size = new System.Drawing.Size(160, 26);
            // 
            // saveImageAsToolStripMenuItem1
            // 
            this.saveImageAsToolStripMenuItem1.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.saveImageAsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveImageAsToolStripMenuItem1.Name = "saveImageAsToolStripMenuItem1";
            this.saveImageAsToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.saveImageAsToolStripMenuItem1.Text = "Save Image As...";
            this.saveImageAsToolStripMenuItem1.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem1_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTileEditor});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(132, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            // 
            // openTileEditor
            // 
            this.openTileEditor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTileEditor.Image = global::LAZYSHELL.Properties.Resources.openTileEditor;
            this.openTileEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTileEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTileEditor.Name = "openTileEditor";
            this.openTileEditor.Size = new System.Drawing.Size(23, 22);
            this.openTileEditor.ToolTipText = "Tile Editor";
            this.openTileEditor.Click += new System.EventHandler(this.openTileEditor_Click);
            // 
            // label86
            // 
            this.label86.BackColor = System.Drawing.SystemColors.Control;
            this.label86.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label86.Location = new System.Drawing.Point(0, 25);
            this.label86.Name = "label86";
            this.label86.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.label86.Size = new System.Drawing.Size(67, 21);
            this.label86.TabIndex = 1;
            this.label86.Text = "Size";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // e_tileSetSize
            // 
            this.e_tileSetSize.Increment = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.e_tileSetSize.Location = new System.Drawing.Point(67, 25);
            this.e_tileSetSize.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.e_tileSetSize.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.e_tileSetSize.Name = "e_tileSetSize";
            this.e_tileSetSize.Size = new System.Drawing.Size(65, 21);
            this.e_tileSetSize.TabIndex = 2;
            this.e_tileSetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.e_tileSetSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.e_tileSetSize.ValueChanged += new System.EventHandler(this.e_tileSetSize_ValueChanged);
            // 
            // e_molds
            // 
            this.e_molds.Dock = System.Windows.Forms.DockStyle.Left;
            this.e_molds.FormattingEnabled = true;
            this.e_molds.IntegralHeight = false;
            this.e_molds.LastSelectedIndex = -1;
            this.e_molds.Location = new System.Drawing.Point(0, 25);
            this.e_molds.Name = "e_molds";
            this.e_molds.Size = new System.Drawing.Size(78, 284);
            this.e_molds.TabIndex = 1;
            this.e_molds.SelectedIndexChanged += new System.EventHandler(this.e_molds_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importIntoTilemap,
            this.toolStripSeparator9,
            this.newMold,
            this.deleteMold,
            this.duplicateMold,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.e_moldWidth,
            this.e_moldHeight,
            this.toolStripSeparator7,
            this.e_moldShowGrid,
            this.showBG,
            this.toolStripSeparator8,
            this.e_moldZoomIn,
            this.e_moldZoomOut,
            this.toggleZoomBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(665, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // importIntoTilemap
            // 
            this.importIntoTilemap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importIntoTilemap.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.importIntoTilemap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importIntoTilemap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importIntoTilemap.Name = "importIntoTilemap";
            this.importIntoTilemap.Size = new System.Drawing.Size(23, 22);
            this.importIntoTilemap.ToolTipText = "Import Image(s)";
            this.importIntoTilemap.Click += new System.EventHandler(this.importIntoTilemap_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // newMold
            // 
            this.newMold.Image = global::LAZYSHELL.Properties.Resources.new_small;
            this.newMold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newMold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMold.Name = "newMold";
            this.newMold.Size = new System.Drawing.Size(23, 22);
            this.newMold.ToolTipText = "New Mold";
            this.newMold.Click += new System.EventHandler(this.newMold_Click);
            // 
            // deleteMold
            // 
            this.deleteMold.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteMold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteMold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteMold.Name = "deleteMold";
            this.deleteMold.Size = new System.Drawing.Size(23, 22);
            this.deleteMold.ToolTipText = "Delete Mold";
            this.deleteMold.Click += new System.EventHandler(this.deleteMold_Click);
            // 
            // duplicateMold
            // 
            this.duplicateMold.Image = global::LAZYSHELL.Properties.Resources.duplicate_small;
            this.duplicateMold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.duplicateMold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateMold.Name = "duplicateMold";
            this.duplicateMold.Size = new System.Drawing.Size(23, 22);
            this.duplicateMold.ToolTipText = "Duplicate Mold";
            this.duplicateMold.Click += new System.EventHandler(this.duplicateMold_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel1.Text = "Size";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // showBG
            // 
            this.showBG.Checked = true;
            this.showBG.CheckOnClick = true;
            this.showBG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showBG.Image = global::LAZYSHELL.Properties.Resources.checkerboard;
            this.showBG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showBG.Name = "showBG";
            this.showBG.Size = new System.Drawing.Size(23, 22);
            this.showBG.ToolTipText = "Background Transparency (B)";
            this.showBG.Click += new System.EventHandler(this.showBG_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toggleZoomBox
            // 
            this.toggleZoomBox.CheckOnClick = true;
            this.toggleZoomBox.Image = global::LAZYSHELL.Properties.Resources.zoomBox;
            this.toggleZoomBox.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleZoomBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleZoomBox.Name = "toggleZoomBox";
            this.toggleZoomBox.Size = new System.Drawing.Size(23, 22);
            this.toggleZoomBox.ToolTipText = "Zoom Box (Z)";
            this.toggleZoomBox.Click += new System.EventHandler(this.toggleZoomBox_Click);
            // 
            // labelCoords
            // 
            this.labelCoords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCoords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCoords.Location = new System.Drawing.Point(102, 285);
            this.labelCoords.Name = "labelCoords";
            this.labelCoords.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.labelCoords.Size = new System.Drawing.Size(431, 24);
            this.labelCoords.TabIndex = 4;
            this.labelCoords.Text = "(x: 0, y: 0) Pixel";
            this.labelCoords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EffectMolds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 309);
            this.ControlBox = false;
            this.Controls.Add(this.panelMoldImage);
            this.Controls.Add(this.labelCoords);
            this.Controls.Add(this.panel105);
            this.Controls.Add(this.toolStrip6);
            this.Controls.Add(this.e_molds);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EffectMolds";
            this.panelMoldImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxE_Mold)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.panel105.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEffectTileset)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e_tileSetSize)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private LAZYSHELL.NewPanel panelMoldImage;
        private LAZYSHELL.NewPictureBox pictureBoxE_Mold;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripButton e_moldShowGrid;
        private System.Windows.Forms.ToolStripButton draw;
        private System.Windows.Forms.ToolStripButton erase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton e_moldZoomIn;
        private System.Windows.Forms.ToolStripButton e_moldZoomOut;
        private System.Windows.Forms.Panel panel105;
        private System.Windows.Forms.PictureBox pictureBoxEffectTileset;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.NumericUpDown e_tileSetSize;
        private LAZYSHELL.NewListBox e_molds;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newMold;
        private System.Windows.Forms.ToolStripButton deleteMold;
        private System.Windows.Forms.ToolStripButton duplicateMold;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton select;
        private System.Windows.Forms.ToolStripButton cut;
        private System.Windows.Forms.ToolStripButton copy;
        private System.Windows.Forms.ToolStripButton paste;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton openTileEditor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton mirror;
        private System.Windows.Forms.ToolStripButton invert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton selectAll;
        private ToolStripNumericUpDown e_moldWidth;
        private ToolStripNumericUpDown e_moldHeight;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton showBG;
        private System.Windows.Forms.ToolStripButton importIntoTilemap;
        private System.Windows.Forms.Label labelCoords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toggleZoomBox;
    }
}