using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class EffectSequences : NewForm
    {
        #region Variables
        // main editor accessed variables
        private Effects effectsEditor;
        private Effect effect { get { return effectsEditor.Effect; } set { effectsEditor.Effect = value; } }
        private EffectMolds molds { get { return effectsEditor.Molds; } set { effectsEditor.Molds = value; } }
        private E_Animation animation { get { return effectsEditor.Animation; } set { effectsEditor.Animation = value; } }
        private int availableBytes { get { return effectsEditor.AvailableBytes; } set { effectsEditor.AvailableBytes = value; } }
        // local variables
                private E_Tileset tileset { get { return animation.Tileset_tiles; } set { animation.Tileset_tiles = value; } }
        private E_Mold mold { get { return (E_Mold)animation.Molds[(int)frameMold.Value]; } }
        private E_Sequence sequence { get { return (E_Sequence)animation.Sequences[0]; } }
        private E_Sequence.Frame frame { get { return (E_Sequence.Frame)sequence.Frames[index]; } }
        private int index
        {
            get { return (int)frames.Tag; }
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
        private List<Bitmap> sequenceImages = new List<Bitmap>();
        private Bitmap sequenceImage;
        private Bitmap frameImage;
        private int width { get { return animation.Width * 16; } }
        private int height { get { return animation.Height * 16; } }
        private double ratio { get { return (double)width / (double)height; } }
        private int duration_temp = 0;
        // special controls
        #endregion
        #region Functions
        public EffectSequences(Effects effectsEditor)
        {
            this.effectsEditor = effectsEditor;
            this.animation = animation;
            InitializeComponent();
            this.pictureBoxSequence.Size = new Size(animation.Width * 16, animation.Height * 16);
            InitializeFrames();
            index = 0;
            pictureBoxSequence.Location = new Point(
                (panelSequence.Width / 2) - (pictureBoxSequence.Width / 2),
                (panelSequence.Height / 2) - (pictureBoxSequence.Height / 2));
        }
        public void Reload(Effects effectsEditor)
        {
            this.effectsEditor = effectsEditor;
            this.animation = animation;
            InitializeFrames();
            index = 0;
            pictureBoxSequence.Location = new Point(
                (panelSequence.Width / 2) - (pictureBoxSequence.Width / 2),
                (panelSequence.Height / 2) - (pictureBoxSequence.Height / 2));
        }
        private void InitializeFrames()
        {
            this.Updating = true;
            panelFrames.AutoScrollPosition = new Point(0, 0);
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
            SetSequenceFrameImages();
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
                this.panelFrames.AutoScrollPosition = new Point(index * (frames.Controls[index].Width + 4), 0);
            }
            else
            {
                frameMold.Enabled = false; frameMold.Value = 0;
                duration.Enabled = false; duration.Value = 1;
                sequenceImage = null;
            }
            this.Updating = false;
        }
        public void DrawFrames()
        {
            this.frames.Controls.Clear();
            this.listBoxFrames.BeginUpdate();
            this.listBoxFrames.Items.Clear();
            this.frames.Width = (width + 4) * sequence.Frames.Count + Screen.PrimaryScreen.WorkingArea.Width;
            this.frames.Height = height + 8;
            for (int i = 0; i < sequence.Frames.Count; i++)
            {
                PictureBox frame = new PictureBox();
                frame.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
                frame.BorderStyle = BorderStyle.None;
                frame.Name = "frame" + i;
                frame.Size = new Size(width, height);
                frame.Location = new Point((frame.Width + 4) * i + 4, 4);
                frame.Tag = i;
                frame.MouseDown += new MouseEventHandler(frame_MouseDown);
                frame.Paint += new PaintEventHandler(frame_Paint);
                frame.PreviewKeyDown += new PreviewKeyDownEventHandler(frame_PreviewKeyDown);
                this.frames.Controls.Add(frame);
                listBoxFrames.Items.Add("Frame " + i);
            }
            this.listBoxFrames.EndUpdate();
        }
        public void RealignFrames()
        {
            this.frames.Width = (width + 4) * sequence.Frames.Count + Screen.PrimaryScreen.WorkingArea.Width;
            this.frames.Height = height + 8;
            int i = 0;
            foreach (PictureBox frame in frames.Controls)
            {
                frame.Location = new Point((frame.Width + 4) * i + 4, 4);
                frame.Size = new Size(width, height);
                frame.Tag = i;
                listBoxFrames.Items[i] = "Frame " + i++;
            }
        }
        public void InvalidateFrameImages()
        {
            pictureBoxSequence.Invalidate();
            foreach (PictureBox frame in frames.Controls)
                frame.Invalidate();
        }
        public void SetSequenceFrameImages()
        {
            sequenceImages.Clear();
            int i = 0;
            foreach (E_Sequence.Frame frame in sequence.Frames)
            {
                if (frame.Mold < animation.Molds.Count)
                {
                    int[] pixels = ((E_Mold)animation.Molds[frame.Mold]).MoldPixels(animation, tileset);
                    frameImage = Do.PixelsToImage(pixels, width, height);
                    sequenceImages.Add(new Bitmap(frameImage));
                }
                else
                {
                    //MessageBox.Show("Mold for frame #" + i.ToString() + " is not valid. Change to lower value.", "LAZYSHELL++");
                    sequenceImages.Add(new Bitmap(256, 256));
                }
                i++;
            }
            this.pictureBoxSequence.Size = new Size(animation.Width * 16, animation.Height * 16);
        }
        #endregion
        #region Event Handlers
        // main
        private void pictureBoxSequence_Paint(object sender, PaintEventArgs e)
        {
            if (frames.Tag == null)
                return;
            if (!molds.ShowBG)
                e.Graphics.Clear(Color.FromArgb(animation.PaletteSet.Palette[0]));
            if (sequenceImage != null)
                e.Graphics.DrawImage(sequenceImage, 0, 0);
        }
        private void panelSequence_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxSequence.Location = new Point(
                (panelSequence.Width / 2) - (pictureBoxSequence.Width / 2),
                (panelSequence.Height / 2) - (pictureBoxSequence.Height / 2));
        }
        private void frame_Paint(object sender, PaintEventArgs e)
        {
            PictureBox frame = (PictureBox)sender;
            if ((int)frame.Tag >= sequenceImages.Count)
                return;
            if (!molds.ShowBG)
                e.Graphics.Clear(Color.FromArgb(animation.PaletteSet.Palette[0]));
            Rectangle dst = new Rectangle(0, 0, frame.Width, frame.Height);
            Rectangle src = new Rectangle(0, 0, width, height);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            e.Graphics.DrawImage(sequenceImages[(int)frame.Tag], dst, src, GraphicsUnit.Pixel);
            //
            if (sequence.Frames[(int)frame.Tag].Mold >= animation.Molds.Count)
            {
                Font font = new Font("Tahoma", 10F, FontStyle.Bold);
                SizeF size = e.Graphics.MeasureString("(INVALID MOLD INDEX)", font, new PointF(0, 0), StringFormat.GenericDefault);
                Point point = new Point((frame.Width - (int)size.Width) / 2, (frame.Height - (int)size.Height) / 2);
                Do.DrawString(e.Graphics, point, "(INVALID MOLD INDEX)", Color.Black, Color.Red, font);
            }
            if (index == (int)frame.Tag)
            {
                e.Graphics.DrawRectangle(
                    new Pen(new SolidBrush(Color.Red)),
                    new Rectangle(0, 0, frame.Width - 1, frame.Height - 1));
                if (!molds.Picture.Focused)
                    frame.Focus();
            }
            else
            {
                e.Graphics.DrawRectangle(
                    new Pen(new SolidBrush(SystemColors.ControlDark)),
                    new Rectangle(0, 0, frame.Width - 1, frame.Height - 1));
            }
        }
        private void frame_MouseDown(object sender, MouseEventArgs e)
        {
            if (PlaybackSequence.IsBusy)
                return;
            PictureBox frame = (PictureBox)sender;
            frame.Focus();
            index = (int)frame.Tag;
        }
        private void frame_SizeChanged(object sender, EventArgs e)
        {
            PictureBox frame = (PictureBox)sender;
            double ratio = (double)width / (double)height;
            frame.Width = (int)(frame.Height * ratio);
        }
        private void frame_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (PlaybackSequence.IsBusy)
                return;
            if (e.KeyData == Keys.Right || e.KeyData == Keys.Down)
                index++;
            if (e.KeyData == Keys.Left || e.KeyData == Keys.Up)
                index--;
            if (e.KeyData == Keys.Delete)
                deleteFrame.PerformClick();
        }
        private void panelFrames_SizeChanged(object sender, EventArgs e)
        {
            double ratio = (double)width / (double)height;
            frames.Width = (width + 4) * sequence.Frames.Count + Screen.PrimaryScreen.WorkingArea.Width;
        }
        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            index = listBoxFrames.SelectedIndex;
        }
        private void duration_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            frame.Duration = (byte)duration.Value;
            SetSequenceFrameImages();
        }
        private void frameMold_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if ((int)frameMold.Value >= animation.Molds.Count)
                frameMold.Value = animation.Molds.Count - 1;
            frame.Mold = (byte)frameMold.Value;
            SetSequenceFrameImages();
            InvalidateFrameImages();
        }
        // playback
        private void play_Click(object sender, EventArgs e)
        {
            PlaybackSequence.CancelAsync();
            effectsEditor.EnableOnPlayback(false);
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
        }
        private void foward_Click(object sender, EventArgs e)
        {
            index++;
        }
        private void PlaybackSequence_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; !PlaybackSequence.CancellationPending; i++)
            {
                if (PlaybackSequence.CancellationPending) break;
                if (i >= frames.Controls.Count) i = 0;
                PlaybackSequence.ReportProgress(i);
                duration_temp = ((E_Sequence.Frame)sequence.Frames[i]).Duration;
                if (duration_temp >= 1)
                    Thread.Sleep(duration_temp * (1000 / 60));
                else
                    Thread.Sleep(1000 / 60);
                if (PlaybackSequence.CancellationPending) break;
            }
        }
        private void PlaybackSequence_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            sequenceImage = new Bitmap(sequenceImages[e.ProgressPercentage]);
            pictureBoxSequence.Invalidate();
        }
        private void PlaybackSequence_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Updating = false;
            effectsEditor.EnableOnPlayback(true);
            panelFrames.BringToFront();
            RefreshFrame();
        }
        // adding,deleting
        private void newFrame_Click(object sender, EventArgs e)
        {
            if (sequence.Frames.Count == 256)
            {
                MessageBox.Show("Sequences cannot contain more than 256 frames total.", "LAZYSHELL++",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int index;
            if (sequence.Frames.Count != 0)
                index = this.index + 1;
            else
                index = this.index;
            sequence.Frames.Insert(index, new E_Sequence.Frame().New());
            DrawFrames();
            RefreshFrame();
            SetSequenceFrameImages();
            this.index = index;
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
            toolStrip1.Enabled = duplicate.Enabled = deleteFrame.Enabled =
                moveFrameBack.Enabled = moveFrameFoward.Enabled = reverseFrames.Enabled = true;
        }
        private void deleteFrame_Click(object sender, EventArgs e)
        {
            int index = this.index;
            sequence.Frames.RemoveAt(index);
            frames.Controls.RemoveAt(index);
            sequenceImages.RemoveAt(index);
            listBoxFrames.Items.RemoveAt(index);
            if (index >= sequence.Frames.Count && sequence.Frames.Count != 0)
                this.index = index - 1;
            else if (sequence.Frames.Count != 0)
                this.index = index;
            RefreshFrame();
            RealignFrames();
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
            if (sequence.Frames.Count == 0)
                toolStrip1.Enabled = duplicate.Enabled = deleteFrame.Enabled =
                    moveFrameBack.Enabled = moveFrameFoward.Enabled = reverseFrames.Enabled = false;
        }
        private void duplicate_Click(object sender, EventArgs e)
        {
            int index = this.index;
            sequence.Frames.Insert(index + 1, this.frame.Copy());
            DrawFrames();
            RefreshFrame();
            SetSequenceFrameImages();
            this.index = index + 1;
            // update free space
            animation.Assemble();
            effectsEditor.CalculateFreeSpace();
        }
        private void moveFrameBack_Click(object sender, EventArgs e)
        {
            if (this.index == 0)
                return;
            int index = this.index - 1;
            sequence.Frames.Reverse(index, 2);
            sequenceImages.Reverse(index, 2);
            RealignFrames();
            this.index--;
        }
        private void moveFrameFoward_Click(object sender, EventArgs e)
        {
            if (this.index == sequence.Frames.Count - 1)
                return;
            int index = this.index;
            sequence.Frames.Reverse(index, 2);
            sequenceImages.Reverse(index, 2);
            RealignFrames();
            this.index++;
        }
        private void reverseFrames_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reverse the order of all frames in the effect sequence.\n\n" +
                "Continue with process?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
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
