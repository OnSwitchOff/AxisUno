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
using CommunityToolkit.WinUI.UI.Controls.Primitives;
using System.Collections.ObjectModel;
using AxisUno.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI;

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

            tbx_partner.GotFocus += Tbx_partner_GotFocus;
            dg.GotFocus += Gr_GotFocus;
        }

        private void Gr_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.GroupsBtnPanelVisibility = Visibility.Collapsed;
            ViewModel.GoodsBtnPanelVisibility = Visibility.Visible;
            ViewModel.PartnersBtnPanelVisibility = Visibility.Collapsed;
        }

        private void Tbx_partner_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.GroupsBtnPanelVisibility = Visibility.Collapsed;
            ViewModel.GoodsBtnPanelVisibility = Visibility.Collapsed;
            ViewModel.PartnersBtnPanelVisibility = Visibility.Visible;
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           
            if (e.PropertyName == "SaleTitle")
            {
                NavigationViewItem selectedItem = ((MainView)App.MainWindow.Content).NavigationView.SelectedItem as NavigationViewItem;
                selectedItem.Content = (sender as SaleViewModel).SaleTitle;
            }
        }

        public SaleViewModel ViewModel { get; set; }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
