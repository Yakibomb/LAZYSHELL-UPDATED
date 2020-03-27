using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    [Serializable()]
    public class BattleCommand
    {
        // variables
        public bool Locked = true;
        protected bool modified; public bool Modified { get { return this.modified; } set { this.modified = value; } }
        protected bool valid = false; public bool Valid { get { return this.valid; } set { valid = value; } }
        public int Length { get { return commandData.Length; } }
        public byte Opcode
        {
            get
            {
                if (this.Length > 0)
                    return this.commandData[0];
                else
                    return 0;
            }
            set { this.commandData[0] = value; }
        }
        public byte Param1
        {
            get
            {
                if (this.Length > 1)
                    return this.commandData[1];
                else
                    return 0;
            }
            set
            {
                if (this.Length > 1)
                    this.commandData[1] = value;
            }
        }
        public byte Param2
        {
            get
            {
                if (this.Length > 2)
                    return this.commandData[2];
                else
                    return 0;
            }
            set
            {
                if (this.Length > 2)
                    this.commandData[2] = value;
            }
        }
        public byte Param3
        {
            get
            {
                if (this.Length > 3)
                    return this.commandData[3];
                else
                    return 0;
            }
            set
            {
                if (this.Length > 3)
                    this.commandData[3] = value;
            }
        }
        protected byte[] commandData; public byte[] CommandData { get { return commandData; } set { commandData = value; } }
        // constructor
        public BattleCommand(byte[] commandData)
        {
            this.commandData = commandData;
        }
        // functions
        public TreeNode Node
        {
            get
            {
                TreeNode node = new TreeNode(ToString());
                if (Opcode == 0xFD || Opcode == 0xFE)
                    node.BackColor = Color.FromArgb(255, 255, 160);
                else if (Opcode == 0xFF)
                    node.BackColor = Color.FromArgb(160, 255, 160);
                node.ForeColor = modified ? Color.Red : SystemColors.ControlText;
                node.Checked = modified;
                node.Tag = this;
                return node;
            }
        }
        public BattleCommand Copy()
        {
            return new BattleCommand(Bits.Copy(commandData));
        }
        public override string ToString()
        {
            return Interpreter.Instance.InterpretCommand(this);
        }
    }
}
