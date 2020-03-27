
namespace LAZYSHELL
{
    partial class TileEditor
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonInvertTile = new System.Windows.Forms.Button();
            this.buttonMirrorTile = new System.Windows.Forms.Button();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.subtileStatus = new System.Windows.Forms.CheckedListBox();
            this.subtileIndex = new System.Windows.Forms.NumericUpDown();
            this.subtilePalette = new System.Windows.Forms.NumericUpDown();
            this.panel111 = new System.Windows.Forms.Panel();
            this.pictureBoxTile = new System.Windows.Forms.PictureBox();
            this.pictureBoxSubtile = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.subtileIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtilePalette)).BeginInit();
            this.panel111.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtile)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Location = new System.Drawing.Point(12, 237);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.Location = new System.Drawing.Point(93, 237);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.FlatAppearance.BorderSize = 0;
            this.buttonReset.Location = new System.Drawing.Point(174, 237);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "Reset";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonInvertTile
            // 
            this.buttonInvertTile.FlatAppearance.BorderSize = 2;
            this.buttonInvertTile.Location = new System.Drawing.Point(174, 60);
            this.buttonInvertTile.Name = "buttonInvertTile";
            this.buttonInvertTile.Size = new System.Drawing.Size(75, 23);
            this.buttonInvertTile.TabIndex = 4;
            this.buttonInvertTile.Text = "Invert";
            this.buttonInvertTile.Click += new System.EventHandler(this.buttonInvertTile_Click);
            // 
            // buttonMirrorTile
            // 
            this.buttonMirrorTile.FlatAppearance.BorderSize = 2;
            this.buttonMirrorTile.Location = new System.Drawing.Point(174, 32);
            this.buttonMirrorTile.Name = "buttonMirrorTile";
            this.buttonMirrorTile.Size = new System.Drawing.Size(75, 23);
            this.buttonMirrorTile.TabIndex = 3;
            this.buttonMirrorTile.Text = "Mirror";
            this.buttonMirrorTile.Click += new System.EventHandler(this.buttonMirrorTile_Click);
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(6, 22);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(35, 13);
            this.label141.TabIndex = 0;
            this.label141.Text = "Index";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(6, 43);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(41, 13);
            this.label142.TabIndex = 2;
            this.label142.Text = "Palette";
            // 
            // subtileStatus
            // 
            this.subtileStatus.BackColor = System.Drawing.SystemColors.Window;
            this.subtileStatus.CheckOnClick = true;
            this.subtileStatus.ColumnWidth = 60;
            this.subtileStatus.Items.AddRange(new object[] {
            "Priority 1",
            "Mirror",
            "Invert"});
            this.subtileStatus.Location = new System.Drawing.Point(6, 68);
            this.subtileStatus.Name = "subtileStatus";
            this.subtileStatus.Size = new System.Drawing.Size(130, 52);
            this.subtileStatus.TabIndex = 4;
            this.subtileStatus.SelectedIndexChanged += new System.EventHandler(this.tileAttributes_SelectedIndexChanged);
            // 
            // subtileIndex
            // 
            this.subtileIndex.Location = new System.Drawing.Point(74, 20);
            this.subtileIndex.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.subtileIndex.Name = "subtileIndex";
            this.subtileIndex.Size = new System.Drawing.Size(62, 21);
            this.subtileIndex.TabIndex = 1;
            this.subtileIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtileIndex.ValueChanged += new System.EventHandler(this.tile8x8Tile_ValueChanged);
            // 
            // subtilePalette
            // 
            this.subtilePalette.Location = new System.Drawing.Point(74, 41);
            this.subtilePalette.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.subtilePalette.Name = "subtilePalette";
            this.subtilePalette.Size = new System.Drawing.Size(62, 21);
            this.subtilePalette.TabIndex = 3;
            this.subtilePalette.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtilePalette.ValueChanged += new System.EventHandler(this.tilePalette_ValueChanged);
            // 
            // panel111
            // 
            this.panel111.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel111.Controls.Add(this.pictureBoxTile);
            this.panel111.Location = new System.Drawing.Point(12, 32);
            this.panel111.Name = "panel111";
            this.panel111.Size = new System.Drawing.Size(68, 68);
            this.panel111.TabIndex = 0;
            // 
            // pictureBoxTile
            // 
            this.pictureBoxTile.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxTile.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTile.Name = "pictureBoxTile";
            this.pictureBoxTile.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxTile.TabIndex = 446;
            this.pictureBoxTile.TabStop = false;
            this.pictureBoxTile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTile_Paint);
            this.pictureBoxTile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTile_MouseClick);
            // 
            // pictureBoxSubtile
            // 
            this.pictureBoxSubtile.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxSubtile.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSubtile.Name = "pictureBoxSubtile";
            this.pictureBoxSubtile.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxSubtile.TabIndex = 446;
            this.pictureBoxSubtile.TabStop = false;
            this.pictureBoxSubtile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSubtile_Paint);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBoxSubtile);
            this.panel1.Location = new System.Drawing.Point(86, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 68);
            this.panel1.TabIndex = 1;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(174, 208);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.autoUpdate.Checked = true;
            this.autoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoUpdate.Location = new System.Drawing.Point(170, 185);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(87, 17);
            this.autoUpdate.TabIndex = 5;
            this.autoUpdate.Text = "Auto-update";
            this.autoUpdate.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label141);
            this.groupBox1.Controls.Add(this.label142);
            this.groupBox1.Controls.Add(this.subtileStatus);
            this.groupBox1.Controls.Add(this.subtileIndex);
            this.groupBox1.Controls.Add(this.subtilePalette);
            this.groupBox1.Location = new System.Drawing.Point(12, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Subtile Properties";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baseConvertor,
            this.helpTips});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(261, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // baseConvertor
            // 
            this.baseConvertor.CheckOnClick = true;
            this.baseConvertor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.baseConvertor.Image = global::LAZYSHELL.Properties.Resources.baseConversion;
            this.baseConvertor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.baseConvertor.Name = "baseConvertor";
            this.baseConvertor.Size = new System.Drawing.Size(23, 22);
            this.baseConvertor.Text = "Base Convertor";
            // 
            // helpTips
            // 
            this.helpTips.CheckOnClick = true;
            this.helpTips.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpTips.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.helpTips.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpTips.Name = "helpTips";
            this.helpTips.Size = new System.Drawing.Size(23, 22);
            this.helpTips.Text = "Help Tips";
            // 
            // TileEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(261, 272);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.autoUpdate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonInvertTile);
            this.Controls.Add(this.buttonMirrorTile);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.panel111);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TileEditor";
            this.ShowIcon = false;
            this.Text = "TILE EDITOR";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TileEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.subtileIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtilePalette)).EndInit();
            this.panel111.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtile)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel111;
        private System.Windows.Forms.PictureBox pictureBoxSubtile;
        private System.Windows.Forms.PictureBox pictureBoxTile;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.NumericUpDown subtilePalette;
        private System.Windows.Forms.NumericUpDown subtileIndex;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.CheckedListBox subtileStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonInvertTile;
        private System.Windows.Forms.Button buttonMirrorTile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton baseConvertor;
        private System.Windows.Forms.ToolStripButton helpTips;
    }
}