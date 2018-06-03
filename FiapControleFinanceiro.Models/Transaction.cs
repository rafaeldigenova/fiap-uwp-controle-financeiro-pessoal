using FiapControleFinanceiro.Models.Abstracts;
using System;

namespace FiapControleFinanceiro.Models
{
    public class Transaction : NotifyableClass
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private DateTime _createdDate;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { Set(ref _createdDate, value); }
        }

        public DateTime ProcessmentDate { get; set; }

        private decimal _ammount;

        public decimal Ammount
        {
            get { return _ammount; }
            set { Set(ref _ammount, value); }
        }

        private int _accountId;

        public int AccountId
        {
            get { return _accountId; }
            set { Set(ref _accountId, value); }
        }
        
        public virtual Account Account { get; set; }

        public Transaction()
        {
            CreatedDate = DateTime.Now;
            ProcessmentDate = DateTime.Now;
        }
    }
}
