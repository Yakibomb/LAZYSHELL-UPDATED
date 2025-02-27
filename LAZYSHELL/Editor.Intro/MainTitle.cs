using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

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
        private Bitmap[]
            tilesetImage = new Bitmap[3],
            tilesetImageP1 = new Bitmap[3],
            spriteImage = new Bitmap[3];
            
        private int layer { get { return tilesetEditor.Layer; } set { tilesetEditor.Layer = value; } }
        // coordinates
        private int TitleX
        {
            get
            {
                return coords.TitleX_b1;
            }
            set
            {
                coords.TitleX_b1 = (byte)(value & 0x1F);
            }
        }
        private int TitleX_Alt
        {
            get
            {
                return coords.TitleX_Alt_b1;
            }
            set
            {
                coords.TitleX_Alt_b1 = (byte)(value & 0x1F);
            }
        }
        private int TitleY
        {
            get
            {
                return coords.TitleY_b1 + (coords.TitleY_b2 * 64);
            }
            set
            {
                coords.TitleY_b1 = (byte)(value % 64);
                coords.TitleY_b2 = (byte)(value / 64);
            }
        }
        private int TitleY_Alt
        {
            get
            {
                return coords.TitleY_Alt_b1 + (coords.TitleY_Alt_b2 * 64);
            }
            set
            {
                coords.TitleY_Alt_b1 = (byte)(value % 64);
                coords.TitleY_Alt_b2 = (byte)(value / 64);
            }
        }
        //
        private int BannerX
        {
            get
            {
                return coords.BannerX_b1;
            }
            set
            {
                coords.BannerX_b1 = (byte)(value & 0x1F);
            }
        }
        private int BannerX_Alt
        {
            get
            {
                return coords.BannerX_Alt_b1;
            }
            set
            {
                coords.BannerX_Alt_b1 = (byte)(value & 0x1F);
            }
        }
        private int BannerY
        {
            get
            {
                return coords.BannerY_b1 + (coords.BannerY_b2 * 64);
            }
            set
            {
                coords.BannerY_b1 = (byte)(value % 64);
                coords.BannerY_b2 = (byte)(value / 64);
            }
        }
        private int BannerY_Alt
        {
            get
            {
                return coords.BannerY_Alt_b1 + (coords.BannerY_Alt_b2 * 64);
            }
            set
            {
                coords.BannerY_Alt_b1 = (byte)(value % 64);
                coords.BannerY_Alt_b2 = (byte)(value / 64);
            }
        }
        //
        private int CreditsX
        {
            get
            {
                return coords.CreditsX_b1;
            }
            set
            {
                coords.CreditsX_b1 = (byte)(value & 0x1F);
            }
        }
        private int CreditsX_Alt
        {
            get
            {
                return coords.CreditsX_Alt_b1;
            }
            set
            {
                coords.CreditsX_Alt_b1 = (byte)(value & 0x1F);
            }
        }
        private int CreditsY
        {
            get
            {
                return coords.CreditsY_b1 + (coords.CreditsY_b2 * 64);
            }
            set
            {
                coords.CreditsY_b1 = (byte)(value % 64);
                coords.CreditsY_b2 = (byte)(value / 64);
            }
        }
        private int CreditsY_Alt
        {
            get
            {
                return coords.CreditsY_Alt_b1 + (coords.CreditsY_Alt_b2 * 64);
            }
            set
            {
                coords.CreditsY_Alt_b1 = (byte)(value % 64);
                coords.CreditsY_Alt_b2 = (byte)(value / 64);
            }
        }
        //
        private Bitmap mainTitlePreview = new Bitmap(256, 224);
        // editors
        private Intro intro;
        public MainTitleCoordinates coords;
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicTitleEditor;
        private GraphicEditor graphicL1L2Editor;
        private PaletteEditor spritePaletteEditor;
        private GraphicEditor spriteGraphicEditor;
        private TilesetEditor tilesetEditor;
        #endregion
        #region Functions
        public MainTitle(Intro intro)
        {
            this.Updating = true;
            this.intro = intro;
            this.coords = new MainTitleCoordinates();
            //
            InitializeComponent();
            RefreshCoordinates();
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

            //disable tileset editor's tileset lock
            tilesetEditor.lockEditing.Checked = false;

            SetTilesetImages();

            titleChoices.SelectedIndex = 0;
            // title toggles
            // title acts as normal (no change)
            if (Model.ROM[0x09E640] == 0xAF &&
                Model.ROM[0x09E641] == 0xFE &&
                Model.ROM[0x09E642] == 0x0F &&
                Model.ROM[0x09E643] == 0x40)
                titleChoices.SelectedIndex = 0;
            // first bootup title always enabled
            else if (Model.ROM[0x09E640] == 0xA9 &&
                Model.ROM[0x09E641] == 0x00 &&
                Model.ROM[0x09E642] == 0xEA &&
                Model.ROM[0x09E643] == 0xEA)
                titleChoices.SelectedIndex = 1;
            // alternate title always enabled
            else if (Model.ROM[0x09E640] == 0xA9 &&
                Model.ROM[0x09E641] == 0x01 &&
                Model.ROM[0x09E642] == 0xEA &&
                Model.ROM[0x09E643] == 0xEA)
                titleChoices.SelectedIndex = 2;

            //
            GC.Collect();
            this.History = new History(this);

            this.Updating = false;
        }

        public void RefreshCoordinates()
        {
            spriteCoordinateX.Value = coords.SpriteX;
            spriteCoordinateY.Value = coords.SpriteY;
            labelSprite.Visible =
                spriteCoordinateY.Visible =
                spriteCoordinateX.Visible =
                alternateTitle.Checked;

            if (!alternateTitle.Checked)
            {
                backgroundCoordinatesY.Value = coords.BG_Y;
                //
                titleCoordinatesX.Value = TitleX;
                creditsCoordinatesX.Value = CreditsX;
                //
                titleCoordinatesY.Value = TitleY;
                creditsCoordinatesY.Value = CreditsY;
                //
                if (syncBannerXY.Checked && !this.Updating)
                {
                    bannerCoordinatesX.Value = TitleX;
                    bannerCoordinatesY.Value = (ushort)(TitleY + 48 > bannerCoordinatesY.Maximum ? bannerCoordinatesY.Maximum : TitleY + 48);
                }
                else
                {
                    bannerCoordinatesX.Value = BannerX;
                    bannerCoordinatesY.Value = BannerY;
                }
            }
            else
            {
                backgroundCoordinatesY.Value = coords.BG_Y_Alt;
                //
                titleCoordinatesX.Value = TitleX_Alt;
                creditsCoordinatesX.Value = CreditsX_Alt;
                //
                titleCoordinatesY.Value = TitleY_Alt;
                creditsCoordinatesY.Value = CreditsY_Alt;
                //
                if (syncBannerXY.Checked && !this.Updating)
                {
                    bannerCoordinatesX.Value = TitleX_Alt;
                    bannerCoordinatesY.Value = (ushort)(TitleY_Alt + 48 > bannerCoordinatesY.Maximum ? bannerCoordinatesY.Maximum : TitleY_Alt + 48);
                }
                else
                {
                    bannerCoordinatesX.Value = BannerX_Alt;
                    bannerCoordinatesY.Value = BannerY_Alt;
                }
            }

            pictureBoxTitle.Invalidate();
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
            //Coordinates
            coords.Assemble();

            switch (titleChoices.SelectedIndex)
            {
                case 1:
                    byte[] AlwaysUseFirstBootTitle = new byte[4] { 0xA9, 0x00, 0xEA, 0xEA };
                    AlwaysUseFirstBootTitle.CopyTo(Model.ROM, 0x09E640); 
                    break;
                case 2:
                    byte[] AlwaysUseAlternateTitle = new byte[4] { 0xA9, 0x01, 0xEA, 0xEA };
                    AlwaysUseAlternateTitle.CopyTo(Model.ROM, 0x09E640);
                    break;
                default:
                    byte[] UsualTitleScreenCheck = new byte[4] { 0xAF, 0xFE, 0x0F, 0x40 };
                    UsualTitleScreenCheck.CopyTo(Model.ROM, 0x09E640);
                    break;
            }
        }
        // drawing
        public void SetTilesetImages()
        {
            int[] pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[0], 16, 32, 0, false);
            tilesetImage[0] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[1], 16, 32, 0, false);
            tilesetImage[1] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixels(tileset.Tilesets_tiles[2], 16, 6, 0, false);
            tilesetImage[2] = Do.PixelsToImage(pixels, 256, 96);
            //
            pixels = Do.TilesetToPixelsPriority1(tileset.Tilesets_tiles[0], 16, 32, 0);
            tilesetImageP1[0] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixelsPriority1(tileset.Tilesets_tiles[1], 16, 32, 0);
            tilesetImageP1[1] = Do.PixelsToImage(pixels, 256, 512);
            pixels = Do.TilesetToPixelsPriority1(tileset.Tilesets_tiles[2], 16, 6, 0);
            tilesetImageP1[2] = Do.PixelsToImage(pixels, 256, 96);
            //
            pixels = Do.GetPixelRegion(Model.TitleSpriteGraphics, 0x20, Model.TitleSpritePalettes.Palettes[0], 16, 0, 0, 4, 6, 0);
            spriteImage[0] = Do.PixelsToImage(pixels, 32, 48);
            pixels = Do.GetPixelRegion(Model.TitleSpriteGraphics, 0x20, Model.TitleSpritePalettes.Palettes[0], 16, 0, 34, 2, 4, 0);
            spriteImage[1] = Do.PixelsToImage(pixels, 16, 32);
            pixels = Do.GetPixelRegion(Model.TitleSpriteGraphics, 0x20, Model.TitleSpritePalettes.Palettes[0], 16, 8, 34, 2, 1, 0);
            spriteImage[2] = Do.PixelsToImage(pixels, 16, 8);
            pictureBoxTitle.Invalidate();
        }

        // loading
        #region EditorFunctions
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
        private void LoadTitleGraphicEditor()
        {
            if (graphicTitleEditor == null)
            {
                graphicTitleEditor = new GraphicEditor(new Function(GraphicTitleUpdate),
                    tileset.GraphicsL3,
                    tileset.GraphicsL3.Length,
                    0, paletteSet, 0, 0x20);
                graphicTitleEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicTitleEditor.Reload(new Function(GraphicTitleUpdate),
                    tileset.GraphicsL3,
                    tileset.GraphicsL3.Length,
                    0, paletteSet, 0, 0x20);
        }
        private void LoadL1L2GraphicEditor()
        {
            if (graphicL1L2Editor == null)
            {
                graphicL1L2Editor = new GraphicEditor(new Function(GraphicL1L2Update),
                    tileset.Graphics,
                    tileset.Graphics.Length,
                    0, paletteSet, 0, 0x20);
                graphicL1L2Editor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicL1L2Editor.Reload(new Function(GraphicL1L2Update),
                    tileset.Graphics,
                    tileset.Graphics.Length,
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
            LoadL1L2GraphicEditor();
            LoadTitleGraphicEditor();
            LoadTilesetEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicTitleUpdate()
        {
            tileset.Assemble(16);
            tileset = new Tileset(paletteSet, "title");
            tileset.GraphicsL3 = graphicTitleEditor.Graphics;
            SetTilesetImages();
            LoadTilesetEditor();
        }
        private void GraphicL1L2Update()
        {
            tileset.Assemble(16);
            tileset = new Tileset(paletteSet, "title");
            tileset.Graphics = graphicL1L2Editor.Graphics;
            SetTilesetImages();
            LoadTilesetEditor();
        }
        private void SpritePaletteUpdate()
        {
            LoadSpriteGraphicEditor();
        }
        private void SpriteGraphicUpdate()
        {
            SetTilesetImages();
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
            if (graphicL1L2Editor != null)
            {
                graphicL1L2Editor.Close();
                graphicL1L2Editor.Dispose();
            }
            if (graphicTitleEditor != null)
            {
                graphicTitleEditor.Close();
                graphicTitleEditor.Dispose();
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
        #endregion
        #region Event Handlers

        private void pictureBoxTitle_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage[0] != null && tilesetImage[1] != null && tilesetImage[2] != null
                && spriteImage[0] != null && spriteImage[1] != null && spriteImage[2] != null
                && tilesetImageP1[0] != null && tilesetImageP1[1] != null && tilesetImageP1[2] != null)
            {
                Color bgcolor = Color.FromArgb(paletteSet.Palette[0]);
                e.Graphics.FillRectangle(new SolidBrush(bgcolor), new Rectangle(new Point(0, 0), pictureBoxTitle.Size));

                //Draw Layer 2 and Layer 1 Background
                int Y_BG = alternateTitle.Checked ? -coords.BG_Y_Alt - 1 : -coords.BG_Y - 1;
                e.Graphics.DrawImage(tilesetImage[1], 0, Y_BG);
                e.Graphics.DrawImage(tilesetImage[0], 0, Y_BG);

                Rectangle upperPart, lowerPart, banner;

                //sprite layer
                if (alternateTitle.Checked)
                {
                    upperPart = new Rectangle(0, 0, 32, 48);
                    lowerPart = new Rectangle(0, 0, 16, 32);
                    Rectangle tip = new Rectangle(0, 0, 16, 8);
                    e.Graphics.DrawImage(spriteImage[0].Clone(upperPart, PixelFormat.DontCare), (int)spriteCoordinateX.Value, (int)spriteCoordinateY.Value);
                    e.Graphics.DrawImage(spriteImage[1].Clone(lowerPart, PixelFormat.DontCare), (int)spriteCoordinateX.Value + 8, (int)spriteCoordinateY.Value + (8 * 6));
                    e.Graphics.DrawImage(spriteImage[2].Clone(tip, PixelFormat.DontCare), (int)spriteCoordinateX.Value + 8, (int)spriteCoordinateY.Value + (8 * 6) + (8 * 4));
                }


                //Layer 3 title
                upperPart = new Rectangle(0, 0, 256, 56);
                lowerPart = new Rectangle(0, 72, 256, 24);
                banner = new Rectangle(0, 48, 256, 24);
                int X_Title = alternateTitle.Checked ? TitleX_Alt * 8 : TitleX * 8;
                int Y_Title = alternateTitle.Checked ? TitleY_Alt - coords.BG_Y_Alt - 1 : TitleY - coords.BG_Y - 1;
                int X_Banner = alternateTitle.Checked ? BannerX_Alt * 8 : BannerX * 8;
                int Y_Banner = alternateTitle.Checked ? BannerY_Alt - coords.BG_Y_Alt - 1 : BannerY - coords.BG_Y - 1;
                int X_Credits = alternateTitle.Checked ? CreditsX_Alt * 8 : CreditsX * 8;
                int Y_Credits = alternateTitle.Checked ? CreditsY_Alt - coords.BG_Y_Alt - 1 : CreditsY - coords.BG_Y - 1;

                e.Graphics.DrawImage(tilesetImage[2].Clone(upperPart, PixelFormat.DontCare),
                    X_Title,
                    Y_Title);
                e.Graphics.DrawImage(tilesetImage[2].Clone(banner, PixelFormat.DontCare),
                    X_Banner,
                    Y_Banner);
                e.Graphics.DrawImage(tilesetImage[2].Clone(lowerPart, PixelFormat.DontCare),
                    X_Credits,
                    Y_Credits);

                // Fake the title wrap around
                if (TitleX > 0 || TitleX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImage[2].Clone(upperPart, PixelFormat.DontCare),
                        X_Title - 256,
                        Y_Title + 8);
                if (BannerX > 0 || BannerX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImage[2].Clone(banner, PixelFormat.DontCare),
                        X_Banner - 256,
                        Y_Banner + 8);
                if (CreditsX > 0 || CreditsX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImage[2].Clone(lowerPart, PixelFormat.DontCare),
                        X_Credits - 256,
                        Y_Credits + 8);


                // Draw Priority1 Tiles
                // Draw layer 2 Priority1
                e.Graphics.DrawImage(tilesetImageP1[1], 0, Y_BG);

                // Next layer 3 Priority1
                e.Graphics.DrawImage(tilesetImageP1[2].Clone(upperPart, PixelFormat.DontCare),
                    X_Title,
                    Y_Title);
                e.Graphics.DrawImage(tilesetImageP1[2].Clone(banner, PixelFormat.DontCare),
                    X_Banner,
                    Y_Banner);
                e.Graphics.DrawImage(tilesetImageP1[2].Clone(lowerPart, PixelFormat.DontCare),
                    X_Credits,
                    Y_Credits);

                // Fake the title wrap around again for priority1
                if (TitleX > 0 || TitleX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImageP1[2].Clone(upperPart, PixelFormat.DontCare),
                        X_Title - 256,
                        Y_Title + 8);
                if (BannerX > 0 || BannerX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImageP1[2].Clone(banner, PixelFormat.DontCare),
                        X_Banner - 256,
                        Y_Banner + 8);
                if (CreditsX > 0 || CreditsX_Alt > 0)
                    e.Graphics.DrawImage(tilesetImageP1[2].Clone(lowerPart, PixelFormat.DontCare),
                        X_Credits - 256,
                        Y_Credits + 8);

                // Draw Layer 1 Priority1
                e.Graphics.DrawImage(tilesetImageP1[0], 0, Y_BG);
            }
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxTitle.DrawToBitmap(mainTitlePreview, pictureBoxTitle.ClientRectangle);
            Do.Export(mainTitlePreview, "mainTitlePreview.png");
        }

        // editors

        #region EditorEvents
        private void openPalettes_Click(object sender, EventArgs e)
        {
            if (paletteEditor == null)
                LoadPaletteEditor();
            paletteEditor.Show();
        }
        private void openTitleGraphics_Click(object sender, EventArgs e)
        {
            if (graphicTitleEditor == null)
                LoadTitleGraphicEditor();
            graphicTitleEditor.Show();
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
        private void openLandscapePalettes_Click(object sender, EventArgs e)
        {
            if (spritePaletteEditor == null)
                LoadSpritePaletteEditor();
            spritePaletteEditor.Show();
        }
        private void openLandscapeGraphics_Click(object sender, EventArgs e)
        {
            if (graphicL1L2Editor == null)
                LoadL1L2GraphicEditor();
            graphicL1L2Editor.Show();
        }
        #endregion

        #region CoordinatesEvents
        private void alternateTitle_Click(object sender, EventArgs e)
        {
            this.Updating = true;
            RefreshCoordinates();
            this.Updating = false;
            this.Modified = true;
        }

        private void spriteCoordinateX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            coords.SpriteX = (byte)spriteCoordinateX.Value;
            pictureBoxTitle.Invalidate();
            this.Modified = true;
        }
        private void spriteCoordinateY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            coords.SpriteY = (byte)spriteCoordinateY.Value;
            pictureBoxTitle.Invalidate();
            this.Modified = true;
        }

        private void backgroundCoordinatesY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (!alternateTitle.Checked)
                coords.BG_Y = (byte)backgroundCoordinatesY.Value;
            else
                coords.BG_Y_Alt = (byte)backgroundCoordinatesY.Value;
            RefreshCoordinates();
            this.Modified = true;
        }

        private void titleCoordinatesX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (titleCoordinatesX.Value == titleCoordinatesX.Minimum)
            {
                titleCoordinatesX.Value = titleCoordinatesX.Maximum - 1;
                if (titleCoordinatesY.Value != titleCoordinatesY.Maximum)
                    titleCoordinatesY.Value += 4;
            }
            else if (titleCoordinatesX.Value == titleCoordinatesX.Maximum)
            {
                titleCoordinatesX.Value = titleCoordinatesX.Minimum + 1;
                if (titleCoordinatesY.Value != titleCoordinatesY.Minimum)
                    titleCoordinatesY.Value -= 4;
            }

            if (!alternateTitle.Checked)
                TitleX = (ushort)titleCoordinatesX.Value;
            else
                TitleX_Alt = (ushort)titleCoordinatesX.Value;
            
            RefreshCoordinates();
            this.Modified = true;
        }
        private void titleCoordinatesY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;

            if (!alternateTitle.Checked)
                TitleY = (ushort)titleCoordinatesY.Value;
            else
                TitleY_Alt = (ushort)titleCoordinatesY.Value;

            RefreshCoordinates();
            this.Modified = true;
        }

        private void bannerCoordinatesX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;

            if (bannerCoordinatesX.Value == bannerCoordinatesX.Minimum)
            {
                bannerCoordinatesX.Value = bannerCoordinatesX.Maximum - 1;
                if (bannerCoordinatesY.Value != bannerCoordinatesY.Maximum)
                    bannerCoordinatesY.Value += 4;
            }
            else if (bannerCoordinatesX.Value == bannerCoordinatesX.Maximum)
            {
                bannerCoordinatesX.Value = bannerCoordinatesX.Minimum + 1;
                if (bannerCoordinatesY.Value != bannerCoordinatesY.Minimum)
                    bannerCoordinatesY.Value -= 4;
            }

            if (!alternateTitle.Checked)
                BannerX = (ushort)bannerCoordinatesX.Value;
            else
                BannerX_Alt = (ushort)bannerCoordinatesX.Value;
            RefreshCoordinates();
            this.Modified = true;
        }

        private void bannerCoordinatesY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;

            if (!alternateTitle.Checked)
                BannerY = (ushort)bannerCoordinatesY.Value;
            else
                BannerY_Alt = (ushort)bannerCoordinatesY.Value;
            RefreshCoordinates();
            this.Modified = true;
        }

        private void creditsCoordinatesX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (creditsCoordinatesX.Value == creditsCoordinatesX.Minimum)
            {
                creditsCoordinatesX.Value = creditsCoordinatesX.Maximum - 1;
                if (creditsCoordinatesY.Value != creditsCoordinatesY.Maximum)
                    creditsCoordinatesY.Value += 4;
            }
            else if (creditsCoordinatesX.Value == creditsCoordinatesX.Maximum)
            {
                creditsCoordinatesX.Value = creditsCoordinatesX.Minimum + 1;
                if (creditsCoordinatesY.Value != creditsCoordinatesY.Minimum)
                    creditsCoordinatesY.Value -= 4;
            }

            if (!alternateTitle.Checked)
                CreditsX = (ushort)creditsCoordinatesX.Value;
            else
                CreditsX_Alt = (ushort)creditsCoordinatesX.Value;
            RefreshCoordinates();
            this.Modified = true;
        }
        private void creditsCoordinatesY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (!alternateTitle.Checked)
                CreditsY = (ushort)creditsCoordinatesY.Value;
            else
                CreditsY_Alt = (ushort)creditsCoordinatesY.Value;
            RefreshCoordinates();
            this.Modified = true;
        }
        #endregion

        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }

        private void syncBannerXY_CheckedChanged(object sender, EventArgs e)
        {
            bannerCoordinatesX.Enabled = !syncBannerXY.Checked;
            bannerCoordinatesY.Enabled = !syncBannerXY.Checked;
        }

        private void titleChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Modified = true;
        }
        #endregion


        [Serializable()]
        public class MainTitleCoordinates
        {
            // universal variables
            private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
            // coordinates
            private byte spriteX;
            private byte spriteY;
            public byte SpriteX { get { return this.spriteX; } set { this.spriteX = value; } }
            public byte SpriteY { get { return this.spriteY; } set { this.spriteY = value; } }
            //
            private byte bgY;
            private byte bgY_alt;
            public byte BG_Y { get { return this.bgY; } set { this.bgY = value; } }
            public byte BG_Y_Alt { get { return this.bgY_alt; } set { this.bgY_alt = value; } }
            //
            private byte titleX_b1;
            private byte titleX_alt_b1;
            private byte titleY_b1;
            private byte titleY_b2;
            private byte titleY_alt_b1;
            private byte titleY_alt_b2;
            public byte TitleX_b1 { get { return this.titleX_b1; } set { this.titleX_b1 = value; } }
            public byte TitleX_Alt_b1 { get { return this.titleX_alt_b1; } set { this.titleX_alt_b1 = value; } }
            public byte TitleY_b1 { get { return this.titleY_b1; } set { this.titleY_b1 = value; } }
            public byte TitleY_b2 { get { return this.titleY_b2; } set { this.titleY_b2 = value; } }
            public byte TitleY_Alt_b1 { get { return this.titleY_alt_b1; } set { this.titleY_alt_b1 = value; } }
            public byte TitleY_Alt_b2 { get { return this.titleY_alt_b2; } set { this.titleY_alt_b2 = value; } }
            //
            private byte bannerX_b1;
            private byte bannerX_alt_b1;
            private byte bannerY_b1;
            private byte bannerY_b2;
            private byte bannerY_alt_b1;
            private byte bannerY_alt_b2;
            public byte BannerX_b1 { get { return this.bannerX_b1; } set { this.bannerX_b1 = value; } }
            public byte BannerX_Alt_b1 { get { return this.bannerX_alt_b1; } set { this.bannerX_alt_b1 = value; } }
            public byte BannerY_b1 { get { return this.bannerY_b1; } set { this.bannerY_b1 = value; } }
            public byte BannerY_b2 { get { return this.bannerY_b2; } set { this.bannerY_b2 = value; } }
            public byte BannerY_Alt_b1 { get { return this.bannerY_alt_b1; } set { this.bannerY_alt_b1 = value; } }
            public byte BannerY_Alt_b2 { get { return this.bannerY_alt_b2; } set { this.bannerY_alt_b2 = value; } }
            //
            private byte creditsX_b1;
            private byte creditsX_alt_b1;
            private byte creditsY_b1;
            private byte creditsY_b2;
            private byte creditsY_alt_b1;
            private byte creditsY_alt_b2;
            public byte CreditsX_b1 { get { return this.creditsX_b1; } set { this.creditsX_b1 = value; } }
            public byte CreditsX_Alt_b1 { get { return this.creditsX_alt_b1; } set { this.creditsX_alt_b1 = value; } }
            public byte CreditsY_b1 { get { return this.creditsY_b1; } set { this.creditsY_b1 = value; } }
            public byte CreditsY_b2 { get { return this.creditsY_b2; } set { this.creditsY_b2 = value; } }
            public byte CreditsY_Alt_b1 { get { return this.creditsY_alt_b1; } set { this.creditsY_alt_b1 = value; } }
            public byte CreditsY_Alt_b2 { get { return this.creditsY_alt_b2; } set { this.creditsY_alt_b2 = value; } }
            //
            // constructor
            public MainTitleCoordinates()
            {
                Disassemble();
            }
            public void Disassemble()
            {
                spriteX = rom[0x09E67B];
                spriteY = rom[0x09E681];
                //
                bgY = rom[0x09E66B];
                bgY_alt = rom[0x09E68B];
                //
                titleX_b1 = (byte)(rom[0x09E9E8] & 0x1F);
                titleY_b1 = (byte)((rom[0x09E9E8] & 0xE0) >> 2);
                titleX_alt_b1 = (byte)(rom[0x09E9F1] & 0x1F);
                titleY_alt_b1 = (byte)((rom[0x09E9F1] & 0xE0) >> 2);

                titleY_b2 = (byte)(rom[0x09E9E9] & 0x07);
                titleY_alt_b2 = (byte)(rom[0x09E9F2] & 0x07);
                //
                bannerX_b1 = (byte)(rom[0x09E818] & 0x1F);
                bannerY_b1 = (byte)((rom[0x09E818] & 0xE0) >> 2);
                bannerX_alt_b1 = (byte)(rom[0x09E821] & 0x1F);
                bannerY_alt_b1 = (byte)((rom[0x09E821] & 0xE0) >> 2);

                bannerY_b2 = (byte)(rom[0x09E819] & 0x07);
                bannerY_alt_b2 = (byte)(rom[0x09E822] & 0x07);
                //
                creditsX_b1 = (byte)(rom[0x09E9E4] & 0x1F);
                creditsY_b1 = (byte)((rom[0x09E9E4] & 0xE0) >> 2);
                creditsX_alt_b1 = (byte)(rom[0x09E9ED] & 0x1F);
                creditsY_alt_b1 = (byte)((rom[0x09E9ED] & 0xE0) >> 2);

                creditsY_b2 = (byte)(rom[0x09E9E5] & 0x07);
                creditsY_alt_b2 = (byte)(rom[0x09E9EE] & 0x07);
            }
            public void Assemble()
            {
                rom[0x09E67B] = spriteX;
                rom[0x09E681] = spriteY;
                //
                rom[0x09E66B] = bgY;
                rom[0x09E68B] = bgY_alt;
                //
                rom[0x09E9E8] = (byte)((titleX_b1 & 0x1F) + ((titleY_b1 << 2) & 0xE0));
                rom[0x09E9E9] = (byte)((titleY_b2 & 0x07) | 0x48);
                rom[0x09E9F1] = (byte)((titleX_alt_b1 & 0x1F) + ((titleY_alt_b1 << 2) & 0xE0));
                rom[0x09E9F2] = (byte)((titleY_alt_b2 & 0x07) | 0x48);
                // 
                rom[0x09E9E4] = (byte)((creditsX_b1 & 0x1F) + ((creditsY_b1 << 2) & 0xE0));
                rom[0x09E9E5] = (byte)((creditsY_b2 & 0x07) | 0x50); //0x50 instead of 0x48 seems to fix the background of the credits not being transparent
                rom[0x09E9ED] = (byte)((creditsX_alt_b1 & 0x1F) + ((creditsY_alt_b1 << 2) & 0xE0));
                rom[0x09E9EE] = (byte)((creditsY_alt_b2 & 0x07) | 0x50); //same here 0x48 -> 0x50
                //
                // These coordinates are for the title AFTER the intro demo ends (without the player skipping straight to the title)
                rom[0x09EF51] = (byte)((titleX_b1 & 0x1F) + ((titleY_b1 << 2) & 0xE0));
                rom[0x09EF52] = (byte)((titleY_b2 & 0x07) | 0x48);
                rom[0x09EF4C] = (byte)((titleX_alt_b1 & 0x1F) + ((titleY_alt_b1 << 2) & 0xE0));
                rom[0x09EF4D] = (byte)((titleY_alt_b2 & 0x07) | 0x48);
                //
                rom[0x09E818] = (byte)((bannerX_b1 & 0x1F) + ((bannerY_b1 << 2) & 0xE0));
                rom[0x09E819] = (byte)((bannerY_b2 & 0x07) | 0x48);
                rom[0x09E821] = (byte)((bannerX_alt_b1 & 0x1F) + ((bannerY_alt_b1 << 2) & 0xE0));
                rom[0x09E822] = (byte)((bannerY_alt_b2 & 0x07) | 0x48);
                //
                rom[0x09E814] = (byte)((creditsX_b1 & 0x1F) + ((creditsY_b1 << 2) & 0xE0));
                rom[0x09E815] = (byte)((creditsY_b2 & 0x07) | 0x50);
                rom[0x09E81D] = (byte)((creditsX_alt_b1 & 0x1F) + ((creditsY_alt_b1 << 2) & 0xE0));
                rom[0x09E81E] = (byte)((creditsY_alt_b2 & 0x07) | 0x50);

                // These are the coordinates for the title SUPER MARIO RPG for when the event "Exor crashes into keep"
                // Commenting these out for now
                //rom[0x09EF56] = (byte)((titleX_b1 & 0x1F) + ((titleY_b1 << 2) & 0xE0));
                //rom[0x09EF57] = (byte)((titleY_b2 & 0x07) | 0x48);
            }
            public void Clear()
            {
                spriteX = 0;
                spriteY = 0;
            }
        }

    }
}
