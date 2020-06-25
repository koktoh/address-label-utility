using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class LayoutException : Exception
    {
        public LayoutException() : base() { }
        public LayoutException(string message) : base(message) { }
        public LayoutException(string message, Exception innerException) : base(message, innerException) { }
    }
}
