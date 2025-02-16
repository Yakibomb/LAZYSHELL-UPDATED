using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Restore : NewForm
    {
        private Settings settings = Settings.Default;
        private byte[] romDst { get { return Model.ROM; } set { Model.ROM = value; } }
        private byte[] romSrc = null;
        // constructor
        public Restore()
        {
            InitializeComponent();
        }
        // event handlers
        private void browseFreshRom_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = settings.LastRomPath;
            openFileDialog1.Title = "Select a SMRPG ROM";
            openFileDialog1.Filter = "SMC files (*.SMC)|*.SMC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
            Retry:
                try
                {
                    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
                    long numBytes = fInfo.Length;
                    FileStream fStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);
                    byte[] temp = br.ReadBytes((int)numBytes);
                    br.Close();
                    fStream.Close();
                    // remove header if it has one
                    if ((temp.Length & 0x200) == 0x200)
                        temp = Bits.GetBytes(temp, 0x200);
                    // Check if valid rom
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    if (encoding.GetString(Bits.GetBytes(temp, 0x7FB2, 4)) != "ARWE")
                    {
                        MessageBox.Show("The game code for this ROM is invalid.", "LAZYSHELL++");
                        return;
                    }
                    romSrc = temp;
                    freshRomTextBox.Text = openFileDialog1.FileName;
                    buttonOK.Enabled = true;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Lazy Shell was unable to load the rom.\n\n" + ex.Message,
                        "LAZYSHELL++", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Cancel)
                        goto Retry;
                }
            }
        }
        private void elements_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Allies
            if (elements.Nodes["Allies"].Nodes["LevelUps"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A134D, romDst, 0x3A134D, 0x30); // character names
                Buffer.BlockCopy(romSrc, 0x3A1AFF, romDst, 0x3A1AFF, 0x3A0); // level-ups
                Buffer.BlockCopy(romSrc, 0x3A42F5, romDst, 0x3A42F5, 0x91); // learned spells
            }
            if (elements.Nodes["Allies"].Nodes["StartingStats"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A002C, romDst, 0x3A002C, 0x120); // all
            }
            if (elements.Nodes["Allies"].Nodes["Timings"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A438A, romDst, 0x3A438A, 0xA4); // weapons only
                Buffer.BlockCopy(romSrc, 0x02C9B3, romDst, 0x02C9B3, 0x14); // defense
            }
            // Animations
            if (elements.Nodes["Animations"].Checked)   // animation scripts
            {
                Buffer.BlockCopy(romSrc, 0x350000, romDst, 0x350000, 0x10000);
                Buffer.BlockCopy(romSrc, 0x3A6000, romDst, 0x3A6000, 0xA000); // battle events
            }
            // Attacks
            if (elements.Nodes["Attacks"].Nodes["Attacks"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x391226, romDst, 0x391226, 0x204);  // attacks
                Buffer.BlockCopy(romSrc, 0x3959F4, romDst, 0x3959F4, 0xB60);  // attack names
            }
            if (elements.Nodes["Attacks"].Nodes["Spells"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A137F, romDst, 0x3A137F, 0xC00);  // spell names
                Buffer.BlockCopy(romSrc, 0x3A20F1, romDst, 0x3A20F1, 0x600);  // spells
                Buffer.BlockCopy(romSrc, 0x3A2B80, romDst, 0x3A2B80, 0x3A0);  // spell desc
            }
            // Audio
            if (elements.Nodes["Audio"].Nodes["Samples"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x042333, romDst, 0x042333, 116 * 3);    // audio sample pointers
                Buffer.BlockCopy(romSrc, 0x060939, romDst, 0x060939, 0x094000 - 0x060939);    // audio samples
                Buffer.BlockCopy(romSrc, 0x146000, romDst, 0x146000, 0x148000 - 0x146000);    // audio samples
                Buffer.BlockCopy(romSrc, 0x1C8000, romDst, 0x1C8000, 0x1CEA00 - 0x1C8000);    // audio samples
            }
            if (elements.Nodes["Audio"].Nodes["SPCTracks"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x042748, romDst, 0x042748, 73 * 3);    // spc track pointers
                Buffer.BlockCopy(romSrc, 0x045526, romDst, 0x045526, 0x1B413);    // spc track data
            }
            if (elements.Nodes["Audio"].Nodes["EventSoundFX"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x042826, romDst, 0x042826, 0x1600);    // event sound fx
            }
            if (elements.Nodes["Audio"].Nodes["BattleSoundFX"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x043E26, romDst, 0x043E26, 0x1600);    // battle sound fx
            }
            // Battlefields
            if (elements.Nodes["Battlefields"].Nodes["Battlefields"].Checked)   // battlefields
            {
                Buffer.BlockCopy(romSrc, 0x39B644, romDst, 0x39B644, 0x200);
                Buffer.BlockCopy(romSrc, 0x34D000, romDst, 0x34D000, 0x3000); // palettes
            }
            if (elements.Nodes["Battlefields"].Nodes["Tilesets"].Checked)   // battlefield tilesets
            {
                Buffer.BlockCopy(romSrc, 0x150000, romDst, 0x150000, 0x10000);
            }
            if (elements.Nodes["Battlefields"].Nodes["Graphics"].Checked)   // battlefield graphics
            {
                Buffer.BlockCopy(romSrc, 0x110000, romDst, 0x110000, 0x36000);
            }
            // Dialogues
            if (elements.Nodes["Dialogues"].Nodes["Dialogues"].Checked)   // dialogues
            {
                Buffer.BlockCopy(romSrc, 0x006935, romDst, 0x006935, 0x8);    // compression table pointers
                Buffer.BlockCopy(romSrc, 0x220000, romDst, 0x220000, 0x29140);    //dialogues
                Buffer.BlockCopy(romSrc, 0x37E000, romDst, 0x37E000, 0x2000); // pointers
            }
            if (elements.Nodes["Dialogues"].Nodes["BattleDialogues"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x396554, romDst, 0x396554, 0x2D7D); // battle dialogues
            }
            if (elements.Nodes["Dialogues"].Nodes["FontsBackgrounds"].Checked)   // fonts, backgrounds
            {
                Buffer.BlockCopy(romSrc, 0x249140, romDst, 0x249140, 0xEC0);
                Buffer.BlockCopy(romSrc, 0x37C000, romDst, 0x37C000, 0x2000);
                Buffer.BlockCopy(romSrc, 0x3DF000, romDst, 0x3DF000, 0x1000);
            }
            // Effects
            if (elements.Nodes["Effects"].Checked)   // spell effects
            {
                Buffer.BlockCopy(romSrc, 0x251000, romDst, 0x251000, 0x200);  // effects
                Buffer.BlockCopy(romSrc, 0x252C00, romDst, 0x252C00, 0xC0);   // main data pointers
                Buffer.BlockCopy(romSrc, 0x330000, romDst, 0x330000, 0x1D000);    // main data
            }
            // Event Scripts
            if (elements.Nodes["EventScripts"].Nodes["EventScripts"].Checked)   // event scripts
            {
                Buffer.BlockCopy(romSrc, 0x1E0000, romDst, 0x1E0000, 0x2E000);
            }
            if (elements.Nodes["EventScripts"].Nodes["ActionScripts"].Checked)   // action scripts
            {
                Buffer.BlockCopy(romSrc, 0x210000, romDst, 0x210000, 0xC000);
            }
            // Formations
            if (elements.Nodes["Formations"].Nodes["Formations"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x392AAA, romDst, 0x392AAA, 0x600);  // formation stats
                Buffer.BlockCopy(romSrc, 0x39C000, romDst, 0x39C000, 0x4000);  // formations
            }
            if (elements.Nodes["Formations"].Nodes["Packs"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x39222A, romDst, 0x39222A, 0x400);  // packs
            }
            // Intro
            if (elements.Nodes["Intro"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3F216F, romDst, 0x3F216F, 0x3FA000 - 0x3F216F);    // compressed data
                Buffer.BlockCopy(romSrc, 0x3F0088, romDst, 0x3F0088, 0x3F0228 - 0x3F0088);    // palettes
            }
            // Items
            if (elements.Nodes["Items"].Nodes["Items"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A014D, romDst, 0x3A014D, 0x1200); // items
                Buffer.BlockCopy(romSrc, 0x3A2F20, romDst, 0x3A2F20, 0x13D2); // item desc, prices
                Buffer.BlockCopy(romSrc, 0x3A46EF, romDst, 0x3A46EF, 0xF00); // item names
            }
            if (elements.Nodes["Items"].Nodes["Shops"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3A44DF, romDst, 0x3A44DF, 0x210); // item names
            }
            // Levels
            if (elements.Nodes["Levels"].Nodes["Layers"].Checked)   // layers
            {
                Buffer.BlockCopy(romSrc, 0x1D0000, romDst, 0x1D0000, 0X2440);
            }
            if (elements.Nodes["Levels"].Nodes["Maps"].Checked)   // maps
            {
                Buffer.BlockCopy(romSrc, 0x1D2440, romDst, 0x1D2440, 0x924);
                Buffer.BlockCopy(romSrc, 0x24A000, romDst, 0x24A000, 0x6000);   // palettes
            }
            if (elements.Nodes["Levels"].Nodes["NPCs"].Checked)   // npcs
            {
                Buffer.BlockCopy(romSrc, 0x148000, romDst, 0x148000, 0x8000);
                Buffer.BlockCopy(romSrc, 0x1DB000, romDst, 0x1DB000, 0x190);  // npc packets
                Buffer.BlockCopy(romSrc, 0x1DB800, romDst, 0x1DB800, 0xE00);  // npc properties
                Buffer.BlockCopy(romSrc, 0x1DDE00, romDst, 0x1DDE00, 0x200);  // partitions
            }
            if (elements.Nodes["Levels"].Nodes["Exits"].Checked)   // exits
            {
                Buffer.BlockCopy(romSrc, 0x1D2D64, romDst, 0x1D2D64, 0x1BA1);
            }
            if (elements.Nodes["Levels"].Nodes["Events"].Checked)   // events
            {
                Buffer.BlockCopy(romSrc, 0x20E000, romDst, 0x20E000, 0x2000);
            }
            if (elements.Nodes["Levels"].Nodes["Overlaps"].Checked)   // overlaps
            {
                Buffer.BlockCopy(romSrc, 0x1D4905, romDst, 0x1D4905, 0x15B8);
            }
            if (elements.Nodes["Levels"].Nodes["Tilemaps"].Checked)   // tilemaps
            {
                Buffer.BlockCopy(romSrc, 0x160000, romDst, 0x160000, 0x48000);
            }
            if (elements.Nodes["Levels"].Nodes["Tilesets"].Checked)   // tilesets
            {
                Buffer.BlockCopy(romSrc, 0x3B0000, romDst, 0x3B0000, 0x2C000);
            }
            if (elements.Nodes["Levels"].Nodes["TileMods"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x1D5EBD, romDst, 0x1D5EBD, 0x2EF3);
            }
            if (elements.Nodes["Levels"].Nodes["SolidMods"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x1D8DB0, romDst, 0x1D8DB0, 0xCFF);
            }
            if (elements.Nodes["Levels"].Nodes["Graphics"].Checked)   // graphics
            {
                Buffer.BlockCopy(romSrc, 0x0A0000, romDst, 0x0A0000, 0x60000);
            }
            if (elements.Nodes["Levels"].Nodes["SolidityMaps"].Checked)   // solidity maps
            {
                Buffer.BlockCopy(romSrc, 0x1B0000, romDst, 0x1B0000, 0x18000);
            }
            // Menus
            if (elements.Nodes["Menus"].Nodes["Text"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3EEF00, romDst, 0x3EEF00, 0x3EF700 - 0x3EEF00);    // menu texts
            }
            if (elements.Nodes["Menus"].Nodes["Graphics"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3E0002, romDst, 0x3E0002, 0x3E0008 - 0x3E0002);    // pointers of compressed data GFX (MenuBGGraphics, MenuFrameGraphics, MenuCursorGraphics)
                Buffer.BlockCopy(romSrc, 0x3E000C, romDst, 0x3E000C, 0x3E000E - 0x3E000C);    // pointers of compressed data PAL (MenuPalettes)
                //ShopBGPalette: 0x3E9A05
                //MenuBGPalette: 0x3E9A28
                //GameSelectGraphics 0x3E9A4A
                //GameSelectPalettes 0x3EB510 (end 0x3EB624)
                //Model.Compress(Model.GameSelectGraphics, 0x3E9A49, 0x2000, 0x3EB2CD - 0x3E9A49, "Game select graphics");
                //Model.Compress(Model.GameSelectTileset, 0x3EB2CD, 0x800, 0x3EB50F - 0x3EB2CD, "Game select tileset");
                //Model.Compress(Model.GameSelectPalettes, 0x3EB50F, 0x200, 0x3EB624 - 0x3EB50F, "Game select palettes");
                //Model.Compress(Model.GameSelectSpeakers, 0x3EB624, 0x600, 0x3EB94A - 0x3EB624, "Game select speakers");
                Buffer.BlockCopy(romSrc, 0x3E9A05, romDst, 0x3E9A05, 0x3EB94A - 0x3E9A05);    // compressed data (everything above, GameSelectSpeakers, GameSelectBGPalette)
                Buffer.BlockCopy(romSrc, 0x3E0E68, romDst, 0x3E0E68, 0x3E2606 - 0x3E0E68);    // compressed data (MenuBGGraphics, other)
                //Buffer.BlockCopy(romSrc, 0x3E99E0, romDst, 0x3E99E0, 0x3EB94A - 0x3E99E0);    // compressed data (GameSelectSpeakers, GameSelectBGPalette)
            }
            if (elements.Nodes["Menus"].Nodes["Tilesets"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x3E0008, romDst, 0x3E0008, 0x02);                   // pointers of compressed data (MenuBGTileset)
                Buffer.BlockCopy(romSrc, 0x3E2869, romDst, 0x3E2869, 0x3E2C80 - 0x3E2869);    // compressed data (MenuBGTileset)

                Buffer.BlockCopy(romSrc, 0x3E0034, romDst, 0x3E0034, 0x02);                   // pointers of compressed data (GameSelectTileset)
                Buffer.BlockCopy(romSrc, 0x3EB2CE, romDst, 0x3EB2CE, 0x3EB50F - 0x3EB2CE);    // compressed data (GameSelectTileset)

                Buffer.BlockCopy(romSrc, 0x3E000A, romDst, 0x3E000A, 0x02);                   // pointers of compressed data (OverworldStarPiecesMenuTileset)
                Buffer.BlockCopy(romSrc, 0x3E2C81, romDst, 0x3E2C81, 0x3E2D53 - 0x3E2C81);    // compressed data (OverworldStarPiecesMenuTileset)
            }
            if (elements.Nodes["Menus"].Nodes["Misc"].Checked)
            {
                romDst[0x03462D] = romSrc[0x03462D]; // music track
                Buffer.BlockCopy(romSrc, 0x0340AA, romDst, 0x0340AA, 0x035023 - 0x0340AA);    // game select sprite sequences
                Buffer.BlockCopy(romSrc, 0x03170D, romDst, 0x03170D, 0x09);     // Overworld Menu List
                Buffer.BlockCopy(romSrc, 0x03327A, romDst, 0x03327A, 0x03328F - 0x03327A);    // X coord of some texts
            }
            // Mini-games
            if (elements.Nodes["MiniGames"].Nodes["MineCarts"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x388000, romDst, 0x388000, 0x8000);
            }
            // Monsters
            if (elements.Nodes["Monsters"].Nodes["Monsters"].Checked)
            {
                Buffer.BlockCopy(romSrc, 0x350202, romDst, 0x350202, 0x200);  // monster death animation
                Buffer.BlockCopy(romSrc, 0x390026, romDst, 0x390026, 0x1120); // monsters
                Buffer.BlockCopy(romSrc, 0x39142A, romDst, 0x39142A, 0xE00);  // monster rewards
                Buffer.BlockCopy(romSrc, 0x3992D1, romDst, 0x3992D1, 0x2373); // monster names, psycho
                Buffer.BlockCopy(romSrc, 0x39B944, romDst, 0x39B944, 0x100);  // monster target cursor
                Buffer.BlockCopy(romSrc, 0x39BB44, romDst, 0x39BB44, 0x100);  // monster flower bonus
            }
            if (elements.Nodes["Monsters"].Nodes["BattleScripts"].Checked)   // battle scripts
            {
                Buffer.BlockCopy(romSrc, 0x3930AA, romDst, 0x3930AA, 0x294A);
            }
            // Sprites
            if (elements.Nodes["Sprites"].Checked)   // sprites
            {
                Buffer.BlockCopy(romSrc, 0x250000, romDst, 0x250000, 0x1000);
                Buffer.BlockCopy(romSrc, 0x251800, romDst, 0x251800, 0x1400);
                Buffer.BlockCopy(romSrc, 0x253000, romDst, 0x253000, 0xDD000);    // everything else
                Buffer.BlockCopy(romSrc, 0x360000, romDst, 0x360000, 0x10000);    // everything else
            }
            // World Maps
            if (elements.Nodes["WorldMaps"].Nodes["WorldMaps"].Checked)   // world maps
            {
                Buffer.BlockCopy(romSrc, 0x3EF800, romDst, 0x3EF800, 0x30); // world map data
            }
            if (elements.Nodes["WorldMaps"].Nodes["Locations"].Checked)   // locations
            {
                Buffer.BlockCopy(romSrc, 0x3EF830, romDst, 0x3EF830, 0x380);  // location data
                Buffer.BlockCopy(romSrc, 0x3EFD00, romDst, 0x3EFD00, 0x220);  // location names
            }
            if (elements.Nodes["WorldMaps"].Nodes["Tilesets"].Checked)   // world map tilesets
            {
                Buffer.BlockCopy(romSrc, 0x3E0000, romDst, 0x3E0000, 0x0E68); // world map logo, banners
                Buffer.BlockCopy(romSrc, 0x3E2E82, romDst, 0x3E2E82, 0x69B6); // world map graphics, tilesets
                Buffer.BlockCopy(romSrc, 0x3E8578, romDst, 0x3E8578, 0xB06); // world map background (clouds/ocean) graphics
            }
            if (elements.Nodes["WorldMaps"].Nodes["Sprites"].Checked)   // world map sprites
            {
                Buffer.BlockCopy(romSrc, 0x3E90A7, romDst, 0x3E90A7, 0x1FB); // world map sprites (Mario, location point) graphics
            }
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in elements.Nodes)
                node.Checked = true;
        }
        private void deselectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in elements.Nodes)
                node.Checked = false;
        }
    }
}
