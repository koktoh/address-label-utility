using System.Collections.Generic;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Helper;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtility.Models.Pdf
{
    public class PdfBuildingContext
    {
        public PdfContext PdfContext { get; set; } = new PdfContext();
        public LabelContext LabelContext { get; set; } = new LabelContext();
        public IEnumerable<IAddress> ToAddressList { get; set; }
        public IAddress FromAddress { get; set; }
        public IEnumerable<LabelContent> LabelContents => BuildingLabelContentHelper.Build(this.ToAddressList, this.FromAddress);
    }
}
