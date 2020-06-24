using AddressLabelUtilityCore.Address;

namespace AddressLabelUtilityCore.Label
{
    public class LabelContent
    {
        public IAddress ToAddress { get; set; }
        public IAddress FromAddress { get; set; }

        public LabelContent() { }

        public LabelContent(IAddress toAddress, IAddress fromAddress)
        {
            this.ToAddress = toAddress;
            this.FromAddress = fromAddress;
        }
    }
}
