using System;
using System.IO;

namespace GW2SE.Base
{
    public class DataUtilities
    {
        public static MemoryStream InitializeStream(MemoryStream Stream)
        {
            Stream.Seek(0, SeekOrigin.Begin);
            return Stream;
        }

        public static UInt16 ToUInt16(MemoryStream Stream)
        {
            var buffer = new byte[2];
            Stream.Read(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static UInt32 ToUInt32(MemoryStream Stream)
        {
            var buffer = new byte[4];
            Stream.Read(buffer, 0, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static UInt64 ToUInt64(MemoryStream Stream)
        {
            var buffer = new byte[8];
            Stream.Read(buffer, 0, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static String ToString(MemoryStream Stream, UInt16 Length)
        {
            var buffer = new byte[Length];
            Stream.Read(buffer, 0, Length);
            return System.Text.Encoding.UTF8.GetString(buffer, 0, Length);
        }

        public static Byte[] ToBytes(MemoryStream Stream, UInt16 Length)
        {
            var buffer = new byte[Length];
            Stream.Read(buffer, 0, Length);
            return buffer;
        }
    }
}
