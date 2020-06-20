using System.Text;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Address
{
    public abstract class AddressBase : IAddress
    {
        public abstract string PostCode { get; set; }
        public abstract string Address1 { get; set; }
        public abstract string Address2 { get; set; }
        public abstract string Address3 { get; set; }
        public abstract string Address4 { get; set; }
        public abstract string Address5 { get; set; }
        public virtual string FullAddress { get => $"{this.Address1}{this.Address2}{this.Address3}{this.Address4}{this.Address5}"; }
        public abstract string Name { get; set; }
        public abstract NameSuffix NameSuffix { get; set; }
        public abstract string PhoneNumber { get; set; }

        protected AddressBase() { }

        protected AddressBase(IAddress @base)
        {
            this.PostCode = @base.PostCode;
            this.Address1 = @base.Address1;
            this.Address2 = @base.Address2;
            this.Address3 = @base.Address3;
            this.Address4 = @base.Address4;
            this.Address5 = @base.Address5;
            this.Name = @base.Name;
            this.NameSuffix = @base.NameSuffix;
            this.PhoneNumber = @base.PhoneNumber;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"〒{this.PostCode}");
            builder.AppendLine();
            builder.AppendLine($"{this.Address1}{this.Address2}{this.Address3}");
            builder.AppendLine($"{this.Address4}{this.Address5}");
            builder.AppendLine($"");
            builder.AppendLine($"");
            builder.AppendLine($"{this.Name}{(this.NameSuffix!=NameSuffix.なし?this.NameSuffix.ToString():string.Empty)}");

            if (this.PhoneNumber.HasMeaningfulValue())
            {
                builder.AppendLine();
                builder.AppendLine($"連絡先: {this.PhoneNumber}");
            }

            return builder.ToString();
        }
    }
}
