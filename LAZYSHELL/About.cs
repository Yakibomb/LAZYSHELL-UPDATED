using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class About : NewForm
    {
        private Editor form1;
        private List<string[]> displayVersion = new List<string[]>();
        private int ver = 0;
        // constructor
        public About(Editor form1)
        {
            this.form1 = form1;
            InitializeComponent();
            #region UPDATED_EDITION
            displayVersion.Add(v1_0_0);
            displayVersion.Add(v1_1_0);
            displayVersion.Add(v1_2_0);
            displayVersion.Add(v1_2_1);
            displayVersion.Add(v1_2_2);
            displayVersion.Add(v1_2_3);
            displayVersion.Add(v1_2_4);
            displayVersion.Add(v1_2_5);
            displayVersion.Add(v1_2_6);
            displayVersion.Add(v1_2_7);
            displayVersion.Add(v1_2_8);
            displayVersion.Add(v1_3_0);
            displayVersion.Add(v1_3_1);
            displayVersion.Add(v1_3_2);
            #endregion

            displayVersion.Add(v2_0_0);
            displayVersion.Add(v2_0_1);
            displayVersion.Add(v2_1_0);
            displayVersion.Add(v2_1_1);
            displayVersion.Add(v2_1_2);
            displayVersion.Add(v2_1_3);
            displayVersion.Add(v2_1_4);

            ver = displayVersion.Count - 1;
            RefreshVersion();
        }
        private void RefreshVersion()
        {
            if (ver == 0)
                imageStuffButtonLeft.Enabled = false;
            else
                imageStuffButtonLeft.Enabled = true;

            if (ver == displayVersion.Count - 1)
                imageStuffButtonRight.Enabled = false;
            else
                imageStuffButtonRight.Enabled = true;

            if (ver > displayVersion.Count - 1)
                return;
            if (ver < 0)
                return;
            imageStuffLabel.Text = " " + displayVersion[ver][0].ToString();
            changeslog1.Text = displayVersion[ver][1].ToString();
        }

        // event handlers
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void imageStuffButtonLeft_Click(object sender, EventArgs e)
        {
            ver--;
            RefreshVersion();
        }

        private void imageStuffButtonRight_Click(object sender, EventArgs e)
        {
            ver++;
            RefreshVersion();
        }

        private string[] v2_x_x = new string[2] { "v2.x.x", "" };

        private string[] v2_1_4 = new string[2] { "v2.1.4", "TOOLS\r\n - New Font Table: Updated move left and move right icons\r\n - Graphic Editor: Updated panelGraphic resize icons\r\n - Graphic Editor: Toolbar on top-right now adjusts with window resizing\r\n\r\nLEVELS\r\n - Updated spaceAnalyzer icon to Statistics icon\r\n - Fixed coordinates / scrollbars in tilemap editor not appearing\r\n\r\nABOUT\r\n - Fixed VS Code breaking my changelogs\r\n\r\nPATCHES\r\n - Trying to open patches again after it's already open won't warn you again\r\n - Stop/Reload patch server button should work more consistently\r\n - Fixed an issue where you could type in the boxes for category, authors and versions\r\n - Made version box invisible during patches loading in\r\n\r\nEVENTS\r\n - Added Manual Jump Offset Updater. This allows for one or a range of scripts to manually adjust each \"jump to\" offset. This should work as a bandaid fix until the reason for why jumps are off by a few bytes sometimes." };
        private string[] v2_1_3 = new string[2] { "v2.1.3", "MAIN TITLE\r\n - Updated HelpTips (hotkey \"F1\")\r\n - Made Title Preview graphics layering more accurate to in-game\r\n - Added feature to export image of title preview to \".png\" format\r\n - Fixed an issue where updating sprite graphics wouldn't update the title preview\r\n - Added configurable banner graphic for title preview\r\n\r\nALLIES\r\n - Coorindates for ABXY's horizonal coordinate now invisible instead of greyed out\r\n - Clear New Game now just asks instead of opening the \"Clear Range\" window\r\n - Fixed an issue with custom ABXY would give an error\r\n\r\nTOOLS\r\n - Previewer: Mario is checked in default settings\r\n\r\nEVENTS SCRIPTS\r\n - Added undocumented commands \"Mario glowing stops\" (0xFD 0xFA)\r\n - Under Action Scripts, for Object memory, fixed \"Remove object @ $70A8 in current level\" not being selectable\r\n - Fixed \"Circle mask, shrink to object {xx] (non-static)\" parameters not setting\r\n - Corrected \"Memory $7000 = quantity of item @ memory $70A7\" (addresses Memory 70A7, 7000 were reversed)\r\n\r\nANIMATIONS\r\n - Fixed an issue with the toolbar at the bottom not docking fully, causing graphical glitches\r\n" };
        private string[] v2_1_2 = new string[2] { "v2.1.2", "MAIN TITLE\r\n - Title preview should be more accurate\r\n - Fixed an issue with the credits at the bottom of the screen would have a non-transparent background\r\n" };
        private string[] v2_1_1 = new string[2] { "v2.1.1", "DIALOGUES\r\n - Fixed bug where fonts and palettes wouldn't save\r\n - Fixed graphic/palette editors not closing when closing dialogues\r\n\r\nMENUS\r\n - Fixed frame graphics using wrong palette\r\n - Fixed frame graphics not resetting with reset button" };
        private string[] v2_1_0 = new string[2] { "v2.1.0", "ALLIES\r\n - Gave it a new coat of paint.\r\n - Added way to set location of target cursor arrow and ABXY\r\n - Added level-up calculator to total the sum of level-up increments and show which spells are learned when.\r\n - Fixed Clear clearing exp needed to level up when clearing a character\r\n - Updated HelpTips\r\n\r\nMAIN TITLE\r\n - Added title preview\r\n - Can edit the locations for Exor, background, main title, and credits for both first boot-up title and after Exor title\r\n - Can force the game to use the first boot title or the alternate Exor title\r\n - Fixed L1/L2 and L3 graphics overwriting each other, made them separate graphic buttons\r\n\r\nATTACKS\r\n - Fixed Damage Calculator to only have one window open at a time\r\n\r\nTOOLS\r\n - Previewer: Added way to switch out Mario\r\n - Graphics Editor: Fixed bug where backup GFX would overwrite current GFX if the window open, then closed on another sprite index\r\n - Restore: Included .SFC/.BAK file types to open\r\n - Restore: Reworked Menus (again). Reassembles compressed data just like Menus editor does, instead of just copying chunks of data over.\r\n - Restore: Fixed bug where opening a subeditor before would not update the subeditor with the new restored changes\r\n - Hex Editor: Fixed crash when opening, typing in the searchBox, closing, then reopening\r\n - Tile Editor: Updated toolbar\r\n - Graphics Editor: Updated toolbar\r\n - Palette Editor: Updated toolbar\r\n\r\nMAIN\r\n - Included file type to open \"SMRPG ROM Backup (*.BAK)\"\r\n\r\nMENUS\r\n - Fixed reload/closing ROM calling function Close(), causing the sub-editor not to close\r\n - Fixed Star Pieces assets from not resetting when closing the editor\r\n - Fixed Game Select Background palette carrying over from ROMs\r\n - Added reset button\r\n - Added tile editor\r\n - Fixed banner palette being glitched in Preview\r\n - Probably fixed more stuff here, but didn't write it down\r\n\r\nMONSTERS\r\n - Fixed stats from updating by switching the monster's index after typing in a new stat value\r\n\r\nPROJECT NOTES\r\n - Removed ROM name from notes\r\n - No longer asks for ROM name. Instead, it checks to see if the note's file name matches the ROM's file name, and asks if it's ok to load.\r\n - Fixed Allies not working\r\n - Added hotkey Ctrl+S to save\r\n - Fixed project notes file always loading without option being checked in Settings.\r\n - The above change also fixes a few other bugs when updating labels.\r\n\r\nITEMS\r\n - Shops: Fixed last index not displaying \"/// NOTHING ///\"\r\n" };
        private string[] v2_0_1 = new string[2] { "v2.0.1", "ATTACKS\r\n - Fixed Berserk status in Monster Attacks not applying correctly\r\n" };
        private string[] v2_0_0 = new string[2] { "v2.0.0", "MAIN\r\n - Updated icons across all editors (using LS4.0 icons+outsourced+custom)\r\n --LS 4.0 icons by giangurgolo\r\n --New icons outsourced from led24.de/iconset/\r\n --New icons outsourced from fatcow.com/free-icons\r\n --New icons outsourced from p.yusukekamiyamane.com/\r\n --New, custom icons by Yaki\r\n --- (All LS 3.19.0 icons still in solution on Github)\r\n - Added jumbo icons layout option\r\n -- As a bonus, can hold \"alt\" to drag+drop editor icons on main editor window\r\n - Fixed \"Close ROM\" not closing the HEX editor\r\n - Fixed \"Project Database\" not asking to save if closing full editor\r\n - Ally name changes are reflected in lists\r\n - Updated toolbar layout across all editors\r\n -- Help, Base Converter, and window changing icons are on the right of the toolbar\r\n - Included \".SFC\" ROM file types to be loaded/saved\r\n - Error message window Abort Application now always closes editor (credit jpmac26)\r\n - Updated .NET Framework from 2.0 to 4.8\r\n - Updated readme.xml\r\n\r\nTOOLS\r\n - Palette Editor: Fixed throw for HTML, either not being hex, or being greater than $F8F8F8\r\n - Palette Editor: Fixed window resolution being slightly smaller than intended\r\n - Graphics Editor: Fixed not being able to reset or undo because palette editor was updated\r\n - ZoomBox: Fixed being able to close it using Alt-F4 in Sprites.\r\n - Hex Editor: Fixed \"Jump To Offset\" jumping to offset prior to the one searched for\r\n - Hex Editor: Added way to search backwards or forwards from offset\r\n - Hex Editor: Added button to highlight recent changes in ROM\r\n - Hex Editor: Added buttons to jump (forwards or backwards) to recent changes in ROM\r\n - Restore: Menus now includes imports for just Text, Graphics, Tilesets, and Misc.\r\n\r\nPREVIEWER\r\n - Rerouted all PreviewROMs to the emulator path. (ZSNES typically likes to have all saves in a certain folder as it, a setting it defaults to)\r\n - Fixed editor generating a backup for PreviewROMs\r\n - Added previewer for Sprites and Effects editors\r\n - Added warning if no emulator is selected\r\n\r\nPROJECT DATABASE\r\n - Fixed error where file is \"being used by another process\" if notes were loaded then saved\r\n - Fixed editor buttons not becoming disabled if there were no notes loaded\r\n - Added warning for \"Close Project\" button\r\n - Fixed bug with \"Close Project\" button where the file directory would not reset, causing errors\r\n - Added \"Reload Project Database\" to refresh notes for any reason\r\n - Added Auto-Update feature\r\n\r\nPATCHES\r\n - Now works! Explore ROM patches in a sleek layout, and patch your ROM on-the-fly if you desire. (Just make sure to backup your ROM before patching!)\r\n - Host server is Yaki's github by default. Can use one or several sites to connect to.\r\n => Instructions on how to host your own patch server is in the default patch server link on github\r\n - Added HelpTips to Patches\r\n\r\nLEVELS\r\n - Added \"Solidity Flat Mode\" (Hotkey \"R\"). Flat Mode reduces the height of all tiles to zero so you can actually see what you're doing.\r\n - Updated color codes for solidity tiles\r\n - Added new color codes on solidity tiles for Stairs, Doors, Conveyerbelts\r\n - Corrected icons for Solid Mods and Tile Mods\r\n - Made Priority1 yellow on Solidity Tiles transparent at while opacity set to 100%\r\n - Fixed bug where editor would not ask to save if changing area music only then closing\r\n - Added Palette offset under \"?\" level info icon\r\n - Renamed a couple levels to more accurately describe them\r\n\r\nATTACKS\r\n - Fixed \"Damage Modifiers\" box not disappearing for non-ally spells\r\n - Changed \"Clear element\" option to reset to NONE (instead of just zero)\r\n - Added checkbox for unused Berserk status (doesn't work unless ASM enables it)\r\n\r\nITEMS\r\n - Added feature to change how a list sorts itself (Alphabetically, w/ or w/out icons, and sorts DUMMY to the bottom)\r\n - Changed \"Clear element\" option to reset to NONE (instead of just zero)\r\n - Added checkbox for unused Berserk status (doesn't work unless ASM enables it)\r\n - Renamed \"Element Weaknesses\" to \"Element Halving\"\r\n - Ally names in Checkboxes update to the ally names in the ROM\r\n - Updated HelpTips to reflect new changes\r\n\r\nANIMATIONS\r\n - Changed command list to have hex number prefix, instead of decimal\r\n -- Additionally for the list, removed names for unused commands\r\n - Fixed \"Timing for single button inputs\" (0xCE, 0xCF) from reversing params when hitting apply\r\n - Added blue color highlight for Jump to Address commands (0xCE, 0xCF, 0xD0, 0xD8)\r\n - Fixed potential crash selecting command menu child (like Monster's list) for the first time\r\n\r\nBATTLEFIELDS\r\n - Added \"Show/Hide Allies (P)\" button, similar to the button in FORMATIONS editor\r\n - Added \"Screen\" button to display a SNES resolution screen (256 x 224)\r\n - Fixed bug where copying a selection, then changing battlefield #, would paste the selection in the prior battlefield\r\n\r\nDIALOGUES\r\n - Fixed dialogues from saving incorrectly, corrupting them. (Thank you Doomsday31415!)\r\n - Fixed bug where going above index 5 (or something) in battle messages would break the UI\r\n\r\nEVENTS \r\n - Fixed Edit Label window not writing to event label\r\n - Added more event names, including complete list of \"Intro Demo\" (Courtesy of Sukasa of now-defunct Acmlm Kafuka BoardII)\r\n\r\nFORMATIONS\r\n - Fixed preview ally portraits from being mirrored horizonally\r\n\r\nMENUS\r\n - Can now edit Foreground and Background using tools found in other editors\r\n - Added \"Overworld Menu - Star Pieces\" to view and edit palettes and tilesets for this menu\r\n - Added editable overworld menu list\r\n - Fixed bug where menu palettes would carry over from previously loaded ROMs.\r\n - Updated HelpTips to reflect new changes\r\n\r\nMONSTERS\r\n - Fixed typo in battle scripts \"Set Target Invicibilty\"\r\n\r\nWORLD MAPS\r\n - Added edit-able Sprite Graphics (yippie!)\r\n - Added edit-able Sprite Palettes (yippie!!)\r\n - Mario's Overworld sprite can be previewed by clicking the Banners button\r\n - Fixed bug where tile editing was enabled when Banners and/or Locations were on\r\n\r\nSPRITES\r\n - Updated the following names:\r\n97 - \"Toadstool's Parachute\" -> \"Snifit's Parachute\"\r\n135 - \"Mine Cart (bad palette)\" -> \"Mine Cart (Seq7 Overworld Sprite)\"\r\n144 - \"Red Dot?\" -> \"Apple (Yo'ster Isle intro sequence)\"\r\n174- \"Small Candy Cloud\" -> \"Mario on Nimbus Busman (Bowser's Keep cutscene)\"\r\n222 - \"banana peel\" -> \"Toadstool Marrymore Accessories\"\r\n521 - \"light blue stars\" -> \"Battle stars (on hit)\"\r\n526 - \"yellow mist / stream\" -> \"Geno's Star Form (Ending Credits)\"\r\n527 - \"yellow mist / stream forms into small star\" -> \"Geno's Bullets and Star Gun\"\r\n786 - \"Wind Crystal\" -> \"Wind Crystal and Fire Crystal\"\r\n789 - \"Water Crystal\" -> \"Water Crystal and Earth Crystal\"\r\n956 - \"normal yellow 5-pronged star\" -> \"Geno in Star Form (Ending Credits)\"\r\n957 - \"brown object dissipating\" -> \"The End background (Ending Credits)\"\r\n958 -> \"tiny glowing pixel\" -> \"Geno's Star's glow effect (Ending Credits)\"\r\n" };

        private string[] v1_3_2 = new string[2] { "v1.3.2", "MONSTERS\r\n- Fixed flower bonus % not saving" };
        private string[] v1_3_1 = new string[2] { "v1.3.1", "MAIN\r\n- Updated all icons to those of Lazy Shell 4.0.0\r\n- File path for ROM now shows just the ROM file's name. (Click the ROM file name to reveal full path)\r\n\r\nANIMATIONS\r\n- Added Weapon Timed-Hit Sounds (for when a weapon is timed correctly)\r\n- Added Character Weapon Scripts (for when characters draw near to attack a monster)" };
        private string[] v1_3_0 = new string[2] { "v1.3.0", "ANIMATIONS\r\n- Added documentation of Ally button inputs, used by opcodes CC, CD, CE, CF, D0, D1, D2, D3, D4, D8.\r\n\r\nATTACKS\r\n- Removed being able to edit the timings for certain ally spells. (i.e. Super Jump, Bowser Crush, etc)\r\n- In place of that, added ability to change the timing of any ally spell and their damage properties.\r\n" };
        private string[] v1_2_8 = new string[2] { "v1.2.8", "ANIMATIONS\r\n- Changed length displayed for commands A7 and DB." };
        private string[] v1_2_7 = new string[2] { "v1.2.7", "DIALOGUES\r\n- Resolved Battle Menu GFX editor displaying the wrong tiles." };
        private string[] v1_2_6 = new string[2] { "v1.2.6", "ANIMATIONS\r\n- At 0xD5: Added formation position #, fixed bug\r\n- At 0x00: Removed \"use palette row 4\", added \"Palette Row #\" value\r\n- A5 0x96: Battle Messages can now be set above 6\r\n\r\nMONSTERS\r\n- Clearing a Monster's Stats will reset win items and Yoshi cookie to the correct value" };
        private string[] v1_2_5 = new string[2] { "v1.2.5", "EVENTS\r\n- Changed \"$70Ax = $7xxx\" to \"$70Ax = $7000\" due to mislabel\r\n- Increased length of HEX display at the bottom\r\n- Added \"(NOT including {xx})\" to randomized number codes.\r\n- Changed description of 3A and 3B commands to be more accurate\r\n\r\nFORMATIONS\r\n- Clearing a formation will set the event # to 102 (from 0) and music to CURRENT (from Normal)" };
        private string[] v1_2_4 = new string[2] { "v1.2.4", "MAIN\r\n- Increased height of search box results\r\n\r\nANIMATIONS\r\n- Fixed bug with monster behavior scripts\r\n\r\nITEMS\r\n- Added hex editor next to item index" };
        private string[] v1_2_3 = new string[2] { "v1.2.3", "MAIN\r\n- Added original SMRPG Documents by giangurgolo, updated by Yakibomb\r\n\r\nANIMATIONS\r\n- At 0xD5: Fixed Summon monster display\r\n- Added numerals next to opcodes\r\n\r\nEVENTS\r\n- Removed numerals from sound lists" };
        private string[] v1_2_2 = new string[2] { "v1.2.2", "ANIMATIONS\r\n- Added new scripts: Faint, Run Away and Victory (under Monster Behavior #1-3)\r\n\r\nEVENTS\r\n- To Open menu commands: Added \"close menu and refresh VRAM\" and now script displays what menu/event will run\r\n" };
        private string[] v1_2_1 = new string[2] { "v1.2.1", "EVENTS\r\n- Switched Memory commands $7xxxx = $7xxxx because it was confusing to read" };
        private string[] v1_2_0 = new string[2] { "v1.2.0", "AUDIO\r\n- Changed names of event sounds and battle sounds\r\n\r\nANIMATIONS\r\n- At 0xD5: Added undocumented Summon Monster\r\n- Added ability to select and edit unused DUMMY spells for allies\r\n- Changed the ability to select sounds and music using an index number\r\n- Removed the numbers from the left of the sound list (for faster searching)\r\n- At 0x00: added undocumented \"behind all sprites\" and \"overlap all sprites\" properties\r\n\r\nEVENTS\r\n- Fixed bug where \"If Memory $704x [x @ 7000] bit {xx} clear...\" auto-jumped to another command\r\n- Fixed bug where \"Shadow On\" was unselectable\r\n- Added undocumented 3A and 3B commands: Jump to if object is less than x tiles close to Mario\r\n" };
        private string[] v1_1_0 = new string[2] { "v1.1.0", "EVENTS\r\n- Switched joypad commands descriptions in category list\r\n- Updated parameters for \"Display pre-game intro title...\" to display correctly in the editor script\r\n" };
        private string[] v1_0_0 = new string[2] { "v1.0.0", "MAIN\r\n-Changed file icon for LazyShell\r\n-Changed the ABOUT LAZY SHELL... to this editor's changelog\r\n\r\nANIMATIONS\r\n- Added ability to edit Flower Bonus Messages, Toad's Battle Tutorial and Weapon Miss Sounds\r\n- Added ability to select and edit unused DUMMY spells for allies\r\n\r\nEVENTS\r\n- Added undocumented bit 7 of Run Background Event... as \"Run as second script\"\r\n- Added move script thread commands under category \"Events\"\r\n- Added undocumented commands to walk and transfer to Memory $7016-1B\r\n- Added undocumented \"Play sound (ch4.5.) ...\"\r\n\r\nLEVELS\r\n - Added undocumented commands {B2,b4} and {B3,b0} as \"Can't enter doors\" and \"Can't walk up stairs\".\r\n\r\nITEMS\r\n - Added new attribute: \"Add stats (item)\"\r\n\r\nWORLD MAPS\r\n - Added ability to view (not edit) Mario's map sprite" };
    }
}