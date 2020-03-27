using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public static class BRR
    {
        static short p1, p2;
        // encoder variables
        static bool[] FIRen;
        static int[] FIRstats;
        static byte[] BRRBuffer;
        static double resample_var;
        static char resample_type;
        // decoder functions
        public static byte[] BRRToWAV(byte[] inBrr, int rate)
        {
            return BRRToWAV(inBrr, rate, 0);
        }
        public static byte[] BRRToWAV(byte[] inBrr, int rate, int loopStart)
        {
            if (inBrr == null)
                inBrr = new byte[9];
            p1 = 0; p2 = 0;
            short[] samples = new short[0];
            byte[] BRR = new byte[9];
            int size = (int)inBrr.Length;
            //if (size % 9 != 0)
            //{
            //    MessageBox.Show("Error : BRR file isn't a multiple of 9 bytes or is too big.");
            //    return null;
            //}
            int blockamount = size / 9;
            int offset = 0;
            size = 0;
            for (int i = 0; i < blockamount; i++)
            {
                BRR = Bits.GetBytes(inBrr, offset, 9); offset += 9;
                samples = append(samples, DecodeBRR(BRR));	//Append 16 BRR samples to existing array
                size += 16;
            }
            //
            int position = loopStart / 9 * 16;
            if (position >= size)
                position = 0;
            size -= position;
            //
            byte[] outWav = new byte[(size << 1) + 44];
            offset = 0;
            Bits.SetChars(outWav, offset, "RIFF".ToCharArray()); offset += 4;
            Bits.SetInt32(outWav, offset, (size << 1) + 36); offset += 4;
            Bits.SetChars(outWav, offset, "WAVEfmt ".ToCharArray()); offset += 8;
            Bits.SetInt32(outWav, offset, 16); offset += 4;
            Bits.SetShort(outWav, offset, 1); offset += 2;
            Bits.SetShort(outWav, offset, 1); offset += 2;
            Bits.SetInt32(outWav, offset, rate); offset += 4;
            Bits.SetInt32(outWav, offset, rate * 2); offset += 4;
            Bits.SetShort(outWav, offset, 2); offset += 2;
            Bits.SetShort(outWav, offset, 16); offset += 2;
            Bits.SetChars(outWav, offset, "data".ToCharArray()); offset += 4;
            Bits.SetInt32(outWav, offset, size << 1); offset += 4;
            //
            for (int i = position; i < size + position; i++)
            {
                Bits.SetShort(outWav, offset, samples[i]);
                offset += 2;
            }
            return outWav;
        }
        public static short[] DecodeBRR(byte[] Data)
        {	//Decode a string of BRR bytes
            int Filter = (Data[0] & 0x0c) >> 2;
            int ShiftAmount = (Data[0] >> 4) & 0x0F;						//Read filter & shift amount
            short[] output = new short[(Data.Length - 1) << 1];					//Output string of 16-bit samples
            for (int i = 0; i < Data.Length - 1; i++)
            {						//Loop for each byte
                DecodeSample((byte)(Data[i + 1] & 0xF0), ShiftAmount, Filter);		//Decode high nybble
                output[i << 1] = p1;
                DecodeSample((byte)(Data[i + 1] << 4), ShiftAmount, Filter);	//Decode low nybble
                output[(i << 1) + 1] = p1;
            }
            return output;
        }
        static void DecodeSample(byte s, int ShiftAmount, int Filter)
        {
            int a = ((s >> 4) ^ 8) - 8;					//Fix numbers that should be negative            
            if (ShiftAmount > 0x0C)
                a = (short)((a >> 14) & 0x7FF);		//Values "invalid" shift counts
            else
                a = a << (ShiftAmount - 1);				//Valid shift count
            switch (Filter)
            {				//Different formulas for 4 filters
                case 1:
                    a += p1 >> 1;
                    a += (-p1) >> 5;
                    break;
                case 2:
                    a += p1;
                    a += (-(p1 + (p1 >> 1))) >> 5;
                    a -= p2 >> 1;
                    a += p2 >> 5;
                    break;
                case 3:
                    a += p1;
                    a += (-(p1 + (p1 << 2) + (p1 << 3))) >> 7;
                    a -= p2 >> 1;
                    a += (p2 + (p2 >> 1)) >> 4;
                    break;
            }
            p2 = p1;
            if ((short)a != a)
                a = (short)(0x7FFF - (a >> 24));
            p1 = (short)(a * 2);
        }
        static short[] append(short[] t1, short[] t2)
        {		//Append 2 short arrays
            short[] a = new short[t1.Length + t2.Length];
            for (int i = 0; i < t1.Length; i++)
            {
                a[i] = t1[i];
            }
            for (int i = 0; i < t2.Length; i++)
            {
                a[t1.Length + i] = t2[i];
            }
            return a;
        }
        static short[] append(short t1, short[] t2)
        {		//Append a short at the start of an array
            short[] a = new short[1 + t2.Length];
            a[0] = t1;
            for (int i = 0; i < t2.Length; i++)
            {
                a[1 + i] = t2[i];
            }
            return a;
        }
        static short[] append(short[] t1, short t2)
        {		//Append a short to the end of an array
            short[] a = new short[t1.Length + 1];
            for (int i = 0; i < t1.Length; i++)
            {
                a[i] = t1[i];
            }
            a[t1.Length] = t2;
            return a;
        }
        // encoder functions
        public static byte[] Encode(byte[] inWav)
        {
            byte loop = 0x02;
            resample_var = 1.0;
            resample_type = 'n';
            FIRen = new bool[] { true, true, true, true };
            FIRstats = new int[] { 0, 0, 0, 0 };
            List<byte> outBrr = new List<byte>();
            BRRBuffer = new byte[9];			//9 bytes long buffer
            int offset = 0;
            if (Bits.GetString(inWav, offset, 4) != "RIFF")
            {
                //"RIFF" letters
                MessageBox.Show("Input file in unsupported format !");
                return null;
            }
            offset += 4;
            offset += 4;
            if (Bits.GetString(inWav, offset, 4) != "WAVE") 					//"WAVE letters
            {
                MessageBox.Show("Input file in unsupported format !");
                return null;
            }
            offset += 4;
            if (Bits.GetString(inWav, offset, 4) != "fmt ") 					//"WAVE letters
            {
                MessageBox.Show("Input file in unsupported format !");
                return null;
            }
            offset += 4;
            int sc1size = Bits.GetInt32(inWav, offset);
            offset += 4;
            if (sc1size < 0x10										//Size of sub-chunk1 (header) must be at least 16
                || Bits.GetShort(inWav, offset) != 1	//Must be in PCM format
                || Bits.GetShort(inWav, offset + 2) != 1)
            {
                //Must be in mono
                MessageBox.Show("Input file in unsupported format !");
                return null;
            }
            offset += 4;
            offset += 8; //ignore sample and byte rate
            //Read block align and bits per sample numbers
            short BlockAlign = (short)Bits.GetShort(inWav, offset); offset += 2;
            short BitPerSample = (short)Bits.GetShort(inWav, offset); offset += 2;
            if (BlockAlign != BitPerSample >> 3)
            {
                MessageBox.Show("Block align problem");
                return null;
            }
            offset += sc1size - 0x10;
            //"data" letters for sub-chunk2
            if (Bits.GetString(inWav, offset, 4) != "data")
            {
                MessageBox.Show("Input file in unsupported format");
                return null;
            }
            offset += 4;
            int Sub2Size = Bits.GetInt32(inWav, offset);		//Read sub-chunk 2 size
            offset += 4;
            short[] samples = new short[Sub2Size / BlockAlign];
            switch (BitPerSample)
            {
                case 8:
                    short sample;
                    for (int i = 0; i < Sub2Size / BlockAlign; i++)
                    {
                        sample = (short)inWav[offset++];		//Convert 8-bit samples to 16-bit
                        sample -= 0x80;
                        samples[i] = (short)(sample << 8);
                    }
                    break;
                case 16:
                    for (int i = 0; i < Sub2Size / BlockAlign; i++)
                    {
                        samples[i] = (short)(Bits.GetShort(inWav, offset));
                        offset += 2;
                    }
                    break;
                default:
                    MessageBox.Show("Error : unsupported amount of bits per sample (8 or 16 are supported");
                    return null;
            }
            samples = resample(samples, resample_type);
            if ((samples.Length & 0xF) != 0)
            {
                MessageBox.Show("Warning ! The Amount of PCM samples isn't a multiple of 16 !");
                MessageBox.Show("The sample will be padded with " + (0x10 - (samples.Length & 0x0F)) + " zeroes at the begining");
                short[] a = new short[0x10 - (samples.Length & 0x0F)];
                samples = append(a, samples);
            }
            bool init = false;
            for (int i = 0; i < 16; i++)
            {
                init |= samples[i] != 0;		//Initialization needed if any of the first 16 samples isn't zero
            }
            //Write initial BRR block
            if (init)
            {
                outBrr.AddRange(new byte[] { loop, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            }
            p1 = 0;
            p2 = 0;
            short[] samples2 = new short[16];
            for (int n = 0; n < samples.Length >> 4; n++)
            {
                for (int i = 0; i < 16; i++)
                {
                    samples2[i] = samples[i + (n << 4)];			//Read 16 samples
                }
                BRRBuffer = new byte[9];
                ADPCMBlockMash(samples2);						//Compute BRR block
                BRRBuffer[0] |= loop;						//Set the loop flag if needed
                if (n == (samples.Length >> 4) - 1) BRRBuffer[0] |= 1;	//Set the end bit if we're on the last block
                outBrr.AddRange(BRRBuffer);
            }
            return outBrr.ToArray();
        }
        static void ADPCMBlockMash(short[] PCMData)
        {
            int smin = 0;
            int kmin = 0;
            double dmin = Double.PositiveInfinity;
            for (int s = 0; s < 13; s++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (FIRen[k])
                    {
                        double d = ADPCMMash(s, k, PCMData, false);
                        if (d < dmin)
                        {
                            kmin = k;		//Memorize the filter, shift values with smaller error
                            dmin = d;
                            smin = s;
                        }
                    }
                }
            }
            BRRBuffer[0] = (byte)((smin << 4) | (kmin << 2));
            ADPCMMash(smin, kmin, PCMData, true);
            FIRstats[kmin]++;
        }
        static double ADPCMMash(int shiftamount, int filter, short[] PCMData, bool write)
        {
            int d;
            double d2 = 0.0;
            int vlin = 0;
            int da, dp, c;
            short l1 = p1;
            short l2 = p2;
            int step = 1 << shiftamount;
            for (int i = 0; i < 16; i++)
            {
                switch (filter)
                {			//Compute linear prediction for filters
                    case 1:
                        vlin = l1 >> 1;
                        vlin += (-l1) >> 5;
                        break;
                    case 2:
                        vlin = l1;
                        vlin += (-(l1 + (l1 >> 1))) >> 5;
                        vlin -= l2 >> 1;
                        vlin += l2 >> 5;
                        break;
                    case 3:
                        vlin = l1;
                        vlin += (-(l1 + (l1 << 2) + (l1 << 3))) >> 7;
                        vlin -= l2 >> 1;
                        vlin += (l2 + (l2 >> 1)) >> 4;
                        break;
                }
                d = (PCMData[i] >> 1) - vlin;		//Difference between linear prediction and current sample
                da = Math.Abs(d);
                if (da > 16384 && da < 32768)
                {								// Take advantage of wrapping
                    d = d - 32768 * (d >> 24);
                }
                dp = d + (step << 2) + (step >> 2);
                c = 0;
                if (dp > 0)
                {
                    if (step > 1)
                        c = dp / (step >> 1);
                    else
                        c = dp << 1;
                    if (c > 15)
                        c = 15;
                }
                c -= 8;
                dp = (c << (shiftamount - 1));		/* quantized estimate of samp - vlin */
                /* edge case, if caller even wants to use it */
                if (shiftamount > 12)
                    dp = (dp >> 14) & ~0x7FF;
                c &= 0x0f;						/* mask to 4 bits */
                l2 = l1;						/* shift history */
                l1 = (short)(clamp_16(vlin + dp) * 2);
                d = PCMData[i] - l1;
                d2 += ((double)d) * d;		/* update square-error */
                if (write)
                {							/* if we want output, put it in proper place */
                    BRRBuffer[(i >> 1) + 1] |= (byte)(c << (4 - ((i & 0x01) << 2)));
                }
            }
            if (write)
            {
                p2 = l2;
                p1 = l1;
            }
            return d2;
        }
        static short clamp_16(int n)
        {
            if ((short)n != n)
                n = (short)(0x7FFF - (n >> 24));
            return (short)n;
        }
        static short[] resample(short[] samples, char type)
        {
            short[] output = new short[(int)(samples.Length / resample_var)];
            switch (type)
            {
                case 'n':								//No interpolation
                    for (int i = 0; i < output.Length; i++)
                    {
                        output[i] = samples[(int)(i * resample_var)];
                    }
                    break;
                case 'l':								//Linear interpolation
                    for (int i = 0; i < output.Length; i++)
                    {
                        int a = (int)(i * resample_var);		//Whole part of index
                        double b = i * resample_var - a;		//Fractional part of index
                        if ((a + 1) == samples.Length) output[i] = samples[a];	//This used only for the last sample
                        else output[i] = (short)((1 - b) * samples[a] + b * samples[a + 1]);
                    }
                    break;
                case 's':								//Sine interpolation
                    for (int i = 0; i < output.Length; i++)
                    {
                        int a = (int)(i * resample_var);
                        double b = i * resample_var - a;
                        double c = (1 - Math.Cos(b * Math.PI)) / 2;
                        if ((a + 1) == samples.Length) output[i] = samples[a];	//This used only for the last sample
                        else output[i] = (short)((1 - c) * samples[a] + c * samples[a + 1]);
                    }
                    break;
                case 'c':								//Cubic interpolation
                    samples = append(samples[0], samples);		//Duplicate first and last sample for interpolation
                    samples = append(samples, samples[samples.Length - 1]);
                    samples = append(samples, samples[samples.Length - 1]);
                    for (int i = 0; i < output.Length; i++)
                    {
                        int a = (int)(i * resample_var);
                        double b = i * resample_var - a;
                        double b2 = b * b;
                        double b3 = b2 * b;
                        double a0 = samples[a + 3] - samples[a + 2] - samples[a] + samples[a + 1];
                        double a1 = samples[a] - samples[a + 1] - a0;
                        double a2 = samples[a + 2] - samples[a];
                        output[i] = (short)(b3 * a0 + b2 * a1 + b * a2 + samples[a + 1]);
                    }
                    break;
                case 'g':									//SNES guassian interpolation (experimental)
                    return SNES_interpolate(samples, (int)(resample_var * 0x1000));
            }
            return output;
        }
        static short[] SNES_interpolate(short[] samples, int pitch)
        {
            short[] k = new short[4 - samples.Length & 0x03];	//Duplicate last sample from 1 to 3 times
            for (int i = 0; i < k.Length; i++)			//to get a fixed amount of 4-sample blocks
                k[i] = samples[samples.Length - 1];
            short[] output = new short[] { };
            samples = append(samples, k);
            int ptch_adder = 0;
            int a = 0;
            int j, d, outx;
            while (true)
            {
                ptch_adder += pitch;
                if (ptch_adder >= 0x4000)
                {
                    ptch_adder -= 0x4000;
                    a += 4;
                }
                if (a >= samples.Length - 4) break;
                j = ptch_adder >> 12;
                d = ptch_adder >> 4 & 0xFF;
                outx = (gauss[255 - d] * samples[a + j]) >> 11 + (gauss[511 - d] * samples[a + j + 1]) >> 11 + (gauss[256 + d] * samples[a + j + 2]) >> 11;
                outx = ((outx & 0x7FFF) ^ 0x4000) - 0x4000;
                outx += (gauss[d] * samples[a + j + 3]) >> 11;
                output = append(output, clamp_16(outx));
            }
            return output;
        }
        // gauss
        static readonly int[] gauss = new int[]
        {
            0x000, 0x000, 0x000, 0x000, 0x000, 0x000, 0x000, 0x000,
            0x000, 0x000, 0x000, 0x000, 0x000, 0x000, 0x000, 0x000,
            0x001, 0x001, 0x001, 0x001, 0x001, 0x001, 0x001, 0x001,
            0x001, 0x001, 0x001, 0x002, 0x002, 0x002, 0x002, 0x002,
            0x002, 0x002, 0x003, 0x003, 0x003, 0x003, 0x003, 0x004,
            0x004, 0x004, 0x004, 0x004, 0x005, 0x005, 0x005, 0x005,
            0x006, 0x006, 0x006, 0x006, 0x007, 0x007, 0x007, 0x008,
            0x008, 0x008, 0x009, 0x009, 0x009, 0x00A, 0x00A, 0x00A,
            0x00B, 0x00B, 0x00B, 0x00C, 0x00C, 0x00D, 0x00D, 0x00E,
            0x00E, 0x00F, 0x00F, 0x00F, 0x010, 0x010, 0x011, 0x011,
            0x012, 0x013, 0x013, 0x014, 0x014, 0x015, 0x015, 0x016,
            0x017, 0x017, 0x018, 0x018, 0x019, 0x01A, 0x01B, 0x01B,
            0x01C, 0x01D, 0x01D, 0x01E, 0x01F, 0x020, 0x020, 0x021,
            0x022, 0x023, 0x024, 0x024, 0x025, 0x026, 0x027, 0x028,
            0x029, 0x02A, 0x02B, 0x02C, 0x02D, 0x02E, 0x02F, 0x030,
            0x031, 0x032, 0x033, 0x034, 0x035, 0x036, 0x037, 0x038,
            0x03A, 0x03B, 0x03C, 0x03D, 0x03E, 0x040, 0x041, 0x042,
            0x043, 0x045, 0x046, 0x047, 0x049, 0x04A, 0x04C, 0x04D,
            0x04E, 0x050, 0x051, 0x053, 0x054, 0x056, 0x057, 0x059,
            0x05A, 0x05C, 0x05E, 0x05F, 0x061, 0x063, 0x064, 0x066,
            0x068, 0x06A, 0x06B, 0x06D, 0x06F, 0x071, 0x073, 0x075,
            0x076, 0x078, 0x07A, 0x07C, 0x07E, 0x080, 0x082, 0x084,
            0x086, 0x089, 0x08B, 0x08D, 0x08F, 0x091, 0x093, 0x096,
            0x098, 0x09A, 0x09C, 0x09F, 0x0A1, 0x0A3, 0x0A6, 0x0A8,
            0x0AB, 0x0AD, 0x0AF, 0x0B2, 0x0B4, 0x0B7, 0x0BA, 0x0BC,
            0x0BF, 0x0C1, 0x0C4, 0x0C7, 0x0C9, 0x0CC, 0x0CF, 0x0D2,
            0x0D4, 0x0D7, 0x0DA, 0x0DD, 0x0E0, 0x0E3, 0x0E6, 0x0E9,
            0x0EC, 0x0EF, 0x0F2, 0x0F5, 0x0F8, 0x0FB, 0x0FE, 0x101,
            0x104, 0x107, 0x10B, 0x10E, 0x111, 0x114, 0x118, 0x11B,
            0x11E, 0x122, 0x125, 0x129, 0x12C, 0x130, 0x133, 0x137,
            0x13A, 0x13E, 0x141, 0x145, 0x148, 0x14C, 0x150, 0x153,
            0x157, 0x15B, 0x15F, 0x162, 0x166, 0x16A, 0x16E, 0x172,
            0x176, 0x17A, 0x17D, 0x181, 0x185, 0x189, 0x18D, 0x191,
            0x195, 0x19A, 0x19E, 0x1A2, 0x1A6, 0x1AA, 0x1AE, 0x1B2,
            0x1B7, 0x1BB, 0x1BF, 0x1C3, 0x1C8, 0x1CC, 0x1D0, 0x1D5,
            0x1D9, 0x1DD, 0x1E2, 0x1E6, 0x1EB, 0x1EF, 0x1F3, 0x1F8,
            0x1FC, 0x201, 0x205, 0x20A, 0x20F, 0x213, 0x218, 0x21C,
            0x221, 0x226, 0x22A, 0x22F, 0x233, 0x238, 0x23D, 0x241,
            0x246, 0x24B, 0x250, 0x254, 0x259, 0x25E, 0x263, 0x267,
            0x26C, 0x271, 0x276, 0x27B, 0x280, 0x284, 0x289, 0x28E,
            0x293, 0x298, 0x29D, 0x2A2, 0x2A6, 0x2AB, 0x2B0, 0x2B5,
            0x2BA, 0x2BF, 0x2C4, 0x2C9, 0x2CE, 0x2D3, 0x2D8, 0x2DC,
            0x2E1, 0x2E6, 0x2EB, 0x2F0, 0x2F5, 0x2FA, 0x2FF, 0x304,
            0x309, 0x30E, 0x313, 0x318, 0x31D, 0x322, 0x326, 0x32B,
            0x330, 0x335, 0x33A, 0x33F, 0x344, 0x349, 0x34E, 0x353,
            0x357, 0x35C, 0x361, 0x366, 0x36B, 0x370, 0x374, 0x379,
            0x37E, 0x383, 0x388, 0x38C, 0x391, 0x396, 0x39B, 0x39F,
            0x3A4, 0x3A9, 0x3AD, 0x3B2, 0x3B7, 0x3BB, 0x3C0, 0x3C5,
            0x3C9, 0x3CE, 0x3D2, 0x3D7, 0x3DC, 0x3E0, 0x3E5, 0x3E9,
            0x3ED, 0x3F2, 0x3F6, 0x3FB, 0x3FF, 0x403, 0x408, 0x40C,
            0x410, 0x415, 0x419, 0x41D, 0x421, 0x425, 0x42A, 0x42E,
            0x432, 0x436, 0x43A, 0x43E, 0x442, 0x446, 0x44A, 0x44E,
            0x452, 0x455, 0x459, 0x45D, 0x461, 0x465, 0x468, 0x46C,
            0x470, 0x473, 0x477, 0x47A, 0x47E, 0x481, 0x485, 0x488,
            0x48C, 0x48F, 0x492, 0x496, 0x499, 0x49C, 0x49F, 0x4A2,
            0x4A6, 0x4A9, 0x4AC, 0x4AF, 0x4B2, 0x4B5, 0x4B7, 0x4BA,
            0x4BD, 0x4C0, 0x4C3, 0x4C5, 0x4C8, 0x4CB, 0x4CD, 0x4D0,
            0x4D2, 0x4D5, 0x4D7, 0x4D9, 0x4DC, 0x4DE, 0x4E0, 0x4E3,
            0x4E5, 0x4E7, 0x4E9, 0x4EB, 0x4ED, 0x4EF, 0x4F1, 0x4F3,
            0x4F5, 0x4F6, 0x4F8, 0x4FA, 0x4FB, 0x4FD, 0x4FF, 0x500,
            0x502, 0x503, 0x504, 0x506, 0x507, 0x508, 0x50A, 0x50B,
            0x50C, 0x50D, 0x50E, 0x50F, 0x510, 0x511, 0x511, 0x512,
            0x513, 0x514, 0x514, 0x515, 0x516, 0x516, 0x517, 0x517,
            0x517, 0x518, 0x518, 0x518, 0x518, 0x518, 0x519, 0x519
       };
    }
}
