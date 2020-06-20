using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.IO
{
    public static class CsvWriter
    {
        public static void Write<T>(string path, IEnumerable<T> records)
            where T : ICsvModel
        {
            using var writer = new StreamWriter(path, false, EncodesResolver.Resolve<T>());
            using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve<T>();
            csvWriter.WriteRecords(records);
        }

        public static void Write(Type type,string path, IEnumerable<object> records)
        {
            using var writer = new StreamWriter(path, false, EncodesResolver.Resolve(type));
            using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve(type);
            csvWriter.WriteRecords(records);
        }
    }
}
