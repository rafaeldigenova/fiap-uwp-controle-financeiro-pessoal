using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapControleFinanceiro.UWP.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetValues<T>(this Enum @enum)
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new InvalidOperationException();

            var values = Enum.GetValues(type).Cast<T>();
            return values.ToList();
        }
    }
}
