using GTASDK;
using GTASDK.ViceCity;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vo_client
{
    
    public class Player : INetSerializable 
    {
        public string Name;
        public CVector Position;
        public Player(string PlayerName)
        {
            Name = PlayerName;
        }
        
        public static Player BuildPlayerData()
        {
            Player p = new Player("null");
            p.Position = CPed.FindPlayerPed().Placement.pos;
            return p;
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Position.X);
            writer.Put(Position.Y);
            writer.Put(Position.Z);
        }
        public void Deserialize(NetDataReader reader)
        {
            Position = new CVector(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
        }
    }
}
