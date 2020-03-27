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
        private LevelExits exits { get { return level.LevelExits; } set { level.LevelExits = value; } } // Exits for the current level
        private Object copyExit;
        public TreeView ExitsFieldTree { get { return exitsFieldTree; } set { exitsFieldTree = value; } }
        public NumericUpDown ExitX { get { return exitX; } set { exitX = value; } }
        public NumericUpDown ExitY { get { return exitY; } set { exitY = value; } }
        #endregion
        #region Methods
        private void InitializeExitFieldProperties()
        {
            this.Updating = true;
            this.exitsFieldTree.Nodes.Clear();
            for (int i = 0; i < exits.Count; i++)
            {
                this.exitsFieldTree.Nodes.Add(new TreeNode("EXIT #" + i.ToString()));
            }
            if (exitsFieldTree.Nodes.Count > 0)
                this.exitsFieldTree.SelectedNode = this.exitsFieldTree.Nodes[0];
            if (exits.Count != 0 && this.exitsFieldTree.SelectedNode != null)
            {
                exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
                this.exitType.SelectedIndex = exits.ExitType;
                SetExitDestinationItems();
                this.exitDest.SelectedIndex = exits.Destination;
                this.exitsShowMessage.Checked = exits.ShowMessage;
                this.exitX.Value = exits.X;
                this.exitY.Value = exits.Y;
                this.exitZ.Value = exits.Z;
                this.exitFace.SelectedIndex = exits.F;
                this.exitLength.Value = exits.Width + 1;
                this.exitHeight.Value = exits.Height;
                this.exits45LengthPlusHalf.Checked = exits.X_Half;
                this.exits135LengthPlusHalf.Checked = exits.Y_Half;
                this.exitDestX.Value = exits.DstX;
                this.exitDestY.Value = exits.DstY;
                this.exitDestZ.Value = exits.DstZ;
                this.exitDestFace.SelectedIndex = exits.DstF;
                this.marioZCoordPlusHalf.Checked = exits.DstYb7;
                foreach (ToolStripItem item in toolStrip5.Items)
                    item.Enabled = true;
                this.exitDest.Enabled = true;
                this.exitsShowMessage.Enabled = true;
                this.exitType.Enabled = true;
                this.exitX.Enabled = true;
                this.exitY.Enabled = true;
                this.exitZ.Enabled = true;
                this.exitFace.Enabled = true;
                this.exitLength.Enabled = true;
                this.exitHeight.Enabled = true;
                this.exits45LengthPlusHalf.Enabled = true;
                this.exits135LengthPlusHalf.Enabled = true;
                if (this.exitType.SelectedIndex == 0)
                {
                    this.exitDestX.Enabled = true;
                    this.exitDestY.Enabled = true;
                    this.exitDestZ.Enabled = true;
                    this.exitDestFace.Enabled = true;
                    this.marioZCoordPlusHalf.Enabled = true;
                }
                else
                {
                    this.exitDestX.Enabled = false;
                    this.exitDestY.Enabled = false;
                    this.exitDestZ.Enabled = false;
                    this.exitDestFace.Enabled = false;
                    this.marioZCoordPlusHalf.Enabled = false;
                }
            }
            else
            {
                foreach (ToolStripItem item in toolStrip5.Items)
                    if (item != exitsInsertField)
                        item.Enabled = false;
                this.exitDest.Enabled = false;
                this.exitsShowMessage.Enabled = false;
                this.exitType.Enabled = false;
                this.exitX.Enabled = false;
                this.exitY.Enabled = false;
                this.exitZ.Enabled = false;
                this.exitFace.Enabled = false;
                this.exitLength.Enabled = false;
                this.exitHeight.Enabled = false;
                this.exits45LengthPlusHalf.Enabled = false;
                this.exits135LengthPlusHalf.Enabled = false;
                this.exitDestX.Enabled = false;
                this.exitDestY.Enabled = false;
                this.exitDestZ.Enabled = false;
                this.exitDestFace.Enabled = false;
                this.marioZCoordPlusHalf.Enabled = false;
                this.exitType.SelectedIndex = 0;
                this.exitDest.SelectedIndex = 0;
                this.exitsShowMessage.Checked = false;
                this.exitX.Value = 0;
                this.exitY.Value = 0;
                this.exitZ.Value = 0;
                this.exitFace.SelectedIndex = 0;
                this.exitLength.Value = 1;
                this.exitHeight.Value = 0;
                this.exits45LengthPlusHalf.Checked = false;
                this.exits135LengthPlusHalf.Checked = false;
                this.exitDestX.Value = 0;
                this.exitDestY.Value = 0;
                this.exitDestZ.Value = 0;
                this.exitDestFace.SelectedIndex = 0;
                this.marioZCoordPlusHalf.Checked = false;
            }
            exitsBytesLeft.Text = CalculateFreeExitSpace() + " bytes left";
            exitsBytesLeft.BackColor = CalculateFreeExitSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void RefreshExitFieldProperties()
        {
            this.Updating = true;
            if (exits.Count != 0 && this.exitsFieldTree.SelectedNode != null)
            {
                exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
                this.exitType.SelectedIndex = exits.ExitType;
                SetExitDestinationItems();
                this.exitDest.SelectedIndex = exits.Destination;
                this.exitsShowMessage.Checked = exits.ShowMessage;
                this.exitX.Value = exits.X;
                this.exitY.Value = exits.Y;
                this.exitZ.Value = exits.Z;
                this.exitFace.SelectedIndex = exits.F;
                this.exitLength.Value = exits.Width + 1;
                this.exitHeight.Value = exits.Height;
                this.exits45LengthPlusHalf.Checked = exits.X_Half;
                this.exits135LengthPlusHalf.Checked = exits.Y_Half;
                this.exitDestX.Value = exits.DstX;
                this.exitDestY.Value = exits.DstY;
                this.exitDestZ.Value = exits.DstZ;
                this.exitDestFace.SelectedIndex = exits.DstF;
                this.marioZCoordPlusHalf.Checked = exits.DstYb7;
                foreach (ToolStripItem item in toolStrip5.Items)
                    item.Enabled = true;
                this.exitDest.Enabled = true;
                this.exitsShowMessage.Enabled = true;
                this.exitType.Enabled = true;
                this.exitX.Enabled = true;
                this.exitY.Enabled = true;
                this.exitZ.Enabled = true;
                this.exitFace.Enabled = true;
                this.exitLength.Enabled = true;
                this.exitHeight.Enabled = true;
                this.exits45LengthPlusHalf.Enabled = true;
                this.exits135LengthPlusHalf.Enabled = true;
                if (this.exitType.SelectedIndex == 0)
                {
                    this.exitDestX.Enabled = true;
                    this.exitDestY.Enabled = true;
                    this.exitDestZ.Enabled = true;
                    this.exitDestFace.Enabled = true;
                    this.marioZCoordPlusHalf.Enabled = true;
                }
                else
                {
                    this.exitDestX.Enabled = false;
                    this.exitDestY.Enabled = false;
                    this.exitDestZ.Enabled = false;
                    this.exitDestFace.Enabled = false;
                    this.marioZCoordPlusHalf.Enabled = false;
                }
            }
            else
            {
                foreach (ToolStripItem item in toolStrip5.Items)
                    if (item != exitsInsertField)
                        item.Enabled = false;
                this.exitDest.Enabled = false;
                this.exitsShowMessage.Enabled = false;
                this.exitType.Enabled = false;
                this.exitX.Enabled = false;
                this.exitY.Enabled = false;
                this.exitZ.Enabled = false;
                this.exitFace.Enabled = false;
                this.exitLength.Enabled = false;
                this.exitHeight.Enabled = false;
                this.exits45LengthPlusHalf.Enabled = false;
                this.exits135LengthPlusHalf.Enabled = false;
                this.exitDestX.Enabled = false;
                this.exitDestY.Enabled = false;
                this.exitDestZ.Enabled = false;
                this.exitDestFace.Enabled = false;
                this.marioZCoordPlusHalf.Enabled = false;
                this.exitType.SelectedIndex = 0;
                this.exitDest.SelectedIndex = 0;
                this.exitsShowMessage.Checked = false;
                this.exitX.Value = 0;
                this.exitY.Value = 0;
                this.exitZ.Value = 0;
                this.exitFace.SelectedIndex = 0;
                this.exitLength.Value = 1;
                this.exitHeight.Value = 0;
                this.exits45LengthPlusHalf.Checked = false;
                this.exits135LengthPlusHalf.Checked = false;
                this.exitDestX.Value = 0;
                this.exitDestY.Value = 0;
                this.exitDestZ.Value = 0;
                this.exitDestFace.SelectedIndex = 0;
                this.marioZCoordPlusHalf.Checked = false;
            }
            exitsBytesLeft.Text = CalculateFreeExitSpace() + " bytes left";
            exitsBytesLeft.BackColor = CalculateFreeExitSpace() >= 0 ? SystemColors.Control : Color.Red;
            this.Updating = false;
        }
        private void SetExitDestinationItems()
        {
            this.exitDest.Items.Clear();
            if (this.exitType.SelectedIndex == 0)
            {
                this.exitDest.DropDownWidth = 490;
                this.exitDest.Items.AddRange(Lists.Numerize(Lists.LevelNames));
            }
            else
            {
                this.exitDest.DropDownWidth = 200;
                this.exitDest.Items.AddRange(Lists.Numerize(Model.Locations));
            }
        }
        public int CalculateFreeExitSpace()
        {
            int used = 0;
            foreach (Level level in levels)
            {
                foreach (Exit exit in level.LevelExits.Exits)
                    used += exit.Length;
            }
            return 0x179F - used;
        }
        private void AddNewExit(Exit exit)
        {
            if (CalculateFreeExitSpace() >= 8)
            {
                this.exitsFieldTree.Focus();
                if (exits.Count < 28)
                {
                    if (exitsFieldTree.Nodes.Count > 0)
                        exits.New(exitsFieldTree.SelectedNode.Index + 1, exit);
                    else
                        exits.New(0, exit);
                    int reselect;
                    if (exitsFieldTree.Nodes.Count > 0)
                        reselect = exitsFieldTree.SelectedNode.Index;
                    else
                        reselect = -1;
                    exitsFieldTree.BeginUpdate();
                    this.exitsFieldTree.Nodes.Clear();
                    for (int i = 0; i < exits.Count; i++)
                        this.exitsFieldTree.Nodes.Add(new TreeNode("EXIT #" + i.ToString()));
                    this.exitsFieldTree.SelectedNode = this.exitsFieldTree.Nodes[reselect + 1];
                    exitsFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more exit fields. The maximum number of exit fields allowed per level is 28.",
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("exits"),
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Event Handlers
        private void exitsFieldTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.SelectedExit = this.exitsFieldTree.SelectedNode.Index;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            RefreshExitFieldProperties();
            picture.Invalidate();
        }
        private void exits45LengthPlusHalf_CheckedChanged(object sender, EventArgs e)
        {
            if (exits45LengthPlusHalf.Checked) exits45LengthPlusHalf.ForeColor = Color.Black;
            else exits45LengthPlusHalf.ForeColor = Color.Gray;
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.X_Half = this.exits45LengthPlusHalf.Checked;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            RefreshExitFieldProperties();
        }
        private void exits135LengthPlusHalf_CheckedChanged(object sender, EventArgs e)
        {
            if (exits135LengthPlusHalf.Checked) exits135LengthPlusHalf.ForeColor = Color.Black;
            else exits135LengthPlusHalf.ForeColor = Color.Gray;
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.Y_Half = this.exits135LengthPlusHalf.Checked;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            RefreshExitFieldProperties();
        }
        private void exitsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (CalculateFreeExitSpace() >= 0)
            {
                exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
                exits.ExitType = (byte)this.exitType.SelectedIndex;
                if (exits.Destination > exitType.Items.Count)
                    exits.Destination = (ushort)(exitType.Items.Count - 1);
                RefreshExitFieldProperties();
            }
            else
            {
                this.exitType.SelectedIndex = 1;
            }
            picture.Invalidate();
        }
        private void exitsMarioZCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstZ = (byte)this.exitDestZ.Value;
        }
        private void exitsMarioYCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstY = (byte)this.exitDestY.Value;
        }
        private void exitsMarioXCoord_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstX = (byte)this.exitDestX.Value;
        }
        private void marioZCoordPlusHalf_CheckedChanged(object sender, System.EventArgs e)
        {
            if (marioZCoordPlusHalf.Checked) marioZCoordPlusHalf.ForeColor = Color.Black;
            else marioZCoordPlusHalf.ForeColor = Color.Gray;
            if (this.Updating)
                return;
            exits.DstYb7 = this.marioZCoordPlusHalf.Checked;
        }
        private void exitsFieldHeight_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.Height = (byte)this.exitHeight.Value;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsFieldLength_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.Width = (byte)(this.exitLength.Value - 1);

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsMarioRadialPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.DstF = (byte)this.exitDestFace.SelectedIndex;
        }
        private void exitsZ_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.Z = (byte)this.exitZ.Value;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsY_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.Y = (byte)this.exitY.Value;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsX_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.X = (byte)this.exitX.Value;

            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsFace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            exits.F = (byte)this.exitFace.SelectedIndex;
            exits.CurrentExit = this.exitsFieldTree.SelectedNode.Index;
            picture.Invalidate();
        }
        private void exitsShowMessage_CheckedChanged(object sender, System.EventArgs e)
        {
            if (exitsShowMessage.Checked) exitsShowMessage.ForeColor = Color.Black;
            else exitsShowMessage.ForeColor = Color.Gray;
            if (this.Updating)
                return;
            exits.ShowMessage = this.exitsShowMessage.Checked;
        }
        private void exitsDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            exits.Destination = (ushort)this.exitDest.SelectedIndex;
            picture.Invalidate();
        }
        private void exitsInsertField_Click(object sender, EventArgs e)
        {
            Point o = new Point(Math.Abs(picture.Left) / zoom, Math.Abs(picture.Top) / zoom);
            Point p = new Point(solidity.PixelCoords[o.Y * 1024 + o.X].X + 2, solidity.PixelCoords[o.Y * 1024 + o.X].Y + 4);
            if (CalculateFreeExitSpace() >= 8)
            {
                this.exitsFieldTree.Focus();
                if (exits.Count < 28)
                {
                    if (exitsFieldTree.Nodes.Count > 0)
                        exits.New(exitsFieldTree.SelectedNode.Index + 1, p);
                    else
                        exits.New(0, p);
                    int reselect;
                    if (exitsFieldTree.Nodes.Count > 0)
                        reselect = exitsFieldTree.SelectedNode.Index;
                    else
                        reselect = -1;
                    exitsFieldTree.BeginUpdate();
                    this.exitsFieldTree.Nodes.Clear();
                    for (int i = 0; i < exits.Count; i++)
                        this.exitsFieldTree.Nodes.Add(new TreeNode("EXIT #" + i.ToString()));
                    this.exitsFieldTree.SelectedNode = this.exitsFieldTree.Nodes[reselect + 1];
                    exitsFieldTree.EndUpdate();
                }
                else
                    MessageBox.Show("Could not insert any more exit fields. The maximum number of exit fields allowed per level is 28.",
                        "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Could not insert the field. " + MaximumSpaceExceeded("exits"),
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void exitsDeleteField_Click(object sender, EventArgs e)
        {
            this.exitsFieldTree.Focus();
            if (this.exitsFieldTree.SelectedNode != null && exits.CurrentExit == this.exitsFieldTree.SelectedNode.Index)
            {
                exits.Remove();
                int reselect = exitsFieldTree.SelectedNode.Index;
                if (reselect == exitsFieldTree.Nodes.Count - 1)
                    reselect--;
                exitsFieldTree.BeginUpdate();
                this.exitsFieldTree.Nodes.Clear();
                for (int i = 0; i < exits.Count; i++)
                    this.exitsFieldTree.Nodes.Add(new TreeNode("EXIT #" + i.ToString()));
                if (exitsFieldTree.Nodes.Count > 0)
                    this.exitsFieldTree.SelectedNode = this.exitsFieldTree.Nodes[reselect];
                else
                {
                    this.exitsFieldTree.SelectedNode = null;

                    RefreshExitFieldProperties();
                    picture.Invalidate();
                }
                exitsFieldTree.EndUpdate();
            }
        }
        private void exitsCopyField_Click(object sender, EventArgs e)
        {
            if (exitsFieldTree.SelectedNode != null)
                copyExit = exits.Exit.Copy();
        }
        private void exitsPasteField_Click(object sender, EventArgs e)
        {
            if (copyExit == null)
                return;
            AddNewExit((Exit)copyExit);
        }
        private void exitsDuplicateField_Click(object sender, EventArgs e)
        {
            AddNewExit(exits.Exit.Copy());
        }
        #endregion
    }
}
