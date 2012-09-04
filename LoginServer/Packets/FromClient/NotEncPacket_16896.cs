using System;

using GW2SE.Base;
using GW2SE.Base.PacketManagement;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.LoginServer.Packets.FromClient
{
    [PacketInformation(16896, true)]
    public class NotEncPacket_16896 : IPacket
    {
        public UInt16 Header { get; private set; }
        public byte[] Seed { get; private set; }

        public bool Initialize(ref NetworkMessage Message)
        {
            DataUtilities.InitializeStream(Message.PacketData);
            Header = DataUtilities.ToUInt16(Message.PacketData);
            Seed = DataUtilities.ToBytes(Message.PacketData, 64);
            return true;
        }

        public void Handle(ref NetworkMessage Message)
        {
            
        }
    }
}