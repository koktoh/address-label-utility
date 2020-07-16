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
        public override bool HasHeader { get; set; } = true;

        [Index(0)]
        [Name("注文番号")]
        public string OrderId { get; set; }
        [Index(1)]
        [Name("ユーザー識別コード")]
        public string UserCode { get; set; }
        [Index(2)]
        [Name("お支払方法")]
        public string Payment { get; set; }
        [Index(3)]
        [Name("注文状況")]
        public string OrderState { get; set; }
        [Index(4)]
        [Name("注文日時")]
        public string OrderDate { get; set; }
        [Index(5)]
        [Name("支払い日時")]
        public string PaymentDate { get; set; }
        [Index(6)]
        [Name("発送日時")]
        public string SendDate { get; set; }
        [Index(7)]
        [Name("合計金額")]
        public string TotalPayment { get; set; }
        [Index(8)]
        [Name("郵便番号")]
        public override string PostCode { get; set; }
        [Index(9)]
        [Name("都道府県")]
        public override string Address1 { get; set; }
        [Index(10)]
        [Name("市区町村・丁目・番地")]
        public override string Address2 { get; set; }
        [Index(11)]
        [Name("マンション・建物名・部屋番号")]
        public override string Address3 { get; set; }
        [Index(12)]
        [Name("氏名")]
        public override string Name { get; set; }
        [Index(13)]
        [Name("電話番号")]
        public override string PhoneNumber { get; set; }
        [Index(14)]
        [Name("商品ID / 数量 / 商品名")]
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
