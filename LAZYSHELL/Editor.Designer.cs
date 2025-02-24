
namespace LAZYSHELL
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panelROMinfo = new System.Windows.Forms.Panel();
            this.infoROM = new System.Windows.Forms.Panel();
            this.romInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.infoROMloaded = new System.Windows.Forms.ToolStrip();
            this.loadRom = new System.Windows.Forms.ToolStripButton();
            this.loadRomTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.openEditorsTray = new System.Windows.Forms.ToolStrip();
            this.openAllies = new System.Windows.Forms.ToolStripButton();
            this.openAnimations = new System.Windows.Forms.ToolStripButton();
            this.openAttacks = new System.Windows.Forms.ToolStripButton();
            this.openAudio = new System.Windows.Forms.ToolStripButton();
            this.openBattlefields = new System.Windows.Forms.ToolStripButton();
            this.openDialogues = new System.Windows.Forms.ToolStripButton();
            this.openEffects = new System.Windows.Forms.ToolStripButton();
            this.openEventScripts = new System.Windows.Forms.ToolStripButton();
            this.openFormations = new System.Windows.Forms.ToolStripButton();
            this.openMainTitle = new System.Windows.Forms.ToolStripButton();
            this.openItems = new System.Windows.Forms.ToolStripButton();
            this.openLevels = new System.Windows.Forms.ToolStripButton();
            this.openMenus = new System.Windows.Forms.ToolStripButton();
            this.openMiniGames = new System.Windows.Forms.ToolStripButton();
            this.openMonsters = new System.Windows.Forms.ToolStripButton();
            this.openSprites = new System.Windows.Forms.ToolStripButton();
            this.openWorldMaps = new System.Windows.Forms.ToolStripButton();
            this.openProject = new System.Windows.Forms.ToolStripButton();
            this.openPatches = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.DockingTray = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.openAll = new System.Windows.Forms.ToolStripButton();
            this.closeAll = new System.Windows.Forms.ToolStripButton();
            this.restoreAll = new System.Windows.Forms.ToolStripButton();
            this.minimizeAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.docking = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadAllData = new System.Windows.Forms.ToolStripButton();
            this.clearModel = new System.Windows.Forms.ToolStripButton();
            this.panelOpenEditorsTray = new System.Windows.Forms.Panel();
            this.hideOpenEditorsPanel = new System.Windows.Forms.ToolStrip();
            this.hideOpenEditors = new System.Windows.Forms.ToolStripButton();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.recentFiles = new System.Windows.Forms.ToolStripDropDownButton();
            this.refreshROM = new System.Windows.Forms.ToolStripButton();
            this.closeROM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.history = new System.Windows.Forms.ToolStripButton();
            this.restoreElementsToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.hexViewer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.openSettings = new System.Windows.Forms.ToolStripButton();
            this.help = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.layoutUpdate = new System.Windows.Forms.ToolStripButton();
            this.hideDock = new System.Windows.Forms.ToolStripButton();
            this.showROMInfo = new System.Windows.Forms.ToolStripButton();
            this.info = new System.Windows.Forms.ToolStripButton();
            this.panelROMinfo.SuspendLayout();
            this.infoROM.SuspendLayout();
            this.infoROMloaded.SuspendLayout();
            this.openEditorsTray.SuspendLayout();
            this.DockingTray.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panelOpenEditorsTray.SuspendLayout();
            this.hideOpenEditorsPanel.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(64, 6);
            // 
            // panelROMinfo
            // 
            this.panelROMinfo.AutoSize = true;
            this.panelROMinfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelROMinfo.BackColor = System.Drawing.SystemColors.ControlText;
            this.panelROMinfo.Controls.Add(this.infoROM);
            this.panelROMinfo.Controls.Add(this.infoROMloaded);
            this.panelROMinfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelROMinfo.Location = new System.Drawing.Point(0, 25);
            this.panelROMinfo.Name = "panelROMinfo";
            this.panelROMinfo.Size = new System.Drawing.Size(528, 75);
            this.panelROMinfo.TabIndex = 1;
            // 
            // infoROM
            // 
            this.infoROM.Controls.Add(this.romInfo);
            this.infoROM.Controls.Add(this.label1);
            this.infoROM.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoROM.Location = new System.Drawing.Point(0, 25);
            this.infoROM.Name = "infoROM";
            this.infoROM.Size = new System.Drawing.Size(528, 50);
            this.infoROM.TabIndex = 1;
            // 
            // romInfo
            // 
            this.romInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.romInfo.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.romInfo.Location = new System.Drawing.Point(101, 0);
            this.romInfo.Name = "romInfo";
            this.romInfo.ReadOnly = true;
            this.romInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.romInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.romInfo.Size = new System.Drawing.Size(427, 50);
            this.romInfo.TabIndex = 1;
            this.romInfo.Text = "";
            this.romInfo.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(101, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rom Name\r\nHeader\r\nChecksum\r\nGamecode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoROMloaded
            // 
            this.infoROMloaded.BackColor = System.Drawing.SystemColors.Control;
            this.infoROMloaded.CanOverflow = false;
            this.infoROMloaded.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoROMloaded.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.infoROMloaded.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRom,
            this.loadRomTextBox});
            this.infoROMloaded.Location = new System.Drawing.Point(0, 0);
            this.infoROMloaded.Name = "infoROMloaded";
            this.infoROMloaded.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.infoROMloaded.Size = new System.Drawing.Size(528, 25);
            this.infoROMloaded.Stretch = true;
            this.infoROMloaded.TabIndex = 0;
            this.infoROMloaded.Text = "toolStrip1";
            this.infoROMloaded.SizeChanged += new System.EventHandler(this.toolStrip1_SizeChanged);
            // 
            // loadRom
            // 
            this.loadRom.AutoSize = false;
            this.loadRom.Image = global::LAZYSHELL.Properties.Resources.cartridge;
            this.loadRom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadRom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadRom.Name = "loadRom";
            this.loadRom.Size = new System.Drawing.Size(101, 22);
            this.loadRom.Text = "Load ROM...";
            this.loadRom.Click += new System.EventHandler(this.loadRom_Click);
            // 
            // loadRomTextBox
            // 
            this.loadRomTextBox.AutoSize = false;
            this.loadRomTextBox.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadRomTextBox.Name = "loadRomTextBox";
            this.loadRomTextBox.ReadOnly = true;
            this.loadRomTextBox.Size = new System.Drawing.Size(418, 25);
            this.loadRomTextBox.Click += new System.EventHandler(this.loadRomTextBox_Click);
            // 
            // openEditorsTray
            // 
            this.openEditorsTray.AllowItemReorder = true;
            this.openEditorsTray.BackColor = System.Drawing.SystemColors.Control;
            this.openEditorsTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openEditorsTray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openEditorsTray.Enabled = false;
            this.openEditorsTray.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openEditorsTray.GripMargin = new System.Windows.Forms.Padding(0);
            this.openEditorsTray.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.openEditorsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAllies,
            this.openAnimations,
            this.openAttacks,
            this.openAudio,
            this.openBattlefields,
            this.openDialogues,
            this.openEffects,
            this.openEventScripts,
            this.openFormations,
            this.openMainTitle,
            this.openItems,
            this.openLevels,
            this.openMenus,
            this.openMiniGames,
            this.openMonsters,
            this.openSprites,
            this.openWorldMaps,
            this.toolStripSeparator1,
            this.openProject,
            this.openPatches});
            this.openEditorsTray.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.openEditorsTray.Location = new System.Drawing.Point(0, 18);
            this.openEditorsTray.Name = "openEditorsTray";
            this.openEditorsTray.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.openEditorsTray.Size = new System.Drawing.Size(99, 465);
            this.openEditorsTray.TabIndex = 2;
            // 
            // openAllies
            // 
            this.openAllies.AutoSize = false;
            this.openAllies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openAllies.Image = global::LAZYSHELL.Properties.Resources.mainAlliesBig;
            this.openAllies.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAllies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAllies.Margin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.openAllies.Name = "openAllies";
            this.openAllies.Size = new System.Drawing.Size(90, 20);
            this.openAllies.Text = "Allies";
            this.openAllies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAllies.ToolTipText = "Edit new game stats and level-ups for allies";
            this.openAllies.Click += new System.EventHandler(this.openAllies_Click);
            // 
            // openAnimations
            // 
            this.openAnimations.AutoSize = false;
            this.openAnimations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openAnimations.Image = global::LAZYSHELL.Properties.Resources.mainAnimationsBig_2;
            this.openAnimations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAnimations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAnimations.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openAnimations.Name = "openAnimations";
            this.openAnimations.Size = new System.Drawing.Size(90, 20);
            this.openAnimations.Text = "Animations";
            this.openAnimations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAnimations.ToolTipText = "Edit battle animations for battle events, spells, attacks, sprites";
            this.openAnimations.Click += new System.EventHandler(this.openAnimations_Click);
            // 
            // openAttacks
            // 
            this.openAttacks.AutoSize = false;
            this.openAttacks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openAttacks.Image = global::LAZYSHELL.Properties.Resources.mainAttacksBig_2;
            this.openAttacks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAttacks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAttacks.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openAttacks.Name = "openAttacks";
            this.openAttacks.Size = new System.Drawing.Size(90, 20);
            this.openAttacks.Text = "Attacks";
            this.openAttacks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAttacks.ToolTipText = "Edit monster attacks, spells, and ally spells";
            this.openAttacks.Click += new System.EventHandler(this.openAttacks_Click);
            // 
            // openAudio
            // 
            this.openAudio.AutoSize = false;
            this.openAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openAudio.Image = global::LAZYSHELL.Properties.Resources.mainAudioBig;
            this.openAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAudio.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openAudio.Name = "openAudio";
            this.openAudio.Size = new System.Drawing.Size(90, 20);
            this.openAudio.Text = "Audio";
            this.openAudio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAudio.ToolTipText = "Edit audio samples and SPC instruments and tracks";
            this.openAudio.Click += new System.EventHandler(this.openAudio_Click);
            // 
            // openBattlefields
            // 
            this.openBattlefields.AutoSize = false;
            this.openBattlefields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openBattlefields.Image = global::LAZYSHELL.Properties.Resources.mainBattlefieldsBig;
            this.openBattlefields.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openBattlefields.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openBattlefields.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openBattlefields.Name = "openBattlefields";
            this.openBattlefields.Size = new System.Drawing.Size(90, 20);
            this.openBattlefields.Text = "Battlefields";
            this.openBattlefields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openBattlefields.ToolTipText = "Edit battle / monster formation backgrounds";
            this.openBattlefields.Click += new System.EventHandler(this.openBattlefields_Click);
            // 
            // openDialogues
            // 
            this.openDialogues.AutoSize = false;
            this.openDialogues.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openDialogues.Image = global::LAZYSHELL.Properties.Resources.mainDialoguesBig_2;
            this.openDialogues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openDialogues.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openDialogues.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openDialogues.Name = "openDialogues";
            this.openDialogues.Size = new System.Drawing.Size(90, 20);
            this.openDialogues.Text = "Dialogues";
            this.openDialogues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openDialogues.ToolTipText = "Edit overworld dialogue scripts, battle dialogues, and fonts";
            this.openDialogues.Click += new System.EventHandler(this.openDialogues_Click);
            // 
            // openEffects
            // 
            this.openEffects.AutoSize = false;
            this.openEffects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openEffects.Image = ((System.Drawing.Image)(resources.GetObject("openEffects.Image")));
            this.openEffects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEffects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openEffects.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openEffects.Name = "openEffects";
            this.openEffects.Size = new System.Drawing.Size(90, 20);
            this.openEffects.Text = "Effects";
            this.openEffects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEffects.ToolTipText = "Edit spell effect animations, palettes, and graphics";
            this.openEffects.Click += new System.EventHandler(this.openEffects_Click);
            // 
            // openEventScripts
            // 
            this.openEventScripts.AutoSize = false;
            this.openEventScripts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openEventScripts.Image = global::LAZYSHELL.Properties.Resources.mainEventScriptsBig_2;
            this.openEventScripts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEventScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openEventScripts.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openEventScripts.Name = "openEventScripts";
            this.openEventScripts.Size = new System.Drawing.Size(90, 20);
            this.openEventScripts.Text = "Event Scripts";
            this.openEventScripts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEventScripts.ToolTipText = "Edit event scripts and their respective command collections";
            this.openEventScripts.Click += new System.EventHandler(this.openEventScripts_Click);
            // 
            // openFormations
            // 
            this.openFormations.AutoSize = false;
            this.openFormations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openFormations.Image = ((System.Drawing.Image)(resources.GetObject("openFormations.Image")));
            this.openFormations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openFormations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFormations.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openFormations.Name = "openFormations";
            this.openFormations.Size = new System.Drawing.Size(90, 20);
            this.openFormations.Text = "Formations";
            this.openFormations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openFormations.ToolTipText = "Edit monster battle formations, formations properties, and packs";
            this.openFormations.Click += new System.EventHandler(this.openFormations_Click);
            // 
            // openMainTitle
            // 
            this.openMainTitle.AutoSize = false;
            this.openMainTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openMainTitle.Image = global::LAZYSHELL.Properties.Resources.mainMainTitleBig;
            this.openMainTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMainTitle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMainTitle.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openMainTitle.Name = "openMainTitle";
            this.openMainTitle.Size = new System.Drawing.Size(90, 20);
            this.openMainTitle.Text = "Intro";
            this.openMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMainTitle.ToolTipText = "Edit the main title logo and backgrounds";
            this.openMainTitle.Click += new System.EventHandler(this.openMainTitle_Click);
            // 
            // openItems
            // 
            this.openItems.AutoSize = false;
            this.openItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openItems.Image = global::LAZYSHELL.Properties.Resources.mainItemsBig_3;
            this.openItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openItems.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openItems.Name = "openItems";
            this.openItems.Size = new System.Drawing.Size(90, 20);
            this.openItems.Text = "Items";
            this.openItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openItems.ToolTipText = "Edit items, equipment, and shops";
            this.openItems.Click += new System.EventHandler(this.openItems_Click);
            // 
            // openLevels
            // 
            this.openLevels.AutoSize = false;
            this.openLevels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openLevels.Image = global::LAZYSHELL.Properties.Resources.mainLevelsBig_3;
            this.openLevels.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openLevels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openLevels.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openLevels.Name = "openLevels";
            this.openLevels.Size = new System.Drawing.Size(90, 20);
            this.openLevels.Text = "Levels";
            this.openLevels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openLevels.ToolTipText = "Edit level / location maps, NPCs, exits and event fields,  etc.";
            this.openLevels.Click += new System.EventHandler(this.openLevels_Click);
            // 
            // openMenus
            // 
            this.openMenus.AutoSize = false;
            this.openMenus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openMenus.Image = global::LAZYSHELL.Properties.Resources.mainMenusBig;
            this.openMenus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMenus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMenus.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openMenus.Name = "openMenus";
            this.openMenus.Size = new System.Drawing.Size(90, 20);
            this.openMenus.Text = "Menus";
            this.openMenus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMenus.ToolTipText = "Edit the overworld and new game menus";
            this.openMenus.Click += new System.EventHandler(this.openMenus_Click);
            // 
            // openMiniGames
            // 
            this.openMiniGames.AutoSize = false;
            this.openMiniGames.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openMiniGames.Image = ((System.Drawing.Image)(resources.GetObject("openMiniGames.Image")));
            this.openMiniGames.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMiniGames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMiniGames.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openMiniGames.Name = "openMiniGames";
            this.openMiniGames.Size = new System.Drawing.Size(90, 20);
            this.openMiniGames.Text = "Mini-games";
            this.openMiniGames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMiniGames.ToolTipText = "Edit the mine cart mini-game tracks";
            this.openMiniGames.Click += new System.EventHandler(this.openMiniGames_Click);
            // 
            // openMonsters
            // 
            this.openMonsters.AutoSize = false;
            this.openMonsters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openMonsters.Image = global::LAZYSHELL.Properties.Resources.mainMonstersBig;
            this.openMonsters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMonsters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMonsters.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openMonsters.Name = "openMonsters";
            this.openMonsters.Size = new System.Drawing.Size(90, 20);
            this.openMonsters.Text = "Monsters";
            this.openMonsters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openMonsters.ToolTipText = "Edit monster battle statistics and properties";
            this.openMonsters.Click += new System.EventHandler(this.openMonsters_Click);
            // 
            // openSprites
            // 
            this.openSprites.AutoSize = false;
            this.openSprites.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openSprites.Image = global::LAZYSHELL.Properties.Resources.mainSpritesBig_2;
            this.openSprites.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openSprites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSprites.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openSprites.Name = "openSprites";
            this.openSprites.Size = new System.Drawing.Size(90, 20);
            this.openSprites.Text = "Sprites";
            this.openSprites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openSprites.ToolTipText = "Edit sprite animations, palettes, and graphics";
            this.openSprites.Click += new System.EventHandler(this.openSprites_Click);
            // 
            // openWorldMaps
            // 
            this.openWorldMaps.AutoSize = false;
            this.openWorldMaps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openWorldMaps.Image = global::LAZYSHELL.Properties.Resources.mainWorldMapsBig;
            this.openWorldMaps.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openWorldMaps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openWorldMaps.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openWorldMaps.Name = "openWorldMaps";
            this.openWorldMaps.Size = new System.Drawing.Size(92, 20);
            this.openWorldMaps.Text = "World Maps";
            this.openWorldMaps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openWorldMaps.ToolTipText = "Edit world maps, locations, palettes, and graphics";
            this.openWorldMaps.Click += new System.EventHandler(this.openWorldMaps_Click);
            // 
            // openProject
            // 
            this.openProject.AutoSize = false;
            this.openProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openProject.Image = global::LAZYSHELL.Properties.Resources.mainProjectBig;
            this.openProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openProject.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openProject.Name = "openProject";
            this.openProject.Size = new System.Drawing.Size(90, 20);
            this.openProject.Text = "Notes";
            this.openProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openProject.ToolTipText = "Open the notes manager";
            this.openProject.Click += new System.EventHandler(this.openProject_Click);
            // 
            // openPatches
            // 
            this.openPatches.AutoSize = false;
            this.openPatches.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openPatches.Image = global::LAZYSHELL.Properties.Resources.mainPatchesBig;
            this.openPatches.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openPatches.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPatches.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openPatches.Name = "openPatches";
            this.openPatches.Size = new System.Drawing.Size(90, 20);
            this.openPatches.Text = "Patches";
            this.openPatches.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openPatches.ToolTipText = "Apply a hack from the patch HTTP server";
            this.openPatches.Click += new System.EventHandler(this.openPatches_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(357, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(104, 6);
            // 
            // DockingTray
            // 
            this.DockingTray.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DockingTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DockingTray.Controls.Add(this.panel2);
            this.DockingTray.Controls.Add(this.toolStrip3);
            this.DockingTray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockingTray.Location = new System.Drawing.Point(103, 100);
            this.DockingTray.Name = "DockingTray";
            this.DockingTray.Size = new System.Drawing.Size(425, 487);
            this.DockingTray.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 462);
            this.panel2.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(1, 1);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(425, 461);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.Visible = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Enabled = false;
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAll,
            this.closeAll,
            this.restoreAll,
            this.minimizeAll,
            this.toolStripSeparator2,
            this.docking,
            this.toolStripSeparator3,
            this.loadAllData,
            this.clearModel});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(425, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // openAll
            // 
            this.openAll.Image = global::LAZYSHELL.Properties.Resources.openAll;
            this.openAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openAll.Name = "openAll";
            this.openAll.Size = new System.Drawing.Size(23, 22);
            this.openAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openAll.ToolTipText = "Open All Editors";
            this.openAll.Click += new System.EventHandler(this.openAll_Click);
            // 
            // closeAll
            // 
            this.closeAll.Image = global::LAZYSHELL.Properties.Resources.closeAll;
            this.closeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeAll.Name = "closeAll";
            this.closeAll.Size = new System.Drawing.Size(23, 22);
            this.closeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeAll.ToolTipText = "Close All Editors";
            this.closeAll.Click += new System.EventHandler(this.closeAll_Click);
            // 
            // restoreAll
            // 
            this.restoreAll.Image = global::LAZYSHELL.Properties.Resources.restoreAll;
            this.restoreAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.restoreAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restoreAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreAll.Name = "restoreAll";
            this.restoreAll.Size = new System.Drawing.Size(23, 22);
            this.restoreAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.restoreAll.ToolTipText = "Restore All Editors";
            this.restoreAll.Click += new System.EventHandler(this.restoreAll_Click);
            // 
            // minimizeAll
            // 
            this.minimizeAll.Image = global::LAZYSHELL.Properties.Resources.minimizeAll;
            this.minimizeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minimizeAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.minimizeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.minimizeAll.Name = "minimizeAll";
            this.minimizeAll.Size = new System.Drawing.Size(23, 22);
            this.minimizeAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minimizeAll.ToolTipText = "Minimize All Editors";
            this.minimizeAll.Click += new System.EventHandler(this.minimizeAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // docking
            // 
            this.docking.CheckOnClick = true;
            this.docking.Image = global::LAZYSHELL.Properties.Resources.dock;
            this.docking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.docking.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.docking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.docking.Name = "docking";
            this.docking.Size = new System.Drawing.Size(23, 22);
            this.docking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.docking.ToolTipText = "Dock Editors";
            this.docking.Click += new System.EventHandler(this.docking_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // loadAllData
            // 
            this.loadAllData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadAllData.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.loadAllData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadAllData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadAllData.Name = "loadAllData";
            this.loadAllData.Size = new System.Drawing.Size(23, 22);
            this.loadAllData.Text = "Reset Editor Memory";
            this.loadAllData.Click += new System.EventHandler(this.loadAllData_Click);
            // 
            // clearModel
            // 
            this.clearModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearModel.Image = global::LAZYSHELL.Properties.Resources.closeAll;
            this.clearModel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearModel.Name = "clearModel";
            this.clearModel.Size = new System.Drawing.Size(23, 22);
            this.clearModel.Text = "Clear Editor Memory";
            this.clearModel.Click += new System.EventHandler(this.clearModel_Click);
            // 
            // panelOpenEditorsTray
            // 
            this.panelOpenEditorsTray.AutoScroll = true;
            this.panelOpenEditorsTray.BackColor = System.Drawing.SystemColors.Control;
            this.panelOpenEditorsTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelOpenEditorsTray.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOpenEditorsTray.Controls.Add(this.openEditorsTray);
            this.panelOpenEditorsTray.Controls.Add(this.hideOpenEditorsPanel);
            this.panelOpenEditorsTray.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOpenEditorsTray.Location = new System.Drawing.Point(0, 100);
            this.panelOpenEditorsTray.Margin = new System.Windows.Forms.Padding(0);
            this.panelOpenEditorsTray.Name = "panelOpenEditorsTray";
            this.panelOpenEditorsTray.Size = new System.Drawing.Size(103, 487);
            this.panelOpenEditorsTray.TabIndex = 5;
            this.panelOpenEditorsTray.SizeChanged += new System.EventHandler(this.panelOpenEditorsTray_SizeChanged);
            // 
            // hideOpenEditorsPanel
            // 
            this.hideOpenEditorsPanel.AllowMerge = false;
            this.hideOpenEditorsPanel.AutoSize = false;
            this.hideOpenEditorsPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.hideOpenEditorsPanel.BackgroundImage = global::LAZYSHELL.Properties.Resources._bg;
            this.hideOpenEditorsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.hideOpenEditorsPanel.CanOverflow = false;
            this.hideOpenEditorsPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideOpenEditorsPanel.GripMargin = new System.Windows.Forms.Padding(0);
            this.hideOpenEditorsPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.hideOpenEditorsPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideOpenEditors});
            this.hideOpenEditorsPanel.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.hideOpenEditorsPanel.Location = new System.Drawing.Point(0, 0);
            this.hideOpenEditorsPanel.Name = "hideOpenEditorsPanel";
            this.hideOpenEditorsPanel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.hideOpenEditorsPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.hideOpenEditorsPanel.Size = new System.Drawing.Size(99, 18);
            this.hideOpenEditorsPanel.TabIndex = 2;
            this.hideOpenEditorsPanel.Click += new System.EventHandler(this.hideOpenEditors_Click);
            // 
            // hideOpenEditors
            // 
            this.hideOpenEditors.AutoSize = false;
            this.hideOpenEditors.AutoToolTip = false;
            this.hideOpenEditors.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.hideOpenEditors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.hideOpenEditors.CheckOnClick = true;
            this.hideOpenEditors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideOpenEditors.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideOpenEditors.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.hideOpenEditors.Image = global::LAZYSHELL.Properties.Resources.moveRight;
            this.hideOpenEditors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hideOpenEditors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideOpenEditors.Margin = new System.Windows.Forms.Padding(-2, -2, 0, 0);
            this.hideOpenEditors.Name = "hideOpenEditors";
            this.hideOpenEditors.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.hideOpenEditors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.hideOpenEditors.RightToLeftAutoMirrorImage = true;
            this.hideOpenEditors.Size = new System.Drawing.Size(105, 22);
            this.hideOpenEditors.Text = " ";
            this.hideOpenEditors.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.hideOpenEditors.ToolTipText = "Collapse open editors tray";
            this.hideOpenEditors.Click += new System.EventHandler(this.hideOpenEditors_Click);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentFiles,
            this.refreshROM,
            this.closeROM,
            this.toolStripSeparator5,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator11,
            this.history,
            this.restoreElementsToolStripMenuItem,
            this.hexViewer,
            this.toolStripSeparator9,
            this.openSettings,
            this.help,
            this.info,
            this.toolStripSeparator6,
            this.layoutUpdate,
            this.hideDock,
            this.showROMInfo});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(528, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // recentFiles
            // 
            this.recentFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recentFiles.Image = global::LAZYSHELL.Properties.Resources.recentFiles;
            this.recentFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.recentFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recentFiles.Name = "recentFiles";
            this.recentFiles.Size = new System.Drawing.Size(29, 22);
            this.recentFiles.ToolTipText = "Recent ROM Files";
            // 
            // refreshROM
            // 
            this.refreshROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshROM.Enabled = false;
            this.refreshROM.Image = global::LAZYSHELL.Properties.Resources.cartridgeReload;
            this.refreshROM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshROM.Name = "refreshROM";
            this.refreshROM.Size = new System.Drawing.Size(23, 22);
            this.refreshROM.ToolTipText = "Reload ROM";
            this.refreshROM.Click += new System.EventHandler(this.refreshROM_Click);
            // 
            // closeROM
            // 
            this.closeROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeROM.Enabled = false;
            this.closeROM.Image = global::LAZYSHELL.Properties.Resources.cartridgeClose;
            this.closeROM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeROM.Name = "closeROM";
            this.closeROM.Size = new System.Drawing.Size(23, 22);
            this.closeROM.ToolTipText = "Close ROM";
            this.closeROM.Click += new System.EventHandler(this.closeROM_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.save_small;
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripMenuItem.ToolTipText = "Save ROM";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.saveAs_small;
            this.saveAsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveAsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.saveAsToolStripMenuItem.ToolTipText = "Save ROM As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // history
            // 
            this.history.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.history.Image = global::LAZYSHELL.Properties.Resources.history;
            this.history.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.history.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(23, 22);
            this.history.ToolTipText = "Event History";
            this.history.Click += new System.EventHandler(this.history_Click);
            // 
            // restoreElementsToolStripMenuItem
            // 
            this.restoreElementsToolStripMenuItem.Enabled = false;
            this.restoreElementsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importBinary;
            this.restoreElementsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.restoreElementsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreElementsToolStripMenuItem.Name = "restoreElementsToolStripMenuItem";
            this.restoreElementsToolStripMenuItem.Size = new System.Drawing.Size(23, 22);
            this.restoreElementsToolStripMenuItem.ToolTipText = "Import elements from another ROM";
            this.restoreElementsToolStripMenuItem.Click += new System.EventHandler(this.restoreElementsToolStripMenuItem_Click);
            // 
            // hexViewer
            // 
            this.hexViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hexViewer.Enabled = false;
            this.hexViewer.Image = global::LAZYSHELL.Properties.Resources.hexEditor;
            this.hexViewer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.hexViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.Size = new System.Drawing.Size(23, 22);
            this.hexViewer.Text = "Open Hex Editor";
            this.hexViewer.Click += new System.EventHandler(this.hexViewer_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // openSettings
            // 
            this.openSettings.Image = global::LAZYSHELL.Properties.Resources.settings;
            this.openSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSettings.Name = "openSettings";
            this.openSettings.Size = new System.Drawing.Size(23, 22);
            this.openSettings.ToolTipText = "Settings";
            this.openSettings.Click += new System.EventHandler(this.openSettings_Click);
            // 
            // help
            // 
            this.help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.help.Image = global::LAZYSHELL.Properties.Resources.help_small;
            this.help.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(23, 22);
            this.help.Text = "Open Help Window";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // layoutUpdate
            // 
            this.layoutUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.layoutUpdate.CheckOnClick = true;
            this.layoutUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.layoutUpdate.Image = global::LAZYSHELL.Properties.Resources.zoomin_small;
            this.layoutUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layoutUpdate.Name = "layoutUpdate";
            this.layoutUpdate.Size = new System.Drawing.Size(23, 22);
            this.layoutUpdate.Text = "Toggle Big Icons";
            this.layoutUpdate.ToolTipText = "Enlarges the editor icons (Hold alt to drag icons)";
            this.layoutUpdate.Click += new System.EventHandler(this.layoutUpdate_Click);
            // 
            // hideDock
            // 
            this.hideDock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.hideDock.CheckOnClick = true;
            this.hideDock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideDock.Image = global::LAZYSHELL.Properties.Resources.dock;
            this.hideDock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideDock.Name = "hideDock";
            this.hideDock.Size = new System.Drawing.Size(23, 22);
            this.hideDock.ToolTipText = "Dock Editors";
            this.hideDock.Click += new System.EventHandler(this.hideDock_Click);
            // 
            // showROMInfo
            // 
            this.showROMInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showROMInfo.Enabled = false;
            this.showROMInfo.Image = global::LAZYSHELL.Properties.Resources.romInfo;
            this.showROMInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showROMInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showROMInfo.Name = "showROMInfo";
            this.showROMInfo.Size = new System.Drawing.Size(23, 22);
            this.showROMInfo.ToolTipText = "Show ROM Info";
            this.showROMInfo.Click += new System.EventHandler(this.showROMInfo_Click);
            // 
            // info
            // 
            this.info.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.info.Image = global::LAZYSHELL.Properties.Resources.mainBig;
            this.info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(23, 22);
            this.info.ToolTipText = "About";
            this.info.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 587);
            this.Controls.Add(this.DockingTray);
            this.Controls.Add(this.panelOpenEditorsTray);
            this.Controls.Add(this.panelROMinfo);
            this.Controls.Add(this.toolStrip4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LAZYSHELL++ - Super Mario RPG Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Editor_FormClosed);
            this.panelROMinfo.ResumeLayout(false);
            this.panelROMinfo.PerformLayout();
            this.infoROM.ResumeLayout(false);
            this.infoROMloaded.ResumeLayout(false);
            this.infoROMloaded.PerformLayout();
            this.openEditorsTray.ResumeLayout(false);
            this.openEditorsTray.PerformLayout();
            this.DockingTray.ResumeLayout(false);
            this.DockingTray.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panelOpenEditorsTray.ResumeLayout(false);
            this.panelOpenEditorsTray.PerformLayout();
            this.hideOpenEditorsPanel.ResumeLayout(false);
            this.hideOpenEditorsPanel.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelROMinfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip infoROMloaded;
        private System.Windows.Forms.ToolStripButton loadRom;
        private System.Windows.Forms.ToolStripTextBox loadRomTextBox;
        private System.Windows.Forms.ToolStrip openEditorsTray;
        private System.Windows.Forms.ToolStripButton openMonsters;
        private System.Windows.Forms.ToolStripButton openLevels;
        private System.Windows.Forms.ToolStripButton openBattlefields;
        private System.Windows.Forms.ToolStripButton openEventScripts;
        private System.Windows.Forms.ToolStripButton openSprites;
        private System.Windows.Forms.ToolStripButton openDialogues;
        private System.Windows.Forms.ToolStripButton openWorldMaps;
        private System.Windows.Forms.ToolStripButton openEffects;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton openPatches;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton openAttacks;
        private System.Windows.Forms.ToolStripButton openItems;
        private System.Windows.Forms.ToolStripButton openMainTitle;
        private System.Windows.Forms.ToolStripButton openFormations;
        private System.Windows.Forms.Panel infoROM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton openAnimations;
        private System.Windows.Forms.Panel DockingTray;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelOpenEditorsTray;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton restoreElementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton showROMInfo;
        private System.Windows.Forms.ToolStripButton docking;
        private System.Windows.Forms.ToolStripDropDownButton recentFiles;
        private System.Windows.Forms.ToolStripButton openAll;
        private System.Windows.Forms.ToolStripButton closeAll;
        private System.Windows.Forms.ToolStripButton minimizeAll;
        private System.Windows.Forms.ToolStripButton restoreAll;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton loadAllData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton clearModel;
        private System.Windows.Forms.ToolStripButton refreshROM;
        private System.Windows.Forms.ToolStripButton closeROM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton openAudio;
        private System.Windows.Forms.ToolStripButton hexViewer;
        private System.Windows.Forms.ToolStripButton history;
        private System.Windows.Forms.ToolStripButton openMenus;
        private System.Windows.Forms.ToolStripButton openMiniGames;
        private System.Windows.Forms.ToolStripButton help;
        private System.Windows.Forms.ToolStripButton openProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton openSettings;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.RichTextBox romInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton info;
        private System.Windows.Forms.ToolStripButton layoutUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton openAllies;
        private System.Windows.Forms.ToolStripButton hideDock;
        private System.Windows.Forms.ToolStrip hideOpenEditorsPanel;
        private System.Windows.Forms.ToolStripButton hideOpenEditors;
    }
}

