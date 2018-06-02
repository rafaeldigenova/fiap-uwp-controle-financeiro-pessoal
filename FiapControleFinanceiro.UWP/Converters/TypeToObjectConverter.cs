using System;
using Windows.UI.Xaml.Data;

namespace FiapControleFinanceiro.UWP.Converters
{
    public class TypeToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ChangeType(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ChangeType(value, targetType);
        }
    }

}
