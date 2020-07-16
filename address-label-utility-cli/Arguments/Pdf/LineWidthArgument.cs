using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Label;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class LineWidthArgument : ArgumentBase
    {
        private static readonly LabelContext _labelContext = new LabelContext();

        public override string[] Alias => new[] { "-w", "--width" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.LineWidth;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; } = _labelContext.OutlineWidth.ToString();

        public override string GetMessage()
        {
            return $"枠線の幅を {this.Argument} に設定します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "枠線の幅を指定してください";

                return false;
            }

            if (!this.Argument.IsInteger())
            {
                message = $"枠線の幅は整数で指定してください (e.g.: 5): {this.Argument}";

                return false;
            }

            return true;
        }
    }
}
