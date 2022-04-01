using AxisUno.ViewModels;
using AxisUno.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Extensions
{
    public class UIElementExtension
    {
        public static readonly DependencyProperty HelpMessageProperty =
            DependencyProperty.RegisterAttached("HelpMessage", typeof(string), typeof(UIElementExtension), new PropertyMetadata(null));

        public static string GetHelpMessage(UIElement obj)
        {
            return (string)obj.GetValue(HelpMessageProperty);            
        }

        public static void SetHelpMessage(UIElement obj, string value)
        {
            obj.SetValue(HelpMessageProperty, value);

            if (!string.IsNullOrEmpty(value))
            {
                obj.PointerEntered += Obj_PointerEntered;
                obj.PointerExited += Obj_PointerExited;
            }
        }

        private static void Obj_PointerExited(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (App.MainWindow.Content is MainView)
            {
                ((MainView)App.MainWindow.Content).ViewModel.HelpMessage = string.Empty;
            }
        }

        private static void Obj_PointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (App.MainWindow.Content is MainView)
            {
                ((MainView)App.MainWindow.Content).ViewModel.HelpMessage = (string)((UIElement)sender).GetValue(HelpMessageProperty);
            }
        }
    }
}
