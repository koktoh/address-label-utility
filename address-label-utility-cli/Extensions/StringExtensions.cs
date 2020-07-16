using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AddressLabelUtilityCli.Extensions
{
    internal static class StringExtensions
    {
        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static string JoinWhiteSpace(this IEnumerable<string> source)
        {
            return source.Join(" ");
        }

        public static bool IsInteger(this string source)
        {
            return Regex.IsMatch(source, "^[0-9]+$");
        }

        public static bool IsNumeral(this string source)
        {
            return Regex.IsMatch(source, @"^[0-9]+\.{0,1}[0-9]*$");
        }
    }
}
