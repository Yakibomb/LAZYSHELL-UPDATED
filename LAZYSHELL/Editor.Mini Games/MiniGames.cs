using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class MiniGames : NewForm
    {
        private MineCart minecart; public MineCart Minecart { get { return minecart; } set { minecart = value; } }
        private State state = State.Instance2;
        //
        public MiniGames()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip1, Keys.Control | Keys.S, new EventHandler(save_Click));
            LoadMineCartEditor();
            //
            minecart.TopLevel = false;
            minecart.Dock = DockStyle.Fill;
            //overworld.SetToolTips(toolTip1);
            panel1.Controls.Add(minecart);
            minecart.BringToFront();
            //openMenus.Checked = true;
            minecart.Visible = true;
            new ToolTipLabel(this, null, helpTips);
            this.History = new History(this, false);
            //
            GC.Collect();
        }
        private void LoadMineCartEditor()
        {
            if (minecart == null)
                minecart = new MineCart(this);
            else
                minecart.Reload(this);
        }
        public void Assemble()
        {
            minecart.Assemble();
            minecart.Modified = false;
            this.Modified = false;
        }
        //
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void importTilesetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] var = (byte[])Do.Import(Model.MinecartM7TilesetSubtiles);
            if (var != null) Model.MinecartM7TilesetSubtiles = var;
            var = (byte[])Do.Import(Model.MinecartM7TilesetPalettes);
            if (var != null) Model.MinecartM7TilesetPalettes = var;
            var = (byte[])Do.Import(Model.MinecartSSTileset);
            if (var != null) Model.MinecartSSTileset = var;
            minecart.RefreshLevel();
        }
        private void importTilemapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] var = (byte[])Do.Import(Model.MinecartM7TilemapA);
            if (var != null) Model.MinecartM7TilemapA = var;
            var = (byte[])Do.Import(Model.MinecartM7TilemapB);
            if (var != null) Model.MinecartM7TilemapB = var;
            var = (byte[])Do.Import(Model.MinecartSSTilemap);
            if (var != null) Model.MinecartSSTilemap = var;
            minecart.RefreshLevel();
        }
        private void exportTilesetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(Model.MinecartM7TilesetSubtiles, "minecartStage1subtiles.bin");
            Do.Export(Model.MinecartM7TilesetPalettes, "minecartStage1palettes.bin");
            Do.Export(Model.MinecartSSTileset, "minecartStage2tileset.bin");
        }
        private void exportTilemapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Do.Export(Model.MinecartM7TilemapA, "minecartStage1tilemap.bin");
            Do.Export(Model.MinecartM7TilemapB, "minecartStage3tilemap.bin");
            Do.Export(Model.MinecartSSTilemap, "minecartStage2tilemap.bin");
        }
        //
        private void MiniGames_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified)
                goto Close;
            state.Draw = false;
            state.Erase = false;
            state.Select = false;
            state.Dropper = false;
            state.Fill = false;
            state.TileGrid = false;
            DialogResult result;
            result = MessageBox.Show("Mini-games have not been saved.\n\nWould you like to save changes?", "LAZY SHELL", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                minecart.MinecartData = null;
                Model.MinecartM7Graphics = null;
                Model.MinecartM7PaletteSet = null;
                Model.MinecartM7TilemapA = null;
                Model.MinecartM7TilemapB = null;
                Model.MinecartM7TilesetPalettes = null;
                Model.MinecartM7TilesetSubtiles = null;
                Model.MinecartObjectGraphics = null;
                Model.MinecartObjectPaletteSet = null;
                Model.MinecartSSBGTileset = null;
                Model.MinecartSSGraphics = null;
                Model.MinecartSSPaletteSet = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            minecart.CloseEditors();
            LAZYSHELL.Properties.Settings.Default.Save();
        }
        private void resetAllObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Doing this will erase all changes to the object data for all stages since the last save, including mushrooms, coins, and screens. Continue?",
                "LAZY SHELL", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            Model.MinecartObjects = null;
            minecart.MinecartData = new MinecartData(Model.MinecartObjects);
            minecart.RefreshLevel();
        }
        private void resetCurrentTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Doing this will erase all changes to the tileset since the last save. Continue?",
                "LAZY SHELL", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (minecart.Index < 2)
            {
                Model.MinecartM7TilesetSubtiles = null;
                Model.MinecartM7TilesetPalettes = null;
            }
            else
                Model.MinecartSSTileset = null;
            minecart.RefreshLevel();
        }
        private void resetCurrentTilemapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Doing this will erase all changes to the tilemap since the last save. Continue?",
                "LAZY SHELL", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (minecart.Index == 0)
                Model.MinecartM7TilemapA = null;
            else if (minecart.Index == 1)
                Model.MinecartM7TilemapB = null;
            else
                Model.MinecartSSTilemap = null;
            minecart.RefreshLevel();
        }
    }
}
