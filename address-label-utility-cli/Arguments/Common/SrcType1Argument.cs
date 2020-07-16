using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class SrcType1Argument : ArgumentBase
    {
        public override string[] Alias => new[] { "-st", "--sourceType", "-st1", "--sourceType1" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.SrcType1;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"ソースファイルを {this.Argument} 形式として読み込みます";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "ソースファイルの形式を指定してください";

                return false;
            }

            return true;
        }
    }
}
