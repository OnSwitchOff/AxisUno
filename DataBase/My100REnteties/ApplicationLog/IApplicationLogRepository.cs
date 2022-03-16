using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.DataBase.My100REnteties.ApplicationLog
{
    public interface IApplicationLogRepository
    {
        Task<ApplicationLog?> GetByIdAsync(int productId);

        ValueTask AddAsync(ApplicationLog product);

        IAsyncEnumerable<ApplicationLog> GetAllAsync();
    }
}
