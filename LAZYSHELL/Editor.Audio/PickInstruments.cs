using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class PickInstruments : NewForm
    {
        private List<int> foundInstruments;
        private List<int> newInstruments;
        private List<ComboBox> instruments;
        public PickInstruments(List<int> foundInstruments, List<int> newInstruments)
        {
            this.foundInstruments = foundInstruments;
            this.newInstruments = newInstruments;
            this.instruments = new List<ComboBox>();
            InitializeComponent();
            setAllInstrument.Items.AddRange(Lists.Numerize(Lists.SampleNames));
            setAllInstrument.SelectedIndex = 87; // Acoustic Piano
            templateGame.SelectedIndex = 0;
            for (int i = 0; i < foundInstruments.Count; i++)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.Location = new Point(0, i * 21 + 10);
                label.Text = "Index " + foundInstruments[i];
                panel1.Controls.Add(label);
                //
                ComboBox instrument = new ComboBox();
                instrument.DropDownStyle = ComboBoxStyle.DropDownList;
                instrument.DropDownWidth = 200;
                instrument.FormattingEnabled = true;
                instrument.Items.AddRange(Lists.Numerize(Lists.SampleNames));
                instrument.Location = new Point(64, i * 21 + 6);
                instrument.Name = "sampleIndex" + i;
                instrument.SelectedIndex = foundInstruments[i];
                instrument.Size = new Size(153, 21);
                instrument.TabIndex = i;
                instrument.Tag = i;
                instrument.SelectedIndexChanged += new EventHandler(instrument_SelectedIndexChanged);
                instruments.Add(instrument);
                panel1.Controls.Add(instrument);
            }
        }
        private void instrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)((ComboBox)sender).Tag;
            newInstruments[index] = instruments[index].SelectedIndex;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void setAllTo_Click(object sender, EventArgs e)
        {
            foreach (ComboBox instrument in instruments)
                instrument.SelectedIndex = setAllInstrument.SelectedIndex;
        }
        private void template_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < instruments.Count; i++)
                instruments[i].SelectedIndex = Lists.SMWSamples[foundInstruments[i]];
        }
    }
}
