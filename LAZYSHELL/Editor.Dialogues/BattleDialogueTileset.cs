using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class BattleDialogueTileset
    {
        // non-serialized variables
        [NonSerialized()]
        private PaletteSet palettes;
        // class variables and accessors
        private byte[] graphics; public byte[] GraphicSet { get { return graphics; } set { graphics = value; } }
        private byte[] tileset_bytes; public byte[] Tileset_bytes { get { return tileset_bytes; } set { tileset_bytes = value; } }
        private Tile[] tileset_tiles; public Tile[] Tileset_tiles { get { return tileset_tiles; } }
        // constructors
        public BattleDialogueTileset(PaletteSet paletteSet)
        {
            this.graphics = Model.DialogueGraphics;
            this.palettes = paletteSet;
            this.tileset_bytes = Model.BattleDialogueTileset_bytes;
            this.tileset_tiles = new Tile[16 * 2];
            for (int i = 0; i < tileset_tiles.Length; i++)
                tileset_tiles[i] = new Tile(i);
            DrawTileset(tileset_bytes, tileset_tiles);
        }
        public BattleDialogueTileset(byte[] graphics, byte[] tileset_bytes, PaletteSet paletteSet)
        {
            this.graphics = graphics;
            this.palettes = paletteSet;
            this.tileset_bytes = tileset_bytes;
            this.tileset_tiles = new Tile[16 * 2];
            for (int i = 0; i < tileset_tiles.Length; i++)
                tileset_tiles[i] = new Tile(i);
            DrawTileset(tileset_bytes, tileset_tiles);
        }
        // assemblers
        private void Assemble()
        {
        }
        // class functions
        public void RedrawTileset()
        {
            DrawTileset(tileset_bytes, tileset_tiles);
        }
        public void DrawTileset(byte[] src, Tile[] dst)
        {
            byte temp;
            ushort tile;
            Subtile source;
            int offset = 0;
            for (int i = 0; i < dst.Length; i++)
            {
                for (int z = 0; z < 2; z++)
                {
                    tile = (ushort)(Bits.GetShort(src, offset) & 0x03FF); offset++;
                    temp = src[offset++];
                    source = Do.DrawSubtile(tile, temp, graphics, palettes.Palettes, 0x20);
                    dst[i].Subtiles[z] = source;
                }
                offset += 60; // jump forward in buffer to grab correct 8x8 tiles
                for (int a = 2; a < 4; a++)
                {
                    tile = (ushort)(Bits.GetShort(src, offset) & 0x03FF); offset++;
                    temp = src[offset++];
                    source = Do.DrawSubtile(tile, temp, graphics, palettes.Palettes, 0x20);
                    dst[i].Subtiles[a] = source;
                }
                if ((i - 15) % 16 == 0)
                    offset += 64;
                offset -= 64; // jump back in buffer so that we can start the next 16x16 tile
            }
        }
        public void DrawTileset(Tile[] src, byte[] dst)
        {
            ushort tile;
            Subtile source;
            int offset = 0;
            for (int i = 0; i < src.Length; i++)
            {
                for (int z = 0; z < 2; z++)
                {
                    source = src[i].Subtiles[z];
                    tile = (ushort)source.Index;
                    Bits.SetShort(dst, offset, tile); offset++;
                    dst[offset] |= (byte)(source.Palette << 2);
                    Bits.SetBit(dst, offset, 5, source.Priority1);
                    Bits.SetBit(dst, offset, 6, source.Mirror);
                    Bits.SetBit(dst, offset, 7, source.Invert); offset++;
                }
                offset += 60; // jump forward in buffer to grab correct 8x8 tiles
                for (int a = 2; a < 4; a++)
                {
                    source = src[i].Subtiles[a];
                    tile = (ushort)source.Index;
                    Bits.SetShort(dst, offset, tile); offset++;
                    dst[offset] |= (byte)(source.Palette << 2);
                    Bits.SetBit(dst, offset, 5, source.Priority1);
                    Bits.SetBit(dst, offset, 6, source.Mirror);
                    Bits.SetBit(dst, offset, 7, source.Invert); offset++;
                }
                if ((i - 15) % 16 == 0)
                    offset += 64;
                offset -= 64; // jump back in buffer so that we can start the next 16x16 tile
            }
        }
    }
}
