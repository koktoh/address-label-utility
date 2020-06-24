using System;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Csv.IO
{
    public static class CsvHasHeaderResolver
    {
        public static bool Resolve<T>()
            where T : ICsvModel
        {
            return Resolve(typeof(T));
        }

        public static bool Resolve(Type type)
        {
            try
            {
                return (bool)type.GetProperty("HasHeader").GetValue(Activator.CreateInstance(type));
            }
            catch
            {
                return false;
            }
        }
    }
}
