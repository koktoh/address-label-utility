using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class PdfException : Exception
    {
        public PdfException() : base() { }
        public PdfException(string message) : base(message) { }
        public PdfException(string message, Exception innerException) : base(message, innerException) { }
    }
}
