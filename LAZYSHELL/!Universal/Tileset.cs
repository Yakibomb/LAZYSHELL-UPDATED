using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using static LAZYSHELL.Mold;
using System.Diagnostics.Eventing.Reader;

namespace LAZYSHELL
{
    [Serializable()]
    public class Tileset
    {
        // class variables
        private LevelMap levelMap;
        private int tilesize;
        private byte[][] tilesets_bytes = new byte[3][];
        private byte[] graphics;
        private byte[] graphicsL3;
        private Tile[][] tilesets_tiles = new Tile[3][];
        // public variables
        public int Width;
        public int Height;
        public int HeightL3;
        public TilesetType Type;
        public PaletteSet paletteSet;
        // accessors
        public byte[] Tileset_bytes { get { return tilesets_bytes[0]; } set { tilesets_bytes[0] = value; } }
        public byte[][] Tilesets_bytes { get { return tilesets_bytes; } set { tilesets_bytes = value; } }
        public byte[] Graphics { get { return graphics; } set { graphics = value; } }
        public byte[] GraphicsL3 { get { return graphicsL3; } set { graphicsL3 = value; } }
        public Tile[] Tileset_tiles { get { return tilesets_tiles[0]; } set { tilesets_tiles[0] = value; } }
        public Tile[][] Tilesets_tiles { get { return tilesets_tiles; } set { tilesets_tiles = value; } }
        // mode7 variables and accessors
        private byte[] subtile_bytes;
        private byte[] palette_bytes;
        public byte[] Subtile_Bytes { get { return subtile_bytes; } set { subtile_bytes = value; } }
        public byte[] Palette_Bytes { get { return palette_bytes; } set { palette_bytes = value; } }
        
        // constructors
        // default tileset
        public Tileset(byte[] tileset_Bytes, byte[] graphics, PaletteSet paletteSet, int width, int height, TilesetType type)
        {
            this.paletteSet = paletteSet;
            this.Tileset_bytes = tileset_Bytes;
            this.Width = width;
            this.Height = height;
            this.tilesize = 2;
            this.Type = type;
            //
            this.graphics = graphics;
            //
            Tileset_tiles = new Tile[Width * Height];
            for (int i = 0; i < Tileset_tiles.Length; i++)
                Tileset_tiles[i] = new Tile(i);
            byte format = (byte)(type != TilesetType.Opening ? 0x20 : 0x10);
            DrawTileset(Tileset_bytes, Tileset_tiles, graphics, format);
            this.tilesets_bytes[0] = this.Tileset_bytes;
            this.tilesets_tiles[0] = this.Tileset_tiles;
        }
        // level tileset
        public Tileset(LevelMap levelMap, PaletteSet paletteSet)
        {
            this.levelMap = levelMap; // grab the current LevelMap
            this.paletteSet = paletteSet; // grab the current Palette Set
            this.Width = 16;
            this.Height = 32;
            this.HeightL3 = 16;
            this.tilesize = 2;
            this.Type = TilesetType.Level;
            // set tileset byte arrays
            tilesets_bytes[0] = Model.Tilesets[levelMap.TilesetL1 + 0x20];
            tilesets_bytes[1] = Model.Tilesets[levelMap.TilesetL2 + 0x20];
            tilesets_bytes[2] = Model.Tilesets[levelMap.TilesetL3];
            // combine graphic sets into one array
            graphics = new byte[0x6000];
            Buffer.BlockCopy(Model.GraphicSets[levelMap.GraphicSetA + 0x48], 0, graphics, 0, 0x2000);
            Buffer.BlockCopy(Model.GraphicSets[levelMap.GraphicSetB + 0x48], 0, graphics, 0x2000, 0x1000);
            Buffer.BlockCopy(Model.GraphicSets[levelMap.GraphicSetC + 0x48], 0, graphics, 0x3000, 0x1000);
            Buffer.BlockCopy(Model.GraphicSets[levelMap.GraphicSetD + 0x48], 0, graphics, 0x4000, 0x1000);
            Buffer.BlockCopy(Model.GraphicSets[levelMap.GraphicSetE + 0x48], 0, graphics, 0x5000, 0x1000);
            if (levelMap.GraphicSetL3 != 0xFF)
                graphicsL3 = Model.GraphicSets[levelMap.GraphicSetL3];
            // initialize 16x16 tile arrays
            tilesets_tiles[0] = new Tile[Width * Height];
            tilesets_tiles[1] = new Tile[Width * Height];
            if (levelMap.GraphicSetL3 != 0xFF)
                tilesets_tiles[2] = new Tile[Width * HeightL3];
            for (int l = 0; l < 3; l++)
            {
                if (tilesets_tiles[l] == null)
                    continue;
                for (int i = 0; i < tilesets_tiles[l].Length; i++)
                    tilesets_tiles[l][i] = new Tile(i);
            }
            // draw all 16x16 tiles
            DrawTileset(tilesets_bytes[0], tilesets_tiles[0], graphics, 0x20);
            DrawTileset(tilesets_bytes[1], tilesets_tiles[1], graphics, 0x20);
            if (levelMap.GraphicSetL3 != 0xFF)
                DrawTileset(tilesets_bytes[2], tilesets_tiles[2], graphicsL3, 0x10);
        }
        // main title tileset
        public Tileset(PaletteSet paletteSet, string type)
        {
            this.paletteSet = paletteSet;
            this.Width = 16;
            this.Height = 32;
            this.HeightL3 = 6;
            this.tilesize = 2;
            this.Type = TilesetType.Title;
            // Decompress data at offsets
            tilesets_bytes[0] = Bits.GetBytes(Model.TitleData, 0x0000, 0x1000);
            tilesets_bytes[1] = Bits.GetBytes(Model.TitleData, 0x1000, 0x1000);
            tilesets_bytes[2] = Bits.GetBytes(Model.TitleData, 0xBBE0, 0x300);
            // Create buffer the size of the combined graphicSets
            graphics = Bits.GetBytes(Model.TitleData, 0x6C00, 0x4FE0);
            graphicsL3 = Bits.GetBytes(Model.TitleData, 0xBEA0, 0x1BC0);
            //
            tilesets_tiles[0] = new Tile[16 * 32];
            tilesets_tiles[1] = new Tile[16 * 32];
            tilesets_tiles[2] = new Tile[16 * 6];
            for (int i = 0; i < tilesets_tiles[0].Length; i++)
                tilesets_tiles[0][i] = new Tile(i);
            for (int i = 0; i < tilesets_tiles[1].Length; i++)
                tilesets_tiles[1][i] = new Tile(i);
            for (int i = 0; i < tilesets_tiles[2].Length; i++)
                tilesets_tiles[2][i] = new Tile(i);
            DrawTileset(tilesets_bytes[0], tilesets_tiles[0], graphics, 0x20);
            DrawTileset(tilesets_bytes[1], tilesets_tiles[1], graphics, 0x20);
            DrawTileset(tilesets_bytes[2], tilesets_tiles[2], graphicsL3, 0x20);
        }
        // minecart M7 tileset
        public Tileset(PaletteSet paletteSet)
        {
            this.paletteSet = paletteSet; // grab the current Palette Set
            this.Width = 16;
            this.Height = 16;
            this.tilesize = 1;
            this.Type = TilesetType.Mode7;
            //
            subtile_bytes = Model.MinecartM7TilesetSubtiles;
            palette_bytes = Model.MinecartM7TilesetPalettes;
            graphics = Model.MinecartM7Graphics;
            // Create our layers for the tilesets (256x512)
            Tileset_tiles = new Tile[Width * Height];
            for (int i = 0; i < Tileset_tiles.Length; i++)
                Tileset_tiles[i] = new Tile(i);
            DrawTileset(subtile_bytes, Tileset_tiles, graphics, 0x20);
        }

        // assemblers
        public void Assemble(int width, int layer)
        {
            if (tilesets_tiles[layer] == null)
                return;
            //
            int offset = 0;
            if (Type == TilesetType.Level || Type == TilesetType.Title)
            {
                for (int y = 0; y < tilesets_tiles[layer].Length / width; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int index = y * width + x;
                        if (index >= tilesets_tiles[layer].Length)
                            continue;
                        Tile tile = tilesets_tiles[layer][index];
                        for (int s = 0; s < 4; s++)
                        {
                            offset = y * (width * 8) + (x * 4);
                            offset += (s % 2) * 2;
                            offset += (s / 2) * (width * 4);
                            Subtile subtile = tile.Subtiles[s];
                            if (subtile == null) continue;
                            Bits.SetShort(tilesets_bytes[layer], offset, (ushort)subtile.Index);
                            tilesets_bytes[layer][offset + 1] |= (byte)(subtile.Palette << 2);
                            Bits.SetBit(tilesets_bytes[layer], offset + 1, 5, subtile.Priority1);
                            Bits.SetBit(tilesets_bytes[layer], offset + 1, 6, subtile.Mirror);
                            Bits.SetBit(tilesets_bytes[layer], offset + 1, 7, subtile.Invert);
                        }
                    }
                }
                if (Type == TilesetType.Level)
                {
                    if (layer == 0)
                        Model.EditTilesets[levelMap.TilesetL1 + 0x20] = true;
                    if (layer == 1)
                        Model.EditTilesets[levelMap.TilesetL2 + 0x20] = true;
                    if (layer == 2)
                        Model.EditTilesets[levelMap.TilesetL3] = true;
                    //
                    Buffer.BlockCopy(graphics, 0, Model.GraphicSets[levelMap.GraphicSetA + 0x48], 0, 0x2000);
                    Buffer.BlockCopy(graphics, 0x2000, Model.GraphicSets[levelMap.GraphicSetB + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x3000, Model.GraphicSets[levelMap.GraphicSetC + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x4000, Model.GraphicSets[levelMap.GraphicSetD + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x5000, Model.GraphicSets[levelMap.GraphicSetE + 0x48], 0, 0x1000);
                    //
                    Model.EditGraphicSets[levelMap.GraphicSetA + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetB + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetC + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetD + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetE + 0x48] = true;
                }
                else if (Type == TilesetType.Title)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.TitleData, 0, 0x1000);
                    Buffer.BlockCopy(tilesets_bytes[1], 0, Model.TitleData, 0x1000, 0x1000);
                    Buffer.BlockCopy(tilesets_bytes[2], 0, Model.TitleData, 0xBBE0, 0x300);
                    Buffer.BlockCopy(graphics, 0, Model.TitleData, 0x6C00, 0x4FE0);
                    Buffer.BlockCopy(graphicsL3, 0x40, Model.TitleData, 0xBEE0, 0x1B80);
                }
            }
            else
                Assemble(width);
        }
        public void Assemble(int width)
        {
            int offset = 0;
            if (Type != TilesetType.Mode7)
            {
                for (int l = 0; l < tilesets_tiles.Length; l++)
                {
                    if (tilesets_tiles[l] == null) continue;
                    for (int y = 0; y < tilesets_tiles[l].Length / width; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Tile tile = tilesets_tiles[l][y * width + x];
                            for (int s = 0; s < 4; s++)
                            {
                                offset = y * (width * 8) + (x * 4);
                                offset += (s % 2) * 2;
                                offset += (s / 2) * (width * 4);
                                Subtile subtile = tile.Subtiles[s];
                                if (subtile == null) continue;
                                Bits.SetShort(tilesets_bytes[l], offset, (ushort)subtile.Index);
                                tilesets_bytes[l][offset + 1] |= (byte)((subtile.Palette & 0x07) << 2);
                                Bits.SetBit(tilesets_bytes[l], offset + 1, 5, subtile.Priority1);
                                Bits.SetBit(tilesets_bytes[l], offset + 1, 6, subtile.Mirror);
                                Bits.SetBit(tilesets_bytes[l], offset + 1, 7, subtile.Invert);
                            }
                        }
                    }
                }
                if (Type == TilesetType.Level)
                {
                    Model.EditTilesets[levelMap.TilesetL1 + 0x20] = true;
                    Model.EditTilesets[levelMap.TilesetL2 + 0x20] = true;
                    Model.EditTilesets[levelMap.TilesetL3] = true;
                    //
                    Buffer.BlockCopy(graphics, 0, Model.GraphicSets[levelMap.GraphicSetA + 0x48], 0, 0x2000);
                    Buffer.BlockCopy(graphics, 0x2000, Model.GraphicSets[levelMap.GraphicSetB + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x3000, Model.GraphicSets[levelMap.GraphicSetC + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x4000, Model.GraphicSets[levelMap.GraphicSetD + 0x48], 0, 0x1000);
                    Buffer.BlockCopy(graphics, 0x5000, Model.GraphicSets[levelMap.GraphicSetE + 0x48], 0, 0x1000);
                    //
                    Model.EditGraphicSets[levelMap.GraphicSetA + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetB + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetC + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetD + 0x48] = true;
                    Model.EditGraphicSets[levelMap.GraphicSetE + 0x48] = true;
                }
                else if (Type == TilesetType.SideScrolling)
                {
                    Buffer.BlockCopy(graphics, 0, Model.MinecartSSGraphics, 0, graphics.Length);
                }
                else if (Type == TilesetType.Title)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.TitleData, 0, 0x1000);
                    Buffer.BlockCopy(tilesets_bytes[1], 0, Model.TitleData, 0x1000, 0x1000);
                    Buffer.BlockCopy(tilesets_bytes[2], 0, Model.TitleData, 0xBBE0, 0x300);
                    Buffer.BlockCopy(graphics, 0, Model.TitleData, 0x6C00, 0x4FE0);
                    Buffer.BlockCopy(graphicsL3, 0x40, Model.TitleData, 0xBEE0, 0x1B80);
                }
                else if (Type == TilesetType.Opening)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.OpeningData, 0, 0x480);
                    Buffer.BlockCopy(graphics, 0, Model.OpeningData, 0x480, 0x1340);
                }
                else if (Type == TilesetType.GameSelectMenu)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.GameSelectTileset, 0, 0x800);
                    Buffer.BlockCopy(graphics, 0, Model.GameSelectGraphics, 0, 0x2000);
                }
                else if (Type == TilesetType.StarPiecesOverworldMenu)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.OverworldStarPiecesMenuTileset, 0, 0x800);
                    Buffer.BlockCopy(graphics, 0, Model.WorldMapLogos, 0, 0x2000);
                }
                else if (Type == TilesetType.MenuBackground)
                {
                    Buffer.BlockCopy(tilesets_bytes[0], 0, Model.MenuBGTileset, 0, 0x800);
                    Buffer.BlockCopy(graphics, 0, Model.MenuBGGraphics, 0, 0x2000);
                }
            }
            else
            {
                if (Tileset_tiles == null)
                    return;
                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int i = y * 16 + x;
                        for (int z = 0; z < 4; z++)
                        {
                            offset = y * (width * 4) + (x * 2);
                            offset += z % 2;
                            offset += (z / 2) * (width * 2);
                            Subtile subtile = Tileset_tiles[i].Subtiles[z];
                            byte tilenum = (byte)subtile.Index;
                            subtile_bytes[offset] = tilenum;
                            palette_bytes[tilenum] = (byte)subtile.Palette;
                        }
                    }
                }
                Buffer.BlockCopy(graphics, 0, Model.MinecartM7Graphics, 0, graphics.Length);
            }
        }
        // class functions
        public void DrawTileset(byte[] src, Tile[] dst, byte[] graphics, byte format)
        {
            byte status = 0;
            ushort tilenum = 0;
            Subtile subtile;
            int offset = 0;
            for (int i = 0; i < Width / 16; i++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = i * 16; x < i * 16 + 16; x++)
                    {
                        int index = y * Width + x;
                        if (index >= dst.Length)
                            continue;
                        for (int z = 0; z < 4; z++)
                        {
                            if (z == 2)
                                offset += tilesize * 30;
                            if (tilesize == 2)
                            {
                                tilenum = (ushort)(Bits.GetShort(src, offset++) & 0x03FF);
                                status = src[offset++];
                            }
                            else
                            {
                                tilenum = src[offset++];
                                status = (byte)(palette_bytes[tilenum] << 2);
                            }
                            subtile = Do.DrawSubtile(tilenum, status, graphics, paletteSet.Palettes, format);
                            dst[index].Subtiles[z] = subtile;
                        }
                        if (x < i * 16 + 15)
                            offset -= tilesize * 32;
                    }
                }
            }
        }
        public void DrawTileset(Tile[] src, byte[] dst)
        {
            ushort tilenum = 0;
            Subtile subtile;
            int offset = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int index = y * Width + x;
                    if (index >= src.Length)
                        continue;
                    for (int z = 0; z < 4; z++)
                    {
                        if (z == 2)
                            offset += tilesize * 30;
                        subtile = src[index].Subtiles[z];
                        if (tilesize == 2)
                        {
                            tilenum = (ushort)subtile.Index;
                            Bits.SetShort(dst, offset, tilenum); offset++;
                            dst[offset] |= (byte)(subtile.Palette << 2);
                            Bits.SetBit(dst, offset, 5, subtile.Priority1);
                            Bits.SetBit(dst, offset, 6, subtile.Mirror);
                            Bits.SetBit(dst, offset, 7, subtile.Invert); offset++;
                        }
                        else
                        {
                            tilenum = (byte)subtile.Index;
                            subtile_bytes[offset++] = (byte)tilenum;
                            palette_bytes[tilenum] = (byte)subtile.Palette;
                        }
                    }
                    if (x < 15)
                        offset -= tilesize * 32;
                }
            }
        }
        public void RedrawTilesets()
        {
            for (int l = 0; l < 3; l++)
            {
                if (tilesets_tiles[l] != null)
                {
                    byte format = (byte)(l != 2 || Type == TilesetType.Title ? 0x20 : 0x10);
                    byte[] graphics = l != 2 ? this.graphics : this.graphicsL3;
                    //
                    if (tilesize == 2)
                        DrawTileset(tilesets_bytes[l], tilesets_tiles[l], graphics, format);
                    else
                        DrawTileset(subtile_bytes, tilesets_tiles[l], graphics, format);
                }
            }
        }
        public void RedrawTilesets(int layer)
        {
            byte format = (byte)(layer != 2 || Type == TilesetType.Title ? 0x20 : 0x10);
            byte[] graphics = layer != 2 ? this.graphics : this.graphicsL3;
            //
            if (tilesets_tiles[layer] != null)
                DrawTileset(tilesets_bytes[layer], tilesets_tiles[layer], graphics, format);
        }
        // accessor functions
        public int GetTileNum(int layer, int x, int y)
        {
            if (layer < 3)
                return tilesets_tiles[layer][y * Width + x].Index;
            else return 0;
        }
        // universal functions
        public void Clear(int count)
        {
            if (Type == TilesetType.Level)
            {
                if (count == 1)
                {
                    Model.Tilesets[levelMap.TilesetL1 + 0x20] = new byte[0x2000];
                    Model.Tilesets[levelMap.TilesetL2 + 0x20] = new byte[0x2000];
                    Model.Tilesets[levelMap.TilesetL3] = new byte[0x1000];
                    Model.EditTilesets[levelMap.TilesetL1 + 0x20] = true;
                    Model.EditTilesets[levelMap.TilesetL2 + 0x20] = true;
                    Model.EditTilesets[levelMap.TilesetL3] = true;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (i < 0x20)
                            Model.Tilesets[i] = new byte[0x1000];
                        else
                            Model.Tilesets[i] = new byte[0x2000];
                        Model.EditTilesets[i] = true;
                    }
                }
            }
            else if (Type == TilesetType.Mode7)
            {
                // Minecart tileset
                for (int i = 0; i < 0x400; i++)
                    subtile_bytes[i] = Model.MinecartM7TilesetSubtiles[i] = 0;
                for (int i = 0; i < 0x100; i++)
                    palette_bytes[i] = Model.MinecartM7TilesetPalettes[i] = 0;
            }
            //
            RedrawTilesets();
        }
    }
}
