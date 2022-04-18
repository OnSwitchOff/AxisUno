// <copyright file="SaleViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Xml;
    using System.Xml.Serialization;
    using AxisUno.DataBase.My100REnteties.Interfaces;
    using AxisUno.DataBase.My100REnteties.Items;
    using AxisUno.DataBase.My100REnteties.ItemsGroups;
    using AxisUno.DataBase.My100REnteties.Partners;
    using AxisUno.DataBase.My100REnteties.PartnersGroups;
    using AxisUno.DataBase.Repositories.Items;
    using AxisUno.DataBase.Repositories.ItemsGroups;
    using AxisUno.DataBase.Repositories.Partners;
    using AxisUno.DataBase.Repositories.PartnersGroups;
    using AxisUno.DataBase.Repositories.Serialization;
    using AxisUno.Models;
    using AxisUno.Services.Navigation;
    using AxisUno.Services.Serialization;
    using AxisUno.Services.Settings;
    using AxisUno.Services.Translation;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using HarabaSourceGenerators.Common.Attributes;
    using MediatR;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.CommonLibrary.Helpers.Validator;
    using Microsoft.UI.Dispatching;
    using Microsoft.UI.Xaml;
    using static AxisUno.Services.Translation.DictionaryModel;

    /// <summary>
    /// Contains fields and commands of operation of "Sale".
    /// </summary>
    [Inject]
    public partial class SaleViewModel : BaseViewModel, IDisposable
    {
        private readonly IMediator mediator;
        private readonly IItemRepository itemsRepository;
        private readonly IItemsGroupsRepository itemsGroupsRepository;
        private readonly IPartnerRepository partnersRepository;
        private readonly IPartnersGroupsRepository partnersGroupsRepository;
        private readonly ISerializationRepository serializationRepository;
        private readonly ISettingsService setting;
        private readonly ISerializationService serializationSale;
        private readonly ISerializationService serializationItems;
        private readonly ISerializationService serializationPartners;

        //private string? pageId = null;

        //public delegate void PageCloseDelegate(string pageId);
        //public PageCloseDelegate PageClose;
        //public delegate void PageTitleChangedDelegate(string newPageTitle);
        //public PageTitleChangedDelegate PageTitleChanged;
        //public string PageId => this.pageId == null ? this.pageId = Guid.NewGuid().ToString() : this.pageId;

        private Timer findPartnerTimer = null;
        private Timer findItemTimer = null;
        private Timer searchTimer = null;
        private ENomenclatures activeNomenclature;

        private bool isSaleTitleReadOnly = true;
        private PartnerModel selectedPartner = null;
        private string selectedPartnerString = string.Empty;
        private ObservableCollection<PartnerModel> partners;
        private PartnerModel selectedPartnerRow = null;
        private ObservableCollection<GroupModel> partnersGroupsTreeView;
        private GroupModel selectedPartnerGroup = null;
        private ObservableCollection<ItemModel> items;
        private ItemModel selectedItemRow = null;
        private ObservableCollection<GroupModel> itemsGroupsTreeView;
        private GroupModel selectedItemGroup = null;
        private string searchString = string.Empty;
        private ObservableCollection<OperationItemModel> operationItemList;
        private OperationItemModel selectedOperationItem;
        private string totalAmount = null;

        private ItemModel tmpItemModel = new ItemModel();
        private ObservableCollection<string> measures = new ObservableCollection<string> { "шт.", "л/" };
        private ObservableCollection<ComboBoxItemModel> itemsGroups = new ObservableCollection<ComboBoxItemModel>();
        private ObservableCollection<string> editGoodItemTypes = new ObservableCollection<string>();
        private ObservableCollection<VATGroupModel> vatGroups = new ObservableCollection<VATGroupModel>();
        private PartnerModel tmpPartnerModel = new PartnerModel();
        private ObservableCollection<ComboBoxItemModel> partnersGroups = new ObservableCollection<ComboBoxItemModel>();
        private GroupModel tmpGroup = new GroupModel();


        
        private string titlePartnerString = "Партнёр:";
        private string totalAmountTitle = "Общая сумма:";
        private string operationItemNameColumnHeader = "Товар";
        private string operationItemMeasureColumnHeader = "Ед.изм.";
        private string operationItemQuantityColumnHeader = "Кол-во";
        private string operationItemPriceInColumnHeader = "Закупочная цена";
        private string operationItemDiscountColumnHeader = "Скидка";
        private string operationItemTotalAmountColumnHeader = "Общая стоимость";
        private Visibility operationItemTotalAmountColumnVisibility = Visibility.Collapsed;
        private Visibility goodsBtnPanelVisibility = Visibility.Visible;
        private Visibility partnersBtnPanelVisibility = Visibility.Collapsed;
        private Visibility groupsBtnPanelVisibility = Visibility.Collapsed;
        private bool isBtnPanelExpanded = false;
        
        private bool isSelectedPartnerLocked;
        private Visibility editPanelVisibility = Visibility.Collapsed;
        private Visibility editGroupGridVisibility = Visibility.Collapsed;
        private string editGroupName = "Наименование";
        private string editGroupDiscount = "Скидка,%";
        private Visibility editGoodGridVisibility = Visibility.Visible;
        private string editGoodCode = "Код";
        private string editGoodName = "Наименование";
        private string editGoodBarcode = "Штрихкод";
        private string editGoodItemGroup = "Группа товара";
        private string editGoodVatGroup = "Группа НДС";
        private string editGoodItemType = "Тип товара";
        private string editGoodSelectedItemType = string.Empty;
        private string editGoodGeneralMeasure = "Основная ед. изм.";
        private string editGoodPrice = "Цена";
        private string editGoodAdditionalIds = "Дополнительные идентификаторы";
        private Visibility editPartnerGridVisibility = Visibility.Collapsed;
        private string saveBtnTitle = "Save";
        private string cancelBtnTitle = "Cancel";

        private bool isColumnCodeVisible = false;
        private bool isColumnBarcodeVisible = false;

        public string TitlePartnerString { get => titlePartnerString; set => SetProperty(ref titlePartnerString, value); }
        public string TotalAmountTitle { get => totalAmountTitle; set => SetProperty(ref totalAmountTitle, value); }
        
        public bool IsSelectedPartnerLocked { get => isSelectedPartnerLocked; set => SetProperty(ref isSelectedPartnerLocked, value); }
        public Visibility EditPanelVisibility { get => editPanelVisibility; set => SetProperty(ref editPanelVisibility, value); }
        public string OperationItemNameColumnHeader { get => operationItemNameColumnHeader; set => SetProperty(ref operationItemNameColumnHeader, value); }
        public string OperationItemMeasureColumnHeader { get => operationItemMeasureColumnHeader; set => SetProperty(ref operationItemMeasureColumnHeader, value); }
        public string OperationItemQuantityColumnHeader { get => operationItemQuantityColumnHeader; set => SetProperty(ref operationItemQuantityColumnHeader, value); }
        public string OperationItemPriceInColumnHeader { get => operationItemPriceInColumnHeader; set => SetProperty(ref operationItemPriceInColumnHeader, value); }
        public string OperationItemDiscountColumnHeader { get => operationItemDiscountColumnHeader; set => SetProperty(ref operationItemDiscountColumnHeader, value); }
        public string OperationItemTotalAmountColumnHeader { get => operationItemTotalAmountColumnHeader; set => SetProperty(ref operationItemTotalAmountColumnHeader, value); }
        public Visibility OperationItemTotalAmountColumnVisibility { get => operationItemTotalAmountColumnVisibility; set => SetProperty(ref operationItemTotalAmountColumnVisibility, value); }
        public Visibility EditGroupGridVisibility { get => editGroupGridVisibility; set => SetProperty(ref editGroupGridVisibility, value); }
        public string EditGroupName { get => editGroupName; set => SetProperty(ref editGroupName, value); }
        public string EditGroupDiscount { get => editGroupDiscount; set => SetProperty(ref editGroupDiscount, value); }
        public Visibility EditGoodGridVisibility { get => editGoodGridVisibility; set => SetProperty(ref editGoodGridVisibility, value); }
        public string EditGoodCode { get => editGoodCode; set => SetProperty(ref editGoodCode, value); }
        public string EditGoodName { get => editGoodName; set => SetProperty(ref editGoodName, value); }
        public string EditGoodBarcode { get => editGoodBarcode; set => SetProperty(ref editGoodBarcode, value); }
        public string EditGoodItemGroup { get => editGoodItemGroup; set => SetProperty(ref editGoodItemGroup, value); }
        public string EditGoodVatGroup { get => editGoodVatGroup; set => SetProperty(ref editGoodVatGroup, value); }
        public string EditGoodItemType { get => editGoodItemType; set => SetProperty(ref editGoodItemType, value); }
        public string EditGoodSelectedItemType { get => editGoodSelectedItemType; set => SetProperty(ref editGoodSelectedItemType, value); }
        public string EditGoodGeneralMeasure { get => editGoodGeneralMeasure; set => SetProperty(ref editGoodGeneralMeasure, value); }        
        public string EditGoodPrice { get => editGoodPrice; set => SetProperty(ref editGoodPrice, value); }
        public string EditGoodAdditionalIds { get => editGoodAdditionalIds; set => SetProperty(ref editGoodAdditionalIds, value); }
        public Visibility EditPartnerGridVisibility { get => editPartnerGridVisibility; set => SetProperty(ref editPartnerGridVisibility, value); }
        public string SaveBtnTitle { get => saveBtnTitle; set => SetProperty(ref saveBtnTitle, value); }
        public string CancelBtnTitle { get => cancelBtnTitle; set => SetProperty(ref cancelBtnTitle, value); }


        /// <summary>
        /// Gets or sets class to invoke main thread.
        /// </summary>
        /// <date>29.03.2022.</date>
        public DispatcherQueue Dispatcher { get; set; }

        /// <summary>
        /// Gets or sets active nomenclature.
        /// </summary>
        /// <date>31.03.2022.</date>
        public ENomenclatures ActiveNomenclature
        {
            get => this.activeNomenclature;
            set => this.SetProperty(ref this.activeNomenclature, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether title of the page can be changed.
        /// </summary>
        /// <date>24.03.2022.</date>
        public bool IsSaleTitleReadOnly
        {
            get => this.isSaleTitleReadOnly;
            set => this.SetProperty(ref this.isSaleTitleReadOnly, value);
        }

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
                    this.selectedPartner = this.partnersRepository.GetPartnerByIdAsync((int)this.SerializationSale[Enums.ESerializationKeys.TbPartnerID]).GetAwaiter().GetResult();
                    this.SelectedPartnerString = this.selectedPartner.Name;
                }

                return this.selectedPartner;
            }

            set => this.SetProperty(ref this.selectedPartner, value);
        }

        /// <summary>
        /// Gets or sets a value to search partners in the database.
        /// </summary>
        /// <date>24.03.2022.</date>
        public string SelectedPartnerString
        {
            get => this.selectedPartnerString;
            set => this.SetProperty(ref this.selectedPartnerString, value);
        }

        /// <summary>
        /// Gets or sets list of partners.
        /// </summary>
        /// <date>24.03.2022.</date>
        public ObservableCollection<PartnerModel> PartnersList
        {
            get
            {
                if (this.partners == null)
                {
                    this.partners = new ObservableCollection<PartnerModel>();
                    this.FillPartnersList();
                }

                return this.partners;
            }
            set => this.SetProperty(ref this.partners, value);
        }

        /// <summary>
        /// Gets or sets partner selected by user in list with partners.
        /// </summary>
        /// <date>31.03.2022.</date>
        public PartnerModel SelectedPartnerRow
        {
            get => this.selectedPartnerRow;
            set => this.SetProperty(ref this.selectedPartnerRow, value);
        }

        /// <summary>
        /// Gets or sets list of groups of partners.
        /// </summary>
        /// <date>24.03.2022.</date>
        public ObservableCollection<GroupModel> PartnersGroupsTreeView
        {
            get
            {
                if (this.partnersGroupsTreeView == null)
                {
                    this.partnersGroupsTreeView = new ObservableCollection<GroupModel>();
                    this.FillNomenclaturesGroupsListAsync(ENomenclatures.PartnersGroups);
                }

                return this.partnersGroupsTreeView;
            }

            set
            {
                this.SetProperty(ref this.partnersGroupsTreeView, value);
            }
        }

        /// <summary>
        /// Gets or sets group of partner selected by user.
        /// </summary>
        /// <date>24.03.2022.</date>
        public GroupModel SelectedPartnerGroup
        {
            get => this.selectedPartnerGroup;
            set => this.SetProperty(ref this.selectedPartnerGroup, value);
        }

        /// <summary>
        /// Gets or sets list of items.
        /// </summary>
        /// <date>24.03.2022.</date>
        public ObservableCollection<ItemModel> ItemsList
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ObservableCollection<ItemModel>();
                    this.FillItemsList();
                }

                return this.items;
            }
            set => this.SetProperty(ref this.items, value);
        }

        /// <summary>
        /// Gets or sets item selected by user in list with items.
        /// </summary>
        /// <date>31.03.2022.</date>
        public ItemModel SelectedItemRow
        {
            get => this.selectedItemRow;
            set => this.SetProperty(ref this.selectedItemRow, value);
        }

        /// <summary>
        /// Gets or sets list of groups of items.
        /// </summary>
        /// <date>24.03.2022.</date>
        public ObservableCollection<GroupModel> ItemsGroupsTreeView
        {
            get
            {
                if (this.itemsGroupsTreeView == null)
                {
                    this.itemsGroupsTreeView = new ObservableCollection<GroupModel>();
                    this.FillNomenclaturesGroupsListAsync(ENomenclatures.ItemsGroups);
                }

                return this.itemsGroupsTreeView;
            }

            set
            {
                this.SetProperty(ref this.itemsGroupsTreeView, value);
            }
        }

        /// <summary>
        /// Gets or sets group of item selected by user.
        /// </summary>
        /// <date>24.03.2022.</date>
        public GroupModel SelectedItemGroup
        {
            get => this.selectedItemGroup;
            set => this.SetProperty(ref this.selectedItemGroup, value);
        }

        /// <summary>
        /// Gets or sets list with added items.
        /// </summary>
        /// <date>24.03.2022.</date>
        public ObservableCollection<OperationItemModel> OperationItemsList
        {
            get
            {
                if (this.operationItemList == null)
                {
                    this.operationItemList = new ObservableCollection<OperationItemModel>();
                    this.operationItemList.Add(new OperationItemModel());
                    this.operationItemList.CollectionChanged += this.OperationItemsList_CollectionChanged;
                }

                return this.operationItemList;
            }

            set
            {
                if (this.operationItemList != null)
                {
                    this.operationItemList.CollectionChanged -= this.OperationItemsList_CollectionChanged;
                }

                this.SetProperty(ref this.operationItemList, value);
                this.operationItemList.CollectionChanged += this.OperationItemsList_CollectionChanged;
            }
        }

        /// <summary>
        /// Gets or sets amount to pay by document.
        /// </summary>
        /// <date>24.03.2022.</date>
        public string TotalAmount
        {
            get
            {
                if (this.totalAmount == null)
                {
                    this.totalAmount = 0.ToString(this.setting.PriceFormat);
                }

                return this.totalAmount;
            }

            set => this.SetProperty(ref this.totalAmount, value);
        }

        /// <summary>
        /// Gets or sets operation item selected by user.
        /// </summary>
        /// <date>24.03.2022.</date>
        public OperationItemModel SelectedOperationItem
        {
            get => this.selectedOperationItem;
            set => this.SetProperty(ref this.selectedOperationItem, value);
        }

        /// <summary>
        /// Gets or sets value to search nomenclatures in according to data entered by user.
        /// </summary>
        /// <date>24.03.2022.</date>
        public string SearchString
        {
            get => this.searchString;
            set => this.SetProperty(ref this.searchString, value);
        }

        public ItemModel TmpItemModel { get => tmpItemModel; set => SetProperty(ref tmpItemModel, value); }
        public ObservableCollection<string> Measures { get => measures; set => SetProperty(ref measures, value); }
        public ObservableCollection<ComboBoxItemModel> ItemsGroups { get => itemsGroups; set => SetProperty(ref itemsGroups, value); }
        public ObservableCollection<VATGroupModel> VATGroups { get => vatGroups; set => SetProperty(ref vatGroups, value); }
        public ObservableCollection<string> EditGoodItemTypes { get => editGoodItemTypes; set => SetProperty(ref editGoodItemTypes, value); }
        public PartnerModel TmpPartnerModel { get => tmpPartnerModel; set => SetProperty(ref tmpPartnerModel, value); }
        public ObservableCollection<ComboBoxItemModel> PartnersGroups { get => partnersGroups; set => SetProperty(ref partnersGroups, value); }
        public GroupModel TmpGroup { get => tmpGroup; set => SetProperty(ref tmpGroup, value); }

        /// <summary>
        /// Gets serialization service for sale page.
        /// </summary>
        /// <date>29.03.2022.</date>
        private ISerializationService SerializationSale
        {
            get
            {
                if (!this.serializationSale.SerializationDataInitialized)
                {
                    this.serializationSale.InitSerializationData(Enums.ESerializationGroups.Sale, this.serializationRepository);
                }

                return this.serializationSale;
            }
        }

        /// <summary>
        /// Gets serialization service for items section.
        /// </summary>
        /// <date>31.03.2022.</date>
        private ISerializationService SerializationItems
        {
            get
            {
                if (!this.serializationItems.SerializationDataInitialized)
                {
                    this.serializationItems.InitSerializationData(Enums.ESerializationGroups.ItemsNomenclature, this.serializationRepository);
                }

                return this.serializationItems;
            }
        }

        /// <summary>
        /// Gets serialization service for partners section.
        /// </summary>
        /// <date>31.03.2022.</date>
        private ISerializationService SerializationPartners
        {
            get
            {
                if (!this.serializationPartners.SerializationDataInitialized)
                {
                    this.serializationPartners.InitSerializationData(Enums.ESerializationGroups.PartnersNomenclature, this.serializationRepository);
                }

                return this.serializationPartners;
            }
        }

        /// <summary>
        /// Gets object of Timer to search partner in according to value is entered by user.
        /// </summary>
        /// <date>30.03.2022.</date>
        private Timer FindPartnerTimer
        {
            get
            {
                if (this.findPartnerTimer == null)
                {
                    this.findPartnerTimer = new Timer()
                    {
                        Interval = 3000,
                    };

                    this.findPartnerTimer.Elapsed += this.FindPartnerTimer_Elapsed;
                }

                return this.findPartnerTimer;
            }
        }

        /// <summary>
        /// Gets object of Timer to search item in according to value is entered by user.
        /// </summary>
        /// <date>30.03.2022.</date>
        private Timer FindItemTimer
        {
            get
            {
                if (this.findItemTimer == null)
                {
                    this.findItemTimer = new Timer()
                    {
                        Interval = 3000,
                    };

                    this.findItemTimer.Elapsed += this.FindItemTimer_Elapsed;
                }

                return this.findItemTimer;
            }
        }

        /// <summary>
        /// Gets object of Timer to search nomenclatures in according to value is entered by user.
        /// </summary>
        /// <date>30.03.2022.</date>
        private Timer SearchTimer
        {
            get
            {
                if (this.searchTimer == null)
                {
                    this.searchTimer = new Timer()
                    {
                        Interval = 3000,
                    };

                    this.searchTimer.Elapsed += this.SearchNomenclatures_Elapsed;
                }

                return this.searchTimer;
            }
        }

        /// <summary>
        /// Adds item to sale basket.
        /// </summary>
        /// <param name="item">Item to add to basket.</param>
        /// <date>31.03.2022.</date>
        public void AddItemToSaleBasket(ItemModel item)
        {
            OperationItemModel? existingItem = this.OperationItemsList.Where(o => o.Item.Id == item.Id).FirstOrDefault();
            if (existingItem == null)
            {
                this.OperationItemsList.Add(new OperationItemModel()
                {
                    Item = item,
                    PartnerDiscount = this.SelectedPartner.Group.Discount,
                });
            }
            else
            {
                existingItem.Qty++;
            }
        }

        /// <summary>
        /// Dispose unmanaged resources.
        /// </summary>
        /// <date>30.03.2022.</date>
        public void Dispose()
        {
            if (this.FindPartnerTimer.Enabled)
            {
                this.FindPartnerTimer.Stop();
            }

            if (this.FindItemTimer.Enabled)
            {
                this.FindItemTimer.Stop();
            }

            if (this.SearchTimer.Enabled)
            {
                this.SearchTimer.Stop();
            }

            this.FindPartnerTimer.Elapsed -= this.FindPartnerTimer_Elapsed;
            this.FindPartnerTimer.Dispose();

            this.FindItemTimer.Elapsed -= this.FindItemTimer_Elapsed;
            this.FindItemTimer.Dispose();

            this.SearchTimer.Elapsed -= this.SearchNomenclatures_Elapsed;
            this.SearchTimer.Dispose();

            foreach (OperationItemModel operationItem in this.OperationItemsList)
            {
                operationItem.PropertyChanged -= this.OperationItem_PropertyChanged;
            }

            this.OperationItemsList.CollectionChanged -= this.OperationItemsList_CollectionChanged;
        }

        /// <summary>
        /// Updates dependent property when main property of the view model was changed.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        /// <date>30.03.2022.</date>
        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.SelectedPartner):
                    this.SelectedPartnerString = this.SelectedPartner.Name;

                    if (this.FindPartnerTimer.Enabled)
                    {
                        this.FindPartnerTimer.Stop();
                    }

                    break;
                case nameof(this.SelectedPartnerString):
                    if (!this.SelectedPartner.Name.Equals(this.SelectedPartnerString))
                    {
                        if (!this.FindPartnerTimer.Enabled)
                        {
                            this.FindPartnerTimer.Start();
                        }
                    }

                    break;
                case nameof(this.SelectedItemGroup):
                    this.ItemsList.Clear();
                    await foreach (ItemModel item in this.itemsRepository.GetItemsAsync(this.SelectedItemGroup.Path, this.SearchString))
                    {
                        this.ItemsList.Add(item);
                    }

                    break;
                case nameof(this.SelectedPartnerGroup):
                    this.PartnersList.Clear();
                    await foreach (PartnerModel partner in this.partnersRepository.GetParnersAsync(this.SelectedPartnerGroup.Path, this.searchString))
                    {
                        this.PartnersList.Add(partner);
                    }

                    break;
                case nameof(this.SearchString):
                    switch (this.ActiveNomenclature)
                    {
                        case ENomenclatures.Items:
                        case ENomenclatures.ItemsGroups:
                            this.ItemsList.Clear();
                            await foreach (ItemModel item in this.itemsRepository.GetItemsAsync(this.SelectedItemGroup.Path, this.SearchString))
                            {
                                this.ItemsList.Add(item);
                            }

                            break;
                        case ENomenclatures.Partners:
                        case ENomenclatures.PartnersGroups:
                            this.PartnersList.Clear();
                            await foreach (PartnerModel partner in this.partnersRepository.GetParnersAsync(this.SelectedPartnerGroup.Path, this.searchString))
                            {
                                this.PartnersList.Add(partner);
                            }

                            break;
                    }

                    break;
            }

            base.OnPropertyChanged(e);
        }

        /// <summary>
        /// Fills list of partners.
        /// </summary>
        /// <date>31.03.2022.</date>
        private async void FillPartnersList()
        {
            string groupPath = await this.partnersGroupsRepository.GetPathByIdAsync((int)this.SerializationPartners[Enums.ESerializationKeys.SelectedGroupNodeTag]);
            await foreach (PartnerModel partner in this.partnersRepository.GetParnersAsync(groupPath, string.Empty))
            {
                this.partners.Add(partner);
            }
        }

        /// <summary>
        /// Fills list of items.
        /// </summary>
        /// <date>31.03.2022.</date>
        private async void FillItemsList()
        {
            string groupPath = await this.itemsGroupsRepository.GetPathByIdAsync((int)this.SerializationItems[Enums.ESerializationKeys.SelectedGroupNodeTag]);
            await foreach (ItemModel item in this.itemsRepository.GetItemsAsync(groupPath, string.Empty))
            {
                this.items.Add(item);
            }
        }

        /// <summary>
        /// Fills list of groups of nomenclatures in according to type of nomenclature.
        /// </summary>
        /// <param name="nomenclature">Type of nomenclature.</param>
        /// <date>01.04.2022.</date>
        private async void FillNomenclaturesGroupsListAsync(ENomenclatures nomenclature)
        {
            switch (nomenclature)
            {
                case ENomenclatures.PartnersGroups:
                    this.FillNomenclaturesGroupsRecursion<PartnersGroup>(
                        this.partnersGroupsTreeView,
                        await this.partnersGroupsRepository.GetPartnersGroupsAsync(),
                        this.partnersGroups);
                    break;
                case ENomenclatures.ItemsGroups:
                    this.FillNomenclaturesGroupsRecursion<ItemsGroup>(
                        this.itemsGroupsTreeView,
                        await this.itemsGroupsRepository.GetItemsGroupsAsync(),
                        this.itemsGroups);
                    break;
            }
        }

        /// <summary>
        /// Fills list of groups of nomenclatures.
        /// </summary>
        /// <typeparam name="T">Type of nomenclature.</typeparam>
        /// <param name="groupsList">List of groups used by current view model.</param>
        /// <param name="repositoryList">List of groups downloaded from database.</param>
        /// <param name="pathLenght">Lenght of path property.</param>
        /// <param name="startIndex">Index to start of search in the list of groups.</param>
        /// <param name="parentGroupPath">Path of a parent group.</param>
        /// <date>01.04.2022.</date>
        private void FillNomenclaturesGroupsRecursion<T>(ObservableCollection<GroupModel> groupsList, List<T> repositoryList, ObservableCollection<ComboBoxItemModel> comboBoxList, int pathLenght = 3, int startIndex = 0, string parentGroupPath = "")
            where T : INomenclaturesGroups
        {
            GroupModel group;

            for (; startIndex < repositoryList.Count; startIndex++)
            {
                if ((repositoryList[startIndex].Path.Length == pathLenght || repositoryList[startIndex].Path.Equals("-1")) && (string.IsNullOrEmpty(parentGroupPath) || repositoryList[startIndex].Path.StartsWith(parentGroupPath)))
                {
                    group = new GroupModel();
                    group.Id = repositoryList[startIndex].Id;
                    group.Name = repositoryList[startIndex].Name;
                    group.Path = repositoryList[startIndex].Path;
                    groupsList.Add(group);
                    comboBoxList.Add(this.CreateComboItem(group));

                    this.FillNomenclaturesGroupsRecursion(group.SubGroups, repositoryList, comboBoxList, pathLenght + 3, startIndex + 1, group.Path);
                }
            }
        }

        /// <summary>
        /// Create ComboBoxItemModel for a list of groups.
        /// </summary>
        /// <param name="group">Group of nomenclatures.</param>
        /// <returns>Returns ComboBoxItemModel object.</returns>
        /// <date>01.04.2022.</date>
        private ComboBoxItemModel CreateComboItem(GroupModel group)
        {
            ComboBoxItemModel comboBoxItem = new ComboBoxItemModel()
            {
                Value = group,
            };
            if (group.Path.Length <= 3)
            {
                comboBoxItem.Text = group.Name;
            }
            else
            {
                comboBoxItem.Text = new string(' ', (int)Math.Pow(2, (double)((group.Path.Length / 3) - 1))) + group.Name;
            }

            return comboBoxItem;
        }

        /// <summary>
        /// Search partner in according to value is entered by user.
        /// </summary>
        /// <param name="sender">Timer.</param>
        /// <param name="e">Event args.</param>
        /// <date>30.03.2022.</date>
        private void FindPartnerTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Dispatcher.TryEnqueue(async () =>
            {
                this.PartnersList.Clear();

                await foreach (Partner partner in this.partnersRepository.GetParnersAsync(this.SelectedPartnerString))
                {
                    this.PartnersList.Add(partner);
                }
            });
        }

        /// <summary>
        /// Search item in according to value is entered by user.
        /// </summary>
        /// <param name="sender">Timer.</param>
        /// <param name="e">Event args.</param>
        /// <date>31.03.2022.</date>
        private void FindItemTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Dispatcher.TryEnqueue(async () =>
            {
                this.ItemsList.Clear();
                await foreach (ItemModel item in this.itemsRepository.GetItemsAsync(this.SelectedOperationItem.Name))
                {
                    this.ItemsList.Add(item);
                }
            });
        }

        /// <summary>
        /// Search nomenclatures in according to value is entered by user.
        /// </summary>
        /// <param name="sender">Timer.</param>
        /// <param name="e">Event args.</param>
        /// <date>31.03.2022.</date>
        private void SearchNomenclatures_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Dispatcher.TryEnqueue(async () =>
            {
                switch (this.ActiveNomenclature)
                {
                    case ENomenclatures.Items:
                    case ENomenclatures.ItemsGroups:
                        this.ItemsList.Clear();
                        await foreach (ItemModel item in this.itemsRepository.GetItemsAsync(this.SelectedItemGroup.Path, this.SearchString))
                        {
                            this.ItemsList.Add(item);
                        }

                        break;
                    case ENomenclatures.Partners:
                    case ENomenclatures.PartnersGroups:
                        this.PartnersList.Clear();
                        await foreach (PartnerModel item in this.partnersRepository.GetParnersAsync(this.SelectedPartnerGroup.Path, this.SearchString))
                        {
                            this.PartnersList.Add(item);
                        }

                        break;
                }
            });
        }

        public Visibility GoodsBtnPanelVisibility { get => goodsBtnPanelVisibility; set => SetProperty(ref goodsBtnPanelVisibility, value); }

        public Visibility GroupsBtnPanelVisibility { get => groupsBtnPanelVisibility; set => SetProperty(ref groupsBtnPanelVisibility, value); }

        public Visibility PartnersBtnPanelVisibility { get => partnersBtnPanelVisibility; set => SetProperty(ref partnersBtnPanelVisibility, value); }

        public Visibility BtnPanelVisibility { get => IsBtnPanelExpanded ? Visibility.Collapsed : Visibility.Visible; }

        public bool IsBtnPanelExpanded { get => isBtnPanelExpanded;
            set {
                SetProperty(ref isBtnPanelExpanded, value);
                OnPropertyChanged("ExpandBtnTransfAngle");
                OnPropertyChanged("BtnPanelVisibility");
            }
        }

        public double ExpandBtnTransfAngle
        {
            get => IsBtnPanelExpanded ? 0 : 180;
        }

        /// <summary>
        /// Add or remove a subscription to the PropertyChanged event of OperationItemModel.
        /// </summary>
        /// <param name="sender">List with operation items.</param>
        /// <param name="e">Event args.</param>
        /// <date>31.03.2022.</date>
        private void OperationItemsList_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (OperationItemModel item in e.NewItems)
                        {
                            if (item != null)
                            {
                                item.PropertyChanged += this.OperationItem_PropertyChanged;
                            }
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (OperationItemModel item in e.OldItems)
                        {
                            if (item != null)
                            {
                                item.PropertyChanged -= this.OperationItem_PropertyChanged;
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Updates dependent property when main property of the OperationItemModel was changed.
        /// </summary>
        /// <param name="sender">OperationItemModel.</param>
        /// <param name="e">Event args.</param>
        /// <date>31.03.2022.</date>
        private async void OperationItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OperationItemModel.Amount):
                    this.TotalAmount = await Task.Run<string>(() =>
                    {
                        return this.OperationItemsList.Sum(i => i.Amount).ToString(this.setting.PriceFormat);
                    });
                    break;
                case nameof(OperationItemModel.Name):
                    if (this.SelectedOperationItem != null)
                    {
                        if (!this.SelectedOperationItem.Item.Name.Equals(this.SelectedOperationItem.Name))
                        {
                            string inputValue = this.SelectedOperationItem.Name;
                            if (BaseValidator.IsBarcode(ref inputValue))
                            {
                                if (this.FindItemTimer.Enabled)
                                {
                                    this.FindItemTimer.Stop();
                                }

                                this.AddItemToSaleBasket(await this.itemsRepository.GetItemByBarcodeAsync(inputValue));
                            }
                            else
                            {
                                if (!this.FindItemTimer.Enabled)
                                {
                                    this.FindItemTimer.Start();
                                }
                            }
                        }
                    }

                    break;
                case nameof(OperationItemModel.Item):
                    if (this.FindItemTimer.Enabled)
                    {
                        this.FindItemTimer.Stop();
                    }

                    break;
            }
        }

        /// <summary>
        /// Adds item to sale basket.
        /// </summary>
        /// <param name="item">Item to add to basket.</param>
        /// <date>31.03.2022.</date>
        private void AddItemToSaleBasket(Item? item)
        {
            if (item != null)
            {
                this.AddItemToSaleBasket((ItemModel)item);
            }
        }

        /// <summary>
        /// Enables field to change title of the page.
        /// </summary>
        /// <date>23.03.2022.</date>
        [ICommand]
        private void ChangeSaleTitleReadOnly()
        {
            this.IsSaleTitleReadOnly = !this.IsSaleTitleReadOnly;
        }

        /// <summary>
        /// Serialize visual data and call dispose method.
        /// </summary>
        /// <date>24.03.2022.</date>
        [ICommand]
        private void CloseSalePage()
        {
            // TODO: serialize data
            this.Dispose();

            if (PageClosing != null)
            {
                PageClosing.Invoke(PageId);
            }
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

        [ICommand]
        private void ChangeBtnPanelExpandMode()
        {
            IsBtnPanelExpanded = !IsBtnPanelExpanded;
        }
    }
}
