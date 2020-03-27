using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class ExportImages : NewForm
    {
        #region Variables
        private int currentIndex;
        private string element;
        private Sprite[] sprites { get { return Model.Sprites; } }
        private Animation[] animations { get { return Model.Animations; } }
        private PaletteSet[] palettes { get { return Model.SpritePalettes; } }
        private ImagePacket[] images { get { return Model.GraphicPalettes; } }
        private byte[] spriteGraphics { get { return Model.SpriteGraphics; } }
        private Level[] levels { get { return Model.Levels; } }
        private LevelMap[] levelMaps { get { return Model.LevelMaps; } }
        private PaletteSet[] paletteSets { get { return Model.PaletteSets; } set { Model.PaletteSets = value; } }
        private PrioritySet[] prioritySets { get { return Model.PrioritySets; } set { Model.PrioritySets = value; } }
        private ProgressBar progressBar;
        #endregion
        // Constructor
        public ExportImages(int currentIndex, string element)
        {
            this.currentIndex = currentIndex;
            this.element = element;
            InitializeComponent();
            if (this.element == "levels")
            {
                this.Text = "EXPORT LEVEL IMAGES - Lazy Shell";
                this.current.Text = "Export current level image";
                this.range.Text = "Export level images within level index range";
                this.oneImageDefault.Text = "One image per level, default size (all levels will be 1024x1024!)";
                this.oneImageCropped.Text = "One image per level, cropped to mask edges";
                this.fromIndex.Maximum = 509;
                this.toIndex.Maximum = 509;
                this.maximumWidth.Visible = false;
                this.label2.Visible = false;
                this.oneSpriteSheet.Visible = false;
                this.oneAnimatedGIF.Visible = false;
            }
        }
        // functions
        private void Export()
        {
            bool gif = oneAnimatedGIF.Checked;
            bool crop = oneImageCropped.Checked || oneSpriteSheet.Checked;
            bool contact = oneSpriteSheet.Checked;
            int maxwidth = (int)maximumWidth.Value;
            int start;
            int end;
            string fullPath;
            if (current.Checked)
            {
                start = currentIndex;
                end = currentIndex + 1;
                if (element == "levels" || (element == "sprites" && oneSpriteSheet.Checked))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Image file (*.png)|*.png";
                    if (element == "levels")
                        saveFileDialog.FileName = "Level #" + start.ToString("d3");
                    else
                        saveFileDialog.FileName = "Sprite #" + start.ToString("d4");
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = "Save Image";
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                        return;
                    else
                        Settings.Default.LastDirectory = saveFileDialog.FileName;
                    fullPath = saveFileDialog.FileName;
                }
                else
                {
                    FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                    folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
                    folderBrowserDialog1.Description = "Select directory to export to";
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
                    else
                        return;
                    fullPath = folderBrowserDialog1.SelectedPath + "\\";
                }
            }
            else
            {
                start = (int)fromIndex.Value;
                end = (int)(toIndex.Value + 1);
                // first, open and create directory
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
                folderBrowserDialog1.Description = "Select directory to export to";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
                else
                    return;
                if (element == "levels")
                    fullPath = folderBrowserDialog1.SelectedPath + "\\" + Model.GetFileNameWithoutPath() + " - Level Images\\";
                else
                    fullPath = folderBrowserDialog1.SelectedPath + "\\" + Model.GetFileNameWithoutPath() + " - Sprite Mold Images\\";
                DirectoryInfo di = new DirectoryInfo(fullPath);
                if (!di.Exists)
                    di.Create();
            }
            // set the backgroundworker properties
            Export_Worker.DoWork += (s, e) => Export_Worker_DoWork(s, e, fullPath, crop, contact, gif, maxwidth, start, end, current.Checked);
            Export_Worker.ProgressChanged += new ProgressChangedEventHandler(Export_Worker_ProgressChanged);
            Export_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Export_Worker_RunWorkerCompleted);
            if (element == "levels")
                progressBar = new ProgressBar("EXPORTING LEVEL IMAGES...", levels.Length, Export_Worker);
            else
                progressBar = new ProgressBar("EXPORTING SPRITE MOLD IMAGES...", sprites.Length, Export_Worker);
            progressBar.Show();
            Export_Worker.RunWorkerAsync();
            this.Enabled = false;
            while (Export_Worker.IsBusy)
                Application.DoEvents();
            this.Enabled = true;
        }
        private void Export_Worker_DoWork(object sender, DoWorkEventArgs e, string fullPath,
            bool crop, bool contact, bool gif, int maxwidth, int start, int end, bool current)
        {
            for (int a = start; a < end; a++)
            {
                if (Export_Worker.CancellationPending)
                    break;
                Sprite s = null;
                if (element == "levels")
                    Export_Worker.ReportProgress(a);
                else
                {
                    s = sprites[a];
                    Export_Worker.ReportProgress(s.Index);
                }
                // if NOT sprite sheet or animated gif (ie. if NOT single image for each element)
                if (!contact && element == "sprites")
                {
                    DirectoryInfo di = new DirectoryInfo(fullPath + "Sprite #" + s.Index.ToString("d4"));
                    if (!di.Exists)
                        di.Create();
                }
                int index = 0;
                int x = 0, y = 0;
                if (this.element == "levels")
                {
                    LevelMap lmap = levelMaps[levels[a].LevelMap];
                    LevelLayer layr = levels[a].Layer;
                    PaletteSet pset = paletteSets[levelMaps[levels[a].LevelMap].PaletteSet];
                    Tileset tset = new Tileset(lmap, pset);
                    LevelTilemap tmap = new LevelTilemap(levels[a], tset);
                    int[] pixels;
                    Rectangle region;
                    if (crop)
                    {
                        region = new Rectangle(
                                layr.MaskLowX * 16, layr.MaskLowY * 16,
                                (layr.MaskHighX - layr.MaskLowX) * 16 + 16,
                                (layr.MaskHighY - layr.MaskLowY) * 16 + 16);
                        pixels = Do.GetPixelRegion(tmap.Pixels, region, 1024, 1024);
                    }
                    else
                    {
                        region = new Rectangle(0, 0, 1024, 1024);
                        pixels = tmap.Pixels;
                    }
                    Bitmap image = Do.PixelsToImage(pixels, region.Width, region.Height);
                    if (!current)
                        image.Save(fullPath + "Level #" + a.ToString("d3") + ".png", ImageFormat.Png);
                    else
                        image.Save(fullPath, ImageFormat.Png);
                    continue;
                }
                // sprites
                if (gif)
                {
                    Animation animation = animations[s.AnimationPacket];
                    foreach (Mold m in animation.Molds)
                    {
                        foreach (Mold.Tile t in m.Tiles)
                            t.DrawSubtiles(s.Graphics, s.Palette, m.Gridplane);
                    }
                    foreach (Sequence sequence in animation.Sequences)
                    {
                        List<int> durations = new List<int>();
                        Bitmap[] croppedFrames = sequence.GetSequenceImages(animation, ref durations);
                        //
                        string path = fullPath + "Sprite #" + s.Index.ToString("d4") + "\\sequence." + index.ToString("d2") + ".gif";
                        if (croppedFrames.Length > 0)
                            Do.ImagesToAnimatedGIF(croppedFrames, durations.ToArray(), path);
                        index++;
                    }
                    continue;
                }
                int[][] molds = new int[animations[s.AnimationPacket].Molds.Count][];
                int[] sheet;
                int biggestHeight = 0;
                int biggestWidth = 0;
                List<Rectangle> sheetRegions = new List<Rectangle>();
                foreach (Mold m in animations[s.AnimationPacket].Molds)
                {
                    foreach (Mold.Tile t in m.Tiles)
                        t.DrawSubtiles(
                            images[s.Image].Graphics(spriteGraphics),
                            palettes[images[s.Image].PaletteNum + s.PaletteIndex].Palette,
                            m.Gridplane);
                    Rectangle region;
                    if (crop)
                    {
                        if (m.Gridplane)
                            region = Do.Crop(m.GridplanePixels(), out molds[index], 32, 32);
                        else
                            region = Do.Crop(m.MoldPixels(), out molds[index], 256, 256);
                        m.MoldTilesPerPixel = null;
                        if (x + region.Width < maxwidth && biggestWidth < x + region.Width)
                            biggestWidth = x + region.Width;
                        // if reached far right boundary of a row, add current row's height
                        if (x + region.Width >= maxwidth)
                        {
                            x = region.Width;  // reset width counter
                            y += biggestHeight;
                            sheetRegions.Add(new Rectangle(x - region.Width, y, region.Width, region.Height));
                            biggestHeight = 0; // start next row
                        }
                        else
                        {
                            sheetRegions.Add(new Rectangle(x, y, region.Width, region.Height));
                            x += region.Width;
                        }
                        if (biggestHeight < region.Height)
                            biggestHeight = region.Height;
                    }
                    else
                    {
                        region = new Rectangle(new Point(0, 0), m.Gridplane ? new Size(32, 32) : new Size(256, 256));
                        molds[index] = m.Gridplane ? m.GridplanePixels() : m.MoldPixels();
                    }
                    if (!contact)
                        Do.PixelsToImage(molds[index], region.Width, region.Height).Save(
                            fullPath + "Sprite #" + s.Index.ToString("d4") + "\\mold." + index.ToString("d2") + ".png", ImageFormat.Png);
                    index++;
                }
                if (contact)
                {
                    sheet = new int[biggestWidth * (y + biggestHeight)];
                    for (int i = 0; i < molds.Length; i++)
                        Do.PixelsToPixels(molds[i], sheet, biggestWidth, sheetRegions[i]);
                    string path = fullPath + (current ? "" : "Sprite #" + s.Index.ToString("d4") + ".png");
                    Do.PixelsToImage(sheet, biggestWidth, y + biggestHeight).Save(path, ImageFormat.Png);
                }
            }
        }
        private void Export_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar != null && progressBar.Visible)
                if (element == "levels")
                    progressBar.PerformStep("EXPORTING LEVEL #" + e.ProgressPercentage + " IMAGE");
                else
                    progressBar.PerformStep("EXPORTING SPRITE #" + e.ProgressPercentage + " MOLD IMAGES");
        }
        private void Export_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (progressBar != null && progressBar.Visible)
                progressBar.Close();
        }
        // event handlers
        private void range_CheckedChanged(object sender, EventArgs e)
        {
            fromIndex.Enabled = range.Checked;
            toIndex.Enabled = range.Checked;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            maximumWidth.Enabled = oneSpriteSheet.Checked;
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            Export();
            this.Close();
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
