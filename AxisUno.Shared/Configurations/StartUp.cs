// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Configurations
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AxisUno.AutofacModules;

    /// <summary>
    /// Class to register services.
    /// </summary>
    internal static class Startup
    {
        /// <summary>
        /// Configures services.
        /// </summary>
        /// <returns>AutofacServiceProvider.</returns>
        internal static IServiceProvider ConfigureServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ActivationModule>();

            builder.RegisterModule<NavigationModule>();

            builder.RegisterModule<LoggingModule>();

            builder.RegisterModule<MediatorModule>();

            builder.RegisterModule<ProcessingModule>();

            builder.RegisterModule<ApplicationServicesModule>();

            builder.RegisterModule(
                new DatabaseModule(
                    DatabaseConfiguration.GetOptions()));

            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<SerializationModule>();
            builder.RegisterModule<ScanningModule>();
            builder.RegisterModule<PaymentModule>();
            builder.RegisterModule<SearchDataModule>();
            builder.RegisterModule<DocumentModule>();
            builder.RegisterModule<AxisCloudModule>();

            return new AutofacServiceProvider(builder.Build());
        }
    }
}
