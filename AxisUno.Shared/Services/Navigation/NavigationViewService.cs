using AxisUno.Extensions;
using AxisUno.ViewModels;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxisUno.Services.Navigation
{
    [Inject]
    public partial class NavigationViewService : INavigationViewService
    {
        private readonly INavigationService _navigationService;
        private readonly IViewsService _viewsService;

        private NavigationView _navigationView = new();

        public IList<object> MenuItems
        {
            get => _navigationView.MenuItems;
        }

        public object SettingsItem
        {
            get => _navigationView.SettingsItem;
        }

        public NavigationViewItem? GetSelectedItem(Type pageType)
        {
            return GetSelectedItem(_navigationView.MenuItems, pageType, _navigationView.FooterMenuItems);
        }

        public void Initialize(NavigationView navigationView)
        {
            _navigationView = navigationView;
            _navigationView.BackRequested += OnBackRequested;
            _navigationView.ItemInvoked += OnItemInvoked;
        }

        public void UnregisterEvents()
        {
            _navigationView.BackRequested -= OnBackRequested;
            _navigationView.ItemInvoked -= OnItemInvoked;
        }

        private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type viewType, IEnumerable<object>? footerMenuItems = null)
        {
            foreach (var item in menuItems.OfType<NavigationViewItem>())
            {
                if (MenuItemHasViewType(item, viewType))
                {
                    return item;
                }

                // If navigation view item contains child collection
                var selectedChild = GetSelectedItem(item.MenuItems, viewType);

                if (selectedChild is not null)
                {
                    return selectedChild;
                }
            }

            if (footerMenuItems != null)
            {
                foreach (var item in footerMenuItems.OfType<NavigationViewItem>())
                {
                    if (MenuItemHasViewType(item, viewType))
                    {
                        return item;
                    }

                    // If navigation view item contains child collection
                    var selectedChild = GetSelectedItem(item.MenuItems, viewType);

                    if (selectedChild is not null)
                    {
                        return selectedChild;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Checks, if the manuItem contains NavigateTo property.
        /// </summary>
        /// <param name="sourcePageType">Type of the requested view.</param>
        /// <returns>True, if it's possible to get a view type.</returns>
        private bool MenuItemHasViewType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var pageKey = menuItem.GetValue(NavigationExtension.NavigateToProperty) as string;

            if (pageKey is not null)
            {
                return _viewsService.GetViewType(pageKey) == sourcePageType;
            }
            return false;
        }

        private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _navigationService.Navigate<SettingsViewModel>();
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
                _navigationView.MenuItems.Add(saleItem);
                _navigationService.Navigate(viewKey);
                return;
            }

            if (viewKey is not null)
            {
                _navigationService.Navigate(viewKey);
            }
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            _navigationService.GoBack();
        }
    }
}
