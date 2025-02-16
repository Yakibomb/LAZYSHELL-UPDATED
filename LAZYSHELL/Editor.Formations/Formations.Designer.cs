
namespace LAZYSHELL
{
    partial class Formations
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
            this.panel10 = new System.Windows.Forms.Panel();
            this.pictureBoxFormation = new System.Windows.Forms.PictureBox();
            this.formationCantRun = new System.Windows.Forms.CheckBox();
            this.label176 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.formationUnknown = new System.Windows.Forms.NumericUpDown();
            this.musicTrack = new System.Windows.Forms.ComboBox();
            this.formationMusic = new System.Windows.Forms.ComboBox();
            this.label150 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.formationNameList = new System.Windows.Forms.ToolStripComboBox();
            this.formationNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchFormationNames = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.formationBattleEvent = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.isometricGrid = new System.Windows.Forms.ToolStripButton();
            this.snapIsometricLeft = new System.Windows.Forms.ToolStripButton();
            this.snapIsometricRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelCoords = new System.Windows.Forms.ToolStripLabel();
            this.select = new System.Windows.Forms.ToolStripButton();
            this.undo = new System.Windows.Forms.ToolStripButton();
            this.redo = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.battlefieldName = new System.Windows.Forms.ToolStripComboBox();
            this.toggleAllies = new System.Windows.Forms.ToolStripButton();
            this.moveUp = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formationUnknown)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Window;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.pictureBoxFormation);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 25);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(260, 224);
            this.panel10.TabIndex = 1;
            // 
            // pictureBoxFormation
            // 
            this.pictureBoxFormation.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxFormation.Location = new System.Drawing.Point(0, -32);
            this.pictureBoxFormation.Name = "pictureBoxFormation";
            this.pictureBoxFormation.Size = new System.Drawing.Size(512, 512);
            this.pictureBoxFormation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxFormation.TabIndex = 286;
            this.pictureBoxFormation.TabStop = false;
            this.pictureBoxFormation.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFormation_Paint);
            this.pictureBoxFormation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFormation_MouseDown);
            this.pictureBoxFormation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFormation_MouseMove);
            this.pictureBoxFormation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFormation_MouseUp);
            this.pictureBoxFormation.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxFormation_PreviewKeyDown);
            // 
            // formationCantRun
            // 
            this.formationCantRun.Appearance = System.Windows.Forms.Appearance.Button;
            this.formationCantRun.BackColor = System.Drawing.SystemColors.Control;
            this.formationCantRun.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formationCantRun.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.formationCantRun.Location = new System.Drawing.Point(99, 41);
            this.formationCantRun.Name = "formationCantRun";
            this.formationCantRun.Size = new System.Drawing.Size(79, 19);
            this.formationCantRun.TabIndex = 4;
            this.formationCantRun.Text = "CAN\'T RUN";
            this.formationCantRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.formationCantRun.UseCompatibleTextRendering = true;
            this.formationCantRun.UseVisualStyleBackColor = false;
            this.formationCantRun.CheckedChanged += new System.EventHandler(this.formationCantRun_CheckedChanged);
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Location = new System.Drawing.Point(6, 20);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(35, 13);
            this.label176.TabIndex = 0;
            this.label176.Text = "Event";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "??????";
            // 
            // formationUnknown
            // 
            this.formationUnknown.Location = new System.Drawing.Point(47, 39);
            this.formationUnknown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.formationUnknown.Name = "formationUnknown";
            this.formationUnknown.Size = new System.Drawing.Size(46, 21);
            this.formationUnknown.TabIndex = 3;
            this.formationUnknown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.formationUnknown.ValueChanged += new System.EventHandler(this.formationUnknown_ValueChanged);
            // 
            // musicTrack
            // 
            this.musicTrack.BackColor = System.Drawing.SystemColors.Window;
            this.musicTrack.DropDownHeight = 262;
            this.musicTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.musicTrack.DropDownWidth = 250;
            this.musicTrack.IntegralHeight = false;
            this.musicTrack.Location = new System.Drawing.Point(46, 38);
            this.musicTrack.Name = "musicTrack";
            this.musicTrack.Size = new System.Drawing.Size(118, 21);
            this.musicTrack.TabIndex = 3;
            this.musicTrack.SelectedIndexChanged += new System.EventHandler(this.musicTrack_SelectedIndexChanged);
            // 
            // formationMusic
            // 
            this.formationMusic.BackColor = System.Drawing.SystemColors.Window;
            this.formationMusic.DropDownHeight = 150;
            this.formationMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formationMusic.DropDownWidth = 100;
            this.formationMusic.IntegralHeight = false;
            this.formationMusic.Items.AddRange(new object[] {
            "Normal",
            "Boss 1",
            "Boss 2",
            "Smithy 1",
            "Moleville Mountain",
            "Booster Hill",
            "Barrel Volcano",
            "Culex",
            "{CURRENT}"});
            this.formationMusic.Location = new System.Drawing.Point(46, 17);
            this.formationMusic.Name = "formationMusic";
            this.formationMusic.Size = new System.Drawing.Size(118, 21);
            this.formationMusic.TabIndex = 1;
            this.formationMusic.SelectedIndexChanged += new System.EventHandler(this.formationMusic_SelectedIndexChanged);
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(6, 41);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(33, 13);
            this.label150.TabIndex = 2;
            this.label150.Text = "Track";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Type";
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formationNameList,
            this.formationNum,
            this.searchBox,
            this.searchFormationNames});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(638, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // formationNameList
            // 
            this.formationNameList.DropDownHeight = 506;
            this.formationNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formationNameList.DropDownWidth = 500;
            this.formationNameList.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.formationNameList.IntegralHeight = false;
            this.formationNameList.Name = "formationNameList";
            this.formationNameList.Size = new System.Drawing.Size(258, 25);
            this.formationNameList.SelectedIndexChanged += new System.EventHandler(this.formationNameList_SelectedIndexChanged);
            // 
            // formationNum
            // 
            this.formationNum.AutoSize = false;
            this.formationNum.ContextMenuStrip = null;
            this.formationNum.Hexadecimal = false;
            this.formationNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.formationNum.Location = new System.Drawing.Point(269, 2);
            this.formationNum.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.formationNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.formationNum.Name = "formationNum";
            this.formationNum.Size = new System.Drawing.Size(60, 21);
            this.formationNum.Text = "0";
            this.formationNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.formationNum.ValueChanged += new System.EventHandler(this.formationNum_ValueChanged);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(240, 25);
            // 
            // searchFormationNames
            // 
            this.searchFormationNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchFormationNames.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchFormationNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchFormationNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchFormationNames.Name = "searchFormationNames";
            this.searchFormationNames.Size = new System.Drawing.Size(23, 22);
            this.searchFormationNames.Text = "Search for formation";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(271, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 194);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formation Monsters";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.musicTrack);
            this.groupBox2.Controls.Add(this.formationMusic);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label150);
            this.groupBox2.Location = new System.Drawing.Point(461, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 65);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Formation Music";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.formationBattleEvent);
            this.groupBox3.Controls.Add(this.formationCantRun);
            this.groupBox3.Controls.Add(this.formationUnknown);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label176);
            this.groupBox3.Location = new System.Drawing.Point(271, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 65);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Formation Properties";
            // 
            // formationBattleEvent
            // 
            this.formationBattleEvent.BackColor = System.Drawing.SystemColors.Window;
            this.formationBattleEvent.DropDownHeight = 300;
            this.formationBattleEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formationBattleEvent.DropDownWidth = 400;
            this.formationBattleEvent.IntegralHeight = false;
            this.formationBattleEvent.Location = new System.Drawing.Point(47, 17);
            this.formationBattleEvent.Name = "formationBattleEvent";
            this.formationBattleEvent.Size = new System.Drawing.Size(131, 21);
            this.formationBattleEvent.TabIndex = 1;
            this.formationBattleEvent.SelectedIndexChanged += new System.EventHandler(this.formationBattleEvent_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 274);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.CanOverflow = false;
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isometricGrid,
            this.snapIsometricLeft,
            this.snapIsometricRight,
            this.toolStripSeparator1,
            this.labelCoords,
            this.select,
            this.undo,
            this.redo});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(260, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // isometricGrid
            // 
            this.isometricGrid.CheckOnClick = true;
            this.isometricGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.isometricGrid.Image = global::LAZYSHELL.Properties.Resources.buttonToggleOrthGrid;
            this.isometricGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.isometricGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isometricGrid.Name = "isometricGrid";
            this.isometricGrid.Size = new System.Drawing.Size(23, 22);
            this.isometricGrid.Text = "Isometric Grid (G)";
            this.isometricGrid.Click += new System.EventHandler(this.isometricGrid_Click);
            // 
            // snapIsometricLeft
            // 
            this.snapIsometricLeft.CheckOnClick = true;
            this.snapIsometricLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapIsometricLeft.Image = global::LAZYSHELL.Properties.Resources.snapIsometricLeft;
            this.snapIsometricLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.snapIsometricLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapIsometricLeft.Name = "snapIsometricLeft";
            this.snapIsometricLeft.Size = new System.Drawing.Size(23, 22);
            this.snapIsometricLeft.Text = "Snap to left";
            // 
            // snapIsometricRight
            // 
            this.snapIsometricRight.CheckOnClick = true;
            this.snapIsometricRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapIsometricRight.Image = global::LAZYSHELL.Properties.Resources.snapIsometricRight;
            this.snapIsometricRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.snapIsometricRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapIsometricRight.Name = "snapIsometricRight";
            this.snapIsometricRight.Size = new System.Drawing.Size(23, 22);
            this.snapIsometricRight.Text = "Snap to right";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // labelCoords
            // 
            this.labelCoords.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.labelCoords.Name = "labelCoords";
            this.labelCoords.Size = new System.Drawing.Size(59, 22);
            this.labelCoords.Text = "(x: 0, y: 0)";
            // 
            // select
            // 
            this.select.CheckOnClick = true;
            this.select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.select.Image = global::LAZYSHELL.Properties.Resources.select_small;
            this.select.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(23, 22);
            this.select.Text = "Select (S)";
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // undo
            // 
            this.undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undo.Image = global::LAZYSHELL.Properties.Resources.undo_small;
            this.undo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(23, 22);
            this.undo.Text = "Undo (Ctrl+Z)";
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
            this.redo.Text = "Redo (Ctrl+Y)";
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.battlefieldName,
            this.toggleAllies});
            this.toolStrip2.Location = new System.Drawing.Point(0, 249);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(260, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel1.Text = " BG ";
            // 
            // battlefieldName
            // 
            this.battlefieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.battlefieldName.DropDownWidth = 250;
            this.battlefieldName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.battlefieldName.Name = "battlefieldName";
            this.battlefieldName.Size = new System.Drawing.Size(196, 25);
            this.battlefieldName.SelectedIndexChanged += new System.EventHandler(this.battlefieldName_SelectedIndexChanged);
            // 
            // toggleAllies
            // 
            this.toggleAllies.CheckOnClick = true;
            this.toggleAllies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleAllies.Image = global::LAZYSHELL.Properties.Resources.marioicon;
            this.toggleAllies.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleAllies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleAllies.Name = "toggleAllies";
            this.toggleAllies.Size = new System.Drawing.Size(23, 22);
            this.toggleAllies.Text = "Show/hide Allies (P)";
            this.toggleAllies.Click += new System.EventHandler(this.toggleAllies_Click);
            // 
            // moveUp
            // 
            this.moveUp.FlatAppearance.BorderSize = 0;
            this.moveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveUp.Image = global::LAZYSHELL.Properties.Resources.moveUp;
            this.moveUp.Location = new System.Drawing.Point(381, 28);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(17, 17);
            this.moveUp.TabIndex = 0;
            this.toolTip1.SetToolTip(this.moveUp, "Move Monster Up");
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.FlatAppearance.BorderSize = 0;
            this.moveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveDown.Image = global::LAZYSHELL.Properties.Resources.moveDown;
            this.moveDown.Location = new System.Drawing.Point(398, 28);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(17, 17);
            this.moveDown.TabIndex = 0;
            this.toolTip1.SetToolTip(this.moveDown, "Move Monster Down");
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // Formations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 299);
            this.ControlBox = false;
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Formations";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFormation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formationUnknown)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.PictureBox pictureBoxFormation;
        private System.Windows.Forms.CheckBox formationCantRun;
        private System.Windows.Forms.ComboBox musicTrack;
        private System.Windows.Forms.ComboBox formationMusic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown formationUnknown;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox formationNameList;
        private System.Windows.Forms.ToolStripButton searchFormationNames;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private ToolStripNumericUpDown formationNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox battlefieldName;
        private System.Windows.Forms.ComboBox formationBattleEvent;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton isometricGrid;
        private System.Windows.Forms.ToolStripButton snapIsometricLeft;
        private System.Windows.Forms.ToolStripButton snapIsometricRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton select;
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton undo;
        private System.Windows.Forms.ToolStripButton redo;
        private System.Windows.Forms.ToolStripLabel labelCoords;
        private System.Windows.Forms.ToolStripButton toggleAllies;
    }
}