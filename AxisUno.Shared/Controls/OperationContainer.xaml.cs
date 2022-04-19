// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AxisUno.Controls
{
    using System.Collections.ObjectModel;
    using AxisUno.Enums;
    using AxisUno.Models;
    using AxisUno.ViewModels;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Media.Imaging;

    public sealed partial class OperationContainer : UserControl
    {
        #region "Declarations"
        // предыдущая высота смещения бегунка полосы прокрутки
        private double previousOffset = 0;
        // массив для хранения высоты страниц
        private double[] pagesHeight;
        // совокупная высота просмотренных/прокрученных пользователем страниц
        private double currentPagesHeight;
        // флаг, показывающий, было ли уже инициировано событие навигации к следующей печатной странице
        private bool isPageNavigationInvoked { get; set; }
        // коэффициент изменения размера отображаемых пользователю страниц из области печати
        private float zoomRatio;
        #endregion

        public OperationContainer()
        {
            this.InitializeComponent();

            zoomRatio = 1.0f;
        }

        
        
        
       
        

        


        /// <summary>
        /// Gets or sets a UIElements of area of title.
        /// </summary>
        /// <date>19.04.2022.</date>
        public object TitleArea
        {
            get => this.GetValue(TitleAreaProperty);
            set => this.SetValue(TitleAreaProperty, value);
        }

        /// <summary>
        /// Register property "TitleArea".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty TitleAreaProperty =
            DependencyProperty.Register("TitleArea", typeof(object), typeof(OperationContainer), null);

        /// <summary>
        /// Gets or sets a command to invoke when button "X" was pressed.
        /// </summary>
        /// <date>19.04.2022.</date>
        public IRelayCommand PageCloseCommand
        {
            get => (IRelayCommand)this.GetValue(PageCloseCommandProperty);
            set => this.SetValue(PageCloseCommandProperty, value);
        }

        /// <summary>
        /// Register property "PageCloseCommand".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty PageCloseCommandProperty =
            DependencyProperty.Register("PageCloseCommand", typeof(IRelayCommand), typeof(OperationContainer), null);

        /// <summary>
        /// Gets or sets a UIElements of area with data of operation.
        /// </summary>
        /// <date>19.04.2022.</date>
        public object OperationArea
        {
            get => this.GetValue(OperationAreaProperty);
            set => this.SetValue(OperationAreaProperty, value);
        }

        /// <summary>
        /// Register property "OperationArea".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty OperationAreaProperty =
            DependencyProperty.Register("OperationArea", typeof(object), typeof(OperationContainer), null);

        /// <summary>
        /// Gets or sets a UIElements of area with data of nomenclatures.
        /// </summary>
        /// <date>19.04.2022.</date>
        public object NomenclatureArea
        {
            get => this.GetValue(NomenclatureAreaProperty);
            set => this.SetValue(NomenclatureAreaProperty, value);
        }

        /// <summary>
        /// Register property "NomenclatureArea".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty NomenclatureAreaProperty =
            DependencyProperty.Register("NomenclatureArea", typeof(object), typeof(OperationContainer), null);

        /// <summary>
        /// Gets or sets a value indicating whether area with data is visible.
        /// </summary>
        /// <date>19.04.2022.</date>
        public bool OperationAreaVisible
        {
            get => (bool)this.GetValue(OperationAreaVisibleProperty);
            set => this.SetValue(OperationAreaVisibleProperty, value);
        }

        /// <summary>
        /// Register property "OperationAreaVisible".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty OperationAreaVisibleProperty =
           DependencyProperty.Register("OperationAreaVisible", typeof(bool), typeof(OperationContainer), new PropertyMetadata(true, null));


        /// <summary>
        /// Gets or sets a UIElements of area with data of document to print.
        /// </summary>
        /// <date>19.04.2022.</date>
        public object PrintContainer
        {
            get => this.GetValue(PrintContainerProperty);
            set => this.SetValue(PrintContainerProperty, value);
        }

        /// <summary>
        /// Register property "PrintContainer".
        /// </summary>
        /// <date>19.04.2022.</date>
        public static readonly DependencyProperty PrintContainerProperty =
           DependencyProperty.Register("PrintContainer", typeof(object), typeof(OperationContainer), null);

        /// <summary>
        /// Hide container with data of document to print.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event args.</param>
        /// <date>19.04.2022.</date>
        private void HidePrintContainer(object sender, RoutedEventArgs e)
        {
            this.OperationAreaVisible = !this.OperationAreaVisible;
        }


        /// <summary>
        /// Ширина страниц в области печати 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public double PageWidth
        {
            get { return (double)GetValue(PageWidthProperty); }
            set { SetValue(PageWidthProperty, value); }
        }

        /// <summary>
        /// Коллекция страниц, отображаемых в области печати
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ObservableCollection<BitmapImage> PdfPages
        {
            get => (ObservableCollection<BitmapImage>)GetValue(PdfPagesProperty);
            set
            {
                SetValue(PdfPagesProperty, value);
                // если страницы переданы - устанавливаем их порядковые номера в ComboBox
                if (value != null && value.Count > 0)
                {
                    Pages.Clear();
                    for (int i = 0; i < value.Count; i++)
                    {
                        Pages.Add(string.Format("{0}/{1}", i + 1, value.Count));
                    }

                    // устанавливаем в качестве текущей первую страницу (индекс "0")
                    CurrentPageIndex = 0;
                }
            }
        }

        /// <summary>
        /// Индекс текущей (отображаемой пользователю) страницы 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        private int CurrentPageIndex
        {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            set
            {
                if (value >= 0 && value < PdfPages?.Count)
                {
                    SetValue(CurrentPageIndexProperty, value);
                }
            }
        }

        /// <summary>
        /// Список порядковых номеров страниц, отображаемых в области печати 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        private ObservableCollection<string> Pages
        {
            get { return (ObservableCollection<string>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }


        /// <summary>
        /// Масштаб отображения страниц в области печати
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        private double ZoomValue
        {
            get { return (double)GetValue(ZoomValueProperty); }
            set
            {
                // поднимаем флаг, чтобы предотвратить выполнение процедур при скролинге страниц
                isPageNavigationInvoked = true;
                // рассчитываем новый коэффициент изменения размера отображаемых пользователю страниц
                zoomRatio = (float)((1 / ((ZoomValue == 0 ? 100 : ZoomValue) / 100)) * (value / 100));
                // изменяем ширину страниц исходя из новых вводных
                PageWidth *= zoomRatio;

                SetValue(ZoomValueProperty, value);
            }
        }

        /// <summary>
        /// Перечень подключенных к данному ПК принтеров
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ObservableCollection<string> InstalledPrinters
        {
            get { return (ObservableCollection<string>)GetValue(InstalledPrintersProperty); }
            set { SetValue(InstalledPrintersProperty, value); }
        }

        /// <summary>
        /// Выбранный пользователем принтер
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public string SelectedPrinter
        {
            get { return (string)GetValue(SelectedPrinterProperty); }
            set { SetValue(SelectedPrinterProperty, value); }
        }

        /// <summary>
        /// Перечень доступных форматов бумаги
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ObservableCollection<ComboBoxItemModel> PageFormats
        {
            get { return (ObservableCollection<ComboBoxItemModel>)GetValue(PageFormatsProperty); }
            set { SetValue(PageFormatsProperty, value); }
        }

        /// <summary>
        /// Выбранный пользователем формат бумаги
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ComboBoxItemModel SelectedPageFormat
        {
            get { return (ComboBoxItemModel)GetValue(SelectedPageFormatProperty); }
            set { SetValue(SelectedPageFormatProperty, value); }
        }

        /// <summary>
        /// Перечень вариантов ориентации бумаги
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ObservableCollection<ComboBoxItemModel> PageOrientations
        {
            get { return (ObservableCollection<ComboBoxItemModel>)GetValue(PageOrientationsProperty); }
            set { SetValue(PageOrientationsProperty, value); }
        }

        /// <summary>
        /// Выбранный пользователем вариант ориентации бумаги
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public ComboBoxItemModel SelectedPageOrientation
        {
            get { return (ComboBoxItemModel)GetValue(SelectedPageOrientationProperty); }
            set { SetValue(SelectedPageOrientationProperty, value); }
        }

        /// <summary>
        /// Оступ слева от края листа 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public double LeftMargin
        {
            get { return (double)GetValue(LeftMarginProperty); }
            set { SetValue(LeftMarginProperty, value); }
        }

        /// <summary>
        /// Оступ сверху от края листа 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public double TopMargin
        {
            get { return (double)GetValue(TopMarginProperty); }
            set { SetValue(TopMarginProperty, value); }
        }

        /// <summary>
        /// Оступ справа от края листа 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public double RightMargin
        {
            get { return (double)GetValue(RightMarginProperty); }
            set { SetValue(RightMarginProperty, value); }
        }

        /// <summary>
        /// Оступ снизу от края листа 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public double BottomMargin
        {
            get { return (double)GetValue(BottomMarginProperty); }
            set { SetValue(BottomMarginProperty, value); }
        }

        /// <summary>
        /// Кол-во печатаемых копий документа
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public int CountCopies
        {
            get { return (int)GetValue(CountCopiesProperty); }
            set { SetValue(CountCopiesProperty, value); }
        }

        /// <summary>
        /// Команда сохранения параметров печати
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public IRelayCommand SavePrintParametersCommand
        {
            get { return (IRelayCommand)GetValue(PageCloseCommandProperty); }
            set { SetValue(PageCloseCommandProperty, value); }
        }

        /// <summary>
        /// Команда отмены изменений параметров печати
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public IRelayCommand CancelPrintParametersCommand
        {
            get { return (IRelayCommand)GetValue(PageCloseCommandProperty); }
            set { SetValue(PageCloseCommandProperty, value); }
        }

        /// <summary>
        /// Команда печати текущего документа
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public IRelayCommand PrintDocumentCommand
        {
            get { return (IRelayCommand)GetValue(PageCloseCommandProperty); }
            set { SetValue(PageCloseCommandProperty, value); }
        }

        /// <summary>
        /// Команда экспорта документа в Pdf 
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public IRelayCommand ExportToPdfCommand
        {
            get { return (IRelayCommand)GetValue(PageCloseCommandProperty); }
            set { SetValue(PageCloseCommandProperty, value); }
        }

        /// <summary>
        /// Версии документов для печати (напр., оригинал, копия и т.п.)
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public EDocumentVersionsPrinting? DocumentVersion
        {
            get { return (EDocumentVersionsPrinting?)GetValue(DocumentVersionProperty); }
            set
            {
                // данная проверка нужна, чтобы предотвратить присвоение "промежуточного" значения при изменении значений RadioButtons
                if (value != null)
                {

                    SetValue(DocumentVersionProperty, value);
                }
            }
        }

        /// <summary>
        /// Видимость секции с перечнем версий документов для печати (напр., оригинал, копия и т.п.)
        /// </summary>
        /// <developer>Сергей Рознюк</developer>
        /// <date>09.04.2021</date>
        public bool IsDocumentVersionPrintingPanelVisibility
        {
            get { return (bool)GetValue(IsDocumentVersionPrintingPanelVisibilityProperty); }
            set { SetValue(IsDocumentVersionPrintingPanelVisibilityProperty, value); }
        }

        
        public static readonly DependencyProperty PdfPagesProperty =
            DependencyProperty.Register("PdfPages", typeof(ObservableCollection<BitmapImage>), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty PageWidthProperty =
            DependencyProperty.Register("PageWidth", typeof(double), typeof(OperationContainer), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty CurrentPageIndexProperty =
           DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(OperationContainer), new PropertyMetadata(0, OnValueChanged));

        public static readonly DependencyProperty PagesProperty =
           DependencyProperty.Register("Pages", typeof(ObservableCollection<string>), typeof(OperationContainer), new PropertyMetadata(new ObservableCollection<string>(), OnValueChanged));

        public static readonly DependencyProperty ZoomValueProperty =
           DependencyProperty.Register("ZoomValue", typeof(double), typeof(OperationContainer), new PropertyMetadata(100.0, OnValueChanged));

        public static readonly DependencyProperty InstalledPrintersProperty =
           DependencyProperty.Register("InstalledPrinters", typeof(ObservableCollection<string>), typeof(OperationContainer), new PropertyMetadata(new ObservableCollection<string>(), OnValueChanged));

        public static readonly DependencyProperty SelectedPrinterProperty =
           DependencyProperty.Register("SelectedPrinter", typeof(string), typeof(OperationContainer), new PropertyMetadata("", OnValueChanged));

        public static readonly DependencyProperty PageFormatsProperty =
           DependencyProperty.Register("PageFormats", typeof(ObservableCollection<ComboBoxItemModel>), typeof(OperationContainer), new PropertyMetadata(new ObservableCollection<ComboBoxItemModel>(), OnValueChanged));

        public static readonly DependencyProperty SelectedPageFormatProperty =
           DependencyProperty.Register("SelectedPageFormat", typeof(ComboBoxItemModel), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty PageOrientationsProperty =
           DependencyProperty.Register("PageOrientations", typeof(ObservableCollection<ComboBoxItemModel>), typeof(OperationContainer), new PropertyMetadata(new ObservableCollection<ComboBoxItemModel>(), OnValueChanged));

        public static readonly DependencyProperty SelectedPageOrientationProperty =
           DependencyProperty.Register("SelectedPageOrientation", typeof(ComboBoxItemModel), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty LeftMarginProperty =
           DependencyProperty.Register("LeftMargin", typeof(double), typeof(OperationContainer), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty TopMarginProperty =
           DependencyProperty.Register("TopMargin", typeof(double), typeof(OperationContainer), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty RightMarginProperty =
           DependencyProperty.Register("RightMargin", typeof(double), typeof(OperationContainer), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty BottomMarginProperty =
           DependencyProperty.Register("BottomMargin", typeof(double), typeof(OperationContainer), new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty CountCopiesProperty =
           DependencyProperty.Register("CountCopies", typeof(double), typeof(OperationContainer), new PropertyMetadata(1, OnValueChanged));

        public static readonly DependencyProperty SavePrintParametersCommandProperty =
            DependencyProperty.Register("SavePrintParametersCommand", typeof(IRelayCommand), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty CancelPrintParametersCommandProperty =
            DependencyProperty.Register("CancelPrintParametersCommand", typeof(IRelayCommand), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty DocumentVersionProperty =
           DependencyProperty.Register("DocumentVersion", typeof(EDocumentVersionsPrinting), typeof(OperationContainer), new PropertyMetadata(EDocumentVersionsPrinting.OriginalAndCopy, OnValueChanged));

        public static readonly DependencyProperty PrintDocumentCommandProperty =
            DependencyProperty.Register("PrintDocumentCommand", typeof(IRelayCommand), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty ExportToPdfCommandProperty =
            DependencyProperty.Register("ExportToPdfCommand", typeof(IRelayCommand), typeof(OperationContainer), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty IsDocumentVersionPrintingPanelVisibilityProperty =
            DependencyProperty.Register("IsDocumentVersionPrintingPanelVisibility", typeof(bool), typeof(OperationContainer), new PropertyMetadata(true, OnValueChanged));
        

        /// <summary>
        /// Обработка изменения всех зарегистрированных свойств
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <developer>Сергей Рознюк</developer>
        /// <date>04.12.2020</date>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
