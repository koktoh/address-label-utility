namespace AddressLabelUtilityCore.Address
{
    public interface IAddress
    {
        public string PostCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string FullAddress { get; }
        public string Name { get; set; }
        public NameSuffix NameSuffix { get; set; }
        public string PhoneNumber { get; set; }
        public string ToAddressString();
    }
}
