using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class SearchSolidTile : NewForm
    {
        private LevelsSolidTiles levelsSolidTiles;
        private SolidityTile[] solidTiles;
        // constructor
        public SearchSolidTile(LevelsSolidTiles levelsSolidTiles, SolidityTile[] solidTiles)
        {
            this.levelsSolidTiles = levelsSolidTiles;
            this.solidTiles = solidTiles;
            InitializeComponent();
            conveyor.SelectedIndex = 0;
            conveyorBeltFast.SelectedIndex = 0;
            conveyorBeltNormal.SelectedIndex = 0;
            doorFormat.SelectedIndex = 0;
            p3OnEdge.SelectedIndex = 0;
            p3OnTile.SelectedIndex = 0;
            p3OverEdge.SelectedIndex = 0;
            solidEdgeNE.SelectedIndex = 0;
            solidEdgeNW.SelectedIndex = 0;
            solidEdgeSE.SelectedIndex = 0;
            solidEdgeSW.SelectedIndex = 0;
            solidQuadrant.SelectedIndex = 0;
            solidQuadrantE.SelectedIndex = 0;
            solidQuadrantN.SelectedIndex = 0;
            solidQuadrantS.SelectedIndex = 0;
            solidQuadrantW.SelectedIndex = 0;
            solidTile.SelectedIndex = 0;
            specialTile.SelectedIndex = 0;
            stairs.SelectedIndex = 0;
            zCoordPlusHalf.SelectedIndex = 0;
        }
        // functions
        private void LoadSearch()
        {
            searchResults.Items.Clear();
            string withProp = "";
            bool notFound;
            int results = 0;
            if (checkHeightOfBaseTile.Checked) withProp += "Height of base tile = " + heightOfBaseTile.Value.ToString() + "\n";
            if (checkHeightOverhead.Checked) withProp += "Height overhead tile = " + heightOverhead.Value.ToString() + "\n";
            if (checkZCoordOverhead.Checked) withProp += "Z Coord of overhead tile = " + zCoordOverhead.Value.ToString() + "\n";
            if (checkZCoordWater.Checked) withProp += "Z Coord of water tile = " + zCoordWater.Value.ToString() + "\n";
            if (checkZCoordPlusHalf.Checked) withProp += "Z Coord + 1/2\n";
            if (checkSolidTile.Checked) withProp += "Solid tile\n";
            if (checkSolidQuadrant.Checked) withProp += "Solid quadrant flag\n";
            if (checkSolidQuadrantN.Checked) withProp += "N quadrant is solid\n";
            if (checkSolidQuadrantW.Checked) withProp += "W quadrant is solid\n";
            if (checkSolidQuadrantE.Checked) withProp += "E quadrant is solid\n";
            if (checkSolidQuadrantS.Checked) withProp += "S quadrant is solid\n";
            if (checkSolidEdgeNW.Checked) withProp += "NW edge is solid\n";
            if (checkSolidEdgeNE.Checked) withProp += "NE edge is solid\n";
            if (checkSolidEdgeSW.Checked) withProp += "SW edge is solid\n";
            if (checkSolidEdgeSE.Checked) withProp += "SE edge is solid\n";
            if (checkP3OnEdge.Checked) withProp += "Priority 3 for objects on tile edge\n";
            if (checkP3OverEdge.Checked) withProp += "Priority 3 for objects above tile edge\n";
            if (checkP3OnTile.Checked) withProp += "Priority 3 for objects on tile\n";
            if (checkConveyor.Checked) withProp += "Conveyor belt runs..." + conveyor.SelectedItem.ToString() + "\n";
            if (checkConveyorBeltFast.Checked) withProp += "Conveyor belt, fast\n";
            if (checkConveyorBeltNormal.Checked) withProp += "Conveyor belt, normal\n";
            if (checkStairs.Checked) withProp += "Stairs lead..." + stairs.SelectedItem.ToString() + "\n";
            if (checkSpecialTile.Checked) withProp += "Special tile format..." + specialTile.SelectedItem.ToString() + "\n";
            if (checkDoorFormat.Checked) withProp += "Door format..." + doorFormat.SelectedItem.ToString() + "\n";
            if (unknownBits.GetItemChecked(0)) withProp += "Byte 5, bit 0 set\n";
            if (unknownBits.GetItemChecked(1)) withProp += "Byte 5, bit 1 set\n";
            if (unknownBits.GetItemChecked(2)) withProp += "Byte 5, bit 2 set\n";
            if (unknownBits.GetItemChecked(3)) withProp += "Byte 5, bit 3 set\n";
            if (unknownBits.GetItemChecked(4)) withProp += "Byte 5, bit 4 set\n";
            for (int i = 0; i < solidTiles.Length; i++)
            {
                notFound = false;
                // SIZE/COORDS
                if (checkHeightOfBaseTile.Checked) { if (heightOfBaseTile.Value != solidTiles[i].BaseTileHeight) notFound = true; }
                if (checkHeightOverhead.Checked) { if (heightOverhead.Value != solidTiles[i].OverheadTileHeight) notFound = true; }
                if (checkZCoordOverhead.Checked) { if (zCoordOverhead.Value != solidTiles[i].OverheadTileZ) notFound = true; }
                if (checkZCoordWater.Checked) { if (zCoordWater.Value != solidTiles[i].WaterTileZ) notFound = true; }
                if (checkZCoordPlusHalf.Checked)
                {
                    if (zCoordPlusHalf.SelectedIndex != (int)(solidTiles[i].BaseTileHeight_Half ? 1 : 0))
                        notFound = true;
                }
                // SOLID QUADRANTS
                if (checkSolidTile.Checked)
                {
                    if (solidTile.SelectedIndex != (int)(solidTiles[i].SolidTile ? 1 : 0)) notFound = true;
                }
                if (checkSolidQuadrant.Checked)
                {
                    if (solidQuadrant.SelectedIndex != (int)(solidTiles[i].SolidQuadrantFlag ? 1 : 0)) notFound = true;
                }
                if (checkSolidQuadrantN.Checked)
                {
                    if (solidQuadrantN.SelectedIndex != (int)(solidTiles[i].SolidTopQuadrant ? 1 : 0)) notFound = true;
                }
                if (checkSolidQuadrantW.Checked)
                {
                    if (solidQuadrantW.SelectedIndex != (int)(solidTiles[i].SolidLeftQuadrant ? 1 : 0)) notFound = true;
                }
                if (checkSolidQuadrantS.Checked)
                {
                    if (solidQuadrantS.SelectedIndex != (int)(solidTiles[i].SolidBottomQuadrant ? 1 : 0)) notFound = true;
                }
                if (checkSolidQuadrantE.Checked)
                {
                    if (solidQuadrantE.SelectedIndex != (int)(solidTiles[i].SolidRightQuadrant ? 1 : 0)) notFound = true;
                }
                // SOLID EDGES
                if (checkSolidEdgeNW.Checked)
                {
                    if (solidEdgeNW.SelectedIndex != (int)(solidTiles[i].SolidNWEdge ? 1 : 0)) notFound = true;
                }
                if (checkSolidEdgeSW.Checked)
                {
                    if (solidEdgeSW.SelectedIndex != (int)(solidTiles[i].SolidSWEdge ? 1 : 0)) notFound = true;
                }
                if (checkSolidEdgeNE.Checked)
                {
                    if (solidEdgeNE.SelectedIndex != (int)(solidTiles[i].SolidNEEdge ? 1 : 0)) notFound = true;
                }
                if (checkSolidEdgeSE.Checked)
                {
                    if (solidEdgeSE.SelectedIndex != (int)(solidTiles[i].SolidSEEdge ? 1 : 0)) notFound = true;
                }
                // PRIORITY 3
                if (checkP3OnEdge.Checked)
                {
                    if (p3OnEdge.SelectedIndex != (int)(solidTiles[i].P3ObjectOnEdge ? 1 : 0)) notFound = true;
                }
                if (checkP3OverEdge.Checked)
                {
                    if (p3OverEdge.SelectedIndex != (int)(solidTiles[i].P3ObjectAboveEdge ? 1 : 0)) notFound = true;
                }
                if (checkP3OnTile.Checked)
                {
                    if (p3OnTile.SelectedIndex != (int)(solidTiles[i].P3ObjectOnTile ? 1 : 0)) notFound = true;
                }
                // CONVEYOR BELT
                if (checkConveyor.Checked) { if (solidTiles[i].ConveryorBeltDirection != conveyor.SelectedIndex) notFound = true; }
                if (checkConveyorBeltFast.Checked)
                {
                    if (conveyorBeltFast.SelectedIndex != (int)(solidTiles[i].ConveyorBeltFast ? 1 : 0)) notFound = true;
                }
                if (checkConveyorBeltNormal.Checked)
                {
                    if (conveyorBeltNormal.SelectedIndex != (int)(solidTiles[i].ConveyorBeltNormal ? 1 : 0)) notFound = true;
                }
                // OTHER
                if (checkStairs.Checked) { if (solidTiles[i].StairsDirection != stairs.SelectedIndex) notFound = true; }
                if (checkSpecialTile.Checked) { if (solidTiles[i].SpecialTileFormat != specialTile.SelectedIndex) notFound = true; }
                if (checkDoorFormat.Checked) { if (doorFormat.SelectedIndex != solidTiles[i].Door) notFound = true; }
                if (!notFound)
                {
                    searchResults.Items.Add("Solid Tile #" + i.ToString());
                    results++;
                }
            }
            if (withProp.Length > 0)
                withProp += "\n";
            withProp += results.ToString() + " results";
            withProperties.Text = withProp;
        }
        // event handlers
        private void selectAll_Click(object sender, EventArgs e)
        {
            Do.SelectAll(panel1, true);
        }
        private void deselectAll_Click(object sender, EventArgs e)
        {
            Do.SelectAll(panel1, false);
        }
        private void checkControl_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Name.Substring(5, 1).ToLower() + checkBox.Name.Substring(6);
            Control control = this.Controls.Find(name, true)[0];
            control.Enabled = checkBox.Checked;
        }
        private void searchButton_Click(object sender, System.EventArgs e)
        {
            LoadSearch();
        }
        private void searchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchResults.SelectedItem == null)
                return;
            levelsSolidTiles.Index = Convert.ToInt32(searchResults.SelectedItem.ToString().Substring(12));
        }
        private void SearchPhysicalTile_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}