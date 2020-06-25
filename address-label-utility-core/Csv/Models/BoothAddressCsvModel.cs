using System;
using System.Linq;
using AddressLabelUtilityCore.Address;
using CsvHelper.Configuration.Attributes;

namespace AddressLabelUtilityCore.Csv.Models
{
    public class BoothAddressCsvModel : CsvModelBase
    {
        private string _item;

        [Ignore]
        public override Encodes Encode { get; set; } = Encodes.UTF8;
        [Ignore]
        public override bool HasHeader { get; set; } = false;

        [Index(0)]
        public string OrderId { get; set; }
        [Index(1)]
        public string UserCode { get; set; }
        [Index(2)]
        public string Payment { get; set; }
        [Index(3)]
        public string OrderState { get; set; }
        [Index(4)]
        public string OrderDate { get; set; }
        [Index(5)]
        public string PaymentDate { get; set; }
        [Index(6)]
        public string SendDate { get; set; }
        [Index(7)]
        public string TotalPayment { get; set; }
        [Index(8)]
        public override string PostCode { get; set; }
        [Index(9)]
        public override string Address1 { get; set; }
        [Index(10)]
        public override string Address2 { get; set; }
        [Index(11)]
        public override string Address3 { get; set; }
        [Index(12)]
        public override string Name { get; set; }
        [Index(13)]
        public override string PhoneNumber { get; set; }
        [Index(14)]
        public override string Item { get => this._item?.Split('/').LastOrDefault().Trim(); set => this._item = value; }

        #region for abstract class

        [Ignore]
        public override string Address4 { get; set; }
        [Ignore]
        public override string Address5 { get; set; }
        [Ignore]
        public override NameSuffix NameSuffix { get; set; } = NameSuffix.様;
        [Ignore]
        public override string FullAddress => base.FullAddress;

        #endregion

        public BoothAddressCsvModel() : base() { }
        public BoothAddressCsvModel(ICsvModel @base) : base(@base) { }
    }
}
