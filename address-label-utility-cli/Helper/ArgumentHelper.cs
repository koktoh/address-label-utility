using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Helper
{
    internal static class ArgumentHelper
    {
        private static readonly IReadOnlyCollection<Type> _argumentTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => !x.IsInterface && x.GetInterface(nameof(IArgument)) != null)
            .ToList();

        public static IEnumerable<IArgument> GetArguments()
        {
            return GetCommonArguments()
                .Concat(GetCsvArguments())
                .Concat(GetPdfArguments());
        }

        public static IEnumerable<IArgument> GetCommonArguments()
        {
            return _argumentTypes
                .Where(x => x.Namespace == "AddressLabelUtilityCli.Arguments.Common")
                .Select(x => Activator.CreateInstance(x) as IArgument);
        }

        public static IEnumerable<IArgument> GetCsvArguments()
        {
            return _argumentTypes
                .Where(x => x.Namespace == "AddressLabelUtilityCli.Arguments.Csv")
                .Select(x => Activator.CreateInstance(x) as IArgument);
        }

        public static IEnumerable<IArgument> GetPdfArguments()
        {
            return _argumentTypes
                .Where(x => x.Namespace == "AddressLabelUtilityCli.Arguments.Pdf")
                .Select(x => Activator.CreateInstance(x) as IArgument);
        }
    }
}
