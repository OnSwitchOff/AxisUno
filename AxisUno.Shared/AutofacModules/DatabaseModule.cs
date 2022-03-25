using Autofac;
using AxisUno.DataBase;

using AxisUno.Infrastructure.Domain.UnitOfWorks.DataStorage;
using AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;
using AxisUno.Services.DataBaseMigration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.AutofacModules
{
    public sealed class DatabaseModule : Module
    {
        private readonly DbContextOptions<DatabaseContext> _dbContextOptions;

        public DatabaseModule(DbContextOptions<DatabaseContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().WithParameter("options", _dbContextOptions).SingleInstance();

            builder.RegisterType<DomainEventsProvider>().As<IDomainEventsProvider>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseMigrationService>().As<IDatabaseMigrationService>().InstancePerLifetimeScope();
            builder.RegisterType<DataStorage>().As<IDataStorage>().InstancePerLifetimeScope();

            //builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
        }
    }
}
