using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using LAZYSHELL.Properties;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.LinkLabel;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;
using System.Drawing.Drawing2D;

namespace LAZYSHELL.Patches
{
    class Patch
    {
        // class variables
        private string patchFolder, patchURL;
        private string title, creationDate, description, features, size, website, version, patchName;
        private Uri patchURI;
        private bool recommend = false;
        private bool isFirstVersion = false;
        private int patchNum, patchImageCount = 0, currentPatchURLnum;
        private Uri readmeURI;
        private Bitmap[] patchImages = new Bitmap[0];
        private string[] patchImagesLink = new string[15];
        private string[] patchImagesDescriptions = new string[15];
        private List<string> categories = new List<string>();
        private List<string> authors = new List<string>();
        private Settings settings = Settings.Default;
        private WebClient client = new WebClient();
        private string UsingGitHubPath;
        private string UsingGitHubEnd;
        // public accessors
        public int PatchNum { get { return this.patchNum; } }
        public string Title { get { return title; } }
        public Bitmap[] PatchImages { get { return patchImages; } }
        public string[] PatchImagesDescriptions { get { return patchImagesDescriptions; } }
        public string CreationDate { get { return creationDate; } }
        public string Description { get { return description; } }
        public string Size { get { return this.size; } set { size = value; } }
        public string PatchName { get { return patchName; } }
        public Uri PatchURI { get { return patchURI; } set { patchURI = value; } }
        public string PatchURL { get { return patchURL; } }
        public Uri ReadmeURI { get { return readmeURI; } }
        public List<string> Categories { get { return categories; } set { categories = value; } }
        public List<string> Authors { get { return authors; } set { authors = value; } }
        public string Features { get { return features; } }
        public string Version { get { return version; } }
        public string Website { get { return website; } }
        public bool Recommend { get { return recommend; } }
        // additional versions connected to this one
        private List<Patch> extraVersions = new List<Patch>();
        public List<Patch> ExtraVersions { get { return extraVersions; } set { extraVersions = value; } }

        // constructor
        public Patch(int patchNum, string patchFolder, byte[] patch, int currentPatchURLnum, bool IsFirstVersion)
        {
            this.patchNum = patchNum;
            this.patchFolder = patchFolder;
            this.title = "";
            this.creationDate = "";
            this.description = "";
            this.features = "";
            this.size = "";
            this.website = "";
            this.isFirstVersion = IsFirstVersion;
            //
            this.currentPatchURLnum = currentPatchURLnum;
            UsingGitHubPath = Settings.Default.PatchServerURLs[currentPatchURLnum].StartsWith("https://github.com/") ? "blob/main/" : "patch/";
            UsingGitHubEnd = Settings.Default.PatchServerURLs[currentPatchURLnum].StartsWith("https://github.com/") ? "?raw=true" : "";

            Dissassemble(patch);
        }
        private void Dissassemble(byte[] patch)
        {
            MemoryStream ms = new MemoryStream(patch);
            StreamReader st = new StreamReader(ms);


            string file = st.ReadToEnd();
            //    MessageBox.Show(file);
            string[] fileArray = file.Split('\n');
            string[][] fileArrayNested = new string[fileArray.Length][];

            for (int q = 0; q < fileArray.Length; q++)
            {
                fileArrayNested[q] = fileArray[q].Split(':');
            }

            /*
            for (int m = 0; m < fileArray.Length; m++)
            {
                for (int d = 0; d < fileArrayNested[m].Length; d++)
                {
                    MessageBox.Show(fileArrayNested[m][d]);
                }
            }
            */

            int i = 0;
            string[] categoriesArray;
            string[] authorsArray;
            string[] imageDescriptionsArray;
            //string lastVar = "";
            for (int g = 0; g < fileArray.Length; g++) fileArray[g] = fileArray[g].Trim('\r', '\n');

            string a = fileArrayNested[0][0].ToLower();
            if (a != "name"
            && a != "title"
            && a != "author"
            && a != "authors"
            && a != "features"
            && a != "desc"
            && a != "description"
            && a != "file size"
            && a != "filesize"
            && a != "size"
            && a != "release date"
            && a != "creation date"
            && a != "date"
            && a != "image"
            && a != "preview"
            && a != "patch"
            && a != "version"
            && a != "link"
            && a != "site"
            && a != "website"
            && a != "tag"
            && a != "category"
            && a != "tags"
            && a != "categories")
            {
                title = fileArray[0];
                i++;
            }

            for (; i < fileArrayNested.Length;)
            {
                switch (fileArrayNested[i][0].ToLower())
                {
                    case "name":
                    case "title":
                        title = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ');
                        goto default;
                    case "author":
                    case "authors":
                        authorsArray = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ').Split(',');
                        foreach (string author in authorsArray)
                        {
                            authors.Add(author.Trim(' '));
                        }
                        goto default;
                    case "features":
                    case "desc":
                    case "description":
                        description += fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ');
                        description += "\r\n";
                        goto default;
                    case "file size":
                    case "filesize":
                    case "size":
                        size = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ');
                        goto default;
                    case "release date":
                    case "creation date":
                    case "date":
                        creationDate = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ');
                        goto default;
                    case "image":
                    case "preview":
                        imageDescriptionsArray = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).Split('|');
                        patchImagesLink[patchImageCount] = imageDescriptionsArray[0].TrimStart(' ');
                        if (imageDescriptionsArray.Length >= 2) patchImagesDescriptions[patchImageCount] = imageDescriptionsArray[1];
                        patchImageCount++;
                        goto default;
                    case "patch":
                        patchName = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).Trim(' ');
                        goto default;
                    case "version":
                        version = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).Trim(' ');
                        goto default;
                    case "link":
                    case "site":
                    case "website":
                        website = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ');
                        goto default;
                    case "tag":
                    case "category":
                    case "tags":
                    case "categories":
                        categoriesArray = fileArray[i].Substring(fileArrayNested[i][0].Length + 1).TrimStart(' ').Split(',');
                        foreach (string category in categoriesArray)
                        {
                            categories.Add(category.Trim(' '));
                        }
                        goto default;
                    case "favorite":
                    case "recommend":
                    case "recommended":
                        recommend = true;
                        goto default;
                    default:
                        //        lastVar = fileArrayNested[i][0];
                        i++;
                        break;
                }

                //
                /*
                    int numLoops = 0;
                    bool breakloop = false;
                    for ( ; i < fileArrayNested.Length; )
                    {
                        numLoops++;
                        int g = i - numLoops;
                    //    MessageBox.Show("numLoops : " + numLoops.ToString() + "\n" + fileArrayNested[g][0] + "\n" + lastVar);
                        switch (lastVar)
                        {
                            case "Title:":
                            case "Author:":
                            case "Description:":
                            case "Size:":
                            case "Date:":
                            case "Screenshots:":
                            case "Patch:":
                                if (fileArrayNested[g][0] != lastVar) breakloop = true;
                                break;
                        }
                        //
                        if (breakloop)
                        {
                            --i; break;
                        }
                        //
                        switch (fileArrayNested[g][0])
                        {
                            case "Title:":
                                goto default;
                            case "Author:":
                                author += "\r\n";
                                author += fileArray[i];
                                goto default;
                            case "Description:":
                                description += "\r\n";
                                description += fileArray[i];
                                goto default;
                            case "Size:":
                                size += "\r\n";
                                size += fileArray[i];
                                goto default;
                            case "Date:":
                                creationDate += "\r\n";
                                creationDate += fileArray[i];
                                goto default;
                            case "Screenshots:":
                                imageName += "\r\n";
                                imageName += fileArray[i];
                                goto default;
                            case "Patch:":
                                goto default;
                            default:
                                lastVar = fileArrayNested[i][0];
                                i++;
                                break;
                        }

                    }
                    //   MessageBox.Show(i.ToString());
                */
            }
            //
            //
            //
            if (patchName == "") return;

            patchURL = settings.PatchServerURLs[currentPatchURLnum] + UsingGitHubPath + this.patchFolder + "\\" + patchName + UsingGitHubEnd;
            if (!(patchURL.StartsWith("https") && patchName.ToLower().EndsWith(".ips") ))
            { return; }
            patchURI = new Uri(patchURL);


            if (isFirstVersion) CheckIfURLreadmeExists();
            DownloadImages();
        }
        private void DownloadImages()
        {
            int settingsMaxImages = Settings.Default.PatchServerMaxImageDownload;
            patchImages = new Bitmap[patchImageCount > settingsMaxImages ? settingsMaxImages : patchImageCount];
            for (int i = 0; i < patchImages.Length;)
            {
                try
                {
                    string imageLink = settings.PatchServerURLs[currentPatchURLnum] + UsingGitHubPath + this.patchFolder + "/" + patchImagesLink[i] + UsingGitHubEnd;
                    string imageName = patchImagesLink[i].ToLower();
                    if (settings.PatchServerURLs[currentPatchURLnum].Contains("https")
                        && (imageName.EndsWith(".bmp") || imageName.EndsWith(".jpg") || imageName.EndsWith(".gif") || imageName.EndsWith(".png")))
                    {
                        Stream st = client.OpenRead(imageLink);
                        patchImages[i] = new Bitmap(st);
                        //
                    }
                    else
                        patchImages[i] = null;
                }
                catch
                {
                    patchImages[i] = null;
                }
                try
                {
                    if (patchImages[i] != null && (patchImages[i].Width > 256 || patchImages[i].Height > 224))
                    {
                        Size resize = new Size(256, 224);
                        float nPercentW = ((float)resize.Width / (float)patchImages[i].Width);
                        float nPercentH = ((float)resize.Height / (float)patchImages[i].Height);
                        int destWidth = (int)(patchImages[i].Width * nPercentW);
                        int destHeight = (int)(patchImages[i].Height * nPercentH);
                        Bitmap b = new Bitmap(destWidth, destHeight);
                        Graphics g = Graphics.FromImage(b);
                        g.InterpolationMode = InterpolationMode.Bilinear;
                        g.DrawImage(patchImages[i], 0, 0, destWidth, destHeight);
                        g.Dispose();
                        patchImages[i] = b;
                    }
                }
                catch { }
                i++;
            }
            int countNull = 0;
            for (int i = 0; i < patchImages.Length; i++)
            {
                if (patchImages[i] == null)
                    countNull++;
            }
            if (countNull == 0) return;

            Bitmap[] newPatchImages = new Bitmap[patchImages.Length - countNull];
            for (int i = 0, v = 0; i < newPatchImages.Length; i++, v++)
            {
                if (patchImages[v] == null) v++;
                newPatchImages[i] = patchImages[v];
            }
            patchImages = new Bitmap[newPatchImages.Length];
            newPatchImages.CopyTo(patchImages, 0);
        }
        private async void CheckIfURLreadmeExists()
        {
            //
            HttpClient client = new HttpClient();
            string readmeLink = settings.PatchServerURLs[currentPatchURLnum] + UsingGitHubPath + this.patchFolder + "\\readme.txt" + UsingGitHubEnd;
            try
            {
                if (settings.PatchServerURLs[currentPatchURLnum].Contains("http"))
                {
                    HttpResponseMessage response = await client.GetAsync(readmeLink);
                    if (response.IsSuccessStatusCode)
                        readmeURI = new Uri(readmeLink);
                    /*
                    WebRequest request = WebRequest.Create(new Uri(readmeLink));
                    request.Method = "HEAD";
                    using (WebResponse response = request.GetResponse())
                    {
                        readmeURI = new Uri(readmeLink);
                    }
                    */
                }
            }
            catch { readmeURI = null; }
        }
    }
}
