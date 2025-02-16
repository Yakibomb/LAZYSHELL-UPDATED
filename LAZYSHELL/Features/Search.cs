using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class Search : NewForm
    {
        #region Variables
        private delegate void Function();
        private Delegate function;
        private ToolStripTextBox searchField;
        private ToolStripButton searchButton;
        private System.Windows.Forms.ToolStripComboBox searchIndexName;
        private ToolStripNumericUpDown searchIndexNum;
        private IList names; public IList Names { get { return names; } set { names = value; } }
        private bool searchFieldEnter = false;
        private bool initialized = false;
        private StringComparison stringComparison
        {
            get
            {
                if (matchCase.Checked)
                    return StringComparison.CurrentCulture;
                else
                    return StringComparison.CurrentCultureIgnoreCase;
            }
        }
        private Timer timer = new Timer();
        private Point searchFieldLocation
        {
            get
            {
                return searchField.Control.PointToScreen(new Point(searchField.Width, searchField.Height));
            }
        }
        #endregion
        // constructor
        /// <summary>
        /// Loads a search form containing the results of a search query.
        /// </summary>
        /// <param name="searchIndexNum">The search index control to update when a search result is selected.</param>
        /// <param name="searchField">The search field control containing the search query text.</param>
        /// <param name="searchButton">The search button control that invokes the search.</param>
        /// <param name="names">The data list to search for a specified query in.</param>
        public Search(ToolStripNumericUpDown searchIndexNum, ToolStripTextBox searchField, ToolStripButton searchButton, IList names)
        {
            InitializeComponent();
            this.listBox.Enabled = true;
            this.listBox.Show();
            this.listBox.BringToFront();
            this.names = names;
            this.searchIndexNum = searchIndexNum;
            this.searchField = searchField;
            this.searchButton = searchButton;
            InitializeProperties();
            LoadSearch();
            this.function = new Function(LoadSearch);
            this.function.DynamicInvoke();
            InitializeTimer();
        }
        /// <summary>
        /// Loads a search form containing the results of a search query.
        /// </summary>
        /// <param name="searchIndexName">The search index control to update when a search result is selected.</param>
        /// <param name="searchField">The search field control containing the search query text.</param>
        /// <param name="searchButton">The search button control that invokes the search.</param>
        /// <param name="names">The data list to search for a specified query in.</param>
        public Search(System.Windows.Forms.ToolStripComboBox searchIndexName, ToolStripTextBox searchField, ToolStripButton searchButton, IList names)
        {
            InitializeComponent();
            this.listBox.Enabled = true;
            this.listBox.Show();
            this.listBox.BringToFront();
            this.names = names;
            this.searchIndexName = searchIndexName;
            this.searchField = searchField;
            this.searchButton = searchButton;
            InitializeProperties();
            this.function = new Function(LoadSearch);
            this.function.DynamicInvoke();
            this.Location = searchFieldLocation;
            InitializeTimer();
        }
        /// <summary>
        /// Loads a search form containing the results of a search query.
        /// </summary>
        /// <param name="searchIndexNum">The search index control to update when a search result is selected.</param>
        /// <param name="searchField">The search field control containing the search query text.</param>
        /// <param name="searchButton">The search button control that invokes the search.</param>
        /// <param name="function">The function to execute when a search is invoked.</param>
        /// <param name="type">The type of control that contains the search results.
        /// Options include: treeView, richTextBox</param>
        public Search(ToolStripNumericUpDown searchIndexNum, ToolStripTextBox searchField, ToolStripButton searchButton, Delegate function, string type)
        {
            InitializeComponent();
            this.searchIndexNum = searchIndexNum;
            this.searchField = searchField;
            this.searchButton = searchButton;
            InitializeProperties();
            this.function = function;
            this.toolStripSeparator1.Visible = type == "richTextBox";
            this.toolStripLabel1.Visible = type == "richTextBox";
            this.replaceAllButton.Visible = type == "richTextBox";
            this.replaceWithText.Visible = type == "richTextBox";
            if (type == "treeView")
            {
                this.treeView.Enabled = true;
                this.treeView.Show();
                this.treeView.BringToFront();
                this.function.DynamicInvoke(treeView, stringComparison, matchWholeWord.Checked);
            }
            else if (type == "richTextBox")
            {
                this.richTextBox.Enabled = true;
                this.richTextBox.Show();
                this.richTextBox.BringToFront();
                this.function.DynamicInvoke(richTextBox, stringComparison, matchWholeWord.Checked, false, "");
            }
            this.Location = searchFieldLocation;
            InitializeTimer();
        }
        // functions
        private void InitializeProperties()
        {
            this.searchField.ForeColor = SystemColors.ControlDark;
            this.searchField.Text = "Find...";
            this.searchField.KeyDown += new KeyEventHandler(searchField_KeyDown);
            this.searchField.KeyUp += new KeyEventHandler(searchField_KeyUp);
            this.searchField.MouseDown += new MouseEventHandler(searchField_MouseDown);
            this.searchField.Leave += new EventHandler(searchField_Leave);
            this.searchButton.CheckOnClick = true;
            this.searchButton.Click += new EventHandler(searchButton_Click);
        }
        public void LoadSearch()
        {
            listBox.BeginUpdate();
            listBox.Items.Clear();
            if (searchField.Text == "")
            {
                listBox.EndUpdate();
                this.Height = 64 + toolStrip1.Height;
                return;
            }
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] == null)
                    continue;
                string name = names[i].ToString();
                int index = name.IndexOf(searchField.Text, stringComparison);
                if (index >= 0)
                {
                    if (matchWholeWord.Checked)
                    {
                        if (index + searchField.Text.Length < name.Length && Char.IsLetter(name, index + searchField.Text.Length))
                            continue;
                        if (index - 1 >= 0 && Char.IsLetter(name, index - 1))
                            continue;
                    }
                    SearchItem searchItem = new SearchItem(i, name);
                    listBox.Items.Add(searchItem);
                }
            }
            this.Height = Math.Min(
                listBox.Items.Count * listBox.ItemHeight + 64 + toolStrip1.Height,
                Screen.PrimaryScreen.WorkingArea.Height - this.Top - 16);
            listBox.EndUpdate();
        }
        private void InitializeTimer()
        {
            timer.Tick += new EventHandler(delegate
            {
                timer.Stop(); this.Location = searchFieldLocation;
            });
            timer.Start();
        }
        #region Event handlers
        private void matchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox.Enabled)
                this.function.DynamicInvoke();
            if (treeView.Enabled)
                this.function.DynamicInvoke(treeView, stringComparison, matchWholeWord.Checked);
            if (richTextBox.Enabled)
                this.function.DynamicInvoke(richTextBox, stringComparison, matchWholeWord.Checked, false, "");
            searchField.Focus();
        }
        private void matchWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox.Enabled)
                this.function.DynamicInvoke();
            if (treeView.Enabled)
                this.function.DynamicInvoke(treeView, stringComparison, matchWholeWord.Checked);
            if (richTextBox.Enabled)
                this.function.DynamicInvoke(richTextBox, stringComparison, matchWholeWord.Checked, false, "");
            searchField.Focus();
        }
        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to replace all occurrences of the specified text in all 4096 dialogues.\n\n" +
                "Are you sure you want to do this?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            if (richTextBox.Enabled)
                this.function.DynamicInvoke(richTextBox, stringComparison, matchWholeWord.Checked, true, replaceWithText.Text);
            searchField.Focus();
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem == null)
                return;
            int index = ((SearchItem)listBox.SelectedItem).Index;
            if (searchIndexNum != null && index < searchIndexNum.Maximum)
                searchIndexNum.Value = index;
            else if (searchIndexName != null && index < searchIndexName.Items.Count)
                searchIndexName.SelectedIndex = index;
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (searchIndexNum != null)
                searchIndexNum.Value = (int)treeView.SelectedNode.Tag;
            else if (searchIndexName != null)
                searchIndexName.SelectedIndex = (int)treeView.SelectedNode.Tag;
        }
        private void searchField_MouseDown(object sender, MouseEventArgs e)
        {
            if (!searchFieldEnter)
            {
                searchField.Text = "";
                searchField.ForeColor = SystemColors.ControlText;
            }
            searchFieldEnter = true;
        }
        private void searchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (!searchFieldEnter)
                searchField.ForeColor = SystemColors.ControlText;
            searchFieldEnter = true;
            if (e.KeyData == Keys.Enter)
            {
                searchButton.Checked = true;
                searchButton_Click(null, null);
                if (richTextBox.Enabled)
                    function.DynamicInvoke(richTextBox, stringComparison, matchWholeWord.Checked, false, "");
            }
        }
        private void searchField_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBox.Enabled)
                this.function.DynamicInvoke();
            if (treeView.Enabled)
                this.function.DynamicInvoke(treeView, stringComparison, matchWholeWord.Checked);
            searchField.Focus();
        }
        private void searchField_Leave(object sender, EventArgs e)
        {
            if (searchField.Text == "")
            {
                searchField.Text = "Find...";
                searchField.ForeColor = SystemColors.ControlDark;
                searchFieldEnter = false;
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            this.Visible = searchButton.Checked;
            if (this.Visible && !initialized)
            {
                this.Location = searchFieldLocation;
                initialized = true;
            }
        }
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.searchButton.Checked = false;
                this.Hide();
            }
        }
        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.searchButton.Checked = false;
            this.Hide();
        }
        #endregion
        private class SearchItem
        {
            public int Index;
            public string Text;
            public SearchItem(int index, string text)
            {
                this.Index = index;
                this.Text = text;
            }
            public override string ToString()
            {
                return this.Text;
            }
        }
    }
}
