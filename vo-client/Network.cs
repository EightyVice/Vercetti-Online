using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GTASDK.ViceCity;
using LiteNetLib;
using LiteNetLib.Utils;

namespace vo_client
{   
    public static class Network
    {
        private enum PacketID : byte
        {
            None,
            ConnectionAccepted,
            PlayerDisconnected,
            PlayerJoined,
            PlayerUpdatePacket
        }
        public static NetManager client;
        public static NetPeer server;
        public static void Connect(IPEndPoint To)
        {
            EventBasedNetListener listener = new EventBasedNetListener();
            client = new NetManager(listener);
            client.Start();
            client.Connect(To, "VERCETTI ONLINE");
            
            // Events
            listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            
        }

        private static void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            PacketID id = (PacketID)reader.GetByte();
            Console.WriteLine($"[NETLOG] Data Received, ID: {(byte)id} ({id})");
            switch (id)
            {
                case PacketID.ConnectionAccepted:
                    Console.WriteLine("[NETLOG] Connected to server");
                    CHud.SetHelpMessage("Connected!!!");
                    server = peer;
                    break;

                case PacketID.PlayerJoined:
                    NetDataWriter x = new NetDataWriter();
                    Player p = new Player("f");
                    break;
            }

            reader.Recycle();
        }

        private static void SendUpdateData()
        {
            NetDataWriter playerSync = new NetDataWriter();
            playerSync.Put((byte)PacketID.PlayerUpdatePacket);
            playerSync.Put(Player.BuildPlayerData());
            server.Send(playerSync, DeliveryMethod.ReliableSequenced);
        }

        public static void Update()
        {
            SendUpdateData();
            client.PollEvents();
        }


    }
}
