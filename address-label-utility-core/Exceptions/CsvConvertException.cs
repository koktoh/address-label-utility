using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class CsvConvertException : CsvException
    {
        public CsvConvertException() : base() { }
        public CsvConvertException(string message) : base(message) { }
        public CsvConvertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
