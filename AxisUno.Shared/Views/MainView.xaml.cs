// <copyright file="MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using AxisUno.Extensions;
    using AxisUno.Services.Translation;
    using AxisUno.ViewModels;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        private readonly ITranslationService translationService;
        private readonly Dictionary<string, SaleView> saleViewList;
        private NavigationViewItemBase? selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();

            this.translationService = TranslationService.CreateInstance();
            this.translationService.LanguageChanged += this.LanguageChanged;

            this.saleViewList = new Dictionary<string, SaleView>();

            this.ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();

            this.navigationView.ItemInvoked += this.NavigationView_ItemInvoked;
            this.navigationView.SelectionChanged += this.NavigationView_SelectionChanged;

            this.frame.Content = new Controls.WelcomeFrame();
            this.frame.Navigated += this.Frame_Navigated;
        }

        /// <summary>
        /// Gets view model of the main page.
        /// </summary>
        /// <date>01.04.2022.</date>
        public MainViewModel ViewModel { get; }

        /// <summary>
        /// Gets NavigationView item of the main page.
        /// </summary>
        /// <date>01.04.2022.</date>
        public NavigationView NavigationView => this.navigationView;

        /// <summary>
        /// Gets default frame to show.
        /// </summary>
        /// <date>21.04.2022.</date>
        private Controls.WelcomeFrame DefaultFrame { get; } = new Controls.WelcomeFrame();

        /// <summary>
        /// Changes values of the items of the NavigationView and the current page.
        /// </summary>
        /// <date>14.04.2022.</date>
        private void LanguageChanged()
        {
            foreach (NavigationViewItemBase item in this.NavigationView.MenuItems)
            {
                string? key;
                if (item.Tag == null)
                {
                    key = item?.GetValue(UIElementExtension.LocalizedTextProperty) as string;
                    if (key != null)
                    {
                        item.Content = this.translationService.Localize(key);
                    }
                }
            }

            foreach (NavigationViewItemBase item in this.NavigationView.FooterMenuItems)
            {
                string? key;
                if (item.Tag == null)
                {
                    key = item?.GetValue(UIElementExtension.LocalizedTextProperty) as string;
                    if (key != null)
                    {
                        item.Content = this.translationService.Localize(key);
                    }
                }
            }

            if (this.frame.Content is SettingsView)
            {
                this.frame.Navigate(typeof(SettingsView));
            }
        }

        /// <summary>
        /// Sets View and ViewModel in according to selected NavigationViewItem.
        /// </summary>
        /// <param name="sender">NavigationView.</param>
        /// <param name="args">Event args.</param>
        /// <date>01.04.2022.</date>
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                this.frame.Navigate(typeof(SettingsView));
                return;
            }

            if (args.InvokedItemContainer is NavigationViewItem selectedItem)
            {
                var viewKey = selectedItem.GetValue(NavigationExtension.NavigateToProperty) as string;

                if (viewKey is not null)
                {
                    switch (viewKey)
                    {
                        case "AxisUno.ViewModels.SaleViewModel":
                            if (this.selectedItem != null)
                            {
                                this.selectedItem.IsSelected = false;
                            }

                            if (selectedItem.Name == "CreateNewSaleViewItem")
                            {
                                this.frame.Navigate(typeof(SaleView));
                                if (this.frame.Content is SaleView saleView)
                                {
                                    saleView.ViewModel.Title = this.translationService.Localize("strSale");
                                    saleView.ViewModel.PageClosing += this.PageClose;
                                    saleView.ViewModel.PageTitleChanging += this.PageTitleChanged;

                                    NavigationViewItem saleItem = new NavigationViewItem()
                                    {
                                        Content = this.translationService.Localize("strSale"),
                                        Icon = new BitmapIcon()
                                        {
                                            UriSource = new System.Uri("ms-appx:///Assets/Icons/prodajba.png"),
                                            ShowAsMonochrome = false,
                                            Width = 18,
                                            Height = 18,
                                            Margin = new Microsoft.UI.Xaml.Thickness(0),
                                        },
                                        Tag = saleView.ViewModel.PageId,
                                        IsSelected = true,
                                    };
                                    saleItem?.SetValue(NavigationExtension.NavigateToProperty, "AxisUno.ViewModels.SaleViewModel");

                                    this.saleViewList.Add(saleView.ViewModel.PageId, saleView);
                                    this.navigationView.MenuItems.Add(saleItem);
                                    this.navigationView.SelectedItem = saleItem;
                                }
                            }
                            else
                            {
                                if (selectedItem.Tag != null &&
                                    selectedItem.Tag is string key &&
                                    this.saleViewList.ContainsKey(key))
                                {
                                    this.frame.Content = this.saleViewList[key];
                                }

                                return;
                            }

                            break;
                        case "AxisUno.ViewModels.DocumentViewModel":
                            this.frame.Navigate(typeof(DocumentView));
                            break;
                        case "AxisUno.ViewModels.CashRegisterViewModel":
                            this.frame.Navigate(typeof(CashRegisterView));
                            break;
                        case "AxisUno.ViewModels.ExchangeViewModel":
                            this.frame.Navigate(typeof(ExchangeView));
                            break;
                        case "AxisUno.ViewModels.ReportsViewModel":
                            this.frame.Navigate(typeof(ReportsView));
                            break;
                        case "AxisUno.ViewModels.SettingsViewModel":
                            this.frame.Navigate(typeof(SettingsView));
                            break;
                        default:
                            this.frame.Content = this.DefaultFrame;
                            break;
                    }
                }

                if (this.frame.Content is Page page && page.DataContext != null && page.DataContext is BaseViewModel viewModel)
                {
                    viewModel.PageClosing += this.PageClose;
                }
            }
        }

        /// <summary>
        /// Sets SelectedItem of NavigationView to selectedItem field.
        /// </summary>
        /// <param name="sender">NavigationView.</param>
        /// <param name="args">Event args.</param>
        /// <date>21.04.2022.</date>
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItemBase itemBase && itemBase.Tag == null)
            {
                this.selectedItem = itemBase;
            }
            else
            {
                this.selectedItem = null;
            }
        }

        /// <summary>
        /// Deletes closed page from the lists.
        /// </summary>
        /// <param name="pageId">Id of the closed page.</param>
        /// <date>14.04.2022.</date>
        private void PageClose(string pageId)
        {
            switch (this.frame.Content)
            {
                case SaleView sale:
                    for (int i = this.navigationView.MenuItems.Count - 1; i >= 0; i--)
                    {
                        if (this.navigationView.MenuItems[i] is NavigationViewItem item)
                        {
                            if (item.Tag != null && item.Tag.ToString() == pageId)
                            {
                                item.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

                                // this.navigationView.MenuItems.Remove(item);
                                this.saleViewList.Remove(pageId);
                                sale.ViewModel.PageClosing -= this.PageClose;
                                sale.ViewModel.PageTitleChanging -= this.PageTitleChanged;
                                break;
                            }
                        }
                    }

                    break;
                default:
                    if (this.frame.Content is Page page)
                    {
                        page.NavigationCacheMode = NavigationCacheMode.Disabled;

                        if (page.DataContext != null && page.DataContext is BaseViewModel viewModel)
                        {
                            viewModel.PageClosing -= this.PageClose;
                            viewModel.PageTitleChanging -= this.PageTitleChanged;
                        }
                    }

                    break;
            }

            if (this.saleViewList.Count > 0)
            {
                this.frame.Content = this.saleViewList.Values.Last();
                this.navigationView.SelectedItem = this.navigationView.
                    MenuItems.
                    Reverse().
                    Where(i =>
                    {
                        if (i is NavigationViewItem item && item.Tag != null && item.Visibility == Microsoft.UI.Xaml.Visibility.Visible)
                        {
                            return true;
                        }

                        return false;
                    }).
                    FirstOrDefault();
            }
            else
            {
                this.frame.Content = this.DefaultFrame;
                this.navigationView.SelectedItem = null;
            }
        }

        /// <summary>
        /// Sets new page title to content of the NavigationViewItem.
        /// </summary>
        /// <param name="newPageTitle">New page title.</param>
        /// <date>14.04.2022.</date>
        private void PageTitleChanged(string newPageTitle)
        {
            if (this.NavigationView.SelectedItem != null && this.NavigationView.SelectedItem is NavigationViewItemBase item)
            {
                item.Content = newPageTitle;
            }
        }

        /// <summary>
        /// Sets the ViewModel to SaleView if one was activated.
        /// </summary>
        /// <param name="sender">Page.</param>
        /// <param name="e">Event args.</param>
        /// <date>01.04.2022.</date>
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is SaleView page && e.Parameter is SaleViewModel viewModel)
            {
                page.ViewModel = viewModel;
            }

        }
    }
}
