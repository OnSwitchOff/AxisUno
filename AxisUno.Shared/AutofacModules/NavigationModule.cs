using Autofac;
using AxisUno.Services.Navigation;
using AxisUno.Views;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.AutofacModules
{
    internal class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(App.MainWindow).As<Window>();

            builder.RegisterType<ViewsService>().As<IViewsService>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationViewService>().As<INavigationViewService>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationService>().As<INavigationService>().InstancePerLifetimeScope();

            builder.RegisterType<MainView>().InstancePerDependency();
        }
    }
}
