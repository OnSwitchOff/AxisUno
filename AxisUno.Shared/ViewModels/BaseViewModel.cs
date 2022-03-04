using AxisUno.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        private readonly INavigationService _navigationService;

        public BaseViewModel(IMediator mediator, INavigationService navigationService)
        {
            _mediator = mediator;
            _navigationService = navigationService;
        }

        public virtual string HeaderText => string.Empty;

        protected IMediator Mediator => _mediator;

        protected INavigationService NavigationService => _navigationService;
    }
}
