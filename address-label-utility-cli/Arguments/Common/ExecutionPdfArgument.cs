namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class ExecutionPdfArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "--pdf" };

        public override string Raw { get; set; }

        public override bool IsRequired => true;

        public override ArgumentKind ArgumentKind => ArgumentKind.ExecPdf;

        public override bool ShouldHaveArgument => false;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"PDF へラベルの出力を行います";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
