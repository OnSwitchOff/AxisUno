using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarabaSourceGenerators.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.ViewModels
{
    [Inject]
    public sealed partial class ReportsViewModel : ObservableObject
    {
        [ICommand]
        private void ClosePage() { }
        //{
        //    get => new RelayCommand(() =>
        //    {

        //    });
        //}
    }
}
