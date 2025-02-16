using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class About : NewForm
    {
        private Editor form1;
        // constructor
        public About(Editor form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }
        // event handlers
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}