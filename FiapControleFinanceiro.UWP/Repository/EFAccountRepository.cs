using FiapControleFinanceiro.Dados;
using FiapControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapControleFinanceiro.UWP.Repository
{
    public class EFAccountRepository : Repository<Account>
    {
        private static readonly Lazy<EFAccountRepository> _instance =
                new Lazy<EFAccountRepository>(() => new EFAccountRepository());

        public static EFAccountRepository Instance { get { return _instance.Value; } }

        public override async Task AtualizarAsync(Account entity)
        {
            using (var context = new FinancialManagerDbContext())
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public override async Task CarregarTodosAsync()
        {
            using (var context = new FinancialManagerDbContext())
            {
                Items.Clear();

                var accounts = context.Accounts.Include(x => x.Transactions).ToList();

                foreach (var account in accounts)
                {
                    Items.Add(account);
                }
            }
        }

        public override async Task CriarAsync(Account entity)
        {
            using (var context = new FinancialManagerDbContext())
            {
                Items.Add(entity);
                context.Accounts.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public override async Task ExcluirAsync(Account entity)
        {
            var account = Items.SingleOrDefault(c => c.Id == entity.Id);

            if (account != null)
            {
                using (var context = new FinancialManagerDbContext())
                {
                    Items.Remove(account);

                    context.Accounts.Remove(account);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
