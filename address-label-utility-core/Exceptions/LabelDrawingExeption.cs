using System;

namespace AddressLabelUtilityCore.Exceptions
{
    public class LabelDrawingExeption : LabelException
    {
        public LabelDrawingExeption() : base() { }
        public LabelDrawingExeption(string message) : base(message) { }
        public LabelDrawingExeption(string message, Exception innerException) : base(message, innerException) { }
    }
}
