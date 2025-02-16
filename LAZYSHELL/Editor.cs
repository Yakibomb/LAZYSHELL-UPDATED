using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;
using LAZYSHELL.Properties;
using System.Runtime;
using static LAZYSHELL.NotesDB;

namespace LAZYSHELL
{
    public partial class Editor : NewForm, IMRUClient
    {
        #region Variables
        private ProgramController AppControl;
        private Settings settings = Settings.Default;
        private bool cancelAnotherLoad;
        // MRU List manager
        private MRUManager mruManager;      // MRU list manager
        private string initialDirectory;    // Initial directory for Save/Load operations
        const string registryPath = "SOFTWARE\\LAZYSHELL\\LazyShell";  // Registry path to keep persistent data
        [DllImport("advapi32.dll", EntryPoint = "RegDeleteKey")]
        public static extern int RegDeleteKeyA(int hKey, string lpSubKey);
        private Restore restore;
        public Panel Panel2 { get { return panel2; } set { panel2 = value; } }
        //
        private ToolStripItem[] EditorMainLayoutDefaults = new ToolStripItem[0];
        private bool DockTrayHidden { get { return settings.EditorMainDockTrayHide; } set { settings.EditorMainDockTrayHide = value; } }
        private bool UseBigIcons { get { return settings.EditorMainLayout; } set { settings.EditorMainLayout = value; } }

        //
        public ToolStripButton buttonAllies { get { return this.openAllies; } set { this.openAllies = value; } }
        public ToolStripButton buttonAnimations { get { return this.openAnimations; } set { this.openAnimations = value; } }
        public ToolStripButton buttonAttacks { get { return this.openAttacks; } set { this.openAttacks = value; } }
        public ToolStripButton buttonAudio { get { return this.openAudio; } set { this.openAudio = value; } }
        public ToolStripButton buttonBattlefields { get { return this.openBattlefields; } set { this.openBattlefields = value; } }
        public ToolStripButton buttonDialogues { get { return this.openDialogues; } set { this.openDialogues = value; } }
        public ToolStripButton buttonEffects { get { return this.openEffects; } set { this.openEffects = value; } }
        public ToolStripButton buttonEventScripts { get { return this.openEventScripts; } set { this.openEventScripts = value; } }
        public ToolStripButton buttonFormations { get { return this.openFormations; } set { this.openFormations = value; } }
        public ToolStripButton buttonIntro { get { return this.openMainTitle; } set { this.openMainTitle = value; } }
        public ToolStripButton buttonItems { get { return this.openItems; } set { this.openItems = value; } }
        public ToolStripButton buttonLevels { get { return this.openLevels; } set { this.openLevels = value; } }
        public ToolStripButton buttonMenus { get { return this.openMenus; } set { this.openMenus = value; } }
        public ToolStripButton buttonMiniGames { get { return this.openMiniGames; } set { this.openMiniGames = value; } }
        public ToolStripButton buttonMonsters { get { return this.openMonsters; } set { this.openMonsters = value; } }
        public ToolStripButton buttonSprites { get { return this.openSprites; } set { this.openSprites = value; } }
        public ToolStripButton buttonWorldMaps { get { return this.openWorldMaps; } set { this.openWorldMaps = value; } }
        public ToolStripButton buttonProject { get { return this.openProject; } set { this.openProject = value; } }
        public ToolStripButton buttonPatches { get { return this.openPatches; } set { this.openPatches = value; } }
        #endregion

        // Constructor
        public Editor(ProgramController controls)
        {
            this.AppControl = controls;
            //
            InitializeComponent();
            //LoadWebpage();
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(saveToolStripMenuItem_Click));
            loadRomTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            // MRU
            LoadSettingsFromRegistry();
            mruManager = new MRUManager();
            mruManager.Initialize(this, recentFiles, registryPath);
            //
            if (settings.LoadLastUsedROM && mruManager.MRUList.Count > 0)
            {
                try
                {
                    Open((string)mruManager.MRUList[0]);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load most recently used ROM.\n\n" + e.Message,
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // create backup list collections BEFORE loading notes
            Model.CreateListCollections();
            if (settings.LoadNotes && settings.NotePathCustom != "")
            {
                try
                {
                    OpenProject(settings.NotePathCustom);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load most recently used project database.\n\n" + e.Message,
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // load layout
            // set defaults for layout
            EditorMainLayoutDefaults = new ToolStripItem[] {
                this.openAllies,
                this.openAnimations,
                this.openAttacks,
                this.openAudio,
                this.openBattlefields,
                this.openDialogues,
                this.openEffects,
                this.openEventScripts,
                this.openFormations,
                this.openMainTitle,
                this.openItems,
                this.openLevels,
                this.openMenus,
                this.openMiniGames,
                this.openMonsters,
                this.openSprites,
                this.openWorldMaps,
                this.toolStripSeparator1,
                this.openProject,
                this.openPatches,
            };

            //Layout update

            //settings.Reset();
            InitializeLayout();
            LoadLayout();

            //
            this.History = new History(this);
            if (!settings.FirstLoad)
                Help.CreateHelp(Model.LAZYSHELL_xml, true);
            settings.FirstLoad = true;
        }
        #region Function
        public static void GuiMain(ProgramController AppControl)
        {
            // Start the application.
            //Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Editor(AppControl));
        }
        // Loading
        private void LoadWebpage()
        {
            XmlDocument LAZYSHELL_help = Model.LAZYSHELL_xml;
            XmlNodeList nodes = LAZYSHELL_help.SelectNodes("//section");
            string documentText = "<html><head><style>";
            documentText += Resources.LAZYSHELL_css;
            documentText += "</style></head>";
            documentText += "<body>";
            foreach (XmlNode node in nodes)
            {
                XmlNode header = node.SelectSingleNode(".//header");
                documentText += "<h1 class=\"subwindow\">" + header.InnerText + "</h1>";
                documentText += "<div class=\"subwindow\">";
                documentText += "<p class=\"subwindow\">";
                XmlNode body = node.SelectSingleNode(".//body");
                documentText += body.InnerText.Replace("\r\n", "<br/>");
                documentText += "</p></div>";
            }
            documentText += "<br/></body></html>";
            webBrowser1.DocumentText = documentText;
        }
        private void Open(string filename)
        {
            if (AppControl.AssembleAndCloseWindows())
            {
                MessageBox.Show("All of the editor's windows must be closed before loading a new ROM.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Model.HexEditor != null && Model.HexEditor.Visible)
                Model.HexEditor.Close();
            bool ret;
            if (filename == null) // Load the rom
                ret = AppControl.OpenRomFile();
            else
                ret = AppControl.OpenRomFile(filename);
            if (ret && !AppControl.Locked()) // Verify it is a SMRPG rom of the correct version
            {
                if (AppControl.GameCode() != "ARWE")
                {
                    if (MessageBox.Show("The game code for this ROM is invalid. There will likely be problems editing the ROM.\n\nLoad anyways?",
                        "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
                openEditorsTray.Enabled = true;
                toolStrip3.Enabled = true;
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != recentFiles && item != openSettings)
                        item.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.restoreElementsToolStripMenuItem.Enabled = true;
                AppControl.CreateNewMd5Checksum(); // Create a new checksum for a new rom
                UpdateRomInfo();
            }
            else if (ret)
            {
                if (AppControl.Locked())
                {
                    this.saveToolStripMenuItem.Enabled = true;
                    this.saveAsToolStripMenuItem.Enabled = true;
                    this.restoreElementsToolStripMenuItem.Enabled = true;
                    UpdateRomInfo();
                }
                openEditorsTray.Enabled = false;
                toolStrip3.Enabled = false;
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != recentFiles && item != openSettings)
                        item.Enabled = false;
            }
            if (ret)
            {
                mruManager.Add(AppControl.GetPathWithoutFileName() + AppControl.GetFileNameWithoutPath());
                settings.Save();
            }
            if (openEditorsTray.Enabled && settings.LoadAllData)
                AppControl.LoadAll();
        }
        private void OpenProject(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("Error loading last used database. The file has been moved, renamed, or no longer exists.",
                    "LAZYSHELL++");
                return;
            }
            Stream s = File.OpenRead(filename);
            BinaryFormatter b = new BinaryFormatter();
            Model.Project = (ProjectDB)b.Deserialize(s);
            Model.RefreshListCollections();
            s.Close();

            //AppControl.Project();
        }
        private void CloseROM()
        {
            if (AppControl.AssembleAndCloseWindows())
                return;
            else if (Model.HexEditor != null && Model.HexEditor.Visible)
                Model.HexEditor.Close();
            AppControl.CloseRomFile();
            openEditorsTray.Enabled = false;
            toolStrip3.Enabled = false;
            foreach (ToolStripItem item in toolStrip4.Items)
                if (item != recentFiles && item != openSettings && item != info)
                    item.Enabled = false;
            this.loadRomTextBox.Text = "";
            this.romInfo.Text = "";
        }
        public void UpdateRomInfo()
        {
            this.loadRomTextBox.Text = Model.GetFileNameWithoutPath();
            this.romInfo.Text =
                AppControl.GetRomName() + "\n" +
                AppControl.HeaderPresent() + "\n" +
                AppControl.RomChecksum() + "\n" +
                AppControl.GameCode();
        }
        // Closing
        private void FinalizeAndSave(FormClosingEventArgs e, int assembleFlag)
        {
            DialogResult result;
            if (e != null && AppControl.AssembleAndCloseWindows())
            {
                e.Cancel = true;
                return;
            }
            if (!AppControl.VerifyMD5Checksum())
            {
                result = MessageBox.Show(
                    "There are changes to the rom that have not been saved.\n\n" +
                    "Would you like to save them now" + (assembleFlag == 1 ? " and quit?" : "?"), "LAZYSHELL++",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (!AppControl.SaveRomFile())
                    {
                        MessageBox.Show(
                            "There was an error saving to \"" + Model.FileName + "\"",
                            "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                {
                    if (e != null)
                        e.Cancel = true;
                    cancelAnotherLoad = true;
                    AppControl.Assemble();
                    return;
                }
                else cancelAnotherLoad = false;
            }
            //
            SaveLayout();
            //
            if (e != null)
            {
                this.Dispose();
                Application.Exit();
            }
        }
        // Notes
        private string GetDirectoryPath(string caption)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = settings.LastDirectory;
            folderBrowserDialog1.Description = caption;
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.LastDirectory = folderBrowserDialog1.SelectedPath;
                return folderBrowserDialog1.SelectedPath;
            }
            else
                return null;
        }
        // MRU list manager
        public void OpenMRUFile(string fileName)
        {
            try
            {
                Open(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not load most recently used ROM.\n\n" + e.Message,
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSettingsFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);
                initialDirectory = (string)key.GetValue(
                    "InitDir",                          // value name
                    Directory.GetCurrentDirectory());   // default value
            }
            catch
            {
                Trace.WriteLine("LoadSettingsFromRegistry failed");
            }
        }

        // custom layout manager

        public void InitializeLayout()
        {
            layoutUpdate.Checked = UseBigIcons;
            hideDock.Checked = DockTrayHidden;

            LayoutHideDock();
            layoutUpdate_Click(null, null);
            //
            switch (settings.EditorMain_showROMInfo)
            {
                case 1:
                    infoROMloaded.Visible = true;
                    infoROM.Visible = true;
                    break;
                case 2:
                    infoROMloaded.Visible = true;
                    infoROM.Visible = false;
                    break;
                case 3:
                    infoROMloaded.Visible = false;
                    infoROM.Visible = false;
                    break;
                default:
                    goto case 3;
            };

        }
        public void LoadLayout()
        {
            openEditorsTray.Items.Clear();

            //
            foreach (string editorSetting in settings.EditorMainLayoutList)
            {
                foreach (ToolStripItem editor in EditorMainLayoutDefaults)
                {
                    if (editorSetting == editor.ToString())
                    {
                        openEditorsTray.Items.Add(editor);
                        break;
                    }
                    if (editorSetting.StartsWith("Event") && editor.ToString().StartsWith("Event"))
                    {
                        openEditorsTray.Items.Add(editor);
                        break;
                    }
                }
            }

            if (UseBigIcons)
            {

                this.openEditorsTray.LayoutStyle = ToolStripLayoutStyle.Flow;
                this.toolStripSeparator1.Margin = !DockTrayHidden ? new Padding(0) : new Padding(10);
                this.toolStripSeparator1.Size = new Size(1, 1);
                //this.layoutUpdate.Image = Resources.zoomout_small;


                foreach (ToolStripItem button in openEditorsTray.Items)
                {
                    if (button == toolStripSeparator1)
                        continue;
                    button.Size = !DockTrayHidden ? new Size(64, 64) : new Size(44, 44);
                    button.TextImageRelation = TextImageRelation.ImageAboveText;
                    button.TextAlign = ContentAlignment.BottomCenter;
                    button.DisplayStyle = !DockTrayHidden ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.Margin = !DockTrayHidden ? new Padding(5) : new Padding(3, 2, 0, 0);
                    button.ImageScaling = ToolStripItemImageScaling.None;

                    if (button == openEventScripts)
                        button.Text = "Events";
                }
            }
            else
            {

                this.openEditorsTray.LayoutStyle = !DockTrayHidden ? ToolStripLayoutStyle.Flow : ToolStripLayoutStyle.VerticalStackWithOverflow;
                this.toolStripSeparator1.Margin = new Padding(0);
                this.toolStripSeparator1.Size = !DockTrayHidden ? new Size(1, 1) : new Size(40, 6);
                //this.layoutUpdate.Image = Resources.zoomin_small;

                foreach (ToolStripItem button in openEditorsTray.Items)
                {
                    if (button == toolStripSeparator1)
                        continue;
                    button.Size = !DockTrayHidden ? new Size(64, 40) : new Size(90, 20);
                    button.TextImageRelation = !DockTrayHidden ? TextImageRelation.ImageAboveText: TextImageRelation.ImageBeforeText;
                    button.TextAlign = !DockTrayHidden ? ContentAlignment.BottomCenter : ContentAlignment.MiddleLeft;
                    button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    button.ImageAlign = !DockTrayHidden ? ContentAlignment.TopCenter : ContentAlignment.MiddleLeft;
                    button.Margin = !DockTrayHidden ? new Padding(2) : new Padding(2, 1, 0, 2);
                    button.ImageScaling = ToolStripItemImageScaling.SizeToFit;


                    if (button == openEventScripts)
                        button.Text = !DockTrayHidden ? "Event\nScripts" : "Event Scripts";

                }
            }
            //
            //
        }

        private void SaveLayout()
        {
            settings.EditorMainLayoutList.Clear();

            for (int i = 0; i < EditorMainLayoutDefaults.Length; i++)
            {
                settings.EditorMainLayoutList.Add(openEditorsTray.Items[i].ToString());
            }
            UseBigIcons = layoutUpdate.Checked;

            settings.Save();
        }

        #endregion
        #region Event Handlers
        // Main buttons
        private void loadRom_Click(object sender, System.EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(null);
        }
        private void toolStrip1_SizeChanged(object sender, EventArgs e)
        {
            loadRomTextBox.Width = infoROMloaded.Width - 105;
        }
        // toolstripMenuItems : File
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(null);
        }
        private void refreshROM_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                Open(Model.FileName);
        }
        private void closeROM_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
                FinalizeAndSave(null, 0);
            if (!cancelAnotherLoad)
                CloseROM();
        }
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Check if read only, if it is do a "Save As" routine
            FileInfo file = new FileInfo(Model.FileName);
            if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                saveAsToolStripMenuItem.PerformClick();
                return;
            }
            // Check if currently in use by another application
            FileStream fs = null;
            try
            {
                fs = File.Open(Model.FileName, FileMode.Open);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Lazy Shell could not save the ROM.\n\nThe file is currently in use by another application.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Now, save the file
            AppControl.SaveRomFile();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppControl.Assemble();
            if (AppControl.SaveRomFileAs())
            {
                UpdateRomInfo();
                mruManager.Add(AppControl.GetPathWithoutFileName() + AppControl.GetFileNameWithoutPath());
                settings.Save();
            }
            else
                MessageBox.Show("Lazy Shell could not save the ROM.\n\nMake sure that the file is not currently in use by another appliaction.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void restoreElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppControl.AssembleAndCloseWindows())
            {
                MessageBox.Show("All of the editor's windows must be closed before importing data from another ROM.", "LAZYSHELL++");
                return;
            }
            restore = new Restore();
            restore.ShowDialog();
        }
        private void publishRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppControl.Publish())
                UpdateRomInfo();
        }
        private void openSettings_Click(object sender, EventArgs e)
        {
            new SettingsEditor().ShowDialog();
        }
        // toolStripMenuitems : Help
        private void history_Click(object sender, EventArgs e)
        {
            NewMessage.Show("EVENT HISTORY - LAZYSHELL++",
                "This is a list of past actions performed by the user exclusively within the Lazy Shell application. " +
                "This is to be used for debugging purposes, chiefly for reproducing bugs and other defects encountered by the user.",
                Model.History, 600, 600, true);
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            Model.HexEditor.Show();
            Model.HexEditor.BringToFront();
        }
        private void helpToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            string path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf('\\') + 1) + "helpTopics\\index.html";
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Could not load the index help file. Try unzipping the program's files again.", "LAZYSHELL++");
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form about = new About(this);
            about.ShowDialog(this);
        }
        private void help_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Open the help database as a navigable web page?\n\n" +
                "Selecting \"No\" will open the help file as a raw text file.",
                "LAZYSHELL++", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            XmlDocument LAZYSHELL_xml = Model.LAZYSHELL_xml;
            if (result == DialogResult.No)
            {
                Help.CreateHelp(LAZYSHELL_xml, false);
                return;
            }
            XmlNodeList icons = LAZYSHELL_xml.SelectNodes(".//*[@icon != '']");
            if (!Directory.Exists("help"))
                Directory.CreateDirectory("help");
            if (!Directory.Exists("help//icons"))
                Directory.CreateDirectory("help//icons");
            File.WriteAllText("help//LAZYSHELL_xml.xml", Resources.LAZYSHELL_xml);
            File.WriteAllText("help//LAZYSHELL_xsl.xsl", Resources.LAZYSHELL_xsl);
            File.WriteAllText("help//LAZYSHELL_css.css", Resources.LAZYSHELL_css);
            foreach (XmlNode icon in icons)
            {
                string path = icon.Attributes["icon"].Value;
                string file = Path.GetFileName(path);
                string name = Path.GetFileNameWithoutExtension(path);
                Bitmap image = (Bitmap)Resources.ResourceManager.GetObject(name);
                if (image == null)
                    continue;
                image.Save("help//icons//" + file);
            }
            Process.Start("help\\LAZYSHELL_xml.xml");
        }
        // other
        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Model.Crashing)
                return;
            FinalizeAndSave(e, 1);
            settings.Save();
        }
        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        // Editor buttons
        private void openAllies_Click(object sender, EventArgs e)
        {
            AppControl.Allies();
            openAllies.Checked = true;
        }
        private void openAnimations_Click(object sender, EventArgs e)
        {
            AppControl.Animations();
        }
        private void openAttacks_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Attacks();
        }
        private void openAudio_Click(object sender, EventArgs e)
        {
            AppControl.Audio();
        }
        private void openBattlefields_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Battlefields();
        }
        private void openDialogues_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Dialogues();
        }
        private void openEffects_Click(object sender, EventArgs e)
        {
            AppControl.Effects();
        }
        private void openEventScripts_Click(object sender, System.EventArgs e)
        {
            AppControl.Scripts();
        }
        private void openFormations_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Formations();
        }
        private void openItems_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Items();
        }
        private void openLevels_Click(object sender, System.EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Levels();
        }
        private void openMainTitle_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.MainTitle();
        }
        private void openMenus_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Menus();
        }
        private void openMiniGames_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.MiniGames();
        }
        private void openMonsters_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.Monsters();
        }
        private void openSprites_Click(object sender, System.EventArgs e)
        {
            //Do.CompareImages();
            AppControl.Sprites();
        }
        private void openWorldMaps_Click(object sender, EventArgs e)
        {
            if (!Comp.LunarCompressExists())
                return;
            AppControl.WorldMaps();
        }
        private void openPatches_Click(object sender, EventArgs e)
        {
            AppControl.Patches();
        }
        private void openProject_Click(object sender, EventArgs e)
        {
            AppControl.Project();
        }
        // window editing
        private void docking_Click(object sender, EventArgs e)
        {
        //    webBrowser1.Visible = !docking.Checked;
            AppControl.DockEditors = docking.Checked;
            if (docking.Checked)
                AppControl.Dock();
            else
                AppControl.Undock();
        }
        private void openAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to open all 17 editor windows.\n\nAre you sure you want to do this?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            AppControl.OpenAll();
        }
        private void closeAll_Click(object sender, EventArgs e)
        {
            AppControl.CloseAll();
        }
        private void minimizeAll_Click(object sender, EventArgs e)
        {
            AppControl.MinimizeAll();
        }
        private void restoreAll_Click(object sender, EventArgs e)
        {
            AppControl.RestoreAll();
        }
        private void loadAllData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "You are about to reset the editor's memory of all elements. Continue?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            AppControl.ClearAll();
            AppControl.LoadAll();
        }
        private void clearModel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "You are about to clear the editor's memory of all elements. Continue?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            AppControl.ClearAll();
        }
        private void loadRomTextBox_Click(object sender, EventArgs e)
        {

            // throw new Exception("test");

            if (this.loadRomTextBox.Text == "")
                return;

            if (this.loadRomTextBox.Text != Model.FileName)
                this.loadRomTextBox.Text = Model.FileName;
            else
                this.loadRomTextBox.Text = Model.GetFileNameWithoutPath();
        }

        private void layoutUpdate_Click(object sender, EventArgs e)
        {
            UseBigIcons = layoutUpdate.Checked;
            settings.Save();

            SaveLayout();
            LoadLayout();
        }

        private void LayoutHideDock()
        {

            if (!hideDock.Checked)
            {
                this.panelOpenEditorsTray.Dock = DockStyle.Top;
                this.panelOpenEditorsTray.Dock = DockStyle.Fill;
            }
            else
            {
                this.panelOpenEditorsTray.Dock = DockStyle.Left;
                this.panelOpenEditorsTray.Dock = DockStyle.Left;
            }

            //   panel2.Enabled = hideDock.Checked;
            //   toolStrip3.Enabled = hideDock.Checked;
            DockingTray.Visible = hideDock.Checked;
            toolStrip3.Visible = hideDock.Checked;
            //    webBrowser1.Visible = hideDock.Checked;

            hideOpenEditorsPanel.Visible = hideDock.Checked;
            this.hideOpenEditors.Checked = true;
            hideOpenEditors_Click(null, null);
            this.hideOpenEditors.Height = 18;
            this.hideOpenEditorsPanel.Height = 18;
            //
            DockTrayHidden = hideDock.Checked;
            settings.Save();
        }
        private void hideDock_Click(object sender, EventArgs e)
        {
            LayoutHideDock();
            //
            SaveLayout();
            LoadLayout();
        }

        private void hideOpenEditors_Click(object sender, EventArgs e)
        {
            this.openEditorsTray.Visible = this.hideOpenEditors.Checked;
            this.panelOpenEditorsTray.Width = this.hideOpenEditors.Checked ? 99 : 22 ;

            this.hideOpenEditorsPanel.Height = this.hideOpenEditors.Checked ? 18 : this.panelOpenEditorsTray.Height - 4;
            this.hideOpenEditors.Height = this.hideOpenEditorsPanel.Height;
            this.hideOpenEditors.Width = this.hideOpenEditorsPanel.Width + 2;

            this.hideOpenEditors.RightToLeftAutoMirrorImage = this.hideOpenEditors.Checked;

            this.hideOpenEditors.Checked = false;

        }
        private void showROMInfo_Click(object sender, EventArgs e)
        {
            settings.EditorMain_showROMInfo++;
            switch (settings.EditorMain_showROMInfo)
            {
                case 1:
                    infoROMloaded.Visible = true;
                    infoROM.Visible = true;
                    break;
                case 2:
                    infoROMloaded.Visible = true;
                    infoROM.Visible = false;
                    break;
                case 3:
                    infoROMloaded.Visible = false;
                    infoROM.Visible = false;
                    settings.EditorMain_showROMInfo = 0;
                    break;
            };
            settings.Save();
        }

        private void panelOpenEditorsTray_SizeChanged(object sender, EventArgs e)
        {
            if (!this.openEditorsTray.Visible && !hideDock.Checked)
            {
                this.hideOpenEditorsPanel.Height = this.panelOpenEditorsTray.Height - 8;
                this.hideOpenEditors.Height = this.hideOpenEditorsPanel.Height;
            }
        }
        #endregion
    }
}