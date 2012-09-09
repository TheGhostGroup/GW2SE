using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using GW2SE.Base.NetworkManagement;

namespace GW2SE.Base.PacketManagement
{
    public class PacketManager
    {
        public Client Client { get; private set; }

        internal PacketManager(Client Client)
        {
            this.Client = Client;
        }

        public void ProcessPackets(NetworkMessage[] NetworkMessages)
        {
            foreach (NetworkMessage message in NetworkMessages)
            {
                ProcessPacket(message);
            }

            Client.ClearQueue();
        }

        public void ProcessPacket(NetworkMessage NetworkMessage)
        {
            IPacketIn packetTemplate;
            if (!PacketRegistrator.Instance.PacketIn.TryGetValue(NetworkMessage.Header, out packetTemplate))
            {
                Console.WriteLine("Unhandled packet [" + NetworkMessage.Header.ToString() + "]. The packet is not supported by this server build");
                return;
            }

            if (packetTemplate.Initialize(NetworkMessage))
            {
                Console.WriteLine();
                Console.WriteLine("---> " + BitConverter.ToString(NetworkMessage.PacketData.ToArray()).Replace("-", " "));
                packetTemplate.Handle(NetworkMessage);
            }
        }
    }
}
