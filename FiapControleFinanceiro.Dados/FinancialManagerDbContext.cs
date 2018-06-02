using FiapControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapControleFinanceiro.Dados
{
    public class FinancialManagerDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=minhasfinancas.db");
        }
    }
}
