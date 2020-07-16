namespace AddressLabelUtilityCli.Arguments
{
    internal interface IArgumentResolver
    {
        IArgument Resolve(IArgument argument);
    }
}
