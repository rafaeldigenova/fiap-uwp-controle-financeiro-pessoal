using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapControleFinanceiro.UWP.ViewModels
{
    public class SettingsViewModel : NotifyableClass
    {
        private int? _currencySetting;

        public int? CurrencySetting
        {
            get
            {
                return StorageService.LerConfiguracao(StorageService.Configuracoes.Currency, 0);
            }
            set
            {
                StorageService.GravarConfiguracao(StorageService.Configuracoes.Currency, value);
            }
        }
    }
}
