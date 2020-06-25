using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Exceptions;
using CsvHelper;

namespace AddressLabelUtilityCore.Csv.IO
{
    public static class CsvReader
    {
        public static IEnumerable<T> Read<T>(string path)
            where T : ICsvModel
        {
            try
            {
                using var reader = new StreamReader(path, EncodesResolver.Resolve<T>());
                using var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
                csvReader.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve<T>();
                return csvReader.GetRecords<T>().ToList();
            }
            catch (Exception ex) when (ex is ValidationException || ex is BadDataException)
            {
                throw new CsvIOException("CSVファイルの書式が不正です", ex);
            }
            catch (IOException ex)
            {
                throw new CsvIOException("CSVファイルを読み込めません", ex);
            }
            catch (Exception ex)
            {
                throw new CsvIOException("CSVファイル読み込み時に不明なエラーが発生しました", ex);
            }
        }

        public static IEnumerable<object> Read(Type type, string path)
        {
            try
            {
                using var reader = new StreamReader(path, EncodesResolver.Resolve(type));
                using var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
                csvReader.Configuration.HasHeaderRecord = CsvHasHeaderResolver.Resolve(type);
                return csvReader.GetRecords(type).ToList();
            }
            catch (Exception ex) when (ex is ValidationException || ex is BadDataException)
            {
                throw new CsvIOException("CSVファイルの書式が不正です", ex);
            }
            catch (IOException ex)
            {
                throw new CsvIOException("CSVファイルを読み込めません", ex);
            }
            catch (Exception ex)
            {
                throw new CsvIOException("CSVファイル読み込み時に不明なエラーが発生しました", ex);
            }
        }
    }
}
