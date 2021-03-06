// <copyright file="ItemModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Models
{
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Microinvest.CommonLibrary.Enums;

    /// <summary>
    /// Describes data of item.
    /// </summary>
    public class ItemModel : ObservableObject
    {
        private int id;
        private string code;
        private ObservableCollection<ItemCodeModel> codes;
        private string name;
        private string barcode;
        private string measure;
        private decimal price;
        private GroupModel group;
        private VATGroupModel vATGroup;
        private EItemTypes itemType;
        private ENomenclatureStatuses status;



        /// <summary>
        /// Initializes a new instance of the <see cref="ItemModel"/> class.
        /// </summary>
        public ItemModel()
        {
            this.id = 0;
            this.code = string.Empty;
            this.codes = new ObservableCollection<ItemCodeModel>();
            this.barcode = string.Empty;
            this.measure = string.Empty;
            this.price = 0.0M;
            this.group = new GroupModel();
            this.vATGroup = new VATGroupModel();
            this.itemType = EItemTypes.Standard;
            this.status = ENomenclatureStatuses.Active;
        }

        /// <summary>
        /// Gets or sets id of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        /// <summary>
        /// Gets or sets code of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Code
        {
            get => this.code;
            set => this.SetProperty(ref this.code, value);
        }

        /// <summary>
        /// Gets or sets additional codes of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public ObservableCollection<ItemCodeModel> Codes
        {
            get => this.codes;
            set => this.SetProperty(ref this.codes, value);
        }

        /// <summary>
        /// Gets or sets name of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets barcode of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Barcode
        {
            get => this.barcode;
            set => this.SetProperty(ref this.barcode, value);
        }

        /// <summary>
        /// Gets or sets measure of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Measure
        {
            get => this.measure;
            set
            {
                this.SetProperty(ref this.measure, value);
            } 
        }

        /// <summary>
        /// Gets or sets sale price of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public decimal Price
        {
            get => this.price;
            set => this.SetProperty(ref this.price, value);
        }

        /// <summary>
        /// Gets or sets group of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public GroupModel Group
        {
            get => this.group;
            set => this.SetProperty(ref this.group, value);
        }

        /// <summary>
        /// Gets or sets vAT group of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public VATGroupModel VATGroup
        {
            get => this.vATGroup;
            set => this.SetProperty(ref this.vATGroup, value);
        }

        /// <summary>
        /// Gets or sets type of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public EItemTypes ItemType
        {
            get => this.itemType;
            set => this.SetProperty(ref this.itemType, value);
        }

        /// <summary>
        /// Gets or sets status of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public ENomenclatureStatuses Status
        {
            get => this.status;
            set => this.SetProperty(ref this.status, value);
        }

       


        /// <summary>
        /// Casts SearchService.Models.ProductModel object to ItemModel.
        /// </summary>
        /// <param name="productModel">Product data.</param>
        /// <date>17.03.2022.</date>
        public static implicit operator ItemModel(Microinvest.SearchService.Models.ProductModel productModel)
        {
            ItemModel product = new ItemModel()
            {
                Name = productModel.Name,
                Barcode = productModel.Barcode.ToString(),
                Measure = productModel.Measure,
            };

            return product;
        }

        /// <summary>
        /// Casts SearchService.Models.ProductModel object to ItemModel.
        /// </summary>
        /// <param name="itemModel">Product data.</param>
        /// <date>25.03.2022.</date>
        public static explicit operator DataBase.My100REnteties.Items.Item(ItemModel itemModel)
        {
            DataBase.My100REnteties.Items.Item item = new DataBase.My100REnteties.Items.Item()
            {
                Id = itemModel.Id,
                Code = itemModel.Code,
                Name = itemModel.Name,
                Barcode = itemModel.Barcode,
                Measure = itemModel.Measure,
                Group = (DataBase.My100REnteties.ItemsGroups.ItemsGroup)itemModel.Group,
                Vatgroup = (DataBase.My100REnteties.Vatgroups.Vatgroup)itemModel.VATGroup,
                ItemType = itemModel.ItemType,
                Status = itemModel.Status,
            };

            return item;
        }

        /// <summary>
        /// Casts SearchService.Models.ProductModel object to ItemModel.
        /// </summary>
        /// <param name="item">Product data from database.</param>
        /// <date>25.03.2022.</date>
        public static explicit operator ItemModel(DataBase.My100REnteties.Items.Item item)
        {
            ItemModel itemModel = new ItemModel()
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                Barcode = item.Barcode,
                Measure = item.Measure,
                Group = (GroupModel)item.Group,
                VATGroup = (VATGroupModel)item.Vatgroup,
                ItemType = item.ItemType,
                Status = item.Status,
            };

            return itemModel;
        }
    }
}
