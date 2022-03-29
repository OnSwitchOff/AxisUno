// <copyright file="DatabaseModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.AutofacModules
{
    using Autofac;
    using AxisUno.DataBase;
    using AxisUno.DataBase.Repositories.ApplicationLog;
    using AxisUno.DataBase.Repositories.Documents;
    using AxisUno.DataBase.Repositories.Exchanges;
    using AxisUno.DataBase.Repositories.Items;
    using AxisUno.DataBase.Repositories.ItemsCodes;
    using AxisUno.DataBase.Repositories.ItemsGroups;
    using AxisUno.DataBase.Repositories.OperationDetails;
    using AxisUno.DataBase.Repositories.OperationHeader;
    using AxisUno.DataBase.Repositories.Partners;
    using AxisUno.DataBase.Repositories.PartnersGroups;
    using AxisUno.DataBase.Repositories.PaymentTypes;
    using AxisUno.DataBase.Repositories.Serialization;
    using AxisUno.DataBase.Repositories.Settings;
    using AxisUno.DataBase.Repositories.VATs;
    using AxisUno.Infrastructure.Domain.UnitOfWorks.DataStorage;
    using AxisUno.Infrastructure.Domain.UnitOfWorks.DomainEventsDispatching;
    using AxisUno.Services.DataBaseMigration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Register database service.
    /// </summary>
    public sealed class DatabaseModule : Module
    {
        private readonly DbContextOptions<DatabaseContext> dbContextOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseModule"/> class.
        /// </summary>
        /// <param name="dbContextOptions">Class to communicate with database.</param>
        public DatabaseModule(DbContextOptions<DatabaseContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        /// <summary>
        /// Add registration to container.
        /// </summary>
        /// <param name="builder">Container in which service will be added.</param>
        /// <date>16.02.2022.</date>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().WithParameter("options", this.dbContextOptions).SingleInstance();

            builder.RegisterType<DomainEventsProvider>().As<IDomainEventsProvider>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseMigrationService>().As<IDatabaseMigrationService>().InstancePerLifetimeScope();
            builder.RegisterType<DataStorage>().As<IDataStorage>().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationLogRepository>().As<IApplicationLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentsRepository>().As<IDocumentsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExchangesRepository>().As<IExchangesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ItemRepository>().As<IItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ItemsCodesRepository>().As<IItemsCodesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ItemsGroupsRepository>().As<IItemsGroupsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OperationDetailsRepository>().As<IOperationDetailsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OperationHeaderRepository>().As<IOperationHeaderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartnersGroupsRepository>().As<IPartnersGroupsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentTypesRepository>().As<IPaymentTypesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SerializationRepository>().As<ISerializationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VATsRepository>().As<IVATsRepository>().InstancePerLifetimeScope();
        }
    }
}
