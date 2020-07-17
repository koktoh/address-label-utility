using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Converter.Default
{
    public class DefaultConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            return records;
        }
    }
}
