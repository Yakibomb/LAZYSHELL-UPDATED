using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor;
using System.Diagnostics;

namespace LAZYSHELL
{
    public partial class Previewer : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
        private string romPath;
        private string emulatorPath = "INVALID";
        private bool rom = false, emulator = false, savestate = false, eventchoice = false, initializing = false;
        private int selectNum;
        private List<Entrance> eventTriggers;
        private bool snes9x;
        private EType behavior;
                private int category;
        private int index;
        private bool automatic = false;
        private byte[] soundFX;
        //
        private byte[] maxStats = new byte[]
        {
            0x1E,0xE7,0x03,0xE7,0x03,0xFF,0xFF,0xFF,0xFF,0xFF,0x0F,0x27,0x21,0x45,0x51,0x00,0x3F,0x00,0x00,0x00,
            0x1E,0xE7,0x03,0xE7,0x03,0xFF,0xFF,0xFF,0xFF,0xFF,0x0F,0x27,0x22,0x44,0x5E,0x00,0xC0,0x0F,0x00,0x00,
            0x1E,0xE7,0x03,0xE7,0x03,0xFF,0xFF,0xFF,0xFF,0xFF,0x0F,0x27,0x1E,0x43,0x5A,0x00,0x00,0xF0,0x00,0x00,
            0x1E,0xE7,0x03,0xE7,0x03,0xFF,0xFF,0xFF,0xFF,0xFF,0x0F,0x27,0x1F,0x42,0x4D,0x00,0x00,0x00,0x1F,0x00,
            0x1E,0xE7,0x03,0xE7,0x03,0xFF,0xFF,0xFF,0xFF,0xFF,0x0F,0x27,0x20,0x41,0x4B,0x00,0x00,0x00,0xE0,0x07
        };
        //
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        #endregion
        // Constructor
        public Previewer(int num, EType behavior)
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            this.selectNum = num;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            InitializeComponent();
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (num == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            Do.AddShortcut(toolStrip1, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip1, Keys.F2, baseConvertor);
            new ToolTipLabel(this, baseConvertor, helpTips);
        }
        public Previewer(int category, int index, bool automatic)
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            this.category = category;
            this.index = index;
            this.selectNum = index;
            this.eventTriggers = new List<Entrance>();
            this.behavior = EType.AnimationScript;
            InitializeComponent();
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (index == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            Do.AddShortcut(toolStrip1, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip1, Keys.F2, baseConvertor);
            new ToolTipLabel(this, baseConvertor, helpTips);
        }
        public Previewer(int index, bool automatic, EType behavior) // SPC Previewer
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            this.index = index;
            this.selectNum = index;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            this.automatic = automatic;
            InitializeComponent();
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (index == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            Do.AddShortcut(toolStrip1, Keys.F1, helpTips);
            Do.AddShortcut(toolStrip1, Keys.F2, baseConvertor);
            new ToolTipLabel(this, baseConvertor, helpTips);
            if (automatic)
                launchButton_Click(null, null);
        }
        public void Reload(int num, EType behavior)
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            if (this.selectNum == num && this.behavior == behavior)
                return;
            this.selectNum = num;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (this.selectIndex.Value == selectNum)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
        }
        public void Reload(int category, int index, bool automatic)
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            this.category = category;
            this.index = index;
            this.selectNum = index;
            this.eventTriggers = new List<Entrance>();
            this.behavior = EType.AnimationScript;
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (index == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
        }
        public void Reload(int index, bool automatic, EType behavior)
        {
            if (settings.PreviewFirstTime)
            {
                DialogResult result = MessageBox.Show("The generated Preview ROM should not be used for anything other than Previews. Doing so will yield unpredictable results.\n\nDo you understand?", "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    settings.PreviewFirstTime = false;
                    settings.Save();
                }
            }
            this.index = index;
            this.selectNum = index;
            this.eventTriggers = new List<Entrance>();
            this.behavior = behavior;
            this.automatic = automatic;
            InitializePreviewer();
            if (!(this.emulator = GetEmulator()))
            {
                MessageBox.Show("The Previewer must contain an emulator file path.", "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (index == 0)
                this.selectNumericUpDown_ValueChanged(null, null);
            UpdateGUI();
            if (automatic)
                launchButton_Click(null, null);
        }
        #region Functions
        private void InitializePreviewer()
        {
            this.initializing = true;
            this.zsnesArgs.Text = settings.PreviewArguments;
            this.dynamicROMPath.Checked = settings.PreviewDynamicRomName;
            this.level.Value = settings.PreviewLevel;
            if (behavior == EType.EventScript)
            {
                this.Text = "PREVIEW EVENT - LAZYSHELL++";
                this.label1.Text = "Event #";
                this.selectIndex.Maximum = 4095;
                this.groupBox2.Enabled = false;
            }
            else if (behavior == EType.Level)
            {
                this.Text = "PREVIEW LEVEL - LAZYSHELL++";
                this.label1.Text = "Level #";
                this.selectIndex.Maximum = 511;
                this.groupBox2.Enabled = false;
            }
            else if (behavior == EType.MineCart)
            {
                this.Text = "PREVIEW MINE CART - LAZYSHELL++";
                this.selectIndex.Enabled = false;
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
            }
            else if (behavior == EType.ActionScript)
            {
                this.Text = "PREVIEW ACTION - LAZYSHELL++";
                this.label1.Text = "Action #";
                this.selectIndex.Maximum = 1023;
                this.groupBox2.Enabled = false;
            }
            else if (behavior == EType.BattleScript)
            {
                this.Text = "PREVIEW BATTLE - LAZYSHELL++";
                this.label1.Text = "Monster #";
                this.selectIndex.Maximum = 255;
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = true;
                this.battleBG.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                this.battleBG.Enabled = true;
                this.battleBG.SelectedIndex = settings.PreviewBattlefield;
            }
            else if (behavior == EType.SPCBattle ||
                behavior == EType.SPCEvent ||
                behavior == EType.SPCTrack)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
            }
            else if (behavior == EType.Sprites)
            {
                this.Text = "PREVIEW SPRITES - LAZYSHELL++";
                this.label1.Text = "Sprite #";
                this.selectIndex.Maximum = 1023;
                this.selectIndex.Value = settings.PreviewSprites;
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                this.battleBG.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                this.battleBG.Enabled = true;
                this.battleBG.SelectedIndex = settings.PreviewBattlefield;
                //this.enableDebug.Checked = true;
                this.enableDebug.Enabled = false;
            }
            else if (behavior == EType.Effects)
            {
                this.Text = "PREVIEW EFFECTS - LAZYSHELL++";
                this.label1.Text = "Effect Index #";
                this.selectIndex.Maximum = 127;
                this.selectIndex.Value = settings.PreviewEffects;
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                this.battleBG.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                this.battleBG.Enabled = true;
                this.battleBG.SelectedIndex = settings.PreviewBattlefield;
                //this.enableDebug.Checked = true;
                this.enableDebug.Enabled = false;
            }
            else if (behavior == EType.AnimationScript)
            {
                this.Text = "PREVIEW ANIMATION - LAZYSHELL++";
                this.label1.Text = "Monster #";
                this.selectIndex.Maximum = 255;
                if (category == (int)AnimScriptType.MonsterSpells)
                    index += 64;
                else if (category == (int)AnimScriptType.Items)
                {
                    index += 96;
                    this.selectIndex.Value = 0;
                    goto Finish;
                }
                else if (category == (int)AnimScriptType.AllySpells
                    || category == (int)AnimScriptType.WeaponMissSounds
                    || category == (int)AnimScriptType.WeaponTimedHitSounds
                    || category == (int)AnimScriptType.WeaponAnimations)
                {
                    this.allyWeapon.Enabled = this.label135.Enabled = false;
                    this.selectIndex.Value = 0;
                    goto Finish;
                }
                foreach (BattleScript script in Model.BattleScripts)
                {
                    foreach (BattleCommand bsc in script.Commands)
                    {
                        switch (category)
                        {
                            case 1:
                                if (bsc.Opcode == 0xEF && bsc.Param1 == index)
                                {
                                    this.selectIndex.Value = script.Index;
                                    goto Finish;
                                }
                                if (bsc.Opcode == 0xF0 && (bsc.Param1 == index || bsc.Param2 == index || bsc.Param3 == index))
                                {
                                    this.selectIndex.Value = script.Index;
                                    goto Finish;
                                }
                                break;
                            case 2:
                                if (bsc.Opcode < 0xE0 && bsc.Opcode == index)
                                {
                                    this.selectIndex.Value = script.Index;
                                    goto Finish;
                                }
                                if (bsc.Opcode == 0xE0 && (bsc.Param1 == index || bsc.Param2 == index || bsc.Param3 == index))
                                {
                                    this.selectIndex.Value = script.Index;
                                    goto Finish;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            Finish:
                this.groupBox1.Enabled = false;
                this.battleBG.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                this.battleBG.Enabled = true;
                this.battleBG.SelectedIndex = settings.PreviewBattlefield;
            }
            // ally stats
            this.allyName.Items.Clear();
            for (int i = 0; i < Model.Characters.Length; i++)
                this.allyName.Items.Add(new string(Model.Characters[i].Name));
            this.allyName.SelectedIndex = 0;
            this.allyWeapon.Items.Clear();
            this.allyWeapon.Items.AddRange(Model.ItemNames.Names);
            this.allyArmor.Items.Clear();
            this.allyArmor.Items.AddRange(Model.ItemNames.Names);
            this.allyAccessory.Items.Clear();
            this.allyAccessory.Items.AddRange(Model.ItemNames.Names);
            //
            this.Updating = true;
            this.allyWeapon.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3));
            this.allyArmor.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 1));
            this.allyAccessory.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 2));
            //
            this.maxOutStats.Checked = settings.PreviewMaxStats;

            alliesInParty.Items.Clear();
            for (int i = 0; i < Model.Characters.Length; i++)
                alliesInParty.Items.Add(Model.CharacterNames.GetUnsortedName(i));

            for (int i = 0; i < Model.Characters.Length; i++)
                alliesInParty.SetItemChecked(i, Bits.GetBit(settings.PreviewAllies, i));

            this.enableDebug.Checked = settings.EnableDebug;
            //
            this.Updating = false;
            //
            romPath = GetRomPath();
            this.initializing = false;
        }
        private bool GetEmulator()
        {
            this.emulatorPath = settings.ZSNESPath; // Gets the saved emulator path
            FileInfo fi;
            try
            {
                fi = new FileInfo(this.emulatorPath);
                if (fi.Exists) // Checks if its a valid path
                    return true;
                else
                    throw new Exception();
            }
            catch
            {
                this.emulatorPath = SelectFile("exe files (*.exe)|*.exe|All files (*.*)|*.*", "C:\\", "Select Emulator");
                if (this.emulatorPath == null || !this.emulatorPath.EndsWith(".exe"))
                    return false;
                fi = new FileInfo(this.emulatorPath);
                if (fi.Exists)
                {
                    settings.ZSNESPath = this.emulatorPath;
                    settings.Save();
                    return true;
                }
                else
                    return false;
            }
        }
        private string SelectFile(string filter, string initDir, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.InitialDirectory = initDir;
            dialog.Title = title;
            return (dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null;
        }
        private void UpdateGUI()
        {
            this.emuPathTextBox.Text = this.emulatorPath;
            this.romPathTextBox.Text = GetRomPath();
            this.selectIndex.Value = this.selectNum;
            this.eventListBox.Items.Clear();
            Entrance ent;
            for (int i = 0; i < eventTriggers.Count; i++)
            {
                ent = eventTriggers[i];
                if (this.behavior == EType.EventScript)
                {
                    if (ent.Flag)
                        this.eventListBox.Items.Add(
                            "Enter event (x:" + ent.X.ToString() +
                            " y:" + ent.Y.ToString() +
                            " z:" + ent.Z.ToString() +
                            ") " + Lists.Numerize(Lists.LevelNames, ent.Destination));
                    else if (ent.MSG != "NPC") // A run event
                        this.eventListBox.Items.Add(
                            "Event field (x:" + ent.X.ToString() +
                            " y:" + ent.Y.ToString() +
                            " z:" + ent.Z.ToString() +
                            ") " + Lists.Numerize(Lists.LevelNames, ent.Destination));
                    else if (ent.MSG == "NPC")
                        this.eventListBox.Items.Add(
                            "NPC #" + ent.Source + " event (x:" + ent.X.ToString() +
                            " y:" + ent.Y.ToString() +
                            " z:" + ent.Z.ToString() +
                            ") " + Lists.Numerize(Lists.LevelNames, ent.Destination));
                }
                else if (this.behavior == EType.Level)
                {
                    this.eventListBox.Items.Add(
                        "(x:" + ent.X.ToString() +
                        " y:" + ent.Y.ToString() +
                        " z:" + ent.Z.ToString() +
                        ") " + Lists.Numerize(Lists.LevelNames, ent.Source));
                }
                else if (this.behavior == EType.MineCart)
                {
                }
                else if (this.behavior == EType.ActionScript)
                {
                    if (ent.Flag)
                        this.eventListBox.Items.Add(
                            "NPC #" + ent.Source.ToString() +
                            " (x:" + ent.X.ToString() +
                            " y:" + ent.Y.ToString() +
                            " z:" + ent.Z.ToString() +
                            ") " + Lists.Numerize(Lists.LevelNames, ent.Destination));
                    else
                        this.eventListBox.Items.Add(
                            "NPC #" + ent.Source.ToString() +
                            " (x:" + ent.X.ToString() +
                            " y:" + ent.Y.ToString() +
                            " z:" + ent.Z.ToString() +
                            ") " + Lists.Numerize(Lists.LevelNames, ent.Destination));
                }
                else if (this.behavior == EType.BattleScript)
                {
                    this.eventListBox.Items.Add(ent.MSG);
                }
            }
            if (this.eventListBox.Items.Count > 0)
            {
                if (this.behavior == EType.BattleScript && this.eventListBox.Items.Count > 1)
                    this.eventListBox.SelectedIndex = 1;
                else
                    this.eventListBox.SelectedIndex = 0;
            }
        }
        // launching
        private bool Prelaunch()
        {
            this.rom = GeneratePreviewRom();
            this.savestate = GeneratePreviewSaveState();
            return (rom == savestate == true);
        }
        private void Launch()
        {
            settings.PreviewArguments = zsnesArgs.Text;
            settings.Save();
            if (rom && emulator && savestate && eventchoice)
                LaunchEmulator(this.emulatorPath, this.romPath, snes9x ? snes9xArgs.Text : zsnesArgs.Text);
            else
            {
                if (!rom)
                    MessageBox.Show("There was a problem generating the preview rom", "LAZYSHELL++");
                if (!emulator)
                    MessageBox.Show("There is a problem with the emulator.\nSNES9X and ZSNESW are the only emulators supported.", "LAZYSHELL++");
                if (!savestate)
                    MessageBox.Show("There was a problem generating the preview save state.", "LAZYSHELL++");
                if (!eventchoice)
                    MessageBox.Show("An invalid destination was selected to preview.", "LAZYSHELL++");
            }
        }
        private bool GeneratePreviewRom()
        {
            bool ret = false;
            // Make backup of current data                
            byte[] backup = Bits.Copy(Model.ROM);
            bool[] editGraphicSets = Bits.Copy(Model.EditGraphicSets);
            bool[] editTilesets = Bits.Copy(Model.EditTilesets);
            bool[] editTilemaps = Bits.Copy(Model.EditTilemaps);
            bool[] editSolidityMaps = Bits.Copy(Model.EditSolidityMaps);
            //
            if (!((this.behavior == EType.EventScript || this.behavior == EType.ActionScript) &&
                this.eventListBox.SelectedIndex < 0 || this.eventListBox.SelectedIndex >= this.eventTriggers.Count))
            {
                switch (this.behavior)
                {
                    case EType.BattleScript:
                        Model.Program.Monsters.Assemble();
                        break;
                    case EType.EventScript:
                    case EType.ActionScript:
                        Model.Program.EventScripts.Assemble();
                        break;
                    case EType.Level:
                        Model.Program.Levels.Assemble();
                        break;
                    case EType.MineCart:
                        Model.Program.MiniGames.Assemble();
                        break;
                    case EType.SPCTrack:
                    case EType.SPCEvent:
                    case EType.SPCBattle:
                        Model.Program.Audio.Assemble(false);
                        if (this.behavior == EType.SPCEvent)
                            soundFX = Bits.GetBytes(Model.ROM, 0x042826, 0x1600);
                        else if (this.behavior == EType.SPCBattle)
                            soundFX = Bits.GetBytes(Model.ROM, 0x043E26, 0x1600);
                        break;
                    case EType.Sprites:
                        Model.Program.Sprites.Assemble();
                        break;
                    case EType.Effects:
                        Model.Program.Effects.Assemble();
                        break;
                }
                PrepareImage();
                // Backup filename
                string fileNameBackup = Model.FileName;
                // Generate preview name;
                this.romPath = GetRomPath();
                string oldFileName = Model.FileName;
                Model.FileName = romPath;
                ret = Model.WriteRom(false); // Save temp rom WITHOUT backing it up
                // Restore name
                Model.FileName = oldFileName;
            }
            //Restore Rom Image
            backup.CopyTo(Model.ROM, 0);
            editGraphicSets.CopyTo(Model.EditGraphicSets, 0);
            editTilesets.CopyTo(Model.EditTilesets, 0);
            editTilemaps.CopyTo(Model.EditTilemaps, 0);
            editSolidityMaps.CopyTo(Model.EditSolidityMaps, 0);
            return ret;
        }
        private bool GeneratePreviewSaveState()
        {
            try
            {
                snes9x = Do.Contains(this.emulatorPath, "snes9x", StringComparison.CurrentCultureIgnoreCase);
                string ext = snes9x ? ".000" : ".zst";
                FileInfo fInfo = new FileInfo(Model.GetPathWithoutFileName(this.emulatorPath) + "RomPreviewBaseSave" + ext);
                if (!fInfo.Exists)
                {
                    byte[] lc;
                    if (snes9x)
                        lc = Resources.RomPreviewBaseSave;
                    else
                        lc = Resources.RomPreviewBaseSave1;
                    File.WriteAllBytes(Model.GetPathWithoutFileName(this.emulatorPath) + "RomPreviewBaseSave" + ext, lc);
                }
                FileStream fs = fInfo.OpenRead();
                BinaryReader br = new BinaryReader(fs);
                byte[] state = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                int offset = 0;
                // modify zst if needed
                if (maxOutStats.Checked)
                    maxStats.CopyTo(state, snes9x ? 0x30487 : 0x20413);
                else
                {
                    foreach (Character character in Model.Characters)
                    {
                        int hp = character.StartingCurrentHP;
                        int attack = character.StartingAttack;
                        int defense = character.StartingDefense;
                        int mgAttack = character.StartingMgAttack;
                        int mgDefense = character.StartingMgDefense;
                        int experience = character.StartingExperience;
                        bool[] spells = new bool[128];
                        for (int i = 0; i < character.StartingMagic.Length; i++)
                            spells[i] = character.StartingMagic[i];
                        //
                        foreach (Character.LevelUp level in character.Levels)
                        {
                            if (level == null) continue;
                            if (level.Index > this.level.Value) break;
                            hp += level.HpPlus;
                            attack += level.AttackPlus;
                            defense += level.DefensePlus;
                            mgAttack += level.MgAttackPlus;
                            mgDefense += level.MgDefensePlus;
                            // used balanced level-up bonus
                            if (level.AttackPlusBonus > level.MgAttackPlusBonus)
                            {
                                attack += level.AttackPlusBonus;
                                defense += level.DefensePlusBonus;
                            }
                            else if (level.MgAttackPlusBonus > level.AttackPlusBonus)
                            {
                                mgAttack += level.MgAttackPlusBonus;
                                mgDefense += level.MgDefensePlusBonus;
                            }
                            else
                                hp += level.HpPlusBonus;
                            experience = level.ExpNeeded;
                            spells[level.SpellLearned] = level.SpellLearned != 0xFF;
                        }
                        offset = snes9x ? 0x30487 : 0x20413;
                        offset += character.Index * 20;
                        state[offset++] = Math.Max(character.StartingLevel, (byte)this.level.Value);
                        Bits.SetShort(state, offset, (ushort)hp); offset += 2;
                        Bits.SetShort(state, offset, (ushort)hp); offset += 2;
                        offset++;
                        state[offset++] = (byte)attack;
                        state[offset++] = (byte)defense;
                        state[offset++] = (byte)mgAttack;
                        state[offset++] = (byte)mgDefense;
                        Bits.SetShort(state, offset, experience); offset += 2;
                        state[offset++] = Bits.StringToByte(settings.AllyEquipment, character.Index * 3);
                        state[offset++] = Bits.StringToByte(settings.AllyEquipment, character.Index * 3 + 1);
                        state[offset++] = Bits.StringToByte(settings.AllyEquipment, character.Index * 3 + 2);
                        offset++;   // unused byte
                        double p = 0;
                        for (int i = 0; i < 32; i++, p += 0.125)
                            Bits.SetBit(state, offset + (int)p, i & 7, spells[i]);
                    }
                }
                // if previewing item, add item to inventory
                if (behavior == EType.AnimationScript && category == (int)AnimScriptType.Items)
                    state[snes9x ? 0x30509 : 0x20495] = (byte)index;
                // else add new game inventory
                else
                {
                    foreach (Slot ind in Model.Slots)
                        state[snes9x ? 0x30509 + ind.Index: 0x20495 + ind.Index] = (byte)ind.Item;
                }

                if (behavior == EType.AnimationScript && category == (int)AnimScriptType.AllySpells)
                {
                    foreach (Character character in Model.Characters)
                    {
                        offset = snes9x ? 0x30487 : 0x20413;
                        offset += (character.Index + 1) * 20 - 4;
                        bool[] spells = new bool[32];
                        spells[index] = true;
                        double p = 0;
                        for (int i = 0; i < 32; i++, p += 0.125)
                            Bits.SetBit(state, offset + (int)p, i & 7, spells[i]);
                    }
                }
                else if (behavior == EType.AnimationScript
                    && category == (int)AnimScriptType.WeaponMissSounds
                    || category == (int)AnimScriptType.WeaponTimedHitSounds
                    || category == (int)AnimScriptType.WeaponAnimations)
                {
                    foreach (Character character in Model.Characters)
                    {
                        offset = snes9x ? 0x30487 : 0x20413;
                        offset += (character.Index + 1) * 20 - 8;
                        state[offset] = (byte)index;
                    }
                }

                if (behavior == EType.SPCEvent ||
                    behavior == EType.SPCBattle)
                    Buffer.BlockCopy(soundFX, 0, state, snes9x ? 0x5BDA4 : 0x33C13, 0x1600);
                    //
                offset = snes9x ? 0x53C9D : 0x41533;
                byte allyCount = 0;
                for (byte i = 0, a = 0; i < alliesInParty.Items.Count; i++)
                {
                    if (alliesInParty.GetItemChecked(i))
                    {
                        state[offset + 0x33 + a] = (byte)(i);
                        a++; allyCount++;
                    }
                }
                state[offset + 0x32] = allyCount;
                //state[offset + 0x33] = 0;   // Mario always in party and in first slot
                state[offset + 0x3F] &= 0xFC;
                state[offset + 0x3F] |= Math.Min((byte)3, allyCount);
                //
                fInfo = new FileInfo(GetStatePath());
                fs = fInfo.OpenWrite();
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(state);
                bw.Close();
                fs.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool PrepareImage()
        {
            Entrance ent = new Entrance();
            int index = this.eventListBox.SelectedIndex;
            if ((this.behavior == EType.EventScript ||
                this.behavior == EType.ActionScript ||
                this.behavior == EType.BattleScript) &&
                index < 0 || index >= this.eventTriggers.Count)
            {
                this.eventchoice = false;
                return false;
            }
            //
            LevelExits storage = new LevelExits();
            storage.New(0, new Point(0, 0));
            storage.CurrentExit = 0;
            if (this.eventTriggers.Count > 0)
            {
                ent = eventTriggers[index];
                storage.DstF = ent.F;
                storage.ShowMessage = ent.ShowMessage;
            }
            else
            {
                storage.DstF = 7;
                storage.ShowMessage = false;
            }
            if (this.behavior == EType.Level)
                storage.Destination = Math.Min((ushort)509, (ushort)this.selectIndex.Value);
            else if (this.behavior == EType.EventScript || this.behavior == EType.ActionScript)
                storage.Destination = ent.Destination;
            storage.ExitType = 0;
            storage.Y = 10;
            storage.DstX = (byte)this.adjustX.Value;
            storage.DstY = (byte)this.adjustY.Value;
            storage.DstZ = (byte)this.adjustZ.Value;
            //
            ushort save = Model.Levels[storage.Destination].LevelEvents.EntranceEvent;
            byte saveMusic = Model.Levels[storage.Destination].LevelEvents.Music;
            Model.Levels[storage.Destination].LevelEvents.EntranceEvent = 0;
            //
            if (this.behavior == EType.BattleScript)
            {
                PrepareBattlePack(ent.Source);
                byte[] eventData = new byte[] { 0x4A, 0x00, 0x00, 0x00, 0xFE };
                eventData[3] = (byte)this.battleBG.SelectedIndex;
                eventData.CopyTo(Model.ROM, 0x1E0C00);
            }
            else if (this.behavior == EType.Level)
            {
                byte[] command = new byte[] { 0xD0, 0, 0 };
                Bits.SetShort(command, 1, save);
                command.CopyTo(Model.ROM, 0x1E0C00);
            }
            else if (this.behavior == EType.MineCart)
            {
                byte[] eventData = new byte[] { 0xFD, 0x4E };
                eventData.CopyTo(Model.ROM, 0x1E0C00);
                switch (this.selectNum)
                {
                    case 0: Model.ROM[0x0393EA] = 1; break;
                    case 1: Model.ROM[0x0393EA] = 3; break;
                    case 2: Model.ROM[0x0393EA] = 2; break;
                    case 3: Model.ROM[0x0393EA] = 4; break;
                }
            }
            else if (this.behavior == EType.AnimationScript)
            {
                int monsterNum = (int)selectIndex.Value;
                PrepareBattlePack(0xFFFF);
                byte[] eventData = new byte[] { 0x4A, 0x00, 0x00, 0x00, 0xFE };
                eventData[3] = (byte)this.battleBG.SelectedIndex;
                eventData.CopyTo(Model.ROM, 0x1E0C00);
                if (category == (int)AnimScriptType.MonsterSpells
                    || category == (int)AnimScriptType.MonsterAttacks)
                {
                    int pointer = Bits.GetShort(Model.ROM, 0x390026 + monsterNum * 2);
                    int offset = 0x390000 + pointer;
                    Model.ROM[offset + 2] = 255;
                    Model.ROM[offset + 7] = 255;
                    pointer = Bits.GetShort(Model.ROM, 0x3930AA + (monsterNum * 2));
                    offset = 0x390000 + pointer;
                    if (category == (int)AnimScriptType.MonsterSpells)
                        new byte[] { 0xEF, (byte)this.index, 0xEC, 0xFF, 0xFF }.CopyTo(Model.ROM, offset);
                    else if (category == (int)AnimScriptType.MonsterAttacks)
                        new byte[] { (byte)this.index, 0xEC, 0xFF, 0xFF }.CopyTo(Model.ROM, offset);
                }
            }
            else if (this.behavior == EType.SPCTrack)
            {
                Model.Levels[storage.Destination].LevelEvents.Music = (byte)this.index;
            }
            else if (this.behavior == EType.SPCEvent)
            {
                Model.Levels[storage.Destination].LevelEvents.Music = 53;
                new byte[] { 0x9C, (byte)this.index }.CopyTo(Model.ROM, 0x1E0C00);
            }
            else if (this.behavior == EType.SPCBattle)
            {
                Model.Levels[0].LevelEvents.Music = 53;
                new byte[] { 0x9C, (byte)this.index }.CopyTo(Model.ROM, 0x1E0C00);
            }
            else if (this.behavior == EType.Sprites
                || this.behavior == EType.Effects)
            {
                PrepareBattlePack(0xFFFF);
                byte[] eventData = new byte[] { 0x4A, 0x00, 0x00, 0x00, 0xFE };
                eventData[3] = (byte)this.battleBG.SelectedIndex;
                eventData.CopyTo(Model.ROM, 0x1E0C00);
            //debug menu stuff
                Model.ROM[0x0106B6] = 0x80; //enables debug menu
                Model.ROM[0x0106AF] = 0x80; //forces debug menu to open w/out pressing Start button
                //
                byte[] InjectedCodePointer = new byte[] { 0x30, 0xB1 }; //pointer (for JSR) to injected code
                InjectedCodePointer.CopyTo(Model.ROM, 0x01AD85);
                //Sets up the boilerplate code that allows us to jump to a custom index
                byte[] InjectedCode = new byte[] {  //
                    0xA9, 0x00, 0x00,           //LDA 0000
                    0x8F, 0x2A, 0x05, 0x7E,     //STA 7E:052A (sets OBJ's sprite number to zero)
                    0x20, 0x00, 0x00,           //JSR to nothing
                    0x60 };
                InjectedCode[1] = (byte)((int)selectIndex.Value & 255);
                InjectedCode[2] = (byte)((int)selectIndex.Value >> 8);
                // sets up pointers to open a debug menu option
                byte[] DebugMenuOption = new byte[] { 0x1A, 0x07 };
                byte[] OBJ = new byte[] { 0x1A, 0x07 };     //pointer (for JSR) to OBJ Debug Menu Option
                byte[] EFFECTS = new byte[] { 0x25, 0x07 }; //pointer (for JSR) to EFFECTS Debug Menu Option
                if (this.behavior == EType.Sprites) DebugMenuOption = OBJ;
                else if (this.behavior == EType.Effects) DebugMenuOption = EFFECTS;
                //Finish
                DebugMenuOption.CopyTo(InjectedCode, 8);
                InjectedCode.CopyTo(Model.ROM, 0x01B130);
                //This prevents the menu number from being cleared in the beginning (allowing us to set a custom one)
                byte[] blank = new byte[] { 0xEA, 0xEA, 0xEA }; //wipes code with NOP
                blank.CopyTo(Model.ROM, 0x01A1CA);      //(for Animations) Removes STA 7E:052A
                blank.CopyTo(Model.ROM, 0x01A3F9);      //(for Animations) Removes STA 7E:052A
            }
            else
            {
                Model.Levels[storage.Destination].LevelEvents.EntranceEvent = save;
                Model.Levels[storage.Destination].LevelEvents.Music = saveMusic;
            }
            //
            SaveLevelExitEvents();
            Model.Levels[storage.Destination].LevelEvents.EntranceEvent = save;
            Model.Levels[storage.Destination].LevelEvents.Music = saveMusic;
            //
            storage.Exit.Assemble(0x1DF000);
            this.eventchoice = true;
            //
            if (enableDebug.Checked)
                Model.ROM[0x0106AF] = 0x80;
            //
            return true;
        }
        private void PrepareBattlePack(int formationNum)
        {
            if (formationNum == 0xFFFF)
            {
                if (this.behavior != EType.AnimationScript)
                {
                    Model.Monsters[0].EntranceStyle = 0;
                    int length = 0xA1D1;
                    Model.Monsters[0].Assemble(ref length);
                }
                int formationIndex = 4;
                byte monster1 = Model.Formations[formationIndex].Monsters[0];
                byte xcoord = Model.Formations[formationIndex].X[0];
                byte ycoord = Model.Formations[formationIndex].Y[0];
                Model.Formations[formationIndex].X[0] = 167;
                Model.Formations[formationIndex].Y[0] = 135;
                Model.Formations[formationIndex].Monsters[0] = 0;
                if (this.behavior != EType.Sprites)
                    Model.Formations[formationIndex].Monsters[0] = (byte)this.selectIndex.Value;
                bool[] uses = new bool[8];
                uses[0] = Model.Formations[formationIndex].Use[0];
                uses[1] = Model.Formations[formationIndex].Use[1];
                uses[2] = Model.Formations[formationIndex].Use[2];
                uses[3] = Model.Formations[formationIndex].Use[3];
                uses[4] = Model.Formations[formationIndex].Use[4];
                uses[5] = Model.Formations[formationIndex].Use[5];
                uses[6] = Model.Formations[formationIndex].Use[6];
                uses[7] = Model.Formations[formationIndex].Use[7];
                Model.Formations[formationIndex].Use[0] = true;
                Model.Formations[formationIndex].Use[1] = false;
                Model.Formations[formationIndex].Use[2] = false;
                Model.Formations[formationIndex].Use[3] = false;
                Model.Formations[formationIndex].Use[4] = false;
                Model.Formations[formationIndex].Use[5] = false;
                Model.Formations[formationIndex].Use[6] = false;
                Model.Formations[formationIndex].Use[7] = false;
                Model.Formations[formationIndex].Assemble();
                Model.Formations[formationIndex].Monsters[0] = monster1;
                Model.Formations[formationIndex].X[0] = xcoord;
                Model.Formations[formationIndex].Y[0] = ycoord;
                Model.Formations[formationIndex].Use[0] = uses[0];
                Model.Formations[formationIndex].Use[1] = uses[1];
                Model.Formations[formationIndex].Use[2] = uses[2];
                Model.Formations[formationIndex].Use[3] = uses[3];
                Model.Formations[formationIndex].Use[4] = uses[4];
                Model.Formations[formationIndex].Use[5] = uses[5];
                Model.Formations[formationIndex].Use[6] = uses[6];
                Model.Formations[formationIndex].Use[7] = uses[7];
                formationNum = formationIndex;
            }
            FormationPack sfp = Model.FormationPacks[0];
            ushort formation1 = sfp.Formations[0];
            ushort formation2 = sfp.Formations[0];
            ushort formation3 = sfp.Formations[0];
            sfp.Formations[0] = (ushort)formationNum;
            sfp.Formations[1] = (ushort)formationNum;
            sfp.Formations[2] = (ushort)formationNum;
            sfp.Assemble();
            sfp.Formations[0] = formation1;
            sfp.Formations[1] = formation2;
            sfp.Formations[2] = formation3;
        }
        private void SaveLevelExitEvents()
        {
            int offsetStart = 0xE400;
            for (int i = 0; i < Model.Levels.Length; i++)
                Model.Levels[i].LevelEvents.Assemble(ref offsetStart);
        }
        private string GetRomPath()
        {
            if (settings.PreviewDynamicRomName)
                return Model.GetPathWithoutFileName(this.emulatorPath) + "PreviewROM_" + Model.GetFileNameWithoutPath();
            else
                return Model.GetPathWithoutFileName(this.emulatorPath) + "PreviewRom.smc";
        }
        private string GetStatePath()
        {
            string ext = snes9x ? ".000" : ".zst";
            if (settings.PreviewDynamicRomName)
                return Model.GetPathWithoutFileName(this.emulatorPath) + "PreviewROM_" + Model.GetFileNameWithoutPathOrExtension() + ext;
            else
                return Model.GetPathWithoutFileName(this.emulatorPath) + "PreviewRom" + ext;
        }
        private void LaunchEmulator(string emulatorPath, string romPath, string args)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = emulatorPath;
            proc.StartInfo.Arguments = args + " " + "\"" + romPath + "\"";
            proc.Start();
            if (snes9x)
            {
                //PostMessage(proc.MainWindowHandle, WM_KEYDOWN, (IntPtr)Keys.F1, (IntPtr)0x003B0001);
                //PostMessage(proc.MainWindowHandle, WM_KEYUP, (IntPtr)Keys.F1, (IntPtr)0x003B0001);
            }
        }
        // scanning data
        private void ScanForEvents()
        {
            this.eventTriggers.Clear();
            ScanForNPCEvents();
            ScanForEnterEvents();
            ScanForRunEvents();
        }
        private void ScanForEnterEvents()
        {
            foreach (Level lvl in Model.Levels)
            {
                if (lvl.LevelEvents.EntranceEvent == this.selectNum)
                {
                    ScanForEntrancesToLevel(lvl.Index);
                }
            }
        }
        private void ScanForRunEvents()
        {
            Entrance ent;
            foreach (Level lvl in Model.Levels) // For every level
            {
                foreach (Event EVENT in lvl.LevelEvents.Events)
                {
                    if (EVENT.RunEvent == this.selectNum) // If this exit points to the level we want
                    {
                        ent = new Entrance();
                        ent.Destination = (ushort)lvl.Index;
                        ent.X = EVENT.X;
                        ent.Y = EVENT.Y;
                        ent.Z = EVENT.Z;
                        ent.F = EVENT.F;
                        ent.ShowMessage = false;
                        ent.Flag = false;
                        eventTriggers.Add(ent); // Add the event trigger
                    }
                }
            }
        }
        private void ScanForEntrancesToLevel(int lvlNum)
        {
            Entrance ent;
            foreach (Level lvl in Model.Levels) // For every level
            {
                foreach (Exit exit in lvl.LevelExits.Exits)
                {
                    if (exit.Destination == lvlNum) // If this exit points to the level we want
                    {
                        ent = new Entrance();
                        ent.Source = (ushort)lvl.Index;
                        ent.Destination = (ushort)exit.Destination;
                        ent.X = exit.DstX;
                        ent.Y = exit.DstY;
                        ent.Z = exit.DstZ;
                        ent.F = exit.DstFace;
                        ent.ShowMessage = exit.ShowMessage;
                        ent.Flag = true; // Indicates an enter event
                        eventTriggers.Add(ent); // Add the event trigger
                    }
                }
            }
        }
        private void ScanForNPCEvents()
        {
            Entrance ent;
            foreach (Level lvl in Model.Levels) // For every level
            {
                int index = 0;
                for (int i = 0; i < lvl.LevelNPCs.Count; i++, index++)
                {
                    NPC npc = lvl.LevelNPCs.Npcs[i];
                    if (npc.EngageType == 2) // skip if battle trigger
                        continue;
                    if (npc.EventORpack + npc.PropertyB == this.selectNum)
                    {
                        ent = new Entrance();
                        ent.Destination = (ushort)lvl.Index;
                        ent.X = npc.X;
                        ent.Y = npc.Y;
                        ent.Z = npc.Z;
                        ent.MSG = "NPC";
                        ent.ShowMessage = false;
                        ent.Source = index;
                        ent.Flag = false;
                        eventTriggers.Add(ent); // Add the event trigger
                    }
                    for (int c = 0; c < npc.Count; c++, index++)
                    {
                        NPC clone = npc.Clones[c];
                        if (npc.EventORpack + clone.PropertyB == this.selectNum)
                        {
                            ent = new Entrance();
                            ent.Destination = (ushort)lvl.Index;
                            ent.X = clone.X;
                            ent.Y = clone.Y;
                            ent.Z = clone.Z;
                            ent.MSG = "NPC";
                            ent.ShowMessage = false;
                            ent.Source = index + 1;
                            ent.Flag = false;
                            eventTriggers.Add(ent); // Add the event trigger
                        }
                    }
                }
            }
        }
        private void ScanForActionReferences(int actionNum)
        {
            Entrance ent;
            foreach (Level lvl in Model.Levels) // For every level
            {
                int counter = 0;
                foreach (NPC npc in lvl.LevelNPCs.Npcs) // For every NPC in each level
                {
                    if (npc.Movement + npc.PropertyC == actionNum) // If this NPC has our action #
                    {
                        ent = new Entrance();
                        ent.Source = counter++;
                        ent.Destination = (ushort)lvl.Index;
                        ent.X = (byte)((npc.X + 2) & 0x3F);
                        ent.Y = (byte)((npc.Y + 2) & 0x7F);
                        ent.Z = npc.Z;
                        ent.F = 7;
                        ent.ShowMessage = false;
                        ent.Flag = true; // Indicates an NPC and not an Instance
                        eventTriggers.Add(ent); // Add the event trigger
                    }
                    foreach (NPC.Clone instance in npc.Clones) // test all instances
                    {
                        if (instance.Movement + instance.PropertyC == actionNum)
                        {
                            ent = new Entrance();
                            ent.Source = counter++;
                            ent.Destination = (ushort)lvl.Index;
                            ent.X = (byte)((instance.X + 2) & 0x3F);
                            ent.Y = (byte)((instance.Y + 2) & 0x7F);
                            ent.Z = instance.Z;
                            ent.F = 7;
                            ent.ShowMessage = false;
                            ent.Flag = false; // Indicates an Instance
                            eventTriggers.Add(ent); // Add the event trigger
                        }
                    }
                }
            }
        }
        private bool ScanFormation(int monsterNum, Formation sfm)
        {
            if (sfm.Monsters[0] == monsterNum && sfm.Use[0])
                return true;
            else if (sfm.Monsters[1] == monsterNum && sfm.Use[1])
                return true;
            else if (sfm.Monsters[2] == monsterNum && sfm.Use[2])
                return true;
            else if (sfm.Monsters[3] == monsterNum && sfm.Use[3])
                return true;
            else if (sfm.Monsters[4] == monsterNum && sfm.Use[4])
                return true;
            else if (sfm.Monsters[5] == monsterNum && sfm.Use[5])
                return true;
            else if (sfm.Monsters[6] == monsterNum && sfm.Use[6])
                return true;
            else if (sfm.Monsters[7] == monsterNum && sfm.Use[7])
                return true;
            return false;
        }
        private void ScanFormationsForMonster(int monsterNum)
        {
            Entrance ent = new Entrance();
            Formation[] formations = Model.Formations;
            ent.Source = 0xFFFF;
            ent.MSG = "Default: " + Model.MonsterNames.GetUnsortedName(monsterNum);
            eventTriggers.Add(ent);
            for (int i = 0; i < formations.Length; i++)
            {
                if (ScanFormation(monsterNum, formations[i]))
                {
                    ent = new Entrance();
                    ent.Source = (ushort)i;
                    ent.MSG = "Formation: " + i.ToString() + " - " + formations[i].ToString();
                    ent.Destination = 0;
                    ent.X = 0;
                    ent.Y = 0;
                    ent.Z = 0;
                    ent.F = 0;
                    eventTriggers.Add(ent);
                }
            }
        }
        #endregion
        #region Event Handlers
        private void linkLabelZSNES_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://zsnes-docs.sourceforge.net/html/advanced.htm#command_line");
        }
        private void linkLabelSNES9X_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.snes9x.com/phpbb2/viewtopic.php?t=3020");
        }
        private void defaultZSNES_Click(object sender, EventArgs e)
        {
            this.zsnesArgs.Text = settings.PreviewArgsDefault;
        }
        private void defaultSNES9X_Click(object sender, EventArgs e)
        {
            this.snes9xArgs.Text = "";
        }
        private void dynamicROMPath_CheckedChanged(object sender, EventArgs e)
        {
            this.dynamicROMPath.ForeColor = dynamicROMPath.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            if (!this.initializing)
            {
                settings.PreviewDynamicRomName = dynamicROMPath.Checked;
                settings.Save();
                this.romPath = GetRomPath();
            }
            UpdateGUI();
        }
        private void changeEmuButton_Click(object sender, EventArgs e)
        {
            string path = SelectFile("exe files (*.exe)|*.exe|All files (*.*)|*.*", settings.LastDirectory, "Select Emulator");
            if (path == null || !path.EndsWith(".exe"))
                return;
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                settings.LastDirectory = Path.GetDirectoryName(path);
                this.emulatorPath = path;
                this.emulator = true;
                settings.ZSNESPath = this.emulatorPath;
                settings.Save();
                UpdateGUI();
            }
        }
        //
        private void eventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.eventListBox.SelectedIndex;
            if (index < 0 || index >= this.eventTriggers.Count)
                return;
            // Set the XYZ values
            this.adjustX.Value = eventTriggers[index].X;
            this.adjustY.Value = eventTriggers[index].Y;
            this.adjustZ.Value = eventTriggers[index].Z;
        }
        private void selectNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.selectNum = (int)selectIndex.Value;
            if (this.behavior == EType.EventScript)
                ScanForEvents();
            else if (this.behavior == EType.Level)
            {
                this.eventTriggers.Clear();
                ScanForEntrancesToLevel((int)selectIndex.Value);
            }
            else if (this.behavior == EType.ActionScript)
            {
                this.eventTriggers.Clear();
                ScanForActionReferences((int)selectIndex.Value);
            }
            else if (this.behavior == EType.BattleScript)
            {
                this.eventTriggers.Clear();
                ScanFormationsForMonster((int)this.selectIndex.Value);
            }
            UpdateGUI();
        }
        private void battleBGListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            settings.PreviewBattlefield = battleBG.SelectedIndex;
            settings.Save();
        }
        //
        private void alliesInParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            byte bits = settings.PreviewAllies;
            for (int i = 0; i < 5; i++)
                Bits.SetBit(ref bits, i, alliesInParty.GetItemChecked(i));
            settings.PreviewAllies = bits;
            settings.Save();
        }
        private void level_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            settings.PreviewLevel = (int)level.Value;
            settings.Save();
        }
        private void allyName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                Model.FontMenu, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void allyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            this.Updating = true;
            this.allyWeapon.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3));
            this.allyArmor.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 1));
            this.allyAccessory.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 2));
            this.Updating = false;
        }
        private void allyWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            byte number = (byte)Model.ItemNames.GetUnsortedIndex(allyWeapon.SelectedIndex);
            settings.AllyEquipment = settings.AllyEquipment.Remove((allyName.SelectedIndex * 3) * 2, 2);
            settings.AllyEquipment = settings.AllyEquipment.Insert((allyName.SelectedIndex * 3) * 2, number.ToString("X2"));
            settings.Save();
        }
        private void allyArmor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            byte number = (byte)Model.ItemNames.GetUnsortedIndex(allyArmor.SelectedIndex);
            settings.AllyEquipment = settings.AllyEquipment.Remove((allyName.SelectedIndex * 3 + 1) * 2, 2);
            settings.AllyEquipment = settings.AllyEquipment.Insert((allyName.SelectedIndex * 3 + 1) * 2, number.ToString("X2"));
            settings.Save();
        }
        private void allyAccessory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            byte number = (byte)Model.ItemNames.GetUnsortedIndex(allyAccessory.SelectedIndex);
            settings.AllyEquipment = settings.AllyEquipment.Remove((allyName.SelectedIndex * 3 + 2) * 2, 2);
            settings.AllyEquipment = settings.AllyEquipment.Insert((allyName.SelectedIndex * 3 + 2) * 2, number.ToString("X2"));
            settings.Save();
        }
        private void maxOutStats_CheckedChanged(object sender, EventArgs e)
        {
            maxOutStats.ForeColor = maxOutStats.Checked ? SystemColors.ControlText : SystemColors.ControlDark;
            level.Enabled = !maxOutStats.Checked;
            allyName.Enabled = !maxOutStats.Checked;
            allyWeapon.Enabled = !maxOutStats.Checked;
            allyArmor.Enabled = !maxOutStats.Checked;
            allyAccessory.Enabled = !maxOutStats.Checked;
            if (this.Updating || initializing)
                return;
            settings.PreviewMaxStats = maxOutStats.Checked;
            settings.Save();
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to clear all equipement for all allies. Go ahead with process?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            settings.AllyEquipment = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            settings.Save();
            this.allyWeapon.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3));
            this.allyArmor.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 1));
            this.allyAccessory.SelectedIndex = Model.ItemNames.GetSortedIndex(Bits.StringToByte(settings.AllyEquipment, allyName.SelectedIndex * 3 + 2));
        }
        private void enableDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Updating || initializing)
                return;
            settings.EnableDebug = enableDebug.Checked;
            settings.Save();
        }
        //
        private void launchButton_Click(object sender, EventArgs e)
        {
            if (Prelaunch())
                Launch();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        public struct Entrance
        {
            public int Source;
            public bool ShowMessage;
            public byte X;
            public byte Y;
            public byte Z;
            public byte F;
            public ushort Destination;
            public bool Flag;
            public string MSG;
        }
    }
}
