using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    [Serializable()]
    public class E_Tileset
    {
        // variables
        private E_Animation animation;
        private int[] palette; public int[] Palette { get { return palette; } set { palette = value; } }
        private byte[] graphics; public byte[] Graphics { get { return graphics; } set { graphics = value; } }
        private Tile[] tileset; public Tile[] Tileset { get { return tileset; } set { tileset = value; } }
        // constructor
        public E_Tileset(E_Animation animation, int index)
        {
            this.animation = animation;
            this.graphics = animation.GraphicSet;
            this.palette = animation.PaletteSet.Palettes[index];
            tileset = new Tile[16 * 16];
            for (int i = 0; i < tileset.Length; i++)
                tileset[i] = new Tile(i);
            DrawTileset(tileset, animation.Tileset_bytes);
        }
        // class functions
        public void RedrawTileset(E_Animation animation, int length)
        {
            this.graphics = animation.GraphicSet;
            for (int i = 0; i < length / 8; i++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Subtile source;
                    if (animation.Codec == 1)
                        source = Do.DrawSubtile((byte)tileset[i].Subtiles[z].Index, 0, graphics, palette, 0x10);
                    else
                        source = Do.DrawSubtile((byte)tileset[i].Subtiles[z].Index, 0, graphics, palette, 0x20);
                    tileset[i].Subtiles[z] = source;
                }
            }
        }
        public void DrawTileset(Tile[] dst, byte[] src)
        {
            byte temp;
            ushort tile;
            Subtile source;
            int offset = 0;
            int i = 0;
            for (; i < dst.Length; i++)
            {
                if (i > 0 && i % 8 == 0) offset += 32;
                if (Bits.GetShort(src, offset) == 0xFFFF) break;
                else
                {
                    for (int z = 0; z < 2; z++)
                    {
                        if (offset >= src.Length - 1)
                            return;
                        tile = (ushort)(Bits.GetShort(src, offset++) & 0x03FF);
                        temp = src[offset++];
                        if (animation.Codec == 1)
                            source = Do.DrawSubtile(tile, temp, graphics, palette, 0x10);
                        else
                            source = Do.DrawSubtile(tile, temp, graphics, palette, 0x20);
                        dst[i].Subtiles[z] = source;
                    }
                    offset += 28; // jump forward in buffer to grab correct 8x8 tiles
                    for (int a = 2; a < 4; a++)
                    {
                        if (offset >= src.Length - 1)
                            return;
                        tile = (ushort)(Bits.GetShort(src, offset++) & 0x03FF);
                        temp = src[offset++];
                        if (animation.Codec == 1)
                            source = Do.DrawSubtile(tile, temp, graphics, palette, 0x10);
                        else
                            source = Do.DrawSubtile(tile, temp, graphics, palette, 0x20);
                        dst[i].Subtiles[a] = source;
                    }
                    offset -= 32; // jump back in buffer so that we can start the next 16x16 tile
                }
            }
            for (; i < 64; i++) // fill up the rest with empty tiles
            {
                for (int z = 0; z < 4; z++)
                {
                    if (animation.Codec == 1)
                        source = Do.DrawSubtile(0, 0, graphics, palette, 0x10);
                    else
                        source = Do.DrawSubtile(0, 0, graphics, palette, 0x20);
                    dst[i].Subtiles[z] = source;
                }
            }
        }
        public void DrawTileset(byte[] dst, Tile[] src)
        {
            ushort tile;
            Subtile source;
            int offset = 0;
            int i = 0;
            for (; i < src.Length; i++)
            {
                if (i > 0 && i % 8 == 0) offset += 32;
                for (int z = 0; z < 2; z++)
                {
                    source = src[i].Subtiles[z];
                    tile = (ushort)source.Index;
                    Bits.SetShort(dst, offset, tile); offset += 2;
                }
                offset += 28; // jump forward in buffer to grab correct 8x8 tiles
                for (int a = 2; a < 4; a++)
                {
                    source = src[i].Subtiles[a];
                    tile = (ushort)source.Index;
                    Bits.SetShort(dst, offset, tile); offset += 2;
                }
                offset -= 32; // jump back in buffer so that we can start the next 16x16 tile
            }
        }
    }
}
