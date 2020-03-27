using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    [Serializable()]
    public sealed class TextHelper
    {
        // static variables
        static TextHelper instance = null;
        static readonly object padlock = new object();
        private string[] keystrokes = Lists.Keystrokes;
        // class variables
        private bool error = false;
        // accessors
        public bool Error { get { return this.error; } }
        public static TextHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new TextHelper();
                    return instance;
                }
            }
        }
        // textView variables
        private const string code00 = "endInput";
        private const string code01 = "newLine";
        private const string code02 = "newLineInput";
        private const string code03 = "newPageInput";
        private const string code04 = "newPage";
        private const string code05 = "pauseInput";
        private const string code06 = "end";
        private const string code07 = "option";
        private const string code08 = "  ";
        private const string code09 = "   ";
        private const string code0A = "    ";
        private const string code0C = "delay";
        private const string code0D = "delay...";
        private const string code1A = "memItem";
        private const string code1C = "memNum...";
        private const string code24 = "heart";
        private const string code25 = "note";
        private const string code2A = "bullet";
        private const string code2B = "bullets";
        private const string code3B = "cornerLeft";
        private const string code3C = "cornerRight";
        private const string code3D = "cornerLeftBold";
        private const string code3E = "cornerRightBold";
        private const string code92 = "ellipsis";
        private const string code97 = "arrowUp";
        private const string code98 = "arrowRight";
        private const string code99 = "arrowLeft";
        // constructor
        TextHelper()
        {
        }
        // public functions
        public char[] Decode(char[] text, bool byteView, string[] tables)
        {
            List<char> letters = new List<char>();
            bool lastBrace = true;
            for (int i = 0; i < text.Length; i++) // For every character of text
            {
                // skip if out of bounds
                if (text[i] >= keystrokes.Length)
                    continue;
                // if a table
                if (tables != null && text[i] >= 0x0E && text[i] <= 0x19)
                {
                    letters.AddRange(tables[text[i] - 0x0E].ToCharArray());
                    continue;
                }
                #region BYTE VIEW
                if (byteView) // We are decoding to numbers
                {
                    if (keystrokes[text[i]] == "") // Is encoded character
                    {
                        switch ((byte)text[i])
                        {
                            case 0x08: letters.AddRange(code08.ToCharArray()); break;
                            case 0x09: letters.AddRange(code09.ToCharArray()); break;
                            case 0x0A: letters.AddRange(code0A.ToCharArray()); break;
                            case 0x0B: // number of spaces
                                if (text.Length > i + 1)
                                {
                                    i++;
                                    for (int a = 0; a < text[i]; a++)
                                        letters.Add(' ');
                                }
                                break;
                            case 0x0D: goto case 0xFF;
                            case 0x1C: goto case 0xFF;
                            case 0xFF:
                                letters.Add('[');
                                letters.AddRange(((byte)text[i]).ToString());
                                letters.Add(']');
                                if (text.Length > i + 1)
                                {
                                    i++;
                                    goto default;
                                }
                                break;
                            default:
                                letters.Add('[');
                                letters.AddRange(((byte)text[i]).ToString());
                                letters.Add(']');
                                break;
                        }
                    }
                    else // Not encoded character
                        letters.Add(Convert.ToChar(keystrokes[text[i]]));
                }
                #endregion
                #region TEXT VIEW
                else // We are decoding to words
                {
                    if (keystrokes[text[i]] == "") // Current byte is encoded
                    {
                        lastBrace = true;
                        switch ((byte)text[i])
                        {
                            case 0x00: letters.Add('['); letters.AddRange(code00); break;
                            case 0x01: letters.Add('['); letters.AddRange(code01); break;
                            case 0x02: letters.Add('['); letters.AddRange(code02); break;
                            case 0x03: letters.Add('['); letters.AddRange(code03); break;
                            case 0x04: letters.Add('['); letters.AddRange(code04); break;
                            case 0x05: letters.Add('['); letters.AddRange(code05); break;
                            case 0x06: letters.Add('['); letters.AddRange(code06); break;
                            case 0x07: letters.Add('['); letters.AddRange(code07); break;
                            case 0x08: letters.AddRange(code08); lastBrace = false; break;
                            case 0x09: letters.AddRange(code09); lastBrace = false; break;
                            case 0x0A: letters.AddRange(code0A); lastBrace = false; break;
                            case 0x0B:
                                i++;
                                for (int a = 0; a < text[i]; a++)
                                    letters.Add(' ');
                                lastBrace = false;
                                break;
                            case 0x0C: letters.Add('['); letters.AddRange(code0C); break;
                            case 0x0D:
                                letters.Add('[');
                                letters.AddRange(code0D);
                                letters.Add(']');
                                letters.Add('[');
                                letters.Add(' ');
                                i++;
                                letters.AddRange(((byte)text[i]).ToString());
                                break;
                            case 0x1A: letters.Add('['); letters.AddRange(code1A); break;
                            case 0x1C:
                                letters.Add('[');
                                letters.AddRange(code1C);
                                letters.Add(']');
                                letters.Add('[');
                                letters.Add(' ');
                                i++;
                                letters.AddRange(((byte)text[i]).ToString());
                                break;
                            case 0x24: letters.Add('['); letters.AddRange(code24); break;
                            case 0x25: letters.Add('['); letters.AddRange(code25); break;
                            case 0x2A: letters.Add('['); letters.AddRange(code2A); break;
                            case 0x2B: letters.Add('['); letters.AddRange(code2B); break;
                            case 0x3B: letters.Add('['); letters.AddRange(code3B); break;
                            case 0x3C: letters.Add('['); letters.AddRange(code3C); break;
                            case 0x3D: letters.Add('['); letters.AddRange(code3D); break;
                            case 0x3E: letters.Add('['); letters.AddRange(code3E); break;
                            case 0x92: letters.Add('['); letters.AddRange(code92); break;
                            case 0x97: letters.Add('['); letters.AddRange(code97); break;
                            case 0x98: letters.Add('['); letters.AddRange(code98); break;
                            case 0x99: letters.Add('['); letters.AddRange(code99); break;
                            default:
                                letters.Add('[');
                                letters.AddRange("ERROR: ");
                                letters.AddRange(((byte)text[i]).ToString());
                                break;
                        }
                        if (lastBrace)
                            letters.Add(']');
                    }
                    else
                        letters.Add(Convert.ToChar(keystrokes[text[i]]));
                }
                #endregion
            }
            return letters.ToArray();
        }
        public char[] Encode(char[] text, bool byteView, string[] tables)
        {
            bool openQuote = true;
            List<char> letters = new List<char>();
            char[] backup = text;
            for (int i = 0; i < text.Length; i++)
            {
                // first see if substring of a table
                if (tables != null)
                {
                    bool skip = false;
                    for (int a = 0; a < 12; a++)
                    {
                        if (tables[a].Length > 0 && FindSubstring(text, i, tables[a]))
                        {
                            letters.Add((char)(a + 0x0E));
                            i += tables[a].Length - 1;
                            skip = true;
                            break;
                        }
                    }
                    // if substring found, cancel operation
                    if (skip) continue;
                }
                #region BYTE VIEW
                if (byteView)
                {
                    // check for byte characters, multiple spaces, or open quotes
                    if (text[i] == '[' || text[i] == '\x20' || text[i] == '\x22')
                    {
                        switch (text[i])
                        {
                            case '[':
                                if (text[i + 1] != ']') // if character at least 1-digit
                                {
                                    char digitOne = (char)(text[i + 1] - 0x30);
                                    if (text.Length > i + 2 && text[i + 2] != ']') // if character at least 2-digit
                                    {
                                        char digitTwo = (char)(text[i + 2] - 0x30);
                                        if (text.Length > i + 3 && text[i + 3] != ']') // if character at least 3-digit
                                        {
                                            char digitThree = (char)(text[i + 3] - 0x30);
                                            letters.Add((char)((digitOne * 100) + (digitTwo * 10) + digitThree));
                                            i += 4;
                                            break;
                                        }
                                        else // 2 digits
                                        {
                                            letters.Add((char)((digitOne * 10) + digitTwo));
                                            i += 3;
                                            break;
                                        }
                                    }
                                    letters.Add(digitOne);
                                    i += 2;
                                    break;
                                }
                                break; // none
                            case '\x20':
                                if (FindSubstring(text, i, "     ")) // code 0B
                                {
                                    int a = 0;
                                    while (i + a < text.Length && text[i + a] == ' ') a++; //; count spaces
                                    letters.Add('\x0B'); // space encode byte
                                    letters.Add((char)a); // space count
                                    i += a - 1; // allign array
                                    break;
                                }
                                if (FindSubstring(text, i, code0A))
                                {
                                    letters.Add('\x0A');
                                    i += code0A.Length - 1;
                                    break;
                                }
                                if (FindSubstring(text, i, code09))
                                {
                                    letters.Add('\x09');
                                    i += code09.Length - 1;
                                    break;
                                }
                                if (FindSubstring(text, i, code08))
                                {
                                    letters.Add('\x08');
                                    i += code08.Length - 1;
                                    break;
                                }
                                letters.Add('\x20');
                                break;
                            case '\x22':
                                if (openQuote)
                                {
                                    letters.Add('\x22');
                                    openQuote = false;
                                }
                                else
                                {
                                    letters.Add('\x23');
                                    openQuote = true;
                                }
                                break;
                            default: letters.Add('\x20'); break;
                        }
                    }
                    else
                        letters.Add(StringIndex(keystrokes, text[i]));
                }
                #endregion
                #region TEXT VIEW
                else
                {
                    if (text[i] == '[' || text[i] == '\x20' || text[i] == '\x22')
                    {
                        switch (text[i])
                        {
                            case '[':
                                i++;
                                int length = 0;
                                while (length < text.Length - i && text[i + length] != ']')
                                    length++;
                                char[] code = new char[length];
                                for (int a = 0; a < length; a++)
                                    code[a] = text[i + a];
                                switch (new string(code))
                                {
                                    case code00: letters.Add('\x00'); break;
                                    case code01: letters.Add('\x01'); break;
                                    case code02: letters.Add('\x02'); break;
                                    case code03: letters.Add('\x03'); break;
                                    case code04: letters.Add('\x04'); break;
                                    case code05: letters.Add('\x05'); break;
                                    case code06: letters.Add('\x06'); break;
                                    case code07: letters.Add('\x07'); break;
                                    case code0C: letters.Add('\x0C'); break;
                                    case code0D:
                                        letters.Add('\x0D');
                                        i += length + 1;
                                        if (text[i + 1] == ' ')
                                            i++; // take care of space
                                        if (text[i + 1] != ']') // would make 1
                                        {
                                            char digitOne = (char)(text[i + 1] - 0x30);
                                            if (text.Length > i + 2 && text[i + 2] != ']') // would make 2 digits
                                            {
                                                char digitTwo = (char)(text[i + 2] - 0x30);
                                                if (text.Length > i + 3 && text[i + 3] != ']') // would make 3 digits
                                                {
                                                    char digitThree = (char)(text[i + 3] - 0x30);
                                                    letters.Add((char)((digitOne * 100) + (digitTwo * 10) + digitThree));
                                                    length = 4;
                                                    break;
                                                }
                                                else // 2 digits
                                                {
                                                    letters.Add((char)((digitOne * 10) + digitTwo));
                                                    length = 3;
                                                    break;
                                                }
                                            }
                                            letters.Add((char)(digitOne));
                                            length = 2;
                                            break;
                                        }
                                        break;
                                    case code1A: letters.Add('\x1A'); break;
                                    case code1C:
                                        letters.Add('\x1C');
                                        i += length + 1;
                                        if (text[i + 1] == ' ')
                                            i++; // take care of space
                                        if (text[i + 1] != ']') // would make 1
                                        {
                                            char digitOne = (char)(text[i + 1] - 0x30);
                                            if (text.Length > i + 2 && text[i + 2] != ']') // would make 2 digits
                                            {
                                                char digitTwo = (char)(text[i + 2] - 0x30);
                                                if (text.Length > i + 3 && text[i + 3] != ']') // would make 3 digits
                                                {
                                                    char digitThree = (char)(text[i + 3] - 0x30);
                                                    letters.Add((char)((digitOne * 100) + (digitTwo * 10) + digitThree));
                                                    length = 4;
                                                    break;
                                                }
                                                else // 2 digits
                                                {
                                                    letters.Add((char)((digitOne * 10) + digitTwo));
                                                    length = 3;
                                                    break;
                                                }
                                            }
                                            letters.Add((char)(digitOne));
                                            length = 2;
                                            break;
                                        }
                                        break;
                                    case code24: letters.Add('\x24'); break;
                                    case code25: letters.Add('\x25'); break;
                                    case code2A: letters.Add('\x2A'); break;
                                    case code2B: letters.Add('\x2B'); break;
                                    case code3B: letters.Add('\x3B'); break;
                                    case code3C: letters.Add('\x3C'); break;
                                    case code3D: letters.Add('\x3D'); break;
                                    case code3E: letters.Add('\x3E'); break;
                                    case code92: letters.Add('\x92'); break;
                                    case code97: letters.Add('\x97'); break;
                                    case code98: letters.Add('\x98'); break;
                                    case code99: letters.Add('\x99'); break;
                                    default: break;
                                }
                                i += length;
                                break;
                            case '\x20':
                                if (FindSubstring(text, i, "     ")) // code 0B
                                {
                                    int a = 0;
                                    while (i + a < text.Length && text[i + a] == ' ')
                                        a++;
                                    letters.Add('\x0B');
                                    letters.Add((char)a);
                                    i += a - 1;
                                    break;
                                }
                                if (FindSubstring(text, i, code0A))
                                {
                                    letters.Add('\x0A');
                                    i += code0A.Length - 1;
                                    break;
                                }
                                if (FindSubstring(text, i, code09))
                                {
                                    letters.Add('\x09');
                                    i += code09.Length - 1;
                                    break;
                                }
                                if (FindSubstring(text, i, code08))
                                {
                                    letters.Add('\x08');
                                    i += code08.Length - 1;
                                    break;
                                }
                                letters.Add('\x20');
                                break;
                            case '\x22': // handles user input quotes
                                if (openQuote)
                                {
                                    letters.Add('\x22');
                                    openQuote = false;
                                }
                                else
                                {
                                    letters.Add('\x23');
                                    openQuote = true;
                                }
                                break;
                            default: letters.Add('\x20'); break;
                        }
                    }
                    else
                        letters.Add(StringIndex(keystrokes, text[i]));
                }
                #endregion
            }
            char[] encoded = new char[letters.Count];
            try
            {
                letters.CopyTo(encoded);
            }
            catch
            {
                error = true;
                return backup;
            }
            if (!VerifyText(encoded))
            {
                error = true;
                return backup;
            }
            error = false;
            return encoded;
        }
        public bool VerifySymbols(char[] symbols, bool byteView)
        {
            bool symbol = false, found = false;
            try
            {
                for (int i = 0; i < symbols.Length; i++)
                {
                    if (symbols[i] == '[')
                    {
                        if (symbols[i + 1] >= '\x30' && symbols[i + 1] <= '\x39')
                            symbol = true;
                        else
                            symbol = false;
                        found = true;
                    }
                    if (byteView != symbol && found)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidSymbol(char symbol)
        {
            if (symbol >= '\x00' && symbol <= '\x1C') return true;
            if (symbol >= '\x20' && symbol <= '\x5A') return true;
            if (symbol >= '\x5C' && symbol <= '\x9F') return true;
            foreach (string keystroke in keystrokes)
                if (keystroke != "" && Convert.ToChar(keystroke) == symbol) return true;
            return false;
        }
        // class functions
        private bool FindSubstring(char[] text, int index, string substring)
        {
            if (text.Length < substring.Length + index)
                return false;
            for (int i = 0; i < substring.Length; i++)
            {
                if (substring[i] != text[i + index])
                    return false;
            }
            return true;
        }
        private char StringIndex(string[] strings, char character)
        {
            for (char i = (char)0; i < strings.Length; i++)
            {
                if (strings[i] == character.ToString())
                    return i;
            }
            return character;
        }
        private bool VerifyText(char[] text)
        {
            bool openBracket = false;
            if (text.Length == 0)
                return true;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '[' && text[i] != ']' && IsValidSymbol(text[i]) == false)
                    if (i == 0 || (text[i - 1] != '\x0B' && text[i - 1] != '\x0D'))
                        return false;
                if (text[i] == '[')
                {
                    if (openBracket == true)
                        return false;
                    openBracket = true;
                }
                if (text[i] == ']')
                {
                    if (openBracket == false)
                        return false;
                    openBracket = false;
                }
            }
            if (openBracket == false)
                return true;
            return false;
        }
    }
}