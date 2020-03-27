using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class IOArchitecture : NewForm
    {
        private string action;
        private int index;
        private LevelMap levelMap;
        private PaletteSet paletteSet;
        private Tileset tileset;
        private Tilemap tilemap;
        private PrioritySet prioritySet;
        private string fullPath;
        // constructor
        public IOArchitecture(string action, int index, LevelMap levelMap, PaletteSet paletteSet, Tileset tileSet, Tilemap tileMap, PrioritySet prioritySet)
        {
            this.action = action;
            this.index = index;
            this.levelMap = levelMap;
            this.paletteSet = paletteSet;
            this.tileset = tileSet;
            this.tilemap = tileMap;
            this.prioritySet = prioritySet;
            InitializeComponent();
            if (action == "import")
                groupBox1.Text = "Import the following elements from architecture file";
            else
                groupBox1.Text = "Export the following elements to architecture file";
        }
        // functions
        private void Export_Architecture()
        {
            byte[] array = new byte[0x80000];
            ExportPalette(array, 0);
            ExportGraphics(array, 0x200);
            ExportTileset(array, 0, 0x10200);
            ExportTileset(array, 1, 0x11200);
            ExportTileset(array, 2, 0x12200);
            ExportTilemap(array, 0, 0x13200);
            ExportTilemap(array, 1, 0x33200);
            ExportTilemap(array, 2, 0x53200);
            ExportPriority(array, 0x73200);
            Do.Export(array, "", fullPath);
        }
        private void ExportPalette(byte[] array, int offset)
        {
            for (int i = 0; checkBox1.Checked && i < paletteSet.Palettes.Length; i++)
            {
                for (int a = 0; a < 16; a++)
                {
                    Bits.SetInt24(array, offset, paletteSet.Palettes[i][a]);
                    offset += 3;
                }
            }
        }
        private void ExportGraphics(byte[] array, int offset)
        {
            if (checkBox2.Checked)
                Buffer.BlockCopy(tileset.Graphics, 0, array, offset, tileset.Graphics.Length);
        }
        private void ExportTileset(byte[] array, int layer, int offset)
        {
            if (checkBox3.Checked && tileset.Tilesets_tiles.Length >= layer + 1 && tileset.Tilesets_tiles[layer] != null)
                foreach (Tile tile in tileset.Tilesets_tiles[layer])
                {
                    foreach (Subtile subtile in tile.Subtiles)
                    {
                        Bits.SetShort(array, offset, subtile.Index);
                        offset += 2;
                        array[offset] = (byte)subtile.Palette;
                        Bits.SetBit(array, offset, 5, subtile.Priority1);
                        Bits.SetBit(array, offset, 6, subtile.Mirror);
                        Bits.SetBit(array, offset, 7, subtile.Invert);
                        offset++;
                    }
                }
        }
        private void ExportTilemap(byte[] array, int layer, int offset)
        {
            if (checkBox4.Checked && tilemap.Tilemaps_Tiles.Length >= 1 && tilemap.Tilemaps_Tiles[layer] != null)
                foreach (Tile tile in tilemap.Tilemaps_Tiles[layer])
                {
                    if (tile == null)
                    {
                        offset += 2;
                        continue;
                    }
                    Bits.SetShort(array, offset, tile.Index); offset++;
                    Bits.SetBit(array, offset, 6, tile.Mirror);
                    Bits.SetBit(array, offset, 7, tile.Invert); offset++;
                }
        }
        private void ExportPriority(byte[] array, int offset)
        {
            if (checkBox5.Checked)
            {
                array[offset++] = (byte)(prioritySet.MainscreenL1 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.MainscreenL2 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.MainscreenL3 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.MainscreenOBJ ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.SubscreenL1 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.SubscreenL2 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.SubscreenL3 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.SubscreenOBJ ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.ColorMathL1 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.ColorMathL2 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.ColorMathL3 ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.ColorMathOBJ ? 0x01 : 0x00);
                array[offset++] = (byte)(prioritySet.ColorMathBG ? 0x01 : 0x00);
                array[offset++] = prioritySet.ColorMathHalfIntensity;
                array[offset++] = prioritySet.ColorMathMinusSubscreen;
            }
        }
        private void Import_Architecture()
        {
            byte[] array = new byte[0x80000];
            array = (byte[])Do.Import(array, fullPath);
            ImportPalette(array, 0);
            ImportGraphics(array, 0x200);
            ImportTileset(array, 0, 0x10200);
            ImportTileset(array, 1, 0x11200);
            ImportTileset(array, 2, 0x12200);
            ImportTilemap(array, 0, 0x13200);
            ImportTilemap(array, 1, 0x33200);
            ImportTilemap(array, 2, 0x53200);
            ImportPriority(array, 0x73200);
        }
        private void ImportPalette(byte[] array, int offset)
        {
            for (int i = 0; checkBox1.Checked && i < paletteSet.Palettes.Length; i++)
            {
                for (int a = 0; a < 16; a++)
                {
                    paletteSet.Blues[i * 16 + a] = array[offset++];
                    paletteSet.Greens[i * 16 + a] = array[offset++];
                    paletteSet.Reds[i * 16 + a] = array[offset++];
                }
            }
        }
        private void ImportGraphics(byte[] array, int offset)
        {
            if (checkBox2.Checked)
                Buffer.BlockCopy(array, offset, tileset.Graphics, 0, tileset.Graphics.Length);
        }
        private void ImportTileset(byte[] array, int layer, int offset)
        {
            if (!checkBox3.Checked || tileset.Tilesets_tiles[layer] == null)
                return;
            int length;
            byte format;
            if (layer == 2)
            {
                length = tileset.GraphicsL3.Length;
                format = 0x10;
            }
            else
            {
                length = tileset.Graphics.Length;
                format = 0x20;
            }
            foreach (Tile tile in tileset.Tilesets_tiles[layer])
            {
                if (tile == null)
                {
                    offset += 3;
                    continue;
                }
                foreach (Subtile subtile in tile.Subtiles)
                {
                    subtile.Index = Bits.GetShort(array, offset);
                    if (subtile.Index * format >= length)
                        subtile.Index = 0;
                    offset += 2;
                    subtile.Palette = array[offset] & 7;
                    subtile.Priority1 = Bits.GetBit(array, offset, 5);
                    subtile.Mirror = Bits.GetBit(array, offset, 6);
                    subtile.Invert = Bits.GetBit(array, offset, 7);
                    offset++;
                }
            }
        }
        private void ImportTilemap(byte[] array, int layer, int offset)
        {
            if (!checkBox4.Checked || tilemap.Tilemaps_Tiles[layer] == null)
                return;
            int counter = 0;
            int extratiles = 256;
            bool mirror, invert;
            for (int i = 0; i < tilemap.Tilemaps_Tiles[layer].Length; i++)
            {
                int tile = Bits.GetShort(array, offset) & 0x1FF;
                mirror = Bits.GetBit(array, offset + 1, 6);
                invert = Bits.GetBit(array, offset + 1, 7);
                tilemap.Tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][tile];
                if (layer != 2)
                {
                    Bits.SetShort(tilemap.Tilemaps_Bytes[layer], counter, tile);
                    counter += 2; offset += 2;
                }
                else
                {
                    tilemap.Tilemaps_Bytes[layer][counter] = (byte)tile;
                    counter++; offset += 2;
                }
                if (tileset.Tilesets_tiles[layer] == null || tileset.Tilesets_tiles[layer].Length != 512)
                    continue;
                if (mirror || invert)
                {
                    Tile copy = tileset.Tilesets_tiles[layer][tile].Copy();
                    if (mirror)
                        Do.FlipHorizontal(copy);
                    if (invert)
                        Do.FlipVertical(copy);
                    Tile contains = Do.Contains(tileset.Tilesets_tiles[layer], copy);
                    if (contains == null)
                    {
                        tileset.Tilesets_tiles[layer][extratiles] = copy;
                        tileset.Tilesets_tiles[layer][extratiles].Index = extratiles;
                        tilemap.Tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][extratiles];
                        Bits.SetShort(tilemap.Tilemaps_Bytes[layer], counter - 2, extratiles);
                        extratiles++;
                    }
                    else
                    {
                        tilemap.Tilemaps_Tiles[layer][i] = tileset.Tilesets_tiles[layer][contains.Index];
                        Bits.SetShort(tilemap.Tilemaps_Bytes[layer], counter - 2, contains.Index);
                    }
                }
            }
        }
        private void ImportPriority(byte[] array, int offset)
        {
            if (checkBox5.Checked)
            {
                prioritySet.MainscreenL1 = array[offset++] == 0x01;
                prioritySet.MainscreenL2 = array[offset++] == 0x01;
                prioritySet.MainscreenL3 = array[offset++] == 0x01;
                prioritySet.MainscreenOBJ = array[offset++] == 0x01;
                prioritySet.SubscreenL1 = array[offset++] == 0x01;
                prioritySet.SubscreenL2 = array[offset++] == 0x01;
                prioritySet.SubscreenL3 = array[offset++] == 0x01;
                prioritySet.SubscreenOBJ = array[offset++] == 0x01;
                prioritySet.ColorMathL1 = array[offset++] == 0x01;
                prioritySet.ColorMathL2 = array[offset++] == 0x01;
                prioritySet.ColorMathL3 = array[offset++] == 0x01;
                prioritySet.ColorMathBG = array[offset++] == 0x01;
                prioritySet.ColorMathOBJ = array[offset++] == 0x01;
                prioritySet.ColorMathHalfIntensity = array[offset++];
                prioritySet.ColorMathMinusSubscreen = array[offset++];
            }
            Model.EditTilemaps[levelMap.TilemapL1 + 0x40] = true;
            Model.EditTilemaps[levelMap.TilemapL2 + 0x40] = true;
            Model.EditTilemaps[levelMap.TilemapL3] = true;
            Model.EditTilesets[levelMap.TilesetL1 + 0x20] = true;
            Model.EditTilesets[levelMap.TilesetL2 + 0x20] = true;
            Model.EditTilesets[levelMap.TilesetL3] = true;
            Model.EditGraphicSets[levelMap.GraphicSetA + 0x48] = true;
            Model.EditGraphicSets[levelMap.GraphicSetB + 0x48] = true;
            Model.EditGraphicSets[levelMap.GraphicSetC + 0x48] = true;
            Model.EditGraphicSets[levelMap.GraphicSetD + 0x48] = true;
            Model.EditGraphicSets[levelMap.GraphicSetE + 0x48] = true;
            Model.EditGraphicSets[levelMap.GraphicSetL3] = true;
            tileset.Assemble(16);
            tilemap.Assemble();
        }
        // event handlers
        private void browseCurrent_Click(object sender, EventArgs e)
        {
            string filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            if (action == "export")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Select directory to export to";
                saveFileDialog.Filter = filter;
                saveFileDialog.FileName = "architecture.SMRPG.level." + index.ToString("d4");
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                textBoxCurrent.Text = saveFileDialog.FileName;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = LAZYSHELL.Properties.Settings.Default.LastRomPath;
                openFileDialog.Title = "Select file to import from";
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                textBoxCurrent.Text = openFileDialog.FileName;
            }
            fullPath = textBoxCurrent.Text;
            buttonOK.Enabled = true;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (action == "import")
                Import_Architecture();
            else
                Export_Architecture();
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
