using System;
using System.Collections.Generic;

namespace GW2SE.Base.PacketManagement
{
    public class PacketRegistrator
    {
        public Dictionary<UInt16, IPacketIn> PacketIn { get; private set; }

        private static readonly PacketRegistrator instance = new PacketRegistrator();

        public static PacketRegistrator Instance
        {
            get
            {
                return instance;
            }
        }

        public PacketRegistrator()
        {
            PacketIn = new Dictionary<UInt16, IPacketIn>();
        }

        public void RegisterPacket(Type PacketType)
        {
            var header = -1;
            var attributes = PacketType.GetCustomAttributes(typeof(PacketHeader), false);
            if (attributes.Length == 1)
            {
                header = ((PacketHeader)attributes[0]).Header;
            }

            if (!typeof(IPacketIn).IsAssignableFrom(PacketType))
            {
                return;
            }

            var packet = (IPacketIn)Activator.CreateInstance(PacketType);

            if (header >= 0 && header <= 65535)
                PacketIn.Add((UInt16)header, packet);
            else
                Console.WriteLine("Invalid packet header in class " + packet.GetType().Name);
        }

        public void RegisterPackets(Type[] PacketTypes)
        {
            foreach (Type pType in PacketTypes)
            {
                RegisterPacket(pType);
            }
        }
    }
}
