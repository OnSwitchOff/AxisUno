using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using AxisUno.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DocumentView : Page
    {
        public DocumentView()
        {
            ViewModel = Ioc.Default.GetRequiredService<DocumentViewModel>();
            this.InitializeComponent();
        }

        public DocumentViewModel ViewModel { get; }
    }
}
