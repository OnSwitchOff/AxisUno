using AxisUno.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;

namespace AxisUno.Configurations
{
    internal static class DatabaseConfiguration
    {
        private const string DatabaseName = "MyApp.db";

        internal static DbContextOptions<DatabaseContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            builder.UseSqlite(GetConnectionString());

            return builder.Options;
        }

        private static string GetConnectionString()
        {
            var fullPath = Path.Combine(GetDatabaseLocation(), DatabaseName);

            return new SqliteConnectionStringBuilder()
            {
                DataSource = fullPath,
            }
            .ToString();
        }

        private static string GetDatabaseLocation()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
