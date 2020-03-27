using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.ScriptsEditor;
using LAZYSHELL.ScriptsEditor.Commands;

namespace LAZYSHELL.Undo
{
    class AnimationEdit : Command
    {
        private AnimationScripts form;
        private AnimationCommand asc;
        private int offset;
        private byte[] data;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asc">The command being modified.</param>
        /// <param name="offset">The offset of the data.</param>
        /// <param name="internalOffset">The internal offset of the command.</param>
        /// <param name="data">The ROM data.</param>
        public AnimationEdit(AnimationScripts form, AnimationCommand asc, int offset, byte[] data)
        {
            this.form = form;
            this.asc = asc;
            this.offset = offset;
            this.data = data;
            Execute(true);
        }
        public void Execute()
        {
            Execute(false);
        }
        public void Execute(bool push)
        {
            for (int i = 0; i < data.Length; i++)
            {
                byte temp = Model.ROM[offset + i];
                Model.ROM[offset + i] = data[i];
                data[i] = temp;
            }
            if (!push)
            {
                AnimationCommand temp = this.asc;
                this.asc = this.form.ASC;
                this.form.ASC = temp;
            }
        }
    }
    class GraphicEdit : Command
    {
        private byte[] changes;
        private byte[] graphics;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        //
        public GraphicEdit(byte[] graphics, byte[] changes)
        {
            this.graphics = graphics;
            this.changes = changes;
        }
        public void Execute()
        {
            byte[] temp = Bits.Copy(graphics);
            changes.CopyTo(graphics, 0);
            temp.CopyTo(changes, 0);
        }
    }
    public enum ScoreEdit
    {
        InsertNote,
        EraseNote,
        PasteNotes,
        DeleteNotes,
        AddStaff,
        DeleteStaff
    }
    class ScoreEditCommand : Command
    {
        // class variables and accessors
        private object collection;
        private ScoreEdit scoreEdit;
        private int index;
        private object itemA;
        private object itemB;
        private object itemC;
        private object items;
        private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
        // constructor
        public ScoreEditCommand(ScoreEdit scoreEdit, object collection, int index, object item)
        {
            this.collection = collection;
            this.scoreEdit = scoreEdit;
            this.index = index;
            this.itemB = item;
            Execute();
        }
        public ScoreEditCommand(ScoreEdit scoreEdit, object collection, object items, int index)
        {
            this.collection = collection;
            this.scoreEdit = scoreEdit;
            this.index = index;
            this.items = items;
            Execute();
        }
        /// <summary>
        /// If inserting a note at a different octave
        /// </summary>
        /// <param name="scoreEdit"></param>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="itemA"></param>
        /// <param name="itemB"></param>
        public ScoreEditCommand(ScoreEdit scoreEdit, object collection, int index, object itemA, object itemB, object itemC)
        {
            this.collection = collection;
            this.scoreEdit = scoreEdit;
            this.index = index;
            this.itemA = itemA;
            this.itemB = itemB;
            this.itemC = itemC;
            Execute();
        }
        // execute
        public void Execute()
        {
            if (scoreEdit == ScoreEdit.EraseNote)
            {
                scoreEdit = ScoreEdit.InsertNote;
                try
                {
                    ((List<object>)collection).Remove((Note)itemB);
                }
                catch
                {
                    if (itemA != null)
                        ((List<SPCCommand>)collection).Remove((SPCCommand)itemA);
                    ((List<SPCCommand>)collection).Remove((SPCCommand)itemB);
                    if (itemC != null)
                        ((List<SPCCommand>)collection).Remove((SPCCommand)itemC);
                }
            }
            else if (scoreEdit == ScoreEdit.InsertNote)
            {
                scoreEdit = ScoreEdit.EraseNote;
                try
                {
                    ((List<object>)collection).Insert(index, (Note)itemB);
                }
                catch
                {
                    if (itemC != null)
                        ((List<SPCCommand>)collection).Insert(index, (SPCCommand)itemC);
                    ((List<SPCCommand>)collection).Insert(index, (SPCCommand)itemB);
                    if (itemA != null)
                        ((List<SPCCommand>)collection).Insert(index, (SPCCommand)itemA);
                }
            }
            else if (scoreEdit == ScoreEdit.PasteNotes)
            {
                scoreEdit = ScoreEdit.DeleteNotes;
                try
                {
                    ((List<object>)collection).InsertRange(index, (List<object>)items);
                }
                catch
                {
                    ((List<SPCCommand>)collection).InsertRange(index, (List<SPCCommand>)items);
                }
            }
            else if (scoreEdit == ScoreEdit.DeleteNotes)
            {
                scoreEdit = ScoreEdit.PasteNotes;
                try
                {
                    ((List<object>)collection).RemoveRange(index, ((List<object>)items).Count);
                }
                catch
                {
                    ((List<SPCCommand>)collection).RemoveRange(index, ((List<SPCCommand>)items).Count);
                }
            }
            else if (scoreEdit == ScoreEdit.AddStaff)
            {
                scoreEdit = ScoreEdit.DeleteStaff;
                ((List<Staff>)collection).Insert(index, (Staff)itemB);
            }
            else if (scoreEdit == ScoreEdit.DeleteStaff)
            {
                scoreEdit = ScoreEdit.AddStaff;
                ((List<Staff>)collection).Remove((Staff)itemB);
            }
        }
    }
    class SpriteEdit : Command
    {
        private List<Mold> molds;
        private ListBox listbox;
        private Mold moldA;
        private Mold moldB;
        private int index;
        private int indexB;
        private SpriteAction action;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        public SpriteEdit(SpriteAction action, List<Mold> molds, ListBox listbox, Mold moldA, Mold moldB, int index, int indexB)
        {
            this.action = action;
            this.molds = molds;
            this.listbox = listbox;
            this.moldA = moldA.Copy();
            this.moldB = moldB.Copy();
            this.index = index;
            this.indexB = indexB;
        }
        public void Execute()
        {
            if (action == SpriteAction.Edit)
            {
                this.molds[index] = this.moldB.Copy();
                Mold temp = this.moldA.Copy();
                this.moldA = this.moldB.Copy();
                this.moldB = temp;
                this.listbox.SelectedIndex = this.index;
            }
            else if (action == SpriteAction.Create)
            {
                this.molds.RemoveAt(index);
                this.listbox.Items.RemoveAt(index);
                this.listbox.SelectedIndex = Math.Min(this.index, this.listbox.Items.Count - 1);
                this.action = SpriteAction.Delete;
            }
            else if (action == SpriteAction.Delete)
            {
                this.molds.Insert(index, moldB.Copy());
                this.listbox.Items.Insert(index, "Mold " + index);
                this.listbox.SelectedIndex = Math.Min(this.index, this.listbox.Items.Count - 1);
                this.action = SpriteAction.Create;
            }
            else if (action == SpriteAction.MoveDown)
            {
                this.molds.Reverse(index, 2);
                this.listbox.SelectedIndex = index;
                this.action = SpriteAction.MoveUp;
            }
            else if (action == SpriteAction.MoveUp)
            {
                this.molds.Reverse(index, 2);
                this.listbox.SelectedIndex = index + 1;
                this.action = SpriteAction.MoveDown;
            }
            else if (action == SpriteAction.IndexChange)
            {
                this.listbox.SelectedIndex = this.indexB;
                int index = this.index;
                this.index = this.indexB;
                this.indexB = index;
            }
            //
        }
    }
    enum SpriteAction
    {
        Edit, MoveUp, MoveDown, Delete, Create, IndexChange
    }
    class TilemapCommand : Command
    {
        private byte[] src;
        private Size srcSize;
        private byte[] changes;
        private Point location;
        private Size size;
        private bool autoRedo = false;
        public bool AutoRedo() { return this.autoRedo; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">The source array to modify.</param>
        /// <param name="srcWidth">The width, in tiles, of the source array.</param>
        /// <param name="srcHeight">The height, in tiles, of the source array.</param>
        /// <param name="changes">The changes to apply to the source array.</param>
        /// <param name="x">The X location, in tiles, where the changes will be applied.</param>
        /// <param name="y">The Y location, in tiles, where the changes will be applied.</param>
        /// <param name="width">The width, in tiles, of the changes.</param>
        /// <param name="height">The height, in tiles, of the changes.</param>
        public TilemapCommand(byte[] src, int srcWidth, int srcHeight, byte[] changes, int x, int y, int width, int height)
        {
            this.src = src;
            this.changes = new byte[changes.Length];
            changes.CopyTo(this.changes, 0);
            this.srcSize = new Size(srcWidth, srcHeight);
            this.size = new Size(width, height);
            this.location = new Point(x, y);
            Execute();
        }
        public void Execute()
        {
            for (int y = location.Y, y_ = 0; y < location.Y + size.Height && y < 16; y++, y_++)
            {
                for (int x = location.X, x_ = 0; x < location.X + size.Width && x < 16; x++, x_++)
                {
                    if (x < 0 || y < 0 || x_ < 0 || y_ < 0) continue;
                    byte temp = src[y * srcSize.Width + x];
                    src[y * srcSize.Width + x] = changes[y_ * size.Width + x_];
                    changes[y_ * size.Width + x_] = temp;
                }
            }
        }
    }
    class TilesetCommand : Command
    {
        private byte[] oldTileset;
        private Tileset tileset;
        private byte[] graphics;
        private int index;
        private byte format;
        private Battlefields form;
        private BattlefieldTileset battlefieldTileset;
        private System.Windows.Forms.ToolStripComboBox name;
        private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
        //
        public TilesetCommand(Tileset tileset, byte[] oldTileset, byte[] graphics,
            byte format, System.Windows.Forms.ToolStripComboBox name)
        {
            this.tileset = tileset;
            this.oldTileset = oldTileset;
            this.graphics = graphics;
            this.format = format;
            this.name = name;
            if (name != null)
                this.index = (int)name.SelectedIndex;
        }
        public TilesetCommand(BattlefieldTileset battlefieldTileset, byte[] oldTileset, Battlefields form)
        {
            this.battlefieldTileset = battlefieldTileset;
            this.oldTileset = oldTileset;
            this.form = form;
            this.index = form.Index;
        }
        //
        public void Execute()
        {
            if (tileset != null)
            {
                byte[] temp = Bits.Copy(tileset.Tileset_bytes);
                oldTileset.CopyTo(tileset.Tileset_bytes, 0);
                tileset.DrawTileset(tileset.Tileset_bytes, tileset.Tileset_tiles, graphics, format);
                oldTileset = temp;
                if (name != null)
                    name.SelectedIndex = index;
            }
            else if (battlefieldTileset != null)
            {
                byte[] temp = Bits.Copy(battlefieldTileset.Tileset_bytes);
                oldTileset.CopyTo(battlefieldTileset.Tileset_bytes, 0);
                battlefieldTileset.DrawTileset(battlefieldTileset.Tileset_bytes, battlefieldTileset.Tileset_tiles);
                oldTileset = temp;
                form.Index = index;
            }
        }
    }
}
