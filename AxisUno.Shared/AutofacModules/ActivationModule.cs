using Autofac;
using AxisUno.Handlers;
using AxisUno.Services.Activation;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.AutofacModules
{
    public sealed class ActivationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActivationService>().As<IActivationService>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultActivationHandler>().As<ActivationHandler<LaunchActivatedEventArgs>>().InstancePerDependency();
        }
    }
}
