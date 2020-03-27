using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable]
    public class LevelTemplate
    {
        [NonSerialized()]
        private Solidity solidity = Solidity.Instance;
        // class variables and accessors
        private string name; public string Name { get { return name; } set { name = value; } }
        private byte[] soliditymap = new byte[0x20C2]; public byte[] Soliditymap { get { return soliditymap; } }
        private byte[][] tilemaps = new byte[3][]; public byte[][] Tilemaps { get { return tilemaps; } }
        Point start; public Point Start { get { return start; } }
        Size size; public Size Size { get { return size; } }
        // so when painting the template it starts at the correct isometric coord
        public bool Even { get { return (size.Width / 16) % 2 == 0; } }
        // constructor
        // functions
        public void Transfer(byte[][] tilemaps, LevelMap levelMap, LevelSolidMap solidityMap, Point start, Point stop)
        {
            this.start = start;
            int offset = 0, o = 0, p = 0;
            size = new Size(stop.X - start.X, stop.Y - start.Y);
            this.tilemaps[0] = new byte[(size.Width * size.Height) / 128];
            this.tilemaps[1] = new byte[(size.Width * size.Height) / 128];
            this.tilemaps[2] = new byte[(size.Width * size.Height) / 256];
            for (int y = start.Y / 16, b = 0; y < stop.Y / 16; y++, b++)
            {
                for (int x = start.X / 16, a = 0; x < stop.X / 16; x++, a++, o++)
                {
                    offset = (x * 2) + (y * 128);
                    this.tilemaps[0][o * 2] = tilemaps[0][offset];
                    this.tilemaps[0][o * 2 + 1] = tilemaps[0][offset + 1];
                    this.tilemaps[1][o * 2] = tilemaps[1][offset];
                    this.tilemaps[1][o * 2 + 1] = tilemaps[1][offset + 1];
                    this.tilemaps[2][o] = tilemaps[2][y * 64 + x];
                }
            }
            for (int y = start.Y; y < stop.Y; y++)
            {
                for (int x = start.X; x < stop.X; x++)
                {
                    p = solidity.PixelTiles[y * 1024 + x] * 2;
                    soliditymap[p] = solidityMap.Tilemap_Bytes[p];
                    soliditymap[p + 1] = solidityMap.Tilemap_Bytes[p + 1];
                }
            }
        }
        // accessor functions
        public int[] GetPixels(Level level, Tileset tileset)
        {
            LevelTilemap tilemap = new LevelTilemap(level, tileset, this);
            int[] pixels = tilemap.Pixels;
            int[] temp = new int[size.Width * size.Height];
            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                    temp[y * size.Width + x] = pixels[y * 1024 + x];
            }
            return temp;
        }
    }
}
