using System;
using AddressLabelUtilityCore.Csv.Converter;

namespace AddressLabelUtility.Models.Csv
{
    internal class CsvBuildContext
    {
        public ConvertKind ConvertFrom { get; set; } = ConvertKind.デフォルト;
        public ConvertKind ConvertTo { get; set; } = ConvertKind.デフォルト;
        public Type TypeOfConvertFrom => ConvertTypeResolver.Resolve(this.ConvertFrom);
        public Type TypeOfConvertTo => ConvertTypeResolver.Resolve(this.ConvertTo);
        public string SrcPath { get; set; }
        public string DestPath { get; set; }
    }
}
