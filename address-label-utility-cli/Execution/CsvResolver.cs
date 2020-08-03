using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCli.Extensions;

namespace AddressLabelUtilityCli.Execution
{
    internal static class CsvResolver
    {
        private static readonly IReadOnlyDictionary<CsvKind, AddressLabelUtilityCore.Csv.CsvKind> _kindDict = new Dictionary<CsvKind, AddressLabelUtilityCore.Csv.CsvKind>
        {
            { CsvKind.Default, AddressLabelUtilityCore.Csv.CsvKind.デフォルト },
            { CsvKind.Booth, AddressLabelUtilityCore.Csv.CsvKind.BOOTH },
            { CsvKind.ClickPost, AddressLabelUtilityCore.Csv.CsvKind.クリックポスト },
        };

        private static readonly IReadOnlyDictionary<AddressLabelUtilityCore.Csv.CsvKind, CsvKind> _argumentDict = new Dictionary<AddressLabelUtilityCore.Csv.CsvKind, CsvKind>
        {
            { AddressLabelUtilityCore.Csv.CsvKind.デフォルト, CsvKind.Default },
            { AddressLabelUtilityCore.Csv.CsvKind.BOOTH, CsvKind.Booth },
            { AddressLabelUtilityCore.Csv.CsvKind.クリックポスト, CsvKind.ClickPost },
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
                return AddressLabelUtilityCore.Csv.CsvTypeResolver.Resolve(result);
            }

            var list = Enum.GetNames(typeof(CsvKind))
                .Select(x => x.ToLower());

            throw new ArgumentException($"正しい種別を指定してください : {list.Join(", ")}");
        }

        public static string ResolveArgument(Type type)
        {
            var target = AddressLabelUtilityCore.Csv.CsvKindResolver.Resolve(type);

            if (_argumentDict.TryGetValue(target, out var result))
            {
                return result.ToString().ToLower();
            }

            throw new ArgumentException();
        }
    }
}
