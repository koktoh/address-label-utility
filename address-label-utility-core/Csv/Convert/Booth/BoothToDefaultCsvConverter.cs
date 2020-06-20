using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Convert.Booth
{
    internal class BoothToDefaultCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            return records.Cast<DefaultAddressCsvModel>();
        }
    }
}
