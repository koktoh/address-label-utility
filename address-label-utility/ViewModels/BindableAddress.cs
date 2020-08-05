using AddressLabelUtilityCore.Address;
using Prism.Mvvm;

namespace AddressLabelUtility.ViewModels
{
    internal class BindableAddress : BindableBase, IAddress
    {
        private NameSuffix _nameSuffix;
        private bool _isSelected;

        public string PostCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Name { get; set; }

        public NameSuffix NameSuffix
        {
            get { return this._nameSuffix; }
            set { this.SetProperty(ref this._nameSuffix, value); }
        }

        public string PhoneNumber { get; set; }

        public string FullAddress { get; }

        public string GetFormattedPostCode() => string.Empty;
        public string GetFormattedPhoneNumber() => string.Empty;
        public string ToAddressString() => string.Empty;

        public bool IsSelected
        {
            get { return this._isSelected; }
            set { this.SetProperty(ref this._isSelected, value); }
        }

        public BindableAddress(IAddress address)
        {
            this.PostCode = address.PostCode;
            this.Address1 = address.Address1;
            this.Address2 = address.Address2;
            this.Address3 = address.Address3;
            this.Address4 = address.Address4;
            this.Address5 = address.Address5;
            this.FullAddress = address.FullAddress;
            this.Name = address.Name;
            this.NameSuffix = address.NameSuffix;
            this.PhoneNumber = address.PhoneNumber;
        }
    }
}
