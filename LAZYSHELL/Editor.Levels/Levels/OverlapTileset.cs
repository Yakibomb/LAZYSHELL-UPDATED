using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    public class OverlapTileset
    {
        // class variables and accessors
        private OverlapTile[] overlapTiles; public OverlapTile[] OverlapTiles { get { return overlapTiles; } }
        public byte[] graphicSet;
        // constructor
        public OverlapTileset()
        {
            overlapTiles = new OverlapTile[104];
            for (int i = 0; i < overlapTiles.Length; i++)
                overlapTiles[i] = new OverlapTile(i);
            DrawOverlapsGraphicSet();
            DrawOverlapsTileSet(overlapTiles);
        }
        // public functions
        public void DrawOverlapsGraphicSet()
        {
            graphicSet = new byte[128 * 96];
            int offset = 0x1DE000;
            int loc = 0;
            for (int o = 0; o < 96; o++)   // for all 104 overlap tiles
            {
                offset = (o * 0x20) + 0x1DE000;
                loc = (o / 8) * 0x400;
                loc += (o % 8) * 0x40;
                int a = 0, b = 0;
                for (int i = 0; i < 8; i++, a++, b += 2)
                {
                    graphicSet[b + loc] = Model.ROM[offset + a];
                    graphicSet[b + 1 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x10 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x11 + loc] = Model.ROM[offset + a];
                }
                for (int i = 0; i < 8; i++, a++, b += 2)
                {
                    graphicSet[b + 0x10 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x11 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x20 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x21 + loc] = Model.ROM[offset + a];
                }
                for (int i = 0; i < 8; i++, a++, b += 2)
                {
                    graphicSet[b + 0x1E0 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x1E1 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x1F0 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x1F1 + loc] = Model.ROM[offset + a];
                }
                for (int i = 0; i < 8; i++, a++, b += 2)
                {
                    graphicSet[b + 0x1F0 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x1F1 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x200 + loc] = Model.ROM[offset + a];
                    graphicSet[b + 0x201 + loc] = Model.ROM[offset + a];
                }
            }
        }
        public void DrawOverlapsTileSet(OverlapTile[] tileset)
        {
            int offset = 0x1CEA00;
            int index = 0, loc = 0, place = 0;
            int[] palette = new int[16];
            for (int i = 0; i < palette.Length; i++)
                palette[i] = Color.Black.ToArgb();
            Subtile source;
            Tile[] subtiles;
            for (int i = 0; i < tileset.Length; i++)   // for all 104 overlap tiles
            {
                subtiles = new Tile[4];
                for (int o = 0; o < subtiles.Length; o++)
                    subtiles[o] = new Tile(o);
                for (int a = 0; a < 4; a++) // the four 16x16 tiles in an overlap tile
                {
                    index = Model.ROM[i * 5 + a + offset];
                    place = (i / 8) * 32;
                    place += ((i % 8) * 2) + (a % 2) + ((a / 2) * 16);
                    subtiles[a].Mirror = Bits.GetBit(Model.ROM, i * 5 + 4 + offset, ((a * 2) + 1) ^ 7);
                    subtiles[a].Invert = Bits.GetBit(Model.ROM, i * 5 + 4 + offset, (a * 2) ^ 7);
                    if (index != 0)
                    {
                        index--;
                        loc = index % 8 * 0x40;
                        loc += index / 8 * 0x400;
                        source = new Subtile(index, graphicSet, loc, palette, false, false, false, false);
                        subtiles[a].Subtiles[0] = source;
                        source = new Subtile(index, graphicSet, loc + 0x20, palette, false, false, false, false);
                        subtiles[a].Subtiles[1] = source;
                        source = new Subtile(index, graphicSet, loc + 0x200, palette, false, false, false, false);
                        subtiles[a].Subtiles[2] = source;
                        source = new Subtile(index, graphicSet, loc + 0x220, palette, false, false, false, false);
                        subtiles[a].Subtiles[3] = source;
                    }
                    else
                    {
                        source = new Subtile(index, new byte[0x20], 0, palette, false, false, false, false);
                        subtiles[a].Subtiles[0] = source;
                        subtiles[a].Subtiles[1] = source;
                        subtiles[a].Subtiles[2] = source;
                        subtiles[a].Subtiles[3] = source;
                    }
                    tileset[i].SetSubtile(subtiles[a], a);
                }
            }
        }
    }
}
