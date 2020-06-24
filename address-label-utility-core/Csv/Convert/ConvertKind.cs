using System;
using System.Collections.Generic;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Convert
{
    public enum ConvertKind
    {
        デフォルト,
        BOOTH,
        クリックポスト,
    }

    public static class ConvertTypeResolver
    {
        private static readonly IReadOnlyDictionary<ConvertKind, Type> _typeDict = new Dictionary<ConvertKind, Type>
        {
            { ConvertKind.デフォルト, typeof(DefaultAddressCsvModel) },
            { ConvertKind.BOOTH, typeof(BoothAddressCsvModel) },
            { ConvertKind.クリックポスト, typeof(ClickPostAddressCsvModel) },
        };

        public static Type Resolve(ConvertKind kind)
        {
            if (_typeDict.TryGetValue(kind, out var result))
            {
                return result;
            }

            return typeof(DefaultAddressCsvModel);
        }
    }

    public static class ConvertKindResolver
    {
        private static readonly IReadOnlyDictionary<Type, ConvertKind> _kindDict = new Dictionary<Type, ConvertKind>
        {
            { typeof(DefaultAddressCsvModel), ConvertKind.デフォルト },
            { typeof(BoothAddressCsvModel), ConvertKind.BOOTH },
            { typeof(ClickPostAddressCsvModel), ConvertKind.クリックポスト },
        };

        public static ConvertKind Resolve(Type type)
        {
            if (_kindDict.TryGetValue(type, out var result))
            {
                return result;
            }

            return ConvertKind.デフォルト;
        }

        public static ConvertKind Resolve<T>()
            where T : ICsvModel
        {
            return Resolve(typeof(T));
        }
    }
}
