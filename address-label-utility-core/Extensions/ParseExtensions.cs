using System;

namespace AddressLabelUtilityCore.Extensions
{
    internal static class ParseExtensions
    {
        public static int ToInt(this object source)
        {
            if (source is null)
            {
                return 0;
            }

            try
            {
                return Convert.ToInt32(source);
            }
            catch
            {
                return 0;
            }
        }
    }
}
