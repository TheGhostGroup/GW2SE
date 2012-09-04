using System;

namespace GW2SE.Base.NetworkManagement
{
    public class NetID
    {
        public int Value { get; private set; }

        public NetID(int Value)
        {
            this.Value = Value;
        }
    }
}
