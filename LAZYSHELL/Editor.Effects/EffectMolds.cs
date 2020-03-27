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
    public partial class EffectMolds : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        // main editor accessed variables
        private Effects effectsEditor;
        private Effect effect { get { return effectsEditor.Effect; } set { effectsEditor.Effect = value; } }
        private EffectSequences sequences { get { return effectsEditor.Sequences; } set { effectsEditor.Sequences = value; } }
        private E_Animation animation { get { return effectsEditor.Animation; } set { effectsEditor.Animation = value; } }
        private int availableBytes { get { return effectsEditor.AvailableBytes; } set { effectsEditor.AvailableBytes = value; } }
        public bool ShowBG { get { return showBG.Checked; } }
        // local variables
        private E_Tileset tileset { get { return animation.Tileset_tiles; } set { animation.Tileset_tiles = value; } }
        private List<E_Mold> molds { get { return animation.Molds; } }
        private E_Mold mold { get { return animation.Molds[e_molds.SelectedIndex]; } }
        private int index { get { return e_molds.SelectedIndex; } set { e_molds.SelectedIndex = value; } }
        private Bitmap tilemapImage;
        private Bitmap tilesetImage;
        private Overlay overlay;
        private int zoom { get { return pictureBoxE_Mold.Zoom; } set { pictureBoxE_Mold.Zoom = value; } }
        private int width { get { return animation.Width * 16; } }
        private int height { get { return animation.Height * 16; } }
        private bool moving = false;
        private Color bgcolor
        {
            get
            {
                return Color.FromArgb(animation.PaletteSet.Palettes[effect.PaletteIndex][0]);
            }
        }
        // mouse
        private bool mouseWithinSameBounds = false;
        private int mouseOverTile = 0;
        private int mouseDownTile = 0;
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition;
        private Point mousePosition;
        private bool mouseEnter = false;
        public PictureBox Picture { get { return pictureBoxE_Mold; } }
        // buffers
        private bool defloating = false;
        private CopyBuffer draggedTiles;
        private CopyBuffer copiedTiles;
        private CopyBuffer selectedTiles;
        private CommandStack commandStack;
        private int commandCount = 0;
        private Bitmap selection;
        // editors
        public TileEditor tileEditor;
        // special controls
        #endregion
        #region Functions
        public EffectMolds(Effects effectsEditor)
        {
            this.effectsEditor = effectsEditor;
            this.overlay = new Overlay();
            this.commandStack = new CommandStack(true);
            InitializeComponent();
            this.pictureBoxE_Mold.ZoomBoxPosition = new Point(64, 0);
            this.Updating = true;
            for (int i = 0; i < animation.Molds.Count; i++)
                this.e_molds.Items.Add("Mold " + i.ToString());
            this.e_molds.SelectedIndex = 0;
            e_moldWidth.Value = animation.Width;
            e_moldHeight.Value = animation.Height;
            e_tileSetSize.Value = animation.TilesetLength;
            this.Updating = false;
            SetTilesetImage();
            SetTilemapImage();
            LoadTileEditor();
        }
        public void Reload(Effects effectsEditor)
        {
            this.effectsEditor = effectsEditor;
            this.commandStack = new CommandStack(true);
            this.overlay.Select.Clear();
            this.overlay.SelectTS.Clear();
            this.selectedTiles = null;
            this.draggedTiles = null;
            this.copiedTiles = null;
            this.selection = null;
            this.Updating = true;
            this.e_molds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.e_molds.Items.Add("Mold " + i.ToString());
            this.e_molds.SelectedIndex = 0;
            e_moldWidth.Value = animation.Width;
            e_moldHeight.Value = animation.Height;
            e_tileSetSize.Value = animation.TilesetLength;
            this.Updating = false;
            SetTilesetImage();
            SetTilemapImage();
            LoadTileEditor();
        }
        private void RefreshMold()
        {
            SetTilemapImage();
        }
        public void SetTilesetImage()
        {
            int[] pixels = Do.TilesetToPixels(tileset.Tileset, 8, 8, 0, false);
            tilesetImage = Do.PixelsToImage(pixels, 128, (int)e_tileSetSize.Value / 64 * 16);
            pictureBoxEffectTileset.Size = tilesetImage.Size;
            pictureBoxEffectTileset.Invalidate();
        }
        public void SetTilemapImage()
        {
            int[] pixels = mold.MoldPixels(animation, tileset);
            tilemapImage = Do.PixelsToImage(pixels, animation.Width * 16, animation.Height * 16);
            pictureBoxE_Mold.Size = new Size(tilemapImage.Width * zoom, tilemapImage.Height * zoom);
            pictureBoxE_Mold.Invalidate();
        }
        // editors
        public void LoadTileEditor()
        {
            if (tileEditor == null)
            {
                tileEditor = new TileEditor(new Function(TileUpdate),
                tileset.Tileset[mouseDownTile], tileset.Graphics,
                animation.PaletteSet, animation.Codec == 1 ? (byte)0x10 : (byte)0x20);
                tileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tileEditor.Reload(new Function(TileUpdate),
                this.tileset.Tileset[mouseDownTile], tileset.Graphics,
                animation.PaletteSet, animation.Codec == 1 ? (byte)0x10 : (byte)0x20);
        }
        private void TileUpdate()
        {
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            sequences.RealignFrames();
        }
        // drawing
        private void DrawHoverBox(Graphics g)
        {
            Rectangle r = new Rectangle(mousePosition.X / 16 * 16 * zoom, mousePosition.Y / 16 * 16 * zoom, 16 * zoom, 16 * zoom);
            g.FillRectangle(new SolidBrush(Color.FromArgb(96, 0, 0, 0)), r);
        }
        private void Draw(Graphics g, int x, int y)
        {
            x /= 16;
            y /= 16;
            // cancel if no selection in the tileset is made
            if (overlay.SelectTS.Empty)
                return;
            // check to see if overwriting same tile(s)
            bool noChange = true;
            for (int y_ = 0; y_ < overlay.SelectTS.Height / 16; y_++)
            {
                for (int x_ = 0; x_ < overlay.SelectTS.Width / 16; x_++)
                {
                    int index = ((overlay.SelectTS.Y / 16) + y_) * 8 + (overlay.SelectTS.X / 16) + x_;
                    int indexInMold = (y + y_) * (width / 16) + x + x_;
                    // cancel if overwriting same tile(s)
                    if (indexInMold < mold.Mold.Length && mold.Mold[indexInMold] != index)
                        noChange = false;
                }
            }
            if (noChange)
                return;
            commandStack.Push(new TilemapCommand(
                mold.Mold, width / 16, height / 16, selectedTiles.BYTE_copy, x, y,
                overlay.SelectTS.Width / 16, overlay.SelectTS.Height / 16));
            commandCount++;
            // draw the tile
            Point p = new Point(x * 16, y * 16);
            int[] pixels = Do.ImageToPixels(overlay.SelectTS.GetSelectionImage(tilesetImage));
            Bitmap image = Do.PixelsToImage(pixels, overlay.SelectTS.Width, overlay.SelectTS.Height);
            p.X *= zoom;
            p.Y *= zoom;
            Rectangle rsrc = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle rdst = new Rectangle(p.X, p.Y, (int)(image.Width * zoom), (int)(image.Height * zoom));
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(image, rdst, rsrc, GraphicsUnit.Pixel);
        }
        private void Erase(int x, int y)
        {
            // cancel if writing same tile over itself
            if (mold.Mold[(y / 16) * (width / 16) + (x / 16)] == 0xFF)
                return;
            commandStack.Push(new TilemapCommand(
                mold.Mold, width / 16, height / 16, new byte[] { 0xFF }, x / 16, y / 16, 1, 1));
            commandCount++;
        }
        private void Undo()
        {
            commandStack.UndoCommand();
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
            }
        }
        private void Redo()
        {
            commandStack.RedoCommand();
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
            }
        }
        private void Cut()
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
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
            if (draggedTiles != null)
            {
                this.copiedTiles = draggedTiles;
                return;
            }
            selection = new Bitmap(tilemapImage.Clone(
                new Rectangle(overlay.Select.Location, overlay.Select.Size), PixelFormat.DontCare));
            int[] copiedTiles = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            this.copiedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
            for (int y = 0, y_ = overlay.Select.Y / 16; y < overlay.Select.Height / 16; y++, y_++)
            {
                for (int x = 0, x_ = overlay.Select.X / 16; x < overlay.Select.Width / 16; x++, x_++)
                    copiedTiles[y * (overlay.Select.Width / 16) + x] = mold.Mold[y_ * (width / 16) + x_];
            }
            this.copiedTiles.Copy = copiedTiles;
        }
        /// <summary>
        /// Start dragging a current selection.
        /// </summary>
        private void Drag()
        {
            if (overlay.Select.Empty || overlay.Select.Size == new Size(0, 0))
                return;
            selection = new Bitmap(tilemapImage.Clone(
                new Rectangle(overlay.Select.Location, overlay.Select.Size), PixelFormat.DontCare));
            int[] copiedTiles = new int[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            this.draggedTiles = new CopyBuffer(overlay.Select.Width, overlay.Select.Height);
            for (int y = 0, y_ = overlay.Select.Y / 16; y < overlay.Select.Height / 16; y++, y_++)
            {
                for (int x = 0, x_ = overlay.Select.X / 16; x < overlay.Select.Width / 16; x++, x_++)
                {
                    int tileX = overlay.Select.X + (x * 16);
                    int tileY = overlay.Select.Y + (y * 16);
                    copiedTiles[y * (overlay.Select.Width / 16) + x] = mold.Mold[y_ * (width / 16) + x_];
                }
            }
            this.draggedTiles.Copy = copiedTiles;
            Delete();
        }
        private void Paste(Point location, CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            moving = true;
            // now dragging a new selection
            draggedTiles = buffer;
            overlay.Select.Refresh(16, location, buffer.Size, pictureBoxE_Mold);
            pictureBoxE_Mold.Invalidate();
            defloating = false;
        }
        /// <summary>
        /// Defloats either a dragged selection or a newly pasted selection.
        /// </summary>
        /// <param name="buffer">The dragged selection or the newly pasted selection.</param>
        private void Defloat(CopyBuffer buffer)
        {
            if (buffer == null)
                return;
            if (overlay.Select.Empty)
                return;
            Point location = new Point();
            location.X = overlay.Select.X / 16;
            location.Y = overlay.Select.Y / 16;
            commandStack.Push(new TilemapCommand(
                mold.Mold, width / 16, height / 16, buffer.BYTE_copy,
                location.X, location.Y, buffer.Width / 16, buffer.Height / 16));
            commandStack.Push(commandCount + 1);
            commandCount = 0;
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
            }
            defloating = true;
            animation.Assemble();
        }
        private void Defloat()
        {
            if (copiedTiles != null && !defloating)
                Defloat(copiedTiles);
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            moving = false;
            overlay.Select.Clear();
            Cursor.Position = Cursor.Position;
        }
        private void Delete()
        {
            if (overlay.Select.Empty)
                return;
            if (tileset.Tileset == null || overlay.Select.Size == new Size(0, 0))
                return;
            Point location = overlay.Select.Location;
            Point terminal = overlay.Select.Terminal;
            byte[] changes = new byte[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            Bits.SetBytes(changes, 0xFF);
            commandStack.Push(new TilemapCommand(
                mold.Mold, width / 16, height / 16, changes,
                overlay.Select.X / 16, overlay.Select.Y / 16, overlay.Select.Width / 16, overlay.Select.Height / 16));
            commandCount++;
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
            }
            animation.Assemble();
        }
        /// <summary>
        /// Flips the mold selection vertically or horizontally.
        /// </summary>
        /// <param name="type">Either "mirror" or "invert".</param>
        private void Flip(string type)
        {
            if (overlay.Select.Empty)
                return;
            if (tileset.Tileset == null || overlay.Select.Size == new Size(0, 0))
                return;
            Point location = overlay.Select.Location;
            Point terminal = overlay.Select.Terminal;
            byte[] flippedTiles = new byte[(overlay.Select.Width / 16) * (overlay.Select.Height / 16)];
            for (int y = 0, y_ = overlay.Select.Y / 16; y < overlay.Select.Height / 16; y++, y_++)
            {
                for (int x = 0, x_ = overlay.Select.X / 16; x < overlay.Select.Width / 16; x++, x_++)
                {
                    flippedTiles[y * (overlay.Select.Width / 16) + x] = mold.Mold[y_ * (width / 16) + x_];
                    if (type == "mirror" && flippedTiles[y * (overlay.Select.Width / 16) + x] != 0xFF)
                        flippedTiles[y * (overlay.Select.Width / 16) + x] ^= 0x40;
                    if (type == "invert" && flippedTiles[y * (overlay.Select.Width / 16) + x] != 0xFF)
                        flippedTiles[y * (overlay.Select.Width / 16) + x] ^= 0x80;
                }
            }
            if (type == "mirror")
                Do.FlipHorizontal(flippedTiles, overlay.Select.Width / 16, overlay.Select.Height / 16);
            if (type == "invert")
                Do.FlipVertical(flippedTiles, overlay.Select.Width / 16, overlay.Select.Height / 16);
            commandStack.Push(new TilemapCommand(
                mold.Mold, width / 16, height / 16, flippedTiles,
                overlay.Select.X / 16, overlay.Select.Y / 16, overlay.Select.Width / 16, overlay.Select.Height / 16));
            commandStack.Push(1);
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
            }
            animation.Assemble();
        }
        #endregion
        #region Event Handlers
        private void pictureBoxEffectTileset_Paint(object sender, PaintEventArgs e)
        {
            if (showBG.Checked)
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(animation.PaletteSet.Palettes[effect.PaletteIndex][0])),
                    new Rectangle(new Point(0, 0), pictureBoxEffectTileset.Size));
            if (tilesetImage != null)
                e.Graphics.DrawImage(tilesetImage, 0, 0, 128, (int)e_tileSetSize.Value / 64 * 16);
            if (e_moldShowGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxEffectTileset.Size, new Size(16, 16), 1, true);
            if (overlay.SelectTS != null)
                overlay.SelectTS.DrawSelectionBox(e.Graphics, 1);
        }
        private void pictureBoxEffectTileset_MouseDown(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxEffectTileset.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxEffectTileset.Height));
            mouseDownTile = (y / 16) * 8 + (x / 16);
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseOverObject == null)
                overlay.SelectTS.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBoxEffectTileset);
            pictureBoxEffectTileset.Invalidate();
            LoadTileEditor();
        }
        private void pictureBoxEffectTileset_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X, pictureBoxEffectTileset.Width));
            int y = Math.Max(0, Math.Min(e.Y, pictureBoxEffectTileset.Height));
            // if making a new selection
            if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.SelectTS != null)
            {
                overlay.SelectTS.Final = new Point(
                        Math.Min(x + 16, pictureBoxEffectTileset.Width),
                        Math.Min(y + 16, pictureBoxEffectTileset.Height));
            }
            pictureBoxEffectTileset.Invalidate();
        }
        private void pictureBoxEffectTileset_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            int x_ = overlay.SelectTS.Location.X / 16;
            int y_ = overlay.SelectTS.Location.Y / 16;
            if (this.selectedTiles == null)
                this.selectedTiles = new CopyBuffer(overlay.SelectTS.Width, overlay.SelectTS.Height);
            int[] selectedTiles = new int[(overlay.SelectTS.Width / 16) * (overlay.SelectTS.Height / 16)];
            for (int y = 0; y < overlay.SelectTS.Height / 16; y++)
            {
                for (int x = 0; x < overlay.SelectTS.Width / 16; x++)
                {
                    int tileX = overlay.SelectTS.X + (x * 16);
                    int tileY = overlay.SelectTS.Y + (y * 16);
                    selectedTiles[y * (overlay.SelectTS.Width / 16) + x] = (y + y_) * 8 + x + x_;
                }
            }
            this.selectedTiles.Copy = selectedTiles;
            pictureBoxEffectTileset.Focus();
        }
        private void pictureBoxE_Mold_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            if (showBG.Checked)
                e.Graphics.FillRectangle(new SolidBrush(bgcolor), new Rectangle(new Point(0, 0), pictureBoxE_Mold.Size));
            if (tilemapImage != null && e.ClipRectangle.Size != new Size(16, 16))
            {
                Rectangle src = new Rectangle(0, 0, tilemapImage.Width, tilemapImage.Height);
                Rectangle dst = new Rectangle(0, 0, tilemapImage.Width * zoom, tilemapImage.Height * zoom);
                e.Graphics.DrawImage(tilemapImage, dst, src, GraphicsUnit.Pixel);
            }
            if (moving && selection != null && overlay.Select != null)
            {
                Rectangle src = new Rectangle(0, 0, overlay.Select.Width, overlay.Select.Height);
                Rectangle dst = new Rectangle(
                    overlay.Select.X * zoom, overlay.Select.Y * zoom,
                    src.Width * zoom, src.Height * zoom);
                e.Graphics.DrawImage(new Bitmap(selection), dst, src, GraphicsUnit.Pixel);
                Do.DrawString(e.Graphics, new Point(dst.X, dst.Y + dst.Height),
                    "click/drag", Color.White, Color.Black, new Font("Tahoma", 6.75F, FontStyle.Bold));
            }
            if (mouseEnter && e.ClipRectangle.Size != new Size(16, 16))
                DrawHoverBox(e.Graphics);
            if (e_moldShowGrid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, pictureBoxE_Mold.Size, new Size(16, 16), zoom, true);
            if (select.Checked && overlay.Select != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                overlay.Select.DrawSelectionBox(e.Graphics, zoom);
            }
        }
        private void pictureBoxE_Mold_MouseDown(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            #region Zooming
            Point p = new Point();
            p.X = Math.Abs(panelMoldImage.AutoScrollPosition.X);
            p.Y = Math.Abs(panelMoldImage.AutoScrollPosition.Y);
            if ((e_moldZoomIn.Checked && e.Button == MouseButtons.Left) || (e_moldZoomOut.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxE_Mold.ZoomIn(e.X, e.Y);
                return;
            }
            else if ((e_moldZoomOut.Checked && e.Button == MouseButtons.Left) || (e_moldZoomIn.Checked && e.Button == MouseButtons.Right))
            {
                pictureBoxE_Mold.ZoomOut(e.X, e.Y);
                return;
            }
            #endregion
            if (e.Button == MouseButtons.Right)
                return;
            #region Drawing, Erasing, Selecting
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
                moving = false;
            }
            if (select.Checked)
            {
                // if we're not inside a current selection to move it, create a new selection
                if (mouseOverObject != "selection")
                    overlay.Select.Refresh(16, x / 16 * 16, y / 16 * 16, 16, 16, pictureBoxE_Mold);
                // otherwise, start dragging current selection
                else if (mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.Select.MousePosition(x, y);
                    if (!moving)    // only do this if the current selection has not been initially moved
                    {
                        moving = true;
                        Drag();
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (draw.Checked)
                {
                    Draw(pictureBoxE_Mold.CreateGraphics(), x, y);
                    panelMoldImage.AutoScrollPosition = p;
                    return;
                }
                if (erase.Checked)
                {
                    Erase(x, y);
                    if (!showBG.Checked)
                        pictureBoxE_Mold.Erase(x / 16 * 16, y / 16 * 16, 16, 16);
                    else
                        pictureBoxE_Mold.Draw(x / 16 * 16, y / 16 * 16, 16, 16, bgcolor);
                    panelMoldImage.AutoScrollPosition = p;
                    return;
                }
            }
            #endregion
            panelMoldImage.AutoScrollPosition = p;
            pictureBoxE_Mold.Invalidate();
        }
        private void pictureBoxE_Mold_MouseMove(object sender, MouseEventArgs e)
        {
            // set a floor and ceiling for the coordinates
            int x = Math.Max(0, Math.Min(e.X / zoom, width));
            int y = Math.Max(0, Math.Min(e.Y / zoom, height));
            labelCoords.Text = "(x: " + x + ", y: " + y + ") Pixel";
            // must first check if within same bounds as last call of MouseMove event
            mouseWithinSameBounds = mouseOverTile == (y / 16 * 64) + (x / 16);
            // now set the properties
            mousePosition = new Point(x, y);
            mouseOverTile = (y / 16 * 64) + (x / 16);
            mouseOverObject = null;
            #region Zooming
            // if either zoom button is checked, don't do anything else
            if (e_moldZoomIn.Checked || e_moldZoomOut.Checked)
            {
                pictureBoxE_Mold.Invalidate();
                return;
            }
            #endregion
            #region Drawing, erasing, selecting
            if (select.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x + 16, y + 16))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Min(x + 16, pictureBoxE_Mold.Width),
                        Math.Min(y + 16, pictureBoxE_Mold.Height));
                }
                // if dragging the current selection
                else if (e.Button == MouseButtons.Left && mouseDownObject == "selection")
                    overlay.Select.Location = new Point(
                        x / 16 * 16 - mouseDownPosition.X,
                        y / 16 * 16 - mouseDownPosition.Y);
                // if mouse not clicked and within the current selection
                else if (e.Button == MouseButtons.None && overlay.Select != null && overlay.Select.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxE_Mold.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxE_Mold.Cursor = Cursors.Cross;
                pictureBoxE_Mold.Invalidate();
                return;
            }
            if (draw.Checked && e.Button == MouseButtons.Left)
            {
                Draw(pictureBoxE_Mold.CreateGraphics(), x, y);
                return;
            }
            else if (erase.Checked && e.Button == MouseButtons.Left)
            {
                Erase(x, y);
                if (!showBG.Checked)
                    pictureBoxE_Mold.Erase(x / 16 * 16, y / 16 * 16, 16, 16);
                else
                    pictureBoxE_Mold.Draw(x / 16 * 16, y / 16 * 16, 16, 16, bgcolor);
                return;
            }
            #endregion
            pictureBoxE_Mold.Invalidate();
            pictureBoxE_Mold.Focus(effectsEditor);
        }
        private void pictureBoxE_Mold_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxE_Mold_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownObject = null;
            if (draw.Checked || erase.Checked)
            {
                SetTilemapImage();
                if (sequences != null)
                {
                    sequences.SetSequenceFrameImages();
                    sequences.RealignFrames();
                }
            }
            if (!moving && commandCount > 0)
            {
                this.commandStack.Push(commandCount);
                commandCount = 0;
            }
            //
            Point p = new Point(Math.Abs(pictureBoxE_Mold.Left), Math.Abs(pictureBoxE_Mold.Top));
            pictureBoxE_Mold.Focus();
            panelMoldImage.AutoScrollPosition = p;
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void pictureBoxE_Mold_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            pictureBoxE_Mold.Focus(effectsEditor);
            pictureBoxE_Mold.Invalidate();
        }
        private void pictureBoxE_Mold_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            pictureBoxE_Mold.Invalidate();
        }
        private void pictureBoxE_Mold_MouseHover(object sender, EventArgs e)
        {
            //pictureBoxE_Mold.Focus();
        }
        private void pictureBoxE_Mold_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Z: toggleZoomBox.PerformClick(); break;
                case Keys.G: e_moldShowGrid.PerformClick(); break;
                case Keys.B: showBG.PerformClick(); break;
                case Keys.D: draw.PerformClick(); break;
                case Keys.E: erase.PerformClick(); break;
                case Keys.S: select.PerformClick(); break;
                case Keys.Control | Keys.V: paste.PerformClick(); break;
                case Keys.Control | Keys.C: copy.PerformClick(); break;
                case Keys.Delete: delete.PerformClick(); break;
                case Keys.Control | Keys.X: cut.PerformClick(); break;
                case Keys.Control | Keys.D: Defloat(); break;
                case Keys.Control | Keys.A: selectAll.PerformClick(); break;
                case Keys.Control | Keys.Z: undoButton.PerformClick(); break;
                case Keys.Control | Keys.Y: redoButton.PerformClick(); break;
            }
        }
        private void e_molds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = e_molds.SelectedIndex;
            if (draggedTiles != null && e_molds.LastSelectedIndex != -1)
            {
                this.Updating = true;
                e_molds.BeginUpdate();
                //
                e_molds.SelectedIndex = e_molds.LastSelectedIndex;
                Defloat();
                e_molds.SelectedIndex = index;
                //
                e_molds.EndUpdate();
                this.Updating = false;
            }
            e_molds.LastSelectedIndex = index;
            RefreshMold();
        }
        private void e_tileSetSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            overlay.SelectTS.Clear();
            if (animation.Codec == 0)
                e_tileSetSize.Value = (int)e_tileSetSize.Value & 0xFFE0;
            else
                e_tileSetSize.Value = (int)e_tileSetSize.Value & 0xFFF0;
            animation.TilesetLength = (int)e_tileSetSize.Value;
            SetTilesetImage();
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void e_moldWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            Defloat();
            int width = animation.Width;
            animation.Width = (byte)e_moldWidth.Value;
            for (int i = 0; i < molds.Count; i++)
            {
                byte[] temp = Bits.Copy(molds[i].Mold);
                for (int y = 0; y < animation.Height; y++)
                {
                    for (int x = 0; x < animation.Width; x++)
                    {
                        if (x >= width)
                            molds[i].Mold[y * animation.Width + x] = 0xFF;
                        else
                            molds[i].Mold[y * animation.Width + x] = temp[y * width + x];
                    }
                }
            }
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
                sequences.RealignFrames();
            }
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void e_moldHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            Defloat();
            animation.Height = (byte)e_moldHeight.Value;
            SetTilemapImage();
            if (sequences != null)
            {
                sequences.SetSequenceFrameImages();
                sequences.RealignFrames();
                sequences.RealignFrames();
            }
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void newMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 32)
            {
                MessageBox.Show("Animations cannot contain more than 32 molds total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = this.index;
            molds.Insert(index + 1, mold.New());
            this.Updating = true;
            e_molds.BeginUpdate();
            e_molds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.e_molds.Items.Add("Mold " + i.ToString());
            e_molds.EndUpdate();
            this.Updating = false;
            this.index = index + 1;
            sequences.SetSequenceFrameImages();
            sequences.RealignFrames();
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void deleteMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 1)
            {
                MessageBox.Show("Animations must contain at least 1 mold.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = this.index;
            molds.RemoveAt(index);
            this.Updating = true;
            e_molds.Items.RemoveAt(index);
            for (int i = 0; i < e_molds.Items.Count; i++)
                e_molds.Items[i] = "Mold " + i;
            this.Updating = false;
            if (index >= molds.Count && molds.Count != 0)
                this.index = index - 1;
            else if (molds.Count != 0)
                this.index = index;
            RefreshMold();
            sequences.SetSequenceFrameImages();
            sequences.RealignFrames();
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void duplicateMold_Click(object sender, EventArgs e)
        {
            if (molds.Count == 32)
            {
                MessageBox.Show("Animations cannot contain more than 32 molds total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = this.index;
            molds.Insert(index + 1, mold.Copy());
            this.Updating = true;
            e_molds.BeginUpdate();
            e_molds.Items.Clear();
            for (int i = 0; i < animation.Molds.Count; i++)
                this.e_molds.Items.Add("Mold " + i.ToString());
            e_molds.EndUpdate();
            this.Updating = false;
            this.index = index + 1;
            sequences.SetSequenceFrameImages();
            sequences.RealignFrames();
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        // drawing
        private void e_moldShowGrid_Click(object sender, EventArgs e)
        {
            pictureBoxE_Mold.Invalidate();
            pictureBoxEffectTileset.Invalidate();
        }
        private void showBG_Click(object sender, EventArgs e)
        {
            pictureBoxE_Mold.Invalidate();
            pictureBoxEffectTileset.Invalidate();
            sequences.InvalidateFrameImages();
        }
        private void draw_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip6, (ToolStripButton)sender);
            e_moldZoomIn.Checked = false;
            e_moldZoomOut.Checked = false;
            if (draw.Checked)
                this.pictureBoxE_Mold.Cursor = NewCursors.Draw;
            else if (!draw.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxE_Mold.Invalidate();
        }
        private void erase_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip6, (ToolStripButton)sender);
            e_moldZoomIn.Checked = false;
            e_moldZoomOut.Checked = false;
            if (erase.Checked)
                this.pictureBoxE_Mold.Cursor = NewCursors.Erase;
            else if (!erase.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxE_Mold.Invalidate();
        }
        private void select_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip6, (ToolStripButton)sender);
            e_moldZoomIn.Checked = false;
            e_moldZoomOut.Checked = false;
            if (select.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Cross;
            else if (!select.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxE_Mold.Invalidate();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            Defloat();
            if (!select.Checked)
                select.PerformClick();
            overlay.Select.Refresh(16, 0, 0, width, height, pictureBoxE_Mold);
            pictureBoxE_Mold.Invalidate();
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
            if (draggedTiles != null)
            {
                Defloat(draggedTiles);
                draggedTiles = null;
            }
            Paste(new Point(0, 0), copiedTiles);
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (!moving)
                Delete();
            else
            {
                moving = false;
                draggedTiles = null;
                pictureBoxE_Mold.Invalidate();
            }
            if (!moving && commandCount > 0)
            {
                commandStack.Push(commandCount);
                commandCount = 0;
            }
        }
        private void undoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void redoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void mirror_Click(object sender, EventArgs e)
        {
            Flip("mirror");
        }
        private void invert_Click(object sender, EventArgs e)
        {
            Flip("invert");
        }
        private void e_moldZoomIn_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip6, (ToolStripButton)sender);
            e_moldZoomOut.Checked = false;
            if (e_moldZoomIn.Checked)
                this.pictureBoxE_Mold.Cursor = NewCursors.ZoomIn;
            else if (!e_moldZoomIn.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxE_Mold.Invalidate();
        }
        private void e_moldZoomOut_Click(object sender, EventArgs e)
        {
            Do.ResetToolStripButtons(toolStrip6, (ToolStripButton)sender);
            e_moldZoomIn.Checked = false;
            if (e_moldZoomOut.Checked)
                this.pictureBoxE_Mold.Cursor = NewCursors.ZoomOut;
            else if (!e_moldZoomOut.Checked)
                this.pictureBoxE_Mold.Cursor = Cursors.Arrow;
            Defloat();
            pictureBoxE_Mold.Invalidate();
        }
        private void toggleZoomBox_Click(object sender, EventArgs e)
        {
            pictureBoxE_Mold.ZoomBoxEnabled = toggleZoomBox.Checked;
        }
        // contextmenustrip
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (e_moldZoomIn.Checked || e_moldZoomOut.Checked)
                e.Cancel = true;
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(tilemapImage,
                "effectAnimation." + animation.Index.ToString("d3") + ".Mold." + index.ToString("d2") + ".png");
        }
        // tileset contextmenustrip
        private void importIntoTilemap_Click(object sender, EventArgs e)
        {
            Bitmap[] imports = new Bitmap[1]; imports = (Bitmap[])Do.Import(imports);
            if (imports == null)
                return;
            if (imports.Length == 0)
                return;
            if (imports.Length > 32)
            {
                MessageBox.Show("The maximum number of imported images must not exceed 32.", "LAZY SHELL");
                return;
            }
            //
            byte[] graphics = new byte[0x10000];
            int[] palette = animation.PaletteSet.Palettes[effect.PaletteIndex];
            Tile[] tiles = new Tile[16 * 16];
            byte[][] tilemaps = new byte[imports.Length][];
            bool newPalette = false;
            if (MessageBox.Show("Would you like to create a new palette from the imported image(s)?", "LAZY SHELL",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                newPalette = true;
            Do.ImagesToTilemaps(ref imports, ref palette, index, animation.Codec == 1 ? (byte)0x10 : (byte)0x20,
                ref graphics, ref tiles, ref tilemaps, newPalette);
            for (int i = 0; i < palette.Length; i++)
            {
                animation.PaletteSet.Reds[i + (effect.PaletteIndex * 16)] = Color.FromArgb(palette[i]).R;
                animation.PaletteSet.Greens[i + (effect.PaletteIndex * 16)] = Color.FromArgb(palette[i]).G;
                animation.PaletteSet.Blues[i + (effect.PaletteIndex * 16)] = Color.FromArgb(palette[i]).B;
            }
            Buffer.BlockCopy(graphics, 0, animation.GraphicSet, 0, Math.Min(graphics.Length, animation.GraphicSet.Length));
            effectsEditor.E_graphicSetSize.Value = Math.Min(graphics.Length, 8192);
            if (graphics.Length > 8192)
                MessageBox.Show("Not enough space to store the necessary amount of SNES graphics data for the imported images. The total required space (" +
                    graphics.Length + " bytes) for the new SNES graphics data exceeds 8192 bytes.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // set tileset length
            int temp = tiles.Length * 8;
            animation.TilesetLength = Math.Min(tiles.Length * 8, 512);
            animation.TilesetLength = animation.TilesetLength / 64 * 64;
            if (animation.TilesetLength == 0)
                animation.TilesetLength += 64;
            else if (animation.TilesetLength <= 512 - 64 && temp % 64 != 0)
                animation.TilesetLength += 64;
            e_tileSetSize.Value = animation.TilesetLength;
            if (tiles.Length * 8 > 512)
                MessageBox.Show("Not enough space to draw the necessary amount of tiles in the tileset for the imported images. The total required space (" +
                    (tiles.Length * 8).ToString() + " bytes) for the new tileset exceeds 512 bytes.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // redraw data
            animation.Tileset_tiles.DrawTileset(animation.Tileset_bytes, tiles);
            animation.Tileset_tiles = new E_Tileset(animation, effect.PaletteIndex);
            for (int i = 0; i < tilemaps.Length; i++)
            {
                // add another mold if not enough
                if (i >= molds.Count)
                {
                    index = molds.Count - 1;
                    newMold.PerformClick();
                }
                Bits.Fill(molds[i].Mold, (byte)0xFF);
                Buffer.BlockCopy(tilemaps[i], 0, molds[i].Mold, 0, Math.Min(tilemaps[i].Length, molds[i].Mold.Length));
            }
            animation.Width = (byte)Math.Min(16, imports[0].Width / 16);
            animation.Height = (byte)Math.Min(16, imports[0].Height / 16);
            e_moldWidth.Value = Math.Min(16, imports[0].Width / 16);
            e_moldHeight.Value = Math.Min(16, imports[0].Height / 16);
            //
            animation.Assemble();
            SetTilesetImage();
            SetTilemapImage();
            sequences.SetSequenceFrameImages();
            sequences.RealignFrames();
            effectsEditor.LoadPaletteEditor();
            effectsEditor.LoadGraphicEditor();
        }
        private void saveImageAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Do.Export(tilesetImage, "effectAnimation." + animation.Index.ToString("d2") + ".Tileset.png");
        }
        // editors
        private void openTileEditor_Click(object sender, EventArgs e)
        {
            tileEditor.Visible = true;
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
