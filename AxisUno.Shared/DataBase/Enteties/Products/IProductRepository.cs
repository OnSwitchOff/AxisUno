using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.DataBase.Enteties.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int productId);

        ValueTask AddAsync(Product product);

        IAsyncEnumerable<Product> GetAllAsync(bool includeProductGroup = false);
    }
}
