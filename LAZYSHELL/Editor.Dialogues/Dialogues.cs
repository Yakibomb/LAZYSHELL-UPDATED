using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Dialogues : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        // main
        private delegate void Function(RichTextBox richTextBox, StringComparison stringComparison, bool matchWholeWord, bool replaceAll, string replaceWith);
        // accessors
        private Dialogue[] dialogues { get { return Model.Dialogues; } set { Model.Dialogues = value; } }
        private Dialogue dialogue { get { return dialogues[index]; } set { dialogues[index] = value; } }
        private DTE[] dte { get { return Model.DTE; } set { Model.DTE = value; } }
        private string[] dteStrByte;
        private string[] dteStrText;
        private string[] dteStr
        {
            get
            {
                if (byteView)
                    return dteStrByte;
                else
                    return dteStrText;
            }
            set
            {
                if (byteView)
                    dteStrByte = value;
                else
                    dteStrText = value;
            }
        }
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } }
        private FontCharacter[] fontTriangle { get { return Model.FontTriangle; } }
        private PaletteSet fontPalette { get { return Model.FontPaletteDialogue; } set { Model.FontPaletteDialogue = value; } }
        // public accessors
        public ToolStripNumericUpDown DialogueNum { get { return dialogueNum; } set { dialogueNum = value; } }
        private DialoguePreview dialoguePreview;
        private TextHelper textHelper;
        private bool byteView { get { return !textView.Checked; } set { textView.Checked = !value; } }
        public int index { get { return (int)dialogueNum.Value; } set { dialogueNum.Value = value; } }
        private bool[] isDialogueChanged = new bool[4096];
        private bool warning;
        private bool modifyDTE;
        private Bitmap
            dialogueBGImage,
            dialogueTextImage;
        private DialogueTileset dialogueTileset;
        // editors
        private BattleDialogues battleDialogues;
        public BattleDialogues BattleDialogues { get { return battleDialogues; } set { battleDialogues = value; } }
        private Fonts fonts;
        public Fonts Fonts { get { return fonts; } set { fonts = value; } }
        private Search searchWindow;
        private EditLabel labelWindow;
        #endregion
        #region Functions
        public Dialogues()
        {
            // 
            dteStrByte = Model.DTEStr(true);
            dteStrText = Model.DTEStr(false);
            FindDuplicateDialoguePointers();
            FindWithinDialogues();
            InitializeComponent();
            Do.AddShortcut(toolStrip3, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip3, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip3, Keys.F2, baseConvertor);
            searchWindow = new Search(dialogueNum, searchBox, search, new Function(LoadSearch), "richTextBox");
            labelWindow = new EditLabel(null, dialogueNum, "Dialogues", false);
            // tileset
            textHelper = TextHelper.Instance;
            dialoguePreview = new DialoguePreview();
            dialogueTileset = new DialogueTileset(Model.FontPaletteDialogue);
            SetTilesetImage();
            // compression table
            this.Updating = true;
            this.dct0E.Text = dte[0].GetText(byteView); dct0E.Tag = 0;
            this.dct0F.Text = dte[1].GetText(byteView); dct0F.Tag = 1;
            this.dct10.Text = dte[2].GetText(byteView); dct10.Tag = 2;
            this.dct11.Text = dte[3].GetText(byteView); dct11.Tag = 3;
            this.dct12.Text = dte[4].GetText(byteView); dct12.Tag = 4;
            this.dct13.Text = dte[5].GetText(byteView); dct13.Tag = 5;
            this.dct14.Text = dte[6].GetText(byteView); dct14.Tag = 6;
            this.dct15.Text = dte[7].GetText(byteView); dct15.Tag = 7;
            this.dct16.Text = dte[8].GetText(byteView); dct16.Tag = 8;
            this.dct17.Text = dte[9].GetText(byteView); dct17.Tag = 9;
            this.dct18.Text = dte[10].GetText(byteView); dct18.Tag = 10;
            this.dct19.Text = dte[11].GetText(byteView); dct19.Tag = 11;
            this.Updating = false;
            // editors
            LoadFontEditor();
            option.Image = Do.PixelsToImage(fontTriangle[0].GetPixels(fontPalette.Palettes[1]), 8, 16);
            battleDialogues = new BattleDialogues(this);
            battleDialogues.TopLevel = false;
            battleDialogues.Dock = DockStyle.Bottom;
            panelDialogues.Controls.Add(battleDialogues);
            battleDialogues.BringToFront();
            battleDialogues.Show();
            fonts.BringToFront();
            // set controls
            this.Updating = true;
            if (settings.RememberLastIndex)
                index = settings.LastDialogue;
            variables.SelectedIndex = 0;
            RefreshDialogueEditor();
            CalculateFreeTableSpace();
            SetTextImage();
            this.Updating = false;
            new ToolTipLabel(this, baseConvertor, helpTips);
            new ToolTipLabel(fonts.NewFontTable, baseConvertor, helpTips);
            //
            this.History = new History(this, null, dialogueNum);
        }
        private void RefreshDialogueEditor()
        {
            this.Updating = true;
            if (dialogue.Reference != index)
                dialogueTextBox.BackColor = SystemColors.Info;
            else
                dialogueTextBox.BackColor = SystemColors.Window;
            this.dialogueTextBox.Text = dialogue.GetText(byteView, dteStr);
            this.dialogueTextBox.SelectionStart = dialogue.GetCaretPosition(byteView);
            int temp = CalculateFreeSpace();
            this.freeBytes.Text = temp.ToString() + " characters left";
            this.freeBytes.BackColor = temp >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void SetTilesetImage()
        {
            dialogueBGImage = Do.PixelsToImage(
                Do.TilesetToPixels(dialogueTileset.Tileset_tiles, 16, 4, 0, false), 256, 56);
            pictureBoxDialogue.Invalidate();
        }
        private void SetTextImage()
        {
            char[] text = dialogueTextBox.Text.ToCharArray();
            bool preview = true, valid = true, fail = false;
            char[] swap;
            int temp;
            for (int i = 0; i < text.Length; i++)
            {
                // Open bracket when we have already had an open bracket
                fail = text[i] == '[' && preview == false;
                if (text[i] == '[') // Open Bracket
                {
                    preview = false;
                    for (temp = ++i; temp < text.Length && text[temp] != ']'; temp++)
                        if (byteView && !(text[temp] >= '0' && text[temp] <= '9'))
                            fail = true;
                }
                else if (preview == false && text[i] == ']') // Close bracket after open bracket
                    preview = true;
                else if (preview == true && valid == true)
                    valid = textHelper.IsValidSymbol(text[i]);
                if (i < text.Length && text[i] == '\n')
                {
                    int tempSel = dialogueTextBox.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    dialogueTextBox.Text = new string(swap);
                    text = dialogueTextBox.Text.ToCharArray();
                    i += 2;
                    dialogueTextBox.SelectionStart = tempSel + 2;
                }
            }
            if (preview && valid && !fail)
            {
                dialogue.SetText(dialogueTextBox.Text, byteView, dteStr);
                int[] pixels = dialoguePreview.GetPreview(fontDialogue, fontTriangle, fontPalette.Palettes[1], fontPalette.Palettes[1], dialogue.Text, 16);
                dialogueTextImage = Do.PixelsToImage(pixels, 256, 56);
            }
            pictureBoxDialogue.Invalidate();
        }
        private int CalculateFreeSpace()
        {
            int index = this.index;
            int used = 0;
            int size = 0;
            int end = 0;
            if (index >= 0x0C00)
            {
                size = (0xFFFF - 0xEDE0) + 0x9000;
                index = 0x0C00;
                end = 0x1000;
            }
            else if (index >= 0x0800)
            {
                size = 0xF2D5;
                index = 0x0800;
                end = 0x0C00;
            }
            else
            {
                size = 0xFD18;
                index = 0x0000;
                end = 0x0800;
            }
            for (int i = index; i < end; i++)
            {
                if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                {
                    used += dialogues[i].Length;
                    if (i < end - 1 && used + dialogues[i + 1].Length > size && i + 1 == dialogues[i + 1].Reference && dialogues[i + 1].Position == 0)
                    {
                        bool test = false;
                        test = (size >= used + dialogues[i + 1].Length);
                    }
                }
            }
            return size - used;
        }
        private int CalculateFreeTableSpace()
        {
            int used = 0;
            for (int i = 0; i < dte.Length; i++)
                used += dte[i].Length + 1;
            int left = 0x40 - used;
            this.freeTableBytes.Text = "(" + left.ToString() + " characters left)";
            this.freeTableBytes.BackColor = left >= 0 ? SystemColors.Control : Color.Red;
            return left;
        }
        private bool FreeSpace(int current)
        {
            int used = 0;
            int size = 0;
            int index = 0;
            int end = 0;
            double left = 0;
            if (current >= 0x0C00)
            {
                size = (0xFFFF - 0xEDE0) + 0x9000;
                index = 0x0C00;
                end = 0x1000;
            }
            else if (current >= 0x0800)
            {
                size = 0xF2D5;
                index = 0x0800;
                end = 0x0C00;
            }
            else
            {
                size = 0xFD18;
                index = 0x0000;
                end = 0x0800;
            }
            for (int i = index; i < end; i++)
            {
                if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                {
                    used += dialogues[i].Length;
                    if (i < end - 1 && used + dialogues[i + 1].Length > size && i + 1 == dialogues[i + 1].Reference && dialogues[i + 1].Position == 0)
                    {
                        bool test = false;
                        test = (size >= used + dialogues[i + 1].Length);
                        if (!test)
                        {
                            i++;
                            return false;
                        }
                    }
                }
            }
            left = (double)(size - used);
            return true;
        }
        public void InsertIntoDialogueText(string toInsert)
        {
            char[] newText = new char[dialogueTextBox.Text.Length + toInsert.Length];
            dialogueTextBox.Text.CopyTo(0, newText, 0, dialogueTextBox.SelectionStart);
            toInsert.CopyTo(0, newText, dialogueTextBox.SelectionStart, toInsert.Length);
            dialogueTextBox.Text.CopyTo(dialogueTextBox.SelectionStart, newText, dialogueTextBox.SelectionStart + toInsert.Length, this.dialogueTextBox.Text.Length - this.dialogueTextBox.SelectionStart);
            dialogue.SetCaretPosition(this.dialogueTextBox.SelectionStart + toInsert.Length, byteView);
            dialogue.SetText(new string(newText), byteView, dteStr);
            RefreshDialogueEditor();
            SetTextImage();
        }
        private void SetDialogueTable(TextBox textBox)
        {
            char[] text = textBox.Text.ToCharArray();
            bool preview = true, valid = true, fail = false;
            char[] swap;
            int temp;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[' && preview == false) // Open bracket when we have already had an open bracket
                    fail = true;
                if (text[i] == '[') // Open Bracket
                {
                    preview = false;
                    i++;
                    temp = i;
                    while (temp < text.Length && text[temp] != ']')
                    {
                        if (byteView)
                        {
                            if (!(text[temp] >= '0' && text[temp] <= '9'))
                                fail = true;
                        }
                        temp++;
                    }
                }
                else if (preview == false && text[i] == ']') // Close bracket after open bracket
                    preview = true;
                else if (preview == true && valid == true)
                    valid = textHelper.IsValidSymbol(text[i]);
                if (i < text.Length && text[i] == '\n')
                {
                    int tempSel = textBox.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    textBox.Text = new string(swap);
                    text = textBox.Text.ToCharArray();
                    i += 2;
                    textBox.SelectionStart = tempSel + 2;
                }
            }
            if (preview && valid && !fail)
                dte[(int)textBox.Tag].SetText(textBox.Text, byteView);
            CalculateFreeTableSpace();
        }
        private void EncodeDialogues_()
        {
        }
        // duplicate dialogues
        private void FindWithinDialogues()
        {
            int i = 0; int diff = 0;
            for (i = 0; i < 0x800; i++)
            {
                if (i < 0x7FF) diff = dialogues[i + 1].Pointer - dialogues[i].Pointer;
                if (i < 0x7FF && dialogues[i].Text.Length > diff && diff > 0)
                {
                    dialogues[i + 1].Parent = i;
                    dialogues[i + 1].Position = diff;
                }
            }
            i = 0; diff = 0;
            for (i = 0x800; i < 0xC00; i++)
            {
                if (i < 0xBFF) diff = dialogues[i + 1].Pointer - dialogues[i].Pointer;
                if (i < 0xBFF && dialogues[i].Text.Length > diff && diff > 0)
                {
                    dialogues[i + 1].Parent = i;
                    dialogues[i + 1].Position = diff;
                }
            }
            i = 0; diff = 0;
            for (i = 0xC00; i < 0x1000; i++)
            {
                if (i < 0xFFF) diff = dialogues[i + 1].Pointer - dialogues[i].Pointer;
                if (i < 0xFFF && dialogues[i].Text.Length > diff && diff > 0)
                {
                    dialogues[i + 1].Parent = i;
                    dialogues[i + 1].Position = diff;
                }
            }
        }
        private void FindDuplicateDialoguePointers()
        {
            int i, a;
            for (i = 0; i < 0x800; i++)
            {
                a = i;
                if (dialogues[i + 1].Pointer == dialogues[a].Pointer)
                {
                    dialogues[i].Reference = i; i++;
                    while (i < 0x800 && dialogues[i].Pointer == dialogues[a].Pointer)
                    {
                        dialogues[i].Reference = a;
                        i++;
                    }
                    i--;
                }
                else dialogues[i].Reference = i;
            }
            for (i = 0x800; i < 0xC00; i++)
            {
                a = i;
                if (dialogues[i + 1].Pointer == dialogues[a].Pointer)
                {
                    dialogues[i].Reference = i; i++;
                    while (i < 0xC00 && dialogues[i].Pointer == dialogues[a].Pointer)
                    {
                        dialogues[i].Reference = a;
                        i++;
                    }
                    i--;
                }
                else dialogues[i].Reference = i;
            }
            for (i = 0xC00; i < 0x1000; i++)
            {
                a = i;
                if (dialogues[i + 1].Pointer == dialogues[a].Pointer)
                {
                    dialogues[i].Reference = i; i++;
                    while (i < 0x1000 && dialogues[i].Pointer == dialogues[a].Pointer)
                    {
                        dialogues[i].Reference = a;
                        i++;
                    }
                    i--;
                }
                else dialogues[i].Reference = i;
            }
        }
        private void LoadSearch(RichTextBox searchResults, StringComparison stringComparison, bool matchWholeWord, bool replaceAll, string replaceWith)
        {
            string dialogueSearch = "";
            int j = 0;
            for (int i = 0; i < dialogues.Length; i++)
            {
                string dialogue = dialogues[i].GetText(byteView, dteStr);
                int index = dialogue.IndexOf(searchBox.Text, stringComparison);
                if (index >= 0)
                {
                    if (matchWholeWord)
                    {
                        if (index + searchBox.Text.Length < dialogue.Length && Char.IsLetter(dialogue, index + searchBox.Text.Length))
                            continue;
                        if (index - 1 >= 0 && Char.IsLetter(dialogue, index - 1))
                            continue;
                    }
                    j++;
                    if (replaceAll)
                    {
                        dialogue = dialogue.Replace(searchBox.Text, replaceWith);
                        dialogues[i].SetText(dialogue, byteView, dteStr);
                    }
                    dialogueSearch += "[" + dialogues[i].Index.ToString() + "]\n";
                    dialogueSearch += dialogues[i].GetText(byteView, dteStr) + "\n\n";
                }
            }
            searchResults.Text = j.ToString() + " results...\n\n" + dialogueSearch;
            if (replaceAll)
            {
                MessageBox.Show(j.ToString() + " occurrences were replaced.", "LAZYSHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dialogueNum_ValueChanged(null, null);
            }
        }
        // editors
        public void LoadFontEditor()
        {
            if (fonts == null)
            {
                fonts = new Fonts(this);
                fonts.TopLevel = false;
                fonts.Dock = DockStyle.Fill;
                panelDialogues.Controls.Add(fonts);
                fonts.BringToFront();
                fonts.Show();
            }
            else
                fonts.Reload(this);
        }
        public void RedrawTileset()
        {
            dialogueTileset = new DialogueTileset(fontPalette);
            SetTilesetImage();
        }
        public void RedrawText()
        {
            SetTextImage();
            battleDialogues.SetTextImage();
            option.Image = Do.PixelsToImage(fontTriangle[0].GetPixels(fontPalette.Palettes[1]), 8, 16);
        }
        // assembler
        public void Assemble()
        {
            // Assemble the overworld menu palette
            Model.FontPaletteMenu.Assemble(Model.MenuPalettes, 0);
            byte[] compressed = new byte[0x200];
            int pointerOffset = Bits.GetShort(Model.ROM, 0x3E000C);  // may have changed when menus last saved
            int maxSize = Bits.GetShort(Model.ROM, 0x3E000E) - pointerOffset;  // may have changed when menus last saved
            int totalSize = Comp.Compress(Model.MenuPalettes, compressed);
            if (totalSize > maxSize)
                MessageBox.Show("Not enough space for compressed menu palettes. The total required space (" +
                    totalSize + " bytes) exceeds " + maxSize +  " bytes.\n\n" + "The menu palettes were not saved.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Bits.SetBytes(Model.ROM, pointerOffset + 0x3E0000 + 1, compressed, 0, totalSize - 1);
            //
            fonts.Assemble();
            battleDialogues.Assemble();
            if (!dialogueTextBox.IsDisposed && !dialogueTextBox.Text.EndsWith("[0]") && !dialogueTextBox.Text.EndsWith("[6]"))
            {
                dialogueTextBox.SelectionStart = dialogueTextBox.Text.Length;
                InsertIntoDialogueText("[0]");
            }
            // Assemble the dialogue
            string original, within;
            int length = 0;
            int i = 0;
            // assemble table
            if (CalculateFreeTableSpace() >= 0)
            {
                for (i = 0; i < dte.Length && length + dte[i].Length < 0x40; i++)
                    dte[i].Assemble(ref length);
            }
            else
                MessageBox.Show("The dialogue table was not saved. Please delete the necessary number of bytes for space.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // 000 - 1ff
            if (FreeSpace(0))
            {
                length = 0x0008;
                for (i = 0; i < 0x0800 && (length + dialogues[i].Length < 0xFD18 || (i != dialogues[i].Reference && !isDialogueChanged[i])); i++)
                {
                    if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                        dialogues[i].Assemble(ref length);
                    else if (dialogues[i].Position != 0)
                    {
                        original = new string(dialogues[dialogues[i].Parent].Text);
                        within = new string(dialogues[i].Text);
                        dialogues[i].Assemble(dialogues[dialogues[i].Parent].Pointer + original.IndexOf(within) + 8);
                    }
                    else
                        dialogues[i].Assemble(dialogues[dialogues[i].Reference].Pointer + 8);
                    // if the next dialogue has a smaller pointer or points to a place in the current dialogue, and both the current and next dialogues haven't changed
                }
            }
            else
                MessageBox.Show("The dialogue in bank 1 was not saved. Please delete the necessary number of bytes for space.\n\nLast dialogue saved was #" + i.ToString() + ". It should have been #2047",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (FreeSpace(0x800))
            {
                length = 0x0004;
                for (i = 0x0800; i < 0x0C00 && (length + dialogues[i].Length < 0xF2D5 || (i != dialogues[i].Reference && !isDialogueChanged[i])); i++)
                {
                    if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                        dialogues[i].Assemble(ref length);
                    else if (dialogues[i].Position != 0)
                    {
                        original = new string(dialogues[dialogues[i].Parent].Text);
                        within = new string(dialogues[i].Text);
                        dialogues[i].Assemble(dialogues[dialogues[i].Parent].Pointer + original.IndexOf(within) + 4);
                    }
                    else
                        dialogues[i].Assemble(dialogues[dialogues[i].Reference].Pointer + 4);
                }
            }
            else
                MessageBox.Show("The dialogue in bank 2 was not saved. Please delete the necessary number of bytes for space.\n\nLast dialogue saved was #" + i.ToString() + ". It should have been #2047",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (FreeSpace(0xC00))
            {
                length = 0x0004;
                for (i = 0x0C00; i < 0x1000 && length + dialogues[i].Length < 0x8FFF; i++)
                {
                    if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                        dialogues[i].Assemble(ref length);
                    else if (dialogues[i].Position != 0)
                    {
                        original = new string(dialogues[dialogues[i].Parent].Text);
                        within = new string(dialogues[i].Text);
                        dialogues[i].Assemble(dialogues[dialogues[i].Parent].Pointer + original.IndexOf(within) + 4);
                    }
                    else
                        dialogues[i].Assemble(dialogues[dialogues[i].Reference].Pointer + 4);
                }
                length = 0xEDE0;
                for (; i < 0x1000 && length + dialogues[i].Length < 0xFFFF; i++)
                {
                    if (i == dialogues[i].Reference && dialogues[i].Position == 0)
                        dialogues[i].Assemble(ref length);
                    else if (dialogues[i].Position != 0)
                    {
                        original = new string(dialogues[dialogues[i].Parent].Text);
                        within = new string(dialogues[i].Text);
                        dialogues[i].Assemble(dialogues[dialogues[i].Parent].Pointer + original.IndexOf(within) + 4);
                    }
                    else
                        dialogues[i].Assemble(dialogues[dialogues[i].Reference].Pointer + 4);
                }
            }
            else
                MessageBox.Show("The dialogue in bank 3 was not saved. Please delete the necessary number of bytes for space.\n\nLast dialogue saved was #" + i.ToString() + ". It should have been #2047",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            battleDialogues.Modified = false;
            fonts.Modified = false;
            this.Modified = false;
        }
        #endregion
        #region Event Handlers
        // main
        private void Dialogues_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modifyDTE)
                EncodeDialogues();
            if (!this.Modified && !battleDialogues.Modified && !fonts.Modified)
                goto Close;
            DialogResult result = MessageBox.Show(
                "Dialogues have not been saved.\n\nWould you like to save changes?", "LAZY SHELL",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Dialogues = null;
                Model.DTE = null;
                Model.BattleDialogues = null;
                Model.BattleMessages = null;
                Model.DialogueGraphics = null;
                Model.BattleDialogueTileset_bytes = null;
                Model.FontDescription = null;
                Model.FontDialogue = null;
                Model.FontMenu = null;
                Model.FontTriangle = null;
                Model.FontPaletteBattle = null;
                Model.FontPaletteDialogue = null;
                Model.FontPaletteMenu = null;
                Model.NumeralGraphics = null;
                Model.NumeralPaletteSet = null;
                Model.BattleMenuGraphics = null;
                Model.BattleMenuPalette = null;
                Model.BonusMessages = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            searchWindow.Close();
            fonts.NewFontTable.Close();
            battleDialogues.Close();
        }
        private void Dialogues_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void panel60_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder3D(e.Graphics, panel60.ClientRectangle, Border3DStyle.Raised, Border3DSide.All);
        }
        private void dialogueNum_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.Updating)
                return;
            dialoguePreview.Reset();
            RefreshDialogueEditor();
            SetTextImage();
            settings.LastDialogue = index;
        }
        private void textView_Click(object sender, EventArgs e)
        {
            this.dialogueTextBox.Text = dialogue.GetText(byteView, dteStr);
        }
        private void syncDupes_CheckedChanged(object sender, EventArgs e)
        {
            if (syncDupes.Checked || warning)
                return;
            MessageBox.Show("Unchecking this will disable sychronization of duplicate dialogues.\n\n" +
                "Modifying any duplicate dialogues might result in a significant loss of available byte space.",
                "LAZY SHELL");
            warning = true;
        }
        private void showDialogues_Click(object sender, EventArgs e)
        {
            panel60.Visible = showDialogues.Checked;
        }
        private void showBattleDialogues_Click(object sender, EventArgs e)
        {
            battleDialogues.Visible = showBattleDialogues.Checked;
        }
        private void showFonts_Click(object sender, EventArgs e)
        {
            fonts.Visible = showFonts.Checked;
        }
        private void showDialogueBG_Click(object sender, EventArgs e)
        {
        }
        private void pictureBoxDialogue_Paint(object sender, PaintEventArgs e)
        {
            if (dialogueBGImage != null)
                e.Graphics.DrawImage(dialogueBGImage, 0, 0);
            if (dialogueTextImage != null)
                e.Graphics.DrawImage(dialogueTextImage, 0, 0);
        }
        private void pageUp_Click(object sender, EventArgs e)
        {
            dialoguePreview.PageUp();
            SetTextImage();
        }
        private void pageDown_Click(object sender, EventArgs e)
        {
            dialoguePreview.PageDown();
            SetTextImage();
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
        }
        private void dialogueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            SetTextImage();
            int temp = CalculateFreeSpace();
            this.freeBytes.Text = temp.ToString() + " characters left";
            this.freeBytes.BackColor = temp >= 0 ? SystemColors.Control : Color.Red;
            if (dialogue.Reference != index)
                dialogueTextBox.BackColor = SystemColors.Info;
            else
                dialogueTextBox.BackColor = SystemColors.Window;
        }
        private void dialogueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue & 0x11) == 0x11)
                return;    // if ctrl+some other key, cancel
            if (e.KeyValue >= 0xA0)
                return;
            int temp = dialogue.Reference;
            //DialogResult result;
            if (!syncDupes.Checked)
                dialogue.Reference = index;
            if (!isDialogueChanged[index])
            {
                if (index < 0x800)
                {
                    for (int i = 0; i < 0x800; i++)
                    {
                        if (!syncDupes.Checked && dialogues[i].Reference == index)
                            dialogues[i].Reference = i;
                        else if (syncDupes.Checked && dialogues[i].Reference == dialogues[index].Reference)
                            dialogues[i].Text = dialogues[index].Text;
                        if (dialogues[i].Parent == index)
                            dialogues[i].Parent = 0;
                    }
                }
                else if (index < 0xC00)
                {
                    for (int i = 0x800; i < 0xC00; i++)
                    {
                        if (!syncDupes.Checked && dialogues[i].Reference == index)
                            dialogues[i].Reference = i;
                        else if (syncDupes.Checked && dialogues[i].Reference == dialogues[index].Reference)
                            dialogues[i].Text = dialogues[index].Text;
                        if (dialogues[i].Parent == index)
                            dialogues[i].Parent = 0;
                    }
                }
                else if (index < 0x1000)
                {
                    for (int i = 0xC00; i < 0x1000; i++)
                    {
                        if (!syncDupes.Checked && dialogues[i].Reference == index)
                            dialogues[i].Reference = i;
                        else if (syncDupes.Checked && dialogues[i].Reference == dialogues[index].Reference)
                            dialogues[i].Text = dialogues[index].Text;
                        if (dialogues[i].Parent == index)
                            dialogues[i].Parent = 0;
                    }
                }
            }
            if (!syncDupes.Checked)
                isDialogueChanged[index] = true;
            int freebytes = CalculateFreeSpace();
            this.freeBytes.Text = freebytes.ToString() + " characters left";
            this.freeBytes.BackColor = freebytes >= 0 ? SystemColors.Control : Color.Red;
            if (dialogue.Reference != index)
                dialogueTextBox.BackColor = SystemColors.Info;
            else
                dialogueTextBox.BackColor = SystemColors.Window;
        }
        private void dialogueTextBox_Enter(object sender, EventArgs e)
        {
        }
        private void dialogueTextBox_Leave(object sender, EventArgs e)
        {
            if (!dialogueTextBox.Text.EndsWith("[0]") && !dialogueTextBox.Text.EndsWith("[6]"))
            {
                dialogueTextBox.SelectionStart = dialogueTextBox.Text.Length;
                InsertIntoDialogueText("[0]");
            }
        }
        // inserts
        private void newLine_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[1]");
            else
                InsertIntoDialogueText("[newLine]");
        }
        private void newLineA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[2]");
            else
                InsertIntoDialogueText("[newLineInput]");
        }
        private void newPage_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[4]");
            else
                InsertIntoDialogueText("[newPage]");
        }
        private void newPageA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[3]");
            else
                InsertIntoDialogueText("[newPageInput]");
        }
        private void endString_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[6]");
            else
                InsertIntoDialogueText("[end]");
        }
        private void endStringA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[0]");
            else
                InsertIntoDialogueText("[endInput]");
        }
        private void pause60f_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[12]");
            else
                InsertIntoDialogueText("[delay]");
        }
        private void pauseA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[5]");
            else
                InsertIntoDialogueText("[pauseInput]");
        }
        private void pauseFramesInsert_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[13][" + this.pauseFrameNum.Value.ToString() + "]");
            else
                InsertIntoDialogueText("[FRAME DELAY][ " + this.pauseFrameNum.Value.ToString() + "]");
            dialogueTextBox.Focus();
        }
        private void variablesInsert_Click(object sender, EventArgs e)
        {
            int variable = this.variables.SelectedIndex;
            if (byteView)
            {
                if (variable > 0)
                {
                    variable--;
                    InsertIntoDialogueText("[28][" + variable.ToString() + "]");
                }
                else
                    InsertIntoDialogueText("[26]");
            }
            else
            {
                if (variable > 0)
                {
                    variable--;
                    InsertIntoDialogueText("[NUMBER FROM EVENT MEMORY][ " + variable.ToString() + "]");
                }
                else
                    InsertIntoDialogueText("[ITEM VARIABLE FROM EVENT Memory $70A7]");
            }
            dialogueTextBox.Focus();
        }
        private void option_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDialogueText("[7]");
            else
                InsertIntoDialogueText("[option]");
        }
        private void allDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "EXPORT DIALOGUES INTO TEXT FILE...";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = "dialogues";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dialogues = File.CreateText(saveFileDialog.FileName);
                for (int i = 0; i < this.dialogues.Length; i++)
                {
                    dialogues.WriteLine(
                        "{" + i.ToString("d4") + "}\t" +
                        this.dialogues[i].GetText(true, dteStr));
                }
                dialogues.Close();
            }
        }
        private void allDialoguesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "IMPORT DIALOGUES FROM TEXT FILE...";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName;
            TextReader tr;
            BinaryFormatter b = new BinaryFormatter();
            try
            {
                tr = new StreamReader(path);
                while (tr.Peek() != -1)
                {
                    string line = tr.ReadLine();
                    int number = Convert.ToInt32(line.Substring(1, 4), 10);
                    line = line.Remove(0, 7);
                    if (!line.EndsWith("[0]") && !line.EndsWith("[6]"))
                        line += "[0]";
                    dialogues[number].SetText(line, true, dteStr);
                }
                dialogueNum_ValueChanged(null, null);
            }
            catch
            {
                MessageBox.Show(
                    "There was a problem loading Dialogue data. Verify that the lines in the\n" +
                    "text file are correctly named.\n\n" +
                    "Each line must begin with the 4-digit dialogue number enclosed in {},\n" +
                    "followed by a tab character, then the raw dialogue itself.",
                    "LAZY SHELL");
            }
        }
        // compression table
        private void dctApply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "You are about to apply the compression table to all dialogues, which involves re-encoding all 4,096 dialogues. This procedure may take up to half a minute to complete.\n\nGo ahead with process?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            EncodeDialogues_();
        }
        private void dct_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            SetDialogueTable((TextBox)sender);
            modifyDTE = CalculateFreeTableSpace() >= 0;
        }
        private void dct_Leave(object sender, EventArgs e)
        {
        }
        private void panel1_Leave(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (modifyDTE)
                EncodeDialogues();
        }
        private void EncodeDialogues()
        {
            if (encodeDialogues.IsBusy)
                return;
            this.Enabled = false;
            this.Text = "***ENCODING DIALOGUES***";
            // reencode all dialogues
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogues[i].SetText(dialogues[i].GetText(byteView, dteStr), byteView, Model.DTEStr(byteView));
                if (i % 16 == 0)
                    progressBar1.PerformStep();
            }
            //
            this.Enabled = true;
            this.Text = "DIALOGUES - Lazy Shell";
            modifyDTE = false;
            progressBar1.Value = 0;
            dteStrByte = Model.DTEStr(true);
            dteStrText = Model.DTEStr(false);
            dialogueNum_ValueChanged(null, null);
        }
        private void encodeDialogues_DoWork(object sender, DoWorkEventArgs e)
        {
        }
        private void encodeDialogues_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void encodeDialogues_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        // menu strip
        private void save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Assemble();
            Cursor.Current = Cursors.Arrow;
        }
        private void importDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Dialogues";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName;
            TextReader tr;
            BinaryFormatter b = new BinaryFormatter();
            this.Enabled = false;
            this.Text = "***IMPORTING DIALOGUES***";
            try
            {
                string[] tables = dteStr;
                tr = new StreamReader(path);
                while (tr.Peek() != -1)
                {
                    string line = tr.ReadLine();
                    int index = Convert.ToInt32(line.Substring(1, 4), 10);
                    line = line.Remove(0, 7);
                    if (this.byteView)
                    {
                        if (!line.EndsWith("[0]") && !line.EndsWith("[6]"))
                            line += "[0]";
                    }
                    else
                        if (!line.EndsWith("[endInput]") && !line.EndsWith("[end]"))
                            line += "[endInput]";
                    dialogues[index].SetText(line, line.EndsWith("[0]") || line.EndsWith("[6]"), tables);
                    if (index % 16 == 0)
                        progressBar1.PerformStep();
                }
                dialogueNum_ValueChanged(null, null);
            }
            catch
            {
                MessageBox.Show(
                    "There was a problem loading dialogues. Verify that the lines are correctly named.\n\n" +
                    "Each line must begin with a 4-digit index enclosed in {}, followed by a tab character, then the raw dialogue.",
                    "LAZY SHELL");
            }
            this.Enabled = true;
            this.Text = "DIALOGUES - Lazy Shell";
            progressBar1.Value = 0;
        }
        private void importBattleDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Battle Dialogues";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string path = openFileDialog.FileName;
            TextReader tr;
            BinaryFormatter b = new BinaryFormatter();
            try
            {
                tr = new StreamReader(path);
                while (tr.Peek() != -1)
                {
                    string line = tr.ReadLine();
                    int number = Convert.ToInt32(line.Substring(1, 4), 10);
                    line = line.Remove(0, 7);
                    if (battleDialogues.byteView)
                    {
                        if (!line.EndsWith("[0]") && !line.EndsWith("[6]"))
                            line += "[0]";
                    }
                    else
                        if (!line.EndsWith("[endInput]") && !line.EndsWith("[end]"))
                            line += "[endInput]";
                    Model.BattleDialogues[number].SetText(line, battleDialogues.byteView);
                }
                dialogueNum_ValueChanged(null, null);
            }
            catch
            {
                MessageBox.Show(
                    "There was a problem loading dialogues. Verify that the lines are correctly named.\n\n" +
                    "Each line must begin with a 4-digit index enclosed in {}, followed by a tab character, then the raw dialogue.",
                    "LAZY SHELL");
            }
        }
        private void exportDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Dialogues";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = "dialogues";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dialogues = File.CreateText(saveFileDialog.FileName);
                for (int i = 0; i < this.dialogues.Length; i++)
                {
                    dialogues.WriteLine(
                        "{" + i.ToString("d4") + "}\t" +
                        this.dialogues[i].GetText(byteView, dteStr));
                }
                dialogues.Close();
            }
        }
        private void exportBattleDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Battle Dialogues";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = "battleDialogues";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dialogues = File.CreateText(saveFileDialog.FileName);
                for (int i = 0; i < Model.BattleDialogues.Length; i++)
                {
                    dialogues.WriteLine(
                        "{" + i.ToString("d4") + "}\t" +
                        Model.BattleDialogues[i].GetText(battleDialogues.byteView));
                }
                dialogues.Close();
            }
        }
        private void clearDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(dialogues, (int)dialogueNum.Value, "CLEAR DIALOGUES...").ShowDialog();
            dialogueNum_ValueChanged(null, null);
        }
        private void clearBattleDialoguesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.BattleDialogues, battleDialogues.Index, "CLEAR BATTLE DIALOGUES...").ShowDialog();
            battleDialogues.RefreshBattleDialogue();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current dialogue. Go ahead with reset?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            dialogue = new Dialogue(index);
            dialoguePreview.Reset();
            RefreshDialogueEditor();
            SetTextImage();
        }
        #endregion
    }
}
