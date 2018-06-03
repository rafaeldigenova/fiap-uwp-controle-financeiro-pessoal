using FiapControleFinanceiro.Models;
using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.UWP.Extensions;
using FiapControleFinanceiro.UWP.Repository;
using FiapControleFinanceiro.UWP.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using System;

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
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Excluir conta permanentemente?",
                Content = "Se você excluir sua conta, os dados associados a ela serão perdidos. Deseja Continuar?",
                PrimaryButtonText = "Excluir",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await AccountRepository.ExcluirAsync(Account);
                RegistroExcluido = true;

                string title = "Conta excluída com sucesso!";
                string content = $"Sua conta {Account.Name} foi excluida com sucesso.";

                NotificationService.SendNotification(title, content, "CF-001", "Transaction", 1);

                NavigationService.GoBack();
            }
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

            string title = "Conta salva com sucesso!";
            string content = $"Sua conta {Account.Name} foi salva com sucesso.";

            NotificationService.SendNotification(title, content, "CF-001", "Transaction", 1);
        }
    }
}
