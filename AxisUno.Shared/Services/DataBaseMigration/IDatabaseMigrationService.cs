using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.Services.DataBaseMigration
{
    public interface IDatabaseMigrationService
    {
        Task MigrateAsync();
    }
}
