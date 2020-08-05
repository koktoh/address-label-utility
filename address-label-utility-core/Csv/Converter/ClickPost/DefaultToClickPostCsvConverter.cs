using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                // 最大 30bytes (半角30文字、全角15文字)
                x.Item = this.TakeString(x.Item, 30);

                return x;
            });
        }

        private string TakeString(string source, int count)
        {
            var length = 0;

            var builder = new StringBuilder();

            foreach (var c in source)
            {
                length += this.GetByteCount(c, Encodes.ShiftJis);

                if (length > count)
                {
                    break;
                }

                builder.Append(c);
            }

            return builder.ToString();
        }

        private int GetByteCount(char c, Encodes encodes)
        {
            return encodes.GetEncoding().GetByteCount(new[] { c });
        }
    }
}
