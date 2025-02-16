using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static LAZYSHELL.Mold;

namespace LAZYSHELL
{
    public class Solidity
    {
        // static variables
        static Solidity instance = null;
        static readonly object padlock = new object();
        static double[] hues = new double[] 
        { 
    // key works on 0 = red, rainbow scale (ROYGBIV), up to violet = 255
            /*grey*/    0.0,    // normal
            /*red*/     0.0,    // solid
            /*blue*/    225.0,  // water
            /*green*/   120.0,   // vine
            /*orange*/   35.0,   // stair
            /*brown*/   60.0,   // door
            /*purple*/   265.0,  // conveyerbelt
            /*yellow*/   60.0,   // Priority overhead
        };
    // sats = saturation
        static double[] sats = new double[] {
            0.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0,
            0.0, 1.0, 0.5, 0.5, 0.5, 0.5, 0.25, 1.0,
        };
        static double[] lums = new double[] {
            -16.0, -16.0, -16.0, -16.0, 0.0, -16.0, -16.0, -16,
            64.0, 64.0, 64.0, 64.0, 64.0, 64.0, 64.0, 64.0
        };
        static int[] alpha = new int[] {
            255, 255, 128, 255, 255, 255, 255, 128
        };
        // class variables
        private SolidityTile tile;

        private const int TilesGeneratedPerBlock = 40;

        private int[][] QuadBasePixels;
        private int[][] quadBasePixels
        {
            get
            {
                if (QuadBasePixels != null)
                    return QuadBasePixels;

                QuadBasePixels = new int[TilesGeneratedPerBlock][];
                //int[] dimensions = new int[16 * 8];
                for (int i = 0; i < TilesGeneratedPerBlock; i++)
                    QuadBasePixels[i] = Do.ImageToPixels(Resources.quadBase);
                return QuadBasePixels;
            }
        }
        private int[][] QuadBlockPixels;
        private int[][] quadBlockPixels
        {
            get
            {
                if (QuadBlockPixels != null)
                    return QuadBlockPixels;

                QuadBlockPixels = new int[TilesGeneratedPerBlock][];
                //int[] dimensions = new int[16 * 24];
                for (int i = 0; i < TilesGeneratedPerBlock; i++)
                    QuadBlockPixels[i] = Do.ImageToPixels(Resources.quadBlock);
                return QuadBlockPixels;
            }
        }
        private int[][] HalfQuadBlockPixels;
        private int[][] halfQuadBlockPixels
        {
            get
            {
                if (HalfQuadBlockPixels != null)
                    return HalfQuadBlockPixels;

                HalfQuadBlockPixels = new int[TilesGeneratedPerBlock][];
               // int[] dimensions = new int[16 * 16];
                for (int i = 0; i < TilesGeneratedPerBlock; i++)
                    HalfQuadBlockPixels[i] = Do.ImageToPixels(Resources.halfQuadBlock);
                return HalfQuadBlockPixels;
            }
        }
        private static int[][] stairsPixelsBase(int stairType)
        {
            int[][] pixelBase = new int[TilesGeneratedPerBlock][];
            switch(stairType)
            {
                case 0:
                    for (int i = 0; i < TilesGeneratedPerBlock; i++)
                        pixelBase[i] = Do.ImageToPixels(Resources.stairsUpRightLow);
                    break;
                case 1:
                    for (int i = 0; i < TilesGeneratedPerBlock; i++)
                        pixelBase[i] = Do.ImageToPixels(Resources.stairsUpRightHigh);
                    break;
                case 2:
                    for (int i = 0; i < TilesGeneratedPerBlock; i++)
                        pixelBase[i] = Do.ImageToPixels(Resources.stairsUpLeftLow);
                    break;
                case 3:
                    for (int i = 0; i < TilesGeneratedPerBlock; i++)
                        pixelBase[i] = Do.ImageToPixels(Resources.stairsUpLeftHigh);
                    break;
            }
            return pixelBase;
        }
        private int[][] stairsUpRightLowPixels = stairsPixelsBase(0);
        private int[][] stairsUpRightHighPixels = stairsPixelsBase(1);
        private int[][] stairsUpLeftLowPixels = stairsPixelsBase(2);
        private int[][] stairsUpLeftHighPixels = stairsPixelsBase(3);
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

            int[][] tile = quadBasePixels;
            int param2 = 8;
            for (int tileNum = 1; tileNum < 8; tileNum++)
            {
                switch (tileNum)
                {
                    case 1: tile = quadBasePixels; param2 = 8; break;
                    case 2: tile = quadBlockPixels; param2 = 24; break;
                    case 3: tile = halfQuadBlockPixels; param2 = 16; break;
                    case 4: tile = stairsUpLeftLowPixels; param2 = 24; break;
                    case 5: tile = stairsUpLeftHighPixels; break;
                    case 6: tile = stairsUpRightLowPixels; break;
                    case 7: tile = stairsUpRightHighPixels; break;
                }
                for (int i = 0; i < 8; i++)
                {
                    Do.Colorize(tile[i], hues[i], sats[i], -32.0, alpha[i]);            //(no priority3) base tile without an overhead tile
                    Do.Colorize(tile[i + 8], hues[i], sats[i + 8], -64.0, alpha[i]);    //(no priority3) shadow base tile with an overhead tile
                    Do.Colorize(tile[i + 16], hues[i], sats[i], 0.0, i == 2 ? 128 : 255);        //(priority3) lighter base tile without an overhead tile
                    Do.Colorize(tile[i + 24], hues[i], sats[i + 8], 32.0, alpha[i]);     //(priority3) lighter shadow base tile with an overhead tile
                    Do.Colorize(tile[i + 32], hues[i], sats[i], -16.0, i == 1 ? 255 : 128);             // overhead light tiles without height (only for overhead priority3 tiles)
                    //
                    Do.Gradient(tile[i], 16, param2, 32, -96, true);
                    Do.Gradient(tile[i + 8], 16, param2, 32, -96, true);
                    if (i != 2) Do.Gradient(tile[i + 16], 16, param2, 32, -96, true);
                    Do.Gradient(tile[i + 24], 16, param2, 32, -96, true);
                    if (i == 1) Do.Gradient(tile[i + 32], 16, param2, 32, -96, true);

                }
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
        public int[] GetTilePixels(SolidityTile tile, byte alpha, bool forceFlat)
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
            if ( forceFlat || (tile.BaseTileHeight == 0 && !tile.BaseTileHeight_Half && tile.StairsDirection == 0) )
            {
                // draw top quadbase
                qBase = GetQuadPixels(true, false, 0, quadBasePixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (16 + 752); y < (24 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - (16 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - (16 + 752)) * 16 + (x - 8)]; }
                }
            // draw left quadbase
                qBase = GetQuadPixels(true, false, 0, quadBasePixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (20 + 752); y < (28 + 752); y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (qBase[(y - (20 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - (20 + 752)) * 16 + x]; }
                }
                // draw right quadbase
                qBase = GetQuadPixels(true, false, 0, quadBasePixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (20 + 752); y < (28 + 752); y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (qBase[(y - (20 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - (20 + 752)) * 16 + (x - 16)]; }
                }
                // draw bottom quadbase
                qBase = GetQuadPixels(true, false, 0, quadBasePixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (24 + 752); y < (32 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - (24 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - (24 + 752)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW TILES THAT HAVE A HEIGHT PLUS 1/2 A TILE******/
            else if (tile.BaseTileHeight == 0 && tile.BaseTileHeight_Half) // total height = 1/2
            {
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (8 + 752); y < (24 + 752); y++) // start 16 pixels above normal base start (ie. 240 - 16)
                {
                    for (int x = 8; x < 24; x++)
                    { if (hqBlock[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (8 + 752)) * 16 + (x - 8)]; }
                }
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (12 + 752); y < (28 + 752); y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (hqBlock[(y - (12 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (12 + 752)) * 16 + x]; }
                }
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (12 + 752); y < (28 + 752); y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (hqBlock[(y - (12 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (12 + 752)) * 16 + (x - 16)]; }
                }
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (16 + 752); y < (32 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (hqBlock[(y - (16 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = hqBlock[(y - (16 + 752)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW STAIRS THAT LEAD UP-LEFT******/
            else if (tile.BaseTileHeight == 0 && tile.StairsDirection == 1)
            {
                stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (0 + 752); y < (24 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (stULHi[(y - (0 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULHi[(y - (0 + 752)) * 16 + (x - 8)]; }
                }
                stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (4 + 752); y < (28 + 752); y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (stULHi[(y - (4 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = stULHi[(y - (4 + 752)) * 16 + x]; }
                }
                stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (4 + 752); y < (28 + 752); y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (stULLo[(y - (4 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stULLo[(y - (4 + 752)) * 16 + (x - 16)]; }
                }
                stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (8 + 752); y < (32 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (stULLo[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULLo[(y - (8 + 752)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW STAIRS THAT LEAD UP-RIGHT******/
            else if (tile.BaseTileHeight == 0 && tile.StairsDirection == 2)
            {
                stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (0 + 752); y < (24 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (stURHi[(y - (0 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURHi[(y - (0 + 752)) * 16 + (x - 8)]; }
                }
                stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (4 + 752); y < (28 + 752); y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (stURLo[(y - (4 + 752)) * 16 + x] != 0) tilePixels[y * 32 + x] = stURLo[(y - (4 + 752)) * 16 + x]; }
                }
                stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (4 + 752); y < (28 + 752); y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (stURHi[(y - (4 + 752)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stURHi[(y - (4 + 752)) * 16 + (x - 16)]; }
                }
                stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (8 + 752); y < (32 + 752); y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (stURLo[(y - (8 + 752)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURLo[(y - (8 + 752)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW TILES THAT HAVE HEIGHT > 0******/
            else if (tile.BaseTileHeight > 0)
            {
                int b = 0;

                // draw top quadblock

                qBlock = GetQuadPixels(true, false, 1, quadBlockPixels, tile.SolidTopQuadrant ? 1 : 0);
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidTopQuadrant ? 1 : 0);
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
                else if (tile.StairsDirection == 1)
                {
                    hChange = (b * 32) - (b * 16);
                    stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels, tile.SolidTopQuadrant ? 1 : 0);
                    for (int y = (0 + 752) - hChange; y < (24 + 752) - hChange; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stULHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)]; }
                    }
                }
                else if (tile.StairsDirection == 2)
                {
                    hChange = (b * 32) - (b * 16);
                    stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels, tile.SolidTopQuadrant ? 1 : 0);
                    for (int y = (0 + 752) - hChange; y < (24 + 752) - hChange; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stURHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURHi[(y - ((0 + 752) - hChange)) * 16 + (x - 8)]; }
                    }
                }



                // draw left quadblock
                qBlock = GetQuadPixels(true, false, 1, quadBlockPixels, tile.SolidLeftQuadrant ? 2 : 0);
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidLeftQuadrant ? 2 : 0);
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
                else if (tile.StairsDirection == 1)
                {
                    hChange = (b * 32) - (b * 16);
                    stULHi = GetQuadPixels(true, false, 1, stairsUpLeftHighPixels, tile.SolidLeftQuadrant ? 2 : 0);
                    for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (stULHi[(y - ((4 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = stULHi[(y - ((4 + 752) - hChange)) * 16 + x]; }
                    }
                }
                else if (tile.StairsDirection == 2)
                {
                    hChange = (b * 32) - (b * 16);
                    stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels, tile.SolidLeftQuadrant ? 2 : 0);
                    for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (stURLo[(y - ((4 + 752) - hChange)) * 16 + x] != 0) tilePixels[y * 32 + x] = stURLo[(y - ((4 + 752) - hChange)) * 16 + x]; }
                    }
                }



                // draw right quadblock
                qBlock = GetQuadPixels(true, false, 1, quadBlockPixels, tile.SolidRightQuadrant ? 3 : 0);
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidRightQuadrant ? 3 : 0);
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
                else if (tile.StairsDirection == 1)
                {
                    hChange = (b * 32) - (b * 16);
                    stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels, tile.SolidRightQuadrant ? 3 : 0);
                    for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (stULLo[(y - ((4 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stULLo[(y - ((4 + 752) - hChange)) * 16 + (x - 16)]; }
                    }
                }
                else if (tile.StairsDirection == 2)
                {
                    hChange = (b * 32) - (b * 16);
                    stURHi = GetQuadPixels(true, false, 1, stairsUpRightHighPixels, tile.SolidRightQuadrant ? 3 : 0);
                    for (int y = (4 + 752) - hChange; y < (28 + 752) - hChange; y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (stURHi[(y - ((4 + 752) - hChange)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = stURHi[(y - ((4 + 752) - hChange)) * 16 + (x - 16)]; }
                    }
                }


                // draw bottom quadblock
                qBlock = GetQuadPixels(true, false, 1, quadBlockPixels, tile.SolidBottomQuadrant ? 4 : 0);
                hqBlock = GetQuadPixels(true, false, 2, halfQuadBlockPixels, tile.SolidBottomQuadrant ? 4 : 0);
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
                else if (tile.StairsDirection == 1)
                {
                    hChange = (b * 32) - (b * 16);
                    stULLo = GetQuadPixels(true, false, 1, stairsUpLeftLowPixels, tile.SolidBottomQuadrant ? 4 : 0);
                    for (int y = (8 + 752) - hChange; y < (32 + 752) - hChange; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stULLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stULLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                    }
                }
                else if (tile.StairsDirection == 2)
                {
                    hChange = (b * 32) - (b * 16);
                    stURLo = GetQuadPixels(true, false, 1, stairsUpRightLowPixels, tile.SolidBottomQuadrant ? 4 : 0);
                    for (int y = (8 + 752) - hChange; y < (32 + 752) - hChange; y++)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (stURLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = stURLo[(y - ((8 + 752) - hChange)) * 16 + (x - 8)]; }
                    }
                }
            }
            // RETURN IF IN FLAT MODE (don't need to draw the overhead tiles)
            if (forceFlat)
                return tilePixels;

            /******DRAW OVERHEAD WATER TILES******/
            int overH = tile.WaterTileZ * 16; int baseT = tile.BaseTileHeight * 16;
            if (tile.WaterTileZ != 0)
            {
                // draw top quadbase
                qBase = GetQuadPixels(false, true, 0, quadBasePixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (16 + 752) - overH - baseT; y < (24 + 752) - overH - baseT; y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                }
                // draw top quadbase
                qBase = GetQuadPixels(false, true, 0, quadBasePixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x]; }
                }
                // draw top quadbase
                qBase = GetQuadPixels(false, true, 0, quadBasePixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)]; }
                }
                // draw top quadbase
                qBase = GetQuadPixels(false, true, 0, quadBasePixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (24 + 752) - overH - baseT; y < (32 + 752) - overH - baseT; y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW OVERHEAD TILES******/
            overH = tile.OverheadTileZ * 16; baseT = tile.BaseTileHeight * 16;
            if (tile.OverheadTileHeight == 0 && tile.OverheadTileZ != 0)
            {
                qBase = GetQuadPixels(false, false, 0, quadBasePixels, tile.SolidTopQuadrant ? 1 : 0);
                for (int y = (16 + 752) - overH - baseT; y < (24 + 752) - overH - baseT; y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((16 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                }
                qBase = GetQuadPixels(false, false, 0, quadBasePixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                {
                    for (int x = 0; x < 16; x++)
                    { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + x]; }
                }
                qBase = GetQuadPixels(false, false, 0, quadBasePixels, tile.SolidRightQuadrant ? 3 : 0);
                for (int y = (20 + 752) - overH - baseT; y < (28 + 752) - overH - baseT; y++)
                {
                    for (int x = 16; x < 32; x++)
                    { if (qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((20 + 752) - overH - baseT)) * 16 + (x - 16)]; }
                }
                qBase = GetQuadPixels(false, false, 0, quadBasePixels, tile.SolidBottomQuadrant ? 4 : 0);
                for (int y = (24 + 752) - overH - baseT; y < (32 + 752) - overH - baseT; y++)
                {
                    for (int x = 8; x < 24; x++)
                    { if (qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBase[(y - ((24 + 752) - overH - baseT)) * 16 + (x - 8)]; }
                }
            }
            /******DRAW OVERHEAD TILES THAT HAVE A HEIGHT > 0******/
            else if (tile.OverheadTileHeight != 0)
            {
                int b = 0;

                qBlock = GetQuadPixels(false, false, 1, quadBlockPixels, tile.SolidTopQuadrant ? 1 : 0);
                hqBlock = GetQuadPixels(false, false, 2, halfQuadBlockPixels, tile.SolidTopQuadrant ? 1 : 0);
                for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                {
                    hChange = (b * 32) - (b * 16);
                    for (int y = (0 + 752) - hChange - overH - baseT; y < (24 + 752) - hChange - overH - baseT; y++) // start 16 pixels above normal base start (ie. 240 - 16)
                    {
                        for (int x = 8; x < 24; x++)
                        { if (qBlock[(y - ((0 + 752) - hChange - overH - baseT)) * 16 + (x - 8)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((0 + 752) - hChange - overH - baseT)) * 16 + (x - 8)]; }
                    }
                }
                qBlock = GetQuadPixels(false, false, 1, quadBlockPixels, tile.SolidLeftQuadrant ? 2 : 0);
                hqBlock = GetQuadPixels(false, false, 2, halfQuadBlockPixels, tile.SolidLeftQuadrant ? 2 : 0);
                for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                {
                    hChange = (b * 32) - (b * 16);
                    for (int y = (4 + 752) - hChange - overH - baseT; y < (28 + 752) - hChange - overH - baseT; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        { if (qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + x] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + x]; }
                    }
                }
                qBlock = GetQuadPixels(false, false, 1, quadBlockPixels, tile.SolidRightQuadrant ? 3 : 0);
                hqBlock = GetQuadPixels(false, false, 2, halfQuadBlockPixels, tile.SolidRightQuadrant ? 3 : 0);
                for (b = 0; b < tile.OverheadTileHeight; b++)    // draw a block for each unit (ie. a height of 6 would draw 6 blocks on top each other)
                {
                    hChange = (b * 32) - (b * 16);
                    for (int y = (4 + 752) - hChange - overH - baseT; y < (28 + 752) - hChange - overH - baseT; y++)
                    {
                        for (int x = 16; x < 32; x++)
                        { if (qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + (x - 16)] != 0) tilePixels[y * 32 + x] = qBlock[(y - ((4 + 752) - hChange - overH - baseT)) * 16 + (x - 16)]; }
                    }
                }
                qBlock = GetQuadPixels(false, false, 1, quadBlockPixels, tile.SolidBottomQuadrant ? 4 : 0);
                hqBlock = GetQuadPixels(false, false, 2, halfQuadBlockPixels, tile.SolidBottomQuadrant ? 4 : 0);
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
            return tilePixels;
        }
        public int[] GetTilePixels(SolidityTile tile)
        {
            return GetTilePixels(tile, 255, false);
        }
        public int[] GetTilemapPixels(Tilemap map)
        {
            return GetTilemapPixels(map, false, false);
        }
        public int[] GetTilemapPixels(Tilemap map, bool inPriority1, bool inFlatMode)
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
                if (inPriority1 ? (tileNum != 0 && tiles[tileNum].P3ObjectOnTile) : tileNum != 0 )
                {
                    //tilePixels = physicalTiles[tileNum].PhysicalTilePixels;
                        tilePixels = GetTilePixels(tiles[tileNum], 255, inFlatMode); // ...draw tile

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
                                    if (inPriority1)
                                        pixels[xPixel + (yPixel * 1024)] = Color.FromArgb(128, 255, 255, 0).ToArgb();
                                    else
                                        pixels[xPixel + (yPixel * 1024)] = tilePixels[b + (a * 32)];
                                }
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
            return GetQuadPixels(isBase, isWater, type, src, 0);
        }
        private int[] GetQuadPixels(bool isBase, bool isWater, int type, int[][] src, int quadrant)
        {
        // type = 1 == stairs
        // type = 2 == +1/2 Z?

            int format = 0;

            int solid = 1 + 32; //this particular tile format is changed during compile
            int water = 2;
            int vine = 3;
            int stair = 4;
            int door = 5;
            int conveyerbelt = 6;
            int priority = 7;

            int shadow = 8;                  //gives tile a darker shade
            int priority3 = 16;              //lighter tile, for those tiles that give priority3 while standing on them
            int lightTransparentTile = 32;   //gives tile alpha, for "OverheadTileZ == 0" tiles. Not compatible with shadow or priority3 tiles
            //
            bool testIfP3 = tile.OverheadTileHeight == 0 && (tile.P3ObjectAboveEdge || tile.P3ObjectOnEdge || tile.P3ObjectOnTile);
            if (testIfP3)
            {
                format = priority3;
            }
            //
            if (tile.SolidTile)
            {
                if (isBase && tile.OverheadTileZ != 0) // it is a base tile and has something overhead
                {
                    format += shadow;
                    //
                    if (tile.SpecialTileFormat == 1)
                        format += vine;
                    else if (tile.SpecialTileFormat == 2)
                        format += water;
                    else if (tile.Door != 0)
                        format += door;
                    else if (tile.StairsDirection != 0)
                        format += stair;
                    else if (tile.ConveyorBeltNormal || tile.ConveyorBeltFast)
                        format += conveyerbelt;

                }
                else if (isBase && tile.OverheadTileZ == 0) // it is a base tile and nothing is overhead
                {
                    format = solid;
                }
                else if (!isBase && tile.OverheadTileZ != 0 && tile.OverheadTileHeight != 0) // it is not a base tile and has something overhead that has height
                    format = solid;
            }
            else if (!isBase && !isWater) // it is an overhead tile
            {
                // for some reason there are tiles that give P3 without P3 flag??
                if (tile.OverheadTileHeight == 0 || testIfP3)
                    format = lightTransparentTile;
                //
                if ( tile.OverheadTileHeight != 0)
                {
                    if (tile.SpecialTileFormat == 1)
                        format += vine;
                    else if (tile.ConveyorBeltNormal || tile.ConveyorBeltFast)
                        format += conveyerbelt;
                    else if (testIfP3 && tile.OverheadTileZ != 0)
                        format = lightTransparentTile;
                }
            }
            else if (isBase && !isWater && tile.SpecialTileFormat != 2 && tile.OverheadTileZ != 0) // it is a base tile and there is something overhead
            {
                if (tile.OverheadTileHeight != 0)
                    format += shadow;
                //
                if (tile.SpecialTileFormat == 1)
                    format += vine;
                else if (tile.Door != 0)
                    format += door;
                else if (tile.StairsDirection != 0)
                    format += stair;
                else if (tile.ConveyorBeltNormal || tile.ConveyorBeltFast)
                    format += conveyerbelt;
            }
            else if ((isWater || tile.SpecialTileFormat == 2) && tile.OverheadTileZ == 0 && tile.WaterTileZ == 0)
                format = water + shadow;
            else if (isWater || tile.SpecialTileFormat == 2)    //it is a water tile
            {
                /*shades (darkest to lightest)
                    water + shadow;
                    water;
                    water + shadow + priority3;
                    water + priority3;
                    water + lightTransparentTile;
                */
                if (testIfP3)
                {
                    if (isBase)
                        format = water;
                    else
                        format = lightTransparentTile + water; //water + shadow + priority3;
                }
                else
                {
                    if (isBase)
                        format = water + shadow;
                    else
                        format = water + priority3;
                }
            }
            else // it is a base tile and there is nothing overhead
            {
                if (tile.SpecialTileFormat == 1)
                    format += vine;
                else if (tile.SpecialTileFormat == 2)
                    format += water;
                else if (tile.Door != 0)
                    format += door;
                else if (tile.StairsDirection != 0)
                    format += stair;
                else if (tile.ConveyorBeltNormal || tile.ConveyorBeltFast)
                    format += conveyerbelt;
                else if (tile.SolidQuadrantFlag && quadrant != 0)
                {
                    switch (quadrant)
                    {
                        case 1:
                            if (tile.SolidTopQuadrant)
                                format = solid;
                            break;
                        case 2:
                            if (tile.SolidLeftQuadrant)
                                format = solid;
                            break;
                        case 3:
                            if (tile.SolidRightQuadrant)
                                format = solid;
                            break;
                        case 4:
                            if (tile.SolidBottomQuadrant)
                                format = solid;
                            break;
                    }
                }
            }

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
