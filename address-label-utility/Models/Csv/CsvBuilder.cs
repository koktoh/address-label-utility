﻿using System.Linq;
using AddressLabelUtilityCore.Csv.Converter;
using AddressLabelUtilityCore.Csv.IO;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtility.Models.Csv
{
    internal class CsvBuilder
    {
        private readonly IConverter _converter;
        private readonly CsvBuildingContext _context;

        public CsvBuilder(CsvBuildingContext context)
        {
            this._converter = ConverterFactory.Create(context.ConvertFrom, context.ConvertTo);
            this._context = context;
        }

        public void Build()
        {
            var csv = CsvReader.Read(this._context.TypeOfConvertFrom, this._context.SrcPath).Cast<ICsvModel>();
            var dest = this._converter.Convert(csv);
            CsvWriter.Write(this._context.TypeOfConvertTo, this._context.DestPath, dest);
        }
    }
}
