using AddressLabelUtilityCore.Csv.Convert.Booth;
using AddressLabelUtilityCore.Csv.Convert.ClickPost;
using AddressLabelUtilityCore.Csv.Convert.Default;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.Convert
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

        public static IConverter Create(ConvertKind from, ConvertKind to)
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

        private static IConverter GetToDefaultConverter(ConvertKind kind)
        {
            return kind switch
            {
                ConvertKind.BOOTH => new BoothToDefaultCsvConverter(),
                ConvertKind.クリックポスト => new ClickPostToDefaultCsvConverter(),
                _ => new DefaultConverter(),
            };
        }

        private static IConverter GetFromDefaultConverter<TDest>()
            where TDest : ICsvModel
        {
            if (typeof(TDest) == typeof(BoothAddressCsvModel))
            {
                return new BoothToDefaultCsvConverter();
            }
            else if (typeof(TDest) == typeof(ClickPostAddressCsvModel))
            {
                return new ClickPostToDefaultCsvConverter();
            }
            else
            {
                return new DefaultConverter();
            }
        }

        private static IConverter GetFromDefaultConverter(ConvertKind kind)
        {
            return kind switch
            {
                ConvertKind.BOOTH => new BoothToDefaultCsvConverter(),
                ConvertKind.クリックポスト => new ClickPostToDefaultCsvConverter(),
                _ => new DefaultConverter(),
            };
        }
    }
}
