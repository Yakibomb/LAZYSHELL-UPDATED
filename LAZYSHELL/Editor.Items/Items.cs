using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public partial class Items : NewForm
    {
        #region Variables
        private Settings settings = Settings.Default;
                private Item[] items { get { return Model.Items; } set { Model.Items = value; } }
        private Item item { get { return items[index]; } set { items[index] = value; } }
        public Item Item { get { return item; } set { item = value; } }
        private int index { get { return (int)itemNum.Value; } set { itemNum.Value = value; } }
        public int Index { get { return index; } set { index = value; } }
        private bool byteView { get { return !textView.Checked; } set { textView.Checked = !value; } }
        private Bitmap descriptionBGEquip;
        private Bitmap descriptionBGItem;
        private Bitmap descriptionText;
        private MenuDescriptionPreview menuDescPreview = new MenuDescriptionPreview();
        private TextHelperReduced textHelper = TextHelperReduced.Instance;
        private Shops shopsEditor;
        private EditLabel labelWindow;
        #endregion
        // constructor
        public Items(Shops shopsEditor)
        {
            this.shopsEditor = shopsEditor;
            InitializeComponent();
            itemName.BackgroundImage = Model.MenuBG;
            labelWindow = new EditLabel(itemName, itemNum, "Items", false);
            InitializeStrings();
            RefreshItems();
            if (settings.RememberLastIndex)
                index = settings.LastItem;
            //
            this.History = new History(this, itemName, itemNum);
        }
        #region Functions
        private void InitializeStrings()
        {
            this.itemName.Items.Clear();
            this.itemName.Items.AddRange(Model.ItemNames.Names);
            string[] temp = new string[96];
            for (int i = 0; i < 96; i++)
                temp[i] = i.ToString();
            this.itemNameIcon.Items.Clear();
            this.itemNameIcon.Items.AddRange(temp);
        }
        public void RefreshItems()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.Updating)
                return;
            this.Updating = true;
            this.itemName.SelectedIndex = Model.ItemNames.GetSortedIndex(index);
            this.itemName.Invalidate();
            this.itemCoinValue.Value = item.Price;
            this.itemSpeed.Value = item.Speed;
            this.itemAttack.Value = item.Attack;
            this.itemDefense.Value = item.Defense;
            this.itemMagicAttack.Value = item.MagicAttack;
            this.itemMagicDefense.Value = item.MagicDefense;
            this.itemAttackRange.Value = item.AttackRange;
            this.itemPower.Value = item.InflictionAmount;
            this.itemNameIcon.SelectedIndex = (int)(item.Name[0] - 0x20);
            this.itemNameIcon.Invalidate();
            this.textBoxItemName.Text = Do.RawToASCII(item.Name, Lists.KeystrokesMenu).Substring(1);
            if (this.itemNum.Value > 0xB0)
            {
                this.textBoxItemDescription.Text = " This item[1] cannot have a[1] description";
                if (item.RawDescription == null)
                    this.textBoxItemDescription_TextChanged(null, null);
                this.groupBox11.Enabled = false;
            }
            else
            {
                this.textBoxItemDescription.Text = item.GetDescription(byteView);
                this.groupBox11.Enabled = true;
            }
            this.itemStatusEffect.SetItemChecked(0, item.EffectMute);
            this.itemStatusEffect.SetItemChecked(1, item.EffectSleep);
            this.itemStatusEffect.SetItemChecked(2, item.EffectPoison);
            this.itemStatusEffect.SetItemChecked(3, item.EffectFear);
            this.itemStatusEffect.SetItemChecked(4, item.EffectMushroom);
            this.itemStatusEffect.SetItemChecked(5, item.EffectScarecrow);
            this.itemStatusEffect.SetItemChecked(6, item.EffectInvincible);
            this.itemStatusChange.SetItemChecked(0, item.ChangeAttack);
            this.itemStatusChange.SetItemChecked(1, item.ChangeDefense);
            this.itemStatusChange.SetItemChecked(2, item.ChangeMagicAttack);
            this.itemStatusChange.SetItemChecked(3, item.ChangeMagicDefense);
            this.itemElemNull.SetItemChecked(0, item.ElemNullIce);
            this.itemElemNull.SetItemChecked(1, item.ElemNullFire);
            this.itemElemNull.SetItemChecked(2, item.ElemNullThunder);
            this.itemElemNull.SetItemChecked(3, item.ElemNullJump);
            this.itemElemWeak.SetItemChecked(0, item.ElemWeakIce);
            this.itemElemWeak.SetItemChecked(1, item.ElemWeakFire);
            this.itemElemWeak.SetItemChecked(2, item.ElemWeakThunder);
            this.itemElemWeak.SetItemChecked(3, item.ElemWeakJump);
            this.itemWhoEquip.SetItemChecked(0, item.EquipMario);
            this.itemWhoEquip.SetItemChecked(1, item.EquipToadstool);
            this.itemWhoEquip.SetItemChecked(2, item.EquipBowser);
            this.itemWhoEquip.SetItemChecked(3, item.EquipGeno);
            this.itemWhoEquip.SetItemChecked(4, item.EquipMallow);
            this.itemUsage.SetItemChecked(0, item.UsageInstantDeath);
            this.itemUsage.SetItemChecked(1, item.HideDigits);
            this.itemUsage.SetItemChecked(2, item.UsageBattleMenu);
            this.itemUsage.SetItemChecked(3, item.UsageOverworldMenu);
            this.itemUsage.SetItemChecked(4, item.UsageReusable);
            this.itemCursorRestore.SetItemChecked(0, item.RestoreFP);
            this.itemCursorRestore.SetItemChecked(1, item.RestoreHP);
            this.itemTargetting.SetItemChecked(0, item.TargetLiveAlly);
            this.itemTargetting.SetItemChecked(1, item.TargetEnemy);
            this.itemTargetting.SetItemChecked(2, item.TargetAll);
            this.itemTargetting.SetItemChecked(3, item.TargetWoundedOnly);
            this.itemTargetting.SetItemChecked(4, item.TargetOnePartyOnly);
            this.itemTargetting.SetItemChecked(5, item.TargetNotSelf);
            this.itemAttackType.SelectedIndex = item.AttackType;
            this.itemType.SelectedIndex = item.ItemType;
            this.itemFunction.SelectedIndex = item.InflictFunction;
            this.itemElemAttack.SelectedIndex = item.ElemAttack;
            this.itemCursor.SelectedIndex = item.CursorBehavior;
            UpdateAttackType();
            // timing
            groupBox11.Visible = index < 37;
            if (index < 37)
            {
                this.lvl1TimingStart.Value = item.WeaponStartLevel1;
                this.lvl2TimingStart.Value = item.WeaponStartLevel2;
                this.lvl2TimingEnd.Value = item.WeaponEndLevel2;
                this.lvl1TimingEnd.Value = item.WeaponEndLevel1;
            }
            this.Updating = false;
            Cursor.Current = Cursors.Arrow;
        }
        private void UpdateAttackType()
        {
            if (item.AttackType == 0)
            {
                this.groupBox3.Text = "Effect <INFLICT>";
                this.groupBox9.Text = "Status <UP>";
            }
            else if (item.AttackType == 1)
            {
                this.groupBox3.Text = "Effect <PROTECT>";
                this.groupBox9.Text = "Status <. . . .>";
            }
            else if (item.AttackType == 2)
            {
                this.groupBox3.Text = "Effect <NULLIFY>";
                this.groupBox9.Text = "Status <DOWN>";
            }
            else if (item.AttackType == 3)
            {
                this.groupBox3.Text = "Effect <. . . .>";
                this.groupBox9.Text = "Status <. . . .>";
            }
        }
        private void SetDescriptionImage()
        {
            if (item.ItemType == 3)
            {
                int[] pixels = menuDescPreview.GetPreview(
                    Model.FontDescription, Model.FontPaletteMenu.Palettes[0],
                    item.RawDescription,
                    new Size(120, 48), new Point(8, 8), 4);
                descriptionText = Do.PixelsToImage(pixels, 120, 48);
                if (descriptionBGEquip == null)
                {
                    int[] bgPixels = Do.ImageToPixels(Model.MenuBG);
                    Do.DrawMenuFrame(bgPixels, 256, new Rectangle(0, 0, 15, 6), Model.MenuFrameGraphics, Model.FontPaletteMenu.Palette);
                    descriptionBGEquip = Do.PixelsToImage(bgPixels, 256, 256);
                }
            }
            else
            {
                int[] pixels = menuDescPreview.GetPreview(
                    Model.FontDescription, Model.FontPaletteMenu.Palettes[0],
                    item.RawDescription,
                    new Size(136, 64), new Point(16, 16), 4);
                descriptionText = Do.PixelsToImage(pixels, 136, 64);
                if (descriptionBGItem == null)
                {
                    int[] bgPixels = Do.ImageToPixels(Model.MenuBG);
                    Do.DrawMenuFrame(bgPixels, 256, new Rectangle(0, 0, 17, 8), Model.MenuFrameGraphics, Model.FontPaletteMenu.Palette);
                    descriptionBGItem = Do.PixelsToImage(bgPixels, 256, 256);
                }
            }
            pictureBoxItemDesc.Invalidate();
        }
        private void InsertIntoDescriptionText(string toInsert)
        {
            char[] newText = new char[this.textBoxItemDescription.Text.Length + toInsert.Length];
            textBoxItemDescription.Text.CopyTo(0, newText, 0, textBoxItemDescription.SelectionStart);
            toInsert.CopyTo(0, newText, textBoxItemDescription.SelectionStart, toInsert.Length);
            textBoxItemDescription.Text.CopyTo(textBoxItemDescription.SelectionStart, newText, textBoxItemDescription.SelectionStart + toInsert.Length, this.textBoxItemDescription.Text.Length - this.textBoxItemDescription.SelectionStart);
            if (byteView)
                item.CaretPositionByteView = this.textBoxItemDescription.SelectionStart + toInsert.Length;
            else
                item.CaretPositionTextView = this.textBoxItemDescription.SelectionStart + toInsert.Length;
            item.SetDescription(new string(newText), byteView);
            textBoxItemDescription.Text = item.GetDescription(byteView);
        }
        #endregion
        #region Event Handlers
        private void itemNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshItems();
            settings.LastItem = index;
            int offset = (index * 18) + 0x3A014D;
            item.BaseOffsetStats = offset;
            offset = 0x3A46EF + (index * 15);
            item.BaseOffsetName = offset;
            offset = (index * 2) + 0x3A40F2;
            item.BaseOffsetPrice = offset;
        }
        private void itemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int offset = (index * 18) + 0x3A014D;
            item.BaseOffsetStats = offset;
            offset = 0x3A46EF + (index * 15);
            item.BaseOffsetName = offset;
            offset = (index * 2) + 0x3A40F2;
            item.BaseOffsetPrice = offset;
            itemNum.Value = Model.ItemNames.GetUnsortedIndex(itemName.SelectedIndex);
        }
        private void itemName_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Do.DrawName(
                sender, e, new BattleDialoguePreview(), Model.ItemNames, Model.FontMenu,
                Model.FontPaletteMenu.Palettes[0], 8, 10, 0, 128, false, false, Model.MenuBG_);
        }
        private void textBoxItemName_TextChanged(object sender, EventArgs e)
        {
            char[] raw = new char[15];
            char[] temp = Do.ASCIIToRaw(this.textBoxItemName.Text, Lists.KeystrokesMenu, 14);
            for (int i = 0; i < 14; i++)
                raw[i + 1] = temp[i];
            raw[0] = (char)(itemNameIcon.SelectedIndex + 32);
            if (item.Name != raw)
            {
                item.Name = raw;
                Model.ItemNames.SetName(
                    item.Index,
                    new string(item.Name));
                Model.ItemNames.SortAlphabetically();
                this.itemName.Items.Clear();
                this.itemName.Items.AddRange(Model.ItemNames.Names);
                this.itemName.SelectedIndex = Model.ItemNames.GetSortedIndex(item.Index);
                shopsEditor.ResortStrings();
            }
        }
        private void itemNameIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.Name[0] = (char)(itemNameIcon.SelectedIndex + 0x20);
            if (Model.ItemNames.GetUnsortedName(index).CompareTo(this.textBoxItemName.Text) != 0)
            {
                Model.ItemNames.SetName(
                    item.Index, new string(item.Name));
                Model.ItemNames.SortAlphabetically();
                this.itemName.Items.Clear();
                this.itemName.Items.AddRange(Model.ItemNames.Names);
                this.itemName.SelectedIndex = Model.ItemNames.GetSortedIndex(item.Index);
            }
        }
        private void itemNameIcon_DrawItem(object sender, DrawItemEventArgs e)
        {
            Do.DrawIcon(sender, e, new MenuTextPreview(), 32, Model.FontMenu, Model.FontPaletteMenu.Palettes[0], false, Model.MenuBG_);
        }
        private void itemCoinValue_ValueChanged(object sender, EventArgs e)
        {
            item.Price = (ushort)this.itemCoinValue.Value;
        }
        private void itemSpeed_ValueChanged(object sender, EventArgs e)
        {
            item.Speed = (sbyte)this.itemSpeed.Value;
        }
        private void itemAttackRange_ValueChanged(object sender, EventArgs e)
        {
            item.AttackRange = (byte)this.itemAttackRange.Value;
        }
        private void itemPower_ValueChanged(object sender, EventArgs e)
        {
            item.InflictionAmount = (byte)this.itemPower.Value;
        }
        private void itemAttack_ValueChanged(object sender, EventArgs e)
        {
            item.Attack = (sbyte)this.itemAttack.Value;
        }
        private void itemDefense_ValueChanged(object sender, EventArgs e)
        {
            item.Defense = (sbyte)this.itemDefense.Value;
        }
        private void itemMagicAttack_ValueChanged(object sender, EventArgs e)
        {
            item.MagicAttack = (sbyte)this.itemMagicAttack.Value;
        }
        private void itemMagicDefense_ValueChanged(object sender, EventArgs e)
        {
            item.MagicDefense = (sbyte)this.itemMagicDefense.Value;
        }
        private void itemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.ItemType = (byte)this.itemType.SelectedIndex;
        }
        private void itemAttackType_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.AttackType = (byte)this.itemAttackType.SelectedIndex;
            UpdateAttackType();
        }
        private void itemFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.InflictFunction = (byte)this.itemFunction.SelectedIndex;
        }
        private void itemElemAttack_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.ElemAttack = (byte)this.itemElemAttack.SelectedIndex;
        }
        private void itemUsage_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.UsageInstantDeath = itemUsage.GetItemChecked(0);
            item.HideDigits = itemUsage.GetItemChecked(1);
            item.UsageBattleMenu = itemUsage.GetItemChecked(2);
            item.UsageOverworldMenu = itemUsage.GetItemChecked(3);
            item.UsageReusable = itemUsage.GetItemChecked(4);
        }
        private void itemStatusEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.EffectMute = itemStatusEffect.GetItemChecked(0);
            item.EffectSleep = itemStatusEffect.GetItemChecked(1);
            item.EffectPoison = itemStatusEffect.GetItemChecked(2);
            item.EffectFear = itemStatusEffect.GetItemChecked(3);
            item.EffectMushroom = itemStatusEffect.GetItemChecked(4);
            item.EffectScarecrow = itemStatusEffect.GetItemChecked(5);
            item.EffectInvincible = itemStatusEffect.GetItemChecked(6);
        }
        private void itemElemNull_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.ElemNullIce = itemElemNull.GetItemChecked(0);
            item.ElemNullFire = itemElemNull.GetItemChecked(1);
            item.ElemNullThunder = itemElemNull.GetItemChecked(2);
            item.ElemNullJump = itemElemNull.GetItemChecked(3);
        }
        private void itemElemWeak_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.ElemWeakIce = itemElemWeak.GetItemChecked(0);
            item.ElemWeakFire = itemElemWeak.GetItemChecked(1);
            item.ElemWeakThunder = itemElemWeak.GetItemChecked(2);
            item.ElemWeakJump = itemElemWeak.GetItemChecked(3);
        }
        private void itemStatusChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.ChangeAttack = itemStatusChange.GetItemChecked(0);
            item.ChangeDefense = itemStatusChange.GetItemChecked(1);
            item.ChangeMagicAttack = itemStatusChange.GetItemChecked(2);
            item.ChangeMagicDefense = itemStatusChange.GetItemChecked(3);
        }
        private void itemWhoEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.EquipMario = itemWhoEquip.GetItemChecked(0);
            item.EquipToadstool = itemWhoEquip.GetItemChecked(1);
            item.EquipBowser = itemWhoEquip.GetItemChecked(2);
            item.EquipGeno = itemWhoEquip.GetItemChecked(3);
            item.EquipMallow = itemWhoEquip.GetItemChecked(4);
        }
        private void itemTargetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.TargetLiveAlly = itemTargetting.GetItemChecked(0);
            item.TargetEnemy = itemTargetting.GetItemChecked(1);
            item.TargetAll = itemTargetting.GetItemChecked(2);
            item.TargetWoundedOnly = itemTargetting.GetItemChecked(3);
            item.TargetOnePartyOnly = itemTargetting.GetItemChecked(4);
            item.TargetNotSelf = itemTargetting.GetItemChecked(5);
        }
        private void itemCursor_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.CursorBehavior = (byte)itemCursor.SelectedIndex;
        }
        private void itemCursorRestore_SelectedIndexChanged(object sender, EventArgs e)
        {
            item.RestoreFP = itemCursorRestore.GetItemChecked(0);
            item.RestoreHP = itemCursorRestore.GetItemChecked(1);
        }
        // description
        private void textBoxItemDescription_TextChanged(object sender, EventArgs e)
        {
            char[] text = textBoxItemDescription.Text.ToCharArray();
            char[] swap;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    int tempSel = textBoxItemDescription.SelectionStart;
                    swap = new char[text.Length + 2];
                    for (int x = 0; x < i; x++)
                        swap[x] = text[x];
                    swap[i] = '[';
                    swap[i + 1] = '1';
                    swap[i + 2] = ']';
                    for (int x = i + 3; x < swap.Length; x++)
                        swap[x] = text[x - 2];
                    textBoxItemDescription.Text = new string(swap);
                    text = textBoxItemDescription.Text.ToCharArray();
                    i += 2;
                    textBoxItemDescription.SelectionStart = tempSel + 2;
                }
            }
            bool flag = textHelper.VerifySymbols(this.textBoxItemDescription.Text.ToCharArray(), byteView);
            if (flag)
            {
                this.textBoxItemDescription.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                item.SetDescription(this.textBoxItemDescription.Text, byteView);
            }
            if (!flag || item.DescriptionError)
                this.textBoxItemDescription.BackColor = System.Drawing.Color.Red;
            SetDescriptionImage();
        }
        private void pictureBoxItemDesc_Paint(object sender, PaintEventArgs e)
        {
            if (item.ItemType == 3 && descriptionBGEquip != null)
                e.Graphics.DrawImage(descriptionBGEquip, 0, 0);
            else if (descriptionBGItem != null)
                e.Graphics.DrawImage(descriptionBGItem, 0, 0);
            if (descriptionText == null)
                SetDescriptionImage();
            e.Graphics.DrawImage(descriptionText, 0, 0);
        }
        private void textView_Click(object sender, EventArgs e)
        {
            this.textBoxItemDescription.Text = item.GetDescription(byteView);
        }
        private void newLine_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDescriptionText("[1]");
            else
                InsertIntoDescriptionText("[newLine]");
        }
        private void endString_Click(object sender, EventArgs e)
        {
            if (byteView)
                InsertIntoDescriptionText("[0]");
            else
                InsertIntoDescriptionText("[endInput]");
        }
        // defense timing
        private void numericUpDown118_ValueChanged(object sender, EventArgs e)
        {
            item.WeaponStartLevel1 = (byte)this.lvl1TimingStart.Value;
        }
        private void numericUpDown120_ValueChanged(object sender, EventArgs e)
        {
            item.WeaponStartLevel2 = (byte)this.lvl2TimingStart.Value;
        }
        private void numericUpDown117_ValueChanged(object sender, EventArgs e)
        {
            item.WeaponEndLevel2 = (byte)this.lvl2TimingEnd.Value;
        }
        private void numericUpDown119_ValueChanged(object sender, EventArgs e)
        {
            item.WeaponEndLevel1 = (byte)this.lvl1TimingEnd.Value;
        }
        // hex editor jump to offset
        private void showitem_hexname(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset(item.BaseOffsetName);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        private void showitem_hexstats(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset(item.BaseOffsetStats);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        private void showitem_hexprice(object sender, EventArgs e)
        {
            Model.HexEditor.SetOffset(item.BaseOffsetPrice);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        #endregion
    }
}
