using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Extensions;

namespace AddressLabelUtilityCli.Arguments
{
    internal abstract class ArgumentResolverBase : IArgumentResolver
    {
        protected readonly IReadOnlyCollection<IArgument> _arguments;

        public ArgumentResolverBase(IEnumerable<IArgument> arguments)
        {
            this._arguments = arguments.ToList();
        }

        public virtual IArgument Resolve(IArgument argument)
        {
            if (this._arguments.Contains(argument))
            {
                var destArgument = this._arguments.Get(argument);

                destArgument.Raw = argument.Raw;

                return destArgument;
            }

            return default;
        }
    }
}
