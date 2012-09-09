using System;

namespace GW2SE.Base.PacketManagement
{
    public class PacketException : Exception
    {
        private string MSG;

        public PacketException() { }
        public PacketException(bool Incoming, string ErrorMessage) { IsIncoming = Incoming; MSG = ErrorMessage; }

        public bool IsIncoming { get; private set; }

        public override string Message
        {
            get
            {
                if (!String.IsNullOrEmpty(MSG))
                    return MSG;

                return base.Message;
            }
        }
    }
}
