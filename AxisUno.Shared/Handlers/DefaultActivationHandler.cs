using AxisUno.Services.DataBaseMigration;
using AxisUno.Services.Navigation;
using AxisUno.ViewModels;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.Handlers
{

    [Inject]
    public partial class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        private readonly INavigationService _navigationService;
        private readonly IDatabaseMigrationService _databaseMigrationService;

        protected override async Task HandleInternalAsync(LaunchActivatedEventArgs? args)
        {
            _navigationService.Navigate<DashboardViewModel>(args?.Arguments);

            await _databaseMigrationService.MigrateAsync();
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs? args)
        {
            // None of the ActivationHandlers has handled the app activation
            return _navigationService.Frame.Content == null;
        }
    }
}
