using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelNPCs
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables
        private int index;
        private byte partition;
        private int startingOffset;
        private List<NPC> npcs = new List<NPC>();
        // external selectors
        private NPC npc;
        private int currentNPC = 0;
        private int selectedNPC;
        #region public accessors
        public int StartingOffset { get { return this.startingOffset; } }
        public List<NPC> Npcs { get { return npcs; } }
        public NPC Npc { get { return npc; } }
        public int Index { get { return index; } set { index = value; } }
        public int Count { get { return npcs.Count; } }
        public int CountAll
        {
            get
            {
                int count = 0;
                foreach (NPC npc in npcs)
                {
                    count++;
                    count += npc.Clones.Count;
                }
                return count;
            }
        }
        public int CloneCount { get { return npc.Count; } }
        // selectors
        public int CurrentNPC
        {
            get
            {
                return this.currentNPC;
            }
            set
            {
                if (this.npcs.Count > value)
                {
                    npc = (NPC)npcs[value];
                    this.currentNPC = value;
                }
            }
        }
        public int SelectedNPC { get { return this.selectedNPC; } set { selectedNPC = value; } }
        public int CurrentClone { get { return npc.CurrentClone; } set { npc.CurrentClone = value; } }
        public int SelectedClone { get { return npc.SelectedClone; } set { npc.SelectedClone = value; } }
        public bool IsCloneSelected { get { return npc.IsCloneSelected; } set { npc.IsCloneSelected = value; } }
        // NPC properties
        public byte Partition { get { return partition; } set { partition = value; } }
        public byte CloneAmount { get { return npc.CloneAmount; } set { npc.CloneAmount = value; } }
        public byte EngageType { get { return npc.EngageType; } set { npc.EngageType = value; } }
        public byte SpeedPlus { get { return npc.SpeedPlus; } set { npc.SpeedPlus = value; } }
        // unknown bits
        public bool B2b3 { get { return npc.B2b3; } set { npc.B2b3 = value; } } // face on trigger
        public bool B2b4 { get { return npc.B2b4; } set { npc.B2b4 = value; } }
        public bool B2b5 { get { return npc.B2b5; } set { npc.B2b5 = value; } }
        public bool B2b6 { get { return npc.B2b6; } set { npc.B2b6 = value; } } // set sequence
        public bool B2b7 { get { return npc.B2b7; } set { npc.B2b7 = value; } } // no floating
        public bool B3b0 { get { return npc.B3b0; } set { npc.B3b0 = value; } }
        public bool B3b1 { get { return npc.B3b1; } set { npc.B3b1 = value; } } // can't walk under
        public bool B3b2 { get { return npc.B3b2; } set { npc.B3b2 = value; } }
        public bool B3b3 { get { return npc.B3b3; } set { npc.B3b3 = value; } } // can't jump on
        public bool B3b4 { get { return npc.B3b4; } set { npc.B3b4 = value; } }
        public bool B3b5 { get { return npc.B3b5; } set { npc.B3b5 = value; } }
        public bool B3b6 { get { return npc.B3b6; } set { npc.B3b6 = value; } } // can't walk through
        public bool B3b7 { get { return npc.B3b7; } set { npc.B3b7 = value; } }
        public bool B4b0 { get { return npc.B4b0; } set { npc.B4b0 = value; } }
        public bool B4b1 { get { return npc.B4b1; } set { npc.B4b1 = value; } }
        public bool B7b6 { get { return npc.B7b6; } set { npc.B7b6 = value; } }
        public bool B7b7 { get { return npc.B7b7; } set { npc.B7b7 = value; } }
        //
        public ushort NPCID { get { return npc.NPCID; } set { npc.NPCID = value; } }
        public ushort Movement { get { return npc.Movement; } set { npc.Movement = value; } }
        public ushort EventORpack { get { return npc.EventORpack; } set { npc.EventORpack = value; } }
        public byte EngageTrigger { get { return npc.EngageTrigger; } set { npc.EngageTrigger = value; } }
        public byte AfterBattle { get { return npc.AfterBattle; } set { npc.AfterBattle = value; } }
        public byte X { get { return npc.X; } set { npc.X = value; } }
        public byte Y { get { return npc.Y; } set { npc.Y = value; } }
        public byte Z { get { return npc.Z; } set { npc.Z = value; } }
        public byte F { get { return npc.F; } set { npc.F = value; } }
        public byte PropertyA { get { return npc.PropertyA; } set { npc.PropertyA = value; } }
        public byte PropertyB { get { return npc.PropertyB; } set { npc.PropertyB = value; } }
        public byte PropertyC { get { return npc.PropertyC; } set { npc.PropertyC = value; } }
        public bool Xb7 { get { return npc.Xb7; } set { npc.Xb7 = value; } }
        public bool Yb7 { get { return npc.Yb7; } set { npc.Yb7 = value; } }
        // clone accessors
        public byte CloneX { get { return npc.CloneX; } set { npc.CloneX = value; } }
        public byte CloneY { get { return npc.CloneY; } set { npc.CloneY = value; } }
        public byte CloneZ { get { return npc.CloneZ; } set { npc.CloneZ = value; } }
        public byte CloneF { get { return npc.CloneF; } set { npc.CloneF = value; } }
        public byte ClonePropertyA { get { return npc.ClonePropertyA; } set { npc.ClonePropertyA = value; } }
        public byte ClonePropertyB { get { return npc.ClonePropertyB; } set { npc.ClonePropertyB = value; } }
        public byte ClonePropertyC { get { return npc.ClonePropertyC; } set { npc.ClonePropertyC = value; } }
        public bool CloneXb7 { get { return npc.CloneXb7; } set { npc.CloneXb7 = value; } }
        public bool CloneYb7 { get { return npc.CloneYb7; } set { npc.CloneYb7 = value; } }
        #endregion
        // constructor
        public LevelNPCs(int index)
        {
            this.index = index;
            Disassemble();
        }
        private void Disassemble()
        {
            int pointerOffset = (index * 2) + 0x148000;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (index == 0x1FF)
                offsetEnd = 0;
            // no npc fields for level
            if (offsetStart >= offsetEnd)
                return;
            // 
            int offset = offsetStart + 0x140000;
            this.startingOffset = offset;
            this.partition = rom[offset++];
            while (offset < offsetEnd + 0x140000)
            {
                NPC tNPC = new NPC();
                tNPC.Disassemble(offset);
                npcs.Add(tNPC);
                offset += 12;
                for (int i = 0; i < tNPC.CloneAmount; i++)
                    offset += 4;
            }
        }
        public void Assemble(ref int offsetStart)
        {
            int pointerOffset = (index * 2) + 0x148000;
            Bits.SetShort(rom, pointerOffset, offsetStart);
            if (npcs.Count == 0)
                return;
            int offset = offsetStart + 0x140000;
            rom[offset++] = partition;
            foreach (NPC npc in npcs)
                npc.Assemble(ref offset);
            offsetStart = (ushort)(offset - 0x140000);
        }
        // list managers
        public void Clear()
        {
            npcs.Clear();
            currentNPC = 0;
        }
        public void New(int index, Point p)
        {
            NPC e = new NPC();
            e.Clear();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            e.Xb7 = true;
            if (index < npcs.Count)
                npcs.Insert(index, e);
            else
                npcs.Add(e);
        }
        public void New(int index, NPC copy)
        {
            NPC e = new NPC();
            e.CopyOver(copy);
            if (index < npcs.Count)
                npcs.Insert(index, e);
            else
                npcs.Add(e);
        }
        public void NewClone(int index, Point p)
        {
            npc.New(index, p);
        }
        public void NewClone(int index, NPC.Clone e)
        {
            npc.New(index, e);
        }
        public void Remove()
        {
            if (currentNPC < npcs.Count)
            {
                npcs.Remove(npcs[currentNPC]);
                this.currentNPC = 0;
            }
        }
        public void RemoveClone()
        {
            npc.Remove();
        }
        public void Reverse(int index)
        {
            npcs.Reverse(index, 2);
        }
        public void ReverseClone(int index)
        {
            npc.Reverse(index);
        }
    }
    [Serializable()]
    public class NPC
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public int Index = 0;
        // class variables
        private Clone clone;
        public List<Clone> Clones = new List<Clone>();
        // external selectors
        public bool Hilite = false;
        private int currentClone = 0;
        public int CurrentClone
        {
            get
            {
                return this.currentClone;
            }
            set
            {
                clone = (Clone)Clones[value];
                this.currentClone = value;
            }
        }
        private int selectedClone;
        public int SelectedClone { get { return this.selectedClone; } set { selectedClone = value; } }
        public Clone Clone_ { get { return clone; } }
        public int Count { get { return Clones.Count; } }
        private bool isCloneSelected;
        public bool IsCloneSelected { get { return isCloneSelected; } set { isCloneSelected = value; } }
        // NPC properties
        private byte cloneAmount; public byte CloneAmount { get { return cloneAmount; } set { cloneAmount = value; } }
        private byte engageType; public byte EngageType { get { return engageType; } set { engageType = value; } }
        private byte speedPlus; public byte SpeedPlus { get { return speedPlus; } set { speedPlus = value; } }
        private ushort npcID; public ushort NPCID { get { return npcID; } set { npcID = value; } }
        private ushort movement; public ushort Movement { get { return movement; } set { movement = value; } }
        private ushort eventORpack; public ushort EventORpack { get { return eventORpack; } set { eventORpack = value; } }
        private byte engageTrigger; public byte EngageTrigger { get { return engageTrigger; } set { engageTrigger = value; } }
        private byte afterBattle; public byte AfterBattle { get { return afterBattle; } set { afterBattle = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte z; public byte Z { get { return z; } set { z = value; } }
        private byte f; public byte F { get { return f; } set { f = value; } }
        private byte propertyA; public byte PropertyA { get { return propertyA; } set { propertyA = value; } }
        private byte propertyB; public byte PropertyB { get { return propertyB; } set { propertyB = value; } }
        private byte propertyC; public byte PropertyC { get { return propertyC; } set { propertyC = value; } }
        private bool xb7; public bool Xb7 { get { return xb7; } set { xb7 = value; } }
        private bool yb7; public bool Yb7 { get { return yb7; } set { yb7 = value; } }
        // unknown bits
        private bool b2b3; public bool B2b3 { get { return b2b3; } set { b2b3 = value; } }
        private bool b2b4; public bool B2b4 { get { return b2b4; } set { b2b4 = value; } }
        private bool b2b5; public bool B2b5 { get { return b2b5; } set { b2b5 = value; } }
        private bool b2b6; public bool B2b6 { get { return b2b6; } set { b2b6 = value; } }
        private bool b2b7; public bool B2b7 { get { return b2b7; } set { b2b7 = value; } }
        private bool b3b0; public bool B3b0 { get { return b3b0; } set { b3b0 = value; } }
        private bool b3b1; public bool B3b1 { get { return b3b1; } set { b3b1 = value; } }
        private bool b3b2; public bool B3b2 { get { return b3b2; } set { b3b2 = value; } }
        private bool b3b3; public bool B3b3 { get { return b3b3; } set { b3b3 = value; } }
        private bool b3b4; public bool B3b4 { get { return b3b4; } set { b3b4 = value; } }
        private bool b3b5; public bool B3b5 { get { return b3b5; } set { b3b5 = value; } }
        private bool b3b6; public bool B3b6 { get { return b3b6; } set { b3b6 = value; } }
        private bool b3b7; public bool B3b7 { get { return b3b7; } set { b3b7 = value; } }
        private bool b4b0; public bool B4b0 { get { return b4b0; } set { b4b0 = value; } }
        private bool b4b1; public bool B4b1 { get { return b4b1; } set { b4b1 = value; } }
        private bool b7b6; public bool B7b6 { get { return b7b6; } set { b7b6 = value; } }
        private bool b7b7; public bool B7b7 { get { return b7b7; } set { b7b7 = value; } }
        // clone properties
        public byte CloneX { get { return clone.X; } set { clone.X = value; } }
        public byte CloneY { get { return clone.Y; } set { clone.Y = value; } }
        public byte CloneZ { get { return clone.Z; } set { clone.Z = value; } }
        public byte CloneF { get { return clone.F; } set { clone.F = value; } }
        public byte ClonePropertyA { get { return clone.PropertyA; } set { clone.PropertyA = value; } }
        public byte ClonePropertyB { get { return clone.PropertyB; } set { clone.PropertyB = value; } }
        public byte ClonePropertyC { get { return clone.PropertyC; } set { clone.PropertyC = value; } }
        public bool CloneXb7 { get { return clone.Xb7; } set { clone.Xb7 = value; } }
        public bool CloneYb7 { get { return clone.Yb7; } set { clone.Yb7 = value; } }
        // functions
        public void Disassemble(int offset)
        {
            byte temp = rom[offset++];
            cloneAmount = (byte)(temp & 0x0F);
            engageType = (byte)((temp & 0x30) >> 4);
            temp = rom[offset++];
            speedPlus = (byte)(temp & 0x07);
            //
            b2b3 = (temp & 0x08) == 0x08;
            b2b4 = (temp & 0x10) == 0x10;
            b2b5 = (temp & 0x20) == 0x20;
            b2b6 = (temp & 0x40) == 0x40;
            b2b7 = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            b3b0 = (temp & 0x01) == 0x01;
            b3b1 = (temp & 0x02) == 0x02;
            b3b2 = (temp & 0x04) == 0x04;
            b3b3 = (temp & 0x08) == 0x08;
            b3b4 = (temp & 0x10) == 0x10;
            b3b5 = (temp & 0x20) == 0x20;
            b3b6 = (temp & 0x40) == 0x40;
            b3b7 = (temp & 0x80) == 0x80;
            //
            b4b0 = (rom[offset] & 0x01) == 0x01;
            b4b1 = (rom[offset] & 0x02) == 0x02;
            //
            npcID = (ushort)((Bits.GetShort(rom, offset++) & 0x0FFF) >> 2);
            //
            movement = (ushort)((Bits.GetShort(rom, offset++) & 0x3FF0) >> 4);
            //
            b7b6 = (rom[offset] & 0x40) == 0x40;
            b7b7 = (rom[offset++] & 0x80) == 0x80;
            //
            ushort tempShort = Bits.GetShort(rom, offset++);
            if (engageType == 2)
                eventORpack = (ushort)(tempShort & 0xFF);
            else
                eventORpack = (ushort)(tempShort & 0xFFF);
            //
            temp = rom[offset++];
            if (engageType == 2)
                afterBattle = (byte)((temp >> 1) & 0x07);
            engageTrigger = Math.Min((byte)((temp & 0xF0) >> 4), (byte)12);
            //
            temp = rom[offset++];
            if (engageType == 0)
            {
                propertyA = (byte)(temp & 0x07);       // npc id+
                propertyB = (byte)((temp & 0xE0) >> 5);         // event id+
                propertyC = (byte)((temp & 0x18) >> 3);
            }
            else if (engageType == 1) 
                propertyA = temp;  // $70A7
            else if (engageType == 2)
            {
                propertyA = (byte)(temp & 0x0F);  // movement+
                propertyB = (byte)((temp & 0xF0) >> 4);    // pack+
            }
            //
            temp = rom[offset++];
            x = (byte)(temp & 0x7F);
            xb7 = (temp & 0x80) == 0x80;
            temp = rom[offset++];
            y = (byte)(temp & 0x7F);
            yb7 = (temp & 0x80) == 0x80;
            temp = rom[offset++];
            z = (byte)(temp & 0x1F);
            f = (byte)((temp & 0xF0) >> 5);
            //
            for (int i = 0; i < cloneAmount; i++)
            {
                Clone tClone = new Clone();
                tClone.Disassemble(offset, engageType);
                Clones.Add(tClone);
                offset += 4;
            }
        }
        public void Assemble(ref int offset)
        {
            rom[offset] = cloneAmount;
            Bits.SetBitsByByte(rom, offset++, (byte)(engageType << 4), true);
            //
            rom[offset] = speedPlus;
            Bits.SetBit(rom, offset, 3, b2b3);
            Bits.SetBit(rom, offset, 4, b2b4);
            Bits.SetBit(rom, offset, 5, b2b5);
            Bits.SetBit(rom, offset, 6, b2b6);
            Bits.SetBit(rom, offset++, 7, b2b7);
            //
            Bits.SetBit(rom, offset, 0, b3b0);
            Bits.SetBit(rom, offset, 1, b3b1);
            Bits.SetBit(rom, offset, 2, b3b2);
            Bits.SetBit(rom, offset, 3, b3b3);
            Bits.SetBit(rom, offset, 4, b3b4);
            Bits.SetBit(rom, offset, 5, b3b5);
            Bits.SetBit(rom, offset, 6, b3b6);
            Bits.SetBit(rom, offset++, 7, b3b7);
            //
            Bits.SetShort(rom, offset, (ushort)(npcID << 2));
            Bits.SetBit(rom, offset, 0, b4b0);
            Bits.SetBit(rom, offset++, 1, b4b1);
            //
            Bits.SetBitsByByte(rom, offset++, (byte)((movement << 4) & 0xF0), true); // lower 4 bits
            rom[offset] = (byte)(movement >> 4); // lower 6 bits
            Bits.SetBit(rom, offset, 6, b7b6);
            Bits.SetBit(rom, offset++, 7, b7b7);
            //
            if (engageType == 2) // if pack (1 byte)
                rom[offset] = (byte)eventORpack;
            else //if event (1 short)
                Bits.SetShort(rom, offset, eventORpack);
            offset++;
            //
            rom[offset] &= 0x0F;
            rom[offset] |= (byte)(engageTrigger << 4);
            if (engageType == 2)
            {
                rom[offset] &= 0xF0;
                rom[offset] |= (byte)(afterBattle << 1);
            }
            offset++;
            //
            rom[offset] = propertyA;
            if (engageType == 0)
                Bits.SetBitsByByte(rom, offset, (byte)(propertyB << 5), true);
            else if (engageType == 2)
                Bits.SetBitsByByte(rom, offset, (byte)(propertyB << 4), true);
            if (engageType == 0)
                Bits.SetBitsByByte(rom, offset, (byte)(propertyC << 3), true);
            offset++;
            //
            rom[offset] = x;
            Bits.SetBit(rom, offset++, 7, xb7);
            rom[offset] = y;
            Bits.SetBit(rom, offset++, 7, yb7);
            rom[offset] = z;
            Bits.SetBitsByByte(rom, offset++, (byte)(f << 5), true);
            //
            for (int i = 0; i < cloneAmount; i++)
            {
                this.CurrentClone = i;
                clone.Assemble(ref offset, engageType);
            }
        }
        // list managers
        public void Clear()
        {
            cloneAmount = 0;
            engageType = 0;
            speedPlus = 0;
            b2b3 = false;
            b2b4 = false;
            b2b5 = false;
            b2b6 = true;
            b2b7 = false;
            b3b0 = false;
            b3b1 = false;
            b3b2 = false;
            b3b3 = false;
            b3b4 = false;
            b3b5 = false;
            b3b6 = true;
            b3b7 = false;
            b4b0 = false;
            b4b1 = false;
            npcID = 0;
            movement = 0;
            eventORpack = 0;
            engageTrigger = 0;
            b7b6 = true;
            b7b7 = true;
            afterBattle = 0;
            x = 0;
            y = 0;
            z = 0;
            xb7 = true;
            yb7 = false;
            f = 0;
            propertyA = 0;
            propertyB = 0;
            propertyC = 0;
        }
        public void New(int index, Point p)
        {
            Clone e = new Clone();
            e.Clear();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < Clones.Count)
                Clones.Insert(index, e);
            else
                Clones.Add(e);
            cloneAmount++;
        }
        public void New(int index, Clone copy)
        {
            Clone e = new Clone();
            e.CopyOver(copy);
            if (index < Clones.Count)
                Clones.Insert(index, e);
            else
                Clones.Add(e);
            cloneAmount++;
        }
        public void Remove()
        {
            if (currentClone < Clones.Count)
            {
                Clones.Remove(Clones[currentClone]);
                this.currentClone = 0;
                cloneAmount--;
            }
        }
        public void Reverse(int index)
        {
            Clones.Reverse(index, 2);
        }
        public void CopyOver(NPC copy)
        {
            cloneAmount = copy.CloneAmount;
            engageType = copy.EngageType;
            speedPlus = copy.SpeedPlus;
            b2b3 = copy.B2b3;
            b2b4 = copy.B2b4;
            b2b5 = copy.B2b5;
            b2b6 = copy.B2b6;
            b2b7 = copy.B2b7;
            b3b0 = copy.B3b0;
            b3b1 = copy.B3b1;
            b3b2 = copy.B3b2;
            b3b3 = copy.B3b3;
            b3b4 = copy.B3b4;
            b3b5 = copy.B3b5;
            b3b6 = copy.B3b6;
            b3b7 = copy.B3b7;
            b4b0 = copy.B4b0;
            b4b1 = copy.B4b1;
            npcID = copy.NPCID;
            movement = copy.Movement;
            eventORpack = copy.EventORpack;
            engageTrigger = copy.EngageTrigger;
            b7b6 = copy.B7b6;
            b7b7 = copy.B7b7;
            afterBattle = copy.AfterBattle;
            x = copy.X;
            y = copy.Y;
            z = copy.Z;
            xb7 = copy.Xb7;
            yb7 = copy.Yb7;
            f = copy.F;
            propertyA = copy.PropertyA;
            propertyB = copy.PropertyB;
            propertyC = copy.PropertyC;
            Clone tInstance;
            foreach (Clone i in copy.Clones)
            {
                tInstance = new Clone();
                tInstance.CopyOver(i);
                Clones.Add(tInstance);
            }
        }
        [Serializable()]
        public class Clone : NPC
        {
            // assemblers
            public void Disassemble(int offset, int engageType)
            {
                byte temp = rom[offset++];
                if (engageType == 0) propertyA = (byte)(temp & 0x07);       // npc id+
                else if (engageType == 1) propertyA = temp;  // $70A7
                else if (engageType == 2) propertyA = (byte)(temp & 0x0F);  // movement+
                //
                if (engageType == 0) propertyB = (byte)((temp & 0xF0) >> 5);         // event id+
                else if (engageType == 2) propertyB = (byte)((temp & 0xF0) >> 4);    // pack+
                //
                if (engageType == 0) propertyC = (byte)((temp & 0x18) >> 3); // movement+
                //
                temp = rom[offset++];
                x = (byte)(temp & 0x7F);
                xb7 = (temp & 0x80) == 0x80;
                temp = rom[offset++];
                y = (byte)(temp & 0x7F);
                yb7 = (temp & 0x80) == 0x80;
                temp = rom[offset++];
                z = (byte)(temp & 0x1F);
                f = (byte)((temp & 0xF0) >> 5);
            }
            public void Assemble(ref int offset, int engageType)
            {
                rom[offset] = propertyA;
                if (engageType == 0)
                    Bits.SetBitsByByte(rom, offset, (byte)(propertyB << 5), true);
                else if (engageType == 2)
                    Bits.SetBitsByByte(rom, offset, (byte)(propertyB << 4), true);
                if (engageType == 0)
                    Bits.SetBitsByByte(rom, offset, (byte)(propertyC << 3), true);
                offset++;
                //
                rom[offset] = x;
                Bits.SetBit(rom, offset, 7, xb7); offset++;
                rom[offset] = y;
                Bits.SetBit(rom, offset, 7, yb7); offset++;
                rom[offset] = z;
                Bits.SetBitsByByte(rom, offset, (byte)(f << 5), true); offset++;
            }
            // universal functions
            public new void Clear()
            {
                x = 0;
                y = 0;
                z = 0;
                xb7 = true;
                yb7 = false;
                f = 0;
                propertyA = 0;
                propertyB = 0;
                propertyC = 0;
            }
            // spawning
            public void CopyOver(Clone copy)
            {
                x = copy.X;
                y = copy.Y;
                z = copy.Z;
                xb7 = copy.xb7;
                yb7 = copy.yb7;
                f = copy.F;
                propertyA = copy.PropertyA;
                propertyB = copy.PropertyB;
                propertyC = copy.PropertyC;
            }
        }
    }
}
