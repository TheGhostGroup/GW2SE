using System;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.Base.PacketManagement
{
    public interface IPacket
    {
        bool Initialize(ref NetworkMessage Message);
        void Handle(ref NetworkMessage Message);
    }
}
