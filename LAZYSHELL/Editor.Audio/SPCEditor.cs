using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor.Commands;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class SPCEditor : NewForm
    {
        #region Variables
        public int Index { get { return (int)trackNum.Value; } set { trackNum.Value = value; } }
        public int Type { get { return soundType.SelectedIndex; } set { soundType.SelectedIndex = value; } }
        public SoundPlayer SoundPlayer = new SoundPlayer();
        private SPC[] spcs;
        private SPC spc { get { return spcs[Index]; } set { spcs[Index] = value; } }
        private Settings settings = Settings.Default;
        private SPCCommand mouseDownSSC;
        private SPCCommand mouseOverSSC;
        private int mouseOverChannel;
        private int mouseDownChannel;
        private bool mouseEnter = false;
        private SPCCommand copiedSSC;
        private Interpreter interpreter = Interpreter.Instance;
        private Previewer previewer;
        private EditLabel labelWindow;
        // control arrays
        private ComboBox[] sampleIndexes;
        private NumericUpDown[] volumes;
        private CheckBox[] activeChannels;
        private CheckBox[] activeInstruments;
        private CommandStack commandStackW;
        private CommandStack commandStackR;
        #endregion
        public SPCEditor()
        {
            InitializeComponent();
            //
            this.Updating = true;
            //
            this.commandStackW = new CommandStack();
            this.commandStackR = new CommandStack();
            labelWindow = new EditLabel(trackName, trackNum, "Songs", true);
            newCommands.Items.AddRange(Lists.SPCCommands);
            percussivePitch.Items.AddRange(Lists.Pitches);
            sampleIndexes = new ComboBox[20];
            volumes = new NumericUpDown[20];
            activeInstruments = new CheckBox[20];
            for (int i = 0; i < sampleIndexes.Length; i++)
            {
                sampleIndexes[i] = new ComboBox();
                sampleIndexes[i].DropDownStyle = ComboBoxStyle.DropDownList;
                sampleIndexes[i].Enabled = false;
                sampleIndexes[i].FormattingEnabled = true;
                sampleIndexes[i].Items.AddRange(Lists.Numerize(Lists.SampleNames));
                sampleIndexes[i].Location = new Point(22, i * 21 + 42);
                sampleIndexes[i].Name = "sampleIndex" + i;
                sampleIndexes[i].Size = new Size(182, 21);
                sampleIndexes[i].TabIndex = i;
                sampleIndexes[i].Tag = i;
                sampleIndexes[i].SelectedIndexChanged += new EventHandler(sampleIndex_SelectedIndexChanged);
                this.groupBoxI.Controls.Add(sampleIndexes[i]);
                //
                volumes[i] = new NumericUpDown();
                volumes[i].Enabled = false;
                volumes[i].Location = new Point(208, i * 21 + 42);
                volumes[i].Maximum = 127;
                volumes[i].Name = "volume" + i;
                volumes[i].Size = new Size(50, 21);
                volumes[i].TabIndex = i;
                volumes[i].Tag = i;
                volumes[i].TextAlign = HorizontalAlignment.Right;
                volumes[i].ValueChanged += new EventHandler(volume_ValueChanged);
                this.groupBoxI.Controls.Add(volumes[i]);
                //
                activeInstruments[i] = new CheckBox();
                activeInstruments[i].AutoSize = true;
                activeInstruments[i].Location = new Point(6, i * 21 + 46);
                activeInstruments[i].Tag = i;
                activeInstruments[i].CheckedChanged += new EventHandler(activeInstrument_CheckedChanged);
                this.groupBoxI.Controls.Add(activeInstruments[i]);
            }
            //
            if (settings.RememberLastIndex)
                Type = settings.LastSoundType;
            percussiveName.Items.AddRange(Lists.Numerize(Lists.SampleNames));
            //
            if (Type == 0)
            {
                spcs = Model.SPCs;
                trackName.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                labelWindow.SetElement("Songs");
            }
            else if (Type == 1)
            {
                spcs = Model.SPCEvent;
                trackName.Items.AddRange(Lists.Numerize(Lists.SoundNames));
                labelWindow.SetElement("Sound FX (Event)");
            }
            else
            {
                spcs = Model.SPCBattle;
                trackName.Items.AddRange(Lists.Numerize(Lists.BattleSoundNames));
                labelWindow.SetElement("Sound FX (Battle)");
            }
            trackNum.Maximum = spcs.Length - 1;
            if (settings.RememberLastIndex)
                Index = settings.LastSPC;
            trackName.SelectedIndex = Index;
            //
            activeChannels = new CheckBox[8];
            for (int i = 0; i < 8; i++)
            {
                activeChannels[i] = new CheckBox();
                activeChannels[i].Location = new Point(6, i * 36 + 52);
                activeChannels[i].Tag = i;
                activeChannels[i].CheckedChanged += new EventHandler(activeChannel_CheckedChanged);
                if (i >= 2)
                    activeChannels[i].Visible = Type == 0;
                groupBoxCT.Controls.Add(activeChannels[i]);
            }
            clefSV.SelectedIndex = 0;
            keySV.SelectedIndex = 0;
            //
            RefreshSPC();
            this.Updating = false;
            //
            this.History = new History(this, trackName, trackNum);
        }
        #region Functions
        private void RefreshSPC()
        {
            this.Updating = true;
            //
            importSPC.Enabled = Index != 0 || Type != 0;
            exportSPC.Enabled = Index != 0 || Type != 0;
            importMML.Enabled = Index != 0 && Type == 0;
            exportMML.Enabled = Index != 0 && Type == 0;
            clear.Enabled = Index != 0 || Type != 0;
            reset.Enabled = Index != 0 || Type != 0;
            // Reverberation
            groupBoxRV.Enabled = Index != 0 && Type == 0;
            delayTime.Value = spc.DelayTime;
            decayFactor.Value = spc.DecayFactor;
            echo.Value = (sbyte)spc.Echo;
            // Instruments
            groupBoxI.Enabled = Index != 0 && Type == 0;
            for (int i = 0; i < sampleIndexes.Length; i++)
            {
                if (Index == 0 || Type != 0 || spc.Samples[i] == null)
                {
                    activeInstruments[i].Checked = false;
                    sampleIndexes[i].SelectedIndex = 0;
                    volumes[i].Value = 0;
                }
                else
                {
                    activeInstruments[i].Checked = spc.Samples[i].Active;
                    sampleIndexes[i].SelectedIndex = spc.Samples[i].Sample;
                    volumes[i].Value = spc.Samples[i].Volume;
                }
                sampleIndexes[i].Enabled = activeInstruments[i].Checked;
                volumes[i].Enabled = activeInstruments[i].Checked;
            }
            // Percussives
            groupBoxPR.Enabled = Index != 0 && Type == 0;
            percussives.Items.Clear();
            if (Index != 0 && Type == 0)
            {
                for (int i = 0; i < spc.Percussives.Count; i++)
                {
                    int index = spc.Percussives[i].Sample;
                    percussives.Items.Add(Lists.Numerize(Lists.SampleNames[index], index, 3));
                }
            }
            if (percussives.Items.Count > 0)
                percussives.SelectedIndex = 0;
            RefreshPercussive();
            // Track editor
            if (Index != 0 || Type != 0)
                spc.CreateNotes();
            groupBoxCT.Enabled = Index != 0 || Type != 0;
            groupBoxSV.Enabled = Index != 0 && Type == 0;
            hScrollBarCT.Maximum = 0;
            hScrollBarSV.Maximum = 0;
            mouseDownSSC = null;
            ControlDisassemble();
            this.Updating = true;
            if (Index != 0 || Type != 0)
            {
                for (int i = 0; i < spc.Channels.Length; i++)
                {
                    activeChannels[i].Checked = spc.ActiveChannels[i];
                    if (!spc.ActiveChannels[i])
                        continue;
                    rToolStripNote.Enabled = true;
                    rToolStripAct.Enabled = true;
                    hScrollBarCT.SmallChange = 24;
                    hScrollBarCT.LargeChange = 24 * 4;
                    if (spc.Channels[i].Count * 24 - channelTracks.Width > hScrollBarCT.Maximum)
                        hScrollBarCT.Maximum = spc.Channels[i].Count * 24;
                    int maximum = 0;
                    if (spc.Notes != null)
                        foreach (Note note in spc.Notes[i])
                        {
                            maximum += note.Ticks;
                            if (maximum > hScrollBarSV.Maximum)
                                hScrollBarSV.Maximum = maximum;
                        }
                }
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
            CalculateFreeSpace(Type, true, false);
            //
            this.Updating = false;
        }
        private void RefreshPercussive()
        {
            percussivePitchIndex.Enabled = spc.Percussives != null && spc.Percussives.Count > 0;
            percussiveName.Enabled = spc.Percussives != null && spc.Percussives.Count > 0;
            percussivePitch.Enabled = spc.Percussives != null && spc.Percussives.Count > 0;
            percussiveVolume.Enabled = spc.Percussives != null && spc.Percussives.Count > 0;
            percussiveBalance.Enabled = spc.Percussives != null && spc.Percussives.Count > 0;
            if (spc.Percussives == null || spc.Percussives.Count == 0 || percussives.SelectedIndex >= spc.Percussives.Count)
            {
                percussivePitchIndex.SelectedIndex = 0;
                percussiveName.SelectedIndex = 0;
                percussivePitch.SelectedIndex = 0;
                percussiveVolume.Value = 0;
                percussiveBalance.Value = 0;
            }
            else
            {
                percussivePitchIndex.SelectedIndex = (int)spc.Percussives[percussives.SelectedIndex].PitchIndex;
                percussiveName.SelectedIndex = spc.Percussives[percussives.SelectedIndex].Sample;
                percussivePitch.SelectedIndex = spc.Percussives[percussives.SelectedIndex].Pitch;
                percussiveVolume.Value = spc.Percussives[percussives.SelectedIndex].Volume;
                percussiveBalance.Value = spc.Percussives[percussives.SelectedIndex].Balance;
            }
        }
        //
        public bool CalculateFreeSpace(int type, bool label, bool warning)
        {
            int offset = 0;
            int endOffset = 0;
            if (type == 0)
            {
                offset = 0x045526;
                endOffset = 0x060939;
                foreach (SPCTrack spc in Model.SPCs)
                {
                    if (spc == null || spc.Samples == null || spc.SPCData == null)
                        continue;
                    offset += 3; // Reverb
                    foreach (SampleIndex sample in spc.Samples)
                        if (sample != null && sample.Active)
                            offset += 2;
                    offset++; // 0xFF
                    offset += 2; // SPC size
                    offset += spc.SPCData.Length;
                    for (int i = 0; i < spc.Channels.Length; i++)
                    {
                        if (!spc.ActiveChannels[i] || spc.Channels[i] == null || spc.Channels[i].Count == 0)
                            continue;
                        SPCCommand lastSSC = spc.Channels[i][spc.Channels[i].Count - 1];
                        if (lastSSC.Opcode != 0xD0 && lastSSC.Opcode != 0xCD && lastSSC.Opcode != 0xCE)
                            offset++;
                    }
                }
            }
            else if (type == 1)
            {
                offset = 0x042C26;
                endOffset = 0x043E26;
                foreach (SPCSound sound in Model.SPCEvent)
                {
                    if (sound == null || sound.Channels == null)
                        continue;
                    offset += sound.Length();
                }
            }
            else
            {
                offset = 0x044226;
                endOffset = 0x045426;
                foreach (SPCSound sound in Model.SPCBattle)
                {
                    if (sound == null || sound.Channels == null)
                        continue;
                    offset += sound.Length();
                }
            }
            int left = endOffset - offset;
            if (label)
            {
                freeSpace.Text = left.ToString() + " available bytes";
                freeSpace.ForeColor = left < 0 ? Color.Red : SystemColors.ControlText;
            }
            if (warning && left < 0)
            {
                if (type == 0)
                    MessageBox.Show("Not enough space to save all SPCs.", "LAZY SHELL");
                if (type == 1)
                    MessageBox.Show("Not enough space to save all event sound effects.", "LAZY SHELL");
                if (type == 2)
                    MessageBox.Show("Not enough space to save all battle sound effects.", "LAZY SHELL");
            }
            return left >= 0;
        }
        private void ResizePanels()
        {
            if (scoreViewer.Checked)
            {
                groupBoxSV.Width = (int)(0.57 * (double)panelSPC.Width);
                groupBoxCT.Width = (int)(0.43 * (double)panelSPC.Width - 6.0);
                groupBoxCT.Left = (int)(0.57 * (double)panelSPC.Width + 6.0);
            }
            else
            {
                groupBoxCT.Width = panelSPC.Width - 542;
                groupBoxCT.Left = 542;
            }
        }
        private void ControlDisassemble()
        {
            this.Updating = true;
            labelOpcode1.Text = "Opcode 1";
            labelParameter1.Text = "Parameter 1";
            labelParameter2.Text = "Parameter 2";
            labelParameter3.Text = "Parameter 3";
            labelBeat.Text = "Beat";
            opcodeByte1.Value = 0;
            opcodeByte1.Maximum = 255;
            opcodeByte1.Enabled = false;
            opcodeByte1.Hexadecimal = true;
            parameterByte1.Value = 0;
            parameterByte1.Maximum = 255;
            parameterByte1.Enabled = false;
            parameterByte1.BringToFront();
            parameterByte2.Value = 0;
            parameterByte2.Maximum = 255;
            parameterByte2.Enabled = false;
            parameterByte2.BringToFront();
            parameterByte3.Value = 0;
            parameterByte3.Maximum = 255;
            parameterByte3.Enabled = false;
            parameterByte3.BringToFront();
            parameterName1.Enabled = false;
            parameterName1.Items.Clear();
            parameterName2.Enabled = false;
            parameterName2.Items.Clear();
            parameterName3.Enabled = false;
            parameterName3.Items.Clear();
            panelParameters.BringToFront();
            // notes
            noteNames.Enabled = false;
            //noteNames.Items.Clear();
            noteLengthName.Enabled = false;
            //noteLengthName.Items.Clear();
            noteLengthName.BringToFront();
            noteLengthByte.Enabled = false;
            noteLengthByte.Value = 0;
            //
            if (mouseDownSSC != null && activeChannels[mouseDownSSC.Channel].Checked)
                switch (mouseDownSSC.Opcode)
                {
                    case 0xC4:
                    case 0xC5:
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        break;
                    case 0xC6:
                        labelParameter1.Text = "Octave = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Maximum = 8;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xC8: labelParameter1.Text = "Channels"; goto case 0xF6;
                    case 0xCD:
                    case 0xCE:
                        labelParameter1.Text = "Sound";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterName1.Enabled = true;
                        if (Type < 2)
                            parameterName1.Items.AddRange(Lists.Numerize(Lists.SoundNames));
                        else
                            parameterName1.Items.AddRange(Lists.Numerize(Lists.BattleSoundNames));
                        parameterName1.SelectedIndex = mouseDownSSC.Param1;
                        parameterName1.BringToFront();
                        break;
                    case 0xCF: labelParameter1.Text = "1/16 pitch = "; goto case 0xF2;
                    case 0xD1: labelParameter1.Text = "Beat duration = "; goto case 0xF6;
                    case 0xD2: labelParameter1.Text = "ARAM $69 = "; goto case 0xF6;
                    case 0xD4: labelParameter1.Text = "Loop count = "; goto case 0xF6;
                    case 0xD9:
                        labelParameter1.Text = "ADSR attack = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 15;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xDA:
                        labelParameter1.Text = "ADSR decay = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 7;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xDB:
                        labelParameter1.Text = "ADSR sustain = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 7;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xDC:
                        labelParameter1.Text = "ADSR release = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 31;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xDD:
                        labelParameter1.Text = "Sample length = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 15;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xDE:
                        labelParameter1.Text = "Sample = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterName1.Enabled = true;
                        parameterName1.BringToFront();
                        parameterName1.Items.AddRange(Lists.Numerize(Lists.SampleNames));
                        parameterName1.SelectedIndex = mouseDownSSC.Param1;
                        break;
                    case 0xDF:
                        labelParameter1.Text = "Pitch = ";
                        labelParameter2.Text = "VOXCON = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Minimum = -16;
                        parameterByte1.Maximum = 15;
                        if (!Bits.GetBit(mouseDownSSC.Param1, 4))
                            parameterByte1.Value = mouseDownSSC.Param1 & 0x0F;
                        else
                            parameterByte1.Value = -(((mouseDownSSC.Param1 & 0x1F) ^ 0x1F) + 1);
                        parameterByte2.Enabled = true;
                        parameterByte2.Value = mouseDownSSC.Param1 >> 5;
                        break;
                    case 0xE0:
                        labelParameter1.Text = "Fadeout = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 31;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xE2:
                        labelParameter1.Text = "Volume = ";
                        parameterByte1.Maximum = 127;
                        goto case 0xF6;
                    case 0xE3:
                        labelParameter1.Text = "Amount = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 127;
                        parameterByte1.Minimum = -128;
                        parameterByte1.Value = (sbyte)mouseDownSSC.Param1;
                        break;
                    case 0xE4:
                    case 0xE5:
                        labelParameter1.Text = "Duration = ";
                        if (mouseDownSSC.Opcode == 0xE4)
                            labelParameter2.Text = "Volume = ";
                        else
                            labelParameter2.Text = "Pitch = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        parameterByte2.Enabled = true;
                        parameterByte2.Maximum = 127;
                        parameterByte2.Minimum = -128;
                        parameterByte2.Value = (sbyte)mouseDownSSC.Param2;
                        break;
                    case 0xE7: labelParameter1.Text = "Balance = "; goto case 0xF6;
                    case 0xE8:
                    case 0xE9:
                        labelParameter1.Text = "Duration = ";
                        if (mouseDownSSC.Opcode == 0xE8)
                            labelParameter2.Text = "End balance = ";
                        else
                            labelParameter2.Text = "Reach = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        parameterByte2.Enabled = true;
                        parameterByte2.Value = mouseDownSSC.Param2;
                        break;
                    case 0xEC:
                    case 0xED: labelParameter1.Text = "1/4 pitch = "; goto case 0xF2;
                    case 0xF0:
                    case 0xF1:
                        labelParameter1.Text = "Amplitude = ";
                        labelParameter2.Text = "Wavelength = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        parameterByte2.Enabled = true;
                        parameterByte2.Value = mouseDownSSC.Param2;
                        if (mouseDownSSC.Opcode == 0xF1)
                        {
                            labelParameter3.Text = "Delay = ";
                            parameterByte3.Enabled = true;
                            parameterByte3.Value = mouseDownSSC.Param3;
                        }
                        break;
                    case 0xF2:
                        if (mouseDownSSC.Opcode == 0xF2)
                            labelParameter1.Text = "Change = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Maximum = 127;
                        parameterByte1.Minimum = -128;
                        parameterByte1.Value = (sbyte)mouseDownSSC.Param1;
                        break;
                    case 0xF4:
                        labelParameter1.Text = "Roughness = ";
                        labelParameter2.Text = "Wavelength = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        parameterByte2.Enabled = true;
                        parameterByte2.Value = mouseDownSSC.Param2;
                        break;
                    case 0xF6:
                        if (mouseDownSSC.Opcode == 0xF6)
                            labelParameter1.Text = "Length = ";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        break;
                    case 0xFC:
                        labelParameter1.Text = "Delay Time";
                        labelParameter2.Text = "Decay Ratio";
                        labelParameter3.Text = "Echo Volume";
                        opcodeByte1.Value = mouseDownSSC.Opcode;
                        parameterByte1.Enabled = true;
                        parameterByte2.Enabled = true;
                        parameterByte3.Enabled = true;
                        parameterByte2.Maximum = 127;
                        parameterByte1.Value = mouseDownSSC.Param1;
                        parameterByte2.Value = mouseDownSSC.Param2;
                        parameterByte3.Value = mouseDownSSC.Param3;
                        break;
                    default:
                        if (mouseDownSSC.Opcode < 0xC4)
                        {
                            panelNotes.BringToFront();
                            noteNames.Enabled = true;
                            noteLengthName.Enabled = mouseDownSSC.Opcode < 0xB6;
                            noteLengthByte.Enabled = mouseDownSSC.Opcode >= 0xB6;
                            //
                            noteNames.SelectedIndex = mouseDownSSC.Opcode % 14;
                            if (mouseDownSSC.Opcode < 0xB6)
                                noteLengthName.SelectedIndex = mouseDownSSC.Opcode / 14;
                            else
                            {
                                labelBeat.Text = "Duration";
                                noteLengthByte.BringToFront();
                                noteLengthByte.Value = mouseDownSSC.Param1;
                            }
                        }
                        else
                        {
                            if (mouseDownSSC.Length > 0)
                            {
                                opcodeByte1.Value = mouseDownSSC.Opcode;
                            }
                            if (mouseDownSSC.Length > 1)
                            {
                                parameterByte1.Enabled = true;
                                parameterByte1.Value = mouseDownSSC.Param1;
                            }
                            if (mouseDownSSC.Length > 2)
                            {
                                parameterByte2.Enabled = true;
                                parameterByte2.Value = mouseDownSSC.Param2;
                            }
                            if (mouseDownSSC.Length > 3)
                            {
                                parameterByte3.Enabled = true;
                                parameterByte3.Value = mouseDownSSC.Param3;
                            }
                        }
                        break;
                }
            CalculateFreeSpace(Type, true, false);
            this.Updating = false;
        }
        private void ControlAssemble()
        {
            if (mouseDownSSC == null)
                return;
            switch (mouseDownSSC.Opcode)
            {
                case 0xCD:
                case 0xCE:
                case 0xDE:
                    mouseDownSSC.Opcode = (byte)opcodeByte1.Value;
                    mouseDownSSC.Param1 = (byte)parameterName1.SelectedIndex;
                    break;
                case 0xDF:
                    mouseDownSSC.Opcode = (byte)opcodeByte1.Value;
                    if (parameterByte1.Value < 0)
                        mouseDownSSC.Param1 = (byte)(0x20 + parameterByte1.Value);
                    else
                        mouseDownSSC.Param1 = (byte)parameterByte1.Value;
                    mouseDownSSC.Param1 |= (byte)((byte)parameterByte2.Value << 5);
                    break;
                case 0xCF:
                case 0xE3:
                case 0xEC:
                case 0xED:
                case 0xF2:
                    mouseDownSSC.Opcode = (byte)opcodeByte1.Value;
                    mouseDownSSC.Param1 = (byte)((sbyte)parameterByte1.Value);
                    break;
                case 0xE4:
                case 0xE5:
                    mouseDownSSC.Opcode = (byte)opcodeByte1.Value;
                    mouseDownSSC.Param1 = (byte)parameterByte1.Value;
                    mouseDownSSC.Param2 = (byte)((sbyte)parameterByte2.Value);
                    break;
                default:
                    if (mouseDownSSC.Opcode < 0xC4)
                    {
                        if (mouseDownSSC.Opcode < 0xB6)
                            mouseDownSSC.Opcode = (byte)(noteLengthName.SelectedIndex * 14);
                        else
                        {
                            mouseDownSSC.Opcode = 0xB6;
                            mouseDownSSC.Param1 = (byte)noteLengthByte.Value;
                        }
                        mouseDownSSC.Opcode += (byte)noteNames.SelectedIndex;
                    }
                    else
                    {
                        if (mouseDownSSC.Length > 0)
                            mouseDownSSC.Opcode = (byte)opcodeByte1.Value;
                        if (mouseDownSSC.Length > 1)
                            mouseDownSSC.Param1 = (byte)parameterByte1.Value;
                        if (mouseDownSSC.Length > 2)
                            mouseDownSSC.Param2 = (byte)parameterByte2.Value;
                        if (mouseDownSSC.Length > 3)
                            mouseDownSSC.Param3 = (byte)parameterByte3.Value;
                    }
                    break;
            }
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            // if changed octave, recreate notes
            if (mouseDownSSC.Opcode == 0xC4 ||
                mouseDownSSC.Opcode == 0xC5 ||
                mouseDownSSC.Opcode == 0xC6)
                spc.CreateNotes();
            CalculateFreeSpace(Type, true, false);
        }
        //
        private bool ImportSPCScript(ref List<SPCCommand> sourceCommands)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import SPC script";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return false;
            string path = openFileDialog.FileName;
            TextReader tr = new StreamReader(path);
            string[] strings = Interpreter.SPCCommands;
            for (int i = 0; i < strings.Length; i++)
                strings[i] = Regex.Replace(strings[i], "\\{[^^]+", "");
            List<SPCCommand> commands = new List<SPCCommand>();
            int octave = 6;
            bool octaveIsSet = false;
            string temp = "";
            string error = "";
            int lineNumber = 1;
            while (tr.Peek() != -1)
            {
                try
                {
                    int opcode = -1;
                    int parameter1 = 0;
                    int parameter2 = 0;
                    int parameter3 = 0;
                    int length = 0;
                    byte[] command;
                    string line = tr.ReadLine();
                    if (line == "") // just skip line if empty, no errors
                        continue;
                    temp = line;
                    #region Notes
                    if (line.StartsWith("Note "))
                    {
                        line = line.Remove(0, 5); // remove "Note "
                        Beat beat = Beat.NULL;
                        Pitch pitch = Pitch.C;
                        int duration = 0;
                        // see if it's a note to play
                        if (line.StartsWith("play: "))
                        {
                            line = line.Remove(0, 6); // remove "play: "
                            bool Ab = line.StartsWith("Ab");
                            // Find pitch based on letter key
                            if (line.StartsWith("Ab")) { pitch = Pitch.Gs; line = line.Remove(0, 2); }
                            else if (line.StartsWith("A#") || line.StartsWith("Bb")) { pitch = Pitch.As; line = line.Remove(0, 2); }
                            else if (line.StartsWith("A")) { pitch = Pitch.A; line = line.Remove(0, 1); }
                            else if (line.StartsWith("B") || line.StartsWith("Cb")) { pitch = Pitch.B; line = line.Remove(0, 1); }
                            else if (line.StartsWith("C#") || line.StartsWith("Db")) { pitch = Pitch.Cs; line = line.Remove(0, 2); }
                            else if (line.StartsWith("C")) { pitch = Pitch.C; line = line.Remove(0, 1); }
                            else if (line.StartsWith("D#") || line.StartsWith("Eb")) { pitch = Pitch.Ds; line = line.Remove(0, 2); }
                            else if (line.StartsWith("D")) { pitch = Pitch.D; line = line.Remove(0, 1); }
                            else if (line.StartsWith("E#")) { pitch = Pitch.F; line = line.Remove(0, 2); }
                            else if (line.StartsWith("E") || line.StartsWith("Fb")) { pitch = Pitch.E; line = line.Remove(0, 1); }
                            else if (line.StartsWith("F#") || line.StartsWith("Gb")) { pitch = Pitch.Fs; line = line.Remove(0, 2); }
                            else if (line.StartsWith("F")) { pitch = Pitch.F; line = line.Remove(0, 1); }
                            else if (line.StartsWith("G#")) { pitch = Pitch.Gs; line = line.Remove(0, 2); }
                            else if (line.StartsWith("G")) { pitch = Pitch.G; line = line.Remove(0, 1); }
                            // Check if octave set in note (only 9 octaves)
                            if (Regex.IsMatch(line[0].ToString(), "[0-8]"))
                            {
                                // if different from current octave, change octave
                                byte value = Convert.ToByte(line[0].ToString(), 10);
                                // Only if not Ab0, b/c Ab0 doesn't exist
                                if (Ab && octave != 0)
                                    octave--;
                                else if (Ab && octave == 0)
                                    continue; // Skip because Ab0 not valid note, thus not valid command
                                if (value != octave || !octaveIsSet)
                                {
                                    if (!octaveIsSet) // if first note, must set current octave
                                    {
                                        command = new byte[] { 0xC6, value };
                                        octave = value;
                                        octaveIsSet = true;
                                    }
                                    else if (value == octave + 1)
                                    {
                                        command = new byte[] { 0xC4 };
                                        octave++;
                                    }
                                    else if (value == octave - 1)
                                    {
                                        command = new byte[] { 0xC5 };
                                        octave--;
                                    }
                                    else
                                    {
                                        command = new byte[] { 0xC6, value };
                                        octave = value;
                                    }
                                    if (Type == 0)
                                        commands.Add(new SPCCommand(command, (SPCTrack)this.spc, mouseOverChannel));
                                    else
                                        commands.Add(new SPCCommand(command, (SPCSound)this.spc, mouseOverChannel));
                                }
                                line = line.Remove(0, 1);
                            }
                            line = line.Remove(0, 2); // remove ", "
                        }
                        else if (line.StartsWith("rest, "))
                        {
                            pitch = Pitch.Rest;
                            line = line.Remove(0, 6); // remove "rest, "
                        }
                        else if (line.StartsWith("tie, "))
                        {
                            pitch = Pitch.Tie;
                            line = line.Remove(0, 5); // remove "tie, "
                        }
                        // now check if beat or fixed duration
                        if (line.StartsWith("beat: "))
                        {
                            line = line.Remove(0, 6);
                            switch (line)
                            {
                                case "whole": beat = Beat.Whole; break;
                                case "half.": beat = Beat.HalfDotted; break;
                                case "half": beat = Beat.Half; break;
                                case "quarter.": beat = Beat.QuarterDotted; break;
                                case "quarter": beat = Beat.Quarter; break;
                                case "8th.": beat = Beat.EighthDotted; break;
                                case "triplet quarter": beat = Beat.QuarterTriplet; break;
                                case "8th": beat = Beat.Eighth; break;
                                case "triplet 8th": beat = Beat.EighthTriplet; break;
                                case "16th": beat = Beat.Sixteenth; break;
                                case "triplet 16th": beat = Beat.SixteenthTriplet; break;
                                case "32nd": beat = Beat.ThirtySecond; break;
                                case "64th": beat = Beat.SixtyFourth; break;
                                default: break;
                            }
                        }
                        else if (line.StartsWith("duration: "))
                        {
                            line = line.Remove(0, 10);
                            duration = Convert.ToByte(line, 10);
                            // check if duration a standard beat length
                            for (int i = 0; i < 13; i++)
                                if (duration == Model.ROM[0x042304 + i])
                                    beat = (Beat)i;
                        }
                        if (beat == Beat.NULL)
                        {
                            opcode = 0xB6 + (int)pitch;
                            parameter1 = duration;
                        }
                        else
                            opcode = (int)beat * 14 + (int)pitch;
                    }
                    #endregion
                    #region Commands
                    else
                    {
                        // look through interpreter strings for legitimate opcodes
                        for (int i = 0xC4; i < strings.Length; i++)
                        {
                            if (strings[i] != "" && line.StartsWith(strings[i]))
                            {
                                opcode = i;
                                line = line.Remove(0, strings[i].Length);
                                break;
                            }
                        }
                        // if line has no matches, check if raw opcode in {}
                        if (opcode == -1 && line.Length >= 4 && line.StartsWith("{") && line.EndsWith("}"))
                        {
                            // remove {} and -
                            line = line.Replace("{", "");
                            line = line.Replace("}", "");
                            string[] parameters = line.Split(new char[] { '-' });
                            line = line.Replace("-", "");
                            length = line.Length / 2;
                            opcode = Convert.ToByte(parameters[0], 16);
                            if (parameters.Length > 1)
                                parameter1 = Convert.ToByte(parameters[1], 16);
                            if (parameters.Length > 2)
                                parameter2 = Convert.ToByte(parameters[2], 16);
                            if (parameters.Length > 3)
                                parameter3 = Convert.ToByte(parameters[3], 16);
                        }
                        // if no matches and not raw opcode, not a legitimate command
                        else if (opcode == -1)
                            continue;
                        // otherwise, it is a legitimate command, so continue reading line
                        #region Opcode Interpreter
                        else
                        {
                            // get array of strings for each individual parameter
                            string[] parameters = line.Split(new char[] { ',' });
                            switch (opcode)
                            {
                                case 0xCD: // Play sound
                                case 0xCE: // Play sound
                                case 0xDE: // Set instrument
                                    line = line.Replace("{", "");
                                    line = line.Replace("}", "");
                                    line = line.Remove(3); // rest not needed, just need sound effect index
                                    parameter1 = Bits.GetInt32(ref line);
                                    break;
                                case 0xC4: // Octave up
                                    octave++; break;
                                case 0xC5: // Octave down
                                    octave--; break;
                                case 0xC6: // Octave = 
                                    parameter1 = Bits.GetInt32(ref line);
                                    octave = parameter1;
                                    octaveIsSet = true;
                                    break;
                                case 0xC8: // Noise on, channels = 
                                case 0xD1: // Beat duration = 
                                case 0xD4: // Start loop, count = 
                                case 0xD9: // ADSR attack = 
                                case 0xDA: // ADSR decay = 
                                case 0xDB: // ADSR sustain = 
                                case 0xDC: // ADSR release = 
                                case 0xDD: // Sample length = 
                                case 0xE0: // Sample fadeout = 
                                case 0xE2: // Volume = 
                                case 0xE7: // Speaker balance = 
                                case 0xF6: // Portamento on, length = 
                                    parameter1 = Bits.GetInt32(ref line);
                                    break;
                                case 0xDF: // Noise transpose, pitch = 
                                    parameter1 = Bits.GetInt32(ref parameters[0]);
                                    parameter2 = Bits.GetInt32(ref parameters[1]);
                                    if (parameter1 < 0)
                                        parameter1 += 0x20;
                                    parameter1 |= parameter2 << 5;
                                    break;
                                case 0xE4: // Volume slide, duration = 
                                case 0xE5: // Portamento, duration = 
                                    parameter1 = Bits.GetInt32(ref parameters[0]);
                                    parameter2 = (byte)((sbyte)Bits.GetInt32(ref parameters[1]));
                                    break;
                                case 0xE8: // Speaker balance shift, duration = 
                                case 0xE9: // Speaker balance pansweep, duration = 
                                    parameter1 = Convert.ToByte(parameters[0], 10);
                                    parameter2 = (byte)Bits.GetInt32(ref parameters[1]);
                                    break;
                                case 0xF0: // Tremolo, rate = 
                                case 0xF1: // Vibrato, rate = 
                                case 0xF4: // Growling, roughness = 
                                    parameter1 = Bits.GetInt32(ref parameters[0]);
                                    parameter2 = Bits.GetInt32(ref parameters[1]);
                                    if (opcode == 0xF1)
                                        parameter3 = Bits.GetInt32(ref parameters[2]);
                                    break;
                                case 0xCF: // Transpose 1/16 pitch = 
                                case 0xE3: // Volume shift, amount = 
                                case 0xEC: // Transpose 1/4 pitch 1 = 
                                case 0xED: // Transpose 1/4 pitch 2 = 
                                case 0xF2: // Beat duration, change = 
                                    parameter1 = (byte)((sbyte)Bits.GetInt32(ref line)); // signed byte
                                    break;
                                case 0xFC: // Reverb, delay = 
                                    parameter1 = Bits.GetInt32(ref parameters[0]);
                                    parameter2 = Bits.GetInt32(ref parameters[1]);
                                    parameter3 = Bits.GetInt32(ref parameters[2]);
                                    break;
                                default: break;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    // create the command data
                    length = SPCScriptEnums.CommandLengths[opcode];
                    command = new byte[length];
                    if (length > 0)
                        command[0] = (byte)opcode;
                    if (length > 1)
                        command[1] = (byte)parameter1;
                    if (length > 2)
                        command[2] = (byte)parameter2;
                    if (length > 3)
                        command[3] = (byte)parameter3;
                    if (Type == 0)
                        commands.Add(new SPCCommand(command, (SPCTrack)this.spc, mouseOverChannel));
                    else
                        commands.Add(new SPCCommand(command, (SPCSound)this.spc, mouseOverChannel));
                }
                catch (Exception ex)
                {
                    error += "Error reading line #" + lineNumber + " (\"" + temp + "\"). " + ex.Message + "\n";
                }
                lineNumber++;
            }
            if (error == "")
                MessageBox.Show("Import successful.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                new NewMessageBox("LAZY SHELL", "There were some errors importing the channel script.", error, "", MessageIcon.Warning).ShowDialog();
            tr.Close();
            sourceCommands = commands;
            return true;
        }
        private bool ImportMMLScript(List<SPCCommand>[] sourceChannels, bool[] activeChannels)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Music Macro Language script";
            openFileDialog.Filter = "MML files (*.mml;*.txt)|*.mml;*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return false;
            //
            PickNativeSPC pickNativeSPC = new PickNativeSPC();
            if (pickNativeSPC.ShowDialog() == DialogResult.Cancel)
                return false;
            NativeSPC nativeFormat = (NativeSPC)pickNativeSPC.Tag;
            //
            string path = openFileDialog.FileName;
            BinaryFormatter b = new BinaryFormatter();
            // clean up unnecessary junk and create script text
            string script = new StreamReader(path).ReadToEnd();
            script = Regex.Replace(script, "[ \t]", ""); // remove all spaces, tabs
            script = Regex.Replace(script, "\r", "\n"); // replace these with newline to simplify
            script = Regex.Replace(script, "\n+", "\n"); // remove empty newlines
            script = Regex.Replace(script, ";[^\n]+\n", ""); // remove all comments
            script = Regex.Replace(script, ";\n", ""); // remove all empty comment lines
            // initialize variables
            List<SPCCommand>[] channels = new List<SPCCommand>[sourceChannels.Length];
            List<int> foundPercussives = new List<int>();
            List<int> foundInstruments = new List<int>();
            List<int> newInstruments = new List<int>();
            int channel = -1;
            int globalVol = -1;
            string temp = "";
            string error = "";
            int lineNumber = 1;
            bool triplet = false;
            bool writingLabel = false;
            bool writingLoop = false;
            int label = -1;
            int currentNestedLoop = -1; // the current nested loop
            int setTicks = -1;
            int lastTicks = -1;
            int octave = 4;
            // percussive variables
            Pitch pitchIndex = 0;
            bool percussiveMode = false;
            Pitch percussivePitch = Pitch.A;
            Pitch lastPitch = Pitch.A;
            Stack<int> loopCount = new Stack<int>();
            List<SPCCommand> lastLoop = null;
            List<SPCCommand>[] labels = new List<SPCCommand>[1000];
            // first create percussive list
            List<Percussives> percussives = new List<Percussives>();
            if (nativeFormat == NativeSPC.SMWLevel || nativeFormat == NativeSPC.SMWOverworld)
            {
                percussives.Add(new Percussives(0, 18, 64, 100, 127));
                percussives.Add(new Percussives(1, 109, 64, 100, 127));
                percussives.Add(new Percussives(2, 51, 64, 100, 127));
                percussives.Add(new Percussives(3, 52, 64, 100, 127));
                percussives.Add(new Percussives(4, 53, 64, 100, 127));
                percussives.Add(new Percussives(5, 67, 64, 100, 127));
                percussives.Add(new Percussives(6, 67, 64, 100, 127));
                percussives.Add(new Percussives(7, 51, 64, 100, 127));
                percussives.Add(new Percussives(8, 51, 64, 100, 127));
                //
                script = script.Replace("$ED$80$6D$2B\n" + "$ED$80$7D$00\n" + "$F0", "");
                script = script.Replace("$ED$80$6D$68\n" + "$ED$80$7D$00\n" + "$F0", "");
            }
            //
            while (script != "")
            {
                try
                {
                    int opcode = -1;
                    int parameter1 = 0;
                    int parameter2 = 0;
                    int parameter3 = 0;
                    int length = 0;
                    byte[] command;
                    //
                    temp = script;
                    // read to end of line
                    // at beginning of loop, b/c must reset after each command
                    #region Notes
                    if (script.StartsWith("#") || script.StartsWith("{Ch"))
                    {
                        if (script.StartsWith("#"))
                        {
                            script = script.Remove(0, 1);
                            channel = Bits.GetInt32(ref script);
                        }
                        else
                        {
                            script = script.Remove(0, 3);
                            channel = Bits.GetInt32(ref script) - 1;
                        }
                        if (channel < 0 || channel > sourceChannels.Length - 1)
                            break;
                        if (channels[channel] == null)
                            channels[channel] = new List<SPCCommand>();
                        percussiveMode = false;
                        continue;
                    }
                    if (Regex.IsMatch(script[0].ToString(), "[a-g]") || script.StartsWith("r") || script.StartsWith("^"))
                    {
                        Beat beat = Beat.NULL;
                        int ticks = -1;
                        bool Cb = false;
                        Pitch pitch = Pitch.A;
                        if (script.StartsWith("f8cd8}"))
                            script = script.Remove(0, 0);
                        if (Regex.IsMatch(script[0].ToString(), "[a-g]"))
                        {
                            Cb = script.StartsWith("c-");
                            if (script.StartsWith("a-")) { pitch = Pitch.Gs; script = script.Remove(0, 2); }
                            else if (script.StartsWith("a+") || script.StartsWith("b-")) { pitch = Pitch.As; script = script.Remove(0, 2); }
                            else if (script.StartsWith("a")) { pitch = Pitch.A; script = script.Remove(0, 1); }
                            else if (script.StartsWith("b") || script.StartsWith("c-")) { pitch = Pitch.B; script = script.Remove(0, 1); }
                            else if (script.StartsWith("c+") || script.StartsWith("d-")) { pitch = Pitch.Cs; script = script.Remove(0, 2); }
                            else if (script.StartsWith("c")) { pitch = Pitch.C; script = script.Remove(0, 1); }
                            else if (script.StartsWith("d+") || script.StartsWith("e-")) { pitch = Pitch.Ds; script = script.Remove(0, 2); }
                            else if (script.StartsWith("d")) { pitch = Pitch.D; script = script.Remove(0, 1); }
                            else if (script.StartsWith("e") || script.StartsWith("f-")) { pitch = Pitch.E; script = script.Remove(0, 1); }
                            else if (script.StartsWith("f+") || script.StartsWith("g-")) { pitch = Pitch.Fs; script = script.Remove(0, 2); }
                            else if (script.StartsWith("f")) { pitch = Pitch.F; script = script.Remove(0, 1); }
                            else if (script.StartsWith("g+")) { pitch = Pitch.Gs; script = script.Remove(0, 2); }
                            else if (script.StartsWith("g")) { pitch = Pitch.G; script = script.Remove(0, 1); }
                            if (percussiveMode)
                                pitch = percussivePitch;
                        }
                        else if (script.StartsWith("r")) // Stop
                        {
                            pitch = Pitch.Rest;
                            script = script.Remove(0, 1);
                        }
                        else if (script.StartsWith("^")) // Tie
                        {
                            pitch = Pitch.Tie;
                            script = script.Remove(0, 1);
                        }
                        // only if not followed by number
                        if (script.StartsWith("="))
                        {
                            script = script.Remove(0, 1);
                            ticks = Bits.GetInt32(ref script);
                            if (ticks == 144) beat = Beat.HalfDotted;
                            if (ticks == 72) beat = Beat.QuarterDotted;
                            if (ticks == 36) beat = Beat.EighthDotted;
                        }
                        else if (Regex.IsMatch(script[0].ToString(), "[0-9]"))
                            ticks = 192 / Bits.GetInt32(ref script);
                        else if (!Regex.IsMatch(script[0].ToString(), "[0-9]") && setTicks != -1)
                            ticks = setTicks;
                        if (triplet && ticks == 3) ticks = 2;
                        else if (triplet && ticks == 6) ticks = 4;
                        else if (triplet && ticks == 12) beat = Beat.SixteenthTriplet;
                        else if (triplet && ticks == 24) beat = Beat.EighthTriplet;
                        else if (triplet && ticks == 48) beat = Beat.QuarterTriplet;
                        else if (triplet && ticks == 96) ticks = 64;
                        else if (ticks == 3) beat = Beat.SixtyFourth;
                        else if (ticks == 6) beat = Beat.ThirtySecond;
                        else if (ticks == 8) beat = Beat.SixteenthTriplet;
                        else if (ticks == 12) beat = Beat.Sixteenth;
                        else if (ticks == 16) beat = Beat.EighthTriplet;
                        else if (ticks == 24) beat = Beat.Eighth;
                        else if (ticks == 32) beat = Beat.QuarterTriplet;
                        else if (ticks == 36) beat = Beat.EighthDotted;
                        else if (ticks == 48) beat = Beat.Quarter;
                        else if (ticks == 72) beat = Beat.QuarterDotted;
                        else if (ticks == 96) beat = Beat.Half;
                        else if (ticks == 144) beat = Beat.HalfDotted;
                        else if (ticks == 192) beat = Beat.Whole;
                        if (beat != Beat.NULL)
                            opcode = (int)beat * 14 + (int)pitch;
                        else if (ticks != -1)
                        {
                            opcode = 0xB6 + (int)pitch;
                            parameter1 = ticks;
                        }
                        else if (setTicks != -1)
                        {
                            opcode = 0xB6 + (int)pitch;
                            parameter1 = setTicks;
                        }
                        else
                        {
                            opcode = 0xB6 + (int)pitch;
                            parameter1 = 0;
                        }
                        if (Cb)
                            channels[channel].Add(new SPCCommand(new byte[] { 0xC5 }, spc, channel));
                        // to use with . command
                        lastPitch = pitch;
                        lastTicks = ticks;
                    }
                    #endregion
                    #region MML Commands
                    // stop reading line if comment
                    else if (script.StartsWith(";")) { script = script.Remove(0, 1); continue; }
                    else if (script.StartsWith("{")) { triplet = true; script = script.Remove(0, 1); continue; }
                    else if (script.StartsWith("}")) { triplet = false; script = script.Remove(0, 1); continue; }
                    else if (script.StartsWith("/")) { script = script.Remove(0, 1); opcode = 0xD7; }
                    else if (script.StartsWith("<")) { script = script.Remove(0, 1); opcode = 0xC5; octave--; }
                    else if (script.StartsWith(">")) { script = script.Remove(0, 1); opcode = 0xC4; octave++; }
                    else if (script.StartsWith("?")) { script = script.Remove(0, 1); opcode = 0xD0; }
                    else if (script.StartsWith("w"))
                    {
                        script = script.Remove(0, 1);
                        parameter1 = Bits.GetInt32(ref script);
                        if (globalVol == -1)
                            globalVol = parameter1 / 2;
                        continue; // no support for global volume here
                    }
                    else if (script.StartsWith("v"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xE2; parameter1 = Bits.GetInt32(ref script) / 2;
                    }
                    else if (script.StartsWith("o"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xC6; parameter1 = octave = Bits.GetInt32(ref script);
                    }
                    else if (script.StartsWith("t"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xD1;
                        parameter1 = Bits.GetInt32(ref script);
                        parameter1 = (int)(10000.0 / ((double)parameter1 * 1.2 * 2));
                    }
                    else if (script.StartsWith("y"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xE7; parameter1 = (Bits.GetInt32(ref script) - 5) * 25;
                    }
                    else if (script.StartsWith("&"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xE5; parameter1 = 10; parameter2 = 10;
                    }
                    else if (script.StartsWith("p"))
                    {
                        script = script.Remove(0, 1);
                        opcode = 0xF1;
                        parameter2 = Bits.GetInt32(ref script);
                        script = script.Remove(0, 1); // remove ","
                        parameter1 = Bits.GetInt32(ref script);
                    }
                    else if (script.StartsWith("@") || script.StartsWith("$DA"))
                    {
                        opcode = 0xDE;
                        if (script.StartsWith("@"))
                        {
                            script = script.Remove(0, 1);
                            parameter1 = Bits.GetInt32(ref script);
                        }
                        else
                        {
                            script = script.Remove(0, 3);
                            parameter1 = Bits.GetByte(ref script);
                        }
                        if (!foundInstruments.Contains(parameter1))
                        {
                            foundInstruments.Add(parameter1);
                            newInstruments.Add(parameter1);
                        }
                        // if instrument is a percussive
                        if (nativeFormat != NativeSPC.Custom)
                        {
                            switch (nativeFormat)
                            {
                                case NativeSPC.SMRPG:
                                    if (!Lists.SMRPGPercussives[parameter1]) break;
                                    if (!foundPercussives.Contains(parameter1))
                                    {
                                        foundPercussives.Add(parameter1);
                                        Percussives pr = new Percussives((byte)pitchIndex++, (byte)parameter1, 64, 100, 127);
                                        percussivePitch = pr.PitchIndex;
                                        percussives.Add(pr);
                                    }
                                    else
                                    {
                                        Percussives pr = percussives.Find(delegate(Percussives p) { return p.Sample == parameter1; });
                                        percussivePitch = pr.PitchIndex;
                                    }
                                    goto default;
                                case NativeSPC.SMWOverworld:
                                case NativeSPC.SMWLevel:
                                    if (Lists.SMWPercussives[parameter1] == -1) break;
                                    percussivePitch = (Pitch)Lists.SMWPercussives[parameter1];
                                    goto default;
                                default:
                                    // if not in percussive mode, add command to switch it on
                                    if (!percussiveMode) channels[channel].Add(new SPCCommand(new byte[] { 0xEE }, spc, channel));
                                    percussiveMode = true;
                                    continue;
                            }
                        }
                        // if in percussive mode, add command to switch it off
                        if (percussiveMode) channels[channel].Add(new SPCCommand(new byte[] { 0xEF }, spc, channel));
                        percussiveMode = false;
                    }
                    else if (script.StartsWith("l"))
                    {
                        script = script.Remove(0, 1);
                        setTicks = 192 / Bits.GetInt32(ref script);
                        continue;
                    }
                    else if (script.StartsWith("*"))
                    {
                        script = script.Remove(0, 1);
                        int count = 1;
                        if (Regex.IsMatch(script[0].ToString(), "[0-9]"))
                            count = Bits.GetInt32(ref script);
                        if (!writingLoop && lastLoop != null && channel != -1)
                        {
                            // if starts with a looping command, remove it
                            if (lastLoop[0].Opcode == 0xD4)
                                lastLoop.RemoveAt(0);
                            while (count-- > 0)
                                foreach (SPCCommand ssc in lastLoop)
                                    channels[channel].Add(ssc);
                        }
                        continue; // labels won't be inserted as commands
                    }
                    else if (script.StartsWith("["))
                    {
                        script = script.Remove(0, 1);
                        if (!writingLoop)
                        {
                            writingLoop = true;
                            lastLoop = new List<SPCCommand>();
                        }
                        // go to end of loop to get count
                        // first get number of nested loops
                        int i = 0;
                        int nestedLoopCount = 0;
                        while (script[i] != ']')
                            if (script[i++] == '[')
                                nestedLoopCount++;
                        // second get loop count
                        i = 0;
                        while (nestedLoopCount >= 0)
                            if (script[i++] == ']')
                                nestedLoopCount--;
                        // only if there's an actual loop, otherwise just a non-looping set
                        currentNestedLoop++;
                        if (i < script.Length && Regex.IsMatch(script[i].ToString(), "[0-9]"))
                        {
                            opcode = 0xD4;
                            parameter1 = Bits.GetInt32(ref script, i);
                        }
                        // otherwise skip, no looping to be done, no commands added
                        else
                            continue;
                    }
                    else if (script.StartsWith("]"))
                    {
                        script = script.Remove(0, 1);
                        currentNestedLoop--;
                        if (currentNestedLoop == -1)
                        {
                            writingLabel = false;
                            writingLoop = false;
                        }
                        // only if actually looping
                        if (script != "" && Regex.IsMatch(script[0].ToString(), "[0-9]"))
                        {
                            opcode = 0xD5;
                            script = script.Remove(0, 1); // remove the count
                        }
                        // otherwise if at end of OUTER-MOST non-nested loop
                        else if (currentNestedLoop == -1)
                            continue;
                    }
                    else if (script.StartsWith("("))
                    {
                        int labelLoop = 1;
                        script = script.Remove(0, 1);
                        label = Bits.GetInt32(ref script);
                        script = script.Remove(0, 1); // remove ")"
                        // if next command starts a loop, we're writing a new label
                        if (script.StartsWith("["))
                            writingLabel = true;
                        // otherwise if number follows set loop count
                        else if (Regex.IsMatch(script[0].ToString(), "[0-9]"))
                            labelLoop = Bits.GetInt32(ref script);
                        // if make new label from the following loop []
                        if (writingLabel)
                            labels[label] = new List<SPCCommand>();
                        // else if reading an already created label, and label exists, and channel set
                        else if (!writingLabel && labels[label] != null && channel != -1)
                        {
                            List<SPCCommand> thisLabel = labels[label];
                            // if label starts with a looping command, remove it
                            if (thisLabel[0].Opcode == 0xD4)
                                thisLabel.RemoveAt(0);
                            while (labelLoop-- > 0)
                                foreach (SPCCommand ssc in thisLabel)
                                    channels[channel].Add(ssc);
                        }
                        continue; // labels won't be inserted as commands
                    }
                    else if (script.StartsWith("."))
                    {
                        script = script.Remove(0, 1);
                        if (lastTicks == 192) { opcode = 2 * 14 + 13; }
                        else if (lastTicks == 144) { opcode = 3 * 14 + 13; }
                        else if (lastTicks == 96) { opcode = 4 * 14 + 13; }
                        else if (lastTicks == 72) { opcode = 5 * 14 + 13; }
                        else if (lastTicks == 64) { opcode = 6 * 14 + 13; }
                        else if (lastTicks == 48) { opcode = 7 * 14 + 13; }
                        else if (lastTicks == 32) { opcode = 8 * 14 + 13; }
                        else if (lastTicks == 24) { opcode = 9 * 14 + 13; }
                        else if (lastTicks == 16) { opcode = 10 * 14 + 13; }
                        else if (lastTicks == 12) { opcode = 11 * 14 + 13; }
                        else if (lastTicks == 6) { opcode = 12 * 14 + 13; }
                        else { opcode = 0xB6 + 13; parameter1 = lastTicks / 2; }
                        lastTicks /= 2;
                    }
                    // q and the hex byte following it
                    else if (script.StartsWith("q")) { script = script.Remove(0, 3); continue; }
                    #endregion
                    #region N-SPC Commands
                    else if (script.StartsWith("$DB")) // speaker balance
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xE7;
                        parameter1 = Bits.GetByte(ref script) * 13;
                    }
                    else if (script.StartsWith("$DC")) // pansweep
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xE8;
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script) * 13;
                    }
                    else if (script.StartsWith("$DD")) // pitch bend/portamento
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xE5;
                        Bits.GetByte(ref script); // skip first value, SMRPG doesn't support
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script) - 0x80 + 12;
                        parameter2 = parameter2 - (octave * 12 + (int)lastPitch);
                    }
                    else if (script.StartsWith("$DE")) // vibrato
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xF1;
                        parameter3 = Bits.GetByte(ref script); // delay
                        parameter2 = Bits.GetByte(ref script) / 4; // wavelength
                        parameter1 = Bits.GetByte(ref script) / 2; // amplitude
                    }
                    else if (script.StartsWith("$DF")) // vibrato off
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xF3;
                    }
                    else if (script.StartsWith("$E0")) // global volume, not supported
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$E1")) // global volume change, not supported
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script); // final balance, not supported
                        continue;
                    }
                    else if (script.StartsWith("$E2")) // tempo
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xD1;
                        parameter1 = Bits.GetByte(ref script);
                        parameter1 = (int)(10000.0 / ((double)parameter1 * 1.2 * 2));
                    }
                    else if (script.StartsWith("$E3")) // tempo change
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xD1;
                        parameter1 = Bits.GetByte(ref script); // speed, not supported
                        parameter2 = Bits.GetByte(ref script);
                        parameter2 = (int)(10000.0 / ((double)parameter2 * 1.2 * 2));
                    }
                    else if (script.StartsWith("$E4")) // global tuning, not supported
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$E5")) // tremolo or sample switch
                    {
                        script = script.Remove(0, 3);
                        int param1 = Bits.GetByte(ref script);
                        if (param1 < 0x80)
                        {
                            opcode = 0xF0;
                            parameter3 = param1; // delay, not supported
                            parameter2 = Bits.GetByte(ref script); // wavelength
                            parameter1 = Bits.GetByte(ref script); // amplitude
                        }
                        else
                        {
                            opcode = 0xDE;
                            parameter1 = param1 & 0x7F; // sample
                            parameter2 = Bits.GetByte(ref script);
                        }
                    }
                    else if (script.StartsWith("$E6")) // tremolo off
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        if (parameter1 < 0xDA) // loop start
                            opcode = 0xD4;
                        else
                            opcode = 0xF3;
                    }
                    else if (script.StartsWith("$E7")) // channel volume
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xE3;
                        parameter1 = Bits.GetByte(ref script);
                    }
                    else if (script.StartsWith("$E8")) // channel volume shift
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xE4;
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                    }
                    else if (script.StartsWith("$E9")) // embedded loop, not supported
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$EA")) // vibrato fade, not supported
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$EB")) // pitch bend to
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$EC")) // pitch bend from
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$ED")) // ADSR
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        if (parameter1 == 0x80)
                        {
                            parameter2 = Bits.GetByte(ref script);
                            parameter3 = Bits.GetByte(ref script);
                            continue; // not supported
                        }
                        else if (parameter1 == 0x81)
                        {
                            opcode = 0xED;
                            parameter2 = (sbyte)Bits.GetByte(ref script) * 4;
                        }
                        else if (parameter1 == 0x82)
                        {
                            int param1 = Bits.GetShort(ref script);
                            int param2 = Bits.GetShort(ref script);
                            while (param2-- >= 0)
                                Bits.GetByte(ref script);
                            continue; // not supported
                        }
                        else if (parameter1 == 0x83)
                        {
                            int param1 = Bits.GetShort(ref script);
                            int param2 = Bits.GetShort(ref script);
                            while (param2-- > 0)
                                Bits.GetByte(ref script);
                            continue; // not supported
                        }
                        else
                        {
                            parameter2 = Bits.GetByte(ref script);
                            //channels[channel].Add(new SPCScriptCommand(new byte[] { 0xD9, (byte)(parameter2 & 0x0F) }, spc, channel));
                            //channels[channel].Add(new SPCScriptCommand(new byte[] { 0xDA, (byte)(parameter2 >> 5) }, spc, channel));
                            //channels[channel].Add(new SPCScriptCommand(new byte[] { 0xDB, (byte)(parameter2 >> 5) }, spc, channel));
                            //channels[channel].Add(new SPCScriptCommand(new byte[] { 0xDC, (byte)(parameter2 & 0x1F) }, spc, channel));
                            continue;
                        }
                    }
                    else if (script.StartsWith("$EE")) // channel tuning
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$EF")) // echo
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$F0")) // reverb off
                    {
                        script = script.Remove(0, 3);
                        opcode = 0xFB;
                    }
                    else if (script.StartsWith("$F1")) // echo
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    else if (script.StartsWith("$F2")) // secondary echo
                    {
                        script = script.Remove(0, 3);
                        parameter1 = Bits.GetByte(ref script);
                        parameter2 = Bits.GetByte(ref script);
                        parameter3 = Bits.GetByte(ref script);
                        continue;
                    }
                    #endregion
                    // if non-MML command, ignore and skip
                    else if (script.StartsWith("$")) { script = script.Remove(0, 3); continue; }
                    // no legitimate commands, so skip and continue
                    else { script = script.Remove(0, 1); continue; }
                    // create the command data
                    if (channel != -1)
                    {
                        length = SPCScriptEnums.CommandLengths[opcode];
                        command = new byte[length];
                        if (length > 0) command[0] = (byte)opcode;
                        if (length > 1) command[1] = (byte)parameter1;
                        if (length > 2) command[2] = (byte)parameter2;
                        if (length > 3) command[3] = (byte)parameter3;
                        SPCCommand ssc;
                        if (Type == 0)
                            ssc = new SPCCommand(command, (SPCTrack)this.spc, channel);
                        else
                            ssc = new SPCCommand(command, (SPCSound)this.spc, channel);
                        channels[channel].Add(ssc);
                        // if writing a label, add the command to the current label too
                        if (writingLabel) labels[label].Add(ssc.Copy());
                        if (writingLoop) lastLoop.Add(ssc.Copy());
                    }
                }
                catch (Exception ex)
                {
                    error += "Error reading line #" + lineNumber + " (\"" + script + "\"). " + ex.Message + "\n";
                }
                lineNumber++;
            }
            if (error == "")
                MessageBox.Show("Import successful.", "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                new NewMessageBox("LAZY SHELL", "There were some errors importing the channel script.", error, "", MessageIcon.Warning).ShowDialog();
            //
            #region Finalize
            this.Updating = true;
            for (int i = 0; i < sourceChannels.Length; i++)
            {
                sourceChannels[i] = channels[i];
                activeChannels[i] = channels[i] != null;
                if (sourceChannels[i] == null)
                    sourceChannels[i] = new List<SPCCommand>();
                this.activeChannels[i].Checked = activeChannels[i];
                // terminate channel with 0xD0 if not already terminated
                if (activeChannels[i] && sourceChannels.Length > 0)
                    switch (sourceChannels[i][sourceChannels[i].Count - 1].Opcode)
                    {
                        case 0xCD:
                        case 0xCE:
                        case 0xD0:
                            break;
                        default:
                            sourceChannels[i].Add(new SPCCommand(new byte[] { 0xD0 }, spc, i));
                            break;
                    }
            }
            // replace samples with native ones
            foundInstruments.Sort();
            newInstruments.Sort();
            switch (nativeFormat)
            {
                case NativeSPC.Custom:
                    PickInstruments pickInstruments = new PickInstruments(foundInstruments, newInstruments);
                    pickInstruments.ShowDialog();
                    break;
                case NativeSPC.SMWOverworld:
                case NativeSPC.SMWLevel:
                    for (int i = 0; i < newInstruments.Count; i++)
                        newInstruments[i] = Lists.SMWSamples[foundInstruments[i]];
                    break;
            }
            for (int a = 0; a < sourceChannels.Length; a++)
            {
                if (sourceChannels[a] == null) continue;
                foreach (SPCCommand ssc in sourceChannels[a])
                {
                    if (ssc.Opcode == 0xDE)
                        for (int i = 0; i < foundInstruments.Count; i++)
                            if (ssc.Param1 == foundInstruments[i])
                            {
                                ssc.Param1 = (byte)newInstruments[i];
                                break;
                            }
                }
            }
            if (globalVol == -1)
                globalVol = 100;
            for (int i = 0; i < 20; i++)
            {
                if (i < newInstruments.Count)
                {
                    if (spc.Samples[i] == null)
                        spc.Samples[i] = new SampleIndex((byte)newInstruments[i], 100);
                    else
                        spc.Samples[i].Sample = newInstruments[i];
                    spc.Samples[i].Volume = globalVol;
                    spc.Samples[i].Active = true;
                    activeInstruments[i].Checked = true;
                    sampleIndexes[i].SelectedIndex = (byte)newInstruments[i];
                    sampleIndexes[i].Enabled = true;
                    volumes[i].Enabled = true;
                    volumes[i].Value = globalVol;
                }
                else
                {
                    if (spc.Samples[i] != null)
                        spc.Samples[i].Active = false;
                    activeInstruments[i].Checked = false;
                    sampleIndexes[i].Enabled = false;
                    volumes[i].Enabled = false;
                }
            }
            // set percussives
            spc.Percussives = percussives;
            //
            this.Updating = false;
            #endregion
            return true;
        }
        private void ExportMMLScript(int type)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - ";
            if (type == 0)
                saveFileDialog.FileName += "SPCMML." + Index.ToString("d2");
            else
                saveFileDialog.FileName += "StaffsMML";
            saveFileDialog.FileName += ".txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            PickNativeSPC pickNativeSPC = new PickNativeSPC();
            if (pickNativeSPC.ShowDialog() == DialogResult.Cancel)
                return;
            NativeSPC nativeFormat = (NativeSPC)pickNativeSPC.Tag;
            //
            StreamWriter writer = File.CreateText(saveFileDialog.FileName);
            string script;
            if (type == 0)
                script = ";SMRPG track {" + Index + "}  " + Lists.MusicNames[Index] + "\n\n";
            else
                script = ";LAZY SHELL SCORE\n\n";
            if (nativeFormat == NativeSPC.SMWLevel)
                script += "$ED $80 $6D $2B\n" + "$ED $80 $7D $00\n" + "$F0\n\n";
            if (nativeFormat == NativeSPC.SMWOverworld)
                script += "$ED $80 $6D $68\n" + "$ED $80 $7D $00\n" + "$F0\n\n";
            //
            List<SPCCommand>[] channels;
            if (type == 0)
            {
                if (MessageBox.Show("Would you like to include nested loops? Selecting \"No\" will save the file to a loopless, uncompressed format.",
                    "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    channels = ((SPCTrack)spc).DecompToMML(nativeFormat);
                else
                    channels = spc.Channels;
            }
            else
                channels = StaffsToChannels();
            List<SPCCommand>[] labels = new List<SPCCommand>[256];
            for (int c = 0; c < channels.Length; c++)
            {
                if (channels == null) break;
                if (channels[c] == null || (type < 3 && !spc.ActiveChannels[c]))
                    continue;
                int sample = 0;
                bool percussionMode = false;
                script += "\n\n#" + c + "\n\n";
                if (c == 0) script += "w180 ";
                for (int i = 0; i < channels[c].Count; i++)
                {
                    //if (i != 0 && i % 20 == 0) 
                    //    script += "\n";
                    SPCCommand ssc = channels[c][i];
                    switch (ssc.Opcode)
                    {
                        case 0xC4: script += ">"; break;
                        case 0xC5: script += "<"; break;
                        case 0xC6: script += " o" + (ssc.Param1 - 0) + " "; break;
                        case 0xD0: break;// if (c == channels.Length - 1) script += "\n?\n"; break;
                        case 0xD1:
                            int tempo = (int)(10000.0 / (double)ssc.Param1 / 2.0 / 1.2);
                            script += " t" + tempo + " "; break;
                        case 0xD4:
                            script += "["; i++;
                            ExportMMLLoop(ref channels, ref script, c, ref i, ssc.Param1, ref percussionMode, ref sample, nativeFormat);
                            script += "\n";
                            break;
                        case 0xD5: break;
                        case 0xD6: break;
                        case 0xD7: script += "\n/\n"; break;
                        case 0xDE:
                            sample = ssc.Param1;
                            switch (nativeFormat)
                            {
                                case NativeSPC.SMWOverworld:
                                case NativeSPC.SMWLevel: script += " @" + Lists.SMRPGtoSMWSamples[ssc.Param1] + " "; break;
                                default: script += "\n$DA $" + ssc.Param1.ToString("X2") + " "; break;
                            }
                            break;
                        case 0xE2: script += " v" + (ssc.Param1 * 2) + " "; break;
                        //case 0xE5: script += "&"; break;
                        case 0xE7: script += " y" + (ssc.Param1 / 25 + 5) + " "; break;
                        case 0xEE: percussionMode = true; break;
                        case 0xEF: percussionMode = false; break;
                        case 0xF1: script += " p" + ssc.Param2 + "," + ssc.Param1 + " "; break;
                        default:
                            if (ssc.Opcode < 0xC4)
                            {
                                Note note = new Note(ssc, 5, percussionMode, sample);
                                // create instrument change for percussives
                                if (percussionMode)
                                {
                                    Percussives percussive = spc.Percussives.Find(
                                        delegate(Percussives p) { return p.PitchIndex == note.Pitch; });
                                    if (percussive != null && sample != percussive.Sample)
                                    {
                                        switch (nativeFormat)
                                        {
                                            case NativeSPC.SMWOverworld:
                                            case NativeSPC.SMWLevel: script += " @" + Lists.SMRPGtoSMWSamples[percussive.Sample] + " "; break;
                                            default: script += "\n$DA $" + percussive.Sample.ToString("X2") + " "; break;
                                        }
                                    }
                                }
                                switch (note.Pitch)
                                {
                                    case Pitch.A: script += "a"; break;
                                    case Pitch.As: script += "a+"; break;
                                    case Pitch.B: script += "b"; break;
                                    case Pitch.C: script += "c"; break;
                                    case Pitch.Cs: script += "c+"; break;
                                    case Pitch.D: script += "d"; break;
                                    case Pitch.Ds: script += "d+"; break;
                                    case Pitch.E: script += "e"; break;
                                    case Pitch.F: script += "f"; break;
                                    case Pitch.Fs: script += "f+"; break;
                                    case Pitch.G: script += "g"; break;
                                    case Pitch.Gs: script += "g+"; break;
                                    case Pitch.Rest: script += "r"; break;
                                    case Pitch.Tie: script += "^"; break;
                                    default: script += "r"; break;
                                }
                                if (ssc.Opcode < 0xB6)
                                    switch (note.Beat)
                                    {
                                        case Beat.Whole: script += "1"; break;
                                        case Beat.HalfDotted: script += "2."; break; // dotted half
                                        case Beat.Half: script += "2"; break;
                                        case Beat.QuarterDotted: script += "4."; break; // dotted quarter
                                        case Beat.Quarter: script += "4"; break;
                                        case Beat.EighthDotted: script += "8."; break; // dotted 8th
                                        case Beat.QuarterTriplet: script += "6"; break; // quarter triplet
                                        case Beat.Eighth: script += "8"; break;
                                        case Beat.EighthTriplet: script += "12"; break; // 8th triplet
                                        case Beat.Sixteenth: script += "16"; break;
                                        case Beat.SixteenthTriplet: script += "24"; break; // 16th triplet
                                        case Beat.ThirtySecond: script += "32"; break;
                                        case Beat.SixtyFourth: script += "64"; break;
                                        default: break;
                                    }
                                else
                                    script += "=" + ssc.Param1;
                            }
                            break;
                    }
                }
            }
            script = Regex.Replace(script, " {2,}", " ");
            script = Regex.Replace(script, "\\[ ", "[");
            script = Regex.Replace(script, " \\]", "]");
            script = Regex.Replace(script, "\\[\n", "[");
            script = Regex.Replace(script, "\n +", "\n");
            script = Regex.Replace(script, "\n{3,}", "\n\n");
            script = Regex.Replace(script, "\n", "\r\n");
            writer.Write(script);
            writer.Close();
        }
        private void ExportMMLLoop(ref List<SPCCommand>[] channels, ref string script, int c, ref int i,
            int loopCount, ref bool percussionMode, ref int sample, NativeSPC nativeFormat)
        {
            int loopStart = i;
            int firstSectionStart = -1;
            int firstSectionEnd = -1;
            bool writingBeginning = false;
            for (; i < channels[c].Count; i++)
            {
                //if (i != 0 && i % 20 == 0)
                //    script += "\n";
                SPCCommand ssc = channels[c][i];
                if (writingBeginning && i == firstSectionStart)
                {
                    i = firstSectionEnd;
                    return;
                }
                switch (ssc.Opcode)
                {
                    case 0xC4: script += ">"; break;
                    case 0xC5: script += "<"; break;
                    case 0xC6: script += " o" + (ssc.Param1 - 0) + " "; break;
                    case 0xD0: break;// if (c == channels.Length - 1) script += "\n?\n"; break;
                    case 0xD4:
                        script += "\n["; i++;
                        ExportMMLLoop(ref channels, ref script, c, ref i, ssc.Param1, ref percussionMode, ref sample, nativeFormat);
                        break;
                    case 0xD5:
                        script += "]" + loopCount;
                        if (firstSectionStart != -1)
                        {
                            firstSectionEnd = i;
                            writingBeginning = true;
                            i = loopStart - 1;
                        }
                        else
                            return;
                        break;
                    case 0xD6:
                        loopCount--;
                        firstSectionStart = i;
                        break;
                    case 0xD7: script += "\n/\n"; break;
                    case 0xDE:
                        sample = ssc.Param1;
                        switch (nativeFormat)
                        {
                            case NativeSPC.SMWOverworld:
                            case NativeSPC.SMWLevel: script += " @" + Lists.SMRPGtoSMWSamples[ssc.Param1] + " "; break;
                            default: script += "\n$DA $" + ssc.Param1.ToString("X2") + " "; break;
                        }
                        break;
                    case 0xE2: script += " v" + (ssc.Param1 * 2) + " "; break;
                    //case 0xE5: script += "&"; break;
                    case 0xE7: script += " y" + (ssc.Param1 / 25 + 5) + " "; break;
                    case 0xEE: percussionMode = true; break;
                    case 0xEF: percussionMode = false; break;
                    case 0xF1: script += " p" + ssc.Param2 + "," + ssc.Param1 + " "; break;
                    default:
                        if (ssc.Opcode < 0xC4)
                        {
                            Note note = new Note(ssc, 5, percussionMode, sample);
                            // create instrument change for percussives
                            if (percussionMode)
                            {
                                Percussives percussive = spc.Percussives.Find(
                                    delegate(Percussives p) { return p.PitchIndex == note.Pitch; });
                                if (percussive != null && sample != percussive.Sample)
                                {
                                    switch (nativeFormat)
                                    {
                                        case NativeSPC.SMWOverworld:
                                        case NativeSPC.SMWLevel: script += " @" + Lists.SMRPGtoSMWSamples[percussive.Sample] + " "; break;
                                        default: script += "\n$DA $" + percussive.Sample.ToString("X2") + " "; break;
                                    }
                                }
                            }
                            switch (note.Pitch)
                            {
                                case Pitch.A: script += "a"; break;
                                case Pitch.As: script += "a+"; break;
                                case Pitch.B: script += "b"; break;
                                case Pitch.C: script += "c"; break;
                                case Pitch.Cs: script += "c+"; break;
                                case Pitch.D: script += "d"; break;
                                case Pitch.Ds: script += "d+"; break;
                                case Pitch.E: script += "e"; break;
                                case Pitch.F: script += "f"; break;
                                case Pitch.Fs: script += "f+"; break;
                                case Pitch.G: script += "g"; break;
                                case Pitch.Gs: script += "g+"; break;
                                case Pitch.Rest: script += "r"; break;
                                case Pitch.Tie: script += "^"; break;
                                default: script += "r"; break;
                            }
                            if (ssc.Opcode < 0xB6)
                                switch (note.Beat)
                                {
                                    case Beat.Whole: script += "1"; break;
                                    case Beat.HalfDotted: script += "2^4"; break; // dotted half
                                    case Beat.Half: script += "2"; break;
                                    case Beat.QuarterDotted: script += "4^8"; break; // dotted quarter
                                    case Beat.Quarter: script += "4"; break;
                                    case Beat.EighthDotted: script += "8^16"; break; // dotted 8th
                                    case Beat.QuarterTriplet: script += "6"; break; // quarter triplet
                                    case Beat.Eighth: script += "8"; break;
                                    case Beat.EighthTriplet: script += "12"; break; // 8th triplet
                                    case Beat.Sixteenth: script += "16"; break;
                                    case Beat.SixteenthTriplet: script += "24"; break; // 16th triplet
                                    case Beat.ThirtySecond: script += "32"; break;
                                    case Beat.SixtyFourth: script += "64"; break;
                                    default: break;
                                }
                            else
                                script += "=" + ssc.Param1;
                        }
                        break;
                }
            }
        }
        //
        private int GetNoteIndex(int x, Key key)
        {
            if (mouseDownChannel == -1)
                return -1;
            int x_ = 64 + GetKeyWidth(key);
            int index = 0;
            foreach (Note note in spc.Notes[mouseDownChannel])
            {
                // skip if not a note, rest, or tie
                if (note.Command.Opcode < 0xC4 && (!note.Rest || showRests.Checked))
                    if (x_ + 8 >= x)//(x >= x_ && x <= x_ + 16) // 2013-04-11
                        break;
                x_ += (int)((double)noteSpacing / 100.0 * note.Ticks);
                index++;
            }
            return index;
        }
        private int GetNoteIndex(int x, Key key, bool writer)
        {
            if (mouseDownStaff == -1)
                return -1;
            int x_ = 64 + GetKeyWidth(key);
            int index = 0;
            Note note;
            foreach (object item in staffs[mouseDownStaff].Notes)
            {
                if (item.GetType() == typeof(Note))
                {
                    note = (Note)item;
                    if (x_ >= x)
                        break;
                    x_ += (int)((double)noteSpacing / 100.0 * note.Ticks);
                }
                index++;
            }
            return index;
        }
        public Accidental GetKeySignature(Key key, Pitch pitch)
        {
            switch (pitch)
            {
                case Pitch.A:
                    if (key >= Key.BMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.GsMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.EbMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.CMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.B:
                    if (key == Key.CsMajor || key == Key.AsMinor) return Accidental.Sharp; // Sharps, major, minor
                    if (key >= Key.FMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.DMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.C:
                    if (key >= Key.DMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.BMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.GbMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.EbMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.D:
                    if (key >= Key.EMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.CsMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.AbMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.FMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.E:
                    if (key >= Key.FsMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.DsMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.BbMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.DMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.F:
                    if (key >= Key.GMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.EMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key == Key.CbMajor || key == Key.AbMinor) return Accidental.Flat; // Flats, major, minor
                    break;
                case Pitch.G:
                    if (key >= Key.AMajor && key <= Key.CsMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.FsMinor && key <= Key.AsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.DbMajor && key <= Key.CbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.BbMinor && key <= Key.AbMinor) return Accidental.Flat; // Flats, major
                    break;
            }
            return Accidental.Natural; // Natural
        }
        /// <summary>
        /// Returns what accidentals need to be SHOWN based on a given key signature. Thus, only non-black keys are checked.
        /// </summary>
        /// <param name="key">The key signature.</param>
        /// <returns></returns>
        public Accidental GetAccidental(Key key, Pitch pitch)
        {
            switch (pitch)
            {
                case Pitch.C:
                    if (key >= Key.DMajor && key <= Key.CsMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.BMinor && key <= Key.AsMinor) return Accidental.Natural; // Natural, minor
                    if (key >= Key.GbMajor && key <= Key.CbMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.EbMinor && key <= Key.AbMinor) return Accidental.Natural; // Natural, minor
                    break;
                case Pitch.Cs:
                    if (key >= Key.CMajor && key <= Key.GMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.AMinor && key <= Key.EMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.FMajor && key <= Key.EbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.DMinor && key <= Key.CMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.D:
                    if (key >= Key.EMajor && key <= Key.CsMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.CsMinor && key <= Key.AsMinor) return Accidental.Natural; // Natural, minor
                    if (key >= Key.AbMajor && key <= Key.CbMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.FMinor && key <= Key.AbMinor) return Accidental.Natural; // Natural, minor
                    break;
                case Pitch.Ds:
                    if (key >= Key.CMajor && key <= Key.AMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.AMinor && key <= Key.FMinor) return Accidental.Sharp; // Sharps, minor
                    if (key == Key.FMajor || key == Key.DMinor) return Accidental.Flat; // Flats, major/minor
                    break;
                case Pitch.E:
                    if (key >= Key.FsMajor && key <= Key.CsMajor) return Accidental.Natural; // Sharps, major
                    if (key >= Key.DsMinor && key <= Key.AsMinor) return Accidental.Natural; // Sharps, minor
                    if (key >= Key.BbMajor && key <= Key.CbMajor) return Accidental.Natural; // Flats, major
                    if (key >= Key.DMinor && key <= Key.AbMinor) return Accidental.Natural; // Flats, minor
                    //if (key == Key.CbMajor || key == Key.AbMinor) return Accidental.Flat;
                    break;
                case Pitch.F:
                    if (key >= Key.GMajor && key <= Key.CsMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.EMinor && key <= Key.AsMinor) return Accidental.Natural; // Natural, minor
                    if (key == Key.CbMajor || key == Key.AbMinor) return Accidental.Natural; // Natural, major/minor
                    break;
                case Pitch.Fs:
                    if (key == Key.CMajor || key == Key.AMinor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.FMajor && key <= Key.AbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.DMinor && key <= Key.FMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.G:
                    if (key >= Key.AMajor && key <= Key.CsMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.FsMinor && key <= Key.AsMinor) return Accidental.Natural; // Natural, minor
                    if (key >= Key.DbMajor && key <= Key.CbMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.BbMinor && key <= Key.AbMinor) return Accidental.Natural; // Natural, major
                    break;
                case Pitch.Gs:
                    if (key >= Key.CMajor && key <= Key.DMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.AMinor && key <= Key.BMinor) return Accidental.Sharp; // Sharps, minor
                    if (key >= Key.FMajor && key <= Key.BbMajor) return Accidental.Flat; // Flats, major
                    if (key >= Key.DMinor && key <= Key.GMinor) return Accidental.Flat; // Flats, minor
                    break;
                case Pitch.A:
                    if (key >= Key.BMajor && key <= Key.CsMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.GsMinor && key <= Key.AsMinor) return Accidental.Natural; // Natural, minor
                    if (key >= Key.EbMajor && key <= Key.CbMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.CMinor && key <= Key.AbMinor) return Accidental.Natural; // Natural, minor
                    break;
                case Pitch.As:
                    if (key >= Key.CMajor && key <= Key.EMajor) return Accidental.Sharp; // Sharps, major
                    if (key >= Key.AMinor && key <= Key.CsMinor) return Accidental.Sharp; // Sharps, minor
                    if (key == Key.CMajor || key == Key.AMinor) return Accidental.Flat; // Flats, major/minor
                    break;
                case Pitch.B:
                    if (key == Key.CsMajor || key == Key.AsMinor) return Accidental.Natural; // Natural, major/minor
                    if (key >= Key.FMajor && key <= Key.CbMajor) return Accidental.Natural; // Natural, major
                    if (key >= Key.DMinor && key <= Key.AbMinor) return Accidental.Natural; // Natural, minor
                    break;
            }
            return Accidental.None; // Natural
        }
        /// <summary>
        /// Returns an octave change command if the previous note is a different octave than a defined one.
        /// </summary>
        /// <param name="octave">The defined octave.</param>
        /// <returns></returns>
        private SPCCommand OctaveChangeBefore(int octave, bool checkNext)
        {
            if (checkNext && mouseOverNote >= 0)
            {
                Note nextNote = null;
                if (mouseOverNote < spc.Notes[mouseOverChannel].Count)
                    nextNote = spc.Notes[mouseOverChannel][mouseOverNote];
                if (nextNote != null && octave == nextNote.Octave)
                    return null;
            }
            //
            SPCCommand sscA = null;
            Note prevNote = null;
            if (mouseOverNote > 0)
                prevNote = spc.Notes[mouseOverChannel][mouseOverNote - 1];
            // if different octave than note before, we should define octave change command before new note
            if (prevNote != null && octave != prevNote.Octave)
            {
                if (prevNote.Octave == octave - 1)
                    sscA = new SPCCommand(new byte[] { 0xC4 }, spc, mouseOverChannel); // octave up 1
                else if (prevNote.Octave == octave + 1)
                    sscA = new SPCCommand(new byte[] { 0xC5 }, spc, mouseOverChannel); // octave down 1
                else if (prevNote.Octave != octave)
                    sscA = new SPCCommand(new byte[] { 0xC6, (byte)octave }, spc, mouseOverChannel);
            }
            // if very first note, we should insert new note at very beginning, and define octave change command before new note
            else if (prevNote == null)
                sscA = new SPCCommand(new byte[] { 0xC6, (byte)octave }, spc, mouseOverChannel);
            return sscA;
        }
        /// <summary>
        /// Returns an octave change command if the next note is a different octave than a defined one.
        /// </summary>
        /// <param name="octave">The defined octave.</param>
        /// <param name="checkPrev">Checks if previous note is same octave as parameter.</param>
        /// <returns></returns>
        private SPCCommand OctaveChangeAfter(int octave, bool checkPrev)
        {
            if (mouseOverNote < 0)
                return null;
            //
            if (checkPrev)
            {
                Note prevNote = null;
                if (mouseOverNote > 0)
                    prevNote = spc.Notes[mouseOverChannel][mouseOverNote - 1];
                if (prevNote != null && octave == prevNote.Octave)
                    return null;
            }
            //
            SPCCommand sscC = null;
            Note nextNote = null;
            if (mouseOverNote + 1 < spc.Notes[mouseOverChannel].Count)
                nextNote = spc.Notes[mouseOverChannel][mouseOverNote];
            // make an octave change come before and after the command if necessary
            // if different octave than BOTH notes before AND after, we should insert new note immediately after PREVIOUS note, 
            // and define octave change command both before and after new note
            if (nextNote != null && octave != nextNote.Octave)
            {
                if (nextNote.Octave == octave + 1)
                    sscC = new SPCCommand(new byte[] { 0xC4 }, spc, mouseOverChannel); // octave up 1
                else if (nextNote.Octave == octave - 1)
                    sscC = new SPCCommand(new byte[] { 0xC5 }, spc, mouseOverChannel); // octave down 1
                else if (nextNote.Octave != octave)
                    sscC = new SPCCommand(new byte[] { 0xC6, (byte)nextNote.Octave }, spc, mouseOverChannel);
            }
            return sscC;
        }
        /// <summary>
        /// Returns the index where a new note should be inserted, based on a comparison 
        /// between a defined octave and the octaves of the previous and next notes.
        /// </summary>
        /// <param name="firstOctave">The defined octave of the first note in the collection.</param>
        /// <param name="lastOctave">The defined octave of the last note in the collection. Same as firstOctave if inserting only one note.</param>
        /// <returns></returns>
        private int OctaveChangeIndex(int firstOctave, int lastOctave)
        {
            if (mouseOverNote < 0)
                return Math.Max(0, spc.Channels[mouseOverChannel].Count - 1);
            int sscIndex = 0;
            // define notes before and after insertion point
            Note noteA = null;
            if (mouseOverNote > 0)
                noteA = spc.Notes[mouseOverChannel][mouseOverNote - 1];
            Note noteC = null;
            if (mouseOverNote < spc.Notes[mouseOverChannel].Count)
                noteC = spc.Notes[mouseOverChannel][mouseOverNote];
            if (noteA != null)
            {
                if (firstOctave != noteA.Octave && noteC != null && lastOctave == noteC.Octave)
                    sscIndex = noteC.Index;
                else
                    sscIndex = noteA.Index + 1;
            }
            else if (noteA == null && noteC != null)
                sscIndex = lastOctave == noteC.Octave ? noteC.Index : 0;
            return sscIndex;
        }
        private void PlayNote(Note note)
        {
            if (note.Rest || !note.IsNote)
                return;
            BRRSample sample = Model.AudioSamples[note.Sample];
            byte[] wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            Do.Play(SoundPlayer, wav, false);
        }
        private void SelectNote(bool writer)
        {
            if (mouseDownNote == -1)
                return;
            if (writer)
                scoreWriterPicture.Invalidate();
            else
            {
                if (mouseDownSSC == null)
                    return;
                int index = mouseDownSSC.Index;
                //PlayNote(spc.Notes[mouseDownChannel][mouseDownNote]);
                hScrollBarCT.Value = Math.Min(hScrollBarCT.Maximum, index * 24);
                scoreViewPicture.Invalidate();
                ControlDisassemble();
            }
        }
        public void Assemble(bool warning)
        {
            int offset = 0x045526;
            if (CalculateFreeSpace(0, false, warning))
                for (int i = 0; i < Model.SPCs.Length; i++)
                    Model.SPCs[i].Assemble(ref offset);
            offset = 0x042C26;
            if (CalculateFreeSpace(1, false, warning))
                for (int i = 0; i < Model.SPCEvent.Length; i++)
                    Model.SPCEvent[i].Assemble(ref offset);
            offset = 0x044226;
            if (CalculateFreeSpace(2, false, warning))
                for (int i = 0; i < Model.SPCBattle.Length; i++)
                    Model.SPCBattle[i].Assemble(ref offset);
        }
        #endregion
        #region Event Handlers
        private void soundType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            trackName.Items.Clear();
            if (Type == 0)
            {
                spcs = Model.SPCs;
                trackName.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                for (int i = 2; i < 8; i++)
                    activeChannels[i].Visible = true;
                labelWindow.SetElement("Songs");
            }
            else if (Type == 1)
            {
                spcs = Model.SPCEvent;
                trackName.Items.AddRange(Lists.Numerize(Lists.SoundNames));
                for (int i = 2; i < 8; i++)
                    activeChannels[i].Visible = false;
                labelWindow.SetElement("Sound FX (Event)");
            }
            else
            {
                spcs = Model.SPCBattle;
                trackName.Items.AddRange(Lists.Numerize(Lists.BattleSoundNames));
                for (int i = 2; i < 8; i++)
                    activeChannels[i].Visible = false;
                labelWindow.SetElement("Sound FX (Battle)");
            }
            if (settings.RememberLastIndex)
                settings.LastSoundType = soundType.SelectedIndex;
            trackNum.Maximum = spcs.Length - 1;
            Index = 0;
            trackName.SelectedIndex = 0;
            RefreshSPC();
        }
        private void trackNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            trackName.SelectedIndex = (int)trackNum.Value;
            if (settings.RememberLastIndex)
                settings.LastSPC = (int)trackNum.Value;
            RefreshSPC();
        }
        private void trackName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            trackNum.Value = trackName.SelectedIndex;
        }
        // Instruments
        private void sampleIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((ComboBox)sender).Tag;
            for (int i = 0; i < spc.Channels.Length; i++)
            {
                if (spc.Channels[i] == null)
                    continue;
                foreach (SPCCommand ssc in spc.Channels[i])
                {
                    if (ssc.Opcode == 0xDE && ssc.Param1 == spc.Samples[index].Sample)
                        ssc.Param1 = (byte)sampleIndexes[index].SelectedIndex;
                }
            }
            spc.Samples[index].Sample = (byte)sampleIndexes[index].SelectedIndex;
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void volume_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((NumericUpDown)sender).Tag;
            spc.Samples[index].Volume = (int)volumes[index].Value;
        }
        private void activeInstrument_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            int index = (int)((CheckBox)sender).Tag;
            if (activeInstruments[index].Checked)
            {
                if (spc.Samples[index] == null)
                    spc.Samples[index] = new SampleIndex(sampleIndexes[index].SelectedIndex, (int)volumes[index].Value);
                spc.Samples[index].Active = true;
                sampleIndexes[index].Enabled = true;
                volumes[index].Enabled = true;
            }
            else
            {
                if (spc.Samples[index] != null)
                    spc.Samples[index].Active = false;
                sampleIndexes[index].Enabled = false;
                volumes[index].Enabled = false;
            }
            CalculateFreeSpace(Type, true, false);
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        // Reverb
        private void delayTime_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.DelayTime = (byte)delayTime.Value;
        }
        private void decayFactor_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.DecayFactor = (byte)decayFactor.Value;
        }
        private void echo_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.Echo = (byte)((sbyte)echo.Value);
        }
        // Percussives
        private void newPercussive_Click(object sender, EventArgs e)
        {
            if (spc.Percussives.Count >= 12)
            {
                MessageBox.Show("No more than 12 percussives allowed.", "LAZY SHELL");
                return;
            }
            int index = percussives.SelectedIndex + 1;
            spc.Percussives.Insert(index, new Percussives(0, 0, 0, 0, 0));
            this.Updating = true;
            percussives.Items.Insert(index, Lists.Numerize(Lists.SampleNames, 0, 3));
            percussives.SelectedIndex = Math.Min(percussives.Items.Count - 1, index);
            RefreshPercussive();
            this.Updating = false;
        }
        private void deletePercussive_Click(object sender, EventArgs e)
        {
            if (spc.Percussives.Count == 0)
                return;
            if (percussives.SelectedIndex < 0)
            {
                MessageBox.Show("No percussives selected.", "LAZY SHELL");
                return;
            }
            int index = percussives.SelectedIndex;
            spc.Percussives.RemoveAt(index);
            this.Updating = true;
            percussives.Items.RemoveAt(index);
            percussives.SelectedIndex = Math.Min(percussives.Items.Count - 1, index);
            RefreshPercussive();
            this.Updating = false;
        }
        private void percussives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshPercussive();
        }
        private void percussivePitchIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.Percussives[percussives.SelectedIndex].PitchIndex = (Pitch)percussivePitchIndex.SelectedIndex;
        }
        private void percussiveName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            int index = percussiveName.SelectedIndex;
            percussives.Items[percussives.SelectedIndex] = Lists.Numerize(Lists.SampleNames[index], index, 3);
            spc.Percussives[percussives.SelectedIndex].Sample = (byte)index;
            this.Updating = false;
        }
        private void percussivePitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.Percussives[percussives.SelectedIndex].Pitch = (byte)percussivePitch.SelectedIndex;
        }
        private void percussiveVolume_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.Percussives[percussives.SelectedIndex].Volume = (byte)percussiveVolume.Value;
        }
        private void percussiveBalance_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            spc.Percussives[percussives.SelectedIndex].Balance = (byte)percussiveBalance.Value;
        }
        // Toolstrip
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the current SPC? You will lose all unsaved changes.",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (Type == 0)
                Model.SPCs[Index] = new SPCTrack(Index);
            else if (Type == 1)
                Model.SPCEvent[Index] = new SPCSound(Index, 0);
            else
                Model.SPCBattle[Index] = new SPCSound(Index, 1);
            RefreshSPC();
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            int offset;
            if (Type == 0)
                offset = Bits.GetInt24(Model.ROM, Index * 3 + 0x042748);
            else if (Type == 1)
                offset = Bits.GetShort(Model.ROM, Index * 4 + 0x042826) - 0x3400 + 0x042C26;
            else
                offset = Bits.GetShort(Model.ROM, Index * 4 + 0x043E26) - 0x3400 + 0x044226;
            Model.HexEditor.SetOffset(offset - 0xC00000);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        private void openPreviewer_Click(object sender, EventArgs e)
        {
            if (previewer != null && previewer.Visible)
                previewer.Close();
            if (Type == 0)
                previewer = new Previewer(Index, autoLaunch.Checked, EType.SPCTrack);
            else if (Type == 1)
                previewer = new Previewer(Index, autoLaunch.Checked, EType.SPCEvent);
            else if (Type == 2)
                previewer = new Previewer(Index, autoLaunch.Checked, EType.SPCBattle);
            if (!autoLaunch.Checked)
                previewer.Show();
        }
        // Track editor, picture
        private void channelTracks_Paint(object sender, PaintEventArgs e)
        {
            if (Index == 0 && Type == 0)
                return;
            //
            SolidBrush brush = new SolidBrush(Color.FromArgb(24, 0, 0, 0));
            if (mouseEnter && mouseOverChannel >= 0 && spc.ActiveChannels[mouseOverChannel])
                e.Graphics.FillRectangle(brush, 0, mouseOverChannel * 36 + 3, channelTracks.Width, 31);
            //
            brush = new SolidBrush(SystemColors.ControlText);
            Font font = new Font("Lucida Console", 8.25F);
            Pen pen = new Pen(Color.Black); pen.DashStyle = DashStyle.Dot;
            int x = 0;
            int y = 0;
            for (int t = 0; t < spc.Channels.Length; t++)
            {
                if (!spc.ActiveChannels[t] || spc.Channels[t] == null)
                    continue;
                pen.Color = Color.Black;
                e.Graphics.DrawLine(pen, 0, t * 36 + 2, channelTracks.Width, t * 36 + 2);
                e.Graphics.DrawLine(pen, 0, t * 36 + 34, channelTracks.Width, t * 36 + 34);
                for (int c = 0; c < spc.Channels[t].Count; c++)
                {
                    x = c * 24 + 8 - hScrollBarCT.Value;
                    y = t * 36 + 10;
                    if (x < -24 || x > channelTracks.Width + 24)
                        continue;
                    SPCCommand ssc = spc.Channels[t][c];
                    switch (ssc.Opcode)
                    {
                        case 0xC4: e.Graphics.DrawImage(Icons.octaveUp, x, y); break;
                        case 0xC5: e.Graphics.DrawImage(Icons.octaveDown, x, y); break;
                        case 0xC6: e.Graphics.DrawImage(Icons.octaveSet, x, y); break;
                        case 0xD0: e.Graphics.DrawImage(Icons.terminate, x, y); break;
                        case 0xD1: e.Graphics.DrawImage(Icons.metronome, x, y); break;
                        case 0xD4: e.Graphics.DrawImage(Icons.loop, x, y); break;
                        case 0xD5: e.Graphics.DrawImage(Icons.loopEnd, x, y); break;
                        case 0xD6: e.Graphics.DrawImage(Icons.firstSection, x, y); break;
                        case 0xD7: e.Graphics.DrawImage(Icons.loopInf, x, y); break;
                        case 0xDE: e.Graphics.DrawImage(Icons.instrument, x, y); break;
                        case 0xE2: e.Graphics.DrawImage(Icons.volume, x, y); break;
                        case 0xE5: e.Graphics.DrawImage(Icons.portamento, x, y); break;
                        case 0xE7: e.Graphics.DrawImage(Icons.speakerBalance, x, y); break;
                        case 0xEE: e.Graphics.DrawImage(Icons.drumsOn, x, y); break;
                        case 0xEF: e.Graphics.DrawImage(Icons.drumsOff, x, y); break;
                        case 0xF0: e.Graphics.DrawImage(Icons.tremolo, x, y); break;
                        case 0xF1: e.Graphics.DrawImage(Icons.vibrato, x, y); break;
                        case 0xFA: e.Graphics.DrawImage(Icons.reverbOn, x, y); break;
                        case 0xFB: e.Graphics.DrawImage(Icons.reverbOff, x, y); break;
                        default:
                            if (ssc.Opcode < 0xC4)
                            {
                                Bitmap image;
                                Note note = new Note(ssc);
                                if (!note.Rest && !note.Tie)
                                {
                                    Bitmap stem = GetStem(note.Ticks, 0, false, Color.Red);
                                    Bitmap head = GetHead(note.Ticks, 0, false, Color.Red);
                                    if (stem != null)
                                        e.Graphics.DrawImage(stem, x, y);
                                    e.Graphics.DrawImage(head, x, y);
                                }
                                else if (note.Rest)
                                {
                                    image = GetRest(note.Ticks, false, Color.Red);
                                    e.Graphics.DrawImage(image, x, y);
                                }
                                else if (note.Tie)
                                    e.Graphics.DrawImage(Icons.tieOver, x, y, 16, 16);
                            }
                            else
                                e.Graphics.DrawString(ssc.Opcode.ToString("X2"), font, brush, x, y + 2);
                            break;
                    }
                    pen.Color = Color.Red;
                    if (ssc == mouseDownSSC)
                        e.Graphics.DrawRectangle(pen, x - 4, y - 4, 24, 24);
                    pen.Color = Color.Black;
                }
            }
        }
        private void channelTracks_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseOverChannel = -1;
            this.mouseOverSSC = null;
            if (Index == 0)
                return;
            int x = e.X + hScrollBarCT.Value - 8;
            int y = e.Y % 36;
            int mouseOverChannel = e.Y / 36;
            if (mouseOverChannel < spc.Channels.Length)
                this.mouseOverChannel = mouseOverChannel;
            //
            if (mouseOverChannel < spc.Channels.Length &&
                spc.ActiveChannels[mouseOverChannel] &&
                spc.Channels[mouseOverChannel] != null &&
                x >= 0 && x % 24 < 16 && y >= 10 && y < 26)
            {
                int index = x / 24;
                if (index < spc.Channels[mouseOverChannel].Count)
                {
                    mouseOverSSC = spc.Channels[mouseOverChannel][index];
                    labelCommand.Text = interpreter.Interpret(mouseOverSSC);
                    labelBits.Text = "{" + BitConverter.ToString(mouseOverSSC.CommandData) + "}";
                    labelIndex.Text = "Index " + index;
                    channelTracks.Cursor = Cursors.Hand;
                }
            }
            else
            {
                labelCommand.Text = "...";
                labelBits.Text = "...";
                labelIndex.Text = "...";
                channelTracks.Cursor = Cursors.Arrow;
            }
            channelTracks.Invalidate();
        }
        private void channelTracks_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            channelTracks.Invalidate();
        }
        private void channelTracks_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            channelTracks.Invalidate();
        }
        private void channelTracks_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownSSC = null;
            mouseDownChannel = -1;
            if (Index == 0)
                return;
            if (mouseOverSSC != null)
            {
                mouseDownSSC = mouseOverSSC;
                mouseDownChannel = mouseOverChannel;
                ControlDisassemble();
            }
            channelTracks.Invalidate();
        }
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            channelTracks.Invalidate();
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            channelTracks.Invalidate();
        }
        private void activeChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            CheckBox activeChannel = (CheckBox)sender;
            int channel = (int)activeChannel.Tag;
            spc.ActiveChannels[channel] = activeChannel.Checked;
            if (spc.Channels[channel].Count == 0)
                spc.Channels[channel].Add(new SPCCommand(new byte[] { 0xD0 }, spc, channel));
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            ControlDisassemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        // Track editor, toolstrip
        private void moveLeft_Click(object sender, EventArgs e)
        {
            if (mouseDownSSC == null)
                return;
            List<SPCCommand> channel = spc.Channels[mouseDownSSC.Channel];
            int index = mouseDownSSC.Index;
            if (index > 0)
                channel.Reverse(index - 1, 2);
            spc.CreateNotes();
            ControlAssemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void moveRight_Click(object sender, EventArgs e)
        {
            if (mouseDownSSC == null)
                return;
            List<SPCCommand> channel = spc.Channels[mouseDownSSC.Channel];
            int index = mouseDownSSC.Index;
            if (index < channel.Count - 1)
                channel.Reverse(index, 2);
            spc.CreateNotes();
            ControlAssemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (mouseDownSSC == null)
                return;
            int index = mouseDownSSC.Index;
            spc.Channels[mouseDownSSC.Channel].Remove(mouseDownSSC);
            if (index >= 0 && spc.Channels[mouseDownSSC.Channel].Count > 0)
            {
                index = Math.Min(spc.Channels[mouseDownSSC.Channel].Count - 1, index);
                mouseDownSSC = spc.Channels[mouseDownSSC.Channel][index];
            }
            else
                mouseDownSSC = null;
            spc.CreateNotes();
            ControlDisassemble();
            ControlAssemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void newNote_Click(object sender, EventArgs e)
        {
            int index = -1;
            int channelIndex = -1;
            if (mouseDownSSC != null)
            {
                index = mouseDownSSC.Index;
                channelIndex = mouseDownSSC.Channel;
            }
            else
            {
                // look for 1st active channel and insert the command at the beginning
                for (int i = 0; i < spc.ActiveChannels.Length; i++)
                    if (spc.ActiveChannels[i]) { channelIndex = i; break; }
            }
            if (channelIndex == -1)
                return;
            if (newCommands.SelectedIndex == -1)
                return;
            int opcode;
            if (newCommands.SelectedIndex == 0)
                opcode = 0;
            else if (newCommands.SelectedIndex == 1)
                opcode = 0xB6;
            else
                opcode = newCommands.SelectedIndex + 0xC2;
            int length = SPCScriptEnums.CommandLengths[opcode];
            SPCCommand ssc;
            if (Type == 0)
                ssc = new SPCCommand(new byte[length], (SPCTrack)spc, channelIndex);
            else
                ssc = new SPCCommand(new byte[length], (SPCSound)spc, channelIndex);
            ssc.Opcode = (byte)opcode;
            List<SPCCommand> channel = spc.Channels[channelIndex];
            channel.Insert(index + 1, ssc);
            mouseDownSSC = ssc;
            spc.CreateNotes();
            ControlDisassemble();
            ControlAssemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void copy_Click(object sender, EventArgs e)
        {
            if (mouseDownSSC == null)
                return;
            copiedSSC = mouseDownSSC.Copy();
        }
        private void paste_Click(object sender, EventArgs e)
        {
            if (this.copiedSSC == null || mouseDownSSC == null)
                return;
            SPCCommand copiedSSC = this.copiedSSC.Copy();
            copiedSSC.Channel = mouseDownSSC.Channel;
            List<SPCCommand> channel = spc.Channels[mouseDownSSC.Channel];
            channel.Insert(mouseDownSSC.Index + 1, copiedSSC);
            mouseDownSSC = copiedSSC;
            spc.CreateNotes();
            ControlDisassemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void duplicate_Click(object sender, EventArgs e)
        {
            if (mouseDownSSC == null)
                return;
            SPCCommand copiedSSC = mouseDownSSC.Copy();
            List<SPCCommand> channel = spc.Channels[mouseDownSSC.Channel];
            channel.Insert(mouseDownSSC.Index + 1, copiedSSC);
            mouseDownSSC = copiedSSC;
            spc.CreateNotes();
            ControlDisassemble();
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void findCommand_Click(object sender, EventArgs e)
        {
            try
            {
                byte value = Convert.ToByte(findCommandText.Text, 16);
                for (int a = 0; a < 3; a++)
                {
                    SPC[] spcs;
                    string type;
                    if (a == 0) { spcs = Model.SPCs; type = "SPC Track"; }
                    else if (a == 1) { spcs = Model.SPCEvent; type = "Event SFX"; }
                    else { spcs = Model.SPCBattle; type = "Battle SFX"; }
                    for (int s = 0; s < spcs.Length; s++)
                    {
                        if (a == 0 && s == 0)
                            continue;
                        for (int c = 0; c < spcs[s].Channels.Length; c++)
                            for (int i = 0; i < spcs[s].Channels[c].Count; i++)
                                if (spcs[s].Channels[c][i].Opcode == value)
                                {
                                    DialogResult dialogResult = MessageBox.Show(
                                        "Found command in " + type + " index " + s + ", channel " + c + "." +
                                        "\n\nGo to command, continue searching, or stop searching?", "LAZY SHELL",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        Type = a;
                                        Index = s;
                                        mouseDownSSC = spcs[s].Channels[c][i];
                                        hScrollBarCT.Value = Math.Min(hScrollBarCT.Maximum, i * 24);
                                        ControlDisassemble();
                                        channelTracks.Invalidate();
                                        return;
                                    }
                                    else if (dialogResult == DialogResult.No)
                                        continue;
                                    else if (dialogResult == DialogResult.Cancel)
                                        return;
                                }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void findCommandText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                findCommand.PerformClick();
        }
        // Track editor, commands
        private void opcodeByte1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
                ControlDisassemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void parameterByte1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void parameterByte2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void parameterByte3_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void parameterName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void noteNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void noteLengthName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void noteLengthByte_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            try
            {
                ControlAssemble();
            }
            catch
            {
                MessageBox.Show("Couldn't change value.");
            }
            channelTracks.Invalidate();
            scoreViewPicture.Invalidate();
        }
        // Score viewer
        private void scoreViewPicture_Paint(object sender, PaintEventArgs e)
        {
            if (Index == 0 || Type != 0)
                return;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            int staffIndex = 0;
            int max = spc.Channels.Length;
            for (; staffIndex < max; staffIndex++)
            {
                if (spc.Channels[staffIndex] == null || !spc.ActiveChannels[staffIndex])
                    continue;
                int x = -hScrollBarSV.Value + 64 + GetKeyWidth(this.key);
                int y = staffIndex * staffHeight;
                int middle = staffHeight / 2;
                // draw staff hilite
                SolidBrush brush = new SolidBrush(Color.White);
                if (staffIndex == mouseDownChannel)
                    e.Graphics.FillRectangle(brush, 0, y + 4, scoreViewPicture.Width, staffHeight - 8);
                // draw staff ledger lines
                DrawBarsLines(e.Graphics, clefSV.SelectedIndex, key, staffIndex, -hScrollBarSV.Value, true, GetStaffWidth(staffIndex));
                // draw notes
                int indexNotes = 0;
                Note lastNote = null; // the last previous note, with a pitch
                Note lastItem = null; // the last previous note
                for (int i = 0; i < spc.Notes[staffIndex].Count; i++)
                {
                    object item = spc.Notes[staffIndex][i];
                    if (item.GetType() != typeof(Note))
                    {
                        indexNotes++;
                        continue;
                    }
                    Note note = (Note)item;
                    if (x < -32 || x - 32 > scoreViewPicture.Width)
                    {
                        x += (int)((double)noteSpacingSV.Value / 100.0 * note.Ticks);
                        if (!note.Rest && !note.Tie)
                            lastNote = note;
                        lastItem = note;
                        indexNotes++;
                        continue;
                    }
                    switch (note.Command.Opcode)
                    {
                        case 0xD7: e.Graphics.DrawImage(Icons.loopInf, (int)x, y + 8); break;
                        case 0xDE:
                            e.Graphics.DrawImage(Icons.instrument, (int)x, y + 8);
                            brush.Color = Color.FromArgb(96, 96, 96);
                            Font font = new Font("Arial", 8.25F);
                            e.Graphics.DrawString(Lists.SampleNames[note.Command.Param1], font, brush, (int)x + 16, y + 8);
                            break;
                        case 0xEE: e.Graphics.DrawImage(Icons.drumsOn, (int)x, y + 8); break;
                        case 0xEF: e.Graphics.DrawImage(Icons.drumsOff, (int)x, y + 8); break;
                        default:
                            bool hilite = mouseEnter && indexNotes == mouseOverNote &&
                                staffIndex == mouseOverChannel && staffIndex == mouseDownChannel;
                            bool select;
                            if (overlay.Select != null)
                                select = WithinSelection(i, false) && staffIndex == mouseDownChannel;
                            else
                                select = mouseDownNote == i && staffIndex == mouseDownChannel;
                            DrawNote(e.Graphics, note, lastNote, lastItem, x, staffHeight,
                                clefSV.SelectedIndex, key, staffIndex, hilite, select);
                            break;
                    }
                    x += (int)((double)noteSpacingSV.Value / 100.0 * note.Ticks);
                    if (!note.Rest && !note.Tie)
                        lastNote = note;
                    lastItem = note;
                    indexNotes++;
                }
                // draw extra lines beyond normal lines, if mouse over it
                if (mouseEnter && staffIndex == mouseOverChannel && staffIndex == mouseDownChannel && !wSelect.Checked)
                {
                    DrawOutsideLines(e.Graphics, new Pen(Color.Gray), mousePosition.X, y, middle,
                        clefSV.SelectedIndex, mouseOverOctave, mouseOverPitch, mouseOverLine);
                    brush.Color = Color.FromArgb(128, Color.Black);
                    if (insertObject != null && mouseEnter && mousePosition.X != -1 && mousePosition.Y != -1)
                        e.Graphics.FillEllipse(brush, mousePosition.X - 4, mousePosition.Y / 4 * 4, 8, 8);
                }
            }
            // draw selection box
            if (rSelect.Checked && overlay.Select != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                overlay.Select.DrawSelectionBox(e.Graphics, 1);
            }
        }
        private void scoreViewPicture_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Math.Max(e.X, 0);
            int y = Math.Max(e.Y, 0);
            #region Selecting
            if (rSelect.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x, y))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Min(x, scoreViewPicture.Width),
                        Math.Min(y, scoreViewPicture.Height));
                }
            }
            #endregion
            #region Mouse Over
            mousePosition = new Point(x, y);
            mouseOverNote = -1;
            mouseOverPitch = Pitch.NULL;
            mouseOverChannel = -1;
            mouseOverSSC = null;
            if (y / staffHeight >= spc.Channels.Length)
            {
                scoreViewPicture.Invalidate();
                labelRNote.Text = "...";
                return;
            }
            //
            mouseOverChannel = y / staffHeight;
            if (mouseOverChannel != mouseDownChannel)
            {
                scoreViewPicture.Invalidate();
                labelRNote.Text = "...";
                return;
            }
            //
            x = Math.Max(x + hScrollBarSV.Value, 0);
            y = y % staffHeight;
            mouseOverNote = GetNoteIndex(x, this.key);
            if (mouseOverNote >= 0 && mouseOverNote < spc.Notes[mouseOverChannel].Count)
            {
                Note note = spc.Notes[mouseOverChannel][mouseOverNote];
                mouseOverSSC = note.Command;
            }
            // 88 pixels p/staff, pitches are separate by 4 pixels, 11 lines p/staff
            int staffOffset = 0;
            if (clefSV.SelectedIndex == 0)
                staffOffset = -1;
            else if (clefSV.SelectedIndex == 1)
                staffOffset = 1;
            // 272 is the staff size where everything is perfect
            int line = (staffHeight / 4) - (y / 4) + ((272 - staffHeight) / 8) + staffOffset;
            mouseOverLine = line % 7;
            switch (mouseOverLine)
            {
                case 0: mouseOverPitch = Pitch.C; labelRNote.Text = "C"; break; // A, A#
                case 1: mouseOverPitch = Pitch.D; labelRNote.Text = "D"; break; // B
                case 2: mouseOverPitch = Pitch.E; labelRNote.Text = "E"; break; // C, C#
                case 3: mouseOverPitch = Pitch.F; labelRNote.Text = "F"; break; // D, D#
                case 4: mouseOverPitch = Pitch.G; labelRNote.Text = "G"; break; // E
                case 5: mouseOverPitch = Pitch.A; labelRNote.Text = "A"; break; // F, F#
                case 6: mouseOverPitch = Pitch.B; labelRNote.Text = "B"; break; // G, G#
                default: mouseOverPitch = Pitch.NULL; labelRNote.Text = ""; break;
            }
            // adjust pitch based on checked accidental AND key signature
            if (rSharp.Checked)
                mouseOverPitch++;
            if (rFlat.Checked)
                mouseOverPitch--;
            Accidental accidental = GetAccidental(this.key, mouseOverPitch);
            if (!rNatural.Checked && accidental == Accidental.Natural)
            {
                if ((this.key >= Key.CMajor && this.key <= Key.CsMajor) ||
                    (this.key >= Key.AMinor && this.key <= Key.AsMinor)) // sharp key
                    mouseOverPitch++;
                else
                    mouseOverPitch--; // flat key
            }
            // 7 lines per octave, so 28 (7 * 4), offset by 52
            mouseOverOctave = line / 7;
            if (mouseOverPitch == Pitch.Rest)
            {
                mouseOverPitch = Pitch.C;
                mouseOverOctave++;
            }
            //
            if (mouseOverChannel == mouseDownChannel)
                labelRNote.Text += mouseOverOctave.ToString();
            else
                labelRNote.Text = "...";
            #endregion
            scoreViewPicture.Invalidate();
        }
        private void scoreViewPicture_MouseDown(object sender, MouseEventArgs e)
        {
            overlay.Select.Clear();
            if (!spc.ActiveChannels[mouseOverChannel])
                return;
            if (mouseOverChannel == -1)
                return;
            if (mouseDownChannel != mouseOverChannel)
            {
                mouseDownChannel = mouseOverChannel;
                return;
            }
            // if clicking picture right after undo/redo, we must do a manual mouseMove
            if (mouseOverNote == -1)
                scoreViewPicture_MouseMove(sender, e);
            // Get index to insert between notes (and commands)
            mouseDownSSC = mouseOverSSC;
            mouseDownNote = mouseOverNote;
            // set the selected command in the track viewer
            SelectNote(false);
            //
            if (rDraw.Checked)
            {
                Draw(false);
                return;
            }
            if (rErase.Checked)
            {
                Erase(false);
                return;
            }
            if (rSelect.Checked)
            {
                overlay.Select.Refresh(1, e.X, e.Y, 1, 1, scoreViewPicture);
                scoreViewPicture.Invalidate();
                return;
            }
            if (rPaste.Checked)
            {
                if (mouseOverSSC != null)
                    Paste(mouseOverSSC.Index, false);
                else
                    Paste(spc.Channels[mouseDownChannel].Count, false);
                return;
            }
        }
        private void scoreViewPicture_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void scoreViewPicture_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            scoreViewPicture.Focus();
        }
        private void scoreViewPicture_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            labelRNote.Text = "...";
            scoreViewPicture.Invalidate();
        }
        private void scoreViewPicture_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.D: rDraw.PerformClick(); break;
                case Keys.E: rErase.PerformClick(); break;
                case Keys.S: rSelect.PerformClick(); break;
                case Keys.Control | Keys.C: rCopy.PerformClick(); break;
                case Keys.Control | Keys.X: rCut.PerformClick(); break;
                case Keys.Delete: rDelete.PerformClick(); break;
                case Keys.Control | Keys.Z: rUndo.PerformClick(); break;
                case Keys.Control | Keys.Y: rRedo.PerformClick(); break;
                case Keys.Left:
                    if (mouseDownNote > 0)
                    {
                        mouseDownNote--;
                        Note note = spc.Notes[mouseDownChannel][mouseDownNote];
                        int ticks = (int)((double)noteSpacing / 100.0 * note.Ticks);
                        mouseDownSSC = note.Command;
                        if (hScrollBarSV.Value - ticks >= 0)
                            hScrollBarSV.Value -= ticks;
                        SelectNote(false);
                    }
                    break;
                case Keys.Right:
                    if (mouseDownNote < spc.Notes[mouseDownChannel].Count - 1)
                    {
                        Note note = spc.Notes[mouseDownChannel][mouseDownNote];
                        int ticks = (int)((double)noteSpacing / 100.0 * note.Ticks);
                        mouseDownNote++;
                        note = spc.Notes[mouseDownChannel][mouseDownNote];
                        mouseDownSSC = note.Command;
                        if (hScrollBarSV.Value + ticks <= hScrollBarSV.Maximum)
                            hScrollBarSV.Value += ticks;
                        SelectNote(false);
                    }
                    break;
            }
        }
        private void staffHeightChannel_ValueChanged(object sender, EventArgs e)
        {
            staffHeightSV.Value = (int)staffHeightSV.Value / 8 * 8;
            scoreViewPicture.Height = (int)(staffHeightSV.Value * 8);
            scoreViewPicture.Invalidate();
        }
        private void keySV_SelectedIndexChanged(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void clefSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void time_ValueChanged(object sender, EventArgs e)
        {
            scoreWriterPicture.Invalidate();
            scoreViewPicture.Invalidate();
        }
        private void noteSpacing_ValueChanged(object sender, EventArgs e)
        {
            if (Index == 0 && Type == 0)
                return;
            noteSpacingSV.Value = (int)noteSpacingSV.Value / 10 * 10;
            this.Updating = true;
            hScrollBarSV.Maximum = 0;
            for (int i = 0; i < spc.Channels.Length; i++)
            {
                activeChannels[i].Checked = spc.ActiveChannels[i];
                if (!spc.ActiveChannels[i])
                    continue;
                int maximum = 0;
                if (spc.Notes != null)
                    foreach (Note note in spc.Notes[i])
                    {
                        maximum += (int)((double)noteSpacingSV.Value / 100.0 * note.Ticks);
                        if (maximum > hScrollBarSV.Maximum)
                            hScrollBarSV.Maximum = maximum;
                    }
            }
            this.Updating = false;
            scoreViewPicture.Invalidate();
        }
        private void showRests_CheckedChanged(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void singleChannelNum_ValueChanged(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void scoreView_Click(object sender, EventArgs e)
        {
            if (scoreViewer.Checked)
            {
                groupBoxI.Visible = false;
                groupBoxRV.Visible = false;
                groupBoxPR.Visible = false;
                groupBoxSV.Visible = true;
                groupBoxSV.BringToFront();
            }
            else
            {
                groupBoxI.Visible = true;
                groupBoxRV.Visible = true;
                groupBoxPR.Visible = true;
                groupBoxSV.Visible = false;
                groupBoxSV.SendToBack();
            }
            mouseOverNote = -1;
            mouseDownNote = -1;
            mouseOverPitch = Pitch.NULL;
            //ResizePanels();
        }
        private void panelSPC_SizeChanged(object sender, EventArgs e)
        {
            //ResizePanels();
        }
        private void hScrollBarSV_ValueChanged(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void hScrollBarSV_Scroll(object sender, ScrollEventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        private void groupBoxSV_Resize(object sender, EventArgs e)
        {
            scoreViewPicture.Invalidate();
        }
        //
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (mouseOverChannel < 0 || !spc.ActiveChannels[mouseOverChannel])
                e.Cancel = true;
        }
        private void importTrack_Click(object sender, EventArgs e)
        {
            if (Type == 0)
                new IOElements((Element[])Model.SPCs, Index, "IMPORT SPCs...").ShowDialog();
            else if (Type == 1)
                new IOElements((Element[])Model.SPCEvent, Index, "IMPORT EVENT SOUNDS...").ShowDialog();
            else
                new IOElements((Element[])Model.SPCBattle, Index, "IMPORT BATTLE SOUNDS...").ShowDialog();
            RefreshSPC();
        }
        private void importMML_Click(object sender, EventArgs e)
        {
            if (!ImportMMLScript(spc.Channels, spc.ActiveChannels))
                return;
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            spc.CreateNotes();
            RefreshSPC();
        }
        private void importScript_Click(object sender, EventArgs e)
        {
            if (!ImportSPCScript(ref spc.Channels[mouseOverChannel]))
                return;
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            RefreshSPC();
        }
        private void exportTrack_Click(object sender, EventArgs e)
        {
            if (Type == 0)
                new IOElements((Element[])Model.SPCs, Index, "EXPORT SPCs...").ShowDialog();
            else if (Type == 1)
                new IOElements((Element[])Model.SPCEvent, Index, "EXPORT EVENT SOUNDS...").ShowDialog();
            else
                new IOElements((Element[])Model.SPCBattle, Index, "EXPORT BATTLE SOUNDS...").ShowDialog();
        }
        private void exportMML_Click(object sender, EventArgs e)
        {
            ExportMMLScript(Type);
        }
        private void exportScript_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - ";
            if (Type == 0)
                saveFileDialog.FileName += "SPCScript." + Index.ToString("d2");
            else if (Type == 1)
                saveFileDialog.FileName += "EVTSoundFX." + Index.ToString("d3");
            else
                saveFileDialog.FileName += "BATSoundFX." + Index.ToString("d3");
            saveFileDialog.FileName += ".channel." + mouseOverChannel;
            saveFileDialog.FileName += ".txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            StreamWriter script = File.CreateText(saveFileDialog.FileName);
            List<SPCCommand> channel = spc.Channels[mouseOverChannel];
            foreach (SPCCommand ssc in channel)
                script.WriteLine(ssc.ToString());
            script.Close();
            //
            //for (int s = 0; s < spcs.Length; s++)
            //{
            //    if (spcs[s].Channels == null) continue;
            //    for (int c = 0; c < spcs[s].Channels.Length; c++)
            //    {
            //        StreamWriter script = File.CreateText(Path.GetDirectoryName(saveFileDialog.FileName) + 
            //            "\\SPCScript." + s + ".channel." + c + ".txt");
            //        foreach (SPCScriptCommand ssc in spcs[s].Channels[c])
            //            script.WriteLine(ssc.ToString());
            //        script.Close();
            //    }
            //}
        }
        private void clearChannel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all data in this channel -- are you sure?", "LAZY SHELL",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            spc.Channels[mouseOverChannel] = new List<SPCCommand>();
            spc.Channels[mouseOverChannel].Add(new SPCCommand(new byte[] { 0xD0 }, spc, mouseOverChannel));
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            RefreshSPC();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            ClearElements clearElements;
            if (Type == 0)
                clearElements = new ClearElements(Model.SPCs, Index, "CLEAR SPCS...");
            else if (Type == 1)
                clearElements = new ClearElements(Model.SPCEvent, Index, "CLEAR EVENT SOUNDS...");
            else
                clearElements = new ClearElements(Model.SPCBattle, Index, "CLEAR BATTLE SOUNDS...");
            clearElements.ShowDialog();
            if (clearElements.DialogResult == DialogResult.Cancel)
                return;
            if (Type == 0)
                ((SPCTrack)spc).AssembleSPCData();
            RefreshSPC();
        }
        //
        private void reader_Click(object sender, EventArgs e)
        {
            ToolStripButton insertObject = (ToolStripButton)sender;
            foreach (ToolStripItem item in rToolStripNote.Items)
                if (item.GetType() == typeof(ToolStripButton) &&
                    item != insertObject)
                    ((ToolStripButton)item).Checked = false;
            foreach (ToolStripItem item in rToolStripNote.Items)
                if (item.GetType() == typeof(ToolStripButton) &&
                    item != insertObject &&
                    item != rSharp &&
                    item != rNatural &&
                    item != rFlat &&
                    item != rTie)
                    ((ToolStripButton)item).Checked = false;
            if (insertObject == ticksNoteButton)
                ticksRestButton.Checked = false;
            if (insertObject == ticksRestButton)
                ticksNoteButton.Checked = false;
            if (insertObject.Checked)
            {
                rDraw.Checked = true;
                this.insertObject = insertObject;
                scoreViewPicture.Cursor = NewCursors.Draw;
            }
            else
            {
                this.insertObject = null;
                scoreViewPicture.Cursor = Cursors.Arrow;
            }
        }
        private void rDraw_CheckedChanged(object sender, EventArgs e)
        {
            if (rDraw.Checked)
            {
                rErase.Checked = false;
                rSelect.Checked = false;
                rPaste.Checked = false;
                scoreViewPicture.Cursor = NewCursors.Draw;
            }
            else
                scoreViewPicture.Cursor = Cursors.Arrow;
            scoreViewPicture.Invalidate();
        }
        private void rErase_CheckedChanged(object sender, EventArgs e)
        {
            if (rErase.Checked)
            {
                rDraw.Checked = false;
                rSelect.Checked = false;
                rPaste.Checked = false;
                scoreViewPicture.Cursor = NewCursors.Erase;
            }
            else
                scoreViewPicture.Cursor = Cursors.Arrow;
            scoreWriterPicture.Invalidate();
        }
        private void rSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (rSelect.Checked)
            {
                rDraw.Checked = false;
                rErase.Checked = false;
                rPaste.Checked = false;
                scoreViewPicture.Cursor = Cursors.Cross;
            }
            else
            {
                overlay.Select.Clear();
                scoreViewPicture.Cursor = Cursors.Arrow;
            }
            scoreViewPicture.Invalidate();
        }
        private void rPaste_CheckedChanged(object sender, EventArgs e)
        {
            if (rPaste.Checked)
            {
                rDraw.Checked = false;
                rErase.Checked = false;
                rSelect.Checked = false;
                scoreViewPicture.Cursor = NewCursors.Draw;
            }
            else
                scoreViewPicture.Cursor = Cursors.Arrow;
            scoreViewPicture.Invalidate();
        }
        private void rDelete_Click(object sender, EventArgs e)
        {
            Delete(false);
        }
        private void rCut_Click(object sender, EventArgs e)
        {
            Delete(false);
            Copy(false);
        }
        private void rCopy_Click(object sender, EventArgs e)
        {
            Copy(false);
        }
        private void rAccidental_Click(object sender, EventArgs e)
        {
            if (sender == rSharp)
            {
                rFlat.Checked = false;
                rNatural.Checked = false;
            }
            else if (sender == rFlat)
            {
                rSharp.Checked = false;
                rNatural.Checked = false;
            }
            else
            {
                rSharp.Checked = false;
                rFlat.Checked = false;
            }
        }
        private void rTie_Click(object sender, EventArgs e)
        {
        }
        private void undo_Click(object sender, EventArgs e)
        {
            if (scoreWriter.Checked)
            {
                commandStackW.UndoCommand();
                SetScrollBars(true);
                wStaffDelete.Enabled = staffs.Count != 0;
                wStaffMoveDown.Enabled = staffs.Count != 0;
                wStaffMoveUp.Enabled = staffs.Count != 0;
                if (mouseDownStaff >= staffs.Count)
                    mouseDownStaff = staffs.Count - 1;
                if (mouseOverStaff >= staffs.Count)
                    mouseOverStaff = staffs.Count - 1;
                mouseOverNote = -1;
                mouseOverPitch = Pitch.NULL;
                //
                RefreshStaff();
                scoreWriterPicture.Invalidate();
            }
            else
            {
                commandStackR.UndoCommand();
                SetScrollBars(false);
                mouseOverNote = -1;
                mouseOverPitch = Pitch.NULL;
                //
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                spc.CreateNotes();
                scoreViewPicture.Invalidate();
                channelTracks.Invalidate();
                CalculateFreeSpace(Type, true, false);
            }
        }
        private void redo_Click(object sender, EventArgs e)
        {
            if (scoreWriter.Checked)
            {
                commandStackW.RedoCommand();
                SetScrollBars(true);
                wStaffDelete.Enabled = staffs.Count != 0;
                wStaffMoveDown.Enabled = staffs.Count != 0;
                wStaffMoveUp.Enabled = staffs.Count != 0;
                if (mouseDownStaff >= staffs.Count)
                    mouseDownStaff = staffs.Count - 1;
                if (mouseOverStaff >= staffs.Count)
                    mouseOverStaff = staffs.Count - 1;
                mouseOverNote = -1;
                mouseOverPitch = Pitch.NULL;
                //
                RefreshStaff();
                scoreWriterPicture.Invalidate();
            }
            else
            {
                commandStackR.RedoCommand();
                SetScrollBars(false);
                mouseOverNote = -1;
                mouseOverPitch = Pitch.NULL;
                //
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                spc.CreateNotes();
                scoreViewPicture.Invalidate();
                channelTracks.Invalidate();
                CalculateFreeSpace(Type, true, false);
            }
        }
        #endregion
    }
}
