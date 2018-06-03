using FiapControleFinanceiro.Models.Abstracts;
using FiapControleFinanceiro.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiapControleFinanceiro.Models
{
    public class Account : NotifyableClass
    {
        public int Id { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public decimal CurentBalance
        {
            get
            {
                return Transactions.Where(x => x.ProcessmentDate < DateTime.Now).Sum(x => x.Ammount);
            }
        }
    }
}
