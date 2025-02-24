LAZYSHELL++  Super Mario RPG Editor
Version: 2.0.0
Date: February 15, 2025
Home Page: https://github.com/Yakibomb/LAZYSHELL-UPDATED/
Written by Yaki, giangurgolo and Omega

_______________________________________________________________________

READ THIS FIRST
_______________________________________________________________________

If this is your first time using Lazy Shell, please take the time to
read the following advice:

1. When planning a hack project, the wisest thing to do is to make sure
   you have the back-up option enabled in the settings. Click the cog icon
   and check either "Create Back-up ROM on Load" or "Create Back-up ROM on
   Save" to enable it. I often hear about users throwing in the towel when
   the application corrupts the ROM, never having bothered to use the
   back-up feature.

2. If you receive an error prompt, please follow the directions in the
   prompt window and copy ALL of the contents of the error message and
   post them to the given link in the window. Unless you do this, there is
   little hope of the bug ever being fixed unless someone else encounters
   it and posts it to the link. Keep in mind, I do read bug reports and I
   do attempt to fix the reported bugs, so your post will not be a waste
   of time.

3. If the unfortunate occasion might arise that the application
   actually does corrupt your ROM, you can try resetting the corrupted
   elements by importing those specific elements from a fresh SMRPG ROM.
   Click the button "Import elements from another ROM" (a down-arrow over
   a small white box), select a fresh ROM, and check the elements you want
   to import.

Should you encounter any issues, errors, problems, or irritations using
the application, please post them on the following Discord:
https://discord.gg/p4epwWmTqf
Press Ctrl+Ins to copy the contents of this message box to the
clipboard. Thank you for using the program.

_______________________________________________________________________

INTRODUCTION
_______________________________________________________________________

Lazy Shell is a third party .NET application written in the C#
programming language which is capable of editing a wide range of
elements within the Super Mario RPG (US) ROM image file. These elements
include allies, battle animations, attacks, spells, sound effects,
music, battlefields, dialogues, fonts, effects, events, formations,
items, shops, level/location maps, the main title, menus, the mine-cart
maps, monsters, sprites, world maps, and more. In addition, it also
includes a help database, support for a patch server network, and a
project manager to help organize a full hack project.

_______________________________________________________________________

PROGRAM REQUIREMENTS
_______________________________________________________________________

Microsoft .NET Framework 4.8 or higher must be installed on the system
for the application to run at all.
Minimal system requirements:
512MB of RAM (1GB recommended)
10MB HD space (more if ROM back-ups used)

_______________________________________________________________________

MAIN FEATURES
_______________________________________________________________________

The editor is comprised of 17 individual editors. 
Various status editors include modification capabilities for the
statuses of monsters, formations, formation packs, items, spells,
attacks, shops, new game properties, level-ups, and timing properties.
The monsters editor contains a battle script editor for each monster. 
The Levels portion allows the user to modify the maps of areas (aka
locations) using a paint-like interface, the NPCs (ie the sprites in
the maps), the exit fields (aka entrances), event fields, overlaps, and
the basic layering properties. A template creator/editor lets the user
to store a separate portion of the map composed of all 3 layers and the
physical layer into a single file. 
The two scripts editors in Lazy Shell enable the user to modify the
event scripts, action scripts, and animations scripts. Commands within
event scripts and action scripts may be added, modified, deleted,
moved, or copied and pasted. Commands within animation scripts may be
modified, moved, or replaced with new commands of the same or smaller
length, but adding/deleting entirely new commands within animations is
not supported and never will be due to the fickle and erratic nature of
the animation script engine. 
The Sprites editor is able to modify a sprite's graphics, palette, and
animations. The effects editor allows the user to edit spell effects
and their respective graphics, palettes, and animations. 
In the Dialogue editor, the user may view and edit the dialogues (aka
the game script) as well as the dialogues which appear in battles and
the graphics of the dialogue background tiles. Fonts, font colors, and
a new font generator will let the user create an entirely new font
table based upon manual editing or a supportive font installed on the
OS.
In the World Maps editor World maps, world map palettes, world map
sprites, and the locations that appear on world maps can be modified.
The logo banner graphics and palettes can be modified as well.
The Audio editor can export, import, clear, and playback the audio
samples used by the SPC engine. The .wav files can be edited in a third
party program such as Audacity. SPC data can be edited in a variety of
ways, allowing the user to create entirely new pieces of music.
Instruments can be changed as well as well as the raw SPC track data,
which can even import custom scripts from a text document.
The mini-games editor so far can modify the minecart mini game maps for
all four stages and the objects in the same manner as levels. The menus
editor allows the user to modify the menu palettes and import an
external image into the menu backgrounds, as well as import an image
into the frame image.

_______________________________________________________________________

EXTRA FEATURES
_______________________________________________________________________

The portions of the editor have tooltips for almost every single
control. Just press F1 (or click the ? buttons found in most editors)
and move the mouse over a control to see what that property is for.
There is also a conversion tooltip for showing the hexadecimal or
decimal value for the value in the control when moving the mouse cursor
over it. Press F2 to enable this feature, or click the base conversion
button found in most editors. 
Users are also able to import and export many elements from previously
exported .dat, .bin, or .pal files in all portions of the editor as a
means of backing up or inter-changing elements. Clearing/erasing data
is managed as well so as to free up space for new scripts or dialogues.
The notes database manager was written for the editor to aid the user
aiming to create a full or partial hack. Indexes for elements such as
monsters, levels, etc. can be added and a user-defined description
provided as well. Adding new indexes is simplified with an option for
adding a specific index within a portion of the editor by right-
clicking the name list or index number. The user can also load a
selected index in the notes database manager into its respective
portion of the editor where it can be modified there.
The patch feature reads a list of patches from the currently defined
patch server http:// or ftp:// location and can apply those patches to
the currently loaded ROM image.
A previewer for levels, event scripts and battle scripts lets the user
load a temporary ROM created from the current modifications in the
currently loaded ROM image into an emulator of choice (the only options
so far are ZSNES and SNES9X). Lazy Shell will create a save state
which, when loaded, will immediately enter the current level or
initiate the current event or battle script.
There are many more smaller features which are too numerous to list
here, and are scattered throughout the editor with the purpose of
easing the use of Lazy Shell and reducing the amount of work required
to complete a task.
That's what all of these extra features are here for. Not as bells and
whistles, but for making the hacking process less headache-inducing.

_______________________________________________________________________

UNSUPPORTED FEATURES
_______________________________________________________________________

Lazy Shell can NOT edit the new game intro sequence and graphics, the
end credits graphics, or end credits fonts. It cannot make any changes
to 65c816 assembly code in the ROM image (with some small exceptions).
Additionally, ROM expansion is also not supported by the application
due to the complications of the SA-1 chip in the game's engine.

_______________________________________________________________________

USING THE PREVIEWER
_______________________________________________________________________

Before using the previewer, do the following: 
1. Make sure all editor files are in the same folder. 
2. Configure the emulator's save-state folder to read/write to the same
folder as any loaded ROM. ZSNES by default already does this, and so
does Snes9x v1.43. However, later versions of Snes9x will by default
read/write to a "Save" folder created within the emulator's main
folder, and if not changed it will fail to load the preview save state.
Choose either the SNES9X or ZSNES emulator file to use when opening the
previewer. ZSNES will automatically load the generated save state when
the emulator is loaded from the previewer, but for SNES9X the F1 key
must be pressed manually to load the generated save state. If the
emulator has problems loading the save state, make sure the 2 steps
above have been completed. 
The latest version of ZSNES (v1.51 as of this release) is supported.
Snes9x v1.43 and/or its derivatives (rerecord, Geiger's debugger, etc.)
are preferred and should work. 

_______________________________________________________________________

IMPORTING CUSTOM WAVS
_______________________________________________________________________

TIPS: follow these steps to successfully import a .WAV file of your
choosing. Say you have a .WAV file, "MyWavFile.wav", that you wish to
replace one of the samples in this audio editor with. Here are the
steps you need to take: 
1. Download and install Audacity, a good free audio editing program.
     http://audacity.sourceforge.net/download/
2. In the Lazy Shell audio editor, export any sample to a file named
"OldSample.wav".
3. Open "MyWavFile.wav" into Audacity.
4. While in Audacity, copy the audio data (Ctrl+A, Ctrl+C). Close
Audacity.
5. Open "OldSample.wav" into Audacity.
6. While in Audacity, paste the copied audio data over the old data
(Ctrl+A, Ctrl+V).
7. Export to a .WAV file named "NewSample.wav".
8. In the Lazy Shell audio editor, import "NewSample.wav".
MAKE SURE THE SELECTED Hz SAMPLE RATE IS THE SAME AS THE IMPORTED .WAV
FILE.
The reason this is the only way to do it is because by using the same
exported file ("OldSample.wav") with the modified WAV data from
"MyWavFile.wav", it retains some obscure data from "OldSample.wav" in
"NewSample.wav" which is necessary to have in order to successfully
import a .WAV file.

_______________________________________________________________________

FILES IN ARCHIVE
_______________________________________________________________________

*** Make sure all files stay within the same directory as each other,
or there will be problems running Lazy Shell ***


"LAZYSHELL.exe"

The application.


"Lunar Compress.dll"

Generates automatically when needed.
This file is essential to Lazy Shell's functionality. It decompresses
and compresses the data that Lazy Shell modifies. It is needed to run
the Stats, Levels, and Sprites editors and must be in the exact same
directory as LAZYSHELL.exe. Without it, the program is almost
completely functionless.


"RomPreviewBaseSave.000, RomPreviewBaseSave.zst"

These generate automatically when needed.
Base savestates for SNES9X and ZSNES, respectively. These are needed
for previewing levels, event scripts, and battle scripts using either
ZSNES or SNES9X. To avoid complications, make sure the emulators are in
the same directory as everything else and that their save directories
are configured likewise.


"changes.txt"

All of the fixes, modifications, and additions since the earliest
versions are chronicled here.


"readme.txt"

This file.


_______________________________________________________________________

BUGS, ERRORS, EXCEPTIONS AND COMPLICATIONS IN FUNCTIONALITY.
_______________________________________________________________________

The editor may occasionally crash or not function properly due to
certain errors in the code (although with each new bugfix I am aiming
to completely remove the possibility of this ever happening). Please
remember that this is almost certainly the programmer's fault and NOT
yours, the user's. As often is the case, when an error surfaces or the
program behaves in a buggy fashion, the user tends to immediately feel
that they are to blame or the programmer is blaming them for the error.
This is not correct: almost all of the time, it's the programmer's
fault. Incidences when it might be the fault of the user may be due to
a corrupt ROM being loaded (a corrupt ROM basically means any SMRPG rom
which has been modified in any way, shape or form). This includes a ROM
edited by Lazy Shell, but errors may occur if a ROM has been modified
outside of Lazy Shell (ie. a hex editor), or often times in much older
versions of Lazy Shell (v2.5 seems to be popular among users who find
problems with v3.x).
If the editor crashes, make sure it is the latest version of Lazy Shell
by clicking the "(i)" button in the main window and comparing the
version there to the version posted on the home page
(http://home.comcast.net/~giangurgolo/smrpg/).
If you are using the latest version, make a post in this Discord:
https://discord.gg/p4epwWmTqf
Explain exactly what you did to produce this error or cause this
disfunctionality to occur, what editor it was in, what order you did
your actions to produce this error or disfunctionality, etc. Also, when
the editor bugs out, an exception prompt appears. If you can manage to
copy the error message after this:
************** Exception Text **************
and include it in your post, it will definitely help. But most
importantly, you must explain in your post what you did. Only posting
the exception text alone will not be adequate enough.
The suggestions above are only suggestions. Sometimes only five words
might be enough for me to quickly locate the bug and fix it. Remember,
an error or bug is most likely NOT YOUR FAULT. Don't be afraid to
report an error should it occur. I do notice and try to fix every bug
that is reported, so your post won't be in vain (unless you're making a
request for an addition to the editor, which I am now ignoring due to
how time-consuming it can be).

_______________________________________________________________________

YAKI'S SPECIAL THANKS
_______________________________________________________________________


		Pidgezero_one, SMRPG Randomizer Team - Feedback, bug finders
		Doomsday31415 - Released source code to fix Dialogues editor save
corruptions
		Will319 - Positive feedback, bug finder, feature requests (flat mode
in particular)
		jpmac26 - For their LazyShell 4.0 source code fork
		AnAxemRanger - Bug finder, positive feedback
		

_______________________________________________________________________

F.A.Q. (FREQUENTLY ASKED QUESTIONS)
_______________________________________________________________________

Q: The editor will not run at all.
A: You need .NET Framework 4.8 or higher installed on your system.

Q: I have no idea what this stuff means!
A: First, look in the glossary at the end of this readme file. Also,
   enable the help feature in the editor. If this feature is enabled,
   you'll be able to see a description of the property and what it does
   by moving the mouse over it. Click the (?) icon at the top of most
   editors or press F1 to enable/disable the help feature.

Q: I want to design or write a new _____. Where do I start?
A: If you're new to this, study the editor. Play around with it for a
   while before delving into a new project. Tweak or mess around with
   things that already exist (ex: scripts, levels, monsters, sprites,
   etc.) to gain an understanding of how everything works or operates.
   Then, when you want to start something new or original, use the
   "template-based" approach which I have always used in my own hacks:
   try thinking of something in the game that most closely resembles
   what you want to do or create, find that element in the editors,
   study it and it's properties/details, and use that as a template for
   your custom-made whatever. Example: copy event script commands into
   your own script that are similar, modify levels that are closest in
   resemblance to what you want to create, use existing monsters that
   are most characteristic of your custom monsters. The OREFFEZEPS
   hack, for example, was made entirely through this method of template
   hacking: I tweaked existing spells/attacks to varying degrees to
   make new original ones. The Bob-omb Mafia's forest levels used
   touched-up tilemaps of dummied maps which already existed (as well
   as the sewers levels).

Q: How do I add a new ally, item, monster, etc.?
A: You cannot add new indexes to any element, but you can replace the
   properties of existing indexes. That is the basic rule of hacking
   SMRPG. Many elements, like sprites, have dummied or unused indexes
   which you may edit or modify to "add" new stuff. Lazy Shell should
   not be viewed as an expansion tool but as a modification tool.

Q: Can Super Mario RPG be expanded?
A: Not with Lazy Shell, but I do believe there was an expansion hack
   project that successfully expanded the ROM. Lunar Expand is
   incapable of expanding SMRPG due to SA-1 chip complications.

Q: I'm looking specifically for _____ matching a name/description.
A: Most of the editors have a search field to the right of the index
   list, tagged with a magnifying glass icon. Use that to search for a
   specific index with a general description, name, or whatever.

Q: The editor crashed and/or corrupted the ROM and I lost my work!
A: First, try clicking "Ignore Error" and saving as a separate ROM.
   Then, try exporting the indexes that didn't glitch out into .dat
   files and import those into a fresh, uncorrupted ROM. In the future,
   enable the back-up feature in the settings: click the grey cog icon
   in the main window to open the settings. There are two types of
   back-ups: you can back-up on load and/or save. Thus you can roll
   back to an earlier edit before the ROM got corrupted by the program.

Q: I loaded a hack and one of the editors crashed.
A: It's possible the hack was written using an earlier version of Lazy
   Shell, particularly v2.x. Earlier versions of the program had badly
   written code and incorrectly saved certain types of data like
   sprites. Therefore newer versions will likely encounter errors with
   hacks made with earlier versions, and/or crash.

Q: What do these "B#,b#" things mean?
A: These are unknown bits. "B0,b0" means "Byte 0, bit 0" and refers to
   bit 0 in the first byte of the index's property data chunk. If
   you're confident you have discovered exactly what these bits do,
   feel free to post it in one of the bug report thread:
   http://acmlm.kafuka.org/board/thread.php?id=7005
   http://www.smwcentral.net/?p=viewthread&t=45572

Q: Is there free space in the ROM where I can insert graphics?
A: Only the space that you see in the editor's own graphics editor can
   be modified. You can import external images into it, or use the more
   powerful import features. For example, in the sprites editor.

Q: I have no idea how scripts work.
A: There are three types of scripts, each with their own scripting
   language: event, battle, and animation scripts. See the glossary for
   a more detailed explanation of scripts and how each different type
   of script works.

Q: Why do the images I import decrease in quality?
A: The number of colors are reduced to 15 (or 3 in 2bpp cases). So if
   you're trying to import a Caravaggio painting, you won't have much
   luck keeping the quality. Palettes are 16 or 4 colors, with the
   first color being reserved for transparent pixels.

Q: What's a .dat file?
A: Exported elements into external files that can be imported into
   other indexes of the same element.

Q: My ROM hack's checksum is bad/failed!
A: Only fresh, unsaved, unmodified ROMs will have a good checksum
   (0x3bb4). The ROM's checksum always fails after a save. It's not a
   bug because the slightest change in the ROM data will create a bad
   checksum. However if you are loading a fresh, supposedly unaltered
   ROM and get a failed checksum, then there may be issues.

Q: The editor crashes, regardless of what ROM is loaded.
A: Try resetting the settings. Click the cog icon in the main window
   and click "Default..." to reset the settings.

Q: Sometimes it asks to save even if I haven't done anything.
A: In some editors, like the sprites editor, switching between indexes
   will reassemble the last loaded index's data thus changing the data
   in the process. This is not a bug, and none of the properties are
   actually changed; just sometimes the raw hex data is slightly
   rearranged from the original order.

Q: What are Lazy Shell's most powerful features?
A: 1. The "Import Image(s)" functions in the sprites and effects
   editors allow the user to replace existing sprites with entirely new
   sprite animations from external image files. These were quite
   difficult to write the code for, but very rewarding in that they
   ultimately added a lot more muscle to the editor.
   2. The palette editor's "Adjust RGB" and "Effects" features let the
   user apply all kinds of effects to the colors, from RGB swapping,
   grayscale, contrast/brightness, colorizing, and more. You can create
   your own palette swaps of sprites, levels, etc. 
   3. The "New Font Table" feature in the dialogues editor in the font
   editor panel lets you replace the SMRPG font with any font installed
   on your system. 
   4. You can flip entire battlefields by just selecting the whole
   battlefield, right-click, and click "mirror" or "invert". 5. A
   built-in hex editor lets the user edit the raw hex data of several
   elements in the game.

______
ALLIES
¯¯¯¯¯¯
Q: Is it possible to add new allies?
A: No, you can only modify the existing five allies.

Q: Can I move another ally to the front to appear in the overworld?
A: Not in Lazy Shell, but with ASM hacking it is possible.

Q: Is it possible to start with other characters besides Mario?
A: You would have to switch the Mario sprite with another character's.

Q: Can I raise the XP to 5 digits and the coins to 4?
A: Not in Lazy Shell.

__________
ANIMATIONS
¯¯¯¯¯¯¯¯¯¯
Q: How do I add things to a weapon or spell script?
A: These are called commands in the animation script editor, and you
   cannot add new ones: only replace, move or edit existing ones.

Q: I want to add new commands.
A: You cannot do this, you can only replace, move or edit commands.

Q: How do I replace commands?
A: Select the command and at the bottom of the editor replace the first
   hex value with the opcode of the command you want to replace it
   with.

Q: Where's the list of command opcodes for animations?
A: The docs_ani-code.txt file that comes packed with Lazy Shell++
   contains all opcodes decoded thus far.

Q: How do I make custom items with custom animations?
A: The easiest things to customize in the animations editor are the
   sprites used, the sound effects, and dialogues. "Current object =
   sprite: whatever" "Current sprite: whatever" "Current action object
   = effect: whatever" "Playback sound: whatever" are among them. It's
   better to start simple before working with stuff like memory.

Q: I want to change a sprite in an animation to something else.
A: You'll want to modify the "Current object = sprite: whatever"
   commands. Modify its properties in the "CURRENT COMMAND PROPERTIES"
   panel on the right and click apply when you are finished.
   Alternatively you can change the hex values below.

Q: How do I make allies use enemy spells/attacks?
A: This would require comprehensive animation script editing, including
   but not limited to careful repositioning of many effect and sprite
   graphics and changing behavior of the sprites, as well as changing
   the object memory addresses, etc. Example: "Light Beam" is
   specifically scripted to shift leftwards towards the allies, so to
   make it realistically target the monsters the initial position and
   direction would need to be changed among other things.

Q: Where are the locations of the dummy spell animation scripts?
A: There are none; dummy spells (except for the 4 nameless ones used by
   Smithy) have no pointers and no scripts.

Q: How do you modify damage in an animation script?
A: I cannot say for sure, but none of the animation scripts appear to
   contain commands that modify damage, aside from the CC command found
   only in ally spells that append damage.

Q: How do I replace Mushroom and Scarecrow with new effects?
A: Most likely it requires ASM hacking, but as this is an unexplored
   territory in the SMRPG ROM I cannot be sure.

_______
ATTACKS
¯¯¯¯¯¯¯
Q: Can I make new "inflict functions" for a custom spell?
A: This involves ASM, thus you cannot do this in Lazy Shell.

_____
AUDIO
¯¯¯¯¯
Q: Is there any way to add music to a SMRPG Rom?
A: Not in Lazy Shell; you can only modify the audio samples used by the
   music SPCs and the SPC instruments and tracks, not add new ones.

Q: Can I add sound effects?
A: Sound effects are also SPCs that use audio samples. Therefore the
   answer is no, like the above question. Some sound effects are empty
   which could be used to "add" custom sounds.

Q: I changed the instrument but it's muted in-game!
A: If the instrument is included among the percussives, you'll have to
   change the instrument index for the percussive as well.

Q: I modified some sound effects, but I can't hear any changes.
A: If you're loading from a save state outside of Lazy Shell, then the
   audio memory contained in the save state won't have the changes you
   made. This is because during sound effect playback the data is read
   from the memory instead of directly from the ROM, and all sound
   effect data is stored to memory when the ROM starts. The previewer,
   though, will let you hear the changes since the modified data is
   stored into the preview save state's audio memory.

Q: I imported an MML script but the channels are out of sync.
A: Sometimes you must delete the first rests contained in the last 7
   channels.

Q: How do I transfer my work in the score writer to the SPC?
A: Export the staffs as scripts, then import the scripts into each of
   the individual channels. You'll have to manually add non-note
   commands in the track editor, like beat durations, volumes, etc.

____________
BATTLEFIELDS
¯¯¯¯¯¯¯¯¯¯¯¯
_________
DIALOGUES
¯¯¯¯¯¯¯¯¯
Q: I need some extra letters that my native language uses.
A: Use the empty slots in the font table. Letters that have accents or
   other diacritic marks can be drawn onto the new letters or imported
   as a new font table.

Q: How do I edit the names in the Level-up bonus screen?
A: This is found by selecting "Battle messages" in the battle dialogues
   editor.

_______
EFFECTS
¯¯¯¯¯¯¯
_____________
EVENT SCRIPTS
¯¯¯¯¯¯¯¯¯¯¯¯¯
Q: I want to change an NPC's sprite's mold or sequence.
A: This must be done in an action queue. First insert an action queue
   with "Objects" > "Action queue...". Select the NPC from the menu.
   Then add the command "Sprite sequence" > "Seq playback, sprite +=".

Q: Can I make an NPC change colors/palettes?
A: This must be done in an action queue, using one of three types of
   commands. In the "Palette" category, choose one of the commands to
   shift the NPC's sprite's palette index forward. View the palettes
   using the sprites editor.

Q: Is there a default event script that is always running?
A: Not by default, but you can point the level's event script to a
   separate synchronous event that contains the memory-checking
   commands you want to run.

Q: How do I make a custom ending?
A: You can't edit the default ending sequence with Lazy Shell, but you
   can write a custom event script to run instead as long as the Smithy
   battle sequence doesn't run.

Q: How do I change a character's animation during a dialogue?
A: Make sure the "Run dialogue: whatever" command has the dialogue
   property "asynchronous" unchecked so any following commands that
   would change a sprite's animation sequence or whatever will run
   while the dialogue is playing. Use the "Pause script, resume on next
   dlg page" commands to pause the script after the animation is
   complete.

Q: I told my script to jump to index $11C, #284.
A: "Jump to $whatever" commands do NOT jump to indexes, they jump to
   addresses as seen in the [] to the left of each command.

Q: How do I make _____ the only active party member?
A: Through an event script use the command "Add/remove party member" in
   the "Party members" category.

Q: When I press The X button the inventory menu does not appear.
A: It must be made accessible using one of the "Joypad enable" commands
   in the "Joypad" category.

__________
FORMATIONS
¯¯¯¯¯¯¯¯¯¯
Q: Some of the monsters in my formation are glitchy or discolored.
A: Too many monsters or having several large monsters in the same
   formation will overload the game's video memory and start smothering
   the graphics of other monsters and sprites. Do not put more than 6
   monsters in a formation.

Q: My monster inexplicably changes palettes in battle.
A: See the previous question.

_____
ITEMS
¯¯¯¯¯
Q: I made a DUMMY item a weapon, but it freezes when used.
A: That's because it doesn't have an animation script. Unfortunately
   you cannot create or edit scripts for the DUMMY items in the
   animations editor because the ROM provides no extra space for it.

Q: Can I make new "inflict functions" for a custom item?
A: This involves ASM, thus you cannot do this in Lazy Shell.

Q: In the shops, there are two "Buy only, no selling" options.
A: They both do exactly the same thing.

______
LEVELS
¯¯¯¯¯¯
Q: My custom level just appears black in-game, music playing.
A: Make sure the layer mask's boundaries are within those of your new
   custom level and that Mario appears within those boundaries.

Q: How do I add something like an NPC to a level?
A: The tabs on the left panel, ex: the "NPCs" tab, contain buttons at
   the top of the tab window that let you insert, delete, copy, etc.
   NPCs, events, exits, overlaps, or mods.

Q: I notice that the NPC # does not correspond to the sprite #.
A: The help labels for the editor explain this. Hit F1 in the editor or
   click the (?) icon at the top of the levels editor.

Q: The NPCs are not showing.
A: Make sure "Show NPC instance" is checked.

Q: Some tiles keep overlapping the NPCs.
A: Those tiles most likely have priority 1 enabled on one or more of
   their subtiles. To stop this, replace the solidity tile at that spot
   with a corresponding solidity tile with the "Priority 1 enabled for
   objects on tile" checked. Otherwise, it could be an overlap tile. To
   stop this, delete the overlap at that spot.

Q: When I stand on the edge of a block it overlaps Mario and NPCs.
A: Look for a solidity tile with "Priority 3 for object on edge"
   enabled in the solidity tile search and draw it there.

Q: How do I get an NPC to start a dialogue?
A: The dialogues for NPCs are initiated in the NPC's event script. Open
   the NPC's event script or assign a new script # and insert a "Run
   dlg: whatever" command.

Q: The NPC graphics are glitchy in-game.
A: You'll want to try changing the "Partition" property. You can look
   for the best partition index for the level by using the parition
   searcher (accessible with the "Partition" button). Keep in mind the
   game only has so much video memory to store the sprites to, so too
   many large NPCs may just be impossible to show.

Q: I have no idea how these partitioning properties work.
A: Unfortunately, this is a grey area in my knowledge, as I am not
   completely sure how the game organizes video memory for NPC sprites
   on loading a level. Clone sprites (ex: multiple townspeople in the
   town levels) should be first in the NPC collection, and a partition
   that has the clone VRAM properties should be set to store them (3 or
   4 sprites per row). Additionally, cloned sprites can only be
   gridplane format sprites. 3 sprites per row is set for 32px width
   sprites, 4 per row is for 24px width. Large sprites that overflow
   and write over cloned sprites might be fixed by setting clone buffer
   A to "empty buffer" to provide extra space for the large sprite.

Q: What does "Could not insert the _____" mean?
A: You'll need to delete other exits, events, npcs, or overlaps to
   insert new ones.

Q: Walking off an edge causes Mario to fall and get stuck.
A: Most likely you forgot to draw in a solidity tile somewhere. The
   isometric orientation of the solidity map sometimes makes it tricky
   to fill in all tiles. Also, make sure to seal off the edges of the
   walkable area with impassable tiles (usually with solid tile #255).

Q: How can I prevent a monster from reappearing after battle?
A: Look in the "AFTER BATTLE..." panel.

Q: Mario's Pipehouse is completely black.
A: For some reason the game applies the proper palette through ASM only
   for this level. If you want to see the level to edit it, change the
   palette to {21} temporarily then back to {50} when finished.

Q: I want a script to play infinitely in the background.
A: The level's "EVENT #" script must jump to a synchronous event that
   loops indefinitely during gameplay.

Q: I want to add a warp trampoline or pipe to another level.
A: Copy/paste a warp trampoline NPC from another level and edit the
   script to point to the desired target level. For the pipes, copy/
   paste the event field at the pipe's coords from another level and
   edit the script likewise. Keep in mind you will be changing the
   target level for the original trampoline/pipe you copied from the
   other level as well.

Q: I want to add a monster to the level that starts a battle.
A: Set the "NPC TYPE" to "Battle" and the "Pack #" to the desired pack
   index. Remember, this is a pack index; not a formation index. Packs
   are three formations each, where one of the three is randomly
   selected for battle.

__________
MAIN TITLE
¯¯¯¯¯¯¯¯¯¯
Q: How do I make my own title screen?
A: The easiest way is to make an image outside of Lazy Shell with a
   paint program (with the same dimensions as the title) and import the
   external image file in the Main Title editor. Right-click the layer
   3 title logo and import the image. Creating an entirely new title
   screen is a very limited operation as there's only enough space for
   any new graphics and/or palettes that are imported.

_____
MENUS
¯¯¯¯¯
__________
MINI-GAMES
¯¯¯¯¯¯¯¯¯¯
Q: How do I delete/add mushrooms to stages 1 and 2?
A: You can't, the mushroom count is fixed to 8 for both stages.

Q: Why aren't the rails arching in stage 4 like in-game?
A: This is probably an effect applied by some code in the assembly I am
   not familiar with. Thus the rails appear flat in the editor.

________
MONSTERS
¯¯¯¯¯¯¯¯
Q: On a game over, things keep happening in battle.
A: Most likely the monster who inflicted the fatal blow has several
   consecutive attacks that need to have conditionals added to its
   script. "If target alive: at least one opponent" should be added
   before the commands following the initial attack.

Q: Battle commands keep executing one after another without stopping.
A: You'll need to add a "Wait 1 turn, restart script" or a "Wait 1
   turn" command. The first is used within an "If" statement.

_______
SPRITES
¯¯¯¯¯¯¯
Q: How do I add custom sprites?
A: You can only edit existing ones. You can import sprite images
   through several methods: import image files into a graphic set,
   import image files into the molds, etc. I strongly recommend
   importing images into the molds as the easiest way to make your own
   custom sprites from external image files.

Q: I want to replace a sprite with a new one from image files.
A: Use the mold import feature: the black down arrow and white box [v]
   icon directly over the mold list tagged "Import Image(s)". You may
   import one or more images over the current molds or append to the
   current molds.

Q: I want to change a monster's sprite to a different one.
A: Monster sprites are found in sprite indexes 256-511. If you want to
   change a specific sprite to another existing sprite, you'll need to
   change the "Image" and "Animation" values to that other sprite's
   image and animation values.

Q: How do I add tiles to a sprite?
A: You can only do this for tilemap formatted molds, not gridplanes.
   Select the tiles in the mold's tileset you want to draw and use the
   pencil tool to draw them.

Q: How do I create new tiles to draw with?
A: Once again, you can only to this for tilemap molds. Click "Insert
   new tile" (paper icon) at the top of the panel on the far right and
   manually set the subtiles.

Q: Are there any unused animation sequences?
A: http://tcrf.net/Super_Mario_RPG:_Legend_of_the_Seven_Stars Scroll
   down to the "Unused _____" sections.

Q: I get glitchy sprites when I load a save state.
A: This is not a bug with the editor nor the emulator. It is simply a
   matter of unsychronized memory: the newly edited ROM data for
   sprites is not sychronized with the save-state memory thus the
   glitchy sprites. This can be remedied by re-entering the area, which
   refreshes the memory.

Q: I want to make _____ a different color, or Mario without a hat.
A: Edit the palette and edit the graphics with the respective editors.

Q: Battle portraits appear discolored in the editor.
A: It appears like that in the editor, but the assembly manually
   applies the correct palette to the battle portraits.

Q: I can't find some sprites, even in the search.
A: Some sprites are within other sprites, nestled among the mold
   indexes. The nok-nok shell, for example, is molds 2-4 within the
   lazy shell sprite.

__________
WORLD MAPS
¯¯¯¯¯¯¯¯¯¯
_______
PATCHES
¯¯¯¯¯¯¯
_______________________________________________________________________

GLOSSARY
_______________________________________________________________________

"2bpp & 4bpp"
These mean "2 bits per pixel" and "4 bits per pixel", and refer to the
format that graphics are in. Graphics in 2bpp format can only use a
maximum of 4 colors, whereas 4bpp graphics can use up to 16 colors. The
Big Boo in Bowser's Terrorize spell only needs 4 colors, thus the
graphics are in 2bpp format to conserve ROM space.

"action queue"
A group of commands in an event script which behave like a custom
embedded action script, containing movement commands for a given NPC.

"action script"
A script comprised of commands which create a series of movements
ascribed to an NPC in a level. A townsperson walking back and forth
randomly is controlled by an action script. A wiggler's unique behavior
is governed by a special action script.

"AMEM"
1. Audio memory, used to store SPC and sample data.
2. Animation memory, used in animation scripts to store sub-routine
flags.

"animation script"
The scripts which animate everything seen in battle. Attacks, spells,
events, etc. are all governed by animation scripts. Animation scripts
are, like battle and event scripts, written in their own scripting
language. The animation scripting language is the most complicated and
difficult to write for, as they tend to jump around wildly throughout
the current bank by means of animation packets and memory-checking
mini-scripts. The animations editor contains many powerful features,
but unfortunately also many limitations on innovation. Some scripts are
painfully large because many of the mini-scripts within a script are
repeated numerous times.

"ASM"
This is essentially the game's raw code which runs when the game is
started. SNES games are generally written in assembly. Most everything
that Lazy Shell modifies is arranged in data chunks and dynamic scripts
and does not involve assembly programming code. Anything Lazy Shell is
incapable of modifying, outside of the defense timing values. "ASM
hacking" is modifying the assembly code outside of Lazy Shell.

"bit"
These behave like flags or switches that are turned on or off. The game
knows what has been done so far in the game because of the bits that
are set or clear. Defeating the Hammer Bros. sets a bit. Each time you
enter that level, an event script checks if the switch is turned on in
order to determine whether or not to show the Hammer Bros NPC and
execute the associated cut scene. The "Jump" bit is already switched on
when you start a new game, and can be switched off in the allies
editor. Checkboxes in the editor are usually associated with bit-wise
data while number values are byte-wise. Example: monster HP is read
byte-wise while elemental weaknesses are read bit-wise.

"command"
These are what comprise the many scripts in the ROM. Example: in the
first encounter with Terrapins, an event script contains a command
"Engage battle, pack: 1, battlefield: [07]" which initiates the battle
with the Terrapins.

"element"
The different things in the ROM that Lazy Shell can modify. The
individual editors can usually edit one or two types of elements.
Elements often have multiple indexes. Monsters are an element that has
256 indexes (0 to 255). Levels are another element that contains 510
indexes, event scripts have 4096 indexes.

"event script"
The game basically progresses by event scripts. Everything you see
happening in a level that isn't controlled by the player (ie. Mario) is
executed by the commands in an event script. Scripts are usually
initialized by a trigger in the level, when Mario comes into contact
with either an event field or an NPC. An event script is also
automatically initialized every time when entering a new level. This
script is the level's own event script (set with the "EVENT #") that
usually contains commands for preparing primarily NPC and memory
related elements before the level is completely loaded.

"event field"
A field which, when Mario touches it, will initiate an event script.
Event fields can actually be made to behave exactly like exit fields
and they often are when other commands must be executed. In these cases
the scripts contain a command pointing to the target level.

"exit field"
A field which, when Mario touches it, will load a new level. Other ROM
editors sometimes call these "entrances".

"field"
An invisible thing in a level that triggers an event or operation when
Mario touches it. Event and exit fields, for example.

"hex"
A numeric system who's places are based on 16's and not 10's like in
"decimal". With just one decimal place, you can count up to 9, but in
just one hexadecimal place you can go up to 15. This is because the
numbers 10-15 are A-F respectively. Thus in just two hexadecimal places
the numbers can go up to 255, which is why this number is so common
throughout the editor. With three hex places, 4096 is highest. The
editor displays only memory addresses in hex format (eg. 00:709F) with
all other elements being in decimal.

"index"
Example: TERRAPIN is an index in the monster element (index 0). The
level for Mario's Pad is index 16 in the levels element, etc. You can
modify the properties of each index by switching to or among them in
the editors using either its drop down list or immediately with its
numeric up/down.

"isometric"
The pseudo 3-D orientation of Super Mario RPG. Other games like Final
Fantasy Tactics and Tactics Ogre are isometrically oriented. Something
is called "isometric" when, instead of being in a flat gridplane, it is
shaped like a diamond and arranged at an angle. Nevertheless Mario
RPG's levels are drawn with square 16x16 tiles and not diamond-shaped
tiles, whereas the solidity tiles are.

"layer"
SMRPG uses five layers: L1, L2, L3, NPCs, BG. By default, NPCs appear
on top of all other layers (excluding priority 1 tiles). After that, L1
appears on top, followed by L2, L3, and BG. The BG is simply the solid
background color behind everything else.

"level"
The places and areas you can enter in-game. Many ROM editors also refer
to these as "locations".

"memory address"
A "slot" where the game stores information that it needs to access
later. Example: the 30 slots for items (7F:F882 to 7F:F89F) have memory
addresses. The memory addresses are the items. Completed events, like
defeating the Hammer Bros (00:7052, bit 6), are stored as a bit in a
memory address. A memory address has 8 bits.

"mode 7"
Mode 7 is a format in SNES which can display a 2D map in a 3D manner.
The 1st and 2nd mine cart levels are in mode 7 format, for instance. It
can also do transformations such as the stretching effect on the world
maps.

"mods"
These can change the tiles or solidity tiles of a level. In the levels
editor, there are two types of mods: tile mods and solidity mods.
Example: Croco blowing up the wall in Moleville Mines. Example: the
green button in Rose Town removing/adding stairs outside.

"mold"
An arrangement of tiles that form a complete image (ie. a sprite
image). A mold is similar to the orientation of a tilemap, except that
sprite molds can either be in a format that arranges the tiles in a
grid (gridplane) or a coordinate system (tilemap). One or more molds
may be contained in a sprite or effect and are used to create a
sequence animation.

"NPC"
Abbreviation for "non-playable character". They are basically the
sprites seen in-game in a level, excluding battles, but with a number
of properties all described by the help tags in the levels editor. An
NPC is not the same as a sprite, it merely has a sprite index assigned
to it among a bunch of other attributes.

"OMEM"
Object memory, referring to the object packets used in animation
scripts. Each time a $68 or $64 command is called a memory address
block is accessible exclusively by the current object, assigned by the
parameters $68 or $64 commands.

"palette"
A set of colors used to draw something. Almost all palettes are 16
colors, except for layer 3 graphics, fonts, and some effect graphics.
SNES games like SMRPG are somewhat limited in the number of colors they
can display, which is why imported image files can decrease
dramatically in quality. Many paint programs have features which can
decrease the color depth of an image to 16 colors.

"priority"
A tile or sprite's priority determines how it will overlap other tiles
or how other tiles will overlap it. "Priority 1" means someting will
overlap all other parts of a level that aren't also set to priority 1.
Higher priority numbers mean it will appear under other things.
Highlighting priority 1 tiles in a level will show what parts of the
level will typically overlap Mario and NPCs.

"script"
A list of 0 or more commands that carry out an action on screen in the
game, such as Toad running into Mario near the beginning of the game,
or Bowser's "Crusher" battle animation, or The Big Boo randomly
selecting either "Lighting Orb" or "Bolt" to use in battle. Examples:
event scripts, action scripts, battle scripts, animations.

"sequence"
An animation. Two types of elements use sequences: sprites and effects.
A sequence is a collection of frames. Each frame is assigned a mold
index from the sprite or effect's mold collection and plays back the
frames as a fully animated sequence.

"solidity"
Also varyingly called called "physical field" or "collision tiles". The
physical properties of something, like a map. Levels in most games have
solidity maps, but usually as tilesets associated with the regular
graphical tileset. As tilesets in SMRPG are grid-based and not
isometric like the solidity tiles, the tilemaps and not the tilesets
have their own solidity maps.

"tilemap"
An example: Levels are 64 rows of tiles, each row is 64 tiles. Many
sprites are "tilemaps" themselves, but here each tile would have its
own coordinate instead of being placed in a grid like levels. Do not
mistake tilemaps with tilesets. "tileset" A collection or "palette" of
tiles used to draw to a tilemap.

"trigger"
When Mario comes into contact with something like an NPC or event
field, and an event script is initiated, it is "triggered". NPCs have a
trigger property which sets the conditions for the script's initiation
when Mario collides with it.

_______________________________________________________________________

EDITORS
_______________________________________________________________________

_______________________________________________________________________

SUB-EDITORS
_______________________________________________________________________

