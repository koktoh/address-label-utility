using AddressLabelUtilityCore.Csv.Converter.Booth;
using AddressLabelUtilityCore.Csv.Converter.ClickPost;
using AddressLabelUtilityCore.Csv.Converter.Default;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Converter
{
    public static class ConverterFactory
    {
        public static IConverter Create<TSource, TDest>()
            where TSource : ICsvModel
            where TDest : ICsvModel
        {
            var toDefaultConverter = GetToDefaultConverter<TSource>();
            var fromDefaultConverter = GetFromDefaultConverter<TDest>();

            return new Converter(toDefaultConverter, fromDefaultConverter);
        }

        public static IConverter Create(CsvKind from, CsvKind to)
        {
            var toDefaultConverter = GetToDefaultConverter(from);
            var fromDefaultConverter = GetFromDefaultConverter(to);

            return new Converter(toDefaultConverter, fromDefaultConverter);
        }

        private static IConverter GetToDefaultConverter<TSource>()
            where TSource : ICsvModel
        {
            if (typeof(TSource) == typeof(BoothAddressCsvModel))
            {
                return new BoothToDefaultCsvConverter();
            }
            else if (typeof(TSource) == typeof(ClickPostAddressCsvModel))
            {
                return new ClickPostToDefaultCsvConverter();
            }
            else
            {
                return new DefaultConverter();
            }
        }

        private static IConverter GetToDefaultConverter(CsvKind kind)
        {
            return kind switch
            {
                CsvKind.BOOTH => new BoothToDefaultCsvConverter(),
                CsvKind.クリックポスト => new ClickPostToDefaultCsvConverter(),
                _ => new DefaultConverter(),
            };
        }

        private static IConverter GetFromDefaultConverter<TDest>()
            where TDest : ICsvModel
        {
            if (typeof(TDest) == typeof(BoothAddressCsvModel))
            {
                return new DefaultToBoothCsvConverter();
            }
            else if (typeof(TDest) == typeof(ClickPostAddressCsvModel))
            {
                return new DefaultToClickPostCsvConverter();
            }
            else
            {
                return new DefaultConverter();
            }
        }

        private static IConverter GetFromDefaultConverter(CsvKind kind)
        {
            return kind switch
            {
                CsvKind.BOOTH => new DefaultToBoothCsvConverter(),
                CsvKind.クリックポスト => new DefaultToClickPostCsvConverter(),
                _ => new DefaultConverter(),
            };
        }
    }
}
