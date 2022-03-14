using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Models
{
    public partial class ItemModel: ObservableObject
    {
        [ObservableProperty]
        private string name;
    }
}
