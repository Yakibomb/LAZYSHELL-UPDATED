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
    public class DTE : Element
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public override int Index { get { return index; } set { index = value; } }
        // non-serialized variables
        [NonSerialized()]
        private TextHelper textHelper;
        // class variables
        private char[] text;
        private int offset;
        private int pointer;
        private bool error = false;
        private int caretPosByteView;
        private int caretPosTextView;
        private int reference;
        private int parent;
        private int position;
        // public accessors
        public char[] Text { get { return text; } }
        public int Length { get { return text.Length; } }
        public int Offset { get { return offset; } set { offset = value; } }
        public int Pointer
        {
            get
            {
                return Bits.GetShort(rom, 0x249000 + index * 2);
            }
        }
        // external managers
        public int Reference { get { return reference; } set { reference = value; } }
        public int Parent { get { return parent; } set { parent = value; } }
        public int Position { get { return position; } set { position = value; } }
        // constructor
        public DTE(int index)
        {
            this.index = index;
            this.textHelper = TextHelper.Instance;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            text = GetText();
        }
        public void Assemble(ref int pointer)
        {
            Bits.SetShort(rom, 0x249000 + index * 2, pointer);
            int offset = pointer + 0x249100;
            char[] raw = new char[text.Length + 1]; text.CopyTo(raw, 0);
            Bits.SetChars(rom, offset, raw);
            pointer += raw.Length;
        }
        // class functions
        private char[] GetText()
        {
            this.pointer = Bits.GetShort(rom, 0x249000 + index * 2); // from pointer table
            this.offset = pointer + 0x249100;
            //
            int counter = this.offset;
            int length = 0;
            int letter = -1;
            while (letter != 0x00 && letter != 0x06)
            {
                letter = rom[counter];
                if (letter == 0x0B || letter == 0x0D || letter == 0x1C)
                {
                    length++;
                    counter++;
                }
                length++;
                counter++;
            }
            length--;
            char[] text = new char[length];
            for (int i = 0; i < length; i++)
                text[i] = (char)rom[this.offset + i];
            return text;
        }
        public string GetText(bool byteView)
        {
            if (!error)
                return new string(textHelper.Decode(text, byteView, null));
            else
                return new string(text);
        }
        public string GetStub(bool byteView)
        {
            string text = GetText(byteView);
            if (text.Length > 40)
            {
                text = text.Substring(0, 37);
                return text + "...";
            }
            else
                return text;
        }
        public int GetCaretPosition(bool byteView)
        {
            if (byteView)
                return caretPosByteView;
            else
                return caretPosTextView;
        }
        public bool SetText(string value, bool byteView)
        {
            this.text = textHelper.Encode(value.ToCharArray(), byteView, null);
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
    }
}
