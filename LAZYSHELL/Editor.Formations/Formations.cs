using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class Formations : NewForm
    {
        #region Variables
        private bool waitBothCoords = false;
        private int mouseOverMonster = -1;
        private int mouseDownMonster = -1;
        private string mouseOverObject;
        private string mouseDownObject;
        private Point mouseDownPosition = new Point(-1, -1);
        private Point mousePosition;
        private bool mouseWithinSameBounds = false;
        private bool move = false;
        private List<SelectedObject> selectedObjects;
        private Overlay overlay;
        private int diffX, diffY;
        private Bitmap formationBGImage;
        private Battlefield[] battlefields { get { return Model.Battlefields; } }
        private PaletteSet[] paletteSets { get { return Model.PaletteSetsBF; } }
        private Formation[] formations { get { return Model.Formations; } set { Model.Formations = value; } }
        private Formation formation { get { return formations[Index]; } set { formations[Index] = value; } }
        public Formation Formation { get { return formation; } set { formation = value; } }
        private SortedList monsterNames { get { return Model.MonsterNames; } set { Model.MonsterNames = value; } }
        private FontCharacter[] fontMenu { get { return Model.FontMenu; } }
        private int[] palette { get { return Model.FontPaletteBattle.Palette; } }
        private Monster[] monsters { get { return Model.Monsters; } }
        public int Index { get { return (int)formationNum.Value; } set { formationNum.Value = value; } }
        public ToolStripTextBox FormationName { get { return searchBox; } }
        public System.Windows.Forms.ToolStripComboBox FormationNames { get { return formationNameList; } }
        public Search searchWindow;
        private EditLabel labelWindow;
        private List<Bitmap> monsterImages = new List<Bitmap>();
        private List<Bitmap> shadowImages = new List<Bitmap>();
        private Bitmap[] allyImages;
        private Bitmap[] statImages;
        private Bitmap[] portraits;
        private CheckBox[] use = new CheckBox[8];
        private CheckBox[] hide = new CheckBox[8];
        private NumericUpDown[] bytes = new NumericUpDown[8];
        private ComboBox[] names = new ComboBox[8];
        private NumericUpDown[] coordX = new NumericUpDown[8];
        private NumericUpDown[] coordY = new NumericUpDown[8];
        private CommandStack commandStack = new CommandStack();
        private byte[] originalX;
        private byte[] originalY;
        private int selectedMonster = -1;
        #endregion
        // Constructor
        public Formations()
        {
            this.overlay = new Overlay();
            Model.MonsterNames = new SortedList(monsters);
            Model.MonsterNames.SortAlphabetically();
            InitializeComponent();
            SetControls();
            searchWindow = new Search(formationNum, searchBox, searchFormationNames, formationNameList.Items);
            labelWindow = new EditLabel(formationNameList, formationNum, "Formations", false);
            InitializeStrings();
            this.formationNameList.SelectedIndex = 0;
            battlefieldName.SelectedIndex = 7;
            RefreshFormations();
            //
            this.History = new History(this, formationNameList, formationNum);
        }
        // functions
        private void SetControls()
        {
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = new NumericUpDown();
                bytes[i].Location = new Point(6, i * 21 + 20);
                bytes[i].Maximum = new decimal(new int[] { 255, 0, 0, 0 });
                bytes[i].Name = "bytes";
                bytes[i].Size = new Size(44, 21);
                bytes[i].TabIndex = 0;
                bytes[i].Tag = i;
                bytes[i].TextAlign = HorizontalAlignment.Right;
                bytes[i].Enter += new EventHandler(monster_Enter);
                bytes[i].ValueChanged += new System.EventHandler(bytes_ValueChanged);
                groupBox1.Controls.Add(bytes[i]);
                //
                names[i] = new ComboBox();
                names[i].BackColor = SystemColors.ControlDarkDark;
                names[i].DrawMode = DrawMode.OwnerDrawFixed;
                names[i].DropDownHeight = 317;
                names[i].DropDownStyle = ComboBoxStyle.DropDownList;
                names[i].DropDownWidth = 142;
                names[i].IntegralHeight = false;
                names[i].ItemHeight = 15;
                names[i].Location = new Point(50, i * 21 + 20);
                names[i].Name = "names";
                names[i].Size = new Size(118, 21);
                names[i].Tag = i;
                names[i].DrawItem += new DrawItemEventHandler(this.monsterName_DrawItem);
                names[i].Enter += new EventHandler(monster_Enter);
                names[i].SelectedIndexChanged += new EventHandler(names_SelectedIndexChanged);
                groupBox1.Controls.Add(names[i]);
                //
                coordX[i] = new NumericUpDown();
                coordX[i].Location = new Point(168, i * 21 + 20);
                coordX[i].Maximum = new decimal(new int[] { 255, 0, 0, 0 });
                coordX[i].Name = "coordX";
                coordX[i].Size = new Size(44, 21);
                coordX[i].Tag = i;
                coordX[i].TextAlign = HorizontalAlignment.Right;
                coordX[i].Enter += new EventHandler(monster_Enter);
                coordX[i].ValueChanged += new EventHandler(coordX_ValueChanged);
                groupBox1.Controls.Add(coordX[i]);
                //
                coordY[i] = new NumericUpDown();
                coordY[i].Location = new Point(214, i * 21 + 20);
                coordY[i].Maximum = new decimal(new int[] { 255, 0, 0, 0 });
                coordY[i].Name = "coordY";
                coordY[i].Size = new Size(44, 21);
                coordY[i].Tag = i;
                coordY[i].TextAlign = HorizontalAlignment.Right;
                coordY[i].Enter += new EventHandler(monster_Enter);
                coordY[i].ValueChanged += new EventHandler(coordY_ValueChanged);
                groupBox1.Controls.Add(coordY[i]);
                //
                use[i] = new CheckBox();
                use[i].AutoSize = true;
                use[i].Location = new Point(261, i * 21 + 23);
                use[i].Name = "use";
                use[i].Text = "Use";
                use[i].Tag = i;
                use[i].Enter += new EventHandler(monster_Enter);
                use[i].CheckedChanged += new EventHandler(use_CheckedChanged);
                groupBox1.Controls.Add(use[i]);
                //
                hide[i] = new CheckBox();
                hide[i].AutoSize = true;
                hide[i].Location = new Point(307, i * 21 + 23);
                hide[i].Name = "hide";
                hide[i].Text = "Hide";
                hide[i].Tag = i;
                hide[i].Enter += new EventHandler(monster_Enter);
                hide[i].CheckedChanged += new EventHandler(hide_CheckedChanged);
                groupBox1.Controls.Add(hide[i]);
            }
        }
        //
        private void InitializeStrings()
        {
            this.Updating = true;
            this.formationNameList.Items.Clear();
            this.formationNameList.Items.AddRange(Lists.Numerize(formations));
            this.formationNameList.SelectedIndex = Index;
            foreach (ComboBox name in names)
                name.Items.AddRange(Model.MonsterNames.Names);
            this.battlefieldName.Items.AddRange(Lists.BattlefieldNames);
            this.formationBattleEvent.Items.AddRange(Lists.Numerize(Lists.BattleEventNames));
            this.musicTrack.Items.AddRange(Lists.MusicNames);
            this.Updating = false;
        }
        public void RefreshFormations()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.Updating)
                return;
            this.Updating = true;
            this.formationNameList.SelectedIndex = Index;
            for (int i = 0; i < 8; i++)
            {
                bytes[i].Value = formation.Monsters[i];
                names[i].SelectedIndex = monsterNames.GetSortedIndex(formation.Monsters[i]);
                coordX[i].Value = formation.X[i];
                coordY[i].Value = formation.Y[i];
            }
            this.formationMusic.SelectedIndex = formation.Music;
            this.formationBattleEvent.SelectedIndex = formation.BattleEvent;
            this.formationUnknown.Value = formation.Unknown;
            this.formationCantRun.Checked = formation.CantRun;
            for (int i = 0; i < 8; i++)
            {
                this.use[i].Checked = formation.Use[i];
                this.hide[i].Checked = formation.Hide[i];
            }
            this.musicTrack.Enabled = formationMusic.SelectedIndex != 8;
            if (formationMusic.SelectedIndex != 8)
                this.musicTrack.SelectedIndex = Model.FormationMusics[formationMusic.SelectedIndex];
            else
                this.musicTrack.SelectedIndex = 0;
            RefreshMonsterImages();
            pictureBoxFormation.Invalidate();
            this.Updating = false;
            Cursor.Current = Cursors.Arrow;
        }
        private void RefreshMonsterImages()
        {
            monsterImages = new List<Bitmap>();
            shadowImages = new List<Bitmap>();
            for (int i = 0; i < 8; i++)
            {
                int[] pixels = Model.Monsters[formation.Monsters[i]].Pixels;
                monsterImages.Add(Do.PixelsToImage(pixels, 256, 256));
                pixels = Model.Monsters[formation.Monsters[i]].Shadow;
                shadowImages.Add(Do.PixelsToImage(pixels, 16, 16));
            }
            formation.PixelIndexes = null;
            pictureBoxFormation.Invalidate();
        }
        private void RefreshFormationBattlefield()
        {
            PaletteSet paletteSet = paletteSets[battlefields[battlefieldName.SelectedIndex].PaletteSet];
            BattlefieldTileset tileSet = new BattlefieldTileset(battlefields[battlefieldName.SelectedIndex], paletteSet);
            int[] quadrant1 = Do.TilesetToPixels(tileSet.Tileset_tiles, 16, 16, 0, false);
            int[] quadrant2 = Do.TilesetToPixels(tileSet.Tileset_tiles, 16, 16, 256, false);
            int[] quadrant3 = Do.TilesetToPixels(tileSet.Tileset_tiles, 16, 16, 512, false);
            int[] quadrant4 = Do.TilesetToPixels(tileSet.Tileset_tiles, 16, 16, 768, false);
            int[] pixels = new int[512 * 512];
            Do.PixelsToPixels(quadrant1, pixels, 512, new Rectangle(0, 0, 256, 256));
            Do.PixelsToPixels(quadrant2, pixels, 512, new Rectangle(256, 0, 256, 256));
            Do.PixelsToPixels(quadrant3, pixels, 512, new Rectangle(0, 256, 256, 256));
            Do.PixelsToPixels(quadrant4, pixels, 512, new Rectangle(256, 256, 256, 256));
            formationBGImage = Do.PixelsToImage(pixels, 512, 512);
            pictureBoxFormation.Invalidate();
        }
        private void SetAllyImages()
        {
            allyImages = new Bitmap[5];
            statImages = new Bitmap[5];
            portraits = new Bitmap[5];
            for (int i = 0; i < allyImages.Length; i++)
            {
                Size size = new Size(0, 0);
                Sprite sprite = Model.Sprites[Model.NPCProperties[i].Sprite];
                int[] pixels = sprite.GetPixels(false, true, 0, 7, false, false, ref size);
                allyImages[i] = Do.PixelsToImage(pixels, size.Width, size.Height);
                //
                pixels = new int[128 * 24];
                int[] palette = Model.BattleMenuPalette.Palette;
                char[] HP = new char[] { '2', '0', '9' }; // Mario
                if (i == 1) HP = new char[] { '2', '1', '1' }; // Toadstool
                if (i == 2) HP = new char[] { '2', '4', '0' }; // Bowser
                if (i == 3) HP = new char[] { '1', '9', '5' }; // Mallow
                if (i == 4) HP = new char[] { '2', '0', '3' }; // Geno
                char[] text = new char[]
                {
                    '\x01','\x01','\x01','\x01','\x01','\x01','\x01','\x01','\x02','\n' ,
                    '\x00',HP[0],HP[1],HP[2],'\x16',HP[0],HP[1],HP[2],'\x10','\n',
                    '\x11','\x11','\x11','\x11','\x11','\x11','\x11','\x11','\x12'
                };
                Do.DrawText(pixels, 128, text, 0, 0, 8, Model.FontBattleMenu, palette);
                statImages[i] = Do.PixelsToImage(pixels, 128, 24);
                //
                palette = Model.Sprites[Model.NPCProperties[i].Sprite].Palette;
                pixels = Model.Sprites[i + 40].GetPixels(true, false, 0, 0, palette, true, false, ref size);
                portraits[i] = Do.PixelsToImage(pixels, 256, 256);
            }
        }
        private void SwitchMonster(int a, int b)
        {
            byte x = formation.X[a];
            byte y = formation.Y[a];
            byte monster = formation.Monsters[a];
            bool use = formation.Use[a];
            bool hide = formation.Hide[a];
            //
            this.coordX[a].Value = formation.X[b];
            this.coordY[a].Value = formation.Y[b];
            this.bytes[a].Value = formation.Monsters[b];
            this.use[a].Checked = formation.Use[b];
            this.hide[a].Checked = formation.Hide[b];
            //
            this.coordX[b].Value = x;
            this.coordY[b].Value = y;
            this.bytes[b].Value = monster;
            this.use[b].Checked = use;
            this.hide[b].Checked = hide;
            //
            this.formationNameList.Items[Index] = Lists.Numerize(formation.ToString(), Index, 3);
        }
        // drawing
        private void Drag()
        {
            if (overlay.Select.Empty)
                return;
            selectedObjects = new List<SelectedObject>();
            for (int y = overlay.Select.Y; y < overlay.Select.Terminal.Y; y++)
            {
                for (int x = overlay.Select.X; x < overlay.Select.Terminal.X; x++)
                {
                    if (y < 0 || y >= 256 ||
                        x < 0 || x >= 256)
                        continue;
                    int index = this.formation.PixelIndexes[y * 256 + x];
                    if (index != -1)
                        selectedObjects.Add(new SelectedObject(index,
                            (int)coordX[index].Value - mousePosition.X,
                            (int)coordY[index].Value - mousePosition.Y));
                }
            }
        }
        private void Undo()
        {
            this.Updating = true;
            commandStack.UndoCommand();
            for (int i = 0; i < 8; i++)
            {
                coordX[i].Value = formation.X[i];
                coordY[i].Value = formation.Y[i];
            }
            this.Updating = false;
            overlay.Select.Clear();
            selectedObjects = null;
            formation.PixelIndexes = null;
            pictureBoxFormation.Invalidate();
            Cursor.Position = Cursor.Position;
        }
        private void Redo()
        {
            this.Updating = true;
            commandStack.RedoCommand();
            for (int i = 0; i < 8; i++)
            {
                coordX[i].Value = formation.X[i];
                coordY[i].Value = formation.Y[i];
            }
            this.Updating = false;
            overlay.Select.Clear();
            selectedObjects = null;
            formation.PixelIndexes = null;
            pictureBoxFormation.Invalidate();
            Cursor.Position = Cursor.Position;
        }
        #region Event Handlers
        private void formationNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.formationNum.Value = this.formationNameList.SelectedIndex;
        }
        private void formationNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshFormations();
            Settings.Default.LastFormation = Index;
        }
        private void monster_Enter(object sender, EventArgs e)
        {
            selectedMonster = (int)((Control)sender).Tag;
        }
        private void moveUp_Click(object sender, EventArgs e)
        {
            if (selectedMonster < 0)
            {
                MessageBox.Show("Must select a monster property before moving.");
                return;
            }
            if (selectedMonster == 0)
                return;
            SwitchMonster(selectedMonster, selectedMonster - 1);
            selectedMonster--;
        }
        private void moveDown_Click(object sender, EventArgs e)
        {
            if (selectedMonster < 0)
            {
                MessageBox.Show("Must select a monster property before moving.");
                return;
            }
            if (selectedMonster == 7)
                return;
            SwitchMonster(selectedMonster, selectedMonster + 1);
            selectedMonster++;
        }
        private void bytes_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((NumericUpDown)sender).Tag;
            this.formation.Monsters[index] = (byte)bytes[index].Value;
            this.names[index].SelectedIndex = this.monsterNames.GetSortedIndex((byte)bytes[index].Value);
            this.formationNameList.Items[Index] = Lists.Numerize(formation.ToString(), Index, 3);
            RefreshMonsterImages();
        }
        private void names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((ComboBox)sender).Tag;
            this.bytes[index].Value = this.monsterNames.GetUnsortedIndex(this.names[index].SelectedIndex);
        }
        private void monsterName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(sender, e, new MenuTextPreview(), monsterNames, fontMenu, palette, true, Model.MenuBG_);
        }
        private void coordX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            //
            int index = (int)((NumericUpDown)sender).Tag;
            if (!move)
            {
                byte[] X = Bits.Copy(formation.X);
                X[index] = (byte)coordX[index].Value;
                commandStack.Push(new MoveEdit(formation, X, Bits.Copy(formation.Y)));
            }
            this.formation.X[index] = (byte)coordX[index].Value;
            //
            if (waitBothCoords)
                return;
            if (!move)
                formation.PixelIndexes = null;
            pictureBoxFormation.Invalidate();
        }
        private void coordY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            //
            int index = (int)((NumericUpDown)sender).Tag;
            if (!move)
            {
                byte[] Y = Bits.Copy(formation.Y);
                Y[index] = (byte)coordY[index].Value;
                commandStack.Push(new MoveEdit(formation, Bits.Copy(formation.X), Y));
            }
            this.formation.Y[index] = (byte)coordY[index].Value;
            //
            if (waitBothCoords)
                return;
            if (!move)
                formation.PixelIndexes = null;
            pictureBoxFormation.Invalidate();
        }
        //
        private void use_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((CheckBox)sender).Tag;
            this.formation.Use[index] = use[index].Checked;
            pictureBoxFormation.Invalidate();
        }
        private void hide_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((CheckBox)sender).Tag;
            this.formation.Hide[index] = hide[index].Checked;
            pictureBoxFormation.Invalidate();
        }
        private void formationMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            formation.Music = (byte)formationMusic.SelectedIndex;
            this.Updating = true;
            this.musicTrack.Enabled = formationMusic.SelectedIndex != 8;
            if (formationMusic.SelectedIndex != 8)
                this.musicTrack.SelectedIndex = Model.FormationMusics[formationMusic.SelectedIndex];
            else
                this.musicTrack.SelectedIndex = 0;
            this.Updating = false;
        }
        private void formationUnknown_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            formation.Unknown = (byte)formationUnknown.Value;
        }
        private void formationCantRun_CheckedChanged(object sender, EventArgs e)
        {
            formationCantRun.ForeColor = formationCantRun.Checked ? Color.Black : SystemColors.ControlDark;
            if (this.Updating)
                return;
            formation.CantRun = formationCantRun.Checked;
        }
        private void battlefieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshFormationBattlefield();
        }
        private void toggleAllies_Click(object sender, EventArgs e)
        {
            pictureBoxFormation.Invalidate();
        }
        private void isometricGrid_Click(object sender, EventArgs e)
        {
            pictureBoxFormation.Invalidate();
        }
        private void select_Click(object sender, EventArgs e)
        {
            pictureBoxFormation.Cursor = select.Checked ? Cursors.Cross : Cursors.Arrow;
            pictureBoxFormation.Invalidate();
        }
        private void undo_Click(object sender, EventArgs e)
        {
            Undo();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            Redo();
        }
        private void formationBattleEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            formation.BattleEvent = (byte)formationBattleEvent.SelectedIndex;
        }
        private void musicTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            Model.FormationMusics[formationMusic.SelectedIndex] = (byte)musicTrack.SelectedIndex;
        }
        //
        private void pictureBoxFormation_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownMonster = -1;
            mouseDownPosition = new Point(-1, -1);
            mouseDownObject = null;
            //
            int x = e.X; int y = e.Y;
            #region Selecting
            if (select.Checked)
            {
                // if we're not inside a current selection to move it, create a new selection
                if (mouseOverObject != "selection")
                {
                    selectedObjects = null;
                    overlay.Select.Refresh(1, x, y, 1, 1, pictureBoxFormation);
                }
                // otherwise, start dragging current selection
                else if (mouseOverObject == "selection")
                {
                    mouseDownObject = "selection";
                    mouseDownPosition = overlay.Select.MousePosition(x, y);
                    originalX = new byte[8];
                    originalY = new byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        originalX[i] = formation.X[i];
                        originalY[i] = formation.Y[i];
                    }
                    if (!move)    // only do this if the current selection has not been initially moved
                    {
                        move = true;
                        Drag();
                    }
                }
                return;
            }
            #endregion
            // dragging
            if (e.Button == MouseButtons.Left)
            {
                if (mouseOverMonster < 0 || mouseOverMonster > 7)
                    return;
                move = true;
                mouseDownMonster = mouseOverMonster;
                diffX = (int)(x - coordX[mouseDownMonster].Value);
                diffY = (int)(y - coordY[mouseDownMonster].Value);
                originalX = new byte[8];
                originalY = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    originalX[i] = formation.X[i];
                    originalY[i] = formation.Y[i];
                }
            }
        }
        private void pictureBoxFormation_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Math.Min(255, Math.Max(0, e.X));
            int y = Math.Min(255, Math.Max(0, e.Y));
            labelCoords.Text = "(x: " + x + ", y: " + y + ")";
            //
            mouseWithinSameBounds = mousePosition == new Point(x, y);
            mousePosition = new Point(x, y);
            mouseOverMonster = -1;
            mouseOverObject = null;
            pictureBoxFormation.Focus();
            //
            #region Selecting
            if (select.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && mouseDownObject == null && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x, y))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Min(x, 256),
                        Math.Min(y, 256));
                }
                // if dragging the current selection
                else if (e.Button == MouseButtons.Left && mouseDownObject == "selection" && !mouseWithinSameBounds)
                {
                    overlay.Select.Location = new Point(
                        x - mouseDownPosition.X,
                        y - mouseDownPosition.Y);
                    foreach (SelectedObject selectedObject in selectedObjects)
                    {
                        int index = selectedObject.Index;
                        coordX[index].Value = (byte)(x + selectedObject.DiffX);
                        coordY[index].Value = (byte)(y + selectedObject.DiffY);
                    }
                    pictureBoxFormation.Invalidate();
                    return;
                }
                // if mouse not clicked and within the current selection
                else if (e.Button == MouseButtons.None && overlay.Select != null && overlay.Select.MouseWithin(x, y))
                {
                    mouseOverObject = "selection";
                    pictureBoxFormation.Cursor = Cursors.SizeAll;
                }
                else
                    pictureBoxFormation.Cursor = Cursors.Cross;
                pictureBoxFormation.Invalidate();
                return;
            }
            #endregion
            #region Dragging
            if (e.Button == MouseButtons.Left)
            {
                x = e.X - diffX;
                y = e.Y - diffY;
                x = Math.Min(255, Math.Max(0, x));
                y = Math.Min(255, Math.Max(0, y));
                if (snapIsometricLeft.Checked && snapIsometricRight.Checked)
                {
                    x = x / 16 * 16;
                    if ((x / 2) - y < 0)
                    {
                        if (Math.Abs((x / 2) - y) % 16 >= 8)
                            y += 16 - (Math.Abs((x / 2) - y) % 16);
                        else
                            y -= Math.Abs((x / 2) - y) % 16;
                    }
                    else
                    {
                        if (Math.Abs((x / 2) - y) % 16 >= 8)
                            y -= 16 - (Math.Abs((x / 2) - y) % 16);
                        else
                            y += Math.Abs((x / 2) - y) % 16;
                    }
                }
                else if (snapIsometricLeft.Checked)
                {
                    x = x / 2 * 2;
                    if ((x / 2) - y < 0)
                    {
                        if (Math.Abs((x / 2) - y) % 16 >= 8)
                            y += 16 - (Math.Abs((x / 2) - y) % 16);
                        else
                            y -= Math.Abs((x / 2) - y) % 16;
                    }
                    else
                    {
                        if (Math.Abs((x / 2) - y) % 16 >= 8)
                            y -= 16 - (Math.Abs((x / 2) - y) % 16);
                        else
                            y += Math.Abs((x / 2) - y) % 16;
                    }
                }
                else if (snapIsometricRight.Checked)
                {
                    x = x / 2 * 2;
                    if (((1024 - x) / 2) - y < 0)
                    {
                        if (Math.Abs(((1024 - x) / 2) - y) % 16 >= 8)
                            y += 16 - (Math.Abs(((1024 - x) / 2) - y) % 16);
                        else
                            y -= Math.Abs(((1024 - x) / 2) - y) % 16;
                    }
                    else
                    {
                        if (Math.Abs(((1024 - x) / 2) - y) % 16 >= 8)
                            y -= 16 - (Math.Abs(((1024 - x) / 2) - y) % 16);
                        else
                            y += Math.Abs(((1024 - x) / 2) - y) % 16;
                    }
                }
                x = Math.Min(255, Math.Max(0, x));
                y = Math.Min(255, Math.Max(0, y));
                //
                if (mouseDownMonster >= 0 && mouseDownMonster <= 7)
                {
                    if (coordX[mouseDownMonster].Value != x &&
                        coordY[mouseDownMonster].Value != y)
                        waitBothCoords = true;
                    coordX[mouseDownMonster].Value = x;
                    waitBothCoords = false;
                    coordY[mouseDownMonster].Value = y;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (e.X > 0 && e.X < 256 && e.Y > 0 && e.Y < 256 &&
                        this.formation.PixelIndexes[e.Y * 256 + e.X] == i)
                    {
                        pictureBoxFormation.Cursor = Cursors.Hand;
                        mouseOverMonster = i;
                        break;
                    }
                    else
                    {
                        pictureBoxFormation.Cursor = Cursors.Arrow;
                        mouseOverMonster = -1;
                    }
                }
            }
            #endregion
        }
        private void pictureBoxFormation_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
            mouseDownPosition = new Point(-1, -1);
            mouseDownObject = null;
            formation.PixelIndexes = null;
            //
            if (originalX != null && originalY != null)
                commandStack.Push(new MoveEdit(formation, originalX, originalY));
            originalX = null;
            originalY = null;
            //
            pictureBoxFormation.Invalidate();
        }
        private void pictureBoxFormation_Paint(object sender, PaintEventArgs e)
        {
            if (formationBGImage != null)
                e.Graphics.DrawImage(formationBGImage, -8, 26);
            if (isometricGrid.Checked)
                new Overlay().DrawIsometricGrid(e.Graphics, Color.Gray, pictureBoxFormation.Size, new Size(16, 16), 1);
            byte[] items = new byte[8];
            for (byte i = 0; i < 8; i++)
                items[i] = i;
            byte[] keys = Bits.Copy(formation.Y);
            Array.Sort(keys, items);
            for (int a = 0; a < 8; a++)
            {
                int i = items[a];
                if (!formation.Use[i]) continue;
                int elevation = monsters[formation.Monsters[i]].Elevation * 16;
                int x = formation.X[i] - 8;
                int y = formation.Y[i] + 14;
                if (elevation > 0)
                    e.Graphics.DrawImage(shadowImages[i], x, y);
                //
                x = formation.X[i] - 128;
                y = formation.Y[i] - 96 - elevation - 1;
                e.Graphics.DrawImage(monsterImages[i], x, y);
            }
            if (toggleAllies.Checked)
            {
                if (allyImages == null || portraits == null)
                    SetAllyImages();
                e.Graphics.DrawImage(allyImages[0], Model.ROM[0x0296BD] - 128, Model.ROM[0x0296BE] - 96 - 1);
                e.Graphics.DrawImage(allyImages[2], Model.ROM[0x0296BF] - 128, Model.ROM[0x0296C0] - 96 - 1);
                e.Graphics.DrawImage(allyImages[3], Model.ROM[0x0296C1] - 128, Model.ROM[0x0296C2] - 96 - 1);
                // draw HPs
                e.Graphics.DrawImage(statImages[0], 24, 94);
                e.Graphics.DrawImage(statImages[2], 48, 70);
                e.Graphics.DrawImage(statImages[3], 72, 46);
                // draw portraits
                e.Graphics.DrawImage(portraits[0], 20 - 128, 82 - 96 - 1);
                e.Graphics.DrawImage(portraits[2], 44 - 128, 58 - 96 - 1);
                e.Graphics.DrawImage(portraits[3], 68 - 128, 34 - 96 - 1);
            }
            if (select.Checked && overlay.Select != null)
                overlay.Select.DrawSelectionBox(e.Graphics, 1);
        }
        private void pictureBoxFormation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.G: isometricGrid.PerformClick(); break;
                case Keys.S: select.PerformClick(); break;
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
            }
        }
        #endregion
        private class SelectedObject
        {
            /// <summary>
            ///  The X difference from where mouse was clicked
            /// </summary>
            public int DiffX;
            /// <summary>
            ///  The Y difference from where mouse was clicked
            /// </summary>
            public int DiffY;
            /// <summary>
            ///  The index of the selected object
            /// </summary>
            public int Index;
            public SelectedObject(int index, int diffX, int diffY)
            {
                this.Index = index;
                this.DiffX = diffX;
                this.DiffY = diffY;
            }
        }
        public class MoveEdit : Command
        {
            // class variables and accessors
            private Formation formation;
            private byte[] x = new byte[8];
            private byte[] y = new byte[8];
            private bool autoRedo = false;
            public bool AutoRedo() { return this.autoRedo; }
            // constructor
            public MoveEdit(Formation formation, byte[] x, byte[] y)
            {
                this.formation = formation;
                this.x = x;
                this.y = y;
            }
            // execute
            public void Execute()
            {
                for (int i = 0; i < 8; i++)
                {
                    byte tempX = formation.X[i];
                    byte tempY = formation.Y[i];
                    formation.X[i] = x[i];
                    formation.Y[i] = y[i];
                    x[i] = tempX;
                    y[i] = tempY;
                }
            }
        }
    }
}
