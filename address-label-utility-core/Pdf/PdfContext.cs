namespace AddressLabelUtilityCore.Pdf
{
    public class PdfContext
    {
        public string OutputPath { get; set; }
        public string FileName { get; set; }
        public PdfSizeSet PdfSizeSet { get; set; } = PdfSizeSet.A4;
        public PdfSize PdfSize => PdfSizeResolver.Resolve(this.PdfSizeSet);
        public float Dpi { get; set; } = 350;
        public bool IsVisibleSeparateLine { get; set; } = false;
    }
}
