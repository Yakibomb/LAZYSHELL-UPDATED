using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LAZYSHELL.Properties;
using LAZYSHELL.ScriptsEditor.Commands;
using LAZYSHELL.Undo;

namespace LAZYSHELL
{
    public partial class SPCEditor
    {
        #region Variables
        private Overlay overlay = new Overlay();
        private Score score = new Score();
        private List<Staff> staffs
        {
            get
            {
                return score.Staffs;
            }
            set
            {
                score.Staffs = value;
            }
        }
        private ToolStripButton insertObject;
        private int mouseOverStaff = -1;
        private Pitch mouseOverPitch = Pitch.NULL;
        private int mouseOverLine = -1;
        private int mouseOverOctave = -1;
        private int mouseDownStaff = -1;
        private int mouseOverNote = -1;
        private int mouseDownNote = -1;
        private Point mousePosition = new Point(-1, -1);
        private Bitmap clefF = Resources.clefF;
        private Bitmap clefG = Resources.clefG;
        private Bitmap insert = Resources.insert;
        private int timeBeats
        {
            get
            {
                if (scoreWriter.Checked)
                    return (int)timeBeatsSW.Value;
                else
                    return (int)timeBeatsSV.Value;
            }
            set
            {
                if (scoreWriter.Checked)
                    timeBeatsSW.Value = value;
                else
                    timeBeatsSV.Value = value;
            }
        }
        private int timeValue
        {
            get
            {
                if (scoreWriter.Checked)
                    return (int)timeValueSW.Value;
                else
                    return (int)timeValueSV.Value;
            }
            set
            {
                if (scoreWriter.Checked)
                    timeValueSW.Value = value;
                else
                    timeValueSV.Value = value;
            }
        }
        private int staffHeight
        {
            get
            {
                if (scoreWriter.Checked)
                    return (int)staffHeightSW.Value;
                else
                    return (int)staffHeightSV.Value;
            }
            set
            {
                if (scoreWriter.Checked)
                    staffHeightSW.Value = value;
                else
                    staffHeightSV.Value = value;
            }
        }
        private int noteSpacing
        {
            get
            {
                if (scoreWriter.Checked)
                    return (int)noteSpacingSW.Value;
                else
                    return (int)noteSpacingSV.Value;
            }
            set
            {
                if (scoreWriter.Checked)
                    noteSpacingSW.Value = value;
                else
                    noteSpacingSV.Value = value;
            }
        }
        private Key key
        {
            get
            {
                if (scoreWriter.Checked)
                    return (Key)keySW.SelectedIndex;
                else
                    return (Key)keySV.SelectedIndex;
            }
            set
            {
                if (scoreWriter.Checked)
                    keySW.SelectedIndex = (int)value;
                else
                    keySV.SelectedIndex = (int)value;
            }
        }
        private List<object> notes
        {
            get
            {
                return staffs[mouseDownStaff].Notes;
            }
            set
            {
                staffs[mouseDownStaff].Notes = value;
            }
        }
        private List<object> copyBufferSW;
        private CopyNotes copyBufferSV;
        #endregion
        #region Functions
        private void RefreshScoreWriter()
        {
        }
        private void RefreshStaff()
        {
            if (staffs.Count == 0 || mouseDownStaff < 0)
                return;
            clef.SelectedIndex = staffs[mouseDownStaff].Clef;
            keySW.SelectedIndex = (int)score.Key;
            //
            foreach (ToolStripItem item in wToolStripMain.Items)
                item.Enabled = true;
            wToolStripNote.Enabled = true;
            wToolStripAct.Enabled = true;
        }
        private List<SPCCommand>[] StaffsToChannels()
        {
            List<SPCCommand>[] channels = new List<SPCCommand>[8];
            for (int c = 0; c < 8 && c < staffs.Count; c++)
            {
                int thisOctave = -1;
                channels[c] = new List<SPCCommand>();
                foreach (object item in staffs[c].Notes)
                {
                    Note note = (Note)item;
                    if (note.Octave == thisOctave + 1)
                    {
                        channels[c].Add(new SPCCommand(new byte[] { 0xC4 }, spc, c));
                        thisOctave++;
                    }
                    else if (note.Octave == thisOctave - 1)
                    {
                        channels[c].Add(new SPCCommand(new byte[] { 0xC5 }, spc, c));
                        thisOctave--;
                    }
                    else if (note.Octave != thisOctave)
                    {
                        channels[c].Add(new SPCCommand(new byte[] { 0xC6, (byte)note.Octave }, spc, c));
                        thisOctave = note.Octave;
                    }
                    channels[c].Add(note.Command);
                }
            }
            return channels;
        }
        private Bitmap GetStem(int ticks, int pitch, bool hilite, Color color)
        {
            Bitmap stem = null;
            if (ticks >= 192) { }
            else if (ticks >= 144) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 96) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 72) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 64) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 48) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 36) { stem = pitch >= 59 ? Icons.noteStemDown8th : Icons.noteStemUp8th; }
            else if (ticks >= 32) { stem = pitch >= 59 ? Icons.noteStemDown : Icons.noteStemUp; }
            else if (ticks >= 24) { stem = pitch >= 59 ? Icons.noteStemDown8th : Icons.noteStemUp8th; }
            else if (ticks >= 18) { stem = pitch >= 59 ? Icons.noteStemDown16th : Icons.noteStemUp16th; }
            else if (ticks >= 16) { stem = pitch >= 59 ? Icons.noteStemDown8th : Icons.noteStemUp8th; }
            else if (ticks >= 12) { stem = stem = pitch >= 59 ? Icons.noteStemDown16th : Icons.noteStemUp16th; }
            else if (ticks >= 9) { stem = pitch >= 59 ? Icons.noteStemDown32nd : Icons.noteStemUp32nd; }
            else if (ticks >= 8) { stem = pitch >= 59 ? Icons.noteStemDown16th : Icons.noteStemUp16th; }
            else if (ticks >= 6) { stem = pitch >= 59 ? Icons.noteStemDown32nd : Icons.noteStemUp32nd; }
            else if (ticks >= 4) { stem = pitch >= 59 ? Icons.noteStemDown32nd : Icons.noteStemUp32nd; }
            else if (ticks >= 3) { stem = pitch >= 59 ? Icons.noteStemDown64th : Icons.noteStemUp64th; }
            else if (ticks >= 2) { stem = pitch >= 59 ? Icons.noteStemDown64th : Icons.noteStemUp64th; }
            else if (ticks >= 1) { stem = Icons.note64th; }
            else { stem = Icons.note64th; }
            if (hilite && stem != null)
                stem = Do.Fill(stem, color);
            return stem;
        }
        private Bitmap GetHead(int ticks, int pitch, bool hilite, Color color)
        {
            Bitmap head;
            if (ticks >= 192) { head = Icons.noteEmpty; }
            else if (ticks >= 144) { head = Icons.noteEmptyDotted; }
            else if (ticks >= 96) { head = Icons.noteEmpty; }
            else if (ticks >= 72) { head = Icons.noteHeadDotted; }
            else if (ticks >= 64) { head = Icons.noteEmptyTriplet; }
            else if (ticks >= 48) { head = Icons.noteHead; }
            else if (ticks >= 36) { head = Icons.noteHeadDotted; }
            else if (ticks >= 32) { head = Icons.noteHeadTriplet; }
            else if (ticks >= 24) { head = Icons.noteHead; }
            else if (ticks >= 18) { head = Icons.noteHeadDotted; }
            else if (ticks >= 16) { head = Icons.noteHeadTriplet; }
            else if (ticks >= 12) { head = Icons.noteHead; }
            else if (ticks >= 9) { head = Icons.noteHeadDotted; }
            else if (ticks >= 8) { head = Icons.noteHeadTriplet; }
            else if (ticks >= 6) { head = Icons.noteHead; }
            else if (ticks >= 4) { head = Icons.noteHeadTriplet; }
            else if (ticks >= 3) { head = Icons.noteHead; }
            else if (ticks >= 2) { head = Icons.noteHeadTriplet; }
            else if (ticks >= 1) { head = Icons.noteHead; }
            else { head = Icons.noteHead; }
            if (hilite)
                head = Do.Fill(head, color);
            return head;
        }
        private Bitmap GetRest(int ticks, bool hilite, Color color)
        {
            Bitmap rest;
            if (ticks >= 192) { rest = Icons.restWhole; }
            else if (ticks >= 144) { rest = Icons.restHalfDotted; }
            else if (ticks >= 96) { rest = Icons.restHalf; }
            else if (ticks >= 72) { rest = Icons.restQuarterDotted; }
            else if (ticks >= 64) { rest = Icons.restHalfTriplet; }
            else if (ticks >= 48) { rest = Icons.restQuarter; }
            else if (ticks >= 36) { rest = Icons.rest8thDotted; }
            else if (ticks >= 32) { rest = Icons.restQuarterTriplet; }
            else if (ticks >= 24) { rest = Icons.rest8th; }
            else if (ticks >= 18) { rest = Icons.rest16thDotted; }
            else if (ticks >= 16) { rest = Icons.rest8thTriplet; }
            else if (ticks >= 12) { rest = Icons.rest16th; }
            else if (ticks >= 9) { rest = Icons.rest32ndDotted; }
            else if (ticks >= 8) { rest = Icons.rest16thTriplet; }
            else if (ticks >= 6) { rest = Icons.rest32nd; }
            else if (ticks >= 4) { rest = Icons.rest32ndTriplet; }
            else if (ticks >= 3) { rest = Icons.rest64th; }
            else if (ticks >= 2) { rest = Icons.rest64thTriplet; }
            else if (ticks >= 1) { rest = Icons.rest64th; }
            else { rest = Icons.rest64th; }
            if (hilite)
                rest = Do.Fill(rest, color);
            return rest;
        }
        private int GetStaffWidth(int staffIndex)
        {
            int staffWidth = 0;
            if (scoreWriter.Checked)
                foreach (object item in staffs[staffIndex].Notes)
                {
                    if (item.GetType() == typeof(Note))
                        staffWidth += (int)((double)noteSpacing / 100.0 * ((Note)item).Ticks);
                }
            else
                foreach (Note note in spc.Notes[staffIndex])
                    staffWidth += (int)((double)noteSpacing / 100.0 * note.Ticks);
            return staffWidth;
        }
        private int GetKeyWidth(Key key)
        {
            int width = 0;
            if (GetKeySignature(key, Pitch.F) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.C) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.G) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.D) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.A) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.E) == Accidental.Sharp) { width += 4; }
            if (GetKeySignature(key, Pitch.B) == Accidental.Sharp) { width += 4; }
            //
            if (GetKeySignature(key, Pitch.B) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.E) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.A) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.D) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.G) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.C) == Accidental.Flat) { width += 4; }
            if (GetKeySignature(key, Pitch.F) == Accidental.Flat) { width += 4; }
            return width;
        }
        private void DrawNote(Graphics g, Note note, Note lastNote, Note lastItem, int x,
            int staffHeight, int clef, Key key, int staffIndex, bool hilite, bool select)
        {
            int y = staffIndex * staffHeight;
            int middle = staffHeight / 2;
            int yCoord = y + middle;
            int clefOffset = 0;
            if (clef == 0)
                clefOffset = -4;
            else if (clef == 1)
                clefOffset = 4;
            Bitmap stem = null;
            Bitmap head = null;
            Bitmap rest = null;
            Color color = Color.Black;
            if (hilite)
                color = Color.Red;
            else if (select)
            {
                color = Color.Blue;
                hilite = true;
            }
            if (!note.Rest && !note.Tie)
            {
                int pitch = note.Octave * 12 + (int)note.Pitch;
                int ticks = note.Ticks;
                yCoord = y + middle + note.Y(key) + clefOffset - 5;
                if (!note.Percussive)
                {
                    head = GetHead(ticks, pitch, hilite, color);
                    stem = GetStem(ticks, pitch, hilite, color);
                    // draw extra lines
                    DrawOutsideLines(g, Pens.Gray, x + 7, y, middle, clef, note.Octave, note.Pitch, note.Line);
                    if (stem != null)
                        g.DrawImage(stem, x, pitch < 59 ? yCoord : yCoord + 12);
                    g.DrawImage(head, x, yCoord);
                    Accidental accidental = GetAccidental(key, note.Pitch);
                    if (accidental == Accidental.Sharp)
                        g.DrawImage(hilite ? Do.Fill(Icons.sharp, color) : Icons.sharp, x - 2, yCoord + 4);
                    else if (accidental == Accidental.Flat)
                        g.DrawImage(hilite ? Do.Fill(Icons.flat, color) : Icons.flat, x - 2, yCoord + 4);
                    else if (accidental == Accidental.Natural)
                        g.DrawImage(hilite ? Do.Fill(Icons.natural, color) : Icons.natural, x - 2, yCoord + 4);
                }
                else
                    g.DrawImage(hilite ? Do.Fill(Icons.notePercussion, color) : Icons.notePercussion, x, yCoord);
            }
            else if (note.Rest)
            {
                int ticks = note.Ticks;
                yCoord = y + middle - 4 - 5;
                rest = GetRest(ticks, hilite, color);
                if (!scoreWriter.Checked && showRests.Checked)
                    g.DrawImage(rest, x, yCoord);
                else if (scoreWriter.Checked)
                    g.DrawImage(rest, x, yCoord);
            }
            else if (note.Tie && lastNote != null && lastItem != null)
            {
                int pitch = lastNote.Octave * 12 + (int)lastNote.Pitch;
                int ticks = note.Ticks;
                yCoord = y + middle + lastNote.Y(key) + clefOffset - 5;
                if (!lastNote.Percussive)
                {
                    head = GetHead(ticks, pitch, hilite, color);
                    stem = GetStem(ticks, pitch, hilite, color);
                    Bitmap sharp = Icons.sharp;
                    // draw extra lines
                    DrawOutsideLines(g, Pens.Gray, x + 7, y, middle, clef, lastNote.Octave, lastNote.Pitch, lastNote.Line);
                    if (stem != null)
                        g.DrawImage(stem, x, pitch < 59 ? yCoord : yCoord + 12);
                    g.DrawImage(head, x, yCoord);
                    Accidental accidental = GetAccidental(key, lastNote.Pitch);
                    if (accidental == Accidental.Sharp)
                        g.DrawImage(hilite ? Do.Fill(Icons.sharp, color) : Icons.sharp, x - 2, yCoord + 4);
                    else if (accidental == Accidental.Flat)
                        g.DrawImage(hilite ? Do.Fill(Icons.flat, color) : Icons.flat, x - 2, yCoord + 4);
                    else if (accidental == Accidental.Natural)
                        g.DrawImage(hilite ? Do.Fill(Icons.natural, color) : Icons.natural, x - 2, yCoord + 4);
                }
                else
                    g.DrawImage(hilite ? Do.Fill(Icons.notePercussion, color) : Icons.notePercussion, x, yCoord);
                // draw tie, must stretch/shrink according to notespacing
                double ratio = (double)noteSpacing / 100.0;
                Rectangle src = new Rectangle(0, 0, 16, 16);
                Rectangle dst = new Rectangle(
                    x - (int)(lastNote.Ticks * ratio) + 8,
                    pitch < 59 ? yCoord + 8 : yCoord + 2,
                    (int)((double)lastNote.Ticks * ratio), 16);
                Bitmap tie = pitch < 59 ? Icons.tieUnder : Icons.tieOver;
                g.DrawImage(tie, dst, src, GraphicsUnit.Pixel);
            }
        }
        private void DrawBarsLines(Graphics g, int clef, Key key, int staffIndex, int xOffset, bool drawClefs, int staffWidth)
        {
            // set variables
            int width = (int)g.ClipBounds.Width;
            int height = staffHeight;
            // draw dotted separators
            Pen pen = new Pen(Color.Gray);
            pen.DashStyle = DashStyle.Dot;
            pen.Alignment = PenAlignment.Center;
            g.DrawLine(pen,
                0, staffIndex * height + 4,
                width, staffIndex * height + 4);
            g.DrawLine(pen,
                0, staffIndex * height + height - 4,
                width, staffIndex * height + height - 4);
            pen.DashStyle = DashStyle.Solid;
            // draw start bars
            int x = xOffset;
            int x_ = xOffset;
            int y = staffIndex * height;
            int middle = height / 2;
            pen.Width = 4;
            g.DrawLine(pen, x, y + middle - 16, x, y + middle + 16);
            pen.Width = 1;
            g.DrawLine(pen, x + 6, y + middle - 16, x + 6, y + middle + 16);
            // draw clefs
            if (drawClefs)
            {
                if (clef == 0)
                    g.DrawImage(clefG, x + 16, y + middle - 24);
                else if (clef == 1)
                    g.DrawImage(clefF, x + 16, y + middle - 24);
            }
            // get X offset from key signature width
            int keyWidth = GetKeyWidth(key);
            x += keyWidth;
            // draw ledger lines
            int measureTop = (staffIndex * height) + (height / 2) - 16;
            int measureLength = (int)((double)timeBeats / (double)timeValue * 192.0);
            measureLength = (int)((double)noteSpacing / 100.0 * (double)measureLength);
            if (staffWidth % measureLength == 0)
                staffWidth = staffWidth / measureLength * measureLength;
            else
                staffWidth = staffWidth / measureLength * measureLength + measureLength;
            int measureLeft = measureLength + 64 + x;
            int maxWidth = Math.Max(staffWidth + x + 64 + 8, measureLeft + 8);
            g.DrawLine(pen, 0, y + middle - 16, maxWidth, y + middle - 16);
            g.DrawLine(pen, 0, y + middle - 8, maxWidth, y + middle - 8);
            g.DrawLine(pen, 0, y + middle, maxWidth, y + middle);
            g.DrawLine(pen, 0, y + middle + 8, maxWidth, y + middle + 8);
            g.DrawLine(pen, 0, y + middle + 16, maxWidth, y + middle + 16);
            // draw measure bar lines
            while (measureLeft < width)
            {
                if (measureLeft - x - 64 >= staffWidth)
                {
                    // draw end bars
                    pen.Width = 1; g.DrawLine(pen, measureLeft, measureTop, measureLeft, measureTop + 32);
                    pen.Width = 4; g.DrawLine(pen, measureLeft + 6, measureTop, measureLeft + 6, measureTop + 32);
                    break;
                }
                else
                {
                    g.DrawLine(Pens.Gray, measureLeft, measureTop, measureLeft, measureTop + 32);
                    measureLeft += measureLength;
                }
            }
            // draw time sig
            Font font = new Font("Times New Roman", 18, FontStyle.Bold);
            if (timeBeats >= 10)
                g.DrawString(timeBeats.ToString(), font, Brushes.Black, 40 + x - 6, measureTop - 4);
            else
                g.DrawString(timeBeats.ToString(), font, Brushes.Black, 40 + x, measureTop - 4);
            if (timeValue >= 10)
                g.DrawString(timeValue.ToString(), font, Brushes.Black, 40 + x - 6, measureTop + 12);
            else
                g.DrawString(timeValue.ToString(), font, Brushes.Black, 40 + x, measureTop + 12);
            // draw key sig (sharps)
            if (GetKeySignature(key, Pitch.F) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 36, y + middle - 26); }
            if (GetKeySignature(key, Pitch.C) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 40, y + middle - 14); }
            if (GetKeySignature(key, Pitch.G) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 44, y + middle - 30); }
            if (GetKeySignature(key, Pitch.D) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 48, y + middle - 18); }
            if (GetKeySignature(key, Pitch.A) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 52, y + middle - 6); }
            if (GetKeySignature(key, Pitch.E) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 56, y + middle - 22); }
            if (GetKeySignature(key, Pitch.B) == Accidental.Sharp) { g.DrawImage(Icons.sharp, x_ + 60, y + middle - 10); }
            // draw key sig (flats)
            if (GetKeySignature(key, Pitch.B) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 36, y + middle - 10); }
            if (GetKeySignature(key, Pitch.E) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 40, y + middle - 22); }
            if (GetKeySignature(key, Pitch.A) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 44, y + middle - 6); }
            if (GetKeySignature(key, Pitch.D) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 48, y + middle - 18); }
            if (GetKeySignature(key, Pitch.G) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 52, y + middle - 30); }
            if (GetKeySignature(key, Pitch.C) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 56, y + middle - 14); }
            if (GetKeySignature(key, Pitch.F) == Accidental.Flat) { g.DrawImage(Icons.flat, x_ + 60, y + middle - 26); }
        }
        private void DrawEndBars(Graphics g, int staffIndex, int xOffset)
        {
            int height = staffHeight;
            int x = xOffset;
            int y = staffIndex * height;
            int middle = height / 2;
            Pen pen = new Pen(Color.Gray, 1); pen.Alignment = PenAlignment.Outset;
            g.DrawLine(pen, x, y + middle - 16, x, y + middle + 16); pen.Width = 4;
            g.DrawLine(pen, x + 6, y + middle - 16, x + 6, y + middle + 16);
        }
        private void DrawOutsideLines(
            Graphics g, Pen pen, int x, int y, int middle,
            int clef, int octave, Pitch pitch, int line)
        {
            int count = -1;
            bool drawUpperLines = false;
            bool drawLowerLines = false;
            int totalPitch = octave * 12 + (int)pitch;
            if (clef == 0 && totalPitch >= 69)
            {
                count = (octave - 5) * 7 + line - 5;
                drawUpperLines = true;
            }
            else if (clef == 1 && totalPitch >= 72)
            {
                count = (octave - 5) * 7 + line - 7;
                drawUpperLines = true;
            }
            if (clef == 0 && totalPitch <= 49)
            {
                count = (4 - octave) * 7 - line + 2;
                drawLowerLines = true;
            }
            else if (clef == 1 && totalPitch <= 53)
            {
                count = (4 - octave) * 7 - line + 4;
                drawLowerLines = true;
            }
            count /= 2;
            if (drawUpperLines)
            {
                while (count-- >= 0)
                    g.DrawLine(pen,
                        x - 6, y + middle - 32 - (count * 8),
                        x + 6, y + middle - 32 - (count * 8));
            }
            if (drawLowerLines)
            {
                while (count-- > 0)
                    g.DrawLine(pen,
                        x - 6, y + middle + 24 + (count * 8),
                        x + 6, y + middle + 24 + (count * 8));
            }
        }
        private void SetScrollBars(bool writer)
        {
            if (writer)
            {
                for (int i = 0; i < staffs.Count; i++)
                {
                    int maximum = 0;
                    foreach (Note note in staffs[i].Notes)
                    {
                        maximum += note.Ticks;
                        if (maximum > hScrollBarSW.Maximum)
                            hScrollBarSW.Maximum = maximum;
                    }
                }
                scoreWriterPicture.Invalidate();
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    int maximum = 0;
                    foreach (Note note in spc.Notes[i])
                    {
                        maximum += note.Ticks;
                        if (maximum > hScrollBarSV.Maximum)
                            hScrollBarSV.Maximum = maximum;
                    }
                }
                scoreViewPicture.Invalidate();
            }
        }
        private bool WithinSelection(int index, bool writer)
        {
            if (overlay.Select.Empty)
                return false;
            int selectionStart;
            int selectionEnd;
            if (writer)
            {
                selectionStart = GetNoteIndex(overlay.Select.Location.X + hScrollBarSW.Value, key, writer);
                selectionEnd = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSW.Value, key, writer);
            }
            else
            {
                selectionStart = GetNoteIndex(overlay.Select.Location.X + hScrollBarSV.Value, key);
                selectionEnd = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSV.Value, key);
            }
            return index >= selectionStart && index < selectionEnd;
        }
        //
        private void Draw(bool writer)
        {
            if (insertObject == null)
                return;
            //
            if (writer)
            {
                if (insertObject == null)
                    return;
                //
                Beat beat = Beat.NULL;
                byte opcode = 0;
                byte param1 = 0;
                Note note;
                SPCCommand ssc;
                switch (insertObject.Name)
                {
                    case "ticksNoteButton": goto case "Note";
                    case "wNoteWhole": beat = Beat.Whole; goto case "Note";
                    case "wNoteHalfD": beat = Beat.HalfDotted; goto case "Note";
                    case "wNoteHalf": beat = Beat.Half; goto case "Note";
                    case "wNoteQuarterD": beat = Beat.QuarterDotted; goto case "Note";
                    case "wNoteQuarter": beat = Beat.Quarter; goto case "Note";
                    case "wNote8thD": beat = Beat.EighthDotted; goto case "Note";
                    case "wNoteQuarterT": beat = Beat.QuarterTriplet; goto case "Note";
                    case "wNote8th": beat = Beat.Eighth; goto case "Note";
                    case "wNote8thT": beat = Beat.EighthTriplet; goto case "Note";
                    case "wNote16th": beat = Beat.Sixteenth; goto case "Note";
                    case "wNote16thT": beat = Beat.SixteenthTriplet; goto case "Note";
                    case "wNote32nd": beat = Beat.ThirtySecond; goto case "Note";
                    case "wNote64th": beat = Beat.SixtyFourth; goto case "Note";
                    case "Note":
                        if (wTie.Checked)
                            goto case "Tie";
                        if (ticksNoteButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + mouseOverPitch);
                            param1 = (byte)ticksNoteValue.Value;
                            ssc = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverStaff);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + mouseOverPitch);
                            ssc = new SPCCommand(new byte[] { opcode }, spc, mouseOverStaff);
                        }
                        note = new Note(ssc, mouseOverOctave, false, 0);
                        commandStackW.Push(new ScoreEditCommand(ScoreEdit.InsertNote, staffs[mouseOverStaff].Notes, mouseDownNote, note));
                        break;
                    case "ticksRestButton": goto case "Rest";
                    case "wRestWhole": beat = Beat.Whole; goto case "Rest";
                    case "wRestHalfD": beat = Beat.HalfDotted; goto case "Rest";
                    case "wRestHalf": beat = Beat.Half; goto case "Rest";
                    case "wRestQuarterD": beat = Beat.QuarterDotted; goto case "Rest";
                    case "wRestQuarter": beat = Beat.Quarter; goto case "Rest";
                    case "wRest8thD": beat = Beat.EighthDotted; goto case "Rest";
                    case "wRestQuarterT": beat = Beat.QuarterTriplet; goto case "Rest";
                    case "wRest8th": beat = Beat.Eighth; goto case "Rest";
                    case "wRest8thT": beat = Beat.EighthTriplet; goto case "Rest";
                    case "wRest16th": beat = Beat.Sixteenth; goto case "Rest";
                    case "wRest16thT": beat = Beat.SixteenthTriplet; goto case "Rest";
                    case "wRest32nd": beat = Beat.ThirtySecond; goto case "Rest";
                    case "wRest64th": beat = Beat.SixtyFourth; goto case "Rest";
                    case "Rest":
                        if (wTie.Checked)
                            goto case "Tie";
                        if (ticksRestButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + 12);
                            param1 = (byte)ticksRestValue.Value;
                            ssc = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverStaff);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + 12);
                            ssc = new SPCCommand(new byte[] { opcode }, spc, mouseOverStaff);
                        }
                        note = new Note(ssc, mouseOverOctave, false, 0);
                        commandStackW.Push(new ScoreEditCommand(ScoreEdit.InsertNote, staffs[mouseOverStaff].Notes, mouseDownNote, note));
                        break;
                    case "Tie":
                        if (mouseDownNote == 0)
                        {
                            MessageBox.Show("Cannot put a tied note at the beginning of the staff.", "LAZY SHELL");
                            return;
                        }
                        if (ticksNoteButton.Checked || ticksRestButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + 13);
                            if (ticksNoteButton.Checked)
                                param1 = (byte)ticksNoteValue.Value;
                            else if (ticksRestButton.Checked)
                                param1 = (byte)ticksRestValue.Value;
                            ssc = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverStaff);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + 13);
                            ssc = new SPCCommand(new byte[] { opcode }, spc, mouseOverStaff);
                        }
                        note = new Note(ssc, mouseOverOctave, false, 0);
                        commandStackW.Push(new ScoreEditCommand(ScoreEdit.InsertNote, staffs[mouseOverStaff].Notes, mouseDownNote, note));
                        break;
                }
                SetScrollBars(true);
            }
            else
            {
                Beat beat = Beat.NULL;
                byte opcode = 0;
                byte param1 = 0;
                int sscIndex = 0;
                SPCCommand sscA = null;
                SPCCommand sscB = null;
                SPCCommand sscC = null;
                switch (insertObject.Name)
                {
                    case "rTicksNoteButton": goto case "Note";
                    case "rNoteWhole": beat = Beat.Whole; goto case "Note";
                    case "rNoteHalfD": beat = Beat.HalfDotted; goto case "Note";
                    case "rNoteHalf": beat = Beat.Half; goto case "Note";
                    case "rNoteQuarterD": beat = Beat.QuarterDotted; goto case "Note";
                    case "rNoteQuarter": beat = Beat.Quarter; goto case "Note";
                    case "rNote8thD": beat = Beat.EighthDotted; goto case "Note";
                    case "rNoteQuarterT": beat = Beat.QuarterTriplet; goto case "Note";
                    case "rNote8th": beat = Beat.Eighth; goto case "Note";
                    case "rNote8thT": beat = Beat.EighthTriplet; goto case "Note";
                    case "rNote16th": beat = Beat.Sixteenth; goto case "Note";
                    case "rNote16thT": beat = Beat.SixteenthTriplet; goto case "Note";
                    case "rNote32nd": beat = Beat.ThirtySecond; goto case "Note";
                    case "rNote64th": beat = Beat.SixtyFourth; goto case "Note";
                    case "Note":
                        if (rTie.Checked)
                            goto case "Tie";
                        if (rTicksNoteButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + mouseOverPitch);
                            param1 = (byte)rTicksNoteValue.Value;
                            sscB = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverChannel);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + mouseOverPitch);
                            sscB = new SPCCommand(new byte[] { opcode }, spc, mouseOverChannel);
                        }
                        sscA = OctaveChangeBefore(mouseOverOctave, true);
                        sscC = OctaveChangeAfter(mouseOverOctave, true);
                        sscIndex = OctaveChangeIndex(mouseOverOctave, mouseOverOctave);
                        commandStackR.Push(new ScoreEditCommand(ScoreEdit.InsertNote, spc.Channels[mouseOverChannel], sscIndex, sscA, sscB, sscC));
                        break;
                    case "rTicksRestButton": goto case "Rest";
                    case "rRestWhole": beat = Beat.Whole; goto case "Rest";
                    case "rRestHalfD": beat = Beat.HalfDotted; goto case "Rest";
                    case "rRestHalf": beat = Beat.Half; goto case "Rest";
                    case "rRestQuarterD": beat = Beat.QuarterDotted; goto case "Rest";
                    case "rRestQuarter": beat = Beat.Quarter; goto case "Rest";
                    case "rRest8thD": beat = Beat.EighthDotted; goto case "Rest";
                    case "rRestQuarterT": beat = Beat.QuarterTriplet; goto case "Rest";
                    case "rRest8th": beat = Beat.Eighth; goto case "Rest";
                    case "rRest8thT": beat = Beat.EighthTriplet; goto case "Rest";
                    case "rRest16th": beat = Beat.Sixteenth; goto case "Rest";
                    case "rRest16thT": beat = Beat.SixteenthTriplet; goto case "Rest";
                    case "rRest32nd": beat = Beat.ThirtySecond; goto case "Rest";
                    case "rRest64th": beat = Beat.SixtyFourth; goto case "Rest";
                    case "Rest":
                        if (rTie.Checked)
                            goto case "Tie";
                        if (rTicksRestButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + 12);
                            param1 = (byte)rTicksRestValue.Value;
                            sscB = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverChannel);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + 12);
                            sscB = new SPCCommand(new byte[] { opcode }, spc, mouseOverChannel);
                        }
                        sscIndex = mouseOverSSC.Index;
                        commandStackR.Push(new ScoreEditCommand(ScoreEdit.InsertNote, spc.Channels[mouseOverChannel], sscIndex, sscB));
                        break;
                    case "Tie":
                        if (mouseDownNote == 0)
                        {
                            MessageBox.Show("Cannot put a tied note at the beginning of the staff.", "LAZY SHELL");
                            return;
                        }
                        if (rTicksNoteButton.Checked || rTicksRestButton.Checked)
                        {
                            opcode = (byte)(13 * 14 + 13);
                            if (rTicksNoteButton.Checked)
                                param1 = (byte)rTicksNoteValue.Value;
                            else if (rTicksRestButton.Checked)
                                param1 = (byte)rTicksRestValue.Value;
                            sscB = new SPCCommand(new byte[] { opcode, param1 }, spc, mouseOverChannel);
                        }
                        else
                        {
                            opcode = (byte)((int)beat * 14 + 13);
                            sscB = new SPCCommand(new byte[] { opcode }, spc, mouseOverChannel);
                        }
                        sscIndex = mouseOverSSC.Index;
                        commandStackR.Push(new ScoreEditCommand(ScoreEdit.InsertNote, spc.Channels[mouseOverChannel], sscIndex, sscB));
                        break;
                }
                spc.CreateNotes();
                SetScrollBars(false);
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                channelTracks.Invalidate();
            }
        }
        private void Erase(bool writer)
        {
            if (writer)
            {
                if (mouseDownNote >= this.notes.Count)
                    return;
                object temp = this.notes[mouseDownNote];
                commandStackW.Push(new ScoreEditCommand(ScoreEdit.EraseNote, this.notes, mouseDownNote, temp));
            }
            else
            {
                if (mouseDownNote >= spc.Notes[mouseOverChannel].Count)
                    return;
                int sscIndex = mouseOverSSC.Index;
                commandStackR.Push(new ScoreEditCommand(ScoreEdit.EraseNote, spc.Channels[mouseOverChannel], sscIndex, mouseDownSSC));
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                spc.CreateNotes();
                channelTracks.Invalidate();
            }
            SetScrollBars(writer);
        }
        private void Delete(bool writer)
        {
            if (overlay.Select.Empty)
                return;
            if (writer)
            {
                int start = GetNoteIndex(overlay.Select.Location.X + hScrollBarSW.Value, this.key, true);
                int end = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSW.Value, this.key, true);
                if (start >= end)
                    return;
                List<object> temp = new List<object>();
                for (int i = start; i < end; i++)
                    temp.Add(this.notes[i]);
                commandStackW.Push(new ScoreEditCommand(ScoreEdit.DeleteNotes, this.notes, temp, start));
            }
            else
            {
                if (spc.Notes[mouseDownChannel].Count == 0)
                    return;
                int start = GetNoteIndex(overlay.Select.Location.X + hScrollBarSV.Value, this.key);
                int end = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSV.Value, this.key);
                if (start >= end)
                    return;
                start = spc.Notes[mouseDownChannel][start].Index;
                if (end < spc.Notes[mouseDownChannel].Count)
                    end = spc.Notes[mouseDownChannel][end].Index + 1;
                else
                    end = spc.Notes[mouseDownChannel][spc.Notes[mouseDownChannel].Count - 1].Index + 1;
                List<SPCCommand> temp = new List<SPCCommand>();
                for (int i = start; i < end; i++)
                    temp.Add(spc.Channels[mouseDownChannel][i]);
                commandStackR.Push(new ScoreEditCommand(ScoreEdit.DeleteNotes, spc.Channels[mouseDownChannel], temp, start));
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                spc.CreateNotes();
                channelTracks.Invalidate();
            }
            SetScrollBars(writer);
        }
        private void Copy(bool writer)
        {
            if (overlay.Select.Empty)
                return;
            if (writer)
            {
                int start = GetNoteIndex(overlay.Select.Location.X + hScrollBarSW.Value, this.key, true);
                int end = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSW.Value, this.key, true);
                if (start >= end)
                    return;
                copyBufferSW = new List<object>();
                for (int i = start; i < end; i++)
                    copyBufferSW.Add(((Note)this.notes[i]).Copy());
            }
            else
            {
                int start = GetNoteIndex(overlay.Select.Location.X + hScrollBarSV.Value, this.key);
                int end = GetNoteIndex(overlay.Select.Terminal.X + hScrollBarSV.Value, this.key);
                if (start >= end)
                    return;
                copyBufferSV = new CopyNotes();
                copyBufferSV.FirstOctave = spc.Notes[mouseDownChannel][start].Octave;
                copyBufferSV.LastOctave = spc.Notes[mouseDownChannel][end - 1].Octave;
                // create a temporary list of notes to be copied, canceling the operation if there's a repeat
                int index = 0;
                List<Note> notes = new List<Note>();
                for (int i = start; i < end; i++)
                {
                    Note note = spc.Notes[mouseDownChannel][i];
                    if (note.Index < index) // this means there was a repeat encountered, we must cancel
                    {
                        MessageBox.Show("The current selection contains a repeat command.\n\n" +
                            "Any notes after the repeat command will NOT be copied.",
                            "LAZY SHELL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    index = note.Index;
                    notes.Add(note);
                }
                if (notes.Count == 0)
                    return;
                //
                start = notes[0].Index;
                end = notes[notes.Count - 1].Index;
                List<SPCCommand> channel = spc.Channels[mouseDownChannel];
                for (int i = start; i <= end; i++)
                {
                    // don't add repeat commands
                    if (channel[i].Opcode < 0xD4 || channel[i].Opcode > 0xD7)
                        copyBufferSV.Commands.Add(channel[i].Copy());
                }
            }
        }
        private void Paste(int index, bool writer)
        {
            if (writer)
            {
                if (copyBufferSW == null)
                    return;
                if (index > this.notes.Count)
                    return;
                List<object> temp = new List<object>();
                foreach (Note note in copyBufferSW)
                    temp.Add(note.Copy());
                commandStackW.Push(new ScoreEditCommand(ScoreEdit.PasteNotes, this.notes, temp, index));
            }
            else
            {
                if (copyBufferSV == null)
                    return;
                if (index > spc.Channels[mouseDownChannel].Count)
                    return;
                List<SPCCommand> temp = new List<SPCCommand>();
                foreach (SPCCommand ssc in copyBufferSV.Commands)
                    temp.Add(ssc.Copy());
                // add octave changes before and/or after if necessary
                int sscIndex = OctaveChangeIndex(copyBufferSV.FirstOctave, copyBufferSV.LastOctave);
                SPCCommand sscA = OctaveChangeBefore(copyBufferSV.FirstOctave, temp.Count == 1);
                SPCCommand sscC = OctaveChangeAfter(copyBufferSV.LastOctave, temp.Count == 1 || sscA != null);
                if (sscA != null)
                    temp.Insert(0, sscA.Copy());
                if (sscC != null)
                    temp.Add(sscC.Copy());
                foreach (SPCCommand ssc in temp)
                    ssc.Channel = mouseDownChannel;
                //
                commandStackR.Push(new ScoreEditCommand(ScoreEdit.PasteNotes, spc.Channels[mouseDownChannel], temp, sscIndex));
                if (Type == 0)
                    ((SPCTrack)spc).AssembleSPCData();
                spc.CreateNotes();
                channelTracks.Invalidate();
            }
            SetScrollBars(writer);
        }
        #endregion
        #region Event Handlers
        private void scoreWriter_Click(object sender, EventArgs e)
        {
            overlay.Select.Clear();
            groupBoxSW.Visible = scoreWriter.Checked;
            groupBoxSW.BringToFront();
        }
        private void hScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            scoreWriterPicture.Invalidate();
        }
        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            scoreWriterPicture.Invalidate();
        }
        //
        private void writer_Click(object sender, EventArgs e)
        {
            ToolStripButton insertObject = (ToolStripButton)sender;
            foreach (ToolStripItem item in wToolStripMain.Items)
                if (item.GetType() == typeof(ToolStripButton) &&
                    item != insertObject)
                    ((ToolStripButton)item).Checked = false;
            foreach (ToolStripItem item in wToolStripNote.Items)
                if (item.GetType() == typeof(ToolStripButton) &&
                    item != insertObject &&
                    item != wSharp &&
                    item != wNatural &&
                    item != wFlat &&
                    item != wTie)
                    ((ToolStripButton)item).Checked = false;
            if (insertObject == ticksNoteButton)
                ticksRestButton.Checked = false;
            if (insertObject == ticksRestButton)
                ticksNoteButton.Checked = false;
            if (insertObject.Checked)
            {
                wDraw.Checked = true;
                this.insertObject = insertObject;
                scoreWriterPicture.Cursor = NewCursors.Draw;
            }
            else
            {
                this.insertObject = null;
                scoreWriterPicture.Cursor = Cursors.Arrow;
            }
        }
        private void wDraw_CheckedChanged(object sender, EventArgs e)
        {
            if (wDraw.Checked)
            {
                wErase.Checked = false;
                wSelect.Checked = false;
                wPaste.Checked = false;
                scoreWriterPicture.Cursor = NewCursors.Draw;
            }
            else
                scoreWriterPicture.Cursor = Cursors.Arrow;
            scoreWriterPicture.Invalidate();
        }
        private void wErase_CheckedChanged(object sender, EventArgs e)
        {
            if (wErase.Checked)
            {
                wDraw.Checked = false;
                wSelect.Checked = false;
                wPaste.Checked = false;
                scoreWriterPicture.Cursor = NewCursors.Erase;
            }
            else
                scoreWriterPicture.Cursor = Cursors.Arrow;
            scoreWriterPicture.Invalidate();
        }
        private void wSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (wSelect.Checked)
            {
                wDraw.Checked = false;
                wErase.Checked = false;
                wPaste.Checked = false;
                scoreWriterPicture.Cursor = Cursors.Cross;
            }
            else
            {
                overlay.Select.Clear();
                scoreWriterPicture.Cursor = Cursors.Arrow;
            }
            scoreWriterPicture.Invalidate();
        }
        private void wPaste_CheckedChanged(object sender, EventArgs e)
        {
            if (wPaste.Checked)
            {
                wDraw.Checked = false;
                wErase.Checked = false;
                wSelect.Checked = false;
                scoreWriterPicture.Cursor = NewCursors.Draw;
            }
            else
                scoreWriterPicture.Cursor = Cursors.Arrow;
            scoreWriterPicture.Invalidate();
        }
        private void wDelete_Click(object sender, EventArgs e)
        {
            if (scoreWriter.Checked)
                Delete(true);
            else
                rDelete.PerformClick();
        }
        private void wCut_Click(object sender, EventArgs e)
        {
            if (scoreWriter.Checked)
            {
                Delete(true);
                Copy(true);
            }
            else
                rCut.PerformClick();
        }
        private void wCopy_Click(object sender, EventArgs e)
        {
            if (scoreWriter.Checked)
                Copy(true);
            else
                rCopy.PerformClick();
        }
        private void wAccidental_Click(object sender, EventArgs e)
        {
            if (sender == wSharp)
            {
                wFlat.Checked = false;
                wNatural.Checked = false;
            }
            else if (sender == wFlat)
            {
                wSharp.Checked = false;
                wNatural.Checked = false;
            }
            else
            {
                wSharp.Checked = false;
                wFlat.Checked = false;
            }
        }
        private void wTie_Click(object sender, EventArgs e)
        {
        }
        //
        private void scoreWriterPicture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            int staffIndex = 0;
            foreach (Staff staff in staffs)
            {
                int x = -hScrollBarSW.Value + 64 + GetKeyWidth(this.key);
                int y = staffIndex * staffHeight;
                int middle = staffHeight / 2;
                // draw staff hilite
                SolidBrush brush = new SolidBrush(Color.White);
                if (staffIndex == mouseDownStaff)
                    e.Graphics.FillRectangle(brush, 0, y + 4, scoreWriterPicture.Width, staffHeight - 8);
                // draw staff ledger lines
                DrawBarsLines(e.Graphics, staff.Clef, score.Key, staffIndex, -hScrollBarSW.Value, true, GetStaffWidth(staffIndex));
                // draw notes
                int indexNotes = 0;
                Note lastNote = null; // the last previous note, with a pitch
                Note lastItem = null; // the last previous note
                for (int i = 0; i < staff.Notes.Count; i++)
                {
                    object item = staff.Notes[i];
                    if (item.GetType() != typeof(Note))
                    {
                        indexNotes++;
                        continue;
                    }
                    Note note = (Note)item;
                    if (x < -32 || x - 32 > scoreWriterPicture.Width)
                    {
                        x += (int)((double)noteSpacingSW.Value / 100.0 * note.Ticks);
                        if (!note.Rest && !note.Tie)
                            lastNote = note;
                        lastItem = note;
                        indexNotes++;
                        continue;
                    }
                    bool hilite = mouseEnter && indexNotes == mouseOverNote &&
                        staffIndex == mouseOverStaff && staffIndex == mouseDownStaff;
                    bool select;
                    if (overlay.Select != null)
                        select = WithinSelection(i, true) && staffIndex == mouseDownStaff;
                    else
                        select = mouseDownNote == i && staffIndex == mouseDownStaff;
                    DrawNote(e.Graphics, note, lastNote, lastItem, x, staffHeight,
                        staff.Clef, score.Key, staffIndex, hilite, select);
                    x += (int)((double)noteSpacingSW.Value / 100.0 * note.Ticks);
                    if (!note.Rest && !note.Tie)
                        lastNote = note;
                    lastItem = note;
                    indexNotes++;
                }
                // draw extra lines beyond normal lines, if mouse over it
                if (mouseEnter && staffIndex == mouseOverStaff && staffIndex == mouseDownStaff && !wSelect.Checked)
                {
                    DrawOutsideLines(e.Graphics, new Pen(Color.Gray), mousePosition.X, y, middle,
                        staff.Clef, mouseOverOctave, mouseOverPitch, mouseOverLine);
                    brush.Color = Color.FromArgb(128, Color.Black);
                    if (insertObject != null && mouseEnter && mousePosition.X != -1 && mousePosition.Y != -1)
                        e.Graphics.FillEllipse(brush, mousePosition.X - 4, mousePosition.Y / 4 * 4, 8, 8);
                }
                staffIndex++;
            }
            // draw selection box
            if (wSelect.Checked && overlay.Select != null)
            {
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                overlay.Select.DrawSelectionBox(e.Graphics, 1);
            }
        }
        private void scoreWriterPicture_MouseDown(object sender, MouseEventArgs e)
        {
            overlay.Select.Clear();
            if (staffs.Count == 0)
                return;
            if (mouseOverStaff == -1)
                return;
            if (mouseDownStaff != mouseOverStaff)
            {
                mouseDownStaff = mouseOverStaff;
                if (mouseDownStaff < staffs.Count)
                    RefreshStaff();
                return;
            }
            // if clicking picture right after undo/redo, we must do a manual mouseMove
            if (mouseOverNote == -1)
                scoreWriterPicture_MouseMove(sender, e);
            // Get index to insert between notes (and commands), 64 is after the clef
            mouseDownNote = mouseOverNote;
            // set the selected command in the track viewer
            SelectNote(true);
            //
            if (wDraw.Checked)
            {
                Draw(true);
                return;
            }
            if (wErase.Checked)
            {
                Erase(true);
                return;
            }
            if (wSelect.Checked)
            {
                overlay.Select.Refresh(1, e.X, e.Y, 1, 1, scoreWriterPicture);
                scoreWriterPicture.Invalidate();
                return;
            }
            if (wPaste.Checked)
            {
                Paste(mouseDownNote, true);
                return;
            }
        }
        private void scoreWriterPicture_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void scoreWriterPicture_MouseEnter(object sender, EventArgs e)
        {
            mouseEnter = true;
            scoreWriterPicture.Focus();
        }
        private void scoreWriterPicture_MouseLeave(object sender, EventArgs e)
        {
            mouseEnter = false;
            labelWNote.Text = "...";
            scoreWriterPicture.Invalidate();
        }
        private void scoreWriterPicture_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Math.Max(e.X, 0);
            int y = Math.Max(e.Y, 0);
            #region Selecting
            if (wSelect.Checked)
            {
                // if making a new selection
                if (e.Button == MouseButtons.Left && overlay.Select != null)
                {
                    // cancel if within same bounds as last call
                    if (overlay.Select.Final == new Point(x, y))
                        return;
                    // otherwise, set the lower right edge of the selection
                    overlay.Select.Final = new Point(
                        Math.Min(x, scoreWriterPicture.Width),
                        Math.Min(y, scoreWriterPicture.Height));
                }
            }
            #endregion
            #region Mouse Over
            mousePosition = new Point(x, y);
            mouseOverNote = -1;
            mouseOverPitch = Pitch.NULL;
            mouseOverStaff = -1;
            if (staffs.Count == 0 || y / staffHeight >= staffs.Count)
            {
                scoreWriterPicture.Invalidate();
                labelWNote.Text = "...";
                return;
            }
            //
            mouseOverStaff = y / staffHeight;
            if (mouseOverStaff != mouseDownStaff)
            {
                scoreWriterPicture.Invalidate();
                labelWNote.Text = "...";
                return;
            }
            //
            x = Math.Max(x + hScrollBarSW.Value, 0);
            y = y % staffHeight;
            mouseOverNote = GetNoteIndex(x, this.key, true);
            // 88 pixels p/staff, pitches are separate by 4 pixels, 11 lines p/staff
            int staffOffset = 0;
            if (clef.SelectedIndex == 0)
                staffOffset = -1;
            else if (clef.SelectedIndex == 1)
                staffOffset = 1;
            // 272 is the staff size where everything is perfect
            int line = (staffHeight / 4) - (y / 4) + ((272 - staffHeight) / 8) + staffOffset;
            mouseOverLine = line % 7;
            switch (mouseOverLine)
            {
                case 0: mouseOverPitch = Pitch.C; labelWNote.Text = "C"; break; // A, A#
                case 1: mouseOverPitch = Pitch.D; labelWNote.Text = "D"; break; // B
                case 2: mouseOverPitch = Pitch.E; labelWNote.Text = "E"; break; // C, C#
                case 3: mouseOverPitch = Pitch.F; labelWNote.Text = "F"; break; // D, D#
                case 4: mouseOverPitch = Pitch.G; labelWNote.Text = "G"; break; // E
                case 5: mouseOverPitch = Pitch.A; labelWNote.Text = "A"; break; // F, F#
                case 6: mouseOverPitch = Pitch.B; labelWNote.Text = "B"; break; // G, G#
                default: mouseOverPitch = Pitch.NULL; labelWNote.Text = ""; break;
            }
            // adjust pitch based on checked accidental AND key signature
            if (wSharp.Checked)
                mouseOverPitch++;
            if (wFlat.Checked)
                mouseOverPitch--;
            Accidental accidental = GetAccidental(this.key, mouseOverPitch);
            if (!wNatural.Checked && accidental == Accidental.Natural)
            {
                if ((this.key >= Key.CMajor && this.key <= Key.CsMajor) ||
                    (this.key >= Key.AMinor && this.key <= Key.AsMinor)) // sharp key
                    mouseOverPitch++;
                else
                    mouseOverPitch--; // flat key
            }
            // 7 lines per octave, so 28 (7 * 4), offset by 52
            mouseOverOctave = line / 7;
            if (mouseOverPitch == Pitch.Rest)
            {
                mouseOverPitch = Pitch.C;
                mouseOverOctave++;
            }
            //
            if (mouseOverStaff == mouseDownStaff)
                labelWNote.Text += mouseOverOctave.ToString();
            else
                labelWNote.Text = "...";
            #endregion
            scoreWriterPicture.Invalidate();
        }
        private void scoreWriterPicture_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.D: wDraw.PerformClick(); break;
                case Keys.E: wErase.PerformClick(); break;
                case Keys.S: wSelect.PerformClick(); break;
                case Keys.Control | Keys.C: wCopy.PerformClick(); break;
                case Keys.Control | Keys.X: wCut.PerformClick(); break;
                case Keys.Delete: wDelete.PerformClick(); break;
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
                case Keys.Left:
                    if (mouseDownNote > 0)
                    {
                        mouseDownNote--;
                        Note note = (Note)staffs[mouseDownStaff].Notes[mouseDownNote];
                        int ticks = (int)((double)noteSpacing / 100.0 * note.Ticks);
                        if (hScrollBarSW.Value - ticks >= 0)
                            hScrollBarSW.Value -= ticks;
                        SelectNote(true);
                    }
                    break;
                case Keys.Right:
                    if (mouseDownNote < staffs[mouseDownStaff].Notes.Count - 1)
                    {
                        Note note = (Note)staffs[mouseDownStaff].Notes[mouseDownNote];
                        int ticks = (int)((double)noteSpacing / 100.0 * note.Ticks);
                        mouseDownNote++;
                        note = (Note)staffs[mouseDownStaff].Notes[mouseDownNote];
                        if (hScrollBarSW.Value + ticks <= hScrollBarSW.Maximum)
                            hScrollBarSW.Value += ticks;
                        SelectNote(true);
                    }
                    break;
            }
        }
        // Common commands
        private void saveScoreFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = settings.NotePathCustom;
            saveFileDialog.Title = "Save as new score file...";
            saveFileDialog.FileName = "score.lsscore";
            saveFileDialog.Filter = "Score File (*.lsnotes)|*.lsscore";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            //
            Stream s = File.Create(saveFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, score);
            s.Close();
        }
        private void openScoreFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = settings.NotePathCustom;
            openFileDialog.Title = "Open existing score file...";
            openFileDialog.Filter = "Score File (*.lsscore)|*.lsscore";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            //
            Stream s = File.OpenRead(openFileDialog.FileName);
            BinaryFormatter b = new BinaryFormatter();
            try
            {
                score = (Score)b.Deserialize(s);
            }
            catch
            {
                MessageBox.Show("This is not a valid score file.", "LAZY SHELL",
                MessageBoxButtons.OK);
                s.Close();
                return;
            }
            s.Close();
            //
            keySW.SelectedIndex = (int)score.Key;
            timeBeatsSW.Value = score.TimeBeats;
            timeValueSW.Value = score.TimeValue;
            if (score.Staffs.Count > 0)
            {
                mouseDownStaff = 0;
                wStaffDelete.Enabled = true;
                wStaffMoveDown.Enabled = true;
                wStaffMoveUp.Enabled = true;
                undo.Enabled = true;
                redo.Enabled = true;
                clef.SelectedIndex = score.Staffs[mouseDownStaff].Clef;
                RefreshStaff();
            }
            SetScrollBars(true);
        }
        private void exportScoreFiles_Click(object sender, EventArgs e)
        {
            // first, open and create directory
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = Settings.Default.LastDirectory;
            folderBrowserDialog1.Description = "Select directory to export to";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                Settings.Default.LastDirectory = folderBrowserDialog1.SelectedPath;
            else
                return;
            //
            for (int i = 0; i < staffs.Count; i++)
            {
                StreamWriter script = File.CreateText(folderBrowserDialog1.SelectedPath + "\\staff" + i + ".txt");
                int octave = -2;
                SPCCommand ssc;
                foreach (object item in staffs[i].Notes)
                {
                    if (item.GetType() == typeof(Note))
                    {
                        Note note = (Note)item;
                        if (note.Octave == octave + 1)
                        {
                            octave++;
                            ssc = new SPCCommand(new byte[] { 0xC4 }, spc, i);
                            script.WriteLine(ssc.ToString());
                        }
                        else if (note.Octave == octave - 1)
                        {
                            octave--;
                            ssc = new SPCCommand(new byte[] { 0xC5 }, spc, i);
                            script.WriteLine(ssc.ToString());
                        }
                        else if (note.Octave != octave)
                        {
                            octave = note.Octave;
                            ssc = new SPCCommand(new byte[] { 0xC6, (byte)octave }, spc, i);
                            script.WriteLine(ssc.ToString());
                        }
                    }
                    script.WriteLine(item.ToString());
                }
                // terminate script
                ssc = new SPCCommand(new byte[] { 0xD0 }, spc, i);
                script.WriteLine(ssc.ToString());
                script.Close();
            }
        }
        private void exportStaffsMML_Click(object sender, EventArgs e)
        {
            ExportMMLScript(3);
        }
        private void importScoreFiles_Click(object sender, EventArgs e)
        {
            List<SPCCommand> commands = new List<SPCCommand>();
            if (!ImportSPCScript(ref commands))
                return;
            //
            List<object> notes = new List<object>();
            int index = 0;
            int octave = 6;
            bool percussive = false;
            int sample = 0;
            while (index < commands.Count)
            {
                SPCCommand ssc = commands[index++];
                switch (ssc.Opcode)
                {
                    case 0xC4: octave++; break;
                    case 0xC5: octave--; break;
                    case 0xC6: octave = ssc.Param1; break;
                    case 0xD4:
                        SequenceLoop(commands, notes, ref index, index, ssc.Param1, ref octave, ref percussive, ref sample);
                        break;
                    case 0xD5: break;
                    case 0xDE: sample = ssc.Param1; goto default;
                    case 0xEE: percussive = true; goto default;
                    case 0xEF: percussive = false; goto default;
                    default:
                        if (ssc.Opcode < 0xC4 || ssc.Opcode == 0xDE || ssc.Opcode == 0xEE || ssc.Opcode == 0xEF)
                            notes.Add(new Note(ssc, octave, percussive, sample));
                        break;
                }
            }
            wStaffNew.PerformClick();
            mouseDownStaff = staffs.Count - 1;
            this.notes = notes;
            SetScrollBars(true);
        }
        private void SequenceLoop(List<SPCCommand> commands, List<object> notes,
            ref int index, int start, int count, ref int octave_, ref bool percussive, ref int sample)
        {
            int octave = octave_;
            while (count > 0 && index < commands.Count)
            {
                SPCCommand ssc = commands[index++];
                if (ssc.Opcode == 0xD6 && count == 1)
                {
                    while (index < commands.Count && commands[index].Opcode != 0xD5) index++; break;
                }
                switch (ssc.Opcode)
                {
                    case 0xC4: octave++; break;
                    case 0xC5: octave--; break;
                    case 0xC6: octave = ssc.Param1; break;
                    case 0xD4:
                        SequenceLoop(commands, notes, ref index, index, ssc.Param1, ref octave, ref percussive, ref sample);
                        break;
                    case 0xD5: count--; if (count > 0) { index = start; octave = octave_; } break;
                    case 0xD6: break;
                    case 0xDE: sample = ssc.Param1; goto default;
                    case 0xEE: percussive = true; goto default;
                    case 0xEF: percussive = false; goto default;
                    default:
                        if (ssc.Opcode < 0xC4 || ssc.Opcode == 0xDE || ssc.Opcode == 0xEE || ssc.Opcode == 0xEF)
                            notes.Add(new Note(ssc, octave, percussive, sample));
                        break;
                }
            }
            octave_ = octave;
        }
        private void staffHeight_ValueChanged(object sender, EventArgs e)
        {
            staffHeightSW.Value = (int)staffHeightSW.Value / 8 * 8;
            scoreWriterPicture.Invalidate();
        }
        private void wStaffNew_Click(object sender, EventArgs e)
        {
            if (Type == 0 && staffs.Count == 8)
                return;
            else if (Type != 0 && staffs.Count == 2)
                return;
            commandStackW.Push(new ScoreEditCommand(ScoreEdit.AddStaff, staffs, staffs.Count, new Staff()));
            if (staffs.Count == 1)
            {
                mouseDownStaff = 0;
                wStaffDelete.Enabled = true;
                wStaffMoveDown.Enabled = true;
                wStaffMoveUp.Enabled = true;
                undo.Enabled = true;
                redo.Enabled = true;
                RefreshStaff();
            }
            scoreWriterPicture.Invalidate();
        }
        private void wStaffDelete_Click(object sender, EventArgs e)
        {
            if (mouseDownStaff == -1)
                return;
            commandStackW.Push(new ScoreEditCommand(ScoreEdit.DeleteStaff, staffs, mouseDownStaff, staffs[mouseDownStaff]));
            wStaffDelete.Enabled = staffs.Count != 0;
            wStaffMoveDown.Enabled = staffs.Count != 0;
            wStaffMoveUp.Enabled = staffs.Count != 0;
            keySW.Enabled = staffs.Count != 0;
            clef.Enabled = staffs.Count != 0;
            if (staffs.Count > 0)
                mouseDownStaff = 0;
            RefreshStaff();
            scoreWriterPicture.Invalidate();
        }
        private void wStaffMoveUp_Click(object sender, EventArgs e)
        {
            if (mouseDownStaff <= 0)
                return;
            staffs.Reverse(mouseDownStaff - 1, 2);
            mouseDownStaff--;
            scoreWriterPicture.Invalidate();
        }
        private void wStaffMoveDown_Click(object sender, EventArgs e)
        {
            if (mouseDownStaff == -1)
                return;
            if (mouseDownStaff >= staffs.Count - 1)
                return;
            staffs.Reverse(mouseDownStaff, 2);
            mouseDownStaff++;
            scoreWriterPicture.Invalidate();
        }
        private void clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffs[mouseDownStaff].Clef = clef.SelectedIndex;
            scoreWriterPicture.Invalidate();
        }
        private void keySW_SelectedIndexChanged(object sender, EventArgs e)
        {
            score.Key = (Key)keySW.SelectedIndex;
            scoreWriterPicture.Invalidate();
        }
        private void timeBeatsSW_ValueChanged(object sender, EventArgs e)
        {
            score.TimeBeats = (int)timeBeatsSW.Value;
            scoreWriterPicture.Invalidate();
        }
        private void timeValueSW_ValueChanged(object sender, EventArgs e)
        {
            score.TimeValue = (int)timeValueSW.Value;
            scoreWriterPicture.Invalidate();
        }
        private void noteSpacingSW_ValueChanged(object sender, EventArgs e)
        {
            noteSpacingSW.Value = (int)noteSpacingSW.Value / 10 * 10;
            SetScrollBars(true);
        }
        // Notes and rests, etc.
        #endregion
    }
    [Serializable()]
    public class Score
    {
        public List<Staff> Staffs = new List<Staff>();
        public Key Key = Key.CMajor;
        public int TimeBeats = 4;
        public int TimeValue = 4;
    }
    [Serializable()]
    public class Staff
    {
        public int Clef = 0; // Treble
        public List<object> Notes = new List<object>();
        public Staff(int clef)
        {
            this.Clef = clef;
        }
        public Staff()
        {
        }
    }
    [Serializable()]
    public class CopyNotes
    {
        public int FirstOctave = 5;
        public int LastOctave = 5;
        public List<SPCCommand> Commands = new List<SPCCommand>();
    }
}
