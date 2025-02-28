
namespace LAZYSHELL.Patches
{
    partial class GamePatches
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
            this.MainDetailsPanel = new System.Windows.Forms.Panel();
            this.StarFavorite = new System.Windows.Forms.Label();
            this.DetailsBox = new System.Windows.Forms.RichTextBox();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.versionDisplayComboBox = new System.Windows.Forms.ComboBox();
            this.downloadReadmeButton = new System.Windows.Forms.Button();
            this.downloadIPStoFileButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.WebsitePanel = new System.Windows.Forms.Panel();
            this.WebsiteLabel = new System.Windows.Forms.Label();
            this.WebsiteBox = new System.Windows.Forms.RichTextBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.DescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.PatchListBox = new System.Windows.Forms.ListBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.DateCreatedLabel = new System.Windows.Forms.Label();
            this.PatchNameLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.downloadingLabel = new System.Windows.Forms.Label();
            this.GameImageDescriptions = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.clock = new System.ComponentModel.BackgroundWorker();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.sortByButtons = new System.Windows.Forms.ToolStripDropDownButton();
            this.unsortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByCategoryComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.sortByAuthorComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageMaxNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.maxImagesLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.verifyIPSButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.openSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.patchHTTPServer = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadPatchServer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.imageStuffPanel = new System.Windows.Forms.Panel();
            this.imageStuffToolStrip = new System.Windows.Forms.ToolStrip();
            this.imageStuffButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.imageStuffLabel = new System.Windows.Forms.ToolStripLabel();
            this.imageStuffButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.MainDetailsPanel.SuspendLayout();
            this.WebsitePanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.imageStuffPanel.SuspendLayout();
            this.imageStuffToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainDetailsPanel
            // 
            this.MainDetailsPanel.Controls.Add(this.StarFavorite);
            this.MainDetailsPanel.Controls.Add(this.DetailsBox);
            this.MainDetailsPanel.Controls.Add(this.DetailsLabel);
            this.MainDetailsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainDetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.MainDetailsPanel.Name = "MainDetailsPanel";
            this.MainDetailsPanel.Size = new System.Drawing.Size(300, 61);
            this.MainDetailsPanel.TabIndex = 0;
            this.MainDetailsPanel.Visible = false;
            // 
            // StarFavorite
            // 
            this.StarFavorite.Image = global::LAZYSHELL.Properties.Resources.star;
            this.StarFavorite.Location = new System.Drawing.Point(3, 2);
            this.StarFavorite.Name = "StarFavorite";
            this.StarFavorite.Size = new System.Drawing.Size(15, 14);
            this.StarFavorite.TabIndex = 0;
            this.StarFavorite.Visible = false;
            // 
            // DetailsBox
            // 
            this.DetailsBox.BackColor = System.Drawing.SystemColors.Control;
            this.DetailsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DetailsBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsBox.Location = new System.Drawing.Point(60, 0);
            this.DetailsBox.Name = "DetailsBox";
            this.DetailsBox.ReadOnly = true;
            this.DetailsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.DetailsBox.Size = new System.Drawing.Size(240, 59);
            this.DetailsBox.TabIndex = 17;
            this.DetailsBox.Text = "";
            this.DetailsBox.WordWrap = false;
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DetailsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsLabel.Location = new System.Drawing.Point(0, 0);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(57, 59);
            this.DetailsLabel.TabIndex = 18;
            this.DetailsLabel.Text = "Name:\r\nAuthors:\r\nDate:\r\nTags:";
            this.DetailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // versionDisplayComboBox
            // 
            this.versionDisplayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionDisplayComboBox.Enabled = false;
            this.versionDisplayComboBox.Items.AddRange(new object[] {
            " "});
            this.versionDisplayComboBox.Location = new System.Drawing.Point(457, 4);
            this.versionDisplayComboBox.Name = "versionDisplayComboBox";
            this.versionDisplayComboBox.Size = new System.Drawing.Size(95, 21);
            this.versionDisplayComboBox.TabIndex = 20;
            this.versionDisplayComboBox.Visible = false;
            this.versionDisplayComboBox.SelectedIndexChanged += new System.EventHandler(this.versionDisplayComboBox_SelectedIndexChanged);
            // 
            // downloadReadmeButton
            // 
            this.downloadReadmeButton.Enabled = false;
            this.downloadReadmeButton.Location = new System.Drawing.Point(150, 3);
            this.downloadReadmeButton.Name = "downloadReadmeButton";
            this.downloadReadmeButton.Size = new System.Drawing.Size(150, 21);
            this.downloadReadmeButton.TabIndex = 13;
            this.downloadReadmeButton.Text = "OPEN README";
            this.downloadReadmeButton.UseCompatibleTextRendering = true;
            this.downloadReadmeButton.UseVisualStyleBackColor = false;
            this.downloadReadmeButton.Click += new System.EventHandler(this.downloadReadmeButton_Click);
            // 
            // downloadIPStoFileButton
            // 
            this.downloadIPStoFileButton.Enabled = false;
            this.downloadIPStoFileButton.Location = new System.Drawing.Point(0, 3);
            this.downloadIPStoFileButton.Name = "downloadIPStoFileButton";
            this.downloadIPStoFileButton.Size = new System.Drawing.Size(150, 21);
            this.downloadIPStoFileButton.TabIndex = 14;
            this.downloadIPStoFileButton.Text = "DOWNLOAD PATCH";
            this.downloadIPStoFileButton.UseCompatibleTextRendering = true;
            this.downloadIPStoFileButton.UseVisualStyleBackColor = false;
            this.downloadIPStoFileButton.Click += new System.EventHandler(this.downloadIPStoFileButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(0, 25);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(300, 21);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "DOWNLOAD + APPLY PATCH";
            this.applyButton.UseCompatibleTextRendering = true;
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // WebsitePanel
            // 
            this.WebsitePanel.Controls.Add(this.WebsiteLabel);
            this.WebsitePanel.Controls.Add(this.WebsiteBox);
            this.WebsitePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.WebsitePanel.Location = new System.Drawing.Point(0, 202);
            this.WebsitePanel.Name = "WebsitePanel";
            this.WebsitePanel.Size = new System.Drawing.Size(300, 23);
            this.WebsitePanel.TabIndex = 3;
            this.WebsitePanel.Visible = false;
            // 
            // WebsiteLabel
            // 
            this.WebsiteLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WebsiteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WebsiteLabel.Location = new System.Drawing.Point(0, 0);
            this.WebsiteLabel.Name = "WebsiteLabel";
            this.WebsiteLabel.Size = new System.Drawing.Size(57, 23);
            this.WebsiteLabel.TabIndex = 19;
            this.WebsiteLabel.Text = "Website:";
            this.WebsiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WebsiteBox
            // 
            this.WebsiteBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.WebsiteBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WebsiteBox.Location = new System.Drawing.Point(60, 0);
            this.WebsiteBox.MaxLength = 256;
            this.WebsiteBox.Name = "WebsiteBox";
            this.WebsiteBox.ReadOnly = true;
            this.WebsiteBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.WebsiteBox.Size = new System.Drawing.Size(240, 22);
            this.WebsiteBox.TabIndex = 16;
            this.WebsiteBox.Text = "";
            this.WebsiteBox.WordWrap = false;
            this.WebsiteBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.WebsiteBox_LinkClicked);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.DescriptionTextBox);
            this.MainPanel.Controls.Add(this.WebsitePanel);
            this.MainPanel.Controls.Add(this.MainDetailsPanel);
            this.MainPanel.Controls.Add(this.ButtonsPanel);
            this.MainPanel.Location = new System.Drawing.Point(252, 28);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(300, 272);
            this.MainPanel.TabIndex = 4;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionTextBox.Location = new System.Drawing.Point(0, 61);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DescriptionTextBox.Size = new System.Drawing.Size(300, 141);
            this.DescriptionTextBox.TabIndex = 2;
            this.DescriptionTextBox.Text = "";
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.downloadReadmeButton);
            this.ButtonsPanel.Controls.Add(this.downloadIPStoFileButton);
            this.ButtonsPanel.Controls.Add(this.applyButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 225);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(300, 47);
            this.ButtonsPanel.TabIndex = 20;
            this.ButtonsPanel.Visible = false;
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.ImagePictureBox.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.ImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(256, 224);
            this.ImagePictureBox.TabIndex = 2;
            this.ImagePictureBox.TabStop = false;
            // 
            // PatchListBox
            // 
            this.PatchListBox.DisplayMember = "null";
            this.PatchListBox.Enabled = false;
            this.PatchListBox.FormattingEnabled = true;
            this.PatchListBox.IntegralHeight = false;
            this.PatchListBox.Location = new System.Drawing.Point(12, 28);
            this.PatchListBox.Name = "PatchListBox";
            this.PatchListBox.Size = new System.Drawing.Size(234, 272);
            this.PatchListBox.TabIndex = 0;
            this.PatchListBox.ValueMember = "Patch";
            this.PatchListBox.SelectedIndexChanged += new System.EventHandler(this.PatchListBox_SelectedIndexChanged);
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(142, 347);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(0, 13);
            this.AuthorLabel.TabIndex = 4;
            // 
            // DateCreatedLabel
            // 
            this.DateCreatedLabel.AutoSize = true;
            this.DateCreatedLabel.Location = new System.Drawing.Point(142, 360);
            this.DateCreatedLabel.Name = "DateCreatedLabel";
            this.DateCreatedLabel.Size = new System.Drawing.Size(0, 13);
            this.DateCreatedLabel.TabIndex = 5;
            // 
            // PatchNameLabel
            // 
            this.PatchNameLabel.AutoSize = true;
            this.PatchNameLabel.Location = new System.Drawing.Point(142, 334);
            this.PatchNameLabel.Name = "PatchNameLabel";
            this.PatchNameLabel.Size = new System.Drawing.Size(0, 13);
            this.PatchNameLabel.TabIndex = 7;
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(142, 373);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(0, 13);
            this.SizeLabel.TabIndex = 8;
            // 
            // downloadingLabel
            // 
            this.downloadingLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.downloadingLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.downloadingLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.downloadingLabel.Location = new System.Drawing.Point(0, 302);
            this.downloadingLabel.Name = "downloadingLabel";
            this.downloadingLabel.Size = new System.Drawing.Size(832, 20);
            this.downloadingLabel.TabIndex = 3;
            this.downloadingLabel.Text = "              ...INITIALIZING...              ";
            this.downloadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameImageDescriptions
            // 
            this.GameImageDescriptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameImageDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameImageDescriptions.Location = new System.Drawing.Point(558, 278);
            this.GameImageDescriptions.Name = "GameImageDescriptions";
            this.GameImageDescriptions.Size = new System.Drawing.Size(260, 21);
            this.GameImageDescriptions.TabIndex = 6;
            this.GameImageDescriptions.Text = "Image Descriptions";
            this.GameImageDescriptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.ImagePictureBox);
            this.panel3.Location = new System.Drawing.Point(558, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 228);
            this.panel3.TabIndex = 4;
            // 
            // clock
            // 
            this.clock.WorkerReportsProgress = true;
            this.clock.WorkerSupportsCancellation = true;
            this.clock.DoWork += new System.ComponentModel.DoWorkEventHandler(this.clock_DoWork);
            this.clock.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.clock_ProgressChanged);
            this.clock.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.clock_RunWorkerCompleted);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortByButtons,
            this.sortByCategoryComboBox,
            this.sortByAuthorComboBox,
            this.helpTips,
            this.toolStripSeparator1,
            this.imageMaxNum,
            this.maxImagesLabel,
            this.toolStripSeparator4,
            this.verifyIPSButton,
            this.toolStripSeparator5,
            this.openSettings,
            this.toolStripSeparator7,
            this.reloadPatchServer,
            this.toolStripSeparator6});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(822, 25);
            this.toolStrip4.TabIndex = 9;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // sortByButtons
            // 
            this.sortByButtons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sortByButtons.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unsortToolStripMenuItem,
            this.sortByTitleToolStripMenuItem,
            this.sortByDateToolStripMenuItem});
            this.sortByButtons.Enabled = false;
            this.sortByButtons.Image = global::LAZYSHELL.Properties.Resources.notepad;
            this.sortByButtons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sortByButtons.Name = "sortByButtons";
            this.sortByButtons.Size = new System.Drawing.Size(29, 22);
            // 
            // unsortToolStripMenuItem
            // 
            this.unsortToolStripMenuItem.CheckOnClick = true;
            this.unsortToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.download;
            this.unsortToolStripMenuItem.Name = "unsortToolStripMenuItem";
            this.unsortToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.unsortToolStripMenuItem.Text = "Sorted by Download";
            this.unsortToolStripMenuItem.Click += new System.EventHandler(this.unsortToolStripMenuItem_Click);
            // 
            // sortByTitleToolStripMenuItem
            // 
            this.sortByTitleToolStripMenuItem.Checked = true;
            this.sortByTitleToolStripMenuItem.CheckOnClick = true;
            this.sortByTitleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sortByTitleToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.mainMainTitle;
            this.sortByTitleToolStripMenuItem.Name = "sortByTitleToolStripMenuItem";
            this.sortByTitleToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.sortByTitleToolStripMenuItem.Text = "Sorted by Name";
            this.sortByTitleToolStripMenuItem.Click += new System.EventHandler(this.sortByTitleToolStripMenuItem_Click);
            // 
            // sortByDateToolStripMenuItem
            // 
            this.sortByDateToolStripMenuItem.CheckOnClick = true;
            this.sortByDateToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.history;
            this.sortByDateToolStripMenuItem.Name = "sortByDateToolStripMenuItem";
            this.sortByDateToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.sortByDateToolStripMenuItem.Text = "Sorted by Release Date";
            this.sortByDateToolStripMenuItem.Visible = false;
            this.sortByDateToolStripMenuItem.Click += new System.EventHandler(this.sortByDateToolStripMenuItem_Click);
            // 
            // sortByCategoryComboBox
            // 
            this.sortByCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByCategoryComboBox.Enabled = false;
            this.sortByCategoryComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.sortByCategoryComboBox.Items.AddRange(new object[] {
            "All Categories"});
            this.sortByCategoryComboBox.Name = "sortByCategoryComboBox";
            this.sortByCategoryComboBox.Size = new System.Drawing.Size(102, 25);
            this.sortByCategoryComboBox.Sorted = true;
            this.sortByCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.sortByCategoryComboBox_SelectedIndexChanged);
            // 
            // sortByAuthorComboBox
            // 
            this.sortByAuthorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByAuthorComboBox.Enabled = false;
            this.sortByAuthorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.sortByAuthorComboBox.Items.AddRange(new object[] {
            "All Authors"});
            this.sortByAuthorComboBox.Name = "sortByAuthorComboBox";
            this.sortByAuthorComboBox.Size = new System.Drawing.Size(102, 25);
            this.sortByAuthorComboBox.Sorted = true;
            this.sortByAuthorComboBox.SelectedIndexChanged += new System.EventHandler(this.sortByAuthorComboBox_SelectedIndexChanged);
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
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // imageMaxNum
            // 
            this.imageMaxNum.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.imageMaxNum.ContextMenuStrip = null;
            this.imageMaxNum.Hexadecimal = false;
            this.imageMaxNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.imageMaxNum.Location = new System.Drawing.Point(764, 3);
            this.imageMaxNum.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.imageMaxNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.imageMaxNum.Name = "itemNum";
            this.imageMaxNum.Size = new System.Drawing.Size(28, 22);
            this.imageMaxNum.Text = "4";
            this.imageMaxNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.imageMaxNum.ValueChanged += new System.EventHandler(this.imageMaxNum_ValueChanged);
            // 
            // maxImagesLabel
            // 
            this.maxImagesLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.maxImagesLabel.Name = "maxImagesLabel";
            this.maxImagesLabel.Size = new System.Drawing.Size(66, 22);
            this.maxImagesLabel.Text = "Max Images";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // verifyIPSButton
            // 
            this.verifyIPSButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.verifyIPSButton.Checked = true;
            this.verifyIPSButton.CheckOnClick = true;
            this.verifyIPSButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.verifyIPSButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.verifyIPSButton.Image = global::LAZYSHELL.Properties.Resources.shield_check;
            this.verifyIPSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.verifyIPSButton.Name = "verifyIPSButton";
            this.verifyIPSButton.Size = new System.Drawing.Size(23, 22);
            this.verifyIPSButton.Click += new System.EventHandler(this.verifyIPSButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // openSettings
            // 
            this.openSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patchHTTPServer});
            this.openSettings.Image = global::LAZYSHELL.Properties.Resources.settings;
            this.openSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSettings.Name = "openSettings";
            this.openSettings.Size = new System.Drawing.Size(73, 22);
            this.openSettings.Text = "Servers";
            this.openSettings.ToolTipText = " ";
            this.openSettings.DropDownClosed += new System.EventHandler(this.openSettings_DropDownClosed);
            this.openSettings.DropDownOpened += new System.EventHandler(this.openSettings_DropDownOpened);
            // 
            // patchHTTPServer
            // 
            this.patchHTTPServer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.patchHTTPServer.DropDownHeight = 100;
            this.patchHTTPServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.patchHTTPServer.DropDownWidth = 100;
            this.patchHTTPServer.IntegralHeight = false;
            this.patchHTTPServer.MaxLength = 44444;
            this.patchHTTPServer.Name = "patchHTTPServer";
            this.patchHTTPServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.patchHTTPServer.Size = new System.Drawing.Size(320, 150);
            this.patchHTTPServer.TextUpdate += new System.EventHandler(this.patchHTTPServer_TextUpdate);
            this.patchHTTPServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.patchHTTPServer_KeyDown);
            this.patchHTTPServer.TextChanged += new System.EventHandler(this.patchHTTPServer_TextChanged);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // reloadPatchServer
            // 
            this.reloadPatchServer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.reloadPatchServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reloadPatchServer.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.reloadPatchServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadPatchServer.Name = "reloadPatchServer";
            this.reloadPatchServer.Size = new System.Drawing.Size(23, 22);
            this.reloadPatchServer.ToolTipText = "Force stop downloading from patch servers";
            this.reloadPatchServer.Click += new System.EventHandler(this.reloadPatchServer_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // imageStuffPanel
            // 
            this.imageStuffPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageStuffPanel.AutoScroll = true;
            this.imageStuffPanel.BackColor = System.Drawing.SystemColors.Control;
            this.imageStuffPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageStuffPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageStuffPanel.Controls.Add(this.imageStuffToolStrip);
            this.imageStuffPanel.Location = new System.Drawing.Point(558, 258);
            this.imageStuffPanel.Margin = new System.Windows.Forms.Padding(0);
            this.imageStuffPanel.Name = "imageStuffPanel";
            this.imageStuffPanel.Size = new System.Drawing.Size(260, 21);
            this.imageStuffPanel.TabIndex = 12;
            // 
            // imageStuffToolStrip
            // 
            this.imageStuffToolStrip.AutoSize = false;
            this.imageStuffToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.imageStuffToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageStuffToolStrip.CanOverflow = false;
            this.imageStuffToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageStuffToolStrip.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageStuffToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.imageStuffToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.imageStuffToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageStuffButtonLeft,
            this.toolStripSeparator3,
            this.imageStuffLabel,
            this.imageStuffButtonRight,
            this.toolStripSeparator2});
            this.imageStuffToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.imageStuffToolStrip.Location = new System.Drawing.Point(0, 0);
            this.imageStuffToolStrip.Name = "imageStuffToolStrip";
            this.imageStuffToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.imageStuffToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.imageStuffToolStrip.Size = new System.Drawing.Size(258, 19);
            this.imageStuffToolStrip.TabIndex = 10;
            // 
            // imageStuffButtonLeft
            // 
            this.imageStuffButtonLeft.AutoSize = false;
            this.imageStuffButtonLeft.AutoToolTip = false;
            this.imageStuffButtonLeft.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.imageStuffButtonLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageStuffButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imageStuffButtonLeft.Enabled = false;
            this.imageStuffButtonLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageStuffButtonLeft.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.imageStuffButtonLeft.Image = global::LAZYSHELL.Properties.Resources.moveLeft;
            this.imageStuffButtonLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imageStuffButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.imageStuffButtonLeft.Margin = new System.Windows.Forms.Padding(0);
            this.imageStuffButtonLeft.Name = "imageStuffButtonLeft";
            this.imageStuffButtonLeft.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.imageStuffButtonLeft.Size = new System.Drawing.Size(64, 19);
            this.imageStuffButtonLeft.Text = " ";
            this.imageStuffButtonLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.imageStuffButtonLeft.Click += new System.EventHandler(this.imageStuffButtonLeft_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 19);
            // 
            // imageStuffLabel
            // 
            this.imageStuffLabel.AutoSize = false;
            this.imageStuffLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageStuffLabel.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.imageStuffLabel.Name = "imageStuffLabel";
            this.imageStuffLabel.Size = new System.Drawing.Size(117, 17);
            this.imageStuffLabel.Text = "0 / 0";
            // 
            // imageStuffButtonRight
            // 
            this.imageStuffButtonRight.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.imageStuffButtonRight.AutoSize = false;
            this.imageStuffButtonRight.AutoToolTip = false;
            this.imageStuffButtonRight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.imageStuffButtonRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageStuffButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imageStuffButtonRight.Enabled = false;
            this.imageStuffButtonRight.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageStuffButtonRight.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.imageStuffButtonRight.Image = global::LAZYSHELL.Properties.Resources.moveRight;
            this.imageStuffButtonRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imageStuffButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.imageStuffButtonRight.Margin = new System.Windows.Forms.Padding(0);
            this.imageStuffButtonRight.Name = "imageStuffButtonRight";
            this.imageStuffButtonRight.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.imageStuffButtonRight.Size = new System.Drawing.Size(64, 20);
            this.imageStuffButtonRight.Text = " ";
            this.imageStuffButtonRight.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.imageStuffButtonRight.Click += new System.EventHandler(this.imageStuffButtonRight_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 19);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::LAZYSHELL.Properties.Resources.shield_question;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // GamePatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 321);
            this.Controls.Add(this.versionDisplayComboBox);
            this.Controls.Add(this.downloadingLabel);
            this.Controls.Add(this.imageStuffPanel);
            this.Controls.Add(this.GameImageDescriptions);
            this.Controls.Add(this.PatchListBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.PatchNameLabel);
            this.Controls.Add(this.DateCreatedLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.toolStrip4);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.MaximizeBox = false;
            this.Name = "GamePatches";
            this.Text = "PATCHES - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GamePatches_FormClosing);
            this.MainDetailsPanel.ResumeLayout(false);
            this.WebsitePanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.ButtonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.imageStuffPanel.ResumeLayout(false);
            this.imageStuffToolStrip.ResumeLayout(false);
            this.imageStuffToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel WebsitePanel;
        private System.Windows.Forms.Panel MainDetailsPanel;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.ListBox PatchListBox;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label StarFavorite;
        private System.Windows.Forms.Label DateCreatedLabel;
        private System.Windows.Forms.RichTextBox DescriptionTextBox;
        private System.Windows.Forms.Label PatchNameLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label GameImageDescriptions;
        private System.Windows.Forms.Label downloadingLabel;
        private System.Windows.Forms.Panel panel3;
        private System.ComponentModel.BackgroundWorker clock;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripDropDownButton openSettings;
        private System.Windows.Forms.Panel imageStuffPanel;
        private System.Windows.Forms.ToolStrip imageStuffToolStrip;
        private System.Windows.Forms.ToolStripButton imageStuffButtonRight;
        private System.Windows.Forms.ToolStripButton imageStuffButtonLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel imageStuffLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton sortByButtons;
        private System.Windows.Forms.ToolStripMenuItem sortByTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsortToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox sortByCategoryComboBox;
        private System.Windows.Forms.ToolStripComboBox sortByAuthorComboBox;
        private System.Windows.Forms.Button downloadReadmeButton;
        private System.Windows.Forms.ToolStripButton reloadPatchServer;
        private System.Windows.Forms.Button downloadIPStoFileButton;
        private System.Windows.Forms.RichTextBox WebsiteBox;
        private System.Windows.Forms.RichTextBox DetailsBox;
        private System.Windows.Forms.Label DetailsLabel;
        private System.Windows.Forms.ToolStripMenuItem PatchServerAddButton;
        private System.Windows.Forms.ToolStripComboBox patchHTTPServer;
        private System.Windows.Forms.Label WebsiteLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ToolStripNumericUpDown imageMaxNum;
        private System.Windows.Forms.ToolStripLabel maxImagesLabel;
        private System.Windows.Forms.ComboBox versionDisplayComboBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton verifyIPSButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton helpTips;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}