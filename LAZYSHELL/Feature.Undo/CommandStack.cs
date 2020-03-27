using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LAZYSHELL.Undo
{
    class CommandStack
    {
        // class variables and accessors
        private Command[] commands;
        private Stack<Command> redoStack;
        private Stack<int> undoCount;
        private Stack<int> redoCount;
        private int index = 0;
        private int undo = 0;
        private bool undoing = false;
        private bool redoing = false;
        private bool manualCount = false;
        /// <summary>
        /// Create a new command stack.
        /// </summary>
        /// <param name="manualCount">Sets whether to manually accept execution counts or automatically assign single instances.</param>
        public CommandStack(bool manualCount)
        {
            commands = new Command[65535];
            redoStack = new Stack<Command>();
            undoCount = new Stack<int>();
            redoCount = new Stack<int>();
            this.manualCount = manualCount;
        }
        public CommandStack()
        {
            commands = new Command[65535];
            redoStack = new Stack<Command>();
            undoCount = new Stack<int>();
            redoCount = new Stack<int>();
        }
        // accessor functions
        public void SetTilemaps(Tilemap tilemap)
        {
            foreach (TilemapEdit command in commands)
                if (command != null)
                    command.Tilemap = tilemap;
        }
        public void SetSolidityMaps(Tilemap tilemap)
        {
            foreach (SolidityEdit command in commands)
                if (command != null)
                    command.Tilemap = tilemap;
        }
        // public functions
        public bool UndoCommand()
        {
            if (this.undoCount.Count <= 0 ||
                commands[index] == null ||
                commands.Length < 1 ||
                undo <= 0)
                return false;
            //
            undoing = true;
            int undoCount = this.undoCount.Pop();
            int redoCount = 0;
            for (; undoCount > 0; undoCount--, redoCount++)
            {
                if (index > 0 && commands[index] != null) // not going to wrap
                {
                    commands[index].Execute();
                    if (!commands[index].AutoRedo())
                        redoStack.Push(commands[index]);
                    index--;
                    undo--;
                }
                else if (index == 0 && commands[index] != null) // wrap
                {
                    commands[index].Execute();
                    if (!commands[index].AutoRedo())
                        redoStack.Push(commands[index]);
                    index = commands.Length - 1;
                    undo--;
                }
            }
            this.redoCount.Push(redoCount);
            undoing = false;
            //
            return true;
        }
        public bool RedoCommand()
        {
            if (this.redoCount.Count <= 0 ||
                redoStack.Count == 0)
                return false;
            //
            redoing = true;
            int redoCount = this.redoCount.Pop();
            int undoCount = 0;
            for (; redoCount > 0; redoCount--, undoCount++)
            {
                if (redoStack.Count > 0)
                {
                    Command cmd = redoStack.Pop();
                    cmd.Execute();
                    if (!cmd.AutoRedo())
                        Push(cmd);
                }
            }
            this.undoCount.Push(undoCount);
            redoing = false;
            //
            return true;
        }
        public void Push(Command cmd)
        {
            if (commands.Length <= 0)
                return;
            if (undoing)
            {
                redoStack.Push(cmd);
                return;
            }
            if (redoStack.Count != 0 && !redoing)
                redoStack.Clear();
            //
            index++;
            if (index < commands.Length)
            {
                commands[index] = cmd;
                if (undo < commands.Length)
                    undo++;
            }
            else if (index >= commands.Length)
            {
                // We have filled the whole array and are now overwriting the old commands
                index = 0;
                commands[index] = cmd;
                if (undo < commands.Length)
                    undo++;
            }
            if (!manualCount)
                undoCount.Push(1);
        }
        public void Push(int count)
        {
            if (commands.Length <= 0)
                return;
            if (undoing)
            {
                redoCount.Push(count);
                return;
            }
            if (redoCount.Count != 0 && !redoing)
                redoCount.Clear();
            //
            undoCount.Push(count);
        }
        public void Clear()
        {
            for (int i = 0; i < commands.Length; i++)
                commands[i] = null;
            redoStack.Clear();
            undoCount.Clear();
            redoCount.Clear();
            //
            index = 0;
            undo = 0;
        }
    }
}
