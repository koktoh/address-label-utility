using AddressLabelUtilityCli.Helper;

namespace AddressLabelUtilityCli.Arguments.Csv
{
    internal class CsvArgumentResolver : ArgumentResolverBase
    {
        public CsvArgumentResolver() : base(ArgumentHelper.GetCsvArguments()) { }
    }
}
