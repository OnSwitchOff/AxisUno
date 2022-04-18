using Microinvest.CommonLibrary.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Extensions
{
    public class NavigationExtension
    {
        // Using a DependencyProperty as the backing store for NavigateTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationExtension), new PropertyMetadata(null));

        public static readonly DependencyProperty DocumentTypeProperty =
           DependencyProperty.RegisterAttached("DocumentType", typeof(EDocumentTypes), typeof(NavigationExtension), new PropertyMetadata(null));

        public static string GetNavigateTo(NavigationViewItem obj)
        {
            return (string)obj.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(NavigationViewItem obj, string value)
        {
            obj.SetValue(NavigateToProperty, value);
        }

        public static EDocumentTypes GetDocumentType(NavigationViewItem obj)
        {
            return (EDocumentTypes)obj.GetValue(DocumentTypeProperty);
        }

        public static void SetDocumentType(NavigationViewItem obj, EDocumentTypes value)
        {
            obj.SetValue(DocumentTypeProperty, value);
        }
    }
}
