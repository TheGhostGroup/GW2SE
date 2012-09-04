using System;

using GW2SE.Base;
using GW2SE.Base.PacketManagement;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.LoginServer.Packets.FromClient
{
    [PacketInformation(1024, true)]
    public class NotEncPacket_1024 : IPacket
    {
        public UInt16 Header { get; set; }
        public UInt16 Data1 { get; set; }
        public UInt32 ClientVersion { get; set; }
        public UInt32 Data3 { get; set; }
        public UInt32 Data4 { get; set; }

        public bool Initialize(ref NetworkMessage Message)
        {
            DataUtilities.InitializeStream(Message.PacketData);
            Header = DataUtilities.ToUInt16(Message.PacketData);
            Data1 = DataUtilities.ToUInt16(Message.PacketData);
            ClientVersion = DataUtilities.ToUInt32(Message.PacketData);
            Data3 = DataUtilities.ToUInt32(Message.PacketData);
            Data4 = DataUtilities.ToUInt32(Message.PacketData);

            if (ClientVersion != Constants.ServerVersion)
            {
                Client client;

                if (!NetworkManager.Instance.Clients.TryGetClient(Message.Client, out client))
                {
                    Console.WriteLine();
                    Console.WriteLine("Could not disconnect a client. NetID: " + Message.Client.ToString());

                    return false;
                }

                Console.WriteLine();
                Console.WriteLine("The client version does not match the server version. ClientVersion: " + ClientVersion.ToString());

                client.Disconnect();

                return false;
            }

            return true;
        }

        public void Handle(ref NetworkMessage Message)
        {
            Console.WriteLine("ClientVersion: " + ClientVersion.ToString());
        }
    }
}
