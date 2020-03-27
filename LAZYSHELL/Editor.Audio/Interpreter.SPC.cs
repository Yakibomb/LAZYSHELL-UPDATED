using System;
using System.Collections.Generic;
using System.Text;
using LAZYSHELL.Properties;

namespace LAZYSHELL.ScriptsEditor.Commands
{
    public partial class Interpreter
    {
        #region Static Data
        public static string[] SPCCommands = new string[]
        {
            "","","","","","","","","","","","","","","","", // 0x00-0x0F
            "","","","","","","","","","","","","","","","", // 0x10-0x1F
            "","","","","","","","","","","","","","","","", // 0x20-0x2F
            "","","","","","","","","","","","","","","","", // 0x30-0x3F
            "","","","","","","","","","","","","","","","", // 0x40-0x4F
            "","","","","","","","","","","","","","","","", // 0x50-0x5F
            "","","","","","","","","","","","","","","","", // 0x60-0x6F
            "","","","","","","","","","","","","","","","", // 0x70-0x7F
            "","","","","","","","","","","","","","","","", // 0x80-0x8F
            "","","","","","","","","","","","","","","","", // 0x90-0x9F
            "","","","","","","","","","","","","","","","", // 0xA0-0xAF
            "","","","","","","","","","","","","","","","", // 0xB0-0xBF
            "",			// 0xC0
            "",			// 0xC1
            "",			// 0xC2
            "",			// 0xC3
            "Octave up",			// 0xC4
            "Octave down",			// 0xC5
            "Octave = {0}",			// 0xC6
            "Slur next note",			// 0xC7
            "Noise on, channels {0}",			// 0xC8
            "Noise on, all channels",			// 0xC9
            "Noise off",			// 0xCA
            "Frequency mode on",			// 0xCB
            "Frequency mode off",			// 0xCC
            "Play channel 0 of sound = {0}",			// 0xCD
            "Play channel 1 of sound = {0}",			// 0xCE
            "Transpose 1/16 pitch = {0}",			// 0xCF
			
            "Terminate script",			// 0xD0
            "Beat duration = {0}",			// 0xD1
            "Audio memory $69 = {0}",			// 0xD2
            "",			// 0xD3
            "Begin repeat, count = {0}",			// 0xD4
            "End repeat (reset octave)",			// 0xD5
            "Repeat ending start",			// 0xD6
            "Begin infinite repeat",			// 0xD7
            "ADSR reset",			// 0xD8
            "ADSR attack = {0}",			// 0xD9
            "ADSR decay = {0}",			// 0xDA
            "ADSR sustain = {0}",			// 0xDB
            "ADSR release = {0}",			// 0xDC
            "Sample length = {0}",			// 0xDD
            "Sample = {0}",			// 0xDE
            "Noise transpose, pitch = {0}, VOXCON = {1}",			// 0xDF
			
            "Sample fadeout = {0}",			// 0xE0
            "",			// 0xE1
            "Volume = {0}",			// 0xE2
            "Volume shift, amount = {0}",			// 0xE3
            "Volume slide, duration = {0}, volume = {1}",			// 0xE4
            "Portamento, duration = {0}, pitch = {1}",			// 0xE5
            "Portamento looping",			// 0xE6
            "Speaker balance = {0}",			// 0xE7
            "Pansweep, duration = {0}, end balance = {1}",			// 0xE8
            "Pansweep loop, duration = {0}, intensity = {1}",			// 0xE9
            "",			// 0xEA
            "",			// 0xEB
            "Transpose 1/4 pitch from 0 = {0}",			// 0xEC
            "Transpose 1/4 pitch = {0}",			// 0xED
            "Percussion mode on",			// 0xEE
            "Percussion mode off",			// 0xEF
			
            "Tremolo, amplitude = {0}, wavelength = {1}, delay = {2}",			// 0xF0
            "Vibrato, amplitude = {0}, wavelength = {1}, delay = {2}",			// 0xF1
            "Beat duration, change = {0}",			// 0xF2
            "Vibrato off",			// 0xF3
            "Growling, roughness = {0}, wavelength = {1}",			// 0xF4
            "",			// 0xF5
            "Portamento on, length = {0}",			// 0xF6
            "Growling off",			// 0xF7
            "Dizziness on",			// 0xF8
            "Dizziness off",			// 0xF9
            "Reverb on",			// 0xFA
            "Reverb off",			// 0xFB
            "Reverb, delay = {0}, decay = {1}, echo = {2}",			// 0xFC
            "",			// 0xFD
            "",			// 0xFE
            ""				// 0xFF
        };
        #endregion
        public string Interpret(SPCCommand ssc)
        {
            string[] vars = new string[16];
            switch (ssc.Opcode)
            {
                case 0xCD:
                case 0xCE:
                    if (ssc.Type < 2)
                        vars[0] = Lists.Numerize(Lists.SoundNames[ssc.Param1], ssc.Param1, 3);
                    else
                        vars[0] = Lists.Numerize(Lists.BattleSoundNames[ssc.Param1], ssc.Param1, 3);
                    break;
                case 0xC6:
                case 0xC8:
                case 0xD1:
                case 0xD4:
                case 0xD9:
                case 0xDA:
                case 0xDB:
                case 0xDC:
                case 0xDD:
                case 0xE0:
                case 0xE2:
                case 0xE7:
                case 0xF6:
                    vars[0] = ssc.Param1.ToString();
                    break;
                case 0xDE:
                    vars[0] = Lists.Numerize(Lists.SampleNames[ssc.Param1], ssc.Param1, 3);
                    break;
                case 0xDF:
                    if (!Bits.GetBit(ssc.Param1, 4))
                        vars[0] = (ssc.Param1 & 0x0F).ToString();
                    else
                        vars[0] = (-((ssc.Param1 ^ 0x1F) + 1)).ToString();
                    vars[1] = (ssc.Param2 >> 5).ToString();
                    break;
                case 0xCF:
                case 0xE3:
                case 0xEC:
                case 0xED:
                case 0xF2:
                    vars[0] = ((sbyte)ssc.Param1).ToString();
                    break;
                case 0xE4:
                case 0xE5:
                    vars[0] = ssc.Param1.ToString();
                    vars[1] = ((sbyte)ssc.Param2).ToString();
                    break;
                case 0xE8:
                case 0xE9:
                case 0xF0:
                case 0xF1:
                case 0xF4:
                    vars[0] = ssc.Param1.ToString();
                    vars[1] = ssc.Param2.ToString();
                    if (ssc.Opcode == 0xF1)
                        vars[2] = ssc.Param3.ToString();
                    break;
                case 0xFC:
                    vars[0] = ssc.Param1.ToString();
                    vars[1] = ssc.Param2.ToString();
                    vars[2] = ssc.Param3.ToString();
                    break;
                default:
                    if (ssc.Opcode < 0xC4)
                        return new Note(ssc).ToString();
                    break;
            }
            string command = SPCCommands[ssc.Opcode];
            if (command == "")
                command = "UNKSPCCMD {{" + BitConverter.ToString(ssc.CommandData) + "}}";
            return string.Format(command, vars);
        }
    }
}
