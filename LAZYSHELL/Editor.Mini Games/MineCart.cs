using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class MineCart : NewForm
    {
        #region Variables
        private delegate void Function();
        private Settings settings = Settings.Default;
        public MiniGames MiniGames;
        private Tilemap tilemap;
        private Tileset tileset;
        private Tileset bgtileset;
        private Overlay overlay = new Overlay();
        private PaletteSet paletteSet;
        private State state = State.Instance2;
        //
        private PaletteEditor stagePaletteEditor;
        private GraphicEditor stageGraphicEditor;
        private PaletteEditor spritePaletteEditor;
        private GraphicEditor spriteGraphicEditor;
        private Previewer previewer;
        private TilemapEditor tilemapEditor;
        private TilesetEditor tilesetEditor;
        public MinecartData MinecartData;
        private List<MCObject> minecartObjects
        {
            get
            {
                if (Index == 2)
                    return MinecartData.SSObjectsA;
                if (Index == 3)
                    return MinecartData.SSObjectsB;
                return null;
            }
            set
            {
                if (Index == 2)
                    MinecartData.SSObjectsA = value;
                if (Index == 3)
                    MinecartData.SSObjectsB = value;
            }
        }
        private int objectIndex { get { return listBoxObjects.SelectedIndex; } set { listBoxObjects.SelectedIndex = value; } }
        private MCObject minecartObject
        {
            get
            {
                return minecartObjects[objectIndex];
            }
            set
            {
                minecartObjects[objectIndex] = value;
            }
        }
        private List<Bitmap> screenImages;
        private Bitmap screenBGImage;
        private int screenIndex
        {
            get
            {
                if (screens.Tag != null)
                    return (int)screens.Tag;
                else
                    return 0;
            }
            set
            {
                if (value >= L1Indexes.Count)
                    value = 0;
                if (value < 0)
                    value = L1Indexes.Count - 1;
                if (L1Indexes.Count > 0)
                {
                    screens.Tag = value;
                    this.Updating = true;
                    this.Updating = false;
                    RefreshScreen();
                }
                foreach (PictureBox picture in screens.Controls)
                    picture.Invalidate();
            }
        }
        public int Index { get { return levelName.SelectedIndex; } set { levelName.SelectedIndex = value; } }
        private List<int> L1Indexes
        {
            get
            {
                if (Index == 2)
                    return MinecartData.L1Screens;
                else
                    return MinecartData.RailScreens;
            }
            set
            {
                if (Index == 2)
                    MinecartData.L1Screens = value;
                else
                    MinecartData.RailScreens = value;
            }
        }
        private List<int> L2Indexes
        {
            get
            {
                if (Index == 2)
                    return MinecartData.L2Screens;
                return null;
            }
            set
            {
                if (Index == 2)
                    MinecartData.L2Screens = value;
            }
        }
        public Label RailColorKey { get { return railColorKey; } set { railColorKey = value; } }
        //
        private int diffX, diffY;
        private int mouseOverObject = -1;
        private int mouseDownObject = -1;
        //
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        #endregion
        #region Functions
        public MineCart(MiniGames miniGames)
        {
            this.MiniGames = miniGames;
            InitializeComponent();
            this.music.Items.AddRange(Lists.Numerize(Lists.MusicNames));
            this.music.SelectedIndex = Model.ROM[0x0393EF];
            this.Updating = true;
            if (settings.RememberLastIndex)
                Index = settings.LastMineCart;
            else
                Index = 0;
            this.Updating = false;
            //
            MinecartData = new MinecartData(Model.MinecartObjects);
            RefreshLevel();
            // load the individual editors
            tilesetEditor.TopLevel = false;
            tilemapEditor.TopLevel = false;
            tilemapEditor.Dock = DockStyle.Fill;
            tilesetEditor.Dock = DockStyle.Right;
            panel1.Controls.Add(tilemapEditor);
            panel1.Controls.Add(tilesetEditor);
            tilemapEditor.Show();
            tilesetEditor.Show();
            //
            this.History = new History(this, levelName, null);
        }
        public void Reload(MiniGames miniGames)
        {
            this.MiniGames = miniGames;
        }
        public void RefreshLevel()
        {
            if (Index < 2)
            {
                paletteSet = Model.MinecartM7PaletteSet;
                tileset = new Tileset(Model.MinecartM7PaletteSet);
                if (Index == 0)
                    tilemap = new Mode7Tilemap(Model.MinecartM7TilemapA, tileset, paletteSet);
                else
                    tilemap = new Mode7Tilemap(Model.MinecartM7TilemapB, tileset, paletteSet);
                toolStripLabel5.Visible = true;
                startX.Visible = true;
                startY.Visible = true;
                toolStripSeparator6.Visible = true;
                startX.Value = Bits.GetShort(Model.ROM, 0x039670);
                startY.Value = Bits.GetShort(Model.ROM, 0x039679);
                panelScreens.Hide();
            }
            else
            {
                paletteSet = Model.MinecartSSPaletteSet;
                tileset = new Tileset(Model.MinecartSSTileset, Model.MinecartSSGraphics, paletteSet, 16, 16, TilesetType.SideScrolling);
                bgtileset = new Tileset(Model.MinecartSSBGTileset, Model.MinecartSSGraphics, paletteSet, 32, 16, TilesetType.SideScrolling);
                tilemap = new SideTilemap(Model.MinecartSSTilemap, null, tileset, paletteSet);
                //
                if (Index == 2)
                    screenWidth.Value = MinecartData.WidthA;
                else
                    screenWidth.Value = MinecartData.WidthB;
                toolStripLabel5.Visible = false;
                startX.Visible = false;
                startY.Visible = false;
                toolStripSeparator6.Visible = false;
                InitializeScreens();
                InitializeObjects();
                panelScreens.Show();
            }
            LoadStagePaletteEditor();
            LoadStageGraphicEditor();
            LoadSpritePaletteEditor();
            LoadSpriteGraphicEditor();
            LoadTilesetEditor();
            LoadTilemapEditor();
            //
            railColorKey.Visible = state.Rails && Index < 2;
            tilesetEditor.Rails = state.Rails && Index < 2;
        }
        private void InitializeObjects()
        {
            listBoxObjects.Items.Clear();
            for (int i = 0; i < minecartObjects.Count; i++)
                listBoxObjects.Items.Add("Object " + i);
            if (minecartObjects.Count > 0)
                objectIndex = 0;
        }
        private void InitializeScreens()
        {
            this.Updating = true;
            pictureBoxScreens.Width = L1Indexes.Count * 256;
            screens.AutoScrollPosition = new Point(0, 0);
            if (L1Indexes.Count == 0)
                toolStrip1.Enabled = false;
            else
            {
                screenIndex = 0;
                toolStrip1.Enabled = true;
                this.screenL1Number.Value = L1Indexes[screenIndex];
                if (Index == 2)
                    this.screenL2Number.Value = L2Indexes[screenIndex];
                this.screenL2Number.Enabled = Index == 2;
            }
            this.Updating = false;
            SetScreenImages();
        }
        private void RefreshObject()
        {
            this.Updating = true;
            objectType.SelectedIndex = minecartObject.Type;
            rowSize.Value = minecartObject.Count;
            objectX.Value = minecartObject.X;
            objectY.Value = minecartObject.Y;
            this.Updating = false;
        }
        private void RefreshScreen()
        {
            this.Updating = true;
            if (L1Indexes != null && L1Indexes.Count != 0)
            {
                this.screenL1Number.Enabled = true;
                this.screenL1Number.Value = L1Indexes[screenIndex];
            }
            else
            {
                screenL1Number.Enabled = false;
                screenL1Number.Value = 0;
            }
            if (Index == 2)
            {
                this.screenL2Number.Enabled = true;
                this.screenL2Number.Value = L2Indexes[screenIndex];
            }
            else
            {
                screenL2Number.Enabled = false;
                screenL2Number.Value = 0;
            }
            this.Updating = false;
        }
        public void SetScreenImage()
        {
            if (MinecartData == null)
                return;
            if (screenIndex >= L1Indexes.Count)
                return;
            if (L1Indexes[screenIndex] < 16)
            {
                byte[] tilemapL1 = Bits.GetBytes(Model.MinecartSSTilemap, L1Indexes[screenIndex] * 256, 256);
                byte[] tilemapL2;
                if (Index == 2)
                    tilemapL2 = Bits.GetBytes(Model.MinecartSSTilemap, L2Indexes[screenIndex] * 256, 256);
                else
                    tilemapL2 = new byte[256];
                SideTilemap tilemap = new SideTilemap(tilemapL1, tilemapL2, tileset, paletteSet);
                screenImages[screenIndex] = Do.PixelsToImage(tilemap.Pixels, 256, 256);
            }
            else
            {
                screenImages[screenIndex] = new Bitmap(256, 256);
            }
            pictureBoxScreens.Invalidate();
        }
        public void SetScreenImages()
        {
            if (MinecartData == null)
                return;
            screenImages = new List<Bitmap>();
            for (int i = 0; i < L1Indexes.Count; i++)
            {
                if (L1Indexes[i] < 16)
                {
                    byte[] tilemapL1 = Bits.GetBytes(Model.MinecartSSTilemap, L1Indexes[i] * 256, 256);
                    byte[] tilemapL2;
                    if (Index == 2)
                        tilemapL2 = Bits.GetBytes(Model.MinecartSSTilemap, L2Indexes[i] * 256, 256);
                    else
                        tilemapL2 = new byte[256];
                    SideTilemap tilemap = new SideTilemap(tilemapL1, tilemapL2, tileset, paletteSet);
                    Bitmap screenImage = Do.PixelsToImage(tilemap.Pixels, 256, 256);
                    screenImages.Add(new Bitmap(screenImage));
                }
                else
                {
                    screenImages.Add(new Bitmap(256, 256));
                }
            }
            pictureBoxScreens.Invalidate();
        }
        public void CloseEditors()
        {
            if (spritePaletteEditor.Visible)
                spritePaletteEditor.Close();
            if (spriteGraphicEditor.Visible)
                spriteGraphicEditor.Close();
            if (stageGraphicEditor.Visible)
                stageGraphicEditor.Close();
            if (stagePaletteEditor.Visible)
                stagePaletteEditor.Close();
            spritePaletteEditor.Dispose();
            spriteGraphicEditor.Dispose();
            stageGraphicEditor.Dispose();
            stagePaletteEditor.Dispose();
            if (previewer != null)
                previewer.Close();
        }
        //
        private void StagePaletteUpdate()
        {
            bgtileset.RedrawTilesets();
            tileset.RedrawTilesets();
            tilemap.RedrawTilemaps();
            LoadTilesetEditor();
            LoadTilemapEditor();
            //
            screenBGImage = null;
            SetScreenImages();
            //
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void StageGraphicUpdate()
        {
            tileset.Assemble(16);
            //
            tileset.RedrawTilesets();
            tilemap.RedrawTilemaps();
            LoadTilesetEditor();
            LoadTilemapEditor();
            //
            SetScreenImages();
        }
        private void SpritePaletteUpdate()
        {
            MinecartData.Mushroom = null;
            MinecartData.Coin = null;
            tilemapEditor.Picture.Invalidate();
            pictureBoxScreens.Invalidate();
        }
        private void SpriteGraphicUpdate()
        {
            MinecartData.Mushroom = null;
            MinecartData.Coin = null;
            tilemapEditor.Picture.Invalidate();
            pictureBoxScreens.Invalidate();
        }
        private void PaletteObjectUpdate()
        {
            MinecartData.Mushroom = null;
            MinecartData.Coin = null;
            pictureBoxScreens.Invalidate();
        }
        private void GraphicObjectUpdate()
        {
            MinecartData.Mushroom = null;
            MinecartData.Coin = null;
            pictureBoxScreens.Invalidate();
        }
        private void TilemapUpdate()
        {
        }
        private void TilesetUpdate()
        {
            tilemap.Assemble();
            tilemap = new Mode7Tilemap(Model.MinecartM7TilemapA, tileset, Model.MinecartM7PaletteSet);
            RefreshLevel();
        }
        //
        private void LoadStagePaletteEditor()
        {
            if (stagePaletteEditor == null)
            {
                stagePaletteEditor = new PaletteEditor(new Function(StagePaletteUpdate), paletteSet, 8, 0, 8);
                stagePaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                stagePaletteEditor.Reload(new Function(StagePaletteUpdate), paletteSet, 8, 0, 8);
        }
        private void LoadStageGraphicEditor()
        {
            if (stageGraphicEditor == null)
            {
                stageGraphicEditor = new GraphicEditor(new Function(StageGraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 0, 0x20);
                stageGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                stageGraphicEditor.Reload(new Function(StageGraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 0, 0x20);
        }
        private void LoadSpritePaletteEditor()
        {
            if (spritePaletteEditor == null)
            {
                spritePaletteEditor = new PaletteEditor(new Function(SpritePaletteUpdate), Model.MinecartObjectPaletteSet, 8, 0, 8);
                spritePaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                spritePaletteEditor.Reload(new Function(SpritePaletteUpdate), Model.MinecartObjectPaletteSet, 8, 0, 8);
        }
        private void LoadSpriteGraphicEditor()
        {
            if (spriteGraphicEditor == null)
            {
                spriteGraphicEditor = new GraphicEditor(new Function(SpriteGraphicUpdate),
                    Model.MinecartObjectGraphics, Model.MinecartObjectGraphics.Length, 0, Model.MinecartObjectPaletteSet, 0, 0x20);
                spriteGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                spriteGraphicEditor.Reload(new Function(SpriteGraphicUpdate),
                    Model.MinecartObjectGraphics, Model.MinecartObjectGraphics.Length, 0, Model.MinecartObjectPaletteSet, 0, 0x20);
        }
        private void LoadTilemapEditor()
        {
            if (tilemapEditor == null)
            {
                tilemapEditor = new TilemapEditor(
                    this, this.tilemap, this.tileset, this.overlay,
                    this.stagePaletteEditor, this.tilesetEditor);
                tilemapEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilemapEditor.Reload(
                  this, this.tilemap, this.tileset, this.overlay,
                  this.stagePaletteEditor, this.tilesetEditor);
        }
        private void LoadTilesetEditor()
        {
            if (tilesetEditor == null)
            {
                tilesetEditor = new TilesetEditor(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
                tilesetEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilesetEditor.Reload(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
            tilesetEditor.EnableLayers(true, false, false);
        }
        private void LoadPreviewer()
        {
            if (previewer == null)
            {
                previewer = new Previewer(Index, EType.MineCart);
                previewer.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                previewer.Reload(Index, EType.MineCart);
        }
        //
        public void Assemble()
        {
            Model.ROM[0x0393EF] = (byte)this.music.SelectedIndex;
            MinecartData.Assemble();
            tilemap.Assemble();
            Model.MinecartM7PaletteSet.Assemble();
            Model.MinecartObjectPaletteSet.Assemble();
            Model.MinecartSSPaletteSet.Assemble();
            //
            int offset = 0x1E;
            byte[] dst = new byte[0x8000];
            Bits.SetShort(dst, 0x00, offset);
            if (!Model.Compress(Model.MinecartM7Graphics, dst, ref offset, 0x8000 - 0x1E, "stage 1,3 graphics"))
                return;
            Bits.SetShort(dst, 0x02, offset);
            if (!Model.Compress(Model.MinecartM7TilesetSubtiles, dst, ref offset, 0x8000 - 0x1E, "stage 1,3 subtiles"))
                return;
            Bits.SetShort(dst, 0x04, offset);
            if (!Model.Compress(Model.MinecartM7Palettes, dst, ref offset, 0x8000 - 0x1E, "stage 1,3 palettes"))
                return;
            Bits.SetShort(dst, 0x06, offset);
            if (!Model.Compress(Model.MinecartM7TilesetPalettes, dst, ref offset, 0x8000 - 0x1E, "stage 1,3 subtile palettes"))
                return;
            Bits.SetShort(dst, 0x08, offset);
            if (!Model.Compress(Model.MinecartM7TilemapA, dst, ref offset, 0x8000 - 0x1E, "stage 1 tilemap"))
                return;
            Bits.SetShort(dst, 0x0A, offset);
            if (!Model.Compress(Model.MinecartM7TilemapB, dst, ref offset, 0x8000 - 0x1E, "stage 3 tilemap"))
                return;
            Bits.SetShort(dst, 0x0C, offset);
            if (!Model.Compress(Model.MinecartM7TilesetBG, dst, ref offset, 0x8000 - 0x1E, "stage 1,3 background tileset"))
                return;
            Bits.SetShort(dst, 0x0E, offset);
            if (!Model.Compress(Model.MinecartObjectGraphics, dst, ref offset, 0x8000 - 0x1E, "object graphics"))
                return;
            Bits.SetShort(dst, 0x10, offset);
            if (!Model.Compress(Model.MinecartObjectPalettes, dst, ref offset, 0x8000 - 0x1E, "object palettes"))
                return;
            Bits.SetShort(dst, 0x12, offset);
            if (!Model.Compress(Model.MinecartSSTilemap, dst, ref offset, 0x8000 - 0x1E, "stage 2,4 tilemap"))
                return;
            Bits.SetShort(dst, 0x14, offset);
            if (!Model.Compress(Model.MinecartSSGraphics, dst, ref offset, 0x8000 - 0x1E, "stage 2,4 graphics"))
                return;
            Bits.SetShort(dst, 0x16, offset);
            if (!Model.Compress(Model.MinecartSSTileset, dst, ref offset, 0x8000 - 0x1E, "stage 2,4 tileset"))
                return;
            Bits.SetShort(dst, 0x18, offset);
            if (!Model.Compress(Model.MinecartSSPalettes, dst, ref offset, 0x8000 - 0x1E, "stage 2,4 palettes"))
                return;
            Bits.SetShort(dst, 0x1A, offset);
            if (!Model.Compress(Model.MinecartSSBGTileset, dst, ref offset, 0x8000 - 0x1E, "stage 4 BG tileset"))
                return;
            Bits.SetShort(dst, 0x1C, offset);
            if (!Model.Compress(Model.MinecartObjects, dst, ref offset, 0x8000 - 0x1E, "object & screen data"))
                return;
            //
            Bits.SetBytes(Model.ROM, 0x388000, dst);
            Bits.SetShort(Model.ROM, 0x039670, (ushort)startX.Value);
            Bits.SetShort(Model.ROM, 0x039679, (ushort)startY.Value);
        }
        #endregion
        #region Event Handlers
        private void levelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshLevel();
            settings.LastMineCart = levelName.SelectedIndex;
        }
        private void music_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Modified = true;
        }
        private void stagePalettesMenuItem_Click(object sender, EventArgs e)
        {
            stagePaletteEditor.Show();
        }
        private void spritePalettesMenuItem_Click(object sender, EventArgs e)
        {
            spritePaletteEditor.Show();
        }
        private void stageGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stageGraphicEditor.Show();
        }
        private void spriteGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spriteGraphicEditor.Show();
        }
        private void previewButton_Click(object sender, EventArgs e)
        {
            LoadPreviewer();
            previewer.Show();
        }
        private void buttonObjects_Click(object sender, EventArgs e)
        {
            panelObjects.Visible = buttonObjects.Checked;
            pictureBoxScreens.Invalidate();
        }
        //
        private void pictureBoxScreens_Paint(object sender, PaintEventArgs e)
        {
            if (L1Indexes.Count == 0)
                return;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            Rectangle dst;
            Rectangle src;
            // draw screens
            for (int i = 0; i < L1Indexes.Count; i++)
            {
                dst = new Rectangle(i * 256, 0, 256, 256);
                if (L1Indexes[i] < L1Indexes.Count)
                {
                    src = new Rectangle(0, 0, 256, 256);
                    if (screenIndex < screenImages.Count)
                    {
                        if (Index == 3 && i % 2 == 0)
                        {
                            if (screenBGImage == null)
                            {
                                int[] BGPixels = Do.TilesetToPixels(bgtileset.Tileset_tiles, 32, 16, 0, false);
                                screenBGImage = Do.PixelsToImage(BGPixels, 512, 256);
                            }
                            dst.Width = 512;
                            src.Width = 512;
                            e.Graphics.DrawImage(screenBGImage, dst, src, GraphicsUnit.Pixel);
                            dst.Width = 256;
                            src.Width = 256;
                        }
                        e.Graphics.DrawImage(screenImages[i], dst, src, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    Font font = new Font("Tahoma", 10F, FontStyle.Bold);
                    SizeF size = e.Graphics.MeasureString("(INVALID SCREEN INDEX)", font, new PointF(0, 0), StringFormat.GenericDefault);
                    Point point = new Point(((256 - (int)size.Width) / 2) + (i * 256), (256 - (int)size.Height) / 2);
                    Do.DrawString(e.Graphics, point, "(INVALID SCREEN INDEX)", Color.Black, Color.Red, font);
                }
                if (this.screenIndex == i)
                {
                    Pen pen = new Pen(new SolidBrush(Color.Gray), 2);
                    pen.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawRectangle(pen, new Rectangle(i * 256, 0, 256 - 1, 256 - 1));
                }
            }
            // draw objects
            for (int i = 0; buttonObjects.Checked && i < minecartObjects.Count; i++)
            {
                MCObject obj = minecartObjects[i];
                Pen pen;
                if (objectIndex == i)
                    pen = new Pen(Color.Red, 2);
                else
                    pen = new Pen(Color.Red, 1);
                e.Graphics.DrawRectangle(pen, new Rectangle(obj.X - 1, obj.Y - 1, obj.Count * 32 - 16 + 2, 16 + 2));
                Bitmap image = obj.Type == 1 ? MinecartData.Coin : MinecartData.Mushroom;
                for (int x = 0; x < obj.Count; x++)
                    e.Graphics.DrawImage(image, x * 32 + obj.X, obj.Y, 16, 16);
            }
        }
        private void pictureBoxScreens_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - diffX; int y = e.Y - diffY;
            if (x > pictureBoxScreens.Width - 1) x = pictureBoxScreens.Width - 1;
            if (x < 0) x = 0;
            if (y > pictureBoxScreens.Height - 1) y = pictureBoxScreens.Height - 1;
            if (y < 0) y = 0;
            //
            if (mouseDownObject >= 0 && e.Button == MouseButtons.Left)
            {
                objectX.Value = Math.Max(256, x);
                objectY.Value = y;
            }
            else if (buttonObjects.Checked)
            {
                for (int i = 0; i < minecartObjects.Count; i++)
                {
                    MCObject mco = minecartObjects[i];
                    if (x >= mco.X && x < mco.X + mco.Width &&
                        y >= mco.Y && y < mco.Y + 16)
                    {
                        pictureBoxScreens.Cursor = Cursors.Hand;
                        mouseOverObject = i;
                        break;
                    }
                    else
                    {
                        pictureBoxScreens.Cursor = Cursors.Arrow;
                        mouseOverObject = -1;
                    }
                }
            }
        }
        private void pictureBoxScreens_MouseDown(object sender, MouseEventArgs e)
        {
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            //
            int x = e.X;
            int y = e.Y;
            if (buttonObjects.Checked && mouseOverObject >= 0 && e.Button == MouseButtons.Left)
            {
                mouseDownObject = mouseOverObject;
                objectIndex = mouseDownObject;
                diffX = (int)(x - minecartObject.X);
                diffY = (int)(y - minecartObject.Y);
            }
            //
            screenIndex = e.X / 256;
            screens.AutoScrollPosition = new Point(autoScrollPosX, autoScrollPosY);
            pictureBoxScreens.Invalidate();
        }
        private void pictureBoxScreens_MouseUp(object sender, MouseEventArgs e)
        {
            diffX = 0;
            diffY = 0;
        }
        private void pictureBoxScreens_MouseEnter(object sender, EventArgs e)
        {
            //if (GetForegroundWindow() == MiniGames.Handle)
            //    pictureBoxScreens.Focus();
            //pictureBoxScreens.Invalidate();
        }
        private void pictureBoxScreens_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Right || e.KeyData == Keys.Down)
                screenIndex++;
            if (e.KeyData == Keys.Left || e.KeyData == Keys.Up)
                screenIndex--;
        }
        private void screens_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBoxScreens.Invalidate();
            screens.Invalidate();
        }
        // screen data
        private void screenWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (Index == 2)
                MinecartData.WidthA = (int)screenWidth.Value;
            else
                MinecartData.WidthB = (int)screenWidth.Value;
        }
        private void screenL1Number_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            L1Indexes[screenIndex] = (int)screenL1Number.Value;
            SetScreenImage();
        }
        private void screenL2Number_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            L2Indexes[screenIndex] = (int)screenL2Number.Value;
            SetScreenImage();
        }
        private void newScreen_Click(object sender, EventArgs e)
        {
            if (L1Indexes.Count >= 255)
            {
                MessageBox.Show("Cannot have more than 255 screens.");
                return;
            }
            L1Indexes.Insert(screenIndex, 0);
            if (Index == 2)
                L2Indexes.Insert(screenIndex, 0);
            screenImages.Insert(screenIndex, new Bitmap(256, 256));
            screenIndex++;
            //
            pictureBoxScreens.Width = L1Indexes.Count * 256;
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            screens.AutoScrollPosition = new Point(autoScrollPosX + 256, autoScrollPosY);
            //
            SetScreenImage();
        }
        private void deleteScreen_Click(object sender, EventArgs e)
        {
            if (L1Indexes.Count == 0)
                return;
            int index = screenIndex;
            L1Indexes.RemoveAt(screenIndex);
            if (Index == 2)
                L2Indexes.RemoveAt(screenIndex);
            screenImages.RemoveAt(screenIndex);
            if (index >= L1Indexes.Count)
                screenIndex--;
            //
            pictureBoxScreens.Width = L1Indexes.Count * 256;
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            screens.AutoScrollPosition = new Point(autoScrollPosX - 256, autoScrollPosY);
        }
        private void duplicateScreen_Click(object sender, EventArgs e)
        {
            if (L1Indexes.Count >= 255)
            {
                MessageBox.Show("Cannot have more than 255 screens.");
                return;
            }
            L1Indexes.Insert(screenIndex, L1Indexes[screenIndex]);
            if (Index == 2)
                L2Indexes.Insert(screenIndex, L2Indexes[screenIndex]);
            screenImages.Insert(screenIndex, screenImages[screenIndex]);
            screenIndex++;
            //
            pictureBoxScreens.Width = L1Indexes.Count * 256;
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            screens.AutoScrollPosition = new Point(autoScrollPosX + 256, autoScrollPosY);
            //
            SetScreenImage();
        }
        private void moveScreenBack_Click(object sender, EventArgs e)
        {
            if (screenIndex <= 0)
                return;
            L1Indexes.Reverse(screenIndex - 1, 2);
            if (Index == 2)
                L2Indexes.Reverse(screenIndex - 1, 2);
            screenImages.Reverse(screenIndex - 1, 2);
            screenIndex--;
            //
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            screens.AutoScrollPosition = new Point(autoScrollPosX - 256, autoScrollPosY);
            //
            pictureBoxScreens.Invalidate();
        }
        private void moveScreenFoward_Click(object sender, EventArgs e)
        {
            if (screenIndex >= L1Indexes.Count - 1)
                return;
            L1Indexes.Reverse(screenIndex, 2);
            if (Index == 2)
                L2Indexes.Reverse(screenIndex, 2);
            screenImages.Reverse(screenIndex, 2);
            screenIndex++;
            //
            int autoScrollPosX = Math.Abs(screens.AutoScrollPosition.X);
            int autoScrollPosY = Math.Abs(screens.AutoScrollPosition.Y);
            screens.AutoScrollPosition = new Point(autoScrollPosX + 256, autoScrollPosY);
            //
            pictureBoxScreens.Invalidate();
        }
        // object data
        private void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshObject();
            pictureBoxScreens.Invalidate();
        }
        private void newObject_Click(object sender, EventArgs e)
        {
            if (minecartObjects.Count >= 255)
            {
                MessageBox.Show("Cannot have more than 255 objects.");
                return;
            }
            int index = objectIndex;
            int x = Math.Abs(screens.AutoScrollPosition.X);
            MCObject newObject = new MCObject(1, Math.Max(272, x + 16), 16, 1);
            minecartObjects.Insert(objectIndex + 1, newObject);
            listBoxObjects.Items.Clear();
            for (int i = 0; i < minecartObjects.Count; i++)
                listBoxObjects.Items.Add("Object " + i);
            objectIndex = index + 1;
        }
        private void deleteObject_Click(object sender, EventArgs e)
        {
            int index = objectIndex;
            minecartObjects.RemoveAt(index);
            listBoxObjects.Items.Clear();
            for (int i = 0; i < minecartObjects.Count; i++)
                listBoxObjects.Items.Add("Object " + i);
            if (index < listBoxObjects.Items.Count)
                objectIndex = index;
            else
                objectIndex = index - 1;
        }
        private void duplicateObject_Click(object sender, EventArgs e)
        {
            if (minecartObjects.Count >= 255)
            {
                MessageBox.Show("Cannot have more than 255 objects.");
                return;
            }
            int index = objectIndex;
            minecartObjects.Insert(objectIndex, minecartObject.Copy());
            listBoxObjects.Items.Clear();
            for (int i = 0; i < minecartObjects.Count; i++)
                listBoxObjects.Items.Add("Object " + i);
            objectIndex = index + 1;
        }
        private void moveObjectBack_Click(object sender, EventArgs e)
        {
            if (objectIndex == 0)
                return;
            minecartObjects.Reverse(objectIndex - 1, 2);
            objectIndex--;
        }
        private void moveObjectForward_Click(object sender, EventArgs e)
        {
            if (objectIndex == listBoxObjects.Items.Count - 1)
                return;
            minecartObjects.Reverse(objectIndex, 2);
            objectIndex++;
        }
        private void objectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            minecartObject.Type = (int)objectType.SelectedIndex;
            pictureBoxScreens.Invalidate();
        }
        private void rowSize_ValueChanged(object sender, EventArgs e)
        {
            minecartObject.Count = (int)rowSize.Value;
            pictureBoxScreens.Invalidate();
        }
        private void objectX_ValueChanged(object sender, EventArgs e)
        {
            minecartObject.X = (int)objectX.Value;
            pictureBoxScreens.Invalidate();
        }
        private void objectY_ValueChanged(object sender, EventArgs e)
        {
            minecartObject.Y = (int)objectY.Value;
            pictureBoxScreens.Invalidate();
        }
        //
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
