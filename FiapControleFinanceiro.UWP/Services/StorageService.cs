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

        public static async Task<byte[]> CarregarImagem()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".jpg");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                var buffer = await FileIO.ReadBufferAsync(file);

                using (DataReader reader = DataReader.FromBuffer(buffer))
                {
                    var imageBytes = new byte[buffer.Length];

                    reader.ReadBytes(imageBytes);

                    return imageBytes;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
