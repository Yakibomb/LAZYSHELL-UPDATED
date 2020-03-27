using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class NotesDB
    {
        // class variables and accessors
        private string generalNotes; public string GeneralNotes { get { return generalNotes; } set { generalNotes = value; } }
        //
        private ArrayList levels; public ArrayList Levels { get { return levels; } set { levels = value; } }
        private ArrayList eventScripts; public ArrayList EventScripts { get { return eventScripts; } set { eventScripts = value; } }
        private ArrayList actionScripts; public ArrayList ActionScripts { get { return actionScripts; } set { actionScripts = value; } }
        private ArrayList battleScripts; public ArrayList BattleScripts { get { return battleScripts; } set { battleScripts = value; } }
        private ArrayList sprites; public ArrayList Sprites { get { return sprites; } set { sprites = value; } }
        private ArrayList effects; public ArrayList Effects { get { return effects; } set { effects = value; } }
        private ArrayList dialogues; public ArrayList Dialogues { get { return dialogues; } set { dialogues = value; } }
        private ArrayList monsters; public ArrayList Monsters { get { return monsters; } set { monsters = value; } }
        private ArrayList formations; public ArrayList Formations { get { return formations; } set { formations = value; } }
        private ArrayList packs; public ArrayList Packs { get { return packs; } set { packs = value; } }
        private ArrayList spells; public ArrayList Spells { get { return spells; } set { spells = value; } }
        private ArrayList attacks; public ArrayList Attacks { get { return attacks; } set { attacks = value; } }
        private ArrayList items; public ArrayList Items { get { return items; } set { items = value; } }
        private ArrayList shops; public ArrayList Shops { get { return shops; } set { shops = value; } }
        //
        private ArrayList memoryBits; public ArrayList MemoryBits { get { return memoryBits; } }
        // constructor
        public NotesDB()
        {
            generalNotes = "";
            levels = new ArrayList();
            eventScripts = new ArrayList();
            actionScripts = new ArrayList();
            battleScripts = new ArrayList();
            memoryBits = new ArrayList();
            sprites = new ArrayList();
            effects = new ArrayList();
            dialogues = new ArrayList();
            monsters = new ArrayList();
            formations = new ArrayList();
            packs = new ArrayList();
            spells = new ArrayList();
            attacks = new ArrayList();
            items = new ArrayList();
            shops = new ArrayList();
        }
        // public functions
        public void AddIndex(int index, ArrayList arrayList)
        {
            arrayList.Insert(index, new Index());
        }
        public void DeleteIndex(int index, ArrayList arrayList)
        {
            arrayList.RemoveAt(index);
        }
        public void SwitchIndex(int index, ArrayList arrayList)
        {
            arrayList.Reverse(index, 2);
        }
        [Serializable()]
        public class Index
        {
            private int indexNumber; public int IndexNumber { get { return indexNumber; } set { indexNumber = value; } }
            private string indexLabel; public string IndexLabel { get { return indexLabel; } set { indexLabel = value; } }
            private string indexDescription; public string IndexDescription { get { return indexDescription; } set { indexDescription = value; } }
            private int address; public int Address { get { return address; } set { address = value; } }
            private int addressBit; public int AddressBit { get { return addressBit; } set { addressBit = value; } }
            public Index()
            {
                indexNumber = 0;
                indexLabel = "(no label)";
                indexDescription = "(no description)";
                address = 0x7000;
                addressBit = 0;
            }
        }
    }
}
