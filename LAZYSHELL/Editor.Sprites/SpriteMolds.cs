using System;
using System.Collections;
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
    public partial class SpriteMolds : NewForm
    {
        #region Variables
        private delegate void Function();
        // main editor accessed variables
        private Sprites spritesEditor;
        private Sprite sprite { get { return spritesEditor.Sprite; } set { spritesEditor.Sprite = value; } }
        private SpriteSequences sequences { get { return spritesEditor.Sequences; } set { spritesEditor.Sequences = value; } }
        private Animation animation { get { return spritesEditor.Animation; } set { spritesEditor.Animation = value; } }
        private ImagePacket image { get { return spritesEditor.Image; } set { spritesEditor.Image = value; } }
        private int[] palette { get { return spritesEditor.Palette; } }
        private int availableBytes { get { return spritesEditor.AvailableBytes; } set { spritesEditor.AvailableBytes = value; } }
        private byte[] graphics { get { return spritesEditor.Graphics; } }
        private PaletteSet paletteSet { get { return spritesEditor.PaletteSet; } set { spritesEditor.PaletteSet = value; } }
        // local variables
        public bool ShowBG { get { return showBG.Checked; } }
        private List<Mold> molds { get { return animation.Molds; } }
        private Mold mold { get { return animation.Molds[listBoxMolds.SelectedIndex]; } set { animation.Molds[listBoxMolds.SelectedIndex] = value; } }
        private Mold moldB = null;
        private int indexB = -1;
        private int index { get { return listBoxMolds.SelectedIndex; } set { listBoxMolds.SelectedIndex = value; } }
        private int index_tile = 0;
        private int index_subtile = 0;
        private Mold.Tile tile { get { return mold.Tiles[index_tile]; } set { mold.Tiles[index_tile] = value; } }
        private List<Mold.Tile> tiles { get { return mold.Tiles; } }
        private Mold.Tile unique_tile { get { return animation.UniqueTiles[index_tile]; } set { animation.UniqueTiles[index_tile] = value; } }
        private Bitmap tilemapImage;
        private Bitmap tilesetImage;
        private List<Bitmap> tileImages = new List<Bitmap>();
        private Overlay overlay;
        private int zoom { get { return pictureBoxMold.Zoom; } }
        private int width { get { return pictureBoxMold.Width; } set { pictureBoxMold.Width = value; } }
        private int height { get { return pictureBoxMold.Height; } set { pictureBoxMold.Height = value; } }
        // mouse
        private bool move = false;
        private bool mouseWithinSameBounds = false;
        private int mouseOverTile = 0xFF;
        private int mouseDownTile = 0xFF;
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition = new Point(-1, -1);
        private Point mousePosition;
        private Point coordinator = new Point(10, 10);
        //private bool mouseEnter = false;
        // buffers
        private CommandStack commandStack;
        private CopyBuffer selectedTiles;
        private CopyBuffer copiedTiles;
        public PictureBox Picture { get { return pictureBoxMold; } }
        // editors
        public TileEditor tileEditor;
        #endregion
        #region Functions
        public SpriteMolds(Sprites spritesEditor)
        {
            this.spritesEditor = spritesEditor;
            this.overlay = new Overlay();
            this.commandStack = new CommandStack();
            InitializeComponent();
            this.pictureBoxMold.KeyDown += new KeyEventHandler(pictureBoxMold_KeyDown);
            this.pictureBoxMold.KeyUp += new KeyEventHandler(pictureBoxMold_KeyUp);
            this.pictureBoxMold.ZoomBoxPosition = new Point(64, 0);
            this.pictureBoxTileset.ZoomBoxPosition = new Point(-256, 32);
            this.Updating = true;
            this.listBoxMolds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.listBoxMolds.Items.Add("Mold " + i.ToString());
            listBoxMolds.SelectedIndex = 0;
            this.Updating = false;
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            }
            RefreshMold();
            this.moldB = this.mold.Copy();
            this.indexB = this.index;
        }
        public void Reload(Sprites spritesEditor)
        {
            this.commandStack = new CommandStack();
            this.Updating = true;
            this.listBoxMolds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.listBoxMolds.Items.Add("Mold " + i.ToString());
            listBoxMolds.SelectedIndex = 0;
            this.Updating = false;
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            }
            RefreshMold();
            this.moldB = this.mold.Copy();
            this.indexB = this.index;
        }
        private void ImportImages(string type)
        {
            Bitmap[] imports = new Bitmap[1]; imports = (Bitmap[])Do.Import(imports);
            if (imports == null || imports.Length == 0)
                return;
            byte[] graphics = new byte[0x8000];
            int[] palette = this.palette;
            //
            bool replacePalette = true;
            bool replaceMolds = true;
            bool alwaysTilemap = true;
            int startingIndex = 0;
            // if yes, start writing new graphics to location of highest used tile index
            if (MessageBox.Show("Replace current " + type + " with imported image(s)?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                replaceMolds = false;
                // look for lowest 8x8 tile index that isn't used
                foreach (Mold.Tile tile in animation.UniqueTiles)
                {
                    foreach (ushort subtile in tile.Subtile_bytes)
                        if (subtile > startingIndex)
                            startingIndex = subtile;
                }
            }
            if (type != "molds" || MessageBox.Show("Import all molds as tilemaps? Selecting no will import smaller images as gridplanes.",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                alwaysTilemap = false;
            if (MessageBox.Show("Would you like to create a new palette from the imported image(s)?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                replacePalette = false;
            if (type == "molds" && imports.Length > 32 || (!replaceMolds && molds.Count + imports.Length > 32))
            {
                MessageBox.Show("Animations cannot contain more than 32 molds total.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // tag tiles with corresponding unique tiles indexes (only if tileset import)
            if (type == "tileset")
                foreach (Mold mold in molds)
                    foreach (Mold.Tile tile in mold.Tiles)
                        for (int a = 0; a < animation.UniqueTiles.Count; a++)
                            if (Bits.Compare(tile.Subtile_bytes, animation.UniqueTiles[a].Subtile_bytes))
                            { tile.Tag = a; break; }
            Do.ImagesToMolds(molds, animation.UniqueTiles, imports, ref palette, ref graphics,
                startingIndex, replaceMolds, replacePalette, type, alwaysTilemap);
            // transfer new unique tiles to mold tiles based on tagged indexes (only if tileset import)
            if (type == "tileset")
                foreach (Mold mold in molds)
                    for (int i = 0; i < mold.Tiles.Count; i++)
                        if (mold.Tiles[i].Tag != null && (int)mold.Tiles[i].Tag < animation.UniqueTiles.Count)
                        {
                            // keep the original coords
                            byte x = mold.Tiles[i].X; byte y = mold.Tiles[i].Y;
                            mold.Tiles[i] = animation.UniqueTiles[(int)mold.Tiles[i].Tag].Copy();
                            mold.Tiles[i].X = x; mold.Tiles[i].Y = y;
                        }
            //
            for (int i = 0; i < palette.Length; i++)
            {
                paletteSet.Reds[i] = Color.FromArgb(palette[i]).R;
                paletteSet.Greens[i] = Color.FromArgb(palette[i]).G;
                paletteSet.Blues[i] = Color.FromArgb(palette[i]).B;
            }
            for (int i = startingIndex * 0x20, a = 0; i < this.graphics.Length && a < graphics.Length; i++, a++)
                this.graphics[i] = graphics[a];
            if (startingIndex * 0x20 + graphics.Length > 16384)
                MessageBox.Show("Not enough space to store the necessary amount of SNES graphics data for the imported images. The total required space (" +
                    (startingIndex * 0x20 + graphics.Length) + " bytes) for the new SNES graphics data exceeds 16384 bytes.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //
            if (type == "molds")
            {
                this.Updating = true;
                listBoxMolds.BeginUpdate();
                listBoxMolds.Items.Clear();
                for (int i = 0; i < animation.Molds.Count; i++)
                    this.listBoxMolds.Items.Add("Mold " + i.ToString());
                listBoxMolds.EndUpdate();
                this.Updating = false;
            }
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(this.graphics, paletteSet.Palette, tile.Gridplane);
            }
            if (type == "molds")
                this.index = 0;
            spritesEditor.LoadPaletteEditor();
            spritesEditor.GraphicUpdate();
            spritesEditor.LoadGraphicEditor();
            //
            foreach (Mold.Tile tile in animation.UniqueTiles)
                tile.DrawSubtiles(this.graphics, paletteSet.Palette, tile.Gridplane);
            SetTilesetImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        // editors
        //public void LoadTileEditor()
        //{
        //    if (tileEditor == null)
        //    {
        //        tileEditor = new TileEditor(new Function(TileUpdate),
        //        tileset.Tileset[mouseDownTile], tileset.Graphics,
        //        spritesEditor.PaletteSet, 0x20);
        //        tileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
        //    }
        //    else
        //        tileEditor.Reload(new Function(TileUpdate),
        //        this.tileset.Tileset[mouseDownTile], tileset.Graphics,
        //        spritesEditor.PaletteSet, 0x20);
        //}
        //private void TileUpdate()
        //{
        //    SetTilesetImage();
        //    SetTilemapImage();
        //    sequences.SetSequenceFrameImages();
        //    sequences.RealignFrames();
        //}
        private void RefreshMold()
        {
            this.Updating = true;
            index_tile = 0;
            index_subtile = 0;
            // disable drawing buttons if gridplane, but always have undo/redo enabled
            foreach (ToolStripItem item in toolStrip4.Items)
                if (item != undo && item != redo)
                    item.Enabled = !this.mold.Gridplane;
            //
            if (this.mold.Gridplane)
            {
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item.GetType() == typeof(ToolStripButton))
                        ((ToolStripButton)item).Checked = false;
                pictureBoxMold.Cursor = Cursors.Arrow;
            }
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                if (item != saveImageAsToolStripMenuItem && item != importIntoTilemapToolStripMenuItem)
                    item.Enabled = !this.mold.Gridplane;
            toolStripDropDownButton1.Enabled = !mold.Gridplane;
            deleteTile.Enabled = !mold.Gridplane;
            duplicateTile.Enabled = !mold.Gridplane;
            pictureBoxTileset.Cursor = this.mold.Gridplane ? Cursors.Arrow : Cursors.Cross;
            label13.Text = this.mold.Gridplane ? "Size" : "X,Y";
            if (mold.Gridplane)
            {
                moldTileXCoord.Enabled = mold.Tiles.Count != 0;
                moldTileYCoord.Enabled = mold.Tiles.Count != 0;
                moldTileProperties.Enabled = mold.Tiles.Count != 0;
            }
            else
            {
                moldTileXCoord.Enabled = mold.Tiles.Count != 0;
                moldTileYCoord.Enabled = mold.Tiles.Count != 0;
                moldTileProperties.Enabled = mold.Tiles.Count != 0;
            }
            moldTileXCoord.Maximum = this.mold.Gridplane ? 32 : 255;
            moldTileXCoord.Increment = this.mold.Gridplane ? 8 : 1;
            moldTileXCoord.Minimum = this.mold.Gridplane ? 24 : 0;
            moldTileYCoord.Maximum = this.mold.Gridplane ? 32 : 255;
            moldTileYCoord.Increment = this.mold.Gridplane ? 8 : 1;
            moldTileYCoord.Minimum = this.mold.Gridplane ? 24 : 0;
            moldTileProperties.BeginUpdate();
            moldTileProperties.Items.Clear();
            moldTileProperties.Items.AddRange(
                this.mold.Gridplane ?
                new string[] { "Mirror", "Invert", "Y++", "Y--" } :
                new string[] { "Mirror", "Invert" });
            moldTileProperties.EndUpdate();
            mouseDownTile = 0xFF;
            RefreshTile();
            // set VRAM req
            SetAvailableVRAM();
            //
            this.Updating = false;
        }
        private void RefreshTile()
        {
            this.Updating = true;
            if (mold.Gridplane)
            {
                panel3.Enabled = true;
                moldTileXCoord.Value = (mold.Tiles[0]).Width;
                moldTileYCoord.Value = (mold.Tiles[0]).Height;
                moldTileProperties.SetItemChecked(2, tile.YPlusOne == 1);
                moldTileProperties.SetItemChecked(3, tile.YMinusOne == 1);
                moldTileProperties.SetItemChecked(0, tile.Mirror);
                moldTileProperties.SetItemChecked(1, tile.Invert);
                subtile.Value = tile.Subtile_bytes[index_subtile];
            }
            // if changing a mold's tile
            else if (mold.Tiles.Count != 0 && mouseDownTile != 0xFF)
            {
                panel3.Enabled = true;
                moldTileXCoord.Value = (mold.Tiles[mouseDownTile]).X;
                moldTileYCoord.Value = (mold.Tiles[mouseDownTile]).Y;
                panel4.Enabled = false;
            }
            // if changing a tileset's tile and NOT a mold's tile
            else if (animation.UniqueTiles.Count != 0 && mouseDownTile == 0xFF && index_tile >= 0)
            {
                panel3.Enabled = false;
                moldTileXCoord.Value = 0;
                moldTileYCoord.Value = 0;
                moldTileProperties.SetItemChecked(0, (animation.UniqueTiles[index_tile]).Mirror);
                moldTileProperties.SetItemChecked(1, (animation.UniqueTiles[index_tile]).Invert);
                subtile.Value = (animation.UniqueTiles[index_tile]).Subtile_bytes[index_subtile];
                panel4.Enabled = true;
            }
            else
            {
                panel3.Enabled = false;
                moldTileXCoord.Value = 0;
                moldTileYCoord.Value = 0;
            }
            SetTilesetImage();
            SetTilemapImage();
            this.Updating = false;
        }
        private void RefreshListbox()
        {
            this.Updating = true;
            listBoxMolds.BeginUpdate();
            listBoxMolds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.listBoxMolds.Items.Add("Mold " + i.ToString());
            listBoxMolds.EndUpdate();
            this.Updating = false;
        }
        public void SetTilesetImage()
        {
            int[] pixels;
            if (mold.Gridplane)
            {
                pixels = (mold.Tiles[0]).GetGridplanePixels();
                tilesetImage = Do.PixelsToImage(pixels, 32, 32);
                pictureBoxTileset.Size = new Size(128, 128);
            }
            else
            {
                pixels = animation.TilesetPixels();
                tilesetImage = Do.PixelsToImage(pixels, 128, (((animation.UniqueTiles.Count - 1) / 8) + 1) * 16);
                pictureBoxTileset.Size = tilesetImage.Size;
            }
            pictureBoxTileset.Invalidate();
        }
        public void SetTilemapImage()
        {
            tileImages = new List<Bitmap>();
            tilemapImage = Do.PixelsToImage(mold.MoldPixels(), 256, 256);
            if (mold.Gridplane)
                tileImages.Add(tilemapImage);
            else
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tileImages.Add(Do.PixelsToImage(tile.Get16x16TilePixels(), 16, 16));
            }
            pictureBoxMold.Invalidate();
        }
        public void SetAvailableVRAM()
        {
            int used = 0;
            foreach (Mold.Tile tile in tiles)
            {
                foreach (ushort subtile in tile.Subtile_bytes)
                    used += 64;
            }
            int available = animation.VramAllocation - used;
            vramRequirement.Text = "Available VRAM: " + (available / 256) + " tiles (" + available + " bytes)";
            if (available < 0)
                vramRequirement.BackColor = Color.Red;
            else
                vramRequirement.BackColor = toolStrip1.BackColor;
        }
        // drawing
        private void DrawHoverBox(Graphics g, int x, int y, int z, Mold.Tile tile)
        {
            Rectangle src = new Rectangle(0, 0, 16, 16);
            Rectangle dst = new Rectangle(x * z, y * z, 16 * z, 16 * z);
            g.DrawImage(Do.PixelsToImage(tile.Get16x16TilePixels(), 16, 16), dst, src, GraphicsUnit.Pixel);
            if (mouseDownPosition != new Point(-1, -1))
                return;
            Pen pen = new Pen(Color.Red); pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.FillRectangle(new SolidBrush(Color.FromArgb(64, Color.Black)), (x * z), (y * z), 16 * z, 16 * z);
            g.DrawRectangle(pen, (x * z), (y * z), 16 * z, 16 * z);
        }
        private void Drag()
        {
            if (overlay.Select.Empty)
                return;
            List<Mold.Tile> tiles = new List<Mold.Tile>();
            for (int y = overlay.Select.Y; y < overlay.Select.Terminal.Y; y++)
            {
                for (int x = overlay.Select.X; x < overlay.Select.Terminal.X; x++)
                {
                    if (y < 0 || y >= 256 ||
                        x < 0 || x >= 256)
                        continue;
                    int index = mold.MoldTilesPerPixel[y * 256 + x];
                    if (index != 0xFF)
                    {
                        Mold.Tile tile = mold.Tiles[index];
                        if (tile.AddedToBuffer)
                            continue;
                        tile.AddedToBuffer = true;
                        tiles.Add(tile);
                    }
                }
            }
            foreach (Mold.Tile tile in mold.Tiles)
                tile.AddedToBuffer = false;
            selectedTiles = new CopyBuffer(tiles);
        }
        private void Draw(int x, int y)
        {
            if (selectedTiles == null)
                return;
            if (mold.Gridplane)
                return;
            // if over a tile, set new tile(s) to coords at the tile
            if (mouseOverTile != 0xFF)
            {
                x = (mold.Tiles[mouseOverTile]).X;
                y = (mold.Tiles[mouseOverTile]).Y;
            }
            for (int y_ = 0; y_ < selectedTiles.Height / 16; y_++)
            {
                for (int x_ = 0; x_ < selectedTiles.Width / 16; x_++)
                {
                    int index = selectedTiles.Copy[y_ * (selectedTiles.Width / 16) + x_];
                    if (index >= animation.UniqueTiles.Count)
                        continue;
                    Mold.Tile tile = (animation.UniqueTiles[index]).Copy();
                    tile.X = (byte)Math.Min(256, Math.Max(0, x + (x_ * 16)));
                    tile.Y = (byte)Math.Min(256, Math.Max(0, y + (y_ * 16)));
                    // if over a tile, replace it with new one
                    if (mouseOverTile != 0xFF)
                    {
                        // only replace if first one drawn
                        if (y_ == 0 && x_ == 0)
                            mold.Tiles[mouseOverTile] = tile;
                        // otherwise insert it
                        else
                            mold.Tiles.Insert(mouseOverTile + (y_ * (selectedTiles.Width / 16) + x_), tile);
                    }
                    else
                        mold.Tiles.Add(tile);
                    tile.DrawSubtiles(graphics, palette, false);
                }
            }
            SetTilemapImage();
        }
        private void Delete()
        {
            if (mold.Gridplane)
                return;
            ArrayList removedTiles = new ArrayList();
            if (overlay.Select != null)
            {
                for (int y = overlay.Select.Y; y < overlay.Select.Terminal.Y; y++)
                {
                    for (int x = overlay.Select.X; x < overlay.Select.Terminal.X; x++)
                    {
                        if (mold.MoldTilesPerPixel[y * 256 + x] != 0xFF)
                        {
                            removedTiles.Add(mold.Tiles[mold.MoldTilesPerPixel[y * 256 + x]]);
                            mold.MoldTilesPerPixel[y * 256 + x] = 0xFF;
                        }
                    }
                }
                foreach (Mold.Tile removedTile in removedTiles)
                    mold.Tiles.Remove(removedTile);
            }
            else if (mouseOverTile != 0xFF)
                tiles.RemoveAt(mouseOverTile);
            else
                return;
            SetTilemapImage();
            animation.Assemble();
            PushCommand(SpriteAction.Edit);
        }
        private void Cut()
        {
            if (mold.Gridplane)
                return;
            Copy();
            Delete();
        }
        private void Copy()
        {
            if (mold.Gridplane)
                return;
            List<Mold.Tile> mold_tiles = new List<Mold.Tile>();
            if (overlay.Select != null)
            {
                for (int y = overlay.Select.Y; y < overlay.Select.Terminal.Y; y++)
                {
                    for (int x = overlay.Select.X; x < overlay.Select.Terminal.X; x++)
                    {
                        if (mold.MoldTilesPerPixel[y * 256 + x] != 0xFF &&
                            !(mold.Tiles[mold.MoldTilesPerPixel[y * 256 + x]]).AddedToBuffer)
                        {
                            mold_tiles.Add(mold.Tiles[mold.MoldTilesPerPixel[y * 256 + x]]);
                            (mold.Tiles[mold.MoldTilesPerPixel[y * 256 + x]]).AddedToBuffer = true;
                        }
                    }
                }
                copiedTiles = new CopyBuffer(mold_tiles, overlay.Select.Width, overlay.Select.Height);
            }
            else if (mouseOverTile != 0xFF)
            {
                mold_tiles.Add(mold.Tiles[mouseOverTile]);
                copiedTiles = new CopyBuffer(mold_tiles, 16, 16);
            }
            foreach (Mold.Tile tile in mold.Tiles)
                tile.AddedToBuffer = false;
        }
        private void Paste(Point location)
        {
            if (copiedTiles == null)
                return;
            if (mold.Gridplane)
                return;
            int diffX = -1;
            int diffY = -1;
            foreach (Mold.Tile tile in copiedTiles.Mold_tiles)
            {
                Mold.Tile copy = tile.Copy();
                if (location.X >= 0 && location.Y >= 0)
                {
                    if (copiedTiles.Mold_tiles.IndexOf(tile) == 0)
                    {
                        diffX = location.X - copy.X;
                        diffY = location.Y - copy.Y;
                    }
                    copy.X += (byte)diffX;
                    copy.Y += (byte)diffY;
                }
                mold.Tiles.Add(copy);
                copy.DrawSubtiles(graphics, palette, false);
            }
            selectedTiles = null;
            overlay.Select.Clear();
            SetTilemapImage();
            animation.Assemble();
            PushCommand(SpriteAction.Edit);
        }
        private void Defloat()
        {
            selectedTiles = null;
            overlay.Select.Clear();
            mouseOverObject = null;
            mouseDownObject = null;
            Cursor.Position = Cursor.Position;
            pictureBoxMold.Invalidate();
        }
        private void Flip(string type)
        {
            if (mold.Gridplane)
                return;
            if (overlay.Select != null)
            {
                // make a cropped selection
                Rectangle region = Do.Crop(mold.MoldPixels(), 256, 256);
                for (int y = region.Y; y < region.Bottom; y++)
                {
                    for (int x = region.X; x < region.Right; x++)
                    {
                        if (mold.MoldTilesPerPixel[y * 256 + x] == 0xFF)
                            continue;
                        if (!overlay.Select.MouseWithin(x, y))
                            continue;
                        Mold.Tile tile = mold.Tiles[mold.MoldTilesPerPixel[y * 256 + x]];
                        if (tile.AddedToBuffer)
                            continue;
                        if (type == "mirror")
                        {
                            tile.X = (byte)((region.Width - (tile.X - region.X)) + region.X - 16);
                            tile.Mirror = !tile.Mirror;
                        }
                        else if (type == "invert")
                        {
                            tile.Y = (byte)((region.Height - (tile.Y - region.Y)) + region.Y - 16);
                            tile.Invert = !tile.Invert;
                        }
                        tile.AddedToBuffer = true;
                    }
                }
            }
            else if (mouseOverTile != 0xFF)
            {
                if (type == "mirror")
                    mold.Tiles[mouseOverTile].Mirror = !mold.Tiles[mouseOverTile].Mirror;
                else if (type == "invert")
                    mold.Tiles[mouseOverTile].Invert = !mold.Tiles[mouseOverTile].Invert;
            }
            else
                return;
            foreach (Mold.Tile tile in mold.Tiles)
                tile.AddedToBuffer = false;
            SetTilemapImage();
            animation.Assemble();
            PushCommand(SpriteAction.Edit);
        }
        private void PushCommand(SpriteAction action)
        {
            commandStack.Push(
                new SpriteEdit(action, this.molds, this.listBoxMolds,
                this.mold.Copy(), this.moldB.Copy(), index, indexB));
            this.moldB = this.mold.Copy();
            this.indexB = this.index;
        }
        private Cursor GetCursor()
        {
            if (draw.Checked)
                return NewCursors.Draw;
            if (erase.Checked)
                return NewCursors.Erase;
            if (select.Checked)
                return Cursors.Cross;
            if (zoomIn.Checked)
                return NewCursors.ZoomIn;
            if (zoomOut.Checked)
                return NewCursors.ZoomOut;
            return Cursors.Arrow;
        }
        #endregion
        #region Event Handlers
        // drawing
        private void pictureBoxMold_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (!showBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(palette[0])),
                    new Rectangle(new Point(0, 0), pictureBoxMold.Size));
            //if (tilemapImage != null)
            //{
            //    Rectangle src = new Rectangle(0, 0, tilemapImage.Width, tilemapImage.Height);
            //    Rectangle dst = new Rectangle(0, 0, tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            //    e.Graphics.DrawImage(tilemapImage, dst, src, GraphicsUnit.Pixel);
            //}
            if (tileImages.Count > 0 && tileImages.Count <= this.tiles.Count)
            {
                for (int i = tileImages.Count - 1; i >= 0; i--)
                {
                    Rectangle rsrc = new Rectangle(0, 0, tileImages[i].Width, tileImages[i].Height);
                    Rectangle rdst = new Rectangle(this.tiles[i].X * zoom, this.tiles[i].Y * zoom,
                        tileImages[0].Width * zoom, tileImages[0].Height * zoom);
                    e.Graphics.DrawImage(tileImages[i], rdst, rsrc, GraphicsUnit.Pixel);
                }
            }
            if (!select.Checked && !mold.Gridplane && mouseOverTile != 0xFF && mouseOverTile < mold.Tiles.Count)
            {
                Mold.Tile tile = mold.Tiles[mouseOverTile];
                DrawHoverBox(e.Graphics, tile.X, tile.Y, zoom, tile);
            }
            if (showMoldPixelGrid.Checked && zoom > 2)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxMold.Size, new Size(1, 1), zoom, false);
            if (select.Checked)
            {
                if (overlay.Select != null)
                {
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                    overlay.Select.DrawSelectionBox(e.Graphics, zoom);
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    List<Mold.Tile> tiles = selectedTiles != null ? selectedTiles.Mold_tiles : mold.Tiles;
                    foreach (Mold.Tile tile in tiles)
                    {
                        if (Do.WithinBounds(overlay.Select.Region, new Rectangle(tile.X, tile.Y, 16, 16), tile))
                            DrawHoverBox(e.Graphics, tile.X, tile.Y, zoom, tile);
                    }
                    //Do.DrawString(e.Graphics, new Point(overlay.Select.X, overlay.Select.Y + overlay.Select.Height),
                    //    "click/drag or move w/arrow keys", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
                }
            }
            if (coordinatorButton.Checked)
            {
                SolidBrush brush;
                if (mouseOverObject == "coordinator" || mouseDownObject == "coordinator")
                    brush = new SolidBrush(Color.Aqua);
                else
                    brush = new SolidBrush(Color.Red);
                e.Graphics.FillRectangle(brush, coordinator.X * zoom, coordinator.Y * zoom, 8 * zoom, 8 * zoom);
            }
        }
        private void pictureBoxMold_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownTile = 0xFF;
            mouseDownPosition = new Point(-1, -1);
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width / zoom));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height / zoom));
            mouseDownObject = null;
            #region Zooming
            Point p = new Point();
            p.X = Math.Abs(panelMoldImage.AutoScrollPosition.X);
            p.Y = Math.Abs(panelMoldImage.AutoScrollPosition.Y);
            if ((zoomIn.Checked && e.Button == MouseButtons.Left) ||
                (zoomOut.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxMold.ZoomIn(e.X, e.Y);
                return;
            }
            else if ((zoomOut.Checked && e.Button == MouseButtons.Left) ||
                (zoomIn.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxMold.ZoomOut(e.X, e.Y);
                return;
            }
            #endregion
            // coordinator
            if (coordinatorButton.Checked && mouseOverObject == "coordinator")
            {
                mouseDownObject = "coordinator";
                mouseDownPosition = new Point(x, y);
                return;
            }
            if (mold.Gridplane)
                return;
            if (e.Button == MouseButtons.Right)
                return;
            #region Selecting
            if (select.Checked)
            {
                // if we're not inside a current selection to move it, create a new selection
                if (mouseOverObject != "selection")
                {
                    selectedTiles = null;
                    overlay.Select.Refresh(1, x, y, 1, 1, pictureBoxMold);
                }
                // otherwise, start dragging current selection
                else if (mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.Select.MousePosition(x, y);
                    if (!move)    // only do this if the current selection has not been initially moved
                    {
                        move = true;
                        Drag();
                    }
                    foreach (Mold.Tile tile in selectedTiles.Mold_tiles)
                        tile.MouseDownPosition = new Point(
                            tile.X - mousePosition.X,
                            tile.Y - mousePosition.Y);
                }
                RefreshTile();
                return;
            }
            #endregion
            #region Drawing, Erasing, Moving Single Tile
            if (draw.Checked)
            {
                Draw(x, y);
                return;
            }
            if (erase.Checked && mouseOverTile != 0xFF)
            {
                mold.Tiles.RemoveAt(mouseOverTile);
                mouseOverTile = 0xFF;
                SetTilemapImage();
                return;
            }
            if (mouseOverTile != 0xFF)
            {
                if ((Control.ModifierKeys & Keys.Control) != 0) // duplicate tile if Ctrl pressed
                {
                    Copy();
                    Paste(new Point(-1, -1));
                    mouseOverTile = mold.Tiles.Count - 1;
                }
                mouseDownTile = mouseOverTile;
                Mold.Tile tile = mold.Tiles[mouseDownTile];
                mouseDownPosition = new Point(
                    mousePosition.X - tile.X,
                    mousePosition.Y - tile.Y);
                index_tile = animation.UniqueTiles.IndexOf(tile);
                index_subtile = 0;
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseOverObject == null)
                    overlay.SelectTS.Refresh(16, (index_tile % 8) * 16, (index_tile / 8) * 16, 16, 16, pictureBoxTileset);
                pictureBoxTileset.Invalidate();
                RefreshTile();
                return;
            }
            RefreshTile();
            #endregion
        }
        private void pictureBoxMold_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width / zoom - 1));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height / zoom - 1));
            labelCoords.Text = "(x: " + x + ", y: " + y + ") Pixel";
            mouseWithinSameBounds = mousePosition == new Point(x, y);
            mousePosition = new Point(x, y);
            mouseOverTile = 0xFF;
            mouseOverObject = null;
            #region Zooming
            // if either zoom button is checked, don't do anything else
            if (zoomIn.Checked || zoomOut.Checked)
            {
                pictureBoxMold.Invalidate();
                return;
            }
            #endregion
            // coordinator
            if (coordinatorButton.Checked && !select.Checked)
            {
                // if moving coordinator
                if (mouseDownPosition != new Point(-1, -1) && mouseDownObject == "coordinator")
                {
                    coordinator.X = x - 4;
                    coordinator.Y = y - 4;
                    pictureBoxMold.Invalidate();
                    return;
                }
                // if mouse over coordinator
                if (x >= coordinator.X && x < coordinator.X + 8 &&
                    y >= coordinator.Y && y < coordinator.Y + 8)
                {
                    mouseOverObject = "coordinator";
                    pictureBoxMold.Cursor = Cursors.Hand;
                    pictureBoxMold.Invalidate();
                    return;
                }
                else
                    pictureBoxMold.Cursor = GetCursor();
                pictureBoxMold.Invalidate();
            }
            //
            if (mold.Gridplane)
                return;
            if (e.Button == MouseButtons.Right)
                return;
            #region Selecting
            if (select.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x, y))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Max(0, Math.Min(e.X / zoom, width / zoom)),
                        Math.Max(0, Math.Min(e.Y / zoom, height / zoom)));
                }
                // if dragging the current selection
                else if (e.Button == MouseButtons.Left && mouseDownObject == "selection" && !mouseWithinSameBounds)
                {
                    overlay.Select.Location = new Point(
                        x - mouseDownPosition.X,
                        y - mouseDownPosition.Y);
                    foreach (Mold.Tile tile in selectedTiles.Mold_tiles)
                    {
                        tile.X = (byte)(x + tile.MouseDownPosition.X);
                        tile.Y = (byte)(y + tile.MouseDownPosition.Y);
                    }
                    // copy tile images to tilemapImage
                    tilemapImage = new Bitmap(256, 256);
                    Graphics graphics = Graphics.FromImage(tilemapImage);
                    for (int i = tileImages.Count - 1; i >= 0; i--)
                        graphics.DrawImage(tileImages[i], tiles[i].X, tiles[i].Y);
                    return;
                }
                // if mouse not clicked and within the current selection
                else if (e.Button == MouseButtons.None && overlay.Select != null && overlay.Select.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxMold.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxMold.Cursor = Cursors.Cross;
                pictureBoxMold.Invalidate();
                return;
            }
            #endregion
            #region Moving Single Tile
            if (mouseDownPosition != new Point(-1, -1) && !mouseWithinSameBounds)
            {
                Mold.Tile tile = mold.Tiles[mouseDownTile];
                this.Updating = true;
                moldTileXCoord.Value = (byte)(x - mouseDownPosition.X);
                moldTileYCoord.Value = (byte)(y - mouseDownPosition.Y);
                this.Updating = false;
                tile.X = (byte)(x - mouseDownPosition.X);
                tile.Y = (byte)(y - mouseDownPosition.Y);
                // copy tile images to tilemapImage
                tilemapImage = new Bitmap(256, 256);
                Graphics graphics = Graphics.FromImage(tilemapImage);
                for (int i = tileImages.Count - 1; i >= 0; i--)
                    graphics.DrawImage(tileImages[i], tiles[i].X, tiles[i].Y);
            }
            else if (!select.Checked)
            {
                // set mouseOverTile
                if (mold.MoldTilesPerPixel == null)
                    return;
                mouseOverTile = mold.MoldTilesPerPixel[y * 256 + x];
                if (!erase.Checked && !draw.Checked)
                    if (mouseOverTile != 0xFF)
                        pictureBoxMold.Cursor = Cursors.Hand;
                    else
                        pictureBoxMold.Cursor = Cursors.Arrow;
            }
            #endregion
            pictureBoxMold.Invalidate();
            pictureBoxMold.Focus();
        }
        private void pictureBoxMold_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDownPosition != new Point(-1, -1) || draw.Checked || erase.Checked)
                PushCommand(SpriteAction.Edit);
            move = false;
            mouseDownPosition = new Point(-1, -1);
            mouseDownObject = null;
            //
            animation.Assemble();
            sequences.SetSequenceFrameImages();
            spritesEditor.CalculateFreeSpace();
            SetTilemapImage();
        }
        private void pictureBoxMold_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxMold.Focus();
            pictureBoxMold.Invalidate();
        }
        private void pictureBoxMold_MouseLeave(object sender, EventArgs e)
        {
            mouseOverTile = 0xFF;
            pictureBoxMold.Invalidate();
        }
        private void pictureBoxMold_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (overlay.Select.Empty)
                        break;
                    if (selectedTiles == null)
                        Drag();
                    int x = 0;
                    int y = 0;
                    if (e.KeyData == (Keys.Left)) x = -1;
                    if (e.KeyData == (Keys.Right)) x = 1;
                    if (e.KeyData == (Keys.Up)) y = -1;
                    if (e.KeyData == (Keys.Down)) y = 1;
                    overlay.Select.Location = new Point(overlay.Select.X + x, overlay.Select.Y + y);
                    foreach (Mold.Tile tile in selectedTiles.Mold_tiles)
                    {
                        tile.X += (byte)x;
                        tile.Y += (byte)y;
                    }
                    pictureBoxMold.Invalidate();
                    break;
                case Keys.Z: toggleZoomBox.PerformClick(); break;
                case Keys.G: showMoldPixelGrid.PerformClick(); break;
                case Keys.B: showBG.PerformClick(); break;
                case Keys.T: coordinatorButton.PerformClick(); break;
                case Keys.D: draw.PerformClick(); break;
                case Keys.E: erase.PerformClick(); break;
                case Keys.S: select.PerformClick(); break;
                case Keys.Control | Keys.V: Paste(mousePosition); break;
                case Keys.Control | Keys.C: copy.PerformClick(); break;
                case Keys.Delete: delete.PerformClick(); break;
                case Keys.Control | Keys.X: cut.PerformClick(); break;
                case Keys.Control | Keys.D: Defloat(); break;
                case Keys.Control | Keys.A: selectAll.PerformClick(); break;
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
            }
        }
        private void pictureBoxMold_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (overlay.Select.Empty)
                        break;
                    PushCommand(SpriteAction.Edit);
                    break;
            }
        }
        private void pictureBoxTileset_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            Rectangle dst = new Rectangle(0, 0, pictureBoxTileset.Width, pictureBoxTileset.Height);
            Rectangle src;
            if (mold.Gridplane)
                src = new Rectangle(0, 0, 32, 32);
            else
                src = dst;
            if (!showBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(palette[0])),
                    new Rectangle(new Point(0, 0), pictureBoxTileset.Size));
            if (tilesetImage != null)
                e.Graphics.DrawImage(tilesetImage, dst, src, GraphicsUnit.Pixel);
            if (showGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxTileset.Size,
                    mold.Gridplane ? new Size(32, 32) : new Size(16, 16), 1, true);
            if (!mold.Gridplane && overlay.SelectTS != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);
            }
        }
        private void pictureBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            this.Updating = true;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxTileset.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxTileset.Height));
            mouseDownTile = 0xFF;
            if (mold.Gridplane)
            {
                index_tile = 0;
                // if beyond bounds of mold size
                if (x / 4 >= (mold.Tiles[0]).Width)
                {
                    index_subtile = 0;
                    subtile.Value = 0;
                    subtile.Enabled = false;
                }
                else
                {
                    index_subtile = (y / 32) * (tile.Width / 8) + (x / 32);
                    subtile.Value = tile.Subtile_bytes[index_subtile];
                    subtile.Enabled = true;
                }
            }
            else
            {
                if ((y / 16) * 8 + (x / 16) < animation.UniqueTiles.Count)
                {
                    index_tile = (y / 16) * 8 + (x / 16);
                    index_subtile = (y % 16 / 8) * 2 + (x % 16 / 8);
                    if (unique_tile.Mirror)
                        index_subtile ^= 1;
                    if (unique_tile.Invert)
                        index_subtile ^= 2;
                    subtile.Value = unique_tile.Subtile_bytes[index_subtile];
                    this.Updating = false;
                    // if making a new selection
                    if (e.Button == MouseButtons.Left && mouseOverObject == null)
                        overlay.SelectTS.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBoxTileset);
                    pictureBoxTileset.Invalidate();
                    RefreshTile();
                }
            }
            this.Updating = false;
        }
        private void pictureBoxTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (mold.Gridplane)
                return;
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxTileset.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxTileset.Height));
            mousePosition = new Point(x, y);
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
            {
                overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBoxTileset.Width),
                        Math.Min(y + 16, pictureBoxTileset.Height));
            }
            pictureBoxTileset.Invalidate();
        }
        private void pictureBoxTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (mold.Gridplane)
                return;
            if (overlay.SelectTS.Empty)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            if (this.selectedTiles == null)
                this.selectedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            int[] selectedTiles = new int[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                    selectedTiles[y * (overlay.SelectTS.Width / 16) + x] = (y + y_) * 8 + x + x_;
            }
            this.selectedTiles.Copy = selectedTiles;
            pictureBoxTileset.Focus();
        }
        private void pictureBoxTileset_MouseLeave(object sender, EventArgs e)
        {
        }
        private void showMoldPixelGrid_Click(object sender, EventArgs e)
        {
            pictureBoxMold.Invalidate();
        }
        // drawing
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //if (mouseOverTile != 0xFF)
            //    e.Cancel = true;
            if (zoomIn.Checked || zoomOut.Checked)
                e.Cancel = true;
        }
        private void select_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip4, (ToolStripButton)sender);
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            selectedTiles = null;
            overlay.Select.Clear();
            pictureBoxMold.Cursor = select.Checked ? Cursors.Cross : Cursors.Arrow;
            pictureBoxMold.Invalidate();
        }
        private void erase_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip4, (ToolStripButton)sender);
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            selectedTiles = null;
            overlay.Select.Clear();
            pictureBoxMold.Cursor = erase.Checked ? NewCursors.Erase : Cursors.Arrow;
            pictureBoxMold.Invalidate();
        }
        private void draw_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip4, (ToolStripButton)sender);
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            overlay.Select.Clear();
            pictureBoxMold.Cursor = draw.Checked ? NewCursors.Draw : Cursors.Arrow;
            pictureBoxMold.Invalidate();
        }
        private void cut_Click(object sender, EventArgs e)
        {
            Cut();
        }
        private void copy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void paste_Click(object sender, EventArgs e)
        {
            int x = Math.Max(0, Math.Min(Math.Abs(panelMoldImage.AutoScrollPosition.X) / zoom / 16 * 16, width - 1));
            int y = Math.Max(0, Math.Min(Math.Abs(panelMoldImage.AutoScrollPosition.Y) / zoom / 16 * 16, height - 1));
            Paste(new Point(x + 24, y + 24));
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste(mousePosition);
        }
        private void delete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            Defloat();
            if (!select.Checked)
                select.PerformClick();
            overlay.Select.Refresh(1, 0, 0, 256, 256, pictureBoxMold);
            pictureBoxMold.Invalidate();
        }
        private void mirror_Click(object sender, EventArgs e)
        {
            Flip("mirror");
        }
        private void invert_Click(object sender, EventArgs e)
        {
            Flip("invert");
        }
        private void undo_Click(object sender, EventArgs e)
        {
            selectedTiles = null;
            overlay.Select.Clear();
            //
            this.Updating = true;
            commandStack.UndoCommand();
            this.moldB = this.mold.Copy();
            this.Updating = false;
            //
            foreach (Mold.Tile tile in mold.Tiles)
                tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            sequences.SetSequenceFrameImages();
            spritesEditor.CalculateFreeSpace();
            SetTilemapImage();
            //
            int index = this.index;
            RefreshListbox();
            //
            this.Updating = true;
            this.index = Math.Min(listBoxMolds.Items.Count - 1, index);
            RefreshMold();
            this.Updating = false;
        }
        private void redo_Click(object sender, EventArgs e)
        {
            selectedTiles = null;
            overlay.Select.Clear();
            //
            this.Updating = true;
            commandStack.RedoCommand();
            this.moldB = this.mold.Copy();
            this.Updating = false;
            //
            foreach (Mold.Tile tile in mold.Tiles)
                tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            sequences.SetSequenceFrameImages();
            spritesEditor.CalculateFreeSpace();
            SetTilemapImage();
            //
            int index = this.index;
            RefreshListbox();
            //
            this.Updating = true;
            this.index = Math.Min(listBoxMolds.Items.Count - 1, index);
            RefreshMold();
            this.Updating = false;
        }
        private void adjustCoords_Click(object sender, EventArgs e)
        {
            CoordAdjust form = new CoordAdjust();
            if (form.ShowDialog() != DialogResult.OK)
                return;
            Point point = form.Point;
            if (form.ApplyToAll)
            {
                foreach (Mold mold in molds)
                {
                    if (mold.Gridplane)
                        continue;
                    foreach (Mold.Tile tile in mold.Tiles)
                    {
                        tile.X = (byte)(tile.X + point.X);
                        tile.Y = (byte)(tile.Y + point.Y);
                    }
                }
            }
            else
            {
                foreach (Mold.Tile tile in tiles)
                {
                    tile.X = (byte)(tile.X + point.X);
                    tile.Y = (byte)(tile.Y + point.Y);
                }
            }
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            animation.Assemble();
        }
        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // one single unselected tile
            if (!select.Checked && mouseOverTile != 0xFF)
            {
                Mold.Tile tile = mold.Tiles[mouseOverTile];
                int index = mold.Tiles.IndexOf(tile);
                if (index >= 0 && index < mold.Tiles.Count)
                {
                    mold.Tiles.Remove(tile);
                    mold.Tiles.Insert(0, tile);
                }
                mold.MoldPixels();
            }
            // multiple selected tiles
            else if (selectedTiles != null)
            {
                int insert = 0;
                foreach (Mold.Tile tile in selectedTiles.Mold_tiles)
                {
                    int index = mold.Tiles.IndexOf(tile);
                    if (index >= 0 && index < mold.Tiles.Count)
                    {
                        mold.Tiles.Remove(tile);
                        mold.Tiles.Insert(insert++, tile);
                    }
                }
                mold.MoldPixels();
            }
            selectedTiles = null;
            SetTilemapImage();
        }
        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // one single unselected tile
            if (!select.Checked && mouseOverTile != 0xFF)
            {
                Mold.Tile tile = mold.Tiles[mouseOverTile];
                int index = mold.Tiles.IndexOf(tile);
                if (index >= 0 && index < mold.Tiles.Count)
                {
                    mold.Tiles.Remove(tile);
                    mold.Tiles.Insert(mold.Tiles.Count - 1, tile);
                }
                mold.MoldPixels();
            }
            // multiple selected tiles
            else if (selectedTiles != null)
            {
                foreach (Mold.Tile tile in selectedTiles.Mold_tiles)
                {
                    int index = mold.Tiles.IndexOf(tile);
                    if (index >= 0 && index < mold.Tiles.Count)
                    {
                        mold.Tiles.Remove(tile);
                        mold.Tiles.Insert(mold.Tiles.Count - 1, tile);
                    }
                }
                mold.MoldPixels();
            }
            selectedTiles = null;
            SetTilemapImage();
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Would you like to crop the saved image to the bounds of the pixel edges?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int[] cropped;
                Rectangle region = Do.Crop(mold.MoldPixels(), out cropped, 256, 256);
                Do.Export(
                    Do.PixelsToImage(cropped, region.Width, region.Height),
                    "animation." + animation.Index.ToString("d3") + ".Mold." + index.ToString("d2") + ".png");
            }
            else
                Do.Export(tilemapImage, "animation." + animation.Index.ToString("d3") + ".Mold." + index.ToString("d2") + ".png");
        }
        private void zoomIn_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip4, (ToolStripButton)sender);
            zoomOut.Checked = false;
            selectedTiles = null;
            overlay.Select.Clear();
            pictureBoxMold.Cursor = zoomIn.Checked ? NewCursors.ZoomIn : Cursors.Arrow;
        }
        private void zoomOut_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip4, (ToolStripButton)sender);
            zoomIn.Checked = false;
            selectedTiles = null;
            overlay.Select.Clear();
            pictureBoxMold.Cursor = zoomOut.Checked ? NewCursors.ZoomOut : Cursors.Arrow;
        }
        private void toggleZoomBox_Click(object sender, EventArgs e)
        {
            pictureBoxMold.ZoomBoxEnabled = toggleZoomBox.Checked;
        }
        private void toggleZoomBox_TS_Click(object sender, EventArgs e)
        {
            pictureBoxTileset.ZoomBoxEnabled = toggleZoomBox_TS.Checked;
        }
        private void coordinatorButton_Click(object sender, EventArgs e)
        {
            pictureBoxMold.Invalidate();
        }
        private void showGrid_Click(object sender, EventArgs e)
        {
            pictureBoxTileset.Invalidate();
        }
        private void showBG_Click(object sender, EventArgs e)
        {
            pictureBoxMold.Invalidate();
            pictureBoxTileset.Invalidate();
            sequences.InvalidateImages();
        }
        private void duplicateTile_Click(object sender, EventArgs e)
        {
            animation.UniqueTiles.Add(this.unique_tile.Copy());
            foreach (Mold.Tile tile in animation.UniqueTiles)
                tile.DrawSubtiles(graphics, paletteSet.Palette, tile.Gridplane);
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void deleteTile_Click(object sender, EventArgs e)
        {
            if (animation.UniqueTiles.Count <= 1)
            {
                MessageBox.Show("Tile collections must contain at least 1 tile.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (Mold mold in molds)
                if (mold.Tiles.Contains(unique_tile))
                    mold.Tiles.Remove(unique_tile);
            animation.UniqueTiles.Remove(unique_tile);
            //
            if (index_tile >= animation.UniqueTiles.Count)
                index_tile = animation.UniqueTiles.Count - 1;
            index_subtile = 0;
            overlay.SelectTS.Clear();
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(graphics, paletteSet.Palette, tile.Gridplane);
            }
            //
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void newTile_Click(object sender, EventArgs e)
        {
            animation.UniqueTiles.Add(new Mold.Tile().New(false));
            foreach (Mold.Tile tile in animation.UniqueTiles)
                tile.DrawSubtiles(graphics, paletteSet.Palette, tile.Gridplane);
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void importTile_Click(object sender, EventArgs e)
        {
            Bitmap[] imports = new Bitmap[1]; imports = (Bitmap[])Do.Import(imports);
            if (imports == null || imports.Length == 0)
                return;
            //
            foreach (Bitmap import in imports)
            {
                int[] pixels = Do.ImageToPixels(import, new Size(16, 16), new Rectangle(0, 0, 16, 16));
                //
                int startingIndex = 0;
                foreach (Mold.Tile tile in animation.UniqueTiles)
                {
                    foreach (ushort subtile in tile.Subtile_bytes)
                        if (subtile > startingIndex)
                            startingIndex = subtile;
                }
                startingIndex++;
                //
                byte[] graphics = new byte[0x80];
                Do.PixelsToBPP(pixels, graphics, new Size(2, 2), this.palette, (byte)0x20);
                Mold.Tile uniqueTile = new Mold.Tile().New(false);
                for (int y = 0; y < uniqueTile.Height / 8; y++)
                {
                    for (int x = 0; x < uniqueTile.Width / 8; x++)
                    {
                        byte[] subtile = Bits.GetBytes(graphics, (y * 2 + x) * 0x20, 0x20);
                        if (Bits.Empty(subtile))
                        {
                            uniqueTile.Subtile_bytes[y * 2 + x] = 0;
                            continue;
                        }
                        int offset_dst = (y * 2 + x + startingIndex) * 0x20;
                        uniqueTile.Subtile_bytes[y * 2 + x] = (ushort)(y * 2 + x + startingIndex);
                        Buffer.BlockCopy(subtile, 0, this.graphics, offset_dst - 0x20, 0x20);
                    }
                }
                //
                animation.UniqueTiles.Add(uniqueTile);
            }
            foreach (Mold.Tile tile in animation.UniqueTiles)
                tile.DrawSubtiles(this.graphics, paletteSet.Palette, tile.Gridplane);
            //
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
            //
            spritesEditor.GraphicUpdate();
            spritesEditor.LoadGraphicEditor();
        }
        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            //importToolStripMenuItem.Visible = mold.Gridplane;
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!mold.Gridplane)
            {
                ImportImages("tileset");
                return;
            }
            Bitmap import = new Bitmap(1, 1); import = (Bitmap)Do.Import(import);
            if (import == null)
                return;
            int[] pixels = Do.ImageToPixels(import, new Size(32, 32), new Rectangle(0, 0, 32, 32));
            if (MessageBox.Show(
                "Would you like to create a new palette from the imported image?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int[] palette = Do.ReduceColorDepth(pixels, 16, this.palette[0]);
                for (int i = 0; i < palette.Length; i++)
                {
                    paletteSet.Reds[i] = Color.FromArgb(palette[i]).R;
                    paletteSet.Greens[i] = Color.FromArgb(palette[i]).G;
                    paletteSet.Blues[i] = Color.FromArgb(palette[i]).B;
                }
            }
            byte[] graphics = new byte[0x200];
            Do.PixelsToBPP(pixels, graphics, new Size(4, 4), this.palette, (byte)0x20);
            for (int y = 0; y < tile.Height / 8; y++)
            {
                for (int x = 0; x < tile.Width / 8; x++)
                {
                    if (tile.Subtile_bytes[y * (tile.Width / 8) + x] == 0)
                        continue;
                    int offset_src = (y * 4 + x) * 0x20;
                    int offset_dst = tile.Subtile_bytes[y * (tile.Width / 8) + x] * 0x20;
                    Buffer.BlockCopy(graphics, offset_src, this.graphics, offset_dst - 0x20, 0x20);
                }
            }
            spritesEditor.GraphicUpdate();
            spritesEditor.LoadGraphicEditor();
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "animation." + animation.Index.ToString("d3") + ".Mold." + index.ToString("d2") + ".png");
        }
        // properties
        private void listBoxMolds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (listBoxMolds.SelectedIndex == -1)
                return;
            selectedTiles = null;
            overlay.Select.Clear();
            PushCommand(SpriteAction.IndexChange);
            RefreshMold();
        }
        private void moldTileXCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (!mold.Gridplane)
                mold.Tiles[mouseDownTile].X = (byte)moldTileXCoord.Value;
            else
                tile.Width = (int)moldTileXCoord.Value;
            if (mold.Gridplane)
                SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            if (mold.Gridplane)
            {
                animation.Assemble();
                spritesEditor.CalculateFreeSpace();
            }
        }
        private void moldTileYCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (!mold.Gridplane)
                mold.Tiles[mouseDownTile].Y = (byte)moldTileYCoord.Value;
            else
                tile.Height = (int)moldTileYCoord.Value;
            if (mold.Gridplane)
                SetTilesetImage();
            pictureBoxMold.Invalidate();
            sequences.SetSequenceFrameImages();
            // update free space
            if (mold.Gridplane)
            {
                animation.Assemble();
                spritesEditor.CalculateFreeSpace();
            }
        }
        private void moldTileProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (mold.Gridplane)
            {
                tile.Mirror = moldTileProperties.GetItemChecked(0);
                tile.Invert = moldTileProperties.GetItemChecked(1);
                tile.YPlusOne = moldTileProperties.GetItemChecked(2) ? (byte)1 : (byte)0;
                tile.YMinusOne = moldTileProperties.GetItemChecked(3) ? (byte)1 : (byte)0;
            }
            // if changing a tileset's tile and NOT a mold's tile
            else if (mouseDownTile == 0xFF)
            {
                unique_tile.Mirror = moldTileProperties.GetItemChecked(0);
                unique_tile.Invert = moldTileProperties.GetItemChecked(1);
                unique_tile.DrawSubtiles(graphics, palette, unique_tile.Gridplane);
            }
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void subtile_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (mold.Gridplane)
            {
                tile.Subtile_bytes[index_subtile] = (ushort)subtile.Value;
                tile.SetTileLength();
                tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            }
            // if changing a tileset's tile and NOT a mold's tile
            else if (mouseDownTile == 0xFF)
            {
                unique_tile.Subtile_bytes[index_subtile] = (ushort)subtile.Value;
                unique_tile.SetTileLength();
                unique_tile.DrawSubtiles(graphics, palette, unique_tile.Gridplane);
            }
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        // adding,deleting
        private void newMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 32)
            {
                MessageBox.Show("Animations cannot contain more than 32 molds total.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            PushCommand(SpriteAction.Create);
            //
            int index = this.index;
            if (MessageBox.Show("Would you like the new mold to be in gridplane format?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                molds.Insert(index + 1, this.mold.New(true));
            else
                molds.Insert(index + 1, this.mold.New(false));
            RefreshListbox();
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            }
            this.index = index + 1;
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void deleteMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 1)
            {
                MessageBox.Show("Animations must contain at least 1 mold.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            PushCommand(SpriteAction.Delete);
            //
            int index = this.index;
            molds.RemoveAt(index);
            this.Updating = true;
            listBoxMolds.Items.RemoveAt(index);
            for (int i = 0; i < listBoxMolds.Items.Count; i++)
                listBoxMolds.Items[i] = "Mold " + i;
            this.Updating = false;
            if (index >= molds.Count && molds.Count != 0)
                this.index = index - 1;
            else if (molds.Count != 0)
                this.index = index;
            RefreshMold();
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void duplicateMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 32)
            {
                MessageBox.Show("Animations cannot contain more than 32 molds total.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            PushCommand(SpriteAction.Create);
            //
            int index = this.index;
            molds.Insert(index + 1, this.mold.Copy());
            RefreshListbox();
            foreach (Mold mold in animation.Molds)
            {
                foreach (Mold.Tile tile in mold.Tiles)
                    tile.DrawSubtiles(graphics, palette, tile.Gridplane);
            }
            this.index = index + 1;
            sequences.SetSequenceFrameImages();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
        }
        private void moveMoldBack_Click(object sender, EventArgs e)
        {
            if (listBoxMolds.SelectedIndex == 0)
                return;
            //
            PushCommand(SpriteAction.MoveUp);
            //
            int index = listBoxMolds.SelectedIndex - 1;
            animation.Molds.Reverse(index, 2);
            this.Updating = true;
            listBoxMolds.SelectedIndex--;
            this.Updating = false;
        }
        private void moveMoldFoward_Click(object sender, EventArgs e)
        {
            if (listBoxMolds.SelectedIndex == animation.Molds.Count - 1)
                return;
            //
            PushCommand(SpriteAction.MoveDown);
            //
            int index = listBoxMolds.SelectedIndex;
            animation.Molds.Reverse(index, 2);
            this.Updating = true;
            listBoxMolds.SelectedIndex++;
            this.Updating = false;
        }
        private void importIntoTilemap_Click(object sender, EventArgs e)
        {
            ImportImages("molds");
        }
        // editors
        private void openTileEditor_Click(object sender, EventArgs e)
        {
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
