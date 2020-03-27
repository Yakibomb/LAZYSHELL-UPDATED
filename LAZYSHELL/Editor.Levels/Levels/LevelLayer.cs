using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelLayer
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables
        private byte levelMap; public byte LevelMap { get { return levelMap; } set { levelMap = value; } }
        private byte messageBox; public byte MessageBox { get { return messageBox; } set { messageBox = value; } }
        private byte prioritySet; public byte PrioritySet { get { return prioritySet; } set { prioritySet = value; } }
        // mask
        private byte maskLowX; public byte MaskLowX { get { return maskLowX; } set { maskLowX = value; } }
        private byte maskLowY; public byte MaskLowY { get { return maskLowY; } set { maskLowY = value; } }
        private byte maskHighX; public byte MaskHighX { get { return maskHighX; } set { maskHighX = value; } }
        private byte maskHighY; public byte MaskHighY { get { return maskHighY; } set { maskHighY = value; } }
        private bool maskLock; public bool MaskLock { get { return maskLock; } set { maskLock = value; } }
        // negative shifting
        private byte xNegL2; public byte XNegL2 { get { return xNegL2; } set { xNegL2 = value; } }
        private byte yNegL2; public byte YNegL2 { get { return yNegL2; } set { yNegL2 = value; } }
        private byte xNegL3; public byte XNegL3 { get { return xNegL3; } set { xNegL3 = value; } }
        private byte yNegL3; public byte YNegL3 { get { return yNegL3; } set { yNegL3 = value; } }
        // layer scrolling
        private bool infiniteScrolling; public bool InfiniteScrolling { get { return infiniteScrolling; } set { infiniteScrolling = value; } }
        private bool scrollWrapL1_HZ; public bool ScrollWrapL1_HZ { get { return scrollWrapL1_HZ; } set { scrollWrapL1_HZ = value; } }
        private bool scrollWrapL1_VT; public bool ScrollWrapL1_VT { get { return scrollWrapL1_VT; } set { scrollWrapL1_VT = value; } }
        private bool scrollWrapL2_HZ; public bool ScrollWrapL2_HZ { get { return scrollWrapL2_HZ; } set { scrollWrapL2_HZ = value; } }
        private bool scrollWrapL2_VT; public bool ScrollWrapL2_VT { get { return scrollWrapL2_VT; } set { scrollWrapL2_VT = value; } }
        private bool scrollWrapL3_HZ; public bool ScrollWrapL3_HZ { get { return scrollWrapL3_HZ; } set { scrollWrapL3_HZ = value; } }
        private bool scrollWrapL3_VT; public bool ScrollWrapL3_VT { get { return scrollWrapL3_VT; } set { scrollWrapL3_VT = value; } }
        private byte scrollDirectionL2; public byte ScrollDirectionL2 { get { return scrollDirectionL2; } set { scrollDirectionL2 = value; } }
        private byte scrollDirectionL3; public byte ScrollDirectionL3 { get { return scrollDirectionL3; } set { scrollDirectionL3 = value; } }
        private byte scrollSpeedL2; public byte ScrollSpeedL2 { get { return scrollSpeedL2; } set { scrollSpeedL2 = value; } }
        private byte scrollSpeedL3; public byte ScrollSpeedL3 { get { return scrollSpeedL3; } set { scrollSpeedL3 = value; } }
        private bool scrollL2Bit7; public bool ScrollL2Bit7 { get { return scrollL2Bit7; } set { scrollL2Bit7 = value; } }
        private bool scrollL3Bit7; public bool ScrollL3Bit7 { get { return scrollL3Bit7; } set { scrollL3Bit7 = value; } }
        //
        private bool culexA; public bool CulexA { get { return culexA; } set { culexA = value; } }
        private bool culexB; public bool CulexB { get { return culexB; } set { culexB = value; } }
        // layer synchronization
        private byte syncL2_VT; public byte SyncL2_VT { get { return syncL2_VT; } set { syncL2_VT = value; } }
        private byte syncL2_HZ; public byte SyncL2_HZ { get { return syncL2_HZ; } set { syncL2_HZ = value; } }
        private byte syncL3_VT; public byte SyncL3_VT { get { return syncL3_VT; } set { syncL3_VT = value; } }
        private byte syncL3_HZ; public byte SyncL3_HZ { get { return syncL3_HZ; } set { syncL3_HZ = value; } }
        // animation effects
        private bool ripplingWater; public bool RipplingWater { get { return ripplingWater; } set { ripplingWater = value; } }
        private byte effectsL3; public byte EffectsL3 { get { return effectsL3; } set { effectsL3 = value; } }
        private byte effectsNPC; public byte EffectsNPC { get { return effectsNPC; } set { effectsNPC = value; } }
        // constructor
        public LevelLayer(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = (index * 18) + 0x1D0040; offset++;
            byte temp = rom[offset++];
            messageBox = temp != 0xFE ? (byte)((temp >> 1) + 1) : (byte)0;
            //
            temp = rom[offset++];
            maskLock = (temp & 0x80) == 0x80;
            maskLowX = (byte)(temp & 0x3F);
            maskLowY = (byte)(rom[offset++] & 0x3F);
            maskHighX = (byte)(rom[offset++] & 0x3F);
            maskHighY = (byte)(rom[offset++] & 0x3F);
            //
            xNegL2 = rom[offset++];
            yNegL2 = rom[offset++];
            xNegL3 = rom[offset++];
            //
            temp = rom[offset++];
            infiniteScrolling = (temp & 0x80) == 0x80;
            yNegL3 = (byte)(temp & 0x7F);
            //
            temp = rom[offset++];
            scrollWrapL1_HZ = (temp & 0x01) == 0x01;
            scrollWrapL1_VT = (temp & 0x02) == 0x02;
            culexA = (temp & 0x04) == 0x04;
            scrollWrapL2_HZ = (temp & 0x08) == 0x08;
            scrollWrapL2_VT = (temp & 0x10) == 0x10;
            culexB = (temp & 0x20) == 0x20;
            scrollWrapL3_HZ = (temp & 0x40) == 0x40;
            scrollWrapL3_VT = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            if ((temp & 0x03) == 0x00) syncL2_HZ = 0;
            else if ((temp & 0x03) == 0x01) syncL2_HZ = 3;
            else if ((temp & 0x03) == 0x02) syncL2_HZ = 1;
            else if ((temp & 0x03) == 0x03) syncL2_HZ = 2;
            if ((temp & 0x0C) == 0x00) syncL2_VT = 0;
            else if ((temp & 0x0C) == 0x04) syncL2_VT = 3;
            else if ((temp & 0x0C) == 0x08) syncL2_VT = 1;
            else if ((temp & 0x0C) == 0x0C) syncL2_VT = 2;
            if ((temp & 0x30) == 0x00) syncL3_HZ = 0;
            else if ((temp & 0x30) == 0x10) syncL3_HZ = 3;
            else if ((temp & 0x30) == 0x20) syncL3_HZ = 1;
            else if ((temp & 0x30) == 0x30) syncL3_HZ = 2;
            if ((temp & 0xC0) == 0x00) syncL3_VT = 0;
            else if ((temp & 0xC0) == 0x40) syncL3_VT = 3;
            else if ((temp & 0xC0) == 0x80) syncL3_VT = 1;
            else if ((temp & 0xC0) == 0xC0) syncL3_VT = 2;
            //
            temp = rom[offset++];
            switch (temp & 0x38)
            {
                case 0x00: scrollDirectionL2 = 0; break;
                case 0x08: scrollDirectionL2 = 1; break;
                case 0x10: scrollDirectionL2 = 2; break;
                case 0x18: scrollDirectionL2 = 3; break;
                case 0x20: scrollDirectionL2 = 4; break;
                case 0x28: scrollDirectionL2 = 5; break;
                case 0x30: scrollDirectionL2 = 6; break;
                case 0x38: scrollDirectionL2 = 7; break;
            }
            switch (temp & 0x07)
            {
                case 0x00: scrollSpeedL2 = 0; break;
                case 0x01: scrollSpeedL2 = 4; break;
                case 0x02: scrollSpeedL2 = 2; break;
                case 0x03: scrollSpeedL2 = 1; break;
                case 0x04: scrollSpeedL2 = 3; break;
                case 0x05: scrollSpeedL2 = 5; break;
                case 0x06: scrollSpeedL2 = 5; break;
                case 0x07: scrollSpeedL2 = 6; break;
            }
            scrollL2Bit7 = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            switch (temp & 0x38)
            {
                case 0x00: scrollDirectionL3 = 0; break;
                case 0x08: scrollDirectionL3 = 1; break;
                case 0x10: scrollDirectionL3 = 2; break;
                case 0x18: scrollDirectionL3 = 3; break;
                case 0x20: scrollDirectionL3 = 4; break;
                case 0x28: scrollDirectionL3 = 5; break;
                case 0x30: scrollDirectionL3 = 6; break;
                case 0x38: scrollDirectionL3 = 7; break;
            }
            switch (temp & 0x07)
            {
                case 0x00: scrollSpeedL3 = 0; break;
                case 0x01: scrollSpeedL3 = 4; break;
                case 0x02: scrollSpeedL3 = 2; break;
                case 0x03: scrollSpeedL3 = 1; break;
                case 0x04: scrollSpeedL3 = 3; break;
                case 0x05: scrollSpeedL3 = 5; break;
                case 0x06: scrollSpeedL3 = 5; break;
                case 0x07: scrollSpeedL3 = 6; break;
            }
            scrollL3Bit7 = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            ripplingWater = (temp & 0x10) == 0x10;
            prioritySet = (byte)(temp & 0x0F);
            //
            temp = rom[offset++];
            switch (temp)
            {
                case 0x00: effectsL3 = 0; break;
                case 0x01: effectsL3 = 1; break;
                case 0x02: effectsL3 = 2; break;
                case 0x03: effectsL3 = 3; break;
                case 0x05: effectsL3 = 4; break;
                case 0x06: effectsL3 = 5; break;
                case 0x07: effectsL3 = 6; break;
                case 0x08: effectsL3 = 7; break;
                case 0x09: effectsL3 = 8; break;
                case 0x0A: effectsL3 = 9; break;
                case 0x0B: effectsL3 = 10; break;
                case 0x0C: effectsL3 = 11; break;
                case 0x0D: effectsL3 = 12; break;
                case 0x0E: effectsL3 = 13; break;
                case 0x0F: effectsL3 = 14; break;
                case 0x10: effectsL3 = 15; break;
                case 0x11: effectsL3 = 16; break;
                case 0x12: effectsL3 = 17; break;
                case 0x14: effectsL3 = 18; break;
                case 0x15: effectsL3 = 19; break;
                case 0x16: effectsL3 = 20; break;
                case 0x17: effectsL3 = 21; break;
                case 0x18: effectsL3 = 22; break;
                default: effectsL3 = 0; break;
            }
            //
            temp = rom[offset++];
            switch (temp)
            {
                case 0x00: effectsNPC = 0; break;
                case 0x05: effectsNPC = 1; break;
                case 0x06: effectsNPC = 2; break;
                case 0x07: effectsNPC = 3; break;
                case 0x0A: effectsNPC = 4; break;
                case 0x0B: effectsNPC = 5; break;
                case 0x0C: effectsNPC = 6; break;
                case 0x0D: effectsNPC = 7; break;
                case 0x0F: effectsNPC = 8; break;
                case 0x10: effectsNPC = 9; break;
                case 0x12: effectsNPC = 10; break;
                case 0x13: effectsNPC = 11; break;
                case 0x15: effectsNPC = 12; break;
                case 0x16: effectsNPC = 13; break;
                case 0x17: effectsNPC = 14; break;
                case 0x18: effectsNPC = 15; break;
                case 0x19: effectsNPC = 16; break;
                case 0x1A: effectsNPC = 17; break;
                case 0x1B: effectsNPC = 18; break;
                case 0x1D: effectsNPC = 19; break;
                case 0x1E: effectsNPC = 20; break;
                case 0x1F: effectsNPC = 21; break;
                case 0x20: effectsNPC = 22; break;
                case 0x21: effectsNPC = 23; break;
                case 0x22: effectsNPC = 24; break;
                default: effectsNPC = 0; break;
            }
        }
        public void Assemble()
        {
            int offset = 0;
            offset = (index * 18) + 0x1D0040; offset++;
            rom[offset] = messageBox != 0 ? (byte)((messageBox - 1) << 1) : (byte)0xFE;
            offset++;
            rom[offset] = maskLowX;
            Bits.SetBit(rom, offset, 7, maskLock); offset++;
            rom[offset] = maskLowY; offset++;
            rom[offset] = maskHighX; offset++;
            rom[offset] = maskHighY; offset++;
            rom[offset] = xNegL2; offset++;
            rom[offset] = yNegL2; offset++;
            rom[offset] = xNegL3; offset++;
            rom[offset] = yNegL3;
            Bits.SetBit(rom, offset, 7, infiniteScrolling); offset++;
            Bits.SetBit(rom, offset, 0, scrollWrapL1_HZ);
            Bits.SetBit(rom, offset, 1, scrollWrapL1_VT);
            Bits.SetBit(rom, offset, 2, culexA);
            Bits.SetBit(rom, offset, 3, scrollWrapL2_HZ);
            Bits.SetBit(rom, offset, 4, scrollWrapL2_VT);
            Bits.SetBit(rom, offset, 5, culexB);
            Bits.SetBit(rom, offset, 6, scrollWrapL3_HZ);
            Bits.SetBit(rom, offset, 7, scrollWrapL3_VT);
            offset++;
            rom[offset] = 0;
            switch (syncL2_HZ)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0x03, false); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x02, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0x03, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x01, true); break;
            }
            switch (syncL2_VT)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0x0C, false); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x08, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0x0C, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x04, true); break;
            }
            switch (syncL3_HZ)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0x30, false); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x20, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0x30, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x10, true); break;
            }
            switch (syncL3_VT)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0xC0, false); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x80, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0xC0, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x40, true); break;
            }
            offset++;
            switch (scrollDirectionL2)
            {
                case 0: rom[offset] = 0x00; break;
                case 1: rom[offset] = 0x08; break;
                case 2: rom[offset] = 0x10; break;
                case 3: rom[offset] = 0x18; break;
                case 4: rom[offset] = 0x20; break;
                case 5: rom[offset] = 0x28; break;
                case 6: rom[offset] = 0x30; break;
                case 7: rom[offset] = 0x38; break;
            }
            switch (scrollSpeedL2)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0x00, true); break;
                case 4: Bits.SetBitsByByte(rom, offset, 0x01, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0x02, true); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x03, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x04, true); break;
                case 5: Bits.SetBitsByByte(rom, offset, 0x05, true); break;
                case 6: Bits.SetBitsByByte(rom, offset, 0x07, true); break;
            }
            Bits.SetBit(rom, offset, 7, scrollL2Bit7);
            offset++;
            switch (scrollDirectionL3)
            {
                case 0: rom[offset] = 0x00; break;
                case 1: rom[offset] = 0x08; break;
                case 2: rom[offset] = 0x10; break;
                case 3: rom[offset] = 0x18; break;
                case 4: rom[offset] = 0x20; break;
                case 5: rom[offset] = 0x28; break;
                case 6: rom[offset] = 0x30; break;
                case 7: rom[offset] = 0x38; break;
            }
            switch (scrollSpeedL3)
            {
                case 0: Bits.SetBitsByByte(rom, offset, 0x00, true); break;
                case 4: Bits.SetBitsByByte(rom, offset, 0x01, true); break;
                case 2: Bits.SetBitsByByte(rom, offset, 0x02, true); break;
                case 1: Bits.SetBitsByByte(rom, offset, 0x03, true); break;
                case 3: Bits.SetBitsByByte(rom, offset, 0x04, true); break;
                case 5: Bits.SetBitsByByte(rom, offset, 0x05, true); break;
                case 6: Bits.SetBitsByByte(rom, offset, 0x07, true); break;
            }
            Bits.SetBit(rom, offset, 7, scrollL3Bit7);
            offset++;
            rom[offset] = 0;
            Bits.SetBit(rom, offset, 4, ripplingWater);
            Bits.SetBitsByByte(rom, offset, prioritySet, true);
            offset++;
            switch (effectsL3)
            {
                case 0: rom[offset] = 0x00; break;
                case 1: rom[offset] = 0x01; break;
                case 2: rom[offset] = 0x02; break;
                case 3: rom[offset] = 0x03; break;
                case 4: rom[offset] = 0x05; break;
                case 5: rom[offset] = 0x06; break;
                case 6: rom[offset] = 0x07; break;
                case 7: rom[offset] = 0x08; break;
                case 8: rom[offset] = 0x09; break;
                case 9: rom[offset] = 0x0A; break;
                case 10: rom[offset] = 0x0B; break;
                case 11: rom[offset] = 0x0C; break;
                case 12: rom[offset] = 0x0D; break;
                case 13: rom[offset] = 0x0E; break;
                case 14: rom[offset] = 0x0F; break;
                case 15: rom[offset] = 0x10; break;
                case 16: rom[offset] = 0x11; break;
                case 17: rom[offset] = 0x12; break;
                case 18: rom[offset] = 0x14; break;
                case 19: rom[offset] = 0x15; break;
                case 20: rom[offset] = 0x16; break;
                case 21: rom[offset] = 0x17; break;
                case 22: rom[offset] = 0x18; break;
            }
            offset++;
            switch (effectsNPC)
            {
                case 0: rom[offset] = 0x00; break;
                case 1: rom[offset] = 0x05; break;
                case 2: rom[offset] = 0x06; break;
                case 3: rom[offset] = 0x07; break;
                case 4: rom[offset] = 0x0A; break;
                case 5: rom[offset] = 0x0B; break;
                case 6: rom[offset] = 0x0C; break;
                case 7: rom[offset] = 0x0D; break;
                case 8: rom[offset] = 0x0F; break;
                case 9: rom[offset] = 0x10; break;
                case 10: rom[offset] = 0x12; break;
                case 11: rom[offset] = 0x13; break;
                case 12: rom[offset] = 0x15; break;
                case 13: rom[offset] = 0x16; break;
                case 14: rom[offset] = 0x17; break;
                case 15: rom[offset] = 0x18; break;
                case 16: rom[offset] = 0x19; break;
                case 17: rom[offset] = 0x1A; break;
                case 18: rom[offset] = 0x1B; break;
                case 19: rom[offset] = 0x1D; break;
                case 20: rom[offset] = 0x1E; break;
                case 21: rom[offset] = 0x1F; break;
                case 22: rom[offset] = 0x20; break;
                case 23: rom[offset] = 0x21; break;
                case 24: rom[offset] = 0x22; break;
            }
        }
        // universal functions
        public void Clear()
        {
            int offset = 0;
            offset = (index * 18) + 0x1D0040; offset++;
            this.messageBox = 0;
            this.maskLock = false;
            this.maskLowX = 0;
            this.maskLowY = 0;
            this.maskHighX = 63;
            this.maskHighY = 63;
            this.xNegL2 = 0;
            this.yNegL2 = 0;
            this.xNegL3 = 0;
            this.infiniteScrolling = false;
            this.yNegL3 = 0;
            this.scrollWrapL1_HZ = false;
            this.scrollWrapL1_VT = false;
            this.culexA = false;
            this.culexB = false;
            this.scrollWrapL2_HZ = false;
            this.scrollWrapL2_VT = false;
            this.scrollWrapL3_HZ = false;
            this.scrollWrapL3_VT = false;
            this.syncL2_HZ = 0;
            this.syncL2_VT = 0;
            this.syncL3_HZ = 0;
            this.syncL3_VT = 0;
            this.scrollDirectionL2 = 0;
            this.scrollSpeedL2 = 0;
            this.scrollDirectionL3 = 0;
            this.scrollSpeedL3 = 0;
            this.ripplingWater = false;
            this.prioritySet = 0;
            this.effectsL3 = 0;
            this.effectsNPC = 0;
        }
    }
}
