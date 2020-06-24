using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace AddressLabelUtility.Views
{
    [ValueConversion(typeof(Enum), typeof(IDictionary<Enum, string>))]
    internal class EnumConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value.GetType().IsEnum)
            {
                throw new ArgumentException();
            }

            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(x => x, x => x.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
