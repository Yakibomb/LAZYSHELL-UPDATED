using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;
using LAZYSHELL.Properties;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class AnimationScripts : NewForm
    {
        #region Variables
        private A_TreeViewWrapper wrapper;
        private AnimationScript[] animationScripts
        {
            get
            {
                switch (animationCategory.SelectedIndex)
                {
                    case 0: return Model.BehaviorAnimMonsters;
                    case 1: return Model.SpellAnimMonsters;
                    case 2: return Model.AttackAnimations;
                    case 3: return Model.EntranceAnimations;
                    case 4: return Model.ItemAnimations;
                    case 5: return Model.SpellAnimAllies;
                    case 6: return Model.WeaponAnimations;
                    case 7: return Model.WeaponSoundScripts;
                    case 8: return Model.WeaponTimedHitScripts;
                    case 9: return Model.BattleEvents;
                    case 10: return Model.BonusMessageAnimations;
                    case 11: return Model.ToadTutorialScript;
                    case 12: return Model.WeaponWrapperAnimations;
                    case 13: return Model.BehaviorAnimAllies;
                }
                return null;
            }
            set
            {
                switch (animationCategory.SelectedIndex)
                {
                    case 0: Model.BehaviorAnimMonsters = value; break;
                    case 1: Model.SpellAnimMonsters = value; break;
                    case 2: Model.AttackAnimations = value; break;
                    case 3: Model.EntranceAnimations = value; break;
                    case 4: Model.ItemAnimations = value; break;
                    case 5: Model.SpellAnimAllies = value; break;
                    case 6: Model.WeaponAnimations = value; break;
                    case 7: Model.WeaponSoundScripts = value; break;
                    case 8: Model.WeaponTimedHitScripts = value; break;
                    case 9: Model.BattleEvents = value; break;
                    case 10: Model.BonusMessageAnimations = value; break;
                    case 11: Model.ToadTutorialScript = value; break;
                    case 12: Model.WeaponWrapperAnimations = value; break;
                    case 13: Model.BehaviorAnimAllies = value; break;
                }
            }
        }
        private AnimationCommand asc; public AnimationCommand ASC { get { return asc; } set { asc = value; } }
        private AnimationCommand ascCopy;
        public int Index { get { return (int)animationNum.Value; } set { animationNum.Value = value; } }
        public int Category { get { return animationCategory.SelectedIndex; } set { animationCategory.SelectedIndex = value; } }
        private CommandStack commandStack;
        private Settings settings = Settings.Default;
        private BattleDialoguePreview battleDialoguePreview;
        private MenuTextPreview menuTextPreview;
        private ToolTipLabel toolTipLabel;
        private EditLabel labelWindow;
        private byte[] animationBank, battleBank, bonusmessageBank;
        Previewer ap;
        #endregion
        // constructor
        public AnimationScripts()
        {
            InitializeComponent();
            this.animationCategory.Items.AddRange(new object[] {
            "Monster Behaviors",
            "Monster Spells",
            "Monster Attacks",
            "Monster Entrances",
            "Items",
            "Ally Spells",
            "Weapons Animations",
            "Weapon Miss Sounds",
            "Weapon Timed-Hit Sounds",
            "Battle Events",
            "Flower Bonus Messages",
            "Toad's Tutorial",
            "Ally Weapon Wrapper",
            "Ally Behaviors"});
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip4, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip4, Keys.F2, baseConvertor);
            toolTipLabel = new ToolTipLabel(this, baseConvertor, helpTips);
            labelWindow = new EditLabel(animationName, animationNum, "Battle Events", true);
            for (int i = 0; i < 9; i++)
            {
                ToolStripNumericUpDown numUpDown = new ToolStripNumericUpDown();
                numUpDown.Hexadecimal = true;
                numUpDown.Maximum = 255;
                numUpDown.MouseMove += new MouseEventHandler(toolTipLabel.ControlMouseMove);
                numUpDown.Width = 40;
                toolStripCommands.Items.Insert(i + 9, numUpDown);
            }
            InitializeEditor();
            this.History = new History(this, animationName, animationNum);
            //
            commands.Items.AddRange(Lists.NumerizeHex(Lists.AnimationCommands));
        }
        #region Methods
        private void InitializeEditor()
        {
            this.Updating = true;
            //
            this.commandStack = new CommandStack();
            animationBank = Bits.GetBytes(Model.ROM, 0x350000, 0x10000);
            battleBank = Bits.GetBytes(Model.ROM, 0x3A6000, 0xA000);
            bonusmessageBank = Bits.GetBytes(Model.ROM, 0x02F000, 0x1000);
            //
            this.menuTextPreview = new MenuTextPreview();
            this.battleDialoguePreview = new BattleDialoguePreview();
            this.wrapper = new A_TreeViewWrapper(this.commandTree);
            //
            if (settings.RememberLastIndex)
                animationCategory.SelectedIndex = settings.LastAnimationCat;
            else
                animationCategory.SelectedIndex = 12;
            RefreshEditor();
            if (settings.RememberLastIndex)
                animationNum.Value = Math.Min((int)animationNum.Maximum, settings.LastAnimation);
            //
            UpdateAnimationCategory(animationCategory.SelectedIndex);

            this.Updating = false;
        }
        private void RefreshEditor()
        {
            animationName.ContextMenuStrip.Enabled = animationCategory.SelectedIndex == 8;
            animationNum.ContextMenuStrip.Enabled = animationCategory.SelectedIndex == 8;
            //
            animationScripts[(int)animationNum.Value].RefreshScript();
            switch (animationCategory.SelectedIndex)
            {
                case 0:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                    {
                        if (Lists.MonsterBehaviors[i] == "")
                            this.animationName.Items.Add("SCRIPT #" + i);
                        else 
                            this.animationName.Items.Add(Lists.MonsterBehaviors[i]);
                    }
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                case 1:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.SpellNames.GetUnsortedName(i + 0x40));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 2:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(Model.AttackNames.Names);
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 3:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(new object[] {
                        "none",
                        "slide in",
                        "long jump",
                        "hop 3 times",
                        "drop from above",
                        "zoom in from right",
                        "zoom in from left",
                        "spread out from back",
                        "hover in",
                        "ready to attack",
                        "fade in",
                        "slow drop from above",
                        "wait, then appear",
                        "spread from front",
                        "spread from middle",
                        "ready to attack"});
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                case 4:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.ItemNames.GetUnsortedName(i + 0x60));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 5:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.SpellNames.GetUnsortedName(i));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 6:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.ItemNames.GetUnsortedName(i));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 7:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.ItemNames.GetUnsortedName(i));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 8:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.ItemNames.GetUnsortedName(i));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 9:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(Lists.Numerize(Lists.BattleEventNames));
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                case 10:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(new object[] {
                        "{NONE}",
                        "ATTACK UP!",
                        "DEFENSE UP!",
                        "HPMAX!",
                        "ONCE AGAIN!",
                        "LUCKY!"});
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                case 11:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(new object[] {
                        "Toad's Tutorial"});
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                case 12:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    for (int i = 0; i < animationScripts.Length; i++)
                        this.animationName.Items.Add(Model.CharacterNames.GetUnsortedName(i));
                    animationName.DrawMode = DrawMode.OwnerDrawFixed;
                    animationName.BackColor = SystemColors.ControlDarkDark;
                    goto default;
                case 13:
                    wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
                    animationName.Items.Clear();
                    this.animationName.Items.AddRange(new object[] {
                        "Ally takes damage",
                        "Ally tries to run",
                        "?",
                    });
                    animationName.DrawMode = DrawMode.Normal;
                    animationName.BackColor = SystemColors.Window;
                    goto default;
                default:
                    if (animationCategory.SelectedIndex == 9)
                        animationName.Width = 400;
                    else
                        animationName.Width = 160;
                    animationName.DropDownWidth = animationName.Width;
                    animationNum.Maximum = animationScripts.Length - 1;
                    break;
            }
            this.animationName.SelectedIndex = 0;
            if (this.commandTree.Nodes.Count > 0)
                this.commandTree.SelectedNode = this.commandTree.Nodes[0];
        }

        private void RedrawTree()
        {
            commandTree.BeginUpdate();
            // redraw the treeview
            int fullParentIndex = commandTree.GetFullParentIndex();
            animationScripts[(int)animationNum.Value].RefreshScript();
            wrapper.ChangeScript(animationScripts[(int)animationNum.Value], false);
            // set the selected node
            wrapper.SelectNode(asc.InternalOffset, fullParentIndex);
            commandTree.EndUpdate();
        }
        //
        private void ControlDisassemble(AnimationCommand asc)
        {
            panelAniControls.SuspendDrawing();
            ResetControls();
            //
            this.Updating = true;
            switch (asc.Opcode)
            {
                case 0x00:
                    aniLabelA1.Text = "Sprite";
                    aniLabelA2.Text = "Sequence";
                    aniLabelB1.Text = "Priority";
                    aniLabelB2.Text = "VRAM address";
                    aniLabelC1.Text = "Palette Row #";
                    aniNameA1.Items.AddRange(Lists.Numerize(Lists.SpriteNames));
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 300;
                    aniNumA1.Maximum = 0x3FF; aniNumA1.Enabled = true;
                    aniNumA2.Maximum = 15; aniNumA2.Enabled = true;
                    aniNumB1.Maximum = 3; aniNumB1.Enabled = true;
                    aniNumC1.Maximum = 15; aniNumC1.Enabled = true;
                    aniNumB2.Hexadecimal = true; aniNumB2.Maximum = 0xFFFF; aniNumB2.Enabled = true;
                    aniBits.Items.AddRange(new object[]{
                        "overwrite VRAM",
                        "looping on",
                        "asc.Param2 & 0x10",
                        "overwrite palette",
                        "mirror",
                        "invert",
                        "behind all sprites",
                        "overlap all sprites"});
                    aniBits.Enabled = true;
                    aniNumA1.Value = aniNameA1.SelectedIndex = Bits.GetShort(asc.CommandData, 3) & 0x3FF;
                    aniNumA2.Value = asc.Param5;
                    aniNumB1.Value = (asc.Param6 & 0x30) >> 4;
                    aniNumB2.Value = Bits.GetShort(asc.CommandData, 7);
                    aniBits.SetItemChecked(0, (asc.Param1 & 0x01) == 0x01);
                    aniBits.SetItemChecked(1, (asc.Param2 & 0x08) == 0x08);
                    aniBits.SetItemChecked(2, (asc.Param2 & 0x10) == 0x10);
                    aniBits.SetItemChecked(3, (asc.Param2 & 0x20) == 0x20);
                    aniBits.SetItemChecked(4, (asc.Param6 & 0x40) == 0x40);
                    aniBits.SetItemChecked(5, (asc.Param6 & 0x80) == 0x80);
                    aniBits.SetItemChecked(6, (asc.Param1 & 0x40) == 0x40);
                    aniBits.SetItemChecked(7, (asc.Param1 & 0x80) == 0x80);
                    aniNumC1.Value = asc.Param6 & 0x0F;
                    break;
                case 0x01:
                case 0x0B:
                    aniLabelA1.Text = "Origin";
                    aniLabelB1.Text = "X coord";
                    aniLabelB2.Text = "Y coord";
                    aniLabelC1.Text = "Z coord";
                    aniNameA1.Items.AddRange(new object[]{
                        "absolute position",
                        "caster's initial position",
                        "target's current position",
                        "caster's current position"});
                    aniNameA1.Enabled = true;
                    aniNumB1.Enabled = true; aniNumB1.Minimum = -0x8000; aniNumB1.Maximum = 0x7FFF;
                    aniNumB2.Enabled = true; aniNumB2.Minimum = -0x8000; aniNumB2.Maximum = 0x7FFF;
                    aniNumC1.Enabled = true; aniNumC1.Minimum = -0x8000; aniNumC1.Maximum = 0x7FFF;
                    aniBits.Enabled = true;
                    aniBits.Items.AddRange(new object[] { "set X coord", "set Y coord", "set Z coord" });
                    aniNameA1.SelectedIndex = (int)(asc.Param1 >> 4);
                    aniNumB1.Value = (short)Bits.GetShort(asc.CommandData, 2);
                    aniNumB2.Value = (short)Bits.GetShort(asc.CommandData, 4);
                    aniNumC1.Value = (short)Bits.GetShort(asc.CommandData, 6);
                    aniBits.SetItemChecked(0, (asc.Param1 & 0x01) == 0x01);
                    aniBits.SetItemChecked(1, (asc.Param1 & 0x02) == 0x02);
                    aniBits.SetItemChecked(2, (asc.Param1 & 0x04) == 0x04);
                    break;
                case 0x03:
                    aniLabelA1.Text = "Sprite";
                    aniLabelB1.Text = "Sequence";
                    aniNameA1.Items.AddRange(Lists.Numerize(Lists.SpriteNames));
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 300;
                    aniNumA1.Maximum = 0x3FF; aniNumA1.Enabled = true;
                    aniNumB1.Maximum = 15; aniNumB1.Enabled = true;
                    aniBits.Items.AddRange(new object[]{
                        "store to VRAM",
                        "looping on",
                        "store palette",
                        "behind all sprites",
                        "overlap all sprites"});
                    aniBits.Enabled = true;
                    aniNumA1.Value = aniNameA1.SelectedIndex = Bits.GetShort(asc.CommandData, 3) & 0x3FF;
                    aniNumB1.Value = asc.Param5 & 15;
                    aniBits.SetItemChecked(0, (asc.Param1 & 0x01) == 0x01);
                    aniBits.SetItemChecked(1, (asc.Param2 & 0x08) == 0x08);
                    aniBits.SetItemChecked(2, (asc.Param2 & 0x20) == 0x20);
                    aniBits.SetItemChecked(3, (asc.Param1 & 0x40) == 0x40);
                    aniBits.SetItemChecked(4, (asc.Param1 & 0x80) == 0x80);
                    break;
                case 0x04:
                    aniLabelA1.Text = "Resume after";
                    aniLabelA2.Text = "# frames";
                    aniNameA1.Items.AddRange(new object[]{
                        "{00}","{01}","{02}","{03}","{04}","{05}",
                        "sprite shift complete",
                        "{07}",
                        "button press",
                        "{09}","{0A}","{0B}","{0C}","{0D}","{0E}","{0F}",
                        "# frames elapsed"});
                    aniNameA1.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1;
                    aniNumA2.Enabled = true; aniNumA2.Maximum = 0xFFFF;
                    aniNumA2.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0x08:
                    aniLabelA2.Text = "Speed";
                    aniLabelB1.Text = "Start position";
                    aniLabelB2.Text = "End position";
                    aniNumA2.Enabled = true; aniNumA2.Maximum = 0x7FFF; aniNumA2.Minimum = -0x8000;
                    aniNumB1.Enabled = true; aniNumB1.Maximum = 0x7FFF; aniNumB1.Minimum = -0x8000;
                    aniNumB2.Enabled = true; aniNumB2.Maximum = 0x7FFF; aniNumB2.Minimum = -0x8000;
                    aniBits.Items.AddRange(new object[]{
                        "apply to Z axis","apply to Y axis","apply to X axis",
                        "set start position","set end position","set speed"});
                    aniBits.Enabled = true;
                    aniNumA2.Value = (short)Bits.GetShort(asc.CommandData, 6);
                    aniNumB1.Value = (short)Bits.GetShort(asc.CommandData, 2);
                    aniNumB2.Value = (short)Bits.GetShort(asc.CommandData, 4);
                    aniBits.SetItemChecked(0, (asc.Param1 & 0x01) == 0x01);
                    aniBits.SetItemChecked(1, (asc.Param1 & 0x02) == 0x02);
                    aniBits.SetItemChecked(2, (asc.Param1 & 0x04) == 0x04);
                    aniBits.SetItemChecked(3, (asc.Param1 & 0x20) == 0x20);
                    aniBits.SetItemChecked(4, (asc.Param1 & 0x40) == 0x40);
                    aniBits.SetItemChecked(5, (asc.Param1 & 0x80) == 0x80);
                    break;
                case 0x09:
                case 0x10:
                case 0x50:
                case 0x51:
                case 0x64:
                    aniLabelB1.Text = "Address";
                    aniNumB1.Maximum = 0xFFFF; aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0x0C:
                    aniLabelA1.Text = "Type";
                    aniLabelB1.Text = "Speed";
                    aniLabelB2.Text = "Arch height";
                    aniNameA1.Enabled = true;
                    aniNameA1.Items.AddRange(new object[] { "{00}", "shift", "transfer", "{04}", "{08}" });
                    aniNumB1.Enabled = true; aniNumB1.Maximum = 0x7FFF; aniNumB1.Minimum = -0x8000;
                    aniNumB2.Enabled = true; aniNumB2.Maximum = 0x7FFF; aniNumB2.Minimum = -0x8000;
                    aniNameA1.SelectedIndex = asc.Param1 / 2;
                    aniNumB1.Value = (short)Bits.GetShort(asc.CommandData, 2);
                    aniNumB2.Value = (short)Bits.GetShort(asc.CommandData, 4);
                    break;
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x2F:
                    aniLabelA1.Text = "Variable type";
                    aniLabelB1.Text = "AMEM";
                    aniLabelB2.Text = "Variable";
                    aniNameA1.Items.AddRange(Interpreter.VariableNames);
                    aniNameA1.Enabled = true;
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Enabled = true; aniNumB1.Hexadecimal = true;
                    aniNumB2.Enabled = true;
                    aniNumB2.Hexadecimal = asc.Param1 >> 4 != 0;
                    switch (asc.Param1 >> 4)
                    {
                        case 0:
                            aniNumB2.Maximum = 0xFFFF;
                            aniNumB2.Value = Bits.GetShort(asc.CommandData, 2);
                            break;
                        case 1:
                        case 5:
                            aniNumB2.Maximum = 0x7EFFFF;
                            aniNumB2.Minimum = 0x7E0000;
                            aniNumB2.Value = Bits.GetShort(asc.CommandData, 2) + 0x7E0000;
                            break;
                        case 2:
                            aniNumB2.Maximum = 0x7FFFFF;
                            aniNumB2.Minimum = 0x7F0000;
                            aniNumB2.Value = Bits.GetShort(asc.CommandData, 2) + 0x7F0000;
                            break;
                        case 3:
                            aniNumB2.Minimum = 0x60;
                            aniNumB2.Maximum = 0x6F;
                            aniNumB2.Value = (asc.Param2 & 0x0F) + 0x60;
                            break;
                        case 4:
                        case 6:
                            aniNumB2.Maximum = 0xFF;
                            aniNumB2.Value = asc.Param2;
                            break;
                    }
                    aniNameA1.SelectedIndex = asc.Param1 >> 4;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    break;
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                    aniLabelA1.Text = "Variable type";
                    aniLabelA2.Text = "AMEM";
                    aniLabelB1.Text = "Variable";
                    aniLabelB2.Text = "Jump to";
                    aniNameA1.Items.AddRange(Interpreter.VariableNames);
                    aniNameA1.Enabled = true;
                    aniNumA2.Minimum = 0x60; aniNumA2.Maximum = 0x6F;
                    aniNumA2.Enabled = true; aniNumA2.Hexadecimal = true;
                    aniNumB1.Enabled = true; aniNumB1.Hexadecimal = asc.Param1 >> 4 != 0;
                    switch (asc.Param1 >> 4)
                    {
                        case 0:
                            aniNumB1.Maximum = 0xFFFF;
                            aniNumB1.Value = Bits.GetShort(asc.CommandData, 2);
                            break;
                        case 1:
                        case 5:
                            aniNumB1.Maximum = 0x7EFFFF;
                            aniNumB1.Minimum = 0x7E0000;
                            aniNumB1.Value = Bits.GetShort(asc.CommandData, 2) + 0x7E0000;
                            break;
                        case 2:
                            aniNumB1.Maximum = 0x7FFFFF;
                            aniNumB1.Minimum = 0x7F0000;
                            aniNumB1.Value = Bits.GetShort(asc.CommandData, 2) + 0x7F0000;
                            break;
                        case 3:
                            aniNumB1.Minimum = 0x60;
                            aniNumB1.Maximum = 0x6F;
                            aniNumB1.Value = (asc.Param2 & 0x0F) + 0x60;
                            break;
                        case 4:
                        case 6:
                            aniNumB1.Maximum = 0xFF;
                            aniNumB1.Value = asc.Param2;
                            break;
                    }
                    aniNumB2.Maximum = 0xFFFF; aniNumB2.Enabled = true; aniNumB2.Hexadecimal = true;
                    aniNameA1.SelectedIndex = asc.Param1 >> 4;
                    aniNumA2.Value = (asc.Param1 & 0x0F) + 0x60;
                    aniNumB2.Value = Bits.GetShort(asc.CommandData, 4);
                    break;
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                    aniLabelB1.Text = "AMEM";
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    break;
                case 0x36:
                case 0x37:
                    aniLabelB1.Text = "AMEM";
                    aniTitleD.Text = "Bits";
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniBits.Items.AddRange(new object[] { "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7" });
                    aniBits.Enabled = true;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param2 & i) == i);
                    break;
                case 0x38:
                case 0x39:
                    aniLabelB1.Text = "AMEM";
                    aniTitleD.Text = "Bits";
                    aniLabelC1.Text = "Jump to";
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniBits.Items.AddRange(new object[] { "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7" });
                    aniBits.Enabled = true;
                    aniNumC1.Maximum = 0xFFFF; aniNumC1.Enabled = true; aniNumC1.Hexadecimal = true;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param2 & i) == i);
                    aniNumC1.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0x40:
                case 0x41:
                    aniLabelB1.Text = "AMEM";
                    aniTitleD.Text = "Bits";
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Enabled = true; aniNumB1.Hexadecimal = true;
                    aniBits.Items.AddRange(new object[] { "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7" });
                    aniBits.Enabled = true;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param2 & i) == i);
                    break;
                case 0x43:
                    aniLabelB1.Text = "Sequence";
                    aniNumB1.Maximum = 0x0F; aniNumB1.Enabled = true;
                    aniBits.Items.AddRange(new object[] { "looping on", "looping off", "b6", "mirror" });
                    aniBits.Enabled = true;
                    aniNumB1.Value = asc.Param1 & 0x0F;
                    for (int i = 0x10, j = 0; j < 4; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param1 & i) == i);
                    break;
                case 0x5D:
                    aniLabelB1.Text = "Object #";
                    aniLabelB2.Text = "Address";
                    aniNumB1.Maximum = 0x0F; aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB2.Maximum = 0xFFFF; aniNumB2.Hexadecimal = true; aniNumB2.Enabled = true;
                    aniBits.Items.AddRange(new object[] { 
                        "b0", "b1", "b2", "character slot", 
                        "b4", "b5", "current target", "b7" });
                    aniBits.Enabled = true;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param1 & i) == i);
                    aniNumB1.Value = asc.Param2;
                    aniNumB2.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0x63:
                    aniLabelA1.Text = "Type";
                    aniNameA1.Items.AddRange(new object[] { "attack name", "spell name", "item name", "???", "???", "???"});
                    aniNameA1.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0x68:
                    aniLabelB1.Text = "Address";
                    aniLabelB2.Text = "Index";
                    aniNumB1.Hexadecimal = true; aniNumB1.Maximum = 0xFFFF; aniNumB1.Enabled = true;
                    aniNumB2.Maximum = 255; aniNumB2.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    aniNumB2.Value = asc.CommandData[3];
                    break;
                case 0x6A:
                case 0x6B:
                    aniLabelB1.Text = "Memory";
                    aniLabelB2.Text = "Value";
                    aniNumB1.Minimum = 0x60; aniNumB1.Maximum = 0x6F;
                    aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB2.Maximum = asc.Opcode == 0x6A ? 0xFF : 0xFFFF; aniNumB2.Enabled = true;
                    aniNumB1.Value = (asc.Param1 & 0x0F) + 0x60;
                    aniNumB2.Value = asc.Opcode == 0x6A ? asc.Param2 : Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0x72:
                    aniLabelA1.Text = "Effect";
                    aniNameA1.Items.AddRange(Lists.Numerize(Lists.EffectNames));
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 250;
                    aniBits.Items.AddRange(new object[]{
                        "looping on","playback off","looping off","b3"});
                    aniBits.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param2;
                    for (int i = 1, j = 0; j < 4; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param1 & i) == i);
                    break;
                case 0x74:
                    aniLabelA1.Text = "Pause until";
                    aniNameA1.Items.AddRange(new object[]{
                        "sequence complete (4bpp)",
                        "sequence complete (2bpp)",
                        "fade in complete",
                        "fade complete (4bpp)",
                        "fade complete (2bpp)"});
                    aniNameA1.Enabled = true;
                    switch (Bits.GetShort(asc.CommandData, 1))
                    {
                        case 0x0004:
                            aniNameA1.SelectedIndex = 0; break;
                        case 0x0008:
                            aniNameA1.SelectedIndex = 1; break;
                        case 0x0200:
                            aniNameA1.SelectedIndex = 2; break;
                        case 0x0400:
                            aniNameA1.SelectedIndex = 3; break;
                        case 0x0800:
                            aniNameA1.SelectedIndex = 4; break;
                        default:
                            break;
                    }
                    break;
                case 0x75:
                    aniLabelB1.Text = "Bits";
                    aniNumB1.Maximum = 0xFFFF; aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0x77:
                case 0x78:
                    aniLabelA1.Text = "Overlap";
                    aniNameA1.Items.AddRange(new object[] { 
                        "transparency off", "overlap all", "overlap none", "overlap all except allies" });
                    aniNameA1.Enabled = true;
                    aniBits.Items.AddRange(new object[]{
                        "b0","4bpp","2bpp","invisible"});
                    aniBits.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1 >> 4;
                    for (int i = 1, j = 0; j < 4; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param1 & i) == i);
                    break;
                case 0x7A:
                    aniLabelA1.Text = "Type";
                    aniLabelA2.Text = "Dialogue #";
                    aniNameA1.Items.AddRange(new object[] { "battle dialogue", "psychopath message", "battle message" });
                    aniNameA1.Enabled = true;
                    aniNumA2.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1 & 3;
                    aniNumA2.Value = asc.Param2;
                    break;
                case 0x96:
                    aniLabelA1.Text = "Message #";
                    aniLabelB1.Text = "X";
                    aniLabelB2.Text = "Y";
                    aniNumA1.Enabled = true;
                    aniNumB1.Enabled = true;
                    aniNumB2.Enabled = true;
                    aniNumB1.Maximum = 127; aniNumB1.Minimum = -128;
                    aniNumB2.Maximum = 127; aniNumB2.Minimum = -128;
                    aniNumA1.Value = asc.Param2;
                    aniNumB1.Value = (sbyte)asc.CommandData[3];
                    aniNumB2.Value = (sbyte)asc.Param4;
                    break;
                case 0x7E:
                    aniLabelB1.Text = "Duration";
                    aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param1;
                    break;
                case 0x80:
                    aniLabelA1.Text = "Type";
                    aniLabelB1.Text = "Color count";
                    aniLabelB2.Text = "Starting color index";
                    aniLabelC1.Text = "Glow duration";
                    //
                    aniNameA1.Enabled = true;
                    aniNameA1.Items.AddRange(new string[] { "eastward reflection", "westward reflection" });
                    aniNumB1.Maximum = 15; aniNumB1.Enabled = true;
                    aniNumB2.Maximum = 15; aniNumB2.Enabled = true;
                    aniNumC1.Enabled = true;
                    //
                    aniNameA1.SelectedIndex = asc.Param1 & 0x01;
                    aniNumB1.Value = asc.Param2 & 0x0F;
                    aniNumB2.Value = asc.Param2 >> 4;
                    aniNumC1.Value = asc.Param3;
                    break;
                case 0x85:
                    aniLabelA1.Text = "Type";
                    aniLabelA2.Text = "Object";
                    aniLabelB1.Text = "Duration";
                    aniNameA1.Items.AddRange(new object[] { "fade out", "fade in" }); aniNameA1.Enabled = true;
                    aniNameA2.Items.AddRange(new object[] { "effect", "sprite", "screen" }); aniNameA2.Enabled = true;
                    aniNumB1.Enabled = true;
                    aniNameA1.SelectedIndex = (asc.Param1 & 0x0F) >> 1;
                    aniNameA2.SelectedIndex = asc.Param1 >> 4;
                    aniNumB1.Value = asc.Param2;
                    break;
                case 0x86:
                    aniLabelA1.Text = "Object";
                    aniLabelB1.Text = "Amount";
                    aniLabelB2.Text = "Speed";
                    aniNameA1.Enabled = true;
                    aniNameA1.Items.AddRange(new string[] { "none", "screen", "sprites", "...", "all" });
                    aniNumB1.Enabled = true;
                    aniNumB2.Enabled = true; aniNumB2.Maximum = 256;
                    //
                    aniNameA1.SelectedIndex = asc.Param1;
                    aniNumB1.Value = asc.Param4;
                    aniNumB2.Value = Bits.GetShort(asc.CommandData, 5);
                    break;
                case 0x8E:
                case 0x8F:
                    aniLabelA1.Text = "Color";
                    aniLabelB1.Text = "Duration";
                    aniNameA1.Items.AddRange(new object[] { 
                        "{none}", "red", "green", "yellow", "blue", "pink", "aqua", "white" });
                    aniNameA1.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1 & 0x07;
                    if (asc.Opcode == 0x8E)
                    {
                        aniNumB1.Enabled = true;
                        aniNumB1.Value = asc.Param2;
                    }
                    break;
                case 0xA3:
                    aniLabelA1.Text = "Effect";
                    aniNameA1.Items.AddRange(Interpreter.ScreenEffects);
                    aniNameA1.Enabled = true;
                    //
                    aniNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0xAB:
                case 0xAE:
                    aniLabelA1.Text = "Sound";
                    aniNameA1.Items.AddRange(Lists.BattleSoundNames);
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 250;
                    aniNameA1.SelectedIndex = asc.Param1;
                    aniNumA1.Maximum = 0xD2; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param1;
                    break;
                case 0xB0:
                    aniLabelA1.Text = "Music";
                    aniNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                    aniNameA1.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1;
                    aniNumA1.Maximum = 0x49; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param1;
                    break;
                case 0xB1:
                    aniLabelA1.Text = "Music";
                    aniNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 250;
                    aniNumA1.Maximum = 0x49; aniNumA1.Enabled = true;
                    aniNumB1.Enabled = true; aniNumB1.Maximum = 0xFFFF;
                    aniNameA1.SelectedIndex = asc.Param1;
                    aniNumA1.Value = asc.Param1;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xB6:
                    aniLabelB1.Text = "Speed";
                    aniLabelB2.Text = "Volume";
                    aniNumB1.Enabled = true;
                    aniNumB2.Enabled = true;
                    aniNumB1.Value = asc.Param1;
                    aniNumB2.Value = asc.Param2;
                    break;
                case 0xBB:
                    aniLabelA1.Text = "Target";
                    aniNameA1.Items.AddRange(Lists.TargetNames);
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 200;
                    aniNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0xBC:
                case 0xBD:
                    aniLabelA1.Text = "Item";
                    aniNameA1.Items.AddRange(Model.ItemNames.Names);
                    aniNameA1.Enabled = true;
                    aniNameA1.BackColor = SystemColors.ControlDarkDark;
                    aniNameA1.DrawMode = DrawMode.OwnerDrawFixed;
                    aniNameA1.ItemHeight = 15;
                    aniNumA1.Maximum = 0xFF; aniNumA1.Enabled = true;
                    aniBits.Items.Add("remove"); aniBits.Enabled = true;
                    aniNameA1.SelectedIndex = Model.ItemNames.GetSortedIndex(
                        Math.Abs((short)Bits.GetShort(asc.CommandData, 1)));
                    aniNumA1.Value = Math.Abs((short)Bits.GetShort(asc.CommandData, 1));
                    aniBits.SetItemChecked(0, asc.Param2 == 0xFF);
                    break;
                case 0xBE:
                    aniLabelB1.Text = "Value";
                    aniNumB1.Maximum = 0xFFFF; aniNumB1.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xBF:
                    aniLabelA1.Text = "Target";
                    aniNameA1.Items.AddRange(Lists.TargetNames);
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 200;
                    aniNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0xC3:
                    aniLabelA1.Text = "Mask";
                    aniNameA1.Items.AddRange(new string[] { 
                        "...", "incline", "incline", "circle", "dome", 
                        "polygon", "wavy circle", "cylinder" });
                    aniNameA1.Enabled = true;
                    aniNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0xCB:
                    aniLabelB1.Text = "Speed";
                    aniNumB1.Maximum = 15; aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param1;
                    break;
                case 0xCE:
                    aniLabelA1.Text = "Start Frame";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param2;
                    //
                    aniLabelA2.Text = "End Frame";
                    aniNumA2.Maximum = 255; aniNumA2.Enabled = true;
                    aniNumA2.Value = asc.Param1;
                    //
                    aniLabelB1.Text = "Start Just-OK";
                    aniNumB1.Maximum = 255; aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param3;
                    //
                    aniLabelB2.Text = "End Just-OK/Start Perfect";
                    aniNumB2.Maximum = 255; aniNumB2.Enabled = true;
                    aniNumB2.Value = asc.Param4;
                    //
                    aniLabelC1.Text = "End Perfect";
                    aniNumC1.Maximum = 255; aniNumC1.Enabled = true;
                    aniNumC1.Value = asc.Param5;
                    //
                    aniLabelC2.Text = "If not timed, Jump to Address";
                    aniNumC2.Maximum = 0xFFFF; aniNumC2.Hexadecimal = true; aniNumC2.Enabled = true;
                    aniNumC2.Value = Bits.GetShort(asc.CommandData, 6);
                    break;
                case 0xCF:
                    aniLabelA1.Text = "Start Frame";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param2;
                    //
                    aniLabelA2.Text = "End Frame";
                    aniNumA2.Maximum = 255; aniNumA2.Enabled = true;
                    aniNumA2.Value = asc.Param1;
                    //
                    aniLabelB1.Text = "End of Timing";
                    aniNumB1.Maximum = 255; aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param3;
                    //
                    aniLabelB2.Text = "If not timed, Jump to Address";
                    aniNumB2.Maximum = 0xFFFF; aniNumB2.Hexadecimal = true; aniNumB2.Enabled = true;
                    aniNumB2.Value = Bits.GetShort(asc.CommandData, 4);
                    break;
                case 0xD0:
                    aniLabelA1.Text = "Frames before jump";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param1;
                    //
                    aniLabelB1.Text = "Jump to Address";
                    aniNumB1.Maximum = 0xFFFF; aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xD2:
                    aniLabelA1.Text = "# of Possible Power-Ups";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param1;
                    break;
                case 0xD3:
                    aniLabelA1.Text = "Start Frame";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param2;
                    //
                    aniLabelA2.Text = "End Frame";
                    aniNumA2.Maximum = 255; aniNumA2.Enabled = true;
                    aniNumA2.Value = asc.Param1;
                    //
                    aniLabelB1.Text = "# of Possible Power-Ups";
                    aniNumB1.Maximum = 255; aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param3;
                    break;
                case 0xD4:
                    aniLabelA1.Text = "Level 1 End";
                    aniNumA1.Maximum = 255; aniNumA1.Enabled = true;
                    aniNumA1.Value = asc.Param1;
                    //
                    aniLabelA2.Text = "Level 2 End";
                    aniNumA2.Maximum = 255; aniNumA2.Enabled = true;
                    aniNumA2.Value = asc.Param2;
                    //
                    aniLabelB1.Text = "Level 3 End";
                    aniNumB1.Maximum = 255; aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param3;
                    //
                    aniLabelB2.Text = "Level 4 End";
                    aniNumB2.Maximum = 255; aniNumB2.Enabled = true;
                    aniNumB2.Value = asc.Param4;
                    //
                    aniLabelC1.Text = "Over-Charged End";
                    aniNumC1.Maximum = 255; aniNumC1.Enabled = true;
                    aniNumC1.Value = asc.Param5;
                    break;
                case 0xD5:
                    aniLabelA1.Text = "Monster";
                    aniNumA1.Value = asc.Param2;
                    aniNameA1.Items.AddRange(Model.MonsterNames.Names);
                    aniNameA1.Enabled = true; aniNameA1.DropDownWidth = 300;
                    aniNumA1.Maximum = 0xFF; aniNumA1.Enabled = true;
                    aniNumA1.Value = aniNameA1.SelectedIndex = asc.Param2;
                    aniNameA1.SelectedIndex = asc.Param2;
                    aniNameA1.SelectedIndex = Model.MonsterNames.GetSortedIndex(asc.Param2);
                    aniNumA1.Value = Math.Abs(asc.Param2);
                    //
                    aniLabelB1.Text = "Formation position";
                    aniLabelB2.Text = "#";
                    aniNumB1.Maximum = 0x07;
                    aniNumB1.Enabled = true;
                    aniNumB1.Value = asc.Param3;
                    //
                    aniTitleD.Text = "Bits";
                    aniNumC1.Value = asc.Param1;
                    aniNumC1.Hexadecimal = true; aniNumC1.Enabled = true;
                    aniBits.Items.AddRange(new object[] { "b0", "b1", "b2", "b3", "b4", "b5", "b6", "b7" });
                    aniBits.Enabled = true;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        aniBits.SetItemChecked(j, (asc.Param1 & i) == i);
                    break;
                case 0xD8:
                    aniLabelB1.Text = "Jump to Address";
                    aniNumB1.Maximum = 0xFFFF; aniNumB1.Hexadecimal = true; aniNumB1.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xE1:
                    aniLabelB1.Text = "Event #";
                    aniLabelB2.Text = "Offset";
                    aniNumB1.Maximum = 0xFFFF;
                    aniNumB1.Enabled = true;
                    aniNumB2.Enabled = true;
                    aniNumB1.Value = Bits.GetShort(asc.CommandData, 1);
                    aniNumB2.Value = asc.Param2;
                    break;
            }
            OrganizeControls();
            commands.SelectedIndex = -1;
            //
            panelAniControls.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssemble()
        {
            switch (asc.Opcode)
            {
                case 0x00:
                    Bits.SetShort(asc.CommandData, 3, (ushort)aniNumA1.Value);
                    asc.Param5 = (byte)aniNumA2.Value;
                    asc.Param6 = (byte)aniNumC1.Value;
                    asc.Param6 |= (byte)((byte)aniNumB1.Value << 4);
                    Bits.SetShort(asc.CommandData, 7, (ushort)aniNumB2.Value);
                    Bits.SetBit(asc.CommandData, 1, 0, aniBits.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 2, 3, aniBits.GetItemChecked(1));
                    Bits.SetBit(asc.CommandData, 2, 4, aniBits.GetItemChecked(2));
                    Bits.SetBit(asc.CommandData, 2, 5, aniBits.GetItemChecked(3));
                    Bits.SetBit(asc.CommandData, 6, 6, aniBits.GetItemChecked(4));
                    Bits.SetBit(asc.CommandData, 6, 7, aniBits.GetItemChecked(5));
                    Bits.SetBit(asc.CommandData, 1, 6, aniBits.GetItemChecked(6));
                    Bits.SetBit(asc.CommandData, 1, 7, aniBits.GetItemChecked(7));
                    break;
                case 0x01:
                case 0x0B:
                    asc.Param1 = (byte)(aniNameA1.SelectedIndex << 4);
                    Bits.SetBit(asc.CommandData, 1, 0, aniBits.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 1, 1, aniBits.GetItemChecked(1));
                    Bits.SetBit(asc.CommandData, 1, 2, aniBits.GetItemChecked(2));
                    Bits.SetShort(asc.CommandData, 2, (ushort)((short)aniNumB1.Value));
                    Bits.SetShort(asc.CommandData, 4, (ushort)((short)aniNumB2.Value));
                    Bits.SetShort(asc.CommandData, 6, (ushort)((short)aniNumC1.Value));
                    break;
                case 0x03:
                    Bits.SetShort(asc.CommandData, 3, (ushort)aniNumA1.Value);
                    asc.Param5 = (byte)aniNumB1.Value;
                    Bits.SetBit(asc.CommandData, 1, 0, aniBits.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 2, 3, aniBits.GetItemChecked(1));
                    Bits.SetBit(asc.CommandData, 2, 5, aniBits.GetItemChecked(2));
                    break;
                case 0x04:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    Bits.SetShort(asc.CommandData, 2, (ushort)aniNumA2.Value);
                    break;
                case 0x08:
                    Bits.SetBit(asc.CommandData, 1, 0, aniBits.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 1, 1, aniBits.GetItemChecked(1));
                    Bits.SetBit(asc.CommandData, 1, 2, aniBits.GetItemChecked(2));
                    Bits.SetBit(asc.CommandData, 1, 5, aniBits.GetItemChecked(3));
                    Bits.SetBit(asc.CommandData, 1, 6, aniBits.GetItemChecked(4));
                    Bits.SetBit(asc.CommandData, 1, 7, aniBits.GetItemChecked(5));
                    Bits.SetShort(asc.CommandData, 2, (ushort)((short)aniNumB1.Value));
                    Bits.SetShort(asc.CommandData, 4, (ushort)((short)aniNumB2.Value));
                    Bits.SetShort(asc.CommandData, 6, (ushort)((short)aniNumA2.Value));
                    break;
                case 0x09:
                case 0x10:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    break;
                case 0x1A:
                case 0x1B:
                    asc.Param1 = 0x01;
                    break;
                case 0x50:
                case 0x51:
                case 0x64:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    break;
                case 0x0C:
                    asc.Param1 = (byte)(aniNameA1.SelectedIndex * 2);
                    Bits.SetShort(asc.CommandData, 2, (ushort)((short)aniNumB1.Value));
                    Bits.SetShort(asc.CommandData, 4, (ushort)((short)aniNumB2.Value));
                    break;
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x2F:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    asc.Param1 |= (byte)(aniNameA1.SelectedIndex << 4);
                    switch (asc.Param1 >> 4)
                    {
                        case 0:
                        case 4:
                        case 6:
                            Bits.SetShort(asc.CommandData, 2, (ushort)aniNumB2.Value);
                            break;
                        case 1:
                        case 5:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB2.Value - 0x7E0000));
                            break;
                        case 2:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB2.Value - 0x7F0000));
                            break;
                        case 3:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB2.Value - 0x60));
                            break;
                    }
                    break;
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                    asc.Param1 = (byte)(aniNumA2.Value - 0x60);
                    asc.Param1 |= (byte)(aniNameA1.SelectedIndex << 4);
                    switch (asc.Param1 >> 4)
                    {
                        case 0:
                        case 4:
                        case 6:
                            Bits.SetShort(asc.CommandData, 2, (ushort)aniNumB1.Value);
                            break;
                        case 1:
                        case 5:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB1.Value - 0x7E0000));
                            break;
                        case 2:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB1.Value - 0x7F0000));
                            break;
                        case 3:
                            Bits.SetShort(asc.CommandData, 2, (ushort)(aniNumB1.Value - 0x60));
                            break;
                    }
                    Bits.SetShort(asc.CommandData, 4, (ushort)aniNumB2.Value);
                    break;
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                case 0x35:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    break;
                case 0x7E:
                    asc.Param1 = (byte)aniNumB1.Value; break;
                case 0x36:
                case 0x37:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 2, j, aniBits.GetItemChecked(j));
                    break;
                case 0x38:
                case 0x39:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 2, j, aniBits.GetItemChecked(j));
                    Bits.SetShort(asc.CommandData, 3, (ushort)aniNumC1.Value);
                    break;
                case 0x40:
                case 0x41:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 2, j, aniBits.GetItemChecked(j));
                    break;
                case 0x43:
                    asc.Param1 = (byte)aniNumB1.Value;
                    for (int i = 0, j = 0; j < 4; i++, j++)
                        Bits.SetBit(asc.CommandData, 1, j + 4, aniBits.GetItemChecked(j));
                    break;
                case 0x5D:
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 1, j, aniBits.GetItemChecked(j));
                    asc.Param2 = (byte)aniNumB1.Value;
                    Bits.SetShort(asc.CommandData, 3, (ushort)aniNumB2.Value);
                    break;
                case 0x63:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0x68:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    asc.CommandData[3] = (byte)aniNumB2.Value;
                    break;
                case 0x6A:
                case 0x6B:
                    asc.Param1 = (byte)(aniNumB1.Value - 0x60);
                    if (asc.Opcode == 0x6B)
                        Bits.SetShort(asc.CommandData, 2, (ushort)aniNumB2.Value);
                    else
                        asc.Param2 = (byte)aniNumB2.Value;
                    break;
                case 0x72:
                    asc.Param2 = (byte)aniNameA1.SelectedIndex;
                    for (int i = 1, j = 0; j < 4; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 1, j, aniBits.GetItemChecked(j));
                    break;
                case 0x74:
                    switch (aniNameA1.SelectedIndex)
                    {
                        case 0:
                            Bits.SetShort(asc.CommandData, 1, 0x0004); break;
                        case 1:
                            Bits.SetShort(asc.CommandData, 1, 0x0008); break;
                        case 2:
                            Bits.SetShort(asc.CommandData, 1, 0x0200); break;
                        case 3:
                            Bits.SetShort(asc.CommandData, 1, 0x0400); break;
                        case 4:
                            Bits.SetShort(asc.CommandData, 1, 0x0800); break;
                        default:
                            break;
                    }
                    break;
                case 0x75:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    break;
                case 0x77:
                case 0x78:
                    asc.Param1 = (byte)(aniNameA1.SelectedIndex << 4);
                    for (int i = 1, j = 0; j < 4; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 1, j, aniBits.GetItemChecked(j));
                    break;
                case 0x7A:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    asc.Param2 = (byte)aniNumA2.Value;
                    break;
                case 0x80:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    asc.Param2 = (byte)aniNumB1.Value;
                    asc.Param2 |= (byte)((byte)aniNumB2.Value << 4);
                    asc.Param3 = (byte)aniNumC1.Value;
                    break;
                case 0x96:
                    asc.Param2 = (byte)aniNumA1.Value;
                    asc.CommandData[3] = (byte)((sbyte)aniNumB1.Value);
                    asc.Param4 = (byte)((sbyte)aniNumB2.Value);
                    break;
                case 0x85:
                    asc.Param1 = (byte)(aniNameA1.SelectedIndex << 1);
                    asc.Param1 |= (byte)(aniNameA2.SelectedIndex << 4);
                    asc.Param2 = (byte)aniNumB1.Value;
                    break;
                case 0x86:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    asc.Param4 = (byte)aniNumB1.Value;
                    Bits.SetShort(asc.CommandData, 5, (ushort)aniNumB2.Value);
                    break;
                case 0x8E:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    asc.Param2 = (byte)aniNumB1.Value;
                    break;
                case 0xA3:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0xAB:
                case 0xAE:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0xB0:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0xB1:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    Bits.SetShort(asc.CommandData, 2, (ushort)aniNumB1.Value);
                    break;
                case 0xB6:
                    asc.Param1 = (byte)aniNumB1.Value;
                    asc.Param2 = (byte)aniNumB2.Value;
                    break;
                case 0xBB:
                case 0xBF:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0xBC:
                case 0xBD:
                    short temp = (short)(-(ushort)aniNumA1.Value);
                    if (aniBits.GetItemChecked(0))
                        Bits.SetShort(asc.CommandData, 1, (ushort)temp);
                    else
                        Bits.SetShort(asc.CommandData, 1, (ushort)aniNumA1.Value);
                    break;
                case 0xBE:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    break;
                case 0xC3:
                    asc.Param1 = (byte)aniNameA1.SelectedIndex;
                    break;
                case 0xCB:
                    asc.Param1 = (byte)aniNumB1.Value;
                    break;
                case 0xCE:
                    asc.Param1 = (byte)aniNumA2.Value;
                    asc.Param2 = (byte)aniNumA1.Value;
                    asc.Param3 = (byte)aniNumB1.Value;
                    asc.Param4 = (byte)aniNumB2.Value;
                    asc.Param5 = (byte)aniNumC1.Value;
                    Bits.SetShort(asc.CommandData, 6, (ushort)aniNumC2.Value);
                    break;
                case 0xCF:
                    asc.Param1 = (byte)aniNumA2.Value;
                    asc.Param2 = (byte)aniNumA1.Value;
                    asc.Param3 = (byte)aniNumB1.Value;
                    Bits.SetShort(asc.CommandData, 4, (ushort)aniNumB2.Value);
                    break;
                case 0xD0:
                case 0xD1:
                    asc.Param1 = (byte)aniNumA1.Value;
                    Bits.SetShort(asc.CommandData, 2, (ushort)aniNumB1.Value);
                    break;
                case 0xD5:
                    asc.Param1 = (byte)(aniNumB1.Value);
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        Bits.SetBit(asc.CommandData, 1, j, aniBits.GetItemChecked(j));
                    asc.Param2 = (byte)aniNumA1.Value;
                    asc.Param3 = (byte)aniNumB1.Value;
                    break;
                case 0xD8:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    break;
                case 0xE1:
                    Bits.SetShort(asc.CommandData, 1, (ushort)aniNumB1.Value);
                    asc.CommandData[3] = (byte)aniNumB2.Value;
                    break;
            }
        }
        private void ResetControls()
        {
            this.Updating = true;
            aniNameA1.DrawMode = DrawMode.Normal; aniNameA1.ItemHeight = 13; aniNameA1.BackColor = SystemColors.Window;
            aniNameA1.Items.Clear(); aniNameA1.ResetText(); aniNameA1.Enabled = false; aniNameA1.DropDownWidth = aniNameA1.Width;
            aniNameA2.Items.Clear(); aniNameA2.ResetText(); aniNameA2.Enabled = false; aniNameA2.DropDownWidth = aniNameA2.Width;
            aniNumA1.Maximum = 255; aniNumA1.Hexadecimal = false; aniNumA1.Minimum = 0; aniNumA1.Value = 0; aniNumA1.Enabled = false;
            aniNumA2.Maximum = 255; aniNumA2.Hexadecimal = false; aniNumA2.Minimum = 0; aniNumA2.Value = 0; aniNumA2.Enabled = false;
            aniNumB1.Maximum = 255; aniNumB1.Hexadecimal = false; aniNumB1.Minimum = 0; aniNumB1.Increment = 1; aniNumB1.Value = 0; aniNumB1.Enabled = false;
            aniNumB2.Maximum = 255; aniNumB2.Hexadecimal = false; aniNumB2.Minimum = 0; aniNumB2.Increment = 1; aniNumB2.Value = 0; aniNumB2.Enabled = false;
            aniNumC1.Maximum = 255; aniNumC1.Hexadecimal = false; aniNumC1.Value = 0; aniNumC1.Enabled = false;
            aniNumC2.Maximum = 255; aniNumC2.Value = 0; aniNumC2.Enabled = false;
            aniBits.ColumnWidth = 134; aniBits.Items.Clear(); aniBits.Enabled = false;
            aniTitleA.Text = "";
            aniTitleB.Text = "";
            aniTitleC.Text = "";
            aniTitleD.Text = "";
            aniLabelA1.Text = "";
            aniLabelA2.Text = "";
            aniLabelB1.Text = "";
            aniLabelB2.Text = "";
            aniLabelC1.Text = "";
            aniLabelC2.Text = "";
            this.Updating = false;
        }
        private void OrganizeControls()
        {
            aniTitleA.Visible = aniTitleA.Enabled =
                aniTitleA.Text != "" ||
                aniLabelA1.Text != "" ||
                aniLabelA2.Text != "";
            aniTitleB.Visible = aniTitleB.Enabled =
                aniTitleB.Text != "" ||
                aniLabelB1.Text != "" ||
                aniLabelB2.Text != "";
            aniTitleC.Visible = aniTitleC.Enabled =
                aniTitleC.Text != "" ||
                aniLabelC1.Text != "" ||
                aniLabelC2.Text != "";
            aniTitleD.Visible = aniTitleD.Enabled =
                aniTitleD.Text != "" ||
                aniBits.Items.Count > 0;
            aniPanelA1.Visible = aniNumA1.Enabled || aniNameA1.Enabled;
            aniPanelA2.Visible = aniNumA2.Enabled || aniNameA2.Enabled;
            aniPanelB1.Visible = aniNumB1.Enabled;
            aniPanelB2.Visible = aniNumB2.Enabled;
            aniPanelC1.Visible = aniNumC1.Enabled;
            aniPanelC2.Visible = aniNumC2.Enabled;
            aniNameA1.Visible = aniNameA1.Enabled;
            aniNameA2.Visible = aniNameA2.Enabled;
            aniNumA1.Visible = aniNumA1.Enabled;
            aniNumA2.Visible = aniNumA2.Enabled;
            if (aniBits.Items.Count < 8)
                aniBits.Height = aniBits.Items.Count * 16 + 4;
            else
                aniBits.Height = 8 * 16 + 4;
            //
            aniTitleA.BringToFront();
            aniTitleB.BringToFront();
            aniTitleC.BringToFront();
            aniTitleD.BringToFront();
            panel1.BringToFront();
            aniPanelA1.BringToFront();
            aniPanelA2.BringToFront();
            aniPanelB1.BringToFront();
            aniPanelB2.BringToFront();
            aniPanelC1.BringToFront();
            aniPanelC2.BringToFront();
            aniLabelA1.BringToFront();
            aniNameA1.BringToFront();
            aniNumA1.BringToFront();
            aniLabelA2.BringToFront();
            aniNameA2.BringToFront();
            aniNumA2.BringToFront();
            //
            if (aniTitleA.Enabled)
                aniTitleA.Text = Lists.AnimationCommands[asc.Opcode];
            else if (aniTitleB.Enabled)
                aniTitleB.Text = Lists.AnimationCommands[asc.Opcode];
            else if (aniTitleC.Enabled)
                aniTitleC.Text = Lists.AnimationCommands[asc.Opcode];
            else if (aniTitleD.Enabled)
                aniTitleD.Text = Lists.AnimationCommands[asc.Opcode];
        }
        private void UpdateCommandData()
        {
        }
        private void NotEnoughSpace()
        {
            MessageBox.Show("Not enough space to replace the selected command(s) with the new command.\n\n" +
                "Try selecting a smaller command from the list or selecting an earlier command within the routine in the tree.",
                "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
            commands.SelectedIndex = -1;
        }
        //
        public void Assemble()
        {
            this.Modified = false;
        }
        #endregion
        #region Event Handlers
        private void animationCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            previewer.Enabled =
                animationCategory.SelectedIndex == 1 ||
                animationCategory.SelectedIndex == 2 ||
                animationCategory.SelectedIndex == 4 ||
                animationCategory.SelectedIndex == 10;
            // so it won't try to load the label editor if we haven't loaded the battle events
            labelWindow.Disable = animationCategory.SelectedIndex != 12;
            if (this.Updating)
                return;
            animationNum.Value = 0;
            RefreshEditor();
            //
            settings.LastAnimationCat = animationCategory.SelectedIndex;
            settings.LastAnimation = (int)animationNum.Value;
        }
        private void animationNum_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            animationScripts[(int)animationNum.Value].RefreshScript();
            //
            if (animationCategory.SelectedIndex == 2)
            {
                animationName.SelectedIndex = Model.AttackNames.GetSortedIndex(Index);
                wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
            }
            else
            {
                animationName.SelectedIndex = Index;
                wrapper.ChangeScript(animationScripts[(int)animationNum.Value]);
            }
            if (this.commandTree.Nodes.Count > 0)
                this.commandTree.SelectedNode = this.commandTree.Nodes[0];
            Cursor.Current = Cursors.Arrow;
            //
            settings.LastAnimationCat = animationCategory.SelectedIndex;
            settings.LastAnimation = (int)animationNum.Value;
        }
        private void animationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (animationCategory.SelectedIndex == 2)
                animationNum.Value = Model.AttackNames.GetUnsortedIndex(animationName.SelectedIndex);
            else
                animationNum.Value = animationName.SelectedIndex;
        }
        private void animationName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Bitmap bgimage = Model.MenuBG_;
            switch (animationCategory.SelectedIndex)
            {
                case 0: if (e.Index < 0 || e.Index > 54) return; break;
                case 1: if (e.Index < 0 || e.Index > 44) return; break;
                case 2: if (e.Index < 0 || e.Index > 128) return; break;
                case 3: if (e.Index < 0 || e.Index > 15) return; break;
                case 4: if (e.Index < 0 || e.Index > 80) return; break;
                case 5: if (e.Index < 0 || e.Index > 31) return; break;
                case 6: if (e.Index < 0 || e.Index > 35) return; break;
                case 7: if (e.Index < 0 || e.Index > 35) return; break;
                case 8: if (e.Index < 0 || e.Index > 35) return; break;
                case 9: if (e.Index < 0 || e.Index > 101) return; break;
                case 10: if (e.Index < 0 || e.Index > 5) return; break;
                case 11: if (e.Index < 0 || e.Index > 0) return; break;
                case 12: if (e.Index < 0 || e.Index > 5) return; break;
                case 13: if (e.Index < 0 || e.Index > 3) return; break;
            }
            int[] temp;
            if (animationCategory.SelectedIndex == 1 ||
                animationCategory.SelectedIndex == 2 ||
                animationCategory.SelectedIndex == 4 ||
                animationCategory.SelectedIndex == 5 ||
                animationCategory.SelectedIndex == 6 ||
                animationCategory.SelectedIndex == 7 ||
                animationCategory.SelectedIndex == 8 ||
                animationCategory.SelectedIndex == 12)
            {
                char[] name;
                switch (animationCategory.SelectedIndex)
                {
                    case 1: // monster spells
                        name = Model.SpellNames.GetUnsortedName(e.Index + 0x40).ToCharArray();
                        temp = battleDialoguePreview.GetPreview(Model.FontDialogue, Model.FontPaletteBattle.Palette, name, false);
                        break;
                    case 2: // monster attacks
                        name = Model.AttackNames.Names[e.Index].ToCharArray();
                        temp = battleDialoguePreview.GetPreview(Model.FontDialogue, Model.FontPaletteBattle.Palette, name, false);
                        break;
                    case 4: // items
                        name = Model.ItemNames.GetUnsortedName(e.Index + 0x60).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    case 5: // ally spells
                        name = Model.SpellNames.GetUnsortedName(e.Index).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    case 6: // weapons
                        name = Model.ItemNames.GetUnsortedName(e.Index).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    case 7: // weapons miss sounds
                        name = Model.ItemNames.GetUnsortedName(e.Index).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    case 8: // weapons timed-hit sounds
                        name = Model.ItemNames.GetUnsortedName(e.Index).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    case 12: // weapons attack wrapper
                        name = Model.CharacterNames.GetUnsortedName(e.Index).ToCharArray();
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true);
                        break;
                    default:
                        name = new char[1];
                        temp = menuTextPreview.GetPreview(Model.FontMenu, Model.FontPaletteBattle.Palette, name, true); break;
                }
                //
                Rectangle background = new Rectangle(0, e.Index * 15 % bgimage.Height, bgimage.Width, 15);
                e.Graphics.DrawImage(bgimage, e.Bounds.X, e.Bounds.Y, background, GraphicsUnit.Pixel);
                // set the pixels
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    e.DrawBackground();
                int[] pixels;
                Bitmap icon;
                if (animationCategory.SelectedIndex == 1 || animationCategory.SelectedIndex == 2)
                {
                    pixels = new int[256 * 32];
                    for (int y = 2, c = 10; c < 32; y++, c++)
                    {
                        for (int x = 2, a = 8; a < 256; x++, a++)
                            pixels[y * 256 + x] = temp[c * 256 + a];
                    }
                    icon = Do.PixelsToImage(pixels, 256, 32);
                }
                else
                {
                    pixels = new int[256 * 14];
                    for (int y = 2, c = 0; y < 14; y++, c++)
                    {
                        for (int x = 2, a = 0; x < 256; x++, a++)
                            pixels[y * 256 + x] = temp[c * 256 + a];
                    }
                    icon = Do.PixelsToImage(pixels, 256, 14);
                }
                e.Graphics.DrawImage(new Bitmap(icon), new Point(e.Bounds.X, e.Bounds.Y));
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(animationName.Items[e.Index].ToString(), e.Font, new SolidBrush(animationName.ForeColor), e.Bounds);
            }
        }
        //
        private void animationScriptTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Updating = true;
            wrapper.SelectedNode = e.Node;
            asc = (AnimationCommand)e.Node.Tag;
            ascCopy = null;
            //
            toolStripCommands.SuspendDrawing();
            ToolStripNumericUpDown numUpDown;
            int i = 9;
            for (int a = 0; a < asc.CommandData.Length && a < 9; a++, i++)
            {
                numUpDown = (ToolStripNumericUpDown)toolStripCommands.Items[i];
                numUpDown.Tag = asc;
                numUpDown.Value = asc.CommandData[a];
                numUpDown.Visible = true;
            }
            for (; i < 18; i++)
            {
                numUpDown = (ToolStripNumericUpDown)toolStripCommands.Items[i];
                numUpDown.Visible = false;
            }
            toolStripCommands.ResumeDrawing();
            //
            emptyAnimationMods.Enabled = true;
            applyAnimationMods.Enabled = true;
            aniMoveDown.Enabled = true;
            aniMoveUp.Enabled = true;
            //
            panelAniControls.Enabled = true;
            applyAnimation.Enabled = true;
            ControlDisassemble(asc);
            this.Updating = false;
        }
        private void animationScriptTree_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.Up:
                case Keys.Shift | Keys.Up:
                    e.SuppressKeyPress = true;
                    aniMoveUp.PerformClick(); break;
                case Keys.Control | Keys.Down:
                case Keys.Shift | Keys.Down:
                    e.SuppressKeyPress = true;
                    aniMoveDown.PerformClick(); break;
                case Keys.Control | Keys.Z:
                    undo.PerformClick(); break;
                case Keys.Control | Keys.Y:
                    redo.PerformClick(); break;
            }
        }
        //
        private void aniMoveDown_Click(object sender, EventArgs e)
        {
            int topOffset = 0;
            AnimationCommand copy = asc.Copy();
            byte[] changes = wrapper.MoveDown(asc, ref topOffset);
            if (changes == null)
                return;
            commandStack.Push(new AnimationEdit(this, copy, topOffset, changes));
            //
            RedrawTree();
        }
        private void aniMoveUp_Click(object sender, EventArgs e)
        {
            int topOffset = 0;
            AnimationCommand copy = asc.Copy();
            byte[] changes = wrapper.MoveUp(asc, ref topOffset);
            if (changes == null)
                return;
            commandStack.Push(new AnimationEdit(this, copy, topOffset, changes));
            //
            RedrawTree();
        }
        private void expandAll_Click(object sender, EventArgs e)
        {
            wrapper.ExpandAll();
            UpdateCommandData();
        }
        private void collapseAll_Click(object sender, EventArgs e)
        {
            wrapper.CollapseAll();
            UpdateCommandData();
        }
        private void applyAnimationMods_Click(object sender, EventArgs e)
        {
            int offset = asc.InternalOffset;
            byte[] temp = new byte[asc.CommandData.Length];
            asc.CommandData.CopyTo(temp, 0);
            try
            {
                int available = asc.Length;
                byte[] changes = new byte[available];
                foreach (ToolStripItem item in toolStripCommands.Items)
                {
                    if (!item.Visible || item.GetType() != typeof(ToolStripNumericUpDown))
                        continue;
                    ToolStripNumericUpDown numUpDown = (ToolStripNumericUpDown)item;
                    int index = toolStripCommands.Items.IndexOf(numUpDown) - 9;
                    // set the new value for the command
                    asc.CommandData[index] = (byte)numUpDown.Value;
                    changes[index] = (byte)numUpDown.Value;
                }
                commandStack.Push(new AnimationEdit(this, asc, asc.InternalOffset, changes));
                // check multiple instances of command in current script, and change each accordingly
                animationScripts[(int)animationNum.Value].RefreshScript();
                ControlDisassemble(asc);
            }
            catch
            {
                for (int i = 0; i < temp.Length; i++)
                    Model.ROM[offset + i] = temp[i];
                temp.CopyTo(asc.CommandData, 0);
                // check multiple instances of command in current script, and change each accordingly
                animationScripts[(int)animationNum.Value].RefreshScript();
                MessageBox.Show("Could not change command values -- the modified command cannot be parsed. Reverting back to original command.",
                    "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ControlDisassemble(asc);
            }
            RedrawTree();
        }
        private void emptyAnimationMods_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in toolStripCommands.Items)
            {
                if (item.GetType() == typeof(ToolStripNumericUpDown))
                    ((ToolStripNumericUpDown)item).Value = 0x0A;
            }
        }
        private void previewer_Click(object sender, EventArgs e)
        {
            if (ap == null || !ap.Visible)
                ap = new Previewer(animationCategory.SelectedIndex, (int)animationNum.Value, true);
            else
                ap.Reload(animationCategory.SelectedIndex, (int)animationNum.Value, true);
            if (ap.IsDisposed)
                return;
            ap.Show();
            ap.BringToFront();
        }
        //
        private void commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (commands.SelectedIndex == -1)
                return;
            if (asc == null)
            {
                MessageBox.Show("Must select a command in the tree to change.");
                commands.SelectedIndex = -1;
                return;
            }
            // get total available space to replace w/new command
            int needed = A_ScriptEnums.GetCommandLength(commands.SelectedIndex, 0);
            if (needed > asc.AvailableSpace(needed, false))
            {
                NotEnoughSpace();
                return;
            }
            //
            ascCopy = asc.Copy();
            byte opcode = (byte)commands.SelectedIndex;
            int length = A_ScriptEnums.GetCommandLength(opcode, 0);
            ascCopy.CommandData = new byte[length];
            Bits.Fill(ascCopy.CommandData, 0x0A);
            ascCopy.Opcode = opcode;
            for (int i = 1; i < ascCopy.Length; i++)
                ascCopy.CommandData[i] = 0;
            ControlDisassemble(ascCopy);
        }
        private void applyAnimation_Click(object sender, EventArgs e)
        {
            int available = asc.Length;
            if (ascCopy != null)
            {
                available = asc.AvailableSpace(ascCopy.Length, false); // number of bytes to replace and/or wipe clean w/0x0A's
                if (ascCopy.Length > available)
                {
                    NotEnoughSpace();
                    return;
                }
                int lastNeeded = asc.AvailableSpace(ascCopy.Length, true); // the last command index needed for space
                if (MessageBox.Show("CAUTION: you are about to replace the selected command " +
                    "in the tree and the following " + (lastNeeded - asc.Index) + " commands.\n\n" +
                    "Continue?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                asc = ascCopy;
            }
            ascCopy = null;
            //
            ControlAssemble();
            byte[] changes = new byte[available];
            for (int i = 0; i < available; i++)
                changes[i] = (byte)(i < asc.Length ? asc.CommandData[i] : 0x0A);
            commandStack.Push(new AnimationEdit(this, asc, asc.InternalOffset, changes));
            //
            RedrawTree();
        }
        private void undo_Click(object sender, EventArgs e)
        {
            if (!commandStack.UndoCommand())
                return;
            RedrawTree();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            if (!commandStack.RedoCommand())
                return;
            RedrawTree();
        }
        private void aniNameA1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            AnimationCommand asc = ascCopy != null ? ascCopy : this.asc;
            switch (asc.Opcode)
            {
                case 0x00:
                case 0x03:
                    aniNumA1.Value = (int)aniNameA1.SelectedIndex;
                    break;
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x2C:
                case 0x2D:
                case 0x2E:
                case 0x2F:
                    aniNumB2.Hexadecimal = aniNameA1.SelectedIndex != 0;
                    switch (aniNameA1.SelectedIndex)
                    {
                        case 0:
                            aniNumB2.Minimum = 0;
                            aniNumB2.Maximum = 0xFFFF;
                            break;
                        case 1:
                        case 5:
                            aniNumB2.Maximum = 0x7EFFFF;
                            aniNumB2.Minimum = 0x7E0000;
                            break;
                        case 2:
                            aniNumB2.Maximum = 0x7FFFFF;
                            aniNumB2.Minimum = 0x7F0000;
                            break;
                        case 3:
                            aniNumB2.Minimum = 0x60;
                            aniNumB2.Maximum = 0x6F;
                            break;
                        case 4:
                        case 6:
                            aniNumB2.Minimum = 0;
                            aniNumB2.Maximum = 0xFF;
                            break;
                    }
                    break;
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                case 0x29:
                case 0x2A:
                case 0x2B:
                    aniNumB1.Hexadecimal = aniNameA1.SelectedIndex != 0;
                    switch (aniNameA1.SelectedIndex)
                    {
                        case 0:
                            aniNumB1.Minimum = 0;
                            aniNumB1.Maximum = 0xFFFF;
                            break;
                        case 1:
                        case 5:
                            aniNumB1.Maximum = 0x7EFFFF;
                            aniNumB1.Minimum = 0x7E0000;
                            break;
                        case 2:
                            aniNumB1.Maximum = 0x7FFFFF;
                            aniNumB1.Minimum = 0x7F0000;
                            break;
                        case 3:
                            aniNumB1.Minimum = 0x60;
                            aniNumB1.Maximum = 0x6F;
                            break;
                        case 4:
                        case 6:
                            aniNumB1.Minimum = 0;
                            aniNumB1.Maximum = 0xFF;
                            break;
                    }
                    break;
                case 0xAB:
                case 0xAE:
                    aniNumA1.Value = (int)aniNameA1.SelectedIndex;
                    break;
                case 0xB0:
                case 0xB1:
                    aniNumA1.Value = (int)aniNameA1.SelectedIndex;
                    break;
                case 0xBC:
                case 0xBD:
                    aniNumA1.Value = Model.ItemNames.GetUnsortedIndex(aniNameA1.SelectedIndex);
                    break;
                case 0xD5:
                    aniNumA1.Value = Model.MonsterNames.GetUnsortedIndex(aniNameA1.SelectedIndex);
                    break;
            }
        }
        private void aniNameA1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void aniNumA1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            AnimationCommand asc = ascCopy != null ? ascCopy : this.asc;
            switch (asc.Opcode)
            {
                case 0x00:
                case 0x03:
                    aniNameA1.SelectedIndex = (byte)aniNumA1.Value;
                    break;
                case 0xAB:
                case 0xAE:
                    aniNameA1.SelectedIndex = (byte)aniNumA1.Value;
                    break;
                case 0xB0:
                case 0xB1:
                    aniNameA1.SelectedIndex = (byte)aniNumA1.Value;
                    break;
                case 0xBC:
                case 0xBD:
                    aniNameA1.SelectedIndex = Model.ItemNames.GetSortedIndex((int)aniNumA1.Value);
                    break;
                case 0xD5:
                    aniNameA1.SelectedIndex = Model.MonsterNames.GetSortedIndex((int)aniNumA1.Value);
                    break;
            }
        }

        //
        private void AnimationScripts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified)
                goto Close;
            DialogResult result = MessageBox.Show("Animations have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Assemble();
            }
            else if (result == DialogResult.No)
            {
                Buffer.BlockCopy(animationBank, 0, Model.ROM, 0x350000, 0x10000);
                Buffer.BlockCopy(battleBank, 0, Model.ROM, 0x3A6000, 0xA000);
                Buffer.BlockCopy(bonusmessageBank, 0, Model.ROM, 0x02F000, 0x1000);
                Model.ToadTutorialScript = null;
                Model.SpellAnimMonsters = null;
                Model.SpellAnimAllies = null;
                Model.AttackAnimations = null;
                Model.ItemAnimations = null;
                Model.BattleEvents = null;
                Model.BonusMessageAnimations = null;
                Model.BehaviorAnimAllies = null;
                Model.BehaviorAnimMonsters = null;
                Model.EntranceAnimations = null;
                Model.WeaponAnimations = null;
                Model.WeaponSoundScripts = null;
                Model.WeaponTimedHitScripts = null;
                Model.ToadTutorialScript = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            if (ap != null)
                ap.Close();
        }
        private void save_Click(object sender, System.EventArgs e)
        {
            Assemble();
        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }


        private void clearActionScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reset_Click(object sender, EventArgs e)
        {

        }

        private void clearEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void UpdateAnimationCategory(int index)
        {
            animationName.Visible = true;
            animationNum.Visible = true;

            Image image = null;
            String text = "";

            switch (index)
            {
                case 0:
                    text = "Monster Behaviors";
                    image = animCategoryMonstersBehaviors.Image;
                    break;
                case 1:
                    text = "Monster Spells";
                    image = animCategoryMonstersSpells.Image;
                    break;
                case 2:
                    text = "Monster Attacks";
                    image = animCategoryMonstersAttacks.Image;
                    break;
                case 3:
                    text = "Monster Entrances";
                    image = animCategoryMonstersEntrances.Image;
                    break;
                case 4:
                    text = "Items";
                    image = animCategoryItems.Image;
                    break;
                case 5:
                    text = "Ally Spells";
                    image = animCategoryAlliesSpells.Image;
                    break;
                case 6:
                    text = "Weapons Animations";
                    image = animCategoryAlliesWeaponsAnimations.Image;
                    break;
                case 7:
                    text = "Weapon Miss Sounds";
                    image = animCategoryAlliesWeaponsMissSounds.Image;
                    break;
                case 8:
                    text = "Weapon Timed-Hit Sounds";
                    image = animCategoryAlliesWeaponsTimedHitSounds.Image;
                    break;
                case 9:
                    text = "Battle Events";
                    image = animCategoryBattleEvents.Image;
                    break;
                case 10:
                    text = "Flower Bonus Messages";
                    image = animCategoryBonusFlowerMessages.Image;
                    break;
                case 11:
                    text = "Toad's Tutorial";
                    image = animCategoryToadsTutorial.Image;
                    break;
                case 12:
                    text = "Ally Weapon Wrapper";
                    image = animCategoryAlliesWeaponsWrapper.Image;
                    break;
                case 13:
                    text = "Ally Behaviors";
                    image = animCategoryAlliesBehaviors.Image;
                    break;
            }
            animationCategory.SelectedIndex = index;
            animationsCategoryDropDownMenu.Image = image;
            animationsCategoryDropDownMenu.Text = text;
        }

        private void animCategoryAlliesWeaponsWrapper_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(12);
        }

        private void animCategoryAlliesWeaponsTimedHitSounds_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(8);
        }

        private void animCategoryAlliesWeaponsMissSounds_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(7);
        }

        private void animCategoryAlliesWeaponsAnimations_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(6);
        }

        private void animCategoryAlliesSpells_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(5);
        }

        private void animCategoryItems_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(4);
        }

        private void animCategoryMonstersEntrances_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(3);
        }

        private void animCategoryMonstersBehaviors_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(0);
        }

        private void animCategoryMonstersAttacks_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(2);
        }

        private void animCategoryMonstersSpells_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(1);
        }

        private void animCategoryAlliesBehaviors_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(13);
        }

        private void animCategoryToadsTutorial_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(11);
        }

        private void animCategoryBattleEvents_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(9);
        }

        private void animCategoryBonusFlowerMessages_Click(object sender, EventArgs e)
        {
            UpdateAnimationCategory(10);
        }


        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = "animationScripts.txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            int i = 0;
            StreamWriter evtscr = File.CreateText(saveFileDialog.FileName);
            evtscr.WriteLine("**************");
            evtscr.WriteLine("MONSTER SPELLS");
            evtscr.WriteLine("**************\n");
            foreach (AnimationScript ans in Model.SpellAnimMonsters)
            {
                evtscr.WriteLine("\nMONSTER SPELL {" + i.ToString("d3") + "} " + Model.SpellNames.GetUnsortedName(i + 64).Substring(1).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n***********");
            evtscr.WriteLine("ALLY SPELLS");
            evtscr.WriteLine("***********\n");
            foreach (AnimationScript ans in Model.SpellAnimAllies)
            {
                evtscr.WriteLine("\nALLY SPELL {" + i.ToString("d3") + "} " + Model.SpellNames.GetUnsortedName(i).Substring(1).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*******");
            evtscr.WriteLine("ATTACKS");
            evtscr.WriteLine("*******\n");
            foreach (AnimationScript ans in Model.AttackAnimations)
            {
                evtscr.WriteLine("\nATTACK {" + i.ToString("d3") + "} " + Model.AttackNames.GetUnsortedName(i).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*****");
            evtscr.WriteLine("ITEMS");
            evtscr.WriteLine("*****\n");
            foreach (AnimationScript ans in Model.ItemAnimations)
            {
                evtscr.WriteLine("\nITEM {" + i.ToString("d3") + "} " + Model.ItemNames.GetUnsortedName(i + 96).Substring(1).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*************");
            evtscr.WriteLine("BATTLE EVENTS");
            evtscr.WriteLine("*************\n");
            foreach (AnimationScript ans in Model.BattleEvents)
            {
                evtscr.WriteLine("\nBATTLE EVENT {" + i.ToString("d3") + "} " +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*********");
            evtscr.WriteLine("BEHAVIORS");
            evtscr.WriteLine("*********\n");
            foreach (AnimationScript ans in Model.BehaviorAnimMonsters)
            {
                evtscr.WriteLine("\nBEHAVIOR {" + i.ToString("d3") + "} " +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*********");
            evtscr.WriteLine("ENTRANCES");
            evtscr.WriteLine("*********\n");
            foreach (AnimationScript ans in Model.EntranceAnimations)
            {
                evtscr.WriteLine("\nENTRANCE {" + i.ToString("d3") + "} " +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*******");
            evtscr.WriteLine("WEAPONS");
            evtscr.WriteLine("*******\n");
            foreach (AnimationScript ans in Model.WeaponAnimations)
            {
                evtscr.WriteLine("\nWEAPON {" + i.ToString("d3") + "} " + Model.ItemNames.GetUnsortedName(i).Substring(1).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
            i = 0;
            evtscr.WriteLine("\n*******");
            evtscr.WriteLine("WEAPONS");
            evtscr.WriteLine("*******\n");
            foreach (AnimationScript ans in Model.WeaponSoundScripts)
            {
                evtscr.WriteLine("\nSOUND {" + i.ToString("d3") + "} " + Model.ItemNames.GetUnsortedName(i).Substring(1).Trim() +
                    "------------------------------------------------------------>\n");
                foreach (AnimationCommand asc in ans.Commands)
                {
                    evtscr.Write((asc.Offset).ToString("X6") + ": ");
                    evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                    dumpAnimationLoop(asc, evtscr, 1);
                }
                i++;
            }
        }
        private void dumpAnimationLoop(AnimationCommand com, StreamWriter evtscr, int level)
        {
            foreach (AnimationCommand asc in com.Commands)
            {
                for (int i = 0; i < level; i++)
                    evtscr.Write("\t");
                evtscr.Write((asc.Offset).ToString("X6") + ": ");
                evtscr.Write("{" + BitConverter.ToString(asc.CommandData) + "}\n");
                dumpAnimationLoop(asc, evtscr, level + 1);
            }
        }
        #endregion
    }
}
