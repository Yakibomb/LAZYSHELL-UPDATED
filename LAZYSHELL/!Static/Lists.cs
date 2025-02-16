using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Net.PeerToPeer.Collaboration;
using System.Security.Cryptography;
using System.Timers;

namespace LAZYSHELL
{
    public static class Lists
    {
        #region Variables
        #region Other
        public static string[] ShopNames = new string[]
        {
            "Mushroom Kingdom",
            "Rose Town Items",
            "Rose Town Armor",
            "Frog Disciple Shop",
            "Moleville Shop",
            "Marrymore Shop",
            "Frog Coin Emporium",
            "Sea Item shop",
            "Seaside Town Items (pre-Yaridovich)",
            "Juice Bar (no card)",
            "Juice Bar (alto card)",
            "Juice Bar (tenor card)",
            "Juice Bar (soprano card)",
            "Seaside Weapons",
            "Seaside Armor",
            "Seaside Accessory",
            "Seaside Health Foods",
            "Monstro Town shop",
            "Nimbus Land shop",
            "Hinopio's Shop",
            "Baby Goomba shop",
            "Nimbus Land Item/Weapon",
            "Croco's Shop 1",
            "Croco's Shop 2",
            "Toad's Shop",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY"
        };
        public static string[] AnimationCommands = new string[]
        {
            "New object: sprite = {xx}, coords (AMEM $32)",			// 0x00
            "AMEM $32 = coords (x,y,z)",			// 0x01
            "{02}",			// 0x02
            "Sprite = {xx}, coords (AMEM $32)",			// 0x03
            "Pause script until...",			// 0x04
            "Remove object",			// 0x05
            "{06}",			// 0x06
            "Return object queue",			// 0x07
            "Move object: speed = {xx}",			// 0x08
            "Jump to address $xxxx",			// 0x09
            "Pause script for 1 frame",			// 0x0A
            "AMEM $40 = coords (x,y,z)",			// 0x0B
            "Move sprite to coords (AMEM $40)",			// 0x0C
            "{0D}",			// 0x0D
            "Reset target mapping memory",			// 0x0E
            "Reset object mapping memory",			// 0x0F
			
            "Jump to subroutine $xxxx",			// 0x10
            "Return subroutine",			// 0x11
            "{12}",			// 0x12
            "{13}",			// 0x13
            "{14}",			// 0x14
            "{15}",			// 0x15
            "{16}",			// 0x16
            "{17}",			// 0x17
            "{18}",			// 0x18
            "{19}",			// 0x19
            "Visibility on",			// 0x1A
            "Visibility off",			// 0x1B
            "{1C}",			// 0x1C
            "{1D}",			// 0x1D
            "{1E}",			// 0x1E
            "{1F}",			// 0x1F
			
            "AMEM (8-bit) $xx = variable {xx}",			// 0x20
            "AMEM (16-bit) $xx = variable {xx}",			// 0x21
            "Variable {xx} = AMEM (8-bit) $xx",			// 0x22
            "Variable {xx} = AMEM (16-bit) $xx",			// 0x23
            "If AMEM (8-bit) $xx = {xx} ...",			// 0x24
            "If AMEM (16-bit) $xx = {xx} ...",			// 0x25
            "If AMEM (8-bit) $xx != {xx} ...",			// 0x26
            "If AMEM (16-bit) $xx != {xx} ...",			// 0x27
            "If AMEM (8-bit) $xx < {xx} ...",			// 0x28
            "If AMEM (16-bit) $xx < {xx} ...",			// 0x29
            "If AMEM (8-bit) $xx >= {xx} ...",			// 0x2A
            "If AMEM (16-bit) $xx >= {xx} ...",			// 0x2B
            "AMEM (8-bit) $xx += {xx}",			// 0x2C
            "AMEM (16-bit) $xx += {xx}",			// 0x2D
            "AMEM (8-bit) $xx -= {xx}",			// 0x2E
            "AMEM (16-bit) $xx -= {xx}",			// 0x2F
			
            "Increment AMEM (8-bit) $xx",			// 0x30
            "Increment AMEM (16-bit) $xx",			// 0x31
            "Decrement AMEM (8-bit) $xx",			// 0x32
            "Decrement AMEM (16-bit) $xx",			// 0x33
            "Clear AMEM (8-bit) $xx",			// 0x34
            "Clear AMEM (16-bit) $xx",			// 0x35
            "Set AMEM $xx bits {xx}",			// 0x36
            "Clear AMEM $xx bits {xx}",			// 0x37
            "If AMEM $xx bits {xx} set...",			// 0x38
            "If AMEM $xx bits {xx} clear...",			// 0x39
            "Attack timer begins",			// 0x3A
            "{3B}",			// 0x3B
            "{3C}",			// 0x3C
            "{3D}",			// 0x3D
            "{3E}",			// 0x3E
            "{3F}",			// 0x3F
			
            "Pause script until AMEM $xx bits {xx} set",			// 0x40
            "Pause script until AMEM $xx bits {xx} clear",			// 0x41
            "{42}",			// 0x42
            "Sprite sequence = {xx}",			// 0x43
            "{44}",			// 0x44
            "AMEM $60 = current target",			// 0x45
            "Check ally mortal status, if all allies down set game over",			// 0x46
            "{47}",			// 0x47
            "{48}",			// 0x48
            "{49}",			// 0x49
            "{4A}",			// 0x4A
            "{4B}",			// 0x4B
            "{4C}",			// 0x4C
            "{4D}",			// 0x4D
            "Pause script until sprite sequence done",			// 0x4E
            "{4F}",			// 0x4F
			
            "If target disabled, jump to address $xxxx",			// 0x50
            "If target alive, jump to address $xxxx",			// 0x51
            "{52}",			// 0x52
            "{53}",			// 0x53
            "{54}",			// 0x54
            "{55}",			// 0x55
            "{56}",			// 0x56
            "{57}",			// 0x57
            "{58}",			// 0x58
            "{59}",			// 0x59
            "{5A}",			// 0x5A
            "{5B}",			// 0x5B
            "{5C}",			// 0x5C
            "Sprite queue [$offset] (sprite = {xx})",			// 0x5D
            "Return sprite queue",			// 0x5E
            "{5F}",			// 0x5F
			
            "{60}",			// 0x60
            "{61}",			// 0x61
            "{62}",			// 0x62
            "Display {xx} message @ OMEM $60",			// 0x63
            "Object queue [$offset] index = AMEM $60",			// 0x64
            "{65}",			// 0x65
            "{66}",			// 0x66
            "{67}",			// 0x67
            "Object queue [$offset, AMEM $60] index = {xx}",			// 0x68
            "OMEM $60 = memory $072C",			// 0x69
            "AMEM $xx = random # between 0 and {xx}",			// 0x6A
            "AMEM $xx = random # between 0 and {xx}",			// 0x6B
            "{6C}",			// 0x6C
            "{6D}",			// 0x6D
            "{6E}",			// 0x6E
            "{6F}",			// 0x6F
			
            "Overlap all",			// 0x70
            "Overlap none",			// 0x71
            "New object: effect = ...",			// 0x72
            "Pause script for 2 frames",			// 0x73
            "Pause script until {xx} complete...",			// 0x74
            "Pause script until bits clear...",			// 0x75
            "Clear effect index",			// 0x76
            "L3 on...",			// 0x77
            "L3 off...",			// 0x78
            "{79}",			// 0x79
            "Display message...",			// 0x7A
            "Pause script until dialogue closed",			// 0x7B
            "{7C}",			// 0x7C
            "{7D}",			// 0x7D
            "Fade out object, duration = ...",			// 0x7E
            "Reset sprite sequence",			// 0x7F
			
            "Shine effect...",			// 0x80
            "{81}",			// 0x81
            "{82}",			// 0x82
            "{83}",			// 0x83
            "{84}",			// 0x84
            "Fade object {xx}, amount = ...",			// 0x85
            "Shake object...",			// 0x86
            "Stop shaking object",			// 0x87
            "{88}",			// 0x88
            "{89}",			// 0x89
            "{8A}",			// 0x8A
            "{8B}",			// 0x8B
            "{8C}",			// 0x8C
            "{8D}",			// 0x8D
            "Screen flash {xx} color, duration = ...",			// 0x8E
            "Screen flash {xx} color",			// 0x8F
			
            "{90}",			// 0x90
            "{91}",			// 0x91
            "{92}",			// 0x92
            "{93}",			// 0x93
            "{94}",			// 0x94
            "Initialize bonus message sequence",			// 0x95
            "Display bonus message...",			// 0x96
            "Pause script until bonus message complete",			// 0x97
            "{98}",			// 0x98
            "{99}",			// 0x99
            "{9A}",			// 0x9A
            "{9B}",			// 0x9B
            "{9C}",			// 0x9C
            "{9D}",			// 0x9D
            "{9E}",			// 0x9E
            "{9F}",			// 0x9F
			
            "{A0}",			// 0xA0
            "{A1}",			// 0xA1
            "{A2}",			// 0xA2
            "Screen effect...",			// 0xA3
            "{A4}",			// 0xA4
            "{A5}",			// 0xA5
            "{A6}",			// 0xA6
            "{A7}",			// 0xA7
            "{A8}",			// 0xA8
            "{A9}",			// 0xA9
            "{AA}",			// 0xAA
            "Play sound (ch.6,7)...",			// 0xAB
            "{AC}",			// 0xAC
            "{AD}",			// 0xAD
            "Play sound (ch.4,5)...",			// 0xAE
            "{AF}",			// 0xAF
			
            "Play music {xx} (current volume)",			// 0xB0
            "Play music {xx} (volume = {xx})",			// 0xB1
            "Stop current sound effect",			// 0xB2
            "{B3}",			// 0xB3
            "{B4}",			// 0xB4
            "{B5}",			// 0xB5
            "Fade out current music to {xx} volume...",			// 0xB6
            "{B7}",			// 0xB7
            "{B8}",			// 0xB8
            "{B9}",			// 0xB9
            "{BA}",			// 0xBA
            "Set target...",			// 0xBB
            "Modify item inventory...",			// 0xBC
            "Modify special item inventory...",			// 0xBD
            "Coins += ...",			// 0xBE
            "Store to item inventory {xx}'s Yoshi Cookie",			// 0xBF
			
            "{C0}",			// 0xC0
            "{C1}",			// 0xC1
            "{C2}",			// 0xC2
            "Mask effect...",			// 0xC3
            "{C4}",			// 0xC4
            "{C5}",			// 0xC5
            "Mask coords = ...",			// 0xC6
            "{C7}",			// 0xC7
            "{C8}",			// 0xC8
            "{C9}",			// 0xC9
            "{CA}",			// 0xCA
            "Sprite sequence speed = ...",			// 0xCB
            "Start tracking for Ally Button Timing",			// 0xCC
            "End tracking for Ally Button Timing",			// 0xCD
            "Timing for One Button Press: Just-OK/Perfect range",			// 0xCE
            "Timing for One Button Press: Any timing range",			// 0xCF
            "Timing for Multiple Button Presses: Wait # frames, then jump",			// 0xD0
            "Timing for Button Mash: ???",			// 0xD1
            "Timing for Button Mash: Possible Power-ups range",			// 0xD2
            "Timing for Rotation: Frame range, Possible Power-ups range",			// 0xD3
            "Timing for Held Button: Hold for frame range",			// 0xD4

            "Summon monster...",			// 0xD5
            "{D6}",			// 0xD6
            "{D7}",			// 0xD7
            "Timing for Mute: ??? then jump",			// 0xD8
            "Display \"Can\'t run\" dialogue",			// 0xD9
            "{DA}",			// 0xDA
            "{DB}",			// 0xDB
            "{DC}",			// 0xDC
            "{DD}",			// 0xDD
            "{DE}",			// 0xDE
            "{DF}",			// 0xDF
			
            "Store OMEM $60 to item inventory",			// 0xE0
            "Run battle event...",			// 0xE1
            "{E2}",			// 0xE2
            "{E3}",			// 0xE3
            "{E4}",			// 0xE4
            "{E5}",			// 0xE5
            "{E6}",			// 0xE6
            "{E7}",			// 0xE7
            "{E8}",			// 0xE8
            "{E9}",			// 0xE9
            "{EA}",			// 0xEA
            "{EB}",			// 0xEB
            "{EC}",			// 0xEC
            "{ED}",			// 0xED
            "{EE}",			// 0xEE
            "{EF}",			// 0xEF
			
            "{F0}",			// 0xF0
            "{F1}",			// 0xF1
            "{F2}",			// 0xF2
            "{F3}",			// 0xF3
            "{F4}",			// 0xF4
            "{F4}",			// 0xF4
            "{F6}",			// 0xF6
            "{F7}",			// 0xF7
            "{F8}",			// 0xF8
            "{F9}",			// 0xF9
            "{FA}",			// 0xFA
            "{FB}",			// 0xFB
            "{FC}",			// 0xFC
            "{FD}",			// 0xFD
            "{FE}",			// 0xFE
            "{FF}"			// 0xFF
        };
        public static string[] Tutorials = new string[]
        {
            "How to equip",
            "How to use items",
            "How to switch allies",
            "How to play beetle mania"
        };
        public static string[] EntranceNames = new string[]
        {
            "no movement for \"Escape\"",
            "slide backward when hit",
            "Bowser Clone sprite",
            "Mario Clone sprite",
            "no reaction when hit",
            "sprite shadow",
            "floating, sprite shadow",
            "floating",
            "floating, slide backward when hit",
            "floating, slide backward when hit",
            "fade out death, floating",
            "fade out death",
            "fade out death",
            "fade out death, Smithy spell cast",
            "fade out death, no \"Escape\" movement",
            "fade out death, no \"Escape\" transition",
            "(normal)",
            "no reaction when hit"
        };
        public static string[] CoinSizes = new string[]
        {
            "none","small","big"
        };
        public static string[] SpriteBehaviors = new string[] {
            "no movement for \"Escape\"",
            "slide backward when hit",
            "Bowser Clone sprite",
            "Mario Clone sprite",
            "no reaction when hit",
            "sprite shadow",
            "floating, sprite shadow",
            "floating",
            "floating, slide backward when hit",
            "floating, slide backward when hit",
            "fade out death, floating",
            "fade out death",
            "fade out death",
            "fade out death, Smithy spell cast",
            "fade out death, no \"Escape\" movement",
            "fade out death, no \"Escape\" transition",
            "(normal)",
            "no reaction when hit"
        };
        public static string[] MonsterSoundStrike = new string[]
        {
            "bite",
            "pierce",
            "claw",
            "blade",
            "slap",
            "knock",
            "smash",
            "wallop 2",
            "wallop",
            "bonk",
            "flopping hit",
            "wallop 3",
            "wallop 4",
            "wallop 4"
        };
        public static string[] MonsterSoundOther = new string[]
        {
            "none",
            "Starslap, Spikey, Enigma",
            "Sparky, Goomba, Birdy",
            "Amanita, Terrapin",
            "Guerilla",
            "Pulsar",
            "Dry Bones",
            "Torte"
        };
        public static string[] CharacterNames
        {
            get
            {
                string[] characterNames = new string[Model.Characters.Length];
                for (int i = 0; i < Model.Characters.Length; i++)
                    characterNames[i] = Model.CharacterNames.GetUnsortedName(i);
                return characterNames;
            }
        }
        public static string[] ButtonNames = new string[]
        {
            "left", "right", "down", "up", "X", "A", "Y", "B"
        };
        public static string[] Directions = new string[]
        {
            "east","southeast","south","southwest",
            "west","northwest","north","northeast"
        };
        public static string[] ColorNames = new string[]
        {
            "black", "blue", "red", "pink", "green", "aqua", "yellow", "white"
        };
        public static string[] LayerNames = new string[]
        {
            "L1", "L2", "L3", "L4", "NPC", "BG", "½ intensity", "Minus sub"
        };
        public static string[] MenuNames = new string[]
        {
            "open game select menu",
            "open overworld menu",
            "open location",
            "open shop menu",
            "open save game menu",
            "open items maxed out menu",
            "UNKNOWN",
            "run menu tutorial",
            "add star piece",
            "run Moleville Mountain",
            "close menu and refresh VRAM",
            "run Moleville Mountain intro",
            "close menu and refresh VRAM",
            "run star piece end sequence",
            "run garden intro sequence",
            "enter gate to Smithy Factory",
            "run world map event sequence"
        };
        #endregion
        #region Maps
        public static string[] WorldMapNames = new string[]
        {
            "Bowser\'s Keep",
            "Mushroom Kingdom",
            "Rose Town",
            "Booster Tower",
            "Seaside Town",
            "Land\'s End",
            "Nimbus Land",
            "Barrel Volcano",
            "Yo\'ster Isle"
        };
        public static string[] MapNames = new string[]
        {
            "To Mario's Pad (before)",
            "Bowser's Keep (before)",
            "To Mario's Pad",
            "Vista Hill",
            "Bowser's Keep",
            "Gate",
            "To Nimbus Land",
            "To Bowser's Keep",
            "Mario's Pad",
            "Mushroom Way",
            "Mushroom Kingdom",
            "Bandit's Way",
            "Kero Sewers",
            "To Mushroom Kingdom",
            "Kero Sewers",
            "Midas River",
            "Tadpole Pond",
            "Rose Way",
            "Rose Town",
            "Forest Maze",
            "Pipe Vault",
            "To Yo'ster Isle",
            "To Moleville",
            "To Pipe Vault",
            "Moleville",
            "Booster Pass",
            "Booster Tower",
            "Booster Hill",
            "Marrymore",
            "To Star Hill",
            "To Marrymore",
            "Star Hill",
            "Seaside Town",
            "Sea",
            "Sunken Ship",
            "To Land's End",
            "To Seaside Town",
            "Land's End",
            "Monstro Town",
            "Bean Valley",
            "Grate Guy's Casino",
            "To Nimbus Land",
            "To Seaside Town",
            "Land's End",
            "Monstro Town",
            "Bean Valley",
            "Grate Guy's Casino",
            "To Nimbus Land",
            "To Bean Valley",
            "Nimbus Land",
            "Barrel Volcano",
            "To Bowser's Keep",
            "Yo'ster Isle",
            "To Pipe Vault",
            "Coal Mines (Bowser's Keep)",
            "Factory (Bowser's Keep)"
        };
        public static string[] objectNames = new string[]
        {
            "Mario",// 0x00
            "Toadstool",			// 0x01
            "Bowser",			// 0x02
            "Geno",// 0x03
            "Mallow",			// 0x04
            "DUMMY 0x05",			// 0x05
            "DUMMY 0x06",			// 0x06
            "DUMMY 0x07",			// 0x07
            "Character in Slot 1",// 0x08
            "Character in Slot 2",// 0x09
            "Character in Slot 3",// 0x0A
            "DUMMY 0x0B",			// 0x0B
            "Screen Focus",			// 0x0C
            "Layer 1",			// 0x0D
            "Layer 2",			// 0x0E
            "Layer 3",			// 0x0F
            			
            "Mem $70A8",			// 0x10
            "Mem $70A9",			// 0x11
            "Mem $70AA",			// 0x12
            "Mem $70AB",			// 0x13
            "NPC #0",			// 0x14
            "NPC #1",			// 0x15
            "NPC #2",			// 0x16
            "NPC #3",			// 0x17
            "NPC #4",			// 0x18
            "NPC #5",			// 0x19
            "NPC #6",			// 0x1A
            "NPC #7",			// 0x1B
            "NPC #8",			// 0x1C
            "NPC #9",			// 0x1D
            "NPC #10",			// 0x1E
            "NPC #11",			// 0x1F
            			
            "NPC #12",			// 0x20
            "NPC #13",			// 0x21
            "NPC #14",			// 0x22
            "NPC #15",			// 0x23
            "NPC #16",			// 0x24
            "NPC #17",			// 0x25
            "NPC #18",			// 0x26
            "NPC #19",			// 0x27
            "NPC #20",			// 0x28
            "NPC #21",			// 0x29
            "NPC #22",			// 0x2A
            "NPC #23",			// 0x2B
            "NPC #24",			// 0x2C
            "NPC #25",			// 0x2D
            "NPC #26",			// 0x2E
            "NPC #27"			// 0x2F
        };

        public static string[] ObjectNames
        {
            get
            {
                for (int i = 0; i < Model.Characters.Length; i++)
                    objectNames[i] = Model.CharacterNames.GetUnsortedName(i);
                return objectNames;
            }
        }
        #endregion
        #region Audio
        public static int[] SMWSamples = new int[]{
            82, 65, 104,78, 44, 66, 23, 96, 100,56,
            18, 65, 109,39, 101,17, 38, 113,65, 0,
            0,  51, 52, 53, 67, 67, 51, 51, 51, 69
        };
        public static int[] SMWPercussives = new int[]{
            -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
            0, -1,1, -1,-1,-1,-1,-1,-1,-1,
            -1,2, 3, 4, 5, 6, 7, 8, -1,9
        };
        public static int[] SMWOctaveLimits = new int[]{
            5,6,6,6,4,5,6,5,5,5,
            4,6,6,5,5,6,6,6,4,4,
            4,4,4,4,4,4,4,4,4,4
        };
        public static bool[] SMRPGPercussives = new bool[]
        {
            false,false,false,false,false,false,false,false,false,false,
            false,false,false,false,false,false,false,false,true, false,
            false,false,false,false,false,true, true, false,false,false,
            false,false,true, true, true, true, false,false,false,false,
            false,false,true, true, false,true, true, true, false,false,
            false,true, true, true, true, false,false,false,false,false,
            true, true, true, false,false,false,false,true, true, true,
            false,false,true, true, false,true, true, true, false,true,
            true, true, false,false,true, false,false,false,false,false,
            false,true, false,false,false,false,false,true, true, true,
            false,false,false,false,false,false,true, true, true, true,
            false,false,false,false,true, false
        };
        public static int[] SMRPGtoSMWSamples = new int[]{
          /*0   1   2   3   4   5   6   7   8   9*/
            29, 29, 29, 29, 29, 29, 29, 29, 29, 29,  // 0
            29, 29, 29, 29, 29, 29, 29, 15, 10, 0,   // 10
            8,  6,  0,  6,  6,  26, 25, 0,  0,  6,   // 20
            0,  11, 15, 23, 2,  22, 22, 26, 16, 13,  // 30
            13, 9,  10, 10, 6,  26, 28, 12, 29, 0,   // 40
            11, 27, 23, 23, 26, 3,  9,  9,  29, 1,   // 50
            10, 10, 12, 11, 2,  1,  5,  24, 26, 10,  // 60
            8,  1,  23, 10, 13, 23, 12, 12, 3,  10,  // 70
            0,  25, 0,  6,  23, 0,  29, 9,  4,  4,   // 80
            10, 12, 8,  16, 6,  0,  7,  23, 23, 25,  // 90
            8,  14, 11, 2,  2,  2,  29, 12, 12, 12,  // 100
            17, 29, 29, 17, 30, 5
        };
        public static string[] SPCCommands = new string[]
        {
            "Note",
            "Note, duration = ",
            "Octave up",
            "Octave down",
            "Octave = ",
            "Slur next note",
            "Noise on, channels = ",
            "Noise on, all channels",
            "Noise off",
            "Frequency mode on",
            "Frequency mode off",
            "Play channel 0 of SFX = ",
            "Play channel 1 of SFX = ",
            "Transpose 1/16 pitch = ",
            "Terminate script",
            "Beat duration = ",
            "Audio memory $69 = ",
            "{D3-xx}",
            "Begin repeat",
            "End repeat",
            "Repeat ending start",
            "Begin infinite repeat",
            "ADSR reset",
            "ADSR attack = ",
            "ADSR decay = ",
            "ADSR sustain = ",
            "ADSR release = ",
            "Sample length = ",
            "Sample = ",
            "Noise transpose, pitch = ",
            "Sample fadeout = ",
            "{E1-xx}",
            "Volume = ",
            "Volume shift, amount = ",
            "Volume slide, duration = ",
            "Portamento, duration = ",
            "Portamento looping",
            "Speaker balance = ",
            "Pansweep, duration = ",
            "Pansweep loop, duration = ",
            "{EA}",
            "{EB}",
            "Transpose 1/4 pitch from 0 = ",
            "Transpose 1/4 pitch = ",
            "Percussion mode on",
            "Percussion mode off",
            "Tremolo = ",
            "Vibrato = ",
            "Beat duration, change = ",
            "Vibrato off",
            "Growling, roughness = ",
            "{F5}",
            "Portamento on = ",
            "Growling off",
            "Dizziness on",
            "Dizziness off",
            "Reverb on",
            "Reverb off",
            "Reverb = ",
            "{FD}",
            "{FE}",
            "{FF}"
        };
        public static string[] Pitches = new string[]
        {
            "C0",
            "C#0",
            "D0",
            "D#0",
            "E0",
            "F0",
            "F#0",
            "G0",
            "G#0",
            "A0",
            "A#0",
            "B0",
            "C1",
            "C#1",
            "D1",
            "D#1",
            "E1",
            "F1",
            "F#1",
            "G1",
            "G#1",
            "A1",
            "A#1",
            "B1",
            "C2",
            "C#2",
            "D2",
            "D#2",
            "E2",
            "F2",
            "F#2",
            "G2",
            "G#2",
            "A2",
            "A#2",
            "B2",
            "C3",
            "C#3",
            "D3",
            "D#3",
            "E3",
            "F3",
            "F#3",
            "G3",
            "G#3",
            "A3",
            "A#3",
            "B3",
            "C4",
            "C#4",
            "D4",
            "D#4",
            "E4",
            "F4",
            "F#4",
            "G4",
            "G#4",
            "A4",
            "A#4",
            "B4",
            "C5",
            "C#5",
            "D5",
            "D#5",
            "E5",
            "F5",
            "F#5",
            "G5",
            "G#5",
            "A5",
            "A#5",
            "B5",
            "C6",
            "C#6",
            "D6",
            "D#6",
            "E6",
            "F6",
            "F#6",
            "G6",
            "G#6",
            "A6",
            "A#6",
            "B6",
            "C7",
            "C#7",
            "D7",
            "D#7",
            "E7",
            "F7",
            "F#7",
            "G7",
            "G#7",
            "A7",
            "A#7",
            "B7",
            "C8",
            "C#8",
            "D8",
            "D#8",
            "E8",
            "F8",
            "F#8",
            "G8",
            "G#8",
            "A8",
            "A#8",
            "B8"
        };
        public static string[] SampleNames = new string[]
        {
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX",
            "SOUND FX: shell kick",
            "SILENCE",
            "SILENCE",
            "SILENCE",
            "SILENCE",
            "SILENCE",
            "SILENCE",
            "Timpani",
            "Orchestra Hit",
            "Snare",
            "Clarinet",
            "Synth Bass",
            "Baritone Sax",
            "Bassoon",
            "Muted Trumpet",
            "Brass Section",
            "Metronome 1",
            "Metronome 2",
            "Oboe",
            "Bassoon",
            "Tuba",
            "Flute",
            "String Section 1",
            "DJ scratch",
            "Tambourine",
            "Bell",
            "Drum & cymbal",
            "Crash Cymbal",
            "Synth Drum",
            "Harp 1",
            "Harp 2",
            "Pizzicato",
            "Old Piano",
            "Fat Snare",
            "Drum Roll",
            "Trumpet",
            "Slap",
            "Melodic Tom",
            "Drum",
            "Jungle Drums Riff",
            "Pipe",
            "Accordion",
            "Bass Drum",
            "Pedal Hi-Hat",
            "Closed Hi-Hat",
            "Clap",
            "Marimba",
            "Honky Tonk Piano 1",
            "Honky Tonk Piano 2",
            "Square",
            "Sawtooth",
            "Drum",
            "Gated Snare",
            "Tap",
            "Choir Aahs",
            "Celesta",
            "Violin",
            "Clean Guitar",
            "Woodblock",
            "Kick Drum",
            "Snare",
            "Synth Bass",
            "Electric Violin",
            "Open Hi-Hat",
            "Shaker",
            "Electric Piano",
            "Maraca",
            "Tom-tom 1",
            "Tom-tom 2",
            "Xylophone",
            "Drum",
            "Pea Whistle",
            "Cowbell",
            "Recorder",
            "French Horn",
            "Open Hi-Hat",
            "Synth Rain",
            "Synth Orchestra",
            "Acoustic Piano",
            "Organ",
            "Cello",
            "Drum",
            "Drum",
            "Bass Guitar",
            "Pizzicato",
            "Saxophone",
            "Flute",
            "Steel Drums",
            "Guiro 1",
            "Guiro 2",
            "Agogo",
            "Acoustic Bass Guitar",
            "Fretless Bass",
            "String Section 2",
            "Bell",
            "Glockenspiel",
            "Vibraphone",
            "Seashore",
            "Dry Tom",
            "Tap",
            "Tom-tom",
            "Lead Guitar",
            "Laugh 1",
            "Laugh 2",
            "Distortion Guitar",
            "Metal Hammer",
            "Harpsichord"
        };
        public static string[] MusicNames = new string[]
        {
            "{CURRENT}",
            "Dodo\'s Coming",
            "Mushroom Kingdom",
            "Fight Against Stronger Monster",
            "Yo\'ster Island",
            "Seaside Town",
            "Fight Against Monsters",
            "Pipe Vault",
            "Invincible Star",
            "Victory",
            "In The Flower Garden",
            "Bowser\'s Castle (1st time)",
            "Fight Against Bowser",
            "Road Is Full Of Dangers",
            "Mario\'s Pad",
            "Here\'s Some Weapons",
            "Let\'s Race",
            "Tadpole Pond",
            "Rose Town",
            "Race Training",
            "Shock!",
            "Sad Song",
            "Midas River",
            "Got A Star Piece (part 1)",
            "Got A Star Piece (part 2)",
            "Fight Against An Armed Boss",
            "Forest Maze",
            "Dungeon Is Full Of Monsters",
            "Let\'s Play Geno",
            "Start Slot Menu",
            "Long Long Ago",
            "Booster\'s Tower",
            "And My Name\'s Booster",
            "Moleville",
            "Star Hill",
            "Mountain Railroad",
            "Explanation",
            "Booster Hill (start)",
            "Booster Hill",
            "Marrymore",
            "New Partner",
            "Sunken Ship",
            "Still The Road Is Full Of Monsters",
            "{SILENCE}",
            "Sea",
            "Heart Beating A Little Faster (part 1)",
            "Heart Beating A Little Faster (part 2)",
            "Grate Guy\'s Casino",
            "Geno Awakens",
            "Celebrational",
            "Nimbus Land",
            "Monstro Town",
            "Toadofsky",
            "{SILENCE}",
            "Happy Adventure, Delighful Adventure",
            "World Map",
            "Factory",
            "Sword Crashes And Stars Scatter",
            "Conversation With Culex",
            "Fight Against Culex",
            "Victory Against Culex",
            "Valentina",
            "Barrel Volcano",
            "Axem Rangers Drop In",
            "The End",
            "Gate",
            "Bowser\'s Castle (2nd time)",
            "Weapons Factory",
            "Fight Against Smithy 1",
            "Fight Against Smithy 2",
            "Ending Part 1",
            "Ending Part 2",
            "Ending Part 3",
            "Ending Part 4"
        };
        public static string[] SoundNames = new string[]
        {
            "{SILENCE}",
            "menu select",
            "menu cancel",
            "menu scroll",
            "jump",
            "block switch",
            "running water",
            "gushing water",
            "waterfall",
            "green switch",
            "trampoline",
            "whoosh away",
            "dizziness",
            "coin",
            "item get",
            "birds tweeting",
            "open door",
            "open front gate",
            "sudden stop",
            "long fall",
            "lighting bolt",
            "rumbling",
            "close door",
            "helicopter",
            "tapping feet",
            "heel click",
            "laughing Bowser",
            "found an item",
            "pipe entrance",
            "alarm clock",
            "surprised monster",
            "spinning flower",
            "underground warp",
            "jumping bouncing fish",
            "squirm writhe",
            "running water",
            "*Tadpole Pond staff: Do",
            "*Tadpole Pond staff: Re",
            "*Tadpole Pond staff: Mi",
            "*Tadpole Pond staff: Fa",
            "*Tadpole Pond staff: So",
            "*Tadpole Pond staff: La",
            "*Tadpole Pond staff: Ti",
            "slipping",
            "ghost float",
            "Goomba taunt",
            "marching troops",
            "snooze",
            "rolling",
            "big shell hit",
            "water droplet",
            "moving yellow switch",
            "deep bounce",
            "bounce",
            "goodnight",
            "coin shower lose fountain",
            "shake head",
            "finger snap",
            "insert",
            "hovering Frogfucius",
            "dynamite bomb explosion",
            "deep uh-oh",
            "big yoshi talk",
            "yoshi talk",
            "spinning copters",
            "thwomp stomp 1",
            "kick ball shell",
            "sword in keep",
            "*Mallow yelling at Croco",
            "*Mallow falling landing",
            "*Mallow sliding on wall",
            "mushroom cure",
            "syrup cure",
            "thwomp stomp 2",
            "Boosters train",
            "rocketing blast",
            "Boosters train horn",
            "Bowyer arrows dancing",
            "click",
            "*yelp in distance",
            "star explanation",
            "star",
            "screeching stop",
            "weird laugh",
            "smoked fireball",
            "1-up flower",
            "big bounce",
            "correct signal",
            "wrong signal",
            "lit fuse",
            "curtain",
            "tumbling boulders",
            "*Lakitu attaches Frogfucius",
            "water splash",
            "frog coin",
            "level up with star",
            "swinging fist",
            "engage in battle",
            "*puzzle game timer",
            "tapping feet",
            "minecart ride",
            "Terrapin attack",
            "time running out",
            "Toadstool crying",
            "chomp spinning",
            "surprise",
            "off balance",
            "drum roll",
            "drum roll end",
            "big shell hit",
            "abstract Star Hill music",
            "water jumping",
            "draining water",
            "Bullet Bill ignition",
            "*orchestra hit A",
            "*orchestra hit A#",
            "*orchestra hit B",
            "Dry Bones crumble",
            "beckoning Tentacle",
            "Czar Dragon roar",
            "Axem Rangers strike!",
            "Axem Ranger teleport",
            "Sky Troopas approaching",
            "chain rumbling noise",
            "Sgt. Flutter flight",
            "enter Star Hill warp",
            "exit Star Hill warp",
            "Yoshi egg hatch",
            "vine growing",
            "baby yoshi",
            "big baby yoshi",
            "*jump on organ",
            "honking horn",
            "march single",
            "curtain sweep",
            "impending blast",
            "*Toadofsky composition: Do",
            "*Toadofsky composition: Re",
            "*Toadofsky composition: Mi",
            "*Toadofsky composition: Fa",
            "*Toadofsky composition: So",
            "*Toadofsky composition: La",
            "*Toadofsky composition: Ti",
            "metronome upbeat ding",
            "click",
            "blacksmith hammer strike",
            "machine transform",
            "Troopa shell poof hide",
            "Yaridovich tranforms",
            "secret hint",
            "jump on save box",
            "open chapel doors",
            "shuffle cards",
            "slot machines",
            "big squish",
            "whistling Mario theme",
            "Link fanfare",
            "firework ascending",
            "firework bang",
            "firework bang big",
            "chomping",
            "ghost",
            "closing gate door"
        };
        public static string[] BattleSoundNames = new string[]
        {
            "{SILENCE}",
            "menu select",
            "menu cancel",
            "menu scroll",
            "jump",
            "birdie tweet",
            "bonus flower status up",
            "error incorrect answer",
            "get dizzy",
            "arrow sling",
            "Mallow punch 1",
            "swoosh run away",
            "bomb explosion",
            "coin",
            "item get",
            "spike sting",
            "bite",
            "Star Gun shoot",
            "Super Jump hit 1",
            "Drain Beam",
            "Aurora Flash",
            "Scarecrow birdies",
            "Corona descends",
            "small laser?",
            "{NULL}",
            "slap big",
            "Flame Wall",
            "item 1-UP",
            "Flame",
            "fire shoot",
            "fire throw",
            "fire hit",
            "fire burn",
            "insert",
            "?",
            "spell power up",
            "snow",
            "monster item toss",
            "frying pan hit 2",
            "claw",
            "pierce",
            "Super Jump hit 4",
            "blade",
            "coin showers into fountain",
            "Bolt",
            "HP Rain cloud",
            "plasma bounce",
            "dry clunk",
            "Mallow punch 2",
            "cymbal crash",
            "Super Jump hit 5",
            "fire throw big",
            "Finger Shot bullets",
            "Thwomp hit ground",
            "hammer hit 1",
            "hammer hit 2",
            "Super Jump hit 1-UP",
            "fire spout__",
            "Super Jump hit 2",
            "Super Jump hit 3",
            "cymbal resonance",
            "item use",
            "monster run away",
            "Geno Blast ignition",
            "egg hatch",
            "Yoshi cant make cookie",
            "Recover HP",
            "Recover FP",
            "Recover Drink",
            "Geno power up",
            "Geno Beam",
            "Psychopath drum roll",
            "Psychopath cloud appears",
            "Psychopath message",
            "quack__",
            "Yoshi talk",
            "stat boost (single)",
            "stat boost (multi)",
            "timed stat boost",
            "rumble (single)",
            "wallop 1",
            "wallop 2",
            "wallop 3",
            "frying pan hit 1",
            "wallop 4",
            "wallop 5",
            "long fall",
            "big bounce",
            "ticking bomb",
            "common monster explosion",
            "birdie call?__",
            "{NULL}",
            "Spear Rain (single)",
            "Bowyer arrow lock button",
            "Shocker",
            "Bowsers Crusher?__",
            "rumble (multi)",
            "plasma toss",
            "click",
            "Willy Wisp",
            "Electroshock sparks",
            "Stench",
            "Static E!",
            "Crystal hits",
            "Blizzard",
            "Rock Candy",
            "Light Beam",
            "bubble pop",
            "Howl",
            "Geno Hand Cannon shoot",
            "huge explosion",
            "Sledge",
            "swing",
            "Geno Finger Shot hit",
            "Spikey attack",
            "transform",
            "Terrapin swing",
            "sting?__",
            "Geno Blast end",
            "Meteor Swarm",
            "deep swallow",
            "big swing",
            "poisoned",
            "Chomp bite",
            "Goomba roll",
            "spike shot",
            "flopping hit",
            "liquid droplet",
            "Amanita curls",
            "throw?__",
            "Valor Up Vigor Up",
            "echoing bubble__",
            "stinger poison",
            "Lullaby Sad Song",
            "Boo disappears",
            "Boo appears",
            "Boo approaches",
            "Bowser Crush stomp!",
            "Endobubble",
            "guitar string",
            "S'crow Bell",
            "Lullaby Mario Theme",
            "Dry Bones attack",
            "toss",
            "Lightning Orb",
            "Artichoker spines",
            "slap",
            "{NULL}",
            "Smithy bullet fingers",
            "enemy jumps high",
            "enemy taunts then hits",
            "spores",
            "hit",
            "Guerrilla thinks",
            "buzzing bee",
            "Sparky hit",
            "Chomp bite",
            "Enigma scatters",
            "yelp__",
            "big deep hit",
            "slap",
            "Spore Chimes Doom Reverb",
            "{NULL}",
            "{NULL}",
            "Carroboscis attack",
            "Sky Troopa flaps",
            "tapping feet",
            "Belome eats",
            "Terrapin attack",
            "teleport attack",
            "submerged under",
            "slap powerful",
            "weapon timing",
            "Terrorize",
            "Solidify",
            "Deathsickle",
            "boss fade out death",
            "Poison Gas 1",
            "Poison Gas 2",
            "Sleepy Bomb",
            "Sleepy Time timed",
            "Lamb's Lure (single)",
            "Sheep Attack 1",
            "floating lamb",
            "Sheep Attack 2 (multi)",
            "Geno Flash shoot",
            "Geno Flash explosion",
            "Mute balloon rises",
            "Petal Blast",
            "Pollen Nap",
            "Come Back",
            "Mute balloon fails",
            "big shell kick",
            "big shell hit 1",
            "big shell hit 2",
            "explosive hit",
            "Geno Flash transformation",
            "Geno Star Gun hit",
            "Ice Rock",
            "Arrow Rain",
            "Spear Rain (multi)",
            "Sword Rain",
            "Corona flash",
            "Mega Drain (single)",
            "chomping",
            "Jinxed",
            "monster swing",
            "monster taunt",
            "Smithy Form 1 - light up",
            "Smithy Form 1 - transform",
            "Booster Express train horn"
        };
        #endregion
        #region Battles
        public static int[] BattleLengths = new int[]
        {
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            4,0,2,2,0,2,3,4,2,0,4,3,1,2,0,2,
            4,2,3,3,4,0,0,0,0,0,0,1,4,1,1,1
        };
        public static byte[] BattleOpcodes = new byte[]
        {
            0xF3,0xF3,0x00,0xE0,0xF0,0xEF,0xEC,0xED,
            0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,
            0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,0xFC,
            0xFC,0xFC,0xE8,0xE7,0xE6,0xE6,0xE7,0xE3,
            0xE5,0xF1,0xF4,0xEA,0xF2,0xF2,0xEB,0xEA,
            0xE2,0xEB,0xFD,0xFE
        };
        public static byte[] BattleParams = new byte[]
        {
            0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
            0x0A,0x05,0x01,0x04,0x03,0x02,0x07,0x13,
            0x12,0x11,0x0D,0x0C,0x14,0x08,0x10,0x10,
            0x06,0x09,0x00,0x01,0x01,0x00,0x00,0x00,
            0x00,0x00,0x00,0x01,0x00,0x01,0x01,0x00,
            0x00,0x00,0x00,0x00
        };
        public static string[] BattlefieldNames = new string[]
        {
            "Forest Maze",
            "Forest Maze: Bowyer\'s Pad",
            "Bean Valley: Beanstalks",
            "Sunken Ship: King Calamari\'s Cellar",
            "Sunken Ship",
            "Moleville Mines",
            "___mines",
            "Bowser\'s Keep",
            "Barrel Volcano: Czar Dragon\'s Pad",
            "Grasslands",
            "Mountains",
            "Mushroom Kingdom House",
            "Booster Tower",
            "Mushroom Kingdom Castle",
            "Kero Sewers: Underwater",
            "Mushroom Kingdom Castle",
            "Bowser\'s Keep Turret: Exor",
            "Booster Tower: Balcony",
            "Smithy Factory: Count Down\'s Pad",
            "Smithy Factory",
            "Barrel Volcano",
            "Kero Sewers",
            "Nimbus Castle",
            "Nimbus Castle: Birdo\'s Room",
            "Nimbus Land",
            "Underground",
            "___uses Mushroom Kingdom tiles",
            "___forested area with unique trees",
            "Mushroom Kingdom",
            "Bowser\'s Keep: Chandeliers",
            "Forest Maze: Path to Bowyer",
            "Level Up foreground",
            "Level Up background",
            "Plateaus",
            "___sea enclave",
            "Marrymore Chapel Sanctuary",
            "Star Hill",
            "Seaside Town Beach",
            "Sea",
            "Blade: Axem Rangers",
            "Smithy Factory: Domino & Cloaker\'s Pad",
            "Bean Valley: Grasslands",
            "Belome Temple",
            "Land\'s End Desert",
            "Factory Grounds: Smithy\'s Pad",
            "Smithy\'s Final Form",
            "Jinx\'s Dojo",
            "Culex",
            "Factory Grounds",
            "Bean Valley: Pipe Room",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____",
            "_____"
        };
        public static string[] BattleEventNames = new string[]
        {
            "Mallow belts Croco, gets back frog coin",
            "Kinklink flashes, loses grip and Bowser falls",
            "Belome swallows Mallow",
            "Geno fights Bowyer, Mario and Mallow run to help",
            "Mack jumps out of battle off screen",
            "Mack returns to battle",
            "Belome spits out Mallow",
            "Countdown runs schedule, 1:00,3:00,5:00,6:00,7:00",
            "Countdown runs schedule, 6:00,9:00,10:00,12:00",
            "Punchinello interludes and prepares to summon Bob-ombs",
            "Punchinello interludes and prepares to summon Mezzo Bombs",
            "Punchinello summons King Bomb which then explodes",
            "___dialogue from Booster fight",
            "___",
            "INTRO SCENE: Punchinello fight",
            "Croco steals items: \"You want them back?\"",
            "Croco returns items: \"Enough! Here's your junk...\"",
            "INTRO SCENE: Knife Guy & Grate Guy fight",
            "Knife Guy & Grate Guy pair up piggy-back",
            "Knife Guy & Grate Guy separate: \"Yikes! They're pretty tough\"",
            "Mario and party run off of balcony after Knife Guy & Grate Guy fight",
            "Johnny challenges Mario to a one-on-one",
            "Yaridovich 'Mirage Attack'",
            "Yaridovich mirage is destroyed, return to single form",
            "Machine Made Yaridovich 'Multiplier'",
            "Drill Bit",
            "INTRO SCENE: Tentacles rise from holes",
            "beat Tentacles, move on to next",
            "beat Tentacles, move on to King Calamari",
            "jump down King Calamari's cellar hole",
            "jump up King Calamari's cellar hole",
            "Bundt moves, Assistant pokes Torte",
            "Bundt moves again, both cooks run away",
            "candles appear on Bundt",
            "\"Blow those candles out!\"",
            "Raspberry is beaten, Snifits & Booster run in and eat cake",
            "Tentacles throw character off screen",
            "GAME INTRO: Mario hammers Sky Troopa",
            "GAME INTRO: Mario stomps Goomba",
            "GAME INTRO: Mallow uses Thunderbolt",
            "GAME INTRO: Geno attacks",
            "GAME INTRO: Geno uses Geno Beam",
            "Bb-bombs explode",
            "Toad's battle mechanics tutorial",
            "Czar Dragon dies",
            "Zombone dies",
            "Czar Dragon summons Helios",
            "___",
            "Valentina summons Dodo, Dodo carries off middle character",
            "Dodo flutters and leaves battle",
            "Dodo returns to Valentina's formation",
            "Valentina & Dodo are beaten",
            "INTRO SCENE: Domino & Cloaker's introduction",
            "Domino teams up with Mad Adder",
            "Cloaker teams up with Earthlink",
            "Shy Away waters Smilax: part 1",
            "Shy Away waters Smilax: part 2",
            "Shy Away waters Smilax: part 3",
            "Thrax is there",
            "Belome confronts a character: \"You all LOOK delicious...\"",
            "Belome clones someone",
            "only Mario is there",
            "Axem Rangers intro scene",
            "Axem Pink quits",
            "Axem Black quits",
            "Axem Yellow quits",
            "Axem Green quits",
            "Axem Rangers group formation",
            "Axem Red quits",
            "Axem Rangers are defeated",
            "Jinx uses Jinxed",
            "Jinx uses Triple Kick",
            "Jinx uses Quicksilver",
            "Jinx uses Bombs Away",
            "Culex summons crystals",
            "Formless changes into Mokura",
            "Boomer is defeated, chandelier crashing scene",
            "___screen flashes white",
            "Dodo flutters and exits battle",
            "Magikoopa summons monster",
            "no one there",
            "Exor is defeated, cries and opens mouth",
            "Smithy (1st Form) is beaten, ground shakes etc.",
            "___screen flashes white",
            "___screen flashes white",
            "___Fear Roulette",
            "Smelter pours molten liquid, Smithy welds",
            "Smithy transforms into Tank Head",
            "Smithy transforms into Magic Head",
            "Smithy transforms into Chest Head",
            "Smithy transforms into Box Head",
            "Smithy's head fades before transforming into other head",
            "Shelly breaks, Birdo appears",
            "beam of light forms around Smithy head before body appears",
            "Punchinello's bombs explode if still alive",
            "bombs explode",
            "___nothing",
            "Smithy waits before transforming head",
            "Smithy is defeated",
            "___",
            "Earthlink/Mad Adder collapses and dies",
            "___Magikoopa is there",
            "<NONE>"
        };
        public static string[] targetNames = new string[]
        {
            "Mario",
            "Toadstool",
            "Bowser",
            "Geno",
            "Mallow",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "____",
            "character in slot 1",
            "character in slot 2",
            "character in slot 3",
            "monster 1 (set)",
            "monster 2 (set)",
            "monster 3 (set)",
            "monster 4 (set)",
            "monster 5 (set)",
            "monster 6 (set)",
            "monster 7 (set)",
            "monster 8 (set)",
            "self",
            "all allies, not self",
            "random ally, not self",
            "all allies, and self",
            "random ally, or self",
            "____",
            "____",
            "____",
            "all opponents",
            "at least one opponent",
            "random opponent",
            "____",
            "at least one ally",
            "monster 1 (call)",
            "monster 2 (call)",
            "monster 3 (call)",
            "monster 4 (call)",
            "monster 5 (call)",
            "monster 6 (call)",
            "monster 7 (call)",
            "monster 8 (call)"
        };
        public static string[] TargetNames
        {
            get
            {
                for (int i = 0; i < Model.Characters.Length; i++)
                    targetNames[i] = Model.CharacterNames.GetUnsortedName(i);
                return targetNames;
            }
        }
        private static string[] monsterBehaviors = new string[0];
        public static string[] MonsterBehaviors
        {
            get
            {
                if (monsterBehaviors.Length == 54)
                    return monsterBehaviors;
                monsterBehaviors = new string[54];
                for (int i = 0; i < monsterBehaviors.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                        case 36:
                            monsterBehaviors[i] = "Initialize in Battle"; break;
                        case 1:
                        case 37:
                            monsterBehaviors[i] = "Idle/On Hit"; break;

                        default: monsterBehaviors[i] = ""; break;
                    }
                }
                return monsterBehaviors;
            }

        }
        #endregion

        #region Monsters
        public static string[] MonsterNames = new string[]
        {
            "Terrapin",
            "Spikey",
            "Sky Troopa",
            "Mad Mallet",
            "Shaman",
            "Crook",
            "Goomba",
            "Piranha Plant",
            "Amanita",
            "Goby",
            "Bloober",
            "Bandana Red",
            "Lakitu",
            "Birdy",
            "Pinwheel",
            "Rat Funk",
            "K-9",
            "Magmite",
            "The Big Boo",
            "Dry Bones",
            "Greaper",
            "Sparky",
            "Chomp",
            "Pandorite",
            "Shy Ranger",
            "Bob-Omb",
            "Spookum",
            "Hammer Bro",
            "Buzzer",
            "Ameboid",
            "Gecko",
            "Wiggler",
            "Crusty",
            "Magikoopa",
            "Leuko",
            "Jawful",
            "Enigma",
            "Blaster",
            "Guerrilla",
            "Baba Yaga",
            "Hobgoblin",
            "Reacher",
            "Shogun",
            "Orb User",
            "Heavy Troopa",
            "Shadow",
            "Cluster",
            "Bahamutt",
            "Octolot",
            "Frogog",
            "Clerk",
            "Gunyolk",
            "Boomer",
            "Remo Con",
            "Snapdragon",
            "Stumpet",
            "Dodo (2nd time)",
            "Jester",
            "Artichoker",
            "Arachne",
            "Carroboscis",
            "Hippopo",
            "Mastadoom",
            "Corkpedite",
            "Terra Cotta",
            "Spikester",
            "Malakoopa",
            "Pounder",
            "Poundette",
            "Sackit",
            "Gu Goomba",
            "Chewy",
            "Fireball",
            "Mr.Kipper",
            "Factory Chief",
            "Bandana Blue",
            "Manager",
            "Bluebird",
            "__nothing",
            "Alley Rat",
            "Chow",
            "Magmus",
            "Li{xx}L Boo",
            "Vomer",
            "Glum Reaper",
            "Pyrosphere",
            "Chomp Chomp",
            "Hidon",
            "Sling Shy",
            "Rob-Omb",
            "Shy Guy",
            "Ninja",
            "Stinger",
            "Goombette",
            "Geckit",
            "Jabit",
            "Star Cruster",
            "Merlin",
            "Muckle",
            "Forkies",
            "Gorgon",
            "Big Bertha",
            "Chained Kong",
            "Fautso",
            "Straw Head",
            "Juju",
            "Armored Ant",
            "Orbison",
            "Tub-O-Troopa",
            "Doppel",
            "Pulsar",
            "__purple Bahamutt",
            "Octovader",
            "Ribbite",
            "Director",
            "__Gunyolk (yellow)",
            "__Boomer (blue)",
            "Puppox",
            "Fink Flower",
            "Lumbler",
            "Springer",
            "Harlequin",
            "Kriffid",
            "Spinthra",
            "Radish",
            "Crippo",
            "Mastablasta",
            "Pile Driver",
            "Apprentice",
            "__nothing",
            "__nothing",
            "__nothing",
            "__Geno redemption",
            "__little bird",
            "Box Boy",
            "Shelly",
            "Super Spike",
            "Dodo",
            "Oerlikon",
            "Chester",
            "Body",
            "__Pile Driver (body)",
            "Torte",
            "Shy Away",
            "Jinx Clone",
            "Machine Made (Shyster)",
            "Machine Made (Drill Bit)",
            "Formless",
            "Mokura",
            "Fire Crystal",
            "Water Crystal",
            "Earth Crystal",
            "Wind Crystal",
            "Mario Clone",
            "Toadstool 2",
            "Bowser Clone",
            "Geno Clone",
            "Mallow Clone",
            "Shyster",
            "Kinklink",
            "__Toadstool (captive)",
            "Hangin{xx} Shy",
            "Smelter",
            "Machine Made (Mack)",
            "Machine Made (Bowyer)",
            "Machine Made (Yaridovich)",
            "Machine Made (Axem Pink)",
            "Machine Made (Axem Black)",
            "Machine Made (Axem Red)",
            "Machine Made (Axem Yellow)",
            "Machine Made (Axem Green)",
            "Goomba (Intro)",
            "Hammer Bro (Intro)",
            "Birdo (Intro)",
            "Bb-Bomb",
            "Magidragon",
            "Starslap",
            "Mukumuku",
            "Zeostar",
            "Jagger",
            "Chompweed",
            "Smithy (Tank Head)",
            "Smithy (Box Head)",
            "__Corkpedite",
            "Microbomb",
            "__Thwomp",
            "Grit",
            "Neosquid",
            "Yaridovich (mirage)",
            "Helio",
            "Right Eye",
            "Left Eye",
            "Knife Guy",
            "Grate Guy",
            "Bundt",
            "Jinx (1st time)",
            "Jinx (2nd time)",
            "Count Down",
            "Ding-A-Ling",
            "Belome (1st time)",
            "Belome (2nd time)",
            "__Belome",
            "Smilax",
            "Thrax        ",
            "Megasmilax",
            "Birdo",
            "Eggbert",
            "Axem Yellow",
            "Punchinello",
            "Tentacles (right)",
            "Axem Red",
            "Axem Green",
            "King Bomb",
            "Mezzo Bomb",
            "__Bundt object",
            "Raspberry",
            "King Calamari",
            "Tentacles (left)",
            "Jinx (3rd time)",
            "Zombone",
            "Czar Dragon",
            "Cloaker (1st time)",
            "Domino (2nd time)",
            "Mad Adder",
            "Mack",
            "Bodyguard",
            "Yaridovich",
            "Drill Bit",
            "Axem Pink",
            "Axem Black",
            "Bowyer",
            "Aero",
            "__Exor (mouth)",
            "Exor",
            "Smithy (1st Form)",
            "Shyper",
            "Smithy (Body)",
            "Smithy (Head)",
            "Smithy (Magic Head)",
            "Smithy (Chest Head)",
            "Croco (1st time)",
            "Croco (2nd time)",
            "__Croco",
            "Earth Link",
            "Bowser",
            "Axem Rangers",
            "Booster",
            "Booster",
            "Snifit",
            "Johnny",
            "Johnny",
            "Valentina",
            "Cloaker (2nd time)",
            "Domino (2nd time)",
            "Candle",
            "Culex",
        };
        #endregion
        #region Sprites
        public static string[] SpriteNames = new string[]
        {
            "Mario (walking, down-left)",
            "Mario (jump, front)",
            "Mario (walking, up-right)",
            "Mario (surprise, left)",
            "Mario (attack, up-right)",
            "Mario (hammer attack, up-right)",
            "Mario (crouch, up-right)",
            "Toadstool (walking, down-left)",
            "Toadstool (walking, up-right)",
            "Toadstool (surprise)",
            "Toadstool (slap attack)",
            "Toadstool (frying pan attack)",
            "Toadstool (fallen/crying)",
            "Bowser (walking, down-left)",
            "Bowser (walking, up-right)",
            "Bowser (surprise)",
            "Bowser (claw attack)",
            "Bowser (swing ball-chain)",
            "Bowser (cast spell)",
            "Mallow (walking, down-left)",
            "Mallow (walking, up-right)",
            "Mallow (surprise)",
            "Mallow (punch)",
            "Mallow (swing stick)",
            "Mallow (still, up-right)",
            "Geno (walking, down-left)",
            "Geno (walking, up-right)",
            "Geno (surprise)",
            "Geno (elbow shot)",
            "Geno (finger shot)",
            "Geno (morph into cannon)",
            "Hammer",
            "Froggie Stick",
            "Cymbals",
            "Chomp",
            "Frying Pan",
            "Parasol",
            "War Fan",
            "Red Mushroom",
            "Red Scarecrow",
            "Mario's battle portrait",
            "Toadstool's battle portrait",
            "Bowser's battle portrait",
            "Mallow's battle portrait",
            "Geno's battle portrait",
            "Yellow Yoshi",
            "Pink Yoshi",
            "Boshi",
            "Croco",
            "Green Yoshi",
            "Booster",
            "Green Yoshi (walk)",
            "Green Yoshi (laying egg)",
            "King Nimbus",
            "Queen Nimbus",
            "Jonathan Jones",
            "Valentina",
            "Magikoopa",
            "Frogfucius",
            "Tadpole",
            "Thwomp",
            "Big Thwomp",
            "Microbomb",
            "Valentina Statue",
            "Toad",
            "Wallet Guy (also casino assistants)",
            "Raini",
            "Old Man",
            "Old Woman",
            "Green/Brown Toad",
            "Chancellor",
            "Pa Mole",
            "Ma Mole",
            "Girl Mole (pink bow)",
            "Girl Mole (yellow bow)",
            "Nimbusite (blue)",
            "Nimbusite (red)",
            "Nimbusite (brown/green)",
            "Nimbusite (yellow/green)",
            "Nimbus Guard",
            "Toadofsky",
            "Mario Doll (Booster's Castle)",
            "Blue Star Piece",
            "Purple Star Piece",
            "Red Star Piece",
            "Gold Star Piece",
            "Green Star Piece",
            "Light Blue Star Piece",
            "Yellow Star Piece",
            "Geno Doll",
            "Bowser Doll",
            "Mario Doll",
            "Toadstool Doll",
            "Blue Stepping Block",
            "Treasure Chest",
            "Empty Treasure Chest",
            "Mario Doll (surprised)",
            "Snifit's Parachute",
            "Rolling Barrel",
            "Trampoline (Warp)",
            "Trampoline (Jump)",
            "Teeter-totter",
            "Save Point",
            "Corkpedite",
            "J Puzzle Block",
            "Yellow Stepping Block",
            "Whirlpool (water)",
            "Hinopio",
            "Factory Hex-Nut",
            "Green Switch",
            "Treasure Chest (bad palette)",
            "Nimbusland Bus Driver",
            "Mushroom Boy",
            "Marrymore Man (green)",
            "Marrymore Woman (yellow)",
            "Marrymore Woman (green)",
            "Marrymore Kid (purple)",
            "Marrymore Kid (blue/green)",
            "Marrymore Bright Card buyer (brown/grey)",
            "Rose Town Gardener (green/grey)",
            "Old Woman (green/grey)",
            "Old Woman (purple/grey)",
            "Fat Yoshi Baby",
            "Yoshi Baby Egg",
            "Gameboy Kid",
            "Frogfucius Student",
            "Chomp (behind)",
            "Wiggler (head)",
            "Block Shadow",
            "Red Magikoopa",
            "Wiggler (body segment)",
            "Dodo (as parson)",
            "Moleville Mine Cart",
            "Knife Guy Juggler (still, red balls)",
            "Knife Guy Juggler",
            "Mine Cart (Seq7 Overworld Sprite)",
            "Mario in Mine Cart",
            "Fireball (surface from lava)",
            "Piranha Plant",
            "Goomba",
            "Bullet Bill",
            "Golden Bullet Bill",
            "Factory Clerk (green)",
            "Land's End Cannon",
            "Apple (Yo'ster Isle intro sequence)",
            "Bob-omb",
            "Commander Troopa",
            "Golden Belome",
            "Birdy Statue",
            "Shyguy in Bowser's Helicopter",
            "Machine Made Bowyer",
            "Machine Made Yaridovich (out of battle)",
            "Machine Made Axem Red",
            "Gunyolk (top section)",
            "Gunyolk (outer section)",
            "Factory Crane",
            "Blue-Green Star Piece (spinning)",
            "Smithy's Hammer",
            "Smithy's Chest",
            "Poison Toxic Gas",
            "Shelly (bottom section)",
            "Dyna and Mite",
            "Seaside Town Fake (green)",
            "Seaside Town Fake Elder (green)",
            "Seaside Town Elder (yellow/green)",
            "Monstermama (golden/brown/red)",
            "Nimbus Guard",
            "Factory Manager (blue)",
            "Factory Director (red)",
            "Boomer (red)",
            "Dr.Topper (green)",
            "Sparkles from Star Piece",
            "Geno Doll",
            "Smelter (back section)",
            "Mario on Nimbus Busman (Bowser's Keep cutscene)",
            "Golden Chomp (back)",
            "Chomp (front)",
            "Grate Guy (from casino)",
            "Marrymore Inn Keeper (blue, striped hat)",
            "Rose Town Treasure Holder",
            "Rose Town Woman (blue/pink, braids)",
            "Marrymore Woman (yellow)",
            "Rose Town Old Man (blue/grey)",
            "Old Woman (grey/red)",
            "Kid (red, striped hat)",
            "Gaz (purple)",
            "(nothing)",
            "(nothing)",
            "Cannon Ball",
            "Croco (still)",
            "Bowser w/Toadstool in Helicopter",
            "Miniature Toad",
            "Coin",
            "Small Coin",
            "Frog Coin",
            "Flower",
            "Big Flower",
            "Sparkle (sideways)",
            "Sparkle (downwards)",
            "Sparkle (circular winding)",
            "Explosion",
            "Mokura's Cloud (blue)",
            "Small Frog Coin",
            "Level Up text from Invincible Star",
            "Grey Explosion (when encountering Fireballs)",
            "Miniature Axem Red",
            "Terrapin (still)",
            "Jinx (walk)",
            "Axem Red",
            "Axem Black",
            "Axem Pink",
            "Axem Yellow",
            "Axem Green",
            "Axem Red teleport",
            "Stumpet (head)",
            "Stumpet (roots, right)",
            "Czar Dragon (body)",
            "Growing Vine Beanstalk",
            "Brick Beanstalk Block",
            "Whirlpool (desert)",
            "Yellow Letter",
            "Yaridovich (out of battle)",
            "Toadstool Marrymore Accessories",
            "Tentacle (extending)",
            "Snifit (black, back)",
            "Level Up Bonus Selection Box",
            "Booster's Tower Entrance Door",
            "Light Green Pipe (top edge)",
            "Level Up Bonus Text",
            "Level Up Bonus Flower",
            "Level Up Bonus Pow Power",
            "Level Up Bonus Star Magic",
            "Level Up Bonus HP",
            "Falling Stepping Bridge Block",
            "Old Classic Mario",
            "Booster's Choo-Choo Train",
            "Magikoopa (blue, walking)",
            "Terrapin (walking)",
            "Splash Water Droplets",
            "Small Sea Fish",
            "Splash Water Geyser",
            "Bowyer",
            "White Gas Cloud",
            "Machine Made Drill Bit",
            "Mushroom House Decor Mailbox",
            "Link Sleeping in Rose Town Inn",
            "Samus Sleeping in Mushroom Kingdom",
            "Grey Stepping Stone",
            "Hinopio's Model Airplane (blue/grey)",
            "Grey Stone Block",
            "Small Black Fence",
            "Wooden Bridge Bowser's Keep (right section)",
            "Grey Stone Bridge Bowser's Keep (right section)",
            "Toadstool Hand Captive from Rope",
            "Plywood Brown Door Bowser's Keep",
            "Beetle",
            "Terrapin",
            "Spikey",
            "Sky Troopa",
            "Mad Mallet",
            "Shaman",
            "Crook",
            "Goomba",
            "Piranha Plant",
            "Amanita",
            "Goby",
            "Bloober",
            "Bandana Red",
            "Lakitu",
            "Birdy",
            "Pinwheel",
            "Rat Funk",
            "K-9",
            "Magmite",
            "The Big Boo",
            "Dry Bones",
            "Greaper",
            "Sparky",
            "Chomp",
            "Pandorite",
            "Shy Ranger",
            "Bob-Omb",
            "Spookum",
            "Hammer Bro",
            "Buzzer",
            "Ameboid",
            "Gecko",
            "Wiggler",
            "Crusty",
            "Magikoopa",
            "Leuko",
            "Jawful",
            "Enigma",
            "Blaster",
            "Guerrilla",
            "Baba Yaga",
            "Hobgoblin",
            "Reacher",
            "Shogun",
            "Orb User",
            "Heavy Troopa",
            "Shadow",
            "Cluster",
            "Bahamutt",
            "Octolot",
            "Frogog",
            "Clerk",
            "Gunyolk",
            "Boomer",
            "Remo Con",
            "Snapdragon",
            "Stumpet",
            "Dodo (2nd time)",
            "Jester",
            "Artichoker",
            "Arachne",
            "Carroboscis",
            "Hippopo",
            "Mastadoom",
            "Corkpedite",
            "Terra Cotta",
            "Spikester",
            "Malakoopa",
            "Pounder",
            "Poundette",
            "Sackit",
            "Gu Goomba",
            "Chewy",
            "Fireball",
            "Mr.Kipper",
            "Factory Chief",
            "Bandana Blue",
            "Manager",
            "Bluebird",
            "__nothing",
            "Alley Rat",
            "Chow",
            "Magmus",
            "Li{xx}L Boo",
            "Vomer",
            "Glum Reaper",
            "Pyrosphere",
            "Chomp Chomp",
            "Hidon",
            "Sling Shy",
            "Rob-Omb",
            "Shy Guy",
            "Ninja",
            "Stinger",
            "Goombette",
            "Geckit",
            "Jabit",
            "Star Cruster",
            "Merlin",
            "Muckle",
            "Forkies",
            "Gorgon",
            "Big Bertha",
            "Chained Kong",
            "Fautso",
            "Straw Head",
            "Juju",
            "Armored Ant",
            "Orbison",
            "Tub-O-Troopa",
            "Doppel",
            "Pulsar",
            "__purple Bahamutt",
            "Octovader",
            "Ribbite",
            "Director",
            "__Gunyolk (yellow)",
            "__Boomer (blue)",
            "Puppox",
            "Fink Flower",
            "Lumbler",
            "Springer",
            "Harlequin",
            "Kriffid",
            "Spinthra",
            "Radish",
            "Crippo",
            "Mastablasta",
            "Pile Driver",
            "Apprentice",
            "__nothing",
            "__nothing",
            "__nothing",
            "__Geno redemption",
            "__little bird",
            "Box Boy",
            "Shelly",
            "Super Spike",
            "Dodo",
            "Oerlikon",
            "Chester",
            "Body",
            "__Pile Driver (body)",
            "Torte",
            "Shy Away",
            "Jinx Clone",
            "Machine Made (Shyster)",
            "Machine Made (Drill Bit)",
            "Formless",
            "Mokura",
            "Fire Crystal",
            "Water Crystal",
            "Earth Crystal",
            "Wind Crystal",
            "Mario Clone",
            "Toadstool 2",
            "Bowser Clone",
            "Geno Clone",
            "Mallow Clone",
            "Shyster",
            "Kinklink",
            "__Toadstool (captive)",
            "Hangin{xx} Shy",
            "Smelter",
            "Machine Made (Mack)",
            "Machine Made (Bowyer)",
            "Machine Made (Yaridovich)",
            "Machine Made (Axem Pink)",
            "Machine Made (Axem Black)",
            "Machine Made (Axem Red)",
            "Machine Made (Axem Yellow)",
            "Machine Made (Axem Green)",
            "Goomba (Intro)",
            "Hammer Bro (Intro)",
            "Birdo (Intro)",
            "Bb-Bomb",
            "Magidragon",
            "Starslap",
            "Mukumuku",
            "Zeostar",
            "Jagger",
            "Chompweed",
            "Smithy (Tank Head)",
            "Smithy (Box Head)",
            "__Corkpedite",
            "Microbomb",
            "__Thwomp",
            "Grit",
            "Neosquid",
            "Yaridovich (mirage)",
            "Helio",
            "Right Eye",
            "Left Eye",
            "Knife Guy",
            "Grate Guy",
            "Bundt",
            "Jinx (1st time)",
            "Jinx (2nd time)",
            "Count Down",
            "Ding-A-Ling",
            "Belome (1st time)",
            "Belome (2nd time)",
            "__Belome",
            "Smilax",
            "Thrax        ",
            "Megasmilax",
            "Birdo",
            "Eggbert",
            "Axem Yellow",
            "Punchinello",
            "Tentacles (right)",
            "Axem Red",
            "Axem Green",
            "King Bomb",
            "Mezzo Bomb",
            "__Bundt object",
            "Raspberry",
            "King Calamari",
            "Tentacles (left)",
            "Jinx (3rd time)",
            "Zombone",
            "Czar Dragon",
            "Cloaker (1st time)",
            "Domino (2nd time)",
            "Mad Adder",
            "Mack",
            "Bodyguard",
            "Yaridovich",
            "Drill Bit",
            "Axem Pink",
            "Axem Black",
            "Bowyer",
            "Aero",
            "__Exor (mouth)",
            "Exor",
            "Smithy (1st Form)",
            "Shyper",
            "Smithy (Body)",
            "Smithy (Head)",
            "Smithy (Magic Head)",
            "Smithy (Chest Head)",
            "Croco (1st time)",
            "Croco (2nd time)",
            "__Croco",
            "Earth Link",
            "Bowser",
            "Axem Rangers",
            "Booster",
            "Booster",
            "Snifit",
            "Johnny",
            "Johnny",
            "Valentina",
            "Cloaker (2nd time)",
            "Domino (2nd time)",
            "Candle",
            "Culex",
            "ABXY action button selection in battle",
            "Rainbow Explosion",
            "Blue Explosion",
            "Green Explosion",
            "Enemy Defeated Explosion Stars",
            "Bomb Explosion",
            "Small White Cloud",
            "Drain Explosion",
            "flower bonus alphabet + symbols",
            "Battle stars (on hit)",
            "Come Back rainbow star",
            "yellow cure stars",
            "....",
            "Bowyer's arrow",
            "Geno's Star Form (Ending Credits)",
            "Geno's Bullets and Star Gun",
            "very small black dot",
            "HP Rain cloud",
            "stat-boost arrows",
            "black rolling coal rock",
            "blue spark",
            "yellow spark",
            "green spark",
            "red spark",
            "rainbow rain",
            "mushroom spores",
            "Lazy shell, Heavy Troopa, nok-nok shell",
            "Orange Lazy Shell",
            "Green Lazy Shell (Tub-O-Troopa)",
            "Snowy eyes",
            "blinking yellow light circle",
            "purple petal",
            "small pink petal",
            "thrown hammer",
            "Bombs Away electric ball",
            "Fire Orb fireball",
            "Willy Wisp purple electric ball",
            "spore (pink/green)",
            "bolt (hardware-wise)",
            "Mute balloon",
            "'Thank You' red dialogue bubble",
            "'Thank You' purple dialogue bubble",
            "'Thank You' blue dialogue bubble",
            "'Thank You' green dialogue bubble",
            "'Thank You' yellow dialogue bubble",
            "'Psychopath' question mark cloud",
            "thrown shuriken",
            "green cure stars",
            "red cure stars",
            "blue cure stars",
            "yellow reusable item sprite with letter I",
            "ABXY buttons from Bowyer's Button Lock",
            "Bowser's spike shot",
            "Geno Flash squinting eyes",
            "green item collection",
            "red item collection",
            "blue item collection",
            "yellow item collection",
            "green spore",
            "'Fear' exclamation point",
            "....",
            "Mokura",
            "Drain",
            "sparkles",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "yellow lightning ball",
            "Fire Orb hit explosion",
            "egg",
            "Lightning Orb blue lightning ball",
            "small yellow spike",
            "large yellow spike",
            "white gas cloud",
            "Blast orange gas cloud",
            "Star Egg little brown bird",
            "Poison Gas green gas cloud",
            "white stars",
            "purple gas cloud",
            "yellow star",
            "Diamond Saw snowflake",
            "blue gas cloud",
            "bone throw",
            "spritz bomb",
            "Wind Crystal and Fire Crystal",
            "green shine web",
            "Mecha-Koopa (Bowser Crush) eyes",
            "Water Crystal and Earth Crystal",
            "plasm water droplet (blue-green)",
            "Ice Rock",
            "black rock",
            "big pink heart",
            "dark red/yellow fireball",
            "light green stars",
            "light orange stars",
            "Sleepy Time sheep/ram",
            "Geno Beam/Blast/Flash red power-up star",
            "....",
            "blue/green bubbles/circles",
            "sleep ZZZ's",
            "backwards yellow spike",
            "Water Blast water spouts",
            "Gunk Ball / Ink Blast",
            "water spout (red)",
            "Royal Flush card",
            "yellow shaking bell",
            "....",
            "blue music note",
            "white pixel dot",
            "....",
            "blue water surfacing/diving droplets",
            "green water surfacing/diving droplets",
            "yellow water surfacing/diving droplets",
            "....",
            "....",
            "....",
            "....",
            "....",
            "Magikoopa's triangle/circle/X cast magic",
            "....",
            "....",
            "....",
            "....",
            "....",
            "flower bonus",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "marching Luigi",
            "marching Toads",
            "conducting Toadofsky",
            "waving Mallow",
            "waving King & Queen Nimbus",
            "Nimbus Busman, Lakitu & Frogfucius",
            "Tadpole",
            "trumpeting Piranhas",
            "Mole miners & star",
            "Dyna & Mite",
            "Hammer Bros & Chomps",
            "Crook & Croco",
            "Bowser in helicopter chasing",
            "Dodo carrying Valentina",
            "red balloon",
            "Booster riding train",
            "Snifits chasing beetle",
            "bouncing Shysters",
            "Mack, Yaridovich, Bowyer",
            "Smithy",
            "Johnny & mates",
            "blue/red/green Toads",
            "riding Yoshi",
            "waving Mario & Toadstool",
            "sparkle",
            "poof",
            "purple firework",
            "smaller red firework",
            "Geno in Star Form (Ending Credits)",
            "The End background (Ending Credits)",
            "Geno's Star's glow effect (Ending Credits)",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "....",
            "...."
        };
        public static string[] NPCPackets = new string[]
        {
            "flower",
            "mushroom",
            "key",
            "super star",
            "monster face",
            "item bag",
            "___sparkles",
            "___sparkles",
            "___bomb explosion",
            "___blue cloud",
            "___small frog coin",
            "___level-up text",
            "___grey explosion",
            "___Axem Red (mini)",
            "___NULL",
            "___NULL",
            "big coin",
            "small coin (not moving)",
            "small coin",
            "frog coin",
            "water splash drops",
            "bullet bill ignition",
            "sparkles (move N)",
            "sparkles (move W)",
            "bomb explosion (+SFX)",
            "blue cloud (+SFX)",
            "Magikoopa (+SFX)",
            "Terrapin",
            "mushroom (thrown SW)",
            "sparkle line (looped)",
            "water splash drops (+SFX)",
            "level-up text",
            "blue cloud",
            "bomb explosion",
            "grey explosion (+SFX)",
            "flower (jumps)",
            "mushroom (jumps)",
            "item bag (jumps)",
            "mushroom (jumps)",
            "sparkle",
            "Axem Red",
            "Axem Black",
            "Axem Pink",
            "Axem Yellow",
            "Axem Green",
            "Axem Red teleport (+SFX)",
            "Axem Red (mini)",
            "blue fire trail (follows object)",
            "star piece (mini)",
            "hammer sparks (+SFX)",
            "water blast (+SFX)",
            "Drill Bit",
            "bomb explosion (faster)",
            "Frog Coin (random walking)",
            "___level-up bonus POW",
            "___level-up bonus S",
            "___level-up bonus HP",
            "___donut lift",
            "___8-bit Mario",
            "___Booster's train",
            "___Magikoopa",
            "___Terrapin",
            "___water splash drops",
            "___river fish",
            "big coin (rolls E)",
            "big coin (falls SE)",
            "big coin (rolls S)",
            "big coin (rolls SW)",
            "big coin (rolls W)",
            "big coin (falls W)",
            "big coin (falls S)",
            "big coin (falls NE)",
            "big coin (falls NE)",
            "___grey brick",
            "___bridge rails",
            "___wooden bridge",
            "___stone bridge",
            "___hanging Toadstool",
            "___plywood door",
            "___beetle"
        };
        #endregion
        #region Effects
        public static string[] EffectNames = new string[]
        {
            "___DUMMY",
            "___DUMMY",
            "Thundershock",
            "Thundershock (BG mask)",
            "Crusher",
            "Meteor Blast",
            "Bolt",
            "Star Rain",
            "Flame (fire engulf)",
            "Mute (balloon)",
            "Flame Stone",
            "Bowser Crush",
            "spell cast spade",
            "spell cast heart",
            "spell cast club",
            "spell cast diamond",
            "spell cast star",
            "Terrorize",
            "Snowy (snow BG, 4bpp)",
            "Snowy (snow FG, 2bpp)",
            "Endobubble (black ball/orb)",
            "___DUMMY",
            "Solidify",
            "___DUMMY",
            "___DUMMY",
            "Psych Bomb (BG)",
            "___DUMMY",
            "Dark Star",
            "Willy Wisp (blue orb/ball BG)",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "Geno Whirl",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "blank white flash (2bpp)",
            "blank white flash (4bpp)",
            "Boulder",
            "black ball/orb",
            "blank blue flash (2bpp)",
            "blank red flash (2bpp)",
            "blank blue flash (4bpp)",
            "blank red flash (4bpp)",
            "___DUMMY",
            "blank dark blue flash (2bpp)",
            "blank dark blue flash (4bpp)",
            "Meteor Shower (snow/confetti)",
            "purple/violet flash (4bpp)",
            "brown flash (4bpp)",
            "dark red blast",
            "dark blue blast",
            "snow/confetti, green",
            "light blue blast",
            "black ball/orb",
            "red ball/orb",
            "green ball/orb",
            "snow/confetti, slate green",
            "snow/confetti, red",
            "orange/red blast (Fire Bomb)",
            "Ice bomb/Solidify BG (blue freeze)",
            "Static E! (electric blast)",
            "green star bunches",
            "blue star bunches",
            "pink star bunches",
            "yellow star bunches",
            "Aurora Flash",
            "Storm",
            "Electroshock",
            "Smithy Treasure Head spell, red",
            "Smithy Treasure Head spell, green",
            "Smithy Treasure Head spell, blue",
            "Smithy Treasure Head spell, yellow",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "Flame Wall (orange/red fire)",
            "Petal Blast 1",
            "Petal Blast 2",
            "Drain Beam BG (4bpp)",
            "Drain Beam FG (2bpp)",
            "___DUMMY",
            "electric bolt",
            "Sand Storm BG (2bpp)",
            "___DUMMY",
            "Pollen Nap (yellow pollen)",
            "Geno Beam, blue",
            "Geno Beam, red",
            "Geno Beam, gold",
            "Geno Beam, yellow",
            "Geno Beam, green",
            "Thunderbolt",
            "Light Beam",
            "Meteor Shower",
            "S\'Crow Dust (purple pollen)",
            "HP Rain BG",
            "HP Rain FG",
            "wavy dark blue lines",
            "wavy blue lines",
            "wavy red lines",
            "wavy brown lines",
            "Sand Storm FG (4bpp)",
            "Sledge",
            "Arrow Rain",
            "Spear Rain",
            "Sword Rain",
            "Lightning Orb (BG waves)",
            "Echofinder",
            "Poison Gas FG 1",
            "Poison Gas FG 2",
            "Poison Gas BG",
            "Smithy Transforms (beam effect)",
            "Smelter\'s molten metal",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY",
            "___DUMMY"
        };
        #endregion
        #region Levels
        public static string[] LevelNames = new string[]
        {
            "Debug Room",
            "____blue BG, nothing there",
            "Bowser's Keep, outside (Mario enters at beginning of game)",
            "Bowser's Keep 1st time, Area 01",
            "Bowser's Keep 1st time, Area 02",
            "Marrymore, outside (during Booster)",
            "Marrymore Inn, 2F",
            "Marrymore Inn, 1F",
            "Bowser's Keep, Area 09 (tall room, w/o save point this time)",
            "Marrymore Inn, regular room",
            "Bowser's Keep 1st time, Area 04 (Throne Room)",
            "Marrymore Inn, 3F",
            "Marrymore Inn, Suite room",
            "Barrel Volcano, falling into Volcano",
            "Booster Hill",
            "Vista Hill",
            "Mario's Pad",
            "Mushroom Kingdom Castle, Main Hall",
            "Mushroom Kingdom Castle, Throne Room",
            "Mushroom Kingdom Castle, Stair Room to Toadstool's room",
            "Mushroom Kingdom Castle, Toadstool's room",
            "Mushroom Kingdom Castle, branch room to Vault/Guest Room",
            "Mushroom Kingdom Castle, Guest room",
            "Mushroom Kingdom, before Croco, Outside",
            "Sunken Ship, post-KC Area 15 (Bandana Red room w/long stairwell)",
            "Sunken Ship, post-KC Area 16 (Entrance to Johnny's room)",
            "Sunken Ship, post-KC Area 12 (underwater room w/stairwell and Zeostars)",
            "Sunken Ship, post-KC Area 13 (large underwater room with a Bloober)",
            "Sunken Ship, post-KC Area 17 (Johnny's room)",
            "Mushroom Kingdom Castle, Throne Room (Toadstool returns)",
            "Mushroom Kingdom Castle, Toadstool's room (Toadstool returns)",
            "Mushroom Kingdom Castle, Vault",
            "Mushroom Kingdom Castle, Entrance to Toadstool's room",
            "Yo'ster Isle, entrance from Pipe Vault",
            "Yo'ster Isle",
            "Booster Tower, 7F (3-level w/parachuting Spookums)",
            "Booster Tower, 6F Area 04 (3-level w/Thwomp on teeter-totter)",
            "Booster Tower, 4F (3-level room w/jumping Spookums)",
            "Booster Tower, 9F (Booster's bomb-throwing room w/rail tracks)",
            "Booster Tower, 5F (Knife Guy's juggling room)",
            "Booster Tower, 5F (Knife Guy's juggline room, after defeat)",
            "Booster Tower, 8F Area 01 (‘minesweeper' room w/coins and hidden Fireballs)",
            "Booster Tower, 3F Area 02 (NES Mario room)",
            "Booster Tower, 1F Area 01 (main room)",
            "Mushroom Kingdom, before Croco, jumping kid's house (1F)",
            "Mushroom Kingdom, before Croco, jumping kid's house (2F)",
            "Mushroom Kingdom, before Croco, Raz and Raini's house",
            "Mushroom Kingdom, before Croco, Item Shop (top floor)",
            "Booster Tower, 8F Area 02 (Zoom Shoes room)",
            "Mushroom Kingdom, before Croco, Inn (1F)",
            "____blue BG, nothing there",
            "Mushroom Kingdom, before Croco, running kid's house",
            "Mushroom Kingdom, Inn (2F)",
            "Mushroom Kingdom, before Croco, Item Shop (basement)",
            "Booster Hill ____dummy",
            "Pipe Vault Entrance",
            "Kero Sewers, Area 02 (long room w/three pipes)",
            "Kero Sewers, Area 03 (large water room w/pipe in center)",
            "Kero Sewers, Area 06 (long water room w/Rat Funks in a line)",
            "Kero Sewers, Area 05 (super star room w/four Rat Funks)",
            "Kero Sewers, Area 04 (large room w/Pandorite and hiding Rat Funks)",
            "Nimbus Land, outside (during Valentina right before fight)",
            "Kero Sewers, Area 01 (water room w/save)",
            "Marrymore Scene",
            "Marrymore, outside",
            "Marrymore Chapel, sanctuary",
            "Rose Way, exit Area where Bowser's Troops gathered",
            "Midas River, business transaction Area",
            "Midas River, barrel jumping river",
            "Midas River, waterfall",
            "Midas River, 1st tunnel",
            "Midas River, 2nd tunnel (both left and right)",
            "Midas River, 3rd tunnel (on left)",
            "Midas River, 4th tunnel (on very bottom right)",
            "Tadpole Pond, Area 02",
            "Tadpole Pond, Area 01",
            "Bandit's Way, Area 01",
            "Bandit's Way, Area 03",
            "Bandit's Way, Area 04",
            "Rose Way, main Area",
            "Rose Way, two fast-floating platforms",
            "Rose Way, treasure chests w/coins Area",
            "Rose Way, winding path w/Crooks",
            "Rose Town, during Bowyer outside",
            "Rose Town, outside",
            "Rose Town, during Bowyer Inn (1F)",
            "Rose Town, Inn (1F)",
            "Rose Town, Item Shop",
            "Smithy's Final Form Defeat: Geno's Redemption",
            "Rose Town, during Bowyer three grandkids' house",
            "Rose Town, three grandkids' house",
            "Rose Town, couple's house",
            "Grate Guy's Casino, inside casino",
            "Rose Town, during Bowyer treasure house (1F)",
            "Rose Town, treasure house (1F)",
            "Rose Town, during Bowyer Inn (2F)",
            "Rose Town, Inn (2F)",
            "Rose Town, during Bowyer treasure house (2F)",
            "Rose Town, treasure house (2F)",
            "Rose Town, Geno Awakens in Inn (1F)",
            "Booster Pass, Area 01",
            "Booster Pass, Area 02",
            "Moleville, Outside (at exit from Mines)",
            "Smithy Factory, Area 17 (Domino and Cloaker's room)",
            "Grate Guy's Casino, front door",
            "Moleville, Dyna and Mite's House ____dummy",
            "Grate Guy's Casino, outside",
            "Nimbus Castle, Area 09 (Statue Room, after Valentina)",
            "Moleville, Outside",
            "Nimbus Castle, Area 01 (entrance hall)",
            "Nimbus Castle, Area 18 (Dodo's statue-polishing room)",
            "Nimbus Castle, Area 04 (left of 4-way path, right-angle red brick path w/ treasure)",
            "Nimbus Castle, Area 17 (right of 4-way path, Save Point)",
            "Nimbus Castle, Area 16 (small two-door room w/treasure, from Area 15)",
            "Nimbus Castle, Area 10 (red brick 2-level room w/treasure from Birdo's room)",
            "Nimbus Castle, Area 03 (4-way path, during Valentina)",
            "Nimbus Castle, Area 02 (left of Area 01)",
            "Nimbus Castle, Area 15 (front of 4-way path, large right-angle room w/ plant)",
            "Nimbus Castle, Area 05 (long 5-exit room, during Valentina)",
            "Nimbus Castle, Area 06 (left-most front door from Area 05)",
            "Nimbus Castle, Area 13 (Throne Room, during Valentina)",
            "Nimbus Castle, path after Throne Room (2nd)",
            "Nimbus Castle, Area 12 (entrance to throne room)",
            "Pipe Vault, Area 01",
            "Pipe Vault, Area 03 (line of pipes)",
            "Pipe Vault, Area 04 (line of coins, 2 hidden treasures)",
            "Pipe Vault, Area 06 (line of red pipes)",
            "Pipe Vault, Area 02",
            "Pipe Vault, Area 07 (long path w/moving platforms)",
            "Pipe Vault, Area 05",
            "Sea, Area 02 (large room with shop)",
            "Sea, Area 04 (bunch of Zeostars)",
            "Sea, Area 05 (from Area 02 w/save point)",
            "Sea, Area 06 (water room w/whirlpools)",
            "Sea, Area 03 (super star room)",
            "Sea, Area 01 (entrance)",
            "Sea, Area 07 (small underwater room)",
            "Land's End, Area 01",
            "Land's End, Area 02",
            "Land's End, Area 03 (Geckits playing cannonball)",
            "Land's End, Area 01,2 (nothing there, unused?)",
            "Land's End, Area 04 (rotating flowers)",
            "Land's End, Area 05 (sky bridge)",
            "Pipe Vault, Goomba-thumping room",
            "Bowser's Keep 6-door, treasure after each room",
            "Star Hill, Area 01",
            "Pipe Vault, Area 02 ___dummy",
            "GAME INTRO: Midas River, water tunnel",
            "GAME INTRO: Bandit's Way, Area 04",
            "GAME INTRO: Midas River, Barrel Jumping",
            "GAME INTRO: Moleville, outside during Bowser's troop scene",
            "GAME INTRO: Booster Hill",
            "Marrymore Chapel, main hall",
            "Marrymore Chapel, entrance to sanctuary",
            "Marrymore Chapel, sanctuary (during Booster)",
            "Marrymore Chapel, kitchen",
            "Marrymore Chapel, kitchen (no sprites/exits, unused?)",
            "Star Hill, Area 03",
            "Star Hill, Area 02",
            "Star Hill, Area 04",
            "Sunken Ship, Area 01",
            "Sunken Ship, Area 03 (Greapers)",
            "Sunken Ship, Area 04 (Greapers & Dry Bones)",
            "Sunken Ship, puzzle room 2",
            "Sunken Ship, Area 02 (from entrance w/save point)",
            "Sunken Ship, Area 06 (puzzle room passageway)",
            "Sunken Ship, puzzle room 1",
            "Sunken Ship, Area 05 (long stairwell with running Alley Rats)",
            "Sunken Ship, puzzle room 3",
            "Sunken Ship, Area 07 (puzzle room passageway branch room w/Shaman)",
            "Sunken Ship, Area 14 ____dummy",
            "Sunken Ship, puzzle room 4",
            "Sunken Ship, puzzle room 5",
            "Sunken Ship, post-KC Area 01 (small room w/Trampoline)",
            "Sea, Area 08 (shore with Sunken Ship)",
            "Sunken Ship, post-KC Area 05 (w/Dry Bones, linked by Mario mirror room)",
            "Sunken Ship, Area 08 (w/save point and green switch for barrel)",
            "Sunken Ship, Area 09 (Password room)",
            "Sunken Ship, post-KC Area 04 (long stairwell w/running Alley Rats)",
            "Sunken Ship, post-KC Area 06 (Mario Mirror room)",
            "Sunken Ship, post-KC Area 02 (small 2-level room)",
            "Sunken Ship, post-KC Area 03 (Alley Rats on cannons)",
            "Sunken Ship, post-KC Area 07 (three Dry Bones)",
            "Sunken Ship, post-KC Area 08 (secret room with Frog Coin)",
            "Sunken Ship, post-KC Area 09 (Hidon's room w/save point)",
            "Sunken Ship, post-KC Area 14 (secret Safety Ring)",
            "Sunken Ship, post-KC Area 18 (warp room from Johnny's room)",
            "Sunken Ship, post-KC Area 10 (water room with frog coins)",
            "Sunken Ship, post-KC Area 11 (water room with whirlpool)",
            "Mario's Pipehouse",
            "Mushroom Kingdom, during Mack, Outside",
            "Mushroom Kingdom, Outside",
            "Booster Tower, 9F Area 02 (Booster's curtain game room)",
            "Booster Tower, 2F Area 03 (steps w/circling Bob-ombs)",
            "Booster Tower, 2F Area 02 (Booster's railway room)",
            "Booster Tower, 6F Area 02 (Booster's Ancestor Game room)",
            "Booster Tower, 2F Area 01 (w/constantly appearing Spookums)",
            "Booster Tower, 1F Area 02 (high Masher room w/teeter-totter)",
            "Booster Tower, 8F Area 03 (3-level w/one Chomp)",
            "Booster Tower, 9F Area 01 (three yellow platforms w/save point)",
            "Booster Tower, 6F Area 03 (Elder's Room w/Chomp)",
            "Booster Tower, 6F Area 01 (small room w/save point)",
            "Booster Tower Entrance",
            "Mushroom Way, Area 01",
            "Mushroom Way, Area 02",
            "Mushroom Way, Area 03",
            "Bandit's Way, Area 05",
            "Bandit's Way, Area 02",
            "Seaside Town, during Yaridovich Outside",
            "Seaside Town, during Yaridovich Inn (1F)",
            "Seaside Town, during Yaridovich Inn (2F)",
            "Seaside Town, during Yaridovich Elder's House (1F)",
            "Seaside Town, during Yaridovich Elder's House (2F)",
            "Seaside Town, during Yaridovich Beetles Are Us/Bomb Shop",
            "Seaside Town, during Yaridovich Weapons and Armor Shop",
            "Seaside Town, during Yaridovich Health Food Store (left-most)",
            "Seaside Town, during Yaridovich Mushroom Boy Shop (middle)",
            "Seaside Town, during Yaridovich Accessory Shop (right-most)",
            "Seaside Town, during Yaridovich Shed (unused b/c inaccessible)",
            "GAME INTRO: Sea, shore with Sunken Ship",
            "Smithy Factory, Area 02 (w/save point)",
            "Smithy Factory, Area 04 (green switch w/Ameboids)",
            "Smithy Factory, Area 03 (Glum Reapers)",
            "Smithy Factory, Area 07 (Count Down's room)",
            "Forest Maze, Area 01",
            "Forest Maze, Area 05 (tree trunk Area)",
            "Forest Maze, Area 02",
            "Forest Maze, Area 09 (leads to 4-path maze)",
            "Forest Maze, Area 04",
            "Forest Maze, Area 06",
            "Forest Maze, 4-way path from Area 09",
            "Forest Maze, Secret entrance",
            "Forest Maze, Bowyer's practice pad",
            "Forest Maze, Area 03 (underground)",
            "Forest Maze, Secret",
            "Forest Maze, Area 08 (underground)",
            "Forest Maze, Area 07 (underground w/sleeping Wiggler)",
            "Smithy Factory, Area 05 (w/save point)",
            "Smithy Factory, fall from lugnut rooms (Area 06 & prior)",
            "Smithy Factory, Area 06 (Ultra Hammer)",
            "Volcano, Area 21 ____dummy",
            "Volcano, Area 02 ____dummy",
            "Forest Maze, all tree trunk underground areas",
            "GAME INTRO: Mushroom Kingdom Castle, Throne Room",
            "GAME INTRO: Yo'ster Isle, talk to Yoshi & run around",
            "GAME INTRO: Pipe Vault, Area 02 (w/Thwomp)",
            "GAME INTRO: Kero Sewers, Entrance",
            "GAME INTRO: Tadpole Pond, Mario summons tadpoles",
            "GAME INTRO: Mushroom Way, Area 01",
            "GAME INTRO: Vista Hill",
            "GAME INTRO: Booster Tower, balcony with Toadstool crying",
            "Bean Valley, piranha pipe Area",
            "Bean Valley, main Area",
            "Bean Valley, magic brick to Beanstalk Area",
            "Bean Valley, Smilax Area",
            "Monstro Town, Jinx's Dojo",
            "Forest Maze, Small area w/tree trunk (unused?)",
            "GAME INTRO: Forest Maze, fighting Magikoopa at Bowyer's Pad",
            "Booster Tower, Balcony at top floor",
            "Booster Tower, 3F Area 01 (green switch for BP secret)",
            "GAME INTRO: Forest Maze, jumping on Wiggler",
            "Bowser's Keep 1st time, Area 03 (lava room w/bridge)",
            "Land's End Underground, Area 04 (buy super stars)",
            "Land's End Underground, Area 01",
            "Land's End Underground, Area 02",
            "Land's End Underground, Area 03",
            "Bowser's Keep, Area 10 (Magikoopa's room)",
            "Monstro Town, entrance",
            "Belome Temple, Area 08 (Belome's room)",
            "ENDING CREDITS: Nimbus Land, Prince Mallow",
            "Land's End secret underground, Area 01 (leads to Kero Sewers)",
            "Moleville Mines, Area 17 (Punchinello's room, after battle)",
            "Moleville Mines, Area 11 (bombed room w/singing Moles)",
            "Moleville Mines, Area 04 (w/trampoline)",
            "Moleville Mines, Area 02",
            "Moleville Mines, Area 06 (small room leading to Area 06)",
            "Moleville Mines, Area 01 (entrance)",
            "Moleville Mines, Area 05 (left of trampoline room)",
            "Moleville Mines, Area 03 (leads back to Area 1)",
            "Moleville Mines, Area 08 (Croco's bombed room)",
            "Moleville Mines, Area 15 (2-level room w/Sparky and 10-coin TC)",
            "Moleville Mines, Area 07 (from Croco's bombed room)",
            "Moleville Mines, Area 10 (small room w/minecart tracks)",
            "Moleville Mines, Area 09 (leads left to Croco's bombed room)",
            "Moleville Mines, Area 18 (minecart room)",
            "Moleville Mines, Area 13 (long minecart tracks room)",
            "Moleville Mines, Area 12 (2-level room, leads to long minecart tracks room)",
            "Moleville Mines, Area 14 (2-level room from long minecart tracks room)",
            "Moleville Mines, Area 16 (large save-point room w/four Bob-ombs)",
            "Moleville Mines, Area 17 (Punchinello's room, before battle)",
            "Moleville Mines, Area 19 (from outside after paying)",
            "GAME INTRO: Booster Tower, 7F (parachuting Spookums)",
            "____unmapped house room",
            "____unmapped house room",
            "____unmapped house room",
            "____unmapped house room",
            "____unmapped house room",
            "____unmapped outside townplace (resembles Seaside Town)",
            "____unmapped house room",
            "____unmapped house room",
            "____unmapped house room",
            "Kero Sewers, Area 07 (water switch room w/Boos)",
            "Kero Sewers, Area 08 (Belome's Room)",
            "Kero Sewers, Area 08 (Belome's Room, after defeat)",
            "Seaside Town, Outside",
            "Seaside Town, Inn (1F)",
            "Seaside Town, Inn (2F)",
            "Seaside Town, Elder's house (1F)",
            "Seaside Town, Elder's house (2F)",
            "Seaside Town, Beetles Are Us",
            "Seaside Town, Weapon and Armor shop",
            "Seaside Town, Health Food Store",
            "Seaside Town, Mushroom Boy's Shop",
            "Seaside Town, Accessory Shop",
            "Seaside Town, Shed",
            "Seaside Town, during Yaridovich Beach",
            "Seaside Town, Beach",
            "Land's End Desert, Area 01",
            "Land's End Desert, Area 02",
            "Land's End Desert, Area 06",
            "Mushroom Kingdom Castle, Entrance to Throne room",
            "Bowser's Keep 6-door, Action Room 2-A (slow elevating platforms)",
            "Bowser's Keep 6-door, Action Room 1-A (jumping Terrapin)",
            "Mushroom Kingdom Castle, during Mack entrance to Throne Room",
            "Monstro Town, outside",
            "Mushroom Kingdom Castle, during Mack Main Hall",
            "Mushroom Kingdom Castle, during Mack Throne Room",
            "Mushroom Kingdom Castle, during Mack stairwell to Toadstool's Room",
            "Mushroom Kingdom Castle, during Mack Toadstool's Room",
            "Mushroom Kingdom Castle, during Mack branch room to Vault/Guest Room",
            "Mushroom Kingdom Castle, during Mack Guest room",
            "Mushroom Kingdom Castle, during Mack Vault",
            "Mushroom Kingdom Castle, during Mack entrance to Toadstool's room",
            "Kero Sewers Entrance",
            "Bean Valley pipe room, left-most pipe",
            "Bean Valley pipe room, right-most pipe (large room)",
            "Moleville, Item Shop",
            "Moleville, Inn",
            "Moleville, Dyna and Mite's house",
            "Moleville, Fireworks shop",
            "Moleville, Special item-trading shop",
            "Nimbus Land, Garro's House",
            "Nimbus Land, lower house",
            "Nimbus Land, Inn",
            "Nimbus Land, Item Shop",
            "Nimbus Land, top-right house (Croco drops Signal Ring)",
            "Nimbus Land, Inn (bedroom)",
            "Bean Valley pipe room, top pipe (leads to Grate Guy's Casino)",
            "Bean Valley pipe room, bottom left",
            "Bean Valley pipe room, bottom right",
            "Smithy Factory, Area 01",
            "Culex's Room",
            "Volcano, Area 21 (Czar Dragon's room)",
            "Volcano, Area 18 (Hino Mart)",
            "Volcano, Area 01",
            "Volcano, Area 03 (secret w/two flowers)",
            "Volcano, Area 08",
            "Volcano, Post-CD Area 01",
            "Volcano, Area 11",
            "Volcano, Area 02",
            "Volcano, Area 04 (bunch of steps)",
            "Volcano, Area 09",
            "Volcano, Area 07 (stomping Corkpedite)",
            "Volcano, Area 15 (stomping Corkpedite)",
            "Volcano, Area 14",
            "Volcano, Post-CD Area 03",
            "Volcano, Area 13 (w/save point)",
            "Volcano, Area 17 (leads to Hinopio's Shop)",
            "Nimbus Land, Royal Bus station",
            "Nimbus Land, entrance (w/warp trampoline)",
            "Nimbus Land, entrance to hot springs",
            "Nimbus Land, fall from platform (1st)",
            "Nimbus Land, fall from platform (2nd)",
            "Nimbus Land, fall from platform (3rd)",
            "Nimbus Land, fall from platform (4th)",
            "ENDING CREDITS: Vista Hill (Exor vanishes from the world)",
            "Bowser's Keep 6-door, Battle Room 2-B (1st fight: Chewy)",
            "Bowser's Keep 6-door, Battle Room 2-C (1st fight: Sparky)",
            "Bean Valley Beanstalks, Area 01",
            "Bean Valley Beanstalks, Area 02",
            "Bean Valley Beanstalks, Area 03 (from right beanstalk of Area 02)",
            "Bean Valley Beanstalks, Area 04 (from left beanstalk of Area 02)",
            "Nimbus Land, entrance (no trampolines/exits)",
            "Volcano, Area 10 (jumping Pyrospheres)",
            "Volcano, Area 05",
            "Volcano, Area 06",
            "Volcano, Area 12 (erupting Stumpet)",
            "Volcano, Area 19 (from Hino Mart w/save point)",
            "Volcano, Post-CD Area 02",
            "Volcano, Area 20 (jumping Pyrospheres)",
            "Volcano, Area 16 (erupting Stumpet)",
            "Volcano, Post-CD Area 04",
            "Volcano, Post-CD Area 06",
            "Volcano, Post-CD Area 07 (warp to World Map)",
            "Volcano, Post-CD Area 05",
            "Monstro Town, Monstermama's house (1F)",
            "Monstro Town, Monstermama's house (2F)",
            "Monstro Town, super-jumping room",
            "Monstro Town, Weapon and Armor Shop",
            "Monstro Town, 3 Musty Fears Inn",
            "Bowser's Keep, Area 13 (2nd throne room, Boomer's room)",
            "Land's End secret underground, Area 02 (leads to Kero Sewers)",
            "Land's End Desert, Area 03",
            "Land's End Desert, Area 05",
            "Land's End Desert, Area 04",
            "Booster Pass, Secret",
            "Factory Grounds, Area 01 (with Toad)",
            "Land's End Cliff (climb w/Sky Troopas)",
            "Nimbus Castle, Area 14 (right-most front door of long 5-exit room) ",
            "Nimbus Castle, Area 09 (Birdo's Room)",
            "Nimbus Castle, Area 07 (straight from Area 06 w/long staircase)",
            "Nimbus Castle, path after Throne room (1st)",
            "Nimbus Castle, Area 11 (long hallway, door to King's Cellar)",
            "Nimbus Castle, King's locked cellar",
            "Nimbus Castle, Area 08 (from Area 07, get Room Key 1 here)",
            "Nimbus Land, small platform after Nimbus Castle throne paths",
            "Nimbus Land, outside (before Valentina)",
            "Gardener's House, outside",
            "Gardener's House",
            "Lazy Shell cloud",
            "Belome Temple, Area 02 (Fortune Room)",
            "Belome Temple, Area 04 (room determined by fortune)",
            "Belome Temple, Area 09 (Belome's Treasure room)",
            "Belome Temple, Area 06 (Belome's fortune room w/elevating platform)",
            "Belome Temple, Area 03 (pipe to room determined by fortune)",
            "Belome Temple, Area 05 (from Fortune Room)",
            "Belome Temple, Area 07 (pipe to Belome's room)",
            "Belome Temple, Area 10 (pipe to Monstro Town)",
            "Belome Temple, Area 01 (w/Warp Trampoline)",
            "GAME INTRO: Nimbus Land, outside with patrolling Birdies",
            "Nimbus Land, outside (during Valentina)",
            "Bowser's Keep 6-door, Puzzle Rooms",
            "ENDING CREDITS: Johnny looking out at sunset on beach shore",
            "Smithy Factory, Area 01 ____dummy",
            "Smithy Factory, Area 09 (falling Axem Reds on conveyor belts)",
            "ENDING CREDITS: Bowser's Keep, Bowser & troops repair",
            "Smithy Factory, Area 01 ____dummy",
            "Nimbus Castle, path after Throne room (3rd)",
            "Nimbus Land, outside (after Valentina)",
            "Bowser's Keep, outside (talk to Exor)",
            "Nimbus Castle, Area 13 (Throne room, after Valentina)",
            "ENDING CREDITS: Toadofsky conducts choir",
            "Smithy Factory, Area 11 (conveyor belts spawning Drill Bits and Macks)",
            "Smithy Factory, Area 16 (small room w/two treasures after falling Yaridovich room)",
            "Smithy Factory, Area 09 ____dummy",
            "Smithy Factory, Area 10 (fall from Area 09)",
            "Bowser's Keep 6-door, exit room after finishing 4 doors",
            "Nimbus Land, hot springs",
            "Bowser's Keep, Area 09 (tall room, w/save point)",
            "Bowser's Keep, Area 11 (Thwomp/Bullet room after Magikoopa's room)",
            "Bowser's Keep, Area 12 (Croco's Shop 2, after Magikoopa's room)",
            "Bowser's Keep, Area 07 (150 coins and a mushroom)",
            "Bowser's Keep, Area 06 (save point w/Croco shop)",
            "Bowser's Keep, Area 05 (dark tunnel, after throne room)",
            "Bowser's Keep, Area 08 (room with 6 doors)",
            "Bowser's Keep 6-door, Action Room 2-C (very slow moving circling platforms)",
            "Bowser's Keep 6-door, Action Room 1-C (Gorilla throwing barrels)",
            "Bowser's Keep 6-door, Action Room 2-B (cannonball riding)",
            "Bowser's Keep 6-door, Action Room 1-B (moving platforms)",
            "Bowser's Keep 6-door, Battle Room 1-A (1st fight: Terra Cotta)",
            "Bowser's Keep 6-door, Battle Room 1-B (1st fight: Alley Rat)",
            "Bowser's Keep 6-door, Battle Room 1-C (1st fight: Bob-Omb)",
            "Bowser's Keep 6-door, Battle Room 2-A (1st fight: Gu Goomba)",
            "Bowser's Keep 6-door, Puzzle Room 1-B (barrel-counting)",
            "Bowser's Keep 6-door, Puzzle Room 1-A (quiz)",
            "Bowser's Keep 6-door, Puzzle Room 2-B (green switches)",
            "Bowser's Keep 6-door, Puzzle Room 1-C (word problem)",
            "Bowser's Keep 6-door, Puzzle Room 2-A (coin collecting)",
            "Bowser's Keep 6-door, Puzzle Room 2-C (ball solitaire)",
            "Factory Grounds, Area 01",
            "Factory Grounds, Area 04 (Gun Yolk's room)",
            "Factory Grounds, Area 02",
            "Factory Grounds, Area 03",
            "Smithy Factory, Area 13 (Bowyers falling down conveyor belts)",
            "Smithy Factory, Area 15 (falling Yaridovichs)",
            "Smithy Factory, Area 12 (lots of consecutive conveyor belts and LIL{xx}BOOS)",
            "Bowser's Keep 2nd Time, Area 01",
            "Bowser's Keep 2nd Time, Area 02",
            "Bowser's Keep 2nd Time, Area 03 (lava room w/bridge)",
            "Bowser's Keep 2nd Time, Area 04 (Throne Room)",
            "Mushroom Kingdom, during Mack, jumping kid's house (1F)",
            "Mushroom Kingdom, during Mack, jumping kid's house (2F)",
            "Mushroom Kingdom, during Mack, Raz and Raini's house",
            "Mushroom Kingdom, during Mack, Item Shop (top floor)",
            "Mushroom Kingdom, during Mack, Item Shop (basement)",
            "Mushroom Kingdom, during Mack, Inn (1F)",
            "ENDING CREDITS: Star Pieces (Rose Town), last star piece to ‘Thank You'",
            "Mushroom Kingdom, during Mack, running kid's house",
            "Mushroom Kingdom, jumping kid's house (1F)",
            "Mushroom Kingdom, jumping kid's house (2F)",
            "Mushroom Kingdom, Raz and Raini's house",
            "Mushroom Kingdom, Item Shop (top floor)",
            "Mushroom Kingdom, Item Shop (basement)",
            "Mushroom Kingdom, Inn (1F)",
            "Mushroom Kingdom, Inn (2F)",
            "Mushroom Kingdom, running kid's house",
            "ENDING CREDITS: Factory Grounds, fight with Smithy",
            "Nimbus Castle, Area 06 ____dummy",
            "Nimbus Castle, Area 10 ____dummy",
            "Nimbus Castle, Area 05 (long 5-exit room, after Valentina)",
            "Nimbus Castle, Area 04 ____dummy",
            "Nimbus Castle, Area 03 (4-way path, after Valentina)",
            "Nimbus Land, Dream Cushion Dream: small cloud, person cheers on Mario/bed floats",
            "Nimbus Land, Dream Cushion Dream: Heavy Troopa laying on Mario",
            "Nimbus Land, Dream Cushion Dream: Tortes are seasoning Mario",
            "ENDING CREDITS: Yo'ster Isle, Croco racing Yoshi",
            "ENDING CREDITS: Marrymore Chapel, Booster wedding Valentina",
            "Smithy Factory, Area 08 (Trampoline after Count Down)",
            "Smithy Factory, Area 14 (w/save point)",
            "Factory Grounds, Smithy's Pad"
        };
        public static string[] GraphicSetNames = new string[]
        {
            "{NONE}",
            "Keep walls",
            "Keep wall decor",
            "Keep doormat, doors",
            "Rope bridge, lava",
            "Keep window grates",
            "Gargoyles and pillars",
            "Barrel Volcano",
            "Royal Bus",
            "Kingdom houses",
            "Castle exterior",
            "Castle doors, fireplace",
            "Keep turrets",
            "Keep gargoyle hill",
            "Keep ground",
            "Keep body",
            "Keep body edges",
            "House interior",
            "Grates, stoves",
            "Crates, boxes",
            "Beds",
            "Shacks interior",
            "House exterior",
            "House doors",
            "Mines plywood",
            "Mines crates, lanterns",
            "Town decor",
            "Castle walls",
            "Castle wall decor",
            "Castle interior",
            "Tower wall decor",
            "Tower curtains",
            "Casino interior",
            "Tower floor",
            "____",
            "Forest terrain",
            "Forest tree trunks",
            "Forest battlefield",
            "Forest dirt",
            "Seashells",
            "Ship walls,doors",
            "Ship interior",
            "Ship pipes",
            "Shark emblem",
            "Doors",
            "Desert decor",
            "____",
            "Temple floors",
            "Temple walls",
            "Temple pillars",
            "Temple steps",
            "Shacks",
            "Mountain",
            "Mountain decor",
            "Mines floor",
            "Mines railing",
            "Stalactites/stalagmites",
            "Molten lava",
            "Arrow signs",
            "Palm trees, hills 1",
            "Palm trees, seat",
            "Seashore rocks",
            "Seashore cliffs",
            "Dojo walls, floor",
            "Grassland hills",
            "Grassland grass",
            "Grassland ground",
            "Pipehouse roof",
            "Pipehouse porch",
            "Tower, exterior",
            "Tower, entrance 1",
            "____",
            "Palm trees, hills 2",
            "Tower, entrance 2",
            "Seashore Ship",
            "Yo’ster Isle",
            "Marrymore Scene",
            "Ground puddle",
            "Plains hills, trees",
            "Plains ground, rock",
            "Rotating flowers",
            "Countryside",
            "____",
            "Plains escarpment",
            "Booster Hill sand",
            "Booster Hill cactus",
            "Booster Hill BG",
            "Nimbus Castle, exterior",
            "Nimbus leaves",
            "Nimbus leaves, briar",
            "Exor Battlefield",
            "Exor's hilt",
            "Exor's head",
            "Exor's face",
            "Exor's arms",
            "Ground/Mist",
            "“Hollow” sign",
            "Nimbus exterior",
            "Nimbus Castle walls",
            "Nimbus Castle interior 1",
            "Nimbus Castle interior 2",
            "Smithy Factory, floor",
            "Smithy Factory, pillar top",
            "Smithy Factory, pillar lower",
            "Smithy Factory, pillar floor",
            "Conveyor belts",
            "Seashore Ship, seafloor",
            "Sanctuary walls",
            "Sanctuary organ",
            "Nimbus house interior 1",
            "Nimbus house interior 2",
            "The Blade",
            "Shelly, nest",
            "Birdo's egg, nest",
            "Keep, throne walls",
            "Keep, throne steps",
            "Keep, throne floor",
            "Keep, throne gargoyles",
            "Chandeliers",
            "____",
            "River water",
            "Star Hill exterior",
            "Star Hill decor",
            "Vista Hill Keep",
            "Beanstalks",
            "Seashore Sunset",
            "Factory floor, crane",
            "Factory structures",
            "Stump battlefield top",
            "Stump battlefield lower",
            "Factory conveyor belts",
            "Mist/clouds",
            "Beanstalk leaf (top)",
            "Beanstalk leaf (lower)",
            "Beanstalk vine (top)",
            "Beanstalk leaf (right)",
            "Ship cellar (top)",
            "Ship cellar (bottom)",
            "Ship, barrels",
            "Mines interior",
            "Factory walls",
            "Keep repairs",
            "Czar Dragon gargoyles",
            "Grasslands grass",
            "Grasslands hills",
            "Mountain bushes",
            "House interior corners",
            "Tower, backdoor",
            "Water sewer walls",
            "Tower balcony clouds",
            "Beanstalk leaf (left)",
            "Castle candle holders",
            "Beanstalk clouds",
            "Dirt mountains",
            "Dirt mountains",
            "Tower balcony top",
            "Tower balcony lower",
            "Countdown",
            "Sewers back wall",
            "____palette tiles",
            "Nimbus Castle interior 3",
            "Birdo's nest egg",
            "Birdo's nest",
            "Nimbus briar",
            "Nimbus leaves",
            "____forest vines",
            "____unknown",
            "Town 2, exterior",
            "Keep carpet, walls",
            "Town 2, decor",
            "Forest path",
            "Level-Up FG",
            "Menu BG 1",
            "Menu BG 2",
            "Plains palm trees",
            "Sea Enclave",
            "Sanctuary organ",
            "Level-Up BG",
            "Star Hill",
            "Beach rocks, sunset",
            "Blade Roof",
            "Blade Roof, BG",
            "Blade Roof, BG",
            "Giant snake body",
            "Desert cactus, floor",
            "Factory floor/walls",
            "Factory chains/bolts",
            "Factory structure",
            "Smithy 2, head/pipes",
            "Smithy 2, small heads",
            "Smithy 2, big heads",
            "Culex battlefield BG",
            "Factory metals",
            "Factory chains/bolts",
            "Nimbus throne",
            "Yo’ster Isle flowers",
            "Desk, floors, boxes",
            "Count Down",
            "____",
            "Vista Hill Exor"
        };
        public static string[] TileSetL3Names = new string[]
        {
            "{NOTHING}",
            "Booster Tower",
            "Mansion, inside",
            "Forest Maze",
            "Sunken Ship",
            "Kero Sewers",
            "____",
            "Water",
            "Grasslands",
            "River",
            "____",
            "Waterfall",
            "Clouds",
            "Yo\'ster Isle",
            "Maps",
            "Towns 2",
            "Sewers",
            "Houses, inside",
            "Grasslands 2",
            "Keep, throne",
            "Booster Hill",
            "Star Hill",
            "Marrymore Scene",
            "Nimbus Land",
            "Keep, inside",
            "Temples",
            "Desert",
            "____",
            "Smithy Factory",
            "____",
            "Smithy 2",
            "____",
            "____",
            "____"
        };
        public static string[] TileSetNames = new string[]
        {
            "Houses, inside  (L1)",
            "Houses, inside  (L2)",
            "____",
            "Keep, puzzles (L2)",
            "Towns 1 (L1)",
            "Towns 1 (L2)",
            "Grasslands 1 (L1)",
            "Towns 2 (L1)",
            "Towns 2 (L2)",
            "Sewers (L1)",
            "Sewers (L2)",
            "Keep, outside (L1)",
            "____",
            "____",
            "Tower, entrance (L1)",
            "Tower, entrance (L2)",
            "Keep, puzzles (L1)",
            "Keep, inside (L1,2)",
            "Pipe Rooms (L1,2)",
            "Mansion (L1)",
            "Mansion (L2)",
            "Forest Maze (L1)",
            "Forest Maze (L2)",
            "Sunken Ship (L1)",
            "Sunken Ship (L2)",
            "Mountains (L1)",
            "Mountains (L2)",
            "Underground (L1,2)",
            "Underground (L1,2)",
            "Tower, inside (L1)",
            "Tower, inside (L2)",
            "Seashore (L1)",
            "Seashore (L2)",
            "Plains (L1,2)",
            "Underground 2 (L1)",
            "Underground 2 (L2)",
            "Riverside (L1)",
            "Riverside (L2)",
            "Clouds (L1)",
            "Clouds (L2)",
            "____",
            "Culex (L1)",
            "Culex (L2)",
            "Grasslands 2 (L1)",
            "Grasslands 2 (L2)",
            "Waterfall (L1)",
            "Waterfall (L2)",
            "Nimbus Castle (L1)",
            "Nimbus Castle (L2)",
            "Yo'ster Isle (L1)",
            "Yo'ster Isle (L2)",
            "Smithy Factory (L1,2)",
            "____",
            "____",
            "Count Down (L1)",
            "____",
            "Sanctuary (L1)",
            "Sanctuary (L2)",
            "Keep, inside (L1,2)",
            "____",
            "____",
            "Shacks (L1)",
            "Grasslands 1 (L2)",
            "Keep, outside (L2)",
            "Keep, throne (L1)",
            "Keep, throne (L2)",
            "Keep, inside (L1)",
            "Keep, inside (L2)",
            "Midas River (L2)",
            "Water Tunnels (L1)",
            "Water Tunnels (L2)",
            "Suite (L1)",
            "Volcano Map (L1)",
            "Star Hill (L1,2)",
            "Vista Hill (L1,2)",
            "Marrymore Scene (L1,2)",
            "Tower Balcony (L1,2)",
            "Bean Valley (L1)",
            "Bean Valley (L2)",
            "Nimbus Land (L1)",
            "Nimbus Land (L2)",
            "Volcano, Map (L2)",
            "Jinx's Dojo (L1,2)",
            "Factory Grounds (L1,2)",
            "____",
            "Ending, Seashore (L1,2)",
            "Ending, Keep (L1,2)",
            "Ending, Toadofsky (L1)",
            "Ending, Toadofsky (L2)",
            "____",
            "Ending, Yo'ster Isle (L1)",
            "Ending, Yo'ster Isle (L2)",
            "____"
        };
        public static string[] TileMapNames = new string[]
        {
            "Bowser’s Keep, outside (L2)",
            "____",
            "Chapel Kitchen (L1)",
            "Chapel Kitchen (L2)",
            "Land's End 1 (L1)",
            "Land's End 1 (L2)",
            "Booster Tower 1 (L1)",
            "Booster Tower 1 (L2)",
            "____",
            "____",
            "Mushroom Kingdom houses (L1)",
            "Mushroom Kingdom houses (L2)",
            "Mario's Pad, outside (L1)",
            "Mario's Pad, outside (L2)",
            "Grate Guy's Casino, outside (L1)",
            "Grate Guy's Casino, outside (L2)",
            "Bowser's Keep 1 (L1)",
            "Bowser's Keep 1 (L2)",
            "Forest Maze 1 (L1)",
            "Forest Maze 1 (L2)",
            "____",
            "____",
            "Rose Town (L1)",
            "Rose Town (L2)",
            "Kero Sewers 1 (L1)",
            "Kero Sewers 1 (L2)",
            "____",
            "____",
            "Tadpole Pond 1 (L1)",
            "Tadpole Pond 1 (L2)",
            "Beach (L1)",
            "Beach (L2)",
            "Castle Rooms (L1)",
            "Castle Rooms (L2)",
            "Sunken Ship 1 (L1)",
            "Sunken Ship 1 (L2)",
            "Forest Maze 1 (L1)",
            "Forest Maze 1 (L2)",
            "Forest Maze 2 (L1)",
            "Forest Maze 2 (L2)",
            "Forest Maze 3 (L1)",
            "Forest Maze 3 (L2)",
            "____",
            "____",
            "Sunken Ship 2 (L1)",
            "Sunken Ship 2 (L2)",
            "Debug Room (L1)",
            "Debug Room (L2)",
            "Barrel Volcano 1 (L1)",
            "Barrel Volcano 1 (L2)",
            "Barrel Volcano 2 (L1)",
            "Barrel Volcano 2 (L2)",
            "Kero Sewers 2 (L1)",
            "Kero Sewers 2 (L2)",
            "Rose Town houses (L1)",
            "Rose Town houses (L2)",
            "Booster Pass secret (L1)",
            "Booster Pass secret (L2)",
            "Booster Tower entrance (L1)",
            "Booster Tower entrance (L2)",
            "Seashore (L1)",
            "Seashore (L2)",
            "Booster Tower 1 (L1)",
            "Booster Tower 1 (L2)",
            "Mushroom Kingdom (L1)",
            "Mushroom Kingdom (L2)",
            "Bowser's Keep outside(L1)",
            "____",
            "Seaside Town houses (L1)",
            "Seaside Town houses (L2)",
            "Moleville shacks (L1)",
            "Moleville shacks (L2)",
            "Forest Maze underground (L1)",
            "Forest Maze underground (L2)",
            "Forest Maze, area 7 (L1)",
            "Forest Maze, area 7 (L2)",
            "Land's End underground (L1)",
            "Land's End underground (L2)",
            "Moleville Mines 1 (L1)",
            "Moleville Mines 1 (L2)",
            "____",
            "____",
            "____",
            "____",
            "Land's End grasslands (L1)",
            "Land's End grasslands (L2)",
            "Moleville Mines 2 (L1)",
            "Moleville Mines 2 (L2)",
            "Moleville Mines 3 (L1)",
            "Moleville Mines 3 (L2)",
            "____",
            "____",
            "Plains (L1)",
            "Plains (L2)",
            "Booster Hill (L1)",
            "Booster Hill (L2)",
            "Tadpole Pond 2 (L1)",
            "Tadpole Pond 2 (L2)",
            "Clouds (L1)",
            "Clouds (L2)",
            "____",
            "____",
            "Bowser's Keep 2 (L1)",
            "Bowser's Keep 2 (L2)",
            "___forest (L1)",
            "___forest (L2)",
            "Midas River (L1)",
            "Midas River (L2)",
            "Yo'ster Isle (L1)",
            "Yo'ster Isle (L2)",
            "Suite (L1)",
            "Suite (L2)",
            "Waterfall (L1)",
            "Waterfall (L2)",
            "___underground (L1)",
            "___underground (L2)",
            "Rose Way (L1)",
            "Rose Way (L2)",
            "____",
            "____",
            "Marrymore (L1)",
            "Marrymore (L2)",
            "Nimbus Castle 1 (L1)",
            "Nimbus Castle 1 (L2)",
            "Nimbus Castle 2 (L1)",
            "Nimbus Castle 2 (L2)",
            "Bowser's Keep Bridge (L1)",
            "Bowser's Keep Bridge (L2)",
            "Sea (L1)",
            "Sea (L2)",
            "Pipe Vault (L1)",
            "Pipe Vault (L2)",
            "____",
            "____",
            "Booster Tower balcony (L1)",
            "Beanstalks (L1)",
            "Smithy Factory 1 (L1)",
            "Smithy Factory 1 (L2)",
            "Smithy Factory 2 (L1)",
            "Smithy Factory 2 (L2)",
            "Smithy Factory 3 (L1)",
            "Smithy Factory 3 (L2)",
            "Nimbus Land houses (L1)",
            "Nimbus Land houses (L2)",
            "Star Hill 2 (L1)",
            "Star Hill 2 (L2)",
            "Bean Valley pipes (L1)",
            "Bean Valley pipes (L2)",
            "____",
            "____",
            "Chapel, main hall (L1)",
            "Chapel, main hall (L2)",
            "Chapel sanctuary (L1)",
            "Chapel sanctuary (L2)",
            "Belome Temple 1 (L1)",
            "Belome Temple 1 (L2)",
            "____",
            "____",
            "____",
            "____",
            "Bandit's Way 1 (L1)",
            "Bandit's Way 1 (L2)",
            "Bandit's Way 2 (L1)",
            "Bandit's Way 2 (L2)",
            "Mario's Pipehouse (L1)",
            "Mario's Pipehouse (L2)",
            "Mushroom Way 1 (L1)",
            "Mushroom Way 1 (L2)",
            "____",
            "____",
            "Kero Sewers, area 1 (L1)",
            "Kero Sewers, area 1 (L2)",
            "Rose Way, area 1 (L1)",
            "Rose Way, area 2 (L2)",
            "Midas River tunnels (L1)",
            "Midas River tunnels (L2)",
            "Booster Pass 1 (L1)",
            "Booster Pass 1 (L2)",
            "Moleville (L1)",
            "Moleville (L2)",
            "Volcano Map (L1)",
            "Volcano Map (L2)",
            "Sunken Ship 3 (L1)",
            "Sunken Ship 3 (L2)",
            "Vista Hill (L1)",
            "Vista Hill (L2)",
            "Marrymore Scene (L1)",
            "Marrymore Scene (L2)",
            "Bean Valley (L1)",
            "Bean Valley (L2)",
            "Beanstalks (L2)",
            "Land's End Underground 2 (L1)",
            "Land's End Underground 2 (L2)",
            "Land's End Desert (L1)",
            "Land's End Desert (L2)",
            "Barrel Volcano 1 (L1)",
            "Barrel Volcano 1 (L2)",
            "Jinx's Dojo (L1)",
            "Factory Grounds 1 (L1)",
            "Monstro Town houses (L1)",
            "Monstro Town houses (L2)",
            "Monstro Town (L1)",
            "Monstro Town (L2)",
            "Bowser's Keep 6-doors 1 (L1)",
            "Bowser's Keep 6-doors 1 (L2)",
            "Culex's Room (L1)",
            "Culex's Room (L2)",
            "Bowser's Keep 6-doors 2 (L1)",
            "Bowser's Keep 6-doors 2 (L2)",
            "Bowser's Keep 3 (L1)",
            "Bowser's Keep 3 (L2)",
            "Bowser's Keep 6-doors 3 (L1)",
            "Bowser's Keep 6-doors 3 (L2)",
            "Bowser's Keep, Magikoopa (L1)",
            "Bowser's Keep, Magikoopa (L1)",
            "Bowser's Keep 6-doors 4 (L1)",
            "Bowser's Keep 6-doors 4 (L2)",
            "Bowser's Keep 6-doors 5 (L1)",
            "Bowser's Keep 6-doors 5 (L2)",
            "Ending: Seashore (L1 & 2)",
            "Factory Grounds 1 (L2)",
            "Factory Grounds 2 (L1)",
            "Factory Grounds 2 (L2)",
            "Ending: Keep repairs (L1)",
            "Ending: Keep repairs (L2)",
            "Ending: Toadofsky (L1)",
            "Ending: Toadofsky (L2)",
            "Bowser's Keep 4 (L1)",
            "Bowser's Keep 4 (L2)",
            "Nimbus clouds 2 (L1)",
            "Nimbus clouds 2 (L2)",
            "Smithy Factory 4 (L1)",
            "Smithy Factory 4 (L2)",
            "____",
            "Ending: Yo'ster Isle (L1)",
            "Ending: Yo'ster Isle (L2)",
            "____",
            "___nothing (L1)",
            "___nothing (L2)",
            "Grate Guy's Casino (L1)",
            "Grate Guy's Casino (L2)",
            "Star Hill 1 (L1)",
            "Star Hill 1 (L2)",
            "____",
            "____"
        };
        public static string[] TileMapL3Names = new string[]
        {
            "{NOTHING}",
            "Booster\'s Tower 1 ",
            "Tadpole Pond 2 ",
            "Mushroom Kingdom Castle ",
            "Forest Maze 1",
            "Forest Maze 2",
            "Sunken Ship 1 ",
            "Kero Sewers 1",
            "Sunken Ship 2 ",
            "Booster\'s Tower 2 ",
            "____",
            "Seashore",
            "Midas River ",
            "____",
            "Waterfall ",
            "____",
            "various areas…",
            "Sea",
            "Tadpole Pond 1 ",
            "Nimbus Clouds",
            "Chapel, main hall ",
            "Plains",
            "Yo\'ster Isle ",
            "Maps",
            "Mushroom Kingdom",
            "Sewers",
            "Pipehouse ",
            "Houses 1",
            "Bowser\'s Keep Throne",
            "Rose Way, area 1 ",
            "Houses 2",
            "____",
            "Rose Way",
            "Moleville shacks ",
            "Houses 3",
            "Suite",
            "Sunken Ship 3",
            "Star Hill 2",
            "Vista Hill ",
            "Seaside Town beach ",
            "Grasslands 2",
            "Marrymore Scene",
            "____",
            "Nimbus Land houses ",
            "Jinx\'s Dojo ",
            "Monstro Town houses",
            "Bowser\'s Keep 6-doors 1",
            "Pipe Rooms",
            "Culex\'s Room ",
            "Bowser\'s Keep 6-doors 2",
            "Bowser\'s Keep 2",
            "Bowser\'s Keep 3",
            "Bowser\'s Keep 6-doors 3",
            "Bowser\'s Keep 6-doors 4",
            "Belome Temple",
            "Land\'s End Desert",
            "Bowser\'s Keep 4",
            "Nimbus Land springs ",
            "Smithy Factory",
            "____",
            "Smithy 2",
            "___nothing",
            "Star Hill 1",
            "____"};
        public static string[] SolidityMapNames = new string[]
        {
            "Debug Room",
            "{NOTHING}",
            "Kero Sewers 1",
            "Bowser\'s Keep 1",
            "____",
            "Mushroom Kingdom Castle",
            "____",
            "____",
            "Gardener\'s House",
            "Seaside Town",
            "____",
            "Forest Maze 3",
            "Midas River, waterfall",
            "Forest Maze 4",
            "Rose Town",
            "____",
            "Forest Maze 2",
            "___underground areas",
            "Sunken Ship 1",
            "Sunken Ship 2",
            "Tadpole Pond 2",
            "____",
            "____",
            "Mushroom Kingdom",
            "Mushroom Kingdom houses",
            "Bowser\'s Keep Throne",
            "Booster\'s Tower 2",
            "Booster\'s Tower 1",
            "Booster\'s Tower entrance",
            "Rose Way",
            "Moleville Mines 1",
            "Moleville Mines 2",
            "Moleville Mines 3",
            "Seaside Town houses",
            "____",
            "Barrel Volcano 1",
            "Barrel Volcano 2",
            "Mario\'s Pad",
            "Rose Town houses",
            "Moleville shacks",
            "Kero Sewers 2",
            "____",
            "____",
            "Bowser\'s Keep 3",
            "Grate Guy\'s Casino",
            "Midas River",
            "Plains",
            "Grasslands",
            "Forest Maze Underground",
            "Forest Maze, area 7",
            "Land\'s End Underground",
            "Suite",
            "____",
            "Nimbus clouds",
            "Nimbus Castle 1",
            "Nimbus Castle 2",
            "Barrel Volcano 3",
            "____",
            "Sea",
            "Pipe Vault",
            "Seashore",
            "____",
            "Smithy Factory 2",
            "Smithy Factory 3",
            "Smithy Factory 1",
            "Tadpole Pond 1",
            "Nimbus Land houses",
            "Star Hill 2",
            "Pipe Rooms",
            "____",
            "____",
            "Chapel, main hall",
            "Chapel sanctuary",
            "Bowser\'s Keep Bridge",
            "Belome\'s Temple 1",
            "____",
            "____",
            "Bandit\'s Way 1",
            "Bandit\'s Way 2",
            "Pipehouse",
            "Mushroom Way 1",
            "____",
            "Kero Sewers 1",
            "Rose Way 1",
            "Waterfall tunnels",
            "Booster Pass 1",
            "Moleville",
            "Marrymore",
            "Marrymore houses",
            "Volcano map",
            "Sunken Ship 2",
            "Vista Hill",
            "Booster Hill",
            "Seaside Town beach",
            "Seaside Town",
            "Land\'s End 1",
            "Land\'s End 2",
            "Bean Valley",
            "Beanstalks",
            "____",
            "Land\'s End 3",
            "Land\'s End desert",
            "Monstro Town houses",
            "Monstro Town",
            "Jinx\'s Dojo",
            "Bowser’s Keep 6-door 1",
            "____",
            "Booster Pass secret",
            "Bowser\'s Keep 4",
            "Bowser\'s Keep 6-door 2",
            "Bowser\'s Keep Magikoopa",
            "Bowser\'s Keep 6-door 3",
            "Bowser\'s Keep 6-door 4",
            "Bowser\'s Keep 6-door 5",
            "Factory Grounds 1",
            "Factory Grounds 2",
            "Bowser\'s Keep 5",
            "Nimbus Clouds 2",
            "Smithy Factory 4",
            "Star Hill 1"};
        public static string[] PaletteSetNames = new string[]
        {
            "Bowser\'s Keep Throne",
            "____",
            "Moleville shacks",
            "Rose Town",
            "____",
            "____",
            "Grasslands 1",
            "____",
            "Bowser\'s Keep Lava",
            "Bowser\'s Keep, outside",
            "Mushroom Kingdom Castle",
            "Forest Maze",
            "Sunken Ship",
            "Sewers",
            "Mountains",
            "Mushroom Kingdom",
            "Marrymore",
            "____",
            "Booster Tower 1",
            "Underground",
            "Bowser\'s Keep 1",
            "Houses",
            "____",
            "____",
            "____",
            "Seaside Town",
            "Booster Tower entrance",
            "Seashore",
            "____",
            "Booster Hill",
            "Rose Way",
            "Nimbus Clouds",
            "Grasslands 2",
            "Culex\'s Room",
            "Plains 1",
            "Plains 2",
            "Nimbus Castle",
            "Grasslands 3",
            "Smithy Factory",
            "____",
            "Sea",
            "Tadpole Pond",
            "Yo\'ster Isle",
            "____",
            "Count Down",
            "Chapel Sanctuary",
            "Bowser\'s Keep Lava",
            "Pipe Rooms",
            "____",
            "Mushroom Kingdom dark",
            "Pipehouse",
            "Waterfall tunnels",
            "Rose Town houses",
            "Rose Town houses",
            "Sewers Gate",
            "Rose Town dark",
            "Booster Tower 2",
            "Suite",
            "Volcano Map",
            "Houses",
            "Star Hill",
            "Marrymore houses",
            "Sunken Ship 2",
            "Vista Hill",
            "Johnny\'s Room",
            "Marrymore Scene",
            "Booster Tower Balcony",
            "Bean Valley",
            "Nimbus Land houses",
            "Jinx\'s Dojo",
            "Monstro Town houses",
            "Monstro Town",
            "Bowser\'s Keep puzzles",
            "Beanstalks",
            "Land\'s End Desert",
            "Seashore sunset",
            "Belome Temple",
            "Nimbus Land",
            "Factory Grounds 2",
            "Factory Grounds 1",
            "Bowser\'s Keep repairs",
            "Nimbus Castle 2",
            "Ending: Toadofsky",
            "Nimbus Land springs",
            "Nimbus Land clouds",
            "Smithy 2",
            "____",
            "Ending: Yo\'ster Isle",
            "Smithy Pad",
            "____",
            "Ending: Nimbus Land",
            "Casino entrance",
            "Casino, inside",
            "Count Down"};
        #endregion
        #region Events
        private static string[] eventLabels = new string[0];
        private static string[] actionLabels = new string[0];
   /*
                  +1731 - Waypoint Event If $074E:3 skip #143 and go to #1746
                   +143 - Waypoint Event
                    +154 - Battle Pack 202 Seaside Town Beach BG(Hex 25 / Dec 37), Goto Event #3839 Via #143
                    +3839 - Sea Shore intro area
                     +145 - Waypoint Event
                      +155 -> Battle Pack 197 Mountains BG(Dec 10 / Hex A), Goto #997 via #145
                      +997 - Intro level Nimbus Land w/ birdbrains
                       +146 - Waypoint Event
                        +156 -> Battle Czar Dragon(204) His BG(8), Goto #2290 Via #146
                        +2290 - Booster Tower, Peach's Intro
                         +141 - Goto #2291
                          +2291 - Open Vista Hill level
                           +147 - Goto #996
                            +996 - Pipe Vault Area 01 (not faded in), "In...' text, Return.  ==END OF SCRIPTS EXECUTION HERE==
            */
        public static string[] EventLabels
        {
            get
            {
                if (eventLabels.Length == 4096)
                    return eventLabels;
                eventLabels = new string[4096];
                for (int i = 0; i < eventLabels.Length; i++)
                {
                    switch (i)
                    {
                        case 16: eventLabels[i] = "Engage in battle (remove permanently after defeat)"; break;
                        case 17: eventLabels[i] = "Engage in battle (remove temporarily after defeat)"; break;
                        case 18: eventLabels[i] = "Engage in battle (do not remove after defeat)"; break;
                        case 19: eventLabels[i] = "Engage in battle (remove permanently after defeat, if ran away, walk through while blinking)"; break;
                        case 20: eventLabels[i] = "Engage in battle (remove temporarily after defeat, if ran away, walk through while blinking)"; break;
                        case 24: eventLabels[i] = "Post-battle, check if lost/won, etc."; break;
                        case 32: eventLabels[i] = "Hit a treasure with a mushroom/star/flower"; break;
                        case 33: eventLabels[i] = "Hit a treasure with an item (item bag sprite)"; break;
                        case 34: eventLabels[i] = "Hit a treasure with coins"; break;
                        case 65: eventLabels[i] = "Jump on trampoline"; break;
                        case 269: eventLabels[i] = "Come up from tree trunk"; break;
                        case 1556: eventLabels[i] = "Jump on wiggler"; break;
                        case 2496: eventLabels[i] = "Game Start: Mario enters Bowser's Keep"; break;
                // Intro Demo Events
                        case 128: eventLabels[i] = "Intro Demo Start - Call Mario, Run Event 2396"; break;
                        case 2396: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 2357: eventLabels[i] = "Intro Demo - Mushroom Way, Mario Intro"; break;
                        case 129: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 48: eventLabels[i] = "Intro Demo - Battle, Pack 196 Beanstalk BG(2), jump via #129 to #992"; break;
                        case 992: eventLabels[i] = "Intro Demo - Kingdom Castle, talk with Chancellor"; break;
                        case 130: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 1733: eventLabels[i] = "Intro Demo - Bandits Way Intro(Power Star / Level Up)"; break;
                        case 1728: eventLabels[i] = "Intro Demo - Waypoint Event, If Mem $704E:3 then skip 131 and go to #1738"; break;
                        case 131: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 149: eventLabels[i] = "Intro Demo - Battle, Pack 201 Grasslands BG(9), jump via #149 to #1725"; break;
                        case 1725: eventLabels[i] = "Intro Demo - Midas River intro, start #1724 sync, spin mario until level exit"; break;
                        case 1724: eventLabels[i] = "Intro Demo - Wait until $7043:1, then fade out and execute three UNKCMDs before ending side script w/ return"; break;
                        case 132: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 1746: eventLabels[i] = "Intro Demo - Midas River barrel jumping intro"; break;
                        case 2288: eventLabels[i] = "Intro Demo - Mallow Intro"; break;
                        case 135: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 2289: eventLabels[i] = "Intro Demo - Tadpole Pond scene"; break;
                        case 134: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 1723: eventLabels[i] = "Intro Demo - Open Wiggler intro level, 6,32,3 southeast run event no message return"; break;
                        case 1718: eventLabels[i] = "Intro Demo - Level Event, control Mario jumping"; break;
                        case 136: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 150: eventLabels[i] = "Intro Demo - Battle Pack 198 Kero Sewers BG(Dec 21 Hex 15)"; break;
                        case 2397: eventLabels[i] = "Intro Demo - Load Geno Intro Level"; break;
                        case 2366: eventLabels[i] = "Intro Demo - Script Geno Intro Level"; break;
                        case 137: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 993: eventLabels[i] = "Intro Demo - Pipe Vault Intro"; break;
                        case 138: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 151: eventLabels[i] = "Intro Demo - Battle Pack 199 Ship BG(4), goto #994 via #138"; break;
                        case 994: eventLabels[i] = "Intro Demo - Yo'ster Isle intro"; break;
                        case 139: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 995: eventLabels[i] = "Intro Demo - Docaty Railroad Intro"; break;
                        case 1738: eventLabels[i] = "Intro Demo - Load/Do Bowser intro scene"; break;
                        case 1730: eventLabels[i] = "Intro Demo - Waypoint Event, if $704E:3 Then skip #140 and go to #1740"; break;
                        case 140: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 152: eventLabels[i] = "Intro Demo - Battle, Pack 203 Mines BG(5), Then jump via #140 to #2398"; break;
                        case 2398: eventLabels[i] = "Intro Demo - Open Booster Tower with Parachuters at 5,53,0 facing northeast no event no message"; break;
                        case 2365: eventLabels[i] = "Intro Demo - Action Script Above Level"; break;
                        case 142: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 153: eventLabels[i] = "Intro Demo - Battle Pack 202 Birdo BG(Dec 23 / Hex 17), jump -> #1740"; break;
                        case 1740: eventLabels[i] = "Intro Demo - Booster Hill Scene"; break;
                        case 1731: eventLabels[i] = "Intro Demo - Waypoint Event, If $074E:3 skip #143 and go to #1746"; break;
                        case 143: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 154: eventLabels[i] = "Intro Demo - Battle Pack 202 Seaside Town Beach BG (Hex 25 / Dec 37), Goto Event #3839 Via #143"; break;
                        case 3839: eventLabels[i] = "Intro Demo - Sea Shore intro area"; break;
                        case 145: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 155: eventLabels[i] = "Intro Demo - Battle Pack 197 Mountains BG (Dec 10 / Hex A), Goto #997 via #145"; break;
                        case 997: eventLabels[i] = "Intro Demo - Intro level Nimbus Land w/ birdbrains"; break;
                        case 146: eventLabels[i] = "Intro Demo - Waypoint Event"; break;
                        case 156: eventLabels[i] = "Intro Demo - Battle Czar Dragon (204) His BG (8), Goto #2290 Via #146"; break;
                        case 2290: eventLabels[i] = "Intro Demo - Booster Tower, Peach's Intro"; break;
                        case 141: eventLabels[i] = "Intro Demo - Waypoint Event, Goto #2291"; break;
                        case 2291: eventLabels[i] = "Intro Demo - Open Vista Hill level"; break;
                        case 147: eventLabels[i] = "Intro Demo - Waypoint Event, Goto #996"; break;
                        case 996: eventLabels[i] = "Intro Demo - Pipe Vault Area 01 (not faded in), \"In...\" text, Return."; break;
                        default: eventLabels[i] = "";  break;
                    }
                }
                return eventLabels;
            }
            set
            {
                eventLabels = value;
            }
        }
        public static string[] ActionLabels
        {
            get
            {
                if (actionLabels.Length == 1024)
                    return actionLabels;
                actionLabels = new string[1024];
                for (int i = 0; i < actionLabels.Length; i++)
                {
                    switch (i)
                    {
                        case 128: actionLabels[i] = "NPC walks around slowly"; break;
                        default: actionLabels[i] = ""; break;
                    }
                }
                return actionLabels;
            }
            set
            {
                actionLabels = value;
            }
        }


        public static int[][] EventOpcodes = new int[][]
        {
            new int[]   // 0
            {
                0x00,0x30,0x31,0x32,0x39,0x3A,0x3B,0x3D,
                0x3E,0x3F,0x42,0xF2,0xF3,0xF4,0xF5,0xF6,
                0xF7,0xF8,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,
                0xFD
            },
            new int[]   // 1
            {
                0x34,0x35
            },
            new int[]   // 2
            {
                0x36,0x54,0x56,0xFD,0xFD,0xFD
            },
            new int[]   // 3
            {
                0x50,0x51,0x52,0x53,0x57,0xFD,0xFD,0xFD,
                0xFD,0xFD,0xFD,0xFD,0xFD,0xFD
            },
            new int[]   // 4
            {
                0x49,0x4A
            },
            new int[]   // 5
            {
                0x4B,0x68,0x6A,0x6B
            },
            new int[]   // 6
            {
                0x4C,0x4F,0xFB,0xFC,0xFD,0xFD,0xFD
            },
            new int[]   // 7
            {
                0x60,0x61,0x62,0x63,0x64,0x65,0x66,0x67
            },
            new int[]   // 8
            {
                0x40,0x44,0x45,0x46,0x47,0x4E,0xD0,0xD1,
                0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,
                0xFD,0xFD,0xFD
            },
            new int[]   // 9
            {
                0xD2,0xD3,0xD4,0xD5,0xD7,0xF9,0xFA
            },
            new int[]   // 10
            {
                0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,
                0x78,0x79,0x7A,0x7B,0x7C,0x7D,0x7E,0x80,
                0x81,0x82,0x83,0x84,0x89,0x8A,0x87,0x8F,
                0xFD,0xFD
            },
            new int[]   // 11
            {
                0x90,0x91,0x92,0x93,0x94,0x95,0x97,0x98,
                0x9B,0x9C,0x9D,0x9E,0xFD,0xFD,0xFD,0xFD,
                0xFD,0xFD
            },
            new int[]   // 12
            {
                0xA0,0xA4,0xA8,0xA9,0xAA,0xAB,0xB0,0xB1,
                0xB2,0xB3,0xB5,0xB7,0xBB,0xBC,0xBD,0xC2,
                0xD6,0xD8,0xDC,0xE0,0xE1,0xE4,0xE5,0xE8,
                0xE9,0xFD,0xFD
            },
            new int[]   // 13
            {
                0x37,0x38,0x55,0x58,0xA3,0xA7,0xAC,0xAD,
                0xAE,0xAF,0xB4,0xB6,0xB8,0xB9,0xBA,0xC0,
                0xC1,0xC3,0xC4,0xC5,0xC6,0xC7,0xC8,0xC9,
                0xCA,0xCB,0xDB,0xDF,0xE2,0xE3,0xE6,0xE7,
                0xEA,0xEB,0xEC,0xED,0xEE,0xEF,0xFD,0xFD,
                0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,
                0xFD,0xFD,0xFD
            },
            new int[]   // 14
            {
                0x5B,0x7F,0xF0,0xF1,0xFD,0xFD
            },
            new int[]   // 15
            {
                0xFE,0xFF
            }
        };
        public static int[][] EventParams = new int[][]
        {
            new int[]   // 0
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x32,0x33,0x34,0x3D,0x3E,0xF0,
                0xF9
            },
            new int[]   // 1
            {
                0x00,0x00
            },
            new int[]   // 2
            {
                0x00,0x00,0x00,0x4B,0x5B,0x64
            },
            new int[]   // 3
            {
                0x00,0x00,0x00,0x00,0x00,0x50,0x51,0x52,
                0x53,0x54,0x55,0x56,0x57,0x5C
            },
            new int[]   // 4
            {
                0x00,0x00
            },
            new int[]   // 5
            {
                0x00,0x00,0x00,0x00
            },
            new int[]   // 6
            {
                0x00,0x00,0x00,0x00,0x4A,0x4C,0x65
            },
            new int[]   // 7
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            },
            new int[]   // 8
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x43,0x46,0x4D,0x4E,0x4F,0x66,0x67,0xF8,
                0x40,0x41,0x42
            },
            new int[]   // 9
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00
            },
            new int[]   // 10
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x30,0x31
            },
            new int[]   // 11
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x94,0x96,0x97,0x9C,
                0xA4,0xA5
            },
            new int[]   // 12
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0xB6,0xB7
            },
            new int[]   // 13
            {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x58,0x59,
                0x5A,0x5D,0x5E,0xAC,0xB0,0xB1,0xB2,0xB3,
                0xB4,0xB5,0xB8
            },
            new int[]   // 14
            {
                0x00,0x00,0x00,0x00,0x60,0x61
            },
            new int[]   // 15
            {
                0x00,0x00
            }
        };
        public static int[][] ActionOpcodes = new int[][]
            {
                new int[]   // 0
                {
                    0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
                    0x0A,0x0B,0x0C,0x13,0x15,0x3D,0x09,0xFD,
                    0xFD,0xFD,0xFD
                },
                new int[]   // 1
                {
                    0x0D,0x0E,0x0F
                },
                new int[]   // 2
                {
                    0x08,0x10,0x3A,0x3B,0x3E,0x3F,0xD0
                },
                new int[]   // 3
                {
                    0x26,0x27,0x28
                },
                new int[]   // 4
                {
                    0x40,0x41,0x42,0x43,0x44,0x45,0x46,0x47,
                    0x48,0x4A,0x4B
                },
                new int[]   // 5
                {
                    0x50,0x51,0x52,0x53,0x54,0x55,0x56,0x57,
                    0x58,0x59,0x5A,0x5B,0x5C,0x5D,0x7E,0x7F
                },
                new int[]   // 6
                {
                    0x60,0x61,0x62,0x63,0x64,0x65,0x66,0x67,
                    0x68,0x69,0x6A,0x6B
                },
                new int[]   // 7
                {
                    0x70,0x71,0x72,0x73,0x74,0x75,0x76,0x77,
                    0x78,0x79,0x7A,0x7B,0x7C,0x7D
                },
                new int[]   // 8
                {
                    0x80,0x81,0x82,0x83,0x84,0x87,0x90,0x91,
                    0x92,0x93,0x94,0x95,0x88,0x89,0x98,0x99
                },
                new int[]   // 9
                {
                    0x9B,0x9C,0x9D,0x9E,0xFD
                },
                new int[]   // 10
                {
                    0xA0,0xA4,0xA8,0xA9,0xAA,0xAB,0xB0,0xB1,
                    0xB2,0xB3,0xB5,0xB7,0xBB,0xBC,0xBD,0xC2,
                    0xD6,0xD8,0xDC,0xE0,0xE1,0xE4,0xE5,0xE8,
                    0xE9,0xFD
                },
                new int[]   // 11
                {
                    0xA3,0xA7,0xAC,0xAD,0xAE,0xAF,0xB4,0xB6,
                    0xB8,0xB9,0xBA,0xC0,0xC1,0xC3,0xC4,0xC5,
                    0xC6,0xCA,0xCB,0xDB,0xDF,0xE2,0xE3,0xE6,
                    0xE7,0xEA,0xEB,0xEC,0xED,0xEE,0xEF,0xFD,
                    0xFD,0xFD,0xFD,0xFD,0xFD
                },
                new int[]   // 12
                {
                    0xD2,0xD3,0xD4,0xD7
                },
                new int[]   // 13
                {
                    0xF2,0xF3,0xF4,0xF4,0xF6,0xF7,0xF8,0xFD,
                    0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,0xFD,
                    0xFD,0xFD
                },
                new int[]   // 14
                {
                    0xF0,0xF1
                },
                new int[]   // 15
                {
                    0xFE,0xFF
                }
            };
        public static int[][] ActionParams = new int[][]
            {
                new int[]   // 0
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,
                    0x02,0x03,0x0F
                },
                new int[]   // 1
                {
                    0x00,0x00,0x00
                },
                new int[]   // 2
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00
                },
                new int[]   // 3
                {
                    0x00,0x00,0x00
                },
                new int[]   // 4
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00
                },
                new int[]   // 5
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                },
                new int[]   // 6
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00
                },
                new int[]   // 7
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00
                },
                new int[]   // 8
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                },
                new int[]   // 9
                {
                    0x00,0x00,0x00,0x00,0x9E
                },
                new int[]   // 10
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0xB6
                },
                new int[]   // 11
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xB0,
                    0xB1,0xB2,0xB3,0xB4,0xB5
                },
                new int[]   // 12
                {
                    0x00,0x00,0x00,0x00
                },
                new int[]   // 13
                {
                    0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x04,
                    0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0C,
                    0x0D,0x0E
                },
                new int[]   // 14
                {
                    0x00,0x00
                },
                new int[]   // 15
                {
                    0x00,0x00
                },
            };
        public static string[] EventNames(int index)
        {
            switch (index)
            {
                case 0:
                    return new string[] 
                    {
                    "Objects (allies, NPCs, screens)...",      // 0x00 - 0x2F
                    "Freeze all NPCs until return",			// 0x30
                    "Unfreeze all NPCs",			// 0x31
                    "If object {xx} present in current level...", // 0x32
                    "If Mario on top of object {xx}...",    // 0x39
                    "If object A & B < (x,y) steps and infinite Z coords apart...",			// 0x3A
                    "If object A & B < (x,y,z) steps apart...",			// 0x3B
                    "If Mario is in the air...",			// 0x3D
                    "Create NPC @ object {xx}'s (x,y,z)...",			// 0x3E
                    "Create NPC @ (x,y,z) of $7010-15...",			// 0x3F
                    "If Mario is on top of an object...",			// 0x42
                    "Object {xx}'s presence in level {xx} is...",			// 0xF2
                    "Object {xx}'s event trigger in level {xx} is...",			// 0xF3
                    "Summon object @ $70A8 to current level",			// 0xF4
                    "Remove object @ $70A8 in current level",			// 0xF5
                    "Enable event trigger for object @ 70A8",			// 0xF6
                    "Disable event trigger for object @ 70A8",			// 0xF7
                    "If object {xx} is present in level {xx}...",			// 0xF8
                    /********FD OPTIONS********/
                    "Remember last object",			// 0x32
                    "If object {xx}'s action script running...",			// 0x33
                    "If object {xx} is underwater...",			// 0x34
                    "If object {xx} is in the air...",			// 0x3D
                    "Create NPC + event {xx} @ (x,y,z) of $7010-15...",			// 0x3E
                    "If object {xx}'s event trigger in level {xx} is...",  // 0xF0
                    "Mario glows"			// 0xF9
                                        };
                case 1:
                    return new string[] 
                    {
                    "Enable buttons {xx} only, reset @ return...",			// 0x34
                    "Enable buttons {xx} only...",			// 0x35
                                        };
                case 2:
                    return new string[] 
                    { 
                    "Add or remove character {xx} in party...",			// 0x36
                    "Equip item {xx} to character {xx}...",			// 0x54
                    "Character {xx}'s HP -= memory $7000",			// 0x56
                    /********FD OPTIONS********/
                    "Experience += experience packet",			// 0x4B
                    "Restore all HP",			// 0x5B
                    "Experience packet = memory $7000"			// 0x64
                    };
                case 3:
                    return new string[] 
                    { 
                    "Store 1 of item {xx} to inventory...",			// 0x50
                    "Remove 1 of item {xx} from inventory...",			// 0x51
                    "Coins += ...",			// 0x52
                    "Frog coins += ...",			// 0x53
                    "FP -= memory $7000",			// 0x57
                    /********FD OPTIONS********/
                    "Store memory $70A7 to item inventory",			// 0x50
                    "Store memory $70A7 to equipment inventory",			// 0x51
                    "Coins += memory $7000",			// 0x52
                    "Coins -= memory $7000",			// 0x53
                    "Frog coins += memory $7000",			// 0x54
                    "Frog coins -= memory $7000",			// 0x55
                    "Current FP += memory $7000",			// 0x56
                    "Maximum FP += memory $7000",			// 0x57
                    "Restore all FP"			// 0x5C
                    };
                case 4:
                    return new string[] 
                    { 
                    "Engage in battle with pack @ $700E",			// 0x49
                    "Engage in battle with pack {xx}..."			// 0x4A
                    };
                case 5:
                    return new string[] 
                    { 
                    "Open location...",			// 0x4B
                    "Open level...",			// 0x68
                    "Apply tile mod to level...",			// 0x6A
                    "Apply solid mod to level..."			// 0x6B
                    };
                case 6:
                    return new string[] 
                    { 
                    "Open shop menu...",			// 0x4C
                    "Open menu/run event sequence...",			// 0x4F
                    "Reset game, choose game",			// 0xFB
                    "Reset game",			// 0xFC
                    /********FD OPTIONS********/
                    "Open save game menu",			// 0x4A
                    "Run menu tutorial...",			// 0x4C
                    "Run level-up bonus sequence"			// 0x65
                    };
                case 7:
                    return new string[] 
                    { 
                    "Run dialogue...",			// 0x60
                    "Run dialogue @ memory $7000...",			// 0x61
                    "Run dialogue for {xx} duration...",			// 0x62
                    "Append to dialogue @ memory $7000...",			// 0x63
                    "Close dialogue",			// 0x64
                    "Un-sync dialogue",			// 0x65
                    "If dialogue option B selected...",			// 0x66
                    "If dialogue option B / C selected..."			// 0x67
                    };
                case 8:
                    return new string[] 
                    { 
                    "Run background event...",			// 0x40
                    "Run background event, pause...",         // 0x44
                    "Run background event, pause (return on exit)...",         // 0x45
                    "Stop background event...",  // 0x46
                    "Resume background event...", // 0x47
                    "Run event sequence...",			// 0x4E
                    "Run event...",			// 0xD0
                    "Run event as subroutine...",			// 0xD1
                    /********FD OPTIONS********/
                    "Stop all background events", // 0x43
                    "Run event at return...",  // 0x46
                    "Run star piece sequence...",			// 0x4D
                    "Run moleville mountain sequence",			// 0x4E
                    "Run moleville mountain intro sequence",			// 0x4F
                    "Display pre-game intro title...",			// 0x66
                    "Run ending credit sequence",			// 0x67
                    "Exor crashes into keep",			// 0xF8
                    "Move script to main thread",    // 0x40
                    "Move script to background thread 1",    // 0x41
                    "Move script to background thread 2"    // 0x42
                    };
                case 9:
                    return new string[] 
                    { 
                    "Jump to address...",			// 0xD2
                    "Jump to subroutine...",			// 0xD3
                    "Loop start, count = ...",			// 0xD4
                    "Loop start, timer = ...",          // 0xD5
                    "Loop end",			// 0xD7
                    "Jump to start of script",			// 0xF9
                    "Jump to start of script"			// 0xFA
                    };
                case 10:
                    return new string[] 
                    {
                    "Fade in from black (sync)",			// 0x70
                    "Fade in from black (async)",			// 0x71
                    "Fade in from black (sync) for {xx} duration...",			// 0x72
                    "Fade in from black (async) for {xx} duration...",			// 0x73
                    "Fade out to black (sync)",			// 0x74
                    "Fade out to black (async)",			// 0x75
                    "Fade out to black (sync) for {xx} duration...",			// 0x76
                    "Fade out to black (async) for {xx} duration...",			// 0x77
                    "Fade in from {xx} color...",			// 0x78
                    "Fade out to {xx} color...",			// 0x79
                    "Star mask, expand from screen center",			// 0x7A
                    "Star mask, shrink to screen center",			// 0x7B
                    "Circle mask, expand from screen center",			// 0x7C
                    "Circle mask, shrink to screen center",			// 0x7D
                    "Initiate battle mask",			// 0x7E
                    "Tint layers {xx} with {xx} color...",			// 0x80
                    "Priority set = ...",			// 0x81
                    "Reset priority set",			// 0x82
                    "Screen flashes with {xx} color...",			// 0x83
                    "Pixellate layers {xx} by {xx} amount...",			// 0x84
                    "Palette set morphs to {xx} set...",			// 0x89
                    "Palette set = ...",			// 0x8A
                    "Circle mask, shrink to object {xx} (non-static)...", // 0x87
                    "Circle mask, shrink to object {xx} (static)...",	    // 0x8F
                    /********FD OPTIONS********/
                    "Unfreeze screen",			// 0x30
                    "Freeze screen"			// 0x31
                    };
                case 11:
                    return new string[] 
                    {
                    "Play music {xx} at current volume...",			// 0x90
                    "Play music {xx} at default volume...",			// 0x91
                    "Fade in music {xx} ...",			// 0x92
                    "Fade out current music",			// 0x93
                    "Stop current music",			// 0x94
                    "Fade out current music to {xx} volume...",			// 0x95
                    "Adjust music tempo by {xx} amount...",			// 0x97
                    "Adjust music pitch by {xx} amount...",			// 0x98
                    "Stop current sound",			// 0x9B
                    "Play {xx} sound (ch.6,7)...",			// 0x9C
                    "Play {xx} sound (ch.6,7) with {xx} speaker balance...",			// 0x9D
                    "Fade out current sound to {xx} volume...",			// 0x9E
                    /********FD OPTIONS********/
                    "Deactivate {xx} sound channels...",        // 0x94
                    "If audio memory $69 >= ...",        // 0x96
                    "If audio memory $69 = ...",        // 0x97
                    "Play {xx} sound (ch.4,5)...",             // 0x9E
                    "Lower current music tempo",			// 0xA4
                    "Slide current music tempo to default"			// 0xA5
                    };
                case 12:
                    return new string[] 
                    { 
                    "Memory $704x bit {xx} set...",			// 0xA0-0xA2
                    "Memory $704x bit {xx} clear...",			// 0xA4-0xA6
                    "Memory $70Ax = ...",			// 0xA8
                    "Memory $70Ax += ...",			// 0xA9
                    "Memory $70Ax += 1...",			// 0xAA
                    "Memory $70Ax -= 1...",			// 0xAB
                    "Memory $7xxx = ...",         // 0xB0
                    "Memory $7xxx += ...",			// 0xB1
                    "Memory $7xxx += 1...",// 0xB2
                    "Memory $7xxx -= 1...",// 0xB3
                    "Memory $70Ax = memory $7000...",         // 0xB5
                    "Memory $7xxx = random # between 0 and {xx}...",			// 0xB7
                    "Memory $7xxx = memory $7000...",        // 0xBB
                    "Memory $7xxx = memory $7xxx...",      // 0xBC
                    "Memory $7xxx <=> memory $7xxx...",			// 0xBD
                    "Memory $7xxx compare to {xx}...",			// 0xC2
                    "Object memory = memory $7xxx...",   // 0xD6
                    "If memory $704x bit {xx} set...",			// 0xD8-0xDA
                    "If memory $704x bit {xx} clear...",// 0xDC-0xDE
                    "If memory $70Ax = ...",			// 0xE0
                    "If memory $70Ax != ...",			// 0xE1
                    "If memory $7xxx = ...",			// 0xE4
                    "If memory $7xxx != ...",			// 0xE5
                    "If random # between 0 and 255 > 128...",			// 0xE8
                    "If random # between 0 and 255 > 66...",			// 0xE9
                    /********FD OPTIONS********/
                    "Memory $7xxx shift left {xx} times...",			// 0xB6
                    "Generate random # between 0 and memory $7xxx..."			// 0xB7
                    };
                case 13:
                    return new string[] 
                    {
                    "Memory $7000 = party capacity",			// 0x37
                    "Memory $7000 = character in {xx} slot...",			// 0x38
                    "Memory $7000 = # of open item slots",			// 0x55
                    "Memory $7000 = current FP",			// 0x58
                    "Memory $704x [x is @ $7000] bit {xx} set...",			// 0xA3
                    "Memory $704x [x is @ $7000] bit {xx} clear...",			// 0xA7
                    "Memory $7000 = ...",			// 0xAC
                    "Memory $7000 += ...",			// 0xAD
                    "Memory $7000 += 1",			// 0xAE
                    "Memory $7000 -= 1",			// 0xAF
                    "Memory $7000 = memory $70Ax...",			// 0xB4
                    "Memory $7000 = random # between 0 and {xx}...",			// 0xB6
                    "Memory $7000 += memory $7xxx...",			// 0xB8
                    "Memory $7000 -= memory $7xxx...",			// 0xB9
                    "Memory $7000 = memory $7xxx...",			// 0xBA
                    "Memory $7000 compare to {xx}...",			// 0xC0
                    "Memory $7000 compare to $7xxx...",			// 0xC1
                    "Memory $7000 = current level",			// 0xC3
                    "Memory $7000 = object's X coord...",			// 0xC4
                    "Memory $7000 = object's Y coord...",			// 0xC5
                    "Memory $7000 = object's Z coord...",			// 0xC6
                    "Memory $7010-15 = (x,y,z) of object...",          // 0xC7
                    "Memory $7016-1B = (x,y,z) of object...",          // 0xC8
                    "Memory $7000 = F coord of object...",            // 0xC9
                    "Memory $7000 = pressed button",			// 0xCA
                    "Memory $7000 = tapped button",			// 0xCB
                    "If Memory $704x [x @ $7000] bit {xx} set...",			// 0xDB
                    "If Memory $704x [x @ $7000] bit {xx} clear...",			// 0xDF
                    "If memory $7000 =...",			// 0xE2
                    "If memory $7000 !=...",			// 0xE3
                    "If memory $7000 all bits {xx} clear...",			// 0xE6
                    "If memory $7000 any bits {xx} set...",			// 0xE7
                    "If loaded memory = 0...",			// 0xEA
                    "If loaded memory != 0...",			// 0xEB
                    "If comparison result is: >=...",			// 0xEC
                    "If comparison result is: <...",			// 0xED
                    "If loaded memory < 0...",			// 0xEE
                    "If loaded memory >= 0...",			// 0xEF
                    /********FD OPTIONS********/
                    "Memory $7000 = quantity of item {xx} in inventory...",			// 0x58
                    "Memory $7000 = coins",			// 0x59
                    "Memory $7000 = frog coins",			// 0x5A
                    "Memory $7000 = equipment {xx} of {xx} character...",			// 0x5D
                    "Memory $70A7 = quantity of item @ memory $7000",			// 0x5E
                    "Memory $7000 = memory $7Fxxxx...",			// 0xAC
                    "Memory $7000 &= {xx}...",			// 0xB0
                    "Memory $7000 |= {xx}...",			// 0xB1
                    "Memory $7000 ^= {xx}...",			// 0xB2
                    "Memory $7000 &= memory $7xxx...",			// 0xB3
                    "Memory $7000 |= memory $7xxx...",			// 0xB4
                    "Memory $7000 ^= memory $7xxx...",			// 0xB5
                    "Memory $7000 = Moleville Mountain timer"			// 0xB8
                    };
                case 14:
                    return new string[] 
                    { 
                    "Pause script if menu open",           // 0x5B
                    "Pause script until screen effect done", // 0x7F
                    "Pause script for {xx} frames...",			// 0xF0
                    "Pause script for {xxxx} frames...",			// 0xF1
                    /********FD OPTIONS********/
                    "Pause script, resume on next dialogue page A",			// 0x60
                    "Pause script, resume on next dialogue page B",			// 0x61
                    };
                case 15:
                    return new string[] 
                    {
                    "Return",			// 0xFE
                    "Return all"			// 0xFF
                    };
                default:
                    return new string[] { };
            }
        }
        public static string[] ActionNames(int index)
        {
            switch (index)
            {
                case 0:
                    return new string[] 
                    { 
                    "Visibility on",			// 0x00
                    "Visibility off",			// 0x01
                    "Sequence playback on",			// 0x02
                    "Sequence playback off",			// 0x03
                    "Sequence looping on",			// 0x04
                    "Sequence looping off",			// 0x05
                    "Fixed F coord on",			// 0x06
                    "Fixed F coord off",			// 0x07
                    "Solidity bits = ...",			// 0x0A
                    "Solidity set {xx} bits...",			// 0x0B
                    "Solidity clear {xx} bits...",			// 0x0C
                    "VRAM priority = ...",			// 0x13
                    "Movement set {xx} bits...",			// 0x15
                    "If in air...",			// 0x3D
                    "Reset properties",			// 0x09
                    /********FD OPTIONS********/
                    "Shadow on/off",			// 0x00 + 0x01
                    "Floating on",			// 0x02
                    "Floating off",			// 0x03
                    "Priority = ...",			// 0x0F
                    };
                case 1:
                    return new string[] 
                    { 
                    "Palette row = ...",			// 0x0D
                    "Palette row += ...",			// 0x0E
                    "Palette row += 1",			// 0x0F
                    };
                case 2:
                    return new string[] 
                    { 
                    "Animation/sequence = ...",			// 0x08
                    "Walking/sequence speed = ...",			// 0x10
                    "If object A & B < (x,y) steps apart...",			// 0x3A
                    "If object A & B < (x,y) steps apart & same Z coord...",	// 0x3B
                    "Create NPC @ object {xx}'s (x,y,z)...",			// 0x3E
                    "Create NPC @ (x,y,z) of $7010-15...",			// 0x3F
                    "Action script = ...",			// 0xD0
                    };
                case 3:
                    return new string[] 
                    { 
                    "Embedded animation routine ($26)...",			// 0x26
                    "Embedded animation routine ($27)...",			// 0x27
                    "Embedded animation routine ($28)...",			// 0x28
                    };
                case 4:
                    return new string[] 
                    { 
                    "Walk 1 step east",			// 0x40
                    "Walk 1 step southeast",			// 0x41
                    "Walk 1 step south",			// 0x42
                    "Walk 1 step southwest",			// 0x43
                    "Walk 1 step west",			// 0x44
                    "Walk 1 step northwest",			// 0x45
                    "Walk 1 step north",			// 0x46
                    "Walk 1 step northeast",			// 0x47
                    "Walk 1 step in F direction",			// 0x48
                    "Z coord += 1 step",			// 0x4A
                    "Z coord -= 1 step",			// 0x4B
                    };
                case 5:
                    return new string[] 
                    { 
                    "Walk {xx} steps east...",			// 0x50
                    "Walk {xx} steps southeast...",			// 0x51
                    "Walk {xx} steps south...",			// 0x52
                    "Walk {xx} steps southwest...",			// 0x53
                    "Walk {xx} steps west...",			// 0x54
                    "Walk {xx} steps northwest...",			// 0x55
                    "Walk {xx} steps north...",			// 0x56
                    "Walk {xx} steps northeast...",			// 0x57
                    "Walk {xx} steps in F direction...",			// 0x58
                    "Walk 20 steps in F direction...",			// 0x59
                    "Z coord += {xx} steps...",			// 0x5A
                    "Z coord -= {xx} steps...",			// 0x5B
                    "Z coord += 20 steps",			// 0x5C
                    "Z coord -= 20 steps",			// 0x5D
                    "Jump at {xx} velocity...",			// 0x7E
                    "Jump (+SFX) at {xx} velocity...",			// 0x7F
                    };
                case 6:
                    return new string[] 
                    { 
                    "Walk {xx} pixels east...",			// 0x60
                    "Walk {xx} pixels southeast...",			// 0x61
                    "Walk {xx} pixels south...",			// 0x62
                    "Walk {xx} pixels southwest...",			// 0x63
                    "Walk {xx} pixels west...",			// 0x64
                    "Walk {xx} pixels northwest...",			// 0x65
                    "Walk {xx} pixels north...",			// 0x66
                    "Walk {xx} pixels northeast...",			// 0x67
                    "Walk {xx} pixels in F direction...",			// 0x68
                    "Walk 16 pixels in F direction",			// 0x69
                    "Z coord += {xx} pixels...",			// 0x6A
                    "Z coord -= {xx} pixels...",			// 0x6B
                    };
                case 7:
                    return new string[] 
                    { 
                    "Face east",			// 0x70
                    "Face southeast",			// 0x71
                    "Face south",			// 0x72
                    "Face southwest",			// 0x73
                    "Face west",			// 0x74
                    "Face northwest",			// 0x75
                    "Face north",			// 0x76
                    "Face northeast",			// 0x77
                    "Face Mario",			// 0x78
                    "Turn clockwise 45°",			// 0x79
                    "Turn in random direction",			// 0x7A
                    "Turn clockwise 45° {xx} times...",			// 0x7B
                    "Face east",			// 0x7C
                    "Face southwest",			// 0x7D
                    };
                case 8:
                    return new string[] 
                    { 
                    "Walk to (x,y)...",			// 0x80
                    "Walk (x,y) steps...",			// 0x81
                    "Transfer to (x,y)...",			// 0x82
                    "Transfer (x,y) steps...",			// 0x83
                    "Transfer (x,y) pixels...",			// 0x84
                    "Transfer to (x,y) of object...",			// 0x87
                    "Bounce to (x,y)...",			// 0x90
                    "Bounce (x,y) steps...",			// 0x91
                    "Transfer to (x,y,z)...",			// 0x92
                    "Transfer (x,y,z) steps...",			// 0x93
                    "Transfer (x,y,z) pixels...",			// 0x94
                    "Transfer to (x,y,z) of object...",			// 0x95
                    "Walk to (x,y) of Mem $7016-1B",    // 0x88
                    "Transfer to (x,y) of Mem $7016-1B",    // 0x89
                    "Walk to (x,y,z) of Mem $7016-1B",    // 0x98
                    "Transfer to (x,y,z) of Mem $7016-1B",    // 0x99
                    };
                case 9:
                    return new string[] 
                    { 
                    "Stop current sound",			// 0x9B
                    "Play sound: {xx} (ch.6,7)...",			// 0x9C
                    "Play sound: {xx} (ch.6,7), speaker balance {xx}...",			// 0x9D
                    "Fade out current sound to volume {xx}...",			// 0x9E
                    "Play sound: {xx} (ch.4,5)...",			// 0x9C
                    };
                case 10:
                    return new string[] 
                    { 
                    "Memory $704x bit {xx} set...",			// 0xA0-0xA2
                    "Memory $704x bit {xx} clear...",			// 0xA4-0xA6
                    "Memory $70Ax = ...",			// 0xA8
                    "Memory $70Ax += ...",			// 0xA9
                    "Memory $70Ax += 1...",// 0xAA
                    "Memory $70Ax -= 1...",// 0xAB
                    "Memory $7xxx = ...",         // 0xB0
                    "Memory $7xxx += ...",			// 0xB1
                    "Memory $7xxx += 1...",// 0xB2
                    "Memory $7xxx -= 1...",// 0xB3
                    "Memory $70Ax = memory $700C...",         // 0xB5
                    "Memory $7xxx = random # between 0 and {xx}...",			// 0xB7
                    "Memory $7xxx = memory $700C...",        // 0xBB
                    "Memory $7xxx = memory $7xxx...",      // 0xBC
                    "Memory $7xxx <=> memory $7xxx...",			// 0xBD
                    "Memory $7xxx compare to...",			// 0xC2
                    "Memory $7xxx load...",   // 0xD6
                    "If memory $704x bit {xx} set...",			// 0xD8-0xDA
                    "If memory $704x bit {xx} clear...",// 0xDC-0xDE
                    "If memory $70Ax = ...",			// 0xE0
                    "If memory $70Ax != ...",			// 0xE1
                    "If memory $7xxx = ...",			// 0xE4
                    "If memory $7xxx != ...",			// 0xE5
                    "If random # between 0 and 255 > 128...",			// 0xE8
                    "If random # between 0 and 255 > 66...",			// 0xE9
                    /********FD OPTIONS********/
                    "Memory $7xxx shift left {xx} times...",			// 0xB6
                    };
                case 11:
                    return new string[] 
                    { 
                    "Memory $704x [x is @ $700C] bit set",			// 0xA3
                    "Memory $704x [x is @ $700C] bit clear",			// 0xA7
                    "Memory $700C = ...",			// 0xAC
                    "Memory $700C += ...",			// 0xAD
                    "Memory $700C += 1",			// 0xAE
                    "Memory $700C -= 1",			// 0xAF
                    "Memory $700C = memory $70Ax...",			// 0xB4
                    "Memory $700C = random # between 0 and {xx}...",			// 0xB6
                    "Memory $700C += memory $7xxx...",			// 0xB8
                    "Memory $700C -= memory $7xxx...",			// 0xB9
                    "Memory $700C = memory $7xxx...",			// 0xBA
                    "Memory $700C compare to {xx}...",			// 0xC0
                    "Memory $700C compare to memory $7xxx...",			// 0xC1
                    "Memory $700C = current level",			// 0xC3
                    "Memory $700C = object's X coord...",			// 0xC4
                    "Memory $700C = object's Y coord...",			// 0xC5
                    "Memory $700C = object's Z coord...",			// 0xC6
                    "Memory $700C = pressed button",			// 0xCA
                    "Memory $700C = tapped button",			// 0xCB
                    "If Memory $704x [x @ $700C] bit set...",			// 0xDB
                    "If Memory $704x [x @ $700C] bit clear...",			// 0xDF
                    "If memory $700C =...",			// 0xE2
                    "If memory $700C !=...",			// 0xE3
                    "If memory $700C all bits {xx} clear...",			// 0xE6
                    "If memory $700C any bits {xx} set...",			// 0xE7
                    "If loaded memory = 0...",			// 0xEA
                    "If loaded memory != 0...",			// 0xEB
                    "If comparison result is: >=...",			// 0xEC
                    "If comparison result is: <...",			// 0xED
                    "If loaded memory < 0...",			// 0xEE
                    "If loaded memory >= 0...",			// 0xEF
                    /********FD OPTIONS********/
                    "Memory $700C &= {xx}...",			// 0xB0
                    "Memory $700C |= {xx}...",			// 0xB1
                    "Memory $700C ^= {xx}...",			// 0xB2
                    "Memory $700C &= memory $7xxx...",			// 0xB3
                    "Memory $700C |= memory $7xxx...",			// 0xB4
                    "Memory $700C ^= memory $7xxx...",			// 0xB5
                    };
                case 12:
                    return new string[] 
                    { 
                    "Jump to address...",			// 0xD2
                    "Jump to subroutine...",			// 0xD3
                    "Loop start, count = ...",			// 0xD4
                    "Loop end",			// 0xD7
                    };
                case 13:
                    return new string[] 
                    { 
                    "Object {xx}'s presence in level {xx} is...",			// 0xF2
                    "Object {xx}'s event trigger is...",			// 0xF3
                    "Summon object @ $70A8 to current level",			// 0xF4
                    "Remove object @ $70A8 in current level",			// 0xF5
                    "Enable event trigger for object @ 70A8",			// 0xF6
                    "Disable event trigger for object @ 70A8",			// 0xF7
                    "If object {xx} is present in level {xx}...",			// 0xF8
                    /********FD OPTIONS********/
                    "Object memory $0E set bit 4",			// 0x04
                    "Object memory $0E clear bit 4",			// 0x05
                    "Object memory $0E set bit 5",			// 0x06
                    "Object memory $0E clear bit 5",			// 0x07
                    "Object memory $09 set bit 7",			// 0x08
                    "Object memory $09 clear bit 7",			// 0x09
                    "Object memory $08 set bit 4",			// 0x0A
                    "Object memory $08 clear bit 3,4",			// 0x0B
                    "Object memory $30 clear bit 4",			// 0x0C
                    "Object memory $30 set bit 4",			// 0x0D
                    "Object memory $09 clear bit 4,6, set bit 5",			// 0x0E
                    };
                case 14:
                    return new string[] 
                    { 
                    "Pause script for {xx} frames...",			// 0xF0
                    "Pause script for {xxxx} frames...",			// 0xF1
                    };
                case 15:
                    return new string[] 
                    { 
                    "Return queue",			// 0xFE
                    "Return all"			// 0xFF
                    };
                default:
                    return new string[] { };
            }
        }
        #endregion
        #region Keystrokes
        public static string[] Keystrokes = new string[]
        {
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            " ","!","“","”","", "", "‘","’","(",")","", "", ",","-",".","/",
			"0","1","2","3","4","5","6","7","8","9","~","", "", "", "", "?",
            "@","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
			"P","Q","R","S","T","U","V","W","X","Y","Z","", "", "", "", "", 
            "", "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o",
			"p","q","r","s","t","u","v","w","x","y","z","", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", ":",";",
			"<",">","", "#","+","×","%","", "", "", "*","'","&","", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
        };
        public static string[] KeystrokesMenu = new string[]
        {
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            " ","", "", "", "", "", "", "", "", "", ".",":",",","", ".","/",
            "0","1","2","3","4","5","6","7","8","9","~","", "", "", "", "?",
            "", "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
            "P","Q","R","S","T","U","V","W","X","Y","Z","", "", "", "", "", 
            "", "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o",
            "p","q","r","s","t","u","v","w","x","y","z","!","#","-","’","", 
            ":",".","“","”","", "", "", "", "", "", "", "", "", "", ":",";",
            "<",">","", "#","+","×","%","", "", "", "*","'","", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
        };
        public static string[] KeystrokesDesc = new string[]
        {
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
			"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            " ","!","“","”","", "", "‘","’","(",")","", "", ",","-",".","/",
            "0","1","2","3","4","5","6","7","8","9","~","", "", "", "", "?",
            "", "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O",
            "P","Q","R","S","T","U","V","W","X","Y","Z","", "", "", "", "", 
            "", "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o",
            "p","q","r","s","t","u","v","w","x","y","z","", "", "-","’","", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "&","", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
        };
        public static string[] KeystrokesBonus = new string[]
        {
			"G","H","E","F","C","D","A","B","I","J","K","L","M","N","O","P",
			"W","X","U","V","S","T","Q","R","Y","Z","!","?",".","*","@",""
        };
        #endregion
        #endregion
        #region Functions
        // numerize
        public static string Numerize(string item, int index, int length)
        {
            return "{" + index.ToString("d" + length) + "}  " + item;
        }
        public static string Numerize(string[] list, int index, int length)
        {
            return "{" + index.ToString("d" + length) + "}  " + list[index];
        }
        public static string Numerize(string[] list, int index)
        {
            if (index >= list.Length)
                return "ERROR: OUT OF BOUNDS INDEX";
            int length = (list.Length - 1).ToString().Length;
            return "{" + index.ToString("d" + length) + "}  " + list[index];
        }
        public static string[] Numerize(int length, string[] list)
        {
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] Numerize(string[] list)
        {
            int length = (list.Length - 1).ToString().Length;
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] NumerizeHex(string[] list)
        {
            int length = (list.Length - 1).ToString().Length;
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                temp[i] = "{" + i.ToString("X" + length).Substring(1) + "}  ";
                if (!list[i].StartsWith("{"))
                    temp[i] += list[i];
            }
            return temp;
        }

        public static string[] Numerize(int start, int end, string[] list)
        {
            int length = (list.Length - 1).ToString().Length;
            string[] temp = new string[end - start];
            for (int i = start; i < list.Length && i < end; i++)
                temp[i - start] = "{" + i.ToString("d" + length) + "}  " + list[i];
            return temp;
        }
        public static string[] Numerize(StringCollection list)
        {
            return Numerize(Convert(list));
        }
        public static string Numerize(StringCollection list, int index)
        {
            return Numerize(Convert(list), index);
        }
        public static string[] Numerize(object[] list)
        {
            return Numerize(Convert(list));
        }
        public static string RemoveTag(string item)
        {
            if (item.StartsWith("{"))
            {
                while (item.Length > 0 && !item.StartsWith(" "))
                    item = item.Remove(0, 1);
                item = item.Trim();
            }
            return item;
        }
        // conversion
        public static string[] Convert(StringCollection list)
        {
            string[] temp = new string[list.Count];
            list.CopyTo(temp, 0);
            return temp;
        }
        public static string[] Convert(object[] list)
        {
            string[] temp = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
                temp[i] = list[i].ToString();
            return temp;
        }
        /// <summary>
        /// Converts any array to a string array.
        /// </summary>
        /// <param name="list">The array to convert.</param>
        /// <param name="length">The number of elements that the string array will contain.</param>
        /// <param name="startIndex">The index of each string to start at.</param>
        /// <returns></returns>
        public static string[] Convert(object[] list, int length, int startIndex)
        {
            string[] temp = new string[length];
            for (int i = 0; i < list.Length && i < length; i++)
                temp[i] = list[i].ToString().Substring(startIndex);
            return temp;
        }
        public static string[] Convert(object[] list, int length)
        {
            return Convert(list, length, 0);
        }
        public static string[] Convert(ComboBox.ObjectCollection list)
        {
            object[] array = new object[list.Count];
            list.CopyTo(array, 0);
            return Convert(array, list.Count, 0);
        }
        // transformation
        public static string[] Resize(string[] list, int count)
        {
            string[] temp = new string[count];
            for (int i = 0; i < list.Length && i < count; i++)
                temp[i] = list[i];
            return temp;
        }
        public static string[] Copy(string[] source)
        {
            if (source == null)
                return null;
            string[] temp = new string[source.Length];
            source.CopyTo(temp, 0);
            return temp;
        }
        public static string ToTitleCase(string source)
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(source.ToLower());
        }
        public static int IndexOf(string[] list, string item)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == item)
                    return i;
            return -1;
        }
        #endregion
    }
}
