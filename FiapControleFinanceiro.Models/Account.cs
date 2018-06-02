using FiapControleFinanceiro.Models.Abstracts;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FiapControleFinanceiro.Models
{
    public class Account : NotifyableClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Transaction> Transactions { get; set; }
        public decimal CurentBalance { get
            {
                return Transactions.Where(x => x.ProcessmentDate < DateTime.Now).Sum(x => x.Ammount);
            }
        }
    }
}
