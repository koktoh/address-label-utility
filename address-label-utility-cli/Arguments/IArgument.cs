namespace AddressLabelUtilityCli.Arguments
{
    interface IArgument
    {
        string[] Alias { get; }
        string Raw { get; set; }
        bool IsRequired { get; }
        ArgumentKind ArgumentKind { get; }
        bool ShouldHaveArgument { get; }
        string Argument { get; set; }
        bool Validate(out string message);
        string GetMessage();
    }
}
