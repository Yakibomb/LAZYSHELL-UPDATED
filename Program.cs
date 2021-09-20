using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using LAZYSHELL.Properties;
using LAZYSHELL.Patches;
using LAZYSHELL.ScriptsEditor;

namespace LAZYSHELL
{
    public class Program
    {
        // class variables and accessors
        private Settings settings = Settings.Default;
        private bool dockEditors;
        public bool DockEditors { get { return dockEditors; } set { dockEditors = value; } }
        #region Editor Windows
        private AlliesEditor allies; public AlliesEditor Allies { get { return allies; } }
        private AnimationScripts animations; public AnimationScripts Animations { get { return animations; } }
        private AttacksEditor attacks; public AttacksEditor Attacks { get { return attacks; } }
        private Audio audio; public Audio Audio { get { return audio; } }
        private Battlefields battlefields; public Battlefields Battlefields { get { return battlefields; } }
        private Dialogues dialogues; public Dialogues Dialogues { get { return dialogues; } }
        private Effects effects; public Effects Effects { get { return effects; } }
        private FormationsEditor formations; public FormationsEditor Formations { get { return formations; } }
        private ItemsEditor items; public ItemsEditor Items { get { return items; } }
        private Levels levels; public Levels Levels { get { return levels; } }
        private Intro intro; public Intro Intro { get { return intro; } }
        private MenusEditor menus; public MenusEditor Menus { get { return menus; } }
        private MiniGames miniGames; public MiniGames MiniGames { get { return miniGames; } }
        private Monsters monsters; public Monsters Monsters { get { return monsters; } }
        private EventScripts eventScripts; public EventScripts EventScripts { get { return eventScripts; } }
        private Sprites sprites; public Sprites Sprites { get { return sprites; } }
        private WorldMaps worldMaps; public WorldMaps WorldMaps { get { return worldMaps; } }
        private GamePatches patches;
        private Project project; public Project Project { get { return project; } }
        private Editor editor
        {
            get
            {
                if (Application.OpenForms.Count == 0)
                    return null;
                return (Editor)Application.OpenForms[0];
            }
            set
            {
                if (Application.OpenForms.Count == 0)
                    return;
                Editor main = (Editor)Application.OpenForms[0];
                main = value;
            }
        }
        #endregion
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Program App = new Program();
        }
        // custom exception form
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Model.History += "***EXCEPTION*** " + e.Exception.Message + ")\n";
            new NewExceptionForm(e.Exception).ShowDialog();
        }
        // Constructor
        public Program()
        {
            Model.Program = this;
            ProgramController controls = new ProgramController(this);
            Editor.GuiMain(controls);
        }
        // File Managing
        public bool OpenRomFile()
        {
            string filename;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Select a SMRPG ROM";
            openFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                filename = openFileDialog1.FileName;
                Model.FileName = filename;
                if (Model.ReadRom())
                {
                    settings.LastRomPath = Model.GetPathWithoutFileName();
                    settings.Save();
                    return true;
                }
            }
            else
                filename = "";
            return false;
        }
        public bool OpenRomFile(string filename)
        {
            Model.FileName = filename;
            if (Model.ReadRom())
            {
                settings.LastRomPath = Model.GetPathWithoutFileName();
                settings.Save();
                return true;
            }
            return false;
        }
        public bool SaveRomFile()
        {
            Assemble();
            if (Model.WriteRom())
            {
                Model.CreateNewMD5Checksum();
                return true;
            }
            return false;
        }
        public bool SaveRomFileAs()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Model.FileName = saveFileDialog1.FileName;
                // Assemble all changes
                Assemble();
                if (Model.WriteRom())
                {
                    settings.LastRomPath = Model.GetPathWithoutFileName();
                    settings.Save();
                    Model.CreateNewMD5Checksum();
                    return true;
                }
                return false;
            }
            return true;
        }
        public void Assemble()
        {
            if (allies != null && allies.Visible)
                allies.Assemble();
            if (animations != null && animations.Visible)
                animations.Assemble();
            if (attacks != null && attacks.Visible)
                attacks.Assemble();
            if (battlefields != null && battlefields.Visible)
                battlefields.Assemble();
            if (dialogues != null && dialogues.Visible)
                dialogues.Assemble();
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.Assemble();
            if (formations != null && formations.Visible)
                formations.Assemble();
            if (items != null && items.Visible)
                items.Assemble();
            if (levels != null && levels.Visible)
                levels.Assemble();
            if (monsters != null && monsters.Visible)
                monsters.Assemble();
            if (sprites != null && sprites.Visible)
                sprites.Assemble();
            if (intro != null && intro.Visible)
                intro.Assemble();
            if (worldMaps != null && worldMaps.Visible)
                worldMaps.Assemble();
        }
        public void CloseRomFile()
        {
            Model.ROMHash = null;
        }
        #region Create Editor Windows
        public void ScreencapHotkeys()
        {
            if (allies == null || !allies.Visible) allies = new AlliesEditor();
            if (animations == null || !animations.Visible) animations = new AnimationScripts();
            if (attacks == null || !attacks.Visible) attacks = new AttacksEditor();
            if (audio == null || !audio.Visible) audio = new Audio();
            if (battlefields == null || !battlefields.Visible) battlefields = new Battlefields();
            if (dialogues == null || !dialogues.Visible) dialogues = new Dialogues();
            if (effects == null || !effects.Visible) effects = new Effects();
            if (eventScripts == null || !eventScripts.Visible) eventScripts = new EventScripts();
            if (formations == null || !formations.Visible) formations = new FormationsEditor();
            if (items == null || !items.Visible) items = new ItemsEditor();
            if (levels == null || !levels.Visible) levels = new Levels();
            if (monsters == null || !monsters.Visible) monsters = new Monsters();
            if (intro == null || !intro.Visible) intro = new Intro();
            if (menus == null || !menus.Visible) menus = new MenusEditor();
            if (miniGames == null || !miniGames.Visible) miniGames = new MiniGames();
            if (sprites == null || !sprites.Visible) sprites = new Sprites();
            if (worldMaps == null || !worldMaps.Visible) worldMaps = new WorldMaps();
        }
        public void CreateAlliesWindow()
        {
            if (allies == null || !allies.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                allies = new AlliesEditor();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, allies);
                else
                    allies.Show();
                Cursor.Current = Cursors.Arrow;
            }
            allies.KeyDown += new KeyEventHandler(editor_KeyDown);
            allies.BringToFront();
        }
        public void CreateAnimationsWindow()
        {
            if (animations == null || !animations.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                animations = new AnimationScripts();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, animations);
                else
                    animations.Show();
                Cursor.Current = Cursors.Arrow;
            }
            animations.KeyDown += new KeyEventHandler(editor_KeyDown);
            animations.BringToFront();
        }
        public void CreateAttacksWindow()
        {
            if (attacks == null || !attacks.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                attacks = new AttacksEditor();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, attacks);
                else
                    attacks.Show();
                Cursor.Current = Cursors.Arrow;
            }
            attacks.KeyDown += new KeyEventHandler(editor_KeyDown);
            attacks.BringToFront();
        }
        public void CreateAudioWindow()
        {
            if (audio == null || !audio.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                audio = new Audio();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, audio);
                else
                    audio.Show();
                Cursor.Current = Cursors.Arrow;
            }
            audio.KeyDown += new KeyEventHandler(editor_KeyDown);
            audio.BringToFront();
        }
        public void CreateBattlefieldsWindow()
        {
            if (battlefields == null || !battlefields.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                battlefields = new Battlefields();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, battlefields);
                else
                    battlefields.Show();
                Cursor.Current = Cursors.Arrow;
            }
            battlefields.KeyDown += new KeyEventHandler(editor_KeyDown);
            battlefields.BringToFront();
        }
        public void CreateDialoguesWindow()
        {
            if (dialogues == null || !dialogues.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                dialogues = new Dialogues();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, dialogues);
                else
                    dialogues.Show();
                Cursor.Current = Cursors.Arrow;
            }
            dialogues.KeyDown += new KeyEventHandler(editor_KeyDown);
            dialogues.BringToFront();
        }
        public void CreateEffectsWindow()
        {
            if (effects == null || !effects.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                effects = new Effects();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, effects);
                else
                    effects.Show();
                Cursor.Current = Cursors.Arrow;
            }
            effects.KeyDown += new KeyEventHandler(editor_KeyDown);
            effects.BringToFront();
        }
        public void CreateEventScriptsWindow()
        {
            if (eventScripts == null || !eventScripts.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                eventScripts = new EventScripts();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, eventScripts);
                else
                    eventScripts.Show();
                Cursor.Current = Cursors.Arrow;
            }
            eventScripts.KeyDown += new KeyEventHandler(editor_KeyDown);
            eventScripts.BringToFront();
        }
        public void CreateFormationsWindow()
        {
            if (formations == null || !formations.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                formations = new FormationsEditor();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, formations);
                else
                    formations.Show();
                Cursor.Current = Cursors.Arrow;
            }
            formations.KeyDown += new KeyEventHandler(editor_KeyDown);
            formations.BringToFront();
        }
        public void CreateItemsWindow()
        {
            if (items == null || !items.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                items = new ItemsEditor();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, items);
                else
                    items.Show();
                Cursor.Current = Cursors.Arrow;
            }
            items.KeyDown += new KeyEventHandler(editor_KeyDown);
            items.BringToFront();
        }
        public void CreateLevelsWindow()
        {
            if (levels == null || !levels.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                levels = new Levels();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, levels);
                else
                    levels.Show();
                Cursor.Current = Cursors.Arrow;
            }
            levels.KeyDown += new KeyEventHandler(editor_KeyDown);
            levels.BringToFront();
        }
        public void CreateMonstersWindow()
        {
            if (monsters == null || !monsters.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                monsters = new Monsters();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, monsters);
                else
                    monsters.Show();
                Cursor.Current = Cursors.Arrow;
            }
            monsters.KeyDown += new KeyEventHandler(editor_KeyDown);
            monsters.BringToFront();
        }
        public void CreateIntroWindow()
        {
            if (intro == null || !intro.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                intro = new Intro();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, intro);
                else
                    intro.Show();
                Cursor.Current = Cursors.Arrow;
            }
            intro.KeyDown += new KeyEventHandler(editor_KeyDown);
            intro.BringToFront();
        }
        public void CreateMenusWindow()
        {
            if (menus == null || !menus.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                menus = new MenusEditor();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, menus);
                else
                    menus.Show();
                Cursor.Current = Cursors.Arrow;
            }
            menus.KeyDown += new KeyEventHandler(editor_KeyDown);
            menus.BringToFront();
        }
        public void CreateMiniGamesWindow()
        {
            if (miniGames == null || !miniGames.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                miniGames = new MiniGames();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, miniGames);
                else
                    miniGames.Show();
                Cursor.Current = Cursors.Arrow;
            }
            miniGames.KeyDown += new KeyEventHandler(editor_KeyDown);
            miniGames.BringToFront();
        }
        public void CreateSpritesWindow()
        {
            if (sprites == null || !sprites.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                sprites = new Sprites();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, sprites);
                else
                    sprites.Show();
                Cursor.Current = Cursors.Arrow;
            }
            sprites.KeyDown += new KeyEventHandler(editor_KeyDown);
            sprites.BringToFront();
        }
        public void CreateWorldMapsWindow()
        {
            if (worldMaps == null || !worldMaps.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                worldMaps = new WorldMaps();
                if (dockEditors)
                    Do.AddControl(editor.Panel2, worldMaps);
                else
                    worldMaps.Show();
                Cursor.Current = Cursors.Arrow;
            }
            worldMaps.KeyDown += new KeyEventHandler(editor_KeyDown);
            worldMaps.BringToFront();
        }
        public void CreatePatchesWindow()
        {
            if ((levels != null && levels.Visible) ||
                (eventScripts != null && eventScripts.Visible) ||
                (sprites != null && sprites.Visible))
            {
                DialogResult result = MessageBox.Show(
                    "It is highly recommended that you close and save any editor windows before patching.\n\n" +
                    "Would you like to save and close all current windows?",
                    "LAZY SHELL", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    CloseAll();
                else
                    if (result == DialogResult.Cancel)
                        return;
            }
            if (patches == null || !patches.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                patches = new GamePatches();
                patches.Show();
                patches.StartDownloadingPatches();
                Cursor.Current = Cursors.Arrow;
            }
        }
        public void CreateProjectWindow()
        {
            if (project == null || !project.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                project = new Project();
                project.Show();
                Cursor.Current = Cursors.Arrow;
            }
        }
        #endregion
        // editor managing
        public void Dock()
        {
            if (allies != null && allies.Visible)
                Do.AddControl(editor.Panel2, allies);
            if (animations != null && animations.Visible)
                Do.AddControl(editor.Panel2, animations);
            if (attacks != null && attacks.Visible)
                Do.AddControl(editor.Panel2, attacks);
            if (audio != null && audio.Visible)
                Do.AddControl(editor.Panel2, audio);
            if (battlefields != null && battlefields.Visible)
                Do.AddControl(editor.Panel2, battlefields);
            if (dialogues != null && dialogues.Visible)
                Do.AddControl(editor.Panel2, dialogues);
            if (effects != null && effects.Visible)
                Do.AddControl(editor.Panel2, effects);
            if (eventScripts != null && eventScripts.Visible)
                Do.AddControl(editor.Panel2, eventScripts);
            if (formations != null && formations.Visible)
                Do.AddControl(editor.Panel2, formations);
            if (items != null && items.Visible)
                Do.AddControl(editor.Panel2, items);
            if (levels != null && levels.Visible)
                Do.AddControl(editor.Panel2, levels);
            if (intro != null && intro.Visible)
                Do.AddControl(editor.Panel2, intro);
            if (menus != null && menus.Visible)
                Do.AddControl(editor.Panel2, menus);
            if (miniGames != null && miniGames.Visible)
                Do.AddControl(editor.Panel2, miniGames);
            if (monsters != null && monsters.Visible)
                Do.AddControl(editor.Panel2, monsters);
            if (sprites != null && sprites.Visible)
                Do.AddControl(editor.Panel2, sprites);
            if (worldMaps != null && worldMaps.Visible)
                Do.AddControl(editor.Panel2, worldMaps);
        }
        public void Undock()
        {
            if (allies != null && allies.Visible)
                Do.RemoveControl(allies);
            if (animations != null && animations.Visible)
                Do.RemoveControl(animations);
            if (attacks != null && attacks.Visible)
                Do.RemoveControl(attacks);
            if (audio != null && audio.Visible)
                Do.RemoveControl(audio);
            if (battlefields != null && battlefields.Visible)
                Do.RemoveControl(battlefields);
            if (dialogues != null && dialogues.Visible)
                Do.RemoveControl(dialogues);
            if (effects != null && effects.Visible)
                Do.RemoveControl(effects);
            if (eventScripts != null && eventScripts.Visible)
                Do.RemoveControl(eventScripts);
            if (formations != null && formations.Visible)
                Do.RemoveControl(formations);
            if (items != null && items.Visible)
                Do.RemoveControl(items);
            if (levels != null && levels.Visible)
                Do.RemoveControl(levels);
            if (intro != null && intro.Visible)
                Do.RemoveControl(intro);
            if (menus != null && menus.Visible)
                Do.RemoveControl(menus);
            if (miniGames != null && miniGames.Visible)
                Do.RemoveControl(miniGames);
            if (monsters != null && monsters.Visible)
                Do.RemoveControl(monsters);
            if (sprites != null && sprites.Visible)
                Do.RemoveControl(sprites);
            if (worldMaps != null && worldMaps.Visible)
                Do.RemoveControl(worldMaps);
        }
        public void OpenAll()
        {
            CreateAlliesWindow();
            CreateAnimationsWindow();
            CreateAttacksWindow();
            CreateAudioWindow();
            CreateBattlefieldsWindow();
            CreateDialoguesWindow();
            CreateEffectsWindow();
            CreateEventScriptsWindow();
            CreateFormationsWindow();
            CreateItemsWindow();
            CreateLevelsWindow();
            CreateIntroWindow();
            CreateMenusWindow();
            CreateMiniGamesWindow();
            CreateMonstersWindow();
            CreateSpritesWindow();
            CreateWorldMapsWindow();
        }
        public void MinimizeAll()
        {
            if (allies != null && allies.Visible)
                allies.WindowState = FormWindowState.Minimized;
            if (animations != null && animations.Visible)
                animations.WindowState = FormWindowState.Minimized;
            if (attacks != null && attacks.Visible)
                attacks.WindowState = FormWindowState.Minimized;
            if (audio != null && audio.Visible)
                audio.WindowState = FormWindowState.Minimized;
            if (battlefields != null && battlefields.Visible)
                battlefields.WindowState = FormWindowState.Minimized;
            if (dialogues != null && dialogues.Visible)
                dialogues.WindowState = FormWindowState.Minimized;
            if (effects != null && effects.Visible)
                effects.WindowState = FormWindowState.Minimized;
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.WindowState = FormWindowState.Minimized;
            if (formations != null && formations.Visible)
                formations.WindowState = FormWindowState.Minimized;
            if (items != null && items.Visible)
                items.WindowState = FormWindowState.Minimized;
            if (levels != null && levels.Visible)
                levels.WindowState = FormWindowState.Minimized;
            if (intro != null && intro.Visible)
                intro.WindowState = FormWindowState.Minimized;
            if (menus != null && menus.Visible)
                menus.WindowState = FormWindowState.Minimized;
            if (miniGames != null && miniGames.Visible)
                miniGames.WindowState = FormWindowState.Minimized;
            if (monsters != null && monsters.Visible)
                monsters.WindowState = FormWindowState.Minimized;
            if (sprites != null && sprites.Visible)
                sprites.WindowState = FormWindowState.Minimized;
            if (worldMaps != null && worldMaps.Visible)
                worldMaps.WindowState = FormWindowState.Minimized;
        }
        public void RestoreAll()
        {
            if (allies != null && allies.Visible)
                allies.WindowState = FormWindowState.Normal;
            if (animations != null && animations.Visible)
                animations.WindowState = FormWindowState.Normal;
            if (attacks != null && attacks.Visible)
                attacks.WindowState = FormWindowState.Normal;
            if (audio != null && audio.Visible)
                audio.WindowState = FormWindowState.Normal;
            if (battlefields != null && battlefields.Visible)
                battlefields.WindowState = FormWindowState.Normal;
            if (dialogues != null && dialogues.Visible)
                dialogues.WindowState = FormWindowState.Normal;
            if (effects != null && effects.Visible)
                effects.WindowState = FormWindowState.Normal;
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.WindowState = FormWindowState.Normal;
            if (formations != null && formations.Visible)
                formations.WindowState = FormWindowState.Normal;
            if (items != null && items.Visible)
                items.WindowState = FormWindowState.Normal;
            if (levels != null && levels.Visible)
                levels.WindowState = FormWindowState.Normal;
            if (intro != null && intro.Visible)
                intro.WindowState = FormWindowState.Normal;
            if (menus != null && menus.Visible)
                menus.WindowState = FormWindowState.Normal;
            if (miniGames != null && miniGames.Visible)
                miniGames.WindowState = FormWindowState.Normal;
            if (monsters != null && monsters.Visible)
                monsters.WindowState = FormWindowState.Normal;
            if (sprites != null && sprites.Visible)
                sprites.WindowState = FormWindowState.Normal;
            if (worldMaps != null && worldMaps.Visible)
                worldMaps.WindowState = FormWindowState.Normal;
        }
        public bool CloseAll()
        {
            if (allies != null && allies.Visible)
                allies.Close();
            if (animations != null && animations.Visible)
                animations.Close();
            if (attacks != null && attacks.Visible)
                attacks.Close();
            if (audio != null && audio.Visible)
                audio.Close();
            if (battlefields != null && battlefields.Visible)
                battlefields.Close();
            if (dialogues != null && dialogues.Visible)
                dialogues.Close();
            if (effects != null && effects.Visible)
                effects.Close();
            if (eventScripts != null && eventScripts.Visible)
                eventScripts.Close();
            if (formations != null && formations.Visible)
                formations.Close();
            if (items != null && items.Visible)
                items.Close();
            if (levels != null && levels.Visible)
                levels.Close();
            if (monsters != null && monsters.Visible)
                monsters.Close();
            if (sprites != null && sprites.Visible)
                sprites.Close();
            if (intro != null && intro.Visible)
                intro.Close();
            if (menus != null && menus.Visible)
                menus.Close();
            if (miniGames != null && miniGames.Visible)
                miniGames.Close();
            if (worldMaps != null && worldMaps.Visible)
                worldMaps.Close();
            if ((allies != null && allies.Visible) ||
                (animations != null && animations.Visible) ||
                (attacks != null && attacks.Visible) ||
                (battlefields != null && battlefields.Visible) ||
                (dialogues != null && dialogues.Visible) ||
                (effects != null && effects.Visible) ||
                (eventScripts != null && eventScripts.Visible) ||
                (formations != null && formations.Visible) ||
                (items != null && items.Visible) ||
                (levels != null && levels.Visible) ||
                (monsters != null && monsters.Visible) ||
                (sprites != null && sprites.Visible) ||
                (intro != null && intro.Visible) ||
                (menus != null && menus.Visible) ||
                (miniGames != null && miniGames.Visible) ||
                (worldMaps != null && worldMaps.Visible))
                return true;
            return false;
        }
        public void LoadAll()
        {
            Model.LoadAll();
        }
        public void ClearAll()
        {
            Model.ClearModel();
        }
        private void editor_KeyDown(object sender, KeyEventArgs e)
        {
            Form editor = (Form)sender;
            if (e.KeyData == Keys.F3)
                Do.CaptureScreens(editor);
        }
    }
}