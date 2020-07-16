using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class DestArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "-o", "--output" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.DestPath;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"{this.Argument} へ出力します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "出力ファイルを指定してください";

                return false;
            }

            return true;
        }
    }
}
