using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Csv.Converter.Booth
{
    internal class BoothToDefaultCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            return records.CopyTo<DefaultAddressCsvModel>();
        }
    }
}
