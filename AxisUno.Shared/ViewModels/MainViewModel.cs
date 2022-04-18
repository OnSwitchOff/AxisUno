using AxisUno.Services.Navigation;
using AxisUno.Services.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.ViewModels
{
    [Inject]
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly INavigationViewService _navigationViewService;

        private readonly ISettingsService settingsService;

        public ISettingsService SettingsService => settingsService;

        private string helpMessage;
        private string licenseData = "Продукт активирован 24,09,1990 №123123123";

        public string HelpMessage {
            get => helpMessage;
            set => SetProperty(ref helpMessage, value);
        }

        public string LicenseData
        {
            get => licenseData;
            set => SetProperty(ref licenseData, value);
        }

        private bool isBackEnabled;
        public bool IsBackEnabled { get => isBackEnabled; set => SetProperty(ref  isBackEnabled,value); }

        private NavigationViewItem selected;
        public NavigationViewItem Selected 
        { 
            get => selected;
            set => SetProperty(ref selected, value);
        }

        public MainViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
        {
            _navigationService = navigationService;
            NavigationService.Navigated += OnNavigated;
            _navigationViewService = navigationViewService;

        }

        public INavigationService NavigationService
        {
            get => _navigationService;
        }

        public INavigationViewService NavigationViewService
        {
            get => _navigationViewService;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;

            var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
            if (selectedItem is not null)
            {
                Selected = selectedItem;
            }
        }

        public MainViewModel()
        {

        }
    }
}
