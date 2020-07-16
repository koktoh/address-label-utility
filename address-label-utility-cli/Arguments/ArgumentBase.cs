using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressLabelUtilityCli.Arguments
{
    internal abstract class ArgumentBase : IArgument
    {
        public abstract string[] Alias { get; }
        public abstract string Raw { get; set; }
        public abstract bool IsRequired { get; }
        public abstract ArgumentKind ArgumentKind { get; }
        public abstract bool ShouldHaveArgument { get; }
        public abstract string Argument { get; set; }

        public abstract string GetMessage();
        public abstract bool Validate(out string message);

        public override bool Equals(object obj)
        {
            return obj is ArgumentBase @base
                && (EqualityComparer<string[]>.Default.Equals(this.Alias, @base.Alias)
                    || this.Alias.Contains(@base.Raw?.Split(" ", StringSplitOptions.RemoveEmptyEntries).First())
                    || this.ArgumentKind == @base.ArgumentKind);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
