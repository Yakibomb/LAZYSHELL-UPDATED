using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using LAZYSHELL.Properties;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class Levels : NewForm
    {
        #region Variables
        private int index { get { return (int)levelNum.Value; } set { levelNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private Stack<int> navigateBackward = new Stack<int>();
        private Stack<int> navigateForward = new Stack<int>();
        private int lastNavigate = 0;
        private bool disableNavigate = false;
        private State state = State.Instance;
        public Overlay overlay = new Overlay(); //object used to generate all the overlays for levels
        private Settings settings = Settings.Default;
        private Solidity solidity = Solidity.Instance;
        private int zoom { get { return tilemapEditor.Zoom; } }
        // elements
        private Level levelCheck; // to verify a level change
        private Level level { get { return levels[index]; } set { levels[index] = value; } }
        public Level Level { get { return level; } set { level = value; } }
        private Level[] levels { get { return Model.Levels; } set { Model.Levels = value; } }
        private LevelMap[] levelMaps { get { return Model.LevelMaps; } set { Model.LevelMaps = value; } }
        private PaletteSet[] paletteSets { get { return Model.PaletteSets; } set { Model.PaletteSets = value; } }
        private PrioritySet[] prioritySets { get { return Model.PrioritySets; } set { Model.PrioritySets = value; } }
        private SolidityTile[] solidTiles { get { return Model.SolidTiles; } set { Model.SolidTiles = value; } }
        private Partitions[] npcSpritePartitions { get { return Model.NPCSpritePartitions; } }
        private NPCProperties[] npcProperties { get { return Model.NPCProperties; } set { Model.NPCProperties = value; } }
        // updating
        private bool closingEditor = false;
        private bool fullUpdate = false; // indicates that we need to do a complete update instead of a fast update
        private string fullPath; public string FullPath { set { fullPath = value; } }
        // control accessors
        public System.Windows.Forms.ToolStripComboBox LevelName { get { return levelName; } set { levelName = value; } }
        public NewPictureBox picture { get { return tilemapEditor.Picture; } set { tilemapEditor.Picture = value; } }
        public NumericUpDown NPCMapHeader { get { return npcMapHeader; } set { npcMapHeader = value; } }
        public NumericUpDown NPCID { get { return npcID; } set { npcID = value; } }
        public ToolStripNumericUpDown LevelNum { get { return levelNum; } set { levelNum = value; } }
        public TabControl TabControl { get { return tabControl; } set { tabControl = value; } }
        // other windows
        private Search searchWindow;
        private EditLabel labelWindow;
        private SpaceAnalyzer analyzer;
        #endregion
        // constructor
        public Levels()
        {
            InitializeComponent();
            this.levelInfo.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader() });
            Do.AddShortcut(toolStripToggle, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStripToggle, Keys.F1, helpTips);
            Do.AddShortcut(toolStripToggle, Keys.F2, baseConvertor);
            searchWindow = new Search(levelNum, searchBox, searchLevelNames, levelName.Items);
            labelWindow = new EditLabel(levelName, levelNum, "Levels", true);
            this.levelName.Items.AddRange(Lists.Numerize(Lists.LevelNames));
            this.layerMessageBox.Items.Add("{NONE}");
            Dialogue[] dialogues = Model.GetDialogues(0, 128);
            string[] tables = Model.DTEStr(true);
            for (int i = 0; i < 128; i++)
                this.layerMessageBox.Items.Add(dialogues[i].GetStub(true, tables));
            this.mapGFXSet1Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames));
            this.mapGFXSet2Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames));
            this.mapGFXSet3Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames));
            this.mapGFXSet4Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames));
            this.mapGFXSet5Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames));
            this.mapTilesetL1Name.Items.AddRange(Lists.Numerize(Lists.TileSetNames));
            this.mapTilesetL2Name.Items.AddRange(Lists.Numerize(Lists.TileSetNames));
            this.mapTilesetL3Name.Items.AddRange(Lists.Numerize(Lists.TileSetL3Names));
            this.mapTilemapL1Name.Items.AddRange(Lists.Numerize(Lists.TileMapNames));
            this.mapTilemapL2Name.Items.AddRange(Lists.Numerize(Lists.TileMapNames));
            this.mapTilemapL3Name.Items.AddRange(Lists.Numerize(Lists.TileMapL3Names));
            this.mapPhysicalMapName.Items.AddRange(Lists.Numerize(Lists.SolidityMapNames));
            this.mapPaletteSetName.Items.AddRange(Lists.Numerize(Lists.PaletteSetNames));
            this.eventMusic.Items.AddRange(Lists.Numerize(Lists.MusicNames));
            this.Refreshing = true;
            if (settings.RememberLastIndex)
            {
                levelNum.Value = settings.LastLevel;
                levelName.SelectedIndex = settings.LastLevel;
            }
            else
                levelName.SelectedIndex = 0;
            this.Refreshing = false;
            LoadSolidityTileset();
            if (!this.Refreshing)
                RefreshLevel();
            this.Refreshing = true;
            Initialize(); // Sets initial control settings
            this.Refreshing = false;
            new ToolTipLabel(this, baseConvertor, helpTips);
            findNPCNumber = new NPCEditor(this, npcID.Value);
            new ToolTipLabel(findNPCNumber, baseConvertor, helpTips);
            //
            this.History = new History(this, levelName, levelNum);
            lastNavigate = index;
        }
        #region Functions
        private void Initialize()
        {
            InitializeLayerProperties();
            InitializeMapProperties();
            InitializeNPCProperties();
            InitializeExitFieldProperties();
            InitializeEventProperties();
            InitializeOverlapProperties();
            InitializeTileModProperties();
            InitializeSolidModProperties();
            overlapTileset = Model.OverlapTileset;
            // load the individual editors
            tilesetEditor.TopLevel = false;
            tilemapEditor.TopLevel = false;
            levelsSolidTiles.TopLevel = false;
            levelsTemplate.TopLevel = false;
            tilesetEditor.Dock = DockStyle.Right;
            tilemapEditor.Dock = DockStyle.Fill;
            levelsSolidTiles.Dock = DockStyle.Right;
            levelsTemplate.Dock = DockStyle.Right;
            panelLevels.Controls.Add(tilesetEditor);
            panelLevels.Controls.Add(tilemapEditor);
            panelLevels.Controls.Add(levelsSolidTiles);
            panelLevels.Controls.Add(levelsTemplate);
            openTilemap.PerformClick();
            openTileset.PerformClick();
            tilesetEditor.BringToFront();
            tilemapEditor.BringToFront();
        }
        public void RefreshLevel()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Refreshing = true; // Start
            try
            {
                if (levelCheck.Index == index && !fullUpdate)
                {
                    tileset.RedrawTilesets(); // Redraw all tilesets
                    tilemap.RedrawTilemaps();
                    tileMods.RedrawTilemaps();
                    LoadTemplateEditor();
                    LoadTilesetEditor();
                    LoadTilemapEditor();
                }
                else
                {
                    CreateNewLevelData();
                    InitializeLayerProperties();
                    InitializeMapProperties();
                    InitializeNPCProperties();
                    InitializeExitFieldProperties();
                    InitializeEventProperties();
                    InitializeOverlapProperties();
                    InitializeTileModProperties();
                    InitializeSolidModProperties();
                }
            }
            catch
            {
                CreateNewLevelData();
            }
            this.Refreshing = false; // Done
            Cursor.Current = Cursors.Arrow;
        }
        private void CreateNewLevelData()
        {
            levelCheck = level;
            if (tilemap != null)
                tilemap.Assemble();
            tileset = new Tileset(levelMap, paletteSet);
            tilemap = new LevelTilemap(level, tileset);
            foreach (Level l in levels)
            {
                l.LevelTileMods.ClearTilemaps();
                l.LevelSolidMods.ClearTilemaps();
            }
            foreach (LevelSolidMods.LevelMod mod in solidMods.Mods)
                mod.Pixels = solidity.GetTilemapPixels(mod);
            solidityMap = new LevelSolidMap(levelMap);
            fullUpdate = false;
            // load the individual editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTemplateEditor();
            LoadTilesetEditor();
            LoadTilemapEditor();
            SetLevelInfo();
        }
        private void LevelChange()
        {
            bool modified = this.Modified;
            tilemap.Assemble(); // Assemble the edited tileMap into the model
            ResetOverlay();
            RefreshLevel();
            if (tilesetEditor.Layer == 2 && levelMap.GraphicSetL3 == 0xFF)
                tilesetEditor.Layer = 0;
            // load the individual editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTemplateEditor();
            LoadTilesetEditor();
            LoadTilemapEditor();
            this.Modified = modified;
            GC.Collect();
        }
        private void ResetOverlay()
        {
            overlay.NPCImages = null;
        }
        private void SetLevelInfo()
        {
            List<string[]> items = new List<string[]>();
            items.Add(new string[] { "Palette", paletteSet.OFFSET.ToString("X6") });
            items.Add(new string[] { "Layer", ((index * 18) + 0x1D0040).ToString("X6") });
            items.Add(new string[] { "Map", ((index * 2) + 0x148000).ToString("X6") });
            int pointer = Bits.GetShort(Model.ROM, (index * 2) + 0x148000);
            int offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "NPC", (offset + 0x140000).ToString("X6") });
            pointer = (index * 2) + 0x1D2D64;
            offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "Exit", (offset + 0x1D0000).ToString("X6") });
            pointer = (index * 2) + 0x20E000;
            offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "Event", (offset + 0x200000).ToString("X6") });
            pointer = (index * 2) + 0x1D4905;
            offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "Overlap", (offset + 0x1D0000).ToString("X6") });
            pointer = (index * 2) + 0x1D5EBD;
            offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "Tile mod", (offset + 0x1D0000).ToString("X6") });
            pointer = (index * 2) + 0x1D8DB0;
            offset = Bits.GetShort(Model.ROM, pointer);
            items.Add(new string[] { "Solid mod", (offset + 0x1D0000).ToString("X6") });
            ListViewItem[] listViewItems = new ListViewItem[items.Count];
            for (int i = 0; i < items.Count; i++)
                listViewItems[i] = new ListViewItem(items[i]);
            levelInfo.Columns[0].Text = "Element";
            levelInfo.Columns[1].Text = "Offset";
            levelInfo.Items.Clear();
            levelInfo.Items.AddRange(listViewItems);
            levelInfo.Height = 181;
        }
        private string MaximumSpaceExceeded(string name)
        {
            return
                "The total number of " + name + " for all levels has exceeded the maximum allotted space.\n\n" +
                "Try removing some " + name + " to increase the amount of free space for new " + name + ".";
        }
        // directories
        private bool CreateDir(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);
            try
            {
                if (!di.Exists)
                {
                    di.Create();
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Sorry, there was an error trying to create the directory : " + dir, "LAZYSHELL++");
                return false;
            }
        }
        private string GetDirectoryPath(string caption)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = settings.LastDirectory;
            folderBrowserDialog1.Description = caption;
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.LastDirectory = folderBrowserDialog1.SelectedPath;
                return folderBrowserDialog1.SelectedPath;
            }
            else
                return null;
        }
        // assemblers
        public void Assemble()
        {
            if (!this.Modified)
                return;
            LevelChange();
            settings.Save();
            foreach (Level l in levels)
                l.Assemble();
            foreach (PrioritySet ps in prioritySets)
                ps.Assemble();
            foreach (LevelMap lm in levelMaps)
                lm.Assemble();
            foreach (PaletteSet ps in paletteSets)
                ps.Assemble(1);
            foreach (NPCProperties np in npcProperties)
                np.Assemble();
            foreach (SolidityTile st in solidTiles)
                st.Assemble();
            foreach (Partitions sp in npcSpritePartitions)
                sp.Assemble();
            int offsetStart = 0x3166;
            if (CalculateFreeExitSpace() >= 0)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelExits.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("Exit fields were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            offsetStart = 0xE400;
            if (CalculateFreeEventSpace() >= 6)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelEvents.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("Event fields were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            offsetStart = 0x8400;
            if (CalculateFreeNPCSpace() >= 4)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelNPCs.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("NPCs were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            offsetStart = 0x4D05;
            if (CalculateFreeOverlapSpace() >= 0)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelOverlaps.Assemble(ref offsetStart);
            }
            else
                MessageBox.Show("Overlaps were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int offset = 0x1D62BD;
            if (CalculateFreeTileModSpace() >= 0)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelTileMods.Assemble(ref offset);
            }
            else
                MessageBox.Show("Tile mods were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            offset = 0x1D91B0;
            if (CalculateFreeSolidModSpace() >= 0)
            {
                for (int i = 0; i < 512; i++)
                    levels[i].LevelSolidMods.Assemble(ref offset);
            }
            else
                MessageBox.Show("Solid mods were not saved because they exceed the maximum alotted space.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Model.Compress(Model.GraphicSets, Model.EditGraphicSets, 0x0A0000, 0x146000, "GRAPHIC SET", 0, 78, 94, 111, 129, 147, 167, 184, 204, 236, 261);
            tilemap.Assemble();
            Model.Compress(Model.Tilemaps, Model.EditTilemaps, 0x160000, 0x1A8000, "TILE MAP", 0, 109, 163, 219, 275);
            Model.Compress(Model.SolidityMaps, Model.EditSolidityMaps, 0x1B0000, 0x1C8000, "SOLIDITY MAP", 0, 80);
            Model.Compress(Model.Tilesets, Model.EditTilesets, 0x3B0000, 0x3DC000, "TILE SET", 0, 58, 91);
            Model.HexEditor.Compare();
            this.Modified = false;
        }
        #endregion
        #region Event Handlers
        private void levelNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Refreshing)
                return;
            levelName.SelectedIndex = (int)levelNum.Value;
            LevelChange();
            levelNum.Focus();
            settings.LastLevel = (int)levelNum.Value;
            //
            if (!disableNavigate)
            {
                navigateBackward.Push(lastNavigate);
                navigateBck.Enabled = true;
                lastNavigate = index;
            }
        }
        private void levelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Refreshing)
                return;
            levelNum.Value = levelName.SelectedIndex;
        }
        private void navigateBck_Click(object sender, EventArgs e)
        {
            if (navigateBackward.Count < 1)
                return;
            navigateForward.Push(index);
            //
            this.Refreshing = true;
            index = navigateBackward.Peek();
            levelName.SelectedIndex = index;
            this.Refreshing = false;
            //
            LevelChange();
            levelNum.Focus();
            settings.LastLevel = (int)levelNum.Value;
            lastNavigate = index;
            navigateBackward.Pop();
            navigateBck.Enabled = navigateBackward.Count > 0;
            navigateFwd.Enabled = true;
        }
        private void navigateFwd_Click(object sender, EventArgs e)
        {
            if (navigateForward.Count < 1)
                return;
            navigateBackward.Push(index);
            //
            this.Refreshing = true;
            index = navigateForward.Peek();
            levelName.SelectedIndex = index;
            this.Refreshing = false;
            //
            LevelChange();
            levelNum.Focus();
            settings.LastLevel = (int)levelNum.Value;
            lastNavigate = index;
            navigateForward.Pop();
            navigateFwd.Enabled = navigateForward.Count > 0;
            navigateBck.Enabled = true;
        }
        // toolstrip menu items : File
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void import_ButtonClick(object sender, EventArgs e)
        {
        }
        private void export_ButtonClick(object sender, EventArgs e)
        {
        }
        private void clear_ButtonClick(object sender, EventArgs e)
        {
        }
        private void spaceAnalyzer_Click(object sender, EventArgs e)
        {
            LevelChange();
            analyzer = new SpaceAnalyzer();
            analyzer.Show();
            new ToolTipLabel(analyzer, baseConvertor, helpTips);
        }
        private void importArchitectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new IOArchitecture("import", index, levelMap, paletteSet, tileset, tilemap, prioritySets[layer.PrioritySet]).ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void exportArchitectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOArchitecture("export", index, levelMap, paletteSet, tileset, tilemap, prioritySets[layer.PrioritySet]).ShowDialog();
        }
        private void importLevelDataAll_Click(object sender, EventArgs e)
        {
            IOElements ioElements = new IOElements(this, (int)levelNum.Value, "IMPORT LEVEL DATA...");
            if (ioElements.ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
            if (CalculateFreeNPCSpace() < 0)
                MessageBox.Show(MaximumSpaceExceeded("npcs"), "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeExitSpace() < 0)
                MessageBox.Show(MaximumSpaceExceeded("exits"), "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeEventSpace() < 0)
                MessageBox.Show(MaximumSpaceExceeded("events"), "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (CalculateFreeOverlapSpace() < 0)
                MessageBox.Show(MaximumSpaceExceeded("overlaps"), "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void exportLevelDataAll_Click(object sender, EventArgs e)
        {
            new IOElements(this, (int)levelNum.Value, "EXPORT LEVEL DATA...").ShowDialog();
        }
        private void exportLevelImagesAll_Click(object sender, EventArgs e)
        {
            ExportImages exportImages = new ExportImages(index, "levels");
            exportImages.ShowDialog();
        }
        private void clearLevelDataAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)levelNum.Value, "CLEAR LEVEL DATA...").ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void clearTilesetsAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)(levelMap.TilesetL1 + 0x20), "CLEAR TILESETS...").ShowDialog() == DialogResult.Cancel)
                return;
            new ClearElements(null, (int)(levelMap.TilesetL2 + 0x20), "CLEAR TILESETS...").AcceptButton.PerformClick();
            if (levelMap.GraphicSetL3 != 0xFF)
                new ClearElements(null, (int)(levelMap.TilesetL3), "CLEAR TILESETS...").AcceptButton.PerformClick();
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void clearTilemapsAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)(levelMap.TilemapL1 + 0x40), "CLEAR TILEMAPS...").ShowDialog() == DialogResult.Cancel)
                return;
            new ClearElements(null, (int)(levelMap.TilemapL2 + 0x40), "CLEAR TILEMAPS...").AcceptButton.PerformClick();
            if (levelMap.GraphicSetL3 != 0xFF)
                new ClearElements(null, (int)(levelMap.TilemapL3), "CLEAR TILEMAPS...").AcceptButton.PerformClick();
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void clearPhysicalMapsAll_Click(object sender, EventArgs e)
        {
            if (new ClearElements(null, (int)mapPhysicalMapNum.Value, "CLEAR SOLIDITY MAPS...").ShowDialog() == DialogResult.Cancel)
                return;
            fullUpdate = true;
            solidityMap = new LevelSolidMap(levelMap);
            solidityMap.Image = null;
            LoadTilemapEditor();
        }
        private void clearAllComponentsAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all level data, tilesets, tilemaps, physical maps and battlefields.\n" +
                "This will essentially wipe the slate clean for anything having to do with levels.\n\n" +
                "Are you sure you want to do this?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            for (int i = 0; i < 510; i++)
            {
                levels[i].Layer.Clear();
                levels[i].LevelEvents.Clear();
                levels[i].LevelExits.Clear();
                levels[i].LevelNPCs.Clear();
                levels[i].LevelOverlaps.Clear();
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                if (i < 0x20)
                    Model.Tilesets[i] = new byte[0x1000];
                else
                    Model.Tilesets[i] = new byte[0x2000];
                Model.EditTilesets[i] = true;
            }
            for (int i = 0; i < Model.Tilemaps.Length; i++)
            {
                if (i < 0x40)
                    Model.Tilemaps[i] = new byte[0x1000];
                else
                    Model.Tilemaps[i] = new byte[0x2000];
                Model.EditTilemaps[i] = true;
            }
            for (int i = 0; i < Model.SolidityMaps.Length; i++)
            {
                Model.SolidityMaps[i] = new byte[0x20C2];
                Model.EditSolidityMaps[i] = true;
            }
            for (int i = 0; i < Model.TilesetsBF.Length; i++)
            {
                Model.TilesetsBF[i] = new byte[0x2000];
                Model.EditTilesetsBF[i] = true;
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
            solidityMap.Image = null;
        }
        private void clearAllComponentsCurrent_Click(object sender, EventArgs e)
        {
            level.Layer.Clear();
            level.LevelEvents.Clear();
            level.LevelExits.Clear();
            level.LevelNPCs.Clear();
            level.LevelOverlaps.Clear();
            Model.Tilesets[levelMap.TilesetL1 + 0x20] = new byte[0x2000];
            Model.Tilesets[levelMap.TilesetL2 + 0x20] = new byte[0x2000];
            Model.Tilesets[levelMap.TilesetL3] = new byte[0x1000];
            Model.EditTilesets[levelMap.TilesetL1 + 0x20] = true;
            Model.EditTilesets[levelMap.TilesetL2 + 0x20] = true;
            Model.EditTilesets[levelMap.TilesetL3] = true;
            Model.Tilemaps[levelMap.TilemapL1 + 0x40] = new byte[0x2000];
            Model.Tilemaps[levelMap.TilemapL2 + 0x40] = new byte[0x2000];
            Model.Tilemaps[levelMap.TilemapL3] = new byte[0x1000];
            Model.EditTilemaps[levelMap.TilemapL1 + 0x40] = true;
            Model.EditTilemaps[levelMap.TilemapL2 + 0x40] = true;
            Model.EditTilemaps[levelMap.TilemapL3] = true;
            solidityMap.Clear(1);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
            solidityMap.Image = null;
        }
        private void unusedGraphicSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all UNUSED graphic sets.\n\n" +
                "Do you wish to continue?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilesets
            bool[] used = new bool[Model.GraphicSets.Length];
            LevelMap lm;
            foreach (Level lv in levels)
            {
                lm = levelMaps[lv.LevelMap];
                used[lm.GraphicSetA + 0x48] = true;
                used[lm.GraphicSetB + 0x48] = true;
                used[lm.GraphicSetC + 0x48] = true;
                used[lm.GraphicSetD + 0x48] = true;
                used[lm.GraphicSetE + 0x48] = true;
                used[lm.GraphicSetL3] = true;
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                if (!used[i])
                {
                    Model.GraphicSets[i] = new byte[Model.GraphicSets[i].Length];
                    Model.EditGraphicSets[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void unusedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "You are about to clear all UNUSED tilesets.\n\n" +
                "Do you wish to continue?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilesets
            bool[] used = new bool[Model.Tilesets.Length];
            LevelMap lm;
            foreach (Level lv in levels)
            {
                lm = levelMaps[lv.LevelMap];
                used[lm.TilesetL1 + 0x20] = true;
                used[lm.TilesetL2 + 0x20] = true;
                used[lm.TilesetL3] = true;
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                if (!used[i])
                {
                    Model.Tilesets[i] = new byte[i < 0x20 ? 0x1000 : 0x2000];
                    Model.EditTilesets[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void unusedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "You are about to clear all UNUSED tilemaps.\n\n" +
              "Do you wish to continue?",
              "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused tilemaps
            bool[] used = new bool[Model.Tilemaps.Length];
            LevelMap lm;
            foreach (Level lv in levels)
            {
                lm = levelMaps[lv.LevelMap];
                used[lm.TilemapL1 + 0x40] = true;
                used[lm.TilemapL2 + 0x40] = true;
                used[lm.TilemapL3] = true;
            }
            for (int i = 0; i < Model.Tilemaps.Length; i++)
            {
                if (!used[i])
                {
                    Model.Tilemaps[i] = new byte[i < 0x40 ? 0x1000 : 0x2000];
                    Model.EditTilemaps[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void unusedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "You are about to clear all UNUSED solidity maps.\n\n" +
              "Do you wish to continue?",
              "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            // Clear unused physical maps
            bool[] used = new bool[Model.SolidityMaps.Length];
            LevelMap lm;
            foreach (Level lv in levels)
            {
                lm = levelMaps[lv.LevelMap];
                used[lm.SolidityMap] = true;
            }
            for (int i = 0; i < Model.SolidityMaps.Length; i++)
            {
                if (!used[i])
                {
                    Model.SolidityMaps[i] = new byte[0x20C2];
                    Model.EditSolidityMaps[i] = true;
                }
            }
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void unusedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // Clear all unused components
            unusedGraphicSetsToolStripMenuItem.PerformClick();
            unusedToolStripMenuItem.PerformClick();
            unusedToolStripMenuItem1.PerformClick();
            unusedToolStripMenuItem2.PerformClick();
        }
        private void arraysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullPath = GetDirectoryPath("Select directory to export arrays to...");
            fullPath += "\\" + Model.GetFileNameWithoutPath() + " - Arrays\\";
            // Create Level Data directory
            if (!CreateDir(fullPath))
                return;
            FileStream fs;
            BinaryWriter bw;
            //try
            //{
            // Create the file to store the level data
            for (int i = 0; i < Model.GraphicSets.Length; i++)
            {
                CreateDir(fullPath + "Graphic Sets\\");
                fs = new FileStream(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.GraphicSets[i], 0, Model.GraphicSets[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.SolidityMaps.Length; i++)
            {
                CreateDir(fullPath + "Solidity Maps\\");
                fs = new FileStream(fullPath + "Solidity Maps\\solidMap." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.SolidityMaps[i], 0, Model.SolidityMaps[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.Tilemaps.Length; i++)
            {
                CreateDir(fullPath + "Tile Maps\\");
                fs = new FileStream(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.Tilemaps[i], 0, Model.Tilemaps[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.Tilesets.Length; i++)
            {
                CreateDir(fullPath + "Tile Sets\\");
                fs = new FileStream(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.Tilesets[i], 0, Model.Tilesets[i].Length);
                bw.Close();
                fs.Close();
            }
            for (int i = 0; i < Model.TilesetsBF.Length; i++)
            {
                CreateDir(fullPath + "Battlefield Tile Sets\\");
                fs = new FileStream(fullPath + "Battlefield Tile Sets\\tileSetBF." + i.ToString("d3") + ".bin", FileMode.Create, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write(Model.TilesetsBF[i], 0, Model.TilesetsBF[i].Length);
                bw.Close();
                fs.Close();
            }
            //}
            //catch
            //{
            //    MessageBox.Show("There was a problem exporting the arrays.");
            //}
        }
        private void arraysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fullPath = GetDirectoryPath("Select directory to import arrays from...");
            fullPath += "\\";
            FileStream fs;
            BinaryReader br;
            try
            {
                // Create the file to store the level data
                for (int i = 0; i < Model.GraphicSets.Length; i++)
                {
                    if (!File.Exists(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Graphic Sets\\graphicSet." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.GraphicSets[i] = br.ReadBytes(Model.GraphicSets[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditGraphicSets[i] = true;
                }
                for (int i = 0; i < Model.SolidityMaps.Length; i++)
                {
                    if (!File.Exists(fullPath + "Solidity Maps\\solidMap." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Solidity Maps\\solidMap." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.SolidityMaps[i] = br.ReadBytes(Model.SolidityMaps[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditSolidityMaps[i] = true;
                }
                for (int i = 0; i < Model.Tilemaps.Length; i++)
                {
                    if (!File.Exists(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Tile Maps\\tileMap." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.Tilemaps[i] = br.ReadBytes(Model.Tilemaps[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditTilemaps[i] = true;
                }
                for (int i = 0; i < Model.Tilesets.Length; i++)
                {
                    if (!File.Exists(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Tile Sets\\tileSet." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.Tilesets[i] = br.ReadBytes(Model.Tilesets[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditTilesets[i] = true;
                }
                for (int i = 0; i < Model.TilesetsBF.Length; i++)
                {
                    if (!File.Exists(fullPath + "Battlefield Tile Sets\\tileSetBF." + i.ToString("d3") + ".bin"))
                        continue;
                    fs = File.OpenRead(fullPath + "Battlefield Tile Sets\\tileSetBF." + i.ToString("d3") + ".bin");
                    br = new BinaryReader(fs);
                    Model.TilesetsBF[i] = br.ReadBytes(Model.TilesetsBF[i].Length);
                    br.Close();
                    fs.Close();
                    Model.EditTilesetsBF[i] = true;
                }
                fullUpdate = true;
                RefreshLevel();
            }
            catch
            {
                MessageBox.Show("There was a problem importing the arrays.", "LAZYSHELL++");
            }
        }
        private void graphicSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryWriter binWriter;
            string path = GetDirectoryPath("Where do you want to save the graphic sets?");
            path += "\\" + Model.GetFileNameWithoutPath() + " - Graphic Sets\\";
            if (!CreateDir(path))
                return;
            if (path == null)
                return;
            try
            {
                for (int i = 0; i < Model.GraphicSets.Length; i++)
                {
                    binWriter = new BinaryWriter(File.Open(path + "graphicSet." + i.ToString("d3") + ".bin", FileMode.Create));
                    binWriter.Write(Model.GraphicSets[i]);
                    binWriter.Close();
                }
            }
            catch (Exception ioexc)
            {
                MessageBox.Show("Lazy Shell was unable to save the graphic sets.\n\n" + ioexc.Message, "LAZYSHELL++");
            }
        }
        private void graphicSetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Import graphic set";
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                filename = openFileDialog1.FileName;
            else
                return;
            string num = filename.Substring(filename.LastIndexOf('.') - 2, 2);
            int index = Int32.Parse(num, System.Globalization.NumberStyles.HexNumber);
            try
            {
                FileInfo fInfo = new FileInfo(filename);
                if (fInfo.Length != 8192)
                {
                    MessageBox.Show("File is incorrect size, Graphic Sets are 8192 bytes", "LAZYSHELL++");
                    return;
                }
                FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                Model.GraphicSets[index] = br.ReadBytes((int)fInfo.Length);
                Model.EditGraphicSets[index] = true;
                br.Close();
                fStream.Close();
                fullUpdate = true;
                RefreshLevel();
                return;
            }
            catch (Exception ioexc)
            {
                MessageBox.Show("Lazy Shell was unable to Import the Graphic Set.\n\n" + ioexc.Message, "LAZYSHELL++");
                return;
            }
        }
        private void dumpTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - NPCS.txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            StreamWriter npcrip = File.CreateText(saveFileDialog.FileName);
            Level tlvl;
            NPC tnpc;
            NPC.Clone tins;
            int offset;
            int cnt;
            string temp;
            //
            //for (int i = 0; i < Lists.LevelNames.Length; i++)
            //{
            //    npcrip.WriteLine("{" + levels[i].LevelNPCs.MapHeader.ToString("d3") + "} " + Lists.Numerize(Lists.LevelNames, i, 3));
            //}
            //npcrip.Close();
            //return;
            //
            for (int i = 0; i < levels.Length; i++)
            {
                cnt = 0;
                tlvl = levels[i];
                offset = tlvl.LevelNPCs.StartingOffset;
                npcrip.WriteLine("[" + i.ToString("d3") + "]" +
                    "------------------------------------------------------------>");
                for (int j = 0; j < tlvl.LevelNPCs.Npcs.Count; j++)
                {
                    tnpc = (NPC)tlvl.LevelNPCs.Npcs[j];
                    if (tnpc.EngageType == 0) temp = (tnpc.EventORpack + tnpc.PropertyB).ToString("d4");
                    else temp = "N/A";
                    npcrip.Write("NPC #" + cnt.ToString("d2") + ", event: " + temp +
                        ", action: " + (tnpc.Movement + tnpc.PropertyC).ToString("d4") + "\n");
                    for (int k = 0; k < tnpc.Clones.Count; k++)
                    {
                        tins = (NPC.Clone)tnpc.Clones[k];
                        if (tnpc.EngageType == 0) temp = (tins.PropertyB + tnpc.EventORpack).ToString("d4");
                        else temp = "N/A";
                        npcrip.Write("NPC #" + (cnt + 1).ToString("d2") + ", event: " + temp +
                        ", action: " + (tnpc.Movement + tins.PropertyC).ToString("d4") + "\n");
                        cnt++;
                    }
                    cnt++;
                }
                npcrip.Write("\n");
            }
            npcrip.Close();
        }
        // hex editor
        private void hexEditor_Click(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset((index * 18) + 0x1D0040);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        // reset
        private void resetLevelMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current level map. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            levelMap = new LevelMap(level.LevelMap);
            mapNum_ValueChanged(null, null);
        }
        private void resetLayerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current layer data. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            layer = new LevelLayer(index);
            levelNum_ValueChanged(null, null);
        }
        private void resetNPCDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current NPCs. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            npcs = new LevelNPCs(index);
            overlay.NPCImages = null;
            InitializeNPCProperties();
        }
        private void resetEventDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current events. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            events = new LevelEvents(index);
            InitializeEventProperties();
        }
        private void resetExitDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current exits. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            exits = new LevelExits(index);
            InitializeExitFieldProperties();
        }
        private void resetOverlapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current overlaps. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            overlaps = new LevelOverlaps(index);
            InitializeOverlapProperties();
        }
        private void resetTilemapModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current tilemap mods. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            tileMods = new LevelTileMods(index);
            foreach (Level l in levels)
                l.LevelTileMods.ClearTilemaps();
            foreach (LevelTileMods.Mod mod in tileMods.Mods)
            {
                mod.TilemapA = new LevelTilemap(level, tileset, mod, false);
                if (mod.Set)
                    mod.TilemapB = new LevelTilemap(level, tileset, mod, true);
            }
            InitializeTileModProperties();
        }
        private void resetSolidityModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current solidity mods. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            solidMods = new LevelSolidMods(index);
            foreach (Level l in levels)
                l.LevelSolidMods.ClearTilemaps();
            foreach (LevelSolidMods.LevelMod mod in solidMods.Mods)
                mod.Pixels = solidity.GetTilemapPixels(mod);
            InitializeSolidModProperties();
        }
        private void resetGraphicSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current graphic sets. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetA + 0x48, levelMap.GraphicSetA + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetB + 0x48, levelMap.GraphicSetB + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetC + 0x48, levelMap.GraphicSetC + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetD + 0x48, levelMap.GraphicSetD + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetE + 0x48, levelMap.GraphicSetE + 0x49, false);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void resetPaletteSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current palette set. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            int palette = levelMap.PaletteSet;
            paletteSet = new PaletteSet(Model.ROM, palette, (palette * 0xD4) + 0x249FE2, 8, 16, 30);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void resetTilesetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current tilesets. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL1 + 0x20, levelMap.TilesetL1 + 0x21, false);
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL2 + 0x20, levelMap.TilesetL2 + 0x21, false);
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL3, levelMap.TilesetL2 + 1, false);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void resetTilemapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current tilemaps. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL1 + 0x40, levelMap.TilemapL1 + 0x41, false);
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL2 + 0x40, levelMap.TilemapL2 + 0x41, false);
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL3, levelMap.TilemapL3 + 1, false);
            fullUpdate = true;
            if (!this.Refreshing)
                RefreshLevel();
        }
        private void resetSolidityMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current solidity map. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.Decompress(Model.SolidityMaps, 0x1B0000, 0x1D0000, 0x20C2, "", levelMap.SolidityMap, levelMap.SolidityMap + 1, false);
            fullUpdate = true;
            solidityMap = new LevelSolidMap(levelMap);
            solidityMap.Image = null;
            LoadTilemapEditor();
        }
        private void resetAllComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to all components. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            levelMap = new LevelMap(level.LevelMap);
            layer = new LevelLayer(index);
            npcs = new LevelNPCs(index);
            overlay.NPCImages = null;
            events = new LevelEvents(index);
            exits = new LevelExits(index);
            overlaps = new LevelOverlaps(index);
            tileMods = new LevelTileMods(index);
            solidMods = new LevelSolidMods(index);
            int palette = levelMap.PaletteSet;
            paletteSet = new PaletteSet(Model.ROM, palette, (palette * 0xD4) + 0x249FE2, 8, 16, 30);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetA + 0x48, levelMap.GraphicSetA + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetB + 0x48, levelMap.GraphicSetB + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetC + 0x48, levelMap.GraphicSetC + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetD + 0x48, levelMap.GraphicSetD + 0x49, false);
            Model.Decompress(Model.GraphicSets, 0x0A0000, 0x150000, 0x2000, "", levelMap.GraphicSetE + 0x48, levelMap.GraphicSetE + 0x49, false);
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL1 + 0x20, levelMap.TilesetL1 + 0x21, false);
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL2 + 0x20, levelMap.TilesetL2 + 0x21, false);
            Model.Decompress(Model.Tilesets, 0x3B0000, 0x3E0000, 0x1000, "", levelMap.TilesetL3, levelMap.TilesetL2 + 1, false);
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL1 + 0x40, levelMap.TilemapL1 + 0x41, false);
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL2 + 0x40, levelMap.TilemapL2 + 0x41, false);
            Model.Decompress(Model.Tilemaps, 0x160000, 0x1B0000, 0x1000, 0x2000, "", 0x40, levelMap.TilemapL3, levelMap.TilemapL3 + 1, false);
            Model.Decompress(Model.SolidityMaps, 0x1B0000, 0x1D0000, 0x20C2, "", levelMap.SolidityMap, levelMap.SolidityMap + 1, false);
            fullUpdate = true;
            RefreshLevel();
        }
        // toolstrip buttons
        private void SpaceAnalyzerMenuItem_Click(object sender, EventArgs e)
        {
            LevelChange();
            SpaceAnalyzer sa = new SpaceAnalyzer();
            sa.Show();
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //
        private void Levels_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Escape)
            //    ResetTileReplace();
        }
        private void Levels_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !tilemapEditor.Modified && !tilesetEditor.Modified)
                goto Close;
            state.Draw = false;
            state.Erase = false;
            state.Select = false;
            state.Dropper = false;
            state.Fill = false;
            state.TileGrid = false;
            state.IsometricGrid = false;
            DialogResult result;
            result = MessageBox.Show("Levels have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Levels = null;
                Model.LevelMaps = null;
                Model.NPCProperties = null;
                Model.PaletteSets = null;
                Model.PrioritySets = null;
                Model.Tilemaps[0] = null;
                Model.Tilesets[0] = null;
                Model.GraphicSets[0] = null;
                Model.SolidityMaps[0] = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            closingEditor = true;
            searchWindow.Close();
            if (tilesetEditor.TileEditor != null)
                tilesetEditor.TileEditor.Close();
            levelsSolidTiles.SearchSolidTile.Close();
            paletteEditor.Close();
            graphicEditor.Close();
            findNPCNumber.Close();
            searchWindow.Dispose();
            if (tilesetEditor.TileEditor != null)
                tilesetEditor.TileEditor.Dispose();
            levelsSolidTiles.SearchSolidTile.Dispose();
            paletteEditor.Dispose();
            graphicEditor.Dispose();
            findNPCNumber.Dispose();
            if (previewer != null)
                previewer.Close();
            if (analyzer != null)
                analyzer.Close();
            if (partitionBrowser != null)
                partitionBrowser.Close();
            settings.Save();
            closingEditor = false;
        }
        private void Levels_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
        #endregion
    }
}
