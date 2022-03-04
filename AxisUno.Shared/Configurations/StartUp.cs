using Autofac;
using Autofac.Extensions.DependencyInjection;
using AxisUno.AutofacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Configurations
{
    internal static class Startup
    {
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

            return new AutofacServiceProvider(builder.Build());
        }
    }
}
