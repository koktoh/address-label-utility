using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.IO
{
    public static class CsvReader
    {
        public static IEnumerable<T> Read<T>(string path)
            where T : ICsvModel
        {
            using var reader = new StreamReader(path, EncodesResolver.Resolve<T>());
            using var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve<T>();
            return csvReader.GetRecords<T>().ToList();
        }

        public static IEnumerable<object> Read(Type type, string path)
        {
            using var reader = new StreamReader(path, EncodesResolver.Resolve(type));
            using var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve(type);
            return csvReader.GetRecords(type).ToList();
        }
    }
}
