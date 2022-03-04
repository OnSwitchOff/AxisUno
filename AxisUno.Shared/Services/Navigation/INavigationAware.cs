using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Services.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom();
    }
}
