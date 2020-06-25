using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class LabelException : Exception
    {
        public LabelException() : base() { }
        public LabelException(string message) : base(message) { }
        public LabelException(string message, Exception innerException) : base(message, innerException) { }
    }
}
