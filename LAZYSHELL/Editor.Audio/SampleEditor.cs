using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class SampleEditor : NewForm
    {
        // variables
        private Settings settings = Settings.Default;
        public SoundPlayer SoundPlayer = new SoundPlayer();
        private byte[] wav;
        private byte[] loop;
        public int Index { get { return (int)sampleNum.Value; } set { sampleNum.Value = value; } }
        private BRRSample[] samples { get { return Model.AudioSamples; } set { Model.AudioSamples = value; } }
        private BRRSample sample { get { return samples[Index]; } set { samples[Index] = value; } }
        //
        private Search searchWindow;
        private EditLabel labelWindow;
        // constructor
        public SampleEditor()
        {
            InitializeComponent();
            sampleName.Items.AddRange(Lists.Numerize(Lists.SampleNames));
            sampleRateName.SelectedIndex = 5;
            sampleName.SelectedIndex = 0;
            if (settings.RememberLastIndex)
            {
                if (settings.LastAudioSample == 0)
                {
                    wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
                    loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
                }
                else
                    sampleNum.Value = settings.LastAudioSample;
            }
            else
            {
                wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
                loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            }
            searchWindow = new Search(sampleNum, searchBox, searchNames, sampleName.Items);
            labelWindow = new EditLabel(sampleName, sampleNum, "Samples", true);
            //
            this.History = new History(this, sampleName, sampleNum);
        }
        // functions
        private void RefreshSample()
        {
            this.Updating = true;
            relFreq.Value = sample.RelFreq;
            relGain.Value = sample.RelGain;
            loopStart.Value = sample.LoopStart / 9;
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
            this.Updating = false;
        }
        private void DrawWavelength(Graphics g, int width, int height, byte[] wav)
        {
            int size = Bits.GetInt32(wav, 0x0028) / 2;
            int offset = 0x2C;
            List<Point> points = new List<Point>();
            double w_ratio = (double)width / (double)size;
            double h_ratio = (double)height / 65536.0;
            for (int i = 0; i < size; i++)
            {
                int x = (int)((double)i * w_ratio);
                int y = (int)((double)(Bits.GetShort(wav, offset) ^ 0x8000) * h_ratio);
                points.Add(new Point(x, y));
                offset += 2;
            }
            int loopStart = (int)((double)sample.LoopStart / (double)sample.Length * (double)width);
            if (loopStart < 0)
                loopStart = 0;
            g.DrawLine(Pens.Gray, 0, height / 2, width, height / 2);
            g.DrawLine(Pens.Gray, loopStart, 0, loopStart, height);
            g.DrawLines(Pens.Lime, points.ToArray());
        }
        public void Assemble()
        {
            int i = 0;
            int offset06 = 0x060939;
            int offset14 = 0x146000;
            int offset1C = 0x1C8000;
            int offset1D = 0x1DC600;
            // check if room for next in bank 14
            for (; i < samples.Length && offset14 + samples[i].Length < 0x148000; i++)
                samples[i].Assemble(ref offset14);
            // check if room for next in bank 06
            for (; i < samples.Length && offset06 + samples[i].Length < 0x094000; i++)
                samples[i].Assemble(ref offset06);
            // check if room for next in bank 1C
            for (; i < samples.Length && offset1C + samples[i].Length < 0x1CEA00; i++)
                samples[i].Assemble(ref offset1C);
            // check if room for next in bank 1D
            for (; i < samples.Length && offset1D + samples[i].Length < 0x1DDE00; i++)
                samples[i].Assemble(ref offset1D);
            if (i < samples.Length)
                MessageBox.Show("Not enough space to save all samples. Stopped saving at index " + i.ToString("d3") + ".");
        }
        // event handlers
        private void sampleNum_ValueChanged(object sender, EventArgs e)
        {
            sampleName.SelectedIndex = (int)sampleNum.Value;
            //
            RefreshSample();
            if (settings.RememberLastIndex)
                settings.LastAudioSample = (int)sampleNum.Value;
        }
        private void sampleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            sampleNum.Value = sampleName.SelectedIndex;
        }
        private void sampleRate_CheckedChanged(object sender, EventArgs e)
        {
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            sampleRateName.Enabled = rateFixed.Checked;
            rateManualValue.Enabled = rateManual.Checked;
        }
        private void sampleRateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
        }
        private void rateManualValue_ValueChanged(object sender, EventArgs e)
        {
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
        }
        private void loopStart_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            sample.LoopStart = (int)loopStart.Value * 9;
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void relGain_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            sample.RelGain = (short)relGain.Value;
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void relFreq_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            sample.RelFreq = (short)relFreq.Value;
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void buttonPitch_Click(object sender, EventArgs e)
        {
            if (pitchChange.SelectedIndex < 0)
                return;
            if (pitchChange.SelectedIndex == 0 && relFreq.Value - 512 >= -32768)
                relFreq.Value -= 512;
            if (pitchChange.SelectedIndex == 1 && relFreq.Value - 256 >= -32768)
                relFreq.Value -= 256;
            if (pitchChange.SelectedIndex == 2 && relFreq.Value + 256 <= 32767)
                relFreq.Value += 256;
            if (pitchChange.SelectedIndex == 3 && relFreq.Value + 512 <= 32767)
                relFreq.Value += 512;
        }
        private void play_Click(object sender, EventArgs e)
        {
            SoundPlayer.Stop();
            if (infiniteLoop.Checked)
                Do.Play(SoundPlayer, loop, true);
            else
                Do.Play(SoundPlayer, wav, false);
        }
        private void back_Click(object sender, EventArgs e)
        {
            if (sampleNum.Value > 0)
                sampleNum.Value--;
            SoundPlayer.Stop();
            Do.Play(SoundPlayer, wav, false);
        }
        private void foward_Click(object sender, EventArgs e)
        {
            if (sampleNum.Value < sampleNum.Maximum)
                sampleNum.Value++;
            SoundPlayer.Stop();
            Do.Play(SoundPlayer, wav, false);
        }
        private void pause_Click(object sender, EventArgs e)
        {
            SoundPlayer.Stop();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawWavelength(e.Graphics, pictureBox1.Width, pictureBox1.Height, wav);
        }
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        //
        private void import_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.AudioSamples, Index, "IMPORT SAMPLE WAVs...").ShowDialog();
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void importBRR_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.AudioSamples, Index, "IMPORT SAMPLE BRRs...").ShowDialog();
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.AudioSamples, Index, "EXPORT SAMPLE WAVs...", sample.Rate).ShowDialog();
        }
        private void exportBRR_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.AudioSamples, Index, "EXPORT SAMPLE BRRs...", sample.Rate).ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            new ClearElements(Model.AudioSamples, Index, "CLEAR SAMPLES...").ShowDialog();
            wav = BRR.BRRToWAV(sample.Sample, sample.Rate);
            loop = BRR.BRRToWAV(sample.Sample, sample.Rate, sample.LoopStart);
            pictureBox1.Invalidate();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the current sample? You will lose all unsaved changes.",
                "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            sample = new BRRSample(Index);
            RefreshSample();
        }
        private void label1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
