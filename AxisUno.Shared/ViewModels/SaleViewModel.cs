// <copyright file="SaleViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using AxisUno.DataBase.Repositories.Items;
    using AxisUno.DataBase.Repositories.Partners;
    using AxisUno.DataBase.Repositories.Serialization;
    using AxisUno.Models;
    using AxisUno.Services.Serialization;
    using AxisUno.Services.Settings;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using HarabaSourceGenerators.Common.Attributes;
    using Microsoft.UI.Xaml;

    /// <summary>
    /// Contains fields and commands of operation of "Sale".
    /// </summary>
    [Inject]
    public partial class SaleViewModel : ObservableObject
    {
        private readonly IItemRepository itemsRepository;
        private readonly IPartnerRepository partnersRepository;
        private readonly ISerializationRepository serializationRepository;
        private readonly ISettingsService setting;
        private readonly ISerializationService serialization;

        private string saleTitle = "Покупка";

        private PartnerModel? selectedPartner = null;

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
        private Visibility operationItemTotalAmountColumnVisibility = Visibility.Collapsed;
        private string searchString = string.Empty;
        private ItemModel? selectedGroup;
        
        private ItemModel? selectedItem;
        private string filterString;
        private bool isSaleTitleReadOnly;
        private bool isSelectedPartnerLocked;
        private Visibility editPanelVisibility;
        private GroupModel selectedTreeViewItem;

        /// <summary>
        /// Gets serialization service in according to type of page.
        /// </summary>
        /// <date>29.03.2022.</date>
        private ISerializationService Serialization
        {
            get
            {
                if (!this.serialization.SerializationDataInitialized)
                {
                    this.serialization.InitSerializationData(Enums.ESerializationGroups.Sale, this.serializationRepository);
                }

                return this.serialization;
            }
        }

        public string SaleTitle { get => saleTitle; set => SetProperty(ref saleTitle,value); }

        public string TitlePartnerString { get => titlePartnerString; set => SetProperty(ref titlePartnerString, value); }

        

        public decimal TotalAmount { get => totalAmount; set { SetProperty(ref totalAmount, value); OnPropertyChanged(nameof(TotalAmountString)); } }

        public string TotalAmountTitle { get => totalAmountTitle; set => SetProperty(ref totalAmountTitle, value); }

        public string TotalAmountString { get => TotalAmount.ToString("F2"); }

        public ObservableCollection<OperationItemModel> OperationItemsList = new ObservableCollection<OperationItemModel>();

        public OperationItemModel? SelectedOperationItem { get => selectedOperationItem; set => SetProperty(ref selectedOperationItem, value); }

        public string SearchString { get => searchString; set => SetProperty(ref searchString, value); }

        public ObservableCollection<GroupModel> ItemsGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ObservableCollection<GroupModel> PartnersGroupsList { get; set; } = new ObservableCollection<GroupModel>();

        public ItemModel? SelectedGroup { get => selectedGroup; set => SetProperty(ref selectedGroup, value); }

        public ObservableCollection<PartnerModel> PartnersList { get; set; } = new ObservableCollection<PartnerModel>();

        /// <summary>
        /// Gets or sets partner is selected for this operation.
        /// </summary>
        /// <date>24.03.2022.</date>
        public PartnerModel SelectedPartner
        {
            get
            {
                if (this.selectedPartner == null)
                {
                    int id = (int)this.Serialization[Enums.ESerializationKeys.TbPartnerID];
                    this.selectedPartner = this.partnersRepository.GetPartnerById((int)this.Serialization[Enums.ESerializationKeys.TbPartnerID]).GetAwaiter().GetResult();
                    this.SelectedPartnerString = this.selectedPartner.Name;

                    this.Serialization[Enums.ESerializationKeys.TbPartnerID].Value = "1";
                    this.Serialization.Update();
                }

                return this.selectedPartner;
            }

            set
            {
                this.SetProperty(ref this.selectedPartner, value);

                this.SelectedPartnerString = this.selectedPartner.Name;
            }
        }

        /// <summary>
        /// Gets or sets a value to search
        /// </summary>
        /// <date>24.03.2022.</date>
        public string SelectedPartnerString
        {
            get => this.selectedPartnerString;
            set => this.SetProperty(ref this.selectedPartnerString, value);
        }

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
       
        public Visibility OperationItemTotalAmountColumnVisibility { get => operationItemTotalAmountColumnVisibility; set => SetProperty(ref operationItemTotalAmountColumnVisibility, value); }

        public ObservableCollection<GroupModel> TreeViewSource { get; set; } = new ObservableCollection<GroupModel>();
        public GroupModel SelectedTreeViewItem { get => selectedTreeViewItem;
            set
            {
                SetProperty(ref selectedTreeViewItem, value);
            }
        }

        [ICommand]
        private void ChangeSaleTitleReadOnly()
        {
            object obj = this.SelectedPartner;
            OperationItemModel operationItem = new OperationItemModel();
            operationItem.Name = "Product1";
            operationItem.Measures.Add(new ItemCodeModel { Measure = "Asd2"});
            operationItem.SelectedMeasure = operationItem.Measures[0];
            OperationItemsList.Add(operationItem);
            SelectedOperationItem = operationItem;
            OperationItemTotalAmountColumnVisibility = OperationItemTotalAmountColumnVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
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
