using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    [Serializable()]
    public class Sequence
    {
        // variables
        private bool active;
        private List<Frame> frames = new List<Frame>();
        public bool Active { get { return this.active; } set { this.active = value; } }
        public List<Frame> Frames { get { return this.frames; } set { this.frames = value; } }
        // assemblers
        public void Disassemble(byte[] buffer, int offset)
        {
            Frame tFrame;
            if (Bits.GetShort(buffer, offset) == 0xFFFF)
                return;
            active = true;  //
            offset = (ushort)(Bits.GetShort(buffer, offset) & 0x7FFF);
            while (offset != 0x7FFF && buffer[offset] != 0)
            {
                tFrame = new Frame();
                tFrame.Disassemble(buffer, offset);
                frames.Add(tFrame);
                offset += 2;
            }
        }
        // accessor functions
        public Bitmap[] GetSequenceImages(Animation animation, ref List<int> durations)
        {
            durations.Clear();
            List<Bitmap> croppedFrames = new List<Bitmap>();
            List<int[]> frames = new List<int[]>();
            Rectangle thisBounds = new Rectangle();
            //
            Point UL = new Point(256, 256);
            Point BR = new Point(0, 0);
            foreach (Sequence.Frame frame in this.frames)
            {
                Rectangle bounds = new Rectangle(0, 0, 1, 1);
                if (frame.Mold < animation.Molds.Count)
                {
                    int[] pixels = animation.Molds[frame.Mold].MoldPixels();
                    animation.Molds[frame.Mold].MoldTilesPerPixel = null;
                    frames.Add(pixels);
                    durations.Add(frame.Duration * (1000 / 60));
                    bounds = Do.Crop(pixels, 256, 256);
                }
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
            if (UL.X >= BR.X ||
                UL.Y >= BR.Y)
                return croppedFrames.ToArray();
            thisBounds.X = UL.X;
            thisBounds.Y = UL.Y;
            thisBounds.Width = BR.X - UL.X;
            thisBounds.Height = BR.Y - UL.Y;
            foreach (int[] pixels in frames)
            {
                int[] cropped = Do.GetPixelRegion(pixels, thisBounds, 256, 256);
                Bits.Fill(cropped, Color.FromArgb(127, 127, 127).ToArgb(), true);
                Bitmap imageCropped = Do.PixelsToImage(cropped, thisBounds.Width, thisBounds.Height);
                croppedFrames.Add(new Bitmap(imageCropped));
            }
            return croppedFrames.ToArray();
        }
        // spawning
        public Sequence New()
        {
            Sequence empty = new Sequence();
            empty.Frames = new List<Frame>();
            return empty;
        }
        public Sequence Copy()
        {
            Sequence copy = new Sequence();
            copy.Frames = new List<Frame>();
            foreach (Frame frame in this.Frames)
                copy.Frames.Add(frame.Copy());
            copy.Active = active;
            return copy;
        }
        // classes
        [Serializable()]
        public class Frame
        {
            // class variables and accessors
            private byte duration; public byte Duration { get { return duration; } set { duration = value; } }
            private byte mold; public byte Mold { get { return mold; } set { mold = value; } }
            // assemblers
            public void Disassemble(byte[] buffer, int offset)
            {
                duration = buffer[offset];
                mold = buffer[offset + 1];
            }
            // spawning
            public Frame New()
            {
                Frame empty = new Frame();
                empty.Duration = 2;
                empty.Mold = 0;
                return empty;
            }
            public Frame Copy()
            {
                Frame copy = new Frame();
                copy.Duration = duration;
                copy.Mold = mold;
                return copy;
            }
        }
    }
}
