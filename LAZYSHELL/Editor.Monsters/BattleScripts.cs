using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;
using LAZYSHELL.Undo;
//

namespace LAZYSHELL
{
    public partial class BattleScripts : NewForm
    {
        #region Variables
        //
        private Monsters monsterEditor;
        public BattleScript[] battleScripts { get { return Model.BattleScripts; } set { Model.BattleScripts = value; } }
        private BattleScript battleScript { get { return battleScripts[index]; } set { battleScripts[index] = value; } }
        public BattleScript BattleScript { get { return battleScript; } set { battleScript = value; } }
        private SortedList spellNames { get { return Model.SpellNames; } set { Model.SpellNames = value; } }
        private SortedList attackNames { get { return Model.AttackNames; } set { Model.AttackNames = value; } }
        private SortedList itemNames { get { return Model.ItemNames; } set { Model.ItemNames = value; } }
        public int index { get { return monsterEditor.Index; } set { monsterEditor.Index = value; } }
        private Bitmap monsterImage;
        private BattleCommand command;
        private List<BattleCommand> commandCopies;
        private CommandStack commandStack;
        private TreeNode modifiedNode;
        //
        private Monster[] monsters { get { return Model.Monsters; } set { Model.Monsters = value; } }
        private Monster monster { get { return monsters[index]; } set { monsters[index] = value; } }
        private bool waitBothCoords = false;
        private bool overTarget = false;
        #endregion
        // Constructor
        public BattleScripts(Monsters monsterEditor)
        {
            this.monsterEditor = monsterEditor;
            this.commandStack = new CommandStack();
            InitializeComponent();
            Initialize();
        }
        #region Functions
        public void Initialize()
        {
            buttonInsert.Enabled = false;
            buttonApply.Enabled = false;
            panelAttack.Visible = false;
            panelTarget.Visible = false;
            panelMemory.Visible = false;
            ResetControls();
            //
            Cursor.Current = Cursors.WaitCursor;
            RefreshScript();
            //
            UpdateBattleScriptsFreeSpace();
            this.monsterTargetArrowX.Value = monster.CursorX;
            this.monsterTargetArrowY.Value = monster.CursorY;
            monsterImage = new Bitmap(monster.Image);
            pictureBoxMonster.Invalidate();
            //
            Cursor.Current = Cursors.Arrow;
        }
        public void RefreshScript()
        {
            RefreshScript(-1);
        }
        public void RefreshScript(int fullIndex)
        {
            List<byte> buffer = new List<byte>();
            foreach (BattleCommand bsc in battleScript.Commands)
                buffer.AddRange(bsc.CommandData);
            battleScript.Script = buffer.ToArray();
            //
            commandTree.BeginUpdate();
            commandTree.Nodes.Clear();
            TreeNode parent = null;
            TreeNode firstCounter = null;
            bool startCounter = false;
            foreach (BattleCommand bsc in battleScript.Commands)
            {
                if (bsc.Opcode == 0xFF)
                    parent = null;
                TreeNode node = bsc.Node;
                if (parent == null)
                    commandTree.Nodes.Add(node);
                else
                    parent.Nodes.Add(node);
                if (bsc.Opcode == 0xFC) // add child nodes
                    parent = node;
                else if (bsc.Opcode == 0xFF && !startCounter)
                {
                    parent = node;
                    firstCounter = node;
                    startCounter = true;
                }
                else if (bsc.Opcode == 0xFE) // end all if hierarchies
                    parent = startCounter ? firstCounter : null;
            }
            commandTree.ExpandAll();
            if (fullIndex >= 0 && fullIndex < commandTree.GetNodeCount(true))
                commandTree.SelectNode(fullIndex);
            commandTree.EndUpdate();
            //
            UpdateBattleScriptsFreeSpace();
            this.monsterTargetArrowX.Value = monster.CursorX;
            this.monsterTargetArrowY.Value = monster.CursorY;
            monsterImage = new Bitmap(monster.Image);
            pictureBoxMonster.Invalidate();
        }
        // Disassembler Commands
        private void ControlDisassemble()
        {
            this.Updating = true;
            buttonInsert.Enabled = true;
            buttonApply.Enabled = true;
            //
            panelRight.SuspendDrawing();
            ResetControls();
            switch (command.Opcode)
            {
                case 0xE0:
                    numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                    numB.Enabled = true; nameB.Enabled = true; doNothingB.Enabled = true;
                    numC.Enabled = true; nameC.Enabled = true; doNothingC.Enabled = true;
                    //
                    this.nameA.Items.AddRange(this.attackNames.Names);
                    this.nameA.DrawMode = DrawMode.OwnerDrawFixed; this.nameA.ItemHeight = 15;
                    this.nameB.Items.AddRange(this.attackNames.Names);
                    this.nameB.DrawMode = DrawMode.OwnerDrawFixed; this.nameB.ItemHeight = 15;
                    this.nameC.Items.AddRange(this.attackNames.Names);
                    this.nameC.DrawMode = DrawMode.OwnerDrawFixed; this.nameC.ItemHeight = 15;
                    numA.Maximum = numB.Maximum = numC.Maximum = 128;
                    //
                    if (command.Param1 != 0xFB)
                        nameA.SelectedIndex = attackNames.GetSortedIndex((int)command.Param1);
                    else
                        doNothingA.Checked = true;
                    if (command.Param2 != 0xFB)
                        nameB.SelectedIndex = attackNames.GetSortedIndex((int)command.Param2);
                    else
                        doNothingB.Checked = true;
                    if (command.Param3 != 0xFB)
                        nameC.SelectedIndex = attackNames.GetSortedIndex((int)command.Param3);
                    else
                        doNothingC.Checked = true;
                    break;
                case 0xE2:
                    target.Enabled = true;
                    labelTargetA.Text = "Set target";
                    //
                    this.target.Items.AddRange(Lists.TargetNames);
                    //
                    this.target.SelectedIndex = command.Param1;
                    break;
                case 0xE3:
                    numA.Enabled = true; nameA.Enabled = true;
                    //
                    this.nameA.BackColor = SystemColors.Window;
                    this.nameA.DropDownWidth = 250;
                    for (int i = 0; i < Model.BattleDialogues.Length; i++)
                        this.nameA.Items.Add(Model.BattleDialogues[i].GetStub());
                    this.nameA.DrawMode = DrawMode.Normal;
                    //
                    this.nameA.SelectedIndex = command.Param1;
                    this.numA.Value = command.Param1;
                    break;
                case 0xE5:
                    numA.Enabled = true; nameA.Enabled = true;
                    //
                    this.nameA.BackColor = SystemColors.Window;
                    this.nameA.DropDownWidth = 400;
                    this.nameA.Items.AddRange(Lists.Numerize(Lists.BattleEventNames));
                    this.nameA.DrawMode = DrawMode.Normal;
                    this.numA.Maximum = Lists.BattleEventNames.Length;
                    //
                    nameA.SelectedIndex = command.Param1;
                    //
                    break;
                case 0xE6:
                    memory.Enabled = true;
                    labelMemoryA.Text = command.Param1 == 0 ? "Increment" : "Decrement" + " mem addr";
                    //
                    this.memory.Value = 0x7EE000 + command.Param2;
                    break;
                case 0xE7:
                    memory.Enabled = true; panelBits.Enabled = true;
                    labelMemoryA.Text = command.Param1 == 0 ? "Set" : "Clear" + " memory address";
                    labelMemoryC.Text = "Bits";
                    //
                    this.memory.Value = 0x7EE000 + command.Param2;
                    SetInitialBits(command.Param3);
                    break;
                case 0xE8:
                    memory.Enabled = true;
                    labelMemoryA.Text = "Clear memory address";
                    //
                    this.memory.Value = 0x7EE000 + command.Param1;
                    break;
                case 0xEA:
                    target.Enabled = true;
                    labelTargetA.Text = command.Param1 == 0 ? "Remove" : "Call" + " Target";
                    //
                    this.target.Items.AddRange(Lists.TargetNames);
                    //
                    this.target.SelectedIndex = command.Param3;
                    break;
                case 0xEB:
                    target.Enabled = true;
                    labelTargetA.Text = command.Param1 == 0 ? "Set" : "Null" + " target invincibility";
                    //
                    this.target.Items.AddRange(Lists.TargetNames);
                    //
                    this.target.SelectedIndex = command.Param2;
                    break;
                case 0xED:
                    labelMemoryB.Text = "Random # less than";
                    comparison.Enabled = true;
                    //
                    this.comparison.Value = command.Param1;
                    break;
                case 0xEF:
                    numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                    //
                    this.nameA.Items.AddRange(this.spellNames.Names);
                    this.nameA.DrawMode = DrawMode.OwnerDrawFixed; this.nameA.ItemHeight = 15;
                    numA.Maximum = 127;
                    //
                    this.nameA.SelectedIndex = spellNames.GetSortedIndex(command.Param1);
                    break;
                case 0xF0:
                    numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                    numB.Enabled = true; nameB.Enabled = true; doNothingB.Enabled = true;
                    numC.Enabled = true; nameC.Enabled = true; doNothingC.Enabled = true;
                    //
                    this.nameA.Items.AddRange(this.spellNames.Names);
                    this.nameA.DrawMode = DrawMode.OwnerDrawFixed; this.nameA.ItemHeight = 15;
                    this.nameB.Items.AddRange(this.spellNames.Names);
                    this.nameB.DrawMode = DrawMode.OwnerDrawFixed; this.nameB.ItemHeight = 15;
                    this.nameC.Items.AddRange(this.spellNames.Names);
                    this.nameC.DrawMode = DrawMode.OwnerDrawFixed; this.nameC.ItemHeight = 15;
                    numA.Maximum = numB.Maximum = numC.Maximum = 127;
                    //
                    if (command.Param1 != 0xFB)
                        nameA.SelectedIndex = spellNames.GetSortedIndex((int)command.Param1);
                    else
                        doNothingA.Checked = true;
                    if (command.Param2 != 0xFB)
                        nameB.SelectedIndex = spellNames.GetSortedIndex((int)command.Param2);
                    else
                        doNothingB.Checked = true;
                    if (command.Param3 != 0xFB)
                        nameC.SelectedIndex = spellNames.GetSortedIndex((int)command.Param3);
                    else
                        doNothingC.Checked = true;
                    break;
                case 0xF1:
                    labelMemoryB.Text = "Behavior animation";
                    comparison.Enabled = true;
                    //
                    comparison.Value = command.Param1;
                    break;
                case 0xF2:
                    target.Enabled = true;
                    labelTargetA.Text = command.Param1 == 0 ? "Disable" : "Enable" + " target";
                    //
                    this.target.Items.AddRange(new object[] {
                        "self",
                        "monster 1",
                        "monster 2",
                        "monster 3",
                        "monster 4",
                        "monster 5",
                        "monster 6",
                        "monster 7",
                        "monster 8"});
                    this.target.SelectedIndex = command.Param2;
                    break;
                case 0xF3:
                    effects.Enabled = true;
                    effects.Items.AddRange(new object[] {
                        "Attack",
                        "Special",
                        "Item"});
                    //
                    effects.SetItemChecked(0, (command.Param2 & 0x01) == 0x01);
                    effects.SetItemChecked(1, (command.Param2 & 0x02) == 0x02);
                    effects.SetItemChecked(2, (command.Param2 & 0x04) == 0x04);
                    break;
                case 0xF4:
                    nameA.Enabled = true;
                    //
                    this.nameA.BackColor = SystemColors.Window;
                    this.nameA.Items.AddRange(new object[] {
                        "Remove Items",
                        "Return Items"});
                    this.nameA.DrawMode = DrawMode.Normal;
                    //
                    nameA.SelectedIndex = command.Param2;
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x01:
                            nameA.Enabled = true; nameB.Enabled = true;
                            //
                            this.nameA.BackColor = SystemColors.Window;
                            this.nameA.Items.AddRange(new object[] {
                                "Attack",
                                "Special",
                                "Item"});
                            this.nameA.DrawMode = DrawMode.Normal;
                            this.nameB.BackColor = SystemColors.Window;
                            this.nameB.Items.AddRange(new object[] {                        
                                "Attack",
                                "Special",
                                "Item"});
                            this.nameB.DrawMode = DrawMode.Normal;
                            //
                            nameA.SelectedIndex = Math.Max(0, (int)(command.Param2 - 2));
                            nameB.SelectedIndex = Math.Max(0, (int)(command.Param3 - 2));
                            break;
                        case 0x02:
                            numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                            numB.Enabled = true; nameB.Enabled = true; doNothingB.Enabled = true;
                            //
                            this.nameA.Items.AddRange(this.spellNames.Names);
                            this.nameA.DrawMode = DrawMode.OwnerDrawFixed;
                            this.nameA.ItemHeight = 15;
                            this.nameB.Items.AddRange(this.spellNames.Names);
                            this.nameB.DrawMode = DrawMode.OwnerDrawFixed;
                            this.nameB.ItemHeight = 15;
                            //
                            if (command.Param2 != 0xFB)
                                nameA.SelectedIndex = spellNames.GetSortedIndex((int)command.Param2);
                            else
                                doNothingA.Checked = true;
                            if (command.Param3 != 0xFB)
                                nameB.SelectedIndex = spellNames.GetSortedIndex((int)command.Param3);
                            else
                                doNothingB.Checked = true;
                            break;
                        case 0x03:
                            numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                            numB.Enabled = true; nameB.Enabled = true; doNothingB.Enabled = true;
                            //
                            this.nameA.Items.AddRange(Model.ItemNames.Names);
                            this.nameA.DrawMode = DrawMode.OwnerDrawFixed;
                            this.nameA.ItemHeight = 15;
                            this.nameB.Items.AddRange(Model.ItemNames.Names);
                            this.nameB.DrawMode = DrawMode.OwnerDrawFixed;
                            this.nameB.ItemHeight = 15;
                            //
                            if (command.Param2 != 0xFB)
                                nameA.SelectedIndex = Model.ItemNames.GetSortedIndex((int)command.Param2);
                            else
                                doNothingA.Checked = true;
                            if (command.Param3 != 0xFB)
                                nameB.SelectedIndex = Model.ItemNames.GetSortedIndex((int)command.Param3);
                            else
                                doNothingB.Checked = true;
                            break;
                        case 0x04:
                            effects.Enabled = true;
                            this.effects.Items.AddRange(new object[] {
                                "Ice",
                                "Thunder",
                                "Fire",
                                "Jump"});
                            //
                            effects.SetItemChecked(0, (command.Param2 & 0x10) == 0x10);
                            effects.SetItemChecked(1, (command.Param2 & 0x20) == 0x20);
                            effects.SetItemChecked(2, (command.Param2 & 0x40) == 0x40);
                            effects.SetItemChecked(3, (command.Param2 & 0x80) == 0x80);
                            break;
                        case 0x06:
                            target.Enabled = true; targetNum.Enabled = true;
                            labelTargetA.Text = "If Target"; labelTargetB.Text = "HP is below";
                            this.target.Items.AddRange(Lists.TargetNames);
                            //
                            this.target.SelectedIndex = command.Param2;
                            targetNum.Value = command.Param3 * 16;
                            break;
                        case 0x07:
                            labelMemoryB.Text = "If HP less than";
                            comparison.Enabled = true;
                            comparison.Maximum = 0xFFFF;
                            //
                            comparison.Value = Bits.GetShort(command.CommandData, 2);
                            break;
                        case 0x08:
                        case 0x09:
                            target.Enabled = true; effects.Enabled = true;
                            labelTargetA.Text = "If target";
                            //
                            this.target.Items.AddRange(Lists.TargetNames);
                            this.effects.Items.AddRange(new object[] {
                                "Mute",
                                "Sleep",
                                "Poison",
                                "Fear",
                                "Mushroom",
                                "Scarecrow",
                                "Invincibility"});
                            //
                            this.target.SelectedIndex = command.Param2;
                            effects.SetItemChecked(0, (command.Param3 & 0x01) == 0x01);
                            effects.SetItemChecked(1, (command.Param3 & 0x02) == 0x02);
                            effects.SetItemChecked(2, (command.Param3 & 0x04) == 0x04);
                            effects.SetItemChecked(3, (command.Param3 & 0x08) == 0x08);
                            effects.SetItemChecked(4, (command.Param3 & 0x20) == 0x20);
                            effects.SetItemChecked(5, (command.Param3 & 0x40) == 0x40);
                            effects.SetItemChecked(6, (command.Param3 & 0x80) == 0x80);
                            break;
                        case 0x0A:
                            comparison.Enabled = true;
                            labelMemoryB.Text = "If attack phase =";
                            //
                            this.comparison.Value = command.Param2;
                            break;
                        case 0x0C:
                            memory.Enabled = true; comparison.Enabled = true;
                            labelMemoryA.Text = "If memory address";
                            labelMemoryB.Text = "Less than";
                            //
                            this.memory.Value = 0x7EE000 + command.Param2;
                            this.comparison.Value = command.Param3;
                            break;
                        case 0x0D:
                            memory.Enabled = true; comparison.Enabled = true;
                            labelMemoryA.Text = "If memory address";
                            labelMemoryB.Text = "Greater than";
                            //
                            this.memory.Value = 0x7EE000 + command.Param2;
                            this.comparison.Value = command.Param3;
                            break;
                        case 0x10:
                            target.Enabled = true;
                            labelTargetA.Text = "If target " + (command.Param2 == 0 ? "alive" : "dead");
                            //
                            this.target.Items.AddRange(Lists.TargetNames);
                            //
                            this.target.SelectedIndex = command.Param3;
                            break;
                        case 0x11:
                            memory.Enabled = true; panelBits.Enabled = true;
                            labelMemoryA.Text = "If memory address";
                            labelMemoryC.Text = "Bits set";
                            //
                            this.memory.Value = 0x7EE000 + command.Param2;
                            SetInitialBits(command.Param3);
                            break;
                        case 0x12:
                            memory.Enabled = true; panelBits.Enabled = true;
                            labelMemoryA.Text = "If memory address";
                            labelMemoryC.Text = "Bits clear";
                            //
                            this.memory.Value = 0x7EE000 + command.Param2;
                            SetInitialBits(command.Param3);
                            break;
                        case 0x13:
                            labelMemoryB.Text = "If in formation";
                            comparison.Enabled = true;
                            comparison.Maximum = 511;
                            //
                            comparison.Value = Bits.GetShort(command.CommandData, 2);
                            break;
                    }
                    break;
                default:
                    if (command.Opcode >= 0xE0)
                        break;
                    numA.Enabled = true; nameA.Enabled = true; doNothingA.Enabled = true;
                    //
                    this.nameA.Items.AddRange(this.attackNames.Names);
                    this.nameA.DrawMode = DrawMode.OwnerDrawFixed; this.nameA.ItemHeight = 15;
                    numA.Maximum = 128;
                    //
                    if (command.Param1 != 0xFB)
                        nameA.SelectedIndex = attackNames.GetSortedIndex((int)command.Param1);
                    else
                        doNothingA.Checked = true;
                    break;
            }
            OrganizeControls();
            panelRight.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssemble()
        {
            switch (command.Opcode)
            {
                case 0xE0:
                case 0xF0:
                    if (!doNothingA.Checked)
                        command.Param1 = (byte)numA.Value;
                    else
                        command.Param1 = 0xFB;
                    if (!doNothingB.Checked)
                        command.Param2 = (byte)numB.Value;
                    else
                        command.Param2 = 0xFB;
                    if (!doNothingC.Checked)
                        command.Param3 = (byte)numC.Value;
                    else
                        command.Param3 = 0xFB;
                    break;
                case 0xE2:
                    command.Param1 = (byte)target.SelectedIndex;
                    break;
                case 0xE3:
                case 0xE5:
                case 0xEF:
                    command.Param1 = (byte)numA.Value;
                    break;
                case 0xED:
                case 0xF1:
                    command.Param1 = (byte)comparison.Value;
                    break;
                case 0xE6:
                    command.Param2 = (byte)(memory.Value - 0x7EE000);
                    break;
                case 0xE7:
                    command.Param2 = (byte)(memory.Value - 0x7EE000);
                    foreach (CheckBox bit in panelBits.Controls)
                        Bits.SetBit(command.CommandData, 3, bit.TabIndex, bit.Checked);
                    break;
                case 0xE8:
                    command.Param1 = (byte)(memory.Value - 0x7EE000);
                    break;
                case 0xEA:
                    command.Param3 = (byte)target.SelectedIndex;
                    break;
                case 0xEB:
                    command.Param2 = (byte)target.SelectedIndex;
                    break;
                case 0xF2:
                    command.Param2 = (byte)target.SelectedIndex;
                    break;
                case 0xF3:
                    for (int i = 0; i < 3; i++)
                        Bits.SetBit(command.CommandData, 2, i, effects.GetItemChecked(i));
                    break;
                case 0xF4:
                    command.Param2 = (byte)nameA.SelectedIndex;
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x01:
                            command.Param2 = (byte)(nameA.SelectedIndex + 2);
                            command.Param3 = (byte)(nameB.SelectedIndex + 2);
                            break;
                        case 0x02:
                        case 0x03:
                            command.Param2 = (byte)(doNothingA.Checked ? 0xFB : numA.Value);
                            command.Param3 = (byte)(doNothingB.Checked ? 0xFB : numB.Value);
                            break;
                        case 0x04:
                            for (int i = 0; i < 4; i++)
                                Bits.SetBit(command.CommandData, 2, i + 4, effects.GetItemChecked(i));
                            break;
                        case 0x06:
                            command.Param2 = (byte)target.SelectedIndex;
                            command.Param3 = (byte)(targetNum.Value / 16);
                            break;
                        case 0x08:
                        case 0x09:
                            command.Param2 = (byte)target.SelectedIndex;
                            Bits.SetBit(command.CommandData, 3, 0, effects.GetItemChecked(0));
                            Bits.SetBit(command.CommandData, 3, 1, effects.GetItemChecked(1));
                            Bits.SetBit(command.CommandData, 3, 2, effects.GetItemChecked(2));
                            Bits.SetBit(command.CommandData, 3, 3, effects.GetItemChecked(3));
                            Bits.SetBit(command.CommandData, 3, 5, effects.GetItemChecked(4));
                            Bits.SetBit(command.CommandData, 3, 6, effects.GetItemChecked(5));
                            Bits.SetBit(command.CommandData, 3, 7, effects.GetItemChecked(6));
                            break;
                        case 0x0A:
                            command.Param2 = (byte)comparison.Value;
                            break;
                        case 0x0C:
                        case 0x0D:
                            command.Param2 = (byte)(memory.Value - 0x7EE000);
                            command.Param3 = (byte)comparison.Value;
                            break;
                        case 0x10:
                            command.Param3 = (byte)target.SelectedIndex;
                            break;
                        case 0x11:
                        case 0x12:
                            command.Param2 = (byte)(memory.Value - 0x7EE000);
                            foreach (CheckBox bit in panelBits.Controls)
                                Bits.SetBit(command.CommandData, 3, bit.TabIndex, bit.Checked);
                            break;
                        case 0x07:
                        case 0x13:
                            Bits.SetShort(command.CommandData, 2, (ushort)comparison.Value);
                            break;
                    }
                    break;
                default:
                    if (command.Opcode < 0xE0)
                        command.Opcode = (byte)numA.Value;
                    break;
            }
        }
        private void ResetControls()
        {
            this.Updating = true;
            //
            nameA.BackColor = SystemColors.ControlDarkDark; nameA.ItemHeight = 15;
            nameA.Items.Clear(); nameA.ResetText(); nameA.DropDownWidth = nameA.Width;
            nameB.BackColor = SystemColors.ControlDarkDark; nameB.ItemHeight = 15;
            nameB.Items.Clear(); nameB.ResetText(); nameB.DropDownWidth = nameB.Width;
            nameC.BackColor = SystemColors.ControlDarkDark; nameC.ItemHeight = 15;
            nameC.Items.Clear(); nameC.ResetText(); nameC.DropDownWidth = nameC.Width;
            numA.Minimum = 0; numA.Maximum = 255; numA.Value = 0;
            numB.Minimum = 0; numB.Maximum = 255; numB.Value = 0;
            numC.Minimum = 0; numC.Maximum = 255; numC.Value = 0;
            numA.Enabled = nameA.Enabled = doNothingA.Enabled = false;
            numB.Enabled = nameB.Enabled = doNothingB.Enabled = false;
            numC.Enabled = nameC.Enabled = doNothingC.Enabled = false;
            doNothingA.Checked = false;
            doNothingB.Checked = false;
            doNothingC.Checked = false;
            //
            target.Enabled = targetNum.Enabled = effects.Enabled = false;
            target.Items.Clear(); target.ResetText(); targetNum.Value = 0;
            effects.Items.Clear(); effects.Height = 68;
            labelTargetA.Text = "";
            labelTargetB.Text = "";
            //
            memory.Enabled = comparison.Enabled = panelBits.Enabled = false;
            memory.Minimum = 0x7EE000; memory.Maximum = 0x7EE00F; memory.Value = 0x7EE000;
            comparison.Minimum = 0; comparison.Maximum = 255; comparison.Value = 0;
            labelMemoryA.Text = "";
            labelMemoryB.Text = "";
            labelMemoryC.Text = "";
            bit0.Checked = false;
            bit1.Checked = false;
            bit2.Checked = false;
            bit3.Checked = false;
            bit4.Checked = false;
            bit5.Checked = false;
            bit6.Checked = false;
            bit7.Checked = false;
            //
            this.Updating = false;
        }
        private void OrganizeControls()
        {
            panelAttack.Visible =
                nameA.Enabled || numA.Enabled || doNothingA.Enabled ||
                nameB.Enabled || numB.Enabled || doNothingB.Enabled ||
                nameC.Enabled || numC.Enabled || doNothingC.Enabled;
            panelAttackA.Visible = nameA.Enabled || numA.Enabled || doNothingA.Enabled;
            panelAttackB.Visible = nameB.Enabled || numB.Enabled || doNothingB.Enabled;
            panelAttackC.Visible = nameC.Enabled || numC.Enabled || doNothingC.Enabled;
            //
            panelTarget.Visible = target.Enabled || targetNum.Enabled || effects.Enabled;
            panelTargetA.Visible = target.Enabled;
            panelTargetB.Visible = targetNum.Enabled;
            if (effects.Items.Count < 4)
                effects.Height = effects.Items.Count * 16 + 4;
            else
                effects.Height = 68;
            effects.Visible = effects.Enabled;
            //
            panelMemory.Visible = memory.Enabled || comparison.Enabled || panelBits.Enabled;
            panelMemoryA.Visible = memory.Enabled;
            panelMemoryB.Visible = comparison.Enabled;
            panelMemoryC.Visible = panelBits.Enabled;
            //
            panelAttackA.BringToFront();
            panelAttackB.BringToFront();
            panelAttackC.BringToFront();
            panelTargetA.BringToFront();
            panelTargetB.BringToFront();
            effects.BringToFront();
            panelMemoryA.BringToFront();
            panelMemoryB.BringToFront();
            panelMemoryC.BringToFront();
        }
        // modify commands
        private void AddCommand(BattleCommand bsc)
        {
            foreach (BattleCommand command in battleScript.Commands)
                command.Modified = false;
            //
            commandTree.ExpandAll();
            int fullIndex = commandTree.GetFullIndex();
            if (fullIndex < 0)
            {
                MessageBox.Show("Must select a command in the command tree on the left before inserting a new command.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (fullIndex + 1 < this.commandTree.GetNodeCount(true))
                battleScript.Insert(++fullIndex, bsc);
            else if (fullIndex + 1 == this.commandTree.GetNodeCount(true))
                battleScript.Insert(fullIndex, bsc);
            else
                battleScript.Insert(0, bsc);
            bsc.Modified = true;
            //
            RefreshScript(fullIndex);
        }
        private void ReplaceCommand(BattleCommand bsc)
        {
            foreach (BattleCommand command in battleScript.Commands)
                command.Modified = false;
            bsc = (BattleCommand)modifiedNode.Tag;
            bsc.Modified = true;
            //
            RefreshScript(commandTree.GetFullIndex());
        }
        private void Remove()
        {
            for (int i = battleScript.Commands.Count - 1; i >= 0; i--)
            {
                TreeNode node = commandTree.GetNode(i);
                if (node.Checked)
                    battleScript.RemoveAt(i);
            }
        }
        private void RemoveAll()
        {
            battleScript.Clear();
            //
            RefreshScript();
        }
        private void MoveUp()
        {
            for (int i = 0; i < battleScript.Commands.Count; i++)
            {
                TreeNode node = commandTree.GetNode(i);
                if (node.Checked)
                    battleScript.Reverse(i - 1, 2);
            }
        }
        private void MoveDown()
        {
            if (battleScript.Commands.Count <= 0)
                return;
            for (int i = battleScript.Commands.Count - 1; i >= 0; i--)
            {
                TreeNode node = commandTree.GetNode(i);
                if (node.Checked)
                    battleScript.Reverse(i, 2);
            }
        }
        private void CopyCommands(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    BattleCommand bsc = (BattleCommand)node.Tag;
                    commandCopies.Add(bsc.Copy());
                }
                CopyCommands(node.Nodes);
            }
        }
        public void PushCommand(byte[] oldScript)
        {
            commandStack.Push(new CommandEdit(battleScripts, index, oldScript, this, commandTree.GetFullIndex()));
        }
        //
        private void UpdateBattleScriptsFreeSpace()
        {
            int bytesLeft = CalculateBattleScriptsLength();
            this.labelBytesLeft.Text = " " + bytesLeft.ToString() + " bytes left";
            this.labelBytesLeft.BackColor = bytesLeft < 0 ? Color.Red : SystemColors.Control;
        }
        private int CalculateBattleScriptsLength()
        {
            int block1Size = 0x274A;//0x3959F3 - 0x3932AA;
            int block2Size = 0x0C00;//0x39FFFF - 0x39F400;
            int totalSize = block1Size + block2Size;
            //
            int length = 0;
            //
            int i = 0;
            for (; i < battleScripts.Length && length + battleScripts[i].Length < block1Size; i++)
                length += battleScripts[i].Length;
            //
            if (i < battleScripts.Length - 1)
                length = block1Size;
            //
            for (; i < battleScripts.Length; i++)
                length += battleScripts[i].Length;
            //
            return totalSize - length - 1;
        }
        //
        private void SetInitialBits(byte bits)
        {
            this.Updating = true;
            //
            if ((bits & 0x01) != 0) bit0.Checked = true;
            if ((bits & 0x02) != 0) bit1.Checked = true;
            if ((bits & 0x04) != 0) bit2.Checked = true;
            if ((bits & 0x08) != 0) bit3.Checked = true;
            if ((bits & 0x10) != 0) bit4.Checked = true;
            if ((bits & 0x20) != 0) bit5.Checked = true;
            if ((bits & 0x40) != 0) bit6.Checked = true;
            if ((bits & 0x80) != 0) bit7.Checked = true;
            //
            this.Updating = false;
        }
        public void Assemble()
        {
            if (CalculateBattleScriptsLength() >= 0)
            {
                AssembleAllBattleScripts();
            }
            else
                MessageBox.Show("There is not enough available space to save the battle scripts to.\n//\nThe battle scripts were not saved.", "LAZY SHELL");
        }
        public void AssembleAllBattleScripts()
        {
            // Assemble BattleScript Data
            int i = 0;
            int pointerTable = 0x3930AA;
            // Block 1
            int offset = 0x3932AA;
            for (; i < battleScripts.Length && offset + battleScripts[i].Length <= 0x3959F3; i++)
            {
                Bits.SetShort(Model.ROM, pointerTable + (i * 2), offset & 0xFFFF);
                battleScripts[i].Assemble(ref offset);
            }
            // Block 2
            offset = 0x39F400;
            for (; i < battleScripts.Length && offset + battleScripts[i].Length <= 0x39FFFF; i++)
            {
                Bits.SetShort(Model.ROM, pointerTable + (i * 2), offset & 0xFFFF);
                battleScripts[i].Assemble(ref offset);
            }
            if (i != battleScripts.Length)
                MessageBox.Show("Not enough space to save all battlescripts.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public void DumpBattlescriptText()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - battleScripts.txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //
            StreamWriter writer = File.CreateText(saveFileDialog.FileName);
            foreach (BattleScript script in battleScripts)
            {
                int level = 0;
                bool startCounter = false;
                string name = Model.MonsterNames.NumerizeUnsorted(script.Index, Lists.KeystrokesMenu);
                writer.WriteLine(name + "------------------------------------------------------------>");
                writer.WriteLine();
                foreach (BattleCommand bsc in script.Commands)
                {
                    if (bsc.Opcode == 0xFF)
                        level = 0;
                    string padding = "".PadLeft(level * 3);
                    writer.WriteLine(padding + bsc.ToString());
                    if (bsc.Opcode == 0xFC)
                        level++;
                    if (bsc.Opcode == 0xFF && !startCounter)
                    {
                        level++;
                        startCounter = true;
                    }
                    if (bsc.Opcode == 0xFE)
                        level = startCounter ? 1 : 0;
                }
                writer.WriteLine();
                writer.WriteLine();
            }
            writer.Close();
        }
        #endregion
        #region Event Handlers
        private void BattleScripts_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void BattleScriptTree_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
                case Keys.Control | Keys.A: Do.SelectAllNodes(commandTree.Nodes, true); break;
                case Keys.Control | Keys.D: Do.SelectAllNodes(commandTree.Nodes, false); break;
                case Keys.Control | Keys.C: BatScrCopyCommand.PerformClick(); break;
                case Keys.Control | Keys.V: BatScrPasteCommand.PerformClick(); break;
                case Keys.Control | Keys.Up:
                case Keys.Shift | Keys.Up:
                    e.SuppressKeyPress = true;
                    BatScrMoveUp.PerformClick();
                    break;
                case Keys.Control | Keys.Down:
                case Keys.Shift | Keys.Down:
                    e.SuppressKeyPress = true;
                    BatScrMoveDown.PerformClick();
                    break;
                case Keys.Delete: BatScrDeleteCommand.PerformClick(); break;
            }
        }
        private void BattleScriptTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BattleCommand bsc = (BattleCommand)e.Node.Tag;
            //
            hexText.Text = BitConverter.ToString(bsc.CommandData);
        }
        private void BattleScriptTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BatScrEditCommand.PerformClick();
        }
        private void BattleScriptTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            BattleCommand bsc = (BattleCommand)e.Node.Tag;
            if (bsc.Opcode != 0xFF)
                bsc.Modified = e.Node.Checked;
        }
        private void BattleScriptTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            BattleCommand bsc = (BattleCommand)e.Node.Tag;
            if (bsc.Opcode == 0xFF)
            {
                MessageBox.Show(
                    "Cannot check command(s).\n\nThe two counter command barriers cannot be removed, modified, or moved.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
        private void BattleScriptTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            commandTree.SelectedNode = e.Node;
            if (e.Button != MouseButtons.Right)
                return;
            goToToolStripMenuItem.Click -= goToDialogue_Click;
            goToToolStripMenuItem.Click -= goToEvent_Click;
            BattleCommand temp = (BattleCommand)commandTree.SelectedNode.Tag;
            if (temp.Opcode == 0xE3)
            {
                e.Node.ContextMenuStrip = contextMenuStripGoto;
                goToToolStripMenuItem.Text = "Edit dialogue...";
                goToToolStripMenuItem.Click += new EventHandler(goToDialogue_Click);
            }
            else if (temp.Opcode == 0xE5)
            {
                e.Node.ContextMenuStrip = contextMenuStripGoto;
                goToToolStripMenuItem.Text = "Edit event...";
                goToToolStripMenuItem.Click += new EventHandler(goToEvent_Click);
            }
        }
        // context menustrip
        private void goToDialogue_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            //
            BattleCommand temp = (BattleCommand)commandTree.SelectedNode.Tag;
            int num = temp.CommandData[1];
            //
            if (Model.Program.Dialogues == null || !Model.Program.Dialogues.Visible)
                Model.Program.CreateDialoguesWindow();
            //
            Model.Program.Dialogues.BattleDialogues.Index = num;
            Model.Program.Dialogues.BringToFront();
        }
        private void goToEvent_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            //
            BattleCommand temp = (BattleCommand)commandTree.SelectedNode.Tag;
            int num = temp.CommandData[1];
            //
            if (Model.Program.Animations == null || !Model.Program.Animations.Visible)
                Model.Program.CreateAnimationsWindow();
            //
            Model.Program.Animations.Category = 7;
            Model.Program.Animations.Index = num;
            Model.Program.Animations.BringToFront();
        }
        // Command properties
        private void listBoxCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte opcode = Lists.BattleOpcodes[listBoxCommands.SelectedIndex];
            byte param1 = Lists.BattleParams[listBoxCommands.SelectedIndex];
            byte[] commandData = new byte[Lists.BattleLengths[opcode]];
            this.command = new BattleCommand(commandData);
            this.command.Opcode = opcode;
            this.command.Param1 = param1;
            if (this.command.Opcode == 0xFC &&
                this.command.Param1 == 0x10 &&
                this.listBoxCommands.Text == "If target dead")
                this.command.Param2 = 0x01;
            ControlDisassemble();
        }
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            command = command.Copy();
            ControlAssemble();
            AddCommand(command.Copy());
            listBoxCommands.Focus();
            //
            PushCommand(oldScript);
        }
        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (modifiedNode == null)
                return;
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            ControlAssemble();
            ReplaceCommand(command);
            BatScrEditCommand.PerformClick();
            buttonApply.Focus();
            //
            PushCommand(oldScript);
        }
        private void name_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xFC:
                    if (command.Param1 == 0x02)
                        goto case 0xF0;
                    if (command.Param1 == 0x03)
                        Do.DrawName(
                            sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                            Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, false, Model.MenuBG_);
                    break;
                case 0xEF:
                case 0xF0:
                    if (e.Index < 0 || e.Index >= 128)
                        break;
                    Do.DrawName(
                        sender, e, new BattleDialoguePreview(), Model.SpellNames,
                        Model.SpellNames.GetUnsortedIndex(e.Index) < 64 ? Model.FontMenu : Model.FontDialogue,
                        Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, false, Model.MenuBG_);
                    break;
                case 0xE0:
                    goto default;
                default:
                    Do.DrawName(
                        sender, e, new BattleDialoguePreview(), Model.AttackNames, Model.FontDialogue,
                        Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, true, Model.MenuBG_);
                    break;
            }
        }
        private void numA_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    nameA.SelectedIndex = attackNames.GetSortedIndex((int)numA.Value);
                    break;
                case 0xE3:
                case 0xE5:
                    nameA.SelectedIndex = (int)numA.Value;
                    break;
                case 0xEF:
                case 0xF0:
                    nameA.SelectedIndex = spellNames.GetSortedIndex((int)numA.Value);
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x02:
                            nameA.SelectedIndex = spellNames.GetSortedIndex((int)numA.Value); break;
                        case 0x03:
                            nameA.SelectedIndex = Model.ItemNames.GetSortedIndex((int)numA.Value); break;
                    }
                    break;
                default:
                    nameA.SelectedIndex = attackNames.GetSortedIndex((int)numA.Value);
                    break;
            }
        }
        private void nameA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    numA.Value = attackNames.GetUnsortedIndex(nameA.SelectedIndex);
                    break;
                case 0xE3:
                case 0xE5:
                    numA.Value = nameA.SelectedIndex;
                    break;
                case 0xEF:
                case 0xF0:
                    numA.Value = spellNames.GetUnsortedIndex(nameA.SelectedIndex);
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x02:
                            numA.Value = spellNames.GetUnsortedIndex(nameA.SelectedIndex);
                            break;
                        case 0x03:
                            numA.Value = Model.ItemNames.GetUnsortedIndex(nameA.SelectedIndex);
                            break;
                    }
                    break;
                default:
                    numA.Value = attackNames.GetUnsortedIndex(nameA.SelectedIndex);
                    break;
            }
        }
        private void numB_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    nameB.SelectedIndex = attackNames.GetSortedIndex((int)numB.Value);
                    break;
                case 0xEF:
                case 0xF0:
                    nameB.SelectedIndex = spellNames.GetSortedIndex((int)numB.Value);
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x02:
                            nameB.SelectedIndex = spellNames.GetSortedIndex((int)numB.Value); break;
                        case 0x03:
                            nameB.SelectedIndex = Model.ItemNames.GetSortedIndex((int)numB.Value); break;
                    }
                    break;
            }
        }
        private void nameB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    numB.Value = attackNames.GetUnsortedIndex(nameB.SelectedIndex);
                    break;
                case 0xEF:
                case 0xF0:
                    numB.Value = spellNames.GetUnsortedIndex(nameB.SelectedIndex);
                    break;
                case 0xFC:
                    switch (command.Param1)
                    {
                        case 0x02:
                            numB.Value = spellNames.GetUnsortedIndex(nameB.SelectedIndex);
                            break;
                        case 0x03:
                            numB.Value = Model.ItemNames.GetUnsortedIndex(nameB.SelectedIndex);
                            break;
                    }
                    break;
                default:
                    numB.Value = nameB.SelectedIndex;
                    break;
            }
        }
        private void numC_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    nameC.SelectedIndex = attackNames.GetSortedIndex((int)numC.Value);
                    break;
                case 0xEF:
                case 0xF0:
                    nameC.SelectedIndex = spellNames.GetSortedIndex((int)numC.Value);
                    break;
            }
        }
        private void nameC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            switch (command.Opcode)
            {
                case 0xE0:
                    numC.Value = attackNames.GetUnsortedIndex(nameC.SelectedIndex);
                    break;
                case 0xEF:
                case 0xF0:
                    numC.Value = spellNames.GetUnsortedIndex(nameC.SelectedIndex);
                    break;
                default:
                    numC.Value = nameC.SelectedIndex;
                    break;
            }
        }
        private void doNothingA_CheckedChanged(object sender, EventArgs e)
        {
            doNothingA.ForeColor = doNothingA.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating)
                return;
            if (doNothingA.Checked)
            {
                nameA.Enabled = false;
                numA.Enabled = false;
            }
            else
            {
                nameA.Enabled = true;
                numA.Enabled = true;
                numA_ValueChanged(null, null);
            }
        }
        private void doNothingB_CheckedChanged(object sender, EventArgs e)
        {
            doNothingB.ForeColor = doNothingB.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating)
                return;
            if (doNothingB.Checked)
            {
                nameB.Enabled = false;
                numB.Enabled = false;
            }
            else
            {
                nameB.Enabled = true;
                numB.Enabled = true;
                numB_ValueChanged(null, null);
            }
        }
        private void doNothingC_CheckedChanged(object sender, EventArgs e)
        {
            doNothingC.ForeColor = doNothingC.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (this.Updating)
                return;
            if (doNothingC.Checked)
            {
                nameC.Enabled = false;
                numC.Enabled = false;
            }
            else
            {
                nameC.Enabled = true;
                numC.Enabled = true;
                numC_ValueChanged(null, null);
            }
        }
        // Editing Buttons
        private void BatScrCopyCommand_Click(object sender, EventArgs e)
        {
            commandCopies = new List<BattleCommand>();
            commandTree.ExpandAll();
            CopyCommands(commandTree.Nodes);
        }
        private void BatScrMoveUp_Click(object sender, EventArgs e)
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            if (battleScript.Commands.Count < 3)
                return;
            if (battleScript.Commands[0].Modified)
                return;
            MoveUp();
            RefreshScript(commandTree.GetFullIndex() - 1);
            //
            PushCommand(oldScript);
        }
        private void BatScrMoveDown_Click(object sender, EventArgs e)
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            if (battleScript.Commands.Count < 3)
                return;
            if (battleScript.Commands[battleScript.Commands.Count - 2].Modified)
                return;
            MoveDown();
            RefreshScript(commandTree.GetFullIndex() + 1);
            //
            PushCommand(oldScript);
        }
        private void BatScrPasteCommand_Click(object sender, EventArgs e)
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            if (commandCopies == null)
                return;
            foreach (BattleCommand bsc in commandCopies)
                AddCommand(bsc.Copy());
            //
            RefreshScript(commandTree.GetFullIndex() + 1);
            //
            PushCommand(oldScript);
        }
        private void BatScrDeleteCommand_Click(object sender, EventArgs e)
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            ResetControls();
            buttonInsert.Enabled = false;
            buttonApply.Enabled = false;
            //
            panelAttack.Visible = false;
            panelTarget.Visible = false;
            panelMemory.Visible = false;
            //
            Remove();
            RefreshScript(commandTree.GetFullIndex());
            //
            PushCommand(oldScript);
        }
        private void BatScrEditCommand_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            this.command = (BattleCommand)commandTree.SelectedNode.Tag;
            // Edit Command
            if (command.Opcode != 0xFF)
            {
                this.modifiedNode = commandTree.SelectedNode;
                commandTree.ExpandAll();
                //
                ControlDisassemble();
            }
            else
            {
                MessageBox.Show(
                    "Cannot edit command(s).\n\nThe two counter command barriers cannot be removed, modified, or moved.",
                    "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                commandTree.SelectedNode.Checked = false;
            }
        }
        private void BatScrExpandAll_Click(object sender, EventArgs e)
        {
            commandTree.ExpandAll();
        }
        private void BatScrCollapseAll_Click(object sender, EventArgs e)
        {
            commandTree.CollapseAll();
        }
        private void undo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.UndoCommand();
            commandTree.EndUpdate();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.RedoCommand();
            commandTree.EndUpdate();
        }
        // image
        private void pictureBoxMonster_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void pictureBoxMonster_MouseMove(object sender, MouseEventArgs e)
        {
            int x = 15 - (e.X / 8); int y = 15 - (e.Y / 8);
            if (x > 15) x = 15; if (x < 0) x = 0;
            if (y > 15) y = 15; if (y < 0) y = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (overTarget)
                {
                    if (monsterTargetArrowX.Value != x && monsterTargetArrowY.Value != y)
                        waitBothCoords = true;
                    monsterTargetArrowX.Value = x;
                    waitBothCoords = false;
                    monsterTargetArrowY.Value = y;
                }
            }
            else
            {
                if ((128 - (monsterTargetArrowX.Value * 8) > e.X && 128 - (monsterTargetArrowX.Value * 8) < e.X + 16) &&
                    (128 - (monsterTargetArrowY.Value * 8) > e.Y && 128 - (monsterTargetArrowY.Value * 8) < e.Y + 16))
                {
                    pictureBoxMonster.Cursor = Cursors.Hand;
                    overTarget = true;
                }
                else
                {
                    pictureBoxMonster.Cursor = Cursors.Arrow;
                    overTarget = false;
                }
            }
        }
        private void pictureBoxMonster_MouseUp(object sender, MouseEventArgs e)
        {
            monsterImage = new Bitmap(monster.Image);
            pictureBoxMonster.Invalidate();
        }
        private void pictureBoxMonster_Paint(object sender, PaintEventArgs e)
        {
            if (monsterImage != null)
                e.Graphics.DrawImage(monsterImage, 0, 0);
        }
        private void monsterTargetArrowX_ValueChanged(object sender, EventArgs e)
        {
            monster.CursorX = (byte)monsterTargetArrowX.Value;
            //
            if (waitBothCoords)
                return;
            monsterImage = new Bitmap(monster.Image);
            pictureBoxMonster.Invalidate();
        }
        private void monsterTargetArrowY_ValueChanged(object sender, EventArgs e)
        {
            monster.CursorY = (byte)monsterTargetArrowY.Value;
            //
            if (waitBothCoords)
                return;
            monsterImage = new Bitmap(monster.Image);
            pictureBoxMonster.Invalidate();
        }
        // toolstrip
        public void Import()
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            new IOElements((Element[])Model.BattleScripts, index, "IMPORT BATTLE SCRIPTS...").ShowDialog();
            Initialize();
            //
            if (!Bits.Compare(oldScript, battleScript.Script))
                PushCommand(oldScript);
        }
        public void Export()
        {
            new IOElements((Element[])Model.BattleScripts, index, "EXPORT BATTLE SCRIPTS...").ShowDialog();
            Initialize();
        }
        public void Clear()
        {
            byte[] oldScript = Bits.Copy(battleScript.Script);
            //
            new ClearElements(Model.BattleScripts, index, "CLEAR BATTLE SCRIPTS...").ShowDialog();
            Initialize();
            //
            if (!Bits.Compare(oldScript, battleScript.Script))
                PushCommand(oldScript);
        }
        #endregion
        private class CommandEdit : Command
        {
            private int index;
            private byte[] oldScript;
            private int selectedIndex;
            private BattleScripts form;
            private BattleScript[] battleScripts;
            private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
            public CommandEdit(BattleScript[] battleScripts, int index, byte[] oldScript, BattleScripts form, int selectedIndex)
            {
                this.index = index;
                this.oldScript = oldScript;
                this.battleScripts = battleScripts;
                this.selectedIndex = selectedIndex;
                this.form = form;
            }
            public void Execute()
            {
                if (battleScripts != null)
                {
                    this.form.index = index; // first switch back to script in index
                    // next, switch the scripts
                    byte[] temp = Bits.Copy(battleScripts[index].Script);
                    battleScripts[index].Script = Bits.Copy(oldScript);
                    battleScripts[index].Commands = null;
                    battleScripts[index].ParseScript();
                    oldScript = temp;
                    //
                    form.RefreshScript(selectedIndex);
                }
            }
        }
    }
}
