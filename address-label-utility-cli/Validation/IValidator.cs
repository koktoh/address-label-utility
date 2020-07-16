using System.Collections.Generic;
using AddressLabelUtilityCli.Arguments;

namespace AddressLabelUtilityCli.Validation
{
    interface IValidator
    {
        bool Validate(IEnumerable<IArgument> args, out string message);
    }
}
