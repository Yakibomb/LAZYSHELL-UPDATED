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
        private LevelLayer layer { get { return level.Layer; } set { level.Layer = value; } }
        public LevelLayer Layer { get { return layer; } }// Layer for the current level
        public NumericUpDown LayerMaskHighX { get { return layerMaskHighX; } set { layerMaskHighX = value; } }
        public NumericUpDown LayerMaskHighY { get { return layerMaskHighY; } set { layerMaskHighY = value; } }
        public NumericUpDown LayerMaskLowX { get { return layerMaskLowX; } set { layerMaskLowX = value; } }
        public NumericUpDown LayerMaskLowY { get { return layerMaskLowY; } set { layerMaskLowY = value; } }
        #endregion
        #region Methods
        private void InitializeLayerProperties()
        {
            this.layerMessageBox.SelectedIndex = layer.MessageBox;
            this.layerPrioritySet.Value = layer.PrioritySet;
            this.layerMainscreenL1.Checked = prioritySets[layer.PrioritySet].MainscreenL1;
            this.layerMainscreenL2.Checked = prioritySets[layer.PrioritySet].MainscreenL2;
            this.layerMainscreenL3.Checked = prioritySets[layer.PrioritySet].MainscreenL3;
            this.layerMainscreenNPC.Checked = prioritySets[layer.PrioritySet].MainscreenOBJ;
            this.layerSubscreenL1.Checked = prioritySets[layer.PrioritySet].SubscreenL1;
            this.layerSubscreenL2.Checked = prioritySets[layer.PrioritySet].SubscreenL2;
            this.layerSubscreenL3.Checked = prioritySets[layer.PrioritySet].SubscreenL3;
            this.layerSubscreenNPC.Checked = prioritySets[layer.PrioritySet].SubscreenOBJ;
            this.layerColorMathL1.Checked = prioritySets[layer.PrioritySet].ColorMathL1;
            this.layerColorMathL2.Checked = prioritySets[layer.PrioritySet].ColorMathL2;
            this.layerColorMathL3.Checked = prioritySets[layer.PrioritySet].ColorMathL3;
            this.layerColorMathNPC.Checked = prioritySets[layer.PrioritySet].ColorMathOBJ;
            this.layerColorMathBG.Checked = prioritySets[layer.PrioritySet].ColorMathBG;
            this.layerColorMathIntensity.SelectedIndex = prioritySets[layer.PrioritySet].ColorMathHalfIntensity;
            this.layerColorMathMode.SelectedIndex = prioritySets[layer.PrioritySet].ColorMathMinusSubscreen;
            this.layerMaskHighX.Value = layer.MaskHighX;
            this.layerMaskHighY.Value = layer.MaskHighY;
            this.layerMaskLowX.Value = layer.MaskLowX;
            this.layerMaskLowY.Value = layer.MaskLowY;
            this.layerL2UpShift.Value = layer.YNegL2;
            this.layerL2LeftShift.Value = layer.XNegL2;
            this.layerL3UpShift.Value = layer.YNegL3;
            this.layerL3LeftShift.Value = layer.XNegL3;
            this.layerInfiniteAutoscroll.Checked = layer.InfiniteScrolling;
            this.layerLockMask.Checked = layer.MaskLock;
            this.layerScrollWrapping.SetItemChecked(0, layer.ScrollWrapL1_HZ);
            this.layerScrollWrapping.SetItemChecked(1, layer.ScrollWrapL1_VT);
            this.layerScrollWrapping.SetItemChecked(2, layer.ScrollWrapL2_HZ);
            this.layerScrollWrapping.SetItemChecked(3, layer.ScrollWrapL2_VT);
            this.layerScrollWrapping.SetItemChecked(4, layer.ScrollWrapL3_HZ);
            this.layerScrollWrapping.SetItemChecked(5, layer.ScrollWrapL3_VT);
            this.layerScrollWrapping.SetItemChecked(6, layer.CulexA);
            this.layerScrollWrapping.SetItemChecked(7, layer.CulexB);
            this.layerL2HSync.SelectedIndex = layer.SyncL2_HZ;
            this.layerL3HSync.SelectedIndex = layer.SyncL3_HZ;
            this.layerL2VSync.SelectedIndex = layer.SyncL2_VT;
            this.layerL3VSync.SelectedIndex = layer.SyncL3_VT;
            this.layerL2ScrollDirection.SelectedIndex = layer.ScrollDirectionL2;
            this.layerL3ScrollDirection.SelectedIndex = layer.ScrollDirectionL3;
            this.layerL2ScrollSpeed.SelectedIndex = layer.ScrollSpeedL2;
            this.layerL3ScrollSpeed.SelectedIndex = layer.ScrollSpeedL3;
            this.layerL2ScrollShift.Checked = layer.ScrollL2Bit7;
            this.layerL3ScrollShift.Checked = layer.ScrollL3Bit7;
            this.layerL3Effects.SelectedIndex = layer.EffectsL3;
            this.layerOBJEffects.SelectedIndex = layer.EffectsNPC;
            this.layerWaveEffect.Checked = layer.RipplingWater;
        }
        #endregion
        #region Event Handlers
        private void layerMessageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.MessageBox = (byte)layerMessageBox.SelectedIndex;
        }
        private void layerPrioritySet_ValueChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
                layer.PrioritySet = (byte)layerPrioritySet.Value;
            this.Updating = true;
            this.layerMainscreenL1.Checked = prioritySets[layer.PrioritySet].MainscreenL1;
            this.layerMainscreenL2.Checked = prioritySets[layer.PrioritySet].MainscreenL2;
            this.layerMainscreenL3.Checked = prioritySets[layer.PrioritySet].MainscreenL3;
            this.layerMainscreenNPC.Checked = prioritySets[layer.PrioritySet].MainscreenOBJ;
            this.layerSubscreenL1.Checked = prioritySets[layer.PrioritySet].SubscreenL1;
            this.layerSubscreenL2.Checked = prioritySets[layer.PrioritySet].SubscreenL2;
            this.layerSubscreenL3.Checked = prioritySets[layer.PrioritySet].SubscreenL3;
            this.layerSubscreenNPC.Checked = prioritySets[layer.PrioritySet].SubscreenOBJ;
            this.layerColorMathL1.Checked = prioritySets[layer.PrioritySet].ColorMathL1;
            this.layerColorMathL2.Checked = prioritySets[layer.PrioritySet].ColorMathL2;
            this.layerColorMathL3.Checked = prioritySets[layer.PrioritySet].ColorMathL3;
            this.layerColorMathNPC.Checked = prioritySets[layer.PrioritySet].ColorMathOBJ;
            this.layerColorMathBG.Checked = prioritySets[layer.PrioritySet].ColorMathBG;
            this.layerColorMathIntensity.SelectedIndex = prioritySets[layer.PrioritySet].ColorMathHalfIntensity;
            this.layerColorMathMode.SelectedIndex = prioritySets[layer.PrioritySet].ColorMathMinusSubscreen;
            this.Updating = false;
            if (!this.Refreshing)
            {
                RefreshLevel();
            }
        }
        private void layerMainscreenL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].MainscreenL1 = layerMainscreenL1.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerMainscreenL1.Checked) layerMainscreenL1.ForeColor = Color.Black;
            else layerMainscreenL1.ForeColor = Color.Gray;
        }
        private void layerMainscreenL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].MainscreenL2 = layerMainscreenL2.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerMainscreenL2.Checked) layerMainscreenL2.ForeColor = Color.Black;
            else layerMainscreenL2.ForeColor = Color.Gray;
        }
        private void layerMainscreenL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].MainscreenL3 = layerMainscreenL3.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerMainscreenL3.Checked) layerMainscreenL3.ForeColor = Color.Black;
            else layerMainscreenL3.ForeColor = Color.Gray;
        }
        private void layerMainscreenNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].MainscreenOBJ = layerMainscreenNPC.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerMainscreenNPC.Checked) layerMainscreenNPC.ForeColor = Color.Black;
            else layerMainscreenNPC.ForeColor = Color.Gray;
        }
        private void layerSubscreenL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].SubscreenL1 = layerSubscreenL1.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerSubscreenL1.Checked) layerSubscreenL1.ForeColor = Color.Black;
            else layerSubscreenL1.ForeColor = Color.Gray;
        }
        private void layerSubscreenL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].SubscreenL2 = layerSubscreenL2.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerSubscreenL2.Checked) layerSubscreenL2.ForeColor = Color.Black;
            else layerSubscreenL2.ForeColor = Color.Gray;
        }
        private void layerSubscreenL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].SubscreenL3 = layerSubscreenL3.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerSubscreenL3.Checked) layerSubscreenL3.ForeColor = Color.Black;
            else layerSubscreenL3.ForeColor = Color.Gray;
        }
        private void layerSubscreenNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].SubscreenOBJ = layerSubscreenNPC.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerSubscreenNPC.Checked) layerSubscreenNPC.ForeColor = Color.Black;
            else layerSubscreenNPC.ForeColor = Color.Gray;
        }
        private void layerColorMathL1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].ColorMathL1 = layerColorMathL1.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerColorMathL1.Checked) layerColorMathL1.ForeColor = Color.Black;
            else layerColorMathL1.ForeColor = Color.Gray;
        }
        private void layerColorMathL2_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].ColorMathL2 = layerColorMathL2.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerColorMathL2.Checked) layerColorMathL2.ForeColor = Color.Black;
            else layerColorMathL2.ForeColor = Color.Gray;
        }
        private void layerColorMathL3_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].ColorMathL3 = layerColorMathL3.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerColorMathL3.Checked) layerColorMathL3.ForeColor = Color.Black;
            else layerColorMathL3.ForeColor = Color.Gray;
        }
        private void layerColorMathNPC_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].ColorMathOBJ = layerColorMathNPC.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerColorMathNPC.Checked) layerColorMathNPC.ForeColor = Color.Black;
            else layerColorMathNPC.ForeColor = Color.Gray;
        }
        private void layerColorMathBG_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                this.prioritySets[layer.PrioritySet].ColorMathBG = layerColorMathBG.Checked;
                if (!this.Refreshing)
                    RefreshLevel();
            }
            if (layerColorMathBG.Checked) layerColorMathBG.ForeColor = Color.Black;
            else layerColorMathBG.ForeColor = Color.Gray;
        }
        private void layerColorMathIntensity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                if (layerColorMathIntensity.SelectedIndex == 0)
                    this.prioritySets[layer.PrioritySet].ColorMathHalfIntensity = 0;//false;
                else if (layerColorMathIntensity.SelectedIndex == 1)
                    this.prioritySets[layer.PrioritySet].ColorMathHalfIntensity = 1;//true;
                if (!this.Refreshing)
                    RefreshLevel();
            }
        }
        private void layerColorMathMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.Updating)
            {
                if (layerColorMathMode.SelectedIndex == 0)
                    this.prioritySets[layer.PrioritySet].ColorMathMinusSubscreen = 0;//false;
                else if (layerColorMathMode.SelectedIndex == 1)
                    this.prioritySets[layer.PrioritySet].ColorMathMinusSubscreen = 1;// true;
                if (!this.Refreshing)
                    RefreshLevel();
            }
        }
        private void layerMaskHighX_ValueChanged(object sender, EventArgs e)
        {
            layer.MaskHighX = (byte)layerMaskHighX.Value;
            picture.Invalidate();
        }
        private void layerMaskLowX_ValueChanged(object sender, EventArgs e)
        {
            layer.MaskLowX = (byte)layerMaskLowX.Value;
            picture.Invalidate();
        }
        private void layerMaskHighY_ValueChanged(object sender, EventArgs e)
        {
            layer.MaskHighY = (byte)layerMaskHighY.Value;
            picture.Invalidate();
        }
        private void layerMaskLowY_ValueChanged(object sender, EventArgs e)
        {
            layer.MaskLowY = (byte)layerMaskLowY.Value;
            picture.Invalidate();
        }
        private void layerLockMask_CheckedChanged(object sender, EventArgs e)
        {
            layer.MaskLock = layerLockMask.Checked;
            if (layerLockMask.Checked) layerLockMask.ForeColor = Color.Black;
            else layerLockMask.ForeColor = Color.Gray;
        }
        private void layerL2LeftShift_ValueChanged(object sender, EventArgs e)
        {
            layer.XNegL2 = (byte)layerL2LeftShift.Value;
        }
        private void layerL3LeftShift_ValueChanged(object sender, EventArgs e)
        {
            layer.XNegL3 = (byte)layerL3LeftShift.Value;
        }
        private void layerL2UpShift_ValueChanged(object sender, EventArgs e)
        {
            layer.YNegL2 = (byte)layerL2UpShift.Value;
        }
        private void layerL3UpShift_ValueChanged(object sender, EventArgs e)
        {
            layer.YNegL3 = (byte)layerL3UpShift.Value;
        }
        private void layerScrollWrapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.ScrollWrapL1_HZ = layerScrollWrapping.GetItemChecked(0);
            layer.ScrollWrapL1_VT = layerScrollWrapping.GetItemChecked(1);
            layer.ScrollWrapL2_HZ = layerScrollWrapping.GetItemChecked(2);
            layer.ScrollWrapL2_VT = layerScrollWrapping.GetItemChecked(3);
            layer.ScrollWrapL3_HZ = layerScrollWrapping.GetItemChecked(4);
            layer.ScrollWrapL3_VT = layerScrollWrapping.GetItemChecked(5);
            layer.CulexA = layerScrollWrapping.GetItemChecked(6);
            layer.CulexB = layerScrollWrapping.GetItemChecked(7);
        }
        private void layerL2VSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.SyncL2_VT = (byte)layerL2VSync.SelectedIndex;
        }
        private void layerL3VSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.SyncL3_VT = (byte)layerL3VSync.SelectedIndex;
        }
        private void layerL2HSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.SyncL2_HZ = (byte)layerL2HSync.SelectedIndex;
        }
        private void layerL3HSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.SyncL3_HZ = (byte)layerL3HSync.SelectedIndex;
        }
        private void layerL2ScrollShift_CheckedChanged(object sender, EventArgs e)
        {
            layer.ScrollL2Bit7 = layerL2ScrollShift.Checked;
            if (layerL2ScrollShift.Checked) layerL2ScrollShift.ForeColor = Color.Black;
            else layerL2ScrollShift.ForeColor = Color.Gray;
        }
        private void layerL3ScrollShift_CheckedChanged(object sender, EventArgs e)
        {
            layer.ScrollL3Bit7 = layerL3ScrollShift.Checked;
            if (layerL3ScrollShift.Checked) layerL3ScrollShift.ForeColor = Color.Black;
            else layerL3ScrollShift.ForeColor = Color.Gray;
        }
        private void layerL2ScrollDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.ScrollDirectionL2 = (byte)layerL2ScrollDirection.SelectedIndex;
        }
        private void layerL2ScrollSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.ScrollSpeedL2 = (byte)layerL2ScrollSpeed.SelectedIndex;
        }
        private void layerL3ScrollDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.ScrollDirectionL3 = (byte)layerL3ScrollDirection.SelectedIndex;
        }
        private void layerL3ScrollSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.ScrollSpeedL3 = (byte)layerL3ScrollSpeed.SelectedIndex;
        }
        private void layerInfiniteAutoscroll_CheckedChanged(object sender, EventArgs e)
        {
            layer.InfiniteScrolling = layerInfiniteAutoscroll.Checked;
            if (layerInfiniteAutoscroll.Checked) layerInfiniteAutoscroll.ForeColor = Color.Black;
            else layerInfiniteAutoscroll.ForeColor = Color.Gray;
        }
        private void layerL3Effects_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.EffectsL3 = (byte)layerL3Effects.SelectedIndex;
        }
        private void layerOBJEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer.EffectsNPC = (byte)layerOBJEffects.SelectedIndex;
        }
        private void layerWaveEffect_CheckedChanged(object sender, EventArgs e)
        {
            layer.RipplingWater = layerWaveEffect.Checked;
            if (layerWaveEffect.Checked) layerWaveEffect.ForeColor = Color.Black;
            else layerWaveEffect.ForeColor = Color.Gray;
        }
        #endregion
    }
}
