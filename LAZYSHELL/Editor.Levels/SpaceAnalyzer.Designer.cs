
namespace LAZYSHELL
{
    partial class SpaceAnalyzer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tileMapPage = new System.Windows.Forms.TabPage();
            this.tilemapListView = new System.Windows.Forms.ListView();
            this.tilemap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pointer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.offset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bytesleft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.soliditymapListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tileMapPage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tileMapPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(379, 673);
            this.tabControl1.TabIndex = 0;
            // 
            // tileMapPage
            // 
            this.tileMapPage.Controls.Add(this.tilemapListView);
            this.tileMapPage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileMapPage.Location = new System.Drawing.Point(4, 20);
            this.tileMapPage.Name = "tileMapPage";
            this.tileMapPage.Size = new System.Drawing.Size(371, 649);
            this.tileMapPage.TabIndex = 0;
            this.tileMapPage.Text = "TILE MAPS";
            this.tileMapPage.UseVisualStyleBackColor = true;
            // 
            // tilemapListView
            // 
            this.tilemapListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tilemap,
            this.bank,
            this.pointer,
            this.offset,
            this.size,
            this.bytesleft});
            this.tilemapListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilemapListView.GridLines = true;
            this.tilemapListView.Location = new System.Drawing.Point(0, 0);
            this.tilemapListView.Name = "tilemapListView";
            this.tilemapListView.Size = new System.Drawing.Size(371, 649);
            this.tilemapListView.TabIndex = 0;
            this.tilemapListView.UseCompatibleStateImageBehavior = false;
            this.tilemapListView.View = System.Windows.Forms.View.Details;
            this.tilemapListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // tilemap
            // 
            this.tilemap.Text = "Tilemap";
            this.tilemap.Width = 56;
            // 
            // bank
            // 
            this.bank.Text = "Bank";
            this.bank.Width = 48;
            // 
            // pointer
            // 
            this.pointer.Text = "Pointer";
            this.pointer.Width = 51;
            // 
            // offset
            // 
            this.offset.Text = "Offset";
            this.offset.Width = 74;
            // 
            // size
            // 
            this.size.Text = "Size";
            this.size.Width = 55;
            // 
            // bytesleft
            // 
            this.bytesleft.Text = "Bytes left";
            this.bytesleft.Width = 67;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.soliditymapListView);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 20);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(371, 649);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SOLIDITY MAPS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // soliditymapListView
            // 
            this.soliditymapListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.soliditymapListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soliditymapListView.GridLines = true;
            this.soliditymapListView.Location = new System.Drawing.Point(0, 0);
            this.soliditymapListView.Name = "soliditymapListView";
            this.soliditymapListView.Size = new System.Drawing.Size(371, 649);
            this.soliditymapListView.TabIndex = 0;
            this.soliditymapListView.UseCompatibleStateImageBehavior = false;
            this.soliditymapListView.View = System.Windows.Forms.View.Details;
            this.soliditymapListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tilemap";
            this.columnHeader1.Width = 56;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bank";
            this.columnHeader2.Width = 48;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Pointer";
            this.columnHeader3.Width = 51;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Offset";
            this.columnHeader4.Width = 74;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.Width = 55;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Bytes left";
            this.columnHeader6.Width = 67;
            // 
            // SpaceAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 673);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpaceAnalyzer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SPACE ANALYZER";
            this.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.tileMapPage.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tileMapPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView tilemapListView;
        private System.Windows.Forms.ColumnHeader tilemap;
        private System.Windows.Forms.ColumnHeader bank;
        private System.Windows.Forms.ColumnHeader pointer;
        private System.Windows.Forms.ColumnHeader offset;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader bytesleft;
        private System.Windows.Forms.ListView soliditymapListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}