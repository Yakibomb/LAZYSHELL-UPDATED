using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class CoordAdjust : NewForm
    {
        public Point Point;
        public bool ApplyToAll;
        public CoordAdjust()
        {
            InitializeComponent();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Point = new Point((int)coordX.Value, (int)coordY.Value);
            this.ApplyToAll = applyToAll.Checked;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void CoordAdjust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                buttonOK.PerformClick();
            else if (e.KeyData == Keys.Escape)
                buttonCancel.PerformClick();
        }
    }
}
