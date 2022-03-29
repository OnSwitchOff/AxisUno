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

        /// <summary>
        /// Casts VATGroupModel object to DataBase.My100REnteties.Vatgroups.Vatgroup.
        /// </summary>
        /// <param name="vATGroup">Data of VAT group.</param>
        /// <date>25.03.2022.</date>
        public static explicit operator DataBase.My100REnteties.Vatgroups.Vatgroup(VATGroupModel vATGroup)
        {
            DataBase.My100REnteties.Vatgroups.Vatgroup vatgroup = new DataBase.My100REnteties.Vatgroups.Vatgroup()
            {
                Id = vATGroup.Id,
                Name = vATGroup.Name,
                Vatvalue = (decimal)vATGroup.Value,
            };

            return vatgroup;
        }

        /// <summary>
        /// Casts DataBase.My100REnteties.Vatgroups.Vatgroup object to VATGroupModel.
        /// </summary>
        /// <param name="vATGroup">Data of VAT group from database.</param>
        /// <date>25.03.2022.</date>
        public static explicit operator VATGroupModel(DataBase.My100REnteties.Vatgroups.Vatgroup vATGroup)
        {
            VATGroupModel vatgroup = new VATGroupModel()
            {
                Id = vATGroup.Id,
                Name = vATGroup.Name,
                Value = (double)vATGroup.Vatvalue,
            };

            return vatgroup;
        }
    }
}
