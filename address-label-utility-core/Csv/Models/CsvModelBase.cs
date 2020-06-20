using AddressLabelUtilityCore.Address;

namespace AddressLabelUtilityCore.Csv.Models
{
    public abstract class CsvModelBase : AddressBase, ICsvModel
    {
        public virtual Encodes Encode { get; set; } = Encodes.UTF8;
        public virtual bool HasHeader { get; set; } = true;

        public abstract override string PostCode { get; set; }
        public abstract override string Address1 { get; set; }
        public abstract override string Address2 { get; set; }
        public abstract override string Address3 { get; set; }
        public abstract override string Address4 { get; set; }
        public abstract override string Address5 { get; set; }
        public abstract override string Name { get; set; }
        public abstract override NameSuffix NameSuffix { get; set; }
        public abstract override string PhoneNumber { get; set; }

        public override string FullAddress => base.FullAddress;
        public abstract string Item { get; set; }

        protected CsvModelBase() { }

        protected CsvModelBase(ICsvModel @base) : base(@base)
        {
            this.Encode = @base.Encode;
            this.HasHeader = @base.HasHeader;
            this.Item = @base.Item;
        }
    }
}
