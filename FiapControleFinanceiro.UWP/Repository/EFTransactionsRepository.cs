using FiapControleFinanceiro.Dados;
using FiapControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapControleFinanceiro.UWP.Repository
{
    public class EFTransactionsRepository : Repository<Transaction>
    {
        private static readonly Lazy<EFTransactionsRepository> _instance =
                new Lazy<EFTransactionsRepository>(() => new EFTransactionsRepository());

        public static EFTransactionsRepository Instance { get { return _instance.Value; } }

        public override async Task AtualizarAsync(Transaction entity)
        {
            using (var context = new FinancialManagerDbContext())
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public override async Task CarregarTodosAsync()
        {
            if (Items.Count > 0)
            {
                return;
            }

            using (var context = new FinancialManagerDbContext())
            {
                var transactions = context.Transactions.ToList();

                foreach (var transaction in transactions)
                {
                    Items.Add(transaction);
                }
            }
        }

        public override async Task CriarAsync(Transaction entity)
        {
            using (var context = new FinancialManagerDbContext())
            {
                Items.Add(entity);
                context.Transactions.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public override async Task ExcluirAsync(Transaction entity)
        {
            var transaction = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (transaction != null)
            {
                using (var context = new FinancialManagerDbContext())
                {
                    Items.Remove(transaction);

                    context.Transactions.Remove(transaction);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
