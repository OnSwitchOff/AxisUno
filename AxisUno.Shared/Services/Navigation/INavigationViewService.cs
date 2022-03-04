﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace AxisUno.Services.Navigation
{
    public interface INavigationViewService
    {
        IList<object> MenuItems { get; }

        object SettingsItem { get; }

        void Initialize(NavigationView navigationView);

        void UnregisterEvents();

        NavigationViewItem? GetSelectedItem(Type pageType);
    }
}
