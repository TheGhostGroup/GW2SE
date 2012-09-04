using System;
using System.IO;
using System.Net.Sockets;

namespace GW2SE.Base.NetworkManagement
{
    public class NetworkMessage
    {
        private MemoryStream packetStream;
        public MemoryStream PacketData { get { return packetStream; } }

        public NetworkMessage(MemoryStream PacketStream, NetID ID)
        {
            packetStream = PacketStream;
            Client = ID;
        }

        public NetID Client { get; private set; }

        public UInt16 Header
        {
            get
            {
                if (packetStream.Length >= 2)
                {
                    var tmp = packetStream.Position;
                    packetStream.Seek(0, SeekOrigin.Begin);

                    var buffer = new byte[2];
                    packetStream.Read(buffer, 0, 2);

                    packetStream.Position = tmp;
                    return BitConverter.ToUInt16(buffer, 0);
                }
                else
                {
                    return (UInt16)0xFFFF;
                }
            }
        }
    }
}
