using AxisUno.DataBase.My100REnteties.ApplicationLog;
using AxisUno.DataBase.My100REnteties.Documents;
using AxisUno.DataBase.My100REnteties.Exchanges;
using AxisUno.DataBase.My100REnteties.Items;
using AxisUno.DataBase.My100REnteties.ItemsCodes;
using AxisUno.DataBase.My100REnteties.ItemsGroups;
using AxisUno.DataBase.My100REnteties.OperationDetails;
using AxisUno.DataBase.My100REnteties.OperationHeaders;
using AxisUno.DataBase.My100REnteties.Partners;
using AxisUno.DataBase.My100REnteties.PartnersGroups;
using AxisUno.DataBase.My100REnteties.PaymentTypes;
using AxisUno.DataBase.My100REnteties.Serializations;
using AxisUno.DataBase.My100REnteties.Settings;
using AxisUno.DataBase.My100REnteties.Stores;
using AxisUno.DataBase.My100REnteties.Vatgroups;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace AxisUno.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base()
        {
            Database.EnsureCreated();
        }

        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        //internal DbSet<Product> Products => Set<Product>();
        //internal DbSet<ProductGroup> ProductGroups => Set<ProductGroup>();
        public DbSet<ApplicationLog> ApplicationLogs => Set<ApplicationLog>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Exchange> Exchanges => Set<Exchange>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ItemsCode> ItemsCodes => Set<ItemsCode>();
        public DbSet<ItemsGroup> ItemsGroups => Set<ItemsGroup>();
        public DbSet<OperationDetail> OperationDetails => Set<OperationDetail>();
        public DbSet<OperationHeader> OperationHeaders => Set<OperationHeader>();
        public DbSet<Partner> Partners => Set<Partner>();
        public DbSet<PartnersGroup> PartnersGroups => Set<PartnersGroup>();
        public DbSet<PaymentType> PaymentTypes => Set<PaymentType>();
        public DbSet<Serialization> Serializations => Set<Serialization>();
        public DbSet<Setting> Settings => Set<Setting>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Vatgroup> Vatgroups => Set<Vatgroup>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder()
                {
                    DataSource = "C:\\temp\\dataBase231.db",
                };

                optionsBuilder.UseSqlite(connectionStringBuilder.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
