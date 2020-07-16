using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Arguments.Csv
{
    internal class DestTypeArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "-ot", "--outputType" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.DestType;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; } = CsvKind.Default.ToString().ToLower();

        public override string GetMessage()
        {
            return $"{this.Argument} 形式で出力します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "出力ファイルの種類を指定してください";

                return false;
            }

            return true;
        }
    }
}
