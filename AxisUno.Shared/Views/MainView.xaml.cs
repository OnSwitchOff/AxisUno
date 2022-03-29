using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using AxisUno.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Windowing;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        private IntPtr hWnd = IntPtr.Zero;
        private AppWindow appW = null;
        private OverlappedPresenter presenter = null;

        public MainView()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
            ViewModel.NavigationService.Frame = frame;
            ViewModel.NavigationViewService.Initialize(navigationView);

            //hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            //WindowId wndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            //appW = AppWindow.GetFromWindowId(wndId);
            //presenter = appW.Presenter as OverlappedPresenter;
            //presenter.IsResizable = false;
        }
        public MainViewModel ViewModel { get; }
    }
}
