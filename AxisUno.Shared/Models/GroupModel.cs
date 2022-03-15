// <copyright file="GroupModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Models
{
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// Describes data of group.
    /// </summary>
    public class GroupModel : ObservableObject
    {
        private int id;
        private string name;
        private string path;
        private double discount;
        private ObservableCollection<GroupModel> subGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupModel"/> class.
        /// </summary>
        public GroupModel()
        {
            this.id = 0;
            this.name = string.Empty;
            this.path = string.Empty;
            this.discount = 0;
            this.subGroups = new ObservableCollection<GroupModel>();
        }

        /// <summary>
        /// Gets or sets get or set Id of group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public int Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        /// <summary>
        /// Gets or sets get or set name of group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        /// <summary>
        /// Gets or sets path of group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public string Path
        {
            get => this.path;
            set => this.SetProperty(ref this.path, value);
        }

        /// <summary>
        /// Gets or sets discount of group.
        /// </summary>
        /// <date>14.03.2022.</date>
        public double Discount
        {
            get => this.discount;
            set => this.SetProperty(ref this.discount, value);
        }

        /// <summary>
        /// Gets or sets list with subgroups.
        /// </summary>
        /// <date>14.03.2022.</date>
        public ObservableCollection<GroupModel> SubGroups
        {
            get => this.subGroups;
            set => this.SetProperty(ref this.subGroups, value);
        }
    }
}