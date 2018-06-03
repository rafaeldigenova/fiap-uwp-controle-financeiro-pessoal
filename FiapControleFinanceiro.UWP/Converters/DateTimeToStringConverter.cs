using System;
using Windows.UI.Xaml.Data;

namespace FiapControleFinanceiro.UWP.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime.TryParse(System.Convert.ToString(value), out DateTime valor);

            return valor.ToString("dd/MM/yyyy HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
