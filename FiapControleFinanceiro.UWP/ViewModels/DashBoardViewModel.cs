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
    public class DashBoardViewModel : NotifyableClass
    {
        private EFAccountRepository AccountRepository { get; set; } = EFAccountRepository.Instance;
        private EFTransactionsRepository TransactionRepository { get; set; } = EFTransactionsRepository.Instance;

        public ObservableCollection<Account> Accounts => AccountRepository.Items;
        public ObservableCollection<Transaction> Transactions => TransactionRepository.Items;

        public async Task Initialize()
        {
            await AccountRepository.CarregarTodosAsync();
            await TransactionRepository.CarregarTodosAsync();
        }
    }
}
