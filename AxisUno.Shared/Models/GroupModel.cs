using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxisUno.Models
{
    public partial class GroupModel: ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string path;

        [ObservableProperty]
        private double discount;

        ObservableCollection<GroupModel> SubGroups { get; set; } = new ObservableCollection<GroupModel>();
    }
}
