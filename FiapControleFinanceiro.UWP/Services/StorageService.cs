using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace FiapControleFinanceiro.UWP.Services
{
    public static class StorageService
    {
        private static ApplicationDataContainer _localSettings => ApplicationData.Current.LocalSettings;

        public enum Configuracoes
        {
            Currency
        }

        public static T LerConfiguracao<T>(Configuracoes configuracao, T defaultValue)
        {
            var value = _localSettings.Values[configuracao.ToString()];

            if (value != null)
            {
                return (T)value;
            }
            else
            {
                return defaultValue;
            }
        }

        public static void GravarConfiguracao(Configuracoes configuracao, object value)
        {
            _localSettings.Values[configuracao.ToString()] = value;
        }
    }
}
