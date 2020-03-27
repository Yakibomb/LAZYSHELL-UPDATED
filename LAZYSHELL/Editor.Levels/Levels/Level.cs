using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class Level
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables
        private int levelMap;
        private LevelLayer layer;
        private LevelNPCs levelNPCs;
        private LevelExits levelExits;
        private LevelEvents levelEvents;
        private LevelOverlaps levelOverlaps;
        private LevelTileMods levelTileMods;
        private LevelSolidMods levelSolidMods;
        // accessors        
        public int LevelMap { get { return levelMap; } set { levelMap = value; } }
        public LevelLayer Layer { get { return layer; } set { layer = value; } }
        public LevelNPCs LevelNPCs { get { return levelNPCs; } set { levelNPCs = value; } }
        public LevelExits LevelExits { get { return levelExits; } set { levelExits = value; } }
        public LevelEvents LevelEvents { get { return levelEvents; } set { levelEvents = value; } }
        public LevelOverlaps LevelOverlaps { get { return levelOverlaps; } set { levelOverlaps = value; } }
        public LevelTileMods LevelTileMods { get { return levelTileMods; } set { levelTileMods = value; } }
        public LevelSolidMods LevelSolidMods { get { return levelSolidMods; } set { levelSolidMods = value; } }
        // constructor, functions
        public Level(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            levelMap = rom[(index * 18) + 0x1D0040];
            this.layer = new LevelLayer(index);
            this.levelNPCs = new LevelNPCs(index);
            this.levelExits = new LevelExits(index);
            this.levelEvents = new LevelEvents(index);
            this.levelOverlaps = new LevelOverlaps(index);
            this.levelTileMods = new LevelTileMods(index);
            this.levelSolidMods = new LevelSolidMods(index);
        }
        public void Assemble()
        {
            rom[(index * 18) + 0x1D0040] = (byte)levelMap;
            layer.Assemble();
        }
        // universal functions
        public void Clear()
        {
            layer.Clear();
            levelEvents.Clear();
            levelExits.Clear();
            levelNPCs.Clear();
            levelOverlaps.Clear();
            levelTileMods.Clear();
            levelSolidMods.Clear();
        }
    }
}
