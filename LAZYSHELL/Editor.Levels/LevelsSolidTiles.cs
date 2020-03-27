using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class LevelsSolidTiles : NewForm
    {
        #region Variables
        //
        private delegate void Function();
        private Delegate update;
        private int index { get { return (int)physicalTileNum.Value; } }
        public int Index { get { return (int)physicalTileNum.Value; } set { physicalTileNum.Value = value; } }
        private SolidityTile[] solidityTiles { get { return Model.SolidTiles; } set { Model.SolidTiles = value; } }
        private SolidityTile solidityTile { get { return solidityTiles[index]; } set { solidityTiles[index] = value; } }
                private Bitmap solidTileImage;
        private Solidity solids;
        public Solidity Solids { get { return solids; } }
        public SearchSolidTile SearchSolidTile;
        #endregion
        // Constructor
        public LevelsSolidTiles(Solidity solids, Delegate update)
        {
            this.solids = solids;
            this.update = update;
            InitializeComponent();
            RefreshPhysicalTile();
            SearchSolidTile = new SearchSolidTile(this, solidityTiles);
        }
        #region Functions
        public void SetSolidTileImage()
        {
            int[] physicalTilePixels = solids.GetTilePixels(solidityTile);
            solidTileImage = Do.PixelsToImage(physicalTilePixels, 32, 784);
            pictureBoxPhysicalTile.Invalidate();
        }
        private void RefreshPhysicalTile()
        {
            this.Updating = true;
            // SIZE/COORDS;
            heightOfBaseTile.Value = solidityTile.BaseTileHeight;
            heightOverhead.Value = solidityTile.OverheadTileHeight;
            zCoordOverhead.Value = solidityTile.OverheadTileZ;
            zCoordWater.Value = solidityTile.WaterTileZ;
            zCoordPlusHalf.SelectedIndex = (int)(solidityTile.BaseTileHeight_Half ? 1 : 0);
            // SOLID QUADRANTS;
            solidTile.SelectedIndex = (int)(solidityTile.SolidTile ? 1 : 0);
            solidQuadrant.SelectedIndex = (int)(solidityTile.SolidQuadrantFlag ? 1 : 0);
            solidQuadrantN.SelectedIndex = (int)(solidityTile.SolidTopQuadrant ? 1 : 0);
            solidQuadrantW.SelectedIndex = (int)(solidityTile.SolidLeftQuadrant ? 1 : 0);
            solidQuadrantS.SelectedIndex = (int)(solidityTile.SolidBottomQuadrant ? 1 : 0);
            solidQuadrantE.SelectedIndex = (int)(solidityTile.SolidRightQuadrant ? 1 : 0);
            // SOLID EDGES;
            solidEdgeNW.SelectedIndex = (int)(solidityTile.SolidNWEdge ? 1 : 0);
            solidEdgeSW.SelectedIndex = (int)(solidityTile.SolidSWEdge ? 1 : 0);
            solidEdgeNE.SelectedIndex = (int)(solidityTile.SolidNEEdge ? 1 : 0);
            solidEdgeSE.SelectedIndex = (int)(solidityTile.SolidSEEdge ? 1 : 0);
            // PRIORITY 3;
            p3OnEdge.SelectedIndex = (int)(solidityTile.P3ObjectOnEdge ? 1 : 0);
            p3OverEdge.SelectedIndex = (int)(solidityTile.P3ObjectAboveEdge ? 1 : 0);
            p3OnTile.SelectedIndex = (int)(solidityTile.P3ObjectOnTile ? 1 : 0);
            // CONVEYOR BELT;
            conveyor.SelectedIndex = solidityTile.ConveryorBeltDirection;
            conveyorBeltFast.SelectedIndex = (int)(solidityTile.ConveyorBeltFast ? 1 : 0);
            conveyorBeltNormal.SelectedIndex = (int)(solidityTile.ConveyorBeltNormal ? 1 : 0);
            // OTHER;
            stairs.SelectedIndex = solidityTile.StairsDirection;
            specialTile.SelectedIndex = solidityTile.SpecialTileFormat;
            doorFormat.SelectedIndex = solidityTile.Door;
            SetSolidTileImage();
            this.Updating = false;
        }
        #endregion
        #region Event handlers
        private void physicalTileNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshPhysicalTile();
        }
        private void physicalTileSearchButton_Click(object sender, System.EventArgs e)
        {
            SearchSolidTile.Show();
            SearchSolidTile.BringToFront();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current solidity tile. Go ahead with reset?",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            solidityTile = new SolidityTile(index);
            physicalTileNum_ValueChanged(null, null);
        }
        private void pictureBoxPhysicalTile_Paint(object sender, PaintEventArgs e)
        {
            if (solidTileImage != null)
                e.Graphics.DrawImage(solidTileImage, 0, 0);
        }
        private void LevelsPhysicalTiles_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        //
        private void heightOfBaseTile_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.BaseTileHeight = (byte)heightOfBaseTile.Value;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void zCoordOverhead_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.OverheadTileZ = (byte)zCoordOverhead.Value;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void heightOverhead_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.OverheadTileHeight = (byte)heightOverhead.Value;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void zCoordWater_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.WaterTileZ = (byte)zCoordWater.Value;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void zCoordPlusHalf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.BaseTileHeight_Half = zCoordPlusHalf.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidTile = solidTile.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidQuadrant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidQuadrantFlag = solidQuadrant.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidQuadrantN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidTopQuadrant = solidQuadrantN.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidQuadrantW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidLeftQuadrant = solidQuadrantW.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidQuadrantE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidRightQuadrant = solidQuadrantE.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidQuadrantS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidBottomQuadrant = solidQuadrantS.SelectedIndex == 1;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void solidEdgeNW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidNWEdge = solidEdgeNW.SelectedIndex == 1;
        }
        private void solidEdgeNE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidNEEdge = solidEdgeNE.SelectedIndex == 1;
        }
        private void solidEdgeSW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidSWEdge = solidEdgeSW.SelectedIndex == 1;
        }
        private void solidEdgeSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SolidSEEdge = solidEdgeSE.SelectedIndex == 1;
        }
        private void p3OnEdge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.P3ObjectOnEdge = p3OnEdge.SelectedIndex == 1;
        }
        private void p3OverEdge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.P3ObjectAboveEdge = p3OverEdge.SelectedIndex == 1;
        }
        private void p3OnTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.P3ObjectOnTile = p3OnTile.SelectedIndex == 1;
        }
        private void conveyor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.ConveryorBeltDirection = (byte)conveyor.SelectedIndex;
        }
        private void conveyorBeltFast_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.ConveyorBeltFast = conveyorBeltFast.SelectedIndex == 1;
        }
        private void conveyorBeltNormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.ConveyorBeltNormal = conveyorBeltNormal.SelectedIndex == 1;
        }
        private void stairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.StairsDirection = (byte)stairs.SelectedIndex;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void specialTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.SpecialTileFormat = (byte)specialTile.SelectedIndex;
            SetSolidTileImage();
            update.DynamicInvoke();
        }
        private void doorFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.Door = (byte)doorFormat.SelectedIndex;
        }
        private void unknownBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            solidityTile.B5b0 = unknownBits.GetItemChecked(0);
            solidityTile.B5b1 = unknownBits.GetItemChecked(1);
            solidityTile.B5b2 = unknownBits.GetItemChecked(2);
            solidityTile.B5b3 = unknownBits.GetItemChecked(3);
            solidityTile.B5b4 = unknownBits.GetItemChecked(4);
        }
        //
        private void conditional_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Color foreColor = e.Index == 1 ? Color.Blue : Color.Red;
            StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
            e.DrawBackground();
            e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), e.Font, new SolidBrush(foreColor), e.Bounds, stringFormat);
            e.DrawFocusRectangle();
        }
        #endregion
    }
}
