using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class Opening : NewForm
    {
        // variables
        private delegate void Function();
        private byte[] bootupLogoData;
        private Tileset tileset;
        private byte[] titleCardGraphics;
        private byte[] bootupLogoGraphics;
        private byte[] titleCardTileset;
        private Bitmap tilesetImage;
        private Intro intro;
        private PaletteEditor paletteEditorBootup;
        private PaletteEditor paletteEditorTitleCard;
        private GraphicEditor graphicEditorBootup;
        private GraphicEditor graphicEditorTitleCard;
        private PaletteSet paletteSetTitleCard { get { return Model.OpeningPalette; } set { Model.OpeningPalette = value; } }
        private PaletteSet paletteSetBootupLogo { get { return Model.BootupPalettes; } set { Model.BootupPalettes = value; } }
        // constructor
        public Opening(Intro intro)
        {
            this.intro = intro;
            //
            titleCardTileset = Bits.GetBytes(Model.OpeningData, 0, 0x480);
            titleCardGraphics = Bits.GetBytes(Model.OpeningData, 0x480);
            tileset = new Tileset(titleCardTileset, titleCardGraphics, paletteSetTitleCard, 16, 9, TilesetType.Opening);
            //
            bootupLogoData = Model.BootupLogoData;
            bootupLogoGraphics = Bits.GetBytes(bootupLogoData, 0x510);
            //
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
            //
        }
        // functions
        private void SetTilesetImage()
        {
            int[] pixels = Do.TilesetToPixels(tileset.Tileset_tiles, 16, 9, 0, false);
            tilesetImage = Do.PixelsToImage(pixels, 256, 144);
            pictureBox1.Invalidate();
        }
        private void LoadPaletteEditorBootup()
        {
            if (paletteEditorBootup == null)
            {
                paletteEditorBootup = new PaletteEditor(new Function(PaletteUpdateBootup), paletteSetBootupLogo, 1, 0, 1);
                paletteEditorBootup.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditorBootup.Reload(new Function(PaletteUpdateBootup), paletteSetBootupLogo, 1, 0, 1);
        }
        private void LoadGraphicEditorBootup()
        {
            if (graphicEditorBootup == null)
            {
                graphicEditorBootup = new GraphicEditor(new Function(GraphicUpdateBootup), bootupLogoGraphics,
                    bootupLogoGraphics.Length, 0, paletteSetBootupLogo, 0, 0x20);
                graphicEditorBootup.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditorBootup.Reload(new Function(GraphicUpdateBootup), bootupLogoGraphics,
                    bootupLogoGraphics.Length, 0, paletteSetBootupLogo, 0, 0x20);
        }
        private void LoadPaletteEditorTitleCard()
        {
            if (paletteEditorTitleCard == null)
            {
                paletteEditorTitleCard = new PaletteEditor(new Function(PaletteUpdateTitleCard), paletteSetTitleCard, 1, 0, 1);
                paletteEditorTitleCard.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditorTitleCard.Reload(new Function(PaletteUpdateTitleCard), paletteSetTitleCard, 1, 0, 1);
        }
        private void LoadGraphicEditorTitleCard()
        {
            if (graphicEditorTitleCard == null)
            {
                graphicEditorTitleCard = new GraphicEditor(new Function(GraphicUpdateTitleCard), titleCardGraphics,
                    titleCardGraphics.Length, 0, paletteSetTitleCard, 0, 0x10);
                graphicEditorTitleCard.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditorTitleCard.Reload(new Function(GraphicUpdateTitleCard), titleCardGraphics,
                    titleCardGraphics.Length, 0, paletteSetTitleCard, 0, 0x10);
        }
        // updating
        private void PaletteUpdateTitleCard()
        {
            tileset = new Tileset(titleCardTileset, titleCardGraphics, paletteSetTitleCard, 16, 9, TilesetType.Opening);
            SetTilesetImage();
            LoadGraphicEditorTitleCard();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdateTitleCard()
        {
            tileset.Assemble(16);
            tileset = new Tileset(titleCardTileset, titleCardGraphics, paletteSetTitleCard, 16, 9, TilesetType.Opening);
            SetTilesetImage();
        }
        private void PaletteUpdateBootup()
        {
            LoadGraphicEditorBootup();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdateBootup()
        {
        }
        public void CloseEditors()
        {
            if (paletteEditorBootup != null)
            {
                paletteEditorBootup.Close();
                paletteEditorBootup.Dispose();
            }
            if (graphicEditorBootup != null)
            {
                graphicEditorBootup.Close();
                graphicEditorBootup.Dispose();
            }
            if (paletteEditorTitleCard != null)
            {
                paletteEditorTitleCard.Close();
                paletteEditorTitleCard.Dispose();
            }
            if (graphicEditorTitleCard != null)
            {
                graphicEditorTitleCard.Close();
                graphicEditorTitleCard.Dispose();
            }
        }
        public void Assemble()
        {
            paletteSetBootupLogo.Assemble();
            paletteSetTitleCard.Assemble();
            tileset.Assemble(256);
            if (Model.Compress(Model.OpeningData, 0x3F1913, 0x17C0, 0x85A, "Title Card"))
                this.Modified = false;
            //
        /* Couldn't get Bootup Logo Graphics to work sadly, due to the weird compression of it
            // bootupLogoGraphics.CopyTo(bootupLogoGFX, 0x510);
            //  public static bool Compress(bootupLogoGFX, byte[] dst, ref int offset, int maxComp, string label)
            byte[] BootupLogoGFX_compressed = new byte[0x2200];
            int offset = 0x0;
            Model.Compress(bootupLogoGraphics, BootupLogoGFX_compressed, ref offset, 0x1BFB, "Bootup Logo");
            byte[] bootupLogoGFX = new byte[0x2710];
            Array.Copy(bootupLogoData, 0, bootupLogoGFX, 0, 0x510);
            Array.Copy(BootupLogoGFX_compressed, 0, bootupLogoGFX, 0x510, BootupLogoGFX_compressed.Length);
            bootupLogoGFX.CopyTo(Model.ROM, 0x3EFD18);
            //    if (Model.Compress(bootupLogoGFX, 0x3EFD17, 0x2710, 0x1BFB, "Bootup Logo"))
            //       this.Modified = false;
            //bootupLogoData.CopyTo(bootupLogoGFX, 0x3EFD18);
            //
        */
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
            if (!toggleBG.Checked)
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(paletteSetTitleCard.Palettes[0][0])),
                    new Rectangle(new Point(0, 0), pictureBox1.Size));
            if (tilesetImage != null)
                e.Graphics.DrawImage(tilesetImage, 0, 0);
        }
        private void openPalettesTitleCard_Click(object sender, EventArgs e)
        {
            if (paletteEditorTitleCard == null)
                LoadPaletteEditorTitleCard();
            paletteEditorTitleCard.Show();
        }
        private void openGraphicsTitleCard_Click(object sender, EventArgs e)
        {
            if (graphicEditorTitleCard == null)
                LoadGraphicEditorTitleCard();
            graphicEditorTitleCard.Show();
        }
        private void openPalettesBootup_Click(object sender, EventArgs e)
        {
            if (paletteEditorBootup == null)
                LoadPaletteEditorBootup();
            paletteEditorBootup.Show();
        }
        private void openGraphicsBootup_Click(object sender, EventArgs e)
        {
            if (graphicEditorBootup == null)
                LoadGraphicEditorBootup();
            graphicEditorBootup.Show();
        }
        private void importImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
            openFileDialog1.Title = "Import opening title cards";
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
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            byte[] graphics = new byte[0x4000];
            int[] palette = paletteSetTitleCard.Palettes[0];
            Do.PixelsToBPP(
                Do.ImageToPixels(import, new Size(256, 144), new Rectangle(0, 0, 256, 144)), graphics,
                new Size(256 / 8, 144 / 8), palette, 0x10);
            byte[] tileset = new byte[0x800];
            byte[] temp = new byte[graphics.Length]; graphics.CopyTo(temp, 0);
            Do.CopyToTileset(graphics, tileset, palette, 0, true, false, 0x10, 2, new Size(256, 144), 0);
            Buffer.BlockCopy(tileset, 0, titleCardTileset, 0, 0x480);
            Buffer.BlockCopy(graphics, 0, titleCardGraphics, 0, 0x1340);
            this.tileset = new Tileset(titleCardTileset, titleCardGraphics, paletteSetTitleCard, 16, 9, TilesetType.Opening);
            SetTilesetImage();
        }
        private void exportImage_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "openingTitleCards.png");
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
