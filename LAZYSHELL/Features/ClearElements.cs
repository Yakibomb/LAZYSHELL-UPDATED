using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;

namespace LAZYSHELL
{
    public partial class ClearElements : NewForm
    {
        private object element;
        private int currentIndex;
        private int start = 0;
        private int end = 0;
        private Type type = null;
        // constructor
        public ClearElements(object element, int currentIndex, string title)
        {
            this.element = element;
            this.currentIndex = currentIndex;
            this.start = this.end = currentIndex;
            if (element != null)
                this.type = element.GetType();
            InitializeComponent();
            this.Text = title;
            if (type != null)
                toIndex.Value = toIndex.Maximum = ((object[])element).Length - 1;
            else if (type == null && this.Text == "CLEAR LEVEL DATA...")
                toIndex.Value = toIndex.Maximum = Model.Levels.Length - 1;
            else if (type == null && this.Text == "CLEAR TILESETS...")
                toIndex.Value = toIndex.Maximum = Model.Tilesets.Length - 1;
            else if (type == null && this.Text == "CLEAR TILEMAPS...")
                toIndex.Value = toIndex.Maximum = Model.Tilemaps.Length - 1;
            else if (type == null && this.Text == "CLEAR SOLIDITY MAPS...")
                toIndex.Value = toIndex.Maximum = Model.SolidityMaps.Length - 1;
            else if (type == null && this.Text == "CLEAR BATTLEFIELD TILESETS...")
                toIndex.Value = toIndex.Maximum = Model.TilesetsBF.Length - 1;
            start = end = currentIndex;
        }
        // event handlers
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                fromIndex.Enabled = false;
                toIndex.Enabled = false;
                start = end = currentIndex;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                fromIndex.Enabled = true;
                toIndex.Enabled = true;
                start = (int)fromIndex.Value;
                end = (int)toIndex.Value;
            }
        }
        private void fromDialogue_ValueChanged(object sender, EventArgs e)
        {
            toIndex.Minimum = fromIndex.Value;
            start = (int)fromIndex.Value;
        }
        private void toDialogue_ValueChanged(object sender, EventArgs e)
        {
            fromIndex.Maximum = toIndex.Value;
            end = (int)toIndex.Value;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            for (int i = start; type != null && i <= end; i++)
                ((Element[])element)[i].Clear();
            if (type == null && this.Text == "CLEAR LEVEL DATA...")
            {
                for (int i = start; i <= end; i++)
                {
                    Model.Levels[i].Layer.Clear();
                    Model.Levels[i].LevelEvents.Clear();
                    Model.Levels[i].LevelExits.Clear();
                    Model.Levels[i].LevelNPCs.Clear();
                    Model.Levels[i].LevelOverlaps.Clear();
                    int levelMap = Model.Levels[i].LevelMap;
                    Model.LevelMaps[levelMap].Clear();
                }
            }
            if (type == null && this.Text == "CLEAR TILESETS...")
            {
                for (int i = start; i <= end; i++)
                {
                    if (i < 0x20)
                        Model.Tilesets[i] = new byte[0x1000];
                    else
                        Model.Tilesets[i] = new byte[0x2000];
                    Model.EditTilesets[i] = true;
                }
            }
            if (type == null && this.Text == "CLEAR TILEMAPS...")
            {
                for (int i = start; i <= end; i++)
                {
                    if (i < 0x40)
                        Model.Tilemaps[i] = new byte[0x1000];
                    else
                        Model.Tilemaps[i] = new byte[0x2000];
                    Model.EditTilemaps[i] = true;
                }
            }
            if (type == null && this.Text == "CLEAR SOLIDITY MAPS...")
            {
                for (int i = start; i <= end; i++)
                {
                    Model.SolidityMaps[i] = new byte[0x20C2];
                    Model.EditSolidityMaps[i] = true;
                }
            }
            if (type == null && this.Text == "CLEAR BATTLEFIELD TILESETS...")
            {
                for (int i = start; i <= end; i++)
                {
                    Model.TilesetsBF[i] = new byte[0x2000];
                    Model.EditTilesetsBF[i] = true;
                }
            }
            this.Tag = new Point(start, end);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
