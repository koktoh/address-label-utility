using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Convert
{
    public class Converter : IConverter
    {
        protected readonly IConverter _toDefaultConverter;
        protected readonly IConverter _fromDefaultConverter;

        public Converter(IConverter toDefaultConerter, IConverter fromDefaultConverter)
        {
            this._toDefaultConverter = toDefaultConerter;
            this._fromDefaultConverter = fromDefaultConverter;
        }

        public IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records)
        {
            var defaultRecords = this.ConvartToDefaultCsvModel(records);
            var destRecords = this.ConvertFromDefaultCsvModel(defaultRecords);

            return destRecords.ToList();
        }

        private IEnumerable<ICsvModel> ConvartToDefaultCsvModel(IEnumerable<ICsvModel> records)
        {
            return this._toDefaultConverter.Convert(records);
        }

        private IEnumerable<ICsvModel> ConvertFromDefaultCsvModel(IEnumerable<ICsvModel> records)
        {
            return this._fromDefaultConverter.Convert(records);
        }
    }
}
