using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    public class LevelSolidMap : Tilemap
    {
        // universal variables
        private Solidity solidity = Solidity.Instance;
        // class variables
        private LevelMap levelMap;
        private byte[] tilemap_Bytes;
        private int[] pixels;
        private Bitmap image;
        // public accessors
        public LevelMap Levelmap { get { return levelMap; } }
        public override byte[] Tilemap_Bytes { get { return tilemap_Bytes; } set { tilemap_Bytes = value; } }
        public override byte[][] Tilemaps_Bytes { get { return null; } set { } }
        public override Tile[] Tilemap_Tiles
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override Tile[][] Tilemaps_Tiles
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override int Width_p
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override int Height_p
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override int[] Pixels
        {
            get
            {
                return pixels;
            }
            set { pixels = value; }
        }
        public override Bitmap Image
        {
            get
            {
                if (image == null)
                {
                    pixels = solidity.GetTilemapPixels(this);
                    image = Do.PixelsToImage(pixels, 1024, 1024);
                }
                return image;
            }
            set { image = value; }
        }
        // constructor
        public LevelSolidMap(LevelMap levelMap)
        {
            this.levelMap = levelMap;
            tilemap_Bytes = Model.SolidityMaps[levelMap.SolidityMap];
        }
        // assmeblers
        public override void Assemble()
        {
            throw new NotImplementedException();
        }
        // accessor functions
        public override int GetTileNum(int index)
        {
            return Bits.GetShort(tilemap_Bytes, index * 2);
        }
        public override int GetTileNum(int layer, int x, int y)
        {
            throw new NotImplementedException();
        }
        public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
        {
            throw new NotImplementedException();
        }
        public override void SetTileNum()
        {
            Model.EditSolidityMaps[levelMap.SolidityMap] = true;
        }
        public override void SetTileNum(int tilenum, int layer, int x, int y)
        {
            throw new NotImplementedException();
        }
        public override int[] GetPixels(int layer, Point location, Size size)
        {
            throw new NotImplementedException();
        }
        public override int[] GetPixels(Point location, Size size)
        {
            throw new NotImplementedException();
        }
        public override int[] GetPriority1Pixels()
        {
            throw new NotImplementedException();
        }
        public override int GetPixelLayer(int x, int y)
        {
            return 0;
        }
        // class functions
        public override void RedrawTilemaps()
        {
            throw new NotImplementedException();
        }
        // universal functions
        public void Clear(int count)
        {
            if (count == 1)
            {
                Model.SolidityMaps[levelMap.SolidityMap] = tilemap_Bytes = new byte[0x20C2];
                Model.EditSolidityMaps[levelMap.SolidityMap] = true;
            }
            else
            {
                tilemap_Bytes = new byte[0x20C2];
                for (int i = 0; i < count; i++)
                {
                    Model.SolidityMaps[i] = new byte[0x20C2];
                    Model.EditSolidityMaps[i] = true;
                }
            }
        }
    }
}
