using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    public class A_TreeViewWrapper
    {
        // class variables
        private NewTreeView treeView; public NewTreeView TreeView { get { return treeView; } set { treeView = value; } }
        private AnimationScript script; public AnimationScript Script { get { return script; } set { script = value; } }
        private TreeNode selectedNode; public TreeNode SelectedNode { get { return selectedNode; } set { selectedNode = value; } }
        // constructor
        public A_TreeViewWrapper(NewTreeView control)
        {
            this.treeView = control;
        }
        // class functions
        public void ChangeScript(AnimationScript script, bool update)
        {
            this.script = script;
            Populate(update);
        }
        public void ChangeScript(AnimationScript script)
        {
            ChangeScript(script, true);
        }
        private void Populate(bool update)
        {
            if (update)
                this.treeView.BeginUpdate();
            this.treeView.Nodes.Clear();
            //
            List<AnimationCommand> commands = script.Commands;
            for (int i = 0; i < commands.Count; i++)
                AddCommand(commands[i]);
            //
            this.treeView.ExpandAll();
            if (update)
                this.treeView.EndUpdate();
        }
        // treeview managers
        private void AddCommand(AnimationCommand command)
        {
            // Add node
            this.treeView.Nodes.Add("[" + (command.Offset).ToString("X6") + "]   " + command.ToString());
            TreeNode node = treeView.Nodes[treeView.Nodes.Count - 1];
            node.Tag = command;
            //
            switch (command.Opcode)
            {
                case 0x07:
                case 0x11:
                case 0x5E:
                    node.BackColor = Color.FromArgb(255, 255, 224, 224);
                    break;
                case 0x09:
                case 0x10:
                case 0x38:
                case 0x47:
                case 0x50:
                case 0x51:
                case 0x5D:
                case 0x64:
                case 0x68:
                case 0xA2:
                case 0xCE:
                case 0xCF:
                case 0xD0:
                case 0xD8:
                    node.BackColor = Color.FromArgb(255, 192, 224, 255);
                    AddNode(command, node);
                    break;
                case 0x04:
                case 0x0A:
                case 0x40:
                case 0x41:
                case 0x4E:
                case 0x74:
                case 0x75:
                case 0x7B:
                case 0x97:
                    node.BackColor = Color.FromArgb(255, 255, 255, 160);
                    AddNode(command, node);
                    break;
                default:
                    if (command.Opcode >= 0x24 && command.Opcode <= 0x2B)
                    {
                        node.BackColor = Color.FromArgb(255, 192, 224, 255);
                        AddNode(command, node);
                    }
                    break;
            }
        }
        private void AddNode(AnimationCommand command, TreeNode node)
        {
            TreeNode childNode;
            foreach (AnimationCommand childCommand in command.Commands)
            {
                node.Nodes.Add("[" + (childCommand.Offset).ToString("X6") + "]   " + childCommand.ToString());
                childNode = node.Nodes[node.Nodes.Count - 1];
                childNode.Tag = childCommand;
                //
                switch (childCommand.Opcode)
                {
                    case 0x07:
                    case 0x11:
                    case 0x5E:
                        childNode.BackColor = Color.FromArgb(255, 255, 224, 224);
                        break;
                    case 0x09:
                    case 0x10:
                    case 0x38:
                    case 0x47:
                    case 0x50:
                    case 0x51:
                    case 0x5D:
                    case 0x64:
                    case 0x68:
                    case 0xA2:
                    case 0xCE:
                    case 0xCF:
                    case 0xD0:
                    case 0xD8:
                        childNode.BackColor = Color.FromArgb(255, 192, 224, 255);
                        AddNode(childCommand, childNode);
                        break;
                    case 0x04:
                    case 0x0A:
                    case 0x40:
                    case 0x41:
                    case 0x4E:
                    case 0x74:
                    case 0x75:
                    case 0x7B:
                    case 0x97:
                        childNode.BackColor = Color.FromArgb(255, 255, 255, 160);
                        AddNode(childCommand, childNode);
                        break;
                    default:
                        if (childCommand.Opcode >= 0x24 && childCommand.Opcode <= 0x2B)
                        {
                            childNode.BackColor = Color.FromArgb(255, 192, 224, 255);
                            AddNode(childCommand, childNode);
                        }
                        break;
                }
            }
        }
        public byte[] MoveUp(AnimationCommand command, ref int topOffset)
        {
            if (selectedNode.PrevNode == null)
                return null;
            if (command.Opcode == 0x07 ||
                command.Opcode == 0x11 ||
                command.Opcode == 0x5E)
                return null;
            AnimationCommand prevCommand = (AnimationCommand)selectedNode.PrevNode.Tag;
            if (prevCommand.Opcode == 0x07 ||
                prevCommand.Opcode == 0x11 ||
                prevCommand.Opcode == 0x5E)
                return null;
            if (selectedNode.Index == 0)
                return null;
            return Reverse(prevCommand, command, ref topOffset, 1);
        }
        public byte[] MoveDown(AnimationCommand command, ref int topOffset)
        {
            if (selectedNode.NextNode == null)
                return null;
            if (command.Opcode == 0x07 ||
                command.Opcode == 0x11 ||
                command.Opcode == 0x5E)
                return null;
            AnimationCommand nextCommand = (AnimationCommand)selectedNode.NextNode.Tag;
            if (nextCommand.Opcode == 0x07 ||
                nextCommand.Opcode == 0x11 ||
                nextCommand.Opcode == 0x5E)
                return null;
            if (selectedNode.Parent != null)
            {
                if (selectedNode.Index >= selectedNode.Parent.Nodes.Count - 1)
                    return null;
            }
            else if (selectedNode.Index >= treeView.Nodes.Count - 1)
                return null;
            return Reverse(command, nextCommand, ref topOffset, 0);
        }
        private byte[] Reverse(AnimationCommand commandA, AnimationCommand commandB, ref int topOffset, int selectedIndex)
        {
            byte[] byteA = Bits.Copy(commandB.CommandData);
            byte[] byteB = Bits.Copy(commandA.CommandData);
            commandA.CommandData = byteA;
            commandB.CommandData = byteB;
            //
            int offset = commandA.InternalOffset;
            topOffset = commandA.InternalOffset;
            byte[] changes = new byte[commandA.Length + commandB.Length];
            for (int i = 0; i < byteA.Length; i++, offset++)
                changes[i] = commandA.CommandData[i];
            int temp = commandB.InternalOffset;
            commandB.InternalOffset = commandA.InternalOffset;
            commandA.InternalOffset = offset;
            int index = byteA.Length;
            for (int i = 0; i < byteB.Length; i++, offset++)
                changes[index++] = commandB.CommandData[i];
            commandA.Offset = commandA.OriginalOffset = commandA.InternalOffset;
            commandB.Offset = commandB.OriginalOffset = commandB.InternalOffset;
            // check multiple instances of command in current script, and change each accordingly
            return changes;
        }
        public void ExpandAll()
        {
            this.treeView.ExpandAll();
        }
        public void CollapseAll()
        {
            this.treeView.CollapseAll();
        }
        /// <summary>
        /// Selects a node in the treeView with a specified internalOffset.
        /// </summary>
        /// <param name="internalOffset">The internal offset of the node's command.</param>
        /// <param name="index">The full parent index of the node.</param>
        public void SelectNode(int internalOffset, int fullParentIndex)
        {
            selectedNode = null;
            int parentIndex = -1; // root has no parent
            int index = 0; // the current full index
            foreach (TreeNode node in this.treeView.Nodes)
            {
                AnimationCommand asc = (AnimationCommand)node.Tag;
                if (internalOffset == asc.InternalOffset && parentIndex == fullParentIndex)
                {
                    selectedNode = node;
                    break;
                }
                else
                    SelectChildNode(node, internalOffset, fullParentIndex, ref index);
                if (selectedNode != null)
                    break;
            }
            this.treeView.SelectedNode = selectedNode;
        }
        private void SelectChildNode(TreeNode parent, int internalOffset, int fullParentIndex, ref int index)
        {
            int parentIndex = parent.Nodes.Count > 0 ? index++ : index;
            foreach (TreeNode child in parent.Nodes)
            {
                AnimationCommand asc = (AnimationCommand)child.Tag;
                if (internalOffset == asc.InternalOffset && parentIndex == fullParentIndex)
                {
                    selectedNode = child;
                    break;
                }
                else
                    SelectChildNode(child, internalOffset, fullParentIndex, ref index);
                if (selectedNode != null)
                    break;
            }
        }
    }
}
