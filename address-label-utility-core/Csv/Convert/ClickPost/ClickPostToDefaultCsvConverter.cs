using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Csv.Convert.ClickPost
{
    internal class ClickPostToDefaultCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            return records.CopyTo<DefaultAddressCsvModel>();
        }
    }
}
