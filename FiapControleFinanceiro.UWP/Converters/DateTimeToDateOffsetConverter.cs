using System;
using Windows.UI.Xaml.Data;

namespace FiapControleFinanceiro.UWP.Converters
{
    public class DateTimeToDateOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime.TryParse(System.Convert.ToString(value), out DateTime valor);

            var dateString = valor.ToString("yyyy-MM-ddTHH:mm:ssZ");

            return DateTimeOffset.Parse(dateString);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset.TryParse(System.Convert.ToString(value), out DateTimeOffset valor);

            return valor.DateTime;
        }
    }
}
