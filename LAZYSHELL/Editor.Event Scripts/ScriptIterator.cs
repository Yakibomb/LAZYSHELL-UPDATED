using System;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.ScriptsEditor
{
    class ScriptIterator
    {
        private EventScript script = null;
        private ActionScript action = null;
        private int parentIndex = 0; public int ParentIndex { get { return this.parentIndex; } }
        private int childIndex = -1; public int ChildIndex { get { return this.childIndex; } }
        //
        public ScriptIterator(EventScript script)
        {
            this.script = script;
        }
        public ScriptIterator(ActionScript action)
        {
            this.action = action;
        }
        /* 
         * Returns the next command in the EventScript or ActionScript
         * Will throw an exception if were at an invalid index, so check the IsDone before calling this
         */
        public EventActionCommand Next()
        {
            EventCommand esc;
            ActionCommand asc;
            if (script != null)
            {
                esc = script.Commands[parentIndex];
                if (esc.QueueTrigger && esc.Queue.Commands.Count > 0)
                {
                    if (childIndex < esc.Queue.Commands.Count)
                    {
                        if (childIndex != -1)
                        {
                            asc = esc.Queue.Commands[childIndex++];
                            if (childIndex == esc.Queue.Commands.Count)
                            {
                                childIndex = -1;
                                parentIndex++;
                            }
                            return (EventActionCommand)asc;
                        }
                        childIndex++;
                        return (EventActionCommand)esc;
                    }
                }
                childIndex = -1;
                parentIndex++;
                return (EventActionCommand)esc;
            }
            else if (action != null)
            {
                asc = action.Commands[parentIndex++];
                return (EventActionCommand)asc;
            }
            throw new Exception("Invalid Script");
        }
        public bool IsDone
        {
            get
            {
                if (script != null)
                {
                    if (parentIndex < script.Commands.Count)
                        return false; // if its not the last command, were not done
                    if (script.Commands.Count <= 0)
                        return true;
                    // Get the last command to check child index
                    EventCommand esc = script.Commands[script.Commands.Count - 1];
                    if (esc.QueueTrigger && esc.Queue.Commands.Count > 0)
                    {
                        if (childIndex != -1 && childIndex < esc.Queue.Commands.Count)
                            return false;
                    }
                }
                else if (action != null)
                {
                    if (parentIndex < action.Commands.Count)
                        return false;
                }
                return true;
            }
        }
    }
}
