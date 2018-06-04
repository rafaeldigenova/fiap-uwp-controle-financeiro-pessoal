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
    public class TransactionsViewModel : NotifyableClass
    {
        private EFTransactionsRepository TransactionRepository { get; set; } = EFTransactionsRepository.Instance;

        public ObservableCollection<Transaction> Transactions => TransactionRepository.Items;

        public async Task Initialize()
        {
            await TransactionRepository.CarregarTodosAsync();

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

            foreach (var transaction in Transactions)
            {
                transaction.Account.Currency = currencyDescription;
            }
        }

        public void ListAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItem == null || listView.SelectedItem as Transaction == null)
            {
                return;
            }

            var transactionId = ((Transaction)listView.SelectedItem).Id;

            NavigationService.Navigate<CreateTransactionPage>(transactionId);
        }
    }
}
