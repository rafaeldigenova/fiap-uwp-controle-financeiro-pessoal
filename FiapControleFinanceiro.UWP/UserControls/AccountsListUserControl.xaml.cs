using FiapControleFinanceiro.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FiapControleFinanceiro.UWP.UserControls
{
    public sealed partial class AccountsListUserControl : UserControl
    {
        public AccountsListUserControl()
        {
            this.InitializeComponent();
        }

        public Account Account
        {
            get { return (Account)GetValue(AccountProperty); }
            set { SetValue(AccountProperty, value); }
        }

        public static readonly DependencyProperty AccountProperty =
            DependencyProperty.Register("Account", typeof(Account), typeof(AccountsListUserControl), new PropertyMetadata(null));
    }
}
