using System;
using System.Globalization;
using System.Windows.Data;

namespace TicTacToe.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverse ? !(bool)value : (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
            {
                return null;
            }

            return !Inverse;
        }
    }
}