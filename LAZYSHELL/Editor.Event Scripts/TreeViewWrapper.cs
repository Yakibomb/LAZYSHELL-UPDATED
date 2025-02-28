using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    public class TreeViewWrapper
    {
        #region Variables
        private NewTreeView treeView;
        private TreeNode selectedNode; public TreeNode SelectedNode { get { return selectedNode; } set { selectedNode = value; } }
        private TreeNode editedNode; public TreeNode EditedNode { get { return editedNode; } set { editedNode = value; } }
        public byte[] CurrentNodeData
        {
            get
            {
                EventCommand esc;
                ActionCommand asc;
                //
                TreeNode node = treeView.SelectedNode;
                if (node == null)
                    return new byte[0];
                int index = node.Index;
                if (!IsRootNode(node))  // we are editing an embedded action queue command
                    node = node.Parent;
                else if (actionScript)  // we are editing an action script command
                {
                    asc = action.Commands[index];
                    return asc.CommandData;
                }
                else    // we are editing an event script command
                {
                    esc = script.Commands[index];
                    return esc.CommandData;
                }
                if (!IsRootNode(node))
                    throw new Exception();
                //
                esc = script.Commands[node.Index];
                asc = esc.Queue.Commands[index];
                return asc.CommandData;
            }
        }
        public int SelectedIndex
        {
            get
            {
                return treeView.GetFullIndex();
            }
            set
            {
                treeView.SelectNode(value);
            }
        }
        private EventScript script; public EventScript Script { get { return this.script; } set { this.script = value; } }
        private ActionScript action; public ActionScript Action { get { return this.action; } set { this.action = value; } }
        private bool actionScript; public bool ActionScript { get { return this.actionScript; } set { this.actionScript = value; } }
        private int scriptDelta = 0; public int ScriptDelta { get { return this.scriptDelta; } set { this.scriptDelta = value; } }
        private ArrayList commandCopies;
        #endregion
        // constructor
        public TreeViewWrapper(NewTreeView control)
        {
            this.treeView = control;
        }
        #region Functions
        public void ChangeScript(EventScript script)
        {
            this.script = script;
            foreach (EventCommand esc in script.Commands)
                esc.ResetOriginalOffset();
            Populate();
        }
        public void ChangeScript(ActionScript action)
        {
            this.action = action;
            foreach (ActionCommand asc in action.Commands)
                asc.ResetOriginalOffset();
            Populate();
        }
        public void Populate()
        {
            this.treeView.Nodes.Clear();
            if (!actionScript)
            {
                for (int i = 0; i < script.Commands.Count; i++)
                    AddCommand(script.Commands[i]);
            }
            else
            {
                for (int i = 0; i < action.Commands.Count; i++)
                    AddCommand(action.Commands[i]);
            }
            this.treeView.ExpandAll();
        }
        // treeview managers
        private void AddCommand(EventCommand esc)
        {
            TreeNode node = esc.Node;
            if (esc.QueueTrigger || esc.Locked)
            {
                if (esc.Queue == null)
                    return;
                List<ActionCommand> queue = esc.Queue.Commands;
                for (int i = 0; queue != null && i < queue.Count; i++)
                {
                    ActionCommand asc = queue[i];
                    TreeNode child = asc.Node;
                    node.Nodes.Add(child);
                }
            }
            // Add command
            this.treeView.Nodes.Add(node);
        }
        private void AddCommand(ActionCommand asc)
        {
            // Add command
            this.treeView.Nodes.Add(asc.Node);
        }
        public void InsertNode(EventCommand esc)
        {
            try
            {
                if (actionScript)
                {
                    foreach (ActionCommand aq in action.Commands)
                        aq.Modified = false;
                }
                else
                {
                    foreach (EventCommand es in script.Commands)
                    {
                        es.Modified = false;
                        if (es.Queue == null) continue;
                        foreach (ActionCommand aq in es.Queue.Commands)
                            aq.Modified = false;
                    }
                }
                this.treeView.BeginUpdate();
                TreeNode node = treeView.SelectedNode;
                // Get index to insert at
                int index = node != null ? treeView.SelectedNode.Index + 1 : 0;
                if (node == null || IsRootNode(node)) // EvenScript Command
                {
                    // Insert into treeview
                    selectedNode = esc.Node;
                    this.treeView.Nodes.Insert(index, selectedNode);
                    // Insert into script at same index
                    esc.Modified = true;
                    this.script.Insert(index, esc);
                    this.scriptDelta += esc.Length;
                }
            }
            finally
            {
                RefreshScript(); // Update offsets and descriptions
                this.treeView.EndUpdate();
            }
        }
        public void InsertNode(ActionCommand asc)
        {
            try
            {
                if (actionScript)
                {
                    foreach (ActionCommand command in action.Commands)
                        command.Modified = false;
                }
                else
                {
                    foreach (EventCommand command in script.Commands)
                    {
                        command.Modified = false;
                        if (command.Queue == null) continue;
                        foreach (ActionCommand aqc in command.Queue.Commands)
                            aqc.Modified = false;
                    }
                }
                this.treeView.BeginUpdate();
                int index;
                TreeNode node = treeView.SelectedNode;
                // embedded action queue
                if (!actionScript)
                {
                    if (node == null)
                        return;
                    // Get index to insert at
                    index = treeView.SelectedNode.Index + 1;
                    if (node.Parent == null)
                    {
                        if ((script.Commands[treeView.SelectedNode.Index]).QueueTrigger)
                            index = 0;
                        else
                        {
                            MessageBox.Show(
                                "Cannot insert an action command outside of an action queue.",
                                "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                        node = node.Parent;
                    //
                    int maxLength;
                    EventCommand esc = (script.Commands[node.Index]);
                    if (esc.Param1 < 0xF0)
                    {
                        maxLength = (esc.Param1 & 0x80) == 0x80 ? 111 : 127;
                        if ((esc.Length - 2 + asc.Length) > maxLength)
                        {
                            MessageBox.Show(
                                "Could not add any more action commands to the queue.",
                                "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        esc.Param1 += (byte)asc.Length;
                    }
                    else
                    {
                        maxLength = (esc.Param2 & 0x80) == 0x80 ? 111 : 127;
                        if ((esc.Length - 3 + asc.Length) > maxLength)
                        {
                            MessageBox.Show(
                                "Could not add any more action commands to the queue.",
                                "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        esc.Param2 += (byte)asc.Length;
                    }
                    // Insert into treeview
                    selectedNode = asc.Node;
                    node.Nodes.Insert(index, selectedNode);
                    // Insert into action queue at same index
                    asc.Modified = true;
                    this.script.Insert(node.Index, index, asc);
                    this.scriptDelta += asc.Length;
                    //
                    treeView.ExpandAll();
                }
                // Insert action script command
                else
                {
                    // Get index to insert at
                    if (node == null)
                        index = 0;
                    else
                        index = treeView.SelectedNode.Index + 1;
                    // ActionScript Command
                    if (node == null || IsRootNode(node))
                    {
                        // Insert into treeview
                        selectedNode = asc.Node;
                        treeView.Nodes.Insert(index, selectedNode);
                        // Insert into script at same index
                        asc.Modified = true;
                        this.action.Insert(index, asc);
                        this.scriptDelta += asc.Length;
                    }
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void ReplaceNode(EventCommand esc)
        {
            try
            {
                TreeNode node = editedNode;
                if (node == null)
                    return;
                this.treeView.BeginUpdate();
                // Get index to insert at
                int index = editedNode.Index;
                selectedNode = new TreeNode(esc.ToString());
                // EvenScript Command
                if (IsRootNode(node))
                {
                    // Insert into treeview
                    this.treeView.Nodes.RemoveAt(index);
                    this.treeView.Nodes.Insert(index, esc.ToString());
                    // Insert into script at same index
                    this.script.RemoveAt(index);
                    this.script.Insert(index, esc);
                    treeView.SelectedNode = this.treeView.Nodes[index];
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void ReplaceNode(ActionCommand asc)
        {
            try
            {
                this.treeView.BeginUpdate();
                TreeNode node = editedNode;
                if (node == null)
                    return;
                // Get index to insert at
                int index = editedNode.Index;
                selectedNode = new TreeNode(asc.ToString());
                if (!actionScript)
                {
                    if (IsRootNode(node))
                        return;
                    node = node.Parent;
                    if (IsRootNode(node))
                    {
                        // Insert into treeview
                        node.Nodes.RemoveAt(index);
                        node.Nodes.Insert(index, asc.ToString());
                        // Insert into action queue at same index
                        this.script.RemoveAt(node.Index, index);
                        this.script.Insert(node.Index, index, asc);
                        treeView.SelectedNode = node.Nodes[index];
                    }
                }
                else
                {
                    // ActionScript Command
                    if (IsRootNode(node))
                    {
                        // Insert into treeview
                        this.treeView.Nodes.RemoveAt(index);
                        this.treeView.Nodes.Insert(index, asc.ToString());
                        // Insert into script at same index
                        this.action.RemoveAt(node.Index);
                        this.action.Insert(index, asc);
                        treeView.SelectedNode = this.treeView.Nodes[index];
                    }
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void RemoveNode()
        {
            try
            {
                this.treeView.BeginUpdate();
                int delta, index;
                TreeNode node;
                TreeNode parent, child;
                for (int i = treeView.Nodes.Count - 1; i >= 0; i--)
                {
                    parent = treeView.Nodes[i];
                    for (int a = parent.Nodes.Count - 1; a >= 0; a--)
                    {
                        child = parent.Nodes[a];
                        if (!child.Checked)
                            continue;
                        delta = -((ActionCommand)child.Tag).CommandData.Length;
                        node = child;
                        if (node == null)
                            return;
                        index = child.Index;
                        node = node.Parent;
                        // Decrease queue length option byte
                        EventCommand esc = script.Commands[node.Index];
                        ActionCommand aqc = esc.Queue.Commands[index];
                        if (esc.Param1 < 0xF0)
                            esc.Param1 -= (byte)aqc.Length;
                        else
                            esc.Param2 -= (byte)aqc.Length;
                        // Remove action command
                        child.Remove();
                        this.script.RemoveAt(parent.Index, child.Index);
                        this.scriptDelta += delta;
                    }
                    if (!parent.Checked)
                        continue;
                    if (!actionScript)
                        delta = -((EventCommand)parent.Tag).CommandData.Length;
                    else
                        delta = -((ActionCommand)parent.Tag).CommandData.Length;
                    node = parent;
                    if (node == null)
                        return;
                    index = parent.Index;
                    // Remove event command
                    parent.Remove();
                    if (!actionScript)
                        this.script.RemoveAt(parent.Index);
                    else
                        this.action.RemoveAt(parent.Index);
                    this.scriptDelta += delta;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void MoveUp()
        {
            this.treeView.BeginUpdate();
            try
            {
                int index1, index2;
                foreach (TreeNode parent in treeView.Nodes)
                {
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (!child.Checked)
                            continue;
                        if (child.Index == 0)
                            break;
                        if (child == null)
                            return;
                        index1 = child.Index;
                        if (child.PrevNode == null)
                            return;
                        index2 = child.PrevNode.Index;
                        Reverse(index1, index2);
                        // if selected node is one of the checked ones
                        if (child == selectedNode)
                            selectedNode = child.PrevNode;
                    }
                    if (!parent.Checked)
                        continue;
                    if (parent.Index == 0)
                        break;
                    if (parent == null)
                        return;
                    index1 = parent.Index;
                    if (parent.PrevNode == null)
                        return;
                    index2 = parent.PrevNode.Index;
                    Reverse(index1, index2);
                    // if selected node is one of the checked ones
                    if (parent == selectedNode)
                        selectedNode = parent.PrevNode;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
            }
            this.treeView.EndUpdate();
        }
        public void MoveDown()
        {
            this.treeView.BeginUpdate();
            try
            {
                int index1, index2;
                TreeNode parent, child;
                for (int i = treeView.Nodes.Count - 1; i >= 0; i--)
                {
                    parent = treeView.Nodes[i];
                    for (int a = parent.Nodes.Count - 1; a >= 0; a--)
                    {
                        child = parent.Nodes[a];
                        if (!child.Checked)
                            continue;
                        if (child.Index == parent.Nodes.Count - 1)
                            break;
                        if (child == null)
                            return;
                        index1 = child.Index;
                        if (child.NextNode == null)
                            return;
                        index2 = child.NextNode.Index;
                        Reverse(index1, index2);
                        // if selected node is one of the checked ones
                        if (child == selectedNode)
                            selectedNode = child.NextNode;
                    }
                    //
                    if (!parent.Checked)
                        continue;
                    if (parent.Index == treeView.Nodes.Count - 1)
                        break;
                    if (parent == null)
                        return;
                    index1 = parent.Index;
                    if (parent.NextNode == null)
                        return;
                    index2 = parent.NextNode.Index;
                    Reverse(index1, index2);
                    // if selected node is one of the checked ones
                    if (parent == selectedNode)
                        selectedNode = parent.NextNode;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
            }
            this.treeView.EndUpdate();
        }
        private void Reverse(int index1, int index2)
        {
            if (IsRootNode(treeView.SelectedNode))
            {
                if (!actionScript)
                    script.Reverse(index1, index2);
                else
                    action.Reverse(index1, index2);
            }
            else
            {
                int parent = treeView.SelectedNode.Parent.Index;
                EventCommand esc = script.Commands[parent];
                esc.Queue.Reverse(index1, index2);
            }
        }
        public void Copy()
        {
            try
            {
                this.treeView.BeginUpdate();
                EventCommand esc;
                ActionCommand asc;
                TreeNode node = this.treeView.SelectedNode;
                if (node == null)
                    return;
                int index = this.treeView.SelectedNode.Index;
                bool parentChecked = false, childChecked = false;
                commandCopies = new ArrayList();
                foreach (TreeNode parent in treeView.Nodes)
                {
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (!child.Checked)
                            continue;
                        childChecked = true;
                        if (parentChecked)
                        {
                            MessageBox.Show(
                                "Cannot create a copy buffer that contains both event and action\n" +
                                "commands. Please uncheck all action OR event commands.",
                                "LAZYSHELL++");
                            commandCopies = null;
                            return;
                        }
                        asc = (ActionCommand)child.Tag;
                        asc.Assemble();
                        commandCopies.Add(asc.Copy());
                    }
                    if (!parent.Checked)
                        continue;
                    parentChecked = true;
                    if (childChecked)
                    {
                        MessageBox.Show(
                            "Cannot create a copy buffer that contains both event and action\n" +
                            "commands. Please uncheck all action OR event commands.",
                            "LAZYSHELL++");
                        commandCopies = null;
                        return;
                    }
                    if (!actionScript)
                    {
                        esc = (EventCommand)parent.Tag;
                        esc.Assemble();
                        commandCopies.Add(esc.Copy());
                    }
                    else
                    {
                        asc = (ActionCommand)parent.Tag;
                        asc.Assemble();
                        commandCopies.Add(asc.Copy());
                    }
                }
            }
            finally
            {
                this.treeView.Select();
                this.treeView.EndUpdate();
            }
        }
        public void Paste()
        {
            foreach (TreeNode parent in treeView.Nodes)
            {
                if (parent.Checked)
                    parent.Checked = false;
                foreach (TreeNode child in parent.Nodes)
                    if (child.Checked)
                        child.Checked = false;
            }
            TreeNode temp = treeView.SelectedNode;
            // pasting event command in event script
            if (commandCopies != null && !actionScript && (treeView.SelectedNode == null || IsRootNode(treeView.SelectedNode)))
            {
                try
                {
                    foreach (EventCommand copy in commandCopies)
                    {
                        InsertNode(copy.Copy());
                        treeView.SelectedNode = temp;
                    }
                }
                catch
                {
                    if (treeView.SelectedNode != null && treeView.SelectedNode.BackColor == Color.FromArgb(192, 224, 255))
                    {
                        foreach (ActionCommand copy in commandCopies)
                        {
                            InsertNode(copy.Copy());
                            treeView.SelectedNode = temp;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot paste action commands outside of an action queue.", "LAZYSHELL++");
                        return;
                    }
                }
            }
            // pasting action command in event script
            else if (commandCopies != null)
            {
                try
                {
                    foreach (ActionCommand ascCopy in commandCopies)
                    {
                        InsertNode(ascCopy.Copy());
                        treeView.SelectedNode = temp;
                    }
                }
                catch
                {
                    MessageBox.Show("You cannot paste event commands inside of an action queue.", "LAZYSHELL++");
                    return;
                }
            }
            this.treeView.Select();
        }
        /// <summary>
        /// Selects a node in the treeView, tagged with the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SelectNode(EventActionCommand command)
        {
            if (command != null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    if (node.Tag == command)
                    {
                        treeView.SelectedNode = node;
                        return;
                    }
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Tag == command)
                        {
                            treeView.SelectedNode = child;
                            return;
                        }
                    }
                }
            }
        }
        private bool IsRootNode(TreeNode node)
        {
            if (node == null)
                return false;
            return node.Text.CompareTo(node.FullPath) == 0;
        }
        public void ExpandAll()
        {
            this.treeView.ExpandAll();
        }
        public void CollapseAll()
        {
            this.treeView.CollapseAll();
        }
        public void ClearAll()
        {
            this.script.Assemble();
            this.treeView.BeginUpdate();
            if (actionScript)
            {
                this.scriptDelta -= action.Length;
                this.action.Clear();
            }
            else
            {
                this.scriptDelta -= script.Length;
                this.script.Clear();
            }
            RefreshScript();
            this.treeView.EndUpdate();
        }
        //
        public void RefreshScript()
        {
            if (!actionScript)
                script.Refresh();
            else
                action.Refresh();
            Populate();
            //
            if (treeView.Nodes.Count != 0 && selectedNode != null)
            {
                if (selectedNode.Parent != null)
                    selectedNode = treeView.Nodes[selectedNode.Parent.Index].Nodes[selectedNode.Index];
                else
                    selectedNode = treeView.Nodes[selectedNode.Index];
                treeView.SelectedNode = selectedNode;
            }
        }
        /// <summary>
        /// Refreshes the script and sets the selected node to the specified (full) index.
        /// </summary>
        /// <param name="fullIndex">The full index of the node to select.</param>
        public void RefreshScript(int fullIndex)
        {
            if (!actionScript)
                script.Refresh();
            else
                action.Refresh();
            Populate();
            //
            treeView.SelectNode(fullIndex);
        }
        #endregion
    }
}
