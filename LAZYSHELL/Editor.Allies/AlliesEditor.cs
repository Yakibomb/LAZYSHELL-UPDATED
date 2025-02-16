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

namespace LAZYSHELL
{
    public partial class AlliesEditor : NewForm
    {
        // variables
        private Settings settings = Settings.Default;
        public Allies alliesEditor;
        private LevelUps levelUpsEditor;
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
            levelUpsEditor = new LevelUps();
            levelUpsEditor.TopLevel = false;
            levelUpsEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(levelUpsEditor);
            levelUpsEditor.Visible = true;
            alliesEditor = new Allies();
            alliesEditor.TopLevel = false;
            alliesEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(alliesEditor);
            alliesEditor.Visible = true;
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
            levelUpsEditor.Modified = false;
            alliesEditor.Modified = false;
            this.Modified = false;
        }
        // event handlers
        private void AlliesEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !alliesEditor.Modified && !levelUpsEditor.Modified)
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
            new IOElements((Element[])Model.Characters, alliesEditor.Index, "IMPORT CHARACTERS...").ShowDialog();
            alliesEditor.RefreshCharacter();
            levelUpsEditor.RefreshLevel();
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Characters, alliesEditor.Index, "EXPORT CHARACTERS...").ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.Characters, alliesEditor.Index, "CLEAR CHARACTERS...").ShowDialog();
            alliesEditor.RefreshCharacter();
            levelUpsEditor.RefreshLevel();
        }
        private void showNewGameStats_Click(object sender, EventArgs e)
        {
            alliesEditor.Visible = showNewGameStats.Checked;
        }
        private void showLevelUps_Click(object sender, EventArgs e)
        {
            levelUpsEditor.Visible = showLevelUps.Checked;
        }
    }
}
