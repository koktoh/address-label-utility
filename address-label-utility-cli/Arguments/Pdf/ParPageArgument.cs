using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Label;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class ParPageArgument : ArgumentBase
    {
        private static readonly LabelContext _labelContext = new LabelContext();

        public override string[] Alias => new[] { "-p", "--parPage" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.ParPage;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; } = _labelContext.ParPage.ToString();

        public override string GetMessage()
        {
            return $"面付を {this.Argument} に設定します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "面付を指定してください";

                return false;
            }

            if (!this.Argument.IsInteger())
            {
                message = $"面付は整数で指定してください (e.g.: 4): {this.Argument}";

                return false;
            }

            return true;
        }
    }
}
