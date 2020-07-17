using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Exceptions;

namespace AddressLabelUtilityCore.Csv.IO
{
    public static class CsvWriter
    {
        public static void Write<T>(string path, IEnumerable<T> records)
            where T : ICsvModel
        {
            try
            {
                using var writer = new StreamWriter(path, false, EncodesResolver.Resolve<T>());
                using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
                csvWriter.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve<T>();
                csvWriter.WriteRecords(records);
            }
            catch (IOException ex)
            {
                throw new CsvIOException("CSVファイルに書き込めません", ex);
            }
            catch (Exception ex)
            {
                throw new CsvIOException("CSVファイルの書き込み時に不明なエラーが発生しました", ex);
            }
        }

        public static void Write(Type type, string path, IEnumerable<object> records)
        {
            try
            {
                using var writer = new StreamWriter(path, false, EncodesResolver.Resolve(type));
                using var csvWriter = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
                csvWriter.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve(type);
                csvWriter.WriteRecords(records);
            }
            catch (IOException ex)
            {
                throw new CsvIOException("CSVファイルに書き込めません", ex);
            }
            catch (Exception ex)
            {
                throw new CsvIOException("CSVファイルの書き込み時に不明なエラーが発生しました", ex);
            }
        }
    }
}
