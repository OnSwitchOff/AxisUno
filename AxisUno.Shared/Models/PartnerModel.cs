using CommunityToolkit.Mvvm.ComponentModel;
using HarabaSourceGenerators.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Models
{
    [Inject]
    public partial class PartnerModel : ObservableObject
    {
        [ObservableProperty]
        private string name;
    }
}
