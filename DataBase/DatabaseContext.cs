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
        internal DbSet<ApplicationLog> ApplicationLogs => Set<ApplicationLog>(); 
        internal DbSet<Document> Documents => Set<Document>();
        internal DbSet<Exchange> Exchanges => Set<Exchange>();
        internal DbSet<Item> Items => Set<Item>();
        internal DbSet<ItemsCode> ItemsCodes => Set<ItemsCode>();
        internal DbSet<ItemsGroup> ItemsGroups => Set<ItemsGroup>();
        internal DbSet<OperationDetail> OperationDetails => Set<OperationDetail>();
        internal DbSet<OperationHeader> OperationHeaders => Set<OperationHeader>();
        internal DbSet<Partner> Partners => Set<Partner>();
        internal DbSet<PartnersGroup> PartnersGroups => Set<PartnersGroup>();
        internal DbSet<PaymentType> PaymentTypes => Set<PaymentType>();
        internal DbSet<Serialization> Serializations => Set<Serialization>();
        internal DbSet<Setting> Settings => Set<Setting>();
        internal DbSet<Store> Stores => Set<Store>();
        internal DbSet<Vatgroup> Vatgroups => Set<Vatgroup>();

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
