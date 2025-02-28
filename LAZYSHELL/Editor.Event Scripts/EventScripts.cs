using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Undo;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL
{
    public partial class EventScripts : NewForm
    {
        #region Variables
        // main
        private Settings settings = Settings.Default;
        //
        private EventScript[] eventScripts { get { return Model.EventScripts; } set { Model.EventScripts = value; } }
        public EventScript[] EVENTScripts { get { return eventScripts; } set { eventScripts = value; } }
        private ActionScript[] actionScripts { get { return Model.ActionScripts; } set { Model.ActionScripts = value; } }
        public ActionScript[] ActionScripts { get { return actionScripts; } set { actionScripts = value; } }
        private ActionScript actionScript { get { return actionScripts[index]; } set { actionScripts[index] = value; } }
        private EventScript eventScript { get { return eventScripts[index]; } set { eventScripts[index] = value; } }
        private byte[] scriptData
        {
            get
            {
                if (!isActionScript)
                    return eventScript.Script;
                else
                    return actionScript.Script;
            }
        }
        //
        private TreeViewWrapper treeViewWrapper;
        public TreeViewWrapper TreeViewWrapper { get { return treeViewWrapper; } }
        private bool isActionScript = false;
        private bool isActionSelected = false;
        private int index { get { return (int)eventNum.Value; } set { eventNum.Value = value; } }

      //  private int buffer;

        private int type { get { return eventName.SelectedIndex; } set { eventName.SelectedIndex = value; } }
        private int scriptLength
        {
            get
            {
                if (!isActionScript)
                    return eventScript.Length;
                else
                    return actionScript.Length;
            }
        }
        private int currentScript = 0;
        private CommandStack commandStack = new CommandStack();
        private Stack<Navigate> navigateBackward = new Stack<Navigate>();
        private Stack<Navigate> navigateForward = new Stack<Navigate>();
        private Navigate lastNavigate;
        private bool disableNavigate;
        // private accessors
        private string commandText
        {
            get
            {
                int[] tree = categoryCommand;
                if (tree != null)
                    return asc == null ?
                        Lists.EventNames(tree[0])[tree[1]] :
                        Lists.ActionNames(tree[0])[tree[1]];
                else
                    return "INVALID";
            }
        }
        private int[] categoryCommand
        {
            get
            {
                int opcode;
                int param1;
                int[][] listBoxOpcodes;
                int[][] listBoxFDOpcodes;
                if (asc == null)
                {
                    listBoxOpcodes = Lists.EventOpcodes;
                    listBoxFDOpcodes = Lists.EventParams;
                    opcode = esc.Opcode;
                    param1 = esc.Param1;
                    if (opcode <= 0x2F) opcode = 0;
                }
                else
                {
                    listBoxOpcodes = Lists.ActionOpcodes;
                    listBoxFDOpcodes = Lists.ActionParams;
                    opcode = asc.Opcode;
                    param1 = asc.Param1;
                }
                if (opcode >= 0xA0 && opcode <= 0xA2) opcode = 0xA0;
                if (opcode >= 0xA4 && opcode <= 0xA6) opcode = 0xA4;
                if (opcode >= 0xD8 && opcode <= 0xDA) opcode = 0xD8;
                if (opcode >= 0xDC && opcode <= 0xDE) opcode = 0xDC;
                if (opcode != 0xFD)
                    for (int a = 0; a < listBoxOpcodes.Length; a++)
                        for (int b = 0; b < listBoxOpcodes[a].Length; b++)
                            if (opcode == listBoxOpcodes[a][b])
                                return new int[] { a, b };
                if (opcode == 0xFD)
                    for (int a = 0; a < listBoxFDOpcodes.Length; a++)
                        for (int b = 0; b < listBoxFDOpcodes[a].Length; b++)
                            if (param1 == listBoxFDOpcodes[a][b])
                                return new int[] { a, b };
                return null;
            }
        }
        // reference variables
        private EventCommand esc;
        private ActionCommand asc;
        private TreeNode modifiedNode;
        // externally accessed controls
        public ToolStripNumericUpDown EventNum { get { return eventNum; } set { eventNum = value; } }
        public System.Windows.Forms.ToolStripComboBox EventName { get { return eventName; } set { eventName = value; } }
        // pointer recalibration
        private bool apply; public bool Apply { get { return apply; } set { apply = value; } }
        private int delta; public int Delta { get { return delta; } set { delta = value; } }
        // other
        private Previewer previewer;
        private Search searchWindow;
        private EditLabel labelWindow;
        private class Navigate
        {
            public int Index;
            public int Type;
            public Navigate(int index, int type)
            {
                this.Index = index;
                this.Type = type;
            }
        }
        #endregion
        #region Functions
        // Constructor
        public EventScripts()
        {
            this.Modified = true;
            InitializeComponent();
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip4, Keys.F2, baseConvertor);
            InitializeEditor();
            searchWindow = new Search(eventNum, searchBox, searchLabels, Lists.EventLabels);
            labelWindow = new EditLabel(eventLabel, eventNum, "Event Scripts", true);
            //this.eventName.Items.AddRange(Lists.Numerize(Lists.EventLabels));
            new ToolTipLabel(this, baseConvertor, helpTips);
            this.History = new History(this, null, eventNum);
            disableNavigate = true;
            if (settings.RememberLastIndex)
            {
                int lastEventScript = settings.LastEventScript;
                type = Math.Max(0, settings.LastEventScriptCat);
                index = Math.Min((int)eventNum.Maximum, lastEventScript);
            }
            else
                type = 0;
            disableNavigate = false;
            lastNavigate = new Navigate(index, type);
            this.Modified = false;
        }
        private void InitializeEditor()
        {
            for (int i = 0; i < Lists.EventLabels.Length; i++)
            {
                if (Lists.EventLabels[i] != null)
                    continue;
                if (Lists.EventLabels[i] != "")
                    continue;
                Lists.EventLabels[i] = "EVENT #" + i;
            }
            for (int i = 0; i < Lists.ActionLabels.Length; i++)
            {
                if (Lists.ActionLabels[i] != null)
                    continue;
                if (Lists.ActionLabels[i] != "")
                    continue;
                Lists.ActionLabels[i] = "ACTION #" + i;
            }
            eventLabel.Text = Lists.EventLabels[(int)this.eventNum.Value];
            treeViewWrapper = new TreeViewWrapper(this.commandTree);
            treeViewWrapper.ChangeScript(eventScripts[(int)this.eventNum.Value]);
            this.autoPointerUpdate.Checked = autoPointerUpdate.Checked;
            UpdateEventScriptsFreeSpace();
        }
        private void RefreshEditor()
        {
            bool modified = this.Modified;
            if (isActionScript)
            {
                foreach (ActionCommand aq in actionScripts[currentScript].Commands)
                    aq.Modified = false;
            }
            else
            {
                foreach (EventCommand es in eventScripts[currentScript].Commands)
                {
                    es.Modified = false;
                    if (es.Queue == null) continue;
                    foreach (ActionCommand aq in es.Queue.Commands)
                        aq.Modified = false;
                }
            }
            // Update Event Script Offsets
            currentScript = (int)this.eventNum.Value;
            ResetLists();
            commandTree.BeginUpdate();
            if (isActionScript)
            {
                UpdateActionOffsets();
                treeViewWrapper.ChangeScript(actionScripts[(int)this.eventNum.Value]);
            }
            else
            {
                UpdateScriptOffsets();
                treeViewWrapper.ChangeScript(eventScripts[(int)this.eventNum.Value]);
            }
            commandTree.EndUpdate();
            UpdateCommandData();
            if (!isActionScript)
            {
                if (Lists.EventLabels[currentScript] == "")
                    eventLabel.Text = "EVENT #" + currentScript;
                else eventLabel.Text = Lists.EventLabels[currentScript];
            }
            else
            {
                if (Lists.ActionLabels[currentScript] == "")
                    eventLabel.Text = "ACTION #" + currentScript;
                else eventLabel.Text = Lists.ActionLabels[currentScript];
            }

            if (isActionScript)
                return;
            switch (currentScript)
            {
                case 0x1D6:
                case 0x72D:
                case 0x72F:
                case 0xD01:
                case 0xE91:
                    commandTree.Enabled = false;
                    categories_es.Enabled = false;
                    categories_aq.Enabled = false;
                    commands.Enabled = false;
                    MessageBox.Show(
                        "Editing of script #" + currentScript.ToString() + " is not allowed due to parsing issues.",
                        "LAZYSHELL++",
                        MessageBoxButtons.OK);
                    break;
                default:
                    commandTree.Enabled = true;
                    categories_es.Enabled = true;
                    categories_aq.Enabled = true;
                    commands.Enabled = true;
                    break;
            }

            this.Modified = modified;
        }
        // GUI settings
        private string[] DialogueNames()
        {
            string[] tables = Model.DTEStr(true);
            string[] names = new string[Model.Dialogues.Length];
            for (int i = 0; i < Model.Dialogues.Length; i++)
                names[i] = Model.Dialogues[i].GetStub(true, tables);
            return names;
        }
        // Editing
        private void InsertEventCommand()
        {
            esc = esc.Copy();
            ControlAssembleEvent();
            treeViewWrapper.InsertNode(esc.Copy());
        }
        private void InsertActionCommand()
        {
            asc = asc.Copy();
            ControlAssembleAction();
            treeViewWrapper.InsertNode(asc.Copy());
        }
        private void PushCommand(ScriptBuffer buffer)
        {
            if (!isActionScript)
                commandStack.Push(new CommandEdit(eventScripts, index, buffer, this));
            else
                commandStack.Push(new CommandEdit(actionScripts, index, buffer, this));
        }
        // Update offsets
        public void UpdateScriptOffsets()
        {
            UpdateScriptOffsets(treeViewWrapper.Script.Index);
        }
        public void UpdateScriptOffsets(int index)
        {
            int end, start;
            //
            if (index >= 0 && index <= 1535)
            {
                start = 0; end = 1535; // Bank 1E
            }
            else if (index >= 1536 && index <= 3071)
            {
                start = 1536; end = 3071; // Bank 1F
            }
            else if (index >= 3072 && index <= 4095)
            {
                start = 3072; end = 4095; // Bank 20
            }
            else
                throw new Exception("Invalid event num");
            //
            int conditionOffset = 0;
            if (index < end)
                conditionOffset = eventScripts[index + 1].BaseOffset;
            else
                conditionOffset = eventScripts[index].BaseOffset + eventScripts[index].Length;
            // set the conditionOffset based on the earliest command whose offset was changed in the current script
            foreach (EventCommand esc in eventScripts[index].Commands)
            {
                if (esc.Offset != esc.OriginalOffset)
                {
                    conditionOffset = esc.Offset;
                    break;
                }
            }
            foreach (EventScript script in eventScripts)
            {
                if (script.Index > end)
                    break;
                if (script.Index >= start && script.Index != index)
                    script.UpdateAllOffsets(treeViewWrapper.ScriptDelta, conditionOffset);
            }
            treeViewWrapper.ScriptDelta = 0;
        }
        public void UpdateActionOffsets()
        {
            UpdateActionOffsets(treeViewWrapper.Action.Index);
        }
        public void UpdateActionOffsets(int index)
        {
            int end, start;
            int conditionOffset = 0;
            //
            if (index >= 0 && index <= 1023)
            {
                start = 0; end = 1023; // Bank 1E
            }
            else
                throw new Exception("Invalid action num");
            //
            if (index < end)
                conditionOffset = actionScripts[index + 1].BaseOffset;
            else
                conditionOffset = actionScripts[index].BaseOffset + actionScripts[index].Length; // Dont need to update anything after this event if its the last one                    
            // set the conditionOffset based on the earliest command whose offset was changed in the current script
            foreach (ActionCommand asc in actionScripts[index].Commands)
            {
                if (asc.Offset != asc.OriginalOffset)
                {
                    conditionOffset = asc.Offset;
                    break;
                }
            }
            foreach (ActionScript script in actionScripts)
            {
                if (script.Index > end)
                    break;
                if (script.Index >= start && script.Index != index)
                    script.UpdateOffsets(treeViewWrapper.ScriptDelta, conditionOffset);
            }
            treeViewWrapper.ScriptDelta = 0;
        }
        // Update controls
        private void UpdateActionScriptsFreeSpace()
        {
            int left = CalculateActionScriptsLength();
            this.labelBytesLeft.Text = " " + left.ToString() + " bytes left ";
            this.labelBytesLeft.BackColor = left < 0 ? Color.Red : SystemColors.Control;
        }
        private int CalculateActionScriptsLength()
        {
            int totalSize = 0xC000 - 0x800;
            int length = 0;
            for (int i = 0; i < actionScripts.Length; i++)
                length += actionScripts[i].Script.Length;
            return totalSize - length - 1;
        }
        private void UpdateEventScriptsFreeSpace()
        {
            int left = CalculateEventScriptsLength();
            this.labelBytesLeft.Text = " " + left.ToString() + " bytes left ";
            this.labelBytesLeft.BackColor = left < 0 ? Color.Red : SystemColors.Control;
        }
        private int CalculateEventScriptsLength()
        {
            int totalSize;
            int length = 0;
            int min;
            int max;
            if (currentScript < 0x600)
            {
                totalSize = 0x10000 - 0xC00;
                min = 0; max = 0x600;
            }
            else if (currentScript < 0xC00)
            {
                totalSize = 0x10000 - 0xC00;
                min = 0x600; max = 0xC00;
            }
            else
            {
                totalSize = 0xE000 - 0x800;
                min = 0xC00; max = 0x1000;
            }
            for (int i = min; i < max; i++)
                length += eventScripts[i].Script.Length;
            return totalSize - length - 1;
        }
        private void ResetLists()
        {
            panelCommands.SuspendDrawing();
            ResetControls();
            buttonInsertEvent.Enabled = false;
            buttonApplyEvent.Enabled = false;
            if (type == 1)   // Action Scripts
            {
                eventNum.Maximum = 1023;
                categories_aq.BringToFront();
                categories_aq.SelectedIndex = 0;
                categories_aq_SelectedIndexChanged(null, null);
                isActionScript = true;
                treeViewWrapper.ActionScript = true;
            }
            else    // Event Scripts
            {
                eventNum.Maximum = 4095;
                categories_es.BringToFront();
                categories_es.SelectedIndex = 0;
                categories_es_SelectedIndexChanged(null, null);
                isActionScript = false;
                treeViewWrapper.ActionScript = false;
            }
            panelCommands.ResumeDrawing();
        }
        private void UpdateCommandData()
        {
            this.eventHexText.Text = BitConverter.ToString(treeViewWrapper.CurrentNodeData);
            if (!isActionScript)
            {
                eventScripts[currentScript].Assemble();
                UpdateEventScriptsFreeSpace();
            }
            else
            {
                actionScripts[currentScript].Assemble();
                UpdateActionScriptsFreeSpace();
            }
        }
        // Saving
        public void Assemble()
        {
            if (!isActionScript)
                UpdateScriptOffsets();
            else
                UpdateActionOffsets();
            // Save current script first
            settings.Save();
            if (CalculateEventScriptsLength() >= 0)
                AssembleAllEventScripts();
            else
                MessageBox.Show("There is not enough available space to save the event scripts to.\n\nThe event scripts were not saved.", "LAZYSHELL++");
            if (CalculateActionScriptsLength() >= 0)
                AssembleAllActionScripts();
            else
                MessageBox.Show("There is not enough available space to save the action scripts to.\n\nThe action scripts were not saved.", "LAZYSHELL++");
            if (!isActionScript)
                Model.HexEditor.SetOffset(eventScript.BaseOffset);
            else
                Model.HexEditor.SetOffset(actionScript.BaseOffset);
            Model.HexEditor.Compare();
            this.Modified = false;
        }
        public void AssembleAllEventScripts()
        {
            foreach (EventScript es in eventScripts)
                es.Assemble();
            int i = 0;
            int pointer = 0;
            int bank = 0x1E0000;
            ushort offset = 0xC00;
            for (; i < 1536; i++, pointer += 2)
            {
                Bits.SetShort(Model.ROM, bank + pointer, offset);
                Bits.SetBytes(Model.ROM, bank + offset, eventScripts[i].Script);
                offset += (ushort)eventScripts[i].Script.Length;
            }
            for (int a = offset; a < 0x10000; a++) Model.ROM[bank + a] = 0xFF;
            pointer = 0;
            bank = 0x1F0000;
            offset = 0xC00;
            for (; i < 3072; i++, pointer += 2)
            {
                Bits.SetShort(Model.ROM, bank + pointer, offset);
                Bits.SetBytes(Model.ROM, bank + offset, eventScripts[i].Script);
                offset += (ushort)eventScripts[i].Script.Length;
            }
            for (int a = offset; a < 0x10000; a++) Model.ROM[bank + a] = 0xFF;
            pointer = 0;
            bank = 0x200000;
            offset = 0x800;
            for (; i < 4096; i++, pointer += 2)
            {
                Bits.SetShort(Model.ROM, bank + pointer, offset);
                Bits.SetBytes(Model.ROM, bank + offset, eventScripts[i].Script);
                offset += (ushort)eventScripts[i].Script.Length;
            }
            for (int a = offset; a < 0xE000; a++) Model.ROM[bank + a] = 0xFF;
        }
        public void AssembleAllActionScripts()
        {
            foreach (ActionScript ac in actionScripts)
                ac.Assemble();
            int i = 0;
            int pointer = 0;
            int bank = 0x210000;
            ushort offset = 0x800;
            for (; i < actionScripts.Length; i++, pointer += 2)
            {
                Bits.SetShort(Model.ROM, bank + pointer, offset);
                Bits.SetBytes(Model.ROM, bank + offset, actionScripts[i].Script);
                offset += (ushort)actionScripts[i].Script.Length;
            }
        }
        // Other
        private void PreviewEventOrAction()
        {
            if (previewer == null || !previewer.Visible)
                previewer = new Previewer(this.currentScript, this.type == 0 ? EType.EventScript : EType.ActionScript);
            else
                previewer.Reload(this.currentScript, this.type == 0 ? EType.EventScript : EType.ActionScript);
            
            if (previewer.IsDisposed) return;
            previewer.Show();
            previewer.BringToFront();
        }
        private void SaveEventNotes()
        {
            try
            {
                //this.EventScriptNotes.SaveFile(notes.GetPath() + "main-scripts-event.rtf");
            }
            catch
            {
                MessageBox.Show("ERROR saving main-scripts-event.rtf, please report this if it persists");
            }
        }
        #endregion
        #region Event Handlers
        private void eventPointer_UpdatePointer(object sender, EventArgs e)
        {
            //
            State.Instance.AutoPointerUpdate = autoPointerUpdate.Checked;
            //
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), 2);
                PushCommand(buffer);
                treeViewWrapper.RefreshScript();
            //
        }
        private void eventNum_ValueChanged(object sender, EventArgs e)
        {
            if (this.Refreshing)
                return;
            RefreshEditor();
            //
            if (!disableNavigate && lastNavigate != null)
            {
                navigateBackward.Push(new Navigate(lastNavigate.Index, lastNavigate.Type));
                navigateBck.Enabled = true;
            }
            if (!disableNavigate)
                lastNavigate = new Navigate(index, type);
            settings.LastEventScript = index;
            settings.LastEventScriptCat = type;
        }
        private void eventName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Refreshing)
                return;
            this.Refreshing = true;
            eventNum.Value = currentScript = 0;
            if (!isActionScript)
                UpdateScriptOffsets();
            else
                UpdateActionOffsets();
            ResetLists();
            if (isActionScript)
                treeViewWrapper.ChangeScript(actionScripts[(int)this.eventNum.Value]);
            else
                treeViewWrapper.ChangeScript(eventScripts[(int)this.eventNum.Value]);
            UpdateCommandData();
            if (!isActionScript)
            {
                if (Lists.EventLabels[currentScript] == "")
                    eventLabel.Text = "EVENT #" + currentScript;
                else eventLabel.Text = Lists.EventLabels[currentScript];

                labelWindow.SetElement("Event Scripts");
            }
            else
            {
                if (Lists.ActionLabels[(int)this.eventNum.Value] == "")
                    eventLabel.Text = "ACTION #" + this.eventNum.Value.ToString();
                else eventLabel.Text = Lists.ActionLabels[(int)this.eventNum.Value];

                labelWindow.SetElement("Action Scripts");
            }
            this.Refreshing = false;
            //
            if (!disableNavigate && lastNavigate != null)
            {
                navigateBackward.Push(new Navigate(lastNavigate.Index, lastNavigate.Type));
                navigateBck.Enabled = true;
            }
            if (!disableNavigate)
                lastNavigate = new Navigate(index, type);
            settings.LastEventScript = index;
            settings.LastEventScriptCat = type;
        }
        private void gotoAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;
            gotoAddrButton.PerformClick();
        }
        private void gotoAddrButton_Click(object sender, EventArgs e)
        {
            int input = 0;
            try
            {
                input = Convert.ToInt32(gotoAddress.Text, 16);
            }
            catch
            {
                MessageBox.Show("Invalid input: address must be in hexadecimal format.");
                return;
            }
            if (isActionScript)
            {
                if (input < 0x210000)
                {
                    MessageBox.Show("Action script offset too low. Must be between $210000 and $21BFFF.");
                    return;
                }
                if (input >= 0x21C000)
                {
                    MessageBox.Show("Action script offset too high. Must be between $210000 and $21BFFF.");
                    return;
                }
                foreach (ActionScript script in actionScripts)
                {
                    foreach (ActionCommand action in script.Commands)
                    {
                        if (action.Offset + action.CommandData.Length > input || action.Offset >= input)
                        {
                            index = script.Index;
                            treeViewWrapper.SelectNode(action);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (input < 0x1E0000)
                {
                    MessageBox.Show("Event script offset too low. Must be between $1E0000 and $20FFFF.");
                    return;
                }
                if (input >= 0x210000)
                {
                    MessageBox.Show("Event script offset too high. Must be between $1E0000 and $20FFFF.");
                    return;
                }
                foreach (EventScript script in eventScripts)
                {
                    foreach (EventCommand command in script.Commands)
                    {
                        if (command.Queue != null)
                        {
                            foreach (ActionCommand action in command.Queue.Commands)
                            {
                                if (action.Offset + action.CommandData.Length > input || action.Offset >= input)
                                {
                                    index = script.Index;
                                    treeViewWrapper.SelectNode(action);
                                    return;
                                }
                            }
                        }
                        if (command.Offset + command.Length > input || command.Offset >= input)
                        {
                            index = script.Index;
                            treeViewWrapper.SelectNode(command);
                            return;
                        }
                    }
                }
            }
        }
        private void EventScripts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified)
                goto Close;
            DialogResult result;
            result = MessageBox.Show("Event Scripts have not been saved.\n\nWould you like to save changes?", "LAZYSHELL++", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.EventScripts = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            settings.Save();
            searchWindow.Close();
            if (previewer != null)
                previewer.Close();
            if (labelWindow.Visible)
                labelWindow.Close();
        }
        private void navigateBck_Click(object sender, EventArgs e)
        {
            if (navigateBackward.Count < 1)
                return;
            navigateForward.Push(new Navigate(index, type));
            //
            disableNavigate = true;
            type = navigateBackward.Peek().Type;
            index = navigateBackward.Peek().Index;
            disableNavigate = false;
            //
            RefreshEditor();
            lastNavigate = new Navigate(index, type);
            navigateBackward.Pop();
            navigateBck.Enabled = navigateBackward.Count > 0;
            navigateFwd.Enabled = true;
        }
        private void navigateFwd_Click(object sender, EventArgs e)
        {
            if (navigateForward.Count < 1)
                return;
            navigateBackward.Push(new Navigate(index, type));
            //
            disableNavigate = true;
            type = navigateForward.Peek().Type;
            index = navigateForward.Peek().Index;
            disableNavigate = false;
            //
            RefreshEditor();
            lastNavigate = new Navigate(index, type);
            navigateForward.Pop();
            navigateFwd.Enabled = navigateForward.Count > 0;
            navigateBck.Enabled = true;
        }

        // tree
        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateCommandData();
            if (this.Refreshing)
                return;
            if (!commandTree.Enabled)
                return;
            if (eventScript.Commands.Count == 0)
                return;
            //
            panelCommands.SuspendDrawing();
            // if selecting an action queue/script command
            if (commandTree.SelectedNode.Parent != null || isActionScript)
            {
                button1.Visible = false;
                isActionSelected = true;
                if (categories_aq.Parent.Controls.GetChildIndex(categories_aq) != 0)
                {
                    categories_aq.BringToFront();
                    commands.Items.Clear();
                    commands.Items.AddRange(Lists.ActionNames(categories_aq.SelectedIndex));
                    categories_aq.SelectedIndex = 0;
                    commands.SelectedIndex = 0;
                }
                if (asc == null && modifiedNode == null)    // if an event command is in the COMMAND PROPERTIES panel
                {
                    ResetControls();
                    buttonInsertEvent.Enabled = false;
                }
            }
            // if selecting an event script command
            else
            {
                EventCommand tempEsc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Index];
                button1.Checked = false;
                button1.Visible = tempEsc.Opcode <= 0x2F && tempEsc.Param1 < 0xF2;
                isActionSelected = false;
                if (categories_es.Parent.Controls.GetChildIndex(categories_es) != 0)
                {
                    categories_es.BringToFront();
                    commands.Items.Clear();
                    commands.Items.AddRange(Lists.EventNames(categories_es.SelectedIndex));
                    categories_es.SelectedIndex = 0;
                    commands.SelectedIndex = 0;
                }
                if (asc != null && modifiedNode == null)    // if an action queue command is in the COMMAND PROPERTIES panel
                {
                    ResetControls();
                    buttonInsertEvent.Enabled = false;
                }
            }
            treeViewWrapper.SelectedNode = commandTree.SelectedNode;
            //
            panelCommands.ResumeDrawing();
        }
        private void commandTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!commandTree.Enabled)
                return;
            // Edit Event/ActionQueue
            EvtScrEditCommand.PerformClick();
        }
        private void commandTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Do.AddHistory(this, index, e.Node, "NodeMouseClick");
            //
            commandTree.SelectedNode = e.Node;
            if (e.Button != MouseButtons.Right)
                return;
            goToToolStripMenuItem.Click -= goToDialogue_Click;
            goToToolStripMenuItem.Click -= goToEvent_Click;
            goToToolStripMenuItem.Click -= goToOffset_Click;
            goToToolStripMenuItem.Click -= goToAction_Click;
            goToToolStripMenuItem.Click -= addMemoryToNotesDatabase_Click;
            goToToolStripMenuItem.Click -= addMemoryToNotesDatabase_Click;
            if (commandTree.SelectedNode.Tag.GetType() == typeof(EventCommand))
            {
                EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
                if (temp.Opcode == 0x60 || temp.Opcode == 0x62)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Edit dialogue...";
                    goToToolStripMenuItem.Click += new EventHandler(goToDialogue_Click);
                }
                else if (temp.Opcode == 0x40 || temp.Opcode == 0x44 || temp.Opcode == 0x45 ||
                    temp.Opcode == 0xD0 || temp.Opcode == 0xD1 || (temp.Opcode == 0xFD && temp.Param1 == 0x46))
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Goto event...";
                    goToToolStripMenuItem.Click += new EventHandler(goToEvent_Click);
                }
                else if (temp.ReadPointer() != 0)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Goto offset...";
                    goToToolStripMenuItem.Click += new EventHandler(goToOffset_Click);
                }
                else if (temp.Opcode <= 0x2F && temp.Param1 >= 0xF2 && temp.Param1 <= 0xF5)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Edit action script...";
                    goToToolStripMenuItem.Click += new EventHandler(goToAction_Click);
                }
                // 0xa0 - 0xa6  // 0xd8 - 0xde
                else if (temp.Opcode == 0xA0 || temp.Opcode == 0xA1 || temp.Opcode == 0xA2 ||
                    temp.Opcode == 0xA4 || temp.Opcode == 0xA5 || temp.Opcode == 0xA6 ||
                    temp.Opcode == 0xD8 || temp.Opcode == 0xD9 || temp.Opcode == 0xDA ||
                    temp.Opcode == 0xDC || temp.Opcode == 0xDD || temp.Opcode == 0xDE)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Add to Project Database...";
                    goToToolStripMenuItem.Click += new EventHandler(addMemoryToNotesDatabase_Click);
                }
                else if (temp.Opcode == 0xFD)
                {
                    if (temp.Param1 == 0xD8 || temp.Param1 == 0xD9 || temp.Param1 == 0xDA ||
                        temp.Param1 == 0xDC || temp.Param1 == 0xDD || temp.Param1 == 0xDE)
                    {
                        e.Node.ContextMenuStrip = contextMenuStripGoto;
                        goToToolStripMenuItem.Text = "Add to Project Database...";
                        goToToolStripMenuItem.Click += new EventHandler(addMemoryToNotesDatabase_Click);
                    }
                }
            }
            else
            {
                ActionCommand temp = (ActionCommand)commandTree.SelectedNode.Tag;
                if (temp.ReadPointer() != 0)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Goto offset...";
                    goToToolStripMenuItem.Click += new EventHandler(goToOffset_Click);
                }
                else if (temp.Opcode == 0xD0)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Edit action script...";
                    goToToolStripMenuItem.Click += new EventHandler(goToAction_Click);
                }
                // 0xa0 - 0xa6  // 0xd8 - 0xde
                else if (temp.Opcode == 0xA0 || temp.Opcode == 0xA1 || temp.Opcode == 0xA2 ||
                    temp.Opcode == 0xA4 || temp.Opcode == 0xA5 || temp.Opcode == 0xA6 ||
                    temp.Opcode == 0xD8 || temp.Opcode == 0xD9 || temp.Opcode == 0xDA ||
                    temp.Opcode == 0xDC || temp.Opcode == 0xDD || temp.Opcode == 0xDE)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Add to Project Database...";
                    goToToolStripMenuItem.Click += new EventHandler(addMemoryToNotesDatabase_Click);
                }
            }
        }
        private void commandTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            EventCommand esc;
            ActionCommand asc;
            if (e.Node.Parent != null || isActionScript)
            {
                asc = (ActionCommand)e.Node.Tag;
                asc.Modified = e.Node.Checked;
            }
            else
            {
                esc = (EventCommand)e.Node.Tag;
                esc.Modified = e.Node.Checked;
            }
        }
        private void commandTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (!commandTree.Enabled)
                return;
            if (!commandTree.Focused)
                return;
            //
            switch (e.KeyData)
            {
                case Keys.Control | Keys.A: Do.SelectAllNodes(commandTree.Nodes, true); break;
                case Keys.Control | Keys.D: Do.SelectAllNodes(commandTree.Nodes, false); break;
                case Keys.Control | Keys.C: EvtScrCopyCommand.PerformClick(); break;
                case Keys.Control | Keys.V: EvtScrPasteCommand.PerformClick(); break;
                case Keys.Shift | Keys.Up:
                case Keys.Control | Keys.Up:
                    e.SuppressKeyPress = true;
                    EvtScrMoveUp.PerformClick();
                    break;
                case Keys.Shift | Keys.Down:
                case Keys.Control | Keys.Down:
                    e.SuppressKeyPress = true;
                    EvtScrMoveDown.PerformClick();
                    break;
                case Keys.Delete: EvtScrDeleteCommand.PerformClick(); break;
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
            }
        }
        // functions
        private void EvtScrMoveUp_Click(object sender, EventArgs e)
        {
            this.Refreshing = true;
            if (commandTree.SelectedNode == null)
                return;
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            try
            {
                esc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Index];
            }
            catch
            {
            }
            treeViewWrapper.MoveUp();
            this.Refreshing = false;
            Do.AddHistory(this, index, commandTree.SelectedNode, "MoveUpCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrMoveDown_Click(object sender, EventArgs e)
        {
            this.Refreshing = true;
            if (commandTree.SelectedNode == null)
                return;
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            try
            {
                esc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Index];
            }
            catch
            {
            }
            treeViewWrapper.MoveDown();
            this.Refreshing = false;
            Do.AddHistory(this, index, commandTree.SelectedNode, "MoveDownCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrCopyCommand_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            treeViewWrapper.Copy();
            Do.AddHistory(this, index, commandTree.SelectedNode, "CopyCommand");
        }
        private void EvtScrPasteCommand_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            treeViewWrapper.Paste();
            UpdateCommandData();
            //
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            Do.AddHistory(this, index, commandTree.SelectedNode, "PasteCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrDeleteCommand_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != null && commandTree.SelectedNode == modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            treeViewWrapper.RemoveNode();
            UpdateCommandData();
            //
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            Do.AddHistory(this, index, commandTree.SelectedNode, "DeleteCommand");
            //
            PushCommand(buffer);
        }
        private void undo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.UndoCommand();
            commandTree.EndUpdate();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.RedoCommand();
            commandTree.EndUpdate();
        }
        private void EvtScrEditCommand_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            panelCommands.SuspendDrawing();
            ResetControls();
            // action queue command
            if (commandTree.SelectedNode.Parent != null)
            {
                this.esc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Parent.Index];
                this.asc = esc.Queue.Commands[commandTree.SelectedNode.Index];
                ControlDisassembleAction();
            }
            // action script command
            else if (isActionScript)
            {
                this.asc = actionScripts[currentScript].Commands[commandTree.SelectedNode.Index];
                this.esc = null;
                ControlDisassembleAction();
            }
            // event script command
            else
            {
                this.esc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Index];
                this.asc = null;
                ControlDisassembleEvent();
            }
            panelCommands.ResumeDrawing();
            //
            buttonApplyEvent.Enabled = true;
            UpdateCommandData();
            //
            modifiedNode = commandTree.SelectedNode;
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            treeViewWrapper.EditedNode = modifiedNode;
        }
        private void EvtScrCollapseAll_Click(object sender, EventArgs e)
        {
            treeViewWrapper.CollapseAll();
            UpdateCommandData();
        }
        private void EvtScrExpandAll_Click(object sender, EventArgs e)
        {
            treeViewWrapper.ExpandAll();
            UpdateCommandData();
        }
        private void EvtScrClearAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "You are about to clear the current script of all commands.\n\nGo ahead with process?",
            "LAZYSHELL++", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            treeViewWrapper.ClearAll();
            UpdateCommandData();
            Do.AddHistory(this, index, "ClearAll");
        }
        private void EventPreview_Click(object sender, EventArgs e)
        {
            PreviewEventOrAction();
        }
        // GUI command editor
        private void categories_es_SelectedIndexChanged(object sender, EventArgs e)
        {
            isActionSelected = false;
            commands.Items.Clear();
            commands.Items.AddRange(Lists.EventNames(categories_es.SelectedIndex));
            commands.SelectedIndex = 0;
        }
        private void categories_aq_SelectedIndexChanged(object sender, EventArgs e)
        {
            isActionSelected = true;
            commands.Items.Clear();
            commands.Items.AddRange(Lists.ActionNames(categories_aq.SelectedIndex));
            commands.SelectedIndex = 0;
        }
        private void commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            byte[] temp;
            int opcode;
            int param1;
            if (!isActionSelected)
            {
                opcode = Lists.EventOpcodes[categories_es.SelectedIndex][commands.SelectedIndex];
                param1 = Lists.EventParams[categories_es.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetEventCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                esc = new EventCommand(temp, 0);
                asc = null;
            }
            else
            {
                opcode = Lists.ActionOpcodes[categories_aq.SelectedIndex][commands.SelectedIndex];
                param1 = Lists.ActionParams[categories_aq.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetActionCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                asc = new ActionCommand(temp, 0);
            }
            modifiedNode = null;  // the COMMAND PROPERTIES panel now contains a new node instead (2008-11-09)
            panelCommands.SuspendDrawing();
            ResetControls();
            if (!isActionSelected)
                ControlDisassembleEvent();
            else
                ControlDisassembleAction();
            panelCommands.ResumeDrawing();
            buttonInsertEvent.Enabled = true;
            buttonApplyEvent.Enabled = false;
        }
        private void button1_CheckedChanged(object sender, EventArgs e)
        {
            if (button1.Checked)
            {
                categories_aq.BringToFront();
                categories_aq.SelectedIndex = 0;
                categories_aq_SelectedIndexChanged(null, null);
            }
            else
            {
                categories_es.BringToFront();
                categories_es.SelectedIndex = 0;
                categories_es_SelectedIndexChanged(null, null);
            }
        }
        private void evtNameA1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            //
            panelCommands.SuspendDrawing();
            if (asc != null)
            {
                switch (asc.Opcode)
                {
                    case 0xF2:
                    case 0xF3:
                    case 0xF8:
                        evtNumA1.Value = evtNameA1.SelectedIndex;  // Level names
                        break;
                }
            }
            else
            {
                switch (esc.Opcode)
                {
                    case 0xF2:
                    case 0xF3:
                    case 0xF8:
                    case 0x68:
                    case 0x6A:
                    case 0x6B:
                    case 0x60:
                    case 0x62:
                        evtNumA1.Value = evtNameA1.SelectedIndex;  // Level names, Dialogue names
                        break;
                    case 0x50:
                    case 0x51:
                        evtNumA1.Value = Model.ItemNames.GetUnsortedIndex(evtNameA1.SelectedIndex);    // Item names
                        break;
                    case 0x4E:
                        this.Updating = true;
                        labelEvtA2.Text = "";
                        labelEvtA3.Text = "";
                        evtNameA2.Items.Clear(); evtNameA2.ResetText(); evtNameA2.Enabled = false; evtNameA2.DropDownWidth = evtNameA2.Width;
                        evtNameA2.DrawMode = DrawMode.Normal; evtNameA2.BackColor = SystemColors.Window; evtNameA2.ItemHeight = 13;
                        evtNumA2.Value = 0; evtNumA2.Maximum = 255; evtNumA2.Enabled = false;
                        evtNumA3.Value = 0; evtNumA3.Maximum = 255; evtNumA3.Enabled = false;
                        switch (evtNameA1.SelectedIndex)
                        {
                            case 2: // open world location
                                labelEvtA2.Text = "Location";
                                evtNameA2.Items.AddRange(Lists.Numerize(Lists.MapNames));
                                evtNameA2.DropDownWidth = 200; evtNameA2.Enabled = true;
                                evtNameA2.SelectedIndex = 0;
                                break;
                            case 3: // open shop menu
                                labelEvtA2.Text = "Shop menu";
                                evtNameA2.Items.AddRange(Lists.ShopNames);
                                evtNameA2.DropDownWidth = 200; evtNameA2.Enabled = true;
                                evtNameA2.SelectedIndex = 0;
                                break;
                            case 5: // items maxed out
                                labelEvtA2.Text = "Toss item";
                                evtNameA2.Items.AddRange(Model.ItemNames.Names);
                                evtNameA2.DrawMode = DrawMode.OwnerDrawFixed; evtNameA2.Enabled = true;
                                evtNumA2.Enabled = true;
                                evtNameA2.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA2.Value);
                                break;
                            case 7: // menu tutorial
                                labelEvtA2.Text = "Tutorial";
                                evtNameA2.Items.AddRange(Lists.Tutorials);
                                evtNameA2.Enabled = true;
                                evtNameA2.SelectedIndex = 0;
                                break;
                            case 8: // add star piece
                            case 13:// run star piece end sequence
                                labelEvtA2.Text = "Star Piece";
                                evtNumA2.Enabled = true;
                                evtNumA2.Maximum = 7;
                                break;
                            case 16:    // world map event
                                labelEvtA2.Text = "Map event";
                                evtNameA2.Items.AddRange(new string[] { "Mario falls to pipehouse", "Mario returns to MK", "Mario takes Nimbus bus" });
                                evtNameA2.Enabled = true;
                                evtNameA2.SelectedIndex = 0;
                                break;
                        }
                        OrganizeControls();
                        this.Updating = false;
                        break;
                    case 0x97:
                        labelEvtA3.Text = evtNameA1.SelectedIndex == 0 ? "Slow down" : "Speed up";
                        break;
                    case 0xFD:
                        switch (esc.Param1)
                        {
                            case 0x58:
                                evtNumA1.Value = Model.ItemNames.GetUnsortedIndex(evtNameA1.SelectedIndex);    // Item names
                                break;
                        }
                        break;
                }
            }
            panelCommands.ResumeDrawing();
        }
        private void evtNameA1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void evtNumA1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (asc != null)
            {
                switch (asc.Opcode)
                {
                    case 0xF2:
                    case 0xF3:
                    case 0xF8:
                        evtNameA1.SelectedIndex = (int)evtNumA1.Value;  // Level names, Dialogue names
                        break;
                }
            }
            else
            {
                switch (esc.Opcode)
                {
                    case 0xF2:
                    case 0xF3:
                    case 0xF8:
                    case 0x68:
                    case 0x6A:
                    case 0x6B:
                    case 0x60:
                    case 0x62:
                        evtNameA1.SelectedIndex = (int)evtNumA1.Value;  // Level names, Dialogue names
                        break;
                    case 0x50:
                    case 0x51:
                        evtNameA1.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA1.Value);    // Item names
                        break;
                }
            }
        }
        private void evtNameA2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (asc != null)
                return;
            //
            switch (esc.Opcode)
            {
                case 0x54:
                case 0x4E:
                    evtNumA2.Value = Model.ItemNames.GetUnsortedIndex(evtNameA2.SelectedIndex);    // Item names
                    break;
                case 0x4A:
                    evtNumA2.Value = evtNameA2.SelectedIndex; // battlefields
                    break;
                default:
                    if (esc.Opcode <= 0x2F)
                    {
                        labelEvtA3.Text = "";
                        groupBoxB.Text = "";
                        evtNumA3.Value = 0; evtNumA3.Maximum = 255; evtNumA3.Enabled = false;
                        evtEffects.Items.Clear(); evtEffects.Enabled = false;
                        if (evtNameA2.SelectedIndex < 3) // queue options need sync bit
                        {
                            evtEffects.Items.AddRange(new string[] { "asynchronous" });
                            evtEffects.Enabled = true;
                        }
                        else if (evtNameA2.SelectedIndex >= 3 && evtNameA2.SelectedIndex <= 6) // options 0xF2-0xF5
                        {
                            labelEvtA3.Text = "Action #";
                            evtNumA3.Maximum = 0x3FF; evtNumA3.Enabled = true;
                        }
                        else
                        {
                            labelEvtA3.Text = "";
                            evtNumA3.Enabled = false;
                        }
                    }
                    break;
            }
            OrganizeControls();
        }
        private void evtNameA2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void evtNumA2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (asc == null)
            {
                switch (esc.Opcode)
                {
                    case 0x54:
                    case 0x4E:
                        if (evtNameA1.SelectedIndex != 8 &&
                            evtNameA1.SelectedIndex != 13)
                            evtNameA2.SelectedIndex = Model.ItemNames.GetSortedIndex((int)evtNumA2.Value);    // Item names
                        break;
                    case 0x4A:
                        evtNameA2.SelectedIndex = (int)evtNumA2.Value;    // battlefields
                        break;
                }
            }
        }
        private void evtEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (asc != null)
            {
                switch (asc.Opcode)
                {
                    case 0x08:
                        labelEvtA4.Text = evtEffects.GetItemChecked(0) ? "Mold" : "Sequence";
                        break;
                }
            }
        }
        private void buttonInsertEvent_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            EventCommand esc;
            // if editing a non-blank script
            if (commandTree.SelectedNode != null)
            {
                // if inserting action queue/script command
                if (commandTree.SelectedNode.Parent != null || isActionScript)
                    InsertActionCommand();
                else
                {
                    esc = eventScripts[currentScript].Commands[commandTree.SelectedNode.Index];
                    // if adding action queue command to an empty queue trigger
                    if (esc.QueueTrigger && isActionSelected)
                        InsertActionCommand();
                    // if inserting an event command
                    else
                        InsertEventCommand();
                }
            }
            // if inserting action command to a blank action script
            else if (isActionScript)
                InsertActionCommand();
            // if inserting event command to a blank event script
            else
                InsertEventCommand();
            if (!isActionScript)
                UpdateEventScriptsFreeSpace();
            else
                UpdateActionScriptsFreeSpace();
            UpdateCommandData();
            //
            if (modifiedNode != null)
            {
                modifiedNode = commandTree.SelectedNode;
                treeViewWrapper.EditedNode = modifiedNode;
            }
            //
            PushCommand(buffer);
        }
        private void buttonApplyEvent_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            if (modifiedNode != null)
            {
                if (modifiedNode.Parent != null || isActionScript)
                {
                    ControlAssembleAction();
                    treeViewWrapper.ReplaceNode(asc);
                    UpdateActionScriptsFreeSpace();
                }
                else
                {
                    ControlAssembleEvent();
                    treeViewWrapper.ReplaceNode(esc);
                    UpdateEventScriptsFreeSpace();
                }
            }
            UpdateCommandData();
            EvtScrEditCommand.PerformClick();
            Do.AddHistory(this, index, commandTree.SelectedNode, "EditCommand");
            //
            PushCommand(buffer);
        }
        // menustrip
        private void save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Assemble();
            Cursor.Current = Cursors.Arrow;
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            if (!isActionScript)
                Model.HexEditor.SetOffset(eventScript.BaseOffset);
            else
                Model.HexEditor.SetOffset(actionScript.BaseOffset);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        // IO elements
        private void importEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            int[] baseOffsets = new int[Model.EventScripts.Length];
            int[] lengths = new int[Model.EventScripts.Length];
            for (int i = 0; i < lengths.Length; i++)
            {
                baseOffsets[i] = Model.EventScripts[i].BaseOffset;
                lengths[i] = Model.EventScripts[i].Length;
            }
            //
            IOElements ioelements = new IOElements((Element[])Model.EventScripts, index, "IMPORT EVENT SCRIPTS...");
            ioelements.ShowDialog();
            if (ioelements.DialogResult != DialogResult.OK)
                return;
            bool importAll = (bool)ioelements.Tag;
            if (importAll)
            {
                // first, update offsets for any changes made in current script
                if (treeViewWrapper.ScriptDelta != 0)
                    UpdateScriptOffsets();
                // now, update offsets following each newly imported script w/new length
                int baseOffset = 0x1E0C00;
                int lastImported = 1;
                for (int i = 0; i < 4092; i++)
                {
                    int delta = Model.EventScripts[i].Length - lengths[i];
                    treeViewWrapper.ScriptDelta += delta;
                    Model.EventScripts[i].BaseOffset = baseOffset;
                    // only refresh script if a new one was imported
                    if (delta != 0 || baseOffset != baseOffsets[i])
                        Model.EventScripts[i].Refresh();
                    // only need to update if new length
                    if (delta != 0 && lastImported != i - 1)
                    {
                        lastImported = i;
                        UpdateScriptOffsets(i);
                        treeViewWrapper.ScriptDelta = 0;
                    }
                    baseOffset += Model.EventScripts[i].Length;
                    if (i == 1535)
                        baseOffset = 0x1F0C00;
                    if (i == 3071)
                        baseOffset = 0x200800;
                }
            }
            else if (!importAll) // if importing single script into current
            {
                UpdateScriptOffsets(index);
                eventScript.BaseOffset = baseOffsets[index];
            }
            treeViewWrapper.ChangeScript(eventScript);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }
        private void importActionScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            int[] baseOffsets = new int[Model.ActionScripts.Length];
            int[] lengths = new int[Model.ActionScripts.Length];
            for (int i = 0; i < lengths.Length; i++)
            {
                baseOffsets[i] = Model.ActionScripts[i].BaseOffset;
                lengths[i] = Model.ActionScripts[i].Length;
            }
            //
            IOElements ioelements = new IOElements((Element[])Model.ActionScripts, index, "IMPORT ACTION SCRIPTS...");
            ioelements.ShowDialog();
            if (ioelements.DialogResult != DialogResult.OK)
                return;
            bool importAll = (bool)ioelements.Tag;
            if (importAll)
            {
                // first, update offsets for any changes made in current script
                if (treeViewWrapper.ScriptDelta != 0)
                    UpdateActionOffsets();
                // now, update offsets following each newly imported script w/new length
                int baseOffset = 0x210800;
                int lastImported = 1;
                for (int i = 0; i < 1024; i++)
                {
                    int delta = Model.ActionScripts[i].Length - lengths[i];
                    treeViewWrapper.ScriptDelta += delta;
                    Model.ActionScripts[i].BaseOffset = baseOffset;
                    // only refresh script if a new one was imported
                    if (delta != 0 || baseOffset != baseOffsets[i])
                        Model.ActionScripts[i].Refresh();
                    // only need to update if new length
                    if (delta != 0 && lastImported != i - 1)
                    {
                        lastImported = i;
                        UpdateActionOffsets(i);
                        treeViewWrapper.ScriptDelta = 0;
                    }
                    baseOffset += Model.ActionScripts[i].Length;
                }
            }
            else if (!importAll) // if importing single script into current
            {
                UpdateActionOffsets(index);
                actionScript.BaseOffset = baseOffsets[index];
            }
            treeViewWrapper.ChangeScript(actionScript);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }
        private void exportEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.EventScripts, index, "EXPORT EVENT SCRIPTS...").ShowDialog();
        }
        private void exportActionScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements((Element[])Model.ActionScripts, index, "EXPORT ACTION SCRIPTS...").ShowDialog();
        }
        private void dumpEventScriptTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - eventScripts.txt";
            saveFileDialog.RestoreDirectory = true;
            List<EventCommand> commands;
            List<ActionCommand> queue;
            EventCommand esc;
            ActionCommand asc;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(saveFileDialog.FileName);
                for (int i = 0; i < eventScripts.Length; i++)
                {
                    commands = eventScripts[i].Commands;
                    writer.WriteLine("[" + i.ToString("d4") + "]" +
                        "------------------------------------------------------------>");
                    for (int j = 0; j < commands.Count; j++)
                    {
                        esc = commands[j];
                        writer.Write((esc.Offset).ToString("X6") + ": ");
                        if (esc.Opcode <= 0x2F && esc.Param1 <= 0xF1 && !esc.Locked)
                        {
                            if (esc.Param1 == 0xF0 || esc.Param1 == 0xF1)
                                writer.Write("{" + BitConverter.ToString(esc.CommandData, 0, 3) + "}            ");
                            else
                                writer.Write("{" + BitConverter.ToString(esc.CommandData, 0, 2) + "}               ");
                            writer.Write(esc.ToString() + "\n");
                            if (esc.Queue.Commands != null)
                            {
                                queue = esc.Queue.Commands;
                                for (int k = 0; k < queue.Count; k++)
                                {
                                    asc = queue[k];
                                    writer.Write("   " + (asc.Offset).ToString("X6") + ": ");
                                    writer.Write("{" + BitConverter.ToString(asc.CommandData) + "}");
                                    for (int l = asc.Length; l < 7; l++)
                                        writer.Write("   ");
                                    writer.Write(asc.ToString() + "\n");
                                }
                            }
                        }
                        else if (esc.Locked)   // 0xd01 and 0xe91 only
                        {
                            writer.Write("NON-EMBEDDED ACTION QUEUE\n");
                            if (esc.Queue.Commands != null)
                            {
                                queue = esc.Queue.Commands;
                                for (int k = 0; k < queue.Count; k++)
                                {
                                    asc = queue[k];
                                    writer.Write("   " + (asc.Offset).ToString("X6") + ": ");
                                    writer.Write("{" + BitConverter.ToString(asc.CommandData) + "}");
                                    for (int l = asc.Length; l < 7; l++)
                                        writer.Write("   ");
                                    writer.Write(asc.ToString() + "\n");
                                }
                            }
                        }
                        else
                        {
                            writer.Write("{" + BitConverter.ToString(esc.CommandData) + "}");
                            for (int k = esc.Length; k < 7; k++)
                                writer.Write("   ");
                            writer.Write(esc.ToString() + "\n");
                        }
                    }
                    writer.Write("\n");
                }
                writer.Close();
            }
        }
        private void dumpActionScriptTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = Model.GetFileNameWithoutPath() + " - actionScripts.txt";
            saveFileDialog.RestoreDirectory = true;
            //
            List<ActionCommand> commands;
            ActionCommand asc;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(saveFileDialog.FileName);
                for (int i = 0; i < actionScripts.Length; i++)
                {
                    commands = actionScripts[i].Commands;
                    writer.WriteLine("[" + i.ToString("d4") + "]" +
                        "------------------------------------------------------------>");
                    if (commands != null)
                    {
                        for (int k = 0; k < commands.Count; k++)
                        {
                            asc = commands[k];
                            writer.Write((asc.Offset).ToString("X6") + ": ");
                            writer.Write("{" + BitConverter.ToString(asc.CommandData) + "}");
                            for (int l = asc.Length; l < 7; l++)
                                writer.Write("   ");
                            writer.Write(asc.ToString() + "\n");
                        }
                    }
                    writer.Write("\n");
                }
                writer.Close();
            }
        }
        private void clearEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            int[] lengths = new int[Model.EventScripts.Length];
            for (int i = 0; i < lengths.Length; i++)
                lengths[i] = Model.EventScripts[i].Length;
            //
            ClearElements window = new ClearElements(Model.EventScripts, index, "CLEAR EVENT SCRIPTS...");
            window.ShowDialog();
            if (window.DialogResult != DialogResult.OK)
                return;
            //
            Point tag = (Point)window.Tag;
            int start = tag.X;
            int end = tag.Y;
            for (int i = start; i <= end; i++)
            {
                treeViewWrapper.ScriptDelta += Model.EventScripts[i].Length - lengths[i];
                if (i == 1535 && end >= 1536)
                {
                    UpdateScriptOffsets(start);
                    treeViewWrapper.ScriptDelta = 0;
                    start = 1536;
                }
                if (i == 3071 && end >= 3072)
                {
                    UpdateScriptOffsets(start);
                    treeViewWrapper.ScriptDelta = 0;
                    start = 3072;
                }
            }
            UpdateScriptOffsets(start);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }//
        private void clearActionScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            int[] lengths = new int[Model.ActionScripts.Length];
            for (int i = 0; i < lengths.Length; i++)
                lengths[i] = Model.ActionScripts[i].Length;
            //
            ClearElements window = new ClearElements(Model.ActionScripts, index, "CLEAR ACTION SCRIPTS...");
            window.ShowDialog();
            if (window.DialogResult != DialogResult.OK)
                return;
            //
            Point tag = (Point)window.Tag;
            int start = tag.X;
            int end = tag.Y;
            for (int i = start; i <= end; i++)
                treeViewWrapper.ScriptDelta += Model.ActionScripts[i].Length - lengths[i];
            UpdateActionOffsets(start);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current script. Go ahead with reset?",
                "LAZYSHELL++", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            commandStack.Clear();
            commandTree.BeginUpdate();
            if (!isActionScript)
            {
                int length = eventScript.Length;
                int baseOffset = eventScript.BaseOffset;
                eventScript = new EventScript(index);
                eventScript.BaseOffset = baseOffset;
                treeViewWrapper.SelectedNode = null;
                treeViewWrapper.ScriptDelta += eventScript.Length - length;
                treeViewWrapper.ChangeScript(eventScript);
                treeViewWrapper.RefreshScript();
            }
            else
            {
                int length = actionScript.Length;
                int baseOffset = actionScript.BaseOffset;
                actionScript = new ActionScript(index);
                actionScript.BaseOffset = baseOffset;
                treeViewWrapper.SelectedNode = null;
                treeViewWrapper.ScriptDelta += actionScript.Length - length;
                treeViewWrapper.ChangeScript(actionScript);
                treeViewWrapper.RefreshScript();
            }
            commandTree.EndUpdate();
        }
        // Tool: Update Pointers
        private void updatePointerScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), treeViewWrapper.SelectedIndex);
            //
            object Script = !isActionScript ? (object)Model.EventScripts : (object)Model.ActionScripts;
            UpdatePointer window = new UpdatePointer(Script, index);
            window.ShowDialog();
            if (window.DialogResult != DialogResult.OK)
                return;
            treeViewWrapper.Populate();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }

        // context menustrip
        private void goToDialogue_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
            int num = Bits.GetShort(temp.CommandData, 1) & 0xFFF;
            if (Model.Program.Dialogues == null || !Model.Program.Dialogues.Visible)
                Model.Program.CreateDialoguesWindow();
            Model.Program.Dialogues.DialogueNum.Value = num;
            Model.Program.Dialogues.BringToFront();
        }
        private void addMemoryToNotesDatabase_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            int address = 0x7000;
            int addressBit = 0;
            string label = "";
            string description = "";
            if (commandTree.SelectedNode.Tag.GetType() == typeof(EventCommand))
            {
                EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
                if (temp.Opcode >= 0xA0 && temp.Opcode <= 0xA2)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xA000) / 8) + 0x7040;
                if (temp.Opcode >= 0xA4 && temp.Opcode <= 0xA6)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xA400) / 8) + 0x7040;
                if (temp.Opcode >= 0xD8 && temp.Opcode <= 0xDA)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xD800) / 8) + 0x7040;
                if (temp.Opcode >= 0xDC && temp.Opcode <= 0xDE)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xDC00) / 8) + 0x7040;
                addressBit = temp.Param1 & 0x07;
                if (temp.Param1 == 0xFD)
                {
                    if (temp.Param1 >= 0xA0 && temp.Param1 <= 0xA2)
                        address = ((((temp.Param1 * 0x100) + temp.Param2) - 0xA000) / 8) + 0x7040;
                    if (temp.Param1 >= 0xA4 && temp.Param1 <= 0xA6)
                        address = ((((temp.Param1 * 0x100) + temp.Param2) - 0xA400) / 8) + 0x7040;
                    if (temp.Param1 >= 0xD8 && temp.Param1 <= 0xDA)
                        address = ((((temp.Param1 * 0x100) + temp.Param2) - 0xD800) / 8) + 0x7040;
                    if (temp.Param1 >= 0xDC && temp.Param1 <= 0xDE)
                        address = ((((temp.Param1 * 0x100) + temp.Param2) - 0xDC00) / 8) + 0x7040;
                    addressBit = temp.Param2 & 0x07;
                }
            }
            else
            {
                ActionCommand temp = (ActionCommand)commandTree.SelectedNode.Tag;
                if (temp.Opcode >= 0xA0 && temp.Opcode <= 0xA2)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xA000) / 8) + 0x7040;
                if (temp.Opcode >= 0xA4 && temp.Opcode <= 0xA6)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xA400) / 8) + 0x7040;
                if (temp.Opcode >= 0xD8 && temp.Opcode <= 0xDA)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xD800) / 8) + 0x7040;
                if (temp.Opcode >= 0xDC && temp.Opcode <= 0xDE)
                    address = ((((temp.Opcode * 0x100) + temp.Param1) - 0xDC00) / 8) + 0x7040;
                addressBit = temp.Param1 & 0x07;
            }
            label = description = "[" + address.ToString("X4") + ", bit: " + addressBit.ToString() + "]";
            if (Model.Program.Project == null || !Model.Program.Project.Visible)
                Model.Program.CreateProjectWindow();
            Project note = Model.Program.Project;
            if (Model.Project == null)
                if (!note.LoadProject())
                    return;
            if (Model.Project != null)
            {
                note.AddingFromEditor("Memory Bits", address, addressBit, label, description);
                note.BringToFront();
            }
            else
            {
                MessageBox.Show("Could not add element to notes database.", "LAZYSHELL++",
                    MessageBoxButtons.OK);
            }
        }
        private void goToEvent_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
            int num = 0;
            if (temp.Opcode != 0xFD)
                num = Bits.GetShort(temp.CommandData, 1) & 0xFFF;
            else
                num = Bits.GetShort(temp.CommandData, 2) & 0xFFF;
            eventNum.Value = num;
        }
        private void goToOffset_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            EventActionCommand temp = (EventActionCommand)commandTree.SelectedNode.Tag;
            int pointer;
            if (isActionScript)
            {
                pointer = temp.ReadPointer() + (actionScript.BaseOffset & 0xFF0000);
                foreach (ActionScript script in actionScripts)
                {
                    foreach (ActionCommand action in script.Commands)
                    {
                        if (action.Offset + action.CommandData.Length > pointer || action.Offset >= pointer)
                        {
                            index = script.Index;
                            treeViewWrapper.SelectNode(action);
                            return;
                        }
                    }
                }
                return;
            }
            pointer = temp.ReadPointer() + (eventScript.BaseOffset & 0xFF0000);
            foreach (EventScript script in eventScripts)
            {
                foreach (EventCommand command in script.Commands)
                {
                    if (command.Queue != null)
                    {
                        foreach (ActionCommand action in command.Queue.Commands)
                        {
                            if (action.Offset + action.CommandData.Length > pointer || action.Offset >= pointer)
                            {
                                if (command.Offset + command.Length > pointer || command.Offset >= pointer)
                                {
                                    index = script.Index;
                                    treeViewWrapper.SelectNode(command);
                                    return;
                                }
                                index = script.Index;
                                treeViewWrapper.SelectNode(action);
                                return;
                            }
                        }
                    }
                    if (command.Offset + command.Length > pointer || command.Offset >= pointer)
                    {
                        index = script.Index;
                        treeViewWrapper.SelectNode(command);
                        return;
                    }
                }
            }
        }
        private void goToAction_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            int num = index;
            if (commandTree.SelectedNode.Tag.GetType() == typeof(EventCommand))
            {
                EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
                num = Bits.GetShort(temp.CommandData, 2);
            }
            else
            {
                ActionCommand temp = (ActionCommand)commandTree.SelectedNode.Tag;
                num = Bits.GetShort(temp.CommandData, 1);
            }
            disableNavigate = true;
            type = 1;
            disableNavigate = false;
            eventNum.Value = num;
        }
        #endregion
        private class CommandEdit : Command
        {
            private int index;
            private EventScripts form;
            private ScriptBuffer buffer;
            private EventScript[] eventScripts;
            private ActionScript[] actionScripts;
            private TreeViewWrapper treeViewWrapper;
            private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
            public CommandEdit(EventScript[] eventScripts, int index, ScriptBuffer buffer, EventScripts form)
            {
                this.form = form;
                this.index = index;
                this.buffer = buffer;
                this.eventScripts = eventScripts;
                this.treeViewWrapper = form.TreeViewWrapper;
            }
            public CommandEdit(ActionScript[] actionScripts, int index, ScriptBuffer buffer, EventScripts form)
            {
                this.form = form;
                this.index = index;
                this.buffer = buffer;
                this.actionScripts = actionScripts;
                this.treeViewWrapper = form.TreeViewWrapper;
            }
            public void Execute()
            {
                if (eventScripts != null)
                {
                    eventScripts[index].Undoing = true;
                    //
                    this.form.type = 0; // first switch back to event scripts
                    this.form.index = index; // then switch back to script in index
                    // now get difference in lengths
                    int length = eventScripts[index].Length;
                    int delta = buffer.OldScript.Length - length;
                    treeViewWrapper.ScriptDelta += delta;
                    // next, switch the scripts
                    byte[] temp = Bits.Copy(eventScripts[index].Script);
                    eventScripts[index].Script = Bits.Copy(buffer.OldScript);
                    eventScripts[index].Commands = null;
                    eventScripts[index].ParseScript();
                    buffer.OldScript = temp;
                    //
                    int newSelectedIndex = treeViewWrapper.SelectedIndex;
                    treeViewWrapper.RefreshScript(buffer.OldSelectedIndex);
                    buffer.OldSelectedIndex = newSelectedIndex;
                    //
                    eventScripts[index].Undoing = false;
                }
                else if (actionScripts != null)
                {
                    actionScripts[index].Undoing = true;
                    //
                    this.form.type = 1; // first switch back to action scripts
                    this.form.index = index; // then switch back to script index
                    // now get difference in lengths
                    int length = actionScripts[index].Length;
                    int delta = buffer.OldScript.Length - length;
                    treeViewWrapper.ScriptDelta += delta;
                    // next, switch the scripts
                    byte[] temp = Bits.Copy(actionScripts[index].Script);
                    actionScripts[index].Script = Bits.Copy(buffer.OldScript);
                    actionScripts[index].Commands = null;
                    actionScripts[index].ParseScript();
                    buffer.OldScript = temp;
                    //
                    treeViewWrapper.RefreshScript(buffer.OldSelectedIndex);
                    buffer.OldSelectedIndex = treeViewWrapper.SelectedIndex;
                    //
                    actionScripts[index].Undoing = false;
                }
            }
        }
        private class ScriptBuffer
        {
            public byte[] OldScript;
            public int OldSelectedIndex;
            public ScriptBuffer(byte[] oldScript, int oldSelectedIndex)
            {
                this.OldScript = oldScript;
                this.OldSelectedIndex = oldSelectedIndex;
            }
        }

        private void toolStrip2_SizeChanged(object sender, EventArgs e)
        {
            eventHexText.Width = toolStrip2.Width - 405;
        }
    }
}
