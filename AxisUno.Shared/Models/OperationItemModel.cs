using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxisUno.Models
{
    public partial class OperationItemModel : ObservableObject
    {        
        private string name;
        private string selectedMeasure;
        private ObservableCollection<string> measureList = new ObservableCollection<string> { "шт.", "бут." };
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string SelectedMeasure { get => selectedMeasure; set => SetProperty(ref selectedMeasure, value); }
        public ObservableCollection<string> MeasureList { get => measureList; set => SetProperty(ref measureList, value); }
    }
}
