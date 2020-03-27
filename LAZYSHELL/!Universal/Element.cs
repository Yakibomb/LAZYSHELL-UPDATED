using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public abstract class Element
    {
        public abstract int Index { get; set; }
        public abstract void Clear();
    }
}
