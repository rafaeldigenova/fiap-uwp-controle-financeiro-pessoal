using FiapControleFinanceiro.Models;
using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.UWP.Pages;
using FiapControleFinanceiro.UWP.Repository;
using FiapControleFinanceiro.UWP.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FiapControleFinanceiro.UWP.ViewModels
{
    public class AccountsViewModel : NotifyableClass
    {
        private EFAccountRepository AccountRepository { get; set; } = EFAccountRepository.Instance;

        public ObservableCollection<Account> Accounts => AccountRepository.Items;

        public async Task Initialize()
        {
            await AccountRepository.CarregarTodosAsync();

            var currency = StorageService.LerConfiguracao(StorageService.Configuracoes.Currency, 0);
            string currencyDescription = string.Empty;

            switch (currency)
            {
                case 0:
                    currencyDescription = "R$";
                    break;
                case 1:
                    currencyDescription = "$";
                    break;
                default:
                    currencyDescription = "R$";
                    break;
            }

            foreach (var account in Accounts)
            {
                account.Currency = currencyDescription;
            }
        }

        public void ListAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItem == null || listView.SelectedItem as Account == null)
            {
                return;
            }

            var accountId = ((Account)listView.SelectedItem).Id;

            NavigationService.Navigate<EditAccountPage>(accountId);
        }
    }
}
