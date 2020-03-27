using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class SpritePartitions : NewForm
    {
        private Levels level;
        private int index { get { return (int)partitionNum.Value; } set { partitionNum.Value = value; } }
        private Partitions[] partitions;
        private Partitions partition { get { return partitions[index]; } set { partitions[index] = value; } }
        //private Bitmap previewImage;
        private LevelNPCs levelNPCs { get { return level.Level.LevelNPCs; } }
        private List<NPC> npcs { get { return levelNPCs.Npcs; } }
                // constructor
        public SpritePartitions(Levels level, Partitions[] partitions, int index)
        {
            InitializeComponent();
            this.level = level;
            this.partitions = partitions;
            this.index = index;
            RefreshPartition();
            this.History = new History(this);
        }
        public void Reload(int index)
        {
            this.index = index;
        }
        // functions
        private void RefreshPartition()
        {
            this.Updating = true;
            extraSpriteBuffer.Value = partition.ExtraSpriteBuffer;
            allyCount.Value = partition.AllySpriteBuffer;
            extraSprites.Checked = partition.ExtraSprites;
            noWaterPalettes.Checked = partition.FullPaletteBuffer;
            byte2a.SelectedIndex = partition.CloneASprite;
            byte2b.SelectedIndex = partition.CloneAMain;
            byte2.Checked = partition.CloneAIndexing;
            byte3a.SelectedIndex = partition.CloneBSprite;
            byte3b.SelectedIndex = partition.CloneBMain;
            byte3.Checked = partition.CloneBIndexing;
            byte4a.SelectedIndex = partition.CloneCSprite;
            byte4b.SelectedIndex = partition.CloneCMain;
            byte4.Checked = partition.CloneCIndexing;
            SetPreviewImage();
            this.Updating = false;
        }
        private void SetPreviewImage()
        {
            //int[] pixels = new int[128 * 256];
            //List<int> cloneSprites = new List<int>();
            //List<int> dynamicSprites = new List<int>();
            //foreach (NPC levelNPC in npcs)
            //{
            //    int NPCID = levelNPC.EngageType == 0 ? Math.Min(511, levelNPC.NPCID + levelNPC.PropertyA) : Math.Min(511, (int)levelNPC.NPCID);
            //    NPCProperties npc = Model.NPCProperties[NPCID];
            //    if (cloneSprites.Count < 3 && !npc.ActiveVRAM && !cloneSprites.Contains((int)npc.Sprite))
            //        cloneSprites.Add((int)npc.Sprite);
            //    else if (!cloneSprites.Contains((int)npc.Sprite))
            //        dynamicSprites.Add((int)npc.Sprite);
            //}
            //// draw clone VRAM
            //int counter = 0;
            //int x = 32;
            //int y = 64;
            //foreach (int sprite in cloneSprites)
            //{
            //    x = 32;
            //    if (counter == 0 && partition.CloneASprite == 7)
            //        y += 64;   // start a block after;
            //    if (counter == 1 && partition.CloneBSprite == 7)
            //        y += 64;   // start a block after;
            //    if (counter == 2 && partition.CloneCSprite == 7)
            //        y += 64;   // start a block after;
            //    int y_ref = y;
            //    DrawNPCToVRAM(pixels, ref x, ref y_ref, sprite, 0, false);
            //    y += 64;
            //    counter++;
            //}
            //// draw shadows
            //// draw dynamic VRAM
            //x = y = 0;
            //DrawNPCToVRAM(pixels, ref x, ref y, 0, 0, true);    // Mario
            //x = y = 0;
            //if (partition.AllySpriteBuffer == 1)
            //    x = 64;
            //if (partition.AllySpriteBuffer == 2)
            //    y = 16;
            //if (partition.AllySpriteBuffer == 2)
            //{
            //    x = 64;
            //    y = 16;
            //}
            //foreach (int sprite in dynamicSprites)
            //    DrawNPCToVRAM(pixels, ref x, ref y, sprite, 0, true);
            //// 
            //previewImage = Do.PixelsToImage(pixels, 128, 256);
            //pictureBox1.Invalidate();
        }
        private void DrawNPCToVRAM(int[] dst, ref int x, ref int y, int spriteIndex, int moldIndex, bool dynamic)
        {
            Sprite sprite = Model.Sprites[spriteIndex];
            Animation animation = Model.Animations[sprite.AnimationPacket];
            ImagePacket image = Model.GraphicPalettes[sprite.Image];
            byte[] graphics = image.Graphics(Model.SpriteGraphics);
            int[] palette = Model.SpritePalettes[image.PaletteNum + sprite.PaletteIndex].Palette;
            //
            for (int i = 0; i < animation.Molds.Count; i++)
            {
                if (dynamic && i > 0)
                    break;
                Mold mold;
                if (dynamic)
                    mold = animation.Molds[moldIndex];
                else
                    mold = animation.Molds[i];
                int counter = 3;
                foreach (Mold.Tile tile in mold.Tiles)
                {
                    tile.DrawSubtiles(graphics, palette, mold.Gridplane);
                    Rectangle srcRegion;
                    Rectangle dstRegion;
                    int[] src = mold.Gridplane ? tile.GetGridplanePixels() : tile.Get16x16TilePixels();
                    if (dynamic)
                    {
                        if (x + tile.Width > 128)
                        {
                            x = 0;
                            y += 16;
                        }
                        srcRegion = new Rectangle(0, 0, tile.Width, 16);
                        dstRegion = new Rectangle(x, y, tile.Width, 16);
                        Do.PixelsToPixels(src, dst, mold.Gridplane ? 32 : 16, 128, srcRegion, dstRegion);
                        if (mold.Gridplane) // draw bottom half of sprite
                        {
                            srcRegion.Y += 16;
                            dstRegion.X += tile.Width;
                            Do.PixelsToPixels(src, dst, 32, 128, srcRegion, dstRegion);
                            x += 64;
                        }
                        else
                            x += tile.Width;
                    }
                    else
                    {
                        if (x + tile.Width > 128)
                        {
                            x = 32;
                            y += 32;
                        }
                        if (mold.Gridplane)
                        {
                            srcRegion = new Rectangle(0, 0, tile.Width, 32);
                            dstRegion = new Rectangle(x, y, tile.Width, 32);
                            Do.PixelsToPixels(src, dst, 32, 128, srcRegion, dstRegion);
                            x += tile.Width;
                        }
                        else
                        {
                            srcRegion = new Rectangle(0, 0, tile.Width, 16);
                            if (counter == 3)
                                dstRegion = new Rectangle(x + 16, y + 16, 16, 16);
                            else if (counter == 2)
                                dstRegion = new Rectangle(x, y + 16, 16, 16);
                            else if (counter == 1)
                                dstRegion = new Rectangle(x + 16, y, 16, 16);
                            else
                                dstRegion = new Rectangle(x, y, 16, 16);
                            Do.PixelsToPixels(src, dst, 16, 128, srcRegion, dstRegion);
                            counter--;
                            if (counter < 0)
                                x += 32;
                        }
                    }
                }
            }
        }
        private void FindIdentical(Partitions partition, StreamWriter total)
        {
            foreach (Partitions p in partitions)
            {
                if (p.Index <= partition.Index)
                    continue;
                if (p.AllySpriteBuffer == partition.AllySpriteBuffer &&
                    p.Byte1b0 == partition.Byte1b0 &&
                    p.Byte1b1 == partition.Byte1b1 &&
                    p.Byte1b2 == partition.Byte1b2 &&
                    p.Byte1b3 == partition.Byte1b3 &&
                    p.CloneASprite == partition.CloneASprite &&
                    p.CloneAMain == partition.CloneAMain &&
                    p.CloneAIndexing == partition.CloneAIndexing &&
                    p.CloneBSprite == partition.CloneBSprite &&
                    p.CloneBMain == partition.CloneBMain &&
                    p.CloneBIndexing == partition.CloneBIndexing &&
                    p.CloneCSprite == partition.CloneCSprite &&
                    p.CloneCMain == partition.CloneCMain &&
                    p.CloneCIndexing == partition.CloneCIndexing &&
                    p.ExtraSprites != partition.ExtraSprites &&
                    p.Index != partition.Index &&
                    p.FullPaletteBuffer == partition.FullPaletteBuffer &&
                    p.ExtraSpriteBuffer == partition.ExtraSpriteBuffer)
                {
                    total.WriteLine(partition.Index + " (" + partition.ExtraSprites + ") and " + p.Index + " (" + p.ExtraSprites + ")");
                }
            }
        }
        // event handlers
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void partitionNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshPartition();
        }
        //
        private void extraSpriteBuffer_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.ExtraSpriteBuffer = (byte)extraSpriteBuffer.Value;
        }
        private void allyCount_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.AllySpriteBuffer = (byte)allyCount.Value;
            SetPreviewImage();
        }
        private void extraSprites_CheckedChanged(object sender, EventArgs e)
        {
            extraSpriteBuffer.Enabled = extraSprites.Checked;
            if (this.Updating)
                return;
            partition.ExtraSprites = extraSprites.Checked;
            SetPreviewImage();
        }
        private void noWaterPalettes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.FullPaletteBuffer = noWaterPalettes.Checked;
        }
        private void byte2a_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneASprite = (byte)byte2a.SelectedIndex;
            SetPreviewImage();
        }
        private void byte2b_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneAMain = (byte)byte2b.SelectedIndex;
            SetPreviewImage();
        }
        private void byte2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneAIndexing = byte2.Checked;
            SetPreviewImage();
        }
        private void byte3a_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneBSprite = (byte)byte3a.SelectedIndex;
            SetPreviewImage();
        }
        private void byte3b_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneBMain = (byte)byte3b.SelectedIndex;
            SetPreviewImage();
        }
        private void byte3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneBIndexing = byte3.Checked;
            SetPreviewImage();
        }
        private void byte4a_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneCSprite = (byte)byte4a.SelectedIndex;
            SetPreviewImage();
        }
        private void byte4b_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneCMain = (byte)byte4b.SelectedIndex;
            SetPreviewImage();
        }
        private void byte4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            partition.CloneCIndexing = byte4.Checked;
            SetPreviewImage();
        }
        //
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //if (previewImage != null)
            //    e.Graphics.DrawImage(previewImage, 0, 0);
        }
    }
}