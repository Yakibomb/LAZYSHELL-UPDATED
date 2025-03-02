using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using static LAZYSHELL.NotesDB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LAZYSHELL
{
    public partial class Effects : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        // main
        private delegate void Function();
        private Overlay overlay = new Overlay();
                private Effect[] effects { get { return Model.Effects; } set { Model.Effects = value; } }
        private E_Animation[] animations { get { return Model.E_animations; } set { Model.E_animations = value; } }
        private int availableBytes = 0;
        // indexed variables
        public int index { get { return (int)number.Value; } set { number.Value = value; } }
        private int palette { get { return (int)e_paletteIndex.Value; } set { e_paletteIndex.Value = value; } }
        private Effect effect { get { return effects[index]; } set { effects[index] = value; } }
        private E_Animation animation { get { return animations[(int)imageNum.Value]; } set { animations[(int)imageNum.Value] = value; } }
        // public variables
        public Effect Effect { get { return effect; } set { effect = value; } }
        public E_Animation Animation { get { return animation; } set { animation = value; } }
        public int AvailableBytes { get { return availableBytes; } set { availableBytes = value; } }
        public NumericUpDown E_graphicSetSize { get { return e_graphicSetSize; } set { e_graphicSetSize = value; } }
        // editors
        private EffectMolds molds;
        public EffectMolds Molds { get { return molds; } set { molds = value; } }
        private EffectSequences sequences;
        public EffectSequences Sequences { get { return sequences; } set { sequences = value; } }
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        private Search searchWindow;
        private EditLabel labelWindow;
        private Previewer previewer;
        // special controls
        #endregion
        #region Functions
        // main
        public Effects()
        {
            // set data
            InitializeComponent();
            Do.AddShortcut(toolStrip2, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip2, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip2, Keys.F2, baseConvertor);
            // tooltips
            toolTip1.InitialDelay = 0;
            searchWindow = new Search(number, searchBox, searchEffectNames, name.Items);
            labelWindow = new EditLabel(name, number, "Effects", true);
            // set control values
            this.Updating = true;
            this.animation.Tileset_tiles = new E_Tileset(animation, palette);
            this.name.Items.AddRange(Lists.Numerize(Lists.EffectNames));
            this.name.SelectedIndex = 0;
            foreach (E_Animation a in animations)
            {
                a.Tileset_tiles = new E_Tileset(a, 0);
                a.Assemble();
            }
            RefreshEffectsEditor();
            this.Updating = false;
            GC.Collect();
            // create editors
            molds.TopLevel = false;
            molds.Dock = DockStyle.Fill;
            panelMolds.Controls.Add(molds);
            molds.BringToFront();
            openMolds.Checked = true;
            molds.Visible = true;
            sequences.TopLevel = false;
            sequences.Dock = DockStyle.Fill;
            panelSequences.Controls.Add(sequences);
            sequences.SendToBack();
            openSequences.Checked = true;
            sequences.Visible = true;
            new ToolTipLabel(this, baseConvertor, helpTips);
            //
            this.History = new History(this, name, number);
            if (settings.RememberLastIndex)
                index = settings.LastEffect;
        }
        private void RefreshEffectsEditor()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Updating = true;
            // main properties
            imageNum.Value = effect.AnimationPacket;
            e_paletteIndex.Value = palette = effect.PaletteIndex;
            xNegShift.Value = effect.X;
            yNegShift.Value = effect.Y;
            // image properties
            e_paletteSetSize.Value = animation.PaletteSetLength;
            e_graphicSetSize.Minimum = animation.Codec == 1 ? 16 : 32;
            e_graphicSetSize.Value = animation.GraphicSetLength;
            e_codec.SelectedIndex = animation.Codec;
            animation.Tileset_tiles = new E_Tileset(animation, palette);
            // editors
            LoadMoldEditor();
            LoadSequenceEditor();
            LoadPaletteEditor();
            LoadGraphicEditor();
            CalculateFreeSpace();
            this.Updating = false;
            GC.Collect();
            Cursor.Current = Cursors.Arrow;
        }
        public void CalculateFreeSpace()
        {
            int totalSize, min, max, length = 0;
            if (animation.Index < 39)
            {
                totalSize = 0xFFFF; min = 0; max = 39;
            }
            else
            {
                totalSize = 0xCFFF; min = 39; max = 64;
            }
            for (int i = min; i < max; i++)
                length += animations[i].BUFFER.Length;
            availableBytes = totalSize - length;
            e_availableBytes.BackColor = availableBytes > 0 ? Color.Lime : Color.Red;
            e_availableBytes.Text = availableBytes.ToString() + " bytes free";
        }
        public void Assemble()
        {
            effect.Assemble();
            animation.Assemble();
            int i = 0;
            int pointer = 0x252C00;
            int offset = 0x330000;
            for (; i < 39 && offset < 0x33FFFF; i++, pointer += 3)
            {
                if (animations[i].BUFFER.Length + offset > 0x33FFFF)
                    break;
                Bits.SetShort(Model.ROM, pointer, (ushort)offset);
                Bits.SetByte(Model.ROM, pointer + 2, (byte)((offset >> 16) + 0xC0));
                Bits.SetBytes(Model.ROM, offset, animations[i].BUFFER);
                offset += animations[i].BUFFER.Length;
            }
            if (i < 39)
                MessageBox.Show("The available space for animation data in bank 0x330000 has exceeded the alotted space.\nAnimation #'s " + i.ToString() + " through 38 will not saved. Please make sure the available animation bytes is not negative.", "LAZYSHELL++");
            offset = 0x340000;
            for (; i < 64 && offset < 0x34CFFF; i++, pointer += 3)
            {
                if (animations[i].BUFFER.Length + offset > 0x34CFFF)
                    break;
                Bits.SetShort(Model.ROM, pointer, (ushort)offset);
                Bits.SetByte(Model.ROM, pointer + 2, (byte)((offset >> 16) + 0xC0));
                Bits.SetBytes(Model.ROM, offset, animations[i].BUFFER);
                offset += animations[i].BUFFER.Length;
            }
            if (i < 64)
                MessageBox.Show("The available space for animation data in bank 0x340000 has exceeded the alotted space.\nAnimation #'s " + i.ToString() + " through 63 will not saved. Please make sure the available animation bytes is not negative.", "LAZYSHELL++");
            molds.Modified = false;
            sequences.Modified = false;
            this.Modified = false;
        }
        public void EnableOnPlayback(bool enable)
        {
            foreach (Control control in this.Controls)
                if (control != panelSequences)
                    control.Enabled = enable;
                else
                    foreach (Control parent in panelSequences.Controls)
                        if (parent != sequences)
                            parent.Enabled = enable;
                        else
                            foreach (Control child in parent.Controls)
                                if (child.Name != "toolStrip1")
                                    child.Enabled = enable;
                                else
                                    foreach (ToolStripItem item in ((ToolStrip)child).Items)
                                        if (item.Name != "pause")
                                            item.Enabled = enable;
        }
        // editors
        public void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), animation.PaletteSet, 8, 0, 8);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), animation.PaletteSet, 8, 0, 8);
        }
        public void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    animation.GraphicSet, animation.GraphicSetLength, 0, animation.PaletteSet, 0,
                    animation.Codec == 1 ? (byte)0x10 : (byte)0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    animation.GraphicSet, animation.GraphicSetLength, 0, animation.PaletteSet, 0,
                    animation.Codec == 1 ? (byte)0x10 : (byte)0x20);
        }
        private void LoadMoldEditor()
        {
            if (molds == null)
                molds = new EffectMolds(this);
            else
                molds.Reload(this);
        }
        private void LoadSequenceEditor()
        {
            if (sequences == null)
                sequences = new EffectSequences(this);
            else
                sequences.Reload(this);
        }
        private void PaletteUpdate()
        {
            animation.Tileset_tiles = new E_Tileset(animation, palette);
            molds.SetTilesetImage();
            molds.SetTilemapImage();
            sequences.SetSequenceFrameImages();
            sequences.InvalidateFrameImages();
            LoadGraphicEditor();
            molds.LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdate()
        {
            animation.Tileset_tiles = new E_Tileset(animation, palette);
            molds.SetTilesetImage();
            molds.SetTilemapImage();
            sequences.SetSequenceFrameImages();
            sequences.InvalidateFrameImages();
            molds.LoadTileEditor();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        #endregion
        #region Event handlers
        private void Effects_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !molds.Modified && !sequences.Modified)
                goto Close;
            DialogResult result = MessageBox.Show(
                "Effects have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.Effects = null;
                Model.E_animations = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            paletteEditor.Close();
            graphicEditor.Close();
            searchWindow.Close();
            molds.tileEditor.Close();
            paletteEditor.Dispose();
            graphicEditor.Dispose();
            searchWindow.Dispose();
            molds.tileEditor.Dispose();
        }
        private void number_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            name.SelectedIndex = (int)number.Value;
            if (animation.Tileset_tiles != null)
                animations[(int)imageNum.Value].Assemble();
            RefreshEffectsEditor();
            settings.LastEffect = index;
            settings.PreviewEffects = index;
        }
        private void name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            number.Value = name.SelectedIndex;
        }
        // basic
        private void e_paletteIndex_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            effect.PaletteIndex = (byte)e_paletteIndex.Value;
            animation.Tileset_tiles = new E_Tileset(animation, effect.PaletteIndex);
            molds.SetTilesetImage();
            molds.SetTilemapImage();
            sequences.SetSequenceFrameImages();
        }
        private void xNegShift_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            effect.X = (byte)xNegShift.Value;
        }
        private void yNegShift_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            effect.Y = (byte)yNegShift.Value;
        }
        private void imageNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            effect.AnimationPacket = (byte)imageNum.Value;
            // image properties
            e_paletteSetSize.Value = animation.PaletteSetLength;
            e_graphicSetSize.Minimum = animation.Codec == 1 ? 16 : 32;
            e_graphicSetSize.Value = animation.GraphicSetLength;
            e_codec.SelectedIndex = animation.Codec;
            //
            animation.Tileset_tiles = new E_Tileset(animation, effect.PaletteIndex);
            CalculateFreeSpace();
            LoadMoldEditor();
            LoadSequenceEditor();
        }
        private void e_paletteSetSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            animation.PaletteSetLength = (ushort)e_paletteSetSize.Value;
            // update free space
            animation.Assemble();
            CalculateFreeSpace();
        }
        private void e_graphicSetSize_ValueChanged(object sender, EventArgs e)
        {
            e_graphicSetSize.Value = (int)e_graphicSetSize.Value & (animation.Codec == 1 ? 0xFFFFF0 : 0xFFFFE0);
            if (this.Updating)
                return;
            animation.GraphicSetLength = (int)e_graphicSetSize.Value;
            // update free space
            animation.Assemble();
            CalculateFreeSpace();
            LoadGraphicEditor();
        }
        private void e_codec_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_graphicSetSize.Minimum = e_codec.SelectedIndex == 1 ? 16 : 32;
            if (this.Updating)
                return;
            animation.Codec = (ushort)e_codec.SelectedIndex;
            animation.Tileset_tiles = new E_Tileset(animation, effect.PaletteIndex);
            molds.SetTilesetImage();
            molds.SetTilemapImage();
            sequences.SetSequenceFrameImages();
            sequences.DrawFrames();
        }
        // editors
        private void showMain_Click(object sender, EventArgs e)
        {
            panel2.Visible = showMain.Checked;
        }
        private void openPalettes_Click(object sender, EventArgs e)
        {
            paletteEditor.Visible = true;
        }
        private void openGraphics_Click(object sender, EventArgs e)
        {
            graphicEditor.Visible = true;
        }
        private void openMolds_Click(object sender, EventArgs e)
        {
            molds.Visible = openMolds.Checked;
        }
        private void openSequences_Click(object sender, EventArgs e)
        {
            panelSequences.Visible = openSequences.Checked;
        }
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        // data managing
        private void save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Assemble();
            Cursor.Current = Cursors.Arrow;
        }
        private void import_Click(object sender, EventArgs e)
        {
            new IOElements(animations, (int)imageNum.Value, "IMPORT EFFECT ANIMATIONS...").ShowDialog();
            this.animation.PaletteSet.BUFFER = Model.ROM;
            foreach (E_Animation animation in animations)
                animation.Assemble();
            RefreshEffectsEditor();
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements(animations, (int)imageNum.Value, "EXPORT EFFECT ANIMATIONS...").ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            ClearElements clearElements = new ClearElements(animations, (int)imageNum.Value, "CLEAR EFFECT ANIMATIONS...");
            clearElements.ShowDialog();
            foreach (E_Animation animation in animations)
                animation.Assemble();
            RefreshEffectsEditor();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current effect and animation index. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            animation = new E_Animation(effect.AnimationPacket);
            effect = new Effect(index);
            number_ValueChanged(null, null);
        }
        private void cullAnimations_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will clean all unused graphics, tiles, and palettes. " +
                "It will increase the amount of free space by thousands of bytes.\n\n" +
                "Go ahead with process?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            foreach (E_Animation image in animations)
            {
                byte format = (byte)(image.Codec == 1 ? 0x10 : 0x20);
                int highestTile = 0;
                int highestSubtile = 0;
                int highestPalette = 0;
                int highestMold = 0;
                // cull tileset size
                // find highest tile in tilemaps
                foreach (E_Mold mold in image.Molds)
                {
                    for (int a = 0; a < mold.Mold.Length; a++)
                        if (mold.Mold[a] < 0xFE && (mold.Mold[a] & 0x3F) > highestTile)
                            highestTile = mold.Mold[a] & 0x3F;
                }
                highestTile++;
                if (highestTile * 8 < image.TilesetLength)
                {
                    int temp = highestTile * 8;
                    image.TilesetLength = Math.Min(highestTile * 8, 512);
                    image.TilesetLength = image.TilesetLength / 64 * 64;
                    if (image.TilesetLength == 0)
                        image.TilesetLength += 64;
                    else if (image.TilesetLength <= 512 - 64 && temp % 64 != 0)
                        image.TilesetLength += 64;
                }
                // cull graphics size
                // find highest subtile index in tileset
                for (int i = 0; i < image.TilesetLength; i += 2)
                    if (image.Tileset_bytes[i] < 0xFF && image.Tileset_bytes[i] > highestSubtile)
                        highestSubtile = image.Tileset_bytes[i];
                highestSubtile++;
                if (highestSubtile * format < image.GraphicSetLength)
                    image.GraphicSetLength = highestSubtile * format;
                // cull palette size
                // find highest palette index in all animations
                foreach (Effect effect in effects)
                {
                    if (effect.AnimationPacket == image.Index && effect.PaletteIndex > highestPalette)
                        highestPalette = effect.PaletteIndex;
                }
                highestPalette++;
                if (highestPalette * 32 < image.PaletteSetLength)
                {
                    int temp = highestPalette * 32;
                    image.PaletteSetLength = (ushort)Math.Min(highestPalette * 32, 256);
                    image.PaletteSetLength = (ushort)(image.PaletteSetLength / 32 * 32);
                    if (image.PaletteSetLength == 0)
                        image.PaletteSetLength += 32;
                    else if (image.PaletteSetLength <= 256 - 32 && temp % 32 != 0)
                        image.PaletteSetLength += 32;
                }
                // cull molds
                // find highest mold index in sequence
                foreach (E_Sequence.Frame frame in image.Sequences[0].Frames)
                {
                    if (frame.Mold > highestMold)
                        highestMold = frame.Mold;
                }
                highestMold++;
                if (highestMold < image.Molds.Count)
                    image.Molds.RemoveRange(highestMold, image.Molds.Count - highestMold);
            }
            foreach (E_Animation animation in animations)
            {
                if (animation.Tileset_tiles != null)
                    animation.Tileset_tiles = new E_Tileset(animation, palette);
                animation.Assemble();
            }
            RefreshEffectsEditor();
        }
        private void previewerButton_Click(object sender, EventArgs e)
        {
            settings.PreviewSprites = index;
            if (previewer == null || !previewer.Visible)
                previewer = new Previewer(index, false, EType.Effects);
            else
                previewer.Reload(index, EType.Effects);
            previewer.Show();
            previewer.BringToFront();
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset(animation.AnimationOffset);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        #endregion
    }
}
