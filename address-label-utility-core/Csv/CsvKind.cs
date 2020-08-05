using System;
using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv
{
    public enum CsvKind
    {
        デフォルト,
        BOOTH,
        クリックポスト,
    }

    public static class CsvTypeResolver
    {
        private static readonly IReadOnlyDictionary<CsvKind, Type> _typeDict = new Dictionary<CsvKind, Type>
        {
            { CsvKind.デフォルト, typeof(DefaultAddressCsvModel) },
            { CsvKind.BOOTH, typeof(BoothAddressCsvModel) },
            { CsvKind.クリックポスト, typeof(ClickPostAddressCsvModel) },
        };

        public static Type Resolve(CsvKind kind)
        {
            if (_typeDict.TryGetValue(kind, out var result))
            {
                return result;
            }

            return typeof(DefaultAddressCsvModel);
        }
    }

    public static class CsvKindResolver
    {
        private static readonly IReadOnlyDictionary<Type, CsvKind> _kindDict = new Dictionary<Type, CsvKind>
        {
            { typeof(DefaultAddressCsvModel), CsvKind.デフォルト },
            { typeof(BoothAddressCsvModel), CsvKind.BOOTH },
            { typeof(ClickPostAddressCsvModel), CsvKind.クリックポスト },
        };

        public static CsvKind Resolve(Type type)
        {
            if (_kindDict.TryGetValue(type, out var result))
            {
                return result;
            }

            return CsvKind.デフォルト;
        }

        public static CsvKind Resolve<T>()
            where T : ICsvModel
        {
            return Resolve(typeof(T));
        }
    }
}
