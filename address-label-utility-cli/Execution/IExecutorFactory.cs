using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Execution
{
    internal interface IExecutorFactory
    {
        IExecutor Create(IEnumerable<IArgument> args);
    }
}
