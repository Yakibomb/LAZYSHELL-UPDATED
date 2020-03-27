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
    public partial class FormationPacks : NewForm
    {
        #region Variables
        //
        private delegate void Function(TreeView treeView, StringComparison stringComparison, bool matchWholeWord);
        private int index { get { return (int)packNum.Value; } set { packNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private FormationPack[] packs { get { return Model.FormationPacks; } set { Model.FormationPacks = value; } }
        private FormationPack pack { get { return packs[index]; } set { packs[index] = value; } }
        public FormationPack Pack { get { return pack; } set { pack = value; } }
                private Formation[] formations { get { return Model.Formations; } }
        private Formations formationsEditor;
        public Search searchWindow;
        private EditLabel labelWindow;
        #endregion
        // constructor
        public FormationPacks(Formations formationsEditor)
        {
            this.formationsEditor = formationsEditor;
            InitializeComponent();
            searchWindow = new Search(packNum, searchBox, searchFormationPacks, new Function(LoadSearch), "treeView");
            labelWindow = new EditLabel(null, packNum, "Packs", false);
            RefreshFormationPacks();
            //
            this.History = new History(this, null, packNum);
        }
        // functions
        public void RefreshFormationPacks()
        {
            if (this.Updating)
                return;
            this.Updating = true;
            this.packFormation1.Value = pack.Formations[0];
            this.packFormation2.Value = pack.Formations[1];
            this.packFormation3.Value = pack.Formations[2];
            RefreshFormationPackStrings();
            this.Updating = false;
        }
        private void RefreshFormationPackStrings()
        {
            this.richTextBox2.Text = formations[pack.Formations[0]].NamePack;
            this.richTextBox3.Text = formations[pack.Formations[1]].NamePack;
            this.richTextBox4.Text = formations[pack.Formations[2]].NamePack;
        }
        private void LoadSearch(TreeView treeView, StringComparison stringComparison, bool matchWholeWord)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            if (searchBox.Text == "")
            {
                treeView.EndUpdate();
                return;
            }
            TreeNode tn;
            TreeNode cn;
            foreach (FormationPack fp in packs)
            {
                if (Do.Contains(
                    formations[fp.Formations[0]].ToString(),
                    searchBox.Text, stringComparison, matchWholeWord) ||
                    Do.Contains(
                    formations[fp.Formations[1]].ToString(),
                    searchBox.Text, stringComparison, matchWholeWord) ||
                    Do.Contains(
                    formations[fp.Formations[2]].ToString(),
                    searchBox.Text, stringComparison, matchWholeWord))
                {
                    tn = treeView.Nodes.Add("PACK #" + fp.Index);
                    tn.Tag = (int)fp.Index;
                    if (Do.Contains(
                        formations[fp.Formations[0]].ToString(),
                        searchBox.Text, stringComparison, matchWholeWord))
                    {
                        cn = tn.Nodes.Add(formations[fp.Formations[0]].ToString());
                        cn.Tag = (int)fp.Index;
                    }
                    if (Do.Contains(
                        formations[fp.Formations[1]].ToString(),
                        searchBox.Text, stringComparison, matchWholeWord))
                    {
                        cn = tn.Nodes.Add(formations[fp.Formations[1]].ToString());
                        cn.Tag = (int)fp.Index;
                    }
                    if (Do.Contains(
                        formations[fp.Formations[2]].ToString(),
                        searchBox.Text, stringComparison, matchWholeWord))
                    {
                        cn = tn.Nodes.Add(formations[fp.Formations[2]].ToString());
                        cn.Tag = (int)fp.Index;
                    }
                }
            }
            treeView.ExpandAll();
            treeView.EndUpdate();
        }
        // event handlers
        private void packNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshFormationPacks();
            Settings.Default.LastFormationPack = index;
        }
        private void packFormation1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            pack.Formations[0] = (ushort)packFormation1.Value;
            RefreshFormationPackStrings();
        }
        private void packFormation2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            pack.Formations[1] = (ushort)packFormation2.Value;
            RefreshFormationPackStrings();
        }
        private void packFormation3_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            pack.Formations[2] = (ushort)packFormation3.Value;
            RefreshFormationPackStrings();
        }
        private void packFormationButton1_Click(object sender, EventArgs e)
        {
            formationsEditor.Index = (int)packFormation1.Value;
        }
        private void packFormationButton2_Click(object sender, EventArgs e)
        {
            formationsEditor.Index = (int)packFormation2.Value;
        }
        private void packFormationButton3_Click(object sender, EventArgs e)
        {
            formationsEditor.Index = (int)packFormation3.Value;
        }
    }
}
