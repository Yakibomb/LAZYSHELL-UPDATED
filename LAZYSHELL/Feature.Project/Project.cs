using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Project : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private List<EIndex> currentIndexes;
        private EIndex currentIndex;
        private EList elist
        {
            get
            {
                EList elist = (EList)listBoxLists.SelectedItem;
                elist = project.ELists.Find(delegate(EList ELIST)
                {
                    return ELIST.Name == elist.Name;
                });
                return elist;
            }
            set
            {
                EList elist = (EList)listBoxLists.SelectedItem;
                elist = project.ELists.Find(delegate(EList ELIST)
                {
                    return ELIST.Name == elist.Name;
                });
                elist = value;
            }
        }
        private ProjectDB project { get { return Model.Project; } set { Model.Project = value; } }
        public NewListView ElementIndexes { get { return elementIndexes; } set { elementIndexes = value; } }
        public NumericUpDown IndexNumber { get { return indexNumber; } set { indexNumber = value; } }
        public TextBox IndexLabel { get { return indexLabel; } set { indexLabel = value; } }
        public RichTextBox IndexDescription { get { return indexDescription; } set { indexDescription = value; } }
        private ListViewColumnSorter elementsColumnSorter = new ListViewColumnSorter();
        private ListViewColumnSorter listsColumnSorter = new ListViewColumnSorter();
        private int listIndex
        {
            get
            {
                if (listViewList.SelectedItems.Count == 0)
                    return -1;
                return Bits.GetInt32(listViewList.SelectedItems[0].SubItems[0].Text);
            }
        }
                private Overlay overlay = new Overlay();
        //
        private FontCharacter[] font
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Menu: return fontMenu;
                    case FontType.Dialogue: return fontDialogue;
                    case FontType.Description: return fontDescription;
                    case FontType.BattleMenu: return fontBattleMenu;
                    default: return fontDialogue;
                }
            }
            set
            {
                switch (FontType)
                {
                    case FontType.Menu: fontMenu = value; break;
                    case FontType.Dialogue: fontDialogue = value; break;
                    case FontType.Description: fontDescription = value; break;
                    case FontType.BattleMenu: fontBattleMenu = value; break;
                }
            }
        }
        private FontCharacter[] fontMenu { get { return Model.FontMenu; } set { Model.FontMenu = value; } }
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } set { Model.FontDialogue = value; } }
        private FontCharacter[] fontDescription { get { return Model.FontDescription; } set { Model.FontDescription = value; } }
        private FontCharacter[] fontBattleMenu { get { return Model.FontBattleMenu; } set { Model.FontBattleMenu = value; } }
        private Bitmap fontTableImage;
        public FontType FontType
        {
            get
            {
                return (FontType)fontType.SelectedIndex;
            }
            set
            {
                fontType.SelectedIndex = (int)value;
            }
        }
        private PaletteSet fontPalettesDialogue { get { return Model.FontPaletteDialogue; } set { Model.FontPaletteDialogue = value; } }
        private PaletteSet fontPalettesMenu { get { return Model.FontPaletteMenu; } set { Model.FontPaletteMenu = value; } }
        private PaletteSet fontPalettesBattle { get { return Model.BattleMenuPalette; } set { Model.BattleMenuPalette = value; } }
        private int[] palette
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Menu:
                    case FontType.Description: return fontPalettesMenu.Palettes[0];
                    case FontType.BattleMenu: return fontPalettesBattle.Palettes[0];
                    default: return fontPalettesDialogue.Palettes[1];
                }
            }
        }
        private string[] keystrokes
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Menu: return project.KeystrokesMenu;
                    case FontType.Dialogue: return project.Keystrokes;
                    case FontType.Description: return project.KeystrokesDesc;
                    default: return null;
                }
            }
        }
        #endregion
        // constructor
        public Project()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip1, Keys.Control | Keys.S, new EventHandler(save_Click));
            this.elementIndexes.ListViewItemSorter = elementsColumnSorter;
            this.listViewList.ListViewItemSorter = listsColumnSorter;

            if (project == null) project = new ProjectDB();

            if (settings.NotePathCustom == "")
            {
                project = null;
                return;
            }
            // load the notes
            if (CompareNoteFileWithROMFile())
            {
                try
                {
                    Stream s = File.OpenRead(settings.NotePathCustom);
                    BinaryFormatter b = new BinaryFormatter();
                    project = (ProjectDB)b.Deserialize(s);
                    Model.RefreshListCollections();
                    projectFile.Text = Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj";
                    s.Close();
                    InitializeFields();
                }
                catch
                {
                    MessageBox.Show("Could not load last used database. The file has been moved, renamed, or no longer exists.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    project = null;
                    settings.NotePathCustom = null;
                    settings.Save();
                }
            }
            //
            //
        }
        #region Functions
        private void InitializeFields()
        {
            projectTitle.Text = project.Title;
            projectAuthor.Text = project.Author;
            projectDate.Text = project.Date;
            projectWebpage.Text = project.Webpage;
            projectDescription.Text = project.Description;
            projectOtherInfo.Text = project.OtherInfo;
            //
            if (elementType.SelectedIndex == 0)
                RefreshElementIndexes();
            else
                elementType.SelectedIndex = 0;
            listBoxLists.BeginUpdate();
            listBoxLists.Items.Clear();
            foreach (EList elist in Model.ELists)
                listBoxLists.Items.Add(elist);
            listBoxLists.EndUpdate();
            listBoxLists.SelectedIndex = 0;
            projectOtherInfo.Text = project.OtherInfo;
            //
            fontType.SelectedIndex = 0;
            InitializeKeystrokes();
            if (project != null)
            { 
                tabControl1.Enabled = true;
                refreshButton.Enabled = true;
                closeButton.Enabled = true;
                save.Enabled = true;
                saveAs.Enabled = true;
            }
            else
            {
                tabControl1.Enabled = false;
                refreshButton.Enabled = false;
                closeButton.Enabled = false;
                save.Enabled = false;
                saveAs.Enabled = false;
            }
        }
        private void RefreshElementIndexes()
        {
            this.Updating = true;
            panelAddressBit.Visible = false;
            panelIndexNumber.Visible = true;
            panelIndexNumber.BringToFront();
            elementIndexes.BeginUpdate();
            elementIndexes.Items.Clear();
            switch ((string)elementType.SelectedItem)
            {
                case "Allies":
                    currentIndexes = project.Allies;
                    indexNumber.Maximum = 4;
                    break;
                case "Levels":
                    currentIndexes = project.Levels;
                    indexNumber.Maximum = 509;
                    break;
                case "Dialogues":
                    currentIndexes = project.Dialogues;
                    indexNumber.Maximum = 4095;
                    break;
                case "Memory Bits":
                    currentIndexes = project.MemoryBits;
                    panelIndexNumber.Visible = false;
                    panelAddressBit.Visible = true;
                    panelAddressBit.BringToFront();
                    break;
                case "Event Scripts":
                    currentIndexes = project.EventScripts;
                    indexNumber.Maximum = 4095;
                    break;
                case "Action Scripts":
                    currentIndexes = project.ActionScripts;
                    indexNumber.Maximum = 1023;
                    break;
                case "Battle Events":
                    currentIndexes = project.BattleEvents;
                    indexNumber.Maximum = 101;
                    break;
                case "Monster Behavior Animations":
                    currentIndexes = project.MonsterBehaviorAnims;
                    indexNumber.Maximum = 54;
                    break;
                case "Monsters":
                    currentIndexes = project.Monsters;
                    indexNumber.Maximum = 255;
                    break;
                case "Formations":
                    currentIndexes = project.Formations;
                    indexNumber.Maximum = 511;
                    break;
                case "Packs":
                    currentIndexes = project.Packs;
                    indexNumber.Maximum = 255;
                    break;
                case "Attacks":
                    currentIndexes = project.Attacks;
                    indexNumber.Maximum = 128;
                    break;
                case "Spells":
                    currentIndexes = project.Spells;
                    indexNumber.Maximum = 127;
                    break;
                case "Items":
                    currentIndexes = project.Items;
                    indexNumber.Maximum = 255;
                    break;
                case "Battlefields":
                    currentIndexes = project.Battlefields;
                    indexNumber.Maximum = 63;
                    break;
                case "Effects":
                    currentIndexes = project.Effects;
                    indexNumber.Maximum = 127;
                    break;
                case "Sprites":
                    currentIndexes = project.Sprites;
                    indexNumber.Maximum = 1023;
                    break;
                case "Shops":
                    currentIndexes = project.Shops;
                    indexNumber.Maximum = 32;
                    break;
                default:
                    panel1.Enabled = false;
                    groupBox1.Enabled = false;
                    indexNumber.Value = 0;
                    indexLabel.Text = "";
                    indexDescription.Text = "";
                    elementIndexes.EndUpdate();
                    elementIndexes.EndUpdate();
                    this.Updating = false;
                    return;
            }
            panel1.Enabled = true;
            if (currentIndexes.Count == 0)
            {
                buttonDelete.Enabled = false;
                buttonMoveDown.Enabled = false;
                buttonMoveUp.Enabled = false;
                buttonLoad.Enabled = false;
                groupBox1.Enabled = false;
                indexNumber.Value = 0;
                address.Value = 0x7000;
                indexLabel.Text = "";
                indexDescription.Text = "";
            }
            int counter = 0;
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (EIndex index in currentIndexes)
            {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                ListViewItem lvitem = new ListViewItem(new string[]
                {
                    (elementType.SelectedItem == "Memory Bits" ? 
                    index.Address.ToString("X4") : index.Index.ToString()) + 
                    (elementType.SelectedItem == "Memory Bits" ? 
                    ":" + index.AddressBit.ToString() : ""),
                    index.Label
                });
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
                lvitem.Tag = counter++;
                listViewItems.Add(lvitem);
            }
            elementIndexes.Items.AddRange(listViewItems.ToArray());
            elementIndexes.EndUpdate();
            this.Updating = false;
        }
        private void RefreshIndex()
        {
            this.Updating = true;
            buttonDelete.Enabled = true;
            buttonMoveDown.Enabled = true;
            buttonMoveUp.Enabled = true;
            buttonLoad.Enabled = true;
            groupBox1.Enabled = true;
            currentIndex = (EIndex)currentIndexes[Do.GetSelectedIndex(elementIndexes)];
            indexNumber.Value = currentIndex.Index;
            indexLabel.Text = currentIndex.Label;
            indexDescription.Text = currentIndex.Description;
            address.Value = currentIndex.Address;
            addressBit.Value = currentIndex.AddressBit;
            this.Updating = false;
        }

        private bool CompareNoteFileWithROMFile()
        {
            if (Model.GetNotePathCustomWithoutPathOrExtension() != Model.GetFileNameWithoutPath())
            {
                DialogResult result = MessageBox.Show("The note's file name does not match this ROM's file name.\n\nLoad anyway?", "LAZYSHELL++",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    project = null;
                    settings.NotePathCustom = "";
                    return false;
                }
            }
            return true;
        }
        public bool LoadProject()
        {
            if (project == null) project = new ProjectDB();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = settings.NotePathCustom;
            openFileDialog.Title = "Open existing project...";
            openFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            openFileDialog.Filter = "Lazy Shell Project/Notes (*.lsproj; *.lsnotes)|*.lsproj;*.lsnotes";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            //
            if (!CompareNoteFileWithROMFile())
                return false;
            //
            Stream s = File.OpenRead(openFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            try
            {
                string extension = Path.GetExtension(openFileDialog.FileName);
                if (extension == ".lsproj")
                {
                    project = (ProjectDB)b.Deserialize(s);
                }
                else if (extension == ".lsnotes")
                {
                    if (MessageBox.Show("This is a notes file -- in order to load this file it must be converted into a project.\n\n" +
                        "Continue loading file?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        s.Close();
                        return false;
                    }
                    NotesDB notes = (NotesDB)b.Deserialize(s);
                    project = CreateProject(notes);
                    openFileDialog.FileName = Path.ChangeExtension(openFileDialog.FileName, "lsproj");
                }
                if (project == null)
                {
                    MessageBox.Show("This is not a valid project file.", "LAZYSHELL++", MessageBoxButtons.OK);
                    s.Close();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("This is not a valid project file.", "LAZYSHELL++", MessageBoxButtons.OK);
                s.Close();
                return false;
            }
            //
            s.Close();
            Model.RefreshListCollections();

            settings.NotePathCustom = openFileDialog.FileName;
        //  projectFile.Text = openFileDialog.FileName;
            projectFile.Text = Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj";
            InitializeFields();
            return true;
        }
        private bool CreateNewProject()
        {
            if (project != null && settings.NotePathCustom != "")
            {
                DialogResult result = MessageBox.Show("Save changes to project database?",
                    "LAZYSHELL++", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    SaveLoadedProject();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = settings.NotePathCustom;
            saveFileDialog.Title = "Create new project...";
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            saveFileDialog.Filter = "Lazy Shell Project (*.lsproj)|*.lsproj";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return false;
            //
            settings.NotePathCustom = saveFileDialog.FileName;
            Stream s = File.Create(saveFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, new ProjectDB());
            s.Close();
            // now load the notes
            s = File.OpenRead(saveFileDialog.FileName);
            b = new BinaryFormatter();
            project = (ProjectDB)b.Deserialize(s);
            s.Close();
            projectFile.Text = Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj";
            InitializeFields();
            return true;
        }
        private void SaveNewProject(string path)
        {
            Model.ResetListCollections();
            Stream s = File.Create(path);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, new ProjectDB());
            s.Close();
        }
        private void SaveLoadedProject()
        {
            if (project == null || settings.NotePathCustom == "")
            {
                SaveAsNewProject();
                return;
            }
            Model.RefreshListCollections();
            Stream s = File.Create(settings.NotePathCustom);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, project);
            s.Close();
        }

        private void AutoUpdate()
        {
            if (autoUpdate.Checked)
                Model.RefreshListCollections();
        }

        private void SaveAsNewProject()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = settings.NotePathCustom;
            saveFileDialog.Title = "Save as new project...";
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + ".lsproj";
            saveFileDialog.Filter = "Project DB (*.lsproj)|*.lsproj";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            settings.NotePathCustom = saveFileDialog.FileName;
            projectFile.Text = Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj";
            //
            Model.RefreshListCollections();
            Stream s = File.Create(saveFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, project);
            s.Close();
        }
        private void AddNewIndex()
        {
            if (Do.GetSelectedIndex(elementIndexes) != -1)
                project.AddIndex(Do.GetSelectedIndex(elementIndexes) + 1, currentIndexes);
            else
                project.AddIndex(0, currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex + 1].Selected = true;
        }
        public void AddingFromEditor(string type, int number, string label, string description)
        {
            string selectedItem = null;
            foreach (string item in elementType.Items)
                if (item.Trim() == type)
                    selectedItem = item;
            if (selectedItem == null)
                return;
            else
            {
                tabControl1.SelectedIndex = 2;
                elementType.SelectedItem = selectedItem;
            }
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[elementIndexes.Items.Count - 1].Selected = true;
            AddNewIndex();
            indexNumber.Value = number;
            indexLabel.Text = label;
            indexDescription.Text = "";
        }
        public void AddingFromEditor(string type, int address, int addressBit, string label, string description)
        {
            string selectedItem = null;
            foreach (string item in elementType.Items)
                if (item.Trim() == type)
                    selectedItem = item;
            if (selectedItem == null)
                return;
            else
            {
                tabControl1.SelectedIndex = 2;
                elementType.SelectedItem = selectedItem;
            }
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[elementIndexes.Items.Count - 1].Selected = true;
            AddNewIndex();
            this.address.Value = address;
            this.addressBit.Value = addressBit;
            indexLabel.Text = label;
            indexDescription.Text = "";
        }
        private void SortIndexes(int column)
        {
            int count = currentIndexes.Count;
            for (int y = 0; y < count - 1; y++)
            {
                for (int x = 0; x < count - 1 - y; x++)
                {
                    EIndex indexA = (EIndex)currentIndexes[x];
                    EIndex indexB = (EIndex)currentIndexes[x + 1];
                    if (column == 0)
                    {
                        if (elementType.SelectedItem.ToString() == "Memory Bits")
                        {
                            if ((indexB.Address.ToString() + indexB.AddressBit.ToString()).CompareTo(
                                (indexA.Address.ToString() + indexA.AddressBit.ToString())) < 0)
                                currentIndexes.Reverse(x, 2);
                        }
                        else
                        {
                            if (indexB.Index.CompareTo(indexA.Index) < 0)
                                currentIndexes.Reverse(x, 2);
                        }
                    }
                    else if (column == 1)
                    {
                        if (indexB.Label.CompareTo(indexA.Label) < 0)
                            currentIndexes.Reverse(x, 2);
                    }
                }
            }
        }
        // element lists
        private void RefreshElementList()
        {
            int index = listBoxLists.SelectedIndex;
            string[] list = elist.Labels;
            listViewList.BeginUpdate();
            listViewList.Items.Clear();
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            int digits = list.Length.ToString().Length;
            for (int i = 0; i < list.Length; i++)
            {
                ListViewItem lvitem = new ListViewItem(new string[]
                {
                    i.ToString(), list[i]
                });
                
                listViewItems.Add(lvitem);
            }
            listViewList.Items.AddRange(listViewItems.ToArray());
            listViewList.EndUpdate();
            //
            listLabel.Text = "";
            listDescription.Text = "";
        }
        private void ExportList(bool exportAll)
        {
            if (listBoxLists.SelectedItem == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            if (exportAll)
                saveFileDialog.FileName = "listCollections";
            else
                saveFileDialog.FileName = "list" + (listBoxLists.SelectedItem.ToString()).Replace(" ", "");
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            List<EList> listsToSave = new List<EList>();
            if (exportAll)
                foreach (EList item in listBoxLists.Items)
                    listsToSave.Add(item);
            else
                listsToSave.Add((EList)listBoxLists.SelectedItem);
            if (listsToSave.Count == 0)
                return;
            StreamWriter writer = File.CreateText(saveFileDialog.FileName);
            for (int a = 0; a < listsToSave.Count; a++)
            {
                string[] list = listsToSave[a].Labels;
                writer.WriteLine("[" + listsToSave[a].Name + "]");
                for (int i = 0; i < list.Length; i++)
                    writer.WriteLine("{" + i.ToString("d" + list.Length.ToString().Length) + "}  " + list[i]);
                writer.WriteLine();
            }
            writer.Close();
        }
        private void ImportList(bool importAll)
        {
            if (listBoxLists.SelectedItem == null)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            if (importAll)
                openFileDialog.FileName = "listCollections";
            else
                openFileDialog.FileName = listBoxLists.SelectedItem.ToString();
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            TextReader reader = new StreamReader(openFileDialog.FileName);
            string[] listToRead = null;
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                // skip line if empty
                if (line == "")
                    continue;
                // if beginning of another list, set current list
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    line = line.Substring(1, line.Length - 2);
                    EList elist = project.ELists.Find(delegate(EList list)
                    {
                        return list.Name == line;
                    });
                    if (elist != null)
                        listToRead = elist.Labels;
                    line = reader.ReadLine();
                }
                // if current list not set, continue
                if (listToRead == null)
                    continue;
                // get tagged index of line
                string tag = Regex.Match(line, "^[^ ]+").Value;
                // skip line completely if not tagged with index
                if (!tag.StartsWith("{") || !tag.EndsWith("}"))
                {
                    line = reader.ReadLine();
                    continue;
                }
                // remove tag from line
                line = line.Substring(tag.Length).Trim();
                int indexNumber = Bits.GetInt32(ref tag);
                string indexLabel = line.Trim();
                // skip line if index is out of bounds of list
                if (indexNumber >= listToRead.Length)
                    continue;
                listToRead[indexNumber] = indexLabel;
            }
            reader.Close();
        }
        private ProjectDB CreateProject(NotesDB notes)
        {
            ProjectDB project = new ProjectDB();
            project.OtherInfo = notes.GeneralNotes;
            foreach (NotesDB.Index index in notes.Allies) project.Allies.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Levels) project.Levels.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Dialogues) project.Dialogues.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.EventScripts) project.EventScripts.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.ActionScripts) project.ActionScripts.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Items) project.Items.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.MemoryBits) project.MemoryBits.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Monsters) project.Monsters.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Packs) project.Packs.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Formations) project.Formations.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.BattleEvents) project.EventScripts.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.MonsterBehaviorAnims) project.EventScripts.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Attacks) project.Attacks.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Spells) project.Spells.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Sprites) project.Sprites.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Effects) project.Effects.Add(new EIndex(index));
            foreach (NotesDB.Index index in notes.Shops) project.Shops.Add(new EIndex(index));
            return project;
        }
        //
        private void SetFontTableImage()
        {
            int[] palette = this.palette;
            int[] pixels = Do.DrawFontTable(font, palette, 8, 256, 384, 32, 24, 8, 16);
            fontTableImage = Do.PixelsToImage(pixels, 256, 384);
            pictureBox1.BackColor = Color.FromArgb(palette[3]);
            pictureBox1.Invalidate();
        }
        private void InitializeKeystrokes()
        {
            this.Updating = true;
            panelFontTable.SuspendDrawing();
            panelFontTable.Controls.Clear();
            TextBox keyBox;
            for (int y = 16; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    int index = y * 8 + x;
                    keyBox = new TextBox();
                    keyBox.BackColor = Color.FromArgb(palette[3]);
                    keyBox.BorderStyle = BorderStyle.None;
                    keyBox.Font = new Font("Arial", 12F);
                    keyBox.ForeColor = Color.FromArgb(palette[1]);
                    keyBox.Left = x * 32;
                    keyBox.MaxLength = 1;
                    keyBox.Multiline = true;
                    if (index == 0)
                        keyBox.ReadOnly = true;
                    keyBox.Size = new Size(31, 23);
                    keyBox.TabIndex = y * 8 + x;
                    keyBox.Tag = index;
                    keyBox.Text = keystrokes[index + 32];
                    keyBox.TextAlign = HorizontalAlignment.Center;
                    keyBox.Top = y * 24;
                    keyBox.Enter += new EventHandler(keyBox_Enter);
                    keyBox.MouseUp += new MouseEventHandler(keyBox_MouseUp);
                    keyBox.TextChanged += new EventHandler(keyBox_TextChanged);
                    //
                    panelFontTable.Controls.Add(keyBox);
                    keyBox.BringToFront();
                }
            }
            panelFontTable.ResumeDrawing();
            this.Updating = false;
        }
        private void RefreshKeystrokes()
        {
            this.Updating = true;
            panelFontTable.SuspendDrawing();
            foreach (TextBox keyBox in panelFontTable.Controls)
            {
                int index = (int)keyBox.Tag;
                keyBox.BackColor = Color.FromArgb(palette[3]);
                keyBox.ForeColor = Color.FromArgb(palette[1]);
                keyBox.Text = keystrokes[index + 32];
            }
            panelFontTable.ResumeDrawing();
            this.Updating = false;
        }
        #endregion
        #region Event Handlers
        private void Project_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (project == null || settings.NotePathCustom == "")
                return;

            DialogResult result = MessageBox.Show("Save changes to project database?", "LAZYSHELL++",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                save.PerformClick();
            else if (result == DialogResult.Cancel)
                e.Cancel = true;
            else if (result == DialogResult.No && project != null)
            {
                // reload notes file
                try
                {
                    Stream s = File.OpenRead(settings.NotePathCustom);
                    BinaryFormatter b = new BinaryFormatter();
                    project = (ProjectDB)b.Deserialize(s);
                    s.Close();
                }
                catch
                {
                    return;
                }
            }
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                DialogResult result = MessageBox.Show("Save changes to project database?", "LAZYSHELL++",
                    MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveLoadedProject();
                if (result == DialogResult.Cancel)
                    return;
            }
            LoadProject();
        }
        // toolstrip
        private void new_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }
        private void load_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        private void save_Click(object sender, EventArgs e)
        {
            // Check if read only, if it is do a "Save As" routine
            FileInfo file = new FileInfo(settings.NotePathCustom);
            if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                saveAs.PerformClick();
                return;
            }
            // Check if currently in use by another application
            FileStream fs = null;
            try
            {
                fs = File.Open(settings.NotePathCustom, FileMode.Open);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Lazy Shell could not save the Project Database.\n\nThe file is currently in use by another application.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveLoadedProject();
        }
        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveAsNewProject();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (project == null || settings.NotePathCustom == "")
                return;
            DialogResult result = MessageBox.Show("Reload project database?", "LAZYSHELL++",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
            else if (settings.NotePathCustom != "")
            {
                // reload notes file
                try
                {
                    // now load the notes
                    Stream s = File.OpenRead(settings.NotePathCustom);
                    BinaryFormatter b = new BinaryFormatter();
                    project = (ProjectDB)b.Deserialize(s);
                    s.Close();
                    InitializeFields();
                }
                catch
                {
                    return;
                }
            }

        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close the project database?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            project = null;
            settings.NotePathCustom = "";
            Model.ResetListCollections();
            Model.Program.Project.Close();
            if (Model.Program.Project == null || !Model.Program.Project.Visible)
                Model.Program.CreateProjectWindow();
        }

        private void alwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTop.Checked;
        }
        private void tagIndexesWithNumbers_Click(object sender, EventArgs e)
        {
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            if (selectedIndex > 0 )
                elementIndexes.Items[selectedIndex].Selected = true;
        }
        // project information
        private void projectTitle_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Title = projectTitle.Text;
        }
        private void projectAuthor_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Author = projectAuthor.Text;
        }
        private void projectDate_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Date = projectDate.Text;
        }
        private void projectWebpage_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Webpage = projectWebpage.Text;
        }
        private void projectDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            project.Description = projectDescription.Text;
        }
        // element lists
        private void listBoxLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshElementList();
        }
        private void listViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            listLabel.Text = elist.Indexes[listIndex].Label;
            listDescription.Text = elist.Indexes[listIndex].Description;
        }
        private void listViewList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Do.SortListView(listViewList, listsColumnSorter, e.Column);
        }
        private void addToElements_Click(object sender, EventArgs e)
        {
            if (listIndex < 0)
            {
                MessageBox.Show("Must select an item in the list before adding it to the notes.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int number = elist.Indexes[listIndex].Index;
            string label = elist.Indexes[listIndex].Label;
            string description = elist.Indexes[listIndex].Description;
            tabControl1.SelectedIndex = 2;
            AddingFromEditor(elist.Name, number, label, description);
        }
        private void listLabel_TextChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            if (elist == null)
                return;
            elist.Indexes[listIndex].Label = listLabel.Text;
            listViewList.SelectedItems[0].SubItems[1].Text = listLabel.Text;
            
            AutoUpdate();
        }
        private void listDescription_TextChanged(object sender, EventArgs e)
        {
            if (listViewList.SelectedItems.Count == 0)
                return;
            if (elist == null)
                return;
            elist.Indexes[listIndex].Description = listDescription.Text;
        }
        private void importCollection_Click(object sender, EventArgs e)
        {
            ImportList(true);
        }
        private void importList_Click(object sender, EventArgs e)
        {
            ImportList(false);
        }
        private void exportCollection_Click(object sender, EventArgs e)
        {
            ExportList(true);
        }
        private void exportList_Click(object sender, EventArgs e)
        {
            ExportList(false);
        }
        private void resetAllLists_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reset all lists in the current project to their default labels.\n\n" +
                "Continue with process?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            foreach (EList elist in project.ELists)
                elist.Reset();
            RefreshElementList();
        }
        private void resetCurrentList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to reset the current list to it's default labels.\n\n" +
                "Continue with process?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            elist.Reset();
            RefreshElementList();
        }
        // element notes
        private void transferToLists_Click(object sender, EventArgs e)
        {
            string item = (string)elementType.SelectedItem;
            EList elist = project.ELists.Find(delegate(EList list)
            {
                return list.Name == item;
            });
            if (elist == null)
                return;
            List<EIndex> eindexes = null;
            switch (item)
            {
                case "Allies": eindexes = project.Allies; break;
                case "Levels": eindexes = project.Levels; break;
                case "Dialogues": eindexes = project.Dialogues; break;
                case "Event Scripts": eindexes = project.EventScripts; break;
                case "Action Scripts": eindexes = project.ActionScripts; break;
                case "Items": eindexes = project.Items; break;
                case "Monsters": eindexes = project.Monsters; break;
                case "Packs": eindexes = project.Packs; break;
                case "Formations": eindexes = project.Formations; break;
                case "Battlefields": eindexes = project.Battlefields; break;
                case "Shops": eindexes = project.Shops; break;
                case "Attacks": eindexes = project.Attacks; break;
                case "Spells": eindexes = project.Spells; break;
                case "Effects": eindexes = project.Effects; break;
                case "Sprites": eindexes = project.Sprites; break;
                case "Battle Events": eindexes = project.BattleEvents; break;
                case "Monster Behavior Animations": eindexes = project.MonsterBehaviorAnims; break;
                default: break;
            }
            if (eindexes == null)
                return;
            foreach (EIndex eindex in eindexes)
            {
                elist.Indexes[eindex.Index].Label = eindex.Label;
            }
            RefreshElementList();
        }
        private void elementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshElementIndexes();
            if (elementIndexes.Items.Count > 0)
                elementIndexes.Items[0].Selected = true;
        }
        private void elementIndexes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) == -1) 
                return;
            SortIndexes(e.Column);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void elementIndexes_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;
            if (this.Updating)
                return;
            RefreshIndex();
            elementIndexes.BeginUpdate();
            elementIndexes.Items[Do.GetSelectedIndex(elementIndexes)].EnsureVisible();
            elementIndexes.EndUpdate();
        }
        private void indexNumber_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Index = (int)indexNumber.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void indexLabel_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Label = indexLabel.Text;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
            
            AutoUpdate();
        }
        private void indexDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Description = indexDescription.Text;

            AutoUpdate();
        }
        private void address_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.Address = (int)address.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void addressBit_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentIndex.AddressBit = (int)addressBit.Value;
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex].Selected = true;
        }
        private void generalNotes_TextChanged(object sender, EventArgs e)
        {
            project.OtherInfo = projectOtherInfo.Text;
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            switch ((string)elementType.SelectedItem)
            {
                case "Levels":
                    if (Model.Program.Levels == null || !Model.Program.Levels.Visible)
                        Model.Program.CreateLevelsWindow();
                    Model.Program.Levels.LevelNum.Value = indexNumber.Value;
                    Model.Program.Levels.BringToFront();
                    break;
                case "Dialogues":
                    if (Model.Program.Dialogues == null || !Model.Program.Dialogues.Visible)
                        Model.Program.CreateDialoguesWindow();
                    Model.Program.Dialogues.index = (int)indexNumber.Value;
                    Model.Program.Dialogues.BringToFront();
                    break;
                case "Memory Bits":
                    break;
                case "Event Scripts":
                    if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                        Model.Program.CreateEventScriptsWindow();
                    Model.Program.EventScripts.EventName.SelectedIndex = 0;
                    Model.Program.EventScripts.EventNum.Value = indexNumber.Value;
                    Model.Program.EventScripts.BringToFront();
                    break;
                case "Action Scripts":
                    if (Model.Program.EventScripts == null || !Model.Program.EventScripts.Visible)
                        Model.Program.CreateEventScriptsWindow();
                    Model.Program.EventScripts.EventName.SelectedIndex = 1;
                    Model.Program.EventScripts.EventNum.Value = indexNumber.Value;
                    Model.Program.EventScripts.BringToFront();
                    break;
                case "Battlefields":
                    if (Model.Program.Battlefields == null || !Model.Program.Battlefields.Visible)
                        Model.Program.CreateBattlefieldsWindow();
                    Model.Program.Battlefields.Index = (int)indexNumber.Value;
                    Model.Program.Battlefields.BringToFront();
                    break;
                case "Monsters":
                    if (Model.Program.Monsters == null || !Model.Program.Monsters.Visible)
                        Model.Program.CreateMonstersWindow();
                    Model.Program.Monsters.Index = (int)indexNumber.Value;
                    Model.Program.Monsters.BringToFront();
                    break;
                case "Formations":
                    if (Model.Program.Formations == null || !Model.Program.Formations.Visible)
                        Model.Program.CreateFormationsWindow();
                    Model.Program.Formations.FormationIndex = (int)indexNumber.Value;
                    Model.Program.Formations.BringToFront();
                    break;
                case "Packs":
                    if (Model.Program.Formations == null || !Model.Program.Formations.Visible)
                        Model.Program.CreateFormationsWindow();
                    Model.Program.Formations.PackIndex = (int)indexNumber.Value;
                    Model.Program.Formations.BringToFront();
                    break;
                case "Attacks":
                    if (Model.Program.Attacks == null || !Model.Program.Attacks.Visible)
                        Model.Program.CreateAttacksWindow();
                    Model.Program.Attacks.attacksEditor.Index = (int)indexNumber.Value;
                    Model.Program.Attacks.BringToFront();
                    break;
                case "Spells":
                    if (Model.Program.Attacks == null || !Model.Program.Attacks.Visible)
                        Model.Program.CreateAttacksWindow();
                    Model.Program.Attacks.spellsEditor.Index = (int)indexNumber.Value;
                    Model.Program.Attacks.BringToFront();
                    break;
                case "Items":
                    if (Model.Program.Items == null || !Model.Program.Items.Visible)
                        Model.Program.CreateItemsWindow();
                    Model.Program.Items.itemsEditor.Index = (int)indexNumber.Value;
                    Model.Program.Items.BringToFront();
                    break;
                case "Allies":
                    if (Model.Program.Allies == null || !Model.Program.Allies.Visible)
                        Model.Program.CreateAlliesWindow();
                    Model.Program.Allies.RefreshCharacter((int)indexNumber.Value);
                    Model.Program.Allies.BringToFront();
                    break;
                case "Effects":
                    if (Model.Program.Effects == null || !Model.Program.Effects.Visible)
                        Model.Program.CreateEffectsWindow();
                    Model.Program.Effects.index = (int)indexNumber.Value;
                    Model.Program.Effects.BringToFront();
                    break;
                case "Sprites":
                    if (Model.Program.Sprites == null || !Model.Program.Sprites.Visible)
                        Model.Program.CreateSpritesWindow();
                    Model.Program.Sprites.Index = (int)indexNumber.Value;
                    Model.Program.Sprites.BringToFront();
                    break;
                case "Shops":
                    if (Model.Program.Items == null || !Model.Program.Items.Visible)
                        Model.Program.CreateItemsWindow();
                    Model.Program.Items.shopsEditor.Index = (int)indexNumber.Value;
                    Model.Program.Items.BringToFront();
                    break;
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) == -1)
                return;
            project.DeleteIndex(Do.GetSelectedIndex(elementIndexes), currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            if (currentIndexes.Count > 0)
                elementIndexes.Items[Math.Min(selectedIndex, currentIndexes.Count - 1)].Selected = true;
        }
        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) == 0)
                return;
            project.SwitchIndex(Do.GetSelectedIndex(elementIndexes) - 1, currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex - 1].Selected = true;
        }
        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (Do.GetSelectedIndex(elementIndexes) >= elementIndexes.Items.Count - 1)
                return;
            project.SwitchIndex(Do.GetSelectedIndex(elementIndexes), currentIndexes);
            int selectedIndex = Do.GetSelectedIndex(elementIndexes);
            RefreshElementIndexes();
            elementIndexes.Items[selectedIndex + 1].Selected = true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddNewIndex();
        }
        // keystrokes
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (fontTableImage == null)
                SetFontTableImage();
            e.Graphics.DrawImage(fontTableImage, 0, 0, 256, 384);
            overlay.DrawTileGrid(e.Graphics, Color.Gray, fontTableImage.Size, new Size(32, 24), false, -1);
        }
        private void keyBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void keyBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            TextBox rtb = (TextBox)sender;
            keystrokes[(int)rtb.Tag + 32] = rtb.Text;
        }
        private void fontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshKeystrokes();
            SetFontTableImage();
        }
        private void openKeystrokes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Load keystroke table";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog1.FileName;
            StreamReader sr = new StreamReader(path);
            string line;
            for (int i = 32; i < keystrokes.Length && (line = sr.ReadLine()) != null; i++)
            {
                if (line.Length > 1)
                {
                    MessageBox.Show("There was a problem opening the keystroke table.\n" +
                        "One or more of the assigned keystrokes has an invalid length.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                keystrokes[i] = line;
            }
            keystrokes[32] = " ";
            RefreshKeystrokes();
        }
        private void saveKeystrokes_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            switch (FontType)
            {
                case FontType.Menu: saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - keystrokesMenu.txt"; break;
                case FontType.Dialogue: saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - keystrokesDialogue.txt"; break;
                case FontType.Description: saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - keystrokesDescriptions.txt"; break;
            }
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save keystroke table";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            StreamWriter sr = new StreamWriter(saveFileDialog.FileName);
            for (int i = 32; i < keystrokes.Length; i++)
            {
                string s = keystrokes[i];
                sr.WriteLine(s);
            }
            sr.Close();
        }
        private void resetKeystrokes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset all keystroke tables to their default values?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            switch (FontType)
            {
                case FontType.Menu: Model.KeystrokesMenu.CopyTo(project.KeystrokesMenu, 0); break;
                case FontType.Dialogue: Model.Keystrokes.CopyTo(project.Keystrokes, 0); break;
                case FontType.Description: Model.KeystrokesDesc.CopyTo(project.KeystrokesDesc, 0); break;
            }
            RefreshKeystrokes();
        }
        private void projectFile_Click(object sender, EventArgs e)
        {
            if (project == null || settings.NotePathCustom == "")
                return;

            if (projectFile.Text == Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj")
                projectFile.Text = settings.NotePathCustom;
            else
                projectFile.Text = Model.GetNotePathCustomWithoutPathOrExtension() + ".lsproj";
        }

        #endregion
    }
}
