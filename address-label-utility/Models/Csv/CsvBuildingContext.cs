using System;
using AddressLabelUtilityCore.Csv;

namespace AddressLabelUtility.Models.Csv
{
    internal class CsvBuildingContext
    {
        public CsvKind ConvertFrom { get; set; } = CsvKind.デフォルト;
        public CsvKind ConvertTo { get; set; } = CsvKind.デフォルト;
        public Type TypeOfConvertFrom => CsvTypeResolver.Resolve(this.ConvertFrom);
        public Type TypeOfConvertTo => CsvTypeResolver.Resolve(this.ConvertTo);
        public string SrcPath { get; set; }
        public string DestPath { get; set; }
    }
}
