using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public class LevelTilemap : Tilemap
    {
        #region Variables
        private Tileset tileset;
        private PaletteSet paletteSet;
        private State state = State.Instance;
        private TilemapType type = TilemapType.None;
        public int Width = 64;
        public int Height = 64;
        private byte[][] tilemaps_Bytes = new byte[3][];
        private Tile[][] tilemaps_Tiles = new Tile[3][];
        private int[] pixels = new int[1024 * 1024];
        private int[] subscreen = null;
        private int[] colorMath = null;
        private int[] tile = new int[256];
        public int[]
            L1Priority0 = new int[1024 * 1024],
            L1Priority1 = new int[1024 * 1024],
            L2Priority0 = new int[1024 * 1024],
            L2Priority1 = new int[1024 * 1024],
            L3Priority0 = new int[1024 * 1024],
            L3Priority1 = new int[1024 * 1024];
        // level variables
        private LevelMap levelMap;
        private LevelLayer levelLayer;
        private PrioritySet prioritySet
        {
            get { return prioritySets[levelLayer.PrioritySet]; }
            set { prioritySets[levelLayer.PrioritySet] = value; }
        }
        private PrioritySet[] prioritySets;
        // accessors
        public override int Width_p { get { return Width * 16; } set { Width = value / 16; } }
        public override int Height_p { get { return Height * 16; } set { Height = value / 16; } }
        public Size Size { get { return new Size(Width, Height); } }
        public Size Size_p { get { return new Size(Width_p, Height_p); } }
        public override Tile[] Tilemap_Tiles { get { return tilemaps_Tiles[0]; } set { tilemaps_Tiles[0] = value; } }
        public override Tile[][] Tilemaps_Tiles { get { return tilemaps_Tiles; } set { tilemaps_Tiles = value; } }
        public override int[] Pixels { get { return pixels; } set { } }
        public override byte[] Tilemap_Bytes { get { return null; } set { } }
        public override byte[][] Tilemaps_Bytes { get { return tilemaps_Bytes; } set { tilemaps_Bytes = value; } }
        public override Bitmap Image
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
        #endregion
        // constructors
        public LevelTilemap(Level level, Tileset tileset)
        {
            this.tileset = tileset;
            this.levelMap = Model.LevelMaps[level.LevelMap];
            this.paletteSet = Model.PaletteSets[levelMap.PaletteSet];
            this.prioritySets = Model.PrioritySets;
            this.levelLayer = level.Layer;
            this.type = TilemapType.Level;
            tilemaps_Bytes[0] = Model.Tilemaps[levelMap.TilemapL1 + 0x40];
            tilemaps_Bytes[1] = Model.Tilemaps[levelMap.TilemapL2 + 0x40];
            tilemaps_Bytes[2] = Model.Tilemaps[levelMap.TilemapL3];
            for (int i = 0; i < 3; i++)
                CreateLayer(i); // Create any required layers
            DrawAllLayers();
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[1024 * 1024];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
        }
        public LevelTilemap(Level level, Tileset tileset, LevelTemplate template)
        {
            this.tileset = tileset;
            this.levelMap = Model.LevelMaps[level.LevelMap];
            this.paletteSet = Model.PaletteSets[levelMap.PaletteSet];
            this.prioritySets = Model.PrioritySets;
            this.levelLayer = level.Layer;
            this.type = TilemapType.Template;
            tilemaps_Bytes[0] = new byte[0x2000];
            tilemaps_Bytes[1] = new byte[0x2000];
            tilemaps_Bytes[2] = new byte[0x1000];
            for (int y = 0; y < template.Size.Height / 16; y++)
            {
                for (int x = 0; x < template.Size.Width / 16; x++)
                {
                    Bits.SetShort(tilemaps_Bytes[0], (y * 64 + x) * 2,
                        Bits.GetShort(template.Tilemaps[0], (y * (template.Size.Width / 16) + x) * 2));
                    Bits.SetShort(tilemaps_Bytes[1], (y * 64 + x) * 2,
                        Bits.GetShort(template.Tilemaps[1], (y * (template.Size.Width / 16) + x) * 2));
                    tilemaps_Bytes[2][y * 64 + x] = template.Tilemaps[2][y * (template.Size.Width / 16) + x];
                }
            }
            for (int i = 0; i < 3; i++)
                CreateLayer(i); // Create any required layers
            DrawAllLayers();
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[1024 * 1024];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
        }
        public LevelTilemap(Level level, Tileset tileset, LevelTileMods.Mod mod, bool set)
        {
            this.tileset = tileset;
            this.levelMap = Model.LevelMaps[level.LevelMap];
            this.paletteSet = Model.PaletteSets[levelMap.PaletteSet];
            this.prioritySets = Model.PrioritySets;
            this.levelLayer = level.Layer;
            this.type = TilemapType.Mod;
            this.Width = mod.Width;
            this.Height = mod.Height;
            L1Priority0 = new int[Width_p * Height_p];
            L1Priority1 = new int[Width_p * Height_p];
            L2Priority0 = new int[Width_p * Height_p];
            L2Priority1 = new int[Width_p * Height_p];
            L3Priority0 = new int[Width_p * Height_p];
            L3Priority1 = new int[Width_p * Height_p];
            pixels = new int[Width_p * Height_p];
            if (!set)
                this.tilemaps_Bytes = mod.TilemapsA;
            else
                this.tilemaps_Bytes = mod.TilemapsB;
            for (int i = 0; i < 3; i++)
                CreateLayer(i); // Create any required layers
            DrawAllLayers();
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[Width_p * Height_p];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
        }
        #region Functions
        // assemblers
        public override void Assemble()
        {
            for (int l = 0; l < 3; l++)
            {
                if (tilemaps_Tiles[l] == null) continue;
                for (int i = 0; i < tilemaps_Tiles[l].Length; i++)
                {
                    if (l < 2)
                        Bits.SetShort(tilemaps_Bytes[l], i * 2, (ushort)tilemaps_Tiles[l][i].Index);
                    else
                        tilemaps_Bytes[2][i] = (byte)tilemaps_Tiles[2][i].Index;
                }
            }
        }
        // class functions
        private void ChangeSingleTile(int layer, int placement, int tile, int x, int y)
        {
            tilemaps_Tiles[layer][placement] = tileset.Tilesets_tiles[layer][tile]; // Change the tile in the layer map
            Tile source = tilemaps_Tiles[layer][placement]; // Grab the new tile
            int[] layerA = null, layerB = null; // Just used to save space
            if (layer == 0)
            {
                layerA = L1Priority0;
                layerB = L1Priority1;
            }
            else if (layer == 1)
            {
                layerA = L2Priority0;
                layerB = L2Priority1;
            }
            else if (layer == 2)
            {
                layerA = L3Priority0;
                layerB = L3Priority1;
            }
            ClearSingleTile(layerA, x, y);
            ClearSingleTile(layerB, x, y);
            // Draw all 4 subtiles to the appropriate array based on priority
            if (!source.Subtiles[0].Priority1) // tile 0
                Do.PixelsToPixels(source.Subtiles[0].Pixels, layerA, Width_p, new Rectangle(x, y, 8, 8));
            else
                Do.PixelsToPixels(source.Subtiles[0].Pixels, layerB, Width_p, new Rectangle(x, y, 8, 8));
            if (!source.Subtiles[1].Priority1) // tile 1
                Do.PixelsToPixels(source.Subtiles[1].Pixels, layerA, Width_p, new Rectangle((x + 8), y, 8, 8));
            else
                Do.PixelsToPixels(source.Subtiles[1].Pixels, layerB, Width_p, new Rectangle((x + 8), y, 8, 8));
            if (!source.Subtiles[2].Priority1) // tile 2
                Do.PixelsToPixels(source.Subtiles[2].Pixels, layerA, Width_p, new Rectangle(x, (y + 8), 8, 8));
            else
                Do.PixelsToPixels(source.Subtiles[2].Pixels, layerB, Width_p, new Rectangle(x, (y + 8), 8, 8));
            if (!source.Subtiles[3].Priority1) // tile 3
                Do.PixelsToPixels(source.Subtiles[3].Pixels, layerA, Width_p, new Rectangle((x + 8), (y + 8), 8, 8));
            else
                Do.PixelsToPixels(source.Subtiles[3].Pixels, layerB, Width_p, new Rectangle((x + 8), (y + 8), 8, 8));
            // If we have a subscreen, draw the new tile to it
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                ClearSingleTile(subscreen, x, y);
                DrawSingleSubscreenTile(x, y);
            }
            ClearSingleTile(pixels, x, y);
            DrawSingleMainscreenTile(x, y);
        }
        private void ClearSingleTile(int[] arr, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                arr[y * Width_p + x + counter] = 0;
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void CopySingleTileToArray(int[] dest, int[] source, int width, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                if (source[i] != 0)
                    dest[y * width + x + counter] = source[i];
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
        }
        private void CopyToPixelArray(int[] dest, int[] source)
        {
            try
            {
                for (int i = 0; i < source.Length; i++)
                    if (source[i] != 0)
                        dest[i] = source[i];
            }
            catch
            {
                // overflow
            }
        }
        private void CreateLayer(int layer)
        {
            if (tilemaps_Bytes[layer] == null)
                return;
            if (tileset.Tilesets_tiles[layer] == null)
                return;
            int offset = 0;
            ushort tileNum;
            byte increment = 2;
            if (layer == 2)
                increment = 1;
            tilemaps_Tiles[layer] = new Tile[Width * Height]; // Create our layer here
            if (layer != 2) // Layers 1 and 2
            {
                for (int i = 0; i < Width * Height && i < tilemaps_Bytes[layer].Length / increment; i++)
                {
                    tileNum = Bits.GetShort(tilemaps_Bytes[layer], offset);
                    if (tileNum > 0x1FF)
                        tileNum = 0;
                    offset += increment;
                    tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][tileNum];
                }
            }
            else // Layer 3
            {
                for (int i = 0; i < Width * Height && i < tilemaps_Bytes[layer].Length / increment; i++)
                {
                    tileNum = tilemaps_Bytes[layer][offset];
                    if (tileNum > 0xFF)
                        tileNum = 0;
                    offset += increment;
                    tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][tileNum];
                }
            }
        }
        private void CreateMainscreen()
        {
            int bgcolor = paletteSet.Palette[16];
            if (HaveSubscreen()) // We are doing color math by the layer
            {
                if (colorMath == null)
                    colorMath = new int[Width_p * Height_p];
                else
                    colorMath = new int[colorMath.Length];
                if (prioritySets[levelLayer.PrioritySet].ColorMathBG && state.BG)
                {
                    for (int i = 0; i < Width_p * Height_p; i++)
                        colorMath[i] = bgcolor;
                    DoColorMath(colorMath);
                    CopyToPixelArray(pixels, colorMath);
                    colorMath = new int[colorMath.Length];
                }
                else if (state.BG)
                {
                    for (int i = 0; i < Width_p * Height_p; i++)
                        pixels[i] = bgcolor;
                }
                if (levelMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        CopyToPixelArray(colorMath, L3Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        CopyToPixelArray(colorMath, L2Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        CopyToPixelArray(colorMath, L1Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        CopyToPixelArray(colorMath, L2Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        CopyToPixelArray(colorMath, L1Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        CopyToPixelArray(colorMath, L3Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                }
                else if (!levelMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        CopyToPixelArray(colorMath, L3Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        CopyToPixelArray(colorMath, L3Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        CopyToPixelArray(colorMath, L2Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        CopyToPixelArray(colorMath, L1Priority0);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        CopyToPixelArray(colorMath, L2Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        CopyToPixelArray(colorMath, L1Priority1);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMath(colorMath);
                        CopyToPixelArray(pixels, colorMath);
                        colorMath = new int[colorMath.Length];
                    }
                }
            }
            else // No color math, we can go ahead and draw the mainscreen
            {
                if (levelMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopyToPixelArray(pixels, L3Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopyToPixelArray(pixels, L2Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopyToPixelArray(pixels, L1Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopyToPixelArray(pixels, L2Priority1);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopyToPixelArray(pixels, L1Priority1);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopyToPixelArray(pixels, L3Priority1);
                }
                else if (!levelMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopyToPixelArray(pixels, L3Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopyToPixelArray(pixels, L3Priority1);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopyToPixelArray(pixels, L2Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopyToPixelArray(pixels, L1Priority0);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopyToPixelArray(pixels, L2Priority1);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopyToPixelArray(pixels, L1Priority1);
                }
                // Apply BG color
                if (state.BG)
                {
                    for (int i = 0; i < Width_p * Height_p; i++)
                    {
                        if (pixels[i] == 0)
                            pixels[i] = bgcolor;
                    }
                }
            }
        }
        private void CreateSubscreen()
        {
            if (levelMap.TopPriorityL3) //[3,0][2,0][1,0][2,1][1,1][3,1]
            {
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    CopyToPixelArray(subscreen, L3Priority0);//DrawLayerByPriorityOne(subscreenPixels, 2, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                    CopyToPixelArray(subscreen, L2Priority0);//DrawLayerByPriorityOne(subscreenPixels, 1, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                    CopyToPixelArray(subscreen, L1Priority0);//DrawLayerByPriorityOne(subscreenPixels, 0, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                    CopyToPixelArray(subscreen, L2Priority1);//DrawLayerByPriorityOne(subscreenPixels, 1, true);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                    CopyToPixelArray(subscreen, L1Priority1);//DrawLayerByPriorityOne(subscreenPixels, 0, true);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    CopyToPixelArray(subscreen, L3Priority1);//DrawLayerByPriorityOne(subscreenPixels, 2, true);
            }
            else if (!levelMap.TopPriorityL3) //[3,0][3,1][2,0][1,0][2,1][1,1]
            {
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    CopyToPixelArray(subscreen, L3Priority0);//DrawLayerByPriorityOne(subscreenPixels, 2, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    CopyToPixelArray(subscreen, L3Priority1);//DrawLayerByPriorityOne(subscreenPixels, 2, true);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                    CopyToPixelArray(subscreen, L2Priority0);//DrawLayerByPriorityOne(subscreenPixels, 1, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                    CopyToPixelArray(subscreen, L1Priority0);//DrawLayerByPriorityOne(subscreenPixels, 0, false);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                    CopyToPixelArray(subscreen, L2Priority1);//DrawLayerByPriorityOne(subscreenPixels, 1, true);
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                    CopyToPixelArray(subscreen, L1Priority1);//DrawLayerByPriorityOne(subscreenPixels, 0, true);
            }
        }
        private void DoColorMath(int[] layer)
        {
            for (int y = 0; y < Height_p; y++)
            {
                for (int x = 0; x < Width_p; x++)
                {
                    if (subscreen[y * Width_p + x] != 0 && layer[y * Width_p + x] != 0)
                    {
                        layer[y * Width_p + x] = Do.ColorMath(layer[y * Width_p + x], subscreen[y * Width_p + x],
                            prioritySets[levelLayer.PrioritySet].ColorMathHalfIntensity == 1,
                            prioritySets[levelLayer.PrioritySet].ColorMathMinusSubscreen == 1);
                    }
                }
            }
        }
        private void DoColorMathOnSingleTile(int[] tile, int x, int y)
        {
            for (int w = 0; w < 16; w++)
            {
                for (int v = 0; v < 16; v++)
                {
                    if (subscreen[(y + w) * Width_p + (x + v)] != 0 && tile[w * 16 + v] != 0)
                    {
                        tile[w * 16 + v] = Do.ColorMath(tile[w * 16 + v], subscreen[(y + w) * Width_p + (x + v)],
                            prioritySets[levelLayer.PrioritySet].ColorMathHalfIntensity == 1,
                            prioritySets[levelLayer.PrioritySet].ColorMathMinusSubscreen == 1);
                    }
                }
            }
        }
        private void DrawAllLayers()
        {
            if (prioritySets[levelLayer.PrioritySet].SubscreenL1 || prioritySets[levelLayer.PrioritySet].MainscreenL1)
            {
                DrawLayerByPriorityOne(L1Priority0, 0, false);
                DrawLayerByPriorityOne(L1Priority1, 0, true);
            }
            if (prioritySets[levelLayer.PrioritySet].SubscreenL2 || prioritySets[levelLayer.PrioritySet].MainscreenL2)
            {
                DrawLayerByPriorityOne(L2Priority0, 1, false);
                DrawLayerByPriorityOne(L2Priority1, 1, true);
            }
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL3 || prioritySets[levelLayer.PrioritySet].MainscreenL3) && levelMap.GraphicSetL3 != 0xFF)
            {
                DrawLayerByPriorityOne(L3Priority0, 2, false);
                DrawLayerByPriorityOne(L3Priority1, 2, true);
            }
        }
        private int[] DrawLayerByPriorityOne(int[] dest, int layer, bool priority1)
        {
            if (dest.Length != Width_p * Height_p || tilemaps_Tiles[layer] == null)
                return null;
            for (int i = 0; i < tilemaps_Tiles[layer].Length; i++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (tilemaps_Tiles[layer][i].Subtiles[z].Priority1 == priority1)
                    {
                        switch (z)
                        {
                            case 0:
                                Do.PixelsToPixels(tilemaps_Tiles[layer][i].Subtiles[z].Pixels, dest, Width_p, new Rectangle((i % Width) * 16, (i / Width) * 16, 8, 8));
                                break;
                            case 1:
                                Do.PixelsToPixels(tilemaps_Tiles[layer][i].Subtiles[z].Pixels, dest, Width_p, new Rectangle((i % Width) * 16 + 8, (i / Width) * 16, 8, 8));
                                break;
                            case 2:
                                Do.PixelsToPixels(tilemaps_Tiles[layer][i].Subtiles[z].Pixels, dest, Width_p, new Rectangle((i % Width) * 16, (i / Width) * 16 + 8, 8, 8));
                                break;
                            case 3:
                                Do.PixelsToPixels(tilemaps_Tiles[layer][i].Subtiles[z].Pixels, dest, Width_p, new Rectangle((i % Width) * 16 + 8, (i / Width) * 16 + 8, 8, 8));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return dest;
        }
        private void DrawSingleMainscreenTile(int x, int y)
        {
            int bgcolor = paletteSet.Palette[16];
            Bits.Clear(tile);
            int[] tileColorMath = new int[16 * 16];
            if (HaveSubscreen())
            {
                if (prioritySets[levelLayer.PrioritySet].ColorMathBG && state.BG)
                {
                    for (int i = 0; i < 256; i++)
                        tileColorMath[i] = bgcolor;
                    DoColorMathOnSingleTile(tileColorMath, x, y);
                    CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                    Bits.Clear(tileColorMath);
                }
                else if (state.BG)
                {
                    for (int i = 0; i < 256; i++)
                        tileColorMath[i] = bgcolor;
                    CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                    Bits.Clear(tileColorMath);
                }
                if (levelMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        tileColorMath = GetTilePixels(L3Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        tileColorMath = GetTilePixels(L2Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        tileColorMath = GetTilePixels(L1Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        tileColorMath = GetTilePixels(L2Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        tileColorMath = GetTilePixels(L1Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        tileColorMath = GetTilePixels(L3Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                }
                else if (!levelMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        tileColorMath = GetTilePixels(L3Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                    {
                        tileColorMath = GetTilePixels(L3Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL3)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        tileColorMath = GetTilePixels(L2Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        tileColorMath = GetTilePixels(L1Priority0, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                    {
                        tileColorMath = GetTilePixels(L2Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL2)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                    {
                        tileColorMath = GetTilePixels(L1Priority1, x, y);
                        if (prioritySets[levelLayer.PrioritySet].ColorMathL1)
                            DoColorMathOnSingleTile(tileColorMath, x, y);
                        CopySingleTileToArray(pixels, tileColorMath, Width_p, x, y);
                        Bits.Clear(tileColorMath);
                    }
                }
            }
            else // No color math, we can go ahead and draw the mainscreen
            {
                if (levelMap.TopPriorityL3) // [3,0][2,0][1,0][2,1][1,1][3,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority1, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority1, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority1, x, y), Width_p, x, y);
                }
                else if (!levelMap.TopPriorityL3) // [3,0][3,1][2,0][1,0][2,1][1,1]
                {
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                        CopySingleTileToArray(pixels, GetTilePixels(L3Priority1, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority0, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL2 && state.Layer2)
                        CopySingleTileToArray(pixels, GetTilePixels(L2Priority1, x, y), Width_p, x, y);
                    if (prioritySets[levelLayer.PrioritySet].MainscreenL1 && state.Layer1)
                        CopySingleTileToArray(pixels, GetTilePixels(L1Priority1, x, y), Width_p, x, y);
                }
                // Apply BG color
                if (state.BG)
                {
                    for (int b = y; b < y + 16; b++)
                    {
                        for (int a = x; a < x + 16; a++)
                        {
                            if (pixels[b * Width_p + a] == 0)
                                pixels[b * Width_p + a] = bgcolor;
                        }
                    }
                }
            }
        }
        private void DrawSingleSubscreenTile(int x, int y)
        {
            if (levelMap.TopPriorityL3) //[3,0][2,0][1,0][2,1][1,1][3,1]
            {
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                {
                    tile = GetTilePixels(L3Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                {
                    tile = GetTilePixels(L2Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                {
                    tile = GetTilePixels(L1Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                {
                    tile = GetTilePixels(L2Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                {
                    tile = GetTilePixels(L1Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                {
                    tile = GetTilePixels(L3Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
            }
            else if (!levelMap.TopPriorityL3) //[3,0][3,1][2,0][1,0][2,1][1,1]
            {
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                {
                    tile = GetTilePixels(L3Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3 && levelMap.GraphicSetL3 != 0xFF)
                {
                    tile = GetTilePixels(L3Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                {
                    tile = GetTilePixels(L2Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                {
                    tile = GetTilePixels(L1Priority0, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2)
                {
                    tile = GetTilePixels(L2Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
                if (prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1)
                {
                    tile = GetTilePixels(L1Priority1, x, y);
                    CopySingleTileToArray(subscreen, tile, Width_p, x, y);
                    Bits.Clear(tile);
                }
            }
        }
        // accessor functions
        public override int GetPixelLayer(int x, int y)
        {
            if (levelMap.TopPriorityL3)
            {
                if (prioritySet.MainscreenL3 && L3Priority1[y * Width_p + x] != 0) return 2;
                else if (prioritySet.MainscreenL1 && L1Priority1[y * Width_p + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority1[y * Width_p + x] != 0) return 1;
                else if (prioritySet.MainscreenL1 && L1Priority0[y * Width_p + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority0[y * Width_p + x] != 0) return 1;
                else if (prioritySet.MainscreenL3 && L3Priority0[y * Width_p + x] != 0) return 2;
            }
            else
            {
                if (prioritySet.MainscreenL1 && L1Priority1[y * Width_p + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority1[y * Width_p + x] != 0) return 1;
                else if (prioritySet.MainscreenL1 && L1Priority0[y * Width_p + x] != 0) return 0;
                else if (prioritySet.MainscreenL2 && L2Priority0[y * Width_p + x] != 0) return 1;
                else if (prioritySet.MainscreenL3 && L3Priority1[y * Width_p + x] != 0) return 2;
                else if (prioritySet.MainscreenL3 && L3Priority0[y * Width_p + x] != 0) return 2;
            }
            return 0;
        }
        public override int[] GetPixels(int layer, Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            switch (layer)
            {
                case 0:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L1Priority0[y * Width_p + x];
                            if (L1Priority1[y * Width_p + x] != 0)
                                pixels[b * s.Width + a] = L1Priority1[y * Width_p + x];
                        }
                    }
                    break;
                case 1:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L2Priority0[y * Width_p + x];
                            if (L2Priority1[y * Width_p + x] != 0)
                                pixels[b * s.Width + a] = L2Priority1[y * Width_p + x];
                        }
                    }
                    break;
                case 2:
                    for (int b = 0, y = p.Y; b < s.Height; b++, y++)
                    {
                        for (int a = 0, x = p.X; a < s.Width; a++, x++)
                        {
                            pixels[b * s.Width + a] = L3Priority0[y * Width_p + x];
                            if (L3Priority1[y * Width_p + x] != 0)
                                pixels[b * s.Width + a] = L3Priority1[y * Width_p + x];
                        }
                    }
                    break;
                default:
                    goto case 0;
            }
            return pixels;
        }
        public override int[] GetPixels(Point p, Size s)
        {
            int[] pixels = new int[s.Width * s.Height];
            int bgcolor = paletteSet.Palette[16];
            for (int b = 0, y = p.Y; b < s.Height; b++, y++)
            {
                for (int a = 0, x = p.X; a < s.Width; a++, x++)
                {
                    int srcIndex = y * Width_p + x;
                    int dstIndex = b * s.Width + a;
                    if (srcIndex >= this.pixels.Length || dstIndex >= pixels.Length)
                        continue;
                    if (this.pixels[srcIndex] != 0)
                        pixels[dstIndex] = Color.FromArgb(this.pixels[srcIndex]).ToArgb();
                    else if (state.BG)
                        pixels[dstIndex] = Color.FromArgb(bgcolor).ToArgb();
                }
            }
            return pixels;
        }
        public override int[] GetPriority1Pixels()
        {
            int[] pixels = new int[Width_p * Height_p];
            for (int y = 0; y < Height_p; y++)
            {
                for (int x = 0; x < Width_p; x++)
                {
                    if (L1Priority1[y * Width_p + x] != 0 ||
                        L2Priority1[y * Width_p + x] != 0 ||
                        L3Priority1[y * Width_p + x] != 0)
                        pixels[y * Width_p + x] = Color.Blue.ToArgb();
                }
            }
            return pixels;
        }
        public override int GetTileNum(int layer, int x, int y, bool ignoretransparent)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= Width_p) x = Width_p - 1;
            if (y >= Height_p) y = Height_p - 1;
            Point p = new Point(x % 16, y % 16);
            y /= 16;
            x /= 16;
            int placement = y * Width + x;
            if (layer < 3 && tilemaps_Tiles[layer] != null)
            {
                if (!ignoretransparent)
                    return tilemaps_Tiles[layer][placement].Index;
                else if (tilemaps_Tiles[layer][placement].Pixels[p.Y * 16 + p.X] != 0)
                    return tilemaps_Tiles[layer][placement].Index;
                else
                    return 0;
            }
            else
                return 0;
        }
        public override int GetTileNum(int layer, int x, int y)
        {
            return GetTileNum(layer, x, y, false);
        }
        public override int GetTileNum(int index)
        {
            return 0;
        }
        private int[] GetTilePixels(int[] src, int x, int y)
        {
            int counter = 0;
            for (int i = 0; i < 256; i++)
            {
                if (src[y * Width_p + x + counter] != 0)
                    tile[i] = src[y * Width_p + x + counter];
                counter++;
                if (counter % 16 == 0)
                {
                    y++;
                    counter = 0;
                }
            }
            return tile;
        }
        public override void SetTileNum(int tilenum, int layer, int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width_p || y >= Height_p)
                return;
            y /= 16;
            x /= 16;
            int index = y * Width + x;
            if (index < 0x1000)
                ChangeSingleTile(layer, index, tilenum, x * 16, y * 16);
            if (type == TilemapType.Mod)
            {
                if (layer < 2)
                    Bits.SetShort(tilemaps_Bytes[layer], (y * Width + x) * 2, (ushort)tilenum);
                else
                    tilemaps_Bytes[layer][y * Width + x] = (byte)tilenum;
                return;
            }
            switch (layer)
            {
                case 0: Model.EditTilemaps[levelMap.TilemapL1 + 0x40] = true; break;
                case 1: Model.EditTilemaps[levelMap.TilemapL2 + 0x40] = true; break;
                case 2: Model.EditTilemaps[levelMap.TilemapL3] = true; break;
            }
        }
        public override void SetTileNum()
        {
        }
        // boolean functions
        private bool HaveSubscreen()
        {
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
                return true;
            return false;
        }
        // universal variables
        public override void RedrawTilemaps()
        {
            L1Priority1 = new int[Width_p * Height_p];
            L2Priority1 = new int[Width_p * Height_p];
            L3Priority1 = new int[Width_p * Height_p];
            DrawAllLayers();
            pixels = new int[pixels.Length];
            if (subscreen != null)
                subscreen = new int[subscreen.Length];
            if ((prioritySets[levelLayer.PrioritySet].SubscreenL1 && state.Layer1) ||
                    (prioritySets[levelLayer.PrioritySet].SubscreenL2 && state.Layer2) ||
                    (prioritySets[levelLayer.PrioritySet].SubscreenL3 && state.Layer3) ||
                    (prioritySets[levelLayer.PrioritySet].SubscreenOBJ && state.NPCs))
            {
                if (subscreen == null)
                    subscreen = new int[Width_p * Height_p];
                CreateSubscreen(); // Create the subscreen if needed
            }
            CreateMainscreen();
        }
        public void Clear(int count)
        {
            if (count == 1)
            {
                Model.Tilemaps[levelMap.TilemapL1 + 0x40] = new byte[0x2000];
                Model.Tilemaps[levelMap.TilemapL2 + 0x40] = new byte[0x2000];
                Model.Tilemaps[levelMap.TilemapL3] = new byte[0x1000];
                Model.EditTilemaps[levelMap.TilemapL1 + 0x40] = true;
                Model.EditTilemaps[levelMap.TilemapL2 + 0x40] = true;
                Model.EditTilemaps[levelMap.TilemapL3] = true;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i < 0x40)
                        Model.Tilemaps[i] = new byte[0x1000];
                    else
                        Model.Tilemaps[i] = new byte[0x2000];
                    Model.EditTilemaps[i] = true;
                }
            }
            RedrawTilemaps();
        }
        #endregion
    }
}
