
namespace LAZYSHELL
{
    partial class SampleEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sampleName = new System.Windows.Forms.ToolStripComboBox();
            this.sampleNum = new LAZYSHELL.ToolStripNumericUpDown();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.searchNames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.import = new System.Windows.Forms.ToolStripDropDownButton();
            this.importWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.importBRR = new System.Windows.Forms.ToolStripMenuItem();
            this.export = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBRR = new System.Windows.Forms.ToolStripMenuItem();
            this.clear = new System.Windows.Forms.ToolStripButton();
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.play = new System.Windows.Forms.ToolStripButton();
            this.stop = new System.Windows.Forms.ToolStripButton();
            this.back = new System.Windows.Forms.ToolStripButton();
            this.foward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.infiniteLoop = new LAZYSHELL.ToolStripCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPitch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pitchChange = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loopStart = new System.Windows.Forms.NumericUpDown();
            this.relGain = new System.Windows.Forms.NumericUpDown();
            this.relFreq = new System.Windows.Forms.NumericUpDown();
            this.sampleRateName = new System.Windows.Forms.ComboBox();
            this.rateManualValue = new System.Windows.Forms.NumericUpDown();
            this.rateFixed = new System.Windows.Forms.RadioButton();
            this.rateManual = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateManualValue)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleName,
            this.sampleNum,
            this.searchBox,
            this.searchNames,
            this.toolStripSeparator4,
            this.import,
            this.export,
            this.clear,
            this.reset,
            this.toolStripSeparator2,
            this.play,
            this.stop,
            this.back,
            this.foward,
            this.toolStripSeparator1,
            this.infiniteLoop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(737, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // sampleName
            // 
            this.sampleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sampleName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sampleName.Name = "sampleName";
            this.sampleName.Size = new System.Drawing.Size(170, 25);
            this.sampleName.SelectedIndexChanged += new System.EventHandler(this.sampleName_SelectedIndexChanged);
            // 
            // sampleNum
            // 
            this.sampleNum.AutoSize = false;
            this.sampleNum.ContextMenuStrip = null;
            this.sampleNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sampleNum.Hexadecimal = false;
            this.sampleNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sampleNum.Location = new System.Drawing.Point(179, 2);
            this.sampleNum.Maximum = new decimal(new int[] {
            115,
            0,
            0,
            0});
            this.sampleNum.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sampleNum.Name = "sampleNum";
            this.sampleNum.Size = new System.Drawing.Size(50, 21);
            this.sampleNum.Text = "0";
            this.sampleNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sampleNum.ValueChanged += new System.EventHandler(this.sampleNum_ValueChanged);
            // 
            // searchBox
            // 
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(150, 25);
            // 
            // searchNames
            // 
            this.searchNames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchNames.Image = global::LAZYSHELL.Properties.Resources.search;
            this.searchNames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.searchNames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchNames.Name = "searchNames";
            this.searchNames.Size = new System.Drawing.Size(23, 22);
            this.searchNames.Text = "Search for effect";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // import
            // 
            this.import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importWAV,
            this.importBRR});
            this.import.Image = global::LAZYSHELL.Properties.Resources.importWAV;
            this.import.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(27, 22);
            // 
            // importWAV
            // 
            this.importWAV.Image = global::LAZYSHELL.Properties.Resources.importWAV;
            this.importWAV.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importWAV.Name = "importWAV";
            this.importWAV.Size = new System.Drawing.Size(141, 22);
            this.importWAV.Text = "Import WAV...";
            this.importWAV.Click += new System.EventHandler(this.import_Click);
            // 
            // importBRR
            // 
            this.importBRR.Image = global::LAZYSHELL.Properties.Resources.importBinary;
            this.importBRR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.importBRR.Name = "importBRR";
            this.importBRR.Size = new System.Drawing.Size(141, 22);
            this.importBRR.Text = "Import BRR...";
            this.importBRR.Click += new System.EventHandler(this.importBRR_Click);
            // 
            // export
            // 
            this.export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportWAV,
            this.exportBRR});
            this.export.Image = global::LAZYSHELL.Properties.Resources.exportWAV;
            this.export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(27, 22);
            // 
            // exportWAV
            // 
            this.exportWAV.Image = global::LAZYSHELL.Properties.Resources.exportWAV;
            this.exportWAV.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportWAV.Name = "exportWAV";
            this.exportWAV.Size = new System.Drawing.Size(139, 22);
            this.exportWAV.Text = "Export WAV...";
            this.exportWAV.Click += new System.EventHandler(this.export_Click);
            // 
            // exportBRR
            // 
            this.exportBRR.Image = global::LAZYSHELL.Properties.Resources.exportBinary;
            this.exportBRR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportBRR.Name = "exportBRR";
            this.exportBRR.Size = new System.Drawing.Size(139, 22);
            this.exportBRR.Text = "Export BRR...";
            this.exportBRR.Click += new System.EventHandler(this.exportBRR_Click);
            // 
            // clear
            // 
            this.clear.Image = global::LAZYSHELL.Properties.Resources.clear_small;
            this.clear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(23, 22);
            this.clear.ToolTipText = "Clear";
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = global::LAZYSHELL.Properties.Resources.reset;
            this.reset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.Text = "Reset sample";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // play
            // 
            this.play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.play.Image = global::LAZYSHELL.Properties.Resources.play;
            this.play.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(23, 22);
            this.play.Text = "Play";
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // stop
            // 
            this.stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stop.Image = global::LAZYSHELL.Properties.Resources.stop;
            this.stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(23, 22);
            this.stop.Text = "Stop";
            this.stop.Click += new System.EventHandler(this.pause_Click);
            // 
            // back
            // 
            this.back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.back.Image = global::LAZYSHELL.Properties.Resources.back;
            this.back.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(23, 22);
            this.back.Text = "Play Previous";
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // foward
            // 
            this.foward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.foward.Image = global::LAZYSHELL.Properties.Resources.foward;
            this.foward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.foward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.foward.Name = "foward";
            this.foward.Size = new System.Drawing.Size(23, 22);
            this.foward.Text = "Play Next";
            this.foward.Click += new System.EventHandler(this.foward_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // infiniteLoop
            // 
            this.infiniteLoop.Checked = false;
            this.infiniteLoop.Name = "infiniteLoop";
            this.infiniteLoop.Padding = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.infiniteLoop.Size = new System.Drawing.Size(89, 22);
            this.infiniteLoop.Text = "Infinite loop";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPitch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pitchChange);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.loopStart);
            this.groupBox1.Controls.Add(this.relGain);
            this.groupBox1.Controls.Add(this.relFreq);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(91, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // buttonPitch
            // 
            this.buttonPitch.Location = new System.Drawing.Point(6, 20);
            this.buttonPitch.Name = "buttonPitch";
            this.buttonPitch.Size = new System.Drawing.Size(57, 21);
            this.buttonPitch.TabIndex = 6;
            this.buttonPitch.Text = "Pitch +/-";
            this.buttonPitch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPitch.UseVisualStyleBackColor = true;
            this.buttonPitch.Click += new System.EventHandler(this.buttonPitch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Loop Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rel. Gain";
            // 
            // pitchChange
            // 
            this.pitchChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pitchChange.FormattingEnabled = true;
            this.pitchChange.Items.AddRange(new object[] {
            "–1 step",
            "–½ step",
            "+½ step",
            "+1 step"});
            this.pitchChange.Location = new System.Drawing.Point(69, 20);
            this.pitchChange.Name = "pitchChange";
            this.pitchChange.Size = new System.Drawing.Size(63, 21);
            this.pitchChange.TabIndex = 1;
            this.pitchChange.SelectedIndexChanged += new System.EventHandler(this.sampleRateName_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Rel. Freq.";
            // 
            // loopStart
            // 
            this.loopStart.Location = new System.Drawing.Point(69, 88);
            this.loopStart.Maximum = new decimal(new int[] {
            7281,
            0,
            0,
            0});
            this.loopStart.Name = "loopStart";
            this.loopStart.Size = new System.Drawing.Size(63, 21);
            this.loopStart.TabIndex = 5;
            this.loopStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.loopStart.ValueChanged += new System.EventHandler(this.loopStart_ValueChanged);
            // 
            // relGain
            // 
            this.relGain.Location = new System.Drawing.Point(69, 66);
            this.relGain.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.relGain.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.relGain.Name = "relGain";
            this.relGain.Size = new System.Drawing.Size(63, 21);
            this.relGain.TabIndex = 3;
            this.relGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.relGain.ValueChanged += new System.EventHandler(this.relGain_ValueChanged);
            // 
            // relFreq
            // 
            this.relFreq.Location = new System.Drawing.Point(69, 44);
            this.relFreq.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.relFreq.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.relFreq.Name = "relFreq";
            this.relFreq.Size = new System.Drawing.Size(63, 21);
            this.relFreq.TabIndex = 1;
            this.relFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.relFreq.ValueChanged += new System.EventHandler(this.relFreq_ValueChanged);
            // 
            // sampleRateName
            // 
            this.sampleRateName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sampleRateName.Enabled = false;
            this.sampleRateName.FormattingEnabled = true;
            this.sampleRateName.Items.AddRange(new object[] {
            "1000 Hz",
            "2000 Hz",
            "4000 Hz",
            "8000 Hz",
            "16000 Hz",
            "32000 Hz",
            "64000 Hz",
            "128000 Hz"});
            this.sampleRateName.Location = new System.Drawing.Point(6, 41);
            this.sampleRateName.Name = "sampleRateName";
            this.sampleRateName.Size = new System.Drawing.Size(79, 21);
            this.sampleRateName.TabIndex = 1;
            this.sampleRateName.SelectedIndexChanged += new System.EventHandler(this.sampleRateName_SelectedIndexChanged);
            // 
            // rateManualValue
            // 
            this.rateManualValue.Location = new System.Drawing.Point(6, 88);
            this.rateManualValue.Maximum = new decimal(new int[] {
            128000,
            0,
            0,
            0});
            this.rateManualValue.Name = "rateManualValue";
            this.rateManualValue.Size = new System.Drawing.Size(79, 21);
            this.rateManualValue.TabIndex = 3;
            this.rateManualValue.Value = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.rateManualValue.ValueChanged += new System.EventHandler(this.rateManualValue_ValueChanged);
            // 
            // rateFixed
            // 
            this.rateFixed.AutoSize = true;
            this.rateFixed.Location = new System.Drawing.Point(6, 20);
            this.rateFixed.Name = "rateFixed";
            this.rateFixed.Size = new System.Drawing.Size(79, 17);
            this.rateFixed.TabIndex = 0;
            this.rateFixed.Text = "Fixed Rate:";
            this.rateFixed.UseVisualStyleBackColor = true;
            this.rateFixed.CheckedChanged += new System.EventHandler(this.sampleRate_CheckedChanged);
            // 
            // rateManual
            // 
            this.rateManual.AutoSize = true;
            this.rateManual.Checked = true;
            this.rateManual.Location = new System.Drawing.Point(6, 68);
            this.rateManual.Name = "rateManual";
            this.rateManual.Size = new System.Drawing.Size(54, 17);
            this.rateManual.TabIndex = 2;
            this.rateManual.TabStop = true;
            this.rateManual.Text = "Other:";
            this.rateManual.UseVisualStyleBackColor = true;
            this.rateManual.CheckedChanged += new System.EventHandler(this.sampleRate_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(229, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 115);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(504, 111);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rateFixed);
            this.groupBox2.Controls.Add(this.rateManual);
            this.groupBox2.Controls.Add(this.sampleRateName);
            this.groupBox2.Controls.Add(this.rateManualValue);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 115);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rate";
            this.groupBox2.Visible = false;
            // 
            // SampleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 140);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SampleEditor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateManualValue)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripNumericUpDown sampleNum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton play;
        private System.Windows.Forms.ToolStripButton stop;
        private System.Windows.Forms.ToolStripButton back;
        private System.Windows.Forms.ToolStripButton foward;
        private System.Windows.Forms.ToolStripButton clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton rateManual;
        private System.Windows.Forms.NumericUpDown rateManualValue;
        private System.Windows.Forms.ToolStripComboBox sampleName;
        private System.Windows.Forms.ComboBox sampleRateName;
        private System.Windows.Forms.RadioButton rateFixed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripCheckBox infiniteLoop;
        private System.Windows.Forms.ToolStripDropDownButton import;
        private System.Windows.Forms.ToolStripMenuItem importWAV;
        private System.Windows.Forms.ToolStripMenuItem importBRR;
        private System.Windows.Forms.ToolStripDropDownButton export;
        private System.Windows.Forms.ToolStripMenuItem exportWAV;
        private System.Windows.Forms.ToolStripMenuItem exportBRR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown loopStart;
        private System.Windows.Forms.NumericUpDown relGain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripButton reset;
        private System.Windows.Forms.NumericUpDown relFreq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripButton searchNames;
        private System.Windows.Forms.Button buttonPitch;
        private System.Windows.Forms.ComboBox pitchChange;
    }
}