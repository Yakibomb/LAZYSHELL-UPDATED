using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class PickNativeSPC : NewForm
    {
        public PickNativeSPC()
        {
            InitializeComponent();
            this.Location = new Point(
                Cursor.Position.X - buttonOK.Location.X - 8,
                Cursor.Position.Y - buttonOK.Location.Y - 8 - 16);
            comboBox1.SelectedIndex = 0;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Tag = (NativeSPC)comboBox1.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
