using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Execute
{
    internal interface IExecutor
    {
        int Execute(IEnumerable<IArgument> args);
    }
}
