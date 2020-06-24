namespace AddressLabelUtilityCore.Pdf
{
    public struct PdfSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public PdfSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
