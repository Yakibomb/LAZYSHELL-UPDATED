using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public class ProgramController
    {
        private Program App;
        public bool DockEditors { get { return App.DockEditors; } set { App.DockEditors = value; } }
        // Constructor
        public ProgramController(Program app)
        {
            this.App = app;
        }
        // assemblers
        public void Assemble()
        {
            App.Assemble();
        }
        public bool AssembleAndCloseWindows()
        {
            return App.CloseAll();
        }
        // functions
        #region File Managing
        public bool OpenRomFile()
        {
            if (App.OpenRomFile())
            {
                Model.ClearModel();
                State.Instance.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                State.Instance2.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                if (!VerifyRom())
                    return false;
                return true;
            }
            else
                return false;

        }
        public bool OpenRomFile(string filename)
        {
            if (App.OpenRomFile(filename))
            {
                Model.ClearModel();
                State.Instance.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                State.Instance2.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
                if (!VerifyRom())
                    return false;
                return true;
            }
            else
                return false;
        }
        public bool SaveRomFile()
        {
            return App.SaveRomFile();
        }
        public bool SaveRomFileAs()
        {
            return App.SaveRomFileAs();
        }
        public void CloseRomFile()
        {
            Model.ClearModel();
            State.Instance.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
            State.Instance2.PrivateKey = null; // Clear the PrivateKey whenever we load a new rom
            App.CloseRomFile();
        }
        public bool VerifyRom()
        {
            return Model.VerifyRom();
        }
        public bool HeaderPresent()
        {
            return Model.HeaderPresent();
        }
        public string GetFileNameWithoutPath()
        {
            return Model.GetFileNameWithoutPath();
        }
        public string GetFileNameWithoutPathOrExtension()
        {
            return Model.GetFileNameWithoutPathOrExtension();
        }
        public string GetRomName()
        {
            return Model.GetRomName();
        }
        public string GetPathWithoutFileName()
        {
            return Model.GetPathWithoutFileName();
        }
        public string RomChecksum()
        {
            return Model.RomChecksum();
        }
        public string GameCode()
        {
            return Model.GameCode();
        }
        public long GetFileSize()
        {
            return Model.GetFileSize();
        }
        #endregion
        #region MD5 hash methods
        public void CreateNewMd5Checksum()
        {
            Model.CreateNewMD5Checksum();
        }
        public bool VerifyMD5Checksum()
        {
            return Model.VerifyMD5Checksum();
        }
        #endregion
        #region Author Stamp
        public bool Locked()
        {
            return Model.Locked;
        }
        public bool Publish()
        {
            // Ask to save rom
            if (MessageBox.Show("Publishing a ROM requires saving. Save and publish this rom now?",
                "LAZYSHELL++", MessageBoxButtons.YesNo) == DialogResult.Yes)
                return App.SaveRomFileAs(); // Save
            return false;
        }
        #endregion
        #region Create Editor Windows
        public void Allies()
        {
            App.CreateAlliesWindow();
        }
        public void Animations()
        {
            App.CreateAnimationsWindow();
        }
        public void Attacks()
        {
            App.CreateAttacksWindow();
        }
        public void Audio()
        {
            App.CreateAudioWindow();
        }
        public void Battlefields()
        {
            App.CreateBattlefieldsWindow();
        }
        public void Dialogues()
        {
            App.CreateDialoguesWindow();
        }
        public void Effects()
        {
            App.CreateEffectsWindow();
        }
        public void Formations()
        {
            App.CreateFormationsWindow();
        }
        public void Items()
        {
            App.CreateItemsWindow();
        }
        public void Levels()
        {
            App.CreateLevelsWindow();
        }
        public void MainTitle()
        {
            App.CreateIntroWindow();
        }
        public void Menus()
        {
            App.CreateMenusWindow();
        }
        public void MiniGames()
        {
            App.CreateMiniGamesWindow();
        }
        public void Monsters()
        {
            App.CreateMonstersWindow();
        }
        public void Scripts()
        {
            App.CreateEventScriptsWindow();
        }
        public void Sprites()
        {
            App.CreateSpritesWindow();
        }
        public void WorldMaps()
        {
            App.CreateWorldMapsWindow();
        }
        public void Patches()
        {
            App.CreatePatchesWindow();
        }
        public void Project()
        {
            App.CreateProjectWindow();
        }
        public void Dock()
        {
            App.Dock();
        }
        public void Undock()
        {
            App.Undock();
        }
        public void OpenAll()
        {
            App.OpenAll();
        }
        public void MinimizeAll()
        {
            App.MinimizeAll();
        }
        public void RestoreAll()
        {
            App.RestoreAll();
        }
        public void CloseAll()
        {
            App.CloseAll();
        }
        public void LoadAll()
        {
            App.LoadAll();
        }
        public void ClearAll()
        {
            App.ClearAll();
        }
        #endregion
    }
}
