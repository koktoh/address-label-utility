using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class DpiArgument : ArgumentBase
    {
        private static readonly PdfContext _pdfContext = new PdfContext();

        public override string[] Alias => new[] { "-d", "--dpi" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.Dpi;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; } = _pdfContext.Dpi.ToString();

        public override string GetMessage()
        {
            return $"DPI を {this.Argument} に設定します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "DPI を指定してください";

                return false;
            }

            if (!this.Argument.IsNumeral())
            {
                message = $"DPI は数字で指定してください (e.g.: 350.5): {this.Argument}";

                return false;
            }

            return true;
        }
    }
}
