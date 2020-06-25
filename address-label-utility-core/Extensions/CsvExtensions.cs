using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Csv.Models;

namespace AddressLabelUtilityCore.Extensions
{
    public static class CsvExtensions
    {
        public static TDest CopyTo<TDest>(this ICsvModel source)
            where TDest : ICsvModel
        {
            var ins = (TDest)Activator.CreateInstance(typeof(TDest));

            ins.Encode = source.Encode;
            ins.HasHeader = source.HasHeader;
            ins.Item = source.Item;

            ins.PostCode = source.PostCode;
            ins.Address1 = source.Address1;
            ins.Address2 = source.Address2;
            ins.Address3 = source.Address3;
            ins.Address4 = source.Address4;
            ins.Address5 = source.Address5;
            ins.Name = source.Name;
            ins.NameSuffix = source.NameSuffix;
            ins.PhoneNumber = source.PhoneNumber;

            return ins;
        }

        public static IEnumerable<TDest> CopyTo<TDest>(this IEnumerable<ICsvModel> source)
            where TDest : ICsvModel
        {
            return source.Select(x => x.CopyTo<TDest>());
        }
    }
}
