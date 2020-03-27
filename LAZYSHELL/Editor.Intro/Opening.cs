using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class Opening : NewForm
    {
        // variables
        private delegate void Function();
        private Tileset tileset;
        private byte[] openingGraphics;
        private byte[] openingTileset;
        private Bitmap tilesetImage;
        private Intro intro;
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        private PaletteSet paletteSet { get { return Model.OpeningPalette; } set { Model.OpeningPalette = value; } }
        // constructor
        public Opening(Intro intro)
        {
            this.intro = intro;
            //
            openingTileset = Bits.GetBytes(Model.OpeningData, 0, 0x480);
            openingGraphics = Bits.GetBytes(Model.OpeningData, 0x480);
            tileset = new Tileset(openingTileset, openingGraphics, paletteSet, 16, 9, TilesetType.Opening);
            InitializeComponent();
            if (Model.ROM[0x00087D] == 0xEA &&
                Model.ROM[0x00087E] == 0xEA &&
                Model.ROM[0x00087F] == 0xEA &&
                Model.ROM[0x000880] == 0xEA)
                disableGardenLoad.Checked = true;
            if (Model.ROM[0x034872] == 0x4C &&
                Model.ROM[0x034873] == 0x44 &&
                Model.ROM[0x034874] == 0x00)
                disableGardenNew.Checked = true;
            //
            SetTilesetImage();
        }
        // functions
        private void SetTilesetImage()
        {
            int[] pixels = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 9, 0, false);
            tilesetImage = Do.PixelsToImage(pixels, 256, 144);
            pictureBox1.Invalidate();
        }
        private void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), paletteSet, 1, 0, 1);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), paletteSet, 1, 0, 1);
        }
        private void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate), openingGraphics,
                    openingGraphics.Length, 0, paletteSet, 0, 0x10);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate), openingGraphics,
                    openingGraphics.Length, 0, paletteSet, 0, 0x10);
        }
        // updating
        private void PaletteUpdate()
        {
            tileset = new Tileset(openingTileset, openingGraphics, paletteSet, 16, 9, TilesetType.Opening);
            SetTilesetImage();
            LoadGraphicEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdate()
        {
            tileset.Assemble(16);
            tileset = new Tileset(openingTileset, openingGraphics, paletteSet, 16, 9, TilesetType.Opening);
            SetTilesetImage();
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
        }
        public void Assemble()
        {
            paletteSet.Assemble();
            tileset.Assemble(256);
            if (Model.Compress(Model.OpeningData, 0x3F1913, 0x17C0, 0x85A, "Opening"))
                this.Modified = false;
            //
            if (disableGardenLoad.Checked)
            {
                Model.ROM[0x00087D] = 0xEA;
                Model.ROM[0x00087E] = 0xEA;
                Model.ROM[0x00087F] = 0xEA;
                Model.ROM[0x000880] = 0xEA;
            }
            else
            {
                Model.ROM[0x00087D] = 0x22;
                Model.ROM[0x00087E] = 0x00;
                Model.ROM[0x00087F] = 0x00;
                Model.ROM[0x000880] = 0xC3;
            }
            if (disableGardenNew.Checked)
            {
                Model.ROM[0x034872] = 0x4C;
                Model.ROM[0x034873] = 0x44;
                Model.ROM[0x034874] = 0x00;
            }
            else
            {
                Model.ROM[0x034872] = 0x82;
                Model.ROM[0x034873] = 0xA8;
                Model.ROM[0x034874] = 0x6D;
            }
        }
        // event handlers
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (toggleBG.Checked)
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(paletteSet.Palettes[0][0])),
                    new Rectangle(new Point(0, 0), pictureBox1.Size));
            if (tilesetImage != null)
                e.Graphics.DrawImage(tilesetImage, 0, 0);
        }
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
        private void importImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
            openFileDialog1.Title = "Import opening credits";
            openFileDialog1.Filter = "Image files (*.gif,*.jpg,*.png)|*.gif;*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if (openFileDialog1.FileName == null)
                return;
            Bitmap import = new Bitmap(Image.FromFile(openFileDialog1.FileName));
            if (import.Width != 256 || import.Height != 144)
            {
                MessageBox.Show(
                    "The dimensions of the imported image must be 256 x 144.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            byte[] graphics = new byte[0x4000];
            int[] palette = paletteSet.Palettes[0];
            Do.PixelsToBPP(
                Do.ImageToPixels(import, new Size(256, 144), new Rectangle(0, 0, 256, 144)), graphics,
                new Size(256 / 8, 144 / 8), palette, 0x10);
            byte[] tileset = new byte[0x800];
            byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
            Do.CopyToTileset(graphics, tileset, palette, 0, true, false, 0x10, 2, new Size(256, 144), 0);
            Buffer.BlockCopy(tileset, 0, openingTileset, 0, 0x480);
            Buffer.BlockCopy(graphics, 0, openingGraphics, 0, 0x1340);
            this.tileset = new Tileset(openingTileset, openingGraphics, paletteSet, 16, 9, TilesetType.Opening);
            SetTilesetImage();
        }
        private void exportImage_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "openingCredits.png");
        }
        private void toggleBG_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void disableGardenLoad_CheckedChanged(object sender, EventArgs e)
        {
            this.Modified = true;
        }
        private void disableGardenNew_CheckedChanged(object sender, EventArgs e)
        {
            this.Modified = true;
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
    }
}
