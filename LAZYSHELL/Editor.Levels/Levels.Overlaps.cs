using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class Levels
    {
        #region Variables
        private LevelOverlaps overlaps { get { return level.LevelOverlaps; } set { level.LevelOverlaps = value; } } // Overlaps for the current level
        private Overlay.Selection overlapSelection = new Overlay.Selection();
        private OverlapTileset overlapTileset;
        private Bitmap overlapsImage;
        private int[] overlapsPixels;
        private Overlap copyOverlap;
        public TreeView OverlapFieldTree { get { return overlapFieldTree; } set { overlapFieldTree = value; } }
        public NumericUpDown OverlapX { get { return overlapX; } set { overlapX = value; } }
        public NumericUpDown OverlapY { get { return overlapY; } set { overlapY = value; } }
        #endregion
        #region Methods
        private void InitializeOverlapProperties()
        {
            this.Updating = true;
            this.overlapFieldTree.Nodes.Clear();
            for (int i = 0; i < overlaps.Count; i++)
            {
                this.overlapFieldTree.Nodes.Add(new TreeNode("OVERLAP #" + i.ToString()));
            }
            if (overlapFieldTree.Nodes.Count > 0)
                this.overlapFieldTree.SelectedNode = this.overlapFieldTree.Nodes[0];
            if (overlaps.Count != 0 && this.overlapFieldTree.SelectedNode != null)
            {
                overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
                this.overlapX.Value = overlaps.X;
                this.overlapY.Value = overlaps.Y;
                this.overlapZ.Value = overlaps.Z;
                this.overlapCoordZPlusHalf.Checked = overlaps.B1b7;
                this.overlapType.Value = overlaps.Type;
                this.overlapUnknownBits.SetItemChecked(0, overlaps.B0b7);
                this.overlapUnknownBits.SetItemChecked(1, overlaps.B2b5);
                this.overlapUnknownBits.SetItemChecked(2, overlaps.B2b6);
                this.overlapUnknownBits.SetItemChecked(3, overlaps.B2b7);
                foreach (ToolStripItem item in toolStrip4.Items)
                    item.Enabled = true;
                this.overlapX.Enabled = true;
                this.overlapY.Enabled = true;
                this.overlapZ.Enabled = true;
                this.overlapCoordZPlusHalf.Enabled = true;
                this.overlapType.Enabled = true;
                this.overlapUnknownBits.Enabled = true;
                this.panelOverlapTileset.Enabled = true;
                this.panelOverlapTileset.Show();
            }
            else
            {
                this.overlapX.Enabled = false;
                this.overlapY.Enabled = false;
                this.overlapZ.Enabled = false;
                this.overlapCoordZPlusHalf.Enabled = false;
                this.overlapType.Enabled = false;
                this.overlapUnknownBits.Enabled = false;
                this.panelOverlapTileset.Enabled = false;
                this.panelOverlapTileset.Hide();
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != overlapFieldInsert)
                        item.Enabled = false;
                this.overlapX.Value = 0;
                this.overlapY.Value = 0;
                this.overlapZ.Value = 0;
                this.overlapCoordZPlusHalf.Checked = false;
                this.overlapType.Value = 0;
                this.overlapUnknownBits.SetItemChecked(0, false);
                this.overlapUnknownBits.SetItemChecked(1, false);
                this.overlapUnknownBits.SetItemChecked(2, false);
                this.overlapUnknownBits.SetItemChecked(3, false);
            }
            //
            overlapSelection.Refresh(32, 0, 0, 32, 32, pictureBoxOverlaps);
            //
            pictureBoxOverlaps.Invalidate();
            overlapsBytesLeft.Text = CalculateFreeOverlapSpace() + " bytes left";
            overlapsBytesLeft.BackColor = CalculateFreeOverlapSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void RefreshOverlapProperties()
        {
            this.Updating = true;
            if (overlaps.Count != 0 && this.overlapFieldTree.SelectedNode != null)
            {
                overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
                this.overlapX.Value = overlaps.X;
                this.overlapY.Value = overlaps.Y;
                this.overlapZ.Value = overlaps.Z;
                this.overlapCoordZPlusHalf.Checked = overlaps.B1b7;
                this.overlapType.Value = overlaps.Type;
                this.overlapUnknownBits.SetItemChecked(0, overlaps.B0b7);
                this.overlapUnknownBits.SetItemChecked(1, overlaps.B2b5);
                this.overlapUnknownBits.SetItemChecked(2, overlaps.B2b6);
                this.overlapUnknownBits.SetItemChecked(3, overlaps.B2b7);
                foreach (ToolStripItem item in toolStrip4.Items)
                    item.Enabled = true;
                this.overlapX.Enabled = true;
                this.overlapY.Enabled = true;
                this.overlapZ.Enabled = true;
                this.overlapCoordZPlusHalf.Enabled = true;
                this.overlapType.Enabled = true;
                this.overlapUnknownBits.Enabled = true;
                this.panelOverlapTileset.Enabled = true;
                this.panelOverlapTileset.Show();
            }
            else
            {
                this.overlapX.Enabled = false;
                this.overlapY.Enabled = false;
                this.overlapZ.Enabled = false;
                this.overlapCoordZPlusHalf.Enabled = false;
                this.overlapType.Enabled = false;
                this.overlapUnknownBits.Enabled = false;
                this.panelOverlapTileset.Enabled = false;
                this.panelOverlapTileset.Hide();
                foreach (ToolStripItem item in toolStrip4.Items)
                    if (item != overlapFieldInsert)
                        item.Enabled = false;
                this.overlapX.Value = 0;
                this.overlapY.Value = 0;
                this.overlapZ.Value = 0;
                this.overlapCoordZPlusHalf.Checked = false;
                this.overlapType.Value = 0;
                this.overlapUnknownBits.SetItemChecked(0, false);
                this.overlapUnknownBits.SetItemChecked(1, false);
                this.overlapUnknownBits.SetItemChecked(2, false);
                this.overlapUnknownBits.SetItemChecked(3, false);
            }
            pictureBoxOverlaps.Invalidate();
            overlapsBytesLeft.Text = CalculateFreeOverlapSpace() + " bytes left";
            overlapsBytesLeft.BackColor = CalculateFreeOverlapSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        public int CalculateFreeOverlapSpace()
        {
            int used = 0;
            foreach (Level level in levels)
            {
                foreach (Overlap overlap in level.LevelOverlaps.Overlaps)
                    used += 4;
            }
            return 0x11B8 - used;
        }
        private void AddNewOverlap(Overlap overlap)
        {
            if (CalculateFreeOverlapSpace() >= 4)
            {
                this.overlapFieldTree.Focus();
                if (overlaps.Count < 28)
                {
                    if (overlapFieldTree.Nodes.Count > 0)
                        overlaps.New(overlapFieldTree.SelectedNode.Index + 1, overlap);
                    else
                        overlaps.New(0, overlap);
                    int reselect;
                    if (overlapFieldTree.Nodes.Count > 0)
                        reselect = overlapFieldTree.SelectedNode.Index;
                    else
                        reselect = -1;
                    overlapFieldTree.BeginUpdate();
                    this.overlapFieldTree.Nodes.Clear();
                    for (int i = 0; i < overlaps.Count; i++)
                        this.overlapFieldTree.Nodes.Add(new TreeNode("OVERLAP #" + i.ToString()));
                    this.overlapFieldTree.SelectedNode = this.overlapFieldTree.Nodes[reselect + 1];
                    overlapFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more overlaps. The maximum number of overlaps allowed per level is 28.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("overlaps"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        private void overlapFieldTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.SelectedOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            RefreshOverlapProperties();
            picture.Invalidate();
        }
        private void overlapFieldInsert_Click(object sender, EventArgs e)
        {
            Point o = new Point(Math.Abs(picture.Left) / zoom, Math.Abs(picture.Top) / zoom);
            Point p = new Point(solidity.PixelCoords[o.Y * 1024 + o.X].X + 2, solidity.PixelCoords[o.Y * 1024 + o.X].Y + 4);
            if (CalculateFreeOverlapSpace() >= 4)
            {
                this.overlapFieldTree.Focus();
                if (overlaps.Count < 28)
                {
                    if (overlapFieldTree.Nodes.Count > 0)
                        overlaps.New(overlapFieldTree.SelectedNode.Index + 1, p);
                    else
                        overlaps.New(0, p);
                    int reselect;
                    if (overlapFieldTree.Nodes.Count > 0)
                        reselect = overlapFieldTree.SelectedNode.Index;
                    else
                        reselect = -1;
                    overlapFieldTree.BeginUpdate();
                    this.overlapFieldTree.Nodes.Clear();
                    for (int i = 0; i < overlaps.Count; i++)
                        this.overlapFieldTree.Nodes.Add(new TreeNode("OVERLAP #" + i.ToString()));
                    this.overlapFieldTree.SelectedNode = this.overlapFieldTree.Nodes[reselect + 1];
                    overlapFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more overlaps. The maximum number of overlaps allowed per level is 28.",
                        "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("overlaps"),
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void overlapFieldDelete_Click(object sender, EventArgs e)
        {
            this.overlapFieldTree.Focus();
            if (this.overlapFieldTree.SelectedNode != null && overlaps.CurrentOverlap == this.overlapFieldTree.SelectedNode.Index)
            {
                overlaps.Remove();
                int reselect = overlapFieldTree.SelectedNode.Index;
                if (reselect == overlapFieldTree.Nodes.Count - 1)
                    reselect--;
                overlapFieldTree.BeginUpdate();
                this.overlapFieldTree.Nodes.Clear();
                for (int i = 0; i < overlaps.Count; i++)
                    this.overlapFieldTree.Nodes.Add(new TreeNode("OVERLAP #" + i.ToString()));
                if (overlapFieldTree.Nodes.Count > 0)
                    this.overlapFieldTree.SelectedNode = this.overlapFieldTree.Nodes[reselect];
                else
                {
                    this.overlapFieldTree.SelectedNode = null;
                    RefreshOverlapProperties();
                }
                overlapFieldTree.EndUpdate();
            }
        }
        private void overlapCoordX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.X = (byte)this.overlapX.Value;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void overlapCoordY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.Y = (byte)this.overlapY.Value;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void overlapCoordZ_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.Z = (byte)this.overlapZ.Value;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void overlapCoordZPlusHalf_CheckedChanged(object sender, EventArgs e)
        {
            overlapCoordZPlusHalf.ForeColor = overlapCoordZPlusHalf.Checked ? Color.Black : Color.Gray;
            if (this.Updating)
                return;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.B1b7 = this.overlapCoordZPlusHalf.Checked;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void overlapType_ValueChanged(object sender, EventArgs e)
        {
            int x = (int)overlapType.Value % 8 * 32;
            int y = (int)overlapType.Value / 8 * 32;
            overlapSelection.Refresh(32, x, y, 32, 32, pictureBoxOverlaps);
            //
            pictureBoxOverlaps.Invalidate();
            picture.Invalidate();
            //
            if (this.Updating)
                return;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
            overlaps.Type = (byte)this.overlapType.Value;
            overlaps.CurrentOverlap = this.overlapFieldTree.SelectedNode.Index;
        }
        private void overlapUnknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            overlaps.B0b7 = overlapUnknownBits.GetItemChecked(0);
            overlaps.B2b5 = overlapUnknownBits.GetItemChecked(1);
            overlaps.B2b6 = overlapUnknownBits.GetItemChecked(2);
            overlaps.B2b7 = overlapUnknownBits.GetItemChecked(3);
        }
        //
        private void pictureBoxOverlaps_MouseDown(object sender, MouseEventArgs e)
        {
            overlapType.Value = (e.Y / 32) * 8 + (e.X / 32);
        }
        private void pictureBoxOverlaps_Paint(object sender, PaintEventArgs e)
        {
            if (overlapsImage == null)
            {
                overlapsPixels = Do.TilesetToPixels(overlapTileset.OverlapTiles, 8, 13, 0);
                overlapsImage = Do.PixelsToImage(overlapsPixels, 256, 416);
            }
            e.Graphics.DrawImage(overlapsImage, 0, 0);
            overlay.DrawTileGrid(e.Graphics, Color.Gray, new Size(256, 416), new Size(32, 32), 1, true);
            if (overlapSelection != null)
                overlapSelection.DrawSelectionBox(e.Graphics, 1);
        }
        //
        private void overlapFieldCopy_Click(object sender, EventArgs e)
        {
            if (overlapFieldTree.SelectedNode != null)
                copyOverlap = overlaps.Overlap.Copy();
        }
        private void overlapFieldPaste_Click(object sender, EventArgs e)
        {
            if (copyOverlap == null)
                return;
            AddNewOverlap((Overlap)copyOverlap);
        }
        private void overlapFieldDuplicate_Click(object sender, EventArgs e)
        {
            AddNewOverlap(overlaps.Overlap.Copy());
        }
        #endregion
    }
}
