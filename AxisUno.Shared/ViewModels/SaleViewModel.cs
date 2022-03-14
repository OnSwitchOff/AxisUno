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
        private string saleTitle = "Покупка";

        [ObservableProperty]
        private string titlePartnerString = "Партнёр:";

        [ObservableProperty]
        private string selectedPartnerString = "Базовый партнёр";

        [ObservableProperty]
        private decimal totalAmount;

        
        [ObservableProperty]
        private string totalAmountTitle = "Общая сумма:";
        [ObservableProperty]
        private string totalAmountString = "0.00";

        public ObservableCollection<OperationItemModel> OperationItemsList { get; set; } = new();

        [ObservableProperty]
        private OperationItemModel selectedOperationItem;

        [ObservableProperty]
        private string searchString;

        public ObservableCollection<GroupModel> ItemsGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> PartnersGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        [ObservableProperty]
        private ItemModel selectedGroup;

        public ObservableCollection<PartnerModel> PartnersList { get; set; } = new ObservableCollection<PartnerModel>();

        [ObservableProperty]
        private PartnerModel selectedPartner;

        public ObservableCollection<ItemModel> ItemsList { get; set; } = new ObservableCollection<ItemModel>();

        [ObservableProperty]
        private ItemModel selectedItem;

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
        private void CloseSalePage()
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
