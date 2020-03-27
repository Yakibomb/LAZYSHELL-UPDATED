using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LAZYSHELL
{
    public class SolidityTile
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables and accessors
        private int totalHeight; public int TotalHeight { get { return totalHeight; } set { totalHeight = value; } }
        //
        private byte baseTileHeight; public byte BaseTileHeight { get { return baseTileHeight; } set { baseTileHeight = value; } }
        private byte overheadTileHeight; public byte OverheadTileHeight { get { return overheadTileHeight; } set { overheadTileHeight = value; } }
        private byte overheadTileZ; public byte OverheadTileZ { get { return overheadTileZ; } set { overheadTileZ = value; } }
        private byte waterTileZ; public byte WaterTileZ { get { return waterTileZ; } set { waterTileZ = value; } }
        //
        private bool conveyorBeltFast; public bool ConveyorBeltFast { get { return conveyorBeltFast; } set { conveyorBeltFast = value; } }
        private bool conveyorBeltNormal; public bool ConveyorBeltNormal { get { return conveyorBeltNormal; } set { conveyorBeltNormal = value; } }
        private bool baseTileHeight_Half; public bool BaseTileHeight_Half { get { return baseTileHeight_Half; } set { baseTileHeight_Half = value; } }
        private bool solidTile; public bool SolidTile { get { return solidTile; } set { solidTile = value; } }
        //
        private bool solidEdgeNW; public bool SolidNWEdge { get { return solidEdgeNW; } set { solidEdgeNW = value; } }
        private bool solidEdgeNE; public bool SolidNEEdge { get { return solidEdgeNE; } set { solidEdgeNE = value; } }
        private bool solidEdgeSW; public bool SolidSWEdge { get { return solidEdgeSW; } set { solidEdgeSW = value; } }
        private bool solidEdgeSE; public bool SolidSEEdge { get { return solidEdgeSE; } set { solidEdgeSE = value; } }
        //
        private bool solidQuadW; public bool SolidLeftQuadrant { get { return solidQuadW; } set { solidQuadW = value; } }
        private bool solidQuadE; public bool SolidRightQuadrant { get { return solidQuadE; } set { solidQuadE = value; } }
        private bool solidQuadN; public bool SolidTopQuadrant { get { return solidQuadN; } set { solidQuadN = value; } }
        private bool solidQuadS; public bool SolidBottomQuadrant { get { return solidQuadS; } set { solidQuadS = value; } }
        //
        private bool p3objectOnEdge; public bool P3ObjectOnEdge { get { return p3objectOnEdge; } set { p3objectOnEdge = value; } }
        private bool p3objectAboveEdge; public bool P3ObjectAboveEdge { get { return p3objectAboveEdge; } set { p3objectAboveEdge = value; } }
        private bool p3objectOnTile; public bool P3ObjectOnTile { get { return p3objectOnTile; } set { p3objectOnTile = value; } }
        private bool solidQuadrantFlag; public bool SolidQuadrantFlag { get { return solidQuadrantFlag; } set { solidQuadrantFlag = value; } }
        //
        private byte converyorBeltDirection; public byte ConveryorBeltDirection { get { return converyorBeltDirection; } set { converyorBeltDirection = value; } }
        private byte stairsDirection; public byte StairsDirection { get { return stairsDirection; } set { stairsDirection = value; } }
        private byte specialTileFormat; public byte SpecialTileFormat { get { return specialTileFormat; } set { specialTileFormat = value; } }
        //
        private byte door; public byte Door { get { return door; } set { door = value; } }
        private bool b5b0; public bool B5b0 { get { return b5b0; } set { b5b0 = value; } }
        private bool b5b1; public bool B5b1 { get { return b5b1; } set { b5b1 = value; } }
        private bool b5b2; public bool B5b2 { get { return b5b2; } set { b5b2 = value; } }
        private bool b5b3; public bool B5b3 { get { return b5b3; } set { b5b3 = value; } }
        private bool b5b4; public bool B5b4 { get { return b5b4; } set { b5b4 = value; } }
        // constructor
        public SolidityTile(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 6) + 0x3DC000;
            //
            byte temp = rom[offset++];
            baseTileHeight = (byte)(temp & 0x0F);
            conveyorBeltFast = (temp & 0x10) == 0x10;
            conveyorBeltNormal = (temp & 0x20) == 0x20;
            baseTileHeight_Half = (temp & 0x40) == 0x40;
            solidTile = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            overheadTileZ = (byte)(temp & 0x0F);
            solidEdgeNW = (temp & 0x10) == 0x10;
            solidEdgeNE = (temp & 0x20) == 0x20;
            solidEdgeSW = (temp & 0x40) == 0x40;
            solidEdgeSE = (temp & 0x80) == 0x80;
            converyorBeltDirection = (byte)((temp >> 4) & 7);
            //
            temp = rom[offset++];
            overheadTileHeight = (byte)(temp & 0x0F);
            switch (temp & 0xF0)
            {
                case 0x00: stairsDirection = 0; break;
                case 0x90: stairsDirection = 1; break;
                case 0xB0: stairsDirection = 2; break;
                default: stairsDirection = 0; break;
            }
            //
            temp = rom[offset++];
            solidQuadN = (temp & 0x01) == 0x01;
            solidQuadW = (temp & 0x02) == 0x02;
            solidQuadE = (temp & 0x04) == 0x04;
            solidQuadS = (temp & 0x08) == 0x08;
            p3objectOnEdge = (temp & 0x10) == 0x10;
            p3objectAboveEdge = (temp & 0x20) == 0x20;
            p3objectOnTile = (temp & 0x40) == 0x40;
            solidQuadrantFlag = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            waterTileZ = (byte)(temp & 0x0F);
            if ((temp & 0xF0) == 0x10)
                specialTileFormat = 1;
            else if ((temp & 0xF0) == 0x80)
                specialTileFormat = 2;
            else
                specialTileFormat = 0;
            //
            totalHeight = (baseTileHeight + overheadTileZ + overheadTileHeight + waterTileZ) * 16;
            if (baseTileHeight_Half)
                totalHeight += 8;
            if (stairsDirection > 0)
                totalHeight += 32;
            //
            door = (byte)((rom[offset] & 0xE0) >> 5);
            b5b0 = (rom[offset] & 0x01) == 0x01;
            b5b1 = (rom[offset] & 0x02) == 0x02;
            b5b2 = (rom[offset] & 0x04) == 0x04;
            b5b3 = (rom[offset] & 0x08) == 0x08;
            b5b4 = (rom[offset] & 0x10) == 0x10;
        }
        public void Assemble()
        {
            int offset = (index * 6) + 0x3DC000;
            //
            rom[offset] = baseTileHeight;
            Bits.SetBit(rom, offset, 4, conveyorBeltFast);
            Bits.SetBit(rom, offset, 5, conveyorBeltNormal);
            Bits.SetBit(rom, offset, 6, baseTileHeight_Half);
            Bits.SetBit(rom, offset++, 7, solidTile);
            //
            rom[offset] = overheadTileZ;
            Bits.SetBit(rom, offset, 4, solidEdgeNW);
            Bits.SetBit(rom, offset, 5, solidEdgeNE);
            Bits.SetBit(rom, offset, 6, solidEdgeSW);
            Bits.SetBit(rom, offset, 7, solidEdgeSE);
            if (conveyorBeltFast || conveyorBeltNormal)
                rom[offset] = (byte)(converyorBeltDirection << 4);
            offset++;
            rom[offset] = overheadTileHeight;
            switch (stairsDirection)
            {
                case 1: rom[offset] |= 0x90; break;
                case 2: rom[offset] |= 0xB0; break;
            }
            offset++;
            Bits.SetBit(rom, offset, 0, solidQuadN);
            Bits.SetBit(rom, offset, 1, solidQuadW);
            Bits.SetBit(rom, offset, 2, solidQuadE);
            Bits.SetBit(rom, offset, 3, solidQuadS);
            Bits.SetBit(rom, offset, 4, p3objectOnEdge);
            Bits.SetBit(rom, offset, 5, p3objectAboveEdge);
            Bits.SetBit(rom, offset, 6, p3objectOnTile);
            Bits.SetBit(rom, offset++, 7, solidQuadrantFlag);
            //
            rom[offset] = waterTileZ;
            if (specialTileFormat == 1)
                rom[offset] |= 0x10;
            else if (specialTileFormat == 2)
                rom[offset] |= 0x80;
            offset++;
            rom[offset] = (byte)(door << 5);
            Bits.SetBit(rom, offset, 0, b5b0);
            Bits.SetBit(rom, offset, 1, b5b1);
            Bits.SetBit(rom, offset, 2, b5b2);
            Bits.SetBit(rom, offset, 3, b5b3);
            Bits.SetBit(rom, offset, 4, b5b4);
        }
    }
}
