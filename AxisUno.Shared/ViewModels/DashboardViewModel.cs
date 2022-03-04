using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarabaSourceGenerators.Common.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.ViewModels
{
    [Inject]
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        [ICommand]
        private async void Click()
        {
           
        }
    }
}
