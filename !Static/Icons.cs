using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL
{
    public static class Icons
    {
        // notation icons
        public static Bitmap noteWhole = Resources.noteWhole;
        public static Bitmap noteHalfDotted = Resources.noteHalfDotted;
        public static Bitmap noteHalf = Resources.noteHalf;
        public static Bitmap noteHalfTriplet = Resources.noteHalfTriplet;
        public static Bitmap noteQuarterDotted = Resources.noteDotted;
        public static Bitmap noteQuarter = Resources.noteQuarter;
        public static Bitmap note8thDotted = Resources.note8thDotted;
        public static Bitmap noteQuarterTriplet = Resources.noteQuarterTriplet;
        public static Bitmap note8th = Resources.note8th;
        public static Bitmap note8thTriplet = Resources.note8thTriplet;
        public static Bitmap note16th = Resources.note16th;
        public static Bitmap note16thDotted = Resources.note16thDotted;
        public static Bitmap note16thTriplet = Resources.note16thTriplet;
        public static Bitmap note32nd = Resources.note32nd;
        public static Bitmap note32ndDotted = Resources.note32ndDotted;
        public static Bitmap note32ndTriplet = Resources.note32ndTriplet;
        public static Bitmap note64th = Resources.note64th;
        public static Bitmap note64thTriplet = Resources.note64thTriplet;
        public static Bitmap notePercussion = Resources.notePercussion;
        public static Bitmap restWhole = Resources.restWhole;
        public static Bitmap restHalfDotted = Resources.restHalfDotted;
        public static Bitmap restHalf = Resources.restHalf;
        public static Bitmap restHalfTriplet = Resources.restHalfTriplet;
        public static Bitmap restQuarterDotted = Resources.restDotted;
        public static Bitmap restQuarter = Resources.restQuarter;
        public static Bitmap rest8thDotted = Resources.rest8thDotted;
        public static Bitmap restQuarterTriplet = Resources.restQuarterTriplet;
        public static Bitmap rest8th = Resources.rest8th;
        public static Bitmap rest8thTriplet = Resources.rest8thTriplet;
        public static Bitmap rest16th = Resources.rest16th;
        public static Bitmap rest16thDotted = Resources.rest16thDotted;
        public static Bitmap rest16thTriplet = Resources.rest16thTriplet;
        public static Bitmap rest32ndDotted = Resources.rest32ndDotted;
        public static Bitmap rest32nd = Resources.rest32nd;
        public static Bitmap rest32ndTriplet = Resources.rest32ndTriplet;
        public static Bitmap rest64th = Resources.rest64th;
        public static Bitmap rest64thTriplet = Resources.rest64thTriplet;
        // note stems, heads
        public static Bitmap noteStemUp = Resources.noteStemUp;
        public static Bitmap noteStemUp8th = Resources.noteStemUp8th;
        public static Bitmap noteStemUp16th = Resources.noteStemUp16th;
        public static Bitmap noteStemUp32nd = Resources.noteStemUp32nd;
        public static Bitmap noteStemUp64th = Resources.noteStemUp64th;
        public static Bitmap noteStemDown = Resources.noteStemDown;
        public static Bitmap noteStemDown8th = Resources.noteStemDown8th;
        public static Bitmap noteStemDown16th = Resources.noteStemDown16th;
        public static Bitmap noteStemDown32nd = Resources.noteStemDown32nd;
        public static Bitmap noteStemDown64th = Resources.noteStemDown64th;
        public static Bitmap noteHead = Resources.noteHead;
        public static Bitmap noteHeadDotted = Resources.noteHeadDotted;
        public static Bitmap noteHeadTriplet = Resources.noteHeadTriplet;
        public static Bitmap noteHeadPercussion = Resources.noteHeadPercussion;
        public static Bitmap noteEmpty = Resources.noteEmpty;
        public static Bitmap noteEmptyDotted = Resources.noteEmptyDotted;
        public static Bitmap noteEmptyTriplet = Resources.noteEmptyTriplet;
        public static Bitmap tieOver = Resources.tieOver;
        public static Bitmap tieUnder = Resources.tieUnder;
        // channel track icons
        public static Bitmap sharp = Resources.sharp;
        public static Bitmap flat = Resources.flat;
        public static Bitmap natural = Resources.natural;
        public static Bitmap octaveUp = Resources.octaveUp;
        public static Bitmap octaveDown = Resources.octaveDown;
        public static Bitmap octaveSet = Resources.octaveSet;
        public static Bitmap terminate = Resources.terminate;
        public static Bitmap metronome = Resources.metronome;
        public static Bitmap loop = Resources.repeatStart;
        public static Bitmap loopEnd = Resources.repeatEnd;
        public static Bitmap firstSection = Resources.firstSection;
        public static Bitmap loopInf = Resources.repeatInf;
        public static Bitmap instrument = Resources.instrument;
        public static Bitmap volume = Resources.volume;
        public static Bitmap portamento = Resources.portamento;
        public static Bitmap speakerBalance = Resources.speakerBalance;
        public static Bitmap tremolo = Resources.tremolo;
        public static Bitmap reverbOn = Resources.reverbOn;
        public static Bitmap reverbOff = Resources.reverbOff;
        public static Bitmap drumsOn = Resources.drumsOn;
        public static Bitmap drumsOff = Resources.drumsOff;
        public static Bitmap vibrato = Resources.vibrato;
        //
        private static Bitmap emptyBlock;
        public static Bitmap Emptyblock
        {
            get
            {
                if (emptyBlock == null)
                {
                    emptyBlock = new Bitmap(256, 256);
                    Graphics g = Graphics.FromImage(emptyBlock);
                    Bitmap transparent = Resources._transparent;
                    for (int y = 0; y < 256; y += 8)
                    {
                        for (int x = 0; x < 256; x += 8)
                            g.DrawImage(transparent, x, y);
                    }
                }
                return emptyBlock;
            }
        }
    }
}
