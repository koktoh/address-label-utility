using System;
using AddressLabelUtilityCore.Address;
using CsvHelper.Configuration.Attributes;

namespace AddressLabelUtilityCore.Csv.Models
{
    public class DefaultAddressCsvModel : CsvModelBase
    {
        [Ignore]
        public override Encodes Encode { get; set; } = Encodes.UTF8;
        [Ignore]
        public override bool HasHeader { get; set; } = true;

        [Index(0)]
        [Name("郵便番号")]
        public override string PostCode { get; set; }
        [Index(1)]
        [Name("住所1")]
        public override string Address1 { get; set; }
        [Index(2)]
        [Name("住所2")]
        public override string Address2 { get; set; }
        [Index(3)]
        [Name("住所3")]
        public override string Address3 { get; set; }
        [Index(4)]
        [Name("住所4")]
        public override string Address4 { get; set; }
        [Index(5)]
        [Name("住所5")]
        public override string Address5 { get; set; }
        [Index(6)]
        [Name("氏名")]
        public override string Name { get; set; }
        [Index(7)]
        [Name("敬称")]
        public override NameSuffix NameSuffix { get; set; }
        [Index(8)]
        [Name("電話番号")]
        public override string PhoneNumber { get; set; }

        [Ignore]
        public override string FullAddress => base.FullAddress;
        [Ignore]
        public override string Item { get; set; }

        public DefaultAddressCsvModel() : base() { }
        public DefaultAddressCsvModel(ICsvModel @base) : base(@base) { }
    }
}
