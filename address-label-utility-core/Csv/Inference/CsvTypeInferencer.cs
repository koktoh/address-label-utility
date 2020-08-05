using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using AddressLabelUtilityCore.Csv.Models;
using AddressLabelUtilityCore.Extensions;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Hnx8.ReadJEnc;

namespace AddressLabelUtilityCore.Csv.Inference
{
    public class CsvTypeInferencer
    {
        private readonly int _defaultCsvFieldCount;
        private readonly int _boothCsvFieldCount;
        private readonly int _clicPostCsvFieldCount;
        private readonly IReadOnlyCollection<string> _defaultCsvHeader;
        private readonly IReadOnlyCollection<string> _boothCsvHeaderList;
        private readonly IReadOnlyCollection<string> _clicPostCsvHeaderList;

        public CsvTypeInferencer()
        {
            this._defaultCsvFieldCount = this.GetFieldCount<DefaultAddressCsvModel>();
            this._boothCsvFieldCount = this.GetFieldCount<BoothAddressCsvModel>();
            this._clicPostCsvFieldCount = this.GetFieldCount<ClickPostAddressCsvModel>();

            this._defaultCsvHeader = this.GetHeaderList<DefaultAddressCsvModel>().ToList();
            this._boothCsvHeaderList = this.GetHeaderList<BoothAddressCsvModel>().ToList();
            this._clicPostCsvHeaderList = this.GetHeaderList<ClickPostAddressCsvModel>().ToList();
        }

        public Type Infer(string path)
        {
            var file = new FileInfo(path);

            using var reader = new FileReader(file);
            var encoding = reader.Read(file).GetEncoding();

            using var sr = new StreamReader(path, encoding);
            using var parser = new CsvParser(sr, CultureInfo.InvariantCulture);
            var record = parser.Read();

            if (this._defaultCsvFieldCount == record.Length && this._defaultCsvHeader.SequenceEqual(record))
            {
                return typeof(DefaultAddressCsvModel);
            }
            else if (this._clicPostCsvFieldCount == record.Length && this._clicPostCsvHeaderList.SequenceEqual(record))
            {
                return typeof(ClickPostAddressCsvModel);
            }
            else if (this._boothCsvFieldCount == record.Length && this._boothCsvHeaderList.SequenceEqual(record))
            {
                return typeof(BoothAddressCsvModel);
            }
            else
            {
                return typeof(DefaultAddressCsvModel);
            }
        }

        private int GetFieldCount<T>()
            where T : ICsvModel
        {
            return typeof(T).GetProperties()
                .Select(x => x.GetCustomAttribute<IndexAttribute>())
                .Where(x => x != null)
                .Count();
        }

        private IEnumerable<string> GetHeaderList<T>()
            where T : ICsvModel
        {
            return typeof(T).GetProperties()
                .Select(x => x.GetCustomAttribute<NameAttribute>())
                .Where(x => x != null)
                .SelectMany(x => x.Names)
                .Where(x => x.HasMeaningfulValue());
        }
    }
}
