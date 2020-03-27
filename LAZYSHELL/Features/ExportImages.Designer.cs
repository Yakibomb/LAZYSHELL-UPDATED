
namespace LAZYSHELL
{
    partial class ExportImages
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
            this.oneImageDefault = new System.Windows.Forms.RadioButton();
            this.oneSpriteSheet = new System.Windows.Forms.RadioButton();
            this.oneImageCropped = new System.Windows.Forms.RadioButton();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.Export_Worker = new System.ComponentModel.BackgroundWorker();
            this.maximumWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.oneAnimatedGIF = new System.Windows.Forms.RadioButton();
            this.range = new System.Windows.Forms.RadioButton();
            this.current = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toIndex = new System.Windows.Forms.NumericUpDown();
            this.fromIndex = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.maximumWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // oneImageDefault
            // 
            this.oneImageDefault.AutoSize = true;
            this.oneImageDefault.Checked = true;
            this.oneImageDefault.Location = new System.Drawing.Point(11, 20);
            this.oneImageDefault.Name = "oneImageDefault";
            this.oneImageDefault.Size = new System.Drawing.Size(342, 17);
            this.oneImageDefault.TabIndex = 0;
            this.oneImageDefault.TabStop = true;
            this.oneImageDefault.Text = "One image per mold, default size (all tilemap molds will be 256x256!)";
            this.oneImageDefault.UseVisualStyleBackColor = true;
            // 
            // oneSpriteSheet
            // 
            this.oneSpriteSheet.AutoSize = true;
            this.oneSpriteSheet.Location = new System.Drawing.Point(11, 54);
            this.oneSpriteSheet.Name = "oneSpriteSheet";
            this.oneSpriteSheet.Size = new System.Drawing.Size(322, 17);
            this.oneSpriteSheet.TabIndex = 2;
            this.oneSpriteSheet.Text = "One sprite sheet image per sprite, molds cropped to pixel edges";
            this.oneSpriteSheet.UseVisualStyleBackColor = true;
            this.oneSpriteSheet.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // oneImageCropped
            // 
            this.oneImageCropped.AutoSize = true;
            this.oneImageCropped.Location = new System.Drawing.Point(11, 37);
            this.oneImageCropped.Name = "oneImageCropped";
            this.oneImageCropped.Size = new System.Drawing.Size(232, 17);
            this.oneImageCropped.TabIndex = 1;
            this.oneImageCropped.Text = "One image per mold, cropped to pixel edges";
            this.oneImageCropped.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(228, 219);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 7;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(309, 219);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 8;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // Export_Worker
            // 
            this.Export_Worker.WorkerReportsProgress = true;
            this.Export_Worker.WorkerSupportsCancellation = true;
            // 
            // maximumWidth
            // 
            this.maximumWidth.Enabled = false;
            this.maximumWidth.Location = new System.Drawing.Point(174, 82);
            this.maximumWidth.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.maximumWidth.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.maximumWidth.Name = "maximumWidth";
            this.maximumWidth.Size = new System.Drawing.Size(62, 21);
            this.maximumWidth.TabIndex = 4;
            this.maximumWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumWidth.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Maximum sprite sheet width";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.oneAnimatedGIF);
            this.groupBox1.Controls.Add(this.oneImageDefault);
            this.groupBox1.Controls.Add(this.maximumWidth);
            this.groupBox1.Controls.Add(this.oneSpriteSheet);
            this.groupBox1.Controls.Add(this.oneImageCropped);
            this.groupBox1.Location = new System.Drawing.Point(12, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 128);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "How would you like to export all images?";
            // 
            // oneAnimatedGIF
            // 
            this.oneAnimatedGIF.AutoSize = true;
            this.oneAnimatedGIF.Location = new System.Drawing.Point(11, 104);
            this.oneAnimatedGIF.Name = "oneAnimatedGIF";
            this.oneAnimatedGIF.Size = new System.Drawing.Size(292, 17);
            this.oneAnimatedGIF.TabIndex = 0;
            this.oneAnimatedGIF.Text = "One animated GIF per sequence, cropped to pixel edges";
            this.oneAnimatedGIF.UseVisualStyleBackColor = true;
            // 
            // range
            // 
            this.range.AutoSize = true;
            this.range.Location = new System.Drawing.Point(12, 29);
            this.range.Name = "range";
            this.range.Size = new System.Drawing.Size(269, 17);
            this.range.TabIndex = 1;
            this.range.TabStop = true;
            this.range.Text = "Export sprite mold images within sprite index range";
            this.range.UseVisualStyleBackColor = true;
            this.range.CheckedChanged += new System.EventHandler(this.range_CheckedChanged);
            // 
            // current
            // 
            this.current.AutoSize = true;
            this.current.Checked = true;
            this.current.Location = new System.Drawing.Point(12, 12);
            this.current.Name = "current";
            this.current.Size = new System.Drawing.Size(186, 17);
            this.current.TabIndex = 0;
            this.current.TabStop = true;
            this.current.Text = "Export current sprite mold images";
            this.current.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "From";
            // 
            // toIndex
            // 
            this.toIndex.Enabled = false;
            this.toIndex.Location = new System.Drawing.Point(155, 52);
            this.toIndex.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.toIndex.Name = "toIndex";
            this.toIndex.Size = new System.Drawing.Size(80, 21);
            this.toIndex.TabIndex = 5;
            this.toIndex.Value = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            // 
            // fromIndex
            // 
            this.fromIndex.Enabled = false;
            this.fromIndex.Location = new System.Drawing.Point(48, 52);
            this.fromIndex.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.fromIndex.Name = "fromIndex";
            this.fromIndex.Size = new System.Drawing.Size(79, 21);
            this.fromIndex.TabIndex = 3;
            // 
            // ExportImages
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(396, 254);
            this.Controls.Add(this.range);
            this.Controls.Add(this.current);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.toIndex);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.fromIndex);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportImages";
            this.Text = "EXPORT MOLD IMAGES - Lazy Shell";
            ((System.ComponentModel.ISupportInitialize)(this.maximumWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.RadioButton oneImageDefault;
        private System.Windows.Forms.RadioButton oneSpriteSheet;
        private System.Windows.Forms.RadioButton oneImageCropped;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.ComponentModel.BackgroundWorker Export_Worker;
        private System.Windows.Forms.NumericUpDown maximumWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton range;
        private System.Windows.Forms.RadioButton current;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown toIndex;
        private System.Windows.Forms.NumericUpDown fromIndex;
        private System.Windows.Forms.RadioButton oneAnimatedGIF;
    }
}