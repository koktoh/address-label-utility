using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Label;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class MarginArgument : ArgumentBase
    {
        private static readonly LabelContext _labelContext = new LabelContext();

        public override string[] Alias => new[] { "-m", "--margin" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.Margin;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; } = _labelContext.MarginRatio.ToString();

        public override string GetMessage()
        {
            return $"マージン(%)を {this.Argument} に設定します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "マージン(%)を指定してください";

                return false;
            }

            if (!this.Argument.IsNumeral())
            {
                message = $"マージン(%)は数字で指定してください (e.g.: 0.5): {this.Argument}";

                return false;
            }

            return true;
        }
    }
}
