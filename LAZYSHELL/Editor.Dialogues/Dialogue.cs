using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LAZYSHELL
{
    [Serializable()]
    public class Dialogue : Element
    {
        #region variables
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private char[] text;
        private int offset;
        private int pointer;
        private bool error = false;
        private int caretPosByteView;
        private int caretPosTextView;
        // non-serialized variables
        [NonSerialized()]
        private TextHelper textHelper;
        private int reference;
        private int parent;
        private int position;
        #endregion
        #region accessors
        // public accessors
        public char[] Text { get { return text; } set { text = value; } }
        public int Length { get { return text.Length; } }
        public int Offset { get { return this.offset; } set { this.offset = value; } }
        public int Pointer { get { return Bits.GetShort(rom, 0x37E000 + index * 2); } }    // this is used to find duplicates
        // external managers       
        /// <summary>
        /// The dialogue's reference dialogue. Ignored if same as index.
        /// </summary>
        public int Reference { get { return reference; } set { reference = value; } }
        /// <summary>
        /// The dialogue's containing dialogue. Ignored if same as index.
        /// </summary>
        public int Parent { get { return parent; } set { parent = value; } }
        /// <summary>
        /// The index, or position, of the dialogue in it's parent container. Ignored if parent same as index.
        /// </summary>
        public int Position { get { return position; } set { position = value; } }
        #endregion
        // constructor
        public Dialogue(int index)
        {
            this.index = index;
            this.textHelper = TextHelper.Instance;
            Disassemble();
        }
        #region functions
        // assemblers
        private void Disassemble()
        {
            GetText();
            //text = GetText();
        }
        public void Assemble(int offset)
        {
            Assemble(ref offset);
        }
        public void Assemble(ref int offset)
        {
            if (Index >= 2048)
                Bits.SetShort(rom, 0x37E000 + Index * 2, (ushort)(offset - 4));
            else
                Bits.SetShort(rom, 0x37E000 + Index * 2, (ushort)(offset - 8));
            this.Offset = offset;

            // Write the dialogue to ROM
            Bits.SetChars(rom, this.Offset, Text);

            // Add dialogue length to offset
            offset += Text.Length;
        }
        /*
        public void Assemble(int offset)
        {
            Assemble(ref offset);
        }
        public void Assemble(ref int offset)
        {
            if (index >= 0x0800)
                Bits.SetShort(rom, 0x37E000 + index * 2, (ushort)(offset - 4));
            else
                Bits.SetShort(rom, 0x37E000 + index * 2, (ushort)(offset - 8));
            int dlgOffset = 0;
            // Select bank to save to
            if (index >= 0x0C00)
                dlgOffset = offset + 0x240000;
            else if (index >= 0x0800)
                dlgOffset = offset + 0x230000;
            else
                dlgOffset = offset + 0x220000;
            Bits.SetChars(rom, dlgOffset, text);
            offset += text.Length;
        }
        */
        // class functions
        // Read/write ROM
        // class functions
        private void GetText()
        {

            int pointer = Bits.GetShort(rom, 0x37E000 + (Index * 2));

            /* This operation checks if pointer points to beyond capacity of the dialogue in the bank.
             * If it is, then it sets the pointer to the last dialogue, thus making it a duplicate.
             * This fixes the problems with dialogues 3066 to 3071. */

            // Get pointer to dialogue in bank
            int secPtr;
            if (Index >= 0xC00)
                secPtr = Bits.GetShort(rom, 0x240000 + (((Index - 0xC00) >> 8) & 0xFE));
            else if (Index >= 0x800)
                secPtr = Bits.GetShort(rom, 0x230000 + (((Index - 0x800) >> 8) & 0xFE));
            else
                secPtr = Bits.GetShort(rom, 0x220000 + ((Index >> 8) & 0xFE));

            // Total size of the pointer table in the respective bank
            int ptrTblSize = Index >= 0x800 ? 4 : 8;


            // Set pointer of dialogue
            pointer = pointer + secPtr - ptrTblSize;
            Bits.SetShort(rom, 0x37E000 + (Index * 2), (ushort)pointer);

            // Actually replace the invalid pointer with a duplicate
            if (Index >= 0xC00)
            {
                if (pointer >= 0xFFFF - 4 || (pointer >= 0x9000 - 4 && pointer < 0xEDE0 - 4))
                {
                    Bits.SetShort(rom, 0x37E000 + (Index * 2), (ushort)(Bits.GetShort(rom, 0x37E000 + ((Index - 1) * 2))));
                    MessageBox.Show("Warning: Invalid pointer for dialogue #" + Index.ToString() + ".  Replaced with duplicate.",
                            "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Index >= 0x800)
            {
                if (pointer >= 0xF2D5 - 4)
                {
                    Bits.SetShort(rom, 0x37E000 + (Index * 2), (ushort)(Bits.GetShort(rom, 0x37E000 + ((Index - 1) * 2))));
                    MessageBox.Show("Warning: Invalid pointer for dialogue #" + Index.ToString() + ".  Replaced with duplicate.",
                            "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (pointer >= 0xFD18 - 8)
                {
                    Bits.SetShort(rom, 0x37E000 + (Index * 2), (ushort)(Bits.GetShort(rom, 0x37E000 + ((Index - 1) * 2))));
                    MessageBox.Show("Warning: Invalid pointer for dialogue #" + Index.ToString() + ".  Replaced with duplicate.",
                            "LAZYSHELL++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Simplify all of the section pointers
            if (Index == 0xFFF)
            {
                Bits.SetShort(rom, 0x220002, 0x0008);
                Bits.SetShort(rom, 0x220004, 0x0008);
                Bits.SetShort(rom, 0x220006, 0x0008);
                Bits.SetShort(rom, 0x230002, 0x0004);
                Bits.SetShort(rom, 0x240002, 0x0004);
            }

            // Set this offset
            if (Index >= 0x0C00)
                this.Offset = this.Pointer + 4 + 0x240000;
            else if (Index >= 0x0800)
                this.Offset = this.Pointer + 4 + 0x230000;
            else
                this.Offset = this.Pointer + 8 + 0x220000;

            // Get total length of text
            int offset = this.Offset;
            int length = 0;
            byte symbol = 0x01;
            while (symbol != 0x00 && symbol != 0x06)
            {
                symbol = rom[offset];

                // Values with a parameter
                if (symbol == 0x0B || symbol == 0x0D || symbol == 0x1C)
                {
                    length++;
                    offset++;
                }
                length++;
                offset++;
            }

            // Finally, initialize the text
            this.Text = new char[length];
            for (int i = 0; i < length; i++)
                this.Text[i] = (char)rom[this.Offset + i];
            
            /*
            int dlgPtr = Bits.GetShort(rom, 0x37E000 + (index * 2));
            int secPtr;
            if (index >= 0xC00)
                secPtr = Bits.GetShort(rom, 0x240000 + (((index - 0xC00) >> 8) & 0xFE));
            else if (index >= 0x800)
                secPtr = Bits.GetShort(rom, 0x230000 + (((index - 0x800) >> 8) & 0xFE));
            else
                secPtr = Bits.GetShort(rom, 0x220000 + ((index >> 8) & 0xFE));
            int numGroup = index >= 0x800 ? 4 : 8;
            Bits.SetShort(rom, 0x37E000 + (index * 2), (ushort)(dlgPtr + secPtr - numGroup));
            // checks if pointer points to beyond capacity of dialogue in bank
            // if it is, then it sets the pointer to the last dialogue, thus making it a duplicate
            // this fixes the problems with dialogues 3066 to 3071
            dlgPtr = Bits.GetShort(rom, 0x37E000 + (index * 2));
            if (index >= 0x800)
            {
                if (dlgPtr >= 0xF2D1 - 4)
                    Bits.SetShort(rom, 0x37E000 + (index * 2), (ushort)(Bits.GetShort(rom, 0x37E000 + ((index - 1) * 2))));
            }
            else
            {
                if (dlgPtr >= 0xFD18 - 8)
                    Bits.SetShort(rom, 0x37E000 + (index * 2), (ushort)(Bits.GetShort(rom, 0x37E000 + ((index - 1) * 2))));
            }
            // simplify all of the section pointers
            if (index == 0xFFF)
            {
                Bits.SetShort(rom, 0x220002, 0x0008);
                Bits.SetShort(rom, 0x220004, 0x0008);
                Bits.SetShort(rom, 0x220006, 0x0008);
                Bits.SetShort(rom, 0x230002, 0x0004);
                Bits.SetShort(rom, 0x240002, 0x0004);
            }
            this.pointer = Bits.GetShort(rom, 0x37E000 + index * 2); // from pointer table
            if (index >= 0x0C00)
                this.offset = this.pointer + 4 + 0x240000;
            else if (index >= 0x0800)
                this.offset = this.pointer + 4 + 0x230000;
            else
                this.offset = this.pointer + 8 + 0x220000;
            //
            int count = this.offset;
            int length = 0;
            byte ptr = 0x01;
            while (ptr != 0x00 && ptr != 0x06)
            {
                ptr = rom[count];
                if (ptr == 0x0B || ptr == 0x0D || ptr == 0x1C)
                {
                    length++;
                    count++;
                }
                length++;
                count++;
            }
            char[] dialogue = new char[length];
            for (int i = 0; i < length; i++)
                dialogue[i] = (char)rom[this.offset + i];
            return dialogue;
            */

        }
        public string GetText(bool byteView, string[] tables)
        {
            if (!error)
                return new string(textHelper.Decode(text, byteView, tables));
            else
                return new string(text);
        }
        public string GetStub(bool byteView, string[] tables)
        {
            string temp = GetText(byteView, tables);
            if (temp.Length > 40)
            {
                temp = temp.Substring(0, 37);
                return temp + "...";
            }
            else
                return temp;
        }
        public int GetCaretPosition(bool byteView)
        {
            if (byteView)
                return caretPosByteView;
            else
                return caretPosTextView;
        }
        public bool SetText(string value, bool byteView, string[] tables)
        {
            this.text = textHelper.Encode(value.ToCharArray(), byteView, tables);
            this.error = textHelper.Error;
            return !error;
        }
        public void SetCaretPosition(int value, bool byteView)
        {
            if (byteView)
                this.caretPosByteView = value;
            else
                this.caretPosTextView = value;
        }
        // universal functions
        public override void Clear()
        {
            text = new char[0];
        }
        #endregion
    }
}