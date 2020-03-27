using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public abstract class SPC : Element
    {
        public override abstract int Index { get; set; }
        public abstract List<SPCCommand>[] Channels { get; set; }
        public abstract bool[] ActiveChannels { get; set; }
        public abstract SampleIndex[] Samples { get; set; }
        public abstract List<Percussives> Percussives { get; set; }
        public abstract byte DelayTime { get; set; }
        public abstract byte DecayFactor { get; set; }
        public abstract byte Echo { get; set; }
        public abstract void Assemble();
        public abstract List<Note>[] Notes { get; set; }
        public abstract void CreateNotes();
    }
}
