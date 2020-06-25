using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class LayoutCalculatingException : LayoutException
    {
        public LayoutCalculatingException() : base() { }
        public LayoutCalculatingException(string message) : base(message) { }
        public LayoutCalculatingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
