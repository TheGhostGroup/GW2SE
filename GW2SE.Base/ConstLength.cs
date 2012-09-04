using System;

namespace GW2SE.Base
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConstLength : System.Attribute
    {
        public UInt16 Length { get; private set; }

        public ConstLength(UInt16 Length)
        {
            this.Length = Length;
        }
    }
}