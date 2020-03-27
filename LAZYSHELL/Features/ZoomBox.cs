using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class ZoomBox : NewForm
    {
        private int zoom;
        public int Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                this.pictureBox.Size = new Size(48 * zoom, 48 * zoom);
                this.pictureBox.Left = -((48 * zoom - this.Width) / 2);
                this.pictureBox.Top = -((48 * zoom - this.Height) / 2);
            }
        }
        public PictureBox PictureBox
        {
            get { return pictureBox; }
            set { pictureBox = value; }
        }
        public ZoomBox(int zoom)
        {
            InitializeComponent();
            this.Zoom = zoom;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //
            Size size = new Size(pictureBox.Width / zoom, pictureBox.Height / zoom);
            Bitmap screenImage = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(screenImage);
            g.CopyFromScreen(Cursor.Position.X - (size.Width / 2), Cursor.Position.Y - (size.Height / 2), 0, 0, size);
            g.Dispose();
            Rectangle src = new Rectangle(0, 0, size.Width, size.Height);
            Rectangle dst = new Rectangle(0, 0, size.Width * zoom, size.Height * zoom);
            e.Graphics.DrawImage(screenImage, dst, src, GraphicsUnit.Pixel);
            // draw cursor
            int width =
                Cursor.Current == Cursors.Arrow ||
                Cursor.Current == Cursors.Cross ||
                Cursor.Current == Cursors.Hand ? 32 : 16;
            Rectangle cursorBounds = new Rectangle(
                    ((24 - Cursor.Current.HotSpot.X) * zoom),
                    ((24 - Cursor.Current.HotSpot.Y) * zoom),
                    width * zoom, width * zoom);
            if (Cursor.Current != null)
                Cursor.Current.DrawStretched(e.Graphics, cursorBounds);
            Rectangle clip = e.ClipRectangle;
            clip.Width--;
            clip.Height--;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDark), clip);
        }
    }
}
