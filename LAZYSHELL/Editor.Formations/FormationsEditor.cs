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
    public partial class FormationsEditor : NewForm
    {
            //
        private Formations formationsEditor;
        private FormationPacks packsEditor;
        private Settings settings = Settings.Default;
        public int FormationIndex { get { return formationsEditor.Index; } set { formationsEditor.Index = value; } }
        public int PackIndex { get { return packsEditor.Index; } set { packsEditor.Index = value; } }
        // constructor
        public FormationsEditor()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip3, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip3, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip3, Keys.F2, baseConvertor);
            // create editors
            formationsEditor = new Formations();
            packsEditor = new FormationPacks(formationsEditor);
            packsEditor.TopLevel = false;
            packsEditor.Dock = DockStyle.Top;
            panel1.Controls.Add(packsEditor);
            packsEditor.Visible = true;
            formationsEditor.TopLevel = false;
            formationsEditor.Dock = DockStyle.Top;
            panel1.Controls.Add(formationsEditor);
            formationsEditor.Visible = true;
            new ToolTipLabel(this, baseConvertor, helpTips);
            //
            if (settings.RememberLastIndex)
            {
                packsEditor.Index = Settings.Default.LastFormationPack;
                formationsEditor.Index = Settings.Default.LastFormation;
            }
            this.History = new History(this, false);
        }
        // functions
        public void Assemble()
        {
            foreach (Formation f in Model.Formations)
                f.Assemble();
            foreach (FormationPack fp in Model.FormationPacks)
                fp.Assemble();
            for (int i = 0; i < Model.FormationMusics.Length; i++)
                Model.ROM[0x029F51 + i] = Model.FormationMusics[i];
            formationsEditor.Modified = false;
            packsEditor.Modified = false;
            this.Modified = false;
        }
        // event handlers
        private void FormationsEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !formationsEditor.Modified && ! packsEditor.Modified)
                goto Close;
            DialogResult result = MessageBox.Show(
                "Formations have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Formations = null;
                Model.FormationMusics = null;
                Model.FormationPacks = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            formationsEditor.searchWindow.Close();
            packsEditor.searchWindow.Close();
        }
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void importFormationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Formations, formationsEditor.Index, "IMPORT FORMATIONS...").ShowDialog();
            formationsEditor.RefreshFormations();
        }
        private void importPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.FormationPacks, packsEditor.Index, "IMPORT PACKS...").ShowDialog();
            packsEditor.RefreshFormationPacks();
        }
        private void exportFormationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Formations, formationsEditor.Index, "EXPORT FORMATIONS...").ShowDialog();
        }
        private void exportPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.FormationPacks, packsEditor.Index, "EXPORT PACKS...").ShowDialog();
        }
        private void clearFormationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.Formations, formationsEditor.Index, "CLEAR FORMATIONS...").ShowDialog();
            formationsEditor.RefreshFormations();
        }
        private void clearPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.FormationPacks, packsEditor.Index, "CLEAR PACKS...").ShowDialog();
            packsEditor.RefreshFormationPacks();
        }
        private void resetFormationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current formation. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            formationsEditor.Formation = new Formation(formationsEditor.Index);
            formationsEditor.RefreshFormations();
        }
        private void resetPackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current pack. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            packsEditor.Pack = new FormationPack(packsEditor.Index);
            packsEditor.RefreshFormationPacks();
        }
        private void showFormations_Click(object sender, EventArgs e)
        {
            formationsEditor.Visible = showFormations.Checked;
        }
        private void showPacks_Click(object sender, EventArgs e)
        {
            packsEditor.Visible = showPacks.Checked;
        }
    }
}
