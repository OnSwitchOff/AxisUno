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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CashRegisterView : Page
    {
        public CashRegisterView()
        {
            ViewModel = Ioc.Default.GetRequiredService<CashRegisterViewModel>();
            this.DataContext = ViewModel;
            this.InitializeComponent();
        }

        public CashRegisterViewModel ViewModel { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PageClosing != null)
            {
                ViewModel.PageClosing.Invoke(string.Empty);
            }
        }
    }
}
