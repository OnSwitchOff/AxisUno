using AxisUno.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxisUno.ViewModels
{
    [Inject]
    public partial class SaleViewModel : ObservableObject
    {

        #region DataRegion
        [ObservableProperty]
        private string saleTitle;

        [ObservableProperty]
        private PartnerModel selectedPartner;


        [ObservableProperty]
        private decimal totalAmount;

        public ObservableCollection<OperationItemModel> OperationItemsList { get; set; } = new();

        [ObservableProperty]
        private OperationItemModel selectedOperationItem;

        [ObservableProperty]
        private string searchString;

        public ObservableCollection<GroupModel> ItemsGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> PartnersGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ObservableCollection<PartnerModel> PartnersList { get; set; } = new ObservableCollection<PartnerModel>();

        public ObservableCollection<ItemModel> ItemsList { get; set; } = new ObservableCollection<ItemModel>();

        [ObservableProperty]
        private string filterString;
        #endregion

        #region StyleRegion
        [ObservableProperty]
        private bool isSaleTitleReadOnly;

        [ObservableProperty]
        private bool isSelectedPartnerLocked;

        [ObservableProperty]
        private Visibility editPanelVisibility;

        #endregion

        [ICommand]
        private void ChangeSaleTitleReadOnly()
        {

        }

        [ICommand]
        private void ChangeSelectedPartnerLocked()
        {

        }

        [ICommand]
        private void SaveSale()
        {

        }

        [ICommand]
        private void ShowEditPanel()
        {

        }


    }
}
