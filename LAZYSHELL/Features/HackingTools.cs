using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAZYSHELL
{
    public partial class HackingTools : NewForm
    {
        private double percent { get { return (double)percentControl.Value * 0.01; } }
        private Delegate update;
        private ToolStripNumericUpDown index;
        // constructor
        public HackingTools(Delegate update, ToolStripNumericUpDown index)
        {
            this.update = update;
            this.index = index;
            InitializeComponent();
        }
        // functions
        private int AddPercent(int value, int max)
        {
            double percentage = (double)value;
            percentage += percentage * percent;
            percentage = (int)Math.Ceiling(percentage);
            return (int)Math.Max(0, Math.Min(max, percentage));
        }
        // event handlers
        private void selectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
        }
        private void deselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            int start = adjustAll.Checked ? 0 : (int)index.Value;
            int end = adjustAll.Checked ? Model.Monsters.Length : (int)index.Value + 1;
            for (int i = start; i < end; i++)
            {
                Monster monster = Model.Monsters[i];
                if (checkedListBox1.GetItemChecked(0))
                    monster.HP = (ushort)AddPercent(monster.HP, 65535);
                if (checkedListBox1.GetItemChecked(1))
                    monster.FP = (byte)AddPercent(monster.FP, 255);
                if (checkedListBox1.GetItemChecked(2))
                    monster.Attack = (byte)AddPercent(monster.Attack, 255);
                if (checkedListBox1.GetItemChecked(3))
                    monster.Defense = (byte)AddPercent(monster.Defense, 255);
                if (checkedListBox1.GetItemChecked(4))
                    monster.MagicAttack = (byte)AddPercent(monster.MagicAttack, 255);
                if (checkedListBox1.GetItemChecked(5))
                    monster.MagicDefense = (byte)AddPercent(monster.MagicDefense, 255);
                if (checkedListBox1.GetItemChecked(6))
                    monster.Evade = (byte)AddPercent(monster.Evade, 100);
                if (checkedListBox1.GetItemChecked(7))
                    monster.MagicEvade = (byte)AddPercent(monster.MagicEvade, 100);
                if (checkedListBox1.GetItemChecked(8))
                    monster.Experience = (ushort)AddPercent(monster.Experience, 65535);
                if (checkedListBox1.GetItemChecked(9))
                    monster.Coins = (byte)AddPercent(monster.Coins, 255);
            }
            update.DynamicInvoke();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void HackingTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
