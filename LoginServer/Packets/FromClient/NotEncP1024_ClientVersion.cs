using System;

using GW2SE.Base;
using GW2SE.Base.PacketManagement;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.LoginServer.Packets.FromClient
{
    [PacketHeader(1024)]
    public class NotEncP1024_ClientVersion : IPacketIn
    {
        public UInt16 Header { get; set; }
        public UInt16 Data1 { get; set; }
        public UInt32 ClientVersion { get; set; }
        public UInt32 Data3 { get; set; }
        public UInt32 Data4 { get; set; }

        public bool Initialize(NetworkMessage Message)
        {
            DataUtilities.InitializeStream(Message.PacketData);
            Header = DataUtilities.ToUInt16(Message.PacketData);
            Data1 = DataUtilities.ToUInt16(Message.PacketData);
            ClientVersion = DataUtilities.ToUInt32(Message.PacketData);
            Data3 = DataUtilities.ToUInt32(Message.PacketData);
            Data4 = DataUtilities.ToUInt32(Message.PacketData);

            return true;
        }

        public void Handle(NetworkMessage Message)
        {
            GW2SE.PluginSystem.Events.LoginServer.OnClientVersionReceived(this);
        }
    }
}
