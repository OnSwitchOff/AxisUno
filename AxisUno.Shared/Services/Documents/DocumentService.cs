﻿// <copyright file="DocumentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using AxisUno.Enums;
    using AxisUno.Models;
    using AxisUno.Services.Settings;
    using HarabaSourceGenerators.Common.Attributes;
    using Microinvest.CommonLibrary.Enums;
    using Microinvest.PDFCreator;
    using Microinvest.PDFCreator.Enums;
    using Microinvest.PDFCreator.Models;

    /// <summary>
    /// Describes service to generate pdf document.
    /// </summary>
    [Inject]
    public partial class DocumentService : IDocumentService
    {
        private ISettingsService settings;
        private MicroinvestPdfDocument pdfDocument;
        private DocumentPageModel pageParameters;
        private double firstPageHeaderHeight;
        private string logoPath;
        private Image logo;
        private string signaturePath;
        private Image signature;
        private double[] tableColumnsWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="settings">Applocation settings.</param>
        [Obsolete]
        public DocumentService(ISettingsService settings)
        {
            this.settings = settings;

            this.pdfDocument = new MicroinvestPdfDocument();

            // устанавливаем флаг, что верхний и нижний колонтитулы первой страницы будут отличными от остальных страниц
            this.pdfDocument.DifferentFirstPageHeaderFooter = true;

            // устанавливаем кол-во знаков после запятой при указании цены
            this.pdfDocument.PriceFormat = this.settings.Culture.NumberFormat.CurrencyDecimalDigits;

            // устанавливаем кол-во знаков после запятой при указании количества
            this.pdfDocument.QuantityFormat = this.settings.Culture.NumberFormat.NumberDecimalDigits;

            // запоминаем высоту верхнего колонтитула первой страницы, чтобы иметь возможность восстановить её в дальнейшем
            this.firstPageHeaderHeight = this.pdfDocument.FirstPageHeaderHeight;

            // устанавливаем путь к изображению с логотипом фирмы
            this.logoPath = this.settings.LogoPath;

            // устанавливаем логотип компании по умолчанию
            this.logo = this.pdfDocument.DefaultHeaderImage;

            // устанавливаем путь к изображению с реквизитами фирмы
            this.signaturePath = string.Empty;

            // устанавливаем изображение с реквизитами компании по умолчанию
            this.signature = this.pdfDocument.DefaultFooterImage;

            this.pageParameters = new DocumentPageModel();
            this.pageParameters.PropertyChanged += this.PageParameters_PropertyChanged;

            // устанавливаем данные по нашей компании
            this.pdfDocument.Saler.Name = this.settings.AppSettings[ESettingKeys.Company];
            this.pdfDocument.Saler.Address =
                string.Format(
                    "{0}, {1}",
                    this.settings.AppSettings[ESettingKeys.City],
                    this.settings.AppSettings[ESettingKeys.Address]);
            this.pdfDocument.Saler.Principal = this.settings.AppSettings[ESettingKeys.Principal];
            this.pdfDocument.Saler.TaxNumber = this.settings.AppSettings[ESettingKeys.TaxNumber];
            this.pdfDocument.Saler.VATNumber = this.settings.AppSettings[ESettingKeys.VATNumber];
            this.pdfDocument.Saler.BankName = this.settings.AppSettings[ESettingKeys.BankName];
            this.pdfDocument.Saler.BIC = this.settings.AppSettings[ESettingKeys.BankBIC];
            this.pdfDocument.Saler.IBAN = this.settings.AppSettings[ESettingKeys.IBAN];
            this.pdfDocument.Saler.StoreName = string.Empty;

            // устанавливаем данные по группам НДС !!!! грузим данные из базы !!!!!!!!!!!!!!!
            this.pdfDocument.VATs.Add(new VATModel() { VATRate = 20, VATBase = 86.19, VATSum = 17.24 });
            this.pdfDocument.VATs.Add(new VATModel() { VATRate = 9, VATBase = 86.19, VATSum = 17.24 });
            this.pdfDocument.VATs.Add(new VATModel() { VATRate = 10, VATBase = 86.19, VATSum = 17.24 });
        }

        /// <summary>
        /// Gets or sets data to generate document.
        /// </summary>
        /// <date>18.03.2022.</date>
        public DataTable ItemsData
        {
            get => this.pdfDocument.DocumentData == null ? this.pdfDocument.DocumentData = new DataTable() : this.pdfDocument.DocumentData;
            set => this.pdfDocument.DocumentData = value;
        }

        /// <summary>
        /// Gets or sets width of the columns of the utem data table.
        /// </summary>
        /// <date>18.03.2022.</date>
        public double[] ItemsTableColumnsWidth
        {
            get
            {
                // если массив с шириной колонок таблицы не сформирован - формируем его с данными по умолчанию
                if (this.tableColumnsWidth == null)
                {
                    this.tableColumnsWidth = new double[this.ItemsData.Columns.Count > 0 ? this.ItemsData.Columns.Count : 1];
                    double defaultWidth = (this.pdfDocument.PageWidth - this.pdfDocument.LeftIndentation - this.pdfDocument.RightIndentation) / this.tableColumnsWidth.Length;
                    for (int i = 0; i < this.tableColumnsWidth.Length; i++)
                    {
                        this.tableColumnsWidth[i] = defaultWidth;
                    }
                }

                return this.tableColumnsWidth;
            }
            set => this.tableColumnsWidth = value;
        }

        /// <summary>
        /// Gets or sets path with logo of company.
        /// </summary>
        /// <date>18.03.2022.</date>
        public string CompanyLogoPath
        {
            get => this.logoPath;
            set => this.SetImageValues(ref this.logoPath, value, ref this.logo);
        }

        /// <summary>
        /// Gets or sets path with signature of company.
        /// </summary>
        /// <date>18.03.2022.</date>
        public string CompanySignaturePath
        {
            get => this.signaturePath;
            set => this.SetImageValues(ref this.signaturePath, value, ref this.signature);
        }

        /// <summary>
        /// Sets data of customer.
        /// </summary>
        /// <date>18.03.2022.</date>
        public PartnerModel CustomerData
        {
            set => this.pdfDocument.Client = (Microinvest.PDFCreator.Models.CompanyModel)value;
        }

        /// <summary>
        /// Gets or sets data of a document fields.
        /// </summary>
        /// <date>18.03.2022.</date>
        public DocumentModel DocumentDescription
        {
            get => this.pdfDocument.Document;
            set => this.pdfDocument.Document = value;
        }

        /// <summary>
        /// Gets or sets parameters of a document page.
        /// </summary>
        /// <date>18.03.2022.</date>
        public DocumentPageModel PageParameters
        {
            get => this.pageParameters;
            set
            {
                this.pageParameters.PropertyChanged -= this.PageParameters_PropertyChanged;
                this.pageParameters = value;
                this.pageParameters.PropertyChanged += this.PageParameters_PropertyChanged;

                this.pdfDocument.LeftIndentation = this.pageParameters.LeftMargin;
                this.pdfDocument.TopIndentation = this.pageParameters.TopMargin;
                this.pdfDocument.RightIndentation = this.pageParameters.RightMargin;
                this.pdfDocument.BottomIndentation = this.pageParameters.BottomMargin;
                this.pdfDocument.Orientation = this.pageParameters.PageOrientation;
                this.pdfDocument.PageFormat = this.pageParameters.PageFormat;
            }
        }

        /// <summary>
        /// Generates report.
        /// </summary>
        /// <returns>Returns true if a report was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        [Obsolete]
        public bool GenerateReport()
        {
            // устанавливаем флаг, что верхний и нижний колонтитулы первой страницы не будут отличными от остальных страниц
            this.pdfDocument.DifferentFirstPageHeaderFooter = false;

            // очищаем предыдущую разметку (если таковая осталась с предыдущего раза)
            this.pdfDocument.Clear();

            try
            {
                // обнуляем "задел", выделенный для верхнего колонтитула первой страницы
                this.pdfDocument.FirstPageHeaderHeight = 0;

                // устанавливаем стиль для визуализации таблицы
                TableVisualizationModel tableVisualization = new TableVisualizationModel();
                tableVisualization.HeaderBackground.Color_R = 30;
                tableVisualization.HeaderBackground.Color_G = 144;
                tableVisualization.HeaderBackground.Color_B = 255;
                tableVisualization.HeaderFont.IsBold = true;
                tableVisualization.DifferentEvenRowsBackground = true;

                // формируем разметку таблицы
                this.pdfDocument.AddNewItemToContent(
                    this.pdfDocument.CreateAndFillTable(
                        this.ItemsData,
                        this.ItemsTableColumnsWidth,
                        tableVisualization));

                // генерируем разметку
                this.pdfDocument.RenderDocument();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generate invoice.
        /// </summary>
        /// <param name="versionPrinting">Version of document to print.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <returns>Returns true if an invoice was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool GenerateInvoice(EDocumentVersionsPrinting versionPrinting = EDocumentVersionsPrinting.Original, EPaymentTypes paymentType = EPaymentTypes.Cash)
        {
            return this.GenerateDocument(EDocumentTypes.Invoice, versionPrinting, paymentType);
        }

        /// <summary>
        /// Generate credit note.
        /// </summary>
        /// <param name="versionPrinting">Versions of document to print.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <returns>Returns true if a credit note was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool GenerateCreditNote(EDocumentVersionsPrinting versionPrinting = EDocumentVersionsPrinting.Original, EPaymentTypes paymentType = EPaymentTypes.Cash)
        {
            return this.GenerateDocument(EDocumentTypes.CreditNote, versionPrinting, paymentType);
        }

        /// <summary>
        /// Generate debit note.
        /// </summary>
        /// <param name="versionPrinting">Versions of document to print.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <returns>Returns true if a debit note was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool GenerateDebitNote(EDocumentVersionsPrinting versionPrinting = EDocumentVersionsPrinting.Original, EPaymentTypes paymentType = EPaymentTypes.Cash)
        {
            return this.GenerateDocument(EDocumentTypes.DebitNote, versionPrinting, paymentType);
        }

        /// <summary>
        /// Generate proform invoice.
        /// </summary>
        /// <param name="versionPrinting">Versions of document to print.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <returns>Returns true if a proform invoice was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool GenerateProformInvoice(EDocumentVersionsPrinting versionPrinting = EDocumentVersionsPrinting.Original, EPaymentTypes paymentType = EPaymentTypes.Cash)
        {
            return this.GenerateDocument(EDocumentTypes.ProformInvoice, versionPrinting, paymentType);
        }

        /// <summary>
        /// Generate receipt.
        /// </summary>
        /// <param name="versionPrinting">Versions of document to print.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <returns>Returns true if a receipt was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool GenerateReceipt(EDocumentVersionsPrinting versionPrinting = EDocumentVersionsPrinting.Original, EPaymentTypes paymentType = EPaymentTypes.Cash)
        {
            return this.GenerateDocument(EDocumentTypes.Receipt, versionPrinting, paymentType);
        }

        /// <summary>
        /// Save document.
        /// </summary>
        /// <param name="path">Path to save document.</param>
        /// <returns>Returns true if a document was saved successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public bool SaveDocument(string path)
        {
            // если документ размечен - сохраняем его и возвращаем true
            if (this.pdfDocument.IsRenderedDocument)
            {
                this.pdfDocument.Save(path);

                return true;
            }

            // иначе возвращаем false
            return false;
        }

        /// <summary>
        /// Convert document to list with images.
        /// </summary>
        /// <returns>Returns true if a document was converted to image list successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        public List<Image> ConvertDocumentToImageList()
        {
            // если документ размечен - возвращаем коллекцию с изображениями страниц
            if (this.pdfDocument.IsRenderedDocument)
            {
                return this.pdfDocument.ConvertPdfToImage();
            }

            // иначе возвращаем пустую коллекцию
            return new List<Image>();
        }

        /// <summary>
        /// Sets parameter of a pdf page if page parameter was changed.
        /// </summary>
        /// <param name="sender">DocumentPageModel.</param>
        /// <param name="e">Event of args.</param>
        /// <date>18.03.2022.</date>
        private void PageParameters_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.pageParameters.LeftMargin):
                    this.pdfDocument.LeftIndentation = this.pageParameters.LeftMargin;
                    break;
                case nameof(this.pageParameters.TopMargin):
                    this.pdfDocument.TopIndentation = this.pageParameters.TopMargin;
                    break;
                case nameof(this.pageParameters.RightMargin):
                    this.pdfDocument.RightIndentation = this.pageParameters.RightMargin;
                    break;
                case nameof(this.pageParameters.BottomMargin):
                    this.pdfDocument.BottomIndentation = this.pageParameters.BottomMargin;
                    break;
                case nameof(this.pageParameters.PageOrientation):
                    this.pdfDocument.Orientation = this.pageParameters.PageOrientation;
                    break;
                case nameof(this.pageParameters.PageFormat):
                    this.pdfDocument.PageFormat = this.pageParameters.PageFormat;
                    break;
            }
        }

        /// <summary>
        /// Sets image to component of a pdf document.
        /// </summary>
        /// <param name="property">Name of pdf document field.</param>
        /// <param name="imagePath">Path of image.</param>
        /// <param name="image">Image to add to pdf document.</param>
        /// <date>18.03.2022.</date>
        private void SetImageValues(ref string property, string imagePath, ref Image image)
        {
            // если файл существует
            if (System.IO.File.Exists(imagePath))
            {
                // получаем разрешение файла
                int lastPointIndex = imagePath.LastIndexOf('.');
                string fileFormat = string.Empty;
                if (lastPointIndex > 0)
                {
                    fileFormat = imagePath.Substring(lastPointIndex + 1, imagePath.Length - lastPointIndex).ToLower();
                }

                // если данный файл является изображением - сохраняем параметры в соответствующих полях
                if (!string.IsNullOrEmpty(fileFormat) && (fileFormat.Equals("png") || fileFormat.Equals("jpg") || fileFormat.Equals("jpeg") || fileFormat.Equals("bmp")))
                {
                    property = imagePath;
                    image = Image.FromFile(property);
                }
                else
                {
                    // иначе "бросаем исключение" для информирования о некорректном формате файла
                    throw new Exception("Incorrect image format!!!");
                }
            }
            else
            {
                // иначе "бросаем исключение" для информирования об отсутствии файла по указанному пути
                throw new Exception("File not exist!!!");
            }
        }

        /// <summary>
        /// Generates document.
        /// </summary>
        /// <param name="documentType">Type of a document.</param>
        /// <param name="versionPrinting">Version of a document to print.</param>
        /// <param name="paymentTypes">Order payment type.</param>
        /// <returns>Returns true if a header and footer of document was generated successfully; otherwise returns false.</returns>
        /// <date>18.03.2022.</date>
        [Obsolete]
        private bool GenerateDocument(EDocumentTypes documentType, EDocumentVersionsPrinting versionPrinting, EPaymentTypes paymentTypes)
        {
            // очищаем предыдущую разметку (если таковая осталась с предыдущего раза)
            this.pdfDocument.Clear();

            try
            {
                // восстанавливаем высоту "задела", выделенного для верхнего колонтитула первой страницы
                this.pdfDocument.FirstPageHeaderHeight = this.firstPageHeaderHeight;

                // размечаем верхний и нижний колонтитулы
                this.pdfDocument.CreateDefaultHeaderFooterGrid();

                // добавляем в ячейку 0Х0 верхнего колонтитула первой страницы изображение с логотипом нашей фирмы
                this.pdfDocument.AddNewItemToFirsPageHeaderFooterGrid(
                    EDocumentAreas.Header,
                    this.pdfDocument.CreateImageObject(
                        this.logo,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.FirstPageHeaderGrid, 0),
                        EHorizontalAlignments.Left,
                        this.pdfDocument.FirstPageHeaderHeight),
                    0,
                    0);

                // добавляем в ячейку 0х1 верхнего колонтитула первой страницы реквизиты нашей фирмы
                this.pdfDocument.AddNewItemToFirsPageHeaderFooterGrid(
                    EDocumentAreas.Header,
                    this.pdfDocument.PrepareOurCompanyData(
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.FirstPageHeaderGrid, 1) / 3,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.FirstPageHeaderGrid, 1) / 3 * 2),
                    0,
                    1);

                // добавляем в ячейку 0х1 нижнего колонтитула первой страницы изображение
                this.pdfDocument.AddNewItemToFirsPageHeaderFooterGrid(
                    EDocumentAreas.Footer,
                    this.pdfDocument.CreateImageObject(
                        this.signature,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.FirstPageFooterGrid, 1),
                        EHorizontalAlignments.Right,
                        this.pdfDocument.FooterHeight),
                    0,
                    1);

                // в ячейке 0х0 нижнего колонтитула будет находиться нумерация строк (вставлена при выполнении функции "CreateDefaultHeaderFooterGrid")

                // добавляем в ячейку 0х1 нижнего колонтитула изображение
                this.pdfDocument.AddNewItemToHeaderFooterGrid(
                    EDocumentAreas.Footer,
                    this.pdfDocument.CreateImageObject(
                        this.signature,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.FirstPageFooterGrid, 1),
                        EHorizontalAlignments.Right,
                        this.pdfDocument.FooterHeight),
                    0,
                    1);

                // формируем тело документа в зависимости от выбранных пользователем параметров
                switch (versionPrinting)
                {
                    case EDocumentVersionsPrinting.Original:
                        this.GenerateDocumentBody(EDocumentAuthenticities.Original, documentType, paymentTypes);
                        break;
                    case EDocumentVersionsPrinting.Copy:
                        this.GenerateDocumentBody(EDocumentAuthenticities.Copy, documentType, paymentTypes);
                        break;
                    case EDocumentVersionsPrinting.OriginalAndCopy:
                        // формируем оригинал документа
                        this.GenerateDocumentBody(EDocumentAuthenticities.Original, documentType, paymentTypes);

                        // добавляем секцию для следующего экземпляра документа
                        this.pdfDocument.AddSection();

                        // формируем копию документа
                        this.GenerateDocumentBody(EDocumentAuthenticities.Copy, documentType, paymentTypes);
                        break;
                    case EDocumentVersionsPrinting.OriginalAndTwoCopies:
                        this.GenerateDocumentBody(EDocumentAuthenticities.Original, documentType, paymentTypes);
                        this.pdfDocument.AddSection();
                        this.GenerateDocumentBody(EDocumentAuthenticities.Copy, documentType, paymentTypes);
                        this.pdfDocument.AddSection();
                        this.GenerateDocumentBody(EDocumentAuthenticities.Copy, documentType, paymentTypes);
                        break;
                    default:
                        break;
                }

                // генерируем разметку
                this.pdfDocument.RenderDocument();

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Generates of a body of document.
        /// </summary>
        /// <param name="documentAuthenticity">Version of a document to print.</param>
        /// <param name="documentType">Type of a document.</param>
        /// <param name="paymentType">Order payment type.</param>
        /// <date>18.03.2022.</date>
        private void GenerateDocumentBody(EDocumentAuthenticities documentAuthenticity, EDocumentTypes documentType, EPaymentTypes paymentType)
        {
            // создаём и размечаем блок с данными о клиенте и документе
            this.pdfDocument.CreateCaptionSection();

            // добаляем данный блок в тело документа
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.CaptionSection);

            // добавляем отступ
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.CreateSpace(1.3));

            // добавляем в тело документа таблицу с товарами
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.PrepareOperationData(this.ItemsTableColumnsWidth));

            // добавляем в тело документа таблицу с разбивкой по таварам в зависимости от групп НДС
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.PrepareVATData(4, 2));

            // создаём и размечаем блок с информацией об оплате и описании документа
            this.pdfDocument.CreateAdditionalDataSection();

            // добавляем в тело данный блок 
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.AdditionalDataSection);

            // добавляем в тело документа информацию о том, кто составил данный документ и кто его получит
            this.pdfDocument.AddNewItemToContent(this.pdfDocument.PrepareSignatureData());

            this.pdfDocument.Document.DocumentName = documentType.ToString(); // взять со словаря!!!
            this.pdfDocument.Document.DocumentDate = DateTime.Now;
            this.pdfDocument.Document.DocumentAuthenticity = documentAuthenticity;
            this.pdfDocument.Document.PaymentType = paymentType.ToString(); // взять со словаря!!!
            this.pdfDocument.Document.DealReason = "Продажба"; // взять со словаря!!!

            switch (documentType)
            {
                case EDocumentTypes.Invoice:
                    this.pdfDocument.Document.DocumentDescription = string.Empty;
                    this.pdfDocument.Document.SourceDocumentNumber = string.Empty;
                    this.pdfDocument.Document.SourceDocumentDate = DateTime.Now;
                    break;
                case EDocumentTypes.DebitNote:
                    break;
                case EDocumentTypes.CreditNote:
                    break;
                case EDocumentTypes.ProformInvoice:
                    this.pdfDocument.Document.DocumentDescription = string.Empty;
                    this.pdfDocument.Document.SourceDocumentNumber = string.Empty;
                    this.pdfDocument.Document.SourceDocumentDate = DateTime.Now;
                    break;
                case EDocumentTypes.Receipt:
                    this.pdfDocument.Document.DocumentDescription = "за продажба на стоки"; // взять со словаря в зависимости от типа операции!!!
                    this.pdfDocument.Document.SourceDocumentNumber = string.Empty;
                    this.pdfDocument.Document.SourceDocumentDate = DateTime.Now;

                    this.pdfDocument.Document.DocumentAuthenticity = EDocumentAuthenticities.Unknown;
                    break;
                default:
                    break;
            }


            // вставляем в ячейку 0Х0 CaptionSection информацию о клиенте
            this.pdfDocument.AddNewItemToCaptionSection(
                this.pdfDocument.PrepareClientData(
                    this.pdfDocument.GetColumnWidth(
                        this.pdfDocument.CaptionSection, 0) / 4,
                    this.pdfDocument.GetColumnWidth(
                        this.pdfDocument.CaptionSection, 0) / 4 * 3
                    ),
                0,
                0);
            // вставляем в ячейку 0Х1 CaptionSection информацию о документе
            this.pdfDocument.AddNewItemToCaptionSection(
                this.pdfDocument.PrepareDocumentData(
                    this.pdfDocument.GetColumnWidth(
                        this.pdfDocument.CaptionSection, 1) / 2,
                    this.pdfDocument.GetColumnWidth(
                        this.pdfDocument.CaptionSection, 1) / 2
                    ),
                0,
                1);

            // вставляем в ячейку 0Х0 AdditionalDataSection информацию об оплате
            this.pdfDocument.AddNewItemToAdditionalDataSection(
                this.pdfDocument.PreparePaymentData(
                    this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 0) / 2,
                    this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 0) / 2),
                0,
                0
                );

            // если данный документ не является квитанцией
            if (documentType != EDocumentTypes.Receipt)
            {
                // вставляем в ячейку 1Х0 AdditionalDataSection информацию о банке
                this.pdfDocument.AddNewItemToAdditionalDataSection(
                    this.pdfDocument.PrepareBankData(
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 0) / 4,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 0) / 4 * 3),
                    1,
                    0
                    );

                // вставляем в ячейку 1Х1 AdditionalDataSection описание по текущей сделке 
                this.pdfDocument.AddNewItemToAdditionalDataSection(
                    this.pdfDocument.PrepareDealDescriptionData(
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 1) / 2,
                        this.pdfDocument.GetColumnWidth(this.pdfDocument.AdditionalDataSection, 1) / 2),
                    1,
                    1
                    );
            }
        }

    }
}
