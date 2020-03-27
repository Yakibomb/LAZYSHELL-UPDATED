
namespace LAZYSHELL
{
    partial class EffectSequences
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
            this.pictureBoxSequence = new System.Windows.Forms.PictureBox();
            this.panelSequence = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.play = new System.Windows.Forms.ToolStripButton();
            this.pause = new System.Windows.Forms.ToolStripButton();
            this.back = new System.Windows.Forms.ToolStripButton();
            this.foward = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.newFrame = new System.Windows.Forms.ToolStripButton();
            this.deleteFrame = new System.Windows.Forms.ToolStripButton();
            this.duplicate = new System.Windows.Forms.ToolStripButton();
            this.reverseFrames = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveFrameBack = new System.Windows.Forms.ToolStripButton();
            this.moveFrameFoward = new System.Windows.Forms.ToolStripButton();
            this.frames = new LAZYSHELL.NewPanel();
            this.PlaybackSequence = new System.ComponentModel.BackgroundWorker();
            this.listBoxFrames = new System.Windows.Forms.ListBox();
            this.panelFrames = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequence)).BeginInit();
            this.panelSequence.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panelFrames.SuspendLayout();
            this.SuspendLayout();
            // 
            // frameMold
            // 
            this.frameMold.AutoSize = false;
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
            // pictureBoxSequence
            // 
            this.pictureBoxSequence.BackgroundImage = global::LAZYSHELL.Properties.Resources._transparent;
            this.pictureBoxSequence.Location = new System.Drawing.Point(15, 0);
            this.pictureBoxSequence.Name = "pictureBoxSequence";
            this.pictureBoxSequence.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxSequence.TabIndex = 396;
            this.pictureBoxSequence.TabStop = false;
            this.pictureBoxSequence.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSequence_Paint);
            // 
            // panelSequence
            // 
            this.panelSequence.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSequence.Controls.Add(this.pictureBoxSequence);
            this.panelSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSequence.Location = new System.Drawing.Point(78, 50);
            this.panelSequence.Name = "panelSequence";
            this.panelSequence.Size = new System.Drawing.Size(308, 287);
            this.panelSequence.TabIndex = 4;
            this.panelSequence.SizeChanged += new System.EventHandler(this.panelSequence_SizeChanged);
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
            this.toolStripSeparator4,
            this.play,
            this.pause,
            this.back,
            this.foward});
            this.toolStrip1.Location = new System.Drawing.Point(78, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(308, 25);
            this.toolStrip1.TabIndex = 2;
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFrame,
            this.deleteFrame,
            this.duplicate,
            this.reverseFrames,
            this.toolStripSeparator1,
            this.moveFrameBack,
            this.moveFrameFoward});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(386, 25);
            this.toolStrip2.TabIndex = 0;
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
            // deleteFrame
            // 
            this.deleteFrame.Image = global::LAZYSHELL.Properties.Resources.delete_small;
            this.deleteFrame.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteFrame.Name = "deleteFrame";
            this.deleteFrame.Size = new System.Drawing.Size(23, 22);
            this.deleteFrame.ToolTipText = "Delete Frame";
            this.deleteFrame.Click += new System.EventHandler(this.deleteFrame_Click);
            // 
            // duplicate
            // 
            this.duplicate.Image = global::LAZYSHELL.Properties.Resources.duplicate_small;
            this.duplicate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.duplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicate.Name = "duplicate";
            this.duplicate.Size = new System.Drawing.Size(23, 22);
            this.duplicate.ToolTipText = "Duplicate Frame";
            this.duplicate.Click += new System.EventHandler(this.duplicate_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFrameBack
            // 
            this.moveFrameBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFrameBack.Image = global::LAZYSHELL.Properties.Resources.back;
            this.moveFrameBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.moveFrameBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFrameBack.Name = "moveFrameBack";
            this.moveFrameBack.Size = new System.Drawing.Size(23, 22);
            this.moveFrameBack.Text = "Move Frame Back";
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
            this.moveFrameFoward.Text = "Move Frame Forward";
            this.moveFrameFoward.Click += new System.EventHandler(this.moveFrameFoward_Click);
            // 
            // frames
            // 
            this.frames.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.frames.Location = new System.Drawing.Point(0, 0);
            this.frames.Name = "frames";
            this.frames.Size = new System.Drawing.Size(104, 104);
            this.frames.TabIndex = 0;
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
            this.listBoxFrames.Location = new System.Drawing.Point(0, 25);
            this.listBoxFrames.Name = "listBoxFrames";
            this.listBoxFrames.Size = new System.Drawing.Size(78, 312);
            this.listBoxFrames.TabIndex = 1;
            this.listBoxFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxFrames_SelectedIndexChanged);
            // 
            // panelFrames
            // 
            this.panelFrames.AutoScroll = true;
            this.panelFrames.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFrames.Controls.Add(this.frames);
            this.panelFrames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFrames.Location = new System.Drawing.Point(78, 50);
            this.panelFrames.Name = "panelFrames";
            this.panelFrames.Size = new System.Drawing.Size(308, 287);
            this.panelFrames.TabIndex = 3;
            this.panelFrames.SizeChanged += new System.EventHandler(this.panelFrames_SizeChanged);
            // 
            // EffectSequences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 337);
            this.ControlBox = false;
            this.Controls.Add(this.panelFrames);
            this.Controls.Add(this.panelSequence);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listBoxFrames);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EffectSequences";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequence)).EndInit();
            this.panelSequence.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panelFrames.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.PictureBox pictureBoxSequence;
        private System.Windows.Forms.Panel panelSequence;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton play;
        private System.Windows.Forms.ToolStripButton pause;
        private System.Windows.Forms.ToolStripButton back;
        private System.Windows.Forms.ToolStripButton foward;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton newFrame;
        private System.Windows.Forms.ToolStripButton deleteFrame;
        private System.Windows.Forms.ToolStripButton duplicate;
        private LAZYSHELL.NewPanel frames;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.ComponentModel.BackgroundWorker PlaybackSequence;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton moveFrameBack;
        private System.Windows.Forms.ToolStripButton moveFrameFoward;
        private System.Windows.Forms.ListBox listBoxFrames;
        private System.Windows.Forms.Panel panelFrames;
        private ToolStripNumericUpDown frameMold;
        private ToolStripNumericUpDown duration;
        private System.Windows.Forms.ToolStripButton reverseFrames;
    }
}