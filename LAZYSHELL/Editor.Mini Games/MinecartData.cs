using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class MinecartData
    {
        // universal variables
        private byte[] buffer; public byte[] BUFFER { get { return buffer; } set { buffer = value; } }
        // public variables and accessors
        public MCObject[] M7ObjectsA = new MCObject[8];
        public MCObject[] M7ObjectsB = new MCObject[8];
        public List<int> L1Screens;
        public List<int> L2Screens;
        public List<int> RailScreens;
        public List<MCObject> SSObjectsA;
        public List<MCObject> SSObjectsB;
        public int WidthA;
        public int WidthB;
        [NonSerialized()]
        private Bitmap mushroom;
        public Bitmap Mushroom
        {
            get
            {
                if (mushroom == null)
                {
                    int[] pixels = Do.GetPixelRegion(Model.MinecartObjectGraphics, 0x20, Model.MinecartObjectPaletteSet.Palettes[1], 16, 0, 0, 2, 2, 0);
                    mushroom = Do.PixelsToImage(pixels, 16, 16);
                }
                return mushroom;
            }
            set { mushroom = value; }
        }
        [NonSerialized()]
        private Bitmap coin;
        public Bitmap Coin
        {
            get
            {
                if (coin == null)
                {
                    int[] pixels = Do.GetPixelRegion(Model.MinecartObjectGraphics, 0x20, Model.MinecartObjectPaletteSet.Palettes[0], 16, 2, 0, 2, 2, 0);
                    coin = Do.PixelsToImage(pixels, 16, 16);
                }
                return coin;
            }
            set { coin = value; }
        }
        // constructor
        public MinecartData(byte[] buffer)
        {
            this.buffer = buffer;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            // mode7 objects
            int offset = Bits.GetShort(buffer, 0);
            for (int i = 0; i < 8; i++)
                M7ObjectsA[i] = new MCObject(buffer[offset++], buffer[offset++]);
            for (int i = 0; i < 8; i++)
                M7ObjectsB[i] = new MCObject(buffer[offset++], buffer[offset++]);
            // side-scrolling objects A
            SSObjectsA = new List<MCObject>();
            offset = Bits.GetShort(buffer, 2);
            WidthA = Bits.GetShort(buffer, offset); offset += 2;
            while (Bits.GetShort(buffer, offset) != 0xFFFF)
            {
                int x = Bits.GetShort(buffer, offset) + 256; offset += 2;
                int type = buffer[offset++];
                int size = buffer[offset++];
                int y = buffer[offset++];
                SSObjectsA.Add(new MCObject(type, x, y, size));
            }
            // side-scrolling objects B
            SSObjectsB = new List<MCObject>();
            offset = Bits.GetShort(buffer, 4);
            WidthB = Bits.GetShort(buffer, offset); offset += 2;
            while (Bits.GetShort(buffer, offset) != 0xFFFF)
            {
                int x = Bits.GetShort(buffer, offset) + 256; offset += 2;
                int type = buffer[offset++];
                int size = buffer[offset++];
                int y = buffer[offset++];
                SSObjectsB.Add(new MCObject(type, x, y, size));
            }
            // side-scrolling L1 screens
            L1Screens = new List<int>();
            offset = Bits.GetShort(buffer, 6);
            while (offset != Bits.GetShort(buffer, 8))
                L1Screens.Add(buffer[offset++]);
            // side-scrolling L2 screens
            L2Screens = new List<int>();
            offset = Bits.GetShort(buffer, 8);
            while (offset != Bits.GetShort(buffer, 10))
                L2Screens.Add(buffer[offset++]);
            // side-scrolling rail screens (2nd map)
            RailScreens = new List<int>();
            offset = Bits.GetShort(buffer, 10);
            while (offset < buffer.Length)
                RailScreens.Add(buffer[offset++]);
        }
        public void Assemble()
        {
            byte[] data = new byte[0x10000];
            int offset = 0xC;
            // mode7 objects
            Bits.SetShort(data, 0, offset);
            for (int i = 0; i < 8; i++)
            {
                data[offset++] = (byte)M7ObjectsA[i].X;
                data[offset++] = (byte)M7ObjectsA[i].Y;
            }
            for (int i = 0; i < 8; i++)
            {
                data[offset++] = (byte)M7ObjectsB[i].X;
                data[offset++] = (byte)M7ObjectsB[i].Y;
            }
            // side-scrolling objects A
            Bits.SetShort(data, 2, offset);
            Bits.SetShort(data, offset, WidthA); offset += 2;
            foreach (MCObject ssobject in SSObjectsA)
            {
                Bits.SetShort(data, offset, ssobject.X - 256); offset += 2;
                data[offset++] = (byte)ssobject.Type;
                data[offset++] = (byte)ssobject.Count;
                data[offset++] = (byte)ssobject.Y;
            }
            Bits.SetShort(data, offset, 0xFFFF); offset += 2;
            // side-scrolling objects B
            Bits.SetShort(data, 4, offset);
            Bits.SetShort(data, offset, WidthB); offset += 2;
            foreach (MCObject ssobject in SSObjectsB)
            {
                Bits.SetShort(data, offset, ssobject.X - 256); offset += 2;
                data[offset++] = (byte)ssobject.Type;
                data[offset++] = (byte)ssobject.Count;
                data[offset++] = (byte)ssobject.Y;
            }
            Bits.SetShort(data, offset, 0xFFFF); offset += 2;
            // side-scrolling L1 screens
            Bits.SetShort(data, 6, offset);
            foreach (int screen in L1Screens)
                data[offset++] = (byte)screen;
            // side-scrolling L2 screens
            Bits.SetShort(data, 8, offset);
            foreach (int screen in L2Screens)
                data[offset++] = (byte)screen;
            // side-scrolling rail screens (2nd map)
            Bits.SetShort(data, 10, offset);
            foreach (int screen in RailScreens)
                data[offset++] = (byte)screen;
            //
            Model.MinecartObjects = new byte[offset];
            Buffer.BlockCopy(data, 0, Model.MinecartObjects, 0, Model.MinecartObjects.Length);
        }
    }
    [Serializable()]
    public class MCObject
    {
        // class variables and accessors
        public Point Location;
        public int Type;
        public int X;
        public int Y;
        public int Count;
        public int Width { get { return Count * 32 - 16; } }
        // constructors
        public MCObject(int x, int y)
        {
            X = x;
            Y = y;
        }
        public MCObject(int type, int x, int y, int count)
        {
            Type = type;
            X = x;
            Y = y;
            Count = count;
        }
        // spawning
        public MCObject Copy()
        {
            MCObject copy = new MCObject(Type, X, Y, Count);
            copy.Location = Location;
            return copy;
        }
    }
}
