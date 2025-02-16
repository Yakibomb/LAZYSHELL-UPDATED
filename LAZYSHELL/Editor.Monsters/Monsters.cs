using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Monsters : NewForm
    {
        #region Variables
            //
        private delegate void Function();
                private Monster[] monsters { get { return Model.Monsters; } set { Model.Monsters = value; } }
        private Monster monster { get { return monsters[Index]; } set { monsters[Index] = value; } }
        private FontCharacter[] fontMenu { get { return Model.FontMenu; } }
        private int[] fontPaletteBattle { get { return Model.FontPaletteBattle.Palettes[0]; } }
        public int Index { get { return (int)monsterNum.Value; } set { monsterNum.Value = value; } }
        private Settings settings = Settings.Default;
        private MenuTextPreview menuTextPreview = new MenuTextPreview();
        private Previewer previewer;
        //
        private BattleScripts battleScriptsEditor;
        private HackingTools hackingToolsWindow;
        private EditLabel labelWindow;
        //
        private bool byteView { get { return !textView.Checked; } set { textView.Checked = !value; } }
        private Bitmap psychopathBGImage { get { return Model.BattleDialogueTilesetImage; } }
        private Bitmap psychopathTextImage;
        private TextHelper textHelper = TextHelper.Instance;
        private BattleDialoguePreview battleDialoguePreview = new BattleDialoguePreview();
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } }
        private int[] fontPaletteDialogue { get { return Model.FontPaletteDialogue.Palettes[1]; } }
        #endregion
        #region Functions
        public Monsters()
        {
            InitializeComponent();
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip4, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip4, Keys.F2, baseConvertor);
            labelWindow = new EditLabel(monsterName, monsterNum, "Monsters", false);
            // create editors
            battleScriptsEditor = new BattleScripts(this);
            battleScriptsEditor.TopLevel = false;
            battleScriptsEditor.Dock = DockStyle.Fill;
            //battleScriptsEditor.SetToolTips(toolTip1);
            hackingToolsWindow = new HackingTools(new Function(RefreshMonsterTab), monsterNum);
            panel1.Controls.Add(battleScriptsEditor);
            battleScriptsEditor.BringToFront();
            battleScriptsEditor.Visible = true;
            toolTip1.InitialDelay = 0;
            InitializeStrings();
            RefreshMonsterTab();
            new ToolTipLabel(this, baseConvertor, helpTips);
            this.History = new History(this, monsterName, monsterNum);
            //
            if (settings.RememberLastIndex)
                Index = settings.LastMonster;
            //MessageBox.Show(battleScriptsEditor.Width + " x " + battleScriptsEditor.Height);
        }
        private void InitializeStrings()
        {
            // monster names
            this.monsterName.Items.Clear();
            this.monsterName.Items.AddRange(Model.MonsterNames.Names);
            // item names
            this.MonsterYoshiCookie.Items.Clear();
            this.MonsterYoshiCookie.Items.AddRange(Model.ItemNames.Names);
            this.ItemWinA.Items.Clear();
            this.ItemWinA.Items.AddRange(Model.ItemNames.Names);
            this.ItemWinB.Items.Clear();
            this.ItemWinB.Items.AddRange(Model.ItemNames.Names);
        }
        private void RefreshMonsterTab()
        {
            if (!this.Updating)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Updating = true;
                this.monsterName.SelectedIndex = Model.MonsterNames.GetSortedIndex(Index);
                this.monsterNameText.Text = Do.RawToASCII(monster.Name, Lists.KeystrokesMenu);
                this.MonsterValHP.Value = monster.HP;
                this.MonsterValSpeed.Value = monster.Speed;
                this.MonsterValAtk.Value = monster.Attack;
                this.MonsterValMgDef.Value = monster.MagicDefense;
                this.MonsterValMgAtk.Value = monster.MagicAttack;
                this.MonsterValDef.Value = monster.Defense;
                this.MonsterValMgEvd.Value = monster.MagicEvade;
                this.MonsterValEvd.Value = monster.Evade;
                this.MonsterValFP.Value = monster.FP;
                this.MonsterValExp.Value = monster.Experience;
                this.MonsterValCoins.Value = monster.Coins;
                this.MonsterValElevation.Value = monster.Elevation;
                this.MonsterFlowerOdds.Value = monster.FlowerOdds * 10;
                this.MonsterElementsNullify.SetItemChecked(0, monster.ElemNullIce);
                this.MonsterElementsNullify.SetItemChecked(1, monster.ElemNullFire);
                this.MonsterElementsNullify.SetItemChecked(2, monster.ElemNullThunder);
                this.MonsterElementsNullify.SetItemChecked(3, monster.ElemNullJump);
                this.MonsterProperties.SetItemChecked(0, monster.Invincible);
                this.MonsterProperties.SetItemChecked(1, monster.MortalityProtection);
                this.MonsterProperties.SetItemChecked(2, monster.DisableAutoDeath);
                this.MonsterProperties.SetItemChecked(3, monster.Palette2bpp);
                this.MonsterEffectsNullify.SetItemChecked(0, monster.EffectNullMute);
                this.MonsterEffectsNullify.SetItemChecked(1, monster.EffectNullSleep);
                this.MonsterEffectsNullify.SetItemChecked(2, monster.EffectNullPoison);
                this.MonsterEffectsNullify.SetItemChecked(3, monster.EffectNullFear);
                this.MonsterEffectsNullify.SetItemChecked(4, monster.EffectNullBerserk);
                this.MonsterEffectsNullify.SetItemChecked(5, monster.EffectNullMushroom);
                this.MonsterEffectsNullify.SetItemChecked(6, monster.EffectNullScarecrow);
                this.MonsterEffectsNullify.SetItemChecked(7, monster.EffectNullInvincible);
                this.MonsterElementsWeakness.SetItemChecked(0, monster.ElemWeakIce);
                this.MonsterElementsWeakness.SetItemChecked(1, monster.ElemWeakFire);
                this.MonsterElementsWeakness.SetItemChecked(2, monster.ElemWeakThunder);
                this.MonsterElementsWeakness.SetItemChecked(3, monster.ElemWeakJump);
                this.MonsterFlowerBonus.SelectedIndex = monster.FlowerBonus;
                this.MonsterMorphSuccess.SelectedIndex = monster.MorphSuccess;
                this.MonsterCoinSize.SelectedIndex = monster.CoinSize;
                this.MonsterBehavior.SelectedIndex = monster.SpriteBehavior;
                this.MonsterEntranceStyle.SelectedIndex = monster.EntranceStyle;
                this.MonsterSoundOther.SelectedIndex = monster.OtherSound;
                this.MonsterSoundStrike.SelectedIndex = monster.StrikeSound;
                this.MonsterYoshiCookie.SelectedIndex = Model.ItemNames.GetSortedIndex(monster.YoshiCookie);
                this.ItemWinA.SelectedIndex = Model.ItemNames.GetSortedIndex(monster.ItemWinA);
                this.ItemWinB.SelectedIndex = Model.ItemNames.GetSortedIndex(monster.ItemWinB);
                //
                this.MonsterPsychopath.Text = monster.GetPsychopath(byteView);
                this.MonsterPsychopath_TextChanged(null, null);
                CalculateFreeSpace();
                SetDialogueImages();
                //
                this.Updating = false;
                Cursor.Current = Cursors.Arrow;
            }
        }
        private void InsertIntoText(string toInsert)
        {
            char[] newText = new char[MonsterPsychopath.Text.Length + toInsert.Length];
            MonsterPsychopath.Text.CopyTo(0, newText, 0, MonsterPsychopath.SelectionStart);
            toInsert.CopyTo(0, newText, MonsterPsychopath.SelectionStart, toInsert.Length);
            MonsterPsychopath.Text.CopyTo(MonsterPsychopath.SelectionStart, newText, MonsterPsychopath.SelectionStart + toInsert.Length, this.MonsterPsychopath.Text.Length - this.MonsterPsychopath.SelectionStart);
            if (byteView)
                monster.CaretPosByteView = this.MonsterPsychopath.SelectionStart + toInsert.Length;
            else
                monster.CaretPosTextView = this.MonsterPsychopath.SelectionStart + toInsert.Length;
            monster.SetPsychopath(new string(newText), byteView);
            this.MonsterPsychopath.Text = monster.GetPsychopath(byteView);
            if (byteView)
                this.MonsterPsychopath.SelectionStart = monster.CaretPosByteView;
            else
                this.MonsterPsychopath.SelectionStart = monster.CaretPosTextView;
        }
        private void CalculateFreeSpace()
        {
            int used = 0; int size = 0xb641 + 0x2229;
            for (int i = 0; i < monsters.Length - 1; i++)
            {
                used += monsters[i].RawPsychopath.Length;
                if (used + monsters[i].RawPsychopath.Length > size)
                {
                    bool test = size >= used + monsters[i].RawPsychopath.Length;
                    if (!test)
                    {
                        freeBytes.Text = "Entry " + i++.ToString() + " Too Long - Cannot Save";
                        return;
                    }
                }
            }
            freeBytes.Text = ((double)(size - used)).ToString() + " characters left";
        }
        private void SetDialogueImages()
        {
            pictureBoxPsychopath.BackColor = Color.FromArgb(fontPaletteDialogue[0]);
            pictureBoxPsychopath.Invalidate();
            MonsterPsychopath_TextChanged(null, null);
        }
        public void Assemble()
        {
            int i = 0;
            int length = 0xA1D1;
            for (; i < monsters.Length && length + monsters[i].RawPsychopath.Length < 0xb641; i++)
                monsters[i].Assemble(ref length);
            length = 0x1C2A;
            for (; i < monsters.Length && length + monsters[i].RawPsychopath.Length < 0x2229; i++)
                monsters[i].Assemble(ref length);
            if (i != monsters.Length)
                MessageBox.Show(
                    "The allotted space for psychopath dialogues has been exceeded. Not all psychopath dialogues have been saved.",
                    "LAZYSHELL++");
            battleScriptsEditor.Assemble();
            battleScriptsEditor.Modified = false;
            this.Modified = false;
        }
        #endregion
        #region Event Handlers
        private void Monsters_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified && !battleScriptsEditor.Modified)
            {
                if (hackingToolsWindow.Visible) hackingToolsWindow.Close();
                return;
            }
            DialogResult result = MessageBox.Show(
                "Monsters and battlescripts have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Assemble();
                battleScriptsEditor.Assemble();
                battleScriptsEditor.Close();
                if (hackingToolsWindow.Visible) 
                    hackingToolsWindow.Close();
                if (previewer != null)
                    previewer.Close();
            }
            else if (result == DialogResult.No)
            {
                Model.Monsters = null;
                Model.MonsterNames = null;
                Model.BattleScripts = null;
                battleScriptsEditor.Close();
                if (hackingToolsWindow.Visible)
                    hackingToolsWindow.Close();
                if (previewer != null)
                    previewer.Close();
                return;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        }
        // main
        private void monsterNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshMonsterTab();
            battleScriptsEditor.Initialize();
            settings.LastMonster = Index;
        }
        private void monsterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.monsterNum.Value = Model.MonsterNames.GetUnsortedIndex(monsterName.SelectedIndex);
        }
        private void monsterName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(sender, e, menuTextPreview, Model.MonsterNames, fontMenu, fontPaletteBattle, true, Model.MenuBG_);
        }
        private void monsterNameText_TextChanged(object sender, EventArgs e)
        {
            if (Model.MonsterNames.GetUnsortedName(monster.Index).CompareTo(this.monsterNameText.Text) != 0)
            {
                monster.Name = Do.ASCIIToRaw(this.monsterNameText.Text, Lists.KeystrokesMenu, 13);
                Model.MonsterNames.SetName(
                    monster.Index,
                    new string(monster.Name));
                Model.MonsterNames.SortAlphabetically();
                this.monsterName.Items.Clear();
                this.monsterName.Items.AddRange(Model.MonsterNames.Names);
                this.monsterName.SelectedIndex = Model.MonsterNames.GetSortedIndex(monster.Index);
            }
        }
        private void battlePreview_Click(object sender, EventArgs e)
        {
            if (previewer == null || !previewer.Visible)
                previewer = new Previewer(Index, EType.BattleScript);
            else
                previewer.Reload(Index, EType.BattleScript);
            previewer.Show();
            previewer.BringToFront();
        }
        // vital stats
        private void MonsterValHP_ValueChanged(object sender, EventArgs e)
        {
            monster.HP = (ushort)MonsterValHP.Value;
        }
        private void MonsterValFP_ValueChanged(object sender, EventArgs e)
        {
            monster.FP = (byte)MonsterValFP.Value;
        }
        private void MonsterValAtk_ValueChanged(object sender, EventArgs e)
        {
            monster.Attack = (byte)MonsterValAtk.Value;
        }
        private void MonsterValDef_ValueChanged(object sender, EventArgs e)
        {
            monster.Defense = (byte)MonsterValDef.Value;
        }
        private void MonsterValMgAtk_ValueChanged(object sender, EventArgs e)
        {
            monster.MagicAttack = (byte)MonsterValMgAtk.Value;
        }
        private void MonsterValMgDef_ValueChanged(object sender, EventArgs e)
        {
            monster.MagicDefense = (byte)MonsterValMgDef.Value;
        }
        private void MonsterValSpeed_ValueChanged(object sender, EventArgs e)
        {
            monster.Speed = (byte)MonsterValSpeed.Value;
        }
        private void MonsterValEvd_ValueChanged(object sender, EventArgs e)
        {
            monster.Evade = (byte)MonsterValEvd.Value;
        }
        private void MonsterValMgEvd_ValueChanged(object sender, EventArgs e)
        {
            monster.MagicEvade = (byte)MonsterValMgEvd.Value;
        }
        // reward stats
        private void MonsterValExp_ValueChanged(object sender, EventArgs e)
        {
            monster.Experience = (ushort)MonsterValExp.Value;
        }
        private void MonsterValCoins_ValueChanged(object sender, EventArgs e)
        {
            monster.Coins = (byte)MonsterValCoins.Value;
        }
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, true, Model.MenuBG_);
        }
        private void ItemWinA_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.ItemWinA = (byte)Model.ItemNames.GetUnsortedIndex(ItemWinA.SelectedIndex);
        }
        private void ItemWinB_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.ItemWinB = (byte)Model.ItemNames.GetUnsortedIndex(ItemWinB.SelectedIndex);
        }
        private void MonsterYoshiCookie_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.YoshiCookie = (byte)Model.ItemNames.GetUnsortedIndex(MonsterYoshiCookie.SelectedIndex);
        }
        // other properties
        private void MonsterMorphSuccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.MorphSuccess = (byte)MonsterMorphSuccess.SelectedIndex;
        }
        private void MonsterCoinSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.CoinSize = (byte)MonsterCoinSize.SelectedIndex;
        }
        private void MonsterEntranceStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.EntranceStyle = (byte)MonsterEntranceStyle.SelectedIndex;
        }
        private void MonsterBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.SpriteBehavior = (byte)MonsterBehavior.SelectedIndex;
        }
        private void MonsterSoundStrike_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.StrikeSound = (byte)MonsterSoundStrike.SelectedIndex;
        }
        private void MonsterSoundOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.OtherSound = (byte)MonsterSoundOther.SelectedIndex;
        }
        private void MonsterValElevation_ValueChanged(object sender, EventArgs e)
        {
            monster.Elevation = (byte)MonsterValElevation.Value;
        }
        // effects, elements
        private void MonsterEffectsNullify_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.EffectNullMute = MonsterEffectsNullify.GetItemChecked(0);
            monster.EffectNullSleep = MonsterEffectsNullify.GetItemChecked(1);
            monster.EffectNullPoison = MonsterEffectsNullify.GetItemChecked(2);
            monster.EffectNullFear = MonsterEffectsNullify.GetItemChecked(3);
            monster.EffectNullBerserk = MonsterEffectsNullify.GetItemChecked(4);
            monster.EffectNullMushroom = MonsterEffectsNullify.GetItemChecked(5);
            monster.EffectNullScarecrow = MonsterEffectsNullify.GetItemChecked(6);
            monster.EffectNullInvincible = MonsterEffectsNullify.GetItemChecked(7);
        }
        private void MonsterElementsWeakness_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.ElemWeakIce = MonsterElementsWeakness.GetItemChecked(0);
            monster.ElemWeakFire = MonsterElementsWeakness.GetItemChecked(1);
            monster.ElemWeakThunder = MonsterElementsWeakness.GetItemChecked(2);
            monster.ElemWeakJump = MonsterElementsWeakness.GetItemChecked(3);
        }
        private void MonsterElementsNullify_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.ElemNullIce = MonsterElementsNullify.GetItemChecked(0);
            monster.ElemNullFire = MonsterElementsNullify.GetItemChecked(1);
            monster.ElemNullThunder = MonsterElementsNullify.GetItemChecked(2);
            monster.ElemNullJump = MonsterElementsNullify.GetItemChecked(3);
        }
        private void MonsterProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.Invincible = MonsterProperties.GetItemChecked(0);
            monster.MortalityProtection = MonsterProperties.GetItemChecked(1);
            monster.DisableAutoDeath = MonsterProperties.GetItemChecked(2);
            monster.Palette2bpp = MonsterProperties.GetItemChecked(3);
        }
        private void MonsterFlowerBonus_SelectedIndexChanged(object sender, EventArgs e)
        {
            monster.FlowerBonus = (byte)MonsterFlowerBonus.SelectedIndex;
        }
        private void MonsterFlowerOdds_ValueChanged(object sender, EventArgs e)
        {
            if (MonsterFlowerOdds.Value % 10 != 0)
                MonsterFlowerOdds.Value = (int)MonsterFlowerOdds.Value / 10 * 10;
            else
                monster.FlowerOdds = (byte)(MonsterFlowerOdds.Value / 10);
        }
        // menustrip
        private void save_Click(object sender, EventArgs e)
        {
            Assemble();
        }
        private void import_Click(object sender, EventArgs e)
        {
            new IOElements(monsters, Index, "IMPORT MONSTERS...").ShowDialog();
            RefreshMonsterTab();
        }
        private void export_Click(object sender, EventArgs e)
        {
            new IOElements(monsters, Index, "EXPORT MONSTERS...").ShowDialog();
        }
        private void clear_Click(object sender, EventArgs e)
        {
            new ClearElements(monsters, Index, "CLEAR MONSTERS...").ShowDialog();
            RefreshMonsterTab();
        }
        private void importBattleScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            battleScriptsEditor.Import();
        }
        private void exportBattleScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            battleScriptsEditor.Export();
        }
        private void dumpBattleScriptTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            battleScriptsEditor.DumpBattlescriptText();
        }
        private void clearBattleScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            battleScriptsEditor.Clear();
        }
        private void hackingTools_Click(object sender, EventArgs e)
        {
            if (!hackingToolsWindow.Visible)
                hackingToolsWindow.Show();
            hackingToolsWindow.BringToFront();
        }
        private void resetCurrentMonsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current monster. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            monster = new Monster(Index);
            monsterNum_ValueChanged(null, null);
        }
        private void resetCurrentBattleScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current battle script. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            battleScriptsEditor.BattleScript = new LAZYSHELL.ScriptsEditor.BattleScript(battleScriptsEditor.index);
            monsterNum_ValueChanged(null, null);
        }
        private void showMonster_Click(object sender, EventArgs e)
        {
            panel13.Visible = !panel13.Visible;
        }
        private void showBattleScripts_Click(object sender, EventArgs e)
        {
            battleScriptsEditor.Visible = !battleScriptsEditor.Visible;
        }
        // psychopath dialogue
        private void MonsterPsychopath_TextChanged(object sender, EventArgs e)
        {
            char[] text = MonsterPsychopath.Text.ToCharArray();
            char[] swap;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    int tempSel = MonsterPsychopath.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    MonsterPsychopath.Text = new string(swap);
                    text = MonsterPsychopath.Text.ToCharArray();
                    i += 2;
                    MonsterPsychopath.SelectionStart = tempSel + 2;
                }
            }
            bool flag = textHelper.VerifySymbols(this.MonsterPsychopath.Text.ToCharArray(), byteView);
            if (flag)
            {
                this.MonsterPsychopath.BackColor = SystemColors.Window;
                monster.SetPsychopath(this.MonsterPsychopath.Text, byteView);
                if (!monster.PsychopathError)
                {
                    monster.SetPsychopath(MonsterPsychopath.Text, byteView);
                    int[] pixels = battleDialoguePreview.GetPreview(fontDialogue, fontPaletteDialogue, monster.RawPsychopath, false);
                    psychopathTextImage = Do.PixelsToImage(pixels, 256, 32);
                    pictureBoxPsychopath.Invalidate();
                }
            }
            if (!flag || monster.PsychopathError)
                this.MonsterPsychopath.BackColor = Color.Red;
            CalculateFreeSpace();
        }
        private void pictureBoxPsychopath_Paint(object sender, PaintEventArgs e)
        {
            if (psychopathBGImage != null)
                e.Graphics.DrawImage(psychopathBGImage, 0, 0);
            if (psychopathTextImage != null)
                e.Graphics.DrawImage(psychopathTextImage, 0, 0);
        }
        private void pageUp_Click(object sender, EventArgs e)
        {
            battleDialoguePreview.PageUp();
            MonsterPsychopath_TextChanged(null, null);
        }
        private void pageDown_Click(object sender, EventArgs e)
        {
            battleDialoguePreview.PageDown();
            MonsterPsychopath_TextChanged(null, null);
        }
        private void byteOrTextView_Click(object sender, EventArgs e)
        {
            MonsterPsychopath.Text = monster.GetPsychopath(byteView);
        }
        private void newLine_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoText("[1]");
            else
                InsertIntoText("[newLine]");
        }
        private void endString_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoText("[0]");
            else
                InsertIntoText("[end]");
        }
        private void pause60f_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoText("[12]");
            else
                InsertIntoText("[delay]");
        }
        private void pauseA_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoText("[2]");
            else
                InsertIntoText("[pauseInput]");
        }
        private void pauseFrames_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoText("[3]");
            else
                InsertIntoText("[delayInput]");
        }
        //
        private void panel13_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder3D(e.Graphics, panel13.ClientRectangle, Border3DStyle.Raised, Border3DSide.All);
        }
        #endregion
    }
}
