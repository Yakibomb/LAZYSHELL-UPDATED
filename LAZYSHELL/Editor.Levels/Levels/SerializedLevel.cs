using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    class SerializedLevel
    {
        public LevelMap LevelMap;
        public int LevelMapNum;
        public byte[] TilesetL1;
        public byte[] TilesetL2;
        public byte[] TilesetL3;
        public byte[] TilemapL1;
        public byte[] TilemapL2;
        public byte[] TilemapL3;
        public byte[] SolidityMap;
        public LevelLayer LevelLayer;
        public LevelNPCs LevelNPCs;
        public LevelExits LevelExits;
        public LevelEvents LevelEvents;
        public LevelOverlaps LevelOverlaps;
        public SerializedLevel()
        {
        }
    }
}
