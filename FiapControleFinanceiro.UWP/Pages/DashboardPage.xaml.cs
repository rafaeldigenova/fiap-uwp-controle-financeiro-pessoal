using FiapControleFinanceiro.UWP.Services;
using FiapControleFinanceiro.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiapControleFinanceiro.UWP.Pages
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashBoardViewModel ViewModel { get; } = new DashBoardViewModel();

        public DashboardPage()
        {
            this.InitializeComponent();
            this.Loaded += DashboardPage_Loaded;
        }

        private async void DashboardPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }

        public void AccountsVerMais_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<AccountsPage>();
        }

        public void TransactionsVerMais_Clicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<TransactionsPage>();
        }
    }
}
