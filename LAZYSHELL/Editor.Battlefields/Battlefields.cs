using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Undo;
using LAZYSHELL.Properties;
using static System.Windows.Forms.AxHost;

namespace LAZYSHELL
{
    public partial class Battlefields : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        // main
        private delegate void Function();
        private int index
        {
            get { return (int)battlefieldNum.Value; }
            set { battlefieldNum.Value = value; }
        }
        public int Index { get { return index; } set { index = value; } }
        private int palette
        {
            get { return battlefields[(int)battlefieldNum.Value].PaletteSet; }
        }
        private Battlefield[] battlefields
        {
            get { return Model.Battlefields; }
            set { Model.Battlefields = value; }
        }
        private Battlefield battlefield
        {
            get { return battlefields[index]; }
            set { battlefields[index] = value; }
        }
        private BattlefieldTileset tileset;
        private PaletteSet[] paletteSets
        {
            get { return Model.PaletteSetsBF; }
            set { Model.PaletteSetsBF = value; }
        }
        public PaletteSet[] PaletteSets
        {
            get { return paletteSets; }
            set { paletteSets = value; }
        }
        private Bitmap battlefieldImage;
        private Overlay overlay;
        // mouse
        private int zoom = 1;
        private bool mouseEnter = false;
        private int mouseDownTile = 0;
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition;
        private Point mousePosition;
        private bool moving = false;
        // editors
        private TileEditor tileEditor;
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        // buffers and stacks
        private Bitmap selection;
        private CopyBuffer draggedTiles;
        private CopyBuffer copiedTiles;
        private CommandStack commandStack = new CommandStack(true);
        private int commandCount = 0;
        // special controls
        private EditLabel labelWindow;
        // Ally image preview
        private Bitmap[] allyImages;
        private Bitmap[] statImages;
        private Bitmap[] portraits;
        //
        private bool ShowBoundaries;
        #endregion
        #region Functions
        // Main
        public Battlefields()
        {
            this.overlay = new Overlay();
            InitializeComponent();
            Do.AddShortcut(toolStrip3, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip3, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip3, Keys.F2, baseConvertor);
            toolTip1.InitialDelay = 0;
            labelWindow = new EditLabel(battlefieldName, battlefieldNum, "Battlefields", true);
            this.battlefieldName.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
            this.battlefieldGFXSet1Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames)); battlefieldGFXSet1Name.Items.Add("{NONE}");
            this.battlefieldGFXSet2Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames)); battlefieldGFXSet2Name.Items.Add("{NONE}");
            this.battlefieldGFXSet3Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames)); battlefieldGFXSet3Name.Items.Add("{NONE}");
            this.battlefieldGFXSet4Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames)); battlefieldGFXSet4Name.Items.Add("{NONE}");
            this.battlefieldGFXSet5Name.Items.AddRange(Lists.Numerize(Lists.GraphicSetNames)); battlefieldGFXSet5Name.Items.Add("{NONE}");
            RefreshBattlefield();
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
            new ToolTipLabel(this, baseConvertor, helpTips);
            this.History = new History(this, battlefieldName, battlefieldNum);
            if (settings.RememberLastIndex)
                index = settings.LastBattlefield;
            //
        }
        public void RefreshBattlefield()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Updating = true;
            tileset = new BattlefieldTileset(battlefield, paletteSets[battlefield.PaletteSet]);
            // Update fields
            battlefieldName.SelectedIndex = index;
            battlefieldGFXSet1Name.SelectedIndex = battlefield.GraphicSetA;
            battlefieldGFXSet1Num.Value = battlefield.GraphicSetA;
            battlefieldGFXSet2Name.SelectedIndex = battlefield.GraphicSetB;
            battlefieldGFXSet2Num.Value = battlefield.GraphicSetB;
            battlefieldGFXSet3Name.SelectedIndex = battlefield.GraphicSetC;
            battlefieldGFXSet3Num.Value = battlefield.GraphicSetC;
            battlefieldGFXSet4Name.SelectedIndex = battlefield.GraphicSetD;
            battlefieldGFXSet4Num.Value = battlefield.GraphicSetD;
            battlefieldGFXSet5Name.SelectedIndex = battlefield.GraphicSetE;
            battlefieldGFXSet5Num.Value = battlefield.GraphicSetE;
            battlefieldTilesetName.SelectedIndex = battlefield.TileSet;
            battlefieldTilesetNum.Value = battlefield.TileSet;
            battlefieldPaletteSetName.SelectedIndex = battlefield.PaletteSet;
            battlefieldPaletteSetNum.Value = battlefield.PaletteSet;
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
            this.Updating = false;
            Cursor.Current = Cursors.Arrow;
        }
        private void SetBattlefieldImage()
        {
            int[] battlefieldPixelsQ1 = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 16, 0, false);
            int[] battlefieldPixelsQ2 = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 16, 256, false);
            int[] battlefieldPixelsQ3 = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 16, 512, false);
            int[] battlefieldPixelsQ4 = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 16, 768, false);
            int[] battlefieldPixels = new int[512 * 512];
            Do.PixelsToPixels(battlefieldPixelsQ1, battlefieldPixels, 512, new Rectangle(0, 0, 256, 256));
            Do.PixelsToPixels(battlefieldPixelsQ2, battlefieldPixels, 512, new Rectangle(256, 0, 256, 256));
            Do.PixelsToPixels(battlefieldPixelsQ3, battlefieldPixels, 512, new Rectangle(0, 256, 256, 256));
            Do.PixelsToPixels(battlefieldPixelsQ4, battlefieldPixels, 512, new Rectangle(256, 256, 256, 256));
            battlefieldImage = Do.PixelsToImage(battlefieldPixels, 512, 512);
            pictureBoxBattlefield.Invalidate();
        }
        private void Clear()
        {
            Model.TilesetsBF[battlefield.TileSet] = new byte[0x2000];
            Model.EditTilesetsBF[battlefield.TileSet] = true;
        }
        public void Assemble()
        {
            tileset.Assemble(16, 16);
            foreach (PaletteSet ps in paletteSets)
                ps.Assemble(2);
            foreach (Battlefield bf in battlefields)
                bf.Assemble();
            Model.Compress(Model.TilesetsBF, Model.EditTilesetsBF, 0x150000, 0x15FFFF, "BATTLEFIELD", 0);
            this.Modified = false;
        }
        // Editor loading
        private void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), paletteSets[palette], 8, 2, 6);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), paletteSets[palette], 8, 2, 6);
        }
        private void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSets[palette], 1, 0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSets[palette], 1, 0x20);
        }
        private void LoadTileEditor()
        {
            if (tileEditor == null)
            {
                tileEditor = new TileEditor(new Function(TileUpdate),
                this.tileset.Tileset_tiles[mouseDownTile],
                tileset.Graphics, paletteSets[battlefield.PaletteSet], 0x20);
                tileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tileEditor.Reload(new Function(TileUpdate),
                this.tileset.Tileset_tiles[mouseDownTile],
                tileset.Graphics, paletteSets[battlefield.PaletteSet], 0x20);
        }
        // Editor updating
        private void TileUpdate()
        {
            tileset.DrawTileset(tileset.Tileset_tiles, tileset.Tileset_bytes);
            SetBattlefieldImage();
        }
        private void PaletteUpdate()
        {
            this.tileset.RedrawTileset();
            SetBattlefieldImage();
            LoadGraphicEditor();
            LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdate()
        {
            this.tileset.Assemble(16, 2);
            this.tileset.RedrawTileset();
            SetBattlefieldImage();
            LoadTileEditor();
        }
        // Editing
        private void DrawHoverBox(Graphics g)
        {
            Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
            g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
        }
        private void Copy()
        {
            if (overlay.SelectTS.Empty)
                return;
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.copiedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int x__ = (x + x_) & 15;
                    int y__ = (y + y_) & 15;
                    int index = y__ * 16 + x__;
                    index += ((x + x_) >> 4) * 256;
                    index += ((y + y_) >> 4) * 512;
                    if (index >= tileset.Tileset_tiles.Length) continue;
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tileset_tiles[index].Copy();
                }
            }
            this.copiedTiles.Tiles = copiedTiles;
        }
        /// <summary>
        /// Start dragging a selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.SelectTS.Empty)
                return;
            // make the copy
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            this.draggedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] draggedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int x__ = (x + x_) & 15;
                    int y__ = (y + y_) & 15;
                    int index = y__ * 16 + x__;
                    index += ((x + x_) >> 4) * 256;
                    index += ((y + y_) >> 4) * 512;
                    if (index >= tileset.Tileset_tiles.Length) continue;
                    draggedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tileset_tiles[index].Copy();
                }
            }
            this.draggedTiles.Tiles = draggedTiles;
            selection = new Bitmap(this.draggedTiles.Image);
            Delete();
        }
        private void Cut()
        {
            if (overlay.SelectTS.Empty || overlay.SelectTS.Size == new Size(0, 0))
                return;
            Copy();
            Delete();
            if (commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            moving = true;
            // now dragging a new selection
            draggedTiles = buffer;
            selection = buffer.Image;
            overlay.SelectTS.Refresh(16, location, buffer.Size, pictureBoxBattlefield);
            pictureBoxBattlefield.Invalidate();
        }
        /// <summary>
        /// "Cements" either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        private void Defloat(CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            if (overlay.SelectTS.Empty)
                return;
            byte[] oldTileset = Bits.Copy(tileset.Tileset_bytes);
            //
            selection = null;
            int x_ = overlay.SelectTS.X / 16;
            int y_ = overlay.SelectTS.Y / 16;
            for (int y = 0; y < buffer.Height / 16; y++)
            {
                for (int x = 0; x < buffer.Width / 16; x++)
                {
                    int x__ = (x + x_) & 15;
                    int y__ = (y + y_) & 15;
                    int index = y__ * 16 + x__;
                    index += ((x + x_) >> 4) * 256;
                    index += ((y + y_) >> 4) * 512;
                    if (index >= tileset.Tileset_tiles.Length || index < 0) continue;
                    if (y < 0 || x < 0) continue;
                    Tile tile = buffer.Tiles[y * (buffer.Width / 16) + x];
                    tileset.Tileset_tiles[index] = tile.Copy();
                    tileset.Tileset_tiles[index].Index = index;
                }
            }
            tileset.DrawTileset(tileset.Tileset_tiles, tileset.Tileset_bytes);
            commandStack.Push(commandCount + 1);
            commandCount = 0;
            RefreshBattlefield();
            SetBattlefieldImage();
            //
            commandStack.Push(new TilesetCommand(tileset, oldTileset, this));
        }
        private void Defloat()
        {
            // if copied tiles were pasted and not dragging a non-copied selection
            if (copiedTiles != null && draggedTiles == null)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            moving = false;
            selection = null;
            overlay.SelectTS.Clear();
            Cursor.Position = Cursor.Position;
        }
        private void Delete()
        {
            if (overlay.SelectTS.Empty)
                return;
            byte[] oldTileset = Bits.Copy(tileset.Tileset_bytes);
            //
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int x__ = (x + x_) & 15;
                    int y__ = (y + y_) & 15;
                    int index = y__ * 16 + x__;
                    index += ((x + x_) >> 4) * 256;
                    index += ((y + y_) >> 4) * 512;
                    if (index >= tileset.Tileset_tiles.Length) continue;
                    tileset.Tileset_tiles[index].Clear();
                    tileset.Tileset_bytes[index * 2] = 0;
                }
            }
            tileset.DrawTileset(tileset.Tileset_tiles, tileset.Tileset_bytes);
            SetBattlefieldImage();
            //
            commandStack.Push(new TilesetCommand(tileset, oldTileset, this));
            commandCount++;
        }
        private void Flip(string type)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            CopyBuffer buffer = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            Tile[] copiedTiles = new Tile[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int x__ = (x + x_) & 15;
                    int y__ = (y + y_) & 15;
                    int index = y__ * 16 + x__;
                    index += ((x + x_) >> 4) * 256;
                    index += ((y + y_) >> 4) * 512;
                    if (index >= tileset.Tileset_tiles.Length) continue;
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tileset_tiles[index].Copy();
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            else if (type == "invert")
                Do.FlipVertical(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            buffer.Tiles = copiedTiles;
            Defloat(buffer);
            tileset.DrawTileset(tileset.Tileset_tiles, tileset.Tileset_bytes);
            SetBattlefieldImage();
        }
        #endregion
        #region Event Handlers
        // main controls
        private void Battlefields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.S))
                Assemble();
        }
        private void Battlefields_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified)
                goto Close;
            DialogResult result = MessageBox.Show(
                "Battlefields have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Battlefields = null;
                Model.TilesetsBF[0] = null;
                Model.PaletteSetsBF = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            tileEditor.Close();
            paletteEditor.Close();
            graphicEditor.Close();
            tileEditor.Dispose();
            paletteEditor.Dispose();
            graphicEditor.Dispose();
        }
        private void battlefieldNum_ValueChanged(object sender, EventArgs e)
        {
            battlefieldName.SelectedIndex = (int)battlefieldNum.Value;
            tileset.Assemble(16, 16);
            RefreshBattlefield();
            settings.LastBattlefield = index;
        }
        private void battlefieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            battlefieldNum.Value = battlefieldName.SelectedIndex;
        }
        private void battlefieldPaletteSetNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.PaletteSet = (byte)battlefieldPaletteSetNum.Value;
            battlefieldPaletteSetName.SelectedIndex = (int)battlefieldPaletteSetNum.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldPaletteSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldPaletteSetNum.Value = battlefieldPaletteSetName.SelectedIndex;
        }
        private void battlefieldGFXSet1Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.GraphicSetA = (byte)battlefieldGFXSet1Num.Value;
            battlefieldGFXSet1Name.SelectedIndex = (int)battlefieldGFXSet1Num.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldGFXSet1Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldGFXSet1Num.Value = battlefieldGFXSet1Name.SelectedIndex;
        }
        private void battlefieldGFXSet2Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.GraphicSetB = (byte)battlefieldGFXSet2Num.Value;
            battlefieldGFXSet2Name.SelectedIndex = (int)battlefieldGFXSet2Num.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldGFXSet2Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldGFXSet2Num.Value = battlefieldGFXSet2Name.SelectedIndex;
        }
        private void battlefieldGFXSet3Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.GraphicSetC = (byte)battlefieldGFXSet3Num.Value;
            battlefieldGFXSet3Name.SelectedIndex = (int)battlefieldGFXSet3Num.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldGFXSet3Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldGFXSet3Num.Value = battlefieldGFXSet3Name.SelectedIndex;
        }
        private void battlefieldGFXSet4Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.GraphicSetD = (byte)battlefieldGFXSet4Num.Value;
            battlefieldGFXSet4Name.SelectedIndex = (int)battlefieldGFXSet4Num.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldGFXSet4Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldGFXSet4Num.Value = battlefieldGFXSet4Name.SelectedIndex;
        }
        private void battlefieldGFXSet5Num_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.GraphicSetE = (byte)battlefieldGFXSet5Num.Value;
            battlefieldGFXSet5Name.SelectedIndex = (int)battlefieldGFXSet5Num.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldGFXSet5Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldGFXSet5Num.Value = battlefieldGFXSet5Name.SelectedIndex;
        }
        private void battlefieldTilesetNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefield.TileSet = (byte)battlefieldTilesetNum.Value;
            battlefieldTilesetName.SelectedIndex = (int)battlefieldTilesetNum.Value;
            tileset = new BattlefieldTileset(battlefield, paletteSets[palette]);
            SetBattlefieldImage();
            // reload editors
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadTileEditor();
        }
        private void battlefieldTilesetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            battlefieldTilesetNum.Value = battlefieldTilesetName.SelectedIndex;
        }
        // open editors
        private void openPalettes_Click(object sender, EventArgs e)
        {
            paletteEditor.Visible = true;
        }
        private void openGraphics_Click(object sender, EventArgs e)
        {
            graphicEditor.Visible = true;
        }
        private void openTileEditor_Click(object sender, EventArgs e)
        {
            tileEditor.Visible = true;
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        // image
        private void pictureBoxBattlefield_Paint(object sender, PaintEventArgs e)
        {
            if (battlefieldImage == null)
                return;
            Rectangle rdst = new Rectangle(0, 0, 512, 512);
            if (!buttonToggleBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(paletteSets[palette].Palette[0])), rdst);
            e.Graphics.DrawImage(battlefieldImage, rdst, 0, 0, 512, 512, GraphicsUnit.Pixel);
            if (moving && selection != null)
            {
                Rectangle rsrc = new Rectangle(0, 0, overlay.SelectTS.Width, overlay.SelectTS.Height);
                rdst = new Rectangle(
                    overlay.SelectTS.X * zoom, overlay.SelectTS.Y * zoom,
                    rsrc.Width * zoom, rsrc.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), rdst, rsrc, GraphicsUnit.Pixel);
                Do.DrawString(e.Graphics, new Point(rdst.X, rdst.Y + rdst.Height),
                    "click/drag", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
            }
            float[][] matrixItems ={ 
               new float[] {1, 0, 0, 0, 0},
               new float[] {0, 1, 0, 0, 0},
               new float[] {0, 0, 1, 0, 0},
               new float[] {0, 0, 0, 0.50F, 0}, 
               new float[] {0, 0, 0, 0, 1}};
            ColorMatrix cm = new ColorMatrix(matrixItems);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (mouseEnter)
                DrawHoverBox(e.Graphics);
            if (buttonToggleCartGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxBattlefield.Size, new Size(16, 16), 1, true);
            if (overlay.SelectTS != null)
                overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);

            if (toggleAllies.Checked)
            {
                if (allyImages == null || portraits == null)
                    SetAllyImages();
                e.Graphics.DrawImage(allyImages[0], Model.ROM[0x0296BD] - 128 + 8, Model.ROM[0x0296BE] - 128 - 1);
                e.Graphics.DrawImage(allyImages[1], Model.ROM[0x0296BF] - 128 + 8, Model.ROM[0x0296C0] - 128 - 1);
                e.Graphics.DrawImage(allyImages[4], Model.ROM[0x0296C1] - 128 + 8, Model.ROM[0x0296C2] - 128 - 1);
                // draw HPs
                e.Graphics.DrawImage(statImages[0], 24 + 8, 94 - 32);
                e.Graphics.DrawImage(statImages[1], 48 + 8, 70 - 32);
                e.Graphics.DrawImage(statImages[4], 72 + 8, 46 - 32);
                // draw portraits
                e.Graphics.DrawImage(portraits[0], 20 - 128 + 8, 82 - 96 - 1 - 32);
                e.Graphics.DrawImage(portraits[1], 44 - 128 + 8, 58 - 96 - 1 - 32);
                e.Graphics.DrawImage(portraits[4], 68 - 128 + 8, 34 - 96 - 1 - 32);
            }

            if (ShowBoundaries && mouseEnter)
                overlay.DrawBoundaries(e.Graphics, mousePosition, zoom);

        }
        private void pictureBoxBattlefield_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            mouseDownObject = null;
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.Focus();
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            if (buttonEditSelect.Checked)
            {
                // if moving an object and outside of it, paste it
                if (moving && mouseOverObject != "selection")
                {
                    // if copied tiles were pasted and not dragging a non-copied selection
                    if (copiedTiles != null && draggedTiles == null)
                        Defloat(copiedTiles);
                    if (draggedTiles != null)
                    {
                        Defloat(draggedTiles);
                        draggedTiles = null;
                    }
                    selection = null;
                    moving = false;
                }
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseOverObject == null)
                    overlay.SelectTS.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBoxBattlefield);
                // if moving a current selection
                if (e.Button == MouseButtons.Left && mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.SelectTS.MousePosition(x, y);
                    if (!moving)    // only do this if the current selection has not been initially moved
                    {
                        moving = true;
                        Drag();
                    }
                }
            }
            int x_ = (x / 16) & 15;
            int y_ = (y / 16) & 15;
            if (x < 256 && y < 256) // 1st quad
                mouseDownTile = y_ * 16 + x_;
            if (x > 256 && y < 256) // 2nd quad
                mouseDownTile = y_ * 16 + x_ + 256;
            if (x < 256 && y > 256) // 3rd quad
                mouseDownTile = y_ * 16 + x_ + 512;
            if (x > 256 && y > 256) // 4th quad
                mouseDownTile = y_ * 16 + x_ + 768;
            LoadTileEditor();
        }
        private void pictureBoxBattlefield_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            pictureBoxBattlefield.Focus();
            pictureBoxBattlefield.Invalidate();
        }
        private void pictureBoxBattlefield_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBoxBattlefield.Invalidate();
        }
        private void pictureBoxBattlefield_MouseMove(object sender, MouseEventArgs e)
        {
            mouseOverObject = null;
            PictureBox pictureBox = (PictureBox)sender;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            mousePosition = new Point(x, y);
            if (buttonEditSelect.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.SelectTS.Final == new Point(x + 16, y + 16))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBox.Width),
                        Math.Min(y + 16, pictureBox.Height));
                }
                // if dragging the current selection
                if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                    overlay.SelectTS.Location = new Point(
                        x / 16 * 16 - mouseDownPosition.X,
                        y / 16 * 16 - mouseDownPosition.Y);
                // check if over selection
                if (e.Button == MouseButtons.None && overlay.SelectTS != null && overlay.SelectTS.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxBattlefield.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxBattlefield.Cursor = Cursors.Cross;
            }
            pictureBox.Invalidate();
        }
        private void pictureBoxBattlefield_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxBattlefield_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.G: buttonToggleCartGrid.PerformClick(); break;
                case Keys.B: buttonToggleBG.PerformClick(); break;
                case Keys.S: buttonEditSelect.PerformClick(); break;
                case Keys.P: toggleAllies.PerformClick(); break;
                case Keys.Control | Keys.C: buttonEditCopy.PerformClick(); break;
                case Keys.Control | Keys.X: buttonEditCut.PerformClick(); break;
                case Keys.Control | Keys.V:
                    if (draggedTiles != null)
                        Defloat(draggedTiles);
                    Paste(new Point(16, 16), copiedTiles);
                    break;
                case Keys.Delete: Delete(); break;
                case Keys.Control | Keys.D: Defloat(); break;
                case Keys.Control | Keys.A:
                    overlay.SelectTS.Refresh(16, 0, 0, 512, 512, pictureBoxBattlefield);
                    pictureBoxBattlefield.Invalidate();
                    break;
                case Keys.Control | Keys.Z: buttonEditUndo.PerformClick(); break;
                case Keys.Control | Keys.Y: buttonEditRedo.PerformClick(); break;
            }
        }
        // drawing buttons
        private void buttonToggleCartGrid_Click(object sender, EventArgs e)
        {
            pictureBoxBattlefield.Invalidate();
        }
        private void buttonToggleBG_Click(object sender, EventArgs e)
        {
            pictureBoxBattlefield.Invalidate();
        }
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (!moving)
                Delete();
            else
            {
                moving = false;
                draggedTiles = null;
                pictureBoxBattlefield.Invalidate();
            }
            if (!moving && commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (copiedTiles == null)
                return;
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            Paste(new Point(0, 0), copiedTiles);
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
            commandStack.UndoCommand();
            SetBattlefieldImage();
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
            commandStack.RedoCommand();
            SetBattlefieldImage();
        }
        private void buttonEditSelect_Click(object sender, EventArgs e)
        {
            if (buttonEditSelect.Checked)
                this.pictureBoxBattlefield.Cursor = System.Windows.Forms.Cursors.Cross;
            else
                this.pictureBoxBattlefield.Cursor = System.Windows.Forms.Cursors.Arrow;
            Defloat();
        }
        // menu strip
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void import_Click(object sender, EventArgs e)
        {
            new IOElements(this, index, "IMPORT BATTLEFIELDS...").ShowDialog();
            foreach (PaletteSet paletteSet in Model.PaletteSetsBF)
                paletteSet.BUFFER = Model.ROM;
            RefreshBattlefield();
            commandStack.Clear();
            commandCount = 0;
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements(this, index, "EXPORT BATTLEFIELDS...").ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            byte[] oldTileset = Bits.Copy(tileset.Tileset_bytes);
            //
            ClearElements clearElements = new ClearElements(null, index, "CLEAR BATTLEFIELD TILESETS...");
            clearElements.ShowDialog();
            if (clearElements.DialogResult == DialogResult.Cancel)
                return;
            RefreshBattlefield();
            //
            if (!Bits.Compare(oldTileset, tileset.Tileset_bytes))
            {
                commandStack.Push(new TilesetCommand(tileset, oldTileset, this));
                commandStack.Push(1);
            }
        }
        // context menu strip
        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("mirror");
        }
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip("invert");
        }
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(battlefieldImage, "battlefield." + index.ToString("d2") + ".png");
        }
        private void exportToBattlefieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tile[] tileset = new Tile[32 * 32];
            tileset = (Tile[])Do.Import(tileset);
            for (int i = 0; i < 32 * 32; i++)
                this.tileset.Tileset_tiles[i] = tileset[i].Copy();
            this.tileset.DrawTileset(this.tileset.Tileset_tiles, this.tileset.Tileset_bytes);
        }
        private void importTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BattlefieldTileset tileset = new BattlefieldTileset();
            tileset = (BattlefieldTileset)Do.Import(tileset);
            if (tileset == null)
                return;
            //
            byte[] oldTileset = Bits.Copy(tileset.Tileset_bytes);
            //
            tileset.Palettes.BUFFER = Model.ROM;
            this.battlefield.GraphicSetA = tileset.Battlefield.GraphicSetA;
            this.battlefield.GraphicSetB = tileset.Battlefield.GraphicSetB;
            this.battlefield.GraphicSetC = tileset.Battlefield.GraphicSetC;
            this.battlefield.GraphicSetD = tileset.Battlefield.GraphicSetD;
            this.battlefield.GraphicSetE = tileset.Battlefield.GraphicSetE;
            this.tileset.Palettes = tileset.Palettes;
            this.tileset.Palettes.CopyTo(Model.PaletteSetsBF[palette]);
            this.tileset.Graphics = tileset.Graphics;
            this.tileset.Tileset_tiles = tileset.Tileset_tiles;
            this.tileset.DrawTileset(this.tileset.Tileset_tiles, this.tileset.Tileset_bytes);
            this.tileset.Assemble(16, 16);
            //
            RefreshBattlefield();
            //
            if (!Bits.Compare(oldTileset, tileset.Tileset_bytes))
            {
                commandStack.Push(new TilesetCommand(tileset, oldTileset, this));
                commandStack.Push(1);
            }
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current battlefield. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            battlefield = new Battlefield(index);
            Model.Decompress(Model.TilesetsBF, 0x150000, 0x160000, 0x2000, "", index, index + 1, false);
            RefreshBattlefield();
        }

        private void SetAllyImages()
        {
            allyImages = new Bitmap[5];
            statImages = new Bitmap[5];
            portraits = new Bitmap[5];
            for (int i = 0; i < allyImages.Length; i++)
            {
                Size size = new Size(0, 0);
                Sprite sprite = Model.Sprites[Model.NPCProperties[i].Sprite];
                int[] pixels = sprite.GetPixels(false, true, 0, 7, false, false, ref size);
                allyImages[i] = Do.PixelsToImage(pixels, size.Width, size.Height);
                //
                pixels = new int[128 * 24];
                int[] palette = Model.BattleMenuPalette.Palette;
                char[] HP = new char[] { '2', '0', '9' }; // Mario
                if (i == 1) HP = new char[] { '2', '1', '1' }; // Toadstool
                if (i == 2) HP = new char[] { '2', '4', '0' }; // Bowser
                if (i == 3) HP = new char[] { '1', '9', '5' }; // Mallow
                if (i == 4) HP = new char[] { '2', '0', '3' }; // Geno
                char[] text = new char[]
                {
                    '\x01','\x01','\x01','\x01','\x01','\x01','\x01','\x01','\x02','\n' ,
                    '\x00',HP[0],HP[1],HP[2],'\x16',HP[0],HP[1],HP[2],'\x10','\n',
                    '\x11','\x11','\x11','\x11','\x11','\x11','\x11','\x11','\x12'
                };
                Do.DrawText(pixels, 128, text, 0, 0, 8, Model.FontBattleMenu, palette);
                statImages[i] = Do.PixelsToImage(pixels, 128, 24);
                //
                palette = Model.Sprites[Model.NPCProperties[i].Sprite].Palette;
                pixels = Model.Sprites[i + 40].GetPixels(true, false, 0, 0, palette, false, false, ref size);
                portraits[i] = Do.PixelsToImage(pixels, 256, 256);
            }
        }
        private void toggleAllies_Click(object sender, EventArgs e)
        {
            pictureBoxBattlefield.Invalidate();
        }
        private void buttonToggleBoundaries_Click(object sender, EventArgs e)
        {
            buttonToggleBoundaries.Checked = !buttonToggleBoundaries.Checked;
            ShowBoundaries = buttonToggleBoundaries.Checked;
            pictureBoxBattlefield.Invalidate();
        }
        #endregion
    }
}
