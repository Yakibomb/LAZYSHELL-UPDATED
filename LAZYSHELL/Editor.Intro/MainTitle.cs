using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class MainTitle : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private PaletteSet paletteSet { get { return Model.TitlePalettes; } set { Model.TitlePalettes = value; } }
        private PaletteSet spritePaletteSet { get { return Model.TitleSpritePalettes; } set { Model.TitleSpritePalettes = value; } }
        private Tileset tileset { get { return Model.TitleTileSet; } set { Model.TitleTileSet = value; } }
        private Overlay overlay = new Overlay();
        private Bitmap[] tilesetImage = new Bitmap[3];
        private int layer { get { return tilesetEditor.Layer; } set { tilesetEditor.Layer = value; } }
        // editors
        private Intro intro;
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        private PaletteEditor spritePaletteEditor;
        private GraphicEditor spriteGraphicEditor;
        private TilesetEditor tilesetEditor;
        #endregion
        #region Functions
        public MainTitle(Intro intro)
        {
            this.intro = intro;
            //
            InitializeComponent();
            pictureBoxTitle.Invalidate();
            LoadTilesetEditor();
            //LoadPaletteEditor();
            //LoadGraphicEditor();
            //LoadSpritePaletteEditor();
            //LoadSpriteGraphicEditor();
            // load the individual editors
            tilesetEditor.TopLevel = false;
            tilesetEditor.Dock = DockStyle.Left;
            this.panel2.Controls.Add(tilesetEditor);
            tilesetEditor.BringToFront();
            tilesetEditor.Show();
            SetTilesetImages();
            //
            GC.Collect();
            this.History = new History(this);
        }
        // assemblers
        public void Assemble()
        {
            // Palette set
            paletteSet.Assemble();
            spritePaletteSet.Assemble();
            tileset.Assemble(16);
            // Tilesets
            if (Model.Compress(Model.TitleData, 0x3F216E, 0xDA60, 0x7E92, "Main title"))
                this.Modified = false;
        }
        // drawing
        private void SetTilesetImages()
        {
            int[] pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[0], 16, 32, 0, false);
            tilesetImage[0] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[1], 16, 32, 0, false);
            tilesetImage[1] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[2], 16, 6, 0, false);
            tilesetImage[2] = Do.PixelsToImage(pixels, 256, 96);
            pictureBoxTitle.Invalidate();
        }
        // loading
        private void LoadTilesetEditor()
        {
            if (tilesetEditor == null)
            {
                tilesetEditor = new TilesetEditor(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
                tilesetEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                tilesetEditor.AutoUpdate = true;
            }
            else
                tilesetEditor.Reload(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
        }
        private void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), paletteSet, 8, 0, 8);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), paletteSet, 8, 0, 8);
        }
        private void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    layer != 2 ? tileset.Graphics : tileset.GraphicsL3,
                    layer != 2 ? tileset.Graphics.Length : tileset.GraphicsL3.Length,
                    0, paletteSet, 0, 0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    layer != 2 ? tileset.Graphics : tileset.GraphicsL3,
                    layer != 2 ? tileset.Graphics.Length : tileset.GraphicsL3.Length,
                    0, paletteSet, 0, 0x20);
        }
        private void LoadSpritePaletteEditor()
        {
            if (spritePaletteEditor == null)
            {
                spritePaletteEditor = new PaletteEditor(new Function(SpritePaletteUpdate), spritePaletteSet, 5, 0, 5);
                spritePaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                spritePaletteEditor.Reload(new Function(SpritePaletteUpdate), spritePaletteSet, 5, 0, 5);
        }
        private void LoadSpriteGraphicEditor()
        {
            if (spriteGraphicEditor == null)
            {
                spriteGraphicEditor = new GraphicEditor(new Function(SpriteGraphicUpdate),
                    Model.TitleSpriteGraphics, Model.TitleSpriteGraphics.Length, 0, spritePaletteSet, 0, 0x20);
                spriteGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                spriteGraphicEditor.Reload(new Function(SpriteGraphicUpdate),
                    Model.TitleSpriteGraphics, Model.TitleSpriteGraphics.Length, 0, spritePaletteSet, 0, 0x20);
        }
        // updating
        private void PaletteUpdate()
        {
            tileset = new Tileset(paletteSet, "title");
            SetTilesetImages();
            LoadGraphicEditor();
            LoadTilesetEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdate()
        {
            tileset.Assemble(16);
            tileset = new Tileset(paletteSet, "title");
            if (layer != 2)
                tileset.Graphics = graphicEditor.Graphics;
            else
                tileset.GraphicsL3 = graphicEditor.Graphics;
            SetTilesetImages();
            LoadTilesetEditor();
        }
        private void SpritePaletteUpdate()
        {
            LoadSpriteGraphicEditor();
        }
        private void SpriteGraphicUpdate()
        {
        }
        private void TilesetUpdate()
        {
            tileset.Assemble(16);
            SetTilesetImages();
        }
        public void CloseEditors()
        {
            if (paletteEditor != null)
            {
                paletteEditor.Close();
                paletteEditor.Dispose();
            }
            if (graphicEditor != null)
            {
                graphicEditor.Close();
                graphicEditor.Dispose();
            }
            if (spritePaletteEditor != null)
            {
                spritePaletteEditor.Close();
                spritePaletteEditor.Dispose();
            }
            if (spriteGraphicEditor != null)
            {
                spriteGraphicEditor.Close();
                spriteGraphicEditor.Dispose();
            }
            tilesetEditor.Close();
            tilesetEditor.Dispose();
        }
        #endregion
        #region Event Handlers
        private void pictureBoxTitle_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage[0] != null && tilesetImage[1] != null && tilesetImage[2] != null)
            {
                Color bgcolor = Color.FromArgb(paletteSet.Palette[0]);
                e.Graphics.FillRectangle(new SolidBrush(bgcolor), new Rectangle(new Point(0, 0), pictureBoxTitle.Size));
                e.Graphics.DrawImage(tilesetImage[1], 0, 0);
                e.Graphics.DrawImage(tilesetImage[0], 0, 0);
                Rectangle upperPart = new Rectangle(0, 0, 256, 72);
                Rectangle lowerPart = new Rectangle(0, 72, 256, 24);
                e.Graphics.DrawImage(tilesetImage[2].Clone(upperPart, PixelFormat.DontCare), 0, 208);
                e.Graphics.DrawImage(tilesetImage[2].Clone(lowerPart, PixelFormat.DontCare), 0, 368);
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        // editors
        private void openPalettes_Click(object sender, EventArgs e)
        {
            if (paletteEditor == null)
                LoadPaletteEditor();
            paletteEditor.Show();
        }
        private void openGraphics_Click(object sender, EventArgs e)
        {
            if (graphicEditor == null)
                LoadGraphicEditor();
            graphicEditor.Show();
        }
        private void openSpritePalettes_Click(object sender, EventArgs e)
        {
            if (spritePaletteEditor == null)
                LoadSpritePaletteEditor();
            spritePaletteEditor.Show();
        }
        private void openSpriteGraphics_Click(object sender, EventArgs e)
        {
            if (spriteGraphicEditor == null)
                LoadSpriteGraphicEditor();
            spriteGraphicEditor.Show();
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
