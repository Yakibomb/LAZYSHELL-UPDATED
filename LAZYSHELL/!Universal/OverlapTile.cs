using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    public class OverlapTile
    {
        // class variables
        private Tile[] subtiles = new Tile[4];
        private int tileNumber; public int TileNumber { get { return tileNumber; } }
        private Tile[] preview;
        // public accessors
        public int[] Pixels
        {
            get
            {
                int[] pixels = new int[32 * 32];
                Do.PixelsToPixels(GetSubtile(0).Pixels, pixels, 32, new Rectangle(0, 0, 16, 16));
                Do.PixelsToPixels(GetSubtile(1).Pixels, pixels, 32, new Rectangle(16, 0, 16, 16));
                Do.PixelsToPixels(GetSubtile(2).Pixels, pixels, 32, new Rectangle(0, 16, 16, 16));
                Do.PixelsToPixels(GetSubtile(3).Pixels, pixels, 32, new Rectangle(16, 16, 16, 16));
                return pixels;
            }
            set
            {
                subtiles[0].Pixels = Do.GetPixelRegion(value, new Rectangle(0, 0, 16, 16), 32, 32);
                subtiles[1].Pixels = Do.GetPixelRegion(value, new Rectangle(16, 0, 16, 16), 32, 32);
                subtiles[2].Pixels = Do.GetPixelRegion(value, new Rectangle(0, 16, 16, 16), 32, 32);
                subtiles[3].Pixels = Do.GetPixelRegion(value, new Rectangle(16, 16, 16, 16), 32, 32);
            }
        }
        public Tile GetSubtile(int placement)
        {
            if (this.isBeingModified)
                return preview[placement];
            return subtiles[placement];
        }
        public void SetSubtile(Tile tile, int placement)
        {
            //[0][1]
            //[2][3]
            if (isBeingModified)
                preview[placement] = tile;
            else
                subtiles[placement] = tile;
        }
        private bool isBeingModified = false;
        public bool IsBeingModified
        {
            get
            {
                return this.isBeingModified;
            }
            set
            {
                this.isBeingModified = value;
                if (this.isBeingModified && this.preview == null)
                {
                    preview = new Tile[4];
                    for (int i = 0; i < 4; i++)
                        preview[i] = subtiles[i];
                }
                else if (!this.isBeingModified)
                {
                    this.preview = null;
                }
            }
        }
        // constructor
        public OverlapTile(int tileNumber)
        {
            this.tileNumber = tileNumber; // set tile Number
        }
        // class functions
        public void ConfirmChanges()
        {
            if (!this.isBeingModified)
                return;
            for (int i = 0; i < 4; i++)
                subtiles[i] = preview[i];
            this.IsBeingModified = false;
        }
    }
}
