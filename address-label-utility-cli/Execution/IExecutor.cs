using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Execution
{
    internal interface IExecutor
    {
        int Execute(IEnumerable<IArgument> args);
    }
}
