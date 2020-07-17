using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Parser
{
    interface IParser
    {
        IEnumerable<IArgument> Parse(string[] args);
    }
}
