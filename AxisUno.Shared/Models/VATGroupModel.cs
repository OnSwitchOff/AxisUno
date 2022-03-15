// <copyright file="VATGroupModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Models
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Describes data of VAT group.
    /// </summary>
    public partial class VATGroupModel : ObservableObject
    {
        private int id;
        private string name;
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VATGroupModel"/> class.
        /// </summary>
        public VATGroupModel()
        {
            this.id = 0;
            this.name = string.Empty;
            this.value = 0;
        }

        /// <summary>
        /// Gets or sets id of VAT group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        /// <summary>
        /// Gets or sets name of VAT group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets vAT value (absolute value, not percent).
        /// </summary>
        /// <date>14.03.2022.</date>
        public double Value
        {
            get => this.value;
            set => this.SetProperty(ref this.value, value);
        }
    }
}
