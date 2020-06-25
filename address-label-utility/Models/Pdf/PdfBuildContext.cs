using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtility.Models.Pdf
{
    public class PdfBuildContext
    {
        public PdfContext PdfContext { get; set; } = new PdfContext();
        public LabelContext LabelContext { get; set; } = new LabelContext();
        public IEnumerable<IAddress> ToAddressList { get; set; }
        public IAddress FromAddress { get; set; }
        public IEnumerable<LabelContent> LabelContents
        {
            get =>
                this.ToAddressList
                    .Select(x =>
                        new LabelContent(
                            new DefaultAddress(x),
                            new DefaultAddress(this.FromAddress)));
        }
    }
}
