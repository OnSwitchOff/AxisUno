using AxisUno.DataBase.Enteties.ProductGroups;
using AxisUno.DataBase.Enteties.Products;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base()
        {
        }

        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        internal DbSet<Product> Products => Set<Product>();

        internal DbSet<ProductGroup> ProductGroups => Set<ProductGroup>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder()
                {
                    DataSource = "Database.db",
                };

                optionsBuilder.UseSqlite(connectionStringBuilder.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
