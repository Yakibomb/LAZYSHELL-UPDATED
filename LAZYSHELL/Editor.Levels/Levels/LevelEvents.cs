using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class LevelEvents
    {
        // universal Variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private List<Event> events = new List<Event>();
        private int currentEvent = 0;
        private int selectedEvent;
        private Event thisEvent;
        // accessors
        public int Length
        {
            get
            {
                int length = 5;
                if (Width > 0)
                    length++;
                return length;
            }
        }
        public int CurrentEvent
        {
            get
            {
                return this.currentEvent;
            }
            set
            {
                if (this.events.Count > value)
                {
                    thisEvent = events[value];
                    this.currentEvent = value;
                }
            }
        }
        public List<Event> Events { get { return events; } }
        public int SelectedEvent { get { return this.selectedEvent; } set { selectedEvent = value; } }
        public Event Event { get { return thisEvent; } set { thisEvent = value; } }        
        public int Count { get { return events.Count; } }
        // level properties
        private byte music; public byte Music { get { return music; } set { music = value; } }
        private ushort entranceEvent; public ushort EntranceEvent { get { return entranceEvent; } set { entranceEvent = value; } }
        // event properties
        public ushort RunEvent { get { return thisEvent.RunEvent; } set { thisEvent.RunEvent = value; } }
        public byte X { get { return thisEvent.X; } set { thisEvent.X = value; } }
        public byte Y { get { return thisEvent.Y; } set { thisEvent.Y = value; } }
        public byte Z { get { return thisEvent.Z; } set { thisEvent.Z = value; } }
        public byte F { get { return thisEvent.F; } set { thisEvent.F = value; } }
        public byte Height { get { return thisEvent.Height; } set { thisEvent.Height = value; } }
        public bool X_Half { get { return thisEvent.X_Half; } set { thisEvent.X_Half = value; } }
        public bool Y_Half { get { return thisEvent.Y_Half; } set { thisEvent.Y_Half = value; } }
        public byte Width { get { return thisEvent.Width; } set { thisEvent.Width = value; } }
        // constructor, functions
        public LevelEvents(int index)
        {
            this.index = index;
            Disassemble();
        }
        private void Disassemble()
        {
            int pointerOffset = (index * 2) + 0x20E000;
            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            if (index == 0x1FF) offsetEnd = 0;
            // no event fields for level
            if (offsetStart >= offsetEnd)
                return;
            //
            int offset = offsetStart + 0x200000;
            music = rom[offset++];
            entranceEvent = Bits.GetShort(rom, offset); offset += 2;
            while (offset < offsetEnd + 0x200000)
            {
                Event tEvent = new Event();
                tEvent.Disassemble(offset);
                events.Add(tEvent);
                offset += 5;
                if (tEvent.Width > 0)
                    offset += 1;
            }
        }
        public void Assemble(ref int offsetStart)
        {
            int pointerOffset = (index * 2) + 0x20E000;
            Bits.SetShort(rom, pointerOffset, offsetStart);
            int offset = offsetStart + 0x200000;
            rom[offset++] = music;
            Bits.SetShort(rom, offset, entranceEvent); offset += 2;
            offsetStart = (ushort)(offset - 0x200000);
            // no exit fields for level
            if (events.Count == 0) 
                return; 
            //
            foreach (Event EVENT in events)
            {
                EVENT.Assemble(rom, offset);
                offset += 5;
                if (EVENT.Width > 0)
                    offset++;
            }
            offsetStart = (ushort)(offset - 0x200000);
        }
        // list managers
        public void Clear()
        {
            events.Clear();
            this.currentEvent = 0;
        }
        public void New(int index, Point p)
        {
            Event e = new Event();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < events.Count)
                events.Insert(index, e);
            else
                events.Add(e);
        }
        public void New(int index, Event copy)
        {
            if (index < events.Count)
                events.Insert(index, copy);
            else
                events.Add(copy);
        }
        public void Remove()
        {
            if (currentEvent < events.Count)
            {
                events.Remove(events[currentEvent]);
                this.currentEvent = 0;
            }
        }
    }
    [Serializable()]
    public class Event
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        public int Index = 0;
        //
        public bool Hilite = false;
        public int Length
        {
            get
            {
                int length = 5;
                if (width > 0)
                    length++;
                return length;
            }
        }
        // event properties
        private ushort runEvent; public ushort RunEvent { get { return runEvent; } set { runEvent = value; } }
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private byte z; public byte Z { get { return z; } set { z = value; } }
        private byte height; public byte Height { get { return height; } set { height = value; } }
        private bool x_half; public bool X_Half { get { return x_half; } set { x_half = value; } }
        private bool y_half; public bool Y_Half { get { return y_half; } set { y_half = value; } }
        private byte width; public byte Width { get { return width; } set { width = value; } }
        private byte f; public byte F { get { return f; } set { f = value; } }
        // constructor
        public Event()
        {
        }
        // assemblers
        public void Disassemble(int offset)
        {
            runEvent = (ushort)(Bits.GetShort(rom, offset++) & 0x0FFF);
            byte temp = rom[offset++];
            bool lengthOverOne = (temp & 0x80) == 0x80;
            //
            temp = rom[offset++];
            if ((temp & 0x80) == 0x80) 
                x_half = true;
            x = (byte)(temp & 0x7F);
            temp = rom[offset++];
            if ((temp & 0x80) == 0x80) 
                y_half = true;
            y = (byte)(temp & 0x7F);
            temp = rom[offset++];
            z = (byte)(temp & 0x1F);
            height = (byte)((temp & 0xF0) >> 5);
            //
            if (lengthOverOne)
            {
                temp = rom[offset++];
                width = (byte)(temp & 0x0F);
                f = (byte)((temp & 0x80) >> 7);
            }
        }
        public void Assemble(byte[] data, int offset)
        {
            Bits.SetShort(data, offset, runEvent); offset++;
            Bits.SetBit(data, offset, 7, width > 0); offset++;
            data[offset] = x;
            Bits.SetBit(data, offset, 7, x_half); offset++;
            data[offset] = y;
            Bits.SetBit(data, offset, 7, y_half); offset++;
            data[offset] = z;
            Bits.SetBitsByByte(data, offset, (byte)(height << 5), true); offset++;
            if (width > 0)
            {
                data[offset] = width;
                Bits.SetBitsByByte(data, offset, (byte)(f << 7), true); offset++;
            }
        }
        public Event Copy()
        {
            Event copy = new Event();
            copy.RunEvent = runEvent;
            copy.X = x;
            copy.Y = y;
            copy.Z = z;
            copy.Height = height;
            copy.F = f;
            copy.Width = width;
            copy.X_Half = x_half;
            copy.Y_Half = y_half;
            copy.F = f;
            return copy;
        }
    }
}
