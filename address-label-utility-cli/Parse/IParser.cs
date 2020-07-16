using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Parse
{
    interface IParser
    {
        IEnumerable<IArgument> Parse(string[] args);
    }
}
