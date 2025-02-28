using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL.Patches
{
    public partial class GamePatches : NewForm
    {
        private Settings settings = Settings.Default;
        private float red, green, blue;
        private bool colorDirection = true; // darker
        //
        private bool downloading = false;
        private bool TaskCompleted = false;
        private readonly HttpClient client_patchList = new HttpClient();
        private readonly HttpClient client = new HttpClient();
        private List<HttpClient> client_patches = new List<HttpClient>();
        private int patchList_ProcessedNumber = 0;
        private int CurrentPatchServerNum = 0;
        private string[] patchList;
        private string[] patchListDebug;
        private List<string> UsingGitHubPath = new List<string>();
        private List<string> UsingGitHubEnd = new List<string>();
        //
        private List<Patch[]> patches = new List<Patch[]>();
        private List<Patch[]> patches_curated = new List<Patch[]>();
        private Patch currentPatch;
        private Patch[] currentPatchBase;
        private int currentPatchDisplayImageIndex = 0;
        //
        private bool VerifyPatch { get { return this.verifyIPSButton.Checked;  } set { this.verifyIPSButton.Checked = value; verifyIPSButton_Click(null, null); } }

        // constructor
        public GamePatches(DialogResult dialogueResult = DialogResult.Yes)
        {
            InitializeComponent();
            //
            UsingGitHubPath = new List<string>();
            UsingGitHubEnd = new List<string>();
            for (int i = 0; i < settings.PatchServerURLs.Count; i++)
            {
                patchHTTPServer.Items.Add(settings.PatchServerURLs[i]);
                UsingGitHubPath.Add(settings.PatchServerURLs[i].StartsWith("https://github.com/") ? "blob/main/" : "");
                UsingGitHubEnd.Add(settings.PatchServerURLs[i].StartsWith("https://github.com/") ? "?raw=true" : "");
            }
            this.imageMaxNum.Value = settings.PatchServerMaxImageDownload;
            VerifyPatch = settings.PatchServerVerify;
            imageStuffLabel.Text = "";
            GameImageDescriptions.Text = "";
            //
            sortByCategoryComboBox.SelectedIndex = 0;
            sortByAuthorComboBox.SelectedIndex = 0;
            //
            this.History = new History(this);
            Do.AddShortcut(toolStrip4, Keys.F1, helpTips);
            new ToolTipLabel(this, null, helpTips);

            if (dialogueResult == DialogResult.Yes)
                Startdownloading();
            else
                ResetParameters(2);
        }

        // functions
        private void ResetParameters(int degree)
        {
            this.client.CancelPendingRequests();
            this.client_patchList.CancelPendingRequests();

            foreach (HttpClient newClient in client_patches)
            {
                if (newClient == null) continue;
             
                newClient.Dispose();
            }
            client_patches.Clear();


            if (degree == 1)
            {
                if (PatchListBox.Items.Count >= 1
                    && PatchListBox.Items[0].ToString() != "(no patches available)")
                {
                    this.MainDetailsPanel.Visible = true;
                    this.ButtonsPanel.Visible = true;
                    //this.WebsitePanel.Visible = true;
                    this.applyButton.Text = "DOWNLOAD + APPLY PATCH";
                    this.applyButton.Visible = true;
                    this.downloadReadmeButton.Visible = true;
                    this.downloadIPStoFileButton.Enabled = true;
                    this.downloadIPStoFileButton.Visible = true;
                    //
                    this.applyButton.Enabled = true;
                    this.PatchListBox.Enabled = true;
                    this.sortByButtons.Enabled = true;
                    this.sortByCategoryComboBox.Enabled = true;
                    this.sortByAuthorComboBox.Enabled = true;
                }
                else if (PatchListBox.Items.Count == 0)
                    PatchListBox.Items.Add("(no patches available)");
                //
                ImagePictureBox.BackgroundImage = null;
                ImagePictureBox.BackgroundImageLayout = 0;
                //
                reloadPatchServer.Image = Resources.update;
                reloadPatchServer.ToolTipText = "Reload downloading for all patch servers";
            }

            if (degree > 0)
            {
                clock.CancelAsync();
                this.Updating = false;
                this.downloading = false;
                this.downloadingLabel.Visible = false;
                imageStuffLabel.Text = "";
                GameImageDescriptions.Text = "";

                if (patches.Count >= 1)
                {
                    versionDisplayComboBox.SelectedIndex = 0;
                    if (PatchListBox.SelectedIndex == -1)
                        PatchListBox.SelectedIndex = 0;
                    DisplayPatchInfo(0);
                }
            }

            if (degree > 1)
            {
                this.downloadReadmeButton.Visible = false;
                this.downloadReadmeButton.Enabled = false;
                this.downloadIPStoFileButton.Visible = false;
                this.downloadIPStoFileButton.Enabled = false;
                this.applyButton.Visible = false;
                this.applyButton.Enabled = false;
                sortByCategoryComboBox.Enabled = false;
                sortByCategoryComboBox.Items.Clear();
                sortByCategoryComboBox.Items.Add("All Categories");
                sortByCategoryComboBox.SelectedIndex = 0;
                sortByAuthorComboBox.Enabled = false;
                sortByAuthorComboBox.Items.Clear();
                sortByAuthorComboBox.Items.Add("All Authors");
                sortByAuthorComboBox.SelectedIndex = 0;
                versionDisplayComboBox.Items.Clear();
                versionDisplayComboBox.Items.Add("Version");
                versionDisplayComboBox.SelectedIndex = 0;
                patches = new List<Patch[]>();
                PatchListBox.Items.Clear();
                DescriptionTextBox.Text = null;
                ImagePictureBox.Image = null;
                this.PatchListBox.Enabled = false;
                //
                reloadPatchServer.Image = Resources.update;
                UsingGitHubPath = new List<string>();
                UsingGitHubEnd = new List<string>();
                for (int i = 0; i < settings.PatchServerURLs.Count; i++)
                {
                    patchHTTPServer.Items.Add(settings.PatchServerURLs[i]);
                    UsingGitHubPath.Add(settings.PatchServerURLs[i].StartsWith("https://github.com/") ? "blob/main/" : "");
                    UsingGitHubEnd.Add(settings.PatchServerURLs[i].StartsWith("https://github.com/") ? "?raw=true" : "");
                }

                if (PatchListBox.Items.Count == 0)
                    PatchListBox.Items.Add("(no patches available)");
                ImagePictureBox.BackgroundImage = null;
                ImagePictureBox.BackgroundImageLayout = 0;

                this.Updating = false;
                this.downloading = false;
            }
        }
        private bool GetWebResponse(Uri link)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(link);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Startdownloading()
        {
            this.Update();
            ResetParameters(0);
            this.downloading = true;
            this.Updating = true;
            this.downloadingLabel.Text = "...DOWNLOADING PATCH LIST...";
            this.downloadingLabel.Visible = true;
            clock.RunWorkerAsync();
            DownloadPatchesList();
        }
        private async void DownloadPatchesList()
        {
            try
            {
                if (!this.downloading) { return; }
                this.downloadingLabel.Text = "...DOWNLOADING PATCH INFO...";
                Uri link = new Uri(settings.PatchServerURLs[CurrentPatchServerNum] + UsingGitHubPath[CurrentPatchServerNum] + "patches" + UsingGitHubEnd[CurrentPatchServerNum]);
            //    if (!GetWebResponse(link)) return;
                Stream response = await client_patchList.GetStreamAsync(link);
                if (response == null) return;
                StreamReader st = new StreamReader(response);
                string file = st.ReadToEnd();
                DescriptionTextBox.Text = "Patches Loading In:\n" + "---------------------------------------------------------------------\n" + file;
                patchList = file.Split('\n');
                patchListDebug = file.Split('\n');
                patchList_ProcessedNumber = 0;

                DownloadPatchInfo(patchList_ProcessedNumber);
            }
            catch
            {
                if (!this.downloading) { return; }
                this.downloadingLabel.Text = "...RETRYING DOWNLOAD...";
                if (settings.PatchServerURLs.Count == CurrentPatchServerNum)
                {
                    ResetParameters(1);
                    return;
                }

                CurrentPatchServerNum++;
                DownloadPatchesList();
            }
        }
        private async void DownloadPatchInfo(int patchNum)
        {
            try
            {
                patchList_ProcessedNumber++;
                if (!this.downloading) { return; }
                this.downloadingLabel.Text = "...DOWNLOADING " + patchList[patchNum] + "...";
                //
                List <byte[]> linksStreams = new List<byte[]>();
                for (int b = 0; b < 256; b++)
                {
                    string s = b == 0 ? "" : b+"";
                    Uri link = new Uri(settings.PatchServerURLs[CurrentPatchServerNum] + UsingGitHubPath[CurrentPatchServerNum] + patchList[patchNum] + "/patchinfo" + s + UsingGitHubEnd[CurrentPatchServerNum]);
                    //if (!GetWebResponse(link)) continue;
                    byte[] response = new byte[0];
                    try
                    {
                        response = await client.GetByteArrayAsync(link);
                        if (response == null) throw new Exception();
                        linksStreams.Add(response);
                    }
                    catch
                    {
                        if (b == 0) continue;
                        break;
                    }
                }
                //
                if (linksStreams.Count == 0) throw new Exception();
                //
                for (int b = linksStreams.Count; b > 0; b--)
                {
                    await AddNewDownload(linksStreams[b - 1], b, linksStreams.Count);
                    if (TaskCompleted == false) throw new Exception();
                }
                //
                patchListDebug[patchList_ProcessedNumber - 1] = "";
                //
            }
            catch
            {
                if (!this.downloading) { return; }
                if (patchList_ProcessedNumber < patchList.Length - 1) patchListDebug[patchList_ProcessedNumber - 1] = patchList[patchList_ProcessedNumber - 1] + " (FAILED)";
                if (patchList_ProcessedNumber >= patchList.Length - 1)
                {
                    if (settings.PatchServerURLs.Count > CurrentPatchServerNum + 1)
                    {
                        CurrentPatchServerNum++;
                        DownloadPatchesList();
                        return;
                    }
                    this.Updating = false;
                    this.downloading = false;
                    ResetParameters(1);
                    return;
                }

            }
            DescriptionTextBox.Text = "Patches Loading In:\n" + "---------------------------------------------------------------------\n";
            foreach (string n in patchListDebug)
            {
                if (n == "") continue;
                DescriptionTextBox.Text += n + "\r\n";
            }
            DownloadPatchInfo(patchList_ProcessedNumber);
            sortByCategoryComboBox_SelectedIndexChanged(null, null);
        }
        public string GetSizeInMemory(long bytesize)
        {


            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = Convert.ToDouble(bytesize);
            int order = 0;
            while (len >= 1024D && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return string.Format(CultureInfo.CurrentCulture, "{0:0.##} {1}", len, sizes[order]);
        }
        private async Task AddNewDownload(byte[] data, int versionNum, int versionsAmount)
        {
            Patch patch = new Patch(patches.Count + 1, patchList[patchList_ProcessedNumber - 1], data, CurrentPatchServerNum, versionNum == versionsAmount ? true : false);
            if (patch.PatchName == "") { TaskCompleted = false; return; }
            //


            //    await Task.Delay(TimeSpan.FromSeconds(0.8));

            if (VerifyPatch)
            {
                try
                {
                    HttpClient newClient = new HttpClient();
                    client_patches.Add(newClient);

                    //    HttpResponseMessage response = await newClient.GetAsync(patch.PatchURI, HttpCompletionOption.ResponseHeadersRead);
                    //    if (!response.IsSuccessStatusCode) throw new Exception();

                    byte[] response = await newClient.GetByteArrayAsync(patch.PatchURI);
                    newClient.Dispose();
                    //client_patches.Remove(newClient);
                    IPSPatch ips = new IPSPatch(response);
                    if (!ips.Verified) throw new Exception();
                    patch.Size = GetSizeInMemory(response.Length);
                }
                catch
                {
                    TaskCompleted = false; return;
                }
            }

            if (versionNum == versionsAmount)
            {
                Patch[] patchArray = new Patch[versionsAmount];
                for (int i = 0; i < patchArray.Length; i++)
                    patchArray[i] = null;
                patchArray[0] = patch;
                patches.Add(patchArray);
                PatchListBox.Items.Add(patch.Title);
            }
            else
            {
                patches[patches.Count - 1][versionsAmount - versionNum] = patch;
            }

            Patch[] basePatch = patches[patches.Count - 1];

            try
            {
                foreach (Patch version in basePatch)
                {
                    if (!this.downloading) { return; }
                    if (version.Recommend
                        && !sortByCategoryComboBox.Items.Contains("Recommended"))
                            sortByCategoryComboBox.Items.Add("Recommended");
                    if (version.Categories.Count == 0
                        && !sortByCategoryComboBox.Items.Contains("Uncategorized"))
                            sortByCategoryComboBox.Items.Add("Uncategorized");
                    else
                    for (int a = 0; a < version.Categories.Count; a++)
                    {
                        if (sortByCategoryComboBox.Items.Contains(version.Categories[a])) continue;

                        sortByCategoryComboBox.Items.Add(version.Categories[a]);
                    }
                }
            }
            catch { }
            try
            {
                foreach (Patch version in basePatch)
                {
                    if (!this.downloading) { TaskCompleted = false; return; }
                    if (version.Authors.Count == 0
                        && !sortByAuthorComboBox.Items.Contains("No Author"))
                        sortByAuthorComboBox.Items.Add("No Author");
                    else
                        for (int a = 0; a < version.Authors.Count; a++)
                        {
                            if (sortByAuthorComboBox.Items.Contains(version.Authors[a])) continue;

                            sortByAuthorComboBox.Items.Add(version.Authors[a]);
                        }
                }
            }
            catch { }
            TaskCompleted = true;
        }
        private void DisplayPatchInfo(int version)
        {
            if (this.Updating) return;
            this.Updating = true;
            if (PatchListBox.SelectedIndex > patches_curated.Count) return;
            if (PatchListBox.SelectedIndex == -1) return;
            version = version < 0 ? 0 : version;
            string DIVIDER = "\r\n---------------------------------------------------------------------\r\n";

            Patch[] patch;
            try
            {
                patch = patches[patches_curated[PatchListBox.SelectedIndex][0].PatchNum - 1];
                currentPatchBase = patch;
                currentPatch = patches[patches_curated[PatchListBox.SelectedIndex][0].PatchNum - 1][version];
            }
            catch {
                MessageBox.Show(
                        "There was an error trying to display this patch.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; }
            //
            if (currentPatch == null) return;
            //

            /////////////
            // VERSION //
            /////////////
            versionDisplayComboBox.Items.Clear();
            for (int i = 0; i < patch.Length; i++)
                if (patch[i] != null)
                    versionDisplayComboBox.Items.Add(patch[i].Version == "" || patch[i].Version == null ? " " : patch[i].Version);

            versionDisplayComboBox.Visible = false;
            if (patch.Length == 1)
            {

                versionDisplayComboBox.Enabled = false;
                versionDisplayComboBox.SelectedIndex = 0;
                if (patch[0].Version != "" &&
                    patch[0].Version != null)
                {
                    versionDisplayComboBox.Visible = true;
                }
            }
            else
            {
                versionDisplayComboBox.Visible = true;
                versionDisplayComboBox.Enabled = true;
                versionDisplayComboBox.SelectedIndex = version;
            }

            ///////////
            // IMAGE //
            ///////////
            ImagePictureBox.Image = Resources.No_Image_Image;
            imageStuffLabel.Text = null;
            imageStuffButtonLeft.Enabled = false;
            imageStuffButtonRight.Enabled = false;
            currentPatchDisplayImageIndex = 0;
            //
            int patchCurrentImage = 0;

            if (patch[version].PatchImages.Length > 0) 
            {
                ImagePictureBox.Image = patch[version].PatchImages[patchCurrentImage];
            }

            if (patch[version].PatchImages.Length > 1)
            {
                imageStuffButtonLeft.Enabled = true;
                imageStuffButtonRight.Enabled = true;

                if (patch[version].PatchImages.Length > 1)
                    imageStuffLabel.Text = (patchCurrentImage + 1).ToString() + " / " + patch[version].PatchImages.Length.ToString();
            }
            //
            GameImageDescriptions.Text = "";
            if (patch[version].PatchImagesDescriptions[0] != "" && patch[version].PatchImages.Length != 0) GameImageDescriptions.Text = patch[version].PatchImagesDescriptions[0];

            //////////
            // MAIN //
            //////////
            DetailsLabel.Text = "Title:";
            DetailsBox.Text = " " + patch[0].Title;

            if (patch[version].Authors.Count == 0)
            {
                DetailsLabel.Text += "\r\n";
                DetailsBox.Text += "\r\n";
            }
            else if (patch[version].Authors.Count == 1)
            {
                DetailsLabel.Text += "\r\n" + "Author:";
                DetailsBox.Text += "\r\n " + patch[version].Authors[0];
            }
            else
            {
                DetailsLabel.Text += "\r\n" + "Authors:";
                DetailsBox.Text += "\r\n ";
                for (int a = 0; a < patch[version].Authors.Count; a++)
                {
                    DetailsBox.Text += patch[version].Authors[a];
                    if (patch[version].Authors.Count - a > 1) DetailsBox.Text += ", ";

                }
            }
            //
            if (patch[version].CreationDate != "")
            {
                DetailsLabel.Text += "\r\n" + "Date:";
                DetailsBox.Text += "\r\n " + patch[version].CreationDate;
            }
            else
            {
                DetailsLabel.Text += "\r\n";
                DetailsBox.Text += "\r\n";
            }
            //
            DetailsLabel.Text += "\r\n";
            DetailsLabel.Text += "Tags:";
            DetailsBox.Text += "\r\n ";
            //
            for (int a = 0; a < patch[version].Categories.Count; a++)
            {
                DetailsBox.Text += patch[version].Categories[a];
                if (patch[version].Categories.Count - a > 1) DetailsBox.Text += ", ";
            }
            //
            DescriptionTextBox.Text = "";
            if (patch[version].Description != "")
            {
                DescriptionTextBox.Text = patch[version].Description;
            }
            //
            WebsiteBox.Text = "";
            WebsiteBox.BackColor = SystemColors.ControlLight;
            this.WebsitePanel.Visible = false;
            if (patch[version].Website != "")
            {
                this.WebsitePanel.Visible = true;
                WebsiteBox.Text = patch[version].Website;
                WebsiteBox.BackColor = SystemColors.HighlightText;
            }
            //
            StarFavorite.Visible = false;
            if (patch[version].Recommend) StarFavorite.Visible = true;
            //
            //
            downloadReadmeButton.Enabled = false;
            if (patch[0].ReadmeURI != null) downloadReadmeButton.Enabled = true;
            //
            downloadIPStoFileButton.Text = patch[version].Size != "" ? "DOWNLOAD (" + patch[version].Size + ")" : "DOWNLOAD PATCH";
            //
            this.Updating = false;
        }
        private void StopDownload()
        {
            downloading = false;
            clock.CancelAsync();
            this.downloadingLabel.Visible = false;
            this.applyButton.Text = "DOWNLOAD + APPLY PATCH";
            client.CancelPendingRequests();
        }

        // event handlers
        private async void applyButton_Click(object sender, EventArgs e)
        {
            if (downloading)
            {
                StopDownload();
                return;
            }
            downloading = true;

            this.applyButton.Text = "CANCEL PATCH";
            this.downloadingLabel.Text = "...DOWNLOADING PATCH...";
            this.downloadingLabel.Visible = true;
            clock.RunWorkerAsync();
            try
            {
                byte[] response = await client.GetByteArrayAsync(currentPatch.PatchURI);
                IPSPatch ips = new IPSPatch(response);
                if (ips.Verified)
                {
                    DialogResult result = MessageBox.Show(
                        "Apply this patch to the currently open ROM image?\n\n" +
                        "Note: This will modify the current rom image, and must be saved in the editor before playing. " +
                        "Make sure to store a backup copy of the ROM image before patching.",
                        "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (ips.ApplyTo(Model.ROM))
                        {
                            // Needed to reset state for new rom image
                            Model.ClearModel();
                            State.Instance.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                            State.Instance2.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                            MessageBox.Show("Patch Applied Succesfully", "LAZYSHELL++");
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("There was an error downloading or applying the IPS patch.\n\nPlease try to download and apply it manually with LunarIPS.", "LAZYSHELL++");
            }
            clock.CancelAsync();
            this.Updating = false;
            this.downloading = false;
            this.downloadingLabel.Visible = false;
            this.applyButton.Text = "DOWNLOAD + APPLY PATCH";
        }
        private async void downloadReadmeButton_Click(object sender, EventArgs e)
        {
            if (downloading)
            {
                StopDownload();
                return;
            }
            try
            {
                downloading = true;
                this.downloadingLabel.Text = "...DOWNLOADING README...";
                this.downloadingLabel.Visible = true;
                clock.RunWorkerAsync();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = settings.LastDirectory;
                saveFileDialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FileName = currentPatch.PatchName.Substring(0, currentPatch.PatchName.Length - 4) + "_readme.txt";
                saveFileDialog.FilterIndex = 0;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var response = await client.GetAsync(currentPatch.ReadmeURI);
                    using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                    Process.Start(saveFileDialog.FileName);
                }
                else if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                }
            }
            catch
            {
                MessageBox.Show("There was an error downloading the readme.\n\nPlease try to download the readme manually.", "LAZYSHELL++");
            }
            this.Updating = false;
            this.downloading = false;
            this.downloadingLabel.Visible = false;
            clock.CancelAsync();
        }
        private async void downloadIPStoFileButton_Click(object sender, EventArgs e)
        {
            if (downloading)
            {
                StopDownload();
                return;
            }
            this.downloading = true;
            this.downloadIPStoFileButton.Text = "CANCEL DOWNLOAD";
            this.downloadingLabel.Text = "...DOWNLOADING PATCH...";
            this.downloadingLabel.Visible = true;
            clock.RunWorkerAsync();
            try
            {
                byte[] response = await client.GetByteArrayAsync(currentPatch.PatchURI);
                IPSPatch ips = new IPSPatch(response);
                if (!ips.Verified) throw new Exception();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = settings.LastDirectory;
                saveFileDialog.Filter = "Lunar IPS Patch (*.ips)|*.ips|All Files (*.*)|*.*";
                saveFileDialog.FileName = currentPatch.PatchName + ".ips";
                saveFileDialog.FilterIndex = 0;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var response2 = await client.GetAsync(currentPatch.PatchURI);
                    using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        await response2.Content.CopyToAsync(fs);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There was an error downloading the patch.\n\n It could be the download link is in the wrong letter-case, the file extension isn't \".ips\", or the download doesn't exist."
                    , "LAZYSHELL++");
            }
            this.downloadIPStoFileButton.Text = currentPatch.Size != "" ? "DOWNLOAD (" + currentPatch.Size + ")" : "DOWNLOAD PATCH";
            clock.CancelAsync();
            this.Updating = false;
            this.downloading = false;
            this.downloadingLabel.Visible = false;
        }
        private void PatchListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.Updating = false;
            DisplayPatchInfo(0);
        }
        private void versionDisplayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating) return;
            DisplayPatchInfo(versionDisplayComboBox.SelectedIndex);
        }
        private void WebsiteBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //Process.Start(e.LinkText);
        }
        private void clock_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!clock.CancellationPending)
            {
                Thread.Sleep(1000 / 30);
                if (red > 185 || red < 89)
                {
                    if (colorDirection)
                    {
                        red = 89;
                        green = 99;
                        blue = 219;
                        colorDirection = false;
                    }
                    else
                    {
                        red = 185;
                        green = 189;
                        blue = 240;
                        colorDirection = true;
                    }
                }
                if (colorDirection) // get darker
                {
                    red -= 96 / 30;
                    green -= 90 / 30;
                    blue -= 21 / 30;
                }
                else // Get Lighter
                {
                    red += 96 / 30;
                    green += 90 / 30;
                    blue += 21 / 30;
                }
                clock.ReportProgress(0);
            }
        }
        private void clock_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.downloadingLabel.BackColor = Color.FromArgb((int)red, (int)green, (int)blue);
        }

        private void imageStuffButtonRight_Click(object sender, EventArgs e)
        {
            if (currentPatch.PatchImages.Length <= 1) return;

            currentPatchDisplayImageIndex++;
            int failsafe = 0;
            for (; currentPatchDisplayImageIndex <= currentPatch.PatchImages.Length || failsafe >= currentPatch.PatchImages.Length; currentPatchDisplayImageIndex++, failsafe++)
            {
                if (currentPatchDisplayImageIndex > currentPatch.PatchImages.Length - 1) currentPatchDisplayImageIndex = 0;
                if (currentPatch.PatchImages[currentPatchDisplayImageIndex] == null) continue;
                break;
            }
            if (failsafe >= currentPatch.PatchImages.Length) { }
            else
            {
                ImagePictureBox.Image = currentPatch.PatchImages[currentPatchDisplayImageIndex];
                imageStuffLabel.Text = (1 + currentPatchDisplayImageIndex).ToString() + " / " + currentPatch.PatchImages.Length.ToString();
                GameImageDescriptions.Text = currentPatch.PatchImagesDescriptions[currentPatchDisplayImageIndex];
            }
        }

        private void imageStuffButtonLeft_Click(object sender, EventArgs e)
        {
            if (currentPatch.PatchImages.Length <= 1) return;

            currentPatchDisplayImageIndex--;
            int failsafe = currentPatchDisplayImageIndex;
            for (; currentPatchDisplayImageIndex <= currentPatch.PatchImages.Length || failsafe >= currentPatch.PatchImages.Length; currentPatchDisplayImageIndex--, failsafe++)
            {
                if (currentPatchDisplayImageIndex < 0) currentPatchDisplayImageIndex = currentPatch.PatchImages.Length - 1;
                if (currentPatch.PatchImages[currentPatchDisplayImageIndex] == null) continue;
                break;
            }
            if (failsafe >= currentPatch.PatchImages.Length) { }
            else
            {
                ImagePictureBox.Image = currentPatch.PatchImages[currentPatchDisplayImageIndex];
                imageStuffLabel.Text = (1 + currentPatchDisplayImageIndex).ToString() + " / " + currentPatch.PatchImages.Length.ToString();
                GameImageDescriptions.Text = currentPatch.PatchImagesDescriptions[currentPatchDisplayImageIndex];
            }
        }


        private void SortBy(string[] names)
        {
            string name;
            Patch[] name2;
            int length = names.Length;

            for (int a = 0; a < length - 1; a++)
            {
                for (int b = 0; b < length - 1 - a; b++)
                {
                    if (names[b + 1].CompareTo(names[b]) < 0)
                    {
                        name = names[b];
                        names[b] = names[b + 1];
                        names[b + 1] = name;

                        name2 = patches_curated[b];
                        patches_curated[b] = patches_curated[b + 1];
                        patches_curated[b + 1] = name2;
                    }
                }
            }
            PatchListBox.Items.Clear();
            foreach (Patch[] patch in patches_curated)
                PatchListBox.Items.Add(patch[0].Title);


            // Re-selects patch in list, if it's there
            if (currentPatchBase != null)
            {
                if (patches_curated.Contains(currentPatchBase))
                    PatchListBox.SelectedIndex = patches_curated.IndexOf(currentPatchBase);
                else PatchListBox.SelectedIndex = 0;
            }

        }
        private void GenerateCuratedList()
        {
            if (sortByCategoryComboBox.SelectedIndex < 0) return;
            if (sortByAuthorComboBox.SelectedIndex < 0) return;

            string currentCategory = sortByCategoryComboBox.Items[sortByCategoryComboBox.SelectedIndex].ToString();
            string currentAuthor = sortByAuthorComboBox.Items[sortByAuthorComboBox.SelectedIndex].ToString();

            patches_curated.Clear();
            foreach (Patch[] patch in patches)
            {
                if (patch == null) continue;
                //
                foreach (Patch version in patch)
                {
                    if (version == null) continue;
                    if (patches_curated.Contains(patch)) break;
                    //
                    if (currentCategory == "All Categories") { }
                    else if (currentCategory == "Uncategorized"
                        && version.Categories.Count == 0) { }
                    else if (currentCategory == "Recommended"
                        && version.Recommend) { }
                    else if (!version.Categories.Contains(currentCategory)) continue;

                    if (currentAuthor == "No Author"
                    && version.Authors.Count == 0)
                        patches_curated.Add(patch);
                    else if (currentAuthor != "All Authors")
                    {
                        if (version.Authors.Contains(currentAuthor))
                            patches_curated.Add(patch);
                    }
                    else patches_curated.Add(patch);
                }
            }
        }
        private void unsortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sortByButtons.Image = Resources.download;
            //
            foreach (ToolStripMenuItem sortby in sortByButtons.DropDownItems)
                sortby.Checked = false;
            unsortToolStripMenuItem.Checked = true;
            //
            GenerateCuratedList();

            string[] names = new string[patches_curated.Count];
            for (int a = 0; a < patches_curated.Count; a++)
                names[a] = patches_curated[a][0].PatchNum.ToString();
            SortBy(names);
        }

        private void sortByTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sortByButtons.Image = Resources.mainMainTitle;
            //
            foreach (ToolStripMenuItem sortby in sortByButtons.DropDownItems)
                sortby.Checked = false;
            sortByTitleToolStripMenuItem.Checked = true;
            //
            GenerateCuratedList();

            string[] names = new string[patches_curated.Count];
            for (int a = 0; a < patches_curated.Count; a++)
                names[a] = patches_curated[a][0].Title.ToString();
            SortBy(names);
        }

        private void sortByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sortByButtons.Image = Resources.history;
            //
            foreach (ToolStripMenuItem sortby in sortByButtons.DropDownItems)
                sortby.Checked = false;
            sortByDateToolStripMenuItem.Checked = true;
            //
            GenerateCuratedList();

            string[] names = new string[patches_curated.Count];
            for (int a = 0; a < patches_curated.Count; a++)
                names[a] = patches_curated[a][0].CreationDate.ToString();
            SortBy(names);
        }

        private void sortByCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            foreach (ToolStripMenuItem sortby in sortByButtons.DropDownItems)
                if (sortby.Checked) sortby.PerformClick();
        }

        private void sortByAuthorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            foreach (ToolStripMenuItem sortby in sortByButtons.DropDownItems)
                if (sortby.Checked) sortby.PerformClick();
        }

        private void reloadPatchServer_Click(object sender, EventArgs e)
        {
            if (this.Updating || this.downloading)
            {
                this.Updating = false;
                this.downloading = false;
                ResetParameters(1);
                return;
            }
            //    reloadPatchServer.Image = Resources.delete_small;
            //    ResetParameters(2);
            //    Startdownloading();
            Model.Program.Patches.Close();
            if (Model.Program.Patches == null || !Model.Program.Patches.Visible)
                Model.Program.CreatePatchesWindow();
        }
        private void openSettings_DropDownOpened(object sender, EventArgs e)
        {
            for (int i = 0; i < patchHTTPServer.Items.Count; i++)
            {
                if (!patchHTTPServer.Items[i].ToString().EndsWith("/"))
                    patchHTTPServer.Items[i] += "/";
                if (!patchHTTPServer.Items[i].ToString().StartsWith("https:"))
                    patchHTTPServer.Items.RemoveAt(i);
            }
            patchHTTPServer.Items.Clear();
            for (int i = 0; i < settings.PatchServerURLs.Count; i++)
            {
                patchHTTPServer.Items.Add(settings.PatchServerURLs[i].ToString());
            }
        }

        private void openSettings_DropDownClosed(object sender, EventArgs e)
        {
            for (int i = 0; i < patchHTTPServer.Items.Count; i++)
            {
                if (!patchHTTPServer.Items[i].ToString().EndsWith("/"))
                    patchHTTPServer.Items[i] += "/";
                if (!patchHTTPServer.Items[i].ToString().StartsWith("https:"))
                    patchHTTPServer.Items.RemoveAt(i);
            }
            settings.PatchServerURLs.Clear();
            for (int i = 0; i < patchHTTPServer.Items.Count; i++)
            {
                settings.PatchServerURLs.Add(patchHTTPServer.Items[i].ToString());
            }
            settings.Save();
        }

        private void patchHTTPServer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                openSettings.Dispose();

            if (e.KeyData == Keys.Enter)
                if (this.patchHTTPServer.Text != "" || this.patchHTTPServer.Text != null)
                {
                    if (!this.patchHTTPServer.Text.ToString().StartsWith("https:"))
                        MessageBox.Show("The server must start with \"https:\" and end with a forward slash.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else if (!patchHTTPServer.Items.Contains(this.patchHTTPServer.Text))
                    {
                        if (!this.patchHTTPServer.Text.ToString().EndsWith("/"))
                            this.patchHTTPServer.Text += "/";
                        patchHTTPServer.Items.Add(this.patchHTTPServer.Text);
                    }
                }

            if (e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
                patchHTTPServer.Items.RemoveAt(patchHTTPServer.FindStringExact(this.patchHTTPServer.Text));

            //
            for (int i = 0; i < patchHTTPServer.Items.Count; i++)
            {
                if (this.patchHTTPServer.Text == "")
                    patchHTTPServer.Items.RemoveAt(patchHTTPServer.FindStringExact(this.patchHTTPServer.Text));
            }
            //
        }

        private void patchHTTPServer_TextUpdate(object sender, EventArgs e)
        {
            if (patchHTTPServer.SelectedIndex == -1) return;
            patchHTTPServer.Items[patchHTTPServer.SelectedIndex] = patchHTTPServer.Text;

        }
        private void patchHTTPServer_TextChanged(object sender, EventArgs e)
        {
            patchHTTPServer.SelectedIndex = patchHTTPServer.FindStringExact(patchHTTPServer.Text);
        }

        private void imageMaxNum_ValueChanged(object sender, EventArgs e)
        {
            settings.PatchServerMaxImageDownload = (int)this.imageMaxNum.Value;
            settings.Save();
        }

        private void verifyIPSButton_Click(object sender, EventArgs e)
        {
            settings.PatchServerVerify = this.verifyIPSButton.Checked;
            settings.Save();
            if (this.verifyIPSButton.Checked) this.verifyIPSButton.Image = Resources.shield_check;
            else this.verifyIPSButton.Image = Resources.shield_question;
        }

        private void clock_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private void GamePatches_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clock.IsBusy)
                clock.CancelAsync();

            this.downloading = false;
            ResetParameters(2);
        }
    }
}