using AxisUno.DataBase;
using HarabaSourceGenerators.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxisUno.Infrastructure.Domain.UnitOfWorks.DataStorage
{
    [Inject]
    public partial class DataStorage : IDataStorage
    {
        private readonly DatabaseContext _context;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
