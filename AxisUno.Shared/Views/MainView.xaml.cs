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
using AxisUno.Extensions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {

        public MainView()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
            navigationView.ItemInvoked += NavigationView_ItemInvoked;
            navigationView.SelectionChanged += NavigationView_SelectionChanged;
            frame.Navigated += Frame_Navigated;
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem == sender.MenuItems[0])
            {
                ViewModel.Selected = sender.MenuItems[sender.MenuItems.Count - 1] as NavigationViewItem;
            }
        }



        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                frame.Navigate(typeof(SettingsView));
                return;
            }

            NavigationViewItem selectedItem = args.InvokedItemContainer as NavigationViewItem;
            var viewKey = selectedItem?.GetValue(NavigationExtension.NavigateToProperty) as string;

            if (viewKey == "AxisUno.ViewModels.SaleViewModel")
            {
                NavigationViewItem saleItem = new NavigationViewItem();
                saleItem.Content = "Sale";
                saleItem?.SetValue(NavigationExtension.NavigateToProperty, "AxisUno.ViewModels.SaleViewModel");
                saleItem.Icon = new SymbolIcon(Symbol.Page);
                navigationView.MenuItems.Add(saleItem);
                frame.Navigate(typeof(SaleView));
                //navigationView.SelectedItem = saleItem;
                return;
            }

            if (viewKey is not null)
            {
                frame.Navigate(typeof(DashboardView));
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        public MainViewModel ViewModel { get; }
    }
}
