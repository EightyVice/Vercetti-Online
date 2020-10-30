using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GTASDK;
namespace vo_client
{
    public static class Patches
    {
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(IntPtr lpAddress,
                                          IntPtr dwSize,
                                          uint flNewProtect,
                                          out uint lpflOldProtect);

        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        public static void PreGamePatches()
        {
            VirtualProtect( (IntPtr)0x401000,                   // Unprotect All game memory
                            (IntPtr)0x27CE00u, 
                            PAGE_EXECUTE_READWRITE, 
                            out uint protOut);


            Memory.WriteString(0x6886AC, "vo-s.scm");           // Load our scm
            Memory.WriteInt32(0x9B5F08, 5);                     // dwGameLoadState = 5
            Memory.MakeNop(0x4D0DA0, 7, false);                 // Never pause the game

            Memory.Write1bBool(0x869642, false);                // For skipping menu and start the game
            Memory.Write1bBool(0x869668, false);                // ...
            Memory.Write1bBool(0x86969C, true);                 // ...
            Memory.Write1bBool(0x869668, false);                // ...
            Memory.Write1bBool(0x869641, true);                 // ...

            
        }
    }
}
