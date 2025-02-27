using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data

        public static string[] EventCommands
        {
            get
            {
                for (int i = 0; i < Model.Characters.Length; i++)
                    eventCommands[i] = Model.CharacterNames.GetUnsortedName(i) + ", {0}";
                return eventCommands;
            }
        }
        public static string[] eventCommands = new string[]
        {
            "Mario, {0}",			// 0x00
            "Toadstool, {0}",			// 0x01
            "Bowser, {0}",			// 0x02
            "Geno, {0}",			// 0x03
            "Mallow, {0}",			// 0x04
            "DUMMY 0x05, {0}",			// 0x05
            "DUMMY 0x06, {0}",			// 0x06
            "DUMMY 0x07, {0}",			// 0x07
            "Character in slot 1, {0}",			// 0x08
            "Character in slot 2, {0}",			// 0x09
            "Character in slot 3, {0}",			// 0x0A
            "DUMMY 0x0B, {0}",			// 0x0B
            "Screen focus, {0}",			// 0x0C
            "Layer 1, {0}",			// 0x0D
            "Layer 2, {0}",			// 0x0E
            "Layer 3, {0}",			// 0x0F
			
            "Object @ memory $70A8, {0}",			// 0x10
            "Object @ memory $70A9, {0}",			// 0x11
            "Object @ memory $70AA, {0}",			// 0x12
            "Object @ memory $70AB, {0}",			// 0x13
            "NPC #0, {0}",			// 0x14
            "NPC #1, {0}",			// 0x15
            "NPC #2, {0}",			// 0x16
            "NPC #3, {0}",			// 0x17
            "NPC #4, {0}",			// 0x18
            "NPC #5, {0}",			// 0x19
            "NPC #6, {0}",			// 0x1A
            "NPC #7, {0}",			// 0x1B
            "NPC #8, {0}",			// 0x1C
            "NPC #9, {0}",			// 0x1D
            "NPC #10, {0}",			// 0x1E
            "NPC #11, {0}",			// 0x1F
			
            "NPC #12, {0}",			// 0x20
            "NPC #13, {0}",			// 0x21
            "NPC #14, {0}",			// 0x22
            "NPC #15, {0}",			// 0x23
            "NPC #16, {0}",			// 0x24
            "NPC #17, {0}",			// 0x25
            "NPC #18, {0}",			// 0x26
            "NPC #19, {0}",			// 0x27
            "NPC #20, {0}",			// 0x28
            "NPC #21, {0}",			// 0x29
            "NPC #22, {0}",			// 0x2A
            "NPC #23, {0}",			// 0x2B
            "NPC #24, {0}",			// 0x2C
            "NPC #25, {0}",			// 0x2D
            "NPC #26, {0}",			// 0x2E
            "NPC #27, {0}",			// 0x2F
			
            "Freeze all NPCs until return",			// 0x30
            "Unfreeze all NPCs",			// 0x31
            "If {0} is present in current level, jump to ${1}",			// 0x32
            "",			// 0x33
            "Enable buttons {{ {0} }} only (reset @ return)",			// 0x34
            "Enable buttons {{ {0} }} only",			// 0x35
            "{0} {1} in party",			// 0x36
            "Memory $7000 = party capacity",			// 0x37
            "Memory $7000 = character in slot {0}",			// 0x38
            "If Mario is on top of {0}, jump to ${1}",			// 0x39
            "If distance between {0} and {1} < ({2},{3}), jump to ${4}",			// 0x3A
            "If distance between {0} and {1} < ({2},{3}) and Z coord is equal, jump to ${4}",			// 0x3B
            "",			// 0x3C
            "If Mario is in the air, jump to ${0}",			// 0x3D
            "Create new NPC ({0}) @ coords of {1} (if null, jump to ${2})",			// 0x3E
            "Create new NPC ({0}) @ coords of $7010-15 (if null, jump to ${1})",			// 0x3F
			
            "Run background event #{0} ({1})",			// 0x40
            "",			// 0x41
            "If Mario on top of an object, jump to ${0}",			// 0x42
            "",			// 0x43
            "Run background event #{0}, timer @ ${1}",			// 0x44
            "Run background event #{0}, timer @ ${1} (return on exit)",			// 0x45
            "Stop background event assigned timer @ ${0}",			// 0x46
            "Resume background event assigned timer @ ${0}",			// 0x47
            "",			// 0x48
            "Engage in battle with pack @ $700E",			// 0x49
            "Engage in battle with pack {0} (battlefield #{1})",			// 0x4A
            "Open location: {0}",			// 0x4B
            "Open shop menu: {0}",			// 0x4C
            "",			// 0x4D
            "Run event sequence: {0}",			// 0x4E
            "Open menu/run event sequence: {0}",			// 0x4F
			
            "Store 1 of item {0} to inventory",			// 0x50
            "Remove 1 of item {0} from inventory",			// 0x51
            "Coins + {0}",			// 0x52
            "Frog coins + {0}",			// 0x53
            "Equip {0} to character {1}",			// 0x54
            "Memory $7000 = # of open item slots",			// 0x55
            "{0}'s HP -= memory $7000",			// 0x56
            "FP -= memory $7000",			// 0x57
            "Memory $7000 = current FP",			// 0x58
            "",			// 0x59
            "",			// 0x5A
            "Pause script if menu open",			// 0x5B
            "",			// 0x5C
            "Reactivate trigger if Mario on top of object",			// 0x5D
            "",			// 0x5E
            "",			// 0x5F
			
            "Run dialogue: {0} ({1}, {2}, {3}, {4})",			// 0x60
            "Run dialogue @ memory $7000 ({1}, {2}, {3}, {4})",			// 0x61
            "Run dialogue: {0} ({1}, {2})",			// 0x62
            "Append dialogue @ memory $7000 to current dialogue",			// 0x63
            "Close dialogue",			// 0x64
            "Un-sync dialogue",			// 0x65
            "If dialogue option B selected, jump to ${0}",			// 0x66
            "If dialogue option B selected, jump to ${0}; if option C, jump to ${1}",			// 0x67
            "Open level: {0}; place Mario @ ({1},{2},{3},{4}) ({5}, {6})",			// 0x68
            "",			// 0x69
            "Modify layer of level {0}; apply mod #{1} ({2})",			// 0x6A
            "Modify solidity of level {0}; apply mod #{1} ({2})",			// 0x6B
            "",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
			
            "Fade in from black (sync)",			// 0x70
            "Fade in from black (async)",			// 0x71
            "Fade in from black (sync), duration: {0}",			// 0x72
            "Fade in from black (async), duration: {0}",			// 0x73
            "Fade out to black (sync)",			// 0x74
            "Fade out to black (async)",			// 0x75
            "Fade out to black (sync), duration: {0}",			// 0x76
            "Fade out to black (async), duration: {0}",			// 0x77
            "Fade in from {0} (duration: {1})",			// 0x78
            "Fade out to {0} (duration: {1})",			// 0x79
            "Star mask, expand from screen center",			// 0x7A
            "Star mask, shrink to screen center",			// 0x7B
            "Circle mask, expand from screen center",			// 0x7C
            "Circle mask, shrink to screen center",			// 0x7D
            "Initiate battle mask",			// 0x7E
            "Pause script until screen effect done",			// 0x7F
			
            "Tint layers {{ {1} }} with 4bpp color {0} (speed: {2})",			// 0x80
            "Priority mainscreen = {{ {0} }}, subscreen = {{ {1} }}, color math = {{ {2} }}",			// 0x81
            "Reset priority set",			// 0x82
            "Screen flashes {0}",			// 0x83
            "Pixellate layers {{ {0} }} (pixel size: {1}, duration: {2})",			// 0x84
            "",			// 0x85
            "",			// 0x86
            "Circle mask (non-static) shrinks to {0} (padding: {1}px, speed: {2})",			// 0x87
            "",			// 0x88
            "Palette set {0} set #{1}, row {2}",			// 0x89
            "Palette set = set #{0}, rows 0 to {1}",			// 0x8A
            "",			// 0x8B
            "",			// 0x8C
            "",			// 0x8D
            "",			// 0x8E
            "Circle mask (static) shrinks to {0} (padding: {1}px, speed: {2})",			// 0x8F
			
            "Play music: {0} (current volume)",			// 0x90
            "Play music: {0} (default volume)",			// 0x91
            "Fade in music: {0}",			// 0x92
            "Fade out current music",			// 0x93
            "Stop current music",			// 0x94
            "Fade out current music to volume {1} (time stretch = {0})",			// 0x95
            "",			// 0x96
            "{0} music tempo by {1} (time stretch = {2})",			// 0x97
            "{0} music pitch by {1} (time stretch = {2})",			// 0x98
            "",			// 0x99
            "",			// 0x9A
            "Stop current sound",			// 0x9B
            "Play sound (ch.6,7): {0}",			// 0x9C
            "Play sound (ch.6,7): {0} (speaker balance = {1})",			// 0x9D
            "Fade out current sound to volume {1} (time stretch = {0})",			// 0x9E
            "",			// 0x9F
			
            "Set memory ${0} bit {1}",			// 0xA0
            "Set memory ${0} bit {1}",			// 0xA1
            "Set memory ${0} bit {1}",			// 0xA2
            "Set memory $704x [x @ $7000] bit {1}",			// 0xA3
            "Clear memory ${0} bit {1}",			// 0xA4
            "Clear memory ${0} bit {1}",			// 0xA5
            "Clear memory ${0} bit {1}",			// 0xA6
            "Clear memory $704x [x @ $7000] bit {1}",			// 0xA7
            "Memory ${0} = {1}",			// 0xA8
            "Memory ${0} += {1}",			// 0xA9
            "Memory ${0} += 1",			// 0xAA
            "Memory ${0} -= 1",			// 0xAB
            "Memory $7000 = {0}",			// 0xAC
            "Memory $7000 += {0}",			// 0xAD
            "Memory $7000 + 1",			// 0xAE
            "Memory $7000 - 1",			// 0xAF
			
            "Memory ${0} = {1}",			// 0xB0
            "Memory ${0} += {1}",			// 0xB1
            "Memory ${0} += 1",			// 0xB2
            "Memory ${0} -= 1",			// 0xB3
            "Memory $7000 = memory ${0}",			// 0xB4
            "Memory ${0} = memory $7000",			// 0xB5
            "Memory $7000 = random # between 0 and {0} (NOT including {0})",			// 0xB6
            "Memory ${0} = random # between 0 and {1} (NOT including {1})",			// 0xB7
            "Memory $7000 += memory ${0}",			// 0xB8
            "Memory $7000 -= memory ${0}",			// 0xB9
            "Memory $7000 = memory ${0}",			// 0xBA
            "Memory ${0} = memory $7000",			// 0xBB
            "Memory ${0} = memory ${1}",			// 0xBC
            "Memory ${0} <=> memory ${1}",			// 0xBD
            "",			// 0xBE
            "",			// 0xBF
			
            "Compare memory $7000 to {0}",			// 0xC0
            "Compare memory $7000 to memory ${0}",			// 0xC1
            "Compare memory ${0} to {1}",			// 0xC2
            "Memory $7000 = current level",			// 0xC3
            "Memory $7000 = X ({1}) coord of {0}",			// 0xC4
            "Memory $7000 = Y ({1}) coord of {0}",			// 0xC5
            "Memory $7000 = Z ({1}) coord of {0}",			// 0xC6
            "Memory $7010-15 = coords of {0}",			// 0xC7
            "Memory $7016-1B = coords of {0}",			// 0xC8
            "Memory $7000 = F coord of {0}",			// 0xC9
            "Memory $7000 = pressed button",			// 0xCA
            "Memory $7000 = tapped button",			// 0xCB
            "",			// 0xCC
            "",			// 0xCD
            "",			// 0xCE
            "",			// 0xCF
			
            "Jump to event #{0}",			// 0xD0
            "Run event #{0} as subroutine",			// 0xD1
            "Jump to address ${0}",			// 0xD2
            "Jump to subroutine ${0}",			// 0xD3
            "Loop start, count = {0}",			// 0xD4
            "Loop start, timer = {0} frames",			// 0xD5
            "Object memory = memory ${0}",			// 0xD6
            "Loop end",			// 0xD7
            "If memory ${0} bit {1} is set, jump to ${2}",			// 0xD8
            "If memory ${0} bit {1} is set, jump to ${2}",			// 0xD9
            "If memory ${0} bit {1} is set, jump to ${2}",			// 0xDA
            "If Memory $704x [x @ $7000] bit {0} set, jump to ${1}",			// 0xDB
            "If memory ${0} bit {1} is clear, jump to ${2}",			// 0xDC
            "If memory ${0} bit {1} is clear, jump to ${2}",			// 0xDD
            "If memory ${0} bit {1} is clear, jump to ${2}",			// 0xDE
            "If memory $704x [x @ $7000] bit {0} clear, jump to ${1}",			// 0xDF
			
            "If memory ${0} = {1}, jump to ${2}",			// 0xE0
            "If memory ${0} != {1}, jump to ${2}",			// 0xE1
            "If memory $7000 = {0}, jump to ${1}",			// 0xE2
            "If memory $7000 != {0}, jump to ${1}",			// 0xE3
            "If memory ${0} = {1}, jump to ${2}",			// 0xE4
            "If memory ${0} != {1}, jump to ${2}",			// 0xE5
            "If memory $7000 all bits {0} clear, jump to ${1}",			// 0xE6
            "If memory $7000 any bits {0} set, jump to ${1}",			// 0xE7
            "If random # between 0 and 255 > 128, jump to ${0}",			// 0xE8
            "If random # between 0 and 255 > 66, jump to ${0}",			// 0xE9
            "If loaded memory = 0, jump to ${0}",			// 0xEA
            "If loaded memory != 0, jump to ${0}",			// 0xEB
            "If comparison result is: >=, jump to ${0}",			// 0xEC
            "If comparison result is: <, jump to ${0}",			// 0xED
            "If loaded memory < 0, jump to ${0}",			// 0xEE
            "If loaded memory >= 0, jump to ${0}",			// 0xEF
			
            "Pause script for {0} frames",			// 0xF0
            "Pause script for {0} frames",			// 0xF1
            "{2} {0} in level {1}",			// 0xF2
            "{2} event trigger of {0} in level {1}",			// 0xF3
            "Summon object @ $70A8 to current level",			// 0xF4
            "Remove object @ $70A8 from current level",			// 0xF5
            "Enable event trigger for object @ $70A8",			// 0xF6
            "Disable event trigger for object @ $70A8",			// 0xF7
            "If {0} is {1} in level {2}; jump to ${3}",			// 0xF8
            "Jump to start of script",			// 0xF9
            "Jump to start of script",			// 0xFA
            "Reset game, choose game",			// 0xFB
            "Reset game",			// 0xFC
            "",			// 0xFD
            "Return",			// 0xFE
            "Return all"			// 0xFF
			        };
        public static string[] EventCommandsFD = new string[]
        {
            "","","","","","","","",
            "","","","","","","","",
            "","","","","","","","",
            "","","","","","","","",
            "","","","","","","","",
            "","","","","","","","",
            "Unfreeze screen",			// 0x30
            "Freeze screen",			// 0x31
            "Remember last object",			// 0x32
            "If {0}'s action script running, jump to ${1}",			// 0x33
            "If {0} is underwater, jump to ${1}",			// 0x34
            "",			// 0x35
            "",			// 0x36
            "",			// 0x37
            "",			// 0x38
            "",			// 0x39
            "",			// 0x3A
            "",			// 0x3B
            "",			// 0x3C
            "If {0} is in the air, jump to ${1}",			// 0x3D
            "Create new NPC ({0}) + event #{1} @ coords of $7010-15 (if null, jump to ${2})",			// 0x3E
            "",			// 0x3F
			
            "Move script to main thread",			// 0x40
            "Move script to background thread 1",			// 0x41
            "Move script to background thread 2",			// 0x42
            "Stop all background events",			// 0x43
            "",			// 0x44
            "",			// 0x45
            "Jump to event #{0} at return",			// 0x46
            "",			// 0x47
            "",			// 0x48
            "",			// 0x49
            "Open save game menu",			// 0x4A
            "Experience += experience packet",			// 0x4B
            "Run menu tutorial: {0}",			// 0x4C
            "Run star piece sequence #{0}",			// 0x4D
            "Run moleville mountain sequence",			// 0x4E
            "Run moleville mountain intro sequence",			// 0x4F
			
            "Store memory $70A7 to item inventory",			// 0x50
            "Store memory $70A7 to equipment inventory",			// 0x51
            "Coins += memory $7000",			// 0x52
            "Coins -= memory $7000",			// 0x53
            "Frog coins += memory $7000",			// 0x54
            "Frog coins -= memory $7000",			// 0x55
            "Current FP += memory $7000",			// 0x56
            "Maximum FP += memory $7000",			// 0x57
            "Memory $7000 = quantity of item {0}",			// 0x58
            "Memory $7000 = coins",			// 0x59
            "Memory $7000 = frog coins",			// 0x5A
            "Restore all HP",			// 0x5B
            "Restore all FP",			// 0x5C
            "Memory $7000 = {1}'s equipped {0}",			// 0x5D
            "Memory $70A7 = quantity of item @ memory $7000",			// 0x5E
            "",			// 0x5F
			
            "Pause script, resume on next dialogue page A",			// 0x60
            "Pause script, resume on next dialogue page B",			// 0x61
            "",			// 0x62
            "",			// 0x63
            "Experience packet = memory $7000",			// 0x64
            "Run level-up bonus sequence",			// 0x65
            "Display pre-game intro title \"{0}\" (y={1} tiles)",			// 0x66
            "Run ending credit sequence",			// 0x67
            "",			// 0x68
            "",			// 0x69
            "",			// 0x6A
            "",			// 0x6B
            "",			// 0x6C
            "",			// 0x6D
            "",			// 0x6E
            "",			// 0x6F
			
            "",			// 0x70
            "",			// 0x71
            "",			// 0x72
            "",			// 0x73
            "",			// 0x74
            "",			// 0x75
            "",			// 0x76
            "",			// 0x77
            "",			// 0x78
            "",			// 0x79
            "",			// 0x7A
            "",			// 0x7B
            "",			// 0x7C
            "",			// 0x7D
            "",			// 0x7E
            "",			// 0x7F
			
            "",			// 0x80
            "",			// 0x81
            "",			// 0x82
            "",			// 0x83
            "",			// 0x84
            "",			// 0x85
            "",			// 0x86
            "",			// 0x87
            "",			// 0x88
            "",			// 0x89
            "",			// 0x8A
            "",			// 0x8B
            "",			// 0x8C
            "",			// 0x8D
            "",			// 0x8E
            "",			// 0x8F
			
            "",			// 0x90
            "",			// 0x91
            "",			// 0x92
            "",			// 0x93
            "Deactivate sound channels {0}",			// 0x94
            "",			// 0x95
            "If audio memory $69 >= {0}",			// 0x96
            "If audio memory $69 = {0}",			// 0x97
            "",			// 0x98
            "",			// 0x99
            "",			// 0x9A
            "",			// 0x9B
            "Play sound (ch.4,5): {0}",			// 0x9C
            "Play sound (ch.6,7): {0} (speaker balance = {1})",			// 0x9D
            "Play music: {0}",			// 0x9E
            "Stop current music",			// 0x9F
			
            "Stop current music",			// 0xA0
            "Stop current music",			// 0xA1
            "Stop current music",			// 0xA2
            "Fade out current music",			// 0xA3
            "Lower current music tempo",			// 0xA4
            "Slide current music tempo to default",			// 0xA5
            "Stop current music",			// 0xA6
            "",			// 0xA7
            "Set memory ${0} bit {1}",			// 0xA8
            "Set memory ${0} bit {1}",			// 0xA9
            "Set memory ${0} bit {1}",			// 0xAA
            "",			// 0xAB
            "Memory $7000 = memory $7F{0}",			// 0xAC
            "",			// 0xAD
            "",			// 0xAE
            "",			// 0xAF
			
            "Memory $7000 &= {0}",			// 0xB0
            "Memory $7000 |= {0}",			// 0xB1
            "Memory $7000 ^= {0}",			// 0xB2
            "Memory $7000 &= memory ${0}",			// 0xB3
            "Memory $7000 |= memory ${0}",			// 0xB4
            "Memory $7000 ^= memory ${0}",			// 0xB5
            "Shift memory ${0} left {1} times",			// 0xB6
            "Generate random # between 0 and memory ${0}",			// 0xB7
            "Memory $7000 = Moleville Mountain timer",			// 0xB8
            "",			// 0xB9
            "",			// 0xBA
            "",			// 0xBB
            "",			// 0xBC
            "",			// 0xBD
            "",			// 0xBE
            "",			// 0xBF
			
            "",			// 0xC0
            "",			// 0xC1
            "",			// 0xC2
            "",			// 0xC3
            "",			// 0xC4
            "",			// 0xC5
            "",			// 0xC6
            "",			// 0xC7
            "",			// 0xC8
            "",			// 0xC9
            "",			// 0xCA
            "",			// 0xCB
            "",			// 0xCC
            "",			// 0xCD
            "",			// 0xCE
            "",			// 0xCF
			
            "",			// 0xD0
            "",			// 0xD1
            "",			// 0xD2
            "",			// 0xD3
            "",			// 0xD4
            "",			// 0xD5
            "",			// 0xD6
            "",			// 0xD7
            "",			// 0xD8
            "",			// 0xD9
            "",			// 0xDA
            "",			// 0xDB
            "",			// 0xDC
            "",			// 0xDD
            "",			// 0xDE
            "",			// 0xDF
			
            "",			// 0xE0
            "",			// 0xE1
            "",			// 0xE2
            "",			// 0xE3
            "",			// 0xE4
            "",			// 0xE5
            "",			// 0xE6
            "",			// 0xE7
            "",			// 0xE8
            "",			// 0xE9
            "",			// 0xEA
            "",			// 0xEB
            "",			// 0xEC
            "",			// 0xED
            "",			// 0xEE
            "",			// 0xEF
			
            "If {0} event trigger {1} in level {2}; jump to ${3}",			// 0xF0
            "",			// 0xF1
            "",			// 0xF2
            "",			// 0xF3
            "",			// 0xF4
            "",			// 0xF4
            "",			// 0xF6
            "",			// 0xF7
            "Exor crashes into keep",			// 0xF8
            "Mario glowing begins",			// 0xF9
            "Mario glowing stops",			// 0xFA
            "",			// 0xFB
            "",			// 0xFC
            "",			// 0xFD
            "Return",			// 0xFE
            "Return all"			// 0xFF
        };
        private static string[] CharacterNames
        {
            get
            {
                string[] characterNames = new string[] {
                    "Mario","Toadstool","Bowser","Geno","Mallow",
                    "INVALID","INVALID","INVALID","INVALID","INVALID",
                    "INVALID","INVALID","INVALID","INVALID","INVALID" };

                for (int i = 0; i < Model.Characters.Length; i++)
                    characterNames[i] = Model.CharacterNames.GetUnsortedName(i);
                return characterNames;
            }
        }
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
        private static string[] Menus = new string[] { 
            "choose game", "overworld menu", "return to world map", "shop 0", "save game", "items maxed out" };
        private static string[] DirectionNames = new string[] { 
            "E", "SE", "S", "SW", "W", "NW", "N", "NE" };
        private static string[] ColorNames = new string[] { 
            "black", "blue", "red", "pink", "green", "aqua", "yellow", "white" };
        private static string[] ButtonNames = new string[] { "left", "right", "down", "up", "X", "A", "Y", "B" };
        private static string[] BitNames = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
        private static string[] LayerNames = new string[] { "L1", "L2", "L3", "L4", "Sprites", "BG", "½ intensity", "Minus sub" };
        private static string[] TutorialNames = new string[] { "How to equip", "How to use items", "How to switch allies", "How to play beetle mania" };
        private static string[] EventNames = new string[] { "Mario falls to pipehouse", "Mario returns to MK", "Mario takes Nimbus bus" };
        #endregion
        public string InterpretCommand(EventCommand esc)
        {
            string[] vars = new string[16];
            switch (esc.Opcode)
            {
                case 0x32:
                    vars[0] = ObjectNames[esc.Param1];
                    vars[1] = Bits.GetShort(esc.CommandData, 2).ToString("X4");
                    break;
                case 0x34:
                case 0x35: vars[0] = GetBits(esc.Param1, ButtonNames, 8); break;
                case 0x36:
                    vars[0] = (esc.Param1 & 0x80) == 0x80 ? "Add" : "Remove";
                    vars[1] = CharacterNames[esc.Param1 & 0x1F];
                    break;
                case 0x38: vars[0] = (esc.Param1 - 8).ToString(); break;
                case 0x39:
                    vars[0] = ObjectNames[esc.Param1];
                    vars[1] = Bits.GetShort(esc.CommandData, 2).ToString("X4");
                    break;
                case 0x3A:
                case 0x3B:
                    vars[0] = ObjectNames[esc.Param1];
                    vars[1] = ObjectNames[esc.Param2];
                    vars[2] = esc.Param3.ToString();
                    vars[3] = esc.Param4.ToString();
                    vars[4] = Bits.GetShort(esc.CommandData, 5).ToString("X4");
                    break;
                case 0x3D:
                case 0xE8:
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString("X4");
                    break;
                case 0x3E:
                    vars[0] = Lists.NPCPackets[esc.Param1];
                    vars[1] = ObjectNames[esc.Param2];
                    vars[2] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0x3F:
                    vars[0] = Lists.NPCPackets[esc.Param1];
                    vars[1] = Bits.GetShort(esc.CommandData, 2).ToString("X4");
                    break;
                case 0x42:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString("X4");
                    vars[1] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0x40:
                    vars[0] = (Bits.GetShort(esc.CommandData, 1) & 0xFFF).ToString();
                    vars[1] = ((esc.Param2 & 0x20) == 0x20) ? "return on exit" : "...";
                    break;
                case 0x44:
                case 0x45:
                    vars[0] = (Bits.GetShort(esc.CommandData, 1) & 0xFFF).ToString();
                    vars[1] = ((esc.Param2 >> 6) * 2 + 0x701C).ToString("X4");
                    break;
                case 0x46:
                case 0x47:
                    vars[0] = (esc.Param1 * 2 + 0x701C).ToString("X4");
                    break;
                case 0xD0:
                case 0xD1: vars[0] = (Bits.GetShort(esc.CommandData, 1) & 0xFFF).ToString(); break;
                case 0x4B: vars[0] = Model.Locations[esc.Param1].ToString(); break;
                case 0x4C: vars[0] = Lists.Numerize(Lists.ShopNames, esc.Param1); break;
                case 0x4F: vars[0] = Lists.MenuNames[esc.Param1].ToString(); break;
                case 0x52:
                case 0x53:
                case 0x72:
                case 0x73:
                case 0x76:
                case 0x77:
                case 0xD4:
                    vars[0] = esc.Param1.ToString(); break;
                case 0xF0:
                    vars[0] = (esc.Param1 + 1).ToString(); break;
                case 0x4A:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param3.ToString("d2");
                    break;
                case 0x50:
                case 0x51:
                    vars[0] = Model.ItemNames.NumerizeUnsorted(esc.Param1) + "\"";
                    break;
                case 0x54:
                    vars[0] = CharacterNames[esc.Param1];
                    vars[1] = Model.ItemNames.NumerizeUnsorted(esc.Param2) + "\"";
                    break;
                case 0x4E:
                    vars[0] = Lists.MenuNames[esc.Param1];
                    switch (esc.Param1)
                    {
                        case 5: vars[0] += " (toss item: " + Model.ItemNames.NumerizeUnsorted(esc.Param2) + ")"; break;
                        case 7: vars[0] += " (" + TutorialNames[esc.Param2] + ")"; break;
                        case 8:
                        case 13: vars[0] += " #" + esc.Param2; break;
                        case 16: vars[0] += " (" + EventNames[esc.Param2] + ")"; break;
                    }
                    break;
                case 0x56: vars[0] = CharacterNames[esc.Param1 & 0x1F]; break;
                case 0x60:
                    vars[0] = "{" + (Bits.GetShort(esc.CommandData, 1) & 0xFFF).ToString() + "}  " +
                        "\"" + Model.Dialogues[Bits.GetShort(esc.CommandData, 1) & 0xFFF].GetStub(true, Model.DTEStr(true)) + "\"";
                    vars[1] = (esc.Param2 & 0x80) == 0x80 ? "async" : "sync";
                    vars[2] = (esc.Param2 & 0x20) == 0x20 ? "closable" : "unclosable";
                    vars[3] = (esc.Param3 & 0x80) == 0x80 ? "BG on" : "BG off";
                    vars[4] = (esc.Param3 & 0x40) == 0x40 ? "multi-line" : "single-line";
                    break;
                case 0x61:
                    vars[0] = (esc.Param1 & 0x80) == 0x80 ? "async" : "sync";
                    vars[1] = (esc.Param1 & 0x20) == 0x20 ? "closable" : "unclosable";
                    vars[2] = (esc.Param2 & 0x80) == 0x80 ? "BG on" : "BG off";
                    vars[3] = (esc.Param2 & 0x40) == 0x40 ? "multi-line" : "single-line";
                    break;
                case 0x62:
                    vars[0] = "{" + (Bits.GetShort(esc.CommandData, 1) & 0xFFF).ToString() + "}  " +
                        "\"" + Model.Dialogues[Bits.GetShort(esc.CommandData, 1) & 0xFFF].GetStub(true, Model.DTEStr(true)) + "\"";
                    vars[1] = (esc.Param2 & 0x80) == 0x80 ? "asynchronous" : "synchronous";
                    switch ((esc.Param2 & 0x60) >> 5)
                    {
                        case 1: vars[2] = "duration: short"; break;
                        case 2: vars[2] = "duration: long"; break;
                        default: vars[2] = "duration: forever"; break;
                    }
                    break;
                case 0x63:
                    vars[0] = (esc.Param1 & 0x80) == 0x80 ? "asynchronous" : "synchronous";
                    vars[1] = (esc.Param1 & 0x20) == 0x20 ? "closable" : "unclosable";
                    break;
                case 0x66:
                case 0xD2:
                case 0xD3:
                case 0xDB:
                case 0xDF: vars[0] = (Bits.GetShort(esc.CommandData, 1)).ToString("X4"); break;
                case 0x67:
                    vars[0] = (Bits.GetShort(esc.CommandData, 1)).ToString("X4");
                    vars[1] = (Bits.GetShort(esc.CommandData, 3)).ToString("X4");
                    break;
                case 0x68:
                    vars[0] = Lists.Numerize(Lists.LevelNames, Bits.GetShort(esc.CommandData, 1) & 0x1FF);
                    vars[1] = esc.Param3.ToString();
                    vars[2] = esc.Param4.ToString();
                    vars[3] = (esc.Param5 & 0x1F).ToString();
                    vars[4] = DirectionNames[((esc.Param5 & 0xE0) >> 5)];
                    vars[5] = (esc.Param2 & 0x80) == 0x80 ? "run entrance event" : "no entrance event";
                    vars[6] = (esc.Param2 & 0x08) == 0x08 ? "show message" : "no message";
                    break;
                case 0x6A:
                case 0x6B:
                    vars[0] = Lists.Numerize(Lists.LevelNames, Bits.GetShort(esc.CommandData, 1) & 0x1FF);
                    vars[1] = ((esc.Param2 & 0x3F) >> 1).ToString();
                    if (esc.Opcode == 0x6A)
                        vars[2] = (esc.Param2 & 0x80) == 0x80 ? "use alternate mod" : "...";
                    else
                        vars[2] = (esc.Param2 & 0x80) == 0x80 ? "permanent" : "temporary";
                    break;
                case 0x78:
                case 0x79:
                    vars[0] = ColorNames[esc.Param2];
                    vars[1] = "duration = " + esc.Param1.ToString();
                    break;
                case 0x80:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString("X4");
                    vars[1] = GetBits(esc.Param3, LayerNames, 8);
                    vars[2] = esc.Param4.ToString();
                    break;
                case 0x81:
                    vars[0] = GetBits(esc.Param1, LayerNames, 8);
                    vars[1] = GetBits(esc.Param2, LayerNames, 8);
                    vars[2] = GetBits(esc.Param3, LayerNames, 8);
                    break;
                case 0x83:
                    vars[0] = ColorNames[esc.Param1];
                    break;
                case 0x84:
                    vars[0] = GetBits(esc.Param1, LayerNames, 4);
                    vars[1] = ((esc.Param1 & 0xF0) >> 4).ToString();
                    vars[2] = (esc.Param2 & 0x3F).ToString();
                    break;
                case 0x89:
                    switch ((esc.Param1 & 0xE0) >> 5)
                    {
                        case 3: vars[0] = "glows continuously to"; break;
                        case 6: vars[0] = "glows once to"; break;
                        case 7: vars[0] = "fades to"; break;
                        default: vars[0] = "INVALID"; break;
                    }
                    vars[1] = esc.Param3.ToString();
                    vars[2] = esc.Param2.ToString();
                    vars[3] = (esc.Param1 & 0x0F).ToString();
                    break;
                case 0x8A:
                    vars[0] = esc.Param2.ToString();
                    vars[1] = (((esc.Param1 & 0xF0) / 16) + 1).ToString();
                    break;
                case 0x87:
                case 0x8F:
                    vars[0] = ObjectNames[esc.Param1];
                    vars[1] = esc.Param2.ToString();
                    vars[2] = esc.Param3.ToString();
                    break;
                case 0x90:
                case 0x91:
                case 0x92:
                    vars[0] = Lists.Numerize(Lists.MusicNames, esc.Param1);
                    break;
                case 0x95:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x9E:
                    vars[0] = esc.Param1.ToString();
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x97:
                    vars[0] = (esc.Param2 & 0x80) == 0x80 ? "Speed up" : "Slow down";
                    vars[1] = Math.Abs((sbyte)esc.Param2).ToString();
                    vars[2] = esc.Param1.ToString();
                    break;
                case 0x98:
                    vars[0] = (esc.Param2 & 0x80) == 0x80 ? "Lower" : "Raise";
                    vars[1] = Math.Abs((sbyte)esc.Param2).ToString();
                    vars[2] = esc.Param1.ToString();
                    break;
                case 0x9C:
                    vars[0] = Lists.Numerize(Lists.SoundNames, esc.Param1);
                    break;
                case 0x9D:
                    vars[0] = Lists.Numerize(Lists.SoundNames, esc.Param1);
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0xA0:
                case 0xA1:
                case 0xA2:
                    vars[0] = (((((esc.Opcode * 0x100) + esc.Param1) - 0xA000) / 8) + 0x7040).ToString("X4");
                    vars[1] = (esc.Param1 & 0x07).ToString();
                    break;
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    vars[0] = (((((esc.Opcode * 0x100) + esc.Param1) - 0xA400) / 8) + 0x7040).ToString("X4");
                    vars[1] = (esc.Param1 & 0x07).ToString();
                    break;
                case 0xA8:
                case 0xA9:
                    vars[0] = (esc.Param1 + 0x70A0).ToString("X4");
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0xAA:
                case 0xAB:
                case 0xB4:
                case 0xB5:
                    vars[0] = (esc.Param1 + 0x70A0).ToString("X4");
                    break;
                case 0xAC:
                case 0xAD:
                case 0xB6:
                case 0xC0:
                case 0xD5:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString();
                    break;
                case 0xF1:
                    vars[0] = (Bits.GetShort(esc.CommandData, 1) + 1).ToString();
                    break;
                case 0xB0:
                case 0xB1:
                    vars[0] = ((esc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(esc.CommandData, 2).ToString();
                    break;
                case 0xB2:
                case 0xB3:
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xC1:
                case 0xD6:
                    vars[0] = ((esc.Param1 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xB7:
                case 0xC2:
                    vars[0] = ((esc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = Bits.GetShort(esc.CommandData, 2).ToString();
                    break;
                case 0xBC:
                case 0xBD:
                    vars[0] = ((esc.Param2 * 2) + 0x7000).ToString("X4");
                    vars[1] = ((esc.Param1 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    vars[0] = ObjectNames[esc.Param1 & 0x3F];
                    vars[1] = (esc.Param1 & 0x40) == 0x40 ? "isometric" : "pixel";
                    break;
                case 0xC7:
                case 0xC8:
                case 0xC9:
                    vars[0] = ObjectNames[esc.Param1 & 0x3F];
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                    vars[0] = (((((esc.Opcode * 0x100) + esc.Param1) - 0xD800) / 8) + 0x7040).ToString("X4");
                    vars[1] = (esc.Param1 & 0x07).ToString();
                    vars[2] = (Bits.GetShort(esc.CommandData, 2)).ToString("X4");
                    break;
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    vars[0] = (((((esc.Opcode * 0x100) + esc.Param1) - 0xDC00) / 8) + 0x7040).ToString("X4");
                    vars[1] = (esc.Param1 & 0x07).ToString();
                    vars[2] = (Bits.GetShort(esc.CommandData, 2)).ToString("X4");
                    break;
                case 0xE0:
                case 0xE1:
                    vars[0] = (esc.Param1 + 0x70A0).ToString("X4");
                    vars[1] = esc.Param2.ToString();
                    vars[2] = (Bits.GetShort(esc.CommandData, 3)).ToString("X4");
                    break;
                case 0xE2:
                case 0xE3:
                    vars[0] = (Bits.GetShort(esc.CommandData, 1)).ToString();
                    vars[1] = (Bits.GetShort(esc.CommandData, 3)).ToString("X4");
                    break;
                case 0xE6:
                case 0xE7:
                    vars[0] = GetBits(Bits.GetShort(esc.CommandData, 1), BitNames, 16);
                    vars[1] = (Bits.GetShort(esc.CommandData, 3)).ToString("X4");
                    break;
                case 0xE4:
                case 0xE5:
                    vars[0] = ((esc.Param1 * 2) + 0x7000).ToString("X4");
                    vars[1] = (Bits.GetShort(esc.CommandData, 2)).ToString();
                    vars[2] = (Bits.GetShort(esc.CommandData, 4)).ToString("X4");
                    break;
                case 0xE9:
                    vars[0] = Bits.GetShort(esc.CommandData, 1).ToString("X4");
                    vars[1] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0xF2:
                case 0xF3:
                    vars[0] = ObjectNames[((esc.Param2 >> 1) & 0x3F)];
                    vars[1] = Lists.Numerize(Lists.LevelNames, (Bits.GetShort(esc.CommandData, 1) & 0x1FF));
                    if (esc.Opcode == 0xF2)
                        vars[2] = (esc.Param2 & 0x80) == 0x80 ? "Summon" : "Remove";
                    else
                        vars[2] = (esc.Param2 & 0x80) == 0x80 ? "Enable" : "Disable";
                    break;
                case 0xF8:
                    vars[0] = ObjectNames[((esc.Param2 >> 1) & 0x3F)];
                    vars[1] = Bits.GetBit(esc.Param2, 7) ? "present" : "absent";
                    vars[2] = Lists.Numerize(Lists.LevelNames, (Bits.GetShort(esc.CommandData, 1) & 0x1FF));
                    vars[3] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0xFD: return FD_Opcodes(esc);
                default:
                    if (esc.Opcode <= 0x2F)
                    {
                        switch (esc.Param1)
                        {
                            case 0xF0:
                            case 0xF1:
                                vars[0] = (esc.Param2 & 0x80) == 0x80 ?
                                    "start embedded action script (async)" :
                                    "start embedded action script (sync)";
                                break;
                            case 0xF2:
                                vars[0] = "action script = #" +
                                    Bits.GetShort(esc.CommandData, 2).ToString() +
                                    " (sync)";
                                break;
                            case 0xF3:
                                vars[0] = "action script = #" +
                                    Bits.GetShort(esc.CommandData, 2).ToString() +
                                    " (async)";
                                break;
                            case 0xF4:
                                vars[0] = "temporary action script = #" +
                                    Bits.GetShort(esc.CommandData, 2).ToString() +
                                    " (sync)";
                                break;
                            case 0xF5:
                                vars[0] = "temporary action script = #" +
                                    Bits.GetShort(esc.CommandData, 2).ToString() +
                                    " (async)";
                                break;
                            case 0xF6: vars[0] = "un-sync action script"; break;
                            case 0xF7: vars[0] = "summon to current level @ Mario's coords"; break;
                            case 0xF8: vars[0] = "summon to current level"; break;
                            case 0xF9: vars[0] = "remove from current level"; break;
                            case 0xFA: vars[0] = "pause action script"; break;
                            case 0xFB: vars[0] = "resume action script"; break;
                            case 0xFC: vars[0] = "enable trigger"; break;
                            case 0xFD: vars[0] = "disable trigger"; break;
                            case 0xFE: vars[0] = "stop embedded action script"; break;
                            case 0xFF: vars[0] = "reset coords"; break;
                            default:
                                vars[0] = "action queue (" +
                                    ((esc.Param1 & 0x80) == 0x80 ? "async" : "sync") +
                                    ")";
                                break;
                        }
                    }
                    break;
            }
            string command = EventCommands[esc.Opcode];
            if (command == "")
                command = "{{" + BitConverter.ToString(esc.CommandData) + "}}";
            return string.Format(command, vars);
        }
        private string FD_Opcodes(EventCommand esc)
        {
            string[] vars = new string[16];
            switch (esc.Param1)
            {
                case 0x33:
                case 0x34:
                case 0x3D:
                    vars[0] = ObjectNames[esc.Param2];
                    vars[1] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0x3E:
                    vars[0] = Lists.NPCPackets[esc.Param2];
                    vars[1] = Bits.GetShort(esc.CommandData, 3).ToString();
                    vars[2] = Bits.GetShort(esc.CommandData, 5).ToString("X4");
                    break;
                case 0x46:
                    vars[0] = Bits.GetShort(esc.CommandData, 2).ToString();
                    break;
                case 0x4C:
                    vars[0] = Lists.Tutorials[esc.Param2];
                    break;
                case 0x4D:
                    vars[0] = esc.Param2.ToString();
                    break;
                case 0x9C:
                    vars[0] = Lists.Numerize(Lists.SoundNames, esc.Param2);
                    break;
                case 0x9D:
                    vars[0] = Lists.Numerize(Lists.SoundNames, esc.Param2);
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x9E:
                    vars[0] = Lists.Numerize(Lists.MusicNames, esc.Param2);
                    break;
                case 0x58:
                    vars[0] = Model.ItemNames.NumerizeUnsorted(esc.Param2);
                    break;
                case 0x5D:
                    switch (esc.Param3 & 0x03)
                    {
                        case 0x01: vars[0] = "armor"; break;
                        case 0x02: vars[0] = "accessory"; break;
                        default: vars[0] = "weapon"; break;
                    }
                    vars[1] = CharacterNames[esc.Param2 & 0x0F];
                    break;
                case 0x66:
                    switch (esc.Param3)
                    {
                        case 0: vars[0] = "Super Mario"; break;
                        case 1: vars[0] = "Princess Toadstool"; break;
                        case 2: vars[0] = "King Bowser"; break;
                        case 3: vars[0] = "Mallow"; break;
                        case 4: vars[0] = "Geno"; break;
                        default: vars[0] = "In..."; break;
                    }
                    vars[1] = esc.Param2.ToString();
                    break;
                case 0x94:
                    vars[0] = GetBits(esc.Param2, null, 8);
                    break;
                case 0x96:
                case 0x97:
                    vars[0] = esc.Param2.ToString();
                    vars[1] = Bits.GetShort(esc.CommandData, 3).ToString("X4");
                    break;
                case 0xA8:
                case 0xA9:
                case 0xAA:
                    vars[0] = (((((esc.Param1 * 0x100) + esc.Param2) - 0xA800) / 8) + 0x7040).ToString("X4");
                    vars[1] = (esc.Param2 & 0x07).ToString();
                    break;
                case 0xAC:
                    vars[0] = (Bits.GetShort(esc.CommandData, 2) + 0xF800).ToString("X4");
                    break;
                case 0xB0:
                case 0xB1:
                case 0xB2:
                    vars[0] = Bits.GetShort(esc.CommandData, 2).ToString();
                    break;
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB7:
                    vars[0] = ((esc.Param2 * 2) + 0x7000).ToString("X4");
                    break;
                case 0xB6:
                    vars[0] = ((esc.Param2 * 2) + 0x7000).ToString("X4");
                    vars[1] = ((esc.Param3 ^ 0xFF) + 1).ToString();
                    break;
                case 0xF0:
                    vars[0] = ObjectNames[((esc.Param3 >> 1) & 0x3F)];
                    vars[1] = Bits.GetBit(esc.Param3, 7) ? "enabled" : "disabled";
                    vars[2] = Lists.Numerize(Lists.LevelNames, (Bits.GetShort(esc.CommandData, 2) & 0x1FF));
                    vars[3] = Bits.GetShort(esc.CommandData, 4).ToString("X4");
                    break;
                default:
                    break;
            }
            string command = EventCommandsFD[esc.Param1];
            if (command == "")
                command = "{{" + BitConverter.ToString(esc.CommandData) + "}}";
            return string.Format(command, vars);
        }
    }
}