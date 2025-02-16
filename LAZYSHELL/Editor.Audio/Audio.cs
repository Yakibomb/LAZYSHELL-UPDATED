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
    public partial class Audio : NewForm
    {
        private SampleEditor sampleEditor;
        private SPCEditor spcEditor;
        //
        public Audio()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip1, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip1, Keys.F1, helpTips);
            // create editors
            spcEditor = new SPCEditor();
            spcEditor.TopLevel = false;
            spcEditor.Dock = DockStyle.Fill;
            //spcEditor.SetToolTips(toolTip1);
            panel1.Controls.Add(spcEditor);
            spcEditor.Visible = true;
            sampleEditor = new SampleEditor();
            sampleEditor.TopLevel = false;
            sampleEditor.Dock = DockStyle.Top;
            sampleEditor.Height = 148;
            //sampleEditor.SetToolTips(toolTip1);
            panel1.Controls.Add(sampleEditor);
            sampleEditor.Visible = true;
            new ToolTipLabel(this, null, helpTips);
            if (Settings.Default.RememberLastIndex)
            {
                sampleEditor.Index = Settings.Default.LastAudioSample;
                spcEditor.Index = Settings.Default.LastSPC;
            }
            this.History = new History(this, false);
        }
        public void Assemble(bool warning)
        {
            sampleEditor.Assemble();
            spcEditor.Assemble(warning);
            sampleEditor.Modified = false;
            spcEditor.Modified = false;
            Model.HexEditor.Compare();
        }
        //
        private void Audio_FormClosing(object sender, FormClosingEventArgs e)
        {
            sampleEditor.SoundPlayer.Stop();
            if (!this.Modified && !sampleEditor.Modified && !spcEditor.Modified)
                return;
            sampleEditor.SoundPlayer.Stop();
            DialogResult result = MessageBox.Show(
              "Audio has not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                save.PerformClick();
            else if (result == DialogResult.No)
            {
                Model.AudioSamples = null;
                Model.SPCs = null;
                Model.SPCBattle = null;
                Model.SPCEvent = null;
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
            Assemble(true);
        }
        private void toggleSamples_Click(object sender, EventArgs e)
        {
            sampleEditor.Visible = toggleSamples.Checked;
        }
        private void toggleSPCs_Click(object sender, EventArgs e)
        {
            spcEditor.Visible = toggleSPCs.Checked;
        }
    }
}
