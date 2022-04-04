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
using AxisUno.DataBase;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {

        private Dictionary<string,SaleViewModel> SaleViewModeList = new Dictionary<string,SaleViewModel>();
      

        public NavigationView NavigationView { get => navigationView; }

        public MainView()
        {
            this.InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
            navigationView.ItemInvoked += NavigationView_ItemInvoked;
            navigationView.SelectionChanged += NavigationView_SelectionChanged;
            frame.Navigated += Frame_Navigated;

            using (DatabaseContext db = new DatabaseContext() )
            {
                var tmp = db.Vatgroups;
            }
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

            if (viewKey == "AxisUno.ViewModels.SaleViewModel" && selectedItem.Name == "CreateNewSaleViewItem")
            {
                NavigationViewItem saleItem = new NavigationViewItem();
                int key = 1;
                while(SaleViewModeList.ContainsKey("Sale" + key))
                {
                    key++;
                }
                saleItem.Name = "Sale" + key;
                saleItem.Content = saleItem.Name;
                saleItem?.SetValue(NavigationExtension.NavigateToProperty, "AxisUno.ViewModels.SaleViewModel");
                saleItem.Icon = new SymbolIcon(Symbol.Page);
            
                navigationView.MenuItems.Add(saleItem);
                frame.Navigate(typeof(SaleView));
                SaleViewModeList.Add(saleItem.Name, ((SaleView)frame.Content).ViewModel);

                return;
            }

            if (viewKey is not null)
            {
                switch (viewKey)
                {
                    case "AxisUno.ViewModels.SaleViewModel":
                        frame.Navigate(typeof(SaleView),SaleViewModeList[selectedItem.Name]);

                        break;
                    case "AxisUno.ViewModels.DashboardViewModel":
                        frame.Navigate(typeof(DashboardView));
                        break;
                    default:
                        frame.Navigate(typeof(DashboardView));
                        break;
                }
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is SaleView && e.Parameter is SaleViewModel)
            {
                (e.Content as SaleView).ViewModel = e.Parameter as SaleViewModel;
                (e.Content as SaleView).ViewModel.SaleTitle = (e.Parameter as SaleViewModel).SaleTitle;
            }

        }

        public MainViewModel ViewModel { get; }
    }
}
