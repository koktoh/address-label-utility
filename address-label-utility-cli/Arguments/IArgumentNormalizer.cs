using System.Collections.Generic;

namespace AddressLabelUtilityCli.Arguments
{
    internal interface IArgumentNormalizer
    {
        IEnumerable<IArgument> Normalize(IEnumerable<IArgument> args);
    }
}
