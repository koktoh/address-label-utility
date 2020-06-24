using System;
using System.Collections.Generic;

namespace AddressLabelUtilityCore.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool HasMeaningfulValue(this string source)
        {
            return !source.IsNullOrWhiteSpace();
        }

        public static IEnumerable<string> SplitNewLine(this string source, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(Environment.NewLine, options);
        }

    }
}
