using FiapControleFinanceiro.UWP.ViewModels;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiapControleFinanceiro.UWP.Pages
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CreateTransactionPage : Page
    {
        public TransactionCreationViewModel ViewModel { get; } = new TransactionCreationViewModel();

        public CreateTransactionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object parameter = e.Parameter;

            if (parameter == null || parameter is int)
            {
                if (parameter == null)
                {
                    parameter = 0;
                }

                ViewModel.CarregarTransacao((int)parameter);
            }
            else if (parameter is string)
            {
                WwwFormUrlDecoder uriDecoder = new WwwFormUrlDecoder(parameter.ToString());
                var rule = uriDecoder.GetFirstValueByName("rule");
                var valor = uriDecoder.GetFirstValueByName("valor");

                ViewModel.CarregarTransacao(0);

                if (!string.IsNullOrWhiteSpace(rule))
                {
                    
                    ViewModel.Transaction.Id = 0;
                    if (rule == "adicionarTransacao")
                    {
                        var valorDecimal = Decimal.TryParse(valor, out decimal valorResult);
                        ViewModel.Transaction.Ammount = valorDecimal ? valorResult : 0m;
                    }
                    else if(rule == "abaterValor")
                    {
                        var valorDecimal = Decimal.TryParse(valor, out decimal valorResult);
                        ViewModel.Transaction.Ammount = valorDecimal ? -valorResult : 0m;
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (!ViewModel.RegistroExcluido) {

                ViewModel.ConfigurarHora(timePicker.Time);

                ViewModel.SalvarTransacao();
            }
        }
    }
}
