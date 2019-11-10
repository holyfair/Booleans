using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace Booleans.Tools.Managers
{
    public class CardNumberConverter : MarkupExtension, IValueConverter
    {
        string text;

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            text = (string)value;
            return '*' + text.Substring(text.Length - 4, text.Length);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return text;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}