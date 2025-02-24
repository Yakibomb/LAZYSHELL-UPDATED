using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class DamageCalculator : NewForm
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
        private Monster[] monsters { get { return Model.Monsters; } }
                private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
        // constructor
        public DamageCalculator()
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
            this.targetWeapon.Items.Clear();
            this.targetWeapon.Items.AddRange(itemNames.Names);
            this.targetArmor.Items.Clear();
            this.targetArmor.Items.AddRange(itemNames.Names);
            this.targetAccessory.Items.Clear();
            this.targetAccessory.Items.AddRange(itemNames.Names);
            this.attackerWeapon.SelectedIndex = itemNames.GetSortedIndex(255);
            this.attackerArmor.SelectedIndex = itemNames.GetSortedIndex(255);
            this.attackerAccessory.SelectedIndex = itemNames.GetSortedIndex(255);
            this.targetWeapon.SelectedIndex = itemNames.GetSortedIndex(255);
            this.targetArmor.SelectedIndex = itemNames.GetSortedIndex(255);
            this.targetAccessory.SelectedIndex = itemNames.GetSortedIndex(255);
            // load entity
            for (int i = 0; i < Model.Characters.Length; i++)
                this.attackerName.Items.Add(new string(Model.Characters[i].Name));
            this.attackerName.SelectedIndex = 0;
            this.targetName.Items.AddRange(Model.MonsterNames.Names);
            this.targetName.SelectedIndex = monsterNames.GetSortedIndex(0);
            this.attackerBonus.SelectedIndex = 0;
            this.targetBonus.SelectedIndex = 0;
            this.Updating = false;
            CalculatePhysical();
            CalculateSpells();
            this.attackerBonus.SelectedIndex = 0;
            this.targetBonus.SelectedIndex = 0;
        }
        // functions
        private void CalculateLevel(bool attacker)
        {
            this.Updating = true;
            ComboBox bonus;
            NumericUpDown hp_;
            NumericUpDown attack_;
            NumericUpDown defense_;
            NumericUpDown mgAttack_;
            NumericUpDown mgDefense_;
            NumericUpDown level_;
            RadioButton radioButton;
            ComboBox names;
            CheckedListBox weakness;
            if (attacker)
            {
                bonus = attackerBonus;
                hp_ = attackerHP;
                attack_ = attackerAttack;
                defense_ = attackerDefense;
                mgAttack_ = attackerMgAttack;
                mgDefense_ = attackerMgDefense;
                level_ = attackerLevel;
                radioButton = attackerTypeAlly;
                names = attackerName;
                weakness = null;
            }
            else
            {
                bonus = targetBonus;
                hp_ = targetHP;
                attack_ = targetAttack;
                defense_ = targetDefense;
                mgAttack_ = targetMgAttack;
                mgDefense_ = targetMgDefense;
                level_ = targetLevel;
                radioButton = targetTypeAlly;
                names = targetName;
                weakness = targetWeakness;
            }
            if (radioButton.Checked)
            {
                Character character = Model.Characters[names.SelectedIndex];
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
                    int hp = character.StartingCurrentHP;
                    int attack = character.StartingAttack;
                    int defense = character.StartingDefense;
                    int mgAttack = character.StartingMgAttack;
                    int mgDefense = character.StartingMgDefense;
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
            }
            else
            {
                Monster monster = monsters[Model.MonsterNames.GetUnsortedIndex(names.SelectedIndex)];
                hp_.Value = monster.HP;
                attack_.Value = monster.Attack;
                defense_.Value = monster.Defense;
                mgAttack_.Value = monster.MagicAttack;
                mgDefense_.Value = monster.MagicDefense;
                if (weakness != null)
                {
                    weakness.SetItemChecked(0, monster.ElemWeakIce);
                    weakness.SetItemChecked(1, monster.ElemWeakThunder);
                    weakness.SetItemChecked(2, monster.ElemWeakFire);
                    weakness.SetItemChecked(3, monster.ElemWeakJump);
                }
            }
            CalculatePhysical();
            CalculateSpells();
            this.Updating = false;
        }
        private void CalculatePhysical()
        {
            double high;
            double low = (double)attackerAttack.Value;
            if (attackerTypeAlly.Checked)
            {
                low += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].Attack;
                low += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].Attack;
                low += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].Attack;
            }
            low -= (double)targetDefense.Value;
            if (targetTypeAlly.Checked)
            {
                low -= (double)items[itemNames.GetUnsortedIndex(targetWeapon.SelectedIndex)].MagicDefense;
                low -= (double)items[itemNames.GetUnsortedIndex(targetArmor.SelectedIndex)].MagicDefense;
                low -= (double)items[itemNames.GetUnsortedIndex(targetAccessory.SelectedIndex)].MagicDefense;
            }
            if (attackerTypeAlly.Checked)
            {
                high = low + items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].AttackRange;
                low -= items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].AttackRange;
            }
            else
                high = low;
            if (timedAttackL2.Checked)
            {
                low *= 2.0;
                high *= 2.0;
            }
            else if (timedAttackL1.Checked)
            {
                low *= 1.5;
                high *= 1.5;
            }
            if (targetDefensePosition.Checked)
            {
                low /= 2;
                high /= 2;
            }
            if (attackerStatus.GetItemChecked(0) || targetStatus.GetItemChecked(2))
            {
                low *= 1.5;
                high *= 1.5;
            }
            if (attackerStatus.GetItemChecked(2) || targetStatus.GetItemChecked(0))
            {
                low /= 2.0;
                high /= 2.0;
            }
            if (low < 1)
                low = 1;
            if (items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].AttackRange != 0)
                singleAttack.Text = Math.Ceiling(low).ToString() + " to " + Math.Ceiling(high).ToString();
            else
                singleAttack.Text = Math.Ceiling(low).ToString();
        }
        private void CalculateSpells()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (Spell spell in spells)
            {
                //double high;
                double low = spell.MagicPower;
                low += (double)attackerMgAttack.Value;
                if (attackerTypeAlly.Checked)
                {
                    low += items[itemNames.GetUnsortedIndex(attackerWeapon.SelectedIndex)].MagicAttack;
                    low += items[itemNames.GetUnsortedIndex(attackerArmor.SelectedIndex)].MagicAttack;
                    low += items[itemNames.GetUnsortedIndex(attackerAccessory.SelectedIndex)].MagicAttack;
                }
                low -= (double)targetMgDefense.Value;
                if (targetTypeAlly.Checked)
                {
                    low -= (double)items[itemNames.GetUnsortedIndex(targetWeapon.SelectedIndex)].MagicDefense;
                    low -= (double)items[itemNames.GetUnsortedIndex(targetArmor.SelectedIndex)].MagicDefense;
                    low -= (double)items[itemNames.GetUnsortedIndex(targetAccessory.SelectedIndex)].MagicDefense;
                }
                if (spell.InflictElement < 4 && targetWeakness.GetItemChecked(spell.InflictElement))
                {
                    low *= 2.0;
                }
                if (timedAttackL2.Checked)
                {
                    low *= 1.5;
                }
                else if (timedAttackL1.Checked)
                {
                    low *= 1.25;
                }
                if (targetDefensePosition.Checked)
                {
                    low /= 2.0;
                }
                if (attackerStatus.GetItemChecked(1) || targetStatus.GetItemChecked(3))
                {
                    low *= 1.5;
                }
                if (attackerStatus.GetItemChecked(3) || targetStatus.GetItemChecked(1))
                {
                    low /= 2.0;
                }
                if (low < 1)
                    low = 1;
                int index = spell.Index;
                ListViewItem item = new ListViewItem(new string[]
                {
                    index.ToString(),
                    Model.SpellNames.GetUnsortedName(spell.Index).Substring(1),
                    Math.Ceiling(low).ToString()
                });
                listViewItems.Add(item);
            }
            listView1.Items.AddRange(listViewItems.ToArray());
            listView1.EndUpdate();
        }
        // event handlers
        private void attackerType_CheckedChanged(object sender, EventArgs e)
        {
            this.Updating = true;
            if (!attackerTypeMonster.Checked)  // ally
            {
                this.attackerName.Items.Clear();
                for (int i = 0; i < Model.Characters.Length; i++)
                    this.attackerName.Items.Add(new string(Model.Characters[i].Name));
                this.attackerName.SelectedIndex = 0;
                this.panelAttackerProperties.Height = 69;
                this.panelAttackerStats.Height = 168;
                this.timedAttackL1.Visible = true;
                this.timedAttackL2.Visible = true;
            }
            else
            {
                this.attackerName.Items.Clear();
                this.attackerName.Items.AddRange(Model.MonsterNames.Names);
                this.attackerName.SelectedIndex = monsterNames.GetSortedIndex(0);
                this.panelAttackerProperties.Height = 21;
                this.panelAttackerStats.Height = 105;
                this.timedAttackL1.Visible = false;
                this.timedAttackL2.Visible = false;
            }
            this.Updating = false;
            loadProperties(sender, e);
        }
        private void targetType_CheckedChanged(object sender, EventArgs e)
        {
            this.Updating = true;
            if (!targetTypeMonster.Checked)  // ally
            {
                this.targetName.Items.Clear();
                for (int i = 0; i < Model.Characters.Length; i++)
                    this.targetName.Items.Add(new string(Model.Characters[i].Name));
                this.targetName.SelectedIndex = 0;
                this.panelTargetProperties.Height = 69;
                this.panelTargetStats.Height = 168;
                this.targetDefensePosition.Visible = true;
                this.targetWeakness.Visible = false;
            }
            else
            {
                this.targetName.Items.Clear();
                this.targetName.Items.AddRange(Model.MonsterNames.Names);
                this.targetName.SelectedIndex = monsterNames.GetSortedIndex(0);
                this.panelTargetProperties.Height = 21;
                this.panelTargetStats.Height = 105;
                this.targetDefensePosition.Visible = false;
                this.targetWeakness.Visible = true;
            }
            this.Updating = false;
            loadProperties(sender, e);
        }
        private void attackerName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (attackerTypeAlly.Checked)
                Do.DrawName(
                    sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                    Model.FontDialogue, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
            else
                Do.DrawName(sender, e, menuTextPreview, Model.MonsterNames, fontMenu, fontPaletteBattle, true, Model.MenuBG_);
        }
        private void targetName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (targetTypeAlly.Checked)
                Do.DrawName(
                    sender, e, new BattleDialoguePreview(), Lists.Convert(Model.Characters),
                    Model.FontDialogue, Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 0, false, false, Model.MenuBG_);
            else
                Do.DrawName(sender, e, menuTextPreview, Model.MonsterNames, fontMenu, fontPaletteBattle, true, Model.MenuBG_);
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
            CalculateLevel(
                sender == attackerTypeAlly ||
                sender == attackerLevel ||
                sender == attackerName ||
                sender == attackerBonus);
        }
        private void calculateTotal(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            if (sender == timedAttackL1 && timedAttackL1.Checked)
                timedAttackL2.Checked = false;
            if (sender == timedAttackL2 && timedAttackL2.Checked)
                timedAttackL1.Checked = false;
            CalculatePhysical();
            CalculateSpells();
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Do.SortListView((ListView)sender, lvwColumnSorter, e.Column);
        }
        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            bool typeMonster = attackerTypeMonster.Checked;
            int name = attackerName.SelectedIndex;
            int level = (int)attackerLevel.Value;
            int bonus = attackerBonus.SelectedIndex;
            int weapon = attackerWeapon.SelectedIndex;
            int armor = attackerArmor.SelectedIndex;
            int accessory = attackerAccessory.SelectedIndex;
            this.Updating = true;
            if (targetTypeMonster.Checked)
                attackerTypeMonster.Checked = true;
            else
                attackerTypeAlly.Checked = true;
            attackerName.SelectedIndex = targetName.SelectedIndex;
            attackerLevel.Value = targetLevel.Value;
            attackerBonus.SelectedIndex = targetBonus.SelectedIndex;
            attackerWeapon.SelectedIndex = targetWeapon.SelectedIndex;
            attackerArmor.SelectedIndex = targetArmor.SelectedIndex;
            attackerAccessory.SelectedIndex = targetAccessory.SelectedIndex;
            CalculateLevel(true);
            //
            if (typeMonster)
                targetTypeMonster.Checked = true;
            else
                targetTypeAlly.Checked = true;
            targetName.SelectedIndex = name;
            targetLevel.Value = level;
            targetBonus.SelectedIndex = bonus;
            targetWeapon.SelectedIndex = weapon;
            targetArmor.SelectedIndex = armor;
            targetAccessory.SelectedIndex = accessory;
            CalculateLevel(false);
            this.Updating = false;
        }
    }
}