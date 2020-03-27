using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class E_Sequence
    {
        // variables
        private List<Frame> frames = new List<Frame>(); 
        public List<Frame> Frames { get { return this.frames; } set { this.frames = value; } }
        public void Disassemble(byte[] sm, int offset)
        {
            Frame tFrame;
            offset = Bits.GetShort(sm, offset);
            while (offset != 0xFFFF && sm[offset] != 0)
            {
                tFrame = new Frame();
                tFrame.Disassemble(sm, offset);
                frames.Add(tFrame);
                offset += 2;
            }
        }
        [Serializable()]
        public class Frame
        {
            // variables
            private byte duration; public byte Duration { get { return duration; } set { duration = value; } }
            private byte mold; public byte Mold { get { return mold; } set { mold = value; } }
            // assemblers
            public void Disassemble(byte[] sm, int offset)
            {
                duration = sm[offset];
                mold = sm[offset + 1];
            }
            // spawning
            public Frame New()
            {
                Frame empty = new Frame();
                empty.Duration = 2;
                empty.Mold = 0;
                return empty;
            }
            public Frame Copy()
            {
                Frame copy = new Frame();
                copy.Duration = duration;
                copy.Mold = mold;
                return copy;
            }
        }
    }
}
