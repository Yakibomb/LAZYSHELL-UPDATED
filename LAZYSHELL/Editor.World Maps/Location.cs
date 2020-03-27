using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class Location
    {
        #region variables
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } }
        // class variables and accessors
        private char[] name; public char[] Name { get { return name; } set { name = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        //
        private byte showCheckBit; public byte ShowCheckBit { get { return showCheckBit; } set { showCheckBit = value; } }
        private ushort showCheckAddress; public ushort ShowCheckAddress { get { return showCheckAddress; } set { showCheckAddress = value; } }
        private bool goLocation; public bool GoLocation { get { return goLocation; } set { goLocation = value; } }
        private ushort runEvent; public ushort RunEvent { get { return runEvent; } set { runEvent = value; } }
        private byte whichLocationCheckBit; public byte WhichLocationCheckBit { get { return whichLocationCheckBit; } set { whichLocationCheckBit = value; } }
        private ushort whichLocationCheckAddress; public ushort WhichLocationCheckAddress { get { return whichLocationCheckAddress; } set { whichLocationCheckAddress = value; } }
        private byte goLocationA; public byte GoLocationA { get { return goLocationA; } set { goLocationA = value; } }
        private byte goLocationB; public byte GoLocationB { get { return goLocationB; } set { goLocationB = value; } }
        //
        private bool enabledToEast; public bool EnabledToEast { get { return enabledToEast; } set { enabledToEast = value; } }
        private bool enabledToSouth; public bool EnabledToSouth { get { return enabledToSouth; } set { enabledToSouth = value; } }
        private bool enabledToWest; public bool EnabledToWest { get { return enabledToWest; } set { enabledToWest = value; } }
        private bool enabledToNorth; public bool EnabledToNorth { get { return enabledToNorth; } set { enabledToNorth = value; } }
        //
        private byte checkBitToEast; public byte CheckBitToEast { get { return checkBitToEast; } set { checkBitToEast = value; } }
        private byte checkBitToSouth; public byte CheckBitToSouth { get { return checkBitToSouth; } set { checkBitToSouth = value; } }
        private byte checkBitToWest; public byte CheckBitToWest { get { return checkBitToWest; } set { checkBitToWest = value; } }
        private byte checkBitToNorth; public byte CheckBitToNorth { get { return checkBitToNorth; } set { checkBitToNorth = value; } }
        private ushort checkAddressToEast; public ushort CheckAddressToEast { get { return checkAddressToEast; } set { checkAddressToEast = value; } }
        private ushort checkAddressToSouth; public ushort CheckAddressToSouth { get { return checkAddressToSouth; } set { checkAddressToSouth = value; } }
        private ushort checkAddressToWest; public ushort CheckAddressToWest { get { return checkAddressToWest; } set { checkAddressToWest = value; } }
        private ushort checkAddressToNorth; public ushort CheckAddressToNorth { get { return checkAddressToNorth; } set { checkAddressToNorth = value; } }
        private byte locationToEast; public byte LocationToEast { get { return locationToEast; } set { locationToEast = value; } }
        private byte locationToSouth; public byte LocationToSouth { get { return locationToSouth; } set { locationToSouth = value; } }
        private byte locationToWest; public byte LocationToWest { get { return locationToWest; } set { locationToWest = value; } }
        private byte locationToNorth; public byte LocationToNorth { get { return locationToNorth; } set { locationToNorth = value; } }
        #endregion
        // constructor
        public Location(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = index * 16 + 0x3EF830;
            x = (byte)rom[offset++];
            y = (byte)rom[offset++];
            showCheckBit = (byte)(rom[offset] & 0x07);
            showCheckAddress = (ushort)(((Bits.GetShort(rom, offset++) & 0x1FF) >> 3) + 0x7045);
            goLocation = (rom[offset++] & 0x40) == 0x40;
            if (!goLocation)
            {
                runEvent = Bits.GetShort(rom, offset);
                offset += 4;
            }
            else
            {
                whichLocationCheckBit = (byte)(rom[offset] & 0x07);
                whichLocationCheckAddress = (ushort)(((Bits.GetShort(rom, offset) & 0x1FF) >> 3) + 0x7045); offset += 2;
                goLocationA = rom[offset++];
                goLocationB = rom[offset++];
            }
            if (Bits.GetShort(rom, offset) == 0xFFFF)
            {
                enabledToEast = false;
                checkAddressToEast = 0x7045;
                offset += 2;
            }
            else
            {
                enabledToEast = true;
                checkBitToEast = (byte)(rom[offset] & 0x07);
                checkAddressToEast = (ushort)(((Bits.GetShort(rom, offset) & 0x1FF) >> 3) + 0x7045); offset++;
                locationToEast = (byte)(rom[offset] >> 1); offset++;
            }
            if (Bits.GetShort(rom, offset) == 0xFFFF)
            {
                enabledToSouth = false;
                checkAddressToSouth = 0x7045;
                offset += 2;
            }
            else
            {
                enabledToSouth = true;
                checkBitToSouth = (byte)(rom[offset] & 0x07);
                checkAddressToSouth = (ushort)(((Bits.GetShort(rom, offset++) & 0x1FF) >> 3) + 0x7045);
                locationToSouth = (byte)(rom[offset++] >> 1);
            }
            if (Bits.GetShort(rom, offset) == 0xFFFF)
            {
                enabledToWest = false;
                checkAddressToWest = 0x7045;
                offset += 2;
            }
            else
            {
                enabledToWest = true;
                checkBitToWest = (byte)(rom[offset] & 0x07);
                checkAddressToWest = (ushort)(((Bits.GetShort(rom, offset++) & 0x1FF) >> 3) + 0x7045);
                locationToWest = (byte)(rom[offset++] >> 1);
            }
            if (Bits.GetShort(rom, offset) == 0xFFFF)
            {
                enabledToNorth = false;
                checkAddressToNorth = 0x7045;
                offset += 2;
            }
            else
            {
                enabledToNorth = true;
                checkBitToNorth = (byte)(rom[offset] & 0x07);
                checkAddressToNorth = (ushort)(((Bits.GetShort(rom, offset++) & 0x1FF) >> 3) + 0x7045);
                locationToNorth = (byte)(rom[offset] >> 1);
            }
            //
            int pointer = Bits.GetShort(rom, index * 2 + 0x3EFD00);
            offset = pointer + 0x3EFD80;
            ArrayList temp = new ArrayList();
            for (int i = 0; rom[offset] != 0x06 && rom[offset] != 0x00; i++)
                temp.Add((char)rom[offset++]);
            name = new char[temp.Count];
            int a = 0;
            foreach (char c in temp)
                name[a++] = c;
        }
        public void Assemble()
        {
            int offset = index * 16 + 0x3EF830;
            rom[offset++] = x;
            rom[offset++] = y;
            Bits.SetShort(rom, offset, (ushort)((showCheckAddress - 0x7045) << 3));
            rom[offset++] |= showCheckBit;
            Bits.SetBit(rom, offset++, 6, goLocation);
            if (!goLocation)
            {
                Bits.SetShort(rom, offset, runEvent); offset += 2;
                Bits.SetShort(rom, offset, 0xFFFF); offset += 2;
            }
            else
            {
                Bits.SetShort(rom, offset, (ushort)((whichLocationCheckAddress - 0x7045) << 3));
                rom[offset] |= whichLocationCheckBit; offset += 2;
                rom[offset++] = goLocationA;
                rom[offset++] = goLocationB;
            }
            if (!enabledToEast)
            {
                Bits.SetShort(rom, offset, 0xFFFF);
                offset += 2;
            }
            else
            {
                Bits.SetShort(rom, offset, (ushort)((checkAddressToEast - 0x7045) << 3));
                rom[offset++] |= checkBitToEast;
                rom[offset++] |= (byte)(locationToEast << 1);
            }
            if (!enabledToSouth)
            {
                Bits.SetShort(rom, offset, 0xFFFF);
                offset += 2;
            }
            else
            {
                Bits.SetShort(rom, offset, (ushort)((checkAddressToSouth - 0x7045) << 3));
                rom[offset++] |= checkBitToSouth;
                rom[offset++] |= (byte)(locationToSouth << 1);
            }
            if (!enabledToWest)
            {
                Bits.SetShort(rom, offset, 0xFFFF);
                offset += 2;
            }
            else
            {
                Bits.SetShort(rom, offset, (ushort)((checkAddressToWest - 0x7045) << 3));
                rom[offset++] |= checkBitToWest;
                rom[offset++] |= (byte)(locationToWest << 1);
            }
            if (!enabledToNorth)
            {
                Bits.SetShort(rom, offset, 0xFFFF);
                offset += 2;
            }
            else
            {
                Bits.SetShort(rom, offset, (ushort)((checkAddressToNorth - 0x7045) << 3));
                rom[offset++] |= checkBitToNorth;
                rom[offset++] |= (byte)(locationToNorth << 1);
            }
        }
        // universal functions
        public void Clear()
        {
            x = 0;
            y = 0;
            showCheckBit = 0;
            showCheckAddress = 0x7045;
            goLocation = false;
            runEvent = 0;
            whichLocationCheckAddress = 0x7045;
            whichLocationCheckBit = 0;
            goLocationA = 0;
            goLocationB = 0;
            enabledToEast = false;
            enabledToSouth = false;
            enabledToWest = false;
            enabledToNorth = false;
            name = new char[0];
        }
        public override string ToString()
        {
            return Do.RawToASCII(name, Lists.Keystrokes);
        }
    }
}
