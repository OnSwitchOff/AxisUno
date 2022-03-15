// <copyright file="OperationItemModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Models
{
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Describes data of operation.
    /// </summary>
    public class OperationItemModel : ObservableObject
    {
        private ItemModel item;
        private string code;
        private string name;
        private string barcode;
        private double qty;
        private ObservableCollection<ItemCodeModel> measures;
        private ItemCodeModel selectedMeasure;
        private decimal selectedMultiplier;
        private double partnerDiscount;
        private double itemDiscount;
        private double discount;
        private decimal price;
        private decimal amount;
        private string note;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationItemModel"/> class.
        /// </summary>
        public OperationItemModel()
        {
            this.item = new ItemModel();
            this.code = string.Empty;
            this.name = string.Empty;
            this.barcode = string.Empty;
            this.qty = 0;
            this.selectedMeasure = new ItemCodeModel();
            this.measures = new ObservableCollection<ItemCodeModel>();
            this.measures.Add(this.selectedMeasure);
            this.selectedMultiplier = 1;
            this.partnerDiscount = 0;
            this.itemDiscount = 0;
            this.discount = 0;
            this.price = 0;
            this.amount = 0;
            this.note = string.Empty;
    }

        /// <summary>
        /// Gets or sets id of additional code of item.
        /// </summary>
        /// <date>14.03.2022.</date>
        public ItemModel Item
        {
            get => this.item;
            set => this.SetProperty(ref this.item, value);
        }

        /// <summary>
        /// Gets or sets code of item.
        /// </summary>
        /// <date>15.03.2022.</date>
        public string Code
        {
            get => this.code;
            set => this.SetProperty(ref this.code, value);
        }

        /// <summary>
        /// Gets or sets name of item.
        /// </summary>
        /// <date>15.03.2022.</date>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets barcode of item.
        /// </summary>
        /// <date>15.03.2022.</date>
        public string Barcode
        {
            get => this.barcode;
            set => this.SetProperty(ref this.barcode, value);
        }

        /// <summary>
        /// Gets or sets quantity of item.
        /// </summary>
        /// <date>15.03.2022.</date>
        public double Qty
        {
            get => this.qty;
            set => this.SetProperty(ref this.qty, value);
        }

        /// <summary>
        /// Gets or sets supported measures list.
        /// </summary>
        /// <date>15.03.2022.</date>
        public ObservableCollection<ItemCodeModel> Measures
        {
            get => this.measures;
            set => this.SetProperty(ref this.measures, value);
        }

        /// <summary>
        /// Gets or sets selected measure.
        /// </summary>
        /// <date>15.03.2022.</date>
        public ItemCodeModel SelectedMeasure
        {
            get => this.selectedMeasure;
            set => this.SetProperty(ref this.selectedMeasure, value);
        }

        /// <summary>
        /// Gets or sets selected multiplier.
        /// </summary>
        /// <date>15.03.2022.</date>
        public decimal SelectedMultiplier
        {
            get => this.selectedMultiplier;
            set => this.SetProperty(ref this.selectedMultiplier, value);
        }

        /// <summary>
        /// Gets or sets discount of group of partners.
        /// </summary>
        /// <date>15.03.2022.</date>
        public double PartnerDiscount
        {
            get => this.partnerDiscount;
            set => this.SetProperty(ref this.partnerDiscount, value);
        }

        /// <summary>
        /// Gets or sets discount of group of items.
        /// </summary>
        /// <date>15.03.2022.</date>
        public double ItemDiscount
        {
            get => this.itemDiscount;
            set => this.SetProperty(ref this.itemDiscount, value);
        }

        /// <summary>
        /// Gets or sets discount.
        /// </summary>
        /// <date>15.03.2022.</date>
        public double Discount
        {
            get => this.discount;
            set => this.SetProperty(ref this.discount, value);
        }

        /// <summary>
        /// Gets or sets price of item.
        /// </summary>
        /// <date>15.03.2022.</date>
        public decimal Price
        {
            get => this.price;
            set => this.SetProperty(ref this.price, value);
        }

        /// <summary>
        /// Gets or sets amount to pay.
        /// </summary>
        /// <date>15.03.2022.</date>
        public decimal Amount
        {
            get => this.amount;
            set => this.SetProperty(ref this.amount, value);
        }

        /// <summary>
        /// Gets or sets notey.
        /// </summary>
        /// <date>15.03.2022.</date>
        public string Note
        {
            get => this.note;
            set => this.SetProperty(ref this.note, value);
        }
    }
}
