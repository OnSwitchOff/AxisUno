using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using AxisUno.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Controls;
using CommunityToolkit.WinUI.UI.Controls.Primitives;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaleView : Page
    {
        public Type DataGridColumnHeaderType { get => typeof(DataGridColumnHeader); }
        public SaleView()
        {

            ViewModel = Ioc.Default.GetRequiredService<SaleViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            this.InitializeComponent();
            if (sp.Children.Count == 0)
            {
                for (int i = 0; i < 255; i += 63)
                {
                    for (int j = 0; j < 255; j += 63)
                    {
                        for (int z = 0; z < 255; z += 63)
                        {
                            byte i1 = (byte)(i % 255);
                            byte j1 = (byte)(j % 255);
                            byte z1 = (byte)(z % 255);
                            Button grid = new Button();
                            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255,i1 ,j1 ,z1 ));
                            grid.Height = 30;
                            grid.Content = $"Dynamic Button:{i1},{j1},{z1}";
                            sp.Children.Add(grid);
                        }
                    }
                }
            }
        }

        private void Dg_Sorting(object? sender, DataGridColumnEventArgs e)
        {
            
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }



        public SaleViewModel ViewModel { get; }
    }
}
