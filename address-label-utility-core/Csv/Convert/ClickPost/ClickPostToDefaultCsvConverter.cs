using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Convert.ClickPost
{
    internal class ClickPostToDefaultCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            return records.Cast<DefaultAddressCsvModel>();
        }
    }
}
