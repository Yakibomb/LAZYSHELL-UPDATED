using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class NewExceptionForm : NewForm
    {
        public NewExceptionForm(Exception exception)
        {
            Model.Crashing = true;
            InitializeComponent();
            Bitmap icon = SystemIcons.Error.ToBitmap();
            pictureBox1.Size = icon.Size;
            pictureBox1.Image = icon;
            string forumthreadA = "http://acmlm.no-ip.org/board/thread.php?id=7005";
            string forumthreadB = "http://www.smwcentral.net/?p=viewthread&t=45572";
            label1.Text = "Lazy Shell has encountered an error. Please copy ALL of the contents of the box below and post them as a new reply to one of these threads:\n\n";
            label1.Links.Add(label1.Text.Length, forumthreadA.Length, forumthreadA);
            label1.Text += forumthreadA + "\n";
            label1.Links.Add(label1.Text.Length, forumthreadB.Length, forumthreadB);
            label1.Text += forumthreadB + "\n\n";
            label1.Text += "Also please briefly explain what you did to cause the error and which # and name of the element you were editing.";
            //
            Assembly assembly = Assembly.GetExecutingAssembly();
            richTextBox1.Text = assembly.ToString() + "\r\n\r\n";
            richTextBox1.Text += "**************Exception Text**************\r\n";
            richTextBox1.Text += exception.Message + "\r\n";
            StringReader reader = new StringReader(exception.StackTrace);
            string line;
            int number = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("   at LAZYSHELL"))
                    continue;
                richTextBox1.Text += line + "\r\n";
            }
            richTextBox1.Text += "\r\n";
            //
            richTextBox1.Text += "**************Recent Event History**************\r\n";
            reader = new StringReader(Model.History);
            line = null;
            number = 0;
            while ((line = reader.ReadLine()) != null && number++ < 10)
                richTextBox1.Text += line + "\r\n";
        }
        //
        private void copyContents_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }
        private void ignoreError_Click(object sender, EventArgs e)
        {
            Model.Crashing = false;
            this.Close();
        }
        private void saveAndClose_Click(object sender, EventArgs e)
        {
        }
        private void saveAsAndClose_Click(object sender, EventArgs e)
        {
        }
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
        private void reportError_Click(object sender, EventArgs e)
        {
            string url = "http://acmlm.kafuka.org/board/newreply.php?id=7005";
            Process proc = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(url);
            proc.StartInfo = startInfo;
            proc.Start();
        }
    }
}
