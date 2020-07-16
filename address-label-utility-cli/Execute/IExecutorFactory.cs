using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Execute
{
    internal interface IExecutorFactory
    {
        IExecutor Create(IEnumerable<IArgument> args);
    }
}
