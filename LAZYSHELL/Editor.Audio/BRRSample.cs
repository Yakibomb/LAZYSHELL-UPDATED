using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class BRRSample : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private byte[] sample; public byte[] Sample { get { return sample; } set { sample = value; } }
        private int loopStart; public int LoopStart { get { return loopStart; } set { loopStart = value; } }
        private short relGain; public short RelGain { get { return relGain; } set { relGain = value; } }
        public short relFreq; public short RelFreq { get { return relFreq; } set { relFreq = value; } }
        public int Rate
        {
            get
            {
                double rate = 32000.0;
                double power = (double)relFreq / 256.0 / 12.0;
                if (power >= 0)
                    rate *= Math.Pow(2.0, power);
                else
                    rate /= Math.Pow(2.0, -power);
                return (int)rate;
            }
        }
        public int Length
        {
            get
            {
                if (sample == null)
                    return 0;
                else return sample.Length;
            }
        }
        // constructor
        public BRRSample(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            int offset = Bits.GetInt24(rom, index * 3 + 0x042333);
            if (offset == 0)
                return;
            offset -= 0xC00000;
            int size = Bits.GetShort(rom, offset); offset += 2;
            sample = Bits.GetBytes(rom, offset, size);
            loopStart = Bits.GetShort(rom, index * 2 + 0x04248F);
            relGain = (short)Bits.GetShort(rom, index * 2 + 0x042577);
            relFreq = (short)Bits.GetShort(rom, index * 2 + 0x04265F);
        }
        public void Assemble(ref int offset)
        {
            if (sample == null)
            {
                Bits.SetInt24(rom, index * 3 + 0x042333, 0);
                return;
            }
            Bits.SetInt24(rom, index * 3 + 0x042333, offset + 0xC00000);
            Bits.SetShort(rom, offset, sample.Length); offset += 2;
            Bits.SetBytes(rom, offset, sample);
            offset += sample.Length;
            //
            Bits.SetShort(rom, index * 2 + 0x04248F, loopStart);
            Bits.SetShort(rom, index * 2 + 0x042577, relGain);
            Bits.SetShort(rom, index * 2 + 0x04265F, relFreq);
        }
        // universal functions
        public override void Clear()
        {
            if (sample != null)
                Array.Clear(sample, 0, sample.Length);
        }
    }
}
