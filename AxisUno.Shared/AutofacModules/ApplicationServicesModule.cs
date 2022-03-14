using Autofac;
using AxisUno.DataBase.Enteties.ProductGroups;
using AxisUno.Services.ThemeSelector;
using AxisUno.ViewModels;

namespace AxisUno.AutofacModules
{
    public class ApplicationServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<DashboardViewModel>().InstancePerDependency();
            builder.RegisterType<ProductsViewModel>().InstancePerDependency();
            builder.RegisterType<ProductViewModel>().InstancePerDependency();
            builder.RegisterType<SettingsViewModel>().InstancePerDependency();
            builder.RegisterType<SaleViewModel>().InstancePerDependency();
            builder.RegisterType<ThemeSelectorService>().As<IThemeSelectorService>().InstancePerDependency();
            builder.RegisterType<GroupPathGenerator>().As<IGroupPathGenerator>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
