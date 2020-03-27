using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    partial class SearchSolidTile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchSolidTile));
            this.specialTile = new System.Windows.Forms.ComboBox();
            this.stairs = new System.Windows.Forms.ComboBox();
            this.conveyor = new System.Windows.Forms.ComboBox();
            this.withProperties = new System.Windows.Forms.RichTextBox();
            this.unknownBits = new System.Windows.Forms.CheckedListBox();
            this.doorFormat = new System.Windows.Forms.ComboBox();
            this.searchResults = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkStairs = new System.Windows.Forms.CheckBox();
            this.checkSpecialTile = new System.Windows.Forms.CheckBox();
            this.checkDoorFormat = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.conveyorBeltNormal = new System.Windows.Forms.ComboBox();
            this.conveyorBeltFast = new System.Windows.Forms.ComboBox();
            this.checkConveyor = new System.Windows.Forms.CheckBox();
            this.checkConveyorBeltFast = new System.Windows.Forms.CheckBox();
            this.checkConveyorBeltNormal = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.p3OnTile = new System.Windows.Forms.ComboBox();
            this.p3OverEdge = new System.Windows.Forms.ComboBox();
            this.p3OnEdge = new System.Windows.Forms.ComboBox();
            this.checkP3OnEdge = new System.Windows.Forms.CheckBox();
            this.checkP3OverEdge = new System.Windows.Forms.CheckBox();
            this.checkP3OnTile = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.solidEdgeSE = new System.Windows.Forms.ComboBox();
            this.solidEdgeSW = new System.Windows.Forms.ComboBox();
            this.solidEdgeNE = new System.Windows.Forms.ComboBox();
            this.solidEdgeNW = new System.Windows.Forms.ComboBox();
            this.checkSolidEdgeNW = new System.Windows.Forms.CheckBox();
            this.checkSolidEdgeSW = new System.Windows.Forms.CheckBox();
            this.checkSolidEdgeSE = new System.Windows.Forms.CheckBox();
            this.checkSolidEdgeNE = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.solidQuadrantS = new System.Windows.Forms.ComboBox();
            this.solidQuadrant = new System.Windows.Forms.ComboBox();
            this.solidTile = new System.Windows.Forms.ComboBox();
            this.solidQuadrantE = new System.Windows.Forms.ComboBox();
            this.checkSolidTile = new System.Windows.Forms.CheckBox();
            this.checkSolidQuadrant = new System.Windows.Forms.CheckBox();
            this.solidQuadrantW = new System.Windows.Forms.ComboBox();
            this.checkSolidQuadrantN = new System.Windows.Forms.CheckBox();
            this.checkSolidQuadrantE = new System.Windows.Forms.CheckBox();
            this.solidQuadrantN = new System.Windows.Forms.ComboBox();
            this.checkSolidQuadrantS = new System.Windows.Forms.CheckBox();
            this.checkSolidQuadrantW = new System.Windows.Forms.CheckBox();
            this.deselectAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.zCoordPlusHalf = new System.Windows.Forms.ComboBox();
            this.heightOfBaseTile = new System.Windows.Forms.NumericUpDown();
            this.zCoordOverhead = new System.Windows.Forms.NumericUpDown();
            this.checkHeightOfBaseTile = new System.Windows.Forms.CheckBox();
            this.heightOverhead = new System.Windows.Forms.NumericUpDown();
            this.checkZCoordPlusHalf = new System.Windows.Forms.CheckBox();
            this.zCoordWater = new System.Windows.Forms.NumericUpDown();
            this.checkZCoordWater = new System.Windows.Forms.CheckBox();
            this.checkHeightOverhead = new System.Windows.Forms.CheckBox();
            this.checkZCoordOverhead = new System.Windows.Forms.CheckBox();
            this.selectAll = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightOfBaseTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCoordOverhead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightOverhead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCoordWater)).BeginInit();
            this.SuspendLayout();
            // 
            // specialTile
            // 
            this.specialTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.specialTile.Enabled = false;
            this.specialTile.Items.AddRange(new object[] {
            "{none}",
            "vines",
            "water"});
            this.specialTile.Location = new System.Drawing.Point(152, 41);
            this.specialTile.Name = "specialTile";
            this.specialTile.Size = new System.Drawing.Size(52, 21);
            this.specialTile.TabIndex = 4;
            // 
            // stairs
            // 
            this.stairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stairs.Enabled = false;
            this.stairs.Items.AddRange(new object[] {
            "{none}",
            "NW,SE",
            "NE,SW"});
            this.stairs.Location = new System.Drawing.Point(152, 20);
            this.stairs.Name = "stairs";
            this.stairs.Size = new System.Drawing.Size(52, 21);
            this.stairs.TabIndex = 3;
            // 
            // conveyor
            // 
            this.conveyor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conveyor.Enabled = false;
            this.conveyor.Items.AddRange(new object[] {
            "E",
            "SE",
            "S",
            "SW",
            "W",
            "NW",
            "N",
            "NE"});
            this.conveyor.Location = new System.Drawing.Point(152, 20);
            this.conveyor.Name = "conveyor";
            this.conveyor.Size = new System.Drawing.Size(52, 21);
            this.conveyor.TabIndex = 3;
            // 
            // withProperties
            // 
            this.withProperties.BackColor = System.Drawing.SystemColors.Window;
            this.withProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.withProperties.Location = new System.Drawing.Point(214, 23);
            this.withProperties.Name = "withProperties";
            this.withProperties.ReadOnly = true;
            this.withProperties.Size = new System.Drawing.Size(229, 268);
            this.withProperties.TabIndex = 2;
            this.withProperties.Text = "";
            // 
            // unknownBits
            // 
            this.unknownBits.CheckOnClick = true;
            this.unknownBits.ColumnWidth = 64;
            this.unknownBits.Items.AddRange(new object[] {
            "{B5,b0}",
            "{B5,b1}",
            "{B5,b2}",
            "{B5,b3}",
            "{B5,b4}"});
            this.unknownBits.Location = new System.Drawing.Point(6, 89);
            this.unknownBits.MultiColumn = true;
            this.unknownBits.Name = "unknownBits";
            this.unknownBits.Size = new System.Drawing.Size(198, 36);
            this.unknownBits.TabIndex = 6;
            // 
            // doorFormat
            // 
            this.doorFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doorFormat.Enabled = false;
            this.doorFormat.Items.AddRange(new object[] {
            "{none}",
            "{unknown}",
            "{unknown}",
            "{unknown}",
            "{unknown}",
            "NE,SW",
            "{unknown}",
            "NW,SE"});
            this.doorFormat.Location = new System.Drawing.Point(152, 62);
            this.doorFormat.Name = "doorFormat";
            this.doorFormat.Size = new System.Drawing.Size(52, 21);
            this.doorFormat.TabIndex = 5;
            // 
            // searchResults
            // 
            this.searchResults.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchResults.FormattingEnabled = true;
            this.searchResults.IntegralHeight = false;
            this.searchResults.Location = new System.Drawing.Point(214, 291);
            this.searchResults.Name = "searchResults";
            this.searchResults.Size = new System.Drawing.Size(229, 409);
            this.searchResults.TabIndex = 3;
            this.searchResults.SelectedIndexChanged += new System.EventHandler(this.searchResults_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.deselectAll);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.selectAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 700);
            this.panel1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.doorFormat);
            this.groupBox6.Controls.Add(this.unknownBits);
            this.groupBox6.Controls.Add(this.specialTile);
            this.groupBox6.Controls.Add(this.stairs);
            this.groupBox6.Controls.Add(this.checkStairs);
            this.groupBox6.Controls.Add(this.checkSpecialTile);
            this.groupBox6.Controls.Add(this.checkDoorFormat);
            this.groupBox6.Location = new System.Drawing.Point(0, 565);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(210, 131);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other";
            // 
            // checkStairs
            // 
            this.checkStairs.AutoSize = true;
            this.checkStairs.Location = new System.Drawing.Point(6, 22);
            this.checkStairs.Name = "checkStairs";
            this.checkStairs.Size = new System.Drawing.Size(76, 17);
            this.checkStairs.TabIndex = 0;
            this.checkStairs.Text = "Stairs lead";
            this.checkStairs.UseVisualStyleBackColor = false;
            this.checkStairs.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSpecialTile
            // 
            this.checkSpecialTile.AutoSize = true;
            this.checkSpecialTile.Location = new System.Drawing.Point(6, 42);
            this.checkSpecialTile.Name = "checkSpecialTile";
            this.checkSpecialTile.Size = new System.Drawing.Size(111, 17);
            this.checkSpecialTile.TabIndex = 1;
            this.checkSpecialTile.Text = "Special tile format";
            this.checkSpecialTile.UseVisualStyleBackColor = false;
            this.checkSpecialTile.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkDoorFormat
            // 
            this.checkDoorFormat.AutoSize = true;
            this.checkDoorFormat.Location = new System.Drawing.Point(6, 62);
            this.checkDoorFormat.Name = "checkDoorFormat";
            this.checkDoorFormat.Size = new System.Drawing.Size(84, 17);
            this.checkDoorFormat.TabIndex = 2;
            this.checkDoorFormat.Text = "Door format";
            this.checkDoorFormat.UseVisualStyleBackColor = false;
            this.checkDoorFormat.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.conveyorBeltNormal);
            this.groupBox5.Controls.Add(this.conveyorBeltFast);
            this.groupBox5.Controls.Add(this.conveyor);
            this.groupBox5.Controls.Add(this.checkConveyor);
            this.groupBox5.Controls.Add(this.checkConveyorBeltFast);
            this.groupBox5.Controls.Add(this.checkConveyorBeltNormal);
            this.groupBox5.Location = new System.Drawing.Point(0, 471);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(210, 89);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Conveyor Belt";
            // 
            // conveyorBeltNormal
            // 
            this.conveyorBeltNormal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conveyorBeltNormal.Enabled = false;
            this.conveyorBeltNormal.Items.AddRange(new object[] {
            "false",
            "true"});
            this.conveyorBeltNormal.Location = new System.Drawing.Point(152, 62);
            this.conveyorBeltNormal.Name = "conveyorBeltNormal";
            this.conveyorBeltNormal.Size = new System.Drawing.Size(52, 21);
            this.conveyorBeltNormal.TabIndex = 5;
            // 
            // conveyorBeltFast
            // 
            this.conveyorBeltFast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conveyorBeltFast.Enabled = false;
            this.conveyorBeltFast.Items.AddRange(new object[] {
            "false",
            "true"});
            this.conveyorBeltFast.Location = new System.Drawing.Point(152, 41);
            this.conveyorBeltFast.Name = "conveyorBeltFast";
            this.conveyorBeltFast.Size = new System.Drawing.Size(52, 21);
            this.conveyorBeltFast.TabIndex = 4;
            // 
            // checkConveyor
            // 
            this.checkConveyor.AutoSize = true;
            this.checkConveyor.Location = new System.Drawing.Point(6, 22);
            this.checkConveyor.Name = "checkConveyor";
            this.checkConveyor.Size = new System.Drawing.Size(118, 17);
            this.checkConveyor.TabIndex = 0;
            this.checkConveyor.Text = "Conveyor belt runs";
            this.checkConveyor.UseVisualStyleBackColor = false;
            this.checkConveyor.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkConveyorBeltFast
            // 
            this.checkConveyorBeltFast.AutoSize = true;
            this.checkConveyorBeltFast.Location = new System.Drawing.Point(6, 42);
            this.checkConveyorBeltFast.Name = "checkConveyorBeltFast";
            this.checkConveyorBeltFast.Size = new System.Drawing.Size(120, 17);
            this.checkConveyorBeltFast.TabIndex = 1;
            this.checkConveyorBeltFast.Text = "Conveyor belt, fast";
            this.checkConveyorBeltFast.UseVisualStyleBackColor = false;
            this.checkConveyorBeltFast.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkConveyorBeltNormal
            // 
            this.checkConveyorBeltNormal.AutoSize = true;
            this.checkConveyorBeltNormal.Location = new System.Drawing.Point(6, 62);
            this.checkConveyorBeltNormal.Name = "checkConveyorBeltNormal";
            this.checkConveyorBeltNormal.Size = new System.Drawing.Size(133, 17);
            this.checkConveyorBeltNormal.TabIndex = 2;
            this.checkConveyorBeltNormal.Text = "Conveyor belt, normal";
            this.checkConveyorBeltNormal.UseVisualStyleBackColor = false;
            this.checkConveyorBeltNormal.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.p3OnTile);
            this.groupBox4.Controls.Add(this.p3OverEdge);
            this.groupBox4.Controls.Add(this.p3OnEdge);
            this.groupBox4.Controls.Add(this.checkP3OnEdge);
            this.groupBox4.Controls.Add(this.checkP3OverEdge);
            this.groupBox4.Controls.Add(this.checkP3OnTile);
            this.groupBox4.Location = new System.Drawing.Point(0, 379);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 88);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Priority";
            // 
            // p3OnTile
            // 
            this.p3OnTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.p3OnTile.Enabled = false;
            this.p3OnTile.Items.AddRange(new object[] {
            "false",
            "true"});
            this.p3OnTile.Location = new System.Drawing.Point(152, 61);
            this.p3OnTile.Name = "p3OnTile";
            this.p3OnTile.Size = new System.Drawing.Size(52, 21);
            this.p3OnTile.TabIndex = 5;
            // 
            // p3OverEdge
            // 
            this.p3OverEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.p3OverEdge.Enabled = false;
            this.p3OverEdge.Items.AddRange(new object[] {
            "false",
            "true"});
            this.p3OverEdge.Location = new System.Drawing.Point(152, 40);
            this.p3OverEdge.Name = "p3OverEdge";
            this.p3OverEdge.Size = new System.Drawing.Size(52, 21);
            this.p3OverEdge.TabIndex = 4;
            // 
            // p3OnEdge
            // 
            this.p3OnEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.p3OnEdge.Enabled = false;
            this.p3OnEdge.Items.AddRange(new object[] {
            "false",
            "true"});
            this.p3OnEdge.Location = new System.Drawing.Point(152, 19);
            this.p3OnEdge.Name = "p3OnEdge";
            this.p3OnEdge.Size = new System.Drawing.Size(52, 21);
            this.p3OnEdge.TabIndex = 3;
            // 
            // checkP3OnEdge
            // 
            this.checkP3OnEdge.AutoSize = true;
            this.checkP3OnEdge.Location = new System.Drawing.Point(6, 22);
            this.checkP3OnEdge.Name = "checkP3OnEdge";
            this.checkP3OnEdge.Size = new System.Drawing.Size(130, 17);
            this.checkP3OnEdge.TabIndex = 0;
            this.checkP3OnEdge.Text = "P3 for object on edge";
            this.checkP3OnEdge.UseVisualStyleBackColor = false;
            this.checkP3OnEdge.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkP3OverEdge
            // 
            this.checkP3OverEdge.AutoSize = true;
            this.checkP3OverEdge.Location = new System.Drawing.Point(6, 42);
            this.checkP3OverEdge.Name = "checkP3OverEdge";
            this.checkP3OverEdge.Size = new System.Drawing.Size(140, 17);
            this.checkP3OverEdge.TabIndex = 1;
            this.checkP3OverEdge.Text = "P3 for object over edge";
            this.checkP3OverEdge.UseVisualStyleBackColor = false;
            this.checkP3OverEdge.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkP3OnTile
            // 
            this.checkP3OnTile.AutoSize = true;
            this.checkP3OnTile.Location = new System.Drawing.Point(6, 62);
            this.checkP3OnTile.Name = "checkP3OnTile";
            this.checkP3OnTile.Size = new System.Drawing.Size(120, 17);
            this.checkP3OnTile.TabIndex = 2;
            this.checkP3OnTile.Text = "P3 for object on tile";
            this.checkP3OnTile.UseVisualStyleBackColor = false;
            this.checkP3OnTile.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.solidEdgeSE);
            this.groupBox3.Controls.Add(this.solidEdgeSW);
            this.groupBox3.Controls.Add(this.solidEdgeNE);
            this.groupBox3.Controls.Add(this.solidEdgeNW);
            this.groupBox3.Controls.Add(this.checkSolidEdgeNW);
            this.groupBox3.Controls.Add(this.checkSolidEdgeSW);
            this.groupBox3.Controls.Add(this.checkSolidEdgeSE);
            this.groupBox3.Controls.Add(this.checkSolidEdgeNE);
            this.groupBox3.Location = new System.Drawing.Point(0, 305);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 68);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Solid Edges";
            // 
            // solidEdgeSE
            // 
            this.solidEdgeSE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidEdgeSE.Enabled = false;
            this.solidEdgeSE.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidEdgeSE.Location = new System.Drawing.Point(153, 41);
            this.solidEdgeSE.Name = "solidEdgeSE";
            this.solidEdgeSE.Size = new System.Drawing.Size(51, 21);
            this.solidEdgeSE.TabIndex = 7;
            // 
            // solidEdgeSW
            // 
            this.solidEdgeSW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidEdgeSW.Enabled = false;
            this.solidEdgeSW.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidEdgeSW.Location = new System.Drawing.Point(53, 41);
            this.solidEdgeSW.Name = "solidEdgeSW";
            this.solidEdgeSW.Size = new System.Drawing.Size(51, 21);
            this.solidEdgeSW.TabIndex = 3;
            // 
            // solidEdgeNE
            // 
            this.solidEdgeNE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidEdgeNE.Enabled = false;
            this.solidEdgeNE.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidEdgeNE.Location = new System.Drawing.Point(153, 20);
            this.solidEdgeNE.Name = "solidEdgeNE";
            this.solidEdgeNE.Size = new System.Drawing.Size(51, 21);
            this.solidEdgeNE.TabIndex = 5;
            // 
            // solidEdgeNW
            // 
            this.solidEdgeNW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidEdgeNW.Enabled = false;
            this.solidEdgeNW.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidEdgeNW.Location = new System.Drawing.Point(53, 20);
            this.solidEdgeNW.Name = "solidEdgeNW";
            this.solidEdgeNW.Size = new System.Drawing.Size(51, 21);
            this.solidEdgeNW.TabIndex = 1;
            // 
            // checkSolidEdgeNW
            // 
            this.checkSolidEdgeNW.AutoSize = true;
            this.checkSolidEdgeNW.Location = new System.Drawing.Point(6, 22);
            this.checkSolidEdgeNW.Name = "checkSolidEdgeNW";
            this.checkSolidEdgeNW.Size = new System.Drawing.Size(43, 17);
            this.checkSolidEdgeNW.TabIndex = 0;
            this.checkSolidEdgeNW.Text = "NW";
            this.checkSolidEdgeNW.UseVisualStyleBackColor = false;
            this.checkSolidEdgeNW.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidEdgeSW
            // 
            this.checkSolidEdgeSW.AutoSize = true;
            this.checkSolidEdgeSW.Location = new System.Drawing.Point(6, 43);
            this.checkSolidEdgeSW.Name = "checkSolidEdgeSW";
            this.checkSolidEdgeSW.Size = new System.Drawing.Size(42, 17);
            this.checkSolidEdgeSW.TabIndex = 2;
            this.checkSolidEdgeSW.Text = "SW";
            this.checkSolidEdgeSW.UseVisualStyleBackColor = false;
            this.checkSolidEdgeSW.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidEdgeSE
            // 
            this.checkSolidEdgeSE.AutoSize = true;
            this.checkSolidEdgeSE.Location = new System.Drawing.Point(110, 43);
            this.checkSolidEdgeSE.Name = "checkSolidEdgeSE";
            this.checkSolidEdgeSE.Size = new System.Drawing.Size(38, 17);
            this.checkSolidEdgeSE.TabIndex = 6;
            this.checkSolidEdgeSE.Text = "SE";
            this.checkSolidEdgeSE.UseVisualStyleBackColor = false;
            this.checkSolidEdgeSE.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidEdgeNE
            // 
            this.checkSolidEdgeNE.AutoSize = true;
            this.checkSolidEdgeNE.Location = new System.Drawing.Point(110, 22);
            this.checkSolidEdgeNE.Name = "checkSolidEdgeNE";
            this.checkSolidEdgeNE.Size = new System.Drawing.Size(39, 17);
            this.checkSolidEdgeNE.TabIndex = 4;
            this.checkSolidEdgeNE.Text = "NE";
            this.checkSolidEdgeNE.UseVisualStyleBackColor = false;
            this.checkSolidEdgeNE.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.solidQuadrantS);
            this.groupBox2.Controls.Add(this.solidQuadrant);
            this.groupBox2.Controls.Add(this.solidTile);
            this.groupBox2.Controls.Add(this.solidQuadrantE);
            this.groupBox2.Controls.Add(this.checkSolidTile);
            this.groupBox2.Controls.Add(this.checkSolidQuadrant);
            this.groupBox2.Controls.Add(this.solidQuadrantW);
            this.groupBox2.Controls.Add(this.checkSolidQuadrantN);
            this.groupBox2.Controls.Add(this.checkSolidQuadrantE);
            this.groupBox2.Controls.Add(this.solidQuadrantN);
            this.groupBox2.Controls.Add(this.checkSolidQuadrantS);
            this.groupBox2.Controls.Add(this.checkSolidQuadrantW);
            this.groupBox2.Location = new System.Drawing.Point(0, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 137);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solid Quadrants";
            // 
            // solidQuadrantS
            // 
            this.solidQuadrantS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidQuadrantS.Enabled = false;
            this.solidQuadrantS.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidQuadrantS.Location = new System.Drawing.Point(105, 64);
            this.solidQuadrantS.Name = "solidQuadrantS";
            this.solidQuadrantS.Size = new System.Drawing.Size(51, 21);
            this.solidQuadrantS.TabIndex = 7;
            // 
            // solidQuadrant
            // 
            this.solidQuadrant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidQuadrant.Enabled = false;
            this.solidQuadrant.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidQuadrant.Location = new System.Drawing.Point(153, 110);
            this.solidQuadrant.Name = "solidQuadrant";
            this.solidQuadrant.Size = new System.Drawing.Size(51, 21);
            this.solidQuadrant.TabIndex = 11;
            // 
            // solidTile
            // 
            this.solidTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidTile.Enabled = false;
            this.solidTile.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidTile.Location = new System.Drawing.Point(153, 89);
            this.solidTile.Name = "solidTile";
            this.solidTile.Size = new System.Drawing.Size(51, 21);
            this.solidTile.TabIndex = 10;
            // 
            // solidQuadrantE
            // 
            this.solidQuadrantE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidQuadrantE.Enabled = false;
            this.solidQuadrantE.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidQuadrantE.Location = new System.Drawing.Point(153, 42);
            this.solidQuadrantE.Name = "solidQuadrantE";
            this.solidQuadrantE.Size = new System.Drawing.Size(51, 21);
            this.solidQuadrantE.TabIndex = 5;
            // 
            // checkSolidTile
            // 
            this.checkSolidTile.AutoSize = true;
            this.checkSolidTile.Location = new System.Drawing.Point(5, 91);
            this.checkSolidTile.Name = "checkSolidTile";
            this.checkSolidTile.Size = new System.Drawing.Size(65, 17);
            this.checkSolidTile.TabIndex = 8;
            this.checkSolidTile.Text = "Solid tile";
            this.checkSolidTile.UseVisualStyleBackColor = false;
            this.checkSolidTile.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidQuadrant
            // 
            this.checkSolidQuadrant.AutoSize = true;
            this.checkSolidQuadrant.Location = new System.Drawing.Point(5, 111);
            this.checkSolidQuadrant.Name = "checkSolidQuadrant";
            this.checkSolidQuadrant.Size = new System.Drawing.Size(116, 17);
            this.checkSolidQuadrant.TabIndex = 9;
            this.checkSolidQuadrant.Text = "Solid quadrant flag";
            this.checkSolidQuadrant.UseVisualStyleBackColor = false;
            this.checkSolidQuadrant.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // solidQuadrantW
            // 
            this.solidQuadrantW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidQuadrantW.Enabled = false;
            this.solidQuadrantW.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidQuadrantW.Location = new System.Drawing.Point(53, 42);
            this.solidQuadrantW.Name = "solidQuadrantW";
            this.solidQuadrantW.Size = new System.Drawing.Size(51, 21);
            this.solidQuadrantW.TabIndex = 3;
            // 
            // checkSolidQuadrantN
            // 
            this.checkSolidQuadrantN.AutoSize = true;
            this.checkSolidQuadrantN.Location = new System.Drawing.Point(66, 22);
            this.checkSolidQuadrantN.Name = "checkSolidQuadrantN";
            this.checkSolidQuadrantN.Size = new System.Drawing.Size(33, 17);
            this.checkSolidQuadrantN.TabIndex = 0;
            this.checkSolidQuadrantN.Text = "N";
            this.checkSolidQuadrantN.UseVisualStyleBackColor = false;
            this.checkSolidQuadrantN.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidQuadrantE
            // 
            this.checkSolidQuadrantE.AutoSize = true;
            this.checkSolidQuadrantE.Location = new System.Drawing.Point(112, 44);
            this.checkSolidQuadrantE.Name = "checkSolidQuadrantE";
            this.checkSolidQuadrantE.Size = new System.Drawing.Size(32, 17);
            this.checkSolidQuadrantE.TabIndex = 4;
            this.checkSolidQuadrantE.Text = "E";
            this.checkSolidQuadrantE.UseVisualStyleBackColor = false;
            this.checkSolidQuadrantE.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // solidQuadrantN
            // 
            this.solidQuadrantN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidQuadrantN.Enabled = false;
            this.solidQuadrantN.Items.AddRange(new object[] {
            "false",
            "true"});
            this.solidQuadrantN.Location = new System.Drawing.Point(105, 20);
            this.solidQuadrantN.Name = "solidQuadrantN";
            this.solidQuadrantN.Size = new System.Drawing.Size(51, 21);
            this.solidQuadrantN.TabIndex = 1;
            // 
            // checkSolidQuadrantS
            // 
            this.checkSolidQuadrantS.AutoSize = true;
            this.checkSolidQuadrantS.Location = new System.Drawing.Point(66, 66);
            this.checkSolidQuadrantS.Name = "checkSolidQuadrantS";
            this.checkSolidQuadrantS.Size = new System.Drawing.Size(32, 17);
            this.checkSolidQuadrantS.TabIndex = 6;
            this.checkSolidQuadrantS.Text = "S";
            this.checkSolidQuadrantS.UseVisualStyleBackColor = false;
            this.checkSolidQuadrantS.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkSolidQuadrantW
            // 
            this.checkSolidQuadrantW.AutoSize = true;
            this.checkSolidQuadrantW.Location = new System.Drawing.Point(14, 44);
            this.checkSolidQuadrantW.Name = "checkSolidQuadrantW";
            this.checkSolidQuadrantW.Size = new System.Drawing.Size(36, 17);
            this.checkSolidQuadrantW.TabIndex = 2;
            this.checkSolidQuadrantW.Text = "W";
            this.checkSolidQuadrantW.UseVisualStyleBackColor = false;
            this.checkSolidQuadrantW.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // deselectAll
            // 
            this.deselectAll.Location = new System.Drawing.Point(108, 0);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(104, 23);
            this.deselectAll.TabIndex = 1;
            this.deselectAll.Text = "Deselect All";
            this.deselectAll.UseVisualStyleBackColor = true;
            this.deselectAll.Click += new System.EventHandler(this.deselectAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.zCoordPlusHalf);
            this.groupBox1.Controls.Add(this.heightOfBaseTile);
            this.groupBox1.Controls.Add(this.zCoordOverhead);
            this.groupBox1.Controls.Add(this.checkHeightOfBaseTile);
            this.groupBox1.Controls.Add(this.heightOverhead);
            this.groupBox1.Controls.Add(this.checkZCoordPlusHalf);
            this.groupBox1.Controls.Add(this.zCoordWater);
            this.groupBox1.Controls.Add(this.checkZCoordWater);
            this.groupBox1.Controls.Add(this.checkHeightOverhead);
            this.groupBox1.Controls.Add(this.checkZCoordOverhead);
            this.groupBox1.Location = new System.Drawing.Point(0, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile height / coordinates";
            // 
            // zCoordPlusHalf
            // 
            this.zCoordPlusHalf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zCoordPlusHalf.Enabled = false;
            this.zCoordPlusHalf.Items.AddRange(new object[] {
            "false",
            "true"});
            this.zCoordPlusHalf.Location = new System.Drawing.Point(153, 102);
            this.zCoordPlusHalf.Name = "zCoordPlusHalf";
            this.zCoordPlusHalf.Size = new System.Drawing.Size(51, 21);
            this.zCoordPlusHalf.TabIndex = 9;
            // 
            // heightOfBaseTile
            // 
            this.heightOfBaseTile.Enabled = false;
            this.heightOfBaseTile.Location = new System.Drawing.Point(153, 18);
            this.heightOfBaseTile.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.heightOfBaseTile.Name = "heightOfBaseTile";
            this.heightOfBaseTile.Size = new System.Drawing.Size(51, 21);
            this.heightOfBaseTile.TabIndex = 5;
            this.heightOfBaseTile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // zCoordOverhead
            // 
            this.zCoordOverhead.Enabled = false;
            this.zCoordOverhead.Location = new System.Drawing.Point(153, 60);
            this.zCoordOverhead.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.zCoordOverhead.Name = "zCoordOverhead";
            this.zCoordOverhead.Size = new System.Drawing.Size(51, 21);
            this.zCoordOverhead.TabIndex = 7;
            this.zCoordOverhead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkHeightOfBaseTile
            // 
            this.checkHeightOfBaseTile.AutoSize = true;
            this.checkHeightOfBaseTile.Location = new System.Drawing.Point(6, 21);
            this.checkHeightOfBaseTile.Name = "checkHeightOfBaseTile";
            this.checkHeightOfBaseTile.Size = new System.Drawing.Size(113, 17);
            this.checkHeightOfBaseTile.TabIndex = 0;
            this.checkHeightOfBaseTile.Text = "Height of base tile";
            this.checkHeightOfBaseTile.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // heightOverhead
            // 
            this.heightOverhead.Enabled = false;
            this.heightOverhead.Location = new System.Drawing.Point(153, 39);
            this.heightOverhead.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.heightOverhead.Name = "heightOverhead";
            this.heightOverhead.Size = new System.Drawing.Size(51, 21);
            this.heightOverhead.TabIndex = 6;
            this.heightOverhead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkZCoordPlusHalf
            // 
            this.checkZCoordPlusHalf.AutoSize = true;
            this.checkZCoordPlusHalf.Location = new System.Drawing.Point(6, 101);
            this.checkZCoordPlusHalf.Name = "checkZCoordPlusHalf";
            this.checkZCoordPlusHalf.Size = new System.Drawing.Size(103, 17);
            this.checkZCoordPlusHalf.TabIndex = 4;
            this.checkZCoordPlusHalf.Text = "Z coord plus 1/2";
            this.checkZCoordPlusHalf.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // zCoordWater
            // 
            this.zCoordWater.Enabled = false;
            this.zCoordWater.Location = new System.Drawing.Point(153, 81);
            this.zCoordWater.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.zCoordWater.Name = "zCoordWater";
            this.zCoordWater.Size = new System.Drawing.Size(51, 21);
            this.zCoordWater.TabIndex = 8;
            this.zCoordWater.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkZCoordWater
            // 
            this.checkZCoordWater.AutoSize = true;
            this.checkZCoordWater.Location = new System.Drawing.Point(6, 81);
            this.checkZCoordWater.Name = "checkZCoordWater";
            this.checkZCoordWater.Size = new System.Drawing.Size(123, 17);
            this.checkZCoordWater.TabIndex = 3;
            this.checkZCoordWater.Text = "Z coord of water tile";
            this.checkZCoordWater.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkHeightOverhead
            // 
            this.checkHeightOverhead.AutoSize = true;
            this.checkHeightOverhead.Location = new System.Drawing.Point(6, 41);
            this.checkHeightOverhead.Name = "checkHeightOverhead";
            this.checkHeightOverhead.Size = new System.Drawing.Size(136, 17);
            this.checkHeightOverhead.TabIndex = 1;
            this.checkHeightOverhead.Text = "Height of overhead tile";
            this.checkHeightOverhead.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // checkZCoordOverhead
            // 
            this.checkZCoordOverhead.AutoSize = true;
            this.checkZCoordOverhead.Location = new System.Drawing.Point(6, 61);
            this.checkZCoordOverhead.Name = "checkZCoordOverhead";
            this.checkZCoordOverhead.Size = new System.Drawing.Size(141, 17);
            this.checkZCoordOverhead.TabIndex = 2;
            this.checkZCoordOverhead.Text = "Z coord of overhead tile";
            this.checkZCoordOverhead.CheckedChanged += new System.EventHandler(this.checkControl_CheckedChanged);
            // 
            // selectAll
            // 
            this.selectAll.Location = new System.Drawing.Point(3, 0);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(104, 23);
            this.selectAll.TabIndex = 0;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.Control;
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchButton.Location = new System.Drawing.Point(214, 0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(229, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search for tiles w/checked properties";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // SearchSolidTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 700);
            this.Controls.Add(this.searchResults);
            this.Controls.Add(this.withProperties);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LAZYSHELL.Properties.Resources.LAZYSHELL_icon;
            this.MaximizeBox = false;
            this.Name = "SearchSolidTile";
            this.Text = "SOLID TILE SEARCH - Lazy Shell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchPhysicalTile_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightOfBaseTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCoordOverhead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightOverhead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCoordWater)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private CheckBox checkConveyor;
        private CheckBox checkConveyorBeltFast;
        private CheckBox checkConveyorBeltNormal;
        private CheckBox checkDoorFormat;
        private CheckBox checkHeightOfBaseTile;
        private CheckBox checkHeightOverhead;
        private CheckBox checkP3OnEdge;
        private CheckBox checkP3OnTile;
        private CheckBox checkP3OverEdge;
        private CheckBox checkSolidEdgeNE;
        private CheckBox checkSolidEdgeNW;
        private CheckBox checkSolidEdgeSE;
        private CheckBox checkSolidEdgeSW;
        private CheckBox checkSolidQuadrant;
        private CheckBox checkSolidQuadrantE;
        private CheckBox checkSolidQuadrantN;
        private CheckBox checkSolidQuadrantS;
        private CheckBox checkSolidQuadrantW;
        private CheckBox checkSolidTile;
        private CheckBox checkSpecialTile;
        private CheckBox checkStairs;
        private CheckBox checkZCoordOverhead;
        private CheckBox checkZCoordPlusHalf;
        private CheckBox checkZCoordWater;
        private CheckedListBox unknownBits;
        private ComboBox conveyor;
        private ComboBox conveyorBeltFast;
        private ComboBox conveyorBeltNormal;
        private ComboBox doorFormat;
        private ComboBox p3OnEdge;
        private ComboBox p3OnTile;
        private ComboBox p3OverEdge;
        private ComboBox solidEdgeNE;
        private ComboBox solidEdgeNW;
        private ComboBox solidEdgeSE;
        private ComboBox solidEdgeSW;
        private ComboBox solidQuadrant;
        private ComboBox solidQuadrantE;
        private ComboBox solidQuadrantN;
        private ComboBox solidQuadrantS;
        private ComboBox solidQuadrantW;
        private ComboBox solidTile;
        private ComboBox specialTile;
        private ComboBox stairs;
        private ComboBox zCoordPlusHalf;
        private ListBox searchResults;
        private NumericUpDown heightOfBaseTile;
        private NumericUpDown heightOverhead;
        private NumericUpDown zCoordOverhead;
        private NumericUpDown zCoordWater;
        private Panel panel1;
        private RichTextBox withProperties;
        private Button searchButton;
        private Button deselectAll;
        private Button selectAll;
        private GroupBox groupBox1;
        private GroupBox groupBox6;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
    }
}