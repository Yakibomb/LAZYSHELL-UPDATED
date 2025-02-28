using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;
using static LAZYSHELL.NotesDB;

namespace LAZYSHELL
{
    public partial class UpdatePointer : NewForm
    {
        private object element;
        private int currentIndex;
        private int start = 0;
        private int end = 0;
        private int index { get { return (int)pointerUpdateNum.Value; } }
        // constructor
        public UpdatePointer(object element, int currentIndex)
        {
            this.element = element;
            this.currentIndex = currentIndex;
            this.start = this.end = currentIndex;
            InitializeComponent();
            if (element == Model.ActionScripts)
                scriptChoices.SelectedIndex = 1;
            else
                scriptChoices.SelectedIndex = 0;

            RefreshSelectedScript();

            toIndex.Value = ((object[])element).Length - 1;
            start = end = currentIndex;
        }
        private void RefreshSelectedScript()
        {
            if (scriptChoices.SelectedIndex == 1)
                element = Model.ActionScripts;
            else
                element = Model.EventScripts;

            toIndex.Maximum = fromIndex.Maximum = ((object[])element).Length - 1;
            fromIndex.Value = fromIndex.Value < fromIndex.Maximum ? fromIndex.Value : fromIndex.Maximum;
            toIndex.Value = toIndex.Value < toIndex.Maximum ? toIndex.Value : toIndex.Maximum;
        }

        // event handlers
        private void scriptChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedScript();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                fromIndex.Enabled = false;
                toIndex.Enabled = false;
                start = end = currentIndex;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                fromIndex.Enabled = true;
                toIndex.Enabled = true;
                start = (int)fromIndex.Value;
                end = (int)toIndex.Value;
            }
        }
        private void fromDialogue_ValueChanged(object sender, EventArgs e)
        {
            toIndex.Minimum = fromIndex.Value;
            start = (int)fromIndex.Value;
        }
        private void toDialogue_ValueChanged(object sender, EventArgs e)
        {
            fromIndex.Maximum = toIndex.Value;
            end = (int)toIndex.Value;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (element == Model.EventScripts)
            {
                int conditionOffset = 0;
                conditionOffset = Model.EventScripts[start].BaseOffset;

                for (int i = start; i <= end; i++)
                    Model.EventScripts[i].UpdateAllOffsets(index, conditionOffset, false);
            }
            else if (element == Model.ActionScripts)
            {
                int conditionOffset = 0;
                conditionOffset = Model.ActionScripts[start].BaseOffset;

                for (int i = start; i <= end; i++)
                    Model.ActionScripts[i].UpdateOffsets(index, conditionOffset, false);
            }
            this.Tag = new Point(start, end);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
