using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public static class Comp
    {
        // external variables
        [DllImport("Lunar Compress.dll")]
        static extern int LunarOpenRAMFile([MarshalAs(UnmanagedType.LPArray)] byte[] data, int fileMode, int size);
        [DllImport("Lunar Compress.dll")]
        static extern int LunarDecompress([MarshalAs(UnmanagedType.LPArray)] byte[] destination, int addressToStart, int maxDataSize, int format1, int format2, int DoNotUseThisYet); // int * to save end addr for calculating size
        [DllImport("Lunar Compress.dll")]
        static extern int LunarSaveRAMFile(string fileName);
        [DllImport("Lunar Compress.dll")]
        static extern int LunarRecompress([MarshalAs(UnmanagedType.LPArray)] byte[] source, [MarshalAs(UnmanagedType.LPArray)] byte[] destination, uint dataSize, uint maxDataSize, uint format, uint format2);
        // compression functions
        public static int Compress(byte[] src, byte[] dst)
        {
            if (!LunarCompressExists())
                return -1;
            if (dst == null)
                return LunarRecompress(src, dst, (uint)src.Length, 0, 3, 3);
            else
                return LunarRecompress(src, dst, (uint)src.Length, (uint)dst.Length, 3, 3);
        }
        public static byte[] Decompress(byte[] data, int offset, int maxSize)
        {
            if (!LunarCompressExists())
                return null;
            //
            byte[] src = new byte[maxSize];
            byte[] dst = new byte[maxSize];
            for (int i = 0; ((i < src.Length) && ((offset + i) < data.Length)); i++)
                src[i] = data[offset + i]; // Copy over all the source data
            if (LunarOpenRAMFile(src, 0, src.Length) == 0) // Load source data as RAMFile
                return null;
            int size = LunarDecompress(dst, 0, dst.Length, 3, 0, 0);
            if (size != 0)
                return dst;
            return null;
        }
        public static int Decompress(byte[] data, byte[] dst, int offset, int maxSize)
        {
            if (!LunarCompressExists())
                return 0;
            //
            byte[] src = new byte[maxSize];
            for (int i = 0; ((i < src.Length) && ((offset + i) < data.Length)); i++)
                src[i] = data[offset + i]; // Copy over all the source data
            if (LunarOpenRAMFile(src, 0, src.Length) == 0) // Load source data as RAMFile
                return 0;
            int size = LunarDecompress(dst, 0, dst.Length, 3, 0, 0);
            return size;
        }
        // accessor functions
        public static bool LunarCompressExists()
        {
            if (!File.Exists("Lunar Compress.dll"))
            {
                byte[] lc = Resources.Lunar_Compress;
                File.WriteAllBytes(Path.GetDirectoryName(Application.ExecutablePath) + '\\' + "Lunar Compress.dll", lc);
            }
            return true;
        }
    }
}
