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
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI;
using Windows.UI.ViewManagement;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaleView : Page
    {
        public SaleView()
        {
            ViewModel = Ioc.Default.GetRequiredService<SaleViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            this.InitializeComponent();
            // this.Loaded += Page_Loaded;

            App.MainWindow.SizeChanged += Window_SizeChanged;

            //this.SizeChanged += Page_SizeChanged;
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            this.Width = App.MainWindow.Bounds.Width - ((App.MainWindow.Content as MainView).Content as NavigationView).ActualWidth; 
            this.Height = App.MainWindow.Bounds.Height - 100;
            this.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = App.MainWindow.Bounds.Width - 322; // Window.Current.Bounds.Width;
            this.Height = App.MainWindow.Bounds.Height - 100; // Window.Current.Bounds.Height;
            this.HorizontalAlignment = HorizontalAlignment.Left;

            // this.TestGrid.Width = this.ActualWidth / 2;
            // ViewModel.GridWidth = this.Width / 2;

            Debug.WriteLine(ViewModel.GridWidth);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ApplicationView.PreferredLaunchViewSize = new Size(50, 50);
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        public SaleViewModel ViewModel { get; }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
