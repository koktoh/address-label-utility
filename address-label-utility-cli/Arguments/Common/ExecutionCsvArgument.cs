namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class ExecutionCsvArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "--csv" };

        public override string Raw { get; set; }

        public override bool IsRequired => true;

        public override ArgumentKind ArgumentKind => ArgumentKind.ExecCsv;

        public override bool ShouldHaveArgument => false;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"CSV ファイルへの変換を行います";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
