
namespace LAZYSHELL
{
    partial class Shops
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
            this.shopDiscounts = new System.Windows.Forms.CheckedListBox();
            this.shopBuyOptions = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.shopName = new LAZYSHELL.ToolStripComboBox();
            this.moveUp = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.groupBoxItems = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // shopDiscounts
            // 
            this.shopDiscounts.CheckOnClick = true;
            this.shopDiscounts.Items.AddRange(new object[] {
            "6% discount",
            "12% discount",
            "25% discount",
            "50% discount"});
            this.shopDiscounts.Location = new System.Drawing.Point(6, 20);
            this.shopDiscounts.Name = "shopDiscounts";
            this.shopDiscounts.Size = new System.Drawing.Size(173, 68);
            this.shopDiscounts.TabIndex = 0;
            this.shopDiscounts.SelectedIndexChanged += new System.EventHandler(this.shopDiscounts_SelectedIndexChanged);
            // 
            // shopBuyOptions
            // 
            this.shopBuyOptions.CheckOnClick = true;
            this.shopBuyOptions.Items.AddRange(new object[] {
            "Buy w/Frog Coins, only once",
            "Buy w/Frog Coins",
            "Buy only, no selling",
            "Buy only, no selling"});
            this.shopBuyOptions.Location = new System.Drawing.Point(6, 20);
            this.shopBuyOptions.Name = "shopBuyOptions";
            this.shopBuyOptions.Size = new System.Drawing.Size(173, 68);
            this.shopBuyOptions.TabIndex = 0;
            this.shopBuyOptions.SelectedIndexChanged += new System.EventHandler(this.shopBuyOptions_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shopName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(191, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // shopName
            // 
            this.shopName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.shopName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.shopName.DropDownHeight = 497;
            this.shopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shopName.DropDownWidth = 250;
            this.shopName.ItemHeight = 15;
            this.shopName.Location = new System.Drawing.Point(7, 1);
            this.shopName.Name = "shopName";
            this.shopName.SelectedIndex = -1;
            this.shopName.SelectedItem = null;
            this.shopName.Size = new System.Drawing.Size(180, 22);
            this.shopName.SelectedIndexChanged += new System.EventHandler(this.shopName_SelectedIndexChanged);
            this.shopName.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.shopName_DrawItem);
            // 
            // moveUp
            // 
            this.moveUp.Location = new System.Drawing.Point(3, 375);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(90, 23);
            this.moveUp.TabIndex = 2;
            this.moveUp.Text = "MOVE UP";
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.Location = new System.Drawing.Point(98, 375);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(90, 23);
            this.moveDown.TabIndex = 3;
            this.moveDown.Text = "MOVE DOWN";
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // groupBoxItems
            // 
            this.groupBoxItems.Location = new System.Drawing.Point(3, 28);
            this.groupBoxItems.Name = "groupBoxItems";
            this.groupBoxItems.Size = new System.Drawing.Size(185, 341);
            this.groupBoxItems.TabIndex = 1;
            this.groupBoxItems.TabStop = false;
            this.groupBoxItems.Text = "Shop Items";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.shopBuyOptions);
            this.groupBox2.Location = new System.Drawing.Point(3, 404);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 94);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shop Options";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.shopDiscounts);
            this.groupBox3.Location = new System.Drawing.Point(3, 504);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 94);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Purchase Discounts";
            // 
            // Shops
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 601);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxItems);
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Shops";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.CheckedListBox shopDiscounts;
        private System.Windows.Forms.CheckedListBox shopBuyOptions;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private LAZYSHELL.ToolStripComboBox shopName;
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.GroupBox groupBoxItems;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}