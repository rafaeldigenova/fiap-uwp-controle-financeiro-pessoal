using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace FiapControleFinanceiro.UWP.ViewModels
{
    public class TransactionCreationViewModel
    {
        public TransactionCreationViewModel()
        {
            DataTransferManager.GetForCurrentView().DataRequested += EditarReceitaViewModel_DataRequested;
        }

        private EFReceitaRepository ReceitaRepository { get; set; } = EFReceitaRepository.Instance;

        private Receita _receita;

        public Receita Receita
        {
            get { return _receita; }
            set { Set(ref _receita, value); }
        }

        public bool RegistroExcluido { get; set; }

        public IEnumerable<Categoria> Categorias => Receita.Categoria.GetValores<Categoria>();

        public void CarregarReceita(Guid id)
        {
            Receita = ReceitaRepository.Items.FirstOrDefault(r => r.Id == id);

            if (Receita == null)
            {
                Receita = new Receita();
            }
        }

        public async void ExcluirReceita()
        {
            await ReceitaRepository.ExcluirAsync(Receita);
            RegistroExcluido = true;
            NavigationService.GoBack();
        }

        private void EditarReceitaViewModel_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            StringBuilder text = new StringBuilder();
            text.AppendLine($"Instruções: {Receita.Instrucoes}");
            text.AppendLine($"Tempo de preparo: {Receita.MinutosPreparo} min");

            request.Data.SetText(text.ToString());
            request.Data.Properties.Title = $"App FIAPRecipes.UWP - {Receita.Titulo}";
        }

        public void CompartilharReceita()
        {
            DataTransferManager.ShowShareUI();
        }

        public async void SalvarReceita()
        {
            if (ReceitaRepository.Items.Any(r => r.Id == Receita.Id))
            {
                await ReceitaRepository.AtualizarAsync(Receita);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Receita.Titulo) && string.IsNullOrWhiteSpace(Receita.Instrucoes))
                    return;

                await ReceitaRepository.CriarAsync(Receita);
            }
        }

        public async void CarregarImagem()
        {
            var imageBytes = await StorageService.CarregarImagem();

            Receita.ImagemBytes = imageBytes ?? Receita.ImagemBytes;
        }

        public void ExcluirImagem()
        {
            Receita.ImagemBytes = null;
        }
    }
}
