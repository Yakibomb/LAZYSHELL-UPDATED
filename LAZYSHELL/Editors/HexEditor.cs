using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LAZYSHELL
{
    public partial class HexEditor : NewForm
    {
        #region Variables
        private byte[] rom
        {
            get
            {
                return viewCurrent.Checked ? current : original;
            }
        }
        private byte[] current_unmodified;
        private byte[] current;
        private byte[] original;
        private int selection { get { return ROMData.SelectionStart / 3 + offset; } }
                private bool atLowerNibble { get { return ROMData.SelectionStart % 3 == 1; } }
        private bool atUpperNibble = false;
        private int end { get { return ((line_count - 1) * 16 + 15) * 3; } }
        private int offset;
        private List<Change> oldProperties = new List<Change>();
        private List<Change> newProperties = new List<Change>();
        private byte[] clipboard;
        private int last_line
        {
            get { return line_count * 16 + (offset & 0xFFFFF0); }
        }
        private int line_count
        {
            get
            {
                return (ROMOffsets.Height - 5) / 15;
            }
        }
        private int line
        {
            get
            {
                return ROMData.SelectionStart / (16 * 3);
            }
        }
        private int byte_count
        {
            get
            {
                return line_count * 16;
            }
        }
        private NewRichTextBox ROMData
        {
            get
            {
                return currentROMData.Visible ? currentROMData : originalROMData;
            }
            set
            {
                if (currentROMData.Visible)
                    currentROMData = value;
                else
                    originalROMData = value;
            }
        }
        private int selectionStart;
        private int selectionLength;
        private bool isMovingUpDown;
        #endregion
        public HexEditor(byte[] current, byte[] original)
        {
            this.current_unmodified = current;
            this.current = Bits.Copy(current);
            this.original = original;
            InitializeComponent();
            RefreshHexEditor();
            Do.AddShortcut(toolStrip2, Keys.F1, helpTips);
            new ToolTipLabel(this, null, helpTips);
        }
        #region Functions
        public void ClearMemory()
        {
            oldProperties.Clear();
            newProperties.Clear();
        }
        public void Compare()
        {
            ClearMemory();
            for (int offset = 0; offset < 0x400000; offset++)
            {
                int changedStart = 0;
                int changedLength = 0;
                int index = offset;
                while (current_unmodified[offset] != current[offset])
                {
                    changedStart = offset++;
                    changedLength += 3;
                }
                if (changedLength > 0)
                    oldProperties.Add(new Change(index, new byte[changedLength / 3], Color.Green));
            }
            current_unmodified.CopyTo(current, 0);
            RefreshHexEditor();
        }
        public void SetOffset(int offset)
        {
            this.offset = offset & 0xFFFFF0;
            this.selectionStart = (offset & 15) * 3;
            this.ROMData.SelectionStart = (offset & 15) * 3;
        }
        public void RefreshHexEditor()
        {
            this.Updating = true;
            vScrollBar1.Value = offset / 16;
            string bytes2 = "";
            string bytes3 = "";
            string offsets = "";
            // in case enlarging when at end of ROM
            while (last_line > current.Length)
                offset -= 16;
            //
            int offset_line = offset & 0xFFFFF0;
            for (int i = 0; i < line_count; i++)
            {
                offsets += (i * 16 + offset_line).ToString("X6");
                if (i < line_count - 1)
                    offsets += "\r";
                for (int a = 0; a < 16; a++)
                {
                    bytes2 += (current[i * 16 + offset_line + a]).ToString("X2") + " ";
                    bytes3 += (original[i * 16 + offset_line + a]).ToString("X2") + " ";
                }
            }
            //
            ROMOffsets.BeginUpdate();
            ROMOffsets.Text = offsets;
            ROMOffsets.EndUpdate();
            //
            ROMData.BeginUpdate();
            currentROMData.Text = bytes2;
            originalROMData.Text = bytes3;
            currentROMData.SelectionStart = 0;
            currentROMData.SelectionLength = currentROMData.Text.Length;
            currentROMData.SelectionColor = Color.DarkBlue;
            for (int offsetCounter = offset & 0xFFFFF0, i = 0; i < line_count * 16; i++)
            {
                // first set the length of the changed offsets, to colorize all at once (b/c faster)
                int changedStart = 0;
                int changedLength = 0;
                Change change = Do.FindOffset(oldProperties, offsetCounter++);
                if (change != null)
                {
                    changedStart = i * 3;
                    if (change.Values.Length == 1)
                    {
                        changedLength = 3;
                        while (Do.Contains(oldProperties, offsetCounter++))
                            changedLength += 3;
                        i += changedLength / 3;
                    }
                    else
                    {
                        offsetCounter--;
                        changedLength = (change.Values.Length - (offsetCounter - change.Offset)) * 3;
                        i += changedLength / 3;
                        offsetCounter += changedLength / 3;
                        offsetCounter++;
                    }
                }
                if (changedLength > 0)
                {
                    currentROMData.SelectionStart = changedStart;
                    currentROMData.SelectionLength = changedLength;
                    currentROMData.SelectionColor = change.Color;
                }
            }
            currentROMData.SelectionStart = originalROMData.SelectionStart = selectionStart;
            currentROMData.SelectionLength = originalROMData.SelectionLength = selectionLength;
            ROMData.Focus();
            ROMData.EndUpdate();
            //
            UpdateInformationLabels();
            this.Updating = false;
        }
        private void UpdateInformationLabels()
        {
            if (selection + 2 >= rom.Length)
                return;
            info_offset.Text = "Offset: " + selection.ToString("X6");
            if (ROMData.SelectionLength / 3 == 3)
                info_value.Text = "Value: " + Bits.GetInt24(rom, selection).ToString();
            else if (ROMData.SelectionLength / 3 == 2)
                info_value.Text = "Value: " + Bits.GetShort(rom, selection).ToString();
            else if (ROMData.SelectionLength / 3 <= 1)
                info_value.Text = "Value: " + rom[selection].ToString();
            else
                info_value.Text = "Value: ";
            int sel = ROMData.SelectionLength / 3;
            info_sel.Text = "Sel: 0x" + sel.ToString("X") + " (" + sel.ToString() + ") bytes";
        }
        #endregion
        #region Event Handlers
        private void HexViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            ROMData.BeginUpdate();
            if (ROMData.SelectionStart / 3 + offset >= rom.Length)
                ROMData.SelectionStart = end;
            else if (ROMData.SelectionStart % 3 == 2 && !atUpperNibble &&
                ROMData.SelectionStart / 3 + offset + 1 < rom.Length)
                ROMData.SelectionStart++;
            else if (ROMData.SelectionStart % 3 == 2 && atUpperNibble)
                ROMData.SelectionStart--;
            atUpperNibble = ROMData.SelectionStart % 3 == 0;
            UpdateInformationLabels();
            ROMData.EndUpdate();
            this.Updating = false;
        }
        private void richTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            //offset += richTextBox2.SelectionStart / 3;
        }
        private void richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectionStart = ROMData.SelectionStart;
            selectionLength = ROMData.SelectionLength;
        }
        private void richTextBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0 && vScrollBar1.Value > 0)
                vScrollBar1.Value--;
            else if (e.Delta < 0 && vScrollBar1.Value < vScrollBar1.Maximum)
                vScrollBar1.Value++;
        }
        private void richTextBox_SizeChanged(object sender, EventArgs e)
        {
            RefreshHexEditor();
        }
        private void richTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int column = ROMData.SelectionStart / 3;
            if (!isMovingUpDown)
            {
                selectionStart = ROMData.SelectionStart;
                selectionLength = ROMData.SelectionLength;
            }
            switch (e.KeyData)
            {
                case Keys.Control | Keys.Z:
                    undo.PerformClick();
                    break;
                case Keys.Control | Keys.Y:
                    redo.PerformClick();
                    break;
                case Keys.Control | Keys.S:
                    save.PerformClick();
                    break;
                case Keys.Down:
                    if (line != line_count - 1)
                        break;
                    offset += 0x10;
                    if (offset + (line_count * 16) >= rom.Length)
                        offset = rom.Length - (line_count * 16);
                    RefreshHexEditor();
                    isMovingUpDown = true;
                    break;
                case Keys.Up:
                    if (line != 0)
                        break;
                    offset -= 0x10;
                    if (offset < 0)
                        offset = 0;
                    RefreshHexEditor();
                    isMovingUpDown = true;
                    break;
                case Keys.PageDown:
                    offset += line_count * 16;
                    if (offset >= rom.Length)
                        offset = rom.Length - (line_count * 16);
                    RefreshHexEditor();
                    isMovingUpDown = true;
                    break;
                case Keys.PageUp:
                    offset -= (line_count * 16);
                    if (offset < 0)
                        offset = 0;
                    RefreshHexEditor();
                    isMovingUpDown = true;
                    break;
                case Keys.Home:
                    selectionStart = 0;
                    offset = 0;
                    RefreshHexEditor();
                    break;
                case Keys.End:
                    selectionStart = line_count * 16 * 3 - 3;
                    offset = rom.Length - (line_count * 16);
                    RefreshHexEditor();
                    break;
                default:
                    byte value = 255;
                    try
                    {
                        value = Convert.ToByte(((char)e.KeyValue).ToString(), 16);
                    }
                    catch
                    {
                        break;
                    }
                    if (value > 15)
                        break;
                    if (!currentROMData.Visible)
                    {
                        MessageBox.Show("Changing the original ROM's data is not allowed.");
                        break;
                    }
                    oldProperties.Add(new Change(offset + column, current[offset + column], Color.Red));
                    if (!atLowerNibble)
                    {
                        value <<= 4;
                        current[offset + column] &= 0x0F;
                        current[offset + column] |= value;
                        RefreshHexEditor();
                        currentROMData.SelectionStart = selectionStart + 1;
                    }
                    else
                    {
                        current[offset + column] &= 0xF0;
                        current[offset + column] |= value;
                        RefreshHexEditor();
                        currentROMData.SelectionStart = selectionStart + 2;
                    }
                    break;
            }
        }
        private void richTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (isMovingUpDown)
                ROMData.SelectionStart = selectionStart;
            isMovingUpDown = false;
        }
        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            offset = Math.Min(vScrollBar1.Value * 16, 0x400000 - (line_count * 16));
            RefreshHexEditor();
        }
        // toolstrip2
        private void save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Saving the ROM in the hex editor resets all elements in all other editors. " +
                "You will lose any changes made there since the last save.\n\n" +
                "Continue with process?", "LAZY SHELL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            current.CopyTo(current_unmodified, 0);
            Model.ClearModel();
            ClearMemory();
            RefreshHexEditor();
        }
        private void copy_Click(object sender, EventArgs e)
        {
            if (ROMData.SelectionLength < 3)
                return;
            int column = ROMData.SelectionStart / 3;
            clipboard = Bits.GetBytes(rom, offset + column, ROMData.SelectionLength / 3);
        }
        private void paste_Click(object sender, EventArgs e)
        {
            int column = ROMData.SelectionStart / 3;
            if (offset + column + clipboard.Length >= 0x400000)
                return;
            oldProperties.Add(new Change(offset + column,
                Bits.GetBytes(current, offset + column, clipboard.Length), Color.Red));
            Bits.SetBytes(current, offset + column, clipboard);
            RefreshHexEditor();
        }
        private void undo_Click(object sender, EventArgs e)
        {
            if (oldProperties.Count == 0)
                return;
            int offset = oldProperties[oldProperties.Count - 1].Offset;
            byte[] oldValues = oldProperties[oldProperties.Count - 1].Values;
            oldProperties.RemoveAt(oldProperties.Count - 1);
            byte[] newValues = Bits.GetBytes(current, offset, oldValues.Length);
            newProperties.Add(new Change(offset, newValues, Color.Red));
            Bits.SetBytes(current, offset, oldValues);
            RefreshHexEditor();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            if (newProperties.Count == 0)
                return;
            int offset = newProperties[newProperties.Count - 1].Offset;
            byte[] newValues = newProperties[newProperties.Count - 1].Values;
            newProperties.RemoveAt(newProperties.Count - 1);
            byte[] oldValues = Bits.GetBytes(current, offset, newValues.Length);
            oldProperties.Add(new Change(offset, oldValues, Color.Red));
            Bits.SetBytes(current, offset, newValues);
            RefreshHexEditor();
        }
        private void fillWith_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;
            if (currentROMData.SelectionLength < 3)
                return;
            int column = ROMData.SelectionStart / 3;
            byte value;
            try
            {
                value = Convert.ToByte(fillWith.Text, 16);
                byte[] values = new byte[currentROMData.SelectionLength / 3];
                Bits.Fill(values, value);
                oldProperties.Add(new Change(offset + column,
                    Bits.GetBytes(current, offset + column, values.Length), Color.Red));
                Bits.Fill(current, value, offset + column, values.Length);
            }
            catch
            {
                return;
            }
            RefreshHexEditor();
        }
        private void baseConvDec_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            try
            {
                long value = Convert.ToInt64(baseConvDec.Text, 10);
                if (value <= 0xFFFFFFFF)
                    baseConvHex.Text = value.ToString("X");
            }
            catch
            {
                baseConvHex.Text = "";
            }
            this.Updating = false;
        }
        private void baseConvHex_TextChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            try
            {
                long value = Convert.ToInt64(baseConvHex.Text, 16);
                baseConvDec.Text = value.ToString();
            }
            catch
            {
                baseConvDec.Text = "";
            }
            this.Updating = false;
        }
        // toolstrip1
        private void viewCurrent_Click(object sender, EventArgs e)
        {
            viewOriginal.Checked = false;
            currentROMData.Show();
            originalROMData.Hide();
            viewCurrent.Checked = true;
            RefreshHexEditor();
            ROMData.Focus();
            ROMData.SelectionStart = selectionStart;
            ROMData.SelectionLength = selectionLength;
        }
        private void viewOriginal_Click(object sender, EventArgs e)
        {
            viewCurrent.Checked = false;
            originalROMData.Show();
            currentROMData.Hide();
            viewOriginal.Checked = true;
            RefreshHexEditor();
            ROMData.Focus();
            ROMData.SelectionStart = selectionStart;
            ROMData.SelectionLength = selectionLength;
        }
        private void gotoAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;
            try
            {
                int input = Convert.ToInt32(gotoAddress.Text, 16);
                if (input >= 0 && input < 0x400000)
                {
                    offset = input & 0xFFFFF0;
                    if (line_count * 16 + offset >= 0x400000)
                        offset = 0x400000 - (line_count * 16);
                    selectionStart = Math.Max(0, (input - offset - 1) * 3);
                }
                else if (input < 0x000000)
                {
                    MessageBox.Show("Offset too low. Must be between $000000 and $3FFFFF.");
                    return;
                }
                else if (input >= 0x400000)
                {
                    MessageBox.Show("Offset too high. Must be between $000000 and $3FFFFF.");
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                RefreshHexEditor();
                ROMData.Focus();
            }
        }
        private void searchValues_KeyDown(object sender, KeyEventArgs e)
        {
            if (searchBox.Text == "" ||
                searchBox.Text.Length % 2 != 0 ||
                e.KeyData != Keys.Enter)
                return;
            try
            {
                byte[] values = new byte[searchBox.Text.Length / 2];
                for (int i = 0; i < searchBox.Text.Length / 2; i++)
                {
                    string ascii = searchBox.Text.Substring(i * 2, 2);
                    byte value = Convert.ToByte(ascii, 16);
                    values[i] = value;
                }
                int offset, foundAt;
                offset = this.offset;
                foundAt = Bits.Find(rom, values, ROMData.SelectionStart / 3 + ROMData.SelectionLength + offset);
                if (foundAt != -1)
                {
                    this.offset = foundAt & 0xFFFFF0;
                    selectionStart = (foundAt & 15) * 3;
                    selectionLength = values.Length * 3;
                    RefreshHexEditor();
                    searchBox.Focus();
                }
                else if (MessageBox.Show("Search string was not found.\n\n" + "Restart from beginning?", "LAZY SHELL",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    offset = this.offset;
                    foundAt = Bits.Find(rom, values, 0);
                    if (foundAt != -1)
                    {
                        this.offset = foundAt & 0xFFFFF0;
                        selectionStart = (foundAt & 15) * 3;
                        selectionLength = values.Length * 3;
                        RefreshHexEditor();
                        searchBox.Focus();
                    }
                    else
                        MessageBox.Show("Search string was not found.");
                }
            }
            catch
            {
                MessageBox.Show("Invalid search string.");
            }
        }
        #endregion
        public class Change
        {
            public int Offset;
            public byte[] Values;
            public byte Value { get { return Values[0]; } }
            public int Length { get { return Values.Length; } }
            public Change(int offset, byte value, Color color)
            {
                Offset = offset;
                Values = new byte[] { value };
                Color = color;
            }
            public Change(int offset, byte[] values, Color color)
            {
                Offset = offset;
                Values = Bits.Copy(values);
                Color = color;
            }
            public Color Color;
        }
    }
}
