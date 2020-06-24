namespace AddressLabelUtilityCore.Layout
{
    public struct LayoutProperty
    {
        public int PageWidth { get; set; }
        public int PageHeight { get; set; }
        public UnitSize UnitSize { get; set; }
        public float Margin { get; set; }
        public int LabelRowCount { get; set; }
        public int LabelColumnCount { get; set; }
        public float LabelWidth { get; set; }
        public float LabelHeight { get; set; }
        public float LabelHeaderWidth { get; set; }
        public float LabelHeaderHeight { get; set; }
        public LabelOrientation LabelOrientation { get; set; }
    }
}
