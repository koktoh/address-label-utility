using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class CsvIOException : CsvException
    {
        public CsvIOException() : base() { }
        public CsvIOException(string message) : base(message) { }
        public CsvIOException(string message, Exception innerException) : base(message, innerException) { }

    }
}
