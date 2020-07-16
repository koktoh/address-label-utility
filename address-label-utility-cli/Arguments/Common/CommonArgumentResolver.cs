using AddressLabelUtilityCli.Helper;

namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class CommonArgumentResolver : ArgumentResolverBase
    {
        public CommonArgumentResolver() : base(ArgumentHelper.GetCommonArguments()) { }
    }
}
