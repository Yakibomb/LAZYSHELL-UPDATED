using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public class Solidity
    {
        // static variables
        static Solidity instance = null;
        static readonly object padlock = new object();
        static double[] hues = new double[] 
        { 
            /*grey*/    0.0,    // normal
            /*pink*/    0.0,    // solid
            /*blue*/    240.0,  // water
            /*green*/   120.0,   // vine
            0.0, 0.0, 240.0, 120.0
        };
        static double[] sats = new double[] { 
            0.0, 1.0, 1.0, 1.0,
            0.0, 1.0, 1.0, 1.0
        };
        static double[] lums = new double[] { 
            0.0, 0.0, 0.0, 0.0,
            64.0, 64.0, 64.0, 64.0
        };
        // class variables
        private SolidityTile tile;
        private int[][] quadBasePixels = new int[8][] { 
            new int[16 * 8], new int[16 * 8], new int[16 * 8], new int[16 * 8],
            new int[16 * 8], new int[16 * 8], new int[16 * 8], new int[16 * 8]};
        private int[][] quadBlockPixels = new int[8][] { 
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24],
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24]};
        private int[][] halfQuadBlockPixels = new int[8][] { 
            new int[16 * 16], new int[16 * 16], new int[16 * 16], new int[16 * 16],
            new int[16 * 16], new int[16 * 16], new int[16 * 16], new int[16 * 16]};
        private int[][] stairsUpRightLowPixels = new int[8][] { 
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24],
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24]};
        private int[][] stairsUpRightHighPixels = new int[8][] { 
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24],
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24]};
        private int[][] stairsUpLeftLowPixels = new int[8][] { 
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24],
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24]};
        private int[][] stairsUpLeftHighPixels = new int[8][] { 
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24],
            new int[16 * 24], new int[16 * 24], new int[16 * 24], new int[16 * 24]};
        // public accessors
        private int[] pixelTiles = new int[1024 * 1024];
        /// <summary>
        /// A tile number is assigned to each pixel (used to display the tile # in the label).
        /// </summary> 
        public int[] PixelTiles { get { return pixelTiles; } set { pixelTiles = value; } }
        private Point[] pixelCoords = new Point[1024 * 1024];
        /// <summary>
        /// An orthographic coord for both X and Y is assigned to each pixel (used to display the Orth X and Y coords in the label).
        /// </summary>
        public Point[] PixelCoords { get { return pixelCoords; } set { pixelCoords = value; } }
        private Point[] tileCoords = new Point[4194];
        /// <summary>
        /// A pixel is assigned to each tile number (used for selecting an orthographic tile).
        /// </summary>
        public Point[] TileCoords { get { return tileCoords; } set { tileCoords = value; } }
        // constructor
        public Solidity()
        {
            for (int i = 0, o = 4; i < 4; i++, o++)
            {
                quadBasePixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.quadBase);
                quadBasePixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.quadBase);
                Do.Colorize(quadBasePixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(quadBasePixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(quadBasePixels[i], 16, 8, 32, -96, true);
                Do.Gradient(quadBasePixels[o], 16, 8, 32, -96, true);
                quadBlockPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.quadBlock);
                quadBlockPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.quadBlock);
                Do.Colorize(quadBlockPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(quadBlockPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(quadBlockPixels[i], 16, 24, 32, -96, true);
                Do.Gradient(quadBlockPixels[o], 16, 24, 32, -96, true);
                halfQuadBlockPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.halfQuadBlock);
                halfQuadBlockPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.halfQuadBlock);
                Do.Colorize(halfQuadBlockPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(halfQuadBlockPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(halfQuadBlockPixels[i], 16, 16, 32, -96, true);
                Do.Gradient(halfQuadBlockPixels[o], 16, 16, 32, -96, true);
                stairsUpLeftLowPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpLeftLow);
                stairsUpLeftLowPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpLeftLow);
                Do.Colorize(stairsUpLeftLowPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(stairsUpLeftLowPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(stairsUpLeftLowPixels[i], 16, 24, 32, -96, true);
                Do.Gradient(stairsUpLeftLowPixels[o], 16, 24, 32, -96, true);
                stairsUpLeftHighPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpLeftHigh);
                stairsUpLeftHighPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpLeftHigh);
                Do.Colorize(stairsUpLeftHighPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(stairsUpLeftHighPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(stairsUpLeftHighPixels[i], 16, 24, 32, -96, true);
                Do.Gradient(stairsUpLeftHighPixels[o], 16, 24, 32, -96, true);
                stairsUpRightLowPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpRightLow);
                stairsUpRightLowPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpRightLow);
                Do.Colorize(stairsUpRightLowPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(stairsUpRightLowPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(stairsUpRightLowPixels[i], 16, 24, 32, -96, true);
                Do.Gradient(stairsUpRightLowPixels[o], 16, 24, 32, -96, true);
                stairsUpRightHighPixels[i] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpRightHigh);
                stairsUpRightHighPixels[o] = Do.ImageToPixels(global::LAZYSHELL.Properties.Resources.stairsUpRightHigh);
                Do.Colorize(stairsUpRightHighPixels[i], hues[i], sats[i], -16.0, 255);
                Do.Colorize(stairsUpRightHighPixels[o], hues[o], sats[o], -64.0, 255);
                Do.Gradient(stairsUpRightHighPixels[i], 16, 24, 32, -96, true);
                Do.Gradient(stairsUpRightHighPixels[o], 16, 24, 32, -96, true);
            }
            SetIsometric();
        }
        public static Solidity Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Solidity();
                    }
                    return instance;
                }
            }
        }
        // accessor functions
        public int[] GetTilePixels(SolidityTile tile, byte alpha)
        {
            this.tile = tile;
            int[] tilePixels = new int[32 * 784];
            int[] qBase = new int[16 * 8];
            int[] qBlock = new int[16 * 24];
            int[] hqBlock = new int[16 * 16];
            int[] stULLo = new int[16 * 24];
            int[] stULHi = new int[16 * 24];
            int[] stURLo = new int[16 * 24];
            int[] stURHi = new int[16 * 24];
            int hChange = 0;
            /******DRAW BASE TILES******/
            if (tile.BaseTileHeight == 0 && !tile.BaseTileHeight_Half && tile.StairsDirection == 0)
            {
                qBase = GetQuadPixels(true, false, 0, quadBasePixels);
                if (tile.SolidTopQuadrant)             // draw top quadbase
                {
                    for (int y = (16 + 752); y < (24 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - (16 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - (16 + 752)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadbase
                {
                    for (int y = (20 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (qBase[(y - (20 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - (20 + 752)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadbase
                {
                    for (int y = (20 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (qBase[(y - (20 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - (20 + 752)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadbase
                {
                    for (int y = (24 + 752); y < (32 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - (24 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - (24 + 752)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW TILES THAT HAVE A HEIGHT PLUS 1/2 A TILE******/
            else if (tile.BaseTileHeight == 0 && tile.BaseTileHeight_Half) // total height = 1/2
            {
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels);
                if (tile.SolidTopQuadrant)             // draw top quadblock
                {
                    for (int y = (8 + 752); y < (24 + 752); y++) // start 16 pixels above normal base start (ie. 240 - 16)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (hqBlock[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (8 + 752)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadblock
                {
                    for (int y = (12 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (hqBlock[(y - (12 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (12 + 752)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadblock
                {
                    for (int y = (12 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (hqBlock[(y - (12 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (12 + 752)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadblock
                {
                    for (int y = (16 + 752); y < (32 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (hqBlock[(y - (16 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (16 + 752)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW STAIRS THAT LEAD UP-LEFT******/
            else if (tile.BaseTileHeight == 0 && tile.StairsDirection == 1)
            {
                if (tile.SolidTopQuadrant)             // draw top quadbase
                {
                    stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels);
                    for (int y = (0 + 752); y < (24 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stULHi[(y - (0 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULHi[(y - (0 + 752)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadbase
                {
                    stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels);
                    for (int y = (4 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (stULHi[(y - (4 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = stULHi[(y - (4 + 752)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadbase
                {
                    stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels);
                    for (int y = (4 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (stULLo[(y - (4 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stULLo[(y - (4 + 752)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadbase
                {
                    stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels);
                    for (int y = (8 + 752); y < (32 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stULLo[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULLo[(y - (8 + 752)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW STAIRS THAT LEAD UP-RIGHT******/
            else if (tile.BaseTileHeight == 0 && tile.StairsDirection == 2)
            {
                if (tile.SolidTopQuadrant)             // draw top quadbase
                {
                    stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels);
                    for (int y = (0 + 752); y < (24 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stURHi[(y - (0 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURHi[(y - (0 + 752)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadbase
                {
                    stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels);
                    for (int y = (4 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (stURLo[(y - (4 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = stURLo[(y - (4 + 752)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadbase
                {
                    stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels);
                    for (int y = (4 + 752); y < (28 + 752); y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (stURHi[(y - (4 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stURHi[(y - (4 + 752)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadbase
                {
                    stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels);
                    for (int y = (8 + 752); y < (32 + 752); y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stURLo[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURLo[(y - (8 + 752)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW TILES THAT HAVE HEIGHT > 0******/
            else if (tile.BaseTileHeight > 0)
            {
                qBlock = GetQuadPixels(true, false, 1, quadBlockPixels);
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels);
                int b = 0;
                if (tile.SolidTopQuadrant)             // draw top quadblock
                {
                    for (b = 0; b < tile.BaseTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (0 + 752) - hChange; y < (24 + 752) - hChange; y++) // start 16 pixels above normal base start (ie. 240 - 16)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (qBlock[(y - ((0 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((0 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    if (tile.BaseTileHeight_Half)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (8 + 752) - hChange; y < (24 + 752) - hChange; y++) // start 16 pixels above normal base start (ie. 240 - 16)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (hqBlock[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    if (tile.StairsDirection == 1)
                    {
                        hChange = (b * 32) - (b * 16);
                        stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels);
                        for (int y = (0 + 752) - hChange; y < (24 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (stULHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    else if (tile.StairsDirection == 2)
                    {
                        hChange = (b * 32) - (b * 16);
                        stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels);
                        for (int y = (0 + 752) - hChange; y < (24 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (stURHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadblock
                {
                    for (b = 0; b < tile.BaseTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            { if (qBlock[(y - ((4 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange)) * 16 + x]; }
                        }
                    }
                    if (tile.BaseTileHeight_Half)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (12 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            { if (hqBlock[(y - ((12 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = hqBlock[(y - ((12 + 752) - hChange)) * 16 + x]; }
                        }
                    }
                    if (tile.StairsDirection == 1)
                    {
                        hChange = (b * 32) - (b * 16);
                        stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            { if (stULHi[(y - ((4 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = stULHi[(y - ((4 + 752) - hChange)) * 16 + x]; }
                        }
                    }
                    else if (tile.StairsDirection == 2)
                    {
                        hChange = (b * 32) - (b * 16);
                        stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            { if (stURLo[(y - ((4 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = stURLo[(y - ((4 + 752) - hChange)) * 16 + x]; }
                        }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadblock
                {
                    for (b = 0; b < tile.BaseTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 16; x < 32; x++)
                            { if (qBlock[(y - ((4 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange)) * 16 + (x - 16)]; }
                        }
                    }
                    if (tile.BaseTileHeight_Half)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (12 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 16; x < 32; x++)
                            { if (hqBlock[(y - ((12 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - ((12 + 752) - hChange)) * 16 + (x - 16)]; }
                        }
                    }
                    if (tile.StairsDirection == 1)
                    {
                        hChange = (b * 32) - (b * 16);
                        stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 16; x < 32; x++)
                            { if (stULLo[(y - ((4 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stULLo[(y - ((4 + 752) - hChange)) * 16 + (x - 16)]; }
                        }
                    }
                    else if (tile.StairsDirection == 2)
                    {
                        hChange = (b * 32) - (b * 16);
                        stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels);
                        for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                        {
                            for (int x = 16; x < 32; x++)
                            { if (stURHi[(y - ((4 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stURHi[(y - ((4 + 752) - hChange)) * 16 + (x - 16)]; }
                        }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadblock
                {
                    for (b = 0; b < tile.BaseTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (8 + 752) - hChange; y < (32 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (qBlock[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    if (tile.BaseTileHeight_Half)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (16 + 752) - hChange; y < (32 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (hqBlock[(y - ((16 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - ((16 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    if (tile.StairsDirection == 1)
                    {
                        hChange = (b * 32) - (b * 16);
                        stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels);
                        for (int y = (8 + 752) - hChange; y < (32 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (stULLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                    else if (tile.StairsDirection == 2)
                    {
                        hChange = (b * 32) - (b * 16);
                        stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels);
                        for (int y = (8 + 752) - hChange; y < (32 + 752) - hChange; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (stURLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                        }
                    }
                }
            }
            /******DRAW OVERHEAD WATER TILES******/
            int overH = tile.WaterTileZ * 16; int baseT = tile.BaseTileHeight * 16;
            if (tile.WaterTileZ != 0)
            {
                qBase = GetQuadPixels(false, true, 0, quadBasePixels);
                if (tile.SolidTopQuadrant)             // draw top quadbase
                {
                    for (int y = (16 + 752) - overH - baseT; y < (24 + 752) - overH - baseT; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadbase
                {
                    for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadbase
                {
                    for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadbase
                {
                    for (int y = (24 + 752) - overH - baseT; y < (32 + 752) - overH - baseT; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW OVERHEAD TILES******/
            overH = tile.OverheadTileZ * 16; baseT = tile.BaseTileHeight * 16;
            if (tile.OverheadTileHeight == 0 && tile.OverheadTileZ != 0)
            {
                qBase = GetQuadPixels(false, false, 0, quadBasePixels);
                if (tile.SolidTopQuadrant)             // draw top quadbase
                {
                    for (int y = (16 + 752) - overH - baseT; y < (24 + 752) - overH - baseT; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadbase
                {
                    for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x]; }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadbase
                {
                    for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)]; }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadbase
                {
                    for (int y = (24 + 752) - overH - baseT; y < (32 + 752) - overH - baseT; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                    }
                }
            }
            /******DRAW OVERHEAD TILES THAT HAVE A HEIGHT > 0******/
            else if (tile.OverheadTileHeight != 0)
            {
                qBlock = GetQuadPixels(false, false, 1, quadBlockPixels); hqBlock = GetQuadPixels(false, false, 2, halfQuadBlockPixels);
                int b = 0;
                if (tile.SolidTopQuadrant)             // draw top quadblock
                {
                    for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (0 + 752) - hChange - overH - baseT; y < (24 + 752) - hChange - overH - baseT; y++) // start 16 pixels above normal base start (ie. 240 - 16)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (qBlock[(y - ((0 + 752) - hChange - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((0 + 752) - hChange - overH - baseT)) * 16 + (x - 8)]; }
                        }
                    }
                }
                if (tile.SolidLeftQuadrant)              // draw left quadblock
                {
                    for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (4 + 752) - hChange - overH - baseT; y < (28 + 752) - hChange - overH - baseT; y++)
                        {
                            for (int x = 0; x < 16; x++)
                            { if (qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + x]; }
                        }
                    }
                }
                if (tile.SolidRightQuadrant)              // draw right quadblock
                {
                    for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (4 + 752) - hChange - overH - baseT; y < (28 + 752) - hChange - overH - baseT; y++)
                        {
                            for (int x = 16; x < 32; x++)
                            { if (qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + (x - 16)]; }
                        }
                    }
                }
                if (tile.SolidBottomQuadrant)              // draw bottom quadblock
                {
                    for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                    {
                        hChange = (b * 32) - (b * 16);
                        for (int y = (8 + 752) - hChange - overH - baseT; y < (32 + 752) - hChange - overH - baseT; y++)
                        {
                            for (int x = 8; x < 24; x++)
                            { if (qBlock[(y - ((8 + 752) - hChange - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((8 + 752) - hChange - overH - baseT)) * 16 + (x - 8)]; }
                        }
                    }
                }
            }
            return tilePixels;
        }
        public int[] GetTilePixels(SolidityTile tile)
        {
            return GetTilePixels(tile, 255);
        }
        public int[] GetTilemapPixels(Tilemap map)
        {
            /*********DRAW THE PHYSICAL MAP FROM BUFFER*********/
            SolidityTile[] tiles = Model.SolidTiles;
            int[] pixels = new int[1024 * 1024];
            int[] tilePixels = new int[32 * 784];
            ushort tileNum;
            int currTilePosX = 0;
            int currTilePosY = 0;
            int offset = 0;
            int xPixel = 0;
            int yPixel = 0;
            while (offset < map.Tilemap_Bytes.Length)
            {
                tileNum = Bits.GetShort(map.Tilemap_Bytes, offset);
                currTilePosX = tileCoords[offset / 2].X;
                currTilePosY = tileCoords[offset / 2].Y;
                if (tileNum != 0)
                {
                    //tilePixels = physicalTiles[tileNum].PhysicalTilePixels;
                    tilePixels = GetTilePixels(tiles[tileNum]);
                    for (int a = 752 - tiles[tileNum].TotalHeight; a < 784; a++)
                    {
                        for (int b = 0; b < 32; b++)
                        {
                            xPixel = currTilePosX + b;
                            yPixel = currTilePosY + a - 768;
                            if (xPixel >= 0 && xPixel < 1024 && yPixel >= 0 && yPixel < 1024)
                            {
                                if (tilePixels[b + (a * 32)] != 0)
                                    pixels[xPixel + (yPixel * 1024)] = tilePixels[b + (a * 32)];
                            }
                        }
                    }
                }
                offset += 2;
            }
            return pixels;
        }
        public int[] GetPriority1Pixels(Tilemap map)
        {
            /*********DRAW THE PHYSICAL MAP FROM BUFFER*********/
            SolidityTile[] tiles = Model.SolidTiles;
            int[] pixels = new int[1024 * 1024];
            int[] tilePixels = new int[32 * 784];
            ushort tileNum;
            int currTilePosX = 0;
            int currTilePosY = 0;
            int offset = 0;
            int xPixel = 0;
            int yPixel = 0;
            while (offset < map.Tilemap_Bytes.Length)
            {
                tileNum = Bits.GetShort(map.Tilemap_Bytes, offset);
                currTilePosX = tileCoords[offset / 2].X;
                currTilePosY = tileCoords[offset / 2].Y;
                if (tileNum != 0 && tiles[tileNum].P3ObjectOnTile)
                {
                    tilePixels = GetTilePixels(tiles[tileNum]);
                    for (int a = 752 - tiles[tileNum].TotalHeight; a < 784; a++)
                    {
                        for (int b = 0; b < 32; b++)
                        {
                            xPixel = currTilePosX + b;
                            yPixel = currTilePosY + a - 768;
                            if (xPixel >= 0 && xPixel < 1024 && yPixel >= 0 && yPixel < 1024)
                            {
                                if (tilePixels[b + (a * 32)] != 0)
                                    pixels[xPixel + (yPixel * 1024)] = Color.FromArgb(128, 255, 255, 0).ToArgb();
                            }
                        }
                    }
                }
                offset += 2;
            }
            return pixels;
        }
        private int[] GetQuadPixels(bool isBase, bool isWater, int type, int[][] src)
        {
            int format = 0;
            if (!isBase && !isWater) // it is an overhead tile
            {
                if (tile.SpecialTileFormat == 1)    // vines (green)
                    format = 3;
                else
                    format = 0; // overhead (white)
            }
            else if (isBase && !isWater && tile.OverheadTileZ != 0) // it is a base tile and there is something overhead
            {
                if (tile.SpecialTileFormat == 1)    // vines (green)
                    format = 3 + 4;
                else if (tile.SpecialTileFormat == 2)   // water (blue)
                    format = 2 + 4;
                else
                    format = 0 + 4;
            }
            else if (!isBase && isWater) // it is an overhead water tile
                format = 2;
            else if (isBase && isWater && (tile.OverheadTileZ != 0 || tile.WaterTileZ != 0)) // it is a base water tile and there is something overhead
                format = 2 + 4;
            else if (isBase && isWater) // it is a base water tile and there is nothing overhead
                format = 2;
            else // it is a base tile and there is nothing overhead
            {
                if (tile.SpecialTileFormat == 1)
                    format = 3;
                else if (tile.SpecialTileFormat == 2)
                    format = 2;
                else
                    format = 0;
            }
            if (tile.SolidTile && !isBase && tile.OverheadTileZ != 0)
                format = 1;
            else if (tile.SolidTile && isBase && tile.OverheadTileZ == 0)
                format = 1;
            return src[format];
        }
        private void SetIsometric()
        {
            /*********ASSIGN AN INITIAL TOP-LEFT PIXEL X,Y COORD TO EACH TILE NUMBER*********/
            int tileOffset = 0;
            int counter = 0;
            int xPixel = 0;
            int yPixel = 0;
            // Do the odd rows
            for (int y = 0; y < 65; y++)
            {
                for (int x = 0; x < 33; x++)
                {
                    xPixel = (x * 32) - 16;
                    yPixel = (y * 16) - 8;
                    tileCoords[tileOffset].X = xPixel;
                    tileCoords[tileOffset].Y = yPixel;
                    tileOffset++;
                }
                counter += 65;
                tileOffset = counter;
            }
            // Do the even rows
            tileOffset = 33;
            counter = 0;
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    xPixel = (x * 32);
                    yPixel = (y * 16);
                    tileCoords[tileOffset].X = xPixel;
                    tileCoords[tileOffset].Y = yPixel;
                    tileOffset++;
                }
                counter += 65;
                tileOffset = counter + 33;
            }
            int currTilePosX = 0;
            int currTilePosY = 0;
            int offset = 0;
            /*********ASSIGN EACH PIXEL (1024 * 1024) A TILE NUMBER*********/
            while (offset < 0x20C2)
            {
                currTilePosX = tileCoords[offset / 2].X;
                currTilePosY = tileCoords[offset / 2].Y;
                SetTileNum(offset, currTilePosX, currTilePosY);
                offset += 2;
            }
            /*********ASSIGN EACH PIXEL (1024 * 1024) X,Y ORTHOGRAPHIC COORDS*********/
            int[] orthCoord = new int[32 * 128];
            int[] orthCoordX = new int[32];
            int[] orthCoordY = new int[128];
            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    orthCoordX[x] = (((x & 127) * 32) + (16 * (y & 1))) - 16;
                    orthCoordY[y] = (y * 8) - 8;
                    SetCoords(x, y, orthCoordX[x], orthCoordY[y]);
                }
            }
        }
        private void SetTileNum(int offset, int currTilePosX, int currTilePosY)
        {
            int leftEdge = 14;
            int rightEdge = 18;
            int temp = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = leftEdge; x < rightEdge; x++)
                {
                    temp = x + currTilePosX + ((y + currTilePosY) * 1024);
                    if (temp >= 0 && temp < (1024 * 1024) &&
                        (x + currTilePosX) < 1024 &&
                        (x + currTilePosX) >= 0 &&
                        pixelTiles[temp] == 0)
                        pixelTiles[temp] = offset / 2;
                }
                leftEdge -= 2;
                rightEdge += 2;
            }
            leftEdge = 0;
            rightEdge = 32;
            for (int y = 8; y < 16; y++)
            {
                for (int x = leftEdge; x < rightEdge; x++)
                {
                    temp = x + currTilePosX + ((y + currTilePosY) * 1024);
                    if (temp >= 0 && temp < (1024 * 1024) &&
                        (x + currTilePosX) < 1024 &&
                        (x + currTilePosX) >= 0 &&
                        pixelTiles[temp] == 0)
                        pixelTiles[temp] = offset / 2;
                }
                leftEdge += 2;
                rightEdge -= 2;
            }
        }
        private void SetCoords(int orthX, int orthY, int currTilePosX, int currTilePosY)
        {
            int leftEdge = 14;
            int rightEdge = 18;
            int temp = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = leftEdge; x < rightEdge; x++)
                {
                    temp = x + currTilePosX + ((y + currTilePosY) * 1024);
                    if (temp >= 0 && temp < (1024 * 1024) && (x + currTilePosX) < 1024 && (x + currTilePosX) >= 0)
                    {
                        pixelCoords[temp].X = orthX;
                        pixelCoords[temp].Y = orthY;
                    }
                }
                leftEdge -= 2;
                rightEdge += 2;
            }
            leftEdge = 0;
            rightEdge = 32;
            for (int y = 8; y < 16; y++)
            {
                for (int x = leftEdge; x < rightEdge; x++)
                {
                    temp = x + currTilePosX + ((y + currTilePosY) * 1024);
                    if (temp >= 0 && temp < (1024 * 1024) && (x + currTilePosX) < 1024 && (x + currTilePosX) >= 0)
                    {
                        pixelCoords[temp].X = orthX;
                        pixelCoords[temp].Y = orthY;
                    }
                }
                leftEdge += 2;
                rightEdge -= 2;
            }
        }
        //
        public void RefreshTilemapImage(Tilemap map, int offset)
        {
            SolidityTile[] tiles = Model.SolidTiles;
            int[] tilePixels = new int[32 * 784];
            ushort tileNum;
            int currTilePosX = 0;
            int currTilePosY = 0;
            int xPixel = 0;
            int yPixel = 0;
            bool twoTiles = false;
            // initialize offset based on column of tile to change
            offset /= 2;
            offset = offset % 65;
            // determine if we start with two or one tile
            if (offset >= 33 && offset <= 64)
            {
                offset -= 33;
                twoTiles = true;
            }
            offset *= 2;
            // do the loop
            while (offset < map.Tilemap_Bytes.Length)
            {
                if (twoTiles)
                {
                    // ...draw eastern half of left tile
                    tileNum = Bits.GetShort(map.Tilemap_Bytes, offset);
                    currTilePosX = tileCoords[offset / 2].X;
                    currTilePosY = tileCoords[offset / 2].Y;
                    if (tileNum != 0)
                    {
                        tilePixels = GetTilePixels(tiles[tileNum]);
                        for (int a = 752 - tiles[tileNum].TotalHeight; a < 784; a++)
                        {
                            for (int b = 16; b < 32; b++)    // b = 16, ie. start at eastern half
                            {
                                xPixel = currTilePosX + b;
                                yPixel = currTilePosY + a - 768;
                                if (xPixel >= 0 && xPixel < 1024 && yPixel >= 0 && yPixel < 1024)
                                {
                                    if (tilePixels[b + (a * 32)] != 0)
                                    {
                                        map.Pixels[xPixel + (yPixel * 1024)] = tilePixels[b + (a * 32)];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int b = Math.Max(currTilePosY, 0); b < Math.Min(Math.Max(currTilePosY, 0) + 16, 1023); b++)
                        {
                            for (int a = Math.Max(currTilePosX, 0) + 16; a < Math.Min(Math.Max(currTilePosX, 0) + 32, 1023); a++)
                            {
                                if (pixelTiles[b * 1024 + a] == offset / 2)
                                    map.Pixels[b * 1024 + a] = 0;
                            }
                        }
                    }
                    offset += 2;
                    if (offset >= map.Tilemap_Bytes.Length) break;
                    // ...draw western half of right tile
                    tileNum = Bits.GetShort(map.Tilemap_Bytes, offset);
                    currTilePosX = tileCoords[offset / 2].X;
                    currTilePosY = tileCoords[offset / 2].Y;
                    if (tileNum != 0)
                    {
                        tilePixels = GetTilePixels(tiles[tileNum]);
                        for (int a = 752 - tiles[tileNum].TotalHeight; a < 784; a++)
                        {
                            for (int b = 0; b < 16; b++)    // b < 16, ie. stop when western half done drawing
                            {
                                xPixel = currTilePosX + b;
                                yPixel = currTilePosY + a - 768;
                                if (xPixel >= 0 && xPixel < 1024 && yPixel >= 0 && yPixel < 1024)
                                {
                                    if (tilePixels[b + (a * 32)] != 0)
                                    {
                                        map.Pixels[xPixel + (yPixel * 1024)] = tilePixels[b + (a * 32)];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int b = Math.Max(currTilePosY, 0); b < Math.Min(Math.Max(currTilePosY, 0) + 16, 1023); b++)
                        {
                            for (int a = Math.Max(currTilePosX, 0); a < Math.Min(Math.Max(currTilePosX, 0) + 16, 1023); a++)
                            {
                                if (pixelTiles[b * 1024 + a] == offset / 2)
                                    map.Pixels[b * 1024 + a] = 0;
                            }
                        }
                    }
                    offset += 64;
                    if (offset >= map.Tilemap_Bytes.Length) break;
                }
                else
                {
                    // ...draw full tile
                    tileNum = Bits.GetShort(map.Tilemap_Bytes, offset);
                    currTilePosX = tileCoords[offset / 2].X;
                    currTilePosY = tileCoords[offset / 2].Y;
                    if (tileNum != 0)
                    {
                        tilePixels = GetTilePixels(tiles[tileNum]);
                        for (int a = 752 - tiles[tileNum].TotalHeight; a < 784; a++)
                        {
                            for (int b = 0; b < 32; b++)
                            {
                                xPixel = currTilePosX + b;
                                yPixel = currTilePosY + a - 768;
                                if (xPixel >= 0 && xPixel < 1024 && yPixel >= 0 && yPixel < 1024)
                                {
                                    if (tilePixels[b + (a * 32)] != 0)
                                    {
                                        map.Pixels[xPixel + (yPixel * 1024)] = tilePixels[b + (a * 32)];
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int b = Math.Max(currTilePosY, 0); b < Math.Min(Math.Max(currTilePosY, 0) + 16, 1023); b++)
                        {
                            for (int a = Math.Max(currTilePosX, 0); a < Math.Min(Math.Max(currTilePosX, 0) + 32, 1023); a++)
                            {
                                if (pixelTiles[b * 1024 + a] == offset / 2)
                                    map.Pixels[b * 1024 + a] = 0;
                            }
                        }
                    }
                    offset += 64;
                    if (offset >= map.Tilemap_Bytes.Length) break;
                }
                twoTiles = !twoTiles;
            }
        }
    }
}
