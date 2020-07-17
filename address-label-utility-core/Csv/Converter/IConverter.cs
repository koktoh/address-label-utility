using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Converter
{
    public interface IConverter
    {
        IEnumerable<ICsvModel> Convert(IEnumerable<ICsvModel> records);
    }
}
