using System.IO;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class Src2Argument : ArgumentBase
    {
        public override string[] Alias => new[] { "-s2", "--source2" };

        public override string Raw { get; set; }

        public override bool IsRequired => true;

        public override ArgumentKind ArgumentKind => ArgumentKind.SrcPath2;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"差出人ソースファイル {this.Argument} を読み込みます";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;

            if (this.Argument.IsNullOrWhiteSpace())
            {
                message = "差出人ソースファイルを指定してください";

                return false;
            }

            if (!File.Exists(this.Argument))
            {
                message = $"ファイルが存在しません: {this.Argument}";

                return false;
            }

            return true;
        }
    }
}
