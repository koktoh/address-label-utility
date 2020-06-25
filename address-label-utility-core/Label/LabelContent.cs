using AddressLabelUtilityCore.Address;

namespace AddressLabelUtilityCore.Label
{
    public class LabelContent
    {
        public AddressBase ToAddress { get; set; }
        public AddressBase FromAddress { get; set; }

        public LabelContent() { }

        public LabelContent(AddressBase toAddress, AddressBase fromAddress)
        {
            this.ToAddress = toAddress;
            this.FromAddress = fromAddress;
        }
    }
}
