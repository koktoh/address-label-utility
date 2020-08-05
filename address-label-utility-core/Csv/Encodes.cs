using System;
using System.Collections.Generic;
using System.Text;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv
{
    public enum Encodes
    {
        UTF8,
        ShiftJis,
    }

    public static class EncodesExtensions
    {
        private static readonly IReadOnlyDictionary<Encodes, Encoding> _encodesMap = new Dictionary<Encodes, Encoding>
        {
            { Encodes.UTF8, Encoding.UTF8 },
            { Encodes.ShiftJis, Encoding.GetEncoding("shift-jis") },
        };

        public static Encoding GetEncoding(this Encodes encode)
        {
            if (_encodesMap.TryGetValue(encode, out var result))
            {
                return result;
            }

            return Encoding.UTF8;
        }
    }

    public static class EncodesResolver
    {
        public static Encoding Resolve<T>()
            where T : ICsvModel
        {
            return Resolve(typeof(T));
        }

        public static Encoding Resolve(Type type)
        {
            try
            {
                var encode = (Encodes)type.GetProperty(nameof(ICsvModel.Encode))
                    .GetValue(Activator.CreateInstance(type));

                return encode.GetEncoding();
            }
            catch
            {
                return Encoding.UTF8;
            }
        }
    }
}
