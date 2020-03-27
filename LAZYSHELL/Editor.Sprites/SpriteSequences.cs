using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class SpriteSequences : NewForm
    {
        #region Variables
        // main editor accessed variables
        private Sprites spritesEditor;
        private Sprite sprite { get { return spritesEditor.Sprite; } set { spritesEditor.Sprite = value; } }
        private SpriteMolds molds { get { return spritesEditor.Molds; } set { spritesEditor.Molds = value; } }
        private Animation animation { get { return spritesEditor.Animation; } set { spritesEditor.Animation = value; } }
        private ImagePacket image { get { return spritesEditor.Image; } set { spritesEditor.Image = value; } }
        private int[] palette { get { return spritesEditor.Palette; } }
        private int availableBytes { get { return spritesEditor.AvailableBytes; } set { spritesEditor.AvailableBytes = value; } }
        // local variables
                private Mold mold { get { return animation.Molds[(int)frameMold.Value]; } }
        private Sequence sequence { get { return animation.Sequences[sequences.SelectedIndex]; } }
        private Sequence.Frame frame { get { return sequence.Frames[index]; } }
        private int index
        {
            get
            {
                if (frames.Tag != null)
                    return (int)frames.Tag;
                else
                    return 0;
            }
            set
            {
                if (value >= sequence.Frames.Count)
                    value = 0;
                if (value < 0)
                    value = sequence.Frames.Count - 1;
                if (sequence.Frames.Count > 0)
                {
                    frames.Tag = value;
                    this.Updating = true;
                    listBoxFrames.SelectedIndex = value;
                    this.Updating = false;
                    RefreshFrame();
                }
                foreach (PictureBox picture in frames.Controls)
                    picture.Invalidate();
            }
        }
        private List<int> indexes
        {
            get
            {
                List<int> array = new List<int>();
                foreach (int indice in listBoxFrames.SelectedIndices)
                    array.Add(indice);
                return array;
            }
        }
        private List<Bitmap> sequenceImages = new List<Bitmap>();
        private Bitmap sequenceImage;
        private Bitmap frameImage;
        private Rectangle bounds;
        private int duration_temp = 0;
        private Sequence sequence_temp = null;
        private ArrayList skip = new ArrayList();
        private List<Sequence.Frame> copiedFrames;
        // special controls
        #endregion
        #region Functions
        public SpriteSequences(Sprites spritesEditor)
        {
            this.spritesEditor = spritesEditor;
            InitializeComponent();
            this.skip.Add(pause);
            this.Updating = true;
            this.sequences.Items.Clear();
            for (int i = 0; i < animation.Sequences.Count; i++)
            {
                if (spritesEditor.Index >= 256 && spritesEditor.Index <= 511)
                    switch (i)
                    {
                        case 0: this.sequences.Items.Add("Idle front"); break;
                        case 1: this.sequences.Items.Add("Idle back"); break;
                        case 2: this.sequences.Items.Add("Recoil"); break;
                        case 3: this.sequences.Items.Add("Attack"); break;
                        case 4: this.sequences.Items.Add("Cast"); break;
                        default: this.sequences.Items.Add("Sequence " + i.ToString()); break;
                    }
                else
                    this.sequences.Items.Add("Sequence " + i.ToString());
            }
            sequences.SelectedIndex = 0;
            sequenceActive.Checked = sequence.Active;
            InitializeFrames();
            index = 0;
            this.Updating = false;
        }
        public void Reload(Sprites spritesEditor)
        {
            if (PlaybackSequence.IsBusy)
                PlaybackSequence.CancelAsync();
            this.spritesEditor = spritesEditor;
            this.Updating = true;
            this.sequences.Items.Clear();
            for (int i = 0; i < animation.Sequences.Count; i++)
            {
                if (spritesEditor.Index >= 256 && spritesEditor.Index <= 511)
                    switch (i)
                    {
                        case 0: this.sequences.Items.Add("Idle front"); break;
                        case 1: this.sequences.Items.Add("Idle back"); break;
                        case 2: this.sequences.Items.Add("Recoil"); break;
                        case 3: this.sequences.Items.Add("Attack"); break;
                        case 4: this.sequences.Items.Add("Cast"); break;
                        default: this.sequences.Items.Add("Sequence " + i.ToString()); break;
                    }
                else
                    this.sequences.Items.Add("Sequence " + i.ToString());
            }
            sequences.SelectedIndex = 0;
            sequenceActive.Checked = sequence.Active;
            InitializeFrames();
            index = 0;
            this.Updating = false;
        }
        private void RefreshSequence()
        {
            if (PlaybackSequence.IsBusy)
                PlaybackSequence.CancelAsync();
            this.Updating = true;
            sequenceActive.Checked = sequence.Active;
            this.Updating = false;
            if (sequence.Frames.Count != 0)
            {
                toolStrip1.Enabled = true;
                deleteFrame.Enabled = true;
                duplicateFrame.Enabled = true;
                moveFrameBack.Enabled = true;
                moveFrameFoward.Enabled = true;
                reverseFrames.Enabled = true;
                panelSequence.Enabled = true;
                frames.Enabled = true;
                InitializeFrames();
            }
            else
            {
                toolStrip1.Enabled = false;
                deleteFrame.Enabled = false;
                duplicateFrame.Enabled = false;
                moveFrameBack.Enabled = false;
                moveFrameFoward.Enabled = false;
                reverseFrames.Enabled = false;
                panelSequence.Enabled = false;
                frames.Enabled = false;
                frames.Controls.Clear();
                listBoxFrames.Items.Clear();
                sequenceImage = null;
                frames.Tag = null;
                pictureBoxSequence.Invalidate();
            }
        }
        //
        private void InitializeFrames()
        {
            this.Updating = true;
            panelFrames.AutoScrollPosition = new Point(0, 0);
            SetSequenceFrameImages();
            DrawFrames();
            if (sequence.Frames.Count == 0)
                toolStrip1.Enabled = false;
            else
            {
                index = 0;
                toolStrip1.Enabled = true;
                this.frameMold.Value = frame.Mold;
                this.duration.Value = frame.Duration;
            }
            this.Updating = false;
        }
        private void RefreshFrame()
        {
            this.Updating = true;
            if (sequence.Frames.Count != 0)
            {
                this.frameMold.Enabled = true;
                this.duration.Enabled = true;
                this.frameMold.Value = frame.Mold;
                this.duration.Value = frame.Duration;
                //this.panelFrames.AutoScrollPosition = new Point(index * ((frameBounds.Width / 2) + 4), 0);
            }
            else
            {
                frameMold.Enabled = false; frameMold.Value = 0;
                duration.Enabled = false; duration.Value = 1;
                sequenceImage = null;
            }
            SetSequenceFrameImage();
            this.Updating = false;
        }
        private void DrawFrames()
        {
            this.panelFrames.AutoScrollPosition = new Point(0, 0);
            this.frames.Controls.Clear();
            this.listBoxFrames.BeginUpdate();
            this.listBoxFrames.Items.Clear();
            frames.Width = sequence.Frames.Count * (bounds.Width + 4) + Screen.PrimaryScreen.WorkingArea.Width;
            for (int i = 0; i < sequence.Frames.Count; i++)
            {
                PictureBox frame = new PictureBox();
                frame.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
                frame.BorderStyle = BorderStyle.None;
                frame.Location = new Point(i * (this.bounds.Width + 4) + 4,
                    (this.frames.Height / 2) - (this.bounds.Height / 2));
                frame.Name = "frame" + i;
                frame.Size = new Size(bounds.Width, bounds.Height);
                frame.Tag = i;
                frame.MouseDown += new MouseEventHandler(frame_MouseDown);
                frame.Paint += new PaintEventHandler(frame_Paint);
                frame.PreviewKeyDown += new PreviewKeyDownEventHandler(frame_PreviewKeyDown);
                this.frames.Controls.Add(frame);
                listBoxFrames.Items.Add("Frame " + i);
            }
            this.listBoxFrames.EndUpdate();
        }
        private void RealignFrames()
        {
            int i = 0;
            foreach (PictureBox frame in frames.Controls)
            {
                frame.Tag = i;
                frame.Left = i * (frame.Width + 4) + 4;
                listBoxFrames.Items[i] = "Frame " + i;
                i++;
            }
            frames.Width = sequence.Frames.Count * (bounds.Width + 4) + Screen.PrimaryScreen.WorkingArea.Width;
        }
        //
        public void InvalidateImages()
        {
            pictureBoxSequence.Invalidate();
            foreach (PictureBox frame in frames.Controls)
                frame.Invalidate();
        }
        public void SetSequenceFrameImages()
        {
            this.sequenceImages.Clear();
            Point UL = new Point(256, 256);
            Point BR = new Point(0, 0);
            foreach (Sequence.Frame frame in sequence.Frames)
            {
                if (frame.Mold < animation.Molds.Count)
                {
                    int[] pixels = animation.Molds[frame.Mold].MoldPixels();
                    this.frameImage = Do.PixelsToImage(pixels, 256, 256);
                    this.sequenceImages.Add(new Bitmap(frameImage));
                    Rectangle bounds = Do.Crop(pixels, 256, 256);
                    // if the mold is empty
                    if (bounds.X == 0 &&
                        bounds.Y == 0 &&
                        bounds.Width == 1 &&
                        bounds.Height == 1)
                        continue;
                    if (bounds.X < UL.X)
                        UL.X = bounds.X;
                    if (bounds.Y < UL.Y)
                        UL.Y = bounds.Y;
                    if (bounds.X + bounds.Width > BR.X)
                        BR.X = bounds.X + bounds.Width;
                    if (bounds.Y + bounds.Height > BR.Y)
                        BR.Y = bounds.Y + bounds.Height;
                }
                else
                    this.sequenceImages.Add(new Bitmap(20, 20));
            }
            this.bounds.X = UL.X - 1;
            this.bounds.Y = UL.Y - 1;
            this.bounds.Width = BR.X - UL.X + 2;
            this.bounds.Height = BR.Y - UL.Y + 2;
            pictureBoxSequence.Size = this.bounds.Size;
            SetSequenceFrameImage();
        }
        public void SetSequenceFrameImage()
        {
            if (index < sequenceImages.Count)
                sequenceImage = new Bitmap((Bitmap)sequenceImages[index]);
            else
                sequenceImage = new Bitmap(20, 20);
            foreach (PictureBox picture in frames.Controls)
                picture.Invalidate();
            pictureBoxSequence.Invalidate();
        }
        #endregion
        #region Event Handlers
        private void pictureBoxSequence_Paint(object sender, PaintEventArgs e)
        {
            if (frames.Tag == null)
                return;
            if (molds.ShowBG)
                e.Graphics.Clear(Color.FromArgb(palette[0]));
            if (sequenceImage != null)
                e.Graphics.DrawImage(sequenceImage, -bounds.X, -bounds.Y, 256, 256);
        }
        private void frame_Paint(object sender, PaintEventArgs e)
        {
            if (sequence.Frames.Count == 0)
                return;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            PictureBox frame = (PictureBox)sender;
            int index = (int)frame.Tag;
            if (molds.ShowBG)
                e.Graphics.Clear(Color.FromArgb(palette[0]));
            if (index < sequence.Frames.Count && sequence.Frames[index].Mold < animation.Molds.Count)
            {
                if (index < sequenceImages.Count)
                    e.Graphics.DrawImage(sequenceImages[index], -bounds.X, -bounds.Y, 256, 256);
            }
            else
            {
                e.Graphics.DrawImage(Resources.warning, 2, 2);
            }
            if (this.indexes.Contains(index))
            {
                e.Graphics.DrawRectangle(
                    new Pen(new SolidBrush(Color.Red)),
                    new Rectangle(0, 0, bounds.Width - 1, bounds.Height - 1));
                if (!molds.Picture.Focused)
                    frame.Focus();
            }
            else
            {
                e.Graphics.DrawRectangle(
                    new Pen(new SolidBrush(SystemColors.ControlDark)),
                    new Rectangle(0, 0, bounds.Width - 1, bounds.Height - 1));
            }
        }
        private void frame_MouseDown(object sender, MouseEventArgs e)
        {
            if (PlaybackSequence.IsBusy)
                return;
            PictureBox frame = (PictureBox)sender;
            frame.Focus();
            index = (int)frame.Tag;
            if ((Control.ModifierKeys & Keys.Control) == 0) // multi-select if Ctrl pressed
            {
                this.Updating = true;
                listBoxFrames.ClearSelected();
                listBoxFrames.SelectedIndex = index;
                this.Updating = false;
            }
            if (panelFrames.HorizontalScroll.Visible)
                panelFrames.HorizontalScroll.Value = index * (bounds.Width + 4);
            panelFrames.Focus();
        }
        private void frame_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (PlaybackSequence.IsBusy)
                return;
            if (e.KeyData == Keys.Right || e.KeyData == Keys.Down)
            {
                index++;
                this.Updating = true;
                listBoxFrames.ClearSelected();
                listBoxFrames.SelectedIndex = index;
            }
            if (e.KeyData == Keys.Left || e.KeyData == Keys.Up)
            {
                index--;
                this.Updating = true;
                listBoxFrames.ClearSelected();
                listBoxFrames.SelectedIndex = index;
            }
            this.Updating = false;
            if (panelFrames.HorizontalScroll.Visible)
                panelFrames.HorizontalScroll.Value = index * (bounds.Width + 4);
        }
        private void panelFrames_SizeChanged(object sender, EventArgs e)
        {
            frames.Width = sequence.Frames.Count * (bounds.Width + 4) + Screen.PrimaryScreen.WorkingArea.Width;
        }
        private void panelSequence_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxSequence.Location = new Point(
                (panelSequence.Width / 2) - (pictureBoxSequence.Width / 2),
                (panelSequence.Height / 2) - (pictureBoxSequence.Height / 2));
        }
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PlaybackSequence.IsBusy)
                PlaybackSequence.CancelAsync();
            if (this.Updating)
                return;
            index = listBoxFrames.SelectedIndex;
            if (panelFrames.HorizontalScroll.Visible)
                panelFrames.HorizontalScroll.Value = index * (bounds.Width + 4);
        }
        private void sequences_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshSequence();
            sequences.Focus();
        }
        private void duration_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            frame.Duration = (byte)duration.Value;
        }
        private void frameMold_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if ((int)frameMold.Value >= animation.Molds.Count)
                frameMold.Value = animation.Molds.Count - 1;
            frame.Mold = (byte)frameMold.Value;
            SetSequenceFrameImages();
        }
        private void play_Click(object sender, EventArgs e)
        {
            sequence_temp = sequence;
            if (sequence_temp == null)
                return;
            PlaybackSequence.CancelAsync();
            spritesEditor.EnableOnPlayback(false);
            panelSequence.BringToFront();
            PlaybackSequence.RunWorkerAsync();
        }
        private void pause_Click(object sender, EventArgs e)
        {
            if (PlaybackSequence.IsBusy) PlaybackSequence.CancelAsync();
        }
        private void back_Click(object sender, EventArgs e)
        {
            index--;
            this.Updating = true;
            listBoxFrames.ClearSelected();
            listBoxFrames.SelectedIndex = index;
            this.Updating = false;
        }
        private void foward_Click(object sender, EventArgs e)
        {
            index++;
            this.Updating = true;
            listBoxFrames.ClearSelected();
            listBoxFrames.SelectedIndex = index;
            this.Updating = false;
        }
        private void PlaybackSequence_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; !PlaybackSequence.CancellationPending; i++)
            {
                if (PlaybackSequence.CancellationPending)
                    break;
                if (i >= frames.Controls.Count)
                    i = 0;
                PlaybackSequence.ReportProgress(i);
                duration_temp = sequence_temp.Frames[i].Duration;
                if (duration_temp >= 1)
                    Thread.Sleep(duration_temp * (1000 / 60));
                else
                    Thread.Sleep(1000 / 60);
                if (PlaybackSequence.CancellationPending)
                    break;
            }
        }
        private void PlaybackSequence_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            sequenceImage = new Bitmap((Bitmap)sequenceImages[e.ProgressPercentage]);
            pictureBoxSequence.Invalidate();
            // if at last frame and no infinite playback
            if (e.ProgressPercentage >= sequenceImages.Count - 1 && !infinitePlayback.Checked)
                PlaybackSequence.CancelAsync();
        }
        private void PlaybackSequence_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spritesEditor.EnableOnPlayback(true);
            panelFrames.BringToFront();
            RefreshFrame();
        }
        private void saveSequenceImages_Click(object sender, EventArgs e)
        {
            int counter = 0;
            int frameCounter = 0;
            foreach (Sequence.Frame frame in sequence.Frames)
            {
                int duration = (int)(((double)frame.Duration / 60.0 * 100.0) / 3);
                while (duration-- > 0)
                    (sequenceImages[frameCounter]).Save(
                        "sprite." + sprite.Index.ToString("d4") + ".Sequence." +
                        index.ToString("d2") + ".Frame." + counter++.ToString("d3") + ".png");
                frameCounter++;
            }
        }
        // adding,deleting
        private void sequenceActive_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            sequence.Active = sequenceActive.Checked;
        }
        private void exportAnimatedGIF_Click(object sender, EventArgs e)
        {
            if (sequences.SelectedIndex < 0)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image file (*.gif)|*.gif";
            saveFileDialog.FileName = "sequence." + sequences.SelectedIndex + ".gif";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save Animated GIF";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            else
                Settings.Default.LastDirectory = saveFileDialog.FileName;
            //
            foreach (Mold m in animation.Molds)
            {
                foreach (Mold.Tile t in m.Tiles)
                    t.DrawSubtiles(sprite.Graphics, sprite.Palette, m.Gridplane);
            }
            List<int> durations = new List<int>();
            Bitmap[] croppedFrames = sequence.GetSequenceImages(animation, ref durations);
            //
            if (croppedFrames.Length > 0)
                Do.ImagesToAnimatedGIF(croppedFrames, durations.ToArray(), saveFileDialog.FileName);
        }
        private void newSequence_Click(object sender, EventArgs e)
        {
            if (animation.Sequences.Count == 16)
            {
                MessageBox.Show("Animations cannot contain more than 16 sequences total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = sequences.SelectedIndex + 1;
            animation.Sequences.Insert(index, sequence.New());
            this.Updating = true;
            sequences.BeginUpdate();
            sequences.Items.Insert(index, "Sequence " + index);
            for (int i = 0; i < sequences.Items.Count; i++)
                sequences.Items[i] = "Sequence " + i;
            sequences.EndUpdate();
            this.Updating = false;
            sequences.SelectedIndex = index;
        }
        private void deleteSequence_Click(object sender, EventArgs e)
        {
            if (animation.Sequences.Count == 1)
            {
                MessageBox.Show("Animations must contain at least one sequence.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = sequences.SelectedIndex;
            animation.Sequences.RemoveAt(index);
            this.Updating = true;
            sequences.BeginUpdate();
            sequences.Items.RemoveAt(index);
            for (int i = 0; i < sequences.Items.Count; i++)
                sequences.Items[i] = "Sequence " + i;
            sequences.EndUpdate();
            this.Updating = false;
            if (index < sequences.Items.Count)
                sequences.SelectedIndex = index;
            else
                sequences.SelectedIndex = index - 1;
        }
        private void duplicateSequence_Click(object sender, EventArgs e)
        {
            if (animation.Sequences.Count == 16)
            {
                MessageBox.Show("Animations cannot contain more than 16 sequences total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = sequences.SelectedIndex + 1;
            animation.Sequences.Insert(index, sequence.Copy());
            this.Updating = true;
            sequences.BeginUpdate();
            sequences.Items.Insert(index, "Sequence " + index);
            for (int i = 0; i < sequences.Items.Count; i++)
                sequences.Items[i] = "Sequence " + i;
            sequences.EndUpdate();
            this.Updating = false;
            sequences.SelectedIndex = index;
        }
        private void moveSequenceBack_Click(object sender, EventArgs e)
        {
            if (sequences.SelectedIndex == 0)
                return;
            int index = sequences.SelectedIndex - 1;
            animation.Sequences.Reverse(index, 2);
            this.Updating = true;
            sequences.SelectedIndex--;
            this.Updating = false;
        }
        private void moveSeqeuenceFoward_Click(object sender, EventArgs e)
        {
            if (sequences.SelectedIndex == animation.Sequences.Count - 1)
                return;
            int index = sequences.SelectedIndex;
            animation.Sequences.Reverse(index, 2);
            this.Updating = true;
            sequences.SelectedIndex++;
            this.Updating = false;
        }
        private void newFrame_Click(object sender, EventArgs e)
        {
            if (sequence.Frames.Count == 256)
            {
                MessageBox.Show("Sequences cannot contain more than 256 frames total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index = 0;
            if (sequence.Frames.Count != 0)
                index = this.index + 1;
            sequence.Frames.Insert(index, new Sequence.Frame().New());
            DrawFrames();
            RefreshFrame();
            SetSequenceFrameImages();
            this.index = index;
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
            toolStrip1.Enabled = duplicateFrame.Enabled = deleteFrame.Enabled =
                moveFrameBack.Enabled = moveFrameFoward.Enabled = reverseFrames.Enabled = true;
        }
        private void copy_Click(object sender, EventArgs e)
        {
            if (listBoxFrames.SelectedIndices.Count == 0)
                return;
            copiedFrames = new List<Sequence.Frame>();
            foreach (int indice in listBoxFrames.SelectedIndices)
                copiedFrames.Add(sequence.Frames[indice].Copy());
        }
        private void paste_Click(object sender, EventArgs e)
        {
            if (sequence.Frames.Count + copiedFrames.Count >= 256)
            {
                MessageBox.Show("Sequences cannot contain more than 256 frames total.", "LAZY SHELL",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index;
            if (sequence.Frames.Count != 0)
                index = this.index + 1;
            else
                index = this.index;
            foreach (Sequence.Frame frame in copiedFrames)
                sequence.Frames.Insert(index++, frame);
            index -= copiedFrames.Count;
            //
            DrawFrames();
            RefreshFrame();
            SetSequenceFrameImages();
            foreach (Sequence.Frame frame in copiedFrames)
                listBoxFrames.SetSelected(index++, true);
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
            toolStrip1.Enabled = duplicateFrame.Enabled = deleteFrame.Enabled =
                moveFrameBack.Enabled = moveFrameFoward.Enabled = reverseFrames.Enabled = true;
        }
        private void duplicateFrame_Click(object sender, EventArgs e)
        {
            copy.PerformClick();
            paste.PerformClick();
        }
        private void deleteFrame_Click(object sender, EventArgs e)
        {
            List<int> indexes = this.indexes;
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                sequence.Frames.RemoveAt(indexes[i]);
                frames.Controls.RemoveAt(indexes[i]);
                sequenceImages.RemoveAt(indexes[i]);
                listBoxFrames.Items.RemoveAt(indexes[i]);
            }
            if (index >= sequence.Frames.Count && sequence.Frames.Count != 0)
                this.index = sequence.Frames.Count - 1;
            else if (sequence.Frames.Count != 0)
                this.index = index;
            //
            RefreshFrame();
            RealignFrames();
            // update free space
            animation.Assemble();
            spritesEditor.CalculateFreeSpace();
            if (sequence.Frames.Count == 0)
                toolStrip1.Enabled = duplicateFrame.Enabled = deleteFrame.Enabled =
                    moveFrameBack.Enabled = moveFrameFoward.Enabled = reverseFrames.Enabled = false;
        }
        private void moveFrameBack_Click(object sender, EventArgs e)
        {
            if (this.indexes[0] == 0)
                return;
            List<int> indexes = this.indexes;
            for (int i = 0; i < indexes.Count; i++)
            {
                listBoxFrames.SetSelected(indexes[i], false);
                listBoxFrames.SetSelected(indexes[i] - 1, true);
                sequence.Frames.Reverse(indexes[i] - 1, 2);
                sequenceImages.Reverse(indexes[i] - 1, 2);
            }
            RealignFrames();
        }
        private void moveFrameFoward_Click(object sender, EventArgs e)
        {
            if (this.indexes[this.indexes.Count - 1] >= listBoxFrames.Items.Count - 1)
                return;
            List<int> indexes = this.indexes;
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                listBoxFrames.SetSelected(indexes[i], false);
                listBoxFrames.SetSelected(indexes[i] + 1, true);
                sequence.Frames.Reverse(indexes[i], 2);
                sequenceImages.Reverse(indexes[i], 2);
            }
            RealignFrames();
        }
        private void reverseFrames_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reverse the order of all frames in the current sequence.\n\n" +
                "Continue with process?", "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            for (int i = 1; i < sequence.Frames.Count; i++)
            {
                for (int c = 0; c < sequence.Frames.Count - i; c++)
                {
                    sequence.Frames.Reverse(c, 2);
                    sequenceImages.Reverse(c, 2);
                }
            }
            index = 0;
            RealignFrames();
        }
        #endregion
    }
}
