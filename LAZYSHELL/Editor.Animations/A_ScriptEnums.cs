using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL.ScriptsEditor
{
    static class A_ScriptEnums
    {
        private static int[] CommandLengths = new int[]
        {
         // 0 1 2 3 4 5 6 7   8 9 A B C D E F
            9,8,1,6,4,1,6,1,  8,3,1,8,6,0,1,1,  // 0x00
            3,1,2,0,1,1,1,1,  3,0,2,2,2,0,2,2,  // 0x10
            4,4,4,4,6,6,6,6,  6,6,6,6,4,4,4,4,  // 0x20
            2,2,2,2,2,2,3,3,  5,5,1,1,3,0,1,6,  // 0x30
            //
            3,3,8,2,2,1,1,4,  0,0,0,0,0,0,1,1,  // 0x40
            3,3,5,0,1,1,1,1,  1,1,1,3,0,5,1,0,  // 0x50
            //
            0,0,1,2,3,0,0,0,  4,1,3,4,1,1,1,0,  // 0x60
            1,1,3,1,3,3,1,2,  2,5,3,1,0,0,2,1,  // 0x70
            //
            4,1,1,2,3,3,7,1,  1,1,2,5,1,1,3,2,  // 0x80
            1,0,0,0,0,1,5,1,  1,0,0,5,9,2,3,1,  // 0x90
            //
            1,0,5,2,1,1,1,3,  3,0,0,2,0,0,2,0,  // 0xA0
            2,4,1,0,0,0,3,0,  0,0,0,2,3,3,3,2,  // 0xB0
            //
            5,0,0,2,1,1,0,2,  2,2,0,2,1,1,8,6,  // 0xC0
            4,1,2,4,6,4,1,0,  3,1,0,2,1,6,1,0,  // 0xD0
            //
            1,4,1,0,1,2,1,0,  0,0,0,0,0,0,0,0,  // 0xE0
            0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,1   // 0xF0
        };
        public static int GetCommandLength(int opcode, int param1)
        {
            int length = CommandLengths[opcode];
            if (length == 0 && opcode == 0xBA)
                length = 2 + (param1 * 2);
            if (length == 0 && opcode == 0xC6)
                length = 2 + param1;
            if (length == 0)
                return 1;
            return length;
        }
    }
}
