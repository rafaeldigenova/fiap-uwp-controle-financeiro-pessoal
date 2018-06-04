using FiapControleFinanceiro.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FiapControleFinanceiro.Models
{
    public class Account : NotifyableClass
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private AccountType _accountType;

        public AccountType AccountType
        {
            get { return _accountType; }
            set { Set(ref _accountType, value); }
        }

        [NotMapped]
        public string Currency { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public decimal CurentBalance
        {
            get
            {
                return Transactions.Where(x => x.ProcessmentDate < DateTime.Now).Sum(x => x.Ammount);
            }
        }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}
