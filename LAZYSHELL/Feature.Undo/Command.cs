using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.Undo
{
    interface Command
    {
        bool AutoRedo();
        void Execute();
    }
}
