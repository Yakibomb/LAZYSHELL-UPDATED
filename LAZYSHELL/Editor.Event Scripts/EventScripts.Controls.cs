using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL
{
    public partial class EventScripts : NewForm
    {
        private void ControlDisassembleEvent()
        {
            this.Updating = true;
            panelCommands.SuspendDrawing();
            int[] tree = categoryCommand;
            if (tree != null)
            {
                categories_es.SelectedIndex = tree[0];
                commands.SelectedIndex = tree[1];
            }
            switch (esc.Opcode)
            {
                // Objects
                case 0x32:  // If object present...
                case 0x39:  // If Mario on top of object...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA3.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 2);
                    break;
                case 0x3A:         // If distance between object A and...
                case 0x3B:         // If distance (Z==) between object A and...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object A";
                    labelEvtA2.Text = "Object B";
                    labelEvtA3.Text = "Less than X";
                    labelEvtA4.Text = "Less than Y";
                    labelEvtC1.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.ObjectNames); evtNameA2.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;          // object A
                    evtNameA2.SelectedIndex = esc.Param2;    // object B
                    evtNumA3.Value = esc.Param3;
                    evtNumA4.Value = esc.Param4;
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 5);
                    break;
                case 0x3D:         // If Mario in air...
                    groupBoxC.Text = commandText;
                    labelEvtC1.Text = "Jump to";
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                case 0x3E:         // Create NPC packet @ obj coords...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Packet";
                    groupBoxC.Text = "If null...";
                    labelEvtC1.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.NPCPackets)); evtNameA2.Enabled = true;
                    evtNameA2.DropDownWidth = 200;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA1.SelectedIndex = esc.Param2;
                    evtNameA2.SelectedIndex = esc.Param1;
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                case 0x3F:         // Create NPC packet...
                    groupBoxA.Text = commandText;
                    labelEvtA2.Text = "Packet";
                    groupBoxC.Text = "If null...";
                    labelEvtC1.Text = "Jump to";
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.NPCPackets)); evtNameA2.Enabled = true;
                    evtNameA2.DropDownWidth = 200;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA2.SelectedIndex = esc.Param1;
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 2);
                    break;
                case 0x42:         // If Mario on top of an object...
                    groupBoxC.Text = commandText;
                    labelEvtC1.Text = "Jump to";
                    labelEvtC2.Text = "Else jump to";
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    evtNumC2.Enabled = true; evtNumC2.Hexadecimal = true; evtNumC2.Maximum = 0xFFFF;
                    //
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 1);
                    evtNumC2.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                case 0xF2:         // Set obj presence...  
                case 0xF3:         // Set obj engage type...
                case 0xF8:         // If object in level ..., presence =...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Level";
                    labelEvtA2.Text = "Object";
                    if (esc.Opcode == 0xF8)
                        labelEvtC1.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LevelNames)); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 500;
                    evtNameA2.Items.AddRange(Lists.ObjectNames); evtNameA2.Enabled = true;
                    evtNumA1.Enabled = true; evtNumA1.Maximum = 511;
                    if (esc.Opcode == 0xF3)
                        evtEffects.Items.AddRange(new object[] { "is enabled" });
                    else
                        evtEffects.Items.AddRange(new object[] { "is present" });
                    evtEffects.Enabled = true;
                    if (esc.Opcode == 0xF8)
                        evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1) & 0x1FF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNameA2.SelectedIndex = (esc.Param2 >> 1) & 0x3F;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x80) == 0x80);
                    if (esc.Opcode == 0xF8)
                        evtNumC1.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                // Joypad
                case 0x34:
                case 0x35:
                    groupBoxB.Text = commandText;
                    evtEffects.Items.AddRange(Lists.ButtonNames); evtEffects.Enabled = true;
                    //
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        evtEffects.SetItemChecked(j, (esc.Param1 & i) == i);
                    break;
                // Party Members
                case 0x36:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    labelEvtA2.Text = "Add/remove";
                    evtNameA1.Items.AddRange(Lists.CharacterNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(new string[] { "remove from party", "add to party" }); evtNameA2.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1 & 0x07;
                    evtNameA2.SelectedIndex = esc.Param1 >> 7;
                    break;
                case 0x54:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    labelEvtA2.Text = "Item";
                    evtNameA1.Items.AddRange(Lists.CharacterNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Model.ItemNames.Names); evtNameA2.Enabled = true;
                    evtNameA2.DrawMode = DrawMode.OwnerDrawFixed;
                    evtNumA2.Maximum = 255; evtNumA2.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1 & 7;
                    evtNumA2.Value = esc.Param2;
                    evtNameA2.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA2.Value);
                    break;
                case 0x56:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    evtNameA1.Items.AddRange(Lists.CharacterNames); evtNameA1.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1 & 7;
                    break;
                // Inventory
                case 0x50:
                case 0x51:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Item";
                    evtNameA1.Items.AddRange(Model.ItemNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.DrawMode = DrawMode.OwnerDrawFixed;
                    evtNumA1.Maximum = 255; evtNumA1.Enabled = true;
                    //
                    evtNumA1.Value = esc.Param1;
                    evtNameA1.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA1.Value);
                    break;
                case 0x52:
                case 0x53:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Value";
                    evtNumA1.Enabled = true;
                    //
                    evtNumA1.Value = esc.Param1;
                    break;
                // Battle
                case 0x4A:
                    groupBoxA.Text = commandText;
                    labelEvtA2.Text = "Battlefield";
                    labelEvtA3.Text = "Pack";
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames)); evtNameA2.Enabled = true;
                    evtNameA2.DropDownWidth = 250;
                    evtNumA2.Maximum = 63; evtNumA2.Enabled = true;
                    evtNumA3.Enabled = true;
                    //
                    evtNumA2.Value = esc.Param3;
                    evtNameA2.SelectedIndex = esc.Param3;
                    evtNumA3.Value = esc.Param1;
                    break;
                // Levels
                case 0x4B:      // Open, world location...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Location";
                    groupBoxB.Text = "Unknown bits";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.MapNames)); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 200;
                    evtEffects.Items.AddRange(new string[] { "bit 5", "bit 6", "bit 7" }); evtEffects.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(1, (esc.Param2 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(2, (esc.Param2 & 0x80) == 0x80);
                    break;
                case 0x68:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Level";
                    labelEvtA2.Text = "F / Z";
                    labelEvtA3.Text = "X";
                    labelEvtA4.Text = "Y";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LevelNames)); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 500;
                    evtNameA2.Items.AddRange(Lists.Directions); evtNameA2.Enabled = true;
                    evtNumA1.Enabled = true; evtNumA1.Maximum = 511;
                    evtNumA2.Enabled = true; evtNumA2.Maximum = 31;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtEffects.Items.AddRange(new object[] { "show message", "run entrance event", "Z + ½" });
                    evtEffects.Enabled = true;
                    //
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1) & 0x1FF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNumA2.Value = esc.Param5 & 0x1F;
                    evtNameA2.SelectedIndex = (esc.Param5 & 0xE0) >> 5;
                    evtNumA3.Value = esc.Param3;
                    evtNumA4.Value = esc.Param4;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x08) == 0x08);
                    evtEffects.SetItemChecked(1, (esc.Param2 & 0x80) == 0x80);
                    evtEffects.SetItemChecked(2, (esc.Param4 & 0x80) == 0x80);
                    break;
                case 0x6A:
                case 0x6B:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Level";
                    labelEvtA3.Text = "Mod #";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LevelNames)); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 500;
                    evtNumA1.Enabled = true; evtNumA1.Maximum = 511;
                    evtNumA3.Enabled = true; evtNumA3.Maximum = 63;
                    //
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1) & 0x1FF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNumA3.Value = (esc.Param2 >> 1) & 0x3F;
                    //
                    if (esc.Opcode == 0x6A)
                        evtEffects.Items.AddRange(new object[] { "alternate" });
                    else
                        evtEffects.Items.AddRange(new object[] { "permanent" });
                    evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x80) == 0x80);
                    break;
                // Open window
                case 0x4C:      // Open, shop menu...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Shop";
                    evtNameA1.Items.AddRange(Lists.ShopNames);
                    evtNameA1.DropDownWidth = 200; evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x4F:      // Open, window...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Menu";
                    evtNameA1.Items.AddRange(Lists.MenuNames);
                    evtNameA1.DropDownWidth = 200;
                    evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                // Dialogue
                case 0x60:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Dialogue";
                    labelEvtA2.Text = "Show above";
                    evtNameA1.Items.AddRange(DialogueNames()); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 250;
                    evtNumA1.Maximum = 4095; evtNumA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.ObjectNames); evtNameA2.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "closable", "asynchronous", "multi-line", "background" });
                    evtEffects.Enabled = true;
                    //
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1) & 0xFFF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNameA2.SelectedIndex = esc.Param3 & 0x3F;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(1, (esc.Param2 & 0x80) == 0x80);
                    evtEffects.SetItemChecked(2, (esc.Param3 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(3, (esc.Param3 & 0x80) == 0x80);
                    break;
                case 0x61:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Show above";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "closable", "asynchronous", "multi-line", "background" });
                    evtEffects.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param2 & 0x3F;
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(1, (esc.Param1 & 0x80) == 0x80);
                    evtEffects.SetItemChecked(2, (esc.Param2 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(3, (esc.Param2 & 0x80) == 0x80);
                    break;
                case 0x62:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Dialogue";
                    labelEvtA3.Text = "Duration";
                    evtNameA1.Items.AddRange(DialogueNames()); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 250;
                    evtNumA1.Maximum = 4095; evtNumA1.Enabled = true;
                    evtNumA3.Maximum = 3; evtNumA3.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "asynchronous" });
                    evtEffects.Enabled = true;
                    //
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1) & 0xFFF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNumA3.Value = (esc.Param2 & 0x60) >> 5;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x80) == 0x80);
                    break;
                case 0x63:
                    groupBoxB.Text = commandText;
                    evtEffects.Items.AddRange(new string[] { "closable", "asynchronous" }); evtEffects.Enabled = true;
                    //
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(1, (esc.Param1 & 0x80) == 0x80);
                    break;
                case 0x66:
                case 0x67:
                    groupBoxA.Text = commandText;
                    //
                    labelEvtA3.Text = esc.Opcode == 0x66 ? "Jump to" : "If B, jump to";
                    evtNumA3.Maximum = 0xFFFF; evtNumA3.Hexadecimal = true; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    if (esc.Opcode == 0x67)
                    {
                        labelEvtA4.Text = "If C, jump to";
                        evtNumA4.Maximum = 0xFFFF; evtNumA4.Hexadecimal = true; evtNumA4.Enabled = true;
                        evtNumA4.Value = Bits.GetShort(esc.CommandData, 3);
                    }
                    break;
                // Events
                case 0x40:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Event";
                    evtNumA3.Maximum = 4095; evtNumA3.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "return on level exit", "bit 6", "Run as second script" }); evtEffects.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1) & 0xFFF;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(1, (esc.Param2 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(2, (esc.Param2 & 0x80) == 0x80);
                    break;
                case 0x44:
                case 0x45:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Event";
                    labelEvtA4.Text = "Timer memory";
                    evtNumA3.Maximum = 4095; evtNumA3.Enabled = true;
                    evtNumA4.Minimum = 0x701C; evtNumA4.Maximum = 0x7022;
                    evtNumA4.Increment = 2; evtNumA4.Hexadecimal = true; evtNumA4.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "bit 4", "bit 5" }); evtEffects.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1) & 0xFFF;
                    evtNumA4.Value = (esc.Param2 >> 6) * 2 + 0x701C;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x10) == 0x10);
                    evtEffects.SetItemChecked(1, (esc.Param2 & 0x20) == 0x20);
                    break;
                case 0x46:
                case 0x47:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Timer memory";
                    evtNumA3.Minimum = 0x701C; evtNumA3.Maximum = 0x7022;
                    evtNumA3.Increment = 2; evtNumA3.Hexadecimal = true; evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1 * 2 + 0x701C;
                    break;
                case 0xD0:
                case 0xD1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Event";
                    evtNumA3.Maximum = 4095; evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1) & 0xFFF;
                    break;
                case 0x4E:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Category";
                    evtNameA1.Items.AddRange(Lists.MenuNames);
                    evtNameA1.DropDownWidth = 200;
                    evtNameA1.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    switch (evtNameA1.SelectedIndex)
                    {
                        case 2: // open world location
                            labelEvtA2.Text = "Location";
                            evtNameA2.Items.AddRange(Lists.Numerize(Lists.MapNames)); evtNameA2.Enabled = true;
                            evtNameA2.SelectedIndex = esc.Param2;
                            break;
                        case 3: // open shop menu
                            labelEvtA3.Text = "Shop menu";
                            evtNumA3.Maximum = 32; evtNumA3.Enabled = true;
                            evtNumA3.Value = esc.Param2;
                            break;
                        case 5: // items maxed out
                            labelEvtA2.Text = "Toss item";
                            evtNameA2.Items.AddRange(Model.ItemNames.Names); evtNameA2.Enabled = true;
                            evtNameA2.DrawMode = DrawMode.OwnerDrawFixed;
                            evtNumA2.Maximum = 255; evtNumA2.Enabled = true;
                            evtNumA2.Value = esc.Param2;
                            evtNameA2.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA2.Value);
                            break;
                        case 7: // menu tutorial
                            labelEvtA2.Text = "Tutorial";
                            evtNameA2.Items.AddRange(new string[] { "How to equip", "How to use items", "How to switch allies", "How to play beetle mania" });
                            evtNameA2.Enabled = true;
                            evtNameA2.SelectedIndex = esc.Param2;
                            break;
                        case 16:    // world map event
                            labelEvtA2.Text = "Map event";
                            evtNameA2.Items.AddRange(new string[] { "Mario falls to pipehouse", "Mario returns to MK", "Mario takes Nimbus bus" });
                            evtNameA2.Enabled = true;
                            evtNameA2.SelectedIndex = esc.Param2;
                            break;
                    }
                    break;
                // Jump to
                case 0xD2:
                case 0xD3:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Address";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                case 0xD4:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Count";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = esc.Param1;
                    break;
                case 0xD5:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Frame timer";
                    evtNumA3.Maximum = 0xFFFF;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                // Screen effects
                case 0x72:
                case 0x73:
                case 0x76:
                case 0x77:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Duration";
                    evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1;
                    break;
                case 0x78:
                case 0x79:
                case 0x83:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Color";
                    evtNameA1.Items.AddRange(Lists.ColorNames); evtNameA1.Enabled = true;
                    if (esc.Opcode != 0x83)
                    {
                        labelEvtA3.Text = "Duration";
                        evtNumA3.Enabled = true;
                        evtNumA3.Value = esc.Param1;
                        evtNameA1.SelectedIndex = esc.Param2;
                    }
                    else
                        evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x80:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Red";
                    labelEvtA2.Text = "Green";
                    labelEvtA3.Text = "Blue";
                    labelEvtA4.Text = "Speed";
                    groupBoxB.Text = "Tint layers";
                    evtNumA1.Enabled = true;
                    evtNumA2.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtEffects.Items.AddRange(Lists.LayerNames); evtEffects.Enabled = true;
                    double multiplier = 8; // 8;
                    ushort color = Bits.GetShort(esc.CommandData, 1);
                    evtNumA1.Value = (byte)((color % 0x20) * multiplier);
                    evtNumA2.Value = (byte)(((color >> 5) % 0x20) * multiplier);
                    evtNumA3.Value = (byte)(((color >> 10) % 0x20) * multiplier);
                    evtNumA4.Value = esc.Param4;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        evtEffects.SetItemChecked(j, (esc.Param3 & i) == i);
                    break;
                case 0x81:
                    groupBoxB.Text = "Mainscreen / Subscreen / Color math";
                    evtEffects.ColumnWidth = 66;
                    evtEffects.Items.AddRange(new string[]
                    {
                        "L1","L2","L3","NPC",
                        "L1","L2","L3","NPC",
                        "L1","L2","L3","NPC", "BG", "½ intensity", "Minus subscreen"
                    });
                    evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x01) == 0x01);
                    evtEffects.SetItemChecked(1, (esc.Param1 & 0x02) == 0x02);
                    evtEffects.SetItemChecked(2, (esc.Param1 & 0x04) == 0x04);
                    evtEffects.SetItemChecked(3, (esc.Param1 & 0x10) == 0x10);
                    evtEffects.SetItemChecked(4, (esc.Param2 & 0x01) == 0x01);
                    evtEffects.SetItemChecked(5, (esc.Param2 & 0x02) == 0x02);
                    evtEffects.SetItemChecked(6, (esc.Param2 & 0x04) == 0x04);
                    evtEffects.SetItemChecked(7, (esc.Param2 & 0x10) == 0x10);
                    evtEffects.SetItemChecked(8, (esc.Param3 & 0x01) == 0x01);
                    evtEffects.SetItemChecked(9, (esc.Param3 & 0x02) == 0x01);
                    evtEffects.SetItemChecked(10, (esc.Param3 & 0x04) == 0x01);
                    evtEffects.SetItemChecked(11, (esc.Param3 & 0x08) == 0x01);
                    evtEffects.SetItemChecked(12, (esc.Param3 & 0x20) == 0x20);
                    evtEffects.SetItemChecked(13, (esc.Param3 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(14, (esc.Param3 & 0x80) == 0x80);
                    break;
                case 0x84:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Pixel size";
                    labelEvtA4.Text = "Duration";
                    groupBoxB.Text = "Apply to layers";
                    evtNumA3.Maximum = 15; evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 63; evtNumA4.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "L1", "L2", "L3", "L4" }); evtEffects.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1 >> 4;
                    evtNumA4.Value = esc.Param2 & 0x3F;
                    for (int i = 1, j = 0; j < 4; i *= 2, j++)
                        evtEffects.SetItemChecked(j, (esc.Param1 & i) == i);
                    break;
                case 0x89:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Type";
                    labelEvtA2.Text = "Duration";
                    labelEvtA3.Text = "Palette set";
                    labelEvtA4.Text = "Palette row";
                    evtNameA1.Items.AddRange(new string[] { "nothing", "glow", "set to", "fade to" });
                    evtNameA1.Enabled = true;
                    evtNumA2.Maximum = 15; evtNumA2.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 16; evtNumA4.Enabled = true;
                    evtEffects.Enabled = true;
                    //
                    switch (esc.Param1 & 0xE0)
                    {
                        case 0x60: evtNameA1.SelectedIndex = 1; break;
                        case 0xC0: evtNameA1.SelectedIndex = 2; break;
                        case 0xE0: evtNameA1.SelectedIndex = 3; break;
                        default: evtNameA1.SelectedIndex = 0; break;
                    }
                    evtNumA2.Value = esc.Param1 & 0x0F;
                    evtNumA3.Value = esc.Param3;
                    evtNumA4.Value = esc.Param2;
                    break;
                case 0x8A:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Palette set";
                    labelEvtA4.Text = "Row 0 to";
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 16; evtNumA4.Minimum = 1; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param2;
                    evtNumA4.Value = (esc.Param1 >> 4) + 1;
                    break;
                case 0x87:
                case 0x8F:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA3.Text = "Width";
                    labelEvtA4.Text = "Speed";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNumA3.Value = esc.Param2;
                    evtNumA4.Value = esc.Param3;
                    break;
                // Playback audio
                case 0x90:
                case 0x91:
                case 0x92:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Music";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames)); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 250;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x95:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Time stretch";
                    labelEvtA4.Text = "To volume";
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1;
                    evtNumA4.Value = esc.Param2;
                    break;
                case 0x97:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Type";
                    labelEvtA3.Text = "Tempo change";
                    labelEvtA4.Text = "Time stretch";
                    evtNameA1.Items.AddRange(new string[] { "slow down", "speed up" }); evtNameA1.Enabled = true;
                    evtNumA3.Enabled = true; evtNumA3.Maximum = 127;
                    evtNumA4.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = (esc.Param2 & 0x80) == 0x80 ? 1 : 0;
                    evtNumA3.Value = Math.Abs((sbyte)esc.Param2);
                    evtNumA4.Value = esc.Param1;
                    break;
                case 0x98:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Type";
                    labelEvtA3.Text = "Pitch change";
                    labelEvtA4.Text = "Time stretch";
                    evtNameA1.Items.AddRange(new string[] { "raise", "lower" }); evtNameA1.Enabled = true;
                    evtNumA3.Enabled = true; evtNumA3.Maximum = 127;
                    evtNumA4.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = (esc.Param2 & 0x80) == 0x80 ? 1 : 0;
                    evtNumA3.Value = Math.Abs((sbyte)esc.Param2);
                    evtNumA4.Value = esc.Param1;
                    break;
                case 0x9C:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 250;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x9D:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    labelEvtA3.Text = "Balance";
                    evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                    evtNameA1.DropDownWidth = 250;
                    evtNumA3.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNumA3.Value = esc.Param2;
                    break;
                case 0x9E:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Time stretch";
                    labelEvtA4.Text = "To volume";
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtNumA3.Value = esc.Param1;
                    evtNumA4.Value = esc.Param2;
                    break;
                // Memory
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Bit";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x709F; evtNumA3.Minimum = 0x7040;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 7; evtNumA4.Enabled = true;
                    //
                    if (esc.Opcode < 0xA4)
                        evtNumA3.Value = ((((esc.Opcode - 0xA0) * 0x100) + esc.Param1) >> 3) + 0x7040;
                    else
                        evtNumA3.Value = ((((esc.Opcode - 0xA4) * 0x100) + esc.Param1) >> 3) + 0x7040;
                    evtNumA4.Value = esc.Param1 & 7;
                    break;
                case 0xA8:
                case 0xA9:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1 + 0x70A0;
                    evtNumA4.Value = esc.Param2;
                    break;
                case 0xAA:
                case 0xAB:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1 + 0x70A0;
                    break;
                case 0xB0:
                case 0xB1:
                case 0xC2:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (esc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(esc.CommandData, 2);
                    break;
                case 0xB2:
                case 0xB3:
                case 0xD6:
                case 0xBB:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = (esc.Param1 * 2) + 0x7000;
                    break;
                case 0xB5:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = esc.Param1 + 0x70A0;
                    break;
                case 0xB7:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Number";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (esc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(esc.CommandData, 2);
                    break;
                case 0xBC:
                case 0xBD:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory A";
                    labelEvtA4.Text = "Memory B";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Increment = 2;
                    evtNumA4.Maximum = 0x71FE; evtNumA4.Minimum = 0x7000;
                    evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (esc.Param2 * 2) + 0x7000;
                    evtNumA4.Value = (esc.Param1 * 2) + 0x7000;
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Bit";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x709F; evtNumA3.Minimum = 0x7040;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 7; evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    if (esc.Opcode < 0xDC)
                        evtNumA3.Value = ((((esc.Opcode - 0xD8) * 0x100) + esc.Param1) >> 3) + 0x7040;
                    else
                        evtNumA3.Value = ((((esc.Opcode - 0xDC) * 0x100) + esc.Param1) >> 3) + 0x7040;
                    evtNumA4.Value = esc.Param1 & 7;
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 2);
                    break;
                case 0xE0:
                case 0xE1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    evtNumA3.Value = esc.Param1 + 0x70A0;
                    evtNumA4.Value = esc.Param2;
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                case 0xE4:
                case 0xE5:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    evtNumA3.Value = (esc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(esc.CommandData, 2);
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 4);
                    break;
                case 0xE8:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                case 0xE9:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    labelEvtA4.Text = "Else jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Maximum = 0xFFFF; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    evtNumA4.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                // Memory $7000
                case 0x38:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Slot";
                    evtNumA3.Maximum = 4; evtNumA3.Enabled = true;
                    //
                    if (esc.Param1 < 8) esc.Param1 = 8;
                    evtNumA3.Value = esc.Param1 - 8;
                    break;
                case 0xAC:
                case 0xAD:
                case 0xB6:
                case 0xC0:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Value";
                    evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                case 0xB4:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = esc.Param1 + 0x70A0;
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xC1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = (esc.Param1 * 2) + 0x7000;
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Units";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(new string[] { "pixel", "isometric" }); evtNameA2.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1 & 0x3F;
                    evtNameA2.SelectedIndex = (esc.Param1 & 0x40) >> 6;
                    break;
                case 0xC7:
                case 0xC8:
                case 0xC9:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0xDB:
                case 0xDF:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                case 0xE2:
                case 0xE3:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Value";
                    labelEvtA4.Text = "Jump to";
                    evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Maximum = 0xFFFF; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    evtNumA4.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                case 0xE6:
                case 0xE7:
                    groupBoxB.Text = commandText;
                    evtEffects.ColumnWidth = 66;
                    evtEffects.Items.AddRange(new string[]{
                        "bit 0","bit 1","bit 2","bit 3","bit 4","bit 5","bit 6","bit 7",
                        "bit 8","bit 9","bit 10","bit 11","bit 12","bit 13","bit 14","bit 15"});
                    evtEffects.Enabled = true;
                    labelEvtC1.Text = "Jump to";
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    for (int b = 1, i = 0; i < 16; b *= 2, i++)
                        evtEffects.SetItemChecked(i, (Bits.GetShort(esc.CommandData, 1) & b) == b);
                    evtNumC1.Value = Bits.GetShort(esc.CommandData, 3);
                    break;
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1);
                    break;
                // Pause script
                case 0xF0:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Frames";
                    evtNumA3.Enabled = true;
                    evtNumA3.Minimum = 1; evtNumA3.Maximum = 256;
                    evtNumA3.Value = esc.Param1 + 1;
                    break;
                case 0xF1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Frames";
                    evtNumA3.Minimum = 1; evtNumA3.Maximum = 65536; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(esc.CommandData, 1) + 1;
                    break;
                default:
                    // Action Queue (same for all sub-indexes, so no need to do a switch for sub)
                    if (esc.Opcode <= 0x2F)
                    {
                        groupBoxA.Text = commandText;
                        labelEvtA1.Text = "Object";
                        labelEvtA2.Text = "Queue";
                        evtNameA1.Items.AddRange(Lists.ObjectNames);
                        evtNameA2.Items.AddRange(new string[]
                        {                                           // OPTIONS:
                        "action queue",                   // 0x00 - 0x7F
                        "start embedded action script",         // 0xF0
                        "start embedded action script",         // 0xF1
                        "set action script (sync)",             // 0xF2
                        "set action script (async)",         // 0xF3
                        "set temp action script (sync)",        // 0xF4
                        "set temp action script (async)",    // 0xF5
                        "un-sync action script",                // 0xF6
                        "summon to current level @ Mario's coords",         // 0xF7
                        "summon to current level",                          // 0xF8
                        "remove from current level",                        // 0xF9
                        "pause action script",                  // 0xFA
                        "resume action script",                 // 0xFB
                        "enable trigger",                       // 0xFC
                        "disable trigger",                      // 0xFD
                        "stop embedded action script",          // 0xFE
                        "reset coords"          // 0xFF
                        });
                        evtNameA2.DropDownWidth = 210;
                        evtNameA1.Enabled = true;
                        evtNameA2.Enabled = true;
                        evtNameA1.SelectedIndex = esc.Opcode;
                        //
                        if (esc.Param1 >= 0xF2)
                        {
                            evtNameA2.SelectedIndex = esc.Param1 - 0xEF;
                        }
                        else evtNameA2.SelectedIndex = 0;
                        if (esc.Param1 >= 0xF2 && esc.Param1 <= 0xF5)
                        {
                            labelEvtA3.Text = "Action #";
                            evtNumA3.Maximum = 0x3FF; evtNumA3.Enabled = true;
                            evtNumA3.Value = Bits.GetShort(esc.CommandData, 2);
                        }
                        else if (esc.Param1 < 0xF2)
                        {
                            evtEffects.Items.AddRange(new string[] { "asynchronous" }); evtEffects.Enabled = true;
                            evtEffects.SetItemChecked(0, (esc.Param1 & 0x80) == 0x80);
                        }
                    }
                    else if (esc.Opcode == 0xFD)
                    {
                        switch (esc.Param1)
                        {
                            // Objects
                            case 0x33:          // If running action script, object...
                            case 0x34:         // If underwater, object...
                            case 0x3D:         // If in air, object...
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Object";
                                labelEvtA3.Text = "Jump to";
                                evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                                evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                                //
                                evtNameA1.SelectedIndex = esc.Param2;
                                evtNumA3.Value = Bits.GetShort(esc.CommandData, 3);
                                break;
                            case 0x3E:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Packet";
                                labelEvtA2.Text = "Event #";
                                groupBoxC.Text = "If null...";
                                labelEvtC1.Text = "Jump to";
                                evtNameA1.Items.AddRange(Lists.Numerize(Lists.NPCPackets)); evtNameA1.Enabled = true;
                                evtNameA1.DropDownWidth = 200;
                                evtNumA2.Maximum = 4095; evtNumA2.Enabled = true;
                                evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                                //
                                evtNameA1.SelectedIndex = esc.Param2;
                                evtNumA2.Value = Bits.GetShort(esc.CommandData, 3) & 0xFFF;
                                evtNumC1.Value = Bits.GetShort(esc.CommandData, 5);
                                break;
                            // Menus
                            case 0x4C:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Menu";
                                evtNameA1.Items.AddRange(Lists.Tutorials); evtNameA1.Enabled = true;
                                evtNameA1.SelectedIndex = esc.Param2;
                                break;
                            // Run event
                            case 0x46:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Event #";
                                evtNumA3.Maximum = 4095;
                                evtNumA3.Enabled = true;
                                evtNumA3.Value = Bits.GetShort(esc.CommandData, 2);
                                break;
                            case 0x4D:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Star #";
                                evtNumA3.Maximum = 7; evtNumA3.Minimum = 1; evtNumA3.Enabled = true;
                                if (esc.Param2 < 1)
                                    esc.Param2 = 1;
                                evtNumA3.Value = esc.Param2;
                                break;
                            case 0x66:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Title";
                                labelEvtA3.Text = "Y (in tiles)";
                                evtNameA1.Items.AddRange(new string[] { "Super Mario", "Princess Toadstool", "King Bowser", "Mallow", "Geno", "In..." });
                                evtNameA1.Enabled = true;
                                evtNumA3.Enabled = true;
                                evtNameA1.SelectedIndex = esc.Param3;
                                evtNumA3.Value = esc.Param2;
                                break;
                            // Playback audio
                            case 0x94:
                                groupBoxB.Text = commandText;
                                evtEffects.Items.AddRange(new string[] { "0", "1", "2", "3", "4", "5", "6", "7" });
                                evtEffects.Enabled = true;
                                for (int i = 1, j = 0; j < 8; i *= 2, j++)
                                    evtEffects.SetItemChecked(j, (esc.Param2 & i) == i);
                                break;
                            case 0x96:
                            case 0x97:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Value";
                                evtNumA3.Enabled = true;
                                evtNumA3.Value = esc.Param2;
                                labelEvtA4.Text = "Jump to";
                                evtNumA4.Enabled = true;
                                evtNumA4.Maximum = 0xFFFF;
                                evtNumA4.Hexadecimal = true;
                                evtNumA4.Value = Bits.GetShort(esc.CommandData, 3);
                                break;
                            case 0x9C:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Sound";
                                evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                                evtNameA1.SelectedIndex = esc.Param2;
                                break;
                            // Memory
                            case 0xB6:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Memory";
                                labelEvtA4.Text = "Shift";
                                evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                                evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                                evtNumA3.Enabled = true;
                                evtNumA4.Maximum = 256; evtNumA4.Minimum = 1; evtNumA4.Enabled = true;
                                evtNumA3.Value = (esc.Param2 * 2) + 0x7000;
                                evtNumA4.Value = (esc.Param3 ^ 0xFF) + 1;
                                break;
                            case 0xB7:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Memory";
                                evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                                evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                                evtNumA3.Enabled = true;
                                evtNumA3.Value = (esc.Param2 * 2) + 0x7000;
                                break;
                            // Memory $7000
                            case 0x58:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Item";
                                evtNameA1.Items.AddRange(Model.ItemNames.Names); evtNameA1.Enabled = true;
                                evtNameA1.DrawMode = DrawMode.OwnerDrawFixed;
                                evtNumA1.Maximum = 255; evtNumA1.Enabled = true;
                                evtNumA1.Value = esc.Param2;
                                evtNameA1.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA1.Value);
                                break;
                            case 0x5D:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Character";
                                labelEvtA2.Text = "Type";
                                evtNameA1.Items.AddRange(Lists.CharacterNames); evtNameA1.Enabled = true;
                                evtNameA2.Items.AddRange(new string[] { "weapon", "armor", "accessory" });
                                evtNameA2.Enabled = true;
                                evtNameA1.SelectedIndex = esc.Param2;
                                evtNameA2.SelectedIndex = esc.Param3;
                                break;
                            case 0xAC:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Memory";
                                evtNumA3.Hexadecimal = true;
                                evtNumA3.Maximum = 0x7FFFFF; evtNumA3.Minimum = 0x7FF800;
                                evtNumA3.Enabled = true;
                                evtNumA3.Value = Bits.GetShort(esc.CommandData, 2) + 0x7FF800;
                                break;
                            case 0xB0:
                            case 0xB1:
                            case 0xB2:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Value";
                                evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                                evtNumA3.Value = Bits.GetShort(esc.CommandData, 2);
                                break;
                            case 0xB3:
                            case 0xB4:
                            case 0xB5:
                                groupBoxA.Text = commandText;
                                labelEvtA3.Text = "Memory";
                                evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                                evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                                evtNumA3.Enabled = true;
                                evtNumA3.Value = (esc.Param2 * 2) + 0x7000;
                                break;
                            case 0xF0:
                                groupBoxA.Text = commandText;
                                labelEvtA1.Text = "Level";
                                labelEvtA2.Text = "Object";
                                labelEvtC1.Text = "Jump to";
                                evtNameA1.Items.AddRange(Lists.Numerize(Lists.LevelNames)); evtNameA1.Enabled = true;
                                evtNameA1.DropDownWidth = 500;
                                evtNameA2.Items.AddRange(Lists.ObjectNames); evtNameA2.Enabled = true;
                                evtNumA1.Enabled = true; evtNumA1.Maximum = 511;
                                evtEffects.Items.AddRange(new object[] { "is enabled" });
                                evtEffects.Enabled = true;
                                evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                                //
                                evtNumA1.Value = Bits.GetShort(esc.CommandData, 2) & 0x1FF;
                                evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                                evtNameA2.SelectedIndex = (esc.Param3 >> 1) & 0x3F;
                                evtEffects.SetItemChecked(0, (esc.Param3 & 0x80) == 0x80);
                                evtNumC1.Value = Bits.GetShort(esc.CommandData, 4);
                                break;
                            default: break;
                        }
                    }
                    break;
            }
            OrganizeControls();
            //
            panelCommands.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssembleEvent()
        {
            switch (esc.Opcode)
            {
                // Objects
                case 0x32:         // If object present...
                case 0x39:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA3.Value);
                    break;
                case 0x3A:         // If distance between object A and...
                case 0x3B:         // If distance (Z==) between object A and...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;          // object A
                    esc.Param2 = (byte)evtNameA2.SelectedIndex;    // object B
                    esc.Param3 = (byte)evtNumA3.Value;
                    esc.Param4 = (byte)evtNumA4.Value;
                    Bits.SetShort(esc.CommandData, 5, (ushort)evtNumC1.Value);
                    break;
                case 0x3D:         // If Mario in air...
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumC1.Value);
                    break;
                case 0x3E:         // Create NPC packet @ obj coords...
                    esc.Param2 = (byte)evtNameA1.SelectedIndex;
                    esc.Param1 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0x3F:         // Create NPC packet...
                    esc.Param1 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0x42:         // If Mario on top of an object...
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumC1.Value);
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumC2.Value);
                    break;
                case 0xF2:         // Set obj presence...  
                case 0xF3:         // Set obj engage type...
                case 0xF8:         // If object in level ..., presence =...
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA1.Value);
                    esc.Param2 &= 1; esc.Param2 |= (byte)(evtNameA2.SelectedIndex << 1);
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(0));    // set bit 7 if true
                    if (esc.Opcode == 0xF8)
                        Bits.SetShort(esc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                // Joypad
                case 0x34:
                case 0x35:
                    for (int i = 0; i < 8; i++)
                        Bits.SetBit(esc.CommandData, 1, i, evtEffects.GetItemChecked(i)); // set bit if true
                    break;
                // Party Members
                case 0x36:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 1, 7, evtNameA2.SelectedIndex == 1);
                    break;
                case 0x54:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA2.Value;
                    break;
                case 0x56:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                // Inventory
                case 0x50:
                case 0x51:
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x52:
                case 0x53:
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Battle
                case 0x4A:
                    esc.Param1 = (byte)evtNumA3.Value;
                    esc.Param3 = (byte)evtNumA2.Value;
                    break;
                // Levels
                case 0x4B:      // Open, world location...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 2, 5, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 6, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(2));
                    break;
                case 0x68:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA1.Value);
                    esc.Param5 = (byte)evtNumA2.Value;
                    esc.Param5 &= 0x1F; esc.Param5 |= (byte)(evtNameA2.SelectedIndex << 5);
                    esc.Param3 = (byte)evtNumA3.Value;
                    esc.Param4 = (byte)evtNumA4.Value;
                    Bits.SetBit(esc.CommandData, 2, 3, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 4, 7, evtEffects.GetItemChecked(2));
                    break;
                case 0x6A:
                case 0x6B:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA1.Value);
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(0));
                    esc.Param2 &= 0x81;
                    esc.Param2 |= (byte)((byte)evtNumA3.Value << 1);
                    break;
                // Open window
                case 0x4C:      // Open, shop menu...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x4F:      // Open, window...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                // Dialogue
                case 0x60:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA1.Value);
                    esc.Param3 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 2, 5, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 3, 6, evtEffects.GetItemChecked(2));
                    Bits.SetBit(esc.CommandData, 3, 7, evtEffects.GetItemChecked(3));
                    break;
                case 0x61:
                    esc.Param2 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 1, 5, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 2, 6, evtEffects.GetItemChecked(2));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(3));
                    break;
                case 0x62:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA1.Value);
                    esc.Param2 &= 0x1F; esc.Param2 |= (byte)((byte)evtNumA3.Value << 5);
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(0));
                    break;
                case 0x63:
                    Bits.SetBit(esc.CommandData, 1, 5, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(1));
                    break;
                case 0x66:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0x67:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA4.Value);
                    break;
                // Events
                case 0x40:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetBit(esc.CommandData, 2, 5, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 6, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(2));
                    break;
                case 0x44:
                case 0x45:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetBit(esc.CommandData, 2, 4, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 5, evtEffects.GetItemChecked(1));
                    esc.Param2 |= (byte)((((int)evtNumA4.Value - 0x701C) / 2) << 6);
                    break;
                case 0x46:
                case 0x47:
                    esc.Param1 = (byte)(((int)evtNumA3.Value - 0x701C) / 2);
                    break;
                case 0xD0:
                case 0xD1:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0x4E:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    switch (evtNameA1.SelectedIndex)
                    {
                        case 2: // open world location
                            esc.Param2 = (byte)evtNameA2.SelectedIndex;
                            break;
                        case 3: // open shop menu
                            esc.Param2 = (byte)evtNameA2.SelectedIndex;
                            break;
                        case 5: // items maxed out
                            esc.Param2 = (byte)evtNumA2.Value;
                            break;
                        case 7: // menu tutorial
                            esc.Param2 = (byte)evtNameA2.SelectedIndex;
                            break;
                        case 8: // add star piece
                        case 13:// run star piece end sequence
                            esc.Param2 = (byte)evtNumA2.Value;
                            break;
                        case 16:    // world map event
                            esc.Param2 = (byte)evtNameA2.SelectedIndex;
                            break;
                    }
                    break;
                // Jump to
                case 0xD2:
                case 0xD3:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xD4:
                    esc.Param1 = (byte)evtNumA3.Value;
                    break;
                case 0xD5:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Screen effects
                case 0x72:
                case 0x73:
                case 0x76:
                case 0x77:
                    esc.Param1 = (byte)evtNumA3.Value;
                    break;
                case 0x78:
                case 0x79:
                case 0x83:
                    if (esc.Opcode != 0x83)
                    {
                        esc.Param1 = (byte)evtNumA3.Value;
                        esc.Param2 = (byte)evtNameA1.SelectedIndex;
                    }
                    else
                        esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x80:
                    ushort color;
                    int r, g, b;
                    r = (int)(evtNumA1.Value / 8);
                    g = (int)(evtNumA2.Value / 8);
                    b = (int)(evtNumA3.Value / 8);
                    color = (ushort)((b << 10) | (g << 5) | r);
                    Bits.SetShort(esc.CommandData, 1, color);
                    esc.Param4 = (byte)evtNumA4.Value;
                    for (int i = 0; i < 8; i++)
                        Bits.SetBit(esc.CommandData, 3, i, evtEffects.GetItemChecked(i));
                    break;
                case 0x81:
                    Bits.SetBit(esc.CommandData, 1, 0, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 1, 1, evtEffects.GetItemChecked(1));
                    Bits.SetBit(esc.CommandData, 1, 2, evtEffects.GetItemChecked(2));
                    Bits.SetBit(esc.CommandData, 1, 4, evtEffects.GetItemChecked(3));
                    Bits.SetBit(esc.CommandData, 2, 0, evtEffects.GetItemChecked(4));
                    Bits.SetBit(esc.CommandData, 2, 1, evtEffects.GetItemChecked(5));
                    Bits.SetBit(esc.CommandData, 2, 2, evtEffects.GetItemChecked(6));
                    Bits.SetBit(esc.CommandData, 2, 4, evtEffects.GetItemChecked(7));
                    Bits.SetBit(esc.CommandData, 3, 0, evtEffects.GetItemChecked(8));
                    Bits.SetBit(esc.CommandData, 3, 1, evtEffects.GetItemChecked(9));
                    Bits.SetBit(esc.CommandData, 3, 2, evtEffects.GetItemChecked(10));
                    Bits.SetBit(esc.CommandData, 3, 4, evtEffects.GetItemChecked(11));
                    Bits.SetBit(esc.CommandData, 3, 5, evtEffects.GetItemChecked(12));
                    Bits.SetBit(esc.CommandData, 3, 6, evtEffects.GetItemChecked(13));
                    Bits.SetBit(esc.CommandData, 3, 7, evtEffects.GetItemChecked(14));
                    break;
                case 0x84:
                    esc.Param1 = (byte)((byte)evtNumA3.Value << 4);
                    esc.Param2 = (byte)evtNumA4.Value;
                    for (int i = 0; i < 4; i++)
                        Bits.SetBit(esc.CommandData, 1, i, evtEffects.GetItemChecked(i));
                    break;
                case 0x89:
                    switch (evtNameA1.SelectedIndex)
                    {
                        case 1: esc.Param1 = 0x60; break;
                        case 2: esc.Param1 = 0xC0; break;
                        case 3: esc.Param1 = 0xE0; break;
                        default: esc.Param1 = 0x00; break;
                    }
                    esc.Param1 &= 0xF0; esc.Param1 |= (byte)evtNumA2.Value;
                    esc.Param3 = (byte)evtNumA3.Value;
                    esc.Param2 = (byte)evtNumA4.Value;
                    break;
                case 0x8A:
                    esc.Param2 = (byte)evtNumA3.Value;
                    esc.Param1 = (byte)(((byte)evtNumA4.Value << 4) - 1);
                    break;
                case 0x87:
                case 0x8F:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA3.Value;
                    esc.Param3 = (byte)evtNumA4.Value;
                    break;
                // Playback audio
                case 0x90:
                case 0x91:
                case 0x92:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x95:
                    esc.Param1 = (byte)evtNumA3.Value;
                    esc.Param2 = (byte)evtNumA4.Value;
                    break;
                case 0x97:
                    if (evtNameA1.SelectedIndex == 0) // slow down
                        esc.Param2 = (byte)evtNumA3.Value;
                    else // speed up
                        esc.Param2 = (byte)((sbyte)-evtNumA3.Value);
                    esc.Param1 = (byte)evtNumA4.Value;
                    break;
                case 0x98:
                    if (evtNameA1.SelectedIndex == 0) // raise
                        esc.Param2 = (byte)evtNumA3.Value;
                    else // lower
                        esc.Param2 = (byte)((sbyte)-evtNumA3.Value);
                    esc.Param1 = (byte)evtNumA4.Value;
                    break;
                case 0x9C:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x9D:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA3.Value;
                    break;
                case 0x9E:
                    esc.Param1 = (byte)evtNumA3.Value;
                    esc.Param2 = (byte)evtNumA4.Value;
                    break;
                // Memory
                case 0xA0:
                case 0xA1:
                case 0xA2:
                    esc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xA0);
                    esc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    esc.Param1 &= 0xF8; esc.Param1 |= (byte)evtNumA4.Value;
                    break;
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    esc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xA4);
                    esc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    esc.Param1 &= 0xF8; esc.Param1 |= (byte)evtNumA4.Value;
                    break;
                case 0xA8:
                case 0xA9:
                    esc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    esc.Param2 = (byte)evtNumA4.Value;
                    break;
                case 0xAA:
                case 0xAB:
                    esc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB0:
                case 0xB1:
                case 0xC2:
                    esc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA4.Value);
                    break;
                case 0xB2:
                case 0xB3:
                case 0xD6:
                case 0xBB:
                    esc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    break;
                case 0xB5:
                    esc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB7:
                    esc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA4.Value);
                    break;
                case 0xBC:
                case 0xBD:
                    esc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    esc.Param1 = (byte)((evtNumA4.Value - 0x7000) / 2);
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                    esc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xD8);
                    esc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    esc.Param1 &= 0xF8; esc.Param1 |= (byte)evtNumA4.Value;
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    esc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xDC);
                    esc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    esc.Param1 &= 0xF8; esc.Param1 |= (byte)evtNumA4.Value;
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0xE0:
                case 0xE1:
                    esc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    esc.Param2 = (byte)evtNumA4.Value;
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0xE4:
                case 0xE5:
                    esc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA4.Value);
                    Bits.SetShort(esc.CommandData, 4, (ushort)evtNumC1.Value);
                    break;
                case 0xE8:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xE9:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA4.Value);
                    break;
                // Memory $7000
                case 0x38:
                    if (esc.Param1 < 8) esc.Param1 = 8;
                    esc.Param1 = (byte)(evtNumA3.Value + 8);
                    break;
                case 0xAC:
                case 0xAD:
                case 0xB6:
                case 0xC0:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xB4:
                    esc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xC1:
                    esc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 1, 6, evtNameA2.SelectedIndex == 1);
                    break;
                case 0xC7:
                case 0xC8:
                case 0xC9:
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0xDB:
                case 0xDF:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xE2:
                case 0xE3:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA4.Value);
                    break;
                case 0xE6:
                case 0xE7:
                    for (int i = 0; i < 16; i++)
                        Bits.SetBit(esc.CommandData, 1, i, evtEffects.GetItemChecked(i));
                    Bits.SetShort(esc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    Bits.SetShort(esc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Pause script
                case 0xF0:
                    esc.Param1 = (byte)(evtNumA3.Value - 1);
                    break;
                case 0xF1:
                    Bits.SetShort(esc.CommandData, 1, (ushort)(evtNumA3.Value - 1));
                    break;
                default:
                    // Action Queue (same for all sub-indexes, so no need to do a switch for sub)
                    if (esc.Opcode <= 0x2F)
                    {
                        byte temp = esc.Opcode;
                        switch (evtNameA2.SelectedIndex)
                        {
                            case 0:
                                esc.Opcode = (byte)evtNameA1.SelectedIndex;
                                Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                                break;
                            case 1:
                            case 2:
                                esc.CommandData = new byte[3];
                                esc.Opcode = (byte)evtNameA1.SelectedIndex;
                                esc.Param1 = (byte)(evtNameA2.SelectedIndex + 0xEF);
                                Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(0));
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                esc.CommandData = new byte[4];
                                esc.Opcode = (byte)evtNameA1.SelectedIndex;
                                esc.Param1 = (byte)(evtNameA2.SelectedIndex + 0xEF);
                                Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA3.Value);
                                break;
                            default:
                                esc.Opcode = (byte)evtNameA1.SelectedIndex;
                                esc.Param1 = (byte)(evtNameA2.SelectedIndex + 0xEF);
                                break;
                        }
                        /*
                         * TODO
                         * set evtNumC value and labelEvtC text according to evtNameB.SelectedIndex
                         */
                    }
                    else if (esc.Opcode == 0xFD)
                    {
                        switch (esc.Param1)
                        {
                            // Objects
                            case 0x33:
                            case 0x34:
                            case 0x3D:         // If in air, object...
                                esc.Param2 = (byte)evtNameA1.SelectedIndex;
                                Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA3.Value);
                                break;
                            case 0x3E:
                                esc.Param2 = (byte)evtNameA1.SelectedIndex;
                                Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA2.Value);
                                Bits.SetShort(esc.CommandData, 5, (ushort)evtNumC1.Value);
                                break;
                            case 0xF0:
                                Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA1.Value);
                                esc.Param3 &= 1; esc.Param3 |= (byte)(evtNameA2.SelectedIndex << 1);
                                Bits.SetBit(esc.CommandData, 3, 7, evtEffects.GetItemChecked(0));    // set bit 7 if true
                                Bits.SetShort(esc.CommandData, 4, (ushort)evtNumC1.Value);
                                break;
                            // Menus
                            case 0x4C:
                                esc.Param2 = (byte)evtNameA1.SelectedIndex;
                                break;
                            // Run event
                            case 0x46:
                                Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA3.Value);
                                break;
                            case 0x4D:
                                esc.Param2 = (byte)evtNumA3.Value;
                                break;
                            case 0x66:
                                esc.Param3 = (byte)evtNameA1.SelectedIndex;
                                esc.Param2 = (byte)evtNumA3.Value;
                                break;
                            // Playback audio
                            case 0x94:
                                for (int i = 0; i < 8; i++)
                                    Bits.SetBit(esc.CommandData, 2, i, evtEffects.GetItemChecked(i));
                                break;
                            case 0x96:
                            case 0x97:
                                esc.Param2 = (byte)evtNumA3.Value;
                                Bits.SetShort(esc.CommandData, 3, (ushort)evtNumA4.Value);
                                break;
                            case 0x9C:
                                esc.Param2 = (byte)evtNameA1.SelectedIndex;
                                break;
                            // Memory
                            case 0xB6:
                                esc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                                esc.Param3 = (byte)((byte)(evtNumA4.Value - 1) ^ 0xFF);
                                break;
                            case 0xB7:
                                esc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                                break;
                            // Memory $7000
                            case 0x58:
                                esc.Param2 = (byte)evtNumA1.Value;
                                break;
                            case 0x5D:
                                esc.Param2 = (byte)evtNameA1.SelectedIndex;
                                esc.Param3 = (byte)evtNameA2.SelectedIndex;
                                break;
                            case 0xAC:
                                Bits.SetShort(esc.CommandData, 2, (ushort)(evtNumA3.Value - 0x7FF800));
                                break;
                            case 0xB0:
                            case 0xB1:
                            case 0xB2:
                                Bits.SetShort(esc.CommandData, 2, (ushort)evtNumA3.Value);
                                break;
                            case 0xB3:
                            case 0xB4:
                            case 0xB5:
                                esc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                                break;
                            default: break;
                        }
                    }
                    break;
            }
        }
        //
        private void ControlDisassembleAction()
        {
            this.Updating = true;
            panelCommands.SuspendDrawing();
            int[] tree = categoryCommand;
            if (tree != null)
            {
                categories_aq.SelectedIndex = tree[0];
                commands.SelectedIndex = tree[1];
            }
            switch (asc.Opcode)
            {
                // Properties
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x15:
                    groupBoxB.Text = commandText;
                    evtEffects.Items.AddRange(new string[] 
                        { 
                            "bit 0", "can't walk under", "can't pass walls", "can't jump through", 
                            "bit 4", "can't pass NPCs", "can't walk through", "bit 7", 
                        });
                    evtEffects.Enabled = true;
                    for (int i = 1, j = 0; j < 8; i *= 2, j++)
                        evtEffects.SetItemChecked(j, (asc.Param1 & i) == i);
                    break;
                case 0x13:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "VRAM priority";
                    evtNumA3.Maximum = 3; evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    break;
                case 0x3D:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                // Palette
                case 0x0D: evtNumA3.Maximum = 15; goto case 0x0E;
                case 0x0E:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Row";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1 & 0x0F;
                    break;
                // Sprite Sequence
                case 0x08:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Sprite +=";
                    evtNumA3.Maximum = 7; evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 127; evtNumA4.Enabled = true;
                    evtEffects.Items.AddRange(new string[]
                    {
                        "read as mold",
                        "looping off",
                        "read as sequence",
                        "mirror sprite"
                    });
                    evtEffects.Enabled = true;
                    evtNumA3.Value = asc.Param1 & 0x07;
                    evtNumA4.Value = asc.Param2 & 0x7F;
                    evtEffects.SetItemChecked(0, (asc.Param1 & 0x08) == 0x08);
                    evtEffects.SetItemChecked(1, (asc.Param1 & 0x10) == 0x10);
                    evtEffects.SetItemChecked(2, (asc.Param1 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(3, (asc.Param2 & 0x80) == 0x80);
                    if (evtEffects.GetItemChecked(0))
                        labelEvtA4.Text = "Mold";
                    else
                        labelEvtA4.Text = "Sequence";
                    break;
                case 0x10:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Speed";
                    evtNameA1.Items.AddRange(new string[]
                    {
                        "normal",
                        "fast","faster",
                        "very fast", "fastest",
                        "slow", "very slow"
                    });
                    evtNameA1.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "walking", "sequence" });
                    evtEffects.Enabled = true;
                    //
                    evtNameA1.SelectedIndex = asc.Param1 & 0x0F;
                    evtEffects.SetItemChecked(0, (asc.Param1 & 0x40) == 0x40);
                    evtEffects.SetItemChecked(1, (asc.Param1 & 0x80) == 0x80);
                    break;
                case 0x3A:         // If distance between object A and...
                case 0x3B:         // If distance (Z==) between object A and...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object A";
                    labelEvtA2.Text = "Usually set to 128?";
                    labelEvtA3.Text = "Tiles";
                    groupBoxC.Text = "If in range...";
                    labelEvtC1.Text = "Jump to...";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA2.Enabled = true;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA1.SelectedIndex = asc.Param1;          // object A
                    evtNumA3.Value = asc.Param3;
                    evtNumA2.Value = asc.Param2;
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 4);
                    break;
                case 0x3E:         // Create NPC packet @ obj coords...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Packet";
                    groupBoxC.Text = "If null...";
                    labelEvtC1.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.NPCPackets)); evtNameA2.Enabled = true;
                    evtNameA2.DropDownWidth = 200;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA1.SelectedIndex = asc.Param2;
                    evtNameA2.SelectedIndex = asc.Param1;
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0x3F:         // Create NPC packet...
                    groupBoxA.Text = commandText;
                    labelEvtA2.Text = "Packet";
                    groupBoxC.Text = "If null...";
                    labelEvtC1.Text = "Jump to";
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.NPCPackets)); evtNameA2.Enabled = true;
                    evtNameA2.DropDownWidth = 200;
                    evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNameA2.SelectedIndex = asc.Param1;
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xD0:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Action #";
                    evtNumA3.Maximum = 0x3FF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1) & 0x3FF;
                    break;
                // Sprite Animation

                // Shift isometric units
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x5A:
                case 0x5B:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Steps";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    break;
                case 0x7E: groupBoxA.Text = commandText; goto case 0x7F;
                case 0x7F:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Steps";
                    evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                // Shift 1px units
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x6A:
                case 0x6B:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Pixels";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    break;
                // Face direction
                case 0x7B:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Turn amount";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    break;
                // Shift to coords
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "X";
                    labelEvtA4.Text = "Y";
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    if (asc.Opcode != 0x80 && asc.Opcode != 0x82)
                    {
                        evtNumA3.Minimum = evtNumA4.Minimum = -128;
                        evtNumA3.Maximum = evtNumA4.Maximum = 127;
                    }
                    evtNumA3.Value = asc.Param1;
                    evtNumA4.Value = asc.Param2;
                    break;
                case 0x87:
                case 0x95:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0x90:
                case 0x91:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Bounce peak";
                    labelEvtA3.Text = "X";
                    labelEvtA4.Text = "Y";
                    evtNumA1.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    if (asc.Opcode != 0x90)
                    {
                        evtNumA3.Minimum = evtNumA4.Minimum = -128;
                        evtNumA3.Maximum = evtNumA4.Maximum = 127;
                    }
                    //
                    evtNumA1.Value = asc.Param3;
                    evtNumA3.Value = asc.Param1;
                    evtNumA4.Value = asc.Param2;
                    break;
                case 0x92:
                case 0x93:
                case 0x94:
                    groupBoxA.Text = commandText;
                    labelEvtA2.Text = "F / Z";
                    labelEvtA3.Text = "X";
                    labelEvtA4.Text = "Y";
                    evtNameA2.Items.AddRange(Lists.Directions); evtNameA2.Enabled = true;
                    evtNumA2.Maximum = 0x31; evtNumA2.Enabled = true;
                    if (asc.Opcode != 0x92)
                    {
                        evtNumA3.Minimum = evtNumA4.Minimum = -128;
                        evtNumA3.Maximum = evtNumA4.Maximum = 127;
                    }
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    //
                    evtNameA2.SelectedIndex = (asc.Param3 & 0xE0) >> 5;
                    evtNumA2.Value = asc.Param3 & 0x1F;
                    evtNumA3.Value = asc.Param1;
                    evtNumA4.Value = asc.Param2;
                    break;
                case 0x88:
                case 0x89:
                case 0x98:
                case 0x99:
                    break;
                // Audio playback
                case 0x9C:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = asc.Param1;
                    break;
                case 0x9D:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    labelEvtA3.Text = "Balance";
                    evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                    evtNumA3.Enabled = true;
                    evtNameA1.SelectedIndex = asc.Param1;
                    evtNumA3.Value = asc.Param2;
                    break;
                case 0x9E:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Time stretch";
                    labelEvtA4.Text = "To volume";
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    evtNumA4.Value = asc.Param2;
                    break;
                // Memory
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Bit";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x709F; evtNumA3.Minimum = 0x7040;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 7; evtNumA4.Enabled = true;
                    //
                    if (asc.Opcode < 0xA4)
                        evtNumA3.Value = ((((asc.Opcode - 0xA0) * 0x100) + asc.Param1) >> 3) + 0x7040;
                    else
                        evtNumA3.Value = ((((asc.Opcode - 0xA4) * 0x100) + asc.Param1) >> 3) + 0x7040;
                    evtNumA4.Value = asc.Param1 & 7;
                    break;
                case 0xA8:
                case 0xA9:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = asc.Param1 + 0x70A0;
                    evtNumA4.Value = asc.Param2;
                    break;
                case 0xAA:
                case 0xAB:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1 + 0x70A0;
                    break;
                case 0xB0:
                case 0xB1:
                case 0xC2:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (asc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xB2:
                case 0xB3:
                case 0xD6:
                case 0xBB:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = (asc.Param1 * 2) + 0x7000;
                    break;
                case 0xB5:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1 + 0x70A0;
                    break;
                case 0xB7:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Number";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (asc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xBC:
                case 0xBD:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory A";
                    labelEvtA4.Text = "Memory B";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Increment = 2;
                    evtNumA4.Maximum = 0x71FE; evtNumA4.Minimum = 0x7000;
                    evtNumA4.Enabled = true;
                    //
                    evtNumA3.Value = (asc.Param2 * 2) + 0x7000;
                    evtNumA4.Value = (asc.Param1 * 2) + 0x7000;
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Bit";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x709F; evtNumA3.Minimum = 0x7040;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 7; evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    if (asc.Opcode < 0xDC) evtNumA3.Value = ((((asc.Opcode - 0xD8) * 0x100) + asc.Param1) >> 3) + 0x7040;
                    else evtNumA3.Value = ((((asc.Opcode - 0xDC) * 0x100) + asc.Param1) >> 3) + 0x7040;
                    evtNumA4.Value = asc.Param1 & 7;
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 2);
                    break;
                case 0xE0:
                case 0xE1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    evtNumA3.Value = asc.Param1 + 0x70A0;
                    evtNumA4.Value = asc.Param2;
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0xE4:
                case 0xE5:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    labelEvtA4.Text = "Value";
                    labelEvtC1.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA4.Maximum = 65535; evtNumA4.Enabled = true;
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    evtNumA3.Value = (asc.Param1 * 2) + 0x7000;
                    evtNumA4.Value = Bits.GetShort(asc.CommandData, 2);
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 4);
                    break;
                case 0xE8:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xE9:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    labelEvtA4.Text = "Else jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Maximum = 0xFFFF; evtNumA4.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    evtNumA4.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                // Memory $700C
                case 0xAC:
                case 0xAD:
                case 0xB6:
                case 0xC0:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Value";
                    evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xB4:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true;
                    evtNumA3.Maximum = 0x719F; evtNumA3.Minimum = 0x70A0;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1 + 0x70A0;
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xC1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Memory";
                    evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                    evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = (asc.Param1 * 2) + 0x7000;
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Units";
                    evtNameA1.Items.AddRange(Lists.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(new string[] { "pixel", "isometric" }); evtNameA2.Enabled = true;
                    evtNameA1.SelectedIndex = asc.Param1 & 0x3F;
                    evtNameA2.SelectedIndex = (asc.Param1 & 0x40) >> 6;
                    break;
                case 0xDB:
                case 0xDF:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xE2:
                case 0xE3:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Value";
                    labelEvtA4.Text = "Jump to";
                    evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                    evtNumA4.Hexadecimal = true; evtNumA4.Maximum = 0xFFFF; evtNumA4.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    evtNumA4.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0xE6:
                case 0xE7:
                    groupBoxB.Text = commandText;
                    evtEffects.ColumnWidth = 66;
                    evtEffects.Items.AddRange(new string[]{
                        "bit 0","bit 1","bit 2","bit 3","bit 4","bit 5","bit 6","bit 7",
                        "bit 8","bit 9","bit 10","bit 11","bit 12","bit 13","bit 14","bit 15"});
                    evtEffects.Enabled = true;
                    labelEvtC1.Text = "Jump to";
                    evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF; evtNumC1.Enabled = true;
                    //
                    for (int b = 1, i = 0; i < 16; b *= 2, i++)
                        evtEffects.SetItemChecked(i, (Bits.GetShort(asc.CommandData, 1) & b) == b);
                    evtNumC1.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                // Jump to
                case 0xD2:
                case 0xD3:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Jump to";
                    evtNumA3.Hexadecimal = true; evtNumA3.Maximum = 0xFFFF; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xD4:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Count";
                    evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1;
                    break;
                // Object memory
                case 0xF2:         // Set obj presence...  
                case 0xF3:         // Set obj engage type...
                case 0xF8:         // If object in level ..., presence =...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Level";
                    labelEvtA2.Text = "Object";
                    if (esc.Opcode == 0xF8)
                        labelEvtC1.Text = "Jump to";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LevelNames));
                    evtNameA1.DropDownWidth = 500;
                    evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.ObjectNames); evtNameA2.Enabled = true;
                    evtNumA1.Enabled = true; evtNumA1.Maximum = 511;
                    if (asc.Opcode == 0xF3)
                        evtEffects.Items.AddRange(new object[] { "is enabled" });
                    else
                        evtEffects.Items.AddRange(new object[] { "is present" });
                    evtEffects.Enabled = true;
                    if (esc.Opcode == 0xF8)
                        evtNumC1.Enabled = true; evtNumC1.Hexadecimal = true; evtNumC1.Maximum = 0xFFFF;
                    //
                    evtNumA1.Value = Bits.GetShort(asc.CommandData, 1) & 0x1FF;
                    evtNameA1.SelectedIndex = (int)evtNumA1.Value;
                    evtNameA2.SelectedIndex = (asc.Param2 >> 1) & 0x3F;
                    evtEffects.SetItemChecked(0, (asc.Param2 & 0x80) == 0x80);
                    if (asc.Opcode == 0xF8)
                        evtNumC1.Value = Bits.GetShort(asc.CommandData, 3);
                    break;
                // Pause script
                case 0xF0:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Frames";
                    evtNumA3.Minimum = 1; evtNumA3.Maximum = 256; evtNumA3.Enabled = true;
                    evtNumA3.Value = asc.Param1 + 1;
                    break;
                case 0xF1:
                    groupBoxA.Text = commandText;
                    labelEvtA3.Text = "Frames";
                    evtNumA3.Minimum = 1; evtNumA3.Maximum = 65536; evtNumA3.Enabled = true;
                    evtNumA3.Value = Bits.GetShort(asc.CommandData, 1) + 1;
                    break;
                case 0xFD:
                    switch (asc.Param1)
                    {
                        // shadow on/off
                        case 0x00:
                        case 0x01:
                            groupBoxA.Text = commandText;
                            labelEvtA1.Text = "Shadow Toggle";
                            evtNameA1.Items.AddRange(new string[] { "Off", "On" });
                            evtNameA1.Enabled = true;
                            evtNameA1.SelectedIndex = asc.Param1;
                            break;
                        // priority
                        case 0x0F:
                            groupBoxA.Text = commandText;
                            labelEvtA3.Text = "Priority";
                            evtNumA3.Maximum = 3; evtNumA3.Enabled = true;
                            evtNumA3.Value = asc.Param2;
                            break;
                        // Memory
                        case 0xB6:
                            groupBoxA.Text = commandText;
                            labelEvtA3.Text = "Memory";
                            labelEvtA4.Text = "Shift";
                            evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                            evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                            evtNumA3.Enabled = true;
                            evtNumA4.Maximum = 256; evtNumA4.Minimum = 1; evtNumA4.Enabled = true;
                            evtNumA3.Value = (asc.Param2 * 2) + 0x7000;
                            evtNumA4.Value = (asc.Param3 ^ 0xFF) + 1;
                            break;
                        // Memory $700C
                        case 0xB0:
                        case 0xB1:
                        case 0xB2:
                            groupBoxA.Text = commandText;
                            labelEvtA3.Text = "Value";
                            evtNumA3.Maximum = 65535; evtNumA3.Enabled = true;
                            evtNumA3.Value = Bits.GetShort(asc.CommandData, 2);
                            break;
                        case 0xB3:
                        case 0xB4:
                        case 0xB5:
                            groupBoxA.Text = commandText;
                            labelEvtA3.Text = "Memory";
                            evtNumA3.Hexadecimal = true; evtNumA3.Increment = 2;
                            evtNumA3.Maximum = 0x71FE; evtNumA3.Minimum = 0x7000;
                            evtNumA3.Enabled = true;
                            evtNumA3.Value = (asc.Param2 * 2) + 0x7000;
                            break;
                        case 0x9E:
                            groupBoxA.Text = commandText;
                            labelEvtA1.Text = "Sound";
                            evtNameA1.Items.AddRange(Lists.SoundNames); evtNameA1.Enabled = true;
                            evtNameA1.SelectedIndex = asc.Param2;
                            break;
                    }
                    break;
            }
            OrganizeControls();
            panelCommands.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssembleAction()
        {
            switch (asc.Opcode)
            {
                // Properties
                case 0x0A:
                case 0x0B:
                case 0x0C:
                case 0x15:
                    for (int i = 0; i < 8; i++)
                        Bits.SetBit(asc.CommandData, 1, i, evtEffects.GetItemChecked(i));
                    break;
                case 0x13:
                    asc.Param1 = (byte)evtNumA3.Value;
                    break;
                case 0x3D:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Palette
                case 0x0D:
                case 0x0E:
                    asc.Param1 &= 0xF0;
                    asc.Param1 |= (byte)evtNumA3.Value;
                    break;
                // Sprite Sequence
                case 0x08:
                    asc.Param1 = (byte)evtNumA3.Value;
                    asc.Param2 = (byte)evtNumA4.Value;
                    Bits.SetBit(asc.CommandData, 1, 3, evtEffects.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 1, 4, evtEffects.GetItemChecked(1));
                    Bits.SetBit(asc.CommandData, 1, 6, evtEffects.GetItemChecked(2));
                    Bits.SetBit(asc.CommandData, 2, 7, evtEffects.GetItemChecked(3));
                    break;
                case 0x10:
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(asc.CommandData, 1, 6, evtEffects.GetItemChecked(0));
                    Bits.SetBit(asc.CommandData, 1, 7, evtEffects.GetItemChecked(1));
                    break;
                case 0x3A:         // If distance between object A and...
                case 0x3B:         // If distance (Z==) between object A and...
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;          // object A
                    asc.Param2 = (byte)evtNumA2.Value;    // object B
                    asc.Param3 = (byte)evtNumA3.Value;
                    Bits.SetShort(asc.CommandData, 4, (ushort)evtNumC1.Value);
                    break;
                case 0x3E:         // Create NPC packet @ obj coords...
                    asc.Param2 = (byte)evtNameA1.SelectedIndex;
                    asc.Param1 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetShort(asc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0x3F:         // Create NPC packet...
                    asc.Param1 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0xD0:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Sprite Animation

                // Shift isometric units
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x5A:
                case 0x5B:
                    asc.Param1 = (byte)evtNumA3.Value;
                    break;
                case 0x7E:
                case 0x7F:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Shift 1px units
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x66:
                case 0x67:
                case 0x68:
                case 0x6A:
                case 0x6B:
                    asc.Param1 = (byte)evtNumA3.Value;
                    break;
                // Face direction
                case 0x7B:
                    asc.Param1 = (byte)evtNumA3.Value;
                    break;
                // Shift to coords
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x83:
                case 0x84:
                    if (asc.Opcode != 0x80 && asc.Opcode != 0x82)
                    {
                        asc.Param1 = (byte)((sbyte)evtNumA3.Value);
                        asc.Param2 = (byte)((sbyte)evtNumA4.Value);
                    }
                    else
                    {
                        asc.Param1 = (byte)evtNumA3.Value;
                        asc.Param2 = (byte)evtNumA4.Value;
                    }
                    break;
                case 0x87:
                case 0x95:
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x90:
                case 0x91:
                    asc.Param3 = (byte)evtNumA1.Value;
                    if (asc.Opcode != 0x90)
                    {
                        asc.Param1 = (byte)((sbyte)evtNumA3.Value);
                        asc.Param2 = (byte)((sbyte)evtNumA4.Value);
                    }
                    else
                    {
                        asc.Param1 = (byte)evtNumA3.Value;
                        asc.Param2 = (byte)evtNumA4.Value;
                    }
                    break;
                case 0x92:
                case 0x93:
                case 0x94:
                    asc.Param3 = (byte)(evtNameA2.SelectedIndex << 5);
                    asc.Param3 &= 0xE0; asc.Param3 |= (byte)evtNumA2.Value;
                    if (asc.Opcode != 0x92)
                    {
                        asc.Param1 = (byte)((sbyte)evtNumA3.Value);
                        asc.Param2 = (byte)((sbyte)evtNumA4.Value);
                    }
                    else
                    {
                        asc.Param1 = (byte)evtNumA3.Value;
                        asc.Param2 = (byte)evtNumA4.Value;
                    }
                    break;
                case 0x88:
                case 0x89:
                case 0x98:
                case 0x99:
                    break;
                // Audio playback
                case 0x9C:
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x9D:
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;
                    asc.Param2 = (byte)evtNumA3.Value;
                    break;
                case 0x9E:
                    asc.Param1 = (byte)evtNumA3.Value;
                    asc.Param2 = (byte)evtNumA4.Value;
                    break;
                // Memory
                case 0xA0:
                case 0xA1:
                case 0xA2:
                    asc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xA0);
                    asc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    asc.Param1 &= 0xF8; asc.Param1 |= (byte)evtNumA4.Value;
                    break;
                case 0xA4:
                case 0xA5:
                case 0xA6:
                    asc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xA4);
                    asc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    asc.Param1 &= 0xF8; asc.Param1 |= (byte)evtNumA4.Value;
                    break;
                case 0xA8:
                case 0xA9:
                    asc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    asc.Param2 = (byte)evtNumA4.Value;
                    break;
                case 0xAA:
                case 0xAB:
                    asc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB0:
                case 0xB1:
                case 0xC2:
                    asc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumA4.Value);
                    break;
                case 0xB2:
                case 0xB3:
                case 0xD6:
                case 0xBB:
                    asc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    break;
                case 0xB5:
                    asc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB7:
                    asc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumA4.Value);
                    break;
                case 0xBC:
                case 0xBD:
                    asc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    asc.Param1 = (byte)((evtNumA4.Value - 0x7000) / 2);
                    break;
                case 0xD8:
                case 0xD9:
                case 0xDA:
                    asc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xD8);
                    asc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    asc.Param1 &= 0xF8; asc.Param1 |= (byte)evtNumA4.Value;
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0xDC:
                case 0xDD:
                case 0xDE:
                    asc.Opcode = (byte)(((((byte)(evtNumA3.Value - 0x7040) << 3) & 0x0F00) >> 8) + 0xDC);
                    asc.Param1 = (byte)(((byte)(evtNumA3.Value - 0x7040) << 3) & 0xF8);
                    asc.Param1 &= 0xF8; asc.Param1 |= (byte)evtNumA4.Value;
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumC1.Value);
                    break;
                case 0xE0:
                case 0xE1:
                    asc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    asc.Param2 = (byte)evtNumA4.Value;
                    Bits.SetShort(asc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0xE4:
                case 0xE5:
                    asc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    Bits.SetShort(asc.CommandData, 2, (ushort)evtNumA4.Value);
                    Bits.SetShort(asc.CommandData, 4, (ushort)evtNumC1.Value);
                    break;
                case 0xE8:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xE9:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetShort(asc.CommandData, 3, (ushort)evtNumA4.Value);
                    break;
                // Memory $700C
                case 0xAC:
                case 0xAD:
                case 0xB6:
                case 0xC0:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xB4:
                    asc.Param1 = (byte)(evtNumA3.Value - 0x70A0);
                    break;
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xC1:
                    asc.Param1 = (byte)((evtNumA3.Value - 0x7000) / 2);
                    break;
                case 0xC4:
                case 0xC5:
                case 0xC6:
                    asc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(asc.CommandData, 1, 6, evtNameA2.SelectedIndex == 1);
                    break;
                case 0xDB:
                case 0xDF:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xE2:
                case 0xE3:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    Bits.SetShort(asc.CommandData, 3, (ushort)evtNumA4.Value);
                    break;
                case 0xE6:
                case 0xE7:
                    for (int i = 0; i < 16; i++)
                        Bits.SetBit(asc.CommandData, 1, i, evtEffects.GetItemChecked(i));
                    Bits.SetShort(asc.CommandData, 3, (ushort)evtNumC1.Value);
                    break;
                case 0xEA:
                case 0xEB:
                case 0xEC:
                case 0xED:
                case 0xEE:
                case 0xEF:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                // Jump to
                case 0xD2:
                case 0xD3:
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA3.Value);
                    break;
                case 0xD4:
                    asc.Param1 = (byte)evtNumA3.Value;
                    break;
                // Object memory
                case 0xF2:         // Set obj presence...  
                case 0xF3:         // Set obj engage type...
                case 0xF8:         // If object in level ..., presence =...
                    Bits.SetShort(asc.CommandData, 1, (ushort)evtNumA1.Value);
                    asc.Param2 &= 1; asc.Param2 |= (byte)(evtNameA2.SelectedIndex << 1);
                    Bits.SetBit(asc.CommandData, 2, 7, evtEffects.GetItemChecked(0));    // set bit 7 if true
                    if (asc.Opcode == 0xF8)
                        Bits.SetShort(asc.CommandData, 3, (ushort)evtNumC1.Value);
                    /* 
                     * TODO
                     * synchronize evtNameA with evtNumA
                     */
                    break;
                // Pause script
                case 0xF0:
                    asc.Param1 = (byte)(evtNumA3.Value - 1);
                    break;
                case 0xF1:
                    Bits.SetShort(asc.CommandData, 1, (ushort)(evtNumA3.Value - 1));
                    break;
                case 0xFD:
                    switch (asc.Param1)
                    {
                        case 0x00:
                        case 0x01:
                            asc.Param1 = (byte)evtNameA1.SelectedIndex;
                            break;
                        case 0x0F:
                            asc.Param2 = (byte)evtNumA3.Value;
                            break;
                        // Memory
                        case 0xB6:
                            asc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                            asc.Param3 = (byte)((byte)(evtNumA4.Value - 1) ^ 0xFF);
                            break;
                        // Memory $700C
                        case 0xB0:
                        case 0xB1:
                        case 0xB2:
                            Bits.SetShort(asc.CommandData, 2, (ushort)evtNumA3.Value);
                            break;
                        case 0xB3:
                        case 0xB4:
                        case 0xB5:
                            asc.Param2 = (byte)((evtNumA3.Value - 0x7000) / 2);
                            break;
                        case 0x9E:
                            asc.Param2 = (byte)evtNameA1.SelectedIndex;
                            break;
                    }
                    break;
            }
        }
        //
        private void ResetControls()
        {
            this.Updating = true;
            evtNameA1.DropDownWidth = evtNameA1.Width; evtNameA1.Items.Clear(); evtNameA1.ResetText(); evtNameA1.Enabled = false;
            evtNameA1.DrawMode = DrawMode.Normal; evtNameA1.ItemHeight = 13; evtNameA1.BackColor = SystemColors.Window;
            evtNameA2.DropDownWidth = evtNameA2.Width; evtNameA2.Items.Clear(); evtNameA2.ResetText(); evtNameA2.Enabled = false;
            evtNameA2.DrawMode = DrawMode.Normal; evtNameA2.ItemHeight = 13; evtNameA2.BackColor = SystemColors.Window;
            evtNumA1.Maximum = 255; evtNumA1.Hexadecimal = false; evtNumA1.Value = 0; evtNumA1.Enabled = false;
            evtNumA2.Maximum = 255; evtNumA2.Hexadecimal = false; evtNumA2.Value = 0; evtNumA2.Enabled = false;
            evtNumA3.Maximum = 255; evtNumA3.Hexadecimal = false; evtNumA3.Minimum = 0; evtNumA3.Increment = 1; evtNumA3.Value = 0; evtNumA3.Enabled = false;
            evtNumA4.Maximum = 255; evtNumA4.Hexadecimal = false; evtNumA4.Minimum = 0; evtNumA4.Increment = 1; evtNumA4.Value = 0; evtNumA4.Enabled = false;
            evtNumC1.Maximum = 255; evtNumC1.Hexadecimal = false; evtNumC1.Value = 0; evtNumC1.Enabled = false;
            evtNumC2.Maximum = 255; evtNumC2.Value = 0; evtNumC2.Enabled = false;
            evtEffects.Height = 68; evtEffects.ColumnWidth = 132; evtEffects.Items.Clear(); evtEffects.Enabled = false;
            groupBoxA.Text = "";
            groupBoxB.Text = "";
            groupBoxC.Text = "";
            labelEvtA1.Text = "";
            labelEvtA2.Text = "";
            labelEvtA3.Text = "";
            labelEvtA4.Text = "";
            labelEvtC1.Text = "";
            labelEvtC2.Text = "";
            this.Updating = false;
        }
        private void OrganizeControls()
        {
            groupBoxA.Visible =
            groupBoxA.Text != "" ||
            labelEvtA1.Text != "" ||
            labelEvtA2.Text != "" ||
            labelEvtA3.Text != "" ||
            labelEvtA4.Text != "";
            groupBoxB.Visible =
                groupBoxB.Text != "" ||
                evtEffects.Items.Count > 0;
            groupBoxC.Visible =
                groupBoxC.Text != "" ||
                labelEvtC1.Text != "" ||
                labelEvtC2.Text != "";
            panelEvtA1.Visible = evtNumA1.Enabled || evtNameA1.Enabled;
            panelEvtA2.Visible = evtNumA2.Enabled || evtNameA2.Enabled;
            panelEvtA3_4.Visible = evtNumA3.Enabled || evtNumA4.Enabled;
            if (evtEffects.Items.Count < 4)
                evtEffects.Height = evtEffects.Items.Count * 16 + 4;
            else
                evtEffects.Height = 68;
            labelEvtA1.Visible = labelEvtA1.Text != "";
            labelEvtA2.Visible = labelEvtA2.Text != "";
            labelEvtA3.Visible = labelEvtA3.Text != "";
            labelEvtA4.Visible = labelEvtA4.Text != "";
            evtNumA1.Visible = evtNumA1.Enabled;
            evtNumA2.Visible = evtNumA2.Enabled;
            evtNumA3.Visible = evtNumA3.Enabled;
            evtNumA4.Visible = evtNumA4.Enabled;
            evtNameA1.Visible = evtNameA1.Enabled;
            evtNameA2.Visible = evtNameA2.Enabled;
            evtNumC2.Visible = evtNumC2.Enabled;
            // organize
            groupBoxA.BringToFront();
            groupBoxB.BringToFront();
            groupBoxC.BringToFront();
            panel1.BringToFront();
            labelEvtA1.BringToFront();
            evtNameA1.BringToFront();
            evtNumA1.BringToFront();
            labelEvtA2.BringToFront();
            evtNameA2.BringToFront();
            evtNumA2.BringToFront();
            labelEvtA3.BringToFront();
            evtNumA3.BringToFront();
            labelEvtA4.BringToFront();
            evtNumA4.BringToFront();
            labelEvtC1.BringToFront();
            evtNumC1.BringToFront();
            labelEvtC2.BringToFront();
            evtNumC2.BringToFront();
            //
            if (evtNameA1.DrawMode == DrawMode.OwnerDrawFixed)
            {
                evtNameA1.BackColor = SystemColors.ControlDarkDark;
                evtNameA1.ItemHeight = 15;
            }
            if (evtNameA2.DrawMode == DrawMode.OwnerDrawFixed)
            {
                evtNameA2.BackColor = SystemColors.ControlDarkDark;
                evtNameA2.ItemHeight = 15;
            }
        }
    }
}
