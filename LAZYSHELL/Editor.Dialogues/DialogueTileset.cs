using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class DialogueTileset
    {
        // variables
        [NonSerialized()]
        private byte[] graphics;
        private PaletteSet palettes;
        private Tile[] tileset_tiles; 
        public Tile[] Tileset_tiles { get { return tileset_tiles; } }
        // constructor
        public DialogueTileset(PaletteSet palettes)
        {
            this.graphics = Model.DialogueGraphics;
            this.palettes = palettes;
            //
            tileset_tiles = new Tile[16 * 4];
            for (int i = 0; i < tileset_tiles.Length; i++)
                tileset_tiles[i] = new Tile(i);
            DrawTileset(tileset_tiles);
        }
        // assemblers
        private void Assemble()
        {
        }
        // class functions
        public void DrawTileset(Tile[] tileset_tiles)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        // for palette index 1
                        byte status = 0x04;
                        byte index = (byte)(y * 16 + (x * 2) + (z % 2));
                        index += z >= 2 ? (byte)8 : (byte)0;
                        Subtile source = Do.DrawSubtile(index, status, graphics, palettes.Palettes, 0x20);
                        tileset_tiles[y * 16 + x].Subtiles[z] = source;
                        tileset_tiles[y * 16 + x + 8].Subtiles[z] = source;
                        // for palette index 1
                        status = 0x44;
                        index ^= 7;
                        source = Do.DrawSubtile(index, status, graphics, palettes.Palettes, 0x20);
                        tileset_tiles[y * 16 + x + 4].Subtiles[z] = source;
                        tileset_tiles[y * 16 + x + 12].Subtiles[z] = source;
                    }
                }
            }
        }
    }
}
