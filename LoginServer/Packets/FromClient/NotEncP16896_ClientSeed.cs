using System;

using GW2SE.Base;
using GW2SE.Base.PacketManagement;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.LoginServer.Packets.FromClient
{
    [PacketHeader(16896)]
    public class NotEncP16896_ClientSeed : IPacketIn
    {
        public UInt16 Header { get; private set; }
        public byte[] Seed { get; private set; }

        public bool Initialize(NetworkMessage Message)
        {
            DataUtilities.InitializeStream(Message.PacketData);
            Header = DataUtilities.ToUInt16(Message.PacketData);
            Seed = DataUtilities.ToBytes(Message.PacketData, 64);
            return true;
        }

        public void Handle(NetworkMessage Message)
        {
            Client client;
            if (!NetworkManager.Instance.Clients.TryGetClient(Message.Client, out client))
                return;

            Console.WriteLine("End of progress...");
            client.Disconnect();
        }
    }
}