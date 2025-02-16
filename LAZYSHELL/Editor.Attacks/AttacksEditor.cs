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
    public partial class AttacksEditor : NewForm
    {
        // variables
            //
        private Settings settings = Settings.Default;
        public Spells spellsEditor;
        public Attacks attacksEditor;
        // constructor
        public AttacksEditor()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip3, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip3, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip3, Keys.F2, baseConvertor);
            this.toolTip1.InitialDelay = 0;
            // create editors
            attacksEditor = new Attacks();
            attacksEditor.TopLevel = false;
            attacksEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(attacksEditor);
            attacksEditor.Visible = true;
            spellsEditor = new Spells();
            spellsEditor.TopLevel = false;
            spellsEditor.Dock = DockStyle.Left;
            panel1.Controls.Add(spellsEditor);
            spellsEditor.Visible = true;
            new ToolTipLabel(this, baseConvertor, helpTips);
            //
            if (settings.RememberLastIndex)
            {
                spellsEditor.Index = settings.LastSpell;
                attacksEditor.Index = settings.LastAttack;
            }
            this.History = new History(this, false);
        }
        // functions
        public void Assemble()
        {
            foreach (Attack a in Model.Attacks)
                a.Assemble();
            // Assemble the Model.Spells
            int i;
            int length = 0x2BB6; // offset to the start of spell descriptions
            for (i = 0; i < Model.Spells.Length && length + (Model.Spells[i].RawDescription != null ? Model.Spells[i].RawDescription.Length : 0) < (0x2bb6 + 0x36A); i++)
                 Model.Spells[i].Assemble(ref length);
            length = 0x55f0; // offset for extra space
            for (; i < Model.Spells.Length && length + (Model.Spells[i].RawDescription != null ? Model.Spells[i].RawDescription.Length : 0) < (0x55f0 + 0xa10); i++)
                 Model.Spells[i].Assemble(ref length);
            if (i != Model.Spells.Length)
                System.Windows.Forms.MessageBox.Show("Spell Descriptions total length exceeds max size, decrease total size to save correctly.\nNote: not all text has been saved.");
            attacksEditor.Modified = false;
            spellsEditor.Modified = false;
            this.Modified = false;
        }
        // event handlers
        private void importSpellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Spells, spellsEditor.Index, "IMPORT SPELLS...").ShowDialog();
            spellsEditor.RefreshSpells();
        }
        private void importAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Attacks, attacksEditor.Index, "IMPORT ATTACKS...").ShowDialog();
            attacksEditor.RefreshAttacks();
        }
        private void exportSpellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Spells, spellsEditor.Index, "EXPORT SPELLS...").ShowDialog();
        }
        private void exportAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.Attacks, attacksEditor.Index, "EXPORT ATTACKS...").ShowDialog();
        }
        private void clearSpellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.Spells, spellsEditor.Index, "CLEAR SPELLS...").ShowDialog();
            spellsEditor.RefreshSpells();
        }
        private void clearAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.Attacks, attacksEditor.Index, "CLEAR ATTACKS...").ShowDialog();
            attacksEditor.RefreshAttacks();
        }
        private void showSpells_Click(object sender, EventArgs e)
        {
            spellsEditor.Visible = showSpells.Checked;
        }
        private void showAttacks_Click(object sender, EventArgs e)
        {
            attacksEditor.Visible = showAttacks.Checked;
        }
        //
        private void AttacksEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !attacksEditor.Modified && !spellsEditor.Modified)
                return;
            DialogResult result = MessageBox.Show(
                "Attacks and spells have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Spells = null;
                Model.Attacks = null;
                Model.AttackNames = null;
                Model.SpellNames = null;
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
        private void damageCalculator_Click(object sender, EventArgs e)
        {
            StatusCalculator calculator = new StatusCalculator();
            calculator.Show();
        }
        private void resetSpellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current spell. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            spellsEditor.Spell = new Spell(spellsEditor.Index);
            spellsEditor.RefreshSpells();
        }
        private void resetAttackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current attack. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            attacksEditor.Attack = new Attack(attacksEditor.Index);
            attacksEditor.RefreshAttacks();
        }

    }
}
