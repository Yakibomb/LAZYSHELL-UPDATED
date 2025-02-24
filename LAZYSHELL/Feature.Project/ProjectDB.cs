using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    [Serializable()]
    public class ProjectDB
    {
        // project information
        private string title; public string Title { get { return title; } set { title = value; } }
        private string author; public string Author { get { return author; } set { author = value; } }
        private string date; public string Date { get { return date; } set { date = value; } }
        private string webpage; public string Webpage { get { return webpage; } set { webpage = value; } }
        private string description; public string Description { get { return description; } set { description = value; } }
        private string generalNotes; public string OtherInfo { get { return generalNotes; } set { generalNotes = value; } }
        // element notes
        private List<EIndex> actionScripts; public List<EIndex> ActionScripts { get { return actionScripts; } set { actionScripts = value; } }
        private List<EIndex> attacks; public List<EIndex> Attacks { get { return attacks; } set { attacks = value; } }
        private List<EIndex> allies; public List<EIndex> Allies { get { return allies; } set { allies = value; } }
        private List<EIndex> battlefields; public List<EIndex> Battlefields { get { return battlefields; } set { battlefields = value; } }
        private List<EIndex> dialogues; public List<EIndex> Dialogues { get { return dialogues; } set { dialogues = value; } }
        private List<EIndex> effects; public List<EIndex> Effects { get { return effects; } set { effects = value; } }
        private List<EIndex> eventScripts; public List<EIndex> EventScripts { get { return eventScripts; } set { eventScripts = value; } }
        private List<EIndex> formations; public List<EIndex> Formations { get { return formations; } set { formations = value; } }
        private List<EIndex> items; public List<EIndex> Items { get { return items; } set { items = value; } }
        private List<EIndex> levels; public List<EIndex> Levels { get { return levels; } set { levels = value; } }
        private List<EIndex> memoryBits; public List<EIndex> MemoryBits { get { return memoryBits; } }
        private List<EIndex> monsters; public List<EIndex> Monsters { get { return monsters; } set { monsters = value; } }
        private List<EIndex> packs; public List<EIndex> Packs { get { return packs; } set { packs = value; } }
        private List<EIndex> shops; public List<EIndex> Shops { get { return shops; } set { shops = value; } }
        private List<EIndex> spells; public List<EIndex> Spells { get { return spells; } set { spells = value; } }
        private List<EIndex> sprites; public List<EIndex> Sprites { get { return sprites; } set { sprites = value; } }
        private List<EIndex> monsterBehaviorAnims; public List<EIndex> MonsterBehaviorAnims { get { return monsterBehaviorAnims; } set { monsterBehaviorAnims = value; } }
        private List<EIndex> battleEvents; public List<EIndex> BattleEvents { get { return battleEvents; } set { battleEvents = value; } }
        // element lists
        private List<EList> elists; public List<EList> ELists { get { return elists; } set { elists = value; } }
        // keystrokes
        private string[] keystrokes; public string[] Keystrokes { get { return keystrokes; } set { keystrokes = value; } }
        private string[] keystrokesMenu; public string[] KeystrokesMenu { get { return keystrokesMenu; } set { keystrokesMenu = value; } }
        private string[] keystrokesDesc; public string[] KeystrokesDesc { get { return keystrokesDesc; } set { keystrokesDesc = value; } }
        // constructor
        public ProjectDB()
        {
            // project information
            title = "";
            author = "";
            date = "";
            webpage = "";
            description = "";
            generalNotes = "";
            // element notes
            actionScripts = new List<EIndex>();
            attacks = new List<EIndex>();
            allies = new List<EIndex>();
            battlefields = new List<EIndex>();
            dialogues = new List<EIndex>();
            effects = new List<EIndex>();
            eventScripts = new List<EIndex>();
            formations = new List<EIndex>();
            items = new List<EIndex>();
            levels = new List<EIndex>();
            memoryBits = new List<EIndex>();
            monsters = new List<EIndex>();
            packs = new List<EIndex>();
            shops = new List<EIndex>();
            spells = new List<EIndex>();
            sprites = new List<EIndex>();
            battleEvents = new List<EIndex>();
            monsterBehaviorAnims = new List<EIndex>();
            // element lists
            elists = new List<EList>();
            foreach (EList elist in Model.ELists)
                elists.Add(elist.Copy());
            //
            keystrokes = Model.Keystrokes;
            keystrokesMenu = Model.KeystrokesMenu;
            keystrokesDesc = Model.KeystrokesDesc;
        }
        // public functions
        public void AddIndex(int index, List<EIndex> arrayList)
        {
            arrayList.Insert(index, new EIndex());
        }
        public void DeleteIndex(int index, List<EIndex> arrayList)
        {
            arrayList.RemoveAt(index);
        }
        public void SwitchIndex(int index, List<EIndex> arrayList)
        {
            arrayList.Reverse(index, 2);
        }
    }
    [Serializable()]
    public class EIndex
    {
        private int index; public int Index { get { return index; } set { index = value; } }
        private string label; public string Label { get { return label; } set { label = value; } }
        private string description; public string Description { get { return description; } set { description = value; } }
        private int address; public int Address { get { return address; } set { address = value; } }
        private int addressBit; public int AddressBit { get { return addressBit; } set { addressBit = value; } }
        public EIndex()
        {
            this.index = 0;
            this.label = "(no label)";
            this.description = "(no description)";
            this.address = 0x7000;
            this.addressBit = 0;
        }
        public EIndex(string label, int index)
        {
            this.index = index;
            this.label = label;
            this.description = "(no description)";
            this.address = 0x7000;
            this.addressBit = 0;
        }
        public EIndex(NotesDB.Index index)
        {
            this.index = index.IndexNumber;
            this.label = index.IndexLabel;
            this.description = index.IndexDescription;
            this.address = index.Address;
            this.addressBit = index.AddressBit;
        }
        public override string ToString()
        {
            return label;
        }
    }
    [Serializable()]
    public class EList
    {
        public string Name;
        public string[] Labels
        {
            get
            {
                string[] labels = new string[Indexes.Length];
                for (int i = 0; i < labels.Length; i++)
                    labels[i] = Indexes[i].Label;
                return labels;
            }
        }
        public EIndex[] Indexes;
        public EList(string name, string[] labels)
        {
            Name = name;
            Indexes = new EIndex[labels.Length];
            for (int i = 0; i < labels.Length; i++)
                Indexes[i] = new EIndex(labels[i], i);
        }
        public EList Copy()
        {
            return new EList(Name, Lists.Copy(Labels));
        }
        public void Reset()
        {
            EList source = Model.ELists.Find(delegate(EList list)
            {
                return list.Name == Name;
            });
            if (source == null)
                return;
            for (int i = 0; i < source.Indexes.Length && i < Indexes.Length; i++)
            {
                Indexes[i].Address = source.Indexes[i].Address;
                Indexes[i].AddressBit = source.Indexes[i].AddressBit;
                Indexes[i].Description = source.Indexes[i].Description;
                Indexes[i].Label = source.Indexes[i].Label;
                Indexes[i].Index = source.Indexes[i].Index;
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
