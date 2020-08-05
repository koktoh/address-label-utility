using System.Text;
using System.Text.RegularExpressions;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCore.Address
{
    public abstract class AddressBase : IAddress
    {
        protected readonly Regex _postCodeRex;
        protected readonly Regex _phoneNumberRex;

        public abstract string PostCode { get; set; }
        public abstract string Address1 { get; set; }
        public abstract string Address2 { get; set; }
        public abstract string Address3 { get; set; }
        public abstract string Address4 { get; set; }
        public abstract string Address5 { get; set; }
        public virtual string FullAddress => $"{this.Address1}{this.Address2}{this.Address3}{this.Address4}{this.Address5}";
        public abstract string Name { get; set; }
        public abstract NameSuffix NameSuffix { get; set; }
        public abstract string PhoneNumber { get; set; }

        protected AddressBase()
        {
            this._postCodeRex = new Regex(@"^(\d{3})-?(\d{4})$");
            this._phoneNumberRex = new Regex(@"^(0\d0|0\d{1,4})-?(\d{1,4})-?(\d{4})$");
        }

        protected AddressBase(IAddress @base) : this()
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

        public virtual string GetFormattedPostCode()
        {
            if (this.PostCode.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            if (!this._postCodeRex.IsMatch(this.PostCode))
            {
                return string.Empty;
            }

            return this._postCodeRex.Replace(this.PostCode, @"〒$1-$2");
        }

        public virtual string GetFormattedPhoneNumber()
        {
            if (this.PhoneNumber.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            if (!this._phoneNumberRex.IsMatch(this.PhoneNumber))
            {
                return string.Empty;
            }

            return this._phoneNumberRex.Replace(this.PhoneNumber, @"$1-$2-$3");
        }

        public string ToAddressString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(this.GetFormattedPostCode());
            builder.AppendLine();
            builder.AppendLine($"{this.Address1}{this.Address2}{this.Address3}");
            builder.AppendLine($"{this.Address4}{this.Address5}");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine($"{this.Name}{(this.NameSuffix != NameSuffix.なし ? $" {this.NameSuffix.ToString()}" : string.Empty)}");

            if (this.PhoneNumber.HasMeaningfulValue())
            {
                builder.AppendLine();
                builder.AppendLine($"連絡先: {this.PhoneNumber}");
            }

            return builder.ToString();
        }
    }
}
