using System;
using GW2SE.Base.NetworkManagement;

namespace GW2SE.Base.PacketManagement
{
    public interface IPacketIn
    {
        bool Initialize(NetworkMessage Message);
        void Handle(NetworkMessage Message);
    }

    public interface IPacketOut
    {
        bool Initialize();
        NetworkMessage Handle();
    }
}
