namespace AddressLabelUtilityCli.Arguments.Pdf
{
    internal class VisibleLineArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "-v", "--visible" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.VisibleLine;

        public override bool ShouldHaveArgument => true;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            return $"枠線を表示します";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
