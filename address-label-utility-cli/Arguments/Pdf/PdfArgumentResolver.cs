using AddressLabelUtilityCli.Helper;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class PdfArgumentResolver : ArgumentResolverBase
    {
        public PdfArgumentResolver() : base(ArgumentHelper.GetPdfArguments()) { }
    }
}
