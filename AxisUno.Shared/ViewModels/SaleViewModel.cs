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
        private string saleTitle = "Покупка";
        private string titlePartnerString = "Партнёр:";
        private string selectedPartnerString = "Базовый партнёр";
        private decimal totalAmount = 0m;
        private string totalAmountTitle = "Общая сумма:";
        private OperationItemModel? selectedOperationItem;
        private string operationItemNameColumnHeader = "Товар";
        private string operationItemMeasureColumnHeader = "Ед.изм.";
        private string operationItemQuantityColumnHeader = "Кол-во";
        private string operationItemPriceInColumnHeader = "Закупочная цена";
        private string operationItemDiscountColumnHeader = "Скидка";
        private string operationItemTotalAmountColumnHeader = "Общая стоимость";
        private string searchString = string.Empty;
        private ItemModel? selectedGroup;
        private PartnerModel? selectedPartner;
        private ItemModel? selectedItem;
        private string filterString;
        private bool isSaleTitleReadOnly;
        private bool isSelectedPartnerLocked;
        private Visibility editPanelVisibility;

        private double gridWidth;
        public double GridWidth
        {
            get => gridWidth;
            set => SetProperty(ref gridWidth, value);
        }

        public string SaleTitle { get => saleTitle; set => SetProperty(ref saleTitle,value); }

        public string TitlePartnerString { get => titlePartnerString; set => SetProperty(ref titlePartnerString, value); }

        public string SelectedPartnerString { get => selectedPartnerString; set => SetProperty(ref selectedPartnerString, value); }

        public decimal TotalAmount { get => totalAmount; set { SetProperty(ref totalAmount, value); OnPropertyChanged(nameof(TotalAmountString)); } }

        public string TotalAmountTitle { get => totalAmountTitle; set => SetProperty(ref totalAmountTitle, value); }

        public string TotalAmountString { get => TotalAmount.ToString("F"); }

        public ObservableCollection<OperationItemModel> OperationItemsList = new ObservableCollection<OperationItemModel>();

        public OperationItemModel? SelectedOperationItem { get => selectedOperationItem; set => SetProperty(ref selectedOperationItem, value); }

        public string SearchString { get=> searchString; set => SetProperty(ref searchString, value); }

        public ObservableCollection<GroupModel> ItemsGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> PartnersGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ItemModel? SelectedGroup { get => selectedGroup; set => SetProperty(ref selectedGroup, value); }

        public ObservableCollection<PartnerModel> PartnersList { get; set; } = new ObservableCollection<PartnerModel>();

        public PartnerModel? SelectedPartner { get => selectedPartner; set => SetProperty(ref selectedPartner, value); }

        public ObservableCollection<ItemModel> ItemsList { get; set; } = new ObservableCollection<ItemModel>();

        public ItemModel? SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        public string FilterString { get => filterString; set => SetProperty(ref filterString, value); }

        public bool IsSaleTitleReadOnly { get => isSaleTitleReadOnly; set => SetProperty(ref isSaleTitleReadOnly, value); }

        public bool IsSelectedPartnerLocked { get => isSelectedPartnerLocked; set => SetProperty(ref isSelectedPartnerLocked, value); }

        public Visibility EditPanelVisibility { get => editPanelVisibility; set => SetProperty(ref editPanelVisibility, value); }

        public string OperationItemNameColumnHeader { get => operationItemNameColumnHeader; set => SetProperty(ref operationItemNameColumnHeader, value); }

        public string OperationItemMeasureColumnHeader { get => operationItemMeasureColumnHeader; set => SetProperty(ref operationItemMeasureColumnHeader, value); }

        public string OperationItemQuantityColumnHeader { get => operationItemQuantityColumnHeader; set => SetProperty(ref operationItemQuantityColumnHeader, value); }

        public string OperationItemPriceInColumnHeader { get => operationItemPriceInColumnHeader; set => SetProperty(ref operationItemPriceInColumnHeader, value); }

        public string OperationItemDiscountColumnHeader { get => operationItemDiscountColumnHeader; set => SetProperty(ref operationItemDiscountColumnHeader, value); }

        public string OperationItemTotalAmountColumnHeader { get => operationItemTotalAmountColumnHeader; set => SetProperty(ref operationItemTotalAmountColumnHeader, value); }

        [ICommand]
        private void ChangeSaleTitleReadOnly()
        {
            OperationItemModel operationItem = new OperationItemModel();
            operationItem.Name = "Product1";
            operationItem.Measures.Add(new ItemCodeModel { Measure = "Asd2"});
            operationItem.SelectedMeasure = operationItem.Measures[0];
            OperationItemsList.Add(operationItem);
            SelectedOperationItem = operationItem;
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
