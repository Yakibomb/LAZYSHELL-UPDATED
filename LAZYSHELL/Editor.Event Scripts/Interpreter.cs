using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        static Interpreter instance = null; // Our instance
        static readonly object padlock = new object(); // Ensures only one instance of this object
        Interpreter()
        {
        }
        public static Interpreter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Interpreter();
                    }
                    return instance;
                }
            }
        }
    }
}
