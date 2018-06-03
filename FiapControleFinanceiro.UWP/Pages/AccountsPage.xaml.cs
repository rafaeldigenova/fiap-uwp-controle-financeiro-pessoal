using FiapControleFinanceiro.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiapControleFinanceiro.UWP.Pages
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class AccountsPage : Page
    {
        public AccountsViewModel ViewModel { get; } = new AccountsViewModel();

        public AccountsPage()
        {
            this.InitializeComponent();
            this.Loaded += AccountssPage_Loaded;
        }

        private async void AccountssPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }
    }
}
