using HarabaSourceGenerators.Common.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.DataBase.My100REnteties.ApplicationLog
{
    [Inject]
    public partial class ApplicationLogRepository : IApplicationLogRepository
    {
        private readonly DatabaseContext _databaseContext;
        public async ValueTask AddAsync(ApplicationLog applicationLog)
        {
            await _databaseContext.ApplicationLogs.AddAsync(applicationLog);
        }

        public IAsyncEnumerable<ApplicationLog> GetAllAsync()
        {
            return _databaseContext.ApplicationLogs.AsAsyncEnumerable();
        }

        public Task<ApplicationLog?> GetByIdAsync(int applicationLogId)
        {
            return _databaseContext.ApplicationLogs.FirstOrDefaultAsync(applicationLog => applicationLog.Id == applicationLogId);
        }
    }
}
