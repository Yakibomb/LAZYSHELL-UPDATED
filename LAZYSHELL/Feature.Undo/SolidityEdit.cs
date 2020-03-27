using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL.Undo
{
    class SolidityEdit : Command
    {
        // variables
        private Levels updater;
        private byte[] changes;
        private Point start, stop, initial;
        private Tilemap tilemap;
        public Tilemap Tilemap { get { return tilemap; } set { tilemap = value; } }
        private Solidity solidity = Solidity.Instance;
        private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
        // constructor
        public SolidityEdit(Levels updater, Tilemap tilemap, Point start, Point stop, Point initial, byte[] changes)
        {
            this.updater = updater;
            this.tilemap = tilemap;
            //
            if (start.Y < stop.Y)
            {
                this.start = start;
                this.stop = stop;
            }
            else if (start == stop && start.X <= stop.X)
            {
                this.start = start;
                this.stop = stop;
            }
            else if (stop.Y < start.Y)
            {
                this.start = stop;
                this.stop = start;
            }
            this.initial = initial;
            this.changes = new byte[changes.Length];
            changes.CopyTo(this.changes, 0);
            //
            Execute();
        }
        public void Execute()
        {
            Point start = this.start;
            Point stop = this.stop;
            if (start.X > 1023) start.X = 1023;
            if (start.Y > 1023) start.Y = 1023;
            if (stop.X > 1023) stop.X = 1023;
            if (stop.X > 1023) stop.X = 1023;
            //
            bool[] made = new bool[changes.Length / 2];
            for (int y = start.Y, b = initial.Y; y < stop.Y; y++, b++)
            {
                for (int x = start.X, a = initial.X; x < stop.X; x++, a++)
                {
                    if (y * 1024 + x < solidity.PixelTiles.Length &&
                        b * 1024 + a < solidity.PixelTiles.Length)
                    {
                        int p = solidity.PixelTiles[y * 1024 + x] * 2;
                        int r = solidity.PixelTiles[b * 1024 + a] * 2;
                        if (!made[p / 2])
                        {
                            byte temp = tilemap.Tilemap_Bytes[p];
                            tilemap.Tilemap_Bytes[p] = changes[r];
                            changes[r] = temp;
                            //
                            temp = tilemap.Tilemap_Bytes[p + 1];
                            tilemap.Tilemap_Bytes[p + 1] = changes[r + 1];
                            changes[r + 1] = temp;
                            //
                            made[p / 2] = true;
                            tilemap.SetTileNum();
                        }
                    }
                }
            }
        }
    }
}
