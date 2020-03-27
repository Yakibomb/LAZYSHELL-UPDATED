using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    class BattlefieldTileset
    {
        // non-serialized variables
        private Battlefield battlefield;
        // class variables
        private byte[] graphics; 
        private byte[] tileset_bytes;
        private Tile[] tileset_tiles;
        private PaletteSet palettes;
        // public accessors
        public Battlefield Battlefield { get { return battlefield; } set { battlefield = value; } }
        public byte[] Graphics { get { return graphics; } set { graphics = value; } }
        public byte[] Tileset_bytes { get { return tileset_bytes; } set { tileset_bytes = value; } }
        public Tile[] Tileset_tiles { get { return tileset_tiles; } set { tileset_tiles = value; } }
        public PaletteSet Palettes { get { return palettes; } set { palettes = value; } }
        // constructors
        public BattlefieldTileset(Battlefield battlefield, PaletteSet palettes)
        {
            this.battlefield = battlefield;
            this.palettes = palettes;
            // compile graphics
            graphics = new byte[0x6000];
            if (battlefield.GraphicSetA < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetA + 0x48], 0, graphics, 0, 0x2000);
            if (battlefield.GraphicSetB < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetB + 0x48], 0, graphics, 0x2000, 0x1000);
            if (battlefield.GraphicSetC < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetC + 0x48], 0, graphics, 0x3000, 0x1000);
            if (battlefield.GraphicSetD < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetD + 0x48], 0, graphics, 0x4000, 0x1000);
            if (battlefield.GraphicSetE < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetE + 0x48], 0, graphics, 0x5000, 0x1000);
            // create tileset
            tileset_bytes = Model.TilesetsBF[battlefield.TileSet];
            tileset_tiles = new Tile[32 * 32];
            for (int i = 0; i < tileset_tiles.Length; i++)
                tileset_tiles[i] = new Tile(i);
            DrawTileset(tileset_bytes, tileset_tiles);
        }
        public BattlefieldTileset(Battlefield battlefield, PaletteSet palettes, Tile[] tileset_tiles)
        {
            this.battlefield = battlefield;
            this.palettes = palettes; 
            this.tileset_bytes = new byte[0x2000];
            this.tileset_tiles = tileset_tiles;
            graphics = new byte[0x6000];
            if (battlefield.GraphicSetA < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetA + 0x48], 0, graphics, 0, 0x2000);
            if (battlefield.GraphicSetB < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetB + 0x48], 0, graphics, 0x2000, 0x1000);
            if (battlefield.GraphicSetC < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetC + 0x48], 0, graphics, 0x3000, 0x1000);
            if (battlefield.GraphicSetD < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetD + 0x48], 0, graphics, 0x4000, 0x1000);
            if (battlefield.GraphicSetE < 0xC8)
                Buffer.BlockCopy(Model.GraphicSets[battlefield.GraphicSetE + 0x48], 0, graphics, 0x5000, 0x1000);
            DrawTileset(tileset_tiles, tileset_bytes);
        }
        public BattlefieldTileset()
        {
        }
        // assemblers
        public void Assemble(int width, int height)
        {
            int offset = 0;
            for (int q = 0; q < 4; q++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Tile tile = tileset_tiles[(y * width + x) + (q * 256)];
                        if (tile == null) continue;
                        for (int s = 0; s < 4; s++)
                        {
                            offset = y * (width * 2 * 2 * 2) + (x * 2 * 2);
                            offset += (s % 2) * 2;
                            offset += (s / 2) * (width * 2 * 2);
                            offset += (q * 256) * 8;
                            Subtile subtile = tile.Subtiles[s];
                            if (subtile == null) continue;
                            Bits.SetShort(tileset_bytes, offset, (ushort)subtile.Index);
                            tileset_bytes[offset + 1] |= (byte)(subtile.Palette << 2);
                            Bits.SetBit(tileset_bytes, offset + 1, 5, subtile.Priority1);
                            Bits.SetBit(tileset_bytes, offset + 1, 6, subtile.Mirror);
                            Bits.SetBit(tileset_bytes, offset + 1, 7, subtile.Invert);
                        }
                    }
                }
            }
            Model.EditTilesetsBF[battlefield.TileSet] = true;
            if (battlefield.GraphicSetA < 0xC8)
                Buffer.BlockCopy(graphics, 0, Model.GraphicSets[battlefield.GraphicSetA + 0x48], 0, 0x2000);
            if (battlefield.GraphicSetB < 0xC8)
                Buffer.BlockCopy(graphics, 0x2000, Model.GraphicSets[battlefield.GraphicSetB + 0x48], 0, 0x1000);
            if (battlefield.GraphicSetC < 0xC8)
                Buffer.BlockCopy(graphics, 0x3000, Model.GraphicSets[battlefield.GraphicSetC + 0x48], 0, 0x1000);
            if (battlefield.GraphicSetD < 0xC8)
                Buffer.BlockCopy(graphics, 0x4000, Model.GraphicSets[battlefield.GraphicSetD + 0x48], 0, 0x1000);
            if (battlefield.GraphicSetE < 0xC8)
                Buffer.BlockCopy(graphics, 0x5000, Model.GraphicSets[battlefield.GraphicSetE + 0x48], 0, 0x1000);
        }
        // class functions
        public void DrawTileset(byte[] src, Tile[] dst)
        {
            byte temp;
            ushort tile;
            Subtile source;
            int offset = 0;
            //
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
                    dst[i].Subtiles[a] = source;;
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
        public void RedrawTileset()
        {
            DrawTileset(tileset_bytes, tileset_tiles);
        }
    }
}
