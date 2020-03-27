using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class Fonts : NewForm
    {
        #region Variables
        // main
        private delegate void Function();
        private Dialogues dialoguesEditor;
        private Overlay overlay;
        // accessors
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private BattleDialogues battleDialogues { get { return dialoguesEditor.BattleDialogues; } set { dialoguesEditor.BattleDialogues = value; } }
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
                    default: return fontTriangle;
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
                    default: fontTriangle = value; break;
                }
            }
        }
        private FontCharacter[] fontMenu { get { return Model.FontMenu; } set { Model.FontMenu = value; } }
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } set { Model.FontDialogue = value; } }
        private FontCharacter[] fontDescription { get { return Model.FontDescription; } set { Model.FontDescription = value; } }
        private FontCharacter[] fontTriangle { get { return Model.FontTriangle; } set { Model.FontTriangle = value; } }
        private FontCharacter[] fontBattleMenu { get { return Model.FontBattleMenu; } set { Model.FontBattleMenu = value; } }
        private PaletteSet fontPalettesDialogue { get { return Model.FontPaletteDialogue; } set { Model.FontPaletteDialogue = value; } }
        private PaletteSet fontPalettesMenu { get { return Model.FontPaletteMenu; } set { Model.FontPaletteMenu = value; } }
        private PaletteSet fontPalettesBattle { get { return Model.BattleMenuPalette; } set { Model.BattleMenuPalette = value; } }
        private GraphicEditor numeralGraphicEditor;
        private PaletteEditor numeralPaletteEditor;
        private GraphicEditor menuGraphicEditor;
        private PaletteEditor menuPaletteEditor;
        private GraphicEditor fontGraphicEditor;
        public FontCharacter[] FontCharacters { get { return font; } set { font = value; } }
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
        private int paletteRow
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Dialogue: return 1;
                    case FontType.Menu: return 0;
                    case FontType.Description: return 0;
                    case FontType.Triangles: return 1;
                    default: return 0;
                }
            }
        }
        private int paletteCol
        {
            get
            {
                if (FontType == FontType.Triangles)
                    return 4;
                return 0;
            }
        }
        private byte format
        {
            get
            {
                if (FontType == FontType.Triangles)
                    return 0x20;
                else
                    return 0x10;
            }
        }
        // font character variables
        private int currentFontChar = 0;
        private int overFontChar = 0;
        private Bitmap fontTableImage;
        private NewFontTable newFontTable;
        public NewFontTable NewFontTable { get { return newFontTable; } set { newFontTable = value; } }
        private string[] keystrokes
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Menu: return Lists.KeystrokesMenu;
                    case FontType.Dialogue: return Lists.Keystrokes;
                    case FontType.Description: return Lists.KeystrokesDesc;
                    default: return null;
                }
            }
        }
        private Settings settings = Settings.Default;
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
        public int[] Palette { get { return palette; } }
        private PaletteSet paletteSet
        {
            get
            {
                switch (FontType)
                {
                    case FontType.Menu:
                    case FontType.Description: return fontPalettesMenu;
                    case FontType.BattleMenu: return fontPalettesBattle;
                    default: return fontPalettesDialogue;
                }
            }
        }
        // special controls
        #endregion
        #region Functions
        public Fonts(Dialogues dialoguesEditor)
        {
            this.overlay = new Overlay();
            this.dialoguesEditor = dialoguesEditor;
            InitializeComponent();
            this.Updating = true;
            FontType = FontType.Dialogue;
            InitializeFonts();
            SetFontTableImage();
            this.Updating = false;
            //
            LoadPaletteEditor();
            LoadGraphicEditor();
            LoadMenuPaletteEditor();
            LoadMenuGraphicEditor();
            //
            newFontTable = new NewFontTable(this);
            newFontTable.FormClosing += new FormClosingEventHandler(newFontTable_FormClosing);
        }
        public void Reload(Dialogues dialoguesEditor)
        {
            SetFontTableImage();
            LoadFontGraphicEditor();
        }
        private void InitializeFonts()
        {
            this.Updating = true;
            switch (FontType)
            {
                case FontType.Menu: fontWidth.Enabled = true; fontWidth.Maximum = 8; break;
                case FontType.Dialogue: fontWidth.Enabled = true; fontWidth.Maximum = 16; break;
                case FontType.Description: fontWidth.Enabled = true; fontWidth.Maximum = 8; break;
                default: fontWidth.Enabled = false; break;
            }
            InitializeFontCharacter();
            InitializeKeystrokes();
            this.Updating = false;
        }
        private void InitializeKeystrokes()
        {
            if (FontType >= FontType.Triangles)
                return;
            if (fontTable.Controls.Count > 0)
            {
                RefreshKeystrokes();
                return;
            }
            this.Updating = true;
            fontTable.SuspendDrawing();
            fontTable.Controls.Clear();
            RichTextBox keyBox;
            for (int y = 16; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    int index = y * 8 + x;
                    keyBox = new RichTextBox();
                    keyBox.BackColor = Color.FromArgb(palette[3]);
                    keyBox.BorderStyle = BorderStyle.None;
                    keyBox.ForeColor = Color.FromArgb(palette[1]);
                    keyBox.Height = 12;
                    keyBox.Left = x * 16;
                    keyBox.MaxLength = 1;
                    keyBox.Multiline = false;
                    keyBox.Size = new Size(16, 12);
                    keyBox.TabIndex = y * 8 + x;
                    keyBox.Tag = index;
                    keyBox.Text = keystrokes[index + 32];
                    keyBox.Top = y * 12;
                    keyBox.Enter += new EventHandler(keyBox_Enter);
                    keyBox.MouseDown += new MouseEventHandler(keyBox_MouseDown);
                    keyBox.MouseMove += new MouseEventHandler(keyBox_MouseMove);
                    keyBox.MouseUp += new MouseEventHandler(keyBox_MouseUp);
                    keyBox.TextChanged += new EventHandler(keyBox_TextChanged);
                    //
                    fontTable.Controls.Add(keyBox);
                    keyBox.BringToFront();
                }
            }
            fontTable.ResumeDrawing();
            this.Updating = false;
        }
        private void RefreshKeystrokes()
        {
            this.Updating = true;
            fontTable.SuspendDrawing();
            foreach (RichTextBox keyBox in fontTable.Controls)
            {
                int index = (int)keyBox.Tag;
                keyBox.BackColor = Color.FromArgb(palette[3]);
                keyBox.ForeColor = Color.FromArgb(palette[1]);
                keyBox.Text = keystrokes[index + 32];
            }
            fontTable.ResumeDrawing();
            this.Updating = false;
        }
        private void InitializeFontCharacter()
        {
            this.Updating = true;
            if (FontType < FontType.Triangles)
                fontWidth.Value = font[currentFontChar].Width;
            LoadFontGraphicEditor();
            this.Updating = false;
        }
        public void RedrawText()
        {
            SetFontTableImage();
            LoadFontGraphicEditor();
            dialoguesEditor.RedrawText();
        }
        public void Assemble()
        {
            fontPalettesDialogue.Assemble();
            fontPalettesMenu.Assemble();
            Bits.SetBit(Model.ROM, 0x3E2D6C, 7, true);
            Bits.SetBit(Model.ROM, 0x3E2D74, 7, true);
            foreach (FontCharacter f in fontMenu) f.Assemble();
            foreach (FontCharacter f in fontDialogue) f.Assemble();
            foreach (FontCharacter f in fontDescription) f.Assemble();
            foreach (FontCharacter f in fontTriangle) f.Assemble();
            Bits.SetBytes(rom, 0x3DF000, Model.DialogueGraphics, 0, 0x700);
            Bits.SetBytes(rom, 0x015943, Model.BattleDialogueTileset_bytes, 0, 0x100);
            //
            Bits.SetBytes(rom, 0x03F800, Model.NumeralGraphics, 0, 0x400);
            Model.NumeralPaletteSet.Assemble();
            Buffer.BlockCopy(Model.BattleMenuGraphics, 0, rom, 0x1F200, 0x600);
            Buffer.BlockCopy(Model.BattleMenuGraphics, 0x600, rom, 0x1ED00, 0x140);
            Model.BattleMenuPalette.Assemble();
        }
        // set images
        private void SetFontTableImage()
        {
            int[] pixels = null;
            int[] palette = this.palette;
            int width = 0, height = 0;
            switch (FontType)
            {
                case FontType.Menu:
                case FontType.Description:
                case FontType.Dialogue:
                    width = 128; height = 192;
                    pixels = Do.DrawFontTable(font, palette, 128, 192, 16, 12, 8, 16);
                    break;
                case FontType.Triangles:
                    width = 112; height = 32;
                    pixels = new int[112 * height];
                    palette = fontPalettesDialogue.Palettes[1];
                    for (int x = 0; x < 7; x++) // left-right triangles
                        Do.PixelsToPixels(fontTriangle[x].GetPixels(palette), pixels, 112, x * 32, 0, 8, 16, true);
                    for (int x = 0; x < 7; x++) // up-down triangles
                        Do.PixelsToPixels(fontTriangle[x + 7].GetPixels(palette), pixels, 112, x * 16, 16, 16, 8, true);
                    break;
            }
            pictureBoxFontTable.Width = width;
            pictureBoxFontTable.Height = height;
            fontTableImage = Do.PixelsToImage(pixels, width, height);
            if (FontType == FontType.Triangles)
                pictureBoxFontTable.BackColor = Color.FromArgb(palette[0]);
            else if (FontType == FontType.BattleMenu)
                pictureBoxFontTable.BackColor = Color.FromArgb(palette[1]);
            else
                pictureBoxFontTable.BackColor = Color.FromArgb(palette[3]);
            pictureBoxFontTable.Invalidate();
        }
        //
        private void LoadGraphicEditor()
        {
            if (numeralGraphicEditor == null)
            {
                numeralGraphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    Model.NumeralGraphics, Model.NumeralGraphics.Length, 0, Model.NumeralPaletteSet, 0, 0x20);
                numeralGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                numeralGraphicEditor.Reload(new Function(GraphicUpdate),
                    Model.NumeralGraphics, Model.NumeralGraphics.Length, 0, Model.NumeralPaletteSet, 0, 0x20);
        }
        private void LoadPaletteEditor()
        {
            if (numeralPaletteEditor == null)
            {
                numeralPaletteEditor = new PaletteEditor(new Function(PaletteUpdate), Model.NumeralPaletteSet, 2, 0, 2);
                numeralPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                numeralPaletteEditor.Reload(new Function(PaletteUpdate), Model.NumeralPaletteSet, 2, 0, 2);
        }
        private void GraphicUpdate()
        {
        }
        private void PaletteUpdate()
        {
            LoadGraphicEditor();
            dialoguesEditor.Modified = true;
        }
        private void LoadMenuGraphicEditor()
        {
            if (menuGraphicEditor == null)
            {
                menuGraphicEditor = new GraphicEditor(new Function(MenuGraphicUpdate),
                    Model.BattleMenuGraphics, Model.BattleMenuGraphics.Length, 0, Model.BattleMenuPalette, 0, 0x20);
                menuGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                menuGraphicEditor.Reload(new Function(MenuGraphicUpdate),
                    Model.BattleMenuGraphics, Model.BattleMenuGraphics.Length, 0, Model.BattleMenuPalette, 0, 0x20);
        }
        private void LoadMenuPaletteEditor()
        {
            if (menuPaletteEditor == null)
            {
                menuPaletteEditor = new PaletteEditor(new Function(MenuPaletteUpdate), Model.BattleMenuPalette, 2, 0, 2);
                menuPaletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                menuPaletteEditor.Reload(new Function(MenuPaletteUpdate), Model.BattleMenuPalette, 2, 0, 2);
        }
        private void MenuGraphicUpdate()
        {
        }
        private void MenuPaletteUpdate()
        {
            LoadMenuGraphicEditor();
            dialoguesEditor.Modified = true;
        }
        private void LoadFontGraphicEditor()
        {
            if (fontGraphicEditor == null)
            {
                fontGraphicEditor = new GraphicEditor(new Function(FontGraphicUpdate),
                    font[currentFontChar], paletteSet, paletteRow, paletteCol, 0x10);
                fontGraphicEditor.SetWidth(font[currentFontChar].Width, font[currentFontChar].Height,
                    font[currentFontChar].MaxWidth / 8, font[currentFontChar].Height / 8);
                fontGraphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
                fontGraphicEditor.DockToPanel(panelCharacter, true, false);
                fontGraphicEditor.ZoomIn();
            }
            else
            {
                fontGraphicEditor.Reload(new Function(FontGraphicUpdate),
                    font[currentFontChar], paletteSet, paletteRow, paletteCol, 0x10);
                fontGraphicEditor.SetWidth(font[currentFontChar].Width, font[currentFontChar].Height,
                    font[currentFontChar].MaxWidth / 8, font[currentFontChar].Height / 8);
            }
        }
        private void FontGraphicUpdate()
        {
            //LoadFontGraphicEditor();
            SetFontTableImage();
            dialoguesEditor.RedrawText();
            dialoguesEditor.Modified = true;
        }
        #endregion
        #region Event handlers
        private void fontType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            currentFontChar = 0;
            this.Updating = true;
            fontWidth.Enabled = FontType < FontType.Triangles;
            toggleKeystrokes.Enabled = FontType < FontType.Triangles;
            if (toggleKeystrokes.Checked)
                toggleKeystrokes.Checked = FontType < FontType.Triangles;
            openNewFontTable.Enabled = FontType < FontType.Triangles;
            this.Updating = false;
            InitializeFonts();
            SetFontTableImage();
            if (FontType < FontType.Triangles)
                newFontTable.Reload();
            else
            {
                newFontTable.Hide();
                toggleKeystrokes.Checked = false;
                pictureBoxFontTable.Visible = true;
                fontTable.Visible = false;
                fontTable.SendToBack();
            }
        }
        private void numeralGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numeralGraphicEditor.Show();
        }
        private void numeralPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numeralPaletteEditor.Show();
        }
        private void battleMenuGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuGraphicEditor.Show();
        }
        private void battleMenuPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuPaletteEditor.Show();
        }
        private void pictureBoxFontTable_Paint(object sender, PaintEventArgs e)
        {
            if (fontTableImage != null)
                e.Graphics.DrawImage(fontTableImage, 0, 0);
            Size s = new Size(16, 12);
            if (showGrid.Checked && FontType != FontType.Triangles)
                overlay.DrawTileGrid(e.Graphics, Color.Gray, fontTableImage.Size, s, true, -1);
        }
        private void pictureBoxFontTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 ||
                e.X >= pictureBoxFontTable.Width ||
                e.Y >= pictureBoxFontTable.Height)
                return;
            pictureBoxFontTable.Focus();
            int before = currentFontChar;
            switch (FontType)
            {
                case FontType.Menu:
                case FontType.Description:
                case FontType.Dialogue: currentFontChar = e.Y / 12 * 8 + (e.X / 16); break;
                case FontType.Triangles:
                    if (e.X > 112)
                        return;
                    currentFontChar = e.Y / 16 * 7 + (e.X / 16); break;
            }
            if (currentFontChar == 59 || currentFontChar == 61)
            {
                MessageBox.Show("Character #91 and #93 cannot be edited because they are reserved for [ and ], respectively.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentFontChar = before;
                return;
            }
            InitializeFontCharacter();
        }
        private void pictureBoxFontTable_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 ||
                e.X >= pictureBoxFontTable.Width ||
                e.Y >= pictureBoxFontTable.Height)
                return;
            switch (FontType)
            {
                case FontType.Menu:
                case FontType.Description:
                case FontType.Dialogue: overFontChar = e.Y / 12 * 8 + (e.X / 16) + 32; break;
                case FontType.Triangles: if (e.X > 112)
                        return; overFontChar = e.Y / 16 * 7 + (e.X / 16); break;
            }
            indexLabel.Text = "[" + overFontChar + "]";
            if (e.Button == MouseButtons.Left)
                pictureBoxFontTable_MouseDown(sender, e);
        }
        private void pictureBoxFontTable_MouseEnter(object sender, EventArgs e)
        {
        }
        private void pictureBoxFontTable_MouseLeave(object sender, EventArgs e)
        {
            indexLabel.Text = "";
        }
        private void toggleKeystrokes_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleKeystrokes.Checked && Model.CheckLoadedProject())
            {
                pictureBoxFontTable.Visible = false;
                fontTable.Visible = true;
                fontTable.BringToFront();
            }
            else
            {
                toggleKeystrokes.Checked = false;
                pictureBoxFontTable.Visible = true;
                fontTable.Visible = false;
                fontTable.SendToBack();
            }
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
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                keystrokes[i] = line;
            }
            InitializeKeystrokes();
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
        private void keyBox_Enter(object sender, EventArgs e)
        {
            ((RichTextBox)sender).SelectAll();
        }
        private void keyBox_MouseMove(object sender, MouseEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            overFontChar = (int)rtb.Tag + 32;
            indexLabel.Text = "[" + overFontChar + "]";
            if (e.Button == MouseButtons.Left)
                keyBox_MouseDown(sender, e);
        }
        private void keyBox_MouseDown(object sender, MouseEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            currentFontChar = (int)rtb.Tag;
            InitializeFontCharacter();
        }
        private void keyBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((RichTextBox)sender).SelectAll();
        }
        private void keyBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RichTextBox rtb = (RichTextBox)sender;
            keystrokes[(int)rtb.Tag + 32] = rtb.Text;
        }
        private void openNewFontTable_Click(object sender, EventArgs e)
        {
            if (FontType < FontType.Triangles)
                newFontTable.Show();
        }
        private void newFontTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            newFontTable.Hide();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current font character. Go ahead with reset?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            font[currentFontChar] = new FontCharacter(currentFontChar, FontType);
            InitializeFontCharacter();
            SetFontTableImage();
            dialoguesEditor.RedrawText();
        }
        private void fontWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            font[currentFontChar].Width = (byte)fontWidth.Value;
            LoadFontGraphicEditor();
            SetFontTableImage();
            dialoguesEditor.RedrawText();
        }
        private void showGrid_Click(object sender, EventArgs e)
        {
            pictureBoxFontTable.Invalidate();
        }
        private void showBG_Click(object sender, EventArgs e)
        {
            pictureBoxFontTable.Invalidate();
        }
        // contextmenustrip1
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Import font graphics";
            openFileDialog1.Filter = "Image files (*.bmp,*.png,*.gif,*.jpg)|*.bmp;*.png;*.gif;*.jpg|Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            FileStream fs;
            BinaryReader br;
            Bitmap import;
            byte[] graphicBlock;
            int[] palette = new int[4];
            fs = File.OpenRead(openFileDialog1.FileName);
            if (Path.GetExtension(openFileDialog1.FileName) == ".jpg" ||
                Path.GetExtension(openFileDialog1.FileName) == ".gif" ||
                Path.GetExtension(openFileDialog1.FileName) == ".png")
            {
                for (int i = 0; i < 4; i++)
                    palette[i] = this.palette[i];
                import = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                graphicBlock = new byte[(import.Width * import.Height) / 4];
                Do.PixelsToBPP(
                    Do.ImageToPixels(import, new Size(import.Width, import.Height)),
                    graphicBlock, new Size(import.Width / 8, import.Height / 8), palette, 0x10);
                switch (FontType)
                {
                    case FontType.Menu:
                        Do.CopyOverFontTable(graphicBlock, font, new Size(import.Width / 8, import.Height / 12), palette);
                        font[0].Width = 4;
                        break;
                    case FontType.Dialogue:
                        Do.CopyOverFontTable(graphicBlock, font, new Size(import.Width / 16, import.Height / 12), palette);
                        font[0].Width = 4;
                        break;
                }
            }
            else
            {
                br = new BinaryReader(fs);
                switch (FontType)
                {
                    case FontType.Menu:
                        graphicBlock = new byte[0xC00];
                        graphicBlock = br.ReadBytes(0xC00);
                        foreach (FontCharacter f in fontMenu)
                            Array.Copy(graphicBlock, f.Index * 0x18, f.Graphics, 0, 0x18);
                        break;
                    case FontType.Dialogue:
                        graphicBlock = new byte[0x1800];
                        graphicBlock = br.ReadBytes(0x1800);
                        foreach (FontCharacter f in fontDialogue)
                            Array.Copy(graphicBlock, f.Index * 0x30, f.Graphics, 0, 0x30);
                        break;
                    case FontType.Description:
                        graphicBlock = new byte[0x800];
                        graphicBlock = br.ReadBytes(0x800);
                        foreach (FontCharacter f in fontDescription)
                            Array.Copy(graphicBlock, f.Index * 0x10, f.Graphics, 0, 0x10);
                        break;
                    case FontType.Triangles:
                        graphicBlock = new byte[0x1C0];
                        graphicBlock = br.ReadBytes(0x1C0);
                        foreach (FontCharacter f in fontTriangle)
                            Array.Copy(graphicBlock, f.Index * 0x20, f.Graphics, 0, 0x20);
                        break;
                }
                br.Close();
            }
            fs.Close();
            InitializeFontCharacter();
            SetFontTableImage();
            dialoguesEditor.RedrawText();
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            FileStream fs;
            BinaryWriter bw;
            try
            {
                // Create the file to store the level data
                switch (FontType)
                {
                    case FontType.Menu:
                        saveFileDialog.FileName = "fontMenuGraphic.bin";
                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;
                        fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        foreach (FontCharacter f in fontMenu)
                            bw.Write(f.Graphics, 0, 0x18);
                        break;
                    case FontType.Dialogue:
                        saveFileDialog.FileName = "fontDialogueGraphic.bin";
                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;
                        fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        foreach (FontCharacter f in fontDialogue)
                            bw.Write(f.Graphics, 0, 0x30);
                        break;
                    case FontType.Description:
                        saveFileDialog.FileName = "fontDescriptionsGraphic.bin";
                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;
                        fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        foreach (FontCharacter f in fontDescription)
                            bw.Write(f.Graphics, 0, 0x10);
                        break;
                    case FontType.BattleMenu:
                        saveFileDialog.FileName = "fontBattleMenuGraphic.bin";
                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;
                        fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        foreach (FontCharacter f in fontBattleMenu)
                            bw.Write(f.Graphics, 0, 0x20);
                        break;
                    default:
                        saveFileDialog.FileName = "fontTrianglesGraphic.bin";
                        if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                            return;
                        fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        foreach (FontCharacter f in fontTriangle)
                            bw.Write(f.Graphics, 0, 0x20);
                        break;
                }
                bw.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("There was a problem exporting the font graphics.");
            }
        }
        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (FontType)
            {
                case FontType.Menu: Do.Export(fontTableImage, "fontTableMenu.png"); break;
                case FontType.Dialogue: Do.Export(fontTableImage, "fontTableDialogue.png"); break;
                case FontType.Description: Do.Export(fontTableImage, "fontTableDescription.png"); break;
                case FontType.Triangles: Do.Export(fontTableImage, "fontTableTriangles.png"); break;
                case FontType.BattleMenu: Do.Export(fontTableImage, "fontTableBattleMenu.png"); break;
            }
        }
        private void insertIntoTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (keystrokes == null)
                return;
            if (keystrokes[overFontChar] == "")
                dialoguesEditor.InsertIntoDialogueText("[" + overFontChar + "]");
            else
                dialoguesEditor.InsertIntoDialogueText(keystrokes[overFontChar]);
        }
        private void insertIntoBattleDialogueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (keystrokes == null)
                return;
            if (keystrokes[overFontChar] == "")
                battleDialogues.InsertIntoBattleDialogueText("[" + overFontChar + "]");
            else
                battleDialogues.InsertIntoBattleDialogueText(keystrokes[overFontChar]);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            insertIntoTextToolStripMenuItem.Enabled = FontType < FontType.Triangles;
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        #endregion
    }
}
