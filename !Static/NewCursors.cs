using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; // remove later
using System.IO;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public static class NewCursors
    {
        // Cursors
        public static Cursor Draw = new Cursor(new MemoryStream(Resources.CursorDraw));
        public static Cursor Dropper = new Cursor(new MemoryStream(Resources.CursorDropper));
        public static Cursor Erase = new Cursor(new MemoryStream(Resources.CursorErase));
        public static Cursor Fill = new Cursor(new MemoryStream(Resources.CursorFill));
        public static Cursor Template = new Cursor(new MemoryStream(Resources.CursorTemplate));
        public static Cursor ZoomIn = new Cursor(new MemoryStream(Resources.CursorZoomIn));
        public static Cursor ZoomOut = new Cursor(new MemoryStream(Resources.CursorZoomOut));
    }
}
