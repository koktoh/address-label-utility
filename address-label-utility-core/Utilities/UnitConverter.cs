namespace AddressLabelUtilityCore.Utilities
{
    public static class UnitConverter
    {
        public static float ConvertMmToPixel(float mm, float dpi)
        {
            var inch = mm / 25.4f;

            return dpi * inch;
        }
    }
}
