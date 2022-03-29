using AxisUno.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly INavigationViewService _navigationViewService;

        private bool isBackEnabled;
        public bool IsBackEnabled { get => isBackEnabled; set=> SetProperty(ref  isBackEnabled,value); }

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
    }
}
