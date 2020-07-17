using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Exceptions;

namespace AddressLabelUtilityCore.Csv.Converter
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
            try
            {
                var defaultRecords = this.ConvartToDefaultCsvModel(records);
                var destRecords = this.ConvertFromDefaultCsvModel(defaultRecords);

                return destRecords.ToList();
            }
            catch (Exception ex)
            {
                throw new CsvConvertException("データの変換に失敗しました", ex);
            }
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
