using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Undo;
using static System.Windows.Forms.AxHost;

namespace LAZYSHELL
{
    public partial class TilemapEditor : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        public NewPictureBox Picture { get { return pictureBoxLevel; } set { pictureBoxLevel = value; } }
        private Levels levels;
        private MineCart minecart;
        private Level level;
        private Tilemap tilemap;
        private LevelSolidMap solidityMap;
        private Solidity solidity = Solidity.Instance;
        private Tileset tileset;
        private Bitmap tilemapImage, p1Image, p1SolidityImage;
        private Overlay overlay;
        private State state;
        // editors
        private TilesetEditor tilesetEditor;
        private LevelsSolidTiles levelsSolidTiles;
        private LevelsTemplate levelsTemplate;
        private PaletteEditor paletteEditor;
        // main classes
        private LevelMap levelMap { get { return levels.LevelMap; } }
        private LevelLayer layer { get { return level.Layer; } }
        private LevelExits exits { get { return level.LevelExits; } set { level.LevelExits = value; } }
        private LevelEvents events { get { return level.LevelEvents; } set { level.LevelEvents = value; } }
        private LevelNPCs npcs { get { return level.LevelNPCs; } set { level.LevelNPCs = value; } }
        private LevelOverlaps overlaps { get { return level.LevelOverlaps; } set { level.LevelOverlaps = value; } }
        private LevelTileMods tileMods { get { return level.LevelTileMods; } set { level.LevelTileMods = value; } }
        private LevelSolidMods solidMods { get { return level.LevelSolidMods; } set { level.LevelSolidMods = value; } }
        private SolidityTile[] solidTiles { get { return Model.SolidTiles; } }
        private LevelTemplate template { get { return levelsTemplate.Template; } }
        private int width { get { return tilemap.Width_p; } }
        private int height { get { return tilemap.Height_p; } }
        private MCObject[] mushrooms
        {
            get
            {
                if (minecart.Index == 0)
                    return minecart.MinecartData.M7ObjectsA;
                else if (minecart.Index == 1)
                    return minecart.MinecartData.M7ObjectsB;
                return null;
            }
            set
            {
                if (minecart.Index == 0)
                    minecart.MinecartData.M7ObjectsA = value;
                else if (minecart.Index == 1)
                    minecart.MinecartData.M7ObjectsB = value;
            }
        }
        private int erase
        {
            get
            {
                if (minecart != null && minecart.Index < 2)
                    return 0x4F;
                else
                    return 0;
            }
        }
        // buffers
        private CopyBuffer draggedTiles;
        private CopyBuffer copiedTiles;
        private CommandStack commandStack;
        private CommandStack commandStack_S;
        private CommandStack commandStack_TM;
        private CommandStack commandStack_SM;
        private int commandCount = 0;
        private Bitmap selection;
        private Bitmap selsolidt;
        private Point selsolidt_location = new Point(-1, -1);
        private bool defloating = false;
        // hover variables
        private string mouseOverObject;
        private int mouseOverTile = 0;
        private int mouseOverSolidTile = 0;
        private int mouseOverNPC = -1;
        private int mouseOverNPCClone = -1;
        private int mouseOverExitField = -1;
        private int mouseOverEventField = -1;
        private int mouseOverOverlap = -1;
        private int mouseOverTileMod = 0;
        private int mouseOverSolidMod = 0;
        private int mouseOverSolidTileNum
        {
            get
            {
                return Bits.GetShort(solidityMap.Tilemap_Bytes, mouseOverSolidTile * 2);
            }
        }
        private int mouseOverMushroom = -1;
        private string mouseDownObject;
        private int mouseDownNPC = -1;
        private int mouseDownNPCClone = -1;
        private int mouseDownExitField = -1;
        private int mouseDownEventField = -1;
        private int mouseDownOverlap = -1;
        private int mouseDownSolidTile = 0;
        private int mouseDownSolidTileNum
        {
            get
            {
                return Bits.GetShort(solidityMap.Tilemap_Bytes, mouseDownSolidTile * 2);
            }
        }
        private int mouseDownSolidTileIndex = -1;
        private int mouseDownTileMod = 0;
        private int mouseDownSolidMod = 0;
        private int mouseDownMushroom = -1;
        private Point mousePosition = new Point(0, 0);
        private Point mouseDownPosition = new Point(0, 0);
        private Point mouseTilePosition
        {
            get
            {
                return new Point(
                    Math.Min(63, mousePosition.X / 16),
                    Math.Min(63, mousePosition.Y / 16));
            }
        }
        private Point mouseIsometricPosition = new Point(0, 0);
        private Point mouseLastIsometricPosition = new Point(0, 0);
        private Point mouseDownIsometricPosition = new Point(0, 0);
        private Point autoScrollPos = new Point();
        private bool mouseWithinSameBounds = false;
        private bool mouseEnter = false;
        private Form form
        {
            get
            {
                if (levels != null)
                    return levels;
                else
                    return minecart.MiniGames;
            }
        }
        private int zoom { get { return pictureBoxLevel.Zoom; } set { pictureBoxLevel.Zoom = value; } }
        public int Zoom { get { return zoom; } }
        #endregion
        #region Functions
        // main
        public TilemapEditor(Form parent, Level level, Tilemap tilemap, LevelSolidMap solidityMap, Tileset tileset, Overlay overlay,
            PaletteEditor paletteEditor, TilesetEditor tilesetEditor, LevelsSolidTiles levelsSolidTiles, LevelsTemplate levelsTemplate)
        {
            this.state = State.Instance;
            this.levels = (Levels)parent;
            this.level = level;
            this.tilemap = tilemap;
            this.solidityMap = solidityMap;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = tilesetEditor;
            this.levelsSolidTiles = levelsSolidTiles;
            this.paletteEditor = paletteEditor;
            this.levelsTemplate = levelsTemplate;
            this.commandStack = new CommandStack(true);
            this.commandStack_S = new CommandStack(true);
            this.commandStack_TM = new CommandStack(true);
            this.commandStack_SM = new CommandStack(true);
            InitializeComponent();
            this.pictureBoxLevel.Size = new Size(tilemap.Width_p * zoom, tilemap.Height_p * zoom);
            this.pictureBoxLevel.ZoomBoxPosition = new Point(64, 0);
            SetLevelImage();
            // toggle
            toggleBG.Checked = !state.BG;
            toggleCartGrid.Checked = state.TileGrid;
            toggleEvents.Checked = state.Events;
            toggleExits.Checked = state.Exits;
            toggleL1.Checked = state.Layer1;
            toggleL2.Checked = state.Layer2;
            toggleL3.Checked = state.Layer3;
            toggleMask.Checked = state.Mask;
            toggleNPCs.Checked = state.NPCs;
            toggleIsoGrid.Checked = state.IsometricGrid;
            toggleOverlaps.Checked = state.Overlaps;
            toggleP1.Checked = state.Priority1;
            toggleSolid.Checked = state.SolidityLayer;
            toggleSolidMods.Checked = state.SolidMods;
            toggleTileMods.Checked = state.TileMods;
            toggleSolidityFlatMode.Checked = state.SolidityLayerFlatMode;
        }
        public void Reload(Form parent, Level level, Tilemap tilemap, LevelSolidMap solidmap, Tileset tileset, Overlay overlay,
            PaletteEditor paletteEditor, TilesetEditor tilesetEditor, LevelsSolidTiles levelsSolidTiles, LevelsTemplate levelsTemplate)
        {
            this.pictureBoxLevel.Size = new Size(tilemap.Width_p * zoom, tilemap.Height_p * zoom);
            if (this.level != level)
            {
                this.commandStack = new CommandStack(true);
                this.commandStack_S = new CommandStack(true);
                this.commandStack_TM = new CommandStack(true);
                this.commandStack_SM = new CommandStack(true);
            }
            else
            {
                this.commandStack.SetTilemaps(tilemap);
                this.commandStack_S.SetSolidityMaps(solidmap);
            }
            this.levels = (Levels)parent;
            this.level = level;
            this.tilemap = tilemap;
            this.solidityMap = solidmap;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = tilesetEditor;
            this.levelsSolidTiles = levelsSolidTiles;
            this.paletteEditor = paletteEditor;
            this.levelsTemplate = levelsTemplate;
            p1Image = null;
            p1SolidityImage = null;
            selection = null;
            draggedTiles = null;
            overlay.Select.Clear();
            SetLevelImage();
        }
        public TilemapEditor(Form parent, Tilemap tilemap, Tileset tileset, Overlay overlay, PaletteEditor paletteEditor, TilesetEditor tilesetEditor)
        {
            this.state = State.Instance2;
            this.minecart = (MineCart)parent;
            this.tilemap = tilemap;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = tilesetEditor;
            this.paletteEditor = paletteEditor;
            this.commandStack = new CommandStack(true);
            InitializeComponent();
            this.pictureBoxLevel.Size = new Size(tilemap.Width_p * zoom, tilemap.Height_p * zoom);
            this.pictureBoxLevel.ZoomBoxPosition = new Point(64, 0);
            SetLevelImage();
            // toggle
            toggleBG.Visible = false;
            toggleMushrooms.Visible = minecart.Index < 2;
            toggleRails.Visible = minecart.Index < 2;
            toggleEvents.Visible = false;
            toggleExits.Visible = false;
            toggleL1.Visible = false;
            toggleL2.Visible = false;
            toggleL3.Visible = false;
            toggleMask.Visible = false;
            toggleNPCs.Visible = false;
            toggleIsoGrid.Visible = false;
            toggleOverlaps.Visible = false;
            toggleP1.Visible = minecart.Index > 1;
            toggleSolid.Visible = false;
            toggleSolidMods.Visible = false;
            toggleTileMods.Visible = false;
            toggleSolidityFlatMode.Visible = false;
            buttonToggleBoundaries.Visible = false;
            opacityToolStripButton.Visible = false;
            toolStripSeparator12.Visible = false;
            //
            tags.Visible = false;
            editAllLayers.Visible = false;
            buttonDragSolidity.Visible = false;
            buttonEditTemplate.Visible = false;
            toolStripSeparator2.Visible = false;
            toolStripSeparator10.Visible = false;
            toolStripSeparator1.Visible = false;
            toolStripSeparator14.Visible = false;
            toolStripSeparator15.Visible = false;
            toolStripSeparator23.Visible = false;
            //
            toggleCartGrid.Checked = state.TileGrid;
            toggleMushrooms.Checked = state.Mushrooms;
            toggleRails.Checked = state.Rails;
            toggleP1.Checked = state.Priority1;
        //    toggleSolidityFlatMode.Checked = state.SolidityLayerFlatMode;
        }
        public void Reload(Form parent, Tilemap tilemap, Tileset tileset, Overlay overlay, PaletteEditor paletteEditor, TilesetEditor levelsTileset)
        {
            this.pictureBoxLevel.Size = new Size(tilemap.Width_p * zoom, tilemap.Height_p * zoom);
            this.commandStack = new CommandStack(true);
            this.minecart = (MineCart)parent;
            this.tilemap = tilemap;
            this.tileset = tileset;
            this.overlay = overlay;
            this.tilesetEditor = levelsTileset;
            this.paletteEditor = paletteEditor;
            //
            p1Image = null;
            selection = null;
            draggedTiles = null;
            overlay.Select.Clear();
            //
            SetLevelImage();
            //
            toggleP1.Visible = minecart.Index > 1;
            toggleMushrooms.Visible = minecart.Index < 2;
            toggleRails.Visible = minecart.Index < 2;
        }
        private void SetLevelImage()
        {
            int[] pixels = tilemap.Pixels;
            tilemapImage = Do.PixelsToImage(pixels, tilemap.Width_p, tilemap.Height_p);
            pictureBoxLevel.Invalidate();
        }
        private void UpdateCoordLabels()
        {
            int x = mousePosition.X;
            int y = mousePosition.Y;
            this.labelTileCoords.Text = "(x: " + (x / 16) + ", y: " + (y / 16) + ") Tile  |  ";
            this.labelTileCoords.Text += "(x: " +
                System.Convert.ToString(mouseIsometricPosition.X) + ", y: " +
                System.Convert.ToString(mouseIsometricPosition.Y) + ") Isometric  |  ";
            this.labelTileCoords.Text += "(x: " + x + ", y: " + y + ") Pixel";
        }
        private void ToggleTilesets()
        {
            levels.OpenTileset.Checked = !toggleSolidMods.Checked && !toggleSolid.Checked;
            levels.OpenSolidTileset.Checked = toggleSolidMods.Checked || toggleSolid.Checked;
            if (levels.OpenTileset.Checked)
            {
                levels.openSolidTileset_Click(null, null);
                levels.openTileset_Click(null, null);
            }
            else if (levels.OpenSolidTileset.Checked)
            {
                levels.openTileset_Click(null, null);
                levels.openSolidTileset_Click(null, null);
            }
        }
        // editing
        private bool CompareTiles(int x_, int y_, int layer)
        {
            Tilemap tilemap;
            if (state.TileMods && tileMods.MOD != null)
                tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
            else
                tilemap = this.tilemap;
            for (int y = overlay.SelectTS.Y, b = y_; y < overlay.SelectTS.Terminal.Y; y += 16, b += 16)
            {
                for (int x = overlay.SelectTS.X, a = x_; x < overlay.SelectTS.Terminal.X; x += 16, a += 16)
                {
                    if (tilemap.GetTileNum(layer, a, b) != tileset.GetTileNum(layer, x / 16, y / 16))
                        return false;
                }
            }
            return true;
        }
        private void DrawBoundaries(Graphics g)
        {
            Rectangle r = new Rectangle(
                mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 256 * zoom, 224 * zoom);
            Pen insideBorder = new Pen(Color.LightGray, 16);
            Pen edgeBorder = new Pen(Color.Black, 2);
            g.DrawRectangle(insideBorder, r.X - 8, r.Y - 8, r.Width + 16, r.Height + 16);
            g.DrawRectangle(edgeBorder, r.X - 16, r.Y - 16, r.Width + 32, r.Height + 32);
            g.DrawRectangle(edgeBorder, r);
        }
        private Bitmap HightlightedTile(int index)
        {
            int[] pixels = solidity.GetTilePixels(Model.SolidTiles[index]);
            for (int y = 768 - Model.SolidTiles[index].TotalHeight; y < 784; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (pixels[y * 32 + x] == 0) continue;
                    Color color = Color.FromArgb(pixels[y * 32 + x]);
                    int r = color.R;
                    int n = 255;
                    int b = 192;
                    if (index == 0)
                        pixels[y * 32 + x] = Color.FromArgb(96, 0, 0, 0).ToArgb();
                    else
                        pixels[y * 32 + x] = Color.FromArgb(255, r, n, b).ToArgb();
                }
            }
            return Do.PixelsToImage(pixels, 32, 784);
        }
        private void DrawHoverBox(Graphics g)
        {
            int mouseOverSolidTileNum = 0;
            if (state.SolidMods && solidMods.Mods.Count != 0)
                mouseOverSolidTileNum = Bits.GetShort(solidMods.MOD.Tilemap_Bytes, mouseOverSolidTile * 2);
            if (state.SolidityLayer && mouseOverSolidTileNum == 0)  // if mod map empty, check if solidity map empty
                mouseOverSolidTileNum = Bits.GetShort(solidityMap.Tilemap_Bytes, mouseOverSolidTile * 2);
            if ((state.SolidityLayer || state.SolidMods) && mouseOverSolidTileNum != 0)
            {
                Bitmap image = HightlightedTile(mouseOverSolidTileNum);
                Point p = new Point(
                    solidity.TileCoords[mouseOverSolidTile].X * zoom,
                    solidity.TileCoords[mouseOverSolidTile].Y * zoom - (768 * zoom));
                Rectangle rsrc = new Rectangle(0, 0, 32, 784);
                Rectangle rdst = new Rectangle(p.X, p.Y, zoom * 32, zoom * 784);
                g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
            }
            else if (state.SolidityLayer || state.SolidMods || state.NPCs || state.Exits || state.Events || state.Overlaps)
            {
                Point p = new Point(
                    solidity.TileCoords[mouseOverSolidTile].X * zoom,
                    solidity.TileCoords[mouseOverSolidTile].Y * zoom);
                Point[] points = new Point[] { 
                    new Point(p.X + (15 * zoom), p.Y), 
                    new Point(p.X - (1 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y)
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), points, System.Drawing.Drawing2D.FillMode.Winding);
                points = new Point[] { 
                    new Point(p.X + (17 * zoom), p.Y), 
                    new Point(p.X + (33 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y)
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), points, System.Drawing.Drawing2D.FillMode.Winding);
                points = new Point[] { 
                    new Point(p.X + (15 * zoom), p.Y + (16 * zoom)), 
                    new Point(p.X - (1 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (16 * zoom))
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), points, System.Drawing.Drawing2D.FillMode.Winding);
                points = new Point[] { 
                    new Point(p.X + (17 * zoom), p.Y + (16 * zoom)), 
                    new Point(p.X + (33 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (8 * zoom)), 
                    new Point(p.X + (16 * zoom), p.Y + (16 * zoom))
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), points, System.Drawing.Drawing2D.FillMode.Winding);
            }
            else
            {
                Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
                g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
            }
        }
        private void DrawTemplate(Graphics g, int x, int y)
        {
            if (template == null)
            {
                MessageBox.Show("Must select a template to paint to the level.", "LAZYSHELL++");
                return;
            }
            Point tL = new Point(x / 16 * 16, y / 16 * 16);
            Point bR = new Point((x / 16 * 16) + template.Size.Width, (y / 16 * 16) + template.Size.Height);
            if (template.Even != (((tL.X / 16) % 2) == 0))
            {
                tL.X += 16;
                bR.X += 16;
            }
            int[][] tiles = new int[3][];
            tiles[0] = new int[template.Tilemaps[0].Length / 2];
            tiles[1] = new int[template.Tilemaps[1].Length / 2];
            tiles[2] = new int[template.Tilemaps[2].Length];
            for (int i = 0; i < tiles[0].Length; i++)
            {
                tiles[0][i] = Bits.GetShort(template.Tilemaps[0], i * 2);
                tiles[1][i] = Bits.GetShort(template.Tilemaps[1], i * 2);
                tiles[2][i] = template.Tilemaps[2][i];
            }
            commandStack.Push(new TilemapEdit(this.levels, tilemap, 0, tL, bR, tiles, true, true, true));
            commandStack_S.Push(new SolidityEdit(this.levels, this.solidityMap, tL, bR, template.Start, template.Soliditymap));
            commandCount++;
            solidityMap.Image = null;
            tilemap.RedrawTilemaps();
            tileMods.RedrawTilemaps();
            SetLevelImage();
        }
        private void Draw(Graphics g, int x, int y)
        {
            if (state.TileMods && tileMods.MOD != null)
            {
                int x_ = x - (tileMods.X * 16);
                int y_ = y - (tileMods.Y * 16);
                if (!tileMods.WithinBounds(x / 16, y / 16) ||
                    overlay.SelectTS.Width / 16 + (x_ / 16) > tileMods.Width ||
                    overlay.SelectTS.Height / 16 + (y_ / 16) > tileMods.Height)
                    return;
                x -= (tileMods.X * 16);
                y -= (tileMods.Y * 16);
            }
            if (state.SolidMods && !solidMods.WithinBounds(mouseOverSolidTile * 2))
                return;
            Tilemap tilemap;
            if (state.TileMods && tileMods.MOD != null)
                tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
            else
                tilemap = this.tilemap;
            if (!state.SolidityLayer && !state.SolidMods)
            {
                int layer = tilesetEditor.Layer;
                // cancel if no selection in the tileset is made
                if (overlay.SelectTS.Empty)
                    return;
                // cancel if layer doesn't exist
                if (this.tileset.Tilesets_tiles[layer] == null)
                    return;
                // cancel if writing same tile over itself
                if (CompareTiles(x, y, layer))
                    return;
                p1Image = null;
                Point location = new Point(x, y);
                Point terminal = new Point(
                    x + overlay.SelectTS.Width,
                    y + overlay.SelectTS.Height);
                bool transparent = minecart == null || minecart.Index > 1;
                // push command
                CommandStack commandStack;
                if (state.TileMods && tileMods.MOD != null)
                    commandStack = this.commandStack_TM;
                else
                    commandStack = this.commandStack;
                commandStack.Push(
                    new TilemapEdit(levels, tilemap, layer, location, terminal,
                        tilesetEditor.SelectedTiles.Copies, false, transparent, editAllLayers.Checked));
                commandCount++;
                //
                if (state.TileMods && tileMods.MOD != null)
                    tileMods.UpdateTilemaps();
                // draw the tile
                Point p = new Point(x / 16 * 16, y / 16 * 16);
                Bitmap image = Do.PixelsToImage(
                    tilemap.GetPixels(p, overlay.SelectTS.Size),
                    overlay.SelectTS.Width, overlay.SelectTS.Height);
                if (state.TileMods && tileMods.MOD != null)
                {
                    p.X += tileMods.X * 16;
                    p.Y += tileMods.Y * 16;
                }
                p.X *= zoom;
                p.Y *= zoom;
                Rectangle rsrc = new Rectangle(0, 0, image.Width, image.Height);
                Rectangle rdst = new Rectangle(p.X, p.Y, (int)(image.Width * zoom), (int)(image.Height * zoom));
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (!state.BG)
                    pictureBoxLevel.Erase(rdst);
                g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
            }
            else if (state.SolidityLayer || state.SolidMods)
            {
                // cancel if physical tile editor not open
                if (levelsSolidTiles == null)
                    return;
                // cancel if overwriting the same tile over itself
                Tilemap map;
                if (state.SolidMods && solidMods.MOD != null)
                    map = solidMods.MOD;
                else
                    map = solidityMap;
                if (map.GetTileNum(mouseOverSolidTile) == (ushort)levelsSolidTiles.Index)
                    return;
                Point initial = new Point(x, y);
                Point final = new Point(x + 1, y + 1);
                byte[] temp = new byte[0x20C2];
                Bits.SetShort(temp, mouseOverSolidTile * 2, (ushort)levelsSolidTiles.Index);
                CommandStack commandStack_S = state.SolidMods ? this.commandStack_SM : this.commandStack_S;
                commandStack_S.Push(new SolidityEdit(this.levels, map, initial, final, initial, temp));
                commandCount++;
                if (state.SolidMods && solidMods.MOD != null)
                    solidMods.MOD.CopyToTiles();
                solidity.RefreshTilemapImage(map, mouseOverSolidTile * 2);
                map.Image = null;
                p1SolidityImage = null;
                pictureBoxLevel.Invalidate();
            }
        }
        private void Erase(Graphics g, int x, int y)
        {
            if (state.TileMods && tileMods.MOD != null)
            {
                if (!tileMods.WithinBounds(x / 16, y / 16))
                    return;
                x -= (tileMods.X * 16);
                y -= (tileMods.Y * 16);
            }
            if (state.SolidMods && !solidMods.WithinBounds(mouseOverSolidTile * 2))
                return;
            Tilemap tilemap;
            if (state.TileMods && tileMods.MOD != null)
                tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
            else
                tilemap = this.tilemap;
            if (!state.SolidityLayer && !state.SolidMods)
            {
                int layer = tilesetEditor.Layer;
                // cancel if overwriting the same tile over itself
                if (!editAllLayers.Checked && this.tileset.Tilesets_tiles[layer] == null)
                    return;
                if (!editAllLayers.Checked && tilemap.GetTileNum(layer, x, y) == erase)
                    return;
                if (editAllLayers.Checked &&
                    tilemap.GetTileNum(0, x, y) == erase &&
                    tilemap.GetTileNum(1, x, y) == erase &&
                    tilemap.GetTileNum(2, x, y) == erase)
                    return;
                //
                p1Image = null;
                bool transparent = minecart == null || minecart.Index > 1;
                CommandStack commandStack;
                if (state.TileMods && tileMods.MOD != null)
                    commandStack = this.commandStack_TM;
                else
                    commandStack = this.commandStack;
                commandStack.Push(
                    new TilemapEdit(
                        this.levels, tilemap, layer, new Point(x, y), new Point(x + 16, y + 16),
                        new int[][] { new int[] { erase }, new int[] { erase }, new int[] { erase }, new int[] { erase } },
                        false, transparent, editAllLayers.Checked));
                commandCount++;
                if (state.TileMods && tileMods.MOD != null)
                    tileMods.UpdateTilemaps();
                Point p = new Point(x / 16 * 16, y / 16 * 16);
                Bitmap image = Do.PixelsToImage(tilemap.GetPixels(p, new Size(16, 16)), 16, 16);
                if (state.TileMods && tileMods.MOD != null)
                {
                    p.X += tileMods.X * 16;
                    p.Y += tileMods.Y * 16;
                }
                p.X *= zoom; p.Y *= zoom;
                Rectangle rsrc = new Rectangle(0, 0, 16, 16);
                Rectangle rdst = new Rectangle(p.X, p.Y, (int)(16 * zoom), (int)(16 * zoom));
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                if (!state.BG)
                    pictureBoxLevel.Erase(rdst);
                g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
            }
            else if (state.SolidityLayer || state.SolidMods)
            {
                // cancel if overwriting the same tile over itself
                if (solidityMap.GetTileNum(mouseOverSolidTile) == 0)
                    return;
                Point tL = new Point(x, y);
                Point bR = new Point(x + 1, y + 1);
                Tilemap map;
                if (state.SolidMods && solidMods.MOD != null)
                    map = solidMods.MOD;
                else
                    map = solidityMap;
                CommandStack commandStack_S = state.SolidMods ? this.commandStack_SM : this.commandStack_S;
                commandStack_S.Push(new SolidityEdit(this.levels, map, tL, bR, tL, new byte[0x20C2]));
                commandCount++;
                if (state.SolidMods && solidMods.MOD != null)
                    solidMods.MOD.CopyToTiles();
                solidity.RefreshTilemapImage(map, mouseOverSolidTile * 2);
                map.Image = null;
                p1SolidityImage = null;
                pictureBoxLevel.Invalidate();
            }
        }
        private void SelectColor(int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            int layer = tilemap.GetPixelLayer(x, y);
            int tileNum = (y / 16) * (width / 16) + (x / 16);
            int placement = ((x % 16) / 8) + (((y % 16) / 8) * 2);
            Tile tile = this.tileset.Tilesets_tiles[layer][tilemap.GetTileNum(layer, x, y)];
            Subtile subtile = tile.Subtiles[placement];
            int multiplier = layer < 2 ? 16 : 4;
            int index = ((y % 16) % 8) * 8 + ((x % 16) % 8);
            int color = (subtile.Palette * multiplier) + subtile.Colors[index];
            if (color < paletteEditor.StartRow * 16)
                color = paletteEditor.StartRow * 16;
            paletteEditor.CurrentColor = color;
            paletteEditor.Show();
        }
        private void Fill(Graphics g, int x, int y)
        {
            Tilemap tilemap = this.tilemap;
            if (!state.SolidityLayer)
            {
                int layer = tilesetEditor.Layer;
                // cancel if no selection in the tileset is made
                if (overlay.SelectTS.Empty)
                    return;
                // cancel if layer doesn't exist
                if (this.tileset.Tilesets_tiles[layer] == null)
                    return;
                // cancel if writing same tile over itself
                if (CompareTiles(x, y, layer))
                    return;
                p1Image = null;
                // store changes
                int[][] changes = new int[3][];
                if (tilemap.Tilemaps_Bytes[0] != null) changes[0] = new int[(width / 16) * (height / 16)];
                if (tilemap.Tilemaps_Bytes[1] != null) changes[1] = new int[(width / 16) * (height / 16)];
                if (tilemap.Tilemaps_Bytes[2] != null) changes[2] = new int[(width / 16) * (height / 16)];
                for (int l = 0; l < 3; l++)
                {
                    Tile[] tiles = tilemap.Tilemaps_Tiles[l];
                    if (changes[l] == null) continue;
                    if (tiles == null) continue;
                    for (int i = 0; i < changes[l].Length && i < tiles.Length; i++)
                    {
                        if (tiles[i] == null) continue;
                        changes[l][i] = tiles[i].Index;
                    }
                }
                // fill up tiles
                Point location = new Point(0, 0);
                Point terminal = new Point(width, height);
                int[] fillTile = tilesetEditor.SelectedTiles.Copies[layer];
                int tile = tilemap.GetTileNum(layer, x, y);
                int vwidth = overlay.SelectTS.Width / 16;
                int vheight = overlay.SelectTS.Height / 16;
                //
                if ((Control.ModifierKeys & Keys.Control) == 0)
                    Do.Fill(changes, layer, editAllLayers.Checked, tile, fillTile,
                        x / 16, y / 16, width / 16, height / 16, vwidth, vheight, "");
                else
                    // non-contiguous fill
                    for (int d = 0; d < height / 16; d += vheight)
                    {
                        for (int c = 0; c < width / 16; c += vwidth)
                        {
                            for (int b = 0; b < vheight; b++)
                            {
                                if (changes[layer][(d + b) * (width / 16) + c] != tile)
                                    break;
                                for (int a = 0; a < vwidth; a++)
                                {
                                    if (changes[layer][(d + b) * (width / 16) + c + a] != tile)
                                        break;
                                    changes[layer][(d + b) * (width / 16) + c + a] = fillTile[b * vwidth + a];
                                }
                            }
                        }
                    }
                bool transparent = minecart == null || minecart.Index > 1;
                commandStack.Push(
                    new TilemapEdit(levels, tilemap, layer, location, terminal, changes, false, transparent, false));
                commandCount++;
            }
            else
            {
                if (solidityMap.GetTileNum(mouseOverSolidTile) == (ushort)levelsSolidTiles.Index)
                    return;
                ushort tile = (ushort)solidityMap.GetTileNum(mouseOverSolidTile);
                ushort fillTile = (ushort)levelsSolidTiles.Index;
                byte[] changes = Bits.Copy(solidityMap.Tilemap_Bytes);
                if ((Control.ModifierKeys & Keys.Control) == 0)
                    Do.Fill(changes, tile, fillTile, (mousePosition.X + 16) / 32 * 32, (mousePosition.Y + 8) / 16 * 16, width, height, "");
                else
                    for (int i = 0; i < changes.Length; i += 2)
                        if (Bits.GetShort(changes, i) == tile)
                            Bits.SetShort(changes, i, fillTile);
                int index = 0;
                int pushes = 0;
                for (int n = 0; n < 128; n++)
                {
                    for (int m = 0; m < 32; m++)
                    {
                        index = n * 32 + m;
                        Point tL = new Point(
                            solidity.TileCoords[index].X + 16,
                            solidity.TileCoords[index].Y + 8);
                        Point bR = new Point(
                            solidity.TileCoords[index].X + 17,
                            solidity.TileCoords[index].Y + 9);
                        if (state.SolidMods && solidMods.MOD != null)
                            commandStack_S.Push(new SolidityEdit(levels, solidMods.MOD, tL, bR, tL, changes));
                        else
                            commandStack_S.Push(new SolidityEdit(levels, solidityMap, tL, bR, tL, changes));
                        pushes++;
                    }
                }
                this.commandStack_S.Push(pushes);
                solidityMap.Image = null;
                p1SolidityImage = null;
                pictureBoxLevel.Invalidate();
            }
        }
        private void Undo()
        {
            if (!state.SolidityLayer && !state.SolidMods)
            {
                if (!state.TileMods)
                    commandStack.UndoCommand();
                else if (state.TileMods && tileMods.MOD != null)
                    commandStack_TM.UndoCommand();
                p1Image = null;
                SetLevelImage();
                if (level != null)
                    tileMods.ClearImages();
            }
            else
            {
                if (!state.SolidMods)
                {
                    commandStack_S.UndoCommand();
                    solidityMap.Image = null;
                    p1SolidityImage = null;
                }
                if (state.SolidMods && solidMods.MOD != null)
                {
                    commandStack_SM.UndoCommand();
                    solidMods.MOD.CopyToTiles();
                    solidMods.MOD.Pixels = Solidity.Instance.GetTilemapPixels(solidMods.MOD);
                    solidMods.MOD.Image = null;
                }
                pictureBoxLevel.Invalidate();
            }
        }
        private void Redo()
        {
            if (!state.SolidityLayer && !state.SolidMods)
            {
                if (!state.TileMods)
                    commandStack.RedoCommand();
                else if (state.TileMods && tileMods.MOD != null)
                    commandStack_TM.RedoCommand();
                p1Image = null;
                SetLevelImage();
                if (level != null)
                    tileMods.ClearImages();
            }
            else
            {
                if (!state.SolidMods)
                {
                    commandStack_S.RedoCommand();
                    solidityMap.Image = null;
                    p1SolidityImage = null;
                }
                else if (state.SolidMods && solidMods.MOD != null)
                {
                    commandStack_SM.RedoCommand();
                    solidMods.MOD.CopyToTiles();
                    solidMods.MOD.Pixels = Solidity.Instance.GetTilemapPixels(solidMods.MOD);
                    solidMods.MOD.Image = null;
                }
                pictureBoxLevel.Invalidate();
            }
        }
        private void Cut()
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
                return;
            if (state.SolidityLayer || state.SolidMods)
                return;
            Copy();
            Delete();
            if (commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void Copy()
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
                return;
            if (state.SolidityLayer || state.SolidMods)
                return;
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            int layer = tilesetEditor.Layer;
            Tilemap tilemap;
            Point location = overlay.Select.Location;
            if (state.TileMods && tileMods.MOD != null)
            {
                if (!tileMods.WithinBounds(location.X / 16, location.Y / 16))
                    return;
                tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
                location.X -= tileMods.X * 16;
                location.Y -= tileMods.Y * 16;
            }
            else
                tilemap = this.tilemap;
            if (editAllLayers.Checked)
                selection = Do.PixelsToImage(
                    tilemap.GetPixels(location, overlay.Select.Size),
                    overlay.Select.Width, overlay.Select.Height);
            else
                selection = Do.PixelsToImage(
                    tilemap.GetPixels(layer, location, overlay.Select.Size),
                    overlay.Select.Width, overlay.Select.Height);
            int[][] copiedTiles = new int[3][];
            this.copiedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
            for (int l = 0; l < 3; l++)
            {
                copiedTiles[l] = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
                for (int y = 0; y < overlay.Select.Height / 16; y++)
                {
                    for (int x = 0; x < overlay.Select.Width / 16; x++)
                    {
                        int tileX = location.X + (x * 16);
                        int tileY = location.Y + (y * 16);
                        copiedTiles[l][y * (overlay.Select.Width / 16) + x] = tilemap.GetTileNum(l, tileX, tileY);
                    }
                }
            }
            this.copiedTiles.Copies = copiedTiles;
        }
        /// <summary>
        /// Start dragging a current selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
                return;
            if (!state.SolidityLayer && !state.SolidMods)
            {
                int layer = tilesetEditor.Layer;
                Point location = overlay.Select.Location;
                Tilemap tilemap;
                if (state.TileMods && tileMods.MOD != null)
                {
                    if (!tileMods.WithinBounds(location.X / 16, location.Y / 16))
                        return;
                    tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
                    location.X -= (tileMods.X * 16);
                    location.Y -= (tileMods.Y * 16);
                }
                else
                {
                    location = overlay.Select.Location;
                    tilemap = this.tilemap;
                }
                if (editAllLayers.Checked)
                    selection = Do.PixelsToImage(
                        tilemap.GetPixels(location, overlay.Select.Size),
                        overlay.Select.Width, overlay.Select.Height);
                else
                    selection = Do.PixelsToImage(
                        tilemap.GetPixels(layer, location, overlay.Select.Size),
                        overlay.Select.Width, overlay.Select.Height);
                int[][] copiedTiles = new int[3][];
                this.draggedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
                for (int l = 0; l < 3; l++)
                {
                    copiedTiles[l] = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
                    for (int y = 0; y < overlay.Select.Height / 16; y++)
                    {
                        for (int x = 0; x < overlay.Select.Width / 16; x++)
                        {
                            int tileX = overlay.Select.X + (x * 16);
                            int tileY = overlay.Select.Y + (y * 16);
                            if (state.TileMods && tileMods.MOD != null)
                            {
                                tileX -= tileMods.X * 16;
                                tileY -= tileMods.Y * 16;
                            }
                            copiedTiles[l][y * (overlay.Select.Width / 16) + x] = tilemap.GetTileNum(l, tileX, tileY);
                        }
                    }
                }
                this.draggedTiles.Copies = copiedTiles;
            }
            Delete();
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (state.SolidityLayer || state.SolidMods)
                return;
            if (buffer == null)
                return;
            if (!buttonEditSelect.Checked)
                buttonEditSelect.PerformClick();
            state.Move = true;
            // now dragging a new selection
            draggedTiles = buffer;
            overlay.Select.Refresh(16, location, buffer.Size, Picture);
            pictureBoxLevel.Invalidate();
            defloating = false;
            //levels.AlertLabel();
        }
        /// <summary>
        /// Defloat either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        private void Defloat(CopyBuffer buffer)
        {
            if (state.SolidityLayer || state.SolidMods)
                return;
            if (buffer == null)
                return;
            if (overlay.Select.Empty)
                return;
            Point location = new Point();
            location.X = overlay.Select.X / 16 * 16;
            location.Y = overlay.Select.Y / 16 * 16;
            int layer = tilesetEditor.Layer;
            Tilemap tilemap;
            if (state.TileMods && tileMods.MOD != null)
            {
                if (!tileMods.WithinBounds(location.X / 16, location.Y / 16))
                    return;
                location.X -= tileMods.X * 16;
                location.Y -= tileMods.Y * 16;
                tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
            }
            else
                tilemap = this.tilemap;
            Point terminal = new Point(location.X + buffer.Width, location.Y + buffer.Height);
            bool transparent = minecart == null || minecart.Index > 1;
            CommandStack commandStack;
            if (state.TileMods && tileMods.MOD != null)
                commandStack = this.commandStack_TM;
            else
                commandStack = this.commandStack;
            commandStack.Push(
                new TilemapEdit(levels, tilemap, layer, location, terminal, buffer.Copies, true, transparent, editAllLayers.Checked));
            commandStack.Push(commandCount + 1);
            commandCount = 0;
            p1Image = null;
            SetLevelImage();
            if (level != null)
                tileMods.ClearImages();
            defloating = true;
        }
        /// <summary>
        /// Defloats pasted tiles and clears the selection
        /// </summary>
        private void Defloat()
        {
            if (copiedTiles != null && !defloating)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            state.Move = false;
            overlay.Select.Clear();
            Cursor.Position = Cursor.Position;
        }
        private void Delete()
        {
            if (overlay.Select.Empty)
                return;
            if (overlay.Select.Size == new Size(0, 0))
                return;
            if (!state.SolidityLayer && !state.SolidMods)
            {
                int layer = tilesetEditor.Layer;
                if (this.tileset.Tilesets_tiles[layer] == null)
                    return;
                if (overlay.Select.Empty)
                    return;
                Point location = overlay.Select.Location;
                Point terminal = overlay.Select.Terminal;
                int[][] changes = new int[][]{
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height],
                    new int[overlay.Select.Width * overlay.Select.Height]};
                if (erase != 0)
                    for (int i = 0; i < 4; i++)
                        Bits.Fill(changes[i], erase);
                // Verify layer before creating command
                Tilemap tilemap;
                if (state.TileMods && tileMods.MOD != null)
                {
                    if (!tileMods.WithinBounds(location.X / 16, location.Y / 16))
                        return;
                    tilemap = levels.TileModsFieldTree.SelectedNode.Parent == null ? tileMods.TilemapA : tileMods.TilemapB;
                    location.X -= (tileMods.X * 16);
                    location.Y -= (tileMods.Y * 16);
                    terminal.X -= (tileMods.X * 16);
                    terminal.Y -= (tileMods.Y * 16);
                }
                else
                    tilemap = this.tilemap;
                bool transparent = minecart == null || minecart.Index > 1;
                CommandStack commandStack;
                if (state.TileMods && tileMods.MOD != null)
                    commandStack = this.commandStack_TM;
                else
                    commandStack = this.commandStack;
                commandStack.Push(
                    new TilemapEdit(
                        levels, tilemap, layer, location, terminal,
                        changes, false, transparent, editAllLayers.Checked));
                commandCount++;
                p1Image = null;
                SetLevelImage();
                if (level != null)
                    tileMods.ClearImages();
            }
            else
            {
                int index = 0;
                int pushes = 0;
                Tilemap map;
                CommandStack commandStack_S;
                if (state.SolidMods && solidMods.MOD != null)
                {
                    map = solidMods.MOD;
                    commandStack_S = this.commandStack_SM;
                }
                else
                {
                    map = solidityMap;
                    commandStack_S = this.commandStack_S;
                }
                for (int y = overlay.Select.Y; y < overlay.Select.Y + overlay.Select.Height; y++)
                {
                    for (int x = overlay.Select.X; x < overlay.Select.X + overlay.Select.Width; x++)
                    {
                        if (index == solidity.PixelTiles[y * width + x])
                            continue;
                        index = solidity.PixelTiles[y * width + x];
                        if (map.GetTileNum(index) == 0)
                            continue;
                        Point tL = new Point(
                            solidity.TileCoords[index].X + 16,
                            solidity.TileCoords[index].Y + 8);
                        Point bR = new Point(
                            solidity.TileCoords[index].X + 17,
                            solidity.TileCoords[index].Y + 9);
                        commandStack_S.Push(new SolidityEdit(levels, map, tL, bR, tL, new byte[0x20C2]));
                        pushes++;
                    }
                }
                commandStack_S.Push(pushes);
                if (!state.SolidMods)
                    p1SolidityImage = null;
                else
                    map.Pixels = Solidity.Instance.GetTilemapPixels(map);
                map.Image = null;
                pictureBoxLevel.Invalidate();
            }
        }
        //
        #endregion
        #region Event Handlers
        // main
        private void pictureBoxLevel_Paint(object sender, PaintEventArgs e)
        {
            RectangleF clone = e.ClipRectangle;
            SizeF remainder = new SizeF((int)(clone.Width % zoom), (int)(clone.Height % zoom));
            clone.Location = new PointF((int)(clone.X / zoom), (int)(clone.Y / zoom));
            clone.Size = new SizeF((int)(clone.Width / zoom), (int)(clone.Height / zoom));
            clone.Width += (int)(remainder.Width * zoom) + 1;
            clone.Height += (int)(remainder.Height * zoom) + 1;
            RectangleF source, dest;
            float[][] matrixItems ={ 
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, ((float)overlayOpacity.Value - 1 ) / 100, 0}, 
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix cm = new ColorMatrix(matrixItems);
            ImageAttributes ia = new ImageAttributes();
        //    if (overlayOpacity.Value < 100)
                ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Rectangle rdst = new Rectangle(0, 0, zoom * width, zoom * height);
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (tilemapImage != null)
            {
                clone.Width = Math.Min(tilemapImage.Width, clone.X + clone.Width) - clone.X;
                clone.Height = Math.Min(tilemapImage.Height, clone.Y + clone.Height) - clone.Y;
                source = clone; source.Location = new PointF(0, 0);
                dest = new RectangleF((int)(clone.X * zoom), (int)(clone.Y * zoom), (int)(clone.Width * zoom), (int)(clone.Height * zoom));
                e.Graphics.DrawImage(tilemapImage.Clone(clone, PixelFormat.DontCare), dest, source, GraphicsUnit.Pixel);
            }
            if (state.TileMods && tileMods.MOD != null)
            {
                foreach (LevelTileMods.Mod mod in tileMods.Mods)
                {
                    if (mod.TilemapA == null)
                        mod.TilemapA = new LevelTilemap(level, tileset, mod, false);
                    if (mod.Set && mod.TilemapB == null)
                        mod.TilemapB = new LevelTilemap(level, tileset, mod, true);
                }
                overlay.DrawLevelTileMods(tileMods, e.Graphics, ia, zoom);
            }
            if (state.Move && selection != null)
            {
                Rectangle rsrc = new Rectangle(0, 0, overlay.Select.Width, overlay.Select.Height);
                rdst = new Rectangle(
                    overlay.Select.X * zoom, overlay.Select.Y * zoom,
                    rsrc.Width * zoom, rsrc.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), rdst, rsrc, GraphicsUnit.Pixel);
                Do.DrawString(e.Graphics, new Point(rdst.X, rdst.Y + rdst.Height),
                    "click/drag", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
            }
            if (state.Priority1 && !state.SolidityLayer)
            {
                cm.Matrix33 = 0.50F;
                ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                if (p1Image == null)
                    p1Image = Do.PixelsToImage(tilemap.GetPriority1Pixels(), width, height);
                e.Graphics.DrawImage(p1Image, rdst, 0, 0, width, height, GraphicsUnit.Pixel, ia);
            }
            if (state.SolidityLayer)
            {
                //breaks level editting; do not refresh directly
                //solidityMap.Image = Do.PixelsToImage(solidity.GetTilemapPixels(solidityMap, false, state.SolidityLayerFlatMode), 1024, 1024);
                Bitmap solidityMapImage = solidityMap.Image;
                if (state.SolidityLayerFlatMode)
                    solidityMapImage = Do.PixelsToImage(solidity.GetTilemapPixels(solidityMap, false, state.SolidityLayerFlatMode), 1024, 1024);

                if (overlayOpacity.Value < 99)
                    e.Graphics.DrawImage(solidityMapImage, rdst, 0, 0, width, height, GraphicsUnit.Pixel, ia);
                else
                    e.Graphics.DrawImage(solidityMapImage, rdst, 0, 0, width, height, GraphicsUnit.Pixel);

                if (state.Priority1)
                {
                    p1SolidityImage = Do.PixelsToImage(solidity.GetTilemapPixels(solidityMap, true, state.SolidityLayerFlatMode), 1024, 1024);
                    e.Graphics.DrawImage(p1SolidityImage, rdst, 0, 0, width, height, GraphicsUnit.Pixel, ia);
                }
                if (selsolidt != null)
                {
                    Rectangle rsrc = new Rectangle(0, 0, selsolidt.Width, selsolidt.Height);
                    rdst = new Rectangle(
                        selsolidt_location.X * zoom, selsolidt_location.Y * zoom,
                        rsrc.Width * zoom, rsrc.Height * zoom);
                    e.Graphics.DrawImage(selsolidt, rdst, rsrc, GraphicsUnit.Pixel);
                }
            }
            if (state.SolidMods)
            {
                overlay.DrawLevelSolidMods(solidMods, solidTiles, e.Graphics, rdst, ia, zoom);
                overlay.DrawLevelSolidMods(solidMods, e.Graphics, zoom);
            }
            if (state.Exits)
            {
                overlay.DrawLevelExits(exits, e.Graphics, zoom);
                if (tags.Checked)
                    overlay.DrawLevelExitTags(exits, e.Graphics, zoom);
            }
            if (state.Events)
            {
                overlay.DrawLevelEvents(events, e.Graphics, zoom);
                if (tags.Checked)
                    overlay.DrawLevelEventTags(events, e.Graphics, zoom);
            }
            if (state.NPCs)
            {
                if (overlay.NPCImages == null)
                    overlay.DrawLevelNPCs(npcs, Model.NPCProperties);
                overlay.DrawLevelNPCs(npcs, e.Graphics, zoom);
                if (tags.Checked)
                    overlay.DrawLevelNPCTags(npcs, e.Graphics, zoom);
            }
            if (state.Overlaps)
                overlay.DrawLevelOverlaps(overlaps, Model.OverlapTileset, e.Graphics, zoom);
            if (state.Mushrooms)
            {
                if (minecart.Index == 0)
                    overlay.DrawLevelMushrooms(minecart.MinecartData, minecart.MinecartData.M7ObjectsA, e.Graphics, zoom);
                else if (minecart.Index == 1)
                    overlay.DrawLevelMushrooms(minecart.MinecartData, minecart.MinecartData.M7ObjectsB, e.Graphics, zoom);
            }
            if (state.Rails && minecart.Index < 2)
                overlay.DrawRailProperties(tilemap.Tilemap_Bytes, 64, 64, e.Graphics, zoom);
            if (!state.Dropper && mouseEnter)
                DrawHoverBox(e.Graphics);
            if (state.TileGrid)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxLevel.Size, new Size(16, 16), zoom, true);
            if (state.IsometricGrid)
                overlay.DrawIsometricGrid(e.Graphics, Color.Gray, pictureBoxLevel.Size, new Size(16, 16), zoom);
            if (state.Mask)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
                overlay.DrawLevelMask(e.Graphics, new Point(layer.MaskHighX, layer.MaskHighY), new Point(layer.MaskLowX, layer.MaskLowY), zoom);
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            }
            if (state.ShowBoundaries && mouseEnter)
                overlay.DrawBoundaries(e.Graphics, mousePosition, zoom);
            if (state.Select && overlay.Select != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                overlay.Select.DrawSelectionBox(e.Graphics, zoom);
            }
        }
        private void pictureBoxLevel_MouseDown(object sender, MouseEventArgs e)
        {
            // in case the tileset selection was dragged
            if (tilesetEditor.DraggedTiles != null)
                tilesetEditor.Defloat(tilesetEditor.DraggedTiles);
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            mouseDownObject = null;
            #region Zooming
            autoScrollPos.X = Math.Abs(panelLevelPicture.AutoScrollPosition.X);
            autoScrollPos.Y = Math.Abs(panelLevelPicture.AutoScrollPosition.Y);
            if ((buttonZoomIn.Checked && e.Button == MouseButtons.Left) || (buttonZoomOut.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxLevel.ZoomIn(e.X, e.Y);
                return;
            }
            else if ((buttonZoomOut.Checked && e.Button == MouseButtons.Left) || (buttonZoomIn.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxLevel.ZoomOut(e.X, e.Y);
                return;
            }
            #endregion
            if (e.Button == MouseButtons.Right)
                return;
            #region Drawing, Erasing, Selecting
            // if moving an object and outside of it, paste it
            if (state.Move && mouseOverObject != "selection")
            {
                // if copied tiles were pasted and not dragging a non-copied selection
                if (copiedTiles != null && draggedTiles == null)
                    Defloat(copiedTiles);
                if (draggedTiles != null)
                {
                    Defloat(draggedTiles);
                    draggedTiles = null;
                }
                state.Move = false;
            }
            if (state.Select)
            {
                //panelLevelPicture.Focus();
                // if we're not inside a current selection to move it, create a new selection
                if (mouseOverObject != "selection")
                {
                    if (!state.TileMods)
                        overlay.Select.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, Picture);
                    else if (tileMods.WithinBounds(x / 16, y / 16))
                        overlay.Select.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, Picture);
                    else
                        overlay.Select.Clear();
                }
                // otherwise, start dragging current selection
                else if (mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.Select.MousePosition(x, y);
                    if (!state.Move)    // only do this if the current selection has not been initially moved
                    {
                        state.Move = true;
                        Drag();
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (mouseOverObject != "solid tile" && Control.ModifierKeys == Keys.Control)
                {
                    findInTileset.PerformClick();
                    return;
                }
                if (state.Dropper)
                {
                    SelectColor(x, y);
                    return;
                }
                if (state.Template)
                {
                    DrawTemplate(pictureBoxLevel.CreateGraphics(), x, y);
                    panelLevelPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Draw)
                {
                    Draw(pictureBoxLevel.CreateGraphics(), x, y);
                    panelLevelPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Erase)
                {
                    Erase(pictureBoxLevel.CreateGraphics(), x, y);
                    panelLevelPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
                if (state.Fill)
                {
                    Fill(pictureBoxLevel.CreateGraphics(), x, y);
                    panelLevelPicture.AutoScrollPosition = autoScrollPos;
                    return;
                }
            }
            #endregion
            #region Object Selection
            if (!state.Template && !state.Draw && !state.Select && !state.Erase && e.Button == MouseButtons.Left)
            {
                if (state.Mask && mouseOverObject != null && mouseOverObject.StartsWith("mask"))
                {
                    levels.TabControl.SelectedIndex = 1;
                    mouseDownObject = mouseOverObject;
                }
                if (state.Exits && mouseOverObject == "exit")
                {
                    levels.TabControl.SelectedIndex = 3;
                    mouseDownObject = "exit";
                    mouseDownExitField = mouseOverExitField;
                    exits.CurrentExit = mouseDownExitField;
                    exits.SelectedExit = mouseDownExitField;
                    levels.ExitsFieldTree.SelectedNode = levels.ExitsFieldTree.Nodes[exits.CurrentExit];
                }
                if (state.Events && mouseOverObject == "event" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 3;
                    mouseDownObject = "event";
                    mouseDownEventField = mouseOverEventField;
                    events.CurrentEvent = mouseDownEventField;
                    events.SelectedEvent = mouseDownEventField;
                    levels.EventsFieldTree.SelectedNode = levels.EventsFieldTree.Nodes[events.CurrentEvent];
                }
                if (state.NPCs && mouseOverObject == "npc" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 2;
                    mouseDownObject = "npc";
                    mouseDownNPC = mouseOverNPC;
                    npcs.CurrentNPC = mouseDownNPC;
                    npcs.SelectedNPC = mouseDownNPC;
                    npcs.IsCloneSelected = false;
                    levels.NpcObjectTree.SelectedNode = levels.NpcObjectTree.Nodes[npcs.CurrentNPC];
                }
                if (state.NPCs && mouseOverObject == "npc clone" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 2;
                    mouseDownObject = "npc clone";
                    mouseDownNPC = mouseOverNPC;
                    mouseDownNPCClone = mouseOverNPCClone;
                    npcs.CurrentNPC = mouseDownNPC;
                    npcs.SelectedNPC = mouseDownNPC;
                    npcs.CurrentClone = mouseDownNPCClone;
                    npcs.SelectedClone = mouseDownNPCClone;
                    npcs.IsCloneSelected = true;
                    levels.NpcObjectTree.SelectedNode = levels.NpcObjectTree.Nodes[npcs.CurrentNPC].Nodes[npcs.CurrentClone];
                }
                if (state.Overlaps && mouseOverObject == "overlap" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 4;
                    mouseDownObject = "overlap";
                    mouseDownOverlap = mouseOverOverlap;
                    overlaps.CurrentOverlap = mouseDownOverlap;
                    overlaps.SelectedOverlap = mouseDownOverlap;
                    levels.OverlapFieldTree.SelectedNode = levels.OverlapFieldTree.Nodes[overlaps.CurrentOverlap];
                }
                if (state.TileMods && mouseOverObject == "tilemod" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 5;
                    mouseDownObject = "tilemod";
                    mouseDownTileMod = mouseOverTileMod;
                    tileMods.CurrentMod = mouseDownTileMod;
                    tileMods.SelectedMod = mouseDownTileMod;
                    levels.TileModsFieldTree.SelectedNode = levels.TileModsFieldTree.Nodes[tileMods.CurrentMod];
                    mouseDownPosition = new Point((x / 16) - tileMods.X, (y / 16) - tileMods.Y);
                }
                if (state.SolidMods && mouseOverObject == "solidmod" && mouseDownObject == null)
                {
                    levels.TabControl.SelectedIndex = 5;
                    mouseDownObject = "solidmod";
                    mouseDownSolidMod = mouseOverSolidMod;
                    solidMods.CurrentMod = mouseDownSolidMod;
                    solidMods.SelectedMod = mouseDownSolidMod;
                    levels.SolidModsFieldTree.SelectedNode = levels.SolidModsFieldTree.Nodes[solidMods.CurrentMod];
                }
                if (state.SolidityLayer && mouseOverObject == "solid tile" && mouseDownObject == null)
                {
                    mouseDownObject = "solid tile";
                    mouseDownSolidTile = mouseOverSolidTile;
                    mouseDownSolidTileIndex = mouseDownSolidTileNum;
                    selsolidt = HightlightedTile(mouseDownSolidTileNum);
                    selsolidt_location = solidity.TileCoords[mouseDownSolidTile];
                    selsolidt_location.Y -= 768;
                    if ((Control.ModifierKeys & Keys.Control) == 0)
                    {
                        Point tL = new Point(x, y);
                        Point bR = new Point(x + 1, y + 1);
                        Tilemap map;
                        if (state.SolidMods && solidMods.MOD != null)
                            map = solidMods.MOD;
                        else
                            map = solidityMap;
                        commandStack_S.Push(new SolidityEdit(this.levels, map, tL, bR, tL, new byte[0x20C2]));
                        if (state.SolidMods && solidMods.MOD != null)
                            solidMods.MOD.CopyToTiles();
                        solidity.RefreshTilemapImage(map, mouseDownSolidTile * 2);
                        map.Image = null;
                        p1SolidityImage = null;
                    }
                }
                if (state.Mushrooms && mouseOverObject == "mushroom" && mouseDownObject == null)
                {
                    mouseDownObject = "mushroom";
                    mouseDownMushroom = mouseOverMushroom;
                }
            }
            #endregion
            panelLevelPicture.AutoScrollPosition = autoScrollPos;
            pictureBoxLevel.Invalidate();
        }
        private void pictureBoxLevel_MouseUp(object sender, MouseEventArgs e)
        {
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            // dragging a solid tile
            if (mouseDownObject == "solid tile")
            {
                selsolidt = null;
                Point initial = new Point(x, y);
                Point final = new Point(x + 1, y + 1);
                byte[] temp = new byte[0x20C2];
                Bits.SetShort(temp, mouseOverSolidTile * 2, mouseDownSolidTileIndex);
                Tilemap map;
                if (state.SolidMods && solidMods.MOD != null)
                    map = (Tilemap)solidMods.MOD;
                else
                    map = solidityMap;
                commandStack_S.Push(new SolidityEdit(this.levels, map, initial, final, initial, temp));
                if (state.SolidMods && solidMods.MOD != null)
                    solidMods.MOD.CopyToTiles();
                solidity.RefreshTilemapImage(map, mouseOverSolidTile * 2);
                map.Image = null;
                p1SolidityImage = null;
                pictureBoxLevel.Invalidate();
                this.commandStack_S.Push(2); // two to undo both deletion and drop
            }
            // drawing 1 or more tiles
            else if (!state.Move && !state.SolidityLayer && !state.SolidMods && commandCount > 0)
            {
                if (!state.TileMods)
                    this.commandStack.Push(commandCount);
                else
                    this.commandStack_TM.Push(commandCount);
                commandCount = 0;
            }
            // drawing 1 or more solid tiles
            else if (!state.Move && commandCount > 0)
            {
                if (!state.SolidMods)
                    this.commandStack_S.Push(commandCount);
                else
                    this.commandStack_SM.Push(commandCount);
                commandCount = 0;
            }
            mouseDownExitField = -1;
            mouseDownEventField = -1;
            mouseDownNPC = -1;
            mouseDownNPCClone = -1;
            mouseDownOverlap = -1;
            mouseDownSolidTile = 0;
            mouseDownSolidTileIndex = -1;
            mouseDownMushroom = -1;
            mouseDownObject = null;
            if (state.Draw || state.Erase || state.Fill)
            {
                if (!state.SolidityLayer && !state.SolidMods)
                {
                    SetLevelImage();
                    if (level != null)
                        tileMods.ClearImages();
                }
                else
                    pictureBoxLevel.Invalidate();
            }
            pictureBoxLevel.Focus(form);
            if (levels != null)
                levels.Modified = true;
            else
                minecart.Modified = true;
        }
        private void pictureBoxLevel_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            // must first check if within same bounds as last call of MouseMove event
            if (state.SolidityLayer || state.SolidMods)
                mouseWithinSameBounds = mouseOverSolidTile ==
                    solidity.PixelTiles[Math.Min(y * width + x, (width - 1) * (height - 1))];
            else
                mouseWithinSameBounds = mouseOverTile == (y / 16 * 64) + (x / 16);
            // now set the properties
            mousePosition = new Point(x, y);
            mouseLastIsometricPosition = new Point(mouseIsometricPosition.X, mouseIsometricPosition.Y);
            mouseIsometricPosition.X = solidity.PixelCoords[Math.Min(y * width + x, (width - 1) * (height - 1))].X;
            mouseIsometricPosition.Y = solidity.PixelCoords[Math.Min(y * width + x, (width - 1) * (height - 1))].Y;
            mouseOverTile = (y / 16 * 64) + (x / 16);
            mouseOverSolidTile = solidity.PixelTiles[Math.Min(y * width + x, (width - 1) * (height - 1))];
            mouseOverObject = null;
            UpdateCoordLabels();
            #region Highlight in tileset
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                int index = 0;
                if (!state.SolidityLayer)
                {
                    int layer_ = 0;
                    bool ignoreTransparent = minecart == null;
                    index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, ignoreTransparent);
                    if (index == 0)
                    {
                        layer_++;
                        index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, ignoreTransparent);
                    }
                    if (index == 0)
                    {
                        layer_++;
                        index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, ignoreTransparent);
                    }
                    tilesetEditor.Layer = layer_;
                    tilesetEditor.mousePosition = new Point(index % 16 * 16, index / 16 * 16);
                    tilesetEditor.PictureBox.Invalidate();
                }
                else if (state.SolidityLayer)
                {
                    index = solidityMap.GetTileNum(solidity.PixelTiles[mousePosition.Y * width + mousePosition.X]);
                    if (!levels.OpenSolidTileset.Checked)
                        levels.OpenSolidTileset.PerformClick();
                    levelsSolidTiles.Index = index;
                }
                else if (state.SolidMods && solidMods.MOD != null)
                {
                    index = solidMods.MOD.GetTileNum(solidity.PixelTiles[mousePosition.Y * width + mousePosition.X]);
                    if (!levels.OpenSolidTileset.Checked)
                        levels.OpenSolidTileset.PerformClick();
                    levelsSolidTiles.Index = index;
                }
            }
            #endregion
            #region Zooming
            // if either zoom button is checked, don't do anything else
            if (buttonZoomIn.Checked || buttonZoomOut.Checked)
            {
                pictureBoxLevel.Invalidate();
                return;
            }
            #endregion
            #region Dropper
            // show zoom box for selecting colors
            if (state.Dropper)
                return;
            #endregion
            #region Drawing, erasing, selecting
            if (state.Select)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.Select != null)
                {
                    if (!state.TileMods)
                    {
                        // cancel if within same bounds as last call
                        if (overlay.Select.Final == new Point(x + 16, y + 16))
                            return;
                        // otherwise, set the lower right edge of the selection
                        overlay.Select.Final = new Point(
                            Math.Min(x + 16, pictureBoxLevel.Width),
                            Math.Min(y + 16, pictureBoxLevel.Height));
                    }
                    else
                    {
                        // cancel if within same bounds as last call
                        if (overlay.Select.Final == new Point(x + 16, y + 16))
                            return;
                        // otherwise, set the lower right edge of the selection
                        overlay.Select.Final = new Point(
                            Math.Max(tileMods.X * 16, Math.Min(x + 16, (tileMods.X + tileMods.Width) * 16)),
                            Math.Max(tileMods.Y * 16, Math.Min(y + 16, (tileMods.Y + tileMods.Height) * 16)));
                    }
                }
                // if dragging the current selection
                else if (!state.SolidityLayer && e.Button == MouseButtons.Left && mouseDownObject == "selection")
                {
                    if (!state.TileMods)
                        overlay.Select.Location = new Point(x / 16 * 16 - mouseDownPosition.X, y / 16 * 16 - mouseDownPosition.Y);
                    else
                    {
                        // the maximum coordinates, determined both by the selection and tile mod sizes
                        int maxX = (tileMods.X + tileMods.Width) * 16 - overlay.Select.Width;
                        int maxY = (tileMods.Y + tileMods.Height) * 16 - overlay.Select.Height;
                        overlay.Select.Location = new Point(
                            Math.Min(maxX, Math.Max(tileMods.X * 16, x / 16 * 16 - mouseDownPosition.X)),
                            Math.Min(maxY, Math.Max(tileMods.Y * 16, y / 16 * 16 - mouseDownPosition.Y)));
                    }
                }
                // if mouse not clicked and within the current selection
                else if (!state.SolidityLayer && e.Button == MouseButtons.None && overlay.Select != null && overlay.Select.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxLevel.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxLevel.Cursor = Cursors.Cross;
                pictureBoxLevel.Invalidate();
                return;
            }
            if (!state.SolidityLayer && !state.SolidMods)
            {
                if (state.Draw && e.Button == MouseButtons.Left)
                {
                    Draw(pictureBoxLevel.CreateGraphics(), x, y);
                    return;
                }
                else if (state.Erase && e.Button == MouseButtons.Left)
                {
                    Erase(pictureBoxLevel.CreateGraphics(), x, y);
                    return;
                }
            }
            else if (state.SolidityLayer || state.SolidMods)
            {
                if (!mouseWithinSameBounds)
                {
                    if (state.Draw && e.Button == MouseButtons.Left)
                        Draw(pictureBoxLevel.CreateGraphics(), x, y);
                    if (state.Erase && e.Button == MouseButtons.Left)
                        Erase(pictureBoxLevel.CreateGraphics(), x, y);
                }
            }
            #endregion
            #region Objects
            if (!state.Template && !state.Draw && !state.Select && !state.Erase && !state.Dropper && !state.Fill)
            {
                #region Check if dragging a field
                if (mouseDownObject != null && e.Button == MouseButtons.Left)  // if dragging a field
                {
                    //if (Math.Abs(mouseIsometricPosition.X - mouseLastIsometricPosition.X) > 0 ||
                    //    Math.Abs(mouseIsometricPosition.Y - mouseLastIsometricPosition.Y) > 0)
                    //    return;
                    if (mouseDownObject == "maskNW")
                    {
                        levels.LayerMaskLowX.Value = Math.Min(mouseTilePosition.X, layer.MaskHighX);
                        levels.LayerMaskLowY.Value = Math.Min(mouseTilePosition.Y, layer.MaskHighY);
                    }
                    if (mouseDownObject == "maskNE")
                    {
                        levels.LayerMaskHighX.Value = Math.Max(mouseTilePosition.X, layer.MaskLowX);
                        levels.LayerMaskLowY.Value = Math.Min(mouseTilePosition.Y, layer.MaskHighY);
                    }
                    if (mouseDownObject == "maskSW")
                    {
                        levels.LayerMaskLowX.Value = Math.Min(mouseTilePosition.X, layer.MaskHighX);
                        levels.LayerMaskHighY.Value = Math.Max(mouseTilePosition.Y, layer.MaskLowY);
                    }
                    if (mouseDownObject == "maskSE")
                    {
                        levels.LayerMaskHighX.Value = Math.Max(mouseTilePosition.X, layer.MaskLowX);
                        levels.LayerMaskHighY.Value = Math.Max(mouseTilePosition.Y, layer.MaskLowY);
                    }
                    if (mouseDownObject == "maskW")
                        levels.LayerMaskLowX.Value = Math.Min(mouseTilePosition.X, layer.MaskHighX);
                    if (mouseDownObject == "maskE")
                        levels.LayerMaskHighX.Value = Math.Max(mouseTilePosition.X, layer.MaskLowX);
                    if (mouseDownObject == "maskN")
                        levels.LayerMaskLowY.Value = Math.Min(mouseTilePosition.Y, layer.MaskHighY);
                    if (mouseDownObject == "maskS")
                        levels.LayerMaskHighY.Value = Math.Max(mouseTilePosition.Y, layer.MaskLowY);
                    if (mouseDownObject == "exit")
                    {
                        if (levels.ExitX.Value != mouseIsometricPosition.X &&
                            levels.ExitY.Value != mouseIsometricPosition.Y)
                            levels.Refreshing = true;
                        levels.ExitX.Value = mouseIsometricPosition.X;
                        levels.Refreshing = false;
                        levels.ExitY.Value = mouseIsometricPosition.Y;
                    }
                    if (mouseDownObject == "event")
                    {
                        if (levels.EventX.Value != mouseIsometricPosition.X &&
                            levels.EventY.Value != mouseIsometricPosition.Y)
                            levels.Refreshing = true;
                        levels.EventX.Value = mouseIsometricPosition.X;
                        levels.Refreshing = false;
                        levels.EventY.Value = mouseIsometricPosition.Y;
                    }
                    if (mouseDownObject == "npc" || mouseDownObject == "npc clone")
                    {
                        if (levels.NpcXCoord.Value != mouseIsometricPosition.X &&
                            levels.NpcYCoord.Value != mouseIsometricPosition.Y)
                            levels.Refreshing = true;
                        levels.NpcXCoord.Value = mouseIsometricPosition.X;
                        levels.Refreshing = false;
                        levels.NpcYCoord.Value = mouseIsometricPosition.Y;
                    }
                    if (mouseDownObject == "overlap")
                    {
                        if (levels.OverlapX.Value != mouseIsometricPosition.X &&
                            levels.OverlapY.Value != mouseIsometricPosition.Y)
                            levels.Refreshing = true;
                        levels.OverlapX.Value = mouseIsometricPosition.X;
                        levels.Refreshing = false;
                        levels.OverlapY.Value = mouseIsometricPosition.Y;
                    }
                    if (mouseDownObject == "tilemod")
                    {
                        int a = Math.Min(Math.Max(0, mouseTilePosition.X - mouseDownPosition.X), 63);
                        int b = Math.Min(Math.Max(0, mouseTilePosition.Y - mouseDownPosition.Y), 63);
                        if (levels.TileModsX.Value != a &&
                            levels.TileModsY.Value != b)
                            levels.Refreshing = true;
                        levels.TileModsX.Value = a;
                        levels.Refreshing = false;
                        levels.TileModsY.Value = b;
                    }
                    if (mouseDownObject == "solidmod")
                    {
                        if (levels.SolidModsX.Value != mouseIsometricPosition.X &&
                            levels.SolidModsY.Value != mouseIsometricPosition.Y)
                            levels.Refreshing = true;
                        levels.SolidModsX.Value = mouseIsometricPosition.X;
                        levels.Refreshing = false;
                        levels.SolidModsY.Value = mouseIsometricPosition.Y;
                    }
                    if (mouseDownObject == "solid tile")
                    {
                        selsolidt_location = solidity.TileCoords[mouseOverSolidTile];
                        selsolidt_location.Y -= 768;
                    }
                    if (mouseDownObject == "mushroom")
                    {
                        mushrooms[mouseDownMushroom].X = x / 16;
                        mushrooms[mouseDownMushroom].Y = y / 16;
                    }
                    pictureBoxLevel.Invalidate();
                    return;
                }
                #endregion
                #region Check if over an object
                else
                {
                    pictureBoxLevel.Cursor = Cursors.Arrow;
                    if (state.Mask)
                    {
                        if (mouseTilePosition.X == layer.MaskLowX && mouseTilePosition.Y == layer.MaskLowY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNWSE;
                            mouseOverObject = "maskNW";
                        }
                        else if (mouseTilePosition.X == layer.MaskLowX && mouseTilePosition.Y == layer.MaskHighY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNESW;
                            mouseOverObject = "maskSW";
                        }
                        else if (mouseTilePosition.X == layer.MaskHighX && mouseTilePosition.Y == layer.MaskLowY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNESW;
                            mouseOverObject = "maskNE";
                        }
                        else if (mouseTilePosition.X == layer.MaskHighX && mouseTilePosition.Y == layer.MaskHighY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNWSE;
                            mouseOverObject = "maskSE";
                        }
                        else if (mouseTilePosition.X == layer.MaskLowX &&
                            mouseTilePosition.Y <= layer.MaskHighY && mouseTilePosition.Y >= layer.MaskLowY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeWE;
                            mouseOverObject = "maskW";
                        }
                        else if (mouseTilePosition.Y == layer.MaskLowY &&
                            mouseTilePosition.X <= layer.MaskHighX && mouseTilePosition.X >= layer.MaskLowX)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNS;
                            mouseOverObject = "maskN";
                        }
                        else if (mouseTilePosition.X == layer.MaskHighX &&
                            mouseTilePosition.Y <= layer.MaskHighY && mouseTilePosition.Y >= layer.MaskLowY)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeWE;
                            mouseOverObject = "maskE";
                        }
                        else if (mouseTilePosition.Y == layer.MaskHighY &&
                            mouseTilePosition.X <= layer.MaskHighX && mouseTilePosition.X >= layer.MaskLowX)
                        {
                            pictureBoxLevel.Cursor = Cursors.SizeNS;
                            mouseOverObject = "maskS";
                        }
                    }
                    if (state.Exits && exits.Count != 0 && mouseOverObject == null)
                    {
                        int index_ext = 0;
                        foreach (Exit exit in exits.Exits)
                        {
                            if (exit.X == mouseIsometricPosition.X &&
                                exit.Y == mouseIsometricPosition.Y)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverExitField = index_ext;
                                mouseOverObject = "exit";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverExitField = 0;
                                mouseOverObject = null;
                            }
                            index_ext++;
                        }
                    }
                    if (state.Events && events.Count != 0 && mouseOverObject == null)
                    {
                        int index_evt = 0;
                        foreach (Event EVENT in events.Events)
                        {
                            if (EVENT.X == mouseIsometricPosition.X &&
                                EVENT.Y == mouseIsometricPosition.Y)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverEventField = index_evt;
                                mouseOverObject = "event";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverEventField = 0;
                                mouseOverObject = null;
                            }
                            index_evt++;
                        }
                    }
                    if (state.NPCs && npcs.Count != 0 && mouseOverObject == null)
                    {
                        int index_npc = 0;
                        foreach (NPC npc in npcs.Npcs)
                        {
                            if (npc.X == mouseIsometricPosition.X &&
                                npc.Y == mouseIsometricPosition.Y)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverNPC = index_npc;
                                mouseOverNPCClone = -1;
                                mouseOverObject = "npc";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverNPC = -1;
                                mouseOverObject = null;
                            }
                            // for all of the instances
                            int index_ins = 0;
                            foreach (NPC.Clone instance in npc.Clones)
                            {
                                if (instance.X == mouseIsometricPosition.X &&
                                    instance.Y == mouseIsometricPosition.Y)
                                {
                                    this.pictureBoxLevel.Cursor = Cursors.Hand;
                                    mouseOverNPC = index_npc;
                                    mouseOverNPCClone = index_ins;
                                    mouseOverObject = "npc clone";
                                    goto finish;
                                }
                                else
                                {
                                    this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                    mouseOverNPCClone = -1;
                                    mouseOverObject = null;
                                }
                                index_ins++;
                            }
                            index_npc++;
                        }
                    }
                finish:
                    if (state.Overlaps && overlaps.Count != 0 && mouseOverObject == null)
                    {
                        int index_ovr = 0;
                        foreach (Overlap overlap in overlaps.Overlaps)
                        {
                            if (overlap.X == mouseIsometricPosition.X &&
                                overlap.Y == mouseIsometricPosition.Y)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverOverlap = index_ovr;
                                mouseOverObject = "overlap";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverOverlap = 0;
                                mouseOverObject = null;
                            }
                            index_ovr++;
                        }
                    }
                    if (state.TileMods && tileMods.Count != 0 && mouseOverObject == null)
                    {
                        int index_tlm = 0;
                        foreach (LevelTileMods.Mod mod in tileMods.Mods)
                        {
                            if (mod.WithinBounds(x / 16, y / 16))
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverTileMod = index_tlm;
                                mouseOverObject = "tilemod";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverTileMod = 0;
                                mouseOverObject = null;
                            }
                            index_tlm++;
                        }
                    }
                    if (state.SolidMods && solidMods.Count != 0 && mouseOverObject == null)
                    {
                        int index_slm = 0;
                        foreach (LevelSolidMods.LevelMod mod in solidMods.Mods)
                        {
                            if (mod.X == mouseIsometricPosition.X &&
                                mod.Y == mouseIsometricPosition.Y)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverSolidMod = index_slm;
                                mouseOverObject = "solidmod";
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverSolidMod = 0;
                                mouseOverObject = null;
                            }
                            index_slm++;
                        }
                    }
                    if (state.SolidityLayer && buttonDragSolidity.Checked)
                    {
                        if (mouseOverSolidTileNum != 0)
                        {
                            this.pictureBoxLevel.Cursor = Cursors.Hand;
                            mouseOverObject = "solid tile";
                        }
                        else
                        {
                            this.pictureBoxLevel.Cursor = Cursors.Arrow;
                            mouseOverObject = null;
                        }
                    }
                    if (state.Mushrooms && mouseOverObject == null && mushrooms != null)
                    {
                        for (int i = 0; i < mushrooms.Length; i++)
                        {
                            if (mushrooms[i].X == x / 16 && mushrooms[i].Y == y / 16)
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Hand;
                                mouseOverObject = "mushroom";
                                mouseOverMushroom = i;
                                break;
                            }
                            else
                            {
                                this.pictureBoxLevel.Cursor = Cursors.Arrow;
                                mouseOverObject = null;
                                mouseOverMushroom = -1;
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion
            if (!state.SolidityLayer && !state.SolidMods &&
                !state.NPCs && !state.Exits && !state.Events && !state.Overlaps && !mouseWithinSameBounds)
                pictureBoxLevel.Invalidate();
            else if (state.SolidityLayer || state.SolidMods ||
                state.NPCs || state.Exits || state.Events || state.Overlaps)
                pictureBoxLevel.Invalidate();
        }
        private void pictureBoxLevel_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            //tilesetEditor.HiliteTile = true;
            pictureBoxLevel.Focus(form);
            pictureBoxLevel.Invalidate();
        }
        private void pictureBoxLevel_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            //tilesetEditor.HiliteTile = false;
            pictureBoxLevel.Invalidate();
        }
        private void pictureBoxLevel_MouseHover(object sender, EventArgs e)
        {
            //pictureBoxLevel.Invalidate();
        }
        private void pictureBoxLevel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W: toggleIsoGrid.PerformClick(); break;
                case Keys.G: toggleCartGrid.PerformClick(); break;
                case Keys.A: editAllLayers.PerformClick(); break;
                case Keys.D: buttonEditDraw.PerformClick(); break;
                case Keys.E: buttonEditErase.PerformClick(); break;
                case Keys.P: buttonEditDropper.PerformClick(); break;
                case Keys.F: buttonEditFill.PerformClick(); break;
                case Keys.S: buttonEditSelect.PerformClick(); break;
                case Keys.T: buttonEditTemplate.PerformClick(); break;
                case Keys.R: toggleSolidityFlatMode.PerformClick(); break;
                case Keys.Control | Keys.V: buttonEditPaste.PerformClick(); break;
                case Keys.Control | Keys.C: buttonEditCopy.PerformClick(); break;
                case Keys.Delete: buttonEditDelete.PerformClick(); break;
                case Keys.Control | Keys.X: buttonEditCut.PerformClick(); break;
                case Keys.Control | Keys.D: Defloat(); break;
                case Keys.Control | Keys.A: selectAll.PerformClick(); break;
                case Keys.Control | Keys.Z: buttonEditUndo.PerformClick(); break;
                case Keys.Control | Keys.Y: buttonEditRedo.PerformClick(); break;
                case Keys.Q: buttonDragSolidity.PerformClick(); break;
                case Keys.Control | Keys.D1: tilesetEditor.Layer = 0; break;
                case Keys.Control | Keys.D2: tilesetEditor.Layer = 1; break;
                case Keys.Control | Keys.D3:
                    if (tileset.Tilesets_tiles[2] != null)
                        tilesetEditor.Layer = 2; break;
            }
        }
        private void panelLevelPicture_Scroll(object sender, ScrollEventArgs e)
        {
            autoScrollPos.X = Math.Abs(panelLevelPicture.AutoScrollPosition.X);
            autoScrollPos.Y = Math.Abs(panelLevelPicture.AutoScrollPosition.Y);
            pictureBoxLevel.Invalidate();
            panelLevelPicture.Invalidate();
        }
        //toolstrip buttons
        private void buttonToggleCartGrid_Click(object sender, EventArgs e)
        {
            state.TileGrid = toggleCartGrid.Checked;
            state.IsometricGrid = toggleIsoGrid.Checked = false;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleOrthGrid_Click(object sender, EventArgs e)
        {
            state.IsometricGrid = toggleIsoGrid.Checked;
            state.TileGrid = toggleCartGrid.Checked = false;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleBG_Click(object sender, EventArgs e)
        {
            state.BG = !toggleBG.Checked;
            tilemap.RedrawTilemaps();
            tileMods.RedrawTilemaps();
            SetLevelImage();
        }
        private void buttonToggleMask_Click(object sender, EventArgs e)
        {
            state.Mask = toggleMask.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleBoundaries_Click(object sender, EventArgs e)
        {
            buttonToggleBoundaries.Checked = !buttonToggleBoundaries.Checked;
            state.ShowBoundaries = buttonToggleBoundaries.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleL1_Click(object sender, EventArgs e)
        {
            state.Layer1 = toggleL1.Checked;
            tilemap.RedrawTilemaps();
            tileMods.RedrawTilemaps();
            SetLevelImage();
        }
        private void buttonToggleL2_Click(object sender, EventArgs e)
        {
            state.Layer2 = toggleL2.Checked;
            tilemap.RedrawTilemaps();
            tileMods.RedrawTilemaps();
            SetLevelImage();
        }
        private void buttonToggleL3_Click(object sender, EventArgs e)
        {
            state.Layer3 = toggleL3.Checked;
            tilemap.RedrawTilemaps();
            tileMods.RedrawTilemaps();
            SetLevelImage();
        }
        private void buttonToggleP1_Click(object sender, EventArgs e)
        {
            state.Priority1 = toggleP1.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleTileMods_Click(object sender, EventArgs e)
        {
            Defloat();
            state.TileMods = toggleTileMods.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleSolidity_Click(object sender, EventArgs e)
        {
            Defloat();
            state.SolidityLayer = toggleSolid.Checked;

            buttonDragSolidity.Enabled = toggleSolid.Checked;
            if (!buttonDragSolidity.Enabled)
                buttonDragSolidity.Checked = false;

            pictureBoxLevel.Invalidate();
            ToggleTilesets();
        }
        private void buttonToggleSolidMods_Click(object sender, EventArgs e)
        {
            Defloat();
            state.SolidMods = toggleSolidMods.Checked;
            pictureBoxLevel.Invalidate();
            ToggleTilesets();
        }

        // Will319 suggested this feature
        // It's a toggleable "Flat Mode" where you can more easily distinguish tiles across the field
        private void buttonToggleSolidityFlatMode_Click(object sender, EventArgs e)
        {
            Defloat();
            state.SolidityLayerFlatMode = toggleSolidityFlatMode.Checked;
            if (!state.SolidityLayer && state.SolidityLayerFlatMode)
            {
                state.SolidityLayer = true;
                toggleSolid.Checked = true;
                buttonDragSolidity.Enabled = true;
            }

            pictureBoxLevel.Invalidate();
            ToggleTilesets();
        }

        //
        //
        //
        private void buttonToggleNPCs_Click(object sender, EventArgs e)
        {
            Defloat();
            state.NPCs = toggleNPCs.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleExits_Click(object sender, EventArgs e)
        {
            Defloat();
            state.Exits = toggleExits.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleEvents_Click(object sender, EventArgs e)
        {
            Defloat();
            state.Events = toggleEvents.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleOverlaps_Click(object sender, EventArgs e)
        {
            Defloat();
            state.Overlaps = toggleOverlaps.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void buttonToggleMushrooms_Click(object sender, EventArgs e)
        {
            Defloat();
            state.Mushrooms = toggleMushrooms.Checked;
            pictureBoxLevel.Invalidate();
        }
        private void toggleRails_Click(object sender, EventArgs e)
        {
            Defloat();
            state.Rails = toggleRails.Checked;
            pictureBoxLevel.Invalidate();
            tilesetEditor.Rails = state.Rails;
            tilesetEditor.PictureBox.Invalidate();
            minecart.RailColorKey.Visible = state.Rails;
        }
        private void tags_Click(object sender, EventArgs e)
        {
            pictureBoxLevel.Invalidate();
        }
        private void opacityToolStripButton_Click(object sender, EventArgs e)
        {
            panelOpacity.Visible = !panelOpacity.Visible;
        }
        private void overlayOpacity_ValueChanged(object sender, EventArgs e)
        {
            labelOverlayOpacity.Text = overlayOpacity.Value.ToString() + "%";
            pictureBoxLevel.Invalidate();
        }
        // drawing
        private void buttonEditDraw_Click(object sender, EventArgs e)
        {
            state.Draw = buttonEditDraw.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonEditDraw.Checked)
                this.pictureBoxLevel.Cursor = NewCursors.Draw;
            else if (!buttonEditDraw.Checked)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditSelect_Click(object sender, EventArgs e)
        {
            state.Select = buttonEditSelect.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Select)
                this.pictureBoxLevel.Cursor = Cursors.Cross;
            else if (!state.Select)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            if (!state.Select)
                buttonEditSelect.PerformClick();
            Defloat();
            overlay.Select.Refresh(16, 0, 0, width, height, Picture);
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditErase_Click(object sender, EventArgs e)
        {
            state.Erase = buttonEditErase.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Erase)
                this.pictureBoxLevel.Cursor = NewCursors.Erase;
            else if (!state.Erase)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            //
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditDropper_Click(object sender, EventArgs e)
        {
            state.Dropper = buttonEditDropper.Checked;
            pictureBoxLevel.ZoomBoxEnabled = state.Dropper;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Dropper)
                this.pictureBoxLevel.Cursor = NewCursors.Dropper;
            else if (!state.Dropper)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            //
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditFill_Click(object sender, EventArgs e)
        {
            state.Fill = buttonEditFill.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (state.Fill)
                this.pictureBoxLevel.Cursor = NewCursors.Fill;
            else if (!state.Fill)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            //
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditTemplate_Click(object sender, EventArgs e)
        {
            state.Template = buttonEditTemplate.Checked;
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonEditTemplate.Checked)
                this.pictureBoxLevel.Cursor = NewCursors.Template;
            else if (!buttonEditTemplate.Checked)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (!state.Move)
                Delete();
            else
            {
                state.Move = false;
                draggedTiles = null;
                pictureBoxLevel.Invalidate();
            }
            if (!state.Move && commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (copiedTiles == null)
                return;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(Math.Abs(panelLevelPicture.AutoScrollPosition.X) / zoom / 16 * 16, width - 1));
            int y = Math.Max(0, Math.Min(Math.Abs(panelLevelPicture.AutoScrollPosition.Y) / zoom / 16 * 16, height - 1));
            x += 32; y += 32;
            if (x + copiedTiles.Width >= width)
                x -= x + copiedTiles.Width - width;
            if (y + copiedTiles.Height >= height)
                y -= x + copiedTiles.Height - height;
            //
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            Paste(new Point(x, y), copiedTiles);
        }
        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonZoomIn.Checked)
                this.pictureBoxLevel.Cursor = NewCursors.ZoomIn;
            else if (!buttonZoomIn.Checked)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
        }
        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            if (buttonZoomOut.Checked)
                this.pictureBoxLevel.Cursor = NewCursors.ZoomOut;
            else if (!buttonZoomOut.Checked)
                this.pictureBoxLevel.Cursor = Cursors.Arrow;
        }
        private void buttonDragSolidity_Click(object sender, EventArgs e)
        {
            state.ClearDrawing();
            Do.ResetToolStripButtons(toolStrip2, (ToolStripButton)sender, editAllLayers);
            Defloat();
            pictureBoxLevel.Invalidate();
        }
        // context menu
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (buttonZoomIn.Checked || buttonZoomOut.Checked)
                e.Cancel = true;
            else if (mouseOverObject == "exit")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                objectFunctionToolStripMenuItem.Text = "Load destination";
                objectFunctionToolStripMenuItem.Tag = mouseOverExitField;
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else if (mouseOverObject == "event")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                objectFunctionToolStripMenuItem.Text = "Edit event's script";
                objectFunctionToolStripMenuItem.Tag = mouseOverEventField;
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else if (mouseOverObject == "npc" || mouseOverObject == "npc clone")
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;
                if (npcs.Npcs[mouseOverNPC].EngageType == 0 || npcs.Npcs[mouseOverNPC].EngageType == 1)
                    objectFunctionToolStripMenuItem.Text = "Edit npc's script";
                else if (npcs.Npcs[mouseOverNPC].EngageType == 2)
                    objectFunctionToolStripMenuItem.Text = "Edit npc's formation pack";
                objectFunctionToolStripMenuItem.Tag = new List<int> { mouseOverNPC, mouseOverNPCClone };
                objectFunctionToolStripMenuItem.Visible = true;
            }
            else if (minecart != null)
            {
                createTileModToolStripMenuItem.Visible = false;
                exportToBattlefieldToolStripMenuItem.Visible = false;
            }
            else
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;
                objectFunctionToolStripMenuItem.Visible = false;
            }
        }
        private void findInTileset_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (state.SolidityLayer)
            {
                index = solidityMap.GetTileNum(solidity.PixelTiles[mousePosition.Y * width + mousePosition.X]);
                if (!levels.OpenSolidTileset.Checked)
                    levels.OpenSolidTileset.PerformClick();
                levelsSolidTiles.Index = index;
                return;
            }
            if (state.SolidMods && solidMods.MOD != null)
            {
                index = solidMods.MOD.GetTileNum(solidity.PixelTiles[mousePosition.Y * width + mousePosition.X]);
                if (!levels.OpenSolidTileset.Checked)
                    levels.OpenSolidTileset.PerformClick();
                levelsSolidTiles.Index = index;
                return;
            }
            // first, use "see-through" approach to look for the exact visible tile clicked on
            int layer = 0;
            bool ignoreTransparent = minecart == null;
            if (state.Layer1)
                index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, ignoreTransparent);
            if (index == 0)
            {
                layer++;
                if (state.Layer2)
                    index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, ignoreTransparent);
            }
            if (index == 0)
            {
                layer++;
                if (state.Layer3)
                    index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, ignoreTransparent);
            }
            if (index != 0) // only if not all layers empty
            {
                tilesetEditor.Layer = layer;
                tilesetEditor.MouseDownTile = index;
                return;
            }
            // if all empty, use raw opaque tile searching approach
            layer = 0;
            if (state.Layer1)
                index = tilemap.GetTileNum(0, mousePosition.X, mousePosition.Y, false);
            if (index == 0)
            {
                layer++;
                if (state.Layer2)
                    index = tilemap.GetTileNum(1, mousePosition.X, mousePosition.Y, false);
            }
            if (index == 0)
            {
                layer++;
                if (state.Layer3)
                    index = tilemap.GetTileNum(2, mousePosition.X, mousePosition.Y, false);
            }
            if (index != 0) // only if not all layers empty
                tilesetEditor.Layer = layer;
            tilesetEditor.MouseDownTile = index;
        }
        private void createTileModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (overlay.Select.Empty)
            {
                MessageBox.Show("Must make a selection before creating a new tile mod.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (overlay.Select.Width / 16 >= 64 ||
                overlay.Select.Height / 16 >= 64)
            {
                MessageBox.Show("Selection must be smaller than 64x64 tiles.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            levels.TabControl.SelectedIndex = 5;
            bool instance = false;
            if (levels.TileModsFieldTree.SelectedNode != null &&
                levels.TileModsFieldTree.SelectedNode.Nodes.Count == 0 &&
                levels.TileModsFieldTree.SelectedNode.Parent == null)
                instance = MessageBox.Show("Create as an alternate tile mod?", "LAZYSHELL++",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            if (!instance && !levels.AddNewTileMod())
                return;
            if (instance && !levels.AddNewTileModInstance())
                return;
            if (!instance)
            {
                levels.TileModsX.Value = overlay.Select.X / 16;
                levels.TileModsY.Value = overlay.Select.Y / 16;
                levels.TileModsWidth.Value = overlay.Select.Width / 16;
                levels.TileModsHeight.Value = overlay.Select.Height / 16;
            }
            bool[] empty = new bool[3];
            byte[][] tilemaps = new byte[3][];
            int width = instance ? tileMods.Width : overlay.Select.Width / 16;
            int height = instance ? tileMods.Height : overlay.Select.Height / 16;
            for (int l = 0; l < 3; l++)
            {
                if (l < 2)
                    tilemaps[l] = new byte[(width * height) * 2];
                else
                    tilemaps[l] = new byte[width * height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int tileX = overlay.Select.Location.X + (x * 16);
                        int tileY = overlay.Select.Location.Y + (y * 16);
                        int tile = this.tilemap.GetTileNum(l, tileX, tileY);
                        if (tile != 0)
                            empty[l] = false;
                        int index = y * width + x;
                        if (l < 2)
                            Bits.SetShort(tilemaps[l], index * 2, (ushort)tile);
                        else
                            tilemaps[l][index] = (byte)tile;
                    }
                }
                if (!instance)
                {
                    levels.TileModsLayers.SetItemChecked(l, !empty[l]);
                    levels.tileModsLayers_SelectedIndexChanged(null, null);
                    tileMods.TilemapsA[l] = tilemaps[l];
                }
                else
                    tileMods.TilemapsB[l] = tilemaps[l];
            }
            if (!instance)
                tileMods.TilemapA = new LevelTilemap(level, tileset, tileMods.MOD, false);
            else
                tileMods.TilemapB = new LevelTilemap(level, tileset, tileMods.MOD, true);
            if (!toggleTileMods.Checked)
                toggleTileMods.PerformClick();
            tileMods.UpdateTilemaps();
            pictureBoxLevel.Invalidate();
        }
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (minecart == null)
                Do.Export(tilemapImage, "level." + level.Index.ToString("d3") + ".png");
            else
                Do.Export(tilemapImage, "minecart." + minecart.Index.ToString("d2") + ".png");
        }


        private void exportToBattlefieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (overlay.Select.Empty)
            {
                MessageBox.Show("Must make a selection before exporting to battlefield.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Tile[] tiles = new Tile[32 * 32];
            int counter = 0;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++, counter++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    int index = tilemap.GetTileNum(tilesetEditor.Layer, tileX, tileY);
                    if (y < overlay.Select.Height / 16 && x < overlay.Select.Width / 16)
                        tiles[counter] = this.tileset.Tilesets_tiles[tilesetEditor.Layer][index].Copy();
                    else
                        tiles[counter] = new Tile(counter);
                    tiles[counter].Index = counter;
                }
            }
            counter = 256;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 16; x < 32; x++, counter++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    int index = tilemap.GetTileNum(tilesetEditor.Layer, tileX, tileY);
                    if (y < overlay.Select.Height / 16 && x < overlay.Select.Width / 16)
                        tiles[counter] = this.tileset.Tilesets_tiles[tilesetEditor.Layer][index].Copy();
                    else
                        tiles[counter] = new Tile(counter);
                    tiles[counter].Index = counter;
                }
            }
            counter = 512;
            for (int y = 16; y < 32; y++)
            {
                for (int x = 0; x < 16; x++, counter++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    int index = tilemap.GetTileNum(tilesetEditor.Layer, tileX, tileY);
                    if (y < overlay.Select.Height / 16 && x < overlay.Select.Width / 16)
                        tiles[counter] = this.tileset.Tilesets_tiles[tilesetEditor.Layer][index].Copy();
                    else
                        tiles[counter] = new Tile(counter);
                    tiles[counter].Index = counter;
                }
            }
            counter = 768;
            for (int y = 16; y < 32; y++)
            {
                for (int x = 16; x < 32; x++, counter++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    int index = tilemap.GetTileNum(tilesetEditor.Layer, tileX, tileY);
                    if (y < overlay.Select.Height / 16 && x < overlay.Select.Width / 16)
                        tiles[counter] = this.tileset.Tilesets_tiles[tilesetEditor.Layer][index].Copy();
                    else
                        tiles[counter] = new Tile(counter);
                    tiles[counter].Index = counter;
                }
            }
            Battlefield battlefield = new Battlefield(0);
            battlefield.GraphicSetA = levelMap.GraphicSetA;
            battlefield.GraphicSetB = levelMap.GraphicSetB;
            battlefield.GraphicSetC = levelMap.GraphicSetC;
            battlefield.GraphicSetD = levelMap.GraphicSetD;
            battlefield.GraphicSetE = levelMap.GraphicSetE;
            PaletteSet paletteset = this.levels.PaletteSet.Copy();
            BattlefieldTileset battlefieldTileset = new BattlefieldTileset(battlefield, paletteset, tiles);
            battlefieldTileset.Battlefield = battlefield;
            battlefieldTileset.Palettes = paletteset;
            battlefieldTileset.Tileset_tiles = tiles;
            Do.Export(battlefieldTileset, "tilemap_battlefield.dat");
        }
        private void objectFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectFunctionToolStripMenuItem.Text == "Load destination")
            {
                Exit exit = exits.Exits[(int)objectFunctionToolStripMenuItem.Tag];
                if (exit.ExitType == 0)
                    levels.Index = exit.Destination;
                else
                {
                    if (Model.Program.WorldMaps == null || !Model.Program.WorldMaps.Visible)
                        Model.Program.CreateWorldMapsWindow();
                    Model.Program.WorldMaps.Index_l = exit.Destination;
                    Model.Program.WorldMaps.BringToFront();
                }
            }
            else if (objectFunctionToolStripMenuItem.Text == "Edit event's script")
            {
                Event EVENT = events.Events[(int)objectFunctionToolStripMenuItem.Tag];
                if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                    Model.Program.CreateEventScriptsWindow();
                Model.Program.EventScripts.EventName.SelectedIndex = 0;
                Model.Program.EventScripts.EventNum.Value = EVENT.RunEvent;
                Model.Program.EventScripts.BringToFront();
            }
            else if (objectFunctionToolStripMenuItem.Text == "Edit npc's script")
            {
                List<int> tag = (List<int>)objectFunctionToolStripMenuItem.Tag;
                NPC npc = npcs.Npcs[tag[0]];
                NPC instance = null;
                if (npc.Clones.Count > 0 && tag[1] >= 0)
                    instance = npc.Clones[tag[1]];
                if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                    Model.Program.CreateEventScriptsWindow();
                Model.Program.EventScripts.EventName.SelectedIndex = 0;
                if (instance == null)
                    Model.Program.EventScripts.EventNum.Value = npc.EventORpack + npc.PropertyB;
                else
                    Model.Program.EventScripts.EventNum.Value = npc.EventORpack + instance.PropertyB;
                Model.Program.EventScripts.BringToFront();
            }
            else if (objectFunctionToolStripMenuItem.Text == "Edit npc's formation pack")
            {
                List<int> tag = (List<int>)objectFunctionToolStripMenuItem.Tag;
                NPC npc = npcs.Npcs[tag[0]];
                NPC instance = null;
                if (npc.Clones.Count > 0 && tag[1] >= 0)
                    instance = npc.Clones[tag[1]];
                if (Model.Program.Formations == null || !Model.Program.Formations.Visible)
                    Model.Program.CreateFormationsWindow();
                int pack = npc.EventORpack + (instance == null ? npc.PropertyB : instance.PropertyB);
                Model.Program.Formations.PackIndex = pack;
                Model.Program.Formations.FormationIndex = Model.FormationPacks[pack].Formations[0];
                Model.Program.Formations.BringToFront();
            }
        }
        #endregion
    }
}
