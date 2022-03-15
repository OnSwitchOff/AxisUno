// <copyright file="PartnerModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Models
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using Microinvest.CommonLibrary.Enums;

    /// <summary>
    /// Describes data of partner.
    /// </summary>
    public partial class PartnerModel : ObservableObject
    {
        private int id;
        private string name;
        private string principal;
        private string city;
        private string address;
        private string phone;
        private string email;
        private string taxNumber;
        private string vATNumber;
        private string bankName;
        private string bankBIC;
        private string iBAN;
        private string discountCardNumber;
        private GroupModel group;
        private ENomenclatureStatuses status;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerModel"/> class.
        /// </summary>
        public PartnerModel()
        {
            this.id = 0;
            this.name = string.Empty;
            this.principal = string.Empty;
            this.city = string.Empty;
            this.address = string.Empty;
            this.phone = string.Empty;
            this.email = string.Empty;
            this.taxNumber = string.Empty;
            this.vATNumber = string.Empty;
            this.bankName = string.Empty;
            this.bankBIC = string.Empty;
            this.iBAN = string.Empty;
            this.discountCardNumber = string.Empty;
            this.group = new GroupModel();
            this.status = ENomenclatureStatuses.Active;
        }

        /// <summary>
        /// Gets or sets id of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        /// <summary>
        /// Gets or sets name of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets partner's person in charge.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Principal
        {
            get => this.principal;
            set => this.SetProperty(ref this.principal, value);
        }

        /// <summary>
        /// Gets or sets city of the partner location or registration.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string City
        {
            get => this.city;
            set => this.SetProperty(ref this.city, value);
        }

        /// <summary>
        /// Gets or sets address of the partner location or registration.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Address
        {
            get => this.address;
            set => this.SetProperty(ref this.address, value);
        }

        /// <summary>
        /// Gets or sets phone of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Phone
        {
            get => this.phone;
            set => this.SetProperty(ref this.phone, value);
        }

        /// <summary>
        /// Gets or sets e-mail of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        /// <summary>
        /// Gets or sets partner's tax number.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string TaxNumber
        {
            get => this.taxNumber;
            set => this.SetProperty(ref this.taxNumber, value);
        }

        /// <summary>
        /// Gets partner's VAT number.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string VATNumber
        {
            get => this.vATNumber;
            private set => this.SetProperty(ref this.vATNumber, value);
        }

        /// <summary>
        /// Gets or sets name of a bank which serves of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string BankName
        {
            get => this.bankName;
            set => this.SetProperty(ref this.bankName, value);
        }

        /// <summary>
        /// Gets or sets bIC of a bank which serves of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string BankBIC
        {
            get => this.bankBIC;
            set => this.SetProperty(ref this.bankBIC, value);
        }

        /// <summary>
        /// Gets iBAN of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string IBAN
        {
            get => this.iBAN;
            private set => this.SetProperty(ref this.iBAN, value);
        }

        /// <summary>
        /// Gets or sets number of partner's discount card.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string DiscountCardNumber
        {
            get => this.discountCardNumber;
            set => this.SetProperty(ref this.discountCardNumber, value);
        }

        /// <summary>
        /// Gets or sets group of partner.
        /// </summary>
        /// <date>14.03.2022.</date>
        public GroupModel Group
        {
            get => this.group;
            set => this.SetProperty(ref this.group, value);
        }

        /// <summary>
        /// Gets or sets partner's status.
        /// </summary>
        /// <date>14.03.2022.</date>
        public ENomenclatureStatuses Status
        {
            get => this.status;
            set => this.SetProperty(ref this.status, value);
        }
    }
}
