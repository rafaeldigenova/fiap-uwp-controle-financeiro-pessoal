using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapControleFinanceiro.UWP.Repository.Base
{
    public interface IRepository<T>
    {
        Task CarregarTodosAsync();
        Task CriarAsync(T entity);
        Task AtualizarAsync(T entity);
        Task ExcluirAsync(T entity);
    }
}
