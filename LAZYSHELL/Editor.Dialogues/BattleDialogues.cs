using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class BattleDialogues : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private Dialogues dialoguesEditor;
        private BattleDialoguePreview textPreview = new BattleDialoguePreview();
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        private BattleDialogueTileset tileset { get { return Model.BattleDialogueTileset_tiles; } set { Model.BattleDialogueTileset_tiles = value; } }
        public BattleDialogueTileset Tileset { get { return tileset; } }
        private Bitmap tilesetImage { get { return Model.BattleDialogueTilesetImage; } set { Model.BattleDialogueTilesetImage = value; } }
        private Bitmap textImage;
        private Overlay overlay;
        private Search search;
        // accessors
        private BattleDialogue[] dialogues
        {
            get
            {
                if (type == 0)
                    return Model.BattleDialogues;
                else
                    return Model.BattleMessages;
            }
        }
        private BattleDialogue dialogue
        {
            get
            {
                if (type == 0)
                    return Model.BattleDialogues[index];
                else
                    return Model.BattleMessages[index];
            }
            set
            {
                if (type == 0)
                    Model.BattleDialogues[index] = value;
                else
                    Model.BattleMessages[index] = value;
            }
        }
        private byte[] graphics { get { return Model.DialogueGraphics; } set { Model.DialogueGraphics = value; } }
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } set { Model.FontDialogue = value; } }
        private PaletteSet fontPalette { get { return Model.FontPaletteDialogue; } set { Model.FontPaletteDialogue = value; } }
        public bool byteView = true;
        // local variables
        private int index { get { return (int)battleDialogueNum.Value; } set { battleDialogueNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private int mouseDownTile = 0;
        private int type { get { return battleDlgType.SelectedIndex; } set { battleDlgType.SelectedIndex = value; } }
        // editors
        private TileEditor tileEditor;
        public GraphicEditor graphicEditor;
        public PaletteEditor paletteEditor;
        public PaletteEditor paletteEditorMenu;
        #endregion
        #region Functions
        public BattleDialogues(Dialogues dialoguesEditor)
        {
            this.dialoguesEditor = dialoguesEditor;
            this.overlay = new Overlay();
            InitializeComponent();
            // tileset
            tileset = new BattleDialogueTileset(fontPalette);
            SetTilesetImage();
            // editors
            LoadPaletteEditor();
            LoadPaletteMenuEditor();
            LoadGraphicEditor();
            LoadTileEditor();
            // controls
            this.Updating = true;
            type = 0;
            search = new Search(battleDialogueNum, searchBox, searchButton, dialogues);
            RefreshBattleDialogue();
            this.Updating = false;
        }
        public new void Close()
        {
            search.Close();
            paletteEditor.Close();
            paletteEditorMenu.Close();
            tileEditor.Close();
            graphicEditor.Close();
            search.Dispose();
            paletteEditor.Dispose();
            paletteEditorMenu.Dispose();
            tileEditor.Dispose();
            graphicEditor.Dispose();
        }
        public void RefreshBattleDialogue()
        {
            this.Updating = true;
            this.index = (int)this.battleDialogueNum.Value;
            if (type == 0)
            {
                this.battleDialogueTextBox.Text = Model.BattleDialogues[index].GetText(byteView);
                this.battleDialogueTextBox.SelectionStart = Model.BattleDialogues[index].GetCaretPosition(byteView);
            }
            else
            {
                this.battleDialogueTextBox.Text = Model.BattleMessages[index].GetText(byteView);
                this.battleDialogueTextBox.SelectionStart = Model.BattleMessages[index].GetCaretPosition(byteView);
            }
            CalculateFreeSpace();
            SetTextImage();
            this.Updating = false;
        }
        private void SetTilesetImage()
        {
            tilesetImage = Do.PixelsToImage(
                Do.TilesetToPixels(tileset.Tileset_tiles, 16, 2, 0, false), 256, 32);
            pictureBoxBattleDialogue.BackColor = Color.FromArgb(fontPalette.Palette[0]);
            pictureBoxBattleDialogue.Invalidate();
        }
        public void SetTextImage()
        {
            char[] text = battleDialogueTextBox.Text.ToCharArray();
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
                    temp = ++i;
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
                    int tempSel = battleDialogueTextBox.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    battleDialogueTextBox.Text = new string(swap);
                    text = battleDialogueTextBox.Text.ToCharArray();
                    i += 2;
                    battleDialogueTextBox.SelectionStart = tempSel + 2;
                }
            }
            if (preview && valid && !fail)
            {
                dialogue.SetText(battleDialogueTextBox.Text, byteView);
                int[] pixels = textPreview.GetPreview(fontDialogue, fontPalette.Palettes[1], dialogue.Text, false);
                textImage = Do.PixelsToImage(pixels, 256, 32);
            }
            pictureBoxBattleDialogue.Invalidate();
        }
        private void CalculateFreeSpace()
        {
            int used = 0; int size;
            if (type == 0)
            {
                size = (0x92d1 - 0x6754) + (0x2aa9 - 0x260a) + (0xbfff - 0xbc58);/*(0xffff - 0xf400) USED FOR BATTLE SCRIPTS NOW*/
                for (int i = 0; i < Model.BattleDialogues.Length - 1; i++)
                {
                    used += Model.BattleDialogues[i].Length;
                    if (used + Model.BattleDialogues[i].Length > size)
                    {
                        bool test = size >= used + Model.BattleDialogues[i].Length;
                        if (!test)
                        {
                            availableBytes.Text = "Entry " + i++.ToString() + " Too Long - Cannot Save";
                            return;
                        }
                    }
                }
            }
            else
            {
                size = (0x2A00 - 0x274D);
                for (int i = 0; i < Model.BattleMessages.Length; i++)
                    used += Model.BattleMessages[i].Length;
            }
            availableBytes.Text = ((double)(size - used)).ToString() + " characters left";
        }
        private bool FreeSpace(bool message)
        {
            int used = 0;
            int size;
            if (!message)
            {
                size = (0x92d1 - 0x6754) + (0x2aa9 - 0x260a) + (0xbfff - 0xbc58);/*(0xffff - 0xf400) USED FOR BATTLE SCRIPTS NOW*/
                for (int i = 0; i < Model.BattleDialogues.Length - 1; i++)
                    used += Model.BattleDialogues[i].Length;
            }
            else
            {
                size = (0x2A00 - 0x274D);
                for (int i = 0; i < Model.BattleMessages.Length; i++)
                    used += Model.BattleMessages[i].Length;
            }
            return size - used < 0;
        }
        public void InsertIntoBattleDialogueText(string toInsert)
        {
            char[] newText = new char[battleDialogueTextBox.Text.Length + toInsert.Length];
            battleDialogueTextBox.Text.CopyTo(0, newText, 0, battleDialogueTextBox.SelectionStart);
            toInsert.CopyTo(0, newText, battleDialogueTextBox.SelectionStart, toInsert.Length);
            battleDialogueTextBox.Text.CopyTo(battleDialogueTextBox.SelectionStart, newText, battleDialogueTextBox.SelectionStart + toInsert.Length, this.battleDialogueTextBox.Text.Length - this.battleDialogueTextBox.SelectionStart);
            if (type == 0)
            {
                Model.BattleDialogues[index].SetCaretPosition(this.battleDialogueTextBox.SelectionStart + toInsert.Length, byteView);
                Model.BattleDialogues[index].SetText(new string(newText), byteView);
            }
            else
            {
                dialogue.SetCaretPosition(this.battleDialogueTextBox.SelectionStart + toInsert.Length, byteView);
                dialogue.SetText(new string(newText), byteView);
            }
            RefreshBattleDialogue();
            SetTextImage();
        }
        public void Assemble()
        {
            int i = 0;
            int length = 0x6754;
            if (!FreeSpace(false))
            {
                for (; i < Model.BattleDialogues.Length && length + Model.BattleDialogues[i].Length < 0x92D1; i++)
                    Model.BattleDialogues[i].Assemble(ref length);
                length = 0x260A;// - 0x392AA9
                for (; i < Model.BattleDialogues.Length && length + Model.BattleDialogues[i].Length < 0x2AA9; i++)
                    Model.BattleDialogues[i].Assemble(ref length);
                length = 0xBC58;// - 0x39BFFF
                for (; i < Model.BattleDialogues.Length && length + Model.BattleDialogues[i].Length < 0xBfff; i++)
                    Model.BattleDialogues[i].Assemble(ref length);
            }
            else
                MessageBox.Show("Battle Dialogue exceeds max size, decrease total size to save correctly.\nNote: not all text has been saved.");
            if (!FreeSpace(true))
            {
                i = 0;
                length = 0x274D;
                for (; i < Model.BattleMessages.Length && length + Model.BattleMessages[i].Length < 0x2A00; i++)
                    Model.BattleMessages[i].Assemble(ref length);
            }
            else
                MessageBox.Show("Battle Message exceeds max size, decrease total size to save correctly.\nNote: not all text has been saved.");
            //
            int offset = 0xF975;
            for (i = 0; i < Model.BonusMessages.Length; i++)
                Model.BonusMessages[i].Assemble(ref offset);
        }
        // editors
        public void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), fontPalette, 2, 1, 1);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), fontPalette, 2, 1, 1);
        }
        public void LoadPaletteMenuEditor()
        {
            if (paletteEditorMenu == null)
            {
                paletteEditorMenu = new PaletteEditor(new Function(PaletteMenuUpdate), Model.FontPaletteMenu, 2, 0, 2);
                paletteEditorMenu.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditorMenu.Reload(new Function(PaletteMenuUpdate), Model.FontPaletteMenu, 2, 0, 2);
        }
        public void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    graphics, graphics.Length, 0, fontPalette, 1, 0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    graphics, graphics.Length, 0, fontPalette, 1, 0x20);
        }
        private void LoadTileEditor()
        {
            if (tileEditor == null)
            {
                tileEditor = new TileEditor(new Function(TileUpdate),
                tileset.Tileset_tiles[mouseDownTile], graphics,
                fontPalette, 0x20);
                tileEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tileEditor.Reload(new Function(TileUpdate),
                tileset.Tileset_tiles[mouseDownTile], graphics,
                fontPalette, 0x20);
        }
        private void TileUpdate()
        {
            tileset.DrawTileset(tileset.Tileset_tiles, tileset.Tileset_bytes);
            SetTilesetImage();
        }
        private void PaletteUpdate()
        {
            dialoguesEditor.LoadFontEditor();
            dialoguesEditor.RedrawTileset();
            dialoguesEditor.RedrawText();
            tileset = new BattleDialogueTileset(fontPalette);
            SetTilesetImage();
            SetTextImage();
            dialoguesEditor.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void PaletteMenuUpdate()
        {
            dialoguesEditor.LoadFontEditor();
        }
        private void GraphicUpdate()
        {
            dialoguesEditor.LoadFontEditor();
            dialoguesEditor.RedrawTileset();
            dialoguesEditor.RedrawText();
            tileset = new BattleDialogueTileset(fontPalette);
            SetTilesetImage();
        }
        #endregion
        #region Event Handlers
        // main
        private void battleDialogueNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (type < 2)
            {
                textPreview.Reset();
                RefreshBattleDialogue();
            }
            else
            {
                bonusTextBox.Text = Model.BonusMessages[index].Text;
                bonusPreview.Invalidate();
            }
        }
        private void battleDialogueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            SetTextImage();
            CalculateFreeSpace();
        }
        private void battleDialogueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void battleDlgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            index = 0;
            if (type == 0)
                battleDialogueNum.Maximum = 255;
            else if (type == 1)
                battleDialogueNum.Maximum = 45;
            else
                battleDialogueNum.Maximum = 6;
            panel1.Visible = type < 2;
            searchBox.Visible = type < 2;
            if (type == 2 && searchButton.Checked)
                searchButton.PerformClick();
            searchButton.Visible = type < 2;
            bonusTextBox.Visible = type == 2;
            bonusPreview.Visible = type == 2;
            this.Updating = false;
            if (type < 2)
            {
                search.Names = dialogues;
                search.LoadSearch();
                textPreview.Reset();
                RefreshBattleDialogue();
            }
            else
            {
                bonusTextBox.Text = Model.BonusMessages[index].Text;
            }
        }
        private void bonusTextBox_TextChanged(object sender, EventArgs e)
        {
            Model.BonusMessages[index].Text = bonusTextBox.Text;
        }
        private void bonusPreview_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;
            string text = Model.BonusMessages[this.index].Text;
            int[] table = Model.Sprites[520].GetTilesetPixels();
            foreach (char letter in text)
            {
                int index = Lists.IndexOf(Lists.KeystrokesBonus, letter.ToString());
                if (index < 0 || index > 31)
                    continue;
                int[] pixels = Do.GetPixelRegion(table, 128, 16, 8, 8, index % 16 * 8, index / 16 * 8);
                e.Graphics.DrawImage(Do.PixelsToImage(pixels, 8, 8), x * 8, 0);
                x++;
            }
        }
        private void pictureBoxBattleDialogue_Paint(object sender, PaintEventArgs e)
        {
            if (tilesetImage != null)
                e.Graphics.DrawImage(tilesetImage, 0, 0, 256, 32);
            if (textImage != null && toggleText.Checked)
                e.Graphics.DrawImage(textImage, 0, 0, 256, 32);
            if (grid.Checked)
                overlay.DrawTileGrid(e.Graphics, Color.Black, pictureBoxBattleDialogue.Size, new Size(16, 16), 1, true);
        }
        private void pictureBoxBattleDialogue_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownTile = (e.Y / 16 * 16) + (e.X / 16);
            LoadTileEditor();
        }
        private void pictureBoxBattleDialogue_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxBattleDialogue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current battle dialogue. Go ahead with reset?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            if (type == 0)
                dialogue = new BattleDialogue(index, 0x396554, 0x390000);
            else
                dialogue = new BattleDialogue(index, 0x3A26F1, 0x3A0000);
            RefreshBattleDialogue();
        }
        // text insertion
        private void pageUp_Click(object sender, EventArgs e)
        {
            textPreview.PageUp();
            SetTextImage();
        }
        private void pageDown_Click(object sender, EventArgs e)
        {
            textPreview.PageDown();
            SetTextImage();
        }
        private void textView_Click(object sender, EventArgs e)
        {
            byteView = !textView.Checked;
            battleDialogueTextBox.Text = dialogue.GetText(byteView);
        }
        private void newLine_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoBattleDialogueText("[1]");
            else
                InsertIntoBattleDialogueText("[newLine]");
        }
        private void endString_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoBattleDialogueText("[0]");
            else
                InsertIntoBattleDialogueText("[end]");
        }
        private void pause60f_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoBattleDialogueText("[12]");
            else
                InsertIntoBattleDialogueText("[delay]");
        }
        private void pauseA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoBattleDialogueText("[2]");
            else
                InsertIntoBattleDialogueText("[pauseInput]");
        }
        private void pauseFrames_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoBattleDialogueText("[3]");
            else
                InsertIntoBattleDialogueText("[delayInput]");
        }
        // editors
        private void toggleText_Click(object sender, EventArgs e)
        {
            pictureBoxBattleDialogue.Invalidate();
        }
        private void grid_Click(object sender, EventArgs e)
        {
            pictureBoxBattleDialogue.Invalidate();
        }
        private void openTileEditor_Click(object sender, EventArgs e)
        {
            tileEditor.Show();
        }
        private void openGraphics_Click(object sender, EventArgs e)
        {
            graphicEditor.Show();
        }
        private void openPalettes_Click(object sender, EventArgs e)
        {
            paletteEditor.Show();
        }
        private void openPaletteMenu_Click(object sender, EventArgs e)
        {
            paletteEditorMenu.Show();
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
    [Serializable()]
    public class BonusMessage
    {
        public string Text;
        public int Index;
        public BonusMessage(int index)
        {
            this.Index = index;
            int offset = 0x020000 + Bits.GetShort(Model.ROM, index * 2 + 0x02F967);
            int length = Model.ROM[offset++];
            this.Text = "";
            for (int i = 0; i < length; i++)
                this.Text += Lists.KeystrokesBonus[Model.ROM[offset++]];
        }
        public void Assemble(ref int offset)
        {
            Bits.SetShort(Model.ROM, Index * 2 + 0x02F967, offset);
            Model.ROM[0x020000 + offset++] = (byte)Text.Length;
            foreach (char letter in Text)
            {
                int index = Lists.IndexOf(Lists.KeystrokesBonus, letter.ToString());
                if (index >= 0 || index <= 31)
                    Model.ROM[0x020000 + offset++] = (byte)index;
                else
                    Model.ROM[0x020000 + offset++] = 0x1F;
            }
        }
    }
}
