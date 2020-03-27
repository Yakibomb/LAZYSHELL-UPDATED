using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class E_Mold
    {
        private byte[] buffer;
        private ushort moldOffset; public ushort MoldOffset { get { return moldOffset; } set { moldOffset = value; } }
        private byte[] mold; public byte[] Mold { get { return mold; } set { mold = value; } }
        public bool Empty
        {
            get
            {
                for (int i = 0; i < mold.Length; i += 2)
                    if (Bits.GetShort(mold, i) != 0xFF)
                        return false;
                return true;
            }
        }
        // disassemblers
        public void Disassemble(byte[] buffer, int offset, ushort end)
        {
            this.buffer = buffer;
            moldOffset = (ushort)offset;
            offset = Bits.GetShort(buffer, offset);
            mold = new byte[256];
            byte[] compressed = new byte[end - offset];
            Buffer.BlockCopy(buffer, offset, compressed, 0, end - offset);
            Decompress(compressed, mold);
        }
        // compression
        public void Decompress(byte[] src, byte[] dst)
        {
            int srcOffset = 0;
            int dstOffset = 0;
            while (srcOffset < src.Length)
            {
                if (src[srcOffset] == 0xFE) // if a filler tile
                {
                    if (srcOffset + 2 >= src.Length) break;
                    srcOffset++;    // skip the type
                    for (int counter = 0; counter < src[srcOffset + 1]; counter++, dstOffset++)
                        dst[dstOffset] = src[srcOffset];
                    srcOffset++;    // skip the fill amount
                    dstOffset--;    // move back one to keep from filling one extra byte
                }
                else
                    dst[dstOffset] = src[srcOffset];
                dstOffset++;
                srcOffset++;
            }
        }
        public byte[] Recompress(byte[] src, int width, int height)
        {
            byte[] dst = new byte[256]; // 256 max size for compressed
            int srcOffset = 0;
            int dstOffset = 0;
            while (srcOffset < width * height)
            {
                dst[dstOffset] = src[srcOffset];
                // make a filler tile only if next TWO tiles are the same
                if (srcOffset < 0xFE &&
                    srcOffset + 1 < src.Length &&
                    srcOffset + 2 < src.Length &&
                    src[srcOffset] == src[srcOffset + 1] &&
                    src[srcOffset] == src[srcOffset + 2])
                {
                    dst[dstOffset] = 0xFE;
                    dstOffset++;
                    dst[dstOffset] = src[srcOffset];    // the tile to fill with
                    byte counter = 1;
                    while (srcOffset < 0xFF && 
                        srcOffset < src.Length &&
                        srcOffset + 1 < src.Length &&
                        src[srcOffset] == src[srcOffset + 1])
                    {
                        counter++;
                        srcOffset++;
                    }
                    dstOffset++;
                    dst[dstOffset] = counter;   // the amount to fill
                }
                srcOffset++;
                dstOffset++;
            }
            byte[] temp = new byte[dstOffset];
            Bits.SetBytes(temp, 0, dst);
            return temp;
        }
        // drawing
        public int[] MoldPixels(E_Animation animation, E_Tileset tileset)
        {
            int[] pixels = new int[(animation.Width * 16) * (animation.Height * 16)];
            for (int y = 0; y < animation.Height; y++)
            {
                for (int x = 0; x < animation.Width; x++)
                {
                    if (y * animation.Width + x >= mold.Length)
                        continue;
                    if (mold[y * animation.Width + x] == 0xFF)
                        continue;
                    int[] tile = new int[16 * 16];
                    ((Tile)tileset.Tileset[mold[y * animation.Width + x] & 0x3F]).Pixels.CopyTo(tile, 0);
                    if ((mold[y * animation.Width + x] & 0x40) == 0x40)
                        Do.FlipHorizontal(tile, 16, 16);
                    if ((mold[y * animation.Width + x] & 0x80) == 0x80)
                        Do.FlipVertical(tile, 16, 16);
                    Do.PixelsToPixels(tile, pixels, animation.Width * 16, new Rectangle(x * 16, y * 16, 16, 16));
                }
            }
            return pixels;
        }
        // spawning
        public E_Mold New()
        {
            E_Mold empty = new E_Mold();
            empty.Mold = new byte[mold.Length];
            return empty;
        }
        public E_Mold Copy()
        {
            E_Mold copy = new E_Mold();
            copy.Mold = new byte[mold.Length];
            mold.CopyTo(copy.Mold, 0);
            copy.MoldOffset = moldOffset;
            return copy;
        }
    }
}
