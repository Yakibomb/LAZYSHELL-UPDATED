using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public class BattleDialogue : Element
    {
        #region variables
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // nonserialized variables
        [NonSerialized()]
        private TextHelperReduced textHelperReduced;
        // variables
        private int index;
        private char[] text;
        private int offset;
        private bool error = false;
        private int caretPosByteView;
        private int caretPosTextView;
        private int pointerOffset;
        private int baseOffset;
        #endregion
        #region accessors
        // accessors        
        public override int Index { get { return index; } set { index = value; } }
        public int Offset { get { return offset; } set { offset = value; } }
        public int Length { get { return text.Length; } }
        public char[] Text { get { return text; } }
        #endregion
        // constructor
        public BattleDialogue(int index, int pointerOffset, int baseOffset)
        {
            this.index = index;
            this.pointerOffset = pointerOffset;
            this.baseOffset = baseOffset;
            this.textHelperReduced = TextHelperReduced.Instance;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            text = GetText();
        }
        public void Assemble(ref int offset)
        {
            Bits.SetShort(rom, pointerOffset + index * 2, offset);
            this.offset = offset + baseOffset;
            //
            Bits.SetChars(rom, this.offset, text);
            offset += text.Length;
        }
        // class functions
        private char[] GetText()
        {
            this.offset = Bits.GetShort(rom, pointerOffset + index * 2) + baseOffset;
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
            char[] text = new char[length];
            for (int i = 0; i < length; i++)
                text[i] = (char)rom[this.offset + i];
            return text;
        }
        public string GetText(bool byteView)
        {
            if (!error)
                return new string(textHelperReduced.Decode(text, byteView, 0, Lists.Keystrokes));
            else
                return new string(text);
        }
        public string GetStub()
        {
            string text = this.GetText(true);
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
            this.text = textHelperReduced.Encode(value.ToCharArray(), byteView, 0, Lists.Keystrokes);
            this.error = textHelperReduced.Error;
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
        public override string ToString()
        {
            return "[" + index.ToString("d3") + "]  " + GetText(true);
        }
    }
}