using AddressLabelUtilityCore.Address;

namespace AddressLabelUtilityCore.Csv.Models
{
    public interface ICsvModel : IAddress
    {
        public Encodes Encode { get; set; }
        public bool HasHeader { get; set; }
        public string Item { get; set; }

    }
}
