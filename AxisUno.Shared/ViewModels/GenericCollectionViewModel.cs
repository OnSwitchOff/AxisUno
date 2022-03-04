using AxisUno.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxisUno.ViewModels
{
    public abstract partial class GenericCollectionViewModel<TModel> : BaseViewModel
        where TModel : ObservableObject
    {
        public GenericCollectionViewModel(IMediator mediator, INavigationService navigationService)
            : base(mediator, navigationService)
        {
        }

        public TModel? SelectedItem { get; set; } = default;

        public ObservableCollection<TModel> Items { get; set; } = new();

        [ICommand]
        protected abstract void ItemClick();

        [ICommand]
        protected abstract void AddNew();

        [ICommand]
        protected abstract void Refresh();

        [ICommand]
        protected abstract void DeleteSelection();
    }
}
