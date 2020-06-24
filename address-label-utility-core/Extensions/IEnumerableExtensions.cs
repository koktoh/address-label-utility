using System.Collections.Generic;
using System.Linq;

namespace AddressLabelUtilityCore.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> GroupByCount<T>(this IEnumerable<T> source, int count)
        {
            return source.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / count)
                .Select(x => x.Select(y => y.Value));
        }
    }
}
