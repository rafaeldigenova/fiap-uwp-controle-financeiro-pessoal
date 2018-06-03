using FiapControleFinanceiro.UWP.Pages;
using FiapControleFinanceiro.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace FiapControleFinanceiro.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            NavigationService.Frame = ContentFrame;
            NavigationService.Navigated += On_Navigated;

            NavigationService.Navigate<DashboardPage>();
        }


        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavView.Header = args.InvokedItem is NavigationViewItem ?
                ((NavigationViewItem)args.InvokedItem).Content : (string)args.InvokedItem;

            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate<SettingsPage>();
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "dashboard":
                    NavigationService.Navigate<DashboardPage>();
                    break;
                case "accounts":
                    NavigationService.Navigate<AccountsPage>();
                    break;
                case "transactions":
                    NavigationService.Navigate<TransactionsPage>();
                    break;
            }
        }


        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ContentFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
            }
            else
            {
                Dictionary<Type, string> lookup = new Dictionary<Type, string>()
                {
                    {typeof(DashboardPage), "dashboard"},
                    {typeof(AccountsPage), "accounts"},
                    {typeof(TransactionsPage), "transactions"},
                    {typeof(CreateTransactionPage), ""},
                    {typeof(EditAccountPage), ""},
                };

                String stringTag = lookup[ContentFrame.SourcePageType];

                var navItem = NavView.MenuItems.FirstOrDefault(item => item is NavigationViewItem && ((NavigationViewItem)item).Tag.Equals(stringTag)) as NavigationViewItem;

                if (navItem != null)
                {
                    navItem.IsSelected = true;
                }
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ((NavigationViewItem)NavView.SettingsItem).Content = "Configurações";
        }
        
        private void NovaTransacaoAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            NavView.Header = "Nova Transação";

            NavigationService.Navigate<CreateTransactionPage>();
        }

        private void NewAccountAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            NavView.Header = "Nova Conta";

            NavigationService.Navigate<EditAccountPage>();
        }
    }
}
