using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisUno.DataBase.Enteties.Monney.Extensions
{
    public static class SumExtensions
    {
        public static MoneyValue Sum<T>(this IEnumerable<T> source, Func<T, MoneyValue> selector)
        {
            return source.Select(selector).Aggregate((x, y) => x + y);
        }

        public static MoneyValue Sum(this IEnumerable<MoneyValue> source)
        {
            return source.Aggregate((x, y) => x + y);
        }
    }
}
