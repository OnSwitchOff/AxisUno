using AxisUno.DataBase;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.Services.DataBaseMigration
{
    [Inject]
    public partial class DatabaseMigrationService : IDatabaseMigrationService
    {
        private readonly DatabaseContext _context;

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
