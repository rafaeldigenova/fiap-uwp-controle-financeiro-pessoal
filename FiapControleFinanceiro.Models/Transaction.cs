using System;
using System.Collections.Generic;
using System.Text;

namespace FiapControleFinanceiro.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ProcessmentDate { get; set; }

        public decimal Ammount { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
