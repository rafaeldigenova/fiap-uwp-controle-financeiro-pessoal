using FiapControleFinanceiro.Models;
using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.UWP.Repository;
using FiapControleFinanceiro.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace FiapControleFinanceiro.UWP.ViewModels
{
    public class TransactionCreationViewModel : NotifyableClass
    {
        public TransactionCreationViewModel()
        {
            DataTransferManager.GetForCurrentView().DataRequested += EditarTransacaoViewModel_DataRequested;
        }

        private EFTransactionsRepository TransactionRepository { get; set; } = EFTransactionsRepository.Instance;
        private EFAccountRepository AccountRepository { get; set; } = EFAccountRepository.Instance;

        private Transaction _transaction;

        public Transaction Transaction
        {
            get { return _transaction; }
            set { Set(ref _transaction, value); }
        }

        public bool RegistroExcluido { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public async Task CarregarTransacao(int id)
        {
            await TransactionRepository.CarregarTodosAsync();
            await AccountRepository.CarregarTodosAsync();

            Accounts = AccountRepository.Items.ToList();

            Transaction = TransactionRepository.Items.FirstOrDefault(x => x.Id == id);

            if (Transaction == null)
            {
                Transaction = new Transaction();
            }
        }

        public async void ExcluirTransacao()
        {
            await TransactionRepository.ExcluirAsync(Transaction);
            RegistroExcluido = true;
            NavigationService.GoBack();
        }

        private void EditarTransacaoViewModel_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            StringBuilder text = new StringBuilder();
            text.AppendLine($"Data de Criação: {Transaction.CreatedDate}");
            text.AppendLine($"Data de Processamento: {Transaction.ProcessmentDate}");

            request.Data.SetText(text.ToString());
            request.Data.Properties.Title = $"Controle Financeiro - {Transaction.Id}";
        }

        public void CompartilharTransacao()
        {
            DataTransferManager.ShowShareUI();
        }

        public async void SalvarTransacao()
        {
            if (TransactionRepository.Items.Any(r => r.Id == Transaction.Id))
            {
                await TransactionRepository.AtualizarAsync(Transaction);
            }
            else
            {
                if (Transaction.Ammount == 0) return;

                await TransactionRepository.CriarAsync(Transaction);
            }
        }
    }
}
