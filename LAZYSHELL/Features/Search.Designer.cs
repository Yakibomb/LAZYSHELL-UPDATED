
namespace LAZYSHELL
{
    partial class Search
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.matchCase = new LAZYSHELL.ToolStripCheckBox();
            this.matchWholeWord = new LAZYSHELL.ToolStripCheckBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.replaceWithText = new System.Windows.Forms.ToolStripTextBox();
            this.replaceAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.Enabled = false;
            this.listBox.FormattingEnabled = true;
            this.listBox.IntegralHeight = false;
            this.listBox.Location = new System.Drawing.Point(0, 25);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(507, 447);
            this.listBox.TabIndex = 0;
            this.listBox.Visible = false;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Enabled = false;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(507, 447);
            this.treeView.TabIndex = 1;
            this.treeView.Visible = false;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Enabled = false;
            this.richTextBox.Location = new System.Drawing.Point(0, 25);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(507, 447);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            this.richTextBox.Visible = false;
            this.richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matchCase,
            this.matchWholeWord,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.replaceWithText,
            this.replaceAllButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(507, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // matchCase
            // 
            this.matchCase.Checked = false;
            this.matchCase.Name = "matchCase";
            this.matchCase.Padding = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.matchCase.Size = new System.Drawing.Size(83, 22);
            this.matchCase.Text = "Match case";
            this.matchCase.CheckedChanged += new System.EventHandler(this.matchCase_CheckedChanged);
            // 
            // matchWholeWord
            // 
            this.matchWholeWord.Checked = false;
            this.matchWholeWord.Name = "matchWholeWord";
            this.matchWholeWord.Padding = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.matchWholeWord.Size = new System.Drawing.Size(118, 22);
            this.matchWholeWord.Text = "Match whole word";
            this.matchWholeWord.CheckedChanged += new System.EventHandler(this.matchWholeWord_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "Replace with";
            this.toolStripLabel1.Visible = false;
            // 
            // replaceWithText
            // 
            this.replaceWithText.Name = "replaceWithText";
            this.replaceWithText.Size = new System.Drawing.Size(150, 25);
            this.replaceWithText.Visible = false;
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.replaceAllButton.Image = global::LAZYSHELL.Properties.Resources.apply;
            this.replaceAllButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.replaceAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(23, 22);
            this.replaceAllButton.Text = "Replace all";
            this.replaceAllButton.Visible = false;
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 472);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SEARCH";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Search_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox replaceWithText;
        private LAZYSHELL.ToolStripCheckBox matchCase;
        private LAZYSHELL.ToolStripCheckBox matchWholeWord;
        private System.Windows.Forms.ToolStripButton replaceAllButton;
    }
}