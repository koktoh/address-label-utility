namespace AddressLabelUtilityCore.Label
{
    public class LabelContext
    {
        public int OutlineWidth { get; set; } = 4;

        /// <summary>
        /// 余白の割合
        /// </summary>
        public float MarginRatio { get; set; } = 5;

        public int ParPage { get; set; } = 4;
    }
}
