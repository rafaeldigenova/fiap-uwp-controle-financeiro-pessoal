using FiapControleFinanceiro.UWP.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FiapControleFinanceiro.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditAccountPage : Page
    {
        public EditAccountViewModel ViewModel { get; } = new EditAccountViewModel();

        public EditAccountPage()
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

                ViewModel.LoadAccount((int)parameter);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (!ViewModel.RegistroExcluido)
                ViewModel.SaveAccount();
        }
    }
}
