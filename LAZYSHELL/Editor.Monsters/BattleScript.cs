using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL.ScriptsEditor
{
    [Serializable()]
    public class BattleScript : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private byte[] script; public byte[] Script { get { return script; } set { script = value; } }
        private List<BattleCommand> commands;
        public List<BattleCommand> Commands { get { return commands; } set { commands = value; } }
        public int Length { get { return script.Length; } }
        // constructor
        public BattleScript(int index)
        {
            this.index = index;
            Disassemble(index);
        }
        // assemblers
        private void Disassemble(int index)
        {
            int bank = 0x390000;
            int offset = Bits.GetShort(rom, bank + 0x30AA + (index * 2));
            int length = GetLength(bank + offset);
            this.script = Bits.GetBytes(rom, bank + offset, length);
            //
            ParseScript();
        }
        public void Assemble(ref int offset)
        {
            Bits.SetBytes(rom, offset, script);
            offset += script.Length;
        }
        // class functions and accessors
        public void ParseScript()
        {
            int offset = 0, length = 0;
            if (script.Length > 0 && this.commands == null)
                this.commands = new List<BattleCommand>();
            //
            BattleCommand bsc;
            while (offset < script.Length)
            {
                length = Lists.BattleLengths[script[offset]];
                bsc = new BattleCommand(Bits.GetBytes(script, offset, length));
                commands.Add(bsc);
                offset += length;
            }
        }
        private int GetLength(int offset)
        {
            int totalLength = 0;
            bool endAll = false;
            bool endIf = false;
            while (!endAll)
            {
                byte opcode = rom[offset];
                if (opcode == 0xFF)
                {
                    if (!endIf)
                        endIf = true;
                    else
                        endAll = true;
                }
                int length = Lists.BattleLengths[opcode];
                totalLength += length;
                offset += length;
            }
            return totalLength;
        }
        public void Add(BattleCommand bsc)
        {
            this.commands.Add(bsc);
        }
        public void Insert(int index, BattleCommand bsc)
        {
            this.commands.Insert(index, bsc);
        }
        public void RemoveAt(int index)
        {
            this.commands.RemoveAt(index);
        }
        public void Reverse(int index, int count)
        {
            this.commands.Reverse(index, count);
        }
        // universal functions
        public override void Clear()
        {
            this.commands.Clear();
            BattleCommand bsc = new BattleCommand(new byte[] { 0xFF });
            this.commands.Add(bsc);
            this.commands.Add(bsc);
            script = new byte[2] { 0xFF, 0xFF };
        }
    }
}