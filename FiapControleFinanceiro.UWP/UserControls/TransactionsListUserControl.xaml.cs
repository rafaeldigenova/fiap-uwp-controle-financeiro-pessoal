using FiapControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Controle de Usuário está documentado em https://go.microsoft.com/fwlink/?LinkId=234236

namespace FiapControleFinanceiro.UWP.UserControls
{
    public sealed partial class TransactionsListUserControl : UserControl
    {
        public TransactionsListUserControl()
        {
            this.InitializeComponent();
        }

        public Transaction Transaction
        {
            get { return (Transaction)GetValue(TransactionProperty); }
            set { SetValue(TransactionProperty, value); }
        }

        public static readonly DependencyProperty TransactionProperty =
            DependencyProperty.Register("Transaction", typeof(Transaction), typeof(TransactionsListUserControl), new PropertyMetadata(null));
    }
}
