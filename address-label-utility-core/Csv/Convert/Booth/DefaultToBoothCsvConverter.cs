using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Csv.Convert.Booth
{
    internal class DefaultToBoothCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            var dest = records.Cast<BoothAddressCsvModel>();

            return dest.Select(x =>
            {
                if (x.Address4.HasMeaningfulValue() || x.Address5.HasMeaningfulValue())
                {
                    x.Address1 += x.Address2;
                    x.Address2 = x.Address3 + x.Address4;
                    x.Address3 = x.Address5;
                    x.Address4 = string.Empty;
                    x.Address5 = string.Empty;
                }

                return x;
            });
        }
    }
}
