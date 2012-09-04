using System;

namespace GW2SE.Base.PacketManagement
{
    public class PacketException : Exception
    {
        private string MSG;
        private Exception Inner;

        public PacketException() { }
        public PacketException(bool Incoming, string ErrorMessage) { IsIncoming = Incoming; MSG = ErrorMessage; }
        public PacketException(bool Incoming, string ErrorMessage, Exception InnerException) { IsIncoming = Incoming; MSG = ErrorMessage; Inner = InnerException; }

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

        public Exception InnerException
        {
            get
            {
                return Inner;
            }
        }
    }
}
