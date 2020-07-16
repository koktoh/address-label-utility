using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class SrcType2Argument : ArgumentBase
    {
        public override string[] Alias => new[] { "-st2", "--sourceType2" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.SrcType2;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"差出人ソースファイルを {this.Argument} 形式として読み込みます";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "差出人ソースファイルの形式を指定してください";

                return false;
            }

            return true;
        }
    }
}
