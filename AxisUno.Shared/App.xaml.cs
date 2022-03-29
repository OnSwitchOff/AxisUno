using System;
using Microsoft.Extensions.Logging;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using AxisUno.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using AxisUno.Configurations;
using AxisUno.Services.Activation;
using AxisUno.Services.Navigation;

namespace AxisUno
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private Window _window;

        public static Window MainWindow { get; internal set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.RequestedTheme = ApplicationTheme.Light;
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

#if NET6_0_OR_GREATER && WINDOWS
            _window = new Window();
            _window.Activate();
#else
			_window = Microsoft.UI.Xaml.Window.Current;
#endif
            App.MainWindow = _window;
            Ioc.Default.ConfigureServices(Startup.ConfigureServices());
            var navServ = Ioc.Default.GetRequiredService<INavigationService>();
            var activationService = Ioc.Default.GetRequiredService<IActivationService>();
            await activationService.ActivateAsync(args);
        }
    }
}
