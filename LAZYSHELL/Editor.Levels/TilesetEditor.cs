using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class TilesetEditor : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private Delegate update;
        public int Layer
        {
            get
            {
                if (tabControl1.SelectedIndex >= 0)
                    return tabControl1.SelectedIndex;
                return 0;
            }
            set { tabControl1.SelectedIndex = value; }
        }
        private PictureBox pictureBox
        {
            get
            {
                if (tabControl1.SelectedIndex == 0)
                    return pictureBoxTilesetL1;
                else if (tabControl1.SelectedIndex == 1)
                    return pictureBoxTilesetL2;
                else
                    return pictureBoxTilesetL3;
            }
            set
            {
                if (tabControl1.SelectedIndex == 0)
                    pictureBoxTilesetL1 = value;
                else if (tabControl1.SelectedIndex == 1)
                    pictureBoxTilesetL2 = value;
                else
                    pictureBoxTilesetL3 = value;
            }
        }
        public PictureBox PictureBox { get { return pictureBox; } set { pictureBox = value; } }
        public bool AutoUpdate { get { return autoUpdate.Checked; } set { autoUpdate.Checked = value; } }
        private Tileset tileset;
        private PaletteSet paletteSet;
        private Bitmap tilesetImage, priority1;
        private Overlay overlay;
        public TileEditor TileEditor;
        private int height { get { return tileset.Height * 16; } }
        public bool Rails;
        // mouse
        private int zoom = 1;
        private bool mouseEnter = false;
        private int mouseDownTile = 0;
        public int MouseDownTile
        {
            get { return mouseDownTile; }
            set
            {
                mouseDownTile = value;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(
                    MouseButtons.Left, 1,
                    value % 16 * 16,
                    value / 16 * 16, 0);
                pictureBoxTileset_MouseDown(null, mouseEventArgs);
                pictureBoxTileset_MouseUp(null, mouseEventArgs);
                overlay.SelectTS.ZoomRegion(tilesetImage);
                pictureBox.Invalidate();
            }
        }
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition;
        public Point mousePosition;
        private bool moving = false;
        // buffers and stacks
        private Bitmap selection;
        private CopyBuffer copiedTiles;
        private CopyBuffer selectedTiles; public CopyBuffer SelectedTiles { get { return selectedTiles; } }
        private CopyBuffer draggedTiles; public CopyBuffer DraggedTiles { get { return draggedTiles; } }
        private CommandStack commandStack = new CommandStack();
        #endregion
        #region Functions
        // main
        public TilesetEditor(Tileset tileset, Delegate update, PaletteSet paletteSet, Overlay overlay)
        {
            this.tileset = tileset;
            this.paletteSet = paletteSet;
            this.overlay = overlay;
            this.update = update;
            //
            InitializeComponent();
            this.pictureBoxTilesetL1.Height = height;
            this.pictureBoxTilesetL2.Height = height;
            this.pictureBoxTilesetL3.Height = height;
            if (this.tileset.Tilesets_tiles[Layer] == null)
            {
                if (this.tileset.Tilesets_tiles[0] != null)
                    this.Layer = 0;
                else if (this.tileset.Tilesets_tiles[1] != null)
                    this.Layer = 1;
                else if (this.tileset.Tilesets_tiles[2] != null)
                    this.Layer = 2;
                else
                    this.Layer = 0;
            }
            LoadTileEditor();
            SetTileSetImage();
        }
        public void Reload(Tileset tileset, Delegate update, PaletteSet paletteSet, Overlay overlay)
        {
            mouseDownTile = 0;
            this.pictureBoxTilesetL1.Height = height;
            this.pictureBoxTilesetL2.Height = height;
            this.pictureBoxTilesetL3.Height = height;
            //
            this.tileset = tileset;
            this.paletteSet = paletteSet;
            this.overlay = overlay;
            this.update = update;
            if (this.tileset.Tilesets_tiles[Layer] == null)
            {
                if (this.tileset.Tilesets_tiles[0] != null)
                    this.Layer = 0;
                else if (this.tileset.Tilesets_tiles[1] != null)
                    this.Layer = 1;
                else if (this.tileset.Tilesets_tiles[2] != null)
                    this.Layer = 2;
                else
                    this.Layer = 0;
            }
            LoadTileEditor();
            SetTileSetImage();
        }
        public void EnableLayers(bool L1, bool L2, bool L3)
        {
            int index = 0;
            if (L1 && !tabControl1.TabPages.Contains(tabPage1))
                tabControl1.TabPages.Insert(index++, tabPage1);
            else if (!L1 && tabControl1.Contains(tabPage1))
                tabControl1.TabPages.Remove(tabPage1);
            else if (tabControl1.Contains(tabPage1))
                index++;
            //
            if (L2 && !tabControl1.TabPages.Contains(tabPage2))
                tabControl1.TabPages.Insert(index++, tabPage2);
            else if (!L2 && tabControl1.Contains(tabPage2))
                tabControl1.TabPages.Remove(tabPage2);
            else if (tabControl1.Contains(tabPage2))
                index++;
            //
            if (L3 && !tabControl1.TabPages.Contains(tabPage3))
                tabControl1.TabPages.Insert(index++, tabPage3);
            else if (!L3 && tabControl1.Contains(tabPage3))
                tabControl1.TabPages.Remove(tabPage3);
            else if (tabControl1.Contains(tabPage3))
                index++;
        }
        private void SetTileSetImage()
        {
            if (tileset.Tilesets_tiles[Layer] != null)
            {
                int height = Layer < 2 ? tileset.Height : tileset.HeightL3;
                int[] tileSetPixels = Do.TilesetToPixels(tileset.Tilesets_tiles[Layer], 16, height, 0, false);
                int[] priority1Pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[Layer], 16, height, 0, true);
                tilesetImage = Do.PixelsToImage(tileSetPixels, 256, height * 16);
                priority1 = Do.PixelsToImage(priority1Pixels, 256, height * 16);
                pictureBox.Height = height * 16;
                pictureBox.Invalidate();
            }
        }
        // tile editor
        private void TileUpdate()
        {
            this.tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void LoadTileEditor()
        {
            if (tileset.Tilesets_tiles[Layer] == null)
                return;
            switch (Layer)
            {
                case 2: // layer 3
                    if (TileEditor == null)
                    {
                        TileEditor = new TileEditor(new Function(TileUpdate),
                            this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            tileset.GraphicsL3, paletteSet, 0x10);
                        TileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                    }
                    else
                        TileEditor.Reload(new Function(TileUpdate),
                            this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                            tileset.GraphicsL3, paletteSet, 0x10);
                    break;
                default:
                    if (TileEditor == null)
                    {
                        TileEditor = new TileEditor(new Function(TileUpdate),
                        this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                        tileset.Graphics, paletteSet, 0x20);
                        TileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                    }
                    else
                        TileEditor.Reload(new Function(TileUpdate),
                        this.tileset.Tilesets_tiles[Layer][mouseDownTile],
                        tileset.Graphics, paletteSet, 0x20);
                    break;
            }
        }
        // editing
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
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
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
                    draggedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            this.draggedTiles.Tiles = draggedTiles;
            selection = new Bitmap(this.draggedTiles.Image);
            Delete();
        }
        private void Cut()
        {
            Copy();
            Delete();
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            moving = true;
            // now dragging a new selection
            draggedTiles = buffer;
            selection = buffer.Image;
            overlay.SelectTS.Refresh(16, location, buffer.Size, PictureBox);
            this.pictureBox.Invalidate();
        }
        /// <summary>
        /// "Cements" either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        public void Defloat(CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            if (overlay.SelectTS.Empty)
                return;
            selection = null;
            int x_ = overlay.SelectTS.X / 16;
            int y_ = overlay.SelectTS.Y / 16;
            for (int y = 0; y < buffer.Height / 16; y++)
            {
                for (int x = 0; x < buffer.Width / 16; x++)
                {
                    if (y + y_ < 0 || y + y_ >= tileset.Height ||
                        x + x_ < 0 || x + x_ >= 16)
                        continue;
                    int index = (y + y_) * 16 + x + x_;
                    Tile tile = buffer.Tiles[y * (buffer.Width / 16) + x];
                    tileset.Tilesets_tiles[Layer][index] = tile.Copy();
                    tileset.Tilesets_tiles[Layer][index].Index = index;
                }
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        private void Delete()
        {
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                    tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Clear();
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            tileset.Assemble(16, Layer);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
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
                    copiedTiles[y * (overlay.SelectTS.Width / 16) + x] =
                        tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_].Copy();
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            else if (type == "invert")
                Do.FlipVertical(copiedTiles, overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16);
            buffer.Tiles = copiedTiles;
            Defloat(buffer);
        }
        private void Priority1(bool priority1)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    Tile tile = tileset.Tilesets_tiles[Layer][(y + y_) * 16 + x + x_];
                    foreach (Subtile subtile in tile.Subtiles)
                        subtile.Priority1 = priority1;
                }
            }
            tileset.DrawTileset(tileset.Tilesets_tiles[Layer], tileset.Tilesets_bytes[Layer]);
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        // import/export
        private void ImportTitle()
        {
            Layer = 0;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
            openFileDialog1.Title = "Import Layer 1";
            openFileDialog1.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog1.FileName == null)
                return;
            Bitmap importL1 = new Bitmap(Image.FromFile(openFileDialog1.FileName));
            if (importL1.Width != 256 || importL1.Height != 512)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be exactly 256 x 512.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int[] importL1pixels = Do.ImageToPixels(importL1, new Size(256, 512), new Rectangle(0, 0, 256, 512));
            Layer = 1;
            openFileDialog1.Title = "Import Layer 2";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog1.FileName == null)
                return;
            Bitmap importL2 = new Bitmap(Image.FromFile(openFileDialog1.FileName));
            if (importL2.Width != 256 || importL2.Height != 512)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be exactly 256 x 512.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int[] importL2pixels = Do.ImageToPixels(importL2, new Size(256, 512), new Rectangle(0, 0, 256, 512));
            // now combine the two into one pixel array
            int[] importPixels = new int[256 * 1024];
            for (int y = 0; y < 512; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    importPixels[y * 256 + x] = importL1pixels[y * 256 + x];
                    importPixels[(y + 512) * 256 + x] = importL2pixels[y * 256 + x];
                }
            }
            byte[] graphics = new byte[0x20000];
            int[][] palettes = new int[8][];
            for (int i = 0; i < 8; i++)
                palettes[i] = paletteSet.Palettes[i];
            int[] paletteIndexes = Do.PixelsToBPP(
                importPixels, graphics,
                new Size(256 / 8, 1024 / 8), palettes, 0x20);
            if (paletteIndexes == null)
                return;
            byte[] tileset = new byte[0x2000];
            Do.CopyToTileset(graphics, tileset, palettes, paletteIndexes, true, false, 0x20, 2, new Size(256, 1024), 0);
            Buffer.BlockCopy(tileset, 0, Model.TitleData, 0, 0x2000);
            Buffer.BlockCopy(graphics, 0, Model.TitleData, 0x6C00, 0x4FE0);
            Model.TitleTileSet = new Tileset(paletteSet, "title");
            this.tileset = Model.TitleTileSet;
        }
        private void ImportTitleLogo()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
            openFileDialog1.Title = "Import title logo";
            openFileDialog1.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog1.FileName == null)
                return;
            Bitmap import = new Bitmap(Image.FromFile(openFileDialog1.FileName));
            if (import.Width != 256 || import.Height != 96)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be 256 x 96.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            byte[] graphics = new byte[0x3000];
            byte[] gameTitle = new byte[0x1C00];
            byte[] gameCopyright = new byte[0x1400];
            int[] palette = paletteSet.Palettes[3];
            Do.PixelsToBPP(
                Do.ImageToPixels(import, new Size(256, 56), new Rectangle(0, 0, 256, 56)), gameTitle,
                new Size(256 / 8, 56 / 8), palette, 0x20);
            palette = paletteSet.Palettes[6];
            Do.PixelsToBPP(
                Do.ImageToPixels(import, new Size(256, 56), new Rectangle(0, 56, 256, 40)), gameCopyright,
                new Size(256 / 8, 40 / 8), palette, 0x20);
            Buffer.BlockCopy(gameTitle, 0, graphics, 0, 0x1C00);
            Buffer.BlockCopy(gameCopyright, 0, graphics, 0x1C00, 0x1400);
            byte[] tileset = new byte[0x300];
            byte[] tilesetTitle = new byte[0x300];
            byte[] tilesetCopyright = new byte[0x300];
            byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
            Do.CopyToTileset(graphics, tilesetTitle, palette, 3, true, true, 0x20, 2, new Size(256, 96), 2);
            Do.CopyToTileset(temp, tilesetCopyright, palette, 6, true, true, 0x20, 2, new Size(256, 96), 2);
            Buffer.BlockCopy(tilesetTitle, 0, tileset, 0, 0x300);
            Buffer.BlockCopy(tilesetCopyright, 0x1C0, tileset, 0x1C0, 0x140);
            Buffer.BlockCopy(tileset, 0, Model.TitleData, 0xBBE0, 0x300);
            Buffer.BlockCopy(graphics, 0, Model.TitleData, 0xBEE0, 0x1B80);
            Model.TitleTileSet = new Tileset(paletteSet, "title");
            this.tileset = Model.TitleTileSet;
        }
        #endregion
        #region Event handlers
        // main
        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (draggedTiles != null)
                Defloat(draggedTiles);
            else
                overlay.SelectTS.Clear();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTileSetImage();
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = tileset.Tilesets_tiles[e.TabPageIndex] == null;
        }
        // image
        private void pictureBoxTileset_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage == null)
                return;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            Rectangle rdst = new Rectangle(0, 0, 256, height);
            if (buttonToggleBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(paletteSet.Palette[16])), rdst);
            e.Graphics.DrawImage(tilesetImage, rdst, 0, 0, 256, height, GraphicsUnit.Pixel);
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
            if (priority1 != null && buttonToggleP1.Checked)
                e.Graphics.DrawImage(priority1, rdst, 0, 0, 256, height, GraphicsUnit.Pixel, ia);
            if (mouseEnter)
                DrawHoverBox(e.Graphics);
            if (Rails)
                overlay.DrawRailProperties(null, 16, 16, e.Graphics, 1);
            if (buttonToggleCartGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBox.Size, new Size(16, 16), 1, true);
            if (overlay.SelectTS != null)
                overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);
        }
        private void pictureBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
                return;
            if (e.Button == MouseButtons.Right)
                return;
            mouseDownObject = null;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            pictureBox.Focus();
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
                overlay.SelectTS.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBox);
            // if moving a current selection
            if (!lockEditing.Checked && e.Button == MouseButtons.Left && mouseOverObject == "selection")
            {
                mouseDownObject = "selection";
                mouseDownPosition = overlay.SelectTS.MousePosition(x, y);
                if (!moving)    // only do this if the current selection has not been initially moved
                {
                    moving = true;
                    Drag();
                }
            }
            mouseDownTile = y / 16 * 16 + (x / 16);
            LoadTileEditor();
        }
        private void pictureBoxTileset_MouseMove(object sender, MouseEventArgs e)
        {
            mouseOverObject = null;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBox.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBox.Height));
            mousePosition = new Point(x, y);
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
                overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBox.Width),
                        Math.Min(y + 16, pictureBox.Height));
            // if dragging the current selection
            if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                overlay.SelectTS.Location = new Point(
                    x / 16 * 16 - mouseDownPosition.X,
                    y / 16 * 16 - mouseDownPosition.Y);
            // check if over selection
            if (!lockEditing.Checked && e.Button == MouseButtons.None && overlay.SelectTS != null && overlay.SelectTS.MouseWithin(x, y))
            {
                mouseOverObject = "selection";
                pictureBox.Cursor = Cursors.SizeAll;
            }
            else
                pictureBox.Cursor = Cursors.Cross;
            pictureBox.Invalidate();
            int index = y / 16 * 16 + (x / 16);
            labelTileIndex.Text = "Tile index: " + index + " ($" + index.ToString("X2") + ")";
        }
        private void pictureBoxTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            if (this.selectedTiles == null)
                this.selectedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            int[][] selectedTiles = new int[3][];
            selectedTiles[Layer] = new int[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                    selectedTiles[Layer][y * (overlay.SelectTS.Width / 16) + x] = (y + y_) * 16 + x + x_;
            }
            this.selectedTiles.Copies = selectedTiles;
            pictureBox.Focus();
        }
        private void pictureBoxTileset_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxTileset_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxTileset_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            pictureBox.Invalidate();
        }
        private void pictureBoxTileset_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBox.Invalidate();
        }
        private void pictureBoxTileset_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V) && !lockEditing.Checked)
                buttonEditPaste.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.C) && !lockEditing.Checked)
                buttonEditCopy.PerformClick();
            if (e.KeyData == Keys.Delete && !lockEditing.Checked)
                buttonEditDelete.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.X) && !lockEditing.Checked)
                buttonEditCut.PerformClick();
            if (e.KeyData == (Keys.Control | Keys.D))
            {
                if (draggedTiles != null)
                    Defloat(draggedTiles);
                else
                {
                    overlay.SelectTS.Clear();
                    Cursor.Position = Cursor.Position;
                    pictureBox.Invalidate();
                }
            }
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                overlay.SelectTS.Refresh(16, 0, 0, 1024, 1024, pictureBox);
                pictureBox.Invalidate();
            }
        }
        private void lockEditing_CheckedChanged(object sender, EventArgs e)
        {
            buttonEditDelete.Enabled = !lockEditing.Checked;
            buttonEditCut.Enabled = !lockEditing.Checked;
            buttonEditCopy.Enabled = !lockEditing.Checked;
            buttonEditPaste.Enabled = !lockEditing.Checked;
            buttonEditMirror.Enabled = !lockEditing.Checked;
            buttonEditInvert.Enabled = !lockEditing.Checked;
        }
        // toolstrip
        private void buttonToggleTileEditor_Click(object sender, EventArgs e)
        {
            TileEditor.Visible = true;
        }
        private void buttonToggleCartGrid_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonToggleBG_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonToggleP1_Click(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }
        private void buttonEditDelete_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Delete();
        }
        private void buttonEditCut_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Cut();
        }
        private void buttonEditCopy_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Copy();
        }
        private void buttonEditPaste_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            if (draggedTiles != null)
                Defloat(draggedTiles);
            Paste(new Point(16, 16), copiedTiles);
        }
        private void buttonEditUndo_Click(object sender, EventArgs e)
        {
        }
        private void buttonEditRedo_Click(object sender, EventArgs e)
        {
        }
        // contextmenustrip
        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cutToolStripMenuItem2.Enabled = !lockEditing.Checked;
            copyToolStripMenuItem2.Enabled = !lockEditing.Checked;
            pasteToolStripMenuItem2.Enabled = !lockEditing.Checked;
            deleteToolStripMenuItem2.Enabled = !lockEditing.Checked;
            priority1SetToolStripMenuItem.Enabled = !lockEditing.Checked;
            priority1ClearToolStripMenuItem.Enabled = !lockEditing.Checked;
            mirrorToolStripMenuItem.Enabled = !lockEditing.Checked;
            invertToolStripMenuItem.Enabled = !lockEditing.Checked;
            importImageToolStripMenuItem.Visible = tileset.Type == TilesetType.Title;
        }
        private void priority1SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Priority1(true);
        }
        private void priority1ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Priority1(false);
        }
        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Flip("mirror");
        }
        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lockEditing.Checked)
                return;
            Flip("invert");
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "tileset.png");
        }
        private void importImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Layer < 2)
                ImportTitle();
            else
                ImportTitleLogo();
            //
            SetTileSetImage();
            if (autoUpdate.Checked)
                update.DynamicInvoke();
        }
        // editors
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            update.DynamicInvoke();
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
