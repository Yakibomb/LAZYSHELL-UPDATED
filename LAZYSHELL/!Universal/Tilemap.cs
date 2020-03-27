using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public abstract class Tilemap
    {
        public abstract int Width_p { get; set; }
        public abstract int Height_p { get; set; }
        public abstract int[] Pixels { get; set; }
        public abstract Tile[] Tilemap_Tiles { get; set; }
        public abstract Tile[][] Tilemaps_Tiles { get; set; }
        public abstract byte[] Tilemap_Bytes { get; set; }
        public abstract byte[][] Tilemaps_Bytes { get; set; }
        public abstract Bitmap Image { get; set; }
        public abstract void SetTileNum();
        public abstract void SetTileNum(int tilenum, int layer, int x, int y);
        public abstract int GetTileNum(int index);
        public abstract int GetTileNum(int layer, int x, int y);
        public abstract int GetTileNum(int layer, int x, int y, bool ignoretransparent);
        public abstract void RedrawTilemaps();
        public abstract int[] GetPixels(int layer, Point location, Size size);
        public abstract int[] GetPixels(Point location, Size size);
        public abstract int[] GetPriority1Pixels();
        public abstract void Assemble();
        public abstract int GetPixelLayer(int x, int y);
    }
}
