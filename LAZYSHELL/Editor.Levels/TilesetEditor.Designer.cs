
namespace LAZYSHELL
{
    partial class TilesetEditor
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
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.priority1SetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priority1ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonToggleTileEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonToggleCartGrid = new System.Windows.Forms.ToolStripButton();
            this.buttonToggleBG = new System.Windows.Forms.ToolStripButton();
            this.buttonToggleP1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonEditDelete = new System.Windows.Forms.ToolStripButton();
            this.buttonEditCut = new System.Windows.Forms.ToolStripButton();
            this.buttonEditCopy = new System.Windows.Forms.ToolStripButton();
            this.buttonEditPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonEditMirror = new System.Windows.Forms.ToolStripButton();
            this.buttonEditInvert = new System.Windows.Forms.ToolStripButton();
            this.buttonEditUndo = new System.Windows.Forms.ToolStripButton();
            this.buttonEditRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBoxTilesetL1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBoxTilesetL2 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pictureBoxTilesetL3 = new System.Windows.Forms.PictureBox();
            this.lockEditing = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.labelTileIndex = new System.Windows.Forms.Label();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem2,
            this.copyToolStripMenuItem2,
            this.pasteToolStripMenuItem2,
            this.deleteToolStripMenuItem2,
            this.toolStripSeparator27,
            this.priority1SetToolStripMenuItem,
            this.priority1ClearToolStripMenuItem,
            this.mirrorToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveImageAsToolStripMenuItem,
            this.importImageToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip2.Size = new System.Drawing.Size(159, 236);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // cutToolStripMenuItem2
            // 
            this.cutToolStripMenuItem2.Image = global::LAZYSHELL.Properties.Resources.cut_small;
            this.cutToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cutToolStripMenuItem2.Name = "cutToolStripMenuItem2";
            this.cutToolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.cutToolStripMenuItem2.Text = "Cut";
            this.cutToolStripMenuItem2.Click += new System.EventHandler(this.buttonEditCut_Click);
            // 
            // copyToolStripMenuItem2
            // 
            this.copyToolStripMenuItem2.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.copyToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copyToolStripMenuItem2.Name = "copyToolStripMenuItem2";
            this.copyToolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.copyToolStripMenuItem2.Text = "Copy";
            this.copyToolStripMenuItem2.Click += new System.EventHandler(this.buttonEditCopy_Click);
            // 
            // pasteToolStripMenuItem2
            // 
            this.pasteToolStripMenuItem2.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.pasteToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pasteToolStripMenuItem2.Name = "pasteToolStripMenuItem2";
            this.pasteToolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.pasteToolStripMenuItem2.Text = "Paste";
            this.pasteToolStripMenuItem2.Click += new System.EventHandler(this.buttonEditPaste_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(158, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.buttonEditDelete_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(155, 6);
            // 
            // priority1SetToolStripMenuItem
            // 
            this.priority1SetToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.priority1ON;
            this.priority1SetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.priority1SetToolStripMenuItem.Name = "priority1SetToolStripMenuItem";
            this.priority1SetToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.priority1SetToolStripMenuItem.Text = "Priority 1 set";
            this.priority1SetToolStripMenuItem.Click += new System.EventHandler(this.priority1SetToolStripMenuItem_Click);
            // 
            // priority1ClearToolStripMenuItem
            // 
            this.priority1ClearToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.priority1OFF;
            this.priority1ClearToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.priority1ClearToolStripMenuItem.Name = "priority1ClearToolStripMenuItem";
            this.priority1ClearToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.priority1ClearToolStripMenuItem.Text = "Priority 1 clear";
            this.priority1ClearToolStripMenuItem.Click += new System.EventHandler(this.priority1ClearToolStripMenuItem_Click);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.mirror_small;
            this.mirrorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.mirrorToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.flip_small;
            this.invertToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.invertToolStripMenuItem.Text = "Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.saveImageAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveImageAsToolStripMenuItem.Text = "Save Image As...";
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // importImageToolStripMenuItem
            // 
            this.importImageToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importImage;
            this.importImageToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importImageToolStripMenuItem.Name = "importImageToolStripMenuItem";
            this.importImageToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.importImageToolStripMenuItem.Text = "Import Image...";
            this.importImageToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonToggleTileEditor,
            this.toolStripSeparator1,
            this.buttonToggleCartGrid,
            this.buttonToggleBG,
            this.buttonToggleP1,
            this.toolStripSeparator13,
            this.buttonEditDelete,
            this.buttonEditCut,
            this.buttonEditCopy,
            this.buttonEditPaste,
            this.toolStripSeparator2,
            this.buttonEditMirror,
            this.buttonEditInvert,
            this.buttonEditUndo,
            this.buttonEditRedo,
            this.toolStripSeparator11});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(272, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // buttonToggleTileEditor
            // 
            this.buttonToggleTileEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToggleTileEditor.Image = global::LAZYSHELL.Properties.Resources.openTileEditor;
            this.buttonToggleTileEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonToggleTileEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToggleTileEditor.Name = "buttonToggleTileEditor";
            this.buttonToggleTileEditor.Size = new System.Drawing.Size(23, 22);
            this.buttonToggleTileEditor.ToolTipText = "Open Tile Editor";
            this.buttonToggleTileEditor.Click += new System.EventHandler(this.buttonToggleTileEditor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonToggleCartGrid
            // 
            this.buttonToggleCartGrid.CheckOnClick = true;
            this.buttonToggleCartGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToggleCartGrid.Image = global::LAZYSHELL.Properties.Resources.buttonToggleGrid;
            this.buttonToggleCartGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonToggleCartGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToggleCartGrid.Name = "buttonToggleCartGrid";
            this.buttonToggleCartGrid.Size = new System.Drawing.Size(23, 22);
            this.buttonToggleCartGrid.Text = "Tile Grid";
            this.buttonToggleCartGrid.Click += new System.EventHandler(this.buttonToggleCartGrid_Click);
            // 
            // buttonToggleBG
            // 
            this.buttonToggleBG.CheckOnClick = true;
            this.buttonToggleBG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonToggleBG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToggleBG.Name = "buttonToggleBG";
            this.buttonToggleBG.Size = new System.Drawing.Size(23, 22);
            this.buttonToggleBG.Text = "BG";
            this.buttonToggleBG.ToolTipText = "BG Color";
            this.buttonToggleBG.Click += new System.EventHandler(this.buttonToggleBG_Click);
            // 
            // buttonToggleP1
            // 
            this.buttonToggleP1.CheckOnClick = true;
            this.buttonToggleP1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonToggleP1.Image = global::LAZYSHELL.Properties.Resources.priority1ON;
            this.buttonToggleP1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonToggleP1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonToggleP1.Name = "buttonToggleP1";
            this.buttonToggleP1.Size = new System.Drawing.Size(23, 22);
            this.buttonToggleP1.Text = "Priority 1";
            this.buttonToggleP1.Click += new System.EventHandler(this.buttonToggleP1_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonEditDelete
            // 
            this.buttonEditDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditDelete.Enabled = false;
            this.buttonEditDelete.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.buttonEditDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditDelete.Name = "buttonEditDelete";
            this.buttonEditDelete.Size = new System.Drawing.Size(23, 22);
            this.buttonEditDelete.Text = "Delete";
            this.buttonEditDelete.Click += new System.EventHandler(this.buttonEditDelete_Click);
            // 
            // buttonEditCut
            // 
            this.buttonEditCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditCut.Enabled = false;
            this.buttonEditCut.Image = global::LAZYSHELL.Properties.Resources.cut_small;
            this.buttonEditCut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditCut.Name = "buttonEditCut";
            this.buttonEditCut.Size = new System.Drawing.Size(23, 22);
            this.buttonEditCut.Text = "Cut";
            this.buttonEditCut.Click += new System.EventHandler(this.buttonEditCut_Click);
            // 
            // buttonEditCopy
            // 
            this.buttonEditCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditCopy.Enabled = false;
            this.buttonEditCopy.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.buttonEditCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditCopy.Name = "buttonEditCopy";
            this.buttonEditCopy.Size = new System.Drawing.Size(23, 22);
            this.buttonEditCopy.Text = "Copy";
            this.buttonEditCopy.Click += new System.EventHandler(this.buttonEditCopy_Click);
            // 
            // buttonEditPaste
            // 
            this.buttonEditPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditPaste.Enabled = false;
            this.buttonEditPaste.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.buttonEditPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditPaste.Name = "buttonEditPaste";
            this.buttonEditPaste.Size = new System.Drawing.Size(23, 22);
            this.buttonEditPaste.Text = "Paste";
            this.buttonEditPaste.Click += new System.EventHandler(this.buttonEditPaste_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonEditMirror
            // 
            this.buttonEditMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditMirror.Enabled = false;
            this.buttonEditMirror.Image = global::LAZYSHELL.Properties.Resources.mirror_small;
            this.buttonEditMirror.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditMirror.Name = "buttonEditMirror";
            this.buttonEditMirror.Size = new System.Drawing.Size(23, 22);
            this.buttonEditMirror.Text = "Mirror";
            this.buttonEditMirror.Click += new System.EventHandler(this.mirrorToolStripMenuItem_Click);
            // 
            // buttonEditInvert
            // 
            this.buttonEditInvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditInvert.Enabled = false;
            this.buttonEditInvert.Image = global::LAZYSHELL.Properties.Resources.flip_small;
            this.buttonEditInvert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditInvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditInvert.Name = "buttonEditInvert";
            this.buttonEditInvert.Size = new System.Drawing.Size(23, 22);
            this.buttonEditInvert.Text = "Invert";
            this.buttonEditInvert.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // buttonEditUndo
            // 
            this.buttonEditUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditUndo.Image = global::LAZYSHELL.Properties.Resources.undo_small;
            this.buttonEditUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditUndo.Name = "buttonEditUndo";
            this.buttonEditUndo.Size = new System.Drawing.Size(23, 22);
            this.buttonEditUndo.Text = "Undo";
            this.buttonEditUndo.Visible = false;
            this.buttonEditUndo.Click += new System.EventHandler(this.buttonEditUndo_Click);
            // 
            // buttonEditRedo
            // 
            this.buttonEditRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEditRedo.Image = global::LAZYSHELL.Properties.Resources.redo_small;
            this.buttonEditRedo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEditRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEditRedo.Name = "buttonEditRedo";
            this.buttonEditRedo.Size = new System.Drawing.Size(23, 22);
            this.buttonEditRedo.Text = "Redo";
            this.buttonEditRedo.Visible = false;
            this.buttonEditRedo.Click += new System.EventHandler(this.buttonEditRedo_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator11.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(272, 547);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.pictureBoxTilesetL1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(264, 520);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LAYER 1";
            // 
            // pictureBoxTilesetL1
            // 
            this.pictureBoxTilesetL1.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTilesetL1.ContextMenuStrip = this.contextMenuStrip2;
            this.pictureBoxTilesetL1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxTilesetL1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTilesetL1.Name = "pictureBoxTilesetL1";
            this.pictureBoxTilesetL1.Size = new System.Drawing.Size(256, 512);
            this.pictureBoxTilesetL1.TabIndex = 1;
            this.pictureBoxTilesetL1.TabStop = false;
            this.pictureBoxTilesetL1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTileset_Paint);
            this.pictureBoxTilesetL1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseClick);
            this.pictureBoxTilesetL1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDoubleClick);
            this.pictureBoxTilesetL1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDown);
            this.pictureBoxTilesetL1.MouseEnter += new System.EventHandler(this.pictureBoxTileset_MouseEnter);
            this.pictureBoxTilesetL1.MouseLeave += new System.EventHandler(this.pictureBoxTileset_MouseLeave);
            this.pictureBoxTilesetL1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseMove);
            this.pictureBoxTilesetL1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseUp);
            this.pictureBoxTilesetL1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxTileset_PreviewKeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.pictureBoxTilesetL2);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(260, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LAYER 2";
            // 
            // pictureBoxTilesetL2
            // 
            this.pictureBoxTilesetL2.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTilesetL2.ContextMenuStrip = this.contextMenuStrip2;
            this.pictureBoxTilesetL2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxTilesetL2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTilesetL2.Name = "pictureBoxTilesetL2";
            this.pictureBoxTilesetL2.Size = new System.Drawing.Size(256, 512);
            this.pictureBoxTilesetL2.TabIndex = 2;
            this.pictureBoxTilesetL2.TabStop = false;
            this.pictureBoxTilesetL2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTileset_Paint);
            this.pictureBoxTilesetL2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseClick);
            this.pictureBoxTilesetL2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDoubleClick);
            this.pictureBoxTilesetL2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDown);
            this.pictureBoxTilesetL2.MouseEnter += new System.EventHandler(this.pictureBoxTileset_MouseEnter);
            this.pictureBoxTilesetL2.MouseLeave += new System.EventHandler(this.pictureBoxTileset_MouseLeave);
            this.pictureBoxTilesetL2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseMove);
            this.pictureBoxTilesetL2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseUp);
            this.pictureBoxTilesetL2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxTileset_PreviewKeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.pictureBoxTilesetL3);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(260, 516);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "LAYER 3";
            // 
            // pictureBoxTilesetL3
            // 
            this.pictureBoxTilesetL3.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTilesetL3.ContextMenuStrip = this.contextMenuStrip2;
            this.pictureBoxTilesetL3.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxTilesetL3.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTilesetL3.Name = "pictureBoxTilesetL3";
            this.pictureBoxTilesetL3.Size = new System.Drawing.Size(256, 512);
            this.pictureBoxTilesetL3.TabIndex = 2;
            this.pictureBoxTilesetL3.TabStop = false;
            this.pictureBoxTilesetL3.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTileset_Paint);
            this.pictureBoxTilesetL3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseClick);
            this.pictureBoxTilesetL3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDoubleClick);
            this.pictureBoxTilesetL3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseDown);
            this.pictureBoxTilesetL3.MouseEnter += new System.EventHandler(this.pictureBoxTileset_MouseEnter);
            this.pictureBoxTilesetL3.MouseLeave += new System.EventHandler(this.pictureBoxTileset_MouseLeave);
            this.pictureBoxTilesetL3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseMove);
            this.pictureBoxTilesetL3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTileset_MouseUp);
            this.pictureBoxTilesetL3.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxTileset_PreviewKeyDown);
            // 
            // lockEditing
            // 
            this.lockEditing.AutoSize = true;
            this.lockEditing.Checked = true;
            this.lockEditing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lockEditing.Dock = System.Windows.Forms.DockStyle.Left;
            this.lockEditing.Location = new System.Drawing.Point(0, 0);
            this.lockEditing.Name = "lockEditing";
            this.lockEditing.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lockEditing.Size = new System.Drawing.Size(117, 26);
            this.lockEditing.TabIndex = 0;
            this.lockEditing.Text = "Lock tileset editing";
            this.lockEditing.UseVisualStyleBackColor = true;
            this.lockEditing.CheckedChanged += new System.EventHandler(this.lockEditing_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Controls.Add(this.autoUpdate);
            this.panel1.Controls.Add(this.lockEditing);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 593);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 26);
            this.panel1.TabIndex = 3;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpdate.Location = new System.Drawing.Point(203, 0);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(69, 26);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoUpdate.Location = new System.Drawing.Point(117, 0);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(86, 26);
            this.autoUpdate.TabIndex = 1;
            this.autoUpdate.Text = "Auto update";
            this.autoUpdate.UseVisualStyleBackColor = true;
            // 
            // labelTileIndex
            // 
            this.labelTileIndex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTileIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTileIndex.Location = new System.Drawing.Point(0, 25);
            this.labelTileIndex.Name = "labelTileIndex";
            this.labelTileIndex.Size = new System.Drawing.Size(272, 21);
            this.labelTileIndex.TabIndex = 1;
            this.labelTileIndex.Text = "Tile index: 0 ($00)";
            this.labelTileIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 619);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelTileIndex);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TilesetEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTilesetL3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.PictureBox pictureBoxTilesetL1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonToggleCartGrid;
        private System.Windows.Forms.ToolStripButton buttonToggleBG;
        private System.Windows.Forms.ToolStripButton buttonToggleP1;
        private System.Windows.Forms.ToolStripButton buttonEditDelete;
        private System.Windows.Forms.ToolStripButton buttonEditCut;
        private System.Windows.Forms.ToolStripButton buttonEditCopy;
        private System.Windows.Forms.ToolStripButton buttonEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton buttonEditUndo;
        private System.Windows.Forms.ToolStripButton buttonEditRedo;
        private System.Windows.Forms.ToolStripButton buttonToggleTileEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxTilesetL2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBoxTilesetL3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem priority1SetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priority1ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.CheckBox lockEditing;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.Label labelTileIndex;
        private System.Windows.Forms.ToolStripMenuItem importImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonEditMirror;
        private System.Windows.Forms.ToolStripButton buttonEditInvert;
    }
}