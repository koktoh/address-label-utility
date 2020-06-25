using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class PdfIOException : PdfException
    {
        public PdfIOException() : base() { }
        public PdfIOException(string message) : base(message) { }
        public PdfIOException(string message, Exception innerException) : base(message, innerException) { }
    }
}
