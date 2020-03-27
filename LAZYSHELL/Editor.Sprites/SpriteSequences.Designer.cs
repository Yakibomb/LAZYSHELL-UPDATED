
namespace LAZYSHELL
{
    partial class SpriteSequences
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
            this.frameMold = new LAZYSHELL.ToolStripNumericUpDown();
            this.duration = new LAZYSHELL.ToolStripNumericUpDown();
            this.panelSequence = new System.Windows.Forms.Panel();
            this.pictureBoxSequence = new System.Windows.Forms.PictureBox();
            this.frames = new LAZYSHELL.NewPanel();
            this.sequences = new System.Windows.Forms.ListBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.exportAnimatedGIF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.newSequence = new System.Windows.Forms.ToolStripButton();
            this.deleteSequence = new System.Windows.Forms.ToolStripButton();
            this.duplicateSequence = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveSequenceBack = new System.Windows.Forms.ToolStripButton();
            this.moveSeqeuenceFoward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.sequenceActive = new LAZYSHELL.ToolStripCheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.play = new System.Windows.Forms.ToolStripButton();
            this.pause = new System.Windows.Forms.ToolStripButton();
            this.back = new System.Windows.Forms.ToolStripButton();
            this.foward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.infinitePlayback = new LAZYSHELL.ToolStripCheckBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.newFrame = new System.Windows.Forms.ToolStripButton();
            this.copy = new System.Windows.Forms.ToolStripButton();
            this.paste = new System.Windows.Forms.ToolStripButton();
            this.deleteFrame = new System.Windows.Forms.ToolStripButton();
            this.duplicateFrame = new System.Windows.Forms.ToolStripButton();
            this.reverseFrames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFrameBack = new System.Windows.Forms.ToolStripButton();
            this.moveFrameFoward = new System.Windows.Forms.ToolStripButton();
            this.PlaybackSequence = new System.ComponentModel.BackgroundWorker();
            this.listBoxFrames = new System.Windows.Forms.ListBox();
            this.panelFrames = new System.Windows.Forms.Panel();
            this.panelSequence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequence)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.panelFrames.SuspendLayout();
            this.SuspendLayout();
            // 
            // frameMold
            // 
            this.frameMold.AutoSize = false;
            this.frameMold.ContextMenuStrip = null;
            this.frameMold.Hexadecimal = false;
            this.frameMold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameMold.Location = new System.Drawing.Point(36, 4);
            this.frameMold.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.frameMold.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.frameMold.Name = "frameMold";
            this.frameMold.Size = new System.Drawing.Size(50, 17);
            this.frameMold.Text = "0";
            this.frameMold.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.frameMold.ValueChanged += new System.EventHandler(this.frameMold_ValueChanged);
            // 
            // duration
            // 
            this.duration.AutoSize = false;
            this.duration.ContextMenuStrip = null;
            this.duration.Hexadecimal = false;
            this.duration.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.duration.Location = new System.Drawing.Point(140, 4);
            this.duration.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.duration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.duration.Name = "duration";
            this.duration.Size = new System.Drawing.Size(50, 17);
            this.duration.Text = "1";
            this.duration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.duration.ValueChanged += new System.EventHandler(this.duration_ValueChanged);
            // 
            // panelSequence
            // 
            this.panelSequence.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSequence.Controls.Add(this.pictureBoxSequence);
            this.panelSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSequence.Location = new System.Drawing.Point(156, 75);
            this.panelSequence.Name = "panelSequence";
            this.panelSequence.Size = new System.Drawing.Size(512, 284);
            this.panelSequence.TabIndex = 6;
            this.panelSequence.SizeChanged += new System.EventHandler(this.panelSequence_SizeChanged);
            // 
            // pictureBoxSequence
            // 
            this.pictureBoxSequence.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxSequence.Location = new System.Drawing.Point(21, 0);
            this.pictureBoxSequence.Name = "pictureBoxSequence";
            this.pictureBoxSequence.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxSequence.TabIndex = 396;
            this.pictureBoxSequence.TabStop = false;
            this.pictureBoxSequence.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSequence_Paint);
            // 
            // frames
            // 
            this.frames.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.frames.Location = new System.Drawing.Point(0, 0);
            this.frames.Name = "frames";
            this.frames.Size = new System.Drawing.Size(264, 264);
            this.frames.TabIndex = 0;
            // 
            // sequences
            // 
            this.sequences.Dock = System.Windows.Forms.DockStyle.Left;
            this.sequences.FormattingEnabled = true;
            this.sequences.IntegralHeight = false;
            this.sequences.Location = new System.Drawing.Point(0, 25);
            this.sequences.Name = "sequences";
            this.sequences.Size = new System.Drawing.Size(78, 334);
            this.sequences.TabIndex = 1;
            this.sequences.SelectedIndexChanged += new System.EventHandler(this.sequences_SelectedIndexChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.CanOverflow = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAnimatedGIF,
            this.toolStripSeparator7,
            this.newSequence,
            this.deleteSequence,
            this.duplicateSequence,
            this.toolStripSeparator3,
            this.moveSequenceBack,
            this.moveSeqeuenceFoward,
            this.toolStripSeparator6,
            this.sequenceActive});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(668, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // exportAnimatedGIF
            // 
            this.exportAnimatedGIF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportAnimatedGIF.Image = global::LAZYSHELL.Properties.Resources.exportImage;
            this.exportAnimatedGIF.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exportAnimatedGIF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportAnimatedGIF.Name = "exportAnimatedGIF";
            this.exportAnimatedGIF.Size = new System.Drawing.Size(23, 22);
            this.exportAnimatedGIF.Text = "Export to Animated GIF";
            this.exportAnimatedGIF.Click += new System.EventHandler(this.exportAnimatedGIF_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // newSequence
            // 
            this.newSequence.Image = global::LAZYSHELL.Properties.Resources.new_small;
            this.newSequence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newSequence.Name = "newSequence";
            this.newSequence.Size = new System.Drawing.Size(23, 22);
            this.newSequence.ToolTipText = "New Sequence";
            this.newSequence.Click += new System.EventHandler(this.newSequence_Click);
            // 
            // deleteSequence
            // 
            this.deleteSequence.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteSequence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSequence.Name = "deleteSequence";
            this.deleteSequence.Size = new System.Drawing.Size(23, 22);
            this.deleteSequence.ToolTipText = "Delete Sequence";
            this.deleteSequence.Click += new System.EventHandler(this.deleteSequence_Click);
            // 
            // duplicateSequence
            // 
            this.duplicateSequence.Image = global::LAZYSHELL.Properties.Resources.duplicate_small;
            this.duplicateSequence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.duplicateSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateSequence.Name = "duplicateSequence";
            this.duplicateSequence.Size = new System.Drawing.Size(23, 22);
            this.duplicateSequence.ToolTipText = "Duplicate Sequence";
            this.duplicateSequence.Click += new System.EventHandler(this.duplicateSequence_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // moveSequenceBack
            // 
            this.moveSequenceBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveSequenceBack.Image = global::LAZYSHELL.Properties.Resources.back;
            this.moveSequenceBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.moveSequenceBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveSequenceBack.Name = "moveSequenceBack";
            this.moveSequenceBack.Size = new System.Drawing.Size(23, 22);
            this.moveSequenceBack.Text = "Move Sequence Back";
            this.moveSequenceBack.Click += new System.EventHandler(this.moveSequenceBack_Click);
            // 
            // moveSeqeuenceFoward
            // 
            this.moveSeqeuenceFoward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveSeqeuenceFoward.Image = global::LAZYSHELL.Properties.Resources.foward;
            this.moveSeqeuenceFoward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.moveSeqeuenceFoward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveSeqeuenceFoward.Name = "moveSeqeuenceFoward";
            this.moveSeqeuenceFoward.Size = new System.Drawing.Size(23, 22);
            this.moveSeqeuenceFoward.Text = "Move Sequence Forward";
            this.moveSeqeuenceFoward.Click += new System.EventHandler(this.moveSeqeuenceFoward_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // sequenceActive
            // 
            this.sequenceActive.Checked = false;
            this.sequenceActive.Name = "sequenceActive";
            this.sequenceActive.Padding = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.sequenceActive.Size = new System.Drawing.Size(60, 22);
            this.sequenceActive.Text = "Active";
            this.sequenceActive.CheckedChanged += new System.EventHandler(this.sequenceActive_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.frameMold,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.duration,
            this.toolStripSeparator1,
            this.play,
            this.pause,
            this.back,
            this.foward,
            this.toolStripSeparator5,
            this.infinitePlayback});
            this.toolStrip1.Location = new System.Drawing.Point(156, 50);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(512, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(29, 22);
            this.toolStripLabel2.Text = "Mold";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel1.Text = "Duration";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // pause
            // 
            this.pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pause.Image = global::LAZYSHELL.Properties.Resources.stop;
            this.pause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(23, 22);
            this.pause.Text = "Stop";
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // back
            // 
            this.back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.back.Image = global::LAZYSHELL.Properties.Resources.back;
            this.back.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(23, 22);
            this.back.Text = "Select Previous Frame";
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
            this.foward.Text = "Select Next Frame";
            this.foward.Click += new System.EventHandler(this.foward_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // infinitePlayback
            // 
            this.infinitePlayback.Checked = true;
            this.infinitePlayback.Name = "infinitePlayback";
            this.infinitePlayback.Padding = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.infinitePlayback.Size = new System.Drawing.Size(113, 22);
            this.infinitePlayback.Text = "Infinite Playback";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFrame,
            this.copy,
            this.paste,
            this.deleteFrame,
            this.duplicateFrame,
            this.reverseFrames,
            this.toolStripSeparator4,
            this.moveFrameBack,
            this.moveFrameFoward});
            this.toolStrip3.Location = new System.Drawing.Point(78, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(590, 25);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // newFrame
            // 
            this.newFrame.Image = global::LAZYSHELL.Properties.Resources.new_small;
            this.newFrame.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFrame.Name = "newFrame";
            this.newFrame.Size = new System.Drawing.Size(23, 22);
            this.newFrame.ToolTipText = "New Frame";
            this.newFrame.Click += new System.EventHandler(this.newFrame_Click);
            // 
            // copy
            // 
            this.copy.Image = global::LAZYSHELL.Properties.Resources.copy_small;
            this.copy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(23, 22);
            this.copy.ToolTipText = "Copy Frame(s)";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // paste
            // 
            this.paste.Image = global::LAZYSHELL.Properties.Resources.paste_small;
            this.paste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(23, 22);
            this.paste.ToolTipText = "Paste Frame(s)";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // deleteFrame
            // 
            this.deleteFrame.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteFrame.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteFrame.Name = "deleteFrame";
            this.deleteFrame.Size = new System.Drawing.Size(23, 22);
            this.deleteFrame.ToolTipText = "Delete Frame(s)";
            this.deleteFrame.Click += new System.EventHandler(this.deleteFrame_Click);
            // 
            // duplicateFrame
            // 
            this.duplicateFrame.Image = global::LAZYSHELL.Properties.Resources.duplicate_small;
            this.duplicateFrame.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.duplicateFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateFrame.Name = "duplicateFrame";
            this.duplicateFrame.Size = new System.Drawing.Size(23, 22);
            this.duplicateFrame.ToolTipText = "Duplicate Frame";
            this.duplicateFrame.Click += new System.EventHandler(this.duplicateFrame_Click);
            // 
            // reverseFrames
            // 
            this.reverseFrames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reverseFrames.Image = global::LAZYSHELL.Properties.Resources.widthDecrease;
            this.reverseFrames.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.reverseFrames.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reverseFrames.Name = "reverseFrames";
            this.reverseFrames.Size = new System.Drawing.Size(23, 22);
            this.reverseFrames.ToolTipText = "Reverse Frames";
            this.reverseFrames.Click += new System.EventHandler(this.reverseFrames_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFrameBack
            // 
            this.moveFrameBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFrameBack.Image = global::LAZYSHELL.Properties.Resources.back;
            this.moveFrameBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.moveFrameBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFrameBack.Name = "moveFrameBack";
            this.moveFrameBack.Size = new System.Drawing.Size(23, 22);
            this.moveFrameBack.Text = "Move Frame(s) Back";
            this.moveFrameBack.Click += new System.EventHandler(this.moveFrameBack_Click);
            // 
            // moveFrameFoward
            // 
            this.moveFrameFoward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFrameFoward.Image = global::LAZYSHELL.Properties.Resources.foward;
            this.moveFrameFoward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.moveFrameFoward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFrameFoward.Name = "moveFrameFoward";
            this.moveFrameFoward.Size = new System.Drawing.Size(23, 22);
            this.moveFrameFoward.Text = "Move Frame(s) Forward";
            this.moveFrameFoward.Click += new System.EventHandler(this.moveFrameFoward_Click);
            // 
            // PlaybackSequence
            // 
            this.PlaybackSequence.WorkerReportsProgress = true;
            this.PlaybackSequence.WorkerSupportsCancellation = true;
            this.PlaybackSequence.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PlaybackSequence_DoWork);
            this.PlaybackSequence.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.PlaybackSequence_ProgressChanged);
            this.PlaybackSequence.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PlaybackSequence_RunWorkerCompleted);
            // 
            // listBoxFrames
            // 
            this.listBoxFrames.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxFrames.FormattingEnabled = true;
            this.listBoxFrames.IntegralHeight = false;
            this.listBoxFrames.Location = new System.Drawing.Point(78, 50);
            this.listBoxFrames.Name = "listBoxFrames";
            this.listBoxFrames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFrames.Size = new System.Drawing.Size(78, 309);
            this.listBoxFrames.TabIndex = 3;
            this.listBoxFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxFrames_SelectedIndexChanged);
            // 
            // panelFrames
            // 
            this.panelFrames.AutoScroll = true;
            this.panelFrames.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFrames.Controls.Add(this.frames);
            this.panelFrames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFrames.Location = new System.Drawing.Point(156, 75);
            this.panelFrames.Name = "panelFrames";
            this.panelFrames.Size = new System.Drawing.Size(512, 284);
            this.panelFrames.TabIndex = 5;
            this.panelFrames.SizeChanged += new System.EventHandler(this.panelFrames_SizeChanged);
            // 
            // SpriteSequences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 359);
            this.ControlBox = false;
            this.Controls.Add(this.panelFrames);
            this.Controls.Add(this.panelSequence);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listBoxFrames);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.sequences);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SpriteSequences";
            this.panelSequence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequence)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.panelFrames.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Panel panelSequence;
        private LAZYSHELL.NewPanel frames;
        private System.Windows.Forms.ListBox sequences;
        private System.Windows.Forms.PictureBox pictureBoxSequence;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton deleteSequence;
        private System.Windows.Forms.ToolStripButton duplicateSequence;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton play;
        private System.Windows.Forms.ToolStripButton pause;
        private System.Windows.Forms.ToolStripButton back;
        private System.Windows.Forms.ToolStripButton foward;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton newFrame;
        private System.Windows.Forms.ToolStripButton deleteFrame;
        private System.Windows.Forms.ToolStripButton duplicateFrame;
        private System.ComponentModel.BackgroundWorker PlaybackSequence;
        private System.Windows.Forms.ListBox listBoxFrames;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton moveSequenceBack;
        private System.Windows.Forms.ToolStripButton moveSeqeuenceFoward;
        private System.Windows.Forms.ToolStripButton moveFrameBack;
        private System.Windows.Forms.ToolStripButton moveFrameFoward;
        private System.Windows.Forms.Panel panelFrames;
        private ToolStripNumericUpDown frameMold = new ToolStripNumericUpDown();
        private ToolStripNumericUpDown duration = new ToolStripNumericUpDown();
        private System.Windows.Forms.ToolStripButton reverseFrames;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private LAZYSHELL.ToolStripCheckBox infinitePlayback;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private ToolStripCheckBox sequenceActive;
        private System.Windows.Forms.ToolStripButton newSequence;
        private System.Windows.Forms.ToolStripButton copy;
        private System.Windows.Forms.ToolStripButton paste;
        private System.Windows.Forms.ToolStripButton exportAnimatedGIF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}