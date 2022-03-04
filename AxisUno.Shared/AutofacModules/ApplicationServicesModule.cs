using Autofac;
using AxisUno.DataBase.Enteties.ProductGroups;
using AxisUno.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.AutofacModules
{
    public class ApplicationServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<DashboardViewModel>().InstancePerDependency();
            builder.RegisterType<ProductsViewModel>().InstancePerDependency();
            //builder.RegisterType<ProductViewModel>().InstancePerDependency();
            builder.RegisterType<SettingsViewModel>().InstancePerDependency();

           // builder.RegisterType<ThemeSelectorService>().As<IThemeSelectorService>().InstancePerDependency();

            //builder.RegisterType<GroupPathGenerator>().As<IGroupPathGenerator>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
