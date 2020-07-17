using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Csv.Converter.ClickPost
{
    internal class DefaultToClickPostCsvConverter : IConverter
    {
        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            var dest = records.CopyTo<ClickPostAddressCsvModel>();

            return dest.Select(x =>
            {
                if (x.Address5.HasMeaningfulValue())
                {
                    x.Address1 += x.Address2;
                    x.Address2 = x.Address3;
                    x.Address3 = x.Address4;
                    x.Address4 = x.Address5;
                    x.Address5 = string.Empty;
                }

                x.Item = x.Item.Length <= 15 ? x.Item : x.Item.Substring(0, 15);

                return x;
            });
        }
    }
}
