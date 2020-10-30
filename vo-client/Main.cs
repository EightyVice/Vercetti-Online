using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTASDK;
using GTASDK.ViceCity;

namespace vo_client
{
    public class Main : VCPlugin
    {

        static bool OnceExecution = true;
        static string ip = "";
        static string port = "";
        static string nick = "";
 
        public Main(string[] cmdLine)
        {
            //gta-vc.exe <ip> <port> <nickname>
            if (cmdLine.Length == 4) {
                ip = cmdLine[1];
                port = cmdLine[2];
                nick = cmdLine[3];
                AllocConsole();
                Console.WriteLine($"[LOG] Starting VC with params {ip} {port} {nick}");
                Patches.PreGamePatches();
                GameTicking += GameTick;
            }
        }

        private void GameTick()
        {
            if (OnceExecution)
            {
                Network.Connect(new IPEndPoint(IPAddress.Parse(ip), int.Parse(port)));
                CHud.SetHelpMessage("Connecting...");
                OnceExecution = false;
            }

            Network.Update();
        }
    }
}
