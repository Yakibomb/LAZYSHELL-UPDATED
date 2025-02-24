using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    class MenuTileset
    {
        // class variables and accessors
        public PaletteSet paletteSet;
        private byte[] tileSet; public byte[] TileSet { get { return tileSet; } }
        private Tileset tileset; public Tileset Tileset { get { return tileset; } set { tileset = value; } }
        private byte[] graphicSet; public byte[] GraphicSet { get { return graphicSet; } set { graphicSet = value; } }
        //
        private Tile[] tilesets_tiles = new Tile[0];
        public Tile[] Tileset_tiles { get { return tilesets_tiles; } set { tilesets_tiles = value; } }
        //

        private byte[] tilesets_bytes = new byte[0];
        public byte[] Tileset_bytes { get { return tilesets_bytes; } set { tilesets_bytes = value; } }

        private TilesetType type; public TilesetType Type { get { return type; } }
        // constructor
        public MenuTileset(PaletteSet paletteSet, byte[] tileSet, byte[] graphicSet, TilesetType type)
        {
            this.Tileset_bytes = tileSet;
            this.paletteSet = paletteSet; // grab the current Palette Set
            // Create our layers for the tilesets (256x256)
            Tileset_tiles = new Tile[16 * 16];
            for (int i = 0; i < Tileset_tiles.Length; i++)
                Tileset_tiles[i] = new Tile(i);
            this.tileSet = tileSet;
            this.graphicSet = graphicSet;
            this.type = type;
            this.tileset = new Tileset(tileSet, graphicSet, paletteSet, 16, 16, type);
            DrawTileset(Tileset_tiles, tileSet);
        }
        // drawing functions
        public void DrawTileset(Tile[] tileSet, byte[] tilesetf)
        {
            byte status, tile, temp;
            Subtile source;
            int offset = 0;
            for (int i = 0; i < tileSet.Length; i++)
            {
                for (int z = 0; z < 2; z++)
                {
                    tile = tilesetf[offset++]; // GFX set?
                    status = tilesetf[offset++]; // Palette Set?
                    
                    if (Type == TilesetType.StarPiecesOverworldMenu)
                    {
                        // isolate palettes, then shift palettes indexes 6 and 7 to 0 and 1.
                        temp = (byte)(status & (0x07 << 2));
                        status -= temp;
                        status += (byte)(temp & (0x01 << 2));
                    }
                    source = Do.DrawSubtile(tile, status, graphicSet, paletteSet.Palettes, 0x20);
                    tileSet[i].Subtiles[z] = source;
                }
                offset += 60; // jump forward in buffer to grab correct 8x8 tiles
                for (int a = 2; a < 4; a++)
                {
                    tile = tilesetf[offset++];
                    status = tilesetf[offset++];
                    if (Type == TilesetType.StarPiecesOverworldMenu)
                    {
                        // isolate palettes, then shift palettes indexes 6 and 7 to 0 and 1.
                        temp = (byte)(status & (0x07 << 2));
                        status -= temp;
                        status += (byte)(temp & (0x01 << 2));
                    }
                    source = Do.DrawSubtile(tile, status, graphicSet, paletteSet.Palettes, 0x20);
                    tileSet[i].Subtiles[a] = source;
                }
                if ((i - 15) % 16 == 0)
                    offset += 64;
                offset -= 64; // jump back in buffer so that we can start the next 16x16 tile
            }
        }
        public void Assemble()
        {
            int width = 16;
            int offset = 0;
            for (int y = 0; y < tilesets_tiles.Length / width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = tilesets_tiles[y * width + x];
                    for (int s = 0; s < 4; s++)
                    {
                        offset = y * (width * 8) + (x * 4);
                        offset += (s % 2) * 2;
                        offset += (s / 2) * (width * 4);
                        Subtile subtile = tile.Subtiles[s];
                        if (subtile == null) continue;
                        //
                        switch (Type)
                        {
                            case TilesetType.GameSelectMenu:
                                subtile.Index = (subtile.Index & 0xFF) | 0x0200;
                                break;
                            case TilesetType.StarPiecesOverworldMenu:
                                subtile.Index = ((subtile.Index & 0xFF) | 0x0200) & 0x03FF;
                                subtile.Palette |= 6;
                                break;
                            case TilesetType.MenuBackground:
                                subtile.Index = ((subtile.Index & 0xFF) | 0x0100) & 0x03FF;
                                subtile.Palette = 5;
                                break;
                        }
                        //
                        Bits.SetShort(tilesets_bytes, offset, (ushort)subtile.Index);
                        tilesets_bytes[offset + 1] |= (byte)((subtile.Palette & 0x07) << 2);
                        Bits.SetBit(tilesets_bytes, offset + 1, 5, subtile.Priority1);
                        Bits.SetBit(tilesets_bytes, offset + 1, 6, subtile.Mirror);
                        Bits.SetBit(tilesets_bytes, offset + 1, 7, subtile.Invert);
                    }
                }
            }
            if (Type == TilesetType.GameSelectMenu)
            {
                Buffer.BlockCopy(tilesets_bytes, 0, Model.GameSelectTileset, 0, 0x800);
                Buffer.BlockCopy(graphicSet, 0, Model.GameSelectGraphics, 0, 0x2000);
            }
            else if (Type == TilesetType.StarPiecesOverworldMenu)
            {
                Buffer.BlockCopy(tilesets_bytes, 0, Model.OverworldStarPiecesMenuTileset, 0, 0x800);
                Buffer.BlockCopy(graphicSet, 0, Model.WorldMapLogos, 0, 0x2000);
            }
            else if (Type == TilesetType.MenuBackground)
            {
                Buffer.BlockCopy(tilesets_bytes, 0, Model.MenuBGTileset, 0, 0x800);
                Buffer.BlockCopy(graphicSet, 0, Model.MenuBGGraphics, 0, 0x2000);
            }
        }
        public void RedrawTileset()
        {
            DrawTileset(Tileset_tiles, tileSet);
        }
        // accessor functions
        public int GetTileNumber(int x, int y)
        {
            return Tileset_tiles[x + y * 16].Index;
        }
        // universal functions
        public void Clear(int count)
        {
            Model.EditMenuTileSet = true;
            RedrawTileset();
        }
    }
}
