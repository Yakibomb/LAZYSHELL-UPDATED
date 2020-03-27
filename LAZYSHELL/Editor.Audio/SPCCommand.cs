using System;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL
{
    [Serializable()]
    public class SPCCommand
    {
        // class variables
        private SPC spc;
        private int channel;
        private byte[] commandData;
        // public accessors
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
            set { this.commandData[1] = value; }
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
            set { this.commandData[2] = value; }
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
            set { this.commandData[3] = value; }
        }
        public int Type
        {
            get
            {
                if (spc.GetType() == typeof(SPCTrack))
                    return 0;
                else
                    return ((SPCSound)spc).Type + 1;
            }
        }
        public int Length { get { return commandData.Length; } }
        public int Channel { get { return this.channel; } set { this.channel = value; } }
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        public List<SPCCommand> Commands { get { return spc.Channels[channel]; } set { spc.Channels[channel] = value; } }
        public int Index { get { return this.Commands.IndexOf(this); } }
        public SPCCommand Prev
        {
            get
            {
                if (Index > 0)
                    return Commands[Index - 1];
                return null;
            }
        }
        public SPCCommand Next
        {
            get
            {
                if (Index < Commands.Count)
                    return Commands[Index + 1];
                return null;
            }
        }
        public Note Note
        {
            get { return new Note(this); }
        }
        // constructor
        public SPCCommand(byte[] commandData, SPC spc, int channel)
        {
            this.spc = spc;
            this.commandData = commandData;
            this.channel = channel;
        }
        // class functions
        public SPCCommand Copy()
        {
            return new SPCCommand(Bits.Copy(commandData), this.spc, this.channel);
        }
        // universal functions
        public override string ToString()
        {
            return Interpreter.Instance.Interpret(this);
        }
    }
}
