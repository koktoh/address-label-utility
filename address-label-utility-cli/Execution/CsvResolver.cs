using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Csv.Converter;

namespace AddressLabelUtilityCli.Execution
{
    internal static class CsvResolver
    {
        private static readonly IReadOnlyDictionary<CsvKind, ConvertKind> _kindDict = new Dictionary<CsvKind, ConvertKind>
        {
            { CsvKind.Default, ConvertKind.デフォルト },
            { CsvKind.Booth, ConvertKind.BOOTH },
            { CsvKind.ClickPost, ConvertKind.クリックポスト },
        };

        private static readonly IReadOnlyDictionary<ConvertKind, CsvKind> _argumentDict = new Dictionary<ConvertKind, CsvKind>
        {
            { ConvertKind.デフォルト, CsvKind.Default },
            { ConvertKind.BOOTH, CsvKind.Booth },
            { ConvertKind.クリックポスト, CsvKind.ClickPost },
        };

        public static Type ResolveType(IArgument argument)
        {
            if (argument.ArgumentKind != ArgumentKind.SrcType1
                && argument.ArgumentKind != ArgumentKind.SrcType2
                && argument.ArgumentKind != ArgumentKind.DestType)
            {
                throw new ArgumentException();
            }

            if (argument.IsDefinedEnumValue<CsvKind>()
                    && _kindDict.TryGetValue(argument.GetArgumentAsEnum<CsvKind>(), out var result))
            {
                return ConvertTypeResolver.Resolve(result);
            }

            var list = Enum.GetNames(typeof(CsvKind))
                .Select(x => x.ToLower());

            throw new ArgumentException($"正しい種別を指定してください : {list.Join(", ")}");
        }

        public static string ResolveArgument(Type type)
        {
            var target = ConvertKindResolver.Resolve(type);

            if (_argumentDict.TryGetValue(target, out var result))
            {
                return result.ToString().ToLower();
            }

            throw new ArgumentException();
        }
    }
}
