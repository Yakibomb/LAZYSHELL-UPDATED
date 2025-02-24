using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class LevelUpsCalculator : NewForm
    {
        private SortedList itemNames { get { return Model.ItemNames; } }
        private SortedList monsterNames { get { return Model.MonsterNames; } }
        private MenuTextPreview menuTextPreview = new MenuTextPreview();
        private FontCharacter[] fontDialogue { get { return Model.FontDialogue; } }
        private FontCharacter[] fontMenu { get { return Model.FontMenu; } }
        private int[] fontPaletteBattle { get { return Model.FontPaletteBattle.Palettes[0]; } }
        private int[] fontPaletteDialogue { get { return Model.FontPaletteDialogue.Palettes[1]; } }
        private Item[] items { get { return Model.Items; } }
        private Attack[] attacks { get { return Model.Attacks; } }
        private Spell[] spells { get { return Model.Spells; } }
        private int index { get { return attackerName.SelectedIndex; } set { attackerName.SelectedIndex = value; } }

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
        // constructor
        public LevelUpsCalculator(int index)
        {
            this.Updating = true;
            InitializeComponent();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            this.attackerWeapon.Items.Clear();
            this.attackerWeapon.Items.AddRange(itemNames.Names);
            this.attackerArmor.Items.Clear();
            this.attackerArmor.Items.AddRange(itemNames.Names);
            this.attackerAccessory.Items.Clear();
            this.attackerAccessory.Items.AddRange(itemNames.Names);
            this.attackerWeapon.SelectedIndex = itemNames.GetSortedIndex(255);
            this.attackerArmor.SelectedIndex = itemNames.GetSortedIndex(255);
            this.attackerAccessory.SelectedIndex = itemNames.GetSortedIndex(255);
            // load entity
            for (int i = 0; i < Model.Characters.Length; i++)
                this.attackerName.Items.Add(new string(Model.Characters[i].Name));
            this.attackerName.SelectedIndex = index;
            this.attackerBonus.SelectedIndex = 0;
            this.Updating = false;
            CalculateSpells();
            this.attackerBonus.SelectedIndex = 0;
        }
        public void Reload(int index)
        {
            this.Updating = true;
            this.attackerName.SelectedIndex = index;
            this.Updating = false;
            CalculateLevel();
        }
        // functions
        public void CalculateLevel()
        {
            this.Updating = true;
            ComboBox bonus;
            NumericUpDown hp_;
            NumericUpDown attack_;
            NumericUpDown defense_;
            NumericUpDown mgAttack_;
            NumericUpDown mgDefense_;
            NumericUpDown level_;
            ComboBox names;

            bonus = attackerBonus;
            hp_ = attackerHP;
            attack_ = attackerAttack;
            defense_ = attackerDefense;
            mgAttack_ = attackerMgAttack;
            mgDefense_ = attackerMgDefense;
            level_ = attackerLevel;
            names = attackerName;

            int hp = 0, attack = 0, defense = 0, mgAttack = 0, mgDefense = 0;

            Character character = Model.Characters[index];
            if (level_.Value == 1)
            {
                hp_.Value = character.StartingCurrentHP;
                attack_.Value = character.StartingAttack;
                defense_.Value = character.StartingDefense;
                mgAttack_.Value = character.StartingMgAttack;
                mgDefense_.Value = character.StartingMgDefense;
            }
            else
            {
                hp = character.StartingCurrentHP;
                attack = character.StartingAttack;
                defense = character.StartingDefense;
                mgAttack = character.StartingMgAttack;
                mgDefense = character.StartingMgDefense;
                foreach (Character.LevelUp level in character.Levels)
                {
                    if (level == null) continue;
                    if (level.Index > level_.Value) break;
                    hp += level.HpPlus;
                    attack += level.AttackPlus;
                    defense += level.DefensePlus;
                    mgAttack += level.MgAttackPlus;
                    mgDefense += level.MgDefensePlus;
                    if (bonus.SelectedIndex == 0)
                    {
                        if (level.AttackPlusBonus > level.MgAttackPlusBonus)
                        {
                            attack += level.AttackPlusBonus;
                            defense += level.DefensePlusBonus;
                        }
                        else if (level.MgAttackPlusBonus > level.AttackPlusBonus)
                        {
                            mgAttack += level.MgAttackPlusBonus;
                            mgDefense += level.MgDefensePlusBonus;
                        }
                        else
                            hp += level.HpPlusBonus;
                    }
                    else if (bonus.SelectedIndex == 1)
                    {
                        hp += level.HpPlusBonus;
                    }
                    else if (bonus.SelectedIndex == 2)
                    {
                        mgAttack += level.MgAttackPlusBonus;
                        mgDefense += level.MgDefensePlusBonus;
                    }
                    else
                    {
                        attack += level.AttackPlusBonus;
                        defense += level.DefensePlusBonus;
                    }
                }
                hp_.Value = hp;
                attack_.Value = attack;
                defense_.Value = defense;
                mgAttack_.Value = mgAttack;
                mgDefense_.Value = mgDefense;
            }

            attack = 0; defense = 0; mgAttack = 0; mgDefense = 0;
            //
            attack += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].Attack;
            attack += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].Attack;
            attack += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].Attack;
            //
            defense += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].Defense;
            defense += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].Defense;
            defense += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].Defense;
            //
            mgAttack += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].MagicAttack;
            mgAttack += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].MagicAttack;
            mgAttack += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].MagicAttack;
            //
            mgDefense += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].MagicDefense;
            mgDefense += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].MagicDefense;
            mgDefense += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].MagicDefense;
            //
            attack_.Value += attack;
            defense_.Value += defense;
            mgAttack_.Value += mgAttack;
            mgDefense_.Value += mgDefense;
            //
            CalculateSpells();
            this.Updating = false;
        }
        private void CalculateSpells()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            Character character = Model.Characters[index];
            for (int i = 0; i < character.StartingMagic.Length; i++)
            {
                if (character.StartingMagic[i] == false) continue;

                ListViewItem item = new ListViewItem(new string[]
                {
                    character.StartingLevel.ToString(),
                    Model.SpellNames.GetUnsortedName(i).Substring(1),
                });
                listViewItems.Add(item);

            }
            for (int i = 0; i < character.Levels.Length; i++)
            {
                if (character.Levels[i] == null) continue;
                if (character.Levels[i].SpellLearned >= 32) continue;
                //
                ListViewItem item = new ListViewItem(new string[]
                {
                    i.ToString(),
                    Model.SpellNames.GetUnsortedName(character.Levels[i].SpellLearned).Substring(1),
                });
                listViewItems.Add(item);
            }
            listView1.Items.AddRange(listViewItems.ToArray());
            listView1.EndUpdate();
        }
        // event handlers
        private void attackerName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                Model.FontDialogue, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
        }
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, true, false, Model.MenuBG_);
        }
        private void loadProperties(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            CalculateLevel();
        }
        private void calculateTotal(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            CalculateLevel();
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Do.SortListView((ListView)sender, lvwColumnSorter, e.Column);
        }
    }
}