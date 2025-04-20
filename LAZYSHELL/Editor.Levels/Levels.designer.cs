using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    partial class Levels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Levels));
            this.levelNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.levelName = new System.Windows.Forms.ToolStripComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.npcMapHeader = new System.Windows.Forms.NumericUpDown();
            this.openPartitions = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.npcAttributes = new System.Windows.Forms.CheckedListBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.npcAfterBattle = new System.Windows.Forms.ComboBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.npcInsertObject = new System.Windows.Forms.ToolStripButton();
            this.npcInsertInstance = new System.Windows.Forms.ToolStripButton();
            this.npcRemoveObject = new System.Windows.Forms.ToolStripButton();
            this.npcCopy = new System.Windows.Forms.ToolStripButton();
            this.npcPaste = new System.Windows.Forms.ToolStripButton();
            this.npcDuplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.npcMoveUp = new System.Windows.Forms.ToolStripButton();
            this.npcMoveDown = new System.Windows.Forms.ToolStripButton();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.npcFace = new System.Windows.Forms.ComboBox();
            this.npcX = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.npcY = new System.Windows.Forms.NumericUpDown();
            this.npcZ_half = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.npcZ = new System.Windows.Forms.NumericUpDown();
            this.label56 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.findNPCNum = new System.Windows.Forms.Button();
            this.npcVisible = new System.Windows.Forms.CheckBox();
            this.npcID = new System.Windows.Forms.NumericUpDown();
            this.npcEngageTrigger = new System.Windows.Forms.ComboBox();
            this.npcMovement = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.npcSpeedPlus = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.npcPropertyA = new System.Windows.Forms.NumericUpDown();
            this.npcEventORPack = new System.Windows.Forms.NumericUpDown();
            this.label104 = new System.Windows.Forms.Label();
            this.npcPropertyB = new System.Windows.Forms.NumericUpDown();
            this.npcEngageType = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.npcPropertyC = new System.Windows.Forms.NumericUpDown();
            this.npcGotoA = new System.Windows.Forms.Button();
            this.label116 = new System.Windows.Forms.Label();
            this.npcGotoB = new System.Windows.Forms.Button();
            this.npcsBytesLeft = new System.Windows.Forms.Label();
            this.npcObjectTree = new System.Windows.Forms.TreeView();
            this.mapNum = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.overlapCoordZPlusHalf = new System.Windows.Forms.CheckBox();
            this.label109 = new System.Windows.Forms.Label();
            this.overlapType = new System.Windows.Forms.NumericUpDown();
            this.overlapX = new System.Windows.Forms.NumericUpDown();
            this.overlapY = new System.Windows.Forms.NumericUpDown();
            this.label103 = new System.Windows.Forms.Label();
            this.overlapZ = new System.Windows.Forms.NumericUpDown();
            this.label107 = new System.Windows.Forms.Label();
            this.overlapFieldTree = new System.Windows.Forms.TreeView();
            this.overlapUnknownBits = new System.Windows.Forms.CheckedListBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.overlapFieldInsert = new System.Windows.Forms.ToolStripButton();
            this.overlapFieldDelete = new System.Windows.Forms.ToolStripButton();
            this.overlapFieldCopy = new System.Windows.Forms.ToolStripButton();
            this.overlapFieldPaste = new System.Windows.Forms.ToolStripButton();
            this.overlapFieldDuplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.overlapsBytesLeft = new System.Windows.Forms.ToolStripLabel();
            this.panelOverlapTileset = new System.Windows.Forms.Panel();
            this.pictureBoxOverlaps = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.eventFace = new System.Windows.Forms.ComboBox();
            this.eventsWidthYPlusHalf = new System.Windows.Forms.CheckBox();
            this.eventGotoA = new System.Windows.Forms.Button();
            this.eventsWidthXPlusHalf = new System.Windows.Forms.CheckBox();
            this.eventLength = new System.Windows.Forms.NumericUpDown();
            this.eventY = new System.Windows.Forms.NumericUpDown();
            this.label127 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.eventX = new System.Windows.Forms.NumericUpDown();
            this.eventZ = new System.Windows.Forms.NumericUpDown();
            this.label131 = new System.Windows.Forms.Label();
            this.eventEvent = new System.Windows.Forms.NumericUpDown();
            this.eventHeight = new System.Windows.Forms.NumericUpDown();
            this.eventsList = new System.Windows.Forms.TreeView();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.eventsInsertField = new System.Windows.Forms.ToolStripButton();
            this.eventsDeleteField = new System.Windows.Forms.ToolStripButton();
            this.eventsCopyField = new System.Windows.Forms.ToolStripButton();
            this.eventsPasteField = new System.Windows.Forms.ToolStripButton();
            this.eventsDuplicateField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.eventsBytesLeft = new System.Windows.Forms.ToolStripLabel();
            this.label63 = new System.Windows.Forms.Label();
            this.panel52 = new System.Windows.Forms.Panel();
            this.exitsFieldTree = new System.Windows.Forms.TreeView();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.exitDestFace = new System.Windows.Forms.ComboBox();
            this.exitType = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.marioZCoordPlusHalf = new System.Windows.Forms.CheckBox();
            this.exitDest = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.exitDestY = new System.Windows.Forms.NumericUpDown();
            this.exitsShowMessage = new System.Windows.Forms.CheckBox();
            this.label124 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.exitDestZ = new System.Windows.Forms.NumericUpDown();
            this.exitDestX = new System.Windows.Forms.NumericUpDown();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.exits135LengthPlusHalf = new System.Windows.Forms.CheckBox();
            this.label119 = new System.Windows.Forms.Label();
            this.exitY = new System.Windows.Forms.NumericUpDown();
            this.exitFace = new System.Windows.Forms.ComboBox();
            this.exitX = new System.Windows.Forms.NumericUpDown();
            this.exitHeight = new System.Windows.Forms.NumericUpDown();
            this.exits45LengthPlusHalf = new System.Windows.Forms.CheckBox();
            this.exitZ = new System.Windows.Forms.NumericUpDown();
            this.label57 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.exitLength = new System.Windows.Forms.NumericUpDown();
            this.panel68 = new System.Windows.Forms.Panel();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.exitsInsertField = new System.Windows.Forms.ToolStripButton();
            this.exitsDeleteField = new System.Windows.Forms.ToolStripButton();
            this.exitsCopyField = new System.Windows.Forms.ToolStripButton();
            this.exitsPasteField = new System.Windows.Forms.ToolStripButton();
            this.exitsDuplicateField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.exitsBytesLeft = new System.Windows.Forms.ToolStripLabel();
            this.label61 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mapPaletteSetName = new System.Windows.Forms.ComboBox();
            this.mapPaletteSetNum = new System.Windows.Forms.NumericUpDown();
            this.label46 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mapBattlefieldName = new System.Windows.Forms.ComboBox();
            this.mapSetL3Priority = new System.Windows.Forms.CheckBox();
            this.mapTilemapL1Num = new System.Windows.Forms.NumericUpDown();
            this.mapPhysicalMapName = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.mapTilemapL2Num = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.mapTilemapL3Name = new System.Windows.Forms.ComboBox();
            this.mapTilemapL2Name = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.mapTilemapL1Name = new System.Windows.Forms.ComboBox();
            this.mapTilemapL3Num = new System.Windows.Forms.NumericUpDown();
            this.label45 = new System.Windows.Forms.Label();
            this.mapBattlefieldNum = new System.Windows.Forms.NumericUpDown();
            this.mapPhysicalMapNum = new System.Windows.Forms.NumericUpDown();
            this.label76 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mapTilesetL3Name = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.mapTilesetL2Name = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.mapTilesetL1Name = new System.Windows.Forms.ComboBox();
            this.mapTilesetL3Num = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.mapTilesetL1Num = new System.Windows.Forms.NumericUpDown();
            this.mapTilesetL2Num = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapGFXSetL3Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet4Num = new System.Windows.Forms.NumericUpDown();
            this.label44 = new System.Windows.Forms.Label();
            this.mapGFXSetL3Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet5Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet5Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet3Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet1Num = new System.Windows.Forms.NumericUpDown();
            this.mapGFXSet1Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet2Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet4Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet3Name = new System.Windows.Forms.ComboBox();
            this.mapGFXSet2Num = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.layerOBJEffects = new System.Windows.Forms.ComboBox();
            this.layerL3Effects = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.layerWaveEffect = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.layerL3ScrollSpeed = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.layerInfiniteAutoscroll = new System.Windows.Forms.CheckBox();
            this.layerL2ScrollShift = new System.Windows.Forms.CheckBox();
            this.layerL3ScrollShift = new System.Windows.Forms.CheckBox();
            this.label85 = new System.Windows.Forms.Label();
            this.layerL3ScrollDirection = new System.Windows.Forms.ComboBox();
            this.layerL2ScrollSpeed = new System.Windows.Forms.ComboBox();
            this.layerL2ScrollDirection = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.layerL3HSync = new System.Windows.Forms.ComboBox();
            this.layerL3VSync = new System.Windows.Forms.ComboBox();
            this.layerL2HSync = new System.Windows.Forms.ComboBox();
            this.layerL2VSync = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.layerScrollWrapping = new System.Windows.Forms.CheckedListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.layerL2LeftShift = new System.Windows.Forms.NumericUpDown();
            this.layerL2UpShift = new System.Windows.Forms.NumericUpDown();
            this.layerL3LeftShift = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.layerL3UpShift = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.layerMaskHighX = new System.Windows.Forms.NumericUpDown();
            this.layerLockMask = new System.Windows.Forms.CheckBox();
            this.layerMaskHighY = new System.Windows.Forms.NumericUpDown();
            this.layerMaskLowX = new System.Windows.Forms.NumericUpDown();
            this.layerMaskLowY = new System.Windows.Forms.NumericUpDown();
            this.layerMessageBox = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.layerColorMathMode = new System.Windows.Forms.ComboBox();
            this.layerColorMathIntensity = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.layerColorMathBG = new System.Windows.Forms.CheckBox();
            this.layerPrioritySet = new System.Windows.Forms.NumericUpDown();
            this.layerColorMathNPC = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL1 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenNPC = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL1 = new System.Windows.Forms.CheckBox();
            this.layerMainscreenNPC = new System.Windows.Forms.CheckBox();
            this.layerColorMathL1 = new System.Windows.Forms.CheckBox();
            this.layerColorMathL3 = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL2 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL3 = new System.Windows.Forms.CheckBox();
            this.layerSubscreenL2 = new System.Windows.Forms.CheckBox();
            this.layerMainscreenL3 = new System.Windows.Forms.CheckBox();
            this.layerColorMathL2 = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tileModsBytesLeft = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.tileModsLayers = new System.Windows.Forms.CheckedListBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tileModsY = new System.Windows.Forms.NumericUpDown();
            this.label50 = new System.Windows.Forms.Label();
            this.tileModsX = new System.Windows.Forms.NumericUpDown();
            this.tileModsHeight = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.tileModsWidth = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.tileModsFieldTree = new System.Windows.Forms.TreeView();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.tileModsInsertField = new System.Windows.Forms.ToolStripButton();
            this.tileModsInsertInstance = new System.Windows.Forms.ToolStripButton();
            this.tileModsDeleteField = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tileModsMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tileModsMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tileModsCopy = new System.Windows.Forms.ToolStripButton();
            this.tileModsPaste = new System.Windows.Forms.ToolStripButton();
            this.tileModsDuplicate = new System.Windows.Forms.ToolStripButton();
            this.label69 = new System.Windows.Forms.Label();
            this.panel55 = new System.Windows.Forms.Panel();
            this.panel27 = new System.Windows.Forms.Panel();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.solidModsY = new System.Windows.Forms.NumericUpDown();
            this.label67 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.solidModsX = new System.Windows.Forms.NumericUpDown();
            this.solidModsWidth = new System.Windows.Forms.NumericUpDown();
            this.solidModsHeight = new System.Windows.Forms.NumericUpDown();
            this.solidModsBytesLeft = new System.Windows.Forms.Label();
            this.solidModsFieldTree = new System.Windows.Forms.TreeView();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.solidModsInsert = new System.Windows.Forms.ToolStripButton();
            this.solidModsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.solidModsMoveUp = new System.Windows.Forms.ToolStripButton();
            this.solidModsMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.solidModsCopy = new System.Windows.Forms.ToolStripButton();
            this.solidModsPaste = new System.Windows.Forms.ToolStripButton();
            this.solidModsDuplicate = new System.Windows.Forms.ToolStripButton();
            this.label68 = new System.Windows.Forms.Label();
            this.toolStripLevel = new System.Windows.Forms.ToolStrip();
            this.navigateBck = new System.Windows.Forms.ToolStripButton();
            this.navigateFwd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchLevelNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.levelGotoEvent = new System.Windows.Forms.ToolStripButton();
            this.eventExit = new LAZYSHELL.ToolStripNumericUpDown();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.eventMusic = new System.Windows.Forms.ToolStripComboBox();
            this.hexEditor = new System.Windows.Forms.ToolStripButton();
            this.propertiesButton = new System.Windows.Forms.ToolStripButton();
            this.openTileset = new System.Windows.Forms.ToolStripButton();
            this.openTilemap = new System.Windows.Forms.ToolStripButton();
            this.openSolidTileset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openPaletteEditor = new System.Windows.Forms.ToolStripButton();
            this.openGraphicEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openTemplates = new System.Windows.Forms.ToolStripButton();
            this.openPreviewer = new System.Windows.Forms.ToolStripButton();
            this.spaceAnalyzer = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelLevels = new System.Windows.Forms.Panel();
            this.toolStripToggle = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importArchitectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.arraysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicSetsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArchitectureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.arraysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLevelImagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.dumpTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetLevelMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetLayerDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetNPCDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetEventDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetExitDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetOverlapDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilemapModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSolidityModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resetPaletteSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetGraphicSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilesetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetTilemapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSolidityMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.resetAllComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clear = new System.Windows.Forms.ToolStripDropDownButton();
            this.clearLevelDataAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.clearTilesetsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTilemapsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPhysicalMapsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.unusedGraphicSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.unusedToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllComponentsAll = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllComponentsCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.levelInfo = new LAZYSHELL.ToolStripListView();
            this.helpTips = new System.Windows.Forms.ToolStripButton();
            this.baseConvertor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcMapHeader)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcZ)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpeedPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcEventORPack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapNum)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overlapType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapZ)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.panelOverlapTileset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOverlaps)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventHeight)).BeginInit();
            this.toolStrip6.SuspendLayout();
            this.panel52.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestX)).BeginInit();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitLength)).BeginInit();
            this.toolStrip5.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPaletteSetNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL1Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL2Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattlefieldNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPhysicalMapNum)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL1Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL2Num)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet4Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSetL3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet5Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet3Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet1Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet2Num)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2LeftShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2UpShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3LeftShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3UpShift)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskLowX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskLowY)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerPrioritySet)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsWidth)).BeginInit();
            this.toolStrip7.SuspendLayout();
            this.panel27.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsHeight)).BeginInit();
            this.toolStrip8.SuspendLayout();
            this.toolStripLevel.SuspendLayout();
            this.panelLevels.SuspendLayout();
            this.toolStripToggle.SuspendLayout();
            this.SuspendLayout();
            // 
            // levelNum
            // 
            this.levelNum.AutoSize = false;
            this.levelNum.ContextMenuStrip = null;
            this.levelNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNum.Hexadecimal = false;
            this.levelNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelNum.Location = new System.Drawing.Point(225, 2);
            this.levelNum.Maximum = new decimal(new int[] {
            509,
            0,
            0,
            0});
            this.levelNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.levelNum.Name = "levelNum";
            this.levelNum.Size = new System.Drawing.Size(46, 21);
            this.levelNum.Text = "0";
            this.levelNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.levelNum.ValueChanged += new System.EventHandler(this.levelNum_ValueChanged);
            // 
            // levelName
            // 
            this.levelName.AutoSize = false;
            this.levelName.DropDownHeight = 500;
            this.levelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelName.DropDownWidth = 500;
            this.levelName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.levelName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelName.IntegralHeight = false;
            this.levelName.Name = "levelName";
            this.levelName.Size = new System.Drawing.Size(214, 21);
            this.levelName.SelectedIndexChanged += new System.EventHandler(this.levelName_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.npcMapHeader);
            this.tabPage3.Controls.Add(this.openPartitions);
            this.tabPage3.Controls.Add(this.groupBox14);
            this.tabPage3.Controls.Add(this.groupBox15);
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Controls.Add(this.groupBox13);
            this.tabPage3.Controls.Add(this.groupBox12);
            this.tabPage3.Controls.Add(this.npcsBytesLeft);
            this.tabPage3.Controls.Add(this.npcObjectTree);
            this.tabPage3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(260, 640);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NPCS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // npcMapHeader
            // 
            this.npcMapHeader.Location = new System.Drawing.Point(201, 27);
            this.npcMapHeader.Maximum = new decimal(new int[] {
            119,
            0,
            0,
            0});
            this.npcMapHeader.Name = "npcMapHeader";
            this.npcMapHeader.Size = new System.Drawing.Size(51, 21);
            this.npcMapHeader.TabIndex = 4;
            this.npcMapHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcMapHeader.ValueChanged += new System.EventHandler(this.npcMapHeader_ValueChanged);
            // 
            // openPartitions
            // 
            this.openPartitions.BackColor = System.Drawing.SystemColors.Control;
            this.openPartitions.FlatAppearance.BorderSize = 0;
            this.openPartitions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openPartitions.Location = new System.Drawing.Point(132, 28);
            this.openPartitions.Name = "openPartitions";
            this.openPartitions.Size = new System.Drawing.Size(67, 20);
            this.openPartitions.TabIndex = 3;
            this.openPartitions.Text = "Partition";
            this.openPartitions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.openPartitions, "Edit NPC event...");
            this.openPartitions.UseCompatibleTextRendering = true;
            this.openPartitions.UseVisualStyleBackColor = false;
            this.openPartitions.Click += new System.EventHandler(this.openPartitions_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.npcAttributes);
            this.groupBox14.Location = new System.Drawing.Point(0, 456);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(260, 158);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Other Properties";
            // 
            // npcAttributes
            // 
            this.npcAttributes.CheckOnClick = true;
            this.npcAttributes.ColumnWidth = 122;
            this.npcAttributes.Items.AddRange(new object[] {
            "Face on trigger",
            "Can\'t enter doors",
            "{B2,b5}",
            "Sequence playback",
            "Can\'t float",
            "Can\'t walk up stairs",
            "Can\'t walk under",
            "Can\'t pass walls",
            "Can\'t jump through",
            "Can\'t pass NPCs",
            "{B3,b5}",
            "Can\'t walk through",
            "{B3,b7}",
            "Slidable along walls",
            "{B4,b1}"});
            this.npcAttributes.Location = new System.Drawing.Point(6, 20);
            this.npcAttributes.MultiColumn = true;
            this.npcAttributes.Name = "npcAttributes";
            this.npcAttributes.Size = new System.Drawing.Size(248, 132);
            this.npcAttributes.TabIndex = 0;
            this.npcAttributes.SelectedIndexChanged += new System.EventHandler(this.npcAttributes_SelectedIndexChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.npcAfterBattle);
            this.groupBox15.Location = new System.Drawing.Point(128, 307);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(132, 47);
            this.groupBox15.TabIndex = 6;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "After Battle";
            // 
            // npcAfterBattle
            // 
            this.npcAfterBattle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcAfterBattle.DropDownWidth = 350;
            this.npcAfterBattle.IntegralHeight = false;
            this.npcAfterBattle.Items.AddRange(new object[] {
            "remove permanently (from level memory)",
            "remove temporarily (return on level re-entry)",
            "do not remove at all (disable trigger)",
            "remove permanently (if ran away, can walk through while blinking)",
            "remove temporarily (if ran away, can walk through while blinking)"});
            this.npcAfterBattle.Location = new System.Drawing.Point(6, 20);
            this.npcAfterBattle.Name = "npcAfterBattle";
            this.npcAfterBattle.Size = new System.Drawing.Size(120, 21);
            this.npcAfterBattle.TabIndex = 0;
            this.npcAfterBattle.SelectedIndexChanged += new System.EventHandler(this.npcAfterBattle_SelectedIndexChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip3.CanOverflow = false;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.npcInsertObject,
            this.npcInsertInstance,
            this.npcRemoveObject,
            this.npcCopy,
            this.npcPaste,
            this.npcDuplicate,
            this.toolStripSeparator10,
            this.npcMoveUp,
            this.npcMoveDown});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(260, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // npcInsertObject
            // 
            this.npcInsertObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcInsertObject.Image = global::LAZYSHELL.Properties.Resources.npcAdd;
            this.npcInsertObject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcInsertObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcInsertObject.Name = "npcInsertObject";
            this.npcInsertObject.Size = new System.Drawing.Size(23, 22);
            this.npcInsertObject.Text = "New NPC";
            this.npcInsertObject.Click += new System.EventHandler(this.npcInsertObject_Click);
            // 
            // npcInsertInstance
            // 
            this.npcInsertInstance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcInsertInstance.Image = global::LAZYSHELL.Properties.Resources.npcClone;
            this.npcInsertInstance.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcInsertInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcInsertInstance.Name = "npcInsertInstance";
            this.npcInsertInstance.Size = new System.Drawing.Size(23, 22);
            this.npcInsertInstance.Text = "New NPC Clone";
            this.npcInsertInstance.Click += new System.EventHandler(this.npcInsertInstance_Click);
            // 
            // npcRemoveObject
            // 
            this.npcRemoveObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcRemoveObject.Image = global::LAZYSHELL.Properties.Resources.npcRemove;
            this.npcRemoveObject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcRemoveObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcRemoveObject.Name = "npcRemoveObject";
            this.npcRemoveObject.Size = new System.Drawing.Size(23, 22);
            this.npcRemoveObject.Text = "Delete NPC";
            this.npcRemoveObject.Click += new System.EventHandler(this.npcRemoveObject_Click);
            // 
            // npcCopy
            // 
            this.npcCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcCopy.Image = ((System.Drawing.Image)(resources.GetObject("npcCopy.Image")));
            this.npcCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcCopy.Name = "npcCopy";
            this.npcCopy.Size = new System.Drawing.Size(23, 22);
            this.npcCopy.Text = "Copy NPC";
            this.npcCopy.Click += new System.EventHandler(this.npcCopy_Click);
            // 
            // npcPaste
            // 
            this.npcPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcPaste.Image = ((System.Drawing.Image)(resources.GetObject("npcPaste.Image")));
            this.npcPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcPaste.Name = "npcPaste";
            this.npcPaste.Size = new System.Drawing.Size(23, 22);
            this.npcPaste.Text = "Paste NPC";
            this.npcPaste.Click += new System.EventHandler(this.npcPaste_Click);
            // 
            // npcDuplicate
            // 
            this.npcDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("npcDuplicate.Image")));
            this.npcDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcDuplicate.Name = "npcDuplicate";
            this.npcDuplicate.Size = new System.Drawing.Size(23, 22);
            this.npcDuplicate.Text = "Duplicate NPC";
            this.npcDuplicate.Click += new System.EventHandler(this.npcDuplicate_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // npcMoveUp
            // 
            this.npcMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcMoveUp.Image = global::LAZYSHELL.Properties.Resources.moveUp;
            this.npcMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcMoveUp.Name = "npcMoveUp";
            this.npcMoveUp.Size = new System.Drawing.Size(23, 22);
            this.npcMoveUp.Text = "Move NPC Up";
            this.npcMoveUp.Click += new System.EventHandler(this.npcMoveUp_Click);
            // 
            // npcMoveDown
            // 
            this.npcMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.npcMoveDown.Image = global::LAZYSHELL.Properties.Resources.moveDown;
            this.npcMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.npcMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.npcMoveDown.Name = "npcMoveDown";
            this.npcMoveDown.Size = new System.Drawing.Size(23, 22);
            this.npcMoveDown.Text = "Move NPC Down";
            this.npcMoveDown.Click += new System.EventHandler(this.npcMoveDown_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.npcFace);
            this.groupBox13.Controls.Add(this.npcX);
            this.groupBox13.Controls.Add(this.label29);
            this.groupBox13.Controls.Add(this.npcY);
            this.groupBox13.Controls.Add(this.npcZ_half);
            this.groupBox13.Controls.Add(this.label30);
            this.groupBox13.Controls.Add(this.npcZ);
            this.groupBox13.Controls.Add(this.label56);
            this.groupBox13.Location = new System.Drawing.Point(128, 360);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(132, 90);
            this.groupBox13.TabIndex = 7;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Isometric Coordinates";
            // 
            // npcFace
            // 
            this.npcFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcFace.Items.AddRange(new object[] {
            "E",
            "SE",
            "S",
            "SW",
            "W",
            "NW",
            "N",
            "NE"});
            this.npcFace.Location = new System.Drawing.Point(35, 62);
            this.npcFace.Name = "npcFace";
            this.npcFace.Size = new System.Drawing.Size(45, 21);
            this.npcFace.TabIndex = 7;
            this.npcFace.SelectedIndexChanged += new System.EventHandler(this.npcRadialPosition_SelectedIndexChanged);
            // 
            // npcX
            // 
            this.npcX.Location = new System.Drawing.Point(35, 20);
            this.npcX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.npcX.Name = "npcX";
            this.npcX.Size = new System.Drawing.Size(45, 21);
            this.npcX.TabIndex = 1;
            this.npcX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcX.ValueChanged += new System.EventHandler(this.npcXCoord_ValueChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 22);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(23, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "X,Y";
            // 
            // npcY
            // 
            this.npcY.Location = new System.Drawing.Point(80, 20);
            this.npcY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.npcY.Name = "npcY";
            this.npcY.Size = new System.Drawing.Size(45, 21);
            this.npcY.TabIndex = 2;
            this.npcY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcY.ValueChanged += new System.EventHandler(this.npcYCoord_ValueChanged);
            // 
            // npcZ_half
            // 
            this.npcZ_half.Appearance = System.Windows.Forms.Appearance.Button;
            this.npcZ_half.BackColor = System.Drawing.SystemColors.Control;
            this.npcZ_half.FlatAppearance.BorderSize = 0;
            this.npcZ_half.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcZ_half.ForeColor = System.Drawing.Color.Gray;
            this.npcZ_half.Location = new System.Drawing.Point(81, 41);
            this.npcZ_half.Name = "npcZ_half";
            this.npcZ_half.Size = new System.Drawing.Size(43, 20);
            this.npcZ_half.TabIndex = 5;
            this.npcZ_half.Text = "+1/2";
            this.npcZ_half.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.npcZ_half.UseCompatibleTextRendering = true;
            this.npcZ_half.UseVisualStyleBackColor = false;
            this.npcZ_half.CheckedChanged += new System.EventHandler(this.npcsZCoordPlusHalf_CheckedChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 65);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(13, 13);
            this.label30.TabIndex = 6;
            this.label30.Text = "F";
            // 
            // npcZ
            // 
            this.npcZ.Location = new System.Drawing.Point(35, 41);
            this.npcZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.npcZ.Name = "npcZ";
            this.npcZ.Size = new System.Drawing.Size(45, 21);
            this.npcZ.TabIndex = 4;
            this.npcZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcZ.ValueChanged += new System.EventHandler(this.npcZCoord_ValueChanged);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 44);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(13, 13);
            this.label56.TabIndex = 3;
            this.label56.Text = "Z";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.findNPCNum);
            this.groupBox12.Controls.Add(this.npcVisible);
            this.groupBox12.Controls.Add(this.npcID);
            this.groupBox12.Controls.Add(this.npcEngageTrigger);
            this.groupBox12.Controls.Add(this.npcMovement);
            this.groupBox12.Controls.Add(this.label4);
            this.groupBox12.Controls.Add(this.npcSpeedPlus);
            this.groupBox12.Controls.Add(this.label3);
            this.groupBox12.Controls.Add(this.npcPropertyA);
            this.groupBox12.Controls.Add(this.npcEventORPack);
            this.groupBox12.Controls.Add(this.label104);
            this.groupBox12.Controls.Add(this.npcPropertyB);
            this.groupBox12.Controls.Add(this.npcEngageType);
            this.groupBox12.Controls.Add(this.label31);
            this.groupBox12.Controls.Add(this.label54);
            this.groupBox12.Controls.Add(this.npcPropertyC);
            this.groupBox12.Controls.Add(this.npcGotoA);
            this.groupBox12.Controls.Add(this.label116);
            this.groupBox12.Controls.Add(this.npcGotoB);
            this.groupBox12.Location = new System.Drawing.Point(128, 56);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(132, 245);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "NPC Properties";
            // 
            // findNPCNum
            // 
            this.findNPCNum.BackColor = System.Drawing.SystemColors.Control;
            this.findNPCNum.FlatAppearance.BorderSize = 0;
            this.findNPCNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findNPCNum.Location = new System.Drawing.Point(6, 68);
            this.findNPCNum.Name = "findNPCNum";
            this.findNPCNum.Size = new System.Drawing.Size(65, 21);
            this.findNPCNum.TabIndex = 4;
            this.findNPCNum.Text = "NPC #";
            this.findNPCNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.findNPCNum, "Edit NPC event...");
            this.findNPCNum.UseCompatibleTextRendering = true;
            this.findNPCNum.UseVisualStyleBackColor = false;
            this.findNPCNum.Click += new System.EventHandler(this.findNPCNum_Click);
            // 
            // npcVisible
            // 
            this.npcVisible.Appearance = System.Windows.Forms.Appearance.Button;
            this.npcVisible.BackColor = System.Drawing.SystemColors.Control;
            this.npcVisible.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcVisible.ForeColor = System.Drawing.Color.Gray;
            this.npcVisible.Location = new System.Drawing.Point(6, 220);
            this.npcVisible.Name = "npcVisible";
            this.npcVisible.Size = new System.Drawing.Size(120, 18);
            this.npcVisible.TabIndex = 18;
            this.npcVisible.Text = "SHOW NPC";
            this.npcVisible.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.npcVisible.UseCompatibleTextRendering = true;
            this.npcVisible.UseVisualStyleBackColor = false;
            this.npcVisible.CheckedChanged += new System.EventHandler(this.npcsShowNPC_CheckedChanged);
            // 
            // npcID
            // 
            this.npcID.Location = new System.Drawing.Point(73, 67);
            this.npcID.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.npcID.Name = "npcID";
            this.npcID.Size = new System.Drawing.Size(53, 21);
            this.npcID.TabIndex = 5;
            this.npcID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcID.ValueChanged += new System.EventHandler(this.npcID_ValueChanged);
            // 
            // npcEngageTrigger
            // 
            this.npcEngageTrigger.DropDownHeight = 171;
            this.npcEngageTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcEngageTrigger.DropDownWidth = 210;
            this.npcEngageTrigger.IntegralHeight = false;
            this.npcEngageTrigger.Items.AddRange(new object[] {
            "(none)",
            "press A from any side",
            "press A from front",
            "do anything EXCEPT touch any side",
            "press A from any side / touch any side",
            "press A from front / touch from front",
            "do anything",
            "hit from below",
            "jump on",
            "jump on / hit from below",
            "touch any side",
            "touch from front",
            "do anything EXCEPT press A"});
            this.npcEngageTrigger.Location = new System.Drawing.Point(53, 41);
            this.npcEngageTrigger.Name = "npcEngageTrigger";
            this.npcEngageTrigger.Size = new System.Drawing.Size(73, 21);
            this.npcEngageTrigger.TabIndex = 3;
            this.npcEngageTrigger.SelectedIndexChanged += new System.EventHandler(this.npcEngageTrigger_SelectedIndexChanged);
            // 
            // npcMovement
            // 
            this.npcMovement.Location = new System.Drawing.Point(73, 109);
            this.npcMovement.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.npcMovement.Name = "npcMovement";
            this.npcMovement.Size = new System.Drawing.Size(53, 21);
            this.npcMovement.TabIndex = 9;
            this.npcMovement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcMovement.ValueChanged += new System.EventHandler(this.npcMovement_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Trigger";
            // 
            // npcSpeedPlus
            // 
            this.npcSpeedPlus.Location = new System.Drawing.Point(73, 130);
            this.npcSpeedPlus.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.npcSpeedPlus.Name = "npcSpeedPlus";
            this.npcSpeedPlus.Size = new System.Drawing.Size(53, 21);
            this.npcSpeedPlus.TabIndex = 11;
            this.npcSpeedPlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcSpeedPlus.ValueChanged += new System.EventHandler(this.npcSpeedPlus_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type";
            // 
            // npcPropertyA
            // 
            this.npcPropertyA.Location = new System.Drawing.Point(73, 151);
            this.npcPropertyA.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.npcPropertyA.Name = "npcPropertyA";
            this.npcPropertyA.Size = new System.Drawing.Size(53, 21);
            this.npcPropertyA.TabIndex = 13;
            this.npcPropertyA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcPropertyA.ValueChanged += new System.EventHandler(this.npcPropertyA_ValueChanged);
            // 
            // npcEventORPack
            // 
            this.npcEventORPack.Location = new System.Drawing.Point(73, 88);
            this.npcEventORPack.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.npcEventORPack.Name = "npcEventORPack";
            this.npcEventORPack.Size = new System.Drawing.Size(53, 21);
            this.npcEventORPack.TabIndex = 7;
            this.npcEventORPack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcEventORPack.ValueChanged += new System.EventHandler(this.npcEventORPack_ValueChanged);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(6, 155);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(19, 13);
            this.label104.TabIndex = 12;
            this.label104.Text = "...";
            // 
            // npcPropertyB
            // 
            this.npcPropertyB.Location = new System.Drawing.Point(73, 172);
            this.npcPropertyB.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.npcPropertyB.Name = "npcPropertyB";
            this.npcPropertyB.Size = new System.Drawing.Size(53, 21);
            this.npcPropertyB.TabIndex = 15;
            this.npcPropertyB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcPropertyB.ValueChanged += new System.EventHandler(this.npcPropertyB_ValueChanged);
            // 
            // npcEngageType
            // 
            this.npcEngageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.npcEngageType.Items.AddRange(new object[] {
            "Object",
            "Treasure",
            "Battle"});
            this.npcEngageType.Location = new System.Drawing.Point(53, 20);
            this.npcEngageType.Name = "npcEngageType";
            this.npcEngageType.Size = new System.Drawing.Size(73, 21);
            this.npcEngageType.TabIndex = 1;
            this.npcEngageType.SelectedIndexChanged += new System.EventHandler(this.npcEngageType_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 175);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(19, 13);
            this.label31.TabIndex = 14;
            this.label31.Text = "...";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 135);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(48, 13);
            this.label54.TabIndex = 10;
            this.label54.Text = "Speed +";
            // 
            // npcPropertyC
            // 
            this.npcPropertyC.Location = new System.Drawing.Point(73, 193);
            this.npcPropertyC.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.npcPropertyC.Name = "npcPropertyC";
            this.npcPropertyC.Size = new System.Drawing.Size(53, 21);
            this.npcPropertyC.TabIndex = 17;
            this.npcPropertyC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.npcPropertyC.ValueChanged += new System.EventHandler(this.npcPropertyC_ValueChanged);
            // 
            // npcGotoA
            // 
            this.npcGotoA.BackColor = System.Drawing.SystemColors.Control;
            this.npcGotoA.FlatAppearance.BorderSize = 0;
            this.npcGotoA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcGotoA.Location = new System.Drawing.Point(6, 89);
            this.npcGotoA.Name = "npcGotoA";
            this.npcGotoA.Size = new System.Drawing.Size(65, 21);
            this.npcGotoA.TabIndex = 6;
            this.npcGotoA.Text = "Event #";
            this.npcGotoA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.npcGotoA, "Edit NPC event...");
            this.npcGotoA.UseCompatibleTextRendering = true;
            this.npcGotoA.UseVisualStyleBackColor = false;
            this.npcGotoA.Click += new System.EventHandler(this.buttonGotoA_Click);
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(6, 197);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(56, 13);
            this.label116.TabIndex = 16;
            this.label116.Text = "Action #+";
            // 
            // npcGotoB
            // 
            this.npcGotoB.BackColor = System.Drawing.SystemColors.Control;
            this.npcGotoB.FlatAppearance.BorderSize = 0;
            this.npcGotoB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcGotoB.Location = new System.Drawing.Point(6, 110);
            this.npcGotoB.Name = "npcGotoB";
            this.npcGotoB.Size = new System.Drawing.Size(65, 21);
            this.npcGotoB.TabIndex = 8;
            this.npcGotoB.Text = "Action #";
            this.npcGotoB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.npcGotoB, "Edit NPC action...");
            this.npcGotoB.UseCompatibleTextRendering = true;
            this.npcGotoB.UseVisualStyleBackColor = false;
            this.npcGotoB.Click += new System.EventHandler(this.buttonGotoB_Click);
            // 
            // npcsBytesLeft
            // 
            this.npcsBytesLeft.BackColor = System.Drawing.SystemColors.Control;
            this.npcsBytesLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.npcsBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.npcsBytesLeft.Location = new System.Drawing.Point(0, 28);
            this.npcsBytesLeft.Name = "npcsBytesLeft";
            this.npcsBytesLeft.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.npcsBytesLeft.Size = new System.Drawing.Size(126, 21);
            this.npcsBytesLeft.TabIndex = 1;
            this.npcsBytesLeft.Text = "bytes left";
            this.npcsBytesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // npcObjectTree
            // 
            this.npcObjectTree.HideSelection = false;
            this.npcObjectTree.HotTracking = true;
            this.npcObjectTree.Location = new System.Drawing.Point(0, 51);
            this.npcObjectTree.Name = "npcObjectTree";
            this.npcObjectTree.ShowPlusMinus = false;
            this.npcObjectTree.ShowRootLines = false;
            this.npcObjectTree.Size = new System.Drawing.Size(126, 399);
            this.npcObjectTree.TabIndex = 2;
            this.npcObjectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.npcObjectTree_AfterSelect);
            // 
            // mapNum
            // 
            this.mapNum.Location = new System.Drawing.Point(50, 7);
            this.mapNum.Maximum = new decimal(new int[] {
            155,
            0,
            0,
            0});
            this.mapNum.Name = "mapNum";
            this.mapNum.Size = new System.Drawing.Size(49, 21);
            this.mapNum.TabIndex = 1;
            this.mapNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapNum.ValueChanged += new System.EventHandler(this.mapNum_ValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 9);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(38, 13);
            this.label33.TabIndex = 0;
            this.label33.Text = "Map #";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 106);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "GFX Set 5";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "GFX Set 4";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "GFX Set 3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "GFX Set 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "GFX Set 1";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Controls.Add(this.panelOverlapTileset);
            this.tabPage5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(260, 640);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "OVERLAP";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox19);
            this.panel1.Controls.Add(this.overlapFieldTree);
            this.panel1.Controls.Add(this.overlapUnknownBits);
            this.panel1.Controls.Add(this.toolStrip4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 220);
            this.panel1.TabIndex = 0;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.overlapCoordZPlusHalf);
            this.groupBox19.Controls.Add(this.label109);
            this.groupBox19.Controls.Add(this.overlapType);
            this.groupBox19.Controls.Add(this.overlapX);
            this.groupBox19.Controls.Add(this.overlapY);
            this.groupBox19.Controls.Add(this.label103);
            this.groupBox19.Controls.Add(this.overlapZ);
            this.groupBox19.Controls.Add(this.label107);
            this.groupBox19.Location = new System.Drawing.Point(127, 28);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(133, 94);
            this.groupBox19.TabIndex = 2;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Tile Properties";
            // 
            // overlapCoordZPlusHalf
            // 
            this.overlapCoordZPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.overlapCoordZPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.overlapCoordZPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overlapCoordZPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.overlapCoordZPlusHalf.Location = new System.Drawing.Point(83, 67);
            this.overlapCoordZPlusHalf.Name = "overlapCoordZPlusHalf";
            this.overlapCoordZPlusHalf.Size = new System.Drawing.Size(43, 19);
            this.overlapCoordZPlusHalf.TabIndex = 7;
            this.overlapCoordZPlusHalf.Text = "+1/2";
            this.overlapCoordZPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.overlapCoordZPlusHalf.UseCompatibleTextRendering = true;
            this.overlapCoordZPlusHalf.UseVisualStyleBackColor = false;
            this.overlapCoordZPlusHalf.CheckedChanged += new System.EventHandler(this.overlapCoordZPlusHalf_CheckedChanged);
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(6, 22);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(34, 13);
            this.label109.TabIndex = 0;
            this.label109.Text = "Tile #";
            // 
            // overlapType
            // 
            this.overlapType.Location = new System.Drawing.Point(46, 20);
            this.overlapType.Maximum = new decimal(new int[] {
            103,
            0,
            0,
            0});
            this.overlapType.Name = "overlapType";
            this.overlapType.Size = new System.Drawing.Size(81, 21);
            this.overlapType.TabIndex = 1;
            this.overlapType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.overlapType.ValueChanged += new System.EventHandler(this.overlapType_ValueChanged);
            // 
            // overlapX
            // 
            this.overlapX.Location = new System.Drawing.Point(37, 45);
            this.overlapX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.overlapX.Name = "overlapX";
            this.overlapX.Size = new System.Drawing.Size(45, 21);
            this.overlapX.TabIndex = 3;
            this.overlapX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.overlapX.ValueChanged += new System.EventHandler(this.overlapCoordX_ValueChanged);
            // 
            // overlapY
            // 
            this.overlapY.Location = new System.Drawing.Point(82, 45);
            this.overlapY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.overlapY.Name = "overlapY";
            this.overlapY.Size = new System.Drawing.Size(45, 21);
            this.overlapY.TabIndex = 4;
            this.overlapY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.overlapY.ValueChanged += new System.EventHandler(this.overlapCoordY_ValueChanged);
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(6, 47);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(23, 13);
            this.label103.TabIndex = 2;
            this.label103.Text = "X,Y";
            // 
            // overlapZ
            // 
            this.overlapZ.Location = new System.Drawing.Point(37, 66);
            this.overlapZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.overlapZ.Name = "overlapZ";
            this.overlapZ.Size = new System.Drawing.Size(45, 21);
            this.overlapZ.TabIndex = 6;
            this.overlapZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.overlapZ.ValueChanged += new System.EventHandler(this.overlapCoordZ_ValueChanged);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(6, 68);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(13, 13);
            this.label107.TabIndex = 5;
            this.label107.Text = "Z";
            // 
            // overlapFieldTree
            // 
            this.overlapFieldTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.overlapFieldTree.HideSelection = false;
            this.overlapFieldTree.HotTracking = true;
            this.overlapFieldTree.Location = new System.Drawing.Point(0, 25);
            this.overlapFieldTree.Name = "overlapFieldTree";
            this.overlapFieldTree.ShowRootLines = false;
            this.overlapFieldTree.Size = new System.Drawing.Size(125, 195);
            this.overlapFieldTree.TabIndex = 1;
            this.overlapFieldTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.overlapFieldTree_AfterSelect);
            // 
            // overlapUnknownBits
            // 
            this.overlapUnknownBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.overlapUnknownBits.CheckOnClick = true;
            this.overlapUnknownBits.ColumnWidth = 60;
            this.overlapUnknownBits.IntegralHeight = false;
            this.overlapUnknownBits.Items.AddRange(new object[] {
            "overlap tile edges",
            "{B2,b5}",
            "{B2,b6}",
            "{B2,b7}"});
            this.overlapUnknownBits.Location = new System.Drawing.Point(127, 128);
            this.overlapUnknownBits.Name = "overlapUnknownBits";
            this.overlapUnknownBits.Size = new System.Drawing.Size(133, 91);
            this.overlapUnknownBits.TabIndex = 3;
            this.overlapUnknownBits.SelectedIndexChanged += new System.EventHandler(this.overlapUnknownBits_SelectedIndexChanged);
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip4.CanOverflow = false;
            this.toolStrip4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overlapFieldInsert,
            this.overlapFieldDelete,
            this.overlapFieldCopy,
            this.overlapFieldPaste,
            this.overlapFieldDuplicate,
            this.toolStripSeparator18,
            this.overlapsBytesLeft});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(260, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // overlapFieldInsert
            // 
            this.overlapFieldInsert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlapFieldInsert.Image = global::LAZYSHELL.Properties.Resources.overlapAdd;
            this.overlapFieldInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlapFieldInsert.Name = "overlapFieldInsert";
            this.overlapFieldInsert.Size = new System.Drawing.Size(23, 22);
            this.overlapFieldInsert.Text = "Insert overlap";
            this.overlapFieldInsert.Click += new System.EventHandler(this.overlapFieldInsert_Click);
            // 
            // overlapFieldDelete
            // 
            this.overlapFieldDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlapFieldDelete.Image = global::LAZYSHELL.Properties.Resources.overlapRemove;
            this.overlapFieldDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlapFieldDelete.Name = "overlapFieldDelete";
            this.overlapFieldDelete.Size = new System.Drawing.Size(23, 22);
            this.overlapFieldDelete.Text = "Delete overlap";
            this.overlapFieldDelete.Click += new System.EventHandler(this.overlapFieldDelete_Click);
            // 
            // overlapFieldCopy
            // 
            this.overlapFieldCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlapFieldCopy.Image = ((System.Drawing.Image)(resources.GetObject("overlapFieldCopy.Image")));
            this.overlapFieldCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.overlapFieldCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlapFieldCopy.Name = "overlapFieldCopy";
            this.overlapFieldCopy.Size = new System.Drawing.Size(23, 22);
            this.overlapFieldCopy.Text = "Copy Overlap";
            this.overlapFieldCopy.Click += new System.EventHandler(this.overlapFieldCopy_Click);
            // 
            // overlapFieldPaste
            // 
            this.overlapFieldPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlapFieldPaste.Image = ((System.Drawing.Image)(resources.GetObject("overlapFieldPaste.Image")));
            this.overlapFieldPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.overlapFieldPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlapFieldPaste.Name = "overlapFieldPaste";
            this.overlapFieldPaste.Size = new System.Drawing.Size(23, 22);
            this.overlapFieldPaste.Text = "Paste Overlap";
            this.overlapFieldPaste.Click += new System.EventHandler(this.overlapFieldPaste_Click);
            // 
            // overlapFieldDuplicate
            // 
            this.overlapFieldDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.overlapFieldDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("overlapFieldDuplicate.Image")));
            this.overlapFieldDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.overlapFieldDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.overlapFieldDuplicate.Name = "overlapFieldDuplicate";
            this.overlapFieldDuplicate.Size = new System.Drawing.Size(23, 22);
            this.overlapFieldDuplicate.Text = "Duplicate Overlap";
            this.overlapFieldDuplicate.Click += new System.EventHandler(this.overlapFieldDuplicate_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // overlapsBytesLeft
            // 
            this.overlapsBytesLeft.Name = "overlapsBytesLeft";
            this.overlapsBytesLeft.Size = new System.Drawing.Size(52, 22);
            this.overlapsBytesLeft.Text = "bytes left";
            // 
            // panelOverlapTileset
            // 
            this.panelOverlapTileset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOverlapTileset.Controls.Add(this.pictureBoxOverlaps);
            this.panelOverlapTileset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOverlapTileset.Enabled = false;
            this.panelOverlapTileset.Location = new System.Drawing.Point(0, 220);
            this.panelOverlapTileset.Name = "panelOverlapTileset";
            this.panelOverlapTileset.Size = new System.Drawing.Size(260, 420);
            this.panelOverlapTileset.TabIndex = 1;
            this.panelOverlapTileset.Visible = false;
            // 
            // pictureBoxOverlaps
            // 
            this.pictureBoxOverlaps.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxOverlaps.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxOverlaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxOverlaps.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxOverlaps.Name = "pictureBoxOverlaps";
            this.pictureBoxOverlaps.Size = new System.Drawing.Size(256, 416);
            this.pictureBoxOverlaps.TabIndex = 0;
            this.pictureBoxOverlaps.TabStop = false;
            this.pictureBoxOverlaps.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxOverlaps_Paint);
            this.pictureBoxOverlaps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOverlaps_MouseDown);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Controls.Add(this.panel52);
            this.tabPage4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(260, 640);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FIELD";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox18);
            this.panel2.Controls.Add(this.eventsList);
            this.panel2.Controls.Add(this.toolStrip6);
            this.panel2.Controls.Add(this.label63);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 362);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 278);
            this.panel2.TabIndex = 1;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.eventFace);
            this.groupBox18.Controls.Add(this.eventsWidthYPlusHalf);
            this.groupBox18.Controls.Add(this.eventGotoA);
            this.groupBox18.Controls.Add(this.eventsWidthXPlusHalf);
            this.groupBox18.Controls.Add(this.eventLength);
            this.groupBox18.Controls.Add(this.eventY);
            this.groupBox18.Controls.Add(this.label127);
            this.groupBox18.Controls.Add(this.label133);
            this.groupBox18.Controls.Add(this.eventX);
            this.groupBox18.Controls.Add(this.eventZ);
            this.groupBox18.Controls.Add(this.label131);
            this.groupBox18.Controls.Add(this.eventEvent);
            this.groupBox18.Controls.Add(this.eventHeight);
            this.groupBox18.Location = new System.Drawing.Point(127, 47);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(133, 167);
            this.groupBox18.TabIndex = 3;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Event Properties";
            // 
            // eventFace
            // 
            this.eventFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventFace.Items.AddRange(new object[] {
            "SE",
            "SW"});
            this.eventFace.Location = new System.Drawing.Point(82, 66);
            this.eventFace.Name = "eventFace";
            this.eventFace.Size = new System.Drawing.Size(45, 21);
            this.eventFace.TabIndex = 12;
            this.eventFace.SelectedIndexChanged += new System.EventHandler(this.eventsFieldRadialPosition_SelectedIndexChanged);
            // 
            // eventsWidthYPlusHalf
            // 
            this.eventsWidthYPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.eventsWidthYPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.eventsWidthYPlusHalf.FlatAppearance.BorderSize = 0;
            this.eventsWidthYPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsWidthYPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.eventsWidthYPlusHalf.Location = new System.Drawing.Point(6, 139);
            this.eventsWidthYPlusHalf.Name = "eventsWidthYPlusHalf";
            this.eventsWidthYPlusHalf.Size = new System.Drawing.Size(121, 21);
            this.eventsWidthYPlusHalf.TabIndex = 14;
            this.eventsWidthYPlusHalf.Text = "NE/SW edge active";
            this.eventsWidthYPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.eventsWidthYPlusHalf.UseCompatibleTextRendering = true;
            this.eventsWidthYPlusHalf.UseVisualStyleBackColor = false;
            this.eventsWidthYPlusHalf.CheckedChanged += new System.EventHandler(this.eventsWidthYPlusHalf_CheckedChanged);
            // 
            // eventGotoA
            // 
            this.eventGotoA.BackColor = System.Drawing.SystemColors.Control;
            this.eventGotoA.FlatAppearance.BorderSize = 0;
            this.eventGotoA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventGotoA.Location = new System.Drawing.Point(6, 20);
            this.eventGotoA.Name = "eventGotoA";
            this.eventGotoA.Size = new System.Drawing.Size(58, 21);
            this.eventGotoA.TabIndex = 0;
            this.eventGotoA.Text = "Event #";
            this.eventGotoA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.eventGotoA, "Edit event field event...");
            this.eventGotoA.UseCompatibleTextRendering = true;
            this.eventGotoA.UseVisualStyleBackColor = false;
            this.eventGotoA.Click += new System.EventHandler(this.buttonGotoD_Click);
            // 
            // eventsWidthXPlusHalf
            // 
            this.eventsWidthXPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.eventsWidthXPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.eventsWidthXPlusHalf.FlatAppearance.BorderSize = 0;
            this.eventsWidthXPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsWidthXPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.eventsWidthXPlusHalf.Location = new System.Drawing.Point(6, 114);
            this.eventsWidthXPlusHalf.Name = "eventsWidthXPlusHalf";
            this.eventsWidthXPlusHalf.Size = new System.Drawing.Size(121, 21);
            this.eventsWidthXPlusHalf.TabIndex = 13;
            this.eventsWidthXPlusHalf.Text = "NW/SE edge active";
            this.eventsWidthXPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.eventsWidthXPlusHalf.UseCompatibleTextRendering = true;
            this.eventsWidthXPlusHalf.UseVisualStyleBackColor = false;
            this.eventsWidthXPlusHalf.CheckedChanged += new System.EventHandler(this.eventsWidthXPlusHalf_CheckedChanged);
            // 
            // eventLength
            // 
            this.eventLength.Location = new System.Drawing.Point(37, 87);
            this.eventLength.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.eventLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eventLength.Name = "eventLength";
            this.eventLength.Size = new System.Drawing.Size(45, 21);
            this.eventLength.TabIndex = 8;
            this.eventLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eventLength.ValueChanged += new System.EventHandler(this.eventsFieldLength_ValueChanged);
            // 
            // eventY
            // 
            this.eventY.Location = new System.Drawing.Point(82, 45);
            this.eventY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.eventY.Name = "eventY";
            this.eventY.Size = new System.Drawing.Size(45, 21);
            this.eventY.TabIndex = 4;
            this.eventY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventY.ValueChanged += new System.EventHandler(this.eventsFieldYCoord_ValueChanged);
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(6, 49);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(23, 13);
            this.label127.TabIndex = 2;
            this.label127.Text = "X,Y";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(6, 70);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(23, 13);
            this.label133.TabIndex = 5;
            this.label133.Text = "Z,F";
            // 
            // eventX
            // 
            this.eventX.Location = new System.Drawing.Point(37, 45);
            this.eventX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.eventX.Name = "eventX";
            this.eventX.Size = new System.Drawing.Size(45, 21);
            this.eventX.TabIndex = 3;
            this.eventX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventX.ValueChanged += new System.EventHandler(this.eventsFieldXCoord_ValueChanged);
            // 
            // eventZ
            // 
            this.eventZ.Location = new System.Drawing.Point(37, 66);
            this.eventZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.eventZ.Name = "eventZ";
            this.eventZ.Size = new System.Drawing.Size(45, 21);
            this.eventZ.TabIndex = 6;
            this.eventZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventZ.ValueChanged += new System.EventHandler(this.eventsFieldZCoord_ValueChanged);
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(6, 90);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(23, 13);
            this.label131.TabIndex = 7;
            this.label131.Text = "L/H";
            // 
            // eventEvent
            // 
            this.eventEvent.Location = new System.Drawing.Point(66, 20);
            this.eventEvent.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.eventEvent.Name = "eventEvent";
            this.eventEvent.Size = new System.Drawing.Size(61, 21);
            this.eventEvent.TabIndex = 1;
            this.eventEvent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventEvent.ValueChanged += new System.EventHandler(this.eventsRunEvent_ValueChanged);
            // 
            // eventHeight
            // 
            this.eventHeight.Location = new System.Drawing.Point(82, 87);
            this.eventHeight.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.eventHeight.Name = "eventHeight";
            this.eventHeight.Size = new System.Drawing.Size(45, 21);
            this.eventHeight.TabIndex = 10;
            this.eventHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventHeight.ValueChanged += new System.EventHandler(this.eventsFieldHeight_ValueChanged);
            // 
            // eventsList
            // 
            this.eventsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.eventsList.HideSelection = false;
            this.eventsList.HotTracking = true;
            this.eventsList.Location = new System.Drawing.Point(0, 44);
            this.eventsList.Name = "eventsList";
            this.eventsList.ShowRootLines = false;
            this.eventsList.Size = new System.Drawing.Size(125, 234);
            this.eventsList.TabIndex = 2;
            this.eventsList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.eventsFieldTree_AfterSelect);
            // 
            // toolStrip6
            // 
            this.toolStrip6.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip6.CanOverflow = false;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eventsInsertField,
            this.eventsDeleteField,
            this.eventsCopyField,
            this.eventsPasteField,
            this.eventsDuplicateField,
            this.toolStripSeparator20,
            this.eventsBytesLeft});
            this.toolStrip6.Location = new System.Drawing.Point(0, 19);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip6.Size = new System.Drawing.Size(260, 25);
            this.toolStrip6.TabIndex = 1;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // eventsInsertField
            // 
            this.eventsInsertField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsInsertField.Image = global::LAZYSHELL.Properties.Resources.eventAdd;
            this.eventsInsertField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsInsertField.Name = "eventsInsertField";
            this.eventsInsertField.Size = new System.Drawing.Size(23, 22);
            this.eventsInsertField.Text = "New Event";
            this.eventsInsertField.Click += new System.EventHandler(this.eventsInsertField_Click);
            // 
            // eventsDeleteField
            // 
            this.eventsDeleteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsDeleteField.Image = global::LAZYSHELL.Properties.Resources.eventRemove;
            this.eventsDeleteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsDeleteField.Name = "eventsDeleteField";
            this.eventsDeleteField.Size = new System.Drawing.Size(23, 22);
            this.eventsDeleteField.Text = "Delete Event";
            this.eventsDeleteField.Click += new System.EventHandler(this.eventsDeleteField_Click);
            // 
            // eventsCopyField
            // 
            this.eventsCopyField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsCopyField.Image = ((System.Drawing.Image)(resources.GetObject("eventsCopyField.Image")));
            this.eventsCopyField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsCopyField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsCopyField.Name = "eventsCopyField";
            this.eventsCopyField.Size = new System.Drawing.Size(23, 22);
            this.eventsCopyField.Text = "Copy Event";
            this.eventsCopyField.Click += new System.EventHandler(this.eventsCopyField_Click);
            // 
            // eventsPasteField
            // 
            this.eventsPasteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsPasteField.Image = ((System.Drawing.Image)(resources.GetObject("eventsPasteField.Image")));
            this.eventsPasteField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsPasteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsPasteField.Name = "eventsPasteField";
            this.eventsPasteField.Size = new System.Drawing.Size(23, 22);
            this.eventsPasteField.Text = "Paste Event";
            this.eventsPasteField.Click += new System.EventHandler(this.eventsPasteField_Click);
            // 
            // eventsDuplicateField
            // 
            this.eventsDuplicateField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eventsDuplicateField.Image = ((System.Drawing.Image)(resources.GetObject("eventsDuplicateField.Image")));
            this.eventsDuplicateField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.eventsDuplicateField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eventsDuplicateField.Name = "eventsDuplicateField";
            this.eventsDuplicateField.Size = new System.Drawing.Size(23, 22);
            this.eventsDuplicateField.Text = "Duplicate Event";
            this.eventsDuplicateField.Click += new System.EventHandler(this.eventsDuplicateField_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
            // 
            // eventsBytesLeft
            // 
            this.eventsBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventsBytesLeft.Name = "eventsBytesLeft";
            this.eventsBytesLeft.Size = new System.Drawing.Size(52, 22);
            this.eventsBytesLeft.Text = "bytes left";
            // 
            // label63
            // 
            this.label63.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label63.Dock = System.Windows.Forms.DockStyle.Top;
            this.label63.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(0, 0);
            this.label63.Name = "label63";
            this.label63.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label63.Size = new System.Drawing.Size(260, 19);
            this.label63.TabIndex = 0;
            this.label63.Text = "EVENT FIELDS";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel52
            // 
            this.panel52.Controls.Add(this.exitsFieldTree);
            this.panel52.Controls.Add(this.groupBox16);
            this.panel52.Controls.Add(this.groupBox17);
            this.panel52.Controls.Add(this.panel68);
            this.panel52.Controls.Add(this.toolStrip5);
            this.panel52.Controls.Add(this.label61);
            this.panel52.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel52.Location = new System.Drawing.Point(0, 0);
            this.panel52.Name = "panel52";
            this.panel52.Size = new System.Drawing.Size(260, 362);
            this.panel52.TabIndex = 0;
            // 
            // exitsFieldTree
            // 
            this.exitsFieldTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.exitsFieldTree.HideSelection = false;
            this.exitsFieldTree.HotTracking = true;
            this.exitsFieldTree.Location = new System.Drawing.Point(0, 44);
            this.exitsFieldTree.Name = "exitsFieldTree";
            this.exitsFieldTree.ShowRootLines = false;
            this.exitsFieldTree.Size = new System.Drawing.Size(125, 318);
            this.exitsFieldTree.TabIndex = 2;
            this.exitsFieldTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.exitsFieldTree_AfterSelect);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.exitDestFace);
            this.groupBox16.Controls.Add(this.exitType);
            this.groupBox16.Controls.Add(this.label37);
            this.groupBox16.Controls.Add(this.marioZCoordPlusHalf);
            this.groupBox16.Controls.Add(this.exitDest);
            this.groupBox16.Controls.Add(this.label59);
            this.groupBox16.Controls.Add(this.exitDestY);
            this.groupBox16.Controls.Add(this.exitsShowMessage);
            this.groupBox16.Controls.Add(this.label124);
            this.groupBox16.Controls.Add(this.label122);
            this.groupBox16.Controls.Add(this.exitDestZ);
            this.groupBox16.Controls.Add(this.exitDestX);
            this.groupBox16.Location = new System.Drawing.Point(127, 195);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(133, 165);
            this.groupBox16.TabIndex = 3;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Destination Properties";
            // 
            // exitDestFace
            // 
            this.exitDestFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitDestFace.Items.AddRange(new object[] {
            "E",
            "SE",
            "S",
            "SW",
            "W",
            "NW",
            "N",
            "NE"});
            this.exitDestFace.Location = new System.Drawing.Point(37, 138);
            this.exitDestFace.Name = "exitDestFace";
            this.exitDestFace.Size = new System.Drawing.Size(45, 21);
            this.exitDestFace.TabIndex = 7;
            this.exitDestFace.SelectedIndexChanged += new System.EventHandler(this.exitsMarioRadialPosition_SelectedIndexChanged);
            // 
            // exitType
            // 
            this.exitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitType.DropDownWidth = 80;
            this.exitType.Items.AddRange(new object[] {
            "Level",
            "Location"});
            this.exitType.Location = new System.Drawing.Point(43, 20);
            this.exitType.Name = "exitType";
            this.exitType.Size = new System.Drawing.Size(84, 21);
            this.exitType.TabIndex = 3;
            this.exitType.SelectedIndexChanged += new System.EventHandler(this.exitsType_SelectedIndexChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 23);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(31, 13);
            this.label37.TabIndex = 2;
            this.label37.Text = "Type";
            // 
            // marioZCoordPlusHalf
            // 
            this.marioZCoordPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.marioZCoordPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.marioZCoordPlusHalf.FlatAppearance.BorderSize = 0;
            this.marioZCoordPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marioZCoordPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.marioZCoordPlusHalf.Location = new System.Drawing.Point(83, 118);
            this.marioZCoordPlusHalf.Name = "marioZCoordPlusHalf";
            this.marioZCoordPlusHalf.Size = new System.Drawing.Size(43, 19);
            this.marioZCoordPlusHalf.TabIndex = 5;
            this.marioZCoordPlusHalf.Text = "+1/2";
            this.marioZCoordPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.marioZCoordPlusHalf.UseCompatibleTextRendering = true;
            this.marioZCoordPlusHalf.UseVisualStyleBackColor = false;
            this.marioZCoordPlusHalf.CheckedChanged += new System.EventHandler(this.marioZCoordPlusHalf_CheckedChanged);
            // 
            // exitDest
            // 
            this.exitDest.DropDownHeight = 431;
            this.exitDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitDest.DropDownWidth = 490;
            this.exitDest.IntegralHeight = false;
            this.exitDest.Items.AddRange(new object[] {
            ""});
            this.exitDest.Location = new System.Drawing.Point(6, 44);
            this.exitDest.Name = "exitDest";
            this.exitDest.Size = new System.Drawing.Size(121, 21);
            this.exitDest.TabIndex = 0;
            this.exitDest.SelectedIndexChanged += new System.EventHandler(this.exitsDestination_SelectedIndexChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 98);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(23, 13);
            this.label59.TabIndex = 0;
            this.label59.Text = "X,Y";
            // 
            // exitDestY
            // 
            this.exitDestY.Location = new System.Drawing.Point(82, 96);
            this.exitDestY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.exitDestY.Name = "exitDestY";
            this.exitDestY.Size = new System.Drawing.Size(45, 21);
            this.exitDestY.TabIndex = 2;
            this.exitDestY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitDestY.ValueChanged += new System.EventHandler(this.exitsMarioYCoord_ValueChanged);
            // 
            // exitsShowMessage
            // 
            this.exitsShowMessage.Appearance = System.Windows.Forms.Appearance.Button;
            this.exitsShowMessage.BackColor = System.Drawing.SystemColors.Control;
            this.exitsShowMessage.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitsShowMessage.ForeColor = System.Drawing.Color.Gray;
            this.exitsShowMessage.Location = new System.Drawing.Point(6, 69);
            this.exitsShowMessage.Name = "exitsShowMessage";
            this.exitsShowMessage.Size = new System.Drawing.Size(121, 21);
            this.exitsShowMessage.TabIndex = 1;
            this.exitsShowMessage.Text = "SHOW MESSAGE";
            this.exitsShowMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exitsShowMessage.UseCompatibleTextRendering = true;
            this.exitsShowMessage.UseVisualStyleBackColor = false;
            this.exitsShowMessage.CheckedChanged += new System.EventHandler(this.exitsShowMessage_CheckedChanged);
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(6, 141);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(13, 13);
            this.label124.TabIndex = 6;
            this.label124.Text = "F";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(6, 119);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(13, 13);
            this.label122.TabIndex = 3;
            this.label122.Text = "Z";
            // 
            // exitDestZ
            // 
            this.exitDestZ.Location = new System.Drawing.Point(37, 117);
            this.exitDestZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.exitDestZ.Name = "exitDestZ";
            this.exitDestZ.Size = new System.Drawing.Size(45, 21);
            this.exitDestZ.TabIndex = 4;
            this.exitDestZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitDestZ.ValueChanged += new System.EventHandler(this.exitsMarioZCoord_ValueChanged);
            // 
            // exitDestX
            // 
            this.exitDestX.Location = new System.Drawing.Point(37, 96);
            this.exitDestX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.exitDestX.Name = "exitDestX";
            this.exitDestX.Size = new System.Drawing.Size(45, 21);
            this.exitDestX.TabIndex = 1;
            this.exitDestX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitDestX.ValueChanged += new System.EventHandler(this.exitsMarioXCoord_ValueChanged);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.exits135LengthPlusHalf);
            this.groupBox17.Controls.Add(this.label119);
            this.groupBox17.Controls.Add(this.exitY);
            this.groupBox17.Controls.Add(this.exitFace);
            this.groupBox17.Controls.Add(this.exitX);
            this.groupBox17.Controls.Add(this.exitHeight);
            this.groupBox17.Controls.Add(this.exits45LengthPlusHalf);
            this.groupBox17.Controls.Add(this.exitZ);
            this.groupBox17.Controls.Add(this.label57);
            this.groupBox17.Controls.Add(this.label105);
            this.groupBox17.Controls.Add(this.exitLength);
            this.groupBox17.Location = new System.Drawing.Point(127, 47);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(133, 142);
            this.groupBox17.TabIndex = 4;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Exit Coordinates";
            // 
            // exits135LengthPlusHalf
            // 
            this.exits135LengthPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.exits135LengthPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.exits135LengthPlusHalf.FlatAppearance.BorderSize = 0;
            this.exits135LengthPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exits135LengthPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.exits135LengthPlusHalf.Location = new System.Drawing.Point(6, 114);
            this.exits135LengthPlusHalf.Name = "exits135LengthPlusHalf";
            this.exits135LengthPlusHalf.Size = new System.Drawing.Size(121, 21);
            this.exits135LengthPlusHalf.TabIndex = 16;
            this.exits135LengthPlusHalf.Text = "NE/SW edge active";
            this.exits135LengthPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exits135LengthPlusHalf.UseCompatibleTextRendering = true;
            this.exits135LengthPlusHalf.UseVisualStyleBackColor = false;
            this.exits135LengthPlusHalf.CheckedChanged += new System.EventHandler(this.exits135LengthPlusHalf_CheckedChanged);
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(6, 23);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(23, 13);
            this.label119.TabIndex = 4;
            this.label119.Text = "X,Y";
            // 
            // exitY
            // 
            this.exitY.Location = new System.Drawing.Point(82, 20);
            this.exitY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.exitY.Name = "exitY";
            this.exitY.Size = new System.Drawing.Size(45, 21);
            this.exitY.TabIndex = 6;
            this.exitY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitY.ValueChanged += new System.EventHandler(this.exitsY_ValueChanged);
            // 
            // exitFace
            // 
            this.exitFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exitFace.Items.AddRange(new object[] {
            "SE",
            "SW"});
            this.exitFace.Location = new System.Drawing.Point(82, 41);
            this.exitFace.Name = "exitFace";
            this.exitFace.Size = new System.Drawing.Size(45, 21);
            this.exitFace.TabIndex = 14;
            this.exitFace.SelectedIndexChanged += new System.EventHandler(this.exitsFace_SelectedIndexChanged);
            // 
            // exitX
            // 
            this.exitX.Location = new System.Drawing.Point(37, 20);
            this.exitX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.exitX.Name = "exitX";
            this.exitX.Size = new System.Drawing.Size(45, 21);
            this.exitX.TabIndex = 5;
            this.exitX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitX.ValueChanged += new System.EventHandler(this.exitsX_ValueChanged);
            // 
            // exitHeight
            // 
            this.exitHeight.Location = new System.Drawing.Point(82, 62);
            this.exitHeight.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.exitHeight.Name = "exitHeight";
            this.exitHeight.Size = new System.Drawing.Size(45, 21);
            this.exitHeight.TabIndex = 12;
            this.exitHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitHeight.ValueChanged += new System.EventHandler(this.exitsFieldHeight_ValueChanged);
            // 
            // exits45LengthPlusHalf
            // 
            this.exits45LengthPlusHalf.Appearance = System.Windows.Forms.Appearance.Button;
            this.exits45LengthPlusHalf.BackColor = System.Drawing.SystemColors.Control;
            this.exits45LengthPlusHalf.FlatAppearance.BorderSize = 0;
            this.exits45LengthPlusHalf.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exits45LengthPlusHalf.ForeColor = System.Drawing.Color.Gray;
            this.exits45LengthPlusHalf.Location = new System.Drawing.Point(6, 89);
            this.exits45LengthPlusHalf.Name = "exits45LengthPlusHalf";
            this.exits45LengthPlusHalf.Size = new System.Drawing.Size(121, 21);
            this.exits45LengthPlusHalf.TabIndex = 15;
            this.exits45LengthPlusHalf.Text = "NW/SE edge active";
            this.exits45LengthPlusHalf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exits45LengthPlusHalf.UseCompatibleTextRendering = true;
            this.exits45LengthPlusHalf.UseVisualStyleBackColor = false;
            this.exits45LengthPlusHalf.CheckedChanged += new System.EventHandler(this.exits45LengthPlusHalf_CheckedChanged);
            // 
            // exitZ
            // 
            this.exitZ.Location = new System.Drawing.Point(37, 41);
            this.exitZ.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.exitZ.Name = "exitZ";
            this.exitZ.Size = new System.Drawing.Size(45, 21);
            this.exitZ.TabIndex = 8;
            this.exitZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitZ.ValueChanged += new System.EventHandler(this.exitsZ_ValueChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 44);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(23, 13);
            this.label57.TabIndex = 7;
            this.label57.Text = "Z,F";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(6, 65);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(23, 13);
            this.label105.TabIndex = 9;
            this.label105.Text = "L/H";
            // 
            // exitLength
            // 
            this.exitLength.Location = new System.Drawing.Point(37, 62);
            this.exitLength.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.exitLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exitLength.Name = "exitLength";
            this.exitLength.Size = new System.Drawing.Size(45, 21);
            this.exitLength.TabIndex = 10;
            this.exitLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.exitLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exitLength.ValueChanged += new System.EventHandler(this.exitsFieldLength_ValueChanged);
            // 
            // panel68
            // 
            this.panel68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel68.BackgroundImage = global::LAZYSHELL.Properties.Resources._bg;
            this.panel68.Location = new System.Drawing.Point(119, 608);
            this.panel68.Name = "panel68";
            this.panel68.Size = new System.Drawing.Size(121, 35);
            this.panel68.TabIndex = 0;
            // 
            // toolStrip5
            // 
            this.toolStrip5.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip5.CanOverflow = false;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitsInsertField,
            this.exitsDeleteField,
            this.exitsCopyField,
            this.exitsPasteField,
            this.exitsDuplicateField,
            this.toolStripSeparator19,
            this.exitsBytesLeft});
            this.toolStrip5.Location = new System.Drawing.Point(0, 19);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip5.Size = new System.Drawing.Size(260, 25);
            this.toolStrip5.TabIndex = 1;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // exitsInsertField
            // 
            this.exitsInsertField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsInsertField.Image = global::LAZYSHELL.Properties.Resources.exitAdd;
            this.exitsInsertField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsInsertField.Name = "exitsInsertField";
            this.exitsInsertField.Size = new System.Drawing.Size(23, 22);
            this.exitsInsertField.Text = "New Exit";
            this.exitsInsertField.Click += new System.EventHandler(this.exitsInsertField_Click);
            // 
            // exitsDeleteField
            // 
            this.exitsDeleteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsDeleteField.Image = global::LAZYSHELL.Properties.Resources.exitRemove;
            this.exitsDeleteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsDeleteField.Name = "exitsDeleteField";
            this.exitsDeleteField.Size = new System.Drawing.Size(23, 22);
            this.exitsDeleteField.Text = "Delete Exit";
            this.exitsDeleteField.Click += new System.EventHandler(this.exitsDeleteField_Click);
            // 
            // exitsCopyField
            // 
            this.exitsCopyField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsCopyField.Image = ((System.Drawing.Image)(resources.GetObject("exitsCopyField.Image")));
            this.exitsCopyField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsCopyField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsCopyField.Name = "exitsCopyField";
            this.exitsCopyField.Size = new System.Drawing.Size(23, 22);
            this.exitsCopyField.Text = "Copy Exit";
            this.exitsCopyField.Click += new System.EventHandler(this.exitsCopyField_Click);
            // 
            // exitsPasteField
            // 
            this.exitsPasteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsPasteField.Image = ((System.Drawing.Image)(resources.GetObject("exitsPasteField.Image")));
            this.exitsPasteField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsPasteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsPasteField.Name = "exitsPasteField";
            this.exitsPasteField.Size = new System.Drawing.Size(23, 22);
            this.exitsPasteField.Text = "Paste Exit";
            this.exitsPasteField.Click += new System.EventHandler(this.exitsPasteField_Click);
            // 
            // exitsDuplicateField
            // 
            this.exitsDuplicateField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitsDuplicateField.Image = ((System.Drawing.Image)(resources.GetObject("exitsDuplicateField.Image")));
            this.exitsDuplicateField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitsDuplicateField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitsDuplicateField.Name = "exitsDuplicateField";
            this.exitsDuplicateField.Size = new System.Drawing.Size(23, 22);
            this.exitsDuplicateField.Text = "Duplicate Exit";
            this.exitsDuplicateField.Click += new System.EventHandler(this.exitsDuplicateField_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
            // 
            // exitsBytesLeft
            // 
            this.exitsBytesLeft.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.exitsBytesLeft.Name = "exitsBytesLeft";
            this.exitsBytesLeft.Size = new System.Drawing.Size(52, 22);
            this.exitsBytesLeft.Text = "bytes left";
            // 
            // label61
            // 
            this.label61.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label61.Size = new System.Drawing.Size(260, 19);
            this.label61.TabIndex = 0;
            this.label61.Text = "EXIT FIELDS";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.ItemSize = new System.Drawing.Size(44, 18);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(5, 4);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(268, 666);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.mapNum);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(260, 640);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "MAPS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mapPaletteSetName);
            this.groupBox4.Controls.Add(this.mapPaletteSetNum);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Location = new System.Drawing.Point(0, 456);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 48);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Palettes";
            // 
            // mapPaletteSetName
            // 
            this.mapPaletteSetName.DropDownHeight = 200;
            this.mapPaletteSetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapPaletteSetName.DropDownWidth = 200;
            this.mapPaletteSetName.IntegralHeight = false;
            this.mapPaletteSetName.Location = new System.Drawing.Point(127, 21);
            this.mapPaletteSetName.Name = "mapPaletteSetName";
            this.mapPaletteSetName.Size = new System.Drawing.Size(127, 21);
            this.mapPaletteSetName.TabIndex = 2;
            this.mapPaletteSetName.SelectedIndexChanged += new System.EventHandler(this.mapPaletteSetName_SelectedIndexChanged);
            // 
            // mapPaletteSetNum
            // 
            this.mapPaletteSetNum.Location = new System.Drawing.Point(76, 21);
            this.mapPaletteSetNum.Maximum = new decimal(new int[] {
            93,
            0,
            0,
            0});
            this.mapPaletteSetNum.Name = "mapPaletteSetNum";
            this.mapPaletteSetNum.Size = new System.Drawing.Size(51, 21);
            this.mapPaletteSetNum.TabIndex = 1;
            this.mapPaletteSetNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapPaletteSetNum.ValueChanged += new System.EventHandler(this.mapPaletteSetNum_ValueChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(60, 13);
            this.label46.TabIndex = 0;
            this.label46.Text = "Palette Set";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mapBattlefieldName);
            this.groupBox3.Controls.Add(this.mapSetL3Priority);
            this.groupBox3.Controls.Add(this.mapTilemapL1Num);
            this.groupBox3.Controls.Add(this.mapPhysicalMapName);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.mapTilemapL2Num);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.mapTilemapL3Name);
            this.groupBox3.Controls.Add(this.mapTilemapL2Name);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.mapTilemapL1Name);
            this.groupBox3.Controls.Add(this.mapTilemapL3Num);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.mapBattlefieldNum);
            this.groupBox3.Controls.Add(this.mapPhysicalMapNum);
            this.groupBox3.Controls.Add(this.label76);
            this.groupBox3.Location = new System.Drawing.Point(0, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 160);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tilemaps";
            // 
            // mapBattlefieldName
            // 
            this.mapBattlefieldName.DropDownHeight = 200;
            this.mapBattlefieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapBattlefieldName.IntegralHeight = false;
            this.mapBattlefieldName.Items.AddRange(new object[] {
            "Bowser\'s Keep",
            "Castle",
            "House",
            "Mushroom Kingdom",
            "Kero Sewers",
            "Mines",
            "Forest",
            "Underground",
            "Sea",
            "Sea Enclave",
            "Sunken Ship",
            "Forest",
            "Barrel Volcano",
            "Star Hill",
            "Castle",
            "Booster Tower",
            "Bowser\'s Keep",
            "Grasslands",
            "Mountains",
            "Plains",
            "Nimbus Land",
            "Nimbus Castle",
            "Count Down",
            "Smithy Factory",
            "Kero Sewers",
            "Bean Valley",
            "Land\'s End Desert",
            "Belome Temple",
            "Pipe Rooms",
            "Beanstalks",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "House",
            "Forest",
            "Mines",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Sunken Ship",
            "Forest",
            "Barrel Volcano",
            "Castle",
            "Kero Sewers",
            "Forest",
            "Booster Tower",
            "Forest",
            "Bowser\'s Keep",
            "Bowser\'s Keep",
            "Grasslands",
            "Mountains",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest",
            "Forest"});
            this.mapBattlefieldName.Location = new System.Drawing.Point(127, 130);
            this.mapBattlefieldName.Name = "mapBattlefieldName";
            this.mapBattlefieldName.Size = new System.Drawing.Size(127, 21);
            this.mapBattlefieldName.TabIndex = 15;
            this.mapBattlefieldName.SelectedIndexChanged += new System.EventHandler(this.mapBattlefieldName_SelectedIndexChanged);
            // 
            // mapSetL3Priority
            // 
            this.mapSetL3Priority.Appearance = System.Windows.Forms.Appearance.Button;
            this.mapSetL3Priority.BackColor = System.Drawing.SystemColors.Control;
            this.mapSetL3Priority.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapSetL3Priority.ForeColor = System.Drawing.Color.Gray;
            this.mapSetL3Priority.Location = new System.Drawing.Point(127, 86);
            this.mapSetL3Priority.Name = "mapSetL3Priority";
            this.mapSetL3Priority.Size = new System.Drawing.Size(127, 21);
            this.mapSetL3Priority.TabIndex = 9;
            this.mapSetL3Priority.Text = "L3 PRIORITY 1";
            this.mapSetL3Priority.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mapSetL3Priority.UseCompatibleTextRendering = true;
            this.mapSetL3Priority.UseVisualStyleBackColor = false;
            this.mapSetL3Priority.CheckedChanged += new System.EventHandler(this.mapSetL3Priority_CheckedChanged);
            // 
            // mapTilemapL1Num
            // 
            this.mapTilemapL1Num.Location = new System.Drawing.Point(76, 21);
            this.mapTilemapL1Num.Maximum = new decimal(new int[] {
            244,
            0,
            0,
            0});
            this.mapTilemapL1Num.Name = "mapTilemapL1Num";
            this.mapTilemapL1Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilemapL1Num.TabIndex = 1;
            this.mapTilemapL1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL1Num.ValueChanged += new System.EventHandler(this.mapTilemapL1Num_ValueChanged);
            // 
            // mapPhysicalMapName
            // 
            this.mapPhysicalMapName.DropDownHeight = 200;
            this.mapPhysicalMapName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapPhysicalMapName.DropDownWidth = 200;
            this.mapPhysicalMapName.IntegralHeight = false;
            this.mapPhysicalMapName.Location = new System.Drawing.Point(127, 109);
            this.mapPhysicalMapName.Name = "mapPhysicalMapName";
            this.mapPhysicalMapName.Size = new System.Drawing.Size(127, 21);
            this.mapPhysicalMapName.TabIndex = 12;
            this.mapPhysicalMapName.SelectedIndexChanged += new System.EventHandler(this.mapPhysicalMapName_SelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 23);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(57, 13);
            this.label43.TabIndex = 0;
            this.label43.Text = "L1 Tilemap";
            // 
            // mapTilemapL2Num
            // 
            this.mapTilemapL2Num.Location = new System.Drawing.Point(76, 42);
            this.mapTilemapL2Num.Maximum = new decimal(new int[] {
            244,
            0,
            0,
            0});
            this.mapTilemapL2Num.Name = "mapTilemapL2Num";
            this.mapTilemapL2Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilemapL2Num.TabIndex = 4;
            this.mapTilemapL2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL2Num.ValueChanged += new System.EventHandler(this.mapTilemapL2Num_ValueChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 44);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(57, 13);
            this.label42.TabIndex = 3;
            this.label42.Text = "L2 Tilemap";
            // 
            // mapTilemapL3Name
            // 
            this.mapTilemapL3Name.DropDownHeight = 200;
            this.mapTilemapL3Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilemapL3Name.DropDownWidth = 200;
            this.mapTilemapL3Name.IntegralHeight = false;
            this.mapTilemapL3Name.Location = new System.Drawing.Point(127, 63);
            this.mapTilemapL3Name.Name = "mapTilemapL3Name";
            this.mapTilemapL3Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilemapL3Name.TabIndex = 8;
            this.mapTilemapL3Name.SelectedIndexChanged += new System.EventHandler(this.mapTilemapL3Name_SelectedIndexChanged);
            // 
            // mapTilemapL2Name
            // 
            this.mapTilemapL2Name.DropDownHeight = 200;
            this.mapTilemapL2Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilemapL2Name.DropDownWidth = 200;
            this.mapTilemapL2Name.IntegralHeight = false;
            this.mapTilemapL2Name.Location = new System.Drawing.Point(127, 42);
            this.mapTilemapL2Name.Name = "mapTilemapL2Name";
            this.mapTilemapL2Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilemapL2Name.TabIndex = 5;
            this.mapTilemapL2Name.SelectedIndexChanged += new System.EventHandler(this.mapTilemapL2Name_SelectedIndexChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 65);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(57, 13);
            this.label41.TabIndex = 6;
            this.label41.Text = "L3 Tilemap";
            // 
            // mapTilemapL1Name
            // 
            this.mapTilemapL1Name.DropDownHeight = 200;
            this.mapTilemapL1Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilemapL1Name.DropDownWidth = 200;
            this.mapTilemapL1Name.IntegralHeight = false;
            this.mapTilemapL1Name.Location = new System.Drawing.Point(127, 21);
            this.mapTilemapL1Name.Name = "mapTilemapL1Name";
            this.mapTilemapL1Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilemapL1Name.TabIndex = 2;
            this.mapTilemapL1Name.SelectedIndexChanged += new System.EventHandler(this.mapTilemapL1Name_SelectedIndexChanged);
            // 
            // mapTilemapL3Num
            // 
            this.mapTilemapL3Num.Location = new System.Drawing.Point(76, 63);
            this.mapTilemapL3Num.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.mapTilemapL3Num.Name = "mapTilemapL3Num";
            this.mapTilemapL3Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilemapL3Num.TabIndex = 7;
            this.mapTilemapL3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilemapL3Num.ValueChanged += new System.EventHandler(this.mapTilemapL3Num_ValueChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 111);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(64, 13);
            this.label45.TabIndex = 10;
            this.label45.Text = "Solidity Map";
            // 
            // mapBattlefieldNum
            // 
            this.mapBattlefieldNum.Location = new System.Drawing.Point(76, 130);
            this.mapBattlefieldNum.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.mapBattlefieldNum.Name = "mapBattlefieldNum";
            this.mapBattlefieldNum.Size = new System.Drawing.Size(51, 21);
            this.mapBattlefieldNum.TabIndex = 14;
            this.mapBattlefieldNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapBattlefieldNum.ValueChanged += new System.EventHandler(this.mapBattlefieldNum_ValueChanged);
            // 
            // mapPhysicalMapNum
            // 
            this.mapPhysicalMapNum.Location = new System.Drawing.Point(76, 109);
            this.mapPhysicalMapNum.Maximum = new decimal(new int[] {
            119,
            0,
            0,
            0});
            this.mapPhysicalMapNum.Name = "mapPhysicalMapNum";
            this.mapPhysicalMapNum.Size = new System.Drawing.Size(51, 21);
            this.mapPhysicalMapNum.TabIndex = 11;
            this.mapPhysicalMapNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapPhysicalMapNum.ValueChanged += new System.EventHandler(this.mapPhysicalMapNum_ValueChanged);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 132);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(55, 13);
            this.label76.TabIndex = 13;
            this.label76.Text = "Battlefield";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mapTilesetL3Name);
            this.groupBox2.Controls.Add(this.label40);
            this.groupBox2.Controls.Add(this.mapTilesetL2Name);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.mapTilesetL1Name);
            this.groupBox2.Controls.Add(this.mapTilesetL3Num);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Controls.Add(this.mapTilesetL1Num);
            this.groupBox2.Controls.Add(this.mapTilesetL2Num);
            this.groupBox2.Location = new System.Drawing.Point(0, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 91);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tilesets";
            // 
            // mapTilesetL3Name
            // 
            this.mapTilesetL3Name.DropDownHeight = 200;
            this.mapTilesetL3Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilesetL3Name.IntegralHeight = false;
            this.mapTilesetL3Name.Location = new System.Drawing.Point(127, 63);
            this.mapTilesetL3Name.Name = "mapTilesetL3Name";
            this.mapTilesetL3Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilesetL3Name.TabIndex = 8;
            this.mapTilesetL3Name.SelectedIndexChanged += new System.EventHandler(this.mapTilesetL3Name_SelectedIndexChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(8, 65);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(52, 13);
            this.label40.TabIndex = 6;
            this.label40.Text = "L3 Tileset";
            // 
            // mapTilesetL2Name
            // 
            this.mapTilesetL2Name.DropDownHeight = 200;
            this.mapTilesetL2Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilesetL2Name.DropDownWidth = 200;
            this.mapTilesetL2Name.IntegralHeight = false;
            this.mapTilesetL2Name.Location = new System.Drawing.Point(127, 42);
            this.mapTilesetL2Name.Name = "mapTilesetL2Name";
            this.mapTilesetL2Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilesetL2Name.TabIndex = 5;
            this.mapTilesetL2Name.SelectedIndexChanged += new System.EventHandler(this.mapTilesetL2Name_SelectedIndexChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(8, 44);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(52, 13);
            this.label35.TabIndex = 3;
            this.label35.Text = "L2 Tileset";
            // 
            // mapTilesetL1Name
            // 
            this.mapTilesetL1Name.DropDownHeight = 200;
            this.mapTilesetL1Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapTilesetL1Name.DropDownWidth = 200;
            this.mapTilesetL1Name.IntegralHeight = false;
            this.mapTilesetL1Name.Location = new System.Drawing.Point(127, 21);
            this.mapTilesetL1Name.Name = "mapTilesetL1Name";
            this.mapTilesetL1Name.Size = new System.Drawing.Size(127, 21);
            this.mapTilesetL1Name.TabIndex = 0;
            this.mapTilesetL1Name.SelectedIndexChanged += new System.EventHandler(this.mapTilesetL1Name_SelectedIndexChanged);
            // 
            // mapTilesetL3Num
            // 
            this.mapTilesetL3Num.Location = new System.Drawing.Point(76, 63);
            this.mapTilesetL3Num.Maximum = new decimal(new int[] {
            33,
            0,
            0,
            0});
            this.mapTilesetL3Num.Name = "mapTilesetL3Num";
            this.mapTilesetL3Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilesetL3Num.TabIndex = 7;
            this.mapTilesetL3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilesetL3Num.ValueChanged += new System.EventHandler(this.mapTilesetL3Num_ValueChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(8, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(52, 13);
            this.label34.TabIndex = 1;
            this.label34.Text = "L1 Tileset";
            // 
            // mapTilesetL1Num
            // 
            this.mapTilesetL1Num.Location = new System.Drawing.Point(76, 21);
            this.mapTilesetL1Num.Maximum = new decimal(new int[] {
            92,
            0,
            0,
            0});
            this.mapTilesetL1Num.Name = "mapTilesetL1Num";
            this.mapTilesetL1Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilesetL1Num.TabIndex = 2;
            this.mapTilesetL1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilesetL1Num.ValueChanged += new System.EventHandler(this.mapTilesetL1Num_ValueChanged);
            // 
            // mapTilesetL2Num
            // 
            this.mapTilesetL2Num.Location = new System.Drawing.Point(76, 42);
            this.mapTilesetL2Num.Maximum = new decimal(new int[] {
            92,
            0,
            0,
            0});
            this.mapTilesetL2Num.Name = "mapTilesetL2Num";
            this.mapTilesetL2Num.Size = new System.Drawing.Size(51, 21);
            this.mapTilesetL2Num.TabIndex = 4;
            this.mapTilesetL2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapTilesetL2Num.ValueChanged += new System.EventHandler(this.mapTilesetL2Num_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mapGFXSetL3Name);
            this.groupBox1.Controls.Add(this.mapGFXSet4Num);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.mapGFXSetL3Num);
            this.groupBox1.Controls.Add(this.mapGFXSet5Name);
            this.groupBox1.Controls.Add(this.mapGFXSet5Num);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.mapGFXSet3Num);
            this.groupBox1.Controls.Add(this.mapGFXSet1Num);
            this.groupBox1.Controls.Add(this.mapGFXSet1Name);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.mapGFXSet2Name);
            this.groupBox1.Controls.Add(this.mapGFXSet4Name);
            this.groupBox1.Controls.Add(this.mapGFXSet3Name);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.mapGFXSet2Num);
            this.groupBox1.Location = new System.Drawing.Point(0, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 153);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graphic Sets";
            // 
            // mapGFXSetL3Name
            // 
            this.mapGFXSetL3Name.DropDownHeight = 197;
            this.mapGFXSetL3Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSetL3Name.IntegralHeight = false;
            this.mapGFXSetL3Name.Items.AddRange(new object[] {
            "Monstro Town",
            "Tower, interior",
            "Castle, interior",
            "Forest Maze",
            "Sunken Ship",
            "Sewers 1",
            "____",
            "Plains",
            "____",
            "Waterfall",
            "Nimbus clouds",
            "Yo\'ster Isle",
            "Town 2",
            "Sewers 2",
            "Houses, interior",
            "Keep throne",
            "____",
            "Maps",
            "Star Hill",
            "Marrymore Scene",
            "Nimbus houses",
            "Keep 2",
            "Temple",
            "Desert",
            "____",
            "Smithy Factory",
            "Smithy Pad",
            "Smithy 2",
            "{NONE}"});
            this.mapGFXSetL3Name.Location = new System.Drawing.Point(127, 125);
            this.mapGFXSetL3Name.Name = "mapGFXSetL3Name";
            this.mapGFXSetL3Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSetL3Name.TabIndex = 17;
            this.mapGFXSetL3Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSetL3Name_SelectedIndexChanged);
            // 
            // mapGFXSet4Num
            // 
            this.mapGFXSet4Num.Location = new System.Drawing.Point(76, 83);
            this.mapGFXSet4Num.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.mapGFXSet4Num.Name = "mapGFXSet4Num";
            this.mapGFXSet4Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSet4Num.TabIndex = 10;
            this.mapGFXSet4Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet4Num.ValueChanged += new System.EventHandler(this.mapGFXSet4Num_ValueChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 127);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(59, 13);
            this.label44.TabIndex = 15;
            this.label44.Text = "L3 GFX Set";
            // 
            // mapGFXSetL3Num
            // 
            this.mapGFXSetL3Num.Location = new System.Drawing.Point(76, 125);
            this.mapGFXSetL3Num.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.mapGFXSetL3Num.Name = "mapGFXSetL3Num";
            this.mapGFXSetL3Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSetL3Num.TabIndex = 16;
            this.mapGFXSetL3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSetL3Num.ValueChanged += new System.EventHandler(this.mapGFXSetL3Num_ValueChanged);
            // 
            // mapGFXSet5Name
            // 
            this.mapGFXSet5Name.DropDownHeight = 197;
            this.mapGFXSet5Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSet5Name.DropDownWidth = 197;
            this.mapGFXSet5Name.IntegralHeight = false;
            this.mapGFXSet5Name.Location = new System.Drawing.Point(127, 104);
            this.mapGFXSet5Name.Name = "mapGFXSet5Name";
            this.mapGFXSet5Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSet5Name.TabIndex = 14;
            this.mapGFXSet5Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSet5Name_SelectedIndexChanged);
            // 
            // mapGFXSet5Num
            // 
            this.mapGFXSet5Num.Location = new System.Drawing.Point(76, 104);
            this.mapGFXSet5Num.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.mapGFXSet5Num.Name = "mapGFXSet5Num";
            this.mapGFXSet5Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSet5Num.TabIndex = 13;
            this.mapGFXSet5Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet5Num.ValueChanged += new System.EventHandler(this.mapGFXSet5Num_ValueChanged);
            // 
            // mapGFXSet3Num
            // 
            this.mapGFXSet3Num.Location = new System.Drawing.Point(76, 62);
            this.mapGFXSet3Num.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.mapGFXSet3Num.Name = "mapGFXSet3Num";
            this.mapGFXSet3Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSet3Num.TabIndex = 7;
            this.mapGFXSet3Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet3Num.ValueChanged += new System.EventHandler(this.mapGFXSet3Num_ValueChanged);
            // 
            // mapGFXSet1Num
            // 
            this.mapGFXSet1Num.Location = new System.Drawing.Point(76, 20);
            this.mapGFXSet1Num.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.mapGFXSet1Num.Name = "mapGFXSet1Num";
            this.mapGFXSet1Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSet1Num.TabIndex = 1;
            this.mapGFXSet1Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet1Num.ValueChanged += new System.EventHandler(this.mapGFXSet1Num_ValueChanged);
            // 
            // mapGFXSet1Name
            // 
            this.mapGFXSet1Name.DropDownHeight = 200;
            this.mapGFXSet1Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSet1Name.DropDownWidth = 200;
            this.mapGFXSet1Name.IntegralHeight = false;
            this.mapGFXSet1Name.Location = new System.Drawing.Point(127, 20);
            this.mapGFXSet1Name.Name = "mapGFXSet1Name";
            this.mapGFXSet1Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSet1Name.TabIndex = 2;
            this.mapGFXSet1Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSet1Name_SelectedIndexChanged);
            // 
            // mapGFXSet2Name
            // 
            this.mapGFXSet2Name.DropDownHeight = 197;
            this.mapGFXSet2Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSet2Name.DropDownWidth = 197;
            this.mapGFXSet2Name.IntegralHeight = false;
            this.mapGFXSet2Name.Location = new System.Drawing.Point(127, 41);
            this.mapGFXSet2Name.Name = "mapGFXSet2Name";
            this.mapGFXSet2Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSet2Name.TabIndex = 5;
            this.mapGFXSet2Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSet2Name_SelectedIndexChanged);
            // 
            // mapGFXSet4Name
            // 
            this.mapGFXSet4Name.DropDownHeight = 197;
            this.mapGFXSet4Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSet4Name.DropDownWidth = 197;
            this.mapGFXSet4Name.IntegralHeight = false;
            this.mapGFXSet4Name.Location = new System.Drawing.Point(127, 83);
            this.mapGFXSet4Name.Name = "mapGFXSet4Name";
            this.mapGFXSet4Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSet4Name.TabIndex = 11;
            this.mapGFXSet4Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSet4Name_SelectedIndexChanged);
            // 
            // mapGFXSet3Name
            // 
            this.mapGFXSet3Name.DropDownHeight = 197;
            this.mapGFXSet3Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGFXSet3Name.DropDownWidth = 197;
            this.mapGFXSet3Name.IntegralHeight = false;
            this.mapGFXSet3Name.Location = new System.Drawing.Point(127, 62);
            this.mapGFXSet3Name.Name = "mapGFXSet3Name";
            this.mapGFXSet3Name.Size = new System.Drawing.Size(127, 21);
            this.mapGFXSet3Name.TabIndex = 8;
            this.mapGFXSet3Name.SelectedIndexChanged += new System.EventHandler(this.mapGFXSet3Name_SelectedIndexChanged);
            // 
            // mapGFXSet2Num
            // 
            this.mapGFXSet2Num.Location = new System.Drawing.Point(76, 41);
            this.mapGFXSet2Num.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.mapGFXSet2Num.Name = "mapGFXSet2Num";
            this.mapGFXSet2Num.Size = new System.Drawing.Size(51, 21);
            this.mapGFXSet2Num.TabIndex = 4;
            this.mapGFXSet2Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mapGFXSet2Num.ValueChanged += new System.EventHandler(this.mapGFXSet2Num_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox11);
            this.tabPage2.Controls.Add(this.label53);
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.layerMessageBox);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(260, 640);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "LAYER";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.layerOBJEffects);
            this.groupBox11.Controls.Add(this.layerL3Effects);
            this.groupBox11.Controls.Add(this.label38);
            this.groupBox11.Controls.Add(this.layerWaveEffect);
            this.groupBox11.Controls.Add(this.label39);
            this.groupBox11.Location = new System.Drawing.Point(0, 570);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(260, 70);
            this.groupBox11.TabIndex = 8;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Layer Animation Effects";
            // 
            // layerOBJEffects
            // 
            this.layerOBJEffects.DropDownHeight = 160;
            this.layerOBJEffects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerOBJEffects.DropDownWidth = 200;
            this.layerOBJEffects.IntegralHeight = false;
            this.layerOBJEffects.Items.AddRange(new object[] {
            "{NOTHING}",
            "waterfall",
            "???",
            "glowing save point (NPC #0)",
            "flashing chandelier",
            "glowing save point (NPC #1)",
            "___",
            "glowing save point (NPC #2)",
            "water tunnel",
            "glowing save point (NPC #3)",
            "___",
            "___",
            "___",
            "___",
            "___",
            "___",
            "glowing magma",
            "___",
            "___",
            "___",
            "___",
            "___",
            "___",
            "___",
            "___"});
            this.layerOBJEffects.Location = new System.Drawing.Point(52, 41);
            this.layerOBJEffects.Name = "layerOBJEffects";
            this.layerOBJEffects.Size = new System.Drawing.Size(102, 21);
            this.layerOBJEffects.TabIndex = 4;
            this.layerOBJEffects.SelectedIndexChanged += new System.EventHandler(this.layerOBJEffects_SelectedIndexChanged);
            // 
            // layerL3Effects
            // 
            this.layerL3Effects.DropDownHeight = 160;
            this.layerL3Effects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL3Effects.DropDownWidth = 200;
            this.layerL3Effects.IntegralHeight = false;
            this.layerL3Effects.Items.AddRange(new object[] {
            "(none)",
            "spinning wall decor, outside",
            "glowing ship lanterns",
            "spinning mushrooms",
            "rippling pond water",
            "spinning wall decor, inside",
            "talking organ pipes",
            "burning torches",
            "moving conveyor belts",
            "flowing ground water",
            "rotating flowers",
            "boiling lava",
            "rippling sewer water",
            "???",
            "spinning Moleville decor",
            "flowing river water",
            "glowing stars",
            "still sea water",
            "moving conveyor belts",
            "spinning Nimbus decor",
            "hot springs",
            "Smelter\'s melted metal",
            "Toadofsky\'s singing choir"});
            this.layerL3Effects.Location = new System.Drawing.Point(52, 20);
            this.layerL3Effects.Name = "layerL3Effects";
            this.layerL3Effects.Size = new System.Drawing.Size(102, 21);
            this.layerL3Effects.TabIndex = 1;
            this.layerL3Effects.SelectedIndexChanged += new System.EventHandler(this.layerL3Effects_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 44);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(40, 13);
            this.label38.TabIndex = 3;
            this.label38.Text = "Sprites";
            // 
            // layerWaveEffect
            // 
            this.layerWaveEffect.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerWaveEffect.BackColor = System.Drawing.SystemColors.Control;
            this.layerWaveEffect.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerWaveEffect.ForeColor = System.Drawing.Color.Gray;
            this.layerWaveEffect.Location = new System.Drawing.Point(157, 20);
            this.layerWaveEffect.Name = "layerWaveEffect";
            this.layerWaveEffect.Size = new System.Drawing.Size(96, 21);
            this.layerWaveEffect.TabIndex = 2;
            this.layerWaveEffect.Text = "RIPPLING WATER";
            this.layerWaveEffect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerWaveEffect.UseCompatibleTextRendering = true;
            this.layerWaveEffect.UseVisualStyleBackColor = false;
            this.layerWaveEffect.CheckedChanged += new System.EventHandler(this.layerWaveEffect_CheckedChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 24);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(18, 13);
            this.label39.TabIndex = 0;
            this.label39.Text = "L3";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(6, 9);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(49, 13);
            this.label53.TabIndex = 0;
            this.label53.Text = "Message";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.layerL3ScrollSpeed);
            this.groupBox10.Controls.Add(this.label83);
            this.groupBox10.Controls.Add(this.layerInfiniteAutoscroll);
            this.groupBox10.Controls.Add(this.layerL2ScrollShift);
            this.groupBox10.Controls.Add(this.layerL3ScrollShift);
            this.groupBox10.Controls.Add(this.label85);
            this.groupBox10.Controls.Add(this.layerL3ScrollDirection);
            this.groupBox10.Controls.Add(this.layerL2ScrollSpeed);
            this.groupBox10.Controls.Add(this.layerL2ScrollDirection);
            this.groupBox10.Location = new System.Drawing.Point(0, 469);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(260, 95);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Layer Auto-scrolling";
            // 
            // layerL3ScrollSpeed
            // 
            this.layerL3ScrollSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL3ScrollSpeed.Items.AddRange(new object[] {
            "(none)",
            "very slow",
            "slow",
            "med slow",
            "med fast",
            "fast",
            "very fast"});
            this.layerL3ScrollSpeed.Location = new System.Drawing.Point(181, 68);
            this.layerL3ScrollSpeed.Name = "layerL3ScrollSpeed";
            this.layerL3ScrollSpeed.Size = new System.Drawing.Size(73, 21);
            this.layerL3ScrollSpeed.TabIndex = 8;
            this.layerL3ScrollSpeed.SelectedIndexChanged += new System.EventHandler(this.layerL3ScrollSpeed_SelectedIndexChanged);
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(6, 71);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(97, 13);
            this.label83.TabIndex = 6;
            this.label83.Text = "L3 Direction/Speed";
            // 
            // layerInfiniteAutoscroll
            // 
            this.layerInfiniteAutoscroll.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerInfiniteAutoscroll.BackColor = System.Drawing.SystemColors.Control;
            this.layerInfiniteAutoscroll.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerInfiniteAutoscroll.ForeColor = System.Drawing.Color.Gray;
            this.layerInfiniteAutoscroll.Location = new System.Drawing.Point(6, 20);
            this.layerInfiniteAutoscroll.Name = "layerInfiniteAutoscroll";
            this.layerInfiniteAutoscroll.Size = new System.Drawing.Size(97, 21);
            this.layerInfiniteAutoscroll.TabIndex = 0;
            this.layerInfiniteAutoscroll.Text = "INFINITE";
            this.layerInfiniteAutoscroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerInfiniteAutoscroll.UseCompatibleTextRendering = true;
            this.layerInfiniteAutoscroll.UseVisualStyleBackColor = false;
            this.layerInfiniteAutoscroll.CheckedChanged += new System.EventHandler(this.layerInfiniteAutoscroll_CheckedChanged);
            // 
            // layerL2ScrollShift
            // 
            this.layerL2ScrollShift.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerL2ScrollShift.BackColor = System.Drawing.SystemColors.Control;
            this.layerL2ScrollShift.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerL2ScrollShift.ForeColor = System.Drawing.Color.Gray;
            this.layerL2ScrollShift.Location = new System.Drawing.Point(109, 20);
            this.layerL2ScrollShift.Name = "layerL2ScrollShift";
            this.layerL2ScrollShift.Size = new System.Drawing.Size(72, 21);
            this.layerL2ScrollShift.TabIndex = 1;
            this.layerL2ScrollShift.Text = "L2 SHIFT";
            this.layerL2ScrollShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerL2ScrollShift.UseCompatibleTextRendering = true;
            this.layerL2ScrollShift.UseVisualStyleBackColor = false;
            this.layerL2ScrollShift.CheckedChanged += new System.EventHandler(this.layerL2ScrollShift_CheckedChanged);
            // 
            // layerL3ScrollShift
            // 
            this.layerL3ScrollShift.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerL3ScrollShift.BackColor = System.Drawing.SystemColors.Control;
            this.layerL3ScrollShift.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerL3ScrollShift.ForeColor = System.Drawing.Color.Gray;
            this.layerL3ScrollShift.Location = new System.Drawing.Point(182, 20);
            this.layerL3ScrollShift.Name = "layerL3ScrollShift";
            this.layerL3ScrollShift.Size = new System.Drawing.Size(72, 21);
            this.layerL3ScrollShift.TabIndex = 2;
            this.layerL3ScrollShift.Text = "L3 SHIFT";
            this.layerL3ScrollShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerL3ScrollShift.UseCompatibleTextRendering = true;
            this.layerL3ScrollShift.UseVisualStyleBackColor = false;
            this.layerL3ScrollShift.CheckedChanged += new System.EventHandler(this.layerL3ScrollShift_CheckedChanged);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(6, 50);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(97, 13);
            this.label85.TabIndex = 3;
            this.label85.Text = "L2 Direction/Speed";
            // 
            // layerL3ScrollDirection
            // 
            this.layerL3ScrollDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL3ScrollDirection.Items.AddRange(new object[] {
            "W",
            "NW",
            "N",
            "NE",
            "E",
            "SE",
            "S",
            "SW"});
            this.layerL3ScrollDirection.Location = new System.Drawing.Point(109, 68);
            this.layerL3ScrollDirection.Name = "layerL3ScrollDirection";
            this.layerL3ScrollDirection.Size = new System.Drawing.Size(72, 21);
            this.layerL3ScrollDirection.TabIndex = 7;
            this.layerL3ScrollDirection.SelectedIndexChanged += new System.EventHandler(this.layerL3ScrollDirection_SelectedIndexChanged);
            // 
            // layerL2ScrollSpeed
            // 
            this.layerL2ScrollSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL2ScrollSpeed.Items.AddRange(new object[] {
            "(none)",
            "very slow",
            "slow",
            "med slow",
            "med fast",
            "fast",
            "very fast"});
            this.layerL2ScrollSpeed.Location = new System.Drawing.Point(181, 47);
            this.layerL2ScrollSpeed.Name = "layerL2ScrollSpeed";
            this.layerL2ScrollSpeed.Size = new System.Drawing.Size(73, 21);
            this.layerL2ScrollSpeed.TabIndex = 5;
            this.layerL2ScrollSpeed.SelectedIndexChanged += new System.EventHandler(this.layerL2ScrollSpeed_SelectedIndexChanged);
            // 
            // layerL2ScrollDirection
            // 
            this.layerL2ScrollDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL2ScrollDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.layerL2ScrollDirection.Items.AddRange(new object[] {
            "W",
            "NW",
            "N",
            "NE",
            "E",
            "SE",
            "S",
            "SW"});
            this.layerL2ScrollDirection.Location = new System.Drawing.Point(109, 47);
            this.layerL2ScrollDirection.Name = "layerL2ScrollDirection";
            this.layerL2ScrollDirection.Size = new System.Drawing.Size(72, 21);
            this.layerL2ScrollDirection.TabIndex = 4;
            this.layerL2ScrollDirection.SelectedIndexChanged += new System.EventHandler(this.layerL2ScrollDirection_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.layerL3HSync);
            this.groupBox9.Controls.Add(this.layerL3VSync);
            this.groupBox9.Controls.Add(this.layerL2HSync);
            this.groupBox9.Controls.Add(this.layerL2VSync);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.label11);
            this.groupBox9.Location = new System.Drawing.Point(0, 395);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(260, 68);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Layer Scrolling Synchronization";
            // 
            // layerL3HSync
            // 
            this.layerL3HSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL3HSync.Items.AddRange(new object[] {
            "None",
            "Low",
            "Normal",
            "High"});
            this.layerL3HSync.Location = new System.Drawing.Point(180, 41);
            this.layerL3HSync.Name = "layerL3HSync";
            this.layerL3HSync.Size = new System.Drawing.Size(74, 21);
            this.layerL3HSync.TabIndex = 5;
            this.layerL3HSync.SelectedIndexChanged += new System.EventHandler(this.layerL3HSync_SelectedIndexChanged);
            // 
            // layerL3VSync
            // 
            this.layerL3VSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL3VSync.Items.AddRange(new object[] {
            "None",
            "Low",
            "Normal",
            "High"});
            this.layerL3VSync.Location = new System.Drawing.Point(107, 41);
            this.layerL3VSync.Name = "layerL3VSync";
            this.layerL3VSync.Size = new System.Drawing.Size(73, 21);
            this.layerL3VSync.TabIndex = 4;
            this.layerL3VSync.SelectedIndexChanged += new System.EventHandler(this.layerL3VSync_SelectedIndexChanged);
            // 
            // layerL2HSync
            // 
            this.layerL2HSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL2HSync.Items.AddRange(new object[] {
            "None",
            "Low",
            "Normal",
            "High"});
            this.layerL2HSync.Location = new System.Drawing.Point(180, 20);
            this.layerL2HSync.Name = "layerL2HSync";
            this.layerL2HSync.Size = new System.Drawing.Size(74, 21);
            this.layerL2HSync.TabIndex = 2;
            this.layerL2HSync.SelectedIndexChanged += new System.EventHandler(this.layerL2HSync_SelectedIndexChanged);
            // 
            // layerL2VSync
            // 
            this.layerL2VSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerL2VSync.Items.AddRange(new object[] {
            "None",
            "Low",
            "Normal",
            "High"});
            this.layerL2VSync.Location = new System.Drawing.Point(107, 20);
            this.layerL2VSync.Name = "layerL2VSync";
            this.layerL2VSync.Size = new System.Drawing.Size(73, 21);
            this.layerL2VSync.TabIndex = 1;
            this.layerL2VSync.SelectedIndexChanged += new System.EventHandler(this.layerL2VSync_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "L2 Vert/Horiz Sync";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "L3 Vert/Horiz Sync";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.layerScrollWrapping);
            this.groupBox8.Location = new System.Drawing.Point(0, 326);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(260, 63);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Layer Scrolling Wrap";
            // 
            // layerScrollWrapping
            // 
            this.layerScrollWrapping.CheckOnClick = true;
            this.layerScrollWrapping.ColumnWidth = 60;
            this.layerScrollWrapping.Items.AddRange(new object[] {
            "L1 horiz",
            "L1 vert",
            "L2 horiz",
            "L2 vert",
            "L3 horiz",
            "L3 vert",
            "Culex",
            "Culex"});
            this.layerScrollWrapping.Location = new System.Drawing.Point(6, 20);
            this.layerScrollWrapping.MultiColumn = true;
            this.layerScrollWrapping.Name = "layerScrollWrapping";
            this.layerScrollWrapping.Size = new System.Drawing.Size(248, 36);
            this.layerScrollWrapping.TabIndex = 0;
            this.layerScrollWrapping.SelectedIndexChanged += new System.EventHandler(this.layerScrollWrapping_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.layerL2LeftShift);
            this.groupBox7.Controls.Add(this.layerL2UpShift);
            this.groupBox7.Controls.Add(this.layerL3LeftShift);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.layerL3UpShift);
            this.groupBox7.Location = new System.Drawing.Point(0, 272);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(260, 48);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Layer -(X,Y) Shifting";
            // 
            // layerL2LeftShift
            // 
            this.layerL2LeftShift.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerL2LeftShift.Location = new System.Drawing.Point(30, 20);
            this.layerL2LeftShift.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerL2LeftShift.Name = "layerL2LeftShift";
            this.layerL2LeftShift.Size = new System.Drawing.Size(44, 21);
            this.layerL2LeftShift.TabIndex = 1;
            this.layerL2LeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL2LeftShift.ValueChanged += new System.EventHandler(this.layerL2LeftShift_ValueChanged);
            // 
            // layerL2UpShift
            // 
            this.layerL2UpShift.Location = new System.Drawing.Point(74, 20);
            this.layerL2UpShift.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerL2UpShift.Name = "layerL2UpShift";
            this.layerL2UpShift.Size = new System.Drawing.Size(44, 21);
            this.layerL2UpShift.TabIndex = 2;
            this.layerL2UpShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL2UpShift.ValueChanged += new System.EventHandler(this.layerL2UpShift_ValueChanged);
            // 
            // layerL3LeftShift
            // 
            this.layerL3LeftShift.Location = new System.Drawing.Point(148, 20);
            this.layerL3LeftShift.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerL3LeftShift.Name = "layerL3LeftShift";
            this.layerL3LeftShift.Size = new System.Drawing.Size(44, 21);
            this.layerL3LeftShift.TabIndex = 4;
            this.layerL3LeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL3LeftShift.ValueChanged += new System.EventHandler(this.layerL3LeftShift_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "L3";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "L2";
            // 
            // layerL3UpShift
            // 
            this.layerL3UpShift.Location = new System.Drawing.Point(192, 20);
            this.layerL3UpShift.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerL3UpShift.Name = "layerL3UpShift";
            this.layerL3UpShift.Size = new System.Drawing.Size(44, 21);
            this.layerL3UpShift.TabIndex = 5;
            this.layerL3UpShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerL3UpShift.ValueChanged += new System.EventHandler(this.layerL3UpShift_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.layerMaskHighX);
            this.groupBox6.Controls.Add(this.layerLockMask);
            this.groupBox6.Controls.Add(this.layerMaskHighY);
            this.groupBox6.Controls.Add(this.layerMaskLowX);
            this.groupBox6.Controls.Add(this.layerMaskLowY);
            this.groupBox6.Location = new System.Drawing.Point(0, 178);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(260, 88);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Layer Mask Edges";
            // 
            // layerMaskHighX
            // 
            this.layerMaskHighX.Location = new System.Drawing.Point(92, 40);
            this.layerMaskHighX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerMaskHighX.Name = "layerMaskHighX";
            this.layerMaskHighX.Size = new System.Drawing.Size(44, 21);
            this.layerMaskHighX.TabIndex = 2;
            this.layerMaskHighX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskHighX.ValueChanged += new System.EventHandler(this.layerMaskHighX_ValueChanged);
            // 
            // layerLockMask
            // 
            this.layerLockMask.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerLockMask.BackColor = System.Drawing.SystemColors.Control;
            this.layerLockMask.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerLockMask.ForeColor = System.Drawing.Color.Gray;
            this.layerLockMask.Location = new System.Drawing.Point(142, 40);
            this.layerLockMask.Name = "layerLockMask";
            this.layerLockMask.Size = new System.Drawing.Size(112, 21);
            this.layerLockMask.TabIndex = 4;
            this.layerLockMask.Text = "LOCK SCROLLING";
            this.layerLockMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerLockMask.UseCompatibleTextRendering = true;
            this.layerLockMask.UseVisualStyleBackColor = false;
            this.layerLockMask.CheckedChanged += new System.EventHandler(this.layerLockMask_CheckedChanged);
            // 
            // layerMaskHighY
            // 
            this.layerMaskHighY.Location = new System.Drawing.Point(51, 61);
            this.layerMaskHighY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerMaskHighY.Name = "layerMaskHighY";
            this.layerMaskHighY.Size = new System.Drawing.Size(41, 21);
            this.layerMaskHighY.TabIndex = 3;
            this.layerMaskHighY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskHighY.ValueChanged += new System.EventHandler(this.layerMaskHighY_ValueChanged);
            // 
            // layerMaskLowX
            // 
            this.layerMaskLowX.Location = new System.Drawing.Point(7, 40);
            this.layerMaskLowX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerMaskLowX.Name = "layerMaskLowX";
            this.layerMaskLowX.Size = new System.Drawing.Size(44, 21);
            this.layerMaskLowX.TabIndex = 1;
            this.layerMaskLowX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskLowX.ValueChanged += new System.EventHandler(this.layerMaskLowX_ValueChanged);
            // 
            // layerMaskLowY
            // 
            this.layerMaskLowY.Location = new System.Drawing.Point(51, 20);
            this.layerMaskLowY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.layerMaskLowY.Name = "layerMaskLowY";
            this.layerMaskLowY.Size = new System.Drawing.Size(41, 21);
            this.layerMaskLowY.TabIndex = 0;
            this.layerMaskLowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerMaskLowY.ValueChanged += new System.EventHandler(this.layerMaskLowY_ValueChanged);
            // 
            // layerMessageBox
            // 
            this.layerMessageBox.BackColor = System.Drawing.SystemColors.Window;
            this.layerMessageBox.DropDownHeight = 301;
            this.layerMessageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerMessageBox.DropDownWidth = 250;
            this.layerMessageBox.IntegralHeight = false;
            this.layerMessageBox.Location = new System.Drawing.Point(61, 6);
            this.layerMessageBox.Name = "layerMessageBox";
            this.layerMessageBox.Size = new System.Drawing.Size(194, 21);
            this.layerMessageBox.TabIndex = 1;
            this.layerMessageBox.SelectedIndexChanged += new System.EventHandler(this.layerMessageBox_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.layerColorMathMode);
            this.groupBox5.Controls.Add(this.layerColorMathIntensity);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.label96);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label95);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.layerColorMathBG);
            this.groupBox5.Controls.Add(this.layerPrioritySet);
            this.groupBox5.Controls.Add(this.layerColorMathNPC);
            this.groupBox5.Controls.Add(this.layerMainscreenL1);
            this.groupBox5.Controls.Add(this.layerSubscreenNPC);
            this.groupBox5.Controls.Add(this.layerSubscreenL1);
            this.groupBox5.Controls.Add(this.layerMainscreenNPC);
            this.groupBox5.Controls.Add(this.layerColorMathL1);
            this.groupBox5.Controls.Add(this.layerColorMathL3);
            this.groupBox5.Controls.Add(this.layerMainscreenL2);
            this.groupBox5.Controls.Add(this.layerSubscreenL3);
            this.groupBox5.Controls.Add(this.layerSubscreenL2);
            this.groupBox5.Controls.Add(this.layerMainscreenL3);
            this.groupBox5.Controls.Add(this.layerColorMathL2);
            this.groupBox5.Location = new System.Drawing.Point(0, 33);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(260, 139);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Layer Priorities";
            // 
            // layerColorMathMode
            // 
            this.layerColorMathMode.BackColor = System.Drawing.SystemColors.Window;
            this.layerColorMathMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerColorMathMode.Items.AddRange(new object[] {
            "Plus",
            "Minus"});
            this.layerColorMathMode.Location = new System.Drawing.Point(183, 112);
            this.layerColorMathMode.Name = "layerColorMathMode";
            this.layerColorMathMode.Size = new System.Drawing.Size(71, 21);
            this.layerColorMathMode.TabIndex = 21;
            this.layerColorMathMode.SelectedIndexChanged += new System.EventHandler(this.layerColorMathMode_SelectedIndexChanged);
            // 
            // layerColorMathIntensity
            // 
            this.layerColorMathIntensity.BackColor = System.Drawing.SystemColors.Window;
            this.layerColorMathIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerColorMathIntensity.Items.AddRange(new object[] {
            "Full",
            "Half"});
            this.layerColorMathIntensity.Location = new System.Drawing.Point(72, 112);
            this.layerColorMathIntensity.Name = "layerColorMathIntensity";
            this.layerColorMathIntensity.Size = new System.Drawing.Size(66, 21);
            this.layerColorMathIntensity.TabIndex = 19;
            this.layerColorMathIntensity.SelectedIndexChanged += new System.EventHandler(this.layerColorMathIntensity_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 22);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 0;
            this.label32.Text = "Priority Set";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(144, 115);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(33, 13);
            this.label96.TabIndex = 20;
            this.label96.Text = "Mode";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 88);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 13);
            this.label22.TabIndex = 12;
            this.label22.Text = "Color Math";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(6, 115);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(50, 13);
            this.label95.TabIndex = 18;
            this.label95.Text = "Intensity";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(61, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Mainscreen";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 68);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 7;
            this.label21.Text = "Subscreen";
            // 
            // layerColorMathBG
            // 
            this.layerColorMathBG.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathBG.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathBG.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathBG.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathBG.Location = new System.Drawing.Point(217, 87);
            this.layerColorMathBG.Name = "layerColorMathBG";
            this.layerColorMathBG.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathBG.TabIndex = 17;
            this.layerColorMathBG.Text = "BG";
            this.layerColorMathBG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathBG.UseCompatibleTextRendering = true;
            this.layerColorMathBG.UseVisualStyleBackColor = false;
            this.layerColorMathBG.CheckedChanged += new System.EventHandler(this.layerColorMathBG_CheckedChanged);
            // 
            // layerPrioritySet
            // 
            this.layerPrioritySet.Location = new System.Drawing.Point(72, 20);
            this.layerPrioritySet.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.layerPrioritySet.Name = "layerPrioritySet";
            this.layerPrioritySet.Size = new System.Drawing.Size(71, 21);
            this.layerPrioritySet.TabIndex = 1;
            this.layerPrioritySet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.layerPrioritySet.ValueChanged += new System.EventHandler(this.layerPrioritySet_ValueChanged);
            // 
            // layerColorMathNPC
            // 
            this.layerColorMathNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathNPC.Location = new System.Drawing.Point(181, 87);
            this.layerColorMathNPC.Name = "layerColorMathNPC";
            this.layerColorMathNPC.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathNPC.TabIndex = 16;
            this.layerColorMathNPC.Text = "NPC";
            this.layerColorMathNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathNPC.UseCompatibleTextRendering = true;
            this.layerColorMathNPC.UseVisualStyleBackColor = false;
            this.layerColorMathNPC.CheckedChanged += new System.EventHandler(this.layerColorMathNPC_CheckedChanged);
            // 
            // layerMainscreenL1
            // 
            this.layerMainscreenL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL1.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL1.Location = new System.Drawing.Point(73, 45);
            this.layerMainscreenL1.Name = "layerMainscreenL1";
            this.layerMainscreenL1.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL1.TabIndex = 3;
            this.layerMainscreenL1.Text = "L1";
            this.layerMainscreenL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL1.UseCompatibleTextRendering = true;
            this.layerMainscreenL1.UseVisualStyleBackColor = false;
            this.layerMainscreenL1.CheckedChanged += new System.EventHandler(this.layerMainscreenL1_CheckedChanged);
            // 
            // layerSubscreenNPC
            // 
            this.layerSubscreenNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenNPC.Location = new System.Drawing.Point(181, 66);
            this.layerSubscreenNPC.Name = "layerSubscreenNPC";
            this.layerSubscreenNPC.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenNPC.TabIndex = 11;
            this.layerSubscreenNPC.Text = "NPC";
            this.layerSubscreenNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenNPC.UseCompatibleTextRendering = true;
            this.layerSubscreenNPC.UseVisualStyleBackColor = false;
            this.layerSubscreenNPC.CheckedChanged += new System.EventHandler(this.layerSubscreenNPC_CheckedChanged);
            // 
            // layerSubscreenL1
            // 
            this.layerSubscreenL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL1.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL1.Location = new System.Drawing.Point(73, 66);
            this.layerSubscreenL1.Name = "layerSubscreenL1";
            this.layerSubscreenL1.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL1.TabIndex = 8;
            this.layerSubscreenL1.Text = "L1";
            this.layerSubscreenL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL1.UseCompatibleTextRendering = true;
            this.layerSubscreenL1.UseVisualStyleBackColor = false;
            this.layerSubscreenL1.CheckedChanged += new System.EventHandler(this.layerSubscreenL1_CheckedChanged);
            // 
            // layerMainscreenNPC
            // 
            this.layerMainscreenNPC.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenNPC.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenNPC.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenNPC.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenNPC.Location = new System.Drawing.Point(181, 45);
            this.layerMainscreenNPC.Name = "layerMainscreenNPC";
            this.layerMainscreenNPC.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenNPC.TabIndex = 6;
            this.layerMainscreenNPC.Text = "NPC";
            this.layerMainscreenNPC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenNPC.UseCompatibleTextRendering = true;
            this.layerMainscreenNPC.UseVisualStyleBackColor = false;
            this.layerMainscreenNPC.CheckedChanged += new System.EventHandler(this.layerMainscreenNPC_CheckedChanged);
            // 
            // layerColorMathL1
            // 
            this.layerColorMathL1.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL1.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL1.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL1.Location = new System.Drawing.Point(73, 87);
            this.layerColorMathL1.Name = "layerColorMathL1";
            this.layerColorMathL1.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL1.TabIndex = 13;
            this.layerColorMathL1.Text = "L1";
            this.layerColorMathL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL1.UseCompatibleTextRendering = true;
            this.layerColorMathL1.UseVisualStyleBackColor = false;
            this.layerColorMathL1.CheckedChanged += new System.EventHandler(this.layerColorMathL1_CheckedChanged);
            // 
            // layerColorMathL3
            // 
            this.layerColorMathL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL3.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL3.Location = new System.Drawing.Point(145, 87);
            this.layerColorMathL3.Name = "layerColorMathL3";
            this.layerColorMathL3.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL3.TabIndex = 15;
            this.layerColorMathL3.Text = "L3";
            this.layerColorMathL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL3.UseCompatibleTextRendering = true;
            this.layerColorMathL3.UseVisualStyleBackColor = false;
            this.layerColorMathL3.CheckedChanged += new System.EventHandler(this.layerColorMathL3_CheckedChanged);
            // 
            // layerMainscreenL2
            // 
            this.layerMainscreenL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL2.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL2.Location = new System.Drawing.Point(109, 45);
            this.layerMainscreenL2.Name = "layerMainscreenL2";
            this.layerMainscreenL2.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL2.TabIndex = 4;
            this.layerMainscreenL2.Text = "L2";
            this.layerMainscreenL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL2.UseCompatibleTextRendering = true;
            this.layerMainscreenL2.UseVisualStyleBackColor = false;
            this.layerMainscreenL2.CheckedChanged += new System.EventHandler(this.layerMainscreenL2_CheckedChanged);
            // 
            // layerSubscreenL3
            // 
            this.layerSubscreenL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL3.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL3.Location = new System.Drawing.Point(145, 66);
            this.layerSubscreenL3.Name = "layerSubscreenL3";
            this.layerSubscreenL3.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL3.TabIndex = 10;
            this.layerSubscreenL3.Text = "L3";
            this.layerSubscreenL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL3.UseCompatibleTextRendering = true;
            this.layerSubscreenL3.UseVisualStyleBackColor = false;
            this.layerSubscreenL3.CheckedChanged += new System.EventHandler(this.layerSubscreenL3_CheckedChanged);
            // 
            // layerSubscreenL2
            // 
            this.layerSubscreenL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerSubscreenL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerSubscreenL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerSubscreenL2.ForeColor = System.Drawing.Color.Gray;
            this.layerSubscreenL2.Location = new System.Drawing.Point(109, 66);
            this.layerSubscreenL2.Name = "layerSubscreenL2";
            this.layerSubscreenL2.Size = new System.Drawing.Size(35, 21);
            this.layerSubscreenL2.TabIndex = 9;
            this.layerSubscreenL2.Text = "L2";
            this.layerSubscreenL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerSubscreenL2.UseCompatibleTextRendering = true;
            this.layerSubscreenL2.UseVisualStyleBackColor = false;
            this.layerSubscreenL2.CheckedChanged += new System.EventHandler(this.layerSubscreenL2_CheckedChanged);
            // 
            // layerMainscreenL3
            // 
            this.layerMainscreenL3.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerMainscreenL3.BackColor = System.Drawing.SystemColors.Control;
            this.layerMainscreenL3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerMainscreenL3.ForeColor = System.Drawing.Color.Gray;
            this.layerMainscreenL3.Location = new System.Drawing.Point(145, 45);
            this.layerMainscreenL3.Name = "layerMainscreenL3";
            this.layerMainscreenL3.Size = new System.Drawing.Size(35, 21);
            this.layerMainscreenL3.TabIndex = 5;
            this.layerMainscreenL3.Text = "L3";
            this.layerMainscreenL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerMainscreenL3.UseCompatibleTextRendering = true;
            this.layerMainscreenL3.UseVisualStyleBackColor = false;
            this.layerMainscreenL3.CheckedChanged += new System.EventHandler(this.layerMainscreenL3_CheckedChanged);
            // 
            // layerColorMathL2
            // 
            this.layerColorMathL2.Appearance = System.Windows.Forms.Appearance.Button;
            this.layerColorMathL2.BackColor = System.Drawing.SystemColors.Control;
            this.layerColorMathL2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerColorMathL2.ForeColor = System.Drawing.Color.Gray;
            this.layerColorMathL2.Location = new System.Drawing.Point(109, 87);
            this.layerColorMathL2.Name = "layerColorMathL2";
            this.layerColorMathL2.Size = new System.Drawing.Size(35, 21);
            this.layerColorMathL2.TabIndex = 14;
            this.layerColorMathL2.Text = "L2";
            this.layerColorMathL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.layerColorMathL2.UseCompatibleTextRendering = true;
            this.layerColorMathL2.UseVisualStyleBackColor = false;
            this.layerColorMathL2.CheckedChanged += new System.EventHandler(this.layerColorMathL2_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.panel8);
            this.tabPage6.Controls.Add(this.panel27);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(260, 640);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "MODS";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.tileModsBytesLeft);
            this.panel8.Controls.Add(this.groupBox21);
            this.panel8.Controls.Add(this.tileModsFieldTree);
            this.panel8.Controls.Add(this.toolStrip7);
            this.panel8.Controls.Add(this.label69);
            this.panel8.Controls.Add(this.panel55);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(260, 300);
            this.panel8.TabIndex = 0;
            // 
            // tileModsBytesLeft
            // 
            this.tileModsBytesLeft.BackColor = System.Drawing.SystemColors.Control;
            this.tileModsBytesLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tileModsBytesLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileModsBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileModsBytesLeft.Location = new System.Drawing.Point(125, 44);
            this.tileModsBytesLeft.Name = "tileModsBytesLeft";
            this.tileModsBytesLeft.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tileModsBytesLeft.Size = new System.Drawing.Size(135, 21);
            this.tileModsBytesLeft.TabIndex = 3;
            this.tileModsBytesLeft.Text = "bytes left";
            this.tileModsBytesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.tileModsLayers);
            this.groupBox21.Controls.Add(this.label26);
            this.groupBox21.Controls.Add(this.tileModsY);
            this.groupBox21.Controls.Add(this.label50);
            this.groupBox21.Controls.Add(this.tileModsX);
            this.groupBox21.Controls.Add(this.tileModsHeight);
            this.groupBox21.Controls.Add(this.label36);
            this.groupBox21.Controls.Add(this.tileModsWidth);
            this.groupBox21.Controls.Add(this.label27);
            this.groupBox21.Location = new System.Drawing.Point(127, 68);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(133, 184);
            this.groupBox21.TabIndex = 4;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Coordinates";
            // 
            // tileModsLayers
            // 
            this.tileModsLayers.CheckOnClick = true;
            this.tileModsLayers.ColumnWidth = 60;
            this.tileModsLayers.Items.AddRange(new object[] {
            "Layer 1",
            "Layer 2",
            "Layer 3",
            "B0b7"});
            this.tileModsLayers.Location = new System.Drawing.Point(6, 110);
            this.tileModsLayers.Name = "tileModsLayers";
            this.tileModsLayers.Size = new System.Drawing.Size(121, 68);
            this.tileModsLayers.TabIndex = 8;
            this.tileModsLayers.SelectedIndexChanged += new System.EventHandler(this.tileModsLayers_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 22);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 13);
            this.label26.TabIndex = 0;
            this.label26.Text = "X Coord";
            // 
            // tileModsY
            // 
            this.tileModsY.Location = new System.Drawing.Point(55, 41);
            this.tileModsY.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.tileModsY.Name = "tileModsY";
            this.tileModsY.Size = new System.Drawing.Size(72, 21);
            this.tileModsY.TabIndex = 3;
            this.tileModsY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tileModsY.ValueChanged += new System.EventHandler(this.tileModsY_ValueChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 85);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(38, 13);
            this.label50.TabIndex = 6;
            this.label50.Text = "Height";
            // 
            // tileModsX
            // 
            this.tileModsX.Location = new System.Drawing.Point(55, 20);
            this.tileModsX.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.tileModsX.Name = "tileModsX";
            this.tileModsX.Size = new System.Drawing.Size(72, 21);
            this.tileModsX.TabIndex = 1;
            this.tileModsX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tileModsX.ValueChanged += new System.EventHandler(this.tileModsX_ValueChanged);
            // 
            // tileModsHeight
            // 
            this.tileModsHeight.Location = new System.Drawing.Point(55, 83);
            this.tileModsHeight.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.tileModsHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileModsHeight.Name = "tileModsHeight";
            this.tileModsHeight.Size = new System.Drawing.Size(72, 21);
            this.tileModsHeight.TabIndex = 7;
            this.tileModsHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tileModsHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileModsHeight.ValueChanged += new System.EventHandler(this.tileModsHeight_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 64);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(40, 13);
            this.label36.TabIndex = 4;
            this.label36.Text = "Length";
            // 
            // tileModsWidth
            // 
            this.tileModsWidth.Location = new System.Drawing.Point(55, 62);
            this.tileModsWidth.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.tileModsWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileModsWidth.Name = "tileModsWidth";
            this.tileModsWidth.Size = new System.Drawing.Size(72, 21);
            this.tileModsWidth.TabIndex = 5;
            this.tileModsWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tileModsWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileModsWidth.ValueChanged += new System.EventHandler(this.tileModsWidth_ValueChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(45, 13);
            this.label27.TabIndex = 2;
            this.label27.Text = "Y Coord";
            // 
            // tileModsFieldTree
            // 
            this.tileModsFieldTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.tileModsFieldTree.HideSelection = false;
            this.tileModsFieldTree.HotTracking = true;
            this.tileModsFieldTree.Location = new System.Drawing.Point(0, 44);
            this.tileModsFieldTree.Name = "tileModsFieldTree";
            this.tileModsFieldTree.ShowRootLines = false;
            this.tileModsFieldTree.Size = new System.Drawing.Size(125, 256);
            this.tileModsFieldTree.TabIndex = 2;
            this.tileModsFieldTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tileModsFieldTree_AfterSelect);
            // 
            // toolStrip7
            // 
            this.toolStrip7.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip7.CanOverflow = false;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileModsInsertField,
            this.tileModsInsertInstance,
            this.tileModsDeleteField,
            this.toolStripSeparator11,
            this.tileModsMoveUp,
            this.tileModsMoveDown,
            this.toolStripSeparator12,
            this.tileModsCopy,
            this.tileModsPaste,
            this.tileModsDuplicate});
            this.toolStrip7.Location = new System.Drawing.Point(0, 19);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip7.Size = new System.Drawing.Size(260, 25);
            this.toolStrip7.TabIndex = 1;
            this.toolStrip7.Text = "toolStrip7";
            // 
            // tileModsInsertField
            // 
            this.tileModsInsertField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsInsertField.Image = ((System.Drawing.Image)(resources.GetObject("tileModsInsertField.Image")));
            this.tileModsInsertField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsInsertField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsInsertField.Name = "tileModsInsertField";
            this.tileModsInsertField.Size = new System.Drawing.Size(23, 22);
            this.tileModsInsertField.Text = "New Tilemap Mod";
            this.tileModsInsertField.Click += new System.EventHandler(this.tileModsInsertField_Click);
            // 
            // tileModsInsertInstance
            // 
            this.tileModsInsertInstance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsInsertInstance.Image = ((System.Drawing.Image)(resources.GetObject("tileModsInsertInstance.Image")));
            this.tileModsInsertInstance.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsInsertInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsInsertInstance.Name = "tileModsInsertInstance";
            this.tileModsInsertInstance.Size = new System.Drawing.Size(23, 22);
            this.tileModsInsertInstance.Text = "New Alternate Tilemap Mod";
            this.tileModsInsertInstance.Click += new System.EventHandler(this.tileModsInsertInstance_Click);
            // 
            // tileModsDeleteField
            // 
            this.tileModsDeleteField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsDeleteField.Image = ((System.Drawing.Image)(resources.GetObject("tileModsDeleteField.Image")));
            this.tileModsDeleteField.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsDeleteField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsDeleteField.Name = "tileModsDeleteField";
            this.tileModsDeleteField.Size = new System.Drawing.Size(23, 22);
            this.tileModsDeleteField.Text = "Delete Tilemap Mod";
            this.tileModsDeleteField.Click += new System.EventHandler(this.tileModsDeleteField_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tileModsMoveUp
            // 
            this.tileModsMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsMoveUp.Image = global::LAZYSHELL.Properties.Resources.moveUp;
            this.tileModsMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsMoveUp.Name = "tileModsMoveUp";
            this.tileModsMoveUp.Size = new System.Drawing.Size(23, 22);
            this.tileModsMoveUp.Text = "Move Tilemap Mod Up";
            this.tileModsMoveUp.Click += new System.EventHandler(this.tileModsMoveUp_Click);
            // 
            // tileModsMoveDown
            // 
            this.tileModsMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsMoveDown.Image = global::LAZYSHELL.Properties.Resources.moveDown;
            this.tileModsMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsMoveDown.Name = "tileModsMoveDown";
            this.tileModsMoveDown.Size = new System.Drawing.Size(23, 22);
            this.tileModsMoveDown.Text = "Move Tilemap Mod Down";
            this.tileModsMoveDown.Click += new System.EventHandler(this.tileModsMoveDown_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // tileModsCopy
            // 
            this.tileModsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsCopy.Image = ((System.Drawing.Image)(resources.GetObject("tileModsCopy.Image")));
            this.tileModsCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsCopy.Name = "tileModsCopy";
            this.tileModsCopy.Size = new System.Drawing.Size(23, 22);
            this.tileModsCopy.Text = "Copy Tilemap Mod";
            this.tileModsCopy.Click += new System.EventHandler(this.tileModsCopy_Click);
            // 
            // tileModsPaste
            // 
            this.tileModsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsPaste.Image = ((System.Drawing.Image)(resources.GetObject("tileModsPaste.Image")));
            this.tileModsPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsPaste.Name = "tileModsPaste";
            this.tileModsPaste.Size = new System.Drawing.Size(23, 22);
            this.tileModsPaste.Text = "Paste Tilemap Mod";
            this.tileModsPaste.Click += new System.EventHandler(this.tileModsPaste_Click);
            // 
            // tileModsDuplicate
            // 
            this.tileModsDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tileModsDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("tileModsDuplicate.Image")));
            this.tileModsDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tileModsDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tileModsDuplicate.Name = "tileModsDuplicate";
            this.tileModsDuplicate.Size = new System.Drawing.Size(23, 22);
            this.tileModsDuplicate.Text = "Duplicate Tilemap Mod";
            this.tileModsDuplicate.Click += new System.EventHandler(this.tileModsDuplicate_Click);
            // 
            // label69
            // 
            this.label69.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label69.Dock = System.Windows.Forms.DockStyle.Top;
            this.label69.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(0, 0);
            this.label69.Name = "label69";
            this.label69.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label69.Size = new System.Drawing.Size(260, 19);
            this.label69.TabIndex = 0;
            this.label69.Text = "TILEMAP MODS";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel55
            // 
            this.panel55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel55.BackgroundImage = global::LAZYSHELL.Properties.Resources._bg;
            this.panel55.Location = new System.Drawing.Point(119, 608);
            this.panel55.Name = "panel55";
            this.panel55.Size = new System.Drawing.Size(121, 124);
            this.panel55.TabIndex = 5;
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.groupBox20);
            this.panel27.Controls.Add(this.solidModsBytesLeft);
            this.panel27.Controls.Add(this.solidModsFieldTree);
            this.panel27.Controls.Add(this.toolStrip8);
            this.panel27.Controls.Add(this.label68);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel27.Location = new System.Drawing.Point(0, 300);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(260, 340);
            this.panel27.TabIndex = 1;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label14);
            this.groupBox20.Controls.Add(this.solidModsY);
            this.groupBox20.Controls.Add(this.label67);
            this.groupBox20.Controls.Add(this.label51);
            this.groupBox20.Controls.Add(this.label64);
            this.groupBox20.Controls.Add(this.solidModsX);
            this.groupBox20.Controls.Add(this.solidModsWidth);
            this.groupBox20.Controls.Add(this.solidModsHeight);
            this.groupBox20.Location = new System.Drawing.Point(127, 68);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(133, 110);
            this.groupBox20.TabIndex = 4;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Coordinates";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "X Coord";
            // 
            // solidModsY
            // 
            this.solidModsY.Location = new System.Drawing.Point(57, 41);
            this.solidModsY.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.solidModsY.Name = "solidModsY";
            this.solidModsY.Size = new System.Drawing.Size(70, 21);
            this.solidModsY.TabIndex = 3;
            this.solidModsY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.solidModsY.ValueChanged += new System.EventHandler(this.solidModsY_ValueChanged);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 85);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(38, 13);
            this.label67.TabIndex = 6;
            this.label67.Text = "Height";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 43);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(45, 13);
            this.label51.TabIndex = 2;
            this.label51.Text = "Y Coord";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 64);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(40, 13);
            this.label64.TabIndex = 4;
            this.label64.Text = "Length";
            // 
            // solidModsX
            // 
            this.solidModsX.Location = new System.Drawing.Point(57, 20);
            this.solidModsX.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.solidModsX.Name = "solidModsX";
            this.solidModsX.Size = new System.Drawing.Size(70, 21);
            this.solidModsX.TabIndex = 1;
            this.solidModsX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.solidModsX.ValueChanged += new System.EventHandler(this.solidModsX_ValueChanged);
            // 
            // solidModsWidth
            // 
            this.solidModsWidth.Location = new System.Drawing.Point(57, 62);
            this.solidModsWidth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.solidModsWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.solidModsWidth.Name = "solidModsWidth";
            this.solidModsWidth.Size = new System.Drawing.Size(70, 21);
            this.solidModsWidth.TabIndex = 5;
            this.solidModsWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.solidModsWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.solidModsWidth.ValueChanged += new System.EventHandler(this.solidModsWidth_ValueChanged);
            // 
            // solidModsHeight
            // 
            this.solidModsHeight.Location = new System.Drawing.Point(57, 83);
            this.solidModsHeight.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.solidModsHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.solidModsHeight.Name = "solidModsHeight";
            this.solidModsHeight.Size = new System.Drawing.Size(70, 21);
            this.solidModsHeight.TabIndex = 7;
            this.solidModsHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.solidModsHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.solidModsHeight.ValueChanged += new System.EventHandler(this.solidModsHeight_ValueChanged);
            // 
            // solidModsBytesLeft
            // 
            this.solidModsBytesLeft.BackColor = System.Drawing.SystemColors.Control;
            this.solidModsBytesLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.solidModsBytesLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.solidModsBytesLeft.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solidModsBytesLeft.Location = new System.Drawing.Point(125, 44);
            this.solidModsBytesLeft.Name = "solidModsBytesLeft";
            this.solidModsBytesLeft.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.solidModsBytesLeft.Size = new System.Drawing.Size(135, 21);
            this.solidModsBytesLeft.TabIndex = 3;
            this.solidModsBytesLeft.Text = "bytes left";
            this.solidModsBytesLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // solidModsFieldTree
            // 
            this.solidModsFieldTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.solidModsFieldTree.HideSelection = false;
            this.solidModsFieldTree.HotTracking = true;
            this.solidModsFieldTree.Location = new System.Drawing.Point(0, 44);
            this.solidModsFieldTree.Name = "solidModsFieldTree";
            this.solidModsFieldTree.ShowRootLines = false;
            this.solidModsFieldTree.Size = new System.Drawing.Size(125, 296);
            this.solidModsFieldTree.TabIndex = 2;
            this.solidModsFieldTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.solidModsFieldTree_AfterSelect);
            // 
            // toolStrip8
            // 
            this.toolStrip8.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip8.CanOverflow = false;
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solidModsInsert,
            this.solidModsDelete,
            this.toolStripSeparator13,
            this.solidModsMoveUp,
            this.solidModsMoveDown,
            this.toolStripSeparator14,
            this.solidModsCopy,
            this.solidModsPaste,
            this.solidModsDuplicate});
            this.toolStrip8.Location = new System.Drawing.Point(0, 19);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip8.Size = new System.Drawing.Size(260, 25);
            this.toolStrip8.TabIndex = 1;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // solidModsInsert
            // 
            this.solidModsInsert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsInsert.Image = ((System.Drawing.Image)(resources.GetObject("solidModsInsert.Image")));
            this.solidModsInsert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsInsert.Name = "solidModsInsert";
            this.solidModsInsert.Size = new System.Drawing.Size(23, 22);
            this.solidModsInsert.Text = "New Solidity Mod";
            this.solidModsInsert.Click += new System.EventHandler(this.solidModsInsert_Click);
            // 
            // solidModsDelete
            // 
            this.solidModsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsDelete.Image = ((System.Drawing.Image)(resources.GetObject("solidModsDelete.Image")));
            this.solidModsDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsDelete.Name = "solidModsDelete";
            this.solidModsDelete.Size = new System.Drawing.Size(23, 22);
            this.solidModsDelete.Text = "Delete Solidity Mod";
            this.solidModsDelete.Click += new System.EventHandler(this.solidModsDelete_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // solidModsMoveUp
            // 
            this.solidModsMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsMoveUp.Image = global::LAZYSHELL.Properties.Resources.moveUp;
            this.solidModsMoveUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsMoveUp.Name = "solidModsMoveUp";
            this.solidModsMoveUp.Size = new System.Drawing.Size(23, 22);
            this.solidModsMoveUp.Text = "Move Solidity Mod Up";
            this.solidModsMoveUp.Click += new System.EventHandler(this.solidModsMoveUp_Click);
            // 
            // solidModsMoveDown
            // 
            this.solidModsMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsMoveDown.Image = global::LAZYSHELL.Properties.Resources.moveDown;
            this.solidModsMoveDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsMoveDown.Name = "solidModsMoveDown";
            this.solidModsMoveDown.Size = new System.Drawing.Size(23, 22);
            this.solidModsMoveDown.Text = "Move Solidity Mod Down";
            this.solidModsMoveDown.Click += new System.EventHandler(this.solidModsMoveDown_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // solidModsCopy
            // 
            this.solidModsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsCopy.Image = ((System.Drawing.Image)(resources.GetObject("solidModsCopy.Image")));
            this.solidModsCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsCopy.Name = "solidModsCopy";
            this.solidModsCopy.Size = new System.Drawing.Size(23, 22);
            this.solidModsCopy.Text = "Copy Solidity Mod";
            this.solidModsCopy.Click += new System.EventHandler(this.solidModsCopy_Click);
            // 
            // solidModsPaste
            // 
            this.solidModsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsPaste.Image = ((System.Drawing.Image)(resources.GetObject("solidModsPaste.Image")));
            this.solidModsPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsPaste.Name = "solidModsPaste";
            this.solidModsPaste.Size = new System.Drawing.Size(23, 22);
            this.solidModsPaste.Text = "Paste Solidity Mod";
            this.solidModsPaste.Click += new System.EventHandler(this.solidModsPaste_Click);
            // 
            // solidModsDuplicate
            // 
            this.solidModsDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.solidModsDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("solidModsDuplicate.Image")));
            this.solidModsDuplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.solidModsDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.solidModsDuplicate.Name = "solidModsDuplicate";
            this.solidModsDuplicate.Size = new System.Drawing.Size(23, 22);
            this.solidModsDuplicate.Text = "Duplicate Solidity Mod";
            this.solidModsDuplicate.Click += new System.EventHandler(this.solidModsDuplicate_Click);
            // 
            // label68
            // 
            this.label68.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label68.Dock = System.Windows.Forms.DockStyle.Top;
            this.label68.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(0, 0);
            this.label68.Name = "label68";
            this.label68.Padding = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.label68.Size = new System.Drawing.Size(260, 19);
            this.label68.TabIndex = 0;
            this.label68.Text = "SOLIDITY MAP MODS";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripLevel
            // 
            this.toolStripLevel.CanOverflow = false;
            this.toolStripLevel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLevel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelName,
            this.levelNum,
            this.navigateBck,
            this.navigateFwd,
            this.toolStripSeparator17,
            this.searchBox,
            this.searchLevelNames,
            this.toolStripSeparator7,
            this.levelGotoEvent,
            this.eventExit,
            this.toolStripLabel2,
            this.eventMusic});
            this.toolStripLevel.Location = new System.Drawing.Point(0, 25);
            this.toolStripLevel.Name = "toolStripLevel";
            this.toolStripLevel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripLevel.Size = new System.Drawing.Size(1055, 25);
            this.toolStripLevel.TabIndex = 1;
            // 
            // navigateBck
            // 
            this.navigateBck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateBck.Enabled = false;
            this.navigateBck.Image = ((System.Drawing.Image)(resources.GetObject("navigateBck.Image")));
            this.navigateBck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.navigateBck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateBck.Name = "navigateBck";
            this.navigateBck.Size = new System.Drawing.Size(23, 22);
            this.navigateBck.ToolTipText = "Navigate Backward";
            this.navigateBck.Click += new System.EventHandler(this.navigateBck_Click);
            // 
            // navigateFwd
            // 
            this.navigateFwd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigateFwd.Enabled = false;
            this.navigateFwd.Image = ((System.Drawing.Image)(resources.GetObject("navigateFwd.Image")));
            this.navigateFwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.navigateFwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigateFwd.Name = "navigateFwd";
            this.navigateFwd.Size = new System.Drawing.Size(23, 22);
            this.navigateFwd.ToolTipText = "Navigate Forward";
            this.navigateFwd.Click += new System.EventHandler(this.navigateFwd_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(145, 25);
            // 
            // searchLevelNames
            // 
            this.searchLevelNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchLevelNames.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchLevelNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchLevelNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchLevelNames.Name = "searchLevelNames";
            this.searchLevelNames.Size = new System.Drawing.Size(23, 22);
            this.searchLevelNames.Text = "Search Level Names";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // levelGotoEvent
            // 
            this.levelGotoEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.levelGotoEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.levelGotoEvent.Name = "levelGotoEvent";
            this.levelGotoEvent.Size = new System.Drawing.Size(52, 22);
            this.levelGotoEvent.Text = "EVENT #";
            this.levelGotoEvent.ToolTipText = "Click to edit event";
            this.levelGotoEvent.Click += new System.EventHandler(this.buttonGotoC_Click);
            // 
            // eventExit
            // 
            this.eventExit.AutoSize = false;
            this.eventExit.ContextMenuStrip = null;
            this.eventExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventExit.Hexadecimal = false;
            this.eventExit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eventExit.Location = new System.Drawing.Point(551, 2);
            this.eventExit.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.eventExit.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.eventExit.Name = "eventExit";
            this.eventExit.Size = new System.Drawing.Size(54, 21);
            this.eventExit.Text = "4095";
            this.eventExit.Value = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.eventExit.ValueChanged += new System.EventHandler(this.eventsExitEvent_ValueChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel2.Text = " MUSIC";
            // 
            // eventMusic
            // 
            this.eventMusic.DropDownHeight = 300;
            this.eventMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventMusic.DropDownWidth = 300;
            this.eventMusic.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.eventMusic.IntegralHeight = false;
            this.eventMusic.Name = "eventMusic";
            this.eventMusic.Size = new System.Drawing.Size(170, 25);
            this.eventMusic.SelectedIndexChanged += new System.EventHandler(this.eventsAreaMusic_SelectedIndexChanged);
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
            this.hexEditor.Click += new System.EventHandler(this.hexEditor_Click);
            // 
            // propertiesButton
            // 
            this.propertiesButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.propertiesButton.Checked = true;
            this.propertiesButton.CheckOnClick = true;
            this.propertiesButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.propertiesButton.Image = global::LAZYSHELL.Properties.Resources.showMain;
            this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(23, 22);
            this.propertiesButton.ToolTipText = "Main Properties Window";
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // openTileset
            // 
            this.openTileset.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openTileset.CheckOnClick = true;
            this.openTileset.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTileset.Image = global::LAZYSHELL.Properties.Resources.openTilesets;
            this.openTileset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTileset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTileset.Name = "openTileset";
            this.openTileset.Size = new System.Drawing.Size(23, 22);
            this.openTileset.ToolTipText = "Tileset";
            this.openTileset.Click += new System.EventHandler(this.openTileset_Click);
            // 
            // openTilemap
            // 
            this.openTilemap.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openTilemap.CheckOnClick = true;
            this.openTilemap.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openTilemap.Image = global::LAZYSHELL.Properties.Resources.openMap;
            this.openTilemap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTilemap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTilemap.Name = "openTilemap";
            this.openTilemap.Size = new System.Drawing.Size(23, 22);
            this.openTilemap.ToolTipText = "Tilemap";
            this.openTilemap.Click += new System.EventHandler(this.openTilemap_Click);
            // 
            // openSolidTileset
            // 
            this.openSolidTileset.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.openSolidTileset.CheckOnClick = true;
            this.openSolidTileset.Image = global::LAZYSHELL.Properties.Resources.buttonPhysicalTiles;
            this.openSolidTileset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openSolidTileset.Name = "openSolidTileset";
            this.openSolidTileset.Size = new System.Drawing.Size(23, 22);
            this.openSolidTileset.ToolTipText = "Solid Tileset";
            this.openSolidTileset.Click += new System.EventHandler(this.openSolidTileset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // openPaletteEditor
            // 
            this.openPaletteEditor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openPaletteEditor.Image = global::LAZYSHELL.Properties.Resources.openPalettes;
            this.openPaletteEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPaletteEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPaletteEditor.Name = "openPaletteEditor";
            this.openPaletteEditor.Size = new System.Drawing.Size(23, 22);
            this.openPaletteEditor.ToolTipText = "Palettes";
            this.openPaletteEditor.Click += new System.EventHandler(this.openPaletteEditor_Click);
            // 
            // openGraphicEditor
            // 
            this.openGraphicEditor.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openGraphicEditor.Image = global::LAZYSHELL.Properties.Resources.openGraphics;
            this.openGraphicEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openGraphicEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openGraphicEditor.Name = "openGraphicEditor";
            this.openGraphicEditor.Size = new System.Drawing.Size(23, 22);
            this.openGraphicEditor.ToolTipText = "BPP Graphics";
            this.openGraphicEditor.Click += new System.EventHandler(this.openGraphicEditor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // openTemplates
            // 
            this.openTemplates.CheckOnClick = true;
            this.openTemplates.Image = global::LAZYSHELL.Properties.Resources.openTemplates;
            this.openTemplates.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTemplates.Name = "openTemplates";
            this.openTemplates.Size = new System.Drawing.Size(23, 22);
            this.openTemplates.ToolTipText = "Templates";
            this.openTemplates.Click += new System.EventHandler(this.openTemplates_Click);
            // 
            // openPreviewer
            // 
            this.openPreviewer.Image = ((System.Drawing.Image)(resources.GetObject("openPreviewer.Image")));
            this.openPreviewer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openPreviewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPreviewer.Name = "openPreviewer";
            this.openPreviewer.Size = new System.Drawing.Size(23, 22);
            this.openPreviewer.ToolTipText = "Previewer";
            this.openPreviewer.Click += new System.EventHandler(this.openPreviewer_Click);
            // 
            // spaceAnalyzer
            // 
            this.spaceAnalyzer.Image = global::LAZYSHELL.Properties.Resources.statistics;
            this.spaceAnalyzer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.spaceAnalyzer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.spaceAnalyzer.Name = "spaceAnalyzer";
            this.spaceAnalyzer.Size = new System.Drawing.Size(23, 22);
            this.spaceAnalyzer.ToolTipText = "Space Analyzer";
            this.spaceAnalyzer.Click += new System.EventHandler(this.spaceAnalyzer_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem6.Text = "toolStripMenuItem6";
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // panelLevels
            // 
            this.panelLevels.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelLevels.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLevels.Controls.Add(this.tabControl);
            this.panelLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLevels.Location = new System.Drawing.Point(0, 50);
            this.panelLevels.Name = "panelLevels";
            this.panelLevels.Size = new System.Drawing.Size(1055, 670);
            this.panelLevels.TabIndex = 2;
            // 
            // toolStripToggle
            // 
            this.toolStripToggle.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripToggle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.toolStripSeparator5,
            this.import,
            this.export,
            this.toolStripSeparator9,
            this.toolStripDropDownButton1,
            this.clear,
            this.toolStripSeparator6,
            this.toolStripDropDownButton2,
            this.spaceAnalyzer,
            this.helpTips,
            this.baseConvertor,
            this.toolStripSeparator15,
            this.openSolidTileset,
            this.openTileset,
            this.openTilemap,
            this.toolStripSeparator1,
            this.openPaletteEditor,
            this.openGraphicEditor,
            this.toolStripSeparator2,
            this.openTemplates,
            this.hexEditor,
            this.openPreviewer,
            this.propertiesButton});
            this.toolStripToggle.Location = new System.Drawing.Point(0, 0);
            this.toolStripToggle.Name = "toolStripToggle";
            this.toolStripToggle.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripToggle.Size = new System.Drawing.Size(1055, 25);
            this.toolStripToggle.TabIndex = 0;
            this.toolStripToggle.Text = "toolStrip2";
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // import
            // 
            this.import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.importArchitectureToolStripMenuItem,
            this.toolStripSeparator30,
            this.arraysToolStripMenuItem1,
            this.graphicSetsToolStripMenuItem1});
            this.import.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(29, 22);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.importData;
            this.allToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.allToolStripMenuItem.Text = "Import Level Data...";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.importLevelDataAll_Click);
            // 
            // importArchitectureToolStripMenuItem
            // 
            this.importArchitectureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importArchitectureToolStripMenuItem.Image")));
            this.importArchitectureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importArchitectureToolStripMenuItem.Name = "importArchitectureToolStripMenuItem";
            this.importArchitectureToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importArchitectureToolStripMenuItem.Text = "Import Architecture...";
            this.importArchitectureToolStripMenuItem.Click += new System.EventHandler(this.importArchitectureToolStripMenuItem_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(177, 6);
            // 
            // arraysToolStripMenuItem1
            // 
            this.arraysToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("arraysToolStripMenuItem1.Image")));
            this.arraysToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.arraysToolStripMenuItem1.Name = "arraysToolStripMenuItem1";
            this.arraysToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.arraysToolStripMenuItem1.Text = "Import Arrays...";
            this.arraysToolStripMenuItem1.Click += new System.EventHandler(this.arraysToolStripMenuItem1_Click);
            // 
            // graphicSetsToolStripMenuItem1
            // 
            this.graphicSetsToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("graphicSetsToolStripMenuItem1.Image")));
            this.graphicSetsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.graphicSetsToolStripMenuItem1.Name = "graphicSetsToolStripMenuItem1";
            this.graphicSetsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.graphicSetsToolStripMenuItem1.Text = "Import Graphic Set...";
            this.graphicSetsToolStripMenuItem1.Click += new System.EventHandler(this.graphicSetsToolStripMenuItem1_Click);
            // 
            // export
            // 
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exportArchitectureToolStripMenuItem,
            this.toolStripSeparator28,
            this.arraysToolStripMenuItem,
            this.graphicSetsToolStripMenuItem,
            this.exportLevelImagesToolStripMenuItem1,
            this.toolStripSeparator32,
            this.dumpTextToolStripMenuItem});
            this.export.Image = ((System.Drawing.Image)(resources.GetObject("export.Image")));
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(29, 22);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItem1.Text = "Export Level Data...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.exportLevelDataAll_Click);
            // 
            // exportArchitectureToolStripMenuItem
            // 
            this.exportArchitectureToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportBinary;
            this.exportArchitectureToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportArchitectureToolStripMenuItem.Name = "exportArchitectureToolStripMenuItem";
            this.exportArchitectureToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.exportArchitectureToolStripMenuItem.Text = "Export Architecture...";
            this.exportArchitectureToolStripMenuItem.Click += new System.EventHandler(this.exportArchitectureToolStripMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(179, 6);
            // 
            // arraysToolStripMenuItem
            // 
            this.arraysToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportBinary;
            this.arraysToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.arraysToolStripMenuItem.Name = "arraysToolStripMenuItem";
            this.arraysToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.arraysToolStripMenuItem.Text = "Export Arrays...";
            this.arraysToolStripMenuItem.Click += new System.EventHandler(this.arraysToolStripMenuItem_Click);
            // 
            // graphicSetsToolStripMenuItem
            // 
            this.graphicSetsToolStripMenuItem.Image = global::LAZYSHELL.Properties.Resources.exportBinary;
            this.graphicSetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.graphicSetsToolStripMenuItem.Name = "graphicSetsToolStripMenuItem";
            this.graphicSetsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.graphicSetsToolStripMenuItem.Text = "Export Graphic Sets...";
            this.graphicSetsToolStripMenuItem.Click += new System.EventHandler(this.graphicSetsToolStripMenuItem_Click);
            // 
            // exportLevelImagesToolStripMenuItem1
            // 
            this.exportLevelImagesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("exportLevelImagesToolStripMenuItem1.Image")));
            this.exportLevelImagesToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportLevelImagesToolStripMenuItem1.Name = "exportLevelImagesToolStripMenuItem1";
            this.exportLevelImagesToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.exportLevelImagesToolStripMenuItem1.Text = "Export Level Images...";
            this.exportLevelImagesToolStripMenuItem1.Click += new System.EventHandler(this.exportLevelImagesAll_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(179, 6);
            // 
            // dumpTextToolStripMenuItem
            // 
            this.dumpTextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dumpTextToolStripMenuItem.Image")));
            this.dumpTextToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.dumpTextToolStripMenuItem.Name = "dumpTextToolStripMenuItem";
            this.dumpTextToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.dumpTextToolStripMenuItem.Text = "Dump NPCs to Text...";
            this.dumpTextToolStripMenuItem.Click += new System.EventHandler(this.dumpTextToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetLevelMapToolStripMenuItem,
            this.resetLayerDataToolStripMenuItem,
            this.resetNPCDataToolStripMenuItem,
            this.resetEventDataToolStripMenuItem,
            this.resetExitDataToolStripMenuItem,
            this.resetOverlapDataToolStripMenuItem,
            this.resetTilemapModsToolStripMenuItem,
            this.resetSolidityModsToolStripMenuItem,
            this.toolStripSeparator3,
            this.resetPaletteSetToolStripMenuItem,
            this.resetGraphicSetToolStripMenuItem,
            this.resetTilesetsToolStripMenuItem,
            this.resetTilemapsToolStripMenuItem,
            this.resetSolidityMapToolStripMenuItem,
            this.toolStripSeparator4,
            this.resetAllComponentsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // resetLevelMapToolStripMenuItem
            // 
            this.resetLevelMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetLevelMapToolStripMenuItem.Image")));
            this.resetLevelMapToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetLevelMapToolStripMenuItem.Name = "resetLevelMapToolStripMenuItem";
            this.resetLevelMapToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetLevelMapToolStripMenuItem.Text = "Reset level map";
            this.resetLevelMapToolStripMenuItem.Click += new System.EventHandler(this.resetLevelMapToolStripMenuItem_Click);
            // 
            // resetLayerDataToolStripMenuItem
            // 
            this.resetLayerDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetLayerDataToolStripMenuItem.Image")));
            this.resetLayerDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetLayerDataToolStripMenuItem.Name = "resetLayerDataToolStripMenuItem";
            this.resetLayerDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetLayerDataToolStripMenuItem.Text = "Reset layer data";
            this.resetLayerDataToolStripMenuItem.Click += new System.EventHandler(this.resetLayerDataToolStripMenuItem_Click);
            // 
            // resetNPCDataToolStripMenuItem
            // 
            this.resetNPCDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetNPCDataToolStripMenuItem.Image")));
            this.resetNPCDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetNPCDataToolStripMenuItem.Name = "resetNPCDataToolStripMenuItem";
            this.resetNPCDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetNPCDataToolStripMenuItem.Text = "Reset NPCs";
            this.resetNPCDataToolStripMenuItem.Click += new System.EventHandler(this.resetNPCDataToolStripMenuItem_Click);
            // 
            // resetEventDataToolStripMenuItem
            // 
            this.resetEventDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetEventDataToolStripMenuItem.Image")));
            this.resetEventDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetEventDataToolStripMenuItem.Name = "resetEventDataToolStripMenuItem";
            this.resetEventDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetEventDataToolStripMenuItem.Text = "Reset event fields";
            this.resetEventDataToolStripMenuItem.Click += new System.EventHandler(this.resetEventDataToolStripMenuItem_Click);
            // 
            // resetExitDataToolStripMenuItem
            // 
            this.resetExitDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetExitDataToolStripMenuItem.Image")));
            this.resetExitDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetExitDataToolStripMenuItem.Name = "resetExitDataToolStripMenuItem";
            this.resetExitDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetExitDataToolStripMenuItem.Text = "Reset exit fields";
            this.resetExitDataToolStripMenuItem.Click += new System.EventHandler(this.resetExitDataToolStripMenuItem_Click);
            // 
            // resetOverlapDataToolStripMenuItem
            // 
            this.resetOverlapDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetOverlapDataToolStripMenuItem.Image")));
            this.resetOverlapDataToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetOverlapDataToolStripMenuItem.Name = "resetOverlapDataToolStripMenuItem";
            this.resetOverlapDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetOverlapDataToolStripMenuItem.Text = "Reset overlaps";
            this.resetOverlapDataToolStripMenuItem.Click += new System.EventHandler(this.resetOverlapDataToolStripMenuItem_Click);
            // 
            // resetTilemapModsToolStripMenuItem
            // 
            this.resetTilemapModsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetTilemapModsToolStripMenuItem.Image")));
            this.resetTilemapModsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTilemapModsToolStripMenuItem.Name = "resetTilemapModsToolStripMenuItem";
            this.resetTilemapModsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetTilemapModsToolStripMenuItem.Text = "Reset tilemap mods";
            this.resetTilemapModsToolStripMenuItem.Click += new System.EventHandler(this.resetTilemapModsToolStripMenuItem_Click);
            // 
            // resetSolidityModsToolStripMenuItem
            // 
            this.resetSolidityModsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetSolidityModsToolStripMenuItem.Image")));
            this.resetSolidityModsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetSolidityModsToolStripMenuItem.Name = "resetSolidityModsToolStripMenuItem";
            this.resetSolidityModsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetSolidityModsToolStripMenuItem.Text = "Reset solidity mods";
            this.resetSolidityModsToolStripMenuItem.Click += new System.EventHandler(this.resetSolidityModsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // resetPaletteSetToolStripMenuItem
            // 
            this.resetPaletteSetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetPaletteSetToolStripMenuItem.Image")));
            this.resetPaletteSetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetPaletteSetToolStripMenuItem.Name = "resetPaletteSetToolStripMenuItem";
            this.resetPaletteSetToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetPaletteSetToolStripMenuItem.Text = "Reset palette set";
            this.resetPaletteSetToolStripMenuItem.Click += new System.EventHandler(this.resetPaletteSetToolStripMenuItem_Click);
            // 
            // resetGraphicSetToolStripMenuItem
            // 
            this.resetGraphicSetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetGraphicSetToolStripMenuItem.Image")));
            this.resetGraphicSetToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetGraphicSetToolStripMenuItem.Name = "resetGraphicSetToolStripMenuItem";
            this.resetGraphicSetToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetGraphicSetToolStripMenuItem.Text = "Reset graphic set";
            this.resetGraphicSetToolStripMenuItem.Click += new System.EventHandler(this.resetGraphicSetToolStripMenuItem_Click);
            // 
            // resetTilesetsToolStripMenuItem
            // 
            this.resetTilesetsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetTilesetsToolStripMenuItem.Image")));
            this.resetTilesetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTilesetsToolStripMenuItem.Name = "resetTilesetsToolStripMenuItem";
            this.resetTilesetsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetTilesetsToolStripMenuItem.Text = "Reset tilesets";
            this.resetTilesetsToolStripMenuItem.Click += new System.EventHandler(this.resetTilesetsToolStripMenuItem_Click);
            // 
            // resetTilemapsToolStripMenuItem
            // 
            this.resetTilemapsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetTilemapsToolStripMenuItem.Image")));
            this.resetTilemapsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetTilemapsToolStripMenuItem.Name = "resetTilemapsToolStripMenuItem";
            this.resetTilemapsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetTilemapsToolStripMenuItem.Text = "Reset tilemaps";
            this.resetTilemapsToolStripMenuItem.Click += new System.EventHandler(this.resetTilemapsToolStripMenuItem_Click);
            // 
            // resetSolidityMapToolStripMenuItem
            // 
            this.resetSolidityMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetSolidityMapToolStripMenuItem.Image")));
            this.resetSolidityMapToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetSolidityMapToolStripMenuItem.Name = "resetSolidityMapToolStripMenuItem";
            this.resetSolidityMapToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetSolidityMapToolStripMenuItem.Text = "Reset solidity map";
            this.resetSolidityMapToolStripMenuItem.Click += new System.EventHandler(this.resetSolidityMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(172, 6);
            // 
            // resetAllComponentsToolStripMenuItem
            // 
            this.resetAllComponentsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetAllComponentsToolStripMenuItem.Image")));
            this.resetAllComponentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetAllComponentsToolStripMenuItem.Name = "resetAllComponentsToolStripMenuItem";
            this.resetAllComponentsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.resetAllComponentsToolStripMenuItem.Text = "Reset all components";
            this.resetAllComponentsToolStripMenuItem.Click += new System.EventHandler(this.resetAllComponentsToolStripMenuItem_Click);
            // 
            // clear
            // 
            this.clear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLevelDataAll,
            this.toolStripSeparator38,
            this.clearTilesetsAll,
            this.clearTilemapsAll,
            this.clearPhysicalMapsAll,
            this.toolStripSeparator29,
            this.unusedGraphicSetsToolStripMenuItem,
            this.unusedToolStripMenuItem,
            this.unusedToolStripMenuItem1,
            this.unusedToolStripMenuItem2,
            this.unusedToolStripMenuItem3,
            this.toolStripSeparator8,
            this.clearAllComponentsAll,
            this.clearAllComponentsCurrent});
            this.clear.Image = ((System.Drawing.Image)(resources.GetObject("clear.Image")));
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(29, 22);
            // 
            // clearLevelDataAll
            // 
            this.clearLevelDataAll.Image = ((System.Drawing.Image)(resources.GetObject("clearLevelDataAll.Image")));
            this.clearLevelDataAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearLevelDataAll.Name = "clearLevelDataAll";
            this.clearLevelDataAll.Size = new System.Drawing.Size(207, 22);
            this.clearLevelDataAll.Text = "Level Data...";
            this.clearLevelDataAll.Click += new System.EventHandler(this.clearLevelDataAll_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(204, 6);
            // 
            // clearTilesetsAll
            // 
            this.clearTilesetsAll.Image = ((System.Drawing.Image)(resources.GetObject("clearTilesetsAll.Image")));
            this.clearTilesetsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearTilesetsAll.Name = "clearTilesetsAll";
            this.clearTilesetsAll.Size = new System.Drawing.Size(207, 22);
            this.clearTilesetsAll.Text = "Tilesets...";
            this.clearTilesetsAll.Click += new System.EventHandler(this.clearTilesetsAll_Click);
            // 
            // clearTilemapsAll
            // 
            this.clearTilemapsAll.Image = ((System.Drawing.Image)(resources.GetObject("clearTilemapsAll.Image")));
            this.clearTilemapsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearTilemapsAll.Name = "clearTilemapsAll";
            this.clearTilemapsAll.Size = new System.Drawing.Size(207, 22);
            this.clearTilemapsAll.Text = "Tilemaps...";
            this.clearTilemapsAll.Click += new System.EventHandler(this.clearTilemapsAll_Click);
            // 
            // clearPhysicalMapsAll
            // 
            this.clearPhysicalMapsAll.Image = ((System.Drawing.Image)(resources.GetObject("clearPhysicalMapsAll.Image")));
            this.clearPhysicalMapsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearPhysicalMapsAll.Name = "clearPhysicalMapsAll";
            this.clearPhysicalMapsAll.Size = new System.Drawing.Size(207, 22);
            this.clearPhysicalMapsAll.Text = "Solidity Maps...";
            this.clearPhysicalMapsAll.Click += new System.EventHandler(this.clearPhysicalMapsAll_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(204, 6);
            // 
            // unusedGraphicSetsToolStripMenuItem
            // 
            this.unusedGraphicSetsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("unusedGraphicSetsToolStripMenuItem.Image")));
            this.unusedGraphicSetsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedGraphicSetsToolStripMenuItem.Name = "unusedGraphicSetsToolStripMenuItem";
            this.unusedGraphicSetsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.unusedGraphicSetsToolStripMenuItem.Text = "Unused graphic sets...";
            this.unusedGraphicSetsToolStripMenuItem.Click += new System.EventHandler(this.unusedGraphicSetsToolStripMenuItem_Click);
            // 
            // unusedToolStripMenuItem
            // 
            this.unusedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("unusedToolStripMenuItem.Image")));
            this.unusedToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem.Name = "unusedToolStripMenuItem";
            this.unusedToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem.Text = "Unused tilesets...";
            this.unusedToolStripMenuItem.Click += new System.EventHandler(this.unusedToolStripMenuItem_Click);
            // 
            // unusedToolStripMenuItem1
            // 
            this.unusedToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("unusedToolStripMenuItem1.Image")));
            this.unusedToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem1.Name = "unusedToolStripMenuItem1";
            this.unusedToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem1.Text = "Unused tilemaps...";
            this.unusedToolStripMenuItem1.Click += new System.EventHandler(this.unusedToolStripMenuItem1_Click);
            // 
            // unusedToolStripMenuItem2
            // 
            this.unusedToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("unusedToolStripMenuItem2.Image")));
            this.unusedToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem2.Name = "unusedToolStripMenuItem2";
            this.unusedToolStripMenuItem2.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem2.Text = "Unused solidity maps...";
            this.unusedToolStripMenuItem2.Click += new System.EventHandler(this.unusedToolStripMenuItem2_Click);
            // 
            // unusedToolStripMenuItem3
            // 
            this.unusedToolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("unusedToolStripMenuItem3.Image")));
            this.unusedToolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.unusedToolStripMenuItem3.Name = "unusedToolStripMenuItem3";
            this.unusedToolStripMenuItem3.Size = new System.Drawing.Size(207, 22);
            this.unusedToolStripMenuItem3.Text = "Unused (all components)...";
            this.unusedToolStripMenuItem3.Click += new System.EventHandler(this.unusedToolStripMenuItem3_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(204, 6);
            // 
            // clearAllComponentsAll
            // 
            this.clearAllComponentsAll.Image = ((System.Drawing.Image)(resources.GetObject("clearAllComponentsAll.Image")));
            this.clearAllComponentsAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearAllComponentsAll.Name = "clearAllComponentsAll";
            this.clearAllComponentsAll.Size = new System.Drawing.Size(207, 22);
            this.clearAllComponentsAll.Text = "All Components (all)...";
            this.clearAllComponentsAll.Click += new System.EventHandler(this.clearAllComponentsAll_Click);
            // 
            // clearAllComponentsCurrent
            // 
            this.clearAllComponentsCurrent.Image = ((System.Drawing.Image)(resources.GetObject("clearAllComponentsCurrent.Image")));
            this.clearAllComponentsCurrent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clearAllComponentsCurrent.Name = "clearAllComponentsCurrent";
            this.clearAllComponentsCurrent.Size = new System.Drawing.Size(207, 22);
            this.clearAllComponentsCurrent.Text = "All Components (current)...";
            this.clearAllComponentsCurrent.Click += new System.EventHandler(this.clearAllComponentsCurrent_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelInfo});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton2.ToolTipText = "Level Offsets";
            // 
            // levelInfo
            // 
            this.levelInfo.AutoSize = false;
            this.levelInfo.Name = "levelInfo";
            this.levelInfo.Size = new System.Drawing.Size(150, 180);
            this.levelInfo.View = System.Windows.Forms.View.Details;
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
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // Levels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 720);
            this.Controls.Add(this.panelLevels);
            this.Controls.Add(this.toolStripLevel);
            this.Controls.Add(this.toolStripToggle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::LAZYSHELL.Properties.Resources.mainLevels_3_ico;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(5, 5);
            this.Name = "Levels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LEVELS - LAZYSHELL++";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Levels_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Levels_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Levels_KeyDown);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcMapHeader)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcZ)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcSpeedPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcEventORPack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npcPropertyC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapNum)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overlapType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlapZ)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panelOverlapTileset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOverlaps)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventHeight)).EndInit();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.panel52.ResumeLayout(false);
            this.panel52.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitDestX)).EndInit();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitLength)).EndInit();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPaletteSetNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL1Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL2Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilemapL3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapBattlefieldNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPhysicalMapNum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL1Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapTilesetL2Num)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet4Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSetL3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet5Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet3Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet1Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapGFXSet2Num)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2LeftShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL2UpShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3LeftShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerL3UpShift)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskHighY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskLowX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerMaskLowY)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerPrioritySet)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileModsWidth)).EndInit();
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solidModsHeight)).EndInit();
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            this.toolStripLevel.ResumeLayout(false);
            this.toolStripLevel.PerformLayout();
            this.panelLevels.ResumeLayout(false);
            this.toolStripToggle.ResumeLayout(false);
            this.toolStripToggle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private Button npcGotoA;
        private Button npcGotoB;
        private Button eventGotoA;
        private CheckBox eventsWidthXPlusHalf;
        private CheckBox eventsWidthYPlusHalf;
        private CheckBox exits135LengthPlusHalf;
        private CheckBox exits45LengthPlusHalf;
        private CheckBox exitsShowMessage;
        private CheckBox layerColorMathBG;
        private CheckBox layerColorMathL1;
        private CheckBox layerColorMathL2;
        private CheckBox layerColorMathL3;
        private CheckBox layerColorMathNPC;
        private CheckBox layerInfiniteAutoscroll;
        private CheckBox layerL2ScrollShift;
        private CheckBox layerL3ScrollShift;
        private CheckBox layerLockMask;
        private CheckBox layerMainscreenL1;
        private CheckBox layerMainscreenL2;
        private CheckBox layerMainscreenL3;
        private CheckBox layerMainscreenNPC;
        private CheckBox layerSubscreenL1;
        private CheckBox layerSubscreenL2;
        private CheckBox layerSubscreenL3;
        private CheckBox layerSubscreenNPC;
        private CheckBox layerWaveEffect;
        private CheckBox mapSetL3Priority;
        private CheckBox marioZCoordPlusHalf;
        private CheckBox npcVisible;
        private CheckBox npcZ_half;
        private CheckBox overlapCoordZPlusHalf;
        private CheckedListBox layerScrollWrapping;
        private CheckedListBox npcAttributes;
        private CheckedListBox overlapUnknownBits;
        private ComboBox eventFace;
        private ComboBox exitDest;
        private ComboBox exitFace;
        private ComboBox exitDestFace;
        private ComboBox exitType;
        private ComboBox layerColorMathIntensity;
        private ComboBox layerColorMathMode;
        private ComboBox layerL2HSync;
        private ComboBox layerL2ScrollDirection;
        private ComboBox layerL2ScrollSpeed;
        private ComboBox layerL2VSync;
        private ComboBox layerL3Effects;
        private ComboBox layerL3HSync;
        private ComboBox layerL3ScrollDirection;
        private ComboBox layerL3ScrollSpeed;
        private ComboBox layerL3VSync;
        private ComboBox layerMessageBox;
        private ComboBox layerOBJEffects;
        private ComboBox mapBattlefieldName;
        private ComboBox mapGFXSet1Name;
        private ComboBox mapGFXSet2Name;
        private ComboBox mapGFXSet3Name;
        private ComboBox mapGFXSet4Name;
        private ComboBox mapGFXSet5Name;
        private ComboBox mapGFXSetL3Name;
        private ComboBox mapPaletteSetName;
        private ComboBox mapPhysicalMapName;
        private ComboBox mapTilemapL1Name;
        private ComboBox mapTilemapL2Name;
        private ComboBox mapTilemapL3Name;
        private ComboBox mapTilesetL1Name;
        private ComboBox mapTilesetL2Name;
        private ComboBox mapTilesetL3Name;
        private ComboBox npcAfterBattle;
        private ComboBox npcEngageTrigger;
        private ComboBox npcEngageType;
        private ComboBox npcFace;
        private Label label103;
        private Label label104;
        private Label label105;
        private Label label107;
        private Label label109;
        private Label label11;
        private Label label116;
        private Label label119;
        private Label label12;
        private Label label122;
        private Label label124;
        private Label label127;
        private Label label131;
        private Label label133;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label53;
        private Label label54;
        private Label label56;
        private Label label57;
        private Label label59;
        private Label label61;
        private Label label63;
        private Label label76;
        private Label label83;
        private Label label85;
        private Label label95;
        private Label label96;
        private NumericUpDown eventHeight;
        private NumericUpDown eventLength;
        private NumericUpDown eventX;
        private NumericUpDown eventY;
        private NumericUpDown eventZ;
        private NumericUpDown eventEvent;
        private NumericUpDown exitLength;
        private NumericUpDown exitHeight;
        private NumericUpDown exitDestX;
        private NumericUpDown exitDestY;
        private NumericUpDown exitDestZ;
        private NumericUpDown exitX;
        private NumericUpDown exitY;
        private NumericUpDown exitZ;
        private NumericUpDown layerL2LeftShift;
        private NumericUpDown layerL2UpShift;
        private NumericUpDown layerL3LeftShift;
        private NumericUpDown layerL3UpShift;
        private NumericUpDown layerMaskHighX;
        private NumericUpDown layerMaskHighY;
        private NumericUpDown layerMaskLowX;
        private NumericUpDown layerMaskLowY;
        private NumericUpDown layerPrioritySet;
        private NumericUpDown mapBattlefieldNum;
        private NumericUpDown mapGFXSet1Num;
        private NumericUpDown mapGFXSet2Num;
        private NumericUpDown mapGFXSet3Num;
        private NumericUpDown mapGFXSet4Num;
        private NumericUpDown mapGFXSet5Num;
        private NumericUpDown mapGFXSetL3Num;
        private NumericUpDown mapNum;
        private NumericUpDown mapPaletteSetNum;
        private NumericUpDown mapPhysicalMapNum;
        private NumericUpDown mapTilemapL1Num;
        private NumericUpDown mapTilemapL2Num;
        private NumericUpDown mapTilemapL3Num;
        private NumericUpDown mapTilesetL1Num;
        private NumericUpDown mapTilesetL2Num;
        private NumericUpDown mapTilesetL3Num;
        private NumericUpDown npcEventORPack;
        private NumericUpDown npcID;
        private NumericUpDown npcMapHeader;
        private NumericUpDown npcMovement;
        private NumericUpDown npcPropertyA;
        private NumericUpDown npcPropertyB;
        private NumericUpDown npcPropertyC;
        private NumericUpDown npcSpeedPlus;
        private NumericUpDown npcX;
        private NumericUpDown npcY;
        private NumericUpDown npcZ;
        private NumericUpDown overlapX;
        private NumericUpDown overlapY;
        private NumericUpDown overlapZ;
        private NumericUpDown overlapType;
        private Panel panel52;
        private Panel panel68;
        private TabControl tabControl;
        private TabPage tabPage5;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private ToolStrip toolStripLevel;
        private ToolStripButton openPreviewer;
        private ToolStripButton openGraphicEditor;
        private ToolStripButton openPaletteEditor;
        private ToolStripButton openTilemap;
        private ToolStripButton openTileset;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem arraysToolStripMenuItem;
        private ToolStripMenuItem arraysToolStripMenuItem1;
        private ToolStripMenuItem clearAllComponentsAll;
        private ToolStripMenuItem clearAllComponentsCurrent;
        private ToolStripMenuItem clearLevelDataAll;
        private ToolStripMenuItem clearPhysicalMapsAll;
        private ToolStripMenuItem clearTilemapsAll;
        private ToolStripMenuItem clearTilesetsAll;
        private ToolStripMenuItem dumpTextToolStripMenuItem;
        private ToolStripMenuItem exportLevelImagesToolStripMenuItem1;
        private ToolStripMenuItem graphicSetsToolStripMenuItem;
        private ToolStripMenuItem graphicSetsToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem unusedToolStripMenuItem;
        private ToolStripMenuItem unusedToolStripMenuItem1;
        private ToolStripMenuItem unusedToolStripMenuItem2;
        private ToolStripMenuItem unusedToolStripMenuItem3;
        private ToolStripSeparator toolStripSeparator28;
        private ToolStripSeparator toolStripSeparator29;
        private ToolStripSeparator toolStripSeparator30;
        private ToolStripSeparator toolStripSeparator32;
        private ToolStripSeparator toolStripSeparator38;
        private ToolStripSeparator toolStripSeparator8;
        private ToolTip toolTip1;
        private TreeView eventsList;
        private TreeView exitsFieldTree;
        private TreeView npcObjectTree;
        private TreeView overlapFieldTree;
        private ToolStripButton openSolidTileset;
        private Panel panelOverlapTileset;
        private PictureBox pictureBoxOverlaps;
        private ToolStripButton openTemplates;
        private Panel panel2;
        private Panel panelLevels;
        private System.Windows.Forms.ToolStripComboBox levelName;
        private ToolStripButton searchLevelNames;
        private ToolStripTextBox searchBox;
        private ToolStrip toolStripToggle;
        private ToolStripButton save;
        private ToolStripDropDownButton import;
        private ToolStripDropDownButton export;
        private ToolStripDropDownButton clear;
        private ToolStripButton spaceAnalyzer;
        private ToolStripButton helpTips;
        private ToolStripButton baseConvertor;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton propertiesButton;
        private ToolStripNumericUpDown levelNum;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private Panel panel1;
        private ToolStrip toolStrip3;
        private ToolStripButton npcMoveUp;
        private ToolStripButton npcMoveDown;
        private ToolStripButton npcCopy;
        private ToolStripButton npcPaste;
        private ToolStripButton npcDuplicate;
        private ToolStripButton npcInsertObject;
        private ToolStripButton npcRemoveObject;
        private ToolStripButton npcInsertInstance;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStrip toolStrip4;
        private ToolStripButton overlapFieldInsert;
        private ToolStripButton overlapFieldDelete;
        private ToolStrip toolStrip6;
        private ToolStripButton eventsDeleteField;
        private ToolStrip toolStrip5;
        private ToolStripButton exitsInsertField;
        private ToolStripButton exitsDeleteField;
        private ToolStripButton eventsInsertField;
        private ToolStripButton eventsCopyField;
        private ToolStripButton eventsPasteField;
        private ToolStripButton eventsDuplicateField;
        private ToolStripButton exitsCopyField;
        private ToolStripButton exitsPasteField;
        private ToolStripButton exitsDuplicateField;
        private ToolStripButton overlapFieldCopy;
        private ToolStripButton overlapFieldPaste;
        private ToolStripButton overlapFieldDuplicate;
        private TabPage tabPage6;
        private Panel panel8;
        private TreeView tileModsFieldTree;
        private ToolStrip toolStrip7;
        private Label label26;
        private Label label27;
        private NumericUpDown tileModsX;
        private Label label36;
        private Label label50;
        private Panel panel55;
        private Panel panel27;
        private TreeView solidModsFieldTree;
        private ToolStrip toolStrip8;
        private Label label14;
        private Label label51;
        private Label label64;
        private Label label67;
        private Label label68;
        private Label label69;
        private NumericUpDown solidModsY;
        private NumericUpDown solidModsX;
        private NumericUpDown solidModsHeight;
        private NumericUpDown solidModsWidth;
        private CheckedListBox tileModsLayers;
        private NumericUpDown tileModsY;
        private NumericUpDown tileModsHeight;
        private NumericUpDown tileModsWidth;
        private ToolStripButton solidModsInsert;
        private ToolStripButton solidModsDelete;
        private ToolStripButton solidModsCopy;
        private ToolStripButton solidModsPaste;
        private ToolStripButton solidModsDuplicate;
        private ToolStripButton tileModsInsertField;
        private ToolStripButton tileModsDeleteField;
        private ToolStripButton tileModsCopy;
        private ToolStripButton tileModsPaste;
        private ToolStripButton tileModsDuplicate;
        private ToolStripButton tileModsInsertInstance;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton tileModsMoveUp;
        private ToolStripButton tileModsMoveDown;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripButton solidModsMoveUp;
        private ToolStripButton solidModsMoveDown;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox eventMusic;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripButton levelGotoEvent;
        private ToolStripNumericUpDown eventExit;
        private Label npcsBytesLeft;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripLabel overlapsBytesLeft;
        private ToolStripMenuItem unusedGraphicSetsToolStripMenuItem;
        private ToolStripMenuItem importArchitectureToolStripMenuItem;
        private ToolStripMenuItem exportArchitectureToolStripMenuItem;
        private ToolStripButton hexEditor;
        private ToolStripSeparator toolStripSeparator20;
        private ToolStripLabel eventsBytesLeft;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripLabel exitsBytesLeft;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem resetLevelMapToolStripMenuItem;
        private ToolStripMenuItem resetNPCDataToolStripMenuItem;
        private ToolStripMenuItem resetEventDataToolStripMenuItem;
        private ToolStripMenuItem resetExitDataToolStripMenuItem;
        private ToolStripMenuItem resetOverlapDataToolStripMenuItem;
        private ToolStripMenuItem resetLayerDataToolStripMenuItem;
        private ToolStripMenuItem resetTilemapModsToolStripMenuItem;
        private ToolStripMenuItem resetSolidityModsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem resetTilesetsToolStripMenuItem;
        private ToolStripMenuItem resetTilemapsToolStripMenuItem;
        private ToolStripMenuItem resetSolidityMapToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem resetAllComponentsToolStripMenuItem;
        private ToolStripMenuItem resetPaletteSetToolStripMenuItem;
        private ToolStripMenuItem resetGraphicSetToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private LAZYSHELL.ToolStripListView levelInfo;
        private Label tileModsBytesLeft;
        private Label solidModsBytesLeft;
        private ToolStripButton navigateBck;
        private ToolStripButton navigateFwd;
        private ToolStripSeparator toolStripSeparator17;
        private Button findNPCNum;
        private Button openPartitions;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox8;
        private GroupBox groupBox7;
        private Label label1;
        private GroupBox groupBox10;
        private GroupBox groupBox9;
        private GroupBox groupBox11;
        private GroupBox groupBox14;
        private GroupBox groupBox15;
        private GroupBox groupBox13;
        private GroupBox groupBox12;
        private Label label4;
        private Label label3;
        private GroupBox groupBox18;
        private GroupBox groupBox17;
        private GroupBox groupBox16;
        private GroupBox groupBox19;
        private GroupBox groupBox20;
        private GroupBox groupBox21;
        private ToolStripSeparator toolStripSeparator9;
    }
}

