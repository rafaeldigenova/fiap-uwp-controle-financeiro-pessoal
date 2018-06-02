using FiapControleFinanceiro.Models.Abstracts;
using System;

namespace FiapControleFinanceiro.Models
{
    public class Transaction : NotifyableClass
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ProcessmentDate { get; set; }

        public decimal Ammount { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
