using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class Levels
    {
        #region Variables
        private LevelTileMods tileMods
        {
            get
            {
                return level.LevelTileMods;
            }
            set
            {
                level.LevelTileMods = value;
            }
        }
        private Object copyTileMod;
        private LevelSolidMods solidMods
        {
            get
            {
                return level.LevelSolidMods;
            }
            set
            {
                level.LevelSolidMods = value;
            }
        }
        private Object copySolidMod;
        public TreeView TileModsFieldTree { get { return tileModsFieldTree; } set { tileModsFieldTree = value; } }
        public TreeView SolidModsFieldTree { get { return solidModsFieldTree; } set { solidModsFieldTree = value; } }
        public NumericUpDown TileModsX { get { return tileModsX; } set { tileModsX = value; } }
        public NumericUpDown TileModsY { get { return tileModsY; } set { tileModsY = value; } }
        public NumericUpDown TileModsWidth { get { return tileModsWidth; } set { tileModsWidth = value; } }
        public NumericUpDown TileModsHeight { get { return tileModsHeight; } set { tileModsHeight = value; } }
        public CheckedListBox TileModsLayers { get { return tileModsLayers; } set { tileModsLayers = value; } }
        public NumericUpDown SolidModsX { get { return solidModsX; } set { solidModsX = value; } }
        public NumericUpDown SolidModsY { get { return solidModsY; } set { solidModsY = value; } }
        #endregion
        #region Functions
        private void InitializeTileModProperties()
        {
            this.Updating = true;
            this.tileModsFieldTree.Nodes.Clear();
            int i = 0;
            foreach (LevelTileMods.Mod mod in tileMods.Mods)
            {
                TreeNode node = new TreeNode("TILE MOD #" + i++.ToString());
                if (mod.Set)
                    node.Nodes.Add("ALTERNATE");
                this.tileModsFieldTree.Nodes.Add(node);
            }
            if (tileModsFieldTree.Nodes.Count > 0)
                this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[0];
            this.tileModsFieldTree.ExpandAll();
            if (tileMods.Mods.Count != 0 && this.tileModsFieldTree.SelectedNode != null)
            {
                tileMods.CurrentMod = this.tileModsFieldTree.SelectedNode.Index;
                tileModsX.Value = tileMods.X;
                tileModsY.Value = tileMods.Y;
                tileModsWidth.Value = tileMods.Width;
                tileModsHeight.Value = tileMods.Height;
                tileModsLayers.SetItemChecked(0, tileMods.Layer1);
                tileModsLayers.SetItemChecked(1, tileMods.Layer2);
                tileModsLayers.SetItemChecked(2, tileMods.Layer3);
                tileModsLayers.SetItemChecked(3, tileMods.B0b7);
                bool enable = this.tileModsFieldTree.SelectedNode.Parent == null;
                foreach (ToolStripItem item in toolStrip7.Items)
                    item.Enabled = enable;
                tileModsInsertInstance.Enabled =
                    this.tileModsFieldTree.SelectedNode.Nodes.Count == 0 &&
                    this.tileModsFieldTree.SelectedNode.Parent == null;
                tileModsDeleteField.Enabled = true;
                groupBox21.Enabled = enable;
            }
            else
            {
                tileModsX.Value = 0;
                tileModsY.Value = 0;
                tileModsWidth.Value = 1;
                tileModsHeight.Value = 1;
                tileModsLayers.SetItemChecked(0, false);
                tileModsLayers.SetItemChecked(1, false);
                tileModsLayers.SetItemChecked(2, false);
                tileModsLayers.SetItemChecked(3, false);
                foreach (ToolStripItem item in toolStrip7.Items)
                    if (item != tileModsInsertField)
                        item.Enabled = false;
                groupBox21.Enabled = false;
            }
            tileModsBytesLeft.Text = CalculateFreeTileModSpace() + " bytes left";
            tileModsBytesLeft.BackColor = CalculateFreeTileModSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void RefreshTileModProperties()
        {
            this.Updating = true;
            if (tileMods.Mods.Count != 0 && this.tileModsFieldTree.SelectedNode != null)
            {
                if (this.tileModsFieldTree.SelectedNode.Parent != null)
                    tileMods.CurrentMod = this.tileModsFieldTree.SelectedNode.Parent.Index;
                else
                    tileMods.CurrentMod = this.tileModsFieldTree.SelectedNode.Index;
                tileModsX.Value = tileMods.X;
                tileModsY.Value = tileMods.Y;
                tileModsWidth.Value = tileMods.Width;
                tileModsHeight.Value = tileMods.Height;
                tileModsLayers.SetItemChecked(0, tileMods.Layer1);
                tileModsLayers.SetItemChecked(1, tileMods.Layer2);
                tileModsLayers.SetItemChecked(2, tileMods.Layer3);
                tileModsLayers.SetItemChecked(3, tileMods.B0b7);
                bool enable = this.tileModsFieldTree.SelectedNode.Parent == null;
                foreach (ToolStripItem item in toolStrip7.Items)
                    item.Enabled = enable;
                tileModsInsertInstance.Enabled =
                    this.tileModsFieldTree.SelectedNode.Nodes.Count == 0 &&
                    this.tileModsFieldTree.SelectedNode.Parent == null;
                tileModsDeleteField.Enabled = true;
                groupBox21.Enabled = enable;
            }
            else
            {
                tileModsX.Value = 0;
                tileModsY.Value = 0;
                tileModsWidth.Value = 1;
                tileModsHeight.Value = 1;
                tileModsLayers.SetItemChecked(0, false);
                tileModsLayers.SetItemChecked(1, false);
                tileModsLayers.SetItemChecked(2, false);
                tileModsLayers.SetItemChecked(3, false);
                foreach (ToolStripItem item in toolStrip7.Items)
                    if (item != tileModsInsertField)
                        item.Enabled = false;
                groupBox21.Enabled = false;
            }
            tileModsBytesLeft.Text = CalculateFreeTileModSpace() + " bytes left";
            tileModsBytesLeft.BackColor = CalculateFreeTileModSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
            picture.Invalidate();
        }
        private int CalculateFreeTileModSpace()
        {
            int used = 0;
            for (int i = 0; i < 512; i++)
            {
                foreach (LevelTileMods.Mod mod in levels[i].LevelTileMods.Mods)
                    used += mod.Length;
            }
            return 0x2AF3 - used;
        }
        private void AddNewTileMod(LevelTileMods.Mod tileMod)
        {
            if (CalculateFreeTileModSpace() >= tileMod.Length)
            {
                this.tileModsFieldTree.Focus();
                if (tileMods.Mods.Count < 32)
                {
                    if (tileModsFieldTree.Nodes.Count > 0)
                        tileMods.Insert(tileModsFieldTree.SelectedNode.Index + 1, tileMod);
                    else
                        tileMods.Insert(0, tileMod);
                    int index;
                    if (tileModsFieldTree.Nodes.Count > 0)
                        index = tileModsFieldTree.SelectedNode.Index;
                    else
                        index = -1;
                    this.tileModsFieldTree.BeginUpdate();
                    this.tileModsFieldTree.Nodes.Clear();
                    int i = 0;
                    foreach (LevelTileMods.Mod mod in tileMods.Mods)
                    {
                        TreeNode node = new TreeNode("TILE MOD #" + i++.ToString());
                        if (mod.Set)
                            node.Nodes.Add("ALTERNATE");
                        this.tileModsFieldTree.Nodes.Add(node);
                    }
                    this.tileModsFieldTree.ExpandAll();
                    this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index + 1];
                    this.tileModsFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more tile mods. The maximum number of tile mods allowed per level is 32.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the mod. " + MaximumSpaceExceeded("tile mods"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public bool AddNewTileMod()
        {
            Point p = new Point(Math.Abs(picture.Left) / zoom / 16, Math.Abs(picture.Top) / zoom / 16);
            if (CalculateFreeTileModSpace() >= 8)
            {
                this.tileModsFieldTree.Focus();
                if (tileMods.Mods.Count < 32)
                {
                    int index = 0;
                    if (tileModsFieldTree.SelectedNode != null)
                        index = tileModsFieldTree.SelectedNode.Parent == null ?
                            tileModsFieldTree.SelectedNode.Index : tileModsFieldTree.SelectedNode.Parent.Index;
                    if (tileModsFieldTree.Nodes.Count > 0)
                        tileMods.Insert(index + 1, p, level, tileset);
                    else
                        tileMods.Insert(0, p, level, tileset);
                    if (tileModsFieldTree.Nodes.Count == 0)
                        index = -1;
                    this.tileModsFieldTree.BeginUpdate();
                    this.tileModsFieldTree.Nodes.Clear();
                    int i = 0;
                    foreach (LevelTileMods.Mod mod in tileMods.Mods)
                    {
                        TreeNode node = new TreeNode("TILE MOD #" + i++.ToString());
                        if (mod.Set)
                            node.Nodes.Add("ALTERNATE");
                        this.tileModsFieldTree.Nodes.Add(node);
                    }
                    this.tileModsFieldTree.ExpandAll();
                    this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index + 1];
                    this.tileModsFieldTree.EndUpdate();
                }
                else
                {
                    MessageBox.Show("Could not insert any more tile mods. The maximum number of tile mods allowed per level is 32.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Could not insert the mod. " + MaximumSpaceExceeded("tile mods"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        public bool AddNewTileModInstance()
        {
            if (CalculateFreeTileModSpace() < 0)
            {
                MessageBox.Show("Could not insert the alternate mod. " + MaximumSpaceExceeded("tile mods"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            tileMods.Set = true;
            if (tileMods.TilemapsA[0] != null)
            {
                tileMods.TilemapsB[0] = new byte[tileMods.TilemapsA[0].Length];
                tileMods.TilemapsA[0].CopyTo(tileMods.TilemapsB[0], 0);
            }
            if (tileMods.TilemapsA[1] != null)
            {
                tileMods.TilemapsB[1] = new byte[tileMods.TilemapsA[1].Length];
                tileMods.TilemapsA[1].CopyTo(tileMods.TilemapsB[1], 0);
            }
            if (tileMods.TilemapsA[2] != null)
            {
                tileMods.TilemapsB[2] = new byte[tileMods.TilemapsA[2].Length];
                tileMods.TilemapsA[2].CopyTo(tileMods.TilemapsB[2], 0);
            }
            tileMods.TilemapB = new LevelTilemap(level, tileset, tileMods.MOD, true);
            this.tileModsFieldTree.BeginUpdate();
            this.tileModsFieldTree.SelectedNode.Nodes.Add("ALTERNATE");
            this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.SelectedNode.Nodes[0];
            this.tileModsFieldTree.EndUpdate();
            return true;
        }
        //
        private void InitializeSolidModProperties()
        {
            this.Updating = true;
            this.solidModsFieldTree.Nodes.Clear();
            for (int i = 0; i < solidMods.Mods.Count; i++)
                this.solidModsFieldTree.Nodes.Add(new TreeNode("SOLID MOD #" + i.ToString()));
            if (solidModsFieldTree.Nodes.Count > 0)
                this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[0];
            if (solidMods.Mods.Count != 0 && this.solidModsFieldTree.SelectedNode != null)
            {
                solidMods.CurrentMod = this.solidModsFieldTree.SelectedNode.Index;
                solidModsX.Value = solidMods.X;
                solidModsY.Value = solidMods.Y;
                solidModsWidth.Value = solidMods.Width;
                solidModsHeight.Value = solidMods.Height;
                foreach (ToolStripItem item in toolStrip8.Items)
                    item.Enabled = true;
                groupBox20.Enabled = true;
            }
            else
            {
                solidModsX.Value = 0;
                solidModsY.Value = 0;
                solidModsWidth.Value = 1;
                solidModsHeight.Value = 1;
                foreach (ToolStripItem item in toolStrip8.Items)
                    item.Enabled = false;
                solidModsInsert.Enabled = true;
                groupBox20.Enabled = false;
            }
            solidModsBytesLeft.Text = CalculateFreeSolidModSpace() + " bytes left";
            solidModsBytesLeft.BackColor = CalculateFreeSolidModSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void RefreshSolidModProperties()
        {
            this.Updating = true;
            if (solidMods.Mods.Count != 0 && this.solidModsFieldTree.SelectedNode != null)
            {
                if (this.solidModsFieldTree.SelectedNode.Parent != null)
                    solidMods.CurrentMod = this.solidModsFieldTree.SelectedNode.Parent.Index;
                else
                    solidMods.CurrentMod = this.solidModsFieldTree.SelectedNode.Index;
                solidModsX.Value = solidMods.X;
                solidModsY.Value = solidMods.Y;
                solidModsWidth.Value = solidMods.Width;
                solidModsHeight.Value = solidMods.Height;
                foreach (ToolStripItem item in toolStrip8.Items)
                    item.Enabled = true;
                groupBox20.Enabled = true;
            }
            else
            {
                foreach (ToolStripItem item in toolStrip8.Items)
                    item.Enabled = false;
                solidModsInsert.Enabled = true;
                groupBox20.Enabled = false;
                solidModsX.Value = 0;
                solidModsY.Value = 0;
                solidModsWidth.Value = 1;
                solidModsHeight.Value = 1;
            }
            solidModsBytesLeft.Text = CalculateFreeSolidModSpace() + " bytes left";
            solidModsBytesLeft.BackColor = CalculateFreeSolidModSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
            picture.Invalidate();
        }
        private int CalculateFreeSolidModSpace()
        {
            int used = 0;
            for (int i = 0; i < 512; i++)
            {
                foreach (LevelSolidMods.LevelMod mod in levels[i].LevelSolidMods.Mods)
                    used += mod.Length;
            }
            return 0x08FF - used;
        }
        private void AddNewSolidMod(LevelSolidMods.LevelMod solidMod)
        {
            if (CalculateFreeSolidModSpace() >= solidMod.Length)
            {
                this.solidModsFieldTree.Focus();
                if (solidMods.Mods.Count < 32)
                {
                    if (solidModsFieldTree.Nodes.Count > 0)
                        solidMods.Insert(solidModsFieldTree.SelectedNode.Index + 1, solidMod);
                    else
                        solidMods.Insert(0, solidMod);
                    int index;
                    if (solidModsFieldTree.Nodes.Count > 0)
                        index = solidModsFieldTree.SelectedNode.Index;
                    else
                        index = -1;
                    this.solidModsFieldTree.BeginUpdate();
                    this.solidModsFieldTree.Nodes.Clear();
                    int i = 0;
                    foreach (LevelSolidMods.LevelMod mod in solidMods.Mods)
                        this.solidModsFieldTree.Nodes.Add("SOLID MOD #" + i++.ToString());
                    this.solidModsFieldTree.ExpandAll();
                    this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[index + 1];
                    this.solidModsFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more solid mods. The maximum number of solid mods allowed per level is 32.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the mod. " + MaximumSpaceExceeded("solid mods"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event handlers
        private void tileModsFieldTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tileModsFieldTree.SelectedNode.Parent == null)
            {
                tileMods.CurrentMod = this.tileModsFieldTree.SelectedNode.Index;
                tileMods.SelectedMod = this.tileModsFieldTree.SelectedNode.Index;
                tileMods.SelectedB = false;
            }
            else
            {
                tileMods.CurrentMod = this.tileModsFieldTree.SelectedNode.Parent.Index;
                tileMods.SelectedMod = this.tileModsFieldTree.SelectedNode.Parent.Index;
                tileMods.SelectedB = true;
            }
            RefreshTileModProperties();
            picture.Invalidate();
        }
        private void tileModsX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            tileMods.X = (int)tileModsX.Value;
            picture.Invalidate();
        }
        private void tileModsY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            tileMods.Y = (int)tileModsY.Value;
            picture.Invalidate();
        }
        private void tileModsWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int temp = tileMods.Width;
            tileMods.Width = (int)tileModsWidth.Value;
            if (CalculateFreeTileModSpace() < 0)
            {
                tileModsWidth.Value = temp;
                MessageBox.Show("Could not change the width. There is not enough free space available.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tileMods.TilemapA = new LevelTilemap(level, tileset, tileMods.MOD, false);
            if (tileMods.Set)
                tileMods.TilemapB = new LevelTilemap(level, tileset, tileMods.MOD, true);
            tileModsBytesLeft.Text = CalculateFreeTileModSpace() + " bytes left";
            tileModsBytesLeft.BackColor = CalculateFreeTileModSpace() >= 0 ? SystemColors.Control : Color.Red;
            picture.Invalidate();
        }
        private void tileModsHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int temp = tileMods.Height;
            tileMods.Height = (int)tileModsHeight.Value;
            if (CalculateFreeTileModSpace() < 0)
            {
                tileModsHeight.Value = temp;
                MessageBox.Show("Could not change the height. There is not enough free space available.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tileMods.TilemapA = new LevelTilemap(level, tileset, tileMods.MOD, false);
            if (tileMods.Set)
                tileMods.TilemapB = new LevelTilemap(level, tileset, tileMods.MOD, true);
            tileModsBytesLeft.Text = CalculateFreeTileModSpace() + " bytes left";
            tileModsBytesLeft.BackColor = CalculateFreeTileModSpace() >= 0 ? SystemColors.Control : Color.Red;
            picture.Invalidate();
        }
        public void tileModsLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            tileMods.Layer1 = tileModsLayers.GetItemChecked(0);
            tileMods.Layer2 = tileModsLayers.GetItemChecked(1);
            tileMods.Layer3 = tileModsLayers.GetItemChecked(2);
            tileMods.B0b7 = tileModsLayers.GetItemChecked(3);
            tileModsBytesLeft.Text = CalculateFreeTileModSpace() + " bytes left";
            tileModsBytesLeft.BackColor = CalculateFreeTileModSpace() >= 0 ? SystemColors.Control : Color.Red;
        }
        private void tileModsInsertField_Click(object sender, EventArgs e)
        {
            AddNewTileMod();
        }
        private void tileModsInsertInstance_Click(object sender, EventArgs e)
        {
            AddNewTileModInstance();
        }
        private void tileModsDeleteField_Click(object sender, EventArgs e)
        {
            if (this.tileModsFieldTree.SelectedNode.Parent == null)
            {
                int index = tileModsFieldTree.SelectedNode.Index;
                tileMods.Mods.RemoveAt(index);
                this.tileModsFieldTree.Nodes.RemoveAt(index);
                if (index >= this.tileModsFieldTree.Nodes.Count)
                    index--;
                this.tileModsFieldTree.BeginUpdate();
                this.tileModsFieldTree.Nodes.Clear();
                int i = 0;
                foreach (LevelTileMods.Mod mod in tileMods.Mods)
                {
                    TreeNode node = new TreeNode("TILE MOD #" + i++.ToString());
                    if (mod.Set)
                        node.Nodes.Add("ALTERNATE");
                    this.tileModsFieldTree.Nodes.Add(node);
                }
                this.tileModsFieldTree.ExpandAll();
                if (this.tileModsFieldTree.Nodes.Count > 0)
                    this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index];
                else
                    RefreshTileModProperties();
                this.tileModsFieldTree.EndUpdate();
            }
            else
            {
                int index = tileModsFieldTree.SelectedNode.Parent.Index;
                tileMods.Set = false;
                this.tileModsFieldTree.SelectedNode.Remove();
                this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index];
            }
        }
        private void tileModsMoveUp_Click(object sender, EventArgs e)
        {
            if (this.tileModsFieldTree.SelectedNode == null)
                return;
            int index = 0;
            if (this.tileModsFieldTree.SelectedNode.Parent == null && tileMods.CurrentMod > 0)
            {
                index = tileModsFieldTree.SelectedNode.Index - 1;
                tileMods.Reverse(tileMods.CurrentMod - 1);
            }
            else
                return;
            this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index];
        }
        private void tileModsMoveDown_Click(object sender, EventArgs e)
        {
            if (this.tileModsFieldTree.SelectedNode == null)
                return;
            int index = 0;
            if (this.tileModsFieldTree.SelectedNode.Parent == null && tileMods.CurrentMod < tileMods.Mods.Count - 1)
            {
                index = tileModsFieldTree.SelectedNode.Index + 1;
                tileMods.Reverse(tileMods.CurrentMod);
            }
            else
                return;
            this.tileModsFieldTree.SelectedNode = this.tileModsFieldTree.Nodes[index];
        }
        private void tileModsCopy_Click(object sender, EventArgs e)
        {
            if (tileModsFieldTree.SelectedNode != null)
                copyTileMod = tileMods.MOD.Copy(level, tileset);
        }
        private void tileModsPaste_Click(object sender, EventArgs e)
        {
            if (copyTileMod == null)
                return;
            AddNewTileMod((LevelTileMods.Mod)copyTileMod);
        }
        private void tileModsDuplicate_Click(object sender, EventArgs e)
        {
            AddNewTileMod(tileMods.MOD.Copy(level, tileset));
        }
        // solidity mods
        private void solidModsFieldTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            solidMods.CurrentMod = this.solidModsFieldTree.SelectedNode.Index;
            solidMods.SelectedMod = this.solidModsFieldTree.SelectedNode.Index;
            solidMods.CurrentMod = this.solidModsFieldTree.SelectedNode.Index;
            RefreshSolidModProperties();
            picture.Invalidate();
        }
        private void solidModsX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidMods.X = (int)solidModsX.Value;
            solidMods.MOD.Pixels = solidity.GetTilemapPixels(solidMods.MOD);
            picture.Invalidate();
        }
        private void solidModsY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidMods.Y = (int)solidModsY.Value;
            solidMods.MOD.Pixels = solidity.GetTilemapPixels(solidMods.MOD);
            picture.Invalidate();
        }
        private void solidModsWidth_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int temp = solidMods.Width;
            solidMods.Width = (int)solidModsWidth.Value;
            if (CalculateFreeSolidModSpace() < 0)
            {
                solidMods.Width = temp;
                MessageBox.Show("Could not change the width. There is not enough free space available.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            solidMods.MOD.Pixels = solidity.GetTilemapPixels(solidMods.MOD);
            solidModsBytesLeft.Text = CalculateFreeSolidModSpace() + " bytes left";
            solidModsBytesLeft.BackColor = CalculateFreeSolidModSpace() >= 0 ? SystemColors.Control : Color.Red;
            picture.Invalidate();
        }
        private void solidModsHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int temp = solidMods.Height;
            solidMods.Height = (int)solidModsHeight.Value;
            if (CalculateFreeSolidModSpace() < 0)
            {
                solidMods.Height = temp;
                MessageBox.Show("Could not change the height. There is not enough free space available.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            solidMods.MOD.Pixels = solidity.GetTilemapPixels(solidMods.MOD);
            solidModsBytesLeft.Text = CalculateFreeSolidModSpace() + " bytes left";
            solidModsBytesLeft.BackColor = CalculateFreeSolidModSpace() >= 0 ? SystemColors.Control : Color.Red;
            picture.Invalidate();
        }
        private void solidModsInsert_Click(object sender, EventArgs e)
        {
            Point o = new Point(Math.Abs(picture.Left) / zoom, Math.Abs(picture.Top) / zoom);
            Point p = new Point(solidity.PixelCoords[o.Y * 1024 + o.X].X + 2, solidity.PixelCoords[o.Y * 1024 + o.X].Y + 4);
            if (CalculateFreeSolidModSpace() >= 4)
            {
                this.solidModsFieldTree.Focus();
                if (solidMods.Mods.Count < 32)
                {
                    int index = 0;
                    if (solidModsFieldTree.SelectedNode != null)
                        index = solidModsFieldTree.SelectedNode.Index;
                    if (solidModsFieldTree.Nodes.Count > 0)
                        solidMods.Insert(index + 1, p);
                    else
                        solidMods.Insert(0, p);
                    if (solidModsFieldTree.Nodes.Count == 0)
                        index = -1;
                    this.solidModsFieldTree.BeginUpdate();
                    this.solidModsFieldTree.Nodes.Clear();
                    int i = 0;
                    foreach (LevelSolidMods.LevelMod mod in solidMods.Mods)
                        this.solidModsFieldTree.Nodes.Add("SOLID MOD #" + i++.ToString());
                    this.solidModsFieldTree.ExpandAll();
                    this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[index + 1];
                    this.solidModsFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more solid mods. The maximum number of solid mods allowed per level is 32.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the mod. " + MaximumSpaceExceeded("solid mods"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void solidModsDelete_Click(object sender, EventArgs e)
        {
            int index = solidModsFieldTree.SelectedNode.Index;
            solidMods.Mods.RemoveAt(index);
            this.solidModsFieldTree.Nodes.RemoveAt(index);
            if (index >= this.solidModsFieldTree.Nodes.Count)
                index--;
            this.solidModsFieldTree.BeginUpdate();
            this.solidModsFieldTree.Nodes.Clear();
            for (int i = 0; i < solidMods.Mods.Count; i++)
                this.solidModsFieldTree.Nodes.Add(new TreeNode("SOLID MOD #" + i.ToString()));
            if (this.solidModsFieldTree.Nodes.Count > 0)
                this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[index];
            else
                RefreshSolidModProperties();
            this.solidModsFieldTree.EndUpdate();
        }
        private void solidModsMoveUp_Click(object sender, EventArgs e)
        {
            if (this.solidModsFieldTree.SelectedNode == null)
                return;
            int index = 0;
            if (this.solidModsFieldTree.SelectedNode.Parent == null && solidMods.CurrentMod > 0)
            {
                index = solidModsFieldTree.SelectedNode.Index - 1;
                solidMods.ReverseMod(solidMods.CurrentMod - 1);
            }
            else return;
            this.solidModsFieldTree.BeginUpdate();
            this.solidModsFieldTree.Nodes.Clear();
            for (int i = 0; i < solidMods.Mods.Count; i++)
                this.solidModsFieldTree.Nodes.Add(new TreeNode("SOLID MOD #" + i.ToString()));
            this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[index];
            this.solidModsFieldTree.EndUpdate();
        }
        private void solidModsMoveDown_Click(object sender, EventArgs e)
        {
            if (this.solidModsFieldTree.SelectedNode == null)
                return;
            int index = 0;
            if (this.solidModsFieldTree.SelectedNode.Parent == null && solidMods.CurrentMod < solidMods.Mods.Count - 1)
            {
                index = solidModsFieldTree.SelectedNode.Index + 1;
                solidMods.ReverseMod(solidMods.CurrentMod);
            }
            else return;
            this.solidModsFieldTree.BeginUpdate();
            this.solidModsFieldTree.Nodes.Clear();
            for (int i = 0; i < solidMods.Mods.Count; i++)
                this.solidModsFieldTree.Nodes.Add(new TreeNode("SOLID MOD #" + i.ToString()));
            this.solidModsFieldTree.SelectedNode = this.solidModsFieldTree.Nodes[index];
            this.solidModsFieldTree.EndUpdate();
        }
        private void solidModsCopy_Click(object sender, EventArgs e)
        {
            if (solidModsFieldTree.SelectedNode == null)
                return;
            copySolidMod = solidMods.MOD.Copy();
        }
        private void solidModsPaste_Click(object sender, EventArgs e)
        {
            if (copySolidMod != null)
                AddNewSolidMod((LevelSolidMods.LevelMod)copySolidMod);
        }
        private void solidModsDuplicate_Click(object sender, EventArgs e)
        {
            AddNewSolidMod(solidMods.MOD.Copy());
        }
        #endregion
    }
}
