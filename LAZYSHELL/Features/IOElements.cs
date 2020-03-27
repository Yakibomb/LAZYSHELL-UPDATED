using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL
{
    public partial class IOElements : NewForm
    {
        private Settings settings = Settings.Default;
        private object element;
        private int currentIndex;
        private string fullPath;
        private Type type;
        private object[] args;
        // constructor
        public IOElements(object element, int currentIndex, string title, params object[] args)
        {
            this.element = element;
            this.currentIndex = currentIndex;
            this.args = args;
            this.type = element.GetType();
            this.TopLevel = true;
            InitializeComponent();
            this.Text = title;
        }
        // event handlers
        private void radioButtonCurrent_CheckedChanged(object sender, EventArgs e)
        {
            browseAll.Enabled = false;
            textBoxAll.Enabled = false;
            browseCurrent.Enabled = true;
            textBoxCurrent.Enabled = true;
            if (radioButtonCurrent.Checked)
            {
                buttonOK.Enabled = textBoxCurrent.Text != "";
            }
            fullPath = textBoxCurrent.Text;
        }
        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            browseCurrent.Enabled = false;
            textBoxCurrent.Enabled = false;
            browseAll.Enabled = true;
            textBoxAll.Enabled = true;
            buttonOK.Enabled = true;
            if (radioButtonAll.Checked)
            {
                buttonOK.Enabled = textBoxAll.Text != "";
            }
            fullPath = textBoxAll.Text;
        }
        private void browseCurrent_Click(object sender, EventArgs e)
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            string name = this.Text.ToLower().Substring(7, this.Text.Length - 7 - 4);
            string ext = ".dat";
            string filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            if (name == "sample wav")
            {
                ext = ".wav";
                filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
            }
            else if (name == "sample brr")
            {
                ext = ".brr";
                filter = "BRR files (*.brr)|*.brr|All files (*.*)|*.*";
            }
            if (this.Text.Substring(0, 6) == "EXPORT")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Select directory to export to";
                saveFileDialog.Filter = filter;
                try
                {
                    saveFileDialog.FileName = name + "." + currentIndex.ToString(
                        "d" + ((object[])element).Length.ToString().Length) + ext;
                }
                catch
                {
                    saveFileDialog.FileName = name + "." + currentIndex.ToString("d4") + ext;
                }
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                textBoxCurrent.Text = saveFileDialog.FileName;
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = settings.LastRomPath;
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
        private void browseAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = settings.LastDirectory;
            if (this.Text.Substring(0, 6) == "EXPORT")
                folderBrowserDialog.Description = "Select directory to export to";
            else
                folderBrowserDialog.Description = "Select directory to import from";
            // Display the openFile dialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            settings.LastDirectory = folderBrowserDialog.SelectedPath;
            textBoxAll.Text = folderBrowserDialog.SelectedPath;
            fullPath = textBoxAll.Text;
            buttonOK.Enabled = true;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            #region Levels
            if (this.Text == "EXPORT LEVEL DATA...")
            {
                this.Enabled = false;
                if (radioButtonCurrent.Checked)
                {
                    // create the serialized level
                    SerializedLevel sLevel = new SerializedLevel();
                    sLevel.LevelLayer = Model.Levels[currentIndex].Layer;
                    sLevel.LevelMapNum = Model.Levels[currentIndex].LevelMap;
                    LevelMap lMap = Model.LevelMaps[Model.Levels[currentIndex].LevelMap];
                    sLevel.LevelMap = lMap;// Add it to serialized level data object
                    sLevel.TilesetL1 = Model.Tilesets[lMap.TilesetL1 + 0x20];
                    sLevel.TilesetL2 = Model.Tilesets[lMap.TilesetL2 + 0x20];
                    sLevel.TilesetL3 = Model.Tilesets[lMap.TilesetL3];
                    sLevel.TilemapL1 = Model.Tilemaps[lMap.TilemapL1 + 0x40];
                    sLevel.TilemapL2 = Model.Tilemaps[lMap.TilemapL2 + 0x40];
                    sLevel.TilemapL3 = Model.Tilemaps[lMap.TilemapL3];
                    sLevel.SolidityMap = Model.SolidityMaps[lMap.SolidityMap];
                    sLevel.LevelNPCs = Model.Levels[currentIndex].LevelNPCs;
                    sLevel.LevelExits = Model.Levels[currentIndex].LevelExits;
                    sLevel.LevelEvents = Model.Levels[currentIndex].LevelEvents;
                    sLevel.LevelOverlaps = Model.Levels[currentIndex].LevelOverlaps;
                    // finally export the serialized levels
                    Do.Export(sLevel, null, fullPath);
                }
                else
                {
                    // create the serialized level
                    SerializedLevel[] sLevels = new SerializedLevel[510];
                    for (int i = 0; i < sLevels.Length; i++)
                    {
                        sLevels[i] = new SerializedLevel();
                        sLevels[i].LevelLayer = Model.Levels[i].Layer;
                        sLevels[i].LevelMapNum = Model.Levels[i].LevelMap;
                        LevelMap lMap = Model.LevelMaps[Model.Levels[i].LevelMap];
                        sLevels[i].LevelMap = lMap;// Add it to serialized level data object
                        sLevels[i].TilesetL1 = Model.Tilesets[lMap.TilesetL1 + 0x20];
                        sLevels[i].TilesetL2 = Model.Tilesets[lMap.TilesetL2 + 0x20];
                        sLevels[i].TilesetL3 = Model.Tilesets[lMap.TilesetL3];
                        sLevels[i].TilemapL1 = Model.Tilemaps[lMap.TilemapL1 + 0x40];
                        sLevels[i].TilemapL2 = Model.Tilemaps[lMap.TilemapL2 + 0x40];
                        sLevels[i].TilemapL3 = Model.Tilemaps[lMap.TilemapL3];
                        sLevels[i].SolidityMap = Model.SolidityMaps[lMap.SolidityMap];
                        sLevels[i].LevelNPCs = Model.Levels[i].LevelNPCs;
                        sLevels[i].LevelExits = Model.Levels[i].LevelExits;
                        sLevels[i].LevelEvents = Model.Levels[i].LevelEvents;
                        sLevels[i].LevelOverlaps = Model.Levels[i].LevelOverlaps;
                    }
                    // finally export the serialized levels
                    Do.Export(sLevels,
                        fullPath + "\\" + Model.GetFileNameWithoutPath() + " - Levels\\" + "level", "LEVEL", true);
                }
            }
            if (this.Text == "IMPORT LEVEL DATA...")
            {
                this.Enabled = false;
                if (radioButtonCurrent.Checked)
                {
                    SerializedLevel sLevel = new SerializedLevel();
                    try
                    {
                        sLevel = (SerializedLevel)Do.Import(sLevel, fullPath);
                    }
                    catch
                    {
                        MessageBox.Show("File not a level data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    Model.Levels[currentIndex].Layer = sLevel.LevelLayer;
                    Model.Levels[currentIndex].Layer.Index = currentIndex;
                    Model.Levels[currentIndex].LevelMap = sLevel.LevelMapNum;
                    LevelMap lMap = sLevel.LevelMap;
                    Model.LevelMaps[Model.Levels[currentIndex].LevelMap] = lMap;
                    Model.Tilesets[lMap.TilesetL1 + 0x20] = sLevel.TilesetL1;
                    Model.Tilesets[lMap.TilesetL2 + 0x20] = sLevel.TilesetL2;
                    Model.Tilesets[lMap.TilesetL3] = sLevel.TilesetL3;
                    Model.EditTilesets[lMap.TilesetL1 + 0x20] = true;
                    Model.EditTilesets[lMap.TilesetL2 + 0x20] = true;
                    Model.EditTilesets[lMap.TilesetL3] = true;
                    Model.Tilemaps[lMap.TilemapL1 + 0x40] = sLevel.TilemapL1;
                    Model.Tilemaps[lMap.TilemapL2 + 0x40] = sLevel.TilemapL2;
                    Model.Tilemaps[lMap.TilemapL3] = sLevel.TilemapL3;
                    Model.EditTilemaps[lMap.TilemapL1 + 0x40] = true;
                    Model.EditTilemaps[lMap.TilemapL2 + 0x40] = true;
                    Model.EditTilemaps[lMap.TilemapL3] = true;
                    Model.SolidityMaps[lMap.SolidityMap] = sLevel.SolidityMap;
                    Model.EditSolidityMaps[lMap.SolidityMap] = true;
                    Model.Levels[currentIndex].LevelNPCs = sLevel.LevelNPCs;
                    Model.Levels[currentIndex].LevelExits = sLevel.LevelExits;
                    Model.Levels[currentIndex].LevelEvents = sLevel.LevelEvents;
                    Model.Levels[currentIndex].LevelOverlaps = sLevel.LevelOverlaps;
                    Model.Levels[currentIndex].LevelNPCs.Index = currentIndex;
                    Model.Levels[currentIndex].LevelExits.Index = currentIndex;
                    Model.Levels[currentIndex].LevelEvents.Index = currentIndex;
                    Model.Levels[currentIndex].LevelOverlaps.Index = currentIndex;
                }
                else
                {
                    SerializedLevel[] sLevels = new SerializedLevel[510];
                    for (int i = 0; i < sLevels.Length; i++)
                        sLevels[i] = new SerializedLevel();
                    try
                    {
                        Do.Import(sLevels, fullPath + "\\" + "level", "LEVEL", true);
                    }
                    catch
                    {
                        MessageBox.Show("One or more files not a level data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    for (int i = 0; i < sLevels.Length; i++)
                    {
                        Model.Levels[i].Layer = sLevels[i].LevelLayer;
                        Model.Levels[i].Layer.Index = currentIndex;
                        Model.Levels[i].LevelMap = sLevels[i].LevelMapNum;
                        LevelMap lMap = sLevels[i].LevelMap;
                        Model.LevelMaps[Model.Levels[i].LevelMap] = lMap;
                        Model.Tilesets[lMap.TilesetL1 + 0x20] = sLevels[i].TilesetL1;
                        Model.Tilesets[lMap.TilesetL2 + 0x20] = sLevels[i].TilesetL2;
                        Model.Tilesets[lMap.TilesetL3] = sLevels[i].TilesetL3;
                        Model.EditTilesets[lMap.TilesetL1 + 0x20] = true;
                        Model.EditTilesets[lMap.TilesetL2 + 0x20] = true;
                        Model.EditTilesets[lMap.TilesetL3] = true;
                        Model.Tilemaps[lMap.TilemapL1 + 0x40] = sLevels[i].TilemapL1;
                        Model.Tilemaps[lMap.TilemapL2 + 0x40] = sLevels[i].TilemapL2;
                        Model.Tilemaps[lMap.TilemapL3] = sLevels[i].TilemapL3;
                        Model.EditTilemaps[lMap.TilemapL1 + 0x40] = true;
                        Model.EditTilemaps[lMap.TilemapL2 + 0x40] = true;
                        Model.EditTilemaps[lMap.TilemapL3] = true;
                        Model.SolidityMaps[lMap.SolidityMap] = sLevels[i].SolidityMap;
                        Model.EditSolidityMaps[lMap.SolidityMap] = true;
                        Model.Levels[i].LevelNPCs = sLevels[i].LevelNPCs;
                        Model.Levels[i].LevelExits = sLevels[i].LevelExits;
                        Model.Levels[i].LevelEvents = sLevels[i].LevelEvents;
                        Model.Levels[i].LevelOverlaps = sLevels[i].LevelOverlaps;
                        Model.Levels[i].LevelNPCs.Index = currentIndex;
                        Model.Levels[i].LevelExits.Index = currentIndex;
                        Model.Levels[i].LevelEvents.Index = currentIndex;
                        Model.Levels[i].LevelOverlaps.Index = currentIndex;
                    }
                }
            }
            #endregion
            #region Battlefields
            if (this.Text == "EXPORT BATTLEFIELDS...")
            {
                Battlefield[] battlefields = Model.Battlefields;
                SerializedBattlefield[] serialized = new SerializedBattlefield[battlefields.Length];
                PaletteSet[] paletteSets = Model.PaletteSetsBF;
                int i = 0;
                foreach (Battlefield battlefield in battlefields)
                    serialized[i] = new SerializedBattlefield(Model.TilesetsBF[battlefields[i].TileSet],
                        paletteSets[battlefields[i++].PaletteSet], battlefield);
                if (radioButtonCurrent.Checked)
                    Do.Export(serialized[currentIndex], null, fullPath);
                else
                    Do.Export(serialized,
                        fullPath + "\\" + Model.GetFileNameWithoutPath() + " - Battlefields\\" + "battlefield",
                        "BATTLEFIELD", true);
            }
            if (this.Text == "IMPORT BATTLEFIELDS...")
            {
                Battlefield[] battlefields = Model.Battlefields;
                if (radioButtonCurrent.Checked)
                {
                    SerializedBattlefield battlefield = new SerializedBattlefield();
                    try
                    {
                        battlefield = (SerializedBattlefield)Do.Import(battlefield, fullPath);
                    }
                    catch
                    {
                        MessageBox.Show("File not a battlefield data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    Model.TilesetsBF[battlefields[currentIndex].TileSet] = battlefield.tileset;
                    battlefields[currentIndex].GraphicSetA = battlefield.graphicSetA;
                    battlefields[currentIndex].GraphicSetB = battlefield.graphicSetB;
                    battlefields[currentIndex].GraphicSetC = battlefield.graphicSetC;
                    battlefields[currentIndex].GraphicSetD = battlefield.graphicSetD;
                    battlefields[currentIndex].GraphicSetE = battlefield.graphicSetE;
                    Model.PaletteSetsBF[battlefields[currentIndex].PaletteSet] = battlefield.paletteSet;
                    battlefields[currentIndex].Index = currentIndex;
                }
                else
                {
                    SerializedBattlefield[] battlefield = new SerializedBattlefield[battlefields.Length];
                    for (int i = 0; i < battlefield.Length; i++)
                        battlefield[i] = new SerializedBattlefield();
                    try
                    {
                        Do.Import(battlefield, fullPath + "\\" + "battlefield", "BATTLEFIELD", true);
                    }
                    catch
                    {
                        MessageBox.Show("One or more files not a battlefield data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    for (int i = 0; i < battlefield.Length; i++)
                    {
                        Model.TilesetsBF[battlefields[i].TileSet] = battlefield[i].tileset;
                        battlefields[i].GraphicSetA = battlefield[i].graphicSetA;
                        battlefields[i].GraphicSetB = battlefield[i].graphicSetB;
                        battlefields[i].GraphicSetC = battlefield[i].graphicSetC;
                        battlefields[i].GraphicSetD = battlefield[i].graphicSetD;
                        battlefields[i].GraphicSetE = battlefield[i].graphicSetE;
                        Model.PaletteSetsBF[battlefields[i].PaletteSet] = battlefield[i].paletteSet;
                        battlefields[i].Index = i;
                    }
                }
            }
            #endregion
            #region Audio
            if (this.Text == "EXPORT SAMPLE BRRs...")
            {
                if (radioButtonCurrent.Checked)
                    Do.Export(Model.AudioSamples[currentIndex].Sample,
                        "sampleBRR." + currentIndex.ToString("d3") + ".brr", fullPath);
                else
                {
                    byte[][] samples = new byte[Model.AudioSamples.Length][];
                    int i = 0;
                    foreach (BRRSample s in Model.AudioSamples)
                        samples[i++] = s.Sample;
                    Do.Export(samples,
                        fullPath + "\\" + Model.GetFileNameWithoutPath() + " - BRR Samples\\" + "sampleBRR",
                        "BRR SAMPLE", true);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (this.Text == "IMPORT SAMPLE BRRs...")
            {
                if (radioButtonCurrent.Checked)
                {
                    try
                    {
                        byte[] sample = (byte[])Do.Import(new byte[1], fullPath);
                        Model.AudioSamples[currentIndex].Sample = sample;
                    }
                    catch
                    {
                        MessageBox.Show("Error importing .brr file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    byte[][] samples = new byte[Model.AudioSamples.Length][];
                    try
                    {
                        Do.Import(samples, fullPath + "\\" + "sampleBRR", "BRR SAMPLE", true);
                    }
                    catch
                    {
                        MessageBox.Show("Error importing .brr file(s).", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    int i = 0;
                    foreach (BRRSample sample in Model.AudioSamples)
                        sample.Sample = samples[i++];
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (this.Text == "EXPORT SAMPLE WAVs...")
            {
                if (radioButtonCurrent.Checked)
                    Do.Export(BRR.BRRToWAV(Model.AudioSamples[currentIndex].Sample, (int)args[0]),
                        "sampleWAV." + currentIndex.ToString("d3") + ".wav", fullPath);
                else
                {
                    byte[][] samples = new byte[Model.AudioSamples.Length][];
                    int i = 0;
                    foreach (BRRSample s in Model.AudioSamples)
                        samples[i++] = BRR.BRRToWAV(s.Sample, (int)args[0]);
                    Do.Export(samples,
                        fullPath + "\\" + Model.GetFileNameWithoutPath() + " - WAV Samples\\" + "sampleWAV",
                        "SAMPLE", true);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (this.Text == "IMPORT SAMPLE WAVs...")
            {
                if (radioButtonCurrent.Checked)
                {
                    try
                    {
                        byte[] sample = (byte[])Do.Import(new byte[1], fullPath);
                        Model.AudioSamples[currentIndex].Sample = BRR.Encode(sample);
                    }
                    catch
                    {
                        MessageBox.Show("Error encoding .wav file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    byte[][] samples = new byte[Model.AudioSamples.Length][];
                    try
                    {
                        Do.Import(samples, fullPath + "\\" + "sampleWAV", "WAV SAMPLE", true);
                    }
                    catch
                    {
                        MessageBox.Show("Error encoding .wav file(s).", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    int i = 0;
                    foreach (BRRSample sample in Model.AudioSamples)
                        sample.Sample = BRR.Encode(samples[i++]);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (this.Text == "EXPORT SPCs...")
            {
                if (radioButtonCurrent.Checked)
                    Do.Export(Model.SPCs[currentIndex], null, textBoxCurrent.Text);
                else
                    Do.Export(Model.SPCs, fullPath + "\\" + Model.GetFileNameWithoutPath() + " - SPCs\\" + "spc", "SPC", true);
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (this.Text == "IMPORT SPCs...")
            {
                if (radioButtonCurrent.Checked)
                {
                    SPCTrack spc = new SPCTrack();
                    try
                    {
                        spc = (SPCTrack)Do.Import(spc, fullPath);
                    }
                    catch
                    {
                        MessageBox.Show("File not an SPC data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    spc.CreateCommands();
                    Model.SPCs[currentIndex] = spc;
                }
                else
                {
                    SPCTrack[] spcs = new SPCTrack[Model.SPCs.Length];
                    for (int i = 0; i < spcs.Length; i++)
                        spcs[i] = new SPCTrack();
                    try
                    {
                        Do.Import(spcs, fullPath + "\\" + "spc", "SPC", true);
                    }
                    catch
                    {
                        MessageBox.Show("One or more files not an SPC data file.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    for (int i = 0; i < spcs.Length; i++)
                    {
                        if (spcs[i].SPCData == null)
                            continue;
                        Model.SPCs[i] = spcs[i];
                        Model.SPCs[i].CreateCommands();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            #endregion
            #region Other
            try
            {
                Element[] array = (Element[])element;
                string name = this.Text.ToLower().Substring(7, this.Text.Length - 7 - 4);
                if (this.Text.Substring(0, 6) == "EXPORT")
                {
                    if (radioButtonCurrent.Checked)
                        Do.Export(array[currentIndex], null, textBoxCurrent.Text);
                    else
                        Do.Export(array,
                            fullPath + "\\" + Model.GetFileNameWithoutPath() + " - " +
                            Lists.ToTitleCase(name) + "s" + "\\" + name,
                            name.ToUpper(), true);
                }
                if (this.Text.Substring(0, 6) == "IMPORT")
                {
                    if (radioButtonCurrent.Checked)
                    {
                        try
                        {
                            array[currentIndex] = (Element)Do.Import(array[currentIndex], fullPath);
                        }
                        catch
                        {
                            MessageBox.Show("Incorrect data file type.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        array[currentIndex].Index = currentIndex;
                    }
                    else
                    {
                        try
                        {
                            Do.Import(array, fullPath + "\\" + name, name.ToUpper(), true);
                        }
                        catch
                        {
                            MessageBox.Show("One or more files incorrect data file type.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        int i = 0;
                        foreach (Element item in array)
                        {
                            item.Index = i++;
                        }
                    }
                }
            }
            catch { }
            #endregion
            this.Tag = radioButtonAll.Checked;
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
