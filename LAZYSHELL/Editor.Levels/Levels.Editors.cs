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
        private delegate void Function();
        private PaletteEditor paletteEditor;
        private GraphicEditor graphicEditor;
        private TilesetEditor tilesetEditor;
        private TilemapEditor tilemapEditor;
        private LevelsSolidTiles levelsSolidTiles;
        private LevelsTemplate levelsTemplate;
        public ToolStripButton OpenTileset { get { return openTileset; } set { openTileset = value; } }
        public ToolStripButton OpenSolidTileset { get { return openSolidTileset; } set { openSolidTileset = value; } }
        private Previewer previewer;
        #endregion
        #region Functions
        private void PaletteUpdate()
        {
            if (closingEditor)
                return;
            fullUpdate = false;
            RefreshLevel();
            this.Modified = true;   // b/c switching colors won't modify checksum
        }
        private void GraphicUpdate()
        {
            if (closingEditor)
                return;
            tileset.Assemble(16, tilesetEditor.Layer);
            fullUpdate = false;
            RefreshLevel();
        }
        private void TilemapUpdate()
        {
            if (closingEditor)
                return;
        }
        private void TilesetUpdate()
        {
            if (closingEditor)
                return;
            tilemap.Assemble();
            tilemap.RedrawTilemaps();// = new LevelTilemap(level, tileset);
            fullUpdate = false;
            RefreshLevel();
        }
        private void SolidityUpdate()
        {
            if (closingEditor)
                return;
            fullUpdate = true;
            solidityMap = new LevelSolidMap(levelMap);
            solidityMap.Image = null;
            LoadTilemapEditor();
        }
        //
        private void LoadPaletteEditor()
        {
            if (paletteEditor == null)
            {
                paletteEditor = new PaletteEditor(new Function(PaletteUpdate), paletteSet, 8, 1, 7);
                paletteEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                paletteEditor.Reload(new Function(PaletteUpdate), paletteSet, 8, 1, 7);
        }
        private void LoadGraphicEditor()
        {
            if (graphicEditor == null)
            {
                graphicEditor = new GraphicEditor(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 1, 0x20);
                graphicEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                graphicEditor.Reload(new Function(GraphicUpdate),
                    tileset.Graphics, tileset.Graphics.Length, 0, paletteSet, 1, 0x20);
        }
        private void LoadTilemapEditor()
        {
            if (tilemapEditor == null)
            {
                tilemapEditor = new TilemapEditor(
                    this, this.level, this.tilemap, this.solidityMap, this.tileset, this.overlay,
                    this.paletteEditor, this.tilesetEditor, this.levelsSolidTiles, this.levelsTemplate);
                tilemapEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilemapEditor.Reload(
                  this, this.level, this.tilemap, this.solidityMap, this.tileset, this.overlay,
                  this.paletteEditor, this.tilesetEditor, this.levelsSolidTiles, this.levelsTemplate);
        }
        private void LoadTilesetEditor()
        {
            if (tilesetEditor == null)
            {
                tilesetEditor = new TilesetEditor(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
                tilesetEditor.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                tilesetEditor.Reload(this.tileset, new Function(TilesetUpdate), this.paletteSet, this.overlay);
        }
        private void LoadSolidityTileset()
        {
            levelsSolidTiles = new LevelsSolidTiles(solidity, new Function(SolidityUpdate));
            levelsSolidTiles.FormClosing += new FormClosingEventHandler(editor_FormClosing);
        }
        private void LoadTemplateEditor()
        {
            if (levelsTemplate == null)
            {
                levelsTemplate = new LevelsTemplate(this, this.overlay);
                levelsTemplate.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                levelsTemplate.Reload(this, this.overlay);
        }
        private void LoadPreviewer()
        {
            if (previewer == null)
            {
                previewer = new Previewer(Index, EType.Level);
                previewer.FormClosing += new FormClosingEventHandler(editor_FormClosing);
            }
            else
                previewer.Reload((int)this.levelNum.Value, EType.Level);
        }
        #endregion
        #region Event handlers
        private void editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
        //
        private void propertiesButton_Click(object sender, EventArgs e)
        {
            tabControl.Visible = propertiesButton.Checked;
        }
        private void openPaletteEditor_Click(object sender, EventArgs e)
        {
            paletteEditor.Visible = true;
        }
        private void openGraphicEditor_Click(object sender, EventArgs e)
        {
            graphicEditor.Visible = true;
        }
        public void openTileset_Click(object sender, EventArgs e)
        {
            tilesetEditor.Visible = openTileset.Checked;
            tilesetEditor.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - tilesetEditor.Size.Width - 5, this.Location.Y);
        }
        private void openTilemap_Click(object sender, EventArgs e)
        {
            tilemapEditor.Visible = openTilemap.Checked;
            tilemapEditor.Size = new Size(
                Screen.PrimaryScreen.WorkingArea.Width - tilesetEditor.Width - this.Width - 10, this.Height);
            tilemapEditor.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
        }
        public void openSolidTileset_Click(object sender, EventArgs e)
        {
            levelsSolidTiles.Visible = openSolidTileset.Checked;
            levelsSolidTiles.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - levelsSolidTiles.Size.Width, this.Location.Y);
        }
        private void openTemplates_Click(object sender, EventArgs e)
        {
            levelsTemplate.Visible = openTemplates.Checked;
            levelsTemplate.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - levelsTemplate.Size.Width, this.Location.Y);
        }
        private void openPreviewer_Click(object sender, EventArgs e)
        {
            LoadPreviewer();
            previewer.Show();
            previewer.BringToFront();
        }
        #endregion
    }
}
