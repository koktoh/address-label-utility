using System.Linq;

namespace AddressLabelUtilityCli.Arguments
{
    internal class DefaultArgument : ArgumentBase
    {
        public override string[] Alias => Enumerable.Empty<string>().ToArray();

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.Default;

        public override bool ShouldHaveArgument => false;

        public override string Argument { get; set; }

        public override bool Validate(out string message)
        {
            message = string.Empty;
            return true;
        }

        public override string GetMessage() => string.Empty;
    }
}
