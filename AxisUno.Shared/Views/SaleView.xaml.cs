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
using System.Collections.ObjectModel;
using AxisUno.Models;
using System.Threading;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaleView : Page
    {
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


            GroupModel group1 = new GroupModel() { Name = "gr1"};

            GroupModel group10 = new GroupModel() { Name = "gr10", ParentGroup = group1 };

            GroupModel group11 = new GroupModel() { Name = "gr11", ParentGroup = group1 };

            group1.SubGroups.Add(group10);
            group1.SubGroups.Add(group11);

            GroupModel group2 = new GroupModel() { Name = "gr2"  };

            GroupModel group20 = new GroupModel() { Name = "gr20", ParentGroup = group2 };

            GroupModel group21 = new GroupModel() { Name = "gr21", ParentGroup = group2 };

            GroupModel group200 = new GroupModel() { Name = "gr200", ParentGroup = group20 };

            GroupModel group201 = new GroupModel() { Name = "gr201", ParentGroup = group20 };
            group2.SubGroups.Add(group20);
            group2.SubGroups.Add(group21);
            group20.SubGroups.Add(group200);
            group20.SubGroups.Add(group201);
            //group11.SubGroups.Add(group2);
            ViewModel.TreeViewSource.Add(group1);
            ViewModel.TreeViewSource.Add(group2);

            ViewModel.SelectedTreeViewItem = group201;

            tw.ItemInvoked += Tw_ItemInvoked;
     
            tw.Loaded += Tw_Loaded;
            page.BringIntoViewRequested += Page_BringIntoViewRequested;

            tw.Expanding += Tw_Expanding;
            tw.Collapsed += Tw_Collapsed;

            page.Loaded += Page_Loaded;

            page.DataContextChanged += Page_DataContextChanged;
        }

        private void Page_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (tw.RootNodes.Count == 0)
            {
                return;
            }
            var x = tw.RootNodes.Count;

            GroupModel? parent = ViewModel.SelectedTreeViewItem.ParentGroup;
            while (parent != null)
            {
                parent.IsExpanded = true;
                parent = parent.ParentGroup;
            }

            await SelectTreeViewNodeAsync();
        }

        private async Task SelectTreeViewNodeAsync()
        {
            await Task.Delay(500);

            TreeViewNode SelectedNode = GetSelectedTreeViewNodeByName(ViewModel.SelectedTreeViewItem.Name, tw.RootNodes);
            if (SelectedNode != null)
            {
                tw.SelectedNode = SelectedNode;
            }
        }



        private void Page_BringIntoViewRequested(UIElement sender, BringIntoViewRequestedEventArgs args)
        {

        }

        private void Tw_Expanding(TreeView sender, TreeViewExpandingEventArgs args)
        {

        }

        private void Tw_Collapsed(TreeView sender, TreeViewCollapsedEventArgs args)
        {

        }

        private void Tw_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private TreeViewNode GetSelectedTreeViewNodeByName(string name, IList<TreeViewNode> rootNodes)
        {
            TreeViewNode SelectedNode = rootNodes.Where(n => ((GroupModel)n.Content).Name == name).FirstOrDefault();
            if (SelectedNode != null)
            {
                return SelectedNode;
            }

            foreach (TreeViewNode rootNode in rootNodes)
            {
                var rootName = ((GroupModel)rootNode.Content).Name;
                var chCount = rootNode.Children.Count;
                if (rootNode.Children.Count == 0)
                {
                    continue;
                }
                SelectedNode = GetSelectedTreeViewNodeByName(name, rootNode.Children);
                if (SelectedNode != null)
                {
                    return SelectedNode;
                }
            }
            return null;
        }

        private void Tw_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {

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
