using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using LAZYSHELL.Properties;

namespace LAZYSHELL.Patches
{
    class Patch
    {
        // class variables
        private string patchName, author, creationDate, description, size, extra;
        private int patchNum;
        private Uri patchURI;
        private Image patchImage;
        private byte flags;
        private string imageName;
        private Settings settings = Settings.Default;
        private WebClient client = new WebClient();
        // public accessors
        public int PatchNum { get { return this.patchNum; } }
        public string PatchName { get { return patchName; } }
        public Image PatchImage { get { return patchImage; } }
        public string Author { get { return author; } }
        public string CreationDate { get { return creationDate; } }
        public string Description { get { return description; } }
        public string Size { get { return this.size; } }
        public string Extra { get { return this.extra; } }
        public Uri PatchURI { get { return patchURI; } }
        public bool AssemblyHack { get { return Bits.GetBit(flags, 0); } }
        public bool GameHack { get { return Bits.GetBit(flags, 1); } }
        public bool FreshRom { get { return Bits.GetBit(flags, 2); } }
        // constructor
        public Patch(int patchNum, byte[] patch)
        {
            this.patchNum = patchNum;
            Dissassemble(patch);
        }
        private void Dissassemble(byte[] patch)
        {
            MemoryStream ms = new MemoryStream(patch);
            ms.Seek(0x1B, SeekOrigin.Current);
            //
            flags = Convert.ToByte(ms.ReadByte());
            //
            StreamReader st = new StreamReader(ms);
            patchName = st.ReadLine();
            author = st.ReadLine();
            size = st.ReadLine(); 
            creationDate = st.ReadLine();
            description = st.ReadLine();
            extra = st.ReadLine(); 
            imageName = st.ReadLine();
            patchURI = new Uri(settings.PatchServerURL + "patch" + this.patchNum.ToString() + "\\" + st.ReadLine());
            //
            DownloadImage();
        }
        private void DownloadImage()
        {
            try
            {
                Stream st = client.OpenRead(settings.PatchServerURL + "patch" + patchNum.ToString() + "\\" + imageName);
                patchImage = new Bitmap(st);
            }
            catch
            {
                patchImage = null;
            }
        }

    }
}
