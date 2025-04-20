using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using static LAZYSHELL.Character;
using static LAZYSHELL.NotesDB;

namespace LAZYSHELL
{
    public partial class AlliesEditor : NewForm
    {
        // variables
        private Settings settings = Settings.Default;
        public Characters charactersEditor;
        public LevelUps levelUpsEditor;
        public Coordinates coordinatesEditor;
        public NewGames newGamesEditor;
        public Character[] Characters { get { return Model.Characters; } set { Model.Characters = value; } }
        public Character Character { get { return Characters[index]; } set { Characters[index] = value; } }
        private int index = 0;
        public int Index { get { return index; } set { index = value; } }
        //
        private Bitmap[] allyImages;
        // constructor
        public AlliesEditor()
        {
            //
            InitializeComponent();
            Do.AddShortcut(toolStrip3, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip3, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip3, Keys.F2, baseConvertor);
            this.toolTip1.InitialDelay = 0;
            // create editors
            coordinatesEditor = new Coordinates(this);
            coordinatesEditor.TopLevel = false;
            coordinatesEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(coordinatesEditor);
            coordinatesEditor.Visible = true;

            levelUpsEditor = new LevelUps(this);
            levelUpsEditor.TopLevel = false;
            levelUpsEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(levelUpsEditor);
            levelUpsEditor.Visible = true;

            charactersEditor = new Characters(this);
            charactersEditor.TopLevel = false;
            charactersEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(charactersEditor);
            charactersEditor.Visible = true;

            newGamesEditor = new NewGames(this);
            newGamesEditor.TopLevel = false;
            newGamesEditor.Dock = DockStyle.Bottom;
            charactersEditor.Controls.Add(newGamesEditor);  //docking to characters editor for now
            newGamesEditor.Visible = true;
            //
            //
            SetAllyImages();
            RefreshCharacter(0);
            RefreshAllyToolTipText();
            new ToolTipLabel(this, baseConvertor, helpTips);
            this.History = new History(this, false);
        }
        // functions
        public void Assemble()
        {
            foreach (Character c in Model.Characters)
                c.Assemble();
            foreach (Slot s in Model.Slots)
                s.Assemble();
            Model.NewGame.Assemble();
            newGamesEditor.Modified = false;
            levelUpsEditor.Modified = false;
            charactersEditor.Modified = false;
            coordinatesEditor.Modified = false;
            this.Modified = false;
        }
        private void SetAllyImages()
        {
            allyImages = new Bitmap[5];
            for (int i = 0; i < allyImages.Length; i++)
            {
                Size size = new Size(0, 0);
                int index = Model.ROM[0x0318A3 + i];
                Sprite sprite = Model.Sprites[index];
                Animation animation = Model.Animations[sprite.AnimationPacket];
                Sequence sequence = null;
                int sequenceIndex = Model.ROM[0x031881];
                if (sequenceIndex < animation.Sequences.Count)
                    sequence = animation.Sequences[sequenceIndex];
                int moldIndex = 0;
                if (sequence != null && sequence.Frames.Count >= 0)
                    moldIndex = sequence.Frames[0].Mold;
                int[] pixels = sprite.GetPixels(true, false, moldIndex, 0, false, true, ref size);
                allyImages[i] = Do.PixelsToImage(pixels, size.Width, size.Height);
            }

            ally1.Image = allyImages[0];
            ally2.Image = allyImages[1];
            ally3.Image = allyImages[2];
            ally4.Image = allyImages[3];
            ally5.Image = allyImages[4];
        }

        public void RefreshAllyToolTipText()
        {
            ally1.ToolTipText = Model.Characters[0].ToString().Trim();
            ally2.ToolTipText = Model.Characters[1].ToString().Trim();
            ally3.ToolTipText = Model.Characters[2].ToString().Trim();
            ally4.ToolTipText = Model.Characters[3].ToString().Trim();
            ally5.ToolTipText = Model.Characters[4].ToString().Trim();
        }

        // event handlers
        private void AlliesEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !charactersEditor.Modified && !levelUpsEditor.Modified && !coordinatesEditor.Modified)
                return;
            //
            DialogResult result = MessageBox.Show(
                "Allies have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Characters = null;
                Model.Slots = null;
                Model.NewGame = null;
                return;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void import_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Characters, charactersEditor.Index, "IMPORT CHARACTERS...").ShowDialog();
            charactersEditor.RefreshCharacter();
            levelUpsEditor.RefreshLevel();
            coordinatesEditor.RefreshCharacter();
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Characters, charactersEditor.Index, "EXPORT CHARACTERS...").ShowDialog();
        }
        private void showNewGameStats_Click(object sender, EventArgs e)
        {
            charactersEditor.Visible = showNewGameStats.Checked;
        }
        private void showLevelUps_Click(object sender, EventArgs e)
        {
            levelUpsEditor.Visible = showLevelUps.Checked;
        }
        private void showABXYCursor_Click(object sender, EventArgs e)
        {
            coordinatesEditor.Visible = showABYXCursor.Checked;
        }

        public void RefreshCharacter(int characterIndex)
        {
            this.index = characterIndex;
            charactersEditor.RefreshCharacter();
            levelUpsEditor.RefreshLevel();
            coordinatesEditor.RefreshCharacter();
            newGamesEditor.RefreshSlots();
            //
            ToolStripButton[] buttons = new ToolStripButton[5] { ally1, ally2, ally3, ally4, ally5 };
            foreach (ToolStripButton b in buttons)
                b.Checked = false;

            buttons[characterIndex].Checked = true;
            //
        }

        private void ally1_Click(object sender, EventArgs e)
        {
            RefreshCharacter(0);
        }
        private void ally2_Click(object sender, EventArgs e)
        {
            RefreshCharacter(1);
        }
        private void ally3_Click(object sender, EventArgs e)
        {
            RefreshCharacter(2);
        }
        private void ally4_Click(object sender, EventArgs e)
        {
            RefreshCharacter(3);
        }
        private void ally5_Click(object sender, EventArgs e)
        {
            RefreshCharacter(4);
        }

        private void resetNewGameStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's new game stats.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Character.Disassemble(index, true, false, false);
            charactersEditor.RefreshCharacter();
        }

        private void resetCurrentLevelupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's level-up index.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Character.Disassemble(index, false, true, false);
            levelUpsEditor.RefreshLevel();
        }

        private void resetCurrentCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's coordinates data.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Character.Disassemble(index, false, false, true);
            coordinatesEditor.RefreshCharacter();
        }

        private void resetAllCharacterNewGameStatusesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to ALL character's new game stats.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            for (int i = 0; i < Characters.Length; i++ )
                Character.Disassemble(i, true, false, false);
            charactersEditor.RefreshCharacter();
        }

        private void resetAllCharacterLevelupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's level-up index.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            for (int i = 0; i < Characters.Length; i++ )
                Character.Disassemble(i, false, true, false);
            levelUpsEditor.RefreshLevel();
        }

        private void resetAllCharacterCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current character's coordinates data.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            for (int i = 0; i < Characters.Length; i++ )
                Character.Disassemble(i, false, false, true);
            coordinatesEditor.RefreshCharacter();
        }

        private void clearCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.Characters, index, "CLEAR CHARACTERS...").ShowDialog();
            charactersEditor.RefreshCharacter();
            levelUpsEditor.RefreshLevel();
            coordinatesEditor.RefreshCharacter();
        }
        private void clearBaseNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to clear the New Game stats data.\n\nGo ahead with clear?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.NewGame.Clear();
            newGamesEditor.RefreshSlots();
        }

        private void resetBaseNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the new game status.\n\nGo ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Model.NewGame = null; 
            newGamesEditor.RefreshSlots();

        }
    }
}
