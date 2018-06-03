using FiapControleFinanceiro.Models;
using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.UWP.Extensions;
using FiapControleFinanceiro.UWP.Repository;
using FiapControleFinanceiro.UWP.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

namespace FiapControleFinanceiro.UWP.ViewModels
{
    public class EditAccountViewModel : NotifyableClass
    {
        public EditAccountViewModel()
        {
            DataTransferManager.GetForCurrentView().DataRequested += EditarAccountViewModel_DataRequested;
        }

        private EFAccountRepository AccountRepository { get; set; } = EFAccountRepository.Instance;

        private Account _account;

        public Account Account
        {
            get { return _account; }
            set { Set(ref _account, value); }
        }

        public bool RegistroExcluido { get; set; }

        public IEnumerable<AccountType> AccountTypes => Account.AccountType.GetValues<AccountType>();

        public void LoadAccount(int id)
        {
            Account = AccountRepository.Items.FirstOrDefault(x => x.Id == id);

            if (Account == null)
            {
                Account = new Account();
            }
        }

        public async void DeleteAccount()
        {
            await AccountRepository.ExcluirAsync(Account);
            RegistroExcluido = true;
            NavigationService.GoBack();
        }

        private void EditarAccountViewModel_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            StringBuilder text = new StringBuilder();
            text.AppendLine($"Tipo da Conta: {Account.AccountType}");
            text.AppendLine($"Saldo: {Account.CurentBalance}");

            request.Data.SetText(text.ToString());
            request.Data.Properties.Title = $"Controle Financeiro - {Account.AccountType}";
        }

        public async void SaveAccount()
        {
            if (AccountRepository.Items.Any(x => x.Id == Account.Id))
            {
                await AccountRepository.AtualizarAsync(Account);
            }
            else
            {
                if (Account.AccountType == 0)
                    return;

                await AccountRepository.CriarAsync(Account);
            }
        }
    }
}
