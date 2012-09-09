using System;

namespace GW2SE.Base.PacketManagement
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PacketHeader : System.Attribute
    {
        public PacketHeader(UInt16 Header)
        {
            this.Header = Header;
        }

        public UInt16 Header { get; private set; }
    }
}
